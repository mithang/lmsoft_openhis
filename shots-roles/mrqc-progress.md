# MRQC Web screenshots — progress (2026-10-27, ~09:10–09:20 ICT)

App: MRQC Web on http://127.0.0.1:5220, account 000000 (HospAdministrator).
Method: local Playwright 1.64 + Firefox (headless), viewport 1366x900 ONLY (no full_page).
Login: POST /Login/CheckLogin with {username: "000000", password: md5("000000")} via curl,
cookies reused in the Playwright context. Scripts: /tmp/mrqc_shot.py, /tmp/mrqc_retry.py
(own scripts; ~/workspace/shot/shot_ff.py untouched, SSO-specific).
Restarts: NONE. MRQC was already UP (PID 4217, Debug build) and stayed UP throughout.
No DB writes (SELECT only), no git operations, no other apps touched.

## Menu enumeration (two ways, deduplicated)

(1) Rendered home page after login (/Home/Index) — MRQC home is a plain pilot link list,
NOT a sidebar menu. Links found (5):
- /QualityControlManage/Apply/Index
- /QualityControlManage/MedicalCenter/Index
- /QualityControlManage/SysItem/Index
- /SystemManage/Common/GetStaffListByDutyCode?dutyCode= (JSON data endpoint, not a page)
- /Login/OutLogin (logout, not a page — not shot)

(2) Sys_Module rows in demo DB Newtouch_MRQC (9 rows, 7 with URLs):
- /QualityControlManage/MedicalCenter/Index (质控中心)
- /QualityControlManage/MedicalCenter/MessageQuery (质控消息查询)
- /QualityControlManage/SysItem/Index (质控项目维护)
- /QualityControlManage/Apply/Index (质控病历申请)
- /SystemManage/SysModule/Index, /SystemManage/SysUser/Index, /SystemManage/SysRole/Index
  (see FAILED below — no such controllers in the MRQC port)
- 2 parent rows with no URL (系统管理, 质控管理) — not pages.

Union of distinct page URLs: 11 (home + login + 4 QC menu pages + 3 SystemManage +
MessageBox + SysItem/Form + 3 MedicalCenter companions). All attempted.

## SHOT (11 files, all NEW in shots-roles/)

| File | URL | HTTP | Notes |
|---|---|---|---|
| mrqc-login.png | /Login/Index (logged out, shot FIRST) | 200 | Login form rendered |
| mrqc-home.png | /Home/Index | 200 | Pilot link list, fully Vietnamese |
| mrqc-medicalcenter.png | /QualityControlManage/MedicalCenter/Index | 200 | VI chrome; grid empty (see caveat) |
| mrqc-messagequery.png | /QualityControlManage/MedicalCenter/MessageQuery | 200 | VI filter + grid headers |
| mrqc-preview.png | /QualityControlManage/MedicalCenter/PreView | 200 | BLANK by design — see note |
| mrqc-qcmessagemanage.png | /QualityControlManage/MedicalCenter/QcMessageManage | 200 | VI send/query forms |
| mrqc-scorefrom.png | /QualityControlManage/MedicalCenter/ScoreFrom | 200 | VI scoring grid |
| mrqc-sysitem.png | /QualityControlManage/SysItem/Index | 200 | VI grid; rows carry DB-sourced Chinese item names (医嘱单, 护理文书…) — DB/enum pass, not view text |
| mrqc-sysitem-form.png | /QualityControlManage/SysItem/Form (no params) | 200 | VI blank form (form companion) |
| mrqc-apply.png | /QualityControlManage/Apply/Index | 200 | VI grid with 3 real PILOT-DEMO rows |
| mrqc-messagebox.png | /Home/MessageBox | 200 | VI stub notice: "Hộp tin nhắn (trang tạm thời của bản thử nghiệm)." — COVERED as stub |

Shot count: 11. Viewed by me (render confirmed, not blank/error): mrqc-home.png,
mrqc-medicalcenter.png, mrqc-apply.png (all Vietnamese, real content); mrqc-preview.png
viewed and confirmed blank (see note). Requirement ≥2 viewed: SATISFIED (4 viewed).

## FAILED with reason (3) — NOT shot, no image possible

All three return HTTP 404 with EMPTY body (Content-Length: 0) even authenticated
(verified via curl with login cookies). Firefox reports this as
NS_ERROR_NET_EMPTY_RESPONSE; retried twice with networkidle AND domcontentloaded —
same result. Cause: the MRQC port's SystemManage area contains only CommonController;
there are NO SysModule/SysUser/SysRole controllers or views in the port. Same class
as the MRMS precedent: these Sys* pages are served by SSO, not by the module app.

- /SystemManage/SysModule/Index — 404, empty body
- /SystemManage/SysUser/Index — 404, empty body
- /SystemManage/SysRole/Index — 404, empty body

## NEEDS PARAMETER

None. Every MRQC view action is parameter-free (verified in controller sources).
Two adjacent notes (not needs-parameter entries):
- PreView standalone renders blank (HTTP 200, empty body text): the view only outputs
  @Html.Raw(ViewBag.WriterControlHtml), which is unset on a direct GET — it is the
  DCSoft document-preview host and only shows content with a document context.
  Shot saved as mrqc-preview.png; flagged here rather than counted as a full page.
- MoreHandleDCWriterServicePage is NOT a view: it returns an explicit VI JSON error
  (HTTP 200, state=error): "Trình soạn thảo tài liệu DCSoft.Writer tạm thời chưa
  được hỗ trợ trong phiên bản .NET Core" — DCSoft stub, by design.

## Caveats observed (not shot failures)

- MedicalCenter GetPatList throws NullReferenceException server-side (mrqc.log,
  MedicalCenterController.cs:line 88) when the page's grid loads, so the patient
  grid stays empty even though the page chrome renders. The page itself is HTTP 200
  and shot; the grid-data error is a live bug for the parent to triage.
- SysItem grid item names are Chinese from the DB (see mrqc-sysitem.png note) —
  belongs to the open DB/enum translation decision, not to view translation.
- Home link /SystemManage/Common/GetStaffListByDutyCode is a JSON endpoint, not a
  page — not shot, correctly excluded.
- xzadmin / admin never attempted (per brief: unknown password / no role).

## Coverage verdict

Every distinct MRQC page that exists in the port is shot (11/11 existing pages),
plus the login page. The only DB menu URLs not shot are the 3 SystemManage URLs
that 404 in this app (SSO-served). No screen missing within the MRQC Web app itself.

## FIXED 2026-10-27: MedicalCenter GetPatList NullReferenceException (line 88)

- Root cause (proven live, not guessed): the page's grid loads with an empty
  search box, which model binding turns into srz = null (and brbz = null when
  no radio value is sent). Center API 8016 validates brbz/srz as required:
  null -> code 20000, BusData.data = null (verified by direct 8016 call and by
  a temporary debug capture of MRQC's exact outbound request/response).
  The old line `apiResp.BusData.data.ToString()` then threw NRE, so the grid
  stayed empty with a server error (mrqc.log entries 2026-10-27 10:24-10:25 ICT).
  A second null source exists on the same line: BusData = null when the app
  token is rejected (8016 returns HttpStatus 401 with BusData null).
- Fix (MedicalCenterController.GetPatList only, no other action touched):
  (1) brbz/srz null -> "" before calling the center ("" means "no text
  filter" and the center accepts it: verified brbz/srz "" -> 10000 + rows);
  (2) if apiResp/BusData/data is still null, return an empty list instead of
  throwing. No patient data invented, no business logic changed.
- run-mrqc.sh updated to the Release DLL + explicit ASPNETCORE_URLS
  (http://127.0.0.1:5220) + DOTNET_SYSTEM_NET_DISABLEIPV6=true, so a future
  recovery.sh restart also runs the fixed build (it previously ran Debug).
- Live verification (000000 only): MRQC Web Release PID 15933, port 5220 -> 200,
  login -> Home/Index. GetPatList with null filters (the exact old NRE case)
  now -> HTTP 200 + the real demo patient row (Khoa Noi, Zyh 01000) for a wide
  date range; same for valid filters. Default grid load (2026-08-01 -> now)
  -> HTTP 200 + [] which is genuinely correct: the only demo patient was
  admitted 2024-10-16, outside the default window. No new NRE in mrqc.log.
- Re-shot (viewport only, new file, old shots untouched):
  mrqc-medicalcenter-fixed.png — viewed by the fixer: fully Vietnamese page,
  filter panel + grid headers + patient-info tabs render, grid body empty for
  the default date range, no error dialog.
- Residual (not fixed, out of this task's scope): sibling actions
  GetMedicalCenterDiag / GetMedicalCenterBlTree use the same unguarded
  `apiResp.BusData.data.ToString()` pattern and would NRE the same way if the
  center ever returned BusData/data null for them; they currently return real
  data (verified live: Diag + BlTree for zyh 01000 -> 10000 rows). No commit,
  no push, no DB writes made by this fix.

## FOLLOW-UP 2026-10-27 (~11:15 ICT): GetPatList re-verified + Diag/BlTree hardened

- GetPatList re-verified live on the pre-restart build (000000 only, login
  200 "Đăng nhập thành công"): null-filter case (brbz/srz omitted, wide range
  2024-01-01 -> 2026-10-27) -> HTTP 200 + exactly 1 real demo patient row
  (Khoa Noi, Zyh 01000); mrqc.log had 0 NullReferenceException entries.
  mrqc-medicalcenter-fixed.png viewed again by this worker: fully Vietnamese
  page — filter panel ("Bệnh nhân đang nằm viện", "Từ ngày này",
  "Đến ngày này", "Họ tên / Số nhập viện"), patient-info panel
  ("Thông tin bệnh nhân", "Họ tên:", "Khoa:", "Số giường:"), tabs
  ("Chẩn đoán", "Hồ sơ", "Y lệnh", "Báo cáo xét nghiệm", "Báo cáo kiểm tra")
  and grid headers render; grid body empty for the default date range,
  no error dialog. Matches the fixer's description.
- Sibling hardening DONE (same file, same guard pattern as GetPatList, no
  other logic changed, no data invented): GetMedicalCenterDiag and
  GetMedicalCenterBlTree both used the identical unguarded
  `apiResp.BusData.data.ToString()` line. Both now return an empty list
  (Diag) / empty tree (BlTree) when apiResp/BusData/data is null, with a
  `??` fallback if deserialization itself returns null. BlTree's existing
  `data.Count` loop is unchanged and handles the empty list.
- Build: dotnet build -c Release, 0 errors (14 pre-existing warnings only).
  Restart (MRQC ONLY, no full recovery.sh run): killed exact old PID 15933,
  started via run-mrqc.sh (Release DLL, ASPNETCORE_URLS
  http://127.0.0.1:5220, DOTNET_SYSTEM_NET_DISABLEIPV6=true, proxy env
  unset) -> new PID 19171, port 5220 listening.
- Live verification after restart (000000 only): login page 200, CheckLogin
  200 "Đăng nhập thành công", GetPatList null filters -> 200 + the same
  1 real patient row (01000), Diag zyh 01000 -> 200 + real diag row
  (Zdmc DB-sourced, as before), BlTree zyh 01000 -> 200 + real tree rows.
  Extra no-data probes: Diag zyh 99999ZZZ -> 200 + [] (guard path exercised,
  no error); BlTree zyh 99999ZZZ -> 200 + template tree rows (center
  returns the template tree regardless of zyh — unchanged center behavior,
  not a guard failure). mrqc.log: 0 NullReferenceException after restart.
- No new screenshot: Diag/BlTree are API-only actions (return JSON, no
  view), so no -fixed image applies; the MedicalCenter page shot
  mrqc-medicalcenter-fixed.png above already covers the UI. Residual note
  from the previous fix is now CLOSED for these two actions. No commit,
  no push, no DB writes; no other app touched.

## Soát tiếng Trung MRQC — 2026-10-27 (worker quét, chỉ sửa text; không build, không chạy app, không commit, không ghi DB)
- Phạm vi: Newtouch.MRQC.Web, Newtouch.MRQC.Infrastructure, Newtouch.MRQC.API. Snapshot trước sửa: /tmp/mrqc-cjk-before.txt; backup 31 file đã sửa: /tmp/mrqc-backup/.
- Kết quả: 31 file đổi, 194 dòng đổi (175 dòng chứa CJK + 19 dòng chỉ chuẩn hóa dấu câu fullwidth). Chuỗi hiển thị đã dịch: 62 (EnumDB Description 51 — gồm 1 Description trong code đã comment, AuthenManageHelper 4, ApplyAndCommon 2, nhãn Log4Net 2, nhãn page.common.js 3). Comment đã dịch: 113. Số dòng mỗi file không đổi; mọi dòng đổi đều nằm trong danh sách CJK ban đầu, ngoại trừ 19 dòng dấu câu được liệt kê riêng.
- CJK trước/sau (dòng chứa ký tự CJK): Web 1398 → 1313 (1300 dòng vendor + 13 dòng keep), Infrastructure 89 → 0, API 0 → 0. Sau quét, CJK phi-vendor còn lại duy nhất là 13 dòng keep: AppName trong appsettings.json, tên file ảnh (中间/开始/结束), tên font (宋体/微软雅黑), footer vendor ở Login/Index.
- Keeps chi tiết và dữ liệu tiếng Trung từ database (tên khoa, thuốc, chẩn đoán... do DB/center API trả về, không sửa code để che) xem báo cáo của worker quét gửi điều phối viên.
- Lưu ý: MedicalCenterController còn 8 dòng region/summary tiếng Trung do worker fix trước bỏ sót — đã dịch, không đụng logic fix đó. Không file nào bị bỏ qua vì encoding (toàn bộ scope decode UTF-8, BOM được giữ nguyên).

## PARITY AUDIT MRQC — 2026-10-27 (~12:05 ICT, worker audit, KHÔNG sửa code, KHÔNG commit, KHÔNG ghi DB)
- Phạm vi đối chiếu trực tiếp: gốc ~/workspace/lmsoft_openhis/Newtouch.HIS.MRQC (Web + Domain + DomainServices + Repository + API + APIRequest + sln 8 project) vs port openhis-core (Web + Infrastructure gộp + API + sln 3 project; Application/DTO/BusinessObjects gốc chỉ chứa file 1.txt placeholder nên gộp không mất gì).
- FIELD: VO/Entity theo từng class (parser ngoặc nhọn, không ước lượng): MrApplyEntity 24/24, QcItemDataEntity 15/15, MedicalApplyVo 21/21, DoctorAdviceRecordVo 21/21, MedicalBlCenterVo 2/2, MedRecordTree 13/13, MedicalHomeDiagVo 7/7, MedicalInspectionExaminationVo 9/9, MsgNoticeGroupVo 1/1, MsgNoticeQueueVo 27/27, MsgSendVo 11/11, PatientDataVo 27/27, DiagDataVo 5/5, BllxmbRecordsVo 1/1, QcBllxMbTreeVo 7/7, ScoreItemVo 8/8, ScoreEntity 8/8, ZkxmEntityVo 10/10; 13 enum EnumDB đủ member 100%. 2 prop "thiếu" duy nhất (MrQcScoreEntity.BlmbId/WritId) vốn đã bị comment-out trong gốc nên không phải gap. API request DTO: AuthGetTokenRequest 0/0, DefaultQueryRequest keyword 1/1, OrgJSONRequestBase OrganizeId 1/1.
- MÀN HÌNH: 9 view nghiệp vụ QC + Login + MessageBox + 8 Shared partial đủ, số dòng view QC giống hệt gốc (159/671/114/69/279/181/99/161), cột lưới/id/js/ajax-url giống hệt (chỉ 1 URL sửa đúng: SysItem tree thêm tiền tố area /QualityControlManage). Controller action: QC 24/24 (Apply 3, MedicalCenter 17, SysItem 7 — kể cả override Index/Form — ClientsData 3, Common 1, Error 3). Thiếu có lý do: 14 view Sys* (SysConfig/SysLog/SysModule/SysRole/SysUser/SysStaff) — gốc cũng KHÔNG có controller cho chúng (chỉ có CommonController), trang Sys* thật do SSO SysManage area phục vụ; khung portal Home (Index/Default/UserOrgChoose + 4 partial) được thay bằng landing page theo đúng kiến trúc port chung (CIS cũng vậy), cổng portal thật là SSO 5208.
- API: api/Auth/GetToken + api/Default/Query + scaffold Get/Post/Put/Delete đủ; envelope 10000/40004; Query no-token + Timestamp hợp lệ -> 10000 data [] (demo DB Sys_Config rỗng), fake token -> TOKEN_IS_EXPIRED, fake token thiếu TokenType -> TOKEN_TYPE_IS_REQUIRED, thiếu Timestamp -> TIMESTAMP_IS_REQUIRED, jsonpCallback bọc đúng, GetToken -> 40004 GETTOKEN_DISABLED (giữ stub theo quyết định đứng, gốc cấp token với identity toàn '?').
- FLOW live (000000, mật khẩu gửi dạng MD5 như JS trang login; login 200 "Đăng nhập thành công"): 11/11 view 200 đã đăng nhập (Home, Apply, SysItem Index/Form, MedicalCenter, ScoreFrom, MessageQuery, QcMessageManage, PreView, MessageBox) + DCSoft stub trả error rõ ràng; JSON đọc: GetBlApplyList (row PILOT-DEMO-003 thật), GetQcBllxMbTree (cây thật, tên DB giữ tiếng Trung do DB trả về), GetZkxmList, GetItemMXData, GetStaffListByDutyCode (2 nhân sự thật), GetPatList (1 BN 01000), Diag, BlTree, GetTyzkxm [], GetDclMsgJson rows [], Getybbxbldata [], ClientsData GetModuleDataJson (menu thật). GetDoctorsAdviceRecord / GetInspectionExaminationRecore / MessageSendQuery trả `null` — ĐÚNG hành vi gốc (gốc cũng return outstr.data.ToJson() khi center không có dữ liệu). Luồng GHI (SubmitForm, ZkDetailDel, SaveScoreDate, ApprovalApply, SendMsg) CHỈ đối chiếu code (mapping field khớp gốc 100%, kể cả SaveScoreDate; SubmitForm port dùng First/TryParse thay FirstOrDefault/Convert — chỉ khác khi dữ liệu hỏng) và KHÔNG chạy live vì gốc cũng không có cổng validation — chạy thật sẽ ghi/xóa DB, vi phạm lệnh cấm ghi.
- Login thiếu 4 action gốc (SSOCallback, ReturnSSOHome, UALogin, UALoginPre) — KHÔNG port: phụ thuộc host SSO/UA ngoài không tồn tại trong triển khai này; UALoginPre còn là đăng nhập không credential (chỉ account+organizeId) nên không được port mù. OutLogin + GetUALoginAddres đã có qua LoginBaseController.
- AppName: port appsettings "MRQC 质控系统" KHÁC system.config gốc ("医疗云质控系统") — báo trung thực, KHÔNG tự đổi: code MRQC Web không hề đọc AppName (grep 0 tham chiếu) nên không ảnh hưởng giao diện; đây là giá trị config diện keep của giai đoạn 2, để điều phối viên quyết.
- Ảnh ghép chuỗi 开始.png/结束.png và nhánh 中间.png không tồn tại: không đụng vào, giữ trung thực với gốc như brief.
- Fix giai đoạn 1 (guard GetPatList/Diag/BlTree) còn nguyên; bản dịch giai đoạn 2 không revert. Kết luận: KHÔNG CÓ FIX — không build lại, không restart, không chụp -fixed (không có trang nào thay đổi), không commit.
