/* =====================================================================
   redesign-grid.js — Helper jqGrid dùng chung, chặng 3 redesign OpenHIS
   Nguồn chuẩn: openhis-core/scripts/redesign/assets/
   Đồng bộ vào wwwroot từng app bằng scripts/redesign/sync-assets.sh.
   Yêu cầu: jQuery (2.1.1, đã nạp trước Newtouch-ui.js trong layout) và
   jqGrid 5.1.0 (file vendor đã xác nhận có setFrozenColumns).
   Không sửa từng lưới: các hàm này dùng lại được cho mọi lưới.
   CHẶNG 3-TIẾP: wrapper cuộn được ÁP TỰ ĐỘNG cho mọi .ui-jqgrid
   (Redesign.autoWrapGrids), không còn chờ từng trang gọi
   makeGridScrollable. Lưới khởi tạo muộn được bắt bằng MutationObserver
   (debounce) + chạy lại sau load; lưới đã bọc, lưới ẩn (width 0) và
   lưới trong .rd-no-scroll / [data-rd-no-scroll] được bỏ qua.
   ===================================================================== */
(function () {
    'use strict';

    /* LƯU Ý THỨ TỰ NẠP (phát hiện chặng 3-tiếp): mọi _Form đều nạp
       redesign-grid.js TRƯỚC jqgrid.min.js (2 dòng), nên guard cũ
       "jqGrid chưa có lúc parse thì return vĩnh viễn" đã làm helper
       chết hoàn toàn trên mọi trang _Form (chỉ còn tag script, không
       có window.Redesign). Vì vậy init được TRÌ HOÃN: nếu jqGrid chưa
       có lúc parse, thử lại ở DOMContentLoaded (lúc đó mọi script head
       đã chạy xong) và ở window load; trang thật sự không có jqGrid
       thì vẫn không làm gì cả, như trước. */
    var initDone = false;

    function init($) {
        if (initDone) { return; }
        initDone = true;

    var DEBOUNCE_MS = 150;
    var OBSERVE_DEBOUNCE_MS = 250;
    var resizeTimer = null;
    var observeTimer = null;

    function debounce(fn) {
        return function () {
            if (resizeTimer) { window.clearTimeout(resizeTimer); }
            resizeTimer = window.setTimeout(fn, DEBOUNCE_MS);
        };
    }

    /* Đo chiều rộng khung chứa thật của lưới: phần tử cha của .ui-jqgrid.
       Trả 0 khi lưới đang ẩn (tab chưa mở) — trường hợp đó bỏ qua để
       khỏi setGridWidth(0) làm lưới sập. Sau khi bọc, cha trực tiếp là
       .rd-grid-scroll, chiều rộng của nó bằng chiều rộng khung chứa
       thật (wrapper là block, max-width:100%) nên cách đo không đổi. */
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

    /* Lưới có thuộc đường loại trừ an toàn không: chính nó hoặc tổ tiên
       mang class rd-no-scroll hoặc thuộc tính data-rd-no-scroll. */
    function isExcluded($box) {
        return $box.closest('.rd-no-scroll, [data-rd-no-scroll]').length > 0;
    }

    /* Lưới có đang hiển thị với chiều rộng thật không. Lưới trong tab
       chưa mở / panel display:none có width 0 → bỏ qua lần này, lần
       chạy sau (khi tab mở, observer bắt class/style đổi) sẽ bọc. */
    function isVisibleGrid($box) {
        try {
            if (!$box.is(':visible')) { return false; }
            return ($box.width() || 0) > 0;
        } catch (e) {
            return false;
        }
    }

    /* Kích hoạt cột neo cho MỘT lưới: chỉ khi colModel của lưới đã khai
       frozen:true (đọc qua getGridParam, không bịa cột neo) và lưới
       chưa có .frozen-bdiv. Trả true nếu sau bước này lưới có cột neo. */
    function activateFrozen($grid, $box) {
        try {
            if ($box.find('.frozen-bdiv').length) { return true; }
            var colModel = $grid.jqGrid('getGridParam', 'colModel');
            var hasFrozen = false;
            if (colModel && colModel.length) {
                for (var i = 0; i < colModel.length; i++) {
                    if (colModel[i] && colModel[i].frozen) { hasFrozen = true; break; }
                }
            }
            if (!hasFrozen) { return false; }
            if ($.isFunction($grid.jqGrid)) {
                $grid.jqGrid('setFrozenColumns');
            }
            return $box.find('.frozen-bdiv').length > 0;
        } catch (e) {
            if (window.console) { window.console.warn('[redesign] setFrozenColumns failed:', e); }
            return false;
        }
    }

    /* Bọc MỘT hộp .ui-jqgrid (nếu đủ điều kiện) + kích hoạt cột neo.
       Trả 'wrapped' | 'already' | 'skipped'. Không ném lỗi ra ngoài. */
    function wrapOne($box) {
        try {
            if ($box.parent().hasClass('rd-grid-scroll')) {
                /* Đã bọc từ trước: vẫn thử kích hoạt cột neo (lưới có
                   thể vừa được tạo xong sau lần bọc trước). */
                var $g0 = $box.find('table.ui-jqgrid-btable').first();
                if ($g0.length) { activateFrozen($g0, $box); }
                return 'already';
            }
            if (isExcluded($box) || !isVisibleGrid($box)) { return 'skipped'; }
            $box.wrap('<div class="rd-grid-scroll"></div>');
            var $grid = $box.find('table.ui-jqgrid-btable').first();
            if ($grid.length) { activateFrozen($grid, $box); }
            return 'wrapped';
        } catch (e) {
            if (window.console) { window.console.warn('[redesign] wrap grid failed:', e); }
            return 'skipped';
        }
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
            if (!$box.length) { return result; }
            var state = wrapOne($box);
            result.wrapped = (state === 'wrapped' || state === 'already');
            if (opts.frozen) {
                result.frozen = activateFrozen($grid, $box);
            } else {
                result.frozen = $box.find('.frozen-bdiv').length > 0;
            }
        } catch (e) {
            if (window.console) { window.console.warn('[redesign] makeGridScrollable failed:', e); }
        }
        return result;
    }

    /* TỰ ĐỘNG áp wrapper cho mọi lưới trên trang (chặng 3-tiếp).
       Quét toàn bộ .ui-jqgrid trong root (mặc định document), bọc lưới
       đủ điều kiện, bỏ qua lưới đã bọc / ẩn / bị loại trừ, mỗi lưới
       lỗi không ảnh hưởng lưới khác. Trả số đếm để probe kiểm chứng. */
    function autoWrapGrids(root) {
        var $scope = root ? $(root) : $(document);
        var counts = { wrapped: 0, already: 0, skipped: 0, frozen: 0 };
        $scope.find('.ui-jqgrid').each(function () {
            var $box = $(this);
            var state = wrapOne($box);
            if (state === 'wrapped') { counts.wrapped++; }
            else if (state === 'already') { counts.already++; }
            else { counts.skipped++; }
            if ($box.find('.frozen-bdiv').length) { counts.frozen++; }
        });
        return counts;
    }

    window.Redesign = window.Redesign || {};
    window.Redesign.refreshGridWidths = refreshGridWidths;
    window.Redesign.makeGridScrollable = makeGridScrollable;
    window.Redesign.autoWrapGrids = autoWrapGrids;

    var refreshDebounced = debounce(function () { refreshGridWidths(); });

    /* Chạy lại sau khi lưới khởi tạo muộn: observer bắt (a) phần tử
       .ui-jqgrid mới chèn vào DOM (jqGrid tạo lưới sau AJAX/khởi tạo
       muộn), (b) class/style đổi trên cây DOM (tab ẩn mở ra, panel
       hiện lên). Debounce để một đợt chèn hàng loạt chỉ chạy một lần;
       lần chạy sau không bọc lại lưới đã bọc nên tự dừng, không lặp. */
    function observeDebounced() {
        if (observeTimer) { window.clearTimeout(observeTimer); }
        observeTimer = window.setTimeout(function () {
            var counts = autoWrapGrids();
            if (counts.wrapped > 0) { refreshGridWidths(); }
        }, OBSERVE_DEBOUNCE_MS);
    }

    $(function () {
        /* Lưới khởi tạo trong script cuối trang: đợi một nhịp rồi đo
           và bọc; observer bắt tiếp các lưới tạo sau đó. */
        window.setTimeout(function () {
            autoWrapGrids();
            refreshGridWidths();
        }, 300);
        if (window.MutationObserver && document.body) {
            new MutationObserver(observeDebounced).observe(document.body, {
                childList: true,
                subtree: true,
                attributes: true,
                attributeFilter: ['class', 'style']
            });
        }
    });
    $(window).on('load', function () {
        autoWrapGrids();
        refreshGridWidths();
    });
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
    }

    function tryInit() {
        var jq = window.jQuery;
        if (!jq || !jq.fn || !jq.fn.jqGrid) { return false; }
        init(jq);
        return true;
    }

    if (!tryInit()) {
        /* jqGrid chưa nạp kịp lúc parse (trường hợp _Form): thử lại khi
           DOM đã sẵn (mọi script head đã chạy) và khi load xong. */
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', tryInit);
        } else {
            window.setTimeout(tryInit, 0);
        }
        window.addEventListener('load', tryInit);
    }
})();
