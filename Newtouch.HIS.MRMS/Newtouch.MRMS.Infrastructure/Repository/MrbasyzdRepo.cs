using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;

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
    /// Ngày: 2019-11-20 11:05
    /// Mô tả: Bảng chẩn đoán trang đầu bệnh án
    /// </summary>
    public class MrbasyzdRepo : RepositoryBase<MrbasyzdEntity>, IMrbasyzdRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
        public MrbasyzdRepo()
            
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(MrbasyzdEntity entity, string keyValue)
        {
            if (entity == null)
            {
                throw new FailedException("Thông tin chẩn đoán không được để trống.");
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy thông tin chẩn đoán.");
                }
                var curOrg = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId ?? "";
                if (!string.IsNullOrWhiteSpace(curOrg) && dbEntity.OrganizeId != curOrg)
                {
                    throw new FailedException("Không có quyền sửa dữ liệu của tổ chức khác.");
                }
                // G22: edit truoc day (1) gan truc tiep 8 field nen field nao
                // khong post se bi xoa ve NULL (loi SQL tho NULL zt), (2) roi
                // roi xuong khoi tao + INSERT tiep nen moi lan sua lai tao
                // them 1 dong trung. Nay merge cac field da post roi return.
                MrmsMergeHelper.MergeSkipNull(entity, dbEntity);
                dbEntity.Modify(keyValue);
                Update(dbEntity);
                return;
            }
            // G22: create truoc day khong kiem tra gi — thieu BAH/ZYH thi
            // loi SQL tho "Cannot insert the value NULL...".
            var missing = new System.Collections.Generic.List<string>();
            if (string.IsNullOrWhiteSpace(entity.BAH))
            {
                missing.Add("Số bệnh án không được để trống");
            }
            else if (entity.BAH.Length > 100)
            {
                missing.Add("Số bệnh án tối đa 100 ký tự");
            }
            if (string.IsNullOrWhiteSpace(entity.ZYH))
            {
                missing.Add("Số nội trú không được để trống");
            }
            else if (entity.ZYH.Length > 50)
            {
                missing.Add("Số nội trú tối đa 50 ký tự");
            }
            if (missing.Count > 0)
            {
                throw new FailedException("Thông tin chẩn đoán chưa đầy đủ: " + string.Join("; ", missing) + ".");
            }
            entity.Create(true);
            Insert(entity);
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue, string organizeId = null)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new FailedException("Không tìm thấy thông tin chẩn đoán.");
            }
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy thông tin chẩn đoán.");
            }
            var org = organizeId;
            if (string.IsNullOrWhiteSpace(org))
            {
                org = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId ?? "";
            }
            if (!string.IsNullOrWhiteSpace(org) && dbEntity.OrganizeId != org)
            {
                throw new FailedException("Không có quyền xóa dữ liệu của tổ chức khác.");
            }
            return Delete(p => p.Id == keyValue);
        }
    }
}