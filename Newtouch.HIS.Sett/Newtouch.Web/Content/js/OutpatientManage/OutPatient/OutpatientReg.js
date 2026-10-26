$(function () {

    //Tải lại khi thay đổi kích thước cửa sổ trình duyệtjqGridchiều rộng của
    $(window).resize(function () {
        initLayout("MyTabGrid");
    });

    //Tải trang，Vô hiệu hóa điều khiển
    DisableSysBasicInfo(false);
    //khi vào trang，TảiKhám ngoại trúLoại
    $("#sel_mzlx").select2({ minimumResultsForSearch: -1 }); //Loại bỏĐiều kiện lọc
    initFPH();
    initLastFPHInfo();
    initZffs();

    //Phím Enter
    $("#txtkh").keypress(function (e) {
        if (e.keyCode === 13) {
            LoadPatientInfo($("#txtkh").val(), true);
            if ($("#linkregistering").parent()[0].className === "active") {
                $("#linkregistering").trigger("click");
            }
            if ($("#linkclosedReg").parent()[0].className == "active") {
                $("#linkclosedReg").trigger("click");
            }
        }
    });
    $("#txtssk").keypress(function (e) {
        //先Xóa trống
        $("#labzl").text('');
        if (e.keyCode == 13) {
            if ($("#txtssk").val() == "" || isNaN($("#txtssk").val())) {
                $.modalAlert("Vui lòng nhập số thực thu và chỉ được nhập số", 'warning');
                return false;
            }
            var zl = parseFloat($("#txtssk").val()) - parseFloat($("#labysk").text());
            $("#labzl").text(zl.toFixed(2)); //Giữ hai chữ số thập phân
        }
    });
});

//Tìm kiếm
$("#btn_search").click(function () {
    LoadPatientInfo($("#txtkh").val(), true);
    if ($("#linkregistering").parent()[0].className == "active") {
        $("#linkregistering").trigger("click");
    }
    if ($("#linkclosedReg").parent()[0].className == "active") {
        $("#linkclosedReg").trigger("click");
    }
})

//Quẹt thẻ
function SK() {
    $.modalAlert("Chức năng đang được phát triển", 'warning');
    return false;
}

//khi vào trang，Tải số hóa đơn
function initFPH() {
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetInvoice?r=" + Math.random(),
        dataType: "text",
        async: false,
        success: function (data) {
            $("#txtfph").val(data);
        }
    });
}

//khi vào trang，Tải thông tin hóa đơn trước
function initLastFPHInfo() {
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetLastSettleInfo?r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {
            //Tra cứuok
            if (data != null && data.length > 0) {
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
var kh = "";
function LoadPatientInfo(kh, isAutoOpenFreeCardReg) {
    if (!kh) {
        return;
    }
    $("#txtkh").val(kh.toUpperCase());
    $.najax({
        url: "/OutpatientManage/OutpatientReg/LoadPatientInfo?kh=" + kh + "&r=" + Math.random(),
        dataType: "json",
        async: true,
        alertbierror: false,
        success: function (data) {
            $("#labxm").text(data.data.xm);
            $("#labxb").text($.getGender(data.data.xb));
            $("#txtfzbz").val(data.data.fzbz);
            if (data.data.fzbz == 0) {
                $("#txtcfz").val("[0]初诊");
            } else {
                $("#txtcfz").val("[1]复诊");
            }
            $("#txtpatid").val(data.data.patid);
            $("#txtcsny").val(data.data.csny);
            $("#txtdb").val(data.data.db);
            $("#txtdbzd").val(data.data.dbzd);
            $("#txtblh").val(data.data.blh);
            $("#txtbrxz").val(data.data.brxz);
            $("#txtbrxzqc").val("[" + data.data.brxz + "]" + data.data.brxzmc);
            $("#txtbrxzbh").val(data.data.brxzbh);
            DisableSysBasicInfo(true); //true:Cờ này lấy từ đăng ký không cần thẻ hoặc từ chi tiết trả về khi tra cứu theo số thẻ, false:Khi tải trang，có thể nhậpkh
        },
        errorCallback: function (data) {
            newtouch_globalevent_f4();
            $("#txtkh").val(kh.toUpperCase());
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

//Cửa sổ bật số hóa đơn
function ShowInvoicePanel() {
    $.modalOpen({
        id: "InvoiceNoPanel",
        title: "Chọn số hóa đơn",
        url: "/OutpatientManage/OutpatientReg/ChooseInvoice",
        width: "300px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].checkFPH();//điểm cửa sổXác nhận的CallbackHàm
        }
    });
}

//Lớp nổi tính chất bệnh nhân
$("#txtbrxzqc").newtouchFloatingSelector({
    height: 200,
    width: 300,
    filter: function (keyword) {
        if (!keyword) {
            return null;
        }
        //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
        var resultObjArr = new Array();
        $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
            if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
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
        checkBrxz($thistr.attr('data-brxz'));
        $("#txtbrxzqc").val("[" + $thistr.attr('data-brxz') + "]" + $thistr.attr('data-brxzmc'));
        $("#txtbrxz").val($thistr.attr('data-brxz'));
        $("#txtbrxzbh").val($thistr.attr('data-brxzbh'));
        return;
    }
});

//checkTính chất bệnh nhân
function checkBrxz(updateBrxz) {
    var kh = $("#txtkh").val();
    $.najax({
        url: "/OutpatientManage/OutpatientReg/CheckBrxz?kh=" + kh + "&updateBrxz=" + updateBrxz + "&r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {

        },
        errorCallback: function (data) {   //Trạng thái为errorKhi自动弹出错误Thông tin，NàyPhương thức为弹出错误Thông tinSau đóCần做的事情

        }
    })
}

//Khám ngoại trúLoại发生改变Xóa trốngKhoa/Bác sĩ
function sel_mzlxChange() {
    $("#txtksys").val('');
}

//Loại đăng ký khám
var currentghlx;
//Lớp nổi lịch trực đăng ký khám
$("#txtksys").newtouchBatchFloatingSelector({
    height: 200,
    width: 350,
    clickautotrigger: true,
    id: 'regSchedule',
    url: '/OutpatientManage/OutpatientReg/GetRegScheduleList',
    ajaxmethod: 'POST',
    ajaxreqdata: function () {
        var reqData = {};
        reqData.keyword = $("#txtksys").val();
        reqData.mzlx = $("#sel_mzlx").val();
        return reqData;
    },
    caption: "Lịch trực đăng ký khám",
    colModel: [
        { label: 'gh', name: 'gh', hidden: true, widthratio: 0 },
        { label: 'ghlx', name: 'ghlx', hidden: true, widthratio: 0 },
        { label: 'ghlxbh', name: 'ghlxbh', hidden: true, widthratio: 0 },
        { label: 'Loại đăng ký khám', name: 'sfxmmc', widthratio: 25 },
        { label: 'Khoa đăng ký khám', name: 'ksmc', widthratio: 25 },
        { label: 'Bác sĩ', name: 'rymc', widthratio: 25 },
        {
            label: 'Bệnh chuyên khoa', name: 'ghzbmc', widthratio: 25, formatter: function (cellvalue) {
                return cellvalue === "null" ? "" : cellvalue;
            }
        },
        { label: 'ks', name: 'ks', hidden: true, widthratio: 0 },
        { label: 'ksbh', name: 'ksbh', hidden: true, widthratio: 0 }
    ],
    itemdbclickhandler: function ($thistr) {
        if ($("#sel_mzlx").val() === "1" || $("#sel_mzlx").val() === "2")//Khám ngoại trú/Cấp cứu  Chỉ hiển thị khoa
        {
            $("#txtksys").val($thistr.attr('data-ksmc'));
        }
        if ($("#sel_mzlx").val() === "3")//Khám chuyên gia  Hiển thị khoa và bác sĩ
        {
            $("#txtksys").val($thistr.attr('data-ksmc') + "/" + $thistr.attr('data-rymc'));
        }
        $("#txtghlx").val($thistr.attr('data-ghlx'));
        $("#txtghlxbh").val($thistr.attr('data-ghlxbh'));
        $("#txtks").val($thistr.attr('data-ks'));
        $("#txtksbh").val($thistr.attr('data-ksbh'));
        $("#txtrygh").val($thistr.attr('data-gh'));
        currentghlx = $thistr.attr('data-ghlx');
        RegisterInfo();
        return;
    }
});

//Tải thông tin đang đăng ký khám 
function RegisterInfo() {
    //var rowIDs = $("#registeringList").getDataIDs();
    //for (var i = 0; i < rowIDs.length; i++) {
    //    var getRow = $("#registeringList").getRowData(rowIDs[i]);
    //    if ($("#txtksys").val() == getRow.ghlx) {
    //        $.modalAlert("Cùng mộtSố不能挂多次", 'warning');
    //        $("#txtksys").val("");
    //        return;
    //    }

    //}
    $('#registeringList').jqGrid("clearGridData");
    if ($("#txtksys").val() != null && $("#ksys").val() !== "") {

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
        //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#labysk").text(list.totalfees);  //Khoản phải thu
        $("#labysk").addClass('moneybg');
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
    feesList = new Object();
    $.najax({
        url: "/OutpatientManage/OutpatientReg/GetOutpatientFees",
        dataType: "json",
        data: { ghlx: currentghlx, isZzjm: isZzjm, isCkf: isCkf, isGbf: isGbf },
        type: "POST",
        async: false,
        success: function (data) {
            feesList.ghf = data.ghfPrice;
            feesList.zlf = data.zlfPrice;
            if (!isCkf) {
                feesList.ckf = " ";
            } else {
                feesList.ckf = data.ckfPrice;
            }
            if (!isGbf) {
                feesList.gbf = " ";
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
        //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
        $("#labysk").text(feesList.totalfees);

    }
    return feesList;
}


//Hủy số khám
function BackNum() {
    var currtab = $("#linkclosedReg").parent();
    if (currtab[0].className === "active") {
        if ($("#closedRegList").getDataIDs().length == 0) {
            $.modalAlert("Không cóHủy số khám的Đăng ký khámThông tin", 'warning');
            return;
        }
        var seleRowid = jQuery("#closedRegList").jqGrid("getGridParam", "selrow");
        if (!(seleRowid)) {
            $.modalAlert("Vui lòng chọn một thông tin đăng ký khám cần hủy số", 'warning');
            return;
        }
        var seleRowData = $("#closedRegList").jqGrid('getRowData', seleRowid);
        if (seleRowData.isth === "已退号") {
            $.modalAlert("Đăng ký khám này đã hủy", 'warning');
            return;
        }
        seleRowData.isReturnCKGBFee = (seleRowData.isReturnCKGBFee === 'Yes');
        $.modalConfirm("Có hủy số khám hay không？", function (flag) {
            if (flag) {
                $.najax({
                    url: "/OutpatientManage/OutpatientReg/BackNum",
                    dataType: "json",
                    data: {
                        patid: $("#txtpatid").val(), ghnm: seleRowData.ghnm, isReturnCKGBFee: seleRowData.isReturnCKGBFee
                    },
                    type: "POST",
                    async: false,
                    alertbierror: false,
                    success: function (data) {
                        $.loading(false);
                        if (data === true) {
                            $.modalAlert("Hủy số khám thành công", 'success');
                            $("#linkclosedReg").trigger("click");
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
        }, false); //false:modalConfirm不Đóngdialog
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
    if (currtab[0].className === "active") {
        //LấyjqGridsố dòng của
        var count = $("#registeringList").getGridParam('records');
        if (count > 0) {
            var regList = new Object();
            regList.fph = $("#txtfph").val();
            regList.patid = $("#txtpatid").val();
            regList.kh = $("#txtkh").val();
            regList.brxz = $("#txtbrxz").val();
            regList.ghlx = $("#txtghlx").val();
            var ysk = 0;
            ysk = $("#labysk").text();
            regList.ssk = $("#txtssk").val();
            //Kiểm tra dữ liệu
            if (!CheckItem(regList)) {
                return false;
            }
            if (parseFloat(regList.ssk) < parseFloat(ysk)) {
                $.modalAlert("Số thực thu không được nhỏ hơn số phải thu", 'warning');
                return false;
            } else {
                var zl = parseFloat($("#txtssk").val()) - parseFloat($("#labysk").text());
                //Book(ysk, regList.ssk);
                //Biểu mẫu quyết toán
                showBillSuccessDialog(ysk, regList.ssk, 0, zl.toFixed(2));
            }

        } else {
            $.modalAlert("Hiện không có thông tin đang đăng ký khám", 'warning');
            return false;
        }
    } else {
        $.modalAlert("Vui lòng到Đang đăng ký khám列表进DòngQuyết toán", 'warning');
    }
}

//下Lưu tạm单
function Book(ysk, ssk) {
    var selRowRowId = $("#registeringList").jqGrid("getGridParam", "selrow");
    if (!selRowRowId && $("#registeringList").getGridParam('records') === 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("Vui lòng chọn bản ghi đăng ký khám", 'warning');
    }

    var selRow = $("#registeringList").jqGrid('getRowData', selRowRowId);

    var jdata = {
        patid: $("#txtpatid").val(),
        brxz: $("#txtbrxz").val(),
        brxzbh: $("#txtbrxzbh").val(),
        ghly: "0",
        mjzbz: $.currentWindow().$("#sel_mzlx").val(),////Cờ khám cấp cứu ngoại trú 1：Khám ngoại trú 2：Cấp cứu
        ksbh: $("#txtksbh").val(),
        ks: $("#txtks").val(),
        ys: $("#txtrygh").val(),
        ghlxbh: $("#txtghlxbh").val(),
        ghlx: $("#txtghlx").val(),
        ghzbbh: 0,
        ghzb: 0,
        fzbz: $("#txtfzbz").val(),
        jzbz: "1",
        ghzt: "1",
        ghxz: 0,
        dbxm: $("#txtdb").val(),
        gsrdh: "",
        zdicd10: "",
        zdmc: "",
        ghf: selRow.ghf,
        zlf: selRow.zlf,
        ckf: selRow.ckf,
        gbf: selRow.gbf,
        zzjm: $("#chk_zzjm").is(":checked") ? true : false,
        kh: $("#txtkh").val(),
        ssk: ssk,
        fph: $("#txtfph").val(),
        isCkf: $("#chk_ckf").is(":checked") ? true : false,
        isGbf: $("#chk_gbf").is(":checked") ? true : false,
        xm: $("#labxm").text(),
        xb: $.getGenderCode($("#labxb").text()),
        csny: $("#txtcsny").val(),
        blh: $("#txtblh").val(),
        updateBrxz: $("#txtbrxz").val()
    };
    $.loading(true, "Đang yêu cầu dữ liệu...");
    //Lưu
    $.najax({
        url: "/OutpatientManage/OutpatientReg/Book",
        dataType: "text",
        data: jdata,
        type: "POST",
        async: false,
        success: function (data) {
            $.loading(false, "Đang yêu cầu dữ liệu...");
            $("#hidOrderId").val(parseInt(data));
        },
        errorCallback: function (data) {
            $.loading(false, "Đang yêu cầu dữ liệu...");
        },
        complete: function () {
            $.loading(false, "Đang yêu cầu dữ liệu...");
        }
    });
}

//Bật cửa sổ xác nhận số tiền
function showBillSuccessDialog(ysk, ssk, srce, zl) {
    var url = "/HospitalizationManage/Settlement/SettSuccessDialog?yingshoukuan=" + ysk
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
    var isZzjm = "";
    if ($("#chk_zzjm").is(":checked")) {
        isZzjm = true;
    } else {
        isZzjm = false;
    }
    var isCkf = false;
    if ($("#chk_ckf").is(":checked")) {
        isCkf = true;
    }
    var isGbf = false;
    if ($("#chk_gbf").is(":checked")) {
        isGbf = true;
    }

    var selRowRowId = $("#registeringList").jqGrid("getGridParam", "selrow");
    if (!selRowRowId && $("#registeringList").getGridParam('records') == 1) {
        selRowRowId = 1;
    }

    if (!selRowRowId) {
        $.modalAlert("Vui lòng chọn bản ghi đăng ký khám", 'warning');
    }

    var selRow = $("#registeringList").jqGrid('getRowData', selRowRowId);

    var ghf = selRow.ghf;
    var zlf = selRow.zlf;
    var ckf = selRow.ckf;
    var gbf = selRow.gbf;
    var xb = $.getGender($("#labxb").text());
    var orderId = 0;
    if ($("#hidOrderId").val() !== "") {
        orderId = parseInt($("#hidOrderId").val());
    }

    //Lưu
    $.najax({
        url: "/OutpatientManage/OutpatientReg/Save",
        dataType: "json",
        //ghxz:0 Đăng ký khám thông thường； jzbz:"1",ghzt:"1" Trường không hợp lệ； ghzbbh:0,ghzb:0 Tạm thời mặc định là0；ghly: "0" Nguồn đăng ký khám là cửa sổ；  gsrdh:"",zdicd10:"",zdmc:"" Tạm thời mặc định trống
        data: { OrderId: orderId, patid: $("#txtpatid").val(), brxz: $("#txtbrxz").val(), brxzbh: $("#txtbrxzbh").val(), ghly: "0", mjzbz: mzlx, ksbh: $("#txtksbh").val(), ks: $("#txtks").val(), ys: $("#txtrygh").val(), ghlxbh: $("#txtghlxbh").val(), ghlx: $("#txtghlx").val(), ghzbbh: 0, ghzb: 0, fzbz: $("#txtfzbz").val(), jzbz: "1", ghzt: "1", ghxz: 0, dbxm: $("#txtdb").val(), gsrdh: "", zdicd10: "", zdmc: "", ghf: ghf, zlf: zlf, ckf: ckf, gbf: gbf, zzjm: isZzjm, kh: $("#txtkh").val(), ssk: ssk, fph: $("#txtfph").val(), isCkf: isCkf, isGbf: isGbf, xm: $("#labxm").text(), xb: xb, csny: $("#txtcsny").val(), blh: $("#txtblh").val(), updateBrxz: $("#txtbrxz").val() },
        type: "POST",
        async: false,
        success: function (data) {
            $.modalAlert("Lưu thành công", 'success');

            //Tải lại số hóa đơn
            initFPH();
            newtouch_globalevent_f4();
            $("#labysk").text = String.Empty;
            $("#labzl").text = String.Empty;
            //Làm mớimục trước次Quyết toánThông tin
            $("#lastSettleInfoList").jqGrid('setGridParam', {}).trigger('reloadGrid');
            $.loading(false);
        },
        errorCallback: function (data) {
            $.loading(false);
            $("#labysk").text = String.Empty;
            $("#labzl").text = String.Empty;
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
    if (RegList.ssk == "" || isNaN(RegList.ssk)) {
        $.modalAlert("Vui lòng nhập số thực thu và chỉ được nhập số", 'warning');
        return false;
    }

    return true;
}

//Phím tắt：Xóa
function newtouch_event_f4() {
    //Xóa trốngGrid
    $('#registeringList').jqGrid("clearGridData");
    $('#closedRegList').jqGrid("clearGridData");
    $('#labxm').text("");
    $('#labxb').text("");
    //$("input[name=radio_xb]").removeAttr("checked");
    //$("input[name=radio_xb]").parent().removeClass('active').css('background-color', '#fff');

    //Bật điều khiển
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

//Vô hiệu hóa
function DisableSysBasicInfo(flag) {
    //true:Cờ này lấy từ đăng ký không cần thẻ hoặc từ chi tiết trả về khi tra cứu theo số thẻ, false:Khi tải trang，có thể nhậpkh
    if (flag) {
        $("#txtkh").attr("disabled", "disabled").css("background-color", "#f1f4f6");
        $("#txtbrxzqc").removeAttr("disabled").removeAttr("style");
        $('input[name="xb"]').each(
            function () {
                $(this).parent().attr("disabled", "disabled");
            }
        );
    } else {
        $("#txtbrxzqc").attr("disabled", "disabled").css("background-color", "#f1f4f6");
    }
    //$("#txtysk").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
    //$("#txtzl").attr("disabled", "disabled").css("background-color", "rgb(238, 238, 238)");
}

//Bật
function EnabledSysBasicInfo() {
    $("#txtkh").removeAttr("disabled").removeAttr("style");
}






