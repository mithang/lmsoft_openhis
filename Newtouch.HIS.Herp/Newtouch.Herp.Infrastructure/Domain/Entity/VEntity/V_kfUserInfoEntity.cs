namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin người dùng kho
    /// </summary>
    public class VKfUserInfoEntity
    {

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Kho ID
        /// </summary>
        public string kfId { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        public string kfName { get; set; }

        /// <summary>
        /// Cấp kho
        /// </summary>
        public int kfLeve { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string dutyName { get; set; }
    }
}
