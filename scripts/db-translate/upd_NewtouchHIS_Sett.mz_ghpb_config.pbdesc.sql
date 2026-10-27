-- DB translation Scope A: NewtouchHIS_Sett.mz_ghpb_config.pbdesc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_ghpb_config] SET [pbdesc] = 'Khoa Phu san Binh thuong' WHERE [pbdesc] = N'妇科普通';
UPDATE [mz_ghpb_config] SET [pbdesc] = 'Don gian Kham noi khoa' WHERE [pbdesc] = N'简易内科门诊';
UPDATE [mz_ghpb_config] SET [pbdesc] = 'Khoa Noi Binh thuong' WHERE [pbdesc] = N'内科普通';
UPDATE [mz_ghpb_config] SET [pbdesc] = 'Khoa Ngoai Binh thuong' WHERE [pbdesc] = N'外科普通';
UPDATE [mz_ghpb_config] SET [pbdesc] = 'Khoa Y hoc co truyen Binh thuong' WHERE [pbdesc] = N'中医科普通';
