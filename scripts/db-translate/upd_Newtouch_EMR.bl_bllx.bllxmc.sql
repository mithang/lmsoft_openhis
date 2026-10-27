-- DB translation Scope A: Newtouch_EMR.bl_bllx.bllxmc (applied live; re-runnable; keyless targets match N'old' values)
USE [Newtouch_EMR];
UPDATE [bl_bllx] SET [bllxmc] = 'Y lenh don' WHERE [bllxmc] = N'医嘱单';
UPDATE [bl_bllx] SET [bllxmc] = 'Vao/ra vien Ghi chep' WHERE [bllxmc] = N'出入院记录';
UPDATE [bl_bllx] SET [bllxmc] = 'Trang dau benh an' WHERE [bllxmc] = N'病案首页';
UPDATE [bl_bllx] SET [bllxmc] = 'Ho so dien bien benh' WHERE [bllxmc] = N'病程记录';
-- Late completions 2026-10-27 (found during code phase; applied live, 2 rows):
UPDATE [bl_bllx] SET [bllxmc] = 'Van ban cham soc' WHERE [bllxmc] = N'护理文书';
UPDATE [bl_bllx] SET [bllxmc] = 'Van ban khac' WHERE [bllxmc] = N'其他文书';
