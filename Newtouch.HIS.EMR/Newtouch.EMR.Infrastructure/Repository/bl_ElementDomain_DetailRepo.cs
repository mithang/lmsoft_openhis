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
                int Id = Convert.ToInt32(keyValue);
                var dbEntity = this.FindEntity(Id);
                dbEntity.Table_Column_No = entity.Table_Column_No;
                dbEntity.Table_Column_Code = entity.Table_Column_Code;
                dbEntity.Table_Colunn_Name = entity.Table_Colunn_Name;
                dbEntity.Table_Column_Type = entity.Table_Column_Type;
                dbEntity.Element_Name = entity.Element_Name;
                dbEntity.Element_ID = entity.Element_ID;
                dbEntity.Element_Type = entity.Element_Type;
                dbEntity.Element_Type_Name = entity.Element_Type_Name;
                dbEntity.Sybz = entity.Sybz;
                dbEntity.ysmxId = entity.ysmxId;
                dbEntity.AreValue = entity.AreValue;
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
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(Convert.ToInt32(keyValue));
                dbEntity.Zt = 0;
                this.Update(dbEntity);
            }
        }

       
    }
}
