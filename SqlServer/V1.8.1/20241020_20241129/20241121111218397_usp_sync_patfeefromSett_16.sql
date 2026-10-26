USE [Newtouch_EMR]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/21/2024 11:12:18


/*======================================
Mô tả quy trình: Tính ngược chi phí trang đầu hồ sơ bệnh án
Người sửa: Đặng Phong
Thời gian sửa: 16:3:17 ngày 27/02/2023
Nội dung sửa: Tối ưu hoá việc tra cứu chi phí và cập nhật chi phí trang đầu hồ sơ bệnh án trong quy trình

Người sửa: Đặng Phong
Thời gian sửa: 17:47:12 ngày 10/03/2023
Nội dung sửa: Khi tổng chi phí bằng 0 thì hiển thị 0

Hệ thống backend: Hạng mục thu phí được bảo trì riêng theo nhóm lớn hồ sơ bệnh án, phiên bản; mã hồ sơ bệnh án xt_basfdl cần đặt tên theo giá trị FeetypeCode chuyển dọc thành ngang
exec [usp_sync_patfeefromSett] '6d5752a7-234a-403e-aa1c-df8b45d3469f','03335'
======================================*/

alter proc [dbo].[usp_sync_patfeefromSett] 
(
@orgId varchar(50),      
@zyh varchar(20)
)
as 

--declare
--@orgId varchar(50),      
--@zyh varchar(20)
--select @orgId=N'9bb029d0-5da0-4118-9d19-06b829eede46',@zyh=N'00040'
CREATE TABLE #temptable 
( 
[zyh] varchar(20), 
[FeetypeCode] varchar(50), 
[FeetypeName] varchar(50), 
[Amount] decimal(38,2) 
)

insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,dlCode,dlmc ,0
  from NewtouchHIS_Base..xt_basfdl where OrganizeId=@orgId and zt=1 order by px


select @zyh zyh,FeetypeCode,FeetypeName,sum(Amount) Amount
  into #brfy
  from V_HIS_InpPatFeeForBA 
 where zyh=@zyh and OrganizeId=@orgId 
 group by FeetypeCode,FeetypeName

update zb set zb.Amount=fy.Amount
  from #temptable zb,#brfy fy
where zb.FeetypeCode=fy.FeetypeCode

/*Cập nhật phí điều trị không phẫu thuật*/
update #temptable set Amount=(select sum(Amount) from #temptable where FeetypeCode  in('LCZDXMF','fsszlxmf')) where FeetypeCode='fsszlxmf'
/*Cập nhật phí điều trị phẫu thuật*/
update #temptable set Amount=(select sum(Amount) from #temptable where FeetypeCode  in('sszlf','MAF','SSF')) where FeetypeCode='sszlf'
/*Cập nhật chi phí thuốc kháng khuẩn*/

/*Thông tin quyết toán*/
 select a.jsnm, a.zyh, a.zje,a.zyts,a.jsksrq,a.jsjsrq ,a.xjzf      
 into #js      
 from [NewtouchHIS_Sett].dbo.zy_js a with(nolock)       
 where a.OrganizeId=@orgId      
 and a.jszt=1 and a.zyh=@zyh      
 and not exists(select 1 from [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.jsnm=b.cxjsnm and jszt=2 and zt='1' ) 

declare @zyf money=(select sum(Amount) from #brfy where FeetypeCode not in('LCZDXMF','MAF','SSF','KJYWF'))
declare @ZFJE money=(select xjzf from #js)
select @ZFJE=isnull(@ZFJE,0)
select @zyf=isnull(@zyf,0)
insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,'ZFY','总费用',@zyf
insert into #temptable (zyh,FeetypeCode,FeetypeName,Amount)
select @zyh,'ZFJE','自付金额',@ZFJE

/*Chuyển dọc thành ngang*/
 select  @zyh zyh,   
  ZFY=sum(case when b.FeetypeCode='ZFY' then Amount else 0.00 end), --Phí dịch vụ y tế thông thường
   ZFJE=sum(case when b.FeetypeCode='ZFJE' then Amount else 0.00 end), --Phí dịch vụ y tế thông thường
 YLFUF=sum(case when b.FeetypeCode='YLFUF' then Amount else 0.00 end), --Phí dịch vụ y tế thông thường      
 ZLCZF=sum(case when b.FeetypeCode='ZLCZF' then Amount else 0.00 end), --Phí thao tác điều trị thông thường      
 HLF=sum(case when b.FeetypeCode='HLF' then Amount else 0.00 end), --Phí chăm sóc      
 QTFY=sum(case when b.FeetypeCode='QTFY' then Amount else 0.00 end), --1.(4) Chi phí khác      
 BLZDF=sum(case when b.FeetypeCode='BLZDF' then Amount else 0.00 end),--Phí chẩn đoán bệnh lý      
 SYSZDF=sum(case when b.FeetypeCode='SYSZDF' then Amount else 0.00 end),--Phí chẩn đoán xét nghiệm      
 YXXZDF=sum(case when b.FeetypeCode='YXXZDF' then Amount else 0.00 end), --Phí chẩn đoán hình ảnh      
 LCZDXMF=sum(case when b.FeetypeCode='LCZDXMF' then Amount else 0.00 end),--Phí hạng mục chẩn đoán lâm sàng      
 FSSZLXMF=sum(case when b.FeetypeCode='FSSZLXMF' then Amount else 0.00 end), --Phí hạng mục điều trị không phẫu thuật      
 WLZLF=sum(case when b.FeetypeCode='WLZLF' then Amount else 0.00 end),--3. Nhóm điều trị - (9) Phí hạng mục điều trị không phẫu thuật - (trong đó: phí điều trị vật lý lâm sàng      
 SSZLF=sum(case when b.FeetypeCode='SSZLF' then Amount else 0.00 end),--Phí điều trị phẫu thuật      
 MAF=sum(case when b.FeetypeCode='MAF' then Amount else 0.00 end),--(10) Phí điều trị phẫu thuật - phí gây mê      
 SSF=sum(case when b.FeetypeCode='SSF' then Amount else 0.00 end),--Phí phẫu thuật      
 KFF=sum(case when b.FeetypeCode='KFF' then Amount else 0.00 end),--(11) Phí phục hồi chức năng      
 ZYZLF=sum(case when b.FeetypeCode='ZYZLF' then Amount else 0.00 end),--Phí điều trị Đông y      
 XYF=sum(case when b.FeetypeCode='XYF' then Amount else 0.00 end),--Phí thuốc Tây y      
 KJYWF=sum(case when b.FeetypeCode='KJYWF' then Amount else 0.00 end),--(Trong đó: chi phí thuốc kháng khuẩn      
 ZCYF=sum(case when b.FeetypeCode='ZCYF' then Amount else 0.00 end),--(14) Phí thuốc thành phẩm Đông y      
 ZCYF1=sum(case when b.FeetypeCode='ZCYF1' then Amount else 0.00 end),--Phí thuốc thảo dược      
 XF=sum(case when b.FeetypeCode='XF' then Amount else 0.00 end),--Phí máu      
 BDBLZPF=sum(case when b.FeetypeCode='BDBLZPF' then Amount else 0.00 end),--Phí chế phẩm albumin      
 QDBLZPF=sum(case when b.FeetypeCode='QDBLZPF' then Amount else 0.00 end),--Phí chế phẩm globulin      
 NXYZLZPF=sum(case when b.FeetypeCode='NXYZLZPF' then Amount else 0.00 end),--Phí chế phẩm yếu tố đông máu      
 XBYZLZPF=sum(case when b.FeetypeCode='XBYZLZPF' then Amount else 0.00 end),--Phí chế phẩm cytokine      
 HCYYCLF=sum(case when b.FeetypeCode='HCYYCLF' then Amount else 0.00 end),--Phí vật tư y tế dùng một lần cho kiểm tra      
 YYCLF=sum(case when b.FeetypeCode='YYCLF' then Amount else 0.00 end),--Phí vật tư y tế dùng một lần cho điều trị      
 YCXYYCLF=sum(case when b.FeetypeCode='YCXYYCLF' then Amount else 0.00 end),--Phí vật tư y tế dùng một lần cho phẫu thuật      
 QTF=sum(case when b.FeetypeCode='QTF' then Amount else 0.00 end)
into #mrfy
 from #temptable b    


/*Cập nhật chi phí*/
update a      
  set a.ZFY=b.ZFY ,      
  a.ZFJE=b.ZFJE ,    
  a.QTZF = b.ZFY - b.ZFJE,        
  a.YLFUF=b.YLFUF, --Phí dịch vụ y tế thông thường      
  a.ZLCZF=b.ZLCZF, --Phí thao tác điều trị thông thường      
  a.HLF=b.HLF, --Phí chăm sóc      
  a.QTFY=b.QTFY, --1.(4) Chi phí khác      
  a.BLZDF=b.BLZDF,--Phí chẩn đoán bệnh lý      
  a.SYSZDF=b.SYSZDF,--Phí chẩn đoán xét nghiệm      
  a.YXXZDF=b.YXXZDF, --Phí chẩn đoán hình ảnh      
  a.LCZDXMF=b.LCZDXMF,--Phí hạng mục chẩn đoán lâm sàng      
  a.FSSZLXMF=b.FSSZLXMF, --Phí hạng mục điều trị không phẫu thuật      
  a.WLZLF=b.WLZLF,--3. Nhóm điều trị - (9) Phí hạng mục điều trị không phẫu thuật - (trong đó: phí điều trị vật lý lâm sàng      
  a.SSZLF=b.SSZLF,--Phí điều trị phẫu thuật      
  a.MAF=b.MAF,--(10) Phí điều trị phẫu thuật - phí gây mê      
  a.SSF=b.SSF,--Phí phẫu thuật      
  a.KFF=b.KFF,--(11) Phí phục hồi chức năng      
  a.ZYZLF=b.ZYZLF,--Phí điều trị Đông y      
  a.XYF=b.XYF,--Phí thuốc Tây y      
  a.KJYWF=b.KJYWF,--(Trong đó: chi phí thuốc kháng khuẩn      
  a.ZCYF=b.ZCYF,--(14) Phí thuốc thành phẩm Đông y      
  a.ZCYF1=b.ZCYF1,--Phí thuốc thảo dược      
  a.XF=b.XF,--Phí máu      
  a.BDBLZPF=b.BDBLZPF,--Phí chế phẩm albumin      
  a.QDBLZPF=b.QDBLZPF,--Phí chế phẩm globulin      
  a.NXYZLZPF=b.NXYZLZPF,--Phí chế phẩm yếu tố đông máu      
  a.XBYZLZPF=b.XBYZLZPF,--Phí chế phẩm cytokine      
  a.HCYYCLF=b.HCYYCLF,--Phí vật tư y tế dùng một lần cho kiểm tra      
  a.YYCLF=b.YYCLF,--Phí vật tư y tế dùng một lần cho điều trị      
  a.YCXYYCLF=b.YCXYYCLF,--Phí vật tư y tế dùng một lần cho phẫu thuật      
  a.QTF=b.QTF      
   --,a.LastModifierCode='admin',a.LastModifyTime=getdate()      
  from mr_basy a,#mrfy b          
  where a.zyh=b.zyh and a.zt='1'
    and a.OrganizeId=@orgId

select * from #temptable  
DROP TABLE #temptable,#brfy,#js,#mrfy
return

