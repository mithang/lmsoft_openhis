-- DB translation Scope A: NewtouchHIS_Base.pacs_jcbw.jcff (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [pacs_jcbw] SET [jcff] = 'The nghieng' WHERE [jcff] = N'侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'The dung nghieng' WHERE [jcff] = N'立侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'The dung truoc sau + The nghieng' WHERE [jcff] = N'前后立位+侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'The thang' WHERE [jcff] = N'正位';
UPDATE [pacs_jcbw] SET [jcff] = 'The thang + The cheo' WHERE [jcff] = N'正位+斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'The truc' WHERE [jcff] = N'轴位';
