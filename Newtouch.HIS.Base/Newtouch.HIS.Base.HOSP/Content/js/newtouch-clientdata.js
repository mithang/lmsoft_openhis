var newtouchclients = [];
$(function () {
    //newtouchclients = $.newtouchclientsInit();
})
$.newtouchclientsInit = function () {
    var dataJson = {
    };
    var init = function () {
        $.najax({
            url: "/NewtouchClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;
                dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = data.sysStaffList;//Nhân sự
                dataJson.sysDepartList = data.sysDepartList,//Khoa
                dataJson.sysPatiAreaList = data.sysPatiAreaList,//Khu bệnh
                dataJson.sysNationalityList = data.sysNationalityList,//Quốc tịch
                dataJson.sysNationList = data.sysNationList,//Dân tộc
                //dataJson.sysChargeItemList = data.sysChargeItemList,//Hạng mục thu phí ghi sổ nội trú
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//Bác sĩ ngoại trú ghi sổ nội trú
                //dataJson.regScheduleList = data.regScheduleList//Lịch trực đăng ký khám
                //GetDYList
                //dataJson.organize = data.organize;
                //dataJson.role = data.role;
                //dataJson.duty = data.duty;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
                //dataJson.authorizeButton = data.authorizeButton;
                dataJson.SysFailedCodeMessageMapList = data.SysFailedCodeMessageMapList;//FailedExceptionQuan hệ ánh xạ gợi ý lỗi
            }
        });
    }
    init();
    return dataJson;
}