using Newtouch.Common;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace NewtouchCIS.Infrastructure
{
    public class ReportCommonHelper
    {
        public static DataSet GetReportDS(string rptlj, string json)
        {
            DataSet dsPrint = new DataSet();
            string filename = rptlj;
            //Định dạng tham số dữ liệu
            XmlDocument paraXml = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(json, "parameter");
            //AppLogger.Info("GetReportDS4");

            //Đọc định dạng báo cáo
            string rptFile = fileToString(filename);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rptFile);
            XmlNodeList rptTableDataSourceList = doc.SelectNodes(@"/Report/Dictionary/TableDataSource");
            string rptDataband = doc.SelectSingleNode(@"/Report/ReportPage/GroupHeaderBand/DataBand").Attributes
                ["DataSource"].InnerText;/// Report / ReportPage / DataBand
            if (rptTableDataSourceList.Count == 0)
            {
                return dsPrint;
            }
            //if (rptTableDataSourceList.Count > 0)
            //{
            int idx = 0;
            foreach (XmlNode item in rptTableDataSourceList) //Nguồn dữ liệu báo cáo tải theo vòng lặp
            {
                //Tên nguồn dữ liệu
                string dsname = item.Attributes["Name"].InnerText;
                //Dữ liệu tham số đầu vào của nguồn dữ liệu tương ứng
                XmlNodeList paraList = paraXml.SelectNodes(@"/parameter/" + dsname);
                //Lấy trường tham số nguồn dữ liệu
                DataTable dtRptPara = ConvertXMLToDataSet(dsname, "<row>" + item.InnerXml + "</row>").Tables[0];
                if (dtRptPara != null && dtRptPara.Rows.Count > 0)
                {
                    DataTable dt = new DataTable(dsname);
                    foreach (DataRow rptPara in dtRptPara.Rows)  //Vòng lặp tham số nguồn dữ liệu
                    {
                        string colname = rptPara["Name"].ToString();
                        string typestring = rptPara["DataType"].ToString();
                        DataColumn cl = new DataColumn(colname,Type.GetType(typestring));
                        if (rptPara.Table.Columns.Contains("Enabled"))
                        {
                            if (rptPara["Enabled"].ToString() != "false")
                            {
                                dt.Columns.Add(cl);
                            }
                        }
                        else
                        {
                            dt.Columns.Add(cl);
                        }
                    }

                    int j = 0;
                    foreach (XmlNode para in paraList)  //Giá trị tương ứng với tham số nguồn dữ liệu
                    {
                        DataRow dr = dt.NewRow();
                        int k = 0;
                        foreach (DataColumn rptPara in dr.Table.Columns)  //Vòng lặp tham số nguồn dữ liệu
                        {
                            string col = rptPara.ColumnName;
                            string val = para.SelectSingleNode(col) == null ? "" : para.SelectSingleNode(col).InnerText;
                            dr[col] = val;
                            k++;
                        }
                        dt.Rows.Add(dr);
                        j++;
                    }
                    dsPrint.Tables.Add(dt);
                }
                idx++;
            }
            //}
            return dsPrint;
        }

        /// <summary>
        /// Lấy dữ liệu trong tệp
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string fileToString(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // Tạo một thực thể StreamReader để đọc tệp, câu lệnh using cũng có thể đóng StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // Đọc và hiển thị dòng từ tệp cho đến cuối tệp 
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData += line;
                    }
                }
            }
            catch (Exception e)
            {
                // Hiển thị thông báo lỗi cho người dùng
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }

        // Đọc tệp có cấu trúc Xml vào DataTable
        public static DataSet ConvertXMLToDataSet(string tbname, string xmlData)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlData);
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmldoc.InnerXml);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                reader.Close();
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                reader.Close();
                throw ex;
            }
        }

    }
}