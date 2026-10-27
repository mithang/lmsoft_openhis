$.fn.dutyStaffFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        url: '/SystemManage/Common/GetStaffListByDutyCode',
        width: 300,
        height: 200,
        clickautotrigger: true,
        ajaxparameters: function ($thisinput) {
            return "dutyCode=" + options.dutyCode + "&keyword=" + $thisinput.val();
        },
        colModel: [
            { label: 'StaffGh', name: 'StaffGh', hidden: true },
            { label: 'ks', name: 'ks', hidden: true },
            { label: 'Tên bác sĩ', name: 'StaffName', widthratio: 38 },
            { label: 'Mã gợi nhớ', name: 'StaffPY', widthratio: 20 },
            { label: 'Tên khoa', name: 'ksmc', widthratio: 40 },
        ],
        itemdbclickhandler: function ($thistr, $thisinput) {
            //bảo hiểmlưuthời giankiểmchứngvalvàdata-Namenhất quán
            $thisinput.attr('data-label', $thistr.attr('data-StaffGh'));
            $thisinput.attr('data-ks', $thistr.attr('data-ks'));
            $thisinput.attr('data-ksmc', $thistr.attr('data-ksmc'));
            $thisinput.val($thistr.attr('data-StaffName'));
        }
    });
}