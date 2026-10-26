var keyValue = $.request("keyValue");
var fromly = $.request("fromly");
var readbz = $.request("readbz");//Đọc thẻDấu hiệu Đọc thẻ类HủyChứng minh nhân dânXác minhCó hay khôngLưuTrongThẻ một cửaThông tin
var cz = "";//Xác nhậnThao tác
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
    //Lấy phiên âm theo họ tên
    $('#xm').keyup(function () {
        $('#py').val($(this).toShouPin());

    });

    $("#nlshow").keyup(function () {
        var nlshowText = $(this).val();
        if (!isNaN(nlshowText) && parseInt(nlshowText) > 0) {
            //是数字
            date = new Date();
            if ($('#nlshowdw').val() == '1') {
                //年
                var tempGetYearDate = function (year) {
                    var time = new Date();
                    time.setYear(time.getFullYear() + year);
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetYearDate(age));
            }
            else if ($('#nlshowdw').val() == '2') {
                //月
                var tempGetMonthDate = function (month) {
                    var time = new Date();
                    time.setMonth(time.getMonth() + month);
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetMonthDate(age));
            }
            else if ($('#nlshowdw').val() == '3') {
                //日
                var tempGetDayDate = function (day) {
                    var time = new Date();
                    time.setDate(time.getDate() + day);//LấyDay天后的Ngày 
                    return $('#csny').attr('data-dateFmt') == 'yyyy-MM-dd' ? $.getDate({ date: time }) : $.getTime({ date: time });
                }
                age = 0 - parseInt(nlshowText);
                $("#csny").val(tempGetDayDate(age));
            }
            else if ($('#nlshowdw').val() == '4') {
                //Khi
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
                //LấyNhập的Số CMND/CCCD
                var sfzh = $(this).val();
                var len = $(this).val().length;
                //if (len == 18 || len == 15) {//checkCard()
                if (len === 18 || len === 15) {
                    var csrq;
                    csrq = sfzh.substr(6, 8);
                    if ((len === 15 && csrq) || len === 18) {
                        csrq = csrq.replace(/(.{4})(.{2})/, "$1-$2-");
                        //LấyGiới tính
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
    ///Chính sách thanh toán BHYT
    $("#brxzmc").newtouchBatchFloatingSelector({
        height: 200,
        width: 400,
        clickautotrigger: true,
        filter: function (keyword) {
            //Duyệt nguồn dữ liệu，用keywordđể lọc ra kết quả
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
            { label: 'Mã số', name: 'brxz', widthratio: 25 },
            { label: 'Tên', name: 'brxzmc', widthratio: 25 },
            { label: 'Phiên âm', name: 'py', widthratio: 25 }
        ],
        itemdbclickhandler: function ($thistr) {
            if ($thistr.attr('data-brxz') === "8") {
                //XétCó hay khôngCầncầnĐọc thẻ
                if ($("#xnhylzh").val() === "" || $("#xnhgrbm").val() === "") {
                    $.modalAlert("Mới农合Bệnh nhânVui lòng trướcĐọc thẻ！", 'error');
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
    //最佳联系方式
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
    //Bệnh nhân来Nguồn
    $("#brly").newtouchBindSelect({
        datasource: function () {
            var resultObjArr = new Array();
            $.each($.itemDetails.getItems('PatientSource'), function () {
                $('#brly').append('<option value="' + this.Code + '">' + this.Name + '</option>');
            });
            return resultObjArr;
        }
    });
    //婚姻
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
    if (fromly) { //KhôngĐăng ký khám ngoại trúĐăng ký nội trúTạo mới弹出 ，HủyKhám bệnhDanh sách thẻKhởi tạo
        return;
    }
    var $gridList = $("#patGridList");
    $gridList.dataGrid({
        //url: "/OutpatientManage/OutpatientAccounting/PatSearchInfo",
        //postData: { zjh: $("#zjh").val(), },
        height: 100,
        //width: $("#divPatCard").width() - 10,
        caption: 'Bệnh nhânKhám bệnhDanh sách thẻ',
        colModel: [
                    { label: 'Khóa chính', name: 'patid', hidden: true },
                    { label: 'Số hồ sơ bệnh án', name: 'blh', width: 80, align: 'left' },
                    { label: 'Họ tên', name: 'xm', width: 80, align: 'left' },
                    { label: 'Tháng năm sinh', name: 'csny', hidden: true, width: 120, align: 'left' },
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
                    if (cardtype==="1")//Có hay khôngĐãLưuTrongThẻ ảo
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
                    $.modalAlert("Hiện tạiSố giấy tờĐãLưuTrongThẻ một cửaThông tin,Vui lòngĐã chọn下方Khám bệnhSố thẻ列表", "error");
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
        Name: 'Tự trả'
    };
    $("#brxzmc").val(ybfyxzCompResult.Name).attr("data-label", ybfyxzCompResult.Code);
    $('#cardtype').val(xnkCardType);//Mặc địnhThẻ ảo
}

function submitForm(callback) {
    debugger
    var result = checkNotNull();
    if (result) {
        var data = $("#form1").formSerialize();
        data = savecitycode(data);
        data.brxz = $("#brxzmc").attr("data-label");
        data.yktbz = data.patid && !fromly ? "yktcardregister" : fromly; //yktregister Cập nhậtThông tin cơ bản同KhiThêm mớiThông tin thẻ
        if (openYbSett == 'True') {
            //Đã bật giao dịch BHYT Xét‘KhôngThẻ BHYT，Không thể chọn tính chất chi phí BHYT’·
            if (data.cardtype !== ybkCardType
                && ($("#brxzmc").val().indexOf('Bảo hiểm y tế') != -1 || $("#brxzmc").val().indexOf('Tai nạn lao động') != -1 || $("#brxzmc").val().indexOf('Sinh đẻ') != -1)) {
                $.modalAlert("KhôngBảo hiểm y tếBệnh nhân，Không thể chọn tính chất chi phí BHYT", 'error');
                return false;
            }
            //Đã bật giao dịch BHYT Xét‘Thẻ BHYT，Chỉ được chọn tính chất chi phí BHYT’
            if (data.cardtype === ybkCardType
                && ($("#brxzmc").val().indexOf('Bảo hiểm y tế') == -1 && $("#brxzmc").val().indexOf('Tai nạn lao động') == -1 && $("#brxzmc").val().indexOf('Sinh đẻ') == -1)) {
                $.modalAlert("Bảo hiểm y tếBệnh nhân，Chỉ được chọn tính chất chi phí BHYT", 'error');
                return false;
            }
        }
        if (!data.xb) {
            $.modalAlert("Vui lòng chọn giới tính！", 'warning');
            return false;
        }
        $.submitForm({
            url: "/PatientManage/HospiterRes/SubmitForm",
            param: { PatientBasicAndCardInfoVO: data },
            success: function () {
                
                var parent = $.request("parent");
                if (parent && parent === 'patientlist') {
                    //来自Bệnh nhân列表页面Kích hoạt
                    $.currentWindow().$("#gridList").resetSelection();
                    $.currentWindow().$("#gridList").trigger("reloadGrid");
                    return;
                }
                else if ((parent && parent === 'cisframe') || parent==="OutChargeRegister") {
                    //来自CIS嵌套NàySửa页面
                    // return;
                    callback({ kh: data.kh, cardtype: data.cardtype,brxz:data.brxz,zjh:data.zjh});
                }
                else if (parent && parent === 'ZYZfToYb') {
                    ///Nội trúTự trả转Bảo hiểm y tế
                    $.currentWindow().CallbackPatientQuery(ajaxref.data);
                }
                else {
                    //来自Đăng ký页面Kích hoạt
                    
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
        if (fromly || readbz!=="1") { //Bệnh nhânThẻ một cửa页面及Bảo hiểm y tếĐọc thẻHủyKhám bệnhDanh sách thẻTải
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
        $.modalAlert("尚ChưaChọnMột dòngKhám bệnh卡记录", "error");
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

//Xác nhận 主页面调用
function PatSearchConfirm() {
    AcceptClick();
}
//jqGrid 双击Đã chọn某Dòng
function btn_edit() {
    PatSearchConfirm();
}
function checkNotNull() {
    //Số thẻ
    var kh = $("#kh").val();
    if (!kh && fromly != "yktmodify") {
        $.modalAlert("Vui lòng nhậpSố thẻ！", 'warning');
        return false;
    }
    //Số hồ sơ bệnh án
    var blh = $("#blh").val();
    if (!blh && fromly != "yktmodify") {
        $.modalAlert("Vui lòng nhậpSố hồ sơ bệnh án！", 'warning');
        return false;
    }
    //Họ tên
    var xm = $("#xm").val();
    if (!xm) {
        $.modalAlert("Vui lòng nhậpHọ tên！", 'warning');
        return false;
    }
    //Chứng minh nhân dân
    var zjh = $("#zjh").val();
    if (!zjh) {
        $.modalAlert("Vui lòng nhậpSố CMND/CCCD！", 'warning');
        return false;
    }

    //Tính chất bệnh nhân
    var brxzmc = $("#brxzmc").val();
    if (!brxzmc && fromly != "yktmodify") {
        $.modalAlert("Vui lòng chọn tính chất bệnh nhân！", 'warning');
        return false;
    }

    //Loại giấy tờ
    var zjlx = $("#zjlx").val();
    if ((zjlx === "" || !zjlx) && zjpz == 'ON') {
        $.modalAlert("Vui lòng chọnLoại giấy tờ！", 'warning');
        return false;
    }
    var zjh = $("#zjh").val();
    if (!!!zjh && zjpz == 'ON') {
        $.modalAlert("Vui lòng nhập số giấy tờ！", 'warning');
        return false;
    }
    ////暂Khi去除身份Xác minh----lixin 20190816
    //if (!checkCard()) {
    //    return false;
    //}
    //Tháng năm sinh
    var csny = $("#csny").val();
    if (csny == null || csny === "" || csny == undefined) {
        $.modalAlert("Vui lòng nhậpTháng năm sinh!", 'warning');
        return false;
    }
    //Phiên âm
    var py = $("#py").val();
    if (!py) {
        if (!!$("#xm").val()) {
            py = $("#xm").toShouPin();
        }
        if (!py) {
            $.modalAlert("Phiên âm为空！", 'warning');
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
        $.modalAlert("Vui lòng chọn giới tính！", 'warning');
        return false;
    }
    //Nghề nghiệp
    var zy = $("#zy").val();
    //if (zy === "" || !zy) {
    //    $.modalAlert("Vui lòng chọnNghề nghiệp ！", 'warning');
    //    return false;
    //}
    //Địa chỉ
    //if (!$('#xian_dz').val()) {
    //    $.modalAlert("Vui lòng nhập现Địa chỉ！", 'warning');
    //    return false;
    //}

    //紧急联络人
    var nlshowdw = $('#nlshowdw option:selected').val();
    var nlshow = $('#nlshow').val();
    if (nlshowdw === '1' && nlshow < 14) {
        if (!$('#jjllr').val()) {
            $.modalAlert("14岁以下Vui lòng nhập紧急联络人 ！", 'warning');
            return false;
        }
        if (!$('#jjlldh').val()) {
            $.modalAlert("14岁以下Vui lòng nhập紧急联络Điện thoại ！", 'warning');
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
            //hf: { required: "Vui lòng chọn婚否！" },
            dh: { isPhone: "Định dạng điện thoại không đúng！" },
            email: { email: "Email格式不Đang确！" },
            phone: { isMobile: "Điện thoại di động格式不Đang确！" },
            jjlldh: { isPhone: "Định dạng điện thoại không đúng！" }
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
    11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "Trong蒙古",
    21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "trên海", 32: "江苏",
    33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南",
    42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆",
    51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃",
    63: "青海", 64: "宁夏", 65: "Mới疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
};

//checkChứng minh nhân dân
checkCard = function () {
    if ($('#zjlx').val() !== "1") {
        //Loại giấy tờ KhôngChứng minh nhân dân
        return true;
    }
    var card = $("#zjh").val().toUpperCase();
    if (!!!card) {
        if (zjpz == 'ON') {
            $.modalAlert("Vui lòng nhập số giấy tờ！", 'warning');
            return false;
        }
        else if (zjpz == 'OFF') {
            return true;
        }
    }
    $("#zjh").val(card);
    //Có hay không为空  
    if (!!!card) {
        $.modalAlert('Vui lòng nhậpSố CMND/CCCD，Số CMND/CCCDKhông được trống', 'warning');
        // $("#zjh").focus;
        return false;
    }
    //Kiểm tra长度，Loại  
    if (isCardNo(card) === false) {
        $.modalAlert('BạnNhập的Số CMND/CCCD码不Đang确，Vui lòng nhập lại', 'warning');
        //$("#zjh").focus;
        return false;
    }
    //Kiểm traTỉnh  
    if (checkProvince(card) === false) {
        $.modalAlert('BạnNhập的Chứng minh nhân dân前两位不Đang确,Vui lòng nhập lại', 'warning');
        //$("#zjh").focus;
        return false;
    }
    //Kiểm traNgày sinh  
    if (checkBirthday(card) === false) {
        $.modalAlert('BạnNhập的Số CMND/CCCD码Ngày sinh不Đang确,Vui lòng nhập lại', 'warning');
        //$("#zjh").focus();
        return false;
    }
    //检验位的检测  
    if (checkParity(card) === false) {
        $.modalAlert('Bạn的Chứng minh nhân dânKý tự kiểm tra不Đang确,Vui lòng nhập lại', 'warning');
        // $("#zjh").focus();
        return false;
    }
    return true;
};


//Kiểm traSố码Có hay không符合规范，包括长度，Loại  
isCardNo = function (card) {
    //Số CMND/CCCD码为15位Hoặc18位，15ký tự thì全为数字，18位前17位为数字，最后一位是Ký tự kiểm tra，可能为数字Hoặc字符X  
    var reg = /(^\d{15}$)|(^\d{17}(\d|X)$)/;
    if (reg.test(card) === false) {
        return false;
    }

    return true;
};

//取Chứng minh nhân dân前两位,Kiểm traTỉnh  
checkProvince = function (card) {
    var province = card.substr(0, 2);
    if (vcity[province] == undefined) {
        return false;
    }
    return true;
};

//Kiểm traNgày sinhCó hay khôngĐang确  
checkBirthday = function (card) {
    var len = card.length;
    //Chứng minh nhân dân15ký tự thì，thứ tự là tỉnh（3位）市（3位）年（2位）月（2位）日（2位）Ký tự kiểm tra（3位），皆为数字  
    if (len == '15') {
        var re_fifteen = /^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/;
        var arr_data = card.match(re_fifteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date('19' + year + '/' + month + '/' + day);
        return verifyBirthday('19' + year, month, day, birthday);
    }
    //Chứng minh nhân dân18ký tự thì，thứ tự là tỉnh（3位）市（3位）年（4位）月（2位）日（2位）Ký tự kiểm tra（4位），Ký tự kiểm tra末尾可能为X  
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

//Kiểm traNgày  
verifyBirthday = function (year, month, day, birthday) {
    var now = new Date();
    var now_year = now.getFullYear();
    //Năm-tháng日Có hay không合理  
    if (birthday.getFullYear() == year && (birthday.getMonth() + 1) == month && birthday.getDate() == day) {
        //Xét phạm vi năm（3tuổi đến100tuổi)  
        //Xét phạm vi năm（1tuổi đến150tuổi)  
        var time = now_year - year;
        //if (time >= 3 && time <= 100) {
        if (time >= 1 && time <= 150) {
            return true;
        }
        return false;
    }
    return false;
};

//Ký tự kiểm tra的检测  
checkParity = function (card) {
    return true;
    //15ký tự chuyển18位  
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

//15ký tự chuyển18位Số CMND/CCCD  
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
