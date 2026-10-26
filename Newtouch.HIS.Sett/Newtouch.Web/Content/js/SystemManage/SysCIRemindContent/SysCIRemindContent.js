$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        url: "/SystemManage/SysCIRemindContent/GetnrGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: "Khóa chính", name: "sfxjsnrbh", hidden: true, key: true },
            { label: 'Hạng mục thu phí', name: 'sfxmmc', width: 100, align: 'left' },
            { label: 'Nội dung cảnh báo khám ngoại trú', name: 'mzjsnr', width: 100, align: 'left' },
            { label: 'Nội dung cảnh báo nội trú', name: 'zyjsnr', width: 220, align: 'left' },
             {
                 label: 'Mức cảnh báo khám ngoại trú', name: 'mzjsjb', width: 50, align: 'left',
                 formatter: function (cellvalue) {
                     if (cellvalue == 0) {
                         return "Không nhắc";
                     } else if (cellvalue == 1) {
                         return "Nhắc thông thường";
                     } else if (cellvalue == 2) {
                         return "Cảnh báo";
                     } else if (cellvalue == 3) {
                         return "Cấm sử dụng";
                     }
                 }
             },
            {
                label: 'Mức cảnh báo nội trú', name: 'zyjsjb', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "Không nhắc";
                    } else if (cellvalue == 1) {
                        return "Nhắc thông thường";
                    } else if (cellvalue == 2) {
                        return "Cảnh báo";
                    } else if (cellvalue == 3) {
                        return "Cấm sử dụng";
                    }
                }
            },
            {
                label: 'Có hiệu lực', name: 'zt', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue == 0) {
                        return "Không hiệu lực"
                    } else {
                        return "Có hiệu lực"
                    }
                }
            },
            { label: 'Người lập hồ sơ', name: 'CreatorCode', width: 50, align: 'left' },
            {
                label: 'Ngày lập hồ sơ', name: 'CreateTime', width: 50, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            }
        ],
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
        title: "Bảo trì nội dung cảnh báo hạng mục thu phí hệ thống",
        url: "/SystemManage/SysCIRemindContent/Form",
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().sfxjsnrbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysCIRemindContent/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    $.deleteForm({
        url: "/SystemManage/SysCIRemindContent/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().sfxjsnrbh },
        success: function () {
            //$.currentWindow().$("#gridList").resetSelection();
            //$.currentWindow().$("#gridList").trigger("reloadGrid");
            $("#gridList").trigger("reloadGrid");
        }
    })
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().sfxjsnrbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysCIRemindContent/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300",
        btn: null,
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemManage/SysCIRemindContent/SubmitForm?keyValue=" + keyValue,
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