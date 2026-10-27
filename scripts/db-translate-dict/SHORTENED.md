# Chuoi rut gon (pass tu dien, 2026-10-27)

Quy tac: ban dich vuot do dai byte cot GBK thi rut gon o ranh gioi tu, giu nghia,
kho^ng bao gio vuot byte cho phep. Cot varchar: ASCII = 1 byte, ky tu Hy Lap (α/β/γ) = 2 byte.
Chu (*) = ban dich goc von vua gioi han cua cot do, nhung van rut gon de giu nhat quan
old -> new toan cuc voi ban sao cung gia tri o bang khac co cot hep hon.

- NewtouchHIS_Base|xt_qxkz|qxmc | gioi han varchar(50)
  cu: 中医特殊疗法开立权限控制
  truoc rut gon (51 byte): Kiem soat ke don Lieu phap dac biet Y hoc co truyen
  sau rut gon (40 byte): Kiem soat ke don Lieu phap dac biet YHCT
- NewtouchHIS_Base|xt_qxkz|qxmc | gioi han varchar(50)
  cu: 中医肛肠开立权限控制
  truoc rut gon (53 byte): Kiem soat ke don Hau mon - truc trang Y hoc co truyen
  sau rut gon (42 byte): Kiem soat ke don Hau mon - truc trang YHCT
- NewtouchHIS_Sett|Sys_Module|Name | gioi han varchar(50)
  cu: 医保科室结算费用统计(东软)
  truoc rut gon (54 byte): Thong ke chi phi quyet toan khoa theo BHYT (Dong Ruan)
  sau rut gon (46 byte): Thong ke quyet toan khoa theo BHYT (Dong Ruan)
- NewtouchHIS_Sett|Sys_Module|Name | gioi han varchar(50)
  cu: 医保科室结算费用统计病人明细
  truoc rut gon (61 byte): Chi tiet benh nhan thong ke chi phi quyet toan khoa theo BHYT
  sau rut gon (46 byte): Chi tiet BN thong ke quyet toan khoa theo BHYT
- NewtouchHIS_Sett|Sys_Module|Name | gioi han varchar(50)
  cu: 自费科室结算费用统计病人明细
  truoc rut gon (58 byte): Chi tiet benh nhan thong ke chi phi quyet toan khoa tu tra
  sau rut gon (43 byte): Chi tiet BN thong ke quyet toan khoa tu tra
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 彩色多普勒超声常规检查(每增加一个部位加收)
  truoc rut gon (77 byte): Kiem tra sieu am Doppler mau thong thuong (moi tang them mot vi tri thu them)
  sau rut gon (44 byte): Sieu am Doppler mau (them 1 vi tri thu them)
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 新型冠状病毒核酸检测（非医保）
  truoc rut gon (53 byte): Xet nghiem acid nucleic virus Corona moi (khong BHYT)
  sau rut gon (47 byte): Xet nghiem acid nucleic Corona moi (khong BHYT)
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 细菌性阴道病唾液酸酶测定
  truoc rut gon (56 byte): Dinh luong enzyme sialidase benh viem am dao do vi khuan
  sau rut gon (38 byte): Dinh luong sialidase viem am dao do VK
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 常规药敏定量试验(MIC)
  truoc rut gon (58 byte): Thu nghiem dinh luong do nhay cam thuoc thong thuong (MIC)
  sau rut gon (41 byte): Thu nghiem dinh luong khang sinh do (MIC)
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100)
  cu: 幽门螺杆菌分型HP（Ure+VacA+CagA)量子点免疫荧光法（自费）
  truoc rut gon (105 byte): Phan loai Helicobacter pylori HP (Ure+VacA+CagA) phuong phap mien dich huynh quang cham luong tu (tu tra)
  sau rut gon (85 byte): Phan loai HP (Ure+VacA+CagA) phuong phap mien dich huynh quang cham luong tu (tu tra)
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 腹部大血管彩色多普勒超声
  truoc rut gon (42 byte): Sieu am Doppler mau mach mau lon vung bung
  sau rut gon (33 byte): Sieu am Doppler mau mach lon bung
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100) (*)
  cu: 血清人绒毛膜促性腺激素亚单位测定(β-HCG)
  truoc rut gon (72 byte): Dinh luong tieu don vi gonadotropin nhau thai nguoi huyet thanh (β-HCG)
  sau rut gon (29 byte): Dinh luong β-HCG huyet thanh
- Newtouch_CIS|jyjc_ztxm|sfxmmc | gioi han varchar(100)
  cu: 超声计算病理彩色图文报告(含图象分析)加收
  truoc rut gon (102 byte): Bao cao hinh anh - van ban mau sieu am tinh benh ly bang may tinh (bao gom phan tich hinh anh) phu thu
  sau rut gon (84 byte): Bao cao hinh anh - van ban mau sieu am tinh benh ly (gom phan tich hinh anh) phu thu
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: CT骨腔平扫+增强
  truoc rut gon (51 byte): CT khoang xuong chup khong can quang + co can quang
  sau rut gon (46 byte): CT khoang xuong khong can quang + co can quang
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: CT骶尾部平扫+增强
  truoc rut gon (54 byte): CT vung cung - cut chup khong can quang + co can quang
  sau rut gon (49 byte): CT vung cung - cut khong can quang + co can quang
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: CT骶髋关节平扫+增强
  truoc rut gon (56 byte): CT khop cung - chang chup khong can quang + co can quang
  sau rut gon (49 byte): CT khop cung-chang khong can quang + co can quang
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞单采血小板1治疗量（AB型Rh阳性）
  truoc rut gon (86 byte): Tieu cau don thu gom γ - chieu xa loai bo bach cau 1 lieu tri lieu (nhom AB Rh duong)
  sau rut gon (46 byte): TC don thu γ chieu xa loai BC 1 lieu (AB Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞单采血小板1治疗量（A型Rh阳性）
  truoc rut gon (85 byte): Tieu cau don thu gom γ - chieu xa loai bo bach cau 1 lieu tri lieu (nhom A Rh duong)
  sau rut gon (45 byte): TC don thu γ chieu xa loai BC 1 lieu (A Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞单采血小板1治疗量（B型Rh阳性）
  truoc rut gon (85 byte): Tieu cau don thu gom γ - chieu xa loai bo bach cau 1 lieu tri lieu (nhom B Rh duong)
  sau rut gon (45 byte): TC don thu γ chieu xa loai BC 1 lieu (B Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞单采血小板1治疗量（O型Rh阳性）
  truoc rut gon (85 byte): Tieu cau don thu gom γ - chieu xa loai bo bach cau 1 lieu tri lieu (nhom O Rh duong)
  sau rut gon (45 byte): TC don thu γ chieu xa loai BC 1 lieu (O Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞悬浮红细胞1单位（O型Rh阳性）
  truoc rut gon (71 byte): Hong cau treo γ - chieu xa loai bo bach cau 1 don vi (nhom O Rh duong)
  sau rut gon (44 byte): HC treo γ chieu xa loai BC 1 don vi (O Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照去白细胞悬浮红细胞2单位（O型Rh阳性）
  truoc rut gon (71 byte): Hong cau treo γ - chieu xa loai bo bach cau 2 don vi (nhom O Rh duong)
  sau rut gon (44 byte): HC treo γ chieu xa loai BC 2 don vi (O Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照悬浮红细胞1.5单位（O型Rh阳性）
  truoc rut gon (56 byte): Hong cau treo γ - chieu xa 1.5 don vi (nhom O Rh duong)
  sau rut gon (38 byte): HC treo γ chieu xa 1.5 don vi (O Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: γ-辐照洗涤红细胞2单位（O型Rh阳性）
  truoc rut gon (53 byte): Hong cau rua γ - chieu xa 2 don vi (nhom O Rh duong)
  sau rut gon (35 byte): HC rua γ chieu xa 2 don vi (O Rh+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 上腹部MR平扫+胰胆管成像(MRCP)
  truoc rut gon (66 byte): MR vung bung tren chup khong can quang + chup ong mat - tuy (MRCP)
  sau rut gon (37 byte): MR bung tren + MRCP (khong can quang)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 中下腹部MR平扫+泌尿道成像（MRU)
  truoc rut gon (74 byte): MR vung bung giua - duoi chup khong can quang + chup duong tiet nieu (MRU)
  sau rut gon (41 byte): MR bung giua-duoi + MRU (khong can quang)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 去白细胞悬浮红细胞1U（AB型Rh阳）
  truoc rut gon (52 byte): Hong cau treo loai bo bach cau 1U (nhom AB Rh duong)
  sau rut gon (32 byte): HC treo loai BC 1U (AB Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 去白细胞悬浮红细胞1U（A型Rh阳）
  truoc rut gon (51 byte): Hong cau treo loai bo bach cau 1U (nhom A Rh duong)
  sau rut gon (31 byte): HC treo loai BC 1U (A Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 去白细胞悬浮红细胞1U（B型Rh阳）
  truoc rut gon (51 byte): Hong cau treo loai bo bach cau 1U (nhom B Rh duong)
  sau rut gon (31 byte): HC treo loai BC 1U (B Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 去白细胞悬浮红细胞1U（O型Rh阳）
  truoc rut gon (51 byte): Hong cau treo loai bo bach cau 1U (nhom O Rh duong)
  sau rut gon (31 byte): HC treo loai BC 1U (O Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50) (*)
  cu: 尿流动力学检测
  truoc rut gon (35 byte): Do dong luc hoc dong chay nuoc tieu
  sau rut gon (25 byte): Do dong luc hoc nuoc tieu
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 尿液分析(10-11项）+尿沉渣定量
  truoc rut gon (63 byte): Phan tich nuoc tieu (10-11 chi tieu) + dinh luong can nuoc tieu
  sau rut gon (43 byte): Phan tich nuoc tieu (10-11) + can nuoc tieu
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 常规药敏定量试验(MIC)
  truoc rut gon (58 byte): Thu nghiem dinh luong do nhay cam thuoc thong thuong (MIC)
  sau rut gon (41 byte): Thu nghiem dinh luong khang sinh do (MIC)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 彩色多普勒超声常规检查(每增加一个部位加收)
  truoc rut gon (77 byte): Kiem tra sieu am Doppler mau thong thuong (moi tang them mot vi tri thu them)
  sau rut gon (44 byte): Sieu am Doppler mau (them 1 vi tri thu them)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 新型冠状病毒核酸检测（非医保）
  truoc rut gon (53 byte): Xet nghiem acid nucleic virus Corona moi (khong BHYT)
  sau rut gon (47 byte): Xet nghiem acid nucleic Corona moi (khong BHYT)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 无痛电子胃肠镜治疗费
  truoc rut gon (52 byte): Phi dieu tri noi soi da day - ruot dien tu khong dau
  sau rut gon (42 byte): Phi dieu tri noi soi da day-ruot khong dau
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 泌尿系彩色超声检查（不包括肾血管）
  truoc rut gon (54 byte): Sieu am mau he tiet nieu (khong bao gom mach mau than)
  sau rut gon (40 byte): Sieu am mau he tiet nieu (tru mach than)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 洗涤红细胞（去白）1单位（O型Rh阳性）
  truoc rut gon (58 byte): Hong cau rua (loai bo bach cau) 1 don vi (nhom O Rh duong)
  sau rut gon (38 byte): HC rua (loai BC) 1 don vi (O Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 洗涤红细胞（去白）2单位（O型Rh阳性）
  truoc rut gon (58 byte): Hong cau rua (loai bo bach cau) 2 don vi (nhom O Rh duong)
  sau rut gon (38 byte): HC rua (loai BC) 2 don vi (O Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 电解质7项（含钙镁磷）
  truoc rut gon (52 byte): Dien giai 7 chi tieu (bao gom calci, magie, phospho)
  sau rut gon (32 byte): Dien giai 7 chi tieu (Ca, Mg, P)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活冰冻血浆150ml（O型RhD 阳性）
  truoc rut gon (61 byte): Huyet tuong dong lanh bat hoat virus 150ml (nhom O RhD duong)
  sau rut gon (47 byte): HT dong lanh bat hoat virus 150ml (O RhD duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活冰冻血浆200ml（O型RhD 阳性）
  truoc rut gon (61 byte): Huyet tuong dong lanh bat hoat virus 200ml (nhom O RhD duong)
  sau rut gon (47 byte): HT dong lanh bat hoat virus 200ml (O RhD duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活血浆新鲜冰冻血浆200ml（AB型Rh阳性）
  truoc rut gon (66 byte): Huyet tuong tuoi dong lanh bat hoat virus 200ml (nhom AB Rh duong)
  sau rut gon (48 byte): HT tuoi dong lanh bat hoat virus 200ml (AB RhD+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活血浆新鲜冰冻血浆200ml（A型Rh阳性）
  truoc rut gon (65 byte): Huyet tuong tuoi dong lanh bat hoat virus 200ml (nhom A Rh duong)
  sau rut gon (47 byte): HT tuoi dong lanh bat hoat virus 200ml (A RhD+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活血浆新鲜冰冻血浆200ml（B型Rh阳性）
  truoc rut gon (65 byte): Huyet tuong tuoi dong lanh bat hoat virus 200ml (nhom B Rh duong)
  sau rut gon (47 byte): HT tuoi dong lanh bat hoat virus 200ml (B RhD+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 病毒灭活血浆新鲜冰冻血浆200ml（O型Rh阳性）
  truoc rut gon (65 byte): Huyet tuong tuoi dong lanh bat hoat virus 200ml (nhom O Rh duong)
  sau rut gon (47 byte): HT tuoi dong lanh bat hoat virus 200ml (O RhD+)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 细菌性阴道病唾液酸酶测定
  truoc rut gon (56 byte): Dinh luong enzyme sialidase benh viem am dao do vi khuan
  sau rut gon (38 byte): Dinh luong sialidase viem am dao do VK
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 肝功6项（特病专用）
  truoc rut gon (55 byte): Chuc nang gan 6 chi tieu (dung rieng cho benh dac biet)
  sau rut gon (40 byte): Chuc nang gan 6 chi tieu (benh dac biet)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 胸椎MR平扫+弥成像散
  truoc rut gon (60 byte): MR dot song nguc chup khong can quang + chup hinh khuech tan
  sau rut gon (47 byte): MR dot song nguc + khuech tan (khong can quang)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 脑MR平扫+功能成像（fMRI)
  truoc rut gon (56 byte): MR nao chup khong can quang + chup hinh chuc nang (fMRI)
  sau rut gon (35 byte): MR nao + chup hinh chuc nang (fMRI)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 血清人绒毛膜促性腺激素亚单位测定(β-HCG)
  truoc rut gon (72 byte): Dinh luong tieu don vi gonadotropin nhau thai nguoi huyet thanh (β-HCG)
  sau rut gon (29 byte): Dinh luong β-HCG huyet thanh
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 血细胞分析+超敏C反应（末梢血）
  truoc rut gon (66 byte): Phan tich te bao mau + protein phan ung C sieu nhay (mau ngoai vi)
  sau rut gon (44 byte): Phan tich te bao mau + hs-CRP (mau ngoai vi)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 运动心肺功能试验（功率车）
  truoc rut gon (53 byte): Thu nghiem chuc nang tim - phoi gang suc (xe dap luc)
  sau rut gon (41 byte): Thu nghiem tim-phoi gang suc (xe dap luc)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 运动心肺功能试验（跑台）
  truoc rut gon (54 byte): Thu nghiem chuc nang tim - phoi gang suc (may chay bo)
  sau rut gon (42 byte): Thu nghiem tim-phoi gang suc (may chay bo)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 颈椎MR平扫+弥成像散
  truoc rut gon (58 byte): MR cot song co chup khong can quang + chup hinh khuech tan
  sau rut gon (45 byte): MR cot song co + khuech tan (khong can quang)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 鼻咽部和喉部MR平扫
  truoc rut gon (53 byte): MR vung mui - hong va thanh quan chup khong can quang
  sau rut gon (43 byte): MR mui-hong va thanh quan (khong can quang)
- Newtouch_CIS|pacs_jcbw|jcff | gioi han varchar(40)
  cu: 球后全部血管彩色多普勒超声
  truoc rut gon (49 byte): Sieu am Doppler mau toan bo mach mau sau nhan cau
  sau rut gon (37 byte): Sieu am Doppler mau mach sau nhan cau
- Newtouch_CIS|pacs_jcbw|jcff | gioi han varchar(40)
  cu: 腹部大血管彩色多普勒超声
  truoc rut gon (42 byte): Sieu am Doppler mau mach mau lon vung bung
  sau rut gon (33 byte): Sieu am Doppler mau mach lon bung
- Newtouch_CIS|pacs_jcbw|jcff | gioi han varchar(40)
  cu: 颞下颌关节开口位片
  truoc rut gon (47 byte): Phim khop thai duong - ham duoi tu the mo mieng
  sau rut gon (24 byte): Phim khop TD-HD mo mieng
- Newtouch_CIS|pacs_jcbw|jcff | gioi han varchar(40)
  cu: 颞下颌关节开口闭口位片
  truoc rut gon (54 byte): Phim khop thai duong - ham duoi tu the mo - ngam mieng
  sau rut gon (29 byte): Phim khop TD-HD mo-ngam mieng
- Newtouch_CIS|pacs_jcbw|jcff | gioi han varchar(40)
  cu: 颞下颌关节闭口位片
  truoc rut gon (49 byte): Phim khop thai duong - ham duoi tu the ngam mieng
  sau rut gon (26 byte): Phim khop TD-HD ngam mieng
- Newtouch_EMR|bl_ElementDomain_Detail|Element_Name | gioi han varchar(20)
  cu: 体格检查内容
  truoc rut gon (22 byte): Noi dung kham the chat
  sau rut gon (13 byte): Kham the chat
- Newtouch_EMR|bl_ElementDomain_Detail|Element_Name | gioi han varchar(20)
  cu: 同步主诉
  truoc rut gon (25 byte): Dong bo trieu chung chinh
  sau rut gon (19 byte): Dong bo trieu chung
- Newtouch_EMR|Mr_Writing_Rules|RulesType | gioi han varchar(20)
  cu: 病员告知委托书
  truoc rut gon (32 byte): Giay uy thac thong bao benh nhan
  sau rut gon (15 byte): Giay uy thac BN
- Newtouch_MRMS|Sys_Module|Name | gioi han varchar(50)
  cu: 出院病人来源构成表(全院)
  truoc rut gon (53 byte): Bang thanh phan nguon benh nhan xuat vien (toan vien)
  sau rut gon (35 byte): Bang nguon BN xuat vien (toan vien)
- Newtouch_MRMS|Sys_Module|Name | gioi han varchar(50)
  cu: 出院病人来源构成表(科室统计)
  truoc rut gon (57 byte): Bang thanh phan nguon benh nhan xuat vien (thong ke khoa)
  sau rut gon (33 byte): Bang nguon BN xuat vien (TK khoa)
- Newtouch_MRMS|mr_dic_sfdlrelbafee|hissfdlmc | gioi han varchar(50)
  cu: 社区卫生服务及预防保健项目
  truoc rut gon (61 byte): Hang muc dich vu y te cong dong va cham soc suc khoe du phong
  sau rut gon (34 byte): Dich vu y te cong dong va du phong
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 去白细胞悬浮红细胞2单位（O型Rh阳性） 
  truoc rut gon (57 byte): Hong cau treo loai bo bach cau 2 don vi (nhom O Rh duong)
  sau rut gon (37 byte): HC treo loai BC 2 don vi (O Rh duong)
- Newtouch_CIS|jyjc_mbzt|ztmc | gioi han varchar(50)
  cu: 颈椎MR平扫+椎管水成像（MRM)　
  truoc rut gon (71 byte): MR cot song co chup khong can quang + chup hinh nuoc ong tuy song (MRM)
  sau rut gon (38 byte): MR cot song co + MRM (khong can quang)
