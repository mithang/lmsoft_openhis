-- DB translation Scope A: Newtouch_EMR.Mr_Writing_Rules.RulesType (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_EMR];
UPDATE [Mr_Writing_Rules] SET [RulesType] = 'Dieu duong Ho so' WHERE [RulesType] = N'护理病程记录';
UPDATE [Mr_Writing_Rules] SET [RulesType] = 'Trang dau benh an' WHERE [RulesType] = N'病案首页';
UPDATE [Mr_Writing_Rules] SET [RulesType] = 'Noi tru Thong bao' WHERE [RulesType] = N'住院告知书';
UPDATE [Mr_Writing_Rules] SET [RulesType] = 'Benh nhan Thong bao' WHERE [RulesType] = N'患者告知书';
UPDATE [Mr_Writing_Rules] SET [RulesType] = 'Kiem thu Benh an' WHERE [RulesType] = N'测试病历';
