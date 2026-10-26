var keyValue = $.request("");
var fromly = $.request("");
var readbz = $.request("");//读卡标识 读卡类取消身份证验证是否存在一卡通信息
var cz = "";//Xác nhận thao tác
var sfzh;
var cityref;
$(function () {
    if ($.request("") === '') {
        fromly = "";
    }
    if (fromly === "") {
        $('').remove();
        $('').show();
    }
    else {
        $('').show();
        $('').remove();
    }
   
    initControl();
    gridInit();
    $('').tab('');
    //Phiên âm lấy họ tên theo
    $('').keyup(function () {
        $('').val($(this).toShouPin());

    });

    $("").keyup(function () {
        var nlshowText = $(this).val();
        if (!isNaN(nlshowText) && parseInt(nlshowText) > 0) {
            //是数字
            date = new Date();
            if ($('').val() == '') {
                //Năm
                var tempGetYearDate = function (year) {
                    var time = new Date();
                    time.setYear(time.getFullYear() + year);
                    return $('').attr('') == '' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("").val(tempGetYearDate(age));
            }
            else if ($('').val() == '') {
                //Tháng
                var tempGetMonthDate = function (month) {
                    var time = new Date();
                    time.setMonth(time.getMonth() + month);
                    return $('').attr('') == '' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("").val(tempGetMonthDate(age));
            }
            else if ($('').val() == '') {
                //Ngày
                var tempGetDayDate = function (day) {
                    var time = new Date();
                    time.setDate(time.getDate() + day);//LấyDayngày của sau ngày 
                    return $('').attr('') == '' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("").val(tempGetDayDate(age));
            }
            else if ($('').val() == '') {
                //Giờ
                var tempGetHourDate = function (hour) {
                    var date1 = new Date().getTime();
                    var date2 = date1 + hour * 60 * 60 * 1000;
                    var newDate = new Date(date2);
                    return $('').attr('') == '' ? $.getDate({ date: newDate }) : $.getTime({ date: newDate });
                }
                age = 0 - parseInt(nlshowText);
                $("").val(tempGetHourDate(age));
            }
        }
    });

    $('').bind("",

        function (e) {
            if (((e.keyCode === 13 && e.type === "") || e.type === "") && $('').val() === "") {
                //số chứng minh nhân dân lấy nhập
                var sfzh = $(this).val();
                var len = $(this).val().length;
                //if (len == 18 || len == 15) {//checkCard()
                if (len === 18 || len === 15) {
                    var csrq;
                    csrq = sfzh.substr(6, 8);
                    if ((len === 15 && csrq) || len === 18) {
                        csrq = csrq.replace(/(.{4})(.{2})/, "");
                        //Lấy giới tính
                        var xb;
                        if (parseInt(sfzh.charAt(16) / 2) * 2 != sfzh.charAt(16))
                            xb = 1;
                        else
                            xb = 0;
                        //$('').val(sfzh);
                        $('').val($('').attr('') == '' ? $.getDate({ date: csrq }) : $.getTime({ date: csrq }));
                        $("").parent().removeClass('');
                        $("").removeAttr('');
                        if (xb == 0) {
                            //$("").attr("", ''); //Nữ
                            //$("").parent().addClass('');
                            //$('').val("");
                            $("").trigger(''); //Nữ

                        }
                        else {
                            //$("").attr("", ''); //Nam
                            //$("").parent().addClass('');
                            //$('').val("");

                            $("").trigger(''); //Nam
                        }
                        setAge($('').val());
                    }
                }
            }
        });
    $("").keyupEnterEvent(function () {
        submitForm();
    });
    $('').change(function () {
        $('').trigger('');
    });
});

function setAge(strBirthday) {
    var theAge = getAgeFromBirthTime({ begin: strBirthday });
    if (!!theAge.text && !!theAge.nldw) {
        $("").val(theAge.nl);
        $('').val(theAge.nldw); //
    }
}

function initControl() {
    ///Chính sách hoàn trả
    $("").newtouchBatchFloatingSelector({
        height: 200,
        width: 400,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，Dùngkeywordđể lọc ra kết quả
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
        caption: "",
        colModel: [
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25 },
            { label: '', name: '', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            if ($thistr.attr('') === "") {
                //Đánh giá có đọc thẻ cần không
                if ($("").val() === "" || $("").val() === "") {
                    $.modalAlert("", '');
                    return;
                }
            }
            $("").attr("", $thistr.attr(''));
            $("").val($thistr.attr(''));
            return;
        }
    });
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr;
        }
    });
    //最佳联系方式
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr;
        }
    });
    //Quốc tịch
    $("").newtouchBindSelect({
        //id: "",
        //text: "",
        datasource: function () {
            var resultObjArr = new Array();
            if (top.window.clients.sysNationalityList) {
                $.each(top.window.clients.sysNationalityList, function (idx, val) {
                    
                    if (val.gjCode == "") {
                        $("").append('' + val.gjCode + '' + val.gjmc + '');
                    } else {
                        $("").append('' + val.gjCode + '' + val.gjmc + '');
                    }
                    //resultObjArr.push(val);
                });
            }
            return resultObjArr;
        }
    });
    //Dân tộc
    $("").newtouchBindSelect({
        //id: "",
        //text: "",
        datasource: function () {
            var resultObjArr = new Array();
            if (top.window.clients.sysNationList) {
                $.each(top.window.clients.sysNationList, function (idx, val) {
                    if (val.mzCode == "") {
                        $("").append('' + val.mzCode + '' + val.mzmc + '');
                    } else {
                        $("").append('' + val.mzCode + '' + val.mzmc + '');
                    }

                    //resultObjArr.push(val);
                });
            }
            return resultObjArr;
        }
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
    //病人来源
    $("").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems(''), function () {
                $('').append('' + this.Code + '' + this.Name + '');
            });
            return resultObjArr;
        }
    });
    //Hôn nhân
    //$("").newtouchBindSelect({
    //    datasource: function () {
    //        var resultObjArr = new Array();
    //        $.each($.itemDetails.getItems(''), function () {
    //            $('').append('' + this.Code + '' + this.Name + '');
    //        });
    //        return resultObjArr;
    //    }
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
    if (zjpz == '') {
        $("").text('');
    }

    //
    $('').bindSelect();
    $('').bindSelect();
    $('').attr('', '');

    $('').citys({
        required: false,
        nodata: '',
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

    $('').citys({
        required: false,
        nodata: '',
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

    $('').citys({
        required: false,
        nodata: '',
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
    $('').citys({
        required: false,
        nodata: '',
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
    if (fromly) { //非门诊挂号住院登记新建弹出 ，取消就诊卡列表初始化
        return;
    }
    var $gridList = $("");
    $gridList.dataGrid({
        //url: "",
        //postData: { zjh: $("").val(), },
        height: 100,
        //width: $("").width() - 10,
        caption: '',
        colModel: [
                    { label: '', name: '', hidden: true },
                    { label: '', name: '', width: 80, align: '' },
                    { label: '', name: '', width: 80, align: '' },
                    { label: '', name: '', hidden: true, width: 120, align: '' },
                    {
                        label: '', name: '', width: 80, align: '', formatter: function (cellvalue) {
                            return $.getGender(cellvalue);
                        }
                    },
                    {
                        label: '', name: '', width: 80, align: '', formatter: function (cellvalue, options, rowObject) {
                            return getAgeFromBirthTime({ begin: rowObject.csny }).text;
                        }
                    },
                    { label: '', name: '', width: 120, align: '' },
                    { label: '', name: '', width: 80, align: '' },
                    { label: '', name: '', width: 80, align: '' },
                    { label: '', name: '', width: 80, align: '' },
                        { label: '', name: '', width: 80, align: '', hidden: true, },
                    { label: '', name: '', width: 80, align: '', hidden: true, },
                        { label: '', name: '', hidden: true }
        ],
        pager: "",
        sortname: '',
        viewrecords: true,
        gridComplete: function () {
            var rowIds = $gridList.jqGrid('');
            var exsistzf = false;
            if (rowIds.length > 0) {
                $.each(rowIds, function (index, value) {
                    var cardtype = $gridList.jqGrid('', rowIds[index]).CardType;
                    if (cardtype==="")//是否已存在虚拟卡
                    {
                        exsistzf = true;
                        return;
                    }
                });
               
                var rowData = $gridList.jqGrid('', rowIds[0]);
                var refkh = '';var refblh='';
                $.najax({
                    url: "" + new Date(),
                    data: { "": rowData.patid },
                    dataType: '',
                    async: false,
                    success: function (rep) {
                        if (rep.state !== '') {
                            refkh = $("").val(); refblh = $("").val();
                            $("").formSerialize(rep);
                            brxzinit();
                            setTimeout(function () {
                                getcitycode(rep);
                            }, 2000);
                        }
                    }
                });
                if (exsistzf) {
                    $("").css('', '');
                    $.modalAlert("", "");
                } else {
                    cz = "";
                    $('').val(refkh);
                    $('').val(refblh);
                }
            }
            else {
                $("").css('', '');
                //$("")[0].reset();//Khởi tạo
                $("").val("");
                //Gán giá trị đối chiếu tính chất bệnh nhân
                brxzinit();
                $.ajax({
                    url: "",
                    data: { khsc: khsc, blhsc: blhsc },
                    dataType: "",
                    async: false,
                    cache: false,
                    success: function (data) {
                        $('').val(data.kh);
                        $('').val(data.blh);
                    }
                });
               
            }
        },
    });
    $("").css('', '');
}

function brxzinit()
{
    //Gán giá trị đối chiếu tính chất bệnh nhân
    var ybfyxzCompResult = {
        Code: '',
        Name: ''
    };
    $("").val(ybfyxzCompResult.Name).attr("", ybfyxzCompResult.Code);
    $('').val(xnkCardType);//默认虚拟卡
}

function submitForm(callback) {
    debugger
    var result = checkNotNull();
    if (result) {
        var data = $("").formSerialize();
        data = savecitycode(data);
        data.brxz = $("").attr("");
        data.yktbz = data.patid && !fromly ? "" : fromly; //yktregister 更新基本信息同时新增卡信息
        if (openYbSett == '') {
            //开启了医保交易 Đánh giá‘非医保卡，Không thể chọn tính chất chi phí bảo hiểm y tế’·
            if (data.cardtype !== ybkCardType
                && ($("").val().indexOf('') != -1 || $("").val().indexOf('') != -1 || $("").val().indexOf('') != -1)) {
                $.modalAlert("", '');
                return false;
            }
            //开启了医保交易 Đánh giá‘医保卡，只能选择医保费用性质’
            if (data.cardtype === ybkCardType
                && ($("").val().indexOf('') == -1 && $("").val().indexOf('') == -1 && $("").val().indexOf('') == -1)) {
                $.modalAlert("", '');
                return false;
            }
        }
        if (!data.xb) {
            $.modalAlert("", '');
            return false;
        }
        $.submitForm({
            url: "",
            param: { PatientBasicAndCardInfoVO: data },
            success: function () {
                
                var parent = $.request("");
                if (parent && parent === '') {
                    //来自患者列表页面触发
                    $.currentWindow().$("").resetSelection();
                    $.currentWindow().$("").trigger("");
                    return;
                }
                else if ((parent && parent === '') || parent==="") {
                    //来自CISTrang sửa này lồng nhau
                    // return;
                    callback({ kh: data.kh, cardtype: data.cardtype,brxz:data.brxz,zjh:data.zjh});
                }
                else if (parent && parent === '') {
                    ///住院自费转医保
                    $.currentWindow().CallbackPatientQuery(ajaxref.data);
                }
                else {
                    //来自登记页面触发
                    
                    $.currentWindow().CallbackPatientQuery({ kh: data.kh, cardtype: data.cardtype });
                }
            }
        });
    }
}

function valiPatZjh()
{   
    if ($("").val())
    {
        if (fromly || readbz!=="") { //患者一卡通页面及医保读卡取消就诊卡列表加载
            return;
        }
        var $gridList = $("");
        $gridList.jqGrid('', {
            url: "",
            postData: { zjh: $("").val(),zjlx:'' },
        }).trigger('');
    }
}
var theAcceptClickCallBack = null;
function AcceptClick(callBack) {

    theAcceptClickCallBack = callBack;
    
    var $gridList = $("");
    var rowIds = $gridList.jqGrid('');
    if (rowIds.length == 0||cz=="") {
        submitForm();
    }
    else { 
    var blh = $gridList.jqGridRowValue().blh;
    if (!!!blh) {
        $.modalAlert("", "");
        return;
    }
    var obj = new Object();
    obj.blh = blh;
    obj.nlshow = $gridList.jqGridRowValue().nlshow;
    obj.xm = $gridList.jqGridRowValue().xm;
    obj.patid = $gridList.jqGridRowValue().patid;
    obj.startTime = $.currentWindow().$("").val();
    obj.endTime = $.currentWindow().$("").val();
    obj.CardId = $gridList.jqGridRowValue().CardId;
    obj.CardType = $gridList.jqGridRowValue().CardType;
    callBack(obj);
    }
}

//Xác nhận Gọi từ trang chính
function PatSearchConfirm() {
    AcceptClick();
}
//jqGrid Nhấp đúp để chọn một dòng
function btn_edit() {
    PatSearchConfirm();
}
function checkNotNull() {
    //Số thẻ
    var kh = $("").val();
    if (!kh && fromly != "") {
        $.modalAlert("", '');
        return false;
    }
    //Số bệnh án
    var blh = $("").val();
    if (!blh && fromly != "") {
        $.modalAlert("", '');
        return false;
    }
    //Họ tên
    var xm = $("").val();
    if (!xm) {
        $.modalAlert("", '');
        return false;
    }
    //Chứng minh nhân dân
    var zjh = $("").val();
    if (!zjh) {
        $.modalAlert("", '');
        return false;
    }

    //Tính chất bệnh nhân
    var brxzmc = $("").val();
    if (!brxzmc && fromly != "") {
        $.modalAlert("", '');
        return false;
    }

    //Loại giấy tờ
    var zjlx = $("").val();
    if ((zjlx === "" || !zjlx) && zjpz == '') {
        $.modalAlert("", '');
        return false;
    }
    var zjh = $("").val();
    if (!!!zjh && zjpz == '') {
        $.modalAlert("", '');
        return false;
    }
    ////暂时去除身份验证----lixin 20190816
    //if (!checkCard()) {
    //    return false;
    //}
    //Năm sinh
    var csny = $("").val();
    if (csny == null || csny === "" || csny == undefined) {
        $.modalAlert("", '');
        return false;
    }
    //Phiên âm
    var py = $("").val();
    if (!py) {
        if (!!$("").val()) {
            py = $("").toShouPin();
        }
        if (!py) {
            $.modalAlert("", '');
            return false;
        }
        else {
            $("").val(py);
        }
    }
    //Giới tính
    var xb = false;
    $('').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("")) {
            xb = true;
            $("").val();
        }
    });
    if (!xb) {
        $.modalAlert("", '');
        return false;
    }
    //Nghề nghiệp
    var zy = $("").val();
    //if (zy === "" || !zy) {
    //    $.modalAlert("", '');
    //    return false;
    //}
    //Địa chỉ
    //if (!$('').val()) {
    //    $.modalAlert("", '');
    //    return false;
    //}

    //紧急联络人
    var nlshowdw = $('').val();
    var nlshow = $('').val();
    if (nlshowdw === '' && nlshow < 14) {
        if (!$('').val()) {
            $.modalAlert("", '');
            return false;
        }
        if (!$('').val()) {
            $.modalAlert("", '');
            return false;
        }
    }

    var validator = $('').validate();
    validator.settings = {
        rules: {
            //hf: { required: true },
            dh: { isPhone: true },
            email: { email: true },
            phone: { isMobile: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            //hf: { required: "" },
            dh: { isPhone: "" },
            email: { email: "" },
            phone: { isMobile: "" },
            jjlldh: { isPhone: "" }
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

    return true;
}
var vcity = {
    11: "", 12: "", 13: "", 14: "", 15: "",
    21: "", 22: "", 23: "", 31: "", 32: "",
    33: "", 34: "", 35: "", 36: "", 37: "", 41: "",
    42: "", 43: "", 44: "", 45: "", 46: "", 50: "",
    51: "", 52: "", 53: "", 54: "", 61: "", 62: "",
    63: "", 64: "", 65: "", 71: "", 81: "", 82: "", 91: ""
};

//checkChứng minh nhân dân
checkCard = function () {
    if ($('').val() !== "") {
        //Loại giấy tờ Chứng minh nhân dân không
        return true;
    }
    var card = $("").val().toUpperCase();
    if (!!!card) {
        if (zjpz == '') {
            $.modalAlert("", '');
            return false;
        }
        else if (zjpz == '') {
            return true;
        }
    }
    $("").val(card);
    //是否为空  
    if (!!!card) {
        $.modalAlert('', '');
        // $("").focus;
        return false;
    }
    //Kiểm tra độ dài，Loại  
    if (isCardNo(card) === false) {
        $.modalAlert('', '');
        //$("").focus;
        return false;
    }
    //Kiểm tra phần tỉnh  
    if (checkProvince(card) === false) {
        $.modalAlert('', '');
        //$("").focus;
        return false;
    }
    //校验生日  
    if (checkBirthday(card) === false) {
        $.modalAlert('', '');
        //$("").focus();
        return false;
    }
    //检验位的检测  
    if (checkParity(card) === false) {
        $.modalAlert('', '');
        // $("").focus();
        return false;
    }
    return true;
};


//检查号码是否符合规范，Bao gồm độ dài，Loại  
isCardNo = function (card) {
    //身份证号码为15位或者18Vị，15位时全为数字，18位前17位为数字，最后一位是校验位，Số cho có thể hoặc Ký tựX  
    var reg = /(^\d{15}$)|(^\d{17}(\d|X)$)/;
    if (reg.test(card) === false) {
        return false;
    }

    return true;
};

//取身份证前两位,Kiểm tra phần tỉnh  
checkProvince = function (card) {
    var province = card.substr(0, 2);
    if (vcity[province] == undefined) {
        return false;
    }
    return true;
};

//检查生日是否正确  
checkBirthday = function (card) {
    var len = card.length;
    //Chứng minh nhân dân15位时，Tỉnh cho thứ tự（3Vị）Thành phố（3Vị）Năm（2Vị）Tháng（2Vị）Ngày（2Vị）Chữ số kiểm tra（3Vị），皆为数字  
    if (len == '') {
        var re_fifteen = /^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/;
        var arr_data = card.match(re_fifteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date('' + year + '' + month + '' + day);
        return verifyBirthday('' + year, month, day, birthday);
    }
    //Chứng minh nhân dân18位时，Tỉnh cho thứ tự（3Vị）Thành phố（3Vị）Năm（4Vị）Tháng（2Vị）Ngày（2Vị）Chữ số kiểm tra（4Vị），校验位末尾可能为X  
    if (len == '') {
        var re_eighteen = /^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/;
        var arr_data = card.match(re_eighteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date(year + '' + month + '' + day);
        return verifyBirthday(year, month, day, birthday);
    }
    return false;
};

//Kiểm tra ngày  
verifyBirthday = function (year, month, day, birthday) {
    var now = new Date();
    var now_year = now.getFullYear();
    //年月日是否合理  
    if (birthday.getFullYear() == year && (birthday.getMonth() + 1) == month && birthday.getDate() == day) {
        //判断年份的范围（3岁到100岁之间)  
        //判断年份的范围（1岁到150岁之间)  
        var time = now_year - year;
        //if (time >= 3 && time <= 100) {
        if (time >= 1 && time <= 150) {
            return true;
        }
        return false;
    }
    return false;
};

//校验位的检测  
checkParity = function (card) {
    return true;
    //15位转18Vị  
    card = changeFivteenToEighteen(card);
    sfzh = card;
    var len = card.length;
    if (len == '') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('', '', '', '', '', '', '', '', '', '', '');
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

//15位转18位身份证号  
changeFivteenToEighteen = function (card) {
    if (card.length == '') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('', '', '', '', '', '', '', '', '', '', '');
        var cardTemp = 0, i;
        card = card.substr(0, 6) + '' + card.substr(6, card.length - 6);
        for (i = 0; i < 17; i++) {
            cardTemp += card.substr(i, 1) * arrInt[i];
        }
        card += arrCh[cardTemp % 11];
        return card;
    }
    return card;
};
