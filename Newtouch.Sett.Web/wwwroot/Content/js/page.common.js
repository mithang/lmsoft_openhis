$.fn.dutyStaffFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetStaffListByDutyCode',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "dutyCode=" + options.dutyCode + "&keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'StaffGh', name: 'StaffGh', hidden: true },
            { label: 'ks', name: 'ks', hidden: true },
            { label: 'Tên bác sĩ', name: 'StaffName', widthratio: 38 },
            { label: 'Mã gợi nhớ', name: 'StaffPY', widthratio: 20 },
            { label: 'Tên khoa', name: 'ksmc', widthratio: 40 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //bảo hiểmlưuthời giankiểmchứngvalvàdata-Namenhất quán
            $thisinput.attr('data-label', $thistr.attr('data-StaffGh'));
            $thisinput.attr('data-ks', $thistr.attr('data-ks'));
            $thisinput.attr('data-ksmc', $thistr.attr('data-ksmc'));
            $thisinput.val($thistr.attr('data-StaffName'));
        }
    });
}

$.fn.sfxmFloatingSelector = function (options) {
    //Mặc địnhoptions
    var defaults = {
        width: 830,
        height: 200,
        clickautotrigger: true,
        url: '/SystemManage/BaseData/SelectSfxmYp',
        ajaxparameters: null,   //Vui lòng chỉ định
        itemdbclickhandler: null,   //Vui lòng chỉ định
        djDecimalPlaces: 2,  //Độ chính xác đơn giá Mặc định2
        searchType: null,   //Chỉ điều khiển ẩn/hiện cột，Không tham gia lọc//Mục tiêu tìm kiếm "yp,sfxm"、"yp"、"sfxm"、"yp.kc"、"sfxm.dwjls"、"yp,sfxm.dwjls"、"yp.kc,sfxm"、"yp.kc,sfxm.dwjls"
        showColModel: null,   //Có thể chỉ định [{name:;widthratio:;},{name:;widthratio:;}] widthratioCó thể chọn
        ypkccbjc: true,    //Kiểm tra sơ bộ tồn kho thuốc   Có bị kiểm soát hay không Có tồn kho hay không0
        colModel: [
            { label: 'Mã', name: 'sfxmCode', widthratio: 14 },
            { label: 'Tên', name: 'sfxmmc', widthratio: 25 },
            {
                label: 'Quy cách', name: 'gg', widthratio: 10
                , hidden: true
            },
            { label: 'Loại y lệnh', name: 'yzlx', hidden: true },
            { label: 'Chữ đầu phiên âm', name: 'py', widthratio: 12 },
            { label: 'Nhóm thu phí lớn', name: 'sfdlCode', hidden: true },
            {
                label: 'Nhóm thu phí lớn', name: 'sfdlmc'
                , widthratio: 10
                , hidden: true
            },
            { label: 'Đơn vị', name: 'dw', widthratio: 6 },
            {
                label: 'Đơn giá', name: 'dj', widthratio: 8, formatter: function (cellvalue) {
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
            { label: 'Chính sách thanh toán BHYT', name: 'zfxz', hidden: true },
            {
                label: 'Chính sách thanh toán BHYT', name: 'zfxzmc', widthratio: 10, formatter: function (cellvalue, a, b) {
                    return $.enum.getDescByValue("EnumZiFuXingZhi", b.zfxz);
                }
            },
            { label: 'zfbl', name: 'zfbl', hidden: true },
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
            { label: 'yfbmCode', name: 'yfbmCode', hidden: true },
            {
                label: 'Nhà thuốc', name: 'yfbmmc', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '1' || b.yzlx === '3' && !!cellvalue ? cellvalue : '';
                }, widthratio: 10, hidden: true
            },
            {
                label: 'Tồn kho', name: 'kcsl', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '1' && !!cellvalue ? cellvalue : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: 'Tồn kho', name: 'clkcsl', formatter: function (cellvalue, a, b) {
                    //được chokcsllà của đơn vị tối thiểu，Ở đây cần hiển thị khám ngoại trú/Số lượng đơn vị nội trú
                    return b.yzlx === '1' || b.yzlx === '3' ? (!!b.kcsl && !!b.cls ? (parseInt(b.kcsl / b.cls)) : '无') : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: 'Điều khiển', name: 'kzbz', formatter: function (cellvalue, a, b) {
                    return cellvalue == '1' ? 'Điều khiển' : '';
                }, widthratio: 6, hidden: true
            },
            { label: 'Ghi chú', name: 'bz', hidden: true },
        ]
    };

    var options = $.extend(defaults, options);

    if (!!!options.searchType) {
        options.searchType = "yp,sfxm"; //Mặc định
    }
    if (options.searchType && !!!options.showColModel) {
        var colsfxmmc = null;
        var colgg = null;
        var colsfdlmc = null;
        var coldwjls = null;
        var colyfbmmc = null;
        var colclkcsl = null;
        var colkzbz = null;
        $.each(options.colModel, function () {
            if (this.name === 'sfxmmc') {
                colsfxmmc = this;
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
        });
        if (!!colsfxmmc && !!colgg && !!colsfdlmc && !!coldwjls && !!colyfbmmc && !!colclkcsl && !!colkzbz) {
            //Đã bị chiếm50宽
            switch (options.searchType) {
                case "yp":
                    colsfxmmc.widthratio = 23;
                    colgg.hidden = undefined;
                    colgg.widthratio = 12;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "sfxm":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 20;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "yp,sfxm":
                case "sfxm,yp":
                    colsfxmmc.widthratio = 30;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    break;
                case "yp.kc":
                case "yp.kc,sfxm":
                case "sfxm,yp.kc":
                    colsfxmmc.widthratio = 19;
                    colgg.hidden = undefined;
                    colgg.widthratio = 11;
                    colyfbmmc.hidden = undefined;
                    colyfbmmc.widthratio = 7;
                    colclkcsl.hidden = undefined;
                    colclkcsl.widthratio = 5;
                    colkzbz.hidden = undefined;
                    colkzbz.widthratio = 6;
                    break;
                case "sfxm.dwjls":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 25;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 10;
                    break;
                case "yp,sfxm.dwjls":
                case "sfxm.dwjls,yp":
                    colsfxmmc.widthratio = 25;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 8;
                    break;
                case "yp.kc,sfxm.dwjls":
                case "sfxm.dwjls,yp.kc":
                    colsfxmmc.widthratio = 16;
                    colgg.hidden = undefined;
                    colgg.widthratio = 9;
                    colyfbmmc.hidden = undefined;
                    colyfbmmc.widthratio = 7;
                    colclkcsl.hidden = undefined;
                    colclkcsl.widthratio = 5;
                    colkzbz.hidden = undefined;
                    colkzbz.widthratio = 5;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 6;
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
                    $.modalAlert("Không thể kê thuốc không có tồn kho", 'warning');
                    return false;
                }
                if ($iiithis.attr('data-kzbz') == '控制') {
                    $.modalAlert("Không thể kê thuốc bị kiểm soát", 'warning');
                    return false;
                }
            }
            return;
        }
    }

    $(this).newtouchFloatingSelector(options);
}

//Tần suất Chung Bộ chọn lớp nổi
