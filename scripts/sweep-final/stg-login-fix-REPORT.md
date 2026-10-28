# SỬA LOGIN STAGING — BÁO CÁO (worker stg-login-fix)

Ngày: 2026-10-28. Base: nhánh V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core`
(git status cuối: chỉ `M roles-shots-progress.md` đã dirty từ trước thuộc điều phối viên
+ `?? scripts/sweep-final/` untracked; worker này KHÔNG commit gì, KHÔNG sửa file nào
ngoài báo cáo này).

## Kết luận nhanh

- **Lỗi HTTP 500 ở BaseAPI staging đã HẾT.** Nguyên nhân chẩn đoán (tiến trình staging
  nạp hỗn hợp DLL cũ/mới trong lúc redeploy prod copy DLL vào bin dùng chung) được
  xác nhận gián tiếp: restart sạch tiến trình staging → endpoint CheckPwd trả BusResult
  JSON đúng dạng, không còn 500.
- **Login staging live CHƯA kiểm chứng xong — bị chặn ở 1 bước duy nhất:** mật khẩu demo
  không có trong phiên làm việc này (xem §4). Worker KHÔNG đoán mò, KHÔNG thử live lần
  nào (0 lần thử login 000000), để tránh khóa tài khoản staging (3 sai liên tiếp → khóa).
  Điều phối viên chỉ cần cung cấp mật khẩu qua kênh đã dùng trước đây, rồi chạy đúng
  1 lệnh ở §5 là xong toàn bộ kiểm chứng.

## 1. Tiến trình BaseAPI staging — kill đúng PID, restart đúng launcher

Xác định bằng listener cổng (không đoán PID):

| | PID | Cổng | Xác minh |
|---|---|---|---|
| BaseAPI staging CŨ (đã kill) | **5420** | 17001 | `ss` listener 17001 → pid 5420; cmdline `dotnet bin/Release/net6.0/HIS.BaseAPI.dll`; cwd `openhis-core/Newtouch.HIS.SSO/HIS.BaseAPI` |
| BaseAPI staging MỚI | **15984** | 17001 | Cùng cmdline/cwd; log `Now listening on: http://127.0.0.1:17001`, `Application started`, hosting `Development` |
| BaseAPI prod (KHÔNG đụng) | **13893** | 7001 | Còn sống suốt quá trình, kiểm chứng cuối vẫn LISTEN 7001 |

- Lệnh kill: `kill 5420` (kill đúng PID, không kill theo pattern). Sau kill: cổng 17001
  hết LISTEN, prod 7001/13893 nguyên vẹn.
- Restart: `nohup bash run-baseapi-stg.sh >> ~/workspace/baseapi-stg.log 2>&1 &`
  (đúng launcher staging — KHÔNG dùng launcher prod). Launcher này: `ASPNETCORE_URLS=
  http://127.0.0.1:17001`, mọi DBConf override sang `*_STG`, Redis `127.0.0.1:6380`,
  proxy env unset. Log khởi động: `~/workspace/baseapi-stg.log`, không có exception
  nào trong đoạn log sau restart.
- Lưu ý: tiến trình mới là con của nohup nên tồn tại sau khi lệnh exec kết thúc
  (đã kiểm chứng lại: PID 15984 còn sống, 17001 còn LISTEN sau nhiều lần kiểm tra).

## 2. Probe CheckPwd — ĐẠT (hết HTTP 500)

Endpoint: `POST http://127.0.0.1:17001/api/login/CheckPwd`, tài khoản KHÔNG tồn tại
`zzz-no-such-user` (không tốn lượt lockout vì user không tồn tại):

1. Body không có AppId → `HTTP 200`, BusResult JSON
   `code 40004, msg "Id ứng dụng (AppId) bắt buộc phải truyền"` — đây là lỗi validation
   envelope, đã chứng tỏ filter/DI khởi tạo bình thường (không còn 500).
2. Body có AppId top-level `"His.SSO"` (AppId trong `Data` không được nhận — AppId
   nằm ở envelope `RequestBase`, xem `Lib.Base/Model/Interface/RequestBase.cs`)
   → `HTTP 200`, `busData: {code: 40004, msg: "Không tìm thấy thông tin người dùng",
   data: null}`, `timeOut: 824` (ms — tra cứu DB thật trên staging, không phải lỗi
   khởi tạo). **Đúng dạng BusResult "không tìm thấy người dùng" như yêu cầu.**

So sánh: trước restart, cùng endpoint trả HTTP 500 do lỗi khởi tạo filter (không tìm
được constructor phù hợp cho 1 type đã obfuscate), xảy ra trước khi kiểm tra mật khẩu.
Sau restart: không còn 500 ở cả 2 dạng probe. Kết luận: giả thuyết hỗn hợp DLL cũ/mới
được khắc phục bằng restart sạch; không cần so sánh DLL thêm ở bước này.

## 3. Login staging live — CHƯA THỰC HIỆN (blocker: thiếu mật khẩu demo)

**Số lần thử login 000000 vào staging trong phiên này: 0.** Lý do, đã kiểm tra kỹ:

- Biến `SSO_PASSWORD` KHÔNG có trong shell của worker này (kiểm tra tên biến, không in
  giá trị). Quét workspace không thấy file nào gán `SSO_PASSWORD`
  (kết quả này khớp báo cáo `redis-stg-rotate-REPORT.md` cùng ngày).
- Worker trước (rotate Redis staging) đã thử đối chứng offline (không chạm lockout):
  tái tạo chuỗi kiểm tra của `UserLogin` (md5 → DES-CBC → md5, dùng đúng DLL
  `NewtouchHIS.Lib.Base`, đã xác nhận khớp trên dữ liệu mẫu) với 10 ứng viên gồm giá
  trị `LoginUser_Default_Password` trong appsettings và các mật khẩu mặc định thường
  gặp → **không ứng viên nào khớp** hash đã lưu của staging 000000.
- Vì lockout thật là 3 lần sai liên tiếp → khóa tài khoản, và nhiệm vụ cấm đoán mò
  mật khẩu, worker DỪNG ở đây thay vì thử live. Tài khoản staging 000000 vì vậy vẫn
  nguyên trạng (không bị khóa bởi worker này, không tốn lượt sai nào).

Điều này KHÔNG mâu thuẫn với chẩn đoán "hash + khóa bí mật của staging khớp hoàn toàn
với demo": chẩn đoán đó nói mật khẩu staging = mật khẩu demo, nhưng giá trị mật khẩu
demo đó không được chuyển cho worker này qua task, env hay file. Cần điều phối viên
cung cấp qua kênh đã dùng cho các lần kiểm chứng trước (hoặc tự chạy 1 lệnh ở §5).

## 4. Trạng thái cụm staging sau sửa (đã kiểm chứng, trừ login live)

| Thành phần | Cổng | PID | Trạng thái |
|---|---|---|---|
| BaseAPI staging (MỚI) | 17001 | 15984 | LISTEN; CheckPwd probe §2 ĐẠT |
| SSO staging | 15208 | 5454 (không đụng) | `GET /Login/Index` = 200; launcher xác nhận gọi BaseAPI 17001 + Redis 6380 + DB `*_STG` |
| PDS staging | 15237 | 5491 (không đụng) | `GET /Login/Index` = 200 |
| Redis staging | 6380 | 5401 (không đụng) | AUTH + PING = PONG (kiểm tra qua socket RESP, do `redis-cli` thiếu `liblzf.so.1` trong VM này) |

Trạng thái prod (đối chứng không bị đụng):

- BaseAPI prod 7001 (PID 13893) LISTEN suốt; probe CheckPwd user không tồn tại → HTTP 200.
- SSO prod 5208 `GET /Login/Index` = 200. Redis prod 6379 AUTH + PONG.
- Không ghi gì vào database demo; không có lệnh SQL ghi nào trong phiên này.
  Không đụng: app prod, launcher prod, Redis prod 6379, mật khẩu SA, static host 41142,
  `recovery.sh` (chỉ đọc qua các báo cáo trước, không sửa), `roles-shots-progress.md`.

## 5. Bước duy nhất còn lại cho điều phối viên (1 lần thử, không thử lại nếu sai)

Khi đã có mật khẩu demo (tính md5 bằng `md5sum` vào biến, không in hash, không gõ
tay hash), chạy đúng 1 lần:

```bash
# SSO staging 15208 — form login, code rỗng, cookie lưu vào jar
curl -s -c /tmp/stg-jar -X POST --data-urlencode "username=000000" \
  --data-urlencode "password=<md5-cua-mat-khau-demo>" --data-urlencode "code=" \
  http://127.0.0.1:15208/Login/CheckLogin
# Kỳ vọng: {"state":"success","message":"Đăng nhập thành công"}
# Sau đó: GET http://127.0.0.1:15208/ và /Home/Index kèm cookie jar → 200,
# và GET trang chính PDS staging 15237 kèm cookie → phục vụ bình thường.
```

- Nếu trả về sai mật khẩu → DỪNG NGAY, không thử lại (tránh khóa tài khoản), báo lại
  cho điều phối viên kèm đúng thông điệp trả về.
- Nếu thành công → cụm staging coi như phục hồi hoàn toàn sau sự cố DLL hỗn hợp.

## 6. Không có gì để commit

Dự kiến trong nhiệm vụ là không có thay đổi code/launcher nào — đúng thực tế: việc sửa
chỉ là restart tiến trình. File báo cáo này nằm trong `scripts/sweep-final/` (thư mục
untracked đã có từ trước, cùng chỗ với 2 báo cáo sweep-final khác), cố ý chưa commit.
