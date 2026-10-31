/*!
 * openhis-format.js — Helper định dạng chuẩn Việt Nam cho OpenHIS (dùng chung).
 * Không phụ thuộc jQuery. Có thể include ở bất kỳ layout/partial nào:
 *   <script src="~/js/openhis-format.js"></script>
 *
 * Cách dùng:
 *   OpenHIS.formatDateVi(new Date())            -> "31/10/2026"
 *   OpenHIS.formatDateVi("2026-10-31")          -> "31/10/2026"
 *   OpenHIS.formatDateVi("2026-10-31", { withTime: true }) -> "31/10/2026 00:00:00"
 *   OpenHIS.formatDateVi(null)                  -> ""
 *   OpenHIS.formatMoneyVnd(1234567.5)           -> "1.234.567,5 ₫"
 *   OpenHIS.formatMoneyVnd(1234567)             -> "1.234.567 ₫"
 *   OpenHIS.formatMoneyVnd("1.234.567")         -> "1.234.567 ₫"
 *   OpenHIS.formatMoneyVnd(null)                -> ""
 *   OpenHIS.formatNumberVi(1234567.891, 2)      -> "1.234.567,89"
 *   OpenHIS.parseDateVi("31/10/2026")           -> Date (hoặc null)
 *   OpenHIS.parseMoneyVnd("1.234.567 ₫")        -> 1234567
 *
 * Ghi chú:
 *   - formatDateVi nhận Date | number (timestamp) | string ("yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss",
 *     "dd/MM/yyyy", ISO 8601). Chuỗi "yyyy-MM-dd" được parse thủ công để tránh lệch múi giờ
 *     do new Date("2026-10-31") hiểu là UTC.
 *   - formatMoneyVnd dùng dấu chấm phân cách hàng nghìn, dấu phẩy thập phân, hậu tố " ₫".
 */
(function (global) {
    'use strict';

    var OpenHIS = global.OpenHIS = global.OpenHIS || {};

    function pad2(n) {
        return (n < 10 ? '0' : '') + n;
    }

    /** Chuyển input về Date; trả về null nếu không hợp lệ. */
    function toDate(value) {
        if (value === null || value === undefined || value === '') {
            return null;
        }
        if (value instanceof Date) {
            return isNaN(value.getTime()) ? null : value;
        }
        if (typeof value === 'number') {
            var dNum = new Date(value);
            return isNaN(dNum.getTime()) ? null : dNum;
        }
        if (typeof value === 'string') {
            var s = value.trim();
            if (!s) { return null; }
            // yyyy-MM-dd[ HH:mm[:ss]] -> parse thủ công, tránh hiểu nhầm UTC
            var m = /^(\d{4})-(\d{2})-(\d{2})(?:[ T](\d{2}):(\d{2})(?::(\d{2}))?)?/.exec(s);
            if (m) {
                return new Date(
                    parseInt(m[1], 10), parseInt(m[2], 10) - 1, parseInt(m[3], 10),
                    m[4] ? parseInt(m[4], 10) : 0,
                    m[5] ? parseInt(m[5], 10) : 0,
                    m[6] ? parseInt(m[6], 10) : 0
                );
            }
            // dd/MM/yyyy[ HH:mm[:ss]]
            m = /^(\d{1,2})\/(\d{1,2})\/(\d{4})(?:[ T](\d{1,2}):(\d{2})(?::(\d{2}))?)?/.exec(s);
            if (m) {
                return new Date(
                    parseInt(m[3], 10), parseInt(m[2], 10) - 1, parseInt(m[1], 10),
                    m[4] ? parseInt(m[4], 10) : 0,
                    m[5] ? parseInt(m[5], 10) : 0,
                    m[6] ? parseInt(m[6], 10) : 0
                );
            }
            var d = new Date(s);
            return isNaN(d.getTime()) ? null : d;
        }
        return null;
    }

    /**
     * Định dạng ngày kiểu Việt Nam.
     * @param {Date|number|string|null} value
     * @param {{withTime?: boolean, withSeconds?: boolean}} [options]
     * @returns {string} "dd/MM/yyyy" hoặc "dd/MM/yyyy HH:mm[:ss]"; "" nếu không hợp lệ
     */
    OpenHIS.formatDateVi = function (value, options) {
        var d = toDate(value);
        if (!d) { return ''; }
        var opts = options || {};
        var out = pad2(d.getDate()) + '/' + pad2(d.getMonth() + 1) + '/' + d.getFullYear();
        if (opts.withTime) {
            out += ' ' + pad2(d.getHours()) + ':' + pad2(d.getMinutes());
            if (opts.withSeconds !== false) {
                out += ':' + pad2(d.getSeconds());
            }
        }
        return out;
    };

    /** Chuỗi ngày + giờ đầy đủ "dd/MM/yyyy HH:mm:ss". */
    OpenHIS.formatDateTimeVi = function (value) {
        return OpenHIS.formatDateVi(value, { withTime: true, withSeconds: true });
    };

    /** Parse chuỗi ngày dd/MM/yyyy (hoặc yyyy-MM-dd) -> Date | null. */
    OpenHIS.parseDateVi = function (text) {
        return toDate(text);
    };

    /** Chuyển giá trị về number (chấp nhận "1.234.567,89" hoặc "1234567.89"). */
    function toNumber(value) {
        if (value === null || value === undefined || value === '') { return null; }
        if (typeof value === 'number') { return isFinite(value) ? value : null; }
        var s = String(value).replace(/\s|₫|đ|VND/gi, '');
        if (!s) { return null; }
        var hasComma = s.indexOf(',') > -1;
        var hasDot = s.indexOf('.') > -1;
        if (hasComma && hasDot) {
            // "1.234.567,89" (VN) hay "1,234,567.89" (US)?
            s = s.lastIndexOf(',') > s.lastIndexOf('.')
                ? s.replace(/\./g, '').replace(',', '.')
                : s.replace(/,/g, '');
        } else if (hasComma) {
            // "1234,5" -> thập phân; "1,234" -> phân cách hàng nghìn
            s = /,\d{3}$/.test(s) ? s.replace(/,/g, '') : s.replace(',', '.');
        } else if (hasDot) {
            // "1.234.567" -> phân cách hàng nghìn
            s = /^\d{1,3}(\.\d{3})+$/.test(s) ? s.replace(/\./g, '') : s;
        }
        var n = parseFloat(s);
        return isNaN(n) ? null : n;
    }

    /**
     * Định dạng số kiểu Việt Nam: "." phân cách hàng nghìn, "," thập phân.
     * @param {number|string|null} value
     * @param {number} [decimals] số chữ số thập phân (mặc định: giữ tối đa 2, bỏ số 0 cuối)
     */
    OpenHIS.formatNumberVi = function (value, decimals) {
        var n = toNumber(value);
        if (n === null) { return ''; }
        var fixed = (decimals === undefined || decimals === null)
            ? String(Math.round(n * 100) / 100)
            : n.toFixed(decimals);
        var parts = fixed.split('.');
        var intPart = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        var decPart = parts.length > 1 ? parts[1].replace(/0+$/, '') : '';
        return decPart ? intPart + ',' + decPart : intPart;
    };

    /**
     * Định dạng tiền VND: "1.234.567 ₫" (phân cách hàng nghìn dấu chấm, hậu tố " ₫").
     * @param {number|string|null} value
     * @param {{suffix?: string, decimals?: number}} [options] suffix mặc định " ₫"
     * @returns {string} "" nếu không hợp lệ
     */
    OpenHIS.formatMoneyVnd = function (value, options) {
        var opts = options || {};
        var n = toNumber(value);
        if (n === null) { return ''; }
        var body = OpenHIS.formatNumberVi(n, opts.decimals);
        var suffix = opts.suffix === undefined ? ' ₫' : opts.suffix;
        return body + suffix;
    };

    /** Parse chuỗi tiền VND ("1.234.567 ₫") -> number | null. */
    OpenHIS.parseMoneyVnd = function (text) {
        return toNumber(text);
    };

})(typeof window !== 'undefined' ? window : this);
