-- DB translation dict pass: Newtouch_EMR.bl_tsfh.fhnr (applied live; re-runnable; keyless UPDATE matching N(old) values)
USE [Newtouch_EMR];
UPDATE [bl_tsfh] SET [fhnr] = 'So thu tu so' WHERE [fhnr] = N'数字序号';
UPDATE [bl_tsfh] SET [fhnr] = 'Ky tu dac biet' WHERE [fhnr] = N'特殊字符';
UPDATE [bl_tsfh] SET [fhnr] = 'Phan giai doan lam sang ung thu phoi' WHERE [fhnr] = N'肺癌临床分期';
