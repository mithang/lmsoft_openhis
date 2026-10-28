# Audit BỔ SUNG — Trang nội bộ SSO (5208), MRQC (5220), Sett (5221)

> **Báo cáo re-audit, không phải báo cáo đầy đủ thay thế đợt trước.**
> Đọc kèm `audit-sso-mrqc-sett.md` (đợt trước: login page, footer vendor,
> asset jQuery 2.1.1 / Bootstrap shell 3.3.4 / center API-only đã xong,
> KHÔNG làm lại ở đây).
> Worker này KHÔNG sửa code, KHÔNG commit, KHÔNG chuyển sang chặng redesign.
> Phương pháp: Playwright + Firefox local, viewport desktop 1280×800,
> mobile 390×844, tablet 768×1024 (chỉ trang chủ nội bộ). Mobile = giả lập
> viewport, KHÔNG phải app native.
> Quy tắc tuân thủ: prod CHỈ ĐỌC (điều hướng + chụp ảnh, không submit form
> nào ngoài nút đăng nhập bằng tài khoản 000000 duy nhất); KHÔNG ghi database
> (kể cả STG); KHÔNG khởi động/dừng app; KHÔNG sửa file app.
> Ảnh mới (tên khác đợt trước, không ghi đè): `~/workspace/shots-redesign/<app>/`,
> hậu tố `-int-` (internal). Ảnh đợt trước bị redirect về login vẫn KHÔNG
> được dùng làm bằng chứng nội bộ.

## 0. Kết quả đăng nhập + DỪNG THEO QUY TẮC CỨNG (đọc trước)

### 0.1. Đăng nhập lần này: thành công ở đa số, thất bại rải rác theo context

| App / viewport | Kết quả đăng nhập | Ghi chú |
|---|---|---|
| SSO desktop | ✅ Thành công | Vào `/Home/Index`, chụp đủ 3 trang |
| SSO mobile | ✅ Thành công | Vào `/Home/Index`, chụp đủ 3 trang |
| SSO tablet | ❌ Thất bại 1 lần | Ở lại `/Login/Index`, thông báo rỗng (`tips=''`) — KHÔNG thử lại |
| MRQC desktop | ✅ Thành công | Chụp đủ 2 trang |
| MRQC mobile | ✅ Thành công | Chụp đủ 2 trang |
| Sett desktop | ✅ Thành công ×2 | 2 lần chạy riêng đều vào được |
| Sett mobile | ❌ Thất bại 1 lần | Thông báo `登录失败，请联系管理员` (tiếng Trung) — KHÔNG thử lại |
| Sett tablet | ⛔ Không thử | Dừng sau thất bại mobile theo quy tắc cứng |

Tổng cộng: **5 lần đăng nhập thành công, 2 lần thất bại, 0 lần thử lại.**
Ngay sau 2 lần thất bại, worker đã DỪNG mọi lần thử đăng nhập tiếp theo
(đúng quy tắc "nếu vẫn thất bại thì dừng lại và báo ngay, không thử lặp
nhiều lần"). Vì vậy **Sett mobile + Sett tablet và SSO tablet CHƯA có ảnh
nội bộ** — xem §6 (khoảng trống bằng chứng còn lại).

### 0.2. Diễn giải thất bại (không kết luận vội là "tài khoản bị khóa")

- Tài khoản **không thể đã bị khóa hoàn toàn**: Sett desktop đăng nhập
  thành công SAU lần SSO tablet thất bại, và trước lần Sett mobile thất bại.
  Mẫu này giống hệt đợt trước: thất bại rải rác theo context/viewport
  (đợt trước: SSO chỉ thành công ở tablet, MRQC thành công mobile+tablet
  nhưng thất bại desktop), không phải khóa đơn điệu.
- Nhưng 2 lần thất bại trong đợt này vẫn là **tín hiệu cần điều phối viên
  kiểm tra trước vòng đăng nhập tiếp theo**: cờ `Sys_UserLogOn.Locked` của
  000000 + log CheckLogin của SSO/Sett tại thời điểm chạy (xem §6, mục 1).
  Worker không tự kiểm tra DB khóa (ngoài phạm vi audit UI) và không đoán
  thêm nguyên nhân (DB timeout? session kick? JS login chưa bind?).
- Hệ quả trực tiếp: mọi kết luận về **mobile Sett và tablet SSO** trong
  báo cáo này đều là "chưa kiểm chứng", không phải "đạt" hay "không đạt".

### 0.3. Lưu ý phương pháp đo tràn (quan trọng cho chặng redesign)

- `documentElement.scrollWidth` = đúng viewport trên MỌI trang đã chụp
  (1280/1280, 390/390). **Nhưng đó không có nghĩa là "không tràn"**: các
  bảng bên trong rộng 1335–1999px nằm trong wrapper cuộn ngang, nên tài
  liệu không tràn mà bảng vẫn vượt viewport. Chặng redesign phải đánh giá
  theo §4.2 (cột định danh có mất khi cuộn không, tiêu đề có dính không,
  có dấu hiệu cuộn không) chứ không chỉ nhìn `scrollWidth`.
- Đếm ký tự CJK trong báo cáo này đo trên `innerText` — placeholder của
  ô input KHÔNG nằm trong `innerText`, nên chuỗi tiếng Trung trong
  placeholder (SyncForm, xem SSO-10) phải soi bằng ảnh mới thấy.
- Console errors: **0 trên mọi trang đã chụp** (kể cả trang lỗi tải
  Bookkeep — vì trang đó không tải xong, không tính là "0 lỗi" cho nó).

## 1. Ảnh mới đã chụp (chỉ liệt kê ảnh có giá trị)

### SSO (`shots-redesign/sso/`)
| Ảnh | Giá trị |
|---|---|
| `notice-int-desktop.png`, `notice-int-mobile.png` | ✅ Hộp thư Notice thật (đã đăng nhập) |
| `syncform-int-desktop.png`, `syncform-int-mobile.png` | ✅ SyncForm thật (đã đăng nhập) |
| `portal-home-int-desktop.png`, `portal-home-int-mobile.png` | ✅ Portal Home thật (đã đăng nhập; bổ sung cho portal tablet đợt trước) |
| (tablet) | ❌ Không có — đăng nhập tablet thất bại, không thử lại |

### MRQC (`shots-redesign/mrqc/`)
| Ảnh | Giá trị |
|---|---|
| `medicalcenter-int-desktop.png`, `medicalcenter-int-mobile.png` | ✅ MedicalCenter thật |
| `apply-int-desktop.png`, `apply-int-mobile.png` | ✅ Apply thật, có 3 dòng dữ liệu đọc-only |

### Sett (`shots-redesign/sett/`)
| Ảnh | Giá trị |
|---|---|
| `home-int-desktop.png` | ✅ Home nội bộ Sett thật (trang danh sách liên kết) |
| `daysett-inpatient-int-desktop.png` | ✅ Lưới quyết toán ngày nội trú thật (desktop) |
| `patientinhoslist-int-desktop.png` | ✅ PatientInHosList thật (desktop) |
| `bookkeepinhos-int-desktop.png` | ❌ **ĐÃ XÓA** — file này thực chất là ảnh Home (điều hướng Bookkeep không tới nơi, xem Sett-07); xóa để không tạo bằng chứng giả |
| (Bookkeep mọi viewport; Sett mobile + tablet mọi trang) | ❌ Không có — xem §6 |

## 2. Lỗi UI/UX theo checklist DESIGN.md §9 — SSO

**SSO-08 — Notice desktop: sạch, nhưng không có h1, không thấy chân lưới**
- Ảnh: `sso/notice-int-desktop.png`. Đo: scroll 1280/1280, không phần tử
  tràn, CJK trong body = 0, console = 0.
- Bộ lọc trạng thái có đủ nhãn + chấm màu (tất cả / chưa gửi / đã gửi /
  đã đọc / đang chờ xử lý), ô "từ khóa" + nút tìm, dropdown "hộp thư đến".
  Trạng thái rỗng hiển thị tiếng Việt "chưa có dữ liệu".
- Thiếu: không có `<h1>`, không có thẻ meta viewport (đo = null — trang
  này là fragment chạy trong iframe portal nên có thể có chủ ý, nhưng khi
  mở trực tiếp thì không đạt bước chẩn đoán §4.3 mục 1). Trang dùng
  bootstrap-table (`#table`), KHÔNG phải jqGrid, nên không có chân lưới
  kiểu "Đang xem x–y trên tổng z dòng" để đối chiếu §9.4 khi 0 dòng.
- Checklist: §9.1 (h1), §9.4 (chân lưới), §9.7. Mức: Low.

**SSO-09 — Notice mobile: bảng 879px trong viewport 390, không cột neo**
- Ảnh: `sso/notice-int-mobile.png`. Đo: bảng 879px, wrapper giữ
  scrollWidth tài liệu = 390 (cuộn ngang trong wrapper — lành mạnh theo
  §4.2 về mặt kỹ thuật), nhưng **không có cột neo**: cuộn sang phải sẽ
  mất cột định danh "nội dung tin nhắn". Dòng rỗng "chưa có dữ liệu" bị
  cắt ở cạnh phải trong ảnh viewport (vì chuỗi căn giữa trong bảng 879px).
- Bộ lọc: chip trạng thái xếp 2 hàng, ô tìm + dropdown xếp dọc — chấp
  nhận được, không tràn. Nút "hộp thư đến" chiếm toàn chiều rộng hàng.
- Checklist: §9.4 (chiến lược A thiếu cột neo), §9.7. Mức: Medium
  (người dùng mobile không thấy được cột định danh khi cuộn).

**SSO-10 — SyncForm: thanh bước 4000px gây tràn thật + placeholder tìm kiếm tiếng Trung**
- Ảnh: `sso/syncform-int-desktop.png`, `sso/syncform-int-mobile.png`.
  Đo: `UL.steps` rộng **4000px**; scroll desktop 1286/1280 (+6px),
  mobile 396/390 (+6px) — đây là tràn tài liệu thật, dù nhỏ (khác với
  bảng trong wrapper ở SSO-09). Nhãn "mục tiêu nhập vào:" bị cắt ký tự
  đầu ở cạnh trái trên cả 2 viewport (trong ảnh thấy vệt ký tự ở mép trái).
- Chuỗi tiếng Trung DUY NHẤT trên trang nội bộ SSO đợt này (chỉ liệt kê,
  không tự viết lại): placeholder ô tìm kiếm của bảng bootstrap-table là
  `搜索` ("tìm kiếm") — thấy rõ ở góc phải bảng trên cả 2 ảnh. Lưu ý:
  ô tìm kiếm riêng của form (input `#input-search`) có placeholder tiếng
  Việt ("tìm từ khóa menu cấp cha") theo source, nên lỗi này thuộc về
  cấu hình locale/default của bootstrap-table, không phải của form.
- Wizard 2 bước ("1 chọn menu" → "2 xác nhận nhập menu") hiển thị đúng,
  nút "bước trước / bước tiếp theo" ở cuối trang; bảng rỗng "chưa có dữ
  liệu" tiếng Việt; trang có meta viewport (`width=device-width`) —
  KHÔNG nhất quán với Notice (không có meta).
- Quan sát hiệu năng (không phải lỗi UI): điều hướng SyncForm desktop
  không đạt `networkidle` trong 60s (trang vẫn render xong); có thể do
  request treo/polling. Chặng redesign nên đối chiếu log mạng, không sửa
  mù.
- Checklist: §9.7 (tràn), §9.8 (chuỗi tiếng Trung), §9.1 (nhãn bị cắt).
  Mức: Medium (placeholder tiếng Trung + tràn bước); Low cho networkidle.

**SSO-11 — Portal desktop (bằng chứng mới, bổ sung SSO-04 ở tablet)**
- Ảnh: `sso/portal-home-int-desktop.png`. Sidebar ở 1280px hiển thị đầy
  đủ nhãn (không bị cắt như ở tablet 768 đợt trước) — vậy SSO-04 là vấn
  đề riêng của breakpoint tablet, không phải mọi viewport.
- WorkSpace DashBoard render được (không trắng như nghi vấn ở tablet):
  carousel 3 thẻ hệ thống, "truy cập nhanh", bảng "bàn làm việc - phân
  tích thống kê" với "Tổng cộng 0 / 0 / 0", biểu đồ "thống kê loại khám"
  trống ("lượt người: 0 người"). Các số 0 là dữ liệu thật trên demo
  (không tự điền, không kết luận là lỗi).
- Lỗi còn lại: (1) thẻ carousel giữa tràn/chồng lên thẻ thứ ba ("hệ thống
  hợp tác y tế - đ..." bị cắt và chồng); (2) footer vendor tiếng Trung
  nằm cuối sidebar, chữ xếp chồng/cắt ở đáy cột trái (chỉ liệt kê, xem
  §5); (3) tên người dùng topbar bị cắt "Quan tri vien【0000…";
  (4) không có meta viewport trong DOM portal (đo = null ở cả desktop +
  mobile — xác nhận lại SSO-06, không còn là mâu thuẫn source/DOM một
  lần); (5) nhãn menu không dấu (SSO-05 vẫn nguyên).
- Thanh tab: container `NAV.page-tabs` rộng 100000px (cơ chế cuộn tab
  nội bộ, tài liệu không tràn) — ghi nhận, không tính là lỗi tràn.
- Checklist: §9.1, §9.7, §9.8. Mức: Medium (carousel chồng + footer đè);
  Low (tên người dùng cắt, menu không dấu).

**SSO-12 — Portal mobile: KHÔNG có nút hamburger/drawer nhìn thấy được**
- Ảnh: `sso/portal-home-int-mobile.png`. Topbar mobile chỉ còn icon
  người dùng ở góc phải, không thấy tên người dùng, không thấy nút mở
  sidebar. Sidebar chỉ còn dải tối hẹp bên trái (dạng thu gọn, không đọc
  được nhãn). Footer tiếng Trung xuất hiện Ở ĐẦU viewport, phía trên
  thanh tab — tức thứ tự DOM/footer trên mobile bị đảo lên trước nội dung.
- Quét DOM theo các selector chuẩn (hamburger/navbar-toggle/
  sidebar-toggle/menu-toggle/fa-bars) cho kết quả **0 ứng cử viên** trên
  mọi trang nội bộ (kể cả portal). Trên portal desktop có 2 icon nhỏ ở
  topbar (cạnh "bàn làm việc") có thể là nút thu gọn sidebar, nhưng
  **chưa được bấm thử** (không nằm trong selector quét) nên không khẳng
  định chức năng. Kết luận cho §3.5: **pattern drawer (hamburger luôn
  thấy + drawer phủ + overlay + nút đóng + khóa cuộn nền + focus trả về
  nút) KHÔNG quan sát được ở mobile** — đây vẫn là khoảng trống đã biết
  trong DESIGN.md §10, nay có bằng chứng ảnh.
- Checklist: §9.1, §9.2, §9.7. Mức: **High** (điều hướng mobile của shell
  portal không dùng được theo bằng chứng hiện có).

## 3. Lỗi UI/UX theo checklist DESIGN.md §9 — MRQC

**MRQC-04 — MedicalCenter desktop: bố cục 2 vùng tốt, nhưng lưới rỗng không có thông báo rỗng**
- Ảnh: `mrqc/medicalcenter-int-desktop.png`. Đo: lưới chính
  `#gridListPat` rộng **1979px** trong wrapper (tài liệu 1280/1280),
  lưới chẩn đoán `#gridZdList` 727px; CJK body = 0; console = 0.
- Bố cục: panel lọc trái (loại bệnh nhân: đang nằm viện / đã xuất viện /
  khám ngoại trú; từ ngày / đến ngày nhập viện mặc định 2026-08-01 →
  2026-10-28; ô "Họ tên / Số nhập viện" + nút tìm), vùng phải là
  "Thông tin bệnh nhân" + tab (Chẩn đoán / Hồ sơ / Y lệnh / Báo cáo xét
  nghiệm / Báo cáo kiểm tra). Khi chưa chọn bệnh nhân, vùng chi tiết
  trống hoàn toàn, **không có dòng hướng dẫn** ("chọn một bệnh nhân để
  xem") và lưới chính cũng không hiện chuỗi rỗng trong vùng nhìn thấy
  (chỉ trắng) — vi phạm §6.2/§9.4 về trạng thái rỗng có hướng dẫn.
- Thiếu: không h1, không meta viewport (đo = null cả desktop + mobile).
- Checklist: §9.4 (trạng thái rỗng), §9.1. Mức: Medium (rỗng không hướng
  dẫn); Low (h1/meta).

**MRQC-05 — MedicalCenter mobile: lọc xếp dọc tốt, lưới không cột neo, chi tiết nằm dưới nếp gấp**
- Ảnh: `mrqc/medicalcenter-int-mobile.png`. Đo: lưới 1999px, tài liệu
  390/390 (wrapper cuộn). Panel lọc xếp thành 1 cột đúng §4.3 (ngày
  tháng chiếm toàn chiều rộng, radio xếp dọc). Lưới chỉ thấy các cột đầu
  (Khoa / Số giường / Họ tên / Giới tính / Tuổi…), các cột sau cần cuộn
  ngang và **không có cột neo** — giống SSO-09.
- Vùng chi tiết bệnh nhân (phía phải trên desktop) nằm dưới nếp gấp trên
  mobile, không xác minh được thứ tự hiển thị trong ảnh viewport (cần
  cuộn — không chụp full-page theo quy tắc skill). Ghi là chưa kiểm
  chứng, không khẳng định là lỗi.
- Checklist: §9.4, §9.7. Mức: Medium.

**MRQC-06 — Apply desktop: lưới có dữ liệu thật, pager tiếng Việt, radio lọc vẫn xếp vỡ**
- Ảnh: `mrqc/apply-int-desktop.png`. Đo: lưới `#gridList` **1763px**,
  19 cột (rn, cb, Id, ApplyDeptName, Bed, PatName, ApplyDoctorName,
  ApplyType, MedicalName, CompletionDate, ApplyDate, ApplyCompletionDate,
  ApplyReason, ApplyStatus, ApproverDeptName, ApproverName, ApprovalDate,
  Ryrq, Cyrq); **3 dòng dữ liệu có sẵn** (PILOT-DEMO-001/002/003,
  Khoa Noi, giường 01–03 — chỉ XEM, không submit, không ghi DB).
- Pager hiển thị trực tiếp trong ảnh: "Hiển thị 1 - 3 trong tổng số
  3 bản ghi" + "Trang 1 trên tổng số 1 trang" + 15 dòng/trang — xem xác
  nhận (a) ở §5. Dòng được chọn nổi bật? Không xác minh (không bấm chọn
  dòng — read-only, không tương tác ngoài điều hướng).
- Lỗi nhỏ: nhóm radio trạng thái ("Tất cả / Chưa phê duyệt / Đã phê
  duyệt") đã vỡ hàng ngay trên desktop ("Đã phê duyệt" rớt xuống dòng
  2); tên hồ sơ trong dòng bị cắt ("PILOT-DEMO Ho so b…") do chiều rộng
  cột cố định.
- Checklist: §9.4 (đạt phần pager), §9.3 (lọc vỡ hàng). Mức: Low.

**MRQC-07 — Apply mobile: form lọc vỡ nặng (ô ngày hiển thị vỡ thành mảnh "2026")**
- Ảnh: `mrqc/apply-int-mobile.png`. Đây là lỗi mobile rõ nhất đợt này:
  2 ô ngày trong "Điều kiện lọc" không còn là ô ngày đọc được mà hiển
  thị chồng/xếp dọc thành các mảnh "2026" (datetime-picker vỡ layout ở
  390px); radio trạng thái xếp dọc từng dòng ("Chưa phê duyệt" tách
  thành 2 dòng); nút "Tra cứu" vẫn bấm được.
- Lưới mobile: tham số jqGrid `width` báo 390 nhưng bảng thực tế 1763px
  (wrapper cuộn) — tức `setGridWidth`/autowidth không đồng bộ với chiều
  rộng bảng thực, đúng cảnh báo §4.2 (lưới không tự co theo container).
  Cột đầu (checkbox + Khoa đăng ký) thấy được, tên bệnh nhân bị cắt
  ("PILOT-DEMO…"), các cột sau cần cuộn ngang, không cột neo.
- Checklist: §9.3 (form mobile), §9.7, §4.2. Mức: **High** (lọc theo ngày
  không dùng được trên mobile — chức năng chính của trang).

## 4. Lỗi UI/UX theo checklist DESIGN.md §9 — Sett

**Sett-04 — Home nội bộ desktop: trang danh sách liên kết trần, không có shell**
- Ảnh: `sett/home-int-desktop.png`. Tiêu đề "Sett — Hệ thống quản lý thu
  phí" + 4 nhóm liên kết (Nghiệp vụ thu phí / quyết toán; Bệnh nhân;
  Báo cáo / truy vấn / DRG / BHYT; Hệ thống / kỹ thuật), mỗi liên kết
  kèm tên area trong ngoặc (vd "Ghi sổ nội trú (HospitalizationManage)").
  Đo: không tràn, CJK = 0, console = 0.
- Giống MRQC-02: trang này KHÔNG có topbar/sidebar/footer (§9.1 không
  đạt nếu coi đây là trang chủ app). Cần quyết định của điều phối viên
  như MRQC-02: đây là trang chủ thật đã thay trong phase 2 ("real home")
  hay trang tạm? Audit không tự kết luận. Nếu là trang tạm thì mức High;
  nếu có chủ ý thì cần ghi vào hồ sơ thiết kế để chặng redesign khỏi
  "chuẩn hóa" nhầm. Tên area trong ngoặc là ngôn ngữ kỹ thuật, không phải
  ngôn ngữ nhiệm vụ (§9.8).
- Mobile + tablet của Home: CHƯA chụp (dừng đăng nhập, xem Sett-08).
- Checklist: §9.1, §9.8. Mức: High nếu là trang tạm; chưa kết luận.

**Sett-05 — DaySett quyết toán ngày nội trú (desktop): cột đầu bị cắt, trạng thái rỗng tô đỏ như lỗi**
- Ảnh: `sett/daysett-inpatient-int-desktop.png`. Thanh công cụ: ô khoảng
  ngày quyết toán (ô bắt đầu hiển thị chuỗi placeholder bị cắt/xếp chồng,
  không đọc được đầy đủ; ô kết thúc "2026-10-28 14:13:54") + nút icon
  làm mới **không nhãn** (§9.5: icon đơn độc).
- Lưới: tiêu đề cột đầu tiên bị cắt ở cạnh trái (trong ảnh chỉ còn
  "…này lần Số tiền quy…") — cột định danh đầu tiên không đọc được ngay
  cả trên desktop. Trạng thái rỗng hiển thị bằng chữ ĐỎ "Không tìm thấy
  dữ liệu liên quan!" — tức trạng thái "chưa có dữ liệu" được tô như lỗi
  (§6.2: phải phân biệt "chưa có dữ liệu" với "lọc không ra kết quả" và
  với lỗi; ở đây cả 3 đều có thể trông giống nhau).
- Pager tiếng Việt thấy trong ảnh: "Trang 1 trên tổng số 0 trang" +
  "Không có dữ liệu hiển thị" + 15 dòng/trang + nút "F6:Xác nhận quyết
  toán" / "F7:Hủy quyết toán" (nhãn là mã phím chức năng, không phải động
  từ nhiệm vụ theo §9.5/§5.1 — người dùng mới không biết F6/F7 làm gì nếu
  không đọc kỹ). Lưu ý: số liệu JSON chi tiết của lưới này bị mất do lần
  chạy đầu crash trước khi ghi file (xem Sett-07) — pager trong ảnh là
  bằng chứng live, không phải số đo JSON.
- Checklist: §9.4, §9.5, §9.6/§6.2. Mức: Medium (cột đầu cắt + rỗng tô đỏ);
  Low (nút icon không nhãn, nhãn F6/F7).

**Sett-06 — PatientInHosList (desktop): lưới thật, 0 dòng mặc định, cùng mẫu rỗng tô đỏ**
- Ảnh: `sett/patientinhoslist-int-desktop.png`. Đo: lưới `#gridList`
  **1335px**, 12 cột (rn, blh, xm, xb, nl, zyh, cw, ryzd, ryrq, cyrq,
  csrq, zjh), records = 0, page = 1, rowNum = 15; CJK = 0; console = 0.
- Ô tìm kiếm duy nhất "Họ tên / Số nhập viện" + nút icon tìm (không
  nhãn). Trạng thái rỗng: chữ đỏ "Không tìm thấy dữ liệu liên quan!" +
  chân lưới "Không có dữ liệu hiển thị" + pager "Trang 1 trên tổng số
  1 trang" (trong ảnh; trích xuất DOM bỏ qua giá trị của ô input số
  trang nên bản JSON ghi "Trang  trên…" — ảnh là bằng chứng đúng).
- Đối chiếu đợt trước: đợt sweep cuối ghi "PatientInHosList still
  stub-empty" — nay trang render lưới + cột + tìm kiếm thật, chỉ là 0
  dòng ở trạng thái mặc định (chưa submit tìm kiếm — read-only nên không
  thử). Không khẳng định DB có hay không có bệnh nhân nội trú; chỉ khẳng
  định giao diện tải được và trạng thái mặc định là rỗng.
- Checklist: §9.4, §9.5, §6.2. Mức: Medium (rỗng tô đỏ); Low (nút icon
  không nhãn, không h1/meta).

**Sett-07 — BookkeepInHos: KHÔNG AUDIT ĐƯỢC (lỗi tải trang, 2 lần, 2 kiểu)**
- Lần 1 (chạy Sett đầu): điều hướng tới
  `/HospitalizationManage/BookkeepInHos/Index` làm **tiến trình Firefox
  đóng** (browser closed giữa chừng, cả phiên audit Sett desktop dừng).
- Lần 2 (chạy lại, chờ `domcontentloaded` thay vì `networkidle`):
  điều hướng **timeout 60s**, URL cuối vẫn là `/Home/Index` (không tới
  được trang) — ảnh chụp lúc đó thực chất là Home nên đã XÓA, không để
  lại file `bookkeepinhos-int-desktop.png`.
- Đây không phải lỗi UI để ghi mức độ, mà là **phát hiện chặn**: trang
  này chưa từng được audit thành công (đợt trước ảnh Bookkeep cũng chỉ
  là trang login redirect). Source đọc thêm (không sửa): controller
  `BookkeepInHosController.Index()` đọc cấu hình hệ thống qua
  `_sysConfigRepo` (vd `HOSP_AccountingPage_May_Medicine`); view 35 KB,
  không thấy ActiveX/DCSoft. Nguyên nhân timeout (server chậm? request
  treo? DI nặng với ~30 service?) cần điều phối viên đối chiếu log Sett
  — audit không kết luận.
- Mức: **Blocker cho hồ sơ Bookkeep** (không có ảnh, không gán chiến
  lược A/B/C — xem §5(c)).

**Sett-08 — Sett mobile + tablet: CHƯA CHỤP (dừng đăng nhập)**
- Sau khi Sett desktop thành công 2 lần, đăng nhập mobile thất bại 1 lần
  với thông báo tiếng Trung `登录失败，请联系管理员` (cùng thông báo với đợt
  trước — và cũng là bằng chứng §9.8: thông báo lỗi đăng nhập của Sett
  vẫn tiếng Trung, trong khi SSO đã tiếng Việt). Theo quy tắc cứng,
  không thử lại mobile, không thử tablet.
- Hệ quả: toàn bộ hành vi mobile của Sett (lưới DaySett/Patient cuộn
  ngang hay thẻ, form lọc 1 cột, nút F6/F7, sidebar shell) vẫn **chưa
  kiểm chứng**; hồ sơ A/B/C ở §5(c) cho Sett mobile là **đề xuất theo
  nhiệm vụ**, chưa phải hành vi đã quan sát trên mobile.

## 5. Xác nhận 3 điểm còn mở từ đợt trước (bằng bằng chứng thật)

### (a) Locale jqGrid hiển thị cuối cùng: ✅ XÁC NHẬN LÀ TIẾNG VIỆT (live)

- **MRQC Apply (bằng chứng mạnh nhất):** jqGrid **5.1.0**, pager hiển
  thị thật trong ảnh desktop + mobile: "Hiển thị 1 - 3 trong tổng số
  3 bản ghi"; giá trị mặc định đo được từ JS: `recordtext` =
  "Hiển thị {0} - {1} trong tổng số {2} bản ghi", `emptyrecords` =
  "Không có dữ liệu hiển thị", `pgtext` =
  "Trang {0} trên tổng số {1} trang" — cả desktop lẫn mobile giống nhau.
  Đây là lần đầu tiên pager lưới thật được đo live (đợt trước chỉ suy từ
  thứ tự nạp source).
- **Sett (bằng chứng ảnh):** DaySett pager "Trang 1 trên tổng số 0 trang"
  + "Không có dữ liệu hiển thị"; PatientInHosList pager "Trang 1 trên
  tổng số 1 trang" + "Không có dữ liệu hiển thị" — đều tiếng Việt.
- **Ngoại lệ duy nhất:** SSO Notice/SyncForm không dùng jqGrid mà dùng
  bootstrap-table — chuỗi rỗng tiếng Việt ("chưa có dữ liệu") nhưng
  placeholder tìm kiếm của bảng SyncForm là tiếng Trung `搜索` (SSO-10).
  Kết luận: locale jqGrid = tiếng Việt đã xác nhận; file
  `grid.locale-cn.js` nạp trước không thắng ở các lưới đã đo — nhưng
  chặng triển khai vẫn không xóa bừa file locale theo cảnh báo §9.8.

### (b) Hành vi sidebar mobile và nút hamburger: ❌ KHÔNG XÁC MINH ĐƯỢC LÀ HOẠT ĐỘNG

- Các trang nội bộ MRQC/Sett/SSO-Notice/SyncForm khi mở trực tiếp đều là
  **fragment không có shell** (không sidebar, không topbar) — quét DOM
  cho 0 ứng cử viên hamburger trên mọi trang. Nút hamburger (nếu có)
  sống ở **shell portal**, không sống ở các trang này.
- Shell duy nhất quan sát được ở mobile là **SSO portal**: không thấy
  nút mở sidebar, sidebar chỉ còn dải tối hẹp, không có drawer/overlay/
  nút đóng/khóa cuộn (SSO-12). Trên portal desktop có 2 icon topbar chưa
  được bấm thử nên không kết luận chức năng.
- Kết luận: yêu cầu §3.5 (hamburger luôn thấy < 992px + drawer) **chưa
  đáp ứng theo bằng chứng quan sát được**. Đây là việc của chặng
  redesign, không phải việc audit tự sửa. Hành vi sidebar mobile của
  Sett/MRQC không thể tách khỏi shell portal — cần kiểm chứng lại trên
  shell sau khi đăng nhập ổn định, không kiểm chứng bằng cách mở
  fragment trực tiếp.

### (c) Hồ sơ chiến lược responsive A/B/C cho từng lưới Sett chính

Theo quy tắc §4.1 (mặc định A trừ khi chứng minh nhiệm vụ là tra cứu
từng bản ghi → B; C chỉ cho lưới xem nhanh trạng thái):

| Lưới Sett | Nhiệm vụ | Chiến lược | Lý do / trạng thái bằng chứng |
|---|---|---|---|
| DaySett `InpatientDaySett` (quyết toán ngày nội trú) | Duyệt loạt chứng từ theo ngày, đối chiếu số tiền/người/giờ | **A** (cuộn ngang + cột neo) | Cần so sánh ngang nhiều dòng; hiện tại đã cuộn ngang trong wrapper nhưng **thiếu cột neo** và cột đầu còn bị cắt (Sett-05). C bị loại (không phải xem nhanh trạng thái); B bị loại (không phải xử lý từng bản ghi). |
| PatientInHosList (danh sách bệnh nhân nội trú) | Tìm một bệnh nhân (theo họ tên/số nhập viện) rồi xử lý bản ghi đó | **B** (dòng → thẻ) | Nhiệm vụ chính là tra cứu từng bản ghi; thẻ phải giữ cùng thứ tự trường + nhãn với lưới desktop (12 cột). Lưu ý: đây là đề xuất theo nhiệm vụ — **mobile chưa chụp được** nên hành vi thẻ chưa quan sát; nếu điều phối viên xác định nhiệm vụ chính thực ra là duyệt loạt thì đổi sang A. |
| BookkeepInHos (ghi sổ nội trú) | Ghi sổ theo từng bệnh nhân/chứng từ (theo tên/khu vực) | **CHƯA GÁN** | Trang không tải được (Sett-07). Hướng phân tích khi tải được: nghiêng **B** nếu thao tác chính là xử lý từng chứng từ, quay về **A** nếu là duyệt loạt. Không gán chính thức khi chưa có ảnh. |
| Các lưới Sett còn lại trong 22 khu vực | Chưa vào phạm vi đợt này | Chưa gán | Mỗi lưới cần một dòng hồ sơ riêng khi audit tới; không áp một chiến lược cho mọi lưới. |

Nhận xét chung: hành vi **hiện tại** của mọi lưới đã đo (desktop) đều
là "bảng rộng trong wrapper cuộn" (giống A nhưng thiếu cột neo) — tức
dù chọn A hay B, chặng redesign vẫn phải bổ sung cột neo (cho A) hoặc
render thẻ (cho B); không được giữ nguyên trạng thái "cuộn mà mất cột
định danh" rồi gọi đó là A.

## 6. Khoảng trống bằng chứng còn lại + việc cần điều phối viên làm

1. **Kiểm tra khóa + log đăng nhập trước vòng tiếp theo:** cờ `Locked`
   của 000000 và log CheckLogin tại thời điểm SSO tablet thất bại và
   Sett mobile thất bại (thông báo Sett vẫn tiếng Trung — cũng cần sửa
   ở chặng ngôn ngữ, xem Sett-08). Không chạy thêm vòng đăng nhập nào
   cho tới khi kiểm tra xong.
2. **BookkeepInHos:** đối chiếu log Sett 5221 khi truy cập
   `/HospitalizationManage/BookkeepInHos/Index` (timeout 60s ×2 + 1 lần
   Firefox đóng) trước khi audit lại trang này.
3. **Chụp bù (khi đăng nhập ổn định):** SSO tablet (portal + Notice +
   SyncForm), Sett mobile + tablet (Home, DaySett, Bookkeep, Patient),
   MRQC tablet (không bắt buộc — desktop + mobile đã đủ cho 2 trang).
4. **Quyết định về trang chủ trần:** Sett Home (Sett-04) và MRQC Home
   (MRQC-02 đợt trước) có phải trang tạm không, trước khi chặng redesign
   "chuẩn hóa" chúng thành shell chuẩn.
5. **Quyết định footer dùng chung (§3.1/§7):** danh sách trang còn chuỗi
   tiếng Trung trong phạm vi đợt này: portal SSO (footer, desktop +
   mobile), SyncForm (placeholder tìm kiếm của bảng `搜索`). Không tự
   viết lại trong audit.
6. **Không thay đổi trong đợt này:** mâu thuẫn receipt-prefix (ngoài
   phạm vi), tên bệnh viện thật, dữ liệu thật, tài khoản demo/secret.

## 7. Đối chiếu checklist §9 (chỉ phần nội bộ mới đo được)

| Mục §9 | SSO nội bộ | MRQC nội bộ | Sett nội bộ |
|---|---|---|---|
| 9.1 Header (topbar, h1, footer) | ❌ Portal: footer tiếng Trung, tên cắt, không meta; Notice/SyncForm: không h1 | ❌ Không h1, không meta | ❌ Home trần không shell; các lưới: không h1/meta |
| 9.2 Sidebar | ❌ Mobile: không hamburger/drawer thấy được | N/A (fragment; shell = portal) | N/A (fragment; shell = portal) |
| 9.3 Form | ⚠️ SyncForm: nhãn cắt, placeholder bảng tiếng Trung | ❌ Apply mobile: ô ngày vỡ | ⚠️ DaySett: ô ngày placeholder cắt; nút icon không nhãn |
| 9.4 Lưới jqGrid | ⚠️ Notice: bảng 879px, không cột neo (bootstrap-table) | ⚠️ Lưới 1763–1999px, không cột neo; pager VI ✅ | ⚠️ Lưới 1335px+, không cột neo; pager VI ✅; rỗng tô đỏ |
| 9.5 Nút | ⚠️ Wizard đủ nút, rõ nhãn | ⚠️ Radio lọc vỡ hàng | ❌ Nút icon không nhãn; nhãn F6/F7 là mã phím |
| 9.6 Thông báo | ⚠️ Lỗi đăng nhập Sett tiếng Trung (khi thất bại) | Chưa kiểm chứng (không thao tác) | ❌ Rỗng tô đỏ như lỗi (DaySett, Patient) |
| 9.7 Mobile | ❌ Portal mobile: không điều hướng được; SyncForm tràn +6px | ❌ Apply mobile: lọc ngày vỡ (High) | ⛔ Chưa chụp (dừng đăng nhập) |
| 9.8 Thị giác + ngôn ngữ | ❌ Footer + placeholder `搜索` tiếng Trung; menu không dấu | ✅ Body 0 CJK; pager VI | ✅ Body 0 CJK; pager VI |

Không mục nào ở trên được tính là "đạt" cho toàn bộ trang nội bộ — bảng
chỉ phản ánh những gì đã đo được; phần lớn mobile Sett và tablet SSO là
"chưa kiểm chứng", không phải "đạt".

## 8. Ngoài phạm vi (ghi nhận, không đụng tới)

- Mâu thuẫn tiền tố chứng từ: không gặp, không đụng tới.
- Tên bệnh viện thật, dữ liệu thật: không tự điền; 3 dòng PILOT-DEMO
  trên lưới Apply chỉ được XEM (read-only), không submit, không sửa.
- Tài khoản demo/mật khẩu mặc định/secret: không thay đổi; chỉ ghi nhận
  2 lần đăng nhập thất bại (§0) để điều phối viên kiểm tra khóa.
- Windows-only (DCSoft/SSRS/WCF): không gặp trong phạm vi đã render;
  Bookkeep timeout chưa kết luận nguyên nhân, không đổ cho Windows-only.
- Không có code nào bị sửa, không có commit nào được tạo trong chặng
  này. Script đo: `/tmp/audit_sms2.py` (SSO/MRQC + Sett lần 1),
  `/tmp/audit_sms3.py` (Sett lần 2); log JSON: `/tmp/audit-sms2-mrqc.json`
  (MRQC), `/tmp/audit-sms3.jsonl` (Sett desktop lần 2); log JSON SSO lần
  1 đã bị ghi đè bởi lần chạy MRQC nhưng các số đo SSO đã được trích
  xuất trong quá trình chạy và đối chiếu với ảnh trước khi ghi đè.

---

*Hết báo cáo audit bổ sung SSO/MRQC/Sett nội bộ. Trạng thái tổng thể:
SSO + MRQC nội bộ đã audit được ở desktop + mobile; Sett chỉ desktop
(một phần); Bookkeep, Sett mobile/tablet, SSO tablet còn trống do dừng
đăng nhập theo quy tắc cứng — không phải do worker bỏ sót mà không báo.*
