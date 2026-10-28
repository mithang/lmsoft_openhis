# Báo cáo nền tảng (foundation) — Chặng 3 REDESIGN UI/UX OpenHIS

> Worker nền tảng, ngày 2026-10-28. **KHÔNG commit** (điều phối viên soát
> rồi mới commit). **KHÔNG triển khai lên prod.** Mọi thay đổi chỉ nằm
> trong repo `openhis-core` dưới dạng chưa commit + build scratch.
> Ngôn ngữ báo cáo: tiếng Việt. Mobile = giả lập viewport, không phải
> app native (không tuyên bố test native).

## 0. Tóm tắt kết quả

| Hạng mục | Trạng thái | Bằng chứng chính |
|---|---|---|
| VIỆC 1 — Pipeline asset | ✅ XONG, có quyết định | `scripts/redesign/ASSET-PIPELINE.md` |
| VIỆC 2(a) — CSS dùng chung | ✅ XONG (nối 22 layout + SSOIndex) | Build 11/11 OK; PDS STG phục vụ file 200 |
| VIỆC 2(b) — Helper JS jqGrid | ✅ XONG (helper + nối layout) | `node --check` OK; eval stub OK; PDS STG 200 |
| VIỆC 2(c) — Shell portal mobile | ⚠️ CODE XONG, CHƯA KIỂM CHỨNG LIVE | Chuỗi đã biên dịch trong DLL SSO; live portal bị chặn bởi lỗi DI của HEAD (xem §5) |
| Kiểm chứng STG | ⚠️ MỘT PHẦN (xem §4–§5) | PDS STG 15237 + SSO STG 15208 (prod-bin) + BaseAPI 17001 + Redis 6380 |

## 1. VIỆC 1 — Pipeline asset (làm trước, chỉ đọc)

**Kết luận: KHÔNG sửa file trên static host trong chặng này.**
Bằng chứng (chi tiết đầy đủ trong `ASSET-PIPELINE.md`):

- Static host 41142 là tiến trình
  `python3 static-cors-server.py 41142 ~/workspace/lmsoft_openhis_run/Newtouch.HIS.Static/Newtouch.HIS.Static`
  (PID 2455 lúc kiểm tra). `curl` file theme live cho md5 trùng file đĩa
  run-copy (`e86d5bebb29e426fdf8a7dca08d8850f`).
- `openhis-core` **không có project `Newtouch.HIS.Static`**; nguồn gốc
  duy nhất là clone vendor V2.2 + run-copy, và run-copy là **repo git
  riêng** (origin là đường dẫn local tới clone gốc), không commit được
  qua `openhis-core`.
- Run-copy đã lệch bản gốc: `framework-ui.css`, `framework-ui.js`,
  `Newtouch-ui.js` khác bản vendor (sửa trong quá trình port trước đây,
  không có commit ghi lại).
- `MvcHelper.GetStaticResourceScriptUrl(url, true)` → static host, không
  có `?V=`; `(url, false)` → app-local từ `wwwroot` của chính app, có
  `?V=` (cache-buster). App-local đã có tiền lệ: `/js/jqgrid/grid.locale-vi.js`,
  SSO `~/css/bootstrap-extensions.css`, `~/js/site.js`.

**Quyết định (ghi trong ASSET-PIPELINE.md §4):** nguồn chuẩn duy nhất
là `scripts/redesign/assets/`; đồng bộ vào `wwwroot` từng app bằng
`scripts/redesign/sync-assets.sh`; layout nạp app-local **sau theme**
(`id="color-skin"`). Sửa trực tiếp trên static host sẽ đổi cả prod lẫn
STG ngay lập tức (chung cổng 41142, không phân biệt môi trường), không
qua review/commit — nên đường lên static host (nếu cần) phải là bước
deploy riêng do điều phối viên quyết, kèm version tên file.

## 2. VIỆC 2(a) — File CSS dùng chung

**File chuẩn:** `scripts/redesign/assets/redesign-tokens.css`
(đồng bộ vào `wwwroot/css/` của 10 app nghiệp vụ + SSO, md5 đồng nhất
`34b3eb4531b2ad9ee5c5ccb6dc487bda`).

- Token DESIGN §2: 6 màu ngữ nghĩa, spacing bội số 4 (4/8/12/16/24),
  bo góc 4px (nút/input) / 6px (thẻ), line-height 1.5 cho chữ nội dung
  và 1.35 cho tiêu đề (§7, chống cắt dấu xếp chồng ế/ữ/ộ/ấ).
- Meta viewport: **bổ sung cho 10 `_LayoutIndex.cshtml`** (trước đó 0/10
  file có viewport) và SSO `_Index.cshtml` + `SSOIndex.cshtml`
  (SSOIndex trước đó không có; `_Index` chỉ có viewport nằm trong
  comment `@* *@`, đã thêm meta thật, giữ nguyên comment).
  `_Form.cshtml` đã có sẵn viewport nên không thêm.
- Chống tràn home/fragment: `body{min-width:0}`, ảnh trong nội dung
  `max-width:100%` (logo dùng background-image nên không bị ảnh hưởng).
  Trang home các app đặt `Layout = null` và tự mang `<head>` riêng nên
  **không** ăn theo `_LayoutIndex` — sửa home per-page là việc chặng sau,
  nền tảng chỉ chuẩn bị token/helper.
- Trạng thái rỗng (§6.2): đã đọc code thật — jqGrid chèn
  `<div class="unwritten" style="color:red">` + chuỗi locale
  `unwrittentext` ("Không tìm thấy dữ liệu liên quan!") vào
  `.ui-jqgrid-bdiv` khi lưới rỗng. Rule `.ui-jqgrid-bdiv > .unwritten`
  trung hòa đúng div đó (xám trung tính + nền nhạt + căn giữa, cần
  `!important` để thắng style inline). Không đổi câu chữ locale:
  phân biệt "chưa có dữ liệu" vs "lọc không ra kết quả" là việc nội
  dung của chặng sau. Kèm component `.rd-empty` cho trang/thẻ tự viết.
- Toolbar/nút: `.toolbar` là class chung thật (130+ chỗ dùng ở 4 app đã
  đếm). Nút nguy hiểm tách vị trí: `.toolbar .btn-danger{margin-left:16px}`.
  Mobile <768px: toolbar bỏ `float:left` của theme, chuyển flex-wrap +
  gap 8px, nút/input tối thiểu 44px chạm.
  **KHÔNG ép "một nút chính duy nhất" bằng CSS theo vị trí DOM:**
  bằng chứng trang PDS FypzIndex đặt nút reload (icon-only, btn-primary)
  TRƯỚC nút "Tạo mới" (btn-primary) — rule hạ cấp theo vị trí sẽ hạ
  nhầm nút chính. Quy ước ghi trong file CSS + báo cáo này: mỗi
  `.toolbar` giữ một `.btn-primary`, hạ cấp nút thừa là việc per-page
  có audit ở chặng sau.
- Form mobile một cột, nhãn trên trường (§4.3): pattern vendor thật là
  `table.form > tr > th.formTitle + td.formValue` (619 `formTitle` chỉ
  riêng PDS). Rule <767px chuyển bảng form thành block một cột, giữ
  nguyên thứ tự DOM (nhãn, trường, nhãn, trường), kể cả hàng 2 cặp.
  Kèm class opt-in `.rd-form-stack` cho form div-based và `.rd-kpi-grid`
  (2 cột KPI mobile) cho trang home khi được sửa ở chặng sau.

**Nối layout:** 10 `_LayoutIndex` + 11 `_Form` (10 app + SSO) +
SSO `_Index` + `SSOIndex` = 24 file, diff thuần cộng 362 dòng, 0 xóa
(sau khi khôi phục BOM cho các file vốn có BOM — xem §6).

## 3. VIỆC 2(b) — Helper JS dùng chung

**File chuẩn:** `scripts/redesign/assets/redesign-grid.js`
(md5 đồng nhất `77c730c48fcf74c913d94aa60348c0cc`).

- `Redesign.refreshGridWidths()`: gọi lại `setGridWidth` theo chiều rộng
  khung chứa thật cho mọi lưới (nguyên nhân bóp méo theo §4.2: lưới
  chỉ đo một lần lúc tải). Tự chạy: sau tải trang 300ms, khi `load`,
  khi `resize` (debounce 150ms), khi `#page-wrapper` đổi class
  (MutationObserver theo dõi cơ chế nav-small sẵn có, không can thiệp
  handler), và khi nhận sự kiện `redesign:sidebar-changed`.
- `Redesign.makeGridScrollable(selector, {frozen})` — chiến lược A (§4.2):
  bọc `.ui-jqgrid` trong `.rd-grid-scroll` (`overflow-x:auto`).
  Cột neo: file jqGrid vendor **đã xác nhận có `setFrozenColumns`**,
  nhưng jqGrid chỉ neo được cột đã khai `frozen:true` trong `colModel`
  lúc tạo lưới — helper kích hoạt và trả `{wrapped, frozen}`, **không
  bịa cột neo** khi trang chưa khai. Khai frozen theo từng lưới (cột
  định danh nào) thuộc chặng sau, theo hồ sơ A/B/C.
- An toàn: guard khi jqGrid chưa nạp; try/catch từng lưới; bỏ qua lưới
  đang ẩn (chiều rộng 0) để khỏi `setGridWidth(0)` làm sập lưới.
- `node --check` OK cho cả hai file JS; CSS cân bằng ngoặc (tokens
  22/22, portal 11/11); eval stub (jQuery/jqGrid giả lập) nạp được và
  tạo `window.Redesign`.

## 4. VIỆC 2(c) — Shell portal mobile

**File chuẩn:** `scripts/redesign/assets/redesign-portal.css` +
`redesign-portal.js` (SSO wwwroot).

- Nguyên nhân gốc đã xác nhận: nút thu gọn `#make-small-nav` nằm trong
  khối `hidden-sm hidden-xs` của header portal → **không thấy được
  dưới 992px**; cơ chế sẵn có của nó chỉ toggle class `nav-small`
  trên `#page-wrapper` (desktop), không phải drawer.
- Thay đổi (chỉ cộng thêm, không sửa cơ chế desktop):
  - Nút hamburger `#rd-nav-toggle` (44×44px, `aria-label="Mở menu"`,
    `aria-expanded`) đặt cạnh logo, **chỉ hiện dưới 992px**
    (`display:none` trên desktop).
  - Drawer: `#nav-col` (khối menu, trừ `.login-foot`) thành fixed
    drawer 280px (tối đa 85vw), trượt từ trái khi `body.rd-drawer-open`.
    Lưu ý portal có **hai div trùng `id="nav-col"`** (menu + footer
    `.login-foot`, lỗi markup sẵn có — không sửa ở đây); rule dùng
    `:not(.login-foot)` để chỉ nhắm khối menu, **không chạm footer**.
  - Overlay `#rd-drawer-overlay` (bấm ngoài để đóng), nút đóng
    `.rd-drawer-close` trong NavCol, đóng bằng Esc, đóng khi bấm menu,
    đóng khi resize lên ≥992px, đóng xong focus trả về nút hamburger.
  - Khóa cuộn nền: body portal vốn đã có inline `overflow:hidden`
    nên trang nền không cuộn được; class drawer khẳng định lại trạng
    thái + overlay chặn thao tác vào nội dung sau.
  - JS viết thuần (không phụ thuộc jQuery nạp ở đâu trong SSOIndex);
    không chạm handler `#make-small-nav` / `nav-small`.
- Footer vendor tiếng Trung trong `_IndexNavCol.cshtml`: **chỉ liệt kê,
  không viết lại** (quy tắc chương trình) — diff NavCol chỉ có 1 dòng
  thêm (nút đóng), span footer nguyên vẹn.

## 5. Kiểm chứng STG — đã làm và chưa làm

Hạ tầng STG đã dựng cho đợt kiểm chứng này (đã dọn sau khi xong, xem §6):
Redis STG 6380 (conf `~/workspace/redis-stg/redis-stg.conf`, cần
`LD_LIBRARY_PATH` tới `redis-libs` theo `recovery.sh` §48),
BaseAPI STG 17001 (launcher gốc, code không đổi), PDS STG 15237,
SSO STG 15208. Chỉ ghi trên DB `*_STG` (mọi env đều trỏ STG; đợt này
chỉ thực hiện GET, không ghi gì thêm). Prod chỉ đọc, không chạm.

**Đã kiểm chứng (bằng chứng cụ thể):**

1. Build scratch 11/11 app, 0 lỗi (tối đa 2 build cùng lúc; output tại
   `~/workspace/foundation-scratch/<app>/`, ngoài repo nên không lo
   lọt DLL vào commit). PDS STG build lại sau patch tạm cổng (xem 4).
2. PDS STG 15237 (build HEAD + view nền tảng) lên và phục vụ:
   - `GET /css/redesign-tokens.css` → 200, 7349 byte, chứa token +
     `.rd-grid-scroll`.
   - `GET /js/redesign-grid.js` → 200, 5402 byte, chứa
     `makeGridScrollable`.
   - `GET /Login/Index` → 200.
3. Chuỗi đã biên dịch trong DLL (`.NET` lưu chuỗi UTF-16, phải dùng
   `strings -e l`): DLL PDS STG chứa `~/css/redesign-tokens.css`,
   `~/js/redesign-grid.js` và viewport (7 lần); DLL SSO scratch chứa
   nút `#rd-nav-toggle`, `#rd-drawer-overlay`, nút `.rd-drawer-close`,
   link portal/tokens/grid — tức view nền tảng biên dịch đúng.
4. SSO STG 15208 chạy bằng **prod-bin** (DLL 10:07, xem 5) phục vụ:
   - `GET /css/redesign-portal.css` → 200, chứa `rd-drawer-open`.
   - `GET /js/redesign-portal.js` → 200, chứa `rd-drawer-open`.
   - `GET /Login/Index` → 200; `GET /Home/SSOIndex` không auth → 302
     về `/Login/Index` (đúng hành vi auth, không phải lỗi).
5. **Phát hiện chặn (blocker) ngoài phạm vi nhưng phải báo:**
   build SSO từ HEAD hiện tại **không khởi động được** ở chế độ
   Development: lỗi DI validation
   `Unable to resolve service for type 'ISysStaffDmnService' while
   attempting to activate 'SysUserVDmnService'` (cùng 2 lỗi tương tự
   với `ISysRoleDmnService`). Bằng chứng phân biệt: cùng cwd + cùng
   env STG, DLL prod-bin 10:07 lên 15208 bình thường, DLL scratch
   build từ HEAD thì abort. Thay đổi của chặng này **không chạm file
   .cs/.csproj nào** (diff chỉ .cshtml + css/js/md/sh) nên lỗi này
   có sẵn ở HEAD, không phải do nền tảng. Nó chặn kiểm chứng live
   portal drawer từ build HEAD cho tới khi backend sửa đăng ký DI.
6. PDS STG cần patch tạm cổng: mọi app nghiệp vụ hardcode
   `builder.WebHost.UseUrls("http://127.0.0.1:52xx")` (chỉ SSO là
   không có UseUrls) và UseUrls thắng `ASPNETCORE_URLS` (đã chứng
   minh: lần chạy đầu với `ASPNETCORE_URLS=15237` vẫn bind 5237 và
   lỗi address in use). Cách làm đã dùng: sửa tạm literal 5237→15237
   trong `Program.cs`, build ra scratch, **khôi phục file ngay** và
   xác nhận `git diff` của file đó rỗng. Launcher STG gốc dùng worktree
   `openhis-stg` (commit cũ) cũng vì lý do này.

**Chưa kiểm chứng — KHÔNG báo đạt, chuyển cho điều phối viên:**

- HTML render thật sau đăng nhập (link tokens trong `_LayoutIndex`,
  drawer mở/đóng/overlay/focus trên viewport 390/768/1280, lưới đo
  lại sau khi sidebar đổi trạng thái, form một cột, trạng thái rỗng
  không còn đỏ): cần (1) sửa lỗi DI SSO ở HEAD (mục 5), (2) đăng nhập
  SSO (mật khẩu qua env `SSO_PASSWORD` — subagent không có và không
  được hỏi/xin credential thô), (3) trình duyệt thật (subagent không
  vận hành live browser). Điều phối viên dùng trình duyệt + tài khoản
  000000 trên STG: `http://127.0.0.1:15208` (portal) và
  `http://127.0.0.1:15237` (PDS) sau khi dựng lại STG theo §6.
- Ảnh kiểm chứng: **không chụp** (lý do trình duyệt như trên).
  Thư mục chuẩn cho chặng sau là
  `~/workspace/openhis-core/shots-redesign/foundation/` (đúng đường
  dẫn tuyệt đối này, tránh lặp cảnh ảnh audit nằm 3 nơi); thư mục
  hiện chưa tạo vì git không giữ thư mục rỗng.
- Kiểm chứng từng trang (hạ cấp nút primary thừa, khai frozen từng
  lưới, đổi câu chữ rỗng, gắn `.rd-form-stack`/`.rd-kpi-grid` cho
  trang home Layout=null): thuộc chặng sau, per-page có audit.

## 6. Vệ sinh, quy tắc cứng, và những gì KHÔNG làm

- Không commit (đúng yêu cầu). `git status`: 24 file sửa (toàn cộng),
  file mới gồm `ASSET-PIPELINE.md`, `assets/` (4 file), `sync-assets.sh`,
  wwwroot copies (24 file). **Không** có DLL/PDB/scratch/build artifact
  trong repo (scratch ở `~/workspace/foundation-scratch/`, ngoài repo).
  Không `git add` gì cả.
- `roles-shots-progress.md` (M) đã sửa sẵn **trước** khi worker bắt đầu
  (thấy ở lần `git status` đầu tiên) — không phải thay đổi của worker,
  để nguyên.
- SA password giữ nguyên; không ghi DB demo (chỉ GET; mọi env STG đều
  trỏ `*_STG`); không copy DLL dưới tiến trình đang chạy (build ra
  scratch, chạy tiến trình STG riêng, cổng riêng; prod 5208/5237/7001/
  41142 không khởi động lại).
- Dọn sau kiểm chứng: đã kill các tiến trình STG do worker khởi động
  (PDS STG, SSO STG prodtest, BaseAPI STG) và kiểm tra cổng STG đã
  trống; Redis STG cũng đã dừng. Prod không bị chạm.
- Không tự quyết: receipt-prefix, 22 quyết định mở, footer vendor
  tiếng Trung, tên bệnh viện thật, xoay secret — chỉ liệt kê/không đụng.
- Sửa BOM: lần ghi đầu bằng script đã vô tình lược BOM của 9 file vốn
  có BOM; đã khôi phục BOM đúng cho các file đó, diff cuối thuần cộng.

## 7. File giao

- `~/workspace/openhis-core/scripts/redesign/ASSET-PIPELINE.md`
- `~/workspace/openhis-core/scripts/redesign/assets/redesign-tokens.css`
- `~/workspace/openhis-core/scripts/redesign/assets/redesign-grid.js`
- `~/workspace/openhis-core/scripts/redesign/assets/redesign-portal.css`
- `~/workspace/openhis-core/scripts/redesign/assets/redesign-portal.js`
- `~/workspace/openhis-core/scripts/redesign/sync-assets.sh`
- Layout đã nối: 10 `_LayoutIndex.cshtml` + 11 `_Form.cshtml` +
  SSO `_Index.cshtml` + `SSOIndex.cshtml` + `_IndexNavCol.cshtml`
  (wwwroot copies đi kèm, sinh bởi sync script).
- Báo cáo này:
  `~/workspace/openhis-core/scripts/redesign/foundation-REPORT.md`
