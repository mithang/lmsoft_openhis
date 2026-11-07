-- Fixup (giai đoạn 1A, điều phối viên): revert 2 dòng nhóm 关系 mà Scope A dịch keyless
-- (upd_Newtouch_MRMS.mr_dic_common.ItemName.sql: 其他 -> 'Khac', 女 -> 'Nu') làm vỡ
-- join BHYT trong proc live usp_Inp_ybupload_cyjs_setlinfo (Sett):
--   left join [Newtouch_MRMS].dbo.[mr_dic_common] g on g.RlueName='关系' and g.itemName=b.GX
-- Join này đòi ItemName khớp b.GX (dữ liệu bệnh nhân tiếng Trung); 2 dòng dịch không
-- bao giờ khớp -> trả NULL cho bệnh nhân có GX = 其他 / 女.
-- Chỉ revert trong phạm vi RlueName = '关系' (ItemCode 3 và 8); các nhóm khác giữ bản
-- dịch vì join của chúng dùng ItemCode/RlueCode (báo cáo 1A mục 6).
-- Backup trước khi sửa: ~/workspace/db-translate-dict-backup/fixup_mr_dic_common_guanxi.before.txt
USE [Newtouch_MRMS];
UPDATE [mr_dic_common] SET [ItemName] = N'女' WHERE [RlueName] = N'关系' AND [ItemName] = 'Nu';
UPDATE [mr_dic_common] SET [ItemName] = N'其他' WHERE [RlueName] = N'关系' AND [ItemName] = 'Khac';
