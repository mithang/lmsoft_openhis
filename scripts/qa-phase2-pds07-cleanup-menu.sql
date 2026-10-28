/* ============================================================================
   QA-phase2 PDS-07 — don 3 menu URL chet cua PDS (404 rong, goc cung khong
   co controller tuong ung — da grep toan cay V2.2 = 0 ket qua, KHONG tao stub).
   CHUA CHAY TREN DB DEMO: chi chay sau khi dieu phoi vien duyet rieng.
   Da kiem chung tren STG (NewtouchHIS_PDS_STG) va xac nhan DB demo
   (NewtouchHIS_PDS, chi SELECT) co dung 3 row, cung Id va cung zt.

   Row (DB PDS Sys_Module):
     1. Id 882e5794-10d4-43a1-959d-01df243a0bf2 — "Tra cuu ton kho thuoc trong ngay"
        /Medicine/PharmaceuticalStocks — zt = 1 (truoc don)
        Parent: 22a1346f-c7fd-42f5-871a-293355c2f3ee ("Quan ly ton kho kho thuoc")
     2. Id 200bedb3-37fe-48d6-a051-0f2125db71d0 — "Thong ke so luong phat thuoc theo y lenh"
        /ReportManage/YpckQuery/DoctorsOrderQuery — zt = 1 (truoc don)
        Parent: a7305f6e-396f-42f4-8ea9-538df9414b8e ("Bao cao")
     3. Id a1f60990-e42c-41d1-8342-b37bec990d0d — "Nha thuoc Y hoc co truyen Phi sac thuoc ho Thong"
        /ReportManage/YpckQuery/DecoctingfeeQuery — zt = 0 SAN (da bi an khoi
        menu theo vai tro, nhung van hien trong luoi quan ly menu va van 404
        khi go truc tiep; script set lai 0 cho dong nhat, khong doi gi)

   Phuong an: tat (zt = '0'), KHONG DELETE — dung quy uoc soft-disable cua
   he thong (menu theo vai tro loc zt = '1', xem SysModuleDmnService.
   GetMenuListByTopOrg). Tat xong, 2 row zt=1 se bien khoi menu vai tro;
   khoi phuc neu can: UPDATE zt = '1' WHERE Id IN (...) tu bang backup.
   Khong can xoa Sys_ModuleButton / authorize lien quan: duong menu loc
   theo zt nen chung khong con hien; giu lai de khoi phuc nguyen ven.
   ============================================================================ */

-- 0. Kiem tra truoc khi chay (expect: 3 dong, zt = 1, 1, 0).
SELECT Id, ParentId, Name, UrlAddress, zt
FROM Sys_Module
WHERE Id IN ('882e5794-10d4-43a1-959d-01df243a0bf2',
             '200bedb3-37fe-48d6-a051-0f2125db71d0',
             'a1f60990-e42c-41d1-8342-b37bec990d0d');

-- 1. Backup (chi tao neu chua co; chay lai script khong ghi de backup cu).
IF OBJECT_ID('Sys_Module_QA2_PDS07_BACKUP', 'U') IS NULL
BEGIN
    SELECT * INTO Sys_Module_QA2_PDS07_BACKUP
    FROM Sys_Module
    WHERE Id IN ('882e5794-10d4-43a1-959d-01df243a0bf2',
                 '200bedb3-37fe-48d6-a051-0f2125db71d0',
                 'a1f60990-e42c-41d1-8342-b37bec990d0d');
END;

-- 2. Tat 3 menu chet (guard ca Id lan UrlAddress, idempotent).
UPDATE Sys_Module
SET zt = '0'
WHERE (Id = '882e5794-10d4-43a1-959d-01df243a0bf2'
       AND UrlAddress = '/Medicine/PharmaceuticalStocks')
   OR (Id = '200bedb3-37fe-48d6-a051-0f2125db71d0'
       AND UrlAddress = '/ReportManage/YpckQuery/DoctorsOrderQuery')
   OR (Id = 'a1f60990-e42c-41d1-8342-b37bec990d0d'
       AND UrlAddress = '/ReportManage/YpckQuery/DecoctingfeeQuery');

-- 3. Kiem tra sau khi chay (expect: 3 dong, deu zt = 0; dong zt=1 con lai = 0).
SELECT Id, Name, UrlAddress, zt
FROM Sys_Module
WHERE Id IN ('882e5794-10d4-43a1-959d-01df243a0bf2',
             '200bedb3-37fe-48d6-a051-0f2125db71d0',
             'a1f60990-e42c-41d1-8342-b37bec990d0d');

SELECT COUNT(*) AS RemainingEnabledDeadMenus
FROM Sys_Module
WHERE Id IN ('882e5794-10d4-43a1-959d-01df243a0bf2',
             '200bedb3-37fe-48d6-a051-0f2125db71d0',
             'a1f60990-e42c-41d1-8342-b37bec990d0d')
  AND zt = '1';
