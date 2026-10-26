namespace Newtouch.HIS.Application
{
    public interface ISysStaffApp
    {

        /// <summary>
        /// Hiệu lực viên nhân viên tự đ ộng nhắc nhở
        /// </summary>
        /// <returns></returns>
        object GetValidStaffList();

        /// <summary>
        /// theo Account(mã nhân viên)lấy nhân sự số hiệu
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int GetSysRybhByAccont(string account);

    }
}
