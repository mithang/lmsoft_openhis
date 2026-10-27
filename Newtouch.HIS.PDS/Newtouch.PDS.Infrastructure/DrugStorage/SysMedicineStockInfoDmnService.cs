using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;

namespace Newtouch.HIS.DomainServices.DrugStorage
{
    /// <summary>
    /// Thao tác tồn kho
    /// </summary>
    public class SysMedicineStockInfoDmnService : DmnServiceBase, ISysMedicineStockInfoDmnService
    {
        public SysMedicineStockInfoDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// Nhập kho từ ngoài Trừ tồn kho(Hủy bỏ)
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypdm"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string WbrkKkc(int sl, string pc, string ph, string ypdm, string organizeId, string yfbmCode, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@ypdm", ypdm),
                new SugarParameter("@zt", ((int) EnumKCZT.Enabled).ToString()),
                new SugarParameter("@sl", sl),
                new SugarParameter("@userCode", userCode)
            };
            return FindList<string>(TSqlStock.cancel_subtract_kcsl, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// Đóng băng tồn kho，và trả về thông tin lô bị đóng băng
        /// </summary>
        /// <param name="kcsl"></param>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string FrozenStork(int kcsl, string ypdm, string yfbmCode, string organizeId, string userCode, out List<FrozenBatchesDetailVEntity> frozenInfo)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@ypdm",ypdm),
                new SugarParameter("@djsl",kcsl),
                new SugarParameter("@yfbmCode",yfbmCode),
                new SugarParameter("@OrganizeId",organizeId),
                new SugarParameter("@userCode",userCode),
            };
            frozenInfo = FindList<FrozenBatchesDetailVEntity>(TSqlStock.frozen_stork_and_return_frozenInfo, param);
            if (frozenInfo == null || frozenInfo.Count <= 0) return "Đóng băng tồn kho thất bại";
            if (frozenInfo.Count == 1 && !string.IsNullOrWhiteSpace(frozenInfo[0].errorMsg))
            {
                return frozenInfo[0].errorMsg;
            }
            return "";
        }

        /// <summary>
        /// Gửi báo hỏng báo thừa
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity syxx)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    var kcxx = kcxxRepo.SelectData(syxx.Ypdm, syxx.Ph, syxx.pc, syxx.yfbmCode, syxx.OrganizeId);
                    var curKcsl = kcxx.Sum(p => p.kcsl);
                    if (syxx.Sysl < 0)//Báo hỏng  Kiểm tra tồn kho có đủ không
                    {
                        if (kcxx.Sum(p => p.kcsl) + syxx.Sysl < 0)
                            return string.Format("Mã là{0}、Số lô{1}、Lô{2}tồn kho thuốc không đủ", syxx.Ypdm, syxx.Ph, syxx.pc);
                    }

                    kcxxRepo.UpdateKcslWithTrans(syxx.Sysl, syxx.pc, syxx.Ph, syxx.Ypdm, syxx.yfbmCode, syxx.OrganizeId,
                        syxx.CreatorCode, db);

                    syxx.Sykc = curKcsl + syxx.Sysl;
                    db.Insert(syxx);

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }
    }
}
