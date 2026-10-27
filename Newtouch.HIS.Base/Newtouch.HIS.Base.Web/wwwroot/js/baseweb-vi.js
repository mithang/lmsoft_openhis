// Base web port: Vietnamese chrome for framework-ui dialogs.
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
