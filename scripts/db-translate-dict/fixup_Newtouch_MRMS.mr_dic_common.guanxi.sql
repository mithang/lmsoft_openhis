-- Fixup (giai doan 1A, dieu phoi vien): revert 2 dong nhom 关系 ma Scope A dich keyless
-- (upd_Newtouch_MRMS.mr_dic_common.ItemName.sql: 其他 -> 'Khac', 女 -> 'Nu') lam vo
-- join BHYT trong proc live usp_Inp_ybupload_cyjs_setlinfo (Sett):
--   left join [Newtouch_MRMS].dbo.[mr_dic_common] g on g.RlueName='关系' and g.itemName=b.GX
-- Join nay doi ItemName khop b.GX (du lieu benh nhan tieng Trung); 2 dong dich khong
-- bao gio khop -> tra NULL cho benh nhan co GX = 其他 / 女.
-- Chi revert trong pham vi RlueName = '关系' (ItemCode 3 va 8); cac nhom khac giu ban
-- dich vi join cua chung dung ItemCode/RlueCode (bao cao 1A muc 6).
-- Backup truoc khi sua: ~/workspace/db-translate-dict-backup/fixup_mr_dic_common_guanxi.before.txt
USE [Newtouch_MRMS];
UPDATE [mr_dic_common] SET [ItemName] = N'女' WHERE [RlueName] = N'关系' AND [ItemName] = 'Nu';
UPDATE [mr_dic_common] SET [ItemName] = N'其他' WHERE [RlueName] = N'关系' AND [ItemName] = 'Khac';
