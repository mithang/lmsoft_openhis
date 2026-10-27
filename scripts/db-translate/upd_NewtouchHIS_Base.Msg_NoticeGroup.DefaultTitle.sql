-- DB translation Scope A: NewtouchHIS_Base.Msg_NoticeGroup.DefaultTitle (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [Msg_NoticeGroup] SET [DefaultTitle] = 'Ban co tin nhan kiem soat benh an moi can xem' WHERE [DefaultTitle] = N'您有新的病历质控消息待查收';
UPDATE [Msg_NoticeGroup] SET [DefaultTitle] = 'Ban co tin nhan kiem soat moi can xem' WHERE [DefaultTitle] = N'您有新的质控消息待查收';
