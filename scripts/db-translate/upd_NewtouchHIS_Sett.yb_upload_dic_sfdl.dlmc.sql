-- DB translation Scope A: NewtouchHIS_Sett.yb_upload_dic_sfdl.dlmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Sett];
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi giuong benh' WHERE [dlmc] = N'床位费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi dang ky' WHERE [dlmc] = N'挂号费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi dieu duong' WHERE [dlmc] = N'护理费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi xet nghiem' WHERE [dlmc] = N'化验费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi kiem tra' WHERE [dlmc] = N'检查费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi khac' WHERE [dlmc] = N'其他费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi phau thuat' WHERE [dlmc] = N'手术费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi vat tu y te' WHERE [dlmc] = N'卫生材料费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi thuoc Tay y' WHERE [dlmc] = N'西药费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Thong thuong Phi chan doan dieu tri' WHERE [dlmc] = N'一般诊疗费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi kham benh' WHERE [dlmc] = N'诊察费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi dieu tri' WHERE [dlmc] = N'治疗费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Phi thuoc thanh pham Dong y' WHERE [dlmc] = N'中成药费';
UPDATE [yb_upload_dic_sfdl] SET [dlmc] = 'Thuoc phien Dong y Phi' WHERE [dlmc] = N'中药饮片费';
