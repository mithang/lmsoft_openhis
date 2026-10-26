using System;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;
using System.Collections.Generic;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Đặt lịch hẹn khám
    /// </summary>
    public class BespeakRegisterApp : AppBase, IBespeakRegisterApp
    {
        private readonly ISysBespeakRegisterRepo _sysBespeakRegisterRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly IMzyyghDmnService _mzyyghDmnService;

        /// <summary>
        /// Sửa đổi quản lý đặt lịch hẹn
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
            return successCount == entities.Count ? "" : "部分修改失败，请重新修改";
        }

        #region Lấy thời gian đặt lịch hẹn cấu hình mặc định

        /// <summary>
        /// Lấy thời gian đặt lịch hẹn khám mặc định
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
        /// Lấy khoảng thời gian đặt lịch hẹn và tổng số có thể đặt trong cấu hình hệ thống
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
        /// Lấy ngày của n ngày tới
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

        #region Hàng bổ sung

        /// <summary>
        /// Ghép HTML hàng bổ sung
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
        /// Gửi chỉnh sửa đặt lịch hẹn khám
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
                    return "请填写预约时段";
                }
                if (p.regTime.IndexOf('-') <= 0) return "预约时段格式不对，请用‘-’分隔开始和结束时间。如：08:50-12:00";
                var arrTime = p.regTime.Split('-');
                if (arrTime.Length != 2) return "预约时段格式不对，请用‘-’分隔开始和结束时间。如：08:50-12:00";
                if (arrTime[0].IndexOf(':') <= 0) return "预约时段格式不对，请用‘-’分隔开始和结束时间。如：08:50-12:00";
                if (arrTime[1].IndexOf(':') <= 0) return "预约时段格式不对，请用‘-’分隔开始和结束时间。如：08:50-12:00";
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
        /// Xóa dữ liệu đặt lịch hẹn khám đã chỉ định
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
        /// Xóa đặt lịch hẹn khám đã quá hạn
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
        /// Lấy thông tin đặt lịch hẹn khám của khoa
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
        /// Ghép nội dung tr 
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
                if (item != null && !string.IsNullOrWhiteSpace(item.Id))   //Tồn tại
                {
                    var tmpTdId = Guid.NewGuid().ToString();
                    result.AppendLine("<td><div class=\"ckbox ckbox-ext\" style=\"margin-top: 3px;\"> ");
                    result.AppendLine("<input role=\"checkbox\" type =\"checkbox\" id =\"chk_" + tmpTdId + "\" attr-tabId=\"" + item.Id + "\" name=\"chk_reg\" colId=\"" + regTimes[i].tmpId + "\" " + (item.zt == "1" ? "checked" : "") + " >");
                    result.AppendLine("<label for=\"chk_" + tmpTdId + "\"></label>");
                    result.AppendLine("</div><input id=\"txt_" + tmpTdId + "\" name=\"bespeakMaxCount\" value=\"" + item.bespeakMaxCount + "\" /></td>");
                }
                else   //Không tồn tại
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
        /// Ghép nội dung tr 
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
        /// Lấy đặt lịch hẹn khám mặc định của khoa
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
        /// Ghép thông tin đặt lịch hẹn khám theo khoa và mã nhân viên
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
        /// Lấy tổng số người có thể đặt và số đã đặt
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
        /// Lưu đặt lịch hẹn khám
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
            if (regData == null || string.IsNullOrWhiteSpace(regData.Id)) return "获取挂号排班信息失败";
            if (regData.bespeakMaxCount <= 0) return "该科室或专家不支持预约挂号";
            var yyygh = _mzyyghRepo.IQueryable(p => p.brId == brId && p.zt == "1");//Đã đặt lịch hẹn khám
            if (yyygh != null && yyygh.Count() >= regData.bespeakMaxCount)
            {
                return "该科室或专家预约挂号已满额";
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
            return _mzyyghRepo.Insert(entity) > 0 ? "" : "保存预约挂号信息失败";
        }

        /// <summary>
        /// Sinh lịch đặt lịch hẹn
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        public string BuildCalendar(int year, int month, string deptCode, string ysgh)
        {
            if (month <= 0) throw new Exception("传入月份不合法,请传入自然数");
            var mzlx = string.IsNullOrWhiteSpace(ysgh) ? "1" : "3";//Cờ ngoại trú/nội trú 1：Khám ngoại trú thông thường 2：Cấp cứu 3：Khám chuyên gia
            var beginTime = Convert.ToDateTime(year + "-" + month + "-01");
            var endTime = beginTime.AddMonths(1).AddDays(-1);
            var calendarDate = GetCalendarDate(mzlx, deptCode, ysgh, beginTime, endTime);
            return AssembleCalendarHtml(calendarDate);
        }

        /// <summary>
        /// Lấy dữ liệu cần thiết cho lịch: Tháng hiện tại
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
        /// Ghép html lịch
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
        /// Ghép td của lịch
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
        /// Ghép lịch trống đầu tháng
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
        /// Ghép lịch trống cuối tháng
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
