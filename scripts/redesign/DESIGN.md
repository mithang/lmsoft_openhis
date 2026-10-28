# Thiết kế lại UI/UX OpenHIS — Tài liệu nghiên cứu pattern

> **Phạm vi của tài liệu này:** CHỈ NGHIÊN CỨU. Tài liệu tổng hợp pattern thiết kế
> học được từ các hệ thống tương tự (HIS/bệnh viện, dashboard quản trị, hệ thống
> dữ liệu dày đặc) và diễn dịch thành cách áp dụng trong stack hiện tại của
> OpenHIS. Tài liệu **không chứa code, ảnh, font hay tài sản sao chép** từ hệ
> thống khác, **không đề xuất migration framework hay host mới**, và **không
> quyết định bất kỳ vấn đề nào ngoài UI** (tiền tố chứng từ, schema thiếu,
> tài khoản demo, dữ liệu thật — nếu gặp trong lúc audit thì chỉ ghi chú là
> ngoài phạm vi, không tự sửa).
>
> **Trạng thái:** Bản nghiên cứu cho điều phối viên soát trước khi chặng audit /
> thiết kế chi tiết bắt đầu. Chưa có số liệu kiểm chứng nào trong tài liệu này
> là kết quả đo trên hệ thống thật — mọi con số về breakpoint, kích thước chạm,
> line-height đều là **mục tiêu đề xuất**, cần chặng kiểm chứng xác nhận bằng
> viewport giả lập (desktop + mobile) rồi mới coi là đạt.

## 0. Stack thực tế đã đọc trong repo (chỉ đọc, không sửa)

Đọc lướt `~/workspace/openhis-core` ngày 2026-10-28 cho thấy bức tranh sau —
chặng audit phải đối chiếu lại trước khi áp bất kỳ class nào, vì tài liệu này
không tự suy ra phiên bản từ tên thư mục:

- **Shell của 10 app nghiệp vụ** (CIS, Herp, EMR, PDS, MRMS, OR, Base Web,
  Base HOSP, MRQC, Sett): mỗi app có `Views/Shared/_LayoutIndex.cshtml` dùng
  chung một bộ asset qua `MvcHelper.GetStaticResourceScriptUrl(...)`, tức là
  asset được phục vụ từ **static host**, không nằm trọn trong `wwwroot` của
  từng app:
  - `~/css/framework-font.css`, `~/css/framework-theme.css` (skin, có
    `id="color-skin"`), `~/css/framework-ui.css`, `~/js/framework-ui.js`,
    `~/js/Newtouch-ui.js`;
  - `~/js/bootstrap/bootstrap.js` + `bootstrap.min.css`;
  - `~/js/jquery/jquery-2.1.1.min.js` (tên file trong layout của CIS),
    `~/js/jquery-ui/jquery-ui.min.js`, `~/js/layout/jquery.layout.js`,
    `~/js/wdtree/tree.js` + `tree.css`;
  - `~/js/jqgrid/jqgrid.min.js` + `jqgrid.css`, kèm **cả hai locale**
    `grid.locale-cn.js` (nạp trước) và `/js/jqgrid/grid.locale-vi.js` (nạp sau,
    file locale Việt nằm trong từng app).
- **Shell cổng SSO** (`Newtouch.HIS.SSO/HIS.SSO`): trang portal dùng
  `Views/Shared/_Index.cshtml`, `_IndexHeaderNav.cshtml`, `_IndexNavCol.cshtml`,
  `_IndexContentWrapper.cshtml` và `Views/Home/SSOIndex.cshtml`. Trang này mang
  class kiểu AdminLTE (`hold-transition skin-black sidebar-mini`,
  `control-sidebar`, `AdminLTE.controlSidebar._fix(...)`, tab/iframe qua
  `~/js/indextab.js`, menu mở bằng iframe `data-target="iframe"`), nhưng
  CSS/JS AdminLTE cũng đi qua static host — **không tìm thấy thư mục
  AdminLTE đóng gói sẵn trong repo** khi grep `wwwroot`. Vì vậy mọi pattern
  dưới đây được viết theo **class và hành vi đã thấy trong repo**
  (sidebar/nav-col, tab iframe, jqGrid, Bootstrap), không giả định AdminLTE 4
  hay Bootstrap 5 có sẵn trong shell nghiệp vụ.
- **Layout độc lập của SSO** (`Views/Shared/_Layout.cshtml` của portal) là
  template ASP.NET Core chuẩn, dùng `~/lib/bootstrap/dist/css/bootstrap.min.css`
  **Bootstrap v5.1.0** (có banner phiên bản trong file) và jQuery trong
  `~/lib/jquery`. Đây là layout trang đơn, khác với shell portal `_Index`.
  **Kết luận thực hành:** đừng copy class Bootstrap 5 (`data-bs-*`,
  `d-none d-md-block`, …) sang các trang nghiệp vụ khi chưa xác nhận phiên
  bản Bootstrap mà static host đang phục vụ. Trong code cũ còn sót class kiểu
  Bootstrap 3 (`hidden-sm hidden-xs`, `nav nav-pills nav-stacked`) — đó là
  dấu hiệu cần xác nhận, không phải bằng chứng chắc chắn.
- **Các vấn đề đã biết mà chương trình giao (làm đầu mối, không phải kết luận
  audit):** trang chủ PDS trên mobile bị bóp méo; sidebar mobile thiếu nút
  mở rộng; bảng WorkSpace cuộn ngang; footer một số trang còn chữ vendor
  tiếng Trung. Mỗi vấn đề được map tới pattern tương ứng ở §5.
- **Quy ước breakpoint đã thấy trong repo** (trong `_IndexNavCol.cshtml` của
  SSO, các bản sửa FIX-1): `max-width: 991px` và `max-width: 767px`. Tài liệu
  này dùng lại hai ngưỡng đó làm chuẩn chung, thay vì bịa ngưỡng mới:
  - Mobile: `< 768px`; Tablet: `768–991px`; Desktop: `≥ 992px`.
  - Viewport kiểm chứng đề xuất (giả lập, không phải app native):
    mobile hẹp (~360–390px), tablet (~768px), desktop (~1280px trở lên).

## 1. Nguyên tắc thiết kế chung

Áp dụng cho cả 11 web app, không phân biệt app nào. Thứ tự ưu tiên là thứ tự
liệt kê — khi xung đột, nguyên tắc đứng trước thắng.

1. **Nhiệm vụ trước trang trí.** Người dùng OpenHIS làm việc theo ca, tra cứu
   và nhập liệu liên tục. Một màn hình tốt là màn hình giúp hoàn thành một
   nhiệm vụ (tìm bệnh nhân, lập phiếu, duyệt đơn) với ít bước nhất, chứ không
   phải màn hình nhiều màu, nhiều hiệu ứng. Mọi yếu tố trang trí không phục vụ
   nhiệm vụ đều là ứng viên để bỏ.
2. **Tổng quan trước, chi tiết khi cần (overview first, details on demand).**
   Màn hình mở đầu chỉ cho cái nhìn tổng quan và điểm cần chú ý; chi tiết đầy
   đủ nằm sau một thao tác chủ động (mở dòng, mở tab, bấm xem thêm). Không
   dồn toàn bộ trường dữ liệu lên màn hình đầu tiên.
3. **Nhất quán hơn sáng tạo.** Cùng một ý nghĩa thì cùng một hình thức ở mọi
   app: cùng màu cho cùng trạng thái, cùng vị trí cho cùng nút, cùng câu chữ
   cho cùng lỗi. Người dùng chuyển app không phải học lại. Sáng tạo chỉ được
   phép trong khuôn khổ token chung (§2).
4. **Không bao giờ chỉ dùng màu để truyền nghĩa.** Trạng thái luôn có thêm
   chữ, icon hoặc hình dạng đi kèm (ví dụ: "Chờ duyệt" + icon đồng hồ, không
   chỉ chấm vàng). Đây là yêu cầu an toàn trong môi trường bệnh viện và là
   điều kiện tối thiểu cho người mù màu.
5. **Mọi thao tác đều có phản hồi nhìn thấy được.** Bấm nút phải thấy trạng
   thái đang xử lý; lưu xong phải thấy xác nhận; lỗi phải nói rõ lỗi gì và
   sửa thế nào; đang tải phải thấy skeleton/spinner chứ không phải trang
   trắng. Không để người dùng phải tự đoán "nó đã lưu chưa".
6. **Ngăn lỗi thay vì chỉ báo lỗi.** Giá trị mặc định an toàn, kiểm tra ngay
   tại trường nhập (không đợi submit cả form dài), xác nhận trước thao tác
   không thể hoàn tác (xóa, hủy, chốt), tách nút nguy hiểm khỏi nút thường.
   Xác nhận chỉ dùng cho thao tác thật sự nguy hiểm — lạm dụng sẽ gây "mệt
   vì xác nhận" và người dùng sẽ bấm đồng ý một cách máy móc.
7. **Mobile là viewport, không phải nền tảng riêng.** OpenHIS không có app
   native; "mobile" trong chương trình này nghĩa là cùng một trang web chạy
   trên viewport hẹp. Vì vậy không thiết kế một bản mobile khác biệt hoàn
   toàn: cùng thông tin, cùng thứ tự ưu tiên, chỉ khác cách trình bày
   (xem §4). Chức năng quan trọng về mặt lâm sàng/vận hành phải dùng được
   trên viewport hẹp, không chỉ xem được.
8. **Tiếng Việt là yêu cầu kỹ thuật, không phải lớp dịch.** Dấu thanh xếp
   chồng (ế, ữ, ộ…) cần không gian dọc; cắt ngắn, viết hoa toàn bộ, chiều cao
   cố định hay line-height quá chặt đều làm vỡ chữ. Xem §7.
9. **Trạng thái rỗng, đang tải và lỗi là thiết kế, không phải trường hợp
   phát sinh.** Mỗi lưới, mỗi thẻ số liệu, mỗi form đều phải có thiết kế cho
   ba trạng thái này trước khi coi là xong (§6).
10. **Không bịa dữ liệu để giao diện trông đầy.** Thẻ số liệu không có mục
    tiêu thì ghi rõ "chưa đặt mục tiêu", không tự gán ngưỡng màu; không có dữ
    liệu thì nói "chưa có dữ liệu" kèm hành động tiếp theo, không hiển thị 0
    như thể đó là số đo thật. (Nguyên tắc này cũng áp dụng cho chặng audit:
    không tạo dữ liệu bệnh nhân/dữ liệu nghiệp vụ giả để chụp màn hình.)

## 2. Hệ thống thị giác dùng chung (token)

Mục tiêu: một bảng token duy nhất, mọi app tham chiếu tới nó, thay vì mỗi app
tự chọn màu/spacing. Cách áp dụng trong stack hiện tại: đặt token vào một file
CSS dùng chung của static host (ví dụ mở rộng `framework-theme.css` /
`framework-ui.css`) hoặc, nếu chưa thể chạm vào static host, vào một file
`redesign-tokens.css` nạp sau theme trong `_LayoutIndex.cshtml` của từng app
— quyết định vị trí file thuộc về chặng triển khai, không thuộc tài liệu này.
Token dưới đây là **đề xuất**, chưa phải giá trị đang chạy trong hệ thống.

### 2.1. Màu — ngữ nghĩa, không trang trí

| Vai trò | Dùng cho | Không dùng cho |
|---|---|---|
| Primary (xanh dương chủ đạo, kế thừa skin hiện tại) | Nút chính, liên kết, menu đang chọn, tiêu điểm | Trạng thái thành công/thất bại |
| Success (xanh lá) | Chỉ trạng thái: hoàn thành, đã duyệt, thành công | Nút chính, trang trí |
| Warning (vàng/cam) | Chỉ trạng thái: chờ xử lý, sắp hết hạn, cần chú ý | Nút thường |
| Danger (đỏ) | Chỉ trạng thái: lỗi, quá hạn, và nút hủy/xóa (đã tách vị trí) | Tiêu đề, nhấn mạnh chung |
| Info (xanh nhạt) | Ghi chú hệ thống, trạng thái trung tính | Cảnh báo |
| Neutral (xám) | Nền, viền, chữ phụ, trạng thái vô hiệu | — |

Quy tắc:

- Tối đa **6 màu có nghĩa** trên một màn hình (kể cả màu trạng thái). Vượt
  quá thì người dùng không còn xây được mô hình "màu này nghĩa là gì".
- Nút chính (primary) chỉ có **một** trên mỗi vùng thao tác chính; các nút còn
  lại là nút phụ (outline/ghost) hoặc nút văn bản. Hai nút primary cạnh nhau
  là lỗi thiết kế.
- Đỏ không dùng làm màu thương hiệu hay màu nhấn trang trí: trong bệnh viện,
  đỏ luôn bị đọc là "nguy hiểm/lỗi".
- Tương phản chữ/nền phải đọc được ở điều kiện ánh sáng phòng làm việc; kiểm
  tra bằng cách giảm độ sáng màn hình khi soát, không chỉ nhìn trên màn hình
  chuẩn.

### 2.2. Chữ

- **Một họ font cho toàn hệ thống**, kế thừa `framework-font.css` đang có;
  không thêm font tải ngoài chỉ vì đẹp (xem §8 "KHÔNG áp dụng"). Nếu font
  hiện tại thiếu dấu tiếng Việt ở một số cỡ, chặng triển khai đề xuất phương
  án thay thế kèm kiểm chứng render — không tự đổi trong chặng nghiên cứu.
- Thang cỡ đề xuất (chỉ là thang, giá trị tuyệt đối do chặng triển khai chốt
  theo font thực tế): tiêu đề trang lớn nhất; tiêu đề vùng nhỏ hơn một bậc;
  nhãn trường và chữ phụ nhỏ hơn chữ nội dung một bậc; **số liệu KPI lớn
  gấp đôi chữ nhãn** để mắt quét được giá trị trước khi đọc nhãn
  (pattern KPI: số 32–48px, nhãn 12–14px trong các hệ thống tham khảo).
- Chỉ dùng **một mức đậm cho một mục đích**: giá trị thì đậm, nhãn thì
  thường. "Mọi thứ đều đậm" tương đương "không có gì nổi bật".
- Không viết hoa toàn bộ cho nhãn dài bằng tiếng Việt (khó đọc dấu, khó
  quét); chỉ viết hoa chữ cái đầu câu hoặc tên riêng.
- Số liệu dùng font có chữ số đẳng rộng (tabular figures) nếu font hỗ trợ,
  để cột tiền/số lượng thẳng hàng khi quét dọc; nếu không, căn phải số và
  giữ định dạng thống nhất cũng đạt phần lớn hiệu quả.

### 2.3. Khoảng cách, bo góc, bóng

- Lưới khoảng cách theo bội số 4 (4 / 8 / 12 / 16 / 24px). Không dùng giá trị
  lẻ tùy hứng từng trang (3px, 5px, 7px…) — chính những lệch nhỏ này cộng lại
  thành cảm giác "mỗi app một kiểu".
- Bo góc: một giá trị cho thẻ (card), một giá trị nhỏ hơn cho nút/input, dùng
  thống nhất. Không trộn card bo tròn lớn với card vuông góc trong cùng app.
- Bóng (shadow): chỉ 2–3 mức (không bóng cho nội dung thường, bóng nhẹ cho
  thẻ nổi, bóng rõ cho modal/drawer). Bóng nặng khắp nơi làm màn hình dữ liệu
  dày đặc thêm rối.
- Mật độ: màn hình nghiệp vụ được phép dày đặc hơn trang giới thiệu, nhưng
  mật độ phải đến từ **nội dung có ích**, không đến từ việc nén padding tới
  mức chữ chạm nhau. Trên mobile, ưu tiên đọc được hơn nhồi thêm dòng.

### 2.4. Icon

- Một bộ icon duy nhất cho toàn hệ thống (hiện tại code đã dùng họ `fa-*`
  Font Awesome trong shell SSO — giữ họ đó, không thêm bộ thứ hai).
- Icon luôn đi kèm chữ ở nút và menu chính; **icon đơn độc chỉ chấp nhận
  được khi có tooltip khi hover/focus và `aria-label` đầy đủ** (ví dụ nút xóa
  trong dòng lưới). Icon không có nhãn là lỗi trên mobile vì không có hover.
- Cùng một hành động thì cùng một icon ở mọi app (thêm, sửa, xóa, tìm kiếm,
  xuất, in, làm mới, xem chi tiết). Chặng audit lập bảng map hành động → icon
  và sửa các chỗ lệch.

## 3. Bố cục tổng thể và điều hướng

### 3.1. Khung trang chuẩn (áp dụng cho mọi app)

Thứ tự từ trên xuống, từ trái sang phải — giống cách mắt quét (trái → phải,
trên → xuống):

1. **Topbar (cố định):** logo/tên hệ thống + tên app hiện tại ở trái; ở phải:
   tên người dùng, khoa/phòng đang làm việc (nếu có), nút đổi mật khẩu/đăng
   xuất, và nút mở sidebar trên mobile. Topbar không chứa menu nghiệp vụ dài;
   menu nghiệp vụ thuộc về sidebar.
2. **Sidebar (trái, cố định trên desktop):** nhóm menu theo nghiệp vụ, không
   theo tên bảng dữ liệu. Mỗi nhóm có tiêu đề nhóm, mỗi mục có icon + nhãn.
   Mục đang chọn phải nhìn thấy ngay (nền nhạt + vạch màu primary ở cạnh,
   chữ đậm hơn) — pattern này xuất hiện nhất quán trong các hệ thống tham
   khảo và đã có tiền lệ trong shell hiện tại.
3. **Vùng nội dung:** mở đầu bằng **tiêu đề trang + breadcrumb** (xem §3.3),
   sau đó là thanh công cụ (tìm kiếm/lọc + nút thao tác), rồi tới nội dung
   chính (lưới/form/dashboard). Footer nằm cuối vùng nội dung, không đè lên
   nội dung khi cuộn.
4. **Footer:** một dòng duy nhất, cùng nội dung ở mọi app: tên hệ thống +
   bản quyền. **Không để tên vendor/đường dẫn gốc bằng tiếng Trung** — đây là
   một trong các vấn đề đã biết; chặng triển khai thay bằng chuỗi tiếng Việt
   dùng chung, không sửa từng trang một cách rời rạc.

Cách áp dụng trong Razor/AdminLTE/jqGrid:

- Giữ cấu trúc `_LayoutIndex.cshtml` + partial header/nav/content-wrapper đã
  có; **không vẽ lại khung từ đầu cho từng app**. Chặng triển khai chỉ chuẩn
  hóa: thứ tự partial, class của wrapper, và vị trí đặt breadcrumb/footer.
- Shell portal SSO mở nội dung app con bằng tab + iframe (`indextab.js`,
  `Newtouch_iframe`): giữ cơ chế này, chỉ sửa hành vi trình bày của nó
  (xem §3.4 và §5). Không thay tab/iframe bằng SPA trong chương trình này.

### 3.2. Sidebar — nhóm, trạng thái, tìm kiếm

- **Nhóm theo luồng công việc**, ví dụ nhóm "Tiếp nhận", "Điều trị",
  "Dược", "Thanh toán", "Hệ thống" — thay vì liệt kê phẳng hàng chục mục.
  Mỗi nhóm tối đa khoảng 7 mục con; vượt quá thì tách nhóm hoặc đưa mục ít
  dùng xuống cuối.
- **Mục thường dùng lên đầu nhóm**, mục quản trị hệ thống xuống cuối. Thứ tự
  menu là thiết kế, không phải thứ tự bảng trong CSDL.
- **Trạng thái active phải bền vững:** khi mở một trang con (form thêm/sửa),
  mục menu cha vẫn giữ trạng thái active để người dùng biết mình đang ở đâu.
  Mất active khi vào trang con là lỗi điều hướng thường gặp.
- **Tìm kiếm trong sidebar (đề xuất, mức ưu tiên thấp):** nếu số mục menu
  lớn, thêm ô lọc menu ở đầu sidebar (lọc theo tên, không cần gọi server).
  Chỉ làm khi chặng audit xác nhận menu thực sự quá dài; không thêm chỉ vì
  "dashboard hiện đại nào cũng có".
- **Badge số lượng:** chỉ dùng cho số việc chờ xử lý thật (ví dụ "Chờ duyệt
  (3)"), nhãn phải nói rõ là số gì. Badge trang trí không có nghĩa thì bỏ.

### 3.3. Breadcrumb và tiêu đề trang

- Mọi trang nghiệp vụ (trừ trang đăng nhập và trang chủ portal) có breadcrumb
  dạng: `Trang chủ / Nhóm / Trang hiện tại`. Breadcrumb là liên kết, trừ mục
  cuối (trang hiện tại, không phải liên kết).
- Tiêu đề trang (`<h1>` duy nhất) dùng **ngôn ngữ nhiệm vụ**, ví dụ "Danh sách
  bệnh nhân nội trú" thay vì tên controller/bảng. Tiêu đề trùng với nhãn menu
  đã bấm vào — người dùng bấm gì thì thấy nấy.
- Trang con (thêm/sửa/chi tiết) có breadcrumb dài thêm một nấc và nút "Quay
  lại" về danh sách. Không bắt người dùng dùng nút Back của trình duyệt làm
  cách duy nhất để quay lại.

### 3.4. Tab và iframe trong shell portal (WorkSpace)

Đây là khu vực nhạy cảm nhất của chương trình vì nó là "khung" của mọi app:

- **Thanh tab phải cuộn được theo chiều ngang** khi nhiều tab, với nút cuộn
  rõ ràng ở hai đầu; tab đang active luôn nằm trong vùng nhìn thấy (tự cuộn
  tới tab active khi mở/đóng tab). Tab tràn ra ngoài mà không cuộn được là
  lỗi.
- **Mỗi tab có nút đóng riêng** (×) và tiêu đề ngắn, không cắt giữa chừng
  bằng dấu ba chấm mà không có tooltip đầy đủ.
- **Không mở tab trùng:** bấm lại menu của tab đang mở thì focus vào tab đó,
  không tạo tab thứ hai giống hệt.
- **WorkSpace (trang bàn làm việc) là một tab như các tab khác**, không phải
  một khung đặc biệt: nội dung của nó tuân theo cùng quy tắc responsive
  (§4). Vấn đề "bảng WorkSpace cuộn ngang" được xử lý bằng pattern bảng ở
  §4.2, không phải bằng cách thu nhỏ chữ toàn trang.
- Trên mobile, tab bar thu gọn: chỉ hiện tab active + nút danh sách tab (mở
  drawer liệt kê tất cả tab) thay vì cố nhét mọi tab vào một hàng hẹp.

### 3.5. Sidebar trên mobile — nút mở rộng là bắt buộc

Đây là một trong các vấn đề đã biết ("sidebar mobile thiếu nút mở rộng") và
là lỗi chặn (blocker) trên viewport hẹp, vì không mở được sidebar thì không
điều hướng được:

- **Nút hamburger phải luôn nhìn thấy trên topbar ở viewport < 992px**,
  kích thước chạm tối thiểu 44×44px, có `aria-label="Mở menu"` và trạng thái
  `aria-expanded`.
- Sidebar mobile mở dạng **drawer phủ lên nội dung** (overlay + nền mờ),
  kèm nút đóng (×) và bấm ra ngoài để đóng; đóng xong focus trả về nút
  hamburger. Không dùng kiểu sidebar "thu nhỏ chỉ còn icon" trên mobile hẹp
  vì icon không nhãn trên màn hình 360px vừa khó bấm vừa khó hiểu.
- Khi drawer mở, **khóa cuộn nền** để người dùng không vô tình cuộn nội dung
  phía sau.
- Trên tablet (768–991px) có thể dùng sidebar thu gọn chỉ icon **nếu** mỗi
  icon có tooltip/label khi chạm giữ hoặc khi mở rộng; nếu không làm được
  tooltip đáng tin cậy trên cảm ứng thì dùng chung drawer như mobile.
- Cách áp dụng: dùng cơ chế collapse/pushmenu đã có trong shell (class
  `sidebar-mini`/`sidebar-collapse` và control-sidebar của AdminLTE trong
  portal) và bổ sung nút + hành vi drawer ở breakpoint hẹp; **không viết
  thêm một hệ thống sidebar thứ hai** song song với cái đang có.

## 4. Responsive cho màn hình hẹp — đặc biệt là bảng dữ liệu lớn

Nguyên tắc đầu tiên: **đừng thu nhỏ bản desktop lại**. Một lưới 12 cột nén
vào màn hình 390px là không dùng được dù chữ có nhỏ tới đâu. Phải chọn chiến
lược theo nhiệm vụ của từng lưới, và ghi lựa chọn đó vào hồ sơ audit của
từng màn hình (không áp một chiến lược cho mọi lưới).

### 4.1. Ba chiến lược cho bảng, và khi nào dùng chiến lược nào

| Chiến lược | Cách trình bày trên mobile | Dùng khi | Không dùng khi |
|---|---|---|---|
| **A. Cuộn ngang + cột neo cố định** | Lưới giữ nguyên cấu trúc; cột định danh đầu tiên (mã, tên) cố định bên trái, các cột còn lại cuộn ngang; tiêu đề dính trên cùng khi cuộn dọc | Người dùng cần **so sánh ngang** nhiều dòng/cột (đối chiếu, duyệt loạt) | Chỉ cần xem một bản ghi; số cột quá lớn khiến cuộn ngang thành "mê cung" |
| **B. Dòng nở thành thẻ (row → card)** | Mỗi dòng thành một thẻ: dòng đầu là định danh + trạng thái, các trường còn lại xếp theo cặp nhãn–giá trị, bấm để mở rộng toàn bộ | Người dùng làm việc **từng bản ghi** (tra cứu, xem chi tiết, xử lý từng đơn) | Cần so sánh nhiều dòng cùng lúc; cần sửa nhanh nhiều ô (inline edit hàng loạt) |
| **C. Cột ưu tiên** | Chỉ hiện 2–3 cột quan trọng nhất; các cột còn lại ẩn sau nút "Xem thêm" của từng dòng | Mobile chủ yếu để **kiểm tra trạng thái nhanh** | Cột bị ẩn lại là cột người dùng cần để ra quyết định |

Quy tắc chọn:

- Mặc định cho lưới nghiệp vụ dày đặc là **A** (bảo toàn cấu trúc, ít rủi ro
  sai lệch dữ liệu nhất), trừ khi chặng audit chứng minh nhiệm vụ của lưới đó
  là tra cứu từng bản ghi — khi đó chọn **B**.
- **C** chỉ dùng cho lưới "xem nhanh trạng thái", không dùng cho lưới nhập
  liệu hay duyệt chứng từ.
- **Không bao giờ** dùng `transform: scale()` hay thu nhỏ font toàn lưới để
  "vừa màn hình". Đó là cách làm cho giao diện trông vừa nhưng không đọc
  được.

### 4.2. Cách áp dụng cho jqGrid trong stack hiện tại

jqGrid là lưới chủ lực của hệ thống nên mọi thay đổi responsive phải đi qua
cấu hình jqGrid, không vẽ lại lưới bằng HTML thuần song song:

- **Wrapper cuộn:** đặt lưới trong container `overflow-x: auto` (tương đương
  `.table-responsive` của Bootstrap) và đặt `min-width` cho lưới theo tổng
  chiều rộng cột tối thiểu, để cột không bị bóp méo. Vấn đề "bảng WorkSpace
  cuộn ngang" cần được chẩn đoán đúng: cuộn ngang **bản thân nó không phải
  lỗi** nếu cột neo + tiêu đề dính hoạt động; lỗi là khi cuộn ngang làm mất
  cột định danh, mất tiêu đề, hoặc làm vỡ bố cục trang (tràn viewport, đẩy
  footer, che nút). Chặng audit phân biệt ba trường hợp này trước khi sửa.
- **Chiều rộng theo viewport:** gọi `setGridWidth` (hoặc `autowidth`) lại khi
  cửa sổ đổi kích thước và khi sidebar đóng/mở — lưới jqGrid không tự co theo
  container. Đây là nguyên nhân phổ biến khiến lưới trông "bóp méo" sau khi
  sidebar thay đổi: chiều rộng lưới được tính một lần lúc tải trang.
- **Cột neo (frozen columns):** dùng tính năng frozen column của jqGrid cho
  cột định danh đầu tiên trong chiến lược A. Nếu phiên bản jqGrid đang dùng
  không hỗ trợ frozen ổn định, phương án dự phòng là giữ cột định danh lặp
  lại trong thẻ chi tiết (chiến lược B) — chặng triển khai xác nhận theo
  phiên bản thực tế, không giả định.
- **Cột ưu tiên:** dùng cấu hình `hidden` theo breakpoint bằng JavaScript khi
  resize (ẩn cột phụ dưới 768px, hiện lại trên desktop), kèm nút "hiện cột"
  hoặc chuyển sang xem chi tiết dòng. Không xóa cột khỏi `colModel` vĩnh
  viễn chỉ vì mobile.
- **Dòng → thẻ (chiến lược B):** không cần viết lưới thứ hai: dùng cùng dữ
  liệu của jqGrid để render thẻ ở breakpoint hẹp (hoặc dùng formatter mở rộng
  chi tiết dòng). Thẻ phải giữ **cùng thứ tự trường và cùng nhãn** với lưới
  desktop để người dùng không phải học lại.
- **Tiêu đề dính (sticky header):** khi lưới cuộn dọc trong vùng có chiều cao
  giới hạn, tiêu đề phải ở lại trên cùng. Nếu jqGrid của hệ thống chưa làm
  được điều này một cách ổn định, phương án chấp nhận được là để toàn trang
  cuộn (tiêu đề trang + lưới cùng cuộn) thay vì nhốt lưới trong khung cuộn
  con mà mất tiêu đề.
- **Hàng và nút trong lưới:** chiều cao dòng đủ để chạm (tối thiểu 44px cho
  vùng bấm trên mobile); nút thao tác trong dòng không dùng icon đơn độc
  không nhãn; tránh thao tác chỉ xuất hiện khi hover (không có hover trên
  cảm ứng).
- **Phân trang:** giữ phân trang phía server (đã là cách jqGrid của hệ thống
  làm việc với dữ liệu lớn), số dòng/trang nhất quán; trên mobile, thanh
  phân trang thu gọn (trước/sau + số trang hiện tại) thay vì liệt kê hàng
  chục số trang.

### 4.3. Bố cục trang trên mobile (không chỉ lưới)

- **Trang chủ PDS bị bóp méo (vấn đề đã biết):** chẩn đoán theo thứ tự sau
  trước khi sửa: (1) có `meta viewport` đúng không; (2) có phần tử nào đặt
  chiều rộng cố định bằng pixel lớn hơn viewport không; (3) lưới/thẻ nào
  không nằm trong wrapper cuộn; (4) sidebar/iframe có ép chiều rộng không.
  Sửa theo nguyên nhân, không đắp thêm CSS `!important` tràn lan. Pattern
  chung: mọi khối nội dung đều phải co được (dùng lưới Bootstrap
  `col-*-*` theo breakpoint, không dùng chiều rộng cố định cho khối chính).
- **Form trên mobile:** nhãn nằm **trên** trường nhập (không nằm ngang trái),
  trường nhập chiếm toàn bộ chiều rộng, các trường liên quan xếp dọc. Form
  nhiều cột trên desktop chuyển thành một cột trên mobile — không giữ hai
  cột hẹp trên màn hình 360px.
- **Dashboard trên mobile:** thẻ KPI xếp 2 cột (hoặc 1 cột nếu thẻ có nhiều
  chữ), không giữ 4 cột; biểu đồ chuyển sang dạng xếp dọc, chú thích
  (legend) đặt dưới biểu đồ thay vì chen bên cạnh.
- **Nút và thanh công cụ trên mobile:** nút chính chiếm toàn chiều rộng hoặc
  nhóm thành hàng cuộn; thanh công cụ nhiều nút chuyển thành nút chính +
  menu "Thêm" (dropdown) chứa nút phụ, để không tràn hàng nút ra ngoài
  viewport.
- **Không ẩn chức năng quan trọng chỉ vì mobile.** Ẩn được: cột phụ, mô tả
  dài, biểu đồ phụ. Không ẩn: nút lưu/duyệt/hủy, trường bắt buộc, cảnh báo
  trạng thái, thông tin định danh bệnh nhân/chứng từ.

## 5. Form và lưới dữ liệu

### 5.1. Form

- **Nhóm trường theo ý nghĩa, có tiêu đề nhóm:** ví dụ "Thông tin bệnh nhân",
  "Chẩn đoán", "Thanh toán". Form dài không nhóm là form khó soát lỗi.
- **Một cột cho form nhập chính trên desktop hẹp, tối đa hai cột cho form
  rộng**; các trường liên quan chặt (ngày bắt đầu – ngày kết thúc, tỉnh –
  huyện) mới đặt cạnh nhau.
- **Nhãn rõ ràng, không dùng placeholder thay nhãn.** Placeholder biến mất
  khi gõ, người dùng quên mình đang nhập gì. Placeholder chỉ dùng cho ví dụ
  định dạng ("VD: 01/01/1990").
- **Trường bắt buộc đánh dấu thống nhất** (dấu * đỏ + chữ "bắt buộc" trong
  mô tả nhóm nếu cần), và thông báo lỗi phải nằm **ngay dưới trường lỗi**,
  nêu rõ cách sửa ("Ngày sinh không được lớn hơn hôm nay", không chỉ "Dữ
  liệu không hợp lệ").
- **Kiểm tra ngay khi rời trường (on blur)** cho các lỗi phát hiện được tại
  chỗ (định dạng ngày, số âm, trường rỗng bắt buộc); kiểm tra liên trường
  (ngày kết thúc sau ngày bắt đầu) khi submit. Không đợi submit mới báo lỗi
  đầu tiên của một form dài.
- **Giá trị mặc định an toàn:** ngày mặc định là hôm nay khi hợp lý, đơn vị
  mặc định là đơn vị thường dùng nhất; giá trị mặc định phải là lựa chọn an
  toàn, không phải lựa chọn "tiện cho demo".
- **Không làm mất dữ liệu đã nhập:** khi submit lỗi, giữ lại toàn bộ giá trị
  đã nhập; khi người dùng lỡ bấm điều hướng khỏi form đang sửa dở, hỏi xác
  nhận (chỉ khi form thật sự có thay đổi chưa lưu).
- **Nút submit:** nút chính ở cuối form, nhãn là động từ nhiệm vụ ("Lưu phiếu",
  "Gửi duyệt") chứ không phải "OK"/"Submit" chung chung; nút phụ "Hủy" đặt
  cạnh nhưng tách biệt về mặt thị giác; **nút xóa/hủy dữ liệu không đặt sát
  nút lưu** (xem §6.3).
- **Ngày/giờ/số theo quy ước Việt Nam:** ngày `dd/MM/yyyy`, giờ 24h, số tiền
  phân tách hàng nghìn bằng dấu chấm + đơn vị `₫` (hoặc đơn vị của trường),
  số thập phân dùng dấu phẩy theo quy ước Việt Nam khi hiển thị cho người
  dùng cuối. Không để định dạng ngày kiểu Mỹ (`MM/dd/yyyy`) lọt vào form
  tiếng Việt — đây là lỗi gây nhầm lẫn ngày/tháng.
- **Trường tra cứu (bệnh nhân, thuốc, danh mục):** có tìm kiếm gõ để lọc,
  hiển thị mã + tên (không chỉ tên, vì tên trùng nhau), và sau khi chọn phải
  thấy rõ đã chọn đúng đối tượng (mã + tên + thông tin phân biệt).
- **Form nhiều bước (nếu có):** hiển thị rõ "Bước x/y" và tên các bước, cho
  quay lại bước trước mà không mất dữ liệu, kiểm tra từng bước trước khi
  sang bước sau.

### 5.2. Lưới dữ liệu (jqGrid) — hành vi, không chỉ hình thức

- **Thanh công cụ của lưới** (tìm kiếm, lọc, thêm, xuất, làm mới) đặt ở một
  vị trí cố định phía trên lưới ở mọi app, cùng thứ tự: tìm kiếm/lọc ở trái,
  nút thao tác ở phải. Người dùng không phải đi tìm nút "Thêm" ở mỗi app một
  chỗ.
- **Lọc phải nhìn thấy được:** khi lưới đang bị lọc, hiển thị rõ điều kiện
  lọc đang áp dụng + nút "Xóa lọc". Lưới đã lọc mà trông như toàn bộ dữ liệu
  là lỗi gây hiểu nhầm nghiêm trọng.
- **Sắp xếp:** tiêu đề cột có thể sắp xếp thì phải có dấu hiệu nhìn thấy
  (mũi tên lên/xuống + trạng thái đang sắp xếp theo cột nào). Sắp xếp không
  làm mất trang hiện tại một cách khó hiểu — hoặc quay về trang 1 và nói rõ,
  hoặc giữ nguyên trang nếu dữ liệu cho phép.
- **Chọn dòng:** dòng được chọn phải nổi bật rõ (nền + vạch), và thao tác
  trên dòng được chọn (sửa/xóa) phải vô hiệu hóa khi chưa chọn dòng nào, kèm
  thông báo hướng dẫn khi bấm ("Vui lòng chọn một dòng trước").
- **Số lượng và tổng:** chân lưới hiển thị "Đang xem x–y trên tổng z dòng"
  bằng tiếng Việt; cột tiền/số lượng có tổng ở chân lưới khi nghiệp vụ cần.
- **Tìm kiếm trong lưới:** ô tìm kiếm có nút xóa nhanh (×), tìm kiếm theo
  mã + tên (không chỉ một trường), và không tự động tìm kiếm sau mỗi ký tự
  nếu việc đó gây tải lại lưới liên tục — dùng nút "Tìm kiếm" hoặc debounce
  hợp lý (quyết định kỹ thuật thuộc chặng triển khai).
- **Hành động hàng loạt:** nếu có chọn nhiều dòng, thanh hành động hàng loạt
  xuất hiện ngay phía trên lưới khi có lựa chọn, nêu rõ "Đã chọn n dòng" và
  các thao tác áp dụng; thao tác nguy hiểm hàng loạt phải xác nhận kèm số
  lượng.

## 6. Dashboard, thẻ số liệu, thông báo và trạng thái

### 6.1. Dashboard và thẻ số liệu (KPI)

- **Mỗi thẻ trả lời một câu hỏi.** Thẻ tốt gồm: nhãn rõ ràng, giá trị chính,
  **điểm so sánh** (kỳ trước, mục tiêu, hoặc trung bình) và hướng thay đổi
  (tăng/giảm + tốt hay xấu — tăng không phải lúc nào cũng tốt, ví dụ số ca
  chờ tăng là xấu). Thẻ chỉ có một con số trơ trọi là thẻ chưa hoàn thành.
- **Tối đa 4–6 thẻ trên một hàng desktop, 2 thẻ trên mobile.** Nhiều hơn thì
  tách thành nhóm theo chủ đề hoặc chuyển xuống trang chi tiết. Dashboard
  nhồi 12 thẻ là dashboard không ai đọc hết.
- **Thứ tự quét:** thông tin quan trọng nhất ở góc trên-trái (điểm mắt quét
  đầu tiên), thẻ cảnh báo/cần hành động trước thẻ thống kê thuần túy.
- **Thẻ bấm được thì phải trông bấm được** (con trỏ, hiệu ứng hover/focus,
  tiêu đề là liên kết tới danh sách chi tiết đằng sau con số). Thẻ không bấm
  được thì không dùng kiểu dáng của liên kết.
- **Biểu đồ:** mỗi biểu đồ trả lời một câu hỏi duy nhất; trục và đơn vị ghi
  rõ; khoảng thời gian so sánh ghi rõ trên thẻ (không để biểu đồ so sánh
  "30 ngày qua" trong khi thẻ bên cạnh so sánh "tháng này"); màu của biểu đồ
  tuân theo token §2, không mỗi biểu đồ một bảng màu. Biểu đồ không có dữ
  liệu thì hiển thị trạng thái rỗng (§6.2), không vẽ trục trống.
- **Số liệu lâm sàng/vận hành phải kèm ngữ cảnh:** giá trị xét nghiệm kèm
  khoảng tham chiếu; số lượng kèm đơn vị và thời điểm chốt số liệu. Con số
  không kèm ngữ cảnh trong bệnh viện là con số có thể bị đọc sai.

### 6.2. Trạng thái: đang tải, rỗng, lỗi

Ba trạng thái này phải được thiết kế cho **mọi** lưới, thẻ và vùng dữ liệu:

| Trạng thái | Hiển thị | Không hiển thị |
|---|---|---|
| Đang tải | Skeleton (khung xám nhấp nháy theo hình dạng nội dung sắp hiện) hoặc spinner có chữ "Đang tải…" | Trang trắng; lưới cũ kèm dữ liệu của lần lọc trước mà không có dấu hiệu đang tải lại |
| Rỗng (không có dữ liệu) | Icon trung tính + câu giải thích ngắn + **hành động tiếp theo** ("Chưa có phiếu nào — [Tạo phiếu mới]" hoặc "Không tìm thấy kết quả — [Xóa bộ lọc]") | Lưới trống không chữ; thông báo lỗi đỏ cho trường hợp "không có dữ liệu" (rỗng không phải lỗi) |
| Lỗi | Thông báo rõ: lỗi gì, ảnh hưởng gì, làm gì tiếp ("Không tải được danh sách. [Thử lại]") | Mã lỗi kỹ thuật trơ trọi; trang trắng; thông báo chung chung "Có lỗi xảy ra" không có lối thoát |

Phân biệt rỗng do **chưa có dữ liệu** với rỗng do **lọc không ra kết quả**:
hai trường hợp này cần hai câu chữ và hai hành động khác nhau (tạo mới vs.
xóa lọc). Gộp chung thành một câu là lỗi nội dung.

### 6.3. Thông báo, xác nhận và thao tác nguy hiểm

Thứ bậc thông báo (từ nhẹ tới nặng):

1. **Toast (góc màn hình, tự ẩn):** chỉ cho xác nhận thành công nhẹ ("Đã lưu")
   và thông tin không chặn công việc. Toast phải đủ lâu để đọc (không biến
   mất sau 1–2 giây) và có nút đóng thủ công.
2. **Thông báo nội tuyến (inline alert trong trang):** cho lỗi của form, lỗi
   của vùng dữ liệu, cảnh báo cần chú ý trước khi tiếp tục. Đặt ở đầu vùng
   liên quan, không đặt ở cuối trang.
3. **Modal xác nhận:** chỉ cho thao tác không thể hoàn tác hoặc ảnh hưởng
   lớn (xóa, hủy phiếu, chốt số liệu). Nội dung modal phải **tóm tắt hậu quả
   cụ thể** ("Xóa phiếu X sẽ xóa cả n dòng chi tiết. Hành động này không thể
   hoàn tác.") kèm nút xác nhận mang nhãn hậu quả ("Xóa phiếu", không phải
   "OK").
4. **Banner hệ thống (đầu trang):** cho sự kiện toàn hệ thống (bảo trì, mất
   kết nối tới dịch vụ dùng chung). Không dùng banner cho thông báo của một
   trang.

Quy tắc thao tác nguy hiểm:

- Nút xóa/hủy đặt **tách xa** nút lưu/nút chính, dùng màu danger + icon thùng
  rác + nhãn chữ đầy đủ; không đặt hai nút sát nhau chỉ khác màu.
- Không dùng `alert()`/`confirm()` mặc định của trình duyệt cho luồng nghiệp
  vụ chính (không nhất quán thị giác, không chứa được tóm tắt hậu quả);
  dùng modal của hệ thống.
- Sau thao tác nguy hiểm thành công, thông báo phải xác nhận **đã xảy ra
  điều gì** ("Đã xóa phiếu X"), không chỉ "Thành công".
- Không xác nhận hai lần cho cùng một thao tác (modal rồi lại toast hỏi
  lại) — một điểm xác nhận duy nhất, rõ ràng.

### 6.4. An toàn thông tin trong giao diện bệnh viện (pattern, không phải tính năng mới)

- **Định danh luôn hiện diện:** màn hình làm việc với bệnh nhân/chứng từ phải
  luôn thấy mã + tên đối tượng đang xử lý ở đầu vùng nội dung (sticky nếu
  trang dài), để không xử lý nhầm hồ sơ khi chuyển tab. Đây là pattern phòng
  nhầm lẫn, không phải thay đổi nghiệp vụ.
- **Nhập liệu nhạy cảm (liều lượng, số tiền, ngày):** hiển thị lại giá trị
  vừa nhập dưới dạng tóm tắt trước khi xác nhận cuối; số tiền lớn hoặc liều
  vượt ngưỡng (nếu nghiệp vụ đã có ngưỡng) cần xác nhận riêng — ngưỡng cụ
  thể thuộc về nghiệp vụ, tài liệu này chỉ quy định **vị trí và hình thức**
  của bước xác nhận.
- **Không hiển thị thông tin nhạy cảm không cần thiết:** màn hình danh sách
  chỉ hiện trường cần cho nhiệm vụ đó; chi tiết nhạy cảm nằm sau thao tác mở
  chi tiết. (Quy tắc hiển thị, không phải phân quyền — phân quyền thuộc về
  backend và ngoài phạm vi tài liệu này.)

## 7. Lưu ý riêng cho giao diện tiếng Việt

- **Dấu thanh cần không gian dọc:** line-height cho chữ nội dung **1.5–1.6**
  (tương đối, không cố định bằng pixel), tiêu đề 1.3–1.4. Không dùng
  `line-height: 1` hay chiều cao cố định bằng đúng cỡ chữ cho nút, badge, ô
  lưới — dấu xếp chồng (ế, ữ, ộ, ấ…) sẽ bị cắt ở cạnh trên/dưới.
- **Chuỗi kiểm chứng bắt buộc khi soát giao diện:** mọi màn hình sau khi sửa
  phải được nhìn qua với các chuỗi chứa dấu xếp chồng ở đầu dòng, cuối dòng
  và trong nút/badge hẹp, ví dụ: "Ủy quyền", "Bệnh nhân nữ", "Xử lý", "Ứng
  trước", "Ổn định", "Dữ liệu". Nếu một trong các chuỗi này bị cắt dấu ở bất
  kỳ trạng thái nào (bình thường, hover, focus, mobile), coi như chưa đạt.
- **Không cắt ngắn bằng dấu ba chấm một cách mù quáng:** tên người, tên
  thuốc, tên khoa bị cắt giữa chừng có thể gây nhầm lẫn. Ưu tiên: cho phép
  xuống dòng trong ô lưới (với chiều cao dòng đủ), hoặc tooltip đầy đủ khi
  buộc phải cắt. Quyết định theo từng cột ở chặng audit.
- **Từ ngữ:** dùng câu chữ nhiệm vụ, tránh thuật ngữ Hán-Việt khó hiểu và
  tránh dịch sát nghĩa từ tiếng Trung/tiếng Anh. Ví dụ: dùng "Lưu", "Gửi
  duyệt", "Hủy phiếu" thay cho các nhãn chung chung; thông báo lỗi nói "làm
  gì tiếp theo", không chỉ nói "lỗi".
- **Định dạng Việt Nam thống nhất** (xem §5.1): ngày `dd/MM/yyyy`, giờ 24h,
  tiền phân tách bằng dấu chấm + `₫`. Mọi app dùng cùng một hàm định dạng —
  không để mỗi app tự format theo một kiểu.
- **Độ dài chuỗi tiếng Việt:** nhãn tiếng Việt thường dài hơn nhãn gốc;
  layout phải chịu được nhãn dài hơn dự kiến khoảng 30% mà không vỡ (nút
  không tràn, menu không cắt). Kiểm chứng bằng cách xem giao diện với các
  nhãn dài nhất thực tế trong hệ thống, không phải với nhãn mẫu ngắn.
- **Footer và chuỗi hệ thống:** toàn bộ chuỗi còn sót bằng tiếng Trung (đặc
  biệt footer vendor) được thay bằng tiếng Việt trong cùng một đợt chuẩn hóa
  chuỗi dùng chung, kèm kiểm tra lại ở mọi app — không sửa lẻ từng trang rồi
  bỏ sót trang khác.

## 8. KHÔNG áp dụng — pattern hay nhưng loại bỏ, kèm lý do

| Pattern / đề xuất | Lý do loại bỏ trong chương trình này |
|---|---|
| Migration sang React/Vue/Angular/SPA, hoặc viết lại frontend | Vượt phạm vi "thiết kế lại trong stack hiện tại"; đổi framework là dự án khác, rủi ro hồi quy toàn hệ thống, không cần thiết để đạt mục tiêu "mọi chức năng chạy được, UI nhất quán". |
| Nâng cấp AdminLTE 4 / Bootstrap 5 cho toàn bộ shell nghiệp vụ | Chưa xác nhận được phiên bản static host đang phục vụ; nâng cấp là quyết định kỹ thuật riêng, cần kiểm chứng tương thích với jqGrid/jQuery UI/framework-ui.js trước. Trong chương trình này chỉ chuẩn hóa trên phiên bản đang chạy. |
| Thay jqGrid bằng AG Grid, TanStack Table, DataTables… | Thay lưới chủ lực đồng nghĩa viết lại mọi màn hình danh sách; pattern responsive của §4 đạt được bằng cấu hình jqGrid hiện có. |
| Thêm Tailwind, hoặc bất kỳ thư viện CSS/JS nặng nào mới | Xung đột với theme/framework CSS hiện có, tăng rủi ro hồi quy; mọi token ở §2 đều thực hiện được bằng CSS thuần trong theme hiện tại. |
| App mobile native (iOS/Android) | Chương trình đã xác định mobile = viewport giả lập; không có app native và không có nhu cầu native trong phạm vi này. |
| Dark mode bắt buộc cho mọi app | Là tính năng mới, gấp đôi công kiểm chứng (mọi trạng thái × hai theme), không phục vụ mục tiêu "chạy hết chức năng, nhất quán". Có thể xem xét sau khi bản sáng đã nhất quán. |
| Sao chép giao diện, ảnh chụp, icon set, font của một HIS/dashboard thương mại cụ thể | Vi phạm quy tắc cứng của chương trình (chỉ học pattern, không copy tài sản độc quyền). Tài liệu này chỉ mô tả pattern ở mức hành vi/bố cục. |
| Tải font ngoài chỉ vì đẹp (kể cả font hỗ trợ tiếng Việt tốt) khi chưa kiểm chứng | Thêm phụ thuộc mạng, rủi ro hiển thị khi ngoại tuyến/mạng chậm, và có thể phá vỡ line-height/dấu đã ổn định. Mọi thay đổi font phải là **đề xuất riêng** kèm kiểm chứng render chuỗi §7, không tự thêm trong chặng redesign. |
| Plugin jQuery mới "cho hiện đại" (trừ plugin nhỏ, có lý do cụ thể) | Mỗi plugin mới là một điểm hỏng mới trên jQuery 2.1.1/jQuery UI cũ. Nếu chặng triển khai thấy cần một plugin nhỏ (ví dụ datepicker đã có sẵn trong shell thì dùng lại), phải ghi rõ tên, lý do, và phương án dự phòng khi plugin không tương thích — không tự thêm âm thầm. |
| Thiết kế lại luồng nghiệp vụ, đổi tên chức năng, đổi quy tắc duyệt/chốt | Ngoài phạm vi UI/UX. Redesign chỉ thay đổi cách trình bày và thứ tự hiển thị, không thay đổi nghiệp vụ, schema, hay tiền tố chứng từ. |
| Dashboard "trang trí": biểu đồ không trả lời câu hỏi nào, KPI không có điểm so sánh, carousel tự chạy | Gây nhiễu, tăng tải nhận thức, và trong bệnh viện có thể làm người dùng bỏ sót cảnh báo thật. |

## 9. Checklist nhất quán — áp dụng cho mọi app

Chặng audit và chặng kiểm chứng dùng checklist này cho **từng app** (SSO,
MRQC, Sett, MRMS, OR, Herp, EMR, Base HOSP, Base Web, CIS, PDS) và **từng
màn hình chính** của app đó. Một mục chưa đạt ở một app vẫn tính là chưa đạt
toàn chương trình — nhất quán xuyên suốt mới là mục tiêu.

### 9.1. Header

- [ ] Topbar: logo/tên hệ thống + tên app + tên người dùng + đăng xuất ở cùng
      vị trí như các app khác.
- [ ] Nút hamburger luôn thấy được ở viewport < 992px, bấm mở được sidebar
      (xem §3.5); nút có `aria-label` và trạng thái mở/đóng.
- [ ] Không có chữ tiếng Trung, không có tên vendor lạ trong header/footer.
- [ ] Tiêu đề trang (`<h1>`) duy nhất, trùng nhãn menu, dùng ngôn ngữ nhiệm vụ.

### 9.2. Sidebar

- [ ] Menu nhóm theo luồng công việc, mục thường dùng lên đầu, mục hệ thống
      xuống cuối; không liệt kê phẳng hàng chục mục.
- [ ] Mục đang chọn (kể cả khi đang ở trang con) nhìn thấy ngay: nền + vạch
      màu + chữ đậm, giống nhau ở mọi app.
- [ ] Mobile: sidebar mở dạng drawer phủ + nền mờ + nút đóng + bấm ngoài để
      đóng; đóng xong focus trả về nút hamburger; nền bị khóa cuộn khi drawer mở.
- [ ] Badge số (nếu có) ghi rõ là số gì, chỉ đếm việc chờ xử lý thật.

### 9.3. Form

- [ ] Nhãn luôn hiện (không dùng placeholder thay nhãn); trường bắt buộc
      đánh dấu thống nhất; lỗi nằm ngay dưới trường lỗi, nêu cách sửa.
- [ ] Nhóm trường có tiêu đề nhóm; form dài có thứ tự tab hợp lý và giữ dữ
      liệu khi submit lỗi.
- [ ] Nút chính duy nhất, nhãn là động từ nhiệm vụ; nút nguy hiểm tách xa
      nút chính; thao tác không hoàn tác có modal xác nhận tóm tắt hậu quả.
- [ ] Ngày `dd/MM/yyyy`, giờ 24h, tiền theo định dạng Việt Nam, thống nhất
      mọi app.
- [ ] Mobile: nhãn trên trường, một cột, trường chiếm toàn chiều rộng.

### 9.4. Lưới (jqGrid)

- [ ] Thanh công cụ ở cùng vị trí, cùng thứ tự (lọc trái, thao tác phải) ở
      mọi app.
- [ ] Điều kiện lọc đang áp dụng luôn nhìn thấy + nút "Xóa lọc".
- [ ] Cột sắp xếp được có chỉ báo hướng sắp xếp; dòng được chọn nổi bật rõ;
      thao tác trên dòng vô hiệu hóa khi chưa chọn dòng.
- [ ] Chân lưới ghi "Đang xem x–y trên tổng z dòng" bằng tiếng Việt.
- [ ] Lưới không tràn viewport: nằm trong wrapper cuộn ngang, chiều rộng cập
      nhật khi resize/sidebar đổi trạng thái, cột định danh không bị mất khi
      cuộn (chiến lược A) hoặc thẻ chi tiết giữ đủ trường (chiến lược B).
- [ ] Mỗi lưới có hồ sơ ghi rõ chiến lược responsive đã chọn (A/B/C) và lý
      do theo nhiệm vụ của lưới đó.
- [ ] Trạng thái đang tải / rỗng / lỗi theo §6.2, phân biệt "chưa có dữ liệu"
      với "lọc không ra kết quả".

### 9.5. Nút

- [ ] Một nút chính duy nhất mỗi vùng thao tác; nút phụ dùng kiểu outline/
      ghost; nút nguy hiểm dùng danger + icon + nhãn chữ, tách vị trí.
- [ ] Mọi nút có nhãn chữ; icon đơn độc (nếu có) có tooltip + `aria-label`.
- [ ] Vùng bấm tối thiểu 44×44px trên mobile; nút đang xử lý có trạng thái
      loading và vô hiệu hóa bấm trùng.
- [ ] Cùng hành động thì cùng icon, cùng nhãn, cùng vị trí tương đối ở mọi app.

### 9.6. Thông báo

- [ ] Thành công nhẹ dùng toast có nút đóng, đủ lâu để đọc; lỗi form dùng
      inline alert tại vùng liên quan; thao tác nguy hiểm dùng modal tóm tắt
      hậu quả; sự kiện hệ thống dùng banner — không lẫn thứ bậc.
- [ ] Thông báo lỗi luôn có lối thoát (Thử lại / Quay lại / Cách sửa),
      không có thông báo "Có lỗi xảy ra" trơ trọi.
- [ ] Không dùng `alert()`/`confirm()` mặc định của trình duyệt cho luồng
      nghiệp vụ chính.

### 9.7. Mobile (viewport giả lập, không app native)

- [ ] Kiểm chứng ở cả ba viewport: hẹp (~360–390px), tablet (~768px),
      desktop (~1280px trở lên).
- [ ] Trang chủ PDS không bóp méo; không phần tử nào tràn viewport; footer
      không đè nội dung; sidebar mở/đóng được (§3.5).
- [ ] Lưới: chiến lược A/B/C đã chọn hoạt động đúng; tiêu đề/cột định danh
      không bị mất; nút trong dòng bấm được bằng ngón tay.
- [ ] Form một cột, nhãn trên trường; dashboard 2 cột (hoặc 1 cột nếu thẻ
      nhiều chữ); thanh công cụ không tràn hàng nút.
- [ ] Chức năng quan trọng (lưu, duyệt, hủy, cảnh báo, định danh) không bị
      ẩn chỉ vì viewport hẹp.
- [ ] Chuỗi dấu xếp chồng (§7) không bị cắt ở bất kỳ trạng thái nào
      (thường, hover, focus, mobile).

### 9.8. Nhất quán thị giác và ngôn ngữ

- [ ] Màu chỉ theo token §2; không quá 6 màu có nghĩa trên một màn hình;
      trạng thái không bao giờ chỉ truyền bằng màu.
- [ ] Một họ font, một thang cỡ, một bộ icon, một lưới khoảng cách (bội số 4)
      xuyên suốt 11 app.
- [ ] Không còn chuỗi tiếng Trung trong giao diện người dùng (footer, tooltip,
      thông báo, locale jqGrid hiển thị) — lưu ý: file `grid.locale-cn.js`
      vẫn đang được nạp trước locale Việt trong layout; chặng triển khai
      xác nhận locale hiển thị cuối cùng là tiếng Việt và xử lý file locale
      thừa theo quyết định riêng, không xóa bừa trong chặng redesign.
- [ ] Câu chữ dùng ngôn ngữ nhiệm vụ, thống nhất giữa các app cho cùng một
      hành động/thông báo.

## 10. Các vấn đề đã biết → pattern xử lý (bảng tra nhanh)

| Vấn đề đã biết | Pattern xử lý | Chặng audit cần xác nhận thêm |
|---|---|---|
| Trang chủ PDS trên mobile bị bóp méo | §4.3: chẩn đoán theo thứ tự viewport → chiều rộng cố định → wrapper cuộn → sidebar/iframe; bố cục co được, không chiều rộng cố định cho khối chính | Phần tử cụ thể nào gây tràn; trang chủ PDS dùng layout nào (khác `_LayoutIndex` không) |
| Sidebar mobile thiếu nút mở rộng | §3.5: hamburger luôn thấy < 992px, drawer + overlay + khóa cuộn nền | Cơ chế collapse hiện có trong từng shell; hành vi focus sau khi đóng |
| Bảng WorkSpace cuộn ngang | §4.2: phân biệt cuộn ngang lành mạnh (cột neo + tiêu đề dính) với cuộn làm vỡ bố cục; `setGridWidth` khi resize/sidebar đổi | Lưới WorkSpace dùng chiến lược A hay B theo nhiệm vụ thực tế của nó |
| Footer một số trang còn chữ vendor tiếng Trung | §3.1 + §7: một footer dùng chung bằng tiếng Việt cho mọi app, soát toàn bộ app trong cùng một đợt | Danh sách đầy đủ các trang còn chuỗi lạ (không chỉ footer) |

## 11. Ghi chú ngoài phạm vi (ghi nhận, không quyết định)

- Mâu thuẫn tiền tố chứng từ giữa các luồng (đã được ghi nhận trong các
  chặng trước): thuộc về nghiệp vụ/dữ liệu, **không** giải quyết bằng redesign
  giao diện. Giao diện chỉ hiển thị đúng số chứng từ mà backend trả về.
- Schema thiếu / bảng không tồn tại, menu chết, stub chưa triển khai: chặng
  redesign không tự bịa màn hình thay thế cho dữ liệu không có; màn hình gặp
  các trường hợp này hiển thị trạng thái lỗi/rỗng theo §6.2 và ghi vào báo
  cáo audit để điều phối viên quyết định.
- Tài khoản demo, mật khẩu mặc định, secret: thuộc về chặng bảo mật, không
  thay đổi trong chặng redesign.
- Tên bệnh viện thật, dữ liệu thật của bệnh viện: thuộc về phía bệnh viện
  cung cấp; giao diện không tự điền tên/mẫu dữ liệu giả.

## 12. Nguồn tham khảo (chỉ học pattern, không copy tài sản)

Các nguồn dưới đây được dùng để tổng hợp pattern ở mức hành vi/bố cục.
Không có code, ảnh, font hay giao diện nào từ các nguồn này được đưa vào
tài liệu hay repo.

- Pattern HIS/dashboard, sidebar + active state, nhóm menu theo nghiệp vụ:
  https://adminlte.io/blog/healthcare-dashboard-templates/
- Bảng dữ liệu lớn: ba chiến lược responsive (cuộn ngang + cột neo, dòng
  thành thẻ, cột ưu tiên) và nguyên tắc "đừng thu nhỏ bản desktop":
  https://ninjatables.com/big-data-table-design/
  và https://wpmanageninja.com/designing-big-data-tables/
- Bảng Bootstrap: sticky header, wrapper cuộn, touch target 44px, không dùng
  scale cho bảng: https://copyprogramming.com/howto/html-table-scale-to-fit
  và https://copyprogramming.com/howto/scrollable-table-with-fixed-header-css-code-example
- jqGrid + Bootstrap: cấu hình responsive/autowidth, ẩn cột phụ trên mobile:
  https://copyprogramming.com/howto/where-to-download-ui-jqgrid-bootstrap-css-from
- KPI/thẻ số liệu: giá trị + điểm so sánh + hướng tốt/xấu, trạng thái rỗng/
  lỗi có hành động tiếp theo, không gán ngưỡng màu khi không có mục tiêu:
  https://fuselabcreative.com/kpis-and-data-dashboard-design/
- Dashboard: mỗi biểu đồ một câu hỏi, màu có ngữ nghĩa thống nhất, thiết kế
  trạng thái rỗng/lỗi trước: https://medium.com/@iosapps/saas-dashboard-ui-inspiration-code-best-modern-examples-for-2027-1851af8ec87b
- An toàn y tế: xác nhận thao tác không hoàn tác, kiểm tra ngay tại trường,
  giá trị mặc định an toàn, định danh bệnh nhân luôn hiện diện:
  https://www.saasfactor.co/blogs/user-interface-design-for-healthcare-applications
  và https://www.webmoghuls.com/ui-mistakes-hospitals-should-avoid-in-2026/
- Nút xóa/nguy hiểm: tách vị trí, nhãn chữ + icon, tooltip + aria-label cho
  icon đơn độc, không chỉ dùng màu: https://www.ssw.com.au/rules/destructive-button-ui-ux
- Tiếng Việt: line-height tương đối 1.5–1.6, không dùng line-height 1 hay
  chiều cao cố định cho chữ có dấu xếp chồng, kiểm chứng render với chuỗi
  dấu thật: https://copyprogramming.com/howto/how-do-diacritical-marks-impact-centering-and-overshooting
  và https://vietnamesetypography.com/type-recommendations/

---

*Hết tài liệu nghiên cứu. Chặng tiếp theo (audit) dùng §9 làm checklist và
§10 làm bảng tra nhanh; mọi điểm "cần xác nhận thêm" trong §0, §4, §10 phải
được xác nhận bằng bằng chứng trên repo/viewport thật trước khi triển khai,
không suy đoán từ tài liệu này.*
