// domtool.js
// 新建
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
// 打开本地文件
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
                //要用post方式      
                type: "Post",
                //方法所在页面和方法名      
                url: "http://localhost:8456/ServicePage.aspx/UploadFiles",
                contentType: "application/json; charset=utf-8",
                data: "{'filename':'" + fileinfo.name + "' ,'base64Content':'" + base64 + "'}",
                dataType: "Text",
                success: function (data) {
                    // console.log("成功");
                    var upFileAddress = JSON.parse(data).d;
                    loadFile(upFileAddress);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // console.log("失败");
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
// 保存文件
function MySaveFile() {

    if ($("#ljwb").text() != "") {
        var ctl = document.getElementById('myWriterControl');
        if (ctl.DCExecuteCommand('FileSave', true, null) == true) {
            //alert("Lưu thành công。服务器端返回:" + ctl.ServerMessage);
            savePrompt("Lưu thành công", "green");
        } else {
            //alert("Lưu thất bại。服务器端返回:" + ctl.ServerMessage);
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
                // console.log("成功");
                // console.log(data);
                savePrompt("Lưu thành công", "green");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                // console.log("失败");
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
//打印文档
function PrintDocument() {
    var ctl = document.getElementById("myWriterControl");
    ctl.PrintDocument();
}
//设置续打
function JumpPrint(setValue) {
    var ctl = document.getElementById("myWriterControl");
    ctl.SetJumpPrintMode(setValue);
    document.getElementById("chkMouseDragScroll").checked = false;
    ctl.setMouseDragScrollMode(false);
}
//设置在预览中，使用鼠标滚轮拖拽
function SetMouseDragScroll(setValue) {
    var ctl = document.getElementById("myWriterControl");
    ctl.setMouseDragScrollMode(setValue);
}
//显示打印预览内容
function OpenPreview() {
    //获得最外面的容器元素并设置位置
    var ctl = document.getElementById("myWriterControl");
    //填充打印预览内容
    ctl.LoadPrintPreview();
}
//关闭打印预览
function ClosePrintPreview() {
    var ctl = document.getElementById("myWriterControl");
    ctl.ClosePrintPreview();
}
//Chế độ biểu mẫu
function changeview(obj) {
    // 获得控件容器元素
    var ctl = document.getElementById("myWriterControl");
    // 绑定控件
    BindingDCWriterClientControl(ctl);
    // 设置控件属性
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.Options.FreeSelection = "true";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // 用Tab键在各个输入域之间切换
    ctl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = "Tab";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.EnableEncryptView = true;
    ctl.DocumentOptions.ViewOptions.PrintBackgroundText = true;
    ctl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
    if (obj.text == "Chế độ biểu mẫu") {
        ctl.Options.FormView = "Normal";
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ sửa</span></span>";
    } else {
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ biểu mẫu</span></span>";
    }
    ctl.ToolbarForPrintPreview = document.getElementById("btrPrintPreview");
    // 创建编辑器框架
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // 打印页面模式，则鼠标拖拽滚动.
            ctl.setMouseDragScrollMode(true);
        } else {
            // 不启用鼠标拖拽滚动。
            ctl.setMouseDragScrollMode(false);
        }
    });

    // 文档加载后事件处理，由于采用AJAX模式，并不确定LoadDocumentFormFile()返回后是否会触发这个事件
    // 因此需要在并不确定LoadDocumentFormFile()函数前设置好这个事件处理代码。
    ctl.DocumentLoad = function () {
        // 文档加载后设置控件的鼠标拖拽滚动模式
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Đã tạo" +
                this.serverPerformances.characters + "ký tự，thời gian xử lý nhân biên tập" +
                this.serverPerformances.serverTicks + "ms,thời gian xử lý phía máy chủ" +
                this.serverPerformances.totalServerTicks + "ms ，thời gian xử lý phía máy khách" +
                this.serverPerformances.clientTicks + "ms。";
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
            //alert("双击了输入域:" + input.id);
        }
    };
}
//Chế độ tự động co giãn
function changeAutoZoom(obj) {
    // 获得控件容器元素
    var ctl = document.getElementById("myWriterControl");
    // 绑定控件
    BindingDCWriterClientControl(ctl);
    // 设置控件属性
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // 用Tab键在各个输入域之间切换
    ctl.DocumentOptions.BehaviorOptions.MoveFocusHotKey = "Tab";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.EnableEncryptView = true;
    ctl.DocumentOptions.ViewOptions.PrintBackgroundText = true;
    ctl.DocumentOptions.ViewOptions.PreserveBackgroundTextWhenPrint = true;
    if (obj.text == "Chế độ tự động co giãn") {
        ctl.Options.AutoZoom = true;
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ sửa</span></span>";
    } else {
        ctl.Options.AutoZoom = false;
        obj.innerHTML = "<span class='l-btn-left'><span class='l-btn-text'>Chế độ tự động co giãn</span></span>";
    }
    ctl.ToolbarForPrintPreview = document.getElementById("btrPrintPreview");
    // 创建编辑器框架
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // 打印页面模式，则鼠标拖拽滚动.
            ctl.setMouseDragScrollMode(true);
        } else {
            // 不启用鼠标拖拽滚动。
            ctl.setMouseDragScrollMode(false);
        }
    });

    // 文档加载后事件处理，由于采用AJAX模式，并不确定LoadDocumentFormFile()返回后是否会触发这个事件
    // 因此需要在并不确定LoadDocumentFormFile()函数前设置好这个事件处理代码。
    var htm = $("#ljwb").text();
    ctl.DocumentLoad = function () {
        // 文档加载后设置控件的鼠标拖拽滚动模式
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Đã tạo" +
                this.serverPerformances.characters + "ký tự，thời gian xử lý nhân biên tập" +
                this.serverPerformances.serverTicks + "ms,thời gian xử lý phía máy chủ" +
                this.serverPerformances.totalServerTicks + "ms ，thời gian xử lý phía máy khách" +
                this.serverPerformances.clientTicks + "ms。";
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
            //alert("双击了输入域:" + input.id);
        }
    };
}
// HTML页面加载后执行本函数
function BuildDCWriterControlFrame() {
    // 获得控件容器元素
    var ctl = document.getElementById("myWriterControl");
    // 绑定控件
    BindingDCWriterClientControl(ctl);
    // 设置控件属性
    ctl.Options.ExcludeKeywords = "月经,子宫,卵巢";
    ctl.DocumentOptions.BehaviorOptions.AcceptDataFormats = "Text";
    ctl.DocumentOptions.ViewOptions.ShowInputFieldStateTag = true;
    ctl.DocumentOptions.ViewOptions.FieldInvalidateValueBackColor = "Yellow";
    // 用Tab键在各个输入域之间切换
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
    //        btn.value = "数值" + iCount;
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
    // 创建编辑器框架
    ctl.BuildFrame(function () {
        if (ctl.Options.ContentRenderMode == "PagePreviewHtml") {
            // 打印页面模式，则鼠标拖拽滚动.
            ctl.setMouseDragScrollMode(true);
        } else {
            // 不启用鼠标拖拽滚动。
            ctl.setMouseDragScrollMode(false);
        }
    });
    // 文档加载后事件处理，由于采用AJAX模式，并不确定LoadDocumentFormFile()返回后是否会触发这个事件
    // 因此需要在并不确定LoadDocumentFormFile()函数前设置好这个事件处理代码。
    ctl.DocumentLoad = function () {
        // 文档加载后设置控件的鼠标拖拽滚动模式
        if (this.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Đã tạo" +
                this.serverPerformances.characters + "ký tự，thời gian xử lý nhân biên tập" +
                this.serverPerformances.serverTicks + "ms,thời gian xử lý phía máy chủ" +
                this.serverPerformances.totalServerTicks + "ms ，thời gian xử lý phía máy khách" +
                this.serverPerformances.clientTicks + "ms。";
        }
        //firstFile 第一次加载文档
        AjaxLoad("入院记录.xml");
    };
    ctl.ondocumentdblclick = function (eventObject) {
        var input = this.CurrentInputField();
        if (input != null) {
            //alert("双击了输入域:" + input.id);
        }
    };
}
function AjaxLoad(fileName) {
    $("#uploadljwb").text("");
    $("#ljwb").html(fileName);
    var ctl = document.getElementById("myWriterControl");
    document.getElementById("lblServerTime").innerHTML = "|Đang tải...";
    ctl.preserveScrollPosition = true; // 加载文档时尽量保留页面滚动位置。
    // 文档加载后事件处理，由于采用AJAX模式，并不确定LoadDocumentFormFile()返回后是否会触发这个事件
    // 因此需要在并不确定LoadDocumentFormFile()函数前设置好这个事件处理代码。
    ctl.DocumentLoad = function () {
        ctl.setMouseDragScrollMode(false); // 文档加载后设置控件的鼠标拖拽滚动模式
        var performances = ctl.getServerPerformances();
        if (ctl.serverPerformances) {
            document.getElementById("lblServerTime").innerHTML = "|Đã tạo" +
                ctl.serverPerformances.characters + "ký tự，thời gian xử lý nhân biên tập" +
                ctl.serverPerformances.serverTicks + "ms,thời gian xử lý phía máy chủ" +
                ctl.serverPerformances.totalServerTicks + "ms ，thời gian xử lý phía máy khách" +
                ctl.serverPerformances.clientTicks + "ms。";
        }
    }
    // 在服务器端响应控件的EventReadFileContent事件，即可使用AJAX模式加载文档。
    ctl.LoadDocumentFromFile(fileName, null);
}
function loadFile(name) {
    $.ajax({
        url: "/" + name,
        dataType: 'text',
        type: 'GET',
        timeout: 2000,
        error: function () {
            alert("Tải tệp XML bị lỗi！");
        },
        success: function (xml) {
            var ctl = document.getElementById("myWriterControl");
            ctl.LoadDocumentFromString(xml, "xml");
            $("#ljwb").text("");
            $("#uploadljwb").text(name);
        }
    });
}
// 获取html
function setHtml() {
    var html = document.getElementById('myWriterControl').SelectionHtml(); //String
    $("#zshtml").toggle();
    $("#mask").toggle();
    //转义一些字符
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
// 获取xml
function setXml() {
    var doc = '<?xml version="1.0" encoding="utf-8" ?>\n';
    var xml = document.getElementById('myWriterControl').SelectionXml(); //String
    $("#zsxml").toggle();
    $("#mask").toggle();
    xml = doc + xml;
    $("#xmlcon").text(xml);
}
// 设置元素文本
function MySetElementText() {
    var ctl = document.getElementById("myWriterControl");
    var node = ctl.CurrentInputField();
    // 获得初始化的ID值
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
            alert("Giá trị văn bản cũ là:" + oldText + "  Giá trị văn bản mới là:" + newText + "  Kết quả thao tác:" + result);
        }
    }
}
//删除当前输入域
function DeleteField() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('DeleteField', false, null);
}
// 自定义右键菜单
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
        label: 'Cắt (Ctrl + x)',
        cmdName: 'cut',
        exec: function () {
            ctl.DCExecuteCommand('Cut', true, null);
        }
    },
    {
        label: 'Dán nội bộ',
        cmdName: 'paste',
        exec: function () {
            ctl.DCExecuteCommand('Paste', true, null);//粘贴内部复制或者剪切的
        }
    }
    ];
    var ctl = document.getElementById("myWriterControl");
    ctl.SetContextMenu(jsonMenu);
}
//文档校验

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
// 插入输入域
function MyExecuteCommand(lst) {
    switch (lst.getAttribute("val")) {
        case "InsertAgeField":// 插入年龄域
            MyInsertNameInputField();
            break;
        case "InsertSexField":// 插入性别域
            MyInsertSexInputField();
            break;
        case "InsertDateTime":// 插入日期域
            MyInsertDateInputField();
            break;
        case "InsertInputCheckbox":// 插入复选框
            MyInsertRadioOrCheckBoxInputField("checkbox");
            break;
        case "InsertInputRadio":// 插入单选框
            MyInsertRadioOrCheckBoxInputField("radio");
            break;
        case "InsertInputButton":// 插入按钮
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
// 插入年龄输入域
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
        "UnitText": "tuổi"
    };
    var field = ctl.DCExecuteCommand("InsertInputField", true, options);
    if (field != null) {
        ctl.FocusAdjacent("afterEnd", field); //这里的第一个参数可以是beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}
// 插入性别输入域
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
            "Text": "Nam",
            "Value": "1"
        },
        {
            "Text": "Nữ",
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
        ctl.FocusAdjacent("afterEnd", field); //这里的第一个参数可以是beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}
// 插入日期输入域
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
        ctl.FocusAdjacent("afterEnd", field); //这里的第一个参数可以是beforeBegin,afterBegin,beforeEnd,afterEnd
    }
}

// 插入单/复选框
function MyInsertRadioOrCheckBoxInputField(type) {
    var ctl = document.getElementById("myWriterControl");
    var options = {
        "Name": "name001", //单选框的Name属性相同
        "Type": type, //radio、checkbox
        "ListItems": [
            {
                "ID": "name001-1",
                "ToolTip": "Thông tin gợi ý",
                "Text": "Nội dung 1",
                "Value": "Giá trị 1"
            },
            {
                "ID": "name001-2",
                "ToolTip": "Thông tin gợi ý",
                "Text": "Nội dung 2",
                "Value": "Giá trị 2"
            },
            {
                "ID": "name001-3",
                "ToolTip": "Thông tin gợi ý",
                "Text": "Nội dung 3",
                "Value": "Giá trị 3"
            }
        ]
    };
    ctl.DCExecuteCommand("insertcheckboxorradio", true, options);
    //if (field != null) {
    //    ctl.FocusAdjacent("afterEnd", field); //这里的第一个参数可以是beforeBegin,afterBegin,beforeEnd,afterEnd
    //}
}

// 插入医学表达式
function MedicalExpressionMenu() {
    var $ul = $("#MedicalExpressionMenu");
    var MedicalExpression = [{
        "name": "通用公式",
        "ExpressionStyle": "FourValues1",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "月经史公式",
        "ExpressionStyle": "FourValues2",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "月经史公式",
        "ExpressionStyle": "ThreeValues",
        "Values": "Value1:14;Value2:14;Value3:14;"
    },
    {
        "name": "月经史公式",
        "ExpressionStyle": "FourValues",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14"
    },
    {
        "name": "瞳孔图公式",
        "ExpressionStyle": "Pupil",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6: 14; Value7: 14; "
    },
    {
        "name": "光定位图公式",
        "ExpressionStyle": "LightPositioning",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14"
    },
    {
        "name": "胎心图公式",
        "ExpressionStyle": "FetalHeart",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14"
    },
    {
        "name": "标尺公式",
        "ExpressionStyle": "PainIndex",
        "Values": "Value1:14;"
    },
    {
        "name": "恒牙牙位图公式",
        "ExpressionStyle": "PermanentTeethBitmap",
        "Values": "Value1:14;Value2:14;Value3:14;Value4:14;Value5:14;Value6:14;Value7:14;Value8:14;Value9:14;Value10:14;Value11:14;Value12:14;Value13:14;Value14:14;Value15:14;Value16:14;Value17:14;Value18:14;Value19:14;Value20:14;Value21:14;Value22:14;Value23:14;Value24:14;Value25:14;Value26:14;Value27:14;Value28:14;Value29:14;Value30:14;Value31:14;Value32:14"
    },
    {
        "name": "乳牙位图公式",
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
function MyInsertMedicalExpression(index, ExpressionStyle, Values) { //通用公式
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
    //                ctl.FocusAdjacent("afterEnd", field); //这里的第一个参数可以是beforeBegin,afterBegin,beforeEnd,afterEnd
    //            }
}
//5.0版本
function MedicalExpressionMenuV5() {
    var ctl = document.getElementById("myWriterControl");
    ctl.DCExecuteCommand('InsertMedicalExpression', true, {})
}

// OCX加载
function MyOCX() {
    window.open("IEWebDemo.htm");
}
//增加大小的显示与隐藏
$(".height-size").hover(function () {
    $(".height-top").toggle();
    $(".height-bottom").toggle();
})
$(".width-size").hover(function () {
    $(".width-top").toggle();
    $(".width-bottom").toggle();
})
//文本变化事件
$(".number-input>input").bind('input propertychange', function () {
    if ($(this).val() < 100 || $(this).val() > 500) {
        $(this).css("border", "1px solid #ef4e2f").siblings("span.text2").show();
    } else {
        $(this).css("border", "1px solid #d4d4d4").siblings("span.text2").hide();
    }
    compare();
});
//加
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
//减
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
    //段前距
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
    //段后距
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
    //行间距
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
    //字体背景颜色
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
    //字体颜色
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
    //字体样式
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
    //字体大小
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
    //插入特殊字符
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
    //插入表格
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



    //对齐方式
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
    //图片对齐方式
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
