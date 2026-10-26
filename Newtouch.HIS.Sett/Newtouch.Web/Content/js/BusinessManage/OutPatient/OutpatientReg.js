$(function () {

    //Tải lại khi thay đổi kích thước cửa sổ trình duyệtjqGridchiều rộng của
    $(window).resize(function () {
        initLayout("");
    });

    //Tải trang，禁用控件
    DisableSysBasicInfo(false);
    initFPH();
    initLastFPHInfo();
    initZffs();

    //回车键
    $("").keypress(function (e) {
        if (e.keyCode == 13) {
            LoadPatientInfo()
            if ($("").parent()[0].className == "") {
                $("").trigger("");
            }
            if ($("").parent()[0].className == "") {
                $("").trigger("");
            }
        }
    });
    $("").keypress(function (e) {
        if (e.keyCode == 13) {
            var zl = parseFloat($("").val()) - parseFloat($("").val());
            $("").val(zl.toFixed(2)); //保留小数点后两位小数
            $("").attr("", "").css("", "");
        }
    });
});

//Vào trang，Tải số hóa đơn
function initFPH() {
    $.ajax({
        url: "",
        dataType: "",
        async: false,
        success: function (data) {
            $("").val(data);
        }
    });
}

//Vào trang，Tải thông tin hóa đơn tờ một trên
function initLastFPHInfo() {
    $.ajax({
        url: "",
        dataType: "",
        async: false,
        success: function (data) {
            //Tra cứuok
            if (data.state = "" & data != null) {
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
            console.log(resultObjArr);
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
function LoadPatientInfo() {
    var kh = $("").val();
    $.ajax({
        url: "" + kh,
        dataType: "",
        async: false,
        success: function (data) {
            if (data.state == "") {
                newtouch_globalevent_f4(event);
                FreeCardReg();
            } else {
                $("").val(data.xm);
                $("").val($.getGender(data.xb));
                $("").val(data.fzbz);
                if (data.fzbz == 0) {
                    $("").val("");
                } else {
                    $("").val("");
                }
                $("").val(data.patid);
                $("").val(data.csny);
                $("").val(data.db);
                $("").val(data.dbzd);
                $("").val(data.blh);
                $("").val(data.brxz);
                $("").val("" + data.brxz + "" + data.brxzmc);
                $("").val(data.brxzbh);
                DisableSysBasicInfo(true); //true:标志是从免卡登记或根据卡号查询返回明细, false:Giờ tải trang，可以输kh

            }
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

//Lớp nổi loại bệnh nhân
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
            if ((val.brxzmc && val.brxzmc.indexOf(keyword) >= 0)
                || (val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)) {
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
        $("").attr("", $thistr.find("").html() + "" + $thistr.find("").html());
        $("").val($thistr.find('').html());
        return;
    }
});

//Loại đăng ký khám
var currentghlx;
//Lớp nổi lịch trực đăng ký khám
$("").newtouchFloatingSelector({
    height: 200,
    width: 350,
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
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        { label: '', name: '', widthratio: 25 },
        {
            label: '', name: '', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue == "" ? "" : cellvalue;
            }
        },
        { label: '', name: '', hidden: true, widthratio: 0 },
        { label: '', name: '', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("").val() == "" || $("").val() == "")//Ngoại trú/Cấp cứu  Khoa hiển thị chỉ
        {
            $("").val($thistr.find('').html());
        }
        if ($("").val() == "")//Khám chuyên gia  Hiển thị khoa và Bác sĩ
        {
            $("").val($thistr.find('').html() + "" + $thistr.find('').html());
        }
        $("").val($thistr.find('').html());
        $("").val($thistr.find('').html());
        $("").val($thistr.find('').html());
        $("").val($thistr.find('').html());
        currentghlx = $thistr.attr('');
        RegisterInfo();


        return;
    }

});

//Tải thông tin đăng ký khám đang 
function RegisterInfo() {
    if ($("").val() != null & $("").val() != "") {


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
        $("").attr("", "").css("", "");
        $("").val(list.totalfees);  //Khoản phải thu
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
    //if ($("").is("") & $("").is(""))
    //{
    //    return false;
    //}
    feesList = new Object();
    $.ajax({
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
        $("").attr("", "").css("", "");
        $("").val(feesList.totalfees);

    }
    return feesList;
}


//Hủy số khám
function BackNum() {
    var currtab = $("").parent();
    if (currtab[0].className == "") {
        var count = $("").getGridParam('');
        if (count >= 1) {
            var ghnm = $("").jqGridRowValue().ghnm;
            if (ghnm == null) {
                $.modalAlert("", '');
                return;
            }
            else {
                if ($("").jqGridRowValue().isth == "") {
                    $.modalAlert("", '');
                    return;
                }
                var isReturnAll = false;
                if ($("").is("")) {
                    isReturnAll = true;
                }
                if (window.confirm("")) {
                    var ghnm = $("").jqGridRowValue().ghnm;
                    $.ajax({
                        url: "",
                        dataType: "",
                        data: { patid: $("").val(), ghnm: ghnm, isReturnAll: isReturnAll },
                        type: "",
                        async: false,
                        success: function (data) {
                            $.loading(false);
                            $.modalAlert("", '');
                            $("").trigger("");
                        }
                    });

                } else {
                    return false;
                }
            }
        }
        else {
            $.modalAlert("", '');
        }
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
    if (currtab[0].className == "") {
        //LấyjqGridsố dòng
        var count = $("").getGridParam('');
        if (count > 0) {
            RegList = new Object();
            RegList.fph = $("").val();
            RegList.patid = $("").val();
            RegList.kh = $("").val();
            RegList.brxz = $("").val();
            RegList.ghlx = $("").val();
            //Xác minh dữ liệu
            if (!CheckItem(RegList)) {
                return false;
            }
            //var isJS;
            var ysk = 0;
            var ssk = 0;
            ysk = $("").val();
            ssk = $("").val();
            zl = $("").val();
            if (parseFloat(ssk) < parseFloat(ysk)) {
                $.modalAlert("", '');
                return;
            } else {
                var zl = parseFloat($("").val()) - parseFloat($("").val());
                //结算窗体
                showBillSuccessDialog(ysk, ssk, 0, zl.toFixed(2));

            }

        } else {
            $.modalAlert("", '');
            return;
        }
    }
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
    var jzxh = 0;  //Số thứ tự khám
    $.ajax({
        url: "",
        dataType: "",
        data: { ghlx: $("").val(), ks: $("").val(), ys: 0, ghzb: 0 },
        type: "",
        async: false,
        success: function (data) {
            if (data == null || data == "") {
                $.modalAlert("", '');
                return;
            }
            else {
                jzxh = data;
            }
        }
    });
    var zzjm = "";
    if ($("").is("")) {
        zzjm = "";
    } else {
        zzjm = "";
    }
    var ghf = ghf = $("").jqGridRowValue().ghf;
    var zlf = zlf = $("").jqGridRowValue().zlf;

    var ckf = 0
    if ($("").jqGridRowValue().ckf != "") {
        ckf = $("").jqGridRowValue().ckf;
    }
    var gbf = 0;
    if ($("").jqGridRowValue().gbf != "") {
        gbf = $("").jqGridRowValue().gbf;
    }
    //Lưu
    $.ajax({
        url: "",
        dataType: "",
        //ghxz:0 Đăng ký khám thông thường； jzbz:"",ghzt:"" Trường không hiệu lực； ghzbbh:0,ghzb:0 暂且默认为0；ghly: "" 挂号来源为窗口；  gsrdh:"",zdicd10:"",zdmc:"" 暂且默认空
        data: { patid: $("").val(), brxz: $("").val(), brxzbh: $("").val(), ghly: "", mjzbz: mzlx, ksbh: $("").val(), ks: $("").val(), rybh: $("").val(), ys: 0, ghlxbh: $("").val(), ghlx: $("").val(), ghzbbh: 0, ghzb: 0, jzxh: jzxh, fzbz: $("").val(), jzbz: "", ghzt: "", ghxz: 0, dbxm: $("").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: ghf, zzjm: zzjm, kh: $("").val(), ysk: ysk, ssk: ssk, fph: $("").val() },
        type: "",
        async: false,
        success: function (data) {
            if (data.state == "") {
                $.modalAlert("", '');

                //Lại tải số hóa đơn
                initFPH();
                Clear();
                $.loading(false);
            }
            else if (data.state == "") {
                $.modalAlert("", '');
                $.loading(false);
                return;
            }
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
    return true;
}
//Xóa
function Clear() {

    //Xóa trốngGrid
    $('').jqGrid("");
    $('').jqGrid("");
    $('').jqGrid("");

    //Xóa trống điều khiển
    newtouch_globalevent_f4(event);

    //启用控件
    EnabledSysBasicInfo();
}

//禁用
function DisableSysBasicInfo(flag) {
    //true:标志是从免卡登记或根据卡号查询返回明细, false:Giờ tải trang，可以输kh
    if (flag) {
        $("").attr("", "").css("", "");
    } else {
        $("").attr("", "").css("", "");
    }
}

//Bật
function EnabledSysBasicInfo() {
    $("").removeAttr("").removeAttr("");;
}


//Hủy bỏ 需求变更：去掉未结算内容 update by caishanshan 20161229
//function Abandon() {
//    var currtab = $("").parent();
//    if (currtab[0].className == "") {
//        var count = $("").getGridParam('');
//        if (count >= 1) {
//            var ghnm = $("").jqGridRowValue().ghnm;
//            if (ghnm == null) {
//                $.modalAlert("", '');
//                return;
//            }
//            else
//            {
//                if (window.confirm("")) {
//                    $.ajax({
//                        url: "",
//                        dataType: "",
//                        data: { ghnm: ghnm },
//                        type: "",
//                        async: false,
//                        success: function (data) {
//                            if (data.state == "") {
//                                $.loading(false);
//                                $.modalAlert("", '');
//                                $("").trigger("");
//                            } else {
//                                $.modalAlert("", '');
//                                return;
//                            }
//                        }
//                    });
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }
//        else
//        {
//            $.modalAlert("", '');
//            return;
//        }
//    }
//    else
//    {
//        $.modalAlert("", '');
//        return;
//    }
//}




