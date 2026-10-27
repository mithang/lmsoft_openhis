var chinese = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九'];
var len = ['十'];
var ydm = ['年', '月', '日'];
var hm = ['时', '分'];
function etchinese(s) {
    //sẽphiếucáisốchữchuyểnhóathànhtrongvăn  
    s = "" + s;
    var result = "";
    for (var i = 0; i < s.length; i++) {
        result += chinese[s.charAt(i)];
    }
    return result;
}
function specialcharacter(s) {
    //đốiđặcđặctình hìnhvàodòngchỗlý，vàgọietchinese(s)Phương pháp，Quay lạiliên quancủatrongvăn  
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
    //kiểm tranhậpcủaNgàyđịnh dạng，vàgọispecialcharacter(s)Phương pháp，sẽliên quansốchữchuyểnhóalàtrongvăn  
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
    // phản ánhxạcái ký tựsốdùng
    var onesPlace = ['零', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // hai ký tựsốcủacái ký tựsốphản ánhxạcóđiểmkhôngcùng
    var onesPlaceForTensDigit = ['', '一', '二', '三', '四', '五', '六', '七', '八', '九', '十']
    // phản ánhxạmười ký tựsố
    var tensPlace = ['', '十', '二十', '三十', '四十', '五十']
    // sốchữchuyểnhóalàtrongvănhàmsố
    var t = num => num <= 10 ? onesPlace[num] : tensPlace[Math.floor(num / 10)] + onesPlaceForTensDigit[Math.floor(num % 10)]
    // sốchữThời gianchuyểnhóalàtrongvănThời gianhàmsố
    return t(hours) + 'điểm' + t(minutes) + 'phần';
    //return hours < 6 ? '凌晨' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 12 ? 'trêntrưa' + t(hours) + 'điểm' + t(minutes) + 'phút' : hours < 18 ? 'dướitrưa' + t(hours - 12) + 'điểm' + t(minutes) + 'phút' : '晚trên' + t(hours - 12) + 'điểm' + t(minutes) + 'phút';

}

