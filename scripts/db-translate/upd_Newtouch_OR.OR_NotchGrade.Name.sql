-- DB translation Scope A: Newtouch_OR.OR_NotchGrade.Name (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_OR];
UPDATE [OR_NotchGrade] SET [Name] = 'Hang 3' WHERE [Name] = N'三级';
UPDATE [OR_NotchGrade] SET [Name] = 'Hang 2' WHERE [Name] = N'二级';
UPDATE [OR_NotchGrade] SET [Name] = 'Hang 1' WHERE [Name] = N'一级';
