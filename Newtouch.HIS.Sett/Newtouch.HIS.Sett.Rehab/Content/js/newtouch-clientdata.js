var newtouchclients = [];
$(function () {
    //newtouchclients = $.newtouchclientsInit();
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
            success: function (data) {
                dataJson.sysPatientNatureList = data.sysPatientNatureList;//Tính chất bệnh nhân, chính sách hoàn trả
                dataJson.sysCashPay = data.sysCashPay;
                dataJson.SysForCashPayList = data.SysForCashPayList;
                dataJson.sysFieldList = data.sysFieldList;
                dataJson.sysStaffList = data.sysStaffList;//Nhân viên
                dataJson.sysDepartList = data.sysDepartList,//Khoa
                dataJson.sysPatiAreaList = data.sysPatiAreaList,//Khu bệnh
                dataJson.sysNationalityList = data.sysNationalityList,//Quốc tịch
                dataJson.sysNationList = data.sysNationList,//Dân tộc
                //dataJson.sysChargeItemList = data.sysChargeItemList,//Hạng mục thu phí ghi sổ nội trú
                dataJson.doctorInHosBookkeep = data.doctorInHosBookkeep//Bác sĩ ngoại trú ghi sổ nội trú
                dataJson.sysMajorClassList = data.sysMajorClassList;//Nhóm lớn
                dataJson.sysChargeItemList = data.sysChargeItemList;//Hạng mục thu phí
                dataJson.sysChargeClassifyList = data.sysChargeClassifyList;//Phân loại thu phí
                dataJson.syszlxmList = data.syszlxmList;//Hạng mục chẩn trị
                dataJson.sysnbsfdlList = data.sysnbsfdlList;//Nhóm lớn thu phí bảo hiểm nông nghiệp
                dataJson.sysbasfdlList = data.sysbasfdlList;//Nhóm lớn thu phí bệnh án
                //dataJson.regScheduleList = data.regScheduleList//Lịch trực đăng ký khám
                //GetDYList
                //dataJson.organize = data.organize;
                //dataJson.role = data.role;
                //dataJson.duty = data.duty;
                //dataJson.authorizeMenu = eval(data.authorizeMenu);
                //dataJson.authorizeButton = data.authorizeButton;
                dataJson.SysFailedCodeMessageMapList = data.SysFailedCodeMessageMapList;//Quan hệ ánh xạ thông báo lỗi FailedException
            }
        });
    }
    init();
    return dataJson;
}