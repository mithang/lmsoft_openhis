/**
 * bootstrap-table Vietnamese (vi-VN) locale, translated from the bundled
 * bootstrap-table-zh-CN locale (v1.22.1). Loaded by the SSO shared layouts
 * (_Form.cshtml, _Index.cshtml) instead of the Chinese locale so that grid
 * pagination / search / toolbar text renders in Vietnamese.
 */
(function ($) {
  'use strict';
  $.fn.bootstrapTable.locales['vi-VN'] = {
    formatCopyRows: function () { return 'Sao chép các dòng'; },
    formatPrint: function () { return 'In'; },
    formatLoadingMessage: function () { return 'Đang tải dữ liệu, vui lòng đợi'; },
    formatRecordsPerPage: function (pageNumber) {
      return 'Hiển thị ' + pageNumber + ' bản ghi mỗi trang';
    },
    formatShowingRows: function (pageFrom, pageTo, totalRows, totalNotFiltered) {
      if (totalNotFiltered !== undefined && totalNotFiltered > 0 && totalNotFiltered > totalRows) {
        return 'Hiển thị từ ' + pageFrom + ' đến ' + pageTo + ' trong tổng số ' + totalRows + ' bản ghi (đã lọc từ ' + totalNotFiltered + ' bản ghi)';
      }
      return 'Hiển thị từ ' + pageFrom + ' đến ' + pageTo + ' trong tổng số ' + totalRows + ' bản ghi';
    },
    formatSRPaginationPreText: function () { return 'Trang trước'; },
    formatSRPaginationPageText: function (page) { return 'Trang ' + page; },
    formatSRPaginationNextText: function () { return 'Trang sau'; },
    formatDetailPagination: function (totalRows) { return 'Tổng số ' + totalRows + ' bản ghi'; },
    formatClearSearch: function () { return 'Xóa bộ lọc'; },
    formatSearch: function () { return 'Tìm kiếm'; },
    formatNoMatches: function () { return 'Không tìm thấy bản ghi phù hợp'; },
    formatPaginationSwitch: function () { return 'Ẩn/Hiện phân trang'; },
    formatPaginationSwitchDown: function () { return 'Hiện phân trang'; },
    formatPaginationSwitchUp: function () { return 'Ẩn phân trang'; },
    formatRefresh: function () { return 'Làm mới'; },
    formatToggleOn: function () { return 'Hiện dạng thẻ'; },
    formatToggleOff: function () { return 'Ẩn dạng thẻ'; },
    formatColumns: function () { return 'Cột'; },
    formatColumnsToggleAll: function () { return 'Chuyển đổi tất cả'; },
    formatFullscreen: function () { return 'Toàn màn hình'; },
    formatAllRows: function () { return 'Tất cả'; },
    formatAutoRefresh: function () { return 'Tự động làm mới'; },
    formatExport: function () { return 'Xuất dữ liệu'; },
    formatJumpTo: function () { return 'Chuyển đến'; },
    formatAdvancedSearch: function () { return 'Tìm kiếm nâng cao'; },
    formatAdvancedCloseButton: function () { return 'Đóng'; },
    formatFilterControlSwitch: function () { return 'Ẩn/Hiện điều khiển lọc'; },
    formatFilterControlSwitchHide: function () { return 'Ẩn điều khiển lọc'; },
    formatFilterControlSwitchShow: function () { return 'Hiện điều khiển lọc'; }
  };
  $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales['vi-VN']);
})(jQuery);
