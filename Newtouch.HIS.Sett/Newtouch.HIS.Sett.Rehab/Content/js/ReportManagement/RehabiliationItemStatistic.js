
var griddata = [];

var gridColumns = [];

// ************************ Phần chung  end  ************************************** //

//Lấy các cột của ItemsGrid, chủ yếu để ghép nhóm lớn
function GetItemsGridColumns() {
    //gridColumns = [
    //    { label: "Ngày", name: "Date", width: "100", align: "center" },
    //    { label: "Số ngoại trú", name: "OutpatientNo", width: "100", align: "center" },
    //    { label: "Họ tên", name: "PatientName", width: "100", align: "center" },
    //    { label: "Giới tính", name: "Gender", width: "60", align: "center" },
    //    { label: "Tuổi", name: "Age", width: "60", align: "center" },
    //    { label: "Triệu chứng bệnh", name: "Disease", width: "120", align: "center" },
    //    { label: "Ghi chú", name: "Remark", width: "120", align: "center" },
    //    { label: "Nhà trị liệu", name: "Doctor", width: "100", align: "center" },
    //    { label: "Chi phí", name: "Total", width: "50", align: "left" },
    //    { label: "Xoa bóp", name: "TuiNa", width: "50", align: "left" },
    //    { label: "Châm cứu", name: "ZhenFa", width: "50", align: "left" },
    //    { label: "Cứu ngải", name: "JiuFa", width: "50", align: "left" },
    //    { label: "Giác hơi", name: "BaGuan", width: "50", align: "left" },
    //    { label: "Xông thuốc", name: "YaoXun", width: "50", align: "left" },
    //    { label: "Kéo giãn", name: "QianYin", width: "50", align: "left" },
    //    { label: "Siêu âm", name: "ChaoSheng", width: "50", align: "left" },
    //    { label: "Thẩm thấu thuốc", name: "YaoTou", width: "50", align: "left" },
    //    { label: "Vật lý trị liệu", name: "LiLiao", width: "50", align: "left" },
    //    { label: "Phục hồi chức năng", name: "kangFu", width: "50", align: "left" },
    //    { label: "Nẹp", name: "FuMu", width: "50", align: "left" },
    //    { label: "Thuốc", name: "Medicine", width: "50", align: "left" },
    //    { label: "Kiểm tra", name: "Check", width: "50", align: "left" },
    //    { label: "Lượt người", name: "PersonTime", width: "50", align: "left" }
    //];
    gridColumns = [
        { label: "Ngày", name: "Date", width: "6%", align: "center" },
        { label: "Số ngoại trú", name: "OutpatientNo", width: "6%", align: "center" },
        { label: "Họ tên", name: "PatientName", width: "6%", align: "center" },
        { label: "Giới tính", name: "Gender", width: "5%", align: "center" },
        { label: "Tuổi", name: "Age", width: "5%", align: "center" },
        { label: "Triệu chứng bệnh", name: "Disease", width: "11%", align: "center" },
        { label: "Ghi chú", name: "Remark", width: "10%", align: "center" },
        { label: "Nhà trị liệu", name: "Doctor", width: "6%", align: "center" },
        { label: "Chi phí", name: "Total", width: "3%", align: "left" },
        { label: "Xoa bóp", name: "TuiNa", width: "3%", align: "left" },
        { label: "Châm cứu", name: "ZhenFa", width: "3%", align: "left" },
        { label: "Cứu ngải", name: "JiuFa", width: "3%", align: "left" },
        { label: "Giác hơi", name: "BaGuan", width: "3%", align: "left" },
        { label: "Xông thuốc", name: "YaoXun", width: "3%", align: "left" },
        { label: "Kéo giãn", name: "QianYin", width: "3%", align: "left" },
        { label: "Siêu âm", name: "ChaoSheng", width: "3%", align: "left" },
        { label: "Thẩm thấu thuốc", name: "YaoTou", width: "3%", align: "left" },
        { label: "Vật lý trị liệu", name: "LiLiao", width: "3%", align: "left" },
        { label: "Phục hồi chức năng", name: "kangFu", width: "3%", align: "left" },
        { label: "Nẹp", name: "FuMu", width: "3%", align: "left" },
        { label: "Thuốc", name: "Medicine", width: "3%", align: "left" },
        { label: "Kiểm tra", name: "Check", width: "3%", align: "left" },
        { label: "Lượt người", name: "PersonTime", width: "3%", align: "left" }
    ];
}

function BindItemData() {
    window.jQuery("#ItemsGrid").jqGrid("clearGridData");
    window.jQuery("#ItemsGrid").jqGrid({
        datatype: "local",
        height: "auto",
        width: document.documentElement.clientWidth-5,
        rowNum: 30,
        shrinkToFit: true,
        autoScroll: false,
        rowList: [10, 20, 30],
        colModel: gridColumns,
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName",
        pager: "#PageItemsGrid",
        ondblClickRow: function (rowid, iRow, iCol, e) {
            ShowSelectedDetail(rowid, iRow, iCol, e);
        }
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#ItemsGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
}

function FullItemsGrid() {
    GetItemsGridColumns();
    GetItemsGridData();
    BindItemData();
}

function GetItemsGridData() {
    griddata = [];
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            Week: "周一",
            Date: "2017-04-01",
            OutpatientNo: "10001",
            PatientName: "张三",
            Gender: "男",
            Age: "30",
            Disease: "骨折",
            Remark: "三级",
            Doctor: "童咪咪",
            Total: "200",
            TuiNa: "20",
            ZhenFa: "60",
            JiuFa: "0",
            BaGuan: "0",
            YaoXun: "0",
            QianYin: "0",
            ChaoSheng: "100",
            YaoTou: "0",
            LiLiao: "20",
            kangFu: "0",
            FuMu: "0",
            Medicine: "",
            Check: "",
            PersonTime: ""
        };
        griddata.push(itemjson);
    }
}

//Chuyển đến trang thông tin chi tiết
function ShowSelectedDetail(rowid, iRow, iCol, e) {
    $.modalOpen({
        id: "Form",
        title: "Chi tiết thống kê hạng mục thu phí ngoại trú",
        url: "/ReportManagement/RehabiliationItemStatisticDetail",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
    //window.location.href = "RehabiliationItemStatisticDetail";
}


// ************************ Dưới đây là phần chi tiết ************************************** //

//Grid liên kết các hạng mục đã chọn
function BindItemDetailData() {
    window.jQuery("#ItemDetailGrid").jqGrid("clearGridData");
    window.jQuery("#ItemDetailGrid").jqGrid({
        datatype: "local",
        height: 665,
        width: document.body.clientWidth,
        rowNum: 30,
        rowList: [10, 20, 30],
        autoScroll: true,
        colModel: [
            { label: "Nhóm lớn", name: "dl", width: "5%", align: "left" },
            { label: "Hạng mục điều trị", name: "ItemName", width: "15%", align: "left" },
            { label: "Hạng mục thay thế", name: "ReplaceTtemName", width: "15%", align: "left" },
            { label: "Số lượng", name: "ItemCount", width: "3%", align: "center" },
            { label: "Đơn vị", name: "Unit", width: "4%", align: "center" },
            { label: "Thời lượng mỗi lần", name: "SingleTimeLength", width: "9%", align: "center" },
            { label: "Đơn giá", name: "UnitPrice", width: "5%", align: "center" },
            { label: "Loại", name: "Category", width: "3%", align: "center" },
            { label: "Nhà trị liệu", name: "Doctor", width: "7%", align: "left" },
            { label: "Thời gian điều trị", name: "TreatDate", width: "8%", align: "center" },
            { label: "Tỷ lệ tự trả", name: "SelfPaidRatio", width: "6%", align: "center" },
            { label: "Tổng giá", name: "Total", width: "5%", align: "center" },
            { label: "Ghi chú", name: "Remark", width: "15%", align: "left" },
            { label: "ItemCode", name: "ItemCode", hidden: true },
            { label: "ReplaceItemCode", name: "ReplaceItemCode", hidden: true },
            { label: "ReplaceTtemCount", name: "ReplaceTtemCount", hidden: true },
            { label: "ReplaceTtemUnit", name: "ReplaceTtemUnit", hidden: true },
            { label: "ReplaceTtemUnitPrice", name: "ReplaceTtemUnitPrice", hidden: true },
            { label: "ReplaceTtemSingleTimeLength", name: "ReplaceTtemSingleTimeLength", hidden: true }
        ],
        pager: "#PageDetailGrid",
        viewrecords: true,
        sortname: "ItemName"
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#ItemDetailGrid").jqGrid('addRowData', i + 1, griddata[i]);
    }
    window.jQuery("#ItemDetailGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
}

function FullItemDetailGrid() {
    GetItemDetailGridData();
    BindItemDetailData();
}

function GetItemDetailGridData() {
    griddata = [];
    for (var i = 0; i < 5; i++) {
        var itemjson = {
            dl: "推拿",
            ItemCode: "1001",
            ItemName: "红外线治疗",
            ItemCount: "2",
            Unit: "次",
            UnitPrice: "20",
            SingleTimeLength: "30",
            Category: "PT",
            Doctor: "童咪咪",
            TreatDate: "2017-04-11",
            SelfPaidRatio: "0.1%",
            Total: "40",
            ReplaceItemCode: "",
            ReplaceTtemName: "",
            ReplaceTtemCount: "",
            ReplaceTtemUnit: "",
            ReplaceTtemUnitPrice: "",
            ReplaceTtemSingleTimeLength: "",
            Remark: ""
        };
        griddata.push(itemjson);
    }
}