# Danh mục ảnh chụp — Chặng 5 (SSO / MRQC / Sett / MRMS / OR)

Tài khoản chụp: **000000 (HospAdministrator)** — role cao nhất. Chỉ dùng tài khoản này;
không thử xzadmin hay admin. Mọi ảnh chụp trên UI đã redesign (nhánh V2.2, HEAD
7d30446a đã redeploy ở chặng 4), trên môi trường prod local. Ảnh nằm trong
`shots-redesign/` (thư mục gitignore — không commit ảnh).

## Tổng số: 192 ảnh — 96 desktop (1280×900) + 96 mobile (390×844)

| App | Desktop | Mobile | Tổng |
|---|---|---|---|
| SSO (5208) | 15 | 15 | 30 |
| MRMS (5232) | 7 | 7 | 14 |
| MRQC (5220) | 10 | 10 | 20 |
| Sett (5221) | 40 | 40 | 80 |
| OR (5231) | 24 | 24 | 48 |
| **Tổng** | **96** | **96** | **192** |

Mobile = giả lập viewport (không có app native). Chỉ chụp viewport, không dùng
full_page (trang dài crash trên 32767px). Không ảnh nào được submit form hay ghi DB.

## SSO (5208)

| Màn hình / chức năng | Viewport | Tên file |
|---|---|---|
| Tab liên app mẫu: Đăng ký khám ngoại trú (Sett) qua shell SSO | desktop | `sso-cross-sett-dangkykham-desktop.png` |
| Tab liên app mẫu: Đăng ký khám ngoại trú (Sett) qua shell SSO | mobile | `sso-cross-sett-dangkykham-mobile.png` |
| Dialog Thêm (tab shell): Menu hệ thống | desktop | `sso-dialog-sysmodule-desktop.png` |
| Dialog Thêm (tab shell): Menu hệ thống | mobile | `sso-dialog-sysmodule-mobile.png` |
| Dialog Thêm (tab shell): Quản lý vai trò | desktop | `sso-dialog-sysrole-desktop.png` |
| Dialog Thêm (tab shell): Quản lý vai trò | mobile | `sso-dialog-sysrole-mobile.png` |
| Form Sửa (có dữ liệu): Menu hệ thống | desktop | `sso-form-edit-sysmodule-desktop.png` |
| Form Sửa (có dữ liệu): Menu hệ thống | mobile | `sso-form-edit-sysmodule-mobile.png` |
| Form Sửa (có dữ liệu): Quản lý vai trò | desktop | `sso-form-edit-sysrole-desktop.png` |
| Form Sửa (có dữ liệu): Quản lý vai trò | mobile | `sso-form-edit-sysrole-mobile.png` |
| Form Thêm: Menu hệ thống | desktop | `sso-form-sysmodule-desktop.png` |
| Form Thêm: Menu hệ thống | mobile | `sso-form-sysmodule-mobile.png` |
| Form Thêm: Quản lý vai trò | desktop | `sso-form-sysrole-desktop.png` |
| Form Thêm: Quản lý vai trò | mobile | `sso-form-sysrole-mobile.png` |
| Form Vai trò người dùng (theo dòng đã chọn) | desktop | `sso-form-userroles-desktop.png` |
| Form Vai trò người dùng (theo dòng đã chọn) | mobile | `sso-form-userroles-mobile.png` |
| Trang chủ (shell) | desktop | `sso-home-desktop.png` |
| Trang chủ (shell) | mobile | `sso-home-mobile.png` |
| Trang đăng nhập | desktop | `sso-login-desktop.png` |
| Trang đăng nhập | mobile | `sso-login-mobile.png` |
| Trung tâm tin nhắn | desktop | `sso-notice-desktop.png` |
| Trung tâm tin nhắn | mobile | `sso-notice-mobile.png` |
| Menu hệ thống | desktop | `sso-sysmodule-desktop.png` |
| Menu hệ thống | mobile | `sso-sysmodule-mobile.png` |
| Quản lý vai trò | desktop | `sso-sysrole-desktop.png` |
| Quản lý vai trò | mobile | `sso-sysrole-mobile.png` |
| Người dùng hệ thống | desktop | `sso-sysuser-desktop.png` |
| Người dùng hệ thống | mobile | `sso-sysuser-mobile.png` |
| Bàn làm việc | desktop | `sso-workspace-desktop.png` |
| Bàn làm việc | mobile | `sso-workspace-mobile.png` |

## MRMS (5232)

| Màn hình / chức năng | Viewport | Tên file |
|---|---|---|
| Tab liên app mẫu: Đăng ký khám ngoại trú (Sett) qua shell MRMS | desktop | `mrms-cross-sett-dangkykham-desktop.png` |
| Tab liên app mẫu: Đăng ký khám ngoại trú (Sett) qua shell MRMS | mobile | `mrms-cross-sett-dangkykham-mobile.png` |
| Trang chủ (shell) | desktop | `mrms-home-desktop.png` |
| Trang chủ (shell) | mobile | `mrms-home-mobile.png` |
| Trang đăng nhập | desktop | `mrms-login-desktop.png` |
| Trang đăng nhập | mobile | `mrms-login-mobile.png` |
| Thông tin cá nhân | desktop | `mrms-personalinfo-desktop.png` |
| Thông tin cá nhân | mobile | `mrms-personalinfo-mobile.png` |
| Menu hệ thống | desktop | `mrms-sysmodule-desktop.png` |
| Menu hệ thống | mobile | `mrms-sysmodule-mobile.png` |
| Quản lý vai trò | desktop | `mrms-sysrole-desktop.png` |
| Quản lý vai trò | mobile | `mrms-sysrole-mobile.png` |
| Người dùng hệ thống | desktop | `mrms-sysuser-desktop.png` |
| Người dùng hệ thống | mobile | `mrms-sysuser-mobile.png` |

## MRQC (5220)

| Màn hình / chức năng | Viewport | Tên file |
|---|---|---|
| Đơn đăng ký hồ sơ kiểm soát chất lượng | desktop | `mrqc-apply-desktop.png` |
| Đơn đăng ký hồ sơ kiểm soát chất lượng | mobile | `mrqc-apply-mobile.png` |
| Form Thêm: Hạng mục kiểm soát chất lượng | desktop | `mrqc-form-sysitem-desktop.png` |
| Form Thêm: Hạng mục kiểm soát chất lượng | mobile | `mrqc-form-sysitem-mobile.png` |
| Trang chủ (menu độc lập) | desktop | `mrqc-home-desktop.png` |
| Trang chủ (menu độc lập) | mobile | `mrqc-home-mobile.png` |
| Trang đăng nhập | desktop | `mrqc-login-desktop.png` |
| Trang đăng nhập | mobile | `mrqc-login-mobile.png` |
| Trung tâm kiểm soát chất lượng | desktop | `mrqc-medicalcenter-desktop.png` |
| Trung tâm kiểm soát chất lượng | mobile | `mrqc-medicalcenter-mobile.png` |
| Tra cứu tin nhắn kiểm soát chất lượng | desktop | `mrqc-messagequery-desktop.png` |
| Tra cứu tin nhắn kiểm soát chất lượng | mobile | `mrqc-messagequery-mobile.png` |
| Quản lý tin nhắn kiểm soát chất lượng | desktop | `mrqc-qcmessagemanage-desktop.png` |
| Quản lý tin nhắn kiểm soát chất lượng | mobile | `mrqc-qcmessagemanage-mobile.png` |
| Chấm điểm kiểm soát chất lượng | desktop | `mrqc-scoreform-desktop.png` |
| Chấm điểm kiểm soát chất lượng | mobile | `mrqc-scoreform-mobile.png` |
| Danh sách nhân sự theo vị trí (JSON) | desktop | `mrqc-stafflist-json-desktop.png` |
| Danh sách nhân sự theo vị trí (JSON) | mobile | `mrqc-stafflist-json-mobile.png` |
| Bảo trì hạng mục kiểm soát chất lượng | desktop | `mrqc-sysitem-desktop.png` |
| Bảo trì hạng mục kiểm soát chất lượng | mobile | `mrqc-sysitem-mobile.png` |

## Sett (5221)

| Màn hình / chức năng | Viewport | Tên file |
|---|---|---|
| Danh mục khu vực | desktop | `sett-area-desktop.png` |
| Danh mục khu vực | mobile | `sett-area-mobile.png` |
| Ví dụ mã QR | desktop | `sett-barcode-sample-desktop.png` |
| Ví dụ mã QR | mobile | `sett-barcode-sample-mobile.png` |
| Ghi sổ nội trú | desktop | `sett-bookkeepinhos-desktop.png` |
| Ghi sổ nội trú | mobile | `sett-bookkeepinhos-mobile.png` |
| Xếp hạng kinh doanh | desktop | `sett-businessranking-desktop.png` |
| Xếp hạng kinh doanh | mobile | `sett-businessranking-mobile.png` |
| Báo cáo theo loại phí | desktop | `sett-chargetypequery-desktop.png` |
| Báo cáo theo loại phí | mobile | `sett-chargetypequery-mobile.png` |
| Xác nhận phát hành hồ sơ điều trị | desktop | `sett-comfirmissue-desktop.png` |
| Xác nhận phát hành hồ sơ điều trị | mobile | `sett-comfirmissue-mobile.png` |
| Bảo hiểm thương mại | desktop | `sett-commercialinsurance-desktop.png` |
| Bảo hiểm thương mại | mobile | `sett-commercialinsurance-mobile.png` |
| Quyết toán ngày | desktop | `sett-daysettle-application-desktop.png` |
| Quyết toán ngày | mobile | `sett-daysettle-application-mobile.png` |
| Đối chiếu sổ sách ngày | desktop | `sett-daysettle-dzr-desktop.png` |
| Đối chiếu sổ sách ngày | mobile | `sett-daysettle-dzr-mobile.png` |
| Sao lưu cơ sở dữ liệu | desktop | `sett-dbbackup-desktop.png` |
| Sao lưu cơ sở dữ liệu | mobile | `sett-dbbackup-mobile.png` |
| Quyết toán ra viện | desktop | `sett-dischargesettle-desktop.png` |
| Quyết toán ra viện | mobile | `sett-dischargesettle-mobile.png` |
| Upload DRG | desktop | `sett-drgupload-desktop.png` |
| Upload DRG | mobile | `sett-drgupload-mobile.png` |
| Xác nhận phí | desktop | `sett-feeconfirm-desktop.png` |
| Xác nhận phí | mobile | `sett-feeconfirm-mobile.png` |
| Form Thêm: Danh mục khu vực | desktop | `sett-form-area-desktop.png` |
| Form Thêm: Danh mục khu vực | mobile | `sett-form-area-mobile.png` |
| Form Thêm: Bảo hiểm thương mại | desktop | `sett-form-commercialinsurance-desktop.png` |
| Form Thêm: Bảo hiểm thương mại | mobile | `sett-form-commercialinsurance-mobile.png` |
| Form Thêm: Sao lưu cơ sở dữ liệu | desktop | `sett-form-dbbackup-desktop.png` |
| Form Thêm: Sao lưu cơ sở dữ liệu | mobile | `sett-form-dbbackup-mobile.png` |
| Form Thêm: Cơ sở tri thức BHYT | desktop | `sett-form-medicalinsurance-kb-desktop.png` |
| Form Thêm: Cơ sở tri thức BHYT | mobile | `sett-form-medicalinsurance-kb-mobile.png` |
| Form Thêm: Danh mục tổ chức | desktop | `sett-form-organize-desktop.png` |
| Form Thêm: Danh mục tổ chức | mobile | `sett-form-organize-mobile.png` |
| Form Thêm: Đặt lịch khám | desktop | `sett-form-outbook-desktop.png` |
| Form Thêm: Đặt lịch khám | mobile | `sett-form-outbook-mobile.png` |
| Form Thêm: Thống kê site | desktop | `sett-form-sitestatistics-desktop.png` |
| Form Thêm: Thống kê site | mobile | `sett-form-sitestatistics-mobile.png` |
| Ví dụ biểu đồ | desktop | `sett-highcharts-sample-desktop.png` |
| Ví dụ biểu đồ | mobile | `sett-highcharts-sample-mobile.png` |
| Trang chủ (menu độc lập) | desktop | `sett-home-desktop.png` |
| Trang chủ (menu độc lập) | mobile | `sett-home-mobile.png` |
| Tiếp nhận nội trú | desktop | `sett-hospiterres-desktop.png` |
| Tiếp nhận nội trú | mobile | `sett-hospiterres-mobile.png` |
| Upload JG | desktop | `sett-jgupload-desktop.png` |
| Upload JG | mobile | `sett-jgupload-mobile.png` |
| Trang đăng nhập | desktop | `sett-login-desktop.png` |
| Trang đăng nhập | mobile | `sett-login-mobile.png` |
| Cơ sở tri thức BHYT | desktop | `sett-medicalinsurance-kb-desktop.png` |
| Cơ sở tri thức BHYT | mobile | `sett-medicalinsurance-kb-mobile.png` |
| KPI tổ chức y tế | desktop | `sett-medicalorg-kpi-desktop.png` |
| KPI tổ chức y tế | mobile | `sett-medicalorg-kpi-mobile.png` |
| Thanh toán vi mô | desktop | `sett-micropayquery-desktop.png` |
| Thanh toán vi mô | mobile | `sett-micropayquery-mobile.png` |
| Ghi sổ hạng mục không điều trị | desktop | `sett-nontreatment-bookkeeping-desktop.png` |
| Ghi sổ hạng mục không điều trị | mobile | `sett-nontreatment-bookkeeping-mobile.png` |
| Danh mục tổ chức | desktop | `sett-organize-desktop.png` |
| Danh mục tổ chức | mobile | `sett-organize-mobile.png` |
| Đặt lịch khám | desktop | `sett-outbook-desktop.png` |
| Đặt lịch khám | mobile | `sett-outbook-mobile.png` |
| Thu phí ngoại trú | desktop | `sett-outpatcharge-desktop.png` |
| Thu phí ngoại trú | mobile | `sett-outpatcharge-mobile.png` |
| Trung tâm bệnh nhân | desktop | `sett-patientcenter-desktop.png` |
| Trung tâm bệnh nhân | mobile | `sett-patientcenter-mobile.png` |
| Danh sách bệnh nhân nội trú | desktop | `sett-patientinhoslist-desktop.png` |
| Danh sách bệnh nhân nội trú | mobile | `sett-patientinhoslist-mobile.png` |
| Biểu đồ chỉ số hiệu suất | desktop | `sett-performanceindicator-desktop.png` |
| Biểu đồ chỉ số hiệu suất | mobile | `sett-performanceindicator-mobile.png` |
| Hoàn phí | desktop | `sett-refund-desktop.png` |
| Hoàn phí | mobile | `sett-refund-mobile.png` |
| Giám sát máy chủ | desktop | `sett-servermonitoring-desktop.png` |
| Giám sát máy chủ | mobile | `sett-servermonitoring-mobile.png` |
| Quyết toán | desktop | `sett-settlement-desktop.png` |
| Quyết toán | mobile | `sett-settlement-mobile.png` |
| Thống kê site | desktop | `sett-sitestatistics-desktop.png` |
| Thống kê site | mobile | `sett-sitestatistics-mobile.png` |
| Y lệnh trị liệu | desktop | `sett-therapistadvice-desktop.png` |
| Y lệnh trị liệu | mobile | `sett-therapistadvice-mobile.png` |

## OR (5231)

| Màn hình / chức năng | Viewport | Tên file |
|---|---|---|
| Mẫu thu phí | desktop | `or-chargetemplate-desktop.png` |
| Mẫu thu phí | mobile | `or-chargetemplate-mobile.png` |
| Form Thêm: Mẫu thu phí | desktop | `or-form-chargetemplate-desktop.png` |
| Form Thêm: Mẫu thu phí | mobile | `or-form-chargetemplate-mobile.png` |
| Form Sửa (có dữ liệu): Phương pháp gây mê | desktop | `or-form-edit-opanesthesia-desktop.png` |
| Form Sửa (có dữ liệu): Phương pháp gây mê | mobile | `or-form-edit-opanesthesia-mobile.png` |
| Form Sửa (có dữ liệu): Cấp độ vết mổ | desktop | `or-form-edit-opnotchgrade-desktop.png` |
| Form Sửa (có dữ liệu): Cấp độ vết mổ | mobile | `or-form-edit-opnotchgrade-mobile.png` |
| Form Sửa (có dữ liệu): Quản lý phẫu thuật | desktop | `or-form-edit-opoperation-desktop.png` |
| Form Sửa (có dữ liệu): Quản lý phẫu thuật | mobile | `or-form-edit-opoperation-mobile.png` |
| Form Thêm: Phương pháp gây mê | desktop | `or-form-opanesthesia-desktop.png` |
| Form Thêm: Phương pháp gây mê | mobile | `or-form-opanesthesia-mobile.png` |
| Form Thêm: Cấp độ vết mổ | desktop | `or-form-opnotchgrade-desktop.png` |
| Form Thêm: Cấp độ vết mổ | mobile | `or-form-opnotchgrade-mobile.png` |
| Form Thêm: Quản lý phẫu thuật | desktop | `or-form-opoperation-desktop.png` |
| Form Thêm: Quản lý phẫu thuật | mobile | `or-form-opoperation-mobile.png` |
| Form Thêm: Quản lý phòng mổ | desktop | `or-form-oproom-desktop.png` |
| Form Thêm: Quản lý phòng mổ | mobile | `or-form-oproom-mobile.png` |
| Form Thêm: Nhân sự phẫu thuật | desktop | `or-form-opstaff-desktop.png` |
| Form Thêm: Nhân sự phẫu thuật | mobile | `or-form-opstaff-mobile.png` |
| Trang chủ (menu độc lập) | desktop | `or-home-desktop.png` |
| Trang chủ (menu độc lập) | mobile | `or-home-mobile.png` |
| Trang đăng nhập | desktop | `or-login-desktop.png` |
| Trang đăng nhập | mobile | `or-login-mobile.png` |
| Phương pháp gây mê | desktop | `or-opanesthesia-desktop.png` |
| Phương pháp gây mê | mobile | `or-opanesthesia-mobile.png` |
| Đăng ký phẫu thuật | desktop | `or-opapply-apply-desktop.png` |
| Đăng ký phẫu thuật | mobile | `or-opapply-apply-mobile.png` |
| Danh sách bệnh nhân nội trú chờ đăng ký phẫu thuật | desktop | `or-opapply-list-desktop.png` |
| Danh sách bệnh nhân nội trú chờ đăng ký phẫu thuật | mobile | `or-opapply-list-mobile.png` |
| Sắp xếp phẫu thuật | desktop | `or-oparrangement-desktop.png` |
| Sắp xếp phẫu thuật | mobile | `or-oparrangement-mobile.png` |
| Báo cáo chi phí phẫu thuật | desktop | `or-opfeereport-desktop.png` |
| Báo cáo chi phí phẫu thuật | mobile | `or-opfeereport-mobile.png` |
| Cấp độ vết mổ | desktop | `or-opnotchgrade-desktop.png` |
| Cấp độ vết mổ | mobile | `or-opnotchgrade-mobile.png` |
| Quản lý phẫu thuật | desktop | `or-opoperation-desktop.png` |
| Quản lý phẫu thuật | mobile | `or-opoperation-mobile.png` |
| Đăng ký sau mổ | desktop | `or-opregister-desktop.png` |
| Đăng ký sau mổ | mobile | `or-opregister-mobile.png` |
| Sổ đăng ký phẫu thuật | desktop | `or-opregisterreport-desktop.png` |
| Sổ đăng ký phẫu thuật | mobile | `or-opregisterreport-mobile.png` |
| Quản lý phòng mổ | desktop | `or-oproom-desktop.png` |
| Quản lý phòng mổ | mobile | `or-oproom-mobile.png` |
| Nhân sự phẫu thuật | desktop | `or-opstaff-desktop.png` |
| Nhân sự phẫu thuật | mobile | `or-opstaff-mobile.png` |
| Báo cáo phòng mổ | desktop | `or-report-desktop.png` |
| Báo cáo phòng mổ | mobile | `or-report-mobile.png` |

## Màn hình bỏ qua kèm lý do

### 1. Mục menu liên app qua shell SSO / MRMS — 176 mục (chỉ chụp 1 mẫu mỗi app)

SSO (93 mục menu) và MRMS (92 mục) dùng chung shell menu thống nhất. Ngoài các mục
own-app, các mục còn lại (SSO: EMR 3 + SETT 46 + CIS 39 = 88; MRMS: tương tự ≈ 88)
khi bấm sẽ mở tab iframe trỏ về **host gốc của vendor** (`SiteBaseWebHost 8001`,
`SiteSettWebHost 8002`, `SiteCisWebHost 8003` trong cấu hình SSO), không phải cổng
VN-Web (5220–5238) — các cổng này không nghe (đã kiểm tra curl 000) nên màn hình
đích không mở được trong đợt này. Đã chụp 1 ảnh mẫu tab launcher mỗi app
(`sso-cross-sett-dangkykham-*`, `mrms-cross-sett-dangkykham-*`). Các màn hình đích
này thuộc nhóm app tương ứng (EMR / SETT / CIS), không phải khoảng trống của chặng này.

### 2. Trang MRMS trả 404 — 3 màn hình × 2 viewport = 6 ảnh (đã chụp đúng trạng thái 404)

- `mrms-sysmodule-*`, `mrms-sysrole-*`, `mrms-sysuser-*`: route không tồn tại trên
  MRMS; SSO mới phục vụ các view Sys* (đúng như ghi chú porting trước đó). 6 ảnh này
  là trang 404 giống hệt nhau, **không** tính là màn hình chức năng; tham chiếu chéo
  sang ảnh SSO tương ứng (`sso-sysmodule-*`, `sso-sysrole-*`, `sso-sysuser-*`).

### 3. Form / dialog không mở được dù đã thử nhiều lần — 11 chức năng

| Chức năng | Lý do (đã kiểm chứng) |
|---|---|
| OR — Form Sửa: Quản lý phòng mổ, Nhân sự phẫu thuật (×2 viewport) | Form mở trống dù dùng **đúng khóa mà nút Sửa của app dùng** (`jqGridRowValue().Id`, GUID thật, đã thử 2 lần). Form action không nạp được dữ liệu cho các bản ghi này. |
| Sett — Form Sửa: Đặt lịch khám (Form update + Dialog Sửa bác sĩ trực), Bảo hiểm thương mại, Cơ sở tri thức BHYT, Danh mục khu vực, Danh mục tổ chức, Thống kê site (×2 viewport) | Lưới rỗng trong DB demo — ảnh chính xác nhận “Không tìm thấy dữ liệu liên quan!” (ví dụ `sett-organize-*`, `sett-area-*`) nên không có dòng để mở form Sửa. Đã thử 2 lần (forms4 + forms5-sett, chờ 10s). |
| Sett — Form Sửa: Sao lưu cơ sở dữ liệu | **Không tồn tại**: mã nguồn DbBackup chỉ có Thêm / Xóa / Tải về, không có nút Sửa. |
| MRQC — Form Sửa: Hạng mục kiểm soát chất lượng (×2 viewport) | Lưới chi tiết phải rỗng: cây trái chỉ có 6 nút cha (`isLeaf=false`, không có nút lá) nên không nạp được dòng chi tiết. Đã thử 4 lần (forms3, forms4, forms5, forms5b + probe nút lá). |
| MRQC — Dialog Chấm điểm chất lượng, MedicalCenter (×2 viewport) | Danh sách bệnh nhân rỗng trong DB demo → không chọn được bệnh nhân / bệnh án để mở dialog `ScoreFrom`. Không bịa dữ liệu. |

### 4. Trang không có form — không phải khoảng trống (ghi để khỏi tìm lại)

- **MRQC**: MessageQuery, QcMessageManage là trang tra cứu / quản lý tin nhắn, không có
  `modalOpen` Form trong mã nguồn. ScoreFrom: bản thân trang đã là form chấm điểm
  (ảnh chính đã chụp). Apply: chỉ có lưới + nút Duyệt là thao tác **ghi** — không bấm
  theo quy tắc chỉ-chụp. StaffList-JSON là endpoint JSON, không có UI/form.
- **OR**: OpApply-List / OpApply-Apply, OpArrangement, OpRegister: “form” render ra JSON
  null hoặc lỗi “Thông tin bệnh nhân không được để trống” / “Object reference not set”
  — cần bệnh nhân / ca phẫu thuật thật, không bịa. Report / OpFeeReport /
  OpRegisterReport là trang báo cáo, không có Form trong mã nguồn.
- **Sett**: 24 trang còn lại (hoàn phí, thu phí, ghi sổ, quyết toán, báo cáo, upload,
  giám sát…) không quét được URL Form Thêm nào render được — phần lớn là trang nghiệp
  vụ / tra cứu / báo cáo mà “form” chính là bản thân trang hoặc cần dữ liệu nghiệp vụ
  thật. Riêng 7 trang danh mục / hệ thống đã có form Thêm (mục trên).
- **SSO**: WorkSpace là dashboard (không có Form); Notice là lưới tin nhắn (không có
  URL Form trong mã trang). Trang Người dùng hệ thống không có form Thêm — chỉ có
  dialog Vai trò người dùng (đã chụp: `sso-form-userroles-*`).

### 5. Ảnh đã xóa vì nhãn sai (không tính vào tổng)

10 ảnh “Form Sửa” OR từ lần chạy forms3 đã bị **xóa** sau khi kiểm chứng: chúng là
form Thêm trống (khóa dòng “1” chỉ là chỉ số dòng jqGrid, không phải khóa bản ghi —
hai ảnh Thêm/Sửa giống hệt nhau từng byte). 6 ảnh Sửa OR hiện tại là bản thật (khóa
GUID, đã kiểm chứng form nạp được 2–4 field có dữ liệu). Tuyệt đối không ghi nhãn
“Sửa” cho ảnh form trống.

## Phương pháp form / dialog (để tái hiện)

- Trang lưới standalone load `framework-ui.js` (`$.modalOpen` tồn tại) nhưng **không**
  load `dialog.js` (layer): `$.modalOpen` gọi `top.layer.open(...)` → lỗi
  `top.layer is undefined` → nút “tạo mới” bấm vào không hiện gì. Vì vậy form được
  chụp bằng cách mở **trực tiếp URL Form** trích từ mã nguồn trang (chỉ xem, không
  submit); dialog layer thật chỉ chụp được qua tab shell SSO (đã chụp đủ SysRole +
  SysModule, cả 2 viewport — riêng SysModule mobile phải dùng JS-click vì
  Playwright click timeout 2 lần).
- Form Sửa chỉ được lưu khi form **nạp được dữ liệu** (≥ 1 field có giá trị); form mở
  trống không được ghi nhãn “Sửa có dữ liệu”.
- Lưới rỗng được chụp trung thực trạng thái rỗng, không bịa dữ liệu.
- **Không dựng instance STG nào trong chặng này — mọi ảnh đều chụp trên prod.**
  Cuối phiên đã xác nhận không còn listener STG nào của chặng này (không tạo mới).

## Sự cố môi trường trong phiên (ghi trung thực)

- **SQL Server chập chờn** (1433 không nghe nhiều lần trong tối 2026-10-28 UTC):
  login thất bại hàng loạt với thông báo chung chung; log BaseAPI chứng minh nguyên
  nhân là lỗi kết nối DB, không phải khóa tài khoản. Người chạy đã khởi động lại SQL
  một lần bằng đúng phương pháp của recovery.sh §2 (2026-10-28 ~21:47 UTC, 1433 lên
  lại); watchdog 30 phút cũng tự hồi phục SQL/Sett các lần khác.
- **App Sett 5221 và OR 5231 chập chờn** (curl 000 hoặc treo giữa lần chạy). Lần chạy
  Sett mobile forms4 bị kẹt ~14 phút khi Sett sập → đã giết và chạy lại sau khi app
  hồi, đủ 7 form Thêm mobile.
- **Tab Firefox crash** trên mobile ở các trang báo cáo OR trong lần chạy forms3
  (chargetemplate, report, opfeereport, opregisterreport) — ảnh chính các trang này
  không bị ảnh hưởng; form Thêm Mẫu thu phí mobile đã chụp lại thành công sau đó.
- **Browser chết khi tạo mobile context** trong forms4 đầu tiên (MRQC, OR) làm mất
  manifest — đã sửa script (try/except quanh new_context + lưu manifest sau mỗi
  viewport) và chạy lại.
- Lần chạy SSO v1 đầu phiên cho ảnh tab chỉ có spinner (7KB) — đã sửa bằng goto trực
  tiếp URL own-app và chụp lại đầy đủ.
- Ghi chú phụ (quan sát được, không thuộc phạm vi sửa): thông báo login thất bại trên
  MRQC và Sett vẫn là tiếng Trung trong khi SSO đã dịch.

## Tài liệu tham chiếu

- Manifest các lần chạy: `~/workspace/shot/s1_manifest_{sso,mrms,mrqc,sett,or}.json`
  (ảnh chính), `s1_manifest_{sso,mrms,mrqc,or}_forms3.json`,
  `s1_manifest_{sso,sett}_forms4.json`, `s1_manifest_{sso,mrqc,or}_forms5.json`,
  `s1_manifest_forms5b.json`, `s1_manifest_sett_forms5.json`.
- Script chụp: `~/workspace/shot/s1_shoot2.py` (ảnh chính), `s1_forms3.py`,
  `s1_forms4.py`, `s1_forms5.py`, `s1_forms5_sett.py`, `s1_forms5b.py`,
  `s1_sysitem_leaf.py`, `s1_dlg_sysmodule_mobile.py`.
