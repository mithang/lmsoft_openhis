
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HLYYRequest;

using Newtouch.CIS.Proxy.HisApiService;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    
    /// </summary>
    public class HlyyProxy 
    {
        
        // Khai báo client là thuộc tính của lớp
        public HisApiClient Client { get; private set; }
        
        public HlyyProxy()
        {
            // Khởi tạo client trong hàm khởi tạo
            Client = new HisApiClient();
        }

       /**
        * Tra cứu trạng thái duyệt đơn thuốc
        */
        public string GetEngDataStatus(string presNo)
        {
            var request = new EngDataStatusReq()
            {
                Info = new RequestInfo
                {
                    PresNo = presNo
                }
            };
            var reqXMl = new getEngDataStatus
            {
                xml = XMLSerializer.XmlSerializeAll(request)
            };

            return Client.getEngDataStatus(reqXMl).@return;
        }
       
        /**
        * Tra cứu trạng thái duyệt đơn thuốc
        */
        public EngineRes engine(EngineReq engineReq)
        {
            var engine = new engine
            {
                getxml = XMLSerializer.XmlSerializeAll(engineReq)
            };
            return XmlSerializerExt.XmlDeSerialize<EngineRes>(Client.engine(engine).@return);
        }
        
        
        /**
       * Hướng dẫn sử dụng
       */
        public string GetHisSmsJson(string ypmcId)
        {
            var getHisSmsJson = new getHisSmsJson
            {
                ypmcid = ypmcId
            };
            return Client.getHisSmsJson(getHisSmsJson).@return;
        }
       


    }
}