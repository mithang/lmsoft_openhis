using System;
using Newtouch.CIS.Compat;
using System.Linq;
using System.Text;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Repository;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Đặt hẹn đăng ký khám
    /// </summary>
    public class BespeakRegisterApp : AppBase, IBespeakRegisterApp
    {
        private readonly ISysBespeakRegisterRepo _sysBespeakRegisterRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly IMzyyghDmnService _mzyyghDmnService;

        // FIX 2026-10-26: the original Framework class declared these readonly
        // fields with no constructor (Autofac-era code; the fields were never
        // assigned in either tree). Under MS DI the parameterless constructor
        // left them null, so every action using them NRE'd (proven:
        // /SystemManage/BespeakRegisterManage/BespeakRegister). All four
        // services are registered in Program.cs, so a proper DI constructor
        // restores the page. GetConfigureRegTime already falls back to a
        // default time list when the bespeakTime config row is absent.
        public BespeakRegisterApp(ISysBespeakRegisterRepo sysBespeakRegisterRepo,
            ISysConfigRepo sysConfigRepo, IMzyyghRepo mzyyghRepo,
            IMzyyghDmnService mzyyghDmnService)
        {
            _sysBespeakRegisterRepo = sysBespeakRegisterRepo;
            _sysConfigRepo = sysConfigRepo;
            _mzyyghRepo = mzyyghRepo;
            _mzyyghDmnService = mzyyghDmnService;
        }

        /// <summary>
        /// Sửa đặt hẹn quản lý
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public string UpdateBespeakRegisterManage(List<SysBespeakRegisterEntity> entities)
        {
            if (entities == null || entities.Count <= 0) return "";
            var successCount = 0;
            entities.ForEach(p =>
            {
                p.Modify();
                if (_sysBespeakRegisterRepo.Update(p) > 0) ++successCount;
            });
            return successCount == entities.Count ? "" : "Một phần sửa thất bại, vui lòng sửa lại";
        }

        #region Lấy mặc định cấu hình đặt hẹn thời gian

        /// <summary>
        /// Lấy mặc định của đặt hẹn đăng ký khám thời gian
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        public List<SysBespeakRegisterDateTimeVO> GetRegisterDateByWeek(DateTime sourceDate)
        {
            var result = new List<SysBespeakRegisterDateTimeVO>();
            var defaultRegTime = GetConfigureRegTime();
            switch (sourceDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    result = GetNextNDay(7, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Tuesday:
                    result = GetNextNDay(6, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Wednesday:
                    result = GetNextNDay(5, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Thursday:
                    result = GetNextNDay(4, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Friday:
                    result = GetNextNDay(3, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Saturday:
                    result = GetNextNDay(2, sourceDate, defaultRegTime);
                    break;
                case DayOfWeek.Sunday:
                    result = GetNextNDay(1, sourceDate, defaultRegTime);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Lấy hệ thống cấu hình đặt hẹn thời gian đoạn và có thể đặt hẹn tổng số
        /// </summary>
        /// <returns></returns>
        public List<SysBespeakRegisterTimeVO> GetConfigureRegTime()
        {
            var defaultRegDateStr = _sysConfigRepo.GetValueByCode("bespeakTime", this.OrganizeId);
            var result = defaultRegDateStr.ToObject<List<SysBespeakRegisterTimeVO>>();
            if (result == null || result.Count <= 0)
            {
                result = new List<SysBespeakRegisterTimeVO>
                {
                    new SysBespeakRegisterTimeVO{ tmpId=Guid.NewGuid().ToString(), regTime="08:30-11:30", bespeakMaxCount=100},
                    new SysBespeakRegisterTimeVO{ tmpId=Guid.NewGuid().ToString(), regTime="13:30-17:00", bespeakMaxCount=100}
                };
            }
            else
            {
                result.ForEach(p =>
                {
                    p.tmpId = Guid.NewGuid().ToString();
                });
            }
            return result;
        }

        /// <summary>
        /// Lấy chưa đến n ngày của ngày
        /// </summary>
        /// <param name="n">n ngày</param>
        /// <param name="source"></param>
        /// <param name="defaultRegTime"></param>
        /// <returns></returns>
        private List<SysBespeakRegisterDateTimeVO> GetNextNDay(int n, DateTime source, List<SysBespeakRegisterTimeVO> defaultRegTime)
        {
            var regDates = new List<SysBespeakRegisterDateTimeVO>();
            for (var i = 0; i < n; i++)
            {
                var tmpDate = source.AddDays(i);
                var item = new SysBespeakRegisterDateTimeVO
                {
                    tmpId = Guid.NewGuid().ToString(),
                    regDate = tmpDate.ToString("yyyy-MM-dd"),
                    week = tmpDate.ToString("ddd"),
                    regTimes = new List<SysBespeakRegisterTimeVO>()
                };
                regDates.Add(item);
                defaultRegTime.ForEach(p =>
                {
                    item.regTimes.Add(new SysBespeakRegisterTimeVO
                    {
                        tabId = "",
                        tmpId = p.tmpId,
                        regTime = p.regTime,
                        bespeakMaxCount = p.bespeakMaxCount,
                    });
                });
            }
            return regDates;
        }

        #endregion

        #region đuổi thêm dòng

        /// <summary>
        /// lắp ráp đuổi thêm dòng HTML
        /// </summary>
        /// <param name="regDatas"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        public string BuildNewRow(List<SysBespeakRegisterDateTimeVO> regDatas, List<SysBespeakRegisterTimeVO> regTimes)
        {
            var result = new StringBuilder();
            regDatas.ForEach(p =>
            {
                result.AppendLine(string.Format("<tr id=\"{0}\" attr-regDate=\"{1}\">", p.tmpId, p.regDate));
                result.AppendLine(string.Format("<th>{0}({1}))", p.week, p.regDate));
                result.AppendLine("<div class=\"ckbox\" style=\"float: right;padding-right: 0;\" > ");
                result.AppendLine("<input role=\"checkbox\" type=\"checkbox\" id=\"chk_" + p.tmpId + "\" name =\"chk_reg_row\" >");
                result.AppendLine("<label for=\"chk_" + p.tmpId + "\"></label>");
                result.AppendLine("</div></th>");
                regTimes.ForEach(q =>
                {
                    var tmpGuid = Guid.NewGuid().ToString();
                    result.AppendLine("<td><div class=\"ckbox ckbox-ext\" style=\"margin-top: 3px;\">");
                    result.AppendLine(string.Format("<input role=\"checkbox\" type=\"checkbox\" id=\"chk_" + tmpGuid + "\" name=\"chk_reg\" attr-tabId=\"\" colId=\"" + q.tmpId + "\" >"));
                    result.AppendLine(string.Format("<label for=\"chk_" + tmpGuid + "\"></label>"));
                    result.AppendLine(string.Format("</div><input id=\"txt_" + tmpGuid + "\" name=\"bespeakMaxCount\" value=\"" + q.bespeakMaxCount + "\" />"));
                    result.AppendLine("</td>");
                });
            });
            return result.ToString();
        }

        #endregion

        /// <summary>
        /// Gửi đặt hẹn đăng ký khám sửa
        /// </summary>
        /// <param name="regData"></param>
        /// <returns></returns>
        public string SubmitForm(List<SysBespeakRegisterEntity> regData)
        {
            if (regData == null || regData.Count <= 0) return "";
            var newData = new List<SysBespeakRegisterEntity>();
            var deleteData = new List<SysBespeakRegisterEntity>();
            foreach (var p in regData)
            {
                if (string.IsNullOrWhiteSpace(p.regTime))
                {
                    return "Vui lòng điền thời đoạn đặt hẹn";
                }
                if (p.regTime.IndexOf('-') <= 0) return "Định dạng khung giờ đặt hẹn không đúng, vui lòng dùng '-' để phân cách thời gian bắt đầu và thời gian kết thúc. Ví dụ: 08:50-12:00";
                var arrTime = p.regTime.Split('-');
                if (arrTime.Length != 2) return "Định dạng khung giờ đặt hẹn không đúng, vui lòng dùng '-' để phân cách thời gian bắt đầu và thời gian kết thúc. Ví dụ: 08:50-12:00";
                if (arrTime[0].IndexOf(':') <= 0) return "Định dạng khung giờ đặt hẹn không đúng, vui lòng dùng '-' để phân cách thời gian bắt đầu và thời gian kết thúc. Ví dụ: 08:50-12:00";
                if (arrTime[1].IndexOf(':') <= 0) return "Định dạng khung giờ đặt hẹn không đúng, vui lòng dùng '-' để phân cách thời gian bắt đầu và thời gian kết thúc. Ví dụ: 08:50-12:00";
                p.regBeginTime = Convert.ToDateTime(p.regDate.ToString("yyyy-MM-dd") + " " + arrTime[0]);
                p.regEndTime = Convert.ToDateTime(p.regDate.ToString("yyyy-MM-dd") + " " + arrTime[1]);

                if (string.IsNullOrWhiteSpace(p.Id))
                {
                    if (p.zt != "1") continue;
                    p.ysgh = p.ysgh ?? "";
                    p.OrganizeId = OrganizeId;
                    p.Create(true);
                    newData.Add(p);
                }
                else
                {
                    if (p.zt == "0")
                    {
                        deleteData.Add(p);
                    }
                    else
                    {
                        p.ysgh = p.ysgh ?? "";
                        p.OrganizeId = OrganizeId;
                        p.Modify();
                        _sysBespeakRegisterRepo.Update(p);
                    }
                }
            }
            _sysBespeakRegisterRepo.Insert(newData);
            DeleteData(deleteData);
            return "";
        }

        /// <summary>
        /// Xóa chỉ định đặt hẹn đăng ký khám dữ liệu
        /// </summary>
        /// <param name="targetData"></param>
        private void DeleteData(List<SysBespeakRegisterEntity> targetData)
        {
            if (targetData == null || targetData.Count <= 0) return;
            var deleteDataCount = targetData.Count;
            for (var i = 0; i < deleteDataCount; i++)
            {
                _sysBespeakRegisterRepo.Delete(targetData[i]);
            }
        }

        /// <summary>
        /// Xóa quá kỳ đặt hẹn đăng ký khám
        /// </summary>
        public void DeleteExpireRegData()
        {
            var yesterdayDate = DateTime.Now.AddDays(-1);
            var targetData = _sysBespeakRegisterRepo.IQueryable(p => p.regDate < yesterdayDate && p.OrganizeId == OrganizeId).ToList();
            if (targetData.Count <= 0) return;
            var tCount = targetData.Count;
            for (var i = 0; i < tCount; i++)
            {
                _sysBespeakRegisterRepo.Delete(targetData[i]);
            }
        }

        /// <summary>
        /// Lấy khoa đặt hẹn đăng ký khám thông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetRegDataHtmlByKs(string deptCode)
        {
            var result = new StringBuilder();
            if (string.IsNullOrWhiteSpace(deptCode))
            {
                return result.ToString();
            }
            var regData = _sysBespeakRegisterRepo.IQueryable(p => p.departmentCode == deptCode && p.ysgh == "" && p.OrganizeId == OrganizeId).ToList();
            if (regData.Count <= 0) return result.ToString();
            var regTimes = regData.Select(p => p.regTime).Distinct().Select(q => new SysBespeakRegisterTimeVO { regTime = q }).OrderBy(n => n.regTime).ToList();
            var colCount = regTimes.Count;
            if (colCount == 0) return result.ToString();
            var regDates = regData.Select(p => p.regDate).Distinct().OrderBy(q => q).ToList();
            var rowCount = regDates.Count;
            if (rowCount == 0) return result.ToString();
            result.AppendLine(BuildRegDataTitleHtml(colCount, regTimes) + "卐");
            for (var r = 0; r < rowCount; r++)
            {
                result.AppendLine(BuildRegDataTrHtml(regDates[r], "", regTimes));
            }
            return result.ToString();
        }

        /// <summary>
        /// build title of reg data table
        /// </summary>
        /// <param name="colCount"></param>
        /// <param name="regTimes"></param>
        /// <param name="colIds"></param>
        /// <returns></returns>
        private string BuildRegDataTitleHtml(int colCount, List<SysBespeakRegisterTimeVO> regTimes)
        {
            var result = new StringBuilder("<tr id=\"tr_first\"><th></th>");
            for (var i = 0; i < colCount; i++)
            {
                regTimes[i].tmpId = Guid.NewGuid().ToString();
                result.AppendLine("<td><div class=\"ckbox ckbox-ext\">");
                result.AppendLine("<input role=\"checkbox\" type=\"checkbox\" id=\"chk_" + regTimes[i].tmpId + "\" name=\"chk_reg_col\" checked>");
                result.AppendLine("<label for=\"chk_" + regTimes[i].tmpId + "\"></label>");
                result.AppendLine("</div>");
                result.AppendLine("<input name=\"txtRegTime\" value=\"" + regTimes[i].regTime + "\" id =\"" + regTimes[i].tmpId + "\" attr-defaultBespeakMaxCount=\"" + regTimes[i].bespeakMaxCount + "\" />");
                result.AppendLine("</td>");
            }
            return result.AppendLine("</tr>").ToString();
        }

        /// <summary>
        /// lắp ráp tr nội dung 
        /// </summary>
        /// <param name="regDate"></param>
        /// <param name="gh"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        private string BuildRegDataTrHtml(DateTime regDate, string gh, List<SysBespeakRegisterTimeVO> regTimes)
        {
            var result = new StringBuilder();
            if (regTimes == null || regTimes.Count <= 0) return result.ToString();
            var regs = _sysBespeakRegisterRepo.IQueryable(p => p.regDate == regDate && p.ysgh == gh && p.OrganizeId == OrganizeId);
            if (regs == null || !regs.Any()) return result.ToString();
            var tmpRowId = Guid.NewGuid().ToString();
            result.AppendLine("<tr id=\"" + tmpRowId + "\" attr-regDate=\"" + regDate.ToString("yyyy-MM-dd") + "\"><th>");
            result.AppendLine(regDate.ToString("ddd") + "(" + regDate.ToString("yyyy-MM-dd") + ")");
            result.AppendLine("<div class=\"ckbox\" style=\"float: right;padding-right: 0;\">");
            result.AppendLine("<input role=\"checkbox\" type=\"checkbox\" id=\"chk_" + tmpRowId + "\" name=\"chk_reg_row\" checked>");
            result.AppendLine("<label for=\"chk_" + tmpRowId + "\"></label>");
            result.AppendLine("</div></th>");
            for (var i = 0; i < regTimes.Count(); i++)
            {
                var p = regTimes[i];
                var item = regs.FirstOrDefault(n => n.regTime == p.regTime);
                if (item != null && !string.IsNullOrWhiteSpace(item.Id))   // tồn tại
                {
                    var tmpTdId = Guid.NewGuid().ToString();
                    result.AppendLine("<td><div class=\"ckbox ckbox-ext\" style=\"margin-top: 3px;\"> ");
                    result.AppendLine("<input role=\"checkbox\" type =\"checkbox\" id =\"chk_" + tmpTdId + "\" attr-tabId=\"" + item.Id + "\" name=\"chk_reg\" colId=\"" + regTimes[i].tmpId + "\" " + (item.zt == "1" ? "checked" : "") + " >");
                    result.AppendLine("<label for=\"chk_" + tmpTdId + "\"></label>");
                    result.AppendLine("</div><input id=\"txt_" + tmpTdId + "\" name=\"bespeakMaxCount\" value=\"" + item.bespeakMaxCount + "\" /></td>");
                }
                else   // không tồn tại
                {
                    var tmpTdId = Guid.NewGuid().ToString();
                    result.AppendLine("<td><div class=\"ckbox ckbox-ext\" style=\"margin-top: 3px;\"> ");
                    result.AppendLine("<input role=\"checkbox\" type =\"checkbox\" id =\"chk_" + tmpTdId + "\" attr-tabId=\"\" name=\"chk_reg\" colId=\"" + regTimes[i].tmpId + "\" >");
                    result.AppendLine("<label for=\"chk_" + tmpTdId + "\"></label>");
                    result.AppendLine("</div><input id=\"txt_" + tmpTdId + "\" name=\"bespeakMaxCount\" value=\"" + regTimes[i].bespeakMaxCount + "\" /></td>");
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// lắp ráp tr nội dung 
        /// </summary>
        /// <param name="regDate"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        private string BuildDefaultRegDataTrHtml(DateTime regDate, List<SysBespeakRegisterTimeVO> regTimes)
        {
            var result = new StringBuilder();
            if (regTimes == null || regTimes.Count <= 0) return result.ToString();
            var tmpRowId = Guid.NewGuid().ToString();
            result.AppendLine("<tr id=\"" + tmpRowId + "\" attr-regDate=\"" + regDate.ToString("yyyy-MM-dd") + "\"><th>");
            result.AppendLine(regDate.ToString("ddd") + "(" + regDate.ToString("yyyy-MM-dd") + ")");
            result.AppendLine("<div class=\"ckbox\" style=\"float: right;padding-right: 0;\">");
            result.AppendLine("<input role=\"checkbox\" type=\"checkbox\" id=\"chk_" + tmpRowId + "\" name=\"chk_reg_row\" >");
            result.AppendLine("<label for=\"chk_" + tmpRowId + "\"></label>");
            result.AppendLine("</div></th>");
            regTimes.ForEach(p =>
            {
                var tmpTdId = Guid.NewGuid().ToString();
                result.AppendLine("<td><div class=\"ckbox ckbox-ext\" style=\"margin-top: 3px;\"> ");
                result.AppendLine("<input role=\"checkbox\" type =\"checkbox\" id =\"chk_" + tmpTdId + "\" attr-tabId=\"\" name=\"chk_reg\" colId=\"" + p.tmpId + "\" >");
                result.AppendLine("<label for=\"chk_" + tmpTdId + "\"></label>");
                result.AppendLine("</div><input id=\"txt_" + tmpTdId + "\" name=\"bespeakMaxCount\" value=\"" + p.bespeakMaxCount + "\" /></td>");

            });
            return result.ToString();
        }

        /// <summary>
        /// Lấy khoa mặc định đặt hẹn đăng ký khám
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetDefaultHtmlByKs(string deptCode)
        {
            var result = new StringBuilder();
            var regDatas = GetRegisterDateByWeek(DateTime.Now);
            if (regDatas == null || regDatas.Count <= 0) return "";
            regDatas = regDatas.OrderBy(p => p.regDate).ToList();
            var regTimes = GetConfigureRegTime();
            if (regTimes == null || regTimes.Count <= 0) return "";
            regTimes = regTimes.OrderBy(p => p.regTime).ToList();
            result.Append(BuildRegDataTitleHtml(regTimes.Count, regTimes));
            result.Append("卐");
            regDatas.ForEach(p =>
            {
                var tmpRegDate = Convert.ToDateTime(p.regDate);
                result.Append(BuildDefaultRegDataTrHtml(tmpRegDate, regTimes));
            });
            return result.ToString();
        }

        /// <summary>
        /// Theo khoa và mã nhân viên lắp ráp đặt hẹn đăng ký khám thông tin
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string GetRegDataHtmlByKsAndGh(string deptCode, string gh)
        {
            if (string.IsNullOrWhiteSpace(deptCode) || string.IsNullOrWhiteSpace(gh)) return "";
            var regData = _sysBespeakRegisterRepo.IQueryable(p => p.departmentCode == deptCode && p.ysgh == gh && OrganizeId == OrganizeId).ToList();
            if (regData == null || regData.Count <= 0) return "";
            var result = new StringBuilder();
            var regTimes = regData.Select(p => p.regTime).Distinct().Select(q => new SysBespeakRegisterTimeVO { regTime = q }).OrderBy(n => n.regTime).ToList();
            var colCount = regTimes.Count;
            if (colCount == 0) return result.ToString();
            var regDates = regData.Select(p => p.regDate).Distinct().OrderBy(q => q).ToList();
            var rowCount = regDates.Count;
            if (rowCount == 0) return result.ToString();
            result.AppendLine(BuildRegDataTitleHtml(colCount, regTimes) + "卐");
            for (var r = 0; r < rowCount; r++)
            {
                result.AppendLine(BuildRegDataTrHtml(regDates[r], gh, regTimes));
            }
            return result.ToString();
        }

        /// <summary>
        /// Lấy có thể đặt hẹn tổng người số và đã đặt hẹn số
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public object GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh)
        {
            var sysBespeakRegisterEntity = _sysBespeakRegisterRepo.IQueryable(p => p.departmentCode == deptCode && p.regDate == regDate && p.regTime == regTime && p.OrganizeId == OrganizeId && p.ysgh == (gh ?? "")).FirstOrDefault();
            if (sysBespeakRegisterEntity == null) return null;
            var yyDetail = _mzyyghRepo.IQueryable(p => p.brId == sysBespeakRegisterEntity.Id && p.zt == "1");
            if (yyDetail != null || yyDetail.Count() >= 0) return new { yyys = yyDetail.Count(), bespeakMaxCount = sysBespeakRegisterEntity.bespeakMaxCount };
            return new { yyys = 0, bespeakMaxCount = sysBespeakRegisterEntity.bespeakMaxCount };
        }

        /// <summary>
        /// Lưu đặt hẹn đăng ký khám
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        public string SaveYY(string yyId, string brId, string blh, int? zjlx, string zjh)
        {
            _mzyyghRepo.DeleteById(yyId);
            var regData = _sysBespeakRegisterRepo.FindEntity(p => p.Id == brId);
            if (regData == null || string.IsNullOrWhiteSpace(regData.Id)) return "Lấy đăng ký khám xếp lịch thông tin thất bại";
            if (regData.bespeakMaxCount <= 0) return "Khoa hoặc chuyên gia không hỗ trợ đặt hẹn đăng ký khám";
            var yyygh = _mzyyghRepo.IQueryable(p => p.brId == brId && p.zt == "1");// đã đặt hẹn đăng ký khám
            if (yyygh != null && yyygh.Count() >= regData.bespeakMaxCount)
            {
                return "Khoa hoặc chuyên gia đặt hẹn đăng ký khám đã đầy mức";
            }

            var entity = new MzyyghEntity
            {
                brId = brId,
                blh = blh,
                OrganizeId = OrganizeId,
                zjlx = zjlx,
                zjh = zjh,
                bespeakNo = yyygh == null || !yyygh.Any() ? 1 : yyygh.Count() + 1
            };
            entity.Create(true);
            return _mzyyghRepo.Insert(entity) > 0 ? "" : "Lưu đặt hẹn đăng ký khám thông tin thất bại";
        }

        /// <summary>
        /// Tạo đặt hẹn xếp lịch ngày lịch
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public string BuildCalendar(int year, int month, string deptCode, string ysgh)
        {
            if (month <= 0) throw new Exception("Tháng truyền vào không hợp lệ, vui lòng truyền vào số tự nhiên");
            var mzlx = string.IsNullOrWhiteSpace(ysgh) ? "1" : "3";// Dấu hiệu ngoại trú/nội trú 1: thông thường ngoại trú 2: khám gấp 3: chuyên gia ngoại trú
            var beginTime = Convert.ToDateTime(year + "-" + month + "-01");
            var endTime = beginTime.AddMonths(1).AddDays(-1);
            var calendarDate = GetCalendarDate(mzlx, deptCode, ysgh, beginTime, endTime);
            return AssembleCalendarHtml(calendarDate);
        }

        /// <summary>
        /// Lấy ngày lịch sở cần dữ liệu hiện tại tháng
        /// </summary>
        /// <param name="mzlx"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<BespeakRegisterScheduling> GetCalendarDate(string mzlx, string deptCode, string ysgh, DateTime beginTime, DateTime endTime)
        {
            var yyghDetail = _mzyyghDmnService.SelectMzyyghDetail(mzlx, deptCode, ysgh, beginTime, endTime, OrganizeId);
            var result = new List<BespeakRegisterScheduling>();
            var tmpDate = beginTime;
            while (tmpDate <= endTime)
            {
                var item = new BespeakRegisterScheduling
                {
                    CalendarDate = tmpDate
                };
                result.Add(item);
                var schedulingItem = yyghDetail.Find(p => p.regDate == tmpDate);
                if (schedulingItem != null) item.schedulingData = schedulingItem;
                tmpDate = tmpDate.AddDays(1);
            }
            return result;
        }

        /// <summary>
        /// lắp ráp ngày lịch html
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string AssembleCalendarHtml(List<BespeakRegisterScheduling> data)
        {
            if (!data.Any()) return "";
            var tagData = data.ToJson().ToObject<List<BespeakRegisterScheduling>>().OrderBy(p => p.CalendarDate).ToList();
            var mixDate = tagData.FirstOrDefault();
            if (mixDate == null) return "";
            var result = new StringBuilder(AssembleEmptyDaysStartOfMonthHtml(mixDate.CalendarDate));
            while (tagData.Any())
            {
                var item = tagData[0];
                switch (item.CalendarDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        result.AppendLine("<td colspan='7' class='td-SeparateLine'></td><tr><td class='td-enable'>");
                        result.Append(AssembleCalendarTdHtml(item));
                        result.Append("</td>");
                        break;
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        result.AppendLine("<td class='td-enable'>");
                        result.Append(AssembleCalendarTdHtml(item));
                        result.Append("</td>");
                        break;
                    case DayOfWeek.Saturday:
                        result.AppendLine("<td class='td-enable td-weekend'>");
                        result.Append(AssembleCalendarTdHtml(item));
                        result.Append("</td>");
                        break;
                    case DayOfWeek.Sunday:
                        result.AppendLine("<td class='td-enable td-weekend'>");
                        result.Append(AssembleCalendarTdHtml(item));
                        result.Append("</td></tr>");
                        break;
                }
                tagData.Remove(item);
            }

            var maxDate = data.OrderByDescending(p => p.CalendarDate).FirstOrDefault().CalendarDate;
            if (result.Length > 0) result.AppendLine(AssembleEmptyDaysEndOfMonthHtml(maxDate));
            return result.ToString();
        }

        /// <summary>
        /// lắp ráp ngày lịch của td
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string AssembleCalendarTdHtml(BespeakRegisterScheduling item)
        {
            var result = new StringBuilder();
            result.AppendLine("<span>" + item.CalendarDate.Day + "</span>");
            if (item.schedulingData != null) result.Append("<span class='sp-regInfo' attr-regDate='" + item.schedulingData.regDate.ToString("yyyy-MM-dd HH:mm:ss") + "'>" + item.schedulingData.bespeakedCount + "/" + item.schedulingData.bespeakMaxCount + "</span>");
            return result.ToString();
        }

        /// <summary>
        /// lắp ráp tháng ban đầu trống trắng ngày lịch
        /// </summary>
        /// <param name="mixDate"></param>
        /// <returns></returns>
        private string AssembleEmptyDaysStartOfMonthHtml(DateTime mixDate)
        {
            var html = new StringBuilder("<tr>");
            switch (mixDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td>");
                    break;
                case DayOfWeek.Wednesday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td><td></td>");
                    break;
                case DayOfWeek.Thursday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td><td></td><td></td>");
                    break;
                case DayOfWeek.Friday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td><td></td><td></td><td></td>");
                    break;
                case DayOfWeek.Saturday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td><td></td><td></td><td></td><td></td>");
                    break;
                case DayOfWeek.Sunday:
                    html.Append("<td colspan='7' class='td-SeparateLine'></td></tr><tr><td></td><td></td><td></td><td></td><td></td><td></td>");
                    break;
            }

            return html.ToString();
        }

        /// <summary>
        /// Lắp ráp các ngày trống cuối tháng của lịch
        /// </summary>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        private string AssembleEmptyDaysEndOfMonthHtml(DateTime maxDate)
        {
            switch (maxDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "<td></td><td></td><td></td><td></td><td></td><td></td></tr>";
                case DayOfWeek.Tuesday:
                    return "<td></td><td></td><td></td><td></td><td></td></tr>";
                case DayOfWeek.Wednesday:
                    return "<td></td><td></td><td></td><td></td></tr>";
                case DayOfWeek.Thursday:
                    return "<td></td><td></td><td></td></tr>";
                case DayOfWeek.Friday:
                    return "<td></td><td></td></tr>";
                case DayOfWeek.Saturday:
                    return "<td></td></tr>";
                default:
                    return "";
            }
        }
    }
}
