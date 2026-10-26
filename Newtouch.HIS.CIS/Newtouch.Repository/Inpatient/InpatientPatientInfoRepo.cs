using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.ValueObjects.Outpatient;
using System.Collections.Generic;

namespace Newtouch.Repository
{
    /// <summary>
    /// Tạo bởi: 张冬林
    /// Ngày: 2018-06-28 13:27
    /// Mô tả: Thông tin bệnh nhân nội trú
    /// </summary>
    public class InpatientPatientInfoRepo : RepositoryBase<InpatientPatientInfoEntity>, IInpatientPatientInfoRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public InpatientPatientInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
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
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

        /// <summary>
        /// Cập nhật cờ đang nằm viện của bệnh nhân nội trú
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
        /// Lấy thông tin bệnh nhân nội trú theo số nhập viện
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public InpatientPatientInfoEntity GetByZyh(string organizeId, string zyh)
        {
            return this.FindEntity(p => p.zyh == zyh && p.OrganizeId == organizeId && p.zt == "1");
        }

        /// <summary>
        /// Lấy thông tin cơ bản của bệnh nhân
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xm">Họ tên</param>
        /// <param name="organizeId">ID tổ chức</param>
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

        //Lấy danh sách khu bệnh
        public List<InpatientAreaVO> TreeViewdata(string orgId)
        {
            var sql = @"select bqCode,bqmc from [NewtouchHIS_Base].[dbo].[xt_bq] where OrganizeId='" + orgId+"' and zt=1";
            return FindList<InpatientAreaVO>(sql);
        }
    }
}