/*******************************************************************************
 * Copyright © 2016 Newtouch Bảo lưu bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IMIDiagnosisAndTreatmentItemRepo : IRepositoryBase<MIDiagnosisAndTreatmentItemEntity>
    {
        object GetzlxmSelect();
    }
}
