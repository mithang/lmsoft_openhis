-- DB translation Scope A: NewtouchHIS_herp.rel_warehouseDept.deptName (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_herp];
UPDATE [rel_warehouseDept] SET [deptName] = 'Kho thuoc' WHERE [deptName] = N'药库';
UPDATE [rel_warehouseDept] SET [deptName] = 'Khoa Rang Ham Mat' WHERE [deptName] = N'口腔科';
UPDATE [rel_warehouseDept] SET [deptName] = 'Phong Xet nghiem' WHERE [deptName] = N'检验室';
UPDATE [rel_warehouseDept] SET [deptName] = 'Khoa Ngoai' WHERE [deptName] = N'外科';
UPDATE [rel_warehouseDept] SET [deptName] = 'Khoa Phu san' WHERE [deptName] = N'妇科';
UPDATE [rel_warehouseDept] SET [deptName] = 'Dien tam do' WHERE [deptName] = N'心电图';
UPDATE [rel_warehouseDept] SET [deptName] = 'Phong Dieu duong' WHERE [deptName] = N'护理部';
UPDATE [rel_warehouseDept] SET [deptName] = 'Khoa Noi' WHERE [deptName] = N'内科';
UPDATE [rel_warehouseDept] SET [deptName] = 'Khoa Y hoc co truyen' WHERE [deptName] = N'中医科';
UPDATE [rel_warehouseDept] SET [deptName] = 'Nha thuoc' WHERE [deptName] = N'药房';
