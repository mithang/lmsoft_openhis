USE [NewtouchHIS_Sett]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/14/2024 10:56:36


/*
exec mz_fymxsc '0','2304202120016',null,null,null,'false','false','6d5752a7-234a-403e-aa1c-df8b45d3469f','843710463','13030011303013054434','2023420163322_nciro041y',null,'0204','000164',null
*/

  
  
--Tải lên chi tiết chi phí ngoại trú  
ALTER PROCEDURE [dbo].[mz_fymxsc]   
@type varchar(1),--0 đăng ký khám, 1 đơn thuốc, 3 đơn thuốc hoàn phí rồi tải lại
@mzh varchar(20),  
@cfnm varchar(500), --Mã nội bộ đơn thuốc
@ghxm varchar(20),  --Hạng mục đăng ký khám
@zlxm varchar(20),  --Hạng mục chẩn đoán và điều trị
@ckf  varchar(20),  --Phí thẻ từ
@gbf varchar(20),  --Phí làm thẻ
@orgId varchar(50),  --Tổ chức
@jzid varchar(50),  --ID khám bệnh
@rybh varchar(50),  --Mã nhân viên
@pch varchar(50), --Số lô thu phí
@jsnm varchar(50), --Mã nội bộ quyết toán 
@ksbm varchar(50),--Mã khoa
@ysbm varchar(50),--Mã bác sĩ
@dise_codg varchar(50)--Mã loại bệnh
as  
  
--INSERT xt_lshtb(col) VALUES(1) --Số thứ tự chi tiết mới  
if @type='0'  
begin  
declare @ksmc varchar(50);
declare @ysmc varchar(50);
declare @gjysbm varchar(50);
select @ysmc=Name,@gjysbm=gjybdm from NewtouchHIS_Base..V_S_Sys_Staff where gh=@ysbm and organizeid=@orgId and zt=1
select @ksmc=Name from NewtouchHIS_Base..V_S_Sys_Department where Code=@ksbm and organizeid=@orgId and zt=1
--Tải lên hạng mục đăng ký khám và chẩn đoán điều trị ngoại trú  
SELECT cast(CONVERT(bigint, CONVERT(varbinary, CAST(N'0x' + replace(newid(),'-','') AS char),1)) as varchar) feedetl_sn ,--Số thứ tự chi tiết chi phí  
  @jzid mdtrt_id,--ID khám bệnh  
  @rybh psn_no,--Mã nhân viên  
  @pch chrg_bchno,--Số lô thu phí  
  '' dise_codg,--Mã loại bệnh  
  @mzh rxno,--Số đơn thuốc  
  '0' rx_circ_flag,--Cờ đơn thuốc mua ngoài  
   convert(varchar(50),GETDATE(),120)   fee_ocur_time, --Thời gian phát sinh chi phí  
  gjybdm med_list_codg,--Mã danh mục y tế  
  sfxmcode medins_list_codg,--Mã danh mục của cơ sở y tế  
   Convert(decimal(13,2),dj) det_item_fee_sumamt,--Tổng chi phí hạng mục chi tiết  
        CONVERT(NUMERIC(10,2),1) cnt ,--Số lượng  
  dj pric,--Đơn giá  
  '' sin_dos_dscr,--Mô tả liều dùng một lần  
  ''used_frqu_dscr,--Mô tả tần suất sử dụng  
  CONVERT(NUMERIC(4,2),1) prd_days,--Số ngày chu kỳ  
  '' medc_way_dscr,--Mô tả đường dùng thuốc  
  @ksbm bilg_dept_codg,--Mã khoa lập phiếu  
  @ksmc bilg_dept_name,--Tên khoa lập phiếu  
  @gjysbm bilg_dr_codg, --Mã bác sĩ lập phiếu  
  @ysmc bilg_dr_name,--Họ tên bác sĩ lập phiếu  
  @ksbm acord_dept_codg,--Mã khoa nhận phiếu  
  @ksmc acord_dept_name,--Tên khoa nhận phiếu  
  @gjysbm orders_dr_code,--Mã bác sĩ nhận phiếu  
  @ysmc orders_dr_name,--Họ tên bác sĩ nhận phiếu  
  cast('1' as varchar) hosp_appr_flag,--Cờ phê duyệt của bệnh viện  
  '' tcmdrug_used_way,--Cách dùng thuốc Đông y  
  '' etip_flag,--Cờ xét nghiệm ngoài  
  '' etip_hosp_code,--Mã bệnh viện xét nghiệm ngoài  
  '' dscg_tkdrug_flag,--Cờ mang thuốc khi xuất viện  
  '' matn_fee_flag--Cờ chi phí sinh đẻ  
FROM    NewtouchHIS_Base..V_S_xt_sfxm  
WHERE  isnull(zfxz,'1')!='1' and OrganizeId =@orgId AND zt='1'  
        AND (sfxmCode IN ( @ghxm ) or 
        sfxmCode IN ( select zlxm from mz_gh_zlxmzh with(nolock) where zt='1'   
and OrganizeId=@orgId and zhcode=@zlxm ) or sfxmCode IN ( @ckf ) or sfxmCode IN (  @gbf ))  
end  
else if  @type='3'
begin
     SELECT    'yp'+cast(mx.cfmxid as varchar) feedetl_sn ,--Số thứ tự chi tiết chi phí  
  gh.jzid mdtrt_id,--ID khám bệnh  
  cd.grbh psn_no,--Mã nhân viên  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--Số lô thu phí  
  @dise_codg dise_codg,--Mã loại bệnh  
  cf.cfh rxno,--Số đơn thuốc  
  '0' rx_circ_flag,--Cờ đơn thuốc mua ngoài  
   convert(varchar(50),mx.CreateTime,120) fee_ocur_time, --Thời gian phát sinh chi phí  
  yp.gjybdm med_list_codg,--Mã danh mục y tế  
  yp.ypCode medins_list_codg,--Mã danh mục của cơ sở y tế  
   Convert(decimal(13,2),ISNULL(mx.je, 0.00)) det_item_fee_sumamt,--Tổng chi phí hạng mục chi tiết  
        ISNULL(mx.sl, 0.00) cnt ,--Số lượng  
  ISNULL(mx.dj, 0.00) pric,--Đơn giá  
  cast(mx.jl as varchar) sin_dos_dscr,--Mô tả liều dùng một lần  
  ''used_frqu_dscr,--Mô tả tần suất sử dụng  
  CONVERT(NUMERIC(4,2),1) prd_days,--Số ngày chu kỳ  
  '' medc_way_dscr,--Mô tả đường dùng thuốc  
  ks.ybksbm bilg_dept_codg,--Mã khoa lập phiếu  
  ks.Name bilg_dept_name,--Tên khoa lập phiếu 
  isnull(staff.gjybdm,staff.zjh) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  --isnull(staff.zjh,cf.ys) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  cf.ysmc bilg_dr_name,--Họ tên bác sĩ lập phiếu  
  ks.ybksbm acord_dept_codg,--Mã khoa nhận phiếu  
  ks.Name acord_dept_name,--Tên khoa nhận phiếu  
  isnull(staff.gjybdm,staff.zjh) orders_dr_code,--Mã bác sĩ nhận phiếu  
  --isnull(staff.zjh,cf.ys) orders_dr_code,--Mã bác sĩ nhận phiếu 
  cf.ysmc orders_dr_name,--Họ tên bác sĩ nhận phiếu  
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--Cờ phê duyệt của bệnh viện  
  '' tcmdrug_used_way,--Cách dùng thuốc Đông y  
  '' etip_flag,--Cờ xét nghiệm ngoài  
  '' etip_hosp_code,--Mã bệnh viện xét nghiệm ngoài  
  '' dscg_tkdrug_flag,--Cờ mang thuốc khi xuất viện  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag, --Cờ chi phí sinh đẻ 
  a.jsmxnm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cfmx] (NOLOCK) mx ON a.cf_mxnm = mx.cfmxId
                                                              AND mx.OrganizeId = a.OrganizeId
                                                              AND mx.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = mx.cfnm
                                                              AND cf.OrganizeId = mx.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1 
                    INNER JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = mx.OrganizeId
                                                              AND yp.zt = '1'
                                                              AND  isnull(yp.zfxz,'1')!='1'  
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
                    LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.CardNo = gh.kh
                                                              AND cd.zt = '1'
                   
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.cf_mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
          UNION ALL
          SELECT    'xm'+cast(xm.xmnm as varchar) feedetl_sn ,--Số thứ tự chi tiết chi phí  
  gh.jzid mdtrt_id,--ID khám bệnh  
  cd.grbh psn_no,--Mã nhân viên  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--Số lô thu phí  
  @dise_codg dise_codg,--Mã loại bệnh  
  cf.cfh rxno,--Số đơn thuốc  
  '0' rx_circ_flag,--Cờ đơn thuốc mua ngoài  
   convert(varchar(50),a.CreateTime,120) fee_ocur_time, --Thời gian phát sinh chi phí  
  sfxm.gjybdm med_list_codg,--Mã danh mục y tế  
  sfxm.sfxmCode medins_list_codg,--Mã danh mục của cơ sở y tế  
   Convert(decimal(13,2),ISNULL(xm.je, 0.00)) det_item_fee_sumamt,--Tổng chi phí hạng mục chi tiết  
        ISNULL(xm.sl, 0.00) cnt ,--Số lượng  
  ISNULL(xm.dj, 0.00) pric,--Đơn giá  
  null sin_dos_dscr,--Mô tả liều dùng một lần  
  ''used_frqu_dscr,--Mô tả tần suất sử dụng  
  CONVERT(NUMERIC(4,2),1) prd_days,--Số ngày chu kỳ  
  '' medc_way_dscr,--Mô tả đường dùng thuốc  
  ks.ybksbm bilg_dept_codg,--Mã khoa lập phiếu  
  ks.Name bilg_dept_name,--Tên khoa lập phiếu  
  isnull(staff.gjybdm,staff.zjh) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  --isnull(staff.zjh,cf.ys) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  cf.ysmc bilg_dr_name,--Họ tên bác sĩ lập phiếu  
  ks.ybksbm acord_dept_codg,--Mã khoa nhận phiếu  
  ks.Name acord_dept_name,--Tên khoa nhận phiếu  
  isnull(staff.gjybdm,staff.zjh) orders_dr_code,--Mã bác sĩ nhận phiếu  
  --isnull(staff.zjh,cf.ys) orders_dr_code,--Mã bác sĩ nhận phiếu 
  cf.ysmc orders_dr_name,--Họ tên bác sĩ nhận phiếu    
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--Cờ phê duyệt của bệnh viện  
  '' tcmdrug_used_way,--Cách dùng thuốc Đông y  
  '' etip_flag,--Cờ xét nghiệm ngoài  
  '' etip_hosp_code,--Mã bệnh viện xét nghiệm ngoài  
  '' dscg_tkdrug_flag,--Cờ mang thuốc khi xuất viện  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag, --Cờ chi phí sinh đẻ  
  a.jsmxnm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_xm] (NOLOCK) xm ON a.mxnm = xm.xmnm
                                                              AND xm.OrganizeId = a.OrganizeId
                                                              AND xm.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                              AND cf.OrganizeId = xm.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1 
                    INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
                                                              AND sfxm.zt = '1'
                                                              AND isnull(sfxm.zfxz,'1')!='1' 
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId 
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
     
end
else
begin   
--Tải lên chi tiết đơn thuốc ngoại trú    
SELECT  'yp'+cast(mx.cfmxid as varchar) feedetl_sn ,--Số thứ tự chi tiết chi phí  
  gh.jzid mdtrt_id,--ID khám bệnh  
  cd.grbh psn_no,--Mã nhân viên  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--Số lô thu phí  
  @dise_codg dise_codg,--Mã loại bệnh  
  cf.cfh rxno,--Số đơn thuốc  
  '0' rx_circ_flag,--Cờ đơn thuốc mua ngoài  
     convert(varchar(50),mx.CreateTime,120) fee_ocur_time, --Thời gian phát sinh chi phí  
  yp.gjybdm med_list_codg,--Mã danh mục y tế  
  yp.ypCode medins_list_codg,--Mã danh mục của cơ sở y tế  
   Convert(decimal(13,2),ISNULL(mx.je, 0.00)) det_item_fee_sumamt,--Tổng chi phí hạng mục chi tiết  
        ISNULL(mx.sl, 0.00) cnt ,--Số lượng  
  ISNULL(mx.dj, 0.00) pric,--Đơn giá  
  cast(mx.jl as varchar) sin_dos_dscr,--Mô tả liều dùng một lần  
  ''used_frqu_dscr,--Mô tả tần suất sử dụng  
  CONVERT(NUMERIC(4,2),1) prd_days,--Số ngày chu kỳ  
  '' medc_way_dscr,--Mô tả đường dùng thuốc  
  ks.ybksbm bilg_dept_codg,--Mã khoa lập phiếu  
  ks.Name bilg_dept_name,--Tên khoa lập phiếu  
  isnull(staff.gjybdm,cf.ys) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  cf.ysmc bilg_dr_name,--Họ tên bác sĩ lập phiếu  
  ks.ybksbm acord_dept_codg,--Mã khoa nhận phiếu  
  ks.Name acord_dept_name,--Tên khoa nhận phiếu  
  isnull(staff.gjybdm,cf.ys) orders_dr_code,--Mã bác sĩ nhận phiếu   
  cf.ysmc orders_dr_name,--Họ tên bác sĩ nhận phiếu  
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--Cờ phê duyệt của bệnh viện  
  '' tcmdrug_used_way,--Cách dùng thuốc Đông y  
  '' etip_flag,--Cờ xét nghiệm ngoài  
  '' etip_hosp_code,--Mã bệnh viện xét nghiệm ngoài  
  '' dscg_tkdrug_flag,--Cờ mang thuốc khi xuất viện  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag--Cờ chi phí sinh đẻ  
        FROM      mz_cf (NOLOCK) cf  
                INNER JOIN mz_cfmx (NOLOCK) mx ON cf.cfnm = mx.cfnm  
                                                    AND mx.OrganizeId = cf.OrganizeId  
                                                    AND mx.zt = '1'  
                INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm  
                                                    AND gh.zt = '1'  
                                                    AND gh.OrganizeId = cf.OrganizeId  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1  
                INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode = mx.yp  
                                                            AND yp.OrganizeId = cf.OrganizeId  
                                                            AND yp.zt = '1'  
                                                           AND isnull(yp.zfxz,'1')!='1'
                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = mx.dl  
                                                            AND dl.OrganizeId = cf.OrganizeId  
                          AND dl.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId  
                                                            AND ks.Code = cf.ks  
                                                            AND ks.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId  
                                                            AND staff.gh = cf.ys  
                                                            AND staff.zt = '1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
        WHERE     cf.OrganizeId = @orgId  
                AND cf.zt = '1'  
                AND cf.cfzt = '0' --Đơn thuốc còn hiệu lực và chưa thu phí  
                AND gh.mzh = @mzh  
                --AND cf.cfnm = @cfnm
                AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
UNION ALL  
       SELECT   'xm'+cast(xm.xmnm as varchar) feedetl_sn ,--Số thứ tự chi tiết chi phí  
  gh.jzid mdtrt_id,--ID khám bệnh  
  cd.grbh psn_no,--Mã nhân viên  
  @pch chrg_bchno,--dbo.f_NextBH() chrg_bchno,--Số lô thu phí  
  @dise_codg dise_codg,--Mã loại bệnh  
  cf.cfh rxno,--Số đơn thuốc  
  '0' rx_circ_flag,--Cờ đơn thuốc mua ngoài  
   convert(varchar(50),xm.CreateTime,120) fee_ocur_time, --Thời gian phát sinh chi phí  
  sfxm.gjybdm med_list_codg,--Mã danh mục y tế  
  sfxm.sfxmCode medins_list_codg,--Mã danh mục của cơ sở y tế  
   Convert(decimal(13,2),ISNULL(xm.je, 0.00)) det_item_fee_sumamt,--Tổng chi phí hạng mục chi tiết  
        ISNULL(xm.sl, 0.00) cnt ,--Số lượng  
  ISNULL(xm.dj, 0.00) pric,--Đơn giá  
  null sin_dos_dscr,--Mô tả liều dùng một lần  
  ''used_frqu_dscr,--Mô tả tần suất sử dụng  
  CONVERT(NUMERIC(4,2),1) prd_days,--Số ngày chu kỳ  
  '' medc_way_dscr,--Mô tả đường dùng thuốc  
  ks.ybksbm bilg_dept_codg,--Mã khoa lập phiếu  
  ks.Name bilg_dept_name,--Tên khoa lập phiếu  
  isnull(staff.gjybdm,cf.ys) bilg_dr_codg, --Mã bác sĩ lập phiếu  
  cf.ysmc bilg_dr_name,--Họ tên bác sĩ lập phiếu  
  ks.ybksbm acord_dept_codg,--Mã khoa nhận phiếu  
  ks.Name acord_dept_name,--Tên khoa nhận phiếu  
  isnull(staff.gjybdm,cf.ys) orders_dr_code,--Mã bác sĩ nhận phiếu   
  cf.ysmc orders_dr_name,--Họ tên bác sĩ nhận phiếu   
  case isnull(zzfbz,'0') when '1' then '2' else '1' end hosp_appr_flag,--Cờ phê duyệt của bệnh viện  
  '' tcmdrug_used_way,--Cách dùng thuốc Đông y  
  '' etip_flag,--Cờ xét nghiệm ngoài  
  '' etip_hosp_code,--Mã bệnh viện xét nghiệm ngoài  
  '' dscg_tkdrug_flag,--Cờ mang thuốc khi xuất viện  
  case gh.mjzbz when  '9' then '1' else '' end matn_fee_flag--Cờ chi phí sinh đẻ  
        FROM      dbo.mz_xm (NOLOCK) xm  
                INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = xm.ghnm  
                                                    AND gh.OrganizeId = xm.OrganizeId  
                                                    AND gh.zt = '1'  
                INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1  
                LEFT JOIN dbo.mz_cf (NOLOCK) cf ON cf.cfnm = xm.cfnm  
                                                    AND cf.OrganizeId = xm.OrganizeId  
                INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm  
                                                            AND sfxm.OrganizeId = xm.OrganizeId  
                                                           AND isnull(sfxm.zfxz,'1')!='1'    
                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = xm.dl  
                                                            AND dl.OrganizeId = xm.OrganizeId  
                                                            AND dl.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId  
                                                            AND ks.Code = cf.ks  
                                                            AND ks.zt = '1'  
                LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId  
                                                            AND staff.gh = cf.ys  
                                                            AND staff.zt = '1'  
LEFT JOIN xt_card (NOLOCK) cd ON cd.OrganizeId = gh.OrganizeId
                                                              AND cd.CardNo = gh.kh and cd.CardTypeName=gh.CardTypeName
                                                              AND cd.zt = '1'
        WHERE     xm.OrganizeId = @orgId  
                AND xm.zt = '1'  
                AND xm.xmzt = '0' --Còn hiệu lực và chưa thu phí   
                AND ( cf.zt IS NULL  
                        OR ( cf.zt = '1'  
                            AND cf.cfzt = '0'  
                            )  
                    ) --Chưa liên kết đơn thuốc, hoặc đơn thuốc còn hiệu lực và chưa thu phí  
                AND gh.mzh = @mzh  
                --AND cf.cfnm = @cfnm   
                AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
  
end  

