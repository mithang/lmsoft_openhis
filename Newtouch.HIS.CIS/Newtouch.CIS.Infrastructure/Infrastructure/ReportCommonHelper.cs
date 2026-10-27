using Newtouch.Common;
using NewtouchHIS.Lib.Base.Extension;
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
            //định dạnghóaDữ liệutham số
            XmlDocument paraXml = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(json, "parameter");
            //AppLogger.Info("GetReportDS4");

            //đọclấybáo cáođịnh dạng
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
            foreach (XmlNode item in rptTableDataSourceList) //Báo cáoNguồn dữ liệuLặp lạitải
            {
                //Nguồn dữ liệuTên
                string dsname = item.Attributes["Name"].InnerText;
                //nhauứngNguồn dữ liệuTham số đầu vàoDữ liệu
                XmlNodeList paraList = paraXml.SelectNodes(@"/parameter/" + dsname);
                //LấyNguồn dữ liệutham sốchữđoạn
                DataTable dtRptPara = ConvertXMLToDataSet(dsname, "<row>" + item.InnerXml + "</row>").Tables[0];
                if (dtRptPara != null && dtRptPara.Rows.Count > 0)
                {
                    DataTable dt = new DataTable(dsname);
                    foreach (DataRow rptPara in dtRptPara.Rows)  //Nguồn dữ liệutham sốLặp lại
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
                    foreach (XmlNode para in paraList)  //Nguồn dữ liệutham sốtương ứngGiá trị
                    {
                        DataRow dr = dt.NewRow();
                        int k = 0;
                        foreach (DataColumn rptPara in dr.Table.Columns)  //Nguồn dữ liệutham sốLặp lại
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
        /// LấyvănmụctrongcủaDữ liệu
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string fileToString(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // tạomột StreamReader củathựcVDđếnđọclấyvănmục ,using ngữ句cũngcó thểĐóng StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // từvănmụcđọclấyvàHiển thịdòng，thẳngđếnvănmụccủacuối 
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData += line;
                    }
                }
            }
            catch (Exception e)
            {
                // tớiNgười dùngHiển thịrasaihủytin
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }

        // Xmlkếtcấu trúccủavănmụcđọcđếnDataTabletrong
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