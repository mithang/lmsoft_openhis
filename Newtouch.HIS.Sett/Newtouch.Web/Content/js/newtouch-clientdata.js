var newtouchclients = [];
$(function () {
    return;
    newtouchclients = $.newtouchclientsInit();
})
$.newtouchclientsInit = function () {
    var dataJson = {
    };
    var init = function () {
        $.ajax({
            url: "",
            type: "",
            dataType: "",
            async: true,
            cache: false,
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;//Tính chất bệnh nhân，Chính sách hoàn trả
                //dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                //dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = eval(data.sysStaffList);//Nhân viên
                dataJson.sysDepartList = eval(data.sysDepartList),//Khoa
                dataJson.sysPatiAreaList = eval(data.sysPatiAreaList),//Khu bệnh
                dataJson.sysNationalityList = eval(data.sysNationalityList),//Quốc tịch
                dataJson.sysNationList = eval(data.sysNationList),//Dân tộc
                //dataJson.sysChargeItemList = data.sysChargeItemList,//Nhập viện hạng mục thu phí ghi sổ
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//Nhập viện bác sĩ ngoại trú ghi sổ
                dataJson.sysMajorClassList = eval(data.sysMajorClassList);//Nhóm lớn
                //dataJson.sysChargeItemList = data.sysChargeItemList;//Hạng mục thu phí
                //dataJson.sysChargeClassifyList = data.sysChargeClassifyList;//收费分类
                //dataJson.syszlxmList = data.syszlxmList;//Hạng mục chẩn trị
                dataJson.sysnbsfdlList = eval(data.sysnbsfdlList);//Nhóm lớn thu phí bảo hiểm nông nghiệp
                dataJson.sysbasfdlList = eval(data.sysbasfdlList);//Nhóm lớn thu phí bệnh án
                dataJson.sysStaffDutyList = eval(data.sysStaffDutyList);//Bác sĩ，Huấn luyện viên sức khỏe
                //dataJson.regScheduleList = data.regScheduleList//Lịch trực đăng ký khám
                //GetDYList
                //dataJson.organize = data.organize;
                //dataJson.role = data.role;
                //dataJson.duty = data.duty;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
                //dataJson.authorizeButton = data.authorizeButton;
                dataJson.commercialInsuranceList = data.commercialInsuranceList;//Bảo hiểm thương mại
            }
        });
    }
    init();
    return dataJson;
}