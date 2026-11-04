# Design System — OpenHIS Visual UI/UX Modernization

> Chỉ đổi TRÌNH BÀY, giữ nguyên 100% chức năng. Mọi quy tắc dưới đây đều suy ra từ cấu trúc thật đã khảo sát (xem `PORTAL_INVENTORY.md`).
> Nền tảng trước đó: đợt redesign 29/10/2026 đã tạo `scripts/redesign/` (`DESIGN.md`, `ASSET-PIPELINE.md`, `assets/`, `sync-assets.sh`, 1.641 ảnh). Chương trình này XÂY TIẾP trên nền đó — không làm lại, không đổi cơ chế pipeline.

## 1. Cơ chế triển khai (bắt buộc)

1. **Nguồn chuẩn duy nhất**: `scripts/redesign/assets/` gồm `redesign-tokens.css`, `redesign-grid.js`, `redesign-portal.css`, `redesign-portal.js`. Mọi thay đổi thị giác trong repo đều sửa ở đây.
2. **Đồng bộ**: chạy `bash scripts/redesign/sync-assets.sh` để chép sang `wwwroot/css|js/` của 10 app nghiệp vụ + SSO. **Cấm sửa tay bản trong `wwwroot`** — sẽ bị ghi đè ở lần sync sau. Sau sync, kiểm chứng md5 của `redesign-tokens.css` giống hệt nhau ở cả 11 portal (mốc Phase 1: `e14e0ef8f7188b552207c81ec8eab45b`).
3. **Cấm sửa static host 41142** (`Newtouch.HIS.Static`, ngoài repo). Mọi file `framework-ui.css` / `framework-theme.css` / `common.css` / Bootstrap / jqGrid / JS khung đều ở đó; sửa chúng là đổi toàn hệ thống + đổi prod ngay, ngoài phạm vi và ngoài repo.
4. **Thứ tự nạp là hợp đồng**: `redesign-tokens.css` nạp SAU `framework-theme.css` trong `_Index.cshtml`/`_Form.cshtml`, app-local qua `GetStaticResourceScriptUrl(..., false)` nên có `?V=` chống cache. Không đổi vị trí nạp, không đổi tên file, không thêm file CSS mới vào layout nếu chưa chứng minh được thứ tự nạp an toàn bằng screenshot trước/sau.
5. **Razor**: chỉ sửa markup khi thật cần cho trình bày; giữ nguyên mọi ID, name, attribute, Razor expression, Tag Helper, event handler, selector JS. Mọi sửa Razor phải liệt kê trong commit message và đối chiếu diff từng dòng.
6. **Không thêm dependency frontend** (không CDN mới, không thư viện mới, không đổi phiên bản Bootstrap/jQuery/jqGrid). Stack đóng: Bootstrap 3.3.4, jQuery 2.1.1, jqGrid 5.1.0, jQuery UI 1.10.4, FontAwesome 4.7.0.

## 2. Token (kế thừa + mở rộng)

Token hiện có trong `:root` của `redesign-tokens.css` (giữ nguyên tên và giá trị, chỉ mở rộng khi pilot chứng minh cần):

```css
--rd-primary: #337ab7; --rd-success: #5cb85c; --rd-warning: #f0ad4e;
--rd-danger: #d9534f; --rd-info: #5bc0de;
--rd-text: #333333; --rd-text-muted: #6b7280;
--rd-bg-muted: #f8f9fa; --rd-border: #dddddd;
--rd-space-1: 4px; --rd-space-2: 8px; --rd-space-3: 12px;
--rd-space-4: 16px; --rd-space-6: 24px;
--rd-radius: 4px; --rd-radius-card: 6px;
--rd-line-height: 1.5; --rd-line-height-heading: 1.35;
```

Nguyên tắc token của chương trình này (khóa sau pilot, pilot là bằng chứng):

- **Màu ngữ nghĩa lấy theo Bootstrap 3 mặc định** (`#337ab7`…); token `--rd-primary` CHỈ dùng cho thành phần redesign mới, KHÔNG override `.btn-primary` toàn cục (theme skin từng app có thể đã override nút — tránh đổi màu nút đang chạy).
- **Bổ sung có kiểm soát** (tên `--rd-*`, giá trị lấy từ palette đang chạy, không phát minh màu mới): màu nền surface/header bảng, màu viền focus, màu nền hàng hover/chọn của jqGrid, màu nền sidebar/header shell, shadow 2 mức (nhẹ cho card, nhẹ hơn cho dropdown/modal). Mọi token mới phải có đúng 1 nơi định nghĩa và được dùng — không token chết.
- **Khoảng cách bội số 4**; **bo góc 2 mức** (4px cho nút/input, 6px cho card/panel); **line-height tương đối** (không cố định pixel) để dấu tiếng Việt xếp chồng không bị cắt; **`formTitle` 210px** cho nhãn tiếng Việt (khung mặc định 90px sẽ cắt nhãn dài).
- **Mật độ dữ liệu là yêu cầu, không phải tùy chọn**: không tăng padding/chiều cao hàng vượt mức làm giảm số dòng nhìn thấy trên màn hình lâm sàng; whitespace tăng phải có lý do đọc được chứng minh bằng screenshot.

## 3. Thành phần

- **Toolbar / nút**: giữ nguyên class `.toolbar`, thứ tự nút, phím tắt F1–F12 (Herp `_BottomButtonsView`/`_MiddleButtonsView`); chỉ đổi màu nền/viền/bóng/bo góc qua token, giữ nguyên trạng thái hover/focus/active/disabled và thứ tự hiển thị.
- **Form**: `table.form > th.formTitle + td.formValue` là hợp đồng markup; `.formValue` + `.has-error` là selector của JS validate — không đổi tên, không đổi `display`. Input giữ nguyên tên/ID; chỉ đổi viền, nền focus, bo góc, khoảng cách.
- **Bảng jqGrid**: DOM của grid sinh động sau render (`div.unwritten`, `.ui-jqgrid*`, `.ui-jqgrid-btable`, `#gridList/#gridPager`); style qua `redesign-grid.js` + token, không đổi cấu trúc DOM, không ẩn cột, không đổi chiều rộng cột bằng CSS `!important` tràn lan.
- **Card/Panel**: panel Bootstrap 3 + `.panel-title` (line-height heading 1.35); bo góc 6px, shadow nhẹ, tiêu đề rõ hierarchy; ảnh trong vùng nội dung `max-width: 100%` (logo dùng background-image nên không bị ảnh hưởng).
- **Modal**: giữ nguyên hành vi mở/đóng/focus của Bootstrap 3 modal; chỉ đổi viền, bo góc, shadow, khoảng cách header/body/footer.
- **Shell (AdminLTE)**: sidebar `#sidebar-nav`, tab `.menuTab`/`.content-tabs`, iframe `.Newtouch_iframe`, `#page-wrapper`, `#nav-col`, `#theme-wrapper` — chỉ đổi màu nền/viền/typography qua `redesign-portal.css` (SSO) và token; không đổi cấu trúc, không ẩn mục menu, không đổi cơ chế đổi skin (`id="color-skin"`, `localStorage`) — palette mới phải được test cùng cơ chế skin ở MRMS.
- **Trang đăng nhập**: mỗi portal có login view + `login.css` riêng (nội dung khác nhau giữa các portal — không đồng bộ mù); đổi trong phạm vi từng portal, giữ nguyên field/hành vi submit.
- **Trang in / báo cáo / template**: SSRS, phiếu in, template y khoa (EMR), `_PrintCommonView` (Sett) — KHÔNG modernize giao diện tương tác; chỉ sửa nếu chứng minh được không ảnh hưởng bản in.
- **Responsive**: CSS responsive không được ẩn chức năng, không được bỏ phần tử khỏi luồng tab/focus; kiểm chứng ở 2 viewport (desktop + hẹp) bằng screenshot.

## 4. Hướng thị giác

Phong cách enterprise healthcare SaaS: sạch, tối giản, chuyên nghiệp, nhất quán, hierarchy rõ, shadow tinh tế, dễ đọc. Tránh: gradient quá mức, animation quá mức, chi tiết trang trí không cần thiết, card quá to, whitespace quá nhiều làm mất mật độ dữ liệu, quá nhiều màu, phần tử UI không cần thiết. Ưu tiên khả năng đọc dữ liệu lâm sàng và mật độ thông tin.

## 5. Cấm kỵ (selector JS bám vào — không đổi tên, không xóa, không `display:none`)

`.formValue`, `.has-error`, `.ui-jqgrid`, `.ui-jqgrid-btable`, `.ui-jqgrid-bdiv`, `div.unwritten`, `#gridList`, `#gridPager`, `.menuTab`, `.Newtouch_iframe`, `.content-tabs`, `.tabClose*`, `#sidebar-nav`, `#page-wrapper`, `#nav-col`, `#theme-wrapper`, `#color-skin`, `#loadingPage`, `.toolbar`, `.menuItem`, `[data-id]`, `.dropdown-toggle`, `.submenu`, `#btn_bottombutton_f*`, các class mà `$.bindSelect`, `$.reload`, `$.LoadNavTree()` sinh ra hoặc truy vấn. Danh sách đầy đủ theo từng portal nằm trong 3 file danh mục Phase 1 (§ rủi ro) — đối chiếu trước khi sửa selector nào.

## 6. Rủi ro đã biết và cách xử lý

1. **Cache cũ**: tài nguyên static-host không có `?V=`; sau mỗi lần đổi, kiểm chứng bằng screenshot thật (không tin status 200) và hard-refresh phiên chụp.
2. **Thứ tự nạp không đồng nhất** giữa `_Index` và `_Form` (token ở vị trí khác nhau; `common.css` nạp sau token trong `_Index`; `framework-ui.css` nạp lặp 2 lần trong `_Form`): mọi thay đổi phải chụp cả 2 loại trang.
3. **Inline style trong view** và màu hardcode (đặc biệt Herp `*InlineEdit`): ghi nhận, không đuổi theo sửa hết trong một vòng; ưu tiên token phủ được.
4. **PDS không nạp jQuery core trong layout** (chỉ `jquery.choose.js`): xác minh runtime ở Phase 2 trước khi kết luận; không sửa theo suy đoán.
5. **Hai bản sao static host** khác nhau 32 file: Phase 2 đối chiếu với host 41142 đang chạy trước khi kết luận bản nào đang phục vụ; không sửa bản nào.
6. **SSO scaffold Bootstrap 5**: view thiếu khai Layout sẽ rơi vào `_Layout.cshtml` Bootstrap 5/jQuery 3.5.1 — khi chụp pilot, kiểm chứng view pilot khai Layout tường minh.
7. **CIS `_FileUpload.cshtml`** có link Font Awesome 6.0.0 từ cdnjs đang hoạt động; EMR `Default.cshtml` có iframe thời tiết ngoài — giữ nguyên, ghi nhận, không "dọn" trong chương trình này.
8. **File lạ không phần mở rộng** trong `Views/Login/` của Base Web/Base HOSP (`XXcf5Y9E`, `XXsyzLsA`): không mở, không động vào; kiểm tra nguồn gốc là việc riêng ngoài chương trình này.

## 7. Kiểm chứng (bắt buộc sau mỗi thay đổi)

Build project bị ảnh hưởng 0 lỗi; screenshot trước/sau (viewport, Playwright + Firefox, đăng nhập thật 000000); nút vẫn chạy; form submit đúng; tên/ID input không đổi (đối chiếu diff); tương tác JS còn hoạt động; dropdown/modal như cũ; đích điều hướng không đổi; quyền/render có điều kiện còn nguyên; không phần tử nào biến mất (so ảnh + đếm phần tử chức năng chính); quyền/render có điều kiện còn nguyên. Nếu cải tiến giao diện đòi đổi chức năng thì KHÔNG làm, giữ nguyên và ghi limitation vào `REFACTOR_PROGRESS.md`.
