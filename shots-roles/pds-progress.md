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

