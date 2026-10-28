/* =====================================================================
   redesign-grid.js — Helper jqGrid dùng chung, chặng 3 redesign OpenHIS
   Nguồn chuẩn: openhis-core/scripts/redesign/assets/
   Đồng bộ vào wwwroot từng app bằng scripts/redesign/sync-assets.sh.
   Yêu cầu: jQuery (2.1.1, đã nạp trước Newtouch-ui.js trong layout) và
   jqGrid 5.1.0 (file vendor đã xác nhận có setFrozenColumns).
   Không sửa từng lưới: các hàm này dùng lại được cho mọi lưới.
   ===================================================================== */
(function ($) {
    'use strict';

    if (!$ || !$.fn || !$.fn.jqGrid) {
        /* jqGrid chưa nạp (trang không có lưới): không làm gì cả. */
        return;
    }

    var DEBOUNCE_MS = 150;
    var resizeTimer = null;

    function debounce(fn) {
        return function () {
            if (resizeTimer) { window.clearTimeout(resizeTimer); }
            resizeTimer = window.setTimeout(fn, DEBOUNCE_MS);
        };
    }

    /* Đo chiều rộng khung chứa thật của lưới: phần tử cha của .ui-jqgrid.
       Trả 0 khi lưới đang ẩn (tab chưa mở) — trường hợp đó bỏ qua để
       khỏi setGridWidth(0) làm lưới sập. */
    function containerWidth($grid) {
        var $box = $grid.closest('.ui-jqgrid');
        var w = $box.length ? $box.parent().width() : 0;
        if (!w && $box.length) { w = $box.width(); }
        return w || 0;
    }

    /* Gọi lại setGridWidth cho mọi lưới trên trang.
       Nguyên nhân lưới bóp méo (DESIGN §4.2): chiều rộng lưới chỉ tính
       một lần lúc tải; khi sidebar đóng/mở hoặc cửa sổ đổi kích thước,
       lưới không tự co theo container. Hàm này sửa đúng chỗ đó. */
    function refreshGridWidths(root) {
        var $scope = root ? $(root) : $(document);
        $scope.find('table.ui-jqgrid-btable').each(function () {
            var $grid = $(this);
            try {
                var w = containerWidth($grid);
                if (w > 0) { $grid.jqGrid('setGridWidth', w); }
            } catch (e) {
                /* Một lưới lỗi không được làm hỏng các lưới còn lại. */
                if (window.console) { window.console.warn('[redesign] setGridWidth failed:', e); }
            }
        });
    }

    /* Chiến lược A (§4.2): cuộn ngang + cột neo.
       - Bọc .ui-jqgrid trong .rd-grid-scroll (overflow-x:auto) nếu chưa bọc.
       - Cột neo: jqGrid chỉ neo được cột đã khai frozen:true trong
         colModel LÚC TẠO lưới; gọi setFrozenColumns sau tạo chỉ kích
         hoạt khi đã khai. Nếu trang chưa khai frozen, hàm trả
         { frozen: false } và KHÔNG bịa cột neo — việc khai frozen theo
         từng lưới (cột định danh nào) thuộc chặng sau, có hồ sơ A/B/C.
       - Trả { wrapped, frozen } để trang gọi kiểm chứng được. */
    function makeGridScrollable(gridSelector, options) {
        var opts = options || {};
        var $grid = $(gridSelector);
        var result = { wrapped: false, frozen: false };
        if (!$grid.length) { return result; }
        try {
            var $box = $grid.closest('.ui-jqgrid');
            if ($box.length && !$box.parent().hasClass('rd-grid-scroll')) {
                $box.wrap('<div class="rd-grid-scroll"></div>');
                result.wrapped = true;
            } else if ($box.length) {
                result.wrapped = true;
            }
            if (opts.frozen) {
                if ($box.find('.frozen-bdiv').length) {
                    result.frozen = true; /* đã neo từ trước */
                } else if ($.isFunction($grid.jqGrid)) {
                    $grid.jqGrid('setFrozenColumns');
                    result.frozen = $box.find('.frozen-bdiv').length > 0;
                }
            }
        } catch (e) {
            if (window.console) { window.console.warn('[redesign] makeGridScrollable failed:', e); }
        }
        return result;
    }

    window.Redesign = window.Redesign || {};
    window.Redesign.refreshGridWidths = refreshGridWidths;
    window.Redesign.makeGridScrollable = makeGridScrollable;

    var refreshDebounced = debounce(function () { refreshGridWidths(); });

    $(function () {
        /* Lưới khởi tạo trong script cuối trang: đợi một nhịp rồi đo. */
        window.setTimeout(refreshGridWidths, 300);
    });
    $(window).on('load', refreshGridWidths);
    $(window).on('resize', refreshDebounced);

    /* Sidebar đóng/mở đổi class của #page-wrapper (cơ chế nav-small
       của index.common.js) → lưới phải đo lại. Theo dõi attribute
       class, không can thiệp handler sẵn có. */
    $(function () {
        var target = document.getElementById('page-wrapper');
        if (target && window.MutationObserver) {
            new MutationObserver(refreshDebounced).observe(target, {
                attributes: true,
                attributeFilter: ['class']
            });
        }
    });

    /* Trang nào tự quản lý sidebar có thể phát sự kiện này sau khi
       đổi trạng thái sidebar xong. */
    $(document).on('redesign:sidebar-changed', refreshDebounced);
})(window.jQuery);
