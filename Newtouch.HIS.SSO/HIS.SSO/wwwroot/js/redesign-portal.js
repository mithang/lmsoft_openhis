/* =====================================================================
   redesign-portal.js — Drawer mobile cho portal SSO, chặng 3 redesign
   Nguồn chuẩn: openhis-core/scripts/redesign/assets/
   Đồng bộ vào SSO wwwroot bằng scripts/redesign/sync-assets.sh.
   Viết bằng JavaScript thuần (không phụ thuộc jQuery nạp ở đâu trong
   SSOIndex) và KHÔNG chạm handler #make-small-nav / nav-small sẵn có
   (cơ chế desktop giữ nguyên). Hành vi theo DESIGN §3.5:
   mở bằng nút hamburger (chỉ thấy dưới 992px), đóng bằng nút đóng,
   bấm ra ngoài (overlay), phím Esc, bấm menu, hoặc resize lên desktop.
   Đóng xong focus trả về nút hamburger.
   ===================================================================== */
(function () {
    'use strict';

    function init() {
        var toggle = document.getElementById('rd-nav-toggle');
        var overlay = document.getElementById('rd-drawer-overlay');
        var navCol = document.querySelector('#page-wrapper > .row > #nav-col:not(.login-foot)');
        if (!toggle || !overlay) { return; }
        var closeBtn = navCol ? navCol.querySelector('.rd-drawer-close') : null;

        function isOpen() {
            return document.body.classList.contains('rd-drawer-open');
        }

        function open() {
            document.body.classList.add('rd-drawer-open');
            toggle.setAttribute('aria-expanded', 'true');
            if (closeBtn) { closeBtn.focus(); }
        }

        function close(returnFocus) {
            if (!isOpen()) { return; }
            document.body.classList.remove('rd-drawer-open');
            toggle.setAttribute('aria-expanded', 'false');
            if (returnFocus) { toggle.focus(); }
        }

        toggle.addEventListener('click', function (e) {
            e.preventDefault();
            if (isOpen()) { close(true); } else { open(); }
        });
        overlay.addEventListener('click', function () { close(true); });
        if (closeBtn) {
            closeBtn.addEventListener('click', function () { close(true); });
        }
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' || e.key === 'Esc') { close(true); }
        });
        /* Bấm một mục menu trong drawer thì đóng drawer (điều hướng
           vẫn do indextab.js / menuItem handler sẵn có đảm nhiệm). */
        if (navCol) {
            navCol.addEventListener('click', function (e) {
                var a = e.target && e.target.closest ? e.target.closest('a') : null;
                if (a) { close(false); }
            });
        }
        /* Lên desktop thì drawer không còn ý nghĩa: gỡ class để khỏi
           kẹt trạng thái (CSS drawer vốn chỉ có hiệu lực < 992px). */
        window.addEventListener('resize', function () {
            if (window.innerWidth >= 992) { close(false); }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
