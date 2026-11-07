# OpenHIS — chạy bằng Docker trên máy local

Chỉ cần chạy một lệnh là test được:

```bash
./setup.sh            # 6 dịch vụ lõi: SQL Server, Redis, static, BaseAPI, Sett-API, SSO
./setup.sh --all      # đủ 23 app (thêm profile "all")
```

Sau đó mở **http://127.0.0.1:5208** (dùng `127.0.0.1`, không dùng `localhost`)
và đăng nhập demo **`000000` / `000000`**.

> ⚠️ **GIỚI HẠN QUAN TRỌNG — đọc trước khi chạy:** bộ setup này **chưa
> từng chạy container thật**. Máy viết script (VM dev) không có Docker,
> nên không thể `docker compose up` để kiểm chứng. Những gì đã kiểm
> chứng trên VM: cú pháp file (bash/YAML/Python), logic sinh câu lệnh
> RESTORE (chạy read-only với sqlcmd live), và publish Release của 3
> project lõi — xem § "Đã kiểm chứng / chưa kiểm chứng" ở cuối. Nếu gặp
> lỗi lúc build/up, xem § "Troubleshooting" và báo lại log để sửa.

## 1. Yêu cầu

| Thứ | Yêu cầu |
|---|---|
| Docker | Docker Desktop (Windows/macOS) hoặc Docker Engine (Linux), kèm Compose v2 (`docker compose`). Không dùng `sudo`/`systemctl` trong script. |
| Hệ điều hành | Linux, macOS, hoặc Windows với **WSL2** / Git Bash. Script là bash, không chạy trên `cmd.exe` / PowerShell thuần. |
| RAM | Tối thiểu **4 GB trống cho lõi**, **8 GB cho full 23 app**. SQL Server 2022 trong container cần ~2 GB riêng. |
| Đĩa | ~10 GB cho images + 701 MB backup + dữ liệu SQL sau restore. |
| Mạng | Lần build đầu cần Internet (tải .NET SDK 6.0 + NuGet packages). |
| Công cụ | `python3` (bước restore), `curl`, `openssl` (sinh secret; thiếu thì dùng `/dev/urandom`). |
| Apple Silicon (M1/M2/M3) | SQL Server 2022 không có image ARM64 — compose đã gán `platform: linux/amd64` nên nó chạy dưới emulation (chậm hơn, vẫn test được). |

## 2. Chuẩn bị database backup

Script **không tự bịa schema và không chạy lại 142 script upgrade** —
backup đã chứa trạng thái cuối (đã upgrade V1.8 → V2.2, đã dịch DB sang
tiếng Việt). Có 2 cách đưa backup vào:

**Cách A — file local (khuyên dùng):** đặt 14 file `.bak` vào `./sqlbackup/`
(tên file kiểu `NewtouchHIS_Base.bak`, hoặc kiểu cũ có hậu tố ngày
`NewtouchHIS_Base_20241108.bak` — cả hai đều restore được).

**Cách B — tải bằng URL:** đặt biến môi trường rồi chạy:

```bash
OPENHIS_BACKUP_URL=https://example.com/sqlbackup-current.zip ./setup.sh
```

URL phải trỏ tới file `.zip` hoặc `.tar.gz` chứa các file `.bak`.

**Link backup tươi hiện tại** (14 DB demo, trạng thái đúng HEAD `1afd0d10`,
701 MB → zip 96 MB):

- https://muse.ai/files/1326356320569011/2278649682678261/s5f37wr39nj6jgkiawclvcrj/sqlbackup-current.zip
- **Hết hạn: 2026-11-09 09:23 ICT** — tải trước hạn. Sau hạn thì xin link mới
  hoặc dùng bản V1.8 trong `sqlbackup/20241108` (cần chạy upgrade sau đó —
  chưa có trong script này).

Không có cả hai nguồn thì `setup.sh` báo lỗi rõ ràng và dừng lại, không
làm gì thêm.

## 3. Cách chạy

```bash
git clone <repo> && cd <repo>        # hoặc vào thư mục repo đã có
./setup.sh                           # lõi: SQL + Redis + static + BaseAPI + Sett-API + SSO
```

Script làm, theo thứ tự:

1. Kiểm tra `docker` + `docker compose` + `python3` + `curl`.
2. Kiểm tra các cổng 1433/6379/41142/5208/7001/7002 (full: thêm 17 cổng
   app) còn trống. **Cổng bận thì báo rõ cổng nào + tiến trình nào và
   dừng — không tự kill gì.**
3. Sinh `.env` nếu chưa có (SA_PASSWORD, REDIS_PASSWORD, APP_SECRET,
   AUTHCENTER_JWT_KEY — random mạnh bằng `openssl rand`, không in ra
   màn hình). Chạy lại thì dùng lại `.env` cũ; xóa `.env` để sinh mới.
4. Lấy backup (local `./sqlbackup/*.bak` hoặc `$OPENHIS_BACKUP_URL`).
5. Build image lõi (`docker compose build`); `--all` thì build hết.
6. Up SQL Server + Redis, chờ tới khi `sqlcmd SELECT 1` và
   `redis-cli ping` trả lời (timeout 180s / 60s).
7. Restore DB bằng `docker/db/restore_dbs.py`: đọc `RESTORE FILELISTONLY`
   rồi `RESTORE ... WITH MOVE ... REPLACE` vào `/var/opt/mssql/data`.
   **Idempotent** — DB nào đã có trong `sys.databases` thì bỏ qua, nên
   chạy lại `setup.sh` an toàn. (Dùng `--skip-restore` để bỏ hẳn bước này.)
8. Up app lõi, chờ SSO 200 + Sett-API 200 (timeout 300s).
9. Smoke test: đăng nhập demo **đúng 1 lần** (`POST /Login/CheckLogin`
   với password là md5 của `000000`). Thất bại thì in log SSO/BaseAPI
   và dừng — **không thử lại** (3 lần sai liên tiếp khóa tài khoản
   000000 trong 60 phút).
10. In bảng URL + lệnh dừng/mở full.

Dừng / mở lại:

```bash
docker compose down        # dừng hết, giữ dữ liệu (volumes sqldata/redisdata)
docker compose down -v     # dừng hết + XÓA dữ liệu (DB + Redis)
docker compose up -d       # mở lại lõi
docker compose --profile all up -d   # mở lại đủ 23 app
docker compose logs -f sso          # xem log một service
```

## 4. Các service và cổng

Cổng host giữ nguyên số như trên VM, nên mọi URL trong tài liệu đều đúng.

| Service | Cổng | Ảnh / build | Ghi chú |
|---|---|---|---|
| `sqlserver` | 1433 | `mcr.microsoft.com/mssql/server:2022-latest` | SA password từ `.env`, volume `sqldata` |
| `redis` | 6379 | `redis:7-alpine` | `requirepass` từ `.env`, AOF bật, volume `redisdata` |
| `static` | 41142 | `docker/static/Dockerfile` (python:3-slim) | file tĩnh + CORS `*` (font mới load được) |
| `baseapi` | 7001 | `docker/Dockerfile.app` (HIS.BaseAPI) | lõi |
| `settapi` | 7002 | `docker/Dockerfile.app` (Newtouch.Sett.API) | lõi |
| `sso` | 5208 | `docker/Dockerfile.app` (HIS.SSO) | lõi, trang đăng nhập |
| `sett` | 5221 | Sett Web | profile `all` |
| `mrqc` / `mrqcapi` | 5220 / 5320 | MRQC Web / API | profile `all` |
| `or` / `orapi` | 5231 / 5331 | OR Web / API | profile `all` |
| `mrms` / `mrmsapi` | 5232 / 5332 | MRMS Web / API | profile `all` |
| `basehosp` / `hospapi` | 5233 / 5333 | Base HOSP Web / API | profile `all` |
| `cis` / `cisapi` | 5234 / 5334 | CIS Web / API | profile `all` |
| `emr` / `emrapi` | 5235 / 5335 | EMR Web / API | profile `all` |
| `herp` / `herpapi` | 5236 / 5336 | Herp Web / API | profile `all` |
| `pds` / `pdsapi` | 5237 / 5337 | PDS Web / API | profile `all` |
| `baseweb` | 5238 | Base Web | profile `all` |
| `authcenter` | 8015 | AuthenticationCenter | profile `all` |
| `apimanage` | 8016 | WebAPI.Manage | profile `all` |

Tất cả 23 app build từ **một Dockerfile chung** (`docker/Dockerfile.app`,
multi-stage `dotnet/sdk:6.0` → `dotnet/aspnet:6.0`), tham số hoá bằng
build args `PROJECT` / `DLL` / `PORT` / `CONFIG` (mặc định Release).
Các Dockerfile có sẵn trong repo (HIS.SSO, AuthenticationCenter,
WebAPI.Manage) là **Windows-based** (`windowsservercore-ltsc2019`) nên
không dùng được trên Linux Docker.

## 5. Host mapping — app tìm nhau bằng gì

Đây là phần dễ sai nhất khi đưa app lên Docker, nên ghi rõ từng key kèm
bằng chứng grep trong repo (đọc trong `docker-compose.yml`, khối
`x-app-env`):

| Key (env override) | Giá trị trong Docker | Loại | Bằng chứng |
|---|---|---|---|
| `BusinessConfig__AppAPIHost__HisAppBaseAPIHost` | `http://baseapi:7001/` | **server-side** | `AuthController.cs:185` (module API): `host + "/api/login/CheckPwd"` — chính server gọi BaseAPI |
| `BusinessConfig__AppAPIHost__SiteBaseAPIHost` | `http://baseapi:7001/` | **server-side** | SSO login gọi BaseAPI CheckPwd qua key này |
| `BusinessConfig__AppAPIHost__SiteAuthCenterHost` | `http://authcenter:8015/` | **server-side** | `TokenService.cs:31`, `InfraCompat.cs:67` — server gọi AuthCenter lấy token |
| `BusinessConfig__AppAPIHost__SiteApiManageHost` | `http://apimanage:8016/` | **server-side** | `AuthenManageHelper.cs:68` — server gọi Manage |
| `BusinessConfig__AppAPIHost__SiteSettAPIHost` | `http://settapi:7002/` | **server-side** | `ApiBaseController.cs:19` — WebAPI.Manage proxy sang Sett.API |
| `BusinessConfig__AppAPIHost__SitePdsAPIHost` | `http://pdsapi:5337/` | **server-side** | `SitePdsAPIHelper.cs:25` — server gọi PDS.API |
| `BusinessConfig__AppAPIHost__SiteCisAPIHost` | `http://cisapi:5334/` | **server-side** | `ApiBaseController.cs:23`, `SiteCisAPIHelper.cs:25` — server gọi CIS.API |
| `BusinessConfig__AppAPIHost__SiteStaticHost` | `http://127.0.0.1:41142/` | **browser-facing — GIỮ NGUYÊN** | `MvcHelper.cs` — render vào `<script>/<link>`; **browser** load, browser ở ngoài network Docker |
| `AccessConfig__Document_Domain` | `127.0.0.1` | **browser-facing — GIỮ NGUYÊN** | cùng lý do trên |

Ba lưu ý từ bằng chứng grep:

1. **`HisAppBaseAPIHost: "HIS.BaseAPI"` là giá trị chết trên VM.**
   Một số `appsettings.json` đặt key này là `"HIS.BaseAPI"` (tên service
   Docker của vendor ngày xưa). `HttpClientHelper.PostAsync` không tự
   thêm scheme, nên URL `HIS.BaseAPI/api/login/CheckPwd` ném exception
   và `AuthController` fail-closed (trả null). Trên VM đường này không
   bao giờ thành công — login web đi qua `SiteBaseAPIHost` thay vào đó.
   Compose override thành `http://baseapi:7001/` nên trong Docker đường
   này **sống lại** (đúng hơn trên VM). Không có `/etc/hosts` nào trỏ
   `HIS.BaseAPI` trên VM — đã kiểm tra.
2. **`SiteCisAPIHost: http://localhost:7003/` là giá trị cũ/sai.**
   Cổng 7003 không tồn tại (CIS.API thật là 5334). Compose override
   thành `http://cisapi:5334/` để sửa. Tương tự `SiteSettAPIHost:
   http://localhost:17361/` (cổng staging cũ) cũng được override về 7002.
3. **`BusinessDB__DefaultConn` giữ nguyên slot `{0}`.**
   Code tự format: `string.Format(DefaultConn, dbName)` theo từng app
   (`ServiceExtensions.cs:540`, `DbContext.cs:18`). Compose chỉ đổi
   `Data Source=localhost` → `sqlserver`, còn `{0}` để nguyên nên mỗi
   app vẫn lấy đúng catalog của nó từ `DBConf` trong appsettings.
   `RedisConfig__Connection` đổi `127.0.0.1:6379` → `redis:6379`.

**Secrets:** `setup.sh` sinh **một `APP_SECRET` dùng chung** cho BaseAPI
(`SSOAuthIdentity[0]`), WebAPI.Manage (`SSOAuthIdentity[0]`) và
AuthCenter (`JWT__SSOAuthApp__0..7`, 8 AppId: API.Manage, sett.web,
base.web, NewtouchHIS.SelfTerminal, MRQC, Oh_Newtouch_Clinic, EMR.web,
HIS.BaseAPI). Trên VM mỗi app có secret riêng
(`BASEAPI_APPSECRET`, `MANAGE_APPSECRET`, `AUTHCENTER_APPSECRET_0..7`);
dùng chung một secret chỉ để local test cho đơn giản — **production phải
dùng secret riêng từng app**. SSO portal không cần AppSecret (không có
`SSOAuthIdentity` trong appsettings của nó — đã grep). `.env` không bao
giờ được commit (đã thêm vào `.gitignore`).

**`UseUrls` hardcode:** 19/23 app hardcode `UseUrls("http://127.0.0.1:PORT")`
trong `Program.cs` (vd CIS Web 5234, CIS API 5334, Sett 5221, MRQC 5220…);
`ASPNETCORE_URLS` không override được `UseUrls`. Dockerfile patch lúc
**build** bằng sed (`127.0.0.1` → `*`) trên **bản copy trong stage build**
— source repo không đổi. Sett.API đã đọc URL từ env (fallback 7002) nên
không cần patch nhưng vẫn chịu cùng sed ở fallback (vô hại). SSO và
BaseAPI không hardcode `UseUrls`, dùng `ASPNETCORE_URLS=http://+:PORT`
từ compose.

## 6. Troubleshooting

**Cổng bận** — `setup.sh` báo cổng + tiến trình giữ cổng rồi dừng. Trên
máy đã chạy OpenHIS kiểu VM (dotnet trực tiếp) thì dừng stack đó trước.
Không tự kill gì.

**SQL Server không lên (timeout 180s)** — thường do thiếu RAM (cần ~2 GB
trống) hoặc SA password không đủ mạnh (nếu tự sửa `.env`: tối thiểu 8 ký
tự, có hoa + thường + số + ký tự đặc biệt, không dùng `$`). Xem log:
`docker compose logs sqlserver`. Xóa `.env` để `setup.sh` sinh lại
secret mới (lưu ý: đổi SA password thì phải `docker compose down -v`
để tạo lại volume `sqldata`, vì password SA chỉ đặt lúc khởi tạo lần đầu).

**Redis không lên** — xem `docker compose logs redis`. Kiểm tra
`REDIS_PASSWORD` trong `.env` khớp với container (đổi `.env` sau khi
container đã tạo thì phải `down -v` để tạo lại).

**Build .NET lỗi (NuGet restore / publish)** — lần build đầu cần mạng.
Nếu một project publish Release lỗi, thử đổi `CONFIG: Release` →
`CONFIG: Debug` cho service đó trong `docker-compose.yml` rồi build lại
(trên VM, MRMS Web, OR Web/API, CIS Web/API vốn chạy bản Debug). Báo
lại log build để sửa gốc.

**Đăng nhập demo thất bại** — `setup.sh` chỉ thử **1 lần** rồi dừng kèm
log SSO/BaseAPI. **Không thử lại bằng tay**: 3 lần sai liên tiếp khóa
tài khoản 000000 trong 60 phút. Kiểm tra: DB đã restore đủ 14 DB chưa
(`docker compose exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "SELECT name FROM sys.databases"`),
BaseAPI có lên không (`curl http://127.0.0.1:7001/` trả 404 là bình
thường — 404 nghĩa là server đang chạy), Redis có `PONG` không.

**Ảnh load thiếu / icon vuông (tofu)** — static host (41142) phải trả
`Access-Control-Allow-Origin: *` (xem `docker/static/Dockerfile`).
Kiểm tra: `curl -I http://127.0.0.1:41142/js/framework-ui.js` phải có
header CORS và HTTP 200.

**Mở bằng `localhost` thay vì `127.0.0.1`** — login/session có thể lỗi
do `Document_Domain` + cookie domain là `127.0.0.1`. Luôn mở
`http://127.0.0.1:5208`.

**Apple Silicon build chậm / SQL Server lỗi** — SQL Server chạy emulation
`linux/amd64` nên khởi động chậm (vài phút). Chờ hết timeout 180s rồi
xem log trước khi kết luận lỗi.

## 7. Đã kiểm chứng / chưa kiểm chứng (trung thực)

Đã kiểm chứng trên VM dev (2026-11-07, worktree HEAD `e8089cfd`):

- `bash -n setup.sh` OK (đã sửa 1 bug lúc validate: biến `CURL` chứa
  `--noproxy *` không quote sẽ bị glob-expand thành tên file — đổi
  thành hàm `curl_loopback()`); `shellcheck` không có trên VM nên chưa
  chạy. `python3 -m py_compile docker/db/restore_dbs.py` OK,
  `python3 -m py_compile docker/static/static-cors-server.py` OK,
  `yaml.safe_load docker-compose.yml` OK + kiểm tra cấu trúc: 26 service,
  23 app (6 lõi + 20 profile `all`), cổng không trùng, đủ 23 `.csproj`
  tồn tại, tên DLL khớp tên project, env override có đủ trên cả 23 app,
  `depends_on` merge-key resolve đúng.
- Logic sinh câu `RESTORE ... WITH MOVE` chạy read-only với sqlcmd live
  trên VM: `RESTORE FILELISTONLY` trên backup tươi
  `NewtouchHIS_Base.bak` trả 2 file (D + L), câu RESTORE sinh ra đúng
  dạng; `db_name_from_file` đúng cả 17 tên file (kiểu tươi
  `NewtouchHIS_Base.bak` và kiểu cũ `NewtouchHIS_Base_20241108.bak`).
  Lệnh sed patch `UseUrls` (`127.0.0.1` → `*`) đã thử trên bản copy tạm
  của `Program.cs` CIS Web — bản copy ra `http://*:5234`, source repo
  không đổi.
- **Publish Release 3 project lõi trên VM, mỗi lần 1 project, đều PASS
  (exit=0):** HIS.SSO.dll 577,024 B (28s, chỉ warning MVC1000 có sẵn),
  HIS.BaseAPI.dll 44,032 B (7s, chỉ warning CS1591), Newtouch.Sett.API.dll
  147,456 B (4s, chỉ warning CS0649). RAM trong lúc build: available
  598–706 MB, không chạm cổng lõi đang chạy.
- Mapping host server-side vs browser-facing: grep từng key trong `.cs`
  / `.cshtml` (bảng §5). Giá trị chết `HIS.BaseAPI` / `HIS.AuthCenter`
  và giá trị cũ `localhost:7003` / `localhost:17361` đã xác minh bằng code.
- Đường dẫn `.csproj` + tên DLL + cổng của cả 23 app: đối chiếu
  `run-*.sh`, `recovery.sh` và thư mục `bin/Release` trên VM.
- Không có secret thật nào trong file commit (grep tên biến / giá trị
  secret — chỉ còn lời gọi `rand_pw`/`rand_hex` trong `setup.sh` và
  placeholder trong `.env.example`; `.env` đã thêm vào `.gitignore`).

**Chưa kiểm chứng — vì VM không có Docker:**

- Chưa từng `docker compose build` / `up` thật: Dockerfile, compose
  healthcheck, thứ tự `depends_on`, restore trong container, login
  smoke test đều là **lý thuyết dựa trên code đã đọc**, chưa chạy.
- Chưa biết `dotnet publish` Release có thành công cho 20 app còn lại
  không (chỉ thử 3 project lõi — cả 3 đều PASS, xem trên).
- Chưa biết SQL Server 2022 container + 14 DB restore mất bao lâu /
  tốn bao nhiêu RAM trên máy user.

## 8. Giới hạn khác (kế thừa từ chương trình OpenHIS)

- Các tính năng Windows-only vẫn là stub + thông báo tiếng Việt, không
  chạy được trên Linux container: CIS ActiveX + `Lis_HisInterface.dll`,
  EMR/MRQC DCSoft.Writer ActiveX, Herp SSRS (13 `.rdl`), WCF SOAP
  clients, SignalR 2+OWIN (CIS), Office Interop Excel (Sett).
- BHYT XML / QĐ 697, cổng thanh toán, GuiAn, WCF/YY-external: chưa
  chứng minh end-to-end (cần tài khoản/chứng thư thật).
- Dữ liệu trong backup là dữ liệu **demo** (không phải dữ liệu bệnh viện
  thật). Không dùng bộ này cho bệnh viện thật.
