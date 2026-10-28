# Audit UI/UX — PDS / Base HOSP / Base Web (BÁO CÁO ĐẦY ĐỦ)

> **Phạm vi:** 3 app — PDS (5237), Base HOSP (5233), Base Web (5238). Mỗi app: trang
> đăng nhập + trang chủ + 2–3 trang chức năng chính, ở desktop 1280 + mobile 390×844;
> trang chủ thêm tablet 768. Mobile = giả lập viewport (không có app native).
> **Quy tắc đã tuân thủ:** prod chỉ đọc — chỉ điều hướng + chụp ảnh, KHÔNG submit form,
> KHÔNG ghi DB (kể cả STG); chỉ dùng tài khoản 000000/mật khẩu 000000 (md5 tính bằng
> `md5sum`, không đoán mật khẩu khác); KHÔNG khởi động/dừng app; KHÔNG sửa code,
> KHÔNG commit; footer chữ vendor tiếng Trung CHỈ LIỆT KÊ, không tự viết lại;
> mâu thuẫn receipt-prefix ngoài phạm vi, không đụng tới.
>
> **Ảnh + số đo:** `shots-redesign/pds/`, `shots-redesign/hosp/`,
> `shots-redesign/baseweb/` (thư mục đã gitignore). Số đo: `diag.json` mỗi app +
> `retry-diag.json`, `/tmp/one-*.json`, `/tmp/two-*.json`, `/tmp/three-*.json`
> (bản sao làm việc, không phải deliverable).
>
> **Lưu ý về đợt gián đoạn giữa chừng:** giữa phiên audit, SQL Server local chết
> (mất cổng 1433) khiến đăng nhập thất bại hàng loạt; điều phối viên đã khôi phục
> stack và xác nhận tài khoản 000000 KHÔNG bị khóa (Locked=0), đăng nhập SSO thành
> công. Mọi kết quả dưới đây đều thu được SAU khi stack khôi phục, trừ khi ghi rõ
> khác. Các lần "đăng nhập thất bại" trong đợt gián đoạn được xác định là do SQL
> chết, KHÔNG phải lỗi cấu hình app — không dùng chúng làm kết luận.

## 0. Kết luận tổng thể (đọc trước)

1. **Lỗi chặn duy nhất, và nó là lỗi HỆ THỐNG của cả 3 app (không riêng PDS):**
shell trang chủ 3 app dùng chung một mẫu (`#theme-wrapper` > `#header-navbar` >
`#sidebar-nav` + `.content-tabs` + iframe `.Newtouch_iframe`). Trên mobile,
sidebar bị `display: none` nhưng iframe vẫn bị tính chiều rộng như thể sidebar
còn đó → nội dung chỉ còn ~170px, bóp méo toàn bộ dashboard; đồng thời
**không có nút hamburger nào để mở lại sidebar** — người dùng mobile không điều
hướng được. Đây là vi phạm §3.5/§9.2/§9.7 ở mức chặn.
2. **Không có `<h1>` nào** trên bất kỳ trang nào đã đo (trang chủ, danh sách, form
của cả 3 app) — vi phạm §9.1. Tiêu đề tab trình duyệt của HOSP/Base Web còn là
`Index`/`Form` (tên controller/action mặc định), không phải ngôn ngữ nhiệm vụ.
3. **Lưới jqGrid nào cũng thiếu cột neo và thiếu hồ sơ chiến lược A/B/C:**
cuộn ngang nội bộ có, nhưng cột định danh mất khỏi tầm nhìn khi cuộn trên mobile.
4. **Trạng thái rỗng của lưới bị trình bày như lỗi:** chữ đỏ
"Không tìm thấy dữ liệu liên quan!" + chân phân trang vỡ mẫu
("Trang trên tổng số 1 trang", "Trang 0 trên tổng số trang") — vi phạm §6.2/§9.4.
5. **Điểm tốt, xác nhận được:** locale jqGrid hiển thị cuối cùng là **tiếng Việt**
trên mọi trang có lưới; jQuery 2.1.1 / jqGrid 5.1.0 / Bootstrap 3.3.4 thống nhất;
không có lỗi console nào trên các trang đã đo (trừ 1 lần tải font FontAwesome lỗi
ở PDS tablet, xem §4.5).

## 1. PDS (5237)

### 1.1 Trang đăng nhập (desktop 1280, mobile 390, tablet 768)

- Có `meta viewport` (`width=device-width, initial-scale=1.0`).
- **Lỗi kỹ thuật đo được:** `scrollWidth` 1799 (desktop) / 548 (mobile) vượt viewport,
thủ phạm `div.login-head` (825px desktop). Nguyên nhân: trang login dùng
`body { transform: scale(screen.width/1920)}` + `body.style.width = innerWidth/s`
(hàm `resize()` trong `Views/Login/Index.cshtml`) — body luôn rộng ~1920px rồi thu
nhỏ bằng scale. Người dùng không thấy thanh cuộn ngang (body `overflow: hidden`)
nhưng toàn trang bị thu nhỏ theo tỉ lệ màn hình — đúng kỹ thuật mà DESIGN.md §4.1
cấm ("không bao giờ dùng `transform: scale()` để vừa màn hình").
- Ảnh `home-desktop-login.png`: tiêu đề tab "Chào mừng đăng nhập" xuống dòng, dòng
"đăng nhập" đè lên mép trên ô nhập tài khoản (chồng lấn header/form, lỗi thị giác
nhẹ).
- Footer tiếng Trung (liệt kê): `Copyright © 2025 湖北天天数链技术有限公司`,
`《天天开源软件（Bản cộng đồng）Thỏa thuận cấp phép》`.

### 1.2 Trang chủ Home/Index (desktop 1280, mobile 390, tablet 768) — PHÁT HIỆN NẶNG NHẤT

- **Layout:** KHÔNG dùng `_LayoutIndex`. `Views/Home/Index.cshtml` đặt
`Layout = null`, tự mang shell riêng trong trang: `#theme-wrapper` >
`#header-navbar` > `#sidebar-nav` (`collapse navbar-collapse navbar-ex1-collapse`)
+ `.content-tabs` (tab bar, `.menuTabs` rộng 100000px) + iframe `.Newtouch_iframe`
(`Home/Default` = dashboard, `Home/About`).
- **(a) Chẩn đoán mobile theo đúng thứ tự DESIGN.md §4.3 (bằng chứng đo được):**
1. **Meta viewport: KHÔNG CÓ** trên `Home/Index` (`metaViewport = null` ở cả 3
viewport; chỉ trang login mới có). Nguyên nhân gốc số 1.
2. **Chiều rộng cố định:** JS của shell gán chiều rộng bằng pixel theo viewport lúc
tải trang (`#theme-wrapper`, `#header-navbar`, `.container` = đúng số px viewport;
desktop `#sidebar-nav` = 179,2px).
3. **Wrapper cuộn:** dashboard nằm trong iframe, không có wrapper cuộn ngang nào
cứu được khi iframe bị bóp.
4. **Sidebar/iframe ép chiều rộng (nguyên nhân trực tiếp gây bóp méo):** trên
mobile `#sidebar-nav` bị `display: none` (Bootstrap collapse) NHƯNG iframe vẫn
bị tính chiều rộng như thể sidebar còn đó: đo được iframe `.Newtouch_iframe`
`x = 208, w = 170` trên viewport 390 (desktop: `x = 189, w = 1081`).
Dashboard `Home/Default` bên trong chỉ còn 170px để vẽ: 4 thẻ KPI
("Số chờ xử lý 0", "Thuốc điều giá 0", "Duyệt nhập kho 0",
"Đơn ngoại trú chờ phát 216") bị cắt số, biểu đồ "Thống kê phát thuốc (lần)",
"Thống kê loại thuốc năm nay" bị nén nát, nhãn trục cắt vụn
(xem `home-mobile.png`). `document.scrollWidth` vẫn = 390 vì
`html/body { overflow-x: hidden}` che tràn — **không tràn viewport nhưng nội
dung bị bóp méo**, đúng như mô tả "trang chủ PDS mobile bị bóp méo".
5. Dải trái tối trên ảnh mobile chứa footer bản quyền bị ép thành cột hẹp, chữ
xếp dọc khó đọc; tab bar tách 2 hàng (tab "Trang chủ chào mừng" y=101, nút
cuộn tab y=55–101).
- **(d) Sidebar mobile: KHÔNG MỞ ĐƯỢC — lỗi chặn §3.5/§9.2.** Trên mobile không tìm
thấy nút hamburger nào (đã probe `.sidebar-toggle`, `[data-toggle=offcanvas]`,
`.navbar-toggle`, `[data-widget=pushmenu]` — đều không có). Nút duy nhất liên
quan là `#make-small-nav.btn.btnshrink` — chỉ hiện trên desktop, trên mobile
biến mất khỏi topbar cùng với logo. `#sidebar-nav display: none`, không drawer,
không overlay, không nút đóng. Ảnh minh chứng: `home-mobile.png`,
`home-mobile-sidebar.png` (sau khi thử bấm nút mở rộng: trạng thái không đổi).
- Desktop: sidebar 179,2px; tab bar có nút cuộn trái/phải + "Thao tác tab" +
fullscreen, hoạt động bình thường; topbar có tên user
`000000`, "Danh sách tin nhắn 0" (badge không ghi rõ
là số gì — vi phạm nhẹ §9.2).
- **Không có `<h1>`** (§9.1). Footer tiếng Trung: có (cùng 2 chuỗi như trang login).
- KPI dashboard: các thẻ chỉ có con số trơ trọi, không có điểm so sánh
(kỳ trước/mục tiêu) — vi phạm §6.1.

### 1.3 Trang PDS-03 — `/BillManage/OutOrInStorageBill/Form` (desktop + mobile)

- Form nhập kho, lưới chi tiết `gridList`. Chỉ xem, không submit.
- Lưới rộng **1549px** trong khung 1278px (desktop) / 388px (mobile):
`bdivScrollW = 1549 > bdivClientW` — lưới tự cuộn ngang BÊN TRONG `.ui-jqgrid-bdiv`
(cuộn ngang lành mạnh theo §4.2: cột không bị bóp), NHƯNG **không có cột neo cố
định** và **không có chiến lược A/B/C nào được khai báo**; trên mobile, cuộn ngang
1549px trong khung 390px khiến cột định danh (Thao tác, Tên thuốc) mất khỏi tầm
nhìn khi cuộn sang phải — theo §4.2 đây là cuộn ngang làm mất cột định danh = lỗi.
- 16+ cột: Thao tác, *Tên thuốc, Loại thuốc, *Số lượng, Đơn vị, *Tổng giá nhập,
*Số lô, Hóa đơn, Hạn sử dụng, Ngày lập hóa đơn, Giá bán buôn, Tổng giá bán buôn,
Đơn giá lẻ, Tổng tiền bán lẻ, Tỷ lệ chiết khấu, Quy cách… Không có nút
"Xem thêm"/"hiện cột" trên mobile (§9.4).
- Không có chữ tiếng Trung. Không có `<h1>`.
- **(e) Hồ sơ lưới:** nhiệm vụ là nhập từng dòng chứng từ → ứng viên **B**
(dòng → thẻ, giữ đủ trường theo cùng thứ tự nhãn desktop) là phù hợp nhất;
phương án dự phòng **A + frozen column** cho cột Thao tác/Tên thuốc.
- Ảnh: `pds03-form-desktop.png`, `pds03-form-mobile.png`.

### 1.4 Trang kho — `/StorageManage/DrugStockSearch/StockQuery` (desktop + mobile)

- Lưu ý route (để chặng redesign khỏi đi nhầm): StockQuery KHÔNG ở
`/PharmacyDrugStorage/StockQuery` (trả 404; view tồn tại nhưng controller nằm ở
area `StorageManage`, `DrugStockSearchController.StockQuery`). Route đúng:
`/StorageManage/DrugStockSearch/StockQuery`.
- Desktop: 2 lưới jqGrid, `gridW ≈ 1278–1280`, `bdivScrollW ≈ bdivClientW` (vừa
khung); tiêu đề tab "Kho thuốc - Nhà thuốc: Tra cứu lượng tồn kho"; điều kiện tra
cứu (Từ khóa, Trạng thái tồn kho, Trạng thái khoa này, Không hiển thị tồn kho
bằng 0); "Tổng giá lẻ：0.00đ" (dấu `：` toàn giác — cần thống nhất định dạng §5.1).
Không có chữ tiếng Trung.
- Mobile (`stockquery-mobile.png` — bằng chứng trực quan rõ nhất cho lỗi form/lưới):
- Panel lọc là hàng bảng (table row) không xuống dòng: ô "Từ khóa" + nhãn
"Trạng thái tồn kho" bị cắt ở mép phải, checkbox "Không hiển thị tồn kho bằng 0"
không nhìn thấy — vi phạm §9.3/§4.3 (form mobile phải một cột, nhãn trên trường).
- Lưới 1: bảng **1217px** trong khung 386px; lưới 2: **1037px** trong khung 388px.
Chỉ thấy ~3 cột đầu (Tên thuốc, Mã BHYT quốc gia, Mã thuốc…), không có cột neo,
không có dấu hiệu nào cho biết còn cột bên phải ngoài mép cột bị cắt.
- Trạng thái rỗng: chữ **đỏ** "Không tìm thấy dữ liệu liên quan!" — rỗng không
phải lỗi, dùng màu đỏ là vi phạm §6.2 (xem thêm §4.5).
- Chân phân trang: "Trang 0 trên tổng số trang" — mẫu câu vỡ (thiếu số trang,
xem §4.5).
- Nút nổi "‹" màu xám ở góc dưới-phải đè lên vùng phân trang trên mobile.
- **(e) Hồ sơ lưới:** nhiệm vụ tra cứu tồn kho, so sánh ngang nhiều dòng →
chiến lược **A** (cuộn ngang + cột neo cố định cho cột Tên thuốc + tiêu đề dính).
- Ảnh: `stockquery-desktop.png`, `stockquery-mobile.png`.

## 2. Base HOSP (5233)

### 2.1 Trang đăng nhập (desktop 1280, mobile 390, tablet 768)

- Có meta viewport; cùng mẫu scale-body như PDS (`scrollWidth` 1799 desktop /
548 mobile / 1079 tablet; thủ phạm `div.login-head` 825px) — cùng lỗi §4.1.
- Footer tiếng Trung (liệt kê): `Copyright © 2025 湖北天天数链技术有限公司`,
`《天天开源软件（Bản cộng đồng）Thỏa thuận cấp phép》`
(bản home viết "Thỏa thuận cấp phép phần mềm 天天开源 (bản cộng đồng)" — câu chữ
footer KHÔNG thống nhất giữa trang login và trang chủ, vi phạm §3.1).

### 2.2 Trang chủ Home/Index (desktop 1280, mobile 390, tablet 768)

- Cùng shell như PDS (không meta viewport, không `<h1>`, `.menuTabs` 100000px).
- Mobile: cùng lỗi bóp méo hệ thống — `.row` 396px, `#content-wrapper` 341px
(vượt 390 một chút, bị `overflow-x: hidden` che), iframe dashboard bị bóp,
**không có hamburger** (probe `btnFound = false`, ảnh `home-mobile-sidebar.png`
không đổi trạng thái). Footer tiếng Trung có mặt.
- Desktop: topbar "Benh vien Newtouch HIS", user 000000, tab "Thao tác tab".
- Tablet 768: đo được `scrollW = 768`, không tràn (ảnh `home-tablet.png` từ đợt chạy
trước khi `diag.json` bị ghi đè; số đo tablet lấy từ dòng log đợt đó).

### 2.3 Danh sách phân loại khoản thu — `/ChargeCategory/Index` (desktop + mobile)

- Lưu ý route: KHÔNG phải `/Settlement/ChargeCategory/Index` (404 — thư mục
`Controllers/Settlement/` không tạo segment route; namespace controller là
`Controllers`). Route đúng: `/ChargeCategory/Index`.
- Tiêu đề tab trình duyệt chỉ là `Index` (vi phạm §9.1: tiêu đề phải là ngôn ngữ
nhiệm vụ, trùng nhãn menu). Không có `<h1>`. Không có chữ tiếng Trung.
- Thanh công cụ (§9.4/§9.5 — 3 lỗi trên một hàng):
- Hai nút **cùng màu primary xanh** đặt cạnh nhau: "＋ Tạo mới" và "Sửa" —
vi phạm "một nút chính duy nhất mỗi vùng thao tác".
- Nút "Sửa" đang **bật dù chưa chọn dòng nào** — vi phạm §9.4 (thao tác trên
dòng phải vô hiệu hóa khi chưa chọn dòng).
- Ô lọc "Tên" + dropdown "Hiệu lực" bị cắt ở mép phải trên mobile.
- Lưới 14 cột (Tên, Mã, Cờ ngoại trú/nội trú, Loại nhóm lớn, Phân loại sở thuộc,
Thuế suất, Hiệu lực, Người tạo, Thời gian tạo, Sửa cuối, Thời gian sửa cuối,
Thứ tự…): desktop bảng 1288px trong khung 1278px (vừa, cuộn nội bộ 10px);
mobile bảng 1288px trong khung 388px, không cột neo. Có dữ liệu thật
(21+ dòng: "Phi kham benh 00000097", "Phi tiem 00000096"…), dòng đọc rõ,
chiều cao dòng đủ chạm.
- **(e) Hồ sơ lưới:** nhiệm vụ duyệt/tra cứu danh mục → chiến lược **A**
(cuộn ngang + frozen cột Tên; trên mobile thu gọn thanh phân trang).
- Ảnh: `chargecategory-index-desktop.png`, `chargecategory-index-mobile.png`.

### 2.4 Form tạo phân loại khoản thu — `/ChargeCategory/Form` (desktop + mobile)

- Route đúng: `/ChargeCategory/Form`. Tiêu đề tab chỉ là `Form`. Không `<h1>`.
Meta viewport chỉ có `width=device-width` (thiếu `initial-scale=1.0`).
- Trường bắt buộc đánh dấu `*` thống nhất (Tổ chức, Tên, Mã, Mã đầu chữ,
Loại nhóm lớn, Cờ ngoại trú/nội trú); có nhóm "Thông tin cơ bản".
- **Mobile (`chargecategory-form-mobile.png` — lỗi nhãn):**
- Form giữ bố cục nhãn-trái/trường-phải trên 390px; nhãn dài bị cắt giữa chừng:
"Nhóm lớn báo cáo n…", "Nhóm lớn quyết toá…", "Nhóm lớn báo cáo c…" —
vi phạm §9.3 (mobile: nhãn trên trường, một cột) và §7 (không cắt nhãn bằng
dấu ba chấm mù quáng).
- Đo được `.container` rộng **420px**, `.nav-tabs`/`tab-content` chạm tới
x = 415 trên viewport 390 — tức vùng form tràn 25–40px nhưng bị
`overflow-x: hidden` che nên không thấy thanh cuộn; phần bị che chính là
các nhãn dài.
- Dòng ví dụ "Ví dụ quy đổi thuế suất:30%Cần nhập0.3" dính chữ, thiếu khoảng
trắng (lỗi nội dung nhẹ).
- "Trạng thái: [✓] Hiệu lực" hiển thị rõ.
- Không có chữ tiếng Trung. Không có nút "Quay lại" về danh sách nhìn thấy
(§3.3).
- Ảnh: `chargecategory-form-desktop.png`, `chargecategory-form-mobile.png`.

## 3. Base Web (5238)

### 3.1 Trang đăng nhập (desktop 1280, mobile 390, tablet 768)

- Có meta viewport; cùng mẫu scale-body (desktop `scrollW` 1280 nhưng
`div.login-head` 1160px là phần tử tràn; mobile 390).
- Footer tiếng Trung (liệt kê): chỉ còn chuỗi ngắn `天天开源`
("Mã nguồn hệ thống này theo giấy phép của 天天开源") — **ít hơn HOSP/PDS**,
tức footer KHÔNG dùng chung một chuỗi ở mọi app (vi phạm §3.1/§9.1).

### 3.2 Trang chủ Home/Index (desktop 1280, mobile 390, tablet 768)

- Cùng shell như PDS/HOSP (không meta viewport, không `<h1>`, `.menuTabs`
100000px, `#make-small-nav` chỉ hiện trên desktop).
- Desktop (`home-desktop.png` — ảnh tốt nhất cho dashboard Base Web):
- Topbar: logo là **ẢNH chứa chữ tiếng Trung "新致医疗云HIS"** (probe text không
bắt được vì là ảnh — liệt kê ở đây, không tự viết lại); user 000000; nút
hamburger thu gọn sidebar (`#make-small-nav`) hiện ở desktop.
- Sidebar nhóm theo luồng: "Quản trị hệ thống", "Quản lý từ điển", "Tổ chức",
"Dữ liệu cơ bản" — đúng §3.2 (nhóm theo nghiệp vụ, không liệt kê phẳng).
- 5 thẻ KPI một hàng ("Ghi nợ ngoại trú", "Hoàn phí ngoại trú",
"Ghi nợ nội trú", "Thực hiện ghi nợ nội trú", "Hoàn phí nội trú"):
chỉ có con số trơ trọi, **không có điểm so sánh** (vi phạm §6.1);
chữ thẻ đè lên icon trang trí nền (lỗi thị giác nhẹ).
- Biểu đồ "Số lượt khám" (tháng 1–12, toàn 0) và "Thống kê loại khám"
(0 người Ngoại trú / 0 người Nội trú): biểu đồ không có dữ liệu nhưng vẫn vẽ
trục trống, không có trạng thái rỗng + hành động tiếp theo (vi phạm §6.2).
- Mobile (`home-mobile.png`): cùng lỗi bóp méo hệ thống như PDS — iframe ~170px,
5 thẻ KPI chỉ còn chữ cái đầu ("G…", "H…"), biểu đồ nát, chú thích đè nhau;
**không có hamburger** (`btnFound = false`).
- Tablet 768: sidebar hiện ("000000 Quản trị hệ thống Quản lý từ điển Tổ chức
Dữ liệu cơ bản Trang chủ Thao tác tab"); có phần tử `#ajax-loader` rộng 1536px
(phần tử ẩn, không ảnh hưởng thị giác nhưng là rác đo được).
- Ảnh: `home-desktop.png`, `home-mobile.png`, `home-tablet.png`.

### 3.3 Cấu hình toàn cục — `/SysGlobalConfig/Index` (desktop + mobile)

- Lưu ý route: KHÔNG phải `/Sys/SysGlobalConfig/Index` (404). Route đúng:
`/SysGlobalConfig/Index`.
- Tiêu đề tab chỉ là `Index`. Không có `<h1>`. Không có chữ tiếng Trung.
- Lưới 8 cột (Tên, Mã, Giá trị, Mô tả, Thứ tự, Hiệu lực, Thời gian tạo,
Thời gian sửa cuối): desktop bảng 1259px trong khung 1278px (vừa, không tràn);
mobile bảng **781px** trong khung 388px, header 801px — không cột neo.
- Thanh công cụ: "Tạo mới", "Sửa" (cùng mẫu 2 nút xanh như HOSP — xem §2.3).
- **Lưới RỖNG (không có dòng cấu hình nào):** hiển thị chữ **đỏ**
"Không tìm thấy dữ liệu liên quan!" + chân phân trang vỡ mẫu
"Trang trên tổng số 1 trang" — cùng 2 lỗi §6.2/§9.4 như StockQuery
(rỗng trình bày như lỗi; không có câu "Đang xem x–y trên tổng z dòng" bằng
tiếng Việt; không có hành động tiếp theo "Tạo mới" ngay tại trạng thái rỗng
dù nút Tạo mới nằm ở toolbar trên).
- **(e) Hồ sơ lưới:** danh sách cấu hình, tra cứu nhanh → chiến lược **A**
(8 cột, frozen cột Tên; mobile có thể cân nhắc C vì chỉ cần kiểm tra trạng
thái nhanh, nhưng A an toàn hơn do có cột Giá trị cần đối chiếu).
- Ảnh: `sysglobalconfig-index-desktop.png`, `sysglobalconfig-index-mobile.png`.

### 3.4 Form tạo khoa — `/Organize/Form` (desktop + mobile)

- Lưu ý route: KHÔNG phải `/Sys/Organize/Form` (404). Route đúng: `/Organize/Form`.
- Tiêu đề tab chỉ là `Form`. Không có `<h1>`. Không có chữ tiếng Trung.
- Trường: *Tên, Tên viết tắt, *Mã, *Phân loại (dropdown), Email, *Điện thoại,
**WeChat** (nhãn không dịch — vi phạm §9.8 ngôn ngữ thống nhất), Fax, Thứ tự,
Tùy chọn [✓] Hiệu lực, *Địa chỉ, Giới thiệu. Dấu `*` bắt buộc thống nhất.
- **Mobile (`organize-form-mobile.png` — vi phạm §9.3 rõ nhất):** form giữ
**hai cột** trên 390px (cặp trường trái/phải chen nhau); dropdown Phân loại chỉ
hiện "==Vui l…"; checkbox "Hiệu lực" rơi xuống dòng dưới nhãn "Tùy chọn"
(căn lề lệch). Chỉ 2 trường cuối (Địa chỉ, Giới thiệu) là toàn chiều rộng.
- Ảnh: `organize-form-desktop.png`, `organize-form-mobile.png`.

## 4. Xác nhận §0/§4/§10 bằng bằng chứng thật (cho cả 3 app)

### 4.1 (b) Phiên bản asset static host đang phục vụ

Probe JS trên trang thật (không suy từ tên file):

| Asset | Kết quả | Trang đo được |
|---|---|---|
| jQuery | **2.1.1** (xác định được) | Mọi trang (login, home, danh sách, form) |
| jqGrid | **5.1.0** (xác định được) | Mọi trang có lưới (PDS-03, StockQuery, ChargeCategory, SysGlobalConfig) |
| Bootstrap | **3.3.4** (xác định được qua `$.fn.modal.Constructor.VERSION`) | Mọi trang có lưới + trang chủ sau đăng nhập; trên trang login thì `bsModal = false` (modal chưa nạp) nên **không xác định được từ trang login** — không phải bằng chứng "không dùng Bootstrap" |
| Font Awesome | Không xác định được phiên bản từ JS; có 1 lần console báo `fontawesome-webfont.woff2` download failed (status 2152398850) trên PDS tablet — icon có thể mất ở lần tải đó | PDS home tablet |

Hệ quả cho chặng redesign (khớp DESIGN.md §0): **không được copy class Bootstrap 5**
(`data-bs-*`, `d-none d-md-block`…) sang các trang nghiệp vụ — shell đang chạy
Bootstrap 3.3.4 + jQuery 2.1.1 + jqGrid 5.1.0. Class kiểu Bootstrap 3
(`hidden-xs`, `nav nav-tabs`, `col-*-*`) mới là class đang chạy thật.

### 4.2 (c) Locale jqGrid hiển thị cuối cùng: TIẾNG VIỆT — xác nhận trên cả 3 app

Trên mọi trang có lưới: `recordtext = "Hiển thị {0} - {1} trong tổng số {2} bản ghi"`,
`emptyrecords = "Không có dữ liệu hiển thị"` — tức `grid.locale-vi.js` nạp sau đã
thắng `grid.locale-cn.js` nạp trước, đúng như DESIGN.md §0 mô tả. File locale-cn
thừa vẫn tồn tại trong layout nhưng không quyết định chuỗi hiển thị (chặng triển
khai xử lý file thừa theo quyết định riêng, không xóa bừa — theo §9.8).

### 4.3 (d) Hành vi sidebar mobile hiện tại và nút mở rộng

- **Không có nút mở rộng nào trên mobile ở cả 3 app, mọi trang** (home, danh sách,
form): probe tìm mọi selector hamburger đều trống; thử bấm nút mở rộng (nếu có)
không đổi trạng thái (`home-*-sidebar.png` giống ảnh thường).
- Cơ chế collapse hiện có: desktop dùng `#make-small-nav.btn.btnshrink` (thu gọn
sidebar, chỉ hiện ≥ 992px) + Bootstrap `collapse` cho `#sidebar-nav`
(ẩn hẳn dưới ngưỡng); **không có drawer/overlay/khóa cuộn nền/focus trả về**
theo §3.5 ở bất kỳ app nào.
- Trạng thái active của menu cha khi ở trang con: chưa đo được (các trang chức năng
được mở bằng URL trực tiếp, không qua click menu trong shell) — chặng redesign
cần đo bổ sung bằng luồng click menu thật.

### 4.4 (e) Hồ sơ chiến lược responsive A/B/C cho từng lưới chính

| Lưới | Nhiệm vụ | Chiến lược đề xuất | Lý do |
|---|---|---|---|
| PDS-03 `gridList` (chi tiết phiếu nhập kho) | Nhập từng dòng chứng từ | **B** (dự phòng A + frozen) | Làm việc từng bản ghi, không so sánh ngang |
| PDS StockQuery `gridYPZL` + lưới chi tiết | Tra cứu tồn kho, đối chiếu | **A** | Cần so sánh ngang nhiều dòng/cột |
| HOSP ChargeCategory `gridList` | Duyệt/tra cứu danh mục thu phí | **A** | So sánh ngang, 14 cột |
| Base Web SysGlobalConfig `gridList` | Tra cứu cấu hình | **A** (cân nhắc C) | 8 cột, cần đối chiếu Giá trị; C chỉ nếu mobile chỉ kiểm tra trạng thái nhanh |
| Hiện trạng chung | — | **Không chiến lược nào** | Cuộn ngang nội bộ có, cột neo không, thẻ chi tiết không |

### 4.5 Trạng thái rỗng / phân trang / console (phát hiện chung, §6.2/§9.4)

- Chuỗi rỗng tùy chỉnh "Không tìm thấy dữ liệu liên quan!" (màu đỏ) xuất hiện ở
StockQuery và SysGlobalConfig khi lưới rỗng, **khác** với chuỗi mặc định của
locale Việt ("Không có dữ liệu hiển thị") — tức có code riêng ghi đè trạng thái
rỗng, và nó dùng màu lỗi cho trường hợp không phải lỗi (§6.2: rỗng ≠ lỗi).
- Chân phân trang khi rỗng vỡ mẫu: "Trang trên tổng số 1 trang"
(SysGlobalConfig desktop) và "Trang 0 trên tổng số trang" (StockQuery mobile) —
không đạt §9.4 ("Đang xem x–y trên tổng z dòng" bằng tiếng Việt).
- Console: 0 lỗi trên mọi trang đã đo, trừ 1 lần tải font FontAwesome lỗi ở PDS
tablet (xem §4.1). Không có lỗi JS nào trên các form/lưới đã đo.

### 4.6 Các vấn đề đã biết trong DESIGN.md §10 — đối chiếu cuối

| Vấn đề §10 | Kết luận audit |
|---|---|
| Trang chủ PDS mobile bóp méo | **Xác nhận + chẩn đoán xong (§1.2):** thiếu meta viewport → chiều rộng pixel → iframe bị ép 170px; và **lỗi này có ở cả HOSP và Base Web** (cùng shell), không riêng PDS |
| Sidebar mobile thiếu nút mở rộng | **Xác nhận ở cả 3 app, mọi trang (§4.3)** — lỗi chặn |
| Bảng WorkSpace cuộn ngang | Phân biệt xong: cuộn ngang nội bộ của jqGrid là lành mạnh; lỗi là **mất cột định danh + mất tiêu đề khi cuộn** và **iframe dashboard bị bóp** (không phải cuộn ngang của lưới) |
| Footer tiếng Trung | Danh sách đầy đủ (chỉ liệt kê): PDS login+home (2 chuỗi dài); HOSP login+home (2 chuỗi, câu chữ không thống nhất login vs home); Base Web login (chuỗi ngắn `天天开源`); Base Web home: logo ẢNH "新致医疗云HIS". **Không thống nhất footer giữa các app** |

## 5. Checklist §9 — bảng tổng hợp (✗ = chưa đạt, ◐ = một phần, ✓ = đạt, — = chưa đo)

| Mục §9 | PDS | HOSP | Base Web |
|---|---|---|---|
| 9.1 Topbar cùng vị trí (logo/tên app/user/đăng xuất) | ◐ (desktop đạt; mobile mất logo + nút thu gọn) | ◐ (như PDS) | ◐ (như PDS; logo là ảnh chữ Trung) |
| 9.1 `<h1>` duy nhất trùng nhãn menu | ✗ (không có h1) | ✗ (không có h1; tab title `Index`/`Form`) | ✗ (không có h1; tab title `Index`/`Form`) |
| 9.1 Không chữ tiếng Trung header/footer | ✗ (footer vendor) | ✗ (footer vendor) | ✗ (footer + logo ảnh) |
| 9.2 Sidebar mobile drawer + nút mở | ✗ lỗi chặn | ✗ lỗi chặn | ✗ lỗi chặn |
| 9.2 Menu nhóm theo luồng, active rõ | ◐ (nhóm có; active trang con chưa đo) | — (trang con mở bằng URL) | ✓ nhóm (desktop); active trang con chưa đo |
| 9.3 Form: nhãn hiện, `*` thống nhất, lỗi dưới trường | ◐ (PDS-03 đạt nhãn/`*`; HOSP form nhãn bị cắt mobile) | ✗ mobile (nhãn cắt, container 420px) | ✗ mobile (giữ 2 cột, select cắt) |
| 9.3 Mobile: nhãn trên trường, một cột | ✗ (StockQuery lọc cắt; PDS-03 lưới) | ✗ (Form giữ nhãn-trái) | ✗ (Organize giữ 2 cột) |
| 9.4 Toolbar cùng vị trí/thứ tự | ◐ (lưới có toolbar; thứ tự chưa thống nhất hẳn) | ✗ (2 nút primary + Sửa bật khi chưa chọn) | ✗ (2 nút xanh như HOSP) |
| 9.4 Lọc đang áp dụng + nút Xóa lọc | — (không bấm lọc — audit chỉ đọc) | — | — |
| 9.4 Chân lưới "Đang xem x–y trên tổng z" tiếng Việt | ✗ khi rỗng (mẫu vỡ) | ✗ khi rỗng (chưa đo lưới rỗng HOSP) | ✗ khi rỗng (mẫu vỡ) |
| 9.4 Lưới không tràn viewport + cột neo | ✗ (cuộn nội bộ có, neo không) | ✗ (như PDS) | ✗ (như PDS) |
| 9.4 Hồ sơ A/B/C từng lưới | Đề xuất ở §4.4 (hiện chưa có) | Đề xuất ở §4.4 | Đề xuất ở §4.4 |
| 9.4 Trạng thái tải/rỗng/lỗi theo §6.2 | ✗ (rỗng màu đỏ) | — | ✗ (rỗng màu đỏ) |
| 9.5 Một nút chính/vùng; icon đơn độc có nhãn | ◐ (form PDS-03 chưa đo nút submit — không submit) | ✗ (2 primary + Sửa không vô hiệu) | ✗ (2 nút xanh) |
| 9.6 Thông báo có lối thoát, không `alert()` mặc định | — (không kích hoạt thông báo — audit chỉ đọc) | — | — |
| 9.7 Ba viewport | ✓ home; ✓ form/kho (desktop+mobile) | ✓ home; ✓ Index/Form (desktop+mobile) | ✓ đủ |
| 9.7 Chức năng quan trọng không bị ẩn vì mobile | ✗ (sidebar/menu bị ẩn hẳn, không mở được) | ✗ (như PDS) | ✗ (như PDS) |
| 9.8 Một font/icon/token; không chuỗi Trung | ✗ (footer; woff2 có lần lỗi) | ✗ (footer) | ✗ (footer + logo + nhãn WeChat) |
| 9.8 Ngôn ngữ nhiệm vụ thống nhất | ◐ (PDS tốt hơn; HOSP/Base Web title `Index`/`Form`) | ✗ (title) | ✗ (title + WeChat) |

Ghi chú trung thực về giới hạn đo: audit chỉ đọc (không submit, không bấm lọc/sắp
xếp/xóa) nên các mục §9.4 (lọc đang áp dụng), §9.6 (thông báo/xác nhận) và trạng
thái active menu trang con chưa đo được — chặng redesign cần đo bổ sung bằng luồng
click thật. Chuỗi kiểm chứng dấu xếp chồng §7 ("Ủy quyền", "Bệnh nhân nữ", "Xử lý",
"Ứng trước", "Ổn định", "Dữ liệu") chưa được quét chuyên biệt; chữ tiếng Việt có
dấu trong mọi ảnh đã xem đều render nguyên vẹn, không thấy cắt dấu.

## 6. Ghi chú về độ tin cậy của phép đo (để điều phối viên không đọc sai số liệu)

- **Login AJAX chậm khi tải nặng:** một số context đăng nhập xong vẫn ở trang login
sau 5s chờ cố định (thông báo "Phiên đăng nhập đã hết hạn") — nguyên nhân là probe
chuyển trang trước khi request login hoàn tất, KHÔNG phải lỗi đăng nhập của app.
Sau khi đổi probe sang chờ redirect tới `/Home/Index` tối đa 30s, mọi lần đăng
nhập đều thành công. Nút login có trạng thái "loading..." nhưng không có phản hồi
timeout khi chậm — ghi nhận nhẹ theo §6.2/§9.5, không tính là lỗi chặn.
- **Screenshot Playwright timeout 30s** xảy ra 2 lần (Base Web home desktop,
SysGlobalConfig desktop) trong lúc máy tải nặng (full dotnet stack + Firefox) —
đã khắc phục bằng cách chụp lại với timeout 60s và đo diag trước khi chụp.
Các ảnh cuối cùng đều là ảnh thật của đúng trang, không phải ảnh trang login
(đã kiểm tra bằng mắt: trừ các ảnh `*-login.png` vốn là trang login).
- **Tràn viewport bị che:** nhiều trang đặt `html/body { overflow-x: hidden}` nên
`document.scrollWidth` vẫn bằng viewport dù phần tử con vượt ra ngoài
(ví dụ HOSP Form `.container` 420px trên viewport 390). Vì vậy báo cáo này ghi
cả hai số: `scrollWidth` và danh sách phần tử vượt mép (`offenders`) — chỉ nhìn
`scrollWidth` sẽ kết luận sai là "không tràn".
- **Route:** các 404 gặp ban đầu (`/Settlement/ChargeCategory/...`,
`/Sys/SysGlobalConfig/...`, `/Sys/Organize/...`, `/PharmacyDrugStorage/StockQuery`)
đều là do audit đoán sai route (thư mục controller không tạo segment route),
KHÔNG phải menu chết — route đúng đã ghi ở từng mục.

## 7. Danh sách ảnh minh chứng (theo app × trang × viewport)

- PDS: `home-desktop.png`, `home-mobile.png`, `home-tablet.png`,
`pds03-form-desktop.png`, `pds03-form-mobile.png`, `stockquery-desktop.png`,
`stockquery-mobile.png` (+ `*-login.png`, `home-mobile-sidebar.png`,
`pds03-form-mobile-sidebar.png`).
- HOSP: `home-desktop.png`, `home-mobile.png`, `home-tablet.png`,
`chargecategory-index-desktop.png`, `chargecategory-index-mobile.png`,
`chargecategory-form-desktop.png`, `chargecategory-form-mobile.png`
(+ `*-login.png`, `*-sidebar.png`).
- Base Web: `home-desktop.png`, `home-mobile.png`, `home-tablet.png`,
`sysglobalconfig-index-desktop.png`, `sysglobalconfig-index-mobile.png`,
`organize-form-desktop.png`, `organize-form-mobile.png`
(+ `*-login.png`, `*-sidebar.png`).
