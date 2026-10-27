-- DB translation Scope A: NewtouchHIS_Sett.table_invoice_placeCode.name (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [table_invoice_placeCode] SET [name] = 'Tong vien' WHERE [name] = N'总院';
