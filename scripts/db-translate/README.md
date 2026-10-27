# DB translation scripts (Scope A) — đợt dịch tiếng Trung còn lại, 2026-10-27

- Mỗi file `upd_<DB>.<bảng>.<cột>.sql` chứa các UPDATE tái tạo trạng thái đã áp dụng live.
- Script dùng giá trị ĐÃ ÁP DỤNG (live), không phải bản dịch từ map hiện tại — chạy lại trên DB
  đã áp dụng sẽ không đổi gì (keyed UPDATE ghi đè cùng giá trị; keyless UPDATE khớp N'old'
  sẽ không khớp vì old đã bị thay).
- Target keyed: UPDATE theo khóa chính (Id/AuthId/ztxmId/...). Target keyless (vd Sys_Report,
  xt_ypjx, xt_yzpc, xt_kssType): UPDATE khớp theo giá trị cũ N'...' — chỉ chạy khi giá trị
  cũ còn tồn tại.
- Sys_Auth_App keyed theo AuthId (không có cột Id).
- `lis_sys_fydm` (Sett/Interface) là VIEW trên CIS jyjc_zt.ztmc — dịch jyjc_zt thì view tự theo,
  không có script riêng.
- Chạy bằng sqlcmd UTF-8 (`-f 65001`); KHÔNG chạy trên DB đang có dữ liệu thật của bệnh viện
  mà chưa backup — script đổi tên hiển thị (menu, thuốc, khoa...) và với enum tiền tố chứng từ
  (xem báo cáo) sẽ đổi tiền tố số chứng từ MỚI phát sinh.
- Danh sách đầy đủ (số dòng theo DB/bảng, chuỗi rút gọn, ngoại lệ) nằm trong báo cáo cuối
  của đợt dịch và file `roles-shots-progress.md` (mục mới nhất).
