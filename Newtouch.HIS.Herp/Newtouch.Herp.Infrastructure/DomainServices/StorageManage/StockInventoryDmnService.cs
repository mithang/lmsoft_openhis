using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using SqlSugar;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure.EF;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.DomainServices.StorageManage
{
    /// <summary>
    /// Kiểm kê tồn kho
    /// </summary>
    public class StockInventoryDmnService : DmnServiceBase, IStockInventoryDmnService
    {
        private readonly IKfKcxxDmnService _kfKcxxDmnService;
        private readonly IKcSyxxRepo _kcSyxxRepo;

        public StockInventoryDmnService(IKfKcxxDmnService kfKcxxDmnService, IKcSyxxRepo kcSyxxRepo)
        {
            _kfKcxxDmnService = kfKcxxDmnService;
            _kcSyxxRepo = kcSyxxRepo;
        }

        /// <summary>
        /// Lấy thời gian kiểm kê
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VInventoryDateDropDownEntity> GetPdSj(string warehouseId, string organizeId)
        {
            const string sql = @"
SELECT Id pdId, pdsj FROM (
SELECT pdxx.Id,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
FROM kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.Id
WHERE pdxx.warehouseId =@warehouseId 
and pdxx.OrganizeId=@OrganizeId
GROUP BY pdxx.Id, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
) a
ORDER BY a.CreateTime DESC
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return FindList<VInventoryDateDropDownEntity>(sql, param);
        }

        /// <summary>
        /// Lấy thời gian kiểm kê chưa hoàn thành
        /// </summary>
        /// <returns></returns>
        public List<VInventoryDateDropDownEntity> GetHangUpPdDates(string warehouseId, string organizeId)
        {
            var strSql = new StringBuilder(@"
SELECT Id pdId, pdsj FROM (
SELECT pdxx.Id,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
FROM kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.Id
WHERE pdxx.warehouseId =@warehouseId 
AND pdxx.OrganizeId=@OrganizeId
AND pdxx.Jssj IS NULL 
GROUP BY pdxx.Id, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
) a
");
            var param = new SugarParameter[]
            {
                new SugarParameter("@warehouseId",warehouseId),
                new SugarParameter("@OrganizeId",organizeId)
            };
            return FindList<VInventoryDateDropDownEntity>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// Tạo thông tin kiểm kê
        /// </summary>
        public string GenerateInventoryInfo(string warehouseId, string organizeId)
        {
            var paraList = new List<SugarParameter>
            {
                new SugarParameter("@warehouseId",warehouseId),
                new SugarParameter("@OrganizeId",organizeId),
                new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
            };
            return FindListScalar<string>(generate_inventory_info, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VInventoryInfoEntity> QueryInventoryInfoList(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT mx.productId, wz.name wzmc, wz.gg, dbo.[f_getComplexWzSlandDw](mx.llsl,pu.zhyz,dw.name,zxdw.name) llsl
,Floor(mx.sjsl / mx.zhyz) deptSjsl, dw.name deptdw,(mx.sjsl % mx.zhyz) minSjsl, zxdw.name zxdw
,mx.zhyz, mx.ph, mx.pc, mx.yxq, mx.lsj
,CONVERT(NUMERIC(11,2),mx.lsj*mx.llsl/mx.Zhyz) lllsje
,CONVERT(NUMERIC(11,2),mx.lsj*mx.sjsl/mx.Zhyz) sjlsje
,ISNULL(wz.py,'') py, mx.Id pdmxId, pdxx.CreateTime
FROM dbo.kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) mx ON mx.pdId=pdxx.Id
LEFT JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=mx.productId AND wz.OrganizeId=pdxx.OrganizeId
LEFT JOIN dbo.rel_productWarehouse(NOLOCK) pw ON pw.warehouseId=pdxx.warehouseId AND pw.productId=mx.productId AND pw.OrganizeId=pdxx.OrganizeId
LEFT JOIN dbo.rel_productUnit(NOLOCK) pu ON pu.productId=pw.productId AND pu.unitId=pw.unitId
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=pu.unitId 
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit
WHERE pdxx.OrganizeId=@OrganizeId
AND pdxx.warehouseId=@warehouseId 
AND pdxx.Id=@pdId
");
            var p = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@pdId", param.pdId),
            };
            if (!string.IsNullOrWhiteSpace(param.keyWord))
            {
                sql.AppendLine("AND (wz.name LIKE '%'+@keyWord+'%' OR wz.py LIKE '%'+@keyWord+'%') ");
                p.Add(new SugarParameter("@keyWord", param.keyWord));
            }
            if (!string.IsNullOrWhiteSpace(param.lb))
            {
                sql.AppendLine("AND wz.typeId=@lbId ");
                p.Add(new SugarParameter("@lbId", param.lb));
            }
            if (!string.IsNullOrWhiteSpace(param.wzzt))
            {
                sql.AppendLine("AND wz.zt=@wzzt ");
                p.Add(new SugarParameter("@wzzt", param.wzzt));
            }
            switch (param.kcxs)
            {
                case (int)EnumKCXS.Xslkc://Hiển thị tồn kho bằng 0
                    sql.AppendLine("AND (mx.Llsl = 0 OR mx.Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl://Không hiển thị số lượng lý thuyết bằng 0
                    sql.AppendLine("AND mx.Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl://Không hiển thị số lượng thực tế bằng 0
                    sql.AppendLine("AND mx.Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl://Không hiển thị khi cả hai đều bằng 0
                    sql.AppendLine("AND (mx.Llsl <> 0 OR mx.Sjsl <> 0) ");
                    break;
            }
            return QueryWithPage<VInventoryInfoEntity>(sql.ToString(), pagination, p.ToArray());
        }

        /// <summary>
        /// Lấy thông tin kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VInventoryInfoEntity> QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT s.productId,s.wzmc,s.gg,dbo.[f_getComplexWzSlandDw](s.lsl,s.zhyz,s.deptdw,s.zxdw) llsl,s.deptdw,s.zxdw,s.zhyz,s.lsj
,CONVERT(NUMERIC(11,2),s.lsj/s.zhyz*s.lsl) lllsje,CONVERT(NUMERIC(11,2),s.lsj/s.zhyz*s.sjsl) sjlsje,s.py,s.CreateTime 
,Floor(s.sjsl / s.zhyz) deptSjsl
,(s.sjsl % s.zhyz) minSjsl
FROM (
	SELECT mx.productId, wz.name wzmc, wz.gg, SUM(mx.llsl) lsl,SUM(mx.sjsl) sjsl, dw.name deptdw, zxdw.name zxdw ,mx.zhyz, mx.lsj
	,ISNULL(wz.py,'') py, pdxx.CreateTime
	FROM dbo.kc_pdxx(NOLOCK) pdxx
	INNER JOIN dbo.kc_pdxxmx(NOLOCK) mx ON mx.pdId=pdxx.Id
	LEFT JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=mx.productId AND wz.OrganizeId=pdxx.OrganizeId
	LEFT JOIN dbo.rel_productWarehouse(NOLOCK) pw ON pw.warehouseId=pdxx.warehouseId AND pw.productId=mx.productId AND pw.OrganizeId=pdxx.OrganizeId
	LEFT JOIN dbo.rel_productUnit(NOLOCK) pu ON pu.productId=pw.productId AND pu.unitId=pw.unitId
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=pu.unitId 
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit
	WHERE pdxx.OrganizeId=@OrganizeId
	AND pdxx.warehouseId=@warehouseId 
	AND pdxx.Id=@pdId 
");
            var p = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@pdId", param.pdId),
            };
            if (!string.IsNullOrWhiteSpace(param.keyWord))
            {
                sql.AppendLine("	AND (wz.name LIKE '%'+@keyWord+'%' OR wz.py LIKE '%'+@keyWord+'%') ");
                p.Add(new SugarParameter("@keyWord", param.keyWord));
            }
            if (!string.IsNullOrWhiteSpace(param.lb))
            {
                sql.AppendLine("	AND wz.typeId=@lbId ");
                p.Add(new SugarParameter("@lbId", param.lb));
            }
            if (!string.IsNullOrWhiteSpace(param.wzzt))
            {
                sql.AppendLine("	AND wz.zt=@wzzt ");
                p.Add(new SugarParameter("@wzzt", param.wzzt));
            }
            switch (param.kcxs)
            {
                case (int)EnumKCXS.Xslkc://Hiển thị tồn kho bằng 0
                    sql.AppendLine("	AND (mx.Llsl = 0 OR mx.Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl://Không hiển thị số lượng lý thuyết bằng 0
                    sql.AppendLine("	AND mx.Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl://Không hiển thị số lượng thực tế bằng 0
                    sql.AppendLine("	AND mx.Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl://Không hiển thị khi cả hai đều bằng 0
                    sql.AppendLine("	AND (mx.Llsl <> 0 OR mx.Sjsl <> 0) ");
                    break;
            }

            sql.AppendLine("	GROUP BY mx.productId,wz.name,wz.gg,dw.name,zxdw.name,mx.zhyz,mx.lsj,wz.py,pdxx.CreateTime");
            sql.AppendLine(") s ");
            return QueryWithPage<VInventoryInfoEntity>(sql.ToString(), pagination, p.ToArray());
        }

        /// <summary>
        /// Hủy kiểm kê 
        /// Xóa Thông tin kiểm kê（xt_yp_pdxx）、Chi tiết thông tin kiểm kê（xt_yp_pdxxmx）
        /// </summary>
        public void CancelInventory(long pdId)
        {
            var sql = new StringBuilder(@"
DELETE FROM dbo.kc_pdxx WHERE Id=@pdId_param;
IF @@ERROR <>0
BEGIN
	SELECT 0 AS Jgxx
END
ELSE
BEGIN
	DECLARE @tmpTab TABLE(pdmxId BIGINT);
	INSERT INTO @tmpTab SELECT TOP 200 Id FROM dbo.kc_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;

	WHILE EXISTS(SELECT 1 FROM @tmpTab)
	BEGIN
		DELETE FROM dbo.kc_pdxxmx WHERE Id IN (SELECT pdmxId FROM @tmpTab);
		DELETE FROM @tmpTab;
		INSERT INTO @tmpTab SELECT TOP 200 Id FROM dbo.kc_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;
	END
	SELECT 1 AS Jgxx
END
");
            var result = FindListScalar<int>(sql.ToString(), new SugarParameter[]
            {
                new SugarParameter("@pdId_param", pdId)
            }).FirstOrDefault();
            if (result == 0)
            {
                throw new Exception("Hủy thất bại!");
            }
        }

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        /// <returns></returns>
        public string EndInventory(long pdId, string organizeId, string creatorCode)
        {
            var paraList = new List<SugarParameter>
            {
                new SugarParameter("@pdId", pdId),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@CreatorCode", creatorCode)
            };
            return FindListScalar<string>(end_Inventory, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// Lưu hàng loạt thông tin chênh lệch
        /// </summary>
        /// <param name="syList"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int InsertSyxxBatch(List<KcSyxxEntity> syList, string warehouseId, string organizeId)
        {
            var ret = 0;
            if (syList == null || syList.Count <= 0) return ret;
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                syList.ForEach(p =>
                {
                    _kcSyxxRepo.Insert(p);
                    if (_kfKcxxDmnService.UpdateKcsl(p.productId, p.pc, p.Ph, warehouseId, p.Sysl, organizeId, p.CreatorCode) <= 0)
                    {
                        throw new Exception("Sửa số lượng tồn kho thất bại");
                    }
                    ++ret;
                });
                db.Commit();
            }
            return ret;
        }

        /// <summary>
        /// Lưu chi tiết kiểm kê sau thay đổi
        /// </summary>
        /// <param name="saveInventoryDto"></param>
        /// <param name="pdId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SaveInventoryDetailNoPc(List<SaveInventoryDTO> saveInventoryDto, long pdId, string warehouseId, string organizeId)
        {
            var result = "";
            using (var db = new EFDbTransaction(Db).BeginTrans())
            {
                foreach (var p in saveInventoryDto)
                {
                    var param = new SugarParameter[]
                    {
                        new SugarParameter("@proId",p.productId),
                        new SugarParameter("@pdId",pdId),
                        new SugarParameter("@OrganizeId",organizeId),
                        new SugarParameter("@warehouseId",warehouseId),
                        new SugarParameter("@sl",p.sjsl),
                    };
                    result = FindListScalar<string>(save_inventory_info, param).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(result)) break;
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    db.Commit();
                }
            }
            return result;
        }
        // SQL scripts embedded from the original Infrastructure/TSql constants
        // (that folder is not part of the Core copy); user-visible messages translated.
        private const string generate_inventory_info = @"
BEGIN TRANSACTION;
BEGIN TRY
	DECLARE @pdId BIGINT;
	DECLARE @pdkssj DATETIME;
	SET @pdkssj=(SELECT TOP 1 pdxx.Jssj jssj 
				FROM dbo.kc_pdxx pdxx 
				WHERE zt='1' AND OrganizeId=@OrganizeId 
				ORDER BY pdxx.Jssj DESC)
	IF @pdkssj IS NULL OR @pdkssj=''
	BEGIN
		SET @pdkssj=GETDATE();
	END 

	INSERT INTO dbo.kc_pdxx( OrganizeId ,warehouseId ,kssj ,jssj ,Pdfs ,zt ,px ,creatorCode ,createTime ,lastModifyTime ,lastModifierCode)
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
		SELECT 'Tạo thông tin kiểm kê chính thất bại'  AS Jgxx;
		ROLLBACK TRANSACTION;
		RETURN;
	END

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpSourcepdmx') and type='U')
	BEGIN
		DROP TABLE #tmpSourcepdmx;
	END
		
	SELECT ROW_NUMBER() OVER (ORDER BY bmwz.productId) AS Id, kc.Id KcId, bmwz.productId
	, (CASE WHEN kc.Id IS NULL THEN 'CSHPDSC' ELSE kc.ph END) ph
	, (CASE WHEN kc.Id IS NULL THEN CONVERT(VARCHAR(8),GETDATE(),112) + REPLACE( CONVERT(VARCHAR(12),GETDATE(),14),':','')  ELSE kc.pc END) pc
	, (CASE WHEN kc.Id IS NULL THEN DATEADD(YEAR,1,GETDATE()) ELSE kc.yxq END) yxq
	, ISNULL(SUM(kc.kcsl),0) llsl, ISNULL(SUM(kc.kcsl),0) sjsl, ISNULL(wzdw.zhyz,1) zhyz , ISNULL(wz.lsj*ISNULL(wzdw.zhyz,1),0) lsj
	INTO #tmpSourcepdmx
	FROM dbo.rel_productWarehouse(NOLOCK) bmwz
	LEFT JOIN dbo.rel_productUnit(NOLOCK) wzdw ON wzdw.productId=bmwz.productId AND wzdw.unitId=bmwz.unitId AND wzdw.OrganizeId=bmwz.OrganizeId
	INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=bmwz.productId AND wz.OrganizeId=bmwz.OrganizeId
	LEFT JOIN dbo.kf_kcxx(NOLOCK) kc on kc.productId=bmwz.productId AND kc.warehouseId=bmwz.warehouseId AND kc.OrganizeId=bmwz.OrganizeId
	WHERE bmwz.warehouseId=@warehouseId 
	AND bmwz.OrganizeId=@OrganizeId 
	GROUP BY kc.Id, bmwz.productId, kc.ph, kc.pc, kc.yxq, wzdw.zhyz, wz.lsj

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
				
			INSERT INTO dbo.kc_pdxxmx( pdId ,productId ,ph ,pc ,yxq ,llsl ,sjsl ,zhyz ,lsj ,px ,zt ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
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
				
				INSERT INTO dbo.kf_kcxx( Id ,OrganizeId ,warehouseId ,productId ,ph ,pc ,yxq ,kcsl ,djsl ,crkmxId ,jj ,zhyz ,zt ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode)
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
	SELECT 'Tạo thông tin kiểm kê thất bại!' AS Jgxx;
	PRINT ERROR_MESSAGE();
	ROLLBACK TRANSACTION;
END CATCH
";
        private const string save_inventory_info = @"
BEGIN TRANSACTION
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpData') and type='U')
BEGIN
	DROP TABLE #tmpData;
END

DECLARE @pdmxId BIGINT;
DECLARE @sjsl INT;--Số lượng thực tế của bảng chi tiết kiểm kê

SELECT xm.productId, wz.name wzmc, xm.Id pdmxId, xm.pc,xm.ph,xm.sjsl
INTO #tmpData
FROM dbo.kc_pdxxmx xm
INNER JOIN dbo.kc_pdxx pd ON pd.Id=xm.pdId AND pd.zt='1' 
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=xm.productId AND wz.OrganizeId=pd.OrganizeId AND wz.zt='1'
WHERE xm.productId=@proId
AND pd.Id=@pdId
AND pd.OrganizeId=@OrganizeId
AND pd.warehouseId=@warehouseId
AND xm.zt='1'

IF @sl>0 --Tồn kho thực tế còn dư，thì cộng trực tiếp vào một lô hàng nhập kho gần nhất
BEGIN
	SELECT TOP 1 @pdmxId=pdmxId,@sjsl=sjsl FROM #tmpData ORDER BY pc DESC
	UPDATE dbo.kc_pdxxmx SET sjsl=sjsl+@sl WHERE Id=@pdmxId AND zt='1'
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

--Tồn kho thực tế ít hơn lý thuyết，thì ưu tiên trừ từ lô hàng nhập kho gần nhất，không đủ thì trừ từng lô theo thứ tự ngược của lô hàng
DECLARE @sysl INT;--Số lượng còn lại，Số lượng sau mỗi lần bù trừ
SET @sysl=@sl;
WHILE EXISTS(SELECT 1 FROM #tmpData WHERE sjsl>0)
BEGIN
	IF @sysl>=0 --Tất cả số lượng còn lại đã phân bổ xong，thì thoát vòng lặp，Kết thúc lưu
	BEGIN
		BREAK;
	END 
	
	--Còn tồn kho chưa phân bổ，Tiếp tục tìm lô hàng phù hợp để trừ tồn kho
	SELECT TOP 1 @pdmxId=pdmxId,@sjsl=sjsl FROM #tmpData WHERE sjsl>0 ORDER BY pc DESC
	SET @sysl=@sysl+@sjsl;
	IF (@sysl<=0)
	BEGIN
		UPDATE dbo.kc_pdxxmx SET sjsl=0 WHERE Id=@pdmxId AND zt='1'
		IF @@ROWCOUNT<=0
		BEGIN
			SELECT 'Lưu chi tiết kiểm kê thất bại'
			ROLLBACK TRANSACTION
			RETURN;
		END 
	END 
	ELSE IF(@sysl>0)
	BEGIN
		UPDATE dbo.kc_pdxxmx SET sjsl=@sysl WHERE Id=@pdmxId AND zt='1'
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
	SELECT 'Số tồn kho của hệ thống không đủ để bù trừ số thực tế đã nhập, vui lòng kiểm tra trong quá trình kiểm kê có phát sinh xuất kho hoặc báo hỏng hay không!';
END
ELSE
BEGIN
	SELECT '';
END
COMMIT TRANSACTION;
RETURN
";
        private const string end_Inventory = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#pdInfo') and type='U')
BEGIN
	DROP TABLE #pdInfo;
END

SELECT ROW_NUMBER() OVER (ORDER BY M.Id) AS Id, p.warehouseId, M.productId, ISNULL(M.ph,'') ph, Isnull(M.pc,'') pc
,Isnull(M.yxq,'1899-01-01') yxq, (M.sjsl - M.llsl) sysl, M.lsj, M.zhyz, bmwz.unitId
INTO #pdInfo
From dbo.kc_pdxx P WITH(NOLOCK)
INNER JOIN dbo.kc_pdxxmx M WITH(NOLOCK) ON P.Id=M.pdId AND M.sjsl-M.llsl<>0
INNER JOIN dbo.rel_productWarehouse(NOLOCK) bmwz ON bmwz.warehouseId=p.warehouseId AND bmwz.productId=m.productId AND bmwz.OrganizeId=p.OrganizeId
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
	DECLARE @productName VARCHAR(100);--Tên vật tư
	DECLARE @Ph VARCHAR(30);
	DECLARE @pc VARCHAR(30);
	DECLARE @Yxq DATETIME;
	DECLARE @Sysl INT;
	DECLARE @djh VARCHAR(50);--Tham số đầu ra
	DECLARE @Sykc INT;--Tồn kho còn lại
	DECLARE @Lsj MONEY;--Giá bán lẻ
	DECLARE @Zhyz INT;
	DECLARE @Jj MONEY;
	DECLARE @unitId VARCHAR(50);

	SELECT TOP 1 @id=Id, @warehouseId=warehouseId, @productId=productId, @Ph=ph, @pc=pc, @Yxq=yxq, @Sysl=sysl,@Lsj=lsj, @Zhyz=zhyz, @unitId=unitId FROM #pdInfo;
	WHILE EXISTS(SELECT 1 FROM #pdInfo)
	BEGIN
		SELECT @Kcsl = Sum(Isnull(kcsl,0)) From dbo.kf_kcxx WITH(NOLOCK) WHERE warehouseId=@warehouseId AND productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId
		SELECT @CrkmxId = CrkmxId From dbo.kf_kcxx WITH(NOLOCK)	WHERE warehouseId=@warehouseId And productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01')  AND OrganizeId=@OrganizeId
		SELECT @Jj=(CASE WHEN sum(kcsl)=0 THEN SUM(jj)/COUNT(1) ELSE ISNULL(sum(jj*kcsl)/sum(Kcsl),0) END) From dbo.kf_kcxx WITH(NOLOCK) WHERE warehouseId=@warehouseId And productId=@productId AND OrganizeId=@OrganizeId
		SELECT @productName=name FROM dbo.wz_product(NOLOCK) wz WHERE wz.Id=@productId AND OrganizeId=@OrganizeId;

		If Isnull(@Kcsl,0) + @Sysl < 0
		BEGIN
			ROLLBACK TRANSACTION
			SELECT @productName + ' không đủ tồn kho!' 
			RETURN;
		END		
			
		--Tạo số phiếu báo hỏng/báo thừa
		SET @djh='BSBYD'+REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(23), GETDATE(),25), '-',''),':',''),' ',''),'.','');
		UPDATE kf_kcxx Set kcsl = kcsl + @Sysl, LastModifierCode=@CreatorCode, LastModifyTime=GETDATE() Where warehouseId=@warehouseId AND productId=@productId And Isnull(ph,'') = @Ph AND Isnull(pc,'') = @pc And Isnull(yxq,'1899-01-01') = Isnull(@Yxq,'1899-01-01') AND OrganizeId=@OrganizeId
		IF @@ERROR <>0
		BEGIN
			ROLLBACK TRANSACTION
			SELECT 'Sửa tồn kho thất bại!';
			RETURN;
		END

		SELECT @Sykc = Isnull(@Kcsl,0) + @Sysl
		IF @Sysl > 0
		BEGIN
			INSERT INTO dbo.kc_syxx( OrganizeId ,warehouseId ,productId ,Ph ,pc ,Yxq ,Bgsj ,Sysl ,Lsj ,Zhyz ,Syyy ,Zrr ,Djh ,Sykc ,Jj ,remark ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode, UnitId)
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
				SELECT 'Ghi thông tin lãi lỗ thất bại!'
				RETURN;
			END
		END
		ELSE
		BEGIN
			INSERT INTO dbo.kc_syxx( OrganizeId ,warehouseId ,productId ,Ph ,pc ,Yxq ,Bgsj ,Sysl ,Lsj ,Zhyz ,Syyy ,Zrr ,Djh ,Sykc ,Jj ,remark ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode, UnitId)
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
				SELECT 'Ghi thông tin lãi lỗ thất bại!' ;
				RETURN;
			END
		END
		DELETE #pdInfo WHERE Id=@id;
		SELECT TOP 1 @id=Id, @warehouseId=warehouseId, @productId=productId, @Ph=ph, @pc=pc, @Yxq=yxq, @Sysl=sysl,@Lsj=lsj, @Zhyz=zhyz, @unitId=unitId FROM #pdInfo;
	END
END
	
UPDATE kc_pdxx SET Jssj = GETDATE(), LastModifierCode=@CreatorCode, LastModifyTime=GETDATE()
WHERE Id = @pdId
IF @@ERROR <>0
BEGIN
	ROLLBACK TRANSACTION
	SELECT 'Sửa kho thông tin kiểm kê thất bại!' ;
	RETURN;
END
	
COMMIT TRANSACTION
SELECT '';
";
    }
}
