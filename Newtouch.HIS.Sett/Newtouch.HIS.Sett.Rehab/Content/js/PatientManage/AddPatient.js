var PatientInputDto = new Object;
$(function () {
    $('#myTab a:first').tab('show');
    gridList();
});

function gridList() {
    PatientInputDto.zyh = $("#txtNo").val();
    PatientInputDto.patientName = $("#txtName").val();
    PatientInputDto.BeginDate = $("#ksrq").val();
    PatientInputDto.EndDate = $("#jsrq").val();
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        treeGrid: true,
        treeGridModel: "adjacency",
        //ExpandColumn: "OrganizeId",
        postData: PatientInputDto,
        url: "/Patient/GetGridJson",
        height: $(window).height - 215,
        colModel: [
     { label: "Họ tên", name: "Name", width: "8%", align: "left" },
     { label: "Giới tính", name: "Gender", width: "8%", align: "center" },
     { label: "Tuổi", name: "Age", width: "8%", align: "center" },
     { label: "Ngày sinh", name: "Birthday", width: "10%", align: "center" },
     { label: "Tính chất bệnh nhân", name: "Nature", width: "8%", align: "left" },
     { label: "Chẩn đoán", name: "Diagnosis", width: "20%", align: "left" },
     { label: "Số ngoại trú", name: "OutPatientNo", width: "10%", align: "center" },
     { label: "Số nhập viện", name: "InpatientNo", width: "10%", align: "center" },
     { label: "Thời gian điều trị gần nhất", name: "TreatDate", width: "10%", align: "left" },
     { label: "Tỷ lệ tự trả", name: "LastTreatmentTime", width: "8%", align: "center" },
     { label: "Ghi chú", name: "Remark", width: "10%", align: "left" }
        ],
        pager: "#gridPager",
        viewrecords: true,
        unwritten: false,
        sortname: "ItemName",
        ondblClickRow: function (rowid) {
            EditSelectedItem(rowid);
        }
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: PatientInputDto
        }).trigger('reloadGrid');
    })
}

function checkNotNull() {
    var validator = $('#form1').validate();
    validator.settings = {
        rules: {
            mzh: { required: true },
            xm: { required: true },
            brxz: { required: true },
            nl: { required: true },
            hf: { required: true },
            zjh: { isIdentity: true },
            email: { email: true },
            phone: { isMobile: true },
            dh: { isPhone: true },
            jjlldh: { isPhone: true }
        },
        messages: {
            mzh: { required: "Phải nhập số ngoại trú" },
            xm: { required: "Phải nhập họ tên" },
            brxz: { required: "Phải nhập tính chất bệnh nhân" },
            nl: { required: "Phải nhập tuổi" },
            hf: { required: "Phải chọn tình trạng hôn nhân" },
            blh: { required: "Phải chọn tình trạng hôn nhân" },
            zjh: { isIdentity: "Định dạng giấy tờ không đúng" },
            email: { email: "Định dạng email không đúng" },
            phone: { isMobile: "Định dạng điện thoại di động không đúng" },
            dh: { isPhone: "Định dạng điện thoại không đúng" },
            jjlldh: { isPhone: "Định dạng điện thoại không đúng" }
        },
        showErrors: function (errorMap, errorList) {
            if (!$.isEmptyObject(errorList)) {
                $.modalAlert(errorList[0].message, 'warning');
            }
        }
    }
    if (!validator.form()) {
        return false;
    }
    //Số bệnh án
    var blh = $("#blh").val();
    if (!blh) {
        $.modalAlert("Số bệnh án trống!", 'warning');
        return false;
    }
    //Phiên âm
    var py = $("#py").val();
    if (!py) {
        $.modalAlert("Phiên âm trống!", 'warning');
        return false;
    }
    //Giới tính
    var xb = false;
    $('input[name="xb"]').each(function () {
        var $this = $(this);
        if ($this.parent().hasClass("active")) {
            xb = true;
            $("#xb").val();
        }
    });
    if (!xb) {
        $.modalAlert("Vui lòng chọn giới tính!", 'warning');
        return false;
    }
    return true;
}

//*****************************  Dưới đây là chức năng tra cứu bệnh nhân  **********************************//

var gridData = [];
//function GetPatientsInfo() {
//    for (var i = 0; i <= 10; i++) {
//        var item = {
//            Name: "张三",
//            Gender: "男",
//            Age: "26",
//            Birthday: "1989-06-21",
//            Nature: "自费",
//            Diagnosis: "骨折",
//            OutPatientNo: "10001",
//            InpatientNo: "",
//            LastTreatmentTime: "2017-04-23",
//            Remark: ""
//        }
//        gridData.push(item);
//    }
//}

//function BindPatientsData() {
//    window.jQuery("#PatientListGrid").jqGrid("clearGridData");
//    window.jQuery("#PatientListGrid").jqGrid({
//        //datatype: "local",
//        height: 300,
//        width: document.body.clientWidth - 20,
//        rowNum: 30,
//        rowList: [10, 20, 30],
//        autoScroll: true,
//        colModel: [
//            { label: "Họ tên", name: "Name", width: "8%", align: "left" },
//            { label: "Giới tính", name: "Gender", width: "8%", align: "center" },
//            { label: "Tuổi", name: "Age", width: "8%", align: "center" },
//            { label: "Ngày sinh", name: "Birthday", width: "10%", align: "center" },
//            { label: "Tính chất bệnh nhân", name: "Nature", width: "8%", align: "left" },
//            { label: "Chẩn đoán", name: "Diagnosis", width: "20%", align: "left" },
//            { label: "Số ngoại trú", name: "OutPatientNo", width: "10%", align: "center" },
//            { label: "Số nhập viện", name: "InpatientNo", width: "10%", align: "center" },
//            { label: "Thời gian điều trị gần nhất", name: "TreatDate", width: "10%", align: "left" },
//            { label: "Tỷ lệ tự trả", name: "LastTreatmentTime", width: "8%", align: "center" },
//            { label: "Ghi chú", name: "Remark", width: "10%", align: "left" }
//        ],
//        pager: "#PagePatientListGrid",
//        viewrecords: true,
//        unwritten: false,
//        sortname: "ItemName",
//        ondblClickRow: function (rowid) {
//            EditSelectedItem(rowid);
//        }
//    });
//    //for (var i = 0; i <= gridData.length; i++) {
//    //    window.jQuery("#PatientListGrid").jqGrid("addRowData", i + 1, gridData[i]);
//    //}
//    window.jQuery("#PatientListGrid").closest(".ui-jqgrid-bdiv").css({ "overflow-x": "hidden" });
//}

function EditSelectedItem(rowId) {
    $.modalOpen({
        id: "Form",
        title: "Sửa thông tin bệnh nhân",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

function AddNewPatient() {
    $.modalOpen({
        id: "Form",
        title: "Thêm mới bệnh nhân",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}