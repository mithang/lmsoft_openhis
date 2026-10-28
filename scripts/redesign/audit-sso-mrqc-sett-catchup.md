# Audit BẮT KỊP — SSO tablet + Sett mobile/tablet + BookkeepInHos

> **Báo cáo catch-up, chỉ làm các mục còn thiếu của đợt re-audit trước.**
> Đọc kèm `audit-sso-mrqc-sett-internal.md` (các trang nội bộ đã xong,
> KHÔNG làm lại ở đây) và `DESIGN.md` §9/§10.
> Worker này KHÔNG sửa code, KHÔNG commit, KHÔNG làm lại phần đã xong.
> Phương pháp: Playwright + Firefox local, viewport desktop 1280×800,
> mobile 390×844, tablet 768×1024. Mobile = giả lập viewport, KHÔNG phải
> app native.
> Quy tắc tuân thủ: prod CHỈ ĐỌC (điều hướng + chụp ảnh, không submit form
> nào ngoài nút đăng nhập bằng tài khoản 000000 duy nhất); KHÔNG ghi
> database (kể cả STG); KHÔNG khởi động/dừng app; KHÔNG sửa file app.
> Ảnh mới (tên khác đợt trước, hậu tố `-cu-`, không ghi đè):
> `~/workspace/shots-redesign/<app>/`. Script đo: `/tmp/audit_catchup.py`,
> log JSON: `/tmp/audit-catchup.jsonl`.

## 0. Kết quả đăng nhập: 4/4 thành công, 0 thất bại, 0 lần thử lại

| Lượt đăng nhập | Kết quả | Ghi chú |
|---|---|---|
| SSO tablet 768 | ✅ Thành công | Vào `/Home/Index`, chụp portal |
| Sett mobile 390 | ✅ Thành công | Vào `/Home/Index`, chụp đủ 4 trang (kể cả Bookkeep) |
| Sett tablet 768 | ✅ Thành công | Vào `/Home/Index`, chụp đủ 3 trang |
| Sett desktop 1280 | ✅ Thành công | Chỉ chạy Bookkeep (các trang desktop khác đã xong đợt trước) |

Kết luận này khớp bối cảnh điều phối viên đã xác minh (000000 không bị
khóa, Locked=0): các lần thất bại rải rác đợt trước (SSO tablet, Sett
mobile) là thất bại theo context, không phải lockout — lần này cả hai
đều đăng nhập thành công ngay lần đầu, không cần thử lại. Không quan sát
thấy thông báo lỗi đăng nhập nào trong đợt này (kể cả thông báo tiếng
Trung của Sett đợt trước — lần này không xuất hiện vì đăng nhập thành
công, không phải vì đã sửa).

## 1. SSO portal ở tablet 768 — BỔ SUNG SSO-04/SSO-11/SSO-12

Ảnh: `~/workspace/shots-redesign/sso/portal-home-cu-tablet.png`.
Đo: scroll 768/768 (không tràn tài liệu), CJK trong body = 25,
console = 0 (đo trong phiên; không ghi console hook riêng cho trang này
ngoài hook chung), meta viewport = null, h1 = null, 0 ứng cử viên
hamburger.

**SSO-13 — Sidebar tablet: nhãn menu bị cắt, không xuống dòng (xác nhận SSO-04)**
- Sidebar ở 768px là cột hẹp cố định (~145px), KHÔNG phải drawer.
  Nhãn menu chỉ còn từ đầu: "Quan ly", "Quan ly", "Thong tin",
  "Quan ly", "Quan ly", "Cai dat he", "Quan ly", "Tram y ta" — phần còn
  lại của nhãn bị cắt, không xuống dòng, không tooltip thấy được trong
  ảnh. Trên desktop (SSO-11) cùng sidebar hiển thị đủ nhãn — vậy đây
  đúng là vấn đề riêng breakpoint tablet, không phải mọi viewport.
- Topbar: tên người dùng vẫn bị cắt "Quan tri vien【0000…" (như SSO-11).
- Không thấy nút hamburger/drawer nào (quét DOM = 0 ứng cử viên) —
  nhất quán SSO-12: ở tablet, sidebar vừa không thu gọn được (không có
  nút mở/đóng thấy được), vừa không đủ rộng để đọc nhãn.
- Checklist: §9.1, §9.2, §9.7. Mức: **High** (điều hướng ở tablet không
  đọc được nhãn menu).
- Phần còn lại của portal ở tablet: WorkSpace render được (không trắng):
  carousel 3 thẻ xếp dọc đủ chiều rộng, đọc được ("hệ thống thanh toán",
  "hệ thống hợp tác y tế - điều dưỡng", "hệ thống nhà thuốc - kho thuốc");
  "truy cập nhanh" 2 cột đọc được; bảng "bàn làm việc - phân tích thống
  kê" chỉ thấy 3 cột đầu trong viewport, các cột sau cần cuộn ngang.
  Footer vendor tiếng Trung xuất hiện ở cuối cột sidebar, phía trên
  thanh tab (chỉ liệt kê, không tự viết lại — xem §5).

## 2. Sett Home ở mobile + tablet — BỔ SUNG Sett-04

Ảnh: `~/workspace/shots-redesign/sett/home-cu-mobile.png`,
`~/workspace/shots-redesign/sett/home-cu-tablet.png`.
Đo cả 2 viewport: scroll = đúng viewport (390/390, 768/768), không phần
tử tràn, CJK = 0, h1 = null, meta viewport = null.

**Sett-09 — Home mobile + tablet: sạch, một cột, nhưng vẫn là trang trần không shell**
- Nội dung giống hệt desktop (Sett-04): tiêu đề "Sett — Hệ thống quản
  lý thu phí" + đoạn giới thiệu + 4 nhóm liên kết (Nghiệp vụ thu phí /
  quyết toán; Bệnh nhân; Báo cáo / truy vấn / DRG / BHYT; Hệ thống /
  kỹ thuật) + "Đăng xuất" cuối trang (thấy ở tablet; mobile thì nhóm
  cuối nằm dưới nếp gấp — ảnh viewport không tới đó, không khẳng định
  thiếu).
- Mobile: tiêu đề nhóm xuống dòng tự nhiên ("Nghiệp vụ thu phí / quyết
  toán" thành 2 dòng), liên kết xếp 1 cột, đọc được, không tràn, không
  cắt. Tablet: cùng bố cục, đủ chiều rộng, không lỗi mới.
- Vẫn không có topbar/sidebar/footer, vẫn không có `<h1>` (tiêu đề lớn
  là phần tử thường, không phải h1), vẫn lộ tên area kỹ thuật trong
  ngoặc — tức Sett-04 không phải vấn đề riêng desktop; quyết định của
  điều phối viên về "trang chủ trần có phải trang tạm không" (mục 4
  trong §6 đợt trước) vẫn còn nguyên và nay áp cho cả 3 viewport.
- Checklist: §9.1, §9.8. Mức: như Sett-04 (High nếu là trang tạm; chưa
  kết luận).

## 3. Sett DaySett nội trú ở mobile + tablet — BỔ SUNG Sett-05

Ảnh: `~/workspace/shots-redesign/sett/daysett-inpatient-cu-mobile.png`,
`~/workspace/shots-redesign/sett/daysett-inpatient-cu-tablet.png`.
Đo: jqGrid 5.1.0, 7 cột (rn, Id, bcje, fphs, CreatorName, LastJsTime,
CreateTime), records = 0, rowNum = 15, locale pager tiếng Việt
(recordtext/emptyrecords/pgtext như desktop); lưới `#gridList` rộng
1027px ở cả 2 viewport (wrapper cuộn, tài liệu không tràn: 390/390,
768/768); CJK = 0; không h1, không meta viewport.

**Sett-10 — DaySett mobile: ô lọc ngày KHÔNG nhìn thấy trong ảnh viewport**
- Ở tablet, thanh lọc thấy rõ: nút icon làm mới (không nhãn) + "Ngày
  quyết toán:" + ô bắt đầu + "—" + ô kết thúc "2026-10-28 14:23:05".
- Ở mobile, ảnh viewport chỉ thấy nút icon làm mới rồi tới ngay tiêu
  đề lưới — các ô ngày không nhìn thấy được (dù nhãn "Ngày quyết toán:"
  vẫn có trong DOM text). Không khẳng định ô ngày "bị ẩn" hay "nằm
  ngoài viewport do bố cục": chỉ khẳng định người dùng mobile không
  thấy/không dùng được bộ lọc ngày trong ảnh đã chụp — chặng redesign
  cần soi lại bố cục thanh lọc ở 390px trước khi kết luận.
- Checklist: §9.3, §9.7. Mức: **High nếu bộ lọc thật sự không tới được
  trên mobile** (lọc theo ngày là chức năng chính của trang); ghi là
  "chưa xác minh cơ chế" chứ không gán chắc là lỗi ẩn phần tử.

**Sett-11 — DaySett mobile + tablet: cột đầu vẫn bị cắt, rỗng vẫn tô đỏ (như desktop)**
- Tiêu đề cột đầu tiên bị cắt ở cạnh trái trên cả 2 viewport (trong
  ảnh chỉ còn "…này lần Số tiền qu…") — giống Sett-05 trên desktop,
  không phải vấn đề riêng desktop.
- Trạng thái rỗng: chữ đỏ "Không tìm thấy dữ liệu liên quan!" + chân
  lưới "Không có dữ liệu hiển thị" + pager "Trang 1 trên tổng số
  0 trang" (tablet thấy đủ; mobile thấy đủ pager, nút F6/F7 nằm dưới
  nếp gấp nên không thấy trong ảnh viewport — không kết luận thiếu).
  Tablet thấy đủ nút "F6:Xác nhận quyết toán" / "F7:Hủy quyết toán"
  (nhãn mã phím như Sett-05).
- Lưới không có cột neo: cuộn ngang sẽ mất cột định danh — hồ sơ
  chiến lược A cho DaySett ở §5 đợt trước được giữ nguyên, nay có thêm
  bằng chứng mobile/tablet (thiếu cột neo ở cả 3 viewport).
- Checklist: §9.4, §9.5, §9.6/§6.2. Mức: Medium (cột đầu cắt + rỗng tô
  đỏ); Low (nút icon không nhãn, nhãn F6/F7).

## 4. Sett PatientInHosList ở mobile + tablet — BỔ SUNG Sett-06

Ảnh: `~/workspace/shots-redesign/sett/patientinhoslist-cu-mobile.png`,
`~/workspace/shots-redesign/sett/patientinhoslist-cu-tablet.png`.
Đo: jqGrid 5.1.0, 12 cột (rn, blh, xm, xb, nl, zyh, cw, ryzd, ryrq,
cyrq, csrq, zjh), records = 0, rowNum = 15, locale pager tiếng Việt;
lưới `#gridList` rộng 1335px ở cả 2 viewport (wrapper cuộn, tài liệu
không tràn: 390/390, 768/768); CJK = 0; không h1, không meta viewport.

**Sett-12 — PatientInHosList mobile + tablet: lọc dùng được, lưới không cột neo, rỗng tô đỏ**
- Thanh lọc dùng được ở cả 2 viewport: ô "Họ tên / Số nhập viện" +
  nút icon tìm (không nhãn) — mobile ô tìm chiếm gần hết chiều rộng,
  tablet chiếm ~1/3, đều đọc được, không vỡ.
- Mobile chỉ thấy 4 cột đầu (Số hồ sơ / Họ tên / Giới tính / Tuổi),
  tablet thấy 7 cột đầu (thêm Số nhập viện / Số giường / Chẩn đoán
  nhập viện); các cột sau cần cuộn ngang, không cột neo — đúng mẫu
  "cuộn mà mất cột định danh" đã ghi ở Sett-06.
- Trạng thái rỗng giống desktop: chữ đỏ "Không tìm thấy dữ liệu liên
  quan!" + "Không có dữ liệu hiển thị" + pager "Trang 1 trên tổng số
  1 trang".
- Hồ sơ chiến lược B cho lưới này (§5 đợt trước: tra cứu từng bản ghi
  → dòng thành thẻ) được giữ nguyên — nay có bằng chứng mobile: thẻ
  phải giữ cùng thứ tự trường + nhãn với 12 cột desktop, vì hiện tại
  người dùng mobile phải cuộn ngang qua 1335px mới tới được các cột
  ngày/số giấy tờ.
- Checklist: §9.4, §9.5, §9.6/§6.2. Mức: Medium (rỗng tô đỏ, không cột
  neo); Low (nút icon không nhãn, không h1/meta).

## 5. Sett BookkeepInHos ở desktop + mobile — GIẢI QUYẾT Sett-07 (không còn là blocker)

### 5.1. Chẩn đoán tải trang: LẦN NÀY TẢI ĐƯỢC, NHANH, SẠCH — timeout đợt trước không tái hiện

| Chỉ số | Mobile 390 | Desktop 1280 |
|---|---|---|
| URL cuối | `/HospitalizationManage/BookkeepInHos/Index` ✅ (tới nơi) | Cùng URL ✅ |
| `domcontentloaded` | 0,5s | 1,2s |
| `networkidle` | đạt sau 1,1s | đạt sau 2,4s |
| Tổng thời gian (kèm chờ 3s rồi chụp) | 4,2s | 6,4s |
| Console errors | 0 | 0 |
| Request failed / HTTP ≥ 400 | 0 / 0 | 0 / 0 |
| Lỗi Bookkeep trong log Sett | Không kiểm tra log trong đợt này (đợt trước đã ghi: log Sett không có dòng lỗi Bookkeep nào) | — |

Kết luận chẩn đoán: **trang BookkeepInHos tải được bình thường trong
đợt này ở cả 2 viewport; timeout 60s ×2 + 1 lần Firefox đóng ở đợt
trước là sự cố rải rác theo context (cùng họ với các lần đăng nhập thất
bại rải rác), không phải treo thật của trang.** Bằng chứng là 1 lần
thành công mỗi viewport — đủ để gỡ trạng thái "Blocker cho hồ sơ
Bookkeep" và cho phép audit UI dưới đây, nhưng chưa đủ để khẳng định
"không bao giờ timeout": nếu chặng redesign gặp lại timeout ở trang
này, cần đối chiếu log Sett tại thời điểm đó thay vì mặc định là lỗi
trang. Không tự sửa gì (đúng phạm vi catch-up).

Ảnh: `~/workspace/shots-redesign/sett/bookkeepinhos-cu-desktop.png`,
`~/workspace/shots-redesign/sett/bookkeepinhos-cu-mobile.png`.
Đo: jqGrid 5.1.0, 20 cột (rn, newid, sfxm, sfxmmc, dlmc, dw, dj, sl,
je, ksmc, ysmc, tdrq, ks, ys, dl, zfxz, zfbl, yzlx, yfdm, cls),
records = 0, **rowNum = -1 (không phân trang)**, locale pager tiếng
Việt; CJK = 0; không h1, không meta viewport.

**Sett-13 — Bookkeep desktop: form nhập đầy đủ, đọc được; lưới vừa viewport; trạng thái rỗng trắng trơn**
- Form "Thông tin bệnh nhân nhập viện" (Mã nhập viện + nút "Truy vấn",
  Họ tên / Giới tính / Tuổi / Số giấy tờ / Chẩn đoán nhập viện / Số
  điện thoại / Ngày sinh / Ngày nhập viện — đều trống vì chưa chọn
  bệnh nhân, read-only nên không submit) + form ghi sổ (radio "Mục /
  Vật tư", *Ngày ghi sổ mặc định 2026-10-28 14:23:19, *Bác sĩ, *Khoa,
  Khoa thực hiện mặc định "Khoa Noi", *Mục thu phí, *Số lượng, Đơn vị,
  Đơn giá(đồng) — dấu * đánh dấu bắt buộc, nhất quán). Không submit,
  không ghi DB.
- Lưới `#gridList` rộng 1237px, vừa trong viewport 1280 (không tràn,
  không cần cuộn ngang ở desktop); tiêu đề cột đầy đủ (Mã / Mục thu
  phí / Loại phí / Đơn vị / Đơn giá / Số lượng / Số tiền / Khoa /
  Bác sĩ / Ngày ghi…).
- Trạng thái rỗng: lưới chỉ có hàng tiêu đề + vùng trắng, **không có
  dòng "Không có dữ liệu hiển thị" nhìn thấy được** trong ảnh (khác
  DaySett/PatientInHosList tô đỏ) — tức trạng thái rỗng ở đây là
  "trắng trơn, không hướng dẫn", vi phạm §6.2/§9.4 theo hướng ngược
  lại với rỗng tô đỏ.
- Nút: DOM có "F2:Thêm / F3:Sửa đổi / F6:Xóa"; ảnh viewport thấy thêm
  cụm nút góc dưới-phải hiển thị "F4:", "F7:Mẫu", "F8:" (nhãn chỉ còn
  mã phím, phần chữ sau dấu hai chấm bị cắt hoặc vốn đã ngắn — không
  soi thêm DOM nên ghi đúng những gì ảnh cho thấy). Nhãn mã phím là
  vấn đề §9.5 đã biết (như F6/F7 ở DaySett).
- Checklist: §9.3 (đạt phần form desktop), §9.4 (rỗng không hướng dẫn),
  §9.5 (nhãn mã phím). Mức: Medium (rỗng trắng trơn); Low (nhãn phím).

**Sett-14 — Bookkeep mobile: form nhập VỠ NẶNG, không dùng được (lỗi mobile rõ nhất đợt catch-up)**
- Bảng form rộng 1125px trong viewport 390: các ô nhập bị bóp thành
  khe dọc hẹp, không đọc/không nhập được theo ảnh (ô *Ngày ghi sổ,
  *Bác sĩ, *Khoa đều chỉ còn vệt dọc); nhãn bên phải bị cắt ở cạnh
  viewport ("Khoa thực hi…", "Đơn giá(đ…").
- Lưới `#gridList` rộng 1237px, mobile chỉ thấy 2 cột đầu (Mã / Mục thu
  phí), các cột sau cần cuộn ngang, không cột neo.
- Cụm nút góc dưới-phải vẫn thấy ("F4:", "F7:Mẫu", "F8:") nhưng form
  phía trên đã vỡ nên nút không cứu được luồng nhập liệu.
- Checklist: §9.3 (form mobile), §9.7, §4.2. Mức: **High** (nhập ghi sổ
  không dùng được trên mobile — chức năng chính của trang).
- Hồ sơ chiến lược (nay ĐÃ GÁN được, gỡ "CHƯA GÁN" ở §5 đợt trước):
  **B (dòng → thẻ / form 1 cột)** — nhiệm vụ chính là nhập/xử lý từng
  chứng từ ghi sổ theo từng bệnh nhân, không phải duyệt loạt; form
  mobile phải thành 1 cột nhãn-trên-trường, các dòng chi tiết đã nhập
  render dạng thẻ giữ đủ trường chính (mục thu phí, số lượng, đơn giá,
  số tiền, khoa, bác sĩ, ngày ghi). Lưu ý rowNum = -1 (không phân
  trang): thẻ phải xử lý được danh sách dài, không giả định chỉ 15
  dòng/trang.

## 6. Đối chiếu checklist §9 — chỉ phần catch-up mới đo được

| Mục §9 | SSO portal tablet | Sett Home mob/tab | Sett DaySett mob/tab | Sett Patient mob/tab | Sett Bookkeep desk/mob |
|---|---|---|---|---|---|
| 9.1 Header | ❌ Nhãn sidebar cắt, tên cắt, không meta, footer tiếng Trung (chỉ liệt kê) | ❌ Trần không shell, không h1 | ❌ Không h1/meta | ❌ Không h1/meta | ❌ Không h1/meta |
| 9.2 Sidebar | ❌ Không hamburger/drawer thấy được; sidebar hẹp cắt nhãn | N/A (fragment; shell = portal) | N/A | N/A | N/A |
| 9.3 Form | N/A (portal) | N/A | ❌ Mobile: ô ngày không thấy trong ảnh | ✅ Ô tìm dùng được cả 2 viewport | ✅ Desktop đạt; ❌ Mobile vỡ nặng |
| 9.4 Lưới | N/A (dashboard) | N/A | ⚠️ 1027px, không cột neo; pager VI ✅; rỗng tô đỏ | ⚠️ 1335px, không cột neo; pager VI ✅; rỗng tô đỏ | ⚠️ Desktop 1237px vừa viewport; mobile 1237px không cột neo; rỗng trắng trơn |
| 9.5 Nút | ⚠️ Icon topbar chưa xác minh chức năng | N/A | ❌ Nút icon không nhãn; nhãn F6/F7 là mã phím | ❌ Nút icon không nhãn | ❌ Nhãn chỉ còn mã phím (F2/F3/F6/F4/F7/F8) |
| 9.6 Thông báo | Chưa kiểm chứng (không thao tác) | Chưa kiểm chứng | ❌ Rỗng tô đỏ như lỗi | ❌ Rỗng tô đỏ như lỗi | ❌ Rỗng trắng trơn, không hướng dẫn |
| 9.7 Mobile | ❌ Sidebar tablet không đọc được nhãn | ✅ Không tràn, 1 cột đọc được | ❌ Lọc ngày không thấy (mobile) | ⚠️ Cuộn ngang mất cột định danh | ❌ Form mobile vỡ (High) |
| 9.8 Ngôn ngữ | ❌ Footer tiếng Trung (chỉ liệt kê); body còn lại 0 CJK ngoài footer | ✅ Body 0 CJK | ✅ Body 0 CJK; pager VI | ✅ Body 0 CJK; pager VI | ✅ Body 0 CJK; pager VI |

## 7. Khoảng trống bằng chứng còn lại sau catch-up

1. **Đã lấp đầy:** SSO tablet (portal), Sett mobile + tablet (Home,
   DaySett, PatientInHosList), Sett Bookkeep desktop + mobile. Cùng
   với đợt trước, nhóm SSO/MRQC/Sett nay đã có ảnh nội bộ ở cả 3
   viewport cho các trang trong phạm vi (trừ MRQC tablet — không bắt
   buộc theo §6 đợt trước, desktop + mobile đã đủ).
2. **Còn mở, chuyển cho điều phối viên:**
   - Cơ chế khiến ô lọc ngày DaySett không nhìn thấy ở mobile 390
     (Sett-10): cần soi bố cục thanh lọc trước khi chặng redesign kết
     luận là ẩn hay tràn.
   - Quyết định trang chủ trần (Sett-04/Sett-09, MRQC-02): nay áp cho
     cả 3 viewport của Sett Home.
   - Footer dùng chung (§3.1/§7): danh sách trang còn chuỗi tiếng Trung
     trong phạm vi catch-up: portal SSO tablet (footer, chỉ liệt kê).
   - Bookkeep timeout đợt trước: nay kết luận là sự cố rải rác, không
     còn là blocker; nếu tái hiện thì đối chiếu log Sett tại thời điểm
     đó.
3. **Không thay đổi:** mâu thuẫn receipt-prefix (ngoài phạm vi), tên
   bệnh viện thật, dữ liệu thật, tài khoản demo/secret, Windows-only.
   Không có code nào bị sửa, không có commit nào được tạo trong chặng
   này.

---

*Hết báo cáo audit bắt kịp SSO/MRQC/Sett. Trạng thái tổng thể sau
catch-up: toàn bộ mục còn thiếu đã có ảnh + số đo; phát hiện mới quan
trọng nhất là Bookkeep mobile vỡ form (High) và sidebar portal tablet
cắt nhãn (High); Bookkeep không còn là blocker tải trang.*
