# Audit UI/UX — SSO (5208), MRQC (5220), Sett (5221) + Center hosts (8015/8016)

> **Trạng thái báo cáo: AUDIT DỞ DANG DO CHẶN ĐĂNG NHẬP — đọc §0 trước.**
> Worker audit này KHÔNG sửa code, KHÔNG commit, KHÔNG chuyển sang chặng redesign.
> Phương pháp: Playwright + Firefox local, viewport desktop 1280×800, tablet 768×1024,
> mobile 390×844. Mobile = giả lập viewport, KHÔNG phải app native.
> Quy tắc tuân thủ: prod chỉ đọc (điều hướng + chụp ảnh); không submit form nào ngoài
> nút đăng nhập bằng tài khoản 000000 duy nhất; không ghi database (kể cả STG);
> không khởi động/dừng app; không sửa file app.
> Ảnh: `~/workspace/shots-redesign/<app>/` (thư mục dùng chung với các worker audit khác).

## 0. CHẶN (BLOCKER) — trang nội bộ hầu hết CHƯA audit được

### 0.1. Chuyện gì đã xảy ra

- **Chặng chụp 1** (script `/tmp/audit_sms.py`): đăng nhập bằng cách điền
`#txt_account` = 000000, `#txt_password` = 000000, bấm `#login_button`
(trang tự md5 mật khẩu phía client qua `jquery.md5.js`, giống flow chuẩn).
Kết quả thất thường theo context: SSO chỉ thành công ở context tablet;
MRQC thành công ở mobile + tablet nhưng thất bại ở desktop; Sett thành công
ở mobile nhưng phiên không giữ được khi điều hướng tiếp (xem §0.3).
- **Chặng chẩn đoán** (script `/tmp/audit_sms_diag.py`, chờ `networkidle` +
chờ điều hướng tới `/Home/Index` tối đa 30s): đăng nhập **thất bại cả 3 app**,
thông báo:
- SSO 5208: “Đăng nhập thất bại, vui lòng liên hệ quản trị viên”
- MRQC 5220: “登录失败，请联系管理员” (tiếng Trung, xem SSO-07)
- Sett 5221: “登录失败，请联系管理员” (tiếng Trung, xem SSO-07)
- CheckLogin đều trả HTTP 200 (middleware lỗi trả 200 nên status không chứng
minh được gì); cookie phiên không được cấp, chỉ có cookie `*_ClientId`.
- **Sau khi chẩn đoán, kiểm tra hạ tầng (chỉ đọc): SQL Server KHÔNG còn lắng
nghe.** `ss -ltn` không có cổng 1433, không có tiến trình `sqlservr`;
`sqlcmd` tới 127.0.0.1 báo login timeout / server not found. Trong khi đó
`Data Source=.` trong `HIS.BaseAPI/appsettings.json` nghĩa là app trông đợi
SQL Server local. **Đây là nguyên nhân khả dĩ nhất của các lỗi đăng nhập
hiện tại** (CheckLogin gặp lỗi kết nối DB và trả về thông báo thất bại chung
chung), chứ chưa chắc tài khoản đã bị khóa.
- **Không loại trừ khả năng tài khoản cũng bị khóa:** các lần đăng nhập thất
bại ở chặng 1 (có thể do bấm nút khi JS chưa bind xong, gửi mật khẩu rỗng)
có thể đã cộng dồn vào bộ đếm khóa theo cơ chế lockout đã biết (3 lần sai →
khóa). Khi DB hoạt động lại, điều phối viên cần kiểm tra cờ `Locked` của
000000 trước khi kết luận.
- **Hậu quả cho audit này:** tất cả ảnh “trang nội bộ” chụp khi chưa đăng
nhập thành công thực chất chỉ là ảnh trang đăng nhập (URL cuối vẫn là
`/Login/Index`). Các ảnh đó vẫn có giá trị audit cho **trang đăng nhập**,
nhưng **không được dùng làm bằng chứng cho trang nội bộ**. Chỉ 2 trang nội
bộ thật sự render được: SSO portal tablet và MRQC home tablet (xem §2).
- **Yêu cầu điều phối viên trước khi audit lại:** (1) khôi phục SQL Server
(watchdog/launcher — ngoài phạm vi worker này, không tự khởi động);
(2) kiểm tra + mở khóa 000000 nếu `Locked=1`; (3) cho chạy lại audit nội bộ
với script chờ `networkidle` + chờ điều hướng 30s (bài học từ chặng 1: chờ
3s sau khi bấm nút là không đủ tin cậy). **Từ thời điểm chẩn đoán xong,
worker đã DỪNG mọi lần thử đăng nhập** để tránh làm nặng thêm khóa tài khoản.

### 0.2. Những gì báo cáo này KHẲNG ĐỊNH được và KHÔNG khẳng định được

| Nội dung | Trạng thái |
|---|---|
| Trang đăng nhập cả 3 app × desktop + mobile | ✅ Đã chụp + đo đầy đủ |
| SSO portal ở tablet 768 | ✅ Đã chụp (đăng nhập thành công) |
| MRQC Home/Index ở tablet 768 | ✅ Đã chụp (không cần phiên? xem MRQC-02) |
| Center hosts 8015/8016: root + Swagger | ✅ Đã probe + chụp Swagger |
| Phiên bản asset static host, thứ tự nạp locale (mức source) | ✅ Đã xác nhận (xem §4) |
| SSO Notice, SyncForm; MRQC MedicalCenter, Apply; Sett Home, lưới quyết toán, BookkeepInHos | ❌ CHƯA audit (bị chặn đăng nhập — ảnh hiện có chỉ là trang login, không tính) |
| Locale jqGrid hiển thị cuối cùng, hành vi sidebar mobile, nút hamburger | ❌ CHƯA xác minh live (không trang lưới/sidebar nào render được) |
| Hồ sơ chiến lược responsive A/B/C cho từng lưới Sett | ❌ CHƯA xác định (không lưới nào render được; §4.4 chỉ nêu hướng phân tích cho lần audit lại, không phải quyết định) |
| Console errors | Trên các trang đã render: 0 lỗi console (nhưng phạm vi hẹp, chỉ login + portal + MRQC home) |

## 1. Ảnh đã chụp (chỉ liệt kê ảnh có giá trị — ảnh trang nội bộ bị redirect về login được đánh dấu *)

Ảnh nằm ở `~/workspace/shots-redesign/`:

### SSO (`shots-redesign/sso/`)
| Ảnh | Giá trị |
|---|---|
| `login-desktop.png`, `login-mobile.png` | ✅ Trang đăng nhập thật |
| `portal-home-tablet.png` | ✅ Portal thật (đã đăng nhập) |
| `portal-home-desktop.png`, `portal-home-mobile.png`, `portal-home-mobile-drawer.png`, `notice-desktop.png`, `notice-mobile.png`, `syncform-desktop.png`, `syncform-mobile.png` | ⚠️ *Thực chất là trang đăng nhập (redirect) — KHÔNG dùng làm bằng chứng nội bộ |

### MRQC (`shots-redesign/mrqc/`)
| Ảnh | Giá trị |
|---|---|
| `login-desktop.png`, `login-mobile.png` | ✅ Trang đăng nhập thật |
| `home-tablet.png` | ✅ Home thật (trang trần không layout, xem MRQC-02) |
| `home-desktop.png`, `home-mobile.png`, `home-mobile-drawer.png`, `medicalcenter-desktop.png`, `medicalcenter-mobile.png`, `apply-desktop.png`, `apply-mobile.png` | ⚠️ *Thực chất là trang đăng nhập (redirect) — KHÔNG dùng làm bằng chứng nội bộ |

### Sett (`shots-redesign/sett/`)
| Ảnh | Giá trị |
|---|---|
| `login-desktop.png`, `login-mobile.png` | ✅ Trang đăng nhập thật |
| `home-desktop.png`, `home-mobile.png`, `home-mobile-drawer.png`, `home-tablet.png`, `daysett-inpatient-desktop.png`, `daysett-inpatient-mobile.png`, `bookkeepinhos-desktop.png`, `bookkeepinhos-mobile.png` | ⚠️ *Thực chất là trang đăng nhập (redirect) — KHÔNG dùng làm bằng chứng nội bộ |

### Center hosts (ảnh ở thẳng `shots-redesign/`)
| Ảnh | Giá trị |
|---|---|
| `authcenter-swagger-desktop.png`, `authcenter-swagger-mobile.png` | ✅ Swagger UI AuthCenter (200) |
| `webapi-manage-swagger-desktop.png`, `webapi-manage-swagger-mobile.png` | ✅ Swagger UI WebAPI.Manage (200) |
| (root `/` cả 2 host) | Không chụp được trang: root trả 404 rỗng, Firefox báo `NS_ERROR_NET_EMPTY_RESPONSE` — bản thân nó là bằng chứng API-only (xem §3) |

## 2. Lỗi UI/UX theo checklist DESIGN.md §9 (chỉ từ trang đã render thật)

### SSO

**SSO-01 — Trang đăng nhập: tiêu đề “chào mừng” đè lên chữ “đăng nhập” và ô nhập**
- Ảnh: `sso/login-desktop.png`, `sso/login-mobile.png`.
- Trên desktop, dòng “chào mừng” (viết thường, có gạch chân xanh) và dòng
“đăng nhập” (đậm, nằm ngay trên ô tài khoản) chồng nhau; trên mobile cũng
vậy. Đây là lỗi thị giác rõ ràng, không phải do viewport.
- Checklist §9.8 (nhất quán thị giác/ngôn ngữ), §9.5 (nhãn nút rõ ràng).
- Mức: Medium (trang cửa ngõ của toàn hệ thống; vẫn bấm được nhưng trông lỗi).

**SSO-02 — Trang đăng nhập mobile: khối header rộng 1160px, logo bị cắt**
- Đo được: viewport 390px, phần tử `DIV.login-head` rộng 1160px, cạnh phải
tới 1160px (vượt viewport gần 3 lần). Ảnh `sso/login-mobile.png` cho thấy
logo “天天开源” bị cắt ở cạnh trái, các icon nền tràn sau form.
- `documentElement.scrollWidth` vẫn = 390 vì trang dùng `overflow-x: hidden`
(nội dung bị cắt chứ không cuộn được) — tức là lỗi “cắt nội dung”, không
phải “cuộn ngang lành mạnh” theo §4.2.
- Checklist §9.7 (mobile: không phần tử nào tràn viewport).
- Mức: Medium.

**SSO-03 — Footer vendor tiếng Trung trên mọi trang SSO đã render**
- Chỉ liệt kê, KHÔNG tự viết lại (theo quy tắc cứng):
- Trang đăng nhập: `Copyright © 2025湖北天天数链技术有限公司 mã nguồn của hệ
thống này được cấp phép theo《天天开源软件（社区版）许可协议》`
(25 ký tự CJK đo được trong DOM).
- Portal tablet: cùng footer, cộng thêm chuỗi tải trang tiếng Trung
`数据加载中` (“đang tải dữ liệu”) và `请稍后` (“vui lòng chờ”) xuất hiện
trong DOM portal (33 ký tự CJK).
- Checklist §9.1, §9.8 (không còn chuỗi tiếng Trung trong giao diện).
- Mức: Low–Medium (footer là chuỗi hệ thống dùng chung; chuỗi “đang tải”
tiếng Trung là lỗi nội dung, vì nó hiện ra với người dùng khi tải trang).

**SSO-04 — Portal ở tablet 768: sidebar hiện nhưng nhãn bị cắt, tên người dùng bị cắt, nội dung WorkSpace trắng**
- Ảnh: `sso/portal-home-tablet.png`.
- Sidebar trái luôn hiện ở 768px nhưng cột quá hẹp: các nhãn menu bị cắt
(“Quan ly”, “Cai dat he”, “Tram y ta”) — người dùng không đọc được hết tên
mục. Tên người dùng trên topbar bị cắt thành “Quan tri vien【0000…”.
- Tab “trang chủ” đang active nhưng vùng iframe WorkSpace bên dưới hoàn toàn
trắng tại thời điểm chụp (sau khi chờ tải trang) — chưa rõ là tải chậm hay
WorkSpace không render ở viewport này; cần kiểm tra lại khi audit lại.
- Footer tiếng Trung đè lên vùng thanh tab (footer nằm cuối vùng nội dung
nhưng visually chồng lên hàng tab).
- Checklist §9.1 (header), §9.2 (sidebar: mục đọc được đầy đủ), §3.4 (tab/
iframe), §9.7 (tablet).
- Mức: High cho nhãn sidebar bị cắt (điều hướng ở tablet bị ảnh hưởng);
Medium cho WorkSpace trắng (chưa kết luận được nguyên nhân).

**SSO-05 — Nhãn menu portal không dấu, không nhất quán với nơi khác**
- DOM portal tablet: `Quan tri vien`, `Quan ly he thong`,
`Quan ly benh an`, `Thong tin benh nhan`, … — toàn bộ không dấu, trong khi
MRQC home và các trang khác dùng tiếng Việt có dấu. Đây không phải chuỗi
tiếng Trung, nhưng là không nhất quán ngôn ngữ xuyên app.
- Checklist §9.8. Mức: Low.

**SSO-06 — Portal render ở tablet KHÔNG có thẻ meta viewport trong DOM**
- Đo được: `viewportMeta = null` trên DOM portal tablet. Trong source,
`Views/Shared/_Index.cshtml` dòng 11 CÓ `<meta name="viewport"
content="width=device-width, initial-scale=1.0" />`. Mâu thuẫn giữa source
và DOM render cần chặng triển khai đối chiếu (có thể portal thực tế dùng
layout/partial khác, hoặc meta nằm ngoài document đo được) — audit chỉ ghi
nhận mâu thuẫn, không kết luận.
- Checklist §4.3 (bước chẩn đoán đầu tiên: có meta viewport đúng không).
- Mức: Low (chờ xác minh lại; không tự sửa).

**SSO-07 — Thông báo đăng nhập thất bại: SSO tiếng Việt, MRQC/Sett tiếng Trung**
- SSO: “Đăng nhập thất bại, vui lòng liên hệ quản trị viên”.
- MRQC, Sett: “登录失败，请联系管理员” (nguyên văn tiếng Trung).
- Cùng một tình huống, 3 app dùng 2 ngôn ngữ — lỗi nhất quán ngôn ngữ (§9.8).
Ghi chú: thông báo này cũng không nói rõ nguyên nhân (sai mật khẩu? khóa
tài khoản? lỗi hệ thống?) và không có lối thoát (§9.6: lỗi phải có
“Thử lại / cách sửa”) — nhưng trong lần audit này nguyên nhân thật có thể
là DB down, nên câu chữ “liên hệ quản trị viên” là đúng hướng, chỉ sai ngôn
ngữ ở MRQC/Sett.
- Mức: Medium (MRQC/Sett).

### MRQC

**MRQC-01 — Trang đăng nhập desktop: document rộng 1799px trên viewport 1280px**
- Đo được: desktop `scrollWidth = 1799`, `clientWidth = 1280`; mobile
`scrollWidth = 548` trên viewport 390, phần tử `DIV.login-head` rộng 825px.
Quét theo rect không chỉ ra một phần tử con duy nhất vượt viewport (nền/
ảnh trang trí có chiều rộng cố định lớn), nhưng ảnh mobile
(`mrqc/login-mobile.png`) xác nhận logo bị cắt ở cạnh trái — tức nội dung
bị cắt do `overflow-x: hidden` trong layout, giống SSO-02.
- Ảnh desktop trông bình thường vì phần vượt bị cắt ngoài khung nhìn, nhưng
đó vẫn là tràn bố cục (§9.7).
- Mức: Low–Medium.

**MRQC-02 — Home/Index ở tablet là trang trần, không có shell layout**
- Ảnh: `mrqc/home-tablet.png`. Trang chỉ có tiêu đề, danh sách liên kết
(“Trung tâm kiểm soát chất lượng (MedicalCenter)”, “Tra cứu tin nhắn kiểm
soát chất lượng”, …, “Danh sách nhân sự theo vị trí (JSON, dữ liệu thực
của Base)”) và “Đăng xuất” — không topbar, không sidebar, không footer,
không script/CSS nào được nạp (`scripts = []`, `bodyTextLen = 488`).
- Checklist §9.1 (topbar/sidebar/footer ở cùng vị trí như các app khác):
KHÔNG ĐẠT — trang này không dùng shell `_LayoutIndex`.
- Cần điều phối viên xác nhận: đây có phải “home thật” đã được thay trong
phase 2 (“real home replacing temp stub”) hay vẫn là trang tạm? Audit
không tự kết luận là stub hay thiết kế có chủ ý. Nếu là trang tạm thì đây
là lỗi High (trang chủ của app không có điều hướng chuẩn); nếu có chủ ý
thì cần ghi vào hồ sơ thiết kế để các app khác khỏi “chuẩn hóa” nhầm.
- Mức: High nếu là trang tạm; chưa kết luận (cần quyết định của điều phối viên).

**MRQC-03 — Footer vendor tiếng Trung, wording khác SSO**
- Trang đăng nhập MRQC: `Copyright © 2025湖北天天数链技术有限公司本系统软件源代码
许可来源于《天天开源软件（社区版）许可协议》` (38 ký tự CJK) — khác wording
footer SSO (SSO: “mã nguồn của hệ thống này được cấp phép theo…”, MRQC:
“本系统软件源代码许可来源于…”). Tức footer không chỉ còn tiếng Trung mà còn
không thống nhất giữa các app — đúng trường hợp §3.1 yêu cầu “một footer
dùng chung” thay vì sửa lẻ từng trang.
- Checklist §9.1, §9.8. Mức: Low–Medium.

### Sett

**Sett-01 — Trang đăng nhập sạch hơn SSO/MRQC (điểm dương + điểm không nhất quán)**
- Ảnh: `sett/login-mobile.png` (và desktop tương ứng). Form căn giữa gọn,
KHÔNG bị lỗi chồng tiêu đề như SSO-01; “Chào mừng” viết hoa đúng.
Trên mobile logo vẫn hơi cắt ở cạnh trái nhưng form không vỡ.
- Đây là điểm dương của Sett, đồng thời là bằng chứng các app dùng template
đăng nhập không thống nhất (§9.8: cùng một màn hình, 3 app 3 kiểu trình
bày — SSO chồng tiêu đề, MRQC gọn nhưng nền tràn, Sett gọn nhất).
- Mức: thông tin (dùng làm mẫu đối chiếu khi chuẩn hóa login).

**Sett-02 — Footer vendor tiếng Trung, giống MRQC**
- Cùng wording MRQC (38 ký tự CJK). Checklist §9.1, §9.8. Mức: Low–Medium.

**Sett-03 — Trang nội bộ Sett CHƯA audit được (không phải lỗi UI, là khoảng trống bằng chứng)**
- Home, lưới quyết toán ngày nội trú (`DaySettle/InpatientDaySett`), và trang
khu vực (`BookkeepInHos/Index`) đều chưa render được do chặn §0.
- Hệ quả: không xác định được toolbar lưới, chân lưới “Đang xem x–y trên
tổng z dòng”, trạng thái rỗng/lỗi, nút trong dòng, hay hành vi mobile của
bất kỳ lưới Sett nào. Tất cả các mục §9.4 của Sett đều ở trạng thái
“chưa kiểm chứng”, không được tính là đạt hay không đạt.

## 3. Center hosts 8015/8016 — API-only + Swagger UI

- Probe: `GET /` → HTTP 404, body rỗng (0 byte) ở cả 2 host; Firefox báo
`NS_ERROR_NET_EMPTY_RESPONSE` — không có trang web nghiệp vụ nào ở gốc.
- `GET /swagger/index.html` → HTTP 200 (~4,7 KB) ở cả 2 host, render Swagger
UI đầy đủ, không cần đăng nhập:
- AuthCenter 8015 (`NewtouchHIS.AuthenticationCenter` v1.0, OAS3): nhóm
`Auth` — `POST /api/Auth/GetAppToken`, `GetAppFrienAuthToken`,
`UserTokenRefresh`, `GetUserToken`; nhóm `Login` —
`POST /api/Login/UserLogin`. Ảnh: `authcenter-swagger-desktop.png`.
- WebAPI.Manage 8016 (`NewtouchHIS.WebAPI.Manage` v1.0, OAS3): nhóm `Auth`
với mô tả tiếng Việt (“Quản lý xác thực”, “Hệ thống thứ ba lấy Token”, …).
Ảnh: `webapi-manage-swagger-desktop.png`.
- Kết luận: **2 host là API-only; “trang web” duy nhất của chúng là Swagger
UI (tài liệu API), không phải giao diện nghiệp vụ, nên không có gì để
redesign theo §9.** Không cố tạo giao diện cho chúng (đúng quy tắc cứng).
Ghi chú: Swagger UI lộ danh sách endpoint auth mà không cần đăng nhập —
đây là quan sát, không phải kết luận bảo mật; chặng bảo mật quyết định có
nên khóa Swagger ở môi trường production hay không.

## 4. Xác nhận các điểm mở từ DESIGN.md §0/§4/§10

### 4.1. (a) Phiên bản asset static host đang phục vụ — xác định được / không xác định được

Đo live trên trang thật (không suy từ tên file):

| Asset | Kết quả live | Bằng chứng |
|---|---|---|
| jQuery | ✅ **2.1.1** | `$.fn.jquery` trên trang đăng nhập cả 3 app + portal SSO |
| Bootstrap (shell nghiệp vụ/portal) | ✅ **3.3.4** | `$.fn.modal.Constructor.VERSION` trên portal SSO tablet; script `~/js/bootstrap/bootstrap.js` từ static host |
| Bootstrap 5 | ✅ **Không có** trên shell nghiệp vụ/portal | `window.bootstrap` = false; class kiểu Bootstrap 5 (`data-bs-*`, `d-none d-md-block`) không được dùng ở đây — đúng cảnh báo §0: đừng copy class Bootstrap 5 sang trang nghiệp vụ |
| jqGrid (phiên bản) | ❌ **Không xác định được live** | `$.jgrid` = null trên mọi trang đã render (trang đăng nhập + portal không nạp jqGrid); không trang lưới nào render được do chặn §0 |
| Nguồn asset | ✅ **Static host 127.0.0.1:41142** | Mọi script src đều dạng `http://127.0.0.1:41142/js/...` (jquery, bootstrap, framework-ui.js, Newtouch-ui.js, indextab.js, …) |
| Layout độc lập của SSO (`_Layout.cshtml`, Bootstrap v5.1.0 theo §0) | ❌ Không gặp trong audit này | Không trang nào render bằng layout đó trong phạm vi đã chụp; vẫn là điểm cần xác nhận riêng nếu trang đơn nào dùng nó |

### 4.2. (b) Locale jqGrid hiển thị cuối cùng — xác nhận ở mức source, CHƯA xác minh live

Thứ tự nạp (đọc source layout, không sửa):

- **SSO portal** (`Views/Shared/_Index.cshtml`): dòng 25 nạp
`grid.locale-cn.js` (qua static host), dòng 26 nạp `/js/jqgrid/grid.locale-vi.js`
→ locale Việt nạp sau, **dự kiến** thắng trên portal.
- **MRQC, Sett** (`Views/Shared/_LayoutIndex.cshtml`): chỉ nạp
`grid.locale-cn.js` ở dòng 18; locale Việt được nạp ở tầng partial:
`_Index.cshtml` dòng 35 và `_Form.cshtml` dòng 48/53 nạp
`/js/jqgrid/grid.locale-vi.js`.
- Hệ quả: **locale hiển thị cuối cùng phụ thuộc vào việc trang có dùng
partial `_Index`/`_Form` hay không.** Trang dùng partial → tiếng Việt
(nạp sau); trang không dùng partial → chỉ có locale Trung → chuỗi jqGrid
(phân trang, “không tìm thấy bản ghi”, …) hiển thị tiếng Trung.
Đây là rủi ro đã được §9.8 cảnh báo (“xác nhận locale hiển thị cuối cùng
là tiếng Việt và xử lý file locale thừa theo quyết định riêng, không xóa
bừa”).
- **Chưa xác minh live:** không trang lưới nào render được, nên không có
ảnh/bản ghi pager nào chứng minh locale hiển thị thực tế. Lần audit lại
phải chụp pager của một lưới MRQC và một lưới Sett ở desktop và đọc chuỗi
`recordtext`/`emptyrecords` thực tế.

### 4.3. (c) Hành vi sidebar mobile và nút mở rộng — CHƯA xác minh được

- Probe hamburger trên mobile chỉ chạy được trên trang đăng nhập (không có
sidebar): 0 nút ứng cử viên, 0 sidebar — đây là kết quả kỳ vọng, không
phải phát hiện.
- Trên portal tablet 768px, sidebar **luôn hiện** (không phải drawer) nhưng
nhãn bị cắt (SSO-04). Hành vi ở < 992px theo §3.5 (hamburger luôn thấy,
drawer + overlay + khóa cuộn nền + focus trả về nút) **chưa được kiểm chứng
ở bất kỳ app nào**, vì không trang nội bộ nào mở được trên mobile.
- Đây là mục kiểm chứng bắt buộc của lần audit lại, vì “sidebar mobile thiếu
nút mở rộng” vốn đã là vấn đề đã biết trong §10.

### 4.4. (d) Sett: hồ sơ chiến lược responsive A/B/C — CHƯA xác định (hướng phân tích cho lần audit lại)

Không lưới Sett nào render được nên **không gán chiến lược nào** trong báo
cáo này. Hướng phân tích (đề xuất, chưa phải quyết định) cho lần audit lại,
theo nhiệm vụ từng lưới và quy tắc chọn ở §4.1:

| Lưới Sett | Nhiệm vụ (đọc từ tên/khu vực) | Hướng phân tích khi audit lại |
|---|---|---|
| `DaySettle/InpatientDaySett` (quyết toán ngày nội trú) | Duyệt loạt chứng từ theo ngày — cần so sánh ngang nhiều dòng | Ưu tiên xét **A** (cuộn ngang + cột định danh neo) trước, vì mặc định §4.1 là A trừ khi chứng minh được nhiệm vụ là tra cứu từng bản ghi |
| `BookkeepInHos/Index` (ghi sổ nội trú) | Làm việc theo từng bệnh nhân/chứng từ | Ưu tiên xét **B** (dòng → thẻ) nếu audit lại cho thấy thao tác chính là xử lý từng bản ghi; nếu là duyệt loạt thì quay về A |
| Các lưới Sett còn lại trong 22 khu vực | Chưa liệt kê hết (ngoài phạm vi lần này) | Mỗi lưới cần một dòng hồ sơ riêng: nhiệm vụ → chiến lược → lý do; không áp một chiến lược cho mọi lưới |

Lưu ý §4.2: cuộn ngang **bản thân nó không phải lỗi** — chỉ là lỗi khi làm
mất cột định danh, mất tiêu đề, hoặc vỡ bố cục (tràn viewport, đẩy footer,
che nút). Lần audit lại phải phân biệt ba trường hợp này trước khi sửa.

## 5. Đối chiếu checklist §9 (tóm tắt trạng thái)

| Mục §9 | SSO | MRQC | Sett |
|---|---|---|---|
| 9.1 Header (topbar, h1, footer) | ❌ Footer tiếng Trung; h1 portal chưa kiểm chứng | ❌ Footer tiếng Trung; home không có header | ❌ Footer tiếng Trung; nội bộ chưa kiểm chứng |
| 9.2 Sidebar | ❌ Nhãn cắt ở tablet; mobile chưa kiểm chứng | Chưa kiểm chứng | Chưa kiểm chứng |
| 9.3 Form | Chưa kiểm chứng (chỉ form đăng nhập: SSO chồng tiêu đề) | Chưa kiểm chứng | Chưa kiểm chứng (form đăng nhập: sạch nhất) |
| 9.4 Lưới jqGrid | Chưa kiểm chứng | Chưa kiểm chứng | Chưa kiểm chứng |
| 9.5 Nút | Chưa kiểm chứng (nút đăng nhập: 1 nút chính, đúng) | Chưa kiểm chứng | Chưa kiểm chứng |
| 9.6 Thông báo | ❌ Lỗi đăng nhập SSO tiếng Việt, MRQC/Sett tiếng Trung | ❌ Như SSO | ❌ Như SSO |
| 9.7 Mobile | ❌ Login-head tràn 1160px; portal tablet cắt nhãn | ❌ Login-head tràn 825px | ⚠️ Login mobile gọn nhất; nội bộ chưa kiểm chứng |
| 9.8 Thị giác + ngôn ngữ | ❌ Footer + “đang tải” tiếng Trung; menu không dấu | ❌ Footer tiếng Trung, wording khác SSO | ❌ Footer tiếng Trung, wording khác SSO |

Không mục nào ở trên được tính là “đạt” cho trang nội bộ — bảng này chỉ phản
ánh những gì đã đo được; phần lớn là “chưa kiểm chứng”, không phải “đạt”.

## 6. Ngoài phạm vi (ghi nhận, không đụng tới)

- Mâu thuẫn tiền tố chứng từ: không gặp, không đụng tới (đúng quy tắc).
- Tên bệnh viện thật, dữ liệu thật: không tự điền; audit không tạo dữ liệu
bệnh nhân/giả lập nào để chụp màn hình.
- Tài khoản demo/mật khẩu mặc định/secret: không thay đổi; chỉ ghi nhận
khả năng tài khoản bị khóa (§0.1) để điều phối viên xử lý ở chặng bảo mật.
- Windows-only (DCSoft/SSRS/WCF): không gặp trong phạm vi đã render.

## 7. Việc cần làm tiếp (cho điều phối viên, không phải cho worker này)

1. Khôi phục SQL Server local (kiểm tra tiến trình/launcher; worker này không
tự khởi động theo quy tắc cứng).
2. Kiểm tra cờ `Locked` của 000000, mở khóa nếu cần (chỉ sau khi DB hoạt động lại).
3. Chạy lại audit nội bộ cho SSO (portal, Notice, SyncForm), MRQC (home shell,
MedicalCenter, Apply), Sett (home, InpatientDaySett, BookkeepInHos) ở cả
desktop + mobile (+ tablet cho home), với script chờ `networkidle` và chờ
điều hướng 30s; kiểm chứng bổ sung: locale pager jqGrid live, hamburger/
drawer mobile, chiến lược A/B/C từng lưới Sett, WorkSpace iframe ở tablet,
meta viewport của portal.
4. Quyết định về MRQC-02 (Home/Index trần có phải trang tạm không) trước khi
chặng redesign “chuẩn hóa” nó.
5. Quyết định về footer dùng chung tiếng Việt (§3.1/§7) — danh sách trang có
footer tiếng Trung trong phạm vi này: đăng nhập SSO, đăng nhập MRQC, đăng
nhập Sett, portal SSO (cộng chuỗi “đang tải” tiếng Trung trong DOM portal).

---

*Hết báo cáo audit SSO/MRQC/Sett. Không có code nào bị sửa, không có commit
nào được tạo trong chặng này. Script đo đạc: `/tmp/audit_sms.py`,
`/tmp/audit_sms_diag.py`, `/tmp/audit_sms_swagger.py` (log JSON:
`/tmp/audit-sms-log.jsonl`).*
