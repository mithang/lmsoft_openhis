-- DB translation Scope A: NewtouchHIS_Base.Msg_NoticeGroup.DefaultContentData (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [Msg_NoticeGroup] SET [DefaultContentData] = 'Ten bac si, Ten benh nhan' WHERE [DefaultContentData] = N'医生姓名,患者姓名';
