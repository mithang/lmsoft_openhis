function noticeMrqc(objStr) {
    var jsonObj = JSON.parse(objStr);
    return '<div class="list-group"><div class="list-group-item"><span class="list-group-item-heading">'
        + jsonObj.xm + '( ' + jsonObj.jzh + '--' + jsonObj.ch + '--' + jsonObj.ks + ' )' + '</span></div><div class="list-group-item"><span class="list-group-item-heading">Bệnh án:'
        + jsonObj.hzwd + '</span><p class="list-group-item-text">Mô tả kiểm soát chất lượng:' + jsonObj.fknr + '</p><p class="list-group-item-text">Xử lý trong thời hạn:' + $.getTime({ date: jsonObj.qxclsj }); + '</p></div></div>';
    //    return '<ul class="list-group"><li class="list-group-item">' + jsonObj.xm + '(' + jsonObj.jzh + '--' + jsonObj.ch + ')'
    //        + '</li><li class="list-group-item">khoa:' + jsonObj.ks + '</li><li class="list-group-item">bệnh án:' + jsonObj.hzwd
    //        + '</li><li class="list-group-item">mô tả kiểm soát chất lượng:' + jsonObj.fknr + '</li><li class="list-group-item">xử lý trong thời hạn:' + jsonObj.qxclsj + '</li></ul>';
}