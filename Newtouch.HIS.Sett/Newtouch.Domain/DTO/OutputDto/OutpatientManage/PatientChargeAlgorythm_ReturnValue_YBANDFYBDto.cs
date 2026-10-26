
namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    public class PatientChargeAlgorythm_ReturnValue_YBANDFYBDto
    {
        public PatientChargeAlgorythm_ReturnValue_YBANDFYBDto()
        {
            YB = new PatientChargeAlgorithm_ReturnValueDto();
            FYB = new PatientChargeAlgorithm_ReturnValueDto();
        }
        /// <summary>
        /// Cần giao dịch BHYT
        /// </summary>
        public PatientChargeAlgorithm_ReturnValueDto YB { get; set; }

        /// <summary>
        /// Không cần giao dịch BHYT
        /// </summary>
        public PatientChargeAlgorithm_ReturnValueDto FYB { get; set; }

        /// <summary>
        /// Tổng chi phí trong phạm vi quyết toán BHYT
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                return YB.ybjsfwze;
            }
        }
    }
}
