using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISunPurchaseDmnService
    {
        string getMessageDigest(string content);
        PurchaseHead GetHead();
        string PurchaseInterface(string responsexml, string OrganizeId, string param,string jydm, string jymc);

        //Khai báo đơn hàng（YY009）
        string Purchase_YY009(string orgId, string cgId);
        //Xác nhận khai báo đơn hàng（YY010）
        string Purchase_YY010(string orgId, string cgId);

        //Truy vấn và lấy hóa đơn(YY004)
        List<OutputStructYY004> Purchase_YY004(string orgId, string yqbm, string fpmxbh);
        //Nghiệm thu hóa đơn (YY019)
        string Purchase_YY019(PurchaseMainYY019 main, string orgId);
        //Lấy dữ liệu chi tiết đơn giao hàng
        List<OutputStructYY003> Purchase_YY003(string orgId, string yqbm, string psmxbh);
        //Nghiệm thu chi tiết giao hàng
        string Purchase_YY018(PurchaseMainYY018 main, string orgId);
        //Khai báo đơn trả hàng（YY011）
        string Purchase_YY011(string orgId, string cgId);
        //Xác nhận khai báo đơn trả hàng（YY012）
        string Purchase_YY012(string orgId, string cgId);

        //Truyền báo thông tin cơ bản điểm giao hàng bệnh viện(YY001)
        string Purchase_YY001(string orgId, string Id, string czlx);
    }
}
