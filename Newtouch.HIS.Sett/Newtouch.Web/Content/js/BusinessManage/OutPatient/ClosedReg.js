$(function (){
    closedRegList();
})
function closedRegList() {
    var isJz = false;
    if ($("#sel_mzlx").val() == 3) {
        isJz = true;    //Cấp cứu
    }
    //var isJs = true;  //nghĩa là yêu cầu đã quyết toán
    var $closedRegList = $("#closedRegList");
    $closedRegList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/BusinessManage/OutPatient/GetSettledFeesList",
        height: 142,
        colModel: [
            { label: 'STT', name: '', width: 100, algin: 'left' },
            { label: 'Mã nội bộ đăng ký khám', name: 'ghnm', hidden: true, width: 100, algin: 'left' },
            { label: 'Loại đăng ký khám', name: 'sfxmmc', width: 100, algin: 'left' },
            { label: 'Loại bệnh nhân', name: 'brxzmc', width: 100, algin: 'left' },
            { label: 'Hạng mục bệnh nặng', name: 'dbxmmc', width: 100, algin: 'left' },
            { label: 'Tổng số tiền', name: 'zje', width: 100, algin: 'left' },
            { label: 'Đã hủy số khám hay chưa', name: 'isth', width: 100, algin: 'left' },
            { label:'Có trả thẻ từ hay không/Phí làm thẻ',id:'isReturnAll', formatter: "checkbox" }
        ]
    });

    $("#linkclosedReg").click(function () {
        //Mỗi lần nhấntab页，Điều chỉnhGridChiều rộng
        initLayout("MyTabGrid");

        $closedRegList.jqGrid('setGridParam', {
            postData: { kh: $("#txtkh").val(), isJz: isJz },
        }).trigger('reloadGrid');
    });

}