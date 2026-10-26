USE [Newtouch_CIS]
GO

-- Loại sự kiện: ALTER_PROCEDURE
-- Thời gian thay đổi: 11/10/2024 22:56:50


/*
Lý do sửa: Bệnh viện phản ánh phí giường khi vào/ra khu bị tính trùng lặp
Cách sửa: Sau khi xác minh, chủ yếu là do ngày tính phí của hạng mục tính phí lăn bị đặt lại khi ra khu nên không đúng
Người sửa: Chu Tuấn
Ngày sửa: 2022-09-13
Cờ sửa đổi: 20220913 
*/    
  
ALTER proc [dbo].[usp_zy_PatOutArea]   
 @zyh varchar(20),  
 @OrganizeId varchar(50),  
 @rq DATETIME,  
 @rygh varchar(50),  
 @outMsg VARCHAR(300)=NULL OUTPUT  
as  
begin  
  
SELECT 1 FROM dbo.zy_brxxk WHERE zyh=@zyh AND OrganizeId=@OrganizeId AND zybz =1  
IF @@rowcount = 0   
        BEGIN    
            SELECT  @outMsg = 'F|在院患者中找不到该患者！'    
            RETURN    
        END  
SELECT 1 FROM dbo.zy_cqyz WHERE zyh = @zyh AND OrganizeId=@OrganizeId AND zt=1 AND yzzt IN (0,1,2)  
IF @@rowcount > 0   
        BEGIN    
            SELECT  @outMsg = 'F|该病人存在长期医嘱未执行情况，请先停止！'    
            RETURN    
        END  
SELECT 1 FROM dbo.zy_lsyz WHERE zyh = @zyh AND OrganizeId=@OrganizeId AND zt=1 AND yzzt IN (0,1)  
IF @@rowcount > 0   
        BEGIN    
            SELECT  @outMsg = 'F|该病人存在临时医嘱未执行情况，请先执行或作废！'    
            RETURN    
        END  
--select 1 from [dbo].[v_yzisfy] where zyh=@zyh AND OrganizeId=@OrganizeId and (tybz='2' and fybz='2') and zt='1'--Đã phát thuốc nhưng chưa trả thuốc  
--IF @@rowcount > 0   
--        BEGIN    
--            SELECT  @outMsg = 'F|该病人存在已发药未退药的，请先退药！'    
--            RETURN    
--        END  
--select 1 from [dbo].[v_yzisfy] where zyh=@zyh AND OrganizeId=@OrganizeId and  fybz='1' and zt='1'--Chưa phát thuốc  
--IF @@rowcount > 0   
--        BEGIN    
--            SELECT  @outMsg = 'F|该病人存在未发药的，请先发药！'    
--            RETURN    
--        END  

DECLARE @tfsl NUMERIC(10,1)=0,@gdgdrq DATE  
/*SELECT @gdgdrq=ISNULL(gdxmzxrq,CONVERT(DATE,CONVERT(nvarchar(10),GETDATE(),120))) FROM dbo.zy_brxxk WHERE zyh=@zyh AND zt='1'  */
/*20220913*/

update zy_fymxk 
   set zt = '0' 
 where yzxh = '999999999999' 
   and zyh = @zyh
   and sl = 0
   and OrganizeId=@OrganizeId
   AND zt='1'

select top 1 @gdgdrq = ISNULL(zxrq,CONVERT(DATE,CONVERT(nvarchar(10),GETDATE(),120))) 
  from zy_fymxk 
 where yzxh = '999999999999' 
   and zyh = @zyh
   and sl <> 0
   and OrganizeId=@OrganizeId
   AND zt='1'
order by zxrq desc

SET @rq = CONVERT(DATETIME,@rq,120)  
IF SUBSTRING(CONVERT(VARCHAR(20),CONVERT(VARCHAR(30),CONVERT(DATETIME,@rq) ,20)),12,2)>'12'  
 BEGIN  
 SELECT @tfsl=Value FROM dbo.Sys_Config WHERE Code='PmOutAreaBedFee' AND OrganizeId=@OrganizeId AND zt='1'  
 END  
 ELSE  
 BEGIN  
 SELECT @tfsl=Value FROM dbo.Sys_Config WHERE Code='AmOutAreaBedFee' AND OrganizeId=@OrganizeId AND zt='1'  
 END  

if(DATEDIFF(day,@gdgdrq,DATEADD(DAY,0,@rq))<>1)  
begin  
  SELECT @tfsl=(@tfsl+DATEDIFF(day,@gdgdrq,DATEADD(DAY,0,@rq))-1)  
end  
 IF  @tfsl > 0  --@tfsl <> 0   ngày 12/08/2024 Chèn bản ghi âm để gắn hạng mục, đồng bộ sang bảng tính phí; dữ liệu bản ghi dương tương ứng khi tải lên bảo hiểm y tế chưa rõ ràng
 BEGIN  
 EXEC usp_zy_BedItemsFeeByZyh @zyh,@OrganizeId,@rygh,@tfsl,@outMsg  
 END  

 IF @outMsg LIKE 'F%'  
 RETURN  
SELECT  @outMsg = 'T|可正常出区！'  
RETURN     
end  
  
  

