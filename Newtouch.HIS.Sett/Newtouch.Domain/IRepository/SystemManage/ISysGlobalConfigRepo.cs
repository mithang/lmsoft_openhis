namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysGloablConfigRepo
    {
        /// <summary>
        /// TheoCodeLấy cấu hìnhValue
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetValueByCode(string code);

        /// <summary>
        /// TheoCodeLấy cấu hìnhValue
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetIntValueByCode(string code, int defaultValue = 0);

        /// <summary>
        /// TheoCodeLấy cấu hìnhValue
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool? GetBoolValueByCode(string code, bool? defaultValue = null);
    }

}
