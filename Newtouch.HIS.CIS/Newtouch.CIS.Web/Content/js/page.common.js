//Tần suất Chung Bộ chọn lớp nổi
$.fn.pcFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 280,
		height: 280,
		//minlength: 2,
		focusautotrigger: true,
		className: "ui-pc-container",
		caption: "Chọn tần suất",
		url: '/SystemManage/BaseData/GetOrderFrequencyList',
		ajaxparameters: function ($thisinput) {
			return "";
		}
	};
	var options = $.extend(defaults, options);

	$(this).frequencyNewtouchFloatingSelector(options);
}

//Cách dùng thuốc Chung Bộ chọn lớp nổi
$.fn.yfFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 250,
		height: 200,
		//minlength: 2,
		focusautotrigger: true,
		caption: "Chọn cách dùng",
		url: '/SystemManage/SysBaseData/GetMedicineUsageList',
		ajaxparameters: function ($thisinput) {
			return "keyword=" + $.trim($thisinput.val());
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'Mã', name: 'yfCode', widthratio: 60 },
			{ label: 'Tên', name: 'yfmc', widthratio: 35 }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Hạng mục thu phí Lớp nổi (bao gồm thuốc, hạng mục, cũng có thể không phải hạng mục điều trị)
$.fn.sfxmFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
	    width: 1550,
		height: 380,
		clickautotrigger: true,
		url: '/SystemManage/BaseData/SelectSfxmYp',
		ajaxparameters: null,   //Vui lòng chỉ định
		itemdbclickhandler: null,   //Vui lòng chỉ định
		djDecimalPlaces: 2,  //Độ chính xác đơn giá Mặc định2
		searchType: null,   //Chỉ kiểm soát ẩn hiện cột, không tham gia lọc//Mục tiêu tìm kiếm "yp,sfxm"、"yp"、"sfxm"、"yp.kc"、"sfxm.dwjls"、"yp,sfxm.dwjls"、"yp.kc,sfxm"、"yp.kc,sfxm.dwjls"
		showColModel: null,   //Có thể chỉ định [{name:;widthratio:;},{name:;widthratio:;}] widthratioCó thể chọn
		ypkccbjc: true,    //Kiểm tra sơ bộ tồn kho thuốc   Có bị kiểm soát không Có tồn kho không0

		colModel: [
			{ label: 'Mã', name: 'sfxmCode', widthratio: 6 },
			{ label: 'Tên', name: 'sfxmmc', widthratio: 6 },
			{
				label: 'Quy cách', name: 'gg', widthratio: 6
				, hidden: true
			},
			{ label: 'Loại y lệnh', name: 'yzlx', hidden: true },
			{ label: 'Phiên âm đầu', name: 'py', widthratio: 5, hidden: true },
			{ label: 'Nhóm lớn thu phí', name: 'sfdlCode', hidden: true },
			{
				label: 'Nhóm lớn thu phí', name: 'sfdlmc'
				, widthratio: 6
				, hidden: true
			},
			{ label: 'Đơn vị', name: 'dw', widthratio: 3 },
			{
				label: 'Đơn giá', name: 'dj', widthratio: 6, formatter: function (cellvalue) {
					return (!!cellvalue || cellvalue == 0) ? (parseFloat(cellvalue).toFixed(options.djDecimalPlaces ? options.djDecimalPlaces : 2)) : '';
				}
			},
			{ label: 'Chiến lược tính giá', name: 'jjcl', hidden: true },
			{
				label: 'Số lượng đo', name: 'dwjls', widthratio: 8, formatter: function (cellvalue) {
					return !!cellvalue ? cellvalue : '';
				}
				, hidden: true
			},
			//{ label: 'Chính sách hoàn trả', name: 'zfxz', hidden: true },
			//{
			//    label: 'Chính sách hoàn trả', name: 'zfxzmc', widthratio: 8, formatter: function (cellvalue, a, b) {
			//        return $.enum.getDescByValue("EnumZiFuXingZhi", b.zfxz);
			//    }
			//},
			{ label: 'Chính sách hoàn trả', name: 'zfxz', hidden: true },
			{
				label: 'Chính sách hoàn trả', name: 'zfxzmc', widthratio: 5, formatter: function (cellvalue, a, b) {
					return $.enum.getDescByValue("EnumZiFuXingZhi", b.zfxz);
				}
			},
			{ label: 'Tỷ lệ tự trả', name: 'zfbl', widthratio: 3,hidden: true },
			{ label: 'Dấu hiệu thuốc đặc biệt', name: 'tsypbz', hidden: true },
			{
				label: 'Dấu hiệu thuốc đặc biệt', name: 'tsypbzmc', hidden: true, formatter: function (cellvalue, a, b) {
					return $.enum.getDescByValue("EnumYpsx", b.tsypbz);
				}
			},
			{ label: 'ypjxCode', name: 'ypjxCode', hidden: true },
			{ label: 'duration', name: 'duration', hidden: true },
			{ label: 'jldw', name: 'jldw', hidden: true },
			{ label: 'jldwzhxs', name: 'jldwzhxs', hidden: true },
			{ label: 'cls', name: 'cls', hidden: true },
			{ label: 'zyqzlx', name: 'zyqzlx', hidden: true },
			{ label: 'zxks', name: 'zxks', hidden: true },
			{ label: 'zxksmc', name: 'zxksmc', hidden: true },
			{ label: 'ybdm', name: 'ybdm', hidden: true },
			{ label: 'xzyy', name: 'xzyy', hidden: true },
			{ label: 'xzyysm', name: 'xzyysm', hidden: true },
			{ label: 'mrjl', name: 'mrjl', hidden: true },
			{ label: 'mrpc', name: 'mrpc', hidden: true },
			{ label: 'mrpcmc', name: 'mrpcmc', hidden: true },
			{ label: 'mryf', name: 'mryf', hidden: true },
			{ label: 'mryfmc', name: 'mryfmc', hidden: true },
			{ label: 'yfbmCode', name: 'yfbmCode', hidden: true },
			{ label: 'jlfwBegin', name: 'jlfwBegin', hidden: true },
			{ label: 'jlfwEnd', name: 'jlfwEnd', hidden: true },
			{ label: 'pcfwBegin', name: 'pcfwBegin', hidden: true },
			{ label: 'pcfwEnd', name: 'pcfwEnd', hidden: true },
			{ label: 'kssKy', name: 'kssKy', hidden: true },
			{ label: 'kssqxjb', name: 'kssqxjb', hidden: true },
			{
				label: 'kssmc', name: 'kssmc', hidden: true, formatter: function (cellvalue, a, b) {
					return $.enum.getDescByValue("EnumKss", b.kssqxjb);
				} },
			{
			    label: 'Giá giới hạn bảo hiểm y tế', name: 'cxjje', formatter: function (cellvalue, a, b) {
					if (b.cxjje == null) {
						return "0.00"
					}
					else return b.cxjje
				}, widthratio: 4, hidden: true
			},
			{
				label: 'Nhà thuốc', name: 'yfbmmc', formatter: function (cellvalue, a, b) {
					return b.yzlx === '1' && !!cellvalue ? cellvalue : '';
				}, widthratio: 4, hidden: true
			},
			{
				label: 'Tồn kho', name: 'kcsl', formatter: function (cellvalue, a, b) {
					return b.yzlx === '1'&&!!cellvalue ? cellvalue : '';
				}, widthratio: 4, hidden: true
			},
			{
				label: 'Tồn kho', name: 'clkcsl', formatter: function (cellvalue, a, b) {
					//给的kcsllà đơn vị tối thiểu, ở đây cần hiển thị ngoại trú/Số lượng đơn vị nhập viện
					return b.yzlx === '1' ? (!!b.kcsl && !!b.cls ? (parseInt(b.kcsl / b.cls)) : '无') : '';
				}, widthratio: 4, hidden: true
			},
			{
				label: 'Kháng sinh', name: 'isKss', formatter: function (cellvalue, a, b) {
					return cellvalue == '1' ? '是' : '否';
				}, widthratio: 3, hidden: true
			},
			{
				label: 'Liều đơn lần', name: 'jlfw', formatter: function (cellvalue, a, b) {
					if (b.jlfwBegin || b.jlfwEnd) {
						return b.jlfwBegin + "-" + b.jlfwEnd;
					}
					else return ''
				}, widthratio: 7, hidden: true
			},
			{
				label: 'Phạm vi tần suất', name: 'pcfw', formatter: function (cellvalue, a, b) {
					if (b.pcfwBegin || b.pcfwEnd) {
						return b.pcfwBegin + "-" + b.pcfwEnd;
					}
					else return ''
				}, widthratio: 7, hidden: true
			},
			{
				label: 'Kiểm soát', name: 'kzbz', formatter: function (cellvalue, a, b) {
					return cellvalue == '1' ? '控制' : '';
				}, widthratio: 3, hidden: true
			},
            {
                label: 'Nhà sản xuất', name: 'sccj'/*, hidden: true*/, formatter: function (cellvalue, a, b) {
                    return cellvalue == undefined ? '' : cellvalue;
                }, widthratio: 8
            },
            { label: 'Mã bảo hiểm y tế quốc gia', name: 'gjybdm', widthratio: 8 },
			{ label: 'Ghi chú', name: 'bz' }
		]
	};
	var options = $.extend(defaults, options);

	if (!!!options.searchType) {

		options.searchType = "yp,sfxm"; //Mặc định
	}
	if (options.searchType && !!!options.showColModel) {
	    var colsfxmmc = null;
	    var colsfxmCode = null;
		var colgg = null;
		var colsfdlmc = null;
		var coldwjls = null;
		var colyfbmmc = null;
		var colclkcsl = null;
		var colkzbz = null;
		var colisKss = null;
		var coljlfw = null;
		var colpcfw = null;
		var colzfbl = null;
		var colbz = null;
		var colcxjje = null;
		var coltsypbz = null;
		var colsccj = null;
		var colgjybdm = null;
		$.each(options.colModel, function () {
			if (this.name === 'bz') {
				colbz = this;
			}
			if (this.name === 'zfbl') {
				colzfbl = this;
			}
			if (this.name === 'sfxmmc') {
				colsfxmmc = this;
			}
			if (this.name === 'zfxzmc') {
			    colzfxzmc = this;
			}
			if (this.name === 'dw') {
			    coldw = this;
			}
			if (this.name === 'sfxmCode') {
			    colsfxmCode=this;
			}
			else if (this.name === 'gg') {
				colgg = this;
			}
			else if (this.name === 'sfdlmc') {
				colsfdlmc = this;
			}
			else if (this.name === 'dwjls') {
				coldwjls = this;
			}
			else if (this.name === 'yfbmmc') {
				colyfbmmc = this;
			}
			else if (this.name === 'clkcsl') {
				colclkcsl = this;
			}
			else if (this.name === 'kzbz') {
				colkzbz = this;
			}
			else if (this.name === 'isKss') {
				colisKss = this;
			}
			else if (this.name === 'jlfw') {
				coljlfw = this;
			}
			else if (this.name === 'pcfw') {
				colpcfw = this;
			}
			else if (this.name == 'cxjje') {
				colcxjje = this;
			}
			else if (this.name === 'sccj') {
			    colsccj = this;
			}
			else if (this.name === 'gjybdm') {
			    colgjybdm = this;
			}
			if (this.name === 'tsypbzmc') {
				coltsypbz = this;
			}
		});
		if (!!colsfxmmc && !!colgg && !!colsfdlmc && !!coldwjls && !!colyfbmmc && !!colclkcsl && !!colkzbz) {
			//Đã bị chiếm50宽
			switch (options.searchType) {
				case "yp": 
					colsfxmmc.widthratio = 10;
					colgg.hidden = undefined;
					colgg.widthratio = 8;
					colsfdlmc.hidden = undefined;
					colsfdlmc.widthratio = 10;
					colisKss.hidden = undefined;
					colisKss.widthratio = 6;
					colzfbl.hidden = undefined;
					colzfbl.widthratio = 10;
					colbz.hidden = undefined;
					colbz.widthratio = 5;
					colsccj.hidden = undefined;
					colsccj.widthratio = 10;
					colgjybdm.hidden = undefined;
					colgjybdm.widthratio = 5;
					//coljlfw.hidden = undefined;
					//coljlfw.widthratio = 10;
					//colpcfw.hidden = undefined;
					//colpcfw.widthratio = 10;
					break;
				case "sfxm": 
					colgg.hidden = undefined;
					colgg.widthratio = 10;
					colsfxmmc.widthratio = 20;
					colsfdlmc.hidden = undefined;
					colsfdlmc.widthratio = 12;
					colcxjje.hidden = undefined;
					colcxjje.widthratio = 10;
					//colclkcsl.hidden = undefined;
					//colclkcsl.widthratio = 10;
					break;
				case "yp,sfxm":
			    case "sfxm,yp":
			        colsfxmCode.widthratio = 9;
			        colsfxmmc.widthratio = 20;
			        coldw.widthratio = 8;
			        colgjybdm.hidden = true;
                    colzfxzmc.widthratio = 8;
					colgg.hidden = undefined;
					colgg.widthratio = 12;
					//colisKss.hidden = undefined;
					//colisKss.widthratio = 6;
					colzfbl.hidden = undefined;
					colzfbl.widthratio = 10;
					colbz.hidden = undefined;
					colbz.widthratio = 19;
					//coljlfw.hidden = undefined;
					//coljlfw.widthratio = 10;
					//colpcfw.hidden = undefined;
					//colpcfw.widthratio = 10;
					break;
			    case "yp.kc":
			    case "yyhc":
				case "yp.kc,sfxm":
				case "sfxm,yp.kc": //cxjje 
					coltsypbz.hidden = undefined;
					coltsypbz.widthratio = 8;
					colsfxmmc.widthratio = 15;
					colgg.hidden = undefined;
					colgg.widthratio = 8;
					colyfbmmc.hidden = undefined;
					colyfbmmc.widthratio = 5;
					colclkcsl.hidden = undefined;
					colclkcsl.widthratio = 4;
					colkzbz.hidden = undefined;
					colkzbz.widthratio = 4;
					colisKss.hidden = undefined;
					colisKss.widthratio = 4;
					colzfbl.hidden = undefined;
					colzfbl.widthratio = 5;
					colbz.hidden = undefined;
					colbz.widthratio = 5;
					colbz.cx = undefined;
					colbz.widthratio = 5;
					colcxjje.hidden = undefined;
					colcxjje.widthratio = 6;
					colsccj.widthratio = 8;
					colgjybdm.hidden = undefined;
					colgjybdm.widthratio = 8;
					//coljlfw.hidden = undefined;
					//coljlfw.widthratio = 10;
					//colpcfw.hidden = undefined;
					//colpcfw.widthratio = 10;
					break;
				case "sfxm.dwjls": 
					colgg.hidden = undefined;
					colgg.widthratio = 10;
					colsfxmmc.widthratio = 25;
					coldwjls.hidden = undefined;
					coldwjls.widthratio = 10;
					colcxjje.hidden = undefined;
					colcxjje.widthratio = 10;
					break;
				case "yp,sfxm.dwjls":
				case "sfxm.dwjls,yp": 
					colsfxmmc.widthratio = 12;
					colgg.hidden = undefined;
					colgg.widthratio = 10;
					coldwjls.hidden = undefined;
					coldwjls.widthratio = 8;
					colisKss.hidden = undefined;
					colisKss.widthratio = 7;
					colzfbl.hidden = undefined;
					colzfbl.widthratio = 10;
					colbz.hidden = undefined;
					colbz.widthratio = 5;
					//coljlfw.hidden = undefined;
					//coljlfw.widthratio = 10;
					//colpcfw.hidden = undefined;
					//colpcfw.widthratio = 10;
					break;
				case "yp.kc,sfxm.dwjls":
				case "sfxm.dwjls,yp.kc": 
					colyfbmmc.hidden = undefined;
					coltsypbz.widthratio = 9;
					colsfxmmc.widthratio = 12;
					colgg.hidden = undefined;
					colgg.widthratio = 8;
					colyfbmmc.hidden = undefined;
					colyfbmmc.widthratio = 7;
					colclkcsl.hidden = undefined;
					colclkcsl.widthratio = 5;
					colkzbz.hidden = undefined;
					colkzbz.widthratio = 5;
					coldwjls.hidden = undefined;
					coldwjls.widthratio = 6;
					colisKss.hidden = undefined;
					colisKss.widthratio = 6;
					colzfbl.hidden = undefined;
					colzfbl.widthratio = 10;
					colbz.hidden = undefined;
					colbz.widthratio = 5;
					//coljlfw.hidden = undefined;
					//coljlfw.widthratio = 10;
					//colpcfw.hidden = undefined;
					//colpcfw.widthratio = 10;
					break;
				default:
					break;
			}
		}
	}

	if (options.showColModel) {
		//[{name:;widthratio:;},{name:;widthratio:;}] widthratioCó thể chọn
		$.each(options.colModel, function () {
			var thisCol = this;
			var matchedShow = $.jsonWhere(options.showColModel, function (v) {
				return v.name == thisCol.name;
			});
			if (matchedShow && matchedShow.length) {
				thisCol.hidden = undefined;
				if (matchedShow[0].widthratio) {
					thisCol.widthratio = matchedShow[0].widthratio;
				}
			}
			else {
				thisCol.hidden = true;
			}
		});
	}

	if (options.searchType.indexOf('yp.kc') >= 0 && options.ypkccbjc === true) {

		options.itemdbclickhandleBefore = function ($iiithis) {
			
			if ($iiithis.attr('data-yzlx') == '1') {
				if ((!isNaN($iiithis.attr('data-clkcsl')) && parseInt($iiithis.attr('data-clkcsl')) <= 0) || $iiithis.attr('data-clkcsl') == '无') {
					$.modalAlert("Không thể lập thuốc không có tồn kho", 'warning');
					return false;
				}
				if ($iiithis.attr('data-kzbz') == '控制') {
					$.modalAlert("Không thể lập thuốc bị kiểm soát", 'warning');
					return false;
				}
			}
			return;
		}
	}
	$(this).newtouchFloatingSelector(options);
}

//Khoa hệ thống Bộ chọn lớp nổi
$.fn.ksFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 300,
		height: 180,
		//minlength: 2,
		focusautotrigger: true,
		caption: "Chọn khoa",
		url: '/SystemManage/SysBaseData/SelectDepartmentList',
		ajaxparameters: function ($thisinput) {
		    if (!!!options.iszlks) {
		        return "keyword=" + $.trim($thisinput.val());
		    } else {
		        return "keyword=" + $.trim($thisinput.val()) + "&zlks=" + options.iszlks;
		    }
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'Mã', name: 'Code', widthratio: 45 },
			{ label: 'Tên', name: 'Name', widthratio: 50 }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}
//Lớp nổi mẫu đơn thuốc
$.fn.cfzutaoFloatingSelector12 = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 350,
		height: 280,
		focusautotrigger: true,
		caption: "Chọn bộ mẫu",
		url: '/PresTemplate/GetCfmbList',
		ajaxparameters: function ($thisinput) {
			return "mblx="+ options.mblx + "&cflx="+options.cflx+"&expandCflx=null&mbKeyword=" + $.trim($thisinput.val());
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'cfmbId', name: 'mbId', hidden: true },
			{ label: 'Tên mẫu', name: 'mbmc', widthratio: 30 },
			{
				label: 'Loại đơn thuốc',
				name: 'cflx',
				widthratio: 20,
				formatter: function(value, options, row) {
					// Chuyển đổi giá trị số thành chữ Hán
					const prescriptionTypes = {
						1: "西药处方",
						2: "中药处方",
						3: "检验处方",
						4: "检查处方",
						5: "康复处方",
						6: "常规项目处方",
					};
					return prescriptionTypes[value] || "未知类型"; // Giá trị mặc định là "Loại không xác định"
				}
			},
			{ label: 'Mô tả', name: 'Description', widthratio: 30, hidden: true},
			{ label: 'Lưu ý', name: 'Remark', widthratio: 30 ,hidden: true}
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Bộ mẫu Bộ chọn lớp nổi
$.fn.zutaoFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 350,
		height: 280,
		focusautotrigger: true,
		caption: "Chọn bộ mẫu",
		url: '/TemplateManage/GroupPackage/SelectGroupPackageList',
		ajaxparameters: function ($thisinput) {
			return "type=" + options.type + "&keyword=" + $.trim($thisinput.val());
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'ztId', name: 'ztId', hidden: true },
			{ label: 'Tên', name: 'ztmc', widthratio: 30 },
			{
				label: 'Loại', name: 'Type', widthratio: 20, hidden: true
			},
			{ label: 'Mô tả', name: 'Description', widthratio: 30 },
			{ label: 'Lưu ý', name: 'Remark', widthratio: 30 }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Vị trí hệ thống Bộ chọn lớp nổi
$.fn.bwFloatingSelector = function (options, data) {
	//Mặc địnhoptions
	var defaults = {
		width: 300,
		height: 250,
		focusautotrigger: true,
		isinputchangetriggered: false,
		mutiselect: true,
        bwlx:"pt",
		caption: "Chọn vị trí",
		url: '/SystemManage/BodyParts/GetSysBodyParts',
		ajaxparameters: function ($thisinput) {
			return "";
		}
	};
	var options = $.extend(defaults, options);
	$(this).multiquencyNewtouchFloatingSelector(options, data);
}

//Vị trí hệ thống Bộ chọn lớp nổi
$.fn.yxbwFloatingSelector = function (options, data) {
	//Mặc địnhoptions
	var defaults = {
		width: 300,
		height: 200,
		focusautotrigger: true,
		isinputchangetriggered: false,
		bwlx: "jc",
		mutiselect: true,
		caption: "Chọn vị trí",
		url: '/SystemManage/BodyParts/GetYxBodyParts',
		ajaxparameters: function ($thisinput) {
			return "";
		}
	};
	var options = $.extend(defaults, options);
	$(this).multiquencyNewtouchFloatingSelector(options, data);
}


//Dặn dò hệ thống Bộ chọn lớp nổi
$.fn.ztFloatingSelector = function (options) {
	//Mặc địnhoptions
	var defaults = {
		width: 300,
		height: 280,
		focusautotrigger: true,
		caption: "Chọn dặn dò",
		url: '/SystemManage/DoctorRemark/GetSysDoctorRemark',
		ajaxparameters: function ($thisinput) {
			return "keyword=" + $.trim($thisinput.val());
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'Mã', name: 'ztCode', widthratio: 45 },
			{ label: 'Tên', name: 'ztmc', widthratio: 50 }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Trạm bác sĩ--》Nhập văn bản--》Nút hộp văn bản chỉ định
//$.fn.zsFloatingSelector = function (options) {
//    var defaults = {
//        width: 400,
//        height: 400,
//        focusautotrigger: true,
//        caption: "Nội dung phẫu thuật",
//        url: '/SystemManage/DoctorRemark/surgery_record',
//        ajaxparameters: function ($thisinput) {
//            return "keyword=" + $.trim($thisinput.val());
//        },
//        itemdbclickhandler: null,
//        colModel: [
//            { label: 'Tên phẫu thuật', name: 'ssmc', widthratio: 33 },
//            { label: 'Thời gian phẫu thuật', name: 'sssj', widthratio: 33 },
//            { label: 'Bác sĩ phẫu thuật chính', name: 'Name', widthratio: 33 }
//        ]
//    };
//    var options = $.extend(defaults, options);

//    $(this).newtouchFloatingSelector(options);
//}



//Chẩn đoán
$.fn.zdFloatingSelector = function (options) {
	var defaults = {
		url: '/SystemManage/SysBaseData/GetDiagnosisList',
		width: 600,
		height: 200,
		clickautotrigger: true,
		caption: "Chọn chẩn đoán",
		ajaxparameters: function ($thisinput) {
			if (!!!options.ybnhlx) {
				return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx;
			} else {
				return "keyword=" + $thisinput.val() + "&zdlx=" + options.zdlx + "&ybnhlx=" + options.ybnhlx;
			}

		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'Mã', name: 'zdCode', hidden: true},
			{ label: 'Tên', name: 'zdmc', widthratio: 60 },
			{ label: 'Phiên âm', name: 'py', widthratio: 20 },
			{ label: 'icd10', name: 'icd10', widthratio: 20 },
			{ label: 'icd10Mã bổ sung', name: 'icd10fjm', hidden: true }
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Hội chứng
$.fn.zyzhFloatingSelector = function (options) {
	var defaults = {
		url: '/SystemManage/SysBaseData/GetTCMSymptomsList',
		width: 400,
		height: 200,
		clickautotrigger: true,
		caption: "Chọn hội chứng",
		ajaxparameters: function ($thisinput) {
			return "keyword=" + $thisinput.val();
		},
		itemdbclickhandler: null,
		colModel: [
			{ label: 'Mã', name: 'zhCode', hidden: true },
			{ label: 'Tên', name: 'zhmc', widthratio: 80 },
			{ label: 'Phiên âm', name: 'py', widthratio: 20 },
		]
	};
	var options = $.extend(defaults, options);

	$(this).newtouchFloatingSelector(options);
}

//Bộ chọn nhân viên vị trí
$.fn.dutyStaffFloatingSelector = function (options) {
	$(this).newtouchFloatingSelector({
		url: '/SystemManage/SysStaff/GetStaffListByDutyCode',
		width: 300,
		height: 200,
		clickautotrigger: true,
		ajaxparameters: function ($thisinput) {
			return "dutyCode=" + options.dutyCode + "&keyword=" + $thisinput.val();
		},
		colModel: [
			{ label: 'StaffGh', name: 'StaffGh', hidden: true },
			{ label: 'ks', name: 'ks', hidden: true },
			{ label: 'Tên bác sĩ', name: 'StaffName', widthratio: 48 },
			{ label: 'Tên khoa', name: 'ksmc', widthratio: 50 },
		],
		itemdbclickhandler: function ($thistr, $thisinput) {
			//Xác thực khi lưuvalvàdata-NameNhất quán
			$thisinput.attr('data-StaffGh', $thistr.attr('data-StaffGh'));
			$thisinput.attr('data-ks', $thistr.attr('data-ks'));
			$thisinput.attr('data-ksmc', $thistr.attr('data-ksmc'));
			$thisinput.val($thistr.attr('data-StaffName'));
		}
	});
}

//Chỉ định suất ăn thường
$.fn.yszsFloatingSelector = function (options, data) {
	//Mặc địnhoptions
	var defaults = {
		width: 300,
		height: 280,
		focusautotrigger: true,
		isinputchangetriggered: false,
		mutiselect: true,
		caption: "Chọn chỉ định",
		url: '/NurseManage/DietaryAdvice/GetyszsList',
		ajaxparameters: function ($thisinput) {
			return "keyword=" + options.searchdl;
		}
	};
	var options = $.extend(defaults, options);
	$(this).multiFoodFloatingSelector(options, data);
}

//Loại y lệnh (dài hạn, tạm thời) Bộ chọn lớp nổi
$.fn.yzlbFloatingSelector = function (options, data) {
	//Mặc địnhoptions
	var defaults = {
		width: 100,
		height: 40,
		focusautotrigger: true,
		isinputchangetriggered: false,
		mutiselect: true,
		caption: "Chọn loại y lệnh",
		url: '/SystemManage/SysBaseData/GetYzlb',
		ajaxparameters: function ($thisinput) {
			return "";
		}
	};
	var options = $.extend(defaults, options);
	$(this).frequencyNewtouchFloatingSelector(options, data);
}


//Phòng khám hệ thống Bộ chọn lớp nổi
$.fn.zsFloatingSelector = function (options) {
    //Mặc địnhoptions
    var defaults = {
        width: 300,
        height: 180,
        //minlength: 2,
        focusautotrigger: true,
        caption: "Chọn phòng khám",
        url: '/SystemManage/SysBaseData/SelectConsultList',
        ajaxparameters: function ($thisinput) {
            return "ksCode=" + options.ksCode + "&keyword=" + $.trim($thisinput.val());
        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Mã', name: 'Code', widthratio: 45 },
            { label: 'Tên', name: 'Name', widthratio: 50 }
        ]
    };
    var options = $.extend(defaults, options);

    $(this).newtouchFloatingSelector(options);
}
