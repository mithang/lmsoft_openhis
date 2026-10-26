$(function () {
    document.body.className = localStorage.getItem('config-skin');
    if ($("[data-toggle='tooltip']").length) {
        $("[data-toggle='tooltip']").tooltip();
    }

    //Nhấn Enter để đóng Alert
    $(document).keydown(function (event) {
        event = event || window.event;
        //Đóng dialog
        if (event.keyCode == 13) {
            if (top.top.$('.layui-layer-dialog').length == 1) {
                top.top.$('.layui-layer-dialog').find('.btn-primary').trigger('click');
            }
        }
    });
    //Lựa chọn của dropdown
    $('select[id].form-an').on("select2:close", function (e) {
        $(this).parents('.has-error').find('i.error').remove();
        $(this).parents('.has-error').removeClass('has-error');
        //select bỏ error
        $(this).removeClass('error');
    });

})
//AppId
$.getCurrentAppId = function () {
    var temp = $('body:eq(0)').attr('data-appid');
    if (temp)
        return temp;
    else
        return "";
};
$.getLocationHost = function () {
    var reg = new RegExp("^[^/]*//[^/]*");
    var matched = reg.exec(location.href);
    if (matched.length > 0 && location.href.StartWith(matched[0])) {
        return matched[0];
    }
    return null;
}
$.getUrlWithHost = function (url) {
    if (!!url && url.StartWith('/')) {
        var locationHost = $.getLocationHost();
        if (!!locationHost) {
            return locationHost + url;
        }
    }
    return url;
}
//
$.htmlEncode = function (value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}
$.htmlDecode = function (value) {
    return $('<div/>').html(value).text();
}
$.reload = function () {
    location.reload();
    return false;
}
//Đóng gói lại yêu cầu ajax, tăng cường chặn thống nhất lỗi SYSTEM_ERROR
$.najax = function (options) {
    var opts = $.extend({
        alerterror: true, alertbierror: true, cache: false, dataType: "json"
    }, options);
    //alertbierror: Khi cấu hình ‘return Error()’ có tự động nhắc thông báo lỗi hay không. Mặc định true sẽ nhắc thông báo
    opts.success = function (data, st, xhr) {
        if (opts.alerterror) {
            if (!$.xhrSuccessDataExCheckHandle(data, opts.alertbierror)) {
                if (options.errorCallback) {
                    options.errorCallback(data, st, xhr);   //Thông thường là xử lý ‘return Error()’ của máy chủ
                }
                return false;
            }
        }
        //else không khác gì ajax
        if (options.success) {  //Ngoài ‘return Error()’ của máy chủ ra thì đều sẽ vào đây
            if (!(options.loading === false) || options.loadingtext) {
                $.loading(false);
            }
            options.success(data, st, xhr);
        }
    }
    if (!(options.loading === false) || options.loadingtext) {
        $.loading(true, options.loadingtext);
        opts.error = function (XMLHttpRequest, textStatus, errorThrown) {
            $.loading(false);
            $.modalMsg(errorThrown, "error");
            if (options.error) {
                options.error(XMLHttpRequest, textStatus, errorThrown);
            }
        };
        opts.complete = function () {
            $.loading(false);
            if (options.complete) {
                options.complete();
            }
        };
    }
    $.ajax(opts);
}
$.loading = function (bool, text) {
    var $loadingpage = top.$("#loadingPage");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("Đang tải dữ liệu, vui lòng chờ…");
    }
    var iiiiiw = top.$('body').width();
    var iiiiih = top.$('body').height();
    if (window.top == window) {
        var iiiiiw = $(window).width();
        var iiiiih = $(window).height();
    }
    $loadingtext.css("left", (iiiiiw - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (iiiiih - $loadingtext.height()) / 2);
}
$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (decodeURIComponent(ar[1]) == 'undefined') {
                return "";
            } else {
                return decodeURIComponent(ar[1]);
            }
        }
    }
    return "";
}
$.encodeRequestParameters = function (params) {
    var resultParams = "";
    var arr = params.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (resultParams) {
            resultParams += "&";
        }
        resultParams += ar[0];
        resultParams += "=";
        if (ar[1]) {
            resultParams += encodeURIComponent(ar[1]);
        }
    }
    return resultParams;
}
$.currentWindow = function (iframeId) {
    if (!!!iframeId) {
        iframeId = top.$(".Newtouch_iframe:visible").attr("id");
    }
    if (!!!iframeId) {
        return null;    //not found
    }
    for (var frameIndex = 0; frameIndex < top.frames.length; frameIndex++) {
        try {
            var thisIdxWindow = null;
            thisIdxWindow = top.top.window[frameIndex];
            if (thisIdxWindow && thisIdxWindow.frameElement.id && thisIdxWindow.frameElement.id == iframeId) {
                return thisIdxWindow;
            }
        }
        catch (err) {
            //Vượt miền, v.v.
            //Đảm bảo continue
        }
    }
    //return top.frames[iframeId];
    return null;
}
$.browser = function () {
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1;
    if (isOpera) {
        return "Opera"
    };
    if (userAgent.indexOf("Firefox") > -1) {
        return "FF";
    }
    if (userAgent.indexOf("Chrome") > -1) {
        if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
            return "Chrome";
        } else {
            return "360";
        }
    }
    if (userAgent.indexOf("Safari") > -1) {
        return "Safari";
    }
    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
        return "IE";
    };
}
$.download = function (url, data, method) {
    if (url && data) {
        data = typeof data == 'string' ? data : jQuery.param(data);
        var inputs = '';
        $.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
        });
        $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
    };
};
//Hộp bật lên chỉnh sửa
$.modalOpen = function (options) {
    options.url = $.getUrlWithHost(options.url);
    var defaults = {
        id: null,
        title: 'Cửa sổ hệ thống',
        width: "100px",
        height: "100px",
        url: '',
        shade: 0.3,
        btn: ['Xác nhận', 'Đóng'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
        callBack: null,
        showleftlalbel: false,
        leftlalbelchecked: true,
        leftlalbelcheckedasClose: true,
    };
    var options = $.extend(defaults, options);
    var _width = options.width;
    var _height = options.height;
    try {
        _width = top.$(window).width() > parseInt(options.width.replace('px', ''))
            ? options.width
            : top.$(window).width() + 'px';
        _height = top.$(window).height() > parseInt(options.height.replace('px', ''))
            ? options.height
            : top.$(window).height() + 'px';

    } catch (ex) {}
    var configss = {
        id: options.id,
        closeBtn: options.closeBtn,
        type: 2,
        shade: options.shade,
        title: options.title,
        fix: false,
        area: [_width, _height],
        content: options.url,
        btn: options.btn,
        btnclass: options.btnclass,
        showleftlalbel: options.showleftlalbel,
        leftlabel: {
            text: options.leftlabeltext || "Xác nhận và đóng"
            , checked: options.leftlalbelchecked
        },
        yes: function () {
            if (options.showleftlalbel === true) {
                options.callBack(options.id
                    , options.leftlalbelcheckedasClose ? top.$("#leftlabel").is(":checked") : !top.$("#leftlabel").is(":checked"));
            }
            else {
                options.callBack(options.id);
            }
        }, cancel: function () {
            if (options.cancelCallBack) {
                return options.cancelCallBack(options.id, "cancel");
            }
            else {
                return true;
            }
        }
        , no: function () {
            if (options.cancelCallBack) {
                return options.cancelCallBack(options.id, "no");
            }
            else {
                return true;
            }
        }
    }
    top.layer.open(configss);
}
$.modalConfirm = function (content, callBack, close, confirmOptions) {
    var defaults = {
        icon: "fa-exclamation-circle",
        title: "Thông báo hệ thống",
        btn: ['Xác nhận', 'Hủy'],
        btnclass: ['btn btn-primary', 'btn btn-danger'],
    };
    var confirmOptions = $.extend(defaults, confirmOptions);
    top.layer.confirm(content, confirmOptions, function () {
        callBack(true);
        if (!(close === false)) {
            top.layer.closeAll('dialog');
        }
    }, function () {
        callBack(false);
    });
}
$.modalAlert = function (content, type) {
    var icon = "";
    if (type == 'success') {
        icon = "fa-check-circle";
    }
    if (type == 'error') {
        icon = "fa-times-circle";
    }
    if (type == 'warning') {
        icon = "fa-exclamation-circle";
    }
    top.layer.alert(content, {
        icon: icon,
        title: "Thông báo hệ thống",
        btn: ['Xác nhận'],
        btnclass: ['btn btn-primary'],
    });
    //Đặt tiêu điểm
    if (top.top.$('.layui-layer-dialog').length == 1) {
        top.top.$('.layui-layer-dialog').trigger('focus');
    }
}
$.modalMsg = function (content, type, msgtime) {
    if (!msgtime) {
        msgtime = 2000;
    }
    if (type != undefined) {
        var icon = "";
        if (type == 'success') {
            icon = "fa-check-circle";
        }
        if (type == 'ok') {
            icon = "fa-check-circle-o";
        }
        if (type == 'error') {
            icon = "fa-times-circle";
        }
        if (type == 'warning') {
            icon = "fa-exclamation-circle";
        }
        top.layer.msg(content, {
            icon: icon, time: msgtime, shift: 5
        });
        top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
    } else {
        top.layer.msg(content);
    }
}
$.modalClose = function (iframeId) {
    try {
        if (!iframeId) {
            if (window.frameElement && window.frameElement.id) {
                iframeId = window.frameElement.id;
            }
            else {
                iframeId = window.name;
            }
        }
        var index = top.layer.getFrameIndex(iframeId); //Lấy chỉ mục của lớp iframe hiện tại trước
        var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
        var IsClose = $IsdialogClose.is(":checked");
        if ($IsdialogClose.length == 0) {
            IsClose = true;
        }
        if (IsClose) {
            top.layer.close(index);
        } else {
            location.reload();
        }
    }
    catch (modalCloseExcption) { }
}
//Lấy nội dung nhắc lỗi
$.getFailedAlertMessage = function (data) {
    var msg = data.message;
    if (data.code) {
        var msgMapList;
        try {
            msgMapList = top.top.$.clients.get($.getCurrentAppId(), "SysFailedCodeMessageMapList");
        }
        catch (eeeeeeeee) {
            //Để tương thích với các phiên bản trước
            msgMapList = top.top.clients.SysFailedCodeMessageMapList;
        }
        if (msgMapList) {
            for (i = 0; i < msgMapList.length; i++) {
                if (data.code == msgMapList[i].code) {
                    msg = msgMapList[i].msg;
                    i = msgMapList.length;  //Không tiếp tục nữa
                }
            }
        }
    }
    if (!msg) {
        msg = data.msg;
    }
    if (!msg) {
        msg = data.message;
    }
    if (!msg) {
        msg = "Mã lỗi: " + data.code;
    }
    return msg;
};
//Xử lý thống nhất data mà XHR trả về, nếu bất thường thì xử lý trực tiếp và trả về false. Ngược lại trả về true
$.xhrSuccessDataExCheckHandle = function (data, alertbierror) {
    if (data && data.state == 'error') {
        if (data.code === 'SESSION_TIMEOUT') {
            //Đăng nhập hết thời gian
            top.top.location.href = '/Login/Index';
            return false;
        }
        else if (data.code === 'SESSION_SIDELINED') {
            //Tài khoản này đã đăng nhập ở nơi khác, bạn đã bị buộc đăng xuất; nếu không phải do bạn thao tác, vui lòng đăng nhập lại và kịp thời đổi mật khẩu
            top.top.location.href = '/Login/Index';
            return false;
        }
        else if (data.code === 'SYSTEM_ERROR') {
            //Đã xảy ra ngoại lệ nội bộ
            //Xử lý thống nhất ngoại lệ nội bộ
            //$.modalAlert($.getFailedAlertMessage(data), data.state);    //Tạm thời như vậy
            $.modalConfirm('Chương trình đã xảy ra lỗi nội bộ<br/>Nhấn nút “Chi tiết ngoại lệ” để xem chi tiết', function (flag) {
                if (flag) {
                    ;
                }
                else {
                    var exStackTrace = $.htmlEncode(data.exStackTrace);
                    //exStackTrace = exStackTrace.replace('\\r\\n', '<br/>');
                    top.layer.open({
                        type: 1,
                        title: 'Chi tiết ngoại lệ',
                        area: ['700px', '500px'],
                        closeBtn: 1,
                        shadeClose: true,
                        skin: 'divSysExceptionDetail',
                        content: '<h3>' + data.message + '</h3><div>' + exStackTrace + '</div>'
                    });
                }
            }, null, {
                    btn: ['Xác nhận', 'Chi tiết ngoại lệ']
                });
        }
        else {
            if (alertbierror === undefined || alertbierror === true) {
                $.modalAlert($.getFailedAlertMessage(data), data.state);
            }
        }
        return false;
    }
    return true;
}

$.submitForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        loading: "Đang gửi dữ liệu...",
        success: null,
        close: true,
        successwithtipmsg: true
    };
    var options = $.extend(defaults, options);
    $.loading(true, options.loading);
    window.setTimeout(function () {
        if ($('[name=__RequestVerificationToken]').length > 0) {
            options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
        }
        $.najax({
            url: options.url,
            data: options.param,
            type: "post",
            loadingtext: options.loading,
            success: function (data) {
                //Nhắc thông tin thành công
                if (options.successwithtipmsg && data.message) {
                    $.modalMsg(data.message, data.state);
                }

                options.success(data);

                //Nếu đã close trong success thì cũng không sao
                if (options.close == true) {
                    $.modalClose();
                }
            }
        });
    }, 10);
}
$.deleteForm = function (options) {
    var defaults = {
        prompt: "Lưu ý: Bạn có chắc chắn muốn xóa dữ liệu này không?",
        url: "",
        param: [],
        loading: "Đang xóa dữ liệu...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    $.modalConfirm(options.prompt, function (r) {
        if (r) {
            $.loading(true, options.loading);
            window.setTimeout(function () {
                $.ajax({
                    url: options.url,
                    data: options.param,
                    type: "post",
                    dataType: "json",
                    success: function (data) {
                        $(".operate").animate({ "left": '-100.1%' }, 200);

                        if (!$.xhrSuccessDataExCheckHandle(data)) {
                            return false;
                        }

                        options.success(data);
                        $.modalMsg(data.message, data.state);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.loading(false);
                        $.modalMsg(errorThrown, "error");
                    },
                    beforeSend: function () {
                        $.loading(true, options.loading);
                    },
                    complete: function () {
                        $.loading(false);
                    }
                });
            }, 10);
        }
    });

}
$.jsonWhere = function (data, action) {
    if (action == null) return;
    var reval = new Array();
    $(data).each(function (i, v) {
        if (action(v)) {
            reval.push(v);
        }
    })
    return reval;
}
$.fn.jqGridRowValue = function () {
    var $grid = $(this);
    var selectedRowIds = $grid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var json = [];
        var len = selectedRowIds.length;
        for (var i = 0; i < len; i++) {
            var rowData = $grid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
        return json;
    } else {
        return $grid.jqGrid('getRowData', $grid.jqGrid('getGridParam', 'selrow'));
    }
}
$.fn.formValid = function (options) {
    var defaults = {
        errorPlacement: function (error, element) {
            element.parents('.formValue').addClass('has-error');
            element.parents('.has-error').find('i.error').remove();
            element.parents('.has-error').append('<i class="form-control-feedback fa fa-exclamation-circle error" data-placement="left" data-toggle="tooltip" title="' + error + '"></i>');
            $("[data-toggle='tooltip']").tooltip();
            if (element.parents('.input-group').hasClass('input-group')) {
                element.parents('.has-error').find('i.error').css('right', '33px')
            }
        },
        success: function (element) {
            element.parents('.has-error').find('i.error').remove();
            element.parents('.has-error').removeClass('has-error');
        }
    };
    var options = $.extend(defaults, options);
    return $(this).valid(options);
}
$.fn.formSerialize = function (formdate, forlabel) {
    var element = $(this);
    if (!!formdate) {
        for (var key in formdate) {
            var $id = element.find('#' + key);
            var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value == "true" || value == "1") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "select":
                    if ($id.find('option[value="' + value + '"]').length > 0) {
                        $id.val(value).trigger("change");
                    }
                    break;
                case 'radio':
                    $('input[name="' + key + '"]').each(function () {
                        var $this = $(this);
                        if ($this.val() == value) {
                            $this[0].checked = true;
                            $this.parent().addClass('active');
                        }
                    });

                    break;
                default:
                    $id.val(value);
                    if (forlabel === true && $id.length > 0 && $id[0].tagName == 'LABEL') {
                        $id.html(value);
                    }
                    break;
            }
        };
        return false;
    }
    var postdata = {
    };
    element.find('input,select,textarea,hidden').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        if (!!!id) {
            return; //Không có thuộc tính id thì continue
        }
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            case 'radio':
                postdata[id] = element.find('input[name="' + id + '"]:checked').val();
                break;
            default:
                //var value = ($this.val() == null || $this.val() == "") ? "&nbsp;" : $this.val();
                var value = ($this.val() == null || $this.val() == "") ? "" : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/&nbsp;/g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};
$.fn.formReset = function () {
    $(this).removeAttr('novalidate');
    $(this).find(".error").parents('.has-error').find('i.error').remove();
    $(this).find(".error").parents('.has-error').removeClass('has-error');
    $(this).find(".error").removeClass('error');
}
$.fn.bindSelect = function (options) {
    var defaults = {
        id: "id",
        text: "text",
        search: false,
        url: "",
        param: [],
        change: null,
        pleaseselect: false,
        pleaseselectText: null,
        handlenoresult: true,
        dropdownAutoWidth: !1,
        selectedValue: null,    //Mục đã chọn
        data: null,
        datasource: null,
        minimumResultsForSearch: Infinity   //Không hiển thị search
    };
    var options = $.extend(defaults, options);
    var $element = $(this);

    options.init = function (data) {
        if ($element.find('option:first').length == 1
            && (!!$element.find('option:first').attr('default') || $element.find('option:first').attr('value') === undefined || $element.find('option:first').attr('value') === '')
        ) {
            options.pleaseselect = true;
            options.pleaseselectText = $element.find('option:first').html();
        }
        else {
            options.pleaseselect = false;
        }
        $element.html('');  //Xóa sạch trước
        if (options.pleaseselect === true) {
            $element.append($("<option value=''>" + options.pleaseselectText + "</option>"));
        }
        if (data && data.length > 0) {
            $.each(data, function (i) {
                $element.append($("<option></option>").val(data[i][options.id]).html(data[i][options.text]));
            });
            if ($element && options.selectedValue) {
                //$element.val(options.selectedValue);
                $element.find("option[value='" + options.selectedValue + "']").attr('selected', 'selected');
            }
        }
        else {
            if (options.pleaseselect !== true && options.handlenoresult === true) {
                //Xử lý noresult, và un pleaseselect
                $element.append($("<option value=''>No results found</option>"));
            }
        }
        $element.select2({
            minimumResultsForSearch: options.minimumResultsForSearch,
            dropdownAutoWidth: options.dropdownAutoWidth
        });
        $element.on("change", function (e) {
            if (options.change != null) {
                //modified by sunny  Tại đây vì tùy chọn của hộp thả xuống đã thêm ‘Vui lòng chọn’, vị trí chỉ mục không đúng
                //options.change(data[$(this).find("option:selected").index()]);
                options.change(data);
            }
            $("#select2-" + $element.attr('id') + "-container").html($(this).find("option:selected").text().replace(/　　/g, ''));
        });
    }

    if (options.url != "") {
        $.najax({
            url: options.url,
            data: options.param,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                if (options.callBack) {
                    options.callBack(data);
                }
                options.init(data);
            }
        });
    }
    else if (options.data || options.datasource) {
        if (options.datasource) {
            options.data = options.datasource();
        }
        if (options.data) {
            options.init(options.data);
        }
    }
    else {
        $element.select2({
            minimumResultsForSearch: options.minimumResultsForSearch,
            dropdownAutoWidth: options.dropdownAutoWidth
        });
    }
}
$.fn.authorizeButton = function () {
    var moduleId = top.$(".Newtouch_iframe:visible").attr("id").substr(6);
    var dataJson = top.clients.authorizeButton[moduleId];
    var $element = $(this);
    $element.find('a[authorize=yes]').attr('authorize', 'no');
    if (dataJson != undefined) {
        $.each(dataJson, function (i) {
            $element.find("#" + dataJson[i].Code).attr('authorize', 'yes');
        });
    }
    $element.find("[authorize=no]").parents('li').prev('.split').remove();
    $element.find("[authorize=no]").parents('li').remove();
    $element.find('[authorize=no]').remove();
}
//Tạo bảng Grid
$.fn.dataGrid = function (options) {
    var defaults = {
        datatype: "json",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true
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
    }
    if (!!!options.pager) {
        options.rowNum = -1;    //Không phân trang, hiển thị tất cả
    }
    //Grid không phân trang, thống nhất tắt sắp xếp - kết thúc
    var $element = $(this);
    $element.css({
        "cursor": "pointer"
    });
    if (!options.onSelectRow) {
        options["onSelectRow"] = function (rowid, status) {
            var $operate = $(".operate");
            if ($operate.length > 0) {
                var selRow = $(this).jqGrid("getGridParam", "selrow");
                if (selRow) {
                    var length = selRow.length;
                    if (length > 0) {
                        $operate.animate({ "left": 0 }, 200);
                    } else {
                        $operate.animate({ "left": '-100.1%' }, 200);
                    }
                    $operate.find('.close').click(function () {
                        $operate.animate({ "left": '-100.1%' }, 200);
                    });
                }
                else {
                    $operate.animate({ "left": '-100.1%' }, 200);
                }
            }
            if (window.btn_loaddata && !options.multiselect) {
                btn_loaddata();
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
    options.beforeProcessing = function (data, st, xhr) {
        if (!$.xhrSuccessDataExCheckHandle(data)) {
            return false;
        }
        return true;
    };
    $element.jqGrid(options);
};

$.fn.nfnAddReadonly = function () {
    $(this).addClass('newtouch_Readonly').attr('disabled', 'disabled');
}

$.fn.nfnRemoveReadonly = function () {
    $(this).removeClass('newtouch_Readonly').removeAttr('disabled');
}

$.fn.keydownEnterEvent = function (func) {
    $(this).keydown(function (event, arg1, arg2) {
        event = event || window.event;
        if (event.keyCode == 13) {
            if (func) {
                var returnwhat = func(event, arg1, arg2);
                if (returnwhat === true || returnwhat === false) {
                    return returnwhat;
                }
            }
        }
    });
}
$.fn.keyupEnterEvent = function (func) {
    $(this).keyup(function (event, arg1, arg2) {
        event = event || window.event;
        if (event.keyCode == 13) {
            if (func) {
                func(event, arg1, arg2);
            }
        }
    });
}
//Số 0 ở đầu trong js
$.zeroPrefixInteger = function (num, length) {
    return (Array(length).join('0') + num).slice(-length);
}
//Phép tính số thập phân trong js xuất hiện nhiều chữ số thập phân
$.addNum = function (num1, num2) {
    var sq1, sq2, m;
    try {
        sq1 = num1.toString().split(".")[1].length;
    }
    catch (e) {
        sq1 = 0;
    }
    try {
        sq2 = num2.toString().split(".")[1].length;
    }
    catch (e) {
        sq2 = 0;
    }
    m = Math.pow(10, Math.max(sq1, sq2));
    return (num1 * m + num2 * m) / m;
}
/**
 ** Hàm trừ, dùng để có kết quả trừ chính xác
 ** Giải thích: Kết quả trừ của javascript sẽ có sai số, khi trừ hai số thực thì khá rõ. Hàm này trả về kết quả trừ chính xác hơn.
 ** Cách gọi: accSub(arg1,arg2)
 ** Giá trị trả về: Kết quả chính xác của arg1 trừ arg2
 **/
$.accSub = function (arg1, arg2) {
    if (!!!arg2) {
        return arg1;
    }
    var r1, r2, m, n;
    try {
        r1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
        r2 = 0;
    }
    m = Math.pow(10, Math.max(r1, r2)); //last modify by deeka //Điều khiển động độ dài chính xác
    n = (r1 >= r2) ? r1 : r2;
    return ((parseFloat(arg1) * m - parseFloat(arg2) * m) / m).toFixed(n);
}
/* Lấy giới tính */
$.getGender = function (value) {
    if (value === undefined || value === null) {
        return '';
    }
    var valStr = $.trim(value.toString());
    if (valStr === "男" || valStr === "女" || valStr.length != 1) {
        return valStr;
    }
    return valStr == '1' ? '男' : (valStr == '2' ? '女' : (valStr == '3' ? '不详' : ''));
}
/* Lấy giới tính */
$.getGenderCode = function (value) {
    if (value === undefined || value === null) {
        return '3'; //3 không rõ
    }
    var valStr = $.trim(value.toString());
    return valStr == "男" ? "1" : (valStr == "女" ? "2" : "3"); //1 nam, 2 nữ, 3 không rõ
}
/* Khi không có giá trị thì trả về 0 */
$.undefinedwith0 = function (value) {
    if (value === undefined || value === null || value === '') {
        return 0;
    }
    return value;
}
/* Khi không có giá trị thì trả về 1 */
$.undefinedwith1 = function (value) {
    if (value === undefined || value === null || value === '') {
        return 1;
    }
    return value;
}
/* Khi không có giá trị thì trả về String.Empty */
$.undefinedwithEmptyString = function (value) {
    if (value === undefined || value === null || value === '') {
        return '';
    }
    return value;
}
/* Nhân bản đối tượng */
$.cloneObject = function (obj) {
    var o;
    if (typeof obj == "object") {
        if (obj === null) {
            o = null;
        }
        else {
            if (obj instanceof Array) {
                o = [];
                for (var i = 0, len = obj.length; i < len; i++) {
                    o.push(clone(obj[i]));
                }
            }
            else {
                o = {
                };
                for (var j in obj) {
                    o[j] = clone(obj[j]);
                }
            }
        }
    }
    else {
        o = obj;
    }
    return o;
}
/* Mở rộng enum - bắt đầu */
$.enum = {
    getItems: function (type) {
        var enumsTemp;
        try {
            enumsTemp = top.top.$.clients.get($.getCurrentAppId(), "enums");
        }
        catch (eeeeeeeee) {
            //Để tương thích với các phiên bản trước
            enumsTemp = top.top.clients.enums;
        }
        if (enumsTemp) {
            var matched = $.jsonWhere(enumsTemp, function (v) {
                return v.Type == type;
            });
            if (matched && matched.length == 1) {
                return matched[0].Items;
            }
        }
        return [];
    },
    getValueByDesc: function (type, desc) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Desc == desc;
            });
            if (matched && matched.length == 1) {
                return matched[0].Value;
            }
        }
        return '';
    },
    getNameByDesc: function (type, desc) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Desc == desc;
            });
            if (matched && matched.length == 1) {
                return matched[0].Name;
            }
        }
        return '';
    },
    getNameByValue: function (type, value) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Value == value;
            });
            if (matched && matched.length == 1) {
                return matched[0].Name;
            }
        }
        return '';
    },
    getDescByValue: function (type, value) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Value == value;
            });
            if (matched && matched.length == 1) {
                return matched[0].Desc;
            }
        }
        return '';
    },
    getValueByName: function (type, name) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Name == name;
            });
            if (matched && matched.length == 1) {
                return matched[0].Value;
            }
        }
        return '';
    },
    getDescByName: function (type, name) {
        var arr = this.getItems(type);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Name == name;
            });
            if (matched && matched.length == 1) {
                return matched[0].Desc;
            }
        }
        return '';
    },

};

/* bindselect start*/
$.fn.enumBindSelect = function (options) {
    var defaults = {
        enumtype: null,
        id: "Value",
        text: "Desc",
        someValues: null,
        someNames: null,

    };
    var options = $.extend(defaults, options);
    //Lấy data theo optims.enumtype
    if (!!!options.enumtype) {
        return;
    }
    var data = $.enum.getItems(options.enumtype);
    if (!!!data) {
        return;
    }
    if (options.someValues && options.someValues.length && options.someValues.length > 0) {
        //Chỉ hiển thị một vài mục trong đó
        data = $.jsonWhere(data, function (v) {
            return (',' + options.someValues + ',').indexOf(',' + v.Value.toString() + ',') >= 0;
        });
    }
    else if (options.someNames && options.someNames.length && options.someNames.length > 0) {
        //Chỉ hiển thị một vài mục trong đó
        data = $.jsonWhere(data, function (v) {
            return (',' + options.someNames + ',').indexOf(',' + v.Name.toString() + ',') >= 0;
        });
    }
    options.data = data;
    var $element = $(this);
    $element.bindSelect(options);
}
//load tự động tải items cho select
$(function () {
    //Khởi tạo dropdown enum
    $('select[data-EnumType]').each(function () {
        $(this).enumBindSelect({
            enumtype: $(this).attr('data-EnumType')
            , someValues: $(this).attr('data-SomeValues')
            , someNames: $(this).attr('data-SomeNames')
            , selectedValue: $(this).attr('data-SelectedValue')
        });
    });
});
/* bindselect end*/

/* Mở rộng enum - kết thúc */

/* Mở rộng từ điển - bắt đầu */
$.itemDetails = {
    //Khi cần trả về bản ghi không hợp lệ thì tham số thứ hai là true
    getItems: function (type, isContantsInvalid) {
        var itemDetailsTemp;
        try {
            itemDetailsTemp = top.top.$.clients.get($.getCurrentAppId(), "itemDetails");
        }
        catch (eeeeeeeee) {
            //Để tương thích với các phiên bản trước
            itemDetailsTemp = top.top.clients.itemDetails;
        }
        if (itemDetailsTemp) {
            var matched = $.jsonWhere(itemDetailsTemp, function (v) {
                return v.Type == type;
            });
            if (matched && matched.length == 1) {
                var details = matched[0].Items;
                if (details && details.length) {
                    if (!!!isContantsInvalid) {
                        //Loại trừ các bản ghi không hợp lệ
                        details = $.jsonWhere(details, function (v) {
                            return v.zt === "1"
                        });
                    }
                }
                return details;
            }
        }
        return [];
    },
    getCodeByName: function (type, name) {
        var arr = this.getItems(type, true);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Name == name;
            });
            if (matched && matched.length >= 1) {
                return matched[0].Code;
            }
        }
        return '';
    },
    getNameByCode: function (type, code) {
        var arr = this.getItems(type, true);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Code == code;
            });
            if (matched && matched.length >= 1) {
                return matched[0].Name;
            }
        }
        return '';
    },
}

/* bindselect end*/
$.fn.itemDetailsBindSelect = function (options) {
    var defaults = {
        itemtype: null,
        id: "Code",
        text: "Name",
    };
    var options = $.extend(defaults, options);
    //Lấy data theo optims.itemtype
    if (!!!options.itemtype) {
        return;
    }
    var data = $.itemDetails.getItems(options.itemtype);
    if (!!!data) {
        return;
    }
    options.data = data;
    var $element = $(this);
    $element.bindSelect(options);
}
/* bindselect end*/
$(function () {
    //Khởi tạo dropdown từ điển
    $('select[data-ItemType]').each(function () {
        $(this).itemDetailsBindSelect({
            itemtype: $(this).attr('data-ItemType')
            , selectedValue: $(this).attr('data-SelectedValue')
        });
    });
});

/* Mở rộng từ điển - kết thúc */

/* deepClone thay cho slice */
$.deepClone = {
    getType: function (obj) {
        //tostring sẽ trả về hàm tạo tương ứng với các thẻ khác nhau
        var toString = Object.prototype.toString;
        var map = {
            '[object Boolean]': 'boolean',
            '[object Number]': 'number',
            '[object String]': 'string',
            '[object Function]': 'function',
            '[object Array]': 'array',
            '[object Date]': 'date',
            '[object RegExp]': 'regExp',
            '[object Undefined]': 'undefined',
            '[object Null]': 'null',
            '[object Object]': 'object'
        };
        if (obj instanceof Element) {
            return 'element';
        }
        return map[toString.call(obj)];
    },
    clone: function (data) {
        var type = this.getType(data);
        var obj;
        if (type === 'array') {
            obj = [];
        } else if (type === 'object') {
            obj = {
            };
        } else {
            //Không còn tầng tiếp theo nữa
            return data;
        }
        if (type === 'array') {
            for (var i = 0, len = data.length; i < len; i++) {
                obj.push(this.clone(data[i]));
            }
        } else if (type === 'object') {
            for (var key in data) {
                obj[key] = this.clone(data[key]);
            }
        }
        return obj;
    }
};
/* deepClone thay cho slice */

//$('any').val() trả về undefined, nval trả về chuỗi rỗng
$.fn.nval = function () {
    var val = $(this).val();
    if (!!val || !isNaN(val)) {
        return val;
    }
    else {
        return '';
    }
}

//Mở rộng đối chiếu danh mục ba bên
$.CataComparison = {
    getItems: function (ttMark, code) {
        var cataComparisonList = top.top.$.clients.get($.getCurrentAppId(), "CataloguesComparisonList");
        if (cataComparisonList) {
            var matched = $.jsonWhere(cataComparisonList, function (v) {
                return v.Code == code && v.TTMark == ttMark;
            });
            if (matched && matched.length == 1) {
                var details = matched[0].Items;
                return details;
            }
        }
        return [];
    },
    getItemsByTTCode: function (ttMark, ttCode) {
        var cataComparisonList = top.top.$.clients.get($.getCurrentAppId(), "CataloguesComparisonList");
        if (cataComparisonList) {
            var matched = $.jsonWhere(cataComparisonList, function (v) {
                return v.TTCode == ttCode && v.TTMark == ttMark;
            });
            if (matched && matched.length == 1) {
                var details = matched[0].Items;
                return details;
            }
        }
        return [];
    },
    getItem: function (ttMark, code, itemCode) {
        var arr = this.getItems(ttMark, code);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.Code == itemCode;
            });
            if (matched && matched.length >= 1) {
                return matched[0];
            }
        }
        return {};
    },
    getItemByTT: function (ttMark, ttCode, ttItemCode) {
        var arr = this.getItemsByTTCode(ttMark, ttCode);
        if (arr) {
            var matched = $.jsonWhere(arr, function (v) {
                return v.TTCode == ttItemCode;
            });
            if (matched && matched.length >= 1) {
                return matched[0];
            }
        }
        return {};
    },
}