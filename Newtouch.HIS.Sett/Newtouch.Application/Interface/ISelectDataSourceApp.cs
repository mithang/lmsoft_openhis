using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// (dư ới 拉)chọn dữ liệu源
    /// </summary>
    public interface ISelectDataSourceApp
    {
        string getEffectPatiNatureList();
    }
}
