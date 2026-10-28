# Báo cáo chặng 3-TIẾP — Nối `_Index` + tự động bọc lưới (index-wire)

> Worker chặng 3-tiếp, ngày 2026-10-28 (ICT). **KHÔNG commit, KHÔNG
> deploy prod.** Mọi thay đổi chỉ nằm trong repo `openhis-core` dưới
> dạng chưa commit + build scratch Debug ngoài repo. Mobile = giả lập
> viewport (Playwright + Firefox local), không phải app native.
> Mật khẩu chỉ truyền qua biến môi trường tạm thời của phiên probe,
> không ghi vào file, báo cáo, log hay ảnh.

## 0. Kết luận

| Hạng mục | Kết quả |
|---|---|
| (1) Nối `_Index` vào nền tảng | ✅ ĐẠT — 10/10 file `_Index` còn thiếu đã nối (663 view), thuần cộng 27 dòng, BOM giữ nguyên |
| (2) Tự động bọc `.rd-grid-scroll` | ✅ ĐẠT trên trang `_Index` và `_Form` đã probe (xem §4) |
| Phát hiện ngoài brief, đã sửa trong chặng | ⚠️ Helper redesign **chết hoàn toàn trên mọi trang `_Form`** từ thời foundation (xem §3) — đã sửa bằng deferred-init, kiểm chứng live |
| Cột neo (frozen) | ⏳ CHƯA kiểm chứng live chặng này — 0 lưới PDS khai `frozen:true` nên không có gì để kích hoạt; code chỉ kích hoạt khi colModel đã khai (không bịa cột) |
| Cuộn ngang với tràn lớn | ⚠️ CHƯA chứng minh trên trang đã probe — wrapper có mặt + cuộn được, nhưng lưới tự co về viewport nên tràn thực tế chỉ 2px (xem §4.1) |
| Layout scaffold khu vực (22 view) | ⏳ KHÔNG nối trong chặng này, cần điều phối viên chốt (xem §5) |

## 1. Bảng quét layout × app (đếm từ source, không ước lượng)

Cách đếm: quét mọi `.cshtml` (trừ `wwwroot`, `bin`, `obj`,
`_porting-src`, thư mục `*-tmp`, `sett-stage-*`) và đếm câu lệnh
`Layout = "..."` trực tiếp; view không gán `Layout` mà nằm dưới một
`_ViewStart` trỏ tới layout khu vực thì đếm vào layout khu vực đó.
File layout/partial trong `Shared/` không tính là view.

### 1.1. `_Index` — layout lưới chủ đạo, trước chặng này chỉ SSO có nối

| App | View dùng `_Index` | Trước chặng | Sau chặng |
|---|---|---|---|
| SSO 5208 | 5 | ✅ đã nối (foundation) | không đổi |
| MRQC 5220 | 5 | ❌ chưa | ✅ nối |
| Sett 5221 | 177 | ❌ chưa | ✅ nối |
| MRMS 5232 | 27 | ❌ chưa | ✅ nối |
| OR 5231 | 16 | ❌ chưa | ✅ nối |
| Herp 5236 | 66 | ❌ chưa | ✅ nối |
| EMR 5235 | 45 | ❌ chưa | ✅ nối |
| Base HOSP 5233 | 68 | ❌ chưa | ✅ nối |
| Base Web 5238 | 16 | ❌ chưa | ✅ nối |
| CIS 5234 | 128 | ❌ chưa | ✅ nối |
| PDS 5237 | 115 | ❌ chưa | ✅ nối |
| **Tổng mới nối** | **663** | | |

Ví dụ PDS: `PharmacyDrugStorage/StockQuery`, `Dictionaries/FypzIndex`
và 113 trang Index/query khác. `_LayoutIndex` mà foundation nối trước
đây thực tế chỉ có **8 view dùng trực tiếp** (Sett 3, Base HOSP 2, Base Web 1,
CIS 2); 6 app còn lại có file `_LayoutIndex` nhưng 0 view nào dùng —
giữ nguyên nối foundation, không đụng tới.

### 1.2. `_Form` — đã nối từ foundation, nhưng helper chết (xem §3)

| App | View dùng `_Form` | Trạng thái |
|---|---|---|
| SSO 6, MRQC 3, Sett 261, MRMS 6, OR 6, Herp 21, EMR 27, Base HOSP 87, Base Web 21, CIS 89, PDS 44 (tổng 571) | Đã nối link/script từ foundation | Tag script có mặt nhưng `window.Redesign` = undefined trên mọi trang `_Form` do thứ tự nạp (§3); sau sửa deferred-init, probe live xác nhận `Redesign` có mặt + wrapper xuất hiện (§4) |

### 1.3. Các layout khác — liệt kê đủ, không nối bừa

| Layout | View dùng | Quyết định chặng này |
|---|---|---|
| `_LayoutIndex` (10 file, foundation đã nối) | 8 view trực tiếp (bảng §1.1) | Giữ nguyên, không sửa |
| `Layout = null` (trang tự mang `<head>`: Home, Login, trang độc lập) | SSO 4, MRQC 2, Sett 12, MRMS 11, OR 6, Herp 0, EMR 18, Base HOSP 4, Base Web 3, CIS 24, PDS 5 (tổng 89) | Không nối — sửa per-page ở chặng sau, như foundation đã kết luận cho trang Home |
| SSO `SSOIndex` (portal, `Layout = null`) | 1 trang portal | Đã nối từ foundation (tokens + portal css/js); không có jqGrid nên không cần grid.js |
| Layout scaffold khu vực `Areas/*/Views/Shared/_Layout.cshtml` (mẫu MVC mặc định, `~/Content/Site.css`, không theme, không jqGrid) | CIS DoctorManage 8 + NurseManage 12 = 20; Sett PatientManage 2; tổng **22** | **KHÔNG nối** — khác stack hoàn toàn (jQuery 1.10, không framework-ui/jqGrid, grid.js sẽ early-return; tokens CSS áp lên trang scaffold-bootstrap chưa từng được kiểm chứng; CIS/Sett không nằm trong 3 app build của chặng này). Cần điều phối viên chốt: nối tokens-only ở một chặng có STG CIS/Sett, hay để ngoài nền tảng (xem §5) |
| SSO `_Layout` (mẫu ASP.NET mặc định, `_ViewStart` gốc) | 2 (Home/DashBoard, Shared/Error) | Không nối — không phải shell portal (portal thật là SSOIndex); DashBoard là trang legacy |
| Root `_Layout` các app còn lại | 0 view (không `_ViewStart` nào trỏ tới, không view nào gán trực tiếp) | Không nối, đúng quy tắc "không nối layout không có view dùng" |
| CIS `_mobileIndex` (stack H5/ydui) | 1 (`InfusionManage/query`) | Không nối — stack mobile H5 khác, tokens desktop không phù hợp |
| EMR `_IndexOpenEditor` | 1 (`MedRecordTemplate/testbl`, trang test, không CSS/JS) | Không nối — trang test |
| Sett `_IndexV` | 0 | Không nối |
| CIS `_FileUpload` dùng như layout | 1 (`ApplyFormUpload`) | Không nối — là partial upload, không phải layout thật |

## 2. File đã sửa + diff tóm tắt

**Không commit, không `git add`.** `roles-shots-progress.md` (M) đã sửa
sẵn từ trước phiên (như foundation đã ghi), không phải thay đổi của
worker này, để nguyên.

### 2.1. `scripts/redesign/assets/redesign-grid.js` (nguồn chuẩn)

- Thêm `Redesign.autoWrapGrids(root)` — quét mọi `.ui-jqgrid`, bọc
  trong `.rd-grid-scroll`, trả số đếm `{wrapped, already, skipped,
  frozen}` để probe kiểm chứng.
- **Tự động chạy:** sau tải trang 300ms, khi `load`, và qua
  `MutationObserver` (debounce 250ms) trên `document.body`
  (`childList` + `subtree` + `class`/`style`) để bắt lưới khởi tạo muộn
  (AJAX, tab ẩn mở ra). Lần chạy sau không bọc lại lưới đã bọc nên
  tự dừng, không lặp vô hạn.
- **Loại trừ an toàn:** bỏ qua lưới đã bọc, lưới ẩn (`:visible` sai
  hoặc width 0 — tránh `setGridWidth(0)`), lưới trong tổ tiên có class
  `rd-no-scroll` hoặc thuộc tính `data-rd-no-scroll`. Mỗi lưới lỗi
  được try/catch riêng.
- **Cột neo:** với mỗi lưới, đọc `colModel` qua
  `getGridParam('colModel')`; chỉ gọi `setFrozenColumns` khi đã có cột
  khai `frozen:true` và chưa có `.frozen-bdiv`. **Không bịa cột neo.**
- `makeGridScrollable()` giữ nguyên chữ ký, dùng chung logic bọc mới
  để không phân kỳ.
- `node --check` OK.

### 2.2. Sửa deferred-init — lý do và nội dung (§3 chi tiết)

File cũ mở đầu bằng guard: jqGrid chưa có lúc parse thì `return`
vĩnh viễn. Nhưng **cả 11 file `_Form` đều nạp `redesign-grid.js`
TRƯỚC `jqgrid.min.js` đúng 2 dòng** (ví dụ PDS `_Form`: redesign dòng
34, jqgrid dòng 36), trong khi `_Index`/`_LayoutIndex` nạp jqGrid
trước nên vẫn chạy. Hậu quả: trên mọi trang `_Form`,
`window.Redesign` không tồn tại — helper foundation thực chất chưa
từng chạy trên 571 view `_Form`, dù tag script vẫn có mặt (probe
foundation chỉ kiểm tra tag, không kiểm tra `window.Redesign`; hiện
tượng "lưới đo lại" ở SyncDeptDrug trước đây là handler riêng của
trang, không phải `refreshGridWidths`).

Sửa: IIFE không return vĩnh viễn nữa mà thử init lại ở
`DOMContentLoaded` (lúc đó mọi script head đã chạy xong) và ở
`window load`; trang thật sự không có jqGrid thì vẫn không làm gì.
Không đổi thứ tự script trong 11 file `_Form` (tránh diff rộng);
deferred-init tự sửa cho mọi layout có thứ tự ngược, kể cả layout
chưa biết.

### 2.3. 10 file `Views/Shared/_Index.cshtml` (mỗi app 1 file)

Mỗi file chỉ cộng thêm, 0 xóa (tổng 27 dòng):

- Link tokens app-local ngay sau theme (`id="color-skin"`), đúng
  pattern foundation:
  `<link href="@MvcHelper.GetStaticResourceScriptUrl("~/css/redesign-tokens.css", false)" rel="stylesheet" />`
- Script grid app-local ngay sau `Newtouch-ui.js`:
  `<script src="@MvcHelper.GetStaticResourceScriptUrl("~/js/redesign-grid.js", false)"></script>`
- Meta viewport (`width=device-width, initial-scale=1`) sau `<title>`
  cho 7 file trước đó chưa có (Base HOSP, Base Web, CIS, EMR, PDS,
  MRQC, Sett); Herp/MRMS/OR đã có sẵn nên không thêm.
- BOM giữ nguyên từng file (MRQC/Sett/Herp/OR có BOM, còn lại không);
  diff đã kiểm tra thuần cộng, không dòng xóa nào.
- **Sự cố đã sửa ngay trong phiên:** lần ghi đầu vô tình chèn `\"`
  thay vì `"` vào 10 file (lỗi escape của script ghi); phát hiện qua
  `git diff`, đã sửa hết và xác minh diff thuần cộng trước khi build.
  Không file nào khác bị ảnh hưởng.

### 2.4. `scripts/redesign/assets/redesign-tokens.css` (chỉ sửa comment)

Đổi comment ở `.rd-grid-scroll` từ "do `makeGridScrollable()` bọc"
thành "do `autoWrapGrids()` tự bọc (giữ `makeGridScrollable()` để gọi
tay)". Không đổi rule CSS nào.

### 2.5. Đồng bộ wwwroot

Đã chạy `bash scripts/redesign/sync-assets.sh`: 11 app × (tokens +
grid) + SSO portal css/js, md5 đồng nhất toàn bộ:
tokens `e14e0ef8f7188b552207c81ec8eab45b`,
grid mới `d043bef2496a8b05c45fdabc40bfc8ce`.
(wwwroot là bản sao đã sinh — sửa tay sẽ bị ghi đè ở lần sync sau.)

## 3. Build scratch + dựng STG (tuân thủ quy tắc cứng)

- Build **Debug duy nhất** (`dotnet publish -c Debug -o scratch`),
  tối đa 1 build cùng lúc trong phiên này: SSO → PDS → Herp, 0 lỗi
  (chỉ warning MVC1000/vendor có sẵn). Không publish Release từ cây
  này; prod chạy `bin/Release` nên không bị ghi đè.
- PDS `Program.cs`: patch tạm literal `5237→15237` lúc build, khôi
  phục ngay, `git diff` của file đó rỗng. Herp tương tự
  `5236→15236`, diff rỗng. SSO không có `UseUrls` nên không patch.
- DLL `NewtouchHIS.Lib.Base` trong scratch PDS/Herp đã thay bằng bản
  sửa DI (`8eaae85e...`, trùng `his-dll-common/Net6/`) **trước khi
  chạy** (scratch PDS/Herp ban đầu mang bản cũ `fa27fc8f...` từ
  HintPath run-copy); SSO scratch đã tự mang bản sửa. Không copy DLL
  dưới tiến trình đang chạy.
- Thứ tự dựng: Redis STG 6380 (daemon, `LD_LIBRARY_PATH` tới
  `redis-libs`, PID 8286) → BaseAPI STG 17001 (script 7fix có sẵn,
  PID 8346) → SSO STG 15208 (PID 8418) → PDS STG 15237 (PID 8419) →
  Herp STG 15236 (PID 8420), tất cả `Development` (DI validation bật),
  DB `*_STG`, BaseAPI 17001, Redis 6380. Số lỗi
  `Unable to resolve service` trên cả 3 log STG = **0**.
  Trang login cả 3 app đều 200 trước khi probe.
- Chỉ đọc, không submit nghiệp vụ, không ghi DB nào (kể cả STG,
  ngoài session login). Không chạm run-copy hay static host 41142.
- Scratch + script chạy STG nằm ngoài repo:
  `~/workspace/index-wire-scratch/` (không commit).

## 4. Kết quả probe live (Playwright + Firefox local, tài khoản 000000)

### 4.1. (i) PDS StockQuery — trang `_Index` ✅ ĐẠT (có lưu ý cuộn)

Route `/StorageManage/DrugStockSearch/StockQuery`:

| Chỉ số | Desktop 1440 | Mobile 390 |
|---|---|---|
| Link `redesign-tokens.css` | true | true |
| Script `redesign-grid.js` | true | true |
| `window.Redesign` / `autoWrapGrids` | true / true | true / true |
| Số lưới `.ui-jqgrid` | 2 | 2 |
| Số wrapper `.rd-grid-scroll` | **2** | **2** |
| Wrapper (scrollWidth × clientWidth) | 1438×1438, 1440×1440 | 390×388, 392×390 |
| Thử cuộn (`scrollLeft = 50`) | không tràn nên không thử | **0 → 2** (đúng max = scrollWidth − clientWidth) |

**Kết luận (i):** wrapper tự động xuất hiện trên 100% lưới của trang
`_Index`, kể cả lưới thứ hai. Cơ chế cuộn ngang hoạt động
(overflow-x:auto + scrollLeft thay đổi được). **Lưu ý trung thực:**
trên trang này tràn thực tế chỉ 2px ở mobile vì `refreshGridWidths`
co lưới về đúng viewport — chưa chứng minh được cảnh cuộn dài (ví
dụ lưới `shrinkToFit=false` hoặc có cột neo). Wrapper ở đây đóng vai
trò lưới an toàn cho các lưới không tự co, không phá bố cục: ảnh
mobile cho thấy form xếp chồng gọn, lưới đủ 390px, pager đầy đủ.

### 4.2. (ii) PDS SyncDeptDrug — trang `_Form` ✅ ĐẠT (sau sửa §2.2)

Route `/Medicine/SyncDeptDrug`. Lần probe đầu (trước sửa
deferred-init): `tokens/grid = true` nhưng `Redesign = false`,
`wrappers = 0` — chính là bằng chứng §3. Sau sửa + sync lại (STG
phục vụ wwwroot từ source nên không cần restart), probe lại:

| Viewport | Chiều rộng lưới đo live | Wrapper |
|---|---|---|
| 1440 | 1440px | có |
| 900 | 900px | có |
| trở lại 1440 | 1440px | có |

Lưới bám chiều rộng viewport qua cả 3 bước, wrapper tồn tại suốt quá
trình resize, không kẹt kích thước cũ, không bóp méo.

### 4.3. (iii) PreViewInfo — trạng thái rỗng ✅ ĐẠT (hồi quy của (d))

Route `/DrugStorage/PreViewInfo` (0 dòng), desktop 1440:

- Phần tử `.ui-jqgrid-bdiv > .unwritten` hiển thị
  "Không tìm thấy dữ liệu liên quan!" với computed style:
  màu `rgb(107, 114, 128)` (xám #6b7280), `text-align: center`,
  nền `rgb(248, 249, 250)` — **không đỏ**, đúng như bổ sung (d).
- Đồng thời wrapper nay đã có: 1 lưới / 1 wrapper, tokens + grid = true,
  `Redesign` có mặt (trước sửa là 0 wrapper).

### 4.4. (iv) SSO portal mobile — drawer ✅ ĐẠT (hồi quy nhanh)

Route `/Home/SSOIndex`, viewport 390×844, sau đăng nhập SSO STG 15208:

- Bấm `#rd-nav-toggle` (JS click): `body.rd-drawer-open = true` ✅
- Bấm Esc: đóng, `rd-drawer-open = false` ✅
- (Bổ sung (a) trước đây đã chứng minh đủ 4 đường đóng; chặng này
  chỉ hồi quy nhanh mở + Esc theo brief.)

### 4.5. Herp — kiểm chứng HTML (không nằm trong danh sách probe bắt buộc)

- Curl login Herp STG 15236: `{"state":"success","message":"Đăng nhập
  thành công"}`.
- GET `/WarehouseManage/Warehouse/Index` (trang `_Index` Herp) với
  cookie login: HTML chứa đúng 1 lần `redesign-tokens.css` và 1 lần
  `redesign-grid.js` ✅. Không probe trình duyệt cho Herp (ngoài
  phạm vi probe của brief; build Herp đã xác nhận `_Index` biên dịch
  được với link/script mới).

### 4.6. Sự cố login trong phiên probe (ghi trung thực)

- Lần probe trình duyệt đầu tiên: sau click `#login_button`, trang
  vẫn ở `/Login/Index`, `.tips_msg` rỗng. **Không thử mật khẩu khác,
  không đoán.**
- Chẩn đoán bằng curl POST form-encoded cùng mật khẩu qua env:
  `{"state":"success","message":"Đăng nhập thành công"}` + cookie
  phiên — chứng tỏ mật khẩu đúng, lỗi nằm ở tương tác trình duyệt
  (trang login nạp script từ static host; lần đầu chỉ chờ
  `domcontentloaded` + 1s, handler click có thể chưa bind).
- Sửa probe: chờ `networkidle` + 2s rồi mới điền form, chờ redirect
  tới `/Home/Index` tối đa 15s. Các lần sau login trình duyệt thành
  công cả PDS 15237 và SSO 15208 (URL đích `/Home/Index`, tips rỗng).
  Đây là sửa phương pháp probe, không phải thử mật khẩu khác.

### 4.7. Ảnh (thư mục bắt buộc, tuyệt đối)

`~/workspace/openhis-core/shots-redesign/index-wire/` (gitignored,
không commit):

- `pds-stockquery-desktop.png` — StockQuery desktop, 2 lưới đã bọc
- `pds-stockquery-mobile.png` — StockQuery mobile 390, form xếp chồng, lưới đủ viewport
- `pds-syncdeptdrug-desktop.png` — SyncDeptDrug sau resize
- `pds-previewinfo-desktop.png` — PreViewInfo, trạng thái rỗng
- `sso-portal-mobile-closed.png`, `sso-portal-mobile-drawer-open.png`,
  `sso-portal-mobile-after-esc.png` — portal mobile drawer

## 5. Phần chưa đạt + quyết định cần điều phối viên chốt

1. **Layout scaffold khu vực (22 view: CIS 20 + Sett 2, cộng SSO
   `_Layout` 2 view legacy):** có nối tokens-only (không grid, vì
   không có jqGrid) trong một chặng có STG CIS/Sett để kiểm chứng,
   hay để ngoài nền tảng? Worker khuyến nghị: nối tokens-only ở
   chặng CIS/Sett riêng (rủi ro thấp, CSS đã có sẵn rule mobile cho
   `table.form`), nhưng **không tự quyết** vì chưa có STG để kiểm
   chứng trong chặng này.
2. **Cột neo live:** cần một trang đã khai `frozen:true` để kiểm
   chứng `setFrozenColumns` qua auto-wrap (ứng viên theo báo cáo
   trước: EMR `NursingRecord` hoặc Base HOSP). PDS không có lưới nào
   khai frozen nên chặng này không thể đóng mục này.
3. **Cuộn ngang tràn lớn:** cần một lưới `shrinkToFit=false` hoặc
   tổng cột vượt viewport sau khi đo lại để chứng minh cuộn dài;
   StockQuery chỉ tràn 2px nên chỉ chứng minh được cơ chế.
4. **Phạm vi `_Form` đã mở rộng:** sửa deferred-init chạm tới cả 11
   app (mọi trang `_Form` trước đây helper đều chết). Khi deploy,
   cần re-sweep nhanh một trang `_Form` mỗi app để chắc không có
   trang nào phụ thuộc vào việc helper "không chạy" (không phát hiện
   trường hợp nào như vậy trên PDS trong chặng này).

## 6. Dọn dẹp cuối phiên + những gì KHÔNG làm

- Đã kill đúng PID đã khởi động: Redis STG 8286, BaseAPI 8346, SSO
  8418, PDS 8419, Herp 8420. Kiểm tra sau kill: các cổng STG 6380 /
  17001 / 15208 / 15237 / 15236 đều trống.
- Prod không bị chạm: sau dọn, login prod 5208 / 5237 / 5236 vẫn 200
  (7001/41142 trả 404 cho `/Login/Index` là bình thường vì đó là
  API host/static host, không phải trang login).
- Không commit, không `git add`, không sửa run-copy/static host,
  không restart app prod hay SQL Server (SQL 1433 sống suốt phiên),
  không ghi DB demo (mọi test ghi đều không có; chỉ GET + session
  login trên DB `*_STG`), không viết lại footer tiếng Trung, không
  đụng mâu thuẫn receipt-prefix, không tuyên bố test native.
- Không copy DLL dưới tiến trình đang chạy; không publish Release
  từ cây này.

## 7. File giao (không commit)

- Báo cáo này: `scripts/redesign/index-wire-REPORT.md`
- Nguồn chuẩn: `scripts/redesign/assets/redesign-grid.js`,
  `scripts/redesign/assets/redesign-tokens.css` (comment)
- 10 file `_Index` đã nối + wwwroot copies (đồng bộ bởi
  `scripts/redesign/sync-assets.sh`)
- Ảnh: `shots-redesign/index-wire/` (7 ảnh, §4.7)
- Scratch + script STG (ngoài repo, không commit):
  `~/workspace/index-wire-scratch/`
