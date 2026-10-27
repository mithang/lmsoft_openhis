function KfChange() {
    $.modalOpen({
        id: "Form",
        title: "Chuyển kho",
        url: "/Home/KfChange",
        width: "500px",
        height: "400px",
        callBack: function (iframeId) {
            $.currentWindow(iframeId).AcceptClick(function () {

            });
        }
    });
}

//Lấy chênh lệch số ngày giữa hai ngày
function getDaysBetween(date1, date2) {
    const ONE_DAY = 1000 * 60 * 60 * 24; // Số mili-giây trong một ngày
    const date1Time = date1.getTime(); // Lấy dấu thời gian
    const date2Time = date2.getTime();

    const difference = Math.abs(date1Time - date2Time); // Lấy chênh lệch thời gian
    return Math.round(difference / ONE_DAY); // Số ngày giữa hai ngày
}
