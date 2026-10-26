
//Ngăn hành vi mặc định
function stopDefault(e) {
    e = e || window.event;
    if (e && e.preventDefault) {
        e.preventDefault();
    }
    else if (window.event) {
        window.event.returnValue = false;
    }
    return false;
}

//Ngăn nổi bọt
function stopPropagation(e) {
    e = e || window.event;
    if (e && e.stopPropagation) {   //Nếu đã cung cấp đối tượng sự kiện thì đây là trình duyệt không phải IE
        e.stopPropagation();    //Vì vậy nó hỗ trợ phương thức stopPropagation() của W3C
    }
    else if (window.event) {
        window.event.cancelBubble = true;  //Ngược lại, chúng ta cần dùng cách của IE để hủy nổi bọt sự kiện
    }
    return false;
}

/* Lấy tọa độ của phần tử DOM */
var getDOMPositionPoint = function (obj) { //Lấy vị trí của phần tử so với viền body
    var t = obj.offsetTop; //Lấy lề trên của phần tử tương ứng với vùng chứa cha
    var l = obj.offsetLeft; //Lề trên tương ứng với vùng chứa cha
    //Kiểm tra có vùng chứa cha hay không, nếu có thì cộng dồn lề của nó
    while (obj = obj.offsetParent) {//Tương đương obj = obj.offsetParent; while (obj != undefined)
        t += obj.offsetTop; //Cộng dồn lề trên của vùng chứa cha
        l += obj.offsetLeft; //Cộng dồn lề trái của vùng chứa cha
    }
    return { t: t, l: l };
}

//newtouch global
window.newtouch_globalconfig = {
    $currentActiveWindow: null,  //Đối tượng Window đang được kích hoạt
    f4opions: null,

};

//
var newtouch_setCurrentWindow = function (win) {
    if (!win) {
        win = window;
    }
    try {
        window.top.top.newtouch_globalconfig.$currentActiveWindow = window.top.newtouch_globalconfig.$currentActiveWindow =
            window.newtouch_globalconfig.$currentActiveWindow = $(win);

        if (win.newtouch_globalconfig) {
            window.top.top.newtouch_globalconfig.f4opions = window.top.newtouch_globalconfig.f4opions =
                window.newtouch_globalconfig.f4opions = win.newtouch_globalconfig.f4opions;
        }
    }
    catch (errr) { }
};

var newtouch_getCurrentActiveWindow = function () {
    if (window.newtouch_globalconfig.$currentActiveWindow == null) {
        window.newtouch_globalconfig.$currentActiveWindow = $(window);
    }
    if (window.newtouch_globalconfig.$currentActiveWindow[0].top
        == window.newtouch_globalconfig.$currentActiveWindow[0]) {
        //Nếu là Home/Index thì lấy iframe đang được kích hoạt đó
        var curFrameDataid = $(window.document).find('.page-tabs-content a.active').attr('data-id');
        var $activeIframe = $(window.document).find('.content-iframe iframe[data-id="' + curFrameDataid + '"]');
        newtouch_setCurrentWindow($activeIframe[0].contentWindow)
    }
    return window.newtouch_globalconfig.$currentActiveWindow;
}

//F1
var newtouch_globalevent_f1 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f1) {
        $currentActiveWindow[0].newtouch_event_f1(event);
        return true;
    }
    return false;
};

//F2
var newtouch_globalevent_f2 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f2) {
        $currentActiveWindow[0].newtouch_event_f2(event);
        return true;
    }
    return false;
};

//F3
var newtouch_globalevent_f3 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f3) {
        $currentActiveWindow[0].newtouch_event_f3(event);
        return true;
    }
    return false;
};

//F4 xóa nội dung biểu mẫu của trang
var newtouch_globalevent_f4 = function (event, options) {

    var $currentActiveWindow = newtouch_getCurrentActiveWindow();   //Phải đặt sau window.newtouch_globalconfig.f4opions

    var defaults = {
        container: "body",
        inner: true,   //Có thực hiện phương thức xóa tùy chỉnh bên trong trang hay không
        justinner: false,   //Có ‘chỉ’ thực hiện phương thức xóa tùy chỉnh bên trong trang hay không
    };
    if (options) {
        var options = $.extend(defaults, options);
    }
    else if (window.newtouch_globalconfig.f4opions) {
        var options = $.extend(defaults, window.newtouch_globalconfig.f4opions);
    }
    else {
        options = defaults;
    }

    if (!(options.justinner === true)) {
        var $thisContainer = $($currentActiveWindow[0].document).find(options.container);
        //Xóa trạng thái đã chọn của hộp kiểm
        $thisContainer.find(':checked').not('.formClearIgnore').trigger("click");
        //Xóa trạng thái đã chọn của select
        $thisContainer.find('select').not('.formClearIgnore')
            .not('.ui-pg-selbox')   //Loại trừ select số trang của phân trang
            .each(function (idx, val) {
                $(val)[0].selectedIndex = 0;
            });
        $thisContainer.find('select').not('.formClearIgnore')
            .not('.ui-pg-selbox')   //Loại trừ select số trang của phân trang
            .trigger("change");
        $thisContainer.find('textarea').not('.formClearIgnore').val(''); //Xóa nội dung vùng văn bản
        $thisContainer.find('[type=text]').not('.formClearIgnore').val(''); //Xóa nội dung hộp văn bản
        $thisContainer.find('.formValue>label[id]').not('.formClearIgnore').html(''); //Xóa nội dung label dưới formValue
    }

    if (options.inner === true && $currentActiveWindow[0].newtouch_event_f4) {
        $currentActiveWindow[0].newtouch_event_f4(event);
    }
    if (event) {
        stopDefault(event);
    }
};

//F5 làm mới trang (trang hiện tại)
var newtouch_globalevent_f5 = function (event) {
    if (top.top.$('.dropdown-menu .tabReload').length > 0) {
        top.top.$('.dropdown-menu .tabReload').trigger('click');
        if (event) {
            stopDefault(event);
        }
    }
    else {
        window.location.href = window.location.href;
    }
};

//F6
var newtouch_globalevent_f6 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f6) {
        $currentActiveWindow[0].newtouch_event_f6(event);
        return true;
    }
    return false;
};

//F7
var newtouch_globalevent_f7 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f7) {
        $currentActiveWindow[0].newtouch_event_f7(event);
        return true;
    }
    return false;
};

//F8
var newtouch_globalevent_f8 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f8) {
        $currentActiveWindow[0].newtouch_event_f8(event);
        return true;
    }
    return false;
};

//F9
var newtouch_globalevent_f9 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f9) {
        $currentActiveWindow[0].newtouch_event_f9(event);
        return true;
    }
    return false;
};

//F10
var newtouch_globalevent_f10 = function (event) {
    var $currentActiveWindow = newtouch_getCurrentActiveWindow();
    if ($currentActiveWindow[0].newtouch_event_f10) {
        $currentActiveWindow[0].newtouch_event_f10(event);
        return true;
    }
    return false;
};

//Sự kiện keydown của document
$(document).keydown(function (event) {
    //global start
    $('.form-an-cur').removeClass('form-an-cur');  //AutoNext V2
    //globle end
    event = event || window.event;
    if ((event.keyCode >= 112 && event.keyCode <= 123)) {
        newtouch_setCurrentWindow();    //Xác định lại window hiện tại     //Sau lớp bật lên, đóng lớp bật lên thì phải định vị lại đến trang chính
    }

    if (1 == 2) {

    }
    else if (((event.keyCode >= 112 && event.keyCode <= 114) || (event.keyCode >= 117 && event.keyCode <= 121)) && top.top.$('#loadingPage:visible').length >= 1) {
        return false;
    }
    else if (event.keyCode == 8) {  //Backspace
        if (!(event.target.nodeName == 'INPUT' || event.target.nodeName == 'SELECT' || event.target.nodeName == 'TEXTAREA')) {
            stopDefault(event);
            stopPropagation(event);
            return false;
        }
    }
    else if (event.keyCode == 112) { //F1
        if (newtouch_globalevent_f1(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 113) { //F2
        if (newtouch_globalevent_f2(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 114) { //F3
        if (newtouch_globalevent_f3(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 115) { //F4 xóa
        newtouch_globalevent_f4(event);
        stopPropagation(event); //Ngăn nổi bọt ra ngoài
    }
    else if (event.keyCode == 116) { //F5 làm mới
        newtouch_globalevent_f5(event);
        stopPropagation(event); //Ngăn nổi bọt ra ngoài
    }
    else if (event.keyCode == 117) { //F6
        if (newtouch_globalevent_f6(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 118) { //F7
        if (newtouch_globalevent_f7(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 119) { //F8
        if (newtouch_globalevent_f8(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 120) { //F9
        if (newtouch_globalevent_f9(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (event.keyCode == 121) { //F10
        if (newtouch_globalevent_f10(event)) {
            stopDefault(event);
            stopPropagation(event); //Ngăn nổi bọt ra ngoài
        }
    }
    else if (false) {

    }
    //stopPropagation(event);
});

/* Dropdown mặc định của newtouch, gắn dữ liệu local, không phải yêu cầu ajax */
$.fn.newtouchBindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        change: null,
        data: null,
        datasource: null,
        attdata: null //Thêm thuộc tính tùy chỉnh
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    if (options.datasource) {   //Nếu có datasource
        options.data = options.datasource();
    }
    $.each(options.data, function (i) {
        if (options.attdata) {
            $element.append($('<option data-' + options.attdata + '="' + options.data[i][options.attdata] + '"></option>').val(options.data[i][options.id]).html(options.data[i][options.text]));
        }
        else {
            $element.append($("<option></option>").val(options.data[i][options.id]).html(options.data[i][options.text]));
        }
    });
    if (options.selectedValue) {
        $element.val(options.selectedValue);
    }
    $element.select2({
        minimumResultsForSearch: options.search == true ? 0 : -1
    });
    $element.on("change", function (e) {
        if (options.change != null) {
            //modified by sunny  Tại đây vì tùy chọn của hộp thả xuống đã thêm ‘Vui lòng chọn’, vị trí chỉ mục không đúng
            //options.change(data[$(this).find("option:selected").index()]);
            options.change(options.data);
        }
        $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
    });
}


//Tạo bảng Grid
$.fn.dataNewGrid = function (options, data) {
    var defaults = {
        datatype: "local",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        multiselect: false,
        caption: ""
    };
    var options = $.extend(defaults, options);
    //Grid không phân trang, thống nhất tắt sắp xếp - bắt đầu
    if ((options.sortable === false || !!!options.pager)
        && options.colModel && options.colModel.length > 0) {
        //
        for (i = 0; i < options.colModel.length; i++) {
            if (options.colModel[i].sortable === undefined) {
                options.colModel[i].sortable = false;
            }
        }
        //
        if (!!!options.pager) {
            options.rowNum = -1;    //Không phân trang, hiển thị tất cả
        }
    }
    //Grid không phân trang, thống nhất tắt sắp xếp - kết thúc
    var $element = $(this);
    $element.css({ "cursor": "pointer" });
    if (!options.onSelectRow) {
        options["onSelectRow"] = function (rowid, status) {
            var $operate = $(".operate");
            if ($operate.length > 0) {
                var length = $(this).jqGrid("getGridParam", "selrow").length;
                if (length > 0) {
                    $operate.animate({ "left": 0 }, 200);
                } else {
                    $operate.animate({ "left": '-100.1%' }, 200);
                }
                $operate.find('.close').click(function () {
                    $operate.animate({ "left": '-100.1%' }, 200);
                });
            }
            if (!options.multiselect && window.btn_selectrow) {
                btn_selectrow();
            }
            //Chọn nhiều
            if (options.multiselect && window.grid_checkedit) {
                grid_checkedit();
            }
            if (options.onSelectRow_page) {
                options.onSelectRow_page(rowid, status);
            }
        };
    }
    if (!options.ondblClickRow) {
        options["ondblClickRow"] = function (rowid) {
            if (options.ondblClickRow_page) {
                options.ondblClickRow_page(rowid);
            }
            else if (window.btn_edit) {
                btn_edit(rowid);
            }
        }
    }
    $element.jqGrid(options);
    for (var i = 0; i <= data.length; i++) {
        $element.jqGrid('addRowData', i + 1, data[i]);
    }
    if (data.length > 0) {
        $('.ui-jqgrid-bdiv').find(".unwritten").remove();
    }
};


//Khi cửa sổ trình duyệt thay đổi kích thước thì tải lại chiều rộng của jqGrid
function initLayout(formId) {
    reSetGridWidth();
    return;
    $("table[rel=" + formId + "]").each(function () {
        var rel = $(this).attr("rel");
        if (rel) {
            var $form = $("#" + rel);
            var gridWidth = $form.width() - 3;
            $(this).setGridWidth(gridWidth, true);
        }
    });
}

/* newtouch local gắn jGrid mặc định */
$.fn.newtouchLocalDataGrid = function (options, data) {
    var defaults = {
        datatype: "local",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        posttofirst: true,
        showloadingLayer: false
    };
    var options = $.extend(defaults, options);
    if (!!!options.pager) {
        options.rowNum = -1;    //Không phân trang, hiển thị tất cả
    }
    var $element = $(this);
    if (!options.actions || options.actions.indexOf("declare") >= 0) {
        //Grid không phân trang, thống nhất tắt sắp xếp - bắt đầu
        if (options.colModel && options.colModel.length > 0) {
            //
            for (i = 0; i < options.colModel.length; i++) {
                if (options.colModel[i].sortable === undefined) {
                    options.colModel[i].sortable = false;
                }
            }
            //
        }
        //Grid không phân trang, thống nhất tắt sắp xếp - kết thúc
        //Khai báo
        $element.jqGrid(options);
    }
    if ((!options.actions || options.actions.indexOf("addRowData") >= 0) && data) {
        //Gắn dữ liệu
        //Lấy id hàng lớn nhất hiện tại
        var baseId = 0;
        var curRowIds = $element.jqGrid('getDataIDs');
        if (curRowIds && curRowIds.length && curRowIds.length > 0) {
            for (var j = 0; j < curRowIds.length; j++) {
                if (curRowIds[j] && parseInt(curRowIds[j]) && parseInt(curRowIds[j]) != NaN && parseInt(curRowIds[j]) > baseId) {
                    baseId = parseInt(curRowIds[j]);
                }
            }
        }
        if (data instanceof Array) {
            //Đã truyền vào một mảng
            if (data.length == 1) {
                if (options.posttofirst) {
                    $element.addRowData(baseId + 1, data[0], "first");
                }
                else {
                    $element.addRowData(baseId + 1, data[0]);
                }
            }
            else {
                //Đã truyền vào một loạt dữ liệu
                if (options.posttofirst) {
                    for (var i = 0; i <= data.length; i++) {
                        $element.addRowData(baseId + i + 1, data[i], "first");
                    }
                }
                else {
                    for (var i = 0; i <= data.length; i++) {
                        $element.jqGrid('addRowData', baseId + i + 1, data[i]);
                    }
                }
            }
        }
        else {
            //Không phải mảng, đã truyền trực tiếp vào một object
            if (options.posttofirst) {
                $element.addRowData(baseId + 1, data, "first");
            }
            else {
                $element.addRowData(baseId + 1, data);
            }
        }
    }
};

/* Bộ chọn lớp nổi của newtouch */
$.fn.newtouchFloatingSelector = function (options) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        leftshift: 0,   //Độ lệch trái   //Chỉ có hiệu lực khi căn chỉnh với input
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: 'GET',
        ajaxreqdata: null,  //function post hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,    //function get
        isjsonp: false,  //Có sử dụng jsonp hay không
        isinputchangetriggered: true,    //Được kích hoạt bởi input change (ví dụ: nhấn nút để lớp nổi hiện ra thì truyền false)
        inputtextcheck: null,   //Được kích hoạt bởi input change, có thể xác thực văn bản đã nhập trước, function
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('id');
        if (options.id) {
            options.id = "divfloat_" + options.id;
        }
    }
    if (!options.id) {
        //Tạo id ngẫu nhiên
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }
    if (!options.colModel || options.colModel.length <= 0) {
        throw new Error("argument colModel is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //Lớp nổi liên quan

    //Tạo lớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id);
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            var thisContainerHeight = options.height + 2;
            //Vì có thanh cuộn ngang, chiều cao của thanh cuộn ngang
            if (options.tableWidth &&
                options.width <= options.tableWidth + 15) {
                thisContainerHeight += 15;
            }
            $divContainer.css({
                height: thisContainerHeight,
                width: options.width
            });
            //Tiêu đề
            //170829 ẩn caption
            //if (options.caption) {
            //    $divContainer.append('<div class="ui-nfs-caption">' + options.caption + '<div style="float:right;margin-right: 10px;cursor:pointer;" ><i class="fa fa-times" aria-hidden="true"></i></div></div>');
            //    $('div#' + options.id + ' div.ui-nfs-caption i.fa-times').click(function () {
            //        $('body').find('div#' + options.id).hide();
            //    });
            //}
            $divContainer.append('<div class="ui-nfs-container"><table></table></div>')
            if (options.tableWidth) {
                $divContainer.find('table').css('width', options.tableWidth);
            }
            //thead
            $divContainer.find('table').append('<thead><tr></tr></thead>');
            if (options.colModel && options.colModel.length > 0) {
                for (i = 0; i < options.colModel.length; i++) {
                    var thHtml = '<th style="';
                    if (options.colModel[i].widthratio) {
                        thHtml += 'width:' + options.colModel[i].widthratio + '%;';
                    }
                    else if (options.colModel[i].width) {
                        thHtml += 'width:' + options.colModel[i].width + 'px;';
                    }

                    if (options.colModel[i].hidden && options.colModel[i].hidden == true) {
                        thHtml += 'display:none;';
                    }
                    thHtml += '"';
                    if (i == options.colModel.length - 1) {
                        //Ngoại lệ về kiểu
                        thHtml += ' class="isthelasttd"';
                    }
                    thHtml += '>' + options.colModel[i].label + '</th>';
                    $divContainer.find('table thead tr').append(thHtml);
                }
            }
        }
        options.container = $divContainer;
    };
    //Định vị lớp nổi (theo vị trí của element)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        }
        else {
            //Định vị theo tọa độ của options.$this
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            }
            else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    if (options.leftshift) {
                        options.container.css('left', (thisDomPoint.l - options.leftshift) + "px");
                    }
                    else {
                        options.container.css('left', (thisDomPoint.l) + "px");
                    }
                }
                else {
                    options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //Phía dưới không đủ chỗ thì đặt lên phía trên
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            }
            else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //Tải dữ liệu
    options.loaddata = function () {
        //options.container.html('loading data ...');
        if (!options.url) {
            if (options.filter) {
                var resultObjArr;
                if (options.isinputchangetriggered) {
                    resultObjArr = (options.filter)(options.$this.val());
                }
                else {
                    resultObjArr = options.filter();
                }
                if (resultObjArr) {
                    (options.funcBindRowData)(resultObjArr);
                }
            }
        }
        else {
            //Lấy dữ liệu qua giao diện
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //Ai kích hoạt thì trả về cho trang
                if (thisParameters) {
                    thisUrl += "?" + $.encodeRequestParameters(thisParameters);
                }
            }
            if (options.ajaxreqdata) {
                var thisReqData = options.ajaxreqdata();
            }

            options.lastRequestMark = thisUrl + (!!thisReqData ? thisReqData.toString() : '');

            if (!options.isjsonp) {
                $.ajax({
                    type: options.ajaxmethod,
                    url: thisUrl,
                    data: thisReqData,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {

                            //Ngăn hiển thị lộn xộn
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                //console.log("skip bind data");
                                return;
                            }

                            if (options.filter) {
                                data = (options.filter)(data);
                            }
                            (options.funcBindRowData)(data);
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
            else {
                //jsonp tải dữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn hiển thị lộn xộn
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }

                            if (!(data.data instanceof Object)) {
                                data.data = JSON.parse(data.data);
                            }
                            if (options.filter) {
                                (options.funcBindRowData)((options.filter)(data.data));
                            }
                            else {
                                (options.funcBindRowData)(data.data);
                            }
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
        }
    };
    //Nhận kết quả trả về, duyệt và hiển thị
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr && options.colModel && options.colModel.length > 0) {
            if (options.container) {
                options.container.find('table tbody').html(''); //Xóa nội dung tbody hiện tại
            }
            if (options.container.find('table tbody').length <= 0) {
                options.container.find('table').append('<tbody></tbody>');
            }
            if (options.height) {
                options.container.find('table tbody').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var trHtml = '<tr';
                //Kiểu tr
                if (i == 0) {
                    trHtml += ' class="active"';    //Hàng đầu tiên
                }
                for (j = 0; j < options.colModel.length; j++) {
                    var thistdVal = resultObjArr[i][options.colModel[j].name];
                    if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) {
                            thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]);
                        }
                        else {
                            if (options.colModel[j].formatter === "date") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getDate({ date: $.strToDate(thistdVal) });
                                }
                            }
                            else if (options.colModel[j].formatter === "datetime") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false });
                                }
                            }
                        }
                    }
                    trHtml += ' data-' + options.colModel[j].name + '="' + (thistdVal === null ? "" : thistdVal) + '"';
                }
                trHtml += ">";
                for (j = 0; j < options.colModel.length; j++) {
                    trHtml += '<td';
                    trHtml += ' style="';
                    if (options.colModel[j].widthratio) {
                        trHtml += ' width:' + options.colModel[j].widthratio + '%;';
                    }
                    else if (options.colModel[j].width) {
                        trHtml += 'width:' + options.colModel[j].width + 'px;';
                    }
                    if (options.colModel[j].align) {
                        trHtml += 'text-align:' + options.colModel[j].align + ';';
                    }
                    if (options.colModel[j].hidden && options.colModel[j].hidden == true) {
                        trHtml += 'display:none;';
                    }
                    trHtml += '"';
                    if (j == options.colModel.length - 1) {
                        //Ngoại lệ về kiểu
                        trHtml += ' class="isthelasttd"';
                    }
                    var thistdVal = resultObjArr[i][options.colModel[j].name];
                    if (options.colModel[j].formatter) {
                        if ($.isFunction(options.colModel[j].formatter)) {
                            thistdVal = options.colModel[j].formatter(thistdVal, null, resultObjArr[i]);
                        }
                        else {
                            if (options.colModel[j].formatter === "date") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getDate({ date: $.strToDate(thistdVal) });
                                }
                            }
                            else if (options.colModel[j].formatter === "datetime") {
                                if (thistdVal && $.strToDate(thistdVal)) {
                                    thistdVal = $.getTime({ date: $.strToDate(thistdVal), second: false });
                                }
                            }
                        }
                    }
                    trHtml += '>' + (thistdVal === null ? "" : thistdVal) + '</td>';
                }
                trHtml += "</tr>";
                options.container.find('table tbody').append(trHtml);
            }
            //Thay đổi thanh cuộn dọc   //Đã hy sinh việc thead nổi ở đầu
            if (options.tableWidth &&
                options.width <= options.tableWidth + 15) {
                var tblHeight = resultObjArr.length * 40;
                if (tblHeight > options.height) {
                    $('body').find('div#' + options.id).find('table tbody').css('height', '');
                    $('body').find('div#' + options.id).find('table').css('height', resultObjArr.length * 26 + 10 + 33);
                }
            }
        }
    }
    //Gắn sự kiện chọn khi nhấn chuột
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'tbody tr', function () {
                var godbclick = true;
                if (options.itemdbclickhandleBefore) {
                    godbclick = options.itemdbclickhandleBefore($(this));    //Xử lý trước khi nhấn đúp một hàng
                }
                if (!(godbclick === false)) {
                    options.itemdbclickhandler($(this), options.$this); //Ai kích hoạt thì trả về cho trang
                }
                if (options.isinputchangetriggered) {
                    //Nếu được kích hoạt bởi change của chính input thì đặt tiêu điểm cho hộp văn bản
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //Xử lý sau khi nhấn đúp một hàng
                }
                options.container.find('table tbody').html(''); //Xóa nội dung tbody hiện tại
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location();   //Định vị
        if (options.container) {
            options.container.find('table tbody').html(''); //Xóa nội dung tbody hiện tại
        }
        options.container.show();   //Định vị xong rồi mới hiển thị
        options.loaddata();   //Tải dữ liệu
        options.binddbclickchooseevent();   //Sự kiện chọn khi nhấn đúp  //Lẽ ra chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();
                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu thì ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }
        else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                if (options.focusfromdbclickitem === true) {
                    options.focusfromdbclickitem = false;   //Không thể kích hoạt focus theo vòng lặp
                    return;
                }

                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu thì ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }

        options.$this.keyup(function (event) {   //Phải là keyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }

            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //Giá trị không thay đổi thì không kích hoạt
            }

            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu thì ẩn đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //Gắn sự kiện phím lên/xuống của bàn phím
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 && options.container) {
                var $trs = options.container.find('table tbody tr');    //Tất cả các tr trong lớp nổi hiện tại
                if ($trs.length == 0) {
                    return; //Không có hàng
                }
                var curActiveIndex = -1;    //Hàng active hiện tại
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0;  //Chỉ mục đích
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                }
                else {
                    if (event.keyCode == 38) {
                        //alert("Đã nhấn phím ↑!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 40) {
                        //alert("Đã nhấn phím ↓!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    }
                    else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnh vị trí cuộn
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('table tbody tr.active');    //Tất cả các tr trong lớp nổi hiện tại
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');    //
                        //stopDefault(event);
                        //stopPropagation(event);
                    }
                }
            }
        });

        //Nhấn chuột vào nơi khác thì ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
    else {
        //Được kích hoạt bởi sự kiện click
        options.$this.click(function () {
            $('body div.ui-nfs:visible').hide();

            options.okload();
        });

        //Nhấn chuột vào nơi khác thì ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
};
/* Gắn cùng một loại lớp nổi cho nhiều phần tử */
$.fn.newtouchBatchFloatingSelector = function (options) {
    this.each(function () {
        var defaults = {
        };
        defaults = $.extend(defaults, options);
        $(this).newtouchFloatingSelector(defaults);
    });
};

//Gợi ý
$.newtouchAlert = function (data) {
    if (!$.xhrSuccessDataExCheckHandle(data)) {
        return false;
    }

    //success？
    var msg = data.message;
    if (!msg) {
        msg = data.msg;
    }
    if (!msg) {
        msg = data.message;
    }
    var type = 'success';

    $.modalAlert(msg, type);
};

(function ($) {
    //Kích hoạt ngay
    newtouch_setCurrentWindow();
    $(function () {
        //Kích hoạt sau khi ready xong
        $('.form-control-focus').focus();   //Đặt tiêu điểm
        $('.newtouch_Readonly').attr("disabled", "disabled");
        CheckStorage();
        //Chuyển đổi icon

        $('.icontoggle').click(function () {
            var $dstnDiv = $(this).parent().prev().children('tbody.dispTbody');
            if ($(".icontoggle").hasClass("fa-angle-double-down")) {
                $dstnDiv.fadeToggle("slow");
                $(".icontoggle").removeClass("fa-angle-double-down");
                $(".icontoggle").addClass("fa-angle-double-up");
            } else {
                $dstnDiv.fadeToggle("hide");
                $(".icontoggle").removeClass("fa-angle-double-up");
                $(".icontoggle").addClass("fa-angle-double-down");
            }
        });
    });
    window.onload = function () {
        //Ở cuối cùng
        AutoNext_V2.init(); //keydown đã gắn nhiều sự kiện, thực hiện theo thứ tự, auto-next ở cuối cùng
    };
})(jQuery);

/* AutoNext V2 */
var AutoNext_V2 = {
    init: function () {
        $('[id].form-an').keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 13) {
                var nodes = $('.form-an:visible');
                for (i = 0; i < nodes.length; i++) {
                    if ($(this)[0].id == nodes[i].id) {
                        if ($(this).is('.form-an-end')) {
                            return; //Không đi tiếp xuống dưới nữa, nhưng có thể đi từ cái trước đến
                        }
                        var ancheck = $(this).attr('form-an-ancheck');
                        var goonnext = true;
                        //ancheck là một hàm xác thực viết trong html, trả về bool. true thì tiếp tục nhảy xuống sau, ngược lại không tự động nhảy xuống cái tiếp theo
                        if (ancheck) {
                            goonnext = eval(ancheck + "('" + $(this).val() + "')");
                        }
                        if (goonnext === true) {
                            var $nextEle = null;
                            if (i == nodes.length - 1) {
                                $nextEle = $(nodes[0]);
                            }
                            else {
                                $nextEle = $(nodes[i + 1]);
                            }
                            if ($nextEle[0].tagName == 'SELECT') {
                                if ($nextEle.next('.select2').length == 1) {
                                    $nextEle.addClass('form-an-cur');
                                    $nextEle.select2("open");
                                    //Hoàn toàn sẽ không kích hoạt sự kiện keydown đã định nghĩa ở trên này
                                }
                                else {

                                }
                            }
                            else {
                                $nextEle.addClass('form-an-cur').trigger('focus');
                            }
                        }
                        return;
                    }
                }
            }
        });
        $('select[id].form-an').on("select2:select", function (e) {
            var nodes = $('.form-an:visible');
            for (i = 0; i < nodes.length; i++) {
                if ($(this)[0].id == nodes[i].id) {
                    if ($(this).is('.form-an-end')) {
                        return; //Không đi tiếp xuống dưới nữa, nhưng có thể đi từ cái trước đến
                    }
                    var ancheck = $(this).attr('form-an-ancheck');
                    var goonnext = true;
                    //ancheck là một hàm xác thực viết trong html, trả về bool. true thì tiếp tục nhảy xuống sau, ngược lại không tự động nhảy xuống cái tiếp theo
                    if (ancheck) {
                        goonnext = eval(ancheck + "('" + $(this).val() + "')");
                    }
                    if (goonnext === true) {
                        var $nextEle = null;
                        if (i == nodes.length - 1) {
                            $nextEle = $(nodes[0]);
                        }
                        else {
                            $nextEle = $(nodes[i + 1]);
                        }
                        if ($nextEle[0].tagName == 'SELECT') {
                            if ($nextEle.next('.select2').length == 1) {
                                $nextEle.addClass('form-an-cur');
                                $nextEle.select2("open");
                                //Hoàn toàn sẽ không kích hoạt sự kiện keydown đã định nghĩa ở trên này
                            }
                            else {

                            }
                        }
                        else {
                            $nextEle.addClass('form-an-cur').trigger('focus');
                        }
                    }
                    return;
                }
            }
        });
    }
}

function CheckStorage() {
    var basePath = '~/'; //Đường dẫn cơ sở
    var cssPath = getlocalStorage("cssPath"); //Lấy đường dẫn giao diện mặc định
    if (cssPath != "") {
        $("#color-skin").attr("href", cssPath);
    }
}

/*Lưu trữ storage*/
function setlocalStorage(c_name, value) {
    var strKey = c_name;
    var storage = window.localStorage;
    storage.setItem(strKey, value);
}

/*Đọc*/
function getlocalStorage(c_name) {
    var strKey = c_name;
    var storage = window.localStorage;
    if (storage.getItem(strKey) != null) {
        //alert(storage.getItem(strKey)+'localStorage');
        return storage.getItem(strKey);
    }
    return "";
}

//Khi cửa sổ trình duyệt thay đổi kích thước
$(window).resize(function () {
    //Điều chỉnh chiều rộng của jqGrid
    reSetGridWidth();

});
//Đặt lại chiều rộng của grid
function reSetGridWidth($jqgridTable) {
    if (!$jqgridTable) {
        //Tất cả các grid trong trang //có thể nhìn thấy
        $jqgridTable = $("table.ui-jqgrid-btable:visible");
    }
    //Điều chỉnh chiều rộng của jqGrid
    $jqgridTable.each(function () {
        var rel = $(this).attr("rel");
        if (rel) {
            //Để tương thích với phiên bản cũ
            var $form = $("#" + rel);
            var gridWidth = $form.width() * 0.999;
            if ($(this).width() >= gridWidth) {
                $(this).setGridWidth(gridWidth, false);
            }
            else {
                $(this).setGridWidth(gridWidth, true);
            }
        }
        else {
            var gridWidth = $("#gbox_" + $(this).attr('id')).parent().width() * 0.999;
            if ($(this).width() >= gridWidth) {
                $(this).setGridWidth(gridWidth, false);
            }
            else {
                $(this).setGridWidth(gridWidth, true);
            }
        }
    });
}
//Chuyển chuỗi thành ngày, định dạng không phải ngày thì trả về null
$.strToDate = function (str) {
    var d;
    var d = new Date(str);
    if (isNaN(d)) {
        return null;
    }
    else {
        return d;
    }
}

$.getDate = function (options) {
    if (!!!options) {
        options = {};   //Ngăn báo lỗi
    }
    //ute undefined then empty
    if ((options.emptywhenundefined || options.ute) && !!!options.date) {
        return "";
    }
    var oDate = new Date(); //Khởi tạo một đối tượng thời gian;
    if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date;
            idateStr = idateStr.replace(/T/g, ' '); //json T
            idateStr = idateStr.replace(new RegExp(/-/gm), "/"); 　　//Chỉ cần chuyển tất cả '-' thành '/' là được
            if (idateStr.length > 19) {
                idateStr = idateStr.substr(0, 19);  //Loại bỏ phần mili giây, nếu không IE sẽ báo lỗi
            }
            oDate = new Date(idateStr);
        }
        else {
            oDate = options.date;  //Dùng ngày được truyền vào đó
        }
    }
    var year = oDate.getFullYear();   //Lấy năm của hệ thống;
    var month = oDate.getMonth() + 1;   //Lấy tháng của hệ thống, vì tháng được tính từ 0 nên phải cộng 1
    if (month < 10) {
        month = "0" + month;
    }
    var date = oDate.getDate(); // Lấy ngày của hệ thống,
    if (date < 10) {
        date = "0" + date;
    }
    return year + "-" + month + "-" + date;
}

$.getTime = function (options) {
    if (!!!options) {
        options = {};   //Ngăn báo lỗi
    }
    if ((options.emptywhenundefined || options.ute) && !!!options.date) {
        return "";
    }
    var oDate = new Date(); //Khởi tạo một đối tượng thời gian;
    if (options.date) {
        if (typeof (options.date) == typeof ('abcc')) {
            var idateStr = options.date;
            idateStr = idateStr.replace(/T/g, ' '); //json T
            idateStr = idateStr.replace(new RegExp(/-/gm), "/"); 　　//Chỉ cần chuyển tất cả '-' thành '/' là được
            if (idateStr.length > 19) {
                idateStr = idateStr.substr(0, 19);  //Loại bỏ phần mili giây, nếu không IE sẽ báo lỗi
            }
            oDate = new Date(idateStr);
        }
        else {
            oDate = options.date;  //Dùng ngày được truyền vào đó
        }
    }
    var year = oDate.getFullYear();   //Lấy năm của hệ thống;
    var month = oDate.getMonth() + 1;   //Lấy tháng của hệ thống, vì tháng được tính từ 0 nên phải cộng 1
    var hour = oDate.getHours();
    var min = oDate.getMinutes();
    if (month < 10) {
        month = "0" + month;
    }
    var date = oDate.getDate(); // Lấy ngày của hệ thống,
    if (date < 10) {
        date = "0" + date;
    }
    if (hour < 10) {
        hour = "0" + hour;
    }
    if (min < 10) {
        min = "0" + min;
    }
    var reslut = year + "-" + month + "-" + date + " " + hour + ":" + min;
    if (!(options.second === false)) {
        var second = oDate.getSeconds();
        if (second < 10) {
            second = "0" + second;
        }
        reslut = reslut + ":" + second;
    }
    if (options.ms === true) {
        var second = oDate.getMilliseconds();
        if (second < 10) {
            second = "00" + second;
        }
        else if (second < 100) {
            second = "0" + second;
        }
        reslut = reslut + "." + second;
    }
    return reslut;
}

/* Đã lỗi thời */
//Lấy tuổi theo năm tháng sinh
function GetAgeByCsny(strBirthday) {
    if (!!!strBirthday) {
        return "";
    }
    var returnAge;
    strBirthday = strBirthday.split(" ")[0];
    var strBirthdayArr = strBirthday.split("-");
    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];

    d = new Date();
    var nowYear = d.getFullYear();
    var nowMonth = d.getMonth() + 1;
    var nowDay = d.getDate();

    if (nowYear == birthYear) {
        returnAge = 0;//Cùng năm thì là 0 tuổi
    }
    else {
        var ageDiff = nowYear - birthYear; //Hiệu số năm
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;//Hiệu số ngày
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
            else {
                var monthDiff = nowMonth - birthMonth;//Hiệu số tháng
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
        }
        else {
            returnAge = -1;//Trả về -1 nghĩa là ngày sinh nhập vào sai, muộn hơn hôm nay
        }
    }

    return returnAge;//Trả về tuổi tròn

}
//Nếu không nhỏ hơn 1 năm thì tính theo năm; ngược lại nếu không nhỏ hơn 1 tháng thì N tháng M ngày; ngược lại nếu không nhỏ hơn 3 ngày thì tính theo ngày; ngược lại tính theo giờ
function getAgeFromBirthTime(options) {
    try {
        if (!options.begin) {
            return { nl: -1, nldw: -1, text: '' };
        }
        //Định dạng lại thành yyyy-MM-dd HH:mm:ss
        options.begin = $.getTime({ date: options.begin });
        if (!options.end) {
            options.end = $.getTime();
        }
        else if (options.end.length == 10) {
            //Chưa cung cấp giờ phút giây thì định dạng lại
            options.end = $.getTime({ date: options.end });
        }

        if (options.begin >= options.end) {
            return { nl: -1, nldw: -1, text: '' };
        }

        var reg = new RegExp(
            /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})(\s)(\d{1,2})(:)(\d{1,2})(:{0,1})(\d{0,2})$/);
        var beginArr = options.begin.match(reg);
        var endArr = options.end.match(reg);

        var days = 0;
        var month = 0;
        var year = 0;

        days = endArr[4] - beginArr[4];
        if (days < 0) {
            month = -1;
            days = 30 + days;
        }

        month = month + (endArr[3] - beginArr[3]);
        if (month < 0) {
            year = -1;
            month = 12 + month;
        }

        year = year + (endArr[1] - beginArr[1]);

        if (year < 0) {
            return { nl: -1, nldw: -1, text: '' };
        }

        var nl = -1; //Tuổi, dạng số
        var nldw = -1;   //Đơn vị tuổi: 1 năm, 2 tháng, 3 ngày, 4 giờ
        var nlStr = '';

        if (year >= 1) {
            //năm
            nldw = 1;
            nl = year;
            if (month >= 6) {
                nl = nl + 1;
            }
            nlStr = nl + ' tuổi';
        }
        else if (month >= 1) {
            //tháng
            nldw = 2;
            nl = month;
            nlStr = nl + ' tháng';
        }
        else if (days >= 3) {
            //ngày
            nldw = 3;
            nl = days;
            nlStr = nl + ' ngày';
        }
        else {
            //giờ
            nldw = 4;
            var begDate = new Date(beginArr[1], beginArr[3] - 1,
                beginArr[4], beginArr[6], beginArr[8], beginArr[10]);
            var endDate = new Date(endArr[1], endArr[3] - 1, endArr[4],
                endArr[6], endArr[8], endArr[10]);

            var between = (endDate.getTime() - begDate.getTime()) / 1000;
            days = Math.floor(between / (24 * 3600));
            var hours = Math.floor(between / 3600 - (days * 24));
            nl = days * 24 + hours;
            if (nl == 0) {
                nl = 1; //1 giờ
            }
            nlStr = nl + ' giờ';
        }

        return { nl: nl, nldw: nldw, text: nlStr };
    }
    catch (errr) {
        return { nl: -1, nldw: -1, text: '' };
    }
}
/* Đã lỗi thời */
function getBrowserType() {
    return "Chrome";
}

//Bốn bỏ, sáu vào, năm thành đôi (làm tròn ngân hàng)
function roundingBy4she6ru5chengshuang(src, pos) {
    if (isNaN(src)) {
        throw Error('src not valid');
    }
    var result = 0;
    var isNagt = false;  //Là số âm  //Như vậy là không chính xác
    if (src < 0) {
        //throw Error('negative not support');
        src = -src;
        isNagt = true;
    }
    //src 11.2350000
    src = src.toString();//.replace("-", "");   //‘11.2350000’
    var pointIndex = src.indexOf('.');  //2
    if (pointIndex <= 0) {
        //Vốn dĩ không có dấu thập phân
        result = parseFloat(src).toFixed(pos);
    }
    else {
        //
        if (pointIndex + pos + 1 >= src.length) {
            //Số chữ số sau dấu chấm vốn dĩ không đủ
            result = parseFloat(src).toFixed(pos);
        }
        else {
            //Tìm trong src chỉ mục của chữ số mà chúng ta muốn giữ lại
            var resultIndex = pointIndex + pos; //4
            if (src.substr(resultIndex + 1, 1) <= '4') {
                //4 thì bỏ
                result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos);
            }
            else if (src.substr(resultIndex + 1, 1) >= '6') {
                //6 thì làm tròn lên
                result = (parseFloat(src.substring(0, resultIndex + 1))
                    + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
            }
            else {
                //src.substring(5, 1) 5
                var tn = true; //Sau 5 không có số nào khác ‘0’
                for (iiiii = resultIndex + 2; iiiii <= src.length - 1; iiiii++) {
                    if (src.substr(iiiii, 1) != '0') {
                        tn = false;
                        break;
                    }
                }
                if (!tn) {
                    //Sau 5 còn có số khác ‘0’ thì nhất định làm tròn lên
                    result = (parseFloat(src.substring(0, resultIndex + 1))
                        + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);
                }
                else {
                    //Xét tính chẵn lẻ
                    if (parseInt(src.substr(resultIndex, 1)) % 2 == 0) {
                        result = parseFloat(src.substring(0, resultIndex + 1)).toFixed(pos);
                    }
                    else {
                        result = (parseFloat(src.substring(0, resultIndex + 1))
                            + parseFloat(1 / Math.pow(10, pos))).toFixed(pos);  //Làm tròn thành số chẵn
                    }
                }
            }
        }
    }
    if (isNagt) {
        return -result;
    }
    return result;
}
//Tải cây điều hướng
$.LoadNavTree = function () {
    var data = top.clients.authorizeMenu;
    var _html = "";

    var language_type = top.$.cookie($.getCurrentAppId() + '_Newtouch_language_type');

    $.each(data, function (i) {
        var row = data[i];
        if (row.ParentId == "0" || !row.ParentId) {
            _html += '<li>';
            _html += '<a data-id="' + row.Id + '" href="#" class="dropdown-toggle"><i class="'
                + row.Icon + '"></i><span data-cnName="' + row.Name + '">'
                + ((language_type == 'en' && row.EnName) ? row.EnName : row.Name)
                + '</span><i class="fa fa-angle-right drop-icon"></i></a>';
            var childNodes = row.ChildNodes;
            if (childNodes.length > 0) {
                _html += '<ul class="submenu">';
                $.each(childNodes, function (i) {
                    var subrow = childNodes[i];
                    _html += '<li>';
                    _html += '<a class="menuItem" data-target="' + subrow.Target
                        + '" data-cnName="' + subrow.Name
                        + '" title="' + (!!subrow.Description ? subrow.Description : ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name))
                        + '" data-AppId="' + (!!subrow.AppId ? subrow.AppId : "") + '" data-id="' + subrow.Id + '" href="' + subrow.UrlAddress + '" data-index="' + subrow.px + '">' + ((language_type == 'en' && subrow.EnName) ? subrow.EnName : subrow.Name) + '</a>';
                    _html += '</li>';
                });
                _html += '</ul>';
            }
            _html += '</li>';
        }
    });
    $("#sidebar-nav ul").prepend(_html);
}
//Mở rộng prototype - bắt đầu
/**  
* Định dạng đối tượng thời gian của js; 
* eg:format="yyyy-MM-dd hh:mm:ss";   
*/
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,  //month   
        "d+": this.getDate(),     //day   
        "h+": this.getHours(),    //hour   
        "m+": this.getMinutes(),  //minute   
        "s+": this.getSeconds(), //second   
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter   
        "S": this.getMilliseconds() //millisecond   
    }
    var week = ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"];
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(w+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, week[this.getDay()]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return fmt;
}

/** 
*Thay đổi ngày trong js  
* y năm, m tháng, d ngày, h giờ, n phút, s giây  
*/
Date.prototype.add = function (part, value) {
    value *= 1;
    if (isNaN(value)) {
        value = 0;
    }
    switch (part) {
        case "y":
            this.setFullYear(this.getFullYear() + value);
            break;
        case "m":
            this.setMonth(this.getMonth() + value);
            break;
        case "d":
            this.setDate(this.getDate() + value);
            break;
        case "h":
            this.setHours(this.getHours() + value);
            break;
        case "n":
            this.setMinutes(this.getMinutes() + value);
            break;
        case "s":
            this.setSeconds(this.getSeconds() + value);
            break;
        default:

    }
}
$.dateAdd = function (date, part, value) {
    date.add(part, value);
    return date;
}
//Chuỗi EndWith
String.prototype.StartWith = function (str) {
    var reg = new RegExp("^" + str);
    return reg.test(this);
};
String.prototype.EndWith = function (str) {
    var reg = new RegExp(str + "$");
    return reg.test(this);
};
//Mở rộng prototype - kết thúc

//Array Remove start
Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};
//Array Remove end

//IndexOf không phân biệt chữ hoa/thường
String.prototype.indexOfIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) {
        return this.toLowerCase().indexOf(str.toLowerCase());
    }
    else {
        return -1;
    }
}
//== không phân biệt chữ hoa/thường
String.prototype.equalsIgnoreCase = function (str) {
    if ((this || this == '0') && (str || str == '0')) {
        return this.toLowerCase() === str.toLowerCase();
    }
    else {
        return -1;
    }
}