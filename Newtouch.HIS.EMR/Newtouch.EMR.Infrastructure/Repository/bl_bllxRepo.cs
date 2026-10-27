using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using Newtouch.Infrastructure;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2018-08-30 11:31
    /// Mô tả：Nhóm lớn bệnh án
    /// </summary>
    public class bl_bllxRepo : RepositoryBase<bl_bllxEntity>, Ibl_bllxRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>


        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(bl_bllxEntity entity, string keyValue)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var check = this.FindEntity(p => p.bllx == entity.bllx && p.zt == "1" && p.Id != keyValue && p.OrganizeId==entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("Mã định danh nhóm lớn bệnh án này đã tồn tại!");
                }
                if (string.IsNullOrWhiteSpace(entity.ParentId))
                {
                    //dbEntity.bllxcode = entity.bllxcode;
                    dbEntity.relTB = entity.relTB;
                    dbEntity.MenuLev = entity.MenuLev;
                    dbEntity.MenuLevName = entity.MenuLevName;
                    dbEntity.RelDutys = entity.RelDutys;
                    sql = @" update [bl_bllx] 
set MenuLev=@MenuLev,MenuLevName=@MenuLevName,RelDutys=@RelDutys,mzbz=@mzbz,[LastModifyTime]=getdate(),[LastModifierCode]='auto'
where ParentId=@id and zt='1' and OrganizeId=@orgId ";
                    this.ExecuteSqlCommand(sql,new SugarParameter[] {
                        new SugarParameter("@MenuLev",entity.MenuLev),
                        new SugarParameter("@MenuLevName",entity.MenuLevName??string.Empty),
                        new SugarParameter("@RelDutys",entity.RelDutys??string.Empty),
                        new SugarParameter("@id",dbEntity.Id),
                        new SugarParameter("@orgId",dbEntity.OrganizeId),
                        new SugarParameter("@mzbz",entity.mzbz),
                    });
                }
                dbEntity.mzbz = entity.mzbz;
                dbEntity.bllxmc = entity.bllxmc;
                dbEntity.px = entity.px;
                dbEntity.zt = entity.zt == "true" ? "1" : "0";
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime= DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                var check = this.FindEntity(p => p.bllx == entity.bllx && p.zt == "1" && p.OrganizeId==entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("Mã định danh nhóm lớn bệnh án này đã tồn tại!");
                }
                if (!string.IsNullOrWhiteSpace(entity.ParentId))
                {
                    bl_bllxEntity pety = this.FindEntity(p => p.Id == entity.ParentId && p.zt == "1");
                    if (pety != null)
                    {
                        entity.bllx = pety.bllx + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("bl_bllx.bllx", entity.OrganizeId, "{0:D3}", false);
                        entity.relTB = pety.relTB;
                        entity.RelDutys = pety.RelDutys;
                        entity.bllxcode = pety.bllxcode;
                        entity.MenuLev = pety.MenuLev;
                    }
                    else
                    {
                        throw new FailedException("Nhóm lớn bệnh án không tồn tại, vui lòng chọn lại!");
                    }
                }
                else
                {
                    sql = "select 1 from " + entity.relTB;
                    try
                    {
                        int tbcheck = this.ExecuteSqlCommand(sql);
                    }
                    catch (Exception ex)
                    {
                        throw new FailedException("Vui lòng kiểm tra liên kết bảng nghiệp vụ!" + ex.Message);
                    }
                }
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsRoot = string.IsNullOrWhiteSpace(entity.ParentId) == true ? "1" : "0";
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue,string orgId,string user)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(orgId))
            {
                bl_bllxEntity ety = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == orgId && p.zt == "1");
                if (ety!=null)
                {
                    ety.zt = "0";
                    ety.LastModifierCode = user;
                    ety.LastModifyTime = DateTime.Now;
                    this.Update(ety);
                }
                else
                {
                    throw new FailedException("Loại bệnh án không tồn tại hoặc đã bị xóa, vui lòng chọn lại!");
                }
            }
            else
            {
                throw new FailedException("Loại bệnh án không tồn tại hoặc đã bị xóa, vui lòng chọn lại!");
            }
        }

    }
}
