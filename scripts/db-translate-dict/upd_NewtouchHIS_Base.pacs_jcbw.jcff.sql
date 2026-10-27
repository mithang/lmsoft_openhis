-- DB translation dict pass: NewtouchHIS_Base.pacs_jcbw.jcff (applied live; re-runnable; keyless UPDATE matching N(old) values)
USE [NewtouchHIS_Base];
UPDATE [pacs_jcbw] SET [jcff] = 'Thang - nghieng' WHERE [jcff] = N'正侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang - cheo' WHERE [jcff] = N'正斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'Truoc - sau + nghieng' WHERE [jcff] = N'前后位+侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Truoc - sau + cheo' WHERE [jcff] = N'前后位+斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang + nghieng phai' WHERE [jcff] = N'正位+右侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang + nghieng trai' WHERE [jcff] = N'正位+左侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Truoc - sau' WHERE [jcff] = N'前后位';
UPDATE [pacs_jcbw] SET [jcff] = 'Truoc - sau nam + nghieng' WHERE [jcff] = N'前后卧位+侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Nam nghieng' WHERE [jcff] = N'卧侧位';
UPDATE [pacs_jcbw] SET [jcff] = 'Mo + dong mieng' WHERE [jcff] = N'张+闭口位';
UPDATE [pacs_jcbw] SET [jcff] = 'Tu the cheo' WHERE [jcff] = N'斜位位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang + cheo phai' WHERE [jcff] = N'正位+右斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang + cheo trai' WHERE [jcff] = N'正位+左斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'Thang - nghieng + cheo doi' WHERE [jcff] = N'正侧位+双斜位';
UPDATE [pacs_jcbw] SET [jcff] = 'Tu the ech' WHERE [jcff] = N'蛙式位';
