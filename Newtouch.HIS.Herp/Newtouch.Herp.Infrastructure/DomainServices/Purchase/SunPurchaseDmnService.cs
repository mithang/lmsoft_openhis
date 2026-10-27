using Newtonsoft.Json;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.DomainServices;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Infrastructure;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Newtouch.Herp.DomainServices.Purchase
{
    public class SunPurchaseDmnService : DmnServiceBase, ISunPurchaseDmnService
    {

        public SunPurchaseDmnService()
        {
        }

        
        public string sJgbm = Constants.GetAppConfigValue("OrganizeCodeSd");
        public string HospitalName = Constants.GetAppConfigValue("HospitalName");
        public string sUser = Constants.GetAppConfigValue("PurchaseUser");
        public string sPwd = Constants.GetAppConfigValue("PurchasePwd");
        public string sVersion = "1.0.0.0";

        #region Phương pháp chung của API

        public string getMessageDigest(string content)
        {
            try
            {
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(content);
                    byte[] hashBytes = sha1.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //Đọc xml Giá trị của nút đã chỉ định
        public string ReadXmlNodes(string xml, string node)
        {
            string text = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNode nodeA = xmlDoc.SelectSingleNode("XMLDATA");
                XmlNode nodeB = nodeA.SelectSingleNode("MAIN");
                XmlNode nodeC = nodeB.SelectSingleNode(node);
                Trace.TraceInformation("nodeC: " + nodeC);

                if (nodeC != null)
                {
                    text = nodeC.InnerText;
                }
            }
            catch (Exception ex)
            {

                Trace.TraceInformation("Đọc xml Giá trị của nút đã chỉ định bất thường: " + ex);
            }
            return text;
        }

        public string ReadXmlContent(string xml, string nodeUrl)
        {

            string text = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNode node = xmlDoc.SelectSingleNode(nodeUrl);
                if (node != null)
                {
                    text = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("Đọc xml Giá trị của nút đã chỉ định bất thường: " + ex);
                throw new Exception(ex.ToString());
            }
            return text;
        }

        //Truy vấn thông báo lỗi
        public void GetErrorOut(string responseXML)
        {
            //Thông báo lỗi
            var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
            if (ztcljg != "00000")
            {
                var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                throw new Exception(cwxx);
            }
        }


        public PurchaseHead GetHead()
        {
            PurchaseHead head = new PurchaseHead();
            head.IP = "192.168.2.140";
            head.MAC = "CC96E5EF3897";
            head.BZXX = "备注信息";
            return head;
        }

        //public string PurchaseInterface(string responsexml, string OrganizeId, string param, string jydm, string jymc)
        //{
        //    using (var db = new EFDbTransaction(Db).BeginTrans())
        //    {
        //        try
        //        {
        //            //var sUser = "";
        //            //var sPwd = "";
        //            //var sJgbm = "";
        //            //var sVersion = "1.0.0.0";
        //            //var sXxlx = "";
        //            //var sSign = "";
        //            //var xmlData = responsexml;
        //            //var responseXML = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><XMLDATA><HEAD><JSSJ>Thời gian nhận</JSSJ><ZTCLJG>Kết quả xử lý nội dung thông báo</ZTCLJG><CWXX>Nội dung thông báo lỗi</CWXX><BZXX>Thông tin ghi chú</BZXX></HEAD><MAIN><DDBH>Mã đơn hàng</DDBH></MAIN><DETAIL><OutputDetailYY009><DDMXBH>Mã chi tiết đơn hàng</DDMXBH><SXH>Số thứ tự</SXH><SPLX>Loại hàng hóa</SPLX><ZXSPBM>Mã thống nhất hàng hóa</ZXSPBM><CLJG>Kết quả xử lý</CLJG><CLQKMS>Mô tả tình hình xử lý</CLQKMS></OutputDetailYY009></DETAIL></XMLDATA>";

        //            var sXxlx = jydm;
        //            var sSign = getMessageDigest(responsexml);
        //            //Gọi nền tảng mua sắm Sunshine WebService API
        //            YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
        //            var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);

        //            var jsonout = responseXML.XmlDeSerialize<OutputYY009>();
        //            var purchaseLogEntity = new PurchaseLogEntity
        //            {
        //                OrganizeId = OrganizeId,
        //                jydm = jydm,
        //                jymc = jymc,
        //                param = param,
        //                XmlRequest = responsexml,
        //                XmlResponse = responseXML,
        //                zt = "1",
        //            };
        //            purchaseLogEntity.Create(true);
        //            db.Insert(purchaseLogEntity);
        //            db.Commit();
        //        }
        //        catch (Exception ex)
        //        {

        //            return ex.Message;
        //        }
        //        return "";
        //    }
        //}

        #endregion

        public string Purchase_YY111(string orgId, string cgId)
        {
            try
            {
                string HospitalCode = Constants.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = Constants.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SugarParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY111 @orgId,@cgId";
                mainsqlpar.Add(new SugarParameter("@orgId", orgId));
                mainsqlpar.Add(new SugarParameter("@cgId", cgId));
                PurchaseMainYY111 main = ProcFirst<PurchaseMainYY111>(mainsql.ToString(), mainsqlpar.ToArray());
                //detail
                var detailsqlpar = new List<SugarParameter>();
                var detailsql = @"exec usp_PurchaseDetail_YY111 @orgId,@cgId";
                detailsqlpar.Add(new SugarParameter("@orgId", orgId));
                detailsqlpar.Add(new SugarParameter("@cgId", cgId));
                List<PurchaseStructYY111> st = ProcList<PurchaseStructYY111>(detailsql.ToString(), detailsqlpar.ToArray());

                PurchaseDetailY111 detail = new PurchaseDetailY111();
                detail.STRUCT = st;
                PurchaseHead head = GetHead();
                PurchaseYY111 xmlData = new PurchaseYY111();
                xmlData.MAIN = main;
                xmlData.DETAIL = detail;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY111";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                string responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                var param = cgId;

                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY111",
                        jymc = "耗材采购单填报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                if (ztcljg != "00000")
                {
                    var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                    throw new Exception(cwxx);
                }

                //Cập nhật mã đơn hàng
                var ddbh = ReadXmlNodes(responseXML, "DDBH");
                return ddbh;

            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        public string Purchase_YY112(string orgId, string cgId)
        {
            try
            {
                //main
                var mainsqlpar = new List<SugarParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY112 @orgId,@cgId";
                mainsqlpar.Add(new SugarParameter("@orgId", orgId));
                mainsqlpar.Add(new SugarParameter("@cgId", cgId));
                PurchaseMainYY112 main = ProcFirst<PurchaseMainYY112>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY112 xmlData = new PurchaseYY112();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY112";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                XElement xmlElement = XElement.Parse(responseXML);
                string jsonString = JsonConvert.SerializeXNode(xmlElement);
                //OutputYY156 entity = JsonConvert.DeserializeObject<OutputYY156>(jsonString);
                //var jsonout = responseXML.XmlDeSerialize<OutputYY156>();
                var param = cgId;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY112",
                        jymc = "耗材采购单确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);

                return "";

            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }


        public List<OutputStructYY156> Purchase_YY156(string orgId, string qybm, string fpmxbhcxtj)
        {
            try
            {
                //main
                var mainsqlpar = new List<SugarParameter>();
                PurchaseMainYY156 main = new PurchaseMainYY156();
                main.QYBM = qybm;
                main.FPMXBHCXTJ = fpmxbhcxtj;

                PurchaseHead head = GetHead();
                PurchaseYY156 xmlData = new PurchaseYY156();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY156";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);

                List<OutputStructYY156> tmpList = new List<OutputStructYY156>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                foreach (XmlNode node in nodelist)
                {
                    OutputStructYY156 entity = new OutputStructYY156();
                    entity.FPBH = node["FPBH"].InnerText;
                    entity.FPMXBH = node["FPMXBH"].InnerText;
                    entity.FPDM = node["FPDM"].InnerText;
                    entity.FPH = node["FPH"].InnerText;
                    entity.FPRQ = node["FPRQ"].InnerText;
                    entity.FPHSZJE = decimal.Parse(node["FPHSZJE"].InnerText);
                    entity.QYBM = node["QYBM"].InnerText;
                    entity.YYBM = node["YYBM"].InnerText;
                    entity.PSDBM = node["PSDBM"].InnerText;
                    entity.CGLX = node["CGLX"].InnerText;
                    //entity.FPBZ = node["FPBZ"].InnerText;
                    entity.SFWPSFP = node["SFWPSFP"].InnerText;
                    //entity.WPSFPSM = node["WPSFPSM"].InnerText;
                    entity.SFCH = node["SFCH"].InnerText;
                    entity.HCTBDM = node["HCTBDM"].InnerText;
                    entity.HCXFDM = node["HCXFDM"].InnerText;
                    entity.YYBDDM = node["YYBDDM"].InnerText;
                    //entity.GGHXSM = node["GGHXSM"].InnerText;
                    entity.GLMXBH = node["GLMXBH"].InnerText;
                    entity.XSDDH = node["XSDDH"].InnerText;
                    entity.SCPH = node["SCPH"].InnerText;
                    entity.SCRQ = node["SCRQ"].InnerText;
                    //entity.YXEQ = node["YXEQ"].InnerText;
                    entity.SPSL = decimal.Parse(node["SPSL"].InnerText);
                    entity.WSDJ = decimal.Parse(node["WSDJ"].InnerText);
                    entity.HSDJ = decimal.Parse(node["HSDJ"].InnerText);
                    entity.SL = decimal.Parse(node["SL"].InnerText);
                    entity.SE = decimal.Parse(node["SE"].InnerText);
                    entity.HSJE = decimal.Parse(node["HSJE"].InnerText);
                    entity.PFJ = decimal.Parse(node["PFJ"].InnerText);
                    entity.LSJ = decimal.Parse(node["LSJ"].InnerText);
                    entity.ZCZH = node["ZCZH"].InnerText;
                    tmpList.Add(entity);
                }

                var param = qybm;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY156",
                        jymc = "耗材按发票明细获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);
                //return jsonout;
                return tmpList;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        public string Purchase_YY132(PurchaseMainYY132 main, string orgId)
        {
            try
            {
                string HospitalCode = Constants.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = Constants.GetAppConfigValue("HospitalName");
                PurchaseHead head = GetHead();
                PurchaseYY132 xmlData = new PurchaseYY132();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = main.FPH;
                //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                var sXxlx = "YY132";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API

                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                XElement xmlElement = XElement.Parse(responseXML);
                string jsonString = JsonConvert.SerializeXNode(xmlElement);

                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY132",
                        jymc = "耗材发票验收确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);

                return null;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }


        public List<OutputStructYY153> Purchase_YY153(string orgId, string qybm, string psmxbhcxtj)
        {
            try
            {
                //main
                var mainsqlpar = new List<SugarParameter>();
                PurchaseMainYY153 main = new PurchaseMainYY153();
                main.QYBM = qybm;
                main.PSMXBHCXTJ = psmxbhcxtj;

                PurchaseHead head = GetHead();
                PurchaseYY153 xmlData = new PurchaseYY153();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY153";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào API mua sắm: " + qybm + "," + psmxbhcxtj + "," + main);
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                List<OutputStructYY153> tmpList = new List<OutputStructYY153>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                foreach (XmlNode node in nodelist)
                {
                    OutputStructYY153 entity = new OutputStructYY153();
                    entity.PSDBM = node["PSDBM"].InnerText;
                    entity.PSDBH = node["PSDBH"].InnerText;
                    entity.PSMXBH = node["PSMXBH"].InnerText;
                    entity.PSDH = node["PSDH"].InnerText;
                    entity.QYBM = node["QYBM"].InnerText;
                    entity.PSMXTMLX = node["PSMXTMLX"].InnerText;
                    entity.PSMXTM = node["PSMXTM"].InnerText;
                    entity.DDMXBH = node["DDMXBH"].InnerText;
                    entity.YYJHDH = node["YYJHDH"].InnerText;
                    entity.SXH = node["SXH"].InnerText;
                    entity.CWXX = node["CWXX"].InnerText;
                    entity.XSDDH = node["XSDDH"].InnerText;
                    entity.HCTBDM = node["HCTBDM"].InnerText;
                    entity.HCXFDM = node["HCXFDM"].InnerText;
                    entity.YYBDDM = node["YYBDDM"].InnerText;
                    entity.PM = node["PM"].InnerText;
                    entity.GG = node["GG"].InnerText;
                    entity.XH = node["XH"].InnerText;
                    entity.GGXHSM = node["GGXHSM"].InnerText;
                    entity.DW = node["DW"].InnerText;
                    entity.SCQY = node["SCQY"].InnerText;
                    entity.SCPH = node["SCPH"].InnerText;
                    entity.SCRQ = node["SCRQ"].InnerText;
                    entity.YXRQ = node["YXRQ"].InnerText;
                    entity.PSL = decimal.Parse(node["PSL"].InnerText);
                    tmpList.Add(entity);
                }

                var param = qybm;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY153",
                        jymc = "耗材按配送明细获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);

                return tmpList;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        public string Purchase_YY131(PurchaseMainYY131 main, List<PurchaseStructYY131> st, string orgId)
        {
            try
            {
                PurchaseDetailYY131 detail = new PurchaseDetailYY131();
                detail.STRUCT = st;
                PurchaseHead head = GetHead();
                PurchaseYY131 xmlData = new PurchaseYY131();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                xmlData.DETAIL = detail;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = main.PSYSLX;
                //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                var sXxlx = "YY131";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API

                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                //XElement xmlElement = XElement.Parse(responseXML);
                //string jsonString = JsonConvert.SerializeXNode(xmlElement);
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY131",
                        jymc = "耗材配送验收确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);


                return null;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }


        public string Purchase_YY113(string orgId, string thId)
        {
            try
            {

                string HospitalCode = Constants.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = Constants.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SugarParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY113 @orgId,@thId";
                mainsqlpar.Add(new SugarParameter("@orgId", orgId));
                mainsqlpar.Add(new SugarParameter("@thId", thId));
                PurchaseMainYY113 main = ProcFirst<PurchaseMainYY113>(mainsql.ToString(), mainsqlpar.ToArray());
                //detail
                var detailsqlpar = new List<SugarParameter>();
                var detailsql = @"exec usp_PurchaseDetail_YY113 @orgId,@thId";
                detailsqlpar.Add(new SugarParameter("@orgId", orgId));
                detailsqlpar.Add(new SugarParameter("@thId", thId));
                List<PurchaseStructYY113> st = ProcList<PurchaseStructYY113>(detailsql.ToString(), detailsqlpar.ToArray());

                PurchaseDetailY113 detail = new PurchaseDetailY113();
                detail.STRUCT = st;
                PurchaseHead head = GetHead();
                PurchaseYY113 xmlData = new PurchaseYY113();
                xmlData.MAIN = main;
                xmlData.DETAIL = detail;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                //var logstr = PurchaseInterface(responsexml, orgId, param, "YY009", "Khai báo đơn hàng");//API

                var sXxlx = "YY113";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API

                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                //XElement xmlElement = XElement.Parse(responseXML);
                //string jsonString = JsonConvert.SerializeXNode(xmlElement);
                //OutputYY113 entity = JsonConvert.DeserializeObject<OutputYY113>(jsonString);
                //var jsonout = responseXML.XmlDeSerialize<OutputYY113>();

                //Trace.TraceInformation("thdbh:: " + entity.MAIN.THDBH);
                //Trace.TraceInformation("jsonout:: " + jsonout);
                var param = thId;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY113",
                        jymc = "耗材退货单填报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);


                //Cập nhật mã đơn hàng
                var ddbh = ReadXmlNodes(responseXML, "THDBH");
                return ddbh;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        public string Purchase_YY114(string orgId, string thId)
        {
            try
            {
                //main
                var mainsqlpar = new List<SugarParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY114 @orgId,@thId";
                mainsqlpar.Add(new SugarParameter("@orgId", orgId));
                mainsqlpar.Add(new SugarParameter("@thId", thId));
                PurchaseMainYY114 main = ProcFirst<PurchaseMainYY114>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY114 xmlData = new PurchaseYY114();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY114";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API

                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                //YsxtMainServiceImplService ysxtMainServiceClient = new YsxtMainServiceImplService();
                //Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + ysxtMainServiceClient.Url);
                //var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                XElement xmlElement = XElement.Parse(responseXML);
                string jsonString = JsonConvert.SerializeXNode(xmlElement);
                //OutputYY114 entity = JsonConvert.DeserializeObject<OutputYY114>(jsonString);
                //var jsonout = responseXML.XmlDeSerialize<OutputYY114>();
                var param = thId;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY114",
                        jymc = "耗材退货单确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);

                return "";

            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }


        public OutputYY157 Purchase_YY157(PurchaseMainYY157 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY157 xmlData = new PurchaseYY157();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY157";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY157 output = new OutputYY157();
                OutputHead outHead = new OutputHead();
                OutputMainYY157 outMain = new OutputMainYY157();
                OutputDetailYY157 outDetail = new OutputDetailYY157();
                List<OutputStructYY157> outStruct = new List<OutputStructYY157>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHFPBH = "";
                    }
                    else
                    {
                        outMain.DCZHFPBH = node["DCZHFPBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY157> tmpList = new List<OutputStructYY157>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY157 entity = new OutputStructYY157();
                        entity.FPBH = node["FPBH"].InnerText;
                        entity.FPDM = node["FPDM"].InnerText;
                        entity.FPH = node["FPH"].InnerText;
                        entity.FPRQ = node["FPRQ"].InnerText;
                        entity.FPHSZJE = decimal.Parse(node["FPHSZJE"].InnerText);
                        entity.QYBM = node["QYBM"].InnerText;
                        entity.YYBM = node["YYBM"].InnerText;
                        entity.PSDBM = node["PSDBM"].InnerText;
                        entity.CGLX = node["CGLX"].InnerText;
                        //entity.FPBZ = node["FPBZ"].InnerText;
                        entity.FPMXTS = int.Parse(node["FPMXTS"].InnerText);
                        entity.FPZT = node["FPZT"].InnerText;
                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.QYBM;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY157",
                        jymc = "耗材发票信息获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }


        public string Purchase_YY133(PurchaseMainYY133 main, string orgId)
        {
            try
            {
                string HospitalCode = Constants.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = Constants.GetAppConfigValue("HospitalName");
                PurchaseHead head = GetHead();
                PurchaseYY133 xmlData = new PurchaseYY133();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = main.FPH;
                //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                var sXxlx = "YY133";
                var sSign = getMessageDigest(responsexml);

                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);

                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY133",
                        jymc = "耗材发票支付确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                ////Thông báo lỗi
                //var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                //if (cwxx != null)
                //{
                //    throw new Exception(cwxx);
                //}

                return null;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }


        public string Purchase_YY101(string orgId, string Id,string czlx)
        {
            try
            {
                //main
                var mainsqlpar = new List<SugarParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY101 @orgId,@Id,@czlx";
                mainsqlpar.Add(new SugarParameter("@orgId", orgId));
                mainsqlpar.Add(new SugarParameter("@Id", Id));
                mainsqlpar.Add(new SugarParameter("@czlx", czlx));
                PurchaseMainYY101 main = ProcFirst<PurchaseMainYY101>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY101 xmlData = new PurchaseYY101();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY101";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                var param = Id;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY101",
                        jymc = "耗材配送点传报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //Thông báo lỗi
                GetErrorOut(responseXML);

                return "Success";
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return e.Message;
            }
        }

        #region Truy vấn API

        //YY154 Lấy phiếu phân phối vật tư tiêu hao(YY154)
        public OutputYY154 Purchase_YY154(PurchaseMainYY154 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY154 xmlData = new PurchaseYY154();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY154";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY154 output = new OutputYY154();
                OutputHead outHead = new OutputHead();
                OutputMainYY154 outMain = new OutputMainYY154();
                OutputDetailYY154 outDetail = new OutputDetailYY154();
                List<OutputStructYY154> outStruct = new List<OutputStructYY154>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHPSDBH = "";
                    }
                    else
                    {
                        outMain.DCZHPSDBH = node["DCZHPSDBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY154> tmpList = new List<OutputStructYY154>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY154 entity = new OutputStructYY154();
                        if (node.SelectSingleNode("PSDBH") != null) { entity.PSDBH = node["PSDBH"].InnerText; }
                        if (node.SelectSingleNode("PSDH") != null) { entity.PSDH = node["PSDH"].InnerText; }
                        if (node.SelectSingleNode("QYBM") != null) { entity.QYBM = node["QYBM"].InnerText; }
                        if (node.SelectSingleNode("PSMXTS") != null) { entity.PSMXTS = int.Parse(node["PSMXTS"].InnerText); }
                        if (node.SelectSingleNode("PSDZT") != null) { entity.PSDZT = node["PSDZT"].InnerText; }

                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.PSDBHCXTJ;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY154",
                        jymc = "耗材配送单获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY155 Lấy chi tiết phân phối vật tư tiêu hao(YY155)
        public OutputYY155 Purchase_YY155(PurchaseMainYY155 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY155 xmlData = new PurchaseYY155();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY155";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY155 output = new OutputYY155();
                OutputHead outHead = new OutputHead();
                OutputMainYY155 outMain = new OutputMainYY155();
                OutputDetailYY155 outDetail = new OutputDetailYY155();
                List<OutputStructYY155> outStruct = new List<OutputStructYY155>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHPSMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHPSMXBH = node["DCZHPSMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY155> tmpList = new List<OutputStructYY155>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY155 entity = new OutputStructYY155();
                        if (node.SelectSingleNode("PSMXBH") != null) { entity.PSMXBH = node["PSMXBH"].InnerText; }
                        if (node.SelectSingleNode("PSMXTMLX") != null) { entity.PSMXTMLX = node["PSMXTMLX"].InnerText; }
                        if (node.SelectSingleNode("PSMXTM") != null) { entity.PSMXTM = node["PSMXTM"].InnerText; }
                        if (node.SelectSingleNode("DDMXBH") != null) { entity.DDMXBH = node["DDMXBH"].InnerText; }
                        if (node.SelectSingleNode("YYJHDH") != null) { entity.YYJHDH = node["YYJHDH"].InnerText; }
                        if (node.SelectSingleNode("SXH") != null) { entity.SXH = node["SXH"].InnerText; }
                        if (node.SelectSingleNode("CWXX") != null) { entity.CWXX = node["CWXX"].InnerText; }
                        if (node.SelectSingleNode("XSDDH") != null) { entity.XSDDH = node["XSDDH"].InnerText; }
                        if (node.SelectSingleNode("HCTBDM") != null) { entity.HCTBDM = node["HCTBDM"].InnerText; }
                        if (node.SelectSingleNode("HCXFDM") != null) { entity.HCXFDM = node["HCXFDM"].InnerText; }
                        if (node.SelectSingleNode("YYBDDM") != null) { entity.YYBDDM = node["YYBDDM"].InnerText; }
                        if (node.SelectSingleNode("PM") != null) { entity.PM = node["PM"].InnerText; }
                        if (node.SelectSingleNode("GG") != null) { entity.GG = node["GG"].InnerText; }
                        if (node.SelectSingleNode("XH") != null) { entity.XH = node["XH"].InnerText; }
                        if (node.SelectSingleNode("GGXHSM") != null) { entity.GGXHSM = node["GGXHSM"].InnerText; }
                        if (node.SelectSingleNode("DW") != null) { entity.DW = node["DW"].InnerText; }
                        if (node.SelectSingleNode("SCQY") != null) { entity.SCQY = node["SCQY"].InnerText; }
                        if (node.SelectSingleNode("SCPH") != null) { entity.SCPH = node["SCPH"].InnerText; }
                        if (node.SelectSingleNode("SCRQ") != null) { entity.SCRQ = node["SCRQ"].InnerText; }
                        if (node.SelectSingleNode("YXRQ") != null) { entity.YXRQ = node["YXRQ"].InnerText; }
                        if (node.SelectSingleNode("PSL") != null) { entity.PSL = decimal.Parse(node["PSL"].InnerText); }

                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.PSMXBHCXTJ;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY155",
                        jymc = "耗材配送明细获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY158 Lấy chi tiết hóa đơn vật tư tiêu hao(YY158)

        public OutputYY158 Purchase_YY158(PurchaseMainYY158 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY158 xmlData = new PurchaseYY158();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY158";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY158 output = new OutputYY158();
                OutputHead outHead = new OutputHead();
                OutputMainYY158 outMain = new OutputMainYY158();
                OutputDetailYY158 outDetail = new OutputDetailYY158();
                List<OutputStructYY158> outStruct = new List<OutputStructYY158>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHFPMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHFPMXBH = node["DCZHFPMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY158> tmpList = new List<OutputStructYY158>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY158 entity = new OutputStructYY158();
                        if (node.SelectSingleNode("FPMXBH") != null) { entity.FPMXBH = node["FPMXBH"].InnerText; }
                        if (node.SelectSingleNode("SFWPSFP") != null) { entity.SFWPSFP = node["SFWPSFP"].InnerText; }
                        if (node.SelectSingleNode("WPSFPSM") != null) { entity.WPSFPSM = node["WPSFPSM"].InnerText; }
                        if (node.SelectSingleNode("SFCH") != null) { entity.SFCH = node["SFCH"].InnerText; }
                        if (node.SelectSingleNode("HCTBDM") != null) { entity.HCTBDM = node["HCTBDM"].InnerText; }
                        if (node.SelectSingleNode("HCXFDM") != null) { entity.HCXFDM = node["HCXFDM"].InnerText; }
                        if (node.SelectSingleNode("YYBDDM") != null) { entity.YYBDDM = node["YYBDDM"].InnerText; }
                        if (node.SelectSingleNode("GGXHSM") != null) { entity.GGXHSM = node["GGXHSM"].InnerText; }
                        if (node.SelectSingleNode("GLMXBH") != null) { entity.GLMXBH = node["GLMXBH"].InnerText; }
                        if (node.SelectSingleNode("XSDDH") != null) { entity.XSDDH = node["XSDDH"].InnerText; }
                        if (node.SelectSingleNode("SCPH") != null) { entity.SCPH = node["SCPH"].InnerText; }
                        if (node.SelectSingleNode("SCRQ") != null) { entity.SCRQ = node["SCRQ"].InnerText; }
                        if (node.SelectSingleNode("YXRQ") != null) { entity.YXRQ = node["YXRQ"].InnerText; }
                        if (node.SelectSingleNode("SPSL") != null) { entity.SPSL = decimal.Parse(node["SPSL"].InnerText); }
                        if (node.SelectSingleNode("WSDJ") != null) { entity.WSDJ = decimal.Parse(node["WSDJ"].InnerText); }
                        if (node.SelectSingleNode("HSDJ") != null) { entity.HSDJ = decimal.Parse(node["HSDJ"].InnerText); }
                        if (node.SelectSingleNode("SL") != null) { entity.SL = decimal.Parse(node["SL"].InnerText); }
                        if (node.SelectSingleNode("SE") != null) { entity.SE = decimal.Parse(node["SE"].InnerText); }
                        if (node.SelectSingleNode("HSJE") != null) { entity.HSJE = decimal.Parse(node["HSJE"].InnerText); }
                        if (node.SelectSingleNode("PFJ") != null) { entity.PFJ = decimal.Parse(node["PFJ"].InnerText); }
                        if (node.SelectSingleNode("LSJ") != null) { entity.LSJ = decimal.Parse(node["LSJ"].InnerText); }
                        if (node.SelectSingleNode("ZCZH") != null) { entity.ZCZH = node["ZCZH"].InnerText; }


                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.FPMXBHCXTJ;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY158",
                        jymc = "耗材发票信息获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY151 Lấy thông tin chi tiết mua sắm vật tư tiêu hao(YY151)

        public OutputYY151 Purchase_YY151(PurchaseMainYY151 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY151 xmlData = new PurchaseYY151();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY151";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY151 output = new OutputYY151();
                OutputHead outHead = new OutputHead();
                OutputMainYY151 outMain = new OutputMainYY151();
                OutputDetailYY151 outDetail = new OutputDetailYY151();
                List<OutputStructYY151> outStruct = new List<OutputStructYY151>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHDDMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHDDMXBH = node["DCZHDDMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY151> tmpList = new List<OutputStructYY151>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY151 entity = new OutputStructYY151();
                        if (node.SelectSingleNode("DDLX")!=null)
                        {
                            entity.DDLX = node["DDLX"].InnerText;
                        }

                        if (node.SelectSingleNode("DDLX") != null) { entity.DJTXF = node["DJTXF"].InnerText; }
                        if (node.SelectSingleNode("DDMXBH") != null) { entity.DDMXBH = node["DDMXBH"].InnerText;}
                        if (node.SelectSingleNode("DDBH") != null) { entity.DDBH = node["DDBH"].InnerText;}
                        if (node.SelectSingleNode("SXH") != null) { entity.SXH = node["SXH"].InnerText;}
                        if (node.SelectSingleNode("YYJHDH") != null) { entity.YYJHDH = node["YYJHDH"].InnerText;}
                        if (node.SelectSingleNode("QYBM") != null) { entity.QYBM = node["QYBM"].InnerText;}
                        if (node.SelectSingleNode("PSDBM") != null) { entity.PSDBM = node["PSDBM"].InnerText;}
                        if (node.SelectSingleNode("CGLX") != null) { entity.CGLX = node["CGLX"].InnerText;}
                        if (node.SelectSingleNode("HCTBDM") != null) { entity.HCTBDM = node["HCTBDM"].InnerText;}
                        if (node.SelectSingleNode("HCXFDM") != null) { entity.HCXFDM = node["HCXFDM"].InnerText;}
                        if (node.SelectSingleNode("YYBDDM") != null) { entity.YYBDDM = node["YYBDDM"].InnerText;}
                        if (node.SelectSingleNode("PM") != null) { entity.PM = node["PM"].InnerText;}
                        if (node.SelectSingleNode("GG") != null) { entity.GG = node["GG"].InnerText;}
                        if (node.SelectSingleNode("XH") != null) { entity.XH = node["XH"].InnerText;}
                        if (node.SelectSingleNode("GGXHSM") != null) { entity.GGXHSM = node["GGXHSM"].InnerText;}
                        if (node.SelectSingleNode("DW") != null) { entity.DW = node["DW"].InnerText;}
                        if (node.SelectSingleNode("SCQY") != null) { entity.SCQY = node["SCQY"].InnerText;}
                        if (node.SelectSingleNode("CGGGXH") != null) { entity.CGGGXH = node["CGGGXH"].InnerText;}
                        if (node.SelectSingleNode("PSSM") != null) { entity.PSSM = node["PSSM"].InnerText;}
                        if (node.SelectSingleNode("CGSL") != null) { entity.CGSL = decimal.Parse(node["CGSL"].InnerText);}
                        if (node.SelectSingleNode("CGDJ") != null) { entity.CGDJ = decimal.Parse(node["CGDJ"].InnerText);}
                        if (node.SelectSingleNode("SFJJ") != null) { entity.SFJJ = node["SFJJ"].InnerText;}
                        if (node.SelectSingleNode("PSYQ") != null) { entity.PSYQ = node["PSYQ"].InnerText;}
                        if (node.SelectSingleNode("CWXX") != null) { entity.CWXX = node["CWXX"].InnerText;}
                        if (node.SelectSingleNode("DCPSBS") != null) { entity.DCPSBS = node["DCPSBS"].InnerText;}
                        if (node.SelectSingleNode("BZSM") != null) { entity.BZSM = node["BZSM"].InnerText;}
                        if (node.SelectSingleNode("CGFS") != null) { entity.CGFS = node["CGFS"].InnerText;}
                        if (node.SelectSingleNode("XTBM") != null) { entity.XTBM = node["XTBM"].InnerText;}
                        if (node.SelectSingleNode("SFHBSFW") != null) { entity.SFHBSFW = node["SFHBSFW"].InnerText;}

                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.QYBM;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY151",
                        jymc = "耗材采购明细信息获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }


        //YY152 Lấy thông tin chi tiết trả hàng vật tư tiêu hao(YY152)

        public OutputYY152 Purchase_YY152(PurchaseMainYY152 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY152 xmlData = new PurchaseYY152();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY152";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY152 output = new OutputYY152();
                OutputHead outHead = new OutputHead();
                OutputMainYY152 outMain = new OutputMainYY152();
                OutputDetailYY152 outDetail = new OutputDetailYY152();
                List<OutputStructYY152> outStruct = new List<OutputStructYY152>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHTHMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHTHMXBH = node["DCZHTHMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY152> tmpList = new List<OutputStructYY152>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY152 entity = new OutputStructYY152();
                        if (node.SelectSingleNode("DJTXF") != null) { entity.DJTXF = node["DJTXF"].InnerText; }
                        if (node.SelectSingleNode("QYBM") != null) { entity.QYBM = node["QYBM"].InnerText; }
                        if (node.SelectSingleNode("PSDBM") != null) { entity.PSDBM = node["PSDBM"].InnerText; }
                        if (node.SelectSingleNode("THDBH") != null) { entity.THDBH = node["THDBH"].InnerText; }
                        if (node.SelectSingleNode("THMXBH") != null) { entity.THMXBH = node["THMXBH"].InnerText; }
                        if (node.SelectSingleNode("SXH") != null) { entity.SXH = node["SXH"].InnerText; }
                        if (node.SelectSingleNode("CGLX") != null) { entity.CGLX = node["CGLX"].InnerText; }
                        if (node.SelectSingleNode("THLX") != null) { entity.THLX = node["THLX"].InnerText; }
                        if (node.SelectSingleNode("HCTBDM") != null) { entity.HCTBDM = node["HCTBDM"].InnerText; }
                        if (node.SelectSingleNode("HCXFDM") != null) { entity.HCXFDM = node["HCXFDM"].InnerText; }
                        if (node.SelectSingleNode("YYBDDM") != null) { entity.YYBDDM = node["YYBDDM"].InnerText; }
                        if (node.SelectSingleNode("PM") != null) { entity.PM = node["PM"].InnerText; }
                        if (node.SelectSingleNode("GG") != null) { entity.GG = node["GG"].InnerText; }
                        if (node.SelectSingleNode("XH") != null) { entity.XH = node["XH"].InnerText; }
                        if (node.SelectSingleNode("GGXHSM") != null) { entity.GGXHSM = node["GGXHSM"].InnerText; }
                        if (node.SelectSingleNode("DW") != null) { entity.DW = node["DW"].InnerText; }
                        if (node.SelectSingleNode("SCQY") != null) { entity.SCQY = node["SCQY"].InnerText; }
                        if (node.SelectSingleNode("CGGGXH") != null) { entity.CGGGXH = node["CGGGXH"].InnerText; }
                        if (node.SelectSingleNode("SCPH") != null) { entity.SCPH = node["SCPH"].InnerText; }
                        if (node.SelectSingleNode("SCRQ") != null) { entity.SCRQ = node["SCRQ"].InnerText; }
                        if (node.SelectSingleNode("YXRQ") != null) { entity.YXRQ = node["YXRQ"].InnerText; }
                        if (node.SelectSingleNode("PSMXTMLX") != null) { entity.PSMXTMLX = node["PSMXTMLX"].InnerText; }
                        if (node.SelectSingleNode("PSMXTM") != null) { entity.PSMXTM = node["PSMXTM"].InnerText; }
                        if (node.SelectSingleNode("THSL") != null) { entity.THSL = decimal.Parse(node["THSL"].InnerText); }
                        if (node.SelectSingleNode("THDJ") != null) { entity.THDJ = decimal.Parse(node["THDJ"].InnerText); }
                        if (node.SelectSingleNode("THYY") != null) { entity.THYY = node["THYY"].InnerText; }
                        if (node.SelectSingleNode("CGFS") != null) { entity.CGFS = node["CGFS"].InnerText; }
                        if (node.SelectSingleNode("XTBM") != null) { entity.XTBM = node["XTBM"].InnerText; }
                        if (node.SelectSingleNode("SFHBSFW") != null) { entity.SFHBSFW = node["SFHBSFW"].InnerText; }


                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.QYBM;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY152",
                        jymc = "耗材退货明细获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }


        //YY159 Lấy trạng thái chi tiết mua sắm theo đơn mua sắm vật tư tiêu hao(YY159)
        public OutputYY159 Purchase_YY159(PurchaseMainYY159 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY159 xmlData = new PurchaseYY159();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY159";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY159 output = new OutputYY159();
                OutputHead outHead = new OutputHead();
                OutputMainYY159 outMain = new OutputMainYY159();
                OutputDetailYY159 outDetail = new OutputDetailYY159();
                List<OutputStructYY159> outStruct = new List<OutputStructYY159>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHDDMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHDDMXBH = node["DCZHDDMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY159> tmpList = new List<OutputStructYY159>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY159 entity = new OutputStructYY159();
                        if (node.SelectSingleNode("DDMXBH") != null) { entity.DDMXBH = node["DDMXBH"].InnerText; }
                        if (node.SelectSingleNode("CGMXZT") != null) { entity.CGMXZT = node["CGMXZT"].InnerText; }
                        if (node.SelectSingleNode("QYKC") != null) { entity.QYKC = node["QYKC"].InnerText; }
                        if (node.SelectSingleNode("CGMXSHYJ") != null) { entity.CGMXSHYJ = node["CGMXSHYJ"].InnerText; }
                        if (node.SelectSingleNode("CGDCLSM") != null) { entity.CGDCLSM = node["CGDCLSM"].InnerText; }
                        
                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.DDBH;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY159",
                        jymc = "耗材按采购单获取采购明细状态",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY160 Lấy trạng thái hóa đơn vật tư tiêu hao(YY160)

        public OutputYY160 Purchase_YY160(PurchaseMainYY160 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY160 xmlData = new PurchaseYY160();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY160";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY160 output = new OutputYY160();
                OutputHead outHead = new OutputHead();
                OutputMainYY160 outMain = new OutputMainYY160();
                OutputDetailYY160 outDetail = new OutputDetailYY160();
                List<OutputStructYY160> outStruct = new List<OutputStructYY160>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHFPBH = "";
                    }
                    else
                    {
                        outMain.DCZHFPBH = node["DCZHFPBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY160> tmpList = new List<OutputStructYY160>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY160 entity = new OutputStructYY160();
                        if (node.SelectSingleNode("FPBH") != null) { entity.FPBH = node["FPBH"].InnerText; }
                        if (node.SelectSingleNode("FPDM") != null) { entity.FPDM = node["FPDM"].InnerText; }
                        if (node.SelectSingleNode("FPH") != null) { entity.FPH = node["FPH"].InnerText; }
                        if (node.SelectSingleNode("FPZT") != null) { entity.FPZT = node["FPZT"].InnerText; }
                        
                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.FPH;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY160",
                        jymc = "耗材发票状态获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY161 Lấy trạng thái phiếu phân phối vật tư tiêu hao(YY161)
        public OutputYY161 Purchase_YY161(PurchaseMainYY161 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY161 xmlData = new PurchaseYY161();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY161";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY161 output = new OutputYY161();
                OutputHead outHead = new OutputHead();
                OutputMainYY161 outMain = new OutputMainYY161();
                OutputDetailYY161 outDetail = new OutputDetailYY161();
                List<OutputStructYY161> outStruct = new List<OutputStructYY161>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHPSMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHPSMXBH = node["DCZHPSMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY161> tmpList = new List<OutputStructYY161>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY161 entity = new OutputStructYY161();
                        if (node.SelectSingleNode("PSMXBH") != null) { entity.PSMXBH = node["PSMXBH"].InnerText; }
                        if (node.SelectSingleNode("PSMXZT") != null) { entity.PSMXZT = node["PSMXZT"].InnerText; }
                        if (node.SelectSingleNode("YYYTGS") != null) { entity.YYYTGS = decimal.Parse(node["YYYTGS"].InnerText); }
                        if (node.SelectSingleNode("YYYBGS") != null) { entity.YYYBGS = decimal.Parse(node["YYYBGS"].InnerText); }
                        if (node.SelectSingleNode("YSYTGS") != null) { entity.YSYTGS = decimal.Parse(node["YSYTGS"].InnerText); }
                        if (node.SelectSingleNode("YSYBGS") != null) { entity.YSYBGS = decimal.Parse(node["YSYBGS"].InnerText); }


                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.PSDBH ;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY161",
                        jymc = "发票配送单状态获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        //YY162 Lấy trạng thái chi tiết trả hàng theo phiếu trả hàng vật tư tiêu hao(YY162)
        public OutputYY162 Purchase_YY162(PurchaseMainYY162 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY162 xmlData = new PurchaseYY162();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY162";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY162 output = new OutputYY162();
                OutputHead outHead = new OutputHead();
                OutputMainYY162 outMain = new OutputMainYY162();
                OutputDetailYY162 outDetail = new OutputDetailYY162();
                List<OutputStructYY162> outStruct = new List<OutputStructYY162>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHTHMXBH = "";
                    }
                    else
                    {
                        outMain.DCZHTHMXBH = node["DCZHTHMXBH"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY162> tmpList = new List<OutputStructYY162>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY162 entity = new OutputStructYY162();
                        if (node.SelectSingleNode("THMXBH") != null) { entity.THMXBH = node["THMXBH"].InnerText; }
                        if (node.SelectSingleNode("THMXZT") != null) { entity.THMXZT = node["THMXZT"].InnerText; }
                        if (node.SelectSingleNode("THDQYCLSM") != null) { entity.THDQYCLSM = node["THDQYCLSM"].InnerText; }

                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.THDBH;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY162",
                        jymc = "耗材按退货单获取退货明细状态",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }


        //YY164 Lấy thông tin doanh nghiệp(YY164)
        public OutputYY164 Purchase_YY164(PurchaseMainYY164 main, string orgId)
        {
            try
            {
                PurchaseHead head = GetHead();
                PurchaseYY164 xmlData = new PurchaseYY164();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY164";
                var sSign = getMessageDigest(responsexml);
                //Gọi nền tảng mua sắm Sunshine WebService API
                Trace.TraceInformation("============= " + sXxlx + " ============= ");
                Trace.TraceInformation("Gọi tham số đầu vào quy định của giao dịch mua sắm: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                Trace.TraceInformation("Gọi yêu cầu giao dịch mua sắm url: " + responsexml);
                                var responseXML = SendRecvSoap(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230825/032652/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><DCZHFPBH>20230810010003144455</DCZHFPBH><JLS>1</JLS></MAIN><DETAIL><STRUCT><FPBH>20230810010003144455</FPBH><FPMXTS>1</FPMXTS><FPZT>30</FPZT><FPDM>23312000000053652517</FPDM><FPH>23312000000053652517</FPH><FPRQ>20230809</FPRQ><FPHSZJE>16800</FPHSZJE><QYBM>QY000000000000007993</QYBM><YYBM>77370296000</YYBM><PSDBM>MDWGK01</PSDBM><CGLX>1</CGLX></STRUCT></DETAIL></XMLDATA>";
                //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><XMLDATA><HEAD><JSSJ>20230828/025645/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX></BZXX></HEAD><MAIN><SFWJ>1</SFWJ><JLS>0</JLS></MAIN><DETAIL/></XMLDATA>";
                Trace.TraceInformation("Gọi tham số đầu ra của giao dịch mua sắm: " + responseXML);

                OutputYY164 output = new OutputYY164();
                OutputHead outHead = new OutputHead();
                OutputMainYY164 outMain = new OutputMainYY164();
                OutputDetailYY164 outDetail = new OutputDetailYY164();
                List<OutputStructYY164> outStruct = new List<OutputStructYY164>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXML);
                //Xuất main Nút
                XmlNodeList nodeMain = xmlDoc.SelectNodes("XMLDATA/MAIN");
                foreach (XmlNode node in nodeMain)
                {
                    outMain.SFWJ = node["SFWJ"].InnerText;
                    if (outMain.SFWJ == "1")
                    {
                        outMain.DCZHQYBM = "";
                    }
                    else
                    {
                        outMain.DCZHQYBM = node["DCZHQYBM"].InnerText;
                    }
                    outMain.JLS = int.Parse(node["JLS"].InnerText);
                }

                //Xuất detail Nút
                //List<OutputStructYY164> tmpList = new List<OutputStructYY164>();
                if (outMain.JLS > 0)
                {
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY164 entity = new OutputStructYY164();
                        if (node.SelectSingleNode("QYBM") != null) { entity.QYBM = node["QYBM"].InnerText; }
                        if (node.SelectSingleNode("QYMC") != null) { entity.QYMC = node["QYMC"].InnerText; }
                        if (node.SelectSingleNode("QYDZ") != null) { entity.QYDZ = node["QYDZ"].InnerText; }
                        if (node.SelectSingleNode("LXR") != null) { entity.LXR = node["LXR"].InnerText; }
                        if (node.SelectSingleNode("LXDH") != null) { entity.LXDH = node["LXDH"].InnerText; }

                        outStruct.Add(entity);
                    }
                    outDetail.STRUCT = outStruct;

                }
                output.MAIN = outMain;
                output.DETAIL = outDetail;

                var param = main.QYBMCXTJ;
                using (var db = new EFDbTransaction(Db).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY164",
                        jymc = "企业信息获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //Thông báo lỗi
                GetErrorOut(responseXML);
                return output;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Gọi giao dịch mua sắm thất bại: " + e.Message);
                return null;
            }
        }

        #endregion

        

        // Stored procedures (usp_PurchaseMain_YY*/usp_PurchaseDetail_YY*) cannot
        // go through SqlQueryable (it wraps the text as a SELECT subquery and
        // rejects "exec"); Ado.SqlQuery executes them directly, as EF did.
        private List<T> ProcList<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return Db.Ado.SqlQuery<T>(sql, parameters);
        }

        private T ProcFirst<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return Db.Ado.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        #region WebService Gọi API (Core port: hand-written SOAP client, option 2)

        // DispatcherService contract (from Connected Services PurchaseWebService:
        // SendRecv.wsdl + Reference.cs, BasicHttpBinding, soapAction=""):
        //   POST http://192.168.20.110/ysxtqx-ws/services/dispatcher
        //   <SendRecv xmlns="http://ws.framework.shys.wondersgroup.com/">
        //     <sUser/><sPwd/><sJgbm/><sVersion/><sXxlx/><sSign/><xmlData/>
        //   </SendRecv>
        //   -> <SendRecvResponse><return>XMLDATA...</return></SendRecvResponse>
        // No dotnet-svcutil/CoreWCF regeneration: the contract is a single
        // string-in/string-out operation, so a raw envelope is exact and has
        // no extra package dependency. The endpoint host is the vendor's
        // on-prem address from app.config; it does not exist in this
        // deployment, so calls fail to connect here (callers catch and
        // return null / the error message, exactly as the original did).
        // IPv4-only sockets + no proxy, same sandbox rule as
        // HerpHttpClientHelper (default dual-mode sockets are intercepted).
        private static readonly System.Net.Http.HttpClient _soapClient = CreateSoapClient();

        private static System.Net.Http.HttpClient CreateSoapClient()
        {
            var handler = new System.Net.Http.SocketsHttpHandler
            {
                UseProxy = false,
                ConnectCallback = async (context, cancellationToken) =>
                {
                    var addresses = await System.Net.Dns.GetHostAddressesAsync(context.DnsEndPoint.Host, cancellationToken);
                    var ipv4 = System.Linq.Enumerable.FirstOrDefault(addresses, a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    System.Net.Sockets.Socket socket;
                    if (ipv4 != null)
                    {
                        socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                        await socket.ConnectAsync(new System.Net.IPEndPoint(ipv4, context.DnsEndPoint.Port), cancellationToken);
                    }
                    else
                    {
                        socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetworkV6, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                        await socket.ConnectAsync(context.DnsEndPoint, cancellationToken);
                    }
                    return new System.Net.Sockets.NetworkStream(socket, ownsSocket: true);
                }
            };
            var client = new System.Net.Http.HttpClient(handler);
            client.Timeout = System.TimeSpan.FromSeconds(30);
            return client;
        }

        private string SendRecvSoap(string sUser, string sPwd, string sJgbm,
            string sVersion, string sXxlx, string sSign, string xmlData)
        {
            string url = Constants.GetAppConfigValue("PurchaseWebServiceUrl")
                ?? "http://192.168.20.110/ysxtqx-ws/services/dispatcher";
            string esc(string v) => System.Security.SecurityElement.Escape(v ?? string.Empty);
            string envelope = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                + "<soap:Body>"
                + "<SendRecv xmlns=\"http://ws.framework.shys.wondersgroup.com/\">"
                + "<sUser>" + esc(sUser) + "</sUser>"
                + "<sPwd>" + esc(sPwd) + "</sPwd>"
                + "<sJgbm>" + esc(sJgbm) + "</sJgbm>"
                + "<sVersion>" + esc(sVersion) + "</sVersion>"
                + "<sXxlx>" + esc(sXxlx) + "</sXxlx>"
                + "<sSign>" + esc(sSign) + "</sSign>"
                + "<xmlData>" + esc(xmlData) + "</xmlData>"
                + "</SendRecv>"
                + "</soap:Body>"
                + "</soap:Envelope>";
            var content = new System.Net.Http.StringContent(envelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "\"\"");
            var response = _soapClient.PostAsync(url, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string soapXml = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var doc = new System.Xml.XmlDocument();
            doc.LoadXml(soapXml);
            var node = doc.GetElementsByTagName("return").Item(0);
            if (node == null) throw new System.Exception("Phản hồi SOAP không chứa phần tử return");
            return node.InnerText;
        }

        #endregion


    }
}

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// XML serialize helpers ported from the original Infrastructure
    /// XmlSerializerExt (kept here, inside the owned Purchase service file,
    /// so no other copy file needs to change).
    /// </summary>
    public static class PurchaseXmlSerializerExt
    {
        public static string XmlSerialize(this object ob, System.Text.Encoding encoding = null)
        {
            if (encoding == null) encoding = System.Text.Encoding.UTF8;
            try
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var ser = new System.Xml.Serialization.XmlSerializer(ob.GetType());
                    var ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                    ns.Add(string.Empty, string.Empty);
                    ser.Serialize(ms, ob, ns);
                    return encoding.GetString(ms.GetBuffer()).TrimEnd(new char[1]);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError("[Herp] Xml Serialize error: " + ex);
                return string.Empty;
            }
        }

        public static T XmlDeSerialize<T>(this string xml, System.Text.Encoding encoding = null)
        {
            if (encoding == null) encoding = System.Text.Encoding.UTF8;
            try
            {
                var settings = new System.Xml.XmlReaderSettings { CheckCharacters = false };
                using (var ms = new System.IO.MemoryStream(encoding.GetBytes(xml)))
                using (var reader = System.Xml.XmlReader.Create(ms, settings))
                {
                    return (T)new System.Xml.Serialization.XmlSerializer(typeof(T)).Deserialize(reader);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError("[Herp] Xml Deserialize error: " + ex);
                return default(T);
            }
        }
    }
}
