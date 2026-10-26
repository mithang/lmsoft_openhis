USE [NewtouchHIS_Sett]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/22/2024 17:42:47




  
  
/*-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  

Người sửa: Đặng Phong
Thời gian sửa: 11:12:35 ngày 21/09/2023
Nội dung sửa: 
1. Giao dịch 4101A sửa mã khoa nhập viện adm_dept_code thành adm_dept_codg,
mã khoa xuất viện dscg_dept_code thành dscg_dept_codg
2. Giao dịch 4101A bổ sung tham số đầu vào tuổi ngày của trẻ sơ sinh age_days
exec usp_Inp_ybupload_cyjs_setlinfoV2 @OrgId='6d5752a7-234a-403e-aa1c-df8b45d3469f',@zyh='03333' 

Người sửa: Đặng Phong
Thời gian sửa: 11:44:45 ngày 07/02/2024
Nội dung sửa: Chỉ lấy thông tin quyết toán lần cuối cùng
-- =============================================*/  
ALTER proc [dbo].[usp_Inp_ybupload_cyjs_setlinfoV2]  
(
 @orgId varchar(50),    
 @zyh varchar(20)    
)
as    


 
declare @age_days varchar(10)/*Tuổi ngày của trẻ sơ sinh*/
select @age_days=nlshow from NewtouchHIS_Sett.dbo.zy_brjbxx with(nolock) where OrganizeId=@orgId and zyh=@zyh and zt=1 and zybz<>'9'

if(@age_days not like'%岁%' and @age_days not like'%月%')
begin
	select @age_days=replace(@age_days,'日','')
	select @age_days=replace(@age_days,'天','')
	select @age_days=ltrim(rtrim(@age_days))
end
else
begin
	select @age_days=null
end



 create table #yb_gx  
(  
ba_code varchar(10),  
ba_name varchar(50),  
yb_code varchar(10),  
yb_name varchar(50),  
)  
  
insert into #yb_gx(ba_code, ba_name, yb_code, yb_name)  
select 0,'本人或户主',1,'本人'  
union all  
select 1,'配偶',10,'配偶'  
union all  
select 2,'子',20,'子'  
union all  
select 3,'女',30,'女'  
union all  
select 4,'孙子、孙女或外孙子、外孙女',40,'孙子、孙女或外孙子'  
union all  
select 5,'父母',50,'父母'  
union all  
select 6,'祖父母或外祖父母',60,'祖父母或外祖父母'  
union all  
select 7,'兄、弟、姐、妹',70,'兄、弟、姐、妹'  
union all  
select 8,'其他',80,'其他'  
  
  
 select    
 a.psn_no ,    
 a.mdtrt_id,  
 a.setl_id,    
 --'' hi_no,  
 b.BAH medcasno,  
 --'重庆重医附二院宽仁康复医院' fixmedins_name,'H50010302473' fixmedins_code,'3' hi_setl_lv,   
 convert(varchar(50),getdate(),120) dcla_time,    
 isnull(c.GJ,'CHN') ntly, -- Quốc tịch, kiểu ký tự, 6, Y, Y   
 isnull(c.zy,'90') prfs, -- Nghề nghiệp, kiểu ký tự, 6, Y, Y  
 (case when b.xzz_sn<>'' then isnull(b.XZZ_SN,'')+isnull(b.XZZ_SI,'')+isnull(b.XZZ_QX,'')+isnull(b.XZZ_JD,'') end) curr_addr, -- Địa chỉ hiện tại, kiểu ký tự, 200   
 b.GZDWJDZ emp_name, -- Tên đơn vị, kiểu ký tự, 200    
 b.GZDWJDZ emp_addr, -- Địa chỉ đơn vị, kiểu ký tự, 200    
 b.DWDH emp_tel, -- Điện thoại đơn vị, kiểu ký tự, 50   
 b.DWYB poscode, -- Mã bưu chính, kiểu ký tự, 6  
 null coner_name, -- Họ tên người liên hệ, kiểu ký tự, 50, Y    
 --b.GX patn_rlts, -- Quan hệ với bệnh nhân, kiểu ký tự, 6, Y, Y    
  isnull(h.yb_code ,1) patn_rlts,  
 (case when b.LXRDZ_SN<>'' then isnull(b.LXRDZ_SN,'')+isnull(b.LXRDZ_SI,'')+isnull(b.LXRDZ_QX,'')+isnull(b.LXRDZ_JD,'') end) coner_addr, -- Địa chỉ người liên hệ, kiểu ký tự, 200, Y   
 b.LXRDH coner_tel, -- Điện thoại người liên hệ, kiểu ký tự, 50, Y   
 '' nwb_adm_type, -- Loại nhập viện của trẻ sơ sinh, kiểu ký tự, 3, Y  
 b.XSECSTZ nwb_bir_wt, -- Cân nặng lúc sinh của trẻ sơ sinh, kiểu số, 6,2, chính xác đến 10 gam (g)  
 b.XSERYTZ nwb_adm_wt, -- Cân nặng lúc nhập viện của trẻ sơ sinh, kiểu số, 6,2, chính xác đến 10 gam (g)  
 '' mul_nwb_bir_wt,  
 '' mul_nwb_adm_wt,  
 null opsp_diag_caty, --Khoa chẩn đoán bệnh mãn tính/đặc biệt ngoại trú, kiểu ký tự, 50, 35    
 null opsp_mdtrt_date, --Ngày khám bệnh mãn tính/đặc biệt ngoại trú, kiểu ngày   
 b.RYTJ adm_way, -- Đường nhập viện, kiểu ký tự, 3, Y   
 '10' trt_type, -- Loại điều trị, kiểu ký tự, 3, Y   
 CONVERT(varchar(100),ciszyxx.ryrq, 120) adm_time, -- Thời gian nhập viện, kiểu ngày giờ  
   ciszyxx.DeptCode adm_dept_codg,--Mã khoa nhập viện  
  dept.name adm_dept_name,  --Tên khoa nhập viện  
  '0' traf_dept_flat, --Cờ chuyển khoa  
 c.ZKKB refldept_dept, -- Khoa chuyển đến, kiểu ký tự, 6, Y, tham chiếu mã khoa (dept); nếu chuyển khoa hơn một lần thì dùng “→” để nối  
  CONVERT(varchar(100),ciszyxx.cqrq, 120) dscg_time, --  Thời gian xuất viện, kiểu ngày giờ   
  ciszyxx.DeptCode dscg_dept_codg,--Mã khoa xuất viện  
  dept.name dscg_dept_name,  --Tên khoa xuất viện  
 --b.CYKB dscg_caty, --  Khoa xuất viện, kiểu ký tự, 6, Y, Y, tham chiếu mã khoa (dept)  
 ( select deptcode from DRG_dept where deptname= b.CYKB)dscg_caty,   
 b.MZZD otp_wm_dise, -- Chẩn đoán Tây y ngoại trú (cấp cứu), kiểu ký tự, 200    
 b.MZZDDM wm_dise_code, -- Mã bệnh chẩn đoán Tây y, kiểu ký tự, 20    
 '' otp_tcm_dise, -- Chẩn đoán Đông y ngoại trú (cấp cứu), kiểu ký tự, 200   
 '' tcm_dise_code, -- Mã chẩn đoán Đông y, kiểu ký tự, 20  
 '' vent_used_dura, -- Thời gian sử dụng máy thở, kiểu ký tự, 10, định dạng: số ngày/số giờ/số phút, ví dụ: 1/13/24   
 '' pwcry_bfadm_coma_dura, -- Thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não, kiểu ký tự, 10, định dạng: số ngày/số giờ/số phút, ví dụ: 1/13/24  
 '' pwcry_afadm_coma_dura, --Thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não, kiểu ký tự, 10, định dạng: số ngày/số giờ/số phút, ví dụ: 1/13/24    
 0 spga_nurscare_days, --Số ngày chăm sóc đặc biệt, kiểu số, 3    
 0 lv1_nurscare_days, --Số ngày chăm sóc cấp 1, kiểu số, 3    
 0 scd_nurscare_days, --Số ngày chăm sóc cấp 2, kiểu số, 3    
 0 lv3_nurscare_days, --Số ngày chăm sóc cấp 3, kiểu số, 3  
 b.LYFS dscg_way, -- Cách rời viện, kiểu ký tự, 3, Y   
 b.YZZY_YLJG acp_medins_name, --Tên cơ sở dự kiến tiếp nhận  
 '' acp_optins_code, --Mã cơ sở dự kiến tiếp nhận  
 zyjs.jsnm bill_code, -- Mã hóa đơn, kiểu ký tự, 50, Y  
 zyjs.jsnm bill_no, -- Số hóa đơn, kiểu ký tự, 30, Y  
 a.medins_setl_id biz_sn, -- Số thứ tự nghiệp vụ, kiểu ký tự, 50, Y  
 b.SFZZYJH days_rinp_flag_31, --Cờ kế hoạch tái nhập viện trong 31 ngày sau xuất viện, kiểu ký tự, 3, Y    
 b.MD days_rinp_pup_31, --Mục đích tái nhập viện trong 31 ngày sau xuất viện, kiểu ký tự, 200  
 --c.ZZYS chfpdr_code, -- Mã bác sĩ chẩn đoán chính, kiểu ký tự, 30  
 --case when c.zzys is null then j.gh else c.zzys  end chfpdr_code,  
  --j.gjybdm chfpdr_code,  
 '' chfpdr_code,  
 CONVERT(varchar(100),b.RYSJ, 120) setl_begn_date, --Ngày bắt đầu quyết toán, kiểu ngày, Y   
  CONVERT(varchar(100),zyjs.CreateTime, 120) setl_end_date, --Ngày kết thúc quyết toán, kiểu ngày, Y   
 d.DepartmentCode medins_fill_dept, --Bộ phận kê khai của cơ sở y tế, kiểu ký tự, 100, Y   
 a.czydm medins_fill_psn, --Người kê khai của cơ sở y tế  
 ''resp_nurs_code, 
  (case when a.clr_way in(1,2,9) then a.clr_way when a.clr_way=3 then 5 when a.clr_way=4 then 6 else 1 end) hi_paymtd, -- Phương thức thanh toán bảo hiểm y tế, kiểu ký tự, 3, Y, Y    
  ''stas_type,
  @age_days age_days/*Tuổi ngày của trẻ sơ sinh*/
 --a.psn_name, --Họ tên người, kiểu ký tự, 50, Y    
 --a.gend, -- Giới tính, kiểu ký tự, 6, Y, Y    
 --a.brdy, -- Ngày sinh, kiểu ngày, Y    
 --a.age, --     
 --b.BZYZSNL nwb_age, --(Tuổi dưới 1 tuổi) Tuổi, kiểu số, 3, bắt buộc khi dưới 1 tuổi, đơn vị ngày    
 --a.naty, -- Dân tộc, kiểu ký tự, 3, Y, Y    
 --a.psn_cert_type  patn_cert_type, --Loại giấy tờ của bệnh nhân, kiểu ký tự, 3, Y, Y    
 --a.certno, -- Số giấy tờ, kiểu ký tự, 50, Y, số giấy tờ của bệnh nhân    
 --a.insutype hi_type, -- Loại bảo hiểm y tế, kiểu ký tự, 3, Y, Y, insutype > hi_type?    
 --'500103' insuplc, -- Nơi tham gia bảo hiểm, kiểu ký tự, 6, Y    
 --'' sp_psn_type, -- Loại đối tượng đặc biệt, kiểu ký tự, 6, Y    
 --1 ipt_med_type, -- Loại y tế nhập viện, kiểu ký tự, 3, Y, Y     
 --c.RYKB adm_caty, -- Khoa nhập viện, kiểu ký tự, 6, Y, Y, tham chiếu mã khoa (dept)      
 --b.SJZYTS act_ipt_days, --  Số ngày nhập viện thực tế, kiểu số, 3     
 --zdcnt diag_code_cnt, --Số lượng mã chẩn đoán, kiểu số, 3    
 --(case when b.MZZDDM=zyzd.JBDM then 1 else 0 end) maindiag_flag, -- Cờ chẩn đoán chính, kiểu ký tự, 3, Y, Y    
 --ss.cnt oprn_oprt_code_cnt, -- Số lượng mã phẫu thuật/thủ thuật, kiểu số, 3    
 --'' bld_cat, -- Loại máu truyền, kiểu ký tự, 3, Y    
 --0 bld_amt, -- Lượng máu truyền, kiểu số, 6    
 --'' bld_unt, -- Đơn vị đo lượng máu truyền, kiểu ký tự, 3     
 --b.ZZYS chfpdr_name, -- Họ tên bác sĩ chẩn đoán chính, kiểu ký tự, 50     
 --a.preselfpay_amt psn_selfpay , --Cá nhân tự trả, kiểu số, 16,2, Y    
 --a.fulamt_ownpay_amt psn_ownpay , --Cá nhân tự chi, kiểu số, 16,2, Y    
 --a.acct_pay , --Chi từ tài khoản cá nhân, kiểu số, 16,2, Y    
 --a.psn_cash_pay psn_cashpay, -- Cá nhân thanh toán bằng tiền mặt, kiểu số, 16,2, Y    
 --'65840' hsorg , --Cơ quan bảo hiểm y tế, kiểu ký tự, 100, Y     
 --'HIS1' hsorg_opter , --Người xử lý của cơ quan bảo hiểm y tế, kiểu ký tự, 50    
 --select *    
 from drjk_zyjs_input i with(nolock)     
 left join drjk_zyjs_output a with(nolock)  on i.setl_id=a.setl_id and i.psn_no=a.psn_no and a.zt='1'    
 left join Newtouch_EMR.dbo.mr_basy b with(nolock)  on a.zyh=b.ZYH  and b.zt='1'  and b.organizeid=@orgId  
 left join Newtouch_EMR.dbo.mr_basy_rel_code c with(nolock)  on b.id=c.syid and b.organizeid=c.organizeid   
 left join (select zyjs.* from  zy_js zyjs    
 left join zy_js zyjs1 on  zyjs.jsnm=zyjs1.cxjsnm or zyjs.cxjsnm=zyjs1.jsnm where zyjs.zyh=@zyh  
and zyjs1.jsnm is null) zyjs on i.zyh=zyjs.zyh and zyjs.zt='1' and b.organizeid=zyjs.organizeid   
  left join [drjk_rybl_input] rybl with(nolock) on i.zyh=rybl.zyh and rybl.zt='1'  
  left join Newtouch_CIS..zy_brxxk ciszyxx on i.zyh=ciszyxx.zyh and ciszyxx.zt='1'  and b.organizeid=ciszyxx.organizeid   
 --left join(select organizeid,bah,zyh,count(ssjczbm)cnt     
 --  from Newtouch_EMR.dbo.mr_basy_ss   with(nolock)    
 --  where zt='1'    
 --  group by bah,zyh,organizeid) ss on b.BAH=ss.BAH and b.OrganizeId=ss.OrganizeId and b.ZYH=ss.ZYH    
 --left join(select organizeid,bah,zyh,count(JBDM)zdcnt     
 --  from Newtouch_EMR.dbo.mr_basy_zd  with(nolock)    
 --  where zt='1'    
 --  group by bah,zyh,organizeid) zd on b.BAH=zd.BAH and b.OrganizeId=zd.OrganizeId and b.ZYH=zd.ZYH    
 --left join Newtouch_EMR.dbo.mr_basy_zd zyzd with(nolock) on b.BAH=zyzd.BAH and b.OrganizeId=zyzd.OrganizeId and b.ZYH=zyzd.ZYH and zyzd.ZDOrder=1    
 left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] d on a.czydm=d.gh --and OrganizeId=''    
 left join [NewtouchHIS_Base].[dbo].[Sys_Department] dept on ciszyxx.deptCode=dept.Code and ciszyxx.organizeId=dept.organizeId and dept.zt=1   
 left join [NewtouchHIS_Sett].dbo.DRG_dept f on  f.deptName=dept.name   
 left join [Newtouch_MRMS].dbo.[mr_dic_common] g on g.RlueName='关系' and g.itemName=b.GX  
  left join #yb_gx h on h.ba_name=b.GX  
  left join [NewtouchHIS_Base].dbo.sys_staff j on j.name =b.zzys  
 where i.zt='1' and a.zt='1'    
 and  d.OrganizeId=@orgId and     
 a.zyh=@zyh    
 order by a.setl_id desc/*Tải lên thông tin quyết toán lần cuối cùng*/
    
    
 drop table #yb_gx  
return
  
  
  
  
  
  

