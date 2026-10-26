$(function () {

    //Tải lại khi thay đổi kích thước cửa sổ trình duyệtjqGridchiều rộng của
    $(window).resize(function () {
        initLayout("");
    });

    //Tải trang，禁用控件
    DisableSysBasicInfo(false);
    //Vào trang，Tải loại ngoại trú
    $("").select2({ minimumResultsForSearch: -1 }); //去掉筛选条件
    initFPH();
    initLastFPHInfo();
    initZffs();

    //回车键
    $("").keypress(function (e) {
        if (e.keyCode === 13) {
            LoadPatientInfo($("").val(), true);
            if ($("").parent()[0].className === "") {
                $("").trigger("");
            }
            if ($("").parent()[0].className == "") {
                $("").trigger("");
            }
        }
    });
    $("").keypress(function (e) {
        //Xóa trống trước
        $("").text('');
        if (e.keyCode == 13) {
            if ($("").val() == "" || isNaN($("").val())) {
                $.modalAlert("", '');
                return false;
            }
            var zl = parseFloat($("").val()) - parseFloat($("").text());
            $("").text(zl.toFixed(2)); //保留小数点后两位小数
        }
    });
});

//Tìm kiếm
$("").click(function () {
    LoadPatientInfo($("").val(), true);
    if ($("").parent()[0].className == "") {
        $("").trigger("");
    }
    if ($("").parent()[0].className == "") {
        $("").trigger("");
    }
})

//Quẹt thẻ
function SK() {
    $.modalAlert("", '');
    return false;
}

//Vào trang，Tải số hóa đơn
function initFPH() {
    $.najax({
        url: "" + Math.random(),
        dataType: "",
        async: false,
        success: function (data) {
            $("").val(data);
        }
    });
}

//Vào trang，Tải thông tin hóa đơn tờ một trên
function initLastFPHInfo() {
    $.najax({
        url: "" + Math.random(),
        dataType: "",
        async: false,
        success: function (data) {
            //Tra cứuok
            if (data != null && data.length > 0) {
                $("").val(data[0].fph);
                $("").val(data[0].xjzf);
                $("").val(data[0].ysk);
                $("").val(data[0].zl);
            } else //Tra cứuok，Thông tin quyết toán đăng ký khám không còn ngày khi nhưng
            {
                $("").val('');
                $("").val('');
                $("").val('');
                $("").val('');

            }
        }
    });
}

//Vào trang，Tải phương thức thanh toán
function initZffs() {
    $("").newtouchBindSelect({
        id: "",
        text: "",
        datasource: function () {
            //Duyệt nguồn dữ liệu
            var resultObjArr = new Array();
            $.each(top.clients.SysForCashPayList, function (idx, val) {
                if (val.zhxz == '') {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;
        }
    });

}

//Đăng ký không cần thẻ
function FreeCardReg() {
    $.modalOpen({
        id: "",
        title: "",
        url: "",
        width: "",
        height: "",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//Thông tin tải số thẻ theo
var kh = "";
function LoadPatientInfo(kh, isAutoOpenFreeCardReg) {
    if (!kh) {
        return;
    }
    $("").val(kh.toUpperCase());
    $.najax({
        url: "" + kh + "" + Math.random(),
        dataType: "",
        async: true,
        alertbierror: false,
        success: function (data) {
            $("").text(data.data.xm);
            $("").text($.getGender(data.data.xb));
            $("").val(data.data.fzbz);
            if (data.data.fzbz == 0) {
                $("").val("");
            } else {
                $("").val("");
            }
            $("").val(data.data.patid);
            $("").val(data.data.csny);
            $("").val(data.data.db);
            $("").val(data.data.dbzd);
            $("").val(data.data.blh);
            $("").val(data.data.brxz);
            $("").val("" + data.data.brxz + "" + data.data.brxzmc);
            $("").val(data.data.brxzbh);
            DisableSysBasicInfo(true); //true:标志是从免卡登记或根据卡号查询返回明细, false:Giờ tải trang，可以输kh
        },
        errorCallback: function (data) {
            newtouch_globalevent_f4();
            $("").val(kh.toUpperCase());
            if (isAutoOpenFreeCardReg === true) {
                FreeCardReg();
            }
            else {
                $.newtouchAlert(data);
            }
            return;
        }

    });
}

//Cửa sổ bật lên số hóa đơn
function ShowInvoicePanel() {
    $.modalOpen({
        id: "",
        title: "",
        url: "",
        width: "",
        height: "",
        callBack: function (iframeId) {
            top.frames[iframeId].checkFPH();//Hàm callback xác nhận khi nhấn cửa sổ
        }
    });
}

//Lớp nổi tính chất bệnh nhân
$("").newtouchFloatingSelector({
    height: 200,
    width: 300,
    filter: function (keyword) {
        if (!keyword) {
            return null;
        }
        //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
        var resultObjArr = new Array();
        $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
            if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                resultObjArr.push(val);
            }
        });
        return resultObjArr;

    },
    caption: "",
    colModel: [
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 }
    ],
    itemdbclickhandler: function ($thistr) {
        checkBrxz($thistr.attr(''));
        $("").val("" + $thistr.attr('') + "" + $thistr.attr(''));
        $("").val($thistr.attr(''));
        $("").val($thistr.attr(''));
        return;
    }
});

//checkTính chất bệnh nhân
function checkBrxz(updateBrxz) {
    var kh = $("").val();
    $.najax({
        url: "" + kh + "" + updateBrxz + "" + Math.random(),
        dataType: "",
        async: false,
        success: function (data) {

        },
        errorCallback: function (data) {   //Cho trạng tháierrorThông tin lỗi bật lên tự động giờ，该方法为弹出错误信息之后需做的事情

        }
    })
}

//门诊类型发生改变清空科室/Bác sĩ
function sel_mzlxChange() {
    $("").val('');
}

//Loại đăng ký khám
var currentghlx;
//Lớp nổi lịch trực đăng ký khám
$("").newtouchBatchFloatingSelector({
    height: 200,
    width: 350,
    clickautotrigger: true,
    id: '',
    url: '',
    ajaxmethod: '',
    ajaxreqdata: function () {
        var reqData = {};
        reqData.keyword = $("").val();
        reqData.mzlx = $("").val();
        return reqData;
    },
    caption: "",
    colModel: [
        { label: '', name: '', hidden: true, widthratio: 0 },
        { label: '', name: '', hidden: true, widthratio: 0 },
        { label: '', name: '', hidden: true, widthratio: 0 },
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        {
            label: '', name: '', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue === "" ? "" : cellvalue;
            }
        },
        { label: '', name: '', hidden: true, widthratio: 0 },
        { label: '', name: '', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("").val() === "" || $("").val() === "")//Ngoại trú/Cấp cứu  Khoa hiển thị chỉ
        {
            $("").val($thistr.attr(''));
        }
        if ($("").val() === "")//Khám chuyên gia  Hiển thị khoa và Bác sĩ
        {
            $("").val($thistr.attr('') + "" + $thistr.attr(''));
        }
        $("").val($thistr.attr(''));
        $("").val($thistr.attr(''));
        $("").val($thistr.attr(''));
        $("").val($thistr.attr(''));
        $("").val($thistr.attr(''));
        currentghlx = $thistr.attr('');
        RegisterInfo();
        return;
    }
});

//Tải thông tin đăng ký khám đang 
function RegisterInfo() {
    //var rowIDs = $("").getDataIDs();
    //for (var i = 0; i < rowIDs.length; i++) {
    //    var getRow = $("").getRowData(rowIDs[i]);
    //    if ($("").val() == getRow.ghlx) {
    //        $.modalAlert("", '');
    //        $("").val("");
    //        return;
    //    }

    //}
    $('').jqGrid("");
    if ($("").val() != null && $("").val() !== "") {

        //tab页中所需的数据保存到一个对象
        list = new Object();
        list.ghlx = $("").val();
        list.brxz = $("").val();
        list.brxzbh = $("").val();
        list.dbxm = $("").val();
        //Lấy phí đăng ký khám Phí chẩn trị Phí thẻ từ Phí phôi
        var feesList = GHFees(true);
        list.ghf = feesList.ghf;
        list.zlf = feesList.zlf;
        list.ckf = feesList.ckf;
        list.gbf = feesList.gbf;
        list.totalfees = feesList.totalfees;

        //Đang đăng ký khám
        registeringListNew([list]);
        //$("").attr("", "").css("", "");
        $("").text(list.totalfees);  //Khoản phải thu
        $("").addClass('');
    }
}

//Lấy phí đăng ký khám
function GHFees(flag) {
    var isZzjm = false;
    var isCkf = false;
    var isGbf = false;
    if ($("").is("")) {
        isZzjm = true;
    }
    if ($("").is("")) {
        isCkf = true;
    }
    if ($("").is("")) {
        isGbf = true;
    }
    feesList = new Object();
    $.najax({
        url: "",
        dataType: "",
        data: { ghlx: currentghlx, isZzjm: isZzjm, isCkf: isCkf, isGbf: isGbf },
        type: "",
        async: false,
        success: function (data) {
            feesList.ghf = data.ghfPrice;
            feesList.zlf = data.zlfPrice;
            if (!isCkf) {
                feesList.ckf = "";
            } else {
                feesList.ckf = data.ckfPrice;
            }
            if (!isGbf) {
                feesList.gbf = "";
            } else {
                feesList.gbf = data.gbfPrice;
            }

            feesList.totalfees = data.totalfees;
        }
    });
    //单独点击CheckBox
    if (!flag) {
        //var rowData = $('').jqGrid('', 1);
        $('').jqGrid('', 1, '', feesList.ckf);
        $('').jqGrid('', 1, '', feesList.gbf);
        $('').jqGrid('', 1, '', feesList.totalfees);
        //Cập nhậtGridđồng thời của chi phí，Cập nhật số tiền tổng
        //$("").attr("", "").css("", "");
        $("").text(feesList.totalfees);

    }
    return feesList;
}


//Hủy số khám
function BackNum() {
    var currtab = $("").parent();
    if (currtab[0].className === "") {
        if ($("").getDataIDs().length == 0) {
            $.modalAlert("", '');
            return;
        }
        var seleRowid = jQuery("").jqGrid("", "");
        if (!(seleRowid)) {
            $.modalAlert("", '');
            return;
        }
        var seleRowData = $("").jqGrid('', seleRowid);
        if (seleRowData.isth === "") {
            $.modalAlert("", '');
            return;
        }
        seleRowData.isReturnCKGBFee = (seleRowData.isReturnCKGBFee === '');
        $.modalConfirm("", function (flag) {
            if (flag) {
                $.najax({
                    url: "",
                    dataType: "",
                    data: {
                        patid: $("").val(), ghnm: seleRowData.ghnm, isReturnCKGBFee: seleRowData.isReturnCKGBFee
                    },
                    type: "",
                    async: false,
                    alertbierror: false,
                    success: function (data) {
                        $.loading(false);
                        if (data === true) {
                            $.modalAlert("", '');
                            $("").trigger("");
                        }
                        else {
                            $.newtouchAlert(data);
                        }
                    }
                });
            }
            else {
                return false;
            }
        }, false); //false:modalConfirmKhông đóngdialog
    }
    else {
        $.modalAlert("", '');
        return;
    }
}

//Lưu
function Save() {
    //先看当前是不是在正在挂号列表，再看列表有没有数据，Có nếu Mặc định dòng một thứ，若没有则提示
    var currtab = $("").parent();
    if (currtab[0].className === "") {
        //LấyjqGridsố dòng
        var count = $("").getGridParam('');
        if (count > 0) {
            var regList = new Object();
            regList.fph = $("").val();
            regList.patid = $("").val();
            regList.kh = $("").val();
            regList.brxz = $("").val();
            regList.ghlx = $("").val();
            var ysk = 0;
            ysk = $("").text();
            regList.ssk = $("").val();
            //Xác minh dữ liệu
            if (!CheckItem(regList)) {
                return false;
            }
            if (parseFloat(regList.ssk) < parseFloat(ysk)) {
                $.modalAlert("", '');
                return false;
            } else {
                var zl = parseFloat($("").val()) - parseFloat($("").text());
                //Book(ysk, regList.ssk);
                //结算窗体
                showBillSuccessDialog(ysk, regList.ssk, 0, zl.toFixed(2));
            }

        } else {
            $.modalAlert("", '');
            return false;
        }
    } else {
        $.modalAlert("", '');
    }
}

//下暂存单
function Book(ysk, ssk) {
    var selRowRowId = $("").jqGrid("", "");
    if (!selRowRowId && $("").getGridParam('') === 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("", '');
    }

    var selRow = $("").jqGrid('', selRowRowId);

    var jdata = {
        patid: $("").val(),
        brxz: $("").val(),
        brxzbh: $("").val(),
        ghly: "",
        mjzbz: $.currentWindow().$("").val(),////Cờ ngoại trú cấp cứu 1：Ngoại trú 2：Cấp cứu
        ksbh: $("").val(),
        ks: $("").val(),
        ys: $("").val(),
        ghlxbh: $("").val(),
        ghlx: $("").val(),
        ghzbbh: 0,
        ghzb: 0,
        fzbz: $("").val(),
        jzbz: "",
        ghzt: "",
        ghxz: 0,
        dbxm: $("").val(),
        gsrdh: "",
        zdicd10: "",
        zdmc: "",
        ghf: selRow.ghf,
        zlf: selRow.zlf,
        ckf: selRow.ckf,
        gbf: selRow.gbf,
        zzjm: $("").is("") ? true : false,
        kh: $("").val(),
        ssk: ssk,
        fph: $("").val(),
        isCkf: $("").is("") ? true : false,
        isGbf: $("").is("") ? true : false,
        xm: $("").text(),
        xb: $.getGenderCode($("").text()),
        csny: $("").val(),
        blh: $("").val(),
        updateBrxz: $("").val()
    };
    $.loading(true, "");
    //Lưu
    $.najax({
        url: "",
        dataType: "",
        data: jdata,
        type: "",
        async: false,
        success: function (data) {
            $.loading(false, "");
            $("").val(parseInt(data));
        },
        errorCallback: function (data) {
            $.loading(false, "");
        },
        complete: function () {
            $.loading(false, "");
        }
    });
}

//Cửa sổ bật lên xác nhận số tiền
function showBillSuccessDialog(ysk, ssk, srce, zl) {
    var url = "" + ysk
        + "" + ssk
        + "" + srce
        + "" + zl;
    $.modalOpen({
        id: "",
        title: "",
        url: url,
        width: "",
        height: "",
        callBack: function (iframeId) {
            $.modalClose("");   //Đóng biểu mẫu số tiền quyết toán
            $.loading(true, "");
            SaveMethod(ysk, ssk);
        }
    });
}
//Thực hiện lưu cụ thể
function SaveMethod(ysk, ssk) {
    var mzlx = $.currentWindow().$("").val(); //Cờ ngoại trú cấp cứu 1：Ngoại trú 2：Cấp cứu
    var isZzjm = "";
    if ($("").is("")) {
        isZzjm = true;
    } else {
        isZzjm = false;
    }
    var isCkf = false;
    if ($("").is("")) {
        isCkf = true;
    }
    var isGbf = false;
    if ($("").is("")) {
        isGbf = true;
    }

    var selRowRowId = $("").jqGrid("", "");
    if (!selRowRowId && $("").getGridParam('') == 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("", '');
    }

    var selRow = $("").jqGrid('', selRowRowId);

    var ghf = selRow.ghf;
    var zlf = selRow.zlf;
    var ckf = selRow.ckf;
    var gbf = selRow.gbf;
    var xb = $.getGender($("").text());
    var orderId = 0;
    if ($("").val() !== "") {
        orderId = parseInt($("").val());
    }

    //Lưu
    $.najax({
        url: "",
        dataType: "",
        //ghxz:0 Đăng ký khám thông thường； jzbz:"",ghzt:"" Trường không hiệu lực； ghzbbh:0,ghzb:0 暂且默认为0；ghly: "" 挂号来源为窗口；  gsrdh:"",zdicd10:"",zdmc:"" 暂且默认空
        data: { OrderId: orderId, patid: $("").val(), brxz: $("").val(), brxzbh: $("").val(), ghly: "", mjzbz: mzlx, ksbh: $("").val(), ks: $("").val(), ys: $("").val(), ghlxbh: $("").val(), ghlx: $("").val(), ghzbbh: 0, ghzb: 0, fzbz: $("").val(), jzbz: "", ghzt: "", ghxz: 0, dbxm: $("").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: gbf, zzjm: isZzjm, kh: $("").val(), ssk: ssk, fph: $("").val(), isCkf: isCkf, isGbf: isGbf, xm: $("").text(), xb: xb, csny: $("").val(), blh: $("").val(), updateBrxz: $("").val() },
        type: "",
        async: false,
        success: function (data) {
            $.modalAlert("", '');

            //Lại tải số hóa đơn
            initFPH();
            newtouch_globalevent_f4();
            $("").text = String.Empty;
            $("").text = String.Empty;
            //Làm mới thông tin quyết toán lần một trên
            $("").jqGrid('', {}).trigger('');
            $.loading(false);
        },
        errorCallback: function (data) {
            $.loading(false);
            $("").text = String.Empty;
            $("").text = String.Empty;
        }
    });

}

//Lưu xác minh
function CheckItem(RegList) {
    if (RegList.fph == "") {
        $.modalAlert("", '');
        return;
    }
    if (RegList.patid == "") {
        $.modalAlert("", '');
        return false;
    }
    if (RegList.kh == "") {
        $.modalAlert("", '');
        return false;
    }
    if (RegList.brxz == "") {
        $.modalAlert("", '');
        return false;
    }
    if (RegList.ghlx == "") {
        $.modalAlert("", '');
        return false;
    }
    if (RegList.ssk == "" || isNaN(RegList.ssk)) {
        $.modalAlert("", '');
        return false;
    }

    return true;
}

//Phím tắt：Xóa
function newtouch_event_f4() {
    //Xóa trốngGrid
    $('').jqGrid("");
    $('').jqGrid("");
    $('').text("");
    $('').text("");
    //$("").removeAttr("");
    //$("").parent().removeClass('').css('', '');

    //启用控件
    EnabledSysBasicInfo();
}
//Phím tắt:Lưu
function newtouch_event_f8() {
    Save();
}
//Phím tắt：Hủy số khám
function newtouch_event_f9() {
    BackNum();
}

//禁用
function DisableSysBasicInfo(flag) {
    //true:标志是从免卡登记或根据卡号查询返回明细, false:Giờ tải trang，可以输kh
    if (flag) {
        $("").attr("", "").css("", "");
        $("").removeAttr("").removeAttr("");
        $('').each(
            function () {
                $(this).parent().attr("", "");
            }
        );
    } else {
        $("").attr("", "").css("", "");
    }
    //$("").attr("", "").css("", "");
    //$("").attr("", "").css("", "");
}

//Bật
function EnabledSysBasicInfo() {
    $("").removeAttr("").removeAttr("");
}






