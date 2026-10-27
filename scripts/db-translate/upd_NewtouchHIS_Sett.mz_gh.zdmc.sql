-- DB translation Scope A: NewtouchHIS_Sett.mz_gh.zdmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_gh] SET [zdmc] = 'Viem da day ruot nhiem trung' WHERE [zdmc] = N'感染性胃肠炎';
UPDATE [mz_gh] SET [zdmc] = 'Cum co viem phoi, virus khong xac dinh' WHERE [zdmc] = N'流行性感冒伴有肺炎，病毒未标明';
UPDATE [mz_gh] SET [zdmc] = 'Benh Thai am' WHERE [zdmc] = N'太阴病';
