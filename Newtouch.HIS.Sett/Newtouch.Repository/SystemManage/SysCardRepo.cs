using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCardRepo : RepositoryBase<SysCardEntity>, ISysCardRepo
    {
        public SysCardRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy tất cả các thẻ 
        /// </summary>
        /// <returns></returns>
        public List<SysCardEntity> GetList(string orgId)
        {
            return this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// theo số thẻ lấy bệnh nhân mã nội bộ
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetPatidByCardNo(string cardno, string orgId)
        {
            var cardEntity = GetCardEntity(cardno, orgId);
            return cardEntity != null ? cardEntity.patid.ToString() : "";
        }

        /// <summary>
        /// theo số thẻ lấy loại thẻ
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCardType(string cardno, string orgId)
        {
            var cardEntity = GetCardEntity(cardno, orgId);
            return cardEntity != null ? cardEntity.CardType : "";
        }

        /// <summary>
        /// Lấy mới 虚拟 thẻ số thẻ
        /// </summary>
        /// <returns></returns>
        public string GetCardSerialNo(string orgId)
        {
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_card", orgId);
        }

        public void SubmitForm(SysCardEntity SysCardEntity, string keyValue, string orgId)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                SysCardEntity.Modify(keyValue);
                this.Update(SysCardEntity);
            }
            else
            {
                SysCardEntity.OrganizeId = orgId;
                SysCardEntity.Create();
                this.Insert(SysCardEntity);
            };
        }

        /// <summary>
        /// theo số thẻ và tổ chức lấy thẻ thông tin(trư ớc giả sử khác nhau loại của thẻ số thẻ sẽ không trùng lặp)
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysCardEntity GetCardEntity(string cardno, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardNo == cardno && p.OrganizeId == orgId && p.zt == "1");
        }
        
        /// <summary>
        /// Lấy thẻ thực thể
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysCardEntity GetCardEntity(string cardType, string cardno, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardType == cardType && p.CardNo == cardno && p.OrganizeId == orgId && p.zt == "1");
        }

        public SysCardEntity GetCardEntity(int patid,string cardType, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardType == cardType && p.patid == patid && p.OrganizeId == orgId && p.zt=="1");
        }
    }
}


