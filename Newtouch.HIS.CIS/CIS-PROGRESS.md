# CIS port — progress (2026-10-26)

## DONE
- Infra builds 0 errors (was 732): RepositoryBase/DmnServiceBase/EFDbTransaction
  now expose the full FrameworkBase surface (LINQ IQueryable, FindList/
  FirstOrDefault/QueryWithPage incl. DbParameter[] overloads, Insert incl.
  List<T>, Update/Delete/DetacheEntity, ExecuteSqlCommand incl. no-arg).
- Compat: AppConfig, HttpMethods, RedisHelper, LogCore (+Fatal, string
  overload), SiteAPIRequestHelperBase.Request public + autoAppendToken,
  DefaultResponse superset + generic, AppLogger (static Info, instance
  Warn), Tools.Json, ToInt (string/int?/double/enum), ToBool, MapperTo
  (1-type, 2-type, copy-into incl. List<string> ignoreProps), Clone,
  ToDataTable, DateTime? ToDateString/ToDateTimeString/IsEmpty,
  FailedException (code,msg) + Msg, FailedCodeException.
- Cross-module: SysConfigRepo (Sys_GlobalConfig — VERIFIED EMPTY in demo DB,
  so empty + controller defaults is correct behaviour), SysStaffRepo,
  TTCataloguesComparisonDmnService stub, SysDiagnosisRepo (xt_zd by ICD10),
  SysDepartmentRepo, SysUserDmnService (V_C_Sys_StaffDuty — columns verified
  live: StaffId/StaffName/StaffPY/StaffGh/OrganizeId/DutyCode/DutyName/zt/kflb),
  GetMediUsageList (SysMedicineUsageVEntity), GetMediFormlUsageList
  (zy_ypjxyfdz — 0 rows in demo DB), TSql StockInventory/StockOperate.
- Stubs (explicit, never faked): CMM SOAP 01/02/07/08/09 (WCF), HLYY SOAP,
  BGESB, GuiAn WebServiceClient, SelectSfxmYp (empty list).
- Web skeleton builds 0 errors; Program.cs wired: AppConfig.Init +
  143 AddScoped (0 duplicate interfaces), SignalR hub /signalr.
- DB probe tool: /tmp/dbprobe (System.Data.SqlClient, NOT sqlcmd — sqlcmd
  segfaults with mixed ODBC libs; .sqlserver.env has NO export, use set -a).

## NEXT
1. Port controllers (script port-controllers.py — see plan in session):
   59 files (13 root incl. real ClientsDataController, 49 area); SKIP
   Login/Error/Home skeletons; SKIP App_Start/Areas duplicates.
   Add DI ctors for unassigned readonly service fields (known original
   bug pattern, same as MRMS fix). Hand-fix: InfusionController (QR/
   barcode byte[] path), ClientsDataController (async GetMenuList),
   MedicalRecordController (HlyyProxy try/catch).
2. Port views (434) + layouts + ActiveX notices (4 views).
3. Translation pass (diacritics) + Home area-link corrections.
4. Run 5234 (launcher run-5234.sh + DOTNET_SYSTEM_NET_DISABLEIPV6=true
   + proxy env unset), login verify, 1 real page per Area (7) HTTP 200
   with DB data, Playwright-Firefox shots to shots-cis/.
5. Report. API on 5334 only after Web DoD.

## DONE 2026-10-26 (Web Scope A complete)
- Controllers: 59 ported (10 root + 49 area) + Login/Error/Home skeletons; DI ctors
  generated for 56 controllers with unassigned readonly service fields (original bug).
  Build 0 errors, 209 warnings (mostly CS0105/CS0108).
- Views: 311 cshtml (306 ported + skeletons); layouts adapted (jqGrid vi locale,
  cis-vi.js modal override, ClientsData sync bootstrap); SignalR Core client
  (Chat page ported); ActiveX notices in 4 views (never faked).
- Translation: dict pass 8,352 replacements (304 files) + verified-page iteration 2
  (148 key hits) + title sweep (104 + 23 title lines) + script-label pass (146 hits).
  Login footer restored to original vendor Chinese (proper noun, SSO precedent).
- Verification (temp auth lift, FULLY REVERTED — 0 temp markers remain):
  7/7 areas render real HTML 200: ClinicApply, Medicine, InHosQueryIndex,
  QueueInfo, DispensingQuery, InspectionCategory/Chat, PresTemplate; + root
  PerformIndex, Examination. Screenshots in shots-cis/ (11 PNGs, viewed).
  Auth-ON final verified: login 200, Home 302, protected 302, ajax SESSION_TIMEOUT.
- Login chain verified via wrong-password probe (BaseAPI 7001 -> DB rejects correctly);
  SUCCESS login as 000000 NOT verified (SSO_PASSWORD not in this env; do not brute-force)
  -> open item for parent.
- Persistence: recovery.sh section 19 (5234, guarded) + watchdog check list updated
  (5234 + login HTTP check); recovery.sh syntax OK; 5234 login 200 after edit.
- Running: CIS Web 5234 (PID 46974 at final check), launcher run-5234.sh.
- REMAINING: CIS.API (13 controllers, NewtouchCIS.API) on 5334 NOT ported — needs
  envelope + token handling per OR.API precedent (AuthController likely mints tokens;
  stub as disabled if unvalidated). Best-effort items: GetWardListByStaffGh semantics
  (inferred), SysConfigReader.OrgReportLink stub, SelectSfxmYp empty-list stub,
  WCF/CMM/BGESB/HLYY explicit-error stubs, NPOI replaced by HTML-xls export.
  Remaining Chinese: DB-sourced values (dept/person names, diagnosis, 消化内科/熊甜甜/
  张三/李四), logic comparisons (是/否, 男/女, sqzt), binary Lib login prefix
  (登录失败，请联系管理员:), comments, vendor proper nouns, JS-comment fragments,
  long-tail untranslated labels on non-screenshotted pages.
