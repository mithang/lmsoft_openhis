using System.Drawing;
using System.Drawing.Imaging;

namespace NewtouchHIS.Lib.Base
{
    public class CommonTools
    {
        /// <summary>
        /// Lấy luồng byte của hình ảnh mã xác minh ngẫu nhiên (và ghi mã xác minh vào Session)
        /// </summary>
        /// <param name="chkCode">outMã xác minh</param>
        /// <returns></returns>
        public byte[] GetVerifyCode(out string chkCode)
        {
            int codeW = 80;
            int codeH = 30;
            int fontSize = 16;
            chkCode = string.Empty;
            //Danh sách màu, dùng cho mã xác minh, đường nhiễu, điểm nhiễu 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //Danh sách phông chữ, dùng cho mã xác minh 
            string[] font = { "Times New Roman" };
            //Bộ ký tự của mã xác minh, đã loại bỏ một số ký tự dễ gây nhầm lẫn 
            char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //Tạo chuỗi mã xác minh 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //Tạo khung vẽ
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //Vẽ đường nhiễu 
            for (int i = 0; i < 3; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //Vẽ chuỗi mã xác minh 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
            }
            //Ghi hình ảnh mã xác minh vào luồng bộ nhớ, và xuất nó dưới dạng "image/Png" định dạng đầu ra 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
    }
}
