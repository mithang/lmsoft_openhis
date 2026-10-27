# Giai đoạn 4 — EMR Web + EMR API chuyển Debug → Release

- Ngày: 2026-10-27 (buổi trưa, giờ ICT).
- Repo: `~/workspace/openhis-core`, nhánh `V2.2`.
- Spec: `~/workspace/production-readiness-design.md` §6.6 (EMR Debug → Release là mục
  bắt buộc trước production; chỉ yêu cầu build Release + đổi launcher + kiểm chứng
  lại login/trang, không yêu cầu đổi `ASPNETCORE_ENVIRONMENT`).
- Nguyên tắc tuân thủ: KHÔNG commit, KHÔNG push, KHÔNG `git add -A`, KHÔNG ghi DB
  demo (chỉ login + đọc trang/API), KHÔNG sửa `recovery.sh`, checkout
  `~/workspace/lmsoft_openhis`, run copy `~/workspace/lmsoft_openhis_run`, hay mật
  khẩu SA trong `HIS.BaseAPI/appsettings.json`. Không in mật khẩu hay token đầy đủ
  (token chỉ ghi 8 ký tự đầu; mật khẩu/md5 không ghi).

## 0. Trạng thái cây lúc bắt đầu

- `git status --short`: sạch (không file đổi).
- `git log -1`: `fc6e963d Giai doan 3: Sett-API host 7002 (BookingESB/PatientAnon/OrderCenter) + override 8016 sang 7002 + recovery section 27`.
- Local = remote tại thời điểm bắt đầu (theo brief; không push/pull trong giai đoạn).
- Quét cổng lúc bắt đầu: 26 cổng UP (danh sách đầy đủ ở §5).

## 1. Launcher cũ/mới và PID

### 1.1. Xác nhận trước khi làm: cả hai đều chạy `bin/Debug`

| App | Launcher (repo root) | DLL cũ đang chạy | PID cũ | Cổng |
|---|---|---|---|---|
| EMR Web | `run-emr.sh` | `bin/Debug/net6.0/Newtouch.EMR.Web.dll` | 2792 | 5235 |
| EMR API | `run-emr-api.sh` | `bin/Debug/net6.0/Newtouch.EMR.API.dll` | 8174 | 5335 |

Xác nhận bằng `ps`/`ss -ltnp`: cmdline của PID 2792 và 8174 đúng đường dẫn
`bin/Debug` nói trên; cổng 5235/5335 LISTEN trên `127.0.0.1` thuộc đúng 2 PID này.

### 1.2. Sau khi chuyển: cả hai chạy `bin/Release`

| App | DLL mới đang chạy | PID mới | Cổng | Cách dừng cũ |
|---|---|---|---|---|
| EMR API | `bin/Release/net6.0/Newtouch.EMR.API.dll` | 16804 | 5335 | `kill 8174` theo PID chính xác, cổng DOWN sau ~2s |
| EMR Web | `bin/Release/net6.0/Newtouch.EMR.Web.dll` | 17162 | 5235 | `kill 2792` theo PID chính xác, cổng DOWN sau ~2s |

- Không dùng `pkill -f` (pattern `Newtouch.EMR` trùng cả dòng lệnh shell của chính
  worker — đã thấy khi `pgrep -f` trả về cả shell đang chạy lệnh).
- Thứ tự thực hiện: API trước, Web sau; mỗi app làm trọn vòng
  dừng → build → sửa launcher → restart → kiểm chứng rồi mới sang app sau.
- Luôn dừng app trước khi build lại project đó (tránh lỗi khóa file PDB
  MSB3027/MSB3021 như bài học giai đoạn 1B). Cả hai build đều chạy khi app đã dừng.

### 1.3. Nội dung diff launcher (toàn bộ diff của giai đoạn)

Mỗi launcher chỉ đổi đúng 1 dòng — dòng đường dẫn DLL. Mọi thứ khác giữ nguyên:
proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, secret qua env
(`source ~/workspace/.sqlserver.env` + override connection string, không ghi secret
vào file), `ASPNETCORE_ENVIRONMENT=Development` (xem ghi chú §6).

```diff
# run-emr-api.sh (dòng 10)
-  ~/.dotnet/dotnet bin/Debug/net6.0/Newtouch.EMR.API.dll
+  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.EMR.API.dll

# run-emr.sh (dòng 10)
-  ~/.dotnet/dotnet bin/Debug/net6.0/Newtouch.EMR.Web.dll
+  ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.EMR.Web.dll
```

- `bash -n` cả hai launcher sau khi sửa: SYNTAX OK.
- Lưu ý về URL: hai launcher này từ đầu đã KHÔNG mang `ASPNETCORE_URLS`
  (khác một số launcher khác). Cổng không đổi vì URL nằm trong code:
  `Newtouch.EMR.Web/Program.cs` có `UseUrls("http://127.0.0.1:5235")` và
  `Newtouch.EMR.API/Program.cs` có `UseUrls("http://127.0.0.1:5335")`.
  Vì vậy không cần và không nên thêm `ASPNETCORE_URLS` vào launcher ở giai đoạn
  này — thêm vào cũng bị `UseUrls` trong code override, chỉ gây nhiễu.
  Cổng sau restart vẫn đúng 5235/5335 (đã kiểm chứng bằng `ss`).

### 1.4. `recovery.sh` section 14/15 — KHÔNG cần sửa

- Section 14 (EMR Web, dòng 125–131) và section 15 (EMR API, dòng 133–139) đều
  gọi launcher (`~/workspace/openhis-core/run-emr.sh` /
  `~/workspace/openhis-core/run-emr-api.sh`), KHÔNG hardcode đường dẫn
  `bin/Debug`. Vì launcher đã đổi sang Release nên lần watchdog restart tới sẽ
  tự chạy bản Release. Worker KHÔNG sửa `recovery.sh` (đúng lệnh).
- Không có dòng nào khác trong `recovery.sh` hardcode `bin/Debug` cho EMR
  (grep `emr|EMR|5235|5335` chỉ ra 2 section trên + dòng thứ tự + dòng tổng kết).

## 2. Kết quả build Release

Tên project đã đối chiếu trong cây trước khi build (đúng brief):
`Newtouch.HIS.EMR/Newtouch.EMR.Web/Newtouch.EMR.Web.csproj` và
`Newtouch.HIS.EMR/Newtouch.EMR.API/Newtouch.EMR.API.csproj`.

### 2.1. EMR API — `dotnet build -c Release`: 0 lỗi

- `0 Error(s)`, 9 warning, toàn bộ là MSB3277 (conflict version
  `Microsoft.IdentityModel.Tokens` 6.8.0 vs 6.10.0, chọn 6.8.0) — cảnh báo
  có sẵn từ trước, không phải lỗi chỉ có ở Release, không sửa, không tắt
  warning thành lỗi. Không có lỗi `#if DEBUG`, điều kiện biên dịch hay file
  chỉ có ở Debug.
- Trước giai đoạn này chưa từng có bản Release của EMR API trên đĩa
  (`bin/Release/.../Newtouch.EMR.API.dll` không tồn tại; chỉ có bản Debug từ
  giai đoạn 2). Build lần này tạo mới: DLL Release timestamp 2026-10-27 11:57.
- Không sửa code nào cho API (GetToken thật của giai đoạn 2 giữ nguyên).

### 2.2. EMR Web — `dotnet build -c Release`: 0 lỗi

- `0 Error(s)`, 48 warning, gồm các warning có sẵn (ví dụ CS0114
  `ErrorController.NotFound()` hides `ControllerBase.NotFound()`). Không có lỗi
  chỉ có ở Release, không sửa code, không đổi hành vi nghiệp vụ.
- Bản Release Web đã từng được build ở giai đoạn 1B (DLL timestamp cũ
  2026-10-27 10:22); build lần này rebuild trên cây hiện tại, DLL Release mới
  timestamp 2026-10-27 11:58 (cùng kích thước 3309568 byte).
- Không sửa code nào cho Web.

## 3. Kiểm chứng live sau restart (chỉ đọc, không ghi DB demo)

### 3.1. EMR API 5335 — GetToken thật (hồi quy giai đoạn 2): ĐẠT

- `POST /api/Auth/GetToken`, body `AppId="EMR"`, `Timestamp` ISO 8601,
  `Account=000000`, `Password` = md5 tính bằng `md5sum` (không gõ tay),
  `TokenType="EMR"` → HTTP 200, `code=10000`, cấp token (prefix `7f27adae`).
- Dùng token qua header `token` gọi `POST /api/Default/Query` (Sys_Config)
  → `code=10000`, `data` 6 dòng thật (khớp đúng con số giai đoạn 2 đã báo).
- Sai mật khẩu → `code=40004`, msg "Tên đăng nhập hoặc mật khẩu không đúng,
  hoặc tài khoản đã bị khóa", `data` null (không cấp token).
- Smoke lại lần cuối sau khi toàn bộ stack đã ổn định: GetToken đúng
  credential → `code=10000`, token prefix `210d6030`.
- Kết luận: GetToken thật của giai đoạn 2 còn nguyên sau khi chuyển Release.
  Không tuyên bố gì thêm ngoài 3 ô kiểm chứng trên (ma trận đầy đủ của giai
  đoạn 2 không làm lại ở đây — ngoài phạm vi giai đoạn 4).

### 3.2. EMR Web 5235 — login + trang: ĐẠT

- `GET /Login/Index` → 200 (trước khi login).
- `POST /Login/CheckLogin` form `username=000000`, `password` = md5 tính bằng
  `md5sum`, `code` rỗng → HTTP 200,
  `{"state":"success","message":"Đăng nhập thành công",...}`, cookie
  `EMR_newtouch_loginuserkey_2016` được set (chỉ ghi tên cookie, không ghi giá trị).
- `GET /MedicalRecordManage/MedicalRecord/Index` kèm cookie login → HTTP 200,
  44889 byte, chứa nội dung tiếng Việt ("Bệnh án" xuất hiện 6 lần).
- DCSoft: trang vẫn chứa các chuỗi template demo DCSoft
  ("DCSoft demo center/08Mã/New dEMO/DCSoft.Activ...") và `<title>` rỗng.
  Đây là TRẠNG THÁI ĐÃ BIẾT TỪ TRƯỚC (giai đoạn dịch đã ghi: EMR
  MedicalRecord/Index vẫn còn tiêu đề template demo DCSoft), KHÔNG phải hồi
  quy do chuyển Release, và KHÔNG được báo là lỗi mới. Không tuyên bố DCSoft
  editor chạy được — khối Windows-only này thuộc giai đoạn 10, giai đoạn này
  chỉ chuyển cấu hình build.

### 3.3. SSO không bị phá

- `POST http://127.0.0.1:5208/Login/CheckLogin` với 000000 (md5 bằng `md5sum`)
  → HTTP 200, `{"state":"success","message":"Đăng nhập thành công",...}`.

## 4. Quét cổng sau khi cả hai app đã chuyển xong: 26/26 UP

Kiểm tra từng cổng bằng `ss -ltn` (không chỉ tin log khởi động):

```text
UP: 1433 5208 6379 41142 5220 5221 5231 5232 5233 5234 5235 5236 5237 5238
    5320 5331 5332 5333 5334 5335 5336 5337 7001 7002 8015 8016  (26 cổng)
DOWN: (không có)
```

(1431/1434 là cổng phụ của SQL Server, không nằm trong 26 cổng của stack.)

## 5. RAM và độ ổn định

- Lúc bắt đầu: tổng 7935 MB, free 199 MB, available 876 MB.
- Lúc kết thúc: free 181 MB, available 354 MB. Cả hai app Release đều đang đứng
  vững (cổng LISTEN, login/API trả kết quả) — không app nào crash sau restart.
- Không dừng app khác để lấy RAM. Build chạy tuần tự từng project một.
- Ghi chú cho giai đoạn sau: RAM vẫn rất eo hẹp (available ~350 MB sau khi đủ
  26 cổng chạy). Staging song song toàn stack trên VM này vẫn không khả thi
  (theo kết luận thiết kế §staging) — không phải vấn đề mới của giai đoạn 4.

## 6. Ghi chú / blocker cho giai đoạn sau

1. **Không có blocker.** Cả hai app đã chạy Release, mọi kiểm chứng của giai đoạn
   đều đạt, diff chỉ có 2 file launcher, chưa commit/push (để điều phối viên soát).
2. **Không phải blocker, để giai đoạn đóng gói quyết:** launcher vẫn giữ
   `ASPNETCORE_ENVIRONMENT=Development` (đúng phạm vi "chỉ đổi dòng DLL" của
   giai đoạn này và đúng §6.6). Khi đóng gói production, cân nhắc chuyển sang
   `Production` — nhưng đó là việc của giai đoạn đóng gói, không tự đổi ở đây
   vì đổi environment có thể thay đổi hành vi (trang lỗi, log) và cần kiểm
   chứng riêng.
3. **DCSoft không đổi:** editor bệnh án vẫn là khối Windows-only, trang
   MedicalRecord/Index vẫn mang template demo DCSoft như trước. Thuộc giai
   đoạn 10, không thuộc giai đoạn 4.
4. **Kỹ thuật vận hành (đã xử lý, ghi lại cho worker sau):** lệnh exec khởi động
   server dạng `nohup ./run-*.sh &` bị runtime giữ lại dưới dạng session
   "running" dù shell đã thoát (kể cả khi dùng `setsid`). Hai session đó đã
   được dọn bằng `process.clear` SAU KHI xác nhận server vẫn UP bằng `ss`/`ps`
   độc lập — `clear` chỉ xóa bản ghi session, không động vào server. Tuyệt đối
   KHÔNG dùng `process.kill` lên các session kiểu này vì nó sẽ giết cả cây
   tiến trình gồm server. Cách dừng server đúng vẫn là `kill <PID chính xác>`.
5. Log runtime: `~/workspace/openhis-core/emr.log` (Web) và
   `~/workspace/openhis-core/emr-api.log` (API); cả hai đều ghi
   "Application started", `Hosting environment: Development`,
   content root đúng thư mục project. (Lưu ý: lệnh `tail emr.log` chạy ở `~`
   sẽ báo không thấy file — log nằm ở repo root, không phải `~`.)
6. Build outputs `bin/Release` không hiện trong `git status` (đã ignore như các
   build khác) — diff chờ soát chỉ gồm 2 launcher.

## 7. Trạng thái cây lúc kết thúc (chưa commit, chưa push)

- `git status --short`:
  - `M run-emr-api.sh`
  - `M run-emr.sh`
  - (cộng file báo cáo này, untracked, tại `scripts/emr-release/REPORT.md`)
- `git log -1` vẫn `fc6e963d` (không commit mới).
- Không file nào khác bị đổi. Không động vào `recovery.sh`, checkout
  `lmsoft_openhis`, run copy, hay `appsettings.json` của BaseAPI.
