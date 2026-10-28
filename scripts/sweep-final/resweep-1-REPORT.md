# RE-SWEEP-1 — Báo cáo kiểm chứng lại (SSO + MRQC + Center + Sett)

- Worker: RE-SWEEP-1. Ngày: 2026-10-28 (ICT).
- Base: V2.2 HEAD `e9d625e3` tại `~/workspace/openhis-core`
  (FIX-1 `3f8d9723` + FIX-2B `e9d625e3`, đã DEPLOY LÊN PROD xong 16/16 app theo
  `scripts/sweep-final/redeploy2-REPORT.md` — đã đọc trước khi làm việc).
- Phạm vi VERIFY-ONLY: KHÔNG sửa code, KHÔNG commit, KHÔNG sửa
  `roles-shots-progress.md`, KHÔNG deploy/khởi động lại app prod nào.
  Mọi app prod kiểm chứng xong vẫn chạy đúng PID của redeploy-2 (xem §7).
- Tài khoản kiểm chứng duy nhất: `000000` / `000000` (web gõ `000000` vào ô mật
  khẩu; POST CheckLogin dùng password = md5(000000) tính bằng `md5sum`, không gõ
  tay hash). Không lần nào login báo khóa/sai; không đoán mật khẩu.
  Số lần thử login sai mật khẩu trong phiên này: 0.
- Nguyên tắc DB: mọi ghi kiểm chứng CHỈ trên DB `*_STG`, đã dọn sạch và đối chiếu
  về baseline (xem từng mục). Trên PROD chỉ đọc và kiểm tra validation-gate;
  KHÔNG ghi DB demo dưới mọi hình thức (không fixture, không mở khóa, không sửa
  user). Login kiểm chứng trên prod chỉ tạo session Redis như mọi lần login
  bình thường.

## Kết quả tổng quan

| Mục | Kết luận |
|---|---|
| 1a. A-01 SSO Notice msgtype=1 (prod) | ĐẠT (lưới trả `{"rows":[],"total":0}`, không còn lỗi SqlSugar; prod demo có 0 dòng notice nên lưới rỗng là đúng — xem bằng chứng dương tính STG ở 1b) |
| 1b. A-01 dương tính STG (2 nhánh) | ĐẠT — msgtype=1 trả cả 2 dòng fixture (nhánh `=` và nhánh `Contains`) |
| 1c. Sett B3 GetSettlementGridJson (prod) | ĐẠT — đúng dạng jqGrid, không lỗi null |
| 1d. Sett B1 title + appId login (prod) | ĐẠT — title Sett, `appId = 'Sett'`, 0 lần `MRQC` |
| 1e. Sett B4 title BusinessRanking (prod) | ĐẠT — `Xếp hạng nghiệp vụ`, 0 lần `Theo dõi thuốc` |
| 1f. PatientInHosList keyword (prod) | ĐẠT Ở MỨC GHI NHẬN GAP — gửi keyword → `[]`, không lỗi; server vẫn là stub trả rỗng, KHÔNG phải lọc thật |
| 1g. SETT-03 vẫn 404 (prod) | XÁC NHẬN LÀ GAP GỐC, không phải regression |
| 2. A-02 SyncForm (STG, build mới từ HEAD) | ĐẠT — cây 3 tầng đúng Guid mới, đã dọn sạch, nguồn MRQC nguyên vẹn |
| 3a. Mobile SSO-07 portal + WorkSpace (prod) | ĐẠT (theo FIX-1) — tab một hàng không đè iframe, carousel xếp dọc đọc đủ, footer full-width, không tràn trang; còn tồn sidebar dải icon + bảng cuộn ngang trong bảng (ghi rõ ở §3) |
| 3b. Mobile MRQC-05 MedicalCenter (prod) | ĐẠT (theo FIX-1) — hết tràn 15px (`scrollWidth == clientWidth == 390`); lưới vẫn trống thân khi mở (giải thích ở §4, không phải lỗi tải) |
| 3c. Mobile SETT-17 Settlement (prod) | ĐẠT (theo FIX-1) — form xếp dọc dùng được, không bị bóp; bảng tràn ngang trong bảng |
| 3d. (Bổ sung ngoài task) Sett login mobile B2 (prod) | ĐẠT — `transform: none`, card căn giữa 360px, không còn `scale(0.711)` |
| 4. MRQC-03 (nợ sweep-A) | KẾT LUẬN DỨT KHOÁT: TÌM KIẾM HOẠT ĐỘNG — nút đúng là `#btn_search`, bấm với khoảng ngày phủ dòng demo trả đúng 1 dòng `A Binh` (xem §4) |
| 5. Sett sort + lọc trên lưới CÓ DỮ LIỆU | CHƯA KIỂM CHỨNG ĐƯỢC — lưới prod đều rỗng/null và service Sett Web là stub trả null/rỗng bất kể DB (xem §5, không bịa) |
| 6a. Hồi quy login prod SSO/MRQC/Sett | ĐẠT — 3/3 success, trang chính 200 |
| 6b. Center prod validation-gate | ĐẠT — GetAppToken/UserLogin `{}` → 40004 đúng dạng |
| 6c. NoticeJob Data gate + quyết định mở | ĐẠT Ở MỨC HIỆN TRẠNG — cổng Data 40004 đúng (prod + STG); đổi trạng thái không JWT vẫn thành công trên STG (10000, DB 1→0) — quyết định vẫn MỞ, worker KHÔNG tự gắn auth |

## 1. Kiểm chứng lại từng fix FIX-1 trên PROD (chỉ đọc)

### 1a. A-01 — Notice inbox msgtype=1 (prod 5208, session thật)

- Trang `/SysManage/Notice/Index` → 200.
- `POST /SysManage/Notice/GetNoticeList` (`limit=5&offset=0`):
  - `queryParams[msgtype]=1` → `{"rows":[],"total":0}` — KHÔNG còn lỗi
    `SqlFunc.IIF ... argument do not support` của sweep-A.
  - `msgtype=2` → `{"rows":[],"total":0}`; `msgtype=999` → `{"rows":[],"total":0}`.
- Đối chiếu DB demo (chỉ đọc): `NewtouchHIS_Base.dbo.Msg_NoticeQueue` = 0 dòng.
  Lưới rỗng trên prod vì vậy là ĐÚNG dữ liệu, không phải bằng chứng âm tính
  giả; bằng chứng dương tính nằm ở §1b (STG có fixture).

### 1b. A-01 — Bằng chứng dương tính trên STG (build mới từ HEAD, xem §2)

- Fixture chèn vào `NewtouchHIS_Base_STG.dbo.Msg_NoticeQueue` (nhóm có sẵn
  `0851787a-…`, org demo `6d5752a7-…`, AppId nhóm `MRQC`, `SendFrom='000000'`):
  - `RESW1-A01-USER` (`RecipientType=1`, `Recipient='000000'` — nhánh bằng);
  - `RESW1-A01-GROUP` (`RecipientType=2`, `Recipient='000000'` — nhánh Contains).
  - Lưu ý khi tái hiện: các cột NOT NULL bắt buộc là `NoticeId`, `GroupYwlx`
    (= 0, khớp nhóm), `NoticeRange` (= 1, khớp nhóm) ngoài các cột đã nêu trong
    FIX-1; lần chèn đầu thiếu 2 cột này nên INSERT bị từ chối, không phải lỗi app.
- `POST /SysManage/Notice/GetNoticeList` trên SSO staging 15209, cả 3 msgtype
  1/2/999 đều trả `total: 2` với cả 2 dòng fixture, `SendFromName` /
  `RecipientName` = quản trị viên, không có trường lỗi nào.
- Dọn sạch: `DELETE ... WHERE Id LIKE 'RESW1-A01%'` → 2 dòng bị xóa,
  tổng `Msg_NoticeQueue` STG về 0 (baseline).
- Quan sát nhỏ (không xếp bug, không thuộc phạm vi fix): trong response STG lần
  này `AppName` là `null` (FIX-1 ghi `AppName` là MRQC). Hàng vẫn trả đủ, lỗi
  SqlSugar đã hết; khác biệt này nằm ở trường hiển thị tên app, không ảnh hưởng
  kết luận A-01. Ghi lại để vòng sau đối chiếu nếu cần, không tự kết luận
  regression khi chưa có bằng chứng đối chứng cùng payload với FIX-1.

### 1c. Sett B3 — GetSettlementGridJson (prod 5221, session thật)

- Trang `/OutpatientManage/Settlement/SettQuery` → 200.
- `POST GetSettlementGridJson` không tham số và có
  `keyword=test&page=1&rows=10` đều →
  `{"rows":[],"total":0,"page":1,"records":0}` — đúng dạng jqGrid, không còn
  `{"state":"error","message":"Value cannot be null. (Parameter 'source')"}`.
- Lưới rỗng là đúng vì service stub trả rỗng (xem §5); điểm kiểm chứng ở đây là
  controller đã guard null, không phải dữ liệu.

### 1d. Sett B1 — title + appId trang login (prod 5221, không cần auth)

- HTML trả `<title>Sett Hệ thống quản lý thu phí</title>` (dạng entity HTML),
  `var appId = 'Sett'`, số lần xuất hiện chuỗi `MRQC` trong trang = 0.
- Session cookie thực tế sau login vẫn là `Sett_…` (khớp config), như sweep-B2
  đã thu hẹp phạm vi ảnh hưởng.

### 1e. Sett B4 — title BusinessRanking (prod 5221, session thật)

- Trang `/DeanInquiryManage/BusinessRanking/Index` → 200, 16.425 byte.
- Title = `Xếp hạng nghiệp vụ`; số lần xuất hiện `Theo dõi thuốc` = 0.

### 1f. PatientInHosList keyword (prod 5221, session thật)

- Trang `/PatientManage/PatientInHosList/Index` → 200.
- `POST LoadData` với `keyword=Nguyen&rows=15&page=1` → `[]`, không lỗi, nhanh.
- Kết luận đúng mức: request/response chạy sạch và field `keyword` đã bind được
  vào VO (FIX-1), nhưng server vẫn là stub `PatientList` trả rỗng bất kể input
  (đối chiếu source: stub trả danh sách rỗng) nên đây KHÔNG phải bằng chứng lọc
  thật. Lọc thật vẫn nợ service thật, như FIX-1 đã ghi.

### 1g. SETT-03 — BaseData/SelectSfxmYp (prod 5221, session thật)

- `GET /SystemManage/BaseData/SelectSfxmYp?keyword=test&mzzybz=1&dllb=1,2` → 404.
- Đây là gap gốc đã xác nhận từ sweep-B/B2/FIX-1 (endpoint chưa từng tồn tại
  trong cây Sett; ghi chú "replaced by" trỏ tới controller Framework cũ không có
  trong cây nguồn này), KHÔNG phải regression của FIX-1/FIX-2 hay redeploy-2.
  Selector khoản thu phí trên `OutpatCharge/Index2018` và `BookkeepInHos/Accounting`
  vì vậy vẫn chưa lấy được dữ liệu tìm kiếm — cần quyết định vòng sau như FIX-1
  đã nêu (port endpoint từ proc `SETT_usp_SelectSfxmYp` hoặc đổi URL selector).

## 2. A-02 SyncForm — kiểm chứng lại trên STG build mới từ HEAD

### Môi trường staging (tuân thủ giới hạn 2 app cùng lúc, build tuần tự)

- Lần build đầu từ cây `openhis-core` bị lỗi `MSB3027` (file PDB của
  `NewtouchHIS.Base.DomainService` trong `bin/` prod đang bị tiến trình prod giữ
  — đúng cạm bẫy đã biết: build staging không được dùng chung `bin/` với prod).
  Xử lý: tạo git worktree riêng `~/workspace/resweep1-build` tại đúng HEAD
  `e9d625e3`, publish từ worktree sang thư mục scratch mới
  `~/workspace/resweep1-scratch/` — không chạm `bin/` prod, không sửa cây chính.
- Build tuần tự, mỗi lần 1 build, proxy env UNSET,
  `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, đều 0 error (chỉ warning có sẵn):
  1. `HIS.BaseAPI` (Release) → `resweep1-scratch/baseapi`;
  2. `HIS.SSO` (Release) → `resweep1-scratch/sso`
     (`NewtouchHIS.Base.DomainService.dll` md5 giống hệt ở cả 2 bản publish).
- Chạy staging (2 app cùng lúc, không vượt giới hạn): BaseAPI 17011 (PID 29284)
  + SSO 15209 (PID 29401), DB `*_STG`, Redis staging 6380, launcher scratch
  riêng (không dùng launcher prod). Login staging `000000` thành công ngay,
  `/Home/Index`, `/SysManage/Notice/Index`, `/SysManage/SysModule/Index` → 200.
- Cuối phiên đã kill đúng 2 PID này, cổng 17011/15209 đã xuống; worktree
  `resweep1-build` đã gỡ (theo cách làm của FIX-1). Thư mục publish scratch giữ
  lại để đối chiếu, không ảnh hưởng prod.

### Chu trình submit (đúng bước tái hiện sweep-A, payload không pre-encode)

- Nguồn còn nguyên: `Newtouch_MRQC_STG.Sys_Module` gốc
  `20d5a18a-…` ("Kiem soat chat luong Quan ly", `ParentId = NULL`) + con
  `24a940d0-…` ("Trung tam kiem soat chat luong", `ParentId` = Id gốc).
- `POST /SysManage/SysModule/SubmitMenuSync` với `parentMenu=RESW1-SYNC-PARENT`,
  `fromAppId=MRQC`, `list[0]` = gốc, `list[1]` = con (kèm `ParentId` = Id gốc) →
  `{"state":"success"}`.
- Đối chiếu `Newtouch_Union_STG.Sys_Module`:
  - `b62db30f-…` `RESW1-SYNC-PARENT`, `ParentId = NULL` (cha mới, đúng);
  - `14616129-…` bản sao gốc, `ParentId = b62db30f-…` (trước FIX-1 là NULL);
  - `0f832f73-…` bản sao con, `ParentId = 14616129-…` (Guid mới của bản sao gốc;
    trước FIX-1 là Id gốc `20d5a18a-…`, dangling ở DB Union).
  - Kiểm tra dangling: số dòng Union có `ParentId = '20d5a18a-…'` = 0.
  - Tên bản sao có khoảng trắng bình thường, không bị double-encode `%20`
    (payload gửi qua `--data-urlencode`, encode đúng 1 lần).
- Hồi quy: `GetTreeGridJson` với đúng `queryParams` của UI
  (`keyword` + `validLimit`) → trả cây menu JSON (dòng đầu `Quan ly he thong`).
- Dọn sạch (ghi trung thực cả sai sót đã sửa): lần xóa đầu gõ sai 1 ký tự trong
  Id dòng con (`…9612…` thay vì `…9613…`) nên chỉ xóa được 2/3 dòng; đã phát
  hiện bằng truy vấn theo tên và xóa bổ sung đúng Id
  `0f832f73-adaf-4bc5-9613-5612661dc02a`. Kiểm chứng cuối: Union không còn dòng
  nào tên chứa `RESW1` hay tên 2 menu nguồn (COUNT = 0), không còn dòng nào trỏ
  `ParentId` về Id nguồn; nguồn MRQC_STG còn nguyên 2/2.

## 3. Mobile (viewport emulation 390×844, KHÔNG phải app native)

OpenHIS không có app native; mọi "mobile" dưới đây là emulation viewport trên
Playwright + Firefox local. Không bao giờ tuyên bố test app native.

### 3a. SSO-07 — portal + WorkSpace (prod 5208, session thật)

- Ảnh: `shots-sweep/resw1-sso-portal-mobile.png`,
  `shots-sweep/resw1-sso-workspace-mobile.png`.
- Đo được trên portal: `scrollWidth == clientWidth == 390` (hết tràn 3px của
  sweep-A); tab bar `y=81..121`, các nút tab tại `y=81` (đỉnh bar), iframe bắt
  đầu `y=131` (hở 10px dưới bar — không đè iframe, fix FIX-1 còn hiệu lực);
  footer rộng 390px thành dòng ngang (không còn dải dọc).
- Ảnh portal cho thấy: tab một hàng ("trang chủ", "»", "thao tác tab",
  fullscreen); 3 badge carousel xếp dọc, đọc đủ chữ ("hệ thống thanh toán",
  "hệ thống hợp tác y tế - điều dưỡng", "hệ thống nhà thuốc - kho thuốc"),
  không chồng lấn chữ; link truy cập nhanh + date picker tốt.
- Ảnh WorkSpace trực tiếp cho thấy cùng 3 badge xếp dọc đọc đủ; bảng thống kê
  rộng ~680px nên bị cắt phải trong viewport — đó là cuộn ngang NỘI BỘ trong
  bảng (đã được sweep-A chấp nhận), không phải tràn trang (trang vẫn 390/390).
- Còn tồn đúng như FIX-1 đã ghi (không giấu):
  - Sidebar mobile vẫn là dải icon 64px của chế độ `nav-small` (nhìn thấy dải
    tối + nút `<<` ở mép trái ảnh portal); nút thu gọn header bị
    `hidden-sm hidden-xs` nên mobile không có cách mở rộng menu. Cần quyết định
    JS/layout riêng, ngoài phạm vi re-sweep.
  - Bảng WorkSpace vẫn cần vuốt ngang trong bảng (xem trên).
- Minh bạch phương pháp: lần chạy mobile SSO đầu tiên bị lỗi parser cookie của
  worker (bỏ qua các dòng `#HttpOnly_` trong file jar của curl nên session SSO
  không được nạp, ảnh thu được thực chất là trang login). Đã sửa parser, chạy
  lại và chụp đúng portal/WorkSpace; ảnh cuối trong thư mục là ảnh thật sau
  login (đã xem trực tiếp để xác nhận). Ảnh login SSO mobile lần đầu đã bị ghi
  đè, không dùng làm bằng chứng portal.

### 3b. MRQC-05 — MedicalCenter (prod 5220, session thật)

- Ảnh: `shots-sweep/resw1-mrqc-medicalcenter-mobile.png`.
- Đo được: `scrollWidth == clientWidth == 390`, `.dv-body` rộng 390px, không
  phần tử lá nào vượt viewport — hết tràn 15px của sweep-A (405/390), fix FIX-1
  còn hiệu lực. Form lọc xếp dọc, đọc được.
- Lưới vẫn trống thân khi mở trang (chỉ có header `Khoa | Số giường | Họ tên |
  Giới tính | Tuổi | …`). Đây KHÔNG còn là "chưa kết luận" nữa: xem §4 — lưới
  trống khi mở là do khoảng ngày mặc định của form không phủ dòng demo duy
  nhất, không phải lỗi tải lưới.

### 3c. SETT-17 — Settlement (prod 5221, session thật)

- Ảnh: `shots-sweep/resw1-sett-settlement-mobile.png`.
- Đo được: `scrollWidth == clientWidth == 390`, `body.style.transform = none`.
  Ảnh cho thấy form xếp dọc, chữ cỡ bình thường, dùng được (ô tìm
  `Vui lòng nhập số thẻ/Mã nhập viện`, các nhóm `Thông tin bệnh nhân nhập viện`,
  `Thông tin quyết toán`, `Lịch sử nộp tiền của bệnh nhân`,
  `Thông tin chứng từ`); các bảng rộng tràn ngang trong bảng, cần cuộn ngang
  (thiết kế desktop cố định, không phải bóp nhỏ). Đúng trạng thái FIX-1/B2 đã
  mô tả cho trang sau login.

### 3d. (Bổ sung) Sett login mobile — fix B2 trên prod

- Ảnh: `shots-sweep/resw1-sett-login-mobile.png` (trang login, không cần auth).
- Đo được: `scrollWidth == clientWidth == 390`, `body.style.transform = none`,
  `body.style.width = ""`, card `.login-b` tại `x=15, w=360, right=375` (căn
  giữa, lề 15px 2 bên), title `Sett Hệ thống quản lý thu phí`.
- Đối chiếu FIX-1: trước fix là `scale(0.711…)`, `width = 548.17px` (và trên điện
  thoại thật còn ~20%). Fix B2 (bỏ `resize()`) đã live trên prod sau redeploy-2.

## 4. MRQC-03 — KẾT LUẬN DỨT KHOÁT: tìm kiếm hoạt động (nợ sweep-A đã đóng)

### Xác định đúng nút bằng DOM (trước khi bấm)

Trang `/QualityControlManage/MedicalCenter/Index` (prod, HTML server) có đúng
3 nút mang icon `fa-search`, phân biệt rõ bằng id + handler:

| Nút | Vị trí trong DOM | Handler (đọc từ script trang) | Lưới đích |
|---|---|---|---|
| `#btn_search` | Cạnh ô `#txt_srz` (placeholder "Họ tên / Số nhập viện") | `$('#gridListPat').jqGrid('setGridParam', {url: "/QualityControlManage/MedicalCenter/GetPatList", postData: getSearchPostData()}).trigger('reloadGrid')` với `brbz/ksrq/jsrq/srz` từ form lọc chính | `#gridListPat` (danh sách bệnh nhân — lưới chính) |
| `#btn_Jysearch` | Cạnh ô ngày xét nghiệm (`txt_Jykssj`/`txt_Jyjssj`) | Reload `#gridJyList` qua `GetInspectionExaminationRecore` (`reportType: "jy"`) | `#gridJyList` (báo cáo xét nghiệm) |
| `#btn_Jcsearch` | Cạnh ô ngày kiểm tra (`txt_Jckssj`/`txt_Jcjssj`) | Reload `#gridJcList` qua `GetInspectionExaminationRecore` (`reportType: "jc"`) | `#gridJcList` (báo cáo kiểm tra) |

Sweep-A đã bấm "nút `fa-search` đầu tiên" mà chưa phân biệt — 2 nút sau không
phải nút tìm kiếm bệnh nhân, và worker A khi đó đếm nhầm bảng khác được 16 dòng.
Lần này chỉ bấm `#btn_search` và chỉ đếm `#gridListPat`.

### Kết quả bấm nút thật trên trình duyệt (prod, session thật, desktop 1366×900)

- Form mặc định khi mở trang: radio `brlx = 1` (bệnh nhân đang nằm viện) được
  chọn, `txt_kssj = 2026-08-01 00:00:00`, `txt_jssj = 2026-10-28 10:16:03`,
  `txt_srz` rỗng.
- Lưới khi mở trang: `#gridListPat` = 0 dòng.
- Bấm `#btn_search` với giá trị mặc định: vẫn 0 dòng.
- Đổi `txt_kssj` → `2020-01-01 00:00:00`, `txt_jssj` → `2026-10-28 23:59:59`
  (giữ `brbz=1`, `srz` rỗng), bấm lại `#btn_search`: `#gridListPat` = **1 dòng**:
  `1 | Khoa Noi | 01 | A Binh | 25 tuoi | 2024-10-16 16:28:56 |
  142726200001010001 | 01000 | Tu chi tra | …` (ảnh
  `shots-sweep/resw1-mrqc-medicalcenter-search.png`).
- Đối chứng: `#gridJyList` và `#gridJcList` vẫn 0 dòng sau khi bấm nút chính
  (không bị bấm nhầm); endpoint `GetPatList` qua curl cho cùng kết quả:
  khoảng rộng `brbz=1` → 1 dòng demo (`Zyh=01000`, `Xm=A Binh`); khoảng mặc
  định `2026-08-01→2026-10-28` → `[]`; `brbz=2` khoảng rộng → `[]`;
  `brbz=3` khoảng rộng → 1 dòng demo.
- **Kết luận:** chức năng tìm kiếm MedicalCenter HOẠT ĐỘNG ĐÚNG. Lưới trống khi
  mở trang và sau lần bấm đầu được giải thích trọn vẹn bằng dữ liệu: dòng demo
  duy nhất nhập viện ngày 2024-10-16, nằm ngoài khoảng ngày mặc định của form
  (từ 2026-08-01). Đây là hành vi lọc đúng, không phải lỗi tải lưới, không phải
  lỗi nút bấm. A-06 của sweep-A coi như ĐÓNG. (Ghi chú: nếu bệnh viện muốn lưới
  mở lên đã có dòng thì cần dữ liệu bệnh nhân trong khoảng mặc định hoặc đổi
  mặc định khoảng ngày — đó là quyết định dữ liệu/giao diện, không phải bug
  tìm kiếm.)

## 5. Sett sort + lọc trên lưới CÓ DỮ LIỆU — CHƯA KIỂM CHỨNG ĐƯỢC (lý do chính xác)

- Đã thử trên prod demo (chỉ đọc, session thật):
  - SettQuery `GetSettlementGridJson` (có/không keyword) → `{"rows":[],"total":0,…}`;
  - PatientInHosList `LoadData` (có/không keyword) → `[]`;
  - BusinessRanking `GetMzysgzl` / `GetMzkssr` / `GetYsyw` / `GetZyywks` → `null`;
  - Area `GetTreeGridJson` → rỗng.
- Không có lưới Sett Web nào trên prod có dữ liệu để quan sát thứ tự sắp xếp hay
  hiệu quả lọc thay đổi.
- Vì sao STG cũng không cứu được (bằng chứng code, không phải suy đoán):
  các service đứng sau các lưới này là stub trả rỗng/null BẤT KỂ dữ liệu trong
  DB — `SettlementController` guard trên service stub trả null (B3);
  `PatientListPrintDmnServiceStub.PatientList` trả rỗng; `DeanInquiryDmnService`
  (`Stubs_DeanInquiryManage.cs`) trả `null` cho cả 4 endpoint BusinessRanking.
  Do đó chạy thêm Sett staging cũng chỉ cho lưới rỗng như prod đối với các lưới
  này, và worker không dựng Sett staging chỉ để chứng minh lại điều đã rõ từ
  code + response prod.
- Kết luận trung thực: sort + lọc trên lưới CÓ DỮ LIỆU của Sett Web **chưa kiểm
  chứng được** vì thiếu dữ liệu do kiến trúc stub, không phải vì thiếu session
  hay thiếu lượt thử. Cần service thật (hoặc ít nhất 1 lưới Sett Web có service
  thật + dữ liệu) mới kiểm chứng được; sweep-B2 đã ghi cùng kết luận với lý do
  lưới rỗng + header checkbox generic. Không báo phần này là xong.

## 6. Hồi quy nhanh

### 6a. Login prod 3 app + trang chính

| App (port) | CheckLogin `000000` | `/Home/Index` | Ghi chú |
|---|---|---|---|
| SSO (5208) | success `Đăng nhập thành công` | 200 (28.788 byte) | |
| MRQC Web (5220) | success | 200 | |
| Sett Web (5221) | success | 200 | |
| Login page (không cookie) | — | 200 cả 3 app (5208/5220/5221) | |

### 6b. Center prod — validation-gate đúng dạng

- `POST /api/Auth/GetAppToken` (8015) với `{}` → HTTP 200, envelope
  `BusData code 40004`, msg `Id ứng dụng (AppId) bắt buộc phải truyền`
  (fail-closed, không 500, không echo secret — fix CTR-01 còn hiệu lực).
- `POST /api/Login/UserLogin` (8016) với `{}` → HTTP 200, cùng envelope 40004
  như trên.
- MRQC API (5320, kiểm chứng thêm): `POST /api/Default/Query` với `{}` →
  `code 40004, sub_code TIMESTAMP_IS_REQUIRED` (fail-closed).
- Không cấp token thật trên prod trong đợt này (tránh tạo token thật; GetToken
  thật đã kiểm chứng ở phase 2).

### 6c. NoticeJob — Data gate đúng, quyết định đổi-trạng-thái-không-JWT vẫn MỞ

- Trên PROD (chỉ cổng validation, KHÔNG dùng QueueId thật để tránh ghi demo):
  - `NoticeProcSuccess` / `NoticeSendSuccess` với `AppId` nhưng thiếu `Data` →
    HTTP 200, `code 40004`, msg `Thông tin tổ chức,Tin nhắnId không được để trống`,
    `data: false`.
  - `NoticeImmediatelyWaitProc` thiếu `Data` → 40004
    `Thông tin tổ chức không được để trống`.
  - Không `AppId` → 40004 `Id ứng dụng (AppId) bắt buộc phải truyền`.
- Trên STG (build mới từ HEAD, WebAPI.Manage staging 18016, PID 30504, 1 app
  staging duy nhất tại thời điểm đó; fixture đã dọn sạch sau đó):
  - Cổng Data: thiếu `Data` → 40004 đúng như prod.
  - Đổi trạng thái KHÔNG JWT: `NoticeProcSuccess` với `AppId=SSO`,
    `OrganizeId` demo, `Data.QueueId = RESW1-NOTICE-001` (fixture `NoticeStu=1`)
    → HTTP 200, `code 10000`, `data: true`; đối chiếu DB STG:
    `NoticeStu 1 → 0`. Hành vi này còn nguyên như sweep-A (A-10).
  - Dọn sạch: fixture đã xóa, `Msg_NoticeQueue` STG về 0; Manage staging đã kill
    đúng PID 30504, cổng 18016 đã xuống.
- **Không tự gắn auth, không dựng network rule, không redesign** — đúng quyết
  định đã ghi (qa-phase2/api-auth.md §6 / sweep-A A-10): phương án (giới hạn
  network 8016 NoticeJob hoặc shared secret/header nội bộ) vẫn CHỜ QUYẾT ĐỊNH
  của điều phối viên. Việc đổi trạng thái không JWT vẫn thành công là HIỆN
  TRẠNG đã kiểm chứng lại, không phải lỗi mới và không phải chấp thuận ngầm.

## 7. Dọn dẹp và trạng thái cuối

- STG đã dọn: `Msg_NoticeQueue` STG = 0 (cả fixture A-01 lẫn fixture NoticeJob);
  `Sys_Module` Union_STG không còn dòng RESW1/named-menu nào, không còn dòng
  trỏ về Id nguồn MRQC; nguồn `Sys_Module` MRQC_STG còn nguyên 2/2.
- STG account `000000`: không ghi strike nào trong phiên này (mọi login staging
  đều thành công ngay với mật khẩu đúng; không có lần thử sai nào).
- Demo DB (prod): KHÔNG ghi gì (chỉ 1 lần COUNT chỉ-đọc bảng notice để đối
  chiếu; không fixture, không mở khóa, không sửa user).
- App staging của worker này đã khởi động và đã kill đúng PID:
  BaseAPI-stg 29284 (17011), SSO-stg 29401 (15209), Manage-stg 30504 (18016);
  cả 3 cổng đã xuống (kiểm chứng bằng `ss`). Không kill app nào của worker
  khác; hạ tầng dùng chung (SQL 1433, Redis staging 6380, static 41142) để
  nguyên đang chạy.
- Prod nguyên vẹn, không restart: SSO 5208 PID 23575, MRQC 5220 PID 23730,
  Sett 5221 PID 23898, AuthCenter 8015 PID 24088, WebAPI.Manage 8016 PID 24229
  — trùng đúng PID của redeploy-2; cuối phiên login page 3 app đều 200.
- Repo: KHÔNG sửa code, KHÔNG commit. HEAD vẫn `e9d625e3`; `git status` cuối
  chỉ còn `M roles-shots-progress.md` (có từ trước, của điều phối viên, không
  chạm) và các file `?? scripts/sweep-final/` (báo cáo các worker, trong đó có
  file này). Worktree `resweep1-build` đã gỡ; thư mục publish
  `~/workspace/resweep1-scratch/` (baseapi/sso/manage) giữ lại để đối chiếu.
- Ảnh (trong `~/workspace/openhis-core/shots-sweep/`, đã gitignore theo quy tắc
  `shots*/`, không lọt vào commit):
  - `resw1-sso-portal-mobile.png` — portal SSO mobile thật sau login (tab một
    hàng, carousel xếp dọc, footer ngang).
  - `resw1-sso-workspace-mobile.png` — WorkSpace mobile (3 badge xếp dọc, bảng
    cắt phải cần cuộn ngang trong bảng).
  - `resw1-mrqc-medicalcenter-mobile.png` — MedicalCenter mobile (form xếp dọc,
    không tràn, lưới trống khi mở).
  - `resw1-mrqc-medicalcenter-search.png` — MedicalCenter desktop sau khi bấm
    `#btn_search` với khoảng ngày rộng: 1 dòng `A Binh`.
  - `resw1-sett-settlement-mobile.png` — Settlement mobile (form xếp dọc dùng được).
  - `resw1-sett-login-mobile.png` — Sett login mobile (card căn giữa 360px,
    không scale).

## 8. Phần không kiểm chứng được / không test + lý do (đầy đủ, không giấu)

1. **Sett sort + lọc trên lưới có dữ liệu** — xem §5 (lưới rỗng + service stub).
2. **Ghi qua Sett Web UI** — ngoài phạm vi re-sweep này và đã có kết luận
   sweep-B2 (đường ghi Sett Web phần lớn là stub); không thử lại.
3. **Render sau login trên prod của các trang Sett/MRQC ngoài các trang đã nêu**
   — chỉ kiểm chứng các trang trong task + hồi quy login/trang chính; các trang
   còn lại đã có sweep-B2 (staging) và redeploy-2 (login + trang chính 200).
4. **Thiết bị thật / app native / xoay ngang / tablet** — chỉ có emulation
   390×844 trên Firefox local; OpenHIS không có app native.
5. **Sidebar mobile triệt để** — còn tồn theo FIX-1 (§3a); cần quyết định
   JS/layout riêng, không tự làm trong re-sweep verify-only.
6. **NoticeJob đổi trạng thái trên PROD** — cố tình không thử (sẽ ghi DB demo);
   hiện trạng đã kiểm chứng trên STG (§6c).
7. **SETT-03, PatientInHosList lọc thật, footer vendor tiếng Trung (A-03)** —
   các gap/quyết định mở đã ghi từ trước, không thuộc phạm vi sửa của re-sweep
   verify-only; trạng thái từng mục đã ghi đúng ở §1.
8. **AppName = null trong response Notice STG** (§1b) — quan sát nhỏ chưa đối
   chứng cùng payload với FIX-1; ghi lại, không kết luận regression.

## 9. Chuyển giao cho điều phối viên (không phải quyết định)

1. A-01, A-02, Sett B1/B3/B4, SSO-07, MRQC-05, SETT-17, Sett login B2: fix FIX-1
   đã live trên prod sau redeploy-2 và kiểm chứng đạt trong đợt này.
2. MRQC-03 (A-06): ĐÓNG — tìm kiếm hoạt động; lưới trống khi mở là do khoảng
   ngày mặc định không phủ dòng demo 2024.
3. Còn mở, cần quyết định (không tự quyết): NoticeJob đổi trạng thái không JWT
   (giới hạn network 8016 hoặc shared secret/header); SETT-03 (port endpoint
   hoặc đổi URL selector); PatientInHosList lọc thật (service thật); sidebar
   mobile (cơ chế toggle riêng); footer vendor (A-03).
4. Còn nợ kiểm chứng (không báo là xong): Sett sort + lọc trên lưới có dữ liệu
   — cần service thật + dữ liệu trước khi kiểm chứng được.
