
function Search() {

}

//Đăng ký bệnh nhân
function AddNewPatient() {
    $.modalOpen({
        id: "Form",
        title: "Đăng ký bệnh nhân",
        url: "/Patient/AddPatient",
        width: "1000px",
        height: "824px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}

//Lưu hạng mục
function AddItem() {
    BindItemData();
}

//Sửa hạng mục
function EditItem() {

}


function CleanDefaultValue(v) {
    if (v === "门诊号/身份证号/姓名") {
        $("#txtSearchInfo").val("");
    }
}
