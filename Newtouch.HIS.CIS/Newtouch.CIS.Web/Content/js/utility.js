var chinese = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九'];
var len = ['十'];
var ydm = ['年', '月', '日'];
var hm = ['时', '分'];
function etchinese(s) {
    //Chuyển đổi một số đơn lẻ thành tiếng Trung  
    s = "" + s;
    var result = "";
    for (var i = 0; i < s.length; i++) {
        result += chinese[s.charAt(i)];
    }
    return result;
}
function specialcharacter(s) {
    //Xử lý trường hợp đặc biệt, và gọietchinese(s)Phương pháp, trả về tiếng Trung liên quan  
    s = "" + s;
    var result = "";
    if (s.length == 2) {
        if (s.charAt(0) == "1") {
            if (s.charAt(1) == "0") return len[0];
            return len[0] + chinese[s.charAt(1)];
        }
        if (s.charAt(1) == "0") return chinese[s.charAt(0)] + len[0];
        return chinese[s.charAt(0)] + len[0] + chinese[s.charAt(1)];
    }
    return etchinese(s)
}
function transformchinese(s) {
    //Xác thực định dạng ngày đã nhập, và gọispecialcharacter(s)Phương pháp, chuyển đổi số liên quan thành tiếng Trung  
    var datePat = /^(\d{2}|\d{4})(\/|-)(\d{1,2})(\2)(\d{1,2})$/;
    var matchArray = s.match(datePat);
    var ok = "";
    if (matchArray == null) return false;
    for (var i = 1; i < matchArray.length; i = i + 2) {
        ok += specialcharacter(matchArray[i] - 0) + ydm[(i - 1) / 2];
    }
    return ok;
}  

function transToChineseHM(hours, minutes) {
    // Ánh xạ chữ số hàng đơn vị dùng
    var onesPlace = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // Ánh xạ chữ số hàng đơn vị của số có hai chữ số hơi khác
    var onesPlaceForTensDigit = ['', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // Ánh xạ chữ số hàng chục
    var tensPlace = ['', '十', '二十', '三十', '四十', '五十']
    // Hàm chuyển đổi số thành tiếng Trung
    var t = num => num <= 10 ? onesPlace[num] : tensPlace[Math.floor(num / 10)] + onesPlaceForTensDigit[Math.floor(num % 10)]
    // Hàm chuyển đổi thời gian số thành thời gian tiếng Trung
    return t(hours) + '点' + t(minutes) + '分';
    //return hours < 6 ? 'Rạng sáng' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 12 ? 'Buổi sáng' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 18 ? 'Buổi chiều' + t(hours - 12) + 'điểm' + t(minutes) + 'phút' : 'Buổi tối' + t(hours - 12) + 'điểm' + t(minutes) + 'phút';

}

