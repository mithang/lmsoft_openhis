$(function () {
    $.clientsInit();
})
$.clientsInit = function () {
    var extToClients = function (data) {
        //duyệt data của Thuộc tính mở rộng triển đến window.clients
        if (!!!window.clients) {
            window.clients = {};
        }
        for (dataitemName in data) {
            var itemdata = data[dataitemName];
            if (typeof (itemdata) == typeof ('abc')) {
                //chữ ký chuỗi thì cần Sử dụng eval Phương pháp
                itemdata = eval(itemdata);
            }
            window.clients[dataitemName] = itemdata;
        }
    }
    //phát ajax yêu cầu
    var funcClientsAjax = function (url, async) {
        $.ajax({
            url: url,
            type: "get",
            dataType: "json",
            async: async,
            cache: false,
            success: function (data) {
                extToClients(data);
            }
        });
    };

    //Đồng bộ yêu cầu  //VD menu phiếu nội dung phải cần dùng Đồng bộ phương kiểu
    funcClientsAjax("/ClientsData/GetClientsDataJson", false);
    //Yêu cầu không đồng bộ
    //funcClientsAjax("/NewtouchClientsData/GetClientsDataJson", true);
}