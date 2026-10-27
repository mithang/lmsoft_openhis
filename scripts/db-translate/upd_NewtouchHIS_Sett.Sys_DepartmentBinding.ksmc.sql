-- DB translation Scope A: NewtouchHIS_Sett.Sys_DepartmentBinding.ksmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [Sys_DepartmentBinding] SET [ksmc] = 'Khoa Noi' WHERE [ksmc] = N'内科';
UPDATE [Sys_DepartmentBinding] SET [ksmc] = 'Khoa Y hoc co truyen' WHERE [ksmc] = N'中医科';
