namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Duyệt chứng từ
    /// </summary>
    public class BillApprovalDTO
    {
        /// <summary>
        /// Chứng từ ID
        /// </summary>
        public long djId { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
