using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using System.Text;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Linq;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2018-09-11 10:46
    /// Mô tả:Bảng quan hệ văn bản bệnh án của bệnh nhân
    /// </summary>
    public class ZymeddocsrelationRepo : RepositoryBase<ZymeddocsrelationEntity>, IZymeddocsrelationRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        public List<ZymeddocsrelationEntity> GetTreeList(string orgId, string zyh)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId && p.zt == "1"&&p.zyh==zyh).OrderBy(p => p.CreateTime).ToList();
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(ZymeddocsrelationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
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
        /// Cập nhật quan hệ bệnh án của bệnh nhân
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="zyh"></param>
        /// <param name="BlId"></param>
        /// <param name="BllxId"></param>
        /// <param name="OrgId"></param>
        public void SubmitEntity(ZymeddocsrelationEntity entity)
        {
            //Tìm bệnh án duy nhất
            var ety = FindEntity(p => p.blId == entity.blId && p.OrganizeId == entity.OrganizeId);

            if (ety != null)
            {
                //Kiểm tra thông tin bệnh án
                if (ety.zyh == entity.zyh && ety.mbId == entity.mbId)
                {
                    ety.blzt = entity.blzt;
                    ety.blrq = entity.blrq;
                    ety.blmc = entity.blmc;
                    ety.blbt = entity.blbt;
                    ety.Memo = entity.Memo;
                    ety.ysgh = entity.ysgh;
                    ety.ysxm = entity.ysxm;
                    ety.zt = entity.zt;

                    var dbEntity = this.FindEntity(ety.Id);
                    this.Update(dbEntity);
                }
                else
                {
                    throw new FailedException("Thông tin bệnh án bất thường");
                }
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                this.Insert(entity);
            }
        }

    }
}
