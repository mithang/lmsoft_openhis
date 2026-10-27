-- DB translation Scope A: NewtouchHIS_Base.Msg_NoticeGroup.GroupName (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [Msg_NoticeGroup] SET [GroupName] = 'Tin nhan van ban benh an' WHERE [GroupName] = N'病历文书消息';
UPDATE [Msg_NoticeGroup] SET [GroupName] = 'Tin nhan to kiem soat van ban benh an' WHERE [GroupName] = N'病历文书质控组消息';
UPDATE [Msg_NoticeGroup] SET [GroupName] = 'Tin nhan to kiem soat benh an' WHERE [GroupName] = N'病历质控组消息';
