-- DB translation Scope A: Newtouch_MRMS.mr_dic_bafeetype.Name (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_MRMS];
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi dieu duong' WHERE [Name] = N'护理费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi khac' WHERE [Name] = N'其他费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi thuoc thanh pham Dong y' WHERE [Name] = N'中成药费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi phau thuat' WHERE [Name] = N'手术费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi phuc hoi' WHERE [Name] = N'康复费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi thuoc Dong y' WHERE [Name] = N'中草药费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Dieu tri Y hoc co truyen Phi' WHERE [Name] = N'中医治疗费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi gay me' WHERE [Name] = N'麻醉费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phi thuoc Tay y' WHERE [Name] = N'西药费';
UPDATE [mr_dic_bafeetype] SET [Name] = 'Phau thuat Phi dieu tri' WHERE [Name] = N'手术治疗费';
