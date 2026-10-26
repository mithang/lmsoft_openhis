//1. Tham số sau dấu bằng của biến gr_InstallPath là thư mục website nơi chứa tệp cài đặt plugin, thông thường bắt đầu từ thư mục gốc của website
//   Bắt đầu tìm địa chỉ, tệp cài đặt plugin nhất định phải tồn tại trong thư mục đã chỉ định.
//2. Tham số sau dấu bằng của gr_Version là số phiên bản của gói cài đặt plugin, nếu có gói cài đặt plugin phiên bản mới thì nên tải lên phiên bản mới
//   Tệp cài đặt plugin này lên thư mục tương ứng của website và cập nhật số phiên bản tại đây.
//3、Để biết thêm chi tiết, vui lòng tham khảo phần “Plugin báo cáo (Báo cáo WEB) -> Triển khai gói cài đặt plugin trên máy chủ” trong trợ giúp
var gr_InstallPath = "grinstall"; //Trong dự án thực tế nên viết thư mục tìm địa chỉ từ thư mục gốc, ví dụ gr_InstallPath="/myapp/report/grinstall"; 
var gr_Version = "5,8,14,626";

//Số đăng ký dưới đây là số đăng ký thử nghiệm phát triển trên máy cục bộ, khi địa chỉ truy cập báo cáo là localhost thì có thể bỏ cờ dùng thử
//Sau khi mua và đăng ký, vui lòng dùng tên người dùng đăng ký và số đăng ký của bạn để thay thế giá trị trong biến dưới đây
var gr_UserName = '锐浪报表插件本机开发测试注册';
var gr_SerialNo = '4DFB949E066NYS7W11L8KAT53SA177391Q9LZQ094WUT9C9J3813SX8PTQC4ALPB9UAQN6TMA55Q3BN8E5726Z5A839QAD9P6E76TKNK5';

//Plugin báo cáo hiện tại chỉ có thể dùng trong trình duyệt 32-bit
var _gr_platform = window.navigator.platform;
if (_gr_platform.indexOf("64") > 0)
    alert("Plugin báo cáo Grid++Report của Ruilang không thể chạy trên trình duyệt 64-bit, các chức năng báo cáo và in ấn liên quan sẽ không hoạt động bình thường, vui lòng dùng trình duyệt 32-bit!");

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
//EventParams - Chỉ định các sự kiện mà đối tượng báo cáo cần phản hồi, ví dụ dạng "<param name='OnInitialize' value=OnInitialize> <param name='OnProcessBegin' value=OnProcessBegin>", có thể chỉ định nhiều sự kiện
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

//Tạo plugin hiển thị in báo cáo với nhiều tham số hơn, để biết chi tiết vui lòng xem IGRPrintViewer trong trợ giúp
//PluginID - ID của plugin, có thể lấy biến tham chiếu của plugin bằng cách var ReportViewer = document.getElementById("%PluginID%");
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//ReportURL - URL lấy mẫu báo cáo
//DataURL - URL lấy dữ liệu báo cáo
//AutoRun - Chỉ định plugin sau khi tạo có tự động tạo và hiển thị báo cáo hay không, giá trị là false hoặc true
//ExParams - Chỉ định diễn giải thêm về thuộc tính plugin, dạng chuỗi tham số như: "<param name="%ParamName%" value="%Value%">"
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

//Tạo plugin hiển thị truy vấn báo cáo với nhiều tham số hơn, để biết chi tiết vui lòng xem IGRDisplayViewer trong trợ giúp
//PluginID - ID của plugin, có thể lấy biến tham chiếu của plugin bằng cách var ReportViewer = document.getElementById("%PluginID%");
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//ReportURL - URL lấy mẫu báo cáo
//DataURL - URL lấy dữ liệu báo cáo
//AutoRun - Chỉ định plugin sau khi tạo có tự động tạo và hiển thị báo cáo hay không, giá trị là false hoặc true
//ExParams - Chỉ định diễn giải thêm về thuộc tính plugin, dạng chuỗi tham số như: "<param name="%ParamName%" value="%Value%">"
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

//Tạo plugin trình thiết kế báo cáo (Designer) với ID là ReportDesigner, để biết chi tiết vui lòng xem IGRDesigner trong trợ giúp
//Width - Chiều rộng hiển thị của plugin, "100%" là chiều rộng toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//Height - Chiều cao hiển thị của plugin, "100%" là chiều cao toàn bộ vùng hiển thị, "500" nghĩa là 500 điểm ảnh màn hình
//LoadReportURL - URL đọc mẫu báo cáo, khi chạy sẽ đọc dữ liệu mẫu báo cáo từ URL này và tải vào plugin trình thiết kế
//SaveReportURL - URL lưu mẫu báo cáo, lưu dữ liệu kết quả sau khi thiết kế, dịch vụ tại URL này sẽ lưu bền mẫu báo cáo ở phía máy chủ WEB
//DataURL - URL lấy dữ liệu khi báo cáo chạy, khi vào chế độ xem in và chế độ xem truy vấn trong trình thiết kế thì lấy dữ liệu báo cáo từ URL này
//ExParams - Chỉ định diễn giải thêm về thuộc tính plugin, dạng chuỗi tham số như: "<param name="%ParamName%" value="%Value%">"
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

//Tạo plugin hiển thị in báo cáo (PrintViewer) với ID là ReportViewer, giải thích tham số tham khảo CreatePrintViewerEx2
function CreatePrintViewerEx(Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    CreatePrintViewerEx2("ReportViewer", Width, Height, ReportURL, DataURL, AutoRun, ExParams)
}

//Tạo plugin hiển thị truy vấn báo cáo (DisplayViewer) với ID là ReportViewer, giải thích tham số tham khảo CreateDisplayViewerEx2
function CreateDisplayViewerEx(Width, Height, ReportURL, DataURL, AutoRun, ExParams)
{
    CreateDisplayViewerEx2("ReportViewer", Width, Height, ReportURL, DataURL, AutoRun, ExParams)
}

//Tạo plugin hiển thị in báo cáo (PrintViewer) với ID là ReportViewer, kích thước plugin là 100% lấp đầy vùng vị trí, sau khi tạo plugin sẽ tự động chạy, giải thích tham số tham khảo CreatePrintViewerEx2
function CreatePrintViewer(ReportURL, DataURL)
{
    CreatePrintViewerEx("100%", "100%", ReportURL, DataURL, true, "");
}

//Tạo plugin hiển thị truy vấn báo cáo (DisplayViewer) với ID là ReportViewer, kích thước plugin là 100% lấp đầy vùng vị trí, sau khi tạo plugin sẽ tự động chạy, giải thích tham số tham khảo CreateDisplayViewerEx2
function CreateDisplayViewer(ReportURL, DataURL)
{
    CreateDisplayViewerEx("100%", "100%", ReportURL, DataURL, true, "");
}

//Tạo plugin trình thiết kế báo cáo (Designer) với ID là ReportDesigner, kích thước plugin là 100% lấp đầy vùng vị trí, giải thích tham số tham khảo CreateDesignerEx
function CreateDesigner(LoadReportURL, SaveReportURL, DataURL)
{
    CreateDesignerEx("100%", "100%", LoadReportURL, SaveReportURL, DataURL, "");
}