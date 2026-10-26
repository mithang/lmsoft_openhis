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
            url: "/NewtouchClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: true,
            cache: false,
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;//Tính chất bệnh nhân，Chính sách thanh toán BHYT
                //dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                //dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = eval(data.sysStaffList);//Nhân viên
                dataJson.sysDepartList = eval(data.sysDepartList),//Khoa
                dataJson.sysPatiAreaList = eval(data.sysPatiAreaList),//Khu bệnh
                dataJson.sysNationalityList = eval(data.sysNationalityList),//Quốc tịch
                dataJson.sysNationList = eval(data.sysNationList),//Dân tộc
                //dataJson.sysChargeItemList = data.sysChargeItemList,//Hạng mục thu phí ghi sổ nội trú
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//Bác sĩ khám ngoại trú ghi sổ nội trú
                dataJson.sysMajorClassList = eval(data.sysMajorClassList);//Nhóm lớn
                //dataJson.sysChargeItemList = data.sysChargeItemList;//Hạng mục thu phí
                //dataJson.sysChargeClassifyList = data.sysChargeClassifyList;//Phân loại thu phí
                //dataJson.syszlxmList = data.syszlxmList;//Hạng mục khám chữa bệnh
                dataJson.sysnbsfdlList = eval(data.sysnbsfdlList);//Nhóm lớn thu phí bảo hiểm nông nghiệp
                dataJson.sysbasfdlList = eval(data.sysbasfdlList);//Nhóm lớn hồ sơ bệnh án
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