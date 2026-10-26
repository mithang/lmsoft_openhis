using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.OutpatientManage;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientApp : AppBase, IOutPatientApp
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;

        /// <summary>
        /// Lưughi sổ ngoại trú(chỉ thêm mới)
        /// </summary>
        public void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            if (accDto == null || accDto.Count < 1)
            {
                throw new FailedException("缺少计划数据");
            }
            var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
            if (ghIsExits == null)
            {
                throw new FailedException("门诊号不存在，请确认");
            }
            var ItemsGroupBO = new FeeItemsGroupedBO
            {
                xmList = new List<OutpatientItemEntity>(),//Thêm chi tiết hạng mục
            };
            foreach (var item in accDto)
            {
                int zsl = 0;//tổng tính phí số lư ợng
                var dcsl = 0;// mỗi lần số lư ợng= điều trị lư ợng /Đ ơn vịtính lư ợng số
                int? zzll = item.zll * item.sl;
                var zje = item.Zje;
                if (item.sfxmCode != null)
                {
                    dcsl = CommmHelper.CalcSfxmSl(item.zll, item.dwjls, item.jjcl);
                    //tổngsố
                    zsl = (dcsl * item.sl).ToInt();
                    zje = zsl * item.dj;    //LạiTínhTổng số tiền
                }

                //Hạng mục ngoại trú
                var mzxm = new OutpatientItemEntity
                {
                    xmnm = 0,
                    sfxm = item.sfxmCode,
                    dl = item.sfdlCode,
                    dj = item.dj,
                    sl = zsl,
                    je = zje,
                    dw = item.dw,
                    zzll = zzll,
                    dczll = item.zll,
                    zxcs = item.sl, // thực hiện số lần 
                    bz = item.bz,
                    ssbz = "9",    //Trực tiếpvào thực hiện quá trình trong như vậy không có thể thao tác hoàn phí
                    sfrq = item.jzsj
                };
                mzxm.Create();
                ItemsGroupBO.xmList.Add(mzxm);
            }

            // mới hạng mục mớiquyết toán
            if (ItemsGroupBO.xmList != null && ItemsGroupBO.xmList.Count() > 0)
            {
                IList<int> cfnmList = new List<int>();
                IList<int> xmnmList = new List<int>();
                IList<string> updateSkipSettledCfhList;
                IList<FeeItemsGroupedBO> ItemsGroupBOList = new List<FeeItemsGroupedBO>();
                ItemsGroupBOList.Add(ItemsGroupBO);
                //Gửichi tiết(Đ ơn thuốc thì cập nhật bảng chính thông tin)
                _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmList, out xmnmList, out updateSkipSettledCfhList, ghIsExits.mzh, ghIsExits.ys, ItemsGroupBOList);
                // Cập nh ậtĐ ơn thuốc ngoại trú của thông tin quyết toán(cập nhật thông tin quyết toán)
                var settAddBO = new OutpatientSettlementAddBO()
                {
                    cfnmList = cfnmList,
                    xmnmList = xmnmList,
                    autoGenePlan = true,    // tự đ ộng tạo ngoại trú kế hoạch 
                };
                _outPatientUniversalDmnService.AddSettlement(orgId, ghIsExits.mzh, settAddBO);
            }
        }

    }
}
