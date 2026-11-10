using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    public class bl_ElementDomain_DetailRepo : RepositoryBase<bl_ElementDomain_DetailEntity>, Ibl_ElementDomain_DetailRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        public void SubmitForm(bl_ElementDomain_DetailEntity entity, string keyValue)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                if (!int.TryParse(keyValue, out int Id))
                {
                    throw new FailedException("Mã trường bệnh án không hợp lệ.");
                }
                var dbEntity = this.FindEntity(Id);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy trường bệnh án.");
                }
                // Chi copy field chuoi khi form co gui (bo-null): sua thieu field
                // khong duoc xoa gia tri cu (mau G2). Cac field int UI luon post
                // nen giu copy nhu cu (khong phan biet duoc 0-bi-thieu voi 0-that).
                dbEntity.Table_Column_No = entity.Table_Column_No;
                dbEntity.Table_Column_Code = entity.Table_Column_Code ?? dbEntity.Table_Column_Code;
                dbEntity.Table_Colunn_Name = entity.Table_Colunn_Name ?? dbEntity.Table_Colunn_Name;
                dbEntity.Table_Column_Type = entity.Table_Column_Type ?? dbEntity.Table_Column_Type;
                dbEntity.Element_Name = entity.Element_Name ?? dbEntity.Element_Name;
                dbEntity.Element_ID = entity.Element_ID ?? dbEntity.Element_ID;
                dbEntity.Element_Type = entity.Element_Type;
                dbEntity.Element_Type_Name = entity.Element_Type_Name ?? dbEntity.Element_Type_Name;
                dbEntity.Sybz = entity.Sybz;
                dbEntity.ysmxId = entity.ysmxId ?? dbEntity.ysmxId;
                dbEntity.AreValue = entity.AreValue ?? dbEntity.AreValue;
                dbEntity.Px = entity.Px;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                var check = this.FindEntity(p => p.Table_Column_Code == entity.Table_Column_Code && p.Zt == 1 && p.OrganizeId == entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("Trường bệnh án đã tồn tại!");
                }
                entity.Zt = 1;
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                throw new FailedException("Vui lòng chọn trường bệnh án cần xóa.");
            }
            if (!int.TryParse(keyValue, out int Id))
            {
                throw new FailedException("Mã trường bệnh án không hợp lệ.");
            }
            var dbEntity = this.FindEntity(Id);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy trường bệnh án.");
            }
            dbEntity.Zt = 0;
            this.Update(dbEntity);
        }

       
    }
}
