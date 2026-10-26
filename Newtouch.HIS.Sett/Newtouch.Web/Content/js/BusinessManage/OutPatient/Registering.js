$(function () {
    registeringListNew([]);
});

function registeringListNew(mydata) {
    var $registeringList = $("#registeringList");
    $registeringList.dataNewGrid({  
        height: 142,
        colModel: [
            { label: 'Loại đăng ký khám', name: 'ghlx', width: 100, algin: 'left' },
            { label: 'Mã tính chất bệnh nhân', name: 'brxzbh', hidden: true, width: 100, algin: 'left' },
            { label: 'Loại bệnh nhân', name: 'brxz', width: 100, algin: 'left' },
            { label: 'Hạng mục bệnh nặng', name: 'dbxm', width: 100, algin: 'left' },
            { label: 'Phí đăng ký khám', name: 'ghf', width: 100, algin: 'left' },
            { label: 'Phí khám chữa bệnh', name: 'zlf', width: 100, algin: 'left' },
            { label: 'Phí thẻ từ', name: 'ckf', width: 100, algin: 'left' },
            { label: 'Phí làm thẻ', name: 'gbf', width: 100, algin: 'left' },
            { label: 'Tổng số tiền', name: 'totalfees', width: 100, algin: 'left' },
        ]
    }, mydata);
    if (mydata.length > 0)
    {
        $('.ui-jqgrid-bdiv').find(".unwritten").remove();
    }


}