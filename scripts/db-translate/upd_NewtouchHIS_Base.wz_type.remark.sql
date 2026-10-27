-- DB translation Scope A: NewtouchHIS_Base.wz_type.remark (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [wz_type] SET [remark] = 'Chi tiet' WHERE [remark] = N'明细';
UPDATE [wz_type] SET [remark] = 'Nhom nho' WHERE [remark] = N'小组';
UPDATE [wz_type] SET [remark] = 'Can khu trung' WHERE [remark] = N'需消毒';
