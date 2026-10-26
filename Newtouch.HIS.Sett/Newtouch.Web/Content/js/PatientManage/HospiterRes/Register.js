
var keyValue = $.request("keyValue");
var zyhpz = "";
var boolsearchwithkh = $("#searchwithkh").val() === "ON";
var patInfoObj = {};
var ybnhlx = "";
var IsSuccess = true;//Dấu hiệu bất thường,falseDấu hiệu có bất thường
$(function () {
    $("#ryrq").val($.getTime());
    $("#bje").val("");
    zyhpz = $("#zyhpz").val();
    $("#zyh,#kh").keydown(function (e) {
        var ev = document.all ? window.event : e;
        var v_id = $(ev.target).attr('id');
        if ((e.keyCode === 13 && boolsearchwithkh) || (v_id === "zyh" && e.keyCode === 13 && $.isEmptyObject(patInfoObj))) {
            var kh = $("#kh").val();
            $.ajax({
                url: "/PatientManage/HospiterRes/CheckCardState",
                data: { "kh": kh, "zyh": $("#zyh").val() },
                dataType: 'json',
                async: false,
                success: function (rep) {
                    if (rep && rep.data) {
                        ClearAll();
                        CallbackPatientQuery(rep.data);
                        return;
                        //if (rep === 0) {
                        //    //Không có thẻ thì làm đăng ký không cần thẻ 
                        //    btn_NocardRes();
                        //} else {
                        //    if (rep !== undefined) {
                        //        fillInfo(rep);
                        //    }
                        //}
                    }

                    //Không có thẻ thì làm đăng ký không cần thẻ 
                    btn_NocardRes();
                }
            });
        }
    });


    initControl();
    $("#lxrdz2").keyupEnterEvent(function () {
        btn_Save();
    });

    if (!boolsearchwithkh) {
        //Số hồ sơ bệnh án，Họ tên，Phiên âmTừ khóaTìm kiếm
        $('#kh').newtouchBatchFloatingSelector({
            width: 500,
            height: 200,
            caption: "Chọn bệnh nhân",
            url: "/PatientManage/HospiterRes/PatSearchInfo",
            ajaxparameters: function ($thisinput) {
                var keyword = $thisinput.val().trim();
                return "keyword=" + keyword;
            },
            itemdbclickhandler: function ($thistr, $thisinput) {
                CallbackPatientQuery($thistr.attr('data-blh'));
            },
            colModel: [
                { label: 'Khóa chính', name: 'patid', hidden: true },
                { label: 'Số hồ sơ bệnh án', name: 'blh', width: 130, align: 'left' },
                { label: 'Họ tên', name: 'xm', width: 120, align: 'left' },
                { label: 'Tháng năm sinh', name: 'csny', hidden: true, width: 100, align: 'left' },
                {
                    label: 'Giới tính', name: 'xb', width: 70, align: 'left', formatter: function (cellvalue) {
                        return $.getGender(cellvalue);
                    }
                },
                {
                    label: 'Tuổi', name: 'nl', width: 100, align: 'left', formatter: function (cellvalue, a, b) {
                        return getAgeFromBirthTime({ begin: b.csny }).text;
                    }
                },
                { label: 'brly', name: 'brly', align: 'left', hidden: true },
                { label: 'zjh', name: 'zjh', align: 'left', hidden: true },
                { label: 'kh', name: 'kh', align: 'left', hidden: true },
                { label: 'phone', name: 'phone', align: 'left', hidden: true },
                { label: 'dy', name: 'dy', align: 'left', hidden: true },
                { label: 'zjlx', name: 'zjlx', align: 'left', hidden: true },
                { label: 'sycs', name: 'sycs', align: 'left', hidden: true },
                { label: 'dybh', name: 'dybh', align: 'left', hidden: true },
                { label: 'lxr', name: 'lxr', align: 'left', hidden: true },
                { label: 'lxrgx', name: 'lxrgx', align: 'left', hidden: true },
                { label: 'lxrdh', name: 'lxrdh', align: 'left', hidden: true },
            ]
        });
    }
});

function fillInfo(rep) {
    //有卡显示Thông tin cơ bản
    $("#form1").formSerialize(rep);
    getcitycode(rep);
    if (!rep.zyh) {
        //是MớiĐăng ký
        if (rep.hf || rep.hf == 0) {
            $("#hy").val(rep.hf).trigger('change');
        }
        else {
            $("#hy").val('').trigger('change');
        }
    }
    if ((rep.zyh !== null && rep.hasOwnProperty("zyh")) || zyhpz == "ON") {
        $("#zyh").attr("disabled", "disabled").css("background-color", "#f1f4f6");
        $('#zyh').attr("placeholder", "DoHệ thống自动生成");
        $('#zyhlog').html("");
    }
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
    if (rep["mzCode"]) {
        $("#mzmc").attr("data-label", rep["mzCode"]);
    }
    if (rep["gjCode"]) {
        $("#gjmc").attr("data-label", rep["gjCode"]);   //会：显示无，值有
    }
    if (rep["zjlx"]) {
        var type = rep["zjlx"].trim();
        switch (type) {
            case "1": type = "Chứng minh nhân dân："; break;
            case "2": type = "护照："; break;
            case "3": type = "军官证："; break;
            default:
                type = "其他："; break;

        }
        $("#zjlx").html(type);
    }

    var patModel = rep;
    $("#xb").html($.getGender(patModel.xb));
    $("#csny").html((patModel.csny && patModel.csny.length >= 10 ? patModel.csny.substring(0, 10) : ""));
    $("#zjh").html(patModel.zjh);
    $("#jsr").html(patModel.jsr);
    $("#xm").html(patModel.xm);
    $("#nlshow").html(getAgeFromBirthTime({ begin: patModel.csny }).text);
    $("#phone").html(patModel.phone);
    //$("#dy").html(patModel.dybh === 1 ? '外地' : (patModel.dybh === 0 ? '本地' : 'Chưa知'));
    $("#kh").val(boolsearchwithkh ? patModel.kh : patModel.blh);
    $("#blh").html(boolsearchwithkh ? patModel.blh : patModel.kh);
    $("#hiddenzjlx").val(patModel.zjlx);
    //TảiThông tin bệnh nhân
    patInfoObj.xm = patModel.xm; //Họ tên
    patInfoObj.xb = patModel.xb; //Giới tính
    patInfoObj.csny = patModel.csny;
    patInfoObj.patid = patModel.patid; //Bệnh nhânMã nội bộ
    patInfoObj.blh = patModel.blh; //Số hồ sơ bệnh án
    patInfoObj.zjh = patModel.zjh; //Số giấy tờ
    patInfoObj.jsr = patModel.jsr; //Giới thiệu人
    patInfoObj.brxz = patModel.brxz;//Tính chất bệnh nhân
    patInfoObj.zjlx = patModel.zjlx; //Loại giấy tờ
    patInfoObj.csny = patModel.csny;//Tháng năm sinh
    //Vô hiệu hóaHệ thốngBệnh nhânThông tin cơ bản
    DisableSysBasicInfo();
}
function loadPatiInfo(rep) {
    getcitycode(rep);
    $("#gms").val(rep.gms);
    $("#cs_dz").val(rep.cs_dz);
    $("#xian_dz").val(rep.xian_dz);
    $("#hu_dz").val(rep.hu_dz);
    $("#jjlxr_dz").val(rep.jjlxr_dz);
}

function btn_NocardRes(t) {
    if (t != null && t === 1) {
        localStorage.removeItem("patientform");
    }
    $.modalOpen({
        id: "Form",
        title: "Thẻ một cửa办理",
        url: "/PatientManage/HospiterRes/PatientBasic?T=" + new Date() + "&readbz=" + t,
        width: "1000px",
        height: "450px",
        callBack: function (iframeId) {
            //top.frames[iframeId].submitForm();
            $.currentWindow(iframeId).AcceptClick(function (obj) {
                if (obj.CardType === '2') {
                    if (IsOpenSfzYbJz !== "ON") {
                        $.modalAlert("ChưaBậtBảo hiểm y tế可按Chứng minh nhân dânKhám bệnh动态Cấu hình!", 'warning');
                        $.modalClose("Form");
                        return;
                    }
                    //Chứng minh nhân dân调用Bảo hiểm y tế接口
                    //...
                }
                GetQueryFphAjax(obj);
                $.modalClose("Form");
            });
        }
    });
}

function initControl() {
    ///Huấn luyện viên sức khỏe
    $("#jkjlmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysStaffDutyList, function (idx, val) {
                if (((val.StaffPY && val.StaffPY.indexOf(keyword.toLowerCase()) >= 0)
                    || (val.StaffName && val.StaffName.indexOf(keyword.toLowerCase()) >= 0)
                    || keyword.trim() == "")
                    && val.DutyCode == "RehabDoctor") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Huấn luyện viên sức khỏe",
        colModel: [
            {
                label: 'Mã', name: 'StaffGh', widthratio: 25
            },
            {
                label: 'Tên', name: 'StaffName', widthratio: 25
            },
            { label: 'Phiên âm', name: 'StaffPY', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#jkjlmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#jkjlmc").val($thistr.find('td:eq(1)').html());
            return;
        }
    });
    ///Bác sĩ
    $("#doctormc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();

            $.each(top.window.clients.sysStaffDutyList, function (idx, val) {
                if (((val.StaffPY && val.StaffPY.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)
                    || (val.StaffName && val.StaffName.indexOf(keyword.toLowerCase()) >= 0)
                    || keyword.trim() == "")
                    && val.DutyCode == "Doctor") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Bác sĩ",
        colModel: [
            {
                label: 'Mã nhân viên', name: 'StaffGh', widthratio: 25
            },
            {
                label: 'Tên', name: 'StaffName', widthratio: 25
            },
            { label: 'Phiên âm', name: 'StaffPY', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#doctormc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#doctormc").val($thistr.find('td:eq(1)').html());
            return;
        }
    });
    ///Chính sách thanh toán BHYT
    $("#brxzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)
                    || (val.brxzmc && val.brxzmc.indexOf(keyword) >= 0)
                    || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Tính chất bệnh nhân",
        colModel: [
            {
                label: 'Số hiệu', name: 'brxzbh', widthratio: 25
            },
            {
                label: 'Mã', name: 'brxz', widthratio: 25
            },
            {
                label: 'Tên', name: 'brxzmc', widthratio: 25
            },
            { label: 'Phiên âm', name: 'py', widthratio: 25 },
            { label: 'brxzlb', name: 'brxzlb', widthratio: 25,hidden:true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#brxzmc").attr("data-brxzmc", $thistr.find("td:eq(2)").html())
                .attr("data-label", $thistr.find("td:eq(1)").html()).attr("data-brxzlb", $thistr.find("td:eq(4)").html());
            $("#brxzmc").val($thistr.find('td:eq(2)').html());
            if (openYbSett && medicalInsurance == "guian" && $("#brxzmc").attr("data-label") === "1") {
                ybnhlx = "yb";
            }
            if (openXnhSett && medicalInsurance == "guian" && $("#brxzmc").attr("data-label") === "8") {
                ybnhlx = "nh";
            }
            $('#zdmc1').trigger('newtouchBatchFloatingSelector');
            $('#zdmc2').trigger('newtouchBatchFloatingSelector');
            $('#zdmc3').trigger('newtouchBatchFloatingSelector');
            //hss 2019.7.19 Cùng mộtBệnh nhânLưuTrong多个交易LoạiKhi，TheoChính sách thanh toán BHYTTảiSố thẻ
            $.ajax({
                type: "POST",
                url: "/PatientManage/HospiterRes/GetkhInfoBybrxz",
                data: { patid: $('#patid').val(), brxz: $("#brxzmc").attr("data-label") },
                dataType: "json",
                async: false,
                success: function (r) {
                    if (!!r.data && !!r.data.CardNo) {
                        if (!boolsearchwithkh) {
                            //Đã bật tìm kiếm theo số hồ sơ bệnh án
                            $("#blh").html(r.data.CardNo);

                        } else {
                            $("#kh").val(r.data.CardNo);
                        }
                    }
                }
            });
            return;
        }
    });
    ///Chẩn đoán nhập viện zdmc1
    $("#zdmc1").newtouchBatchFloatingSelector({
        height: 200,
        width: 600,
        clickautotrigger: true,
        url: "/PatientManage/HospiterRes/GetryzdSelect",
        ajaxparameters: function () {
            return "ryzd=" + $("#zdmc1").val() + "&ybnhlx=" + ybnhlx;
        },
        caption: "Chẩn đoán nhập viện",
        colModel: [
            { label: 'Tên', name: 'zdmc', widthratio: 60 },
            { label: 'Phiên âm', name: 'py', widthratio: 20 },
            { label: 'icd10', name: 'icd10', widthratio: 20 },
            { label: 'Số hiệu', name: 'zdbh', hidden: true },
            { label: 'Mã nội bộ', name: 'zdnm', hidden: true }
        ],
        itemdbclickhandler: function (data) {
            $("#zdmc1").attr("data-label", data.attr('data-zdbh') + "-" + data.attr('data-zdnm'));
            $("#zdmc1").val(data.attr('data-zdmc'));
            return;
        }
    });
    ///Chẩn đoán nhập viện zdmc2
    $("#zdmc2").newtouchBatchFloatingSelector(
        {
            height: 200,
            width: 600,
            url: "/PatientManage/HospiterRes/GetryzdSelect",
            clickautotrigger: true,
            ajaxparameters: function () {
                return "ryzd=" + $("#zdmc2").val() + "&ybnhlx=" + ybnhlx;
            },
            caption: "Chẩn đoán nhập viện",
            colModel: [
                { label: 'Tên', name: 'zdmc', widthratio: 60 },
                { label: 'Phiên âm', name: 'py', widthratio: 20 },
                { label: 'icd10', name: 'icd10', widthratio: 20 },
                { label: 'Số hiệu', name: 'zdbh', hidden: true },
                { label: 'Mã nội bộ', name: 'zdnm', hidden: true }
            ],
            itemdbclickhandler: function (data) {
                $("#zdmc2").attr("data-label", data.attr('data-zdbh') + "-" + data.attr('data-zdnm'));
                $("#zdmc2").val(data.attr('data-zdmc'));
                return;
            }
        });
    ///Chẩn đoán nhập viện zdmc3
    $("#zdmc3").newtouchBatchFloatingSelector({
        height: 200,
        width: 600,
        clickautotrigger: true,
        url: "/PatientManage/HospiterRes/GetryzdSelect",
        ajaxparameters: function () {
            return "ryzd=" + $("#zdmc3").val() + "&ybnhlx=" + ybnhlx;
        },
        caption: "Chẩn đoán nhập viện",
        colModel: [
            { label: 'Tên', name: 'zdmc', widthratio: 60 },
            { label: 'Phiên âm', name: 'py', widthratio: 20 },
            { label: 'icd10', name: 'icd10', widthratio: 20 },
            { label: 'Số hiệu', name: 'zdbh', hidden: true },
            { label: 'Mã nội bộ', name: 'zdnm', hidden: true }
        ],
        itemdbclickhandler: function (data) {
            $("#zdmc3").attr("data-label", data.attr('data-zdbh') + "-" + data.attr('data-zdnm'));
            $("#zdmc3").val(data.attr('data-zdmc'));
            return;
        }
    });
    ///Khoa
    $("#ksmc").newtouchBatchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysDepartList, function (idx, val) {
                if (((val.py && val.py.toLowerCase().indexOf(keyword) >= 0)
                    || (val.Name && val.Name.indexOf(keyword) >= 0)
                    || keyword.trim() == "") && val.mzzybz != '1' && val.Code != 'KS17') {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Khoa",
        colModel: [
            { label: 'Số hiệu', name: 'Code', widthratio: 30 },
            { label: 'Tên', name: 'Name', widthratio: 50 },
            { label: 'Phiên âm', name: 'py', hidden: true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#ksmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#ksmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///Khu bệnh
    $("#bqmc").newtouchBatchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            var ks = $("#ksmc").attr("data-label");
            var bqlist = top.window.clients.sysWardDeptRelation.filter((item) => item.ks == ks);
            $.each(bqlist, function (idx, val) {
                if ((val.bqpy && val.bqpy.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)
                    || (val.bqmc && val.bqmc.indexOf(keyword) >= 0)
                    || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Khu bệnh",
        colModel: [
            {
                label: 'Số hiệu', name: 'bq', widthratio: 25
            },
            {
                label: 'Tên', name: 'bqmc', widthratio: 50
            }
        ],
        itemdbclickhandler: function ($thistr) {
            $("#bqmc").attr("data-label", $thistr.find("td:eq(0)").html());
            $("#bqmc").val($thistr.find('td:eq(1)').html());
            return;
        },
    });
    ///Dân tộc
    $("#mzmc").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysNationList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)
                    || (val.mzmc && val.mzmc.indexOf(keyword) >= 0)
                    || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Dân tộc",
        colModel: [
            {
                label: 'Mã', name: 'mzCode', widthratio: 30
            },
            {
                label: 'Tên', name: 'mzmc', widthratio: 30
            },
            { label: 'Phiên âm', name: 'py', widthratio: 30 }
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
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysNationalityList, function (idx, val) {
                if ((val.py && val.py.indexOf(keyword.toLowerCase()) >= 0)
                    || (val.gjmc && val.gjmc.indexOf(keyword) >= 0)
                    || keyword.trim() == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Quốc tịch",
        colModel: [
            {
                label: 'Mã', name: 'gjCode', widthratio: 25
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
    //Nghề nghiệp
    $("#zy").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('Profession'), function () {
                $('#zy').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    //转出医院
    //$("#zcyy").newtouchBindSelect({
    //    datasource: function () {
    //		var resultObjArr = new Array();
    //		$.each($.itemDetails.getItems('zcyy'), function () {
    //			$('#zcyy').append('<option value="' + this.Code + '">' + this.Name + '</option>');
    //		});
    //		return resultObjArr;
    //	}
    //});
    //Quan hệ người liên hệ khẩn cấp
    $("#lxrgx").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('RelativeType'), function () {
                $('#lxrgx').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });

    //Quan hệ người liên hệ khẩn cấp
    $("#lxrgx2").newtouchBindSelect({
        datasource: function () {
            var resultObjArr2 = new Array(); $.each($.itemDetails.getItems('RelativeType'), function () {
                $('#lxrgx2').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr2;
        }
    });

    if (!boolsearchwithkh) {
        //Đã bật tìm kiếm theo số hồ sơ bệnh án
        $("#lbl_blh").html("Số thẻ：");
        $("#lbl_kh").html("Số hồ sơ bệnh án/Họ tên：");
    }

    //Chẩn đoán nhập viện bắt buộc
    if ($('#isDiagnosisRequired').val() == 'True') {
        $('#spanZyzd').html('*').addClass('rema');
    }

    //
    $('#rytj').bindSelect();
    //
    $('#hy').bindSelect();
}
var errorMsg = "";//Lưu thông tin lỗi
function btn_Save() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data = savecitycode(data);
        data["csny"] = patInfoObj.csny;
        data["jkjl"] = $("#jkjlmc").attr("data-label");
        data["doctor"] = $("#doctormc").attr("data-label");
        if ($("#brxzmc").val()) {
            data["brxz"] = $("#brxzmc").attr("data-label");
        }
        else {
            data["brxz"] = null;
        }
        if (!data["brxz"]) {
            $.modalAlert("Chính sách thanh toán BHYTBắt buộc", 'warning');
            return;
        }
        if ($("#rytj").val() == "3" && !$("#zcyy").val()) {
            $.modalAlert("Nếu là其他医院转入，Vui lòng chọn转出医院", 'warning');
            return;
        }
        var zd1 = $("#zdmc1").attr("data-label");
        data["ryzd1"] = null;
        if (zd1 && $("#zdmc1").val().trim() != "" && $("#zdmc1").val() != null) {
            data["ryzd1"] = (zd1.split('-', 1))[0];
        } else {
            data["ryzd1"] = null;
        }
        var zd2 = $("#zdmc2").attr("data-label");
        data["ryzd2"] = null;
        if (zd2 && $("#zdmc2").val().trim() != "" && $("#zdmc2").val() != null) {
            data["ryzd2"] = (zd2.split('-', 1))[0];
        } else {
            data["ryzd2"] = null;
        }
        var zd3 = $("#zdmc3").attr("data-label");
        data["ryzd3"] = null;
        if (zd3 && $("#zdmc3").val().trim() != "" && $("#zdmc3").val() != null) {
            data["ryzd3"] = (zd3.split('-', 1))[0];
        } else {
            data["ryzd3"] = null;
        }
        data["ks"] = $("#ksmc").attr("data-label");
        data["bq"] = $("#bqmc").attr("data-label");
        data["mzCode"] = $("#mzmc").attr("data-label");
        data["gjCode"] = $("#gjmc").attr("data-label");
        data["xm"] = $("#xm").html();
        data["blh"] = $("#blh").html();
        if (!boolsearchwithkh) {
            tempp = data["blh"];
            data["blh"] = data["kh"];
            data["kh"] = tempp;
        }
        data["xb"] = $.getGenderCode($("#xb").html());
        data["zjlx"] = $("#hiddenzjlx").val();
        data["zjh"] = $("#zjh").html();
        data["xnhgrbm"] = $("#xnhgrbm").val();
        data["xnhylzh"] = $("#xnhylzh").val();
        data["inpId"] = $("#inpId").val();

        data["bzbm"] = $("#sel_tsbbz").val();
        data["bzmc"] = $("#sel_tsbbz").find("option:selected").text();
        data["cardtype"] = $("#readCardCardType").val();
        data["CardTypeName"] = $("#readCardCardType option:selected").text();

        //Chẩn đoán nhập viện bắt buộc
        if ($('#isDiagnosisRequired').val() == 'True'
            && !!!data["ryzd1"]) {
            $.modalAlert("Chẩn đoán nhập việnBắt buộc phải nhập", 'warning');
            return;
        }


        var jylsh = "";//Lưu số giao dịch
        var jyyzm = "";//Lưu mã kiểm tra giao dịch

        if (openYbSett && $("#brxzmc").attr("data-brxzlb") === "1") //Bật BHYT,Tính chất BHYT
        {
            if (cqPatInfo.ybVer != "shanghaiV5") {
                if (cqPatInfo.ybVer == undefined) {
                    $.modalAlert("Chính sách thanh toán BHYT为【Bảo hiểm y tếBệnh nhân】，Vui lòng trướcĐọc thẻ！", 'warning');
                    return;
                }
            }
            if (!data.zyh) //(无Số nội trú)Đăng ký nội trú
            {
                if (isZYZT) {
                    $.modalAlert("NàyBệnh nhânTrongBảo hiểm y tếtrong尚ChưaXuất viện，不能办理Nhập viện！", 'warning');
                    return;
                }
            } else { //(有Số nội trú)Đăng ký nội trúSửa
                //LấyĐăng ký nội trú反馈Thông tin

            }
        }
        var jzlx = cqPatInfo.mdtrt_cert_type;
        data["jzlx"] = cqPatInfo.jzlx;//$('#readCardCardType').val();
        data["cardtype"] = cqPatInfo.jzlx;
        if (IsSuccess) {
            $.submitForm({
                url: "/PatientManage/HospiterRes/SaveSysHosBasicInfo",
                loading: "ĐangTrongGửiDữ liệu...",
                param: { vo: data, isybjy: openYbSett },
                successwithtipmsg: false,
                success: function (datareq) {
                    if (datareq.message === "添加成功") {

                        if (openYbSett && $("#brxzmc").attr("data-brxzlb") === "1") {

                            if (cqPatInfo.ybVer == "shanghaiV5") {
                                shanghaiV5ybrydj(datareq, data, jzlx);//trên海五期Bảo hiểm y tếĐăng ký nội trú SJ11
                            } else if (cqPatInfo.ybVer == "gjyb") {
                                gjybrydj(datareq, jzlx);//Gọi chương trình BHYT quốc gia2401 Đăng ký nội trú
                            }
                        }
                        if (!IsSuccess) {
                            $.najax({
                                url: "/PatientManage/HospiterRes/CancelAdmission?zyh=" + datareq.data.zyh,
                                dataType: 'json',
                                async: false,
                                success: function (data) {
                                    if (data.message == "取消入院成功！") {
                                        $.modalAlert(errorMsg, 'error');
                                    }
                                    $.modalAlert(errorMsg, "error");
                                }
                            });
                        } else {
                            $.modalAlert("Đăng ký nội trúThành công！Số nội trú là:" + datareq.data.zyh, 'success');
                        }

                    }
                    else if (datareq.message === "修改成功") {
                        $.ajax({
                            type: "POST",
                            url: "/PatientManage/HospiterRes/GetCQjzdjInfo",
                            data: { zyh: data.zyh },
                            dataType: "json",
                            cache: false,
                            async: false,
                            success: function (payoptype) {
                                if (!payoptype) {
                                    $.modalAlert("LấyNội trúKhám bệnhĐăng kýThất bại", 'error');
                                    return;
                                }
                                if (cqPatInfo.ybVer == "shanghaiV5") {
                                    shanghaiV5djcx(jzlx, $('#zyh').val());//trên海五期Bảo hiểm y tếĐăng ký nội trú撤销
                                    shanghaiV5ybrydj(datareq, data, jzlx);//trên海五期Bảo hiểm y tếĐăng ký nội trú SJ11

                                } else {
                                    gjybrydjxg(payoptype);//Gọi chương trình BHYT quốc gia2401 Đăng ký nội trú
                                }
                            }
                        });
                        if (!IsSuccess) {
                            errorMsg = "Bảo hiểm y tếĐăng ký nội trúSửaThất bại！" + errorMsg;
                        } else {
                            $.modalAlert("Số nội trú là:" + datareq.data.zyh + "，Sửa thành công", 'warning');
                        }
                    } else {
                        //Thất bại，重庆Bảo hiểm y tếĐăng ký红冲
                        if (openYbSett && $("#brxzmc").attr("data-brxzlb") === "1") {
                            $.najax({
                                type: "GET",
                                url: "/OutpatientManage/OutpatCharge/GetCQjzdjDataInfo?zymzh=" + datareq.data.zyh,
                                dataType: "json",
                                loadingtext: "Đang yêu cầuHISHủyĐăng ký nội trú，Vui lòng đợi…",
                                success: function (ajaxresp) {
                                    if (ajaxresp && ajaxresp.jylsh || cqPatInfo.ybVer == "shanghaiV5") {
                                        if (cqPatInfo.ybVer == "shanghaiV5") {
                                            shanghaiV5djcx(jzlx, $('#zyh').val());//trên海五期Bảo hiểm y tếĐăng ký nội trú撤销
                                        }
                                        else {
                                            var payoptype = { hisId: datareq.data.zyh, 'mdtrt_id': ajaxresp.jylsh, 'operatorId': curUserCode, "operatorName": curUserName, 'insuplc_admdvs': $("#xzqh").val(), 'psn_no': $("#rybh").val() };
                                            $.ajax({
                                                type: "POST",
                                                url: "http://127.0.0.1:33333/api/YiBao/HospitaUpMdtrtinfo_2404",
                                                dataType: "json",
                                                data: payoptype,
                                                async: false,
                                                success: function (data) {
                                                    var cqybjyDenySettleReturn = eval('(' + data + ')');
                                                    if (cqybjyDenySettleReturn.infcode == "0") {
                                                        //////Khám bệnhĐăng kýHủy，不Cầncần落地
                                                    } else {
                                                        $.modalAlert('Hủy đăng ký nội trú thất bại：' + cqybjyDenySettleReturn.err_msg, 'success');
                                                    }
                                                }
                                            });
                                        }
                                    }
                                }
                            });
                        }
                        $.modalAlert("Đăng ký nội trúLưu thất bại", 'warning');
                    }
                    ClearAll();
                    $.loading(false);
                }
            });
        } else
            $.modalAlert(errorMsg, 'error');
    }
}
function gjybrydjxg(payoptype) {
    payoptype.mdtrt_cert_type = cqPatInfo.mdtrt_cert_type;
    payoptype.orgId = orgId;
    payoptype.med_type = ($("#rytj").val() == "" ? "21" : $("#rytj").val());//($("#rytj").val() == "3" ? "22" : "21");
    payoptype.czlb = 1;
    $.ajax({
        type: "POST",
        url: "http://127.0.0.1:33333/api/YiBao/HospitaMdtrtinfo_2403",
        dataType: "json",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('(' + data + ')');
            if (medicalReg) {
                if (medicalReg.infcode == "0") {
                }
                else {
                    IsSuccess = false;
                    errorMsg = medicalReg.err_msg;
                }
            }
        }
    });
}
function shanghaiV5djcx(jzlx, zyh) {
    var payoptype = {
        cardtype: jzlx,
        carddata: jzlx == "3" ? cqPatInfo.ecToken : cqPatInfo.sbkh,
        hisId: zyh,//Số nội trú
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("#xzqh").val()
    };
    $.ajax({
        type: "POST",
        url: "http://127.0.0.1:33333/api/FifthPhaseYiBao/ybInterface_SJ21",
        dataType: "json",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('(' + data + ')');
            if (medicalReg) {
                if (medicalReg.xxfhm === "P001") {

                }
                else {
                    //$.modalAlert("Đăng ký khám ngoại trú BHYT thất bại：" + medicalReg.ErrorMsg, 'error');
                    IsSuccess = false;
                    errorMsg = "Bảo hiểm y tếĐăng ký nội trú撤销Thất bại：" + medicalReg.err_msg;
                }
            }
        }
    });
}
function gjybrydj(datareq, jzlx) {
    var payoptype = {
        mdtrt_cert_type: jzlx,
        mdtrt_cert_no: jzlx == "01" ? cqPatInfo.ecToken : (jzlx == "02" ? $("#sfzh").val() : patModel.kh),
        hisId: datareq.data.zyh,//Số nội trú
        med_type: ($("#rytj").val() == "" ? "21" : $("#rytj").val()),//21Thông thườngNội trú，22转入Nội trú
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("#xzqh").val(),
        orgId: orgId,
        psn_no: $("#rybh").val(),
        dise_codg: $("#sel_tsbbz").val(),
        dise_name: $("#sel_tsbbz").find("option:selected").text()
    };
    $.ajax({
        type: "POST",
        url: "http://127.0.0.1:33333/api/YiBao/HospitaMdtrtinfo_2401",
        dataType: "json",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('(' + data + ')');
            if (medicalReg) {
                if ((medicalReg.infcode == "0" || medicalReg.infcode == 0) && medicalReg.output.result.mdtrt_id) {
                    $.najax({
                        type: "POST",
                        data: {
                            patid: $("#patid").val(),
                            zymzh: datareq.data.zyh,
                            jylsh: medicalReg.output.result.mdtrt_id,
                            jytype: "2",
                            medicalInList: null
                        },
                        url: "/OutpatientManage/OutpatientReg/SaveCqybMedicalReg",
                        dataType: "json",
                        loading: false,
                        success: function (ajaxresp) {
                        }
                    });
                }
                else {
                    //$.modalAlert("Đăng ký khám ngoại trú BHYT thất bại：" + medicalReg.ErrorMsg, 'error');
                    IsSuccess = false;
                    errorMsg = "Bảo hiểm y tếĐăng ký nội trúThất bại：" + medicalReg.err_msg;
                }
            }
        }
    });
}
function shanghaiV5ybrydj(datareq, data, jzlx) {
    var payoptype = {
        cardtype: cqPatInfo.jzlx,
        carddata: cqPatInfo.carddata,//jzlx == "3" ? cqPatInfo.ecToken : cqPatInfo.sbkh,
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("#xzqh").val(),
        orgId: orgId,
        hisId: datareq.data.zyh//Số nội trú
    };
    $.ajax({
        type: "POST",
        url: "http://127.0.0.1:33333/api/FifthPhaseYiBao/ybInterface_SJ11",
        dataType: "json",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('(' + data + ')');
            if (medicalReg) {
                if (medicalReg.xxfhm === "P001") {
                    $.najax({
                        type: "POST",
                        data: {
                            patid: $("#patid").val(),
                            zymzh: datareq.data.zyh,
                            jylsh: medicalReg.xxnr.jzdyh,
                            jytype: "2",
                            medicalInList: null
                        },
                        url: "/OutpatientManage/OutpatientReg/SaveCqybMedicalReg",
                        dataType: "json",
                        loading: false,
                        success: function (ajaxresp) {
                        }
                    });
                }
                else {
                    IsSuccess = false;
                    errorMsg = "Dịch vụ BHYT【SJ11Đăng ký nội trúThất bại】：" + medicalReg.fhxx;
                }
            } else {
                IsSuccess = false;
                errorMsg = "Dịch vụ BHYT【SJ11Đăng ký nội trúChưa返回Thông tin】：" + medicalReg.fhxx;
            }
        }, error: function (request, error, ex) {
            success = false;
            errMsg = "Dịch vụ BHYT【SJ11】Đăng ký nội trú不可访问：[" + ex + "]";
        }
    });
}
function PrinyZYInfo() {
    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ，无法打印Nội trúThông tin", 'error');
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
            if (data.message = "打印Thành công！") {
                $.modalAlert(data.message, "success");
            }
        }
    });
}

function PrintWDInfo() {
    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ，无法打印腕带", 'error');
        return;
    } else {
        $.modalAlert("Chức năng đang được phát triển", 'warning');
        return;
    }
}

function Cancel() {
    debugger;
    if (!$("#patid").val()) {
        $.modalAlert("Thông tin cơ bản bệnh nhân chưa đầy đủ，无法打印HủyNhập viện", 'error');
        return;
    }

    var IsSuccess = true;//Dấu hiệu bất thường,falseDấu hiệu có bất thường
    var errorMsg = "";//Lưu thông tin lỗi
    var jylsh = "";//Lưu số giao dịch
    var jyyzm = "";//Lưu mã kiểm tra giao dịch

    if (openYbSett && $("#brxzmc").attr("data-brxzlb") === "1") //Bật BHYT,贵安Bảo hiểm y tế,Tính chất BHYT
    {
        $.najax({
            type: "GET",
            url: "/PatientManage/HospiterRes/CheckCancelAdmission?zyh=" + $("#zyh").val(),
            dataType: "json",
            async: false,
            loadingtext: "Đang yêu cầuHISHủy đăng ký nội trú，Vui lòng đợi…",
            success: function () {
                $.najax({
                    type: "GET",
                    url: "/OutpatientManage/OutpatCharge/GetCQjzdjDataInfo?zymzh=" + $('#zyh').val(),
                    dataType: "json",
                    async: false,
                    loadingtext: "Đang yêu cầuHISHủy đăng ký nội trú，Vui lòng đợi…",
                    success: function (ajaxresp) {
                        if (ajaxresp && (ajaxresp.jylsh || cqPatInfo.ybVer == "shanghaiV5")) {
                            if (cqPatInfo.ybVer == "shanghaiV5") {
                                var jzlx = $('#readCardCardType').val();
                                shanghaiV5djcx(jzlx, $('#zyh').val());//trên海五期Bảo hiểm y tếĐăng ký nội trú撤销
                            } else {
                                var payoptype = { hisId: $('#zyh').val(), 'mdtrt_id': ajaxresp.jylsh, 'operatorId': curUserCode, "operatorName": curUserName, 'insuplc_admdvs': $("#xzqh").val(), 'psn_no': $("#rybh").val() };

                                $.ajax({
                                    type: "POST",
                                    url: "http://127.0.0.1:33333/api/YiBao/HospitaUpMdtrtinfo_2404",
                                    dataType: "json",
                                    data: payoptype,
                                    async: false,
                                    success: function (data) {
                                        var cqybjyDenySettleReturn = eval('(' + data + ')');
                                        if (cqybjyDenySettleReturn.infcode == "0") {
                                        } else {
                                            $.modalAlert('Hủy đăng ký nội trú thất bại：' + cqybjyDenySettleReturn.err_msg, 'error');
                                            IsSuccess = false;
                                        }
                                    }
                                });
                            }
                        }
                    }
                });
            }
        });


    }
    if (IsSuccess) {
        $.najax({
            url: "/PatientManage/HospiterRes/CancelAdmission?zyh=" + $("#zyh").val(),
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.message == "取消入院成功！") {
                    if (openYbSett && medicalInsurance == "guian" && $("#brxzmc").attr("data-brxzlb") === "1") {
                        var ybReq = {
                            astr_jylsh: jylsh,  //Thông tin BHYT trả về
                            astr_jyyzm: jyyzm  //Thông tin BHYT trả về
                        }
                        var ybjySettReturn;// = $.guianyibao.YibaoConfirm(ybReq);//贵安Bảo hiểm y tếXác nhậnGửi
                        $.ajax({
                            url: "http://127.0.0.1:12345/api/YiBao/YibaoConfirm",
                            data: ybReq,
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                ybjySettReturn = eval('(' + data + ')');
                            }
                        });
                        if (!(ybjySettReturn.Code == 0)) {
                            $.modalAlert("Bảo hiểm y tếHủyĐăng ký nội trú，流水Số【" + jylsh + "】GửiThất bại，Vui lòng前往“不Xác nhận交易”Xử lý", 'error');
                        }
                    }
                    $.modalAlert("Số nội trú là:" + $("#zyh").val() + "，HủyThành công", 'warning');
                    AbledSysBasicInfo();
                    newtouch_globalevent_f4(event);
                }
                $.modalAlert(data.message, "success");
            }
        });
    }


}

function DisableSysBasicInfo() {
    $("#csny").attr("disabled", "disabled");
    $('input[name="xb"]').each(
        function () { $(this).parent().attr("disabled", "disabled"); });
    $("#blh").attr("disabled", "disabled");
    $("#phone").attr("disabled", "disabled");
    //$("#dy").attr("disabled", "disabled");
    $("#hf").attr("disabled", "disabled");
}

function AbledSysBasicInfo() {

    $("#mzmc").removeAttr("data-label");
    $("#gjmc").removeAttr("data-label");
    $("#jkjlmc").removeAttr("data-label");
    $("#xm").html("");
    $("#zjh").html("");
    $("#xb").html("");
    $("#nlshow").html("");
    $("#blh").html("");
    $("#phone").html("");
    $("#cblb").val("");
    //$("#dy").html("");
    $("#zyh").removeAttr("disabled").removeAttr("style");
    $('#zyh').attr("placeholder", "");
    patInfoObj = {};
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
    var yjj = 0;
    $.najax({
        url: "/SystemManage/InpatientAccountManage/GetZyAccount?zyh=" + $("#zyh").val() + "&zhxz=3",
        cache: false,
        async: false,
        alertbierror: false,
        success: function (zhyeData) {
            if (zhyeData && zhyeData.data) {
                yjj = zhyeData.data.zhye;
            }
        }
    });
    if (yjj > 0) {
        $.modalAlert("预交金为：" + yjj + "，Vui lòng trước退预交金TrongHủyNội trú", 'error');
        return;
    } else {
        Cancel();
    }
   
}

function newtouch_event_f4() {
    AbledSysBasicInfo();
    $("#patid").val("");
}

function checkNotNull() {
    var patid = $("#patid").val();
    if (!patid) {
        $.modalAlert("Bệnh nhânThông tin cơ bản不LưuTrong，无法Lưu", 'warning');
        return false;
    }
    if ($("#rytj").val() == "9921") {
        if ($('#syrq').val().length == 0) {
            $.modalAlert("Mới生儿随母Nhập việnVui lòng nhậpSinh đẻNgày", 'warning');
            return false;
        }
        if ($('#syfwzh').val().length == 0) {
            $.modalAlert("Mới生儿随母Nhập việnVui lòng nhậpSinh đẻ服务证Số", 'warning');
            return false;
        }
    }
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            doctormc: { required: true },
            zdmc1: { required: true },
            ryrq: { required: true },
            ksmc: { required: true },
            bqmc: { required: true },
            kh: { required: true },
            zyh: { required: true }
        },
        messages: {
            doctormc: { required: "Bác sĩBắt buộc phải nhập" },
            zdmc1: { required: "Chẩn đoán nhập viện一Bắt buộc phải nhập" },
            ryrq: { required: "Nhập việnThời gianBắt buộcChọn" },
            ksmc: { required: "KhoaBắt buộc phải nhập" },
            bqmc: { required: "Khu bệnhBắt buộc phải nhập" },
            kh: { required: (boolsearchwithkh ? "Số thẻ" : "Số hồ sơ bệnh án") + "Bắt buộc phải nhập" },
            zyh: { required: "Số nội trúBắt buộc phải nhập" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                if (Array.isArray(errorList) && errorList.length == 0) {
                    return;
                }
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }

    if ($.getDate({ date: $('#ryrq').val() }) > $.getDate()) {
        $.modalAlert('Nhập việnThời gianKhông được lớn hơn当天', 'warning');
        return false;
    }
    return true;
}

//Nhân viênTra cứu
function GetPatSerarchView(blh) {
    $.modalOpen({
        id: "patSearch",
        title: "Tra cứu bệnh nhân",
        url: "/OutpatientManage/OutpatientAccounting/SysPatEntitiesblhView?t=" + Math.random() + "&blh=" + blh,
        width: "700px",
        height: "600px",
        callBack: function (iframeId) {
            top.frames[iframeId].PatDbGrid(); //Trong sự kiện cửa sổ bật lên
        }
    });
}

//按Số hồ sơ bệnh án检索Bệnh nhânThông tin cơ bản
function CallbackPatientQuery(obj) {
    GetQueryFphAjax(obj);
}


