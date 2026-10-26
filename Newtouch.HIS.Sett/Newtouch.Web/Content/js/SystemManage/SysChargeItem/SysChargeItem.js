$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysChargeItem/GetsfGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "Khóa chính", name: "sfxmbh", hidden: true, key: true },
            { label: "Mã", name: "sfxm", width: 80, align: 'left' },
            { label: 'Hạng mục thu phí', name: 'sfxmmc', width: 100, align: 'left' },
            { label: 'Phiên âm', name: 'py', width: 50, align: 'left' },
            { label: 'Đơn vị', name: 'dw', width: 50, align: 'left' },
            { label: 'Đơn giá', name: 'dj', width: 50, align: 'left' },
            { label: 'Nhóm lớn', name: 'dlmc', width: 100, align: 'left' },
            { label: 'Phân loại', name: 'flmc', width: 100, align: 'left' },
            { label: 'Tỷ lệ tự chịu', name: 'zfbl', width: 50, align: 'left' },
            { label: 'Mã bảo hiểm y tế', name: 'ybdm', width: 100, align: 'left' },
            { label: 'Mã giá', name: 'wjdm', width: 100, align: 'left' },
             {
                 label: 'Phạm vi', name: 'mzzybz', width: 80, align: 'left',
                 formatter: function (cellvalue) {
                     if (cellvalue == "0") {
                         return "Chung";
                     } else if (cellvalue == "1") {
                         return "Khám ngoại trú";
                     } else if (cellvalue == "2") {
                         return "Nội trú";
                     } else {
                         return "";
                     }
                 }
             }, {
                 label: 'Thực hiện', name: 'ssbz', width: 50, align: 'left',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return "是"
                     } else {
                         return "否"
                     }
                 }
             },
{
    label: 'Đặc biệt', name: 'tsbz', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "是"
        } else {
            return "否"
        }
    }
},
{
    label: 'Cảnh báo', name: 'jsbz', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "是"
        } else {
            return "否"
        }
    }
},
{ label: 'Hạng mục thu phí', name: 'sfbz', width: 50, align: 'left' },
{
    label: 'Có hiệu lực', name: 'zt', width: 50, align: 'left',
    formatter: function (cellvalue) {
        if (cellvalue == 0) {
            return "Không hiệu lực"
        } else {
            return "Có hiệu lực"
        }
    }
}],
        pager: "#gridPager",
        sortname: 'CreateTime desc',
        rowNum: '10',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "Bảo trì hạng mục thu phí",
        url: "/SystemManage/SysChargeItem/Form",
        width: "800px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysChargeItem/Form?keyValue=" + keyValue,
        width: "700px",
        height: "520px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysChargeItem/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxmbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxmbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysChargeItem/Form?keyValue=" + keyValue,
        width: "700px",
        height: "560px",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysChargeItem/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").resetSelection();
            $.currentWindow().window.frames['iframeExpenseManage'].$("#gridList").trigger("reloadGrid");
            $.loading(false);
        }
    })
}