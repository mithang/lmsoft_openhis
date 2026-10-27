# Base/HOSP translation sweep — progress (retry agent, 2026-10-26)

## Batch 0 — inventory (done)
- Refined scan (comments stripped, wwwroot excluded): HOSP.Web .cshtml 666 runs was
  comment-inflated; true user-visible code remainder = 51 distinct runs / 68 occ.
- catalogl.js (250 runs, drug-catalog field labels): ZERO references in any .cshtml/.cs
  → dead static file, excluded from scope (exception, not user-visible).
- Maps 1–8 (1093 entries) already applied except 58+16 keys; applying them changes
  little. New map needed for the 51 runs.
- KEY CORRECTION to parent's 20:24 verdict: HOSP Organize Index.cshtml is ALREADY
  fully Vietnamese in code (Mã quốc gia, Phân loại cơ sở, Tên/Mã vùng BHYT) — the
  organize.png Chinese headers came from a stale pre-translation app instance
  (shot 20:03 ICT, app restarted 20:21 ICT). Fresh screenshots after rebuild will
  confirm. Base Web home sidebar/tiles/chart titles have NO Chinese literals
  anywhere in Base code (broad grep empty) and Base DB Sys_Module names ARE Chinese
  (verified by read-only sqlcmd) → home chrome is DB-driven, NOT a code gap;
  goes to the DB-pass exception list, not code translation.

## Batch 1 — code map (done)
- New map vi-map-9.json (37 entries) applied: 118 replacements in 31 files
  (HOSP.Web 91, Base.Web 10, Infrastructure 17).
- Targeted fixes beyond the map (applier skip-rules): bracketed confirm strings
  in User/Index (whole-run regex swallowed 【】), Department/Index button skipped
  by over-broad SQL filter (id "YBKS-update" contains "update"), Notice.cshtml
  nickname check restored to compare against the already-Vietnamese default
  value (was comparing to stale Chinese placeholder — real bug fix), Application
  Form example-URL placeholder 如→VD in both webs.
- Final code remainder: 14 distinct runs / 18 occ, ALL classified keeps:
  vendor proper nouns + login license line (SSO precedent), font name, SQL
  value, insurance data filename parts, insurance error-message comparison
  operand, [Obsolete] warning text, RDLC report-template content.
- wwwroot untouched per established rule: newtouch-clientdata.js dict keys +
  medicine.js 1 run (server-dataItems keys, previous passes also excluded
  wwwroot); catalogl.js unreferenced dead file (see Batch 0).

## Batch 2 — build + restart + verify (done)
- Found + fixed a build break from the earlier sweep: EnumSffjdlmc members had
  been translated to Vietnamese-with-spaces (invalid C# identifiers). Restored
  original Chinese member names (identifiers, not user-visible); Vietnamese
  Description attributes kept as display text. No other enum damage found.
- Rebuilt solution Release: 0 errors, 11 warnings (after stopping old PIDs,
  which held bin files locked — MSB3021, expected).
- Restarted via run-base-hosp.sh / run-base-web.sh / run-base-api.sh: new PIDs
  51662/51663/51664; 5233 + 5238 login 200; API Query (ISO Timestamp + crkbz=0)
  → code 10000 + real rows.
- Login success verified via POST CheckLogin (cookie method) on both webs.

## Batch 3 — fresh screenshots (done)
- Form-login shot script proved flaky (session pages redirected to Login);
  switched to cookie-based login (shot-base-v2c.py): all 17 session pages
  saved at real URLs with -v2 names (9 HOSP + 8 Base Web).
- Viewed: organize-v2.png (fully Vietnamese headers — old shot was stale),
  baseweb-home-v2.png (sidebar, tiles, stats titles all Vietnamese — visible
  home chrome is NOT DB-bound after all; only vendor logo/footer proper nouns
  remain), department-v2.png (fully Vietnamese chrome + real data rows).
- CORRECTION to Batch 0 note: Base DB Sys_Module names are Chinese, but the
  served home chrome renders Vietnamese — menu source is evidently not that
  table (likely Union Sys_Module, translated in the earlier DB pass). Deeper
  menu items remain a DB-pass watch item only if spotted elsewhere.
