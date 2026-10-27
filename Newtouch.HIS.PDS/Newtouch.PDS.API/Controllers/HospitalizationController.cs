using System;
using DbConfigHelper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Hospitalizatiion;
using Newtonsoft.Json;
using SqlSugar;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// Port của api/Hospitalization/ZYTYInterface (giao diện trả thuốc nội trú).
    ///
    /// KHÁC BIỆT CÓ CHỦ ĐÍCH SO VỚI CÁC CONTROLLER ENVELOPE KHÁC:
    /// - Controller gốc KHÔNG kế thừa ApiControllerBase mà là ApiController thuần: không
    ///   kiểm tra token, không envelope code/sub_code — trả thẳng ActResult. Bản port giữ
    ///   nguyên: không [ApiController] (để body null vẫn vào action và trả ActResult
    ///   ValidationFailure như gốc, thay vì HTTP 400 của [ApiController]), serialize
    ///   ActResult bằng Newtonsoft (indent + yyyy-MM-dd HH:mm:ss) như formatter gốc.
    /// - SQL gốc dùng Tools.DB.DbHelper (EF6-era helper, không có trong bản port) nên bản
    ///   này chạy cùng câu SQL qua SqlSugar Ado với đúng tên bảng 3 phần
    ///   (NewtouchHIS_PDS.dbo.zy_ypyzxx, NewtouchHIS_Base.dbo.V_C_xt_yp) và cùng tham số.
    /// - Thông điệp lỗi gốc là tiếng Trung trong response trả về cho client — đã dịch sang
    ///   tiếng Việt (logic, mã lỗi, điều kiện so sánh giữ nguyên).
    /// - QUIRK GỐC ĐƯỢC GIỮ NGUYÊN (đừng "sửa" nếu không hiểu hệ quả): sau vòng lặp,
    ///   ResultCode luôn bị gán AllFailure kể cả khi mọi dòng đều thành công (chỉ có
    ///   IsSucceed phân biệt thành công/thất bại). Client cũ có thể đang dựa vào hành vi
    ///   này nên bản port tái tạo đúng như code gốc.
    /// - Theo chính sách no-write, endpoint này CHỈ biên dịch và nối dây — KHÔNG gọi thử
    ///   live trên DB demo vì nhánh thành công chạy UPDATE thật (sqtybz = 1).
    /// </summary>
    [Route("api/Hospitalization")]
    public class HospitalizationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public HospitalizationController(IConfiguration config)
        {
            _config = config;
        }

        private SqlSugarClient NewDb()
        {
            var template = _config["BusinessDB:DefaultConn"];
            string conn;
            try { conn = string.Format(template, "NewtouchHIS_PDS"); }
            catch { conn = template; }
            return new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = conn,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
            });
        }

        private ContentResult JsonContent(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };
            return Content(JsonConvert.SerializeObject(obj, settings), "application/json");
        }

        [HttpPost("ZYTYInterface")]
        public IActionResult ZYTYInterface([FromBody] List<DrugRerurnRequest> request)
        {
            var res = new ActResult();
            if (request == null || request.Count <= 0)
            {
                res.IsSucceed = false;
                res.ResultMsg = "Dữ liệu yêu cầu trống";
                res.ResultCode = (int)ResultCode.ValidationFailure;
                return JsonContent(res);
            }
            var resultList = new List<DrugReturnResult>();
            foreach (var par in request)
            {
                var result = new DrugReturnResult();
                result.yzId = par.yzId;
                result.ypCode = par.ypCode;
                result.zh = par.zh;
                result.zxrq = par.zxrq;
                if (string.IsNullOrEmpty(par.yzId))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error1;
                    result.ErrorMsg = "Số y lệnh không được để trống";
                    resultList.Add(result);
                    continue;
                }
                if (string.IsNullOrEmpty(par.ypCode))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error2;
                    result.ErrorMsg = "Mã thuốc không được để trống";
                    resultList.Add(result);
                    continue;
                }
                if (string.IsNullOrEmpty(par.tysqr))
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error7;
                    result.ErrorMsg = "Người yêu cầu trả thuốc không được để trống";
                    resultList.Add(result);
                    continue;
                }
                if (par.zxrq == null || par.zxrq == DateTime.MinValue)
                {
                    result.IsSucceed = false;
                    result.ErrorCode = (int)ErrorCode.Error8;
                    result.ErrorMsg = "Ngày thực hiện không được để trống";
                    resultList.Add(result);
                    continue;
                }
                string error;
                int isOk = IsCouldReturn(par.yzId.Trim(), par.ypCode.Trim(), par.zxrq, out error);
                if (isOk > 0)
                {
                    result.IsSucceed = false;
                    result.ErrorCode = isOk;
                    result.ErrorMsg = error;
                    resultList.Add(result);
                }
                else
                {
                    string strSql = "update " + DbNames.Name("PdsDb", "NewtouchHIS_PDS") + ".dbo.zy_ypyzxx set sqtybz = @sqtybz where zxId = @zxId and ypCode = @ypCode and zxrq = @zxrq";
                    int count;
                    using (var db = NewDb())
                    {
                        count = db.Ado.ExecuteCommand(strSql,
                            new SugarParameter("@sqtybz", "1"),
                            new SugarParameter("@zxId", par.yzId),
                            new SugarParameter("@ypCode", par.ypCode),
                            new SugarParameter("@zxrq", par.zxrq));
                    }
                    if (count <= 0)
                    {
                        result.IsSucceed = false;
                        result.ErrorMsg = "Trả thuốc thất bại";
                        resultList.Add(result);
                    }
                }
            }
            if (resultList.Count > 0)
            {
                res.IsSucceed = false;
            }
            // QUIRK GỐC: luôn gán AllFailure kể cả khi mọi dòng thành công (xem ghi chú class).
            res.ResultCode = (int)ResultCode.AllFailure;
            res.Data = resultList;
            return JsonContent(res);
        }

        /// <summary>
        /// Trước khi trả thuốc, phán đoán xem bản ghi phát thuốc có phù hợp điều kiện trả thuốc hay không.
        /// </summary>
        public int IsCouldReturn(string zxId, string ypCode, DateTime? zxrq, out string error)
        {
            const string strSql = @"
select a.fybz,a.sqtybz,b.ypmc 
from NewtouchHIS_PDS.dbo.zy_ypyzxx a 
inner join NewtouchHIS_Base.dbo.V_C_xt_yp b on a.ypCode = b.ypCode and a.organizeId = b.OrganizeId 
where a.zxId = @zxId 
and a.ypCode = @ypCode 
and a.zxrq =@zxrq ";

            IList<YzxxVEntity> obj;
            try
            {
                using (var db = NewDb())
                {
                    obj = db.Ado.SqlQuery<YzxxVEntity>(strSql,
                        new SugarParameter("@zxId", zxId),
                        new SugarParameter("@ypCode", ypCode),
                        new SugarParameter("@zxrq", zxrq));
                }
            }
            catch
            {
                error = "Không thể tra cứu thông tin y lệnh";
                return (int)ErrorCode.Error3;
            }

            if (obj != null && obj.Count > 0)
            {
                foreach (var item in obj)
                {
                    var ypmc = item.ypmc;
                    switch (item.fybz)
                    {
                        case "1":
                            error = ypmc + " chưa phát thuốc, không được trả thuốc";
                            return (int)ErrorCode.Error4;
                        case "3":
                            error = ypmc + " đã được trả thuốc rồi";
                            return (int)ErrorCode.Error5;
                        default:
                            {
                                if (!"0".Equals(item.sqtybz.ToString()))
                                {
                                    error = ypmc + " đã yêu cầu trả thuốc, không được thao tác trùng lặp";
                                    return (int)ErrorCode.Error6;
                                }

                                break;
                            }
                    }
                }
                error = "";
                return (int)ErrorCode.Success;
            }

            error = "Không thể tra cứu thông tin y lệnh";
            return (int)ErrorCode.Error3;
        }
    }
}
