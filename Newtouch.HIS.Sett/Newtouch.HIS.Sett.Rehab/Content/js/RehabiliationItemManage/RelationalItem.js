$(function () {
    Search();
});

//Tìm kiếm
function Search() {
    GetItemData();
    BindItemData();
}

//Mở mô-đun tạo mới quan hệ liên kết
function ShowRelationalItem() {
    $.modalOpen({
        id: "Form",
        title: "Tạo mới quan hệ liên kết",
        url: "/RelateRehabilitationItem/RelateItem",
        width: "1000px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//var mydata = [
//    { RelationName: "运动治疗", ItemCode: "1001", ItemName: "红外线治疗", relationType: "源项目", Unit: "部位", SingleTimeLength: "30", UnitPrice: "20", Remark: "" },
//    { RelationName: "运动治疗", ItemCode: "2001", ItemName: "激光疗法", relationType: "目标项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "自费" },
//    { RelationName: "立体动态干扰电治疗", ItemCode: "2002", ItemName: "低频脉冲电治疗", relationType: "目标项目", Unit: "部位", SingleTimeLength: "10", UnitPrice: "20", Remark: "丙" },
//    { RelationName: "立体动态干扰电治疗", ItemCode: "1005", ItemName: "超短波（小功率）", relationType: "源项目", Unit: "部位", SingleTimeLength: "", UnitPrice: "8", Remark: "自费" },
//    { RelationName: "神经肌肉电刺激治疗", ItemCode: "1002", ItemName: "神经肌肉电刺激治疗", relationType: "源项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "" },
//    { RelationName: "神经肌肉电刺激治疗", ItemCode: "2010", ItemName: "经皮神经电刺激治疗（≥3照射区）", relationType: "目标项目", Unit: "部位", SingleTimeLength: "20", UnitPrice: "30", Remark: "" },
//    { RelationName: "立体动态干扰电治疗", ItemCode: "1006", ItemName: "超短波（大功率）", relationType: "源项目", Unit: "部位", SingleTimeLength: "", UnitPrice: "8", Remark: "自费" },
//    { RelationName: "立体动态干扰电治疗", ItemCode: "2005", ItemName: "超短波（脉冲）", relationType: "目标项目", Unit: "部位", SingleTimeLength: "", UnitPrice: "8", Remark: "丙" },
//    { RelationName: "微针针刺（手针）", ItemCode: "2006", ItemName: "超短波（小功率）（≥3照射区）", relationType: "源项目", Unit: "", SingleTimeLength: "", UnitPrice: "24", Remark: "限面瘫" },
//    { RelationName: "微针针刺（手针）", ItemCode: "2007", ItemName: "超短波（大功率）（≥3照射区）", relationType: "目标项目", Unit: "", SingleTimeLength: "", UnitPrice: "24", Remark: "限面瘫" },
//    { RelationName: "微波治疗", ItemCode: "1008", ItemName: "微波治疗（≥3照射区）", relationType: "源项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "丙" },
//    { RelationName: "微波治疗", ItemCode: "2009", ItemName: "单纯超声波", relationType: "目标项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "自费" },
//    { RelationName: "牵引（颈牵引）", ItemCode: "1003", ItemName: "中频脉冲电治疗", relationType: "源项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "" },
//    { RelationName: "牵引（颈牵引）", ItemCode: "2011", ItemName: "灸法（艾箱灸）", relationType: "目标项目", Unit: "部位", SingleTimeLength: "5", UnitPrice: "10", Remark: "" }
//];

var mydata = [];

function GetItemData() {
    for (var i = 0; i < 10; i++) {
        var item = {
            RehaliationItemName: "运动治疗",
            RehaliationItemSingleTimeLength: "20",
            RehaliationItemUnitPrice: "50元/次",
            RehaliationItemRemark: "",
            HisItemName: "运动",
            HisItemSingleTimeLength: "25",
            HisItemUnitPrice: "50元/次",
            HisItemRemark: ""
        }
        mydata.push(item);
    }
}

function BindItemData() {
    window.jQuery("#relationalItemGridList").jqGrid("clearGridData");
    window.jQuery("#relationalItemGridList").jqGrid({
        data: mydata,
        datatype: "local",
        width: document.body.clientWidth,
        height: "auto",
        rowNum: 20,
        rowList: [10, 20, 30],
        colModel: [
            { label: "Hạng mục phục hồi chức năng", name: "RehaliationItemName", index: "RehaliationItemName", width: "25%", align: "left" },
            { label: "Thời lượng phục hồi chức năng", name: "RehaliationItemSingleTimeLength", index: "RehaliationItemSingleTimeLength", width: "7%", align: "center" },
            { label: "Tiêu chuẩn thu phí phục hồi chức năng", name: "RehaliationItemUnitPrice", index: "RehaliationItemUnitPrice", width: "8%", align: "center" },
            { label: "Ghi chú phục hồi chức năng", name: "RehaliationItemRemark", index: "RehaliationItemRemark", width: "10%", align: "left" },
            { label: "Hạng mục His", name: "HisItemName", index: "HisItemName", width: "25%", align: "left" },
            { label: "Thời lượng His", name: "HisItemSingleTimeLength", index: "HisItemSingleTimeLength", width: "7%", align: "center" },
            { label: "Tiêu chuẩn thu phí His", name: "HisItemUnitPrice", index: "HisItemUnitPrice", width: "8%", align: "center" },
            { label: "Ghi chú His", name: "HisItemRemark", index: "HisItemRemark", width: "10%", align: "left" }
        ],
        pager: "#relationalItemGridListPager",
        viewrecords: true,
        shrinkToFit: true,
        sortname: 'RelationName',
        ondblClickRow: function (rowid, iRow, iCol, e) {
            ShowEditRelationItemForm(GetRelationID(rowid));
        }
    });
    for (var i = 0; i <= griddata.length; i++) {
        window.jQuery("#relationalItemGridList").jqGrid('addRowData', i + 1, mydata[i]);
    }
    window.jQuery("#relationalItemGridList").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
}

//Lấy ID liên kết
function GetRelationID(rowId) {
    return 10001;
}

//Chuyển đến trang sửa quan hệ liên kết
function ShowEditRelationItemForm(relationID) {
    $.modalOpen({
        id: "Form",
        title: "Sửa quan hệ liên kết",
        url: "/RelateRehabilitationItem/RelateItem",
        width: "900px",
        height: "200px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
