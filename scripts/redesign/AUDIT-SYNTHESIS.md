# Tổng hợp audit UI/UX OpenHIS — đầu vào chặng 3 redesign

> Tài liệu tổng hợp từ 7 báo cáo audit (chặng 2), đã commit. Mọi phát hiện đều
> có ảnh + số đo trong báo cáo gốc; tài liệu này chỉ tổng hợp, xếp hạng và
> gán quyết định, không thêm phát hiện mới. Khi có mâu thuẫn giữa tài liệu
> này và báo cáo gốc, báo cáo gốc thắng.
>
> Báo cáo gốc: `DESIGN.md` (nghiên cứu), `audit-sso-mrqc-sett.md` (partial),
> `audit-sso-mrqc-sett-internal.md`, `audit-sso-mrqc-sett-catchup.md`,
> `audit-mrms-or-emr.md` (+ §10 bổ sung), `audit-cis-herp.md`,
> `audit-pds-base.md`. Ảnh: 3 vị trí (xem §6).

## 1. Stack đã xác nhận bằng bằng chứng live (chặng redesign KHÔNG được đoán khác)

- jQuery **2.1.1** trên mọi app (ngoại lệ duy nhất: trang truyền dịch CIS
  dùng jQuery 1.10.2). jqGrid **5.1.0**. Bootstrap shell nghiệp vụ **3.3.4**
  (xác nhận qua `$.fn.modal.Constructor.VERSION`) — **không có Bootstrap 5
  trên shell nghiệp vụ**, không được copy class Bootstrap 5 (`data-bs-*`,
  `d-md-block`…) sang trang nghiệp vụ. Layout độc lập của SSO dùng
  Bootstrap 5.1.0 nhưng đó là trang đơn, không phải shell nghiệp vụ.
- Asset nghiệp vụ đi qua **static host 127.0.0.1:41142** (không nằm trọn
  trong `wwwroot` từng app): `framework-font/theme/ui.css`,
  `framework-ui.js`, `Newtouch-ui.js`, bootstrap, jquery, jquery-ui,
  `jquery.layout.js`, wdtree, jqGrid. Chặng 3 phải xác định pipeline asset
  (repo → static host → runtime) trước khi sửa CSS/JS dùng chung.
- Locale jqGrid hiển thị cuối cùng là **tiếng Việt** trên mọi lưới đã đo
  (file `grid.locale-cn.js` nạp trước, `grid.locale-vi.js` của app thắng).
  Ngoại lệ: SSO dùng bootstrap-table, không phải jqGrid.
- Kiến trúc portal: SSO mở app con bằng **tab + iframe** (`indextab.js`,
  `.Newtouch_iframe`). Trang nội bộ mở trực tiếp là **fragment không shell**
  theo thiết kế — portal cấp shell. Xem quyết định §5.
- Breakpoint sẵn có trong repo: **991px / 767px** (chuẩn chung).
  `overflow-x: hidden` xuất hiện nhiều nơi nên `scrollWidth` = viewport
  không chứng minh được không tràn — chặng redesign phải xem cả phần tử
  con vượt mép, không chỉ nhìn `scrollWidth`.

## 2. Vấn đề hệ thống, xếp hạng (blocker trước)

### Chặn (mobile không dùng được hoặc chức năng không tới được)
1. **Không có hamburger/drawer trên mobile ở bất kỳ đâu.** Portal SSO
   mobile: không thấy nút mở sidebar, sidebar chỉ còn dải tối hẹp. Trang
   chủ mobile MRMS/EMR: shell không thành drawer, không thấy hamburger.
   PDS/Base home mobile: không có nút hamburger nào, mọi trang. Ứng viên
   duy nhất `a.btn.btnshrink` (MRMS/EMR) có kích thước 0. Không mở được
   sidebar = không điều hướng được trên mobile.
2. **Iframe trang chủ PDS/Base bị bóp còn 170px trên viewport 390.**
   Nguyên nhân trực tiếp: sidebar `display: none` nhưng iframe vẫn tính
   rộng như sidebar còn đó. Nguyên nhân gốc theo thứ tự §4.3: (1) không có
   `meta viewport` trên home, (2) chiều rộng pixel do JS gán lúc tải,
   (3) dashboard trong iframe không có wrapper cuộn cứu được,
   (4) sidebar/iframe ép chiều rộng. KPI cắt số, biểu đồ nát.
3. **Lưới mobile không cuộn được / mất cột.** Nặng nhất: NursingRecord
   (EMR) không cuộn bằng bất kỳ cơ chế nào (bdiv không thừa chiều rộng,
   tổ tiên không cuộn được, body chặn cuộn trang) — các cột sau "Nhiệt độ"
   không với tới được. OPStaff: cột biến mất (nén thay vì cuộn/thẻ).
   Mọi lưới Sett: cột đầu bị cắt khi cuộn, không có cột neo. Mọi lưới
   PDS/Base/Herp: không cột neo, cột định danh mất khi cuộn mobile.
4. **Form mobile vỡ.** MRQC Apply: ô ngày thành mảnh "2026" xếp dọc,
   lưới 1763px nhưng tham số jqGrid báo width 390. EMR Main: form không
   xếp dọc, nhãn/trường chồng lấn. Bookkeep mobile: form 1125px trong
   viewport 390, ô nhập bóp thành khe dọc, nhãn phải bị cắt. HOSP Form:
   giữ nhãn-trái, nhãn dài cắt, `.container` 420px vượt 390. Base
   Organize: giữ 2 cột, select cắt, nhãn "WeChat" chưa dịch. StockQuery:
   panel lọc cắt mép phải. Mua hàng (Herp) mobile: nút "Tra cứu" cắt
   còn "Tra c", nút cuối trang bị cắt/đè.

### Cao (nhất quán / chức năng bị che)
5. **Không có `<h1>` trên bất kỳ trang nào đã đo.** Tiêu đề tab HOSP/Base
   Web chỉ là "Index"/"Form" (tên controller/action mặc định). Trang chủ
   trần Sett/MRQC lộ tên area kỹ thuật, không có tiêu đề nhiệm vụ.
6. **Trạng thái rỗng trình bày như lỗi.** Chữ đỏ "Không tìm thấy dữ liệu
   liên quan!" (Sett DaySett/Patient, StockQuery, SysGlobalConfig) + chân
   phân trang vỡ mẫu ("Trang trên tổng số 1 trang", "Trang 0 trên tổng
   số trang", "1 trên tổng số 0 trang"). Bookkeep desktop: rỗng trắng
   trơn không hướng dẫn.
7. **Trang đăng nhập tràn ngang.** MRMS/OR/EMR: +519px desktop, +158px
   mobile, cả 3 app. SSO: chồng tiêu đề, khối login-head 1160px trên
   viewport 390. MRQC: 1799px trên viewport 1280 (bị `overflow-x: hidden`
   che). 3 app SSO/MRQC/Sett dùng 3 kiểu template đăng nhập khác nhau.
8. **Portal tablet + SyncForm.** Sidebar tablet ~145px, nhãn chỉ còn từ
   đầu ("Quan ly", "Thong tin", "Cai dat he"…), không xuống dòng; tên
   người dùng topbar cắt; không có `meta viewport` trong DOM (xác nhận
   SSO-06); carousel chồng thẻ thứ ba; footer đè đáy cột trái. SyncForm:
   thanh bước rộng 4000px gây tràn thật +6px, nhãn bị cắt ký tự đầu,
   điều hướng desktop không đạt networkidle trong 60s.
9. **Dashboard.** Herp home: không h1/topbar/sidebar/breadcrumb/footer/
   meta viewport; 5 KPI đều 0, không điểm so sánh; icon thẻ giá là ¥
   thay vì ₫; cảnh báo 0 dùng màu đỏ; panel biểu đồ tròn trống không
   trạng thái rỗng; mobile/tablet giữ 5 thẻ/hàng → bóp, nhãn vỡ/cắt;
   Highcharts cắt phải (x=604 mobile 390, x=849 tablet 768). MRMS/EMR
   home mobile: KPI/biểu đồ/tab chồng lấn. OR home: không có shell
   (danh sách liên kết trần).
10. **Toolbar/nút không nhất quán.** Kho (Herp): cả 5 nút đều
    `btn-primary`, reload chỉ có icon, không thấy chân lưới/pager/Xóa
    lọc. HOSP/Base Web: 2 nút cùng primary ("Tạo mới" + "Sửa") cạnh
    nhau, "Sửa" bật dù chưa chọn dòng. Nút toolbar Herp mobile ~30px
    (< 44px chạm). Nút F6/F7 dùng mã phím thay vì động từ nhiệm vụ.
    DaySett: nút icon làm mới không nhãn.

### Trung bình / ghi nhận
11. **Trang đăng nhập 3 template không thống nhất** (SSO/MRQC/Sett);
    Sett đăng nhập sạch nhất. Login button có trạng thái loading nhưng
    không có phản hồi timeout (ghi chú phương pháp, chưa tính lỗi app).
12. **Console:** 0 lỗi gần như mọi nơi, trừ: EMR Main `top.layer is
    undefined` ×2 (chỉ trang này); PDS tablet lỗi tải font woff2 1 lần.
13. **Route thật đã xác minh:** EMR `MRHomePage/Index` không tồn tại
    (trả JSON lỗi), route thật là `Main`. StockQuery, ChargeCategory,
    SysGlobalConfig, Organize: 404 ban đầu là do đoán sai route, không
    phải menu chết — route đúng đã ghi trong báo cáo PDS+Base.
14. **Trang báo cáo SSRS (Herp):** đúng tinh thần §6.2/§11 (không bịa dữ
    liệu, không 404) — khối Windows-only, ngoài redesign.

## 3. Hồ sơ chiến lược responsive A/B/C cho từng lưới (từ audit)

- **A (cuộn ngang + cột neo):** MrDept, MrFee, OPStaff, NursingRecord
  (neo Số nhập viện/Họ tên/Ngày), DaySett nội trú (đã cuộn ngang nhưng
  thiếu cột neo), Kho Herp (cần neo "Tên kho"), Mua hàng Herp (cần neo
  "Số đơn hàng" + sửa để cột phải tới được), StockQuery, ChargeCategory,
  SysGlobalConfig (cân nhắc C cho SysGlobalConfig).
- **B (dòng thành thẻ):** PatientInHosList (tra cứu từng bệnh nhân, đã có
  bằng chứng mobile), BookkeepInHos (nhập/xử lý từng chứng từ; lưu ý
  lưới `rowNum = -1`, không phân trang), PDS-03 (dự phòng A+frozen).
- **Chưa gán:** các lưới còn lại trong 22 khu vực Sett (chưa audit hết);
  OpApply: xếp dọc pane trên mobile.
- **Dashboard:** 2 cột mobile theo §4.3 (Herp home hiện giữ 5 cột).

## 4. Tóm tắt từng app (một dòng mỗi app, chi tiết trong báo cáo gốc)

- **SSO:** login chồng tiêu đề + mobile tràn; portal tablet cắt nhãn +
  không hamburger; SyncForm tràn + nhãn cắt; Notice đã sửa ở sweep
  trước, audit nội bộ đủ.
- **MRQC:** login tràn desktop; home trần (câu hỏi trang tạm, xem §5);
  MedicalCenter + Apply đủ, Apply mobile vỡ nặng.
- **Sett:** login sạch nhất; home trần 3 viewport; DaySett/Patient/
  Bookkeep đủ cả mobile (sau catch-up); Bookkeep timeout đợt trước là
  sự cố rải rác, không phải treo thật (nhưng mới 1 lần thành công/
  viewport nên nếu tái hiện thì đối chiếu log Sett tại thời điểm đó).
- **MRMS:** home mobile không drawer, không hamburger; MrDept/MrFee đủ.
- **OR:** home không shell; OPStaff mất cột mobile; chuỗi đơn thuốc đủ.
- **EMR:** home mobile không drawer; Main route thật đã xác minh;
  NursingRecord không cuộn được (cao nhất nhóm này).
- **CIS:** đã audit đủ từ trước (trừ trang truyền dịch jQuery 1.10.2);
  chi tiết trong báo cáo CIS+Herp.
- **Herp:** home nhiều lỗi nhất (xem §2.9); kho/mua hàng/chuyển kho đủ.
- **PDS:** home iframe bóp 170px (hệ thống, xem §2.2); PDS-03 + kho đủ.
- **Base HOSP / Base Web:** home cùng shell lỗi với PDS; form + lưới đủ;
  tab title mặc định; logo Base Web home là ảnh chứa chữ Trung.

## 5. Quyết định điều phối đã chốt (ghi rõ, có thể đảo ngược, nêu lại ở báo cáo cuối)

1. **Fragment là thiết kế, không bọc thêm shell.** Trang nội bộ mở trực
   tiếp không có shell vì portal iframe cấp shell — redesign KHÔNG bọc
   thêm shell cho fragment; sửa bố cục nội dung fragment + sửa shell
   portal (sidebar/hamburger/drawer). Trang chủ trần Sett/MRQC được xử
   lý theo hướng "standalone responsive tối thiểu": thêm `meta
   viewport`, sửa tràn, đặt tiêu đề nhiệm vụ — không ép full shell.
2. **Footer vendor tiếng Trung VẪN list-only** (quy tắc chương trình,
   không tự viết lại khi chưa có quyết user). Danh sách đầy đủ ở §7.
3. **Bookkeep timeout đợt trước là sự cố rải rác** (lần này tải được cả
   2 viewport, 0 console error, 0 request lỗi) — không coi là treo thật,
   nhưng chưa khẳng định "không bao giờ timeout".
4. **Ảnh audit nằm ở 3 vị trí** (xem §6) — không di chuyển để khỏi gãy
   link sandbox trong báo cáo đã commit. Chặng 5 chụp cuối bắt buộc
   một vị trí chuẩn duy nhất.

## 6. Vị trí ảnh audit (gitignore, không commit)

- `~/workspace/shots-redesign/` (workspace root): sso, mrqc, sett, pds,
  hosp, baseweb + 2 ảnh swagger center.
- `~/workspace/openhis-core/shots-redesign/`: emr, mrms, or +
  supplement.json.
- `~/workspace/openhis-core/scripts/redesign/shots-redesign/`: cis, herp
  + probe.json.

## 7. Danh sách chuỗi tiếng Trung còn lại — CHỈ LIỆT KÊ, không viết lại

Footer/login: SSO login + portal, MRQC login, Sett login, MRMS/OR/EMR
login, EMR home, PDS login+home (2 chuỗi dài), HOSP login+home (wording
không thống nhất login vs home), Base Web login (chuỗi ngắn), Base Web
home logo là ảnh. Khác: placeholder tìm kiếm SyncForm (`搜索`), thông
báo lỗi đăng nhập Sett/MRQC (tiếng Trung) trong khi SSO đã tiếng Việt,
nhãn "WeChat" (Base Organize Form), ô "Quản trị viên" chứa chuỗi Trung
trong **dữ liệu demo** (thuộc chặng dữ liệu, không phải chuỗi giao diện).
Thông báo lỗi đăng nhập Sett trong log middleware cũng tiếng Trung.

## 8. Khoảng trống audit còn lại — chặng 3 phải xử lý, không được bịa

- Soi bố cục thanh lọc DaySett ở 390px (mobile: ô ngày không thấy trong
  ảnh viewport dù nhãn có trong DOM; tablet thì thấy rõ).
- Cuộn ngang thực tế trong wrapper lưới Herp (mới thấy cấu trúc, chưa
  cuộn thử); hành vi trong iframe portal SSO (chưa kiểm chứng).
- Nút nguy hiểm/modal/toast, pager nhiều trang, chuỗi dấu xếp chồng §7
  chuyên biệt (chữ Việt trong ảnh đã xem đều nguyên vẹn, không thấy cắt
  dấu — nhưng chưa có quét chuyên biệt).
- Trạng thái lọc đang áp dụng, thông báo/xác nhận (§9.6), trạng thái
  active menu khi ở trang con (audit chỉ đọc, không submit/bấm lọc nên
  chưa đo được).
- Các lưới còn lại trong 22 khu vực Sett chưa gán chiến lược A/B/C.

## 9. Ngoài phạm vi redesign (nhắc lại, không đụng tới)

Mâu thuẫn receipt-prefix; schema thiếu/bảng không tồn tại; 22 quyết định
mở từ sweep cuối (CTR-05, OR DeleteForm, credential client nội bộ, tên
bệnh viện thật, xoay secret, Debug→Release, Sett ghi 184 stub…); khối
Windows-only; dữ liệu thật của bệnh viện; tài khoản demo/secret.
