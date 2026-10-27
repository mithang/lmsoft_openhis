using System.Collections.Generic;
using Newtouch.Infrastructure.TSql;
using Newtouch.Infrastructure.EF;
using Newtouch.CIS.Compat;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.IDomainServices.DeptStorageManage;
using Newtouch.Domain.IRepository.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;
using Newtouch.Infrastructure;

namespace Newtouch.DomainServices.DeptStorageManage
{
    /// <summary>
    /// Tồn khoBảo trì
    /// </summary>
    public class KfKcxxDmnService : DmnServiceBase, IKfKcxxDmnService
    {
        private readonly IKfKcxxRepo _kfKcxxRepo;
        // private readonly IWzProductRepo _wzProductRepo;

        public KfKcxxDmnService() : base()
        {
        }

         /// <summary>
         /// SửaTồn khoSố lượng(thêmTồn kho)，khôngSửađôngkếtSố lượng  Quay lạihìnhhưởngdòng
         /// </summary>
         /// <param name="productId"></param>
         /// <param name="pc"></param>
         /// <param name="ph"></param>
         /// <param name="warehouseId"></param>
         /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
         /// <param name="organizeId"></param>
         /// <param name="userCode"></param>
         /// <returns>Quay lạihìnhhưởngdòng</returns>
         public int UpdateKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
         {
             if (sl >= 0) return AddKcsl(productId, pc, ph, warehouseId, sl, organizeId, userCode);
             var subtractResult = JustSubtractKcsl(new UpdateKcslDTO { organizeId = organizeId, sl = (-1 * sl), userCode = userCode, ks = warehouseId,pc = pc, ph = ph, productId = productId });
             if (!string.IsNullOrWhiteSpace(subtractResult)) throw new System.Exception(subtractResult);
             return 1;
         }
//
//         /// <summary>
//         /// SửaTồn khoSố lượng，khôngSửađôngkếtSố lượng  Phương pháplàlôlượngSửanhaucùngLôSố lôcủavật tư，慎dùng
//         /// </summary>
//         /// <param name="updateKcslDto"></param>
//         /// <returns></returns>
//         public string UpdateKcsl(List<UpdateKcslDTO> updateKcslDto)
//         {
//             if (updateKcslDto == null || updateKcslDto.Count <= 0) return "vui lòngtruyềnvàocầnSửaTồn khothông tin";
//             using (var db = new EFDbTransaction().BeginTrans())
//             {
//                 foreach (var p in updateKcslDto)
//                 {
//                     if (UpdateKcsl(p.productId, p.pc, p.ph, p.warehouseId, p.sl, p.organizeId, p.userCode) <= 0)
//                     {
//                         var wz = _wzProductRepo.FindEntity(o => o.Id == p.productId && o.OrganizeId == p.organizeId && o.zt == ((int)Enumzt.Enable).ToString());
//                         return string.Format("Sửavật tư【{0}】củaTồn khothất bại", wz.name ?? "");
//                     }
//                 }
//                 db.Commit();
//             }
//             return "";
//         }
//
        /// <summary>
        /// đuổithêmTồn kho，khôngSửađôngkếtSố lượng Quay lạihìnhhưởngdòng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">Đơn vị nhỏ nhấtSố lượng  phảicầnlàđangsố</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Quay lạihìnhhưởngdòng</returns>
        public int AddKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
        {
            if (sl <= 0) throw new System.Exception("Số lượngphảicầnlàđangsố");
            const string sql = @"
UPDATE dbo.dept_kcxx 
SET kcsl=kcsl+@sl, LastModifierCode=@userCode, LastModifyTime=GETDATE()
WHERE Id=(SELECT TOP 1 Id FROM dbo.dept_kcxx WHERE productId=@proId 
    AND ks=@warehouseId 
    AND OrganizeId=@OrganizeId
    AND pc=@pc
    AND ph=@ph
    AND zt='1' 
)
AND ks=@warehouseId 
AND OrganizeId=@OrganizeId
AND pc=@pc
AND ph=@ph
AND zt='1'
SELECT @@ROWCOUNT ;
";
            var param = new DbParameter[]
            {
                new SqlParameter("@proId", productId),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@sl", sl),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph)
            };
            return FirstOrDefault<int>(sql, param);
        }
//
         // /// <summary>
         // /// SửaTồn khoSố lượng，khôngSửađôngkếtSố lượng
         // /// </summary>
         // /// <param name="updateKcslDto"></param>
         // /// <returns></returns>
         // public string JustSubtractKcsl(List<UpdateKcslDTO> updateKcslDto)
         // {
         //     if (updateKcslDto == null || updateKcslDto.Count <= 0) return "Tồn khothông tinkhông thểlàtrống";
         //     using (var db = new EFDbTransaction().BeginTrans())
         //     {
         //         foreach (var p in updateKcslDto)
         //         {
         //             var result = JustSubtractKcsl(p);
         //             if (!string.IsNullOrWhiteSpace(result)) return result;
         //         }
         //         db.Commit();
         //     }
         //     return "";
         // }
//
//         /// <summary>
//         /// SửaTồn khoSố lượng，khôngSửađôngkếtSố lượng
//         /// </summary>
//         /// <param name="updateKcslDto"></param>
//         /// <param name="db"></param>
//         /// <returns></returns>
//         public string JustSubtractKcslNoTrans(List<UpdateKcslDTO> updateKcslDto, Newtouch.Infrastructure.EF.EFDbTransaction db = null)
//         {
//             if (updateKcslDto == null || updateKcslDto.Count <= 0) return "Tồn khothông tinkhông thểlàtrống";
//             foreach (var p in updateKcslDto)
//             {
//                 var result = JustSubtractKcsl(p, db);
//                 if (!string.IsNullOrWhiteSpace(result)) return result;
//             }
//             return "";
//         }
//
         /// <summary>
         /// SửaTồn khoSố lượng，khôngSửađôngkếtSố lượng
         /// </summary>
         /// <param name="updateKcslDto"></param>
         /// <param name="db"></param>
         /// <returns></returns>
         public string JustSubtractKcsl(UpdateKcslDTO updateKcslDto, Newtouch.Infrastructure.EF.EFDbTransaction db = null)
         {
             if (updateKcslDto == null) return "Tồn khothông tinkhông thểđể trống";
             var param = new DbParameter[]
             {
                 new SqlParameter("@proId",updateKcslDto.productId),
                 new SqlParameter("@warehouseId", updateKcslDto.ks),
                 new SqlParameter("@OrganizeId", updateKcslDto.organizeId),
                 new SqlParameter("@userCode", updateKcslDto.userCode),
                 new SqlParameter("@sl", updateKcslDto.sl),
                 new SqlParameter("@pc", updateKcslDto.pc),
                 new SqlParameter("@ph", updateKcslDto.ph)
             };
             return db != null ? db.FirstOrDefault<string>(StockOperate.just_subtract_kcsl, param)
                 : FirstOrDefault<string>(StockOperate.just_subtract_kcsl, param);
         }
//
//         /// <summary>
//         /// giảmTồn khoSố lượng、đôngkếtSố lượng（Xuất khocó thểdùng）
//         /// </summary>
//         /// <param name="productId"></param>
//         /// <param name="pc"></param>
//         /// <param name="ph"></param>
//         /// <param name="warehouseId"></param>
//         /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
//         /// <param name="organizeId"></param>
//         /// <param name="userCode"></param>
//         /// <returns></returns>
//         public string SubtractKcslAndDjsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
//         {
//             var param = new DbParameter[]
//             {
//                 new SqlParameter("@proId", productId),
//                 new SqlParameter("@warehouseId", warehouseId),
//                 new SqlParameter("@OrganizeId", organizeId),
//                 new SqlParameter("@userCode", userCode),
//                 new SqlParameter("@sl", sl),
//                 new SqlParameter("@pc", pc),
//                 new SqlParameter("@ph", ph)
//             };
//             return FirstOrDefault<string>(StockOperate.subtract_freeze_and_kcsl, param);
//         }
//
//         /// <summary>
//         /// Rã đông
//         /// </summary>
//         /// <param name="productId"></param>
//         /// <param name="pc"></param>
//         /// <param name="ph"></param>
//         /// <param name="warehouseId"></param>
//         /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
//         /// <param name="organizeId"></param>
//         /// <param name="userCode"></param>
//         /// <returns></returns>
//         public string Unfreeze(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
//         {
//             var param = new DbParameter[]
//             {
//                 new SqlParameter("@proId", productId),
//                 new SqlParameter("@warehouseId", warehouseId),
//                 new SqlParameter("@OrganizeId", organizeId),
//                 new SqlParameter("@userCode", userCode),
//                 new SqlParameter("@sl", sl),
//                 new SqlParameter("@pc", pc),
//                 new SqlParameter("@ph", ph)
//             };
//             return FirstOrDefault<string>(StockOperate.just_unfreeze, param);
//         }
//
//         /// <summary>
//         /// đôngkếtTồn kho
//         /// </summary>
//         /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
//         /// <param name="pc"></param>
//         /// <param name="ph"></param>
//         /// <param name="productId"></param>
//         /// <param name="organizeId"></param>
//         /// <param name="warehouseId"></param>
//         /// <param name="userCode"></param>
//         /// <returns></returns>
//         public string FrozenKc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode)
//         {
//             var param = new DbParameter[]
//             {
//                 new SqlParameter("@proId", productId),
//                 new SqlParameter("@pc", pc),
//                 new SqlParameter("@ph", ph),
//                 new SqlParameter("@OrganizeId", organizeId),
//                 new SqlParameter("@warehouseId", warehouseId),
//                 new SqlParameter("@userCode", userCode),
//                 new SqlParameter("@sl", sl)
//             };
//             return FindList<string>(StockOperate.just_frozen_kcsl, param.ToArray()).FirstOrDefault();
//         }

        // /// <summary>
        // /// ngoàibộNhập kho khấuTồn kho(Hủy bỏ)
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="productId"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="warehouseId"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // public string WbrkKkc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode)
        // {
        //     var param = new DbParameter[]
        //     {
        //         new SqlParameter("@pc", pc),
        //         new SqlParameter("@ph", ph),
        //         new SqlParameter("@OrganizeId", organizeId),
        //         new SqlParameter("@warehouseId", warehouseId),
        //         new SqlParameter("@proId", productId),
        //         new SqlParameter("@zt", ((int) Enumzt.Enable).ToString()),
        //         new SqlParameter("@sl", sl),
        //         new SqlParameter("@userCode", userCode)
        //     };
        //     return FindList<string>(StockOperate.cancel_subtract_kcsl, param.ToArray()).FirstOrDefault();
        // }

        // /// <summary>
        // /// Thu hồiNhập kho（trực tiếpXuất khoHủy bỏ） nếucầnSử dụngPhương phápvui lòngbộtrênviệcvụ，Phương pháptrongkhônglàmvềcuộn
        // /// </summary>
        // /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
        // /// <param name="pc"></param>
        // /// <param name="ph"></param>
        // /// <param name="productId"></param>
        // /// <param name="organizeId"></param>
        // /// <param name="rkbm"></param>
        // /// <param name="ckbm"></param>
        // /// <param name="userCode"></param>
        // /// <returns></returns>
        // public string RevokeInstorage(int sl, string pc, string ph, string productId, string organizeId, string rkbm, string ckbm, string userCode)
        // {
        //     //Nhập khoKhoa phòngkhấuTồn kho
        //     var substractResult = JustSubtractKcsl(new UpdateKcslDTO { organizeId = organizeId, pc = pc, ph = ph, productId = productId, sl = sl, userCode = userCode, warehouseId = rkbm });
        //     if (!string.IsNullOrWhiteSpace(substractResult)) return substractResult;
        //
        //     //Xuất khoKhoa phòngcònTồn kho
        //     if (UpdateKcsl(productId, pc, ph, ckbm, sl, organizeId, userCode) <= 0)
        //     {
        //         return "cònTồn khothất bại";
        //     }
        //     return "";
        // }

        /// <summary>
        /// LấyTồn khothông tin kết chuyểndùng
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VKcxxEntity> GetKcxxForJz(string warehouseId, string organizeId)
        {
            const string sql = @"
SELECT kcxx.productId, kcxx.ph, kcxx.pc, kcxx.yxq, kcxx.kcsl, ISNULL(CONVERT(NUMERIC(11,4),wz.lsj*kcxx.zhyz),0) bmlsj, kcxx.jj, kcxx.zhyz
FROM dbo.kf_kcxx(NOLOCK) kcxx
INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.productId=kcxx.productId AND rpw.warehouseId=kcxx.warehouseId AND rpw.OrganizeId=kcxx.OrganizeId AND rpw.zt='1'
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=kcxx.productId AND wz.OrganizeId=kcxx.OrganizeId AND wz.zt='1'
WHERE kcxx.OrganizeId=@OrganizeId
AND kcxx.warehouseId=@warehouseId
AND kcxx.zt=@zt
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@zt", ((int) Enumzt.Enable).ToString()),
            };
            return FindList<VKcxxEntity>(sql, param);
        }

        //        /// <summary>
        //        /// Thu hồiNhập kho（trực tiếpXuất khoHủy bỏ）
        //        /// </summary>
        //        /// <param name="sl">Đơn vị nhỏ nhấtSố lượng</param>
        //        /// <param name="pc"></param>
        //        /// <param name="ph"></param>
        //        /// <param name="productId"></param>
        //        /// <param name="organizeId"></param>
        //        /// <param name="rkbm"></param>
        //        /// <param name="ckbm"></param>
        //        /// <param name="userCode"></param>
        //        /// <returns></returns>
        //        public string RevokeInstorage(int sl, string pc, string ph, string productId, string organizeId, string rkbm, string ckbm, string userCode)
        //        {
        //            const string sql = @"
        //DECLARE @rkkcsl INT;--Nhập khoTồn kho
        //DECLARE @rkdjsl INT;--Nhập khođôngkếtTồn kho
        //DECLARE @res INT;
        //SELECT @rkkcsl=kcsl, @rkdjsl=djsl FROM dbo.kf_kcxx WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt=@zt AND warehouseId=@rkbm
        //IF @sl <= (@rkkcsl-@rkdjsl)
        //BEGIN
        //	UPDATE dbo.kf_kcxx 
        //	SET kcsl=kcsl-@sl, LastModifierCode=@userCode, LastModifyTime=GETDATE() 
        //	WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt=@zt AND warehouseId=@rkbm;
        //	SELECT @res=@@ROWCOUNT;
        //	IF @res>0
        //	BEGIN
        //		UPDATE dbo.kf_kcxx
        //		SET kcsl=kcsl+@sl, LastModifierCode=@userCode, LastModifyTime=GETDATE()
        //		WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt=@zt AND warehouseId=@ckbm;
        //		SELECT @res=@@ROWCOUNT ;
        //		IF @res>0
        //		BEGIN
        //			SELECT '';
        //		END
        //		ELSE
        //		BEGIN
        //			SELECT 'Xuất khoKhoa phòngvàoTồn khothất bại';
        //		END
        //	END 
        //	ELSE
        //	BEGIN
        //		SELECT 'Nhập khoKhoa phòngraTồn khothất bại';
        //	END
        //END
        //ELSE 
        //BEGIN
        //	SELECT 'Tồn khokhôngđủ';
        //END 
        //";
        //            var param = new DbParameter[]
        //            {
        //                new SqlParameter("@pc", pc),
        //                new SqlParameter("@ph", ph),
        //                new SqlParameter("@OrganizeId", organizeId),
        //                new SqlParameter("@rkbm", rkbm),
        //                new SqlParameter("@ckbm", ckbm),
        //                new SqlParameter("@proId", productId),
        //                new SqlParameter("@zt", ((int) Enumzt.Enable).ToString()),
        //                new SqlParameter("@sl", sl),
        //                new SqlParameter("@userCode",userCode)
        //            };
        //            return FindList<string>(sql, param.ToArray()).FirstOrDefault();
        //        }
    }
}
