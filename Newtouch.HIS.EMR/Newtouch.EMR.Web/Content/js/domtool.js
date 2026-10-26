// domtool.js
// Tạo mới
function newFile() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('FileNew', true, null);
    $("#ljwb").text("");
    $("#uploadljwb").text("");
}
function FileOpen() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('FileOpen', true, null);
}
// Mở tệp cục bộ
function openLocalFile() {
    var inputBox = document.getElementById("sfile");
    if (inputBox.files[0] && (inputBox.files[0].type == "text/xml" || inputBox.files[0].type == "application/xml")) {
        var fileinfo = inputBox.files[0];
        var reader = new FileReader();
        reader.readAsDataURL(fileinfo);
        reader.onload = function () {
            var base64 = reader.result;
            //var ctl = document.getElementById("myWriterControl");
            //var str = base64.substr(base64.indexOf("base64,") + 7, base64.length);
            //ctl.LoadDocumentFromBase64String(str, "xml");
            $.ajax({
                //Phải dùng phương thức post      
                type: "Post",
                //Trang chứa phương thức và tên phương thức      
                url: "http://localhost:8456/ServicePage.aspx/UploadFiles",
                contentType: "application/json; charset=utf-8",
                data: "{'filename':'" + fileinfo.name + "' ,'base64Content':'" + base64 + "'}",
                dataType: "Text",
                success: function (data) {
                    // console.log("Thành công");
                    var upFileAddress = JSON.parse(data).d;
                    loadFile(upFileAddress);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // console.log("Thất bại");
                    alert("Mở tệp thất bại");
                }
            });
        };
        reader.onerror = function (error) {
            console.log(error);
        }
    }
    inputBox.value = "";
}
// Lưu tệp
function MySaveFile() {

    if ($("#ljwb").text() != "") {
        var ctl = document.getElementById('myWriterControl');
        if (ctl.DCExecuteCommand('FileSave', true, null) == true) {
            //alert("Lưu thành công. Phía máy chủ trả về:" + ctl.ServerMessage);
            savePrompt("Lưu thành công", "green");
        } else {
            //alert("Lưu thất bại. Phía máy chủ trả về:" + ctl.ServerMessage);
            savePrompt("Lưu thất bại", "red");
        }
    } else {
        var ctl = document.getElementById('myWriterControl');
        var fileContent = ctl.SaveDocumentToBase64String("XML");
        // console.log(fileContent);
        var file = $("#uploadljwb").text().replace(/\//g, "\\\\");
        //console.log(file);
        $.ajax({
            type: "Post",
            url: "http://localhost:8456/ServicePage.aspx/SaveFiles",
            contentType: "application/json; charset=utf-8",
            data: "{'filename':'" + file + "' ,'xmlContent':'" + fileContent + "'}",
            dataType: "Text",
            success: function (data) {
                // console.log("Thành công");
                // console.log(data);
                savePrompt("Lưu thành công", "green");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // console.log("Thất bại");
                savePrompt("Lưu thất bại", "red");
            }
        });
    }
}
function savePrompt(msg, color) {
    $("#savePrompt").text(msg).css({ "top": $("#myWriterControl").offset().top - 60, "background-color": color, "transform": "scale(1)" });
    setTimeout(function () {
        $("#savePrompt").css({ "transform": "scale(0)" });
    }, 1000)
}
//In tài liệu
function PrintDocument() {
    var ctl = document.getElementById("myWriterControl");
    ctl.PrintDocument();
}
//Đặt in tiếp
function JumpPrint(setValue) {
    var ctl = document.getElementById("myWriterControl");
    ctl.SetJumpPrintMode(setValue);
    document.getElementById("chkMouseDragScroll").checked = false;
    ctl.setMouseDragScrollMode(false);
}
//Đặt trong bản xem trước, dùng bánh xe chuột để kéo
function SetMouseDragScroll(setValue) {
    var ctl = document.getElementById("myWriterControl");
    ctl.setMouseDragScrollMode(setValue);
}
//Hiển thị nội dung xem trước khi in
function OpenPreview() {
    //Lấy phần tử chứa ngoài cùng và đặt vị trí
    var ctl = document.getElementById("myWriterControl");
    //Điền nội dung xem trước khi in
    ctl.LoadPrintPreview();
}
//Đóng xem trước khi in
function ClosePrintPreview() {
    var ctl = document.getElementById("myWriterControl");
    ctl.ClosePrintPreview();
}
//Chế độ biểu mẫu
function changeview(obj) {
    // Lấy phần tử chứa điều khiển
    var ctl = document.getElementById("myWriterControl");
    // Ràng buộc điều khiển
    BindingDCWriterClientControl(ctl);
    // Đặt thuộc tính điều khiển
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.Options.FreeSelection = "true";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // Dùng phím Tab để chuyển đổi giữa các trường nhập
    ctl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = "Tab";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.EnableEncryptView = true;
    ctl.DocumentOptions.ViewOptions.PrintBackgroundText = true;
    ctl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
    if (obj.text == "Chế độ biểu mẫu") {
        ctl.Options.FormView = "Normal";
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ chỉnh sửa</span></span>";
    } else {
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ biểu mẫu</span></span>";
    }
    ctl.ToolbarForPrintPreview = document.getElementById("btrPrintPreview");
    // Tạo khung trình soạn thảo
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // Ở chế độ trang in, cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(true);
        } else {
            // Không bật cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(false);
        }
    });

    // Xử lý sự kiện sau khi tài liệu tải xong, do dùng chế độ AJAX nên không chắc sự kiện này có được kích hoạt sau khi LoadDocumentFormFile() trả về hay không
    // Do đó cần đặt sẵn mã xử lý sự kiện này trước hàm LoadDocumentFormFile() mà không chắc chắn.
    ctl.DocumentLoad = function () {
        // Sau khi tài liệu tải xong, đặt chế độ cuộn kéo thả chuột của điều khiển
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Tạo" +
                this.serverPerformances.characters + "ký tự, nhân xử lý của trình soạn thảo tốn" +
                this.serverPerformances.serverTicks + "mili giây, phía máy chủ tốn" +
                this.serverPerformances.totalServerTicks + "mili giây, phía máy khách tốn" +
                this.serverPerformances.clientTicks + "mili giây.";
        }
        var htm = $("#ljwb").text();
        if (htm != "") {
            AjaxLoad(htm);
        } else {
            ctl.DocumentLoad = function () { }
            loadFile($("#uploadljwb").text());
        }
    };
    ctl.ondocumentdblclick = function (eventObject) {
        var input = this.CurrentInputField();
        if (input != null) {
            //alert("Đã nhấp đúp vào trường nhập:" + input.id);
        }
    };
}
//Chế độ tự động thu phóng
function changeAutoZoom(obj) {
    // Lấy phần tử chứa điều khiển
    var ctl = document.getElementById("myWriterControl");
    // Ràng buộc điều khiển
    BindingDCWriterClientControl(ctl);
    // Đặt thuộc tính điều khiển
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // Dùng phím Tab để chuyển đổi giữa các trường nhập
    ctl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = "Tab";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.EnableEncryptView = true;
    ctl.DocumentOptions.ViewOptions.PrintBackgroundText = true;
    ctl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
    if (obj.text == "Chế độ tự động thu phóng") {
        ctl.Options.AutoZoom = true;
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ chỉnh sửa</span></span>";
    } else {
        ctl.Options.AutoZoom = false;
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ tự động thu phóng</span></span>";
    }
    ctl.ToolbarForPrintPreview = document.getElementById("btrPrintPreview");
    // Tạo khung trình soạn thảo
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // Ở chế độ trang in, cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(true);
        } else {
            // Không bật cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(false);
        }
    });

    // Xử lý sự kiện sau khi tài liệu tải xong, do dùng chế độ AJAX nên không chắc sự kiện này có được kích hoạt sau khi LoadDocumentFormFile() trả về hay không
    // Do đó cần đặt sẵn mã xử lý sự kiện này trước hàm LoadDocumentFormFile() mà không chắc chắn.
    var htm = $("#ljwb").text();
    ctl.DocumentLoad = function () {
        // Sau khi tài liệu tải xong, đặt chế độ cuộn kéo thả chuột của điều khiển
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Tạo" +
                this.serverPerformances.characters + "ký tự, nhân xử lý của trình soạn thảo tốn" +
                this.serverPerformances.serverTicks + "mili giây, phía máy chủ tốn" +
                this.serverPerformances.totalServerTicks + "mili giây, phía máy khách tốn" +
                this.serverPerformances.clientTicks + "mili giây.";
        }
        var htm = $("#ljwb").text();
        if (htm != "") {
            AjaxLoad(htm);
        } else {
            ctl.DocumentLoad = function () { }
            loadFile($("#uploadljwb").text());
        }
    };

    ctl.ondocumentdblclick = function (eventObject) {
        var input = this.CurrentInputField();
        if (input != null) {
            //alert("Đã nhấp đúp vào trường nhập:" + input.id);
        }
    };
}
// Thực thi hàm này sau khi trang HTML tải xong
function BuildDCWriterControlFrame() {
    // Lấy phần tử chứa điều khiển
    var ctl = document.getElementById("myWriterControl");
    // Ràng buộc điều khiển
    BindingDCWriterClientControl(ctl);
    // Đặt thuộc tính điều khiển
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // Dùng phím Tab để chuyển đổi giữa các trường nhập
    ctl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = "Tab";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.EnableEncryptView = true;
    ctl.DocumentOptions.ViewOptions.PrintBackgroundText = true;
    ctl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
    //ctl.Options.FormView = "Normal";
    ctl.ToolbarForPrintPreview = document.getElementById("btrPrintPreview");
    //ctl.EventDropdownEditor = function (args) {
    //    for (var iCount = 0; iCount < 20; iCount++) {
    //        var btn = document.createElement("input");
    //        btn.type = "button";
    //        btn.value = "Giá trị số" + iCount;
    //        btn.style.width = "200px";
    //        btn.onclick = function () {
    //            args.setValue(this.value, this.value + "_value");
    //            args.close();
    //        };
    //        args.container.appendChild(btn);
    //        args.container.appendChild(document.createElement("br"));
    //    }
    //    args.container.style.width = "210px";
    //    args.container.style.height = "300px";
    //}
    //ctl.DocumentOptions.BehaviorOptions.AutoClearTextFormatWhenPasteOrInsertContent = true;
    // Tạo khung trình soạn thảo
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // Ở chế độ trang in, cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(true);
        } else {
            // Không bật cuộn kéo thả chuột.
            ctl.setMouseDragScrollMode(false);
        }
    });
    // Xử lý sự kiện sau khi tài liệu tải xong, do dùng chế độ AJAX nên không chắc sự kiện này có được kích hoạt sau khi LoadDocumentFormFile() trả về hay không
    // Do đó cần đặt sẵn mã xử lý sự kiện này trước hàm LoadDocumentFormFile() mà không chắc chắn.
    ctl.DocumentLoad = function () {
        // Sau khi tài liệu tải xong, đặt chế độ cuộn kéo thả chuột của điều khiển
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Tạo" +
                this.serverPerformances.characters + "ký tự, nhân xử lý của trình soạn thảo tốn" +
                this.serverPerformances.serverTicks + "mili giây, phía máy chủ tốn" +
                this.serverPerformances.totalServerTicks + "mili giây, phía máy khách tốn" +
                this.serverPerformances.clientTicks + "mili giây.";
        }
        //firstFile Lần đầu tải tài liệu
        AjaxLoad("入院记录.xml");
    };
    ctl.ondocumentdblclick = function (eventObject) {
        var input = this.CurrentInputField();
        if (input != null) {
            //alert("Đã nhấp đúp vào trường nhập:" + input.id);
        }
    };
}
function AjaxLoad(fileName) {
    $("#uploadljwb").text("");
    $("#ljwb").html(fileName);
    var ctl = document.getElementById("myWriterControl");
    document.getElementById("lblServerTime").innerHTML = "|Đang tải...";
    ctl.preserveScrollPosition = true; // Khi tải tài liệu, cố gắng giữ nguyên vị trí cuộn trang.
    // Xử lý sự kiện sau khi tài liệu tải xong, do dùng chế độ AJAX nên không chắc sự kiện này có được kích hoạt sau khi LoadDocumentFormFile() trả về hay không
    // Do đó cần đặt sẵn mã xử lý sự kiện này trước hàm LoadDocumentFormFile() mà không chắc chắn.
    ctl.DocumentLoad = function () {
        ctl.setMouseDragScrollMode(false); // Sau khi tài liệu tải xong, đặt chế độ cuộn kéo thả chuột của điều khiển
        var performances = ctl.getServerPerformances();
        if (ctl.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Tạo" +
                ctl.serverPerformances.characters + "ký tự, nhân xử lý của trình soạn thảo tốn" +
                ctl.serverPerformances.serverTicks + "mili giây, phía máy chủ tốn" +
                ctl.serverPerformances.totalServerTicks + "mili giây, phía máy khách tốn" +
                ctl.serverPerformances.clientTicks + "mili giây.";
        }
    }
    // Khi phía máy chủ phản hồi sự kiện EventReadFileContent của điều khiển, có thể tải tài liệu ở chế độ AJAX.
    ctl.LoadDocumentFromFile(fileName, null);
}
function loadFile(name) {
    $.ajax({
        url: "/" + name,
        dataType: 'text',
        type: 'GET',
        timeout: 2000,
        error: function () {
            alert("Lỗi tải tệp XML!");
        },
        success: function (xml) {
            var ctl = document.getElementById("myWriterControl");
            ctl.LoadDocumentFromString(xml, "xml");
            $("#ljwb").text("");
            $("#uploadljwb").text(name);
        }
    });
}
// Lấy html
function setHtml() {
    var html = document.getElementById('myWriterControl').SelectionHtml(); //String
    $("#zshtml").toggle();
    $("#mask").toggle();
    //Thoát một số ký tự
    if (html != null && html.indexOf("$") > 0) {
        html = html.replace(/[$]/g, "@")
        html = html.replace(/@3C/g, "<");
        html = html.replace(/@3E/g, ">");
        html = html.replace(/@25/g, "%");
        html = html.replace(/@26quot;/g, "\'");
        html = html.replace(/dcr@/, "<!DOCTYPE html>");
    }
    $("#htmlcon").text(html);
}
// Lấy xml
function setXml() {
    var doc = '<?xml version="1.0" encoding="utf-8" ?>\n';
    var xml = document.getElementById('myWriterControl').SelectionXml(); //String
    $("#zsxml").toggle();
    $("#mask").toggle();
    xml = doc + xml;
    $("#xmlcon").text(xml);
}
// Đặt văn bản phần tử
function MySetElementText() {
    var ctl = document.getElementById("myWriterControl");
    var node = ctl.CurrentInputField();
    // Lấy giá trị ID khởi tạo
    var initID = "";
    while (node != null) {
        initID = node.id;
        if (initID != null && initID.length > 0) {
            break;
        }
        node = node.parentNode;
    }
    var id = window.prompt("Vui lòng nhập ID phần tử", initID);
    if (id == null || id == "") {
        alert("Vui lòng nhập ID phần tử");
        return false;
    } else {
        var text = window.prompt("Vui lòng nhập văn bản");
        if (id != null && id.length > 0) {
            var oldText = ctl.GetElementTextByID(id);
            var newText = text;
            var result = ctl.SetElementTextByID(id, newText);
            alert("Giá trị văn bản cũ là:" + oldText + "  Giá trị văn bản mới:" + newText + "  Kết quả thao tác:" + result);
        }
    }
}
//Xóa trường nhập hiện tại
function DeleteField() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('DeleteField', false, null);
}
// Menu chuột phải tùy chỉnh
function MySetContextMenu() {
    var jsonMenu = [{
        label: 'Đây là menu do người dùng thêm',
        cmdName: 'cleardoc',
        icon: 'aligntd',
        exec: function () {
            alert("Thêm một menu");
        }
    },
    {
        label: '',
        cmdName: 'selectall'
    },
    {
        label: '',
        cmdName: 'cleardoc',
        exec: function () {
            ctl.DCExecuteCommand('cleardoc');
        }
    },
        '-',
    {
        group: '',
        icon: 'justifyjustify',
        subMenu: [{
            label: '',
            cmdName: 'justify',
            value: 'left'
        },
        {
            label: '',
            cmdName: 'justify',
            value: 'right'
        },
        {
            label: '',
            cmdName: 'justify',
            value: 'center'
        },
        {
            label: '',
            cmdName: 'justify',
            value: 'justify'
        }
        ]
    },
    {
        cmdName: 'copy'
    },
    {
        label: 'Cắt (Ctrl + X)',
        cmdName: 'cut',
        exec: function () {
            ctl.DCExecuteCommand('Cut', true, null);
        }
    },
    {
        label: 'Dán nội bộ',
        cmdName: 'paste',
        exec: function () {
            ctl.DCExecuteCommand('Paste', true, null);//Dán nội dung đã sao chép hoặc cắt nội bộ
        }
    }
    ];
    var ctl = document.getElementById("myWriterControl");
    ctl.SetContextMenu(jsonMenu);
}
//Kiểm tra tài liệu

function MyValueValidate() {
    var result = document.getElementById('myWriterControl').DCExecuteCommand("DocumentValueValidate",
        false, null);
    var sel = document.getElementById("validateResult");
    if (sel == null) {
        return;
    }
    while (sel.firstChild != null) {
        sel.removeChild(sel.firstChild);
    }
    if (result != null && result.length > 0) {
        for (var iCount = 0; iCount < result.length; iCount++) {
            var item = result[iCount];
            var opt = document.createElement("option");
            opt.result = item;
            opt.appendChild(document.createTextNode(iCount + " |" + item.ElementID + " " + item
                .Message));
            sel.appendChild(opt);
        }
    }
}
function validateResult_Click() {
    var sel = document.getElementById("validateResult");
    if (sel == null || sel.selectedIndex < 0) {
        return;
    }
    var opt = sel.options[sel.selectedIndex];
    var result = opt.result;
    result.Select();
}
// Chèn trường nhập
function MyExecuteCommand(lst) {
    switch (lst.getAttribute("val")) {
        case "InsertAgeField":// Chèn trường tuổi
            MyInsertNameInputField();
            break;
        case "InsertSexField":// Chèn trường giới tính
            MyInsertSexInputField();
            break;
        case "InsertDateTime":// Chèn trường ngày
            MyInsertDateInputField();
            break;
        case "InsertInputCheckbox":// Chèn hộp chọn nhiều
            MyInsertRadioOrCheckBoxInputField("checkbox");
            break;
        case "InsertInputRadio":// Chèn hộp chọn đơn
            MyInsertRadioOrCheckBoxInputField("radio");
            break;
        case "InsertInputButton":// Chèn nút
            MyInsertButtonInputField();
            break;
        default:
            var ctl = document.getElementById("myWriterControl");
            var nam = lst.getAttribute("val");
            var sho = lst.getAttribute("showui") == "true";
            var par = lst.getAttribute("parameter");
            ctl.DCExecuteCommand(nam, sho, par);
    }
}
// Chèn trường nhập tuổi
function MyInsertNameInputField() {
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "ContentReadonly": false,
        "ToolTip": "Vui lòng nhập tuổi",
        "BackgroundText": "Tuổi",
        "Text": "30",
        "InnerValue": "30",
        "ValidateStyle": "Required:True;ValueType:Numeric;CheckMaxValue:True;MaxValue:150;CheckMinValue:True;MinValue:0",
        "EnableValueValidate": true,
        "Name": "Tuổi",
        "ID": "txtAge",
        "StartBorderText": "【",
        "EndBorderText": "】",
        "UnitText": "岁"
    };
    var field = ctl.DCExecuteCommand("InsertInputField", true, options);
    if (field != null) {
        ctl.FocusAdjacent("afterEnd", field); //Tham số đầu tiên ở đây có thể là beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}
// Chèn trường nhập giới tính
function MyInsertSexInputField() {
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "ContentReadonly": false,
        "ToolTip": "Vui lòng chọn giới tính",
        "BackgroundText": "Giới tính",
        "EnableValueValidate": true,
        "Name": "Giới tính",
        "ID": "txtSex",
        "StartBorderText": "<",
        "EndBorderText": ">",
        "ListItems": [{
            "Text": "男",
            "Value": "1"
        },
        {
            "Text": "女",
            "Value": "0"
        },
        {
            "Text": "Khác",
            "Value": "2"
        }
        ],
        "InnerEditStyle": "DropdownList",
        "EditorActiveMode": "MouseClick"
    };
    var field = ctl.DCExecuteCommand("InsertInputField", true, options);
    if (field != null) {
        ctl.FocusAdjacent("afterEnd", field); //Tham số đầu tiên ở đây có thể là beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}
// Chèn trường nhập ngày
function MyInsertDateInputField() {
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "ContentReadonly": false,
        "ToolTip": "Vui lòng nhập ngày",
        "BackgroundText": "Ngày",
        "Text": "2015-01-09 09:39",
        "InnerEditStyle": "DateTime",
        "EditorActiveMode": "MouseClick",
        "EnableValueValidate": true,
        "Name": "Ngày",
        "ID": "txtDate"
    };
    var field = ctl.DCExecuteCommand("InsertInputField", true, options);
    if (field != null) {
        ctl.FocusAdjacent("afterEnd", field); //Tham số đầu tiên ở đây có thể là beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}

// Chèn hộp chọn đơn/nhiều
function MyInsertRadioOrCheckBoxInputField(type) {
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "Name": "name001", //Thuộc tính Name của các hộp chọn đơn giống nhau
        "Type": type, //radio、checkbox
        "ListItems": [
            {
                "ID": "name001-1",
                "ToolTip": "Thông tin nhắc nhở",
                "Text": "Nội dung 1",
                "Value": "Giá trị 1"
            },
            {
                "ID": "name001-2",
                "ToolTip": "Thông tin nhắc nhở",
                "Text": "Nội dung 2",
                "Value": "Giá trị 2"
            },
            {
                "ID": "name001-3",
                "ToolTip": "Thông tin nhắc nhở",
                "Text": "Nội dung 3",
                "Value": "Giá trị 3"
            }
        ]
    };
    ctl.DCExecuteCommand("insertcheckboxorradio", true, options);
    //if (field != null) {
    //    ctl.FocusAdjacent("afterEnd", field); //Tham số đầu tiên ở đây có thể là beforeBegin,afterBegin,beforeEnd,afterEnd
    //}
}

// Chèn biểu thức y học
function MedicalExpressionMenu() {
    var $ul = $("#MedicalExpressionMenu");
    var MedicalExpression = [{
        "name": "Công thức chung",
        "ExpressionStyle": "FourValues1",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "Công thức tiền sử kinh nguyệt",
        "ExpressionStyle": "FourValues2",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "Công thức tiền sử kinh nguyệt",
        "ExpressionStyle": "ThreeValues",
        "Values": "Value1:14;Value2:14;Value3:14;"
    },
    {
        "name": "Công thức tiền sử kinh nguyệt",
        "ExpressionStyle": "FourValues",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "Công thức sơ đồ đồng tử",
        "ExpressionStyle": "Pupil",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6: 14; Value7: 14; "
    },
    {
        "name": "Công thức sơ đồ định vị ánh sáng",
        "ExpressionStyle": "LightPositioning",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14"
    },
    {
        "name": "Công thức sơ đồ tim thai",
        "ExpressionStyle": "FetalHeart",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14"
    },
    {
        "name": "Công thức thước",
        "ExpressionStyle": "PainIndex",
        "Values": "Value1:14;"
    },
    {
        "name": "Công thức sơ đồ răng vĩnh viễn",
        "ExpressionStyle": "PermanentTeethBitmap",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14;Value10:14;Value11:14;Value12:14;Value13:14;Value14:14;Value15:14;Value16:14;Value17:14;Value18:14;Value19:14;Value20:14;Value21:14;Value22:14;Value23:14;Value24:14;Value25:14;Value26:14;Value27:14;Value28:14;Value29:14;Value30:14;Value31:14;Value32:14"
    },
    {
        "name": "Công thức sơ đồ răng sữa",
        "ExpressionStyle": "DeciduousTeech",
        "Values": "Value1:Ⅴ;Value2:Ⅳ;Value3:Ⅲ;Value4:Ⅱ;Value5:Ⅰ;Value6:Ⅰ;Value7:Ⅱ;Value8:Ⅲ;Value9:Ⅳ;Value10:Ⅴ;Value11:Ⅴ;Value12:Ⅳ;Value13:Ⅲ;Value14:Ⅱ;Value15:Ⅰ;Value16:Ⅰ;Value17:Ⅱ;Value18:Ⅲ;Value19:Ⅳ;Value20:Ⅴ"
    }
    ];
    for (let i in MedicalExpression) {
        var name = MedicalExpression[i]["name"];
        var ExpressionStyle = MedicalExpression[i]["ExpressionStyle"];
        var Values = MedicalExpression[i]["Values"];
        // <option value="value">text</option>
        $ul.append(
            `<option ExpressionStyle="${ExpressionStyle}" Values="${Values}" value="${name}">${name}</option>`
        )
    }
}
function MyInsertMedicalExpression(index, ExpressionStyle, Values) { //Công thức chung
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "ID": "expression" + index,
        "ExpressionStyle": ExpressionStyle,
        "Type": "XTextNewMedicalExpressionElement",
        "FontSize": "12",
        "Width": "112px",
        "Height": "46px",
        "Values": Values
    };
    var field = ctl.DCExecuteCommand("insertmedicalexpression", false, options);
    //            if (field != null) {
    //                ctl.FocusAdjacent("afterEnd", field); //Tham số đầu tiên ở đây có thể là beforeBegin,afterBegin,beforeEnd,afterEnd
    //            }
}
//Phiên bản 5.0
function MedicalExpressionMenuV5() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('InsertMedicalExpression', true, {})
}

// Tải OCX
function MyOCX() {
    window.open("IEWebDemo.htm");
}
//Hiển thị và ẩn tăng kích thước
$(".height-size").hover(function () {
    $(".height-top").toggle();
    $(".height-bottom").toggle();
})
$(".width-size").hover(function () {
    $(".width-top").toggle();
    $(".width-bottom").toggle();
})
//Sự kiện thay đổi văn bản
$(".number-input>input").bind('input propertychange', function () {
    if ($(this).val() < 100 || $(this).val() > 500) {
        $(this).css("border", "1px solid #ef4e2f").siblings("span.text2").show();
    } else {
        $(this).css("border", "1px solid #d4d4d4").siblings("span.text2").hide();
    }
    compare();
});
//Tăng
function accAdd(arg1, arg2) {
    var r1, r2, m, c;
    try {
        r1 = arg1.toString().split(".")[1].length;
    } catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    } catch (e) {
        r2 = 0;
    }
    c = Math.abs(r1 - r2);
    m = Math.pow(10, Math.max(r1, r2));
    if (c > 0) {
        var cm = Math.pow(10, c);
        if (r1 > r2) {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", "")) * cm;
        } else {
            arg1 = Number(arg1.toString().replace(".", "")) * cm;
            arg2 = Number(arg2.toString().replace(".", ""));
        }
    } else {
        arg1 = Number(arg1.toString().replace(".", ""));
        arg2 = Number(arg2.toString().replace(".", ""));
    }
    return (arg1 + arg2) / m;
}
//Giảm
function accSub(arg1, arg2) {
    var r1, r2, m, n;
    try {
        r1 = arg1.toString().split(".")[1].length;
    } catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    } catch (e) {
        r2 = 0;
    }
    m = Math.pow(10, Math.max(r1, r2));
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
}
$(function () {
    //Khoảng cách trước đoạn
    $("#rowspacingup").click(function () {
        $("#rowspacingup1").toggle();
    })
    $("#rowspacingup1").mouseover(function () {
        $("#rowspacingup1").show();
    }).mouseout(function () {
        $("#rowspacingup1").hide();
    })
    $("#rowspacingup1 .combobox-item").click(function () {
        var rowspacing = $(this).text();
        var str = parseInt(rowspacing) + ",top";
        document.getElementById('myWriterControl').DCExecuteCommand('rowspacing', true,
            str);
        $("#rowspacingup1").hide();
    })
    //Khoảng cách sau đoạn
    $("#rowspacingdown").click(function () {
        $("#rowspacingdown1").toggle()
    })
    $("#rowspacingdown1").mouseover(function () {
        $("#rowspacingdown1").show();
    }).mouseout(function () {
        $("#rowspacingdown1").hide();
    })
    $("#rowspacingdown1 .combobox-item").click(function () {
        var rowspacing = $(this).text();
        var str = parseInt(rowspacing) + ",bottom";
        document.getElementById('myWriterControl').DCExecuteCommand('rowspacing', true,
            str);
        $("#rowspacingdown1").hide();
    })
    //Khoảng cách dòng
    $("#lineheight").click(function () {
        $("#lineheight1").toggle();
    })
    $("#lineheight1").mouseover(function () {
        $("#lineheight1").show();
    }).mouseout(function () {
        $("#lineheight1").hide();
    })
    $("#lineheight1 .combobox-item").click(function () {
        var lineheight = $(this).text();
        var str = parseInt(lineheight);
        document.getElementById('myWriterControl').DCExecuteCommand('lineheight', false,
            str);
        $("#lineheight1").hide();
    })
    //Màu nền phông chữ
    $("#backcolor").click(function () {
        $("#bcolor").toggle();
    })
    $(".bgclor span").click(function () {
        var bgc = $(this).attr("data-color");
        document.getElementById('myWriterControl').DCExecuteCommand('BackColor', false,
            bgc);
        $("#bcolor").hide();
        return false;
    })
    $(".bgclor span").mouseover(function () {
        var bgca = $(this).attr("data-color");
        if (bgca == "#ffffff") {
            $("#bcolor .fui-colorpicker-preview").attr("style", "background-color: " +
                bgca + "; border-color: #eeeeee;")
        } else {
            $("#bcolor .fui-colorpicker-preview").attr("style", "background-color: " +
                bgca + "; border-color: " + bgca + ";")
        }
    })
    $("#bcolor").mouseover(function () {
        $("#bcolor").show();
    }).mouseout(function () {
        $("#bcolor").hide();
    })
    //Màu phông chữ
    $("#fontcolor").click(function () {
        $("#fcolor").toggle();
    })
    $(".fgclor span").click(function () {
        var bgc = $(this).attr("data-color");
        document.getElementById('myWriterControl').DCExecuteCommand('Color', false,
            bgc);
        $("#fcolor").hide();
        return false;
    })
    $(".fgclor span").mouseover(function () {
        var bgcaa = $(this).attr("data-color");
        if (bgcaa == "#ffffff") {
            $("#fcolor .fui-colorpicker-preview").attr("style", "background-color: " +
                bgcaa + "; border-color: #eeeeee;")
        } else {
            $("#fcolor .fui-colorpicker-preview").attr("style", "background-color: " +
                bgcaa + "; border-color: " + bgcaa + ";")
        }
    })
    $("#fcolor").mouseover(function () {
        $("#fcolor").attr("style", "display:block");
    }).mouseout(function () {
        $("#fcolor").attr("style", "display:none");
    })
    //Kiểu phông chữ
    $("#fontfa").click(function () {
        $("#fontf").toggle();
    })
    $("#fontf").mouseover(function () {
        $("#fontf").show();
    }).mouseout(function () {
        $("#fontf").hide();
    })
    $("#fontf .combobox-item").click(function () {
        var fontF = $(this).text();
        document.getElementById('myWriterControl').DCExecuteCommand('FontName', true,
            fontF);
        $("#fontf").hide();
    })
    //Cỡ phông chữ
    $("#fontsize").click(function () {
        $("#fontsizeli").toggle();
    })
    $("#fontsizeli").mouseover(function () {
        $("#fontsizeli").show();
    }).mouseout(function () {
        $("#fontsizeli").hide();
    })
    $("#fontsizeli .combobox-item").click(function () {
        var fontSize = $(this).text();
        document.getElementById('myWriterControl').DCExecuteCommand('fontSize', true,
            fontSize);
        $("#fontsizeli").hide();
    })
    //Chèn ký tự đặc biệt
    $("#insertszf").click(function () {
        $("#tszfa").toggle();
        $("#mask").toggle();
    })
    $("#tabHeads span").click(function () {
        $(this).addClass("focus");
        $(this).siblings("span").removeClass("focus");
        var aaa = $(this).attr("tabsrc");
        $("#" + aaa).show().siblings("div").hide();
    })
    $("#tabBodys span").click(function () {
        var tszfc = $(this).text();
        document.getElementById('myWriterControl').DCExecuteCommand('Spechars', true,
            '' + tszfc + '');
        $("#tszfa").hide();
        $("#mask").hide();
    })
    
    $("#closeimga").click(function () {
        $("#zshtml").hide();
        $("#mask").hide();
    })
    $("#closeimgb").click(function () {
        $("#zsxml").hide();
        $("#mask").hide();
    })
    //Chèn bảng
    $("#inserttab").click(function () {
        $("#table").toggle()
    })
    $("#qd").click(function () {
        var row = $("#row").val();
        var column = $("#column").val();
        var tt = column + "," + row;
        document.getElementById('myWriterControl').DCExecuteCommand('inserttable', true,
            tt);
        $("#table").attr("style", "display:none");
    })
    var tophe = $(".abq").height();
    var zzgd = tophe + 114;
    $("#kjgd").attr("style", "height: calc(100vh - " + zzgd + "px);");
    /*$("#inserttable").click(function () {
        if ($("#table").attr("style") == "display:block") {
            $("#table").attr("style", "display:none");
        } else {
            $("#table").attr("style", "display:block");
        }
    })*/
    /*$("#qd").click(function () {
        var row = $("#row").val();
        var column = $("#column").val();
        var tt = column + "," + row;
        document.getElementById('myWriterControl').DCExecuteCommand('inserttable', true,
            tt);
        $("#table").attr("style", "display:none");
    })*/



    //Kiểu căn lề
    $("#textaline").click(function () {
        /*if ($("#textaline1").attr("style") == "display:block") {
            $("#textaline1").attr("style", "display:none");
        } else {
            $("#textaline1").attr("style", "display:block");
        }*/
        $("#textaline1").toggle();
    })
    $("#textalineleft").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('justifyleft', true,
            null);
        $("#textaline1").attr("style", "display:none");
        return false;
    })
    $("#textalineright").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('justifyright',
            true, null);
        $("#textaline1").attr("style", "display:none");
        return false;
    })
    $("#textalinecenter").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('justifycenter',
            true, null);
        $("#textaline1").attr("style", "display:none");
        return false;
    })
    $("#textaline1").mouseover(function () {
        $("#textaline1").attr("style", "display:block");
    }).mouseout(function () {
        $("#textaline1").attr("style", "display:none");
    })
    //Kiểu căn lề hình ảnh
    $("#imagefloat").click(function () {
        /*if ($("#imagefloat1").attr("style") == "display:block") {
            $("#imagefloat1").attr("style", "display:none");
        } else {
            $("#imagefloat1").attr("style", "display:block");
        }*/
        $("#imagefloat1").toggle();
    })
    $("#imagefloatleft").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('imagefloat', true,
            'left');
        $("#imagefloat1").attr("style", "display:none");
        return false;
    })
    $("#imagefloatright").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('imagefloat', true,
            'right');
        $("#imagefloat1").attr("style", "display:none");
        return false;
    })
    $("#imagefloatcenter").click(function () {
        document.getElementById('myWriterControl').DCExecuteCommand('imagefloat', true,
            'center');
        $("#imagefloat1").attr("style", "display:none");
        return false;
    })
    $("#imagefloat1").mouseover(function () {
        $("#imagefloat1").attr("style", "display:block");
    }).mouseout(function () {
        $("#imagefloat1").attr("style", "display:none");
    })
})
