# Kiểm chứng VISUAL nền tảng (foundation) — chặng 3 REDESIGN OpenHIS

> Worker kiểm chứng visual, ngày 2026-10-28 (ICT ~22:03–22:35, UTC 15:03–15:35).
> **KHÔNG commit, KHÔNG triển khai lên prod.** Báo cáo này là file duy nhất
> ngoài ảnh (ảnh trong `shots-redesign/foundation/`).
> Ngôn ngữ: tiếng Việt. Mobile = giả lập viewport (Playwright + Firefox
> local), KHÔNG phải app native, không tuyên bố test native.

## 0. Kết luận nhanh — đọc trước

| Hạng mục | Kết quả |
|---|---|
| (e) File nền tảng phục vụ 200 trên STG | ✅ ĐẠT — SSO STG + PDS STG, md5 trùng file chuẩn đã commit (xem §3) |
| Sửa DI b73a1026 trên STG | ✅ ĐẠT — SSO STG 15208 + PDS STG 15237 khởi động ở Development, **0 lỗi `Unable to resolve service`** trong log |
| (a) Drawer portal mobile | ❌ **CHƯA KIỂM CHỨNG LIVE** — bị chặn ở đăng nhập (xem §1) |
| (b) Lưới PDS resize/wrapper/frozen | ❌ **CHƯA KIỂM CHỨNG LIVE** — bị chặn ở đăng nhập; thêm phát hiện về phạm vi ở §4.2 |
| (c) Form mobile một cột | ❌ **CHƯA KIỂM CHỨNG LIVE** — bị chặn ở đăng nhập |
| (d) Lưới rỗng không còn đỏ | ❌ **CHƯA KIỂM CHỨNG LIVE** — bị chặn ở đăng nhập |
| Ảnh chụp | ⚠️ Chỉ 6 ảnh trang login + redirect (trang duy nhất tới được khi chưa đăng nhập) |

### Hai điều điều phối viên PHẢI biết ngay

**1. Sự cố prod SSO do chính worker này gây ra — ĐÃ TỰ HỒI PHỤC, không cần
hành động thêm (nhưng phải ghi nhận).** Lần build đầu tiên của worker dùng
`dotnet publish -c Release` theo đúng chữ task; `publish -o scratch` vẫn
build vào `bin/Release` của project trước rồi mới copy ra scratch, nên đã
ghi đè 2 DLL vào thư mục bin của tiến trình prod SSO đang chạy
(`NewtouchHIS.Base.Domain.dll` 15:05:32 UTC,
`HIS.SSO.DomainService.dll` 15:05:37 UTC — mtime đo được). Tiến trình prod
cũ sau đó (15:08:51 UTC) lỗi một request `/Login/Index` với
`TypeLoadException: Could not load type 'NewtouchHIS.Lib.Base.EnumExtend.DBEnum'`
rồi thoát; cổng 5208 mất listener. **Worker KHÔNG tự khởi động lại prod.**
Tiến trình SSO mới (PID 45191) đã lên lúc 15:16:56 UTC qua cơ chế recovery
sẵn có (log `sso.log` mới, truncate), trang login prod trả lại 200, log mới
0 lỗi DI — tức bộ DLL mới (đã gồm DLL Lib.Base sửa DI) chạy được, lỗi
15:08 chỉ là hệ quả của việc thay file dưới tiến trình đang chạy, không
phải bộ DLL mới không tương thích. Cửa sổ gián đoạn ≈ 8 phút
(15:08:51 → 15:16:56 UTC). Các app/API/center prod khác vẫn LISTEN suốt
dù bin của Center cũng bị build lại lúc 15:05 — **watch item:** nếu center
(8015/8016) hoặc API nào sau này lỗi lazy-load tương tự thì nguyên nhân
nghi vấn đầu tiên là đợt ghi bin này. Bài học: **không bao giờ publish
Release trực tiếp từ cây source prod đang chạy; luôn dùng Debug publish
(bin/Debug tách biệt) như tiền lệ của worker sửa DI.**

**2. Kiểm chứng (a)–(d) BỊ CHẶN vì không có mật khẩu tài khoản 000000.**
`SSO_PASSWORD` không có trong shell của worker; quét nhanh không thấy file
nào gán nó (khớp kết luận của các báo cáo sweep-final trước); README
vendor gốc không ghi mật khẩu mặc định. **Số lần thử đăng nhập 000000
trong phiên này: 0** — worker không đoán, không thử live lần nào, đúng
quy tắc (thử sai có thể khóa tài khoản; worker sửa login staging trước
đây cũng dừng ở đúng điểm này). Mọi trang cần kiểm chứng (portal
SSOIndex, lưới/form PDS) đều yêu cầu đăng nhập nên không tới được.
**Để hoàn tất (a)–(d) chỉ cần đúng một thứ:** mật khẩu 000000 qua kênh
đã dùng cho các lần kiểm chứng trước, rồi chạy đúng 1 lần login trên STG
(theo mẫu §5 của báo cáo stg-login-fix, `--max-time 30` trở lên); nếu
lần đó sai thì DỪNG, không thử lại. Ngoài ra worker đã làm xong toàn bộ
phần không cần đăng nhập (dựng STG, DI, file 200, ảnh trang login) để lần
chạy bổ sung chỉ còn bước login + probe.

## 1. Hạ tầng STG đã dựng (đúng thứ tự, kiểm tra cổng trước khi chạy)

SQL Server 1433 sống suốt phiên (không phải nguyên nhân của bất kỳ lỗi
nào dưới đây). Tất cả tiến trình STG đã dọn cuối phiên (xem §7).

| Thành phần | Cổng | PID (đã dọn) | Kết quả |
|---|---|---|---|
| Redis STG | 6380 | 42618 | LISTEN, dùng đúng `LD_LIBRARY_PATH=~/workspace/redis-libs/usr/lib/x86_64-linux-gnu` |
| BaseAPI STG | 17001 | 42666 (launcher 42665) | LISTEN, `Development`, root trả 404 (đúng — không có route root) |
| SSO STG (scratch mới) | 15208 | 43722 (launcher 43721) | LISTEN, `Development`, login 200, **0 lỗi DI** |
| PDS STG (scratch mới) | 15237 | 43926 (launcher 43924) | LISTEN, `Development`, login 200, **0 lỗi DI** |

Script chạy STG của phiên này (ngoài repo, không commit):
`~/workspace/foundation-visual-scratch/run-sso-visual-stg.sh`,
`run-pds-visual-stg.sh` (env STG: DB `*_STG`, BaseAPI 17001, Redis 6380,
Development; cwd = thư mục project source để wwwroot phục vụ từ source).

### Độ lệch so với task — build Release → Debug (có lý do, có bằng chứng)

Task yêu cầu build Release sang scratch mới. Worker đã thử 3 cách:

1. `publish -c Release -o scratch` (mặc định): **thất bại MSB3027** — copy
   PDB vào `bin/Release` bị tiến trình prod giữ (đây cũng là lần ghi đè
   2 DLL prod nêu ở §0; các lần copy PDB thì thất bại nên PDB prod còn
   nguyên, nhưng DLL đã kịp bị ghi đè trước khi tới bước copy PDB).
2. `publish -c Release` + redirect `BaseIntermediateOutputPath` /
   `BaseOutputPath` chung: **thất bại MSB4006** (circular dependency).
3. Cùng redirect nhưng kèm `$(MSBuildProjectName)` theo từng project:
   **thất bại biên dịch** (`OrderByType` không tồn tại trong
   `SugarRepository.cs` — đường obj bị đổi làm vỡ resolve, trong khi
   build mặc định biên dịch được tới bước copy).
4. `publish -c Debug -o scratch` (tiền lệ của worker sửa DI): **thành
   công cả SSO lẫn PDS**, 0 lỗi, DLL ra scratch đầy đủ.

Vì vậy scratch cuối cùng là **Debug, không phải Release**. Ảnh hưởng tới
kết luận kiểm chứng là **không đáng kể và được ghi rõ**: Razor view biên
dịch như nhau ở cả hai cấu hình; file tĩnh phục vụ từ wwwroot source
(cwd) chứ không từ output build; validation DI là do môi trường
Development quyết định, không do Debug/Release. Điều phối viên muốn
khẳng định Release tuyệt đối thì phải build trên cây source không có
tiến trình prod nào đang chạy (hoặc sau khi dừng prod theo quy trình
deploy), không thể build Release an toàn từ cây này trong khi prod sống.

### PDS: patch tạm cổng + DLL Lib.Base (làm đúng quy trình)

- `Program.cs` PDS: sửa tạm literal `5237→15237` trước build, **khôi
  phục ngay sau build, `git diff` của file đó đã xác nhận rỗng**.
- PDS tham chiếu Lib.Base qua HintPath của run-copy (DLL cũ
  `fa27fc8f0878e636a85f8b75e6a10c61`), không phải `his-dll-common`.
  Theo đúng thủ tục của worker sửa DI, worker đã **copy DLL Lib.Base đã
  sửa (`8eaae85e513ca28d0c06f79eae30eb2c`, trùng file đã commit ở
  `his-dll-common/Net6/`) vào thư mục scratch PDS TRƯỚC khi chạy PDS**
  (app chưa chạy — không copy dưới tiến trình đang chạy). SSO scratch
  đã tự mang DLL sửa (md5 trùng) vì HintPath của SSO trỏ `his-dll-common`.
- **Hệ quả cần quyết định ở tầng deploy (không tự làm):** DLL Lib.Base
  cho PDS và các app dùng HintPath run-copy vẫn là bản cũ trong cây
  run-copy; sửa DI chỉ thực sự tới được các app đó khi DLL mới được đưa
  vào vị trí HintPath của chúng (worker sửa DI đã nêu ở §6 báo cáo của
  họ; worker này chỉ thay trong scratch, không đụng run-copy hay repo).

## 2. Kiểm chứng DI (b73a1026) trên STG — ĐẠT

- SSO STG 15208 (build HEAD + DLL sửa): log `Hosting environment:
  Development`, `Now listening on: http://127.0.0.1:15208`, số lần
  `Unable to resolve service` = **0**, `GET /Login/Index` = 200.
- PDS STG 15237 (build HEAD + DLL sửa thay trong scratch): tương tự,
  `Development`, `Now listening on: http://127.0.0.1:15237`, **0 lỗi DI**,
  `GET /Login/Index` = 200.
- Chuỗi drawer trong DLL SSO scratch (`.NET` lưu UTF-16, dùng
  `strings -e l` + `strings`): có `rd-nav-toggle`,
  `#rd-drawer-overlay`, nút `.rd-drawer-close`, link
  `~/css/redesign-portal.css` + `~/js/redesign-portal.js` — view portal
  nền tảng biên dịch đúng trong DLL scratch.
- Trang login SSO STG (HTML 8965 byte) **không chứa** link redesign nào
  (probe Playwright: `tokens_link/grid_js/portal_css/portal_js` đều
  `false`) — đúng thiết kế: trang login dùng layout riêng, không nạp
  file nền tảng; drawer chỉ có trên portal sau đăng nhập.

## 3. (e) File nền tảng phục vụ 200 trên STG — ĐẠT

Đo bằng curl `--max-time 30`, proxy env unset:

| File | SSO STG 15208 | PDS STG 15237 | md5 (khớp file chuẩn đã commit) |
|---|---|---|---|
| `/css/redesign-tokens.css` | 200, 7349 byte | 200, 7349 byte | `34b3eb4531b2ad9ee5c5ccb6dc487bda` |
| `/js/redesign-grid.js` | 200, 5402 byte | 200, 5402 byte | `77c730c48fcf74c913d94aa60348c0cc` |
| `/css/redesign-portal.css` | 200, 3600 byte, chứa `rd-drawer-open` ×4 | — (chỉ SSO) | — |
| `/js/redesign-portal.js` | 200, 3120 byte, chứa `rd-drawer-open` ×3 | — (chỉ SSO) | — |
| `GET /Login/Index` | 200 | 200 | — |
| `GET /Home/SSOIndex` (chưa auth) | **302 về `/Login/Index`** (đúng hành vi auth, không phải lỗi) | — | — |

## 4. (a)–(d): CHƯA KIỂM CHỨNG LIVE — lý do + bằng chứng thay thế + phát hiện về phạm vi

**Không báo đạt phần nào dưới đây.** Những gì ghi ở đây là bằng chứng
mức code/tĩnh để lần chạy bổ sung (sau khi có mật khẩu) đi thẳng vào
đúng trang, không phải kết luận đạt.

### 4.1 (a) Shell portal mobile — chưa kiểm chứng live

- Cần: portal sau đăng nhập (`/Home/SSOIndex`) ở viewport 390: nút
  `#rd-nav-toggle` thấy được dưới 992px, bấm mở drawer, overlay, nút
  đóng, Esc, bấm menu đóng drawer, focus trả về hamburger; desktop:
  drawer ẩn, cơ chế `nav-small` cũ nguyên vẹn.
- Đã đọc code JS portal thật (`redesign-portal.js`): mở/toggle bằng
  nút hamburger, đóng bằng nút đóng / overlay / Esc / resize ≥992px,
  đóng khi bấm link menu trong drawer. **Lưu ý cần điều phối viên xác
  nhận kỳ vọng (không phải lỗi live):** code hiện tại đóng khi bấm menu
  bằng `close(false)` — tức **không trả focus về nút hamburger** (hợp lý
  vì bấm menu là điều hướng/mở tab), trong khi đóng bằng nút đóng /
  overlay / Esc dùng `close(true)` — có trả focus. Nếu tiêu chí (a) yêu
  cầu focus trả về hamburger **cả khi bấm menu** thì đó là một lệch nhỏ
  giữa tiêu chí và code, cần quyết định sửa code hay sửa tiêu chí; worker
  không tự đổi code.
- Bằng chứng thay thế đã có: chuỗi drawer trong DLL scratch (§2); trang
  login không có drawer (probe `rd_toggle/rd_overlay = false`) — đúng vì
  drawer chỉ thuộc portal sau đăng nhập.

### 4.2 (b) Lưới PDS — chưa kiểm chứng live + phát hiện phạm vi quan trọng

- Cần: resize cửa sổ + đổi trạng thái sidebar → lưới đo lại chiều rộng
  (không bóp méo); wrapper cuộn ngang `.rd-grid-scroll` có mặt; cột neo
  ở lưới đã khai frozen.
- **Phát hiện 1 — layout nối nền tảng không bao phủ trang lưới PDS phổ
  biến nhất:** PDS `_Index.cshtml` (layout của `StockQuery`,
  `FypzIndex` và hầu hết trang Index/query — các trang này chạy trong
  iframe của shell) **không nạp** `redesign-tokens.css` /
  `redesign-grid.js`; chỉ `_LayoutIndex.cshtml` và `_Form.cshtml` có
  nạp. Hệ quả: dù helper có đúng, các trang `_Index` cũng không được
  hưởng resize/wrapper/trạng thái rỗng mới. Đây là giới hạn phạm vi của
  chặng nền tảng (foundation-REPORT §2 cũng ghi home/Layout=null là
  việc chặng sau), nhưng điều phối viên cần chốt cho lần kiểm chứng sau:
  hoặc (i) kiểm chứng (b)/(d) trên các trang `_Form` có lưới (dưới đây),
  hoặc (ii) mở rộng phạm vi nối `_Index` ở chặng sau rồi mới kiểm chứng
  trên `StockQuery`/`FypzIndex`. Worker không tự nối thêm layout.
- **Phát hiện 2 — `_LayoutIndex` hiện không có view PDS nào dùng:**
  quét toàn repo, PDS không có view nào chọn layout `_LayoutIndex`
  (các view chọn `_Index`, `_Form`, hoặc `Layout = null`). Nối
  `_LayoutIndex` ở PDS hiện chỉ có tác dụng dự phòng/nhất quán, không
  tạo ra trang lưới nào để kiểm chứng trong phạm vi PDS.
- **Mục tiêu đúng cho lần kiểm chứng (b)/(d) sau khi có login (trang
  `_Form` có lưới jqGrid, đã quét từ source):**
  `Views/DrugStorage/PreViewInfo.cshtml`, `Views/Medicine/SyncDeptDrug.cshtml`,
  `Views/OutPatientPharmacy/ChooseCf.cshtml`,
  `Views/SysMedicine/SysMedicineAdd.cshtml`,
  `Views/SysMedicine/YbbxblForm.cshtml`,
  `Areas/OutOrInStoredManage/Views/DeliveryDirect/From.cshtml`,
  `Areas/OutOrInStoredManage/Views/Purchase/BillView.cshtml`,
  `Areas/OutOrInStoredManage/Views/Purchase/PurchaseView.cshtml`.
- **Cột neo (frozen): không có lưới PDS nào để kiểm chứng.** Quét toàn
  bộ view PDS: **0 chỗ khai `frozen:true`** (helper theo thiết kế không
  bịa cột neo khi trang chưa khai — foundation-REPORT §3). Lưới khai
  frozen thật chỉ có ở EMR (`NursingRecord/Index`) và Base HOSP
  (`Combination/Index`, `materialIndex`, `CatalogComparison`) — ngoài
  phạm vi task lần này (chỉ SSO + PDS). Nếu muốn kiểm chứng cột neo
  live thì cần một task riêng build EMR/HOSP STG, hoặc một lưới PDS
  được khai frozen ở chặng sau.

### 4.3 (c) Form mobile một cột — chưa kiểm chứng live

- Mục tiêu đúng sau khi có login: trang `_Form` có `table.form`, ví dụ
  `Views/Dictionaries/FypzForm.cshtml` (mở từ `FypzIndex`, chỉ xem —
  không submit nghiệp vụ). Rule một cột (<767px, nhãn trên trường) nằm
  trong `redesign-tokens.css` đã phục vụ 200 đúng md5 (§3); hiệu ứng
  live trên form thật chưa đo được vì chưa vào được form (cần đăng nhập
  + điều hướng từ `FypzIndex`).

### 4.4 (d) Lưới rỗng không còn đỏ — chưa kiểm chứng live

- Rule trung hòa `.ui-jqgrid-bdiv > .unwritten` nằm trong
  `redesign-tokens.css` đã phục vụ đúng md5; nhưng trạng thái rỗng live
  (lọc không ra kết quả trên một lưới `_Form`, xem danh sách §4.2) chưa
  quan sát được vì chưa đăng nhập. Lưu ý: trang `StockQuery` (nơi audit
  cũ thấy chữ rỗng đỏ) dùng layout `_Index` nên **sẽ vẫn đỏ** cho tới
  khi `_Index` được nối nền tảng — đừng dùng `StockQuery` làm bằng
  chứng đạt/không đạt cho (d) ở chặng này.

## 5. Ảnh đã chụp (trang tới được khi chưa đăng nhập)

Thư mục bắt buộc: `~/workspace/openhis-core/shots-redesign/foundation/`
(Playwright + Firefox local, 0 lỗi console, 0 pageerror trong cả 2
viewport; lần chạy đầu timeout screenshot do tải hệ thống nặng, chạy lại
với timeout 120s thì đủ 6/6 ảnh).

| Ảnh | Nội dung xác nhận bằng mắt |
|---|---|
| `sso-login-desktop.png` (647 KB) | Trang login SSO STG thật: form “chào mừng / đăng nhập”, ô tài khoản + mật khẩu, nút “đăng nhập”; footer vendor tiếng Trung (chỉ liệt kê, không viết lại) |
| `sso-login-mobile.png` (134 KB) | Cùng trang ở viewport 390 — **quan sát thêm ngoài phạm vi nền tảng:** thẻ login tràn mép phải, footer bị cắt (lỗi tràn login đã biết từ audit; trang login không nạp tokens nên nền tảng không sửa nó ở chặng này) |
| `sso-ssoindex-unauth-desktop.png` / `-mobile.png` | Nội dung giống hệt trang login (bằng chứng trình duyệt: truy cập `/Home/SSOIndex` khi chưa auth bị đưa về `/Login/Index`; URL cuối là `/Login/Index`, probe `txt_account = true`) |
| `pds-login-desktop.png` (419 KB) / `pds-login-mobile.png` (138 KB) | Trang login PDS STG thật, tiếng Việt + footer vendor tiếng Trung |

Không có ảnh nào cho (a)–(d) vì chưa đăng nhập được — không dựng ảnh giả
từ trang login để mạo nhận là portal/lưới.

## 6. Những gì KHÔNG làm / không đụng tới

- Không commit (kể cả không `git add` gì). `git status` cuối phiên: chỉ
  `M roles-shots-progress.md` (đã dirty từ trước phiên, thuộc điều phối
  viên — foundation-REPORT §6 và di-fix-REPORT §7 đều ghi) và file báo
  cáo này (untracked, theo đúng yêu cầu task). `Program.cs` của PDS đã
  khôi phục, diff rỗng (§1).
- Không ghi DB nào: toàn bộ phiên chỉ GET (login page, file tĩnh);
  **0 POST đăng nhập, 0 submit form**; mọi env STG đều trỏ DB `*_STG` +
  Redis 6380. Không ghi DB demo.
- SA password giữ nguyên; không secret nào xuất hiện trong báo cáo này
  (mật khẩu chỉ nằm trong file env có sẵn, không trích dẫn; mật khẩu
  demo 000000 không được biết, không được đoán, không được ghi ra).
- Không copy DLL dưới tiến trình đang chạy, **trừ sự cố §0** (publish
  Release ghi đè bin prod — đã khai báo đầy đủ, không giấu).
- Footer tiếng Trung chỉ liệt kê; receipt-prefix ngoài phạm vi, không đụng.
- Không tuyên bố test app native (mobile = viewport emulation).

## 7. Dọn dẹp cuối phiên (đã xác minh)

Đã kill đúng PID các tiến trình STG do worker khởi động: Redis STG
42618, BaseAPI STG 42666 (+launcher 42665), SSO STG 43722 (+launcher
43721), PDS STG 43926 (+launcher 43924). Kiểm tra sau kill: các cổng
15208/15237/17001/6380 đều hết LISTEN; không còn tiến trình dotnet STG
nào; BaseAPI prod 7001 (PID 2467) và PDS prod 5237 còn sống. Trạng thái
prod cuối phiên: SSO 5208 **đã hồi phục** (PID 45191, login 200, log mới
0 lỗi DI — xem §0); các cổng prod còn lại LISTEN bình thường; SQL 1433
sống. Không tự khởi động lại bất kỳ app prod nào trong phiên này.

## 8. Cần điều phối viên quyết / cung cấp để hoàn tất (a)–(d)

1. **Mật khẩu 000000** qua kênh đã dùng trước đây (bắt buộc, duy nhất):
   sau khi có, chạy đúng 1 lần login STG (SSO 15208 + PDS 15237 cần dựng
   lại theo §1, script đã để sẵn trong
   `~/workspace/foundation-visual-scratch/`), rồi probe theo checklist
   §4.1–§4.4 trên các trang mục tiêu đã liệt kê. Nếu lần login đó sai:
   DỪNG, không thử lại, báo lại thông điệp trả về.
2. **Chốt kỳ vọng focus khi bấm menu trong drawer** (§4.1): giữ code
   `close(false)` hiện tại hay yêu cầu trả focus cả khi bấm menu?
3. **Chốt phạm vi kiểm chứng (b)/(d):** dùng các trang `_Form` có lưới
   (§4.2) hay mở rộng nối `_Index` trước rồi mới kiểm chứng
   `StockQuery`/`FypzIndex`? (Quyết định chặng sau; worker không tự nối.)
4. **Ghi nhận sự cố §0:** bộ DLL prod SSO hiện là hỗn hợp mới (Domain/
   DomainService build 15:05 + Lib.Base sửa DI + HIS.SSO.dll cũ) nhưng đã
   chạy ổn sau restart (login 200, 0 lỗi DI); điều phối viên quyết xem
   có cần rebuild sạch bin prod SSO ở đợt deploy tới hay giữ nguyên.
5. **Kiểm chứng cột neo live** (nếu cần): cần task riêng cho EMR/HOSP
   STG (nơi duy nhất có lưới khai frozen) hoặc đợi chặng sau khai frozen
   cho một lưới PDS.

## 9. File giao

- Báo cáo này:
  `~/workspace/openhis-core/scripts/redesign/foundation-visual-REPORT.md`
- Ảnh (6 file, gitignored hay chưa là do cấu hình repo — worker chỉ ghi
  đúng thư mục bắt buộc):
  `~/workspace/openhis-core/shots-redesign/foundation/sso-login-desktop.png`,
  `sso-login-mobile.png`, `sso-ssoindex-unauth-desktop.png`,
  `sso-ssoindex-unauth-mobile.png`, `pds-login-desktop.png`,
  `pds-login-mobile.png`
- Scratch + script + log của phiên (ngoài repo, không commit):
  `~/workspace/foundation-visual-scratch/` (`sso/`, `pds/`,
  `run-sso-visual-stg.sh`, `run-pds-visual-stg.sh`, `shot_stg2.py`,
  `sso-stg.log`, `pds-stg.log`, `baseapi-stg.log`, `shot_stg2.log`)
