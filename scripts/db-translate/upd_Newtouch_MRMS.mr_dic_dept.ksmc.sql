-- DB translation Scope A: Newtouch_MRMS.mr_dic_dept.ksmc (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_MRMS];
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Noi' WHERE [ksmc] = N'内科';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Ngoai' WHERE [ksmc] = N'外科';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Y hoc co truyen' WHERE [ksmc] = N'中医科';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khac Nghiep vu Khoa' WHERE [ksmc] = N'其他业务科室';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Nhi' WHERE [ksmc] = N'儿科';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Mat' WHERE [ksmc] = N'眼科';
UPDATE [mr_dic_dept] SET [ksmc] = 'Khoa Rang Ham Mat' WHERE [ksmc] = N'口腔科';
