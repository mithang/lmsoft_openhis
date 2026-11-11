using Newtouch.Infrastructure.EF;
using Newtouch.Tools;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.Entity;

using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Thực đơn App
    /// </summary>
    public class ModuleApp : AppBase, IModuleApp
    {
        private readonly ISysModuleRepo _moduleRepository;

        /// <summary>
        /// Lấy danh sách thực đơn hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysModuleEntity> GetValidList()
        {
            return _moduleRepository.GetValidList().OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModuleEntity GetForm(string keyValue)
        {
            return _moduleRepository.FindEntity(p => p.Id == keyValue);
        }

        /// <summary>
        /// Xóa thực đơn
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            // G37 F37-4: (a) module phai ton tai — keyValue bogus -> loi TV
            // thay vi Success 0 dong; (b) ParentId NULL o 12 dong goc ->
            // Equals NRE (IQueryable materialize toan bang, predicate chay
            // in-memory) — check null truoc. ParentId la varchar(50)
            // (khong phai char(n)) nen Equals in-memory la chinh xac.
            if (_moduleRepository.FindEntity(p => p.Id == keyValue) == null)
            {
                throw new FailedException("Không tìm thấy thực đơn cần xóa");
            }
            if (_moduleRepository.IQueryable().Count(t => t.ParentId != null && t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("Xóa thất bại! Đối tượng thao tác chứa dữ liệu cấp dưới.");
            }
            else
            {
                _moduleRepository.Delete(t => t.Id == keyValue);
            }
        }

    }
}
