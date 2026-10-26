$(function () {
    gridList();
});
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/SystemManage/SysPatiChargeLogic/GetsfGridJson",
        ExpandColumn: "orgId",
        postData: { keyword: $("#txt_keyword").val(), ogrId: $("#OrganizeId").val() },
        height: $(window).height() - 128,
        colModel: [
            { label: "Khóa chính", name: "brsfsfbh", hidden: true, key: true },
            { label: "Tính chất bệnh nhân", name: "brxzmc", width: 80, align: 'left' },
            { label: 'Nhóm lớn', name: "dlmc", width: 100, align: 'left' },
            { label: 'Hạng mục thu phí', name: "sfxmmc", width: 100, align: 'left' },
            {
                label: 'Phạm vi áp dụng', name: "mzzybz", width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue === "0") {
                            return "Chung";
                        } else if (cellvalue === "1") {
                            return "Khám ngoại trú";
                        } else if (cellvalue === "2") {
                            return "Bộ phận";
                        } else if (cellvalue === "3") {
                            return "Nội trú";
                        } else if (cellvalue === "4") {
                            return "Hệ thống";
                        } else {
                            return "";
                        }
                    }
                    return "";
                }
            },
            {
                label: 'Cấp độ thuật toán', name: 'sfjb', width: 80, align: 'left'
            },
            {
                label: "Phạm vi chi phí", name: 'fyfw', width: 130, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "0") {
                            return "Được thanh toán BHYT";
                        } else if (cellvalue == "1") {
                            return "Được thanh toán BHYT+Tự chịu theo loại";
                        } else if (cellvalue == "2") {
                            return "Được thanh toán BHYT+Tự chịu theo loại+Tự trả";
                        } else if (cellvalue == "3") {
                            return "Được thanh toán BHYT+Tự chịu theo loại+Tự trả+Hoàn toàn tự trả";
                        }
                    }
                    return "";
                }
            },
            { label: 'Tỷ lệ tự chịu', name: 'zfbl', width: 80, align: 'left' },
            {
                label: 'Tính chất tự chịu', name: 'zfxz', width: 80, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == "0") {
                            return "Tự trả";
                        } else if (cellvalue == "1") {
                            return "Tự lo";
                        }
                    }
                    return "";
                }
            },
            { label: 'Giới hạn chi phí', name: 'fysx', width: 50, align: 'left' },
            {
                label: 'Có hiệu lực', name: 'zt', width: 50, align: 'left',
                formatter: function (cellvalue) {
                    if (cellvalue) {
                        if (cellvalue == 0) {
                            return "Không hiệu lực";
                        } else {
                            return "Có hiệu lực";
                        }
                    }
                    return "";
                }
            }
        ],
        pager: "#gridPager",
        sortname: "CreateTime desc",
        rowNum: "10",
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid("setGridParam",
            {
                postData: { keyword: $("#txt_keyword").val(), ogrId: $("#OrganizeId").val() }
            }).trigger("reloadGrid");
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "Bảo trì thuật toán thu phí bệnh nhân hệ thống",
        url: "/SystemManage/SysPatiChargeLogic/Form",
        width: "700px",
        height: "350px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("Vui lòng chọn trước một thông tin", "warning");
        return;
    }
    var keyValue = $("#gridList").jqGridRowValue().brsfsfbh;
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin",
        url: "/SystemManage/SysPatiChargeLogic/Form?keyValue=" + keyValue,
        width: "700px",
        height: "300px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_delete() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("Vui lòng chọn trước một thông tin", "warning");
        return;
    }
    $.deleteForm({
        url: "/SystemManage/SysPatiChargeLogic/DeleteForm",
        param: { keyValue: $("#gridList").jqGridRowValue().brsfsfbh },
        success: function() {
            $.currentWindow().$("#gridList").resetSelection();
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    });
}
function btn_details() {
    var seleRowid = jQuery("#gridList").jqGrid("getGridParam", "selrow");
    if (!(seleRowid)) {
        $.modalAlert("Vui lòng chọn trước một thông tin", "warning");
        return;
    }
    var keyValue = $("#gridList").jqGridRowValue().brsfsfbh;
    $.modalOpen({
        id: "Details",
        title: "Xem cơ sở",
        url: "/SystemManage/SysPatiChargeLogic/Form?keyValue=" + keyValue,
        width: "700px",
        height: "350px",
        btn: null,
    });
}