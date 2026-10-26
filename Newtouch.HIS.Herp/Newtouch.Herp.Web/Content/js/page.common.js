//Vô hiệu hóa phần mềm  1:Vô hiệu hóa  2:Bật
function readonly(list, type) {
    if (list && list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            if (type === 1) {//Vô hiệu hóa
                $("#" + list[i]["id"]).addClass('newtouch_Readonly').attr("disabled", "disabled").css("background-color", "rgb(238,238,238)");
            } else {//Bật
                $("#" + list[i]["id"]).removeClass("newtouch_Readonly").removeAttr("disabled").css("background-color", "");
            }

        }
    }
}