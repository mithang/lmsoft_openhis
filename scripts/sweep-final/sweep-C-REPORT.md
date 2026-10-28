# SWEEP CUỐI — WORKER C: MRMS + OR + EMR (báo cáo kiểm chứng, KHÔNG sửa code)

Ngày: 2026-10-28. Base: nhánh V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core`.
Worker: SWEEP-C. Phạm vi duy nhất: MRMS Web 5232 (+ API 5332), OR Web 5231 (+ API 5331),
EMR Web 5235 (+ API 5335). Các app khác không đụng tới. Không sửa code, không commit gì.
`roles-shots-progress.md` không sửa (thuộc điều phối viên; `git status` cuối vẫn chỉ có
`M roles-shots-progress.md` từ trước + `?? scripts/sweep-final/` untracked).

## §0. BLOCKER — đọc trước: kiểm thử có đăng nhập CHƯA thực hiện được

**Kết quả kiểm chứng có đăng nhập (trang chính sau login, mọi luồng ghi): CHƯA LÀM ĐƯỢC
trong phiên này, và báo cáo này KHÔNG tuyên bố chúng là xong.**

- Biến `SSO_PASSWORD` **không có** trong shell của worker này (chỉ kiểm tra tên biến,
  không in giá trị). Quét env file: `~/workspace/.appsecrets.env` không chứa mật khẩu demo;
  không file nào trong workspace gán `SSO_PASSWORD` (khớp kết luận của 2 báo cáo trước
  trong cùng thư mục này: `redis-stg-rotate-REPORT.md`, `stg-login-fix-REPORT.md`).
- Worker này đã thực hiện **0 lần thử login tài khoản 000000** (cả prod lẫn staging),
  vì lockout thật là 3 lần sai liên tiếp → khóa tài khoản, và nhiệm vụ cấm đoán mò mật khẩu.
- **Mâu thuẫn với tiền đề trong task cần điều phối viên xác nhận:** task giao nói "login
  staging đã kiểm chứng thành công (SSO staging 15208, tài khoản demo 000000)", nhưng
  báo cáo `stg-login-fix-REPORT.md` trên đĩa (cùng ngày 2026-10-28) ghi rõ login staging
  **chưa kiểm chứng xong, 0 lần thử**, chỉ còn chờ điều phối viên cung cấp mật khẩu qua
  kênh đã dùng trước đây. Worker này không tự hòa giải mâu thuẫn đó bằng cách đoán mật
  khẩu; mật khẩu không được chuyển cho worker này qua task, env hay file.
- Hậu quả trực tiếp (ghi đúng, không lấp):
  - Mọi trang sau login, mọi grid/filter/search/sort có dữ liệu, mọi luồng
    thêm/sửa/xóa/lưu: **CHƯA TEST LIVE** trong đợt này (cột "Trạng thái" dưới đây ghi
    `CHƯA TEST (cần login)` ở đúng chỗ, không ghi "đạt").
  - Mọi kiểm thử ghi trên staging: **CHƯA CHẠY** (không có session staging thì không có
    luồng ghi nào để chạy; và worker không ghi gì vào DB demo dưới mọi hình thức —
    tổng số lệnh SQL ghi trong phiên này: 0).
  - Những gì ĐÃ kiểm chứng live trong phiên này: trang login (200 + form + screenshot
    2 viewport), cổng kiểm tra auth không cookie (302 về Login), API validation
    không token / token sai / tài khoản không tồn tại (không tốn lượt lockout),
    staging boot + cổng auth, và kiểm chứng source cho các điểm tồn đọng (§4).
- Cần điều phối viên: cung cấp mật khẩu demo qua kênh đã dùng trước đây, rồi chạy lại
  đúng phần "Nợ lại" ở §6 (1 phiên có login là đủ, không cần chạy lại phần đã đạt ở đây).

## §1. Hiện trạng prod đã kiểm chứng (không đoán PID, đọc từ listener/cmdline)

| App | Cổng | PID | Binary đang chạy | Ghi chú |
|---|---|---|---|---|
| MRMS Web | 5232 | 9609 | `bin/Debug/net6.0/Newtouch.MRMS.Web.dll` | **Debug** — đúng như task lưu ý, không tự đổi launcher |
| MRMS API | 5332 | 9830 | `bin/Release/net6.0/Newtouch.MRMS.API.dll` | Release |
| OR Web | 5231 | 10054 | `bin/Debug/net6.0/Newtouch.OR.Web.dll` | **Debug** — đúng như task lưu ý |
| OR API | 5331 | 10296 | `bin/Debug/net6.0/Newtouch.OR.API.dll` | **Debug** — đúng như task lưu ý |
| EMR Web | 5235 | 10982 | `bin/Release/net6.0/Newtouch.EMR.Web.dll` | Release (đã chuyển Release) |
| EMR API | 5335 | 11206 | `bin/Release/net6.0/Newtouch.EMR.API.dll` | Release |
| SSO prod (đối chứng) | 5208 | 8505 | Release | Chỉ dùng để kiểm tra route Sys* (§4) |
| BaseAPI prod (đối chứng) | 7001 | 13893 | Release | Không đụng |
| SSO staging | 15208 | 5454 | Release (worktree staging) | Đã LISTEN sẵn, không đụng |
| BaseAPI staging | 17001 | 15984 | Release (openhis-core) | Đã LISTEN sẵn, không đụng |
| Redis staging | 6380 | 5401 | redis-server | Đã LISTEN sẵn, không đụng |

Caveat staging: các launcher `run-*-stg.sh` trỏ binary từ worktree `~/workspace/openhis-stg`
(header ghi cùng commit `0a730b1b`, chỉ đổi literal cổng) — tức binary staging **cũ hơn
HEAD `e66c7bda`**. Kết quả staging dưới đây chứng minh cụm staging boot/phục vụ được,
không chứng minh code HEAD trên staging.

## §2. Danh mục chức năng (từ controller source + view) và trạng thái kiểm thử

Quy ước cột "Test": `ĐẠT` = đã kiểm live; `SOURCE` = chỉ kiểm chứng source (chưa test
live); `CHƯA TEST` = cần login, chưa làm được. Không có nhóm nào bị ghi "đạt" khi chưa
qua live test.

### 2a. MRMS Web 5232 (+ API 5332)

Nhóm chức năng (controller → thao tác):

| Nhóm | Controller / action chính | Thêm | Sửa | Xóa | Lấy/lọc/tìm/sắp xếp | Test |
|---|---|---|---|---|---|---|
| Trang đầu bệnh án (MainRecord) | MainRecord: Main/MainPreView/MainEdit/MainPatInfo/MainDiag/MainOplist/MainOpEdit/OutHosPatQuery/MainZyInfo/MainFeeInfo, GetGridList/GetEMRPatGridList/GetFormJson/GetZdGridList/GetOpGridList, SubmitZdList/SubmitOpList/SubmitPatBasic/SubmitMR, guidang/CXguidang/CXGDZT, SettlementQuery, MedicalRecordExportQuery, DiagnosticSave | – | Submit* | – | Get* (grid) | Trang Main + Report/Index: CHƯA TEST live (cần login). guidang/CXguidang (DbNames fix phase 7): CHƯA TEST live. Trang no-cookie: ĐẠT 302 |
| Tiếp nhận / xem bệnh án | Record: Receive, PatList, GetMedRecordTree, PreView, PreViewV2 (stub phase 2), LoadMedicalRecordHtml, LockRecord, updateRecordStu, HandleDCWriterServicePage | – | Lock/update | – | PatList, cây bệnh án | CHƯA TEST live |
| Danh mục khoa bệnh án | MrDept: GetPagintionList, GetDicDeptList, GetFormJson, SubmitForm, SubmitDicDept, DeleteData | SubmitForm | SubmitForm | DeleteData | GetPagintion* | CHƯA TEST live; no-cookie GetPagintionList: ĐẠT 302 |
| Phí bệnh án | MrFee: GetPagintionList, GetFormJson, GetFeeOne, GetFeeTwo, submitForm, DeleteData | submitForm | submitForm | DeleteData | Get* | CHƯA TEST live |
| Chi tiết phí | MrFeeItem: getFeeTree, GetPagintionListById, GetPagintionItem, Save, SavebyHISsfdl, SavebySfxm, DeleteForm | Save* | Save* | DeleteForm | Get* + cây phí | CHƯA TEST live |
| Liên kết phí | MrFeeRel: GetPagintionList, GetFeeSel, GetFormJson, Save | Save | Save | – | Get* | CHƯA TEST live |
| Báo cáo bệnh viện | MrHospitalReport: getOrgId + view Index (SSRS) | – | – | – | – | SOURCE (§4); CHƯA TEST live |
| Báo cáo tử vong / phổ bệnh / nội trú / phẫu thuật / đăng ký mổ / lịch sử / buồng bệnh | DeathReport, DiseaseReport, InHospitalReport (Hospitalizationstatistics, CyvrkyKs, CyvrkyQy, Typesetting, MedicalRecord_Summary), OperationReport, OpRegisterReport, PatientHistory (GetPagintionHospitalList/ZdList/SSList, GetFormJson), WardReport, Report (Index stub phase 2 + HomePageReport + EMRBasyPrintReport, SSRS) | – | – | – | GetPagintion* | SOURCE (§4); Report/Index no-cookie: ĐẠT 302; nội dung báo cáo: CHƯA TEST live |
| Hồ sơ trang đầu (basy) | Mrbasy, Mrbasyss, Mrbasyzd, Mrdicblzd, Mrdicdept, Mrreldept: GetFormJson, SubmitForm, DeleteForm | SubmitForm | SubmitForm | DeleteForm | GetFormJson | CHƯA TEST live |
| Dùng chung hệ thống | SystemManage Common: GetDiagList/GetDiagnosisList/GetOperationList/GetAnesList/GetNotchGradeList/GetCommonList/GetDepartment/GetXtbrxz/GetNationality/GetNationas/GetStaffListByDutyCode/GetHisSfdl; SysConfig TemplateIndex/TemplateForm (notice); SysStaff Selector (notice); SysUser PersonalInfo/Roles/Selector (notice + link SSO); SysModule/SysRole (notice + link SSO); SysLog (notice) | – | – | – | Get* (danh mục) | SOURCE (§4); SysModule/Index no-cookie: ĐẠT 302 |
| Đăng nhập / trang chủ | Login (UALogin), Home (Index/Default/UserOrgChoose/SwithOrg/SyncSysConfigParams) | – | – | – | – | Login: ĐẠT 2 viewport (§3); Home no-cookie: ĐẠT 302 |
| MRMS API | Auth GetToken; Default Query (+ Get/Post/Put/Delete mẫu) | – | – | – | Query | Validation: ĐẠT (§3); Query có token thật: CHƯA TEST |

### 2b. OR Web 5231 (+ API 5331)

| Nhóm | Controller / action chính | Thêm | Sửa | Xóa | Lấy/lọc/tìm/sắp xếp | Test |
|---|---|---|---|---|---|---|
| Đăng ký mổ (Apply) | OpApply: Index/ApplyCard/ApplyIndex/ApplyOPDetail/OpApplyCard, GetPatGridList/GetPatInfo/GetGridListOP/GetFormJson, SubmitForm/SubmitForm1, DeleteApply/DeleteApplyExtend; root ORApplyInfo: Index/GetFormJson/SubmitForm/DeleteForm | SubmitForm* | SubmitForm* | DeleteApply(*) / DeleteForm root | GetPatGridList, GetGridListOP | Area delete: SOURCE (repo call thật); root DeleteForm: SOURCE — lỗi B3 (§5). Live: CHƯA TEST |
| Sắp xếp mổ | OpArrangement: Index/ArrangementDetail/OperationProcDetail, GetGridList, submitForm, getArrangeIdByApplyId/GetArrangeObjByApplyId/GetApplyInfoByKey, DeleteForm; root OROperation/ORArrangement tương tự | submitForm | submitForm | DeleteForm | Get* | CHƯA TEST live |
| Đăng ký sau mổ | OpRegister: Index/PostoperativeReg, GetPagintionListForRegistration, getRegistrationInfo, submitForm, DeleteData; root ORRegistration | submitForm | submitForm | DeleteData | GetPagintion* | CHƯA TEST live |
| Phòng mổ | OpRoom: Index, GetGridList, GetPagintionList, GetFormJson, submitForm, DeleteData; root ORRoom | submitForm | submitForm | DeleteData | Get* | No-cookie Index + GetGridList: ĐẠT 302; còn lại CHƯA TEST |
| Nhân sự mổ / gây mê | OPStaff, OPAnesthesia: Index, GetGridList, GetPagintionList, GetFormJson, submitForm, DeleteData; root ORStaff | submitForm | submitForm | DeleteData | Get* | CHƯA TEST live |
| Phẫu thuật / phân loại vết mổ | OpOperation, OpNotchGrade: Index, GetGridList, GetPagintionList, GetFormJson, submitForm, DeleteData | submitForm | submitForm | DeleteData | Get* | CHƯA TEST live |
| Báo cáo phí mổ / đăng ký mổ | OpFeeReport Index, OpRegisterReport Index, Report Index + OperationNoticeReport (SSRS) | – | – | – | – | SOURCE (§4); CHƯA TEST live |
| Dữ liệu nền + mẫu phí | SystemManage BaseData (Index, GetDiagnosisList, GetOperationList, GetFloatStaffList), Common (Index, GetNewFieldUniqueValueDB), SysChargeTemplate (ChargeTemplateIndex, ChargeTemplate_EditForm, ChargeTemplate_GetGridJson, ChargeTemplate_SubmitForm, GetDicChargeItems, ChargeTemplate_EditForm_Data) | ChargeTemplate_SubmitForm | ChargeTemplate_SubmitForm | – | Get* | ChargeTemplateIndex no-cookie: ĐẠT 302; còn lại CHƯA TEST |
| Dùng chung | Operation Common: Index, Getbqlist/GetOplist/GetOperationAjax/GetOperations/GetStafflist/GetAneslist/GetRoomlist/GetNotchGradelist | – | – | – | Get* | Common/GetRoomlist no-cookie: ĐẠT 302 |
| Đăng nhập / trang chủ | Login (UALogin), Home (Index, SyncSysConfigParams) | – | – | – | – | Login: ĐẠT 2 viewport (§3); Home no-cookie: ĐẠT 302 |
| OR API | Auth GetToken; Default Query; ORPatient ORPatList; UA GetUserInfo | – | – | – | ORPatList | Validation: ĐẠT (§3); ORPatList có token thật: CHƯA TEST |

### 2c. EMR Web 5235 (+ API 5335)

| Nhóm | Controller / action chính | Thêm | Sửa | Xóa | Lấy/lọc/tìm/sắp xếp | Test |
|---|---|---|---|---|---|---|
| Trang đầu bệnh án (MRHomePage) | MRHomePage: Main/MainPreView/MainEdit/MainPatInfo/MainDiag/MainOplist/MainOpEdit/OutHosPatQuery/MainZyInfo/MainFeeInfo/MainFYInfo, GetGridList/GetEMRPatGridList/GetFormJson/GetFormJsonFee/GetZdGridList/GetOpGridList, SubmitZdList/SubmitOpList/SubmitPatBasic/SubmitMR, HomePageUpload(JS)/HomePageUpload, UpdateUploadStu, SettlementQuery, DiagnosticSave; Report Index + BasyReport (SSRS) | – | Submit* | – | Get* | BasyReport no-cookie: ĐẠT 302; còn lại CHƯA TEST live |
| Bệnh án nội trú (MedicalRecord) | MedicalRecord: MedicalRecordEdiForAdd/Edit(+V2), PreView/PreViewEdit/PrintView, RecordList, LockRecord/UpdateLockzt, BL_Save, LoadTemplateHtml/SavePatientBl/LoadMedicalRecordHtml/UpdatePatientBl/LoadHistoryBackBl, YZForm/AdviceGridView/MergeView, GetLis/PacsSqdh*GridJson, editorPross, HandleDCWriterServicePage (stub DCSoft) | BL_Save, SavePatientBl | Edit*, BL_Save | – | RecordList, Get*Tree, GetBlxxbyJydm | SOURCE DCSoft (§4); live: CHƯA TEST |
| Mẫu bệnh án | MedRecordTemplate: FormQX, MedicalTemplate, Left/RightMedicalTemplate, BlTemplate, testbl (demo), MedicalOpenEditor, TemplateEdit, GetGridJson/GetFormJson, submitForm, DeleteForm, SubmitAssigned, GetTemplateXml/SaveTemplateXml, GetTemplateHtml/SaveTemplateHtml, HandelDCWriterServicePage | submitForm | submitForm | DeleteForm | Get* | SOURCE testbl demo (§4); live: CHƯA TEST |
| Quản lý bệnh án (Blctgl) | Blctgl: CTGLList, YXFHlist, GetTreeList, GetFormJson, SubmitForm, DeleteForm, ShowTab, DiagList, ShowDiagList, GetDiagnosisList/Grid, Getyxfhlist | SubmitForm | SubmitForm | DeleteForm | Get* | CHƯA TEST live |
| Cấu trúc bệnh án (Bljgh) | Bljgh: BlmlAdd, GetElementTree, GetGridJson, GetYsList, SubmitForm, DeleteForm, GetTableColumnList, GetElementMx, GetBlTable, GetBljgMain, BljgMainSubmitForm, DeleteBljgMainForm | SubmitForm* | SubmitForm* | DeleteForm* | Get* | CHƯA TEST live |
| Nguồn dữ liệu (Blys) | Blys: YSList, GetGridJson, GetDataSourceList, GetYsTree, GetFormJson, SubmitForm, DeleteForm, toCS | SubmitForm | SubmitForm | DeleteForm | Get* | CHƯA TEST live |
| Quy tắc bệnh án | MedicalRules: GetBllxTree, GetRulesList, GetItemMXData, SubmitForm, RulesDel | SubmitForm | SubmitForm | RulesDel | Get* | CHƯA TEST live |
| Điều dưỡng | NursingRecord: WZForm/SSForm/OpenOutForm/OpenPutForm, GetPatWard(Tree), SubmitSrl/SubmitScl, SubmitForm/WZ/SS, GetGridJson, GetFormJson/WZ/SS, DeleteForm, GetSrlScl/GetScl/Getzyhrq/Getzyhcqrq | Submit* | Submit* | DeleteForm | Get* | CHƯA TEST live |
| Ngoại trú | Outpatient: Index, PatList, MedRecordTreeEditJson, GetMedRecordTree | – | MedRecordTreeEditJson | – | PatList, cây bệnh án | CHƯA TEST live |
| Thông tin bệnh nhân / cây bệnh án | PatInfo: CommitShow, MedicalApplySend, patientFloatingSelectorSource, Getzbq/ycq/myPatientList, PatList, GetPatinfo, PatMedRecordList, GetMedRecordTree, MedRecordTreeEdit(V2)(Json), UpdatePatMedRecordRel, UpdatePlanStu, CommitPatMedRecord, BackPatMedRecord, selectBJQX, PatHistoricalMedRecord, getRepeatBl, checkBlRules, SubmitForm, MrqcAdd, GetMrqcScore | SubmitForm, Commit* | Update* | Back* | PatList, GetMedRecordTree | PatList no-cookie: ĐẠT 302; còn lại CHƯA TEST |
| Thiết kế mẫu | TemplateDesign: Index, HljlGraph, BtnSubmit, HljlLoadData, BL_Save, HljlCtrlQx, Infodiv, DelRecords | BtnSubmit/BL_Save | – | DelRecords | HljlLoadData | CHƯA TEST live |
| Xem trước y lệnh | YzPreView: YzPreView, GetZyCqYzData, GetZyLsYzData, GetInpatInfo, GetHljldData, GetPrintTempleate, MoreHandleDCWriterServicePage | – | – | – | Get* | CHƯA TEST live |
| Loại bệnh án (Common) | SystemManage Common: RecordType(Form/ChildForm), GetBllxListGrid/TreeGrid, GetBllxbyId, SubmitForm, DeleteForm, GetBllxTreeList, GetBlmbTreeList, GetYsTreeList, GetYsMXList, GetStaffSign, GetBllxList, GetDiagList, GetDiagnosisList/Grid, GetOperationList(Page), GetAnesList, GetNotchGradeList, GetCommonList, GetDepartment, GetInpatientArea, GetXtbrxz, GetNationality, GetNationas, GetStaffListByDutyCode, GetDutyList | SubmitForm | SubmitForm | DeleteForm | Get* | GetBllxListGrid no-cookie: ĐẠT 302; còn lại CHƯA TEST |
| Quản trị hệ thống | SysConfig (Index/Form/TemplateIndex/TemplateForm — notice, dù view thật còn trên đĩa), SysLog Index (notice), SysModule Index/Form (notice), SysRole Index/Form (notice), SysStaff Selector (notice), SysUser PersonalInfo/Roles/Selector/Index (notice) | – | – | – | – | SOURCE (§4); SysModule/Index no-cookie: ĐẠT 302 |
| Đăng nhập / trang chủ | Login (OutLogin, UALogin), Home (Index, GetNewFieldUniqueValue, SyncSysConfigParams) | – | – | – | – | Login: ĐẠT 2 viewport (§3, kèm lỗi B1/B2); Home no-cookie: ĐẠT 302 |
| EMR API | Auth GetToken; BljgData BljghDataDealwith; Default Query (+ Get/Post/Put/Delete mẫu) | – | – | – | Query, BljghDataDealwith | Validation: ĐẠT (§3, kèm ghi nhận B6); có token thật: CHƯA TEST |

## §3. Kết quả kiểm chứng live đã đạt (không cần login)

### 3a. Trang login — desktop 1440×900 + mobile 390×844 (Playwright + Firefox local)

| App | URL | Desktop | Mobile | Console error | Ghi chú |
|---|---|---|---|---|---|
| MRMS | 5232/Login/Index | 200, `#txt_account` + `#login_button` có, tiêu đề "MRMS Hệ thống bệnh án" | 200, form đăng nhập dùng được, nền trang trí bị cắt 2 biên (thẩm mỹ, xem B5) | `[]` | Ảnh `sweep-c-mrms-login-desktop/mobile.png` |
| OR | 5231/Login/Index | 200, form đủ, tiêu đề "OR Hệ thống phòng mổ" | 200, form dùng được | `[]` | Ảnh `sweep-c-or-login-desktop/mobile.png` |
| EMR | 5235/Login/Index | 200, form đủ, tiêu đề "EMRHệ thống quản lý" (thiếu khoảng trắng sau "EMR" — nguyên văn) | 200, form dùng được | `[]` | Ảnh `sweep-c-emr-login-desktop/mobile.png`; kèm lỗi B1 + B2 |

Cả 3 trang login đều còn dòng footer vendor tiếng Trung ở cuối trang
(`Copyright © 2025 湖北天天数链技术有限公司 ... 《天天开源软件(社区版)许可协议》`) và logo
chữ Trung `天天开源` — đây là **tên riêng + thỏa thuận cấp phép của vendor**, thuộc nhóm
ngoại lệ đã ghi trong đợt quét CJK trước, KHÔNG tính là bug mới trong đợt này (xem §5,
mục ghi nhận). Không có captcha trên trang login nào (khớp cấu hình `isCheckChkCode=false`).

### 3b. Cổng kiểm tra auth không cookie — tất cả ĐẠT 302 về /Login/Index

Prod (curl, không cookie): MRMS/OR/EMR `Home/Index` → 302 về đúng `/Login/Index` của
từng app; MRMS `MainRecord/Main`, `MrDept/Index`, `Report/Index`, `SysModule/Index`;
OR `OpApply/Index`, `OpRoom/Index`, `SysChargeTemplate/ChargeTemplateIndex`;
EMR `MedicalRecord/Index`, `PatInfo/Index`, `MedRecordTemplate/Index`, `Report/BasyReport`,
`SysModule/Index`; các endpoint JSON `GetGridList`, `GetPagintionList`, `GetRoomlist`,
`PatList`, `GetBllxListGrid` → 302 (không rò JSON khi chưa login). Đây là hành vi đúng
sau fix phase 7 (HandlerLogin gỡ khỏi ControllerBase). Ảnh `home-nocookie` chỉ là trang
login sau redirect (byte giống hệt ảnh login, đúng kỳ vọng — không phải nội dung riêng).

### 3c. API validation — không token / token sai / tài khoản không tồn tại

| Probe | Kết quả live |
|---|---|
| API root `/` 5332/5331/5335 | 404 cả 3 — đúng (API thuần, không có default route) |
| MRMS `POST /api/Default/Query {}` | 40004 `TIMESTAMP_IS_REQUIRED` |
| MRMS `POST /api/Default/Query {Timestamp, AppId, keyword}` (không token) | 40004 `TOKEN_IS_REQUIRED` / "Token truy cập trống" |
| OR `POST /api/ORPatient/ORPatList {}` | 40004 `TIMESTAMP_IS_REQUIRED` |
| OR `POST /api/ORPatient/ORPatList {Timestamp, AppId}` (không token) | 40004 `TOKEN_IS_REQUIRED` / "Token truy cập trống" |
| OR `POST /api/UA/GetUserInfo {Timestamp, AppId, access_token=bogus}` | 40004 `AUTH_FAILED` / "Access_token không hợp lệ hoặc đã hết hạn" |
| MRMS `POST /api/Auth/GetToken {Timestamp, AppId, TokenType, Account=zzz-no-such-user, Password=md5 rỗng}` | 40004 `AUTH_FAILED` / "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa" (fail-closed, không lộ user có tồn tại hay không; tài khoản không tồn tại nên không tốn lượt lockout) |
| OR `POST /api/Auth/GetToken` (cùng body) | 40004 `AUTH_FAILED`, cùng message tiếng Việt |
| EMR `POST /api/Auth/GetToken {}` | HTTP 400 ProblemDetails `{"errors":{"AppId":["AppId là bắt buộc"]}}` — dạng `[ApiController]` (xem B6) |
| EMR `POST /api/Auth/GetToken` (đủ envelope, tài khoản không tồn tại) | 40004 `AUTH_FAILED` / "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa" (dạng envelope riêng của EMR API: `msg` ở top-level, `sub_msg: null`) |
| EMR `POST /api/Default/Query {}` và `POST /api/BljgData/BljghDataDealwith {}` | HTTP 400 ProblemDetails `AppId là bắt buộc` |

Thứ tự validation của envelope MRMS/OR đã rõ: Timestamp → Token → auth. GetToken kiểm
tra credential thật (phase 2) thể hiện qua message fail-closed thống nhất.

### 3d. Staging boot + cổng auth (mỗi lần 1 app, kill đúng PID, đã xác nhận cổng DOWN)

| App staging | Cổng | PID đã chạy | Probe | Kill |
|---|---|---|---|---|
| MRMS staging | 15232 | 20138 | Login 200 (10.840 byte, lệch nhẹ so với prod 10.831 do binary staging cũ hơn); Home/Main/SysModule/Report Index no-cookie → 302 về Login staging | `kill 20138`, đã xác nhận 15232 DOWN + PID gone |
| OR staging | 15231 | 20867 | Login 200 (10.803 byte vs prod 10.785); Home/OpApply/OpRoom/ChargeTemplateIndex no-cookie → 302 | `kill 20867`, đã xác nhận 15231 DOWN + PID gone |
| EMR staging | 15235 | 21593 | Login 200 (10.727 byte vs prod 10.709); Home/MedicalRecord/PatInfo/BasyReport/SysModule Index no-cookie → 302 | `kill 21593`, đã xác nhận 15235 DOWN + PID gone |

Không có app staging nào của worker này còn chạy cuối phiên. Không có script staging
riêng cho MRMS/OR/EMR **API** trong repo (`run-*-api-stg.sh` không tồn tại cho 3 app này)
— nên API chỉ được probe trên prod ở mức validation (§3c); luồng API có token thật
thuộc phần nợ lại §6.

## §4. Kiểm chứng riêng các điểm còn tồn (ghi đúng hiện trạng, không sửa)

### 4a. EMR DCSoft — vẫn là template demo, editor KHÔNG chạy được (ghi đúng, không tuyên bố ngược lại)

- `MedicalRecordController.HandleDCWriterServicePage()` (và `MedRecordTemplateController`,
  `MRMS RecordController`, `MRMS MainRecord` tương tự) trả JSON lỗi显式, không 404:
  message: "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản
  build .NET Core). Cần thay bằng trình soạn thảo web khác."
- View demo còn nguyên: `MedRecordTemplate/testbl.cshtml` (và `testbl - 复制.cshtml`) load
  script editor ngoài `http://127.0.0.1:3071/iframe/HmEditorIfame.js` (địa chỉ dev đã chết,
  không có service 3071 trong cụm này) + các dòng huimei editor đã comment; view
  `MedicalRecord/MedicalRecordEditDemo.cshtml` tồn tại nhưng là demo.
- Các view soạn thảo thật (`MedicalRecordEdiForAdd/Edit(+V2)`, `PreView/Edit`, `PrintView`)
  đặt `ViewBag.WriterControlXmlData` nhưng các dòng `ViewBag.WriterControlHtml =
  eng.GetAllContentHtml()` đều đang bị comment trong controller → trang soạn thảo không
  render được nội dung editor DCSoft.
- Kết luận: **editor DCSoft không chạy được trên triển khai Linux/.NET Core này**; mọi
  luồng "soạn/lưu/xem bệnh án qua editor" thuộc phần CHƯA TEST live (cần login + cần
  editor thay thế — quyết định sản phẩm, ngoài phạm vi sweep). Không tuyên bố editor
  chạy được ở bất kỳ chỗ nào trong báo cáo này.

### 4b. MRMS/OR có stub SSRS — Windows-only, ghi đúng hiện trạng

- MRMS: các `*ReportController.Index()` chỉ set `ViewBag.ReportServerHOST =
  AppSettings["ReportServer.HOST"]` rồi trả view; view SSRS thực hiện redirect JS tới
  ReportServer. `appsettings.json` của MRMS Web vẫn trỏ `ReportServer.HOST` tới địa chỉ
  ngoài `http://59.80.30.160:56014/ReportServer/` (không có trong cụm này).
- MRMS `ReportManage/Report/Index`: nhận định cũ "view-not-found" **đã lỗi thời** — phase 2
  (commit `aff78cf5`, M-05) đã thêm `Views/Report/Index.cshtml` dạng trang liệt kê các
  báo cáo sẵn có + ghi chú "các báo cáo SSRS cần máy chủ báo cáo riêng; khi chưa cấu hình,
  trang báo cáo sẽ hiển thị thông báo thay vì số liệu". Nội dung trang này CHƯA TEST live
  (cần login) — chỉ xác nhận view tồn tại trên source.
- OR: `Report/Index` là stub tương tự (ghi chú SSRS trong view); `OperationNoticeReport`
  set `ReportServerHOST` từ config và view tự chèn thông báo tiếng Việt khi HOST trống:
  "Báo cáo SSRS chưa được cấu hình (ReportServer.HOST trống): máy chủ SQL Server
  Reporting Services không chạy trên Linux nên trang báo cáo không thể hiển thị nội dung."
- EMR: `MRHomePage/Report/BasyReport` đã sửa hành vi gốc (JS-redirect tới
  `ReportViewer.aspx` rồi 404) thành thông báo SSRS显式 khi HOST trống, giữ nguyên nhánh
  redirect khi có cấu hình SSRS thật.
- Kết luận: **không báo cáo SSRS nào chạy được trên Linux trong đợt này**; tất cả đều là
  stub/thông báo — đúng hiện trạng Windows-only đã ghi từ trước, không phải hồi quy.

### 4c. OR DeleteForm bị comment — ghi nhận, KHÔNG tự bỏ comment

- File `Newtouch.HIS.OR/Newtouch.OR.Web/Controllers/ORApplyInfoController.cs:68-72`:
  `DeleteForm(string keyValue)` có dòng `_oRApplyInfoRepo.DeleteForm(keyValue);` **bị
  comment**, rồi `return Success("Thao tác thành công.");` — tức gọi API xóa này sẽ nhận
  "thao tác thành công" trong khi **không dòng nào bị xóa**. Comment này có từ cây gốc
  vendor (không phải do port thêm vào), nên theo task worker chỉ ghi nhận.
- Đối chiếu trong cùng app: 5 root controller còn lại (`ORArrangement`,
  `OROperation`, `ORRegistration`, `ORRoom`, `ORStaff`) đều gọi repo thật trước khi trả
  Success; area `OpApplyController.DeleteApply/DeleteApplyExtend` cũng gọi repo thật
  (`_ORApplyInfoExpandRepo.DeleteForm`). Chỉ riêng `ORApplyInfoController.DeleteForm`
  là no-op giả thành công.
- Trạng thái: **source-confirmed, CHƯA test live** (cần login + staging; và test xóa thật
  chỉ được chạy trên staging sau khi login mở). Mức độ: xem B3. Cần quyết định sản phẩm
  (bỏ comment / xóa action / đổi message) — worker không tự quyết.

### 4d. MRMS Sys* view — SSO phục vụ một phần, đã kiểm chứng route-by-route

MRMS/EMR Sys* không phải trang chức năng thật: controller gốc chỉ có trong nhị phân
`FrameworkBase.MultiOrg.Web` (không source) nên port trả **notice tiếng Việt** thay vì 404
(`SysManageNotice.cshtml`; Index/Form dùng `override` vì base khai báo virtual — đúng
lesson phase 2). Kiểm chứng link SSO trong notice (no-cookie: 302 về SSO login = route
tồn tại; 404 = route không tồn tại):

| SSO route | Kết quả | MRMS notice trỏ tới | Đúng? |
|---|---|---|---|
| `/SysManage/SysModule/Index` | 302 (tồn tại) | SysModule notice → link này | Đúng |
| `/SysManage/SysRole/Index` | 302 (tồn tại) | SysRole notice → link này | Đúng |
| `/SysManage/User/Index` | 302 (tồn tại) | SysUser Index notice → link này (không trỏ `/SysManage/SysUser/Index` — route đó 404) | Đúng |
| `/SysManage/User/UserRoles` | 302 (tồn tại) | SysUser Roles notice → link này | Đúng |
| `/SysManage/SysUser/Index`, `/SysManage/SysLog/Index`, `/SysManage/SysConfig/Index`, `/SysManage/SysStaff/Index` | 404 cả 4 | MRMS SysLog/SysConfig/SysStaff notice **không gắn link SSO** (đúng, vì SSO không có route); SysUser PersonalInfo notice không gắn link (đúng, SSO không có trang tương đương) | Đúng |
| EMR Sys* notice | — | EMR SysModule/SysUser/SysRole/SysLog/SysConfig/SysStaff notice **không gắn link SSO nào** (chỉ có PageName) | Không sai link; chỉ là notice thuần |

Nhận định cũ "SSO phục vụ Sys*" cần thu hẹp lại: **SSO chỉ phục vụ SysModule, SysRole,
User (+UserRoles); SysUser/SysLog/SysConfig/SysStaff theo tên đó không tồn tại trên SSO**.
MRMS đã xử lý đúng điểm này trong notice; EMR chọn notice thuần không link. View thật
của EMR Sys* (`SysConfig/Index/Form/Template*`, `SysModule/Index/Form`, `SysRole/Index/Form`,
`SysUser/Index/...`) còn trên đĩa nhưng controller trả notice thay vì render chúng — là
thiết kế stub có chủ ý, không phải file thiếu.
OR không có Sys* controller riêng (SystemManage của OR chỉ có BaseData, Common,
SysChargeTemplate) — ngoài phạm vi mục này.

## §5. Bug tìm được (chỉ những gì đã tái hiện live hoặc xác nhận từ source, có vị trí chính xác)

| ID | App / trang / action | Viewport | Mức | Tái hiện | Mong đợi vs thực tế | Text lỗi / bằng chứng |
|---|---|---|---|---|---|---|
| B1 | EMR Web, `/Login/Index` | Desktop + Mobile | Thấp (thẩm mỹ, gây hiểu lầm) | Mở trang login EMR bằng Firefox (Playwright) | Banner "đang dùng IE" chỉ hiện trên IE ≤ 8; trên Firefox phải ẩn | Thực tế: banner hiện trên Firefox ở cả 2 viewport: "Bạn vẫn đang dùng IE, Trước khi dùng hệ thống bình thường, vui lòng nâng cấp trình duyệt của bạn lên IE8 phiên bản trở lên…" + link nâng cấp IE8/Chrome. Nguyên nhân source: `EMR/Views/Login/Index.cshtml:22` đặt `<div id="errorie">` **không** bọc trong `<!--[if lte IE 8]>…<![endif]-->`; trong khi MRMS/OR cùng vị trí có bọc conditional comment nên banner ẩn đúng trên trình duyệt hiện đại. Ảnh: `sweep-c-emr-login-desktop/mobile.png` |
| B2 | EMR Web, `/Login/Index` | Desktop + Mobile | Thấp (thẩm mỹ) | Mở trang login EMR | Nhãn tab nằm gọn 1 dòng như MRMS/OR ("Chào mừng") | Thực tế: nhãn tab EMR là "Chào mừng đăng nhập" (`Index.cshtml:41`) dài hơn, bị wrap xuống dòng 2 và **chèn lên ô nhập tài khoản** ("đăng nhập" in đậm đè lên viền trên của input). MRMS/OR dùng nhãn "Chào mừng" nên không wrap. Ảnh: `sweep-c-emr-login-desktop/mobile.png` |
| B3 | OR Web, `ORApplyInfoController.DeleteForm` (root, không phải area OpApply) | N/A (API web, source-confirmed; live CHƯA test do cần login) | Cao (toàn vẹn dữ liệu: báo thành công giả) | (Sau khi login mở, trên staging) gọi `DeleteForm?keyValue=<id apply tồn tại>` rồi kiểm tra lại danh sách | Hoặc xóa thật rồi báo thành công, hoặc báo lỗi/không hỗ trợ | Thực tế theo source (`ORApplyInfoController.cs:68-72`): repo call bị comment, action luôn `return Success("Thao tác thành công.")` mà không xóa gì. Cần quyết định sản phẩm trước khi sửa (xem §4c). Không tự bỏ comment trong đợt sweep |
| B4 | Cả 3 app, trang login mobile | Mobile 390×844 | Thấp (thẩm mỹ) | Mở trang login mobile | Nền trang trí co gọn trong viewport | Thực tế: ảnh nền trang trí (vòng HIS + logo) bị cắt ở biên trái/phải trên mobile; form đăng nhập vẫn căn giữa và dùng được nên không chặn luồng. Ảnh: `sweep-c-*-login-mobile.png` |
| B5 | EMR API, envelope không thống nhất với MRMS/OR API | N/A (API) | Thấp (ghi nhận, không gọi là bug chức năng) | `POST /api/Auth/GetToken {}` và `POST /api/Default/Query {}` | Envelope lỗi thống nhất 40004 như MRMS/OR | Thực tế: EMR API (dạng `[ApiController]`) trả HTTP 400 ProblemDetails `{"errors":{"AppId":["AppId là bắt buộc"]}}` khi thiếu AppId; khi đủ envelope thì trả 40004 nhưng `msg` ở top-level và `sub_msg: null` (MRMS/OR đặt message trong `sub_msg`). Đây là mẫu đã biết từ parity CIS (lệch `[ApiController]` → 400 ProblemDetails thay vì envelope `{FIELD}_IS_REQUIRED`), ghi nhận để vòng fix quyết có thống nhất hay không |

Không tính là bug mới: footer/logo tiếng Trung vendor trên trang login (§3a) — ngoại lệ
đã ghi từ đợt quét CJK; tiêu đề EMR "EMRHệ thống quản lý" thiếu khoảng trắng — nguyên
văn từ title gốc, ghi nhận tại §3a, không lập bug riêng.

## §6. Phần không tái hiện được / không test được + lý do, và phần nợ lại

### Không test được trong phiên này (nguyên nhân duy nhất: thiếu mật khẩu demo, §0)

1. Toàn bộ trang sau login của 3 app (trang chính, dashboard, mọi grid/filter/search/
   sort có dữ liệu, mọi form thêm/sửa, mọi luồng xóa/lưu, guidang/CXguidang MRMS,
   HomePageUpload EMR, MrqcAdd/GetMrqcScore, ChargeTemplate_SubmitForm OR, các báo cáo
   SSRS nội dung thật, editor DCSoft, cây bệnh án, cây phí, cây khoa) — cần session
   đăng nhập; worker có 0 session.
2. Mọi luồng ghi staging (kể cả test xóa B3, test lưu bệnh án, test sắp xếp mổ) —
   chưa chạy; không có gì để "tái hiện" vì chưa có lần chạy nào.
3. API có token thật (Query/ORPatList/BljghDataDealwith với token từ GetToken hợp lệ,
   UA GetUserInfo với token thật) — cần credential thật; chỉ mới tới được cổng
   validation (§3c).
4. Trang chính sau login trên mobile (viewport 390×844) — "trang chính" trong task
   hiểu là trang sau login; ảnh mobile đã chụp chỉ là login + redirect về login
   (xem §3b). Không tuyên bố đã test mobile cho trang chính.

### Không tái hiện được (đã kiểm chứng ngược lại)

- Lỗi view-not-found của MRMS `ReportManage/Report/Index` từ đợt chụp cũ: **không còn
  tái hiện ở mức source** — view stub đã được thêm ở `aff78cf5` (§4b). Live vẫn nợ
  (cần login) nhưng lỗi cũ không còn đúng ở mức source.
- Lỗi redirect-loop Sys* của MRMS/EMR: không tái hiện ở mức route — Sys* trả notice
  (MRMS) và no-cookie cho 302 về Login đúng chuẩn (§3b, §4d).

### Nợ lại cho phiên có mật khẩu (chạy 1 lần là đủ, theo thứ tự)

1. Login staging 15208 bằng 000000 (1 lần; nếu sai thì DỪNG, không thử lại — tránh khóa).
2. Login prod 3 app + chụp trang chính desktop + mobile (trang chính thật, không phải
   redirect).
3. Trên staging (mỗi lần tối đa 2 app, kill đúng PID): chạy lại toàn bộ bảng §2 ở mức
   đọc (grid/filter/search/sort) rồi mới tới ghi — thứ tự ghi: MRMS MrDept/MrFee/Mrbasy
   Submit/Delete, guidang/CXguidang; OR OpApply Submit/DeleteApply + root DeleteForm
   (B3, kiểm tra DB sau xóa); EMR PatInfo/MedicalRecord/Template/Bljgh/Nursing Submit/
   Delete; OR ChargeTemplate_SubmitForm; EMR BasyReport với HOST trống (xem thông báo
   SSRS có hiện không).
4. API có token thật: GetToken → Query/ORPatList/BljghDataDealwith/UA GetUserInfo.
5. Kiểm tra lại B1/B2 sau fix (vòng fix sau, không phải đợt này).

## §7. Các quyết định mở — CHỈ ghi nhận hiện trạng, không tự quyết hay redesign

- Schema `zy_bqksby` / `zy_ksbyth`: **0 kết quả** khi grep toàn bộ source MRMS + OR +
  EMR (`.cs`). Không có entity/migration/mapping nào cho 2 bảng này trong phạm vi C;
  không đề xuất schema, không tự tạo bảng.
- 12 entity `StringLength(23)` riêng lẻ: **0 kết quả** khi grep `StringLength(23)` trong
  source MRMS + OR + EMR. Không có gì để đối chiếu trong phạm vi C; không tự chuẩn hóa.
- E-11 `GetAuthedWardSelectJson`: **0 kết quả** khi grep trong source MRMS + OR + EMR.
  Không có action/service nào tên này trong phạm vi C; không tự thiết kế lại.
- Tiền tố số chứng từ: **không gặp trong đợt này** (không có luồng ghi nào được chạy,
  không có số chứng từ nào được cấp) nên không có gì để thống nhất; worker không tự
  thống nhất tiền tố cũ/mới ở bất kỳ chỗ nào trong báo cáo này.

## §8. Ảnh đã chụp (12 file, thư mục `shots-sweep/`)

Thư mục: `~/workspace/openhis-core/shots-sweep/` — đã kiểm tra: thư mục này thuộc pattern
`shots*/` trong `.gitignore` (dòng 11) nên **không thể lọt vào commit**; `git status` cuối
vẫn chỉ có `M roles-shots-progress.md` (của điều phối viên, không đụng) và
`?? scripts/sweep-final/` (thư mục báo cáo untracked đã có từ trước).

- `sweep-c-mrms-login-desktop.png`, `sweep-c-mrms-login-mobile.png`
- `sweep-c-mrms-home-nocookie-desktop.png`, `sweep-c-mrms-home-nocookie-mobile.png`
  (nội dung = trang login sau redirect, byte giống ảnh login — đúng kỳ vọng)
- `sweep-c-or-login-desktop.png`, `sweep-c-or-login-mobile.png`
- `sweep-c-or-home-nocookie-desktop.png`, `sweep-c-or-home-nocookie-mobile.png`
- `sweep-c-emr-login-desktop.png`, `sweep-c-emr-login-mobile.png` (thấy rõ B1 + B2)
- `sweep-c-emr-home-nocookie-desktop.png`, `sweep-c-emr-home-nocookie-mobile.png`
- (Thư mục còn chứa ảnh của worker khác: `sett-B-*`, `sweepE-*` — không phải của worker C,
  không đụng tới.)

## §9. Cam kết cuối phiên

- Không sửa code, không commit, không sửa `roles-shots-progress.md`, không sửa launcher,
  không đổi mật khẩu SA, không ghi DB demo (0 lệnh SQL ghi), không ghi DB staging
  (không có session để ghi).
- Mọi tiến trình staging do worker này khởi động (MRMS 20138, OR 20867, EMR 21593) đều
  đã kill đúng PID và đã xác nhận cổng DOWN; không còn tiến trình staging nào của worker
  này chạy cuối phiên. Các tiến trình prod/staging có sẵn (5208, 7001, 15208, 17001,
  6380, 5231/5232/5235, 5331/5332/5335) không bị đụng.
- "Mobile" trong báo cáo này = emulation viewport 390×844 bằng Playwright + Firefox
  local. OpenHIS không có app native — không ở đâu trong báo cáo này tuyên bố đã test
  app native.
- Kết luận tổng thể: **đợt sweep C chưa hoàn tất ở phần cần login** — phần kiểm chứng
  được (login page, cổng auth, API validation, staging boot, các điểm tồn đọng §4) đều
  đạt/ngoại trừ 3 bug B1–B3 + 2 ghi nhận B4–B5; phần còn lại (§6) cần đúng 1 thứ từ điều
  phối viên là mật khẩu demo, rồi chạy lại theo danh sách nợ.
