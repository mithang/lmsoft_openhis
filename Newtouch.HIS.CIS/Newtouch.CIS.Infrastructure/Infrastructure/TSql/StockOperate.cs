namespace Newtouch.Infrastructure.TSql
{
    /// <summary>
    /// Tồn khoThao tác
    /// </summary>
    public class StockOperate
    {
        /// <summary>
        /// giảmTồn kho khôngcầnRã đông
        /// </summary>
        public const string just_subtract_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Tồn kho tiêu hao phải lớn hơn 0';
		ROLLBACK TRANSACTION
		RETURN;
	END 

	DECLARE @totalKcsl INT;
	SELECT @totalKcsl=ISNULL(SUM(kcsl),0) FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' 
	IF @totalKcsl<@sl
	BEGIN
		SELECT 'Tồn kho không đủ，Xuất khothất bại';
		ROLLBACK TRANSACTION;
		RETURN
	END 

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, kcsl INTO #allkcxx FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' 

	DECLARE @kcsl INT; --Tồn kho khả dụng hiện tại
	DECLARE @kcId VARCHAR(50);
	DECLARE @sykc INT;
	SET @sykc=@sl;

	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sykc>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@kcsl=kcsl FROM #allkcxx
		IF @kcsl>=@sykc
		BEGIN
			UPDATE dbo.Dept_kcxx SET kcsl=kcsl-@sykc, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			AND productId=@proId 
			AND ks=@warehouseId 
			AND OrganizeId=@OrganizeId 
			AND pc=@pc 
			AND ph=@ph 
			AND zt='1'  
			SET @sykc=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.Dept_kcxx SET kcsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			AND productId=@proId 
			AND ks=@warehouseId 
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
        /// <summary>
        /// chỉđôngkếtTồn kho
        /// </summary>
        public const string just_frozen_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @dqkykc INT; --Tồn kho khả dụng hiện tại
	SELECT @dqkykc=SUM(kcsl-djsl) FROM dbo.Dept_kcxx WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId
	IF @sl > @dqkykc
	BEGIN
		SELECT 'Tồn kho không đủ，Gửi đơn thất bại';
		ROLLBACK TRANSACTION;
		RETURN
	END
	IF @sl <=0
	BEGIN
		SELECT 'Tồn kho cần đóng băng phải lớn hơn 0';
		ROLLBACK TRANSACTION;
		RETURN
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id kcId,(kcsl-djsl) kykc INTO #allkcxx FROM dbo.Dept_kcxx WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId
	DECLARE @kcId VARCHAR(50),@kykc INT, @sysl INT;
	SET @sysl=@sl
	WHILE EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0
	BEGIN
		SELECT TOP 1 @kcId=kcId, @kykc=kykc FROM #allkcxx
		IF @kykc>=@sysl
		BEGIN
			UPDATE dbo.Dept_kcxx 
			SET djsl=djsl+@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
			WHERE Id=@kcId AND productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId
			SET @sysl=0;
		END 
		ELSE
        BEGIN
			UPDATE dbo.Dept_kcxx
			SET djsl=djsl+@kykc, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId
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

        /// <summary>
        /// giảmđôngkếtvàkhấuTồn kho  Bình thườngdùng làmPhê duyệtthông qua
        /// </summary>
        public const string subtract_freeze_and_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @kcsl INT; --Tồn kho hiện tại
	DECLARE @djsl INT; --hiện tạiđôngkếtTồn kho
	SELECT @kcsl=ISNULL(SUM(kcsl),0), @djsl=ISNULL(SUM(djsl),0)
	FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId AND djsl>0 AND kcsl>0
	IF @kcsl<=0 OR @djsl<=0
	BEGIN
		SELECT 'Trong kho chưa có vật tư đã đóng băng';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	IF @kcsl<@sl OR @kcsl<@djsl
	BEGIN
		SELECT 'Số lượng đã đóng băng nhỏ hơn số lượng xuất kho，禁止出库';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END

	SELECT Id kcId, kcsl, djsl INTO #allkcxx 
	FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND zt='1' AND ks=@warehouseId AND djsl>0 AND kcsl>0
	
	DECLARE @sysl INT,@curkcId VARCHAR(50), @curkcsl INT, @curdjsl INT, @tmpsl int;
	SET @tmpsl=0;
	SET @sysl=@sl;
	WHILE EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0
	BEGIN
		SELECT TOP 1 @curkcId=kcId, @curkcsl=kcsl, @curdjsl=djsl FROM #allkcxx
		IF @sysl<=@curdjsl AND @sysl<=@curkcsl
		BEGIN
			UPDATE dbo.Dept_kcxx SET kcsl=kcsl-@sysl, djsl=djsl-@sysl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@curkcId AND productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1'
			SET @sysl=0
		END 
		ELSE 
		BEGIN
			--lấyđôngkếtsốvàTồn khosốtrongnhấtnhỏcủamộtGán giá trịcho@tmpsl
			IF @curdjsl<=@curkcsl
			BEGIN 
				SET @tmpsl=@curdjsl;
			END 
			ELSE
            BEGIN
				SET @tmpsl=@curkcsl;
			END 
			UPDATE dbo.Dept_kcxx SET kcsl=kcsl-@tmpsl, djsl=djsl-@tmpsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
			WHERE Id=@curkcId AND productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1'
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

        /// <summary>
        /// chỉRã đông
        /// </summary>
        public const string just_unfreeze = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Số lượng cần rã đông phải lớn hơn 0'
		ROLLBACK TRANSACTION
		RETURN;
	END 
	DECLARE @totalDjsl INT;
	SELECT @totalDjsl=ISNULL(SUM(djsl),0) 
	FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' AND djsl>0
	
	IF @sl>@totalDjsl
	BEGIN
		SELECT '已冻结Tổng lượng小于cầnRã đông数，Rã đôngthất bại'
		ROLLBACK TRANSACTION
		RETURN;
	END 
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, djsl INTO #allkcxx FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt='1' AND djsl>0
		
	DECLARE @djsl INT; --hiện tạiđôngkếtSố lượng
	DECLARE @kcId VARCHAR(50); --Tồn kho hiện tạiID
	DECLARE @sysl INT; --Số lượng còn lại
	SET @sysl=@sl; 
	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sysl>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@djsl=djsl FROM #allkcxx
		IF @djsl>=@sysl
		BEGIN
			UPDATE dbo.Dept_kcxx SET djsl=djsl-@sysl, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId 
			SET @sysl=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.Dept_kcxx SET djsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
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

        /// <summary>
        /// Hủy bỏ  giảmTồn kho
        /// </summary>
        public const string cancel_subtract_kcsl = @"
BEGIN TRANSACTION
BEGIN TRY 
	IF @sl<=0
	BEGIN
		SELECT 'Số lượng tồn kho cần hủy bỏ phải lớn hơn 0';
		ROLLBACK TRANSACTION;
		RETURN;
	END 
	DECLARE @totalDjsl INT;
	DECLARE @totalKcsl INT;
	SELECT @totalKcsl=ISNULL(SUM(kcsl),0), @totalDjsl=ISNULL(SUM(djsl),0) FROM dbo.Dept_kcxx 
	WHERE pc=@pc AND ph=@ph AND OrganizeId=@OrganizeId AND ks=@warehouseId AND productId=@proId AND zt=@zt
	IF @totalDjsl>0  
	BEGIN
		-- cóđôngkếtMô tảđangởbịsử dụng，hủy bỏcó thểhướnggâysaukỳkhấuTồn khoTồn kho không đủ
		SELECT '该Lô存在冻结Tồn kho，không thểhủy bỏ';
		ROLLBACK TRANSACTION;
		RETURN;
	END
	IF @totalKcsl<@sl
	BEGIN
		SELECT 'Tồn kho không đủ，không thểhủy bỏ';
		ROLLBACK TRANSACTION;
		RETURN;
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#allkcxx') and type='U')
	BEGIN
		DROP TABLE #allkcxx;
	END
	SELECT Id, kcsl INTO #allkcxx FROM dbo.Dept_kcxx 
	WHERE productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt

	DECLARE @kcsl INT; --Tồn kho khả dụng hiện tại
	DECLARE @kcId VARCHAR(50); --Tồn kho hiện tạiID
	DECLARE @sykc INT; --Tồn kho còn lại
	SET @sykc=@sl;

	WHILE(EXISTS(SELECT 1 FROM #allkcxx) AND @sykc>0)
	BEGIN
		SELECT TOP 1 @kcId=Id,@kcsl=kcsl FROM #allkcxx
		IF @kcsl>=@sykc
		BEGIN
			UPDATE dbo.Dept_kcxx SET kcsl=kcsl-@sykc, LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
			SET @sykc=0;
		END 
		ELSE 
		BEGIN
			UPDATE dbo.Dept_kcxx SET kcsl=0,LastModifyTime=GETDATE(),LastModifierCode=@userCode
			WHERE Id=@kcId AND productId=@proId AND ks=@warehouseId AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND zt=@zt
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
