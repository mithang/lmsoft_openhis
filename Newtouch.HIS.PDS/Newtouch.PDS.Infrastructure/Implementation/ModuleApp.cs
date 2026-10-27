using Newtouch.Infrastructure.EF;
using Newtouch.Tools;
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
    /// Thực đơnApp
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
            return _moduleRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// Xóa thực đơn
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            if (_moduleRepository.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("Xóa thất bại！Đối tượng thao tác chứa dữ liệu cấp dưới。");
            }
            else
            {
                _moduleRepository.Delete(t => t.Id == keyValue);
            }
        }

    }
}
