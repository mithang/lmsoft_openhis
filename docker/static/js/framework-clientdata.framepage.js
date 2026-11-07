try {
    if (window.top && window.top.$ && $.getLocationHost() != window.top.$.getLocationHost()) {
        var appId = $.getCurrentAppId();
        if (!!appId) {
            if (window.top.$.clients && window.top.$.clients.checkFirst && window.top.$.clients.checkFirst(appId)) {
                $.ajax({
                    url: "/ClientsData/GetClientsDataJson",
                    type: "get",
                    dataType: "json",
                    async: false,
                    cache: false,
                    success: function (data) {
                        try { window.top.$.clients.extToClients(data, appId); } catch (e) {}
                    }
                });
                $.ajax({
                    url: "/ClientsData/GetAsyncClientsDataJson",
                    type: "get",
                    dataType: "json",
                    async: false,
                    cache: false,
                    success: function (data) {
                        try { window.top.$.clients.extToClients(data, appId); } catch (e) {}
                    }
                });
            }
        }
    }
} catch (e) {
    // Cross-origin / cross-port fallback
}