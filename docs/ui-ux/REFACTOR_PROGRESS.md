# Tiến độ refactor giao diện — OpenHIS Visual UI/UX Modernization

> Lệnh user 04/11/2026: chạy liên tục theo vòng, mỗi vòng áp dụng lại prompt UI/UX; tự làm, tự duyệt, tự kiểm tra, KHÔNG chờ duyệt ở bất kỳ chặng nào (duyệt bằng screenshot trước/sau + kiểm tra hành vi). Không bao giờ tuyên bố hoàn thành 10.000 vòng. Khi 3 vòng liên tiếp không còn việc không-cần-quyết-định-mới thì PARKED và báo rõ.
> Trạng thái vòng lặp cũ (research→code, C195–C197) PARKED từ 03/11/2026 — không liên quan chương trình này.

## U1 — Phase 1: Khảo sát (2026-11-04, HOÀN THÀNH)

- 3 worker chỉ-đọc song song, 0 sửa source, 0 build, 0 commit trong khảo sát. Bằng chứng: `~/workspace/uiux-phase1/inventory-sso-cis-emr.md` (711 dòng), `inventory-herp-mrms-or.md` (645 dòng), `inventory-pds-mrqc-sett-base.md` (459 dòng).
- Kết luận: 1.564 file `.cshtml`; layout thật là `_Index`/`_Form`; CSS khung ở static host 41142 ngoài repo; điểm đặt design system là `scripts/redesign/assets/` + `sync-assets.sh`; `redesign-tokens.css` giống hệt nhau ở cả 11 portal (md5 `e14e0ef8…`); stack đóng Bootstrap 3.3.4 / jQuery 2.1.1 / jqGrid 5.1.0.
- 3 file docs này được tạo trong cùng vòng U1 (commit riêng, xem git log).

## U2 — Phase 2: Đối chiếu runtime + Pilot (ĐANG CHUẨN BỊ)

- [ ] Đối chiếu host 41142 đang chạy thuộc bản sao static nào (`lmsoft_openhis/` hay `lmsoft_openhis_run/`); xác minh bất thường PDS thiếu jQuery core; xác minh nguồn menu runtime của Base Web.
- [ ] Chọn 1 trang đại diện (trang danh sách jqGrid có dữ liệu demo, thuộc portal có shell) — ghi lý do chọn.
- [ ] Chụp baseline (trước) của trang pilot + trang form cùng portal.
- [ ] Mở rộng token trong `scripts/redesign/assets/`, sync, build 0 lỗi, chụp sau, kiểm chứng hành vi theo DESIGN_SYSTEM.md §7.
- [ ] Commit + push có kiểm chứng `git ls-remote`.

## Triển khai 11 portal (Phase 7)

| Portal | Cổng | Trạng thái | Commit | Ảnh trước/sau |
|---|---|---|---|---|
| SSO | 5208 | Chưa làm | — | — |
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
