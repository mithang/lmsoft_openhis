(function ($) {
    //
    var initedAppIdArr = new Array();
    //
    $.clients = {
        extToClients: function (data, appId) {
            //Duyệt các thuộc tính của data và mở rộng sang clients
            if (!!!appId) {
                if (!!!top.top.clients) {
                    top.top.clients = {
                    };
                }
            }
            else {
                if (!!!top.top.appclients) {
                    top.top.appclients = {
                    };
                }
                if (!!!top.top.appclients[appId]) {
                    top.top.appclients[appId] = {
                    };
                }
            }
            for (dataitemName in data) {
                var itemdata = data[dataitemName];
                if (typeof (itemdata) == typeof ('abc')) {
                    //Nếu là chuỗi thì phải dùng phương thức eval
                    itemdata = eval(itemdata);
                }
                if (!!!appId) {
                    top.top.clients[dataitemName] = itemdata;
                }
                else {
                    top.top.appclients[appId][dataitemName] = itemdata;
                }
            }
        },
        funcClientsAjax: function (url, async, appId) {
            $.ajax({
                url: url,
                type: "get",
                dataType: "json",
                async: async,
                cache: false,
                success: function (data) {
                    $.clients.extToClients(data, appId);
                }
            });
        },
        checkFirst: function (appId) {
            if (!!!appId) {
                appId = "";
            }
            var matchedd = $.jsonWhere(initedAppIdArr, function (v) {
                return v == appId;
            });
            if (matchedd && matchedd.length && matchedd.length > 0) {
                return false;
            }
            initedAppIdArr.push(appId);
            return true;
        },
        get: function (appId, key) {
            if (top.top.appclients
                && top.top.appclients[appId]
                && top.top.appclients[appId][key]) {
                return top.top.appclients[appId][key];
            }
            else {
                return top.top.clients[key];
            }
        },
        init: function (appId, locationHost) {
            if ($.clients.checkFirst(appId)) {
                //Tránh tải nhiều lần
                if (!!!appId) {
                    //Yêu cầu đồng bộ menu
                    $.clients.funcClientsAjax("/ClientsData/GetModuleDataJson", false);
                    //Yêu cầu đồng bộ
                    $.clients.funcClientsAjax("/ClientsData/GetClientsDataJson", false);
                    //Yêu cầu không đồng bộ
                    $.clients.funcClientsAjax("/ClientsData/GetAsyncClientsDataJson", true);
                }
                else {
                    //Đều gửi theo cách yêu cầu đồng bộ
                    $.clients.funcClientsAjax((!!locationHost ? locationHost : "") + "/ClientsData/GetClientsDataJson", false, appId);
                    $.clients.funcClientsAjax((!!locationHost ? locationHost : "") + "/ClientsData/GetAsyncClientsDataJson", false, appId);
                }
            }
        }
    };

    $(function () {
        $.clients.init();
    });
})(jQuery);