using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    public class HttpTpClientHelper
    {
        public static string Post(string data, string uri, string token)
        {
            // trư ớc theo ngư ời dùng yêu cầu Củauri xây dựng yêu cầu Đ ịa chỉ
            string serviceUrl = string.Format(uri);
            // tạo web truy cập Đ ối tư ợng
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            // ngư ời dùng truyền qua 来của dữ liệu chuyển 成"UTF-8"Của字 mục 流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            if (token != null && token != "")
            {
                myRequest.Headers.Add("Authorization", "Bearer " + token);
            }
            //发送 yêu cầu 
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();

            //Lấygiao diệnquay lại giá trị 
            // thông qua web truy cập Đ ối tư ợng lấy响 nên nội dung 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            // thông qua 响 nên nội dung 流 tạo StreamReaderĐ ối tư ợng,因là StreamReader hơn cao cấp hơn sắp 
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//Nếu có mã 问题 thì dùng này Phư ơng thức
            string returnXml = reader.ReadToEnd();//利 dùng StreamReader thì có thể từ 响 nên nội dung từ 头读 đ ến 尾
            reader.Close();
            myResponse.Close();
            return returnXml;

        }
    }
}
