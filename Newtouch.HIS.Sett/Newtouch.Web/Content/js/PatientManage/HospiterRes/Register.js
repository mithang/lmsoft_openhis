
var keyValue = $.request("");
var zyhpz = "";
var boolsearchwithkh = $("").val() === "";
var patInfoObj = {};
var ybnhlx = "";
var IsSuccess = true;//异常标示,false标示有异常
$(function () {
    $("").val($.getTime());
    $("").val("");
    zyhpz = $("").val();
    $("").keydown(function (e) {
        var ev = document.all ? window.event : e;
        var v_id = $(ev.target).attr('');
        if ((e.keyCode === 13 && boolsearchwithkh) || (v_id === "" && e.keyCode === 13 && $.isEmptyObject(patInfoObj))) {
            var kh = $("").val();
            $.ajax({
                url: "",
                data: { "": kh, "": $("").val() },
                dataType: '',
                async: false,
                success: function (rep) {
                    if (rep && rep.data) {
                        ClearAll();
                        CallbackPatientQuery(rep.data);
                        return;
                        //if (rep === 0) {
                        //    //无卡做免卡登记 
                        //    btn_NocardRes();
                        //} else {
                        //    if (rep !== undefined) {
                        //        fillInfo(rep);
                        //    }
                        //}
                    }

                    //无卡做免卡登记 
                    btn_NocardRes();
                }
            });
        }
    });


    initControl();
    $("").keyupEnterEvent(function () {
        btn_Save();
    });

    if (!boolsearchwithkh) {
        //Số bệnh án，Họ tên，拼音关键字搜索
        $('').newtouchBatchFloatingSelector({
            width: 500,
            height: 200,
            caption: "",
            url: "",
            ajaxparameters: function ($thisinput) {
                var keyword = $thisinput.val().trim();
                return "" + keyword;
            },
            itemdbclickhandler: function ($thistr, $thisinput) {
                CallbackPatientQuery($thistr.attr(''));
            },
            colModel: [
                { label: '', name: '', hidden: true },
                { label: '', name: '', width: 130, align: '' },
                { label: '', name: '', width: 120, align: '' },
                { label: '', name: '', hidden: true, width: 100, align: '' },
                {
                    label: '', name: '', width: 70, align: '', formatter: function (cellvalue) {
                        return $.getGender(cellvalue);
                    }
                },
                {
                    label: '', name: '', width: 100, align: '', formatter: function (cellvalue, a, b) {
                        return getAgeFromBirthTime({ begin: b.csny }).text;
                    }
                },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
                { label: '', name: '', align: '', hidden: true },
            ]
        });
    }
});

function fillInfo(rep) {
    //有卡显示基本信息
    $("").formSerialize(rep);
    getcitycode(rep);
    if (!rep.zyh) {
        //是新登记
        if (rep.hf || rep.hf == 0) {
            $("").val(rep.hf).trigger('');
        }
        else {
            $("").val('').trigger('');
        }
    }
    if ((rep.zyh !== null && rep.hasOwnProperty("")) || zyhpz == "") {
        $("").attr("", "").css("", "");
        $('').attr("", "");
        $('').html("");
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);
    }
    if (rep[""]) {
        $("").attr("", rep[""]);   //会：Hiển thị không có，值有
    }
    if (rep[""]) {
        var type = rep[""].trim();
        switch (type) {
            case "": type = ""; break;
            case "": type = ""; break;
            case "": type = ""; break;
            default:
                type = ""; break;

        }
        $("").html(type);
    }

    var patModel = rep;
    $("").html($.getGender(patModel.xb));
    $("").html((patModel.csny && patModel.csny.length >= 10 ? patModel.csny.substring(0, 10) : ""));
    $("").html(patModel.zjh);
    $("").html(patModel.jsr);
    $("").html(patModel.xm);
    $("").html(getAgeFromBirthTime({ begin: patModel.csny }).text);
    $("").html(patModel.phone);
    //$("").html(patModel.dybh === 1 ? '' : (patModel.dybh === 0 ? '' : ''));
    $("").val(boolsearchwithkh ? patModel.kh : patModel.blh);
    $("").html(boolsearchwithkh ? patModel.blh : patModel.kh);
    $("").val(patModel.zjlx);
    //Tải thông tin bệnh nhân
    patInfoObj.xm = patModel.xm; //Họ tên
    patInfoObj.xb = patModel.xb; //Giới tính
    patInfoObj.csny = patModel.csny;
    patInfoObj.patid = patModel.patid; //Mã nội bộ bệnh nhân
    patInfoObj.blh = patModel.blh; //Số bệnh án
    patInfoObj.zjh = patModel.zjh; //Số giấy tờ
    patInfoObj.jsr = patModel.jsr; //Người giới thiệu
    patInfoObj.brxz = patModel.brxz;//Tính chất bệnh nhân
    patInfoObj.zjlx = patModel.zjlx; //Loại giấy tờ
    patInfoObj.csny = patModel.csny;//Năm sinh
    //禁用系统病人基本信息
    DisableSysBasicInfo();
}
function loadPatiInfo(rep) {
    getcitycode(rep);
    $("").val(rep.gms);
    $("").val(rep.cs_dz);
    $("").val(rep.xian_dz);
    $("").val(rep.hu_dz);
    $("").val(rep.jjlxr_dz);
}

function btn_NocardRes(t) {
    if (t != null && t === 1) {
        localStorage.removeItem("");
    }
    $.modalOpen({
        id: "",
        title: "",
        url: "" + new Date() + "" + t,
        width: "",
        height: "",
        callBack: function (iframeId) {
            //top.frames[iframeId].submitForm();
            $.currentWindow(iframeId).AcceptClick(function (obj) {
                if (obj.CardType === '') {
                    if (IsOpenSfzYbJz !== "") {
                        $.modalAlert("", '');
                        $.modalClose("");
                        return;
                    }
                    //Giao diện bảo hiểm y tế gọi chứng minh nhân dân
                    //...
                }
                GetQueryFphAjax(obj);
                $.modalClose("");
            });
        }
    });
}

function initControl() {
    ///Huấn luyện viên sức khỏe
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysStaffDutyList, function (idx, val) {
                if (((val.StaffPY && val.StaffPY.indexOf(keyword.toLowerCase()) >= 0)
                    || (val.StaffName && val.StaffName.indexOf(keyword.toLowerCase()) >= 0)
                    || keyword.trim() == "")
                    && val.DutyCode == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 25
            },
            { label: '', name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        }
    });
    ///Bác sĩ
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();

            $.each(top.window.clients.sysStaffDutyList, function (idx, val) {
                if (((val.StaffPY && val.StaffPY.toLowerCase().indexOf(keyword.toLowerCase()) >= 0)
                    || (val.StaffName && val.StaffName.indexOf(keyword.toLowerCase()) >= 0)
                    || keyword.trim() == "")
                    && val.DutyCode == "") {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 25
            },
            { label: '', name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        }
    });
    ///Chính sách hoàn trả
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
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
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 25
            },
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25,hidden:true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html())
                .attr("", $thistr.find("").html()).attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            if (openYbSett && medicalInsurance == "" && $("").attr("") === "") {
                ybnhlx = "";
            }
            if (openXnhSett && medicalInsurance == "" && $("").attr("") === "") {
                ybnhlx = "";
            }
            $('').trigger('');
            $('').trigger('');
            $('').trigger('');
            //hss 2019.7.19 同个病人存在多个交易类型时，根据报销政策加载卡号
            $.ajax({
                type: "",
                url: "",
                data: { patid: $('').val(), brxz: $("").attr("") },
                dataType: "",
                async: false,
                success: function (r) {
                    if (!!r.data && !!r.data.CardNo) {
                        if (!boolsearchwithkh) {
                            //Bật tìm kiếm số bệnh án
                            $("").html(r.data.CardNo);

                        } else {
                            $("").val(r.data.CardNo);
                        }
                    }
                }
            });
            return;
        }
    });
    ///Chẩn đoán nhập viện zdmc1
    $("").newtouchBatchFloatingSelector({
        height: 200,
        width: 600,
        clickautotrigger: true,
        url: "",
        ajaxparameters: function () {
            return "" + $("").val() + "" + ybnhlx;
        },
        caption: "",
        colModel: [
            { label: '', name: '', widthratio: 60 },
            { label: '', name: '', widthratio: 20 },
            { label: '', name: '', widthratio: 20 },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true }
        ],
        itemdbclickhandler: function (data) {
            $("").attr("", data.attr('') + "" + data.attr(''));
            $("").val(data.attr(''));
            return;
        }
    });
    ///Chẩn đoán nhập viện zdmc2
    $("").newtouchBatchFloatingSelector(
        {
            height: 200,
            width: 600,
            url: "",
            clickautotrigger: true,
            ajaxparameters: function () {
                return "" + $("").val() + "" + ybnhlx;
            },
            caption: "",
            colModel: [
                { label: '', name: '', widthratio: 60 },
                { label: '', name: '', widthratio: 20 },
                { label: '', name: '', widthratio: 20 },
                { label: '', name: '', hidden: true },
                { label: '', name: '', hidden: true }
            ],
            itemdbclickhandler: function (data) {
                $("").attr("", data.attr('') + "" + data.attr(''));
                $("").val(data.attr(''));
                return;
            }
        });
    ///Chẩn đoán nhập viện zdmc3
    $("").newtouchBatchFloatingSelector({
        height: 200,
        width: 600,
        clickautotrigger: true,
        url: "",
        ajaxparameters: function () {
            return "" + $("").val() + "" + ybnhlx;
        },
        caption: "",
        colModel: [
            { label: '', name: '', widthratio: 60 },
            { label: '', name: '', widthratio: 20 },
            { label: '', name: '', widthratio: 20 },
            { label: '', name: '', hidden: true },
            { label: '', name: '', hidden: true }
        ],
        itemdbclickhandler: function (data) {
            $("").attr("", data.attr('') + "" + data.attr(''));
            $("").val(data.attr(''));
            return;
        }
    });
    ///Khoa
    $("").newtouchBatchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysDepartList, function (idx, val) {
                if (((val.py && val.py.toLowerCase().indexOf(keyword) >= 0)
                    || (val.Name && val.Name.indexOf(keyword) >= 0)
                    || keyword.trim() == "") && val.mzzybz != '' && val.Code != '') {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "",
        colModel: [
            { label: '', name: '', widthratio: 30 },
            { label: '', name: '', widthratio: 50 },
            { label: '', name: '', hidden: true }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        },
    });
    ///Khu bệnh
    $("").newtouchBatchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
            var resultObjArr = new Array();
            var ks = $("").attr("");
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
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 50
            }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        },
    });
    ///Dân tộc
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
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
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 30
            },
            {
                label: '', name: '', widthratio: 30
            },
            { label: '', name: '', widthratio: 30 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        },
    });
    ///Quốc tịch
    $("").newtouchFloatingSelector({
        height: 200,
        width: 300,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
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
        caption: "",
        colModel: [
            {
                label: '', name: '', widthratio: 25
            },
            {
                label: '', name: '', widthratio: 25
            },
            { label: '', name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            $("").attr("", $thistr.find("").html());
            $("").val($thistr.find('').html());
            return;
        },
    });
    //Nghề nghiệp
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr;
        }
    });
    //转出医院
    //$("").newtouchBindSelect({
    //    datasource: function () {
    //		var resultObjArr = new Array();
    //		$.each($.itemDetails.getItems(''), function () {
    //			$('').append('' + this.Code + '' + this.Name + '');
    //		});
    //		return resultObjArr;
    //	}
    //});
    //Quan hệ người liên hệ khẩn cấp
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr;
        }
    });

    //Quan hệ người liên hệ khẩn cấp
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr2 = new Array(); $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr2;
        }
    });

    if (!boolsearchwithkh) {
        //Bật tìm kiếm số bệnh án
        $("").html("");
        $("").html("");
    }

    //入院诊断必填
    if ($('').val() == '') {
        $('').html('').addClass('');
    }

    //
    $('').bindSelect();
    //
    $('').bindSelect();
}
var errorMsg = "";//存放出错信息
function btn_Save() {
    var result = checkNotNull();
    if (result) {
        var data = $("").formSerialize();
        data = savecitycode(data);
        data[""] = patInfoObj.csny;
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        if ($("").val()) {
            data[""] = $("").attr("");
        }
        else {
            data[""] = null;
        }
        if (!data[""]) {
            $.modalAlert("", '');
            return;
        }
        if ($("").val() == "" && !$("").val()) {
            $.modalAlert("", '');
            return;
        }
        var zd1 = $("").attr("");
        data[""] = null;
        if (zd1 && $("").val().trim() != "" && $("").val() != null) {
            data[""] = (zd1.split('', 1))[0];
        } else {
            data[""] = null;
        }
        var zd2 = $("").attr("");
        data[""] = null;
        if (zd2 && $("").val().trim() != "" && $("").val() != null) {
            data[""] = (zd2.split('', 1))[0];
        } else {
            data[""] = null;
        }
        var zd3 = $("").attr("");
        data[""] = null;
        if (zd3 && $("").val().trim() != "" && $("").val() != null) {
            data[""] = (zd3.split('', 1))[0];
        } else {
            data[""] = null;
        }
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        data[""] = $("").attr("");
        data[""] = $("").html();
        data[""] = $("").html();
        if (!boolsearchwithkh) {
            tempp = data[""];
            data[""] = data[""];
            data[""] = tempp;
        }
        data[""] = $.getGenderCode($("").html());
        data[""] = $("").val();
        data[""] = $("").html();
        data[""] = $("").val();
        data[""] = $("").val();
        data[""] = $("").val();

        data[""] = $("").val();
        data[""] = $("").find("").text();
        data[""] = $("").val();
        data[""] = $("").text();

        //入院诊断必填
        if ($('').val() == ''
            && !!!data[""]) {
            $.modalAlert("", '');
            return;
        }


        var jylsh = "";//存放交易流水号
        var jyyzm = "";//存放交易校验码

        if (openYbSett && $("").attr("") === "") //Bật bảo hiểm y tế,Tính chất bảo hiểm y tế
        {
            if (cqPatInfo.ybVer != "") {
                if (cqPatInfo.ybVer == undefined) {
                    $.modalAlert("", '');
                    return;
                }
            }
            if (!data.zyh) //(Số nhập viện không có)Đăng ký nhập viện
            {
                if (isZYZT) {
                    $.modalAlert("", '');
                    return;
                }
            } else { //(Số nhập viện có)Sửa đăng ký nhập viện
                //获取入院登记反馈信息

            }
        }
        var jzlx = cqPatInfo.mdtrt_cert_type;
        data[""] = cqPatInfo.jzlx;//$('').val();
        data[""] = cqPatInfo.jzlx;
        if (IsSuccess) {
            $.submitForm({
                url: "",
                loading: "",
                param: { vo: data, isybjy: openYbSett },
                successwithtipmsg: false,
                success: function (datareq) {
                    if (datareq.message === "") {

                        if (openYbSett && $("").attr("") === "") {

                            if (cqPatInfo.ybVer == "") {
                                shanghaiV5ybrydj(datareq, data, jzlx);//上海五期医保入院登记 SJ11
                            } else if (cqPatInfo.ybVer == "") {
                                gjybrydj(datareq, jzlx);//Gọi chương trình bảo hiểm y tế quốc gia2401 Nhập viện đăng ký
                            }
                        }
                        if (!IsSuccess) {
                            $.najax({
                                url: "" + datareq.data.zyh,
                                dataType: '',
                                async: false,
                                success: function (data) {
                                    if (data.message == "") {
                                        $.modalAlert(errorMsg, '');
                                    }
                                    $.modalAlert(errorMsg, "");
                                }
                            });
                        } else {
                            $.modalAlert("" + datareq.data.zyh, '');
                        }

                    }
                    else if (datareq.message === "") {
                        $.ajax({
                            type: "",
                            url: "",
                            data: { zyh: data.zyh },
                            dataType: "",
                            cache: false,
                            async: false,
                            success: function (payoptype) {
                                if (!payoptype) {
                                    $.modalAlert("", '');
                                    return;
                                }
                                if (cqPatInfo.ybVer == "") {
                                    shanghaiV5djcx(jzlx, $('').val());//上海五期医保入院登记撤销
                                    shanghaiV5ybrydj(datareq, data, jzlx);//上海五期医保入院登记 SJ11

                                } else {
                                    gjybrydjxg(payoptype);//Gọi chương trình bảo hiểm y tế quốc gia2401 Nhập viện đăng ký
                                }
                            }
                        });
                        if (!IsSuccess) {
                            errorMsg = "" + errorMsg;
                        } else {
                            $.modalAlert("" + datareq.data.zyh + "", '');
                        }
                    } else {
                        //Thất bại，重庆医保登记红冲
                        if (openYbSett && $("").attr("") === "") {
                            $.najax({
                                type: "",
                                url: "" + datareq.data.zyh,
                                dataType: "",
                                loadingtext: "",
                                success: function (ajaxresp) {
                                    if (ajaxresp && ajaxresp.jylsh || cqPatInfo.ybVer == "") {
                                        if (cqPatInfo.ybVer == "") {
                                            shanghaiV5djcx(jzlx, $('').val());//上海五期医保入院登记撤销
                                        }
                                        else {
                                            var payoptype = { hisId: datareq.data.zyh, '': ajaxresp.jylsh, '': curUserCode, "": curUserName, '': $("").val(), '': $("").val() };
                                            $.ajax({
                                                type: "",
                                                url: "",
                                                dataType: "",
                                                data: payoptype,
                                                async: false,
                                                success: function (data) {
                                                    var cqybjyDenySettleReturn = eval('' + data + '');
                                                    if (cqybjyDenySettleReturn.infcode == "") {
                                                        //////Khám bệnh hủy đăng ký，不需要落地
                                                    } else {
                                                        $.modalAlert('' + cqybjyDenySettleReturn.err_msg, '');
                                                    }
                                                }
                                            });
                                        }
                                    }
                                }
                            });
                        }
                        $.modalAlert("", '');
                    }
                    ClearAll();
                    $.loading(false);
                }
            });
        } else
            $.modalAlert(errorMsg, '');
    }
}
function gjybrydjxg(payoptype) {
    payoptype.mdtrt_cert_type = cqPatInfo.mdtrt_cert_type;
    payoptype.orgId = orgId;
    payoptype.med_type = ($("").val() == "" ? "" : $("").val());//($("").val() == "" ? "" : "");
    payoptype.czlb = 1;
    $.ajax({
        type: "",
        url: "",
        dataType: "",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('' + data + '');
            if (medicalReg) {
                if (medicalReg.infcode == "") {
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
        carddata: jzlx == "" ? cqPatInfo.ecToken : cqPatInfo.sbkh,
        hisId: zyh,//Số nhập viện
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("").val()
    };
    $.ajax({
        type: "",
        url: "",
        dataType: "",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('' + data + '');
            if (medicalReg) {
                if (medicalReg.xxfhm === "") {

                }
                else {
                    //$.modalAlert("" + medicalReg.ErrorMsg, '');
                    IsSuccess = false;
                    errorMsg = "" + medicalReg.err_msg;
                }
            }
        }
    });
}
function gjybrydj(datareq, jzlx) {
    var payoptype = {
        mdtrt_cert_type: jzlx,
        mdtrt_cert_no: jzlx == "" ? cqPatInfo.ecToken : (jzlx == "" ? $("").val() : patModel.kh),
        hisId: datareq.data.zyh,//Số nhập viện
        med_type: ($("").val() == "" ? "" : $("").val()),//21Nhập viện thông thường，22转入住院
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("").val(),
        orgId: orgId,
        psn_no: $("").val(),
        dise_codg: $("").val(),
        dise_name: $("").find("").text()
    };
    $.ajax({
        type: "",
        url: "",
        dataType: "",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('' + data + '');
            if (medicalReg) {
                if ((medicalReg.infcode == "" || medicalReg.infcode == 0) && medicalReg.output.result.mdtrt_id) {
                    $.najax({
                        type: "",
                        data: {
                            patid: $("").val(),
                            zymzh: datareq.data.zyh,
                            jylsh: medicalReg.output.result.mdtrt_id,
                            jytype: "",
                            medicalInList: null
                        },
                        url: "",
                        dataType: "",
                        loading: false,
                        success: function (ajaxresp) {
                        }
                    });
                }
                else {
                    //$.modalAlert("" + medicalReg.ErrorMsg, '');
                    IsSuccess = false;
                    errorMsg = "" + medicalReg.err_msg;
                }
            }
        }
    });
}
function shanghaiV5ybrydj(datareq, data, jzlx) {
    var payoptype = {
        cardtype: cqPatInfo.jzlx,
        carddata: cqPatInfo.carddata,//jzlx == "" ? cqPatInfo.ecToken : cqPatInfo.sbkh,
        operatorId: curUserCode,
        operatorName: curUserName,
        insuplc_admdvs: $("").val(),
        orgId: orgId,
        hisId: datareq.data.zyh//Số nhập viện
    };
    $.ajax({
        type: "",
        url: "",
        dataType: "",
        data: payoptype,
        async: false,
        success: function (data) {
            var medicalReg = eval('' + data + '');
            if (medicalReg) {
                if (medicalReg.xxfhm === "") {
                    $.najax({
                        type: "",
                        data: {
                            patid: $("").val(),
                            zymzh: datareq.data.zyh,
                            jylsh: medicalReg.xxnr.jzdyh,
                            jytype: "",
                            medicalInList: null
                        },
                        url: "",
                        dataType: "",
                        loading: false,
                        success: function (ajaxresp) {
                        }
                    });
                }
                else {
                    IsSuccess = false;
                    errorMsg = "" + medicalReg.fhxx;
                }
            } else {
                IsSuccess = false;
                errorMsg = "" + medicalReg.fhxx;
            }
        }, error: function (request, error, ex) {
            success = false;
            errMsg = "" + ex + "";
        }
    });
}
function PrinyZYInfo() {
    if (!$("").val()) {
        $.modalAlert("", '');
        return;
    } else {
        $.modalAlert("", '');
        return;
    }
    var data = $("").formSerialize();
    data[""] = $("").attr("");
    data[""] = $("").attr("");
    data[""] = $("").attr("");
    $.ajax({
        url: "",
        data: data,
        dataType: '',
        async: false,
        success: function (data) {
            if (data.message = "") {
                $.modalAlert(data.message, "");
            }
        }
    });
}

function PrintWDInfo() {
    if (!$("").val()) {
        $.modalAlert("", '');
        return;
    } else {
        $.modalAlert("", '');
        return;
    }
}

function Cancel() {
    debugger;
    if (!$("").val()) {
        $.modalAlert("", '');
        return;
    }

    var IsSuccess = true;//异常标示,false标示有异常
    var errorMsg = "";//存放出错信息
    var jylsh = "";//存放交易流水号
    var jyyzm = "";//存放交易校验码

    if (openYbSett && $("").attr("") === "") //Bật bảo hiểm y tế,贵安医保,Tính chất bảo hiểm y tế
    {
        $.najax({
            type: "",
            url: "" + $("").val(),
            dataType: "",
            async: false,
            loadingtext: "",
            success: function () {
                $.najax({
                    type: "",
                    url: "" + $('').val(),
                    dataType: "",
                    async: false,
                    loadingtext: "",
                    success: function (ajaxresp) {
                        if (ajaxresp && (ajaxresp.jylsh || cqPatInfo.ybVer == "")) {
                            if (cqPatInfo.ybVer == "") {
                                var jzlx = $('').val();
                                shanghaiV5djcx(jzlx, $('').val());//上海五期医保入院登记撤销
                            } else {
                                var payoptype = { hisId: $('').val(), '': ajaxresp.jylsh, '': curUserCode, "": curUserName, '': $("").val(), '': $("").val() };

                                $.ajax({
                                    type: "",
                                    url: "",
                                    dataType: "",
                                    data: payoptype,
                                    async: false,
                                    success: function (data) {
                                        var cqybjyDenySettleReturn = eval('' + data + '');
                                        if (cqybjyDenySettleReturn.infcode == "") {
                                        } else {
                                            $.modalAlert('' + cqybjyDenySettleReturn.err_msg, '');
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
            url: "" + $("").val(),
            dataType: '',
            async: false,
            success: function (data) {
                if (data.message == "") {
                    if (openYbSett && medicalInsurance == "" && $("").attr("") === "") {
                        var ybReq = {
                            astr_jylsh: jylsh,  //Thông tin trả về bảo hiểm y tế
                            astr_jyyzm: jyyzm  //Thông tin trả về bảo hiểm y tế
                        }
                        var ybjySettReturn;// = $.guianyibao.YibaoConfirm(ybReq);//贵安医保确认提交
                        $.ajax({
                            url: "",
                            data: ybReq,
                            dataType: "",
                            async: false,
                            success: function (data) {
                                ybjySettReturn = eval('' + data + '');
                            }
                        });
                        if (!(ybjySettReturn.Code == 0)) {
                            $.modalAlert("" + jylsh + "", '');
                        }
                    }
                    $.modalAlert("" + $("").val() + "", '');
                    AbledSysBasicInfo();
                    newtouch_globalevent_f4(event);
                }
                $.modalAlert(data.message, "");
            }
        });
    }


}

function DisableSysBasicInfo() {
    $("").attr("", "");
    $('').each(
        function () { $(this).parent().attr("", ""); });
    $("").attr("", "");
    $("").attr("", "");
    //$("").attr("", "");
    $("").attr("", "");
}

function AbledSysBasicInfo() {

    $("").removeAttr("");
    $("").removeAttr("");
    $("").removeAttr("");
    $("").html("");
    $("").html("");
    $("").html("");
    $("").html("");
    $("").html("");
    $("").html("");
    $("").val("");
    //$("").html("");
    $("").removeAttr("").removeAttr("");
    $('').attr("", "");
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
        url: "" + $("").val() + "",
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
        $.modalAlert("" + yjj + "", '');
        return;
    } else {
        Cancel();
    }
   
}

function newtouch_event_f4() {
    AbledSysBasicInfo();
    $("").val("");
}

function checkNotNull() {
    var patid = $("").val();
    if (!patid) {
        $.modalAlert("", '');
        return false;
    }
    if ($("").val() == "") {
        if ($('').val().length == 0) {
            $.modalAlert("", '');
            return false;
        }
        if ($('').val().length == 0) {
            $.modalAlert("", '');
            return false;
        }
    }
    var validator = $('').validate();
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
            doctormc: { required: "" },
            zdmc1: { required: "" },
            ryrq: { required: "" },
            ksmc: { required: "" },
            bqmc: { required: "" },
            kh: { required: (boolsearchwithkh ? "" : "") + "" },
            zyh: { required: "" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                if (Array.isArray(errorList) && errorList.length == 0) {
                    return;
                }
                $.modalAlert(errorList[0].message, '');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }

    if ($.getDate({ date: $('').val() }) > $.getDate()) {
        $.modalAlert('', '');
        return false;
    }
    return true;
}

//Tra cứu nhân viên
function GetPatSerarchView(blh) {
    $.modalOpen({
        id: "",
        title: "",
        url: "" + Math.random() + "" + blh,
        width: "",
        height: "",
        callBack: function (iframeId) {
            top.frames[iframeId].PatDbGrid(); //Trong sự kiện cửa sổ bật lên
        }
    });
}

//按病历号检索病人基本信息
function CallbackPatientQuery(obj) {
    GetQueryFphAjax(obj);
}


