using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// wdtree Nguồn dữ liệu Model
    /// </summary>
    public class TreeViewModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// parentId tương ứng với id cấp trên trong câyid
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// Văn bản hiển thị
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Trạng thái được chọn
        /// </summary>
        public int? checkstate { get; set; }

        /// <summary>
        /// Có hay khôngshow check
        /// </summary>
        public bool showcheck { get; set; }

        /// <summary>
        /// Đã tải nút con hay chưa
        /// </summary>
        public bool complete { get; set; }

        /// <summary>
        /// Có hay không Mở rộng(if has child)
        /// </summary>
        public bool isexpand { get; set; }

        /// <summary>
        /// Có cấp dưới hay không
        /// </summary>
        public bool hasChildren { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Trường mở rộng1
        /// </summary>
        public string Ex1 { get; set; }

        /// <summary>
        /// Trường mở rộng2
        /// </summary>
        public string Ex2 { get; set; }

        /// <summary>
        /// Trường mở rộng3
        /// </summary>
        public string Ex3 { get; set; }

        /// <summary>
        /// Trường mở rộng4
        /// </summary>
        public string Ex4 { get; set; }

        /// <summary>
        /// Trường mở rộng5
        /// </summary>
        public string Ex5 { get; set; }

        /// <summary>
        /// Trường mở rộng6
        /// </summary>
        public string Ex6 { get; set; }

    }

    /// <summary>
    /// Xây dựng wdtree json
    /// </summary>
    public static class TreeView
    {
        /// <summary>
        /// Xây dựng wdtree json
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">ParentId của phần tử tầng cao nhấtParentId</param>
        /// <returns></returns>
        public static string TreeViewJson(this List<TreeViewModel> data, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeViewModel> item = data.FindAll(t => t.parentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeViewModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.Code != null)
                    {
                        strJson.Append("\"Code\":\"" + entity.Code + "\",");
                    }
                    if (entity.Ex1 != null)
                    {
                        strJson.Append("\"Ex1\":\"" + entity.Ex1 + "\",");
                    }
                    if (entity.Ex2 != null)
                    {
                        strJson.Append("\"Ex2\":\"" + entity.Ex2 + "\",");
                    }
                    if (entity.Ex3 != null)
                    {
                        strJson.Append("\"Ex3\":\"" + entity.Ex3 + "\",");
                    }
                    if (entity.Ex4 != null)
                    {
                        strJson.Append("\"Ex4\":\"" + entity.Ex4 + "\",");
                    }
                    if (entity.Ex5 != null)
                    {
                        strJson.Append("\"Ex5\":\"" + entity.Ex5 + "\",");
                    }
                    if (entity.Ex6 != null)
                    {
                        strJson.Append("\"Ex6\":\"" + entity.Ex6 + "\",");
                    }
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeViewJson(data, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
