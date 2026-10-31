$(function () {
    $.clientsInit();
})
$.clientsInit = function () {
    var extToClients = function (data) {
        //duyệtdatacủaThuộc tính mở rộngtriểnđếnwindow.clients
        if (!!!window.clients) {
            window.clients = {};
        }
        for (dataitemName in data) {
            var itemdata = data[dataitemName];
            if (typeof (itemdata) == typeof ('abc')) {
                //chữkýchuỗithìcầnSử dụngevalPhương pháp
                itemdata = eval(itemdata);
            }
            window.clients[dataitemName] = itemdata;
        }
    }
    //phátajaxyêu cầu
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

    //Đồng bộyêu cầu  //VDmenuphiếunội dungphảicầndùngĐồng bộphươngkiểu
    funcClientsAjax("/ClientsData/GetClientsDataJson", false);
    //Yêu cầu không đồng bộ
    //funcClientsAjax("/NewtouchClientsData/GetClientsDataJson", true);
}