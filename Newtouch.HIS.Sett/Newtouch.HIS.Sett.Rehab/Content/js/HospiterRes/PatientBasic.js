var keyValue = $.request("keyValue");
$(function () {
    initControl();
    //Lấy số thẻ mới nhất
    $.ajax({
        url: "/PatientManage/HospiterRes/GetCardNoAndBLH?r=" + Math.random(),
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            $.currentWindow().$("#kh").val(data.kh);
        }
    });
    $('#nl').val("");
    $('#mz').val("");
    $('#gj').val("");
    $("#hf").val("");
    $('#myTab a:first').tab('show');
    //Lấy phiên âm theo họ tên
    $('#xm').blur(function () {
        $('#py').val($(this).toPinyin());

    });

    $('#zjh').bind("keydown blur", function (e) {
        if ($('#zjlx').val() == "0") {
            //Lấy số CMND đã nhập
            var sfzh = $(this).val();
            var csrq;
            if (sfzh.length == 15) {
                csrq = "19" + sfzh.substr(6, 6);
            } else if (sfzh.length == 18) {
                csrq = sfzh.substr(6, 8);
            }
            if ((sfzh.length == 15 && !csrq) || sfzh.length == 18) {
                csrq = csrq.replace(/(.{4})(.{2})/, "$1-$2-");
                //Lấy giới tính
                var xb;
                if (parseInt(sfzh.substr(16, 1) % 2 == 1)) {
                    xb = 1;
                } else {
                    xb = 0;
                }
                //Lấy tuổi
                var myDate = new Date();
                var month = myDate.getMonth() + 1;
                var day = myDate.getDate();
                var age = myDate.getFullYear() - sfzh.substring(6, 10) - 1;
                if (sfzh.substring(10, 12) < month || sfzh.substring(10, 12) == month && sfzh.substring(12, 14) <= day) {
                    age++;
                }
                $('#zjh').val(sfzh);
                $('#csny').val(csrq);
                //$('input[name="xb"]:checked').parent().remove('active');
                if (xb == 0) {
                    //  $("input[name=xb]:eq(0)").removeAttr("checked");
                    if ($("input[name=xb]:eq(0)").parent().hasClass('active')) {
                        $("input[name=xb]:eq(0)").parent().removeClass('active');
                    }
                    $("input[name=xb]:eq(1)").attr("checked", 'checked');//Nữ
                    $("input[name=xb]:eq(1)").parent().addClass('active');
                } else {
                    // $("input[name=xb]:eq(1)").removeAttr("checked");
                    if ($("input[name=xb]:eq(1)").parent().hasClass('active')) {
                        $("input[name=xb]:eq(1)").parent().removeClass('active');
                    }
                    $("input[name=xb]:eq(0)").attr("checked", 'checked');//Nam
                    $("input[name=xb]:eq(0)").parent().addClass('active');
                }
                $('#nl').val(age);
            }
        }
    });
})

function getAge(date) {
    var aDate = new Date();
    var thisYear = aDate.getFullYear();
    var brith = date;
    brith = brith.substr(0, 4);
    age = (thisYear - brith);
    $("#nl").val(age);
}

function initControl() {
    ///Chính sách hoàn trả
    $("#brxz").newtouchFloatingSelector({
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
            { label: 'Mã số', name: 'brxzbh', widthratio: 25 },
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
    //Khu vực
    //$("#dy").newtouchBindSelect({
    //    datasource: function () {
    //        var resultObjArr = new Array();
    //        if (top.clients.dataItems) {
    //            $.each(top.clients.dataItems, function (idx, val) {
    //                if (idx == "AreaCity") {
    //                    $.each(val, function (key, value) {
    //                        $('#dy').append('<option value="' + key + '">' + value + '</option>');
    //                    });
    //                }
    //            });
    //        }
    //        return resultObjArr;
    //    }
    //});
    //Trình độ học vấn
    $("#xl").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Education") {
                        $.each(val, function (key, value) {
                            $('#xl').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //Quốc tịch
    $("#gj2").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Nationality") {
                        $.each(val, function (key, value) {
                            $('#gj2').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //Dân tộc
    $("#mz2").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Nation") {
                        $.each(val, function (key, value) {
                            $('#mz2').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
    //Nghề nghiệp
    $("#zy").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            if (top.clients.dataItems) {
                $.each(top.clients.dataItems, function (idx, val) {
                    if (idx == "Office") {
                        $.each(val, function (key, value) {
                            $('#zy').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }
                });
            }
            return resultObjArr;
        }
    });
}

function submitForm() {
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data["brxz"] = $("#brxz").attr("data-label");
        //data["dy"] = $("#dy").attr("data-label");
        $.submitForm({
            url: "/PatientManage/HospiterRes/SubmitForm",
            param: data,
            success: function () {
                $.loading(false);
                if ($.currentWindow().$("#txtkh").length == 1) {
                    //Đăng ký miễn thẻ khi đăng ký khám ngoại trú
                    $.currentWindow().LoadPatientInfo(data.kh);
                }
                else {
                    //Đăng ký thông tin cơ bản bệnh nhân nội trú miễn thẻ
                    var kh = $.currentWindow().$("#kh").val();
                    getSysBasicInfoByCardNo(kh);
                }
                $.currentWindow().DisableSysBasicInfo(true); //true:dấu hiệu là từ đăng ký miễn thẻ hoặc trả về chi tiết theo truy vấn số thẻ, false:khi tải trang, có thể nhập kh
            }
        })
    }
}

function getSysBasicInfoByCardNo() {
    var kh = $.currentWindow().$("#kh").val();
    $.ajax({
        url: "/PatientManage/HospiterRes/GetBasicInfoByCardNo?keyValue=" + kh,
        dataType: "json",
        async: false,
        success: function (data) {
            $.currentWindow().$("#kh").val(data.kh);
            $.currentWindow().$("#xm").val(data.xm);
            $.currentWindow().$("#xb").val(data.xb);
            $.currentWindow().$("#csny").val(data.csny);
            $.currentWindow().$("#kh").val(data.kh);
            $.currentWindow().$("#zjh").val(data.zjh);
            if (data.xb == 0) {
                $.currentWindow().$("input[name=xb]:eq(1)").attr("checked", 'checked');//Nữ
                $.currentWindow().$("input[name=xb]:eq(1)").parent().addClass('active');
            } else {
                $.currentWindow().$("input[name=xb]:eq(0)").attr("checked", 'checked');//Nam
                $.currentWindow().$("input[name=xb]:eq(0)").parent().addClass('active');
            }
            $.currentWindow().$("#nl").val(data.nl);
            $.currentWindow().$("#blh").val(data.blh);
            $.currentWindow().$("#dh").val(data.dh);
            $.currentWindow().$("#dy").val(data.dy);
            $.currentWindow().$("#hf").val(data.hf);
            $.currentWindow().$("#patid").val(data.patid);
            if (data.zjlx) {
                var type = data.zjlx.trim();
                switch (type) {
                    case "0": type = "Chứng minh nhân dân"; break;
                    case "1": type = "Hộ chiếu"; break;
                    case "2": type = "Chứng nhận sĩ quan"; break;
                    default: break;

                }
                $.currentWindow().$("#zjlx").html(type);
            }
            if (data.cs_sheng) {
                $.currentWindow().$("#cs_sheng").val(data.cs_sheng);
            }
            if (data.cs_shi) {
                $.currentWindow().$("#cs_shi").val(data.cs_shi);
            }
            if (data.cs_xian) {
                $.currentWindow().$("#cs_xian").val(data.cs_xian);
            }
            if (data.xian_dz) {
                $.currentWindow().$("#xian_dz").val(data.xian_dz);
            }
            if (data.xian_sheng) {
                $.currentWindow().$("#xian_sheng").val(data.xian_sheng);
            }
            if (data.xian_shi) {
                $.currentWindow().$("#xian_shi").val(data.xian_shi);
            }
            if (data.xian_xian) {
                $.currentWindow().$("#xian_xian").val(data.xian_xian);
            }
            if (data.hu_dz) {
                $.currentWindow().$("#hu_dz").val(data.hu_dz);
            }
            if (data.hu_sheng) {
                $.currentWindow().$("#hu_sheng").val(data.hu_sheng);
            }
            if (data.hu_shi) {
                $.currentWindow().$("#hu_shi").val(data.hu_shi);
            }
            if (data.hu_xian) {
                $.currentWindow().$("#hu_xian").val(data.hu_xian);
            }
            $.loading(false);
        }
    });
}

function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            xm: { required: true },
            brxz: { required: true },
            nl: { required: true },
            hf: { required: true },
            zjh: { isIdentity: true },
            email: { email: true },
            phone: { isMobile: true },
            dh: { isPhone: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            xm: { required: "Phải nhập họ tên" },
            brxz: { required: "Phải nhập tính chất bệnh nhân" },
            nl: { required: "Phải nhập tuổi" },
            hf: { required: "Phải chọn tình trạng hôn nhân" },
            blh: { required: "Phải chọn tình trạng hôn nhân" },
            zjh: { isIdentity: "Định dạng giấy tờ không đúng" },
            email: { email: "Định dạng email không đúng" },
            phone: { isMobile: "Định dạng điện thoại di động không đúng" },
            dh: { isPhone: "Định dạng điện thoại không đúng" },
            jjlldh: { isPhone: "Định dạng điện thoại không đúng" }
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
    //Số bệnh án
    var blh = $("#blh").val();
    if (!blh) {
        $.modalAlert("Số bệnh án trống!", 'warning');
        return false;
    }
    //Phiên âm
    var py = $("#py").val();
    if (!py) {
        $.modalAlert("Phiên âm trống!", 'warning');
        return false;
    }
    //Giới tính
    var xb = false;
    $('input[name="xb"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            xb = true;
            $("#xb").val()
        }
    });
    if (!xb) {
        $.modalAlert("Vui lòng chọn giới tính!", 'warning');
        return false;
    }
    return true;
}