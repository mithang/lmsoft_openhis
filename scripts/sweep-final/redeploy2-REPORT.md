# REDEPLOY-2 — Deploy fix vòng sweep (FIX-1 + FIX-2) lên prod

- Worker: REDEPLOY-2. Ngày: 2026-10-28 (ICT).
- Base: nhánh V2.2, HEAD `e9d625e32c66256a85a7614cf80e02aee355561f` tại `~/workspace/openhis-core`
  (FIX-1 `3f8d9723` + FIX-2 `e9d625e3`; prod trước đợt này vẫn chạy code trong bộ nhớ từ build phase 2 `e66c7bda`).
- Tài khoản kiểm chứng: chỉ dùng đúng `000000` / `000000` (POST CheckLogin dùng `password = md5(000000) = 670b14728ad9902aecba32e22fa4f6bd`).
  Không có lần login nào báo khóa/sai; không đoán mật khẩu, không probe sai mật khẩu.
- Kết quả tổng: **16/16 app trong phạm vi đã deploy + kiểm chứng đạt, 0 blocker, 0 rollback.**
  Kiểm chứng sâu từng fix (A-01 Notice msgtype=1, A-02 SyncForm, Sett B3…) thuộc về re-sweep, không phải việc của redeploy.

## 1. Quy trình áp dụng cho mọi app (không bỏ bước)

1. Đọc launcher prod → giữ nguyên cấu hình build (Debug cho MRMS Web, OR Web, CIS Web/API; Release cho các app còn lại,
   MRMS API là Release theo launcher `run-mrms-api.sh`). Tuyệt đối không đổi Debug→Release, không sửa launcher.
2. Build từ HEAD `e9d625e3` sang thư mục build riêng `~/workspace/redeploy-build2/<app>` (không build vào bin prod đang chạy),
   tuần tự 1 build tại một thời điểm, proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`. Mọi build đều 0 error.
3. Sao lưu DLL cũ vào `<app>/bin/<Cfg>/.bak-redeploy2-20261028/` (kèm PDB) trước khi copy.
4. Kill đúng PID cũ (đối chiếu port owner trước khi kill), xác nhận tiến trình chết hẳn rồi mới copy DLL, rồi restart NGAY
   bằng launcher prod (không để tiến trình cũ chạy trên DLL mới — bài học PDS 5237 BadImageFormat).
5. Kiểm chứng xong mới sang app tiếp theo: web → login page 200 → CheckLogin `000000` success → `/` và `/Home/Index` 200
   kèm cookie; API/center → port lắng nghe + probe route thật không token, envelope fail-closed (không 500).
6. Nguyên tắc copy: **chỉ ghi đè DLL đã có sẵn trong bin prod và tương ứng với source đã đổi trong FIX-1/FIX-2**
   (app DLL khi project của app đổi + DLL infra/DomainService đã đổi). KHÔNG copy Framework.Web, KHÔNG copy Lib.Base
   (không source nào đổi trong đợt này). App nào chỉ đổi DLL dùng chung thì chỉ copy DLL đó, không copy app DLL.

## 2. Phạm vi đã xác định bằng project reference + kiểm tra bin thực tế

- FIX-1 đổi: `NewtouchHIS.Base.DomainService.dll` (MsgQueue `NoticeQueryPage`, SysModule `AddRange`),
  `Newtouch.Sett.Infrastructure.dll` (`Types_PatientManage`), và các web DLL chứa view đổi (SSO, Sett, MRQC).
- FIX-2 đổi: `Newtouch.HIS.Base.Infrastructure.dll`, `Newtouch.CIS.Infrastructure.dll`,
  `Newtouch.EMR.Infrastructure.dll`, `Newtouch.MRMS.Infrastructure.dll`, và các web/API DLL có file đổi
  (CIS Web, Herp Web, MRMS Web, OR Web, EMR Web + EMR API `ApiControllerBase`).
- Center 8015/8016: **đã kiểm chứng load assembly đã đổi** → DEPLOY cả hai.
  Cả hai đều ProjectReference trực tiếp `NewtouchHIS.Base.DomainService` và bin đều chứa DLL đó.
  WebAPI.Manage còn gọi trực tiếp `IMsgQueueDmnService` đã đổi (NoticeJobController, MsgQueueController,
  RemoteTreatedController). AuthCenter không gọi trực tiếp 2 method đã đổi nhưng load cùng assembly
  (ProjectReference + bin) nên nhận bản DLL mới cho đồng nhất — app DLL của AuthCenter/WebAPI.Manage không đổi
  source nên không copy app DLL (xem lưu ý §4).
- Các app KHÔNG deploy (lý do kiểm chứng, xem §3): BaseAPI 7001, Sett-API 7002, PDS 5237/5337,
  MRQC API 5320, OR API 5331, Herp API 5336.

## 3. Bảng từng app

DLL md5: cột "cũ → mới" là md5 trong bin prod trước/sau copy. "Đã trùng HEAD" = DLL trên đĩa trước khi copy
đã trùng md5 với build mới từ HEAD (do các build staging trước đó đã ghi đè bin prod trong khi tiến trình prod
vẫn chạy code cũ trong bộ nhớ) — restart lần này mới kích hoạt code HEAD, nên vẫn tính là deploy có restart kiểm chứng.

| App (port) | PID cũ → mới | Build | DLL đã copy (md5 cũ → mới) | Login / probe | Trang chính / kết quả | Blocker |
|---|---|---|---|---|---|---|
| SSO (5208) | 5655 → 23575 | Release | HIS.SSO.dll 7bc4… (đã trùng HEAD); NewtouchHIS.Base.DomainService.dll c7c1… (đã trùng HEAD) | ✅ success `Đăng nhập thành công` | ✅ `/` 200, `/Home/Index` 200 | Không |
| MRQC Web (5220) | 5734 → 23730 | Release | Newtouch.MRQC.Web.dll bb35… → 6812… | ✅ success | ✅ 200 / 200 | Không |
| Sett Web (5221) | 9584 → 23898 | Release | Newtouch.Sett.Web.dll f6db… (đã trùng HEAD); Newtouch.Sett.Infrastructure.dll e145… (đã trùng HEAD) | ✅ success | ✅ 200 / 200 | Không |
| AuthCenter (8015) | 6778 → 24088 | Release | NewtouchHIS.Base.DomainService.dll 2081… → c7c1… (app DLL không đổi source nên không copy) | — (center) | ✅ `POST /api/Auth/GetAppToken` `{}` → HTTP 200, BusData code 40004 `Id ứng dụng (AppId) bắt buộc phải truyền` (fail-closed, không 500; giống baseline phase 2) | Không |
| WebAPI.Manage (8016) | 6830 → 24229 | Release | NewtouchHIS.Base.DomainService.dll 2081… → c7c1… (app DLL không đổi source nên không copy) | — (center) | ✅ `POST /api/Login/UserLogin` `{}` → HTTP 200, BusData code 40004 `Id ứng dụng (AppId) bắt buộc phải truyền` (fail-closed, không 500) | Không |
| CIS Web (5234) | 6414 → 24425 | **Debug** (theo launcher) | Newtouch.CIS.Web.dll 76dc… → 5f94…; Newtouch.CIS.Infrastructure.dll dd45… → 144b… | ✅ success | ✅ 200 / 200 | Không |
| CIS API (5334) | 6461 → 24571 | **Debug** (theo launcher) | Newtouch.CIS.Infrastructure.dll dd45… → 144b… (trùng build CIS Web; API project không đổi source nên không copy API DLL) | — (API) | ✅ `POST /api/Auth/GetToken` `{}` → HTTP 200, code 40004 `TIMESTAMP_IS_REQUIRED` (fail-closed, không 500) | Không |
| Herp Web (5236) | 5992 → 24723 | Release | Newtouch.Herp.Web.dll f228… → 11e1… | ✅ success | ✅ 200 / 200 | Không |
| MRMS Web (5232) | 5818 → 24874 | **Debug** (theo launcher) | Newtouch.MRMS.Web.dll 7ab2… → ac29…; Newtouch.MRMS.Infrastructure.dll d734… → b4cf… (bản Debug) | ✅ success | ✅ 200 / 200 | Không |
| MRMS API (5332) | 6596 → 25009 | Release | Newtouch.MRMS.Infrastructure.dll 46db… → 29b2… (bản Release; API project không đổi source nên không copy API DLL) | — (API) | ✅ `POST /api/Default/Query` `{}` → HTTP 200, code 40004 `TIMESTAMP_IS_REQUIRED` (fail-closed, không 500) | Không |
| OR Web (5231) | 5865 → 25148 | **Debug** (theo launcher) | Newtouch.OR.Web.dll b697… → ea77… | ✅ success | ✅ 200 / 200 | Không |
| EMR Web (5235) | 6145 → 25330 | Release | Newtouch.EMR.Web.dll 82f9… (đã trùng HEAD); Newtouch.EMR.Infrastructure.dll bd96… → 4c01… | ✅ success | ✅ 200 / 200 | Không |
| EMR API (5335) | 6192 → 25508 | Release | Newtouch.EMR.API.dll 211c… (đã trùng HEAD); Newtouch.EMR.Infrastructure.dll bd96… → 4c01… (trùng build EMR Web) | — (API) | ✅ `POST /api/Auth/GetToken` `{}` → HTTP 400 validation `AppId là bắt buộc` (giống baseline trước đợt này, không 500) | Không |
| Base HOSP Web (5233) | 6253 → 25661 | Release | Newtouch.HIS.Base.Infrastructure.dll 6038… → 7d6c… (web project không đổi source nên không copy web DLL) | ✅ success | ✅ 200 / 200 | Không |
| Base Web (5238) | 6300 → 25819 | Release | Newtouch.HIS.Base.Infrastructure.dll 6038… → 7d6c… (web project không đổi source nên không copy web DLL) | ✅ success | ✅ 200 / 200 | Không |
| Base HOSP API (5333) | 6358 → 25973 | Release | Newtouch.HIS.Base.Infrastructure.dll 112f… → 7d6c… (API project không đổi source nên không copy API DLL) | — (API) | ✅ `POST /api/Auth/GetToken` `{}` → HTTP 200, code 40004 `TIMESTAMP_IS_REQUIRED` (fail-closed, không 500) | Không |

Backup DLL cũ giữ tại (mỗi app một thư mục, kèm PDB):

- `Newtouch.HIS.SSO/HIS.SSO/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.MRQC.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.Sett.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Center/NewtouchHIS.AuthenticationCenter/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.CIS/Newtouch.CIS.Web/bin/Debug/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.CIS/Newtouch.CIS.API/bin/Debug/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Herp/Newtouch.Herp.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.MRMS/Newtouch.MRMS.Web/bin/Debug/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.MRMS/Newtouch.MRMS.API/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.OR/Newtouch.OR.Web/bin/Debug/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.EMR/Newtouch.EMR.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.EMR/Newtouch.EMR.API/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.Web/bin/Release/net6.0/.bak-redeploy2-20261028/`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.API/bin/Release/net6.0/.bak-redeploy2-20261028/`

Rollback thủ công nếu cần: kill PID mới đúng app → copy DLL từ thư mục backup về lại `bin/<Cfg>/` → restart bằng
launcher prod → kiểm chứng lại như §1 bước 5. (Đợt này không app nào phải rollback.)

## 4. Lưu ý, độ lệch và việc báo lại điều phối viên

- **Bin prod đã bị ghi đè trước đợt này (không phải do worker này):** một số DLL trên đĩa (SSO, Sett, EMR Web/API)
  đã trùng md5 với build mới từ HEAD trước khi worker này copy, trong khi tiến trình prod vẫn chạy code cũ
  trong bộ nhớ (tiến trình SSO/CIS… khởi động từ trước FIX-1/FIX-2). Nguyên nhân khớp với cơ chế đã biết:
  build staging dùng chung thư mục `bin/` với prod nên đã ghi đè file đĩa. Worker này vẫn backup → kill → copy →
  restart đủ bước cho mọi app, nên code HEAD chỉ thực sự live sau restart lần này. Hệ quả cho lần sau: vẫn phải
  restart mới tính là deploy xong, không được kết luận "DLL đã mới nên khỏi restart".
- **App DLL build mới khác md5 dù source app không đổi:** ví dụ AuthCenter app DLL build mới `a315…` khác bin cũ
  `ae7f…` dù FIX-1/FIX-2 không sửa file nào của AuthCenter. Worker chỉ copy DLL của assembly đã đổi source
  (DomainService/infra) và KHÔNG copy app DLL trong các trường hợp đó, để không đưa thay đổi ngoài phạm vi vào prod.
  Điều phối viên có thể quyết định có muốn chuẩn hoá app DLL hay không ở đợt sau; runtime hiện tại không cần.
- **Nhóm Base trước đợt này chạy 2 bản infra khác nhau:** HOSP Web và Base Web cũ đều `6038…`, nhưng HOSP API cũ
  là `112f…`. Sau đợt này cả 3 đồng nhất `7d6c…` (build Release từ HEAD).
- **Không deploy — lý do kiểm chứng (không phải đoán mù):**
  - BaseAPI 7001 (PID 5571 giữ nguyên): có ProjectReference tới `NewtouchHIS.Base.DomainService` và bin chứa DLL đó,
    nhưng code BaseAPI chỉ gọi `ISysModuleDmnService.GetEntity` (qua `SystemedController.GetMenubyIdAsync`), không gọi
    2 method đã đổi trong FIX-1 (`MsgQueue.NoticeQueryPage`, `SysModule.AddRange`). Fix không tới live path nào của
    BaseAPI; task liệt kê BaseAPI trong nhóm KHÔNG deploy nên không restart. Nếu điều phối viên muốn đồng nhất DLL
    DomainService mọi nơi thì đó là quyết định riêng, không phải yêu cầu của fix.
  - PDS Web 5237 (PID 13744) / PDS API 5337 (PID 6652): PDS có `SysModuleDmnService` RIÊNG trong
    `PDS.Infrastructure`, controller PDS gọi service của chính nó, không gọi Center DomainService đã đổi;
    Center DLL trong bin PDS chỉ là bản copy transitive qua HintPath trỏ tới `lmsoft_openhis_run/.../HIS.BaseAPI/bin`
    cũ. Không deploy.
  - Herp API 5336 (PID 6070): không gọi method nào đã đổi; FIX-2 Herp chỉ đổi `HomeController` của Web. Không deploy.
  - MRQC API 5320 (PID 6706): csproj không reference assembly nào đã đổi; FIX-1 MRQC chỉ đổi 1 view của Web. Không deploy.
  - OR API 5331 (PID 5927): csproj không reference assembly nào đã đổi; FIX-2 OR chỉ đổi Web. Không deploy.
  - Sett-API 7002 (PID 6954): bin không chứa `Newtouch.Sett.Infrastructure.dll` (chỉ Sett.Web reference assembly này);
    `Types_PatientManage` đổi không tới Sett-API. Không deploy.
- **KHÔNG đổi:** Framework.Web DLL, Lib.Base DLL (không source nào đổi); Redis prod 6379 / staging 6380
  (staging chỉ có Redis nghe, không có tiến trình app staging nào chạy trong lúc deploy); mật khẩu SA;
  DB demo (không ghi gì — login kiểm chứng chỉ tạo session Redis); static host 41142; `recovery.sh` (chỉ đọc);
  launcher prod; `roles-shots-progress.md` (của điều phối viên).
- **KHÔNG commit gì.** Sau đợt deploy: HEAD vẫn `e9d625e3`; `git status` chỉ còn `M roles-shots-progress.md`
  (có từ trước đợt này) và các file `?? scripts/sweep-final/` (báo cáo các worker, có từ trước + báo cáo này).
  Build output nằm ngoài repo tại `~/workspace/redeploy-build2/`; log restart ghi vào `~/workspace/openhis-core/redeploy2.log`
  (file log, gitignore).

## 5. Kiểm chứng cuối sau app cuối cùng

- Tất cả 23 port prod đều đang lắng nghe: 5208, 5220, 5221, 5231, 5232, 5233, 5234, 5235, 5236, 5237, 5238,
  5320, 5331, 5332, 5333, 5334, 5335, 5336, 5337, 7001, 7002, 8015, 8016
  (gồm 6 app không deploy trong đợt này, vẫn chạy như trước, không restart).
- Login lại toàn bộ 10 web app đã deploy bằng `000000`/`000000`: **10/10 success** (`Đăng nhập thành công`)
  và trang `/` 200 (5208, 5220, 5221, 5231, 5232, 5233, 5234, 5235, 5236, 5238).
- 6 API/center host đã deploy đều probe fail-closed như bảng §3, không 500.
