-- DB translation Scope A: Newtouch_MRMS.mr_rel_dept.hisdeptname (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_MRMS];
UPDATE [mr_rel_dept] SET [hisdeptname] = 'Khoa Noi' WHERE [hisdeptname] = N'内科';
UPDATE [mr_rel_dept] SET [hisdeptname] = 'Khoa Ngoai' WHERE [hisdeptname] = N'外科';
