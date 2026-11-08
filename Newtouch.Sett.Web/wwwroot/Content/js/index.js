//Tinh so luong theo lieu luong dieu tri mot lan
$.CalcSfxmSl = function (zll, dwjls, jjcl, throwExWhenZero) {
    if (!!!zll) {
        throw new Error("Thiếu liều lượng điều trị");
        return;
    }
    if (!!!dwjls) {
        throw new Error("Thiếu số lượng đơn vị đo");
        return;
    }
    if (!!!jjcl) {
        throw new Error("Thiếu chiến lược tính giá");
        return;
    }
    var res = Math.floor(zll / dwjls);
    if (res <= 0 && !(throwExWhenZero === false)) {
        throw new Error("Không cho phép số lượng 0");
        return;
    }
    return res;
}