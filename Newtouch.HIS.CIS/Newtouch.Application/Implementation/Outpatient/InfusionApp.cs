using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Quản lý tiêm
    /// </summary>
    public class InfusionApp : AppBase, IInfusionApp
    {
        private readonly IMzsyypxxRepo _mzsyypxx;
        private readonly IMzsyzxxxRepo _mzsyzxxxRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMzsyypxxDmnService _mzsyypxxDmnService;

        /// <summary>
        /// Đồng bộ thông tin đơn thuốc đã thanh toán
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh,string mzh)
        {
            #region Bước 1: Theo điều kiện, tra cứu thông tin đơn thuốc đã thanh toán từ hệ thống thanh toán settRpDetail
            var settRpDetail = OutpatientSettledRpQuery(kssj, jssj, fph, kh, mzh);
            if (settRpDetail == null || settRpDetail.Count <= 0) return "";
            #endregion

            #region Bước 2: Theo điều kiện, lấy thông tin truyền dịch nội bộ, điều kiện căn cứ giống bước 1 syxx
            var syxx = _mzsyypxx.IQueryable(p => p.sfsj >= kssj && p.sfsj < jssj && p.OrganizeId == OrganizeId && p.zt == "1");
            if (syxx.Any())
            {
                if (!string.IsNullOrWhiteSpace(mzh)) syxx = syxx.Where(p => p.mzh == mzh);
                if (!string.IsNullOrWhiteSpace(fph)) syxx = syxx.Where(p => p.fph == fph);
            }
            #endregion

            #region Bước 3: Lấy giá trị khác biệt giữa settRpDetail và syxx——lấy ra thông tin truyền dịch mới tăng newData
            var newData = ScreeningNewInfusionData(settRpDetail, syxx.ToList());
            #endregion

            #region Bước 4: Lưu thông tin truyền dịch mới tăng
            return SaveNewInfusionData(newData);
            #endregion
        }

        /// <summary>
        /// Lọc ra thông tin thanh toán đơn thuốc mới
        /// </summary>
        /// <param name="tagDate"></param>
        /// <param name="srcData"></param>
        /// <returns></returns>
        public List<OutpatientSettledRpQueryResponseDTO> ScreeningNewInfusionData(List<OutpatientSettledRpQueryResponseDTO> tagDate, List<MzsyypxxEntity> srcData)
        {
            if (tagDate == null || tagDate.Count <= 0) return null;
            if (srcData == null || srcData.Count <= 0) return tagDate;
            var result = new List<OutpatientSettledRpQueryResponseDTO>();
            tagDate.ForEach(p =>
            {
                if (!srcData.Exists(q => q.jsmxnm == p.jsmxnm && q.cfmxId == p.cfmxId && q.ypCode == p.ypCode)) result.Add(p);
            });
            return result;
        }

        /// <summary>
        /// Tra cứu chi tiết đơn thuốc đã thanh toán
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh)
        {
            var yfCode = _sysConfigRepo.GetValueByCode("syyfCode", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(yfCode)) throw new Exception("请联系管理员配置输液用法代码动态参数,参数编码：syyfCode");
            var response = _mzsyypxxDmnService.OutpatientSettledRpQuery(this.OrganizeId,kssj, jssj, fph, kh, yfCode,mzh);

			if (response != null) return response;
            throw new Exception(response == null ? "获取已结算处方明细失败" : "未查询到数据");
        }
		
		/// <summary>
		/// Tra cứu bệnh nhân
		/// </summary>
		/// <param name="kh"></param>
		/// <param name="fph"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <returns></returns>
		public List<MzsyypxxEntity> PatientListQuery(string kh, string fph, DateTime kssj, DateTime jssj)
        {
            var mzsyypxx = _mzsyypxx.IQueryable(p => p.sfsj >= kssj && p.sfsj < jssj && p.kh.Contains(kh ?? "") && p.fph.Contains(fph ?? ""));
            if (mzsyypxx == null || !mzsyypxx.Any()) return null;
            var result = new List<MzsyypxxEntity>();
            mzsyypxx.ToList().ForEach(p =>
            {
                if (!result.Any(q => q.xm == p.xm && q.kh == p.kh)) result.Add(new MzsyypxxEntity { xm = p.xm, kh = p.kh });
            });
            return result;
        }

        /// <summary>
        /// Lưu thông tin truyền dịch mới
        /// </summary>
        /// <param name="source">Nguồn dữ liệu</param>
        /// <returns></returns>
        public string SaveNewInfusionData(List<OutpatientSettledRpQueryResponseDTO> source)
        {
            if (source == null || source.Count <= 0) return "";
            var successCount = 0;
            source.ForEach(p =>
            {
                successCount += _mzsyypxxDmnService.InsertNewMzsyypxx(p, OrganizeId, UserIdentity.UserCode);
            });
            if (successCount == 0) return "新输液信息保存失败";
            return successCount < source.Count ? "部分输液信息保存失败" : "";
        }

        /// <summary>
        /// Nhóm
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="groupNo">Số nhóm</param>
        /// <returns></returns>
        public string Grouping(List<long> tags, string groupNo)
        {
            if (tags == null || tags.Count <= 0) return "请选择要成组的药品";
            var successCount = 0;
            tags.ForEach(p =>
            {
                if (p == 0) return;
                var entity = _mzsyypxx.FindEntity(q => q.Id == p);
                if (entity == null || entity.Id == 0) return;
                entity.groupNo = groupNo;
                entity.Modify();
                successCount += _mzsyypxx.Update(entity);
            });
            if (successCount == 0) return "未检索到符合要求的药品";
            return successCount != tags.Count ? "部分成组成功" : "";
        }

        /// <summary>
        /// Bắt đầu truyền dịch
        /// </summary>
        /// <param name="tags">ID chi tiết truyền dịch</param>
        /// <param name="startDateTime">jThời gian kết thúc</param>
        /// <param name="seatNum">Số ghế/số giường</param>
        /// <returns></returns>
        public string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum)
        {
            if (tags == null || tags.Count <= 0) return "请选择要开始输液的药品";
            var successCount = 0;
            tags.ForEach(p =>
            {
                if (p == 0) return;
                var entity = _mzsyzxxxRepo.FindEntity(q => q.syypxxId == p);
                if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
                {
                    entity = new MzsyzxxxEntity
                    {
                        syypxxId = p,
                        OrganizeId = OrganizeId,
                        sykssj = startDateTime,
                        seatNum = seatNum ?? ""
                    };
                    entity.Create(true);
                    successCount += _mzsyzxxxRepo.Insert(entity);
                }
                else
                {
                    entity.sykssj = startDateTime;
                    entity.seatNum = seatNum ?? "";
                    entity.Modify();
                    successCount += _mzsyzxxxRepo.Update(entity);
                }
            });
            if (successCount == 0) return "未检索到符合要求的药品";
            return successCount != tags.Count ? "部分输液开始成功" : "";
        }

        /// <summary>
        /// Kết thúc truyền dịch
        /// </summary>
        /// <param name="tags">ID chi tiết truyền dịch</param>
        /// <param name="endDateTime">jThời gian kết thúc</param>
        /// <returns></returns>
        public string EndInfusion(List<long> tags, DateTime endDateTime)
        {
            if (tags == null || tags.Count <= 0) return "请选择要结束输液的药品";
            var successCount = 0;
            tags.ForEach(p =>
            {
                if (p == 0) return;
                var entity = _mzsyzxxxRepo.FindEntity(q => q.syypxxId == p);
                if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
                {
                    entity = new MzsyzxxxEntity
                    {
                        syypxxId = p,
                        OrganizeId = OrganizeId,
                        syjssj = endDateTime
                    };
                    entity.Create(true);
                    successCount += _mzsyzxxxRepo.Insert(entity);
                }
                else
                {
                    entity.syjssj = endDateTime;
                    entity.Modify();
                    successCount += _mzsyzxxxRepo.Update(entity);
                }
            });
            if (successCount == 0) return "未检索到符合要求的药品";
            return successCount != tags.Count ? "部分输液结束成功" : "";
        }

        /// <summary>
        /// Tạo số nhóm
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public string CreateGroupNo(string kh,string mzh)
        {
            var allGroupNo = _mzsyypxxDmnService.SelectAllGroupNoByKh(kh, OrganizeId,mzh);
            if (allGroupNo == null || allGroupNo.Count <= 0) return "1";
            var tmpList = new List<int>();
            foreach (var item in allGroupNo)
            {
                int num;
                int.TryParse(item, out num);
                if (num > 0) tmpList.Add(Convert.ToInt32(item));
            }

            if (tmpList.Count == 0) return "1";
            var maxNum = tmpList.OrderByDescending(p => p).ToList()[0];
            return (++maxNum).ToString();
        }

        /// <summary>
        /// Đặt số ghế
        /// </summary>
        /// <param name="tag">ID đích</param>
        /// <param name="seatNum">Số ghế</param>
        /// <returns></returns>
        public string SetSeatNum(long tag, string seatNum)
        {
            if (tag == 0) return "请选择要结束输液的药品";
            var entity = _mzsyzxxxRepo.FindEntity(q => q.syypxxId == tag);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
            {
                entity = new MzsyzxxxEntity
                {
                    syypxxId = tag,
                    OrganizeId = OrganizeId,
                    seatNum = seatNum
                };
                entity.Create(true);
                return _mzsyzxxxRepo.Insert(entity) > 0 ? "" : "保存座位号失败";
            }

            entity.seatNum = seatNum;
            entity.Modify();
            return _mzsyzxxxRepo.Update(entity) > 0 ? "" : "保存座位号失败";
        }

        /// <summary>
        /// Tra cứu thông tin truyền dịch
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzsyypxxVO> MzsyxxQuery(List<long> ids, string organizeId)
        {
            var result = new List<MzsyypxxVO>();
            if (ids == null || ids.Count == 0) return result;
            ids.ForEach(p =>
            {
                var item = _mzsyypxxDmnService.SelectMzsyypxxById(p, organizeId);
                if (item != null) result.Add(item);
            });
            return result;
        }
    }
}
