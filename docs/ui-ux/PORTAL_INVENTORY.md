# Danh mục portal — OpenHIS Visual UI/UX Modernization

> Chương trình: hiện đại hóa GIAO DIỆN 11 portal MVC, chỉ đổi trình bày, giữ nguyên 100% chức năng.
> Ngày khảo sát Phase 1: 2026-11-04 (chỉ đọc, không sửa source). Repo `openhis-core`, nhánh V2.2, HEAD `e69f1b03`.
> Bằng chứng chi tiết: `~/workspace/uiux-phase1/inventory-sso-cis-emr.md` (711 dòng),
> `~/workspace/uiux-phase1/inventory-herp-mrms-or.md` (645 dòng),
> `~/workspace/uiux-phase1/inventory-pds-mrqc-sett-base.md` (459 dòng).
> Mọi số lượng dưới đây đều đếm bằng lệnh (`find`/`grep`/`wc`), không ước lượng.

## 1. Bảng tóm tắt 11 portal

| # | Portal | Thư mục Web trong repo | Cổng | `.cshtml` | Area | Shell trang chủ | Đăng nhập |
|---|---|---|---|---|---|---|---|
| 1 | SSO | `Newtouch.HIS.SSO/HIS.SSO` | 5208 | 30 | 1 (SysManage, 10 view) | Có, AdminLTE + `SSOIndex.cshtml` (bản redesign) | `Views/Login/Index.cshtml` (180 dòng, `login.css` cục bộ) |
| 2 | CIS | `Newtouch.HIS.CIS/Newtouch.CIS.Web` | 5234 | 311 | 7 (225 view) | Không có — chạy trong iframe của shell SSO | `Views/Login/Index.cshtml` (215 dòng, CSS từ host tĩnh) |
| 3 | EMR | `Newtouch.HIS.EMR/Newtouch.EMR.Web` | 5235 | 122 | 3 (103 view) | Có, AdminLTE + `Default.cshtml` dashboard (405 dòng) | `Views/Login/Index.cshtml` (220 dòng, CSS từ host tĩnh) |
| 4 | Herp | `Newtouch.HIS.Herp/Newtouch.Herp.Web` | 5236 | 110 | 10 | Không có khung riêng — dashboard KPI độc lập (343 dòng), dựa vào SSO | `Views/Login/Index.cshtml` (215 dòng) |
| 5 | MRMS | `Newtouch.HIS.MRMS/Newtouch.MRMS.Web` | 5232 | 64 | 4 | Có, khung AdminLTE đầy đủ duy nhất (header + sidebar + tab/iframe + đổi skin) | `Views/Login/Index.cshtml` (218 dòng) |
| 6 | OR | `Newtouch.HIS.OR/Newtouch.OR.Web` | 5231 | 42 | 3 | Không có — trang đích liệt kê link (65 dòng), dựa vào SSO | `Views/Login/Index.cshtml` (218 dòng) |
| 7 | PDS | `Newtouch.HIS.PDS/Newtouch.PDS.Web` | 5237 | 184 | 5 (85 view) | Có, AdminLTE (`#theme-wrapper`, `#sidebar-nav`, `.content-tabs`, `iframe.Newtouch_iframe`) | `Views/Login/Index.cshtml` (199 dòng) |
| 8 | MRQC | `Newtouch.MRQC.Web` | 5220 | 23 | 2 (14 view) | Không có — `Home/Index` chỉ là trang danh sách link (30 dòng) | `Views/Login/Index.cshtml` (218 dòng) |
| 9 | Sett | `Newtouch.Sett.Web` (ở root repo) | 5221 | 507 | 22 (484 view) | Không có — `Home/Index` chỉ là trang danh sách link (60 dòng) | `Views/Login/Index.cshtml` (225 dòng) |
| 10 | Base Web | `Newtouch.HIS.Base/Newtouch.HIS.Base.Web` | 5238 | 51 | 0 | Có, shell AdminLTE | `Views/Login/Index.cshtml` (185 dòng) |
| 11 | Base HOSP Web | `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web` | 5233 | 184 | 5 (74 view) | Có, shell AdminLTE | `Views/Login/Index.cshtml` (215 dòng) |

Tổng: **1.564 file `.cshtml`**. Portal lớn nhất là Sett (507), tiếp theo là CIS (311). Base Web "sạch" nhất (51 view, không Area).

Ngoài 11 portal còn 12 host API/dịch vụ cùng chạy net6.0 (BaseAPI 7001, Sett.API 7002, 8 API module, AuthCenter 8015, WebAPI.Manage 8016) — các host này không có giao diện Razor, ngoài phạm vi modernize.

## 2. Kiến trúc giao diện thật (5 kết luận quyết định cách làm)

1. **Không có `_Layout.cshtml` truyền thống.** `_ViewStart.cshtml` ở hầu hết portal đặt `Layout = null`. Mọi view nghiệp vụ tự khai `Layout = "~/Views/Shared/_Index.cshtml"` (trang danh sách + jqGrid) hoặc `"~/Views/Shared/_Form.cshtml"` (trang form). Hai file này chỉ là `<head>` + `@RenderBody()`, không chứa header/sidebar/menu/footer. Ngoại lệ duy nhất: SSO đặt `Layout = "_Layout"` trong `_ViewStart` gốc, nên view nào không khai Layout sẽ rơi vào scaffold Bootstrap 5 — bẫy khi sửa.
2. **Shell nằm trong `Home/Index.cshtml` từng portal**, không nằm trong layout. Chỉ SSO, EMR, MRMS, PDS, Base Web, Base HOSP có shell đầy đủ; CIS/Herp/OR/MRQC/Sett không có shell riêng (CIS chạy trong iframe của SSO; MRQC/Sett chỉ có trang link). Các file `_Layout.cshtml` theo Area và `_LayoutIndex.cshtml` (37 dòng) đều là file chết — 0 view dùng, mọi view đều override.
3. **CSS/JS khung không nằm trong repo.** Gần như toàn bộ CSS/JS dùng chung (`framework-ui.css`, `framework-theme.css`, `framework-font.css`, `common.css`, Bootstrap, jQuery, jqGrid, `framework-ui.js`, `Newtouch-ui.js`, …) được nạp qua `MvcHelper.GetStaticResourceScriptUrl(...)` sang **static host riêng `http://127.0.0.1:41142/`** (project `Newtouch.HIS.Static`, ngoài repo `openhis-core`). Sửa `wwwroot` của portal không chạm tới các file này. Sửa static host thì ảnh hưởng đồng thời mọi portal và đổi cả prod ngay — **cấm sửa static host trong chương trình này**.
4. **Điểm đặt design system trong repo là cặp file app-local giống hệt nhau ở cả 11 portal**: `wwwroot/css/redesign-tokens.css` (193 dòng, md5 `e14e0ef8f7188b552207c81ec8eab45b` ở cả 11 portal, đã kiểm chứng) và `wwwroot/js/redesign-grid.js` (261 dòng), cùng `js/jqgrid/grid.locale-vi.js` và file `*-vi.js` riêng từng portal. Cả 22 file `_Index.cshtml`/`_Form.cshtml` đều tham chiếu `redesign-tokens.css` đúng 1 lần, nạp SAU `framework-theme.css`, app-local có `?V=` chống cache. Nguồn chuẩn duy nhất của các file này là `scripts/redesign/assets/`, đồng bộ bằng `scripts/redesign/sync-assets.sh` — **không sửa tay bản trong `wwwroot`**, sửa ở `assets/` rồi sync. SSO còn có thêm `redesign-portal.css` (109 dòng) + `redesign-portal.js` (70 dòng) cho shell.
5. **Stack cố định, cũ, đóng**: Bootstrap **3.3.4**, jQuery **2.1.1**, jqGrid **5.1.0**, jQuery UI 1.10.4, FontAwesome 4.7.0 (OR thêm bootstrap-table v1.22.1; EMR thêm EasyUI + ribbon + Summernote 0.8.6 + DCSoft). Mọi class Bootstrap 4/5 đều không dùng được nếu không đổi stack — mà đổi stack là đổi chức năng, ngoài phạm vi. Không có bundle/minify, không có CDN cho CSS/JS lõi (ngoại lệ: 1 link Font Awesome 6.0.0 từ cdnjs trong CIS `_FileUpload.cshtml`; EMR `Default.cshtml` có iframe thời tiết `http://i.tianqi.com/...`).

## 3. Menu và điều hướng

Menu **không render từ Razor**. Cơ chế chung: DB `Sys_Module` → BaseAPI → JSON `authorizeMenu` → JS `$.LoadNavTree()` (trong `Newtouch-ui.js` ở static host) dựng HTML vào `#sidebar-nav` rỗng trong shell. Đổi CSS của menu phải giữ nguyên các class/ID mà JS sinh ra (`dropdown-toggle`, `submenu`, `menuItem`, `data-id`, …). Base Web là trường hợp đặc biệt: `ClientsDataController` không trả `authorizeMenu` — nguồn menu khi chạy chưa xác định được từ đọc tĩnh, cần kiểm chứng runtime ở Phase 2 (ghi nhận, không đoán).

## 4. Điểm riêng từng portal cần giữ

- **SSO**: shell + `SSOIndex.cshtml` là bản redesign đã có (343 dòng, route riêng `/Home/SSOIndex`, dùng riêng Bootstrap 3.4.1 trong thư mục `3.4.1` của static host); `_ViewStart` rơi vào scaffold Bootstrap 5 nếu view thiếu khai Layout; có thêm `redesign-portal.css/js`.
- **CIS**: lớn thứ hai (311 view, 7 Area); view form/danh sách dùng chung `_Index` 128 / `_Form` 89; Area DoctorManage có `_Layout.cshtml` duy nhất khác md5 (jQuery 1.10.2 chuyển lên `<head>` vì script inline dùng `$` ngay trong body — không được phá thứ tự này).
- **EMR**: thêm EasyUI/ribbon/Summernote/DCSoft (`dc_main.js`); `Default.cshtml` dashboard có iframe thời tiết ngoài; 67 template y khoa + file backup — trang in/template không thuộc diện modernize tương tác.
- **Herp**: dashboard KPI + Highcharts; 5 view `*InlineEdit` có màu hardcode nội tuyến; `_BottomButtonsView`/`_MiddleButtonsView` (phím F1–F12); không có `Views/_ViewStart.cshtml` gốc.
- **MRMS**: khung AdminLTE đầy đủ duy nhất + đổi skin qua `localStorage` + `id="color-skin"`; cụm hồ sơ bệnh án `MainRecord` 11 file/2.697 dòng dạng tab/partial lồng nhau; nhiều báo cáo SSRS.
- **OR**: bootstrap-table + `_PatientCard.cshtml` + `card.css` + phiếu in phẫu thuật; trang chủ chỉ liệt kê link.
- **PDS**: portal duy nhất **không nạp jQuery core trong layout** (chỉ `jquery.choose.js`, 61 dòng, không chứa jQuery) — bất thường ghi nhận ở Phase 1, phải xác minh runtime trước khi kết luận là lỗi; không sửa theo suy đoán.
- **MRQC**: nhỏ (23 view), không shell, `Home/Index` 30 dòng.
- **Sett**: lớn nhất (507 view, 22 Area; `SystemManage` 108 view, `ReportManage/Report` 94 view SSRS); có `_ChartForm`/`_IndexV` (đều chết), `_PrintCommonView`, `_YibaoCommonView`; trang chủ không shell.
- **Base Web**: sạch nhất (51 view, không Area); nguồn menu runtime chưa rõ (xem §3).
- **Base HOSP Web**: bản mở rộng trùng lặp của Base Web (184 view, Area `Sys` trùng tên view), thêm `_crossDomain`, ActiveX BHYT Guian, 29 URL BHYT hardcode — phần BHYT Guizhou không động vào.

## 5. Rủi ro đã biết khi đổi CSS (tóm tắt — chi tiết đầy đủ trong DESIGN_SYSTEM.md §6)

- Sửa `wwwroot` không chạm tới CSS khung (nằm ở static host); sửa static host ảnh hưởng toàn hệ thống — chốt cơ chế assets + sync.
- JS bám chặt class/ID CSS (menu/tab/grid/validate/skin); đổi tên hoặc `display:none` các selector này là vỡ chức năng.
- Thứ tự nạp CSS là "hợp đồng ngầm", không đồng nhất giữa `_Index` và `_Form`, có file nạp lặp 2 lần; `redesign-tokens.css` ở vị trí khác nhau giữa hai layout.
- Tài nguyên static-host không có `?V=` nên dễ bị cache cũ; tài nguyên app-local có `?V=` qua `MvcHelper`.
- Nhãn tiếng Việt cần `formTitle` 210px (mặc định 90px); `line-height` tương đối để dấu xếp chồng (ế, ữ, ộ, ấ) không bị cắt.
- Inline style rải rác trong view; `id="color-skin"` của cơ chế đổi skin; DOM jqGrid sinh động sau render; 2 file lạ không phần mở rộng trong `Views/Login/` của Base Web/Base HOSP (`XXcf5Y9E`, `XXsyzLsA`, ~10,7 KB) — chỉ ghi nhận, không mở/không động vào, cần kiểm tra nguồn gốc riêng ngoài chương trình này.
- Hai bản sao của `Newtouch.HIS.Static` (`lmsoft_openhis/` 471 file và `lmsoft_openhis_run/` 472 file) khác nhau 32 file (config, skin/IE, `CreateControl.js`, `Newtouch-ui.source.js`, 1 file chỉ có ở run-copy); các file lõi chỉ khác comment tiếng Trung/tiếng Việt, cùng số dòng/selector/mã màu. Phase 2 phải đối chiếu với host 41142 đang chạy để biết bản nào đang được phục vụ — chưa kết luận.
