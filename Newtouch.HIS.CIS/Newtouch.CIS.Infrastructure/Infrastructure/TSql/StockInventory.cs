namespace Newtouch.Infrastructure.TSql
{
    public class StockInventory
    {
        /// <summary>
        /// tạoKiểm kêthông tin
        /// </summary>
        public const string generate_inventory_info = @"
BEGIN TRANSACTION;
BEGIN TRY
	DECLARE @pdId BIGINT;
	DECLARE @pdkssj DATETIME;
	SET @pdkssj=(SELECT TOP 1 pdxx.Jssj jssj 
				FROM dbo.dept_pdxx pdxx 
				WHERE zt='1' AND OrganizeId=@OrganizeId 
				ORDER BY pdxx.Jssj DESC)
	IF @pdkssj IS NULL OR @pdkssj=''
	BEGIN
		SET @pdkssj=GETDATE();
	END 

	INSERT INTO dbo.dept_pdxx( OrganizeId ,warehouseId ,kssj ,jssj ,Pdfs ,zt ,px ,creatorCode ,createTime ,lastModifyTime ,lastModifierCode)
	VALUES  (	@OrganizeId , -- OrganizeId - varchar(50)
			    @warehouseId , -- warehouseId - varchar(100)
				@pdkssj , -- Kssj - datetime
				NULL , -- Jssj - datetime
				0 , -- Pdfs - smallint
				'1' , -- zt - char(1)
				NULL , -- px - int
				@CreatorCode , -- CreatorCode - varchar(50)
				GETDATE() , -- CreateTime - datetime
				NULL , -- LastModifyTime - datetime
				NULL  -- LastModifierCode - varchar(50)
		    )
	SET @pdId= @@IDENTITY  
	IF(@pdId<=0)
	BEGIN
		SELECT 'tạoKiểm kê主thông tinthất bại'  AS Jgxx;
		ROLLBACK TRANSACTION;
		RETURN;
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpSourcepdmx') and type='U')
	BEGIN
		DROP TABLE #tmpSourcepdmx;
	END
		
	SELECT ROW_NUMBER() OVER (ORDER BY kc.productId) AS Id, kc.Id KcId, kc .productId
	, (CASE WHEN kc.Id IS NULL THEN 'CSHPDSC' ELSE kc.ph END) ph
	, (CASE WHEN kc.Id IS NULL THEN CONVERT(VARCHAR(8),GETDATE(),112) + REPLACE( CONVERT(VARCHAR(12),GETDATE(),14),':','')  ELSE kc.pc END) pc
	, (CASE WHEN kc.Id IS NULL THEN DATEADD(YEAR,1,GETDATE()) ELSE kc.yxq END) yxq
	, ISNULL(SUM(kc.kcsl),0) llsl, ISNULL(SUM(kc.kcsl),0) sjsl, ISNULL(wzdw.zhyz,1) zhyz , ISNULL(wz.lsj*ISNULL(wzdw.zhyz,1),0) lsj
	INTO #tmpSourcepdmx
	FROM dbo.Dept_kcxx(NOLOCK) kc 
	LEFT JOIN NewtouchHIS_herp.dbo.rel_productUnit(NOLOCK) wzdw ON wzdw.productId=kc .productId And wzdw.OrganizeId=kc.OrganizeId
	INNER JOIN NewtouchHIS_herp.dbo.wz_product(NOLOCK) wz ON wz.Id=kc.productId AND wz.OrganizeId=kc .OrganizeId
	WHERE kc.ks=@warehouseId 
	AND kc .OrganizeId=@OrganizeId 
    and wzdw.zhyz =1
	GROUP BY kc.Id, kc.productId, kc.ph, kc.pc, kc.yxq, wzdw.zhyz, wz.lsj

	IF EXISTS(SELECT 1 FROM #tmpSourcepdmx)
	BEGIN
		IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpUserfulpdmx') and type='U')
		BEGIN
			DROP TABLE #tmpUserfulpdmx;
		END
		SELECT * INTO #tmpUserfulpdmx FROM #tmpSourcepdmx --WHERE KcId IS NOT NULL AND KcId<>'' 		
			
		DECLARE @Ids TABLE(	Id BIGINT);
		INSERT INTO @Ids SELECT TOP 200 Id FROM #tmpUserfulpdmx;
		CREATE INDEX tmpUserfulpdmx_index ON #tmpUserfulpdmx(Id);
			
		WHILE EXISTS(SELECT 1 FROM @Ids)
		BEGIN
				
			INSERT INTO dbo.dept_pdxxmx( pdId ,productId ,ph ,pc ,yxq ,llsl ,sjsl ,zhyz ,lsj ,px ,zt ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
								SELECT @pdId, productId,ph, pc, yxq, llsl, sjsl, zhyz, lsj, NULL, '1', @CreatorCode, GETDATE(), NULL, NULL 
								FROM #tmpUserfulpdmx WHERE Id IN (SELECT Id FROM @Ids);

			DELETE FROM #tmpUserfulpdmx WHERE Id IN (SELECT Id FROM @Ids);
			DELETE FROM @Ids
			INSERT INTO @Ids SELECT TOP 200 Id FROM #tmpUserfulpdmx;
		END


		IF EXISTS(SELECT 1 FROM #tmpSourcepdmx WHERE KcId IS NULL OR KcId='')
		BEGIN
			IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpUseless') and type='U')
			BEGIN
				DROP TABLE #tmpUseless;
			END

			SELECT * 
			INTO #tmpUseless 
			FROM #tmpSourcepdmx 
			WHERE KcId IS NULL OR KcId='';
			
			DECLARE @rCount BIGINT;
			DECLARE @currentId BIGINT;
			DECLARE @newjj NUMERIC(11,4);
			DECLARE @productId VARCHAR(50);
			DECLARE @zhyz NUMERIC,@kcxh varchar(50);
			DECLARE @ph CHAR(30);
			DECLARE @pc VARCHAR(30);
			DECLARE @yxq DATETIME;
			DECLARE @llsl INT;
			DECLARE @sjsl INT;

			SELECT @rCount=COUNT(0) FROM #tmpUseless;
			WHILE @rCount>0
			BEGIN
				SELECT TOP 1 @currentId=Id, @productId=productId, @zhyz=Zhyz, @newjj=lsj, @pc=pc, @ph=ph, @yxq=yxq ,@llsl=llsl, @sjsl=sjsl
				FROM #tmpUseless;
				
				INSERT INTO dbo.Dept_kcxx( Id ,OrganizeId ,ks,productId ,ph ,pc ,yxq ,kcsl ,djsl ,crkmxId ,jj ,zhyz ,zt ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
				VALUES  (	NEWID() , -- Id - varchar(50)
							@OrganizeId , -- OrganizeId - varchar(50)
							@warehouseId , -- warehouseId - varchar(50)
							@productId , -- productId - varchar(50)
							@ph , -- ph - varchar(50)
							@pc , -- pc - varchar(50)
							@yxq , -- yxq - datetime
							0 , -- kcsl - int
							0 , -- djsl - int
							-1 , -- crkmxId - bigint
							@newjj , -- jj - numeric
							@zhyz , -- zhyz - int
							'1' , -- zt - varchar(1)
							@CreatorCode , -- CreatorCode - varchar(50)
							GETDATE() , -- CreateTime - datetime
							NULL , -- LastModifyTime - datetime
							NULL  -- LastModifierCode - varchar(50)
				)

				DELETE FROM #tmpUseless WHERE Id=@currentId;
				SELECT @rCount=COUNT(0) FROM #tmpUseless;
			END
		END
	END
	COMMIT TRANSACTION;
	SELECT '' AS Jgxx;
END TRY
BEGIN CATCH
	SELECT 'tạoKiểm kêthông tinthất bại！' AS Jgxx;
	PRINT ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
END CATCH
";

        /// <summary>
        /// LưuKiểm kêchi tiết
        /// </summary>
        public const string save_inventory_info = @"
BEGIN TRANSACTION
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpData') and type='U')
BEGIN
	DROP TABLE #tmpData;
END

DECLARE @pdmxId BIGINT;
DECLARE @sjsl INT;--Kiểm kêchi tiếtbảngthực tếSố lượng

SELECT xm.productId, wz.name wzmc, xm.Id pdmxId, xm.pc,xm.ph,xm.sjsl
INTO #tmpData
FROM dbo.dept_pdxxmx xm
INNER JOIN dbo.dept_pdxx pd ON pd.Id=xm.pdId AND pd.zt='1' 
INNER JOIN NewtouchHIS_herp.dbo.wz_product(NOLOCK) wz ON wz.Id=xm.productId AND wz.OrganizeId=pd.OrganizeId AND wz.zt='1'
WHERE xm.productId=@proId
AND pd.Id=@pdId
AND pd.OrganizeId=@OrganizeId
AND pd.warehouseId=@warehouseId
AND xm.zt='1'

IF @sl>0 --thực tếTồn khocónhiều，thìtrực tiếpThêmđếngần nhấtNhập khocủamộtLôtrên
BEGIN
	SELECT TOP 1 @pdmxId=pdmxId,@sjsl=sjsl FROM #tmpData ORDER BY pc DESC
	UPDATE dbo.dept_pdxxmx SET sjsl=sjsl+@sl WHERE Id=@pdmxId AND zt='1'
	IF @@ROWCOUNT<=0
	BEGIN
		SELECT 'Lưu chi tiết kiểm kê thất bại'
		ROLLBACK TRANSACTION
		RETURN;
	END 
	SELECT '';
	COMMIT TRANSACTION;
	RETURN;
END 

--thực tếTồn khosolýluậnít，thìưutrướctừgần nhấtNhập khocủaLôgiảm，khôngđủthìtheoLô倒thứ tự逐cáikhấu
DECLARE @sysl INT;--Số lượng còn lại，mỗi lần抵hủysaucủaSố lượng
SET @sysl=@sl;
WHILE EXISTS(SELECT 1 FROM #tmpData WHERE sjsl>0)
BEGIN
	IF @sysl>=0 --tất cảSố lượng còn lạiđềuphầnphốixong，thìThoátLặp lại，Kết thúcLưu
	BEGIN
		BREAK;
	END 
	
	--còncóTồn kho còn lạichưaphầnphối，kếtiếp寻tìmkýhợpcầncầucủaLôthựchiệnkhấuTồn kho
	SELECT TOP 1 @pdmxId=pdmxId,@sjsl=sjsl FROM #tmpData WHERE sjsl>0 ORDER BY pc DESC
	SET @sysl=@sysl+@sjsl;
	IF (@sysl<=0)
	BEGIN
		UPDATE dbo.dept_pdxxmx SET sjsl=0 WHERE Id=@pdmxId AND zt='1'
		IF @@ROWCOUNT<=0
		BEGIN
			SELECT 'Lưu chi tiết kiểm kê thất bại'
			ROLLBACK TRANSACTION
			RETURN;
		END 
	END 
	ELSE IF(@sysl>0)
	BEGIN
		UPDATE dbo.dept_pdxxmx SET sjsl=@sysl WHERE Id=@pdmxId AND zt='1'
		IF @@ROWCOUNT<=0
		BEGIN
			SELECT 'Lưu chi tiết kiểm kê thất bại'
			ROLLBACK TRANSACTION
			RETURN;
		END
		SET @sysl=0; 
	END

	DELETE FROM #tmpData WHERE pdmxId=@pdmxId
END 
IF @sysl<>0
BEGIN
	SELECT 'Hệ thốngcủaTồn khosốkhôngđủlấy抵hủynhậpcủathực tếsố，vui lòngKiểm traKiểm kêquatrìnhtronglàKhôngphátsinhXuất khohoặcBáo hỏngđợithao tác！';
END
ELSE
BEGIN
	SELECT '';
END
COMMIT TRANSACTION;
RETURN
";

        /// <summary>
        /// Kết thúcKiểm kê
        /// </summary>
        public const string end_Inventory = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#pdInfo') and type='U')
BEGIN
	DROP TABLE #pdInfo;
END

SELECT ROW_NUMBER() OVER (ORDER BY M.Id) AS Id, p.warehouseId, M.productId, ISNULL(M.ph,'') ph, Isnull(M.pc,'') pc
,Isnull(M.yxq,'1899-01-01') yxq, (M.sjsl - M.llsl) sysl, M.lsj, M.zhyz, pu.unitId
INTO #pdInfo
From dbo.dept_pdxx P WITH(NOLOCK)
INNER JOIN dbo.dept_pdxxmx M WITH(NOLOCK) ON P.Id=M.pdId AND M.sjsl-M.llsl<>0
INNER JOIN NewtouchHIS_herp.dbo.wz_product wz(NOLOCK)  ON wz.id=M.productId AND wz.OrganizeId=p.OrganizeId
INNER JOIN NewtouchHIS_herp.dbo.rel_productUnit pu ON pu.productId = M.productId
Where P.Id = @pdId
AND P.OrganizeId=@OrganizeId

SET XACT_ABORT ON 
BEGIN TRANSACTION;

IF EXISTS(SELECT 1 FROM #pdInfo)
BEGIN
	CREATE INDEX pdInfo_index_id ON #pdInfo(Id);
	DECLARE @id BIGINT;
	DECLARE @Kcsl INT;
	DECLARE @CrkmxId BIGINT;
	DECLARE @warehouseId VARCHAR(50); 
	DECLARE @productId VARCHAR(50);
	DECLARE @productName VARCHAR(100);--vật tưtên
	DECLARE @Ph VARCHAR(30);
	DECLARE @pc VARCHAR(30);
	DECLARE @Yxq DATETIME;
	DECLARE @Sysl INT;
	DECLARE @djh VARCHAR(50);--truyềnratham số
	DECLARE @Sykc INT;--Tồn kho còn lại
	DECLARE @Lsj MONEY;--Giá bán lẻ
	DECLARE @Zhyz INT;
	DECLARE @Jj MONEY;
	DECLARE @unitId VARCHAR(50);

	SELECT TOP 1 @id=Id, @warehouseId=warehouseId, @productId=productId, @Ph=ph, @pc=pc, @Yxq=yxq, @Sysl=sysl,@Lsj=lsj, @Zhyz=zhyz, @unitId=unitId FROM #pdInfo;
	WHILE EXISTS(SELECT 1 FROM #pdInfo)
	BEGIN
		SELECT @Kcsl = Sum(Isnull(kcsl,0)) From dbo.Dept_kcxx WITH(NOLOCK) WHERE ks=@warehouseId AND productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId
		SELECT @CrkmxId = CrkmxId From dbo.Dept_kcxx WITH(NOLOCK)	WHERE ks=@warehouseId And productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01')  AND OrganizeId=@OrganizeId
		SELECT @Jj=(CASE WHEN sum(kcsl)=0 THEN SUM(jj)/COUNT(1) ELSE ISNULL(sum(jj*kcsl)/sum(Kcsl),0) END) From dbo.Dept_kcxx WITH(NOLOCK) WHERE ks=@warehouseId And productId=@productId AND OrganizeId=@OrganizeId
		SELECT @productName=name FROM NewtouchHIS_herp.dbo.wz_product(NOLOCK) wz WHERE wz.Id=@productId AND OrganizeId=@OrganizeId;

		If Isnull(@Kcsl,0) + @Sysl < 0
		BEGIN
			ROLLBACK TRANSACTION
			SELECT @productName + 'Tồn kho không đủ！' 
			RETURN;
		END		
			
		--tạoBáo hỏng báo thừaphiếusố
		SET @djh='BSBYD'+REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(23), GETDATE(),25), '-',''),':',''),' ',''),'.','');
		UPDATE Dept_kcxx Set kcsl = kcsl + @Sysl, LastModifierCode=@CreatorCode, LastModifyTime=GETDATE() Where ks=@warehouseId AND productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId
		IF @@ERROR <>0
		BEGIN
			ROLLBACK TRANSACTION
			SELECT 'sửaTồn khothất bại！';
			RETURN;
		END

		SELECT @Sykc = Isnull(@Kcsl,0) + @Sysl
		IF @Sysl > 0
		BEGIN
			INSERT INTO dbo.dept_syxx( OrganizeId ,ks ,productId ,Ph ,pc ,Yxq ,Bgsj ,Sysl ,Lsj ,Zhyz ,Syyy ,Zrr ,Djh ,Sykc ,Jj ,remark ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode, UnitId)
									VALUES  (
									@OrganizeId , -- OrganizeId - varchar(50)
									@warehouseId , -- warehouseId - varchar(50)
									@productId , -- productId - varchar(50)
									@Ph , -- Ph - varchar(30)
									@pc , -- pc - varchar(30)
									@Yxq , -- Yxq - datetime
									GETDATE() , -- Bgsj - datetime
									@Sysl , -- Sysl - int
									@Lsj , -- Lsj - decimal
									@Zhyz , -- Zhyz - int
									'AF753115-B94B-4BAD-AA45-5EE786D0F2DF' , -- Syyy - char(10)
									@CreatorCode , -- Zrr - varchar(50)
									@djh , -- Djh - varchar(50)
									@Sykc , -- Sykc - int
									ISNULL(@Jj,0) , -- Jj - decimal
									'' , -- remark - varchar(1000)
									'1' , -- zt - char(1)
									0 , -- px - int
									@CreatorCode , -- CreatorCode - varchar(50)
									GETDATE() , -- CreateTime - datetime
									NULL , -- LastModifyTime - datetime
									NULL , -- LastModifierCode - varchar(50)
									@unitId
									)
			IF @@ERROR <>0
			BEGIN
				ROLLBACK TRANSACTION
				SELECT 'Ghi thông tin lãi lỗ vào kho thất bại！'
				RETURN;
			END
		END
		ELSE
		BEGIN
			INSERT INTO dbo.dept_syxx( OrganizeId ,ks ,productId ,Ph ,pc ,Yxq ,Bgsj ,Sysl ,Lsj ,Zhyz ,Syyy ,Zrr ,Djh ,Sykc ,Jj ,remark ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode, UnitId)
									VALUES  (
									@OrganizeId , -- OrganizeId - varchar(50)
									@warehouseId , -- warehouseId - varchar(50)
									@productId , -- productId - varchar(50)
									@Ph , -- Ph - varchar(30)
									@pc , -- pc - varchar(30)
									@Yxq , -- Yxq - datetime
									GETDATE() , -- Bgsj - datetime
									@Sysl , -- Sysl - int
									@Lsj , -- Lsj - decimal
									@Zhyz , -- Zhyz - int
									'1B30DBC0-EF98-47D2-9B18-D9A4EFBA9776' , -- Syyy - char(10)
									@CreatorCode , -- Zrr - char(16)
									@djh , -- Djh - char(30)
									@Sykc , -- Sykc - int
									ISNULL(@Jj,0) , -- Jj - decimal
									'' , -- remark - varchar(1000)
									'1' , -- zt - char(1)
									0 , -- px - int
									@CreatorCode , -- CreatorCode - varchar(50)
									GETDATE() , -- CreateTime - datetime
									NULL , -- LastModifyTime - datetime
									NULL , -- LastModifierCode - varchar(50)
									@unitId
									)
			IF @@ERROR <>0
			BEGIN
				ROLLBACK TRANSACTION
				SELECT 'Ghi thông tin lãi lỗ vào kho thất bại！' ;
				RETURN;
			END
		END
		DELETE #pdInfo WHERE Id=@id;
		SELECT TOP 1 @id=Id, @warehouseId=warehouseId, @productId=productId, @Ph=ph, @pc=pc, @Yxq=yxq, @Sysl=sysl,@Lsj=lsj, @Zhyz=zhyz, @unitId=unitId FROM #pdInfo;
	END
END
	
UPDATE dept_pdxx SET Jssj = GETDATE(), LastModifierCode=@CreatorCode, LastModifyTime=GETDATE()
WHERE Id = @pdId
IF @@ERROR <>0
BEGIN
	ROLLBACK TRANSACTION
	SELECT 'sửaKiểm kêthông tinkhothất bại！' ;
	RETURN;
END
	
COMMIT TRANSACTION
SELECT '';
";
    }
}
