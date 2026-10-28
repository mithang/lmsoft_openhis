# Danh mục ảnh CHẶNG 5 — Chụp toàn bộ màn hình và chức năng (role cao nhất)

> Nhóm app: Herp 5236, EMR 5235, Base HOSP 5233, Base Web 5238, CIS 5234, PDS 5237.
> Tài khoản duy nhất sử dụng: **000000 (HospAdministrator)** — không bao giờ thử xzadmin hay admin.
> Nhánh: V2.2, HEAD `7d30446a` (UI đã redesign, đã redeploy lên prod ở chặng 4).
> Công cụ: Playwright + Firefox local. Chỉ chụp viewport (không dùng full_page).
> Khung hình: desktop 1280×900, mobile 390×844 (giả lập viewport — OpenHIS không có app native).
> Thời gian chụp: phiên 2026-10-29 (giờ +07). Ảnh lưu tại `shots-redesign/` (thư mục gitignore — KHÔNG commit ảnh).
> Quy tắc cứng đã tuân thủ: CHỈ CHỤP, không submit form nào, không ghi nghiệp vụ nào lên prod hay DB demo;
> mọi ảnh đều chụp trên prod; không dựng instance STG nào trong chặng này (xem mục 4 về các màn hình cần ghi trước).
> Mật khẩu demo chỉ dùng để đăng nhập, không ghi trong file này.

## 1. Tổng hợp

| App | Cổng | Desktop | Mobile | Tổng |
|---|---|---|---|---|
| Herp — Hệ thống quản lý vật tư | 5236 | 102 | 103 | 205 |
| EMR — Bệnh án điện tử | 5235 | 82 | 83 | 165 |
| Base HOSP — Hệ thống quản trị (bệnh viện) | 5233 | 155 | 156 | 311 |
| Base Web — Dữ liệu nền | 5238 | 41 | 42 | 83 |
| CIS — Hệ thống thông tin lâm sàng | 5234 | 219 | 206 | 425 |
| PDS — Hệ thống nhà thuốc / kho thuốc | 5237 | 128 | 132 | 260 |
| **Tổng** | | **727** | **722** | **1449** |

Độ phủ theo view nguồn của app (số route có file .cshtml tương ứng đã chụp được / tổng route liệt kê từ source):

| App | Desktop | Mobile |
|---|---|---|
| Herp — Hệ thống quản lý vật tư | 93/95 | 93/95 |
| EMR — Bệnh án điện tử | 81/86 | 81/86 |
| Base HOSP — Hệ thống quản trị (bệnh viện) | 154/165 | 154/165 |
| Base Web — Dữ liệu nền | 40/41 | 40/41 |
| CIS — Hệ thống thông tin lâm sàng | 213/261 | 199/261 |
| PDS — Hệ thống nhà thuốc / kho thuốc | 127/162 | 130/162 |

Phương pháp liệt kê route: (1) đăng nhập 000000, lấy toàn bộ menu sidebar/trang chủ mà role này thấy,
mở từng màn hình menu và từng route liên kết tới được (2 cấp); (2) liệt kê toàn bộ view `.cshtml`
của từng app từ source (trừ partial `_*.cshtml`, Shared, Login, Error, trang test/demo) và mở trực tiếp từng
route bằng GET chỉ-đọc; (3) với mỗi controller có view `Form`, chụp form ở trạng thái mở (không submit);
(4) nút "Thêm" trên thanh công cụ lưới được thử bấm trong probe — không mở dialog nhìn thấy được
(body không đổi, không có layer mới), nên form được chụp qua route `Form` thay vì qua dialog.

## 2. Herp — Hệ thống quản lý vật tư (herp, cổng 5236)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/BillManage/ApplyBill/Query` | herp-billmanage-applybill-query-desktop.png | herp-billmanage-applybill-query-mobile.png |
| `/BillManage/OutOrInStorageBill/Approval` | herp-billmanage-outorinstoragebill-approval-desktop.png | herp-billmanage-outorinstoragebill-approval-mobile.png |
| `/BillManage/OutOrInStorageBill/MyTemporaryCrkdj` | herp-billmanage-outorinstoragebill-mytemporarycrkdj-desktop.png | herp-billmanage-outorinstoragebill-mytemporarycrkdj-mobile.png |
| `/BillManage/OutOrInStorageBill/Query` | herp-billmanage-outorinstoragebill-query-desktop.png | herp-billmanage-outorinstoragebill-query-mobile.png |
| `/BillManage/PurchasingOrder/AuditPurchaseOrder` | herp-billmanage-purchasingorder-auditpurchaseorder-desktop.png | herp-billmanage-purchasingorder-auditpurchaseorder-mobile.png |
| `/BillManage/PurchasingOrder/GeneratingPurchaseOrder` | herp-billmanage-purchasingorder-generatingpurchaseorder-desktop.png | herp-billmanage-purchasingorder-generatingpurchaseorder-mobile.png |
| `/BillManage/PurchasingOrder/PurchaseDetailView` | herp-billmanage-purchasingorder-purchasedetailview-desktop.png | herp-billmanage-purchasingorder-purchasedetailview-mobile.png |
| `/BillManage/PurchasingOrder/PurchaseOrderQuery` | herp-billmanage-purchasingorder-purchaseorderquery-desktop.png | herp-billmanage-purchasingorder-purchaseorderquery-mobile.png |
| `/BillManage/PurchasingOrder/PurchaseView` | herp-billmanage-purchasingorder-purchaseview-desktop.png | herp-billmanage-purchasingorder-purchaseview-mobile.png |
| `/BillManage/PurchasingPlan/AuditPurchasingPlan` | herp-billmanage-purchasingplan-auditpurchasingplan-desktop.png | herp-billmanage-purchasingplan-auditpurchasingplan-mobile.png |
| `/BillManage/PurchasingPlan/FillPurchasingPlan` | herp-billmanage-purchasingplan-fillpurchasingplan-desktop.png | herp-billmanage-purchasingplan-fillpurchasingplan-mobile.png |
| `/BillManage/PurchasingPlan/PurchasingPlan` | herp-billmanage-purchasingplan-purchasingplan-desktop.png | herp-billmanage-purchasingplan-purchasingplan-mobile.png |
| `/BillManage/PurchasingPlan/PurchasingPlanQuery` | herp-billmanage-purchasingplan-purchasingplanquery-desktop.png | herp-billmanage-purchasingplan-purchasingplanquery-mobile.png |
| `/DeliveryManage/DeliveryQuery/Index` | herp-deliverymanage-deliveryquery-index-desktop.png | herp-deliverymanage-deliveryquery-index-mobile.png |
| `/DepartmentManage/Department/Form` | herp-departmentmanage-department-form-desktop.png | herp-departmentmanage-department-form-mobile.png |
| `/DepartmentManage/Department/Index` | herp-departmentmanage-department-index-desktop.png | herp-departmentmanage-department-index-mobile.png |
| `/Home/About` | herp-home-about-desktop.png | herp-home-about-mobile.png |
| `/Home/Index` | herp-home-desktop.png | herp-home-mobile.png |
| `/Home/KfChange` | herp-home-kfchange-desktop.png | herp-home-kfchange-mobile.png |
| `/Home/SwithKf` | herp-home-swithkf-desktop.png | herp-home-swithkf-mobile.png |
| `/LicenceManage/Licence/Form` | herp-licencemanage-licence-form-desktop.png | herp-licencemanage-licence-form-mobile.png |
| `/LicenceManage/Licence/Index` | herp-licencemanage-licence-index-desktop.png | herp-licencemanage-licence-index-mobile.png |
| `/LicenceManage/Licence/LicenceBelonged` | herp-licencemanage-licence-licencebelonged-desktop.png | herp-licencemanage-licence-licencebelonged-mobile.png |
| `/LicenceManage/Licence/LicenceBelongedForm` | herp-licencemanage-licence-licencebelongedform-desktop.png | herp-licencemanage-licence-licencebelongedform-mobile.png |
| `/LicenceManage/Licence/LicenceType` | herp-licencemanage-licence-licencetype-desktop.png | herp-licencemanage-licence-licencetype-mobile.png |
| `/LicenceManage/Licence/LicenceTypeForm` | herp-licencemanage-licence-licencetypeform-desktop.png | herp-licencemanage-licence-licencetypeform-mobile.png |
| `/Login/Index` | herp-login-desktop.png | herp-login-mobile.png |
| `/ProductManage/PriceAdjustment/PriceAdjustmentApproval` | herp-productmanage-priceadjustment-priceadjustmentapproval-desktop.png | herp-productmanage-priceadjustment-priceadjustmentapproval-mobile.png |
| `/ProductManage/PriceAdjustment/PriceAdjustmentHistory` | herp-productmanage-priceadjustment-priceadjustmenthistory-desktop.png | herp-productmanage-priceadjustment-priceadjustmenthistory-mobile.png |
| `/ProductManage/PriceAdjustment/PriceAdjustmentProfitAndLossQuery` | herp-productmanage-priceadjustment-priceadjustmentprofitandlossquery-desktop.png | herp-productmanage-priceadjustment-priceadjustmentprofitandlossquery-mobile.png |
| `/ProductManage/PriceAdjustment/PriceAdjustmentRequest` | herp-productmanage-priceadjustment-priceadjustmentrequest-desktop.png | herp-productmanage-priceadjustment-priceadjustmentrequest-mobile.png |
| `/ProductManage/Product/Form` | herp-productmanage-product-form-desktop.png | herp-productmanage-product-form-mobile.png |
| `/ProductManage/Product/Index` | herp-productmanage-product-index-desktop.png | herp-productmanage-product-index-mobile.png |
| `/ProductManage/ProductCrkfs/Form` | herp-productmanage-productcrkfs-form-desktop.png | herp-productmanage-productcrkfs-form-mobile.png |
| `/ProductManage/ProductCrkfs/Index` | herp-productmanage-productcrkfs-index-desktop.png | herp-productmanage-productcrkfs-index-mobile.png |
| `/ProductManage/ProductSfxm/Form` | herp-productmanage-productsfxm-form-desktop.png | herp-productmanage-productsfxm-form-mobile.png |
| `/ProductManage/ProductSfxm/Index` | herp-productmanage-productsfxm-index-desktop.png | herp-productmanage-productsfxm-index-mobile.png |
| `/ProductManage/ProductType/Form` | herp-productmanage-producttype-form-desktop.png | herp-productmanage-producttype-form-mobile.png |
| `/ProductManage/ProductType/Index` | herp-productmanage-producttype-index-desktop.png | herp-productmanage-producttype-index-mobile.png |
| `/ProductManage/ProductUnit/Form` | herp-productmanage-productunit-form-desktop.png | herp-productmanage-productunit-form-mobile.png |
| `/ProductManage/ProductUnit/Index` | herp-productmanage-productunit-index-desktop.png | herp-productmanage-productunit-index-mobile.png |
| `/PurchaseManage/Purchase/Bill` | herp-purchasemanage-purchase-bill-desktop.png | herp-purchasemanage-purchase-bill-mobile.png |
| `/PurchaseManage/Purchase/BillForm` | herp-purchasemanage-purchase-billform-desktop.png | herp-purchasemanage-purchase-billform-mobile.png |
| `/PurchaseManage/Purchase/BillPay` | herp-purchasemanage-purchase-billpay-desktop.png | herp-purchasemanage-purchase-billpay-mobile.png |
| `/PurchaseManage/Purchase/BillPayForm` | herp-purchasemanage-purchase-billpayform-desktop.png | herp-purchasemanage-purchase-billpayform-mobile.png |
| `/PurchaseManage/Purchase/Delivery` | herp-purchasemanage-purchase-delivery-desktop.png | herp-purchasemanage-purchase-delivery-mobile.png |
| `/PurchaseManage/Purchase/DeliveryForm` | herp-purchasemanage-purchase-deliveryform-desktop.png | herp-purchasemanage-purchase-deliveryform-mobile.png |
| `/PurchaseManage/Purchase/Form` | herp-purchasemanage-purchase-form-desktop.png | herp-purchasemanage-purchase-form-mobile.png |
| `/PurchaseManage/Purchase/Index` | herp-purchasemanage-purchase-index-desktop.png | herp-purchasemanage-purchase-index-mobile.png |
| `/PurchaseManage/Purchase/LocationForm` | herp-purchasemanage-purchase-locationform-desktop.png | herp-purchasemanage-purchase-locationform-mobile.png |
| `/PurchaseManage/Purchase/LocationIndex` | herp-purchasemanage-purchase-locationindex-desktop.png | herp-purchasemanage-purchase-locationindex-mobile.png |
| `/PurchaseManage/Purchase/PurchaseView` | herp-purchasemanage-purchase-purchaseview-desktop.png | herp-purchasemanage-purchase-purchaseview-mobile.png |
| `/PurchaseManage/Purchase/QueryYY151` | herp-purchasemanage-purchase-queryyy151-desktop.png | herp-purchasemanage-purchase-queryyy151-mobile.png |
| `/PurchaseManage/Purchase/QueryYY152` | herp-purchasemanage-purchase-queryyy152-desktop.png | herp-purchasemanage-purchase-queryyy152-mobile.png |
| `/PurchaseManage/Purchase/QueryYY154` | herp-purchasemanage-purchase-queryyy154-desktop.png | herp-purchasemanage-purchase-queryyy154-mobile.png |
| `/PurchaseManage/Purchase/QueryYY157` | herp-purchasemanage-purchase-queryyy157-desktop.png | herp-purchasemanage-purchase-queryyy157-mobile.png |
| `/PurchaseManage/Purchase/QueryYY159` | herp-purchasemanage-purchase-queryyy159-desktop.png | herp-purchasemanage-purchase-queryyy159-mobile.png |
| `/PurchaseManage/Purchase/QueryYY160` | herp-purchasemanage-purchase-queryyy160-desktop.png | herp-purchasemanage-purchase-queryyy160-mobile.png |
| `/PurchaseManage/Purchase/QueryYY161` | herp-purchasemanage-purchase-queryyy161-desktop.png | herp-purchasemanage-purchase-queryyy161-mobile.png |
| `/PurchaseManage/Purchase/QueryYY162` | herp-purchasemanage-purchase-queryyy162-desktop.png | herp-purchasemanage-purchase-queryyy162-mobile.png |
| `/PurchaseManage/Purchase/QueryYY164` | herp-purchasemanage-purchase-queryyy164-desktop.png | herp-purchasemanage-purchase-queryyy164-mobile.png |
| `/PurchaseManage/Purchase/ReForm` | herp-purchasemanage-purchase-reform-desktop.png | herp-purchasemanage-purchase-reform-mobile.png |
| `/PurchaseManage/Purchase/ReIndex` | herp-purchasemanage-purchase-reindex-desktop.png | herp-purchasemanage-purchase-reindex-mobile.png |
| `/Report/Index` | herp-report-index-desktop.png | herp-report-index-mobile.png |
| `/Report/PrintReport` | herp-report-printreport-desktop.png | herp-report-printreport-mobile.png |
| `/StorageManage/PSIStatistics/Index` | herp-storagemanage-psistatistics-index-desktop.png | herp-storagemanage-psistatistics-index-mobile.png |
| `/StorageManage/ProfitAndLoss/Index` | herp-storagemanage-profitandloss-index-desktop.png | herp-storagemanage-profitandloss-index-mobile.png |
| `/StorageManage/ProfitAndLoss/QueryProfitAndLoss` | herp-storagemanage-profitandloss-queryprofitandloss-desktop.png | herp-storagemanage-profitandloss-queryprofitandloss-mobile.png |
| `/StorageManage/ProfitAndLoss/Syyy` | herp-storagemanage-profitandloss-syyy-desktop.png | herp-storagemanage-profitandloss-syyy-mobile.png |
| `/StorageManage/ProfitAndLoss/SyyyForm` | herp-storagemanage-profitandloss-syyyform-desktop.png | herp-storagemanage-profitandloss-syyyform-mobile.png |
| `/StorageManage/StockCarryOver/Index` | herp-storagemanage-stockcarryover-index-desktop.png | herp-storagemanage-stockcarryover-index-mobile.png |
| `/StorageManage/StockInventory/Index` | herp-storagemanage-stockinventory-index-desktop.png | herp-storagemanage-stockinventory-index-mobile.png |
| `/StorageManage/StockInventory/InventoryAction` | herp-storagemanage-stockinventory-inventoryaction-desktop.png | herp-storagemanage-stockinventory-inventoryaction-mobile.png |
| `/StorageManage/StockInventory/InventoryQuery` | herp-storagemanage-stockinventory-inventoryquery-desktop.png | herp-storagemanage-stockinventory-inventoryquery-mobile.png |
| `/StorageManage/Storage/ApplyOutStock` | herp-storagemanage-storage-applyoutstock-desktop.png | herp-storagemanage-storage-applyoutstock-mobile.png |
| `/StorageManage/Storage/DeliveryOfReturn` | herp-storagemanage-storage-deliveryofreturn-desktop.png | herp-storagemanage-storage-deliveryofreturn-mobile.png |
| `/StorageManage/Storage/DeliveryOfReturnInlineEdit` | herp-storagemanage-storage-deliveryofreturninlineedit-desktop.png | herp-storagemanage-storage-deliveryofreturninlineedit-mobile.png |
| `/StorageManage/Storage/DeliveryToDepartment` | herp-storagemanage-storage-deliverytodepartment-desktop.png | herp-storagemanage-storage-deliverytodepartment-mobile.png |
| `/StorageManage/Storage/DeliveryToDepartmentInlineEdit` | herp-storagemanage-storage-deliverytodepartmentinlineedit-desktop.png | herp-storagemanage-storage-deliverytodepartmentinlineedit-mobile.png |
| `/StorageManage/Storage/DepartmentApply` | herp-storagemanage-storage-departmentapply-desktop.png | herp-storagemanage-storage-departmentapply-mobile.png |
| `/StorageManage/Storage/DirectDelivery` | herp-storagemanage-storage-directdelivery-desktop.png | herp-storagemanage-storage-directdelivery-mobile.png |
| `/StorageManage/Storage/DirectDeliveryInlineEdit` | herp-storagemanage-storage-directdeliveryinlineedit-desktop.png | herp-storagemanage-storage-directdeliveryinlineedit-mobile.png |
| `/StorageManage/Storage/ExpiredStorageQuery` | herp-storagemanage-storage-expiredstoragequery-desktop.png | herp-storagemanage-storage-expiredstoragequery-mobile.png |
| `/StorageManage/Storage/InStorage` | herp-storagemanage-storage-instorage-desktop.png | herp-storagemanage-storage-instorage-mobile.png |
| `/StorageManage/Storage/InStorageInlineEdit` | herp-storagemanage-storage-instorageinlineedit-desktop.png | herp-storagemanage-storage-instorageinlineedit-mobile.png |
| `/StorageManage/Storage/OutStorage` | herp-storagemanage-storage-outstorage-desktop.png | herp-storagemanage-storage-outstorage-mobile.png |
| `/StorageManage/Storage/OutStorageInlineEdit` | herp-storagemanage-storage-outstorageinlineedit-desktop.png | herp-storagemanage-storage-outstorageinlineedit-mobile.png |
| `/StorageManage/Storage/WarehouseStorageQuery` | herp-storagemanage-storage-warehousestoragequery-desktop.png | herp-storagemanage-storage-warehousestoragequery-mobile.png |
| `/SupplierManage/Supplier/Form` | herp-suppliermanage-supplier-form-desktop.png | herp-suppliermanage-supplier-form-mobile.png |
| `/SupplierManage/Supplier/Index` | herp-suppliermanage-supplier-index-desktop.png | herp-suppliermanage-supplier-index-mobile.png |
| `/SystemManage/SystemManage/Index` | herp-systemmanage-systemmanage-index-desktop.png | herp-systemmanage-systemmanage-index-mobile.png |
| `/SystemManage/SystemManage/SysConfig` | herp-systemmanage-systemmanage-sysconfig-desktop.png | herp-systemmanage-systemmanage-sysconfig-mobile.png |
| `/SystemManage/SystemManage/SysLog` | herp-systemmanage-systemmanage-syslog-desktop.png | herp-systemmanage-systemmanage-syslog-mobile.png |
| `/SystemManage/SystemManage/SysModule` | herp-systemmanage-systemmanage-sysmodule-desktop.png | herp-systemmanage-systemmanage-sysmodule-mobile.png |
| `/SystemManage/SystemManage/SysRole` | herp-systemmanage-systemmanage-sysrole-desktop.png | herp-systemmanage-systemmanage-sysrole-mobile.png |
| `/SystemManage/SystemManage/SysStaff` | herp-systemmanage-systemmanage-sysstaff-desktop.png | herp-systemmanage-systemmanage-sysstaff-mobile.png |
| `/SystemManage/SystemManage/SysUser` | herp-systemmanage-systemmanage-sysuser-desktop.png | herp-systemmanage-systemmanage-sysuser-mobile.png |
| `/WarehouseManage/Warehouse/Form` | herp-warehousemanage-warehouse-form-desktop.png | herp-warehousemanage-warehouse-form-mobile.png |
| `/WarehouseManage/Warehouse/Index` | herp-warehousemanage-warehouse-index-desktop.png | herp-warehousemanage-warehouse-index-mobile.png |
| `/WarehouseManage/Warehouse/ProductUnit` | herp-warehousemanage-warehouse-productunit-desktop.png | herp-warehousemanage-warehouse-productunit-mobile.png |
| `/WarehouseManage/Warehouse/SynceProduct` | herp-warehousemanage-warehouse-synceproduct-desktop.png | herp-warehousemanage-warehouse-synceproduct-mobile.png |
| `/WarehouseManage/Warehouse/WarehouseProductQuery` | herp-warehousemanage-warehouse-warehouseproductquery-desktop.png | herp-warehousemanage-warehouse-warehouseproductquery-mobile.png |

## 2. EMR — Bệnh án điện tử (emr, cổng 5235)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/Home/Default` | emr-home-default-desktop.png | emr-home-default-mobile.png |
| `/Home/Index` | emr-home-desktop.png | emr-home-mobile.png |
| `/Login/Index` | emr-login-desktop.png | emr-login-mobile.png |
| `/MRHomePage/MRHomePage/Main` | emr-mrhomepage-mrhomepage-main-desktop.png | emr-mrhomepage-mrhomepage-main-mobile.png |
| `/MRHomePage/MRHomePage/MainDiag` | emr-mrhomepage-mrhomepage-maindiag-desktop.png | emr-mrhomepage-mrhomepage-maindiag-mobile.png |
| `/MRHomePage/MRHomePage/MainEdit` | emr-mrhomepage-mrhomepage-mainedit-desktop.png | emr-mrhomepage-mrhomepage-mainedit-mobile.png |
| `/MRHomePage/MRHomePage/MainFYInfo` | emr-mrhomepage-mrhomepage-mainfyinfo-desktop.png | emr-mrhomepage-mrhomepage-mainfyinfo-mobile.png |
| `/MRHomePage/MRHomePage/MainFeeInfo` | emr-mrhomepage-mrhomepage-mainfeeinfo-desktop.png | emr-mrhomepage-mrhomepage-mainfeeinfo-mobile.png |
| `/MRHomePage/MRHomePage/MainOpEdit` | emr-mrhomepage-mrhomepage-mainopedit-desktop.png | emr-mrhomepage-mrhomepage-mainopedit-mobile.png |
| `/MRHomePage/MRHomePage/MainOplist` | emr-mrhomepage-mrhomepage-mainoplist-desktop.png | emr-mrhomepage-mrhomepage-mainoplist-mobile.png |
| `/MRHomePage/MRHomePage/MainPatInfo` | emr-mrhomepage-mrhomepage-mainpatinfo-desktop.png | emr-mrhomepage-mrhomepage-mainpatinfo-mobile.png |
| `/MRHomePage/MRHomePage/MainZyInfo` | emr-mrhomepage-mrhomepage-mainzyinfo-desktop.png | emr-mrhomepage-mrhomepage-mainzyinfo-mobile.png |
| `/MRHomePage/Report/BasyReport` | emr-mrhomepage-report-basyreport-desktop.png | emr-mrhomepage-report-basyreport-mobile.png |
| `/MedicalRecordManage/Blctgl/CTGLList` | emr-medicalrecordmanage-blctgl-ctgllist-desktop.png | emr-medicalrecordmanage-blctgl-ctgllist-mobile.png |
| `/MedicalRecordManage/Blctgl/DiagList` | emr-medicalrecordmanage-blctgl-diaglist-desktop.png | emr-medicalrecordmanage-blctgl-diaglist-mobile.png |
| `/MedicalRecordManage/Blctgl/Form` | emr-medicalrecordmanage-blctgl-form-desktop.png | emr-medicalrecordmanage-blctgl-form-mobile.png |
| `/MedicalRecordManage/Blctgl/Index` | emr-medicalrecordmanage-blctgl-index-desktop.png | emr-medicalrecordmanage-blctgl-index-mobile.png |
| `/MedicalRecordManage/Blctgl/ShowDiagList` | emr-medicalrecordmanage-blctgl-showdiaglist-desktop.png | emr-medicalrecordmanage-blctgl-showdiaglist-mobile.png |
| `/MedicalRecordManage/Blctgl/ShowTab` | emr-medicalrecordmanage-blctgl-showtab-desktop.png | emr-medicalrecordmanage-blctgl-showtab-mobile.png |
| `/MedicalRecordManage/Blctgl/YXFHlist` | emr-medicalrecordmanage-blctgl-yxfhlist-desktop.png | emr-medicalrecordmanage-blctgl-yxfhlist-mobile.png |
| `/MedicalRecordManage/Bljgh/BlmlAdd` | emr-medicalrecordmanage-bljgh-blmladd-desktop.png | emr-medicalrecordmanage-bljgh-blmladd-mobile.png |
| `/MedicalRecordManage/Bljgh/Form` | emr-medicalrecordmanage-bljgh-form-desktop.png | emr-medicalrecordmanage-bljgh-form-mobile.png |
| `/MedicalRecordManage/Bljgh/Index` | emr-medicalrecordmanage-bljgh-index-desktop.png | emr-medicalrecordmanage-bljgh-index-mobile.png |
| `/MedicalRecordManage/Blys/Form` | emr-medicalrecordmanage-blys-form-desktop.png | emr-medicalrecordmanage-blys-form-mobile.png |
| `/MedicalRecordManage/Blys/Index` | emr-medicalrecordmanage-blys-index-desktop.png | emr-medicalrecordmanage-blys-index-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/BlTemplate` | emr-medicalrecordmanage-medrecordtemplate-bltemplate-desktop.png | emr-medicalrecordmanage-medrecordtemplate-bltemplate-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/Form` | emr-medicalrecordmanage-medrecordtemplate-form-desktop.png | emr-medicalrecordmanage-medrecordtemplate-form-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/FormQX` | emr-medicalrecordmanage-medrecordtemplate-formqx-desktop.png | emr-medicalrecordmanage-medrecordtemplate-formqx-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/Index` | emr-medicalrecordmanage-medrecordtemplate-index-desktop.png | emr-medicalrecordmanage-medrecordtemplate-index-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/LeftMedicalTemplate` | emr-medicalrecordmanage-medrecordtemplate-leftmedicaltemplate-desktop.png | emr-medicalrecordmanage-medrecordtemplate-leftmedicaltemplate-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/MedicalOpenEditor` | emr-medicalrecordmanage-medrecordtemplate-medicalopeneditor-desktop.png | emr-medicalrecordmanage-medrecordtemplate-medicalopeneditor-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/MedicalTemplate` | emr-medicalrecordmanage-medrecordtemplate-medicaltemplate-desktop.png | emr-medicalrecordmanage-medrecordtemplate-medicaltemplate-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/RightMedicalTemplate` | emr-medicalrecordmanage-medrecordtemplate-rightmedicaltemplate-desktop.png | emr-medicalrecordmanage-medrecordtemplate-rightmedicaltemplate-mobile.png |
| `/MedicalRecordManage/MedRecordTemplate/TemplateEdit` | emr-medicalrecordmanage-medrecordtemplate-templateedit-desktop.png | emr-medicalrecordmanage-medrecordtemplate-templateedit-mobile.png |
| `/MedicalRecordManage/MedicalRecord/Doctor` | emr-medicalrecordmanage-medicalrecord-doctor-desktop.png | emr-medicalrecordmanage-medicalrecord-doctor-mobile.png |
| `/MedicalRecordManage/MedicalRecord/Form` | emr-medicalrecordmanage-medicalrecord-form-desktop.png | emr-medicalrecordmanage-medicalrecord-form-mobile.png |
| `/MedicalRecordManage/MedicalRecord/Index` | emr-medicalrecordmanage-medicalrecord-index-desktop.png | emr-medicalrecordmanage-medicalrecord-index-mobile.png |
| `/MedicalRecordManage/MedicalRecord/MedicalRecordEdiForAdd` | emr-medicalrecordmanage-medicalrecord-medicalrecordediforadd-desktop.png | emr-medicalrecordmanage-medicalrecord-medicalrecordediforadd-mobile.png |
| `/MedicalRecordManage/MedicalRecord/MedicalRecordEdiForAddV2` | emr-medicalrecordmanage-medicalrecord-medicalrecordediforaddv2-desktop.png | emr-medicalrecordmanage-medicalrecord-medicalrecordediforaddv2-mobile.png |
| `/MedicalRecordManage/MedicalRecord/MedicalRecordEdit` | emr-medicalrecordmanage-medicalrecord-medicalrecordedit-desktop.png | emr-medicalrecordmanage-medicalrecord-medicalrecordedit-mobile.png |
| `/MedicalRecordManage/MedicalRecord/MedicalRecordEditV2` | emr-medicalrecordmanage-medicalrecord-medicalrecordeditv2-desktop.png | emr-medicalrecordmanage-medicalrecord-medicalrecordeditv2-mobile.png |
| `/MedicalRecordManage/MedicalRecord/MergeView` | emr-medicalrecordmanage-medicalrecord-mergeview-desktop.png | emr-medicalrecordmanage-medicalrecord-mergeview-mobile.png |
| `/MedicalRecordManage/MedicalRecord/PreView` | emr-medicalrecordmanage-medicalrecord-preview-desktop.png | emr-medicalrecordmanage-medicalrecord-preview-mobile.png |
| `/MedicalRecordManage/MedicalRecord/PreViewEdit` | emr-medicalrecordmanage-medicalrecord-previewedit-desktop.png | emr-medicalrecordmanage-medicalrecord-previewedit-mobile.png |
| `/MedicalRecordManage/MedicalRecord/PrintView` | emr-medicalrecordmanage-medicalrecord-printview-desktop.png | emr-medicalrecordmanage-medicalrecord-printview-mobile.png |
| `/MedicalRecordManage/MedicalRecord/RecordList` | emr-medicalrecordmanage-medicalrecord-recordlist-desktop.png | emr-medicalrecordmanage-medicalrecord-recordlist-mobile.png |
| `/MedicalRecordManage/MedicalRecord/YZForm` | emr-medicalrecordmanage-medicalrecord-yzform-desktop.png | emr-medicalrecordmanage-medicalrecord-yzform-mobile.png |
| `/MedicalRecordManage/MedicalRules/Form` | emr-medicalrecordmanage-medicalrules-form-desktop.png | emr-medicalrecordmanage-medicalrules-form-mobile.png |
| `/MedicalRecordManage/MedicalRules/Index` | emr-medicalrecordmanage-medicalrules-index-desktop.png | emr-medicalrecordmanage-medicalrules-index-mobile.png |
| `/MedicalRecordManage/NursingRecord/Form` | emr-medicalrecordmanage-nursingrecord-form-desktop.png | emr-medicalrecordmanage-nursingrecord-form-mobile.png |
| `/MedicalRecordManage/NursingRecord/Index` | emr-medicalrecordmanage-nursingrecord-index-desktop.png | emr-medicalrecordmanage-nursingrecord-index-mobile.png |
| `/MedicalRecordManage/NursingRecord/OpenOutForm` | emr-medicalrecordmanage-nursingrecord-openoutform-desktop.png | emr-medicalrecordmanage-nursingrecord-openoutform-mobile.png |
| `/MedicalRecordManage/NursingRecord/OpenPutForm` | emr-medicalrecordmanage-nursingrecord-openputform-desktop.png | emr-medicalrecordmanage-nursingrecord-openputform-mobile.png |
| `/MedicalRecordManage/NursingRecord/SSForm` | emr-medicalrecordmanage-nursingrecord-ssform-desktop.png | emr-medicalrecordmanage-nursingrecord-ssform-mobile.png |
| `/MedicalRecordManage/NursingRecord/WZForm` | emr-medicalrecordmanage-nursingrecord-wzform-desktop.png | emr-medicalrecordmanage-nursingrecord-wzform-mobile.png |
| `/MedicalRecordManage/Outpatient/Index` | emr-medicalrecordmanage-outpatient-index-desktop.png | emr-medicalrecordmanage-outpatient-index-mobile.png |
| `/MedicalRecordManage/PatInfo/Index` | emr-medicalrecordmanage-patinfo-index-desktop.png | emr-medicalrecordmanage-patinfo-index-mobile.png |
| `/MedicalRecordManage/PatInfo/MedRecordTreeEdit` | emr-medicalrecordmanage-patinfo-medrecordtreeedit-desktop.png | emr-medicalrecordmanage-patinfo-medrecordtreeedit-mobile.png |
| `/MedicalRecordManage/PatInfo/MedRecordTreeEditV2` | emr-medicalrecordmanage-patinfo-medrecordtreeeditv2-desktop.png | emr-medicalrecordmanage-patinfo-medrecordtreeeditv2-mobile.png |
| `/MedicalRecordManage/PatInfo/MedicalApplySend` | emr-medicalrecordmanage-patinfo-medicalapplysend-desktop.png | emr-medicalrecordmanage-patinfo-medicalapplysend-mobile.png |
| `/MedicalRecordManage/PatInfo/MrqcAdd` | emr-medicalrecordmanage-patinfo-mrqcadd-desktop.png | emr-medicalrecordmanage-patinfo-mrqcadd-mobile.png |
| `/MedicalRecordManage/PatInfo/PatHistoricalMedRecord` | emr-medicalrecordmanage-patinfo-pathistoricalmedrecord-desktop.png | emr-medicalrecordmanage-patinfo-pathistoricalmedrecord-mobile.png |
| `/MedicalRecordManage/PatInfo/PatMedRecordList` | emr-medicalrecordmanage-patinfo-patmedrecordlist-desktop.png | emr-medicalrecordmanage-patinfo-patmedrecordlist-mobile.png |
| `/MedicalRecordManage/TemplateDesign/HljlGraph` | emr-medicalrecordmanage-templatedesign-hljlgraph-desktop.png | emr-medicalrecordmanage-templatedesign-hljlgraph-mobile.png |
| `/SystemManage/Common/RecordType` | emr-systemmanage-common-recordtype-desktop.png | emr-systemmanage-common-recordtype-mobile.png |
| `/SystemManage/Common/RecordTypeChildForm` | emr-systemmanage-common-recordtypechildform-desktop.png | emr-systemmanage-common-recordtypechildform-mobile.png |
| `/SystemManage/Common/RecordTypeForm` | emr-systemmanage-common-recordtypeform-desktop.png | emr-systemmanage-common-recordtypeform-mobile.png |
| `/SystemManage/SysConfig/Form` | emr-systemmanage-sysconfig-form-desktop.png | emr-systemmanage-sysconfig-form-mobile.png |
| `/SystemManage/SysConfig/Index` | emr-systemmanage-sysconfig-index-desktop.png | emr-systemmanage-sysconfig-index-mobile.png |
| `/SystemManage/SysConfig/TemplateForm` | emr-systemmanage-sysconfig-templateform-desktop.png | emr-systemmanage-sysconfig-templateform-mobile.png |
| `/SystemManage/SysConfig/TemplateIndex` | emr-systemmanage-sysconfig-templateindex-desktop.png | emr-systemmanage-sysconfig-templateindex-mobile.png |
| `/SystemManage/SysLog/Index` | emr-systemmanage-syslog-index-desktop.png | emr-systemmanage-syslog-index-mobile.png |
| `/SystemManage/SysModule/Form` | emr-systemmanage-sysmodule-form-desktop.png | emr-systemmanage-sysmodule-form-mobile.png |
| `/SystemManage/SysModule/Index` | emr-systemmanage-sysmodule-index-desktop.png | emr-systemmanage-sysmodule-index-mobile.png |
| `/SystemManage/SysRole/Form` | emr-systemmanage-sysrole-form-desktop.png | emr-systemmanage-sysrole-form-mobile.png |
| `/SystemManage/SysRole/Index` | emr-systemmanage-sysrole-index-desktop.png | emr-systemmanage-sysrole-index-mobile.png |
| `/SystemManage/SysStaff/Selector` | emr-systemmanage-sysstaff-selector-desktop.png | emr-systemmanage-sysstaff-selector-mobile.png |
| `/SystemManage/SysUser/Index` | emr-systemmanage-sysuser-index-desktop.png | emr-systemmanage-sysuser-index-mobile.png |
| `/SystemManage/SysUser/PersonalInfo` | emr-systemmanage-sysuser-personalinfo-desktop.png | emr-systemmanage-sysuser-personalinfo-mobile.png |
| `/SystemManage/SysUser/Roles` | emr-systemmanage-sysuser-roles-desktop.png | emr-systemmanage-sysuser-roles-mobile.png |
| `/SystemManage/SysUser/Selector` | emr-systemmanage-sysuser-selector-desktop.png | emr-systemmanage-sysuser-selector-mobile.png |
| `/SystemManage/YzPreView/YzPreView` | emr-systemmanage-yzpreview-yzpreview-desktop.png | emr-systemmanage-yzpreview-yzpreview-mobile.png |

## 2. Base HOSP — Hệ thống quản trị (bệnh viện) (hosp, cổng 5233)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/AccessManage/Registor/Form` | hosp-accessmanage-registor-form-desktop.png | hosp-accessmanage-registor-form-mobile.png |
| `/AccessManage/Registor/Index` | hosp-accessmanage-registor-index-desktop.png | hosp-accessmanage-registor-index-mobile.png |
| `/AntibioticType/AntibioticTypeQuerry` | hosp-antibiotictype-antibiotictypequerry-desktop.png | hosp-antibiotictype-antibiotictypequerry-mobile.png |
| `/AntibioticType/AntibioticTypeSettingForm` | hosp-antibiotictype-antibiotictypesettingform-desktop.png | hosp-antibiotictype-antibiotictypesettingform-mobile.png |
| `/ChargeCategory/Form` | hosp-chargecategory-form-desktop.png | hosp-chargecategory-form-mobile.png |
| `/ChargeCategory/Index` | hosp-chargecategory-index-desktop.png | hosp-chargecategory-index-mobile.png |
| `/CommonLibrary/ChargeItemForm` | hosp-commonlibrary-chargeitemform-desktop.png | hosp-commonlibrary-chargeitemform-mobile.png |
| `/CommonLibrary/Index` | hosp-commonlibrary-index-desktop.png | hosp-commonlibrary-index-mobile.png |
| `/CommonLibrary/SysCommonChargeCategoryForm` | hosp-commonlibrary-syscommonchargecategoryform-desktop.png | hosp-commonlibrary-syscommonchargecategoryform-mobile.png |
| `/CommonLibrary/SysCommonMedicineAdd` | hosp-commonlibrary-syscommonmedicineadd-desktop.png | hosp-commonlibrary-syscommonmedicineadd-mobile.png |
| `/CommonLibrary/SysCommonStuffAdd` | hosp-commonlibrary-syscommonstuffadd-desktop.png | hosp-commonlibrary-syscommonstuffadd-mobile.png |
| `/CommonLibrary/SysMedicalOrderFrequencyForm` | hosp-commonlibrary-sysmedicalorderfrequencyform-desktop.png | hosp-commonlibrary-sysmedicalorderfrequencyform-mobile.png |
| `/Consult/Index` | hosp-consult-index-desktop.png | hosp-consult-index-mobile.png |
| `/Consult/NormalForm` | hosp-consult-normalform-desktop.png | hosp-consult-normalform-mobile.png |
| `/Demo/Form` | hosp-demo-form-desktop.png | hosp-demo-form-mobile.png |
| `/Demo/Index` | hosp-demo-index-desktop.png | hosp-demo-index-mobile.png |
| `/Department/Details` | hosp-department-details-desktop.png | hosp-department-details-mobile.png |
| `/Department/Form` | hosp-department-form-desktop.png | hosp-department-form-mobile.png |
| `/Department/Index` | hosp-department-index-desktop.png | hosp-department-index-mobile.png |
| `/Duty/Index` | hosp-duty-index-desktop.png | hosp-duty-index-mobile.png |
| `/Home/About` | hosp-home-about-desktop.png | hosp-home-about-mobile.png |
| `/Home/Default` | hosp-home-default-desktop.png | hosp-home-default-mobile.png |
| `/Home/Index` | hosp-home-desktop.png | hosp-home-mobile.png |
| `/Home/Notice` | hosp-home-notice-desktop.png | hosp-home-notice-mobile.png |
| `/Home/UserOrgChoose` | hosp-home-userorgchoose-desktop.png | hosp-home-userorgchoose-mobile.png |
| `/ItemsData/Form` | hosp-itemsdata-form-desktop.png | hosp-itemsdata-form-mobile.png |
| `/ItemsData/Index` | hosp-itemsdata-index-desktop.png | hosp-itemsdata-index-mobile.png |
| `/ItemsType/Index` | hosp-itemstype-index-desktop.png | hosp-itemstype-index-mobile.png |
| `/Login/Index` | hosp-login-desktop.png | hosp-login-mobile.png |
| `/MedicineSupplier/Form` | hosp-medicinesupplier-form-desktop.png | hosp-medicinesupplier-form-mobile.png |
| `/MedicineSupplier/Index` | hosp-medicinesupplier-index-desktop.png | hosp-medicinesupplier-index-mobile.png |
| `/Module/Details` | hosp-module-details-desktop.png | hosp-module-details-mobile.png |
| `/Module/Form` | hosp-module-form-desktop.png | hosp-module-form-mobile.png |
| `/Module/Index` | hosp-module-index-desktop.png | hosp-module-index-mobile.png |
| `/ModuleButton/Details` | hosp-modulebutton-details-desktop.png | hosp-modulebutton-details-mobile.png |
| `/ModuleButton/Form` | hosp-modulebutton-form-desktop.png | hosp-modulebutton-form-mobile.png |
| `/ModuleButton/Index` | hosp-modulebutton-index-desktop.png | hosp-modulebutton-index-mobile.png |
| `/ModuleButton/Organizes` | hosp-modulebutton-organizes-desktop.png | hosp-modulebutton-organizes-mobile.png |
| `/Organize/Details` | hosp-organize-details-desktop.png | hosp-organize-details-mobile.png |
| `/Organize/Form` | hosp-organize-form-desktop.png | hosp-organize-form-mobile.png |
| `/Organize/Index` | hosp-organize-index-desktop.png | hosp-organize-index-mobile.png |
| `/PharmacyDepartment/Form` | hosp-pharmacydepartment-form-desktop.png | hosp-pharmacydepartment-form-mobile.png |
| `/PharmacyDepartment/Index` | hosp-pharmacydepartment-index-desktop.png | hosp-pharmacydepartment-index-mobile.png |
| `/PharmacyDepartment/OpenMadicineForm` | hosp-pharmacydepartment-openmadicineform-desktop.png | hosp-pharmacydepartment-openmadicineform-mobile.png |
| `/PharmacyDepartment/OpenMedicineIndex` | hosp-pharmacydepartment-openmedicineindex-desktop.png | hosp-pharmacydepartment-openmedicineindex-mobile.png |
| `/PharmacyDrugStorage/SysMedicineClassification/Form` | hosp-pharmacydrugstorage-sysmedicineclassification-form-desktop.png | hosp-pharmacydrugstorage-sysmedicineclassification-form-mobile.png |
| `/PharmacyDrugStorage/SysMedicineClassification/Index` | hosp-pharmacydrugstorage-sysmedicineclassification-index-desktop.png | hosp-pharmacydrugstorage-sysmedicineclassification-index-mobile.png |
| `/PharmacyDrugStorage/SysMedicineFormulation/CorrelationUsage` | hosp-pharmacydrugstorage-sysmedicineformulation-correlationusage-desktop.png | hosp-pharmacydrugstorage-sysmedicineformulation-correlationusage-mobile.png |
| `/PharmacyDrugStorage/SysMedicineFormulation/Form` | hosp-pharmacydrugstorage-sysmedicineformulation-form-desktop.png | hosp-pharmacydrugstorage-sysmedicineformulation-form-mobile.png |
| `/PharmacyDrugStorage/SysMedicineFormulation/Index` | hosp-pharmacydrugstorage-sysmedicineformulation-index-desktop.png | hosp-pharmacydrugstorage-sysmedicineformulation-index-mobile.png |
| `/PharmacyDrugStorage/SysMedicineStorageIOMode/Form` | hosp-pharmacydrugstorage-sysmedicinestorageiomode-form-desktop.png | hosp-pharmacydrugstorage-sysmedicinestorageiomode-form-mobile.png |
| `/PharmacyDrugStorage/SysMedicineStorageIOMode/Index` | hosp-pharmacydrugstorage-sysmedicinestorageiomode-index-desktop.png | hosp-pharmacydrugstorage-sysmedicinestorageiomode-index-mobile.png |
| `/PharmacyDrugStorage/SysMedicineUnit/Form` | hosp-pharmacydrugstorage-sysmedicineunit-form-desktop.png | hosp-pharmacydrugstorage-sysmedicineunit-form-mobile.png |
| `/PharmacyDrugStorage/SysMedicineUnit/Index` | hosp-pharmacydrugstorage-sysmedicineunit-index-desktop.png | hosp-pharmacydrugstorage-sysmedicineunit-index-mobile.png |
| `/PharmacyDrugStorage/SysMedicineUsage/Form` | hosp-pharmacydrugstorage-sysmedicineusage-form-desktop.png | hosp-pharmacydrugstorage-sysmedicineusage-form-mobile.png |
| `/PharmacyDrugStorage/SysMedicineUsage/Index` | hosp-pharmacydrugstorage-sysmedicineusage-index-desktop.png | hosp-pharmacydrugstorage-sysmedicineusage-index-mobile.png |
| `/PharmacyWindow/Form` | hosp-pharmacywindow-form-desktop.png | hosp-pharmacywindow-form-mobile.png |
| `/PharmacyWindow/Index` | hosp-pharmacywindow-index-desktop.png | hosp-pharmacywindow-index-mobile.png |
| `/ProductManage/ProductCrkfs/Form` | hosp-productmanage-productcrkfs-form-desktop.png | hosp-productmanage-productcrkfs-form-mobile.png |
| `/ProductManage/ProductCrkfs/Index` | hosp-productmanage-productcrkfs-index-desktop.png | hosp-productmanage-productcrkfs-index-mobile.png |
| `/ProductManage/ProductType/Form` | hosp-productmanage-producttype-form-desktop.png | hosp-productmanage-producttype-form-mobile.png |
| `/ProductManage/ProductType/Index` | hosp-productmanage-producttype-index-desktop.png | hosp-productmanage-producttype-index-mobile.png |
| `/ProductManage/ProductUnit/Form` | hosp-productmanage-productunit-form-desktop.png | hosp-productmanage-productunit-form-mobile.png |
| `/ProductManage/ProductUnit/Index` | hosp-productmanage-productunit-index-desktop.png | hosp-productmanage-productunit-index-mobile.png |
| `/RehabChargeClassification/Form` | hosp-rehabchargeclassification-form-desktop.png | hosp-rehabchargeclassification-form-mobile.png |
| `/RehabChargeClassification/Index` | hosp-rehabchargeclassification-index-desktop.png | hosp-rehabchargeclassification-index-mobile.png |
| `/RehabChargeItem/Form` | hosp-rehabchargeitem-form-desktop.png | hosp-rehabchargeitem-form-mobile.png |
| `/RehabChargeItem/Index` | hosp-rehabchargeitem-index-desktop.png | hosp-rehabchargeitem-index-mobile.png |
| `/RehabChargeItemComparison/Form` | hosp-rehabchargeitemcomparison-form-desktop.png | hosp-rehabchargeitemcomparison-form-mobile.png |
| `/RehabChargeItemComparison/Index` | hosp-rehabchargeitemcomparison-index-desktop.png | hosp-rehabchargeitemcomparison-index-mobile.png |
| `/ReportManagement/Form` | hosp-reportmanagement-form-desktop.png | hosp-reportmanagement-form-mobile.png |
| `/ReportManagement/Index` | hosp-reportmanagement-index-desktop.png | hosp-reportmanagement-index-mobile.png |
| `/ReportManagement/MianForm` | hosp-reportmanagement-mianform-desktop.png | hosp-reportmanagement-mianform-mobile.png |
| `/Role/Details` | hosp-role-details-desktop.png | hosp-role-details-mobile.png |
| `/Role/Form` | hosp-role-form-desktop.png | hosp-role-form-mobile.png |
| `/Role/Index` | hosp-role-index-desktop.png | hosp-role-index-mobile.png |
| `/Settlement/GzybBaseInfo/Index` | hosp-settlement-gzybbaseinfo-index-desktop.png | hosp-settlement-gzybbaseinfo-index-mobile.png |
| `/Settlement/GzybBaseInfo/MedicalInsuranceCatalogue` | hosp-settlement-gzybbaseinfo-medicalinsurancecatalogue-desktop.png | hosp-settlement-gzybbaseinfo-medicalinsurancecatalogue-mobile.png |
| `/Settlement/SysDiagnosis/Form` | hosp-settlement-sysdiagnosis-form-desktop.png | hosp-settlement-sysdiagnosis-form-mobile.png |
| `/Settlement/SysDiagnosis/Index` | hosp-settlement-sysdiagnosis-index-desktop.png | hosp-settlement-sysdiagnosis-index-mobile.png |
| `/Settlement/SysTCMSyndrome/Form` | hosp-settlement-systcmsyndrome-form-desktop.png | hosp-settlement-systcmsyndrome-form-mobile.png |
| `/Settlement/SysTCMSyndrome/Index` | hosp-settlement-systcmsyndrome-index-desktop.png | hosp-settlement-systcmsyndrome-index-mobile.png |
| `/Staff/Details` | hosp-staff-details-desktop.png | hosp-staff-details-mobile.png |
| `/Staff/Duties` | hosp-staff-duties-desktop.png | hosp-staff-duties-mobile.png |
| `/Staff/ExcelExportChooseColumns` | hosp-staff-excelexportchoosecolumns-desktop.png | hosp-staff-excelexportchoosecolumns-mobile.png |
| `/Staff/Form` | hosp-staff-form-desktop.png | hosp-staff-form-mobile.png |
| `/Staff/Index` | hosp-staff-index-desktop.png | hosp-staff-index-mobile.png |
| `/Staff/Selector` | hosp-staff-selector-desktop.png | hosp-staff-selector-mobile.png |
| `/Sys/Application/Form` | hosp-sys-application-form-desktop.png | hosp-sys-application-form-mobile.png |
| `/Sys/Application/Index` | hosp-sys-application-index-desktop.png | hosp-sys-application-index-mobile.png |
| `/Sys/Application/Organizes` | hosp-sys-application-organizes-desktop.png | hosp-sys-application-organizes-mobile.png |
| `/Sys/Combination/CatalogComparison` | hosp-sys-combination-catalogcomparison-desktop.png | hosp-sys-combination-catalogcomparison-mobile.png |
| `/Sys/Combination/Index` | hosp-sys-combination-index-desktop.png | hosp-sys-combination-index-mobile.png |
| `/Sys/Combination/materialIndex` | hosp-sys-combination-materialindex-desktop.png | hosp-sys-combination-materialindex-mobile.png |
| `/Sys/Combination/projectIndex` | hosp-sys-combination-projectindex-desktop.png | hosp-sys-combination-projectindex-mobile.png |
| `/Sys/Duty/Form` | hosp-sys-duty-form-desktop.png | hosp-sys-duty-form-mobile.png |
| `/Sys/Duty/Index` | hosp-sys-duty-index-desktop.png | hosp-sys-duty-index-mobile.png |
| `/Sys/ItemsData/Form` | hosp-sys-itemsdata-form-desktop.png | hosp-sys-itemsdata-form-mobile.png |
| `/Sys/ItemsData/Index` | hosp-sys-itemsdata-index-desktop.png | hosp-sys-itemsdata-index-mobile.png |
| `/Sys/ItemsType/Form` | hosp-sys-itemstype-form-desktop.png | hosp-sys-itemstype-form-mobile.png |
| `/Sys/ItemsType/Index` | hosp-sys-itemstype-index-desktop.png | hosp-sys-itemstype-index-mobile.png |
| `/Sys/Organize/Details` | hosp-sys-organize-details-desktop.png | hosp-sys-organize-details-mobile.png |
| `/Sys/Organize/Form` | hosp-sys-organize-form-desktop.png | hosp-sys-organize-form-mobile.png |
| `/Sys/Organize/Index` | hosp-sys-organize-index-desktop.png | hosp-sys-organize-index-mobile.png |
| `/Sys/SysGlobalConfig/Form` | hosp-sys-sysglobalconfig-form-desktop.png | hosp-sys-sysglobalconfig-form-mobile.png |
| `/Sys/SysGlobalConfig/Index` | hosp-sys-sysglobalconfig-index-desktop.png | hosp-sys-sysglobalconfig-index-mobile.png |
| `/Sys/SysNation/Form` | hosp-sys-sysnation-form-desktop.png | hosp-sys-sysnation-form-mobile.png |
| `/Sys/SysNation/Index` | hosp-sys-sysnation-index-desktop.png | hosp-sys-sysnation-index-mobile.png |
| `/Sys/SysNationality/Form` | hosp-sys-sysnationality-form-desktop.png | hosp-sys-sysnationality-form-mobile.png |
| `/Sys/SysNationality/Index` | hosp-sys-sysnationality-index-desktop.png | hosp-sys-sysnationality-index-mobile.png |
| `/SysAgriInsuranceChargeCategory/Form` | hosp-sysagriinsurancechargecategory-form-desktop.png | hosp-sysagriinsurancechargecategory-form-mobile.png |
| `/SysAgriInsuranceChargeCategory/Index` | hosp-sysagriinsurancechargecategory-index-desktop.png | hosp-sysagriinsurancechargecategory-index-mobile.png |
| `/SysChargeCategoryTypeRelation/Form` | hosp-syschargecategorytyperelation-form-desktop.png | hosp-syschargecategorytyperelation-form-mobile.png |
| `/SysChargeCategoryTypeRelation/Index` | hosp-syschargecategorytyperelation-index-desktop.png | hosp-syschargecategorytyperelation-index-mobile.png |
| `/SysChargeClassification/Form` | hosp-syschargeclassification-form-desktop.png | hosp-syschargeclassification-form-mobile.png |
| `/SysChargeClassification/Index` | hosp-syschargeclassification-index-desktop.png | hosp-syschargeclassification-index-mobile.png |
| `/SysChargeItem/Form` | hosp-syschargeitem-form-desktop.png | hosp-syschargeitem-form-mobile.png |
| `/SysChargeItem/Index` | hosp-syschargeitem-index-desktop.png | hosp-syschargeitem-index-mobile.png |
| `/SysChargeItem/ybbxblForm` | hosp-syschargeitem-ybbxblform-desktop.png | hosp-syschargeitem-ybbxblform-mobile.png |
| `/SysDiagnosis/Form` | hosp-sysdiagnosis-form-desktop.png | hosp-sysdiagnosis-form-mobile.png |
| `/SysDiagnosis/Index` | hosp-sysdiagnosis-index-desktop.png | hosp-sysdiagnosis-index-mobile.png |
| `/SysDiagnosis/Surgery` | hosp-sysdiagnosis-surgery-desktop.png | hosp-sysdiagnosis-surgery-mobile.png |
| `/SysDiagnosis/SurgeryFrom` | hosp-sysdiagnosis-surgeryfrom-desktop.png | hosp-sysdiagnosis-surgeryfrom-mobile.png |
| `/SysMedicalOrderFrequency/Form` | hosp-sysmedicalorderfrequency-form-desktop.png | hosp-sysmedicalorderfrequency-form-mobile.png |
| `/SysMedicalOrderFrequency/Index` | hosp-sysmedicalorderfrequency-index-desktop.png | hosp-sysmedicalorderfrequency-index-mobile.png |
| `/SysMedicalRecordChargeCategory/Form` | hosp-sysmedicalrecordchargecategory-form-desktop.png | hosp-sysmedicalrecordchargecategory-form-mobile.png |
| `/SysMedicalRecordChargeCategory/Index` | hosp-sysmedicalrecordchargecategory-index-desktop.png | hosp-sysmedicalrecordchargecategory-index-mobile.png |
| `/SysMedicine/Index` | hosp-sysmedicine-index-desktop.png | hosp-sysmedicine-index-mobile.png |
| `/SysMedicine/SysMedicineAdd` | hosp-sysmedicine-sysmedicineadd-desktop.png | hosp-sysmedicine-sysmedicineadd-mobile.png |
| `/SysMedicineAuthority/Form` | hosp-sysmedicineauthority-form-desktop.png | hosp-sysmedicineauthority-form-mobile.png |
| `/SysMedicineAuthority/Index` | hosp-sysmedicineauthority-index-desktop.png | hosp-sysmedicineauthority-index-mobile.png |
| `/SysMedicineAuthorityRelation/Form` | hosp-sysmedicineauthorityrelation-form-desktop.png | hosp-sysmedicineauthorityrelation-form-mobile.png |
| `/SysMedicineAuthorityRelation/Index` | hosp-sysmedicineauthorityrelation-index-desktop.png | hosp-sysmedicineauthorityrelation-index-mobile.png |
| `/SysMedicineClassification/Index` | hosp-sysmedicineclassification-index-desktop.png | hosp-sysmedicineclassification-index-mobile.png |
| `/SysMedicineElectronicPrescription/Index` | hosp-sysmedicineelectronicprescription-index-desktop.png | hosp-sysmedicineelectronicprescription-index-mobile.png |
| `/SysMedicineFormulation/Index` | hosp-sysmedicineformulation-index-desktop.png | hosp-sysmedicineformulation-index-mobile.png |
| `/SysMedicineUnit/Index` | hosp-sysmedicineunit-index-desktop.png | hosp-sysmedicineunit-index-mobile.png |
| `/SysMedicineUsage/Index` | hosp-sysmedicineusage-index-desktop.png | hosp-sysmedicineusage-index-mobile.png |
| `/SysNation/Index` | hosp-sysnation-index-desktop.png | hosp-sysnation-index-mobile.png |
| `/SysNationality/Index` | hosp-sysnationality-index-desktop.png | hosp-sysnationality-index-mobile.png |
| `/SysTCMSyndrome/Form` | hosp-systcmsyndrome-form-desktop.png | hosp-systcmsyndrome-form-mobile.png |
| `/SysTCMSyndrome/Index` | hosp-systcmsyndrome-index-desktop.png | hosp-systcmsyndrome-index-mobile.png |
| `/SysWardBed/Form` | hosp-syswardbed-form-desktop.png | hosp-syswardbed-form-mobile.png |
| `/SysWardBed/Index` | hosp-syswardbed-index-desktop.png | hosp-syswardbed-index-mobile.png |
| `/SysWardRoom/Form` | hosp-syswardroom-form-desktop.png | hosp-syswardroom-form-mobile.png |
| `/SysWardRoom/Index` | hosp-syswardroom-index-desktop.png | hosp-syswardroom-index-mobile.png |
| `/User/CorrelationPharmacy` | hosp-user-correlationpharmacy-desktop.png | hosp-user-correlationpharmacy-mobile.png |
| `/User/Details` | hosp-user-details-desktop.png | hosp-user-details-mobile.png |
| `/User/Form` | hosp-user-form-desktop.png | hosp-user-form-mobile.png |
| `/User/Index` | hosp-user-index-desktop.png | hosp-user-index-mobile.png |
| `/User/PersonalInfo` | hosp-user-personalinfo-desktop.png | hosp-user-personalinfo-mobile.png |
| `/User/RevisePassword` | hosp-user-revisepassword-desktop.png | hosp-user-revisepassword-mobile.png |
| `/User/Selector` | hosp-user-selector-desktop.png | hosp-user-selector-mobile.png |
| `/Ward/Form` | hosp-ward-form-desktop.png | hosp-ward-form-mobile.png |
| `/Ward/Index` | hosp-ward-index-desktop.png | hosp-ward-index-mobile.png |

## 2. Base Web — Dữ liệu nền (baseweb, cổng 5238)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/Application/Form` | baseweb-application-form-desktop.png | baseweb-application-form-mobile.png |
| `/Application/Index` | baseweb-application-index-desktop.png | baseweb-application-index-mobile.png |
| `/Application/Organizes` | baseweb-application-organizes-desktop.png | baseweb-application-organizes-mobile.png |
| `/Duty/Form` | baseweb-duty-form-desktop.png | baseweb-duty-form-mobile.png |
| `/Duty/Index` | baseweb-duty-index-desktop.png | baseweb-duty-index-mobile.png |
| `/Home/About` | baseweb-home-about-desktop.png | baseweb-home-about-mobile.png |
| `/Home/Default` | baseweb-home-default-desktop.png | baseweb-home-default-mobile.png |
| `/Home/Index` | baseweb-home-desktop.png | baseweb-home-mobile.png |
| `/ItemsData/Form` | baseweb-itemsdata-form-desktop.png | baseweb-itemsdata-form-mobile.png |
| `/ItemsData/Index` | baseweb-itemsdata-index-desktop.png | baseweb-itemsdata-index-mobile.png |
| `/ItemsType/Form` | baseweb-itemstype-form-desktop.png | baseweb-itemstype-form-mobile.png |
| `/ItemsType/Index` | baseweb-itemstype-index-desktop.png | baseweb-itemstype-index-mobile.png |
| `/Login/Index` | baseweb-login-desktop.png | baseweb-login-mobile.png |
| `/Organize/Details` | baseweb-organize-details-desktop.png | baseweb-organize-details-mobile.png |
| `/Organize/Form` | baseweb-organize-form-desktop.png | baseweb-organize-form-mobile.png |
| `/Organize/Index` | baseweb-organize-index-desktop.png | baseweb-organize-index-mobile.png |
| `/RehabChargeClassification/Form` | baseweb-rehabchargeclassification-form-desktop.png | baseweb-rehabchargeclassification-form-mobile.png |
| `/RehabChargeClassification/Index` | baseweb-rehabchargeclassification-index-desktop.png | baseweb-rehabchargeclassification-index-mobile.png |
| `/RehabChargeItem/Form` | baseweb-rehabchargeitem-form-desktop.png | baseweb-rehabchargeitem-form-mobile.png |
| `/RehabChargeItem/Index` | baseweb-rehabchargeitem-index-desktop.png | baseweb-rehabchargeitem-index-mobile.png |
| `/SysDiagnosis/Form` | baseweb-sysdiagnosis-form-desktop.png | baseweb-sysdiagnosis-form-mobile.png |
| `/SysDiagnosis/Index` | baseweb-sysdiagnosis-index-desktop.png | baseweb-sysdiagnosis-index-mobile.png |
| `/SysGlobalConfig/Form` | baseweb-sysglobalconfig-form-desktop.png | baseweb-sysglobalconfig-form-mobile.png |
| `/SysGlobalConfig/Index` | baseweb-sysglobalconfig-index-desktop.png | baseweb-sysglobalconfig-index-mobile.png |
| `/SysMedicineClassification/Form` | baseweb-sysmedicineclassification-form-desktop.png | baseweb-sysmedicineclassification-form-mobile.png |
| `/SysMedicineClassification/Index` | baseweb-sysmedicineclassification-index-desktop.png | baseweb-sysmedicineclassification-index-mobile.png |
| `/SysMedicineFormulation/CorrelationUsage` | baseweb-sysmedicineformulation-correlationusage-desktop.png | baseweb-sysmedicineformulation-correlationusage-mobile.png |
| `/SysMedicineFormulation/Form` | baseweb-sysmedicineformulation-form-desktop.png | baseweb-sysmedicineformulation-form-mobile.png |
| `/SysMedicineFormulation/Index` | baseweb-sysmedicineformulation-index-desktop.png | baseweb-sysmedicineformulation-index-mobile.png |
| `/SysMedicineStorageIOMode/Form` | baseweb-sysmedicinestorageiomode-form-desktop.png | baseweb-sysmedicinestorageiomode-form-mobile.png |
| `/SysMedicineStorageIOMode/Index` | baseweb-sysmedicinestorageiomode-index-desktop.png | baseweb-sysmedicinestorageiomode-index-mobile.png |
| `/SysMedicineUnit/Form` | baseweb-sysmedicineunit-form-desktop.png | baseweb-sysmedicineunit-form-mobile.png |
| `/SysMedicineUnit/Index` | baseweb-sysmedicineunit-index-desktop.png | baseweb-sysmedicineunit-index-mobile.png |
| `/SysMedicineUsage/Form` | baseweb-sysmedicineusage-form-desktop.png | baseweb-sysmedicineusage-form-mobile.png |
| `/SysMedicineUsage/Index` | baseweb-sysmedicineusage-index-desktop.png | baseweb-sysmedicineusage-index-mobile.png |
| `/SysNation/Form` | baseweb-sysnation-form-desktop.png | baseweb-sysnation-form-mobile.png |
| `/SysNation/Index` | baseweb-sysnation-index-desktop.png | baseweb-sysnation-index-mobile.png |
| `/SysNationality/Form` | baseweb-sysnationality-form-desktop.png | baseweb-sysnationality-form-mobile.png |
| `/SysNationality/Index` | baseweb-sysnationality-index-desktop.png | baseweb-sysnationality-index-mobile.png |
| `/SysTCMSyndrome/Form` | baseweb-systcmsyndrome-form-desktop.png | baseweb-systcmsyndrome-form-mobile.png |
| `/SysTCMSyndrome/Index` | baseweb-systcmsyndrome-index-desktop.png | baseweb-systcmsyndrome-index-mobile.png |

## 2. CIS — Hệ thống thông tin lâm sàng (cis, cổng 5234)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/Apply/Examination` | cis-apply-examination-desktop.png | cis-apply-examination-mobile.png |
| `/Apply/Inspection` | cis-apply-inspection-desktop.png | cis-apply-inspection-mobile.png |
| `/BespeakRegister/BespeakRegister` | cis-bespeakregister-bespeakregister-desktop.png | cis-bespeakregister-bespeakregister-mobile.png |
| `/ClinicManage/ClinicApply/Index` | cis-clinicmanage-clinicapply-index-desktop.png | cis-clinicmanage-clinicapply-index-mobile.png |
| `/DoctorManage/Apply/Examination` | cis-doctormanage-apply-examination-desktop.png | cis-doctormanage-apply-examination-mobile.png |
| `/DoctorManage/Apply/Inspection` | cis-doctormanage-apply-inspection-desktop.png | cis-doctormanage-apply-inspection-mobile.png |
| `/DoctorManage/DietaryAdvice/Index` | cis-doctormanage-dietaryadvice-index-desktop.png | cis-doctormanage-dietaryadvice-index-mobile.png |
| `/DoctorManage/DoctorsAdvice/AdviceLeaveHospitalStop` | cis-doctormanage-doctorsadvice-adviceleavehospitalstop-desktop.png | cis-doctormanage-doctorsadvice-adviceleavehospitalstop-mobile.png |
| `/DoctorManage/DoctorsAdvice/AdviceList` | cis-doctormanage-doctorsadvice-advicelist-desktop.png | cis-doctormanage-doctorsadvice-advicelist-mobile.png |
| `/DoctorManage/DoctorsAdvice/AdviceListLook` | cis-doctormanage-doctorsadvice-advicelistlook-desktop.png | cis-doctormanage-doctorsadvice-advicelistlook-mobile.png |
| `/DoctorManage/DoctorsAdvice/AdviceStop` | cis-doctormanage-doctorsadvice-advicestop-desktop.png | cis-doctormanage-doctorsadvice-advicestop-mobile.png |
| `/DoctorManage/DoctorsAdvice/ContinuePrint` | cis-doctormanage-doctorsadvice-continueprint-desktop.png | cis-doctormanage-doctorsadvice-continueprint-mobile.png |
| `/DoctorManage/DoctorsAdvice/Form` | cis-doctormanage-doctorsadvice-form-desktop.png | cis-doctormanage-doctorsadvice-form-mobile.png |
| `/DoctorManage/DoctorsAdvice/HistoricalOrders` | cis-doctormanage-doctorsadvice-historicalorders-desktop.png | cis-doctormanage-doctorsadvice-historicalorders-mobile.png |
| `/DoctorManage/DoctorsAdvice/Index` | cis-doctormanage-doctorsadvice-index-desktop.png | cis-doctormanage-doctorsadvice-index-mobile.png |
| `/DoctorManage/DoctorsAdvice/PatientHistoricalOrders` | cis-doctormanage-doctorsadvice-patienthistoricalorders-desktop.png | cis-doctormanage-doctorsadvice-patienthistoricalorders-mobile.png |
| `/DoctorManage/DoctorsAdvice/SettInfoShow` | cis-doctormanage-doctorsadvice-settinfoshow-desktop.png | cis-doctormanage-doctorsadvice-settinfoshow-mobile.png |
| `/DoctorManage/DoctorsAdvice/TransferWardsTOP` | cis-doctormanage-doctorsadvice-transferwardstop-desktop.png | cis-doctormanage-doctorsadvice-transferwardstop-mobile.png |
| `/DoctorManage/DoctorsAdvice/ZyyzQueryAdviceList` | cis-doctormanage-doctorsadvice-zyyzqueryadvicelist-desktop.png | cis-doctormanage-doctorsadvice-zyyzqueryadvicelist-mobile.png |
| `/DoctorManage/ElectronicPrescription/Index` | cis-doctormanage-electronicprescription-index-desktop.png | cis-doctormanage-electronicprescription-index-mobile.png |
| `/DoctorManage/ElectronicPrescription/backForm` | cis-doctormanage-electronicprescription-backform-desktop.png | cis-doctormanage-electronicprescription-backform-mobile.png |
| `/DoctorManage/ElectronicPrescription/cfForm` | cis-doctormanage-electronicprescription-cfform-desktop.png | cis-doctormanage-electronicprescription-cfform-mobile.png |
| `/DoctorManage/ElectronicPrescription/examForm` | cis-doctormanage-electronicprescription-examform-desktop.png | cis-doctormanage-electronicprescription-examform-mobile.png |
| `/DoctorManage/ElectronicPrescription/medicineForm` | cis-doctormanage-electronicprescription-medicineform-desktop.png | cis-doctormanage-electronicprescription-medicineform-mobile.png |
| `/DoctorManage/Medicine/Index` | cis-doctormanage-medicine-index-desktop.png | cis-doctormanage-medicine-index-mobile.png |
| `/DoctorManage/Medicine/OrderDiagnosisForm` | cis-doctormanage-medicine-orderdiagnosisform-desktop.png | cis-doctormanage-medicine-orderdiagnosisform-mobile.png |
| `/DoctorManage/Medicine/TCMMedicine` | cis-doctormanage-medicine-tcmmedicine-desktop.png | cis-doctormanage-medicine-tcmmedicine-mobile.png |
| `/DoctorManage/Operation/ArrangeForm` | cis-doctormanage-operation-arrangeform-desktop.png | cis-doctormanage-operation-arrangeform-mobile.png |
| `/DoctorManage/Operation/ArrangeQuery` | cis-doctormanage-operation-arrangequery-desktop.png | cis-doctormanage-operation-arrangequery-mobile.png |
| `/DoctorManage/Operation/OperationSupplement` | cis-doctormanage-operation-operationsupplement-desktop.png | cis-doctormanage-operation-operationsupplement-mobile.png |
| `/DoctorManage/Operation/originalCharge` | cis-doctormanage-operation-originalcharge-desktop.png | cis-doctormanage-operation-originalcharge-mobile.png |
| `/DoctorManage/Orginal/ConsumablesIndex` | cis-doctormanage-orginal-consumablesindex-desktop.png | cis-doctormanage-orginal-consumablesindex-mobile.png |
| `/DoctorManage/Orginal/CurrentdayView` | cis-doctormanage-orginal-currentdayview-desktop.png | cis-doctormanage-orginal-currentdayview-mobile.png |
| `/DoctorManage/Orginal/Index` | cis-doctormanage-orginal-index-desktop.png | cis-doctormanage-orginal-index-mobile.png |
| `/DoctorManage/OutpatientQuery/ConsultDetail` | cis-doctormanage-outpatientquery-consultdetail-desktop.png | — |
| `/DoctorManage/OutpatientQuery/ConsultRecord` | cis-doctormanage-outpatientquery-consultrecord-desktop.png | — |
| `/DoctorManage/OutpatientQuery/MZcasehistory` | cis-doctormanage-outpatientquery-mzcasehistory-desktop.png | — |
| `/DoctorManage/OutpatientQuery/QueryOutpatientDetail` | cis-doctormanage-outpatientquery-queryoutpatientdetail-desktop.png | — |
| `/DoctorManage/OutpatientQuery/QueryOutpatientReservation` | cis-doctormanage-outpatientquery-queryoutpatientreservation-desktop.png | — |
| `/DoctorManage/Rehab/Index` | cis-doctormanage-rehab-index-desktop.png | — |
| `/DoctorManage/Takemedicine/Index` | cis-doctormanage-takemedicine-index-desktop.png | — |
| `/DoctorManage/Words/Index` | cis-doctormanage-words-index-desktop.png | — |
| `/Home/Index` | cis-home-desktop.png | cis-home-mobile.png |
| `/InfusionManage/query` | cis-infusionmanage-query-desktop.png | — |
| `/Login/Index` | cis-login-desktop.png | cis-login-mobile.png |
| `/MainBusiness/Index` | cis-mainbusiness-index-desktop.png | — |
| `/MainBusiness/TcmHis` | cis-mainbusiness-tcmhis-desktop.png | — |
| `/MedicalRecord/AdmissionNoticeForm` | cis-medicalrecord-admissionnoticeform-desktop.png | — |
| `/MedicalRecord/BLContinuePrint` | cis-medicalrecord-blcontinueprint-desktop.png | cis-medicalrecord-blcontinueprint-mobile.png |
| `/MedicalRecord/BaseMedicineQuery` | cis-medicalrecord-basemedicinequery-desktop.png | cis-medicalrecord-basemedicinequery-mobile.png |
| `/MedicalRecord/CommonDiag` | cis-medicalrecord-commondiag-desktop.png | cis-medicalrecord-commondiag-mobile.png |
| `/MedicalRecord/Form` | cis-medicalrecord-form-desktop.png | cis-medicalrecord-form-mobile.png |
| `/MedicalRecord/GetlisPeportForm` | cis-medicalrecord-getlispeportform-desktop.png | cis-medicalrecord-getlispeportform-mobile.png |
| `/MedicalRecord/HistoryMedicalRecordForm` | cis-medicalrecord-historymedicalrecordform-desktop.png | — |
| `/MedicalRecord/Index` | cis-medicalrecord-index-desktop.png | cis-medicalrecord-index-mobile.png |
| `/MedicalRecord/LisSqdhQueryForm` | cis-medicalrecord-lissqdhqueryform-desktop.png | cis-medicalrecord-lissqdhqueryform-mobile.png |
| `/MedicalRecord/LogQuery` | cis-medicalrecord-logquery-desktop.png | cis-medicalrecord-logquery-mobile.png |
| `/MedicalRecord/MedicalRecord` | cis-medicalrecord-medicalrecord-desktop.png | cis-medicalrecord-medicalrecord-mobile.png |
| `/MedicalRecord/OutpatientTelemedicine` | cis-medicalrecord-outpatienttelemedicine-desktop.png | — |
| `/MedicalRecord/PacsSqdhQueryForm` | cis-medicalrecord-pacssqdhqueryform-desktop.png | cis-medicalrecord-pacssqdhqueryform-mobile.png |
| `/MedicalRecord/ReservationForm` | cis-medicalrecord-reservationform-desktop.png | cis-medicalrecord-reservationform-mobile.png |
| `/MedicalRecord/TelemedicineRequest` | cis-medicalrecord-telemedicinerequest-desktop.png | cis-medicalrecord-telemedicinerequest-mobile.png |
| `/MedicalRecord/TelxdReport` | cis-medicalrecord-telxdreport-desktop.png | cis-medicalrecord-telxdreport-mobile.png |
| `/MedicalRecord/ToothBitmap` | cis-medicalrecord-toothbitmap-desktop.png | cis-medicalrecord-toothbitmap-mobile.png |
| `/MedicalRecord/UpdateMedicalRecord` | cis-medicalrecord-updatemedicalrecord-desktop.png | cis-medicalrecord-updatemedicalrecord-mobile.png |
| `/MedicalRecord/blctForm` | cis-medicalrecord-blctform-desktop.png | cis-medicalrecord-blctform-mobile.png |
| `/MedicalRecord/dzcfcxForm` | cis-medicalrecord-dzcfcxform-desktop.png | cis-medicalrecord-dzcfcxform-mobile.png |
| `/NurseManage/Allergy/AllergyForm` | cis-nursemanage-allergy-allergyform-desktop.png | cis-nursemanage-allergy-allergyform-mobile.png |
| `/NurseManage/Allergy/AllergyInfoQuery` | cis-nursemanage-allergy-allergyinfoquery-desktop.png | cis-nursemanage-allergy-allergyinfoquery-mobile.png |
| `/NurseManage/Allergy/InHosAllergyManage` | cis-nursemanage-allergy-inhosallergymanage-desktop.png | cis-nursemanage-allergy-inhosallergymanage-mobile.png |
| `/NurseManage/Allergy/OutHosAllergyManage` | cis-nursemanage-allergy-outhosallergymanage-desktop.png | cis-nursemanage-allergy-outhosallergymanage-mobile.png |
| `/NurseManage/BedLevelCharge/Form` | cis-nursemanage-bedlevelcharge-form-desktop.png | cis-nursemanage-bedlevelcharge-form-mobile.png |
| `/NurseManage/BedLevelCharge/Index` | cis-nursemanage-bedlevelcharge-index-desktop.png | cis-nursemanage-bedlevelcharge-index-mobile.png |
| `/NurseManage/DepartmentSupplies/ExpiredStorageQuery` | cis-nursemanage-departmentsupplies-expiredstoragequery-desktop.png | cis-nursemanage-departmentsupplies-expiredstoragequery-mobile.png |
| `/NurseManage/DepartmentSupplies/StorageQuery` | cis-nursemanage-departmentsupplies-storagequery-desktop.png | cis-nursemanage-departmentsupplies-storagequery-mobile.png |
| `/NurseManage/DepartmentSupplies/WzSldIndex` | cis-nursemanage-departmentsupplies-wzsldindex-desktop.png | cis-nursemanage-departmentsupplies-wzsldindex-mobile.png |
| `/NurseManage/DietaryAdvice/DietBase` | cis-nursemanage-dietaryadvice-dietbase-desktop.png | cis-nursemanage-dietaryadvice-dietbase-mobile.png |
| `/NurseManage/DietaryAdvice/Form` | cis-nursemanage-dietaryadvice-form-desktop.png | cis-nursemanage-dietaryadvice-form-mobile.png |
| `/NurseManage/EntryRegistration/ChangeAreaForm` | cis-nursemanage-entryregistration-changeareaform-desktop.png | cis-nursemanage-entryregistration-changeareaform-mobile.png |
| `/NurseManage/EntryRegistration/EntryAreaFrom` | cis-nursemanage-entryregistration-entryareafrom-desktop.png | cis-nursemanage-entryregistration-entryareafrom-mobile.png |
| `/NurseManage/EntryRegistration/Index` | cis-nursemanage-entryregistration-index-desktop.png | cis-nursemanage-entryregistration-index-mobile.png |
| `/NurseManage/EntryRegistration/RecallOutArea` | cis-nursemanage-entryregistration-recalloutarea-desktop.png | cis-nursemanage-entryregistration-recalloutarea-mobile.png |
| `/NurseManage/ExceReportPrint/Index` | cis-nursemanage-excereportprint-index-desktop.png | cis-nursemanage-excereportprint-index-mobile.png |
| `/NurseManage/ExceReportPrint/PrintedContinue` | cis-nursemanage-excereportprint-printedcontinue-desktop.png | cis-nursemanage-excereportprint-printedcontinue-mobile.png |
| `/NurseManage/ExceReportPrint/QueryPrintIndex` | cis-nursemanage-excereportprint-queryprintindex-desktop.png | cis-nursemanage-excereportprint-queryprintindex-mobile.png |
| `/NurseManage/Infusion/DrugReactManage` | cis-nursemanage-infusion-drugreactmanage-desktop.png | cis-nursemanage-infusion-drugreactmanage-mobile.png |
| `/NurseManage/Infusion/InfusionHistory` | cis-nursemanage-infusion-infusionhistory-desktop.png | cis-nursemanage-infusion-infusionhistory-mobile.png |
| `/NurseManage/Infusion/Injection` | cis-nursemanage-infusion-injection-desktop.png | cis-nursemanage-infusion-injection-mobile.png |
| `/NurseManage/Infusion/InjectionForm` | cis-nursemanage-infusion-injectionform-desktop.png | cis-nursemanage-infusion-injectionform-mobile.png |
| `/NurseManage/Infusion/PrintBottleLabel` | cis-nursemanage-infusion-printbottlelabel-desktop.png | cis-nursemanage-infusion-printbottlelabel-mobile.png |
| `/NurseManage/Infusion/observationFrom` | cis-nursemanage-infusion-observationfrom-desktop.png | cis-nursemanage-infusion-observationfrom-mobile.png |
| `/NurseManage/InpatientList/InHosBookkeep` | cis-nursemanage-inpatientlist-inhosbookkeep-desktop.png | cis-nursemanage-inpatientlist-inhosbookkeep-mobile.png |
| `/NurseManage/InpatientList/InHosFee` | cis-nursemanage-inpatientlist-inhosfee-desktop.png | cis-nursemanage-inpatientlist-inhosfee-mobile.png |
| `/NurseManage/InpatientList/InHosFeeRefund` | cis-nursemanage-inpatientlist-inhosfeerefund-desktop.png | cis-nursemanage-inpatientlist-inhosfeerefund-mobile.png |
| `/NurseManage/InpatientList/InHosMedReturn` | cis-nursemanage-inpatientlist-inhosmedreturn-desktop.png | cis-nursemanage-inpatientlist-inhosmedreturn-mobile.png |
| `/NurseManage/InpatientList/InHosNursingInput` | cis-nursemanage-inpatientlist-inhosnursinginput-desktop.png | cis-nursemanage-inpatientlist-inhosnursinginput-mobile.png |
| `/NurseManage/InpatientList/InHosPrint` | cis-nursemanage-inpatientlist-inhosprint-desktop.png | cis-nursemanage-inpatientlist-inhosprint-mobile.png |
| `/NurseManage/InpatientList/InHosQueryIndex` | cis-nursemanage-inpatientlist-inhosqueryindex-desktop.png | cis-nursemanage-inpatientlist-inhosqueryindex-mobile.png |
| `/NurseManage/InpatientList/InHosRecallOutArea` | cis-nursemanage-inpatientlist-inhosrecalloutarea-desktop.png | cis-nursemanage-inpatientlist-inhosrecalloutarea-mobile.png |
| `/NurseManage/InpatientList/InHosRegistration` | cis-nursemanage-inpatientlist-inhosregistration-desktop.png | cis-nursemanage-inpatientlist-inhosregistration-mobile.png |
| `/NurseManage/InpatientList/Index` | cis-nursemanage-inpatientlist-index-desktop.png | cis-nursemanage-inpatientlist-index-mobile.png |
| `/NurseManage/InpatientList/IndexCenter` | cis-nursemanage-inpatientlist-indexcenter-desktop.png | cis-nursemanage-inpatientlist-indexcenter-mobile.png |
| `/NurseManage/InpatientList/Inpatient` | cis-nursemanage-inpatientlist-inpatient-desktop.png | cis-nursemanage-inpatientlist-inpatient-mobile.png |
| `/NurseManage/InpatientList/MedicalorderQuery` | cis-nursemanage-inpatientlist-medicalorderquery-desktop.png | cis-nursemanage-inpatientlist-medicalorderquery-mobile.png |
| `/NurseManage/InpatientList/OrderAudit` | cis-nursemanage-inpatientlist-orderaudit-desktop.png | cis-nursemanage-inpatientlist-orderaudit-mobile.png |
| `/NurseManage/InpatientList/OrderExecution` | cis-nursemanage-inpatientlist-orderexecution-desktop.png | cis-nursemanage-inpatientlist-orderexecution-mobile.png |
| `/NurseManage/InpatientMedReturn/Drug_withdrawal` | cis-nursemanage-inpatientmedreturn-drug-withdrawal-desktop.png | cis-nursemanage-inpatientmedreturn-drug-withdrawal-mobile.png |
| `/NurseManage/InpatientMedReturn/Index` | cis-nursemanage-inpatientmedreturn-index-desktop.png | cis-nursemanage-inpatientmedreturn-index-mobile.png |
| `/NurseManage/InpatientVitalSigns/Form` | cis-nursemanage-inpatientvitalsigns-form-desktop.png | cis-nursemanage-inpatientvitalsigns-form-mobile.png |
| `/NurseManage/InpatientVitalSigns/NursingInput` | cis-nursemanage-inpatientvitalsigns-nursinginput-desktop.png | cis-nursemanage-inpatientvitalsigns-nursinginput-mobile.png |
| `/NurseManage/InpatientVitalSigns/QueryIndex` | cis-nursemanage-inpatientvitalsigns-queryindex-desktop.png | cis-nursemanage-inpatientvitalsigns-queryindex-mobile.png |
| `/NurseManage/InpatientVitalSigns/TemperatureChartIndex` | cis-nursemanage-inpatientvitalsigns-temperaturechartindex-desktop.png | cis-nursemanage-inpatientvitalsigns-temperaturechartindex-mobile.png |
| `/NurseManage/OrderAudit/AuditTips` | cis-nursemanage-orderaudit-audittips-desktop.png | cis-nursemanage-orderaudit-audittips-mobile.png |
| `/NurseManage/OrderAudit/Drug_Inquiry` | cis-nursemanage-orderaudit-drug-inquiry-desktop.png | cis-nursemanage-orderaudit-drug-inquiry-mobile.png |
| `/NurseManage/OrderAudit/Index` | cis-nursemanage-orderaudit-index-desktop.png | cis-nursemanage-orderaudit-index-mobile.png |
| `/NurseManage/OrderAudit/Inputresults` | cis-nursemanage-orderaudit-inputresults-desktop.png | cis-nursemanage-orderaudit-inputresults-mobile.png |
| `/NurseManage/OrderAudit/InputresultsSkintest` | cis-nursemanage-orderaudit-inputresultsskintest-desktop.png | cis-nursemanage-orderaudit-inputresultsskintest-mobile.png |
| `/NurseManage/OrderAudit/PreparationStock` | cis-nursemanage-orderaudit-preparationstock-desktop.png | cis-nursemanage-orderaudit-preparationstock-mobile.png |
| `/NurseManage/OrderAudit/PrepareMedicineApply` | cis-nursemanage-orderaudit-preparemedicineapply-desktop.png | cis-nursemanage-orderaudit-preparemedicineapply-mobile.png |
| `/NurseManage/OrderAudit/SkinTestInfo` | cis-nursemanage-orderaudit-skintestinfo-desktop.png | cis-nursemanage-orderaudit-skintestinfo-mobile.png |
| `/NurseManage/OrderAudit/Ward_Application` | cis-nursemanage-orderaudit-ward-application-desktop.png | cis-nursemanage-orderaudit-ward-application-mobile.png |
| `/NurseManage/OrderExecution/ApplyFormUpload` | cis-nursemanage-orderexecution-applyformupload-desktop.png | cis-nursemanage-orderexecution-applyformupload-mobile.png |
| `/NurseManage/OrderExecution/BindingFeeForm` | cis-nursemanage-orderexecution-bindingfeeform-desktop.png | cis-nursemanage-orderexecution-bindingfeeform-mobile.png |
| `/NurseManage/OrderExecution/Index` | cis-nursemanage-orderexecution-index-desktop.png | cis-nursemanage-orderexecution-index-mobile.png |
| `/NurseManage/OrderExecution/MedicalSkillExecution` | cis-nursemanage-orderexecution-medicalskillexecution-desktop.png | cis-nursemanage-orderexecution-medicalskillexecution-mobile.png |
| `/NurseManage/OrderExecution/MedicalSkillQuery` | cis-nursemanage-orderexecution-medicalskillquery-desktop.png | cis-nursemanage-orderexecution-medicalskillquery-mobile.png |
| `/NurseManage/OutpatientConsult/ConsultCall` | cis-nursemanage-outpatientconsult-consultcall-desktop.png | cis-nursemanage-outpatientconsult-consultcall-mobile.png |
| `/NurseManage/OutpatientConsult/ConsultDoctor` | cis-nursemanage-outpatientconsult-consultdoctor-desktop.png | cis-nursemanage-outpatientconsult-consultdoctor-mobile.png |
| `/NurseManage/OutpatientConsult/ConsultInfo` | cis-nursemanage-outpatientconsult-consultinfo-desktop.png | cis-nursemanage-outpatientconsult-consultinfo-mobile.png |
| `/NurseManage/OutpatientConsult/Index` | cis-nursemanage-outpatientconsult-index-desktop.png | cis-nursemanage-outpatientconsult-index-mobile.png |
| `/NurseManage/PrepareMedicine/MedicineUseQuery` | cis-nursemanage-preparemedicine-medicineusequery-desktop.png | cis-nursemanage-preparemedicine-medicineusequery-mobile.png |
| `/NurseManage/PrepareMedicine/PrepareMedicineReturn` | cis-nursemanage-preparemedicine-preparemedicinereturn-desktop.png | cis-nursemanage-preparemedicine-preparemedicinereturn-mobile.png |
| `/NurseManage/PrepareMedicine/PrepareMedicineReturnAdd` | cis-nursemanage-preparemedicine-preparemedicinereturnadd-desktop.png | cis-nursemanage-preparemedicine-preparemedicinereturnadd-mobile.png |
| `/NurseManage/PrepareMedicine/ReportLossAndProfit` | cis-nursemanage-preparemedicine-reportlossandprofit-desktop.png | cis-nursemanage-preparemedicine-reportlossandprofit-mobile.png |
| `/NurseManage/PrepareMedicine/ReportLossAndProfitQuery` | cis-nursemanage-preparemedicine-reportlossandprofitquery-desktop.png | cis-nursemanage-preparemedicine-reportlossandprofitquery-mobile.png |
| `/NurseManage/ProfitAndLoss/Index` | cis-nursemanage-profitandloss-index-desktop.png | cis-nursemanage-profitandloss-index-mobile.png |
| `/NurseManage/ProfitAndLoss/QueryProfitAndLoss` | cis-nursemanage-profitandloss-queryprofitandloss-desktop.png | cis-nursemanage-profitandloss-queryprofitandloss-mobile.png |
| `/NurseManage/ProfitAndLoss/Syyy` | cis-nursemanage-profitandloss-syyy-desktop.png | cis-nursemanage-profitandloss-syyy-mobile.png |
| `/NurseManage/ProfitAndLoss/SyyyForm` | cis-nursemanage-profitandloss-syyyform-desktop.png | cis-nursemanage-profitandloss-syyyform-mobile.png |
| `/NurseManage/StockCarryOver/Index` | cis-nursemanage-stockcarryover-index-desktop.png | cis-nursemanage-stockcarryover-index-mobile.png |
| `/NurseManage/StockInventory/Index` | cis-nursemanage-stockinventory-index-desktop.png | cis-nursemanage-stockinventory-index-mobile.png |
| `/NurseManage/StockInventory/InventoryAction` | cis-nursemanage-stockinventory-inventoryaction-desktop.png | cis-nursemanage-stockinventory-inventoryaction-mobile.png |
| `/NurseManage/StockInventory/InventoryQuery` | cis-nursemanage-stockinventory-inventoryquery-desktop.png | cis-nursemanage-stockinventory-inventoryquery-mobile.png |
| `/NurseManage/TreatmentPrint/Index` | cis-nursemanage-treatmentprint-index-desktop.png | cis-nursemanage-treatmentprint-index-mobile.png |
| `/NurseManage/WardMaintenance/AttributesPlusBedForm` | cis-nursemanage-wardmaintenance-attributesplusbedform-desktop.png | cis-nursemanage-wardmaintenance-attributesplusbedform-mobile.png |
| `/NurseManage/WardMaintenance/AttributesPlusRoomForm` | cis-nursemanage-wardmaintenance-attributesplusroomform-desktop.png | cis-nursemanage-wardmaintenance-attributesplusroomform-mobile.png |
| `/NurseManage/WardMaintenance/BedBindingCostForm` | cis-nursemanage-wardmaintenance-bedbindingcostform-desktop.png | cis-nursemanage-wardmaintenance-bedbindingcostform-mobile.png |
| `/NurseManage/WardMaintenance/BedForm` | cis-nursemanage-wardmaintenance-bedform-desktop.png | cis-nursemanage-wardmaintenance-bedform-mobile.png |
| `/NurseManage/WardMaintenance/DoctorBindingForm` | cis-nursemanage-wardmaintenance-doctorbindingform-desktop.png | cis-nursemanage-wardmaintenance-doctorbindingform-mobile.png |
| `/NurseManage/WardMaintenance/InAreaDiagnoosisForm` | cis-nursemanage-wardmaintenance-inareadiagnoosisform-desktop.png | cis-nursemanage-wardmaintenance-inareadiagnoosisform-mobile.png |
| `/NurseManage/WardMaintenance/InHospitalBedCard` | cis-nursemanage-wardmaintenance-inhospitalbedcard-desktop.png | cis-nursemanage-wardmaintenance-inhospitalbedcard-mobile.png |
| `/NurseManage/WardMaintenance/InHospitalBedCardPrint` | cis-nursemanage-wardmaintenance-inhospitalbedcardprint-desktop.png | cis-nursemanage-wardmaintenance-inhospitalbedcardprint-mobile.png |
| `/NurseManage/WardMaintenance/OutAreaDiagnoosis` | cis-nursemanage-wardmaintenance-outareadiagnoosis-desktop.png | cis-nursemanage-wardmaintenance-outareadiagnoosis-mobile.png |
| `/NurseManage/WardMaintenance/OutAreaDiagnoosisForm` | cis-nursemanage-wardmaintenance-outareadiagnoosisform-desktop.png | cis-nursemanage-wardmaintenance-outareadiagnoosisform-mobile.png |
| `/NurseManage/WardMaintenance/OutAreaForm` | cis-nursemanage-wardmaintenance-outareaform-desktop.png | cis-nursemanage-wardmaintenance-outareaform-mobile.png |
| `/NurseManage/WardMaintenance/RoomForm` | cis-nursemanage-wardmaintenance-roomform-desktop.png | cis-nursemanage-wardmaintenance-roomform-mobile.png |
| `/NurseManage/WardMaintenance/WardWardRoomBedMaintenance` | cis-nursemanage-wardmaintenance-wardwardroombedmaintenance-desktop.png | cis-nursemanage-wardmaintenance-wardwardroombedmaintenance-mobile.png |
| `/OutpatientNurse/OutpatientPricing` | cis-outpatientnurse-outpatientpricing-desktop.png | cis-outpatientnurse-outpatientpricing-mobile.png |
| `/OutpatientNurse/PerformIndex` | cis-outpatientnurse-performindex-desktop.png | cis-outpatientnurse-performindex-mobile.png |
| `/OutpatientNurse/Skintestquery` | cis-outpatientnurse-skintestquery-desktop.png | cis-outpatientnurse-skintestquery-mobile.png |
| `/OutpatientNurse/observationQuery` | cis-outpatientnurse-observationquery-desktop.png | cis-outpatientnurse-observationquery-mobile.png |
| `/OutpatientNurse/prescriptionquery` | cis-outpatientnurse-prescriptionquery-desktop.png | cis-outpatientnurse-prescriptionquery-mobile.png |
| `/PatientList/Form` | cis-patientlist-form-desktop.png | cis-patientlist-form-mobile.png |
| `/PatientList/InPatSearchView` | cis-patientlist-inpatsearchview-desktop.png | cis-patientlist-inpatsearchview-mobile.png |
| `/PatientList/Index` | cis-patientlist-index-desktop.png | cis-patientlist-index-mobile.png |
| `/PatientList/InpatientInfoShow` | cis-patientlist-inpatientinfoshow-desktop.png | cis-patientlist-inpatientinfoshow-mobile.png |
| `/PatientList/InpatientList` | cis-patientlist-inpatientlist-desktop.png | cis-patientlist-inpatientlist-mobile.png |
| `/PatientList/PatientList` | cis-patientlist-patientlist-desktop.png | cis-patientlist-patientlist-mobile.png |
| `/PatientList/PatientVisitList` | cis-patientlist-patientvisitlist-desktop.png | cis-patientlist-patientvisitlist-mobile.png |
| `/Prescription/AddKfjyForm` | cis-prescription-addkfjyform-desktop.png | cis-prescription-addkfjyform-mobile.png |
| `/Prescription/ConsumablesPrescription` | cis-prescription-consumablesprescription-desktop.png | cis-prescription-consumablesprescription-mobile.png |
| `/Prescription/Index` | cis-prescription-index-desktop.png | cis-prescription-index-mobile.png |
| `/Prescription/RegularItemPrescription` | cis-prescription-regularitemprescription-desktop.png | cis-prescription-regularitemprescription-mobile.png |
| `/Prescription/RehabPrescription` | cis-prescription-rehabprescription-desktop.png | cis-prescription-rehabprescription-mobile.png |
| `/Prescription/TCMPrescription` | cis-prescription-tcmprescription-desktop.png | cis-prescription-tcmprescription-mobile.png |
| `/Prescription/TCMPrescription2022` | cis-prescription-tcmprescription2022-desktop.png | cis-prescription-tcmprescription2022-mobile.png |
| `/Prescription/WMPrescription` | cis-prescription-wmprescription-desktop.png | cis-prescription-wmprescription-mobile.png |
| `/QueueManage/QueueInfo/Index` | cis-queuemanage-queueinfo-index-desktop.png | cis-queuemanage-queueinfo-index-mobile.png |
| `/ReportManage/Report/DispensingQuery` | cis-reportmanage-report-dispensingquery-desktop.png | cis-reportmanage-report-dispensingquery-mobile.png |
| `/ReportManage/Report/Index` | cis-reportmanage-report-index-desktop.png | cis-reportmanage-report-index-mobile.png |
| `/ReportManage/Report/PrintKssCfPropReport` | cis-reportmanage-report-printksscfpropreport-desktop.png | cis-reportmanage-report-printksscfpropreport-mobile.png |
| `/ReportManage/Report/PrintKssJePMReport` | cis-reportmanage-report-printkssjepmreport-desktop.png | cis-reportmanage-report-printkssjepmreport-mobile.png |
| `/ReportManage/Report/PrintKssSlPMReport` | cis-reportmanage-report-printkssslpmreport-desktop.png | cis-reportmanage-report-printkssslpmreport-mobile.png |
| `/ReportManage/Report/ReportConfig` | cis-reportmanage-report-reportconfig-desktop.png | cis-reportmanage-report-reportconfig-mobile.png |
| `/ReportManage/Report/ReportConfigForm` | cis-reportmanage-report-reportconfigform-desktop.png | cis-reportmanage-report-reportconfigform-mobile.png |
| `/SystemManage/AuxiliaryDictionary/Form` | cis-systemmanage-auxiliarydictionary-form-desktop.png | cis-systemmanage-auxiliarydictionary-form-mobile.png |
| `/SystemManage/AuxiliaryDictionary/Index` | cis-systemmanage-auxiliarydictionary-index-desktop.png | cis-systemmanage-auxiliarydictionary-index-mobile.png |
| `/SystemManage/BespeakRegisterManage/BespeakRegister` | cis-systemmanage-bespeakregistermanage-bespeakregister-desktop.png | cis-systemmanage-bespeakregistermanage-bespeakregister-mobile.png |
| `/SystemManage/BespeakRegisterManage/Index` | cis-systemmanage-bespeakregistermanage-index-desktop.png | cis-systemmanage-bespeakregistermanage-index-mobile.png |
| `/SystemManage/BodyParts/Form` | cis-systemmanage-bodyparts-form-desktop.png | cis-systemmanage-bodyparts-form-mobile.png |
| `/SystemManage/BodyParts/Index` | cis-systemmanage-bodyparts-index-desktop.png | cis-systemmanage-bodyparts-index-mobile.png |
| `/SystemManage/BodyParts/JcForm` | cis-systemmanage-bodyparts-jcform-desktop.png | cis-systemmanage-bodyparts-jcform-mobile.png |
| `/SystemManage/DoctorRemark/Form` | cis-systemmanage-doctorremark-form-desktop.png | cis-systemmanage-doctorremark-form-mobile.png |
| `/SystemManage/DoctorRemark/Index` | cis-systemmanage-doctorremark-index-desktop.png | cis-systemmanage-doctorremark-index-mobile.png |
| `/SystemManage/InspectionCategory/Chat` | cis-systemmanage-inspectioncategory-chat-desktop.png | cis-systemmanage-inspectioncategory-chat-mobile.png |
| `/SystemManage/InspectionCategory/Form` | cis-systemmanage-inspectioncategory-form-desktop.png | cis-systemmanage-inspectioncategory-form-mobile.png |
| `/SystemManage/InspectionCategory/Index` | cis-systemmanage-inspectioncategory-index-desktop.png | cis-systemmanage-inspectioncategory-index-mobile.png |
| `/SystemManage/UsageLinkage/Form` | cis-systemmanage-usagelinkage-form-desktop.png | cis-systemmanage-usagelinkage-form-mobile.png |
| `/SystemManage/UsageLinkage/Index` | cis-systemmanage-usagelinkage-index-desktop.png | cis-systemmanage-usagelinkage-index-mobile.png |
| `/SystemManage/VisitDeptSet/Form` | cis-systemmanage-visitdeptset-form-desktop.png | cis-systemmanage-visitdeptset-form-mobile.png |
| `/SystemManage/VisitDeptSet/Index` | cis-systemmanage-visitdeptset-index-desktop.png | cis-systemmanage-visitdeptset-index-mobile.png |
| `/TemplateManage/DiagnosticTemplate/Index` | cis-templatemanage-diagnostictemplate-index-desktop.png | cis-templatemanage-diagnostictemplate-index-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/FoodForm` | cis-templatemanage-doctorservicetemplate-foodform-desktop.png | cis-templatemanage-doctorservicetemplate-foodform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/Index` | cis-templatemanage-doctorservicetemplate-index-desktop.png | cis-templatemanage-doctorservicetemplate-index-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/ItemForm` | cis-templatemanage-doctorservicetemplate-itemform-desktop.png | cis-templatemanage-doctorservicetemplate-itemform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/MedicineForm` | cis-templatemanage-doctorservicetemplate-medicineform-desktop.png | cis-templatemanage-doctorservicetemplate-medicineform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/RehabForm` | cis-templatemanage-doctorservicetemplate-rehabform-desktop.png | cis-templatemanage-doctorservicetemplate-rehabform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/TCMmedicineForm` | cis-templatemanage-doctorservicetemplate-tcmmedicineform-desktop.png | cis-templatemanage-doctorservicetemplate-tcmmedicineform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/TakeMedicineForm` | cis-templatemanage-doctorservicetemplate-takemedicineform-desktop.png | cis-templatemanage-doctorservicetemplate-takemedicineform-mobile.png |
| `/TemplateManage/DoctorServiceTemplate/WordForm` | cis-templatemanage-doctorservicetemplate-wordform-desktop.png | cis-templatemanage-doctorservicetemplate-wordform-mobile.png |
| `/TemplateManage/GroupPackage/Index` | cis-templatemanage-grouppackage-index-desktop.png | cis-templatemanage-grouppackage-index-mobile.png |
| `/TemplateManage/InspectionTemplate/Index` | cis-templatemanage-inspectiontemplate-index-desktop.png | cis-templatemanage-inspectiontemplate-index-mobile.png |
| `/TemplateManage/MRecordTemplate/Index` | cis-templatemanage-mrecordtemplate-index-desktop.png | cis-templatemanage-mrecordtemplate-index-mobile.png |
| `/TemplateManage/PresTemplate/Index` | cis-templatemanage-prestemplate-index-desktop.png | cis-templatemanage-prestemplate-index-mobile.png |
| `/TemplateManage/PresTemplate/RegularItemForm` | cis-templatemanage-prestemplate-regularitemform-desktop.png | cis-templatemanage-prestemplate-regularitemform-mobile.png |
| `/TemplateManage/PresTemplate/RehabForm` | cis-templatemanage-prestemplate-rehabform-desktop.png | cis-templatemanage-prestemplate-rehabform-mobile.png |
| `/TemplateManage/PresTemplate/TCMForm` | cis-templatemanage-prestemplate-tcmform-desktop.png | cis-templatemanage-prestemplate-tcmform-mobile.png |
| `/TemplateManage/PresTemplate/WMForm` | cis-templatemanage-prestemplate-wmform-desktop.png | cis-templatemanage-prestemplate-wmform-mobile.png |
| `/TemplateManage/PresTemplate/XzyyForm` | cis-templatemanage-prestemplate-xzyyform-desktop.png | cis-templatemanage-prestemplate-xzyyform-mobile.png |

## 2. PDS — Hệ thống nhà thuốc / kho thuốc (pds, cổng 5237)

| Màn hình / route | Desktop | Mobile |
|---|---|---|
| `/BillManage/ApplyBill/ApplyOutStock` | pds-billmanage-applybill-applyoutstock-desktop.png | pds-billmanage-applybill-applyoutstock-mobile.png |
| `/BillManage/ApplyBill/Query` | pds-billmanage-applybill-query-desktop.png | pds-billmanage-applybill-query-mobile.png |
| `/BillManage/OutOrInStorageBill/Approval` | pds-billmanage-outorinstoragebill-approval-desktop.png | pds-billmanage-outorinstoragebill-approval-mobile.png |
| `/BillManage/OutOrInStorageBill/DrugPreparation` | pds-billmanage-outorinstoragebill-drugpreparation-desktop.png | pds-billmanage-outorinstoragebill-drugpreparation-mobile.png |
| `/BillManage/OutOrInStorageBill/Form` | pds-billmanage-outorinstoragebill-form-desktop.png | pds-billmanage-outorinstoragebill-form-mobile.png |
| `/BillManage/OutOrInStorageBill/OutOrder` | pds-billmanage-outorinstoragebill-outorder-desktop.png | pds-billmanage-outorinstoragebill-outorder-mobile.png |
| `/BillManage/OutOrInStorageBill/Query` | pds-billmanage-outorinstoragebill-query-desktop.png | pds-billmanage-outorinstoragebill-query-mobile.png |
| `/Dictionaries/FypzForm` | pds-dictionaries-fypzform-desktop.png | pds-dictionaries-fypzform-mobile.png |
| `/Dictionaries/FypzIndex` | pds-dictionaries-fypzindex-desktop.png | pds-dictionaries-fypzindex-mobile.png |
| `/Dictionaries/KsFypzForm` | pds-dictionaries-ksfypzform-desktop.png | pds-dictionaries-ksfypzform-mobile.png |
| `/Dictionaries/KsFypzIndex` | pds-dictionaries-ksfypzindex-desktop.png | pds-dictionaries-ksfypzindex-mobile.png |
| `/Dictionaries/syyy` | pds-dictionaries-syyy-desktop.png | pds-dictionaries-syyy-mobile.png |
| `/Dictionaries/syyyForm` | pds-dictionaries-syyyform-desktop.png | pds-dictionaries-syyyform-mobile.png |
| `/DrugStorage/ApplyDelivery` | pds-drugstorage-applydelivery-desktop.png | pds-drugstorage-applydelivery-mobile.png |
| `/DrugStorage/BriefInStorage` | — | pds-drugstorage-briefinstorage-mobile.png |
| `/DrugStorage/ConfirmationOfReturn` | — | pds-drugstorage-confirmationofreturn-mobile.png |
| `/DrugStorage/DirectDeliveryBatch` | — | pds-drugstorage-directdeliverybatch-mobile.png |
| `/DrugStorage/ExcelExportChooseColumns` | pds-drugstorage-excelexportchoosecolumns-desktop.png | pds-drugstorage-excelexportchoosecolumns-mobile.png |
| `/DrugStorage/HandOutMedicine` | pds-drugstorage-handoutmedicine-desktop.png | pds-drugstorage-handoutmedicine-mobile.png |
| `/DrugStorage/HandOutMedicineByRequest` | pds-drugstorage-handoutmedicinebyrequest-desktop.png | pds-drugstorage-handoutmedicinebyrequest-mobile.png |
| `/DrugStorage/HandOutMedicineInfo` | pds-drugstorage-handoutmedicineinfo-desktop.png | pds-drugstorage-handoutmedicineinfo-mobile.png |
| `/DrugStorage/HandOutMedicineToks` | pds-drugstorage-handoutmedicinetoks-desktop.png | pds-drugstorage-handoutmedicinetoks-mobile.png |
| `/DrugStorage/InStorage` | pds-drugstorage-instorage-desktop.png | pds-drugstorage-instorage-mobile.png |
| `/DrugStorage/OutStorage` | pds-drugstorage-outstorage-desktop.png | pds-drugstorage-outstorage-mobile.png |
| `/DrugStorage/PSIStatistics2018` | pds-drugstorage-psistatistics2018-desktop.png | pds-drugstorage-psistatistics2018-mobile.png |
| `/DrugStorage/PreViewInfo` | pds-drugstorage-previewinfo-desktop.png | pds-drugstorage-previewinfo-mobile.png |
| `/DrugStorage/PriceAdjustmentApproval` | pds-drugstorage-priceadjustmentapproval-desktop.png | pds-drugstorage-priceadjustmentapproval-mobile.png |
| `/DrugStorage/PriceAdjustmentHistory` | pds-drugstorage-priceadjustmenthistory-desktop.png | pds-drugstorage-priceadjustmenthistory-mobile.png |
| `/DrugStorage/PriceAdjustmentRequest` | pds-drugstorage-priceadjustmentrequest-desktop.png | pds-drugstorage-priceadjustmentrequest-mobile.png |
| `/DrugStorage/StockCarryDown` | pds-drugstorage-stockcarrydown-desktop.png | pds-drugstorage-stockcarrydown-mobile.png |
| `/Home/About` | pds-home-about-desktop.png | pds-home-about-mobile.png |
| `/Home/Default` | pds-home-default-desktop.png | pds-home-default-mobile.png |
| `/Home/Index` | pds-home-desktop.png | pds-home-mobile.png |
| `/Home/SetUserPharmacyForm` | pds-home-setuserpharmacyform-desktop.png | pds-home-setuserpharmacyform-mobile.png |
| `/HospitalizationPharmacy/DeptMedicineApplyReturn` | pds-hospitalizationpharmacy-deptmedicineapplyreturn-desktop.png | pds-hospitalizationpharmacy-deptmedicineapplyreturn-mobile.png |
| `/HospitalizationPharmacy/DeptMedicineApplySend` | pds-hospitalizationpharmacy-deptmedicineapplysend-desktop.png | pds-hospitalizationpharmacy-deptmedicineapplysend-mobile.png |
| `/HospitalizationPharmacy/DispenseIndex` | pds-hospitalizationpharmacy-dispenseindex-desktop.png | pds-hospitalizationpharmacy-dispenseindex-mobile.png |
| `/HospitalizationPharmacy/FybdIndex` | pds-hospitalizationpharmacy-fybdindex-desktop.png | pds-hospitalizationpharmacy-fybdindex-mobile.png |
| `/HospitalizationPharmacy/PrescriptionDetailFrom` | pds-hospitalizationpharmacy-prescriptiondetailfrom-desktop.png | pds-hospitalizationpharmacy-prescriptiondetailfrom-mobile.png |
| `/HospitalizationPharmacy/PrescriptionPadQuery` | pds-hospitalizationpharmacy-prescriptionpadquery-desktop.png | pds-hospitalizationpharmacy-prescriptionpadquery-mobile.png |
| `/HospitalizationPharmacy/QueryIndex` | pds-hospitalizationpharmacy-queryindex-desktop.png | pds-hospitalizationpharmacy-queryindex-mobile.png |
| `/HospitalizationPharmacy/QueryIndexV2` | pds-hospitalizationpharmacy-queryindexv2-desktop.png | pds-hospitalizationpharmacy-queryindexv2-mobile.png |
| `/HospitalizationPharmacy/RepercussionIndex` | pds-hospitalizationpharmacy-repercussionindex-desktop.png | pds-hospitalizationpharmacy-repercussionindex-mobile.png |
| `/HospitalizationPharmacy/RepercussionIndex2021` | pds-hospitalizationpharmacy-repercussionindex2021-desktop.png | pds-hospitalizationpharmacy-repercussionindex2021-mobile.png |
| `/Login/Index` | pds-login-desktop.png | pds-login-mobile.png |
| `/Medicine/ExpiredDrugs` | pds-medicine-expireddrugs-desktop.png | pds-medicine-expireddrugs-mobile.png |
| `/Medicine/InventoryQureyReport` | pds-medicine-inventoryqureyreport-desktop.png | pds-medicine-inventoryqureyreport-mobile.png |
| `/Medicine/MedicineInfo` | pds-medicine-medicineinfo-desktop.png | pds-medicine-medicineinfo-mobile.png |
| `/Medicine/PriceAdjustmentProfitAndLossQuery` | pds-medicine-priceadjustmentprofitandlossquery-desktop.png | pds-medicine-priceadjustmentprofitandlossquery-mobile.png |
| `/Medicine/ReportLossAndProfit` | pds-medicine-reportlossandprofit-desktop.png | pds-medicine-reportlossandprofit-mobile.png |
| `/Medicine/ReportLossAndProfitQuery` | pds-medicine-reportlossandprofitquery-desktop.png | pds-medicine-reportlossandprofitquery-mobile.png |
| `/Medicine/RequestOfReturn` | pds-medicine-requestofreturn-desktop.png | pds-medicine-requestofreturn-mobile.png |
| `/Medicine/SyncDeptDrug` | pds-medicine-syncdeptdrug-desktop.png | pds-medicine-syncdeptdrug-mobile.png |
| `/Medicine/SysMedicine` | pds-medicine-sysmedicine-desktop.png | pds-medicine-sysmedicine-mobile.png |
| `/OutOrInStoredManage/Apply/Index` | pds-outorinstoredmanage-apply-index-desktop.png | pds-outorinstoredmanage-apply-index-mobile.png |
| `/OutOrInStoredManage/DeliveryDirect/From` | pds-outorinstoredmanage-deliverydirect-from-desktop.png | pds-outorinstoredmanage-deliverydirect-from-mobile.png |
| `/OutOrInStoredManage/DeliveryDirect/Index` | pds-outorinstoredmanage-deliverydirect-index-desktop.png | pds-outorinstoredmanage-deliverydirect-index-mobile.png |
| `/OutOrInStoredManage/DeliveryToDepartment/Index` | pds-outorinstoredmanage-deliverytodepartment-index-desktop.png | pds-outorinstoredmanage-deliverytodepartment-index-mobile.png |
| `/OutOrInStoredManage/InStorage/Index` | pds-outorinstoredmanage-instorage-index-desktop.png | pds-outorinstoredmanage-instorage-index-mobile.png |
| `/OutOrInStoredManage/Purchase/Bill` | pds-outorinstoredmanage-purchase-bill-desktop.png | pds-outorinstoredmanage-purchase-bill-mobile.png |
| `/OutOrInStoredManage/Purchase/BillForm` | pds-outorinstoredmanage-purchase-billform-desktop.png | pds-outorinstoredmanage-purchase-billform-mobile.png |
| `/OutOrInStoredManage/Purchase/BillView` | pds-outorinstoredmanage-purchase-billview-desktop.png | pds-outorinstoredmanage-purchase-billview-mobile.png |
| `/OutOrInStoredManage/Purchase/Delivery` | pds-outorinstoredmanage-purchase-delivery-desktop.png | pds-outorinstoredmanage-purchase-delivery-mobile.png |
| `/OutOrInStoredManage/Purchase/Form` | pds-outorinstoredmanage-purchase-form-desktop.png | pds-outorinstoredmanage-purchase-form-mobile.png |
| `/OutOrInStoredManage/Purchase/Index` | pds-outorinstoredmanage-purchase-index-desktop.png | pds-outorinstoredmanage-purchase-index-mobile.png |
| `/OutOrInStoredManage/Purchase/PurchaseApproval` | pds-outorinstoredmanage-purchase-purchaseapproval-desktop.png | pds-outorinstoredmanage-purchase-purchaseapproval-mobile.png |
| `/OutOrInStoredManage/Purchase/PurchasePlan` | pds-outorinstoredmanage-purchase-purchaseplan-desktop.png | pds-outorinstoredmanage-purchase-purchaseplan-mobile.png |
| `/OutOrInStoredManage/Purchase/PurchasePlanFrom` | pds-outorinstoredmanage-purchase-purchaseplanfrom-desktop.png | pds-outorinstoredmanage-purchase-purchaseplanfrom-mobile.png |
| `/OutOrInStoredManage/Purchase/PurchaseView` | pds-outorinstoredmanage-purchase-purchaseview-desktop.png | pds-outorinstoredmanage-purchase-purchaseview-mobile.png |
| `/OutOrInStoredManage/ReturnInward/Index` | pds-outorinstoredmanage-returninward-index-desktop.png | pds-outorinstoredmanage-returninward-index-mobile.png |
| `/OutOrInStoredManage/ReturnInwardToGys/Index` | pds-outorinstoredmanage-returninwardtogys-index-desktop.png | pds-outorinstoredmanage-returninwardtogys-index-mobile.png |
| `/OutPatientPharmacy/ChooseCf` | pds-outpatientpharmacy-choosecf-desktop.png | pds-outpatientpharmacy-choosecf-mobile.png |
| `/OutPatientPharmacy/DrugArrangement` | pds-outpatientpharmacy-drugarrangement-desktop.png | pds-outpatientpharmacy-drugarrangement-mobile.png |
| `/OutPatientPharmacy/DrugArrangement2018` | pds-outpatientpharmacy-drugarrangement2018-desktop.png | pds-outpatientpharmacy-drugarrangement2018-mobile.png |
| `/OutPatientPharmacy/DrugArrangement2019` | pds-outpatientpharmacy-drugarrangement2019-desktop.png | pds-outpatientpharmacy-drugarrangement2019-mobile.png |
| `/OutPatientPharmacy/DrugArrangementQuery` | pds-outpatientpharmacy-drugarrangementquery-desktop.png | pds-outpatientpharmacy-drugarrangementquery-mobile.png |
| `/OutPatientPharmacy/DrugDelivery2018` | pds-outpatientpharmacy-drugdelivery2018-desktop.png | pds-outpatientpharmacy-drugdelivery2018-mobile.png |
| `/OutPatientPharmacy/DrugRepercussion` | pds-outpatientpharmacy-drugrepercussion-desktop.png | pds-outpatientpharmacy-drugrepercussion-mobile.png |
| `/OutPatientPharmacy/DrugSearch` | pds-outpatientpharmacy-drugsearch-desktop.png | pds-outpatientpharmacy-drugsearch-mobile.png |
| `/OutPatientPharmacy/ElectronicPrescription` | pds-outpatientpharmacy-electronicprescription-desktop.png | pds-outpatientpharmacy-electronicprescription-mobile.png |
| `/OutPatientPharmacy/MzPrescriptionQuery` | pds-outpatientpharmacy-mzprescriptionquery-desktop.png | pds-outpatientpharmacy-mzprescriptionquery-mobile.png |
| `/OutPatientPharmacy/OutpatientDrugArrangementForzenQuery` | pds-outpatientpharmacy-outpatientdrugarrangementforzenquery-desktop.png | pds-outpatientpharmacy-outpatientdrugarrangementforzenquery-mobile.png |
| `/OutPatientPharmacy/PrescriptionDetailFrom` | pds-outpatientpharmacy-prescriptiondetailfrom-desktop.png | pds-outpatientpharmacy-prescriptiondetailfrom-mobile.png |
| `/OutPatientPharmacy/RePrintMedicineDoc` | pds-outpatientpharmacy-reprintmedicinedoc-desktop.png | pds-outpatientpharmacy-reprintmedicinedoc-mobile.png |
| `/Pharmacy/RequisitionForm` | pds-pharmacy-requisitionform-desktop.png | pds-pharmacy-requisitionform-mobile.png |
| `/Pharmacy/RequisitionSearchIndex` | pds-pharmacy-requisitionsearchindex-desktop.png | pds-pharmacy-requisitionsearchindex-mobile.png |
| `/PharmacyDepartment/Form` | pds-pharmacydepartment-form-desktop.png | pds-pharmacydepartment-form-mobile.png |
| `/PharmacyDepartment/Index` | pds-pharmacydepartment-index-desktop.png | pds-pharmacydepartment-index-mobile.png |
| `/PharmacyDrugStorage/InventoryAction` | pds-pharmacydrugstorage-inventoryaction-desktop.png | pds-pharmacydrugstorage-inventoryaction-mobile.png |
| `/PharmacyDrugStorage/InventoryActionV2` | pds-pharmacydrugstorage-inventoryactionv2-desktop.png | pds-pharmacydrugstorage-inventoryactionv2-mobile.png |
| `/PharmacyDrugStorage/InventoryQuery` | pds-pharmacydrugstorage-inventoryquery-desktop.png | pds-pharmacydrugstorage-inventoryquery-mobile.png |
| `/PharmacyDrugStorage/InventoryQueryV2` | pds-pharmacydrugstorage-inventoryqueryv2-desktop.png | pds-pharmacydrugstorage-inventoryqueryv2-mobile.png |
| `/PharmacyDrugStorage/ReceiptApproval` | pds-pharmacydrugstorage-receiptapproval-desktop.png | pds-pharmacydrugstorage-receiptapproval-mobile.png |
| `/PharmacyDrugStorage/ReceiptQueryV2` | pds-pharmacydrugstorage-receiptqueryv2-desktop.png | pds-pharmacydrugstorage-receiptqueryv2-mobile.png |
| `/PharmacyDrugStorage/StockInventoryV2` | pds-pharmacydrugstorage-stockinventoryv2-desktop.png | pds-pharmacydrugstorage-stockinventoryv2-mobile.png |
| `/PharmacyWindow/Form` | pds-pharmacywindow-form-desktop.png | pds-pharmacywindow-form-mobile.png |
| `/PharmacyWindow/Index` | pds-pharmacywindow-index-desktop.png | pds-pharmacywindow-index-mobile.png |
| `/PurchaseManage/Purchase/LocationForm` | pds-purchasemanage-purchase-locationform-desktop.png | pds-purchasemanage-purchase-locationform-mobile.png |
| `/PurchaseManage/Purchase/LocationIndex` | pds-purchasemanage-purchase-locationindex-desktop.png | pds-purchasemanage-purchase-locationindex-mobile.png |
| `/Report/PrintReport` | pds-report-printreport-desktop.png | pds-report-printreport-mobile.png |
| `/ReportManage/GqYpcx/GqYpcx` | pds-reportmanage-gqypcx-gqypcx-desktop.png | pds-reportmanage-gqypcx-gqypcx-mobile.png |
| `/ReportManage/GqYpcx/IteamSumQuery` | pds-reportmanage-gqypcx-iteamsumquery-desktop.png | pds-reportmanage-gqypcx-iteamsumquery-mobile.png |
| `/ReportManage/JsYpcx/QxIndex` | pds-reportmanage-jsypcx-qxindex-desktop.png | pds-reportmanage-jsypcx-qxindex-mobile.png |
| `/ReportManage/JsYpcx/YPCKMXIndex` | pds-reportmanage-jsypcx-ypckmxindex-desktop.png | pds-reportmanage-jsypcx-ypckmxindex-mobile.png |
| `/ReportManage/KsYpckQuery/KsYpckQuery` | pds-reportmanage-ksypckquery-ksypckquery-desktop.png | pds-reportmanage-ksypckquery-ksypckquery-mobile.png |
| `/ReportManage/Outpatient/DrugDeliveryStatistics` | pds-reportmanage-outpatient-drugdeliverystatistics-desktop.png | pds-reportmanage-outpatient-drugdeliverystatistics-mobile.png |
| `/ReportManage/Purchase/PurchaseBillDetailQuery` | pds-reportmanage-purchase-purchasebilldetailquery-desktop.png | pds-reportmanage-purchase-purchasebilldetailquery-mobile.png |
| `/ReportManage/YpckQuery/AntibioticCFQuery` | pds-reportmanage-ypckquery-antibioticcfquery-desktop.png | pds-reportmanage-ypckquery-antibioticcfquery-mobile.png |
| `/ReportManage/YpckQuery/AntibioticQuery` | pds-reportmanage-ypckquery-antibioticquery-desktop.png | pds-reportmanage-ypckquery-antibioticquery-mobile.png |
| `/ReportManage/YpckQuery/CFQuantity` | pds-reportmanage-ypckquery-cfquantity-desktop.png | pds-reportmanage-ypckquery-cfquantity-mobile.png |
| `/ReportManage/YpckQuery/DrugSubtotal` | pds-reportmanage-ypckquery-drugsubtotal-desktop.png | pds-reportmanage-ypckquery-drugsubtotal-mobile.png |
| `/ReportManage/YpckQuery/HospitalPrescription` | pds-reportmanage-ypckquery-hospitalprescription-desktop.png | pds-reportmanage-ypckquery-hospitalprescription-mobile.png |
| `/ReportManage/YpckQuery/OutpatientPrescription` | pds-reportmanage-ypckquery-outpatientprescription-desktop.png | pds-reportmanage-ypckquery-outpatientprescription-mobile.png |
| `/ReportManage/YpckQuery/YPDrugComparisonTable` | pds-reportmanage-ypckquery-ypdrugcomparisontable-desktop.png | pds-reportmanage-ypckquery-ypdrugcomparisontable-mobile.png |
| `/ReportManage/YpckQuery/YPDrugUsage` | pds-reportmanage-ypckquery-ypdrugusage-desktop.png | pds-reportmanage-ypckquery-ypdrugusage-mobile.png |
| `/ReportManage/YpckQuery/YPInOutBound` | pds-reportmanage-ypckquery-ypinoutbound-desktop.png | pds-reportmanage-ypckquery-ypinoutbound-mobile.png |
| `/ReportManage/YpckQuery/YpckQuery` | pds-reportmanage-ypckquery-ypckquery-desktop.png | pds-reportmanage-ypckquery-ypckquery-mobile.png |
| `/ReportManage/YpckQuery/YpckQueryNew` | pds-reportmanage-ypckquery-ypckquerynew-desktop.png | pds-reportmanage-ypckquery-ypckquerynew-mobile.png |
| `/ReportManage/YpckQuery/YprkQuery` | pds-reportmanage-ypckquery-yprkquery-desktop.png | pds-reportmanage-ypckquery-yprkquery-mobile.png |
| `/ReportManage/Ypfy/ypfyQuery` | pds-reportmanage-ypfy-ypfyquery-desktop.png | pds-reportmanage-ypfy-ypfyquery-mobile.png |
| `/ReportManage/Zyfytj/QxIndex` | pds-reportmanage-zyfytj-qxindex-desktop.png | pds-reportmanage-zyfytj-qxindex-mobile.png |
| `/ReportManage/yf/PharmacyMonthlyQuery` | pds-reportmanage-yf-pharmacymonthlyquery-desktop.png | pds-reportmanage-yf-pharmacymonthlyquery-mobile.png |
| `/Staff/Selector` | pds-staff-selector-desktop.png | pds-staff-selector-mobile.png |
| `/StorageManage/DrugStockSearch/ExpiredDateForm` | pds-storagemanage-drugstocksearch-expireddateform-desktop.png | pds-storagemanage-drugstocksearch-expireddateform-mobile.png |
| `/StorageManage/DrugStockSearch/ExpiredDateManage` | pds-storagemanage-drugstocksearch-expireddatemanage-desktop.png | pds-storagemanage-drugstocksearch-expireddatemanage-mobile.png |
| `/StorageManage/DrugStockSearch/ExpiredDrugsView` | pds-storagemanage-drugstocksearch-expireddrugsview-desktop.png | pds-storagemanage-drugstocksearch-expireddrugsview-mobile.png |
| `/StorageManage/DrugStockSearch/StockQuery` | pds-storagemanage-drugstocksearch-stockquery-desktop.png | pds-storagemanage-drugstocksearch-stockquery-mobile.png |
| `/SysMedicine/Index` | pds-sysmedicine-index-desktop.png | pds-sysmedicine-index-mobile.png |
| `/SysMedicine/SysMedicineAdd` | pds-sysmedicine-sysmedicineadd-desktop.png | pds-sysmedicine-sysmedicineadd-mobile.png |
| `/User/PersonalInfo` | pds-user-personalinfo-desktop.png | pds-user-personalinfo-mobile.png |
| `/User/Selector` | pds-user-selector-desktop.png | pds-user-selector-mobile.png |

## 3. Màn hình đã bỏ qua kèm lý do

### 3.1. Route view của app nhưng GET trực tiếp không render được màn hình

Mỗi dòng là một route; lý do là kết quả lần thử cuối sau restart (xem mục 4 về các lần SQL chết).
Các route chỉ mở được trong dialog/iframe kèm tham số (id bản ghi, khóa ngoại) hoặc thuộc controller
không có action GET tương ứng sẽ rơi vào nhóm này — không bịa dữ liệu để mở chúng, không ghi DB demo.

#### Herp — Hệ thống quản lý vật tư: 2 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/BillManage/Test/Index` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/SystemManage/SystemManage/Notice` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)

#### EMR — Bệnh án điện tử: 5 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/Home/UserOrgChoose` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/MedicalRecordManage/Blys/YSList` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/MedicalRecordManage/MedicalRecord/Bt_openPreview` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/MedicalRecordManage/MedicalRecord/Order_information` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/MedicalRecordManage/PatInfo/CommitShow` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)

#### Base HOSP — Hệ thống quản trị (bệnh viện): 11 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/Consult/ExpertForm` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/DataMedicalInsurance/Index` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/Department/WardInfo` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ItemsType/Form` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/Module/Organizes` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/CloneButton` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Role/Users` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Staff/ConsultInfo` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/Staff/WardInfo` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/Sys/Organize/Applications` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/User/Roles` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)

#### Base Web — Dữ liệu nền: 1 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/Organize/Applications` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)

#### CIS — Hệ thống thông tin lâm sàng: 62 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/ClinicManage/ClinicMetting/Index` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/DoctorManage/Apply/ReportTree` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/DoctorManage/DoctorsAdvice/TemplatePresForm` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/DoctorManage/DoctorsAdvice/ZyyzQuery` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/DoctorManage/OutpatientQuery/ConsultDetail` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Outpatien
- `/DoctorManage/OutpatientQuery/ConsultRecord` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Outpatien
- `/DoctorManage/OutpatientQuery/MZcasehistory` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Outpatien
- `/DoctorManage/OutpatientQuery/QueryOutpatientDetail` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Outpatien
- `/DoctorManage/OutpatientQuery/QueryOutpatientReservation` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Outpatien
- `/DoctorManage/Rehab/Index` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Rehab/Ind
- `/DoctorManage/Takemedicine/Index` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Takemedic
- `/DoctorManage/Words/Index` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/DoctorManage/Words/Ind
- `/Home/Default` — desktop: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response); mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/Home/Default", waiting
- `/Home/UserOrgChoose` — desktop: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response); mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/Home/UserOrgChoose", w
- `/InfusionManage/query` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/InfusionManage/query",
- `/MainBusiness/CmmAlertForm` — desktop: Không trả về màn hình HTML (JSON/trống); mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MainBusiness/CmmAlertF
- `/MainBusiness/Index` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MainBusiness/Index", w
- `/MainBusiness/Index1` — desktop: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response); mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MainBusiness/Index1", 
- `/MainBusiness/TcmHis` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MainBusiness/TcmHis", 
- `/MedicalRecord/AdmissionNoticeForm` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MedicalRecord/Admissio
- `/MedicalRecord/AdvanceRemind` — desktop: Không trả về màn hình HTML (JSON/trống); mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MedicalRecord/AdvanceR
- `/MedicalRecord/HistoryMedicalRecordForm` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MedicalRecord/HistoryM
- `/MedicalRecord/OutpatientTelemedicine` — desktop: đã chụp; mobile: Lỗi tải trang: Page.goto: Timeout 15000ms exceeded.
Call log:
  - navigating to "http://127.0.0.1:5234/MedicalRecord/Outpatie
- `/MedicalRecord/ViewXindianList` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/NurseManage/Infusion/HosAllergyManage` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/NurseManage/InpatientList/InHosBlWriting` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/NurseManage/InpatientVitalSigns/FullScreenTemperatureChartIndex` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/NurseManage/sldAPPlication/WzSldIndex` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Prescription/Dzcf` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Prescription/HistoryPresForm` — desktop: Không trả về màn hình HTML (JSON/trống); mobile: Lỗi tải trang: Page.goto: Navigation to "http://127.0.0.1:5234/Prescription/HistoryPresForm" is interrupted by another naviga
- `/ReportManage/Report/MRReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintCqyzReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintJcReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintJyReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintLsyzReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/PrintYzYpReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/RegularItemPresReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/RehabPresReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/RehabPresTreatmentReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/TCMPresReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/ReportManage/Report/WMPresReport` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)
- `/SystemManage/SysConfig/ConfigsPreviewIndex` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysConfig/Form` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysConfig/Index` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysConfig/TemplateForm` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysConfig/TemplateIndex` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysLog/Index` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysModule/Form` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysModule/Index` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysRole/Form` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysRole/Index` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysStaff/Selector` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysUser/Index` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysUser/PersonalInfo` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysUser/Roles` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/SysUser/Selector` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/TTCataloguesComparison/DetailFrom` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/TTCataloguesComparison/DetailIndex` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/TTCataloguesComparison/MainFrom` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SystemManage/TTCataloguesComparison/MainIndex` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/TemplateManage/PresTemplate/PresTemplateDetail` — cả 2 khung hình: Không trả về màn hình HTML (JSON/trống)

#### PDS — Hệ thống nhà thuốc / kho thuốc: 35 route thiếu ít nhất một khung hình ("đã chụp" nghĩa là khung hình kia có ảnh ở mục 2)

- `/AntibioticType/AntibioticTypeQuery` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/AntibioticType/AntibioticTypeSettingForm` — cả 2 khung hình: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/DrugStorage/BriefInStorage` — desktop: Lỗi tải trang: Page.wait_for_timeout: Page crashed; mobile: đã chụp
- `/DrugStorage/ConfirmationOfReturn` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/DrugStorage/ConfirmationOfReturn", ; mobile: đã chụp
- `/DrugStorage/DirectDeliveryBatch` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/DrugStorage/DirectDeliveryBatch", w; mobile: đã chụp
- `/HospitalizationPharmacy/RepercussionIndex2019` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/HospitalizationPharmacy/Repercussio; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Medicine/SysMedicineAdd` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Medicine/SysMedicineAdd", waiting u; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Module/Details` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Module/Details", waiting until "dom; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Module/Form` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Module/Form", waiting until "domcon; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Module/Index` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Module/Index", waiting until "domco; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Module/Organizes` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Module/Organizes", waiting until "d; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/CloneButton` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ModuleButton/CloneButton", waiting ; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/Details` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ModuleButton/Details", waiting unti; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/Form` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ModuleButton/Form", waiting until "; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/Index` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ModuleButton/Index", waiting until ; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ModuleButton/Organizes` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ModuleButton/Organizes", waiting un; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/OutOrInStoredManage/Transfer/Apply` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/OutOrInStoredManage/Transfer/Apply"; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/OutOrInStoredManage/Transfer/Warehousing` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/OutOrInStoredManage/Transfer/Wareho; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/OutPatientPharmacy/DrugDelivery` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/OutPatientPharmacy/DrugDelivery", w; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/PSIStatistics` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/PSIStatistics",; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/ReceiptQuery` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/ReceiptQuery", ; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/StockCarryOver` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/StockCarryOver"; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/StockInventory` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/StockInventory"; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/StockInventoryIndex` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/StockInventoryI; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/StockInventoryIndexV2` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/StockInventoryI; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyDrugStorage/StockQuery` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyDrugStorage/StockQuery", wa; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/PharmacyWindow/PharmacyWindowIndex` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/PharmacyWindow/PharmacyWindowIndex"; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/ReportManage/StandingBook/MainPage` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/ReportManage/StandingBook/MainPage"; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Role/Details` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Role/Details", waiting until "domco; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Role/Form` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Role/Form", waiting until "domconte; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/Role/Index` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/Role/Index", waiting until "domcont; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SysConfig/ConfigsPreviewIndex` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/SysConfig/ConfigsPreviewIndex", wai; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SysConfig/Form` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/SysConfig/Form", waiting until "dom; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SysConfig/Index` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/SysConfig/Index", waiting until "do; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)
- `/SysMedicine/YbbxblForm` — desktop: Lỗi tải trang: Page.goto: Page crashed
Call log:
  - navigating to "http://127.0.0.1:5237/SysMedicine/YbbxblForm", waiting un; mobile: Máy chủ đóng kết nối không trả lời (route không tồn tại trong app này hoặc handler lỗi trước khi trả response)

### 3.2. Liên kết menu chung (shared menu) trên trang chủ EMR và PDS — route của app khác

Trang chủ EMR và PDS đều render cùng một cây menu toàn hệ thống (89 liên kết, đã đếm lại và thử từng
liên kết sau khi SQL hồi phục). Trên host EMR, chỉ 4 liên kết là của chính EMR và đã chụp được
(`/SystemManage/SysUser/PersonalInfo`, `/MedicalRecordManage/PatInfo/Index`,
`/MedicalRecordManage/PatInfo/PatMedRecordList`, `/MedicalRecordManage/Outpatient/Index`);
85 liên kết còn lại trỏ tới controller của app khác và máy chủ EMR đóng kết nối không trả lời
(NS_ERROR_NET_EMPTY_RESPONSE) khi mở trực tiếp trên host EMR.
Trên host PDS, cả 89 liên kết đều đóng kết nối không trả lời — kể cả các trang vốn là của EMR —
chứng tỏ đây là menu chung, không phải điều hướng riêng của PDS. Các trang thật của PDS đã được
chụp qua danh sách view của chính PDS (mục 2), không qua cây menu chung này.

EMR — 85 liên kết bỏ qua (máy chủ đóng kết nối không trả lời):

- `/SysManage/SysModule/Index` — Menu he thong
- `/SysManage/SysRole/Index` — Quan ly vai tro
- `/SysManage/User/Index` — Nguoi dung he thong
- `/PatientManage/HospiterRes/Index` — Thong tin benh nhan
- `/OutpatientManage/OutpatientReg/OutpatientReg2018` — Dang ky kham ngoai tru
- `/OutpatientManage/OutpatCharge/Index2018` — Thu phi ngoai tru
- `/OutpatientManage/OutpatientRefund/Index2018` — Hoan phi ngoai tru
- `/OutpatientManage/OutpatientChargeQuery/OutpatientChargeQuery` — Tra cuu thu phi
- `/PatientManage/HospiterRes/Register` — Dang ky nhap vien
- `/SystemManage/InpatientAccountManage/Index` — Quan ly tien tam ung noi tru
- `/HospitalizationManage/BookkeepInHos/Index` — Bo sung phi noi tru
- `/HospitalizationManage/DischargeSettle/PartialSettleIndex` — Quyet toan giua chung
- `/HospitalizationManage/DischargeSettle/SimulateIndex` — Quyet toan truoc bao hiem y te
- `/HospitalizationManage/DischargeSettle/Index` — Quyet toan ra vien
- `/HospitalizationManage/DischargeSettle/CancelIndex` — Huy quyet toan ra vien
- `/HospitalizationManage/InpatientRefund/Index` — Hoan phi noi tru
- `/PatientManage/Inpatient/InPatientQueryIndex` — Tra cuu benh nhan noi tru
- `/HospitalizationManage/Settlement/InPatientSettQuery` — Tra cuu quyet toan noi tru
- `/HospitalizationManage/Settlement/HospitalizationSettlementQuery` — Tra cuu quyet toan ra vien
- `/ReportManage/Report/InpatientList` — Danh sach benh nhan noi tru
- `/Treatment/Index` — Cau hinh to hop khoan thu phi
- `/SystemManage/FinancialInvoice/FinanicalInvoiceIndex` — Cau hinh hoa don tai vu
- `/SystemManage/PatientCharacteristics/Index` — Tinh chat benh nhan
- `/SystemManage/SysParameterConfig/ChargeTemplateIndex` — Bao tri mau thu phi
- `/SystemManage/FinancialReceipt/Index` — Cau hinh bien lai tai vu
- `/OutpatientManage/OutBook/Index` — Lich truc khoa ngoai tru
- `/OutpatientManage/OutBook/RosterCalendar` — Lich truc ngoai tru
- `/NurseManage/Infusion/Injection` — Thuc hien truyen dich
- `/NurseManage/Infusion/InfusionHistory` — Tra cuu truyen dich
- `/OutpatientNurse/PerformIndex` — Thuc hien thu phan ung da
- `/OutpatientNurse/Skintestquery` — Tra cuu thu phan ung da
- `/OutpatientNurse/observationQuery` — Tra cuu theo doi luu vien
- `/OutpatientNurse/prescriptionquery` — In benh an ngoai tru
- `/NurseManage/OutpatientConsult/Index` — Phan luong kham ngoai tru
- `/NurseManage/OutpatientConsult/ConsultCall` — Goi so phan luong kham
- `/NurseManage/OutpatientConsult/ConsultDoctor` — Bac si phan luong kham
- `/MainBusiness/Index` — Don thuoc ngoai tru
- `/MedicalRecord/LogQuery` — Nhat ky ngoai tru
- `/DoctorManage/OutpatientQuery/ConsultRecord` — Ho so kham ngoai tru
- `/DoctorManage/OutpatientQuery/ConsultDetail` — Chi tiet kham ngoai tru
- `/PatientList/InpatientList` — Tong quan benh nhan
- `/NurseManage/WardMaintenance/OutAreaDiagnoosis` — Chan doan noi tru
- `/DoctorManage/DoctorsAdvice/Index` — Quan ly y lenh
- `/NurseManage/InpatientList/Inpatient` — Tong quan benh nhan
- `/NurseManage/OrderAudit/InputresultsSkintest` — Nhap thu phan ung da
- `/NurseManage/PrepareMedicine/MedicineUseQuery` — Tra cuu su dung thuoc
- `/NurseManage/EntryRegistration/Index` — Dang ky vao khu
- `/NurseManage/OrderAudit/Index` — Duyet y lenh
- `/NurseManage/OrderExecution/Index` — Thuc hien y lenh
- `/NurseManage/InpatientMedReturn/Index` — Tra thuoc theo y lenh
- `/NurseManage/EntryRegistration/RecallOutArea` — Goi lai ra khoi khu
- `/NurseManage/InpatientVitalSigns/QueryIndex` — Nhap dieu duong
- `/NurseManage/InpatientVitalSigns/NursingInput` — Nhap dieu duong nhieu benh nhan
- `/NurseManage/InpatientVitalSigns/TemperatureChartIndex` — Tra cuu phieu do 3 chi so
- `/NurseManage/ExceReportPrint/Index` — In phieu thuc hien
- `/NurseManage/ExceReportPrint/QueryPrintIndex` — Tra cuu thuc hien y lenh
- `/NurseManage/InpatientMedReturn/Drug_withdrawal` — Tra cuu tra thuoc theo y lenh
- `/ReportManage/Report/DispensingQuery` — Tra cuu phat thuoc theo y lenh
- `/PatientManage/PatientCenter/Index` — Trung tam benh nhan noi tru
- `/TemplateManage/GroupPackage/Index` — Bao tri go xet nghiem
- `/TemplateManage/PresTemplate/Index` — Bao tri mau don thuoc
- `/TemplateManage/MRecordTemplate/Index` — Bao tri mau benh an
- `/TemplateManage/InspectionTemplate/Index` — Doi chieu mau go kiem tra
- `/TemplateManage/DoctorServiceTemplate/Index` — Bao tri mau y lenh
- `/ReportManage/Report/AllMedicineInsuranceQuery	` — Bao cao nghiep vu bao hiem y te
- `/ReportManage/Report/OutpatientWorkloadQuery` — Thong ke khoi luong bac si ngoai tru
- `/ReportManage/Report/OutpatientQuery` — Bao cao kham ngoai tru va cap cuu
- `/ReportManage/Report/FYXMMXQuery` — Thong ke chi tiet khoan muc
- `/ReportManage/Report/MZJBFYFX` — Phan tich chi phi bao hiem cu tru ngoai tru
- `/ReportManage/Report/MZFYRBHZ_Query` — Tong hop bang quyet toan ngay ngoai tru
- `/ReportManage/Report/MZCBFYFX` — Phan tich chi phi bao hiem thanh pho ngoai tru
- `/ReportManage/Report/OutpatientSituationQuery` — Thong ke thu phi don thuoc ngoai tru
- `/ReportManage/Report/PrintReport` — Chi phi trong ngay
- `/ReportManage/Report/OutpatientDailyFee` — Bang quyet toan ngay chi phi ngoai tru
- `/ReportManage/Report/HosFeeSettSummry` — Bang quyet toan ngay chi phi noi tru
- `/ReportManage/Report/Hospitalizationcost` — Chi tiet chi phi noi tru
- `/ReportManage/Report/Hospitalizationclassification` — Danh sach phan loai benh nhan noi tru
- `/ReportManage/Report/YBSBHZQDQuery` — Tong hop khai bao quyet toan chi phi
- `/ReportManage/Report/YBSBSC_ZGQuery` — Bang quyet toan cong nhan vien thanh thi
- `/ReportManage/Report/YBSBSC_JMQuery` — Bang quyet toan cu dan thanh thi
- `/ReportManage/Report/YBSBSC_YDQuery` — Bang quyet toan bao hiem y te khac dia phuong
- `/ReportManage/Report/YBSBSC_HZBKQuery` — Bao cao ho tro tuong tro y te
- `/DaySettleManage/DaySettle/ApplicationIndex` — Doi soat tong so thanh toan
- `/DaySettleManage/DaySettle/ApplicationDetailed` — Doi soat chi tiet thanh toan
- `/DaySettleManage/DaySettle/DzrIndex` — Doi soat ngay bao hiem y te

PDS — 89 liên kết bỏ qua (máy chủ đóng kết nối không trả lời):

- `/SystemManage/SysUser/PersonalInfo` — Thông tin cá nhân
- `/SysManage/SysModule/Index` — Menu he thong
- `/SysManage/SysRole/Index` — Quan ly vai tro
- `/SysManage/User/Index` — Nguoi dung he thong
- `/MedicalRecordManage/PatInfo/Index` — Quan ly benh an noi tru
- `/MedicalRecordManage/PatInfo/PatMedRecordList` — Quan ly benh an
- `/MedicalRecordManage/Outpatient/Index` — Quan ly phuc hoi ngoai tru
- `/PatientManage/HospiterRes/Index` — Thong tin benh nhan
- `/OutpatientManage/OutpatientReg/OutpatientReg2018` — Dang ky kham ngoai tru
- `/OutpatientManage/OutpatCharge/Index2018` — Thu phi ngoai tru
- `/OutpatientManage/OutpatientRefund/Index2018` — Hoan phi ngoai tru
- `/OutpatientManage/OutpatientChargeQuery/OutpatientChargeQuery` — Tra cuu thu phi
- `/PatientManage/HospiterRes/Register` — Dang ky nhap vien
- `/SystemManage/InpatientAccountManage/Index` — Quan ly tien tam ung noi tru
- `/HospitalizationManage/BookkeepInHos/Index` — Bo sung phi noi tru
- `/HospitalizationManage/DischargeSettle/PartialSettleIndex` — Quyet toan giua chung
- `/HospitalizationManage/DischargeSettle/SimulateIndex` — Quyet toan truoc bao hiem y te
- `/HospitalizationManage/DischargeSettle/Index` — Quyet toan ra vien
- `/HospitalizationManage/DischargeSettle/CancelIndex` — Huy quyet toan ra vien
- `/HospitalizationManage/InpatientRefund/Index` — Hoan phi noi tru
- `/PatientManage/Inpatient/InPatientQueryIndex` — Tra cuu benh nhan noi tru
- `/HospitalizationManage/Settlement/InPatientSettQuery` — Tra cuu quyet toan noi tru
- `/HospitalizationManage/Settlement/HospitalizationSettlementQuery` — Tra cuu quyet toan ra vien
- `/ReportManage/Report/InpatientList` — Danh sach benh nhan noi tru
- `/Treatment/Index` — Cau hinh to hop khoan thu phi
- `/SystemManage/FinancialInvoice/FinanicalInvoiceIndex` — Cau hinh hoa don tai vu
- `/SystemManage/PatientCharacteristics/Index` — Tinh chat benh nhan
- `/SystemManage/SysParameterConfig/ChargeTemplateIndex` — Bao tri mau thu phi
- `/SystemManage/FinancialReceipt/Index` — Cau hinh bien lai tai vu
- `/OutpatientManage/OutBook/Index` — Lich truc khoa ngoai tru
- `/OutpatientManage/OutBook/RosterCalendar` — Lich truc ngoai tru
- `/NurseManage/Infusion/Injection` — Thuc hien truyen dich
- `/NurseManage/Infusion/InfusionHistory` — Tra cuu truyen dich
- `/OutpatientNurse/PerformIndex` — Thuc hien thu phan ung da
- `/OutpatientNurse/Skintestquery` — Tra cuu thu phan ung da
- `/OutpatientNurse/observationQuery` — Tra cuu theo doi luu vien
- `/OutpatientNurse/prescriptionquery` — In benh an ngoai tru
- `/NurseManage/OutpatientConsult/Index` — Phan luong kham ngoai tru
- `/NurseManage/OutpatientConsult/ConsultCall` — Goi so phan luong kham
- `/NurseManage/OutpatientConsult/ConsultDoctor` — Bac si phan luong kham
- `/MainBusiness/Index` — Don thuoc ngoai tru
- `/MedicalRecord/LogQuery` — Nhat ky ngoai tru
- `/DoctorManage/OutpatientQuery/ConsultRecord` — Ho so kham ngoai tru
- `/DoctorManage/OutpatientQuery/ConsultDetail` — Chi tiet kham ngoai tru
- `/PatientList/InpatientList` — Tong quan benh nhan
- `/NurseManage/WardMaintenance/OutAreaDiagnoosis` — Chan doan noi tru
- `/DoctorManage/DoctorsAdvice/Index` — Quan ly y lenh
- `/NurseManage/InpatientList/Inpatient` — Tong quan benh nhan
- `/NurseManage/OrderAudit/InputresultsSkintest` — Nhap thu phan ung da
- `/NurseManage/PrepareMedicine/MedicineUseQuery` — Tra cuu su dung thuoc
- `/NurseManage/EntryRegistration/Index` — Dang ky vao khu
- `/NurseManage/OrderAudit/Index` — Duyet y lenh
- `/NurseManage/OrderExecution/Index` — Thuc hien y lenh
- `/NurseManage/InpatientMedReturn/Index` — Tra thuoc theo y lenh
- `/NurseManage/EntryRegistration/RecallOutArea` — Goi lai ra khoi khu
- `/NurseManage/InpatientVitalSigns/QueryIndex` — Nhap dieu duong
- `/NurseManage/InpatientVitalSigns/NursingInput` — Nhap dieu duong nhieu benh nhan
- `/NurseManage/InpatientVitalSigns/TemperatureChartIndex` — Tra cuu phieu do 3 chi so
- `/NurseManage/ExceReportPrint/Index` — In phieu thuc hien
- `/NurseManage/ExceReportPrint/QueryPrintIndex` — Tra cuu thuc hien y lenh
- `/NurseManage/InpatientMedReturn/Drug_withdrawal` — Tra cuu tra thuoc theo y lenh
- `/ReportManage/Report/DispensingQuery` — Tra cuu phat thuoc theo y lenh
- `/PatientManage/PatientCenter/Index` — Trung tam benh nhan noi tru
- `/TemplateManage/GroupPackage/Index` — Bao tri go xet nghiem
- `/TemplateManage/PresTemplate/Index` — Bao tri mau don thuoc
- `/TemplateManage/MRecordTemplate/Index` — Bao tri mau benh an
- `/TemplateManage/InspectionTemplate/Index` — Doi chieu mau go kiem tra
- `/TemplateManage/DoctorServiceTemplate/Index` — Bao tri mau y lenh
- `/ReportManage/Report/AllMedicineInsuranceQuery	` — Bao cao nghiep vu bao hiem y te
- `/ReportManage/Report/OutpatientWorkloadQuery` — Thong ke khoi luong bac si ngoai tru
- `/ReportManage/Report/OutpatientQuery` — Bao cao kham ngoai tru va cap cuu
- `/ReportManage/Report/FYXMMXQuery` — Thong ke chi tiet khoan muc
- `/ReportManage/Report/MZJBFYFX` — Phan tich chi phi bao hiem cu tru ngoai tru
- `/ReportManage/Report/MZFYRBHZ_Query` — Tong hop bang quyet toan ngay ngoai tru
- `/ReportManage/Report/MZCBFYFX` — Phan tich chi phi bao hiem thanh pho ngoai tru
- `/ReportManage/Report/OutpatientSituationQuery` — Thong ke thu phi don thuoc ngoai tru
- `/ReportManage/Report/PrintReport` — Chi phi trong ngay
- `/ReportManage/Report/OutpatientDailyFee` — Bang quyet toan ngay chi phi ngoai tru
- `/ReportManage/Report/HosFeeSettSummry` — Bang quyet toan ngay chi phi noi tru
- `/ReportManage/Report/Hospitalizationcost` — Chi tiet chi phi noi tru
- `/ReportManage/Report/Hospitalizationclassification` — Danh sach phan loai benh nhan noi tru
- `/ReportManage/Report/YBSBHZQDQuery` — Tong hop khai bao quyet toan chi phi
- `/ReportManage/Report/YBSBSC_ZGQuery` — Bang quyet toan cong nhan vien thanh thi
- `/ReportManage/Report/YBSBSC_JMQuery` — Bang quyet toan cu dan thanh thi
- `/ReportManage/Report/YBSBSC_YDQuery` — Bang quyet toan bao hiem y te khac dia phuong
- `/ReportManage/Report/YBSBSC_HZBKQuery` — Bao cao ho tro tuong tro y te
- `/DaySettleManage/DaySettle/ApplicationIndex` — Doi soat tong so thanh toan
- `/DaySettleManage/DaySettle/ApplicationDetailed` — Doi soat chi tiet thanh toan
- `/DaySettleManage/DaySettle/DzrIndex` — Doi soat ngay bao hiem y te

### 3.3. Các trường hợp đặc biệt (không tính là bỏ qua, ghi rõ để khỏi hiểu lầm)

- **EMR `/Home/Default` (nhánh root-only theo lệnh chặng): ĐÃ CHỤP, không bỏ qua.** Thực tế kiểm chứng:
  với tài khoản 000000, `/Home/Default` render đầy đủ dashboard (KPI, biểu đồ, thông báo) — xem
  `emr-home-default-desktop.png`. Nhánh chỉ-dành-cho-root (nếu có) không chặn 000000 ở trang này,
  và không có tài khoản nào khác được thử để đối chiếu.
- **HOSP: sidebar của 000000 chỉ hiện "Thông tin cá nhân".** Trang chủ HOSP sau đăng nhập là shell
  trống với duy nhất liên kết PersonalInfo. Toàn bộ các trang dữ liệu nền của HOSP (ChargeCategory,
  SysMedicine, Ward, ...) vẫn được chụp qua danh sách view của app (mục 2, HOSP) — tức là các trang
  đó tồn tại và render được, nhưng không xuất hiện trong sidebar của role này.
- **Màn hình stub Windows-only: chụp đúng trạng thái stub, không bỏ qua.** Ví dụ:
  `herp-report-index-*` (thông báo SSRS: 13 báo cáo .rdl chưa chạy được trên ASP.NET Core 6.0),
  `emr-medicalrecordmanage-medicalrecord-index-*` (banner stub điều khiển DCSoft: không tải/cài đặt được,
  kèm thanh công cụ editor), các trang in báo cáo của CIS/PDS (in qua SSRS/DCSoft).
- **Màn hình cần tạo bản ghi trước mới xem được chi tiết:** không dựng STG, không ghi DB demo;
  các route dạng xem/chi tiết/sửa đã được mở trực tiếp bằng GET ở trạng thái rỗng/form trống và chụp
  trung thực; route nào GET trực tiếp không render thì nằm trong mục 3.1 kèm lý do.
- **Mobile = giả lập viewport 390×844**, không phải app native; không tuyên bố khác.

## 4. Ghi chú phương pháp và sự cố trong phiên (để điều phối viên đối chiếu)

- **SQL Server chết lặp lại nhiều lần trong phiên** (VM restart lúc đầu phiên xóa toàn bộ tiến trình,
  rồi SQL chết thêm nhiều lần giữa chừng khi tải cao). Mỗi lần đều chạy `recovery.sh` (phê duyệt tự động
  có sẵn) để đưa 1433 lên lại; không app nào bị restart vì cổng prod vẫn up. Các pass đang chạy lúc SQL
  chết (login fail, route timeout) đều được chạy lại sau khi SQL hồi phục; shooter có cơ chế tự kiểm tra
  1433 mỗi 20 route và tự recovery + đăng nhập lại giữa pass.
- **VM restart giữa phiên xóa `/tmp`**: toàn bộ script và manifest tạm thời mất. Danh mục này được tái tạo
  từ ảnh còn lại trên đĩa (588 ảnh lúc tái tạo, đối soát 0 lệch) rồi chụp bổ sung các route còn thiếu;
  script và manifest mới giữ tại `~/workspace/s2_work/` (ngoài repo) để không mất nữa.
- **Firefox crash sau các pass dài** (browser đóng bất ngờ, các route sau đó ghi nhận sai là "browser has been
  closed"): đã loại 370 bản ghi sai đó khỏi danh sách bỏ qua và sửa shooter khởi động lại browser mỗi
  40 route; các route bị ảnh hưởng đã được chụp lại trong các pass sau.
- **Lần đầu chạy, CIS/PDS/SSO đăng nhập trực tiếp thất bại** ("Đăng nhập thất bại, vui lòng liên hệ quản trị
  viên") — nguyên nhân là SQL đã chết lúc đó, không phải tài khoản sai hay bị khóa; sau recovery, cả 6 app
  đều đăng nhập 000000 thành công và ảnh "home" chụp nhầm trang login ban đầu của CIS/PDS đã bị xóa, chụp lại.
- **Không còn listener STG nào của chặng này:** không tạo instance STG nào (không cổng riêng, không DB
  `*_STG` mới); cuối phiên chỉ còn các cổng prod đã biết. Không còn tiến trình chụp nào CỦA CHẶNG NÀY
  còn chạy (các tiến trình Firefox còn lại lúc cuối phiên thuộc worker khác, xem sự cố dưới).
- **Sự cố dọn dẹp cuối phiên (khai báo trung thực):** khi kiểm tra tiến trình còn sót, tôi đã kill một cây
  Firefox (PID 29059, khởi động 21:07) vì tưởng là trình duyệt mồ côi của chính mình; sau đó xác minh
  được cây đó gần như chắc chắn thuộc worker khác (shell của worker đó chạy `s1_forms4.py mrqc ...`,
  khởi động 21:06:59, tức 1 giây trước cây Firefox). Tôi không chạm gì thêm vào tiến trình của worker đó;
  shell mrqc của họ vẫn còn sống lúc kiểm tra cuối, và một worker SSO khác (`s1_forms4.py sso ...`) đang
  chạy bình thường. Điều phối viên nên hỏi worker MRQC xem pass của họ có cần chạy lại phần bị gián đoạn
  không. Đây là lỗi của tôi, không phải lỗi hệ thống.
- **Không commit ảnh:** thư mục `shots-redesign/` đã gitignore; file danh mục này chưa commit (điều phối viên quyết).
- **Không sửa `roles-shots-progress.md`.**
