# FIX-1 — Báo cáo vòng fix (SSO + MRQC + Center + Sett)

- Worker: FIX-1. Base: V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core`.
- Ngày: 2026-10-28 (ICT). Không deploy prod (redeploy là worker riêng sau đó).
- Không ghi database demo: mọi ghi kiểm chứng đều trên DB `*_STG`, đã dọn sạch
  (xác nhận count về baseline ở từng mục). Không đụng `roles-shots-progress.md`.
- Tài khoản kiểm chứng: chỉ dùng đúng `000000`/`000000` (web gõ `000000` vào ô
  mật khẩu; POST CheckLogin dùng `password = md5(000000)` tính bằng `md5sum`).
  Không lần nào login báo khóa/sai; không đoán mật khẩu.
- Lưu ý môi trường: lúc nhận việc VM vừa restart nên không còn tiến trình nào
  chạy. Worker tự khởi động lại hạ tầng dùng chung cần cho staging (SQL Server
  1433, Redis staging 6380, static host 41142) và cụm staging riêng của mình.
  Tất cả app staging chạy từ bản publish sang thư mục scratch
  `~/workspace/fix1-scratch/` (baseapi/sso/sett/mrqc), KHÔNG ghi đè bin prod,
  KHÔNG dùng launcher prod. Cổng staging riêng để tránh đụng worker khác:
  BaseAPI 17011, SSO 15209, Sett 15222, MRQC 15224 (Sett/MRQC hard-code
  `UseUrls` cổng prod trong `Program.cs` nên build staging dùng git worktree
  riêng `~/workspace/fix1-build` ở HEAD + copy đúng file đã sửa + đổi literal
  cổng; worktree này đã gỡ sau khi build xong, không commit từ đó).
- Build: tuần tự, mỗi lần 1 build. 6 lần publish (SSO, Sett, MRQC, BaseAPI,
  rồi publish lại SSO + MRQC sau fix mobile) đều 0 error (chỉ warning có sẵn
  như MVC1000/MSB3277). Lỗi duy nhất gặp khi publish lại là file PDB/DLL đang
  bị tiến trình staging giữ (MSB3027) — đã dừng đúng tiến trình staging rồi
  publish lại thành công, không phải lỗi code.
- Cạm bẫy đã gặp và sửa: lần chạy staging đầu tiên lỗi `WRONGPASS` vì worker
  đã copy literal chuỗi `<redacted>` (hệ thống che secret trong output) thành
  giá trị mật khẩu trong launcher scratch; đã sửa launcher dùng biến env thật
  (`MSSQL_SA_PASSWORD`, `REDIS_STG_PASS` từ file env 0600). Không secret nào
  xuất hiện trong báo cáo này hay trong commit.
- Trong lúc kiểm chứng, tiến trình Sett scratch từng bị watchdog/coordinator
  kill (SIGTERM, log "Application is shutting down...") vì nó làm kẹt guard
  pgrep của `recovery.sh` khi khôi phục Sett prod 5221 — xem chi tiết và
  cách xử lý ở mục "Thông báo vận hành" cuối báo cáo. Cuối phiên đã kill
  toàn bộ 4 app scratch để trả RAM và tránh nhầm lẫn; SQL/Redis/static để
  lại chạy cho các worker khác.

## Kết quả tổng quan

| Bug | Kết quả |
|---|---|
| A-01 SSO Notice msgtype=1 | SỬA + kiểm chứng STG (cả 2 nhánh, có dữ liệu thật) |
| A-02 SyncForm AddRange | SỬA + kiểm chứng STG (cây 3 tầng đúng liên kết) |
| Sett B1 title/appId login | SỬA + kiểm chứng STG |
| Sett B2 resize() mobile login | SỬA + kiểm chứng STG (đo + ảnh) |
| Sett B3 GetSettlementGridJson null | SỬA + kiểm chứng STG |
| Sett B4 title BusinessRanking | SỬA + kiểm chứng STG |
| Sett PatientInHosList keyword | SỬA 1 phần (thêm field VO) + ghi GAP server (stub chưa lọc) |
| SETT-03 BaseData/SelectSfxmYp | KHÔNG SỬA — GAP đã xác nhận (gốc cũng không có endpoint) |
| SSO-07/MRQC-05 mobile | SỬA + kiểm chứng STG (đo + ảnh); còn tồn 2 giới hạn ghi rõ dưới đây |

## 1. A-01 (cao) — SSO Notice inbox msgtype=1 luôn lỗi

- Đối chiếu HEAD: bug CÓ trong HEAD
  (`Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/MsgQueue/MsgQueueDmnService.cs`,
  `NoticeQueryPage`, dòng ~411).
- Cách sửa: bỏ toán tử 3 ngôi mà SqlSugar không dịch được
  (`SqlFunc.IIF + Contains`), viết lại thành OR của 2 nhánh AND, giữ nguyên
  ngữ nghĩa: `RecipientType == user` thì khớp bằng (`==`), các loại còn lại
  thì khớp `Contains`. `RecipientType` là `int` không nullable nên 2 nhánh
  phân hoạch đúng toàn bộ dòng như toán tử 3 ngôi cũ.
- Bằng chứng STG (SSO 15209 + BaseAPI 17011, session `000000` thật):
  - Trước fix (sweep-A): msgtype=1 → lỗi `SqlFunc.IIF ... argument do not
    support`; msgtype=2/999 → OK.
  - Sau fix, chưa có dữ liệu: msgtype=1/2/999 đều → `{"rows":[],"total":0}`.
  - Kiểm chứng dương tính với dữ liệu thật: chèn 2 dòng fixture vào
    `NewtouchHIS_Base_STG.dbo.Msg_NoticeQueue` (org demo
    `6d5752a7-...`, nhóm `0851787a-...`, `SendFrom='000000'`):
    `FIX1-A01-USER` (`RecipientType=1`, `Recipient='000000'`, nhánh bằng) và
    `FIX1-A01-GROUP` (`RecipientType=2`, `Recipient='000000'`, nhánh Contains).
    `POST /SysManage/Notice/GetNoticeList` với `msgtype=1` trả cả 2 dòng
    (kèm `SendFromName`/`RecipientName` = quản trị viên, `AppName` MRQC).
    msgtype=2 cũng trả cả 2 dòng (lọc theo `SendFrom`).
  - Dọn sạch: `DELETE ... WHERE Id LIKE 'FIX1-A01%'` → count `0`, tổng số
    dòng `Msg_NoticeQueue` STG về `0` (baseline).
  - Hồi quy: trang `/SysManage/Notice/Index` → 200.

## 2. A-02 (cao) — SyncForm AddRange lỗi liên kết cây

- Đối chiếu HEAD: bug CÓ trong HEAD
  (`Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysModuleDmnService.cs`,
  `AddRange`).
- Căn nguyên đúng như sweep-A: code tra cha trong `parentMenu` (danh sách VO
  trước khi đổi Id, vẫn mang Id gốc) và khớp theo `Name`, nên bản sao con
  trỏ Id gốc; bản sao gốc không bao giờ được gán `defaultParentId`.
- Cách sửa: lập map Id gốc → Guid mới của bản sao. Bản sao gốc được gán
  `defaultParentId` khi có (cha tùy chỉnh do `SubmitMenuSync` tạo hoặc node
  cha có sẵn đã chọn); không có thì giữ root. Bản sao con được gán Guid mới
  trước, rồi `ParentId` được remap qua map theo Id (kể cả tầng sâu hơn, khi
  cha của con cũng là một bản sao con); cha không nằm trong tập copy thì
  giữ hành vi cũ (`defaultParentId ?? giá trị gốc`).
- Bằng chứng STG (đúng bước tái hiện sweep-A, payload form không pre-encode
  để tránh lỗi double-encode `%20` của harness sweep-A):
  - Nguồn: `Newtouch_MRQC_STG.Sys_Module` gốc
    `20d5a18a-62d5-4ba4-b806-fb97a710f782` ("Kiem soat chat luong Quan ly")
    + con `24a940d0-badd-48fe-a97b-591d8c4f1932`
    ("Trung tam kiem soat chat luong"); cả 2 tên chưa có trong Union_STG.
  - `POST /SysManage/SysModule/SubmitMenuSync` với `parentMenu=FIX1-SYNC-PARENT`,
    `fromAppId=MRQC` → `{"state":"success"}`.
  - Đối chiếu `Newtouch_Union_STG.Sys_Module`:
    - `63d832b0-...` `FIX1-SYNC-PARENT`, `ParentId = NULL` (cha mới, đúng);
    - `85b00c96-...` bản sao gốc, `ParentId = 63d832b0-...` (trước fix là NULL);
    - `c9c872bf-...` bản sao con, `ParentId = 85b00c96-...` (trước fix là Id
      gốc `20d5a18a-...`, dangling ở DB Union).
  - Dọn sạch: xóa đúng 3 Id → count `0`; nguồn MRQC_STG còn nguyên 2/2.
  - Hồi quy: `GetTreeGridJson` với đúng `queryParams` của UI
    (`keyword` + `validLimit`) → trả cây menu JSON; trang
    `/SysManage/SysModule/Index` → 200. (Lưu ý: gọi thiếu object
    `queryParams` vẫn NRE tại `SysModuleBaseController` dòng 156 — đó là
    A-07 đã biết từ sweep-A, ngoài phạm vi FIX-1, không thay đổi.)

## 3. Sett B1 (thấp) — view login Sett mang title + appId MRQC

- Đối chiếu HEAD: bug CÓ trong HEAD
  (`Newtouch.Sett.Web/Views/Login/Index.cshtml` dòng 15 và 159).
- Cách sửa: title → `Sett Hệ thống quản lý thu phí` (theo mẫu title MRQC và
  title Home Sett "Sett — Hệ thống quản lý thu phí"); `appId` → `'Sett'`.
  Hành vi session cookie giữ nguyên là Sett (cookie thực tế đã là
  `Sett_newtouch_loginuserkey_2016` từ config `AppId: "Sett"`); việc đổi
  appId chỉ sửa đúng tên cookie lỗi đăng nhập và lệnh xóa cookie khi mở
  trang login (trước đó xóa nhầm `MRQC_...`).
- Bằng chứng STG (Sett 15222, trang login không cần auth): HTML trả
  `<title>Sett Hệ thống quản lý thu phí</title>` (encode HTML entity nên
  grep chuỗi thô trượt — đã xác nhận bằng tag title), `var appId = 'Sett'`,
  0 lần xuất hiện `MRQC`. Login Sett staging bằng `000000` thành công,
  cookie session là Sett, `/Home/Index` → 200.

## 4. Sett B2 — view login Sett dùng resize() theo screen.width/1920

- Đối chiếu HEAD: bug CÓ trong HEAD (chỉ ở view login; grep toàn Sett Web
  chỉ thấy `screen.width / 1920` ở đúng file này — các trang sau login
  không có cơ chế này, không đụng tới).
- Cách sửa (chỉ view login): xóa hàm `resize()` (scale toàn trang theo
  `screen.width/1920`), `window.onresize` và lệnh gọi `resize()` của nó.
  Giữ nguyên cơ chế căn giữa theo chiều cao trong `$.login.init()`
  (`.wrapper`/`.container`, không scale). Bổ sung thêm media query
  (≤768px, chỉ trong view login) để card 280px vốn `float:right` +
  `margin-right:100px` không tràn điện thoại 390px: card căn giữa
  (`float:none; margin:40px auto 0`), header `width:auto`. Desktop không đổi.
- Bằng chứng STG (Playwright + Firefox, viewport 390×844, trang login Sett):
  - Trước fix (sweep-B2): `body.style.transform = scale(0.711...)`,
    `body.style.width = 548.17px`; trên điện thoại thật
    (`screen.width=390`) sẽ còn ~20%.
  - Sau fix: `body.style.transform = none`, `bodyWidth = ""`,
    `document.scrollWidth == clientWidth == 390` (tràn 0, không phần tử
    nào vượt viewport); card `.login-b` tại `x=15, w=360, right=375`
    (căn giữa, lề 15px 2 bên). Ảnh:
    `shots-sweep/fix1-sett-login-mobile.png` (thư mục này đã gitignore,
    không vào commit).

## 5. Sett B3 (trung bình) — GetSettlementGridJson lỗi null mọi lần tải

- Đối chiếu HEAD: bug CÓ trong HEAD
  (`Newtouch.Sett.Web/Areas/OutpatientManage/Controllers/SettlementController.cs`
  dòng ~55: `list.Select(...)` trong khi service stub trả null).
- Cách sửa: controller guard — `list = GetSettlementList(...) ?? new
  List<OutpatientSettlementVO>()`, trả đúng dạng jqGrid
  `{rows, total, page, records}` (rỗng khi không có dữ liệu, không bịa dữ
  liệu). Guard thêm `GetSettZffsResultList(...) ?? new List<SettZffsDto>()`
  để tránh NRE thứ hai cùng họ. Không sửa service stub thành dữ liệu giả;
  khi nào có service thật thì lưới mới có dòng (ghi rõ trong comment code).
- Bằng chứng STG (session Sett thật): `POST GetSettlementGridJson` không
  tham số và có `keyword=test&page=1&rows=10` đều →
  `{"rows":[],"total":0,"page":1,"records":0}` (trước fix:
  `{"state":"error","message":"Value cannot be null. (Parameter 'source')"}`).
  Trang `/OutpatientManage/Settlement/SettQuery` → 200 (kiểm chứng lại sau
  khi Sett scratch bị kill và khởi động lại — vẫn đúng dạng).

## 6. Sett B4 (thấp) — title BusinessRanking hard-code "Theo dõi thuốc"

- Đối chiếu HEAD: bug CÓ trong HEAD
  (`Newtouch.Sett.Web/Areas/DeanInquiryManage/Views/BusinessRanking/Index.cshtml`
  dòng 2).
- Cách sửa: `ViewBag.Title = "Xếp hạng nghiệp vụ"` (khớp nội dung trang:
  xếp hạng khối lượng công việc/thu nhập bác sĩ và khoa, gồm 4 bảng xếp hạng
  trong view).
- Bằng chứng STG: trang → 200, chứa `Xếp hạng nghiệp vụ`, không còn
  `Theo dõi thuốc`.

## 7. Sett PatientInHosList — view gửi keyword nhưng VO rỗng

- Đối chiếu HEAD: đúng như sweep-B — `PatientListInputVO`
  (`Newtouch.Sett.Infrastructure/Domain/Types_PatientManage.cs`) là class
  rỗng; view `PatientManage/PatientInHosList/Index.cshtml` gửi
  `postData: { keyword: $("#txt_keyword").val() }` tới `LoadData`.
- Cách sửa: thêm field `public string keyword { get; set; }` vào VO
  (khớp tên tham số view gửi; model binding không phân biệt hoa/thường)
  để keyword bind được vào VO.
- GAP (ghi rõ, KHÔNG bịa logic lọc): server sau `LoadData` hiện là stub
  `PatientListPrintDmnServiceStub.PatientList` trả danh sách rỗng bất kể
  input — tức server CHƯA TỪNG có xử lý keyword. Việc lọc thật cần service
  thật (thuộc quyết định phạm vi stub Sett, xem mục ngoại lệ) nên không
  triển khai ở đây.
- Bằng chứng STG: trang `PatientInHosList/Index` → 200;
  `POST LoadData` với `keyword=Nguyen` → `[]` (không lỗi; lọc thật vẫn nợ
  theo gap trên). Build Sett 0 error chứng minh field VO hợp lệ.

## 8. SETT-03 — plugin gọi /SystemManage/BaseData/SelectSfxmYp → 404

- Đối chiếu cây gốc TRƯỚC khi quyết định (theo đúng task):
  - Cây Sett gốc `~/workspace/lmsoft_openhis/Newtouch.HIS.Sett/Newtouch.Web/Areas/SystemManage/Controllers/`:
    KHÔNG có `BaseDataController`; `SysChargeItemController.cs` gốc cũng
    đã mang ghi chú `[Obsolete("replaced by/SystemManage/BaseData/SelectSfxmYp")]`
    và `page.common.js` gốc cũng đã gọi URL đó.
  - Cây CIS gốc và OR gốc: `BaseDataController` của chúng KHÔNG có action
    `SelectSfxmYp` (CIS gốc chỉ có `SelectDepartmentList`,
    `GetTCMSymptomsList`, `GetYzlb`, `SelectConsultList`).
  - Kết luận: endpoint này CHƯA TỪNG tồn tại trong cây Sett (ghi chú
    "replaced by" trỏ tới một controller của assembly Framework dùng chung
    thời .NET Framework cũ, không có trong cây nguồn này).
- Quyết định theo task: GHI LÀ GAP, KHÔNG tự thiết kế endpoint mới.
  Không sửa code cho mục này.
- Bằng chứng STG: `GET /SystemManage/BaseData/SelectSfxmYp?keyword=test&mzzybz=1&dllb=1,2`
  với session hợp lệ vẫn → 404 (như sweep-B2). Selector khoản thu phí trên
  `OutpatCharge/Index2018` và `BookkeepInHos/Accounting` vì vậy vẫn chưa
  lấy được dữ liệu tìm kiếm — cần quyết định vòng sau (port endpoint từ
  proc `SETT_usp_SelectSfxmYp` có sẵn trong cây SQL gốc, hoặc đổi URL
  selector sang endpoint đã có). Ghi chú thêm: proc
  `usp_SelectSfxmYp` hiện được OR dùng nội bộ
  (`OR.Infrastructure/SysChargeItemDmnService.cs`), không phải endpoint
  web Sett.

## 9. SSO-07 / MRQC-05 mobile (390×844 emulation, KHÔNG app native)

Ba tầng chồng lấn/tràn đã sửa và đo lại; mọi số đo dưới đây đều từ
Playwright + Firefox trên staging (không phải trình duyệt task).

### 9a. WorkSpace carousel (SSO-07)

- Cơ chế (đo được): badge là Bootstrap `.label` (`white-space:nowrap`)
  trong 3 cột `col-xs-4` + `.app-item{width:20%; margin-left:2rem}`; trong
  iframe portal mobile (~232px) badge bị ép thành dải 47px, chữ cắt cụt.
- Sửa (`Views/Home/WorkSpace.cshtml`, chỉ media ≤768px, desktop không đổi):
  `.app-blocktheme-blue .app-item { float:none; width:100%; margin-left:0;
  margin-bottom:8px; white-space:normal; text-align:center; }` — badge xếp
  dọc, đủ rộng, chữ wrap.
- Sau fix: 3 badge xếp dọc tại `y=14/50/107`, rộng 230px, đọc đủ chữ
  ("hệ thống thanh toán", "hệ thống hợp tác y tế - điều dưỡng" wrap 2 dòng,
  "hệ thống nhà thuốc - kho thuốc"), `overlaps = []`.

### 9b. Thanh tab portal đè lên iframe (SSO-07, lỗi nặng nhất, phát hiện khi đo lại)

- Triệu chứng ban đầu sau fix 9a: ảnh vẫn thấy nút "thao tác tab" che mất
  "hệ th" của badge đầu ("ống thanh toán").
- Căn nguyên (đo rect + đọc CSS served từ 41142): layout tab bar của theme
  (`position:relative` cho bar, `position:absolute` cho các nút roll,
  `right:40px` cho btn-group) CHỈ nằm trong khối
  `@media (min-width: 992px)` của `framework-theme.css`; dưới 992px bar
  giữ chiều cao theme 41px nhưng các nút rớt xuống dòng 2 tràn qua iframe
  (đo được: bar `y=81..121`, nút `y=128..174`, iframe từ `y=121`).
- Sửa (`Views/Shared/_IndexContentWrapper.cshtml`, chỉ media ≤991px,
  khôi phục đúng geometry theme, KHÔNG đặt `overflow:hidden` cho bar để
  menu dropdown của tab vẫn mở phủ lên iframe như desktop):
  `.content-tabs{position:relative; height:42px}`,
  `.roll-nav{position:absolute; top:0; z-index:2}`,
  `.roll-left{left:0}`, `.tabRight{right:120px}`,
  `.btn-group{right:40px; width:80px}`, `.fullscreen{right:0}`,
  `button{height:39px; line-height:39px}`,
  `nav.page-tabs{margin-left:40px; height:40px; overflow:hidden}`.
- Sau fix (đo lại): bar `position:relative`, các nút tại `y=81` (đỉnh bar),
  nút tab bottom `=120`, iframe top `=131` (hở 11px, overlap âm) — hết
  chồng lấn. Ảnh `shots-sweep/fix1-sso-portal-mobile3.png` (crop phóng to
  `fix1-portal-top3.png` đã xem): tab bar một hàng
  ("trang chủ", "»", "thao tác tab", fullscreen), 3 badge đọc đủ, không cắt.

### 9c. Footer + tràn 3px của portal (SSO-07)

- Sửa (`Views/Shared/_IndexNavCol.cshtml`): media ≤991px cho
  `#nav-col.login-foot { width:100% !important; height:auto; }` (bỏ dải
  dọc do inline `width:14%` gây ra); media ≤767px cho
  `#page-wrapper > .row { margin-left:-5px; margin-right:-5px; }` (row
  margin -8px đối đầu container padding 5px gây tràn 393/390).
- Sau fix: portal `scrollWidth == clientWidth == 390`, không phần tử nào
  vượt viewport; footer thành dòng full-width 390×26 đọc được.

### 9d. MRQC MedicalCenter tràn ngang (MRQC-05)

- Cơ chế: handler `$(window).resize` trong view đặt
  `$(".dv-body").width($(window).width())`; `.dv-body` nằm trong nội dung
  portal có padding/margin nên sau bất kỳ lần resize nào trang tràn
  ~15px (sweep-A đo 405/390; lần đo đầu của worker này chưa trigger resize
  nên đo được 390 — đã tái hiện bằng cách dispatch sự kiện resize).
- Sửa (`MedicalCenter/Index.cshtml`, chỉ media ≤768px, desktop không đổi):
  `.dv-body { width: auto !important; }` (`!important` thắng inline width
  do handler đặt).
- Sau fix: trước và sau resize đều `scrollWidth == clientWidth == 390`,
  tràn trang = 0; form lọc xếp dọc đọc được, lưới cuộn ngang NỘI BỘ
  (bảng jqGrid rộng ~2000px do nhiều cột — đó là thiết kế cuộn trong lưới,
  không phải tràn trang). Ảnh `shots-sweep/fix1-mrqc-medicalcenter-mobile2.png`.

### 9e. Còn tồn (ghi rõ, không giấu)

- Sidebar portal mobile vẫn là dải icon 64px của chế độ `nav-small`
  (nhãn menu ẩn, submenu chỉ mở theo hover desktop) — nút thu gọn header
  (`#make-small-nav`) bị `hidden-sm hidden-xs` nên mobile không có cách mở
  rộng menu. Sửa triệt để cần cơ chế toggle menu cho mobile (JS + layout),
  vượt mức "tối thiểu" của task nên không làm ở đây; nội dung chính
  (WorkSpace, bảng) đã đọc và dùng được.
- Bảng thống kê WorkSpace vẫn cần vuốt ngang trong bảng (đã được sweep-A
  chấp nhận là hành vi cuộn trong bảng, không phải tràn trang).
- Lưới MedicalCenter mở lên vẫn trống thân (A-06 chưa kết luận của sweep-A,
  ngoài phạm vi FIX-1 — không sửa, không báo là xong).

## Hồi quy luồng kề cận (đã chạy, đều đạt)

- SSO: login `000000` thành công nhiều lần (curl + UI); `/Home/Index`,
  `/SysManage/Notice/Index`, `/SysManage/SysModule/Index` → 200;
  `GetTreeGridJson` với đúng `queryParams` UI → cây menu JSON.
- Sett: login `000000` thành công; `/Home/Index`,
  `/OutpatientManage/Settlement/SettQuery`,
  `/PatientManage/PatientInHosList/Index`,
  `/DeanInquiryManage/BusinessRanking/Index` → 200; session cookie vẫn Sett.
- MRQC: login `000000` thành công (UI); `/Home/Index` → 200;
  MedicalCenter tải bình thường (trừ thân lưới trống như A-06 đã ghi).
- Không có lần login nào báo khóa/sai trong toàn phiên.

## Ngoại lệ tuyệt đối KHÔNG tự quyết (chỉ ghi nhận nếu gặp)

- Không gặp trường hợp nào cần quyết trong phạm vi FIX-1 ngoài các gap đã
  ghi (SETT-03, PatientInHosList lọc server, sidebar mobile). Các mục sau
  không đụng tới theo task: NoticeJob đổi trạng thái không JWT; stub ghi
  Sett 184 vị trí `NotSupportedException`; SETT-06 máy chủ báo cáo; chuẩn
  hóa Debug→Release launcher; xoay secret; bắt đổi mật khẩu lần đầu.

## Thông báo vận hành của điều phối viên (16:33 ICT) — đã áp dụng

Điều phối viên gửi thông báo vận hành trong lúc worker đang tổng kết; cả 3
điểm đều đã được xử lý và ghi nhận như sau (không thay đổi phạm vi bug):

1. **Sett scratch PID 6027 bị kill — đã khởi động lại instance mới và kiểm
   chứng lại đầy đủ.** Tiến trình Sett scratch đầu tiên của worker (nghe
   cổng 15222, thư mục `fix1-scratch/sett`) bị kill vì guard pgrep theo tên
   tiến trình của `recovery.sh` tưởng Sett prod vẫn chạy, khiến Sett prod
   5221 không được khởi động lại sau restart 16:16 ICT. Worker phát hiện
   Sett scratch mất kết nối (cổng 15222 CLOSED, log dừng ở
   "Application is shutting down...") khi đang chạy kiểm chứng hồi quy,
   đã khởi động lại instance scratch mới từ cùng thư mục scratch
   (staging env, DB `*_STG`, Redis 6380) — PID mới 15111, khởi động
   09:32:24 UTC, cổng 15222 — và kiểm chứng lại đầy đủ trên instance mới:
   `/Home/Index` → 200 (session cũ trong Redis staging còn hiệu lực),
   `/OutpatientManage/Settlement/SettQuery` → 200,
   `GetSettlementGridJson` → `{"rows":[],"total":0,"page":1,"records":0}`,
   trang login vẫn title Sett và 0 lần `screen.width / 1920`.
   Sett prod 5221 đã lên lại 200 (do watchdog/coordinator khôi phục);
   worker KHÔNG chạm vào Sett prod ngoài 1 GET chỉ-đọc trang login để xác
   nhận nó đang lên — không login, không ghi, không restart tiến trình prod.
2. **Bài học redeploy (PDS prod 5237 lỗi 500 BadImageFormatException do DLL
   bị build ghi đè dưới tiến trình đang chạy từ 09:18) — đã áp dụng cho
   staging của worker từ trước khi nhận thông báo.** Quy trình của worker
   vốn đã là dừng-tiến-trình-trước-rồi-mới-publish: lần publish lại SSO +
   MRQC sau fix mobile bị lỗi MSB3027 (file PDB/DLL đang bị tiến trình
   staging giữ) chính là vì chưa dừng app; worker đã kill đúng PID staging
   rồi publish lại thành công và restart app ngay sau đó. Không lần nào
   worker copy DLL đè lên tiến trình đang chạy. Sự cố PDS là phía prod
   (đã được khởi động lại sạch, login 200) — worker không đụng tới PDS.
3. **Guard `recovery.sh` cho Sett và PDS đã sửa thành kiểm tra theo thư mục
   làm việc của tiến trình prod** (thay cho pgrep theo tên) — ghi nhận:
   từ nay các lệnh `dotnet publish`/`build` của worker không còn đánh lừa
   recovery nữa. Kỷ luật staging của worker giữ nguyên không đổi: app
   staging chạy từ thư mục scratch riêng, kill theo PID chính xác (đối
   chiếu `cwd` qua `/proc/<pid>/cwd`), không kill theo pattern tên, không
   dùng cổng prod, không dùng launcher prod.

## Commit

- Sẽ commit đúng 11 pathspec (10 file source + báo cáo này), KHÔNG
  `git add -A`, KHÔNG DLL/PDB, KHÔNG đụng `his-dll-common`, KHÔNG secret,
  KHÔNG `roles-shots-progress.md` (của điều phối viên, đã dirty từ trước),
  KHÔNG các sửa đổi của worker khác đang cùng dirty trong tree (FIX-2…).
- File sửa:
  1. `Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/MsgQueue/MsgQueueDmnService.cs` (A-01)
  2. `Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysModuleDmnService.cs` (A-02)
  3. `Newtouch.Sett.Web/Views/Login/Index.cshtml` (B1, B2)
  4. `Newtouch.Sett.Web/Areas/OutpatientManage/Controllers/SettlementController.cs` (B3)
  5. `Newtouch.Sett.Web/Areas/DeanInquiryManage/Views/BusinessRanking/Index.cshtml` (B4)
  6. `Newtouch.Sett.Infrastructure/Domain/Types_PatientManage.cs` (PatientInHosList VO)
  7. `Newtouch.HIS.SSO/HIS.SSO/Views/Home/WorkSpace.cshtml` (SSO-07 carousel)
  8. `Newtouch.HIS.SSO/HIS.SSO/Views/Shared/_IndexNavCol.cshtml` (SSO-07 footer + row)
  9. `Newtouch.HIS.SSO/HIS.SSO/Views/Shared/_IndexContentWrapper.cshtml` (SSO-07 tab bar)
  10. `Newtouch.MRQC.Web/Areas/QualityControlManage/Views/MedicalCenter/Index.cshtml` (MRQC-05)
  11. `scripts/sweep-final/fix-1-REPORT.md` (báo cáo này)
- Lưu ý redeploy (cho worker redeploy, không phải việc FIX-1): code fix
  A-01/A-02 nằm trong `NewtouchHIS.Base.DomainService.dll` — SSO portal kéo
  DLL này qua `HIS.SSO.DomainService` (ProjectReference), nên chỉ cần
  redeploy SSO portal từ HEAD mới là đủ cho 2 fix đó (không cần copy DLL
  rời); các fix Sett/MRQC/SSO-view đi kèm đúng app của chúng. Framework.Web
  DLL không bị sửa trong đợt này.
