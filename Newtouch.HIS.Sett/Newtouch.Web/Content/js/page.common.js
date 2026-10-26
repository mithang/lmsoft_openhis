//Hạng mục thu phí Lớp nổi（Bao gồm thuốc、Hạng mục，Hạng mục điều trị không có thể cũng）
$.fn.sfxmFloatingSelector = function (options) {
    //Mặc địnhoptions
    var defaults = {
        width: 830,
        height: 200,
        clickautotrigger: true,
        url: '',
        ajaxparameters: null,   //请指定
        itemdbclickhandler: null,   //请指定
        djDecimalPlaces: 2,  //单价精度 Mặc định2
        searchType: null,   //仅控制列显隐，不参与筛选//检索目标 ""、""、""、""、""、""、""、""
        showColModel: null,   //可指定 [{name:;widthratio:;},{name:;widthratio:;}] widthratio可选
        ypkccbjc: true,    //药品库存初步检查   是否被控制 是否库存0
        colModel: [
            { label: '', name: '', widthratio: 14 },
            { label: '', name: '', widthratio: 25 },
            {
                label: '', name: '', widthratio: 10
                , hidden: true
            },
            { label: '', name: '', hidden: true },
            { label: '', name: '', widthratio: 12 },
            { label: '', name: '', hidden: true },
            {
                label: '', name: ''
                , widthratio: 10
                , hidden: true
            },
            { label: '', name: '', widthratio: 6 },
            {
                label: '', name: '', widthratio: 8, formatter: function (cellvalue) {
                    return (!!cellvalue || cellvalue == 0) ? (parseFloat(cellvalue).toFixed(options.djDecimalPlaces ? options.djDecimalPlaces : 2)) : '';
                }
            },
            { label: '', name: '', hidden: true },
            {
                label: '', name: '', widthratio: 8, formatter: function (cellvalue) {
                    return !!cellvalue ? cellvalue : '';
                }
                , hidden: true
            },
            { label: '', name: '', hidden: true },
            {
                label: '', name: '', widthratio: 10, formatter: function (cellvalue, a, b) {
                    return $.enum.getDescByValue("", b.zfxz);
                }
            },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true },
            {
                label: '', name: '', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '' || b.yzlx === '' && !!cellvalue ? cellvalue : '';
                }, widthratio: 10, hidden: true
            },
            {
                label: '', name: '', formatter: function (cellvalue, a, b) {
                    return b.yzlx === '' && !!cellvalue ? cellvalue : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: '', name: '', formatter: function (cellvalue, a, b) {
                    //chokcsl是最小单位的，这里要显示门诊/số lượng nhập viện đơn vị
                    return b.yzlx === '' || b.yzlx === '' ? (!!b.kcsl && !!b.cls ? (parseInt(b.kcsl / b.cls)) : '') : '';
                }, widthratio: 6, hidden: true
            },
            {
                label: '', name: '', formatter: function (cellvalue, a, b) {
                    return cellvalue == '' ? '' : '';
                }, widthratio: 6, hidden: true
            },
            { label: '', name: '', hidden: true },
        ]
    };

    var options = $.extend(defaults, options);

    if (!!!options.searchType) {
        options.searchType = ""; //Mặc định
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
            if (this.name === '') {
                colsfxmmc = this;
            }
            else if (this.name === '') {
                colgg = this;
            }
            else if (this.name === '') {
                colsfdlmc = this;
            }
            else if (this.name === '') {
                coldwjls = this;
            }
            else if (this.name === '') {
                colyfbmmc = this;
            }
            else if (this.name === '') {
                colclkcsl = this;
            }
            else if (this.name === '') {
                colkzbz = this;
            }
        });
        if (!!colsfxmmc && !!colgg && !!colsfdlmc && !!coldwjls && !!colyfbmmc && !!colclkcsl && !!colkzbz) {
            //已被占用50宽
            switch (options.searchType) {
                case "":
                    colsfxmmc.widthratio = 23;
                    colgg.hidden = undefined;
                    colgg.widthratio = 12;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 20;
                    colsfdlmc.hidden = undefined;
                    colsfdlmc.widthratio = 12;
                    break;
                case "":
                case "":
                    colsfxmmc.widthratio = 30;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    break;
                case "":
                case "":
                case "":
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
                case "":
                    colgg.hidden = undefined;
                    colgg.widthratio = 10;
                    colsfxmmc.widthratio = 25;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 10;
                    break;
                case "":
                case "":
                    colsfxmmc.widthratio = 25;
                    colgg.hidden = undefined;
                    colgg.widthratio = 15;
                    coldwjls.hidden = undefined;
                    coldwjls.widthratio = 8;
                    break;
                case "":
                case "":
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
        //[{name:;widthratio:;},{name:;widthratio:;}] widthratio可选
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

    if (options.searchType.indexOf('') >= 0 && options.ypkccbjc === true) {
        options.itemdbclickhandleBefore = function ($iiithis) {
            if ($iiithis.attr('') == '') {
                if ((!isNaN($iiithis.attr('')) && parseInt($iiithis.attr('')) <= 0) || $iiithis.attr('') == '') {
                    $.modalAlert("", '');
                    return false;
                }
                if ($iiithis.attr('') == '') {
                    $.modalAlert("", '');
                    return false;
                }
            }
            return;
        }
    }

    $(this).newtouchFloatingSelector(options);
}

//Tần suất Chung 浮层选择器
$.fn.pcFloatingSelector = function (options) {
    //Mặc địnhoptions
    var defaults = {
        width: 280,
        height: 280,
        //minlength: 2,
        focusautotrigger: true,
        className: "",
        caption: "",
        url: '',
        ajaxparameters: function ($thisinput) {
            return "";
        }
    };
    var options = $.extend(defaults, options);

    $(this).frequencyNewtouchFloatingSelector(options);
}

//系统部位 浮层选择器
$.fn.bwFloatingSelector = function (options, data) {
    //Mặc địnhoptions
    var defaults = {
        width: 300,
        height: 280,
        focusautotrigger: true,
        isinputchangetriggered: false,
        mutiselect: true,
        caption: "",
    };
    data = $.itemDetails.getItems('', false);
    var options = $.extend(defaults, options);
    $(this).multiquencyNewtouchFloatingSelector(options, data);
}


//报价器语音地址接口
function BJQYY(postdata,asynctype){
	$.ajax({
		type: "",
		url: "",
		data: postdata,
		dataType: "",
		async: asynctype,
		success: function (data) {
		},
		error: function (ex) {
			console.log("", ex);
		}
	});
}