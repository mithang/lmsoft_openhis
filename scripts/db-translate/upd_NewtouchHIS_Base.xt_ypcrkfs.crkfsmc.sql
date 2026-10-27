-- DB translation Scope A: NewtouchHIS_Base.xt_ypcrkfs.crkfsmc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Xuat kho bao hong' WHERE [crkfsmc] = N'报损出库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Xuat kho dieu chuyen' WHERE [crkfsmc] = N'调拨出库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Xuat kho dieu chinh gia' WHERE [crkfsmc] = N'更价出库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Nhap kho dieu chinh gia' WHERE [crkfsmc] = N'更价入库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Nhap kho mua thuoc' WHERE [crkfsmc] = N'购药入库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Cuc quan ly duoc rut thuoc' WHERE [crkfsmc] = N'药监局抽药';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Tra thuoc kho duoc' WHERE [crkfsmc] = N'药库退药';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Nhap kho tang' WHERE [crkfsmc] = N'赠送入库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Xuat kho binh thuong' WHERE [crkfsmc] = N'正常出库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Nhap kho binh thuong' WHERE [crkfsmc] = N'正常入库';
UPDATE [xt_ypcrkfs] SET [crkfsmc] = 'Nhap kho che pham' WHERE [crkfsmc] = N'制剂入库';
