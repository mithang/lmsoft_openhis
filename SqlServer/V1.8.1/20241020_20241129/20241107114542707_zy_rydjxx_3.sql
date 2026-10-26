USE [NewtouchHIS_Sett]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/07/2024 11:45:42



















/**
exec [zy_rydjxx] '03334','6d5752a7-234a-403e-aa1c-df8b45d3469f','02','152524194801044224','21','','1',''
*/

  
ALTER PROCEDURE [dbo].[zy_rydjxx]     
@hisId varchar(20), --Số nhập viện  
@orgId varchar(50),--Tổ chức  
@mdtrt_cert_type varchar(5), --Loại khám bệnh  
@mdtrt_cert_no varchar(50), --Số chứng từ khám bệnh  
@med_type varchar(20),--Loại hình y tế  
@mdtrt_id varchar(50), --ID khám bệnh  
@type varchar(2), --Loại: 1 đăng ký, 2 sửa thông tin, 3 làm thủ tục xuất viện  
@hisIdNum varchar(10) --Số ngẫu nhiên 2 chữ số
as  
if @type='1'  
begin  
--declare @Rnum varchar (10)= CAST(CEILING(RAND()* 100) AS varchar);
declare @jzlx varchar(10)='310';
if(@mdtrt_cert_type='02')
begin
set @jzlx='4' --Chứng minh nhân dân
end
if(@mdtrt_cert_type='01') --Chứng từ điện tử
begin
set @jzlx='3'
end
if(@mdtrt_cert_type='03') --Thẻ bảo hiểm xã hội
begin
set @jzlx='2'
end
select cd.grbh psn_no,--Mã nhân viên  
'390' insutype,--Loại hình bảo hiểm 
  --  cd.xzlx insutype,--Loại hình bảo hiểm  
    a.lxr coner_name,--Họ tên người liên hệ  
    a.lxrdh tel,--Số điện thoại liên hệ  
    convert(varchar(20),a.ryrq,120) begntime,--Thời gian bắt đầu  
	 '02'  mdtrt_cert_type,--Loại chứng từ khám bệnh 
    --@mdtrt_cert_type  mdtrt_cert_type,--Loại chứng từ khám bệnh  
	'152524194801044224' mdtrt_cert_no,
   -- @mdtrt_cert_no mdtrt_cert_no,--Số chứng từ khám bệnh  
    @med_type med_type,--Loại hình y tế  
    @hisId+isnull(@hisIdNum,'')  ipt_no,--Số nhập viện  
    a.blh medrcdno,--Số hồ sơ bệnh án  
    c.gjybdm atddr_no,--Mã bác sĩ điều trị chính  
    c.name chfpdr_name,--Họ tên bác sĩ chẩn đoán chính  
    d.zdmc adm_diag_dscr,--Mô tả chẩn đoán nhập viện  
    dept.ybksbm adm_dept_codg,--Mã khoa nhập viện  
    dept.name adm_dept_name,--Tên khoa nhập viện  
    a.cw adm_bed,--Giường nhập viện  
    d.icd10 dscg_maindiag_code,--Mã chẩn đoán chính khi nhập viện  
    d.zdmc dscg_maindiag_name, --Tên chẩn đoán chính khi nhập viện  
    '' main_cond_dscr,--Mô tả tình trạng bệnh chính  
    isnull(bzbm,'') dise_codg, --Mã loại bệnh  
    isnull(bzmc,'') dise_name,--Tên loại bệnh  
    ''  oprn_oprt_code,--Mã phẫu thuật/thủ thuật  
    '' oprn_oprt_name,--Tên phẫu thuật/thủ thuật  
    a.syfwzh fpsc_no,--Số giấy chứng nhận dịch vụ kế hoạch hóa gia đình  
    case rytj when '52' then '1' else '' end matn_type,--Loại sinh đẻ  
    '' birctrl_type,--Loại phẫu thuật kế hoạch hóa gia đình  
    case rytj when '52' then '0' else '' end latechb_flag,--Cờ sinh muộn  
    0 geso_val, --Số tuần thai  
    0 fetts,--Số lần mang thai  
    0 fetus_cnt,--Số thai nhi  
    case rytj when '52' then '0' else '' end pret_flag,--Cờ sinh non  
    case  when syrq is  null then convert(varchar(10),GETDATE(),120) else convert(varchar(10),syrq,120) end  birctrl_matn_date,--Ngày phẫu thuật kế hoạch hóa gia đình hoặc ngày sinh  
    '' dise_type_code--Loại bệnh  
from NewtouchHIS_Sett..zy_brjbxx a  
LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = a.patid  
                AND b.OrganizeId = a.OrganizeId  
                AND b.zt = '1'  
LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c ON c.OrganizeId = a.OrganizeId  
                  AND c.gh = a.doctor  
                  AND c.zt = '1'  
left join [NewtouchHIS_Sett].[dbo].[zy_rydzd] d on d.zyh=a.zyh and d.OrganizeId=a.OrganizeId and d.zt='1' and d.zdpx='1'  
left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=a.ks and dept.OrganizeId=a.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = a.OrganizeId and cd.CardType=@jzlx and cd.patid=a.patid
                                                              --AND cd.CardNo = a.kh
                                                              AND cd.zt = '1'
where a.zyh=@hisId and a.OrganizeId=@orgId and a.zt='1' and  a.zybz<>'9'  
end  
  
else if @type='3'  
begin  
SELECT  jzdj.jylsh mdtrt_id,--ID khám bệnh  
  cd.grbh psn_no,--Mã nhân viên  
     cd.xzlx insutype,--Loại hình bảo hiểm  
     convert(varchar(20),ciszyxx.cqrq,120)  endtime,--Thời gian kết thúc 
	 --convert(varchar(20),zybrxx.cyrq,120)  endtime, 
  isnull(bzbm,'') dise_codg, --Mã loại bệnh  
   isnull(bzmc,'') dise_name,--Tên loại bệnh   
  '' oprn_oprt_code,--Mã phẫu thuật/thủ thuật  
  '' oprn_oprt_name,--Tên phẫu thuật/thủ thuật  
  zybrxx.syfwzh fpsc_no,--Số giấy chứng nhận dịch vụ kế hoạch hóa gia đình  
  '' matn_type,--Loại sinh đẻ  
  '' birctrl_type,--Loại phẫu thuật kế hoạch hóa gia đình  
  '' latechb_flag,--Cờ sinh muộn  
  0 geso_val,--Số tuần thai  
  0 fetts,--Số lần mang thai  
  0 fetus_cnt,--Số thai nhi  
  '' pret_flag,--Cờ sinh non  
  case  when syrq is  null then convert(varchar(10),GETDATE(),120) else convert(varchar(10),syrq,120) end birctrl_matn_date,--Ngày phẫu thuật kế hoạch hóa gia đình hoặc ngày sinh  
  '' cop_flag,--Cờ có biến chứng kèm theo  
         dept.ybksbm dscg_dept_codg,--ciszyxx.DeptCode cyks ,Mã khoa xuất viện  
   dept.Name dscg_dept_name,--Tên khoa xuất viện  
   zybrxx.cw dscg_bed,--Giường xuất viện  
  --case cyzd.cyqk when '6' then '2' when '4' then '5' when '5' then '9' else '1' end dscg_way,--Cách rời viện  
  --'' die_date --Ngày tử vong  
  case ciszyxx.cyfs when '3' then '2' when '4' then '5' else '1' end dscg_way,--Cách rời viện: 2 chuyển viện, 5 tử vong, 1 rời viện theo y lệnh
  case ciszyxx.cyfs when '4' then convert(varchar(10),cyrq,121) else  '' end die_date --Ngày tử vong 
FROM    zy_brjbxx zybrxx  
        INNER JOIN Newtouch_CIS..zy_brxxk ciszyxx ON ciszyxx.OrganizeId = zybrxx.OrganizeId  
                                                     AND ciszyxx.zyh = zybrxx.zyh  
                                                     AND ciszyxx.zt = '1'  
  left join [Newtouch_CIS].[dbo].[zy_PatDxInfo] cyzd on cyzd.zyh=zybrxx.zyh and cyzd.OrganizeId=zybrxx.OrganizeId and cyzd.zt='1'  
             and cyzd.zdlx='0' and zdlb='2'  
  LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = zybrxx.patid  
                AND b.OrganizeId = zybrxx.OrganizeId  
                AND b.zt = '1'  
  left join [NewtouchHIS_Sett]..[cqyb_OutPut02] jzdj on jzdj.zymzh=zybrxx.zyh and jzdj.OrganizeId=zybrxx.OrganizeId and jzdj.zt='1'  
        LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] staff ON staff.OrganizeId = ciszyxx.OrganizeId  
                                                              AND staff.gh = ciszyxx.ysgh  
                                                              AND staff.zt = '1'  
        left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=ciszyxx.DeptCode and dept.OrganizeId=ciszyxx.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = zybrxx.OrganizeId
                                                              AND cd.CardNo = zybrxx.kh
                                                              AND cd.zt = '1'
WHERE   zybrxx.zt = '1'  
        AND zybrxx.zyh = @hisId  
        AND zybrxx.OrganizeId = @orgId  
  
end  
  
else  
begin  
  
select   
    jzdj.jylsh,--ID khám bệnh  
    cd.grbh psn_no,--Mã nhân viên  
    a.lxr coner_name,--Họ tên người liên hệ  
    a.lxrdh tel,--Số điện thoại liên hệ  
    convert(varchar(20),a.ryrq,120) begntime,--Thời gian bắt đầu  
    convert(varchar(20),getdate(),120) endtime,--Thời gian kết thúc  
    @mdtrt_cert_type  mdtrt_cert_type,--Loại chứng từ khám bệnh  
    @med_type med_type,--Loại hình y tế  
    (a.zyh+isnull(jzh,'')) ipt_otp_no,--Số nhập viện  
    a.blh medrcdno,--Số hồ sơ bệnh án  
    c.gjybdm atddr_no,--Mã bác sĩ điều trị chính  
    c.name chfpdr_name,--Họ tên bác sĩ chẩn đoán chính  
    isnull(d.zdmc,ryzd.icd10) adm_diag_dscr,--Mô tả chẩn đoán nhập viện  
    dept.ybksbm adm_dept_codg,--Mã khoa nhập viện  
    dept.name adm_dept_name,--Tên khoa nhập viện  
    a.cw adm_bed,--Giường nhập viện  
    isnull(d.zddm,ryzd.icd10) dscg_maindiag_code,--Mã chẩn đoán chính khi nhập viện  
    isnull(d.zdmc,ryzd.zdmc) dscg_maindiag_name, --Tên chẩn đoán chính khi nhập viện  
    '' main_cond_dscr,--Mô tả tình trạng bệnh chính  
    isnull(bzbm,'') dise_codg, --Mã loại bệnh  
    isnull(bzmc,'') dise_name,--Tên loại bệnh   
    ''  oprn_oprt_code,--Mã phẫu thuật/thủ thuật  
    '' oprn_oprt_name,--Tên phẫu thuật/thủ thuật  
    a.syfwzh fpsc_no,--Số giấy chứng nhận dịch vụ kế hoạch hóa gia đình  
    case rytj when '52' then '1' else '' end matn_type,--Loại sinh đẻ  
    '' birctrl_type,--Loại phẫu thuật kế hoạch hóa gia đình  
     case rytj when '52' then '0' else '' end latechb_flag,--Cờ sinh muộn  
    0 geso_val, --Số tuần thai  
    0 fetts,--Số lần mang thai  
    0 fetus_cnt,--Số thai nhi  
    case rytj when '52' then '0' else '' end pret_flag,--Cờ sinh non  
    case  when syrq is  null then convert(varchar(10),'2021-12-31 23:53:53',120) else convert(varchar(10),syrq,120) end birctrl_matn_date,--Ngày phẫu thuật kế hoạch hóa gia đình hoặc ngày sinh  
    '' dise_type_code--Loại bệnh  
from NewtouchHIS_Sett..zy_brjbxx a  
LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] b ON b.patid = a.patid  
                AND b.OrganizeId = a.OrganizeId  
                AND b.zt = '1'  
left join [NewtouchHIS_Sett]..[cqyb_OutPut02] jzdj on jzdj.zymzh=a.zyh and jzdj.OrganizeId=a.OrganizeId and jzdj.zt='1'  
LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c ON c.OrganizeId = a.OrganizeId  
                  AND c.gh = a.doctor  
                  AND c.zt = '1'  
LEFT JOIN [Newtouch_CIS].[dbo].[zy_PatDxInfo] d ON d.zyh = a.zyh  
                  AND d.OrganizeId = a.OrganizeId  
                  AND d.zt = '1' and d.zdlb='1' and d.zdlx='0'
LEFT JOIN [NewtouchHIS_Sett].[dbo].zy_rydzd ryzd ON ryzd.zyh = a.zyh  
                  AND ryzd.OrganizeId = a.OrganizeId  
                  AND ryzd.zt = '1' and ryzd.zdpx='1' 
left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=a.ks and dept.OrganizeId=a.OrganizeId and dept.zt='1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = a.OrganizeId
                                                              AND cd.CardNo = a.kh
                                                              AND cd.zt = '1'
where a.zyh=@hisId and a.OrganizeId=@orgId and a.zt='1' and  a.zybz<>'9'  
  
end  
  
  

















