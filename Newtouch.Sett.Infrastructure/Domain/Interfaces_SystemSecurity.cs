using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true: Newtouch.Application/Interface/SystemSecurity/IDbBackupApp.cs
    public interface IDbBackupApp
    {
        void DeleteForm(string keyValue);
        DbBackupEntity GetForm(string keyValue);
        List<DbBackupEntity> GetList(string queryJson);
        void SubmitForm(DbBackupEntity dbBackupEntity);
    }

    public interface IFilterIPApp
    {
        object DeleteForm(object p0);
        IFilterIPApp_GetFormDto GetForm(object p0);
        IFilterIPApp_GetListDto GetList(object p0);
        object SubmitForm(object p0, object p1);
    }

    public interface ILogApp
    {
        ILogApp_GetListDto GetList(object p0, object p1);
        object RemoveLog(object p0);
    }
}
