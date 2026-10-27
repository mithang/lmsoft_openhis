# Sett Phase 1 — scaffold (from the MRQC pilot pattern)

## Paths
- Main scaffold (DO NOT EDIT after staging copies are made; merge happens here later):
  `~/workspace/openhis-core/Newtouch.HIS.Sett/` (sln),
  `~/workspace/openhis-core/Newtouch.Sett.Web/`,
  `~/workspace/openhis-core/Newtouch.Sett.Infrastructure/`
- Source (READ-ONLY, never modify): `~/workspace/lmsoft_openhis_run/Newtouch.HIS.Sett/`
- Never touch: MRQC app (5220), SSO (5208), BaseAPI (7001), static host (41142),
  Redis, SQL Server schemas, or another agent's staging dir.

## Scaffold state (verified)
- `dotnet build` (Release): 0 errors, 12 warnings (all pre-existing, inherited from
  the MRQC skeleton: MSB3277 version conflicts, CS0114 NotFound hiding, MVC1000
  Partial warnings — harmless, do not chase them).
- Smoke test: app listens on 127.0.0.1:5221, `/Login/Index` HTTP 200,
  `/Home/Index` HTTP 302 (redirect to login when unauthenticated — correct).
- Login works via LoginBaseController -> BaseAPI (7001), same as MRQC.
  Playwright login: fill `#txt_account` / `#txt_password` with 000000 / 000000,
  click `#login_button`, wait for `/Home/Index` (see `~/workspace/openhis-core/shots/shot_apply.py`).

## Run pattern (staging apps; final app uses 5221)
- `cd` into the Web dir first (content root matters for appsettings + wwwroot).
- Source the SA env file and override the connection string via env (NEVER print
  the env file or the password; NEVER write the password into appsettings):
  `source ~/workspace/.sqlserver.env`
  `env -u http_proxy -u https_proxy -u HTTP_PROXY -u HTTPS_PROXY -u ALL_PROXY -u all_proxy ASPNETCORE_ENVIRONMENT=Development BusinessDB__DefaultConn="Data Source=localhost;Initial Catalog={0};User ID=sa;Password=$MSSQL_SA_PASSWORD" ~/.dotnet/dotnet bin/Release/net6.0/Newtouch.Sett.Web.dll`
- .NET 6 SDK at `~/.dotnet` (full path). NuGet restore via the machine proxy.
- Staging ports: OutpatientManage 5222, HospitalizationManage 5223, PayManage 5224.
  Change `UseUrls` in the staging `Program.cs` only.

## CRITICAL — Framework Sett controllers have NO constructors
Framework Sett controllers declare `private readonly I...` fields with no ctor and
no assignments (CS0649 pattern — the fields would be null). DO NOT copy that.
In the Core port, ADD an explicit constructor injecting every service the
controller uses, and register every service in the staging `Program.cs` DI
(`AddScoped<IFoo, Foo>()`). This is a fix, not a behavior change.

## Porting rules
- `System.Web.Mvc` -> `Microsoft.AspNetCore.Mvc`; `JsonResult`/`ActionResult`
  -> `IActionResult` (keep `Content(...)` + `AjaxResult.ToJson()` where the
  Framework code did that; `AjaxResult`/`Pagination` come from Lib.Base).
- Base class: create `Newtouch.Sett.Web/Controllers/ControllerBase.cs` as
  `[HandlerLogin] public abstract class ControllerBase : OrgControllerBase {}`
  (OrgControllerBase from NewtouchHIS.Framework.Web; HandlerLogin from
  Lib.Framework.Attributes). This mirrors Sett's Framework local base.
  If a controller calls `Success(...)`/`Error(...)`, check Framework.Web's
  BaseController/OrgControllerBase members first; otherwise use
  `Content(new AjaxResult { state = ..., message = ..., data = ... }.ToJson())`.
- Autofac -> built-in Microsoft DI. `Global.asax` -> `Program.cs` (area route
  `{area:exists}/{controller=Home}/{action=Index}/{id?}` already configured).
- Data layer: EF6 + FrameworkBase RepositoryBase/DmnServiceBase/
  DefaultDatabaseFactory -> SqlSugar via Lib.DataBaseSvr, exactly like MRQC:
  entities with `[SugarTable]/[SugarColumn]` mapping to REAL table/column names;
  repos extend `RepositoryBase<T>` (Sett DB) / `BaseRepositoryBase<T>` (Base DB)
  / `UnionRepositoryBase<T>` (Union DB) in `Newtouch.Sett.Repository`;
  services implement the SAME interface names the controllers use
  (e.g. `IOutpatientRegApp`, `IOutPatientDmnService`) so controller code
  changes stay minimal. Keep interface names; put interfaces next to their
  implementation in Infra (MRQC kept `Domain/Interfaces.cs`; per-area files
  under `Infrastructure/Domain/IDomainServices/<Area>/` are also fine).
- SqlSugar gotchas (all bit the MRQC pilot):
  - `ToPageList` COUNT breaks if the raw SQL contains ORDER BY: strip ORDER BY
    for the count query (or page in memory only for small result sets).
  - Entity property CLR type MUST match the column type: an nvarchar column
    holding dates (e.g. ApprovalDate) maps to `string`, not `DateTime`.
  - Missing SQL functions (MRQC: `dbo.f_split` did not exist) -> implement the
    split in C#, do not create DB objects (NO schema changes in this task).
- Views: copy the Area's views; rewrite `@using` to `Newtouch.Sett.*` +
  `NewtouchHIS.Lib.Framework.Operator` etc.; keep `Layout` as in the original
  (`~/Views/Shared/_Index.cshtml` / `_Form.cshtml`, already translated in the
  scaffold). All 3 Areas use jqGrid (no bootstrap-table): make sure views load
  `grid.locale-vi.js` (in `wwwroot/js/jqgrid/`, copied from MRQC), NOT any
  zh-CN locale. Modal/dialog chrome is overridden by `wwwroot/js/sett-vi.js`
  (already referenced by both shared layouts) — DO NOT edit the static host.
- Translation (same rules as MRQC): ALL user-visible Chinese strings in ported
  views/controllers -> Vietnamese WITH diacritics. Skip: comments, identifiers,
  URLs, IDs, vendor proper nouns, log-only strings.
- WCF insurance clients (Proxy layer): if an in-scope action calls one, port to
  HttpClient+XML ONLY if the endpoint contract is visible in code/config;
  otherwise return an explicit error JSON (never a 404, never fake success)
  and report it as a stub.
- Office Interop Excel: ONLY if it lies on the Area's code paths -> replace
  with NPOI (already in the repo); otherwise report as out of scope.
- DB-sourced Chinese: list table/column, NO UPDATEs, NO schema changes.
- Verification is READ-ONLY (no DB writes, unlike MRQC's write-cycle check):
  ported Save actions only need to compile; verify read pages with data.

## Staging workflow (per area agent)
1. Copy `Newtouch.Sett.Web/` + `Newtouch.Sett.Infrastructure/` (no bin/obj)
   side-by-side into your staging dir; set the staging port in `Program.cs`.
2. Port your Area (controllers + views + the layer files it calls; if it needs
   a shared Sett service outside the Area, port just that service and report it).
3. `dotnet build -c Release` on the staging Web csproj: 0 errors.
4. Run on the staging port; Playwright-Firefox (venv `~/workspace/shot/venv`)
   login + load >=1 page with DB data; keep the staging app RUNNING for the
   merge step if possible, else record exact curl verification.
5. Report: controllers/views ported + strings translated (counts), files
   created, stubs with reasons, DB-sourced Chinese (table/column), staging
   port, verification URLs + results. Do NOT start other Areas or modules.
