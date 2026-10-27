-- DB translation Scope A: NewtouchHIS_Base.t_haocai.bz (applied live; re-runnable; keyless targets match N'old' values)
USE [NewtouchHIS_Base];
UPDATE [t_haocai] SET [bz] = 'So hieu chung tu dang ky' WHERE [bz] = N'备案凭证编号';
UPDATE [t_haocai] SET [bz] = 'So dang ky san pham' WHERE [bz] = N'产品备案号';
UPDATE [t_haocai] SET [bz] = 'So chung tu dang ky san pham' WHERE [bz] = N'产品备案凭证号';
UPDATE [t_haocai] SET [bz] = 'Tieu chuan san pham' WHERE [bz] = N'产品标准';
UPDATE [t_haocai] SET [bz] = 'So dang ky san pham' WHERE [bz] = N'产品注册号';
UPDATE [t_haocai] SET [bz] = 'So dang ky thiet bi y te' WHERE [bz] = N'医疗器械备案号';
UPDATE [t_haocai] SET [bz] = 'So hieu chung tu dang ky thiet bi y te' WHERE [bz] = N'医疗器械备案凭证编号';
UPDATE [t_haocai] SET [bz] = 'Tieu chuan thuc hien' WHERE [bz] = N'执行标准';
UPDATE [t_haocai] SET [bz] = 'Chi co so giay phep' WHERE [bz] = N'只有许可证号';
UPDATE [t_haocai] SET [bz] = 'So giay chung nhan dang ky' WHERE [bz] = N'注册证号';
