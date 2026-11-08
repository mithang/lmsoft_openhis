-- R4-M4 seed: du 8 gia tri VN x moi to hop (OrganizeId, yfbmCode) dang co trong xt_yp_dj (PDS_STG)
-- Quy tac: djh=1, xgdjh=0, CreatorCode='root', CreateTime=getdate(), zt='1', ypdjId=newid()
-- djqz set tay theo mapping da co (NBSLCK cho gia tri moi 'Linh noi bo xuat kho don')
-- Idempotent: WHERE NOT EXISTS (RTRIM(djmc), OrganizeId, yfbmCode); khong dong den 6 dong TQ.
-- LUU Y DEPLOY: file nay chay tren STG voi USE NewtouchHIS_PDS_STG; khi chay tren DB demo/prod
-- thi doi USE thanh ten DB dich (vd NewtouchHIS_PDS) truoc khi chay. Da chay tren STG: +54 dong.
USE NewtouchHIS_PDS_STG;
SET NOCOUNT ON;
INSERT INTO dbo.xt_yp_dj (ypdjId, OrganizeId, yfbmCode, djmc, djqz, djh, xgdjh, CreatorCode, CreateTime, zt)
SELECT NEWID(), c.OrganizeId, c.yfbmCode, v.djmc, v.djqz, 1, 0, 'root', GETDATE(), '1'
FROM (SELECT DISTINCT OrganizeId, yfbmCode FROM dbo.xt_yp_dj) c
CROSS JOIN (VALUES
 ('Linh thuoc noi bo don','NBSLD'),
 ('Nhap kho thuoc don','YPRKD'),
 ('Xuat kho ra ngoai don','WBCKD'),
 ('Bao cao hong mat va thua don','BSBYD'),
 ('Noi bo Phat thuoc don','NBFYD'),
 ('Phat thuoc theo khoa don','KSFYD'),
 ('Linh Phat thuoc don','SLFYD'),
 ('Linh noi bo xuat kho don','NBSLCK')
) v(djmc, djqz)
WHERE NOT EXISTS (
  SELECT 1 FROM dbo.xt_yp_dj d
  WHERE RTRIM(d.djmc) = v.djmc AND d.OrganizeId = c.OrganizeId AND d.yfbmCode = c.yfbmCode
);
SELECT @@ROWCOUNT AS inserted_rows;
