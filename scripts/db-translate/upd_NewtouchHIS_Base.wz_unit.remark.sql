-- DB translation Scope A: NewtouchHIS_Base.wz_unit.remark (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [wz_unit] SET [remark] = 'Don vi noi bo' WHERE [remark] = N'内部单位';
UPDATE [wz_unit] SET [remark] = 'Dung cho mua sam' WHERE [remark] = N'用于采购';
