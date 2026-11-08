-- ============================================================================
-- R4-M3: VIEW ten ASCII cho bang danh muc BHYT quoc gia gb_*
-- Batch: R4-M3-IMPL (vong 4 dich OpenHIS), phuong an B (coordinator chot)
--   GIU NGUYEN bang gb_ypxx / gb_clxx / gb_sfxm (danh muc chuan BHYT quoc
--   gia; import lai tu file chuan se khoi phuc ten cot tieng Trung) va tao
--   3 VIEW ten ASCII tro toi bang goc. Code C# SELECT tu view bang ten
--   cot ASCII (trung alias VO cu) nen het chu Han trong khoi SELECT gb_*.
--   Import chuan sau nay chi can chay lai script nay de tao lai view.
-- DB dich: NewtouchHIS_Base_STG (gb_* chi ton tai o DB nay).
--   Doi DB khac: sua dong USE ben duoi cho dung ten DB dich.
-- Ma hoa file: UTF-8 (ten cot goc co chu Han) — chay sqlcmd voi -f 65001,
--   vd: sqlcmd -S <server> -d <db> -f 65001 -i r4-create-gb-views.sql
-- Re-runnable: CREATE OR ALTER VIEW (SQL Server 2016 SP1+).
-- KHONG dung bang _bak (gb_clxx_bak, gb_sfxm_xjbak): khong co code/DB
--   object nao tham chieu; de nguyen.
-- Cot trong view = dung alias ASCII hien dung trong SELECT cua
--   SysMedicineDmnService.cs (Base + PDS), GetYpypxxlist /
--   GetYbclxxlist / GetYbxmxxlist, nhanh case "1":
--   - vw_gb_ypxx: 30 cot (29 cot CJK goc + pym; khong lay yb_xybz, yb_xysm)
--   - vw_gb_clxx: 14 cot (11 cot CJK goc + ybxz + pym; cot goc
--     [单件产品名称] xuat hien 2 lan voi 2 alias ybtym / djcpmc — giu
--     nguyen quirk nay nhu code cu. Cot goc [医保通用名] KHONG co trong
--     view vi ban Base khong SELECT no; ban PDS truoc day SELECT no lam
--     ybtym, nay dong bo ve ban Base — xem r4-batch-r4-m3-report.md)
--   - vw_gb_sfxm: 11 cot (9 cot CJK goc + ybxz + pym; 175 cot F10-F184
--     khong bao gio SELECT nen khong dua vao view)
-- Du lieu trong bang van la tieng Trung (ten thuoc/vat tu/hang muc, gia
--   tri 甲/乙/丙, 材料/项目) — M3 chi don CJK khoi CODE, khong dich noi
--   dung tra cuu BHYT. Migration gia tri du lieu la follow-up rieng.
-- ============================================================================
USE [NewtouchHIS_Base_STG];
GO

CREATE OR ALTER VIEW dbo.vw_gb_ypxx AS
SELECT
    [药品代码] AS ypdm,
    [数据来源] AS sjly,
    [注册名称] AS zcmc,
    [商品名称] AS spmc,
    [注册剂型] AS zcjx,
    [实际剂型] AS sjjx,
    [注册规格] AS zcgg,
    [实际规格] AS sjgg,
    [包装材质] AS bzcz,
    [最小包装数量] AS zxbzsl,
    [最小制剂单位] AS zxzjdw,
    [最小包装单位] AS zxbzdw,
    [药品企业] AS ypqy,
    [上市药品持有人] AS shypcyr,
    [批准文号] AS pzwh,
    [原批准文号] AS ypzwh,
    [药品本位码] AS ypbwm,
    [分包装企业名称] AS fbzqymc,
    [生产企业] AS scqy,
    [市场状态] AS sczt,
    [医保药品名称] AS ybypmc,
    [2021版甲乙类] AS ybjyl,
    [医保剂型] AS ybjx,
    [编号] AS bh,
    [备注] AS bz,
    [甲乙类] AS jyl,
    [备注1] AS bz1,
    [备注（特殊属性标识）] AS bztssxbs,
    [医保支付标准] AS ybzfbz,
    pym
FROM dbo.gb_ypxx;
GO

CREATE OR ALTER VIEW dbo.vw_gb_clxx AS
SELECT
    [耗材代码] AS hcdm,
    [一级分类] AS yjfl,
    [二级分类] AS ejfl,
    [三级分类] AS sjfl,
    [单件产品名称] AS ybtym,
    [材质] AS cz,
    [特征] AS tz,
    [注册备案号] AS zcbah,
    [单件产品名称] AS djcpmc,
    [耗材企业] AS hcqy,
    [规格型号数] AS ggxhs,
    [注册备案人] AS zcbar,
    ybxz,
    pym
FROM dbo.gb_clxx;
GO

CREATE OR ALTER VIEW dbo.vw_gb_sfxm AS
SELECT
    [结算传输编码] AS jscsbm,
    [国家项目代码] AS gjxmdm,
    [国家项目名称] AS gjxmmc,
    [编码] AS bm,
    [项目名称] AS xmmc,
    [项目内涵] AS xmnh,
    [除外内容] AS cwnr,
    [计价单位] AS jjdw,
    [说明] AS sm,
    ybxz,
    pym
FROM dbo.gb_sfxm;
GO
