var keyValue = $.request("keyValue");
var fromly = $.request("fromly");
var readbz = $.request("readbz");//Cờ đọc thẻ: khi đọc thẻ thì bỏ qua kiểm tra CMND/CCCD đã có thông tin thẻ khám hay chưa
var cz = "";//Xác nhận thao tác
var sfzh;
var cityref;
$(function () {
    if ($.request("parent") === 'cisframe') {
        fromly = "yktmodify";
    }
    if (fromly === "yktmodify") {
        $('#divYktRegister').remove();
        $('#divYktModify').show();
    }
    else {
        $('#divYktRegister').show();
        $('#divYktModify').remove();
    }
   
    initControl();
    gridInit();
    $('#myTab a:first').tab('show');
    //Lấy phiên âm (pinyin) theo họ tên
    $('#xm').keyup(function () {
        $('#py').val($(this).toShouPin());

    });

    $("#nlshow").keyup(function () {
        var nlshowText = $(this).val();
        if (!isNaN(nlshowText) && parseInt(nlshowText) > 0) {
            //Là số
            date = new Date();
            if ($('#nlshowdw').val() == '1') {
                //Năm
                var tempGetYearDate = function (year) {
                    var time = new Date();
                    time.setYear(time.getFullYear() + year);
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetYearDate(age));
            }
            else if ($('#nlshowdw').val() == '2') {
                //Tháng
                var tempGetMonthDate = function (month) {
                    var time = new Date();
                    time.setMonth(time.getMonth() + month);
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetMonthDate(age));
            }
            else if ($('#nlshowdw').val() == '3') {
                //Ngày
                var tempGetDayDate = function (day) {
                    var time = new Date();
                    time.setDate(time.getDate() + day);//Lấy ngày sau Day ngày 
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetDayDate(age));
            }
            else if ($('#nlshowdw').val() == '4') {
                //Giờ
                var tempGetHourDate = function (hour) {
                    var date1 = new Date().getTime();
                    var date2 = date1 + hour * 60 * 60 * 1000;
                    var newDate = new Date(date2);
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: newDate }) : $.getTime({ date: newDate });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetHourDate(age));
            }
        }
    });

    $('#zjh').bind("keydown blur",

        function (e) {
            if (((e.keyCode === 13 && e.type === "keydown") || e.type === "blur") && $('#zjlx').val() === "1") {
                //Lấy số CMND/CCCD đã nhập
                var sfzh = $(this).val();
                var len = $(this).val().length;
                //if (len == 18 || len == 15) {//checkCard()
                if (len === 18 || len === 15) {
                    var csrq;
                    csrq = sfzh.substr(6, 8);
                    if ((len === 15 && csrq) || len === 18) {
                        csrq = csrq.replace(/(.{4})(.{2})/, "$1-$2-");
                        //Lấy giới tính
                        var xb;
                        if (parseInt(sfzh.charAt(16) / 2) * 2 != sfzh.charAt(16))
                            xb = 1;
                        else
                            xb = 0;
                        //$('#zjh').val(sfzh);
                        $('#csny').val($('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: csrq }) : $.getTime({ date: csrq }));
                        $("input[name=xb]").parent().removeClass('active');
                        $("input[name=xb]").removeAttr('checked');
                        if (xb == 0) {
                            //$("input[name=xb]:eq(1)").attr("checked", 'checked'); //Nữ
                            //$("input[name=xb]:eq(1)").parent().addClass('active');
                            //$('#xb').val("2");
                            $("input[name=xb]:eq(1)").trigger('click'); //Nữ

                        }
                        else {
                            //$("input[name=xb]:eq(0)").attr("checked", 'checked'); //Nam
                            //$("input[name=xb]:eq(0)").parent().addClass('active');
                            //$('#xb').val("1");

                            $("input[name=xb]:eq(0)").trigger('click'); //Nam
                        }
                        setAge($('#csny').val());
                    }
                }
            }
        });
    $("#hu_dz").keyupEnterEvent(function () {
        submitForm();
    });
    $('#nlshowdw').change(function () {
        $('#nlshow').trigger('keyup');
    });
});

function setAge(strBirthday) {
    var theAge = getAgeFromBirthTime({ begin: strBirthday });
    if (!!theAge.text && !!theAge.nldw) {
        $("#nlshow").val(theAge.nl);
        $('#nlshowdw').val(theAge.nldw); //
    }
}

function initControl() {
    ///Chính sách hoàn trả chi phí
    $("#brxzmc").newtouchBatchFloatingSelector({
        height: 200,
        width: 400,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu, dùng keyword để lọc kết quả
            var resultObjArr = new Array();
            $.each(top.window.clients.sysPatientNatureList, function (idx, val) {
                if ((val.py && val.py.toLowerCase().indexOf(keyword.toLowerCase()) >= 0) ||
                    keyword.trim() == "" ||
                    (val.brxzmc && val.brxzmc.toLowerCase().indexOf(keyword) >= 0)) {
                    resultObjArr.push(val);
                }
            });
            return resultObjArr;

        },
        caption: "Tính chất bệnh nhân",
        colModel: [
            { label: 'Mã', name: 'brxz', widthratio: 25 },
            { label: 'Tên', name: 'brxzmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            if ($thistr.attr('data-brxz') === "8") {
                //Kiểm tra có cần đọc thẻ không
                if ($("#xnhylzh").val() === "" || $("#xnhgrbm").val() === "") {
                    $.modalAlert("Bệnh nhân hợp tác y tế nông thôn mới vui lòng đọc thẻ trước!", 'error');
                    return;
                }
            }
            $("#brxzmc").attr("data-label", $thistr.attr('data-brxz'));
            $("#brxzmc").val($thistr.attr('data-brxzmc'));
            return;
        }
    });
    $("#xl").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('Degree'), function () {
                $('#xl').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    //Cách liên hệ tốt nhất
    $("#zjlxfs").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('ContactWay'), function () {
                $('#zjlxfs').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    //Quốc tịch
    $("#gj2").newtouchBindSelect({
        //id: "gjCode",
        //text: "gjmc",
        datasource: function () {
            var resultObjArr = new Array();
            if (top.window.clients.sysNationalityList) {
                $.each(top.window.clients.sysNationalityList, function (idx, val) {
                    
                    if (val.gjCode == "cn") {
                        $("#gj2").append('<option value="' + val.gjCode + '" selected>' + val.gjmc + '</option>');
                    } else {
                        $("#gj2").append('<option value="' + val.gjCode + '">' + val.gjmc + '</option>');
                    }
                    //resultObjArr.push(val);
                });
            }
            return resultObjArr;
        }
    });
    //Dân tộc
    $("#mz2").newtouchBindSelect({
        //id: "mzCode",
        //text: "mzmc",
        datasource: function () {
            var resultObjArr = new Array();
            if (top.window.clients.sysNationList) {
                $.each(top.window.clients.sysNationList, function (idx, val) {
                    if (val.mzCode == "00000001") {
                        $("#mz2").append('<option value="' + val.mzCode + '" selected>' + val.mzmc + '</option>');
                    } else {
                        $("#mz2").append('<option value="' + val.mzCode + '">' + val.mzmc + '</option>');
                    }

                    //resultObjArr.push(val);
                });
            }
            return resultObjArr;
        }
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
    //Nguồn bệnh nhân
    $("#brly").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('PatientSource'), function () {
                $('#brly').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    //Hôn nhân
    //$("#hf").newtouchBindSelect({
    //    datasource: function () {
    //        var resultObjArr = new Array();
    //        $.each($.itemDetails.getItems('hy'), function () {
    //            $('#hf').append('<option value="' + this.Code + '">' + this.Name + '</option>');
    //        });
    //        return resultObjArr;
    //    }
    //});
    //Quan hệ người liên hệ khẩn cấp
    $("#jjllrgx").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('RelativeType'), function () {
                $('#jjllrgx').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    if (zjpz == 'OFF') {
        $(".zjclass").text('');
    }

    //
    $('#zjlx').bindSelect();
    $('#hf').bindSelect();
    $('#brxzmc').attr('autocomplete', 'off');

    $('#citylist_JJLXRDZ').citys({
        required: false,
        nodata: 'disabled',
        //province: ' ',
        //city: '',
        //area: '',
        onChange: function (info) {
            //townFormat(info);
        }
    }, function (api) {
        var info = api.getInfo();
        //townFormat(info);
    });

    $('#citylist_HKDZ').citys({
        required: false,
        nodata: 'disabled',
        //province: ' ',
        //city: '',
        //area: '',
        onChange: function (info) {
            //townFormat(info);
        }
    }, function (api) {
        var info = api.getInfo();
        //townFormat(info);
    });

    $('#citylist_CSD').citys({
        required: false,
        nodata: 'disabled',
        //province: '',
        //city: '',
        //area: '',
        onChange: function (info) {
            //townFormat(info);
        }
    }, function (api) {
        var info = api.getInfo();
        //townFormat(info);
    });
    $('#citylist_XDZ').citys({
        required: false,
        nodata: 'disabled',
        //province: '',
        //city: '',
        //area: '',
        onChange: function (info) {
            //townFormat(info);
        }
    }, function (api) {
        var info = api.getInfo();
        //townFormat(info);
    });


}

function gridInit()
{   
    if (fromly) { //Không phải popup đăng ký nhập viện mới từ đăng ký khám ngoại trú, bỏ khởi tạo danh sách thẻ khám
        return;
    }
    var $gridList = $("#patGridList");
    $gridList.dataGrid({
        //url: "/OutpatientManage/OutpatientAccounting/PatSearchInfo",
        //postData: { zjh: $("#zjh").val(), },
        height: 100,
        //width: $("#divPatCard").width() - 10,
        caption: 'Danh sách thẻ khám của bệnh nhân',
        colModel: [
                    { label: 'Khóa chính', name: 'patid', hidden: true },
                    { label: 'Số bệnh án', name: 'blh', width: 80, align: 'left' },
                    { label: 'Họ tên', name: 'xm', width: 80, align: 'left' },
                    { label: 'Ngày tháng năm sinh', name: 'csny', hidden: true, width: 120, align: 'left' },
                    {
                        label: 'Giới tính', name: 'xb', width: 80, align: 'left', formatter: function (cellvalue) {
                            return $.getGender(cellvalue);
                        }
                    },
                    {
                        label: 'Tuổi', name: 'nlshow', width: 80, align: 'left', formatter: function (cellvalue, options, rowObject) {
                            return getAgeFromBirthTime({ begin: rowObject.csny }).text;
                        }
                    },
                    { label: 'Số giấy tờ', name: 'zjh', width: 120, align: 'left' },
                    { label: 'Số thẻ', name: 'kh', width: 80, align: 'left' },
                    { label: 'Loại thẻ', name: 'CardTypeName', width: 80, align: 'left' },
                    { label: 'Tính chất bệnh nhân', name: 'brxzmc', width: 80, align: 'left' },
                        { label: 'CardType', name: 'CardType', width: 80, align: 'left', hidden: true, },
                    { label: 'CardId', name: 'CardId', width: 80, align: 'left', hidden: true, },
                        { label: '', name: 'CreateTime', hidden: true }
        ],
        pager: "#gridPager",
        sortname: 'CreateTime desc',
        viewrecords: true,
        gridComplete: function () {
            var rowIds = $gridList.jqGrid('getDataIDs');
            var exsistzf = false;
            if (rowIds.length > 0) {
                $.each(rowIds, function (index, value) {
                    var cardtype = $gridList.jqGrid('getRowData', rowIds[index]).CardType;
                    if (cardtype==="1")//Đã tồn tại thẻ ảo chưa
                    {
                        exsistzf = true;
                        return;
                    }
                });
               
                var rowData = $gridList.jqGrid('getRowData', rowIds[0]);
                var refkh = '';var refblh='';
                $.najax({
                    url: "/PatientManage/HospiterRes/GetFormJson?T=" + new Date(),
                    data: { "keyValue": rowData.patid },
                    dataType: 'json',
                    async: false,
                    success: function (rep) {
                        if (rep.state !== 'error') {
                            refkh = $("#kh").val(); refblh = $("#blh").val();
                            $("#form1").formSerialize(rep);
                            brxzinit();
                            setTimeout(function () {
                                getcitycode(rep);
                            }, 2000);
                        }
                    }
                });
                if (exsistzf) {
                    $("#divPatCard").css('display', 'block');
                    $.modalAlert("Số giấy tờ hiện tại đã có thông tin thẻ khám, vui lòng chọn số thẻ khám trong danh sách bên dưới", "error");
                } else {
                    cz = "SaveFrom";
                    $('#kh').val(refkh);
                    $('#blh').val(refblh);
                }
            }
            else {
                $("#divPatCard").css('display', 'none');
                //$("#form1")[0].reset();//Khởi tạo
                $("#patid").val("");
                //Gán đối chiếu tính chất bệnh nhân
                brxzinit();
                $.ajax({
                    url: "/PatientManage/HospiterRes/GetNewCardNoAndBLH",
                    data: { khsc: khsc, blhsc: blhsc },
                    dataType: "json",
                    async: false,
                    cache: false,
                    success: function (data) {
                        $('#kh').val(data.kh);
                        $('#blh').val(data.blh);
                    }
                });
               
            }
        },
    });
    $("#divPatCard").css('display', 'none');
}

function brxzinit()
{
    //Gán đối chiếu tính chất bệnh nhân
    var ybfyxzCompResult = {
        Code: '0',
        Name: 'Tu chi tra'
    };
    $("#brxzmc").val(ybfyxzCompResult.Name).attr("data-label", ybfyxzCompResult.Code);
    $('#cardtype').val(xnkCardType);//Mặc định thẻ ảo
}

function submitForm(callback) {
    debugger
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data = savecitycode(data);
        data.brxz = $("#brxzmc").attr("data-label");
        data.yktbz = data.patid && !fromly ? "yktcardregister" : fromly; //yktregister: cập nhật thông tin cơ bản đồng thời thêm mới thông tin thẻ
        if (openYbSett == 'True') {
            // Đã bật giao dịch BHYT: kiểm tra 'thẻ không phải BHYT thì không được chọn tính chất phí BHYT'
            // Song ngữ: DB xt_brxz đã dịch sang tiếng Việt ở Scope A nên check TQ cũ chết (luôn false/true) — giữ check TQ + thêm check Việt.
            if (data.cardtype !== ybkCardType
                && ($("#brxzmc").val().indexOf('医保') != -1 || $("#brxzmc").val().indexOf('Bao hiem y te') != -1 || $("#brxzmc").val().indexOf('工伤') != -1 || $("#brxzmc").val().indexOf('Tai nan lao dong') != -1 || $("#brxzmc").val().indexOf('生育') != -1)) {
                $.modalAlert("Bệnh nhân không phải BHYT, không được chọn tính chất phí BHYT", 'error');
                return false;
            }
            //Đã bật giao dịch BHYT: kiểm tra 'thẻ BHYT thì chỉ được chọn tính chất phí BHYT'
            if (data.cardtype === ybkCardType
                && ($("#brxzmc").val().indexOf('医保') == -1 && $("#brxzmc").val().indexOf('Bao hiem y te') == -1 && $("#brxzmc").val().indexOf('工伤') == -1 && $("#brxzmc").val().indexOf('Tai nan lao dong') == -1 && $("#brxzmc").val().indexOf('生育') == -1)) {
                $.modalAlert("Bệnh nhân BHYT, chỉ được chọn tính chất phí BHYT", 'error');
                return false;
            }
        }
        if (!data.xb) {
            $.modalAlert("Vui lòng chọn giới tính!", 'warning');
            return false;
        }
        $.submitForm({
            url: "/PatientManage/HospiterRes/SubmitForm",
            param: { PatientBasicAndCardInfoVO: data },
            success: function () {
                
                var parent = $.request("parent");
                if (parent && parent === 'patientlist') {
                    //Kích hoạt từ trang danh sách bệnh nhân
                    $.currentWindow().$("#gridList").resetSelection();
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                    return;
                }
                else if ((parent && parent === 'cisframe') || parent==="OutChargeRegister") {
                    //Từ trang sửa nhúng trong CIS
                    // return;
                    callback({ kh: data.kh, cardtype: data.cardtype,brxz:data.brxz,zjh:data.zjh});
                }
                else if (parent && parent === 'ZYZfToYb') {
                    ///Nhập viện chuyển từ tự phí sang BHYT
                    $.currentWindow().CallbackPatientQuery(ajaxref.data);
                }
                else {
                    //Kích hoạt từ trang đăng ký
                    
                    $.currentWindow().CallbackPatientQuery({ kh: data.kh, cardtype: data.cardtype });
                }
            }
        });
    }
}

function valiPatZjh()
{   
    if ($("#zjh").val())
    {
        if (fromly || readbz!=="1") { //Trang thẻ khám bệnh nhân và đọc thẻ BHYT: bỏ tải danh sách thẻ khám
            return;
        }
        var $gridList = $("#patGridList");
        $gridList.jqGrid('setGridParam', {
            url: "/OutpatientManage/OutpatientAccounting/PatSearchInfo",
            postData: { zjh: $("#zjh").val(),zjlx:'1' },
        }).trigger('reloadGrid');
    }
}
var theAcceptClickCallBack = null;
function AcceptClick(callBack) {

    theAcceptClickCallBack = callBack;
    
    var $gridList = $("#patGridList");
    var rowIds = $gridList.jqGrid('getDataIDs');
    if (rowIds.length == 0||cz=="SaveFrom") {
        submitForm();
    }
    else { 
    var blh = $gridList.jqGridRowValue().blh;
    if (!!!blh) {
        $.modalAlert("Chưa chọn bản ghi thẻ khám nào", "error");
        return;
    }
    var obj = new Object();
    obj.blh = blh;
    obj.nlshow = $gridList.jqGridRowValue().nlshow;
    obj.xm = $gridList.jqGridRowValue().xm;
    obj.patid = $gridList.jqGridRowValue().patid;
    obj.startTime = $.currentWindow().$("#ks").val();
    obj.endTime = $.currentWindow().$("#js").val();
    obj.CardId = $gridList.jqGridRowValue().CardId;
    obj.CardType = $gridList.jqGridRowValue().CardType;
    callBack(obj);
    }
}

//Xác nhận, trang chính gọi
function PatSearchConfirm() {
    AcceptClick();
}
//jqGrid: double-click chọn một dòng
function btn_edit() {
    PatSearchConfirm();
}
function checkNotNull() {
    //Số thẻ
    var kh = $("#kh").val();
    if (!kh && fromly != "yktmodify") {
        $.modalAlert("Vui lòng nhập số thẻ!", 'warning');
        return false;
    }
    //Số bệnh án
    var blh = $("#blh").val();
    if (!blh && fromly != "yktmodify") {
        $.modalAlert("Vui lòng nhập số bệnh án!", 'warning');
        return false;
    }
    //Họ tên
    var xm = $("#xm").val();
    if (!xm) {
        $.modalAlert("Vui lòng nhập họ tên!", 'warning');
        return false;
    }
    //CMND/CCCD
    var zjh = $("#zjh").val();
    if (!zjh) {
        $.modalAlert("Vui lòng nhập số CMND/CCCD!", 'warning');
        return false;
    }

    //Tính chất bệnh nhân
    var brxzmc = $("#brxzmc").val();
    if (!brxzmc && fromly != "yktmodify") {
        $.modalAlert("Vui lòng chọn tính chất bệnh nhân!", 'warning');
        return false;
    }

    //Loại giấy tờ
    var zjlx = $("#zjlx").val();
    if ((zjlx === "" || !zjlx) && zjpz == 'ON') {
        $.modalAlert("Vui lòng chọn loại giấy tờ!", 'warning');
        return false;
    }
    var zjh = $("#zjh").val();
    if (!!!zjh && zjpz == 'ON') {
        $.modalAlert("Vui lòng nhập số giấy tờ!", 'warning');
        return false;
    }
    ////Tạm bỏ xác thực CMND/CCCD ----lixin 20190816
    //if (!checkCard()) {
    //    return false;
    //}
    //Ngày tháng năm sinh
    var csny = $("#csny").val();
    if (csny == null || csny === "" || csny == undefined) {
        $.modalAlert("Vui lòng nhập ngày tháng năm sinh!", 'warning');
        return false;
    }
    //Phiên âm
    var py = $("#py").val();
    if (!py) {
        if (!!$("#xm").val()) {
            py = $("#xm").toShouPin();
        }
        if (!py) {
            $.modalAlert("Phiên âm trống!", 'warning');
            return false;
        }
        else {
            $("#py").val(py);
        }
    }
    //Giới tính
    var xb = false;
    $('input[name="xb"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            xb = true;
            $("#xb").val();
        }
    });
    if (!xb) {
        $.modalAlert("Vui lòng chọn giới tính!", 'warning');
        return false;
    }
    //Nghề nghiệp
    var zy = $("#zy").val();
    //if (zy === "" || !zy) {
    //    $.modalAlert("Vui lòng chọn nghề nghiệp!", 'warning');
    //    return false;
    //}
    //Địa chỉ
    //if (!$('#xian_dz').val()) {
    //    $.modalAlert("Vui lòng nhập địa chỉ hiện tại!", 'warning');
    //    return false;
    //}

    //Người liên hệ khẩn cấp
    var nlshowdw = $('#nlshowdw option:selected').val();
    var nlshow = $('#nlshow').val();
    if (nlshowdw === '1' && nlshow < 14) {
        if (!$('#jjllr').val()) {
            $.modalAlert("Dưới 14 tuổi vui lòng nhập người liên hệ khẩn cấp!", 'warning');
            return false;
        }
        if (!$('#jjlldh').val()) {
            $.modalAlert("Dưới 14 tuổi vui lòng nhập điện thoại liên hệ khẩn cấp!", 'warning');
            return false;
        }
    }

    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            //hf: { required: true },
            dh: { isPhone: true },
            email: { email: true },
            phone: { isMobile: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            //hf: { required: "Vui lòng chọn tình trạng hôn nhân!" },
            dh: { isPhone: "Định dạng điện thoại không đúng!" },
            email: { email: "Định dạng email không đúng!" },
            phone: { isMobile: "Định dạng điện thoại di động không đúng!" },
            jjlldh: { isPhone: "Định dạng điện thoại không đúng!" }
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

    return true;
}
var vcity = {
    11: "Bắc Kinh", 12: "Thiên Tân", 13: "Hà Bắc", 14: "Sơn Tây", 15: "Nội Mông",
    21: "Liêu Ninh", 22: "Cát Lâm", 23: "Hắc Long Giang", 31: "Thượng Hải", 32: "Giang Tô",
    33: "Chiết Giang", 34: "An Huy", 35: "Phúc Kiến", 36: "Giang Tây", 37: "Sơn Đông", 41: "Hà Nam",
    42: "Hồ Bắc", 43: "Hồ Nam", 44: "Quảng Đông", 45: "Quảng Tây", 46: "Hải Nam", 50: "Trùng Khánh",
    51: "Tứ Xuyên", 52: "Quý Châu", 53: "Vân Nam", 54: "Tây Tạng", 61: "Thiểm Tây", 62: "Cam Túc",
    63: "Thanh Hải", 64: "Ninh Hạ", 65: "Tân Cương", 71: "Đài Loan", 81: "Hồng Kông", 82: "Ma Cao", 91: "Nước ngoài"
};

//check CMND/CCCD
checkCard = function () {
    if ($('#zjlx').val() !== "1") {
        //Loại giấy tờ không phải CMND/CCCD
        return true;
    }
    var card = $("#zjh").val().toUpperCase();
    if (!!!card) {
        if (zjpz == 'ON') {
            $.modalAlert("Vui lòng nhập số giấy tờ!", 'warning');
            return false;
        }
        else if (zjpz == 'OFF') {
            return true;
        }
    }
    $("#zjh").val(card);
    //Có trống không  
    if (!!!card) {
        $.modalAlert('Vui lòng nhập số CMND/CCCD, số CMND/CCCD không được để trống', 'warning');
        // $("#zjh").focus;
        return false;
    }
    //Kiểm tra độ dài, loại  
    if (isCardNo(card) === false) {
        $.modalAlert('Số CMND/CCCD bạn nhập không đúng, vui lòng nhập lại', 'warning');
        //$("#zjh").focus;
        return false;
    }
    //Kiểm tra tỉnh  
    if (checkProvince(card) === false) {
        $.modalAlert('2 ký tự đầu của số CMND/CCCD bạn nhập không đúng, vui lòng nhập lại', 'warning');
        //$("#zjh").focus;
        return false;
    }
    //Kiểm tra ngày sinh  
    if (checkBirthday(card) === false) {
        $.modalAlert('Ngày sinh trong số CMND/CCCD bạn nhập không đúng, vui lòng nhập lại', 'warning');
        //$("#zjh").focus();
        return false;
    }
    //Kiểm tra ký tự kiểm tra  
    if (checkParity(card) === false) {
        $.modalAlert('Ký tự kiểm tra trong số CMND/CCCD của bạn không đúng, vui lòng nhập lại', 'warning');
        // $("#zjh").focus();
        return false;
    }
    return true;
};


//Kiểm tra số có đúng quy chuẩn không, gồm độ dài và loại  
isCardNo = function (card) {
    //Số CMND/CCCD 15 hoặc 18 ký tự; 15 ký tự thì toàn số, 18 ký tự thì 17 ký tự đầu là số, ký tự cuối là ký tự kiểm tra, có thể là số hoặc chữ X  
    var reg = /(^\d{15}$)|(^\d{17}(\d|X)$)/;
    if (reg.test(card) === false) {
        return false;
    }

    return true;
};

//Lấy 2 ký tự đầu của CMND/CCCD, kiểm tra tỉnh  
checkProvince = function (card) {
    var province = card.substr(0, 2);
    if (vcity[province] == undefined) {
        return false;
    }
    return true;
};

//Kiểm tra ngày sinh có đúng không  
checkBirthday = function (card) {
    var len = card.length;
    //CMND/CCCD 15 ký tự: thứ tự tỉnh (3) thành phố (3) năm (2) tháng (2) ngày (2) ký tự kiểm tra (3), đều là số  
    if (len == '15') {
        var re_fifteen = /^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/;
        var arr_data = card.match(re_fifteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date('19' + year + '/' + month + '/' + day);
        return verifyBirthday('19' + year, month, day, birthday);
    }
    //CMND/CCCD 18 ký tự: thứ tự tỉnh (3) thành phố (3) năm (4) tháng (2) ngày (2) ký tự kiểm tra (4), ký tự kiểm tra cuối có thể là X  
    if (len == '18') {
        var re_eighteen = /^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/;
        var arr_data = card.match(re_eighteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date(year + '/' + month + '/' + day);
        return verifyBirthday(year, month, day, birthday);
    }
    return false;
};

//Kiểm tra ngày  
verifyBirthday = function (year, month, day, birthday) {
    var now = new Date();
    var now_year = now.getFullYear();
    //Năm tháng ngày có hợp lý không  
    if (birthday.getFullYear() == year && (birthday.getMonth() + 1) == month && birthday.getDate() == day) {
        //Xét phạm vi năm (từ 3 đến 100 tuổi)  
        //Xét phạm vi năm (từ 1 đến 150 tuổi)  
        var time = now_year - year;
        //if (time >= 3 && time <= 100) {
        if (time >= 1 && time <= 150) {
            return true;
        }
        return false;
    }
    return false;
};

//Kiểm tra ký tự kiểm tra  
checkParity = function (card) {
    return true;
    //Chuyển 15 ký tự sang 18 ký tự  
    card = changeFivteenToEighteen(card);
    sfzh = card;
    var len = card.length;
    if (len == '18') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
        var cardTemp = 0, i, valnum;
        for (i = 0; i < 17; i++) {
            cardTemp += card.substr(i, 1) * arrInt[i];
        }
        valnum = arrCh[cardTemp % 11];
        if (valnum == card.substr(17, 1)) {
            return true;
        }

        return false;
    }
    return false;
};

//Chuyển số CMND/CCCD 15 ký tự sang 18 ký tự  
changeFivteenToEighteen = function (card) {
    if (card.length == '15') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
        var cardTemp = 0, i;
        card = card.substr(0, 6) + '19' + card.substr(6, card.length - 6);
        for (i = 0; i < 17; i++) {
            cardTemp += card.substr(i, 1) * arrInt[i];
        }
        card += arrCh[cardTemp % 11];
        return card;
    }
    return card;
};
