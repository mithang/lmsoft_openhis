# MRQC.API — .NET 6 port (Scope B)

Host: `http://127.0.0.1:5320` (launcher `~/workspace/openhis-core/run-mrqc-api.sh`,
log `mrqc-api.log`, Release build 0 errors). Added to `Newtouch.HIS.MRQC.Core.sln`.

## Correction to the task brief

The brief said MRQC.API "exists only as a skeleton" in `openhis-core`, but no
MRQC.API project existed there at all (only mentions in `final-verification.md`).
This port was therefore built fresh — cloned from the MRMS.API twin (the original
MRQC.API is structurally identical: same 2 controllers, same request DTOs, same
unassigned-repo bug, same `?`-identity GetToken) and adapted to MRQC's infra.

## What was ported (original: `Newtouch.HIS.MRQC/Newtouch.MRQC.API`, Framework 4.6)

- `POST api/Default/Query` — real: returns all `Newtouch_MRQC..Sys_Config` rows
  (no keyword filter, faithful to original). Demo DB has 0 rows, so `data: []`
  is correct, not a failure (same situation as OR.API's empty table).
- `POST api/Auth/GetToken` — explicit-error stub `GETTOKEN_DISABLED`, never
  returns a token. The original minted tokens for `"?"` identities (UserId /
  Account / OrganizeId / TopOrganizeId all `"?"`) with no credential check —
  porting it verbatim would be a security regression. Re-enabling needs a real
  credential check, a user design decision.
- `GET/POST/PUT/DELETE api/Default` scaffolds — `NOT_IMPLEMENTED` stubs
  (original returned only `"value"`/empty, no business logic).

## Envelope (OR.API / CIS.API / MRMS.API / PDS.API precedent)

`code/sub_code/sub_msg/data`, SUCCESS=10000 / FAIL=40004, Newtonsoft indent +
`yyyy-MM-dd HH:mm:ss`, `jsonpCallback` wrapping (original Query takes
`JSONRequestBase`), raw token as Redis key with 20-min renewal.
`[ApiController]` model-state filter kept at default, so an empty body returns
HTTP 400 before the action (same as MRMS.API/OR.API).

## Original bug fixed (same class as MRMS.API)

`DefaultController._sysConfigRepo` was never assigned in Framework (ctor took
only `IComponentContext`) — the port injects a repo through DI, so Query runs
instead of NullReference.

## MRQC-specific adaptation

MRQC Web's Infrastructure has **no** SysConfig repo (only MrApplyRepo /
QcItemDataRepo), so the API carries its own minimal `ISysConfigRepo` +
`SysConfigEntity` (`Services/SysConfigRepo.cs`, `[SugarTable("Sys_Config")]`,
via the Web's `MrqcDb` → `MrQcDb` = Newtouch_MRQC). The Web's Infrastructure
project was NOT modified. The 12 entity columns were verified against
`INFORMATION_SCHEMA.COLUMNS` of `Newtouch_MRQC.dbo.Sys_Config`.

## Known limits (honest, same as siblings)

- DES-encrypted tokens sent by old clients will read as expired: the global
  TokenDecrypt algorithm lives in the binary API.Common DLL with no source, so
  the port uses the raw token as Redis key (see MRMS.API README for the same note).
- No UA GetUserInfo endpoint exists in the original MRQC.API, so TokenStore has
  no UA lookup (unlike CIS.API).
- Write paths: none exist in this API (Query is read-only; scaffolds are stubs).
