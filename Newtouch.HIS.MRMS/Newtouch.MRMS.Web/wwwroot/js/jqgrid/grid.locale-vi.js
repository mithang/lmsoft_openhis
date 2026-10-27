;(function($){
/**
 * jqGrid Vietnamese Translation for the MRQC .NET Core pilot.
 * Ported from the Chinese locale shipped with the app (grid.locale-cn.js).
 * Loaded AFTER grid.locale-cn.js in _Index/_Form so every default string
 * (pager footer, loading text, search/edit/delete dialogs) renders Vietnamese.
 * Day/month names and date masks are unchanged (already English).
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
	defaults : {
	    recordtext: "Hiển thị {0} - {1} trong tổng số {2} bản ghi",
		emptyrecords: "Không có dữ liệu hiển thị",
		loadtext: "Đang tải...",
		pgtext : "Trang {0} trên tổng số {1} trang",
		// jqGrid core default for empty tree grids (not covered by the cn locale):
		// without this, SysItem's tree shows the Chinese "没有找到您要的相关数据!".
		unwrittentext: "Không tìm thấy dữ liệu liên quan!"
	},
	search : {
		caption: "Tìm kiếm...",
		Find: "Tìm",
		Reset: "Đặt lại",
		odata: ['bằng', 'không bằng', 'nhỏ hơn', 'nhỏ hơn hoặc bằng', 'lớn hơn', 'lớn hơn hoặc bằng',
			'bắt đầu với','không bắt đầu với','thuộc về','không thuộc về','kết thúc với','không kết thúc với','chứa','không chứa','rỗng','không rỗng'],
		groupOps: [	{ op: "AND", text: "Tất cả" },	{ op: "OR",  text: "Bất kỳ" }	],
		matchText: " khớp",
		rulesText: " quy tắc"
	},
	edit : {
		addCaption: "Thêm bản ghi",
		editCaption: "Sửa bản ghi",
		bSubmit: "Gửi",
		bCancel: "Hủy",
		bClose: "Đóng",
		saveData: "Dữ liệu đã thay đổi, có lưu không?",
		bYes : "Có",
		bNo : "Không",
		bExit : "Hủy",
		msg: {
			required:"Trường này là bắt buộc",
			number:"Vui lòng nhập số hợp lệ",
			minValue: "Giá trị nhập phải lớn hơn hoặc bằng ",
			maxValue: "Giá trị nhập phải nhỏ hơn hoặc bằng ",
			email: "Đây không phải địa chỉ e-mail hợp lệ",
			integer: "Vui lòng nhập số nguyên hợp lệ",
			date: "Vui lòng nhập thời gian hợp lệ",
			url: "URL không hợp lệ. Tiền tố phải là ('http://' hoặc 'https://')",
			nodefined : " chưa được định nghĩa!",
			novalue : " cần giá trị trả về!",
			customarray : "Hàm tùy chỉnh cần trả về một mảng!",
			customfcheck : "Custom function should be present in case of custom checking!"
		}
	},
	view : {
		caption: "Xem bản ghi",
		bClose: "Đóng"
	},
	del : {
		caption: "Xóa",
		msg: "Xóa các bản ghi đã chọn?",
		bSubmit: "Xóa",
		bCancel: "Hủy"
	},
	nav : {
		edittext: "",
		edittitle: "Sửa bản ghi đã chọn",
		addtext:"",
		addtitle: "Thêm bản ghi mới",
		deltext: "",
		deltitle: "Xóa bản ghi đã chọn",
		searchtext: "",
		searchtitle: "Tìm kiếm",
		refreshtext: "",
		refreshtitle: "Làm mới bảng",
		alertcap: "Chú ý",
		alerttext: "Vui lòng chọn bản ghi",
		viewtext: "",
		viewtitle: "Xem bản ghi đã chọn"
	},
	col : {
		caption: "Chọn cột",
		bSubmit: "Xác nhận",
		bCancel: "Hủy"
	},
	errors : {
		errcap : "Lỗi",
		nourl : "Chưa đặt url",
		norecords: "Không có bản ghi cần xử lý",
		model : "Độ dài colNames và colModel không bằng nhau!"
	},
	formatter : {
		integer : {thousandsSeparator: " ", defaultValue: '0'},
		number : {decimalSeparator:".", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0.00'},
		currency : {decimalSeparator:".", thousandsSeparator: " ", decimalPlaces: 2, prefix: "", suffix:"", defaultValue: '0.00'},
		date : {
			dayNames:   [
				"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat",
		         "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
			],
			monthNames: [
				"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
				"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
			],
			AmPm : ["am","pm","AM","PM"],
			S: function (j) {return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th'},
			srcformat: 'Y-m-d',
			newformat: 'm-d-Y',
			masks : {
				ISO8601Long:"Y-m-d H:i:s",
				ISO8601Short:"Y-m-d",
				ShortDate: "Y/j/n",
				LongDate: "l, F d, Y",
				FullDateTime: "l, F d, Y g:i:s A",
				MonthDay: "F d",
				ShortTime: "g:i A",
				LongTime: "g:i:s A",
				SortableDateTime: "Y-m-d\\TH:i:s",
				UniversalSortableDateTime: "Y-m-d H:i:sO",
				YearMonth: "F, Y"
			},
			reformatAfterEdit : false
		},
		baseLinkUrl: '',
		showAction: '',
		target: '',
		checkbox : {disabled:true},
		idName : 'id'
	}
});
})(jQuery);
