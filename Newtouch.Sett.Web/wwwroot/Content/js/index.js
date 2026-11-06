//Tinh so luong theo lieu luong dieu tri mot lan
$.CalcSfxmSl = function (zll, dwjls, jjcl, throwExWhenZero) {
    if (!!!zll) {
        throw new Error("Thieu lieu luong dieu tri");
        return;
    }
    if (!!!dwjls) {
        throw new Error("Thieu so luong don vi do");
        return;
    }
    if (!!!jjcl) {
        throw new Error("Thieu chien luoc tinh gia");
        return;
    }
    var res = Math.floor(zll / dwjls);
    if (res <= 0 && !(throwExWhenZero === false)) {
        throw new Error("Khong cho phep so luong 0");
        return;
    }
    return res;
}