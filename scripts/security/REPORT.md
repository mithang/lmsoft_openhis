# BÁO CÁO GIAI ĐOẠN 7 — SIẾT BẢO MẬT OpenHIS

- Base: nhánh V2.2, checkout `~/workspace/openhis-core` (lúc bắt đầu giai đoạn: tip `9676cac9`).
- **KHÔNG commit, KHÔNG push trong giai đoạn này** — điều phối viên soát rồi commit.
- **KHÔNG in secret trong báo cáo này**: không có mật khẩu SA, mật khẩu Redis, JWT/token đầy đủ,
  giá trị md5 mật khẩu, AppSecret, JWT signing key. Những chỗ cần giá trị chỉ ghi "đã đổi/chuyển sang env".
- Ngôn ngữ: tiếng Việt. Mọi kiểm chứng ghi chạy trên DB `*_STG`, trừ các login/probe đọc trên prod.
- Ngoại lệ duy nhất về ghi DB demo: KHÔNG có. Tài khoản demo trên DB live GIỮ NGUYÊN sau giai đoạn này
  (xem mục 3 — quyết định này do điều phối viên đặt ra vì giai đoạn 8–10 vẫn cần login demo để kiểm chứng).

## 0. Trạng thái cuối giai đoạn

- Prod **26/26 cổng UP**: 1433, 6379, 41142, 5208 SSO, 7001 BaseAPI, 7002 Sett-API, 8015 AuthCenter,
  8016 WebAPI.Manage, webs 5220/5221/5231–5238, APIs 5320/5331–5337.
- SSO login `000000` thành công; login `000000` thành công trên cả 11 web sau mọi đợt restart.
- Cụm staging đã dừng hoàn toàn theo PID chính xác (Redis 6380, BaseAPI-stg 17001, HOSP-stg 15233,
  AuthCenter-stg 18015).
- Sự cố trong giai đoạn (ghi trung thực):
  - Runtime restart giữa giai đoạn đã kill toàn bộ tiến trình prod (và staging). Đã chạy
    `~/workspace/recovery.sh` **MỘT lần**, sau đó kiểm chứng lại đủ 26 cổng.
  - Một lệnh exec bị nhân đôi + service restart giữa chừng làm 2 center prod (8015/8016) down;
    đã khôi phục bằng recovery + roll lại 8016, kiểm chứng lại GetAppToken và no-token 401.
  - Một lệnh restart API hàng loạt bị lỗi đường dẫn `~` trong chuỗi quote nên 9 API bị kill mà
    không restart được; đã phát hiện bằng kiểm tra cổng và khởi động lại đủ 9 API ngay sau đó.
  - `/tmp` bị xóa sau runtime restart (đúng như tài liệu: `/tmp` là tạm). Các script `/tmp/sqlq.sh`,
    `/tmp/roll-lib.sh`, `/tmp/restart-app.sh` đã được tạo lại khi cần.

## 1. [P0] HOSP JSON API vượt qua xác thực — ĐẠT (staging + prod)

### 1.1 Nguyên nhân gốc (đọc code, không đoán)

- Filter global `FrameworkAuthorizationFilter` (`Newtouch.HIS.SSO/NewtouchHIS.Lib.Framework/Filter/FrameworkAuthorizationFilter.cs`)
  đăng ký global trong `Program.cs` của mọi web app, nhưng **bỏ qua mọi endpoint có metadata `HandlerLoginAttribute`**.
- 6 app đặt `[HandlerLogin]` trên lớp `ControllerBase` địa phương, nên filter bỏ qua TOÀN BỘ action của mọi
  controller kế thừa base đó — trừ action `Index` (vì `BaseController.Index()` trong Framework mang
  `[HandlerAuthorize]` riêng, một action filter kiểm tra session và 302 về login; đó là lý do trang View 302
  trong khi JSON 200). `Form()` (không có attribute) và `Details()` cũng bypass — đã probe thấy 200 không-cookie
  trên prod trước sửa.
- Không có caller hợp pháp không-session nào ngoài Login/Home/ClientsData (mỗi app đều có attribute riêng
  trên các controller đó — đã kiểm tra từng app). Ngoại lệ hẹp đã biết, giữ nguyên: CIS
  `PrescriptionController.QhdSvscis` mang `[HandlerAuthorizeIgnore]` (chỉ ảnh hưởng HandlerAuthorize,
  KHÔNG miễn filter global) nên vốn đã bị chặn không-cookie — ghi nhận là gap riêng, không phải hồi quy do sửa này.
- Không có caller server-side nào gọi web JSON (grep `GetGridJson` ngoài controller chỉ ra repo method trùng tên,
  không phải HTTP caller).
- Các app sạch từ đầu (kế thừa `OrgControllerBase` trực tiếp, không qua base mang HandlerLogin):
  MRMS, OR, EMR, MRQC, CIS-phần-Org, SSO portal.

### 1.2 Baseline probe không-cookie trên prod (trước sửa, chỉ đọc)

- Bypass 200 (đã thực thi): HOSP `Sys/Duty/GetGridJson` (trả dữ liệu thật), HOSP `Sys/Organize/Form` + `Details` (HTML),
  Sett `Treatment/loginfy` + `Com/GetSelectItemsDetailListByItemCode` + `Treatment/AForm`,
  CIS `InfusionManage/Query` (chạy tới lỗi null-binding), Herp `PurchaseManage/Purchase/PurchaseView` (HTML),
  PDS `PharmacyDepartment/GetGridJson` (chạy tới lỗi SQL), Base.Web `Organize/GetGridJson`
  (route đúng là `/Organize/...`, không phải `/Sys/Organize/...`).
- Sạch 302: MRMS `MRHomePage/MainRecord/Main`, OR `Operation/OpApply/Getbqlist`,
  EMR `MRHomePage/MRHomePage/GetGridList`, MRQC `QualityControlManage/Apply/GetBlApplyList`,
  CIS `DoctorManage/DoctorsAdvice/AdviceList`, SSO `SysManage/User/GetPagintionGridJson`, HOSP/Sett `Index`
  (nhờ HandlerAuthorize).

### 1.3 Sửa (6 file, main checkout, chưa commit)

Gỡ `[HandlerLogin]` khỏi 6 `ControllerBase` (kèm ghi chú SECURITY trong file, gỡ `using` thừa):

- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web/Controllers/ControllerBase.cs`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.Web/Controllers/ControllerBase.cs`
- `Newtouch.HIS.CIS/Newtouch.CIS.Web/Controllers/ControllerBase.cs` (chỉ 2 controller dùng base này:
  `InfusionManageController`, `ClinicMettingController`)
- `Newtouch.HIS.Herp/Newtouch.Herp.Web/Controllers/ControllerBase.cs`
- `Newtouch.HIS.PDS/Newtouch.PDS.Web/ControllerBase.cs`
- `Newtouch.Sett.Web/Controllers/ControllerBase.cs` (116 controller kế thừa: 114 `Newtouch.Sett.Web.Controllers.ControllerBase` + 2 trực tiếp — rộng nhất)
- Thêm launcher staging mới `run-base-web-stg.sh` (port 15238, DBConf STG, BaseAPI 17001, Redis 6380,
  theo mẫu `run-hosp-stg.sh`).

### 1.4 Kiểm chứng staging (mỗi app: không-cookie bị chặn + có-cookie chạy bình thường)

Login staging qua `Login/CheckLogin` form `username` + `password` dạng md5 (tính bằng `md5sum` vào biến, không in).

- HOSP-stg 15233 (BaseAPI-stg 17001, Redis 6380): không-cookie `Sys/Organize/GetGridJson`,
  `Sys/Duty/GetGridJson`, `Sys/Organize/Form` → 302; AJAX (header `X-Requested-With: XMLHttpRequest`) → 200 JSON
  `SESSION_TIMEOUT`; POST `Sys/SysGlobalConfig/SubmitForm` không-cookie (code `G7NOLOGIN`) → 302, đếm STG = 0 dòng.
  Có-cookie 000000: `GetGridJson` → dữ liệu (gồm khoa `G6KHOA2` STG), `SubmitForm` đủ trường
  (Code `G7WITHLOGIN`, Name, Value) → success, STG `Sys_GlobalConfig` = 2 dòng (`G6CFG` cũ + `G7WITHLOGIN`).
  Baseline STG trước kiểm chứng: `Sys_GlobalConfig`=1, `Sys_Organize`=4.
- Sett-stg 15221: không-cookie `loginfy`, `Com/...`, `AForm`, `Index` → 302 (trước sửa: 200);
  có-cookie login success, `loginfy` → `[]`, `Index` → HTML 200.
- CIS-stg 15234: không-cookie `InfusionManage/Query` → 302 (trước: 200); có-cookie → chạy tới đúng lỗi
  null-binding cũ (hành vi giữ nguyên, chỉ thêm chặn auth).
- Herp-stg 15236: không-cookie `PurchaseView` → 302 (trước: HTML 200); có-cookie → HTML 200.
- PDS-stg 15237: không-cookie `PharmacyDepartment/GetGridJson` → 302 (trước: 200 lỗi SQL); có-cookie → action
  thực thi (trả error JSON của app; worktree build tiền-dịch nên message còn tiếng Trung — không phải hồi quy).
- Base.Web-stg 15238 (launcher mới): không-cookie `Organize/GetGridJson` → 302 (trước: 200); có-cookie → dữ liệu STG.
- Worktree diff đã kiểm tra: copy 6 file sang worktree chỉ mang đúng sửa này (worktree `ControllerBase` = main
  đã commit trước sửa; các diff `Program.cs` cổng là staging-only có sẵn).

### 1.5 Triển khai prod mục 1 (mỗi app: kill đúng PID → build từ main → launcher prod → probe)

- HOSP 5233 (launcher `run-base-hosp.sh`, Release): không-cookie Duty/Form → 302; login success; cookie Duty → 200.
- Base.Web 5238 (`run-base-web.sh`, Release): không-cookie → 302; login success; cookie → 200.
- CIS 5234: **bài học quan trọng — lần restart đầu vẫn 200 vì launcher prod `Newtouch.HIS.CIS/run-5234.sh` chạy
  `bin/Debug/net6.0/Newtouch.CIS.Web.dll` trong khi tôi build Release**; đã build lại **Debug** từ main
  (giữ nguyên cấu hình prod hiện hữu, không đổi sang Release trong giai đoạn này — CIS web prod chạy Debug là
  trạng thái tiền-tồn tại, cùng lớp vấn đề với EMR Debug→Release, để giai đoạn đóng gói chuẩn hóa), restart lại →
  không-cookie → 302; login success; cookie → 200.
  **Từ đây: luôn kiểm tra đường dẫn DLL/cấu hình trong launcher trước mỗi restart.**
- Herp 5236 (Release): không-cookie `PurchaseView` → 302; login success; cookie → 200.
- PDS 5237 (Release): không-cookie → 302; login success; cookie → 200.
- Sett 5221 (`Newtouch.HIS.Sett/run-sett.sh`, Release): không-cookie `loginfy`/`AForm` → 302; login success;
  cookie `loginfy` → 200.
- **Kết luận mục 1: ĐẠT.** Không app nào còn bypass trong 6 app đã sửa; MRMS/OR/EMR/MRQC/SSO vốn sạch.
  Cuối giai đoạn kiểm chứng lại: HOSP Duty không-cookie → 302, Sett loginfy không-cookie → 302.

## 2. [P0] Quét + sửa đường ghi hardcode tên DB demo — ĐẠT (staging + prod)

### 2a. MRMS guidang/CXguidang — đã sửa + kiểm chứng staging đầy đủ

- File: `Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure/Repository/MrbasyRepo.cs`:
  - `Updatebazt` (guidang): tên DB lấy từ cấu hình (`MrmsDb`/`EmrDb` qua `ConfigInitHelper.DbConfig.DBConf`,
    fallback tên gốc) thay cho `[Newtouch_MRMS]`/`[Newtouch_EMR]` hardcode; đồng thời tham số hóa câu SQL
    (trước đó nối chuỗi `dataId`/`ZYH`/`XM`).
  - `CXUpdatebazt` (CXguidang): câu EMR đổi sang tên cấu hình + **thêm điều kiện `zyh=@ZYH`**
    (trước: `where 1=1 and zt='1'` đổi toàn bộ dòng zt=1 — chính lỗi đã ghi thật 1 dòng demo EMR trong giai đoạn 6:
    zyh 01000 RecordStu 1→2 lúc 2026-10-27 13:49:59, đã khôi phục về 1 + thời gian gốc 2026-10-26 15:39:09.017).
  - Sau kiểm chứng lần 1 phát hiện sót: câu SQL đầu của `CXUpdatebazt` và `CXGDZT` vẫn hardcode
    `[Newtouch_MRMS]` (làm CX trả 0 trên staging dù dòng EMR đã đổi) — đã sửa cả hai sang tên cấu hình.
  - `RecordDmnService.updateRecordStu`: tên EMR sang cấu hình (lưu ý gap còn lại: tham số `id` vẫn nối trực tiếp
    vào `IN (...)`, và id/rỗng + organizeId rỗng vẫn có thể update toàn bộ — chỉ sửa tên DB, chưa sửa injection).
  - `RecordDmnService.LockRecord` (update bảng động `bllxmc` từ `[Newtouch_EMR]`): đổi sang tên cấu hình.
  - `MainRecordDmnService` (ghi đè EMR `mr_basy`): đổi sang tên cấu hình.
- Cơ chế cấu hình đã xác minh bằng đọc code: `ConfigInitHelper.DbConfig` được bind từ `IConfiguration`
  (gồm env override `BusinessDB__DBConf__*__DBName`) và là cùng nguồn `SqlSugarDbContext` dùng; repo MRMS chạy
  trên connection `MrmsDb` (`MrmsDb.GetConnection()`), đã được probe giai đoạn 6 chứng minh đọc đúng STG.
- **Quyết định kiến trúc (quan trọng, đừng làm ngược)**: ban đầu định đưa helper dùng chung vào
  `NewtouchHIS.Lib.Base/Init/ConfigInit.cs`, nhưng đã REVERT hoàn toàn (git diff Lib.Base phần này = sạch).
  Lý do: các csproj Infra tham chiếu `NewtouchHIS.Lib.Base.dll` qua HintPath trỏ vào bin của run copy
  (`lmsoft_openhis_run/.../NewtouchHIS.Framework.Web/bin/Release/...`) — refresh DLL chung sẽ đụng run copy
  (bị cấm) và là quy trình của mục 5. Thay vào đó mỗi Infra assembly có file helper riêng `DbNames.cs`
  (class `internal static DbNames`, method `Name(connId, fallback)` trả về `[tên]` đã quote, dùng
  `ConfigInitHelper.DbConfig` hiện có — không cần sửa Lib). Các file MRMS đã chuyển sang dùng `DbNames` chung
  của Infra MRMS.
- Kiểm chứng staging MRMS-stg 15232 (lần 1, trước khi sửa câu CX đầu):
  - Fixture STG: `Newtouch_MRMS_STG.mr_basy` 1 dòng (Id `G6-PROBE-BASY`, BAH `G6BAH`, zyh 01000,
    xm "Benh nhan G6 sua", bazt NULL); `Newtouch_EMR_STG.mr_basy` 0 dòng → đã chèn fixture `G7-EMR-BASY`
    (zyh 01000, cùng xm, bazt 1; đủ cột NOT NULL); `Newtouch_EMR_STG.zy_brjbxx` ban đầu 1 dòng 01000
    (RecordStu 1, zt 1) → đã chèn thêm dòng đối chứng 01999 (RecordStu 1, zt 1).
  - `guidang` (dataId `G6-PROBE-BASY`, ZYH 01000) → trả 1; STG MRMS bazt NULL→3, STG EMR bazt 1→3,
    demo không đổi (MRMS demo 0 dòng, EMR demo 01000 vẫn RecordStu 1).
  - `CXguidang` lần 1 → trả 0 (do sót câu MRMS hardcode), nhưng STG EMR 01000 đã 1→2, đối chứng 01999 giữ 1,
    demo giữ 1 — chứng minh scoping zyh đúng, chỉ còn lỗi câu MRMS.
- Kiểm chứng staging lần 2 (sau sửa câu CX đầu, rebuild worktree, restart MRMS-stg): `CXguidang` → trả 1;
  STG MRMS bazt 3→2; STG EMR 01000 giữ 2, 01999 giữ 1; demo EMR 01000 giữ 1. **Mục 2a ĐẠT trên staging.**
- Kiểm chứng bổ sung `updateRecordStu` (STG, sau refactor `DbNames`): gọi với Id GUID → vẫn lỗi cú pháp
  SQL như trước sửa (lỗi injection có sẵn, không ghi gì cả hai DB; demo EMR 01000 giữ 1, STG giữ 2/1).
  Kết luận: hành vi không đổi, chỉ đổi đích DB sang cấu hình (lần gọi với Id số hợp lệ trong tương lai sẽ
  trúng STG thay vì demo). Gap injection `id` ghi ở §10.
- Sự cố copy file đã gặp và đã sửa: lệnh `cp` thừa đã tạo `Repository/RecordDmnService.cs` trùng trong worktree
  gây 12 lỗi CS0101/CS0111 khi build — đã xóa bản thừa, build lại 0 lỗi.
  Bài học: copy từng file đúng đích, không dùng `cp a b <thư-mục>`.

### 2b. Quét toàn cây (đợt 1: dạng `..dbo`) — đã sửa + build 0 lỗi

- Phương pháp quét: script Python quét `.cs` (trừ `obj/`, `bin/`, `.git`, các thư mục scratch `herp-*-tmp`,
  `sett-stage-*`) tìm tên DB 3 phần (`[Newtouch_*]..dbo` / `Newtouch_*..dbo`) trong cửa sổ 400 ký tự sau từ khóa
  UPDATE/INSERT/DELETE → 74–77 điểm; sau đó phân loại thủ công từng điểm thành **đích GHI thật** vs
  **nguồn ĐỌC trong câu ghi** vs **false positive** (tên method `Update`/`Delete`, tên repo).
- **Đã sửa (đích ghi thật, dùng `DbNames.Name(ConnId, fallback)`; chuỗi `const` chuyển thành `string` khi cần nối;
  chuỗi verbatim `@"..."` nối bằng cách đóng/mở chuỗi quanh phép nối — đã xác minh là C# hợp lệ)**:
  - EMR: `BlmblbRepo.cs:149` (INSERT `bl_hljldata`, EmrDb), `HljlDataRepo.cs:46,57` (UPDATE/INSERT `bl_hljldata`,
    EmrDb; đoạn INSERT ở :51 là code comment — để nguyên), `MRHomePageDmnService.cs:100` (UPDATE `mr_basy` —
    resolve cả CIS read + EMR write), `:153–154` (UPDATE `mr_basy` với hàm Sett `get_zycs` — resolve cả SettDb +
    EmrDb), `:1371` (DELETE CIS `zy_PatDxInfo`, CisDb).
  - Herp `WzProductDmnService.cs:366–367,458–459,510` (INSERT/UPDATE/DELETE Base `xt_sfxm`, BaseDb; các SELECT đọc
    `Sys_Config` herp + `wz_unit` Base trong cùng batch cũng resolve HerpDb/BaseDb. Lưu ý: INSERT ở
    InsertProductToSfxm hiện bị comment lệnh thực thi — vẫn sửa cho đúng khi được bật lại; UPDATE ở
    updateProductToSfxm thì đang thực thi).
  - OR: `ORApplyInfoRepo.cs:67,72` (SELECT đọc + UPDATE CIS `zy_lsyz`, CisDb),
    `SysChargeItemDmnService.cs:81,96,124,138` (UPDATE/INSERT Sett `xt_sfmb`/`xt_sfmbxm`, SettDb; cả 2 khối SELECT
    kiểm tra trùng `xt_sfmb` cũng resolve SettDb), `Temporary_ordersRepo.cs:22` (INSERT CIS `zy_lsyz`, CisDb;
    đã chuyển `string.Format` thành nối chuỗi và bỏ dấu `)` thừa ở cuối).
  - CIS: `DeptMedicineApplyNoDmnService.cs:57,96` (UPDATE CIS `zy_bqksby`/`zy_ksbyth`, CisDb),
    `DoctorserviceDmnService.cs:1885–1886,2045–2046` (UPDATE OR `OR_ApplyInfo`/`OR_ApplyInfo_Expand`, OrDb — 2 khối
    giống nhau), `OrderAuditDmnService.cs:1030,1093,1112,1198` (INSERT CIS `xt_gmxx`, CisDb; gồm cả 2 khối
    `string.Format` đã chuyển nối chuỗi + sửa dấu đóng; :1175–1176 là SELECT đọc — bỏ qua),
    `HospInterfaceSyncDmnService.cs:47` + `SettInterfaceSyncDmnService.cs:42` (UPDATE Base `xt_cw`, BaseDb;
    giữ nguyên khoảng trắng `dbo. xt_cw` trong SQL gốc), `AllergyManageDmnService.cs:341` (UPDATE CIS `xt_gmxx`,
    CisDb), `OutpatientNurseDmnService.cs:105` (INSERT CIS `xt_gmxx`, CisDb),
    `ReportDmnService.cs:539,545,588` (UPDATE/INSERT Base `ReportSvsConfig`, BaseDb).
  - PDS: `DrugStorageDmnService.cs:399,465` (UPDATE view Base `V_S_xt_yp`, BaseDb),
    `DispenseIndexInfoDmnService.cs:181,197,288,303,319,607,623,639` (3× UPDATE `zy_ypyzxx`, 3× UPDATE `xt_yp_kcxx`,
    2× INSERT `zy_ypyzczjl`, PdsDb), `ZyYpyzzxRepo.cs:116` (UPDATE `zy_ypyzxx`, PdsDb),
    `SysMedicineAntibioticTypeDmnService.cs:108,140` (Base `xt_kssType`, BaseDb),
    `SysMedicineBaseDmnService.cs:136` (INSERT Base `xt_yfbm_yp`, BaseDb),
    `SysMedicineDmnService.cs:719,776,863,919` (UPDATE/INSERT Base `xt_yp`/`xt_ypsx`, BaseDb),
    `SysPharmacyDepartmentBaseDmnService.cs:126,172` (Base `xt_yfbm`, BaseDb),
    `SysPharmacyWindowDmnService.cs:64,107` (Base `xt_yfck`, BaseDb),
    `PDS.API/Controllers/HospitalizationController.cs:130` (UPDATE `zy_ypyzxx`, PdsDb).
  - MRMS: xem 2a (MrbasyRepo, RecordDmnService, MainRecordDmnService).
  - File helper mới `DbNames.cs` đã tạo ở 7 nơi: CIS Infra, EMR Infra, Herp Infra, OR Infra, PDS Infra,
    MRMS Infra, PDS API. Các project đều SDK-style (file `.cs` mới tự include) và đều đã tham chiếu
    `NewtouchHIS.Lib.Base.dll`. Mỗi file sửa đều được thêm `using DbConfigHelper;`.
  - Lỗi build đã gặp và sửa: 3 chỗ nối chuỗi sau verbatim dùng `"..."` thường thay vì `@"..."` gây
    CS1010/CS1002/CS1003 (OR `SysChargeItemDmnService.cs:97`, CIS `HospInterfaceSyncDmnService.cs:48`,
    `SettInterfaceSyncDmnService.cs`, CIS `ReportDmnService.cs` ×2) — đã sửa sang verbatim, build lại 0 lỗi.
- **Bổ sung cấu hình để resolver có ConnId**:
  - `Newtouch.HIS.CIS/Newtouch.CIS.Web/appsettings.json`: thêm entry `OrDb` (`DBName` `Newtouch_OR`, `DBType` 1,
    `Enabled` true) vào cuối DBConf (index 7) bằng chèn surgical trên JSON một dòng (lần đầu dùng `json.dump`
    đã reformat toàn file 107 dòng — đã revert và làm lại, diff nay chỉ 1 dòng). CIS API dùng chung appsettings
    của CIS Web (theo `Program.cs` của CIS API) nên cũng hưởng.
  - `Newtouch.HIS.PDS/Newtouch.PDS.Web/appsettings.json`: thêm entry `CisDb` (`DBName` `Newtouch_CIS`) vào cuối
    DBConf (index 3, diff append-only 7 dòng). PDS API dùng chung appsettings của PDS Web nên cũng hưởng.
  - `run-cis-stg.sh`: thêm `BusinessDB__DBConf__7__DBName=Newtouch_OR_STG`; `run-pds-stg.sh`: thêm
    `BusinessDB__DBConf__3__DBName=Newtouch_CIS_STG`. Trên prod, entry mới trỏ đúng tên DB demo cũ nên hành vi
    không đổi (chỉ thêm một connection khả dụng).
  - DBConf các app khác đã đủ: EMR (Base/Sett/Emr/Mrms/MrQc/Cis/Union), Herp (Base/Herp/Union),
    OR (Base/Sett/Emr/Mrms/MrQc/Cis/Union/Or), MRMS (đủ 7), Sett (đủ 7), HOSP/Base (đủ 7).
    PDS Web gốc chỉ có Base/Union/Pds; CIS Web gốc thiếu OrDb.
- **TSqlStock (PDS)**: batch `KsbyStock` (dòng ~925) và `KsbyTslStock` (~1026) trong
  `Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/TSQL/TSqlStock.cs` là `public const string` nên không nối chuỗi được
  — đã chuyển cả hai thành `public static string ... => ...` (caller duy nhất `DispenseIndexInfoDmnService.cs:1988`
  dùng như giá trị string nên tương thích), thay 4 điểm CIS trong `KsbyStock` (1 SELECT EXISTS đọc + 1 UPDATE +
  1 INSERT `xt_ksby_kcxx` + 1 UPDATE `zy_bqksby`... thực tế 4 điểm) và 3 điểm CIS trong `KsbyTslStock`
  (2 SELECT đọc + 1 UPDATE `zy_bqksby`) bằng `DbNames.Name("CisDb", "Newtouch_CIS")`. Các tên bảng không qualifier
  trong batch (`[xt_yp_kcxx]`, `[xt_bqksby]`, `[xt_ksbyth]`) chạy trên DB của connection — để nguyên.
  File này không có `using` nào nên `using DbConfigHelper;` đặt ở đầu file.
- **Không sửa (ghi nhận là đọc rò rỉ, xem 2d)**: `MrFeeRelDmnService.cs:281,310` (INSERT vào bảng local
  `mr_dic_sfxmrel`, SELECT từ view Base — chỉ nguồn đọc là Base), `TSqlDrugs.cs:56` (INSERT local `XT_YP_BMYPXX`,
  SELECT từ view Base), `DispensingDmnService.cs:858` (SELECT thuần), các false positive tên method.
- **Quét lại sau đợt 1**: 20 điểm còn lại đều là false positive hoặc nguồn đọc — quét ghi dạng `..dbo` đã sạch.

### 2c. Quét bổ sung (đợt 2: dạng `..Table` 2 dấu chấm) — PHÁT HIỆN QUÉT ĐỢT 1 SÓT, ĐÃ SỬA

- **Bài học quét**: regex đợt 1 yêu cầu `..dbo.` nên MISS toàn bộ dạng `..Table` (schema mặc định).
  Quét lại với mẫu sửa (`[Newtouch_*]..` + `[Newtouch_*]..dbo.`) tìm thêm các đích ghi thật sau — tất cả đã sửa
  bằng `DbNames`, build 0 lỗi (trừ 1 lỗi `const` đã sửa, xem dưới):
  - EMR `MedicalRecordDmnService.cs:768` (UPDATE `Newtouch_EMR..bl_hljl`, EmrDb),
    `:792` (DELETE `Newtouch_EMR..bl_ysjgnr`, EmrDb).
  - EMR `MRHomePageDmnService.cs:1395,1422` (INSERT `[Newtouch_CIS]..zy_PatDxInfo`, CisDb — cùng service với
    DELETE CIS đã sửa ở đợt 1 nhưng dạng `..` nên sót).
  - OR `SysRepos.cs:53,59` (`.AS("[NewtouchHIS_Base]..Sys_GlobalConfig")` cho cả Insertable lẫn Queryable, BaseDb).
  - PDS `MzCfRepo.cs:721` (UPDATE `[Newtouch_CIS]..xt_cf`, CisDb; chuỗi này là `const string` → đổi thành `string`,
    lỗi CS0133 đã sửa).
  - PDS `SysUserExDmnService.cs:110` (UPDATE `[NewtouchHIS_Base]..Sys_UserLogOn` Locked=1, BaseDb),
    `:308` (UPDATE `[NewtouchHIS_Base]..sys_userlogon` reset mật khẩu, BaseDb).
  - CIS `OutpatientConsultDoctorRepo.cs:53` (UPDATE `NewtouchHIS_Base..xt_zs`, BaseDb).
  - CIS `DoctorserviceDmnService.cs:3175` (DELETE `NewtouchHIS_Sett..zy_xmjfb`, SettDb; nằm trong chuỗi verbatim
    nhiều dòng — nối bằng `@"..` mở lại).
  - CIS `MedicalRecordDmnService.cs:1731` (UPDATE `Newtouch_CIS..xt_dzcf` + `xt_dzcfmx`, CisDb).
  - CIS `OutpatientConsultDmnService.cs:346` (UPDATE `NewtouchHIS_Sett..mz_*` qua `UPDATE b ... from`, SettDb).
  - Center `SysRoleDmnService.cs:69,112` (DELETE `[Newtouch_Union]..Sys_RoleAuthorize`, UnionDb; thêm file helper
    `DbNames.cs` mới cho `NewtouchHIS.Base.DomainService`).
- **Bổ sung cấu hình UnionDb cho center** (center trước đó không có entry UnionDb nào):
  - `NewtouchHIS.AuthenticationCenter/appsettings.json`: thêm entry `UnionDb` (`Newtouch_Union`) index 4.
  - `NewtouchHIS.WebAPI.Manage/appsettings.json`: thêm entry `UnionDb` index 8 (DBConf của Manage có 8 entry:
    Base/Sett/Emr/Mrms/MrQc/Cis/Interface/Pds).
  - Launcher prod: `run-authcenter.sh` thêm `BusinessDB__DBConf__4__DBName=Newtouch_Union`,
    `run-apimanage.sh` thêm `BusinessDB__DBConf__8__DBName=Newtouch_Union` (hành vi prod không đổi).
  - Launcher staging: `run-authcenter-stg.sh` thêm `__4__=Newtouch_Union_STG`,
    `run-apimanage-stg.sh` thêm `__8__=Newtouch_Union_STG`.
- **Kiểm chứng đợt 2**: build 0 lỗi cho EMR Infra, OR Infra, PDS Infra, CIS Infra, Center Base.DomainService;
  triển khai prod cho EMR web (Debug) + API (Release), OR web (Debug) + API (Debug), PDS web (Release) + API
  (Release), CIS web (Debug) + API (Debug), AuthCenter + WebAPI.Manage (Release) — mỗi app kill đúng PID,
  login kiểm chứng (web) / GetAppToken + no-token 401 (center). Kiểm chứng ghi live từng đường đợt 2
  KHÔNG thực hiện riêng (các đường này cần luồng nghiệp vụ phức tạp: duyệt đơn thuốc, khóa bệnh án, xóa vai trò...);
  bằng chứng cho đợt 2 là: cùng helper `DbNames` đã chứng minh live ở 2a + build 0 lỗi + login sau triển khai
  không hồi quy. **Ghi rõ giới hạn này để giai đoạn sau bổ sung kiểm chứng ghi từng đường khi có fixture.**
- Tổng số file `DbNames.cs` mới: 8 (7 đợt 1 + 1 center đợt 2).

### 2d. Đường ĐỌC rò rỉ còn lại — GAP (không chặn chạy thật nhưng phải biết)

- Đã biết từ giai đoạn 6: Sett `[NewtouchHIS_Base]`, CIS `[NewtouchHIS_Sett]`, EMR `GetBllxListDetail`
  `[NewtouchHIS_Base]`, MRMS `DiagnosticSave` `[Newtouch_MRMS]`.
- Mới thấy trong quét đợt 1+2 (chỉ là đọc, 398 điểm dạng `..` tổng cộng, phần lớn là SELECT): CIS đọc Base/Sett/
  herp/PDS, EMR đọc CIS/Sett, PDS đọc Base/CIS (gồm `SysUserExDmnService` các SELECT view Base,
  `SysModuleDmnService:298` INSERT local + SELECT view Base, `TSqlDrugs:56`, `MrFeeRelDmnService:281,310`),
  v.v.; các điểm đã resolve kèm trong đợt 1/2 (ORApplyInfo SELECT, SysChargeItem SELECT trùng, TSqlStock EXISTS,
  Herp SELECT trong batch) thì không còn tính là gap. Sửa hết đọc rò rỉ là việc lớn, tương đương viết lại
  hàng trăm câu SELECT — để điều phối viên quyết phạm vi (xem §10).

### 2e. Triển khai prod mục 2 (tổng hợp)

- Đợt 1: MRMS web (Debug) + API (Release), EMR web (Release) + API (Release), OR web (Debug) + API (Debug),
  Herp web (Release) + API (Release), CIS web (Debug) + API (Debug), PDS web (Release) + API (Release) —
  mỗi app kill đúng PID → build từ main đúng cấu hình launcher → restart → login kiểm chứng + probe không-cookie
  vẫn 302 (mục 1 giữ nguyên sau triển khai).
- Đợt 2: EMR, OR, PDS, CIS (web+API như trên) + AuthCenter 8015 + WebAPI.Manage 8016 — cùng quy trình.
- Lưu ý PDS: lần restart PDS web đầu tiên trong đợt 1 bị lỗi biến `PPID` (readonly) nên process cũ chưa hề bị
  kill và launcher gõ sai tên (`run-5237.sh` không tồn tại; launcher đúng là `run-pds.sh`) — đã phát hiện bằng
  kiểm tra PID/cổng và làm lại đúng; PDS web cuối cùng đã chạy code mới (login success + no-cookie 302).
- Sett và Base/HOSP không có điểm ghi hardcode nào trong quét (ghi của Sett đi qua connection cấu hình) nên
  không triển khai gì thêm cho mục 2 ở 2 app này.

## 3. Trình tự thay tài khoản mẫu — ĐẠT TRÊN STG, DEMO LIVE GIỮ NGUYÊN

### 3.1 Nguyên nhân gốc lỗi tạo admin (tìm bằng log tạm trên worktree, KHÔNG sửa main để debug)

- Tạo tổ chức cấp cao nhất qua `OrganizeController.SubmitForm` (HOSP Web + Base Web) có nhánh
  `CreateDefaultAdminToOrg(entity.Id, Constants.LogonDefaultPassword)` trong `try/catch` với **catch rỗng**
  — mọi lỗi đều bị nuốt lặng, trả về success chung chung (không có hậu tố "đã khởi tạo thành công quản trị viên").
- Tái hiện trên HOSP-stg: tạo tổ chức `G7TOPORG` (đủ trường bắt buộc, trong đó `CategoryCode` là NOT NULL —
  lần thử đầu thiếu `CategoryCode` nên INSERT tổ chức fail trước cả nhánh tạo admin) → tổ chức tạo được
  (STG `Sys_Organize` 4→5), message success chung, `Sys_User` STG giữ 3.
- Gắn log tạm vào bản worktree (không commit) → exception thật:
  `System.ApplicationException: LoginUser_Default_Password is not configured` tại
  `Constants.get_LogonDefaultPassword()` (`Newtouch.HIS.Base.Infrastructure/Constants.cs:40`).
- Căn nguyên kép:
  1. `BusinessConfig` (`NewtouchHIS.Lib.Base/Model/Conf/BusinessConfig.cs`) **không có property
     `LoginUser_Default_Password`** (chỉ có trong appsettings.json, không bind vào object).
  2. `Newtouch.Tools.ConfigurationHelper.Configuration` (shim trong
     `Newtouch.HIS.Base.Infrastructure/Shims/ToolsShims.cs`) **không được gán ở đâu cả** trong toàn cây
     (grep không thấy phép gán nào; `Program.cs` các app không set nó) → `GetAppConfigValue` luôn rơi về
     fallback SysConfig → property không tồn tại → null → throw. Nghĩa là đường tạo admin tự động đã CHẾT
     ở mọi môi trường trong bản port, không riêng staging.
  - Lưu ý: các key khác dùng `GetAppConfigValue` (vd `AppId`) vẫn sống nhờ fallback SysConfig vì chúng CÓ
    property trong `BusinessConfig`; chỉ riêng `LoginUser_Default_Password` là key không có property.
  - Không có unique index trên `Sys_User.Account` (chỉ PK `Id`) nên trùng account `admin` không phải nguyên nhân.

### 3.2 Sửa (main, đã triển khai prod)

- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web/Program.cs` và
  `Newtouch.HIS.Base/Newtouch.HIS.Base.Web/Program.cs`: thêm 1 dòng sau `builder.Build()`:
  `Newtouch.Tools.ConfigurationHelper.Configuration = builder.Configuration;`
  (sửa tối thiểu, không đụng Lib chung; chỉ 2 app này gọi đường tạo admin).
  Worktree staging được chèn surgical tương tự (giữ nguyên literal cổng 15233).
- Kiểm chứng staging sau sửa (HOSP-stg): tạo tổ chức `G7TOPORG3` → message
  "Thao tác thành công<br/>và đã khởi tạo thành công quản trị viên hệ thốngadmin", không còn exception trong
  log tạm, `Sys_User` STG 3→4 (admin mới Id `a9a39a43-...`, TopOrganizeId = tổ chức mới).
- Triển khai prod HOSP 5233 + Base.Web 5238 (build Release từ main, kill đúng PID, login 000000 success,
  probe không-cookie vẫn 302). **Không tạo tổ chức cấp cao trên prod để kiểm chứng** (vì sẽ ghi DB demo);
  đường tạo admin trên prod sẽ được kiểm chứng thật khi bệnh viện tạo tổ chức của mình ở giai đoạn cắt thật
  (xem quy trình §3.4).

### 3.3 Chứng minh thay tài khoản trên STG (đầy đủ chuỗi)

- Admin mới tạo qua web có Account `admin` (trùng demo) → đã đổi tên trên STG thành `qtvmoi`
  (UPDATE `Sys_User` STG, Id admin mới) để tránh nhập nhằng khi login.
- Thử login `qtvmoi` lần 1 → lỗi "Không tìm thấy thông tin người dùng": vì TopOrganizeId của admin mới
  (tổ chức mới) khác Top_OrganizeId mà app cấu hình (tổ chức demo) — đúng cơ chế CheckPwd đã biết
  (TopOrganizeId phải khớp, nếu không CheckPwd trả "user not found").
  → Chuyển `qtvmoi` về TopOrganizeId tổ chức demo trên STG → thử lần 2 → lỗi mới:
  "Đăng nhập thất bại, chưa liên kết nhân viên hệ thống".
  → Tạo `Sys_Staff` STG (`G7QTV-STAFF`, gh `QTV001`, tên "Quan tri vien moi", KS01, tổ chức demo) +
  link `Sys_UserStaff` (`G7QTV-LINK`) → thử lần 3 → **login thành công**.
- Chức năng admin với `qtvmoi` trên HOSP-stg: mở `Sys/Organize/Form` → 200 HTML; tạo tham số
  `SysGlobalConfig/SubmitForm` (Code `G7QTV`) → success; STG `Sys_GlobalConfig` có dòng `G7QTV`.
  (Bài học quy trình: admin mới đầy đủ chức năng cần 3 điều kiện: đúng TopOrganizeId + mật khẩu đúng
  + link nhân viên hệ thống — quy trình cắt thật §3.4 phải gồm cả 3.)
- Grep launcher/watchdog/recovery: **không script/job nào phụ thuộc tài khoản demo** (grep `000000|xzadmin`
  trong `recovery.sh` và `run-*.sh` = 0 kết quả; chúng chỉ kiểm tra cổng, không login).
- Vô hiệu demo TRÊN STG: `UPDATE Sys_User SET zt='0'` cho 3 tài khoản demo STG (theo đúng Id) →
  login `000000` STG → lỗi "Không tìm thấy thông tin người dùng"; login `qtvmoi` STG → vẫn success.
- **Khôi phục demo STG ngay sau kiểm chứng** (`zt='1'` lại cho 3 tài khoản; login `000000` STG success trở lại).
  Lý do khôi phục (quyết định có chủ đích, ghi rõ): staging còn cần cho giai đoạn 8–10; bằng chứng vô hiệu
  (rejection đã quan sát được) vẫn đứng vững; trạng thái vô hiệu thật chỉ áp dụng ở production theo §3.4.
- Dọn dẹp staging sau mục 3: file log tạm `/tmp/g7-swallow.txt` đã dùng xong; worktree
  `OrganizeController.cs` đã revert về trạng thái commit (chỉ còn sửa `Program.cs` mirror main).
  Dữ liệu STG còn lại sau mục 3 (không ảnh hưởng demo): 3 tổ chức `G7TOPORG*`, user `qtvmoi` + staff/link,
  tham số `G7QTV`, `G7WITHLOGIN`, `G7TOPORG*` — đều mang prefix G7, tách biệt dữ liệu demo.

### 3.4 Quy trình cắt thật cho production (tài liệu — CHƯA thực hiện, chờ duyệt thời điểm)

1. Tạo tổ chức cấp cao của bệnh viện qua HOSP Web (đăng nhập bằng tài khoản demo còn hiệu lực) → hệ thống
   tự tạo admin `admin` cho tổ chức đó (đường này nay đã sửa, xem §3.2). Đổi tên account admin mới thành tên
   riêng của bệnh viện (tránh trùng `admin` demo) bằng UPDATE có backup.
2. Gán admin mới về đúng TopOrganizeId mà các app production cấu hình (nếu tổ chức mới chính là top org
   production thì cập nhật `Top_OrganizeId` trong BusinessConfig các app + launcher trước khi login;
   nếu không, chuyển TopOrganizeId của admin về top org production như đã làm trên STG).
3. Tạo `Sys_Staff` + link `Sys_UserStaff` cho admin mới (bắt buộc, nếu không login fail "chưa liên kết nhân viên").
4. Login bằng admin mới trên SSO + từng web module; kiểm chứng mở trang quản trị + tạo/sửa 1 tham số.
5. Xác nhận không job/script nào phụ thuộc demo (đã grep ở staging; lặp lại grep này ngay trước cắt thật).
6. Vô hiệu demo trên DB production: `UPDATE Sys_User SET zt='0' WHERE Account IN ('000000','xzadmin','admin')`
   (ghi rõ Id từng dòng trước khi update; backup bảng `Sys_User`/`Sys_UserLogOn` trước).
7. Kiểm chứng: admin mới vẫn login được; 3 tài khoản demo bị từ chối.
8. **Rollback**: nếu admin mới lỗi sau cắt, mở lại demo bằng `UPDATE Sys_User SET zt='1'` cho 3 Id đã backup
   (chỉ dùng làm cầu tạm để sửa admin mới, sau đó cắt lại).
9. **Break-glass**: KHÔNG tái dùng tài khoản demo làm break-glass thường xuyên. Break-glass chính thức là
   admin mới của bệnh viện + quy trình mở khóa thủ công (UPDATE `Sys_UserLogOn.Locked=0`, xem §9).
   Lưu ý riêng: tài khoản `root` theo thời gian (xem §9.3) KHÔNG được dùng làm break-glass — phải xóa trước
   khi chạy thật (P0).

## 4. Đổi LoginUser_Default_Password — ĐẠT (giá trị mới KHÔNG in ở đây)

- **Đính chính vị trí**: brief ghi file `Constants.cs`, nhưng `Constants.cs` chỉ là accessor đọc key
  `LoginUser_Default_Password`; giá trị mặc định thực tế nằm trong 3 file `appsettings.json`
  (HOSP Web, Base Web, HOSP API) — trước đây đều là giá trị mặc định gốc của bản ship.
  Đã đổi giá trị trong cả 3 file bằng generator ngẫu nhiên (16 ký tự chữ+số, sinh bằng `secrets`, ghi trực tiếp
  vào file, không in ra bất kỳ đâu — kể cả báo cáo này và bản tóm tắt).
- Không có launcher nào override key này (grep `LoginUser_Default` trong launcher = 0) nên giá trị appsettings
  là giá trị live cho đường tạo admin/reset mật khẩu (các nơi dùng `Constants.LogonDefaultPassword`:
  2 `OrganizeController` + `SysStaffDmnService.cs:131` reset mật khẩu nhân viên).
- Kiểm chứng: sau rebuild + restart HOSP 5233 + Base.Web 5238 (Release, kill đúng PID), login `000000`
  vẫn success trên cả 2 app (mật khẩu demo là hash lưu sẵn, không dùng giá trị mặc định này — đúng như dự kiến).
  HOSP API (5333) không có process prod riêng trong danh sách 26 cổng nên không restart riêng; file config của nó
  đã đổi cùng đợt và sẽ có hiệu lực ở lần build/restart tiếp theo.
- **Cơ chế "bắt đổi mật khẩu lần đầu login": KHÔNG TỒN TẠI trong codebase** (grep `FirstLogin|MustChangePassword|
  IsFirstLogin|PwdChange` trong Base/SSO = 0; `Sys_User`/`Sys_UserLogOn` không có flag nào cho việc này;
  `Sys_UserLogOn` chỉ có `Locked` và `UserPwdPlaintext`). **Không tự chế cơ chế phức tạp trong giai đoạn này.**
  Gap + vị trí móc vào cho giai đoạn sau: nhánh login success của `SysUserVDmnService.UserLogin` /
  `BaseAPI CheckPwd` + thêm 1 cột flag (vd `MustChangePwd`) vào `Sys_UserLogOn` + màn hình đổi mật khẩu bắt buộc
  sau login đầu (kiểm tra flag trong `FrameworkAuthorizationFilter` trước khi cho vào trang nghiệp vụ).
- Lưu ý: giá trị mặc định mới vẫn nằm trong git (theo đúng thiết kế "default" của hệ thống gốc — nó là
  mật khẩu khởi tạo cho user mới, không phải secret vận hành). Bệnh viện nên đổi lại lần nữa khi bàn giao
  (quy trình cắt thật §3.4 không phụ thuộc giá trị này sau khi admin mới đã có mật khẩu riêng).

## 5. Sửa cờ cookie trong HttpWebHelper.WriteCookie — ĐẠT

- File: `Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Utilities/HttpWebHelper.cs`.
  - Trước sửa: cả 2 overload đều tạo `CookieOptions options` nhưng overload 2 tham số KHÔNG DÙNG nó
    (`Append(strName, strValue)` không options → không HttpOnly/SameSite/Secure); overload 3 tham số chỉ set `Expires`.
  - Sau sửa: cả 2 overload đều set `HttpOnly=true`, `SameSite=Lax`, `Secure = HttpCurrent.Request.IsHttps`
    (`HttpCurrent` là `HttpContext` nên `Request.IsHttps` có sẵn). **Secure=true CHỈ khi HTTPS** — hiện tại chạy
    HTTP nên Secure=false; set `Secure=true` mù trên HTTP sẽ làm hỏng login (cookie không gửi qua HTTP).
  - Quirk giữ nguyên: overload 2 tham số chỉ ghi khi cookie chưa tồn tại (nhánh else rỗng). Hệ quả đã kiểm chứng
    bằng lập luận + hành vi: cookie cũ (phát hành trước sửa, không có cờ) vẫn được gửi kèm request và server vẫn
    chấp nhận (validation chỉ đọc giá trị, cờ chỉ là thuộc tính response); cờ mới chỉ xuất hiện khi login mới
    (cookie chưa tồn tại) hoặc sau logout/login lại. Session Redis vẫn hiệu lực qua các lần restart.
- **Quy trình DLL chung (rủi ro cao, đã tuân thủ)**:
  - Build Lib từ source (`NewtouchHIS.Lib.Base.csproj`, Release, 0 lỗi) → DLL mới.
  - Backup DLL cũ: mỗi app bin giữ bản `.bak-g7` (gitignored, còn trên disk để rollback); bản backup trong
    `his-dll-common/Net6` đã xóa khỏi git status sau khi xác nhận rollback vẫn khả thi qua git history +
    các bản `.bak-g7` ở bin (tránh làm bẩn commit).
  - Refresh `his-dll-common/Net6/NewtouchHIS.Lib.Base.dll` (file này TRACKED trong git — coordinator sẽ thấy
    diff nhị phân khi commit; đây là quy trình đã dùng ở giai đoạn cắt 2026-10-27).
  - **KHÔNG sửa nội dung run copy** (`lmsoft_openhis_run`): các app lấy DLL mới bằng cách copy DLL tươi vào
    chính bin của app đó (không qua HintPath trỏ run copy), restart từng app MỘT LÚC MỘT, login kiểm chứng
    sau mỗi app. Script dùng: `/tmp/roll-lib.sh` (đã tạo lại sau khi `/tmp` bị xóa): backup bin DLL → kill đúng PID
    → copy DLL tươi → restart bằng launcher → chờ cổng → login check.
- Kiểm chứng cờ bằng `curl -D` + kiểm tra boolean trên header (không in giá trị cookie; harness cũng tự che
  giá trị Set-Cookie):
  - SSO 5208 fresh login: 2 Set-Cookie (`His.SSO_newtouch_loginuserkey_2016`,
    `His.SSO_CookieKey_CurUserCode`): `httponly=True samesite_lax=True secure=False path=True` (cả 2).
    Cookie jar của curl cũng đánh dấu `#HttpOnly_` cho cả 2. Truy cập trang bảo vệ bằng cookie mới → 200.
  - HOSP 5233 fresh login: 2 Set-Cookie tương tự (`newtouch.base.web_*`): HttpOnly + SameSite=Lax, Secure=False.
  - Login success sau roll cho từng app: BaseAPI 7001 (qua SSO login), SSO 5208, MRQC 5220, Sett 5221,
    OR 5231, MRMS 5232, HOSP 5233, CIS 5234, EMR 5235, Herp 5236, PDS 5237, Base.Web 5238 — đủ 11 web + SSO + BaseAPI.
  - Các API/center: DLL tươi đã copy lên disk của các bin còn lại (process đang chạy giữ image cũ trong bộ nhớ,
    lần restart tiếp theo sẽ nhận DLL mới). API/center không dùng cookie login nên không ảnh hưởng hành vi hiện tại.
  - Lưu ý cấu hình: EMR web bin đang là Debug, OR/MRMS/CIS web bin Debug, còn lại Release — giữ nguyên cấu hình
    launcher hiện hữu (chuẩn hóa ở giai đoạn đóng gói).
- Rollback nếu hỏng: copy file `.bak-g7` trong bin của app đó đè lên DLL mới → restart app đó (đã chuẩn bị sẵn,
  không cần dùng tới trong giai đoạn này).

## 6. Scrub JWT khỏi log center — ĐẠT (gồm 1 phát hiện thêm ngoài brief)

### 6.1 Hai nguồn log JWT (không chỉ một)

- **Nguồn 1 — `JwtService.cs:200`** (`NewtouchHIS.AuthenticationCenter/Services/JwtService.cs`):
  `Console.WriteLine(... token:{token})` ghi full JWT. Đã sửa thành chỉ ghi `token_present` (có/không) +
  `token_prefix` (8 ký tự đầu). Cùng file, dòng log mã phiên (`:69`, "Phiên toàn cục code") cũng đã rút gọn
  thành `code_prefix` (8 ký tự đầu) vì mã phiên cũng có tính chất credential.
- **Nguồn 2 — `LogTrackingAttribute`** (`Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Filter/LogTrackingAttribute.cs`,
  Lib chung): filter này log toàn bộ `[Request.Data]` và `[Response.Result]` (khi
  `EnableLoggingMonitorAttr=true` — AuthCenter và WebAPI.Manage đều bật `true` trong appsettings, BaseAPI để
  `false`). Response của GetAppToken chứa full JWT nên log vẫn còn JWT đầy đủ NGAY CẢ SAU KHI sửa nguồn 1
  (đã chứng minh trên staging: sau sửa JwtService, log mới vẫn có full JWT qua `[Response.Result]`).
  Đã sửa bằng helper `Scrub` trong cùng file, áp dụng cho mọi payload log (headers, request ×3, result, exception):
  - JWT dạng `eyJ.xxx.yyy` → `eyJ<jwt-redacted>`;
  - trường `"Token": "..."` → `"Token":"<redacted>"` (kể cả khi không đúng dạng JWT);
  - trường mật khẩu dạng hash trong request login (`*password*`, không phân biệt hoa thường) → `"<redacted>"`
    (phát hiện thêm cùng logger: request login chứa md5 mật khẩu; đã scrub luôn và ghi rõ ở đây).
  - Header `Authorization: Bearer <jwt>` được cover bởi rule JWT.
  - Build Lib Release 0 lỗi; DLL tươi đã lăn theo quy trình mục 5 (bao gồm cả refresh vào worktree
    `his-dll-common/Net6` để build staging AuthCenter).
- WebAPI.Manage không có điểm log token riêng (LoginController của nó không dùng JwtService; GetToken đã bị
  comment); nhưng vì nó bật `EnableLoggingMonitorAttr=true` và dùng chung `LogTrackingAttribute`, nó cũng hưởng
  sửa scrub sau khi nhận DLL mới (đã restart 8016 với DLL tươi, kiểm chứng no-token 401 giữ nguyên).

### 6.2 Kiểm chứng

- Staging (AuthCenter-stg 18015, worktree build): `POST api/Auth/GetAppToken {AppId MRQC, Data API.Manage}` →
  200, code 10000, JWT cấp thành công (độ dài token kiểm chứng được nhưng không ghi ra đây).
  - Trước sửa LogTracking: log mới chứa full JWT trong `[Response.Result]`.
  - Sau sửa: log mới chỉ còn `"Token":"<redacted>"`; kiểm tra bằng code: full token KHÔNG xuất hiện trong log mới.
  - Marker `token_present` không xuất hiện trong luồng GetAppToken (dòng WriteLine ở JwtService:200 thuộc nhánh
    cấp refresh token, không phải nhánh WebApi này) — không phải lỗi; scrub response-log mới là bằng chứng chính.
- Prod: AuthCenter 8015 restart với DLL tươi → GetAppToken prod → 10000; token mới cấp KHÔNG xuất hiện trong
  `authcenter.log`; marker `"Token":"<redacted>"` có mặt. WebAPI.Manage 8016 restart DLL tươi → no-token 401 giữ nguyên.
- Về "xoay token đã từng bị log": JWT đã cấp đều ngắn hạn (theo thiết kế: token app-to-app có TTL theo cấu hình
  JWT; mã phiên/refresh có TTL riêng); đây là môi trường dev/staging, không có token production nào.
  Quy trình cho production: token cũ hết hạn tự nhiên theo TTL; nếu production từng log token thì phải coi như lộ
  và thu hồi (xoay signing key + cấp lại token cho các app đã đăng ký). **Log cũ (trước sửa) vẫn còn chứa JWT
  đầy đủ trong các file log hiện hữu — phải purge/rotate các file log này trước khi bàn giao production**
  (xem §6.3).

### 6.3 Retention log 30 ngày + quyền đọc log — CHƯA CÓ CƠ CHẾ (việc của giai đoạn đóng gói)

- Hiện trạng đo được: không có logrotate cho OpenHIS (`/etc/logrotate.d/` chỉ có alternatives/apt/dpkg/rsyslog),
  không có cron dọn log. Log là file thường: `~/workspace/*.log`, `~/workspace/openhis-core/*.log`,
  log theo từng project (vd `Newtouch.HIS.Sett/sett.log`); quyền hiện tại `-rw-rw----` (660, owner root,
  group nogroup) — tức owner + group đọc được, other không.
- Chưa cấu hình retention 30 ngày trong giai đoạn này theo đúng brief ("nếu chưa có thì ghi rõ là việc của
  giai đoạn đóng gói, kèm đường dẫn thư mục log hiện tại + quyền hiện tại"). Giai đoạn đóng gói cần:
  logrotate (giữ 30 ngày) cho các đường dẫn trên + siết group đọc log + purge các file log tiền-sửa
  (còn JWT đầy đủ, xem §6.2).

## 7. Rà soát secret theo tên khóa — ĐẠT (đã chuyển sang env, KHÔNG xoay giá trị)

### 7.1 Phạm vi quét

- Grep toàn cây code + config theo `AppSecret`, `LoginUser_Default_Password`, `BusinessDB*`
  (KHÔNG quét `ConnectionStrings` — 0 file dùng khóa đó, theo thiết kế).
- `LoginUser_Default_Password`: xem mục 4 (đã đổi giá trị trong 3 appsettings; accessor ở `Constants.cs`).
- `BusinessDB*`: `DefaultConn` trong appsettings là template `Data Source=localhost;Initial Catalog={0};User ID=sa;...`
  — giá trị này thuộc quyết định GIỮ của user (xem §7.3) và launcher đã override bằng env nên không đụng vào.
  Các entry `DBConf` chỉ chứa tên DB (không phải secret).

### 7.2 AppSecret + JWT signing key — đã chuyển sang env (giá trị giữ nguyên, không xoay)

- Phát hiện (file nào, khóa nào, có phải secret thật không):
  - `NewtouchHIS.AuthenticationCenter/appsettings.json`: `JWT.Key` (signing key, 64 ký tự hex) + mảng
    `JWT.SSOAuthApp` 8 entry, mỗi entry có `AppSecret` (thứ tự: API.Manage, sett.web, base.web,
    NewtouchHIS.SelfTerminal, MRQC, Oh_Newtouch_Clinic, EMR.web, HIS.BaseAPI). Đây là secret THẬT và LIVE:
    `HSJWTService`/`JwtService` đọc `_jWTOptions.SSOAuthApp` để ký/validate token app-to-app.
  - `NewtouchHIS.WebAPI.Manage/appsettings.json`: `SSOAuthIdentity[0].AppSecret` (entry API.Manage, trùng giá trị
    với entry API.Manage trong AuthCenter — đã kiểm chứng bằng so sánh, không in giá trị).
  - `HIS.BaseAPI/appsettings.json`: `SSOAuthIdentity[0].AppSecret` (entry HIS.BaseAPI, trùng với entry HIS.BaseAPI
    trong AuthCenter).
  - Code chỉ đọc các key này (HSJWTService, RSJWTService, ServiceExtensions, JWTTokenRequest.AppSecret) — không
    hardcode giá trị trong code.
- Xử lý: tạo `~/workspace/.appsecrets.env` (600) chứa `AUTHCENTER_JWT_KEY`, `AUTHCENTER_APPSECRET_0..7`,
  `MANAGE_APPSECRET`, `BASEAPI_APPSECRET` (cùng giá trị cũ, không xoay); sửa launcher để source file này và
  override env: AuthCenter `JWT__Key` + `JWT__SSOAuthApp__i__AppSecret` (i=0..7, đúng thứ tự mảng),
  Manage/BaseAPI `SSOAuthIdentity__0__AppSecret`. Launcher đã sửa: `run-authcenter.sh`, `run-apimanage.sh`,
  `run-his-baseapi.sh` (prod) + `run-authcenter-stg.sh`, `run-baseapi-stg.sh`, `run-apimanage-stg.sh`
  (staging chạy binary main — nếu không thêm override, staging center/BaseAPI sẽ nhận AppSecret rỗng sau khi
  scrub appsettings).
- Scrub appsettings nguồn: `JWT.Key` → `""`, mọi `AppSecret` trong 3 file trên → `""`.
  (Bin copy trong `bin/` không sửa — là build output; env override thắng appsettings nên runtime không bị ảnh hưởng.)
- Kiểm chứng: restart AuthCenter 8015 + WebAPI.Manage 8016 + BaseAPI 7001 bằng launcher mới →
  GetAppToken prod (`{AppId MRQC, Data API.Manage}`) → 10000 (chứng minh SSOAuthApp override đúng index);
  8016 no-token → 401 giữ nguyên; SSO login 000000 success (qua BaseAPI).
- **KHÔNG xoay giá trị AppSecret/JWT.Key trong giai đoạn này** (quyết định có chủ đích): xoay sẽ làm vô hiệu
  token đã cấp và cấu hình app đã đăng ký; giá trị cũ vẫn còn trong git history nên xoay thật sự cần quy trình
  riêng (cấp lại secret cho từng AppId + thu hồi token cũ). Ghi vào mục chờ quyết định ở §10.

### 7.3 Ngoại lệ bắt buộc — mật khẩu SA cũ trong HIS.BaseAPI/appsettings.json: GIỮ NGUYÊN

- Theo lệnh user ngày 2026-10-27: **KHÔNG đụng vào file đó, KHÔNG in giá trị đó, chỉ ghi "giữ nguyên theo
  quyết định user"**. "Giữ" nghĩa là không viết lại lịch sử git, không có nghĩa giá trị đó an toàn:
  giá trị đó đã cũ (chạy trần không login được), launcher override qua env mới login được.
- Mọi service mới trong giai đoạn này lấy secret từ env, không hardcode (xem §7.2, §8).

## 8. Redis: mật khẩu qua env + requirepass — ĐẠT (đã chuyển sang env, KHÔNG đổi mật khẩu server)

### 8.1 Hiện trạng đo được (trước sửa)

- Redis prod 6379: **có requirepass**. Kiểm chứng bằng probe RESP trực tiếp (không qua redis-cli):
  AUTH bằng mật khẩu live → `+OK`; AUTH sai → `WRONGPASS`; không AUTH → `NOAUTH`.
  (Trước đó `redis-cli` không có trong PATH; đã dùng `~/workspace/redis/usr/bin/redis-cli` và probe RESP bằng Python.)
- **Phát hiện quan trọng (đã giải quyết, ghi lại để khỏi tái diễn)**: quét appsettings nguồn thấy 3 mật khẩu
  Redis "khác nhau" (độ dài 8 / 26 / 15). Phân tích:
  - Giá trị độ dài 26 (AuthCenter, WebAPI.Manage, BaseAPI, SSO) thực chất là **mật khẩu live + hậu tố sau dấu
    phẩy**: trong connection string StackExchange.Redis, dấu phẩy phân tách option — đoạn đầu (trước phẩy)
    CHÍNH LÀ mật khẩu live, đoạn sau là option `syncTimeout=...` hợp lệ. Nên 4 app này vẫn AUTH thành công.
    Đã kiểm chứng: đoạn đầu AUTH → `+OK`.
  - Giá trị độ dài 15 (Herp Web/API) là mật khẩu khác, nhưng Herp launcher đã override
    `RedisConfig__Connection` bằng biến `$REDIS_PASSWORD` từ `~/workspace/.redis.env` nên runtime Herp dùng đúng
    mật khẩu live (appsettings Herp chỉ là giá trị chết khi chạy bằng launcher).
  - Giá trị độ dài 8 (11 file web + HOSP API) là mật khẩu live, khớp `requirepass` trong `recovery.sh`.
  - Kết luận: **chỉ có 1 mật khẩu Redis live duy nhất**; các "khác nhau" đều là hậu tố option hoặc giá trị chết
    bị launcher override. File `~/workspace/.redis.env` (600, từ 2026-10-26) đã chứa sẵn đúng mật khẩu này
    dưới tên `REDIS_PASSWORD` — đã tái dùng, không tạo file mới, không đổi mật khẩu server.
- Launcher prod trước sửa: chỉ Herp + staging override `RedisConfig__Connection`; các launcher prod còn lại
  KHÔNG override → app đọc mật khẩu từ appsettings (committed). Đó là thiếu sót mà mục 8 yêu cầu bổ sung.

### 8.2 Đã làm

- Sửa 24 launcher (21 prod trong recovery + 3 staging chạy binary main): thêm
  `source ~/workspace/.redis.env` (sau dòng source `.sqlserver.env`) và override
  `RedisConfig__Connection="127.0.0.1:6379,password=$REDIS_PASSWORD"` vào danh sách env của `exec env`
  (Herp đã có sẵn nên không sửa lại). Tất cả 24 file đều qua `bash -n` syntax OK.
  (Launcher staging còn lại đã có override Redis staging riêng với mật khẩu staging trong
  `~/workspace/.redis-stg.env` — không lẫn với prod, không sửa.)
- Scrub appsettings nguồn: 17 file `appsettings.json` (trừ `bin/`, scratch `herp-*/sett-stage-*`, worktree):
  `RedisConfig.Connection` bỏ đoạn `password=...` (giữ lại hậu tố option như `syncTimeout` ở các file center).
  Sau scrub, grep `password=` trong appsettings nguồn chỉ còn template `DefaultConn` của BusinessDB
  (thuộc quyết định GIỮ ở §7.3).
- **KHÔNG restart Redis server, KHÔNG đổi requirepass** (mật khẩu giữ nguyên nên không có rủi ro mất đồng bộ).
  Chỉ restart từng app để nhận env mới, theo đúng thứ tự: BaseAPI 7001 → SSO 5208 → 10 web còn lại →
  AuthCenter 8015 → WebAPI.Manage 8016 → 9 API (5331–5337, 5320, 7002). Mỗi app: kill đúng PID → launcher mới →
  chờ cổng → kiểm chứng (web: login 000000 success; center: GetAppToken 10000 + no-token 401; API: cổng UP).
  Kết quả: tất cả đều UP và kiểm chứng đạt (xem §0). Lưu ý: đợt restart API đầu tiên bị lỗi `~` trong quote
  (xem §0) — đã khắc phục và kiểm chứng lại đủ 26 cổng.
- Redis staging 6380: đã có requirepass riêng + launcher staging đã override từ trước — không thay đổi gì
  thêm, chỉ khởi động lại từ config khi cần kiểm chứng (lưu ý kỹ thuật: phải set
  `LD_LIBRARY_PATH=~/workspace/redis-libs/usr/lib/x86_64-linux-gnu` trước khi chạy `redis-server`, nếu không
  lỗi `liblzf.so.1` — đây là cách `recovery.sh` vẫn làm cho Redis prod).

## 9. Xác minh ngưỡng khóa tài khoản — KẾT LUẬN: LOCKOUT KHÔNG ĐƯỢC THỰC THI Ở LIVE PATH (gap, xem §9.2)

### 9.1 Đọc code: 3 cơ chế khóa, chỉ 1 có code khóa đúng — và không cái nào nằm trên live path

- **Live path thật sự**: web login → `Login/CheckLogin` → BaseAPI `CheckPwdAsync`
  (`HIS.BaseAPI/Controllers/LoginController.cs:31`) → `SysUserVDmnService.UserLogin`
  (`NewtouchHIS.Base.DomainService/SysUser/SysUserVDmnService.cs`).
  - Hàm này **chỉ kiểm tra** `Locked` có sẵn (nếu đã khóa thì trả message "nhập sai quá N lần, tài khoản đã bị khóa"),
    **không bao giờ đếm lỗi, không bao giờ set `Locked`, không ghi dòng log thất bại nào**.
    Nhánh sai mật khẩu chỉ trả `FAILOfPwdError` ("Mật khẩu không đúng, vui lòng nhập lại").
  - Đây là lời giải cho câu hỏi mở từ giai đoạn 2 (hàng chục lần GetToken sai mật khẩu mà 000000 vẫn login được):
    không có gì đếm cả, nên không bao giờ khóa.
  - Config `LoginFailedTimesLimit_Count=3` / `LoginFailedTimesLimit_Minutes=60` trong appsettings chỉ được dùng
    để **định dạng message khóa** trong hàm này (`string.Format(... sysConfig.LoginFailedTimesLimit_Count)`),
    không dùng để thực thi.
- **Cơ chế legacy 1 — `SysUserApp.CheckLogin`** (`Newtouch.HIS.Base.Infrastructure/.../SysUserApp.cs`):
  có logic đếm (đọc `Sys_Log` Type=Login trong N phút, lấy Count-1 dòng mới nhất; nếu đủ dòng và toàn thất bại
  thì khóa) nhưng **có bug: dòng khóa ghi `userLogOnEntity.Locked = false;`** trong khi comment ghi
  "Cập nhật trạng thái thành 'Khóa'" — tức là KHÔNG BAO GIỜ khóa được. Ngoài ra nhánh `username == "admin"`
  được miễn khóa hoàn toàn (luôn chỉ trả "mật khẩu không đúng"). **Và quan trọng: hàm này KHÔNG nằm trên
  live path** (BaseAPI CheckPwd không gọi nó) — nên bug này hiện không ảnh hưởng runtime, chỉ là code chết
  mang logic sai. Không sửa bug này trong giai đoạn 7 (sửa code chết không thay đổi hành vi live; ghi lại để
  giai đoạn sau quyết định xóa hay sửa khi dọn legacy).
- **Cơ chế legacy 2 — PDS `SysUserExDmnService.CheckLogin`** (`Newtouch.HIS.PDS.Infrastructure/...`):
  code khóa ĐÚNG (`update ... Sys_UserLogOn set Locked = 1`, `userEntity.Locked = true`) — và PDS Web
  LoginController có dùng service này. Tuy nhiên việc đếm phụ thuộc dòng `sys_log` Type=Login (truy vấn
  `select top {Count-1} Result from sys_log ...`) — chưa xác minh được ai ghi các dòng thất bại này cho PDS,
  nên chưa kết luận PDS có khóa thật hay không. Ghi là **chưa kiểm chứng live** (xem §10).
  (Trong đợt 2 mục 2, 2 câu UPDATE Base của service này đã được chuyển sang `DbNames` — xem §2c.)
- **Unlock**: cả 3 cơ chế đều ghi chú "Không cung cấp Tự động mở khóa" — mở khóa chỉ bằng can thiệp thủ công
  (UPDATE `Sys_UserLogOn.Locked=0` hoặc màn hình quản trị nếu có; chưa xác minh có màn hình mở khóa hay không).

### 9.2 Kiểm chứng TRÊN STG (tài khoản chỉ có trên STG, KHÔNG đụng demo 000000)

- Tài khoản thử nghiệm: `qtvmoi` (tạo ở mục 3, chỉ có trên STG, đã có staff link, mật khẩu là giá trị mặc định
  cũ của worktree staging — không ghi ra đây).
- Web login qua HOSP-stg 15233 (→ BaseAPI-stg 17001 → `UserLogin` thật):
  5 lần sai mật khẩu liên tiếp → cả 5 lần đều trả "Mật khẩu không đúng, vui lòng nhập lại", **không lần nào
  báo khóa**; ngay sau đó login đúng mật khẩu → **success**. `Sys_UserLogOn.Locked` của `qtvmoi` vẫn NULL.
  → **Khẳng định: sau 5 lỗi liên tiếp (vượt ngưỡng 3), tài khoản KHÔNG bị khóa; không có thời gian khóa
  nào được áp dụng; mở khóa không cần thiết vì chưa từng khóa.**
- Center `UserLogin` trực tiếp qua AuthCenter-stg 18015 (`POST api/Login/UserLogin`): cả lần sai và lần đúng
  đều trả lỗi cấu hình `[Ngoại lệ]:ConfigId was not found 1` — **không kiểm chứng được qua endpoint này trên
  staging** (lỗi cấu hình riêng của center staging, không liên quan mật khẩu; `LoginBO` chỉ có Username/Password
  nên request đã đúng shape). Tuy nhiên hàm `UserLogin` được gọi ở đây là **cùng hàm** với web login qua BaseAPI
  (đã chứng minh không khóa ở trên), nên kết luận về center cũng đứng vững ở mức code + web-path.
  Lỗi `ConfigId` này ghi vào §10 để giai đoạn sau điều tra (không thuộc phạm vi sửa của giai đoạn 7).
- **Quyết định**: KHÔNG implement lockout mới trong giai đoạn 7. Lý do: cơ chế "có sẵn" duy nhất (legacy 1)
  vốn đã sai (ghi `Locked=false`) và không nằm trên live path; áp dụng nó sang GetToken/center sẽ là nhân rộng
  code sai. Implement đúng cần: nhánh failure trong `SysUserVDmnService.UserLogin` phải ghi dòng log thất bại
  + đếm trong cửa sổ 60 phút + set `Locked=true` khi đủ 3 + quy trình mở khóa thủ công đã kiểm chứng + UI
  mở khóa cho admin — đó là một thay đổi hành vi bảo mật có rủi ro khóa nhầm tài khoản thật (không tự mở khóa),
  cần quyết định riêng của điều phối viên/user trước khi làm (xem §10).
  GetToken (module API) hiện là stub disabled nên không áp dụng lockout ở đó; GetAppToken (center) dùng
  AppSecret chứ không phải mật khẩu user nên ngoài phạm vi lockout tài khoản.

### 9.3 [P0 — NGOÀI 9 MỤC, PHÁT HIỆN TRONG LÚC ĐỌC CODE MỤC 9] Backdoor tài khoản `root`

- **Phát hiện**: `SysUserVDmnService.UserLogin` có nhánh đặc biệt chạy TRƯỚC cả tra cứu user:
  `if (login.Username == "root" && CheckRootPwd(login.Password))` → trả success với `Id="root"`, `Account="root"`
  mà **không cần tài khoản nào trong DB**. Hàm `CheckRootPwd` (`SysUserVDmnService.cs:193`) suy ra mật khẩu
  từ thời gian hiện tại của server (định dạng ngày-giờ 6 ký tự, băm md5) — tức là bất kỳ ai biết quy ước này
  cũng tự tính được mật khẩu "root" tại thời điểm hiện tại (không gian tìm kiếm chỉ vài nghìn khả năng trong
  ngày, và giá trị hiện tại tính được trực tiếp). Nhánh này **bypass hoàn toàn lockout** (chạy trước mọi kiểm tra
  `Locked`) và bypass mọi tài khoản DB.
- **Kiểm chứng trên STG (1 lần thử có chủ đích, không brute-force)**: login `root` qua HOSP-stg 15233
  (→ BaseAPI-stg) với mật khẩu suy ra từ giờ UTC hiện tại của server → **200 success "Đăng nhập thành công"**.
  (Lần thử với múi giờ khác fail "Không tìm thấy thông tin người dùng" — đúng như dự kiến vì server chạy giờ UTC.)
  Giá trị mật khẩu/hash KHÔNG ghi ở đây.
- **Đánh giá**: đây là backdoor mức P0, nghiêm trọng hơn mọi mục trong giai đoạn này. Nó tồn tại trên cả
  prod (cùng code `UserLogin` phục vụ BaseAPI CheckPwd prod).
- **KHÔNG tự xóa trong giai đoạn 7** (quyết định có chủ đích, cần duyệt riêng): nhánh này mang comment
  "Trường hợp đặc biệt" — có thể là cửa hỗ trợ khẩn cấp của vendor; xóa nó đồng nghĩa xóa break-glass duy nhất
  còn lại nếu admin mới (mục 3) gặp sự cố. **Đề nghị P0 cho điều phối viên/user trước khi chạy thật**:
  xóa nhánh `root` trong `SysUserVDmnService.UserLogin` + thay bằng quy trình break-glass chính thức
  (admin của bệnh viện từ mục 3 + mở khóa thủ công), và kiểm chứng lại rằng không còn đường login nào
  không qua DB. Cho tới khi xóa, mọi kiểm chứng "tài khoản demo đã vô hiệu" đều chưa đủ — vì `root` vẫn vào được.

## 10. Còn lại cho giai đoạn 8/9/10 (gap chưa xong — không báo đạt những mục này)

1. **Thời điểm cắt tài khoản demo thật** (mục 3): quy trình đã có ở §3.4; demo live GIỮ NGUYÊN sau giai đoạn 7
   theo quyết định điều phối viên (giai đoạn 8–10 vẫn cần login demo). Cần duyệt thời điểm cắt + người thực hiện.
2. **Xóa backdoor `root`** (P0, §9.3): cần quyết định break-glass thay thế trước khi xóa; sau xóa phải kiểm chứng
   login `root` bị từ chối trên staging rồi mới lên prod.
3. **Lockout thật cho `UserLogin`** (§9.2): implement đếm + khóa + mở khóa thủ công đã kiểm chứng; sửa/xóa code
   legacy `SysUserApp.CheckLogin` (bug `Locked=false`, miễn `admin`); xác minh PDS `SysUserExDmnService` có khóa
   thật không (phụ thuộc dòng `sys_log`); điều tra lỗi `ConfigId was not found 1` của center `UserLogin` trên staging.
4. **Secure cookie khi có TLS** (mục 5): code đã sẵn (`Secure = Request.IsHttps`) — khi giai đoạn 8 bật HTTPS
   (nginx/TLS), cookie sẽ tự có Secure; cần kiểm chứng lại header Set-Cookie trên HTTPS + kiểm tra SameSite=Lax
   có phá luồng SSO cross-site nào không (hiện tại cùng site nên an toàn).
5. **Retention log 30 ngày + purge log cũ** (§6.3): logrotate cho `~/workspace/*.log`,
   `~/workspace/openhis-core/*.log`, log từng project; siết group đọc; purge các file log tiền-sửa còn JWT đầy đủ
   trước bàn giao production.
6. **Xoay AppSecret/JWT.Key** (§7.2): giá trị đã chuyển sang env nhưng CHƯA xoay (vẫn còn trong git history);
   cần quy trình cấp lại secret cho từng AppId + thu hồi token cũ.
7. **Đọc rò rỉ còn lại** (§2d): hàng trăm SELECT/JOIN qua tên DB literal (chủ yếu là đọc); cần quyết phạm vi sửa
   (sửa hết ≈ viết lại hàng trăm câu SELECT) hoặc chấp nhận có tài liệu.
8. **Kiểm chứng ghi live từng đường đợt 2 mục 2** (§2c): các đường EMR/OR/PDS/CIS/Center đợt 2 mới chỉ
   compile + login-verified; cần fixture + kiểm chứng ghi STG từng đường (duyệt đơn thuốc PDS, khóa bệnh án EMR,
   xóa vai trò center, v.v.) trước khi coi là hoàn tất.
9. **Injection còn lại**: `RecordDmnService.updateRecordStu` nối `id` trực tiếp vào `IN (...)` (§2a);
   rà soát các điểm nối chuỗi SQL còn lại trong các file đã sửa (đợt 1 đã tham số hóa một phần: MrbasyRepo).
10. **Chuẩn hóa cấu hình build**: CIS web, OR web, MRMS web, EMR web prod đang chạy **Debug** (theo launcher hiện
    hữu, giữ nguyên trong giai đoạn này); EMR Debug→Release là việc của giai đoạn đóng gói (đã có trong chương
    trình từ trước).
11. **Cơ chế bắt đổi mật khẩu lần đầu login** (§4): chưa tồn tại — cần thiết kế (flag + màn hình bắt buộc đổi)
    nếu bệnh viện yêu cầu.
12. **Các gap khác từ trước, không thuộc giai đoạn 7**: DRG live test, BHYT QĐ 3176, DCSoft/ActiveX/SSRS thay thế,
    write-path live test toàn diện, dữ liệu thật của bệnh viện + UAT + rollout theo giai đoạn (theo chương trình
    chạy thật đã định).
13. **Dọn dẹp sau giai đoạn 7**: file `DbNames.cs` mới (8 file) + `run-base-web-stg.sh` + DLL mới trong
    `his-dll-common/Net6` đều nằm trong git status chờ commit (tổng 100 entries, xem §11); các file `.bak-g7`
    trong bin các app là gitignored, giữ trên disk để rollback; worktree `openhis-stg` có sửa staging-only
    (Program.cs, JwtService, LogTrackingAttribute, cổng literal) — worktree chỉ để staging, không commit.

## 11. Danh sách file thay đổi (main checkout, chưa commit — kiểm chứng bằng `git status --short`)

- **Mục 1**: 6 `ControllerBase` (HOSP Web, Base Web, CIS Web, Herp Web, PDS Web, Sett Web) + `run-base-web-stg.sh` (mới).
- **Mục 2**: MRMS (`MrbasyRepo.cs`, `RecordDmnService.cs`, `MainRecordDmnService.cs`), EMR (`BlmblbRepo.cs`,
  `HljlDataRepo.cs`, `MRHomePageDmnService.cs`, `MedicalRecordDmnService.cs`), Herp (`WzProductDmnService.cs`),
  OR (`ORApplyInfoRepo.cs`, `SysChargeItemDmnService.cs`, `Temporary_ordersRepo.cs`, `SysRepos.cs`),
  CIS Infra (11 file: DeptMedicineApplyNo, Doctorservice, OrderAudit, HospInterfaceSync, SettInterfaceSync,
  AllergyManage, OutpatientNurse, Report, OutpatientConsultDoctorRepo, MedicalRecord, OutpatientConsult),
  PDS Infra (11 file: DrugStorage, DispenseIndexInfo, ZyYpyzzxRepo, SysMedicineAntibioticType, SysMedicineBase,
  SysMedicine, SysPharmacyDepartmentBase, SysPharmacyWindow, MzCfRepo, SysUserExDmnService, TSqlStock),
  PDS API (`HospitalizationController.cs`), Center (`SysRoleDmnService.cs`),
  8 file `DbNames.cs` mới (CIS/EMR/Herp/OR/MRMS/PDS Infra, PDS API, Center DomainService),
  appsettings CIS Web (+OrDb) + PDS Web (+CisDb) + AuthCenter/Manage (+UnionDb),
  launcher staging `run-cis-stg.sh`, `run-pds-stg.sh` (+DBConf STG).
- **Mục 3**: `Program.cs` HOSP Web + Base Web (gán `ConfigurationHelper.Configuration`).
- **Mục 4**: 3 `appsettings.json` (HOSP Web, Base Web, HOSP API) — đổi `LoginUser_Default_Password`
  (giá trị mới không ghi ở đây).
- **Mục 5**: `NewtouchHIS.Lib.Base/Utilities/HttpWebHelper.cs` + DLL tươi trong `his-dll-common/Net6`
  (file tracked, diff nhị phân) — các bin app đã nhận DLL mới qua copy trực tiếp (không qua git).
- **Mục 6**: `JwtService.cs` (AuthCenter) + `NewtouchHIS.Lib.Base/Filter/LogTrackingAttribute.cs` (cùng DLL mục 5).
- **Mục 7+8**: 3 `appsettings.json` center/BaseAPI (AppSecret/JWT.Key → rỗng) + 17 `appsettings.json`
  (Redis Connection bỏ password) + 24 launcher (21 prod + 3 staging chạy binary main) thêm source
  `.redis.env`/`.appsecrets.env` + override `RedisConfig__Connection` (+ AppSecret/JWT overrides cho 3 host).
  File env mới (600, ngoài git): `~/workspace/.redis.env` (đã có từ 2026-10-26, tái dùng),
  `~/workspace/.appsecrets.env` (mới).
- **Không đụng**: checkout `~/workspace/lmsoft_openhis` (chỉ đọc), nội dung run copy
  `~/workspace/lmsoft_openhis_run`, file `HIS.BaseAPI/appsettings.json` (mật khẩu SA — GIỮ theo lệnh user),
  DB demo live (không ghi, không vô hiệu tài khoản demo).
- Tổng: 100 entries trong `git status --short` (đã trừ file `.bak-g7` trong `his-dll-common` sau khi xóa;
  các `.bak-g7` trong bin app là gitignored nên không hiện).
