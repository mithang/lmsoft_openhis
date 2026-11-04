# Tiến độ refactor giao diện — OpenHIS Visual UI/UX Modernization

> Lệnh user 04/11/2026: chạy liên tục theo vòng, mỗi vòng áp dụng lại prompt UI/UX; tự làm, tự duyệt, tự kiểm tra, KHÔNG chờ duyệt ở bất kỳ chặng nào (duyệt bằng screenshot trước/sau + kiểm tra hành vi). Không bao giờ tuyên bố hoàn thành 10.000 vòng. Khi 3 vòng liên tiếp không còn việc không-cần-quyết-định-mới thì PARKED và báo rõ.
> Trạng thái vòng lặp cũ (research→code, C195–C197) PARKED từ 03/11/2026 — không liên quan chương trình này.

## U1 — Phase 1: Khảo sát (2026-11-04, HOÀN THÀNH)

- 3 worker chỉ-đọc song song, 0 sửa source, 0 build, 0 commit trong khảo sát. Bằng chứng: `~/workspace/uiux-phase1/inventory-sso-cis-emr.md` (711 dòng), `inventory-herp-mrms-or.md` (645 dòng), `inventory-pds-mrqc-sett-base.md` (459 dòng).
- Kết luận: 1.564 file `.cshtml`; layout thật là `_Index`/`_Form`; CSS khung ở static host 41142 ngoài repo; điểm đặt design system là `scripts/redesign/assets/` + `sync-assets.sh`; `redesign-tokens.css` giống hệt nhau ở cả 11 portal (md5 `e14e0ef8…`); stack đóng Bootstrap 3.3.4 / jQuery 2.1.1 / jqGrid 5.1.0.
- 3 file docs này được tạo trong cùng vòng U1 (commit riêng, xem git log).

## U2 — Phase 2: Đối chiếu runtime + Pilot (2026-11-04, HOÀN THÀNH)

- Đối chiếu runtime: host 41142 đang phục vụ **bản run-copy** `lmsoft_openhis_run/Newtouch.HIS.Static` (md5 body HTTP trùng run-copy 6/6 file, khác bản gốc 4/6). Không sửa static host.
- Trang pilot: **SSO `/SysManage/SysRole/Index`** (lưới jqGrid, 5 vai trò demo, toolbar 4 nút + tìm kiếm + phân trang) + form `/SysManage/SysRole/Form` cùng portal. Chạy cổng tạm 25211, DB `*_STG`, đăng nhập thật 000000, 0 ghi demo.
- Thay đổi: CHỈ CSS, 14 file, `+1388/−12` (commit `c94a9134`, push đã kiểm chứng `ls-remote`): mở rộng `scripts/redesign/assets/redesign-tokens.css` (+111/−1, 8 token mới từ palette đang chạy) + `redesign-portal.css` (+28, shell SSOIndex), sync sang 11 portal (md5 tokens giống hệt 11/11). Build SSO Release 0 lỗi, DLL không đổi. Không sửa Razor/controller/model/API/DB/auth.
- Bằng chứng: `~/workspace/shots-uiux-pilot/before|after/` (login/shell/pilot/form/ssoindex). Khác biệt thị giác CÓ ở lưới/form (header lưới đậm + vạch dưới 2px `#00a0ea`, nền pager, nhãn form đậm, panel bo 6px + bóng); chiều cao hàng giữ 30,5px, mật độ dữ liệu không đổi. Hành vi trước=sau: 5 hàng/dữ liệu giống hệt, 6 input form giống hệt, menu 30 link shell / 109 link SSOIndex giống hệt, login md5 giống hệt. LƯU Ý: tìm kiếm SysRole không lọc + nút next phân trang không sang trang là lỗi hành vi CÓ SẴN (đọc code: `GetPagintionList` bỏ qua keyword), KHÔNG sửa vì ngoài phạm vi UI-only — ghi vào limitation.
- Chưa xác minh: PDS thiếu jQuery core ở runtime (chỉ có bằng chứng tĩnh); shell SSO `/Home/Index` không nạp token nên không hưởng đổi mới (xem limitation).

## U3 — Triển khai 10 portal còn lại + overlay prod (2026-11-04, HOÀN THÀNH, đã kiểm chứng)

- 3 worker song song (R1: SSO/CIS/EMR; R2: Herp/MRMS/OR; R3: PDS/MRQC/Sett/Base/Base HOSP). Không commit code trong U3 (CSS đã commit ở c94a9134). Không sửa DLL/deps.json, không sửa shed-apps.txt/recovery.sh/MEMORY.md, không động static host.
- Cơ chế phục vụ CSS xác nhận bằng thực nghiệm ở cả 3 nhóm: CSS được phục vụ từ **project `wwwroot` qua manifest static-web-assets** (`ContentRoots` trỏ tới project `wwwroot`); thư mục bin không có `wwwroot` (trừ bản sao chết của SSO). Cổng tạm thật không khả thi vì `UseUrls` hardcode thắng mọi override — các lần chạy đều bind cổng prod trong lúc shed, 1 process tại 1 thời điểm, kill đúng PID, đã trả về shed.
- Mỗi portal: backup CSS cũ + manifest md5, goodcopy trước/sau + kiểm chứng hành vi (lưới/form/menu), overlay đúng vị trí phục vụ, kiểm chứng live trên cổng prod. Ảnh: `~/workspace/shots-uiux-rollout/<portal>/{before,after,live}/`; backup: `~/workspace/css-backup-u3/`.
- Đối chiếu cuối vòng (điều phối viên): **11/11 file tokens = md5 mới `93ccb53c`**, khớp HEAD; `git status` chỉ còn file bẩn có từ trước; 10 cổng shed đã xuống, SSO 5208 vẫn chạy.
- Đóng 2 bất thường Phase 1: (1) PDS jQuery — jQuery 2.1.1 được nạp động bởi `jquery.choose.js` qua `document.write` (DOM live có, HTML nguồn không) — không phải lỗi, không sửa. (2) Nguồn menu Base Web — markup CỨNG trong `Views/Home/Index.cshtml`, `GetClientsDataJson` không trả `authorizeMenu` — đã xác minh runtime.
- Ghi nhận mới (không sửa, xem limitation): MRQC `/QualityControlManage/MedicalCenter/Index` báo jQuery 1.7.2 (file `/Scripts/jquery.min.js` cũ nạp cuối, ghi đè 2.1.1); Base HOSP sidebar trống khi chạy độc lập (authorizeMenu trống); nhiều lưới STG 0 hàng là trạng thái dữ liệu thật (so sánh hành vi vẫn đầy đủ ở mức khung/toolbar/tìm kiếm/form).

## Triển khai 11 portal (Phase 7)

| Portal | Cổng | Trạng thái | Commit | Ảnh trước/sau |
|---|---|---|---|---|
| SSO | 5208 | Xong: pilot + overlay live, không restart | `c94a9134` | `shots-uiux-pilot/` + `shots-uiux-rollout/sso/` |
| CIS | 5234 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/cis/` |
| EMR | 5235 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/emr/` |
| Herp | 5236 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/herp/` |
| MRMS | 5232 | Xong: before/after/live + đổi skin | — (CSS ở `c94a9134`) | `shots-uiux-rollout/mrms/` |
| OR | 5231 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/or/` |
| PDS | 5237 | Xong: before/after/live + kết luận jQuery | — (CSS ở `c94a9134`) | `shots-uiux-rollout/pds/` |
| MRQC | 5220 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/mrqc/` |
| Sett | 5221 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/sett/` |
| Base Web | 5238 | Xong: before/after/live + nguồn menu | — (CSS ở `c94a9134`) | `shots-uiux-rollout/base/` |
| Base HOSP Web | 5233 | Xong: before/after/live | — (CSS ở `c94a9134`) | `shots-uiux-rollout/basehosp/` |

Quy tắc triển khai: token dùng chung qua assets + sync (không sửa lẻ từng portal trừ `login.css`/điểm riêng đã ghi trong PORTAL_INVENTORY.md §4); mỗi portal build 0 lỗi + screenshot trước/sau + kiểm chứng hành vi rồi mới sang portal tiếp theo; KHÔNG overlay prod trong vòng pilot — triển khai prod theo quy tắc publish an toàn (từng file tại chỗ, giữ backup, kiểm chứng login thật) ở vòng triển khai riêng.

## Chụp toàn bộ màn hình/chức năng (sau khi hoàn thành modernization)

Yêu cầu user 04/11/2026 ~08:54 ICT: chụp TOÀN BỘ chức năng/màn hình theo từng portal, đủ 11 portal và đủ mọi chức năng trong từng portal; lập danh mục route/menu từng portal, chụp từng màn hình đã đăng nhập (000000), đối chiếu danh mục, ghi rõ màn hình nào không chụp được và lý do thật, không bịa, không bỏ sót thầm lặng. Ảnh lưu theo từng portal dưới `~/workspace/shots-uiux/<portal>/`.

| Portal | Tổng route/menu | Đã chụp | Chưa chụp được (lý do thật) | Ảnh trên đĩa |
|---|---|---|---|---|
| SSO | 17 | 17 | 0 | 19 |
| CIS | 233 | 233 | 0 | 238 |
| EMR | 72 | 72 | 0 | 76 |
| Herp | 102 | 102 | 0 | 131 |
| MRMS | 57 | 57 | 0 | 75 |
| OR | 37 | 37 | 0 (3 ảnh là JSON lỗi view-not-found) | 50 |
| PDS | 168 | 135 | 33 route HTTP 404 thân rỗng (28 không có action/controller, 5 có action+view vẫn 404) | 140 |
| MRQC | 11 | 11 | 0 | 17 |
| Sett | 491 | 445 | 46 route HTTP 404 thân rỗng (view tồn tại nhưng không có action/controller phục vụ) | 447 |
| Base Web | 42 | 41 | 1 route chết 404 (`/Organize/Applications`, view mồ côi) | 43 |
| Base HOSP Web | 167 | 162 | 5 route chết 404 (không có action/tham chiếu trong code) | 173 |

**U4 DONE (2026-11-04, điều phối viên + 5 worker, đã kiểm chứng độc lập): tổng danh mục 1.397 route, đã chụp 1.312, chưa chụp 85 (tất cả đều là route chết 404 có bằng chứng curl + trình duyệt, liệt kê từng mục trong file COVERAGE của từng portal). Tổng ảnh trên đĩa: 1.409 file PNG (gồm login/home/menu-nav/biến thể có id thật). Chụp bằng Playwright + Firefox, viewport 1366×900, đăng nhập thật 000000, DB `*_STG`, 0 ghi DB demo, không sửa code; mọi cổng module đã trả về shed, 6 cổng lõi nguyên vẹn. Danh mục + kết quả chi tiết từng route: `~/workspace/shots-uiux/<portal>/COVERAGE.md` (portal trong bảng: sso, cis, emr, herp, mrms, or, pds, mrqc, sett, base-web, base-hosp). Mẫu số = union route GET-view trong code + link menu runtime, khử trùng; partial/fragment không route trực tiếp liệt kê riêng trong COVERAGE, không tính là màn hình. Bất thường mới phát hiện trong U4 (chỉ ghi, không sửa — chi tiết trong COVERAGE từng portal): logo/chữ tiếng Trung trên trang login các portal; chữ "chào mừng"/"đăng nhập" chồng lấn ở login SSO/Base HOSP; Base HOSP sidebar trống khi chạy độc lập; shell SSO/EMR `/Home/Index` không nạp token; MRQC MedicalCenter chạy jQuery 1.7.2 (ghi đè 2.1.1); CIS logo login chữ Trung Quốc + lỗi nền NullReferenceException ở 2 API lưới; 9 route Sett trả JSON lỗi thay vì trang; 13 route Sett trắng hoàn toàn khi mở trực tiếp (fragment view); Form SysRole/SysModule SSO không nạp dữ liệu với keyValue thật.**

## Limitation / mục chờ quyết định (không tự quyết, không bịa)

- Đổi stack (Bootstrap 4/5, jQuery mới) — ngoài phạm vi, cần quyết định riêng.
- Sửa static host 41142 hoặc file `framework-ui.css`/`framework-theme.css` — cấm trong chương trình này.
- Mọi cải tiến giao diện đòi đổi workflow/quyền/hành vi — giữ nguyên, ghi vào đây khi gặp.
- Màn hình cần dữ liệu/quyền mà staging chặn (credential STG chưa có quyết định) — ghi rõ ở bảng chụp ảnh, không chụp giả.
- Nguồn gốc 2 file lạ không phần mở rộng trong `Views/Login/` của Base Web/Base HOSP — việc kiểm tra riêng, ngoài chương trình này.
- Lỗi hành vi có sẵn phát hiện ở pilot (vòng sửa logic riêng, không thuộc UI-only): tìm kiếm SysRole không lọc (GetPagintionList bỏ qua keyword); nút next phân trang SysRole không sang trang. Form SysRole không đạt sự kiện `load` (request nền treo) ở cả 2 pha — ghi nhận, chưa rõ nguyên nhân.
- Shell SSO `/Home/Index` không nạp token: muốn đổi mới shell này phải đụng layout/route — cần quyết định riêng; hiện chỉ SSOIndex hưởng token qua `redesign-portal.css`.
- PDS thiếu jQuery core ở runtime: chờ vòng U3 xác minh (nhóm R3). → ĐÃ ĐÓNG ở U3: jQuery 2.1.1 nạp động qua `jquery.choose.js` + `document.write`, không phải lỗi.
- Ghi nhận mới ở U3 (không sửa, cần quyết định riêng nếu muốn động vào): MRQC trang MedicalCenter báo jQuery 1.7.2 do `/Scripts/jquery.min.js` cũ nạp cuối ghi đè 2.1.1; Base HOSP sidebar trống khi chạy độc lập (authorizeMenu trống); shell SSO `/Home/Index` và EMR `/Home/Index` không nạp token (chỉ SSOIndex/EMR Default có) — muốn phủ token cho 2 shell này phải đụng layout/route.
- Giới hạn kiểm chứng U3: cổng tạm thật không làm được do `UseUrls` hardcode (chạy trên cổng prod đang shed, cô lập tuần tự); nhiều lưới STG 0 hàng là dữ liệu thật — so sánh hành vi ở mức khung/toolbar/tìm kiếm/form, "lưới có dữ liệu" chỉ đạt ở một số trang (SSO SysRole 5 hàng, CIS VisitDeptSet 2, EMR MedRecordTemplate 15, MRMS MrDept 37, OR OpRoom 2, PDS FypzIndex 2).

## U5 — Sửa lỗi thị giác thuần túy do U4 ghi nhận (2026-11-04, HOÀN THÀNH, đã kiểm chứng độc lập bởi điều phối viên)

- Phạm vi: CHỈ CSS/text trình bày, giữ 100% hành vi. Mọi lỗi nền/hành vi (85 route 404, shell không nạp token, sidebar trống, NRE, Form không nạp dữ liệu, route Sett trắng/JSON lỗi, jQuery ghi đè) đều loại trừ, chỉ ghi vào mục chờ quyết định.
- **U5-SHELL — carousel WorkSpace SSO** (commit `45e4030c`, 1 file `HIS.SSO/wwwroot/css/bootstrap-extensions.css` +26 dòng, chỉ `#myCarousel`, desktop ≥769px, giữ nguyên bản sửa mobile): lỗi có từ trước redesign (ảnh before/after `shell.png` chỉ khác 1 pixel; harness đo tràn +177px do `.app-item width:20%` + `.label white-space:nowrap`). Sau sửa: tràn 0px ở 1176px và 1366px, mobile giữ nguyên. Điều phối viên chụp live sau sửa (`~/workspace/u5-shell/after/live-home-*.png`, đăng nhập thật 000000): chữ không còn tràn sang thẻ kề; SSO không restart, HTTP md5 CSS `65fd6354…`, `ls-remote` đã kiểm chứng.
- **U5-LOGIN — chồng chữ tab login** (commit `e797eec2`, 3 file `login.css`, mỗi file +3/−1: `.lo_mod width:119px → 200px` + `white-space:nowrap`, giữ nguyên text/ID/handler/JS): sửa 3/3 portal bị ảnh hưởng (SSO, Base Web, Base HOSP); 8 portal còn lại tab "Chào mừng" ngắn nên không chồng, không sửa. Điều phối viên kiểm chứng live cả 3 cổng (số đo DOM: tab 46px/1 dòng, không chồng; đăng nhập thật 000000 vào `/Home/Index`; HTTP md5 SSO `1cdaafdc…`, Base `243a828c…`; cổng shed đã trả về đủ 20/20). Build Release 0 lỗi cả 3 project.
- **Chữ Hán trang login**: 0 ký tự Hán trong markup 11 view login + partial `_FrontReference` (đã quét lại). Logo là ảnh chứa chữ Hán khắc trong ảnh — KHÔNG sửa/thay, chờ quyết định: `NewTouch.png` (10 portal, `天天开源`), `001.png` (Base Web, `应用后台管理系统`), `bj-01.jpg` bản B (Herp/MRMS/OR/MRQC/Sett, ký tự `云` mờ), `bj-01.jpg` bản Base-Web (chữ Hán nhỏ trên huy hiệu). Tên font `微软雅黑`/`宋体` trong `login.css` là định danh font, giữ nguyên.
- **Ghi nhận mới, chưa sửa, cần quyết định**: (1) Sidebar chung bị cắt nhãn dài ở EMR/MRMS/PDS/SSO — do `framework-ui.css` (cấm sửa), cần quyết định cơ chế override chung. (2) Ứng viên PDS Default (thẻ KPI tràn đáy ở bề rộng iframe) và EMR Default (danh sách chồng cột ngày) — cần worker có quyền sửa view + restart cổng shed + đăng nhập live. (3) Nhãn "truy cập nhanh" SSO xuống dòng là quirk đọc được, giữ nguyên.
- Trạng thái sau U5: HEAD `e797eec2` = `origin/V2.2`; cây chính `openhis-core` còn 4 file CSS chưa commit nhưng nội dung khớp đúng remote tip (overlay phục vụ prod) + file bẩn có từ trước `roles-shots-progress.md`. Vòng tiếp theo chỉ khi có dữ liệu/quyết định mới hoặc user ra lệnh mới — KHÔNG phải hoàn thành 10.000 vòng.
