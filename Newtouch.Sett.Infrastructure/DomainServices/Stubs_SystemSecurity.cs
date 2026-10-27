using Newtouch.Sett.Domain;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.DomainServices
{
    public class DbBackupApp : IDbBackupApp
    {
        public void DeleteForm(string keyValue)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public DbBackupEntity GetForm(string keyValue)
        {
            return null;
        }

        public List<DbBackupEntity> GetList(string queryJson)
        {
            return new List<DbBackupEntity>();
        }

        public void SubmitForm(DbBackupEntity dbBackupEntity)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class FilterIPApp : IFilterIPApp
    {
        public object DeleteForm(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

        public IFilterIPApp_GetFormDto GetForm(object p0)
        {
            return new IFilterIPApp_GetFormDto();
        }

        public IFilterIPApp_GetListDto GetList(object p0)
        {
            return new IFilterIPApp_GetListDto();
        }

        public object SubmitForm(object p0, object p1)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }

    public class LogApp : ILogApp
    {
        public ILogApp_GetListDto GetList(object p0, object p1)
        {
            return new ILogApp_GetListDto();
        }

        public object RemoveLog(object p0)
        {
            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (chỉ xác minh đọc).");
        }

    }
}
