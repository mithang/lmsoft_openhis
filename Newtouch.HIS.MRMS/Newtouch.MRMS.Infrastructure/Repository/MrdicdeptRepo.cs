using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo: chl
    /// Ngày: 2019-11-20 13:45
    /// Mô tả: Khoa bệnh án
    /// </summary>
    public class MrdicdeptRepo : RepositoryBase<MrdicdeptEntity>, IMrdicdeptRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrdicdeptRepo()
            
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MrdicdeptEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                // Sua: chi validate field duoc post (merge-skip-null, tien le G2/G18);
                // form post thieu ksmc thi giu ksmc cu, khong xoa ve NULL.
                if (entity.ksbm != null)
                {
                    if (string.IsNullOrWhiteSpace(entity.ksbm))
                    {
                        throw new FailedException("Mã khoa bệnh án không được để trống");
                    }
                    if (entity.ksbm.Length > 50)
                    {
                        throw new FailedException("Mã khoa bệnh án tối đa 50 ký tự");
                    }
                    if (this.IQueryable().Any(p => p.ksbm == entity.ksbm && p.Id != keyValue && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
                    {
                        throw new FailedException("Mã không được trùng lặp");
                    }
                }
                if (entity.ksmc != null && entity.ksmc.Length > 100)
                {
                    throw new FailedException("Tên khoa bệnh án tối đa 100 ký tự");
                }
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy thông tin khoa bệnh án.");
                }
                if (!string.IsNullOrWhiteSpace(entity.OrganizeId) && dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Không có quyền sửa dữ liệu của tổ chức khác.");
                }
                if (entity.ksbm != null)
                {
                    dbEntity.ksbm = entity.ksbm;
                }
                if (entity.ksmc != null)
                {
                    dbEntity.ksmc = entity.ksmc;
                }

                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
                // Tao moi: ksbm/ksmc bat buoc. Kiem tra trung chi trong to chuc hien tai
                // va chi voi row dang hoat dong (zt=1): ma cua row da xoa mem duoc dung lai,
                // dong nhat nhanh sua; dong thoi ksbm rong khong khop row NULL co san nua.
                if (string.IsNullOrWhiteSpace(entity.ksbm))
                {
                    throw new FailedException("Mã khoa bệnh án không được để trống");
                }
                if (entity.ksbm.Length > 50)
                {
                    throw new FailedException("Mã khoa bệnh án tối đa 50 ký tự");
                }
                if (string.IsNullOrWhiteSpace(entity.ksmc))
                {
                    throw new FailedException("Tên khoa bệnh án không được để trống");
                }
                if (entity.ksmc.Length > 100)
                {
                    throw new FailedException("Tên khoa bệnh án tối đa 100 ký tự");
                }
                if (this.IQueryable().Any(p => p.ksbm == entity.ksbm && p.OrganizeId == entity.OrganizeId && p.zt == "1"))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue, string organizeId = null)
        {
            // G21: FindEntity null -> loi sach thay vi NRE; truyen organizeId thi
            // chan xoa row cua to chuc khac.
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy thông tin khoa bệnh án.");
            }
            if (!string.IsNullOrWhiteSpace(organizeId) && dbEntity.OrganizeId != organizeId)
            {
                throw new FailedException("Không có quyền xóa dữ liệu của tổ chức khác.");
            }
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

        /// <summary>
        /// Lấy danh sách khoa bệnh án trong hộp thả xuống
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetDicDeptList(string organizeId)
        {
            string sql = @"select * from mr_dic_dept(nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            var result = this.FindList<MrdicdeptEntity>(sql, new SugarParameter[] {
                new SugarParameter("@organizeId", organizeId)
            });
            return result;
        }

        /// <summary>
        /// Lấy danh sách bệnh án phân trang
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword)
        {

            var sql = new StringBuilder();
            sql.Append("select * from mr_dic_dept(nolock) where zt != 0");
            List<SugarParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SugarParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or ksbm like @keyword");
                sql.Append(" or ksmc like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<MrdicdeptEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }
    }
}