# Audit UI/UX — MRMS (5232), OR (5231), EMR (5235)

> Phạm vi: AUDIT ONLY. Không sửa code, không commit, không khởi động/dừng app,
> không submit form, không ghi database (kể cả STG). Chỉ điều hướng bằng GET và
> chụp ảnh. Mobile = giả lập viewport, KHÔNG phải app native.
> Khung phân loại: `scripts/redesign/DESIGN.md` §9 (checklist) và §10 (bảng tra nhanh).
> Tài khoản duy nhất dùng để đăng nhập: `000000` (mật khẩu tính md5 bằng `md5sum`,
> không đoán mật khẩu khác).

## 0. Tóm tắt điều hành

- **Pass chính (trước sự cố) THÀNH CÔNG:** đăng nhập `000000` thành công ở cả 3 app;
  đã chụp đủ: đăng nhập + trang chủ + 2–3 trang chức năng chính, ở desktop 1280
  và mobile 390×844, trang chủ thêm tablet 768. Console errors = `[]` trên mọi
  trang đã chụp ở pass chính.
- **SỰ CỐ HẠ TẦNG giữa chừng (không phải lỗi UI, cần điều phối viên xử lý):**
  khoảng ~21:00 ICT ngày 2026-10-28, SQL Server ngừng phục vụ (không còn tiến
  trình `sqlservr`, không còn listener cổng 1433; log BaseAPI ghi
  `CheckPwd` lỗi kết nối database). Từ đó mọi lần đăng nhập lại đều thất bại
  ("Đăng nhập thất bại, vui lòng liên hệ quản trị viên" ở MRMS;
  "Phiên đăng nhập hệ thống đã hết hạn" ở EMR). **Đây KHÔNG được kết luận là
  tài khoản bị khóa** — bằng chứng chỉ ra lỗi kết nối DB, không phải sai mật khẩu.
  Worker audit đã DỪNG dò đăng nhập sau khi xác định nguyên nhân, không tự
  khởi động lại SQL Server hay app (ngoài quyền audit).
- **Hậu quả của sự cố đối với audit này (3 mục CHƯA xác minh được, cần chạy lại
  sau khi DB hồi phục):** (1) ảnh thật route `MRHomePage/Main` của EMR (route thật
  thay cho `Index` đã chết — xem EMR-03); (2) nút hamburger trên trang chủ mobile
  (lần bấm thử bị vô hiệu vì phiên đã mất, xem §4c); (3) khả năng cuộn ngang
  bên trong lưới NursingRecord (bdiv) — chỉ mới thấy cột bị cắt ở mép viewport.
- **Phát hiện nặng nhất (xuyên app):**
  1. Shell trang chủ mobile của MRMS/EMR **không responsive**: sidebar không thành
     drawer, nội dung iframe bị bóp hẹp, KPI/biểu đồ/tab chồng lấn, không thấy
     nút hamburger (MRMS-HOME-M1, EMR-HOME-M1).
  2. Trang chủ OR **không có shell** (không topbar/sidebar/tên người dùng), chỉ là
     danh sách liên kết trần — lệch hoàn toàn với MRMS/EMR (OR-HOME-01).
  3. Lưới mobile bị **nén cột (shrinkToFit)** thay vì cuộn ngang/thẻ: cột biến mất
     khỏi màn hình mà không có cách nào xem được (OPStaff, MrDept, MrFee).
  4. Trang đăng nhập **tràn ngang cả desktop (+519px) lẫn mobile (+158px)** ở cả 3 app.
  5. Route EMR `MRHomePage/Index` **không tồn tại** (action `Index()` đã bị comment
     trong source; view `Index.cshtml` không có) — trả về JSON lỗi thay vì trang.
  6. Footer/logo vendor tiếng Trung còn trên trang đăng nhập cả 3 app và trang
     chủ EMR (chỉ liệt kê, không viết lại — xem §5).

## 1. Phương pháp và bằng chứng

- Công cụ: Playwright + Firefox local, venv `~/workspace/shot/venv`.
  Script pass chính: `/tmp/audit_ui.py` (không lưu vào repo);
  pass bổ sung: `/tmp/audit_suppl.py`, `/tmp/audit_suppl2.py`, `/tmp/audit_suppl3.py`.
- Mỗi trang đo: `scrollWidth − clientWidth` của document (tràn viewport),
  chiều rộng lưới `.ui-jqgrid`, chuỗi locale jqGrid thật (`$.jgrid.defaults`),
  phiên bản asset thật, console errors, ảnh viewport.
- Ảnh (gitignored, không commit): `shots-redesign/<app>/`
  (`mrms/`, `or/`, `emr/`), kèm `probe.json` mỗi app và `supplement.json`.
  Lưu ý: các file `*-mobile-after-sidebar.png` **giống hệt** ảnh mobile tương ứng,
  vì trang chức năng là nội dung iframe không có shell nên không có nút sidebar
  để bấm — đó là bằng chứng, không phải kết quả "sidebar đã kiểm tra xong".
  Các ảnh chụp nhầm trang đăng nhập ở pass bổ sung (do mất phiên) đã bị xóa để
  khỏi gây nhầm lẫn; chỉ giữ ảnh pass chính + `login-mobile.png` (trang đăng nhập
  không cần DB nên vẫn hợp lệ).

## 2. MRMS (5232)

### 2.1. Trang đăng nhập — `login-desktop.png`, `login-mobile.png`

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| MRMS-LOGIN-01 | Cao | **Tràn ngang:** document overflow +519px ở desktop 1280, +158px ở mobile 390. Ảnh nền cố định chiều rộng lớn hơn viewport; người dùng cuộn ngang ra vùng trống. (§9.7) | `login-desktop.png`, `login-mobile.png` |
| MRMS-LOGIN-02 | Thấp | Form gọn, nhãn tiếng Việt rõ ("Tài khoản đăng nhập", "Mật khẩu đăng nhập", nút "Đăng nhập"); không có captcha (đúng cấu hình). | `login-desktop.png` |
| MRMS-LOGIN-03 | Liệt kê | Logo + footer vendor tiếng Trung (xem §5). | `login-desktop.png`, `login-mobile.png` |

### 2.2. Trang chủ — `home-desktop.png`, `home-tablet.png`, `home-mobile.png`

- Desktop: dashboard đầy đủ nhất trong 3 app — 5 thẻ KPI, biểu đồ đường
  "Thống kê đăng ký khám", biểu đồ donut "Thống kê theo loại đăng ký", 3 danh
  sách "Cập nhật hệ thống / Thông báo / Văn hóa doanh nghiệp". Sidebar đủ mục,
  topbar có tên app "Benh vien Newtouch HIS" + tài khoản `000000` + nút ≡.
- Tablet 768: sidebar bị cắt nhãn ("Quan ly", "Cai dat", "Tram y" — nhãn tiếng
  Việt dài hơn chiều rộng sidebar, bị cắt cứng không dấu ba chấm/tooltip);
  danh sách tin tức **chồng lấn ngày tháng lên tiêu đề**
  ("Phát hành phiên …2016-12-21" đè nhau); thẻ KPI vẫn giữ 5 cột nên nhãn bị
  bóp ("Đăng ký", "Thu phí" xuống dòng, khó đọc).
- Mobile 390: **lỗi nặng** — sidebar chỉ còn dải hẹp không đọc được mục nào;
  5 thẻ KPI bị nén thành 5 cột hẹp (~70px/cột), số và nhãn chồng lấn;
  biểu đồ đường: nhãn trục tháng chồng nhau không đọc được; donut: legend xếp
  dọc chiếm hết chiều rộng; topbar chỉ còn icon người dùng, **không thấy nút
  hamburger**, không thấy tên app/tên người dùng.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| MRMS-HOME-M1 | Chặn (mobile) | Shell mobile không thành drawer (§3.5): sidebar không mở được, không overlay, không nút đóng; nội dung iframe bị bóp thay vì xếp lại. (§9.2, §9.7) | `home-mobile.png` |
| MRMS-HOME-T1 | Trung bình | Tablet: nhãn sidebar + tin tức chồng lấn ngày tháng; KPI không giảm xuống 2 cột (§4.3). | `home-tablet.png` |
| MRMS-HOME-D1 | Thấp | KPI chỉ có con số trơ trọi, **không có điểm so sánh/mục tiêu/hướng tốt-xấu** (§6.1); icon trang trí lệch nghĩa (búa gỗ cho "Quyết toán xuất viện", xe tải cho "Quản lý bệnh nhân"). | `home-desktop.png` |
| MRMS-HOME-D2 | Thấp | Không có `<h1>` (probe `h1: []`); không có breadcrumb; thanh tab chỉ có "Trang chủ / Thao tác tab". (§9.1) | `home-desktop.png` |

### 2.3. Khoa bệnh án (MrDept) — `mrdept-desktop.png`, `mrdept-mobile.png`

- Desktop: 2 lưới cạnh nhau (37 + 17 bản ghi), chân lưới tiếng Việt đúng chuẩn
  ("Hiển thị 1 - 10 trong tổng số 37 bản ghi"). Thanh công cụ thống nhất:
  Tạo mới / Sửa / Xóa + ô tìm kiếm.
- Mobile: chỉ lưới trái vừa viewport; lưới phải nằm dưới (ngoài ảnh viewport);
  ô tìm kiếm xuống dòng thứ hai; tên khoa dài ("Khoa Y hoc cham soc tich cuc")
  bị cắt ở mép phải.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| MRMS-DEPT-01 | Trung bình | Lưới mobile bị nén cột thay vì cuộn ngang có cột neo; không có nút "Xem thêm"/cột neo. Hồ sơ chiến lược: **A** (so sánh ngang 2 lưới đối chiếu mã khoa) — xem §4d. | `mrdept-mobile.png` |
| MRMS-DEPT-02 | Thấp | **3 nút primary xanh giống hệt nhau** trên một thanh công cụ (Tạo mới/Sửa/Xóa),
  nút Xóa không dùng danger, không tách vị trí (§2.1, §9.5). Không có chỉ báo
  "đang lọc" / nút "Xóa lọc" (§9.4). | `mrdept-desktop.png` |

### 2.4. Nhóm thu phí bệnh án (MrFee) — `mrfee-desktop.png`, `mrfee-mobile.png`

- Desktop: lưới 6 cột, 25 bản ghi, hiển thị tốt; có chỉ báo sắp xếp ở cột
  "Tên nhóm cấp 1"; một ô tên dài bị cắt ở mép cột ("Phi hang muc chan doan
  lam sang" + ký tự "c" lửng) — không xuống dòng, không tooltip thấy được.
- Mobile: lưới nén về đúng 390px (probe `w=390, scrollW=392`) — cột bị bóp,
  không cuộn ngang, không thẻ chi tiết.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| MRMS-FEE-01 | Trung bình | Như MRMS-DEPT-01: chiến lược **A** (lưới tra cứu/phân cấp 3 cấp, cần so sánh ngang). Hiện tại nén cột làm mất thông tin. | `mrfee-mobile.png` |
| MRMS-FEE-02 | Thấp | Cắt chuỗi không tooltip (§7); cùng lỗi nút primary ×3 + Xóa không danger như trên. | `mrfee-desktop.png` |

## 3. OR (5231)

### 3.1. Trang đăng nhập — `login-desktop.png`, `login-mobile.png`

Giống hệt MRMS: tràn ngang +519px desktop / +158px mobile; form tiếng Việt tốt;
logo + footer vendor tiếng Trung (xem §5).

### 3.2. Trang chủ — `home-desktop.png`, `home-tablet.png`, `home-mobile.png`

**Không có shell.** Trang `/Home/Index` của OR trả về một trang trần: tiêu đề
" Hệ thống phòng mổ", 3 nhóm liên kết ("Đăng ký và sắp xếp phẫu thuật",
"Danh mục", "Báo cáo") + liên kết "Đăng xuất" ở cuối. Không topbar, không
sidebar, không tên người dùng, không tab, không KPI/biểu đồ; probe cho thấy
**không nạp jQuery/Bootstrap nào** trên trang này. Mobile hiển thị được (danh
sách một cột) nhưng đó là do trang quá đơn giản, không phải do responsive tốt.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| OR-HOME-01 | Cao | **Không nhất quán shell xuyên app** (§9.1, §9.2): OR không có khung
  chung như MRMS/EMR; người dùng chuyển app phải học lại điều hướng; không thấy
  tên người dùng/khoa đang làm việc; đăng xuất chỉ là một liên kết cuối trang. | `home-desktop.png`, `home-mobile.png` |
| OR-HOME-02 | Thấp | Không `<h1>`, không breadcrumb; liên kết chỉ có chữ xanh mặc định, không icon (§2.4: icon phải kèm chữ — ở đây ngược lại: chữ không icon, chấp nhận được nhưng lệch chuẩn chung). | `home-desktop.png` |

### 3.3. Nhân viên phẫu thuật (OPStaff) — `opstaff-desktop.png`, `opstaff-mobile.png`

- Desktop: lưới 1 bản ghi, chân lưới tiếng Việt đúng. Thanh công cụ giống MRMS.
- Mobile: **lỗi rõ nhất của nhóm lưới nén cột** — chỉ còn 2 cột nhìn thấy
  ("Mã nhân viên", "Họ tên"); các cột còn lại (Mã gợi nhớ, Thời gian tạo,
  Người tạo) biến mất khỏi màn hình, không cuộn được, không thẻ chi tiết.
  Hàng dữ liệu gần như trống (chỉ thấy số thứ tự "1").

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| OR-STAFF-01 | Cao (mobile) | Cột biến mất do nén lưới (§4.2 cấm "thu nhỏ để vừa màn hình"). Hồ sơ chiến lược: **A** mặc định (danh mục nhân sự cần đối chiếu ngang); nếu audit nghiệp vụ xác nhận nhiệm vụ chính là tra cứu từng người thì chuyển **B** (thẻ). | `opstaff-mobile.png` |
| OR-STAFF-02 | Thấp | Cùng lỗi nút primary ×3 + Xóa không danger; không chỉ báo lọc. | `opstaff-desktop.png` |

### 3.4. Đơn đăng ký phẫu thuật (OpApply) — `opapply-desktop.png`, `opapply-mobile.png`

- Desktop: bố cục 2 pane tốt — trái: danh sách bệnh nhân nội trú + lọc theo
  khoa; phải: tab "Thông tin bệnh nhân / Đơn đăng ký phẫu thuật", thẻ hồ sơ
  màu tím, nút "Đăng ký" xanh lá, tab trạng thái (Tất cả / Đã gửi đơn /
  Đã duyệt / Đã hủy). Trạng thái rỗng hiển thị bằng chữ đỏ
  "Không tìm thấy dữ liệu liên quan!" (không có dữ liệu thật để hiển thị —
  audit không bịa dữ liệu).
- Mobile: **vỡ bố cục** — 2 pane không xếp dọc: pane trái chiếm toàn viewport,
  pane phải bị đẩy ra ngoài; 2 tab phải chồng lấn nhau ("Thông tin bệnh nhân"
  đè "Đơn đăng ký phẫu thuật"); ô tìm kiếm bị cắt ("Tên bệnh n…", "Cho…").

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| OR-APPLY-M1 | Chặn (mobile) | Pane không xếp dọc trên mobile (§4.3); tab chồng lấn; form đăng ký không với tới được trên viewport hẹp — chức năng lâm sàng bị chặn, không chỉ xấu. | `opapply-mobile.png` |
| OR-APPLY-01 | Trung bình | Trạng thái rỗng dùng **màu đỏ như lỗi** và không có hành động tiếp theo
  ("Xóa bộ lọc" / "Tạo mới") — vi phạm §6.2 (rỗng không phải lỗi). | `opapply-desktop.png`, `opapply-mobile.png` |
| OR-APPLY-02 | Thấp | Nút "Đăng ký" xanh lá đặt sát mép thẻ, nhãn bị cắt nhẹ ở desktop ("Đăng ký" + ký tự lửng ở mép phải); tab trạng thái không có số lượng/badge giải thích. | `opapply-desktop.png` |

## 4. EMR (5235)

### 4.1. Trang đăng nhập — `login-desktop.png`, `login-mobile.png`

Giống MRMS/OR: tràn ngang +519px desktop / +158px mobile; footer tiếng Trung
dài hơn (cả câu giấy phép, xem §5). Lưu ý: tiêu đề trang EMR dính chữ
("EMRHệ thống quản lý") — lỗi nhỏ về khoảng trắng tiêu đề.

### 4.2. Trang chủ — `home-desktop.png`, `home-tablet.png`, `home-mobile.png`

- Desktop: shell giống MRMS (sidebar + topbar + tab), nhưng **nội dung trống**:
  tab "Tổng quan bệnh nhân" chỉ có bộ lọc (Đang nằm viện / Ra viện / Của tôi,
  khoa, nút "Đồng bộ bệnh nhân") và vùng danh sách trống — **không có thông
  báo trạng thái rỗng** ("Chưa có bệnh nhân" + hành động tiếp theo).
  Topbar có 3 badge đỏ 5/6/16 **không ghi rõ là số gì** (§3.2: badge phải nói
  rõ việc chờ xử lý). Nhãn sidebar bị cắt ("Cai dat he thong thu",
  "Tram bac si ngoai…"). Tab "TrangKýThao tác" bị nối chữ/chồng lấn ngay cả
  ở desktop. Footer vendor tiếng Trung **đè lên cuối sidebar**.
- Tablet: tương tự desktop (sidebar cắt nhãn, nội dung trống).
- Mobile: **lỗi nặng nhất nhóm trang chủ** — dải sidebar hẹp, footer tiếng Trung
  hiện ở góc trên-trái đè lên nội dung, tab "TrangKýThao tác" chồng lấn, ô lọc
  "==Vui lòng chọn==" + nút làm mới xanh chiếm gần hết chiều rộng, danh sách
  trống không có chữ giải thích; topbar chỉ còn icon người dùng + kính lúp,
  **không thấy hamburger**.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| EMR-HOME-M1 | Chặn (mobile) | Như MRMS-HOME-M1, nặng hơn: footer đè nội dung, tab chồng lấn, không hamburger thấy được. (§9.2, §9.7) | `home-mobile.png` |
| EMR-HOME-D1 | Trung bình | Nội dung trống không có trạng thái rỗng (§6.2); badge số không nhãn (§3.2); nhãn sidebar cắt (§7: layout không chịu được nhãn dài hơn ~30%). | `home-desktop.png` |
| EMR-HOME-D2 | Thấp | Không `<h1>`; tiêu đề tab nối chữ; footer tiếng Trung đè sidebar (xem §5). | `home-desktop.png` |

### 4.3. Trang chủ bệnh án (MRHomePage) — `mrhomepage-desktop.png`, `mrhomepage-mobile.png`

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| EMR-03 | Cao (chức năng) | **Route được giao không tồn tại:** `GET /MRHomePage/MRHomePage/Index`
  trả về JSON lỗi `{"state":"error","message":"The view 'Index' was not found…"}`
  (HTTP 200 với thân lỗi — đúng kiểu "200 giả" mà skill cảnh báo). Nguyên nhân
  từ source: `Index()` trong `MRHomePageController.cs` đã bị comment,
  không có `Views/MRHomePage/Index.cshtml`; action thật là `Main()` với
  `Main.cshtml`. Đây là phát hiện về route/menu, **không tự sửa**; ảnh thật của
  `Main` chưa chụp được do sự cố DB (§0) — cần chạy lại sau khi DB hồi phục.
  Lỗi này cũng vi phạm §6.2 (người dùng cuối thấy JSON kỹ thuật, không có
  lối thoát "Quay lại"). | `mrhomepage-desktop.png`, `mrhomepage-mobile.png` |

### 4.4. Phiếu điều dưỡng (NursingRecord) — `nursingrecord-desktop.png`, `nursingrecord-mobile.png`

- Desktop: form lọc trái ("Họ tên/Số nhập viện", "Đang nằm viện"), vùng bệnh
  nhân trống (không có dữ liệu thật — không bịa), lưới dấu hiệu sinh tồn
  **rộng 3.498px** với hàng chục cột (Nhiệt độ, Mạch, Tần số thở, Huyết áp tâm
  thu/tâm trương, Ý thức, Trái/phải đồng tử, Phản xạ ánh sáng…). Ảnh viewport
  cho thấy tiêu đề lưới bị cắt ở mép phải, **không thấy thanh cuộn ngang**;
  probe: document không tràn (overflow 0) nên phần lưới thừa nằm trong
  container con — nhưng khả năng cuộn của container con chưa xác minh được
  do sự cố DB (§0). Nút "Nhập mới / Sửa / Hủy hiệu lực / In" đều xanh primary.
- Ngày trong ô lọc hiển thị dạng `2026-10-01 — 2026-10-28` (yyyy-MM-dd),
  **không theo định dạng Việt Nam dd/MM/yyyy** (§5.1, §7).
- Mobile: cùng lưới 3.498px trên viewport 390 — cột định danh (Số nhập viện,
  Họ tên, Ngày) có nguy cơ mất khi cuộn nếu không neo.

| ID | Mức | Phát hiện (§9) | Ảnh |
|---|---|---|---|
| EMR-NURS-01 | Cao (cần xác minh lại) | Lưới siêu rộng không có cột neo + tiêu đề dính thấy được; nếu container con không cuộn được thì toàn bộ cột sau "Phản xạ ánh sáng" là không với tới được. Hồ sơ chiến lược: **A** (đối chiếu dọc theo thời gian + ngang giữa các chỉ số là nhiệm vụ chính; cột Số nhập viện/Họ tên/Ngày neo cố định). | `nursingrecord-desktop.png`, `nursingrecord-mobile.png` |
| EMR-NURS-02 | Trung bình | Ngày yyyy-MM-dd thay vì dd/MM/yyyy (§5.1); form lọc nhãn nằm ngang, trên mobile sẽ chật — cần nhãn trên trường (§4.3). | `nursingrecord-desktop.png` |
| EMR-NURS-03 | Thấp | Vùng bệnh nhân trống không có thông báo rỗng; 4 nút primary xanh giống nhau. | `nursingrecord-desktop.png` |

## 5. Chuỗi tiếng Trung còn lại — CHỈ LIỆT KÊ, KHÔNG VIẾT LẠI

| Trang | Vị trí chuỗi lạ |
|---|---|
| Đăng nhập MRMS / OR / EMR (`login-desktop.png`, `login-mobile.png`) | Logo "天天开源" góc trên-trái; footer "Copyright © 2025湖北天天数链技术有限公司…" + tên thỏa thuận "《天天开源软件（社区版）许可协议》" (EMR dịch một phần câu giấy phép sang tiếng Việt nhưng vẫn giữ tên thỏa thuận và tên công ty tiếng Trung). |
| Trang chủ EMR (`home-desktop.png`, `home-mobile.png`) | Footer vendor tiếng Trung đè cuối sidebar (desktop) / góc trên-trái (mobile). |
| Trang chủ MRMS, trang chủ OR, các trang chức năng MrDept/MrFee/OPStaff/OpApply/NursingRecord | Probe không tìm thấy phần tử `<footer>` và không thấy chuỗi tiếng Trung trong thân trang đã chụp. |

Lưu ý: file `grid.locale-cn.js` vẫn được nạp (từ static host) **trước**
`grid.locale-vi.js` của từng app trong `_LayoutIndex.cshtml` — nhưng locale
hiển thị cuối cùng đã là tiếng Việt (xem §4b), nên đây chỉ là file thừa cần
quyết định riêng, không phải lỗi hiển thị.

## 6. Xác nhận §0 / §4 / §10 bằng bằng chứng thật

### 6a. Phiên bản asset static host đang phục vụ (probe trên trang thật + header file qua HTTP)

| Asset | Xác định được | Bằng chứng |
|---|---|---|
| jQuery | ✅ **2.1.1** | `$.fn.jquery` trên trang thật + banner file `http://127.0.0.1:41142/js/jquery/jquery-2.1.1.min.js` |
| jqGrid | ✅ **5.1.0** | `$.jgrid.version` trên trang thật + banner file static host |
| Bootstrap | ✅ **3.3.4** | Banner file `http://127.0.0.1:41142/js/bootstrap/bootstrap.js` ("Bootstrap v3.3.4"); trên trang thật: `$.fn.modal` + `$.fn.tooltip` + `emulateTransitionEnd` đều có (dấu hiệu Bootstrap 3, không phải 5) |
| Bootstrap của SSO portal | ⚠️ Không xác định trong audit này | DESIGN.md §0 nói layout `_Layout` của SSO dùng Bootstrap 5.1.0 — ngoài phạm vi 3 app này, không kiểm chứng |
| jQuery UI / datetimepicker / validate | Chỉ thấy tên file trong danh sách script, không xác định phiên bản | Không có banner/version runtime đọc được — ghi rõ là chưa xác định, không đoán |

Kết luận §0: **đừng dùng class Bootstrap 5** (`data-bs-*`, `d-none d-md-block`)
cho các trang nghiệp vụ — shell nghiệp vụ đang chạy Bootstrap 3.3.4.

### 6b. Locale jqGrid hiển thị cuối cùng: TIẾNG VIỆT ✅

Trên mọi trang lưới (MrDept, MrFee, OPStaff, OpApply, NursingRecord):
`$.jgrid.defaults.recordtext = "Hiển thị {0} - {1} trong tổng số {2} bản ghi"`,
`pgtext = "Trang {0} trên tổng số {1} trang"`,
`emptyrecords = "Không có dữ liệu hiển thị"`, và chân lưới hiển thị
"Hiển thị 1 - 10 trong tổng số 37 bản ghi". Thứ tự nạp: `grid.locale-cn.js`
(static host) → `grid.locale-vi.js` (của từng app) → locale Việt thắng.
**Không có chuỗi tiếng Trung nào trong lưới.**

### 6c. Hành vi sidebar mobile hiện tại và nút mở rộng: KHÔNG ĐẠT ❌

- Trang chức năng mở trực tiếp (MrDept, MrFee, OPStaff, OpApply, NursingRecord)
  là nội dung iframe **không có shell**: không sidebar, không topbar, không nút
  nào để mở menu — người dùng mở deep-link sẽ không điều hướng được nếu không
  qua trang chủ. (Đây là cấu trúc khung, không phải lỗi của từng trang.)
- Trang chủ MRMS/EMR ở mobile 390: **không thấy nút hamburger** trong ảnh và
  trong probe phần tử; sidebar không thành drawer phủ + nền mờ mà chỉ thu
  thành dải hẹp không đọc được; không có nút đóng, không khóa cuộn nền,
  không `aria-label`/`aria-expanded` nào quan sát được. Nút ≡ trên topbar
  desktop có tồn tại (ảnh desktop) nhưng ở mobile nó biến mất cùng với
  tên app/tên người dùng.
- **Chưa xác minh được (do sự cố DB):** hành vi sau khi bấm nút thu gọn "«"
  trên trang chủ mobile — các lần bấm thử ở pass bổ sung đều rơi vào trang
  đăng nhập do mất phiên, nên không tính là bằng chứng hành vi. Cần chạy lại
  sau khi DB hồi phục.
- Kết luận §10: vấn đề "sidebar mobile thiếu nút mở rộng" **vẫn đúng** với
  MRMS/EMR; OR không có sidebar ngay cả ở desktop nên ngoài bảng này.

### 6d. Hồ sơ chiến lược responsive A/B/C cho từng lưới chính

Theo DESIGN.md §4.1 (mặc định A trừ khi nhiệm vụ là tra cứu từng bản ghi):

| Lưới | Nhiệm vụ quan sát được | Chiến lược đề xuất | Lý do |
|---|---|---|---|
| MRMS MrDept (2 lưới đối chiếu) | Đối chiếu mã khoa bệnh án ↔ mã HIS | **A** — cuộn ngang + cột "Mã khoa bệnh án" neo | Cần so sánh ngang 2 lưới cùng lúc |
| MRMS MrFee (phân cấp 3 cấp) | Tra cứu cấu trúc phí theo cấp | **A** — cuộn ngang + cột "Mã nhóm cấp 1" neo | Cấu trúc phân cấp cần nhìn ngang |
| OR OPStaff (danh mục nhân sự) | Danh mục tra cứu | **A** (mặc định); cân nhắc **B** nếu xác nhận nhiệm vụ chính là xem từng người | Hiện tại nén cột làm mất 4/6 cột |
| OR OpApply (lưới bệnh nhân + lưới đơn) | Chọn bệnh nhân rồi lập đơn | **A** cho lưới bệnh nhân; **pane phải xếp dọc dưới pane trái** trên mobile (lỗi bố cục, không phải chọn A/B/C) | Nhiệm vụ theo từng bệnh nhân nhưng vẫn cần đối chiếu danh sách |
| EMR NursingRecord (dấu hiệu sinh tồn) | Theo dõi chỉ số theo thời gian, nhập liệu theo dòng | **A** — cuộn ngang + neo "Số nhập viện / Họ tên / Ngày" + tiêu đề dính | Cần so sánh ngang nhiều chỉ số cùng thời điểm; **B** (thẻ) chỉ phù hợp xem 1 lần đo, không phù hợp nhập loạt |
| Lưới WorkSpace (trang chủ) | — | Không thuộc 3 app này; không kết luận | — |

Trạng thái hiện tại của TẤT CẢ các lưới trên: **chưa áp dụng chiến lược nào** —
jqGrid đang `shrinkToFit` (nén cột cho vừa viewport) hoặc lưới siêu rộng bị
cắt ở mép, không có cột neo, không có nút "Xem thêm"/thẻ chi tiết.

## 7. Đối chiếu nhanh checklist §9 (3 app × các mục chính)

| Mục §9 | MRMS | OR | EMR |
|---|---|---|---|
| 9.1 Topbar cùng vị trí + `<h1>` duy nhất | Topbar có; `<h1>` ❌ | Topbar ❌, `<h1>` ❌ | Topbar có; `<h1>` ❌ |
| 9.2 Sidebar nhóm + active + drawer mobile | Nhóm có; active thấy được (desktop); drawer ❌ | Sidebar ❌ hoàn toàn | Nhóm có; drawer ❌ |
| 9.3 Form (nhãn, bắt buộc, lỗi dưới trường) | Không kiểm chứng submit (audit chỉ đọc); form lọc có nhãn | Như MRMS | Như MRMS; ngày sai định dạng ❌ |
| 9.4 Lưới (công cụ cùng vị trí, chân lưới VI, không tràn) | Công cụ ✅, chân lưới ✅, không tràn ❌ (nén cột) | Công cụ ✅, chân lưới ✅, không tràn ❌ | Công cụ ✅, chân lưới n/a (rỗng), không tràn ❌ (lưới 3.498px) |
| 9.5 Nút (1 primary, danger tách vị trí, 44px mobile) | ❌ (3 primary xanh, Xóa không danger) | ❌ (như MRMS) | ❌ (như MRMS) |
| 9.6 Thông báo (toast/inline/modal, có lối thoát) | Không quan sát được (không submit) | Rỗng dùng đỏ, không lối thoát ❌ | Rỗng không thông báo ❌ |
| 9.7 Mobile (3 viewport, không tràn, chức năng không bị ẩn) | ❌ (KPI bóp, sidebar dải hẹp) | Trang chủ ✅ (do đơn giản); OpApply ❌ (pane đẩy ra ngoài) | ❌ (footer đè, tab chồng, lưới cắt) |
| 9.8 Thị giác + ngôn ngữ (1 font/icon, không tiếng Trung UI) | Lưới ✅ VI; footer/login ❌ | Lưới ✅ VI; login ❌ | Lưới ✅ VI; login + home footer ❌ |
| Chuỗi dấu xếp chồng (§7) | Không thấy cắt dấu trong lưới; nhãn dài bị cắt chữ (không phải cắt dấu) | Như MRMS | Như MRMS |

## 8. Ngoài phạm vi — ghi nhận, không đụng tới

- Mâu thuẫn tiền tố chứng từ: không gặp, không xử lý (theo quy tắc cứng).
- Dữ liệu thật / tên bệnh viện thật / tài khoản demo / secret: không thay đổi.
- `MRHomePage/Index` chết là vấn đề route (không phải UI thuần) — báo cho
  điều phối viên quyết định (sửa menu sang `Main` hay khôi phục `Index`).
- Sự cố SQL Server ngừng phục vụ (~21:00 ICT 2026-10-28): thuộc về vận hành/
  watchdog, không thuộc redesign; audit không tự khởi động lại.

## 9. Việc cần làm tiếp (cho điều phối viên, không phải cho worker audit này)

1. Khôi phục SQL Server (watchdog/recovery) rồi **chạy lại 3 mục còn thiếu**:
   ảnh `MRHomePage/Main` desktop + mobile, hành vi nút thu gọn/hamburger trang
   chủ mobile, khả năng cuộn ngang bên trong lưới NursingRecord.
2. Chặng redesign ưu tiên theo thứ tự: (a) drawer sidebar mobile cho shell
   MRMS/EMR (§3.5) + xếp dọc pane OpApply; (b) chiến lược A cho mọi lưới
   (wrapper cuộn + cột neo + `setGridWidth` khi resize); (c) đưa OR về cùng
   shell với MRMS/EMR; (d) chuẩn hóa nút (1 primary, Xóa = danger tách vị trí);
   (e) trạng thái rỗng theo §6.2; (f) sửa tràn trang đăng nhập; (g) footer
   tiếng Việt dùng chung (đợt chuẩn hóa chuỗi, không sửa lẻ).
3. Không cần worker audit này chạy thêm vòng nào — audit đã dừng đúng phạm vi.
