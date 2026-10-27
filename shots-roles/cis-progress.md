# CIS Web screenshots — progress (2026-10-27 ICT)

App: CIS Web on http://127.0.0.1:5234, account 000000 (HospAdministrator).
Method: local Playwright 1.64 + Firefox (headless), viewport 1366x900 ONLY (no full_page).
Login: POST /Login/CheckLogin FORM-encoded {username: "000000", password: md5("000000"),
code: ""} — note: a JSON-body POST FAILS with "登录失败，请联系管理员" (binary-Lib
message); form encoding succeeds ("Đăng nhập thành công"). Cookies
CIS_newtouch_loginuserkey_2016 + CIS_CookieKey_CurUserCode reused in the Playwright
context. Scripts: /tmp/cis_shot.py (pass 1), /tmp/cis_shot2.py (pass 2), own scripts;
~/workspace/shot/shot_ff.py untouched (SSO-specific). xzadmin/admin NOT attempted.
Restarts: NONE. CIS 5234 was already UP and stayed UP throughout (200 at end).
No DB writes (SELECT only), no git operations, no other apps touched.

## Menu enumeration (two ways, deduplicated)

(1) Rendered home page after login (/Home/Index) — CIS home is a plain portal link list,
NOT a sidebar menu. Links found (11 incl. /Home itself + BaseData 404 below):
/ClinicManage/ClinicApply/Index, /DoctorManage/DoctorsAdvice/Index,
/MedicalRecord/Index, /NurseManage/InpatientList/Index, /PatientList/Index,
/Prescription/Index, /QueueManage/QueueInfo/Index, /ReportManage/Report/Index,
/SystemManage/BaseData/Index (404, see FAILED), /TemplateManage/PresTemplate/Index.
Rendered-page <a> collection across all pass-1 pages added NO new page URLs
(only the same portal links) — CIS navigation is JS-driven, so pass 2 was built
from controller Form/page actions instead (see below).

(2) Sys_Module rows in demo DB Newtouch_CIS (SELECT only): 93 rows, 89 distinct
URLAddress values (AppId mostly NULL — the table is the shared original menu table,
not CIS-scoped). Union with home links + root/area page actions = 110 probe
candidates; 92 returned HTTP 200 (incl. /Login/Index), 16 returned 404, 1 stray
probe line was a sqlcmd footer artifact, 1 was /Home duplicating /Home/Index.

Pass 2 (Form companions): every parameterless View-returning action found in the
port's controllers was probed (164 candidates). 114 returned HTML 200 (minus
/Home duplicate and the pass-1 Chat duplicate = 112 shot); the rest were JSON/data
endpoints, POST-only actions (405), or view-missing JSON errors — listed below,
NOT pages, so no shot applies.

## SHOT COUNT: 204 files, all NEW in shots-roles/ (cis-*.png)

- Pass 1: 92 (login + home + 90 menu/root pages; /Apply/Test included — see
  error-body note; /InfusionManage/Query included — see needs-parameter note).
- Pass 2: 112 Form companions / deeper pages.
- Playwright failures: 0 in either pass (every attempted URL produced a file).

Viewed by me (render confirmed, not blank/error): cis-login.png (VI login form;
vendor footer Chinese is a proper noun, SSO precedent), cis-home-index.png
(fully Vietnamese portal list), cis-doctormanage-doctorsadvice-index.png
(VI tabs + grid chrome), cis-queuemanage-queueinfo-index.png (fully Vietnamese
queue board), cis-nursemanage-allergy-allergyform.png (VI allergy form +
validation banner), cis-apply-test.png (JSON error body, viewed as evidence),
cis-clinicmanage-clinicmetting-index.png (viewed and confirmed BLANK — see note).
Requirement ≥2 viewed: SATISFIED (7 viewed).

## Caveats on shot content (all shot, honestly noted)

- BLANK BY DESIGN: /ClinicManage/ClinicMetting/Index — view file is 226 bytes
  (title "Hội nghị video" + 2 script refs, NO body markup); shot is a blank page.
  Same class as MRQC PreView.
- NEAR-BLANK STANDALONE (JS/frame-hosted content): /DoctorManage/Apply/Examination,
  /DoctorManage/Apply/inspection, /Apply/Examination, /Apply/Inspection — standalone
  body text is only "Ấn tượng lâm sàng / Đề nghị / Ghi chú"-class labels; the views
  are 18–26 KB of JS that renders inside a parent frame/modal. Shots are faithful
  standalone renders, not failures.
- EMPTY GRIDS ARE CORRECT: many index pages show empty grids (demo tables empty,
  e.g. DoctorsAdvice "trên tổng số 0 trang", QueueInfo "Số người chờ: 0") — not failures.
- DB-sourced values in grids (e.g. ward names 内科一病区, patient names) are
  demo-DB content, same class as other modules' DB/enum pass — not view text.

## FAILED with reason — NO shot possible (16 × HTTP 404, empty body, verified via curl with login cookies, retried in probe)

Other apps' menus (not in the CIS port — these areas belong to Sett/other apps):
- /Dietary/Index, /Dietary/RefundIndex (dietary module, no Dietary controller in CIS port)
- /HospitalizationPharmacy/DispenseIndex (pharmacy module)
- /MedicalRecordManage/PatInfo/Index, /MedicalRecordManage/PatInfo/PatMedRecordList
- /OutpatientManage/OutpatientReg/OutpatientReg2018 (+ ?readcard=true variant — Sett's registration page)
- /StorageManage/Storage/DepartmentApply

Sys* pages served by SSO, not by the module app (same class as MRQC/MRMS precedent):
- /SystemManage/SysModule/Index, /SystemManage/SysUser/Index, /SystemManage/SysRole/Index,
  /SystemManage/SysLog/Index, /SystemManage/SysConfig/Index

No such controller/action in the CIS port:
- /SystemManage/Template/Index (no TemplateController in SystemManage area)
- /SystemManage/TTCataloguesComparison/DetailIndex (no TTCataloguesComparison controller
  in the port — NOTE: this differs from the brief's "TTCataloguesComparison empty" note;
  the DetailIndex route itself 404s; the stub, if any, lives elsewhere, not at this URL)
- /SystemManage/BaseData/Index (BaseDataController exists but has NO Index action —
  data endpoints only; this is a BROKEN PORTAL LINK on the CIS home page)

## POST-only actions (405 on GET — not pages, no shot)

- /BespeakRegister/CancelYY (405), /NurseManage/StockCarryOver/CarryOverProduct (405)

## NEEDS PARAMETER (shot as error evidence where applicable)

- /InfusionManage/Query WITHOUT ?tags=… → HTTP 200 JSON error body
  "Cannot perform runtime binding on a null reference" (Views/InfusionManage/query.cshtml:36).
  Shot cis-infusionmanage-query.png shows this error. With ?tags=… it is expected to
  render; the tags value is caller-supplied, so no default shot was taken. Faithful
  param-dependent behaviour per brief.
- All other parameterised actions (e.g. DoctorsAdvice Get*Json, grid JSON endpoints,
  Print* report pages needing IDs) are data/print endpoints, not menu screens.

## ERROR BODIES shot (HTTP 200 envelope, no usable screen — recorded, not hidden)

- /Apply/Test → JSON: "The view 'Test' was not found" (no Test.cshtml in port).
  Shot cis-apply-test.png shows the full JSON error. This is a dev-test menu row
  (Sys_Module name "Test"), not a real clinical screen.
- /InfusionManage/Query (no tags) → see NEEDS PARAMETER above.
- Probes that returned JSON errors on GET (NOT shot — they are action endpoints,
  not pages): ClinicApply/Accept ("Không tìm thấy thông tin xếp lịch"),
  ClinicApply/Back (proxy status-line error), ClinicApply/EnterMetting (missing
  registration code), DoctorsAdvice/StopSubmitForm + advicedc ("医嘱不存在"),
  OrderAudit/QueryPrepareMedicine ("Invalid object name 'zy_bqksbymx'"),
  OrderExecution/ApplyFormUpload ("Invalid object name 'xt_JyjcFileUpload'"),
  PrepareMedicine/QueryPrepareMedicine ("Invalid object name 'zy_ksbythmx'"),
  DepartmentSupplies/ProfitAndLoss + OrderExecution/ApplyFormUploadForm +
  WardMaintenance/DispensingQuery + PatientList/PatientPreDiag (view not found),
  QueueInfo/CallNumber (null reference), EntryRegistration/EnterArea,
  Infusion/Grouping, PrepareMedicine/SelectLoss* (null reference), SkintestTree
  ("请选择查询类别"). These indicate missing demo tables/params, not missing screens.

## KNOWN-CONTEXT items — status observed

- ActiveX LIS views (4, VI notice): not separately reachable as menu URLs in this
  pass; the LIS query forms shot (LisSqdhQueryForm, GetlisPeportForm) render as
  normal query forms. No new ActiveX screen found in menu enumeration.
- WCF/CMM/BGESB/HLYY proxies: MainBusiness/CmmAlertForm shot; CMM-backed actions
  return explicit errors when invoked (see ClinicApply/Back probe note above).
- SSRS shells: ReportManage ReportConfig/ReportConfigForm + *PresReport pages shot
  (they render as config/report shells).
- NoticeHub push stub / SelectSfxmYp empty / SysConfigReader.OrgReportLink empty:
  no standalone menu screens; nothing to shoot beyond the pages above.
- MzBespeakRegister no-token INTERNAL_SERVER_ERROR: this is a CIS.API behaviour,
  out of scope for Web screenshots; root /BespeakRegister/BespeakRegister shot 200.
- Clinic/GetClinicToken external vendor API: a JS call inside clinic pages, not a
  standalone screen; not separately shot (external dependency per brief).
- Center hosts 8015/8016 UP during this run; remote-consultation entry
  (ClinicMetting) is the blank view noted above, not a center failure.

## Full shot tables

### Pass 1 — menu/home pages (91 + login)

| File | URL | HTTP |
|---|---|---|
| cis-login.png | /Login/Index (logged out, shot FIRST) | 200 |
| cis-home-index.png | /Home/Index | 200 |
| cis-apply-examination.png | /Apply/Examination | 200 |
| cis-apply-index.png | /Apply/Index | 200 |
| cis-apply-inspection.png | /Apply/Inspection | 200 |
| cis-apply-test.png | /Apply/Test | 200 |
| cis-bespeakregister-bespeakregister.png | /BespeakRegister/BespeakRegister | 200 |
| cis-clinicmanage-clinicapply-index.png | /ClinicManage/ClinicApply/Index | 200 |
| cis-clinicmanage-clinicmetting-index.png | /ClinicManage/ClinicMetting/Index | 200 |
| cis-doctormanage-apply-examination.png | /DoctorManage/Apply/Examination | 200 |
| cis-doctormanage-apply-inspection.png | /DoctorManage/Apply/inspection | 200 |
| cis-doctormanage-doctorsadvice-index.png | /DoctorManage/DoctorsAdvice/Index | 200 |
| cis-doctormanage-electronicprescription-index.png | /DoctorManage/ElectronicPrescription/Index | 200 |
| cis-doctormanage-operation-arrangequery.png | /DoctorManage/Operation/ArrangeQuery | 200 |
| cis-doctormanage-operation-operationsupplement.png | /DoctorManage/Operation/OperationSupplement | 200 |
| cis-doctormanage-outpatientquery-consultdetail.png | /DoctorManage/OutpatientQuery/ConsultDetail | 200 |
| cis-doctormanage-outpatientquery-consultrecord.png | /DoctorManage/OutpatientQuery/ConsultRecord | 200 |
| cis-doctormanage-outpatientquery-mzcasehistory.png | /DoctorManage/OutpatientQuery/MZcasehistory | 200 |
| cis-doctormanage-outpatientquery-queryoutpatientdetail.png | /DoctorManage/OutpatientQuery/QueryOutpatientDetail | 200 |
| cis-doctormanage-outpatientquery-queryoutpatientreservation.png | /DoctorManage/OutpatientQuery/QueryOutpatientReservation | 200 |
| cis-infusionmanage-query.png | /InfusionManage/Query | 200 |
| cis-mainbusiness-index.png | /MainBusiness/Index | 200 |
| cis-medicalrecord-index.png | /MedicalRecord/Index | 200 |
| cis-medicalrecord-logquery.png | /MedicalRecord/LogQuery | 200 |
| cis-medicalrecord-medicalrecord.png | /MedicalRecord/MedicalRecord | 200 |
| cis-nursemanage-allergy-allergyinfoquery.png | /NurseManage/Allergy/AllergyInfoQuery | 200 |
| cis-nursemanage-allergy-inhosallergymanage.png | /NurseManage/Allergy/InHosAllergyManage | 200 |
| cis-nursemanage-allergy-outhosallergymanage.png | /NurseManage/Allergy/OutHosAllergyManage | 200 |
| cis-nursemanage-bedlevelcharge-index.png | /NurseManage/BedLevelCharge/Index | 200 |
| cis-nursemanage-departmentsupplies-expiredstoragequery.png | /NurseManage/DepartmentSupplies/ExpiredStorageQuery | 200 |
| cis-nursemanage-departmentsupplies-storagequery.png | /NurseManage/DepartmentSupplies/StorageQuery | 200 |
| cis-nursemanage-departmentsupplies-wzsldindex.png | /NurseManage/DepartmentSupplies/WzSldIndex | 200 |
| cis-nursemanage-dietaryadvice-dietbase.png | /NurseManage/DietaryAdvice/DietBase | 200 |
| cis-nursemanage-entryregistration-index.png | /NurseManage/EntryRegistration/Index | 200 |
| cis-nursemanage-entryregistration-recalloutarea.png | /NurseManage/EntryRegistration/RecallOutArea | 200 |
| cis-nursemanage-excereportprint-index.png | /NurseManage/ExceReportPrint/Index | 200 |
| cis-nursemanage-excereportprint-queryprintindex.png | /NurseManage/ExceReportPrint/QueryPrintIndex | 200 |
| cis-nursemanage-infusion-drugreactmanage.png | /NurseManage/Infusion/DrugReactManage | 200 |
| cis-nursemanage-infusion-infusionhistory.png | /NurseManage/Infusion/InfusionHistory | 200 |
| cis-nursemanage-infusion-injection.png | /NurseManage/Infusion/Injection | 200 |
| cis-nursemanage-inpatientlist-inhosqueryindex.png | /NurseManage/InpatientList/InHosQueryIndex | 200 |
| cis-nursemanage-inpatientlist-index.png | /NurseManage/InpatientList/Index | 200 |
| cis-nursemanage-inpatientlist-inpatient.png | /NurseManage/InpatientList/Inpatient | 200 |
| cis-nursemanage-inpatientmedreturn-drug_withdrawal.png | /NurseManage/InpatientMedReturn/Drug_withdrawal | 200 |
| cis-nursemanage-inpatientmedreturn-index.png | /NurseManage/InpatientMedReturn/Index | 200 |
| cis-nursemanage-inpatientvitalsigns-nursinginput.png | /NurseManage/InpatientVitalSigns/NursingInput | 200 |
| cis-nursemanage-inpatientvitalsigns-queryindex.png | /NurseManage/InpatientVitalSigns/QueryIndex | 200 |
| cis-nursemanage-inpatientvitalsigns-temperaturechartindex.png | /NurseManage/InpatientVitalSigns/TemperatureChartIndex | 200 |
| cis-nursemanage-orderaudit-index.png | /NurseManage/OrderAudit/Index | 200 |
| cis-nursemanage-orderaudit-inputresultsskintest.png | /NurseManage/OrderAudit/InputresultsSkintest | 200 |
| cis-nursemanage-orderexecution-index.png | /NurseManage/OrderExecution/Index | 200 |
| cis-nursemanage-orderexecution-medicalskillexecution.png | /NurseManage/OrderExecution/MedicalSkillExecution | 200 |
| cis-nursemanage-orderexecution-medicalskillquery.png | /NurseManage/OrderExecution/MedicalSkillQuery | 200 |
| cis-nursemanage-outpatientconsult-consultcall.png | /NurseManage/OutpatientConsult/ConsultCall | 200 |
| cis-nursemanage-outpatientconsult-consultdoctor.png | /NurseManage/OutpatientConsult/ConsultDoctor | 200 |
| cis-nursemanage-outpatientconsult-index.png | /NurseManage/OutpatientConsult/Index | 200 |
| cis-nursemanage-preparemedicine-medicineusequery.png | /NurseManage/PrepareMedicine/MedicineUseQuery | 200 |
| cis-nursemanage-profitandloss-queryprofitandloss.png | /NurseManage/ProfitAndLoss/QueryProfitAndLoss | 200 |
| cis-nursemanage-profitandloss-syyy.png | /NurseManage/ProfitAndLoss/Syyy | 200 |
| cis-nursemanage-profitandloss-index.png | /NurseManage/ProfitAndLoss/index | 200 |
| cis-nursemanage-stockcarryover-index.png | /NurseManage/StockCarryOver/index | 200 |
| cis-nursemanage-stockinventory-index.png | /NurseManage/StockInventory/index | 200 |
| cis-nursemanage-wardmaintenance-outareadiagnoosis.png | /NurseManage/WardMaintenance/OutAreaDiagnoosis | 200 |
| cis-nursemanage-wardmaintenance-wardwardroombedmaintenance.png | /NurseManage/WardMaintenance/WardWardRoomBedMaintenance | 200 |
| cis-outpatientnurse-performindex.png | /OutpatientNurse/PerformIndex | 200 |
| cis-outpatientnurse-skintestquery.png | /OutpatientNurse/Skintestquery | 200 |
| cis-outpatientnurse-observationquery.png | /OutpatientNurse/observationQuery | 200 |
| cis-outpatientnurse-prescriptionquery.png | /OutpatientNurse/prescriptionquery | 200 |
| cis-patientlist-index.png | /PatientList/Index | 200 |
| cis-patientlist-inpatientlist.png | /PatientList/InpatientList | 200 |
| cis-patientlist-patientvisitlist.png | /PatientList/PatientVisitList | 200 |
| cis-prescription-index.png | /Prescription/Index | 200 |
| cis-queuemanage-queueinfo-index.png | /QueueManage/QueueInfo/Index | 200 |
| cis-reportmanage-report-dispensingquery.png | /ReportManage/Report/DispensingQuery | 200 |
| cis-reportmanage-report-index.png | /ReportManage/Report/Index | 200 |
| cis-systemmanage-auxiliarydictionary-index.png | /SystemManage/AuxiliaryDictionary/Index | 200 |
| cis-systemmanage-bespeakregistermanage-bespeakregister.png | /SystemManage/BespeakRegisterManage/BespeakRegister | 200 |
| cis-systemmanage-bespeakregistermanage-index.png | /SystemManage/BespeakRegisterManage/Index | 200 |
| cis-systemmanage-bodyparts-index.png | /SystemManage/BodyParts/Index | 200 |
| cis-systemmanage-doctorremark-index.png | /SystemManage/DoctorRemark/Index | 200 |
| cis-systemmanage-inspectioncategory-chat.png | /SystemManage/InspectionCategory/Chat | 200 |
| cis-systemmanage-inspectioncategory-index.png | /SystemManage/InspectionCategory/Index | 200 |
| cis-systemmanage-usagelinkage-index.png | /SystemManage/UsageLinkage/Index | 200 |
| cis-systemmanage-visitdeptset-index.png | /SystemManage/VisitDeptSet/Index | 200 |
| cis-templatemanage-diagnostictemplate-index.png | /TemplateManage/DiagnosticTemplate/Index | 200 |
| cis-templatemanage-doctorservicetemplate-index.png | /TemplateManage/DoctorServiceTemplate/Index | 200 |
| cis-templatemanage-grouppackage-index-type-jc.png | /TemplateManage/GroupPackage/Index?type=jc | 200 |
| cis-templatemanage-grouppackage-index-type-jy.png | /TemplateManage/GroupPackage/Index?type=jy | 200 |
| cis-templatemanage-inspectiontemplate-index-type-jc.png | /TemplateManage/InspectionTemplate/Index?type=jc | 200 |
| cis-templatemanage-inspectiontemplate-index-type-jy.png | /TemplateManage/InspectionTemplate/Index?type=jy | 200 |
| cis-templatemanage-mrecordtemplate-index.png | /TemplateManage/MRecordTemplate/Index | 200 |
| cis-templatemanage-prestemplate-index.png | /TemplateManage/PresTemplate/Index | 200 |

### Pass 2 — Form companions / deeper pages (112)

| File | URL | HTTP |
|---|---|---|
| cis-doctormanage-doctorsadvice-advicelist.png | /DoctorManage/DoctorsAdvice/AdviceList | 200 |
| cis-doctormanage-doctorsadvice-advicelistlook.png | /DoctorManage/DoctorsAdvice/AdviceListLook | 200 |
| cis-doctormanage-doctorsadvice-advicestop.png | /DoctorManage/DoctorsAdvice/AdviceStop | 200 |
| cis-doctormanage-doctorsadvice-continueprint.png | /DoctorManage/DoctorsAdvice/ContinuePrint | 200 |
| cis-doctormanage-doctorsadvice-historicalorders.png | /DoctorManage/DoctorsAdvice/HistoricalOrders | 200 |
| cis-doctormanage-doctorsadvice-patienthistoricalorders.png | /DoctorManage/DoctorsAdvice/PatientHistoricalOrders | 200 |
| cis-doctormanage-doctorsadvice-settinfoshow.png | /DoctorManage/DoctorsAdvice/SettInfoShow | 200 |
| cis-doctormanage-doctorsadvice-templatepresform.png | /DoctorManage/DoctorsAdvice/TemplatePresForm | 200 |
| cis-doctormanage-doctorsadvice-transferwardstop.png | /DoctorManage/DoctorsAdvice/TransferWardStop | 200 |
| cis-doctormanage-electronicprescription-backform.png | /DoctorManage/ElectronicPrescription/backForm | 200 |
| cis-doctormanage-electronicprescription-cfform.png | /DoctorManage/ElectronicPrescription/cfForm | 200 |
| cis-doctormanage-electronicprescription-examform.png | /DoctorManage/ElectronicPrescription/examForm | 200 |
| cis-doctormanage-electronicprescription-medicineform.png | /DoctorManage/ElectronicPrescription/medicineForm | 200 |
| cis-doctormanage-operation-arrangeform.png | /DoctorManage/Operation/ArrangeForm | 200 |
| cis-mainbusiness-cmmalertform.png | /MainBusiness/CmmAlertForm | 200 |
| cis-mainbusiness-tcmhis.png | /MainBusiness/TcmHis | 200 |
| cis-medicalrecord-admissionnoticeform.png | /MedicalRecord/AdmissionNoticeForm | 200 |
| cis-medicalrecord-advanceremind.png | /MedicalRecord/AdvanceRemind | 200 |
| cis-medicalrecord-basemedicinequery.png | /MedicalRecord/BaseMedicineQuery | 200 |
| cis-medicalrecord-blcontinueprint.png | /MedicalRecord/BlContinuePrint | 200 |
| cis-medicalrecord-blctform.png | /MedicalRecord/BlctForm | 200 |
| cis-medicalrecord-commondiag.png | /MedicalRecord/CommonDiag | 200 |
| cis-medicalrecord-getlispeportform.png | /MedicalRecord/GetlisPeportForm | 200 |
| cis-medicalrecord-historymedicalrecordform.png | /MedicalRecord/HistoryMedicalRecordForm | 200 |
| cis-medicalrecord-lissqdhqueryform.png | /MedicalRecord/LisSqdhQueryForm | 200 |
| cis-medicalrecord-outpatienttelemedicine.png | /MedicalRecord/OutpatientTelemedicine | 200 |
| cis-medicalrecord-pacssqdhqueryform.png | /MedicalRecord/PacsSqdhQueryForm | 200 |
| cis-medicalrecord-reservationform.png | /MedicalRecord/ReservationForm | 200 |
| cis-medicalrecord-telxdreport.png | /MedicalRecord/TelxdReport | 200 |
| cis-medicalrecord-toothbitmap.png | /MedicalRecord/ToothBitmap | 200 |
| cis-medicalrecord-viewxindianlist.png | /MedicalRecord/ViewXindianList | 200 |
| cis-nursemanage-allergy-allergyform.png | /NurseManage/Allergy/AllergyForm | 200 |
| cis-nursemanage-entryregistration-changeareaform.png | /NurseManage/EntryRegistration/ChangeAreaForm | 200 |
| cis-nursemanage-entryregistration-entryareafrom.png | /NurseManage/EntryRegistration/EntryAreaFrom | 200 |
| cis-nursemanage-excereportprint-printedcontinue.png | /NurseManage/ExceReportPrint/PrintedContinue | 200 |
| cis-nursemanage-infusion-injectionform.png | /NurseManage/Infusion/InjectionForm | 200 |
| cis-nursemanage-infusion-printbottlelabel.png | /NurseManage/Infusion/PrintBottleLabel | 200 |
| cis-nursemanage-infusion-observationfrom.png | /NurseManage/Infusion/observationFrom | 200 |
| cis-nursemanage-inpatientlist-inhosbookkeep.png | /NurseManage/InpatientList/InHosBookkeep | 200 |
| cis-nursemanage-inpatientlist-inhosfee.png | /NurseManage/InpatientList/InHosFee | 200 |
| cis-nursemanage-inpatientlist-inhosfeerefund.png | /NurseManage/InpatientList/InHosFeeRefund | 200 |
| cis-nursemanage-inpatientlist-inhosmedreturn.png | /NurseManage/InpatientList/InHosMedReturn | 200 |
| cis-nursemanage-inpatientlist-inhosnursinginput.png | /NurseManage/InpatientList/InHosNursingInput | 200 |
| cis-nursemanage-inpatientlist-inhosprint.png | /NurseManage/InpatientList/InHosPrint | 200 |
| cis-nursemanage-inpatientlist-inhosrecalloutarea.png | /NurseManage/InpatientList/InHosRecallOutArea | 200 |
| cis-nursemanage-inpatientlist-inhosregistration.png | /NurseManage/InpatientList/InHosRegistration | 200 |
| cis-nursemanage-inpatientlist-indexcenter.png | /NurseManage/InpatientList/IndexCenter | 200 |
| cis-nursemanage-inpatientlist-medicalorderquery.png | /NurseManage/InpatientList/MedicalorderQuery | 200 |
| cis-nursemanage-inpatientlist-orderaudit.png | /NurseManage/InpatientList/OrderAudit | 200 |
| cis-nursemanage-inpatientlist-orderexecution.png | /NurseManage/InpatientList/OrderExecution | 200 |
| cis-nursemanage-inpatientvitalsigns-form.png | /NurseManage/InpatientVitalSigns/Form | 200 |
| cis-nursemanage-inpatientvitalsigns-fullscreentemperaturechartindex.png | /NurseManage/InpatientVitalSigns/FullScreenTemperatureChartIndex | 200 |
| cis-nursemanage-orderaudit-audittips.png | /NurseManage/OrderAudit/AuditTips | 200 |
| cis-nursemanage-orderaudit-inputresults.png | /NurseManage/OrderAudit/Inputresults | 200 |
| cis-nursemanage-orderaudit-preparationstock.png | /NurseManage/OrderAudit/PreparationStock | 200 |
| cis-nursemanage-orderaudit-preparemedicineapply.png | /NurseManage/OrderAudit/PrepareMedicineApply | 200 |
| cis-nursemanage-orderaudit-skintestinfo.png | /NurseManage/OrderAudit/SkinTestInfo | 200 |
| cis-nursemanage-orderexecution-bindingfeeform.png | /NurseManage/OrderExecution/BindingFeeForm | 200 |
| cis-nursemanage-outpatientconsult-consultinfo.png | /NurseManage/OutpatientConsult/ConsultInfo | 200 |
| cis-nursemanage-preparemedicine-preparemedicinereturn.png | /NurseManage/PrepareMedicine/PrepareMedicineReturn | 200 |
| cis-nursemanage-preparemedicine-preparemedicinereturnadd.png | /NurseManage/PrepareMedicine/PrepareMedicineReturnAdd | 200 |
| cis-nursemanage-preparemedicine-reportlossandprofit.png | /NurseManage/PrepareMedicine/ReportLossAndProfit | 200 |
| cis-nursemanage-preparemedicine-reportlossandprofitquery.png | /NurseManage/PrepareMedicine/ReportLossAndProfitQuery | 200 |
| cis-nursemanage-profitandloss-syyyform.png | /NurseManage/ProfitAndLoss/SyyyForm | 200 |
| cis-nursemanage-stockinventory-inventoryaction.png | /NurseManage/StockInventory/InventoryAction | 200 |
| cis-nursemanage-stockinventory-inventoryquery.png | /NurseManage/StockInventory/InventoryQuery | 200 |
| cis-nursemanage-wardmaintenance-attributesplusbedform.png | /NurseManage/WardMaintenance/AttributesPlusBedForm | 200 |
| cis-nursemanage-wardmaintenance-attributesplusroomform.png | /NurseManage/WardMaintenance/AttributesPlusRoomForm | 200 |
| cis-nursemanage-wardmaintenance-bedbindingcostform.png | /NurseManage/WardMaintenance/BedBindingCostForm | 200 |
| cis-nursemanage-wardmaintenance-bedform.png | /NurseManage/WardMaintenance/BedForm | 200 |
| cis-nursemanage-wardmaintenance-doctorbindingform.png | /NurseManage/WardMaintenance/DoctorBindingForm | 200 |
| cis-nursemanage-wardmaintenance-inareadiagnoosisform.png | /NurseManage/WardMaintenance/InAreaDiagnoosisForm | 200 |
| cis-nursemanage-wardmaintenance-inhospitalbedcard.png | /NurseManage/WardMaintenance/InHospitalBedCard | 200 |
| cis-nursemanage-wardmaintenance-inhospitalbedcardprint.png | /NurseManage/WardMaintenance/InHospitalBedCardPrint | 200 |
| cis-nursemanage-wardmaintenance-outareadiagnoosisform.png | /NurseManage/WardMaintenance/OutAreaDiagnoosisForm | 200 |
| cis-nursemanage-wardmaintenance-outareaform.png | /NurseManage/WardMaintenance/OutAreaForm | 200 |
| cis-nursemanage-wardmaintenance-roomform.png | /NurseManage/WardMaintenance/RoomForm | 200 |
| cis-outpatientnurse-outpatientpricing.png | /OutpatientNurse/OutpatientPricing | 200 |
| cis-patientlist-inpatsearchview.png | /PatientList/InPatSearchView | 200 |
| cis-patientlist-inpatientinfo.png | /PatientList/InpatientInfo | 200 |
| cis-patientlist-inpatientinfoshow.png | /PatientList/InpatientInfoShow | 200 |
| cis-patientlist-patientlist.png | /PatientList/PatientList | 200 |
| cis-prescription-addkfjyform.png | /Prescription/AddKfjyForm | 200 |
| cis-prescription-consumablesprescription.png | /Prescription/ConsumablesPrescription | 200 |
| cis-prescription-historypresform.png | /Prescription/HistoryPresForm | 200 |
| cis-prescription-regularitemprescription.png | /Prescription/RegularItemPrescription | 200 |
| cis-prescription-rehabprescription.png | /Prescription/RehabPrescription | 200 |
| cis-prescription-tcmprescription.png | /Prescription/TCMPrescription | 200 |
| cis-prescription-tcmprescription2022.png | /Prescription/TCMPrescription2022 | 200 |
| cis-prescription-wmprescription.png | /Prescription/WMPrescription | 200 |
| cis-reportmanage-report-mrreport.png | /ReportManage/Report/MRReport | 200 |
| cis-reportmanage-report-regularitempresreport.png | /ReportManage/Report/RegularItemPresReport | 200 |
| cis-reportmanage-report-rehabpresreport.png | /ReportManage/Report/RehabPresReport | 200 |
| cis-reportmanage-report-rehabprestreatmentreport.png | /ReportManage/Report/RehabPresTreatmentReport | 200 |
| cis-reportmanage-report-reportconfig.png | /ReportManage/Report/ReportConfig | 200 |
| cis-reportmanage-report-reportconfigform.png | /ReportManage/Report/ReportConfigForm | 200 |
| cis-reportmanage-report-tcmpresreport.png | /ReportManage/Report/TCMPresReport | 200 |
| cis-reportmanage-report-wmpresreport.png | /ReportManage/Report/WMPresReport | 200 |
| cis-systemmanage-bodyparts-jcform.png | /SystemManage/BodyParts/JcForm | 200 |
| cis-templatemanage-doctorservicetemplate-foodform.png | /TemplateManage/DoctorServiceTemplate/FoodForm | 200 |
| cis-templatemanage-doctorservicetemplate-itemform.png | /TemplateManage/DoctorServiceTemplate/ItemForm | 200 |
| cis-templatemanage-doctorservicetemplate-medicineform.png | /TemplateManage/DoctorServiceTemplate/MedicineForm | 200 |
| cis-templatemanage-doctorservicetemplate-rehabform.png | /TemplateManage/DoctorServiceTemplate/RehabForm | 200 |
| cis-templatemanage-doctorservicetemplate-tcmmedicineform.png | /TemplateManage/DoctorServiceTemplate/TCMmedicineForm | 200 |
| cis-templatemanage-doctorservicetemplate-takemedicineform.png | /TemplateManage/DoctorServiceTemplate/TakeMedicineForm | 200 |
| cis-templatemanage-doctorservicetemplate-wordform.png | /TemplateManage/DoctorServiceTemplate/WordForm | 200 |
| cis-templatemanage-prestemplate-prestemplatedetail.png | /TemplateManage/PresTemplate/PresTemplateDetail | 200 |
| cis-templatemanage-prestemplate-regularitemform.png | /TemplateManage/PresTemplate/RegularItemForm | 200 |
| cis-templatemanage-prestemplate-rehabform.png | /TemplateManage/PresTemplate/RehabForm | 200 |
| cis-templatemanage-prestemplate-tcmform.png | /TemplateManage/PresTemplate/TCMForm | 200 |
| cis-templatemanage-prestemplate-wmform.png | /TemplateManage/PresTemplate/WMForm | 200 |
| cis-templatemanage-prestemplate-xzyyform.png | /TemplateManage/PresTemplate/XzyyForm | 200 |

---

## Ghi chú soát tiếng Trung CIS — 2026-10-27 (worker afbda5db, chỉ sửa text, không build/chạy/commit)

- Phạm vi: toàn bộ Newtouch.HIS.CIS — Web (311 .cshtml + .cs/.js), API, Infrastructure (757 .cs). Quét 1.186 file (trừ bin/obj); encoding tất cả UTF-8/ASCII, không file nào không dám sửa vì encoding.
- Trước: Web 7.564 dòng CJK, API 2 dòng, Infra 6.441 dòng (tổng 14.007 dòng / 89.376 ký tự CJK). Sau: Web 259, API 0, Infra 575 (tổng 834 dòng / 1.083 ký tự) — giảm ~94% số dòng CJK.
- Đã dịch: ~23.391 lượt thay trong comment + ~2.219 lượt chuỗi hiển thị/thông báo (chuỗi UI, message throw/return, #region, log label). 841 file đổi; số dòng mỗi file không đổi (git diff insertions == deletions, 0 dòng xóa không chứa CJK).
- Còn lại là keeps, không dịch: mã logic/so sánh (临/长, 男/女, 是/否, 已执行…), literal trong chuỗi SQL và SQL CASE, toàn bộ [Description(...)] của enum (kể cả enum dùng làm tiền tố số chứng từ — chỉ dịch comment xung quanh), dữ liệu demo (hàng mẫu giường bệnh, bảng số Hán trong utility.js), tên riêng vendor (Kim Phong Dịch Thông PACS/LIS, tiêu đề giấy phép Thiên Thiên mã nguồn mở, tác giả file), ký hiệu 卐 trong HTML bảng hẹn, JS vendor/minified (0 dòng CJK).
- Dữ liệu tiếng Trung từ database vẫn còn và cần quyết định ghi DB riêng: giới tính, loại y lệnh, trạng thái y lệnh/đăng ký/đồng bộ, kết quả test da, giá trị ăn uống, loại khám, nhóm thuốc — code không che các giá trị này.
- Ngoài phạm vi, không sửa: _porting-src/Shared (24 file, bản sao tham chiếu porting; bản live là Web/Views/Shared), sweep-scripts/port-*.py/translate-cis.py (tooling), *.log, bin/obj.
- Hạn chế: bản dịch ở mức từ/cụm (nhiều comment còn câu Việt pha mảnh cũ từ các đợt trước); không build/chạy app để kiểm chứng trực quan theo brief.

## Parity audit CIS 2026-10-27 (worker CIS, không commit, không ghi DB)
- Đối chiếu với V2.2 (git show V2.2, không dùng working tree dich-tieng-viet): cây gốc có thư mục chết App_Start/Areas (172 file, 0 include trong csproj) — đã loại khỏi đếm. Live: controller 60/60 file, view 308/308 (port +2 file infra _ViewImports/_ViewStart); field đếm thật trên 308 view cặp: input 1362=1362, select 164=164, textarea 51=51, name= 801=801, colModel 309=309, 0 view lệch (chỉ 3 partial Shared lệch jqGrid +2 do đoạn fetch clients dự phòng của port, không phải field).
- API: 13/13 controller, 27/27 action; tham số giống hệt (chỉ khác tên method C# TCM_HIS_01→TcmHis01, route HttpPost("TCM_HIS_01") giữ nguyên); DTO live đủ thuộc tính (các lệch chỉ là type chết: ActResultDTO 0 usage, OperateTime đã comment-out ở gốc, TcmHis01Request action đã comment-out, OutpatientCFDRequestDTO 0 usage, OrgRequestBase đã gộp vào subclass, MqGeneralTaskBody nằm ở Infrastructure). Envelope 10000/40004 + token BODY Token/TokenType, Redis key = raw token, value JSON UserCode/OrganizeId/TokenType (TokenStore, gia hạn 20 phút, UA 120 phút) — đã kiểm chứng live.
- ĐÃ FIX (build Debug 0 lỗi, Web khởi động lại PID 14324, cấu hình Debug như launcher): HomeController thêm GetNewFieldUniqueValue, MSGQuery, NoticeReadSync; LoginController thêm UALogin (theo tiền lệ OR, fallback về trang Login vì Core không có QuickLoginSuccess) + ReturnSSOHome (redirect SSOHomeHost nếu cấu hình, ngược lại về Login). GetUALoginAddres + OutLogin vốn đã kế thừa từ LoginBaseController (không phải gap).
- Live đã kiểm chứng (000000/000000): login → Home/Index 200; GetNewFieldUniqueValue → success data "00000002"; MSGQuery → 200 []; NoticeReadSync rỗng → error "Cập nhật trạng thái đã đọc thất bại" (đã tới 8016, data=false — đúng nhánh lỗi của gốc); GetUALoginAddres?appId=CIS → success data null; UALogin không token → về trang Login; ReturnSSOHome → về trang Login (chưa cấu hình SSOHomeHost); OutLogin 200; các trang PatientList/DoctorsAdvice/MainBusiness/Prescription/OrderAudit/ClinicApply Index đều render khi đã đăng nhập; ClinicApply.Accept rỗng dừng ở cổng "Không tìm thấy thông tin xếp lịch" (chưa gọi 8016, không ghi).
- MzBespeakRegister XÁC NHẬN LẠI, KHÔNG SỬA: gốc không gắn [DefaultAuthorize]; no-token + đủ field bắt buộc → 20000/INTERNAL_SERVER_ERROR NRE (trung thực với gốc); fake token → TOKEN_IS_EXPIRED; thiếu Timestamp → TIMESTAMP_IS_REQUIRED; thiếu TokenType → TOKEN_TYPE_IS_REQUIRED; GetToken → 40004 GETTOKEN_DISABLED.
- Hội chẩn từ xa: ClinicApply.Accept → 8016 api/Outpatient/OutpatientRegist → api/RemoteTreated/DoctorMeetingApply (chân 8016 đã được center-agent kiểm chứng: fake applyId → 40004 validation, không ghi); phía CIS dừng ở cổng xếp lịch do demo DB không có lịch khoa 00000080 nên chưa tới được chân 8016 trong lần kiểm này. ClinicMetting/Index là shell rỗng ở cả gốc (223 byte) lẫn port (226 byte) — ảnh trắng là trung thực, không phải lỗi.
- CÒN THIẾU, CÓ LÝ DO (không giấu): (1) Home.SyncSysConfigParams — view gọi nó (SysConfig/Index) không có controller ở cả gốc lẫn port (SSO phục vụ Sys*), action này INSERT, và entity port SysGlobalConfigEntity không có cột OrganizeId cho logic copy theo org. (2) SSOCallback — SSO net6 (5208) không có endpoint GetOpenId, đăng nhập SSO của port đi qua SSO + CheckLogin. (3) UALoginPre dạng action — cần QuickLoginSuccess (không có tương đương Core), không view nào gọi. (4) Home/Index port vẫn là landing tối giản, không phải portal frame gốc (frame cần FrameworkBase HomeController + menu BaseAPI + SignalR notice hub đã stub) — SSO portal phục vụ điều hướng thay thế; các partial _IndexHeaderNav/_IndexNavCol đã port nhưng không còn layout nào dùng. (5) Lệch đã biết, không sửa trong đợt này: [ApiController] toàn port khiến field [Required] thiếu trả HTTP 400 ProblemDetails thay vì envelope {FIELD}_IS_REQUIRED (chỉ lệch dạng lỗi của request không hợp lệ; envelope của request hợp lệ vẫn đúng).
- Ảnh mới (viewport, không ghi đè): cis-systemmanage-auxiliarydictionary-form-fixed.png, cis-clinicmanage-clinicmetting-index-fixed.png, cis-home-index-fixed.png — đã tự xem cả 3 (form tiếng Việt đầy đủ field; home landing tiếng Việt; clinicmetting trắng đúng như gốc).
- Không commit; không sửa app khác, không sửa root (trừ ghi chú này); không ghi DB (mọi luồng ghi chỉ tới cổng validation).
