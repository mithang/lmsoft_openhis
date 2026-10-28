# Báo cáo cuối — Redesign chặng 4 (redeploy + re-sweep) + chặng 5 (chụp toàn bộ) + vá guard recovery.sh

> Base: nhánh V2.2, HEAD `7d30446a` (kiểm chứng local = remote, 11/11 login prod 200, không còn listener STG).
> Chặng 3 đã xong trước đó (per-app redesign, báo cáo `scripts/redesign/perapp-complete-REPORT.md`).
> Chặng 4: 0 commit mới (HEAD không đổi). Các báo cáo chặng: `stage4-b1-REPORT.md`, `stage4-b2-REPORT.md`,
> `shots-s1-CATALOG.md`, `shots-s2-CATALOG.md` — commit cùng báo cáo này.
> Ảnh trong `shots-redesign/` (gitignore, không commit).

## 1. Kết luận tổng thể

- **Chặng 4 hoàn tất:** 12 app có thay đổi chặng 3 đã lên prod đúng build (B1: MRQC, Sett, MRMS, OR; B2: Herp, EMR, HOSP, Base Web, CIS, PDS + Herp API, PDS API copy DLL Infra). SSO, 3 API nhóm B1, EMR API, HOSP API, CIS API, Center không đổi nên chỉ kiểm chứng, không restart thừa. Re-sweep desktop + mobile sạch trong phạm vi chặng 3.
- **Chặng 5 hoàn tất:** **1.641 ảnh** với role cao nhất **000000 (HospAdministrator)** — S1 192 ảnh (SSO/MRQC/Sett/MRMS/OR, tập trung form Thêm/Sửa) + S2 1.449 ảnh (Herp/EMR/HOSP/Base Web/CIS/PDS, liệt kê toàn bộ view source). Chỉ chụp, không submit, không ghi DB demo.
- **Vá guard recovery.sh hoàn tất** (xem §4) và đã kiểm chứng bằng lần chạy no-op toàn bộ: exit 0, không app nào bị restart.
- **Không tuyên bố "sạch tuyệt đối":** phần còn tồn đều có lý do ở §6 — gồm 1 quirk title source có sẵn, thông báo login tiếng Trung còn sót ở 2 app, menu liên app trỏ host vendor không nghe, sidebar mobile toggle chưa được xác nhận trong báo cáo chặng 4, và các quyết định mở vốn đã liệt kê từ sweep cuối.

## 2. Chặng 4 — Redeploy (đúng quy tắc build mới từ postmortem P0)

Quy tắc đã tuân thủ ở mọi app: kiểm tra ps xem prod chạy bin/Debug hay bin/Release trước publish; chỉ build trong worktree staging riêng dưới `~/workspace` (B1: `~/workspace/stage4-b1`, B2: `~/workspace/stage4-b2`), không bao giờ publish trong cây prod; cây từng patch tạm cổng thì không dùng cho build prod; chỉ copy DLL+PDB sau khi kill đúng PID cũ; restart bằng launcher; kiểm chứng login + trang chính sau mỗi app.

| App | Cổng | Config prod | Kết quả |
|---|---|---|---|
| MRQC | 5220 | Release | Login 000000 success, Home mới 200 |
| Sett | 5221 | Release | Login success, Home mới 200; cột `bcje` giữ nguyên |
| MRMS | 5232 | Debug (giữ launcher) | Login success, dashboard title mới |
| OR | 5231 | Debug (giữ launcher) | Login success, Home mới 200 |
| Herp | 5236 | Release | Login success, Home 200 |
| EMR | 5235 | Release | Login success, `/Home/Default` nay 200 (trước chặng 3 là 404) |
| HOSP | 5233 | Release | Login success, Home 200; ChargeCategory giữ treeGrid |
| Base Web | 5238 | Release | Login success, Home 200 |
| CIS | 5234 | Debug (giữ launcher) | Login success, Home 200 |
| PDS | 5237 | Release | Login success, Home 200 |
| Herp API | 5336 | — (copy DLL Infra) | Probe fail-closed 40004, không 500 |
| PDS API | 5337 | — (copy DLL Infra) | Probe fail-closed, không 500 |
| SSO 5208, MRQC.API 5320, MRMS.API 5332, OR.API 5331, EMR API 5335, HOSP API 5333, CIS API 5334, Center 8015/8016 | — | — | Chỉ kiểm chứng (LISTEN, fail-closed đúng baseline), không restart |

Backup DLL cũ: B2 lưu tại `bin/<Cfg>/.bak-stage4b2-20261029/` cho các app của nhóm.

## 3. Chặng 4 — Re-sweep (desktop 1280 + mobile 390, chỉ đọc trên prod)

- Checklist chặng 3 đạt trên toàn bộ trang đổi: frozen columns (+frozenBdiv), 1 nút primary, nút Sửa disabled ban đầu rồi bật khi chọn dòng (đo trực tiếp MRMS MrDept, OR OpStaff, EMR NursingRecord — xem dưới), home responsive không tràn (scrollW = viewport), icon ₫ (fa-cny = 0), PDS StockQuery `#gridYPZL` frozen + 2 nút về default.
- **Mục mở chặng 3 đã ĐO ĐƯỢC — EMR NursingRecord bật-nút-Sửa:** tạo 1 fixture tối thiểu trên STG (`bl_hljl_yb`, chỉ 5 cột NOT NULL, không cần hồ sơ bệnh nhân), dựng EMR STG từ worktree riêng patch cổng (đã hủy worktree sau test) + Redis STG + BaseAPI STG. Kết quả live: 1 dòng, trước click Sửa disabled=true/aria=true → sau click disabled=false/aria=false. Dọn sạch: fixture đã xóa (0 dòng), PID STG đã kill, không còn listener STG.
- **EMR `/Home/Default` với 000000:** trái với giả định root-only trong lệnh chặng 5, trang render đầy đủ dashboard KPI/biểu đồ/thông báo với 000000 và đã chụp (`emr-home-default-desktop.png`). Nhánh root-only coi như không còn là mục mở.
- Các offender MRMS shell (tab bar rộng, vượt mép mobile 3px) là mục ĐÃ BIẾT từ chặng 3, scrollW vẫn = viewport, không phải hồi quy.
- Sự cố giữa phiên: VM restart quét sạch service + xóa /tmp ở cả hai nhóm; recovery.sh khôi phục đủ 26 cổng trên DLL mới và kiểm chứng lại toàn bộ (login đều success cuối phiên).

## 4. Vá guard recovery.sh (lỗ hổng phát hiện từ sự cố P0 chặng 3)

- Vấn đề cũ: section app chỉ restart khi cổng DOWN **và** `prod_app_running(cwd)` sai; một tiến trình prod-cwd kẹt (không nghe cổng prod) sẽ chặn mãi lần restart thật.
- Cách sửa: helper mới `reap_wedged_prod_app <port> <cwd>` gọi đầu mỗi nhánh "cổng DOWN" ở **20/20 section**; chỉ kill tiến trình dotnet có `/proc/<pid>/cwd` khớp đúng cwd prod, KHÔNG nghe cổng prod (đối chiếu PID qua `ss -ltnp`), VÀ tuổi > 600 giây (`ps -o etimes=`). Tuổi ≤ 600s không kill (có thể đang bind chậm); cwd không đọc được coi như không phải prod, không kill. Chế độ `REAP_DRY_RUN=1` chỉ in ra thay vì kill. Quy tắc ghi trong comment script kèm ngày + lý do P0.
- Backup: `~/workspace/recovery.sh.bak-guard-20261029` (đối chiếu: 20 guard cũ, 0 helper mới). Diff thuần cộng thêm 59 dòng, 0 dòng xóa. `bash -n` OK. Dry-run trên 20 app khỏe: danh sách "sẽ kill" rỗng (có ý nghĩa — mỗi app đều có đúng 1 tiến trình dotnet prod-cwd là listener của cổng đó).
- Kiểm chứng cuối của điều phối viên: chạy no-op `recovery.sh` toàn bộ sau khi mọi worker xong — exit 0, PID 4 app đối chứng (Sett 2646, MRMS 2718, OR 2787, PDS 3909) không đổi, login sau chạy vẫn 200. Giới hạn còn lại: lần kiểm chứng thực tế đầu tiên của logic reap vẫn là lần watchdog tới gặp đúng kịch bản cổng tắt + tiến trình kẹt tuổi > 10 phút (chưa tái hiện được mà không phá app đang chạy).

## 5. Chặng 5 — Chụp toàn bộ với role 000000 (tổng 1.641 ảnh)

Chỉ dùng tài khoản 000000 (HospAdministrator) — không thử xzadmin hay admin. Chỉ chụp viewport (desktop 1280×900, mobile 390×844), không full_page. Không submit, không ghi DB demo; không dựng instance STG nào ở chặng 5 (mọi ảnh đều chụp trên prod). Ảnh trong `shots-redesign/` (gitignore).

| App | Desktop | Mobile | Tổng | Ghi chú phương pháp |
|---|---|---|---|---|
| SSO 5208 | 15 | 15 | 30 | Form Thêm/Sửa Menu, Vai trò, Vai trò người dùng; dialog qua tab shell |
| MRQC 5220 | 10 | 10 | 20 | Form Thêm Hạng mục (tham số thật bllx=yzd) |
| Sett 5221 | 40 | 40 | 80 | Form Thêm 7 trang đủ 2 viewport |
| MRMS 5232 | 7 | 7 | 14 | Form có dữ liệu; 3 trang 404 (route thuộc SSO) chụp đúng trạng thái |
| OR 5231 | 24 | 24 | 48 | Form Thêm 5 trang + Mẫu thu phí; Form Sửa có dữ liệu 3 trang |
| Herp 5236 | 102 | 103 | 205 | Liệt kê view source 93/95 |
| EMR 5235 | 82 | 83 | 165 | Liệt kê view source 81/86; gồm Home/Default |
| Base HOSP 5233 | 155 | 156 | 311 | Liệt kê view source 154/165 |
| Base Web 5238 | 41 | 42 | 83 | Liệt kê view source 40/41 |
| CIS 5234 | 219 | 206 | 425 | Liệt kê view source 213/261 desktop, 199/261 mobile |
| PDS 5237 | 128 | 132 | 260 | Liệt kê view source 127/162 desktop, 130/162 mobile |

Danh mục chi tiết: `shots-s1-CATALOG.md` (339 dòng) + `shots-s2-CATALOG.md` (1.285 dòng) — liệt kê từng ảnh + từng màn hình bỏ qua kèm lý do. Đối soát cuối S2: 0 file ảnh lệch khỏi danh mục, 0 file đĩa không có trong danh mục.

Màn hình bỏ qua có lý do (không bịa, không chụp lén bằng dữ liệu giả):
- 116 route view thiếu ít nhất một khung hình (lý do từng route trong danh mục S2 mục 3.1) + 11 form/dialog không mở được dù thử nhiều lần (OR Sửa Phòng mổ + Nhân sự mở trống; Sett Sửa 7 chức năng do lưới demo rỗng; MRQC Sửa Hạng mục + dialog Chấm điểm MedicalCenter do danh sách bệnh nhân rỗng).
- Menu liên app: S1 ghi 176 mục qua shell SSO/MRMS trỏ về host gốc vendor (8001/8002/8003, không nghe) nên không mở được màn hình đích — chỉ chụp 1 ảnh mẫu tab launcher mỗi app; S2 ghi 89 liên kết tương tự (trên host EMR chỉ 4 là của EMR, còn lại đóng kết nối; trên host PDS cả 89 đều đóng kết nối). Đây là cây menu liên app, không phải điều hướng riêng của từng app.
- Sửa sai quan trọng (S1): 10 ảnh "Sửa" OR từ forms3 đã bị xóa sau kiểm chứng — chúng thực chất là form Thêm trống (khóa "1" chỉ là chỉ số dòng jqGrid, Thêm/Sửa giống hệt nhau từng byte). 6 ảnh Sửa OR hiện tại là bản thật, không ghi nhãn "Sửa" cho ảnh form trống.
- Màn hình stub chụp đúng trạng thái hiện tại: Herp Báo cáo (thông báo SSRS 13 .rdl), EMR MedicalRecord Index (banner stub DCSoft), các trang in báo cáo CIS/PDS.
- Caveat: dialog standalone lỗi `top.layer is undefined` nên form chụp qua URL Form trực tiếp; dialog layer thật chỉ chụp được qua tab shell SSO (đã đủ).
- Phát hiện ngoài phạm vi sửa: sidebar HOSP của 000000 chỉ hiện "Thông tin cá nhân" nhưng toàn bộ trang dữ liệu nền vẫn render và đã chụp qua danh sách view.

Sự cố chặng 5 và cách xử lý:
- SQL Server chết lặp lại nhiều lần (VM restart + chết khi tải cao); mỗi lần chạy `recovery.sh` đưa 1433 lên lại; shooter tự kiểm tra 1433 mỗi 20 route và tự recovery + đăng nhập lại (cứu 2 pass CIS mobile, PDS desktop). S1 khởi động lại SQL một lần bằng đúng phương pháp recovery.sh §2.
- VM restart xóa /tmp giữa phiên S2: script/manifest tạm mất; danh mục tái tạo từ 588 ảnh còn trên đĩa (đối soát 0 lệch) rồi chụp bổ sung; script/manifest mới giữ tại `~/workspace/s2_work/` (ngoài repo).
- Firefox crash sau pass dài (S2): 370 bản ghi bỏ qua sai đã loại và chụp lại sau khi sửa shooter khởi động lại browser mỗi 40 route. S1: browser chết khi tạo mobile context ở forms4 đầu (mất manifest MRQC/OR) → sửa script lưu manifest sau mỗi viewport và chạy lại; tab crash ở trang báo cáo OR mobile (ảnh chính không ảnh hưởng).
- **Vụ S2 kill nhầm Firefox của S1 (~21:07, PID 29059):** đã xác nhận là pass MRQC forms4 của S1, cho 0 output (không manifest, 0 file mới). S1 kiểm tra lại trực tiếp: mọi khoảng trống còn lại đều do dữ liệu/trạng thái thật (lưới rỗng, không có URL Form, nút Duyệt là thao tác ghi nên cấm bấm) — **không còn gì thiếu, không cần chạy lại.** S2 không chạm gì thêm vào tiến trình S1 sau đó; các lần chạy tiếp theo của S1 đều hoàn thành bình thường. Vụ việc coi như đóng.
- Sett 5221 sập giữa lần chạy forms4 mobile của S1 (kẹt ~14 phút) → đã giết, chạy lại sau khi app hồi, đủ 7 form mobile.

## 6. Còn tồn và lý do (không báo đạt phần chưa xong)

1. **Title InpatientDaySett mang "OutpatientDaySett"** — quirk có sẵn trong source (dòng 2, diff HEAD không đụng title), worker B1 kiểm chứng, không tự sửa trong chặng redeploy; ứng viên đợt sửa sau.
2. **Thông báo login thất bại trên MRQC và Sett vẫn là tiếng Trung** (SSO đã dịch) — quan sát ngoài phạm vi sửa của chặng 4/5.
3. **Menu liên app trỏ host vendor 8001/8002/8003 không nghe** — các màn hình đích không mở được từ menu này; cần quyết định phạm vi (đây có phải host của bộ ứng dụng gốc không nằm trong diện port hay không) trước khi sửa/xóa liên kết.
4. **Sidebar mobile toggle** — mục deferred từ chương trình redesign ban đầu, không được xác nhận trong báo cáo chặng 4; cần đối chiếu lại trước khi tuyên bố xong.
5. **Cột Sett `bcje` giữ nguyên** — chờ user xác nhận (quyết định chặng 3, nhắc lại).
6. **Herp `ddsj` giữ nguyên; HOSP ChargeCategory giữ treeGrid** — quyết định chặng 3.
7. **Mâu thuẫn receipt-prefix chưa thống nhất** (luồng mới tiền tố Việt, luồng hardcode tiền tố cũ) — ngoài phạm vi UI, nhắc lại từ sweep cuối.
8. **22 quyết định mở từ sweep cuối + dòng chữ vendor footer SSO Home/MRQC Login** — chỉ liệt kê, không tự quyết thay user (CTR-05, schema kcyjz/bảng thiếu, OR DeleteForm, credential client nội bộ, tên bệnh viện thật, xoay secret, Debug→Release, Sett ghi 184 stub, dữ liệu thật + UAT, v.v.).
9. **5 app vẫn chạy bản Debug** (MRMS Web, OR Web/API, CIS Web/API) vì launcher trỏ Debug — chuẩn hóa Release chờ quyết phase 8.
10. **Lần kiểm chứng thực tế đầu tiên của logic reap** trong recovery.sh vẫn chờ lần watchdog gặp đúng kịch bản (xem §4).

## 7. Nhắc lại postmortem P0 chặng 3 (để không lặp lại)

Nguyên nhân: 3 app OR/MRMS/CIS ở prod chạy build Debug từ chính cây repo; worker chặng 3 chạy `dotnet publish -c Debug` với giả định prod chạy Release, nhưng publish đã ghi đè `bin/Debug` dưới tiến trình prod đang chạy → login prod 500 `BadImageFormatException: Bad IL format` (OR 5231, MRMS 5232, CIS 5234), trong khi cổng vẫn LISTEN nên watchdog không phát hiện. DLL trên đĩa vẫn tốt (cùng bản publish đã chạy STG đạt) — lỗi nằm ở trạng thái tiến trình sau khi file dưới nó bị thay. Khôi phục: restart 3 app prod từ DLL trên đĩa (đã xong trước chặng 4, 11/11 login 200 khi bắt đầu chặng 4). Bài học thành quy tắc cứng chặng 4: chỉ build trong worktree riêng dưới `~/workspace`; không bao giờ publish trong cây prod; cây từng patch tạm cổng thì hủy và dùng cây mới cho build prod; chỉ copy DLL sau khi kill đúng PID; kiểm chứng login + trang chính sau mỗi app. Vá guard recovery.sh (§4) là lớp phòng thủ thứ hai cho trường hợp tiến trình kẹt tương tự.

## 8. Bằng chứng và file

- `scripts/redesign/stage4-b1-REPORT.md`, `stage4-b2-REPORT.md` (chặng 4, 2 nhóm app).
- `scripts/redesign/shots-s1-CATALOG.md`, `shots-s2-CATALOG.md` (danh mục ảnh chặng 5).
- `shots-redesign/` — 1.641 ảnh PNG (gitignore, không commit).
- `~/workspace/recovery.sh` (đã vá guard) + backup `~/workspace/recovery.sh.bak-guard-20261029`.
- Manifest các lần chạy S1: `~/workspace/shot/s1_manifest_*.json`; script/manifest S2: `~/workspace/s2_work/` (ngoài repo).
- Tiến trình tổng hợp: `roles-shots-progress.md` (mục mới, không xóa cũ).
