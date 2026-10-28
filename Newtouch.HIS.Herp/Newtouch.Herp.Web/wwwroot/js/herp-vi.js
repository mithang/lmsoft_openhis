// MRQC pilot: Vietnamese chrome for framework-ui dialogs.
//
// The shared static host's framework-ui.js ships $.modalAlert/$.modalConfirm with
// hardcoded Chinese dialog titles and buttons, and that
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

// QA phase 2 (H-01): framework-ui.js tren static host (khong sua o day) co ham
// $.modalMsg goi thang top.layer.msg. Khi trang mo truc tiep, khong co khung
// portal cap layer, loi "top.layer is undefined" nem ra (InStorage/OutStorage/
// DirectDelivery). Override giong cach modalAlert/modalConfirm da lam o tren:
// co layer thi dung layer, khong thi hien toast don gian trong trang.
(function () {
    if (!window.jQuery) { return; }
    $.modalMsg = function (content, type, msgtime) {
        if (!msgtime) { msgtime = 2000; }
        if (window.top && window.top.layer) {
            if (type !== undefined) {
                var icon = "";
                if (type == 'success') { icon = "fa-check-circle"; }
                if (type == 'ok') { icon = "fa-check-circle-o"; }
                if (type == 'error') { icon = "fa-times-circle"; }
                if (type == 'warning') { icon = "fa-exclamation-circle"; }
                window.top.layer.msg(content, { icon: icon, time: msgtime, shift: 5 });
            } else {
                window.top.layer.msg(content);
            }
            return;
        }
        var $toast = $("#herp-toast");
        if ($toast.length === 0) {
            $toast = $("<div id=\"herp-toast\" style=\"position:fixed;left:50%;bottom:30px;transform:translateX(-50%);background:#333;color:#fff;padding:10px 18px;border-radius:4px;z-index:99999;\"></div>");
            $("body").append($toast);
        }
        $toast.text(content).show();
        setTimeout(function () { $toast.hide(); }, msgtime);
    };
})();

// QA phase 2 (H-07): giong M-10 — bindSelect() cua framework-ui khi du lieu rong
// chen option tieng Anh "No results found" (vd #ddllb o InventoryAction/
// InventoryQuery, #unitId o ProductUnit). Doi thanh tieng Viet ngay sau goi goc
// (bindSelect dong bo nen option da co mat khi ham tra ve).
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
