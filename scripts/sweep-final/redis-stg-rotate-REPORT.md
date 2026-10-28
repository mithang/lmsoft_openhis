# Xoay mật khẩu Redis STAGING — Báo cáo

- Ngày: 2026-10-28 (ICT). Base: nhánh V2.2, HEAD `e66c7bda`.
- Phạm vi: CHỈ Redis staging (cổng 6380, `~/workspace/redis-stg`) + launcher staging
  (`run-*-stg.sh`) + file env staging. KHÔNG đụng Redis prod 6379, mật khẩu prod,
  launcher prod, tiến trình prod, database demo.
- Quy tắc secret: báo cáo này KHÔNG chứa bất kỳ giá trị mật khẩu nào (cũ hay mới),
  chỉ nêu tên file. Mật khẩu staging mới chỉ nằm trong 2 file nêu ở §2.
- `roles-shots-progress.md` KHÔNG bị worker này sửa (xem §5).

## Kết luận

| Hạng mục | Kết quả |
|---|---|
| Xoay mật khẩu Redis staging | **ĐẠT** — mật khẩu mới đã có hiệu lực trên Redis 6380, mật khẩu cũ bị từ chối |
| Cụm staging tối thiểu chạy lại | **ĐẠT** — Redis 6380 + BaseAPI 17001 + SSO 15208 + PDS 15237 đều LISTEN, log sạch lỗi Redis |
| Đăng nhập staging SSO bằng 000000 | **BLOCKER — chưa kiểm chứng được** (thiếu mật khẩu demo; xem §4, KHÔNG đoán mò vì lockout 3 lần) |
| Prod nguyên vẹn | **ĐẠT** — xem §5 |
| Commit git | **KHÔNG commit** — worker này không sửa file nào trong repo ngoài file báo cáo này (untracked, cố ý để lại chưa commit theo lệnh) |

## 1. Khảo sát ban đầu

- Staging Redis lúc nhận việc đang DỪNG (không có listener 6380, không có tiến
  trình staging nào chạy). Config: `~/workspace/redis-stg/redis-stg.conf`
  (port 6380, bind 127.0.0.1, `daemonize yes`, `dir .../redis-stg/data`).
- Cách staging lấy mật khẩu (mẫu đã đúng, được giữ nguyên): mọi launcher staging
  đọc secret lúc chạy từ file env ngoài repo bằng
  `REDIS_STG_PASS="$(cat ~/workspace/.redis-stg.env)"` rồi truyền
  `RedisConfig__Connection="127.0.0.1:6380,password=$REDIS_STG_PASS"`.
  Không launcher staging nào chứa mật khẩu literal.
- File env staging: `~/workspace/.redis-stg.env` (0600, 1 dòng, không có `KEY=`).
  File conf staging: `~/workspace/redis-stg/redis-stg.conf` (0600).

## 2. Thay đổi đã thực hiện (chỉ 2 file, đều ngoài repo, đều 0600)

1. `~/workspace/redis-stg/redis-stg.conf` — dòng `requirepass` đổi sang mật khẩu mới.
2. `~/workspace/.redis-stg.env` — nội dung đổi sang cùng mật khẩu mới.
3. Mật khẩu mới: 32 ký tự (chữ hoa + chữ thường + chữ số, sinh bằng `secrets`),
   đã kiểm tra khác mật khẩu cũ và khác mật khẩu prod (so sánh trong bộ nhớ,
   không in giá trị). Mật khẩu mới đang lưu ở 2 file trên (chỉ tên file, không giá trị).
4. KHÔNG sửa launcher nào: quét 19 file tiêu thụ staging
   (18 `run-*-stg*.sh` trong `openhis-core` + `~/workspace/run-sso-stg-15209.sh`)
   → **0 lần xuất hiện mật khẩu cũ** (trước và sau xoay). Vì vậy không có
   pathspec launcher nào cần commit.
5. Lưu ý khởi động: `redis-server` staging cần
   `LD_LIBRARY_PATH=~/workspace/redis-libs/usr/lib/x86_64-linux-gnu`
   (thiếu `liblzf.so.1` nếu không đặt — lỗi gặp lần đầu, đã xử lý, không liên
   quan tới mật khẩu).

## 3. Kiểm chứng staging (đã chạy lại, đang để chạy)

| Dịch vụ staging | Cổng | Kết quả |
|---|---|---|
| Redis staging | 6380 | LISTEN 127.0.0.1:6380, pid riêng (không phải pid prod) |
| BaseAPI staging | 17001 | LISTEN, log `Application started`, môi trường Development |
| SSO staging | 15208 | LISTEN, log `Application started`, trang `/Login/Index` HTTP 200 |
| PDS staging (app module) | 15237 | LISTEN, trang chính `/` HTTP 200, title `Hệ thống nhà thuốc - kho thuốc` |

Kiểm chứng Redis (bằng socket RESP + `redis-cli`, mật khẩu đọc từ file env,
không ghi vào log lệnh):

- `PING` không AUTH → bị từ chối (`NOAUTH`): ĐẠT.
- `AUTH` bằng mật khẩu cũ → bị từ chối (`WRONGPASS`): ĐẠT.
- `AUTH` bằng mật khẩu mới → `OK`, sau đó `PING` → `PONG`: ĐẠT.
- `CLIENT LIST` trên 6380: **6 kết nối StackExchange.Redis** (2 kết nối/app × 3 app
  staging, age trùng thời điểm khởi động cụm) — chứng tỏ cả 3 app staging đã
  xác thực thành công vào Redis staging bằng mật khẩu mới. Kết nối với mật khẩu
  cũ sẽ không thể xuất hiện trong danh sách này.
- Log 3 app staging: không có lỗi `NOAUTH`/`WRONGPASS`/kết nối Redis.

Lưu ý `GetAuthCode`: endpoint `/Login/GetAuthCode` của SSO staging trả JSON lỗi
`The type initializer for 'Gdip' threw an exception` — lỗi này xảy ra ở bước
tạo ảnh captcha, TRƯỚC bước ghi Redis, và là giới hạn libgdiplus trên Linux
có sẵn từ trước (prod SSO cũng trả cùng dạng JSON). Nó giải thích vì sao
`KEYS *` trên staging rỗng sau khi gọi GetAuthCode, và KHÔNG phải lỗi xác
thực Redis. Trang đăng nhập của ta không dùng captcha (`isCheckChkCode` false)
nên lỗi này không chặn đăng nhập.

## 4. BLOCKER: đăng nhập staging SSO bằng 000000 chưa kiểm chứng được

- Tài khoản demo staging `000000` hiện **chưa bị khóa** (`Locked=0` trên cả
  `V_C_Sys_User` và `Sys_UserLogOn` của DB `NewtouchHIS_Base_STG`).
- Mật khẩu demo KHÔNG có trong môi trường của worker này và KHÔNG tìm thấy
  trên đĩa:
  - Biến `SSO_PASSWORD` không có trong shell của worker.
  - Quét toàn bộ `~/workspace` (trừ `node_modules/.git/bin/obj`) tìm dòng gán
    `SSO_PASSWORD`: **0 file**.
  - `~/workspace/.appsecrets.env` không chứa `SSO_PASSWORD`.
  - Kiểm tra offline (không chạm lockout): tái tạo đúng chuỗi kiểm tra của
    `UserLogin` (md5 → DES-CBC → md5, dùng đúng DLL `NewtouchHIS.Lib.Base`
    qua chương trình C# đối chứng, đã xác nhận khớp với bản Python trên cả
    dữ liệu mẫu) với 10 ứng viên gồm giá trị `LoginUser_Default_Password`
    trong appsettings và các mật khẩu mặc định thường gặp → **không ứng viên
    nào khớp** với hash đã lưu của tài khoản staging `000000`.
- Vì lockout thật là **3 lần sai liên tiếp → khóa tài khoản** (7-fix), worker
  KHÔNG đoán mò thêm để tránh khóa tài khoản demo staging mà đợt sweep vẫn cần.
- Cần điều phối viên cung cấp mật khẩu demo (kênh đã dùng cho các lần kiểm
  chứng trước) rồi chạy lại đúng 1 bước: POST `/Login/CheckLogin` tới
  `http://127.0.0.1:15208` với `username=000000`, `password=md5(mật khẩu demo)`,
  `code` rỗng, sau đó GET `/Home/Index` với cookie nhận được. Cụm staging
  đang được để chạy sẵn cho bước này (xem §6).

## 5. Prod KHÔNG bị đụng — bằng chứng

- Trang đăng nhập prod SSO `http://127.0.0.1:5208/Login/Index`: HTTP 200.
- Redis prod 6379: vẫn LISTEN, pid 2386 từ 07:02 UTC, **không restart,
  không đổi mật khẩu**.
- Tiến trình prod (BaseAPI 7001, SSO 5208, các app module prod) không bị
  chạm tới; worker chỉ khởi động tiến trình staging (cổng 15208/15237/17001/6380).
- `git status` trong `openhis-core` về launcher: `run-*.sh` có **0 file dirty**.
  Toàn bộ `git status` lúc kết thúc chỉ có:
  - `M roles-shots-progress.md` — đã dirty từ trước khi worker nhận việc,
    thuộc điều phối viên, worker không sửa.
  - `M his-dll-common/Net6/NewtouchHIS.Lib.Base.dll` — xuất hiện lúc 07:17 UTC
    trong cửa sổ làm việc; KHÔNG do lệnh nào của worker này gây ra (worker
    không ghi gì vào cây repo ngoài file báo cáo này; chương trình C# đối
    chứng chỉ tham chiếu đọc DLL đó). Khả năng cao là một worker đồng thời
    khác build/copy. Worker KHÔNG tự revert (đó có thể là thay đổi có chủ ý
    của người khác) — nhờ điều phối viên xác nhận file này.
  - File báo cáo này: untracked mới, cố ý chưa commit (không có thay đổi
    launcher nào để commit).

## 6. Trạng thái để lại + kiểm lại nhanh

- Cụm staging đang CHẠY (không tắt sau kiểm chứng): Redis 6380, BaseAPI 17001,
  SSO 15208, PDS 15237. Log lần khởi động này:
  `~/workspace/baseapi-stg-rotate.log`, `~/workspace/sso-stg-rotate.log`,
  `~/workspace/pds-stg-rotate.log`.
- Mật khẩu staging mới lưu tại (chỉ tên file): `~/workspace/.redis-stg.env`
  và dòng `requirepass` trong `~/workspace/redis-stg/redis-stg.conf`.
- Khi có mật khẩu demo, kiểm lại đăng nhập staging theo §4; nếu thành công,
  phiên đăng nhập sẽ ghi qua Redis staging 6380 (cùng `RedisHelper`/`OperatorProvider`
  mà các app staging đã xác thực kết nối ở §3).
