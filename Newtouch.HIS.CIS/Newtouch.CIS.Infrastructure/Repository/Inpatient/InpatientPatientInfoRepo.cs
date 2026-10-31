using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using Newtouch.CIS.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.CIS.Compat;
using Newtouch.Domain.ValueObjects.Outpatient;
using System.Collections.Generic;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 13:27
    /// Mô tả:Nhập việnThông tin bệnh nhân
    /// </summary>
    public class InpatientPatientInfoRepo : RepositoryBase<InpatientPatientInfoEntity>, IInpatientPatientInfoRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public InpatientPatientInfoRepo()
            : base()
        {

        }

        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void SubmitForm(InpatientPatientInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// Cập nhậtNhập việnbệnh nhânởviệnCờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        public void UpdateInpatientStatus(string orgId, string zyh, string zybz, string ryzd, string ryzdmc, string brxz, string brxzmc, string lxr, string lxrgx, string lxrdh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return;
            }
            var entity = this.IQueryable().FirstOrDefault(p => p.OrganizeId == orgId && p.zyh == zyh && p.zt == "1");
            if (entity == null) return;
            if (entity.zybz.ToString() != zybz && !string.IsNullOrWhiteSpace(zybz))
            {
                var zybzArr = new[] {
                    ((int)EnumZYBZ.Djz).ToString()
                    ,((int)EnumZYBZ.Ycy).ToString()
                    ,((int)EnumZYBZ.Wry).ToString()
                };
                if (!zybzArr.Contains(zybz))
                {
                    throw new FailedCodeException("destination status update forbidden");
                }
                entity.zybz = Convert.ToInt32(zybz);
            }
            if (entity.zddm != ryzd && !string.IsNullOrWhiteSpace(ryzd))
            {
                entity.zddm = ryzd;
            }
            if (entity.zdmc != ryzdmc && !string.IsNullOrWhiteSpace(ryzdmc))
            {
                entity.zdmc = ryzdmc;
            }
            if (entity.brxzdm != brxz && !string.IsNullOrWhiteSpace(brxz))
            {
                entity.brxzdm = brxz;
            }
            if (entity.brxzmc != brxzmc && !string.IsNullOrWhiteSpace(brxzmc))
            {
                entity.brxzmc = brxzmc;
            }
            if (entity.lxr != lxr && lxr != null)
            {
                entity.lxr = lxr;
            }
            if (entity.lxrgx != lxrgx && lxrgx != null)
            {
                entity.lxrgx = lxrgx;
            }
            if (entity.lxrdh != lxrdh && lxrdh != null)
            {
                entity.lxrdh = lxrdh;
            }

            entity.Modify();
            this.Update(entity);
        }

        /// <summary>
        /// thông quaSố nhập việnLấyNhập việnThông tin bệnh nhân
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientPatientInfoEntity GetByZyh(string organizeId, string zyh)
        {
            return this.FindEntity(p => p.zyh == zyh && p.OrganizeId == organizeId && p.zt == "1");
        }

        /// <summary>
        /// Lấybệnh nhânThông tin cơ bản
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xm">Họ tên</param>
        /// <param name="organizeId">Tổ chứcID</param>
        /// <returns></returns>
        public InpatientPatientInfoEntity SelectData(string zyh, string xm, string organizeId)
        {
            const string sql = "SELECT *  FROM [dbo].[zy_brxxk](NOLOCK) WHERE zyh=@zyh AND xm=@xm AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId",organizeId),
                new SqlParameter("@xm",xm),
                new SqlParameter("@zyh",zyh),
            };
            return FirstOrDefault<InpatientPatientInfoEntity>(sql, param);
        }

        //LấyKhu bệnhdanh sách
        public List<InpatientAreaVO> TreeViewdata(string orgId)
        {
            var sql = @"select bqCode,bqmc from [NewtouchHIS_Base].[dbo].[xt_bq] where OrganizeId='" + orgId+"' and zt=1";
            return FindList<InpatientAreaVO>(sql);
        }
    }
}