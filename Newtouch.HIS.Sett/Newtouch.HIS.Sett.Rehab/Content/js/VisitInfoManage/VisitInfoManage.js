var griddata = [];

//Grid liên kết các hạng mục đã chọn
function BindVisitInfoData() {
    window.jQuery("#AccountingListGrid").jqGrid("clearGridData");
    window.jQuery("#AccountingListGrid").jqGrid({
        datatype: "local",
        height: "auto",
        width: $("#dvVisitG").width(),
        shrinkToFit: true,
        rowNum: 30,
        rowList: [10, 20, 30],
        autoScroll: true,
        colModel: [
            {
                label: "Trả lại", name: "IS_RETURN", width: "3%", align: "left", editable: true, edittype: "checkbox", formatter: function (cellvalue) {
                    return "<input name='chkReturn' class='IS_RETURN' type='checkbox' value='' />";
                }
            },
            { label: "Hạng mục điều trị", name: "ItemName", width: "15%", align: "left" },
            { label: "Hạng mục thay thế", name: "ReplaceTtemName", width: "15%", align: "left" },
            { label: "Số lượng", name: "ItemCount", width: "3%", align: "left" },
            {
                label: "Số lượng trả lại", name: "RETURNS_SL", width: "5%", align: "left", formatter: function (cellvalue) {
                    return "<input name='IS_RETURN' type='text' class='tuishuliang' style='width: 35px; height:15px;' checked value='" + cellvalue + "' />";
                }
            },
            { label: "Đơn vị", name: "Unit", width: "5%", align: "left" },
            { label: "Thời lượng mỗi lần", name: "SingleTimeLength", width: "9%", align: "left" },
            { label: "Đơn giá", name: "UnitPrice", width: "5%", align: "left" },
            { label: "Loại", name: "Category", width: "3%", align: "left" },
            { label: "Nhà trị liệu", name: "Doctor", width: "5%", align: "left" },
            { label: "Thời gian điều trị", name: "TreatDate", width: "10%", align: "left" },
            { label: "Tỷ lệ tự trả", name: "SelfPaidRatio", width: "5%", align: "left" },
            { label: "Tổng giá", name: "Total", width: "5%", align: "left" },
            { label: "Ghi chú", name: "Remark", width: "12%", align: "left" },
            { label: "No", name: "No", hidden: true },
            { label: "ItemCode", name: "ItemCode", hidden: true },
            { label: "ReplaceItemCode", name: "ReplaceItemCode", hidden: true },
            { label: "ReplaceTtemCount", name: "ReplaceTtemCount", hidden: true },
            { label: "ReplaceTtemUnit", name: "ReplaceTtemUnit", hidden: true },
            { label: "ReplaceTtemUnitPrice", name: "ReplaceTtemUnitPrice", hidden: true },
            { label: "Visit", name: "Visit", hidden: true },
            { label: "ReplaceTtemSingleTimeLength", name: "ReplaceTtemSingleTimeLength", hidden: true }
        ],
        pager: "#PageAccountingListGrid",
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName"
        //ondblClickRow: function (rowid) {
        //    EditSelectItem(rowid);
        //}
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#AccountingListGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
    window.jQuery("#AccountingListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function GetVisitInfo(rowId) {
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            No: i.toString(),
            ItemCode: "1001",
            ItemName: "红外线治疗",
            ItemCount: "2",
            RETURNS_SL: "1",
            Unit: "次",
            UnitPrice: "20",
            SingleTimeLength: "30",
            Category: "PT",
            Doctor: "童咪咪",
            TreatDate: "2017-04-11",
            SelfPaidRatio: "0.1%",
            Total: "40",
            IS_RETURN: "",
            ReplaceItemCode: "",
            ReplaceTtemName: "",
            ReplaceTtemCount: "",
            ReplaceTtemUnit: "",
            ReplaceTtemUnitPrice: "",
            ReplaceTtemSingleTimeLength: "",
            Visit: "",
            Remark: ""
        };
        griddata.push(itemjson);
    }
    return griddata;
}

function LoadChargeItems(rowId) {
    GetVisitInfo(rowId);
    BindVisitInfoData();
}

function Refund() {
    var refundCount = 0;
    var v = "";
    $("[name='chkReturn']:checked").each(function () {
        var tds = $(this).parent().parent().find("td");
        if (tds != undefined && tds.length > 0) {
            for (var n = 0; n < tds.length; n++) {
                if ($(tds[n]).attr("aria-describedby").replace("AccountingListGrid_", "") === "RETURNS_SL") {
                    refundCount = $(tds[n]).find("input").val();
                    continue;
                }
                if ($(tds[n]).attr("aria-describedby").replace("AccountingListGrid_", "") === "No") {
                    v = $(tds[n]).text();
                    for (var i = 0; i < griddata.length; i++) {
                        if (griddata[i].No === v) {
                            griddata[i].ItemCount = parseInt(griddata[i].ItemCount) - parseInt(refundCount);
                            if (griddata[i].ItemCount <= 0) griddata.splice(i, 1);
                        }
                    }
                }
            }
        }
    });
    BindVisitInfoData();
}

//Phím tắt: Xóa
function newtouch_event_f4() {
    $("#AccountingListGrid").jqGrid("clearGridData");
    BindVisitInfoData();
}

//Phím tắt: F6: Hoàn phí
function newtouch_event_f6() {
    $.modalConfirm("Bạn có chắc muốn hoàn phí không?",
        function (flag) {
            if (flag === true) Refund();
        });
}

