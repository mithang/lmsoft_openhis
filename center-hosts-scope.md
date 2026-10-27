# Center hosts 8015 / 8016 — scoping report (investigation only, 2026-10-27)

## 1. What 8015 and 8016 ARE (original Framework tree, `~/workspace/lmsoft_openhis`)

Both live in ONE solution, `Newtouch.HIS.WebAPI.Manage/Newtouch.HIS.WebAPI.Manage.sln`
(9 projects), and — decisively — **both web hosts are ALREADY ASP.NET Core 6.0
(`net6.0`), not Framework**. There is nothing to "port" in the retargeting sense;
the remaining work is: copy/build/run + config fixes + Vietnamese translation of
messages (see §4–§5).

| Port (deployment) | Project | Path | Type | Serves |
|---|---|---|---|---|
| **8015** | `NewtouchHIS.AuthenticationCenter` | `Newtouch.HIS.WebAPI.Manage/NewtouchHIS.AuthenticationCenter/` | ASP.NET Core 6.0 API host, **0 views**, 2 controllers, 7 .cs files | JWT auth center: issues app/user tokens that modules present to 8016 |
| **8016** | `NewtouchHIS.WebAPI.Manage` | `Newtouch.HIS.WebAPI.Manage/NewtouchHIS.WebAPI.Manage/` | ASP.NET Core 6.0 API host, **0 views**, ~31 controllers, 65 .cs files, 151 `[Route]` hits | Cross-module API gateway/aggregation: CIS, EMR, MRQC, Sett, PDS, PACS, LIS, DRGS, PatientCenter, OrderCenter, System, ExtClinic areas + file upload + SignalR notice hub |

Evidence for the port mapping (deployment ports, NOT launchSettings):

- Module configs, original: `Newtouch.HIS.MRQC/Newtouch.MRQC.Web/Configs/system.config`
  (same in CIS/Sett/PDS/EMR configs):
  `<add key="SiteAuthCenterHost" value="http://localhost:8015/"/>`,
  `<add key="SiteApiManageHost" value="http://localhost:8016/"/>`.
  Ported apps carry the same values, e.g. `openhis-core/Newtouch.MRQC.Web/appsettings.json`
  (`SiteAuthCenterHost: http://127.0.0.1:8015/`, `SiteApiManageHost: http://127.0.0.1:8016/`).
- launchSettings ports are DIFFERENT (dev-only, ignored by DLL-direct launches):
  AuthCenter `http://localhost:5043` / `26701`
  (`.../AuthenticationCenter/Properties/launchSettings.json`),
  WebAPI.Manage `http://localhost:5212` / `17546`.
  So a run must bind `ASPNETCORE_URLS` to 8015/8016 explicitly, exactly as
  SSO (5208) / BaseAPI (7001) already do.
- Supporting projects in the same solution, all `net6.0`:
  `NewtouchHIS.Base.Domain` (123 .cs), `NewtouchHIS.Base.DomainService` (30),
  `NewtouchHIS.Base.Repository` (3), `NewtouchHIS.Domain` (80),
  `NewtouchHIS.DomainService` (25), `NewtouchHIS.Services` (5),
  plus `NewtouchHIS.Lib.Base` (in .sln; source present in solution).
- The solution is ALSO present in the run copy
  (`~/workspace/lmsoft_openhis_run/Newtouch.HIS.WebAPI.Manage/`, same 9 projects),
  and the run copy already has the `fix_connections.py` treatment:
  `DefaultConn: Data Source=localhost;...;User ID=sa;Password=<redacted>` in both hosts'
  appsettings. NOT yet fixed there: `Top_OrganizeId` is still the vendor demo org
  `d8ee85f8-d8d1-4b5c-bd9e-1761f44242d8` (our demo org is
  `6d5752a7-234a-403e-aa1c-df8b45d3469f`, cf. SSO/BaseAPI fix), and the
  AuthCenter `SSOAuthApp` entry for `HIS.BaseAPI` still points at
  `http://127.0.0.1:8017` (our BaseAPI is on 7001).

### 8015 AuthCenter — endpoints (all JWT, `api/Auth/*`, `api/Login/*`)

`.../AuthenticationCenter/Controllers/AuthController.cs`:
- `POST api/Auth/GetAppToken` — app-to-app token. Body `{AppId, Data}` where
  `Data` is the target app id (ported modules send `Data = "API.Manage"`).
  It calls `AppManageDmnService.AppKeyGen({FriendAppId: AppId, AppId: Data})`,
  which looks up `Sys_Auth_App` (zt=1) + the `Sys_Auth_AppFriend` link, then
  mints a JWT. Unknown/unlinked app → `FAIL` ("应用未注册，请联系管理员").
  This is REAL registration-based validation — unlike the module APIs'
  `GETTOKEN_DISABLED` stubs, which mint nothing.
- `POST api/Auth/GetAppFrienAuthToken`, `POST api/Auth/UserTokenRefresh`,
  `POST api/Auth/GetUserToken` (user identity → token).
- `POST api/Login/UserLogin` (`LoginController.cs`): username + MD5 password
  against `SysUser` (via `ISysUserVDmnService`), then JWT. AuthCenter makes NO
  outbound HTTP calls (no HttpClient) — it only reads its own DB + Redis (db 1).

### 8016 WebAPI.Manage — endpoint families (all `api/*`, global `AuthorizeFilter`)

`Program.cs:62` adds a global `AuthorizeFilter` (from Lib) — every area action
requires the Bearer token issued by 8015. `Program.cs:110` maps SignalR hub
`HisNoticeHub` at `MessageCenter:Hub` (`/hubs/msgcenter`). Root controllers:
`Auth` (GetAppToken/GetUserToken proxies), `Login/UserLogin`, `File`
(UploadFile/WebUploaFile/DrgExcelUpload/FileDelete), `SysNotice`
(PushMsg, getmsg/{nick}/{id}), `Init`. Areas (13): CIS (MedicalDoctorsAdvice,
OutpMedicalRecord, RemoteTreated), DRGS (DRGGrouper), EMR (MedicalApplyHandle,
MedicalBl, MedicalHome), ExtClinic (Clinic), HisJob (NoticeJob), LIS, MRQC
(MRQCScore, MedicalRecordApply), OrderCenter (PayOrder), PACS, PDS
(DrugStorage), PatientCenter (PatBasicInfo), Sett (Booking,
InHosPatientService, OutpatientService), System (AppManage, BaseData,
MsgQueue, SysOrg, SysUser).

## 2. Which ported modules call 8015/8016 (call sites)

The shared pattern (ported from Framework `AuthenManageHelper` to HttpClient,
proxy bypassed): `GetToken()` → `POST {SiteAuthCenterHost}api/Auth/GetAppToken`
(20-min in-memory cache) → `POST {SiteApiManageHost}api/...` with
`Authorization: Bearer <token>`. On 401 it refreshes the token and retries once.

| Caller module | Helper file | Calls 8015 for token | Calls 8016 for data (endpoints) |
|---|---|---|---|
| **MRQC Web** | `openhis-core/Newtouch.MRQC.Infrastructure/Infrastructure/AuthenManageHelper.cs` (GetToken :94, HttpPost :117) | yes | `MedicalCenterController.cs`: `api/MedicalBl/MedicalCenterPatInfo`, `MedicalCenterDiaglist`, `MedicalCenterBlwsTree`; `api/MedicalDoctorsAdvice/GetDoctorAdviceRecore`, `GetInspectionExaminationRecore`; `api/MsgQueue/GetNoticeGroup`, `NewNoticeGroup`, `NewNotice`, `NoticeQuery`; `SysItemController.cs`: `api/MedicalBl/MedicalbllxTreeRecord`; `ApplyController.cs`: `api/MedicalApplyHandle/ModifyApplyApprove` |
| **Sett Web** | `openhis-core/Newtouch.Sett.Infrastructure/Infrastructure/AuthenManageHelper.cs` (twin of MRQC's) | helper ported, but **NO Sett controller calls it** (grep: 0 call sites) | none — dead code today |
| **CIS Web** | `openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/Infrastructure/AuthenManageHelper.cs` (+ `Compat/InfraCompat.cs:67-68` reading the two hosts from config) | yes (`api/Auth/GetAppToken`, `UserTokenRefresh`, `GetUserToken`) | `ClinicApplyController.cs`: `api/Outpatient/OutpatientRegist`, `api/RemoteTreated/DoctorMeetingApply`, `UserJoinMeetingApply`, `api/Clinic/GetClinicToken`, `TreatedApplyResult`, `SendHisMedicalRecord`, `SendHisOutpPrescriptionData`; `MedicalRecordController.cs:253,2223`: `api/Clinic/GetClinicPatMedicalRecord`; `NoticeReadSendApi`: `api/NoticeJob/NoticeListRead` |
| **EMR Web** | `openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/Infrastructure/EmrHelpers.cs` (`SiteApiHelperBase`, GetAppToken :231 posts to `{SiteAuthCenterHost}/api/Auth/GetAppToken`) | yes, token only | NO direct 8016 data calls. Its `SiteHispatinfoAPIHelper`/`SiteCISAPIHelper` target `SiteSettAPIHost`/`SiteCISAPIHost` (sibling module APIs) — and neither key is present in EMR appsettings, so that path already fails on missing config before any network call |
| Herp API | only a comment mentioning 8015 in `AuthController.cs:21` | no | no |
| MRMS | only false positives (GUID substrings `...801543F9` in comments) | no | no |

Endpoint-coverage check: EVERY caller endpoint above exists in 8016
(verified by route grep): `ExtClinic/ClinicController` has GetClinicToken,
GetClinicPatMedicalRecord, SendHisMedicalRecord, SendHisOutpPrescriptionData,
TreatedApplyResult; `System/MsgQueueController` has NewNoticeGroup,
GetNoticeGroup, NewNotice, GetNoticeInfo, NoticeQuery; `HisJob/NoticeJobController`
has NoticeListRead; `Sett/BookingController` has OutpatientRegist;
`CIS/RemoteTreatedController` has DoctorMeetingApply/UserJoinMeetingApply;
`EMR/MedicalBlController` has MedicalbllxTreeRecord/MedicalCenterPatInfo/
MedicalCenterDiaglist; `EMR/MedicalApplyHandleController` has
ModifyApplyApprove; `CIS/MedicalDoctorsAdviceController` has
GetDoctorAdviceRecore/GetInspectionExaminationRecore.

## 3. What breaks TODAY because 8015/8016 are absent

Failure mode is **explicit error, not silent corruption**: with no listener on
8015, `GetToken()` throws inside `HttpPost<T>`'s catch as
`FailedException("[鉴权]获取到 Api 身份令牌异常：Connection refused…")`
(MRQC text still Chinese; Sett twin translated), surfaced by the error
middleware as HTTP 200 + error JSON. Pages that never touch the helper are
unaffected (login, grids backed by local DBs, all previously verified pages).

- **MRQC** (user-visible, in `MedicalCenterController` + `ApplyController` +
  `SysItemController`): medical-center patient list (`GetPatList` →
  MedicalCenterPatInfo), diagnosis list (`GetMedicalCenterDiag`),
  record tree (`GetMedicalCenterBlTree`), doctor-advice record
  (`GetDoctorsAdviceRecord`), inspection/examination record
  (`GetInspectionExaminationRecore`), message send/query
  (`SendMsg`, `MessageSendQuery`, `GetDclMsgJson` → MsgQueue), apply approval
  (`ModifyApplyApprove`), template-tree record (`MedicalbllxTreeRecord`).
  I.e. the whole "medical center / QC" cross-system view is down; local QC
  pages (Apply list, SysItem grid, scores) still work.
- **CIS** (in `ClinicApplyController` + `MedicalRecordController`): remote
  consultation workflow — meeting apply/join (`DoctorMeetingApply`,
  `UserJoinMeetingApply`), result feedback (`TreatedApplyResult`), sending
  records/prescriptions outward (`SendHisMedicalRecord`,
  `SendHisOutpPrescriptionData`), clinic token (`GetClinicToken`), outpatient
  registration via center (`OutpatientRegist`), clinic record pull
  (`GetClinicPatMedicalRecord`), notice read (`NoticeListRead`).
  Ordinary CIS clinic/queue/prescription pages backed by the local CIS DB are
  unaffected.
- **EMR**: cross-system token acquisition fails (8015 absent), so any
  `SiteApiHelperBase` request fails at `GetAppToken()`; additionally its
  Sett/CIS data hosts are unconfigured keys (see §2), so the inpatient-info
  sync (`ZybrjbxxDmnService` → `/api/patient/InPatientQuery`) is doubly
  blocked. Local EMR record pages are unaffected.
- **Sett / Herp / MRMS / OR / Base / PDS**: no center dependency — nothing
  breaks from 8015/8016 absence.
- **Not a break, but a data note**: the brief's "enums will show Chinese when
  8015/8016 exist" is backwards in one respect — the center hosts RETURN
  business data (patient/diagnosis/advice/notice records) from the same demo
  DBs, so DB-sourced Chinese names will appear through them exactly as they
  do through local pages. The center hosts do not themselves translate
  anything; their own user-visible strings are error/validation messages
  (§4), which DO need the Vietnamese pass below.

## 4. Portability assessment

- **No retargeting needed.** Both hosts + all 7 supporting projects are
  already `net6.0` with NuGet-only dependencies (JwtBearer 6.0.21,
  NewtonsoftJson 6.0.21, SignalR + StackExchangeRedis, Swashbuckle, Mapster,
  FreeSpire.XLS, System.Configuration.ConfigurationManager) plus project
  references inside the solution. No views, no WebForms, no WCF, no SSRS,
  no ActiveX, no Office Interop.
- **Binary DLLs — limited, checkable.** The two web csprojs HintPath to
  `his-dll-common/Net6/` (Lib.Base/Common/Redis/Services/DataBaseSvr —
  managed .NET assemblies, present in this checkout) and `his-dll-common/DRG/`
  (`chs_drg_11.dll`, a managed PE32 .NET assembly, with a Net6 wrapper
  `NewtouchHIS.DrgGroup.chs_drg_11.dll` also present). No native-code
  P/Invoke found (`DllImport`: none). Residual risk is confined to the 3
  DRG endpoints (`DrgGroupByMedRecord/List/Xls`): if the DRG assembly
  refuses to load on Linux, stub or drop that area only.
- **Translation debt (for the all-Vietnamese goal).** The solution's .cs
  files still carry Chinese user-visible strings (python Unicode scan):
  AuthCenter 22 distinct runs, WebAPI.Manage 122, Base.DomainService 93,
  DomainService 54, Services 11 — mostly error/validation messages
  (e.g. `应用未注册`, `关键信息不可为空`, `会议号回写失败`). Comments are
  partially Vietnamese already. A Vietnamese sweep of this solution is
  required before it can be called done, same pattern as the module sweeps
  (messages only; never comparison/logic strings, never `Sys_Auth_*`
  values). Size is comparable to a medium module sweep, not a port.
- **Config fixes needed at run time** (same class as SSO/BaseAPI, all
  already proven): bind `ASPNETCORE_URLS` to 8015/8016; proxy env unset;
  `DOTNET_SYSTEM_NET_DISABLEIPV6=true`; Redis password `newtouch` (already
  in appsettings); connection strings → localhost + SA (already done in
  run copy); verify `Top_OrganizeId` (still vendor org in run copy) and
  whether AuthCenter's `SSOAuthApp` HIS.BaseAPI domain needs 8017→7001
  (only matters if something presents as HIS.BaseAPI to AuthCenter;
  AuthCenter itself calls nothing outbound, so likely unnecessary —
  verify by testing, do not assume).
- **Auth data already exists — no seeding needed.** Demo DB
  `NewtouchHIS_Base` holds `Sys_Auth_App` (11 rows, all zt=1: API.Manage,
  CIS, EMR, HIS.BaseAPI, His.Job, HIS.SSO, MRQC, NewtouchHIS.SelfTerminal,
  Oh_Newtouch_Clinic, PDS, SETT) and `Sys_Auth_AppFriend` (13 rows, all
  zt=1, both directions for API.Manage ↔ MRQC/CIS/EMR/SETT/PDS). The exact
  flow modules use — `{AppId: MRQC, Data: "API.Manage"}` →
  `AppKeyGen({FriendAppId: MRQC, AppId: API.Manage})` — resolves to the
  `API.Manage → MRQC` friend row. No DB writes required. (Sett's AppId
  case differs: appsettings `Sett` vs DB `SETT`; SQL Server default
  collation is case-insensitive, so it matches — verify at runtime.)
- **Downstream dependency of 8016 itself.** `WebAPI.Manage` appsettings
  sets `SiteSettAPIHost: http://localhost:7002/` (original Sett-API
  deployment port) and `ApiBaseController` calls out to `SiteSettAPIHost` /
  `SiteCISAPIHost` for Patient/Order flows. Our deployment has no Sett API
  on 7002 (Sett is the merged Web app on 5221; CIS API is on 5334). The
  run agent must map these two keys to our ports (or confirm which 8016
  actions actually use them) — otherwise those particular 8016 actions
  fail the same way today's callers fail. CIS side is mappable to 5334;
  Sett side needs a decision (5221 web? a Sett-API shim? leave failing?).

## 5. Recommendation

**RUN both hosts on 8015/8016 — do not stub the call sites, do not leave
them absent.** Reasons: (a) they are already net6.0, so this is a
build+configure+translate task, not a port; (b) every caller endpoint
already exists in 8016, so running them unblocks real user workflows
(MRQC medical-center views, CIS remote consultation) with zero module
changes — module configs already point at 8015/8016; (c) stubbing would
mean inventing fake cross-system data (patient records, consultation
results), which is worse than an explicit error; (d) leaving them absent
leaves the two named workflows permanently broken, contradicting the
"entire system running" goal.

Proposed shape (for the parent to approve/dispatch as ONE run task):

- **Placement:** new home `openhis-core/Newtouch.HIS.Center/` (copy of the
  `Newtouch.HIS.WebAPI.Manage` solution from the run copy, so the
  already-fixed connection strings carry over) — do NOT run from the
  translation checkout, and do NOT modify `lmsoft_openhis_run`'s copy.
  Keep the single `.sln` with all 9 projects; no new solution needed.
- **Ports/launchers:** AuthCenter → `run-authcenter.sh`, `ASPNETCORE_URLS=
  http://127.0.0.1:8015`, log `authcenter.log`; WebAPI.Manage →
  `run-apimanage.sh`, `http://127.0.0.1:8016`, log `apimanage.log`.
  Both: proxy unset, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, Release build.
- **Order:** AuthCenter first (verify `GetAppToken` with a real module
  AppId/Data pair against Base DB, then a `GetUserToken` negative case),
  then WebAPI.Manage (verify Bearer-token gating + one endpoint per caller
  family: MedicalBl, MsgQueue, Clinic, RemoteTreated, Outpatient).
- **Translation:** Vietnamese sweep of the solution's messages only
  (§4 counts), before declaring done; rebuild + restart after.
- **Durability:** `recovery.sh` sections 25 (8015) + 26 (8016), watchdog
  entries for both ports (port check only, as for other API hosts).
- **Verification bar:** MRQC `GetPatList` and CIS `DoctorMeetingApply`
  (or their JSON endpoints with session) return real center data or a
  clean business error — not a connection-refused FailedException; MRQC
  Web 5220 and CIS Web 5234 logins still 200; no other app touched.
- **Open decisions for the parent (not taken here):** Sett-side mapping
  for 8016's `SiteSettAPIHost` (5221 vs shim vs leave failing); whether
  `Top_OrganizeId` in the center appsettings needs the demo-org fix;
  whether the DRG area stays if its assembly fails to load.

## Evidence index (paths verified by reading, not by building)

- `lmsoft_openhis/Newtouch.HIS.WebAPI.Manage/Newtouch.HIS.WebAPI.Manage.sln`
- `.../NewtouchHIS.AuthenticationCenter/Controllers/AuthController.cs:29-31,54-56,79-81,99-101`
- `.../NewtouchHIS.AuthenticationCenter/Controllers/LoginController.cs:27-29`
- `.../NewtouchHIS.AuthenticationCenter/appsettings.json` (JWT/SSOAuthApp/BaseDb)
- `.../NewtouchHIS.WebAPI.Manage/Program.cs:62,107,110`
- `.../NewtouchHIS.WebAPI.Manage/Controllers/ApiBaseController.cs:19-21`
- `.../NewtouchHIS.WebAPI.Manage/Areas/{CIS,DRGS,EMR,ExtClinic,HisJob,LIS,MRQC,OrderCenter,PACS,PDS,PatientCenter,Sett,System}/Controllers/*.cs`
- `.../NewtouchHIS.Base.DomainService/AppManage/AppManageDmnService.cs:36-70`
- `.../NewtouchHIS.Base.Domain/Entity/AppManage/SysAuthAppEntity.cs`,
  `SysAuthAppFriendEntity.cs` (tables `Sys_Auth_App`, `Sys_Auth_AppFriend`)
- Callers: `openhis-core/Newtouch.MRQC.Infrastructure/.../AuthenManageHelper.cs`,
  `openhis-core/Newtouch.Sett.Infrastructure/.../AuthenManageHelper.cs`,
  `openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/.../AuthenManageHelper.cs`
  + `Compat/InfraCompat.cs:67-68`,
  `openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/.../EmrHelpers.cs:217-300`,
  `openhis-core/Newtouch.MRQC.Web/Areas/QualityControlManage/Controllers/MedicalCenterController.cs`
  (GetPatList/GetMedicalCenterDiag/GetMedicalCenterBlTree/GetDoctorsAdviceRecord/
  GetInspectionExaminationRecore/SendMsg/MessageSendQuery),
  `openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web/Areas/ClinicManage/Controllers/ClinicApplyController.cs`
- DB (SELECT only): `NewtouchHIS_Base.dbo.Sys_Auth_App` (11 rows),
  `Sys_Auth_AppFriend` (13 rows); tables absent from Union/Sett/CIS/EMR DBs.
