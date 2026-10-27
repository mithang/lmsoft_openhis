# FINAL verification — OpenHIS .NET 6 migration (2026-10-26, evening ICT)

Method: per-app login (POST /Login/CheckLogin as 000000, md5 via hashlib, ALL session
cookies — first-cookie-only gives false NREs on context pages), then a crawler enumerating
GET actions from controller source (root + Areas, skipping [HttpPost]-only), probing each
route and classifying the BODY (status codes lie: error middleware returns HTTP 200 with
error JSON). A page is GOOD only if the body is real HTML. Scripts (ephemeral, /tmp):
final-crawl.py (declared actions), inherited-sweep.py (base-class Index/Form actions).

## Port sweep at start: all 20 ports UP
1433 SQL, 6379 Redis, 41142 static, 7001 BaseAPI, 5208 SSO, 5220 MRQC, 5221 Sett,
5231 OR, 5232 MRMS, 5233 Base HOSP, 5234 CIS, 5235 EMR, 5236 Herp, 5237 PDS,
5238 Base Web, 5331 OR.API, 5333 HOSP.API, 5334 CIS.API, 5335 EMR API, 5336 Herp API.

## Declared-action crawl (full-cookie run)
| App | Pages | HTML | JSON_ERROR | EMPTY | OTHER |
|-----|------:|-----:|-----------:|------:|------:|
| MRQC 5220 | 9 | 9 | 0 | 0 | 0 |
| Sett 5221 | 365 | 352 | 8 | 1 | 2 fragments (GOOD partial views) + 2 JSON_OTHER |
| MRMS 5232 | 39 | 37 | 1 | 0 | 1 (Error/NotFound page itself) |
| OR 5231 | 28 | 22 | 4 | 1 | 1 (Error/NotFound) |
| CIS 5234 (pre-fix) | 209 | 193 | 14 | 1 | 1 AmbiguousMatch |
| CIS 5234 (post-fix) | 209 | 199 | 9 | 1 | 0 |
| EMR 5235 (pre-fix) | 76 | 69 | 6 | 1 | 0 |
| EMR 5235 (post-fix) | 76 | 70 | 5 | 1 | 0 |
| Herp 5236 | 58 | 57 | 0 | 1 | 0 |
| BaseHOSP 5233 | 69 | 64 | 3 | 0 | 2 (Error/Forbidden, Error/NotFound) |
| BaseWeb 5238 | 11 | 11 | 0 | 0 | 0 |
| PDS 5237 | 133 | 117 | 5 | 11 | 0 |

Inherited Index/Form sweep (all apps): NO ambiguity, NO JSON errors anywhere. Bonus real
HTML pages found: Sett 72, MRMS 3, OR 5, CIS 33, EMR 8, Herp 17, BaseHOSP 90, BaseWeb 28,
PDS 9. Inherited VIEW_NOT_FOUND: Sett 121, MRMS 28, OR 23, CIS 67, EMR 15, Herp 27,
BaseHOSP 18, BaseWeb 0, PDS 50 — checked against exact original view paths: ZERO
port-dropped views (every one is also missing in the original). Inherited EMPTY_404
(MRQC 4, Sett 7, MRMS 4, OR 5, CIS 6, EMR 4, Herp 5, BaseHOSP 10, BaseWeb 4, PDS 4) are
controllers with no inherited action (plain Controller base) — correct 404s, not pages.

## BAD-page triage (all verified against original source)
- View-not-found (~30 declared + ~349 inherited): FAITHFUL reproductions, NOT port bugs.
  Original controller exists, original view file missing (exact-path checks, e.g. BaseHOSP
  ProjectController at Controllers/Sys/ProjectController.cs has ProjectType/Form/ChildForm
  actions but NO Views/Project/ folder at all). Same class as the Sett Report/Index
  exception: Framework base Index()/Form() returning View() with no view file fails
  identically in the original. Do NOT stub (would invent pages the original never had).
  Includes Sett ChargeConfirm, MZRegister, TreatmentplanQueryOutpatient, RevenueForm,
  RemoveLog, AddItem; MRMS Report/Index; OR ORApplyInfo/Index, Operation/Common/Index,
  Report/Index, SystemManage/Common/Index; EMR MRHomePage/Index, OutHosPatQuery,
  Report/Index, TemplateDesign/Index; BaseHOSP Project/ProjectType{,Form,ChildForm};
  PDS PurchaseAdd, PurchaseManage/Purchase/Index, ReportManage/Purchase/Index,
  ReportManage/Ypfy/Index, ReportManage/yf/Index; CIS Apply/Test,
  GetTemplatePresDetailByMbId, ProfitAndLoss, PrepareMedicine/Index, DispensingQuery,
  PatientPreDiag, ApplyFormUploadForm (original OrderExecution views: only
  ApplyFormUpload, BindingFeeForm, Index, MedicalSkillExecution, MedicalSkillQuery).
- EMPTY HTTP 404 (crawler artifacts, NOT real pages): actions commented out in source but
  matched by the method regex — Sett Refund/refund (lines ~120-124), OR
  SystemManage/BaseData/Index, EMR MRHomePage/MainPreView (lines 66-69), CIS
  NurseManage/sldAPPlication/WzSldIndex (lines 44-47), PDS PharmacyDepartment/
  OpenMadicineForm + OpenMedicineIndex (lines 109/119).
- PDS compile-removed controllers (deliberate, Newtouch.PDS.Web.csproj lines 63-72
  <Compile Remove=...>): AntibioticType, PharmacyWindow, SysMedicine, SystemManager,
  Base/Module, Base/Organize, Base/RoleAuthorize, Base/Role, Base/User,
  OutOrInStoredManage/Transfer. Explains 9 of PDS's 11 EMPTY 404s — by design
  (SystemManage-type controllers excluded; SSO owns sys-manage). No other app's csproj
  has Compile Remove entries.
- Herp BillManage/Test/Index: real action returning View(), but the view file contains
  only a BOM + newline (1 byte) — a dev test page, not a menu page. Note only.
- Sett 2 explicit unported-function stubs (KNOWN stub class, Vietnamese messages):
  HospitalizationManage/BookkeepInHos/GetChargeItemData, HospitalizationManage/
  Settlement/SettPreview.
- Param-dependent, faithful (NOT failures): CIS InfusionManage/Query (view accesses
  ViewBag.patInfo.patientName; needs ?tags=...; original view identical), EMR
  MedicalRecord/PreViewEdit (GetMedicalRecord does bllx.Substring(0,1); needs blid+bllx;
  original code identical).
- Demo-data gap (NOT a port bug): CIS NurseManage/OrderExecution/ApplyFormUpload —
  "Invalid object name 'xt_JyjcFileUpload'": the table exists in NONE of the demo DBs
  (checked Newtouch_CIS, NewtouchHIS_Base, Newtouch_Union). Original would fail
  identically on this demo data. Creating the table would be a DB schema write — NOT
  authorized. Note: pre-fix this page NRE'd in the ctor, so the SQL error only became
  visible after the ctor fix.
- Sett OTHER fragments (ChargePayDialog <style> fragment, SysPatiChargeAdd <h2> fragment):
  partial views — GOOD. Sett JSON_OTHER (PatientManage/PatientInHosList/LoadData,
  PatientOutOfHosList/LoadData returning []): JSON data endpoints, not view pages —
  not failures. Error/NotFound + Error/Forbidden pages: the error pages
  themselves — not failures.

## Fixes applied during verification (all verified live after fix)
1. CIS routing ambiguity — Newtouch.HIS.CIS/Newtouch.CIS.Web/Controllers/ControllerBase.cs:
   local ControllerBase re-declared [HttpGet] virtual Index()/Form() HIDING (not
   overriding) Lib BaseController's virtuals → AmbiguousMatchException (500) on
   /ClinicManage/ClinicMetting/Index, /InfusionManage/Index, /InfusionManage/Form.
   Only 2 controllers extend local ControllerBase (InfusionManage, ClinicMetting).
   FIX: both changed to `public override` + comment. After rebuild (Debug) + restart via
   run-5234.sh: ClinicMetting/Index serves real HTML (4126 bytes); InfusionManage
   Index/Form reach view lookup → view-not-found JSON (faithful: original has only
   Views/InfusionManage/query.cshtml).
2. CIS SysConfigRepo wrong table — Newtouch.CIS.Infrastructure/Repository/
   CrossModuleRepos.cs: GetValueByCode queried NewtouchHIS_Base.Sys_GlobalConfig (0 rows
   in demo DB) and ignored organizeId. Codes callers request live in
   Newtouch_CIS..Sys_Config (per-org rows). FIX: query CisDb Sys_Config
   `SELECT TOP 1 Value FROM Sys_Config WHERE Code=@code AND OrganizeId=@org AND zt='1'
   ORDER BY px` + comment. After rebuild + restart: OrderExecution pages serve real
   HTML (Index 40149 bytes, MedicalSkillExecution 18630 bytes).
3. CIS BespeakRegisterApp missing DI ctor — Newtouch.CIS.Infrastructure/Application/
   Implementation/BespeakRegisterApp.cs: 4 readonly repos, NO constructor (original file
   identical — Autofac-era code, fields never assigned in either tree). Under MS DI the
   parameterless ctor left them null → NRE at GetConfigureRegTime() line 89. All four
   services ARE registered in CIS Program.cs. FIX: proper DI ctor assigning all four +
   comment. After rebuild + restart: /SystemManage/BespeakRegisterManage/BespeakRegister
   serves real HTML (35903 bytes). Note: bespeakTime config row exists in CIS Sys_Config
   only for org cdbfce48-..., NOT demo org 6d5752a7-... — fallback defaults cover it;
   adding a demo-org row would be a DB write (NOT authorized).
4. EMR Main StringValues ambiguity — Newtouch.HIS.EMR/Newtouch.EMR.Web/Areas/MRHomePage/
   Controllers/MRHomePageController.cs:61 assigned ViewBag.justshow = Request.Query
   ["justshow"] (StringValues in Core, plain string in Framework); MainEdit.cshtml line
   57 evaluates (justshow == null ? "" : justshow).Equals("true") → runtime binder
   ambiguous == overloads. FIX: .ToString() on the assignment (only such
   ViewBag.* = Request.Query assignment in EMR Web) + comment. After rebuild (Debug, 0
   errors; one pre-existing CS0114 warning, unrelated) + restart via root run-emr.sh:
   /MRHomePage/MRHomePage/Main serves real HTML (177471 bytes), MainEdit 173436 bytes.
   EMR Web PID after restart: 8958. MRHomePage/Index remains view-not-found (faithful).
5. CIS.API BadImageFormatException (self-inflicted build contention, fixed): POST
   /api/MzBespeakRegister/MzBespeakRegisterQuery returned Bad IL from DI resolution —
   verification's own CIS solution rebuilds overwrote the API bin folder while API
   process (PID 4076) was running (DLL timestamp 15:52 vs process start 15:29). FIX:
   stopped API by exact PID 4076, clean rebuild of Newtouch.CIS.API.csproj (Debug, 0
   errors), restarted via run-5334.sh (root 404 as expected). After restart:
   MzBespeakRegisterQuery returns HTTP 400 validation (blh/zjh required) instead of Bad
   IL. Lesson: stop the running app BEFORE rebuilding its solution.
   Crawler bug found and fixed: /tmp/final-crawl.py probe() error path returned 5 values
   but assembly unpacks 6 → crashed the first CIS+EMR re-crawl attempt. Fixed; post-fix
   counts above come from the successful re-run.

## API matrix (temp Redis tokens created then DELETEd, EXISTS 0 after each)
- OR.API 5331: POST /api/Default/Query {Timestamp} → 10000, data []. Missing Timestamp →
  TIMESTAMP_IS_REQUIRED. GetToken → 40004 GETTOKEN_DISABLED + VI explanation (stub).
  ORPatList no token → TOKEN_IS_REQUIRED, fake token → TOKEN_IS_EXPIRED, real temp
  Redis token → 10000 data [] (OR_Registration 0 rows); UA GetUserInfo + temp token →
  10000 real identity (OR.API precedent, verified by OR.API agent).
- HOSP.API 5333: POST /api/MedicineStorageIOMode/Query {Timestamp, crkbz:"0"} → 10000 +
  real rows (mode names are DB-sourced Chinese — known keep). GetToken → 40004
  GETTOKEN_DISABLED + VI explanation.
- Herp API 5336: POST /api/Default/Query {Timestamp} → 10000 + real SysConfig rows
  (sfdlCode row; previously BLOCKED_ON_INTEGRATION — unblocked by Herp ISysConfigRepo).
  Missing Timestamp → TIMESTAMP_IS_REQUIRED. GetToken → 40004 AUTH_NOT_IMPLEMENTED +
  VI explanation (stub).
- EMR API 5335: POST /api/Default/Query without AppId → HTTP 400 "AppId là bắt buộc";
  with AppId, no token → 40004 "Token không hợp lệ hoặc đã hết hạn"; with AppId +
  header token + Redis key EMR:ApiIdentity:{token} + JSON UserIdentity value →
  10000 + real SysConfig rows (earlier failures were test errors: plain-string value
  fails DeserializeObject; key prefix is the port's own convention). GetToken (with
  AppId) → 40004 + VI disabled explanation (stub).
- CIS.API 5334: GetToken → 40004 GETTOKEN_DISABLED + VI explanation (stub). Default/Query
  → empty body (Default controller has no Query action — not a bug). Token mechanics
  (source, TokenStore.cs): RAW token as Redis key; GetUaUser parses JSON for UserCode +
  OrganizeId. Verified with CORRECT mechanics (earlier tests used wrong key pattern
  CIS:ApiIdentity:{token} and header transport — invalid for this API, which reads
  Token/TokenType from the request BODY):
  - PatInfo/InpatientBaseInfo {Token, TokenType:"CIS", Timestamp} → 10000, data null.
  - MzBespeakRegisterQuery {Token, TokenType:"CIS", Timestamp, blh, zjh} → 10000, [].
  - UA GetUserInfo {access_token, AppId, Timestamp} → 10000 + real identity
    {Account:"000000", OrganizeId: demo org}. (Without AppId → AUTH_FAILED, correct
    behavior — the action requires both fields.)
  - Exception: MzBespeakRegister no-token → INTERNAL_SERVER_ERROR envelope (NRE,
    faithful original behavior — no [DefaultAuthorize] — not a new bug, no fix).

## Language check (served HTML, scripts/comments/CSS comments stripped)
- PDS: REAL translation debt — 67 pages with CJK, 263 distinct CJK strings, all genuine
  untranslated UI (labels, placeholders, grid headers, helper text), e.g. 入库单号,
  出库单号, 出库类型, 统计年月, 请输入申领部门, 请输入姓名, 请输入单据号, 请选择发票,
  请选择领药科室, 用于创建或修改发药配置信息, 请填写发药配置信息, 损益原因, 损益标志,
  全部排药, 自动打印, 盘点打印, 展示的出库数量仅供参考. Sampled pages: /BillManage/
  ApplyBill/ApplyOutStock, /BillManage/OutOrInStorageBill/OutOrder, /Dictionaries/
  FypzForm, /Dictionaries/KsFypzForm, /Dictionaries/syyyForm, /DrugStorage/
  ApplyDelivery. This is a module sweep, NOT a verification fix — reported as the main
  language blocker; follow-up sweep task for the parent to dispatch (scope: PDS Web
  views only, no DB writes, CIS-sweep pattern, rebuild + restart via run-pds.sh,
  re-shoot with new filenames).
- All other apps: CJK on served HTML = keeps only (verified by extracting distinct
  strings per app): fonts 微软雅黑/宋体/黑体/楷体 (Error/HttpError + template pages);
  vendor proper nouns 天天开源/天天开源软件/湖北天天数链技术有限公司 (BaseHOSP/EMR Home);
  demo content 张三/李四/熊甜甜/李四主任医师/消化/阑尾炎切除, Sett 大白 (demo patient in
  FeeConfirm demo view); DB/data values: Sett 城保 (hardcoded <option value> insurance
  type, submitted/compared — must keep); EMR adverse-event checkbox VALUES (labels
  already Vietnamese, e.g. "Vết thương toác ra" in MainOpEdit.cshtml line 174 — values
  are saved/compared data, must keep); EMR TemplateEdit 保存 is an icon filename
  (-_保存.ico in a style URL); EMR MedRecordTreeEdit(V2) 住院号/门诊号 are hidden input
  defaults (type=hidden, readonly), not visible text; CIS AdviceList CJK = CSS comments
  only; CIS QueueInfo/ArrangeForm post-strip = demo/DB values only.

## Screenshots (-final)
Dir: ~/workspace/openhis-core/shots-final/ (13 PNGs, Playwright+Firefox, cookie login
as 000000; all viewed personally by the verifier):
- mrqc-home-final.png — MRQC landing, Vietnamese.
- sett-xnh-final.png — Sett XNH notice page, Vietnamese (required shot).
- sett-roster-final.png — Sett roster calendar, Vietnamese.
- mrms-home-final.png — MRMS dashboard, Vietnamese (sidebar labels ASCII without
  diacritics = earlier DB pass pattern; vendor 云HIS logo = keep).
- or-home-final.png — OR landing, Vietnamese.
- cis-clinicmetting-final.png — BLANK in headless Firefox, NOT a failure: the view
  (6 lines, title "Hội nghị video") is byte-equivalent to the original — the original
  page is itself an empty shell whose video UI lives in an external client. Server
  serves real HTML (4126 bytes, Vietnamese title).
- cis-orderexec-final.png — OrderExecution page, Vietnamese (fixed page; empty grid
  correct; 内科一病区 ward name = DB-sourced keep).
- cis-bespeak-final.png — BespeakRegister schedule grid, Vietnamese (fixed page;
  fallback defaults render Mon 2026-10-26…Sun 2026-11-01).
- emr-main-final.png — EMR Main record form, Vietnamese (fixed page; 全公费 dropdown
  value = DB-sourced keep).
- herp-supplier-final.png — Herp supplier grid, Vietnamese (empty grid correct).
  Note: first attempts failed with NS_ERROR_NET_EMPTY_RESPONSE / 404 because the
  shot used the wrong URL (/Supplier/Index); correct route is
  /SupplierManage/Supplier/Index (Area SupplierManage).
- basehosp-home-final.png — BaseHOSP portal frame, header Vietnamese but sidebar/body
  EMPTY in a standalone shot. NOT counted as a page failure: the frame renders its
  sidebar from top.clients.authorizeMenu supplied by the SSO portal host (the frame
  is designed to run inside SSO); standalone, the bootstrap endpoint
  /ClientsData/GetAsyncClientsDataJson 404s on both Base apps (5233 and 5238), so the
  menu stays empty. Same hosting design as the original. Functional BaseHOSP pages
  (organize, itemstype, department, etc.) were verified with content in v2 shots.
- baseweb-home-final.png — Base Web dashboard, full Vietnamese (tiles, charts).
- pds-home-final.png — PDS dashboard, Vietnamese (tiles, charts; 【中药房】 pharmacy
  name = DB-sourced keep; vendor footer = keep). NOTE: the dashboard is Vietnamese
  but 67 deeper PDS pages still carry untranslated Chinese (see Language check) —
  the home shot must not be read as "PDS fully translated".

## Verdict
- Page-serving: YES, except the listed exception classes. Every app serves real HTML
  on all its real pages; every non-HTML result triaged as faithful view-not-found,
  explicit VI stub, param-dependent, crawler artifact, compile-removed controller,
  demo-data gap (xt_JyjcFileUpload table), or data endpoint. No real broken page
  remains unfixed after the 5 fixes above.
- All-Vietnamese: NO — blocked by the PDS sweep (263 distinct CJK strings, 67 pages).
  All other apps: keeps only.
- APIs: all 5 hosts verified with correct token mechanics; GetToken stubs return
  disabled explanations, not crashes.

## PDS Web sweep — DONE (2026-10-26 late ICT, follow-up to the language verdict above)
- Scope: PDS Web views only (183 cshtml; Views/testPrint/Index.cshtml excluded as a demo
  print template with fake patient data). No DB writes, no pushes, no other apps touched.
- Method: comment-stripped CJK scan (985 distinct visible runs at start) -> reused 241
  translations from Base/Sett/CIS maps -> batch 1 (160) + batch 2 (303, after pruning 9
  stray single-char entries + 19 invented extras) -> batch 3 via TSV heredocs (245, after
  the JSON-write channel repeatedly emitted a degenerate generic table 7 times; each bad
  file was deleted, never applied). Exact-run replacement with masking of comparison
  literals, djmc values, report URLs and img filenames, so server-data comparisons were
  never touched. Two real translation-consistency bugs fixed: stale "==请选择==" placeholder
  comparisons in HandOutMedicineToks/RequestOfReturn (placeholders already Vietnamese,
  checks were dead) -> "==Vui lòng chọn==", matching HandOutMedicine.cshtml:675;
  PriceAdjustmentApproval filter options 已拒绝/已撤销 (codes 2/3) -> Vietnamese, server
  comparisons untouched; YbbxblForm 0~100 range alerts (word-order trap) via full-string
  replacements; About "Sql注入" -> "Tiêm SQL".
- Result: 954 + 317 + targeted replacements across ~150 files; comment-stripped leftovers
  985 -> 62 distinct / 83 runs, ALL classified keeps: server-data comparisons (是/否 in
  RepercussionIndex, 已保存/已审核/待审核/已传报/审核通过/已作废/有效/已撤回/正常/控制/
  发药/退药/口服/外用/雾化/静脉滴注/重大疾病/儿童/耐多药/退药成功, [停] marker, 全部 option
  comparisons), djmc lookups (科室发药单/药品入库单/外部出库单), SSRS report names
  (科室备药申请发药单/科室备药退回单/报损报溢打印表/药品过期预警), vendor nouns
  (天天开源软件/湖北天天数链技术有限公司), font (微软雅黑), img filename (消息提醒.png),
  JS comments (记录数), demo table cell (系统内), demo page testPrint (18 runs).
  是/否 display occurrences (inline-edit options, radio labels, code->text formatters)
  translated to Có/Không with the 2 comparisons + 1 demo cell masked; verified zero
  Có/Không inside any comparison afterwards.
- Build: Release, 0 errors (176 pre-existing warnings). Restarted via run-pds.sh only
  (old PID 4582 stopped first; new PID 15242). Login 200, CheckLogin success as 000000.
  Live spot check (comment-stripped served HTML, 10 pages incl. Home, BriefInStorage,
  InStorage, OutStorage, OutOrder, FypzIndex, ApplyDelivery, DrugDelivery2018,
  InventoryAction, SysMedicine): 0 untranslated UI strings; remaining runs are keeps only
  (Home: vendor + img filename; DrugDelivery2018: usage-name comparisons).
- Screenshots (Playwright+Firefox, cookie login, shots-final/): pds-final2-home,
  pds-final2-instorage, pds-final2-outorder, pds-final2-fypz, pds-final2-drugdelivery,
  pds-final2-inventory. Viewed instorage + outorder: fully Vietnamese chrome.
  HONEST CAVEAT: dropdown values 正常入库/正常出库 visible in shots are SERVER-supplied
  enum data (absent from all PDS views/JS) — not view literals, left for the enum/DB pass
  decision like other modules' DB-sourced names; do not read the shots as "zero Chinese
  bytes anywhere".
- Verdict update: the final verification's sole language blocker (PDS, 263 strings /
  67 pages) is CLOSED for view-served UI. All-Vietnamese now blocked only by the
  already-known server/DB layer (enum Descriptions, DB row values) pending user decision.

## MRMS.API port (2026-10-26 ~23:35 ICT, Scope B follow-up)
- Host on 127.0.0.1:5332 (PID 16400, Release build 0 errors, launcher
  ~/workspace/openhis-core/run-mrms-api.sh, log mrms-api.log). Added to
  Newtouch.HIS.MRMS.sln; reuses MRMS Web SqlSugar ISysConfigRepo
  (Newtouch_MRMS..Sys_Config) + MRMS Web appsettings (BusinessDB/RedisConfig,
  env vars re-added last so launcher SA override wins).
- Envelope: exact copy of the OR.API reconstruction (code/sub_code/sub_msg/data,
  SUCCESS=10000/FAIL=40004, Newtonsoft indent + yyyy-MM-dd HH:mm:ss, jsonpCallback,
  raw token as Redis key, 20-min renewal, GETTOKEN_DISABLED stub).
- Original bug fixed: DefaultController._sysConfigRepo never assigned in Framework
  (ctor took only IComponentContext, with a CS0649 pragma admitting it) -> DI ctor now.
- Verification matrix (live, temp Redis token created then DELETEd, EXISTS 0 after):
  - POST api/Default/Query, no token, valid Timestamp -> 10000, data[9] (matches
    demo DB Sys_Config row count 9; original Query has no [DefaultAuthorize], so
    no-token success is faithful, same as OR.API Query).
  - POST api/Default/Query, valid temp token (TokenType MRMS) -> 10000, data[9].
  - Missing Timestamp -> TIMESTAMP_IS_REQUIRED; fake token -> TOKEN_IS_EXPIRED;
    token without TokenType -> TOKEN_TYPE_IS_REQUIRED; mismatched TokenType ->
    TOKEN_TYPE_NOT_MATCH; jsonpCallback -> cb(...) wrapped, code 10000.
  - POST api/Auth/GetToken -> 40004 GETTOKEN_DISABLED, no token in data (stub by
    design: original minted tokens for "?" identities without credential check).
  - GET api/Default/5 -> NOT_IMPLEMENTED (original scaffold returned "value" only).
  - Empty body -> HTTP 400 from [ApiController] model validation before the action
    (same as OR.API; does not reach CommonExecute SOMEARGUMENTS_IS_REQUIRED).
  - MRMS Web 5232 login still 200 after the port (not broken).
- recovery.sh section 22 added (syntax OK); watchdog entry for 5332 still owed
  to parent (cron not edited by this agent). README: Newtouch.MRMS.API/README-MRMS-API.md.
- Remaining APIs after this: PDS.API 5337, MRQC.API (skeleton only).
- MRMS.API note (same evening): first instance (PID 16400, started from a backgrounded
  exec session) died when that session was reaped — same lesson as the Sett incident.
  recovery.sh section 22 restored it (new PID 3202, Query 10000 re-verified live);
  recovery-started instances are the durable path, not nohup from exec sessions.

## PDS.API port (2026-10-27 ~00:20 ICT, Scope B follow-up)
- Host on 127.0.0.1:5337 (PID 7093, Release build 0 errors, launcher
  ~/workspace/openhis-core/run-pds-api.sh, log pds-api.log). Added to
  Newtouch.HIS.PDS.sln; reuses PDS Web SqlSugar infra + the Web's full explicit
  DI registration block (copied mechanically from Web Program.cs) + PDS Web
  appsettings (BusinessDB/RedisConfig, env vars re-added last so launcher SA
  override wins).
- Envelope: reconstructed from the PDS tree's OWN ApiControllerBase mirror
  (Newtouch.PDS.API/App_Start/ApiControllerBase.cs) — code/sub_code/sub_msg/data
  (+ msg, which PDS Stock actions assign), SUCCESS=10000/FAIL=40004, Newtonsoft
  indent + yyyy-MM-dd HH:mm:ss, raw token as Redis key, 20-min renewal.
  Adaptations to the ported RequestBase shim (Timestamp non-nullable DateTime →
  missing = MinValue → TIMESTAMP_IS_REQUIRED; no Validate() → reflection over
  [Required]; no jsonpCallback and no PDS controller ever used the jsonp overload
  → plain JSON; [ApiController] model-state filter suppressed so CommonExecute
  returns {FIELD}_IS_REQUIRED / SOMEARGUMENTS_IS_REQUIRED instead of HTTP 400;
  input formatter switched to Newtonsoft — DTO SysPharmacyDepartmentRequestDto
  redeclares organizeId with different case than base OrganizeId, which made
  System.Text.Json throw HTTP 500; Newtonsoft matches the original formatter).
- Original bugs fixed (same class as MRMS/CIS): unassigned app-service fields in
  Outpatient, Zyypyz, ResourcesOperate (9 actions), PharmacyDepartment (2 fields),
  Stock controllers → proper DI ctors. TestController uses `new
  TestMultithreadTransApp()` as the original did (ported interface only declares
  testEFDbTransaction).
- GetToken stub: keeps the original's non-empty-field gate (missing →
  AUTH_FAILED) then always GETTOKEN_DISABLED — the original minted tokens with
  only a "giả sử xác thực đã thành công" comment and no credential check.
- Hospitalization ZYTYInterface: no envelope/auth/[ApiController] (as original),
  raw SqlSugar SQL with the original 3-part table names, messages translated to
  Vietnamese, logic/codes intact, INCLUDING the original quirk of unconditionally
  setting ResultCode=AllFailure even on full success (documented, not "fixed").
- Verification matrix (live, temp Redis token created then DELETEd, EXISTS 0 after):
  - GetToken: missing Timestamp → TIMESTAMP_IS_REQUIRED; empty fields →
    AUTH_FAILED; full fields → GETTOKEN_DISABLED; null body →
    SOMEARGUMENTS_IS_REQUIRED.
  - DeptPharmacyDrugs: missing Timestamp → TIMESTAMP_IS_REQUIRED; missing
    DeptCode → DEPTCODE_IS_REQUIRED; no token → 10000 + original hard-coded
    string; fake token → TOKEN_IS_EXPIRED; token without type →
    TOKEN_TYPE_IS_REQUIRED; valid temp token (TokenType PDS) → 10000;
    mismatched type → TOKEN_TYPE_NOT_MATCH.
  - Stock/query: no token and temp token → 10000 (read path, empty yplist →
    data null).
  - PharmacyDepartment query: real ypId 822 → 10000 + real yfbm rows
    (DB-sourced Chinese names are keeps); nonexistent ypId → 10000 [];
    ypId "x" → int-conversion error (bad input, original fails the same way).
  - Hospitalization: body [] → ValidationFailure (Vietnamese); missing yzId →
    Error1; fake zxId → Error3 (SELECT only — no UPDATE executed).
  - Write paths (ResourcesOperate, Outpatient, Zyypyz, Empowerment*,
    Stock Prepare/Withdrawal, ClinicTfOperate, TestMultithreadTrans*):
    verified ONLY to the validation gate (TIMESTAMP_IS_REQUIRED /
    {FIELD}_IS_REQUIRED) — NOT executed live on the demo DB (no-write policy).
  - TestToken → 10000 (returns SUCCESS directly, no CommonExecute — as original);
    Values → NOT_IMPLEMENTED (scaffold convention); root GET → status text.
  - PDS Web 5237 login still 200 after the port (not broken).
- Port-time bugs fixed: (1) first Program.cs omitted AddWebServiceBase (SysConfig
  never initialized → OperatorProvider static-ctor NRE) and AddMemoryCache
  (ICache needs IMemoryCache) — fixed to the Web's exact order; (2) first
  launcher wrote a placeholder instead of $MSSQL_SA_PASSWORD → DB login failed —
  fixed; (3) recovery.sh section 23 initially skipped because the invoking exec
  session's own command line matched `pgrep -f "Newtouch.PDS.API"` — re-running
  recovery.sh from a clean command line started the durable instance (PID 7093,
  Query 10000 re-verified). Lesson: never invoke recovery.sh from a command line
  containing a guarded process pattern.
- recovery.sh section 23 added (syntax OK, final ss list includes 5337);
  watchdog entry for 5337 still owed to parent (cron not edited by this agent).
  README: Newtouch.PDS.API/README-PDS-API.md.
- Remaining APIs after this: MRQC.API (skeleton only). Center hosts 8015/8016
  still not ported.

## MRQC.API port (2026-10-27 ~00:25 ICT, Scope B follow-up — LAST module API)
- Host on 127.0.0.1:5320 (PID 7897, Release build 0 errors, launcher
  ~/workspace/openhis-core/run-mrqc-api.sh, log mrqc-api.log). Added to
  Newtouch.HIS.MRQC.Core.sln. CORRECTION to the brief: no MRQC.API skeleton
  existed in openhis-core at all — built fresh by cloning the MRMS.API twin
  (original MRQC.API is structurally identical: same 2 controllers, same
  request DTOs, same unassigned-repo bug, same "?"-identity GetToken).
- Envelope: OR.API/CIS.API/MRMS.API/PDS.API precedent (code/sub_code/sub_msg/data,
  SUCCESS=10000/FAIL=40004, Newtonsoft indent + yyyy-MM-dd HH:mm:ss,
  jsonpCallback, raw token as Redis key, 20-min renewal, GETTOKEN_DISABLED stub).
- Original bug fixed: DefaultController._sysConfigRepo never assigned in Framework
  (ctor took only IComponentContext) -> DI ctor now.
- MRQC-specific: Web Infrastructure has NO SysConfigRepo, so the API carries its
  own minimal ISysConfigRepo + SysConfigEntity (Services/SysConfigRepo.cs,
  [SugarTable("Sys_Config")], via Web's MrqcDb -> MrQcDb = Newtouch_MRQC);
  Web Infrastructure NOT modified. Entity columns verified against
  INFORMATION_SCHEMA.COLUMNS of Newtouch_MRQC.dbo.Sys_Config (same 12 columns
  as MRMS). Demo DB has 0 Sys_Config rows, so Query data [] is correct.
- Verification matrix (live, temp Redis token created then DELETEd, EXISTS 0 after):
  - POST api/Default/Query, no token, valid Timestamp -> 10000, data[] (original
    Query has no [DefaultAuthorize], so no-token success is faithful, same as
    MRMS.API/OR.API Query).
  - POST api/Default/Query, valid temp token (TokenType MRQC) -> 10000, data[].
  - Missing Timestamp -> TIMESTAMP_IS_REQUIRED; fake token -> TOKEN_IS_EXPIRED;
    token without TokenType -> TOKEN_TYPE_IS_REQUIRED; mismatched TokenType ->
    TOKEN_TYPE_NOT_MATCH; jsonpCallback -> cb(...) wrapped, code 10000.
  - POST api/Auth/GetToken -> 40004 GETTOKEN_DISABLED, no token in data (stub by
    design: original minted tokens for "?" identities without credential check).
  - GET api/Default/5 -> NOT_IMPLEMENTED (original scaffold returned "value" only).
  - Empty body -> HTTP 400 from [ApiController] model validation before the action
    (same as MRMS.API/OR.API).
  - MRQC Web 5220 login still 200 after the port (not broken).
- recovery.sh section 24 added (syntax OK, final ss list includes 5320); instance
  started BY recovery.sh itself (durable path, PID 7897). Watchdog entry for 5320
  still owed to parent (cron not edited by this agent).
  README: Newtouch.MRQC.API/README-MRQC-API.md.
- Remaining APIs after this: NONE — all module APIs ported (OR 5331, MRMS 5332,
  HOSP 5333, CIS 5334, EMR 5335, Herp 5336, PDS 5337, MRQC 5320). Center hosts
  8015/8016 still not ported.

## Center hosts 8015/8016 — RUN DONE 2026-10-27 ~01:00 ICT
- Both hosts ALREADY .NET 6 (no port needed): solution copied from run copy to
  ~/workspace/openhis-core/Newtouch.HIS.Center/ (translation checkout + run copy untouched),
  with sibling copies of Newtouch.HIS.Lib (13 MB) and his-dll-common/Net6 + DRG (14.8 MB).
  Config fixes in the copy: Top_OrganizeId -> demo org 6d5752a7-234a-403e-aa1c-df8b45d3469f
  (both hosts), AuthCenter SSOAuthApp BaseAPI domain 8017 -> 7001, API.Manage RedisConfig
  typo ""Connection" -> "Connection" (startup-breaking in original). SiteSettAPIHost LEFT at
  localhost:7002 per parent decision (do not invent a Sett API host); SiteCisAPIHost is dead
  config (CisHttpUrl defined, never used). Release build 0 errors.
- AuthCenter 8015 (PID 11741, run-authcenter.sh) + WebAPI.Manage 8016 (PID 11743,
  run-apimanage.sh), proxy env UNSET, DOTNET_SYSTEM_NET_DISABLEIPV6=true, explicit
  ASPNETCORE_URLS. Verified live, all post-sweep Vietnamese build:
  - POST api/Auth/GetAppToken MRQC -> API.Manage: 10000 + real JWT (token not reprinted).
    Unknown app NOPE -> 40004 "Không tìm thấy ứng dụng xin cấp quyền NOPE".
    CIS -> API.Manage 10000; EMR -> API.Manage 10000 (EMR family token-only, proven).
  - Bearer POST api/MedicalBl/MedicalCenterPatInfo (MRQC family): envelope
    "Yêu cầu thành công", 10000 + real patient rows (DeptCode KS01 / DeptName Khoa Noi;
    demo-data Chinese names like Xm are DB keeps).
  - Bearer POST api/MsgQueue/NoticeQuery: 40004 "Không tìm thấy dữ liệu khả dụng"
    (faithful: demo DB has no notices, not a connection error).
  - No-token POST -> HttpStatus 401 "Xác thực token thất bại:" (Bearer gating works).
  - Bearer POST api/RemoteTreated/DoctorMeetingApply (CIS family), fake applyId + valid
    usercode/username: 40004 "Thông tin đăng ký khám chữa bệnh bất thường, vui lòng gửi
    lại đăng ký" (validation passed, unknown applyId fails before any meeting creation;
    no write executed). Earlier validation-gate message "Mã nhân viên không được để trống"
    also verified surfacing live.
  - CIS Clinic/GetClinicToken deliberately NOT live-tested (calls external vendor clinic
    API via OuterClinicSystemAPIHost, not a local flow).
  - MRQC Web 5220 + CIS Web 5234 login pages still 200 (not broken).
- Vietnamese sweep: 218 MSG-only runs translated (exact set, /tmp/center-vi.tsv); keeps =
  612 Description data-vocabulary runs (proven shared vocabulary: 10 DESC runs precisely
  compared by consumer module code), CMP/SQL/LOG/OTHER runs, comments. Sweep apply bug found
  and fixed: pass-1 keep-masking broke messages containing short keep fragments
  (e.g. 应用 inside 应用未注册), leaving 509 mixed Chinese/Vietnamese lines; pass 2
  (maximal-sequence applier) + 54 full-literal repairs + 1 order-collision fix brought
  mixed code literals to 0 (comment mixed lines left, not user-visible). Envelope messages
  in the COPIED Lib only: 请求成功 -> "Yêu cầu thành công" (AppResultFilter x7),
  token校验失败: -> "Xác thực token thất bại:" (ServiceExtensions). LESSON: host csprojs
  reference Lib.Base via HintPath to his-dll-common/Net6, NOT ProjectReference — a Lib
  source edit alone does NOT refresh host bins; the built Lib.Base.dll must be copied into
  openhis-core/his-dll-common/Net6 first, then rebuilt (no module app references that
  folder; only Lib-internal projects do, and no running module app uses its output).
- Residual gaps (reported, not fixed by invention):
  - Sett-API host: 8016 Sett-area actions (Booking, OutpatientService, Patient,
    InHosPatientService) + OrderCenter PayOrder proxy to SiteSettAPIHost (localhost:7002,
    nonexistent). CIS api/Outpatient/OutpatientRegist via 8016 OutpatientService proxies
    to SettHttpUrl + BookingMethodRoute, so that ONE CIS flow still fails with an explicit
    error even with 8016 up. All other 8016 caller flows (MRQC MedicalBl/DoctorsAdvice/
    MsgQueue/ApplyHandle, CIS ExtClinic/RemoteTreated/HisJob) are served from 8016's own DBs.
  - DRG: 3 endpoints route + validate (empty Data -> 20000 field validation) but full
    engine execution NOT live-tested (implementation is binary-only in
    NewtouchHIS.Lib.Services.dll; no-write policy). If the engine fails on a real record,
    only those 3 endpoints fail explicitly.
  - Framework DataAnnotations messages without custom ErrorMessage stay English
    (e.g. "The username field is required.") — framework text, not app Chinese.
- recovery.sh sections 25 (8015) + 26 (8016) added, syntax OK, final ss list includes
  8015/8016. Instances started via launchers (PIDs 11741/11743). Watchdog entries for
  8015/8016 still owed to parent (cron not edited by this agent).
- Remaining after this: NONE of the planned module/center hosts — full stack now:
  base (1433/6379/41142/7001/5208) + Web (5220/5221/5231/5232/5233/5234/5235/5236/5237/5238)
  + APIs (5320/5331/5332/5333/5334/5335/5336/5337) + center (8015/8016).

- Role screenshots 2026-10-27 (admin 000000 only): 1009 PNGs in shots-roles/ across all 11 Web apps; full coverage table in roles-shots-progress.md (per-app progress files alongside).
