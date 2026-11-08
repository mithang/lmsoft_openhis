-- ============================================================================
-- Herp: fix proc herp_DongBoVatTuSangHangMucThuPhi hardcode ten DB demo (F31)
-- Ngay: 2026-11-09. Vong FOLLOW-UP 31, commit kem theo.
--
-- Van de: ban vendor hardcode [NewtouchHIS_herp] / [NewtouchHIS_Base].
-- Ten nay chi dung tren production. Tren STG cac DB la
-- NewtouchHIS_herp_STG / NewtouchHIS_Base_STG nen proc doc/ghi nham sang
-- DB demo (nguy co ghi cheo demo khi ma vat tu trung nhau).
--
-- Fix: suy ten DB dong tu DB_NAME() (DB hien tai = Herp DB noi proc song):
--   Herp DB = DB_NAME()
--   Base DB = REPLACE(DB_NAME(), '_herp', '_Base')
-- Dung cho ca 2 scheme ten: NewtouchHIS_herp -> NewtouchHIS_Base va
-- NewtouchHIS_herp_STG -> NewtouchHIS_Base_STG. Ten DB ghep bang QUOTENAME.
--
-- Loc iswzsame='1' duoc GIU NGUYEN (quy tac vendor: chi vat tu tieu hao moi
-- dong bo cac cot day du). Duong tao moi (InsertProductToSfxm trong C#) tao
-- dong xt_sfxm cho MOI vat tu nen xoa duoc bat ke iswzsame.
--
-- CACH CHAY: chay script nay KHI DANG NOI TOI Herp DB cua moi truong
-- (khong co lenh USE cung ten DB trong script). Da ap dung tren STG o F31.
-- CHU Y: proc KHONG ton tai tren DB demo — KHONG chay script nay tren demo
-- de tao moi proc khi chua co quyet rieng.
-- ============================================================================
ALTER PROCEDURE [dbo].[herp_DongBoVatTuSangHangMucThuPhi]
    @orgId varchar(50),
    @wzcode varchar(50)
AS
BEGIN
    DECLARE @herpDb sysname = DB_NAME();
    DECLARE @baseDb sysname = REPLACE(@herpDb, N'_herp', N'_Base');
    DECLARE @sfdlCode VARCHAR(50);
    SELECT @sfdlCode = Value FROM [dbo].[Sys_Config] WHERE Code = 'sfdlCode';

    DECLARE @sql NVARCHAR(MAX) = N'
SELECT a.productCode sfxmCode, a.name sfxmmc, @sfdlCode sfdlCode, NULL badlCode, NULL nbdlCode, a.OrganizeId,
NULL duration, a.py, b.name dw, a.lsj dj, NULL flCode, ISNULL(a.zfbl, 1.00) zfbl, ISNULL(a.zfxz, ''1'') zfxz, ''0'' mzzybz, ''0'' ssbz, ''0'' tsbz, ''1'' sfbz,
a.ybdm, NULL wjdm, NULL bz, a.CreatorCode, a.CreateTime, a.LastModifyTime, a.LastModifierCode,
NULL px, a.zt, 1 dwjls, NULL jjcl, NULL zxks, a.gg, NULL LastYBUploadTime,
NULL ssdj, NULL sqlx, 1 ybbz, NULL xnhybdm, a.gjybdm, NULL cxjje, a.zczh pzwh, NULL sccj,
NULL gjybmc, NULL wz_Code, NULL wz_ConsumablesID, NULL wz_SerialNumber,
NULL wz_Packing, 0 wz_IsHight, NULL wz_MaterialCode,
NULL xjbs, NULL ssicd9bm, a.iswzsame
INTO #temp
FROM ' + QUOTENAME(@herpDb) + N'.dbo.wz_product a
LEFT JOIN ' + QUOTENAME(@baseDb) + N'.dbo.wz_unit b ON a.minUnit = b.Id AND b.zt = ''1''
WHERE a.OrganizeId = @orgId AND a.productCode = @wzcode AND a.iswzsame = ''1'';

IF (EXISTS (SELECT 1 FROM ' + QUOTENAME(@baseDb) + N'.dbo.xt_sfxm WHERE OrganizeId = @orgId AND sfxmCode = @wzcode))
BEGIN
UPDATE b SET b.sfxmCode = a.sfxmCode, b.sfxmmc = a.sfxmmc, b.sfdlCode = a.sfdlCode, b.badlCode = a.badlCode, b.nbdlCode = a.nbdlCode,
b.OrganizeId = a.OrganizeId, b.duration = a.duration, b.py = a.py,
b.dw = a.dw, b.dj = a.dj, b.flCode = a.flCode, b.zfbl = a.zfbl, b.zfxz = a.zfxz,
b.mzzybz = a.mzzybz, b.ssbz = a.ssbz, b.tsbz = a.tsbz, b.sfbz = a.sfbz, b.ybdm = a.ybdm,
b.wjdm = a.wjdm, b.bz = a.bz, b.CreatorCode = a.CreatorCode, b.CreateTime = a.CreateTime,
b.LastModifyTime = a.LastModifyTime, b.LastModifierCode = a.LastModifierCode,
b.px = a.px, b.zt = a.zt, b.dwjls = a.dwjls, b.jjcl = a.jjcl, b.zxks = a.zxks, b.gg = a.gg,
b.LastYBUploadTime = a.LastYBUploadTime, b.ssdj = a.ssdj, b.sqlx = a.sqlx, b.ybbz = a.ybbz,
b.xnhybdm = a.xnhybdm, b.gjybdm = a.gjybdm, b.cxjje = a.cxjje, b.pzwh = a.pzwh,
b.sccj = a.sccj, b.gjybmc = a.gjybmc, b.wz_Code = a.wz_Code, b.wz_ConsumablesID = a.wz_ConsumablesID, b.wz_SerialNumber = a.wz_SerialNumber,
b.wz_Packing = a.wz_Packing, b.wz_IsHight = a.wz_IsHight, b.wz_MaterialCode = a.wz_MaterialCode, b.xjbs = a.xjbs,
b.ssicd9bm = a.ssicd9bm, b.iswzsame = a.iswzsame
FROM #temp a
LEFT JOIN ' + QUOTENAME(@baseDb) + N'.dbo.xt_sfxm b ON a.OrganizeId = b.OrganizeId AND a.sfxmCode = b.sfxmCode
END
ELSE
BEGIN
INSERT INTO ' + QUOTENAME(@baseDb) + N'.dbo.xt_sfxm (sfxmCode, sfxmmc, sfdlCode, badlCode, nbdlCode, OrganizeId, duration, py, dw, dj, flCode, zfbl, zfxz, mzzybz, ssbz, tsbz, sfbz, ybdm, wjdm, bz, CreatorCode, CreateTime, LastModifyTime, LastModifierCode, px, zt, dwjls, jjcl, zxks, gg, LastYBUploadTime, ssdj, sqlx, ybbz, xnhybdm, gjybdm, cxjje, pzwh, sccj, gjybmc, wz_Code, wz_ConsumablesID, wz_SerialNumber, wz_Packing, wz_IsHight, wz_MaterialCode, xjbs, ssicd9bm, iswzsame)
SELECT sfxmCode, sfxmmc, sfdlCode, badlCode, nbdlCode, OrganizeId, duration, py, dw, dj, flCode, zfbl, zfxz, mzzybz, ssbz, tsbz, sfbz, ybdm, wjdm, bz, CreatorCode, CreateTime, LastModifyTime, LastModifierCode, px, zt, dwjls, jjcl, zxks, gg, LastYBUploadTime, ssdj, sqlx, ybbz, xnhybdm, gjybdm, cxjje, pzwh, sccj, gjybmc, wz_Code, wz_ConsumablesID, wz_SerialNumber, wz_Packing, wz_IsHight, wz_MaterialCode, xjbs, ssicd9bm, iswzsame FROM #temp
END

DROP TABLE #temp;';

    EXEC sp_executesql @sql,
        N'@orgId varchar(50), @wzcode varchar(50), @sfdlCode varchar(50)',
        @orgId = @orgId, @wzcode = @wzcode, @sfdlCode = @sfdlCode;
END
GO
