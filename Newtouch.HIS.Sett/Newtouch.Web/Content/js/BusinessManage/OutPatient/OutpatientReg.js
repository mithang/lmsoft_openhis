$(function () {

    //Tải lại khi thay đổi kích thước cửa sổ trình duyệtjqGridchiều rộng của
    $(window).resize(function () {
        initLayout("MyTabGrid");
    });

    //Tải trang，Vô hiệu hóa điều khiển
    DisableSysBasicInfo(false);
    initFPH();
    initLastFPHInfo();
    initZffs();

    //Phím Enter
    $("#txtkh").keypress(function (e) {
        if (e.keyCode == 13) {
            LoadPatientInfo()
            if ($("#linkregistering").parent()[0].className == "active") {
                $("#linkregistering").trigger("click");
            }
            if ($("#linkclosedReg").parent()[0].className == "active") {
                $("#linkclosedReg").trigger("click");
            }
        }
    });
    $("#txtssk").keypress(function (e) {
        if (e.keyCode == 13) {
            var zl = parseFloat($("#txtssk").val()) - parseFloat($("#txtysk").val());
            $("#txtzl").val(zl.toFixed(2)); //Giữ hai chữ số thập phân
            $("#txtzl").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        }
    });
});

//khi vào trang，Tải số hóa đơn
function initFPH() {
    $.ajax({
        url: "/BusinessManage/OutPatient/GetInvoice",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#txtfph").val(data);
        }
    });
}

//khi vào trang，Tải thông tin hóa đơn trước
function initLastFPHInfo() {
    $.ajax({
        url: "/BusinessManage/OutPatient/GetLastSettleInfo",
        dataType: "json",
        async: false,
        success: function (data) {
            //Tra cứuok
            if (data.state = "success" & data != null) {
                $("#lblqfph").val(data[0].fph);
                $("#lblzje").val(data[0].xjzf);
                $("#lblys").val(data[0].ysk);
                $("#lblzl").val(data[0].zl);
            } else //Tra cứuok，Nhưng trong ngày chưa có thông tin quyết toán đăng ký khám
            {
                $("#lblqfph").val('');
                $("#lblzje").val('');
                $("#lblys").val('');
                $("#lblzl").val('');

            }
        }
    });
}

//khi vào trang，Tải phương thức thanh toán
function initZffs() {
    $("#ddlzffs").newtouchBindSelect({
        id: "xjzffsbh",
        text: "xjzffsmc",
        datasource: function () {
            //Duyệt nguồn dữ liệu
            var resultObjArr = new Array();
            $.each(top.clients.SysForCashPayList, function (idx, val) {
                if (val.zhxz == 'a') {
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
        id: "Form",
        title: "Đăng ký không cần thẻ",
        url: "/PatientManage/HospiterRes/PatientBasic",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//Tải thông tin theo số thẻ
function LoadPatientInfo() {
    var kh = $("#txtkh").val();
    $.ajax({
        url: "/BusinessManage/OutPatient/LoadPatientInfo?kh=" + kh,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.state == "error") {
                newtouch_globalevent_f4(event);
                FreeCardReg();
            } else {
                $("#txtxm").val(data.xm);
                $("#txtxb").val($.getGender(data.xb));
                $("txtfzbz").val(data.fzbz);
                if (data.fzbz == 0) {
                    $("#txtcfz").val("[0]初诊");
                } else {
                    $("#txtcfz").val("[1]复诊");
                }
                $("#txtpatid").val(data.patid);
                $("#txtcsny").val(data.csny);
                $("#txtdb").val(data.db);
                $("#txtdbzd").val(data.dbzd);
                $("#txtblh").val(data.blh);
                $("#txtbrxz").val(data.brxz);
                $("#txtbrxzqc").val("[" + data.brxz + "]" + data.brxzmc);
                $("#txtbrxzbh").val(data.brxzbh);
                DisableSysBasicInfo(true); //true:Cờ này lấy từ đăng ký không cần thẻ hoặc từ chi tiết trả về khi tra cứu theo số thẻ, false:Khi tải trang，có thể nhậpkh

            }
        }
    });
}

//Cửa sổ bật số hóa đơn
function ShowInvoicePanel() {
    $.modalOpen({
        id: "InvoiceNoPanel",
        title: "Chọn số hóa đơn",
        url: "/BusinessManage/OutPatient/ChooseInvoice",
        width: "300px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].checkFPH();//điểm cửa sổXác nhận的CallbackHàm
        }
    });
}

//Loại bệnh nhânLớp nổi
$("#txtbrxz").newtouchFloatingSelector({
    height: 200,
    width: 300,
    filter: function (keyword) {
        if (!keyword) {
            return null;
        }
        //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
        var resultObjArr = new Array();
        $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
            if ((val.brxzmc && val.brxzmc.indexOf(keyword) >= 0)
                || (val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)) {
                resultObjArr.push(val);
            }
        });
        return resultObjArr;

    },
    caption: "Tính chất bệnh nhân",
    colModel: [
        { label: 'Số hiệu', name: 'brxzbh', widthratio: 25 },
        { label: 'Mã', name: 'brxz', widthratio: 25 },
        { label: 'Tên', name: 'brxzmc', widthratio: 25 },
        { label: 'Phiên âm', name: 'py', widthratio: 25 }
    ],
    itemdbclickhandler: function ($thistr) {
        $("#brxz").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
        $("#brxz").val($thistr.find('td:eq(2)').html());
        return;
    }
});

//Loại đăng ký khám
var currentghlx;
//Lớp nổi lịch trực đăng ký khám
$("#txtksys").newtouchFloatingSelector({
    height: 200,
    width: 350,
    id: 'regSchedule',
    url: '/BusinessManage/OutPatient/GetRegScheduleList',
    ajaxmethod: 'POST',
    ajaxreqdata: function () {
        var reqData = {};
        reqData.keyword = $("#txtksys").val();
        reqData.mzlx = $("#sel_mzlx").val();
        return reqData;
    },
    caption: "Lịch trực đăng ký khám",
    colModel: [
        { label: 'ghlx', name: 'ghlx', hidden: true, widthratio: 0 },
        { label: 'ghlxbh', name: 'ghlxbh', hidden: true, widthratio: 0 },
        { label: 'Loại đăng ký khám', name: 'sfxmmc', widthratio: 25 },
        { label: 'Khoa đăng ký khám', name: 'ksmc', widthratio: 25 },
        { label: 'Bác sĩ', name: 'rymc', widthratio: 25 },
        {
            label: 'Bệnh chuyên khoa', name: 'ghzbmc', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue == "null" ? "" : cellvalue;
            }
        },
        { label: 'ks', name: 'ks', hidden: true, widthratio: 0 },
        { label: 'ksbh', name: 'ksbh', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("#sel_mzlx").val() == "1" || $("#sel_mzlx").val() == "2")//Khám ngoại trú/Cấp cứu  Chỉ hiển thị khoa
        {
            $("#txtksys").val($thistr.find('td:eq(3)').html());
        }
        if ($("#sel_mzlx").val() == "3")//Khám chuyên gia  Hiển thị khoa và bác sĩ
        {
            $("#txtksys").val($thistr.find('td:eq(3)').html() + "/" + $thistr.find('td:eq(4)').html());
        }
        $("#txtghlx").val($thistr.find('td:eq(0)').html());
        $("#txtghlxbh").val($thistr.find('td:eq(1)').html());
        $("#txtks").val($thistr.find('td:eq(6)').html());
        $("#txtksbh").val($thistr.find('td:eq(7)').html());
        currentghlx = $thistr.attr('data-ghlx');
        RegisterInfo();


        return;
    }

});

//Tải thông tin đang đăng ký khám 
function RegisterInfo() {
    if ($("#txtksys").val() != null & $("#ksys").val() != "") {


        //đưatabLưu dữ liệu cần trong trang vào một đối tượng
        list = new Object();
        list.ghlx = $("#txtksys").val();
        list.brxz = $("#txtbrxz").val();
        list.brxzbh = $("#txtbrxzbh").val();
        list.dbxm = $("#txtdbxm").val();
        //Lấy phí đăng ký khám Phí khám chữa bệnh Phí thẻ từ Phí làm thẻ
        var feesList = GHFees(true);
        list.ghf = feesList.ghf;
        list.zlf = feesList.zlf;
        list.ckf = feesList.ckf;
        list.gbf = feesList.gbf;
        list.totalfees = feesList.totalfees;

        //Đang đăng ký khám
        registeringListNew([list]);
        $("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#txtysk").val(list.totalfees);  //Khoản phải thu
    }
}

//Lấy phí đăng ký khám
function GHFees(flag) {
    var isZzjm = false;
    var isCkf = false;
    var isGbf = false;
    if ($("#chk_zzjm").is(":checked")) {
        isZzjm = true;
    }
    if ($("#chk_ckf").is(":checked")) {
        isCkf = true;
    }
    if ($("#chk_gbf").is(":checked")) {
        isGbf = true;
    }
    //if ($("#chk_ckf").is(":checked") & $("#chk_gbf").is(":checked"))
    //{
    //    return false;
    //}
    feesList = new Object();
    $.ajax({
        url: "/BusinessManage/OutPatient/GetOutpatientFees",
        dataType: "json",
        data: { ghlx: currentghlx, isZzjm: isZzjm, isCkf: isCkf, isGbf: isGbf },
        type: "POST",
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
    //Nhấn riêngCheckBox
    if (!flag) {
        //var rowData = $('#registeringList').jqGrid('getRowData', 1);
        $('#registeringList').jqGrid('setCell', 1, 'ckf', feesList.ckf);
        $('#registeringList').jqGrid('setCell', 1, 'gbf', feesList.gbf);
        $('#registeringList').jqGrid('setCell', 1, 'totalfees', feesList.totalfees);
        //Cập nhậtGridđồng thời với chi phí，Cập nhật tổng số tiền
        $("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#txtysk").val(feesList.totalfees);

    }
    return feesList;
}


//Hủy số khám
function BackNum() {
    var currtab = $("#linkclosedReg").parent();
    if (currtab[0].className == "active") {
        var count = $("#closedRegList").getGridParam('records');
        if (count >= 1) {
            var ghnm = $("#closedRegList").jqGridRowValue().ghnm;
            if (ghnm == null) {
                $.modalAlert("Vui lòng chọn một thông tin đăng ký khám cần hủy số", 'warning');
                return;
            }
            else {
                if ($("#closedRegList").jqGridRowValue().isth == "已退号") {
                    $.modalAlert("Đăng ký khám này đã hủy", 'warning');
                    return;
                }
                var isReturnAll = false;
                if ($("#isReturnAll").is(":checked")) {
                    isReturnAll = true;
                }
                if (window.confirm("Có hủy số khám hay không？")) {
                    var ghnm = $("#closedRegList").jqGridRowValue().ghnm;
                    $.ajax({
                        url: "/BusinessManage/OutPatient/BackNum",
                        dataType: "json",
                        data: { patid: $("#txtpatid").val(), ghnm: ghnm, isReturnAll: isReturnAll },
                        type: "POST",
                        async: false,
                        success: function (data) {
                            $.loading(false);
                            $.modalAlert("Hủy số khám thành công", 'success');
                            $("#linkclosedReg").trigger("click");
                        }
                    });

                } else {
                    return false;
                }
            }
        }
        else {
            $.modalAlert("Không cóHủy số khám的Thông tin", 'warning');
        }
    }
    else {
        $.modalAlert("Vui lòng chọn mục cần hủy số trong danh sách đã quyết toán", 'warning');
        return;
    }
}

//Lưu
function Save() {
    //Trước tiên xem hiện có trong danh sách đang đăng ký khám hay không，Rồi xem danh sách có dữ liệu hay không，Nếu có Mặc định dòng đầu tiên，Nếu không có thì nhắc
    var currtab = $("#linkregistering").parent();
    if (currtab[0].className == "active") {
        //LấyjqGridsố dòng của
        var count = $("#registeringList").getGridParam('records');
        if (count > 0) {
            RegList = new Object();
            RegList.fph = $("#txtfph").val();
            RegList.patid = $("#txtpatid").val();
            RegList.kh = $("#txtkh").val();
            RegList.brxz = $("#txtbrxz").val();
            RegList.ghlx = $("#txtghlx").val();
            //Kiểm tra dữ liệu
            if (!CheckItem(RegList)) {
                return false;
            }
            //var isJS;
            var ysk = 0;
            var ssk = 0;
            ysk = $("#txtysk").val();
            ssk = $("#txtssk").val();
            zl = $("#txtzl").val();
            if (parseFloat(ssk) < parseFloat(ysk)) {
                $.modalAlert("Số thực thu không được nhỏ hơn số phải thu", 'warning');
                return;
            } else {
                var zl = parseFloat($("#txtssk").val()) - parseFloat($("#txtysk").val());
                //Biểu mẫu quyết toán
                showBillSuccessDialog(ysk, ssk, 0, zl.toFixed(2));

            }

        } else {
            $.modalAlert("Hiện không có thông tin đang đăng ký khám", 'warning');
            return;
        }
    }
}
//Bật cửa sổ xác nhận số tiền
function showBillSuccessDialog(ysk, ssk, srce, zl) {
    var url = "/OutHospital/Bill/BillSuccessDialog?yingshoukuan=" + ysk
        + "&ssk=" + ssk
        + "&srce=" + srce
        + "&zhaoling=" + zl;
    $.modalOpen({
        id: "FormShowBillSuccessDialog",
        title: "Xác nhận quyết toán",
        url: url,
        width: "320px",
        height: "260px",
        callBack: function (iframeId) {
            $.modalClose("FormShowBillSuccessDialog");   //Đóng biểu mẫu số tiền quyết toán
            $.loading(true, "Đang yêu cầu dữ liệu...");
            SaveMethod(ysk, ssk);
        }
    });
}
//Thực hiện lưu cụ thể
function SaveMethod(ysk, ssk) {
    var mzlx = $.currentWindow().$("#sel_mzlx").val(); //Cờ khám cấp cứu ngoại trú 1：Khám ngoại trú 2：Cấp cứu
    var jzxh = 0;  //Số thứ tự khám bệnh
    $.ajax({
        url: "/BusinessManage/OutPatient/GetJZXH",
        dataType: "json",
        data: { ghlx: $("#txtghlx").val(), ks: $("#txtks").val(), ys: 0, ghzb: 0 },
        type: "POST",
        async: false,
        success: function (data) {
            if (data == null || data == "") {
                $.modalAlert("LấySố thứ tự khám bệnhThất bại", 'warning');
                return;
            }
            else {
                jzxh = data;
            }
        }
    });
    var zzjm = "";
    if ($("#chk_zzjm").is(":checked")) {
        zzjm = "true";
    } else {
        zzjm = "false";
    }
    var ghf = ghf = $("#registeringList").jqGridRowValue().ghf;
    var zlf = zlf = $("#registeringList").jqGridRowValue().zlf;

    var ckf = 0
    if ($("#registeringList").jqGridRowValue().ckf != "") {
        ckf = $("#registeringList").jqGridRowValue().ckf;
    }
    var gbf = 0;
    if ($("#registeringList").jqGridRowValue().gbf != "") {
        gbf = $("#registeringList").jqGridRowValue().gbf;
    }
    //Lưu
    $.ajax({
        url: "/BusinessManage/OutPatient/Save",
        dataType: "json",
        //ghxz:0 Đăng ký khám thông thường； jzbz:"1",ghzt:"1" Trường không hợp lệ； ghzbbh:0,ghzb:0 Tạm thời mặc định là0；ghly: "0" Nguồn đăng ký khám là cửa sổ；  gsrdh:"",zdicd10:"",zdmc:"" Tạm thời mặc định trống
        data: { patid: $("#txtpatid").val(), brxz: $("#txtbrxz").val(), brxzbh: $("#txtbrxzbh").val(), ghly: "0", mjzbz: mzlx, ksbh: $("#txtksbh").val(), ks: $("#txtks").val(), rybh: $("#txtrybh").val(), ys: 0, ghlxbh: $("#txtghlxbh").val(), ghlx: $("#txtghlx").val(), ghzbbh: 0, ghzb: 0, jzxh: jzxh, fzbz: $("txtfzbz").val(), jzbz: "1", ghzt: "1", ghxz: 0, dbxm: $("#txtdb").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: ghf, zzjm: zzjm, kh: $("#txtkh").val(), ysk: ysk, ssk: ssk, fph: $("#txtfph").val() },
        type: "POST",
        async: false,
        success: function (data) {
            if (data.state == "success") {
                $.modalAlert("Lưu thành công", 'success');

                //Tải lại số hóa đơn
                initFPH();
                Clear();
                $.loading(false);
            }
            else if (data.state == "error") {
                $.modalAlert("Lưu thất bại", 'error');
                $.loading(false);
                return;
            }
        }
    });

}

//Kiểm tra khi lưu
function CheckItem(RegList) {
    if (RegList.fph == "") {
        $.modalAlert("Số hóa đơn không được trống", 'warning');
        return;
    }
    if (RegList.patid == "") {
        $.modalAlert("Vui lòng thêm thông tin bệnh nhân trước", 'warning');
        return false;
    }
    if (RegList.kh == "") {
        $.modalAlert("Vui lòng nhập số thẻ trước", 'warning');
        return false;
    }
    if (RegList.brxz == "") {
        $.modalAlert("Vui lòng chọn tính chất bệnh nhân", 'warning');
        return false;
    }
    if (RegList.ghlx == "") {
        $.modalAlert("Vui lòng chọn loại đăng ký khám", 'warning');
        return false;
    }
    return true;
}
//Xóa
function Clear() {

    //Xóa trốngGrid
    $('#registeringList').jqGrid("clearGridData");
    $('#notSettledRegList').jqGrid("clearGridData");
    $('#closedRegList').jqGrid("clearGridData");

    //Xóa trốngĐiều khiển
    newtouch_globalevent_f4(event);

    //Bật điều khiển
    EnabledSysBasicInfo();
}

//Vô hiệu hóa
function DisableSysBasicInfo(flag) {
    //true:Cờ này lấy từ đăng ký không cần thẻ hoặc từ chi tiết trả về khi tra cứu theo số thẻ, false:Khi tải trang，có thể nhậpkh
    if (flag) {
        $("#txtkh").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
    } else {
        $("#txtbrxzqc").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238");
    }
}

//Bật
function EnabledSysBasicInfo() {
    $("#txtkh").removeAttr("disabled").removeAttr("style");;
}


//Hủy bỏ Cần求变更：Loại bỏChưaQuyết toánNội dung update by caishanshan 20161229
//function Abandon() {
//    var currtab = $("#linknotSettledReg").parent();
//    if (currtab[0].className == "active") {
//        var count = $("#notSettledRegList").getGridParam('records');
//        if (count >= 1) {
//            var ghnm = $("#notSettledRegList").jqGridRowValue().ghnm;
//            if (ghnm == null) {
//                $.modalAlert("Vui lòng chọn một dòngCầncầnHủy bỏ的Đăng ký khámThông tin", 'warning');
//                return;
//            }
//            else
//            {
//                if (window.confirm("Xác nhậncầnHủy bỏkhông？")) {
//                    $.ajax({
//                        url: "/BusinessManage/OutPatient/SaveRegAbandRecord",
//                        dataType: "json",
//                        data: { ghnm: ghnm },
//                        type: "POST",
//                        async: false,
//                        success: function (data) {
//                            if (data.state == "success") {
//                                $.loading(false);
//                                $.modalAlert("Hủy bỏThành công", 'success');
//                                $("#linknotSettledReg").trigger("click");
//                            } else {
//                                $.modalAlert("Thất bại", 'error');
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
//            $.modalAlert("Không cóHủy bỏ的Thông tin", 'warning');
//            return;
//        }
//    }
//    else
//    {
//        $.modalAlert("Vui lòngTrongChưaQuyết toán列表里ChọncầnHủy bỏ的项", 'warning');
//        return;
//    }
//}




