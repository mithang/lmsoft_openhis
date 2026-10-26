function KfChange() {
    $.modalOpen({
        id: "Form",
        title: "Chuyen kho",
        url: "/Home/KfChange",
        width: "500px",
        height: "400px",
        callBack: function (iframeId) {
            $.currentWindow(iframeId).AcceptClick(function () {

            });
        }
    });
}

//Lay chenh lech So ngay giua hai ngay
function getDaysBetween(date1, date2) {
    const ONE_DAY = 1000 * 60 * 60 * 24; // So mili giay cua mot ngay
    const date1Time = date1.getTime(); // Lay dau thoi gian
    const date2Time = date2.getTime();

    const difference = Math.abs(date1Time - date2Time); // Lay chenh lech thoi gian
    return Math.round(difference / ONE_DAY); // So ngay giua hai ngay
}