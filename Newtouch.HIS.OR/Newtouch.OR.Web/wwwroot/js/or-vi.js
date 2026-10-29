// MRQC pilot: Vietnamese chrome for framework-ui dialogs.
//
// The shared static host's framework-ui.js ships $.modalAlert/$.modalConfirm with
// hardcoded Chinese dialog titles ("系统提示") and buttons ("确认"/"取消"), and that
// file lives outside this app (it must not be edited here). This override, loaded
// AFTER framework-ui.js in _Index/_Form, replaces both functions with same-signature
// Vietnamese versions. Standalone MRQC pages do not load layer.js (the SSO portal
// frame normally provides it), so when layer is absent the functions fall back to
// the browser's native alert()/confirm() instead of throwing.
(function () {
    function viAlert(content, type) {
        var icon = "";
        if (type == 'success') { icon = "fa-check-circle"; }
        if (type == 'error') { icon = "fa-times-circle"; }
        if (type == 'warning') { icon = "fa-exclamation-circle"; }
        if (window.top && window.top.layer) {
            window.top.layer.alert(content, { icon: icon, title: "Thông báo hệ thống", btn: ['Xác nhận'], btnclass: ['btn btn-primary'] });
            if (window.top.$ && window.top.$('.layui-layer-dialog').length == 1) {
                window.top.$('.layui-layer-dialog').trigger('focus');
            }
        } else {
            alert(content);
        }
    }
    function viConfirm(content, callBack, close, confirmOptions) {
        var defaults = { icon: "fa-exclamation-circle", title: "Thông báo hệ thống", btn: ['Xác nhận', 'Hủy'], btnclass: ['btn btn-primary', 'btn btn-danger'] };
        var opts = $.extend({}, defaults, confirmOptions);
        if (window.top && window.top.layer) {
            window.top.layer.confirm(content, opts,
                function () { callBack(true); if (!(close === false)) { window.top.layer.closeAll('dialog'); } },
                function () { callBack(false); });
        } else {
            callBack(confirm(content));
        }
    }
    $.modalAlert = viAlert;
    $.modalConfirm = viConfirm;
})();


// T-02 (gstack sau redesign): guard chung cho ho dialog framework-ui.js.
//
// File framework-ui.js tren static host goi thang top.layer.msg/open ma khong
// guard; khi trang chay standalone (khong co frame portal SSO cung cap layer)
// thi moi loi AJAX (qua $.najax/$.submitForm -> $.modalMsg, vd. SD-01, PL-4)
// va moi nut mo dialog (qua $.modalOpen, vd. SD-10) lai nem them
// "top.layer is undefined", che loi goc / chan dialog. Block nay nap SAU
// framework-ui.js trong _Index/_Form nen la MOT cho duy nhat moi caller
// $.modal* di qua: khi co layer thi uy thac nguyen ven ve ban goc (hanh vi
// trong portal frame khong doi); khi khong co layer thi dung fallback khong
// nem loi (toast cho msg, modal DOM + iframe cho dialog, dong duoc bang
// $.modalClose). Kiem tra layer trong try/catch de ca truong hop top
// cross-origin cung khong nem SecurityError.
(function () {
    if (!window.jQuery) { return; }
    function hasLayer() {
        try { return !!(window.top && window.top.layer); } catch (e) { return false; }
    }
    function fallbackRegistry(win) {
        try {
            if (!win.__viFallbackModals) { win.__viFallbackModals = {}; }
            return win.__viFallbackModals;
        } catch (e) { return null; }
    }
    function closeFallbackModal(id) {
        var wins = [];
        try { if (window.top) { wins.push(window.top); } } catch (e) { /* cross-origin top */ }
        wins.push(window);
        for (var i = 0; i < wins.length; i++) {
            var reg = fallbackRegistry(wins[i]);
            if (reg && reg[id]) {
                var el = reg[id];
                delete reg[id];
                if (el.parentNode) { el.parentNode.removeChild(el); }
                return true;
            }
        }
        return false;
    }

    // --- $.modalMsg: goc goi top.layer.msg khong guard ---
    var _modalMsg = $.modalMsg;
    $.modalMsg = function (content, type, msgtime) {
        if (hasLayer()) { return _modalMsg.apply(this, arguments); }
        var ms = msgtime || 2000;
        var toast = document.createElement("div");
        toast.textContent = content;
        toast.style.cssText = "position:fixed;left:50%;top:20px;transform:translateX(-50%);z-index:100010;background:#333;color:#fff;padding:8px 16px;border-radius:4px;max-width:90%;font-size:14px;";
        document.body.appendChild(toast);
        setTimeout(function () { if (toast.parentNode) { toast.parentNode.removeChild(toast); } }, ms);
    };

    // --- $.modalOpen: goc goi top.layer.open khong guard ---
    var _modalOpen = $.modalOpen;
    $.modalOpen = function (options) {
        if (hasLayer()) { return _modalOpen.apply(this, arguments); }
        var opts = $.extend({ id: null, title: '', width: "800px", height: "500px", url: '', btn: ['Xác nhận', 'Đóng'], btnclass: ['btn btn-primary', 'btn btn-danger'], callBack: null, cancelCallBack: null, showleftlalbel: false, leftlalbelchecked: true, leftlalbelcheckedasClose: true }, options);
        if (!opts.id) { opts.id = "viFallbackModal"; }
        var w = parseInt((opts.width || "").toString().replace('px', ''), 10) || 800;
        var h = parseInt((opts.height || "").toString().replace('px', ''), 10) || 500;
        if (w > window.innerWidth - 40) { w = window.innerWidth - 40; }
        if (h > window.innerHeight - 80) { h = window.innerHeight - 80; }
        closeFallbackModal(opts.id);
        var backdrop = document.createElement("div");
        backdrop.id = "viFallbackModal-" + opts.id;
        backdrop.style.cssText = "position:fixed;left:0;top:0;width:100%;height:100%;background:rgba(0,0,0,0.3);z-index:100009;";
        var dialog = document.createElement("div");
        dialog.style.cssText = "position:absolute;left:50%;top:40px;transform:translateX(-50%);background:#fff;border-radius:4px;box-shadow:0 5px 15px rgba(0,0,0,.5);width:" + w + "px;";
        var header = document.createElement("div");
        header.style.cssText = "padding:10px 15px;border-bottom:1px solid #e5e5e5;font-weight:bold;";
        header.textContent = opts.title || '';
        var closeX = document.createElement("button");
        closeX.type = "button";
        closeX.textContent = "×";
        closeX.style.cssText = "float:right;border:none;background:none;font-size:21px;line-height:1;cursor:pointer;";
        closeX.onclick = function () { doCancel("cancel"); };
        header.appendChild(closeX);
        dialog.appendChild(header);
        var body = document.createElement("div");
        var frame = document.createElement("iframe");
        frame.id = opts.id;
        frame.name = opts.id;
        frame.src = opts.url || 'about:blank';
        frame.style.cssText = "width:100%;height:" + h + "px;border:none;";
        body.appendChild(frame);
        dialog.appendChild(body);
        if (opts.showleftlalbel) {
            var leftRow = document.createElement("div");
            leftRow.style.cssText = "padding:8px 15px;border-top:1px solid #e5e5e5;";
            var leftLabel = document.createElement("label");
            var leftCheck = document.createElement("input");
            leftCheck.type = "checkbox";
            leftCheck.id = "leftlabel";
            leftCheck.checked = !!opts.leftlalbelchecked;
            leftLabel.appendChild(leftCheck);
            leftLabel.appendChild(document.createTextNode(" " + (opts.leftlabeltext || "Xác nhận và đóng")));
            leftRow.appendChild(leftLabel);
            dialog.appendChild(leftRow);
        }
        var footer = document.createElement("div");
        footer.style.cssText = "padding:10px 15px;border-top:1px solid #e5e5e5;text-align:right;";
        function doCancel(kind) {
            if (opts.cancelCallBack) {
                if (opts.cancelCallBack(opts.id, kind) === false) { return; }
            }
            closeFallbackModal(opts.id);
        }
        if (opts.btn && opts.btn[0]) {
            var btnOk = document.createElement("button");
            btnOk.type = "button";
            btnOk.className = (opts.btnclass && opts.btnclass[0]) || 'btn btn-primary';
            btnOk.textContent = opts.btn[0];
            btnOk.style.cssText = "margin-left:5px;";
            btnOk.onclick = function () {
                if (typeof opts.callBack === 'function') {
                    if (opts.showleftlalbel) {
                        var chk = document.getElementById("leftlabel");
                        var checked = chk ? chk.checked : !!opts.leftlalbelchecked;
                        opts.callBack(opts.id, opts.leftlalbelcheckedasClose ? checked : !checked);
                    } else {
                        opts.callBack(opts.id);
                    }
                }
            };
            footer.appendChild(btnOk);
        }
        var btnClose = document.createElement("button");
        btnClose.type = "button";
        btnClose.className = (opts.btnclass && opts.btnclass[1]) || 'btn btn-danger';
        btnClose.textContent = (opts.btn && opts.btn[1]) || 'Đóng';
        btnClose.style.cssText = "margin-left:5px;";
        btnClose.onclick = function () { doCancel("cancel"); };
        footer.appendChild(btnClose);
        dialog.appendChild(footer);
        backdrop.appendChild(dialog);
        document.body.appendChild(backdrop);
        var reg = fallbackRegistry(window);
        if (reg) { reg[opts.id] = backdrop; }
    };

    // --- $.modalClose: ban goc da try/catch nen khong nem, nhung khong dong
    // duoc fallback modal; o day dong fallback truoc, roi moi uy thac. ---
    var _modalClose = $.modalClose;
    $.modalClose = function (iframeId) {
        var id = iframeId;
        if (!id) {
            try {
                if (window.frameElement && window.frameElement.id) { id = window.frameElement.id; }
                else { id = window.name; }
            } catch (e) { /* cross-origin frameElement */ }
        }
        if (id && closeFallbackModal(id)) { return; }
        return _modalClose.apply(this, arguments);
    };
})();

// T-41 (gstack sau redesign): framework-ui.js tren static host (khong sua o
// day) co ham bindSelect() khi du lieu rong se chen option tieng Anh
// "No results found" (vd. #bqlist o OpRegister khi Getbqlist tra rong).
// Mau da ap dung o MRMS/Herp: goi goc xong la option da co mat (bindSelect
// goi AJAX dong bo, async:false) nen doi ngay thanh tieng Viet, ke ca text
// hien thi cua select2. Day KHONG phai locale jqGrid — _Index von da nap
// grid.locale-vi.js sau grid.locale-cn.js.
(function () {
    if (!window.jQuery || !$.fn.bindSelect) { return; }
    var _bindSelect = $.fn.bindSelect;
    $.fn.bindSelect = function () {
        var result = _bindSelect.apply(this, arguments);
        this.each(function () {
            var $el = $(this);
            $el.find("option").each(function () {
                var $opt = $(this);
                if ($opt.text() === "No results found") { $opt.text("Không tìm thấy dữ liệu liên quan!"); }
            });
            var $container = $("#select2-" + $el.attr("id") + "-container");
            if ($container.length && $container.text() === "No results found") {
                $container.text("Không tìm thấy dữ liệu liên quan!");
            }
        });
        return result;
    };
})();
