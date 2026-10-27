-- DB translation Scope A: NewtouchHIS_Base.Sys_Report.SystemName (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [Sys_Report] SET [SystemName] = 'Benh an Quan ly' WHERE [SystemName] = N'病案管理';
UPDATE [Sys_Report] SET [SystemName] = 'Quyet toan He thong' WHERE [SystemName] = N'结算系统';
UPDATE [Sys_Report] SET [SystemName] = 'Phau thuat He thong' WHERE [SystemName] = N'手术系统';
UPDATE [Sys_Report] SET [SystemName] = 'Kham suc khoe Quan ly' WHERE [SystemName] = N'体检管理';
UPDATE [Sys_Report] SET [SystemName] = 'Vat tu Quan ly' WHERE [SystemName] = N'物资管理';
UPDATE [Sys_Report] SET [SystemName] = 'Nha thuoc Kho thuoc' WHERE [SystemName] = N'药房药库';
