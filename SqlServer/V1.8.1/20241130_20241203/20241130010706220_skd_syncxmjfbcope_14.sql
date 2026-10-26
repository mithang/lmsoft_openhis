USE [NewtouchHIS_Sett]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/30/2024 01:07:06







/*
declare @newcount int
exec skd_syncxmjfbcope '6d5752a7-234a-403e-aa1c-df8b45d3469f','2023-03-27 15:29:00', '03199'
select @newcount
select * from zy_xmjfb where zyh='03199' order by createtime desc
select * from [Newtouch_CIS]..zy_fymxk where  zyh='03199' order by createtime desc
*/
ALTER PROCEDURE [dbo].[skd_syncxmjfbcope]  
@orgId VARCHAR(50),
@lqrq datetime,	--Ngày lấy dữ liệu (kèm giờ phút giây)	Sẽ lấy các bản ghi trong vòng 30 ngày từ @lqrq - day1 đến @lqrq
@zyh varchar(max)
AS
begin
	declare @jfCount int = 0	--Tổng số bản ghi được chèn
	declare @zzfbz int --Cờ chuyển sang tự chi
	select zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,sl,CreateTime,zxksdm,Id,CreatorCode,WardCode,OrganizeId,yzxh,@zzfbz zzfbz,yzxz
	into #zy_xm
	from [Newtouch_CIS]..zy_fymxk xmfymx with(nolock)
	where 1=2

	select zyh,id,zzfbz,OrganizeId,zt into #yztab from Newtouch_CIS..zy_cqyz  where 1=2
    insert into #yztab
		select zyh,Id,zzfbz,OrganizeId,zt from (
		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_cqyz
		where zt='1'  and OrganizeId=@orgId and createtime  between DateAdd(day, -120, @lqrq) and @lqrq
		and zyh in (select * from f_split(@zyh,','))
		union all

		select zyh,id,zzfbz,OrganizeId,zt from Newtouch_CIS..zy_lsyz
		where zt='1'  and OrganizeId=@orgId and createtime between DateAdd(day, -120, @lqrq) and @lqrq
		and zyh in (select * from f_split(@zyh,','))
		) yztable

		insert into #zy_xm
		select a.zyh,zxrq,xmdm,ysgh,DeptCode,cwdm,dj,isnull(sl,0)sl,CreateTime,zxksdm,
		a.Id,CreatorCode,WardCode,a.OrganizeId,yzxh,isnull(zzfbz,0) zzfbz,a.yzxz
		from [Newtouch_CIS]..zy_fymxk a with(nolock)
		left join #yztab yzxx on yzxx.id=a.yzxh and yzxx.zyh=a.zyh and yzxx.OrganizeId=a.OrganizeId and yzxx.zt='1'
		where a.OrganizeId=@orgId --and a.zyh=@zyh
		and a.createtime between DateAdd(day, -120, @lqrq) and @lqrq
		and a.yzlb in('-1','1')
		and a.zyh in (select * from f_split(@zyh,','))
		--and isnull(a.sl,0) > 0
		and a.zt=1 and isnull(a.isjf,1)<>'0'
		and not exists(select 1 from zy_xmjfb b with(nolock) 
					where a.OrganizeId=b.OrganizeId and a.zyh=b.zyh and a.id=b.bdzxid)
		and exists(select 1 from zy_brjbxx c with(nolock) where a.OrganizeId=c.OrganizeId and a.zyh=c.zyh and 
					c.zybz in (1,2,7) and c.zt=1)	--Trạng thái đang ở khu bệnh, ra khỏi khu bệnh, chuyển khu
	--2021-9-16 chl: Xét nghiệm tính phí riêng
	select * into #sync_xm from #zy_xm where isnull(yzxz,0)<>1
	if(exists(select 1 from #zy_xm where yzxz=1))
	begin
		insert into #sync_xm
		select *
		from #zy_xm a 
		where a.yzxz=1 
		and exists(select 1 from Newtouch_CIS.dbo.zy_lsyz b with(nolock) 
			where a.yzxh=b.id --and b.yzlx<>6
			)
		
		--print('lis已接收同步')
		--insert into #sync_xm
		--select *
		--from #zy_xm a 
		--where a.yzxz=1 
		--and exists(select 1 from [Newtouch_CIS].dbo.zy_lsyz b with(nolock) 
		--	where a.yzxh=b.id --and b.yzlx=6
		--	and exists(select 1
		--		from Newtouch_Interface.dbo.lis_confirm_zy c with(nolock)
		--		where b.OrganizeId=c.OrganizeId and b.zyh=c.zyh and b.yzh=c.sqdh
		--		 and c.createtime> b.createtime
		--		and c.qrzt=1 and c.zt<>9 and c.sqdh>'' ))  --Đã nhận, tính phí
	end


	if(exists(select 1 from #sync_xm))
	begin
		select @jfCount = count(1) from #sync_xm

		declare @startjfbId int = 0	--Id bắt đầu của bản ghi được chèn
		SELECT @startjfbId = CurrentSerialNo FROM [NewtouchHIS_Sett].[dbo].[EntitySerialNo] where EntityName = 'zy_xmjfb'
		--Id bị chiếm, phải cộng thêm 1
		if(@startjfbId > 0)
			update EntitySerialNo set CurrentSerialNo = CurrentSerialNo + @jfCount + 1 where EntityName = 'zy_xmjfb'
		else
			insert into EntitySerialNo(EntityName,SerialNoMin,SerialNoMax,CurrentSerialNo)
			values('zy_xmjfb', 1, 999999999, @jfCount + 1)

		--Thực hiện chèn
		insert into zy_xmjfb(jfbbh, OrganizeId, zyh, tdrq, sfxm, dl, ys, ks, cw, dj, sl, jfdw
		,zfbl,zfxz,ssbz,ssry,ssrq,zxks,yzxz,yzzt,cxzyjfbbh,bdzxId,CreatorCode,CreateTime,bq,zt,ysmc,ksmc,yzwym,zzfbz)
		select @startjfbId + ROW_NUMBER() over(order by xmfymx.CreateTime) as jfbbh, @orgId OrganizeId
		, xmfymx.zyh, CONVERT(varchar(100), xmfymx.zxrq, 121) tdrq, xmdm sfxm, sfdl.dlCode dl
		, isnull(xmfymx.ysgh, '') ys, isnull(xmfymx.DeptCode, '') ks, xmfymx.cwdm cw
		, xmfymx.dj, xmfymx.sl, sfxm.dw jfdw
		, sfxm.zfbl, sfxm.zfxz, 1 ssbz, '' ssry, xmfymx.CreateTime ssrq,xmfymx.zxksdm
		, '2' yzxz, '1' yzzt, 0 cxzyjfbbh
		, xmfymx.Id bdzxId, xmfymx.CreatorCode, getdate() CreateTime
		, xmfymx.WardCode bq, '1' zt, '' ysmc, '' ksmc,yzxh,zzfbz
		--Giống nhau
		from #sync_xm xmfymx,[NewtouchHIS_Base]..V_S_xt_sfxm sfxm,[NewtouchHIS_Base]..V_S_xt_sfdl sfdl
		where sfxm.sfxmCode = xmfymx.xmdm and sfxm.Organizeid = xmfymx.OrganizeId and sfxm.zt = '1'
		and sfdl.dlCode = sfxm.sfdlCode and sfdl.Organizeid = xmfymx.OrganizeId and sfdl.zt = '1'
	end
	drop table #zy_xm
	drop table #sync_xm
end







