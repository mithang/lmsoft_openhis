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
            // ngư ời dùng truyền qua đếncủa dữ liệu chuyển thành"UTF-8"Củabyte mục luồng
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
            //gửi yêu cầu 
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();

            //Lấygiao diệnquay lại giá trị 
            // thông qua web truy cập Đ ối tư ợng lấyphản hồi nên nội dung 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            // thông qua phản hồi nên nội dung luồng tạo StreamReaderĐ ối tư ợng,vìlà StreamReader hơn cao cấp hơn sắp 
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//Nếu có mã vấn đề thì dùng này Phư ơng thức
            string returnXml = reader.ReadToEnd();//lợi dụng dùng StreamReader thì có thể từ phản hồi nên nội dung từ đọc từ đầu đ ến cuối
            reader.Close();
            myResponse.Close();
            return returnXml;

        }
    }
}
