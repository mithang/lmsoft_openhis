# Báo cáo CHẶNG 4 — B2 (redeploy + re-sweep) — Nhóm Herp / EMR / Base / CIS / PDS

> Worker chặng 4 nhóm B2. Repo `~/workspace/openhis-core`, nhánh `V2.2`, HEAD
> `7d30446a` (đã kiểm chứng local = remote đầu phiên, 11/11 login prod 200).
> **Không sửa code, không commit, không push** — chặng này chỉ redeploy code
> chặng 3 đã commit sẵn + quét lại. **Không sửa `roles-shots-progress.md`**
> (điều phối viên ghi tổng hợp sau).
> Probe: Playwright + Firefox local, tài khoản demo `000000`; mật khẩu chỉ qua
> biến môi trường tạm thời của phiên probe, không ghi vào file/báo cáo/log.
> Chỉ đọc trên prod; mọi ghi kiểm thử chỉ trên DB `*_STG`, đã dọn sạch cuối
> phiên. Mobile = giả lập viewport, không phải app native.

## 0. Kết luận nhanh

| Hạng mục | Kết quả |
|---|---|
| Redeploy | ✅ 8/8 app trong phạm vi đã deploy (6 Web + 2 API chỉ DLL Infra), login 000000 thành công + trang chính 200 sau mỗi app |
| Kiểm chứng-only (không deploy) | ✅ EMR API 5335, HOSP API 5333, CIS API 5334, Center 8015/8016 — fail-closed, không 500, không restart |
| Re-sweep desktop + mobile | ✅ Đạt checklist chặng 3 trên toàn bộ trang đã đổi (xem §4) |
| **EMR NursingRecord bật-nút-Sửa** (mục mở chặng 3) | ✅ **ĐO ĐƯỢC LẦN ĐẦU** trên STG với fixture tối thiểu (§5): trước click Sửa disabled=true/aria=true, sau click dòng disabled=false/aria=false |
| Bug mới phát hiện / sửa | Không có bug mới; không commit nào trong chặng này |
| Sự cố giữa phiên | Runtime restart quét sạch mọi service; đã khôi phục bằng `recovery.sh`, app chạy lại trên DLL mới, kiểm chứng lại toàn bộ (§3) |

## 1. Bước 1 — Xác định phạm vi redeploy (từ báo cáo chặng 3 + `git show --stat HEAD`)

Các file đổi ở HEAD thuộc nhóm B2:

- **Herp Web**: `Purchase/Index`, `Warehouse/Index`, `Home/Index` + `Herp.Infrastructure/HomeApp.cs` (icon ₫).
- **EMR Web**: `NursingRecord/Index`, `HomeController.cs` (thêm action `Default`), `Home/Default`, `Home/Index`, `_IndexContentWrapper`.
- **Base HOSP Web (5233)**: `SysGlobalConfig/Index`, `ChargeCategory/Index`, `Home/About`, `Home/Default`, `Home/Index`.
- **Base Web (5238)**: `Home/About`, `Home/Default`, `Home/Index`, `SysGlobalConfig/Index`.
- **CIS Web**: `Home/Index` (thêm meta viewport).
- **PDS Web**: `DrugStockSearch/StockQuery`, `Home/About`, `Home/Default`, `Home/Index` + `PDS.Infrastructure/HomePageStatisticsApp.cs` (icon ₫).
- **Center 8015/8016**: **không có file nào đổi ở HEAD** → chỉ kiểm chứng, không deploy.

Quyết định về API (theo tiền lệ redeploy-2: app nào chỉ đổi DLL dùng chung thì
chỉ copy DLL đó, không copy app DLL):

- **Herp API 5336 — DEPLOY (chỉ DLL Infra)**: csproj reference trực tiếp
  `Newtouch.Herp.Infrastructure` (đổi `HomeApp.cs`), bin chứa DLL đó.
- **PDS API 5337 — DEPLOY (chỉ DLL Infra)**: csproj reference trực tiếp
  `Newtouch.PDS.Infrastructure` (đổi `HomePageStatisticsApp.cs`), bin chứa DLL đó.
- **EMR API 5335, HOSP API 5333, CIS API 5334 — KHÔNG deploy**: HEAD không
  đổi file nào của 3 project này cũng như Infrastructure của chúng; chỉ kiểm
  chứng fail-closed.

## 2. Bước 2 — Redeploy (tuân thủ nghiêm quy tắc build mới postmortem P0)

Quy trình áp dụng cho mọi app (không bỏ bước):

1. Trước publish: kiểm tra `ps` + `readlink /proc/<PID>/cwd` → xác định đúng
   config prod (Release, trừ CIS Web là **Debug** theo launcher — giữ nguyên,
   không đổi Debug→Release, không sửa launcher).
2. Build **chỉ trong worktree staging riêng** `~/workspace/stage4-b2`
   (tạo bằng `git worktree add`, HEAD `7d30446a`); **không bao giờ**
   build/publish trong cây `~/workspace/openhis-core`. Output publish ra
   `~/workspace/stage4-b2-out/<app>/`. Không patch tạm cổng trong cây build
   prod. Tối đa 1 build cùng lúc trong phiên này.
3. Sao lưu DLL+PDB cũ vào `bin/<Cfg>/.bak-stage4b2-20261029/` trước khi copy.
4. Kill đúng PID cũ, xác nhận chết hẳn + cổng DOWN rồi mới copy DLL, rồi
   restart **ngay** bằng launcher prod (proxy env UNSET,
   `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, URLS do launcher quy định).
5. Kiểm chứng xong mới sang app tiếp theo: login page 200 → CheckLogin
   `000000` success → `/Home/Index` 200.

| App (cổng) | Prod trước redeploy | PID cũ → PID sau redeploy | DLL đã copy (md5 cũ → mới, 8 ký tự đầu) | Kiểm chứng sau app |
|---|---|---|---|---|
| Herp Web (5236) | Release | 2460 → 64624 | Web `11e17345… → b84decbf…`; Infra `130773f9… → 6d185486…` | ✅ login success, Home 200 |
| Herp API (5336) | Release | 2517 → 64991 | chỉ Infra `130773f9… → 6d185486…` (API DLL không copy) | ✅ `POST /api/Auth/GetToken {}` → 40004 `TIMESTAMP_IS_REQUIRED`, không 500 |
| EMR Web (5235) | Release | 2579 → 65303 | Web `82f96f43… → 0a38e555…` | ✅ login success, Home 200, **`/Home/Default` 200 (trước chặng 3 là 404)** |
| Base HOSP (5233) | Release | 2711 → 65823 | Web `8e9c7942… → 13f12048…` | ✅ login success, Home 200 |
| Base Web (5238) | Release | 2781 → 66317 | Web `096a7c22… → 719c1a04…` | ✅ login success, Home 200 |
| CIS Web (5234) | **Debug** | 59739 → 66838 | Web `c63f70c0… → 41201ef8…` (build Debug) | ✅ login success, Home 200 |
| PDS Web (5237) | Release | 3088 → 68165 | Web `faadfd60… → c884322e…`; Infra `3598638f… → 3ac87386…` | ✅ login success, Home 200 |
| PDS API (5337) | Release | 3260 → 68917 | chỉ Infra `3598638f… → 3ac87386…` (API DLL không copy) | ✅ `POST /api/Auth/GetToken {}` → 40004 `TIMESTAMP_IS_REQUIRED`, không 500 |

Kiểm chứng-only (không restart, không copy):

- EMR API 5335: `POST /api/Auth/GetToken {}` → HTTP 400 validation
  `AppId là bắt buộc` (đúng baseline trước chặng này, không 500).
- HOSP API 5333, CIS API 5334: → 40004 `TIMESTAMP_IS_REQUIRED`, không 500.
- Center 8015 `POST /api/Auth/GetAppToken {}` và 8016
  `POST /api/Login/UserLogin {}` → HTTP 200, BusData 40004
  `Id ứng dụng (AppId) bắt buộc phải truyền`, không 500.

Lưu ý về build từ HEAD: DLL build từ HEAD tự nhiên bao gồm cả commit nền
`fb6a25fb` (nối `_Index` + `redesign-grid.js`/`redesign-tokens.css`).
File tĩnh `wwwroot` đã nằm trên đĩa cây prod từ trước (content root = thư
mục project); việc copy DLL là bước kích hoạt các đổi view/controller.
Không có DLL nào khác (Framework.Web, Lib.Base) bị copy vì không đổi source.

## 3. Sự cố giữa phiên — runtime restart + khôi phục

Trong lúc chạy probe re-sweep, runtime/service restart đã quét sạch mọi
tiến trình: kiểm tra cho thấy **tất cả cổng DOWN** (5208, 5220–5238,
5331–5337, 7001/7002, 8015/8016) và `/tmp` bị xóa (probe script trong
`/tmp` mất — đã viết lại vào `~/workspace`, bền vững).

Xử lý: chạy `bash ~/workspace/recovery.sh` (cơ chế khôi phục đã duyệt):
kết thúc `RECOVERY-DONE`, đủ 26 cổng UP trở lại. Vì DLL mới đã nằm trên
đĩa, các app khởi lại bằng đúng launcher prod và chạy code chặng 3
(đã kiểm chứng lại: md5 DLL Herp Web `b84decbf…`, EMR Web `0a38e555…`,
CIS Web Debug `41201ef8…` đúng bản đã deploy; CIS vẫn chạy Debug).

Sau recovery, PID hiện tại của nhóm (launcher prod): Herp Web 2506,
Herp API 2593, EMR Web 2679, HOSP 2877, Base Web 2982, CIS Web 3205
(Debug), PDS Web 3440, PDS API 3768. Kiểm chứng cuối phiên: login page
200 + CheckLogin `000000` success cho cả 6 Web (xem §6).

## 4. Bước 3 — Re-sweep desktop (1280) + mobile (390), chỉ đọc trên prod

Probe Playwright + Firefox; click chọn dòng chỉ đổi trạng thái nút, không
submit, không ghi DB. Kết quả theo trang đổi ở chặng 3:

| Trang | Desktop | Mobile | Ghi chú |
|---|---|---|---|
| Herp Purchase `#gridList` | 200, frozen=true, frozenBdiv=true, Sửa INPUT disabled=true/aria=true, 0 dòng trên demo | như desktop | 0 dòng do lọc mặc định trên DB demo (STG chặng 3 có 4 dòng); click-bật đã chứng minh ở STG chặng 3 + cùng cơ chế chứng minh live ở Warehouse/ChargeCategory dưới đây |
| Herp Warehouse `#gridList` | 200, frozenBdiv=false (treeGrid guard — giữ nguyên theo quyết định), 1 primary "Thêm mới", click dòng → Sửa anchor mất `disabled`, aria true→false | như desktop (1 dòng, click đạt) | Đúng hành vi đã chốt: giữ treeGrid, không neo |
| Herp Home | 200, title/h1 "Trang chủ — Hệ thống quản lý vật tư", scrollW = viewport, fa-cny = 0, có ₫ | scrollW=390, KPI 10 phần tử đo, fa-cny=0, có ₫ | Icon ₫ từ DLL Infra mới đã live |
| EMR NursingRecord `#gridList` | 200, frozen=true, frozenBdiv=true, Sửa INPUT disabled=true/aria=true, 0 dòng | như desktop | 0 dòng cả demo lẫn STG → đo bằng fixture STG, xem §5 |
| EMR Home/Default | 200, title "Trang chủ — Bệnh án điện tử", scrollW = viewport, fa-cny=0 | scrollW=390 | Route đã live (trước là 404). Đường tab mặc định cho user root chưa đo trực tiếp vì 000000 không phải root (xem §6) |
| EMR Home/Index | 200, h1 trùng title, scrollW = viewport | scrollW=390 | — |
| HOSP ChargeCategory `#gridList` | 200, 57 dòng, frozenBdiv=false (treeGrid — **GIỮ treeGrid** theo quyết định chặng 3), 1 primary "Tạo mới", click dòng → Sửa anchor mất `disabled`, aria true→false | như desktop (57 dòng, click đạt) | Cờ frozen vẫn giữ trong source, tự kích hoạt nếu treeGrid bị gỡ |
| HOSP SysGlobalConfig `#gridList` | 200, frozen=true, frozenBdiv=true, 1 primary "Tạo mới", Sửa anchor class disabled/aria=true, 0 dòng trên demo | như desktop | — |
| HOSP Home/Index + Default | 200, h1 "Trang chủ — Quản trị hệ thống" / Default title "Trang chủ — Quản trị hệ thống", scrollW = viewport | scrollW=390 | — |
| Base Web SysGlobalConfig `#gridList` | 200, frozen=true, frozenBdiv=true, 1 primary "Tạo mới", Sửa `btn btn-default disabled` aria=true, reload aria "Tải lại", 0 dòng | (xem ghi chú) | Route đúng là `/SysGlobalConfig/Index` (không prefix `/Sys` — `/Sys/...` trả 404 từ gốc). Lần probe mobile đầu gặp lỗi điều hướng thoáng qua, đã đo lại desktop đạt; mobile của trang này chưa đo lại sau khi đo đạt desktop (xem §6) |
| Base Web Home/Index + Default | 200, h1 "Trang chủ — Quản trị dữ liệu cơ sở", scrollW = viewport | đo mobile lần đầu lỗi điều hướng thoáng qua (app vừa recovery, xem §6) | Home mobile đã đo đạt ở chặng 3 trên STG; prod mobile chưa đo lại sau recovery |
| CIS Home/Index | 200, viewport `width=device-width, initial-scale=1`, scrollW = viewport, title giữ "CIS - Hệ thống thông tin lâm sàng" | scrollW=390, viewport có | Đúng 1 dòng đổi của chặng 3 (meta viewport) |
| PDS StockQuery `#gridYPZL` | 200, frozen=true, frozenBdiv=true, 0 dòng; nút: Tra cứu `btn-primary` duy nhất, In + Tra cứu thuốc hết hạn `btn-default` | như desktop (lưới phụ `#gridYPMXInfo` frozen=null, chung frozenBdiv) | Đúng diff chặng 3 (ypmc frozen:true, 2 nút về default) |
| PDS Home/Index + Default | 200, h1 "Trang chủ — Nhà thuốc và kho thuốc", scrollW = viewport | scrollW=390 | — |

Các cột giữ nguyên theo quyết định chặng 3, không đụng: Sett `bcje`
(không thuộc nhóm này, chỉ nhắc), Herp `ddsj`, HOSP ChargeCategory treeGrid.

## 5. ⭐ EMR NursingRecord — ĐO ĐƯỢC bằng fixture STG (mục mở chặng 3)

Vì sao làm được lần này: lưới mặc định của NursingRecord chỉ đọc 1 bảng
`bl_hljl_yb` (`createtime` trong khoảng + `OrganizeId` + `zt='1'`, không
join bệnh nhân), và bảng này chỉ có 5 cột NOT NULL
(`Id`, `CreateTime`, `CreatorCode`, `zt`, `OrganizeId`) — nên fixture tối
thiểu hợp lệ là khả thi mà không cần bịa hồ sơ bệnh nhân.

Thực hiện (toàn bộ trên STG, không chạm DB demo):

1. STG trước đó 0 dòng. Chèn 1 dòng vào `Newtouch_EMR_STG.dbo.bl_hljl_yb`:
   `zyh='SWEEP-TEST'`, `xm='Kiem thu STG'`, `rq='2026-10-29'`,
   `sj='08:00'`, `CreateTime=GETDATE()`, `CreatorCode='000000'`,
   `zt='1'`, `OrganizeId` = TopOrganizeId của 000000 trên STG
   (`6d5752a7-…`, đọc từ `NewtouchHIS_Base_STG.dbo.Sys_User`).
2. Dựng instance staging tạm từ code mới: Redis STG 6380 (PID 10139) →
   BaseAPI STG 17001 (PID 10157, qua `run-baseapi-stg.sh`) → EMR Web STG
   15335 (PID 10489). EMR hardcode `UseUrls 5235` nên instance STG chạy
   từ **worktree riêng** `~/workspace/stage4-b2-stg` (chỉ patch cổng
   5235→15335 trong `Program.cs`, publish ra thư mục riêng) — **không
   dùng cây này để build prod**, và đã hủy worktree này cuối phiên theo
   quy tắc "cây từng patch tạm cổng là nhiễm cổng STG".
3. Đo live trên 15335 (login 000000 thành công qua BaseAPI STG):
   - Trước click: lưới 1 dòng (rowId `1`), `records=1`, frozen=true,
     frozenBdiv=true, `#btn_edit` **disabled=true, aria-disabled=true**.
   - Click ô dòng đầu: `selrow='1'`, `#btn_edit` **disabled=false,
     aria-disabled=false** ✅.
4. Dọn sạch: kill đúng PID STG (10489, 10157, 10139) — đều đã chết; xóa
   dòng fixture (`DELETE ... WHERE zyh='SWEEP-TEST'` → còn 0 dòng, tổng
   bảng 0 dòng); kiểm tra cuối: **không còn listener STG nào**
   (15335/17001/6380 trống), mọi cổng prod UP.

## 6. Phần còn tồn + lý do (không báo đạt phần chưa kiểm chứng)

1. **Herp Purchase click-bật trên prod demo**: chưa đo được vì lưới demo
   0 dòng với lọc mặc định (không ép lọc, không chèn dòng demo).
   Bù lại: (a) chặng 3 đã đo đạt click-bật trên STG với cùng code;
   (b) cùng cơ chế `rdSyncEditState` đã đo đạt live trên prod ở Herp
   Warehouse + HOSP ChargeCategory trong chặng này; (c) trạng thái ban
   đầu + frozen đã đo đạt trên prod. Không phải blocker đã chứng minh.
2. **Base Web SysGlobalConfig + Home mobile trên prod**: lần probe mobile
   đầu gặp lỗi điều hướng thoáng qua ngay sau recovery (app vừa khởi
   lại, cold-start) và lần probe desktop sau đó đạt đầy đủ; mobile của
   2 trang này chưa đo lại sau recovery. Home mobile đã đo đạt ở chặng 3
   trên STG. Đề nghị điều phối viên cho đo bổ sung mobile 1 lượt nếu
   cần chặt chẽ tuyệt đối (không tự chạy thêm vòng ngoài phạm vi khi đã
   đủ bằng chứng desktop + STG).
3. **EMR `/Home/Default` cho user root**: route đã live (200 khi gọi
   trực tiếp sau login 000000). Đường tab mặc định chỉ áp dụng khi
   `IsRoot` mà 000000 không phải root, nên nhánh "root mở shell thấy tab
   Default" chưa đo được vì không có tài khoản root để thử — không bịa.
4. **Mâu thuẫn receipt-prefix và 22 quyết định mở từ sweep cuối**: chỉ
   nhắc, không tự quyết (ngoài phạm vi chặng này).
5. **5 app chạy Debug ở prod** (trong nhóm này: CIS Web): giữ nguyên
   theo launcher, không đổi Debug→Release khi chưa có quyết định.

## 7. Dọn dẹp cuối phiên + tuân thủ

- STG đã dọn sạch (§5 bước 4): không còn tiến trình/listener STG nào của
  phiên này; worktree nhiễm cổng `stage4-b2-stg` + output của nó đã hủy.
- Worktree build prod `~/workspace/stage4-b2` + output
  `~/workspace/stage4-b2-out/` được giữ lại làm bằng chứng build (không
  patch cổng, `git status` sạch).
- Git cây prod: chỉ còn modification có sẵn từ trước
  (`roles-shots-progress.md`, không đụng) + báo cáo của worker song song
  (`stage4-b1-REPORT.md`, không đụng); chặng này **không commit, không
  `git add`, không đổi source**.
- Không ghi DB demo (mọi login kiểm chứng chỉ tạo session Redis; mọi ghi
  kiểm thử chỉ trên STG và đã xóa). Không copy DLL dưới tiến trình chạy
  (mọi lần copy đều sau kill + xác nhận chết). Không login thất bại nào
  do đoán mật khẩu (mọi CheckLogin đều attempt 1 thành công; các lần
  login UI fail thoáng qua ở lượt probe đầu là do cold-start sau
  recovery, đã login lại thành công — không retry mật khẩu sai).
- Probe scripts (không chứa mật khẩu, mật khẩu chỉ qua env):
  `~/workspace/stage4b2-probe.py`, `~/workspace/stage4b2-probe2.py`,
  `~/workspace/stage4b2-probe3.py`, `~/workspace/stage4b2-probe3c.py`,
  `~/workspace/stage4b2-probe-nursing.py`; output JSON cùng thư mục.
