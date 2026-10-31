
using Newtouch.CIS.Repository;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Lưu ýxạQuản lý
    /// </summary>
    public class InfusionApp : AppBase, IInfusionApp
    {
        private readonly IMzsyypxxRepo _mzsyypxx;
        private readonly IMzsyzxxxRepo _mzsyzxxxRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMzsyypxxDmnService _mzsyypxxDmnService;

        // FIX-2B (B-CIS-05, goc): class nay khong co constructor DI nen 4 field
        // readonly tren luon null — moi lan goi MzsyxxQuery (id dang so hop le)
        // deu NRE o lambda InfusionApp.cs:293, du controller da guard tags rong/
        // sai dinh dang. Cac App khac (vd BespeakRegisterApp) deu co constructor
        // DI; ca 4 service deu da dang ky trong Program.cs nen chi can them
        // constructor nay la khoi phuc duong happy path.
        public InfusionApp(IMzsyypxxRepo mzsyypxx, IMzsyzxxxRepo mzsyzxxxRepo,
            ISysConfigRepo sysConfigRepo, IMzsyypxxDmnService mzsyypxxDmnService)
        {
            _mzsyypxx = mzsyypxx;
            _mzsyzxxxRepo = mzsyzxxxRepo;
            _sysConfigRepo = sysConfigRepo;
            _mzsyypxxDmnService = mzsyypxxDmnService;
        }

        /// <summary>
        /// Đồng bộđãquyết toánĐơn thuốcthông tin
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh,string mzh)
        {
            #region thứmộtbước：theođiều kiệntừquyết toánHệ thốngTra cứuĐã quyết toánĐơn thuốcthông tin settRpDetail
            var settRpDetail = OutpatientSettledRpQuery(kssj, jssj, fph, kh, mzh);
            if (settRpDetail == null || settRpDetail.Count <= 0) return "";
            #endregion

            #region thứhaibước：theođiều kiệnkéolấybảnđịatruyền dịchthông tin，sởtheocứđiều kiệnvàthứmộtbướcnhaucùng syxx
            var syxx = _mzsyypxx.IQueryable(p => p.sfsj >= kssj && p.sfsj < jssj && p.OrganizeId == OrganizeId && p.zt == "1");
            if (syxx.Any())
            {
                if (!string.IsNullOrWhiteSpace(mzh)) syxx = syxx.Where(p => p.mzh == mzh);
                if (!string.IsNullOrWhiteSpace(fph)) syxx = syxx.Where(p => p.fph == fph);
            }
            #endregion

            #region Bước 3: lấy giá trị khác biệt giữa settRpDetail và syxx — lấy ra thông tin truyền dịch mới thêm (newData)
            var newData = ScreeningNewInfusionData(settRpDetail, syxx.ToList());
            #endregion

            #region thứbốnbước：LưuThêm mớicủatruyền dịchthông tin
            return SaveNewInfusionData(newData);
            #endregion
        }

        /// <summary>
        /// LọcramớicủaĐơn thuốcquyết toánthông tin
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
        /// đãquyết toánĐơn thuốcchi tiếtTra cứu
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh)
        {
            var yfCode = _sysConfigRepo.GetValueByCode("syyfCode", this.OrganizeId);
            if (string.IsNullOrWhiteSpace(yfCode)) throw new Exception("vui lòngliênhệQuản lýviêncấu hìnhtruyền dịchCách dùngMãđộngtrạng tháitham số,tham sốMã：syyfCode");
            var response = _mzsyypxxDmnService.OutpatientSettledRpQuery(this.OrganizeId,kssj, jssj, fph, kh, yfCode,mzh);

			if (response != null) return response;
            throw new Exception(response == null ? "LấyĐã quyết toánChi tiết đơn thuốcthất bại" : "chưaTra cứuđếndữ liệu");
        }
		
		/// <summary>
		/// Bệnh nhânTra cứu
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
        /// Lưumớicủatruyền dịchthông tin
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
            if (successCount == 0) return "mớitruyền dịchthông tinLưu thất bại";
            return successCount < source.Count ? "một phầntruyền dịchthông tinLưu thất bại" : "";
        }

        /// <summary>
        /// phútnhóm
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="groupNo">Số nhóm</param>
        /// <returns></returns>
        public string Grouping(List<long> tags, string groupNo)
        {
            if (tags == null || tags.Count <= 0) return "Vui lòng chọncầnthànhnhóm của thuốc";
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
            if (successCount == 0) return "Không tìm thấy thuốc phù hợp yêu cầu";
            return successCount != tags.Count ? "một phầnthànhnhómthành công" : "";
        }

        /// <summary>
        /// truyền dịchBắt đầu
        /// </summary>
        /// <param name="tags">truyền dịchchi tiếtID</param>
        /// <param name="startDateTime">jThời gian kết thúc</param>
        /// <param name="seatNum">ghếsố/Số giường</param>
        /// <returns></returns>
        public string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum)
        {
            if (tags == null || tags.Count <= 0) return "Vui lòng chọncầnbắt đầutruyền dịch của thuốc";
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
            if (successCount == 0) return "Không tìm thấy thuốc phù hợp yêu cầu";
            return successCount != tags.Count ? "một phầntruyền dịchbắt đầuthành công" : "";
        }

        /// <summary>
        /// truyền dịchKết thúc
        /// </summary>
        /// <param name="tags">truyền dịchchi tiếtID</param>
        /// <param name="endDateTime">jThời gian kết thúc</param>
        /// <returns></returns>
        public string EndInfusion(List<long> tags, DateTime endDateTime)
        {
            if (tags == null || tags.Count <= 0) return "Vui lòng chọn thuốc cần kết thúc truyền dịch";
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
            if (successCount == 0) return "Không tìm thấy thuốc phù hợp yêu cầu";
            return successCount != tags.Count ? "một phầntruyền dịchKết thúcthành công" : "";
        }

        /// <summary>
        /// tạoSố nhóm
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
        /// Cài đặtghế ký tựsố
        /// </summary>
        /// <param name="tag">Mục tiêuID</param>
        /// <param name="seatNum">ghế ký tựsố</param>
        /// <returns></returns>
        public string SetSeatNum(long tag, string seatNum)
        {
            if (tag == 0) return "Vui lòng chọn thuốc cần kết thúc truyền dịch";
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
                return _mzsyzxxxRepo.Insert(entity) > 0 ? "" : "Lưu số ghế thất bại";
            }

            entity.seatNum = seatNum;
            entity.Modify();
            return _mzsyzxxxRepo.Update(entity) > 0 ? "" : "Lưu số ghế thất bại";
        }

        /// <summary>
        /// Tra cứutruyền dịchthông tin
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
