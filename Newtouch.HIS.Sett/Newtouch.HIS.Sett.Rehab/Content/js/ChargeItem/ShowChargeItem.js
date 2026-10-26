var VoucharsData = [];
ShowChargeItems();

function ShowChargeItems() {
    GetVoucherList();
    BindVouchersGrid();
}

function GetVoucherList() {
    for (var i = 0; i < 10; i++) {
        var item = {
            PatientName: "张三",
            CardNo: "1000122",
            VoucherNo: "10001",
            CreateVoucherDate: "2017-04-21",
            Total: "265.00"
        };
        VoucharsData.push(item);
    }
}

function BindVouchersGrid() {
    window.jQuery("#VoucherListGrid").jqGrid("clearGridData");
    window.jQuery("#VoucherListGrid").jqGrid({
        datatype: "local",
        height: document.documentElement.clientHeight - 85,
        width: document.documentElement.clientWidth - 5,
        shrinkToFit: true,
        rowNum: 30,
        rowList: [10, 20, 30],
        colModel: [
            { label: "Họ tên", name: "PatientName", width: "20%", align: "left" },
            { label: "Số thẻ", name: "CardNo", width: "20%", align: "center" },
            { label: "Số chứng từ", name: "VoucherNo", width: "20%", align: "center" },
            { label: "Ngày tạo chứng từ", name: "CreateVoucherDate", width: "20%", align: "center" },
            { label: "Tổng số tiền", name: "Total", width: "20%", align: "left" }
        ],
        pager: "#PageVoucherListGrid",
        viewrecords: true,
        unwritten: false,
        ondblClickRow: function (rowid) {
            EditSelectItem(rowid);
        }
    });
    for (var i = 0; i <= VoucharsData.length; i++) {
        window.jQuery("#VoucherListGrid").jqGrid('addRowData', i + 1, VoucharsData[i]);
    }
    window.jQuery("#VoucherListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function EditSelectItem(rowId) {
    $.currentWindow().LoadChargeItems(rowId);
    $.modalClose();
}