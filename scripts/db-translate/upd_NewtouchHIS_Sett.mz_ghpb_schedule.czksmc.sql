-- DB translation Scope A: NewtouchHIS_Sett.mz_ghpb_schedule.czksmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_ghpb_schedule] SET [czksmc] = 'Khoa Y hoc co truyen' WHERE [czksmc] = N'中医科';
UPDATE [mz_ghpb_schedule] SET [czksmc] = 'Khoa Noi' WHERE [czksmc] = N'内科';
