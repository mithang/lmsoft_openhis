-- DB translation Scope A: NewtouchHIS_Base.Msg_NoticeGroup.GroupDesc (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [Msg_NoticeGroup] SET [GroupDesc] = 'Nhac nho van ban kiem soat' WHERE [GroupDesc] = N'质控文书提醒';
UPDATE [Msg_NoticeGroup] SET [GroupDesc] = 'Chuyen dung van ban benh an he thong kiem soat' WHERE [GroupDesc] = N'质控系统病历文书专属';
UPDATE [Msg_NoticeGroup] SET [GroupDesc] = 'Chuyen dung he thong kiem soat' WHERE [GroupDesc] = N'质控系统专属';
