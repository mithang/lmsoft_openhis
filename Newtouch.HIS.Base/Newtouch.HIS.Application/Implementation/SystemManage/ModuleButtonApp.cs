using System.Collections.Generic;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation.SystemManage
{
    /// <summary>
    /// App nút bấm
    /// </summary>
    public class ModuleButtonApp : IModuleButtonApp
    {
        private readonly ISysModuleButtonRepo _moduleButtonRepository;

        public ModuleButtonApp(ISysModuleButtonRepo moduleButtonRepository)
        {
            this._moduleButtonRepository = moduleButtonRepository;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModuleButtonEntity GetForm(string keyValue)
        {
            return _moduleButtonRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// Xóa thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _moduleButtonRepository.Delete(t => t.Id == keyValue);
        }

    }
}
