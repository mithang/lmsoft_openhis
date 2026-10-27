-- DB translation Scope A: NewtouchHIS_Sett.mz_cf.cflxmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [mz_cf] SET [cflxmc] = 'Tay y Don thuoc' WHERE [cflxmc] = N'西药处方';
UPDATE [mz_cf] SET [cflxmc] = 'Thuoc Y hoc co truyen Don thuoc' WHERE [cflxmc] = N'中药处方';
