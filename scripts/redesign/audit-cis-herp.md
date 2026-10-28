# Audit UI/UX — CIS (5234) + Herp (5236)

> Worker AUDIT, chương trình thiết kế lại OpenHIS. **Chỉ audit — không sửa code,
> không commit, không chuyển sang chặng redesign.**
> Phương pháp: Playwright + Firefox local, đăng nhập tài khoản duy nhất `000000`
> (mật khẩu điền plaintext, trang tự md5 ở client như code gốc).
> **Mobile = giả lập viewport, KHÔNG phải app native.**
> Prod chỉ đọc: chỉ điều hướng + chụp ảnh, không submit form nghiệp vụ,
> không ghi database (kể cả STG), không khởi động/dừng app, không sửa file app.
> Mâu thuẫn receipt-prefix ngoài phạm vi — không đụng tới.

## 0. Kết quả tổng quan — đọc trước

### 0.1. CIS: audit được đầy đủ (trong lúc đăng nhập còn hoạt động)

| Trang | Desktop 1280 | Mobile 390×844 | Tablet 768 |
|---|---|---|---|
| Đăng nhập (`/Login/Index`) | ✅ chụp, có lỗi tràn + footer lỗi | ✅ chụp, nền bị cắt | — |
| Trang chủ (`/Home/Index`) | ✅ chụp — **là trang stub, không phải shell thật** | ✅ chụp | ✅ chụp, không tràn |
| Trạm làm việc bác sĩ (`/MainBusiness/Index`) | ✅ chụp — tab + banner, **lưới trống** | ✅ chụp — tab bẻ cột, banner bị che | — |
| Lưới chờ khám (`/PatientList/Index` → redirect `/PatientList/PatientList`) | ✅ chụp — **chỉ có thanh lọc, không có lưới** | ✅ chụp — **tràn viewport 209px** | — |
| Truyền dịch (`/InfusionManage/query`) | ✅ chụp — trang mảnh `_mobileIndex` | ✅ chụp — chữ nhỏ, gần như trống | — |

### 0.2. Herp: ĐÃ AUDIT LẠI THÀNH CÔNG sau khi stack khôi phục (bổ sung 21:0x ICT)

- Lần audit đầu, đăng nhập Herp thất bại do **SQL Server chết (mất cổng 1433)**
  — điều phối viên xác nhận sau đó: SQL đã lên lại, tài khoản `000000` kiểm
  chứng **KHÔNG bị khóa (Locked=0)**, đăng nhập SSO thành công. Đính chính
  cho giả thuyết "khóa tài khoản" trong bản báo cáo đầu: các entry "đã bị
  khóa" trong log CIS buổi sáng thuộc sự cố sweep-final trước đó, không phải
  phiên này; các thất bại muộn trong phiên này là do SQL chết.
- Audit lại đã chụp đủ: trang chủ (desktop + mobile + tablet), kho, mua hàng,
  chuyển kho, báo cáo (desktop + mobile). Ảnh đợt đầu (toàn trang login
  bounce) đã được sao lưu riêng ở `/tmp/herp-first-run-backup` (không nằm
  trong workspace) và **đã bị ghi đè bằng ảnh thật** trong `shots-redesign/herp/`.
- Lưu ý kỹ thuật trong lúc audit lại (để chặng sau tránh lặp): (1) trang
  `/Login/Index` tự xóa cookie phiên khi tải, nên **không được điều hướng
  lại trang Login sau khi đã đăng nhập** (script audit đầu mắc lỗi này ở
  Herp; CIS thoát được là do khác biệt chưa rõ về cookie — không kết luận);
  (2) nút đăng nhập đôi khi không phản hồi vì handler jQuery chưa được bind
  khi script cookie tải chậm — lần thử lại đã dùng chính hàm `loginClick`
  của trang (cùng credential, vẫn đúng 1 lần CheckLogin) để đăng nhập;
  (3) các lần chạy mobile liên tiếp làm Firefox crash/treo do RAM eo hẹp
  (VM 8GB, chỉ còn ~1,1GB trống; đã dọn 2 tiến trình Firefox mồ côi của chính
  worker này, không động tới app/dotnet nào).

| Trang Herp | Desktop 1280 | Mobile 390×844 | Tablet 768 |
|---|---|---|---|
| Đăng nhập (`/Login/Index`) | ✅ chụp (xem §2.1) | ✅ chụp | ✅ chụp (bounce cũ đã thay bằng login thật ở đợt sau) |
| Trang chủ (`/Home/Index`) | ✅ chụp — dashboard 5 thẻ KPI + 2 biểu đồ | ✅ chụp — thẻ bóp 5 cột, biểu đồ bị cắt | ✅ chụp — biểu đồ bị cắt phải |
| Kho (`/WarehouseManage/Warehouse/Index`) | ✅ chụp — lưới có 1 dòng thật | ✅ chụp — chỉ thấy 2 cột đầu | — |
| Mua hàng (`/PurchaseManage/Purchase/Index`) | ✅ chụp — lưới 0 dòng, 13 cột vượt viewport | ✅ chụp — nút "Tra cứu" bị cắt | — |
| Chuyển kho (`/Home/KfChange`) | ✅ chụp — trang tối giản | ✅ chụp, không tràn | — |
| Báo cáo (`/Report/Index`) | ✅ chụp — trang thông báo SSRS | ✅ chụp, không tràn | — |

### 0.3. Lưu ý phương pháp: điều hướng trực tiếp ≠ chạy trong portal SSO

Các trang CIS/Herp là **fragment chạy trong iframe của portal SSO** trong vận
hành thật. Audit này điều hướng trực tiếp từng URL (đúng yêu cầu nhiệm vụ).
Vì vậy một số lỗi dưới đây (đặc biệt `PatientList: $ is not defined`, lưới
không render) có thể **khác khi chạy trong iframe portal** (nơi frame cha có
thể cung cấp jQuery). Điều phối viên nên cho kiểm chứng lại các trang này
**bên trong portal SSO** sau khi mở khóa, trước khi chốt thiết kế. Những lỗi
không phụ thuộc iframe (tràn viewport, nhãn dính chữ, footer, thiếu h1/
breadcrumb/topbar/sidebar) thì đứng vững trong cả hai ngữ cảnh.

### 0.4. Ảnh

Tất cả ảnh ở `shots-redesign/cis/` và `shots-redesign/herp/` (thư mục đã
gitignore, không commit). Quy ước tên: `<trang>-<desktop|mobile|tablet>.png`,
cộng `home-mobile-sidebar.png` (ảnh sau khi thử bấm nút mở sidebar — không đổi,
vì không có nút nào, xem §4.3). File `probe.json` mỗi app chứa số đo tràn,
script nạp, phiên bản asset, console error đầy đủ.

---

## 1. CIS — từng trang × từng lỗi (map theo checklist DESIGN.md §9)

### 1.1. Đăng nhập — `/Login/Index`

Ảnh: `shots-redesign/cis/login-desktop.png`, `shots-redesign/cis/login-mobile.png`.

- [§9.1/§9.8 — footer + vendor] **Footer lỗi, liệt kê (không viết lại):**
  desktop hiển thị `Copyright © 2025` + chuỗi nửa dịch nửa Trung, các từ dính
  nhau không khoảng trắng: "hồbắcngàySố ngàychuỗiKỹ thuậtcóhạncông ty" +
  `《天天开源软件（社区版）许可协议》`. Logo góc trái trên là chữ vendor tiếng
  Trung (ảnh). Trang Herp tương tự nhưng footer **hoàn toàn tiếng Trung**
  (xem §2.1). Đây là 2/2 trang đăng nhập còn chữ vendor.
- [§9.1 — tràn viewport] Desktop: `documentElement.scrollWidth 1799` so với
  `clientWidth 1280` (lệch 519px) dù `body.scrollWidth` chỉ 1280 — do script
  login tự scale body theo `screen.width/1920` (`transform: scale(...)` +
  đặt lại `body.width`). Mobile 390: `div.login-head` rộng **825px** (tràn
  435px), ảnh nền bị cắt trái (huy hiệu HIS bị cắt một nửa trong ảnh mobile),
  footer xuống dòng nhiều lần. Bản thân hộp đăng nhập vẫn căn giữa, bấm được.
- [§9.3 — form] Ô tài khoản/mật khẩu **chỉ có placeholder, không có nhãn
  hiện hữu** ("Tài khoản đăng nhập", "Mật khẩu đăng nhập" biến mất khi gõ).
  Nút "Đăng nhập" là nút chính duy nhất — đạt. Không có captcha trong cấu
  hình này (`#txt_code`/`#imgcode` = 0 phần tử) — đạt về mặt không chặn user.
- [§9.7 — mobile] Hộp login dùng được ở 390px, nhưng nền trang trí bị cắt và
  footer tràn là lỗi thị giác. Không có dấu xếp chồng bị cắt trong các chuỗi
  hiện có ("Chào mừng", "Đăng nhập" render nguyên vẹn).
- Asset trang login: chỉ jQuery 2.1.1 + cookie + md5 từ static host
  (`127.0.0.1:41142`); không có Bootstrap/jqGrid trên trang này (không xác
  định được phiên bản Bootstrap từ trang login — xem §4.1).

### 1.2. Trang chủ — `/Home/Index` (desktop + mobile + tablet)

Ảnh: `shots-redesign/cis/home-desktop.png`, `home-mobile.png`,
`home-tablet.png`, `home-mobile-sidebar.png`.

- **Phát hiện khung:** Đây **không phải trang chủ portal thật**. Nó là trang
  stub không layout (`Layout = null`, không script, không `meta viewport`,
  `margin:30px`): một `<h2>` + danh sách liên kết bullet tới các phân hệ
  (ClinicManage, DoctorManage, NurseManage, QueueManage, ReportManage,
  SystemManage, TemplateManage) + dòng "Chức năng gốc" (Bệnh án ngoại trú |
  Danh sách bệnh nhân | Đơn thuốc). Trang chủ thật được phục vụ bởi cổng SSO
  (ghi ngay trong nội dung trang). Vì vậy **mọi mục §9.1/§9.2 về topbar/
  sidebar đều không áp dụng được ở đây** — không có topbar, sidebar, tên
  người dùng, nút đăng xuất, breadcrumb, footer. Redesign phải quyết định
  rõ: trang này sẽ bị thay bằng WorkSpace của portal, hay được nâng thành
  trang chủ thật — audit không tự quyết.
- [§9.1] Không có `<h1>` (chỉ `<h2>`); tiêu đề dùng ngôn ngữ "bản port .NET 6"
  (chữ kỹ thuật, không phải ngôn ngữ nhiệm vụ).
- [§9.7] Không tràn ở cả 3 viewport (desktop 0, mobile 0, tablet 0). Nhưng đó
  là vì trang gần như trống, không phải vì responsive tốt: ở tablet/mobile
  vẫn là danh sách dọc không có khung điều hướng.
- [§9.2/§3.5] **Không có sidebar, không có nút hamburger** ở mobile (probe:
  0 ứng viên nút, 0 phần tử aside; ảnh sau khi thử bấm `home-mobile-sidebar.png`
  giống hệt ảnh mobile thường). Không thể đánh giá hành vi drawer — vì không
  có gì để mở (xem §4.3).
- Console: 0 lỗi. Nhưng trang không tải bất kỳ asset nào nên cũng không có
  gì để lỗi.

### 1.3. Trạm làm việc bác sĩ — `/MainBusiness/Index`

Ảnh: `shots-redesign/cis/mainbusiness-desktop.png`,
`shots-redesign/cis/mainbusiness-mobile.png`.
Lưu ý: trang **không đạt `networkidle` trong 60s ở cả 2 viewport** (tiếp tục
có request nền — có thể là polling). Ảnh vẫn chụp được sau timeout; đây là
ghi chú hiệu năng, không phải lỗi 500.

- [§9.4 — lưới] **Lưới không hiển thị nội dung.** DOM có `.ui-jqgrid`
  (`hasGrid: true`) nhưng ảnh chỉ thấy 3 tab + banner cảnh báo, phía dưới là
  **trống trắng toàn bộ**, không có dòng dữ liệu, không có trạng thái
  "Đang tải…", không có trạng thái rỗng kiểu §6.2 (locale jqGrid có sẵn
  `emptyrecords: "Không có dữ liệu hiển thị"` nhưng không thấy render).
  Không đánh giá được: thanh công cụ lưới, thứ tự lọc/trái–thao tác/phải,
  chỉ báo sắp xếp, dòng được chọn, chân lưới "Đang xem x–y trên tổng z dòng"
  — **tất cả đều không quan sát được** vì lưới rỗng. Không kết luận "lưới
  hỏng" tuyệt đối (có thể do không có bệnh nhân nào / dữ liệu demo, hoặc do
  lỗi JS dưới đây); kết luận chắc chắn: **người dùng nhìn thấy trang trắng
  không hướng dẫn** — vi phạm §6.2.
- [Console] Lỗi JS ở cả 2 viewport: `PAGEERROR: jclocaldata is not defined`.
- [§9.1/§9.8 — tab] Nhãn tab dính chữ, không khoảng trắng: "Thuốc TâyĐơn
  thuốc" (desktop); mobile thì tab bẻ thành cột hẹp từng từ ("Danh / sách /
  bệnh / nhân", "Thuốc / TâyĐơn / thuốc") — đọc rất khó, vi phạm §7 về nhãn
  dài tiếng Việt và §9.7 (thanh công cụ không tràn nhưng ở đây là **bẻ vụn**).
- [§9.7 — mobile] **Banner cảnh báo bị che mất dòng đầu:** ảnh mobile chỉ
  thấy từ "Windows) không thể chạy trên Linux…" — dòng đầu ("Thông báo:
  Tích hợp LIS ActiveX…") bị thanh tab đè lên. Đây là lỗi chồng lấp (overlap),
  không phải lỗi tràn (đo tràn mobile = 0).
- [§9.1] Không có topbar/sidebar/breadcrumb/`<h1>`/footer (trang fragment
  trong iframe). Không có định danh bệnh nhân đang xử lý dính đầu trang
  (§6.4) — trang chưa chọn bệnh nhân nên chưa đánh giá được, nhưng khung
  cho nó cũng không thấy.
- [§6.3 — banner] Banner ActiveX/LIS là thông báo nội tuyến dài, nội dung
  đúng dạng cảnh báo hệ thống (ghi rõ cần giải pháp thay thế, đã ghi nhận
  triển khai riêng) — về mặt nội dung là tốt; về mặt trình bày thì bị che
  trên mobile (lỗi trên).
- Không tràn viewport ở cả desktop và mobile (đo = 0).

### 1.4. Lưới chờ khám — `/PatientList/Index` → `/PatientList/PatientList`

Ảnh: `shots-redesign/cis/patientlist-desktop.png`,
`shots-redesign/cis/patientlist-mobile.png`.
Đây là trang chức năng chính bị lỗi nặng nhất trong đợt audit.

- [§9.4 — lưới] **Không có lưới.** Ảnh desktop chỉ có thanh lọc; ảnh mobile
  cũng vậy. Không có dòng, không có phân trang, không có chân lưới, không có
  trạng thái rỗng/lỗi. Nguyên nhân trực tiếp quan sát được:
  **console `PAGEERROR: $ is not defined` ở cả 2 viewport**; probe chỉ thấy
  đúng 1 script (`/Content/js/pdjhyybb.js`), **không có jQuery, không có
  jqGrid, không có Bootstrap** trong DOM trang độc lập. Trang này được viết
  với `Layout = null` và dường như trông cậy vào frame cha/portal cung cấp
  `$` — điều hướng trực tiếp làm nó chết. Xem lưu ý §0.3: cần kiểm chứng
  lại trong portal trước khi kết luận "chức năng hỏng", nhưng ở dạng hiện
  tại, **mở trực tiếp URL này cho ra trang không dùng được**.
- [§9.4 — lọc] Thanh lọc hiện hữu nhưng: (1) nhãn dính chữ không khoảng
  trắng: "Khám bệnhLoại:", "Bắt đầuKhám chữa bệnh" (nút), placeholder
  "Vui lòng nhậpSố khám ng…" bị cắt; (2) **không thấy nút "Xóa lọc"** và
  không thấy chỉ báo điều kiện lọc đang áp dụng ngoài các radio đang chọn;
  (3) các nút bấm là nút trình duyệt trần, không class Bootstrap
  (trái với pattern nút §9.5: nút chính/phụ không phân biệt được).
- [§9.7 — mobile] **Tràn viewport 209px** (`scrollWidth 599` so với viewport
  390), 8 phần tử tràn: `table.form` (591px), hàng lọc, và ô tìm kiếm
  `input#txt_keyword` (rộng 180px cố định, bị đẩy ra ngoài tới x=425).
  Nhãn radio bẻ từng từ dọc ("Chờ / khám / bệnh"). **Hai nút hành động
  ("Bắt đầuKhám chữa bệnh", "Tiếp tục điều trị") bị đẩy ra ngoài viewport,
  không thấy trong ảnh mobile** — tức chức năng quan trọng (bắt đầu khám)
  **không bấm được trên mobile** (vi phạm §9.7 "chức năng quan trọng không
  bị ẩn chỉ vì viewport hẹp").
- [§9.3 — form] Nhãn nằm ngang trái, không chuyển thành nhãn-trên-trường ở
  mobile; form không thành một cột; trường ngày là ô trống không rõ định
  dạng (không thấy `dd/MM/yyyy` minh họa).
- [§9.1] Không `<h1>`, không breadcrumb, không topbar/sidebar/footer.
- Đề xuất chiến lược responsive (xem §4.4): lưới này **đề xuất B (dòng → thẻ)**
  vì nhiệm vụ là chọn từng bệnh nhân để bắt đầu khám, không phải so sánh
  ngang nhiều dòng — nhưng đây là đề xuất thiết kế, chỉ có ý nghĩa sau khi
  lưới render được.

### 1.5. Truyền dịch — `/InfusionManage/query`

Ảnh: `shots-redesign/cis/infusion-desktop.png`,
`shots-redesign/cis/infusion-mobile.png`.

- **Khung khác hẳn các trang CIS khác:** dùng `_mobileIndex.cshtml`
  (layout mobile), asset riêng từ static host: jQuery **1.10.2** (thư mục
  `H5/`) + `ydui.flexible.js` + `ydui.js` — tức trang này có **stack JS
  riêng, cũ hơn** (jQuery 1.10.2 so với 2.1.1 ở các trang khác). Đây là điểm
  không nhất quán xuyên app (§9.8: một họ asset dùng chung bị phá vỡ).
- [§9.7] `meta viewport` có `maximum-scale=1.0, user-scalable=0` — **chặn
  zoom của người dùng**, vi phạm khả năng tiếp cận (accessibility); cần bỏ
  khi redesign.
- [§6.2 — trạng thái rỗng] Nội dung duy nhất khi chưa có mã phiếu là dòng đỏ
  "Chưa nhập mã phiếu truyền dịch." + các nhãn nhỏ ("Số ghế/Số giường:",
  "Thời gian bắt đầu:", "Kết thúcThời gian:"). Đây là trạng thái rỗng có
  thông điệp, nhưng **không có hành động tiếp theo** (không thấy ô nhập mã
  phiếu hay nút nào trong ảnh) và các nhãn còn lại render **rất nhỏ** ở
  mobile (hiệu ứng rem của `ydui.flexible` trong viewport 390) — đọc khó.
  Nhãn "Kết thúcThời gian:" lại dính chữ.
- Không tràn viewport ở cả 2 viewport (đo = 0). Không console error.
- Không có topbar/sidebar/breadcrumb/`<h1>` (trang mảnh mobile, `<h1>` rỗng).
- Đề xuất chiến lược (xem §4.4): trang này vốn đã là dạng danh sách/thẻ
  mobile → **giữ hướng B** (thẻ theo từng phiếu truyền dịch), chỉ cần sửa
  cỡ chữ, bỏ chặn zoom, và thêm hành động "nhập mã phiếu" cho trạng thái rỗng.

---

## 2. Herp — những gì kiểm chứng được

### 2.1. Đăng nhập — `/Login/Index` (DUY NHẤT kiểm chứng được)

Ảnh: `shots-redesign/herp/login-desktop.png`, `shots-redesign/herp/login-mobile.png`,
`shots-redesign/herp/home-tablet.png` (thực chất cũng là trang đăng nhập).

- Cùng shell với CIS (hộp "Chào mừng", nút "Đăng nhập" duy nhất, jQuery 2.1.1
  từ static host, không captcha trong cấu hình này) và cùng lỗi tràn:
  desktop `scrollWidth 1799` vs `1280`; mobile `div.login-head` 825px vs
  viewport 390, nền cắt trái; tablet 768: tràn 311px (`1079` vs `768`).
- **Footer liệt kê (không viết lại):** footer Herp là **tiếng Trung toàn bộ**:
  `Copyright © 2025湖北天天数链技术有限公司本系统软件源代码许可来源于《天天开源软件（社区版）许可协议》`
  — khác với CIS (nửa dịch, dính chữ). Hai app dùng cùng mẫu login nhưng
  footer không nhất quán với nhau (§9.8).
- **Lỗi đăng nhập đợt đầu (đã rõ nguyên nhân):** CheckLogin từng trả
  `state: error`, message `登录失败，请联系管理员` — tiếng Trung, khác với
  message tiếng Việt của CIS cho cùng một bước. Nguyên nhân đã được điều
  phối viên xác nhận là **SQL Server chết**, không phải tài khoản bị khóa
  (Locked=0) và không phải lỗi riêng của Herp. Ghi chú còn lại cho chặng
  thiết kế: thông báo lỗi đăng nhập **không nhất quán ngôn ngữ giữa 2 app**
  (CIS tiếng Việt, Herp tiếng Trung) — cần chuẩn hóa chuỗi dùng chung.
  Một điểm giòn khác quan sát được trong lúc audit lại: handler nút đăng
  nhập đôi khi không bind kịp khi script cookie tải chậm (bấm không phản
  hồi, không gửi CheckLogin) — nên lần thử lại đã gọi trực tiếp hàm
  `loginClick` của trang với cùng credential; chặng triển khai nên làm
  cho việc bind nút đăng nhập không phụ thuộc vào plugin cookie.

### 2.2. Trang chủ — `/Home/Index` (desktop + mobile + tablet)

Ảnh: `shots-redesign/herp/home-desktop.png`, `home-mobile.png`,
`home-tablet.png`.

- Khung: `<h2>` "Herp — Hệ thống quản lý vật tư (bản ASP.NET Core 6.0)"
  (chữ kỹ thuật, không phải ngôn ngữ nhiệm vụ); **không `<h1>`, không
  topbar/sidebar/breadcrumb/footer, không `meta viewport`**; hàng liên kết
  điều hướng (Quản lý kho | … | Đăng xuất) đóng vai trò menu — không có
  trạng thái active cho trang hiện tại (§9.2).
- [§6.1 — KPI] 5 thẻ "Số lượng cần xử lý" đều = 0: "Duyệt điều chỉnh giá",
  "Xuất kho chờ duyệt", "Nhập kho ngoài chờ duyệt", "Duyệt trả hàng nội bộ",
  "Vật tư quá hạn". Thẻ **không có điểm so sánh** (kỳ trước/mục tiêu) và
  không có hướng tốt/xấu — đúng dạng "thẻ chưa hoàn thành" theo §6.1 (0 ở
  đây là dữ liệu thật, không phải số bịa). Icon thẻ đầu tiên là ký hiệu **¥
  (yên)** cho "điều chỉnh giá" — sai đơn vị tiền Việt Nam (phải là ₫ hoặc
  icon trung tính), vi phạm §7/§9.8. Hai dòng cảnh báo đỏ phía trên
  ("Cảnh báo tồn kho: 0 mặt hàng | Cảnh báo quá hạn: 0 mặt hàng") dùng màu
  đỏ cho giá trị 0 — đỏ luôn bị đọc là "nguy hiểm" (§2.1).
- [§6.1 — biểu đồ] Biểu đồ đường "Thống kê nhập–xuất–tồn" (Th.1–Th.12, legend
  Xuất kho/Nhập kho/Lãi lỗ) toàn 0; panel "Thống kê nhập kho năm nay"
  (tròn) **trống hoàn toàn, không có trạng thái rỗng** (§6.2) — chỉ có khung
  và tiêu đề.
- [§9.7 — mobile/tablet] **Dashboard không co cột:** mobile vẫn giữ 5 thẻ
  trên một hàng (float 20%), thẻ bị bóp, nhãn bẻ từng từ và cắt ("Duyệt điều
  chỉnh giá" vỡ vụn, "Vật tư quá hạn" cắt phải). Biểu đồ đường bị dồn vào
  cột 64% hẹp, nhãn tháng chéo/cắt; panel tròn trống. Tablet 768: container
  Highcharts rộng 350px nhưng đặt tới x=849 (vượt viewport 768), mobile tới
  x=604 (vượt 390) — **biểu đồ bị cắt phải, không cuộn được** (trang
  `scrollWidth` vẫn bằng viewport do khối cha cắt tràn). Vi phạm §4.3:
  mobile phải là 2 cột (hoặc 1 cột), không giữ 5 cột.
- [§9.2/§3.5] Không có sidebar/hamburger (probe mobile: 0 ứng viên) — giống
  CIS, vấn đề sidebar thuộc tầng portal (xem §4.3).
- Console: 0 lỗi. Asset trang chủ: jQuery 2.1.1 + Chart.js + highcharts
  (trong app) từ static host.

### 2.3. Kho — `/WarehouseManage/Warehouse/Index` (desktop + mobile)

Ảnh: `shots-redesign/herp/warehouse-desktop.png`,
`warehouse-mobile.png`.

- Đây là trang lưới **tốt nhất trong đợt audit** về mặt cấu trúc: toolbar
  đúng pattern §9.4 (trái: nút reload; tiếp: Thêm mới/Sửa/Xóa/Đồng bộ vật tư
  có icon + nhãn; phải: ô tìm kiếm "Tên" + nút kính lúp). Lưới render **1
  dòng thật**: "Kho vat tu nha thuoc", Cấp 0, Người tạo 000000,
  Thời gian tạo 2023-03-28 00:00, 2 toggle xanh (Hiệu lực, Đồng bộ mặc định).
- [§9.5] Nút reload là **icon đơn độc không nhãn** (chỉ `fa-refresh`) —
  cần tooltip + `aria-label` (chưa kiểm chứng có hay không); các nút còn lại
  có nhãn đầy đủ. **Hai nút primary cạnh nhau là lỗi §2.1?** Ở đây cả 5 nút
  đều dùng `btn-primary` — vi phạm "một nút chính duy nhất mỗi vùng thao
  tác" (Thêm mới là chính, còn lại phải là phụ/ghost).
- [§9.4] **Không thấy chân lưới/pager** trong ảnh desktop lẫn mobile
  (probe `gridPager` rỗng) — không có "Đang xem x–y trên tổng z dòng";
  không thấy chỉ báo sắp xếp đang áp dụng dù tiêu đề cột có class sortable;
  không thấy nút "Xóa lọc"; không thấy dòng được chọn nổi bật (chưa chọn
  dòng nào — thao tác Sửa/Xóa khi chưa chọn chưa kiểm chứng, audit không
  bấm).
- [§9.7 — mobile] Toolbar xếp 2 hàng (hàng 2: ô tìm kiếm chiếm gần hết chiều
  rộng) — chấp nhận được. Lưới nằm trong wrapper `.table-responsive`
  rộng 392px ≈ viewport (bảng trong 1149px) — tức **có cuộn ngang trong
  wrapper** (pattern A về mặt cấu trúc), nhưng **không có cột neo**: cuộn
  sang phải sẽ mất cột "Tên kho". Ảnh mobile chỉ thấy 2 cột đầu (Tên kho,
  Vị trí). Chiều cao nút toolbar ~30px, dưới ngưỡng chạm 44px (§9.5).
- [Dữ liệu demo] Ô "Quản trị viên" của dòng duy nhất chứa chuỗi tiếng Trung
  `管理员,新致演示用户` — đây là **nội dung dữ liệu demo** (tên người tạo
  trong DB), không phải chuỗi giao diện; liệt kê để chặng dữ liệu thật xử
  lý, redesign không sửa dữ liệu.
- Console: 0 lỗi. Asset: jQuery 2.1.1, jqGrid 5.1.0, Bootstrap 3.3.4,
  locale-cn trước/vi sau (giống CIS).

### 2.4. Mua hàng — `/PurchaseManage/Purchase/Index` (desktop + mobile)

Ảnh: `shots-redesign/herp/purchase-desktop.png`,
`purchase-mobile.png`.

- Thanh lọc: 2 ô ngày (2026-04-28 → 2026-10-28) + nút "Tra cứu".
  **[§5.1/§7] Định dạng ngày là `yyyy-MM-dd`, không phải `dd/MM/yyyy`** —
  vi phạm quy ước ngày Việt Nam thống nhất.
- [§9.4 — lưới] Lưới **13 cột** (Thời gian đơn hàng, Điểm giao hàng, Loại
  đơn hàng, Số đơn hàng, Số kế hoạch của bệnh viện, Ngày giao hàng muộn
  nhất, Trạng thái đơn hàng, Phương thức mua sắm, Mã hệ thống, Có kèm dịch
  vụ đi kèm không, Số dòng chi tiết, …) nhưng **0 dòng dữ liệu**. Bảng rộng
  1483px vượt viewport 1280 (desktop) và 390 (mobile); trong ảnh desktop,
  các cột phải bị cắt ở mép phải và **không thấy thanh cuộn ngang** trong
  vùng lưới (probe mobile cho thấy có wrapper `.table-responsive` 392px,
  nhưng thao tác cuộn chưa được kiểm chứng — audit chỉ chụp tĩnh). Không
  có cột neo (cuộn sẽ mất cột định danh). Chiến lược: **A** (xem §4.4).
- [§6.2 — trạng thái rỗng] Hai thông điệp rỗng **không nhất quán và sai
  sắc thái**: dòng đỏ "Không tìm thấy dữ liệu liên quan!" (đỏ = lỗi, trong
  khi rỗng không phải lỗi) + chân lưới "Không có dữ liệu hiển thị". Pager
  ghi "Trang 1 trên tổng số 0 trang" (probe trích "Trang trên tổng số
  0 trang" do số trang render muộn) — cặp số 1/0 khó hiểu. Không có hành
  động tiếp theo ("Tạo đơn mới" hay "Xóa bộ lọc").
- [§9.7 — mobile] Nút "Tra cứu" **bị cắt ở mép phải** (chỉ thấy "Tra c");
  lưới chỉ thấy 3 cột đầu; các nút hành động cuối trang (Tạo mới/Sửa/
  Truyền báo/…) bị cắt nửa ở đáy viewport và bị nút mũi tên nổi đè lên.
  Các nút cuối trang này là nút chính/phụ? Tất cả đều `btn` xanh giống
  nhau — lại vi phạm "một nút chính duy nhất".
- Phía dưới lưới còn khối "Chi tiết chứng từ" trống (lưới chi tiết thứ hai
  chưa có dữ liệu) — trang có 2 lưới xếp dọc, đều rỗng.
- Console: 0 lỗi. Asset: giống trang kho.

### 2.5. Chuyển kho — `/Home/KfChange` và Báo cáo — `/Report/Index`

Ảnh: `shots-redesign/herp/kfchange-desktop.png`,
`kfchange-mobile.png`, `report-desktop.png`, `report-mobile.png`.

- Chuyển kho: trang tối giản, không script, không `<h1>`: tiêu đề "Chuyển
  kho hiện tại", 1 dòng "Kho vat tu nha thuoc", liên kết "Về trang chủ".
  Không tràn ở cả 2 viewport. Không có khung chọn kho dạng danh sách (chỉ
  hiện kho hiện tại) — hành vi chọn kho khác chưa quan sát được (audit
  không bấm).
- Báo cáo: **trang thông báo thay cho dữ liệu giả/404 — đúng tinh thần
  §6.2/§11**: ghi rõ 13 báo cáo SSRS gốc chưa chạy được trên ASP.NET Core
  6.0 và cần máy chủ SSRS riêng hoặc xây lại bằng công cụ .NET Core, kèm
  liên kết "Về trang chủ". Không tràn, không console lỗi. Đây là khối
  Windows-only/SSRS ngoài phạm vi redesign — chỉ ghi nhận, không thiết kế
  thay thế.

---

## 3. Các phát hiện xuyên app (CIS + Herp)

1. **Không có khung chuẩn §3.1 ở các trang nghiệp vụ.** Không trang nào trong
   đợt này có đủ topbar + sidebar + breadcrumb + footer dùng chung. Trang
   chủ CIS là stub; các trang chức năng là fragment iframe. Tính nhất quán
   §9.1/§9.2 vì vậy **không thể đạt bằng cách sửa từng trang** — phải quyết
   ở tầng portal/shell (SSO) hoặc chuẩn hóa layout `_LayoutIndex` — quyết
   định thuộc chặng thiết kế chi tiết, audit chỉ ghi hiện trạng.
2. **Không có `<h1>` ở bất kỳ trang nào.** CIS home dùng `<h2>`; các trang
   còn lại không có heading cấp 1; tiêu đề trang (title) có trang trống
   (`PatientList`, `Infusion` title rỗng trong probe).
3. **Nhãn tiếng Việt dính chữ** là lỗi lặp lại khắp CIS: "Khám bệnhLoại:",
   "Thuốc TâyĐơn thuốc", "Bắt đầuKhám chữa bệnh", "Kết thúcThời gian:",
   "Vui lòng nhậpSố khám ng…". Nguyên nhân là các chuỗi dịch được nối mà
   không có khoảng trắng — cần soát chuỗi, không phải sửa CSS.
4. **Chuỗi dấu xếp chồng (§7):** các chuỗi có mặt ("Chào mừng", "Đăng nhập",
   "Chờ khám bệnh", "Tiếp tục điều trị") render không bị cắt dấu. Các chuỗi
   kiểm chứng bắt buộc (§7: "Ủy quyền", "Ứng trước", "Ổn định") **không xuất
   hiện** trên các trang đã audit nên chưa kiểm chứng được — cần đưa vào
   checklist kiểm chứng lại sau redesign.
5. **Console errors:** `MainBusiness: jclocaldata is not defined`;
   `PatientList: $ is not defined` (cả 2 viewport). Các trang còn lại: 0 lỗi
   console trong lần đo (Herp login: 0).
6. **Không có nút nguy hiểm nào được quan sát** (không trang nào render tới
   bước có nút Xóa/Hủy) nên §9.5 về nút danger và §6.3 về modal xác nhận
   **chưa kiểm chứng được** — không ghi đạt/không đạt.
7. **Thông báo:** chỉ quan sát được 2 dạng — banner cảnh báo ActiveX
   (MainBusiness, nội dung tốt, bị che trên mobile) và thông báo lỗi đăng
   nhập (CIS tiếng Việt, Herp tiếng Trung — không nhất quán). Không quan
   sát được toast, modal xác nhận, trạng thái "Đang tải…" có chữ.

---

## 4. Xác nhận các điểm mở từ DESIGN.md §0/§4/§10

### 4.1. (a) Phiên bản asset static host đang phục vụ — ĐÃ XÁC NHẬN (CIS)

Probe trên trang thật (`/MainBusiness/Index`, trang duy nhất nạp đủ stack):

| Asset | Xác định được | Bằng chứng |
|---|---|---|
| jQuery | ✅ **2.1.1** | `jQuery.fn.jquery` = 2.1.1; file `~/js/jquery/jquery-2.1.1.min.js` từ static host `127.0.0.1:41142` (nạp 2 lần trong trang này) |
| jqGrid | ✅ **5.1.0** | `jQuery.jgrid.version` = 5.1.0; file `~/js/jqgrid/jqgrid.min.js` |
| Bootstrap | ✅ **3.3.4** | `jQuery.fn.modal.Constructor.VERSION` = 3.3.4; file `~/js/bootstrap/bootstrap.js` |
| jQuery UI | ✅ có mặt | `~/js/jquery-ui/jquery-ui.min.js` |
| Thứ tự locale jqGrid | ✅ cn trước, vi sau | Script: `grid.locale-cn.js` (static host) → `/js/jqgrid/grid.locale-vi.js` (trong app) |
| Bootstrap 5 / AdminLTE 4 | ❌ **không có** trong shell nghiệp vụ | Không có dấu vết Bootstrap 5 (`data-bs-*`, class `d-*`) hay AdminLTE 4; xác nhận cảnh báo của §0: **đừng copy class Bootstrap 5 sang các trang nghiệp vụ** |

- **Đã xác định được (sau audit lại):** Herp nội bộ dùng đúng stack với CIS:
  jQuery 2.1.1, jqGrid 5.1.0, Bootstrap 3.3.4 (đo trên trang kho và mua hàng),
  locale-cn trước/vi sau, thêm `herp-vi.js` trong app. Trang chủ Herp dùng
  thêm Chart.js + highcharts. Các trang tối giản (chuyển kho, báo cáo) không
  nạp script nào. Trang truyền dịch CIS vẫn là ngoại lệ duy nhất với
  jQuery 1.10.2 (`H5/`).
- Hệ quả cho redesign: mọi pattern §2–§5 phải viết theo Bootstrap 3.3.4 +
  jqGrid 5.1.0 + jQuery 2.1.1, không dùng class Bootstrap 5.

### 4.2. (b) Locale jqGrid hiển thị cuối cùng — TIẾNG VIỆT (mức JS defaults)

Trên `/MainBusiness/Index`, sau khi cả hai file locale đã nạp:

- `recordtext`: "Hiển thị {0} - {1} trong tổng số {2} bản ghi"
- `emptyrecords`: "Không có dữ liệu hiển thị"
- `pgtext`: "Trang {0} trên tổng số {1} trang"

→ **Locale Việt thắng** (nạp sau) ở mức cấu hình jqGrid. Hạn chế: lưới đang
trống nên **chưa nhìn thấy pager tiếng Việt render thật trên màn hình**;
khi lưới có dữ liệu cần chụp lại pager để chốt. **Herp xác nhận tương tự:**
trang kho và mua hàng có cùng bộ defaults tiếng Việt (đo trực tiếp); pager
mua hàng render "Trang 1 trên tổng số 0 trang" — đúng mẫu tiếng Việt nhưng
cặp số 1/0 khó hiểu (xem HERP-09). File `grid.locale-cn.js`
vẫn đang nạp thừa trước đó (đúng như §9.8 lưu ý — xử lý file thừa là quyết
định riêng của chặng triển khai, audit không xóa).

### 4.3. (c) Hành vi sidebar mobile + nút mở rộng — KHÔNG TỒN TẠI ở các trang này

- Trang chủ CIS: không sidebar, không hamburger, không aside, không drawer,
  không overlay (probe mobile: 0 ứng viên; thử bấm không đổi gì).
- Trang chủ Herp: tương tự — chỉ có hàng liên kết dọc, probe mobile 0 ứng
  viên nút mở rộng; ảnh `home-mobile-sidebar.png` giống hệt `home-mobile.png`.
- Các trang chức năng CIS/Herp: fragment, không sidebar.
- Trang đăng nhập: không sidebar (đúng — trang đăng nhập không cần).
- Kết luận: **vấn đề "sidebar mobile thiếu nút mở rộng" (§10) không thể
  kiểm chứng — cũng không thể sửa — ở tầng trang CIS/Herp.** Nó sống ở
  tầng shell portal SSO (tab/iframe + nav-col) hoặc ở `_LayoutIndex` của
  các app khác. Chặng audit của các app còn lại (đặc biệt SSO portal, PDS)
  phải là nơi xác nhận §3.5 (hamburger < 992px, drawer + overlay + khóa
  cuộn nền + focus trả về). Với CIS/Herp, yêu cầu đặt ra cho thiết kế là:
  khi các trang này chạy trong portal, nút mở sidebar của **portal** phải
  hoạt động — không phải mỗi fragment tự có sidebar riêng.

### 4.4. (d) Hồ sơ chiến lược responsive A/B/C cho từng lưới chính

Quy ước §4.1: A = cuộn ngang + cột neo; B = dòng → thẻ; C = cột ưu tiên.
Mặc định §4 là A trừ khi nhiệm vụ chứng minh khác.

| Lưới | Nhiệm vụ thực tế | Chiến lược | Trạng thái |
|---|---|---|---|
| CIS MainBusiness — lưới bệnh nhân trong trạm bác sĩ | Làm việc đa tab, đối chiếu nhiều dòng/cột trong ca khám | **A** (giữ cấu trúc, cột mã/tên bệnh nhân neo trái, `setGridWidth` khi resize) | Đề xuất — chưa kiểm chứng render vì lưới đang trống |
| CIS PatientList — lưới chờ khám | Chọn **từng** bệnh nhân để bắt đầu khám (radio Chờ/Đang/Đã khám + nút Bắt đầu) | **B** (mỗi bệnh nhân thành thẻ: tên + trạng thái + nút Bắt đầu) — A là phương án dự phòng nếu bác sĩ cần so sánh hàng đợi | Đề xuất — lưới hiện chưa render (lỗi `$`) nên chưa kiểm chứng |
| CIS Infusion — danh sách truyền dịch | Xem từng phiếu truyền dịch trên thiết bị/mobile | **B** (trang vốn đã là dạng thẻ/list mobile) | Đề xuất theo layout hiện tại (`_mobileIndex` + ydui); cần sửa cỡ chữ + bỏ chặn zoom |
| Herp Warehouse — lưới kho | Duyệt danh sách kho + thao tác từng dòng (Thêm/Sửa/Xóa/Đồng bộ) | **A** (wrapper cuộn ngang đã có; bổ sung cột neo "Tên kho"; B là phương án dự phòng nếu chỉ xử lý từng kho) | Đã kiểm chứng cấu trúc (wrapper 392px mobile, bảng 1149px); chưa kiểm chứng thao tác cuộn và cột neo |
| Herp Purchase — lưới mua hàng | Duyệt/đối chiếu đơn hàng 13 cột, cần so sánh ngang | **A** (giữ cấu trúc + cột neo "Số đơn hàng"; sửa để cột phải tới được và thanh cuộn thấy được) | Đã kiểm chứng: bảng 1483px vượt viewport, cột phải bị cắt trong ảnh tĩnh; thao tác cuộn chưa kiểm chứng |
| Herp Home dashboard | Không phải lưới | Không áp dụng; theo §4.3: mobile 2 cột (hiện đang 5 cột bóp) | Đã kiểm chứng: mobile/tablet bóp + biểu đồ cắt phải |

Lưu ý §4.2: **không dùng `transform: scale()` để "vừa màn hình"** — nhưng
chính trang đăng nhập hiện tại đang dùng `body.style.transform = scale(...)`
theo độ phân giải (xem §1.1) và gây tràn đo được. Redesign cần bỏ cơ chế
scale này ở trang login, thay bằng layout co được thật.

### 4.5. Đối chiếu bảng tra nhanh §10

| Vấn đề §10 | Kết quả ở CIS/Herp |
|---|---|
| Trang chủ PDS mobile bóp méo | Ngoài phạm vi 2 app này (thuộc worker PDS). Pattern chẩn đoán §4.3 vẫn đúng và đã áp dụng: ở CIS, nguyên nhân tràn login là `transform: scale` + `div.login-head` 825px cố định; tràn PatientList là `table.form` + input 180px cố định. |
| Sidebar mobile thiếu nút mở rộng | Không tồn tại ở tầng trang CIS/Herp (xem §4.3) — vấn đề thuộc tầng portal. |
| Bảng WorkSpace cuộn ngang | WorkSpace thuộc portal SSO, ngoài phạm vi 2 app này. Ở CIS, cuộn ngang hiện tại **không lành mạnh**: PatientList tràn mà không có cột neo/tiêu đề dính (vì lưới không render); MainBusiness không tràn nhưng lưới trống. |
| Footer tiếng Trung | Đã liệt kê đầy đủ cho 2 app: **CIS login** (nửa dịch, dính chữ) + **Herp login** (tiếng Trung toàn bộ) + logo vendor tiếng Trung ở cả 2 trang login. Các trang nội bộ CIS không có footer để liệt kê (fragment/stub không có footer). |

---

## 5. Danh sách lỗi tóm tắt (để chặng thiết kế dùng)

| ID | App/Trang | Lỗi | §9 | Mức |
|---|---|---|---|---|
| CIS-01 | CIS login | Footer nửa dịch + logo vendor tiếng Trung (liệt kê) | §9.1/§9.8 | Thấp* |
| CIS-02 | CIS login | Tràn do `transform: scale` (desktop 519px đo được) + `login-head` 825px (mobile) | §9.7 | Trung bình |
| CIS-03 | CIS login | Ô nhập chỉ có placeholder, không nhãn | §9.3 | Thấp |
| CIS-04 | CIS home | Trang stub: không topbar/sidebar/breadcrumb/footer, không `<h1>`, không `meta viewport` | §9.1/§9.2 | Cao (khung) |
| CIS-05 | CIS MainBusiness | Lưới trống trắng, không trạng thái tải/rỗng (§6.2) | §9.4/§6.2 | Cao |
| CIS-06 | CIS MainBusiness | Console `jclocaldata is not defined` | §9 (console) | Trung bình |
| CIS-07 | CIS MainBusiness | Nhãn tab dính chữ; mobile bẻ cột từng từ; banner bị tab che dòng đầu | §9.7/§9.8 | Cao (mobile) |
| CIS-08 | CIS MainBusiness | Không đạt `networkidle` 60s (polling nền) | Hiệu năng | Thấp |
| CIS-09 | CIS PatientList | Không render lưới; `$ is not defined` (trang độc lập thiếu jQuery) | §9.4 | Cao |
| CIS-10 | CIS PatientList | Tràn mobile 209px; nút Bắt đầu/Tiếp tục bị đẩy khỏi viewport | §9.7 | Cao (mobile) |
| CIS-11 | CIS PatientList | Nhãn/nút dính chữ; nút trần không phân biệt chính/phụ; không nút Xóa lọc | §9.3/§9.4/§9.5 | Trung bình |
| CIS-12 | CIS Infusion | Stack riêng jQuery 1.10.2 (lệch chuẩn 2.1.1) | §9.8 | Trung bình |
| CIS-13 | CIS Infusion | `user-scalable=0` chặn zoom; chữ mobile quá nhỏ; trạng thái rỗng thiếu hành động tiếp theo | §9.7/§6.2 | Trung bình |
| CIS-14 | Tất cả trang CIS | Không `<h1>`, không breadcrumb, không topbar/sidebar | §9.1 | Cao (khung) |
| HERP-01 | Herp login | Footer tiếng Trung toàn bộ + logo vendor (liệt kê) | §9.1/§9.8 | Thấp* |
| HERP-02 | Herp login | Tràn như CIS login (desktop 519, mobile 158 đo scroll, tablet 311) | §9.7 | Trung bình |
| HERP-03 | Herp login | Thông báo lỗi tiếng Trung, không nhất quán với CIS (tiếng Việt); nút đăng nhập đôi khi không bind kịp khi script cookie tải chậm | §9.8/§9.5 | Trung bình |
| HERP-04 | Herp home | Không `<h1>`/topbar/sidebar/breadcrumb/footer/`meta viewport`; KPI không điểm so sánh; icon ¥ sai đơn vị; cảnh báo 0 dùng màu đỏ; panel tròn trống không trạng thái rỗng | §9.1/§6.1/§6.2/§7 | Cao |
| HERP-05 | Herp home mobile/tablet | Giữ 5 thẻ/hàng → bóp, nhãn vỡ/cắt; biểu đồ Highcharts bị cắt phải (mobile tới x=604, tablet tới x=849) | §9.7/§4.3 | Cao (mobile) |
| HERP-06 | Herp kho | 5 nút đều `btn-primary` (vi phạm một nút chính); nút reload icon đơn độc; không thấy chân lưới/pager, không nút Xóa lọc | §9.5/§9.4 | Trung bình |
| HERP-07 | Herp kho mobile | Chỉ thấy 2 cột đầu; không cột neo; nút toolbar ~30px < 44px chạm | §9.7/§9.5 | Trung bình |
| HERP-08 | Herp mua hàng | Ngày `yyyy-MM-dd` (phải `dd/MM/yyyy`); 13 cột vượt viewport, cột phải bị cắt, không cột neo | §9.4/§5.1/§7 | Cao |
| HERP-09 | Herp mua hàng | Trạng thái rỗng kép không nhất quán ("Không tìm thấy…" đỏ + "Không có dữ liệu hiển thị"), pager "1 trên tổng số 0 trang", không hành động tiếp theo; nút "Tra cứu" bị cắt ở mobile | §6.2/§9.7 | Cao |
| HERP-10 | Herp chuyển kho/báo cáo | Trang tối giản/thông báo — không lỗi tràn; báo cáo SSRS là khối ngoài phạm vi (chỉ ghi nhận) | §6.2/§11 | Thấp |

\* Footer vendor: theo nhiệm vụ chỉ liệt kê, không tự viết lại; mức "Thấp"
ở đây là mức độ UI, còn quyết định thay chuỗi thuộc điều phối viên (đã có
tiền lệ: SSO Home + MRQC Login được liệt kê tương tự, chờ quyết định user).

**Vấn đề phát sinh đã xử lý (báo cáo, không sửa):** trong đợt đầu, đăng nhập
thất bại hàng loạt do SQL Server chết; điều phối viên đã khôi phục stack và
xác nhận tài khoản `000000` không bị khóa (Locked=0). Audit Herp đã chạy lại
đầy đủ sau đó. Hai bài học cho các worker khác: (1) đừng điều hướng lại
trang Login sau khi đăng nhập (trang tự xóa cookie phiên); (2) khi nút
đăng nhập bấm không phản hồi, kiểm tra handler có được bind không trước khi
kết luận sai mật khẩu — bấm không gửi request thì không tính là lần đăng
nhập thất bại.

---

## 6. Bằng chứng và giới hạn

- Ảnh + `probe.json`: `shots-redesign/cis/`, `shots-redesign/herp/`
  (gitignored, không commit). Herp hiện có đủ 14 mục (login/home/warehouse/
  purchase/kfchange/report × desktop/mobile + home/login tablet) với URL
  thật đã kiểm chứng trong `probe.json`.
- Số đo tràn, phiên bản asset, console error đều lấy từ trang thật bằng
  Playwright (không suy đoán từ mã nguồn, trừ khi ghi rõ "theo mã nguồn").
- Chưa kiểm chứng: hành vi trong iframe portal SSO (§0.3); thao tác cuộn
  ngang thực tế trong wrapper lưới Herp (mới chỉ thấy cấu trúc wrapper, chưa
  cuộn thử); nút nguy hiểm/modal/toast (§3.6); pager jqGrid khi lưới có
  nhiều trang; chuỗi dấu xếp chồng bắt buộc §7 (§3.4).
- Không sửa code, không commit, không chuyển sang redesign — đúng nhiệm vụ.
