// MRQC pilot: Vietnamese chrome for framework-ui dialogs.
//
// The shared static host's framework-ui.js ships $.modalAlert/$.modalConfirm with
// hardcoded Chinese dialog titles ("Nhắc nhở hệ thống") and buttons ("Xác nhận"/"Hủy"), and that
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

// QA-phase2 PDS-05 (cung root cause CIS-03): xem ghi chu tuong tu trong
// cis-vi.js — $.modalMsg cua framework-ui.js goi thang top.layer.msg(...)
// khong guard; override nap SAU framework-ui.js trong _Index/_Form giu
// nguyen hanh vi khi co layer, khong co layer thi toast khong chan.
(function () {
    $.modalMsg = function (content, type, msgtime) {
        if (window.top && window.top.layer && window.top.layer.msg) {
            if (!msgtime) { msgtime = 2000; }
            if (type !== undefined) {
                var icon = "";
                if (type == 'success') { icon = "fa-check-circle"; }
                if (type == 'ok') { icon = "fa-check-circle-o"; }
                if (type == 'error') { icon = "fa-times-circle"; }
                if (type == 'warning') { icon = "fa-exclamation-circle"; }
                window.top.layer.msg(content, { icon: icon, time: msgtime, shift: 5 });
                if (window.top.$) {
                    window.top.$(".layui-layer-msg").find('i.' + icon).parents('.layui-layer-msg').addClass('layui-layer-msg-' + type);
                }
            } else {
                window.top.layer.msg(content);
            }
            return;
        }
        var ms = msgtime || 2000;
        var toast = document.createElement("div");
        toast.textContent = content;
        toast.style.cssText = "position:fixed;left:50%;top:20px;transform:translateX(-50%);z-index:100010;background:#333;color:#fff;padding:8px 16px;border-radius:4px;max-width:90%;font-size:14px;";
        document.body.appendChild(toast);
        setTimeout(function () { if (toast.parentNode) { toast.parentNode.removeChild(toast); } }, ms);
    };
})();
