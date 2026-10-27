-- DB translation Scope A: NewtouchHIS_Sett.mz_ghpb_schedule.Title (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_ghpb_schedule] SET [Title] = 'Khoa Y hoc co truyen Binh thuong' WHERE [Title] = N'中医科普通';
UPDATE [mz_ghpb_schedule] SET [Title] = 'Don gian Kham noi khoa' WHERE [Title] = N'简易内科门诊';
UPDATE [mz_ghpb_schedule] SET [Title] = 'Khoa Noi Binh thuong' WHERE [Title] = N'内科普通';
