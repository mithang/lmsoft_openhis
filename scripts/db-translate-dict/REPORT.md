# BAO CAO — Pass tu dien cho gia tri Scope A con lai (giai doan 1A)

Ngay: 2026-10-27. Nhanh V2.2, tip luc bat dau 217be33c; trong luc lam, HEAD da
chuyen sang 92c5b538 do commit cua worker 1B (khong anh huong pass nay — pass nay
chi them thu muc moi scripts/db-translate-dict/ va ghi DB live). KHONG commit,
KHONG push (theo lenh — dieu phoi vien commit cuoi giai doan sau khi soat).
Worker nay la luong ghi DB DUY NHAT cua giai doan; worker 1B chi sua code.

## 1. Tong quan

- Danh sach goc: artifact Scope A (`db-translate2/final-db-report.json`, 112 target).
  5 target da ve NULL o completion Scope A (Base xt_yfbm.yfbmmc, Sett xt_brxz.brxzmc,
  Sett xt_xjzffs.xjzffsmc, EMR bl_mblb.mbmc, EMR bl_bllx.bllxmc) -> pass nay lam
  **107 target, 2.223 gia tri DISTINCT, 4.586 dong** (khop dung brief).
- Ket qua: **4.564 dong da dich, 22 dong giu lai co ly do** (xem muc 5),
  107/107 script SQL ap dung thanh cong, 0 batch loi.
- Quet CJK sau ap dung: chi con dung 22 dong ngoai le (muc 7). Khong ALTER schema,
  khong dong cot Code/Id/URL, khong xoa dong.
- Mr_Apply (Newtouch_MRQC): 3 dong, TAT CA deu la PILOT-DEMO, noi dung da tieng Viet,
  0 CJK — theo lenh: BO QUA, khong dich, khong xoa.

## 2. So dong da dich theo DB

| DB | So dong | So cau UPDATE | So target |
|----|--------:|--------------:|----------:|
| NewtouchHIS_Base | 975 | 872 | 36 |
| NewtouchHIS_Sett | 514 | 514 | 8 |
| NewtouchHIS_PDS | 75 | 75 | 8 |
| NewtouchHIS_herp | 36 | 36 | 5 |
| Newtouch_CIS | 2619 | 2615 | 16 |
| Newtouch_EMR | 84 | 82 | 12 |
| Newtouch_His_Log | 8 | 8 | 1 |
| Newtouch_MRMS | 164 | 140 | 11 |
| Newtouch_MRQC | 2 | 2 | 1 |
| Newtouch_OR | 85 | 84 | 7 |
| Newtouch_Union | 2 | 2 | 2 |
| TONG | 4564 | 4430 | 107 |

(So cau UPDATE nho hon so dong o target keyless: 1 cau UPDATE doi nhieu dong
cung gia tri cu. So dong = so dong co gia tri thay doi that.)

## 3. So dong theo tung bang/cot

| DB | Bang | Cot | So dong dich | So cau UPDATE | So gia tri giu lai |
|----|------|-----|------------:|--------------:|------------------:|
| NewtouchHIS_Base | Sys_Module | Name | 14 | 14 | 0 |
| NewtouchHIS_Base | Sys_Report | ReportName | 60 | 60 | 0 |
| NewtouchHIS_Base | Sys_Report | ReportDes | 61 | 61 | 0 |
| NewtouchHIS_Base | Sys_Report | SystemName | 60 | 2 | 0 |
| NewtouchHIS_Base | Sys_Report | PinYin | 44 | 44 | 0 |
| NewtouchHIS_Base | Sys_Items | Name | 13 | 13 | 0 |
| NewtouchHIS_Base | Sys_Items | Description | 5 | 5 | 0 |
| NewtouchHIS_Base | Sys_ItemsDetail | Description | 1 | 1 | 0 |
| NewtouchHIS_Base | Sys_Role | Name | 30 | 30 | 0 |
| NewtouchHIS_Base | Sys_Role | Description | 2 | 2 | 0 |
| NewtouchHIS_Base | Sys_Application | Name | 4 | 4 | 0 |
| NewtouchHIS_Base | Sys_App_Modules | ModuName | 1 | 1 | 0 |
| NewtouchHIS_Base | Sys_App_Modules | ModuDesc | 2 | 2 | 0 |
| NewtouchHIS_Base | xt_ypfl | ypflmc | 3 | 3 | 0 |
| NewtouchHIS_Base | xt_ypdw | ypdwmc | 41 | 41 | 0 |
| NewtouchHIS_Base | xt_ypjx | jxmc | 1 | 1 | 0 |
| NewtouchHIS_Base | xt_kssType | qxjbmc | 50 | 50 | 0 |
| NewtouchHIS_Base | xt_kssType | typeName | 25 | 25 | 0 |
| NewtouchHIS_Base | xt_bq | bqmc | 3 | 3 | 0 |
| NewtouchHIS_Base | xt_yfck | yfckmc | 4 | 4 | 0 |
| NewtouchHIS_Base | wz_unit | name | 37 | 37 | 0 |
| NewtouchHIS_Base | xt_yp | ypmc | 25 | 25 | 0 |
| NewtouchHIS_Base | xt_ypsx | gjybmc | 1 | 1 | 0 |
| NewtouchHIS_Base | xt_ypsx | ypgg | 4 | 4 | 0 |
| NewtouchHIS_Base | xt_qxkz | qxmc | 30 | 30 | 0 |
| NewtouchHIS_Base | ReportSvsConfig | RptName | 8 | 8 | 0 |
| NewtouchHIS_Base | pacs_jcbw | jcbw | 42 | 33 | 0 |
| NewtouchHIS_Base | pacs_jcbw | jcff | 45 | 15 | 0 |
| NewtouchHIS_Base | kf_sffl | Name | 3 | 3 | 0 |
| NewtouchHIS_Base | t_haocai | hzmc | 14 | 12 | 0 |
| NewtouchHIS_Base | t_haocai | gg | 35 | 31 | 0 |
| NewtouchHIS_Base | axm | ypmc | 4 | 4 | 0 |
| NewtouchHIS_Base | Mz_Mtbbzml | mtbbzdlmc | 2 | 2 | 0 |
| NewtouchHIS_Base | Mz_Mtbbzml | mtbbzflmc | 2 | 2 | 0 |
| NewtouchHIS_Base | Mz_Mtbbzml | mtbbzmc | 2 | 2 | 0 |
| NewtouchHIS_Base | Sh_Ybfyxzbl | xmmc | 297 | 297 | 0 |
| NewtouchHIS_Sett | Sys_Module | Name | 8 | 8 | 0 |
| NewtouchHIS_Sett | Sys_Module | Description | 16 | 16 | 0 |
| NewtouchHIS_Sett | Sys_Role | Description | 1 | 1 | 0 |
| NewtouchHIS_Sett | Sys_Config | Name | 183 | 183 | 0 |
| NewtouchHIS_Sett | Sys_Config | Memo | 147 | 147 | 1 |
| NewtouchHIS_Sett | Sys_ConfigTemplate | Name | 50 | 50 | 0 |
| NewtouchHIS_Sett | Sys_ConfigTemplate | Memo | 41 | 41 | 1 |
| NewtouchHIS_Sett | Sys_FailedCodeMessageMapp | msg | 68 | 68 | 0 |
| NewtouchHIS_PDS | Sys_Module | Name | 25 | 25 | 0 |
| NewtouchHIS_PDS | Sys_Module | Description | 1 | 1 | 0 |
| NewtouchHIS_PDS | Sys_Role | Name | 5 | 5 | 0 |
| NewtouchHIS_PDS | Sys_Config | Name | 14 | 14 | 0 |
| NewtouchHIS_PDS | Sys_Config | Memo | 15 | 15 | 0 |
| NewtouchHIS_PDS | Sys_ConfigTemplate | Name | 2 | 2 | 0 |
| NewtouchHIS_PDS | Sys_ConfigTemplate | Memo | 1 | 1 | 0 |
| NewtouchHIS_PDS | Sys_FailedCodeMessageMapp | msg | 12 | 12 | 0 |
| NewtouchHIS_herp | Sys_Module | Name | 24 | 24 | 0 |
| NewtouchHIS_herp | Sys_Module | Description | 1 | 1 | 0 |
| NewtouchHIS_herp | Sys_Config | Memo | 1 | 1 | 0 |
| NewtouchHIS_herp | rel_warehouseDept | deptName | 8 | 8 | 0 |
| NewtouchHIS_herp | kf_warehouse | name | 2 | 2 | 0 |
| Newtouch_CIS | Sys_Module | Name | 26 | 26 | 0 |
| Newtouch_CIS | Sys_Module | Description | 2 | 2 | 0 |
| Newtouch_CIS | Sys_Role | Name | 6 | 6 | 0 |
| Newtouch_CIS | Sys_Config | Name | 238 | 238 | 0 |
| Newtouch_CIS | Sys_Config | Memo | 103 | 103 | 0 |
| Newtouch_CIS | Sys_ConfigTemplate | Name | 74 | 74 | 0 |
| Newtouch_CIS | Sys_ConfigTemplate | Memo | 26 | 26 | 0 |
| Newtouch_CIS | jyjc_ztxm | sfxmmc | 792 | 792 | 0 |
| Newtouch_CIS | jyjc_ztxm | dw | 379 | 379 | 0 |
| Newtouch_CIS | jyjc_zt | ztmc | 44 | 44 | 0 |
| Newtouch_CIS | jyjc_mbzt | ztmc | 248 | 248 | 0 |
| Newtouch_CIS | jyjc_mbzt | mbmc | 184 | 184 | 0 |
| Newtouch_CIS | xt_zt | ztmc | 9 | 9 | 0 |
| Newtouch_CIS | pacs_jcbw | jcff | 51 | 47 | 0 |
| Newtouch_CIS | mz_clyzzd | zdmc | 413 | 413 | 0 |
| Newtouch_CIS | mz_cyzd | zdmc | 24 | 24 | 0 |
| Newtouch_EMR | Sys_Module | Name | 6 | 6 | 0 |
| Newtouch_EMR | Sys_Role | Name | 1 | 1 | 0 |
| Newtouch_EMR | Sys_Config | Name | 6 | 6 | 0 |
| Newtouch_EMR | Sys_Config | Memo | 2 | 2 | 0 |
| Newtouch_EMR | Sys_ConfigTemplate | Name | 6 | 6 | 0 |
| Newtouch_EMR | Sys_ConfigTemplate | Memo | 2 | 2 | 0 |
| Newtouch_EMR | bl_mblb | bllxmc | 33 | 33 | 0 |
| Newtouch_EMR | bl_ysjgnr | ysmc | 15 | 14 | 0 |
| Newtouch_EMR | bl_ElementDomain_Detail | Element_Name | 4 | 4 | 0 |
| Newtouch_EMR | bl_tsfh | fhnr | 3 | 3 | 0 |
| Newtouch_EMR | Mr_Writing_Rules | RulesType | 1 | 1 | 0 |
| Newtouch_EMR | Mr_Writing_Rules | Remark | 5 | 4 | 0 |
| Newtouch_MRMS | Sys_Module | Name | 12 | 12 | 0 |
| Newtouch_MRMS | Sys_Config | Name | 2 | 2 | 0 |
| Newtouch_MRMS | Sys_Config | Memo | 1 | 1 | 0 |
| Newtouch_MRMS | Sys_ConfigTemplate | Name | 5 | 4 | 0 |
| Newtouch_MRMS | Sys_ConfigTemplate | Memo | 1 | 1 | 0 |
| Newtouch_MRMS | mr_dic_common | ItemName | 31 | 28 | 7 |
| Newtouch_MRMS | mr_dic_common | RlueName | 24 | 4 | 1 |
| Newtouch_MRMS | mr_dic_dept | ksmc | 34 | 34 | 0 |
| Newtouch_MRMS | mr_dic_bafeetype | Name | 29 | 29 | 0 |
| Newtouch_MRMS | mr_dic_sfdlrelbafee | hissfdlmc | 20 | 20 | 0 |
| Newtouch_MRMS | mr_rel_dept | hisdeptname | 5 | 5 | 0 |
| Newtouch_MRQC | Sys_Module | Name | 2 | 2 | 0 |
| Newtouch_OR | Sys_Module | Name | 5 | 5 | 0 |
| Newtouch_OR | Sys_Config | Name | 1 | 1 | 0 |
| Newtouch_OR | Sys_ConfigTemplate | Name | 5 | 4 | 0 |
| Newtouch_OR | Sys_FailedCodeMessageMapp | msg | 68 | 68 | 0 |
| Newtouch_OR | OR_Anesthesia | AnesName | 4 | 4 | 0 |
| Newtouch_OR | OR_NotchGrade | Name | 1 | 1 | 0 |
| Newtouch_OR | OR_Room | Name | 1 | 1 | 0 |
| Newtouch_His_Log | Sys_Module | Name | 8 | 8 | 0 |
| Newtouch_Union | Sys_Config | Name | 1 | 1 | 0 |
| Newtouch_Union | Sys_ConfigTemplate | Name | 1 | 1 | 0 |

## 4. Phuong phap va quy uoc

- Tu dien dung chung toan cuc: cung mot chuoi goc o nhieu target phai dich giong nhau;
  script join da kiem tra xung dot old -> new (chi con 2 truong hop co ly do, muc 5).
- **ASCII khong dau cho TAT CA cac cot, ke ca nvarchar**, theo tien le Scope A
  ("cot nvarchar cung dung ASCII de nhat quan"). Ky tu Hy Lap α/β/γ duoc giu nguyen
  o cho gia tri goc da co (da chung minh round-trip duoc trong cung cot GBK;
  kiem chung muc 7).
- Tien to thong nhat cho xt_qxkz: `Kiem soat ke don` + ten hang muc (thay vi
  `Kiem soat quyen ke don`), vi ban day du dai nhat toi 91 ky tu vuot varchar(50).
- Giu nguyen token: ma (YY101, SFXM_GHCKF, GHLX_ZJGH, zls_pbts, 023RUNCLUB...),
  the `<br/>`, hau to `?` (chan doan nghi ngo, dinh truc tiep), so/don vi (ml, U, mg),
  nhom mau ABO, Rh/RhD, T-SPOT, CEA, ten thuong hieu phien am, tien to `Cq`/`Chongqing`
  trong RptName, duong dan URL trong mo ta module.
- Script theo format Scope A: header comment, `USE [DB];`, keyed UPDATE theo khoa chinh,
  keyless `WHERE col = N'old'` (double dau nhay). Gia tri moi chua α/β/γ dung literal
  N'...' (le duy nhat so voi Scope A, de dam bao chuyen doi Unicode -> GBK dung);
  gia tri moi ASCII thuan dung '...' nhu Scope A. Chay bang sqlcmd `-b -f 65001`.
- Backup truoc UPDATE: 1 file XML moi target (keyed: cot PK + gia tri cu;
  keyless: gia tri + so dong) + manifest (xem muc 6).

## 5. Chuoi rut gon

**69 muc** (bao gom 2 muc sua thu cong cho gia tri goc co khoang trang cuoi).
Chi tiet day du (cu -> truoc rut gon -> sau rut gon -> gioi han cot):
`scripts/db-translate-dict/SHORTENED.md`.
Nhom chinh: mau/che pham mau mbzt (viet tat TC/HC/HT/BC, Rh+ thay cho Rh duong),
MR/CT (bo tu `chup`, dung MRCP/MRU/MRM), hang muc phi (bo tu mo rong, dung BN/TK/HP),
ten module Sett/MRMS (bo `chi phi`, dung BN/TK), ten khop thai duong - ham duoi
(TD-HD), Element_Name EMR (varchar 20), RulesType EMR (varchar 20, xem muc 6).
Sau rut gon: 0 ban dich vuot gioi han (script join kiem tra lai toan bo).

## 6. Ngoai le KHONG dich (co bang chung) va truong hop dac biet

- **E1 — MRMS mr_dic_common.RlueName = '关系' (9 dong):** literal join trong 3 proc live
  (RPT_SETT_住院医保结算基金清单_结算信息, usp_Inp_ybupload_cyjs_setlinfo,
  usp_Inp_ybupload_cyjs_setlinfoV2) + 1 ban bak: `g.RlueName='关系' AND g.itemName=b.GX`.
- **E2 — MRMS mr_dic_common.ItemName, nhom 关系, 7 gia tri / 7 dong**
  (本人或户主, 父母, 配偶, 孙子、孙女或外孙子、外孙女, 兄、弟、姐、妹, 子,
  祖父母或外祖父母): khoa join toi du lieu GX cua benh nhan + bang mapping BHYT
  `#yb_gx` (ba_name hardcode tieng Trung trong proc). Cac join mr_dic_common khac
  dung ItemCode/RlueCode (ma) nen cac nhom con lai dich duoc.
- **E3 — Memo chua credential (KHONG BAO GIO tai hien gia tri trong file/bao cao):**
  Sett Sys_Config.Memo (5 dong) va Sys_ConfigTemplate.Memo (1 dong) chung mot memo JSON
  chua tai khoan ben thu ba cua cong bao hiem. De nguyen khong dich; file map chi ghi
  marker KEEP-SECRET (gia tri cu da redact khoi moi output).
- **Da nghia — '副':** cung chuoi cu, 2 nghia theo nganh: don vi lieu thuoc
  (xt_ypdw) = `Thang` (thang thuoc); don vi vat tu chung (wz_unit) = `Bo`.
  Giu ca hai, ghi nhan la ngoai le da nghia (khong ep mot nghia sai len mot bang).
- **Bien the gioi han — '病员告知委托书':** EMR Mr_Writing_Rules.RulesType varchar(20)
  rut gon thanh `Giay uy thac BN`; EMR bl_mblb.bllxmc (cot rong hon) giu ban day du
  `Giay uy thac thong bao benh nhan`. Cung chuoi cu, 2 ban dich vi gioi han cot khac nhau.
- **Bien the collation — 1 dong Sett Sys_Config.Memo:** gia tri goc dung dau phay
  full-width (，) trong khi nhom GROUP BY hien thi ban half-width (,); collation gop
  2 bien the thanh 1 nhom. Dong nay da duoc ap ban dich cua nhom (dau phay ASCII)
  theo PK — khong con dong CJK thua.
- **Coupling da kiem tra, KHONG phai ngoai le (dich an toan):** mr_dic_sfdlrelbafee /
  mr_dic_bafeetype join bang MA; xt_kssType.qxjbmc chi xuat hien trong COMMENT proc
  (so sanh dung ma qxjb); bl_ysjgnr.ysmc / bl_ElementDomain_Detail.Element_Name hit
  trong proc chi la alias cot output; Sys_Config Name grep code = 0 literal (Code moi la
  khoa); jyjc_ztxm.dw khong co literal ma don vi; wz_unit join bang Id; Sys_Report /
  ReportSvsConfig: URL SSRS trong view hardcode duong dan .rdl tieng Trung, dich ten bao
  cao trong DB khong doi URL do (thay SSRS la viec rieng cua chuong trinh);
  mz_clyzzd / mz_cyzd dich theo tien le Scope A (join bang ma ksCode+zdCode; hau to ?
  giu nguyen); proc migration khong hoat dong `add_sfxm_by_zsc` khop ten jyjc voi bang
  staging RONG, org khac org demo, khong co caller trong code -> dich an toan.
- **Du lieu goc dang ngo / loi (dich theo nghia dinh san, co gan co):** xt_ypfl
  '抗徽生物药物' (sai chu 徽 thay cho 微); xt_kssType.typeName ky tu rac '饿'/'我';
  don vi wz_unit '扰'/'郑'/'空'/'梱'/'掀'/'揿'; t_haocai.gg '像双'; pacs_jcbw.jcff '斜位位'
  (lap chu); mr_dic_bafeetype '非手术治疗项目非' (sai chu cuoi, ban sao loi cua
  '非手术治疗项目费' — dich trung ten dong anh em, an toan vi keyed theo Id);
  Sys_ConfigTemplate MRMS/OR '测试分额鹅鹅鹅我' (dong config test rac);
  Sh_Ybfyxzbl '盐酸氮？斯汀' (ky tu ? la mojibake); mbzt '胸椎MR平扫+弥成像散' /
  '脑MR平扫+波潽成像' (sai chu 弥/潽); CIS Config Memo 'ture' (sai chinh ta goc) giu nguyen;
  Sett Sys_Config Name '出院结算时是否在Optima验证状态/td>' giu nguyen manh `/td>`
  (loi du lieu, khong tu sua ngam); 2 gia tri goc co khoang trang cuoi (mbzt) da xu ly
  rieng khi rut gon.

## 7. Kiem chung sau ap dung

- Quet CJK lai tren 107 target: **0 mismatch** so voi ky vong; tong so dong CJK con lai
  = **22**, dung bang cac ngoai le: Sett Sys_Config.Memo 5, Sett Sys_ConfigTemplate.Memo 1,
  MRMS ItemName 7, MRMS RlueName 9. Moi target khac = 0.
- Ky tu Hy Lap round-trip OK: `Dinh luong β-HCG huyet thanh` (jyjc_ztxm.sfxmmc),
  `HC treo γ chieu xa loai BC 1 don vi (O Rh+)` va `TC don thu γ chieu xa loai BC 1 lieu (AB Rh+)`
  (jyjc_mbzt.ztmc) deu co trong DB sau ap dung.
- Ten moi hien trong DB: MRQC Sys_Module `Trung tam kiem soat chat luong`,
  `Tra cuu thong bao kiem soat chat luong`; MRMS mr_dic_dept `Khoa Tai Mui Hong`.
- Cong 200: SSO 5208, MRQC 5220, Sett 5221, MRQC 5232, OR 5231, Base/HOSP 5233,
  CIS 5234, EMR 5235, Herp 5236, PDS 5237, HOSP 5238 (BaseAPI 7001 tra 404 o `/` nhu
  binh thuong — API khong co route root). Khong restart app (gia tri DB doc live).
- **Login 000000 — CAN BO SUNG:** trong shell cua phien nay khong co bien SSO_PASSWORD
  (script chup man hinh doc tu env nay) nen KHONG chay duoc login thanh cong that.
  Da probe duong dan auth: POST CheckLogin username 000000 + mat khau sai tra ve
  HTTP 200 + JSON `Mật khẩu không đúng` — chung to user 000000 TON TAI, duong dan
  SSO -> BaseAPI CheckPwd hoat dong binh thuong sau khi doi DB. Dieu phoi vien chay lai
  1 lan login thanh cong bang mat khau demo that truoc khi dong giai doan.

## 8. Vi tri artifact

- Backup: `~/workspace/db-translate-dict-backup/` — 107 file XML + `manifest.json`
  (so dong truoc, so dong CJK truoc, so cau UPDATE, file backup/SQL, gia tri giu lai,
  gia tri khong co dong, dong bien the collation). KHONG ghi de artifact Scope A.
- Script SQL: `~/workspace/openhis-core/scripts/db-translate-dict/`
  (107 file `upd_<DB>.<bang>.<cot>.sql`, theo format Scope A).
- Ban do dich day du (cu -> moi, 2.223 dong; gia tri E3 da redact):
  `scripts/db-translate-dict/map_review.txt`.
- Log rut gon: `scripts/db-translate-dict/SHORTENED.md` (69 muc).
- File lam viec tam (khong commit): `/tmp/dict1a/` (num/, tr/, join.py, shorten.py,
  gen.py, verify.py, join_map.json, verify_after.json).

## 9. Discovery: cac bang co CJK NGOAI 107 target (KHONG dich o pass nay)

Quet toan bo 14 DB (4.579 cot thuoc 406 bang co dong ma Scope A chua tung quet):
**193 bang HIT CJK, 213 bang ok.** Phan loai (chi tiet day du o ban ghi chep lam viec;
dieu phoi vien quyet dinh pass rieng):

- Catalog chuan quoc gia / bao hiem (Scope A co tinh giu): gb_ypxx (217.407 dong),
  gb_sfxm (+bak), gb_clxx (+bak), sys_icd10_dm (35.587), sys_icd9_dm, sys_icdzl_dm,
  xt_zd (37.781, +bak, xt_newzd), xt_xm_zjb, Cqyb_ybxm, t_gjypbm, t_yp_gjbm.
- Backup / snapshot / temp: moi `*_bak*`, `*_bakV2`, `*_0621`, `*_2023`, `*_0618`,
  `rpt_xt_cw_snap` (184.247 dong, chi cot cwmc), `TTCataloguesComparisonDetail_bak*`,
  `temp_*` cua bdg, `xt_ypsx_bakk`, `bl_ysmxbak`, `bl_ys_bak`, `jyjc_*_bak`,
  `Sys_ShortcutMenubak`.
- Du lieu van hanh / benh nhan / nhan vien (khong dich): `zy_*`, `mz_cf*`, `mz_js`,
  `xt_brjbxxLOG`, `zy_brjbxx`, `OR_Operation.ssmc` (17.599 dong), `OR_NewOperation`,
  `brly.F2`, du lieu kham suc khoe bdg (PE*, TJ_JGMX_YQ_PT, Tj_ybxx_pt),
  `UserLogin` bdg, Customer/PECustomerReservation.
- Log / audit: `Sys_Log` moi app, `DDLChangeLog.TSQLCommand`, `InterfaceLog`,
  `ErrorLog`, `sendMsgLog`.
- He kham suc khoe bdg (he rieng, khong co web app nao trong 25 port): Item, ItemGroup,
  ItemPackage, InvQuestion/Answer, News, Holiday, PowerRole/Model.
- Gia tri cau hinh mang tinh chuc nang (KHONG dich): cot Sys_Config Code/Value/DefaultVal
  co CJK (CIS Sys_Config 364 dong, Sett Sys_Config Value 474, Sett/CIS Sys_ConfigTemplate
  DefaultVal) — code so sanh gia tri, dich se pha logic.
- Bang tu dien nho Scope A bo sot, CAN dieu phoi vien quyet dinh co dich o pass rieng
  khong (chua dich): `xt_sfxm.sfxmmc/dw` (5.854 dong, catalog hang muc thu phi live),
  EMR `bl_ys` (2.217 dong) + `bl_ysmx` (5.823 dong, thu vien element benh an),
  `Sys_Organize` Name/Address (2 dong — ten/dia chi benh vien, doi ten la doi danh tinh
  benh vien), `Sys_ReportTemplate`, `xt_yp.spm/ycmc/bz`, `axm.sccj`, `xt_yptemp`,
  `t_yp_his`, `yb_upload_dic_sfdl.ybmc`, `yb_zdinfo.label` (690 dong),
  `TTCataloguesComparisonMain/Detail` (du lieu mapping — dich se pha mapping).
- Ngoai le dang chu y: `yb_cx_1318` (349.275 dong) khong co CJK o cot chua tung quet;
  `yb_cx_1319` chi hit `crter_name/opter_name` (ten nguoi tao/van hanh).

## 10. Van de ton tai tu truoc CAN BAO (khong thuoc pass DB nay)

- **Scope A da lam vo mot phan join 关系 tu truoc:** 2 dong nhom 关系 da bi dich
  keyless thanh `Khac` (其他), `Nu` (女) trong khi proc van join bang tieng Trung —
  pass nay GIU NGUYEN 7 dong con lai (E2), khong tu revert 2 dong kia. Can quyet dinh
  sua dong bo (proc + DB) o giai doan build.
- `Newtouch.Sett.Web/Areas/OutpatientManage/Controllers/OutpatChargeController.cs:830-831`
  chua ban dich tung phan trong chuoi so sanh gateway GuiAn ("Ngoại trú补偿序号...") —
  chuoi so sanh khong duoc dich, can hoan nguyen ve tieng Trung goc neu dung la khoa
  so sanh (viec cua code, worker 1B / dieu phoi vien).
- Literal CASE trong proc bao cao so sanh cot `dl` phia Sett (`zy_xmjfb.dl`) voi cac
  gia tri tieng Trung (中医骨伤, 灸法, 医疗服务, 医学影像...) — pass nay khong dung Sett
  `dl`, nhung giai doan sau dich `dl` phai dich dong bo proc.
- URL SSRS trong view hardcode duong dan .rdl tieng Trung (xem muc 6).
- Proc migration `add_sfxm_by_zsc` (DB Base) khop ten jyjc voi bang staging rong —
  khong anh huong pass nay, nhung khong nen xoa proc khi chua xac nhan het caller.
- Tien to chung tu: pass nay khong doi enum tien to; su khong nhat quan cu
  (ReceiptNoHelper/hardcode giu tien to cu, GetSpellCode moi dung tien to moi) van ton tai.

## 11. Bo sung cua dieu phoi vien — fixup 2 dong nhom 关系 (sau bao cao 1A)

- Van de muc 10 (Scope A dich vo join 关系) DA DUOC SUA, khong doi proc:
  revert 2 dong trong pham vi `RlueName = '关系'` ve tieng Trung goc
  (ItemCode 3: `Nu` -> `女`; ItemCode 8: `Khac` -> `其他`), vi proc live
  `usp_Inp_ybupload_cyjs_setlinfo` join dung
  `g.RlueName='关系' AND g.itemName=b.GX` (da kiem chung truc tiep tu
  sys.sql_modules). Cac nhom khac giu ban dich (join bang ma).
- Script: `fixup_Newtouch_MRMS.mr_dic_common.guanxi.sql` (ap dung bang
  sqlcmd -b -f 65001, 1 + 1 dong anh huong, exit 0). Backup truoc khi sua:
  `~/workspace/db-translate-dict-backup/fixup_mr_dic_common_guanxi.before.txt`.
- Kiem chung sau fixup: nhom 关系 du 9 dong tieng Trung, 0 dong `Khac`/`Nu` con lai.
- Diem muc 10 ve chuoi so sanh gateway GuiAn (OutpatChargeController.cs:830-831)
  DA XONG tu giai doan 1B (commit 92c5b538), khong thuoc pass nay.
