-- DB translation Scope A: NewtouchHIS_Sett.mz_ghpb_schedule.PeriodDesc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_ghpb_schedule] SET [PeriodDesc] = 'Buoi sang' WHERE [PeriodDesc] = N'上午';
UPDATE [mz_ghpb_schedule] SET [PeriodDesc] = 'Buoi chieu' WHERE [PeriodDesc] = N'下午';
