-- DB translation dict pass: Newtouch_EMR.Mr_Writing_Rules.Remark (applied live; re-runnable; keyless UPDATE matching N(old) values)
USE [Newtouch_EMR];
UPDATE [Mr_Writing_Rules] SET [Remark] = 'Viet trong 3 ngay' WHERE [Remark] = N'三天内书写';
UPDATE [Mr_Writing_Rules] SET [Remark] = 'Viet trong 5 ngay' WHERE [Remark] = N'五天内书写';
UPDATE [Mr_Writing_Rules] SET [Remark] = 'Trong quy dinh' WHERE [Remark] = N'规定内';
UPDATE [Mr_Writing_Rules] SET [Remark] = 'Benh trinh dau tien trong 3 ngay' WHERE [Remark] = N'首次病程三天内';
