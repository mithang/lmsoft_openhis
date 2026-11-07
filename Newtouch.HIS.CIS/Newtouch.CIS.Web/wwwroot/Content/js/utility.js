var chinese = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九'];
var len = ['十'];
var ydm = ['年', '月', '日'];
var hm = ['时', '分'];
function etchinese(s) {
    //Chuyển đổi các chữ số trên phiếu thành chữ Hán  
    s = "" + s;
    var result = "";
    for (var i = 0; i < s.length; i++) {
        result += chinese[s.charAt(i)];
    }
    return result;
}
function specialcharacter(s) {
    //Xử lý trường hợp đặc biệt, gọi phương thức etchinese(s), trả về chữ Hán liên quan  
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
    //Kiểm tra định dạng ngày nhập vào, gọi phương thức specialcharacter(s), chuyển các số liên quan thành chữ Hán  
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
    // Các ký tự số dùng để ánh xạ
    var onesPlace = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // Ký tự số hàng đơn vị dùng để ánh xạ
    var onesPlaceForTensDigit = ['', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // Các ký tự số hàng chục dùng để ánh xạ
    var tensPlace = ['', '十', '二十', '三十', '四十', '五十']
    // Hàm chuyển đổi số thành chữ Hán
    var t = num => num <= 10 ? onesPlace[num] : tensPlace[Math.floor(num / 10)] + onesPlaceForTensDigit[Math.floor(num % 10)]
    // Hàm chuyển đổi thời gian dạng số thành chữ Hán
    return t(hours) + 'điểm' + t(minutes) + 'phút';
    //return hours < 6 ? 'rạng sáng' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 12 ? 'buổi sáng' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 18 ? 'buổi chiều' + t(hours - 12) + 'điểm' + t(minutes) + 'phút' : 'buổi tối' + t(hours - 12) + 'điểm' + t(minutes) + 'phút';

}

