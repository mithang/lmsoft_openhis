# BÁO CÁO — Trang chủ standalone-responsive (đợt PER-APP C, chương trình REDESIGN UI/UX OpenHIS)

**Worker:** PER-APP (C) — chỉ trang chủ các app. **Không commit, không deploy prod.**
**Repo:** `~/workspace/openhis-core`, nhánh `V2.2`. **Ngày:** 2026-10-28.
**Trạng thái tổng thể: DỞ DANG DO BỊ CHẶN — SQL Server chết giữa phiên (xem §6).**
Phần code đã sửa xong cho mọi trang chủ trong phạm vi; phần kiểm chứng live chỉ hoàn tất cho **PDS**
(mobile + tablet Default, mobile shell sau fix). Herp và 8 app còn lại **chưa build/probe** vì
không login được sau khi SQL chết. Báo cáo này ghi đúng những gì đã kiểm chứng, không báo đạt
phần chưa đo.

## 1. Kết luận nhanh (ĐẠT / CHƯA ĐẠT)

| Hạng mục | Kết quả | Bằng chứng |
|---|---|---|
| PDS shell mobile — iframe đủ rộng | **ĐẠT (sau fix)** | iframe `x=153, w=225` → `x=12, w=366` trên viewport 390; `.content-iframe` `w=245` → `w=386`; `scrollW=390` |
| PDS dashboard mobile (standalone + trong iframe) — KPI 2 cột thật | **ĐẠT (sau fix)** | Thẻ KPI `(12,49), (202,49), (12,178), (202,178)`, w=178; trong iframe `(12,…), (190,…)` w=166; offenders rỗng |
| PDS dashboard tablet standalone — KPI 3 cột thật | **ĐẠT (sau fix)** | Hàng đầu `(12,49), (264,49), (517,49)`, w=240; chart `w=669`, không cắt; offenders rỗng |
| PDS dashboard desktop | **ĐẠT (giữ nguyên, không bị fix chạm tới)** | Số đo trước fix: KPI 4 cột, chart `w=381`, `.rd-empty` có mặt; fix chỉ áp dụng ≤991px nên desktop không đổi |
| PDS shell tablet | **CHƯA ĐẠT — chưa đo được** | Lần đo đầu timeout `networkidle`; lần đo lại thì login thất bại do SQL chết (§6); ảnh tablet shell sai (trang login) đã xóa, hiện **không có** `pds-index-tablet.png` |
| PDS shell desktop | **ĐẠT (giữ nguyên)** | Số đo trước fix: iframe `x=189, w=1081`, `scrollW=1280`, sidebar 220px; fix chỉ ≤991px |
| Herp home (code xong, STG đang chạy bản **trước** fix clearfix) | **CHƯA KIỂM CHỨNG** | Cần rebuild (có fix §3.2) + probe 3 viewport sau khi SQL sống lại |
| HOSP / Base Web / MRMS / EMR / Sett / MRQC / OR / CIS home | **CHƯA KIỂM CHỨNG** | Code xong, chưa build/probe (bị chặn bởi SQL chết) |
| Trạng thái rỗng biểu đồ PDS | **ĐẠT** | `.rd-empty` + câu "Chưa có số liệu loại thuốc trong năm nay." + link "Tra cứu tồn kho" (mobile + tablet) |
| Tràn ngang thật (offenders) PDS mobile/tablet | **ĐẠT** | `offenders=[]` ở cả Default standalone mobile/tablet và dashboard trong iframe mobile |

## 2. Tuân thủ quy tắc cứng của đợt

- Build **chỉ Debug publish** sang scratch mới; tuyệt đối không publish Release từ cây này.
  PDS rebuild trong phiên: `dotnet publish -c Debug -o ~/workspace/home-responsive-scratch/pds`, 0 lỗi
  (chỉ warning MVC1000 có sẵn). `Program.cs` patch tạm `5237→15337` lúc build, đã `git checkout`
  khôi phục, `git diff` của file rỗng, `grep UseUrls` về 5237.
- DLL `NewtouchHIS.Lib.Base` trong scratch đã là bản sửa DI `8eaae85e513ca28d0c06f79eae30eb2c`
  (khớp `his-dll-common/Net6/`); DLL PDS scratch chứa `rd-kpi-grid` ×15, `clear: both` ×1,
  câu lệnh triệt clearfix ×2 (kiểm tra bằng `strings -e l`, vì chuỗi Razor lưu UTF-16 nên
  `strings` thường cho 0 — không phải build thiếu).
- Mỗi lần chỉ build 1 app; chạy dotnet với proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`,
  `ASPNETCORE_URLS` rõ ràng; kill theo PID chính xác; không copy DLL dưới tiến trình đang chạy
  (PDS STG cũ kill trước rồi mới publish lại).
- Login chỉ tài khoản demo `000000`; mật khẩu chỉ qua biến môi trường tạm thời của phiên probe,
  **không ghi vào file script, báo cáo, log hay ảnh**, không in ra output. Probe chỉ đọc, không
  submit nghiệp vụ, không ghi DB (kể cả STG, ngoài session login).
- Mobile = giả lập viewport (Firefox + Playwright local), **không** tuyên bố test native.
- Không sửa run-copy, không sửa static host 41142, không restart app prod hay SQL, không commit.

## 3. Hai nguyên nhân gốc tìm ra trong phiên này (trước đây chỉ là giả thuyết)

### 3.1. Iframe mobile PDS bị bóp còn 225px: do tab link float + `.content-iframe` tạo BFC

Số đo mobile 390 trước fix (shell PDS STG 15337): `.content-tabs` đủ rộng (`x=2, w=386`),
nhưng `.content-iframe` chỉ `x=143, w=245`, iframe `x=153, w=225`. Nguyên nhân đo được:

- Link tab đang active (`.menuTab`) có `float: left`, `w=141`, rect `x=2, y=268` — tức **tràn xuống
  dưới** đáy tab bar (`.content-tabs` kết thúc ở `y=262`).
- `.content-iframe` có inline `overflow: hidden` nên tạo block formatting context (BFC) mới; BFC
  phải tránh float đứng trước nó trong cùng ngữ cảnh → hộp iframe bị đẩy sang phải float
  (bắt đầu ở `x=143 ≈ 2+141`) và chỉ còn `386-141=245px`. Đúng khớp số đo.
- Sidebar đã `display: none` nên không phải thủ phạm; hai `div#nav-col` trùng id (một là footer
  vendor `login-foot`, ở ≤991px rơi về `position: relative` thay vì `absolute` như desktop) là
  khối float trái nhưng kết thúc ở `y=217`, trước khi `.content-iframe` bắt đầu, nên không gây bóp.

**Fix (CSS shell, khối `≤991px`, cả 5 file `Index.cshtml` PDS/HOSP/Base Web/MRMS/EMR):**

```css
#theme-wrapper #content-wrapper .content-iframe { clear: both; }
```

Sau fix (mobile 390): `.content-iframe` `x=2, w=386`, `clear=both` (computed), iframe `x=12, w=366`,
bắt đầu ở `y=308` (dưới đáy float `268+40`), `scrollW=390`, sidebar vẫn `none`.
Tab link vẫn tràn dưới tab bar 6px (vấn đề riêng của tab bar, tồn tại trước — xem §7), nhưng
không còn bóp iframe nữa. Fix nằm trong khối `≤991px` nên desktop không đổi.

### 3.2. KPI mobile/tablet xếp lệch 1 ô: do clearfix Bootstrap trở thành grid item

Số đo Default standalone mobile trước fix: container `#dv_needDeal` đã là `display: grid`,
`grid-template-columns: 177.5px 177.5px`, nhưng thẻ đầu "Thuốc điều giá" nằm ở cột 2 hàng 1,
hàng 1 chỉ có 1 thẻ, các thẻ sau lệch 1 ô. Nguyên nhân đo được:

- Bootstrap 3 clearfix: `.panel-body::before, ::after { content: " "; display: table; }`.
  Trong container `display: grid`, pseudo-element `::before` **trở thành grid item** và chiếm ô
  (hàng 1, cột 1), đẩy thẻ KPI đầu sang cột 2. `::after` cũng chiếm 1 ô cuối.
  (Đo trực tiếp: `beforeContent=" "`, `beforeDisplay=table`, `contGrid=177.5px 177.5px`.)

**Fix (CSS dashboard, cả 2 khối grid mobile + tablet, 5 file `Default.cshtml` + Herp `Index.cshtml`):**

```css
.rd-kpi-grid::before, .rd-kpi-grid::after { display: none !important; content: none !important; }
```

Chỉ triệt trong khối grid (≤991px); desktop vẫn float + clearfix như cũ.
Sau fix: mobile `(12,49), (202,49), (12,178), (202,178)` w=178 — đúng 2 cột;
tablet `(12,49), (264,49), (517,49)` w=240 — đúng 3 cột, thẻ thứ 4 xuống hàng 2.
**Lưu ý cho các worker khác:** mọi panel KPI nào dùng `.rd-kpi-grid` trên `.panel-body`
Bootstrap đều cần rule này, nếu không sẽ lệch 1 ô giống hệt.

## 4. Bảng số đo PDS trước/sau (STG 15337, Playwright + Firefox local)

| Trang / viewport | Trước fix | Sau fix |
|---|---|---|
| Shell mobile 390 — iframe | `x=153, w=225, h=744` | `x=12, w=366` (đo lúc tải; ảnh cho thấy dashboard render đủ chiều cao sau khi JS resize) |
| Shell mobile 390 — `.content-iframe` | `x=143, w=245` | `x=2, w=386`, `clear=both` |
| Shell mobile 390 — `#content-wrapper` | `x=-3, w=396`, margin-left `0px` | giữ nguyên `x=-3, w=396` (đúng: wrapper full, chỉ iframe bị bóp trước đây) |
| Shell mobile 390 — scrollW / sidebar | `390` / `display: none` | `390` / `display: none` |
| Default standalone mobile 390 — KPI | hàng đầu 1 thẻ (lệch clearfix) | 2 cột: `(12,49), (202,49)`, w=178; hàng 2 `(12,178), (202,178)` |
| Default standalone mobile 390 — chart / rỗng / tràn | `#container w=329`, `.rd-empty` có, offenders rỗng (đã đúng) | giữ nguyên, offenders vẫn rỗng |
| Dashboard trong iframe mobile 390 — KPI | 1 cột theo cách đếm cũ, thẻ vượt 5px (`right=230` so với 225) | 2 cột w=166 (`x=12` và `x=190`), chart `w=308`, `.rd-empty` có, offenders rỗng |
| Default standalone tablet 768 — KPI | hàng đầu 2 thẻ (lệch clearfix; kỳ vọng 3) | 3 cột: `(12,49), (264,49), (517,49)` w=240; thẻ 4 `(12,176)` |
| Default standalone tablet 768 — chart / rỗng / tràn | `w=669`, `.rd-empty` có, offenders rỗng (đã đúng) | giữ nguyên |
| Shell tablet 768 | chưa đo được (timeout `networkidle` lần 1; login thất bại lần 2 do SQL chết) | **chưa có số đo** |
| Shell/Default desktop 1280 | iframe `x=189, w=1081`; KPI 4 cột; chart `w=381`; `.rd-empty` có; offenders chỉ có dải tab `NAV.page-tabs` rộng 100000px (tồn tại trước, bị `overflow: hidden` chứa, `scrollW` vẫn 1280) | không đổi (fix chỉ ≤991px); ảnh desktop cũ vẫn đại diện đúng |

Số liệu KPI trong ảnh là số liệu thật từ STG (ví dụ "Thuốc hết hạn" 216, "Duyệt nhập kho" 1),
không bịa. Thẻ 0 vẫn là 0; chưa có điểm so sánh nên không ghi điểm so sánh.

## 5. Code đã sửa xong cho các app còn lại (CHƯA kiểm chứng live)

Nội dung sửa theo đúng brief đợt C (xem chi tiết từng file trong `git diff` — worker này
**không** đụng các file của worker khác đang song song sửa, ví dụ SysGlobalConfig, ChargeCategory,
NursingRecord, Purchase, Warehouse, MrDept, MrFee, OpStaff, StockQuery, DaySett):

- **Shell `Views/Home/Index.cshtml` ×5** (PDS, HOSP, Base Web, MRMS, EMR): meta viewport,
  tiêu đề nhiệm vụ + `<h1 class="sr-only">`, CSS shell §3.1 (MRMS/EMR thêm h1 qua partial).
- **`Views/Home/_IndexContentWrapper.cshtml` ×2** (MRMS, EMR): thêm `<h1 class="sr-only">`.
- **`Views/Home/About.cshtml` ×3** (PDS, HOSP, Base Web): viewport thành
  `width=device-width, initial-scale=1`.
- **`Views/Home/Default.cshtml` ×5** (PDS, HOSP, Base Web, MRMS, EMR): meta viewport, tiêu đề
  nhiệm vụ, link tokens app-local, class `rd-kpi-grid` cho panel KPI, CSS dashboard + fix
  clearfix §3.2, nhánh trạng thái rỗng cho biểu đồ (trừ MRMS/EMR dùng dữ liệu demo ngẫu nhiên
  nên không bao giờ rỗng — để nguyên, xem §7).
- **Herp `Views/Home/Index.cshtml` + `HomeApp.cs`**: viewport, tiêu đề
  "Trang chủ — Hệ thống quản lý vật tư" (bỏ "(ASP.NET Core 6.0)"), `rd-kpi-grid` + fix clearfix,
  trạng thái rỗng biểu đồ tròn, icon thẻ giá `fa-cny` (¥) → span `₫`, cảnh báo 0 đổi
  `color:#a94442` → `color:#6b7280` (trung tính).
- **PDS `HomePageStatisticsApp.cs`**: cùng sửa `fa-cny` → span `₫` (nhánh kho thuốc,
  "Duyệt điều chỉnh giá"; sửa nhất quán theo Herp, cùng lỗi UI tiếng Việt trên trang chủ).
- **Sett `Views/Home/Index.cshtml`**: viewport, tiêu đề "Trang chủ — Quản lý thu phí và quyết toán",
  gỡ 29 hậu tố tên area trong nhãn link (URL giữ nguyên), CSS padding mobile.
- **MRQC `Views/Home/Index.cshtml`**: viewport, tiêu đề "Trang chủ — Kiểm soát chất lượng bệnh án",
  gỡ hậu tố `(MedicalCenter)`, `(Apply)`, `(SysItem)`, rút gọn ghi chú link JSON thành `(JSON)`,
  CSS padding mobile.
- **OR `Views/Home/Index.cshtml`**: tiêu đề "Trang chủ — Hệ thống phòng mổ", CSS mobile
  (padding 16px, li 10px, link tap 6px); viewport đã có sẵn nên không thêm.
- **CIS `Views/Home/Index.cshtml`**: chỉ thêm meta viewport (không sửa tiêu đề/link — audit ghi
  rõ cần điều phối viên quyết trang này sẽ bị thay bằng WorkSpace của portal hay nâng thành
  trang chủ thật; worker này không tự quyết).
- Tiêu đề nhiệm vụ đã đặt: PDS "Trang chủ — Nhà thuốc và kho thuốc" (title shell giữ
  "Hệ thống nhà thuốc - kho thuốc"); HOSP "Trang chủ — Quản trị hệ thống"; Base Web
  "Trang chủ — Quản trị dữ liệu cơ sở" (sửa cả title shell vốn nối chữ lỗi); MRMS
  "Trang chủ — Quản lý bệnh án" (title shell giữ động theo AppName); EMR
  "Trang chủ — Bệnh án điện tử" (sửa cả title shell vốn nối chữ lỗi).
- HOSP: **không** khôi phục tab + iframe `/Home/Default` (ở HEAD và bản vendor run-copy đều đã
  bị comment từ commit port gốc; khôi phục là thay đổi chức năng, ngoài phạm vi). Probe shell
  HOSP sau này đo `#content-wrapper` + tab bar; dashboard đo qua `/Home/Default` standalone.
- Quirk log STG không chặn (tồn tại trước, ngoài phạm vi): log PDS STG có
  `SessionMiddleware: Error closing the session. ArgumentNullException ... ConfigurationOptions.DoParse`
  nhưng login và render trang chủ vẫn 200 — lỗi đóng session theo cách cấu hình STG
  (repo script dùng cùng `RedisConfig__Connection`).

## 6. SỰ CỐ CHẶN PHIÊN: SQL Server chết — DỪNG probe theo quy tắc cứng

Diễn biến (giờ ICT 2026-10-28):

1. ~16:35–16:40: probe PDS tablet Default standalone thành công (KPI 3 cột, chart, rỗng).
2. Lần đo shell tablet tiếp theo: login curl thất bại 1 lần, thông điệp trả về nguyên văn:
   `{"state":"error","message":"登录失败，请联系管理员","data":null,"code":null,"exStackTrace":null,"InnerException":null}`
   Theo quy tắc cứng, **dừng ngay, không thử mật khẩu khác, không đoán**. (Cùng tài khoản demo
   này đã login thành công nhiều lần trong phiên, nên đây không phải lỗi sai mật khẩu.)
3. Kiểm tra theo đúng quy trình: `sqlcmd` tới `localhost` timeout mạng (TCP 0x2749), **không có
   listener cổng 1433, không có tiến trình `sqlservr`**; errorlog `/var/opt/mssql/log/errorlog`
   kết thúc 16:24:14 bằng task telemetry, không có dòng shutdown trong đoạn cuối.
   Kết luận: **SQL Server đã dừng/chết sau 16:24**, mọi thao tác cần DB (login, KPI, chart)
   đều không chạy được cho tới khi SQL sống lại.
4. Trang login của prod (5208/5237/5236) vẫn trả 200 (render trang không cần DB), nhưng login
   thật sẽ thất bại tương tự — điều phối viên cần xử lý khôi phục SQL (worker này **không tự
   restart SQL** theo quy tắc cứng). Ghi chú thêm, không quy kết: errorlog có 1 dòng 15:56
   "Login failed for user 'sa'. Reason: Password did not match" (trước phiên probe này, từ
   127.0.0.1) — không phải do worker này (sqlcmd của worker này chạy sau đó và chỉ timeout mạng).

**Việc còn lại khi SQL sống lại, theo thứ tự:**

1. Đo lại shell PDS tablet 768 (dùng `commit` + chờ cố định, không dùng `networkidle` cho
   trang shell) + chụp `pds-index-tablet.png` (hiện đã xóa vì bản cũ là ảnh trang login).
2. Rebuild Herp (Debug, cổng STG 15336, patch tạm `Program.cs` rồi khôi phục, thay DLL Lib.Base
   trước khi chạy) + probe Herp 3 viewport (`index:/Home/Index:dash`, đo thêm màu cảnh báo,
   ký hiệu `₫`, không còn `.fa-cny`).
3. Build từng app còn lại, mỗi lần 1 app, Debug publish, cổng STG dãy 153xx, patch tạm
   `Program.cs` rồi khôi phục + kiểm tra diff rỗng, thay DLL Lib.Base trước khi chạy:
   HOSP 15333, Base Web 15338, MRMS 15332, EMR 15335, Sett 15321, MRQC 15320, OR 15331,
   CIS 15334 (chỉ kiểm chứng viewport). Probe 3 viewport mỗi app (shell: `index:shell,
   default:dash`; Sett/MRQC/OR/CIS: `index:bare`), ảnh vào `shots-redesign/home-responsive/`.
4. Bổ sung bảng số đo các app này vào báo cáo này rồi mới coi đợt C là hoàn tất.

## 7. Vấn đề tồn tại trước, ngoài phạm vi (chỉ liệt kê, không sửa)

- Tab bar shell mobile: link tab vẫn tràn dưới tab bar 6px và các nút tab ("»", "Thao tác tab")
  chồng nhau nhẹ ở mép phải (thấy trong `pds-index-mobile.png`). Không còn bóp iframe, nhưng
  bố cục tab bar mobile vẫn cần worker shell xử lý riêng.
- Footer vendor tiếng Trung trong dải tối bên trái ảnh mobile (chuỗi "Copyright © 2026…" +
  tên công ty vendor): **chỉ liệt kê, không viết lại** theo brief.
- Dải tab `NAV.page-tabs.menuTabs` rộng 100000px (tồn tại trước, bị `overflow: hidden` chứa).
- Mâu thuẫn receipt-prefix (tiếng Việt mới vs cũ theo flow): ngoài phạm vi, không đụng.
- MRMS/EMR dashboard dùng dữ liệu demo ngẫu nhiên (`randomScalingFactor`) nên không có trạng
  thái rỗng — ghi chú, không bịa trạng thái.
- HOSP không có iframe Default mặc định (xem §5); CIS chỉ viewport (xem §5).
- 5 app vẫn chạy build Debug ở prod (MRMS Web, OR Web/API, CIS Web/API — quyết định chờ
  phase 8) không thuộc đợt này.

## 8. Ảnh (đường dẫn tuyệt đối bắt buộc)

- `~/workspace/openhis-core/shots-redesign/home-responsive/pds-index-mobile.png`
  (sau fix: dashboard đủ rộng, KPI 2 cột, footer vendor vẫn hiện ở dải tối trái)
- `~/workspace/openhis-core/shots-redesign/home-responsive/pds-index-desktop.png`
  (trước fix nhưng desktop không bị fix chạm tới nên vẫn đại diện đúng)
- `~/workspace/openhis-core/shots-redesign/home-responsive/pds-default-mobile.png` (sau fix)
- `~/workspace/openhis-core/shots-redesign/home-responsive/pds-default-tablet.png` (sau fix)
- `~/workspace/openhis-core/shots-redesign/home-responsive/pds-default-desktop.png`
  (trước fix, desktop không đổi)
- **Thiếu:** `pds-index-tablet.png` (đã xóa bản sai là ảnh trang login; cần chụp lại sau khi
  SQL sống lại), toàn bộ ảnh Herp/HOSP/Base Web/MRMS/EMR/Sett/MRQC/OR/CIS (chưa probe).

## 9. STG còn chạy cuối phiên (để điều phối viên quyết, chưa kill)

Worker này **giữ nguyên** 2 STG sau để khỏi phải build lại khi SQL sống lại (lệnh kill bên dưới
nếu điều phối viên muốn dọn):

- PDS STG 15337 (bản **sau fix**, đã kiểm chứng): bash PID 27861 + dotnet PID 27868,
  log `~/workspace/home-responsive-scratch/pds-stg2.log`.
- Herp STG 15336 (bản **trước fix clearfix** — bắt buộc rebuild trước khi probe): bash PID 15278
  + dotnet PID 15282, log `~/workspace/home-responsive-scratch/herp-stg.log`.
- Không chạm: Redis STG 6380 (PID 13479), BaseAPI STG 17001 (PID 13503), mọi STG của worker
  khác (PDS/Herp/EMR từ `~/workspace/perapp-grids-scratch/*`, PID 13663/13664/13665 và
  probe `/tmp/probe_perapp.py` đang chạy), mọi app prod.
- Lệnh dọn nếu cần: `kill 27861 27868 15278 15282` rồi kiểm tra `ss -ltn | grep 1533`.
