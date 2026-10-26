using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysPatientChargeWaiverRepo : RepositoryBase<SysPatientChargeWaiverEntity>, ISysPatientChargeWaiverRepo
    {
        public SysPatientChargeWaiverRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysPatientChargeWaiverEntity> GetEffectiveList(int keyValue)
        {
            return null;
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysPatientChargeWaiverEntity GetForm(int keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.brsfjmbh == keyValue).FirstOrDefault();
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue, string orgId)
        {
            this.Delete(a => a.brsfjmbh == keyValue && a.OrganizeId == orgId);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysPatiChargeWaiverEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                sysPatiChargeWaiverEntity.Modify(keyValue);
                this.Update(sysPatiChargeWaiverEntity);
            }
            else
            {
                sysPatiChargeWaiverEntity.OrganizeId = orgId;
                sysPatiChargeWaiverEntity.Create(true, EFDBBaseFuncHelper.GetInstance().GetNewPrimaryKeyInt(SysPatientChargeWaiverEntity.GetTableName()));
                this.Insert(sysPatiChargeWaiverEntity);
            }

        }

        ///// <summary>
        ///// theo phiên âm đầutra cứu hạng mục thu phí
        ///// </summary>
        ///// <param name="parm"></param>
        ///// <returns></returns>
        //public decimal GetXT_brsfjm(string strWhere)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select top 1 * from [dbo].[xt_brsfjm] where zt=1");
        //    sql.Append(strWhere);
        //    var data = this.FindList<SysPatientChargeWaiverEntity>(sql.ToString());
        //    if (data != null && data.Count > 0)
        //    {
        //        return data[0].jmbl;
        //    }
        //    return 0;
        //}

        /// <summary>
        /// Tính hạng mục thu phí số tiền giảm trừ
        /// Bệnh nhânTrạng thái:1;--Hiệu lực
        /// thay đ ổi dấu hiệu:0;--chưa thay đ ổi 
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl"> loại lớn </param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmJe">Số tiền</param>
        /// <param name="outJmbl">Tỷ lệ giảm trừ</param>
        /// <param name="outJmje">Số tiền giảm trừ</param>
        /// <returns>count:Số tiền sau giảm trừ</returns>
        public decimal Get_Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje, string orgId)
        {
            //Tỷ lệ giảm trừ,số tiền giảm trừ,giảm trừ sau của số tiền
            decimal jmbl = 0, jmje = 0, count = 0;

            var entity = IQueryable().FirstOrDefault(p => p.brxz == parmBrxz && p.sfxm == parmSfxm && p.zt == "1" && p.OrganizeId == orgId);
            if (entity != null && entity.jmbl != 0)
            {
                jmbl = entity.jmbl;
                if (jmbl < 0)//Tự trả đ ịnh mức
                {
                    count = parmJe + jmbl;
                    jmje = Math.Abs(jmbl);
                }
                else
                {
                    jmje = parmJe * jmbl;
                    count = parmJe - jmje;
                }
            }
            else
            {
                entity = IQueryable().FirstOrDefault(p => p.dl == parmDl && p.brxz == parmBrxz && p.sfxm == parmSfxm && p.zt == "1" && p.OrganizeId == orgId);
                if (entity != null && entity.jmbl != 0)
                {
                    jmbl = entity.jmbl;
                    if (jmbl < 0)//Tự trả đ ịnh mức
                    {
                        count = Convert.ToDecimal(parmJe + jmbl);
                        jmje = Math.Abs(jmbl);
                    }
                    else
                    {
                        jmje = Convert.ToDecimal(parmJe * jmbl);
                        count = parmJe - jmje;
                    }
                }
            }
            outJmbl = jmbl;
            outJmje = jmje;
            return count;
        }
    }
}
