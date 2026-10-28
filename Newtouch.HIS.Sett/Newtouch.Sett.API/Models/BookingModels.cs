using Newtonsoft.Json.Linq;

namespace Newtouch.Sett.API.Models
{
    /// <summary>
    /// Mã kết quả, khớp ResponseResultCode của Lib.Base (10000/20000/40004).
    /// </summary>
    public static class ResultCode
    {
        public const int SUCCESS = 10000;
        public const int ERROR = 20000;
        public const int FAIL = 40004;
    }

    /// <summary>
    /// Envelope cũ mà 8016 (WebAPI.Manage) đợi: ResponseBaseOld.
    /// 8016 bọc lại bằng InitController.BuildResponseBase: code = resp.code,
    /// msg = msg + sub_code + sub_msg, data = resp.data. Vì vậy host này LUÔN
    /// trả HTTP 200 kèm envelope, kể cả khi nghiệp vụ FAIL — nếu trả HTTP lỗi,
    /// HttpClientHelper.PostAsync của 8016 trả về null và 8016 báo
    /// "Tài nguyên được yêu cầu không phản hồi".
    /// Riêng Y009 phân trang: sub_msg mang JSON Pagination {page, rows, sidx,
    /// sord, records} để 8016 deserialise lại trong BuildResponseBase(resp, pagination).
    /// </summary>
    public class ResponseBaseOld
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }

        public static ResponseBaseOld Ok(object data, string msg = "")
        {
            return new ResponseBaseOld { code = ResultCode.SUCCESS, msg = msg, data = data, sub_code = "", sub_msg = "" };
        }

        // QA PHASE 2 (SETT-01): sub_code trước đây luôn rỗng (trừ vài nơi
        // truyền tay như 10001-10003) nên client chỉ đọc sub_code tưởng lỗi
        // không có mã. Nay Fail/Error tự điền mã mặc định khi caller không
        // truyền (msg GIỮ NGUYÊN — 8016 đọc msg, xem ghi chú envelope ở trên).
        // Cố tình KHÔNG điền sub_msg ở đây: 8016 nối msg+sub_code+sub_msg
        // thành msg (điền sub_msg sẽ nhân đôi msg), và ở nhánh SUCCESS thì
        // sub_msg mang JSON Pagination cho Y009 (không được đụng vào).
        public static ResponseBaseOld Fail(string msg, string subCode = "")
        {
            return new ResponseBaseOld { code = ResultCode.FAIL, msg = msg, data = null, sub_code = string.IsNullOrWhiteSpace(subCode) ? "SETT_FAIL" : subCode, sub_msg = "" };
        }

        public static ResponseBaseOld Error(string msg)
        {
            return new ResponseBaseOld { code = ResultCode.ERROR, msg = msg, data = null, sub_code = "SETT_ERROR", sub_msg = "" };
        }
    }

    /// <summary>
    /// Body mà 8016 gửi sang host này (BookingRequest của WebAPI.Manage).
    /// Chú ý: tên trường giữ nguyên như 8016 serialise bằng Newtonsoft với
    /// DefaultContractResolver: AppId, OrganizeId, methodcode (chữ thường m),
    /// optype, user, Timestamp, paradata. paradata là object phẳng (không phải
    /// Dictionary&lt;string,string&gt; như bản .NET Framework gốc) nên host đọc
    /// qua JObject và chuẩn hoá giá trị thành chuỗi (xem ParaData).
    /// 8016 KHÔNG gửi header Authorization/token nào (dùng overload
    /// PostAsync(url, data) 2 tham số) và bản gốc đánh dấu toàn bộ 3 nhóm
    /// route là [IgnoreTokenDecrypt] — host này vì vậy không yêu cầu xác thực.
    /// </summary>
    public class BookingRequest
    {
        public string AppId { get; set; }
        public string OrganizeId { get; set; }
        public string methodcode { get; set; }
        public string optype { get; set; }
        public string user { get; set; }
        public DateTime Timestamp { get; set; }
        public JObject paradata { get; set; }
    }

    /// <summary>
    /// Đọc paradata không phân biệt hoa/thường của key. Lý do: 8016 gửi key
    /// theo đúng tên property model của nó (vd "ScheduId", "RegType", "ysgh",
    /// "ksrq"/"jsrq"), trong khi code gốc đọc key dạng khác ("scheduId",
    /// "regType", "ys", "outDate"/"FromoutDate") qua Dictionary case-sensitive
    /// nên các filter đó đã từng bị lặng lẽ bỏ qua. Tra cứu case-insensitive
    /// giữ tương thích cả hai dạng key mà không đổi contract.
    /// </summary>
    public class ParaData
    {
        private readonly JObject _obj;

        public ParaData(JObject obj)
        {
            _obj = obj;
        }

        public bool Has(string key)
        {
            return GetToken(key) != null && GetToken(key).Type != JTokenType.Null;
        }

        public string Get(string key)
        {
            var t = GetToken(key);
            if (t == null || t.Type == JTokenType.Null) return null;
            if (t.Type == JTokenType.String) return t.Value<string>();
            if (t.Type == JTokenType.Date) return t.Value<DateTime>().ToString("yyyy-MM-dd HH:mm:ss");
            return t.ToString(Newtonsoft.Json.Formatting.None);
        }

        public JToken GetToken(string key)
        {
            if (_obj == null) return null;
            foreach (var p in _obj.Properties())
            {
                if (string.Equals(p.Name, key, System.StringComparison.OrdinalIgnoreCase)) return p.Value;
            }
            return null;
        }

        public JObject Raw
        {
            get { return _obj; }
        }
    }

    /// <summary>
    /// Terminal suy ra từ AppId, đúng GetBookTerminal của Sett.API gốc:
    /// AppId chứa "SelfTerminal" -&gt; "SelfTerminal", còn lại -&gt; "His".
    /// Dùng làm CreatorCode cho đơn hàng và AppId cho các truy vấn PatientAnon.
    /// </summary>
    public static class Terminal
    {
        public static string FromAppId(string appId)
        {
            if (!string.IsNullOrEmpty(appId) && appId.Contains("SelfTerminal")) return "SelfTerminal";
            return "His";
        }
    }

    public static class SettConstants
    {
        // EnumMzyyzt (gốc EnumDB.cs): book=1, reg=2, cancel=3, bookreg=4, regcancel=5
        public const string YyBook = "1";
        public const string YyReg = "2";
        public const string YyCancel = "3";
        // EnumZYBZ: Xry=0, Bqz=1, Djz=2, Ycy=3
        public const string ZybzDjz = "2";
        public const string ZybzYcy = "3";
        // EnumMzghly: His=0, Yy=1, Yyt=3, WeChat=4, Alipay=5, Tj=6
        public const string GhlyHis = "1"; // OutpatReg dùng hằng "1" (Yy) cho đăng ký qua API
        // EnumGhxz: PTGH=0 (thường)
        public const string GhxzPtgh = "0";
        // EnumOutpatientJzbz: Djz=1 (chờ khám)
        public const string JzbzDjz = "1";
        // EnumJslx: GH=0 (đăng ký khám)
        public const string JslxGh = "0";
        // EnumJieSuanZT: YJ=1 (đã quyết toán)
        public const int JsztYj = 1;
        // EnumOrderType: mz=1, zy=2.
        // EnumOrderStatus — CÓ HAI bảng mâu thuẫn trong cây gốc: EnumDB của Sett
        // (dzf=0, yzf=1, ytk=2, zfz=3, không có zf) và Base.Domain của 8016
        // (dzf=0, zfz=1, yzf=2, ytk=3, zf=4). Code OrderDmnService dùng cả dzf,
        // zfz, yzf, zf nên chỉ nhất quán với bảng Base.Domain — host dùng bảng
        // này (cũng là bảng mà phía gọi 8016 hiểu).
        public const int OrderTypeMz = 1;
        public const int OrderTypeZy = 2;
        public const int OrderStuDzf = 0;
        public const int OrderStuZfz = 1;
        public const int OrderStuYzf = 2;
        public const int OrderStuZf = 4;
        // Tài khoản hệ thống mà Sett.API gốc tự cấp qua GetAuth (APIAdmin)
        public const string ApiUser = "APIAdmin";
    }
}
