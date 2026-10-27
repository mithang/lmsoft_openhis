using Mapster;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Lib.Base.Exceptions;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.DomainService.PatientCenter
{
    public class PatientInfoDmnService : BaseServices<SysPatientBasicInfoEntity>, IPatientInfoDmnService
    {
        public async Task<SysPatInfoVO> PatientQuery(SysPatIndexVO patIndex, EnumBrxz? brxz)
        {
            if (string.IsNullOrWhiteSpace(patIndex.OrganizeId))
            {
                throw new FailedException("Thông tin tổ chức không được để trống");
            }
            //证件号为空时  要求 Họ tên+Số điện thoại  không được để trống
            if (string.IsNullOrWhiteSpace(patIndex.zjh) && (string.IsNullOrWhiteSpace(patIndex.kh) || string.IsNullOrWhiteSpace(patIndex.xm) || string.IsNullOrWhiteSpace(patIndex.dh)))
            {
                throw new FailedException("Họ tên+Số điện thoại+Số thẻ không được để trống");
            }
            //传入证件号 要求 Họ tên không được để trống
            if (!string.IsNullOrWhiteSpace(patIndex.zjh) && string.IsNullOrWhiteSpace(patIndex.xm))
            {
                throw new FailedException("Vui lòng nhập họ tên");
            }
            //查找患者信息
            var where = AssembledWhereEqual<SysPatientBasicInfoEntity, SysPatIndexVO>(patIndex);
            var pats = await baseDal.GetListAsync(where);
            if (pats == null || pats.Count == 0 || pats.FirstOrDefault()?.patid <= 0)
            {
                throw new FailedException("Không tìm thấy thông tin bệnh nhân liên quan");
            }
            //查找就诊卡
            var pat = pats.FirstOrDefault().Adapt<SysPatInfoVO>();
            var card = await baseDal.GetByWhereWithAttr<SysPatCardEntity>(p => p.patid == pat.patid && p.OrganizeId == pat.OrganizeId && p.zt == "1");
            if (card.Count > 0)
            {
                pat.brxz = brxz == null ? (string.IsNullOrWhiteSpace(pat.brxz) ? ((int)EnumBrxz.zf).ToString().ToString() : pat.brxz) : ((int)brxz).ToString();
                pat.kh = card.Where(p => p.brxz == ((int)EnumBrxz.zf).ToString()).FirstOrDefault()?.CardNo;
                if (string.IsNullOrWhiteSpace(pat.kh))
                {
                    pat.kh = card.FirstOrDefault()?.CardNo;
                }
            }
            return pat.Adapt<SysPatInfoVO>();
        }


        public async Task<SysPatInfoVO> GetPatientbyPatid(int patid, string orgId)
        {
            var pat = await baseDal.GetFirstOrDefault(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1");
            if (pat == null)
            {
                throw new FailedException("Không tìm thấy thông tin bệnh nhân");
            }
            return pat.Adapt<SysPatInfoVO>();
        }

        public async Task<List<SysPatCardIndexVO>> GetPatientCard(int patid, string orgId, string? cardNo = null, string? cardType = null)
        {
            var cards = await baseDal.GetByWhereWithAttr<SysPatCardEntity>(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1");
            if (cards == null)
            {
                throw new FailedException("Bệnh nhân này chưa có thẻ khám bệnh hợp lệ");
            }
            if (!string.IsNullOrWhiteSpace(cardNo))
            {
                cards = cards.Where(p => p.CardNo == cardNo).ToList();
            }
            if (!string.IsNullOrWhiteSpace(cardType))
            {
                cards = cards.Where(p => p.CardType == cardType).ToList();
            }
            //return _mapper.Map<List<SysPatCardIndexVO>>(cards);
            return cards.Adapt<List<SysPatCardIndexVO>>();
        }

    }
}
