using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class SysMedicineStorageIOReceiptDetailVO: SysMedicineStorageIOReceiptDetailEntity
    {
        /// <summary>
        /// tồn kho Id
        /// </summary>
        public string kcId { get; set; }
    }
}
