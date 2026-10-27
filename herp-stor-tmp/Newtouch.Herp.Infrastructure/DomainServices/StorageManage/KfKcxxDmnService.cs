using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using SqlSugar;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.DomainServices.StorageManage
{
    /// <summary>
    /// 库存维护
    /// </summary>
    public class KfKcxxDmnService : DmnServiceBase, IKfKcxxDmnService
    {
        private readonly IKfKcxxRepo _kfKcxxRepo;
        private readonly IWzProductRepo _wzProductRepo;

        public KfKcxxDmnService(IKfKcxxRepo kfKcxxRepo, IWzProductRepo wzProductRepo)
        {
            _kfKcxxRepo = kfKcxxRepo;
            _wzProductRepo = wzProductRepo;
        }

        /// <summary>
        /// 修改库存数量(加库存)，不修改冻结数量  返回影响行
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>返回影响行</returns>
        public int UpdateKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
        {
            if (sl >= 0) return AddKcsl(productId, pc, ph, warehouseId, sl, organizeId, userCode);
            var subtractResult = JustSubtractKcsl(new UpdateKcslDTO { organizeId = organizeId, sl = (-1 * sl), userCode = userCode, warehouseId = warehouseId, pc = pc, ph = ph, productId = productId });
            if (!string.IsNullOrWhiteSpace(subtractResult)) throw new System.Exception(subtractResult);
            return 1;
        }

        /// <summary>
        /// 修改库存数量，不修改冻结数量  该方法为批量修改相同批次批号的物资，慎用
        /// </summary>
        /// <param name="updateKcslDto"></param>
        /// <returns></returns>
        public string UpdateKcsl(List<UpdateKcslDTO> updateKcslDto)
        {
            if (updateKcslDto == null || updateKcslDto.Count <= 0) return "Vui lòng truyền thông tin tồn kho cần sửa";
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                foreach (var p in updateKcslDto)
                {
                    if (UpdateKcsl(p.productId, p.pc, p.ph, p.warehouseId, p.sl, p.organizeId, p.userCode) <= 0)
                    {
                        var wz = _wzProductRepo.FindEntity(o => o.Id == p.productId && o.OrganizeId == p.organizeId && o.zt == ((int)Enumzt.Enable).ToString());
                        return string.Format("Sửa tồn kho của vật tư 【{0}】 thất bại", wz.name ?? "");
                    }
                }
                db.Commit();
            }
            return "";
        }

        /// <summary>
        /// 追加库存，不修改冻结数量 返回影响行
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">最小单位数量  必须为正数</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>返回影响行</returns>
        public int AddKcsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
        {
            if (sl <= 0) throw new System.Exception("Số lượng phải là số dương");
            const string sql = @"
UPDATE dbo.kf_kcxx 
SET kcsl=kcsl+@sl, LastModifierCode=@userCode, LastModifyTime=GETDATE()
WHERE Id=(SELECT TOP 1 Id FROM dbo.kf_kcxx WHERE productId=@proId 
    AND warehouseId=@warehouseId 
    AND OrganizeId=@OrganizeId
    AND pc=@pc
    AND ph=@ph
    AND zt='1' 
)
AND warehouseId=@warehouseId 
AND OrganizeId=@OrganizeId
AND pc=@pc
AND ph=@ph
AND zt='1'
SELECT @@ROWCOUNT ;
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@proId", productId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@sl", sl),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph)
            };
            return FirstOrDefaultScalar<int>(sql, param);
        }

        /// <summary>
        /// 修改库存数量，不修改冻结数量
        /// </summary>
        /// <param name="updateKcslDto"></param>
        /// <returns></returns>
        public string JustSubtractKcsl(List<UpdateKcslDTO> updateKcslDto)
        {
            if (updateKcslDto == null || updateKcslDto.Count <= 0) return "Thông tin tồn kho không được để trống";
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                foreach (var p in updateKcslDto)
                {
                    var result = JustSubtractKcsl(p);
                    if (!string.IsNullOrWhiteSpace(result)) return result;
                }
                db.Commit();
            }
            return "";
        }

        /// <summary>
        /// 修改库存数量，不修改冻结数量
        /// </summary>
        /// <param name="updateKcslDto"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public string JustSubtractKcslNoTrans(List<UpdateKcslDTO> updateKcslDto, Newtouch.Infrastructure.EF.EFDbTransaction db = null)
        {
            if (updateKcslDto == null || updateKcslDto.Count <= 0) return "Thông tin tồn kho không được để trống";
            foreach (var p in updateKcslDto)
            {
                var result = JustSubtractKcsl(p, db);
                if (!string.IsNullOrWhiteSpace(result)) return result;
            }
            return "";
        }

        /// <summary>
        /// 修改库存数量，不修改冻结数量
        /// </summary>
        /// <param name="updateKcslDto"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public string JustSubtractKcsl(UpdateKcslDTO updateKcslDto, Newtouch.Infrastructure.EF.EFDbTransaction db = null)
        {
            if (updateKcslDto == null) return "Thông tin tồn kho không được để trống";
            var param = new SugarParameter[]
            {
                new SugarParameter("@proId",updateKcslDto.productId),
                new SugarParameter("@warehouseId", updateKcslDto.warehouseId),
                new SugarParameter("@OrganizeId", updateKcslDto.organizeId),
                new SugarParameter("@userCode", updateKcslDto.userCode),
                new SugarParameter("@sl", updateKcslDto.sl),
                new SugarParameter("@pc", updateKcslDto.pc),
                new SugarParameter("@ph", updateKcslDto.ph)
            };
            if (db != null)
            {
                // The caller owns the transaction: run on the transaction handle's
                // provider (same SqlSugarScope connection within this request flow).
                return db.Db.Ado.SqlQuery<string>(just_subtract_kcsl, param).FirstOrDefault();
            }
            return FirstOrDefaultScalar<string>(just_subtract_kcsl, param);
        }

        /// <summary>
        /// 减库存数量、冻结数量（出库可用）
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubtractKcslAndDjsl(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@proId", productId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@sl", sl),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph)
            };
            return FirstOrDefaultScalar<string>(subtract_freeze_and_kcsl, param);
        }

        /// <summary>
        /// 解冻
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="warehouseId"></param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string Unfreeze(string productId, string pc, string ph, string warehouseId, int sl, string organizeId, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@proId", productId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@sl", sl),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph)
            };
            return FirstOrDefaultScalar<string>(just_unfreeze, param);
        }

        /// <summary>
        /// 冻结库存
        /// </summary>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string FrozenKc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@proId", productId),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@sl", sl)
            };
            return FindListScalar<string>(just_frozen_kcsl, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 外部入库 扣库存(作废)
        /// </summary>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string WbrkKkc(int sl, string pc, string ph, string productId, string organizeId, string warehouseId, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@proId", productId),
                new SugarParameter("@zt", ((int) Enumzt.Enable).ToString()),
                new SugarParameter("@sl", sl),
                new SugarParameter("@userCode", userCode)
            };
            return FindListScalar<string>(cancel_subtract_kcsl, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 撤销入库（直接出库作废） 若要使用该方法请套上事务，方法内不做回滚
        /// </summary>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <param name="rkbm"></param>
        /// <param name="ckbm"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string RevokeInstorage(int sl, string pc, string ph, string productId, string organizeId, string rkbm, string ckbm, string userCode)
        {
            //入库部门扣库存
            var substractResult = JustSubtractKcsl(new UpdateKcslDTO { organizeId = organizeId, pc = pc, ph = ph, productId = productId, sl = sl, userCode = userCode, warehouseId = rkbm });
            if (!string.IsNullOrWhiteSpace(substractResult)) return substractResult;

            //出库部门还库存
            if (UpdateKcsl(productId, pc, ph, ckbm, sl, organizeId, userCode) <= 0)
            {
                return "Trả lại tồn kho thất bại";
            }
            return "";
        }

        /// <summary>
        /// 获取库存信息 结转用
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
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@zt", ((int) Enumzt.Enable).ToString()),
            };
            return FindList<VKcxxEntity>(sql, param);
        }

        //        /// <summary>
        //        /// 撤销入库（直接出库作废）
        //        /// </summary>
        //        /// <param name="sl">最小单位数量</param>
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
        //DECLARE @rkkcsl INT;--入库库存
        //DECLARE @rkdjsl INT;--入库冻结库存
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
        //			SELECT '出库部门入库存失败';
        //		END
        //	END 
        //	ELSE
        //	BEGIN
        //		SELECT '入库部门出库存失败';
        //	END
        //END
        //ELSE 
        //BEGIN
        //	SELECT '库存不足';
        //END 
        //";
        //            var param = new SugarParameter[]
        //            {
        //                new SugarParameter("@pc", pc),
        //                new SugarParameter("@ph", ph),
        //                new SugarParameter("@OrganizeId", organizeId),
        //                new SugarParameter("@rkbm", rkbm),
        //                new SugarParameter("@ckbm", ckbm),
        //                new SugarParameter("@proId", productId),
        //                new SugarParameter("@zt", ((int) Enumzt.Enable).ToString()),
        //                new SugarParameter("@sl", sl),
        //                new SugarParameter("@userCode",userCode)
        //            };
        //            return FindListScalar<string>(sql, param.ToArray()).FirstOrDefault();
        //        }
        // SQL scripts embedded from the original Infrastructure/TSql constants
        // (that folder is not part of the Core copy); user-visible messages translated.
        private const string just_subtract_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Số lượng tiêu hao tồn kho phải lớn hơn 0';
		ROLLBACK TRANSACTION
		RETURN;
	END 

	DECLARE @totalKcsl INT;
	SELECT @totalKcsl=ISNULL(SUM(kcsl),0) FROM dbo.kf_kcxx 
	WHERE productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' 
	IF @totalKcsl<@sl
	BEGIN
		SELECT 'Không đủ tồn kho, xuất kho thất bại';
		ROLLBACK TRANSACTION;
		RETURN
	END 

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, kcsl INTO #allkcxx FROM dbo.kf_kcxx 
	WHERE productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' 

	DECLARE @kcsl INT; --当前可用库存
	DECLARE @kcId VARCHAR(50);
	DECLARE @sykc INT;
	SET @sykc=@sl;

	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sykc>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@kcsl=kcsl FROM #allkcxx
		IF @kcsl>=@sykc
		BEGIN
			UPDATE dbo.kf_kcxx SET kcsl=kcsl-@sykc, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			AND productId=@proId 
			AND warehouseId=@warehouseId 
			AND OrganizeId=@OrganizeId 
			AND pc=@pc 
			AND ph=@ph 
			AND zt='1'  
			SET @sykc=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.kf_kcxx SET kcsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			AND productId=@proId 
			AND warehouseId=@warehouseId 
			AND OrganizeId=@OrganizeId 
			AND pc=@pc 
			AND ph=@ph 
			AND zt='1'  
			SET @sykc=@sykc-@kcsl;
		END 
		DELETE FROM #allkcxx WHERE Id=@kcId;
	END 
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION
	RETURN
END CATCH
";
        private const string just_frozen_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @dqkykc INT; --当前可用库存
	SELECT @dqkykc=SUM(kcsl-djsl) FROM dbo.kf_kcxx WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId
	IF @sl > @dqkykc
	BEGIN
		SELECT 'Không đủ tồn kho, đăng ký thất bại';
		ROLLBACK TRANSACTION;
		RETURN
	END
	IF @sl <=0
	BEGIN
		SELECT 'Số lượng cần đóng băng tồn kho phải lớn hơn 0';
		ROLLBACK TRANSACTION;
		RETURN
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id kcId,(kcsl-djsl) kykc INTO #allkcxx FROM dbo.kf_kcxx WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId
	DECLARE @kcId VARCHAR(50),@kykc INT, @sysl INT;
	SET @sysl=@sl
	WHILE EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0
	BEGIN
		SELECT TOP 1 @kcId=kcId, @kykc=kykc FROM #allkcxx
		IF @kykc>=@sysl
		BEGIN
			UPDATE dbo.kf_kcxx 
			SET djsl=djsl+@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
			WHERE Id=@kcId AND productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId
			SET @sysl=0;
		END 
		ELSE
        BEGIN
			UPDATE dbo.kf_kcxx
			SET djsl=djsl+@kykc, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId
			SET @sysl=@sysl-@kykc
		END 

		DELETE FROM #allkcxx WHERE kcId=@kcId;
	END 
	
	COMMIT TRANSACTION;
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
	RETURN ;
END CATCH
";
        private const string subtract_freeze_and_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @kcsl INT; --当前库存
	DECLARE @djsl INT; --当前冻结库存
	SELECT @kcsl=ISNULL(SUM(kcsl),0), @djsl=ISNULL(SUM(djsl),0)
	FROM dbo.kf_kcxx 
	WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId AND djsl>0 AND kcsl>0
	IF @kcsl<=0 OR @djsl<=0
	BEGIN
		SELECT 'Trong tồn kho chưa có vật tư đã đóng băng';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	IF @kcsl<@sl OR @kcsl<@djsl
	BEGIN
		SELECT 'Số lượng đã đóng băng nhỏ hơn số lượng xuất kho, cấm xuất kho';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END

	SELECT Id kcId, kcsl, djsl INTO #allkcxx 
	FROM dbo.kf_kcxx 
	WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND warehouseId=@warehouseId AND djsl>0 AND kcsl>0
	
	DECLARE @sysl INT,@curkcId VARCHAR(50), @curkcsl INT, @curdjsl INT, @tmpsl int;
	SET @tmpsl=0;
	SET @sysl=@sl;
	WHILE EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0
	BEGIN
		SELECT TOP 1 @curkcId=kcId, @curkcsl=kcsl, @curdjsl=djsl FROM #allkcxx
		IF @sysl<=@curdjsl AND @sysl<=@curkcsl
		BEGIN
			UPDATE dbo.kf_kcxx SET kcsl=kcsl-@sysl, djsl=djsl-@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@curkcId AND productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1'
			SET @sysl=0
		END 
		ELSE 
		BEGIN
			--取冻结数和库存数中最小的一个赋值给@tmpsl
			IF @curdjsl<=@curkcsl
			BEGIN 
				SET @tmpsl=@curdjsl;
			END 
			ELSE
            BEGIN
				SET @tmpsl=@curkcsl;
			END 
			UPDATE dbo.kf_kcxx SET kcsl=kcsl-@tmpsl, djsl=djsl-@tmpsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@curkcId AND productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1'
			SET @sysl=@sysl-@tmpsl;
		END 

		DELETE FROM #allkcxx WHERE kcId=@curkcId;
	END
    	
	COMMIT TRANSACTION;
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
	RETURN ;
END CATCH
";
        private const string just_unfreeze = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Số lượng cần giải đóng băng phải lớn hơn 0'
		ROLLBACK TRANSACTION
		RETURN;
	END 
	DECLARE @totalDjsl INT;
	SELECT @totalDjsl=ISNULL(SUM(djsl),0) 
	FROM dbo.kf_kcxx 
	WHERE productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' AND djsl>0
	
	IF @sl>@totalDjsl
	BEGIN
		SELECT 'Tổng số đã đóng băng nhỏ hơn số cần giải đóng băng, giải đóng băng thất bại'
		ROLLBACK TRANSACTION
		RETURN;
	END 
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, djsl INTO #allkcxx FROM dbo.kf_kcxx 
	WHERE productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' AND djsl>0
		
	DECLARE @djsl INT; --当前冻结数量
	DECLARE @kcId VARCHAR(50); --当前库存ID
	DECLARE @sysl INT; --剩余数量
	SET @sysl=@sl; 
	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@djsl=djsl FROM #allkcxx
		IF @djsl>=@sysl
		BEGIN
			UPDATE dbo.kf_kcxx SET djsl=djsl-@sysl, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			SET @sysl=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.kf_kcxx SET djsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			SET @sysl=@sysl-@djsl;
		END 
		DELETE FROM #allkcxx WHERE Id=@kcId;
	END 
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION
	RETURN
END CATCH
";
        private const string cancel_subtract_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Số lượng tồn kho cần hủy phải lớn hơn 0';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	DECLARE @totalDjsl INT;
	DECLARE @totalKcsl INT;
	SELECT @totalKcsl=ISNULL(SUM(kcsl),0), @totalDjsl=ISNULL(SUM(djsl),0) FROM dbo.kf_kcxx 
	WHERE pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND warehouseId=@warehouseId AND productId=@proId AND zt=@zt
	IF @totalDjsl>0  
	BEGIN
		-- 有冻结说明正在被使用，作废可能导致后期扣库存库存不足
		SELECT 'Lô này đang có tồn kho đóng băng, không thể hủy';
		ROLLBACK TRANSACTION;
		RETURN;
	END
	IF @totalKcsl<@sl
	BEGIN
		SELECT 'Không đủ tồn kho, không thể hủy';
		ROLLBACK TRANSACTION;
		RETURN;
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, kcsl INTO #allkcxx FROM dbo.kf_kcxx 
	WHERE productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt

	DECLARE @kcsl INT; --当前可用库存
	DECLARE @kcId VARCHAR(50); --当前库存ID
	DECLARE @sykc INT; --剩余库存
	SET @sykc=@sl;

	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sykc>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@kcsl=kcsl FROM #allkcxx
		IF @kcsl>=@sykc
		BEGIN
			UPDATE dbo.kf_kcxx SET kcsl=kcsl-@sykc, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
			SET @sykc=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.kf_kcxx SET kcsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND warehouseId=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
			SET @sykc=@sykc-@kcsl;
		END 
		DELETE FROM #allkcxx WHERE Id=@kcId;
	END 
	COMMIT TRANSACTION
	SELECT '';
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE();
	ROLLBACK TRANSACTION
	RETURN
END CATCH
";
    }
}
