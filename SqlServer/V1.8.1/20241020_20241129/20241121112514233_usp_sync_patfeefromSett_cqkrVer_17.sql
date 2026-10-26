USE [Newtouch_EMR]
GO

-- Loại sự kiện: CREATE_PROCEDURE
-- Thời gian thay đổi: 11/21/2024 11:25:14






/*
Tác giả: chl
Thời gian tạo: 2021-7-29
Mô tả: Chi phí trang đầu hồ sơ bệnh án, đồng bộ từ quyết toán Sett, Trùng Khánh Khoan Nhân, hệ thống hồ sơ bệnh án - bảo trì chi phí hồ sơ bệnh án, phiên bản
exec [usp_sync_patfeefromSett] '6d5752a7-234a-403e-aa1c-df8b45d3469f' ,'00137'
*/
CREATE proc [dbo].[usp_sync_patfeefromSett_cqkrVer]
	@orgId varchar(50),
	@zyh varchar(20)
as
begin
	select a.jsnm, a.zyh, a.zje,a.zyts,a.jsksrq,a.jsjsrq ,a.xjzf
	into #js
	from [NewtouchHIS_Sett].dbo.zy_js a with(nolock) 
	where a.OrganizeId=@orgId
	and a.jszt=1 and a.zyh=@zyh and a.zt=1
	and not exists(select 1 from [NewtouchHIS_Sett].dbo.zy_js b with(nolock) where a.jsnm=b.cxjsnm and jszt=2 and zt='1' )
	--and exists(select 1 from Newtouch_EMR.dbo.mr_basy c with(nolock) where a.zyh=c.zyh and c.zt='1')

	select jsnm,yzlx,d.code,sum(jyje) je,--d.shortcode,b.sfxm,c.sfxmmc	
	isnull(d.ShortCode,'QTF')FeetypeCode,
	isnull(d.Name,'其他费') FeetypeName
	into #jsmx
	--select *
	from [NewtouchHIS_Sett].dbo.zy_jsmx a with(nolock)
	left join [NewtouchHIS_Sett].dbo.zy_xmjfb b with(nolock) on a.xmjfbbh=b.jfbbh 
	left join [NewtouchHIS_Base]..xt_sfxm g on b.sfxm=g.sfxmcode and b.OrganizeId=g.OrganizeId and g.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_sfxmrel c with(nolock) on b.sfxm=c.sfxm and g.sfxmmc=c.sfxmmc and b.OrganizeId=c.OrganizeId and c.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_bafeetype d with(nolock) on c.feetypecode=d.code and c.OrganizeId=d.OrganizeId and c.zt='1'
	where exists(select 1 from #js b where a.jsnm=b.jsnm )
	and a.zt='1' and b.zt='1'
	and xmjfbbh>0 
	group by jsnm,yzlx,d.code,d.ShortCode,d.Name--,c.sfxm,c.sfxmmc

	insert into #jsmx
	select  jsnm,yzlx,d.code,sum(jyje) je,--d.shortcode,b.sfxm,c.sfxmmc	
	isnull(d.ShortCode,'QTF')FeetypeCode,
	isnull(d.Name,'其他费') FeetypeName
	--select b.*
	from [NewtouchHIS_Sett].dbo.zy_jsmx a with(nolock)
	left join [NewtouchHIS_Sett].dbo.zy_ypjfb b with(nolock) on a.ypjfbbh=b.jfbbh and b.zt='1'
	left join [NewtouchHIS_Base]..xt_yp g on b.yp=g.ypcode and g.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_sfxmrel c with(nolock) on b.yp=c.sfxm and g.ypmc=c.sfxmmc and b.OrganizeId=c.OrganizeId and c.zt='1'
	left join Newtouch_MRMS.dbo.mr_dic_bafeetype d with(nolock) on c.feetypecode=d.code and c.OrganizeId=d.OrganizeId and d.zt='1'
	where exists(select 1 from #js b where a.jsnm=b.jsnm )
	and a.zt='1' and b.zt='1'
	and ypjfbbh>0 
	group by jsnm,yzlx,d.code,d.ShortCode,d.Name

	select a.zyh,FeetypeCode,FeetypeName,sum(b.je) Amount
	from #js a,#jsmx b 
	where a.jsnm=b.jsnm
	group by a.zyh,FeetypeCode,FeetypeName
	union all
	select zyh,'ZFY','总费用',sum(zje) zje
	from #js group by zyh
	union all
	select zyh,'ZFJE','自付金额',sum(xjzf) xjzf
	from #js group by zyh

	select * into #newjsmx from  (
	select a.zyh,FeetypeCode,FeetypeName,sum(b.je) Amount
	from #js a,#jsmx b 
	where a.jsnm=b.jsnm
	group by a.zyh,FeetypeCode,FeetypeName
	union all
	select zyh,'ZFY','总费用',sum(zje) zje
	from #js group by zyh
	union all
	select zyh,'ZFJE','自付金额',sum(xjzf) xjzf
	from #js group by zyh)a

	if(not exists(select 1 from Newtouch_EMR.dbo.mr_basy with(nolock) where zyh=@zyh and OrganizeId=@orgId and zt='1'))
	begin
		return;
	end
	declare @maxjsnm int;
    declare @zyhs varchar(20);
    select @maxjsnm=max(jsnm),@zyhs=max(zyh) from #js

	select  @zyh zyh,@maxjsnm maxjsnm,
    XJZF=sum(case when b.FeetypeCode='ZFJE' then Amount else 0.00 end),
    ZJE=sum(case when b.FeetypeCode='ZFY' then Amount else 0.00 end),
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
	into #fee
	--select *
	from  #newjsmx b
	group by zyh
	--select  a.zyh,max(a.jsnm) maxjsnm,max(a.xjzf)xjzf,max(a.zje)zje,
	--YLFUF=sum(case when b.FeetypeCode='YLFUF' then je else 0.00 end), --Phí dịch vụ y tế thông thường
	--ZLCZF=sum(case when b.FeetypeCode='ZLCZF' then je else 0.00 end), --Phí thao tác điều trị thông thường
	--HLF=sum(case when b.FeetypeCode='HLF' then je else 0.00 end), --Phí chăm sóc
	--QTFY=sum(case when b.FeetypeCode='QTFY' then je else 0.00 end), --1.(4) Chi phí khác
	--BLZDF=sum(case when b.FeetypeCode='BLZDF' then je else 0.00 end),--Phí chẩn đoán bệnh lý
	--SYSZDF=sum(case when b.FeetypeCode='SYSZDF' then je else 0.00 end),--Phí chẩn đoán xét nghiệm
	--YXXZDF=sum(case when b.FeetypeCode='YXXZDF' then je else 0.00 end), --Phí chẩn đoán hình ảnh
	--LCZDXMF=sum(case when b.FeetypeCode='LCZDXMF' then je else 0.00 end),--Phí hạng mục chẩn đoán lâm sàng
	--FSSZLXMF=sum(case when b.FeetypeCode='FSSZLXMF' then je else 0.00 end), --Phí hạng mục điều trị không phẫu thuật
	--WLZLF=sum(case when b.FeetypeCode='WLZLF' then je else 0.00 end),--3. Nhóm điều trị - (9) Phí hạng mục điều trị không phẫu thuật - (trong đó: phí điều trị vật lý lâm sàng
	--SSZLF=sum(case when b.FeetypeCode='SSZLF' then je else 0.00 end),--Phí điều trị phẫu thuật
	--MAF=sum(case when b.FeetypeCode='MAF' then je else 0.00 end),--(10) Phí điều trị phẫu thuật - phí gây mê
	--SSF=sum(case when b.FeetypeCode='SSF' then je else 0.00 end),--Phí phẫu thuật
	--KFF=sum(case when b.FeetypeCode='KFF' then je else 0.00 end),--(11) Phí phục hồi chức năng
	--ZYZLF=sum(case when b.FeetypeCode='ZYZLF' then je else 0.00 end),--Phí điều trị Đông y
	--XYF=sum(case when b.FeetypeCode='XYF' then je else 0.00 end),--Phí thuốc Tây y
	--KJYWF=sum(case when b.FeetypeCode='KJYWF' then je else 0.00 end),--(Trong đó: chi phí thuốc kháng khuẩn
	--ZCYF=sum(case when b.FeetypeCode='ZCYF' then je else 0.00 end),--(14) Phí thuốc thành phẩm Đông y
	--ZCYF1=sum(case when b.FeetypeCode='ZCYF1' then je else 0.00 end),--Phí thuốc thảo dược
	--XF=sum(case when b.FeetypeCode='XF' then je else 0.00 end),--Phí máu
	--BDBLZPF=sum(case when b.FeetypeCode='BDBLZPF' then je else 0.00 end),--Phí chế phẩm albumin
	--QDBLZPF=sum(case when b.FeetypeCode='QDBLZPF' then je else 0.00 end),--Phí chế phẩm globulin
	--NXYZLZPF=sum(case when b.FeetypeCode='NXYZLZPF' then je else 0.00 end),--Phí chế phẩm yếu tố đông máu
	--XBYZLZPF=sum(case when b.FeetypeCode='XBYZLZPF' then je else 0.00 end),--Phí chế phẩm cytokine
	--HCYYCLF=sum(case when b.FeetypeCode='HCYYCLF' then je else 0.00 end),--Phí vật tư y tế dùng một lần cho kiểm tra
	--YYCLF=sum(case when b.FeetypeCode='YYCLF' then je else 0.00 end),--Phí vật tư y tế dùng một lần cho điều trị
	--YCXYYCLF=sum(case when b.FeetypeCode='YCXYYCLF' then je else 0.00 end),--Phí vật tư y tế dùng một lần cho phẫu thuật
	--QTF=sum(case when b.FeetypeCode='QTF' then je else 0.00 end)
	--into #fee
	--from #js a
	--left join #jsmx b on a.jsnm=b.jsnm
	--group by a.zyh--,FeetypeCode,FeetypeName

	if(exists(select 1 from #fee))
	begin
		--select * from #fee

		begin try
			--begin tran
		print '更新明细费'
		update a
		set	a.ZFY=b.zje ,  a.ZFJE=b.xjzf ,
		a.SJZYTS=(case when isnull(a.SJZYTS,'')='' 
				then convert(varchar(10),c.zyts) else a.SJZYTS end),
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
		from mr_basy a,#fee b
		left join #js c on b.maxjsnm=c.jsnm 
		where a.zyh=b.zyh and a.zt='1'

		end try
		begin catch
			print(@@error)
		end catch

	end
	else
	begin
		print '无可更新费用'
	end

	drop table #newjsmx
	drop table #js
	drop table #jsmx
	drop table #fee

end




