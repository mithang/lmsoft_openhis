-- G30-F3: fix sp_yp_bsby — kcsl chi cong Sysl (don vi nho nhat), khong nhan them zhyz.
--   Base G30 3a/3b: Save Sysl=-1 tren thuoc zhyz=180 lam kcsl 540 -> 360 (sai, dung ra 539);
--   proc mau thuan noi bo: check mat hang (d170) va Sykc (d220) dung Sysl tho, chi UPDATE kcsl nhan @zhyz.
--   Hop dong don vi: YpSyxxVo ghi Sysl la don vi nho nhat; implementation EF cua vendor
--   (MedicineDmnService.SaveReportLossAndProfit(entity,kcsl,jj)) dung kcsl += Sysl.
--   Chi 1 caller trong cay: MedicineDmnService.SaveReportLossAndProfit(List<YpSyxxVo>).
--   Ap dung len STG 2026-11-11 (G30); khi deploy prod phai ap dung ALTER nay len DB prod.
--   File goc: sp_helptext tren NewtouchHIS_PDS_STG, chi doi 1 dong 257 (CREATE -> ALTER).
USE NewtouchHIS_PDS_STG;
GO
-- =============================================

-- Author:		<Li Jian>

-- Create date: <2018-07-16>

-- Description:	<报损报益>

-- =============================================

ALTER PROCEDURE [dbo].[sp_yp_bsby]

	-- Add the parameters for the stored procedure here

	@CreatorCode VARCHAR(50),

	@syxx yp_syxx READONLY

AS

BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from

	-- interfering with SELECT statements.

	SET NOCOUNT ON;

	

	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#syxx') and type='U')

	BEGIN

		DROP TABLE #syxx;

	END



	SELECT NEWID() Id, * INTO #syxx FROM @syxx;



	IF EXISTS(SELECT 1 FROM #syxx)

	BEGIN

		DECLARE @tmpSyxx TABLE(

			[Id] VARCHAR(50) NULL,

			[OrganizeId] VARCHAR(50) NULL,

			[yfbmCode] VARCHAR(100) NULL,

			[Ypdm] VARCHAR(20) NULL,

			[Ph] VARCHAR(30) NULL,

			[Yxq] DATETIME NULL,

			[Bgsj] DATETIME NULL,

			[Sysl] INT NULL,

			[Pfj] DECIMAL(11, 4) NULL,

			[Lsj] DECIMAL(11, 4) NULL,

			[Ykpfj] DECIMAL(11, 4) NULL,

			[Yklsj] DECIMAL(11, 4) NULL,

			[Zhyz] INT NULL,

			[Syyy] VARCHAR(50) NULL,

			[Zrr] CHAR(16) NULL,

			[Djh] CHAR(30) NULL,

			[Sykc] INT NULL,

			[Jj] DECIMAL(11, 4) NULL,

			[pc] VARCHAR(30) NULL,

			[remark] VARCHAR(100) NULL

		);

	

		DECLARE @Id VARCHAR(50);

		DECLARE @Sysl INT;

		DECLARE @currentkc INT;

		DECLARE @yfbmCode VARCHAR(50);

		DECLARE @pc VARCHAR(50);

		DECLARE @ph VARCHAR(50);

		DECLARE @OrganizeId VARCHAR(50);

		DECLARE @ypCode VARCHAR(50);

		DECLARE @kcId VARCHAR(50);

		DECLARE @zhyz INT;



		BEGIN TRANSACTION

		WHILE EXISTS(SELECT 1 FROM #syxx)

		BEGIN		

			INSERT INTO @tmpSyxx SELECT TOP 1 * FROM #syxx

			

			SELECT @Id=ISNULL(Id, ''), @Sysl=ISNULL(Sysl, 0), @OrganizeId=ISNULL(OrganizeId, ''), @pc=ISNULL(pc, ''), @ph=ISNULL(Ph,''), @yfbmCode=ISNULL(yfbmCode,''), @ypCode=Ypdm, @zhyz=Zhyz FROM @tmpSyxx		

			

			SELECT TOP 1 @kcId=ISNULL(kcxx.kcId, ''), @currentkc=ISNULL(kcxx.kcsl,0) 

			FROM dbo.xt_yp_kcxx(NOLOCK) kcxx

			INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=kcxx.ypdm AND yp.OrganizeId=kcxx.OrganizeId

			INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId

			WHERE kcxx.tybz='0' 

			AND kcxx.yfbmCode=@yfbmCode 

			AND kcxx.ypdm=@ypCode 

			AND kcxx.pc=@pc 

			AND kcxx.ph=@ph 

			AND kcxx.OrganizeId=@OrganizeId 

			AND kcxx.zt='1'

		

			IF LEN(LTRIM(RTRIM(ISNULL(@kcId, ''))))<=0

			BEGIN

				SELECT '从库存中未查到有效的药品';

				ROLLBACK TRANSACTION;

				RETURN -1;

			END



			IF(@Sysl<=0 AND @currentkc+@Sysl<0)

			BEGIN

				PRINT '报损'

				SELECT '报损数量不能大于当前可用库存';

				ROLLBACK TRANSACTION

				RETURN -1;				

			END



			INSERT INTO dbo.xt_yp_syxx( syId ,OrganizeId ,yfbmCode ,Ypdm ,Ph ,Yxq ,Bgsj ,Sysl ,Pfj ,Lsj ,Ykpfj ,Yklsj ,Zhyz ,Syyy ,Zrr ,Djh ,Sykc ,Jj ,pc ,zt ,px ,CreatorCode ,CreateTime ,LastModifyTime ,LastModifierCode ,remark )

			SELECT    NEWID() , -- syId - varchar(50)

				        kcxx.OrganizeId , -- OrganizeId - varchar(50)

				        kcxx.yfbmCode , -- yfbmCode - varchar(100)

				        kcxx.ypdm , -- Ypdm - varchar(20)

				        kcxx.Ph , -- Ph - char(30)

				        syxx.Yxq , -- Yxq - datetime

				        syxx.Bgsj , -- Bgsj - datetime

				        syxx.Sysl , -- Sysl - int

				        syxx.Pfj , -- Pfj - decimal

				        syxx.Lsj , -- Lsj - decimal

				        syxx.Ykpfj , -- Ykpfj - decimal

				        syxx.Yklsj , -- Yklsj - decimal

				        syxx.Zhyz , -- Zhyz - int

				        syxx.Syyy , -- Syyy - varchar(50)

				        syxx.Zrr , -- Zrr - char(16)

				        syxx.Djh , -- Djh - char(30)

				        kcxx.Kcsl+syxx.Sysl , -- Sykc - int

				        kcxx.jj , -- Jj - decimal

				        syxx.pc , -- pc - varchar(30)

				        '1' , -- zt - char(1)

				        0 , -- px - int

				        ISNULL(@CreatorCode, '') , -- CreatorCode - varchar(50)

				        GETDATE() , -- CreateTime - datetime

				        NULL , -- LastModifyTime - datetime
				        '' , -- LastModifierCode - varchar(50)

				        syxx.remark  -- remark - varchar(100)

			FROM @tmpSyxx syxx

			INNER JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=syxx.Ypdm AND kcxx.OrganizeId=syxx.OrganizeId AND kcxx.tybz='0' AND kcxx.yfbmCode=syxx.yfbmCode AND kcxx.pc=syxx.pc AND kcxx.ph=syxx.Ph  

			IF @@ERROR <> 0

			BEGIN    

				SELECT '插入损益信息失败!'

				ROLLBACK TRANSACTION;

				RETURN -1;

			END

			

			UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl+@Sysl, LastModifyTime=GETDATE(), LastModifierCode=@CreatorCode WHERE kcId=@kcId

			IF @@ERROR <> 0

			BEGIN    

				SELECT '修改库存失败!'

				ROLLBACK TRANSACTION;

				RETURN -1;

			END

							

			DELETE FROM @tmpSyxx;

			DELETE FROM #syxx WHERE Id=@Id;    			

		END

       

        COMMIT TRANSACTION



		SELECT '';

		RETURN 0;

	END

END
