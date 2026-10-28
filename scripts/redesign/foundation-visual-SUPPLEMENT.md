# Kiểm chứng VISUAL nền tảng — BỔ SUNG (a)–(d) (điều phối viên tự chạy trên STG)

> Bổ sung cho `foundation-visual-REPORT.md` (commit 495c97c8), phần trước đó
> bị chặn vì worker không có mật khẩu. Điều phối viên chạy bổ sung này
> bằng mật khẩu demo đã biết từ bộ nhớ chương trình (tài khoản 000000),
> login STG duy nhất mỗi app, chỉ đọc — không submit nghiệp vụ, không ghi
> DB nào (kể cả STG, ngoài session login). Ngày 2026-10-28 (ICT).
> Mobile = giả lập viewport (Playwright + Firefox local), KHÔNG app native.

## 0. Kết luận

| Hạng mục | Kết quả |
|---|---|
| (a) Drawer portal mobile | ✅ ĐẠT đầy đủ — mở/đóng 4 đường, focus đúng quyết định điều phối |
| (b) Lưới đo lại theo viewport | ✅ ĐẠT (1440→899→1438) — NHƯNG wrapper cuộn `.rd-grid-scroll` ❌ CHƯA có mặt trên trang nào → việc chặng sau |
| (c) Form mobile một cột | ✅ ĐẠT — nhãn trên trường, bảng 368px trong viewport 390 |
| (d) Lưới rỗng không còn đỏ | ✅ ĐẠT — chữ xám trung tính, căn giữa, nền nhạt (đo computed style live) |
| DI b73a1026 (kiểm lại) | ✅ 0 lỗi `Unable to resolve service` trên cả SSO STG + PDS STG |
| Login STG | ✅ SSO 15208 + PDS 15237 đều "Đăng nhập thành công" (curl + trình duyệt) |

## 1. Môi trường bổ sung

- Stack STG dựng lại từ script của worker (`~/workspace/foundation-visual-scratch/run-*-visual-stg.sh`)
  + `run-baseapi-stg.sh`: Redis STG 6380 (daemon PID 4048), BaseAPI STG 17001 (PID 4060),
  SSO STG 15208 (PID 4139, scratch Debug từ HEAD), PDS STG 15237 (PID 4171, scratch Debug từ HEAD).
  SQL 1433 sống suốt phiên (đã khôi phục bằng recovery.sh trước đó sau một lần VM restart
  làm chết toàn bộ 26 cổng — recovery exit 0, đủ 26 cổng, 11 trang login prod 200,
  login prod SSO thành công + SSOIndex 200).
- Build scratch tái sử dụng của worker (HEAD không đổi từ b73a1026): KHÔNG build lại,
  KHÔNG publish Release từ cây prod (bài học sự cố §0 của báo cáo gốc).
- Dọn dẹp cuối phiên: kill đúng PID 4048/4060/4139/4171; kiểm tra 6380/17001/15208/15237
  đều down; prod SSO 5208 + PDS 5237 vẫn 200. Cookie curl tạm trong /tmp đã xóa.

## 2. (a) Drawer portal — ĐẠT

Trang `/Home/SSOIndex` sau đăng nhập, viewport 390×844:

- Nút `#rd-nav-toggle`: box 44×44 tại (15,3), `display: flex`, `visibility: visible`
  (đúng kích thước chạm tối thiểu 44px); khi đóng: `body.rd-drawer-open = false`,
  overlay box 0×0.
- Bấm hamburger (JS click): `rd-drawer-open = true`, overlay phủ toàn viewport 390×844,
  `aria-expanded = true`. Ảnh `sso-portal-mobile-drawer-open.png`.
- Đóng bằng **Esc**: closed, `aria-expanded = false`, focus trả về `#rd-nav-toggle`. ✅
- Đóng bằng **nút `.rd-drawer-close`**: closed, focus trả về `#rd-nav-toggle`. ✅
- Đóng bằng **click overlay phía phải drawer (x=350, ngoài vùng drawer 280px)**:
  closed, focus trả về `#rd-nav-toggle`. ✅
- Đóng bằng **bấm link menu trong drawer**: closed, focus về BODY (điều hướng/mở tab)
  — đúng quyết định điều phối giữ `close(false)` cho trường hợp này. ✅
- Desktop 1280: toggle `display: none`, box 0×0, drawer đóng — cơ chế desktop không bị
  drawer can thiệp. Ảnh `sso-portal-desktop.png`.
- Ghi chú phương pháp: lần probe đầu dùng Playwright `page.click` cho overlay/toggle
  khi drawer đang mở đã timeout vì nút hamburger nằm dưới vùng drawer phủ
  (drawer 280px che toggle tại x=15–59) và click giữa overlay rơi vào vùng drawer —
  đó là giới hạn của phương pháp click, KHÔNG phải lỗi code; probe lần 2 dùng
  JS click + click chuột tại tọa độ ngoài drawer + phím Esc đã xác nhận đủ 4 đường.

## 3. (b) Lưới PDS — đo lại ĐẠT, wrapper cuộn CHƯA có (việc chặng sau)

Trang `Medicine/SyncDeptDrug` (layout `_Form`, đã nạp tokens + grid.js — probe `true` cả hai):

- Viewport 1440 → lưới 1440px; 900 → 899px; trở lại 1440 → 1438px.
  Lưới bám chiều rộng container thật, không kẹt ở kích thước cũ, không bóp méo.
- Mobile 390: lưới 390px, đúng viewport.
- **Wrapper `.rd-grid-scroll`: `false` trên TẤT CẢ trang đã probe**
  (SyncDeptDrug, PreViewInfo, ChooseCf). Nguyên nhân: helper `makeGridScrollable`
  chỉ là hàm chờ được gọi, chưa có trang nào gọi nó; foundation mới chỉ nạp asset
  + tự chạy `refreshGridWidths`. Hệ quả: lưới rộng hơn viewport trên mobile vẫn
  chưa có vùng cuộn ngang riêng + chưa có cột neo (0 lưới PDS khai `frozen:true`,
  như báo cáo gốc đã nêu).
- **Quyết định chặng sau:** tự động áp wrapper cho mọi `.ui-jqgrid` trong
  `redesign-grid.js` lúc tải (kèm cơ chế loại trừ nếu một lưới nào đó vỡ bố cục),
  thay vì sửa từng trang gọi helper; đồng thời nối `_Index` vào nền tảng
  (xem §6). Không tự sửa ở phiên bổ sung này vì đó là thay đổi code chặng sau,
  cần build + kiểm chứng riêng.

## 4. (c) Form mobile — ĐẠT

Trang `Dictionaries/FypzForm` (layout `_Form`, đã nạp tokens), viewport 390×844:

- `th.formTitle` box [11, 122, 368, 21]; `td.formValue` box [11, 143, 368, 42]:
  trường nằm NGAY DƯỚI nhãn (143 = 122 + 21), cùng lề trái x=11 — đúng một cột,
  nhãn trên trường. Bảng form rộng 368px < viewport 390, không tràn.
- Ảnh `pds-form-mobile.png`.

## 5. (d) Lưới rỗng — ĐẠT (đo computed style live, không chỉ đọc CSS)

Trang `DrugStorage/PreViewInfo` (layout `_Form`, 0 dòng dữ liệu), desktop 1440:

- Phần tử `.ui-jqgrid-bdiv > .unwritten` hiển thị "Không tìm thấy dữ liệu liên quan!"
  với computed style: màu `rgb(107, 114, 128)` (xám trung tính #6b7280),
  `text-align: center`, nền `rgb(248, 249, 250)` (xám nhạt) — **không còn màu đỏ
  của inline style gốc**. Quy tắc trung hòa trong `redesign-tokens.css` có hiệu lực live.
- Wording "chưa có dữ liệu" vs "lọc không khớp" vẫn là việc nội dung chặng sau
  (hiện mọi trạng thái rỗng dùng chung một câu của locale) — chỉ màu/trình bày
  là đạt ở chặng này.
- Quan sát thêm (không phải lỗi foundation): trang PreViewInfo có 1 pageerror
  `can't access property "length", mList is null` — lỗi JS gốc của trang khi dữ liệu
  rỗng, tồn tại độc lập với nền tảng (tokens/grid vẫn nạp và chạy). Ghi nhận cho
  đợt per-app kiểm tra, không sửa ở đây.

## 6. Phạm vi chặng sau (từ kết quả bổ sung này)

1. Nối `_Index.cshtml` vào nền tảng ở mọi app dùng nó (PDS StockQuery/FypzIndex
   và các trang Index tương đương) — nếu không, phần lớn trang lưới thực tế
   vẫn ngoài nền tảng dù `_LayoutIndex`/`_Form` đã nối.
2. Tự động áp `.rd-grid-scroll` cho lưới jqGrid (kèm loại trừ an toàn) + khai
   cột neo `frozen:true` theo hồ sơ A/B/C của audit (PDS/Herp/Base/EMR/MRMS/OR/Sett).
3. Khi 2 việc trên xong, kiểm chứng lại (b) trên một trang `_Index` (StockQuery)
   + một lưới đã khai frozen (EMR NursingRecord hoặc Base HOSP) để đóng cột neo live.

## 7. Ảnh bổ sung (cùng thư mục bắt buộc `shots-redesign/foundation/`)

- `sso-portal-mobile-closed.png`, `sso-portal-mobile-drawer-open.png`,
  `sso-portal-mobile-after-esc.png`, `sso-portal-mobile-after-menu.png`,
  `sso-portal-desktop.png`
- `pds-grid-desktop.png`, `pds-grid-mobile.png`, `pds-form-mobile.png`
- Cộng 6 ảnh của worker: `sso-login-desktop/mobile`, `sso-ssoindex-unauth-desktop/mobile`,
  `pds-login-desktop/mobile` — tổng 14 ảnh foundation.

## 8. Những gì KHÔNG làm

- Không commit code, không sửa source, không đụng run-copy/static host, không ghi DB
  (chỉ GET + login session; 0 submit form nghiệp vụ). Không ghi mật khẩu vào file
  hay báo cáo (mật khẩu chỉ qua biến môi trường tạm thời của phiên chạy probe).
- Không tuyên bố test app native. Footer tiếng Trung chỉ liệt kê.
