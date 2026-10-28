// MRQC pilot: Vietnamese chrome for framework-ui dialogs.
//
// The shared static host's framework-ui.js ships $.modalAlert/$.modalConfirm with
// hardcoded Chinese dialog titles ("Hệ thốngNhắc nhở") and buttons ("xác nhận"/"Hủy"), and that
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

// CIS sweep-2: the shared static host's framework-ui.js bindSelect appends a
// literal "<option value=''>No results found</option>" when an enum-data AJAX
// call returns empty (e.g. QueueInfo #ywbz). The host file must not be edited,
// so this CIS-local pass rewrites that placeholder option's display text only
// (its value stays ""). It also refreshes the select2 container text directly
// (same DOM update the framework's own change handler performs) without firing
// change events, so no page logic is triggered. Runs on ready (after the
// framework's ready binding: this file loads after framework-ui.js), once more
// after a delay for async enum AJAX, and via MutationObserver for late binds.
(function () {
    var BAD = "No results found";
    var GOOD = "Không có dữ liệu";
    function fixScope(root) {
        var opts = (root || document).querySelectorAll
            ? (root || document).querySelectorAll("option") : [];
        for (var i = 0; i < opts.length; i++) {
            if (opts[i].text === BAD) {
                opts[i].text = GOOD;
                var sel = opts[i].parentNode;
                if (sel && sel.id) {
                    var cont = document.getElementById("select2-" + sel.id + "-container");
                    if (cont && cont.textContent === BAD) { cont.textContent = GOOD; }
                }
            }
        }
    }
    function fixAll() { fixScope(document); }
    if (window.jQuery) {
        jQuery(fixAll);
        setTimeout(fixAll, 1500);
        if (window.MutationObserver && document.body) {
            var obs = new MutationObserver(function (mutations) {
                for (var m = 0; m < mutations.length; m++) {
                    var nodes = mutations[m].addedNodes;
                    for (var n = 0; n < nodes.length; n++) {
                        var node = nodes[n];
                        if (node.nodeType !== 1) { continue; }
                        if (node.tagName === "OPTION" && node.text === BAD) { fixAll(); return; }
                        if (node.querySelectorAll && node.querySelectorAll("option").length) {
                            var found = false;
                            var inner = node.querySelectorAll("option");
                            for (var k = 0; k < inner.length; k++) {
                                if (inner[k].text === BAD) { found = true; break; }
                            }
                            if (found) { fixAll(); return; }
                        }
                    }
                }
            });
            obs.observe(document.body, { childList: true, subtree: true });
        }
    }
})();

// QA-phase2 CIS-03/PDS-05 (cung root cause, file nay cua CIS): framework-ui.js
// cua static host dinh nghia $.modalMsg goi thang top.layer.msg(...) khong
// guard; khi trang chay doc lap (khong co frame portal SSO cung cap layer)
// moi loi AJAX cua framework (vd. bindSelect loi trong $.najax error) lai
// nem them "top.layer is undefined" va che loi goc. Override nay, nap SAU
// framework-ui.js trong _Index/_Form, giu nguyen hanh vi khi co layer, va
// khi khong co layer thi hien toast khong chan (khong nem loi) thay vi crash.
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
