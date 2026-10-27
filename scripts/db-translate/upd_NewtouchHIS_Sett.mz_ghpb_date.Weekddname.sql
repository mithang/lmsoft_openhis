-- DB translation Scope A: NewtouchHIS_Sett.mz_ghpb_date.Weekddname (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu ba' WHERE [Weekddname] = N'星期二';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu bay' WHERE [Weekddname] = N'星期六';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Chu nhat' WHERE [Weekddname] = N'星期日';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu tu' WHERE [Weekddname] = N'星期三';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu nam' WHERE [Weekddname] = N'星期四';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu sau' WHERE [Weekddname] = N'星期五';
UPDATE [mz_ghpb_date] SET [Weekddname] = 'Thu hai' WHERE [Weekddname] = N'星期一';
