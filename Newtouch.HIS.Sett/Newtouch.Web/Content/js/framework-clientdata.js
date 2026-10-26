$(function () {
    $.clientsInit();
})
$.clientsInit = function () {
    var extToClients = function (data) {
        //Duyệtdatathuộc tính của Mở rộng tớiwindow.clients
        if (!!!window.clients) {
            window.clients = {};
        }
        for (dataitemName in data) {
            var itemdata = data[dataitemName];
            if (typeof (itemdata) == typeof ('abc')) {
                //Chuỗi thì phải dùngevalPhương thức
                itemdata = eval(itemdata);
            }
            window.clients[dataitemName] = itemdata;
        }
    }
    //发ajaxYêu cầu
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

    //Yêu cầu đồng bộ  //Ví dụ nội dung menu bắt buộc dùng cách đồng bộ
    funcClientsAjax("/ClientsData/GetClientsDataJson", false);
    //Yêu cầu không đồng bộ
    funcClientsAjax("/NewtouchClientsData/GetClientsDataJson", true);
}