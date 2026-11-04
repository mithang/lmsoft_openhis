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

## U3 — Triển khai 10 portal còn lại + overlay prod (ĐANG CHẠY)

3 worker song song: R1 (SSO overlay live + CIS + EMR), R2 (Herp + MRMS + OR), R3 (PDS + MRQC + Sett + Base Web + Base HOSP Web). Mỗi portal: goodcopy cổng tạm trước/sau + kiểm chứng hành vi, xác định vị trí CSS đang được phục vụ bằng thực nghiệm, overlay file CSS mới vào đúng vị trí prod (backup file cũ + manifest md5), kiểm chứng live trên cổng prod rồi trả về shed. Worker không commit code.

## Triển khai 11 portal (Phase 7)

| Portal | Cổng | Trạng thái | Commit | Ảnh trước/sau |
|---|---|---|---|---|
| SSO | 5208 | Pilot xong (cổng tạm, chưa overlay prod) | `c94a9134` | `shots-uiux-pilot/` |
| CIS | 5234 | Chưa làm | — | — |
| EMR | 5235 | Chưa làm | — | — |
| Herp | 5236 | Chưa làm | — | — |
| MRMS | 5232 | Chưa làm | — | — |
| OR | 5231 | Chưa làm | — | — |
| PDS | 5237 | Chưa làm | — | — |
| MRQC | 5220 | Chưa làm | — | — |
| Sett | 5221 | Chưa làm | — | — |
| Base Web | 5238 | Chưa làm | — | — |
| Base HOSP Web | 5233 | Chưa làm | — | — |

Quy tắc triển khai: token dùng chung qua assets + sync (không sửa lẻ từng portal trừ `login.css`/điểm riêng đã ghi trong PORTAL_INVENTORY.md §4); mỗi portal build 0 lỗi + screenshot trước/sau + kiểm chứng hành vi rồi mới sang portal tiếp theo; KHÔNG overlay prod trong vòng pilot — triển khai prod theo quy tắc publish an toàn (từng file tại chỗ, giữ backup, kiểm chứng login thật) ở vòng triển khai riêng.

## Chụp toàn bộ màn hình/chức năng (sau khi hoàn thành modernization)

Yêu cầu user 04/11/2026 ~08:54 ICT: chụp TOÀN BỘ chức năng/màn hình theo từng portal, đủ 11 portal và đủ mọi chức năng trong từng portal; lập danh mục route/menu từng portal, chụp từng màn hình đã đăng nhập (000000), đối chiếu danh mục, ghi rõ màn hình nào không chụp được và lý do thật, không bịa, không bỏ sót thầm lặng. Ảnh lưu theo từng portal dưới `~/workspace/shots-uiux/<portal>/`.

| Portal | Tổng route/menu | Đã chụp | Chưa chụp được (lý do thật) |
|---|---|---|---|
| SSO | — | 0 | — |
| CIS | — | 0 | — |
| EMR | — | 0 | — |
| Herp | — | 0 | — |
| MRMS | — | 0 | — |
| OR | — | 0 | — |
| PDS | — | 0 | — |
| MRQC | — | 0 | — |
| Sett | — | 0 | — |
| Base Web | — | 0 | — |
| Base HOSP Web | — | 0 | — |

**Tổng ảnh: 0. Độ phủ: chưa đo.**

## Limitation / mục chờ quyết định (không tự quyết, không bịa)

- Đổi stack (Bootstrap 4/5, jQuery mới) — ngoài phạm vi, cần quyết định riêng.
- Sửa static host 41142 hoặc file `framework-ui.css`/`framework-theme.css` — cấm trong chương trình này.
- Mọi cải tiến giao diện đòi đổi workflow/quyền/hành vi — giữ nguyên, ghi vào đây khi gặp.
- Màn hình cần dữ liệu/quyền mà staging chặn (credential STG chưa có quyết định) — ghi rõ ở bảng chụp ảnh, không chụp giả.
- Nguồn gốc 2 file lạ không phần mở rộng trong `Views/Login/` của Base Web/Base HOSP — việc kiểm tra riêng, ngoài chương trình này.
- Lỗi hành vi có sẵn phát hiện ở pilot (vòng sửa logic riêng, không thuộc UI-only): tìm kiếm SysRole không lọc (GetPagintionList bỏ qua keyword); nút next phân trang SysRole không sang trang. Form SysRole không đạt sự kiện `load` (request nền treo) ở cả 2 pha — ghi nhận, chưa rõ nguyên nhân.
- Shell SSO `/Home/Index` không nạp token: muốn đổi mới shell này phải đụng layout/route — cần quyết định riêng; hiện chỉ SSOIndex hưởng token qua `redesign-portal.css`.
- PDS thiếu jQuery core ở runtime: chờ vòng U3 xác minh (nhóm R3).
