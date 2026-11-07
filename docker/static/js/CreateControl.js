//1. Biến gr_InstallPath: tham số sau dấu bằng là thư mục trang web chứa tệp cài đặt plugin, thường được tìm địa chỉ bắt đầu từ thư mục
//   gốc của trang web, tệp cài đặt plugin nhất định phải tồn tại trong thư mục đã chỉ định.
//2. gr_Version: tham số sau dấu bằng là số phiên bản của gói cài đặt plugin, nếu có gói cài đặt plugin phiên bản mới thì nên tải lên phiên
//   bản mới của tệp cài đặt plugin vào thư mục tương ứng của trang web, và cập nhật số phiên bản ở đây.
//3. Để biết thêm chi tiết, vui lòng tham khảo phần “Plugin báo cáo (báo cáo WEB) -> Triển khai gói cài đặt plugin trên máy chủ” trong trợ giúp
var gr_InstallPath = "grinstall"; //Trong dự án thực tế nên viết thư mục được tìm địa chỉ từ thư mục gốc, ví dụ gr_InstallPath="/myapp/report/grinstall"; 
var gr_Version = "5,8,14,626";

//Số đăng ký dưới đây là số đăng ký phát triển và kiểm thử trên máy này, khi địa chỉ truy cập báo cáo là localhost thì có thể bỏ cờ dùng thử
//Sau khi mua và đăng ký, vui lòng dùng tên người dùng đăng ký và số đăng ký của bạn để thay thế giá trị trong biến dưới đây
var gr_UserName = '锐浪报表插件本机开发测试注册';
var gr_SerialNo = '4DFB949E066NYS7W11L8KAT53SA177391Q9LZQ094WUT9C9J3813SX8PTQC4ALPB9UAQN6TMA55Q3BN8E5726Z5A839QAD9P6E76TKNK5';

//Plugin báo cáo hiện chỉ có thể dùng trong trình duyệt 32-bit
var _gr_platform = window.navigator.platform;
if (_gr_platform.indexOf("64") > 0)
    alert("Plugin báo cáo 锐浪 Grid++Report không thể chạy trong trình duyệt 64-bit, các chức năng báo cáo và in liên quan sẽ không thể hoạt động bình thường, vui lòng chuyển sang trình duyệt 32-bit!");

//Phân biệt trình duyệt (IE hay không)
var _gr_agent = navigator.userAgent.toLowerCase();
var _gr_isIE = (_gr_agent.indexOf("msie")>0)? true : false;

var gr_CodeBase;
if( _gr_isIE )
    gr_CodeBase = 'codebase="' + gr_InstallPath + '/grbsctl5.cab#Version=' + gr_Version + '"';
else
    gr_CodeBase = '';

//Tạo đối tượng báo cáo, đối tượng báo cáo là đối tượng không nhìn thấy được, để biết chi tiết vui lòng xem IGridppReport trong trợ giúp
//Name - Chỉ định ID của đối tượng plugin, có thể dùng mã js document.getElementById("%Name%") để lấy đối tượng báo cáo
//EventParams - Chỉ định sự kiện mà đối tượng báo cáo cần phản hồi, ví dụ dạng "<param name='OnInitialize' value=OnInitialize> <param name='OnProcessBegin' value=OnProcessBegin>", có thể chỉ định nhiều sự kiện
function CreateReport(PluginID, EventParams)
{
    var typeid;
    if( _gr_isIE )
        typeid = 'classid="clsid:25240C9A-6AA5-416c-8CDA-801BBAF03928" ';
    else
        typeid = 'type="application/x-grplugin-report" ';
    typeid += gr_CodeBase;
	document.write('<object id="' + PluginID + '" ' + typeid);
	document.write(' width="0" height="0" VIEWASTEXT>');
	if (EventParams != undefined)
	    document.write(EventParams);
	document.write('</object>');
	
	document.write('<script type="text/javascript">');
	    document.write(PluginID + '.Register("' + gr_UserName + '", "' + gr_SerialNo + '");');
	document.write('</script>');
}

//Tạo plugin hiển thị in báo cáo với nhiều tham số hơn, để biết chi tiết vui lòng xem trong trợ giúp IGRPrintViewer
//PluginID - ID của plugin, có thể thông qua var ReportViewer = document.getElementById("%PluginID%"); cách này để lấy biến tham chiếu của plugin
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//ReportURL - URL để lấy mẫu báo cáo
//DataURL - URL để lấy dữ liệu báo cáo
//AutoRun - Chỉ định plugin sau khi tạo có tự động tạo và hiển thị báo cáo không, giá trị là false hoặc true
//ExParams - Chỉ định thêm phần trình bày thuộc tính của plugin, có dạng: "<param name="%ParamName%" value="%Value%">" — chuỗi tham số như vậy
function CreatePrintViewerEx2(PluginID, Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    var typeid;
    if( _gr_isIE )
        typeid = 'classid="clsid:B7EF88E6-A0AD-4235-B418-6F07D8533A9F" ' + gr_CodeBase;
    else
        typeid = 'type="application/x-grplugin-printviewer"';
	document.write('<object id="' + PluginID + '" ' + typeid);
	document.write(' width="' + Width + '" height="' + Height + '">');
	document.write('<param name="ReportURL" value="' + ReportURL + '">');
	document.write('<param name="DataURL" value="' + DataURL + '">');
	document.write('<param name="AutoRun" value=' + AutoRun + '>');
	document.write('<param name="SerialNo" value="' + gr_SerialNo + '">');
	document.write('<param name="UserName" value="' + gr_UserName + '">');
	document.write(ExParams);
	document.write('</object>');
}

//Tạo plugin hiển thị in báo cáo với nhiều tham số hơn, để biết chi tiết vui lòng xem trong trợ giúp IGRDisplayViewer
//PluginID - ID của plugin, có thể thông qua var ReportViewer = document.getElementById("%PluginID%"); cách này để lấy biến tham chiếu của plugin
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//ReportURL - URL để lấy mẫu báo cáo
//DataURL - URL để lấy dữ liệu báo cáo
//AutoRun - Chỉ định plugin sau khi tạo có tự động tạo và hiển thị báo cáo không, giá trị là false hoặc true
//ExParams - Chỉ định thêm phần trình bày thuộc tính của plugin, có dạng: "<param name="%ParamName%" value="%Value%">" — chuỗi tham số như vậy
function CreateDisplayViewerEx2(PluginID, Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    var typeid;
    if( _gr_isIE )
        typeid = 'classid="clsid:CB45DFE5-6C35-4687-B790-FEC65D512859" ' + gr_CodeBase;
    else
        typeid = 'type="application/x-grplugin-displayviewer"';
	document.write('<object id="' + PluginID + '" ' + typeid);
	document.write(' width="' + Width + '" height="' + Height + '">');
	document.write('<param name="ReportURL" value="' + ReportURL + '">');
	document.write('<param name="DataURL" value="' + DataURL + '">');
	document.write('<param name="AutoRun" value=' + AutoRun + '>');
	document.write('<param name="SerialNo" value="' + gr_SerialNo + '">');
	document.write('<param name="UserName" value="' + gr_UserName + '">');
	document.write(ExParams);
	document.write('</object>');
}

//Tạo plugin thiết kế báo cáo (Designer) với ID là ReportDesigner, để biết chi tiết vui lòng xem IGRDesigner trong trợ giúp
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao của toàn bộ vùng hiển thị, "500" biểu thị 500 điểm ảnh màn hình
//LoadReportURL - URL để đọc mẫu báo cáo, khi chạy sẽ đọc dữ liệu mẫu báo cáo từ URL này và tải vào plugin thiết kế
//SaveReportURL - URL để lưu mẫu báo cáo, lưu dữ liệu kết quả sau khi thiết kế, dịch vụ của URL này sẽ lưu mẫu báo cáo một cách bền vững ở phía máy chủ WEB
//DataURL - URL để lấy dữ liệu khi báo cáo chạy, khi vào chế độ xem in và chế độ xem truy vấn trong trình thiết kế thì lấy dữ liệu báo cáo từ URL này
//ExParams - Chỉ định thêm phần trình bày thuộc tính của plugin, có dạng: "<param name="%ParamName%" value="%Value%">" — chuỗi tham số như vậy
function CreateDesignerEx(Width, Height, LoadReportURL, SaveReportURL, DataURL, ExParams)
{
    var typeid;
    if( _gr_isIE )
        typeid = 'classid="clsid:3C19F439-B64D-4dfb-A96A-661FE70EA04D" ' + gr_CodeBase;
    else
        typeid = 'type="application/x-grplugin-designer"';
	document.write('<object id="ReportDesigner" ' + typeid);
	document.write(' width="' + Width + '" height="' + Height + '">');
	document.write('<param name="LoadReportURL" value="' + LoadReportURL + '">');
	document.write('<param name="SaveReportURL" value="' + SaveReportURL + '">');
	document.write('<param name="DataURL" value="' + DataURL + '">');
	document.write('<param name="SerialNo" value="' + gr_SerialNo + '">');
	document.write('<param name="UserName" value="' + gr_UserName + '">');
	document.write(ExParams);
	document.write('</object>');
}

//Tạo plugin hiển thị in báo cáo (PrintViewer) với ID là ReportViewer, tham khảo phần giải thích tham số CreatePrintViewerEx2
function CreatePrintViewerEx(Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    CreatePrintViewerEx2("ReportViewer", Width, Height, ReportURL, DataURL, AutoRun, ExParams)
}

//Tạo plugin hiển thị truy vấn báo cáo (DisplayViewer) với ID là ReportViewer, tham khảo phần giải thích tham số CreateDisplayViewerEx2
function CreateDisplayViewerEx(Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    CreateDisplayViewerEx2("ReportViewer", Width, Height, ReportURL, DataURL, AutoRun, ExParams)
}

//Tạo plugin hiển thị in báo cáo (PrintViewer) với ID là ReportViewer, kích thước plugin là 100% lấp đầy vùng vị trí, sau khi tạo plugin sẽ tự động chạy, tham khảo phần giải thích tham số CreatePrintViewerEx2
function CreatePrintViewer(ReportURL, DataURL)
{
    CreatePrintViewerEx("100%", "100%", ReportURL, DataURL, true, "");
}

//Tạo plugin hiển thị truy vấn báo cáo (DisplayViewer) với ID là ReportViewer, kích thước plugin là 100% lấp đầy vùng vị trí, sau khi tạo plugin sẽ tự động chạy, tham khảo phần giải thích tham số CreateDisplayViewerEx2
function CreateDisplayViewer(ReportURL, DataURL)
{
    CreateDisplayViewerEx("100%", "100%", ReportURL, DataURL, true, "");
}

//Tạo plugin thiết kế báo cáo (Designer) với ID là ReportDesigner, kích thước plugin là 100% lấp đầy vùng vị trí, tham khảo phần giải thích tham số CreateDesignerEx
function CreateDesigner(LoadReportURL, SaveReportURL, DataURL)
{
    CreateDesignerEx("100%", "100%", LoadReportURL, SaveReportURL, DataURL, "");
}