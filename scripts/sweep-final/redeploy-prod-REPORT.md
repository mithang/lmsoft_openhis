# REDEPLOY PROD — báo cáo worker (sweep cuối OpenHIS)

- **Thời gian:** 2026-10-28 ~14:10–14:45 ICT (giờ hiển thị trong log build là UTC+0 của VM; các mốc build 07:11–07:17 UTC).
- **Base:** nhánh V2.2, HEAD `e66c7bda`, tại `~/workspace/openhis-core`. Tất cả code fix phase 2 đã commit trong diff `dbb5cc25..e66c7bda` (473 files).
- **Kết quả tổng:** **23/23 tiến trình prod đã redeploy thành công, 0 BLOCKER, 0 lần rollback.** Kiểm chứng cuối: 11/11 web app login `000000` thành công + trang chính 200; 11/11 API/center host trả envelope fail-closed (không 500); 26/26 port prod lắng nghe.
- **Không commit:** không có file launcher nào thay đổi nên không commit gì. `git status` cuối chỉ còn `M roles-shots-progress.md` (của điều phối viên, không đụng) và thư mục này (untracked, chứa báo cáo của các worker).

## 1. Danh sách app cần redeploy từ diff `dbb5cc25..e66c7bda` (đường dẫn → app)

| Nhóm file trong diff | App tương ứng | Redeploy? |
|---|---|---|
| `Newtouch.HIS.SSO/HIS.SSO` (20) + `NewtouchHIS.Framework.Web` (2) | SSO 5208 | ✅ |
| `Newtouch.MRQC.Web` (3) | MRQC Web 5220 | ✅ |
| `Newtouch.MRQC.API` (2) | MRQC API 5320 | ✅ |
| `Newtouch.Sett.Web` (336) | Sett Web 5221 | ✅ |
| `Newtouch.HIS.Sett/Newtouch.Sett.API` (2) | Sett-API 7002 | ✅ |
| `Newtouch.HIS.MRMS/Newtouch.MRMS.Web` (6) | MRMS Web 5232 | ✅ |
| `Newtouch.HIS.MRMS/Newtouch.MRMS.API` (2) | MRMS API 5332 | ✅ |
| `Newtouch.HIS.OR/Newtouch.OR.Web` (14) | OR Web 5231 | ✅ |
| `Newtouch.HIS.OR/Newtouch.OR.API` (3) | OR API 5331 | ✅ |
| `Newtouch.HIS.CIS/Newtouch.CIS.Web` (12) + `Newtouch.CIS.Infrastructure` (6) | CIS Web 5234 | ✅ |
| `Newtouch.HIS.CIS/Newtouch.CIS.API` (5) | CIS API 5334 | ✅ |
| `Newtouch.HIS.EMR/Newtouch.EMR.Web` (6) + `Newtouch.EMR.Infrastructure` (3) | EMR Web 5235 | ✅ |
| `Newtouch.HIS.EMR/Newtouch.EMR.API` (3) | EMR API 5335 | ✅ |
| `Newtouch.HIS.Herp/Newtouch.Herp.Web` (2) + `Newtouch.Herp.Infrastructure` (1) | Herp Web 5236 | ✅ |
| `Newtouch.HIS.Herp/Newtouch.Herp.API` (2) | Herp API 5336 | ✅ |
| `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web` (5) | Base HOSP Web 5233 | ✅ |
| `Newtouch.HIS.Base/Newtouch.HIS.Base.Web` (4) | Base Web 5238 | ✅ |
| `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.API` (5) | Base HOSP API 5333 | ✅ |
| `Newtouch.HIS.PDS/Newtouch.PDS.Web` (5) + `Newtouch.PDS.Infrastructure` (4) | PDS Web 5237 | ✅ |
| `Newtouch.HIS.PDS/Newtouch.PDS.API` (9) | PDS API 5337 | ✅ |
| `Newtouch.HIS.Center/NewtouchHIS.Base.Domain` (2) + `DomainService` (1) + `WebAPI.Manage` (1) | AuthCenter 8015 + WebAPI.Manage 8016 | ✅ |
| `Newtouch.HIS.Lib/NewtouchHIS.Lib.Base` (4) | DLL dùng chung → mọi app dùng nó (mục 3) | ✅ |
| `Newtouch.HIS.SSO/HIS.BaseAPI` (0 file) | **BaseAPI 7001: code riêng KHÔNG đổi**, nhưng DLL dùng chung (Lib.Base + Domain + DomainService) đổi → copy DLL mới + restart theo đúng quy tắc task | ✅ |
| `run-*-stg.sh`, `scripts/qa-phase2-pds07-cleanup-menu.sql` | Chỉ staging/SQL dọn menu — không thuộc prod deploy (staging do worker khác sở hữu) | — |

## 2. Build

- Build từ source ở HEAD, mỗi project ra thư mục output riêng (`~/workspace/redeploy-build/<app>`, ngoài repo) — **không build trực tiếp vào bin prod** để tiến trình đang chạy không bị ảnh hưởng giữa chừng.
- Quy tắc chạy: proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, tối đa 1 build cùng lúc (dưới mức tối đa 2 cho phép, vì RAM VM eo hẹp).
- **25/25 build exit 0, 0 error** (log: `~/workspace/redeploy-build.log`; output giữ tại `~/workspace/redeploy-build/`).
- **Sự cố duy nhất trong build (không ảnh hưởng prod):** đợt build đầu ra `/tmp` làm đầy tmpfs 512M → build CIS-Web abort (exit 134, "No space left on device"). Đã khắc phục: chuyển toàn bộ output sang `~/workspace` (đĩa còn 55G), giữ 12 build tốt, build lại 13 project còn lại — tất cả exit 0. Prod không bị ảnh hưởng vì build chưa từng ghi vào bin prod.

## 3. DLL dùng chung đã lan tới những app nào

MD5 DLL mới (build từ HEAD):

- `NewtouchHIS.Framework.Web.dll`: `5a87c61c3532411d96d39b065d582b90` (khác DLL prod cũ `832f1421…` — chứng tỏ prod trước đó đúng là cũ hơn)
- `NewtouchHIS.Lib.Base.dll`: `fc00a78f3a17f29ff33301ca2adb611f` (khác DLL prod cũ `e447c81d…`)
- `NewtouchHIS.Base.Domain.dll`: `0b00780d393687c12bde844c2607bb63`
- `NewtouchHIS.Base.DomainService.dll`: `2081a51112db0a912304c3ba6eed735d`

Nguyên tắc copy: **chỉ ghi đè DLL đã có sẵn trong bin prod, không thêm DLL mới vào bin nào** (tránh thay đổi dependency set ngoài ý muốn).

| DLL dùng chung | Đã copy vào (bin đã chứa sẵn DLL đó) | Không có trong bin → bỏ qua đúng |
|---|---|---|
| Framework.Web | SSO, MRQC Web+API, Sett Web, MRMS Web+API, OR Web+API, CIS Web+API, EMR Web+API, Herp Web+API, Base HOSP Web, Base Web, Base HOSP API, PDS Web+API | Sett-API 7002, AuthCenter 8015, WebAPI.Manage 8016 (không dùng Framework.Web) |
| Lib.Base | Tất cả app ở cột trái + AuthCenter 8015 + WebAPI.Manage 8016 + BaseAPI 7001 | Sett-API 7002 (bin không chứa Lib.Base.dll) |
| Domain + DomainService | SSO, AuthCenter 8015, WebAPI.Manage 8016, BaseAPI 7001 (các app ProjectReference/HintPath tới Domain) | Các app module còn lại (bin không chứa 2 DLL này) |

Lưu ý bẫy DLL đã tránh: fix SSO-03/SETT-16 nằm trong Framework.Web — đã copy DLL Framework.Web mới vào **mọi** app module (không chỉ SSO), nên Sett/MRQC cũng nhận fix; tương tự Lib.Base. Center host dùng Lib.Base qua HintPath `his-dll-common/Net6` (không phải ProjectReference): DLL Lib.Base mới đã được copy trực tiếp vào bin center, **không** dựa vào his-dll-common (xem mục 5.4).

## 4. Bảng từng app (deploy từng app một, kiểm chứng xong mới sang app tiếp)

Quy trình mỗi app: ghi PID cũ → sao lưu DLL prod vào `<app>/bin/<Cfg>/.bak-redeploy-20261028/` (nằm trong `bin/` nên bị gitignore, không làm bẩn tree) → kill đúng PID đó (không kill theo pattern) → copy DLL mới (app DLL + DLL project liên quan + DLL dùng chung mục 3) → khởi động bằng **launcher PROD** → kiểm chứng → mới sang app tiếp theo.

Kiểm chứng web: `GET /Login/Index` = 200 → `POST /Login/CheckLogin` (username `000000`, password md5) = `{"state":"success","message":"Đăng nhập thành công"}` → `GET /` và `GET /Home/Index` kèm cookie = 200.
Kiểm chứng API/center: port lắng nghe + tiến trình sống + probe 1 route thật không token → envelope fail-closed (40004 hoặc validation 400), **không 500** (API host không có trang login/trang chính).

| App (port) | PID cũ → mới | Build | Login kiểm chứng | Trang chính / API probe | Blocker |
|---|---|---|---|---|---|
| SSO (5208) | 2456 → 8505 | Release | ✅ success | ✅ `/` 200, `/Home/Index` 200 | Không |
| MRQC Web (5220) | 2514 → 8770 | Release | ✅ success | ✅ 200 / 200 | Không |
| MRQC API (5320) | 3278 → 8995 | Release | — (API) | ✅ `POST /api/Default/Query` → 200, code 40004 `TIMESTAMP_IS_REQUIRED` | Không |
| Sett Web (5221) | 2558 → 9208 | Release | ✅ success | ✅ 200 / 200 | Không |
| Sett-API (7002) | 3447 → 9382 | Release | — (API) | ✅ `POST /api/OutpBook/BookingESB` → 200, code 40004 `GATEWAY_PARAM_REQUIRED` | Không |
| MRMS Web (5232) | 2600 → 9609 | **Debug** (theo launcher, mục 5.1) | ✅ success | ✅ 200 / 200 | Không |
| MRMS API (5332) | 3174 → 9830 | Release | — (API) | ✅ `POST /api/Default/Query` → 200, code 40004 | Không |
| OR Web (5231) | 2641 → 10054 | **Debug** (theo launcher) | ✅ success | ✅ 200 / 200 | Không |
| OR API (5331) | 2683 → 10296 | **Debug** (theo launcher) | — (API) | ✅ `POST /api/Default/Query` → 200, code 40004 | Không |
| CIS Web (5234) | 3035 → 10537 | **Debug** (theo launcher) | ✅ success | ✅ 200 / 200 | Không |
| CIS API (5334) | 3083 → 10760 | **Debug** (theo launcher) | — (API) | ✅ `POST /api/Auth/GetToken` → 200, code 40004 | Không |
| EMR Web (5235) | 2811 → 10982 | Release | ✅ success | ✅ 200 / 200 | Không |
| EMR API (5335) | 2855 → 11206 | Release | — (API) | ✅ `POST /api/Auth/GetToken` → 400 validation `AppId là bắt buộc` (giống baseline, không 500) | Không |
| Herp Web (5236) | 2726 → 11435 | Release | ✅ success | ✅ 200 / 200 | Không |
| Herp API (5336) | 2767 → 11671 | Release | — (API) | ✅ `POST /api/Default/Query` → 200, code 40004 | Không |
| Base HOSP Web (5233) | 2899 → 11907 | Release | ✅ success | ✅ 200 / 200 | Không |
| Base Web (5238) | 2943 → 12192 | Release | ✅ success | ✅ 200 / 200 | Không |
| Base HOSP API (5333) | 2992 → 12466 | Release | — (API) | ✅ `POST /api/Auth/GetToken` → 200, code 40004 | Không |
| PDS Web (5237) | 3129 → 12742 | Release | ✅ success | ✅ 200 / 200 | Không |
| PDS API (5337) | 3222 → 13097 | Release | — (API) | ✅ `POST /api/Zyypyz/yzzx` → 200, code 40004 | Không |
| AuthCenter (8015) | 3324 → 13405 | Release | — (center) | ✅ `POST /api/Auth/GetAppToken` → 200, BusData code 40004 `AppId bắt buộc` (xem 5.3) | Không |
| WebAPI.Manage (8016) | 3376 → 13736 | Release | — (center) | ✅ `POST /api/Login/UserLogin` → 200, BusData code 40004 `AppId bắt buộc` | Không |
| BaseAPI (7001) | 2407 → 13893 | Không build lại (code riêng 0 file đổi); chỉ copy Lib.Base + Domain + DomainService + restart | ✅ qua SSO login sau restart (CheckPwd vẫn success) | ✅ port 7001 lắng nghe, SSO login success | Không |

- **MRQC-06:** chỉ cần redeploy (source đã sạch, DLL prod cũ hơn) — đã redeploy MRQC Web 5220 + MRQC API 5320, login + API đều đạt.
- **Kiểm chứng lại sau cùng:** sau khi BaseAPI restart (app cuối cùng), đã chạy lại login 11/11 web app (đều success + home 200) và probe 11/11 API/center host (đều fail-closed như bảng) — toàn bộ vẫn đạt. Static host 41142 không restart, header `Access-Control-Allow-Origin: *` vẫn live.

## 5. Ghi chú, độ lệch và việc báo lại điều phối viên (không tự ý sửa)

### 5.1 Độ lệch duy nhất so với task: 5 app build Debug thay vì Release

Launcher PROD hiện tại của 5 app trỏ vào `bin/Debug/net6.0` (từ trước, không phải do phase 2): MRMS Web, OR Web, OR API, CIS Web, CIS API. Nếu build Release mà launcher vẫn load Debug thì code mới không bao giờ chạy — nên worker build đúng cấu hình launcher đang dùng (Debug) cho 5 app này và ghi rõ ở bảng mục 4. **Không tự đổi launcher sang Release.** Đề nghị điều phối viên quyết trong phase 8 (chuẩn hóa Debug→Release + sửa launcher) — đó là thay đổi launcher, cần quyết riêng.

### 5.2 Không có rollback nào

0 lần rollback. Mọi app lên ngay lần đầu, port lên trong vòng kiểm chứng, không app nào phải khôi phục DLL backup. Các thư mục backup vẫn giữ nguyên tại `<app>/bin/<Cfg>/.bak-redeploy-20261028/` để rollback thủ công nếu cần (copy DLL từ đó về lại `bin/<Cfg>/` rồi restart bằng launcher prod).

### 5.3 Thay đổi hành vi có chủ ý (không phải regression): center code 20000 → 40004

Baseline trước deploy: `POST /api/Auth/GetAppToken` (8015) trả BusData `code 20000` khi thiếu AppId. Sau deploy: `code 40004`. Đây đúng là fix CTR-03 của phase 2 (lỗi thiếu tham số = FAIL 40004, không phải ERROR hệ thống 20000) đi qua Lib.Base `AppResultFilter` mới — bằng chứng DLL Lib.Base mới đã live trên center. Tương tự `POST /api/Login/UserLogin` (8016) cũng trả 40004.

### 5.4 `his-dll-common/Net6/NewtouchHIS.Lib.Base.dll` (file tracked) — XÁC NHẬN CUỐI: đã revert về HEAD, tree sạch, KHÔNG commit

**Xác nhận theo yêu cầu kiểm tra của điều phối viên (git status từng báo `M` file này, mtime ~14:17 ICT; worker Redis staging khẳng định không phải do họ):**

- **Đúng là do worker redeploy này gây ra — và là copy CỐ TÌNH, không phải build-scratch vô tình.** Lúc deploy SSO (~14:17 ICT, ngay sau khi build xong), worker đã copy DLL Lib.Base mới (`fc00a78f…`) vào `his-dll-common/Net6/` theo bài học cũ (center build lấy Lib qua HintPath này, không phải ProjectReference). Mốc 14:17 ICT khớp đúng thời điểm copy này.
- **Nhưng worker đã revert file này về HEAD trước khi kết thúc** (lúc 14:23:50 ICT, lệnh `git checkout --`), vì: (1) task gốc cấm commit DLL/PDB và chỉ cho commit khi launcher thay đổi — không launcher nào thay đổi nên không commit gì; (2) hệ thống đang chạy không cần file này — bin center đã chứa trực tiếp DLL Lib.Base mới (`fc00a78f…`), nên revert không ảnh hưởng runtime chút nào.
- **Bằng chứng xác minh cuối (sau revert):** `git status` chỉ còn `M roles-shots-progress.md` (của điều phối viên) và `?? scripts/sweep-final/`; file DLL **không còn trong status**; md5 file trên đĩa `1cfb5299…` **khớp đúng md5 của phiên bản HEAD** (không phải `fc00a78f…` của bản build mới). Worker Redis staging được minh oan: vết `M` đó là của worker redeploy, và hiện đã sạch.
- **Kết luận theo 2 nhánh điều phối viên đưa ra:** không thuộc nhánh (a) — không commit pathspec này (trái quy tắc no-DLL-commit của task gốc và không cần cho runtime); thuộc kết quả của nhánh (b) — file đã về HEAD trước khi kết thúc, không để bẩn không chủ đích.
- **Cảnh báo cho lần build center tiếp theo:** phải copy lại DLL Lib.Base mới vào `his-dll-common/Net6/` trước khi build center, nếu không center build sẽ đóng gói Lib.Base cũ.

### 5.5 Staging lên trong lúc redeploy — worker không đụng tới

Trong lúc worker đang deploy, staging stack đã lên (ghi nhận cuối đợt: port 6380, 15208, 17001 đang lắng nghe). Worker **không chạm** launcher staging, Redis staging 6380, hay tiến trình staging nào. Lưu ý không tránh được: staging dùng chung thư mục `bin/` với prod, nên tiến trình staging khởi động sau thời điểm copy DLL của một app sẽ chạy DLL mới của app đó. Điều phối viên/staging worker tự đánh giá ảnh hưởng (về nguyên tắc đó cũng là code HEAD cần test).

### 5.6 Những gì KHÔNG đụng tới (đúng task)

- Redis staging 6380 + launcher staging: không chạm.
- Database demo: không ghi gì (login kiểm chứng chỉ tạo session Redis, không ghi DB demo).
- Mật khẩu SA, mật khẩu Redis prod: không đọc nội dung, không thay đổi; launcher prod tự lấy từ file env 600 như cũ.
- Static host 41142: không restart (CORS vẫn live).
- `recovery.sh`: chỉ đọc để lấy đường dẫn launcher; không sửa.
- `roles-shots-progress.md`: không sửa (điều phối viên sở hữu).
- Không commit: không launcher nào thay đổi; không DLL/PDB/thư mục lạ nào được commit.

## 6. Việc còn lại cho điều phối viên (không thuộc phạm vi worker này)

1. Quyết chuẩn hóa Debug→Release cho 5 launcher ở mục 5.1 (phase 8) — hiện hệ thống chạy đúng nhưng 5 app vẫn ở cấu hình Debug.
2. Quyết có đưa `his-dll-common/Net6/NewtouchHIS.Lib.Base.dll` mới vào git hay giữ revert + quy trình refresh thủ công (mục 5.4).
3. Final sweep (all apps × all functions × desktop + mobile) chạy trên chính các DLL prod vừa deploy này.
