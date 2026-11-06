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
            //Khi số giấy tờ rỗng  Yêu cầu Họ tên+Số điện thoại  không được để trống
            if (string.IsNullOrWhiteSpace(patIndex.zjh) && (string.IsNullOrWhiteSpace(patIndex.kh) || string.IsNullOrWhiteSpace(patIndex.xm) || string.IsNullOrWhiteSpace(patIndex.dh)))
            {
                throw new FailedException("Họ tên+Số điện thoại+Số thẻ không được để trống");
            }
            //Truyền vào số giấy tờ Yêu cầu Họ tên không được để trống
            if (!string.IsNullOrWhiteSpace(patIndex.zjh) && string.IsNullOrWhiteSpace(patIndex.xm))
            {
                throw new FailedException("Vui lòng nhập họ tên");
            }
            //Tìm thông tin bệnh nhân
            //D-NX25-001: kh = số thẻ (xt_card.CardNo), klx = loại thẻ (xt_card.CardType) —
            //không phải cột của xt_brjbxx nên không đưa vào AssembledWhereEqual mà tra cứu
            //qua xt_card trước, rồi giới hạn kết quả bệnh nhân theo patid của thẻ tìm được.
            //Không tìm thấy thẻ khớp → lỗi nghiệp vụ 40004, không phải ngoại lệ 20000.
            List<int>? cardPatids = null;
            if (!string.IsNullOrWhiteSpace(patIndex.kh))
            {
                var cardsByNo = await baseDal.GetByWhereWithAttr<SysPatCardEntity>(p => p.CardNo == patIndex.kh && p.OrganizeId == patIndex.OrganizeId && p.zt == "1");
                if (!string.IsNullOrWhiteSpace(patIndex.klx))
                {
                    cardsByNo = cardsByNo?.Where(p => p.CardType == patIndex.klx).ToList();
                }
                if (cardsByNo == null || cardsByNo.Count == 0)
                {
                    throw new FailedException("Không tìm thấy thông tin bệnh nhân liên quan");
                }
                cardPatids = cardsByNo.Select(p => p.patid).Distinct().ToList();
            }
            var where = AssembledWhereEqual<SysPatientBasicInfoEntity, SysPatIndexVO>(patIndex);
            var pats = await baseDal.GetListAsync(where);
            if (cardPatids != null && pats != null)
            {
                pats = pats.Where(p => cardPatids.Contains(p.patid)).ToList();
            }
            if (pats == null || pats.Count == 0 || pats.FirstOrDefault()?.patid <= 0)
            {
                throw new FailedException("Không tìm thấy thông tin bệnh nhân liên quan");
            }
            //Tìm thẻ khám
            var pat = pats.FirstOrDefault().Adapt<SysPatInfoVO>();
            var card = await baseDal.GetByWhereWithAttr<SysPatCardEntity>(p => p.patid == pat.patid && p.OrganizeId == pat.OrganizeId && p.zt == "1");
            if (card.Count > 0)
            {
                pat.brxz = brxz == null ? (string.IsNullOrWhiteSpace(pat.brxz) ? ((int)EnumBrxz.zf).ToString().ToString() : pat.brxz) : ((int)brxz).ToString();
                //D-NX25-001: ưu tiên thẻ đúng loại (klx) khi chọn số thẻ trả về; giữ nguyên
                //fallback cũ (thẻ tự chi trả rồi thẻ đầu tiên) khi không có klx/không khớp.
                var khCard = !string.IsNullOrWhiteSpace(patIndex.klx)
                    ? card.FirstOrDefault(p => p.CardType == patIndex.klx)
                    : null;
                khCard ??= card.FirstOrDefault(p => p.brxz == ((int)EnumBrxz.zf).ToString());
                khCard ??= card.FirstOrDefault();
                pat.kh = khCard?.CardNo;
                pat.klx = khCard?.CardType ?? pat.klx;
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
