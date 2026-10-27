# CIS follow-up RETRY — progress (2026-10-26 ~22:05 ICT)

## State found on disk (not redone)
- Web 5234 UP (PID 64802, Debug, run-5234.sh), login 200.
- API 5334 UP (PID 66173, Debug, run-5334.sh), 404 on / expected.
- API: 13/13 controllers present + Envelope + TokenStore + Requests + README-CIS-API.md
  with a full live verification matrix claimed by the errored agent (to be re-verified).
- Web sweep artifacts: shots-cis/cis2-*.png, *-fixed.png, *-tier9.png, sweep-scripts/.
- recovery.sh section 20 (5334) EXISTS — to verify, not duplicate.
- Watchdog 5334 entry MISSING per API README — to add.

## Part 1 verification findings (2026-10-26 ~22:10 ICT)
- Comment-stripped CJK scan of Web (.cshtml+.cs): 349 distinct runs / 1015 occ /
  703 lines. Tier scripts translated word-by-word INSIDE long Chinese sentences,
  leaving mixed VN+CN fragments in user-visible modal messages, labels, headers.
  A full-sentence fix pass is required (not another word pass).
- REAL BUG found (print routing, pre-existing from tier translations): print
  buttons now send titles "InPhiếu truyền dịch"/"InĐơn tiêm"/"InĐơn khí dung"
  but MedicalRecord.cshtml:5060-5075 still compares old CN titles
  ("In输液单"/"In注射单"/"In雾化单"/"InXét nghiệm单") -> rpturl stays "" -> broken
  print. Fix: compare against CURRENT button titles. fastPrint controller
  switch is DEAD (call commented out) -> left untouched. "In电子Đơn thuốc"
  branch (tempCode 1220) has no matching button -> left untouched (changing it
  to "InĐơn thuốc" would clash with the 5086 branch).
- Gender policy (Sett-"Có" bug class): KEEP 男/女/混 everywhere data-linked
  (EntryRegistration + InHosRegistration clusters with item.xb comparisons,
  getGender vendor-fn comparison, demo bed rows). Translate ONLY verified
  display-only spots: ClinicApply/Allergy formatters, #xb writes (no reads
  found), BedForm options (values 1/2), bed-card sex vars, chart lblSex,
  server .cs display ternaries (treepat.text etc.).
- KEEPS (classified, do not touch): all ==/Equals comparisons (yzlb 临/长,
  sfyb 否, syncStatus 已完成/己出报告/等待报告, 阳性, 禁食, 已申请, 膳食自备,
  煎服, 总计, 静滴 default, 控制/是 data flags, syyy 其他, 未录入, 动态心电,
  中/西 indexOf, split('卐'), brzt chart code arrays, 卧, 请假), trailing //
  comments, #region, vendor footer + license title, font names, image
  filenames, demo names/rows (张三/李四/王/张 doctors, 心脏超声 demo row,
  QueueInfo placeholder spans), report/document names (djmc, SSRS paths,
  重庆 report URI), data formats (Age "岁0月" in .cs, AdminArea "无", 转诊),
  console.log-only strings, hidden column (会议号).

## CLOSURE 2026-10-26 ~22:35 ICT (retry agent)
- B2 ran: 253 replacements (Areas messages/labels + B1 leftovers). 2 B2 zero-matches fixed
  by hand: InputresultsSkintest modal (actual text had "test da" inserted by a tier
  pass) + DietaryAdvice Form radios (scope bug: dir-prefixed scope never matched the
  bare filename check). Final comment-stripped scan: 240 lines / 364 occ / 136 runs,
  ALL classified keeps (comparisons, #region/trailing comments, vendor footer +
  license, font names, image filenames, demo rows/names, report/document names,
  data formats, chart codes, gender clusters, hidden column, dead fastPrint switch
  + dead "In电子Đơn thuốc" branch + dev path + console.log). 0 user-visible CJK left
  outside keeps.
- Build: first attempt failed ONLY on PDB copy lock (running Web held the PDB);
  killed exact PID 64802, rebuilt: 0 errors (14 warnings). Web restarted via
  run-5234.sh, new PID 72963, login 200.
- Web verify: POST /Login/CheckLogin 000000 + md5sum(000000) -> {"state":"success",
  "message":"Đăng nhập thành công"}; /Home/Index 200 with session, 302 without
  (auth-ON). 0 auth-lift markers (no lift was used). Shots cis3-queueinfo,
  cis3-clinicapply, cis3-inhosquery in shots-cis/ (Playwright+Firefox, form login);
  queueinfo viewed: fully Vietnamese headers, empty grids correct (no demo data).
- API verify (PID 66173 unchanged, code untouched): UA GetUserInfo + temp token +
  Timestamp -> 10000 Account 000000 + real OrganizeId; MzBespeakRegisterQuery +
  temp token -> 10000 data []; PatInfo InpatientBaseInfo + temp token -> 10000
  data null (no matching zyh); PatInfo no token -> TOKEN_IS_REQUIRED + VI msg;
  fake token -> TOKEN_IS_EXPIRED + VI msg; missing Timestamp ->
  TIMESTAMP_IS_REQUIRED; GetToken -> GETTOKEN_DISABLED + VI explanation (stub kept).
  Temp token DELeted both times, EXISTS 0 after each. NOTE: MzBespeakRegister
  no-token returns envelope INTERNAL_SERVER_ERROR (NRE) — DOCUMENTED original
  behavior, not a new bug: original controller has no [DefaultAuthorize] but
  actions read UserIdentity.OrganizeId (original 500s; port returns envelope
  instead of crashing). No fix applied; do not "fix" by adding auth the original
  lacked without a design decision.
- Recovery: section 20 (5334) verified present + guarded; `bash -n` syntax OK.
  Watchdog his-stack-watchdog updated: 5334 added to step-1 port list (verified
  by re-view); schedule unchanged (30m, next ~22:46 ICT).
- CIS follow-up RETRY COMPLETE. No DB writes, no push/merge, no other module touched.

## POST-CLOSURE RESTART 2026-10-26 ~22:41 ICT
- VM/runtime restart wiped ALL services (4th today: no dotnet/sqlservr/redis
  processes, /var/opt/mssql symlink gone, all 19 ports DOWN). Ran recovery.sh
  (exit 0, ~76s): all 19 ports UP again, CIS restarted via launchers from
  sections 19/20 — new PIDs Web 4004, API 4076 (supersede 72963/66173 above).
- Re-verified after recovery: CIS Web login page 200, CheckLogin 000000 ->
  success JSON, API GetToken -> GETTOKEN_DISABLED. Full API matrix was verified
  pre-restart on PID 66173; API code unchanged by the restart, spot-check
  (GetToken stub) confirms the restarted API serves the same build.
