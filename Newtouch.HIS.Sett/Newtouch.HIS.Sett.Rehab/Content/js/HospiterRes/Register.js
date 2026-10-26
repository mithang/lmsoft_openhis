var keyValue = $.request("keyValue");
$(function () {
    if (!!keyValue) {
        $.ajax({
            url: "/PatientManage/HospiterRes/GetFormJson?T=" + new Date(),
            data: { "keyValue": keyValue },
            dataType: 'json',
            async: false,
            success: function (rep) {
                $("#form1").formSerialize(rep);
                if (rep["jkjl"]) {
                    $("#jkjlmc").attr("data-label", rep["jkjl"]);
                }
                if (rep["doctor"]) {
                    $("#doctormc").attr("data-label", rep["doctor"]);
                }
                if (rep["brxz"]) {
                    $("#brxzmc").attr("data-label", rep["brxz"]);
                }
                if (rep["ryzd1"]) {
                    $("#zdmc1").attr("data-label", rep["ryzd1"]);
                }
                if (rep["ryzd2"]) {
                    $("#zdmc2").attr("data-label", rep["ryzd2"]);
                }
                if (rep["ryzd3"]) {
                    $("#zdmc3").attr("data-label", rep["ryzd3"]);
                }
                if (rep["ks"]) {
                    $("#ksmc").attr("data-label", rep["ks"]);
                }
                if (rep["bq"]) {
                    $("#bqmc").attr("data-label", rep["bq"]);
                }
                if (rep["mz"]) {
                    $("#mzmc").attr("data-label", rep["mz"]);
                }
                if (rep["gj"]) {
                    $("#gjmc").attr("data-label", rep["gj"]);
                }
                if (rep["zjlx"]) {
                    var type = rep["zjlx"].trim();
                    switch (type) {
                        case "0": type = "Chứng minh nhân dân"; break;
                        case "1": type = "Hộ chiếu"; break;
                        case "2": type = "Chứng nhận sĩ quan"; break;
                        default: break;

                    }
                    $("#zjlx").html(type + "<span>*</span>");
                }
                $(".toolbar").hide();
                $("#noCardRes").attr("disabled", "disabled");
            }
        });
    }
    $("#ryrq").val($.getDate());
    $("#bje").val("");

    $("#kh").keydown(function (e) {
        if (e.keyCode == 13) {
            $.ajax({
                url: "/PatientManage/HospiterRes/CheckCardState",
                data: { "kh": $("#kh").val() },
                dataType: 'json',
                async: false,
                success: function (rep) {
                    if (rep == "0") {
                        //Đăng ký miễn thẻ khi không có thẻ 
                        btn_NocardRes();
                    } else {
                        //Có thẻ thì hiển thị thông tin cơ bản
                        $("#form1").formSerialize(rep);
                        if (rep["jkjl"]) {
                            $("#jkjlmc").attr("data-label", rep["jkjl"]);
                        }
                        if (rep["doctor"]) {
                            $("#doctormc").attr("data-label", rep["doctor"]);
                        }
                        if (rep["brxz"]) {
                            $("#brxzmc").attr("data-label", rep["brxz"]);
                        }
                        if (rep["ryzd1"]) {
                            $("#zdmc1").attr("data-label", rep["ryzd1"]);
                        }
                        if (rep["ryzd2"]) {
                            $("#zdmc2").attr("data-label", rep["ryzd2"]);
                        }
                        if (rep["ryzd3"]) {
                            $("#zdmc3").attr("data-label", rep["ryzd3"]);
                        }
                        if (rep["ks"]) {
                            $("#ksmc").attr("data-label", rep["ks"]);
                        }
                        if (rep["bq"]) {
                            $("#bqmc").attr("data-label", rep["bq"]);
                        }
                        if (rep["mz"]) {
                            $("#mzmc").attr("data-label", rep["mz"]);
                        }
                        if (rep["gj"]) {
                            $("#gjmc").attr("data-label", rep["gj"]);
                        }
                        if (rep["zjlx"]) {
                            var type = rep["zjlx"].trim();
                            switch (type) {
                                case "0": type = "Chứng minh nhân dân"; break;
                                case "1": type = "Hộ chiếu"; break;
                                case "2": type = "Chứng nhận sĩ quan"; break;
                                default: break;

                            }
                            $("#zjlx").html(type + "<span>*</span>");
                        }
                        //Vô hiệu hóa thông tin cơ bản bệnh nhân của hệ thống
                        DisableSysBasicInfo();
                    }
                }
            });
        }
    });
    initControl();
});

function btn_NocardRes() {
    $.modalOpen({
        id: "Form",
        title: "Đăng ký bệnh nhân",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function initControl() {
    ///Huấn luyện viên sức khỏe
    $("#jkjlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysStaffList, function (idx, val) {
                if (((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "")
                    && val.ryCode == "1") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Huấn luyện viên sức khỏe",
        colModel: [
            {
                label: 'Mã', name: 'ry', widthratio: 25
            },
            {
                label: 'Tên', name: 'rymc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#jkjlmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#jkjlmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///Bác sĩ
    $("#doctormc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();

            $.each(top.window.newtouchclients.sysStaffList, function (idx, val) {
                if (((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "")
                     && val.ryCode == "2") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Bác sĩ",
        colModel: [
            {
                label: 'Mã', name: 'ry', widthratio: 25
            },
            {
                label: 'Tên', name: 'rymc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#doctormc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#doctormc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///Chính sách hoàn trả
    $("#brxzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Tính chất bệnh nhân",
        colModel: [
            {
                label: 'Mã số', name: 'brxzbh', widthratio: 25
            },
            {
                label: 'Mã', name: 'brxz', widthratio: 25
            },
            {
                label: 'Tên', name: 'brxzmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#brxzmc").val($thistr.find('td:eq(2)').html());
            return;
        }
    });
    ///Chẩn đoán khi nhập viện zdmc1
    $("#zdmc1").newtouchFloatingSelector(
        {
            height: 200,
            width: 300,
            url: "/PatientManage/HospiterRes/GetryzdSelect",
            ajaxparameters: function () {
                return "ryzd=" + $("#zdmc1").val();
            },
            caption: "Chẩn đoán khi nhập viện",
            colModel: [
           {
               label: 'Mã số', name: 'zdbh', widthratio: 25
           },
           {
               label: 'icd10', name: 'icd10', widthratio: 25
           },
           {
               label: 'Tên', name: 'zdmc', widthratio: 25
           },
           { label: 'Mã nội bộ', name: 'zdnm', widthratio: 25 }
            ],
            itemdbclickhandler: function (data) {
                $("#zdmc1").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                $("#zdmc1").val(data.find('td:eq(2)').html());
                return;
            }
        });
    ///Chẩn đoán khi nhập viện zdmc2
    $("#zdmc2").newtouchFloatingSelector(
           {
               height: 200,
               width: 300,
               url: "/PatientManage/HospiterRes/GetryzdSelect",
               ajaxparameters: function () {
                   return "ryzd=" + $("#zdmc2").val();
               },
               caption: "Chẩn đoán khi nhập viện",
               colModel: [
                   {
                       label: 'Mã số', name: 'zdbh', widthratio: 25
                   },
                   {
                       label: 'icd10', name: 'icd10', widthratio: 25
                   },
                   {
                       label: 'Tên', name: 'zdmc', widthratio: 25
                   },
                   { label: 'Mã nội bộ', name: 'zdnm', widthratio: 25 }
               ],
               itemdbclickhandler: function (data) {
                   $("#zdmc2").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                   $("#zdmc2").val(data.find('td:eq(2)').html());
                   return;
               }
           });
    ///Chẩn đoán khi nhập viện zdmc3
    $("#zdmc3").newtouchFloatingSelector(
              {
                  height: 200,
                  width: 300,
                  url: "/PatientManage/HospiterRes/GetryzdSelect",
                  ajaxparameters: function () {
                      return "ryzd=" + $("#zdmc3").val();
                  },
                  caption: "Chẩn đoán khi nhập viện",
                  colModel: [
                       {
                           label: 'Mã số', name: 'zdbh', widthratio: 25
                       },
                       {
                           label: 'icd10', name: 'icd10', widthratio: 25
                       },
                       {
                           label: 'Tên', name: 'zdmc', widthratio: 25
                       },
                       { label: 'Mã nội bộ', name: 'zdnm', widthratio: 25 }
                  ],
                  itemdbclickhandler: function (data) {
                      $("#zdmc3").attr("data-label", data.find("td:eq(0)").html() + "-" + data.find("td:eq(3)").html());
                      $("#zdmc3").val(data.find('td:eq(2)').html());
                      return;
                  }
              });
    ///Khoa
    $("#ksmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysDepartList, function (idx, val) {
                if ((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Khoa",
        colModel: [
            {
                label: 'Mã số', name: 'ksbh', widthratio: 25
            },
            {
                label: 'Mã', name: 'ks', widthratio: 25
            },
            {
                label: 'Tên', name: 'ksmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#ksmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#ksmc").val($thistr.find('td:eq(2)').html());
            return;
        },
    });
    ///Khu bệnh
    $("#bqmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysPatiAreaList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Khu bệnh",
        colModel: [
            {
                label: 'Mã số', name: 'bqbh', widthratio: 25
            },
            {
                label: 'Mã', name: 'bq', widthratio: 25
            },
            {
                label: 'Tên', name: 'bqmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#bqmc").attr("data-label", $thistr.find("td:eq(0)").html() + "-" + $thistr.find("td:eq(1)").html());
            $("#bqmc").val($thistr.find('td:eq(2)').html());
            return;
        },
    });
    ///Dân tộc
    $("#mzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysNationList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Dân tộc",
        colModel: [
            {
                label: 'Mã', name: 'mz', widthratio: 25
            },
            {
                label: 'Tên', name: 'mzmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#mzmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#mzmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///Quốc tịch
    $("#gjmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        filter: function (keyword) {
            if (!keyword) {
                return null;
            }
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.newtouchclients.sysNationalityList, function (idx, val) {
                if ((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0) || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Quốc tịch",
        colModel: [
            {
                label: 'Mã', name: 'gj', widthratio: 25
            },
            {
                label: 'Tên', name: 'gjmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#gjmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#gjmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
}

function btn_Save() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["jkjl"] = $("#jkjlmc").attr("data-label");
        data["doctor"] = $("#doctormc").attr("data-label");
        data["brxz"] = $("#brxzmc").attr("data-label");
        var zd1 = $("#zdmc1").attr("data-label");
        data["ryzd1"] = null;
        if (zd1) {
            data["ryzd1"] = (zd1.split('-', 1))[0];
        }
        var zd2 = $("#zdmc2").attr("data-label");
        data["ryzd2"] = null;
        if (zd2) {
            data["ryzd2"] = (zd2.split('-', 1))[0];
        }
        var zd3 = $("#zdmc3").attr("data-label");
        data["ryzd3"] = null;
        if (zd3) {
            data["ryzd3"] = (zd3.split('-', 1))[0];
        }
        data["ks"] = $("#ksmc").attr("data-label");
        data["bq"] = $("#bqmc").attr("data-label");
        data["mz"] = $("#mzmc").attr("data-label");
        data["gj"] = $("#gjmc").attr("data-label");
        $.submitForm({
            url: "/PatientManage/HospiterRes/SaveSysHosBasicInfo",
            loading: "Đang gửi dữ liệu...",
            param: data,
            successwithtipmsg: false,
            success: function (data) {
                if (data.message == "操作成功") {
                    $.modalAlert("Đăng ký nhập viện thành công! Số nhập viện là:" + data.data, 'warning');
                    ClearAll();
                }
                $.loading(false);
            }
        });
    }
}

function PrinyZYInfo() {
    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ, không thể in thông tin nhập viện", 'error');
        return;
    } else {
        $.modalAlert("Chức năng đang được phát triển", 'warning');
        return;
    }
    var data = $("#form1").formSerialize();
    data["ryzd1"] = $("#zdmc1").attr("data-label");
    data["ryzd2"] = $("#zdmc2").attr("data-label");
    data["ryzd3"] = $("#zdmc3").attr("data-label");
    $.ajax({
        url: "/PatientManage/HospiterRes/PrinyZYInfo",
        data: data,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message = "打印成功！") {
                $.modalAlert(data.message, "success");
            }
        }
    });
}

function PrintWDInfo() {

    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ, không thể in vòng tay", 'error');
        return;
    } else {
        $.modalAlert("Chức năng đang được phát triển", 'warning');
        return;
    }
    $.ajax({
        url: "/PatientManage/HospiterRes/PrintWDInfo",
        data: $("#form1").formSerialize(),
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message = "打印成功！") {
                $.modalAlert(data.message, "success");
            }
        }
    });
}

function Cancel() {
    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ, không thể in thông tin nhập viện", 'error');
        return;
    }
    $.ajax({
        url: "/PatientManage/HospiterRes/CancelAdmission?zyh=" + $("#zyh").val(),
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.message == "取消入院成功！") {
                AbledSysBasicInfo();
                newtouch_globalevent_f4(event);
            }
            $.modalAlert(data.message, "success");
        }
    });
}

function DisableSysBasicInfo() {
    $("#kh").attr("disabled", "disabled");
    $("#zyh").attr("disabled", "disabled");
    $("#xm").attr("disabled", "disabled");
    $("#zjh").attr("disabled", "disabled");
    $("#csny").attr("disabled", "disabled");
    $('input[name="xb"]').each(
        function () {
            $(this).parent().attr("disabled", "disabled");
        }
        );
    $("#nl").attr("disabled", "disabled");
    $("#blh").attr("disabled", "disabled");
    $("#dh").attr("disabled", "disabled");
    $("#dy").attr("disabled", "disabled");
    $("#hf").attr("disabled", "disabled");
}

function AbledSysBasicInfo() {
    $("#kh").removeAttr("disabled");
    $("#zyh").removeAttr("disabled");
    $("#xm").removeAttr("disabled");
    $("#zjh").removeAttr("disabled");
    $("#csny").removeAttr("disabled");
    $('input[name="xb"]').each(
        function () {
            // $(this).parent().removeAttr("disabled");
            $(this).parent().removeClass("active");
        }
        );
    $("#nl").removeAttr("disabled");
    $("#blh").removeAttr("disabled");
    $("#dh").removeAttr("disabled");
    $("#dy").removeAttr("disabled");
    $("#hf").removeAttr("disabled");
}

function ClearAll() {
    AbledSysBasicInfo();
    newtouch_globalevent_f4();
}

function newtouch_globalevent_f6() {
    PrintWDInfo();
}

function newtouch_globalevent_f7() {
    PrinyZYInfo();
}

function newtouch_globalevent_f8() {
    btn_Save();
}

function newtouch_globalevent_f9() {
    Cancel();
}

function newtouch_event_f4() {
    AbledSysBasicInfo();
    $("#patid").val("");
}

function checkNotNull() {
    var patid = $("#patid").val();
    if (!patid) {
        $.modalAlert("Thông tin cơ bản bệnh nhân không tồn tại, không thể lưu", 'warning');
        return false;
    }

    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            brxzmc: { required: true },
            ksmc: { required: true },
            bqmc: { required: true },
            zy: { required: true },
            mzmc: { required: true },
            gjmc: { required: true },
            bje: { required: true },
            lxr: { required: true },
            lxrgx: { required: true },
            lxrdh: { isMobile: true, required: true },
            lxrdz: { required: true },
            kh: { required: true }
        },
        messages: {
            brxzmc: { required: "Phải nhập chính sách hoàn trả" },
            ksmc: { required: "Phải nhập khoa" },
            bqmc: { required: "Phải nhập khu bệnh" },
            mzmc: { required: "Phải nhập dân tộc" },
            gjmc: { required: "Phải nhập quốc tịch" },
            zy: { required: "Phải nhập nghề nghiệp" },
            bje: { required: "Phải nhập mức cảnh báo" },
            lxr: { required: "Phải nhập người liên hệ khẩn cấp" },
            lxrgx: { required: "Phải nhập quan hệ người liên hệ khẩn cấp" },
            lxrdh: { isMobile: "Định dạng điện thoại di động không đúng", required: "Phải nhập điện thoại di động của người liên hệ khẩn cấp" },
            lxrdz: { required: "Phải nhập địa chỉ người liên hệ khẩn cấp" },
            kh: { required: "Phải nhập số thẻ" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    return true;
}
