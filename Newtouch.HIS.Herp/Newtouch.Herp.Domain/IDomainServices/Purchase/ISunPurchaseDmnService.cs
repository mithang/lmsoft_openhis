using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IDomainServices.Purchase
{
    public interface ISunPurchaseDmnService
    {
        //Kê khai đơn mua hàng vật tư tiêu hao(YY111)
        string Purchase_YY111(string orgId, string cgId);
        //Xác nhận đơn mua hàng vật tư tiêu hao(YY112)
        string Purchase_YY112(string orgId, string cgId);

        //Lấy theo chi tiết hóa đơn vật tư tiêu hao(YY156)
        List<OutputStructYY156> Purchase_YY156(string orgId, string qybm, string fpmxbhcxtj);
        //Xác nhận nghiệm thu hóa đơn vật tư tiêu hao(YY132)
        string Purchase_YY132(PurchaseMainYY132 main, string orgId);

        //Lấy theo chi tiết giao hàng vật tư tiêu hao(YY153)
        List<OutputStructYY153> Purchase_YY153(string orgId, string qybm, string psmxbhcxtj);
        //Xác nhận nghiệm thu giao hàng vật tư tiêu hao(YY131)
        string Purchase_YY131(PurchaseMainYY131 main, List<PurchaseStructYY131> st, string orgId);


        //Kê khai phiếu trả hàng vật tư tiêu hao(YY113)
        string Purchase_YY113(string orgId, string thId);
        //Xác nhận phiếu trả hàng vật tư tiêu hao(YY114)
        string Purchase_YY114(string orgId, string thId);

        //Lấy thông tin hóa đơn vật tư tiêu hao(YY157)
        OutputYY157 Purchase_YY157(PurchaseMainYY157 main, string orgId);
        //Xác nhận thanh toán hóa đơn vật tư tiêu hao(YY133)
        string Purchase_YY133(PurchaseMainYY133 main, string orgId);


        //Báo cáo điểm giao hàng vật tư tiêu hao(YY101)
        string Purchase_YY101(string orgId, string Id, string czlx);

        #region Tra cứu giao diện

        //YY154 Lấy phiếu giao hàng vật tư tiêu hao(YY154)
        OutputYY154 Purchase_YY154(PurchaseMainYY154 main, string orgId);
        //YY155 Lấy chi tiết giao hàng vật tư tiêu hao(YY155)
        OutputYY155 Purchase_YY155(PurchaseMainYY155 main, string orgId);
        //YY158 Lấy chi tiết hóa đơn vật tư tiêu hao(YY158)
        OutputYY158 Purchase_YY158(PurchaseMainYY158 main, string orgId);

        //YY151 Lấy thông tin chi tiết mua hàng vật tư tiêu hao(YY151)
        OutputYY151 Purchase_YY151(PurchaseMainYY151 main, string orgId);

        //YY152 Lấy thông tin chi tiết trả hàng vật tư tiêu hao(YY152)
        OutputYY152 Purchase_YY152(PurchaseMainYY152 main, string orgId);

        //YY159 Lấy trạng thái chi tiết mua hàng vật tư tiêu hao theo đơn mua hàng(YY159)
        OutputYY159 Purchase_YY159(PurchaseMainYY159 main, string orgId);

        //YY160 Lấy trạng thái hóa đơn vật tư tiêu hao(YY160)
        OutputYY160 Purchase_YY160(PurchaseMainYY160 main, string orgId);
        //YY161 Lấy trạng thái phiếu giao hàng vật tư tiêu hao(YY161)
        OutputYY161 Purchase_YY161(PurchaseMainYY161 main, string orgId);
        //YY162 Trạng thái chi tiết trả hàng lấy theo phiếu trả hàng vật tư tiêu hao(YY162)
        OutputYY162 Purchase_YY162(PurchaseMainYY162 main, string orgId);

        //YY164 Lấy thông tin doanh nghiệp(YY164)
        OutputYY164 Purchase_YY164(PurchaseMainYY164 main, string orgId);

        #endregion 
    }
}
