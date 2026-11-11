namespace Newtouch.Infrastructure.TSQL
{
    /// <summary>
    /// Thuốc
    /// </summary>
    public class TSqlDrugs
    {
        /// <summary>
        /// Ủy quyền thuốc bộ phận
        /// </summary>
        public const string bm_yp_xxtb = @"
DECLARE @bmypId VARCHAR(50), @ypmc VARCHAR(256), @result VARCHAR(256);
SET @result='';

--G39-F8: operateType chỉ nhận 0 (Thêm) / 1 (Xóa); base: operateType=2 trả '' → Success giả.
IF @operateType NOT IN (0, 1)
BEGIN
	SET @result='Loại thao tác không hợp lệ (chỉ nhận 0: Thêm, 1: Xóa).';
	SELECT @result;
	RETURN;
END

SELECT @bmypId=ISNULL(bmypxx.bmypId, ''), @ypmc=yp.ypmc
FROM NewtouchHIS_Base.dbo.V_C_xt_yp yp  
LEFT JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm=yp.ypCode AND bmypxx.OrganizeId=yp.OrganizeId AND bmypxx.yfbmCode=@yfbmCode AND bmypxx.zt='1'
WHERE yp.OrganizeId=@OrganizeId 
AND yp.zt='1'
AND yp.ypId=@ypId

--G39-F8: ypId không tồn tại → @ypmc NULL; base: INSERT...SELECT khớp 0 dòng, @result '' → Success giả.
IF @ypmc IS NULL
BEGIN
	SET @result='Không tìm thấy thuốc (mã nội bộ '+@ypId+').';
	SELECT @result;
	RETURN;
END

IF @bmypId IS NOT NULL AND @bmypId<>'' 
BEGIN
	IF @operateType=0
	BEGIN
		SET @result= 'Thuốc【'+@ypmc+'】đã tồn tại';
	END
	ELSE IF @operateType=1
	BEGIN
		DELETE FROM dbo.xt_yp_bmypxx WHERE bmypId=@bmypId;
	END
END
ELSE
BEGIN 
	IF @operateType=0
	BEGIN
		INSERT INTO XT_YP_BMYPXX( bmypId ,OrganizeId ,yfbmCode ,Ypdm ,Ypkw ,Zcxh ,Pxfs1 ,Pxfs2 ,Kcsx ,Kcxx ,Jhd ,Jhl ,Ypsxdm ,zt ,Ylsx ,Sysx,CreatorCode,CreateTime)
		--G39-F8: TOP 1 — base: join multiplicity chèn 2 dòng cho 1 lần gọi (ypId 839, dlCode 01).
		SELECT TOP 1 NEWID(),
				@OrganizeId ,
				K.yfbmCode ,
				Y.ypCode ,
				'' ,
				'' ,
				'' ,
				'' ,
				'0' ,
				'0' ,
				'0' ,
				'0' ,
				X.yptssx ,
				'1' ,
				'0' ,
				'0',
				'admin',
				GETDATE()
		FROM    [NewtouchHIS_Base].dbo.V_S_xt_yfbm_yp K
				INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yp Y ON Y.dlCode = K.dlCode AND Y.OrganizeId = k.OrganizeId
				INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx X ON X.ypId = Y.ypId AND X.OrganizeId = k.OrganizeId
				INNER JOIN [newtouchhis_base].dbo.V_S_xt_sfdl sd ON sd.dlCode = Y.dlCode AND sd.OrganizeId=K.OrganizeId AND sd.zt='1'
		WHERE   K.yfbmCode = @yfbmCode
				AND k.OrganizeId = @OrganizeId
				AND Y.ypId=@ypId
		ORDER BY K.yfbmCode 

		IF @@ERROR <> 0
		BEGIN
			SET @result= 'Thêm thuốc khoa này【'+@ypmc+'】thất bại'
		END
	END
	ELSE IF @operateType=1
	BEGIN
		SET @result= 'Thuốc【'+@ypmc+'】không có nhà thuốc này, không thể xóa';
	END
END
    
SELECT @result;
";
    }
}
