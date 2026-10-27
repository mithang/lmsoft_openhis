# PDS Web — screenshot progress (role HospAdministrator, user 000000)

- App: PDS Web on http://127.0.0.1:5237, login as 000000 (cookie flow: POST /Login/CheckLogin with md5 via md5sum; Playwright+Firefox, viewport 1366x900, never full_page).
- Date: 2026-10-27. Restarts: NONE — PDS 5237 was UP (HTTP 200) at start and stayed UP throughout; no other app touched.
- Shots: 127 PNG files in ~/workspace/openhis-core/shots-roles/ (all NEW files, no older shot dir overwritten).
- Shots personally viewed and confirmed rendered (not blank, not error): 5 — pds-login.png, pds-home.png, pds-medicine-medicineinfo.png, pds-outorinstoredmanage-purchase-form.png, pds-report-printreport.png.
- Empty grids on empty demo tables are CORRECT (e.g. MedicineInfo shows 'Không tìm thấy dữ liệu liên quan!').
- Vendor branding Chinese text on the login logo (天天开源) and home footer copyright line is vendor branding, not UI content — noted once, not a shot failure.

## 1. Menu URLs found — method 2: Sys_Module rows in demo DB NewtouchHIS_PDS (82 distinct URLs)

- /AntibioticType/AntibioticTypeQuery
- /BillManage/ApplyBill/Query
- /BillManage/OutOrInStorageBill/Approval
- /BillManage/OutOrInStorageBill/Query
- /Dictionaries/FypzIndex
- /Dictionaries/KsFypzIndex
- /Dictionaries/syyy
- /DrugStorage/ApplyDelivery
- /DrugStorage/DirectDeliveryBatch
- /DrugStorage/HandOutMedicineInfo
- /DrugStorage/PriceAdjustmentApproval
- /DrugStorage/PriceAdjustmentHistory
- /DrugStorage/PriceAdjustmentRequest
- /DrugStorage/PSIStatistics2018
- /DrugStorage/StockCarryDown
- /HospitalizationPharmacy/DispenseIndex
- /HospitalizationPharmacy/FybdIndex
- /HospitalizationPharmacy/PrescriptionPadQuery
- /HospitalizationPharmacy/QueryIndex
- /HospitalizationPharmacy/QueryIndexV2
- /HospitalizationPharmacy/RepercussionIndex
- /HospitalizationPharmacy/RepercussionIndex2021
- /Medicine/InventoryQureyReport
- /Medicine/MedicineInfo
- /Medicine/PharmaceuticalStocks
- /Medicine/PriceAdjustmentProfitAndLossQuery
- /Medicine/ReportLossAndProfit
- /Medicine/ReportLossAndProfitQuery
- /OutOrInStoredManage/Apply/Index
- /OutOrInStoredManage/DeliveryDirect/Index
- /OutOrInStoredManage/DeliveryToDepartment/Index
- /OutOrInStoredManage/InStorage/Index
- /OutOrInStoredManage/Purchase/Bill
- /OutOrInStoredManage/Purchase/Index
- /OutOrInStoredManage/Purchase/PurchaseApproval
- /OutOrInStoredManage/Purchase/PurchasePlan
- /OutOrInStoredManage/ReturnInward/Index
- /OutOrInStoredManage/ReturnInwardToGys/Index
- /OutOrInStoredManage/Transfer/Apply
- /OutOrInStoredManage/Transfer/Warehousing
- /OutPatientPharmacy/DrugArrangement2019
- /OutPatientPharmacy/DrugArrangementQuery
- /OutPatientPharmacy/DrugDelivery2018
- /OutPatientPharmacy/DrugRepercussion
- /OutPatientPharmacy/DrugSearch
- /OutPatientPharmacy/ElectronicPrescription
- /OutPatientPharmacy/MzPrescriptionQuery
- /OutPatientPharmacy/OutpatientDrugArrangementForzenQuery
- /OutPatientPharmacy/RePrintMedicineDoc
- /PharmacyDepartment/Index
- /PharmacyDrugStorage/StockInventoryV2
- /PharmacyDrugStorage/StockInventoryV3
- /PharmacyWindow/Index
- /PurchaseManage/Purchase/LocationIndex
- /ReportManage/GqYpcx/GqYpcx
- /ReportManage/GqYpcx/IteamSumQuery
- /ReportManage/JsYpcx/QxIndex
- /ReportManage/JsYpcx/YPCKMXIndex
- /ReportManage/KsYpckQuery/KsYpckQuery
- /ReportManage/Outpatient/DrugDeliveryStatistics
- /ReportManage/Purchase/PurchaseBillDetailQuery
- /ReportManage/YpckQuery/AntibioticCFQuery
- /ReportManage/YpckQuery/AntibioticQuery
- /ReportManage/YpckQuery/CFQuantity
- /ReportManage/YpckQuery/DecoctingfeeQuery
- /ReportManage/YpckQuery/DoctorsOrderQuery
- /ReportManage/YpckQuery/DrugSubtotal
- /ReportManage/YpckQuery/HospitalPrescription
- /ReportManage/YpckQuery/OutpatientPrescription
- /ReportManage/YpckQuery/YpckQuery
- /ReportManage/YpckQuery/YPDrugComparisonTable
- /ReportManage/YpckQuery/YPDrugUsage
- /ReportManage/YpckQuery/YPInOutBound
- /ReportManage/YpckQuery/YprkQuery
- /ReportManage/Ypfy/ypfyQuery
- /ReportManage/Zyfytj/QxIndex
- /StorageManage/DrugStockSearch/ExpiredDateManage
- /StorageManage/DrugStockSearch/StockQuery
- /SystemManage/SysConfig/Index
- /SystemManage/SysModule/Index
- /SystemManage/SysRole/Index
- /SystemManage/SysUser/Index

## 2. Menu URLs found — method 1: rendered sidebar after login (Playwright DOM + /ClientsData/GetClientsDataJson authorizeMenu)

91 sidebar links collected in Playwright (90 distinct; /NurseManage/OrderAudit/InputresultsSkintest appears twice).
IMPORTANT FINDING: the rendered sidebar does NOT contain PDS menus. PDS sidebar JS renders `top.clients.authorizeMenu`, which comes from BaseAPI /api/System/GetMenuList for the role — it returns Sett/other-app routes (outpatient, hospitalization, nurse stations, templates, reports), in unaccented Vietnamese. ALL 90 return HTTP 404 on the PDS host (verified by curl with session cookies; 5 samples also attempted in Playwright, which throws NS_ERROR_NET_EMPTY_RESPONSE because the 404 body is empty — see section 4). The PDS menu set is therefore the DB list in section 1, not the rendered sidebar. The home screenshot (pds-home.png) shows these cross-app groups in the sidebar, collapsed.

- /DaySettleManage/DaySettle/ApplicationDetailed
- /DaySettleManage/DaySettle/ApplicationIndex
- /DaySettleManage/DaySettle/DzrIndex
- /DoctorManage/DoctorsAdvice/Index
- /DoctorManage/OutpatientQuery/ConsultDetail
- /DoctorManage/OutpatientQuery/ConsultRecord
- /HospitalizationManage/BookkeepInHos/Index
- /HospitalizationManage/DischargeSettle/CancelIndex
- /HospitalizationManage/DischargeSettle/Index
- /HospitalizationManage/DischargeSettle/PartialSettleIndex
- /HospitalizationManage/DischargeSettle/SimulateIndex
- /HospitalizationManage/InpatientRefund/Index
- /HospitalizationManage/Settlement/HospitalizationSettlementQuery
- /HospitalizationManage/Settlement/InPatientSettQuery
- /MainBusiness/Index
- /MedicalRecord/LogQuery
- /MedicalRecordManage/Outpatient/Index
- /MedicalRecordManage/PatInfo/Index
- /MedicalRecordManage/PatInfo/PatMedRecordList
- /NurseManage/EntryRegistration/Index
- /NurseManage/EntryRegistration/RecallOutArea
- /NurseManage/ExceReportPrint/Index
- /NurseManage/ExceReportPrint/QueryPrintIndex
- /NurseManage/Infusion/InfusionHistory
- /NurseManage/Infusion/Injection
- /NurseManage/InpatientList/Inpatient
- /NurseManage/InpatientMedReturn/Drug_withdrawal
- /NurseManage/InpatientMedReturn/Index
- /NurseManage/InpatientVitalSigns/NursingInput
- /NurseManage/InpatientVitalSigns/QueryIndex
- /NurseManage/InpatientVitalSigns/TemperatureChartIndex
- /NurseManage/OrderAudit/Index
- /NurseManage/OrderAudit/InputresultsSkintest
- /NurseManage/OrderExecution/Index
- /NurseManage/OutpatientConsult/ConsultCall
- /NurseManage/OutpatientConsult/ConsultDoctor
- /NurseManage/OutpatientConsult/Index
- /NurseManage/PrepareMedicine/MedicineUseQuery
- /NurseManage/WardMaintenance/OutAreaDiagnoosis
- /OutpatientManage/OutBook/Index
- /OutpatientManage/OutBook/RosterCalendar
- /OutpatientManage/OutpatCharge/Index2018
- /OutpatientManage/OutpatientChargeQuery/OutpatientChargeQuery
- /OutpatientManage/OutpatientRefund/Index2018
- /OutpatientManage/OutpatientReg/OutpatientReg2018
- /OutpatientNurse/PerformIndex
- /OutpatientNurse/Skintestquery
- /OutpatientNurse/observationQuery
- /OutpatientNurse/prescriptionquery
- /PatientList/InpatientList
- /PatientManage/HospiterRes/Index
- /PatientManage/HospiterRes/Register
- /PatientManage/Inpatient/InPatientQueryIndex
- /PatientManage/PatientCenter/Index
- /ReportManage/Report/AllMedicineInsuranceQuery
- /ReportManage/Report/DispensingQuery
- /ReportManage/Report/FYXMMXQuery
- /ReportManage/Report/HosFeeSettSummry
- /ReportManage/Report/Hospitalizationclassification
- /ReportManage/Report/Hospitalizationcost
- /ReportManage/Report/InpatientList
- /ReportManage/Report/MZCBFYFX
- /ReportManage/Report/MZFYRBHZ_Query
- /ReportManage/Report/MZJBFYFX
- /ReportManage/Report/OutpatientDailyFee
- /ReportManage/Report/OutpatientQuery
- /ReportManage/Report/OutpatientSituationQuery
- /ReportManage/Report/OutpatientWorkloadQuery
- /ReportManage/Report/PrintReport
- /ReportManage/Report/YBSBHZQDQuery
- /ReportManage/Report/YBSBSC_HZBKQuery
- /ReportManage/Report/YBSBSC_JMQuery
- /ReportManage/Report/YBSBSC_YDQuery
- /ReportManage/Report/YBSBSC_ZGQuery
- /SysManage/SysModule/Index/
- /SysManage/SysRole/Index
- /SysManage/User/Index
- /SystemManage/FinancialInvoice/FinanicalInvoiceIndex
- /SystemManage/FinancialReceipt/Index
- /SystemManage/InpatientAccountManage/Index?zhxz=zy
- /SystemManage/PatientCharacteristics/Index
- /SystemManage/SysParameterConfig/ChargeTemplateIndex
- /TemplateManage/DoctorServiceTemplate/Index
- /TemplateManage/GroupPackage/Index?type=jc
- /TemplateManage/GroupPackage/Index?type=jy
- /TemplateManage/InspectionTemplate/Index?type=jc
- /TemplateManage/InspectionTemplate/Index?type=jy
- /TemplateManage/MRecordTemplate/Index
- /TemplateManage/PresTemplate/Index
- /Treatment/Index

## 3. Shot (127 files, shots-roles/pds-*.png)

Breakdown: login page (logged out) 1 + home 1 + Home/Default 1 + Home/About 1 + DB menu URLs with HTTP 200: 71 + extras (SetUserPharmacyForm, ExpiredDrugsView, ExpiredDrugsView?type=1) 3 + Form companions / non-menu pages 45 + view-sweep extras (OutOrInStorageBill/Form, PharmacyDepartment/Form, SyncDeptDrug, Report/PrintReport, Staff/Selector) 5 = 127. (71+1+1+1+3+45+5 = 127; ExpiredDrugsView plain and ?type=1 kept as separate files after an initial overwrite was fixed.)

- pds-billmanage-applybill-applyoutstock.png
- pds-billmanage-applybill-query.png
- pds-billmanage-outorinstoragebill-approval.png
- pds-billmanage-outorinstoragebill-drugpreparation.png
- pds-billmanage-outorinstoragebill-form.png
- pds-billmanage-outorinstoragebill-outorder.png
- pds-billmanage-outorinstoragebill-query.png
- pds-dictionaries-fypzform.png
- pds-dictionaries-fypzindex.png
- pds-dictionaries-ksfypzform.png
- pds-dictionaries-ksfypzindex.png
- pds-dictionaries-syyy.png
- pds-dictionaries-syyyform.png
- pds-drugstorage-applydelivery.png
- pds-drugstorage-briefinstorage.png
- pds-drugstorage-confirmationofreturn.png
- pds-drugstorage-directdeliverybatch.png
- pds-drugstorage-excelexportchoosecolumns.png
- pds-drugstorage-handoutmedicine.png
- pds-drugstorage-handoutmedicinebyrequest.png
- pds-drugstorage-handoutmedicineinfo.png
- pds-drugstorage-handoutmedicinetoks.png
- pds-drugstorage-instorage.png
- pds-drugstorage-outstorage.png
- pds-drugstorage-previewinfo.png
- pds-drugstorage-priceadjustmentapproval.png
- pds-drugstorage-priceadjustmenthistory.png
- pds-drugstorage-priceadjustmentrequest.png
- pds-drugstorage-psistatistics2018.png
- pds-drugstorage-stockcarrydown.png
- pds-home-about.png
- pds-home-default.png
- pds-home-setuserpharmacyform.png
- pds-home.png
- pds-hospitalizationpharmacy-deptmedicineapplyreturn.png
- pds-hospitalizationpharmacy-deptmedicineapplysend.png
- pds-hospitalizationpharmacy-dispenseindex.png
- pds-hospitalizationpharmacy-fybdindex.png
- pds-hospitalizationpharmacy-prescriptiondetailfrom.png
- pds-hospitalizationpharmacy-prescriptionpadquery.png
- pds-hospitalizationpharmacy-queryindex.png
- pds-hospitalizationpharmacy-queryindexv2.png
- pds-hospitalizationpharmacy-repercussionindex.png
- pds-hospitalizationpharmacy-repercussionindex2021.png
- pds-login.png
- pds-medicine-expireddrugs.png
- pds-medicine-inventoryqureyreport.png
- pds-medicine-medicineinfo.png
- pds-medicine-priceadjustmentprofitandlossquery.png
- pds-medicine-reportlossandprofit.png
- pds-medicine-reportlossandprofitquery.png
- pds-medicine-requestofreturn.png
- pds-medicine-syncdeptdrug.png
- pds-medicine-sysmedicine.png
- pds-outorinstoredmanage-apply-index.png
- pds-outorinstoredmanage-deliverydirect-from.png
- pds-outorinstoredmanage-deliverydirect-index.png
- pds-outorinstoredmanage-deliverytodepartment-index.png
- pds-outorinstoredmanage-instorage-index.png
- pds-outorinstoredmanage-purchase-bill.png
- pds-outorinstoredmanage-purchase-billform.png
- pds-outorinstoredmanage-purchase-billview.png
- pds-outorinstoredmanage-purchase-delivery.png
- pds-outorinstoredmanage-purchase-form.png
- pds-outorinstoredmanage-purchase-index.png
- pds-outorinstoredmanage-purchase-purchaseapproval.png
- pds-outorinstoredmanage-purchase-purchaseplan.png
- pds-outorinstoredmanage-purchase-purchaseplanfrom.png
- pds-outorinstoredmanage-purchase-purchaseview.png
- pds-outorinstoredmanage-returninward-index.png
- pds-outorinstoredmanage-returninwardtogys-index.png
- pds-outpatientpharmacy-choosecf.png
- pds-outpatientpharmacy-drugarrangement.png
- pds-outpatientpharmacy-drugarrangement2018.png
- pds-outpatientpharmacy-drugarrangement2019.png
- pds-outpatientpharmacy-drugarrangementquery.png
- pds-outpatientpharmacy-drugdelivery2018.png
- pds-outpatientpharmacy-drugrepercussion.png
- pds-outpatientpharmacy-drugsearch.png
- pds-outpatientpharmacy-electronicprescription.png
- pds-outpatientpharmacy-mzprescriptionquery.png
- pds-outpatientpharmacy-outpatientdrugarrangementforzenquery.png
- pds-outpatientpharmacy-prescriptiondetailfrom.png
- pds-outpatientpharmacy-reprintmedicinedoc.png
- pds-pharmacy-requisitionform.png
- pds-pharmacy-requisitionsearchindex.png
- pds-pharmacydepartment-form.png
- pds-pharmacydepartment-index.png
- pds-pharmacydrugstorage-inventoryaction.png
- pds-pharmacydrugstorage-inventoryactionv2.png
- pds-pharmacydrugstorage-inventoryquery.png
- pds-pharmacydrugstorage-inventoryqueryv2.png
- pds-pharmacydrugstorage-receiptapproval.png
- pds-pharmacydrugstorage-receiptqueryv2.png
- pds-pharmacydrugstorage-stockinventoryv2.png
- pds-pharmacydrugstorage-stockinventoryv3.png
- pds-purchasemanage-purchase-locationform.png
- pds-purchasemanage-purchase-locationindex.png
- pds-report-printreport.png
- pds-reportmanage-gqypcx-gqypcx.png
- pds-reportmanage-gqypcx-iteamsumquery.png
- pds-reportmanage-jsypcx-qxindex.png
- pds-reportmanage-jsypcx-ypckmxindex.png
- pds-reportmanage-ksypckquery-ksypckquery.png
- pds-reportmanage-outpatient-drugdeliverystatistics.png
- pds-reportmanage-purchase-purchasebilldetailquery.png
- pds-reportmanage-yf-pharmacymonthlyquery.png
- pds-reportmanage-ypckquery-antibioticcfquery.png
- pds-reportmanage-ypckquery-antibioticquery.png
- pds-reportmanage-ypckquery-cfquantity.png
- pds-reportmanage-ypckquery-drugsubtotal.png
- pds-reportmanage-ypckquery-hospitalprescription.png
- pds-reportmanage-ypckquery-outpatientprescription.png
- pds-reportmanage-ypckquery-ypckquery.png
- pds-reportmanage-ypckquery-ypckquerynew.png
- pds-reportmanage-ypckquery-ypdrugcomparisontable.png
- pds-reportmanage-ypckquery-ypdrugusage.png
- pds-reportmanage-ypckquery-ypinoutbound.png
- pds-reportmanage-ypckquery-yprkquery.png
- pds-reportmanage-ypfy-ypfyquery.png
- pds-reportmanage-zyfytj-qxindex.png
- pds-staff-selector.png
- pds-storagemanage-drugstocksearch-expireddateform.png
- pds-storagemanage-drugstocksearch-expireddatemanage.png
- pds-storagemanage-drugstocksearch-expireddrugsview-type1.png
- pds-storagemanage-drugstocksearch-expireddrugsview.png
- pds-storagemanage-drugstocksearch-stockquery.png

## 4. Failed with reason (no screenshot possible or no screenshot needed)

All of these return HTTP 404 with an EMPTY body (curl-verified with session cookies). Firefox/Playwright goto throws NS_ERROR_NET_EMPTY_RESPONSE on an empty 404 body, so no error-page screenshot exists to take — the evidence is the status + empty body, recorded here. Two retries were not needed: the 404 is deterministic (missing controller/action), confirmed by curl + source.

### 4a. Compile-removed by design (csproj lines 63-72) — 8 DB menu URLs
- /AntibioticType/AntibioticTypeQuery — AntibioticTypeController removed
- /PharmacyWindow/Index — PharmacyWindowController removed
- /OutOrInStoredManage/Transfer/Apply and /OutOrInStoredManage/Transfer/Warehousing — area TransferController removed
- /SystemManage/SysConfig/Index, /SystemManage/SysModule/Index, /SystemManage/SysRole/Index, /SystemManage/SysUser/Index — SystemManagerController removed
- Header link /SystemManage/SysUser/PersonalInfo — same removal (UserController removed); also 404.
- Other compile-removed controllers with no DB menu URL (views exist, routes 404): Module, ModuleButton, Organize, RoleAuthorize, Role, User, SysMedicine — see 4c view list.

### 4b. Port gaps (action missing in ported controller, NOT compile-removed) — 3 DB menu URLs
- /Medicine/PharmaceuticalStocks — no such action in MedicineController
- /ReportManage/YpckQuery/DecoctingfeeQuery — no such action in YpckQueryController (no view file either)
- /ReportManage/YpckQuery/DoctorsOrderQuery — no such action in YpckQueryController (no view file either)

### 4c. Rendered-sidebar URLs — all 90 distinct URLs 404 on PDS host (cross-app Sett routes, see section 2). Samples attempted in Playwright, all empty-404: /OutpatientManage/OutpatientReg/OutpatientReg2018, /HospitalizationManage/DischargeSettle/Index, /NurseManage/InpatientList/Inpatient, /TemplateManage/PresTemplate/Index, /SysManage/SysModule/Index/.

### 4d. View files with no route (404) — 38 view URLs swept, all 404:
- Compile-removed controllers: AntibioticType/AntibioticTypeSettingForm, Module/* (4), ModuleButton/* (5), PharmacyWindow/Form + PharmacyWindowIndex, Role/* (3), SysMedicine/* (3), SysConfig/* (3), User/PersonalInfo + User/Selector.
- Orphan views (no action in ported controller; superseded by newer variants): HospitalizationPharmacy/RepercussionIndex2019, Medicine/SysMedicineAdd, OutPatientPharmacy/DrugDelivery (superseded by DrugDelivery2018), PharmacyDrugStorage/PSIStatistics, ReceiptQuery, StockCarryOver, StockInventory, StockInventoryIndex, StockInventoryIndexV2, StockQuery (superseded by V2/V3 variants), ReportManage/StandingBook/MainPage (no StandingBook controller).

### 4e. Not pages (HTTP 200 but JSON/text, not shot): DrugStorage/QueryLastCarryTime, initialRKDH, initialCKDH; Home/getmesquery (JSON error: Invalid column name 'kcyjz'); HospitalizationPharmacy/DispensePatientInfoQuery; Medicine/initialSYDH; OutOrInStoredManage/Purchase/PurchaseAdd (view-not-found JSON); OutPatientPharmacy/Loadconfig, PyInfoSearch, QueryCfs, WaitingDispenseMedicineQuery (JSON error, incl. 'Connection refused (localhost:17361)'), initPYY, loadpyhj; Pharmacy/RequisitionGetNewDjh; PharmacyDrugStorage/PharmacyDepartmentList, StartInventory; PurchaseManage/Purchase/Index + QueryLocation, ReportManage/Purchase/Index, ReportManage/Ypfy/Index (view-not-found JSON); DrugStorage/CarryOverMedicine (405); PharmacyDepartment/OpenMedicineIndex + OpenMadicineForm (404, actions commented out in source).

## 5. Needs parameter (page-like actions requiring a record id; from controller signatures, not shot)

- /DrugStorage/MedicineAdjustPriceApproval?ypCodeStr=<mã thuốc>&operationType=<1|2> — price-approval page for a specific drug
- /DrugStorage/RequestMedicineInfo?sldId=<số đơn申领> — requisition detail
- /OutPatientPharmacy/SearchFyDetail?cfh=<số đơn thuốc> — prescription detail
- /OutPatientPharmacy/QueryDrugArrangementRpDetail?cfh=<số đơn>&cardNo=<số thẻ> — arrangement detail
- /PharmacyDrugStorage/EndInventory?pdId=<id phiếu kiểm kê>&autoCarryDown=<0|1> — end-inventory action page
- /HospitalizationPharmacy/KcApplyNoReturnDrugs?Sqd=<số đơn> — inpatient return-confirm page
- /Login/UALogin?access_token=<token>&returnUrl=<url> — SSO handoff, not a browsable page
- Note: blank/new Forms load WITHOUT parameters and ARE shot: /PharmacyDepartment/Form, /BillManage/OutOrInStorageBill/Form, /OutOrInStoredManage/Purchase/Form, /Dictionaries/FypzForm, /Dictionaries/KsFypzForm, /Dictionaries/syyyForm, /PurchaseManage/Purchase/LocationForm, /StorageManage/DrugStockSearch/ExpiredDateForm, /Home/SetUserPharmacyForm.

## 6. Excluded demo page (listed once, NOT shot, per brief)
- Views/testPrint/Index.cshtml (/testPrint/Index, HTTP 200) — demo print template with fake patient data.

## 7. Covered stubs / notices (screenshot of the notice counts as COVERED)
- /Report/PrintReport → Vietnamese SSRS notice: 'Báo cáo không khả dụng — Máy chủ báo cáo SSRS chưa được cấu hình trong môi trường này (ReportServer.HOST trống), nên báo cáo không thể hiển thị.' (viewed, renders).
- /Home/About shot (tab is commented out in Home/Index, but the action exists and renders).

## 8. Observations for the coordinator (not failures of this task)
- The PDS sidebar serves Sett/other-app menus to HospAdministrator because authorizeMenu comes unfiltered from BaseAPI GetMenuList. Every sidebar link 404s on the PDS host. This is a cross-app menu-scoping issue, not a missing PDS screen — but it means a user clicking any sidebar item on PDS gets an empty 404.
- Two Form pages return HTTP 200 live although NO Form action exists in the current controller source (PharmacyDepartment/Form, BillManage/OutOrInStorageBill/Form — both render their real Form views). The running DLL appears to predate the current source for these two actions. Both are shot; no action taken (never rebuild over a running app).
- In-page <a> link collection on menu pages returned 0 candidates (PDS grids open forms via JS buttons, not links); Form companions were therefore enumerated systematically instead: all 390 controller actions + all 164 view files swept by HTTP, and every HTML-200 page not already shot was screenshotted.
- Enum/DB Chinese values: not specifically observed in the viewed shots; any Chinese dropdown values in the remaining shots are server-supplied enum data per the brief, not shot failures.


## 9. Fix đợt 2026-10-27 (worker PDS, chỉ sửa trong Newtouch.HIS.PDS, không commit, không ghi DB)

### 9a. LÀM TRƯỚC — 2 Form: KẾT LUẬN SAI GIẢ THUYẾT "DLL CŨ HƠN SOURCE", GIỮ NGUYÊN, KHÔNG SỬA CODE
- Trước rebuild: /PharmacyDepartment/Index 200 (8037B), /PharmacyDepartment/Form 200 (11005B), /BillManage/OutOrInStorageBill/Form 200 (34231B), login 000000 thành công.
- Rebuild Release 0 error; lần build đầu là incremental no-op (DLL giữ nguyên mtime 2026-10-26 16:28), lần 2 sau khi touch 2 controller thì DLL biên dịch lại thật (mtime 2026-10-27 04:17 UTC) nhưng lần copy pdb bị lỗi do app đang chạy khóa file — đã kill PID 20959, build lại sạch 0 error, khởi động lại bằng run-pds.sh (PID mới 21507, ASPNETCORE_URLS=http://127.0.0.1:5237, DOTNET_SYSTEM_NET_DISABLEIPV6=true, proxy unset), login page 200, login 000000 thành công.
- Sau rebuild thật + process mới: 3 URL trên VẪN 200 với byte-size giống hệt trước rebuild. Vậy KHÔNG phải DLL cũ: source hiện tại vốn đã phục vụ được chúng.
- Cơ chế (xác minh bằng probe, không sửa): base class nhị phân OrgControllerBase (NewtouchHIS.Framework.Web.dll) cung cấp action Index()/Form() chung trả về View() theo tên action — view tồn tại thì render HTML 200, view không tồn tại thì ExceptionHandleMiddleware trả JSON "view not found" với HTTP 200 (probe: /Medicine/Index, /Medicine/Form, /DrugStorage/Index, /BillManage/OutOrInStorageBill/Index đều 200 JSON view-not-found). Ngược lại, tên action khác Index/Form mà không có action riêng thì 404 thật (probe: /Medicine/SysMedicineAdd, /PharmacyDepartment/OpenMedicineIndex, /Medicine/NonExistentXYZ đều 404 0B).
- Kết quả: GIỮ NGUYÊN, không thêm action Index/Form vào PharmacyDepartmentController.cs hay OutOrInStorageBillController.cs (thêm vào sẽ là code thừa, và theo đúng chỉ thị "vẫn 200 thì giữ nguyên"). Ảnh chụp lại sau rebuild (viewport, Playwright+Firefox, login form 000000): pds-pharmacydepartment-form-fixed.png (đã tự xem: form tiếng Việt thật "Thông tin cơ bản", không trống, không lỗi), pds-billmanage-outorinstoragebill-form-fixed.png (200, title "Tra cứu chứng từ-Sửa chứng từ"), pds-pharmacydepartment-index-fixed.png (200, title "Index"). Ảnh cũ KHÔNG bị ghi đè.

### 9b. 3 KHOẢNG TRỐNG: KHÔNG PORT ĐƯỢC — GỐC CŨNG KHÔNG CÓ, BÁO RÕ THAY VÌ TỰ TẠO (theo đúng chỉ thị)
- Đối chiếu cây gốc CHỈ ĐỌC ~/workspace/lmsoft_openhis (toàn cây, không chỉ PDS): grep "PharmaceuticalStocks", "DecoctingfeeQuery", "DoctorsOrderQuery" cho 0 kết quả ở mọi file (controller, view, JS, service).
- Cụ thể cây gốc: MedicineController.cs gốc không có action PharmaceuticalStocks, Views/Medicine/ gốc không có file PharmaceuticalStocks.cshtml; YpckQueryController.cs gốc chỉ có YpckQuery, AntibioticQuery, OutpatientPrescription, HospitalPrescription, DrugSubtotal, AntibioticCFQuery, CFQuantity, YPDrugUsage, YPInOutBound, YPDrugComparisonTable, YprkQuery, YpckQueryNew (+ 2 JSON helper), Views/YpckQuery/ gốc không có DecoctingfeeQuery.cshtml hay DoctorsOrderQuery.cshtml. Cây port cũng không có chúng ở bất cứ đâu.
- Kết luận: 3 URL này chỉ tồn tại dưới dạng dòng menu Sys_Module trong demo DB NewtouchHIS_PDS, không có implementation nào trong hệ thống gốc để port sang — kể cả bản gốc Framework cũng 404 ở 3 URL này (dead menu row, không phải khoảng trống do port làm mất). Vì chỉ thị cấm bịa route/dữ liệu và yêu cầu "gốc không có thì báo rõ thay vì tự tạo", worker KHÔNG tạo stub, KHÔNG tạo action/view cho 3 URL này; chúng vẫn 404 0B sau rebuild (curl có cookie 000000, ngày 2026-10-27).
- Quyết định còn lại cho điều phối viên/người dùng: hoặc xóa/sửa 3 dòng menu Sys_Module trong DB (ghi DB — ngoài phạm vi worker này), hoặc chấp nhận 404 faithful-to-original ở 3 URL này, hoặc ra lệnh riêng tạo trang mới thật (kèm nguồn dữ liệu thật). Không có ảnh -fixed cho 3 URL này vì 404 body rỗng làm Playwright goto lỗi NS_ERROR_NET_EMPTY_RESPONSE (đúng như §4 đã ghi); bằng chứng là status curl.
- Phạm vi tuân thủ: chỉ đọc lmsoft_openhis, chỉ động vào PDS (thực tế không đổi file source PDS nào — git status không có file PDS nào bị sửa bởi worker này), không commit/push, không INSERT/UPDATE/DELETE DB, không động app khác; PDS API 5337 không động tới. File roles-shots-progress.md ở root không sửa.

## 10. Soát tiếng Trung đợt 2026-10-27 (worker PDS, chỉ sửa text trong Newtouch.HIS.PDS, KHÔNG build, KHÔNG khởi động app, KHÔNG commit, KHÔNG ghi DB)
- Phạm vi: 882 file in-scope (.cshtml/.cs/.js không vendor; 13 file JS vendor loại khỏi diện sửa). Tất cả UTF-8, LF, không BOM — không có file nào không dám sửa vì encoding.
- Kết quả: 642 file đổi, 8.311 dòng đổi, 10.945 run đã thay — trong đó chuỗi hiển thị 848 run, comment 10.097 run (đếm theo dòng: dòng comment thuần + comment cuối dòng `--`/`//` tính vào comment).
- CJK trước → sau: toàn phạm vi 8.636 dòng / 11.441 run → 331 dòng / 496 run; theo subproject — API: 0 → 0; Infrastructure: 5.831 dòng / 7.880 run → 260 dòng / 416 run; Web: 2.805 dòng / 3.561 run → 71 dòng / 80 run.
- Tự kiểm chứng: số dòng mỗi file không đổi (so với snapshot /tmp/pds-before/); mọi dòng thay đổi đều nằm trong tập dòng CJK ban đầu (0 dòng ngoài tập); EnumDB.cs còn 0 dòng CJK; SQL CASE literal, khóa djmc, so sánh JS, Contains("成功") đều giữ nguyên; cặp "Trả thuốc thành công" đã dịch đồng bộ cả service (DispenseIndexInfoDmnService.cs:667) và JS (RepercussionIndex.cshtml:332).
- Keeps chính (lý do đầy đủ trong báo cáo cho điều phối viên): khóa tra cứu số chứng từ djmc truyền vào proc sp_ksby_Getypdjh (药品入库单, 外部出库单, 申领发药单, 报损报溢单, 内部申领单, 内部申领出库, 科室发药单); literal trong câu SQL/CASE (kể cả tên cột BHYT tiếng Trung và đơn vị 元/); literal JS/C# bị so sánh với giá trị DB (shzt, ddzt, psdzt, syzt, yfmc, bzmc, 全部...); tên báo cáo SSRS, font 微软雅黑, tên ảnh 消息提醒.png, tên vendor/footer, tên tác giả 周滋明, tên thuốc/khoa mẫu hardcode, payload giao thức SunPurchase; mô tả enum tiền tố chứng từ: KHÔNG có Description nào trong PDS được dùng làm tiền tố số chứng từ (số chứng từ lấy từ DB/proc) nên toàn bộ Description còn tiếng Trung trong EnumDB.cs đã dịch, kể cả 药品入库/科室发药/基药出库/批量出库/退货出库/赠送出库/救灾出库.
- Lỗi đã phát hiện và sửa trong đợt: (1) false-positive SQL do thuộc tính XML name="group"; (2) mask JS quá rộng giữ nhầm chuỗi hiển thị; (3) NGHIÊM TRỌNG: marker [停] bị dịch thành [dừng] ở DispenseIndex.cshtml:305 làm lệch cặp indexOf/replace — đã khôi phục nguyên dòng từ backup và whole-keep; (4) dòng comment SQL và #region trong file TSQL bị bỏ sót ở lần chạy 1 — đã sửa luật comment-first và dịch xong; (5) whole-keep testPrint/Index.cshtml:165-168 giữ nhầm cả nhãn 配药员/发药员/审核员/医师 — đã bỏ whole-keep, nhãn dịch xong, tên mẫu 张爱玲/李丽华/吴昊/隋季 giữ nguyên (không có trong từ điển).
- Còn lại = CJK từ DB/SQL/logic (cần quyết định riêng, KHÔNG tự che bằng code): giá trị trạng thái do SQL CASE sinh ra, tên khoa/thuốc/cách dùng/nhóm bệnh từ DB bị JS so sánh trực tiếp, tên module hệ thống trong DB, thông báo từ stored procedure, tên bệnh viện mẫu 上海明德五官科医院 trong dropdown mua hàng. Chi tiết: báo cáo worker cho điều phối viên.

## 11. Parity audit đợt 2026-10-27 (worker PDS, chỉ sửa trong Newtouch.HIS.PDS, không commit/push, không tạo dòng nghiệp vụ mới)

### 11a. Đếm thật từ source (gốc = `git show V2.2:` trong lmsoft_openhis, KHÔNG dùng working-copy dich-tieng-viet)
- MÀN HÌNH/view: gốc Web 198 .cshtml, port 182. 16 view thiếu toàn là Areas/SystemManage (SysConfig 5, SysLog 1, SysModule 2, SysRole 2, SysStaff/Selector 1, SysUser 4 + _ViewStart): gốc cũng KHÔNG có controller SystemManage nào trong PDS (route gốc trỏ sang controller nhị phân FrameworkBase.MultiOrg) — SSO phục vụ các trang Sys* này, đúng mẫu MRMS. Không port thêm, không bịa controller.
- Controller Web: gốc 46 file, port 46 file (+1 ControllerBase.cs mới). Action: 463/463 tên trùng, chữ ký tham số 0 khác biệt (chỉ khác kiểu trả ActionResult→IActionResult và [FromBody] — tương đương Core).
- API: 12/12 controller, 32/32 action, route trùng (chỉ khác tên phương thức Query/query do route template quyết định, không ảnh hưởng). Envelope live: code 10000/40004, jsonpCallback theo base đã port; GetToken giữ stub 40004 GETTOKEN_DISABLED (đã kiểm chứng live, xem 11c).
- FIELD/DTO: so thuộc tính theo từng file (không gộp theo basename — 3 "lệch" ban đầu đều là artifact gộp 2 file trùng tên: CfmxVO, PSIStatisticsVO x2, đều trùng khớp khi so theo đường dẫn). Chỉ 1 cộng thêm có chủ đích: SysModuleEntity.OrganizeId (đa tổ chức, theo mẫu Lib). Cấu trúc view: 182/182 view ghép cặp, chỉ 2 "lệch" đều là chuỗi hiển thị tên tab đã dịch (không phải cột/lưới/field).
- 3 NotImplemented trong port đều có sẵn ở gốc V2.2 (ResourcesOperateApp.cs:408, LogProxy.cs:77, tyCFMainInfo.cs comment) — trung thực, không phải stub mới. Stub Windows-only duy nhất: /Report/PrintReport (thông báo SSRS tiếng Việt).

### 11b. FIX DUY NHẤT — key số chứng từ djmc bị dịch nhầm trong đợt soát tiếng Trung (hồi quy thật, đã sửa)
- Vấn đề: 5 chỗ frontend gửi `djmc: 'Đơn phát thuốc nội bộ'` tới /DrugStorage/InitDjh → proc sp_yp_Getypdjh; gốc gửi `'内部发药单'`. Bằng chứng live sau khi đăng nhập 000000: key gốc → success "NBFYD202610270038"; key tiếng Việt (giá trị cũ) → error "String or binary data would be truncated". Tức là trước fix, 5 luồng phát thuốc nội bộ không lấy được số chứng từ.
- Sửa (khôi phục literal gốc + comment cảnh báo KHÔNG dịch, 5 file): Views/DrugStorage/DirectDeliveryBatch.cshtml:78, Views/DrugStorage/HandOutMedicine.cshtml:371, Views/Medicine/RequestOfReturn.cshtml:334, Areas/OutOrInStoredManage/Views/DeliveryDirect/Index.cshtml:505, Areas/OutOrInStoredManage/Views/ReturnInward/Index.cshtml:501.
- Làm rõ kết luận enum đợt trước: đúng là KHÔNG có Description enum nào được truyền vào proc (các key proc là literal hardcode: 药品入库单/外部出库单/申领发药单/报损报溢单/内部申领单/内部申领出库 + 内部发药单 từ frontend + 报损报溢单 trong TSqlStock.cs:554, tất cả giữ nguyên như gốc). EnumDanJuLX.GetDescription() chỉ dùng để hiển thị trong thông báo lỗi duyệt chứng từ (OutOrInStorageBillApprovalProcess.cs:54-69), dịch nó không hỏng đánh số — nhưng literal djmc ở frontend thì CÓ hỏng, và đợt soát đã dịch nhầm đúng chỗ đó. Các literal so sánh trạng thái trong C# (否/是/药品类/成功...) vẫn giữ nguyên tiếng Trung — đúng, vì so với giá trị DB.
- Các formatter hiển thị theo mã số (Bill.cshtml/Delivery.cshtml: cellvalue số → chuỗi) được dịch là đúng (chỉ hiển thị, không gửi lên server).

### 11c. Kiểm chứng lại 3 mục cũ (bằng chứng mới, không dùng kết luận cũ)
- (1) 3 URL chết: `git grep` toàn cây V2.2 (không chỉ working-copy) cho PharmaceuticalStocks/DecoctingfeeQuery/DoctorsOrderQuery = 0 kết quả; YpckQueryController gốc chỉ có 13 action trang (YpckQuery, AntibioticQuery, OutpatientPrescription, HospitalPrescription, DrugSubtotal, AntibioticCFQuery, CFQuantity, YPDrugUsage, YPInOutBound, YPDrugComparisonTable, YprkQuery, YpckQueryNew + helper), MedicineController gốc không có PharmaceuticalStocks. Sau rebuild, 3 URL vẫn 404 — hành vi trung thực, KHÔNG tạo stub/action.
- (2) 2 Form: sau rebuild Release thật 0 error + process mới (Web PID 22814, run-pds.sh, ASPNETCORE_URLS=http://127.0.0.1:5237), login 000000 thành công, /PharmacyDepartment/Form 200 (11397B) và /BillManage/OutOrInStorageBill/Form 200 (62226B) — vẫn 200 dù source không có action Form riêng. Cơ chế: ControllerBase của port kế thừa OrgControllerBase nhị phân (NewtouchHIS.Framework.Web.dll) cấp Index()/Form() chung. Giữ nguyên, không thêm action thừa.
- (3) Số chứng từ: xem 11b — kết luận cũ "không có enum Description làm tiền tố" đúng về enum, nhưng CHƯA ĐỦ: key proc thực sự là literal djmc (hardcode C# + frontend), và 5 literal frontend đã bị dịch nhầm — nay đã khôi phục. KHÔNG dịch/sửa bất kỳ key proc nào nữa; các key này phải giữ tiếng Trung cho tới khi proc DB được đổi (quyết định riêng, ngoài phạm vi).

### 11d. Live sau fix (Web PID 22814 mới, API PID 3814 giữ nguyên không động)
- Login 000000 → Home/Index "Hệ thống nhà thuốc - kho thuốc" 200. 7 ảnh -fixed (Playwright+Firefox, 1366x900, không ghi đè ảnh cũ; 2 Form dùng hậu tố -fixed2 vì -fixed đã tồn tại): pds-drugstorage-directdeliverybatch-fixed.png, pds-drugstorage-handoutmedicine-fixed.png, pds-medicine-requestofreturn-fixed.png, pds-outorinstoredmanage-deliverydirect-index-fixed.png (ĐÃ TỰ XEM: giao diện tiếng Việt, số chứng từ NBFYD... hiển thị), pds-outorinstoredmanage-returninward-index-fixed.png, pds-pharmacydepartment-form-fixed2.png, pds-billmanage-outorinstoragebill-form-fixed2.png. HTML phục vụ của cả 5 trang djmc đều chứa key gốc, không còn key tiếng Việt; DLL rebuild chứa key gốc (UTF-16), không chứa key tiếng Việt.
- Flow tới validation gate, không ghi dòng nghiệp vụ: SubmitReceiptApproval với DTO rỗng → error tiếng Việt "Chứng từ xuất nhập kho ID Không được để trống！" (không write). API: GetToken đủ trường → 40004 GETTOKEN_DISABLED; thiếu Timestamp → TIMESTAMP_IS_REQUIRED; Stock/query có Timestamp, không token → 10000 (đúng hành vi gốc: không DefaultAuthorize).
- Ghi chú minh bạch: các lần gọi InitDjh (kể cả page load bình thường của DeliveryDirect) đều làm proc sp_yp_Getypdjh cấp số (NBFYD...036/038) — tức bộ đếm số chứng từ có tăng theo đúng cơ chế gốc; không tạo/sửa/xóa dòng nghiệp vụ nào. Giá trị tiếng Trung còn thấy trên UI đều từ DB (vd. tên nhà thuốc 西药库/西药房/中药房 trong PharmacyDepartmentList, dropdown 正常出库) — ngoài phạm vi sửa code, cần quyết định DB riêng.
- Còn thiếu/không fix: 16 view SystemManage (SSO phục vụ, xem 11a); 3 URL menu DB chết (404 trung thực, xem 11c); GetToken vẫn stub theo quyết định bảo mật (không tự bật); SSRS/DCSoft/ActiveX/WCF/Office Interop giữ stub/thông báo như trước.
