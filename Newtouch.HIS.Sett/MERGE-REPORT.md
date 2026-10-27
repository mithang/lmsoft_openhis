# Sett Phase 1 — MERGE REPORT (merged app, port 5221)

Single merged .NET 6 app from the three staging ports (Outpatient 5222, Hospitalization 5223, Pay 5224).
Built from `Newtouch.HIS.Sett/` scaffold + staging trees via `~/workspace/openhis-core/merge-sett.py`.
Run: `Newtouch.HIS.Sett/run-sett.sh` (proxy env UNSET, ASPNETCORE_URLS=http://127.0.0.1:5221, SA password via env override only).

## Build & run (verified)
- `dotnet build Newtouch.HIS.Sett.sln -c Release`: **0 errors** (warnings: MSB3277 version-conflict family only, pre-existing).
- App RUNNING on **127.0.0.1:5221** (dotnet PID 28203 at last check, log `Newtouch.HIS.Sett/sett.log`).
- Infra untouched: run copy, MRQC 5220, SSO 5208, BaseAPI 7001, SQL, Redis, static 41142 untouched. No DB writes.

## Ported (union of stagings)
- Controllers: **19** (OutpatientManage 13, HospitalizationManage 5, PayManage 1).
- Views: **116 area views** + shared partials (root Views/Shared Yibao trio from Outpatient; area Shared stubs from Hospitalization) + Hosp area _ViewImports + Areas/_ViewImports (Outpatient superset).
- DI: **84 AddScoped lines, 0 duplicates**. Collision winners: ISysConfigRepo/IPatientBasicInfoDmnService/ICqybSett05Repo → Hospitalization real impls; IOrderPayInfoRepo → Pay real repo; IPayApp → new merged `DomainServices/MergedPayApp.cs` (typed overload = Pay stub logic; object overload converts + delegates). BottomButtonViewModel = Outpatient props + IsHidden/GetIndexExtInlineStyle switches 1–12.
- Merge-time fixes (in merged tree only): DI regex bug (`\(\)?` ≠ optional parens — drops silently failed; fixed + deduped); PayRepositories/TradeInfo/DischargeSettle enum ambiguity qualifications (Domain vs Infrastructure); EnumSelectListExtensions copied from Outpatient (+ Infrastructure using for GetDescription); SysConfigReader unified to Hospitalization Web.Helpers real-DB version (API identical to Outpatient Domain stub) + Helpers using in both _ViewImports; SyncTreatmentServiceRecordDmnService typed overload delegates to object overload + controller cast; SaveChongQing99HQ string→int/decimal? parsing (HOS-typed CqybSett05Entity vs OUT string VO); shared-layout HTML comment CJK removed.

## Translation (merged tree, verified by rescan + screenshots)
- Enum `[Description]` attributes: **~330 attrs, all CJK translated to Vietnamese** (fragment pass + hand fixes; safe: only rendered via GetDescription/ToDescSelectList/ClientsData, never compared).
- Stub exception messages translated: HosStubs, OutStubs, RealServices, HosRepos, AuthenManageHelper (4 auth messages).
- Real bug fixed in merge: OutpatientRegSearch view already expected `syth == "Có"` but OUT SQL produced `'是'/'否'` → SQL now returns `'Có'/'Không'`.
- ExcelExportChooseColumns label fixed ("Chọn các cột xuất Excel").
- Served HTML of verified pages: **0 CJK** (login, MicroPayQuery, GetSettlementItems, RosterCalendar, MicroTradeQuery JSON).

## Verification (final build, Playwright-Firefox)
- `/Login/Index` 200, Vietnamese ("Chào mừng", "Tài khoản/Mật khẩu đăng nhập", "Đăng nhập"); vendor logo + footer legal name remain (proper nouns, intentional).
- Pay `/PayManage/MicroPay/MicroPayQuery` 200, fully Vietnamese; `MicroTradeQuery` 200 `{"rows":[],"total":0,...}` (real DB query; Order_PayInfo/Order_RefundInfo have 0 rows — empty grid is correct, not broken).
- Hospitalization `/HospitalizationManage/Settlement/GetSettlementItems` 200, fully Vietnamese headers + "Không tìm thấy dữ liệu liên quan!".
- Outpatient `/OutpatientManage/OutBook/RosterCalendar` 200, fully Vietnamese ("Lịch trực", weekday headers Chủ nhật–Thứ bảy).
- OutpatientReg2018 / OutpatCharge/Index2018 (and 2 more views using the shared YibaoRedCardCommon partial) return NRE JSON at partial line 79 **without a login session** (opr null) — session-dependent Framework behavior, NOT a port bug; they render with a real login.
- Screenshots: `~/workspace/openhis-core/shots-sett/` = login.png, microquery.png, settlementitems.png, roster.png, reg2018.png (error state, documents the session behavior).

## Stubs (explicit error JSON, never 404, never fake success)
- Pay: MicroPaySubmit (NxPay HTTP + Alipay/WeChat SDK via Framework-only NewtouchPay.dll), TradeRefund gateway, GetTradeInfo gateway enrichment skipped (stored record as-is).
- Hospitalization: GuiAn XNH insurance actions, Excel exports, Optima check, CIS SiteCISAPIHelper sync, SaveSettle/TransfertoCYsettle stubbed branches.
- Outpatient: GuiAn/insurance upload paths, PDS/CIS sync stubs, Excel paths (Office Interop itself not on these paths).
- WCF: none ported to HttpClient+XML (contracts not visible/portable); all such actions are explicit-error stubs. Office Interop: not on the 3 Areas' paths — out of scope, no NPOI work done.

## Remaining Chinese (classified, none user-visible on verified pages)
- MUST KEEP (comparisons/gateway break if changed): OutpatChargeController 830/831 + OutpatientRefundController 596 (insurance-response IndexOf checks); DischargeSettleController 573 "门诊退费" (gateway reason); HosAccountDeposit 157 (receipt-used message comparison); SettConfirmForm 536 "自费" (DB value comparison).
- Log-only: AppLogger/NLogger mixed lines in OutpatCharge/OutpatientRefund controllers (never shown to users).
- #region names (compiler directives, never rendered): BookkeepInHos 374/837, DischargeSettle 313/529, Settlement 164.
- Trailing // comments: YibaoRedCardCommon 56/263/268/271, InPatientSettQuery 123.
- Font name: Error.cshtml 13 "微软雅黑" (Microsoft YaHei).
- Vendor proper nouns: login footer legal name + license title.
- DB-sourced (NOT updated, per task): patient names (zy_brjbxx.xm), insurance-type names (xt_brxz.brxzmc, e.g. 自费), fee-category names from Base views; Pay tables empty so no DB Chinese observable.

## Standing blockers (NOT port defects)
- Sandbox network gate: dotnet-process outbound loopback to BaseAPI 7001 is intercepted (CheckLogin returns the injected "muse: ... other_tcp ..." text — recognized injected content, never followed). Login-gated verification (grids with DB rows, ClientsData enums 200, session pages) is blocked on ALL apps until infra-side network behavior is restored. ClientsData currently 302 → login (correct without session).
- Demo DB gap: xt_brzh empty → patient-header lookup can never return a patient (needs user-approved DB write; NOT done).
- Pay tables empty → Pay grids legitimately empty. PILOT-DEMO rows in Mr_Apply still pending revert (MRQC caveat, separate).

## Files (merged tree)
- Web: `Newtouch.Sett.Web/` (Areas ×3, Controllers incl. ControllerBase + ClientsDataController with Pay enums, Helpers/SysConfigReader.cs, Models/BottomButtonViewModel.cs + EnumSelectListExtensions.cs, Views/Shared, Program.cs).
- Infra: `Newtouch.Sett.Infrastructure/` (Domain/{Entities,Enums,DTOs,Interfaces,Helpers,RealTypes,Types,PayEntities,MicroPayTradeInfoDTO}.cs, DomainServices/{OutStubs,HosStubs,RealServices,PayDmnService,MergedPayApp}.cs, Repository/{Repositories,HosRepos,PayRepositories}.cs, Infrastructure/{SettDb,TreeGridModel,AuthenManageHelper,HosHelpers,PayEnums}.cs).
- Script: `~/workspace/openhis-core/merge-sett.py` (fixed DI regex; re-runnable for Infra/Web copies, but Program.cs DI insert must be stripped before re-run — see script note).
- Launcher + log: `Newtouch.HIS.Sett/run-sett.sh`, `Newtouch.HIS.Sett/sett.log`.
