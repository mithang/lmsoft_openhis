-- DB translation Scope A: Newtouch_OR.OR_Anesthesia.AnesName (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_OR];
UPDATE [OR_Anesthesia] SET [AnesName] = 'Gay te cuc bo' WHERE [AnesName] = N'局部麻醉';
UPDATE [OR_Anesthesia] SET [AnesName] = 'Gay me toan than' WHERE [AnesName] = N'全身麻醉';
