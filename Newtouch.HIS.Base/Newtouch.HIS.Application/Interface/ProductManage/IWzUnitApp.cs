using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Đơn vị vật tư
    /// </summary>
    public interface IWzUnitApp
    {
        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(WzUnitEntity wzUnitEntity, string keyWord);
    }
}
