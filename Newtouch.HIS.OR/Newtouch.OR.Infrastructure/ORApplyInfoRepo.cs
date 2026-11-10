using Newtouch.OR.ManageSystem.Domain.Entity;
using DbConfigHelper;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using NewtouchHIS.Lib.Base.Model;
using System.Data;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 10:04
    /// Mô tả:Hồ sơ đơn đăng ký phẫu thuật
    /// </summary>
    public class ORApplyInfoRepo : RepositoryBase<ORApplyInfoEntity>, IORApplyInfoRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORApplyInfoRepo()
        {
            
        }

        /// <summary>
        /// G25: kiem tra do dai chuoi theo cot DB; qua dai -> loi ro rang
        /// thay vi loi cat chuoi cua SQL. Tra ve gia tri goc de gan truc tiep.
        /// </summary>
        private static string Len(string value, int max, string name)
        {
            if (value != null && value.Length > max)
            {
                throw new FailedException(name + " tối đa " + max + " ký tự");
            }
            return value;
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                // G25: sua chi trong cung co so (base FindEntity theo khoa
                // chinh nen sua duoc don cua co so khac).
                var dbEntity = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == entity.OrganizeId);
                // G23: nhanh edit truoc day bo qua entity (lay dbEntity roi
                // Modify + Update ngay) nen sua don qua SubmitForm1 la NO-OP.
                // Ghi that bang MapperTo (D-T1-FIX-001); khong tim thay -> loi
                // ro rang thay vi NRE o Update(null).
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy đơn phẫu thuật hợp lệ!");
                }
                // G25: MapperToSkipNull + them "sqzt","zt" vao ignoreProps —
                // base MapperTo copy ca null: entity tu form binder co zt=null
                // nen sua nao cung loi "Cannot insert NULL into column 'zt'",
                // va sqzt bi wipe ve null; zt/sqzt la truong vong doi, khong
                // doi qua SubmitForm (nhu ORArrangementRepo).
                var ignoreProps = new List<string>() { "Id", "OrganizeId", "CreateTime", "CreatorCode", "LastModifyTime", "LastModifierCode", "sqzt", "zt" };
                entity.MapperToSkipNull(dbEntity, ignoreProps);
                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            // G25: tao moi — so don bat buoc + khong trung trong cung co so
            // + do dai theo cot DB (base Insert tran: thieu org -> loi SQL
            // tho; Applyno rong tao dong rac; trung Applyno tao dong trung
            // vi khong co unique constraint tren Applyno).
            if (string.IsNullOrWhiteSpace(entity.Applyno))
            {
                throw new FailedException("Số đơn đăng ký không được để trống");
            }
            entity.Applyno = Len(entity.Applyno, 50, "Số đơn đăng ký");
            entity.zyh = Len(entity.zyh, 50, "Số nhập viện");
            entity.ssdm = Len(entity.ssdm, 50, "Mã phẫu thuật");
            entity.ssmcn = Len(entity.ssmcn, 100, "Tên phẫu thuật");
            entity.ysgh = Len(entity.ysgh, 50, "Mã bác sĩ mổ chính");
            entity.ysxm = Len(entity.ysxm, 100, "Tên bác sĩ mổ chính");
            entity.AnesCode = Len(entity.AnesCode, 50, "Phương pháp gây mê");
            entity.ssbw = Len(entity.ssbw, 100, "Vị trí phẫu thuật");
            entity.sqzt = Len(entity.sqzt, 2, "Trạng thái đơn");
            entity.isgl = Len(entity.isgl, 2, "Cách ly");
            if (this.IQueryable().Any(p => p.Applyno == entity.Applyno && p.zt == "1" && p.OrganizeId == entity.OrganizeId))
            {
                throw new FailedException("Số đơn đăng ký đã tồn tại.");
            }
            EntityHelper.Create(entity, true);
            return Insert(entity);
        }


        


        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue,string orgId,string ssmcn)
        {
            //return Delete(p => p.Id == keyValue);
            
            var dbEntity = this.FindEntity(p=>p.Applyno==keyValue && p.OrganizeId==orgId);
            // G25: xoa dong "var mc = dbEntity.ssmcn;" (carry G23) — chay
            // truoc null-check nen khong tim thay don thi NRE; mc khong dung.
            if (dbEntity != null)
            {
                dbEntity.sqzt = ((int)EnumSqzt.yqx).ToString();
                Update(dbEntity);

                // 7-fix: tham so hoa (truoc day noi zyh/xmmc/ysgh/OrganizeId/sssj truc tiep).
                // Giu nguyen ngu nghia cu: ExecuteSqlCommand tra ve so dong/khac 0 thi update.
                var pars = new SugarParameter[] {
                    new SugarParameter("@zyh", dbEntity.zyh ?? ""),
                    new SugarParameter("@ssmcn", ssmcn ?? ""),
                    new SugarParameter("@ysgh", dbEntity.ysgh ?? ""),
                    new SugarParameter("@orgId", dbEntity.OrganizeId ?? ""),
                    new SugarParameter("@sssj", (object)dbEntity.sssj ?? System.DBNull.Value)
                };
                var sqlcx = @"select 1 from " + DbNames.Name("CisDb", "Newtouch_CIS") + @".[dbo].[zy_lsyz]
where yzlx=3 and zyh=@zyh and xmmc=@ssmcn and ysgh=@ysgh and OrganizeId=@orgId and sssj=@sssj";
                var a = ExecuteSqlCommand(sqlcx, pars);
                if (a !=0)
                {
                    var sql = @"update " + DbNames.Name("CisDb", "Newtouch_CIS") + @".[dbo].[zy_lsyz]
                                set zt=0
                                where yzlx=3 and yzzt=0 and zyh=@zyh and xmmc=@ssmcn and ysgh=@ysgh and OrganizeId=@orgId and sssj=@sssj";
                    ExecuteSqlCommand(sql, pars);
                }
                
            }
            else
            {
                throw new FailedException("Thao tác thất bại, không tìm thấy đơn liên quan.");
            }
        }

        /// <summary>
        /// Xếp lịch phẫu thuật sau,Cập nhật trạng thái đơn đăng ký(2.Đã xếp lịch)
        /// Hủy Xếp lịch phẫu thuật sau,Cập nhật trạng thái đơn đăng ký(1.Đã đăng ký)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int UpdateSqzt(string keyValue,string sqzt )
        {
            // G23: don khong ton tai (khoa rong/khoa ao/khac co so — FindEntity
            // theo khoa chinh, khong loc co so) -> loi ro rang thay vi NRE.
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy đơn phẫu thuật hợp lệ!");
            }
            //properties
            dbEntity.sqzt = sqzt;
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }

    }
}