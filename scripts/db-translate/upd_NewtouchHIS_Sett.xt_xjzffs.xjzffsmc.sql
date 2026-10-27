-- DB translation Scope A: NewtouchHIS_Sett.xt_xjzffs.xjzffsmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'WeChat' WHERE [xjzffsmc] = N'微信';
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Bao hiem y' WHERE [xjzffsmc] = N'医保代支';
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Tai khoan' WHERE [xjzffsmc] = N'预交账户';
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Phong kham' WHERE [xjzffsmc] = N'云诊所';
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Alipay' WHERE [xjzffsmc] = N'支付宝';
-- Late completions 2026-10-27 (found during code phase; applied live, 2 rows):
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Tien mat' WHERE [xjzffsmc] = N'现金';
-- SHORTENED: full form 'Thanh toan tong hop' (19 chars) exceeds varchar(10); applied 'Thanh toan' (10):
UPDATE [xt_xjzffs] SET [xjzffsmc] = 'Thanh toan' WHERE [xjzffsmc] = N'聚合支付';
