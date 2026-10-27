-- DB translation Scope A: NewtouchHIS_Base.xt_yfbm.yfbmmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [xt_yfbm] SET [yfbmmc] = 'Nha thuoc Y hoc co truyen' WHERE [yfbmId] = '8';
UPDATE [xt_yfbm] SET [yfbmmc] = 'Nha thuoc Tay y' WHERE [yfbmId] = '9';
UPDATE [xt_yfbm] SET [yfbmmc] = 'Kho thuoc Y hoc co truyen' WHERE [yfbmId] = '11';
-- Late completion 2026-10-27 (found during code phase; applied live, 1 row):
UPDATE [xt_yfbm] SET [yfbmmc] = 'Kho thuoc Tay y' WHERE [yfbmId] = '10';
