using System;
using DbConfigHelper;
using Newtouch.CIS.Proxy.ServiceReferenceSqsq;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Newtouch.Domain.Entity;

using Newtouch.CIS.Repository;

using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ValueObjects.Operation;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using System.Net.NetworkInformation;
using System.Xml;
using Newtonsoft.Json;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Net;
using Newtouch.Domain.ValueObjects.Apply;
using Newtouch.Domain.DTO;

namespace Newtouch.DomainServices.Inpatient
{
    public class DoctorserviceDmnService : DmnServiceBase, IDoctorserviceDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientPatientInfoRepo _inpatientPatientInfoRepo;
        private readonly IInpatientLongTermOrderRepo _inpatientLongTermOrderRepo;
        private readonly IInpatientSTATOrderRepo _inpatientSTATOrderRepo;
        private readonly IInpatientMedicinejxCodeComparedRepo _inpatientMedicinejxCodeComparedRepo;
        private readonly IInpatientDietDetailSplitRepo _InpatientDietDetailSplitRepo;
        private readonly IInpatientDietBaseRepo _InpatientDietBaseRepo;
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;
        private readonly IInpatientPatientDoctorRepo _inpatientpatientdoctorRepo;
        private readonly ITTCataloguesComparisonDmnService _tTCataloguesComparisonDmnService;
        private readonly IInpatientLongTermOrderRepo _InpatientLongTermOrderRepo;
        private readonly IQhdZnshSqtxRepo _qhdznshsqtxRepo;
        public DoctorserviceDmnService() : base()
        { }

        #region Y lệnhnhập
        /// <summary>
        /// Y lệnhchuyểnđổithànhDữ liệubảngcủaTrạng thái
        /// </summary>
        /// <param name="orgId"></param>3
        /// <param name="reqdoctorservices"></param>
        public void SubmitdoctorService(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata)
        {
            var dto = new SaveDoctorServiceDto()
            {
                cqyzList = new List<InpatientLongTermOrderEntity>(),
                lsyzList = new List<InpatientSTATOrderEntity>(),
                EditcqyzList = new List<InpatientLongTermOrderEntity>(),
                EditlsyzList = new List<InpatientSTATOrderEntity>(),
                ssyzList = new List<InpatientDietDetailSplitEntity>(),
                DelIds = new List<string>(),
                DelssyzIds = new List<string>()
            };

            try
            {
                if (deldata != null && deldata.Count() > 0)
                {
                    dto.DelIds.AddRange(deldata);
                }
                //thaybảngTạm thờicủaLôtậphợp
                var FrequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", orgId) ?? "";
                var FequencyList = FrequencyStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //Số nhómtạotậphợp keyThao tácgiớimặtmangquađếncủaSố nhóm,valuemáy chủtạocủaSố nhóm,nhấtcuốiLưuđếnDữ liệukho
                var diczh = new Dictionary<int, int>();
                var zhreqdoctorservices = reqdoctorservices.ToJson().ToObject<List<DoctorServiceRequestDto>>();
                if (reqdoctorservices != null && reqdoctorservices.Count > 0)
                {
                    var zyh = reqdoctorservices[0].zyh;
                    var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                    if (patientobj == null)
                    {
                        throw new FailedException("bệnh nhânkhôngtồn tạiở");
                    }

                    foreach (var item in reqdoctorservices)
                    {
                        //bệnh nhânNhập việnthông tinbảngcủaThông tin cơ bảnmangđếnY lệnhbảng
                        item.hzxm = patientobj.xm;
                        item.WardCode = patientobj.WardCode;
                        item.DeptCode = patientobj.DeptCode;
                        //khôngtồn tạiởTần suất,khônglàmphương phápkếtiếp.
                        if (string.IsNullOrWhiteSpace(item.pcCode))
                        {
                            continue;
                        }
                        if (FequencyList != null && FequencyList.Length > 0)
                        {
                            foreach (var fl in FequencyList)
                            {

                                var zhcnt = zhreqdoctorservices.Where(p => p.zh == item.zh).Count();
                                if (zhcnt > 1)
                                {
                                    //Số nhómlạiGán giá trị
                                    if (!string.IsNullOrWhiteSpace(item.zh.ToString()))
                                    {
                                        if (diczh.ContainsKey(item.zh.ToInt()))
                                        {
                                            foreach (KeyValuePair<int, int> zhitem in diczh)
                                            {
                                                if (item.zh == zhitem.Key)
                                                {
                                                    item.zh = zhitem.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //tạoSố nhóm
                                            var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_cqlsyz.zh", orgId, "", false);
                                            diczh.Add(item.zh.ToInt(), No.ToInt());
                                            item.zh = No.ToInt();
                                        }
                                    }
                                }
                                else
                                {//Cùng nhómcnt<1củalời,thànhnhómthất bại
                                    item.zh = null;
                                }

                                //Nội dung y lệnh định dạng:Tên thuốc Quy cách thuốc  ThuốcLiều lượng  Đơn vị liều Mã cách dùng Tên tần suất Thời gian thực hiện Vị trí

                                //item.yznr = "【" + item.xmmc + " "
                                //    + (item.ypgg ?? "")
                                //    + "】 " + (item.ypjl.ToString() ?? "")
                                //    + (item.dwwwwwww ?? "")
                                //    + " " + (item.yfmc ?? "")
                                //    + " " + item.pcmc + " "
                                //    + item.zxsj + (item.bw ?? "");
                                item.yznr = item.xmmc + " "
                                    + (item.ypjl.ToString() ?? "")
                                    + (item.dwwwwwww ?? "")
                                    + " " + (item.yfmc ?? "")
                                    + " " + item.pcmc + " "
                                    + item.zxsj + " " + (item.bw ?? "")
                                    + " " + item.ztnr;
                                if (item.yzlx == (int)EnumYzlx.Cydy)
                                {
                                    item.yznr = item.yznr + " " + item.sl + item.zydw;
                                }
                                if (item.yzlx == (int)EnumYzlx.ssyz)
                                {
                                    if (item.xmmc == "禁食")
                                    {
                                        item.yznr = item.xmmc;
                                    }
                                    else if (item.yslb == "Tự chuẩn bị bữa ăn")
                                    {
                                        item.yznr = item.yslb;
                                    }
                                    else
                                    {
                                        item.yznr = (item.yslb ?? item.xmmc) + "[" + item.yszs + "]" + "  " + (item.ypjl.ToString() ?? "") + " " + (item.nlmd ?? "") + "  " + item.yfmc + "  " + item.pcmc;//thông thườngăn uống
                                    }
                                }
                                var ssyzcfId = "";

                                //Y lệnhđối tượngGán giá trị  
                                if (item.pcCode == fl || item.yzlx == ((int)EnumYzlx.Cydy))//Tạm thờiTần suất
                                {
                                    ssyzcfId = item.Id;
                                    //Dữ liệukhotìmkhôngđếnnàyY lệnhghi chépcủalời,thìThêm mới
                                    if (!EditLSdoctorService(item, orgId, ref dto))
                                    {
                                        var addentity = AddLSdoctorService(item, orgId);
                                        if (addentity != null)
                                        {
                                            ssyzcfId = addentity.Id;
                                            dto.lsyzList.Add(addentity);
                                        }
                                    }
                                }
                                else //Dài hạnTần suất
                                {
                                    //TheoSố ngàytínhraThời gian dừng
                                    var tzsj = new DateTime();
                                    if (!string.IsNullOrWhiteSpace(item.ts.ToString()))
                                    {
                                        tzsj = DateTime.Parse(item.kssj).AddDays(item.ts.ToInt());
                                    }
                                    ssyzcfId = item.Id;
                                    if (!EditCQdoctorService(item, orgId, ref dto, tzsj))
                                    {
                                        var addentity = AddCQdoctorService(item, orgId, tzsj);
                                        if (addentity != null)
                                        {
                                            ssyzcfId = addentity.Id;
                                            dto.cqyzList.Add(addentity);
                                        }
                                    }
                                }

                                if (item.yzlx == (int)EnumYzlx.ssyz)
                                {
                                    item.ssyzcfId = ssyzcfId;
                                    EditSSdoctorService(item, orgId, ref dto);
                                    var addentityList = AddSSdoctorService(item, orgId);
                                    if (addentityList != null)
                                    {
                                        dto.ssyzList.AddRange(addentityList);
                                    }
                                }

                            }
                        }
                        else
                        {
                            throw new FailedException("thiếuTạm thờiTần suấtcấu hình");
                        }
                    }
                }
                SavedoctorService(dto, orgId);
            }
            catch (Exception e)
            {

                throw new FailedException("Y lệnhLưu thất bại，" + e.InnerException);
            }
        }

        /// <summary>
        /// Y lệnhchuyểnđổithànhDữ liệubảngcủaTrạng thái Dài hạn,Tạm thờikhôngđicấu hình,cóBác sĩchínhxemđiều khiển
        /// </summary>
        /// <param name="orgId"></param>3
        /// <param name="reqdoctorservices"></param>
        /// <param name="deldata"></param>
        /// <param name="yzh"></param>
        public void SubmitdoctorServiceV2(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata, out string yzh)
        {
            var dto = new SaveDoctorServiceDto
            {
                cqyzList = new List<InpatientLongTermOrderEntity>(),
                lsyzList = new List<InpatientSTATOrderEntity>(),
                EditcqyzList = new List<InpatientLongTermOrderEntity>(),
                EditlsyzList = new List<InpatientSTATOrderEntity>(),
                ssyzList = new List<InpatientDietDetailSplitEntity>(),
                zyyzzdList = new List<InpatientDiagnosisEntity>(),
                DelIds = new List<string>(),
                DelssyzIds = new List<string>()
            };

            try
            {
                foreach (var mx in reqdoctorservices)
                {
                    if (mx.yfztbm != null)
                    {
                        mx.yfztbs = Guid.NewGuid().ToString();
                    }

                }
                #region Mục thu phínhómhợp
                var sfxmztList = reqdoctorservices.Where(p => p.sfxmzt == "Y" || p.yfztbm != null).ToList();
                reqdoctorservices = reqdoctorservices.Where(p => p.sfxmzt != "Y").ToList();
                if (sfxmztList != null && sfxmztList.Count > 0)
                {

                    foreach (var item in sfxmztList)
                    {
                        var sfmbdm = string.IsNullOrWhiteSpace(item.yfztbm) ? item.xmdm : item.yfztbm;
                        var sfxmguid = Guid.NewGuid().ToString();
                        var zhlist = GetSfxmItem(sfmbdm, orgId);
                        if (zhlist.Count == 0) continue;

                        foreach (var sfxmitem in zhlist)
                        {
                            DoctorServiceRequestDto dto1 = new DoctorServiceRequestDto();
                            dto1 = item.Clone();
                            dto1.Id = "999999999";
                            dto1.ztId = sfxmitem.ztId;
                            dto1.ztmc = sfxmitem.ztmc;
                            dto1.zxksdm = sfxmitem.zxks;
                            dto1.xmdm = sfxmitem.sfxm;
                            dto1.xmmc = sfxmitem.sfxmmc;
                            dto1.ztsl = string.IsNullOrWhiteSpace(dto1.yfztbm) ? dto1.sl : 1;
                            dto1.sl = string.IsNullOrWhiteSpace(dto1.yfztbm) ? sfxmitem.sl * dto1.sl : sfxmitem.sl;
                            dto1.dwwwwwww = sfxmitem.dw;
                            dto1.yzlx = (int)EnumYzlx.sfxm;
                            dto1.ypyfdm = string.Empty;
                            dto1.ypjl = string.IsNullOrWhiteSpace(dto1.yfztbm) ? dto1.ypjl : 1;
                            dto1.ypgg = string.IsNullOrWhiteSpace(dto1.yfztbm) ? dto1.ypgg : string.Empty;
                            dto1.yfmc = string.IsNullOrWhiteSpace(dto1.yfztbm) ? dto1.yfmc : string.Empty;
                            dto1.dcztbs = string.IsNullOrWhiteSpace(dto1.yfztbm) ? sfxmguid : string.Empty;
                            reqdoctorservices.Add(dto1);
                        }
                    }

                }
                #endregion

                #region tạoY lệnhsố
                var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.yzh", orgId);
                yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
                int px = 1;
                #endregion
                if (deldata != null && deldata.Count > 0) dto.DelIds.AddRange(deldata);
                //Số nhómtạotậphợp keyThao tácgiớimặtmangquađếncủaSố nhóm,valuemáy chủtạocủaSố nhóm,nhấtcuốiLưuđếnDữ liệukho
                var diczh = new Dictionary<int, int>();
                var zhreqdoctorservices = reqdoctorservices.ToJson().ToObject<List<DoctorServiceRequestDto>>();
                if (reqdoctorservices != null && reqdoctorservices.Count > 0)
                {
                    var zyh = reqdoctorservices[0].zyh;
                    var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                    if (patientobj == null) throw new FailedException("bệnh nhânkhôngtồn tạiở");

                    //IList<DoctorServiceRequestDto> jyList = reqdoctorservices.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.jy))).ToList();
                    //IList<DoctorServiceRequestDto> jcList = reqdoctorservices.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.jc))).ToList();

                    foreach (var item in reqdoctorservices)
                    {
                        //bệnh nhânNhập việnthông tinbảngcủaThông tin cơ bảnmangđếnY lệnhbảng
                        item.hzxm = patientobj.xm;
                        item.WardCode = patientobj.WardCode;
                        item.DeptCode = patientobj.DeptCode;

                        //if (item.yzlx == ((int)EnumYzlx.jy) || item.yzlx == ((int)EnumYzlx.jc))
                        //{
                        //    var jyjcyzh1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //    var jyjcyzh2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.yzh", orgId);
                        //    var jyjcyzh = string.Format("{0}{1}", jyjcyzh1, jyjcyzh2);
                        //    item.yzh = jyjcyzh;
                        //}
                        //else
                        //{
                        item.yzh = yzh;
                        //}

                        //khôngtồn tạiởTần suất,khônglàmphương phápkếtiếp.
                        if (string.IsNullOrWhiteSpace(item.pcCode)) continue;

                        #region Số nhóm
                        var zhcnt = zhreqdoctorservices.Count(p => p.zh == item.zh);
                        if (zhcnt > 1)
                        {
                            //Số nhómlạiGán giá trị
                            if (!string.IsNullOrWhiteSpace(item.zh.ToString()))
                            {
                                if (diczh.ContainsKey(item.zh.ToInt()))
                                {
                                    foreach (KeyValuePair<int, int> zhitem in diczh)
                                    {
                                        if (item.zh == zhitem.Key)
                                        {
                                            item.zh = zhitem.Value;
                                        }
                                    }
                                }
                                else
                                {
                                    //tạoSố nhóm
                                    var No = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_cqlsyz.zh", orgId, "");
                                    diczh.Add(item.zh.ToInt(), No.ToInt());
                                    item.zh = No.ToInt();
                                }
                            }
                        }
                        else
                        {
                            //Cùng nhómcnt<1củalời,thànhnhómthất bại
                            item.zh = null;
                        }
                        #endregion

                        #region Nội dung y lệnh định dạng：Tên thuốc Quy cách thuốc  Thuốcliều lượng  Đơn vị liều Mã cách dùng Tên tần suất Thời gian thực hiện Vị trí
                        item.yznr = item.xmmc + " "
                            + (item.ypjl.ToString() ?? "")
                            + (item.dwwwwwww ?? "")
                            + " " + (item.yfmc ?? "")
                            + " " + item.pcmc + " "
                            + item.zxsj + " " + (item.bw ?? "")
                            + " " + item.ztnr;
                        if (item.yzlx == (int)EnumYzlx.Cydy)
                        {
                            item.yznr = item.yznr + " " + item.sl + item.zydw;
                        }
                        if ((item.yzlx == (int)EnumYzlx.rehab || item.yzlx == (int)EnumYzlx.sfxm || item.yzlx == (int)EnumYzlx.yyhc) && (item.dwwwwwww ?? "").Length > 2)
                        {
                            item.yznr = item.xmmc + " "
                            + (item.ypjl.ToString() ?? " ") + "  "
                            + (item.dwwwwwww ?? "")
                            + " " + (item.yfmc ?? "")
                            + " " + item.pcmc + " "
                            + item.zxsj + " " + (item.bw ?? "")
                            + " " + item.ztnr;
                        }
                        if (item.yzlx == (int)EnumYzlx.ssyz)
                        {
                            if (item.xmmc == "禁食")
                            {
                                item.yznr = item.xmmc;
                            }
                            else if (item.yslb == "Tự chuẩn bị bữa ăn")
                            {
                                item.yznr = item.yslb;
                            }
                            else
                            {
                                item.yznr = (item.yslb ?? item.xmmc) + "[" + item.yszs + "]" + "  " + (item.ypjl.ToString() ?? "") + " " + (item.nlmd ?? "") + "  " + item.yfmc + "  " + item.pcmc;//thông thườngăn uống
                            }
                        }
                        if (item.yzlx == (int)EnumYzlx.zcy)
                        {
                            //Y lệnhđịnh dạng:Tên thuốc Quy cách mỗi lầnLiều lượng mỗi lầnĐơn vị liều Cách dùng Tần suất dánsố
                            item.yznr = item.xmmc + "  " + item.ypgg + " " + item.ypjl + item.dwwwwwww + " " + item.yfmc + " " + item.pcmc + " " + item.sl + " thang";
                        }
                        if (item.yzlx == (int)EnumYzlx.jy)
                        {
                            item.yznr = item.xmmc + " "
                            + (item.sl.ToString() ?? "")
                            + (item.dwwwwwww ?? "")
                            + " " + item.pcmc + " "
                            + item.zxsj
                            + " " + item.ztnr;
                        }
                        #endregion
                        var uuid = Guid.NewGuid().ToString();
                        //thaybảngTạm thờicủaLôtậphợp
                        var frequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", orgId) ?? "";
                        var frequencyLs = (frequencyStr.Split(',') ?? new string[0]).ToList();
                        string ssyzcfId;
                        //Y lệnhđối tượngGán giá trị  
                        if ("临".Equals(item.yzlb) || item.yzlx == (int)EnumYzlx.Cydy || item.yzlx == (int)EnumYzlx.zcy || frequencyLs.Exists(p => p == item.pcCode))// Tần suất tạm thời
                        {
                            ssyzcfId = item.Id;
                            //Dữ liệukhotìmkhôngđếnnàyY lệnhghi chépcủalời,thìThêm mới
                            if (!EditLSdoctorService(item, orgId, ref dto))
                            {
                                var addentity = AddLSdoctorService(item, orgId, uuid);
                                if (addentity != null)
                                {
                                    ssyzcfId = addentity.Id;
                                    dto.lsyzList.Add(addentity);
                                }
                            }
                        }
                        else//Dài hạnTần suất
                        {
                            //TheoSố ngàytínhraThời gian dừng
                            var tzsj = new DateTime();
                            if (!string.IsNullOrWhiteSpace(item.ts.ToString()))
                            {
                                tzsj = DateTime.Parse(item.kssj).AddDays(item.ts.ToInt());
                            }
                            ssyzcfId = item.Id;
                            if (!EditCQdoctorService(item, orgId, ref dto, tzsj))
                            {
                                var addentity = AddCQdoctorService(item, orgId, uuid, tzsj);
                                if (addentity != null)
                                {
                                    ssyzcfId = addentity.Id;
                                    dto.cqyzList.Add(addentity);
                                }
                            }
                        }
                        var addentityListzyyzzd = AddZyZdService(item, orgId, px, uuid);
                        if (addentityListzyyzzd != null)
                        {
                            dto.zyyzzdList.AddRange(addentityListzyyzzd);
                        }
                        px++;
                        if (item.yzlx != (int)EnumYzlx.ssyz) continue;
                        item.ssyzcfId = ssyzcfId;
                        EditSSdoctorService(item, orgId, ref dto);
                        var addentityList = AddSSdoctorService(item, orgId);
                        if (addentityList != null)
                        {
                            dto.ssyzList.AddRange(addentityList);
                        }

                    }
                }
                SavedoctorService(dto, orgId);

            }
            catch (Exception e)
            {

                throw new FailedException("Y lệnhLưu thất bại，" + e.InnerException);
            }
        }
        #region Y lệnhnhắc trước
        /// <summary>
        /// LấythứbaphươngđốichiếuGiá trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="TTcode"></param>
        /// <returns></returns>
        public string GetTTItem(string orgId, string TTtype, string TTcode, string txtype)
        {
            FirstSecondThird TTobj = new FirstSecondThird();
            try
            {
                TTobj = _tTCataloguesComparisonDmnService.GetTTItem(orgId, TTtype, TTcode, txtype);
                return TTobj.First;
            }
            catch (Exception er)
            {
            }
            switch (TTtype)
            {
                case "fylb":
                    return "91";//KhácChi phí
                case "sypc":
                    return "99";
                case "yf":
                    return "9";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// LấyY lệnhnhắc trướcDữ liệu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetqhdyzSqtxData(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx, string rygh, string username, string yzcfh, out string jlId)
        {
            var zyh = reqdoctorservices[0].zyh;
            var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
            var zlysobj = _inpatientpatientdoctorRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
            QhdPrescriptionDTO pre = new QhdPrescriptionDTO();
            REQUESTDATA request = new REQUESTDATA();
            KC21XML kc21 = new KC21XML();
            List<KC22ROW> kc22list = new List<KC22ROW>();
            List<KC33ROW> kc33list = new List<KC33ROW>();
            OrganizationData orgInfo = _medicalRecordDmnService.GetOrgInfo(orgId);

            #region gốctiếtđiểmlắp ráp
            request.AKB020 = orgInfo.orgCode;
            request.AKB021 = orgInfo.orgName;
            request.MSGNO = "5100";
            request.MSGID = orgInfo.orgCode + DateTime.Now.ToString("yyyyMMddHHmmss");
            request.GRANTID = string.Empty;
            request.OPERID = rygh;
            request.OPERNAME = username;
            #endregion

            #region kc21Lắp ráp dữ liệu nút
            kc21.AKC190 = zyh;
            kc21.AKA130 = "21";
            kc21.AKC192 = patientobj.ryrq.ToString("yyyyMMddHHmmss");
            kc21.AKC193 = patientobj.zddm != null ? patientobj.zddm : " ";
            kc21.ZKC274 = patientobj.zdmc != null ? patientobj.zdmc : " ";
            kc21.AKC194 = string.Empty;
            kc21.AKC195 = string.Empty;
            kc21.AKC196 = string.Empty;
            kc21.ZKC275 = string.Empty;
            kc21.ZKC285 = string.Empty;
            kc21.ZKC286 = string.Empty;
            kc21.BKF040 = patientobj.DeptCode;
            kc21.ZKC272 = string.Empty;
            kc21.BKF050 = zlysobj.ysgh;
            kc21.ZKC271 = zlysobj.ysmc;
            kc21.AAC001 = patientobj.grbh;
            kc21.CKC502 = patientobj.cardno;
            kc21.AAC003 = patientobj.xm;
            kc21.AAE135 = patientobj.sfzh;
            kc21.AAC004 = patientobj.sex;
            kc21.BAE450 = Convert.ToDecimal(brxx.age);
            kc21.AAE011 = patientobj.CreatorCode;
            kc21.AAE036 = DateTime.Now.ToString("yyyyMMddHHmmss");
            request.KC21XML = kc21;

            #endregion

            #region kc22Dữ liệu nút
            var sfdlcode = "";
            FirstSecondThird fylbobj = new FirstSecondThird();
            FirstSecondThird dwobj = new FirstSecondThird();
            foreach (var item in reqdoctorservices)
            {
                KC22ROW row = new KC22ROW();
                row.AKC190 = zyh;
                row.AKC220 = yzcfh;
                row.AKC221 = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (item.yzlx == (int)EnumYzlx.Yp || item.yzlx == (int)EnumYzlx.Cydy || item.yzlx == (int)EnumYzlx.zcy)
                {
                    SqtxXmYpInfoVO ypInfo = _medicalRecordDmnService.GetXmYpInfo(item.xmdm, "2", orgId);
                    sfdlcode = ypInfo.sfdlCode;
                    row.AKE001 = ypInfo.ybdm;
                    row.AKC224 = "1";
                    row.AKC225 = ypInfo.dj;
                    row.AKC227 = System.Decimal.Round((ypInfo.dj * item.sl), 4);
                }
                else
                {
                    SqtxXmYpInfoVO xmInfo = _medicalRecordDmnService.GetXmYpInfo(item.xmdm, "1", orgId);
                    sfdlcode = xmInfo.sfdlCode;
                    row.AKE001 = xmInfo.ybdm;
                    row.AKC224 = "2";
                    row.AKC225 = xmInfo.dj;
                    row.AKC227 = System.Decimal.Round((xmInfo.dj * item.sl), 4);
                }
                row.AKE002 = item.xmmc;
                row.AKC515 = item.xmdm;
                row.AKC223 = item.xmmc;
                row.AKA063 = GetTTItem(orgId, "fylb", sfdlcode, "sqtx");//KhácChi phí 
                row.AKC226 = Convert.ToDecimal(item.sl);
                row.AKA070 = string.Empty;
                row.CKC132 = GetTTItem(orgId, "jldw", item.dwwwwwww, "sqtx");
                row.AKA074 = item.ypgg;
                row.AKA071 = item.ypjl;
                row.AKA075 = item.ypjl;
                row.AKA072 = GetTTItem(orgId, "sypc", item.pcCode, "sqtx");
                row.AKA073 = GetTTItem(orgId, "yf", item.ypyfdm, "sqtx");
                row.ZKA101 = GetTTItem(orgId, "xsdw", item.zydw, "sqtx");
                row.AKA067 = "1";
                row.AKC229 = 1;
                row.BKC127 = string.Empty;
                row.BKF050 = rygh;
                row.ZKC271 = username;
                row.AAE011 = patientobj.CreatorCode;
                row.AAE036 = DateTime.Now.ToString("yyyyMMddHHmmss");

                kc22list.Add(row);
            }
            request.KC22XML = kc22list;
            #endregion

            #region kc33Dữ liệu nút
            KC33ROW xyrow = new KC33ROW();
            xyrow.AKC190 = zyh;
            xyrow.BKE150 = 1;
            xyrow.CKC305 = "1";
            xyrow.CKC304 = DateTime.Now.ToString("yyyyMMddHHmmss");
            xyrow.CKC302 = patientobj.zddm;
            xyrow.CKC303 = patientobj.zdmc;
            xyrow.BKF050 = rygh;
            xyrow.ZKC271 = username;
            xyrow.AAE013 = string.Empty;
            kc33list.Add(xyrow);
            request.KC33XML = kc33list;
            #endregion

            pre.REQUESTDATA = request;
            string responsexml = pre.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            var qhdznshentity = new QhdZnshSqtxEntity
            {
                OrganizeId = orgId,
                jydm = "5100",
                mzzyh = zyh,
                Type = "2",
                XmlRequest = responsexml,
                zt = "1",
            };
            var rzid = "";
            _qhdznshsqtxRepo.SubmitForm(qhdznshentity, out rzid);
            jlId = rzid;
            return responsexml;
        }
        #endregion
        /// <summary>
        /// tăng thêmY lệnhChẩn đoán
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="px"></param>
        /// <returns></returns>
        List<InpatientDiagnosisEntity> AddZyZdService(DoctorServiceRequestDto item, string orgId, int px, string guid)
        {
            var list = new List<InpatientDiagnosisEntity>();
            if (item != null && item.yzzdid != null)
            {
                var entity = new InpatientDiagnosisEntity();
                entity.Create(true);
                entity.OrganizeId = orgId;
                entity.yzId = guid;
                entity.yzh = item.yzh;
                entity.zyh = item.zyh;
                entity.zdsj = DateTime.Parse(item.yzzdsj);
                entity.icd10 = item.yzzdid;
                entity.zdmc = item.yzzdmc;
                entity.yztag = item.yztag;
                entity.yzpx = px.ToString();
                entity.yzlx = item.yzlx;
                if ("临".Equals(item.yzlb))
                {
                    entity.yzxz = "临";
                }
                else
                {
                    entity.yzxz = "长";
                }
                entity.zt = "1";
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// SửaY lệnh tạm thời
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool EditLSdoctorService(DoctorServiceRequestDto item, string orgId, ref SaveDoctorServiceDto dto)
        {
            var edititem = _inpatientSTATOrderRepo.FindEntity(p => p.Id == item.Id && p.zt == "1");
            if (edititem != null)//SửaY lệnh tạm thời
            {
                //ThuốcSửa Cách dùngtương ứngBộ mẫutrướcHủy bỏởtạo
                if (edititem.yfztbs != null)
                {
                    var lsyzzt = _inpatientSTATOrderRepo.IQueryable(p => p.yfztbs == edititem.yfztbs && p.zyh == edititem.zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();
                    if (lsyzzt.Count > 0)
                    {
                        foreach (var delztId in lsyzzt)
                        {
                            dto.DelIds.Add(delztId.Id);
                        }
                    }
                }
                if (edititem.xmdm != item.xmdm)
                {
                    throw new FailedException("không thểSửaThuốc");
                }

                var editlsyz = edititem.Clone();
                editlsyz.zh = item.zh;
                editlsyz.pcCode = item.pcCode;
                editlsyz.zxcs = item.zxcs;
                editlsyz.zxzq = item.zxzq;
                editlsyz.zxzqdw = item.zxzqdw;
                editlsyz.zdm = item.zdm;
                editlsyz.zbbz = item.zbbzzzzzzz;
                editlsyz.dw = item.dwwwwwww;
                editlsyz.dwlb = item.dwlb.ToInt();
                editlsyz.zbbz = item.zbbzzzzzzz;
                editlsyz.ypjl = item.ypjl;
                editlsyz.ztnr = item.ztnr;
                editlsyz.yznr = item.yznr;
                editlsyz.ypyfdm = item.ypyfdm;
                editlsyz.kssj = DateTime.Parse(item.kssj);
                editlsyz.sl = item.sl;
                editlsyz.ztId = item.ztId;
                editlsyz.ztmc = item.ztmc;
                editlsyz.zxksdm = item.zxksdm;
                editlsyz.nlmddm = item.nlmddm;
                editlsyz.djbz = item.djbzzzzzzz;
                editlsyz.cydybz = item.cydybzzzzzzz;
                editlsyz.zzfbz = item.iszzffffff;
                editlsyz.isjf = item.isjffffff;
                editlsyz.yztag = item.yztag;
                editlsyz.ispscs = item.ispscsfffff.ToString();
                editlsyz.yfztbs = item.yfztbs;
                editlsyz.yzh = item.yzh;
                editlsyz.Modify(item.Id);
                _inpatientSTATOrderRepo.DetacheEntity(edititem);
                dto.EditlsyzList.Add(editlsyz);
                return true;

            }

            //từDài hạnchuyểnđếnY lệnh tạm thời trước đâyY lệnh dài hạnCài đặtthànhVô hiệu,lạiThêm mớiY lệnh tạm thời
            var edititemtrans = _inpatientLongTermOrderRepo.FindEntity(p => p.Id == item.Id && p.zt == "1");
            if (edititemtrans == null) return false;
            if (edititemtrans.xmdm != item.xmdm)
            {
                throw new FailedException("không thểSửaThuốc");
            }
            dto.DelIds.Add(item.Id);
            dto.lsyzList.Add(AddLSdoctorService(item, orgId));
            return true;
        }
        public InpatientSTATOrderEntity AddLSdoctorService(DoctorServiceRequestDto item, string orgId)
        {
            return AddLSdoctorService(item, orgId, null);
        }
        /// <summary>
        /// tăng thêmY lệnh tạm thời
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        InpatientSTATOrderEntity AddLSdoctorService(DoctorServiceRequestDto item, string orgId, string guid)
        {
            var lsyz = item.MapperTo<DoctorServiceRequestDto, InpatientSTATOrderEntity>();
            lsyz.dw = item.dwwwwwww;
            lsyz.zbbz = item.zbbzzzzzzz;
            lsyz.OrganizeId = orgId;
            lsyz.kssj = DateTime.Parse(item.kssj);
            lsyz.nlmddm = item.nlmddm;
            lsyz.djbz = item.djbzzzzzzz;
            lsyz.cydybz = item.cydybzzzzzzz;
            lsyz.zzfbz = item.iszzffffff;
            lsyz.isjf = item.isjffffff;
            lsyz.yztag = item.yztag;
            lsyz.ispscs = item.ispscsfffff.ToString();
            if (guid == null || guid == "")
            {
                lsyz.Create(true);
            }
            else
            {
                lsyz.Create(true, guid);
            }
            return lsyz;
        }

        /// <summary>
        /// SửaY lệnh dài hạn
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="tzsj"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool EditCQdoctorService(DoctorServiceRequestDto item, string orgId, ref SaveDoctorServiceDto dto, DateTime? tzsj = null)
        {
            var Edititem = _inpatientLongTermOrderRepo.FindEntity(p => p.Id == item.Id && p.zt == "1");
            if (Edititem != null)//SửaY lệnh dài hạn
            {
                if (item.yzlx != (int)EnumYzlx.ssyz && Edititem.xmdm != item.xmdm)
                {
                    throw new FailedException("không thểSửaThuốc");
                }
                //ThuốcSửa Cách dùngtương ứngBộ mẫutrướcHủy bỏởtạo
                if (Edititem.yfztbs != null)
                {
                    var cqyzzt = _inpatientLongTermOrderRepo.IQueryable(p => p.yfztbs == Edititem.yfztbs && p.zyh == Edititem.zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();
                    if (cqyzzt.Count > 0)
                    {
                        foreach (var delztId in cqyzzt)
                        {
                            dto.DelIds.Add(delztId.Id);
                        }
                    }
                }

                var Editcqyz = Edititem.Clone();
                Editcqyz.zh = item.zh;
                Editcqyz.pcCode = item.pcCode;
                Editcqyz.zxcs = item.zxcs;
                Editcqyz.zxzq = item.zxzq;
                Editcqyz.zxzqdw = item.zxzqdw;
                Editcqyz.zdm = item.zdm;
                Editcqyz.zbbz = item.zbbzzzzzzz;
                Editcqyz.dw = item.dwwwwwww;
                Editcqyz.dwlb = item.dwlb.ToInt();
                Editcqyz.zbbz = item.zbbzzzzzzz;
                Editcqyz.ypjl = item.ypjl;
                Editcqyz.ztnr = item.ztnr;
                Editcqyz.yznr = item.yznr;
                Editcqyz.kssj = DateTime.Parse(item.kssj);
                if (tzsj != DateTime.MinValue)
                {
                    Editcqyz.tzsj = tzsj;
                    Editcqyz.tzr = Editcqyz.ysgh;
                }
                Editcqyz.ypyfdm = item.ypyfdm;
                Editcqyz.sl = item.sl;
                Editcqyz.zxksdm = item.zxksdm;
                Editcqyz.nlmddm = item.nlmddm;
                Editcqyz.zzfbz = item.iszzffffff;
                Editcqyz.ispscs = item.ispscsfffff.ToString();
                Editcqyz.isjf = item.isjffffff;
                Editcqyz.yztag = item.yztag;
                Editcqyz.ztId = item.ztId;
                Editcqyz.ztmc = item.ztmc;
                Editcqyz.yfztbs = item.yfztbs;
                Editcqyz.yzh = item.yzh;
                Editcqyz.Modify(item.Id);
                _inpatientLongTermOrderRepo.DetacheEntity(Edititem);
                dto.EditcqyzList.Add(Editcqyz);
                return true;
            }
            else
            {//từY lệnh tạm thờichuyểnđếnY lệnh dài hạn trước đâyY lệnh tạm thờiCài đặtthànhVô hiệu,lạiThêm mớiY lệnh dài hạn
                var Edititemtrans = _inpatientSTATOrderRepo.FindEntity(p => p.Id == item.Id && p.zt == "1");
                if (Edititemtrans != null)
                {
                    if (Edititemtrans.xmdm != item.xmdm)
                    {
                        throw new FailedException("không thểSửaThuốc");
                    }
                    dto.DelIds.Add(item.Id);
                    dto.cqyzList.Add(AddCQdoctorService(item, orgId, tzsj));
                    return true;
                }
                return false;
            }
        }
        public InpatientLongTermOrderEntity AddCQdoctorService(DoctorServiceRequestDto item, string orgId, DateTime? tzsj = null)
        {
            return AddCQdoctorService(item, orgId, null, tzsj);
        }
        /// <summary>
        /// tăng thêmY lệnh dài hạn
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="tzsj"></param>
        /// <returns></returns>
        InpatientLongTermOrderEntity AddCQdoctorService(DoctorServiceRequestDto item, string orgId, string uuid, DateTime? tzsj = null)
        {
            var cqyz = item.MapperTo<DoctorServiceRequestDto, InpatientLongTermOrderEntity>();
            cqyz.dw = item.dwwwwwww;
            cqyz.zbbz = item.zbbzzzzzzz;
            cqyz.zzfbz = item.iszzffffff;
            cqyz.ispscs = item.ispscsfffff.ToString();
            cqyz.isjf = item.isjffffff;
            cqyz.yztag = item.yztag;
            cqyz.OrganizeId = orgId;
            cqyz.kssj = DateTime.Parse(item.kssj);
            if (tzsj != DateTime.MinValue)
            {
                cqyz.tzsj = tzsj;
                cqyz.tzr = cqyz.ysgh;
            }
            cqyz.nlmddm = item.nlmddm;
            if (uuid == null || uuid == "")
            {
                cqyz.Create(true);
            }
            else
            {
                cqyz.Create(true, uuid);
            }
            return cqyz;
        }

        /// <summary>
        /// Sửaăn uốngY lệnh
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool EditSSdoctorService(DoctorServiceRequestDto item, string orgId, ref SaveDoctorServiceDto dto)
        {

            var Edititem = _InpatientDietDetailSplitRepo.IQueryable().Where(p => p.Id == item.Id && p.zt == "1" && p.OrganizeId == orgId);
            if (Edititem != null)//Sửaăn uốngY lệnh
            {
                dto.DelssyzIds.Add(item.Id);
                return true;
            }
            return false;
        }
        /// <summary>
        /// tăng thêmăn uốngY lệnh
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="tzsj"></param>
        /// <returns></returns>
        List<InpatientDietDetailSplitEntity> AddSSdoctorService(DoctorServiceRequestDto item, string orgId)
        {
            var list = new List<InpatientDietDetailSplitEntity>();
            if (item != null && !string.IsNullOrWhiteSpace(item.yszsval))
            {
                var ssIds = item.yszsval.Split(',');
                if (ssIds != null)
                {
                    for (int i = 0; i < ssIds.Length - 1; i++)
                    {
                        var v = ssIds[i];
                        var nameentity = _InpatientDietBaseRepo.FindEntity(p => p.Id == v && p.zt == "1" && p.OrganizeId == orgId);
                        if (nameentity == null)
                        {
                            throw new FailedException("thiếuăn uốngcơ sởcơ sở");
                        }
                        var entity = new InpatientDietDetailSplitEntity();
                        entity.OrganizeId = orgId;
                        entity.MainId = item.ssyzcfId;
                        entity.BaseId = ssIds[i];
                        entity.Name = nameentity.Name;
                        entity.sslb = item.yslbdm;
                        entity.zt = "1";
                        entity.Create(true);
                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// GửiY lệnhđếnDữ liệukho
        /// </summary>
        /// <param name="dto"></param>
        void SavedoctorService(SaveDoctorServiceDto dto, string orgId)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    if (dto != null)
                    {
                        if (dto.lsyzList != null && dto.lsyzList.Count > 0)
                        {
                            foreach (var item in dto.lsyzList)
                            {
                                db.Insert(item);
                            }
                        }
                        if (dto.cqyzList != null && dto.cqyzList.Count > 0)
                        {
                            foreach (var item in dto.cqyzList)
                            {
                                db.Insert(item);
                            }
                        }
                        if (dto.EditlsyzList != null && dto.EditlsyzList.Count > 0)
                        {
                            foreach (var item in dto.EditlsyzList)
                            {
                                db.Update(item);
                            }
                        }
                        if (dto.EditcqyzList != null && dto.EditcqyzList.Count > 0)
                        {
                            foreach (var item in dto.EditcqyzList)
                            {
                                db.Update(item);
                            }
                        }
                        if (dto.EditlsyzList != null && dto.EditlsyzList.Count > 0)
                        {
                            foreach (var item in dto.EditlsyzList)
                            {
                                db.Update(item);
                            }
                        }
                        if (dto.EditcqyzList != null && dto.EditcqyzList.Count > 0)
                        {
                            foreach (var item in dto.EditcqyzList)
                            {
                                db.Update(item);
                            }
                        }
                        //vậtlýXóa
                        if (dto.DelIds != null && dto.DelIds.Count() > 0)
                        {
                            var sql = @"UPDATE  dbo.zy_cqyz
                                SET     zt = 0
                                WHERE   Id IN ( SELECT  *
                                                FROM    dbo.f_split(@str, ',') )
                                        AND OrganizeId = @orgId;
                                UPDATE  dbo.zy_lsyz
                                SET     zt = 0
                                WHERE   Id IN ( SELECT  *
                                                FROM    dbo.f_split(@str, ',') )
                                        AND OrganizeId = @orgId;";

                            db.ExecuteSqlCommand(sql, new[] { new SqlParameter("@orgId", orgId),
                            new SqlParameter("@str", string.Join(",", dto.DelIds))});
                        }
                        if (dto.DelssyzIds != null && dto.DelssyzIds.Count() > 0)
                        {
                            var sql = @"UPDATE  zy_DietServicecf
                                SET     zt = 0
                                WHERE   MainId IN ( SELECT  *
                                                    FROM    dbo.f_split(@str, ',') )
                                        AND OrganizeId = @orgId;";
                            db.ExecuteSqlCommand(sql, new[] { new SqlParameter("@orgId", orgId),
                            new SqlParameter("@str", string.Join(",", dto.DelssyzIds))});
                        }
                    }
                    //Cập nhậtDữ liệukhocủaSố nhóm
                    db.ExecuteSqlCommand("exec dbo.refreshZH @orgId", new[] { new SqlParameter("@orgId", orgId) });

                    if (dto.ssyzList != null && dto.ssyzList.Count > 0)
                    {
                        foreach (var item in dto.ssyzList)
                        {
                            db.Insert(item);
                        }
                    }
                    if (dto.zyyzzdList != null && dto.zyyzzdList.Count > 0)
                    {
                        foreach (var item in dto.zyyzzdList)
                        {
                            db.Insert(item);
                        }
                    }
                    db.Commit();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        /// <summary>
        /// Y lệnhsao chépTạo mới
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="yzlb"></param>
        /// <param name="zyh"></param>
        public void RegistTzyz(string yzId, string OrganizeId, string yzlb, string zyh)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var yzzt = Convert.ToInt32(EnumYzzt.TZ);
                if (yzlb == "临")
                {
                    var lsyz = db.FindEntity<InpatientSTATOrderEntity>(p => p.zyh == zyh && p.Id == yzId && p.yzzt == yzzt && p.OrganizeId == OrganizeId && p.zt == "1");
                    if (lsyz == null)
                        throw new FailedException("chưatìmđếnY lệnhhoặcY lệnhkhôngDừngTrạng thái");
                    List<InpatientSTATOrderEntity> lsyzlist = new List<InpatientSTATOrderEntity>();
                    if (!string.IsNullOrWhiteSpace(lsyz.dcztbs))
                    {
                        lsyzlist = _inpatientSTATOrderRepo.IQueryable(p => p.zyh == zyh && p.dcztbs == lsyz.dcztbs && p.yzzt == yzzt && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
                    }
                    else
                    {
                        lsyzlist.Add(lsyz);
                    }
                    foreach (var item in lsyzlist)
                    {
                        var lsyznew = item.Clone();
                        lsyznew.Id = null;
                        lsyznew.yzzt = Convert.ToInt32(EnumYzzt.Ds);
                        lsyznew.zh = null;
                        lsyznew.zfr = null;
                        lsyznew.zfsj = null;
                        lsyznew.zfysgh = null;
                        lsyznew.shr = null;
                        lsyznew.shsj = null;
                        lsyznew.kssj = DateTime.Now;
                        lsyznew.zxsj = null;
                        lsyznew.zxr = null;
                        var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.yzh", OrganizeId);
                        lsyznew.yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
                        lsyznew.Create(true);
                        db.Insert(lsyznew);
                    }

                }
                else
                {
                    var cqyz = db.FindEntity<InpatientLongTermOrderEntity>(p => p.zyh == zyh && p.Id == yzId && p.yzzt == yzzt && p.OrganizeId == OrganizeId && p.zt == "1");
                    if (cqyz == null)
                        throw new FailedException("chưatìmđếnY lệnhhoặcY lệnhkhôngDừngTrạng thái");
                    List<InpatientLongTermOrderEntity> cqyzlist = new List<InpatientLongTermOrderEntity>();
                    if (!string.IsNullOrWhiteSpace(cqyz.dcztbs))
                    {
                        cqyzlist = _inpatientLongTermOrderRepo.IQueryable(p => p.zyh == zyh && p.dcztbs == cqyz.dcztbs && p.yzzt == yzzt && p.OrganizeId == OrganizeId && p.zt == "1").ToList();
                    }
                    else
                    {
                        cqyzlist.Add(cqyz);
                    }
                    foreach (var item in cqyzlist)
                    {
                        var cqyznew = item.Clone();
                        cqyznew.Id = null;
                        cqyznew.yzzt = Convert.ToInt32(EnumYzzt.Ds);
                        cqyznew.zh = null;
                        cqyznew.tzysgh = null;
                        cqyznew.tzsj = null;
                        cqyznew.tzr = null;
                        cqyznew.tzyy = null;
                        cqyznew.shr = null;
                        cqyznew.shsj = null;
                        cqyznew.kssj = DateTime.Now;
                        cqyznew.zxsj = null;
                        cqyznew.zxr = null;
                        var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_cqyz.yzh", OrganizeId);
                        cqyznew.yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
                        cqyznew.Create(true);
                        db.Insert(cqyznew);
                    }

                }
                db.Commit();
            }
        }
        #endregion

        #region Y lệnhmởlập

        /// <summary>
        /// LấykhingàyHiệu lựcY lệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<CurrentViewVO> GetTodayValidService(Pagination pagination, string orgId, string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("Số nhập việnlàtrống");
            }
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"SELECT  aaaaaa.yzId,
                                    aaaaaa.clbz ,
                                    aaaaaa.kssj ,
                                    aaaaaa.yznr ,
                                    aaaaaa.tzsj ,
                                    aaaaaa.yzzt ,
                                    aaaaaa.zh ,
                                    u.Name lrz
                            FROM    ( SELECT   Id yzId,
                                                '长' clbz ,
                                                kssj ,
                                                CreatorCode ,
                                                ( ISNULL(yznr,'') + ' ' + ISNULL(ztnr, '') ) yznr ,
                                                tzsj ,
                                                yzzt ,
                                                zh
                                      FROM      dbo.zy_cqyz
                                      WHERE     ( yzzt != 3
                                                  AND yzzt != 4
                                                )
                                                AND zyh = @zyh
                                                AND OrganizeId = @orgId
                                                AND zt = '1'
                                      UNION ALL
                                      SELECT     Id yzId,
                                                '临' clbz ,
                                                kssj ,
                                                CreatorCode ,
                                                ( ISNULL(yznr,'') + ' ' + ISNULL(ztnr, '') ) yznr,
                                                null tzsj ,
                                                yzzt ,
                                                zh
                                      FROM      dbo.zy_lsyz
                                      WHERE     ( yzzt != 3
                                                  AND yzzt != 4
                                                )
                                                AND zyh = @zyh
                                                AND CONVERT(VARCHAR(100), kssj, 111) = CONVERT(VARCHAR(100), GETDATE(), 111)
                                                AND OrganizeId = @orgId
                                                AND zt = '1'
                                    ) aaaaaa
                                    LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff u ON u.Account = aaaaaa.CreatorCode
                                                                                       AND u.OrganizeId = @orgId");
            par.Add(new SqlParameter("@zyh", zyh));
            par.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<CurrentViewVO>(sqlstr.ToString(), pagination, par.ToArray());
        }
        /// <summary>
        /// kiểm traY lệnhHiệu lựctính
        /// </summary>
        /// <param name="req"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<DSrepeatResponseDto> DoctorserviceValidate(IList<DSrepeatRequestDto> req, string zyh, string orgId)
        {
            var response = new List<DSrepeatResponseDto>();
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@orgId", orgId));
            inParameters.Add(new SqlParameter("@zyh", zyh));
            inParameters.Add(new SqlParameter("@yzpartable", SqlDbType.Structured) { Value = req.ToDataTable(), TypeName = "yzpartable" });
            return FindList<DSrepeatResponseDto>("exec doctorservicevalidate @zyh,@orgId,@yzpartable", inParameters.ToArray());
        }
        /// <summary>
        /// Tần suấtMãchuyểnđổithànhdài hạn/tạm thờiCờ
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<DSrepeatRequestDto> DSTransferCL(IList<DSrepeatRequestVO> req, string orgId)
        {
            var response = new List<DSrepeatRequestDto>();
            if (req != null && req.Count() > 0)
            {
                //thaybảngTạm thờicủaLôtậphợp
                var FrequencyStr = _sysConfigRepo.GetValueByCode("FrequencyStr", orgId) ?? "";
                var FequencyList = FrequencyStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in req)
                {
                    var entity = new DSrepeatRequestDto() { clbz = 1 };
                    entity = item.MapperTo<DSrepeatRequestVO, DSrepeatRequestDto>();
                    if (FequencyList.Contains(item.pccode))
                    {
                        entity.clbz = 0;//Tạm thời
                    }
                    else
                    {
                        entity.clbz = 1;//Dài hạn
                    }
                    response.Add(entity);
                }
            }

            return response;
        }

        /// <summary>
        /// TheoTần suấtCách dùngMã LấyTần suấtTên cách dùng LấyThuốccủatínhlượngĐơn vịvàĐơn vị nhập viện,Dạng bào chế
        /// </summary>
        /// <param name="pcdm"></param>
        /// <param name="yfdm"></param>
        /// <param name="pcmc"></param>
        /// <param name="yfmc"></param>
        public void GetypPredata(string pcdm, string yfdm, string ypdm, string dwlb, string orgId, ref string pcmc, ref string yfmc, ref string redundant_jldw, ref string jx, ref decimal? jlzhxs, ref decimal? zyzhxs, ref string qzfs)
        {
            pcmc = "";
            yfmc = "";
            redundant_jldw = "";
            jx = "";
            jlzhxs = null;
            zyzhxs = null;
            if (!string.IsNullOrWhiteSpace(yfdm))
            {
                //LấyCách dùng
                var par = new List<SqlParameter>();
                var yfsql = new StringBuilder();
                yfsql.Append(@"SELECT  yf.yfmc
                            FROM    NewtouchHIS_Base..V_S_xt_ypyf yf
                            WHERE   yf.yfCode = @yfcode
                                    AND zt = '1'");
                par.Add(new SqlParameter("@yfcode", yfdm));

                yfmc = FirstOrDefault<string>(yfsql.ToString(), par.ToArray());
            }

            if (!string.IsNullOrWhiteSpace(pcdm))
            {
                //LấyTần suất
                var parpc = new List<SqlParameter>();
                var pcsql = new StringBuilder();
                pcsql.Append(@"SELECT  pc.yzpcmc
                            FROM    NewtouchHIS_Base..V_S_xt_yzpc pc
                            WHERE   pc.yzpccode = @pccode
                                    AND zt = '1'");
                parpc.Add(new SqlParameter("@pccode", pcdm));
                pcmc = this.FirstOrDefault<string>(pcsql.ToString(), parpc.ToArray());
            }

            if (!string.IsNullOrWhiteSpace(ypdm) && !string.IsNullOrWhiteSpace(dwlb))
            {
                //LấyĐơn vị thuốchộp thả xuốngcủaGiá trị
                var paryp = new List<SqlParameter>();
                var ypsql = new StringBuilder();
                //Đơn vịLoạilàĐơn vị liềukhi,lấyĐơn vị nhập viện,phảncủalấyĐơn vị liều
                ypsql.Append(@"SELECT  ( CASE @dwlb
                            WHEN '1' THEN zycldw
                            WHEN 4 THEN jldw
                          END ) dw,jx,
                            jl jlzhxs ,
                            zycls zyzhxs,
                            (CASE jzlx
                              WHEN 1 THEN 'day'
                              WHEN 2 THEN 'times'
                              ELSE 'times'
                            END) qzfs
                FROM    NewtouchHIS_Base..V_C_xt_yp
                WHERE   OrganizeId = @orgId
                        AND ypCode = @ypdm
                        AND zt = '1';");
                paryp.Add(new SqlParameter("@ypdm", ypdm));
                paryp.Add(new SqlParameter("@orgId", orgId));
                paryp.Add(new SqlParameter("@dwlb", dwlb));
                var data = this.FirstOrDefault<MedicineInfoDto>(ypsql.ToString(), paryp.ToArray());
                if (data != null)
                {
                    redundant_jldw = data.dw;
                    jx = data.jx;
                    jlzhxs = data.jlzhxs;
                    zyzhxs = data.zyzhxs;
                    qzfs = data.qzfs;

                }
            }

        }

        /// <summary>
        /// LấySố lượng đơn vị tính
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <param name="orgId"></param>
        /// <param name="dwjls"></param>
        public void GetdwjlsBysfxmCode(string sfxmCode, string orgId, ref int? dwjls,ref decimal? dj)
        {
            if (!string.IsNullOrWhiteSpace(sfxmCode))
            {
                //LấySố lượng đơn vị tính
                var sqlpar = new List<SqlParameter>();
                var par = new StringBuilder();
                par.Append(@"select dwjls,dj from NewtouchHIS_Base..V_S_xt_sfxm where OrganizeId = @orgId
                        AND sfxmCode=@sfxmCode and zt='1'");
                sqlpar.Add(new SqlParameter("@sfxmCode", sfxmCode));
                sqlpar.Add(new SqlParameter("@orgId", orgId));
                var data = this.FirstOrDefault<base_SfxmDto>(par.ToString(), sqlpar.ToArray());
                dwjls = data.dwjls;dj = data.dj;
            }
        }
        /// <summary>
        /// LấyKhoa thực hiệnTên
        /// </summary>
        /// <param name="zxksCode"></param>
        /// <param name="orgId"></param>
        /// <param name="zxksmc"></param>
        public void Getzxksmc(string zxksCode, string orgId, ref string zxksmc)
        {
            if (!string.IsNullOrWhiteSpace(zxksCode))
            {
                //LấySố lượng đơn vị tính
                var sqlpar = new List<SqlParameter>();
                var par = new StringBuilder();
                par.Append(@"select top 1 Name from NewtouchHIS_Base..[Sys_Department] where OrganizeId = @orgId
                        AND Code=@zxksCode and zt='1'");
                sqlpar.Add(new SqlParameter("@zxksCode", zxksCode));
                sqlpar.Add(new SqlParameter("@orgId", orgId));
                zxksmc = this.FirstOrDefault<string>(par.ToString(), sqlpar.ToArray());
            }
        }

        /// <summary>
        /// Tính toánmỗingàyTần suất
        /// </summary>
        /// <param name="zxdw"></param>
        /// <param name="zydw"></param>
        /// <param name="zxcs"></param>
        /// <param name="zxzq"></param>
        /// <param name="zxzqdw"></param>
        /// <returns></returns>
        public float Getpcsl(int zxcs, int zxzq, EnumZxzqdw zxzqdw)
        {
            if (string.IsNullOrWhiteSpace(zxzqdw.ToString()))
            {
                throw new FailedException("Chu kỳ thực hiệnĐơn vịkhông thểlàtrống");
            }
            float rtnsl;
            switch (zxzqdw)
            {
                case EnumZxzqdw.Day:
                    rtnsl = (zxcs / zxzq);
                    break;
                case EnumZxzqdw.Hour:
                    rtnsl = (zxcs / zxzq) * 24;
                    break;
                case EnumZxzqdw.Minute:
                    rtnsl = (zxcs / zxzq) * 24 * 60;
                    break;
                default:
                    rtnsl = 0;
                    break;
            }
            return rtnsl;
        }

        /// <summary>
        /// Tính toánThuốcSố lượng
        /// </summary>
        /// <param name="jlzhxs"></param>
        /// <param name="zyzhxs"></param>
        /// <param name="ypjl"></param>
        /// <param name="ts"></param>
        /// <param name="dwlb"></param>
        /// <param name="orgId"></param>
        /// <param name="zxcs"></param>
        /// <param name="zxzq"></param>
        /// <param name="zxzqdw"></param>
        /// <returns></returns>
        public int Getypsl(decimal? jlzhxs, decimal? zyzhxs, float ypjl, EnumYPdwlb dwlb, string orgId, int zxcs, int zxzq, EnumZxzqdw zxzqdw, string qzfs, int ts = 1)
        {
            //hss Sửavà2019.8.8 Số lượngứngtheochiếuChu kỳ thực hiệnTính toán,vìTần suất y lệnhQOD thaybảngcáchngàymột lần,vàtrước đâyChu kỳ thực hiệncókhuloại,hướnggâycóhỏiđề.
            if (zxzqdw == EnumZxzqdw.Day)
            {
                ts = zxzq;
            }
            //lấychỉnhphươngkiểu
            //var qzfs = _sysConfigRepo.GetValueByCode("QZFS", orgId) ?? "day";
            var zysl = 0;
            if (string.IsNullOrWhiteSpace(dwlb.ToString()))
            {
                throw new FailedException("Đơn vịLoạikhông thểlàtrống");
            }
            var qzint = ypjl * ts;//Mặc địnhĐơn vị nhập viện ,khôngcầnchuyểnđổi
            if (dwlb == EnumYPdwlb.Jldw)
            {//Đơn vị liều chuyểnđổithànhĐơn vị liều

                if (jlzhxs == 0 || zyzhxs == 0)
                {
                    throw new FailedException("Hệ số chuyển đổi liềuvàNhập việnchuyểnđổihệsốkhông thểlàtrống");
                }
                qzint = (ypjl * ts / float.Parse(jlzhxs.ToString()) / float.Parse(zyzhxs.ToString()));
            }

            if (qzfs == "day")
            {
                zysl = (Math.Ceiling(qzint * Getpcsl(zxcs, zxzq, zxzqdw))).ToInt();
            }
            else if (qzfs == "times")
            {
                zysl = (Math.Ceiling(qzint) * Getpcsl(zxcs, zxzq, zxzqdw)).ToInt();
            }
            return zysl;
        }

        ///// <summary>
        ///// LấyKhoa thực hiệnMã
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="xmdm"></param>
        ///// <returns></returns>
        //string Getzxksdm(string orgId,string xmdm) {
        //    //LấyCách dùng
        //    var par = new List<SqlParameter>();
        //    var sql = new StringBuilder();
        //    sql.Append(@"SELECT  yfbmcode
        //        FROM    [dbo].[zy_ypjxyfdz] dz
        //                INNER JOIN NewtouchHIS_Base..V_C_xt_yp yp ON dz.jxcode = yp.jx
        //                                                             AND dz.[OrganizeId] = yp.OrganizeId
        //        WHERE   dz.zt = '1'
        //                AND yp.ypCode = @xmdm
        //                AND dz.[OrganizeId] = @orgId");
        //    par.Add(new SqlParameter("@orgId", orgId));
        //    par.Add(new SqlParameter("@xmdm", xmdm));

        //    return FirstOrDefault<string>(sql.ToString(), par.ToArray());
        //}

        #endregion

        #region Y lệnhTra cứu
        public List<AdviceListGridVO> AdviceGridView(Pagination pagination, AdviceListRequestVO req)
        {
            if (req == null || req.zyh == null)
            {
                throw new FailedException("thiếuĐiều kiện tìm kiếm");
            }

            string yzztExpr = "";
            if (req.dryz) { }
            else if (req.wsh)
            {
                yzztExpr = " and yzzt=@yzzt";
            }
            else if (!string.IsNullOrWhiteSpace(req.yzlb))
            {
                yzztExpr = " and yzzt<>@yzzt";
            }
            if (req.yx == "yx")
            {
                if (req.wsh)
                {
                    yzztExpr = " and (yzzt=@ds or yzzt=@sh or yzzt=@zx or yzzt=@dc)";
                }
                else
                {
                    yzztExpr = " and (yzzt=@sh or yzzt=@zx or yzzt=@dc)";
                }
            }

            var rtn = new List<AdviceListGridVO>();
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"SELECT  *
                            FROM    ( SELECT    '长' yzlb ,iszt,yzh,
                                        case when yzlx in ('6','7') then yzh else yz.Id end Id,
                                        yzlx ,
                                        kssj ,
                                        s.Name ysmc ,
                                        case when yzlx in ('6','7') then ztmc else xmmc end as yzmc,
                                        CONCAT(CONVERT(float,ypjl),dw) as yzjl, ypyf.yfmc, yzpc.yzpcmc,
                                        yz.zh ,
                                        yz.tzsj ,
                                        tzr.Name tzr ,
                                        zxr.Name zxr ,
                                        yz.yzzt ,
                                        yz.CreateTime,
						                yz.shsj,
						                yz.zxsj,
										dept.Name deptName ,
                                        yz.yztag,
                                        case yz.yztag when 'JI' then 'Hướng thần I' when 'JII' then 'Hướng thần II' when 'MZ' then 'Gây mê' else yz.yztag end yztagName,
										isnull(yz.isjf,1) isjf,ispscs,isnull(zzfbz,0) zzfbz , yz.xmdm,case when yzlx='10' then yz.sl* (cast(yz.ypjl as int)) else yz.sl end sl,  yz.Px,
                                        case when yzlx in (2,4,10) then (cast(case when yzlx='10' then convert(numeric(18,2),yz.sl*yz.ypjl) else yz.sl end as varchar)+ypxx.zycldw) end zycldw,
                                        yfztbs,yply,isfsyz,zyh,hzxm xm
                              FROM      (
                                              select 'Y' iszt,row_number() over(partition by ztid,yzh order by createtime desc) num ,*
                                                from zy_cqyz yz with(nolock) where ztid is not null
                                                and yz.zt = '1'
                                                and yz.OrganizeId =@orgId
                                                and yz.zyh=@zyh and yfztbs is null" + yzztExpr + @"
                                                union all
										      select 'N' iszt,1 num ,*
                                                from zy_cqyz yz with(nolock) where ztid is  null 
                                                and yz.zt = '1'
                                                and yz.OrganizeId =@orgId
                                                and yz.zyh=@zyh" + yzztExpr + @"
                                        ) yz
                                        LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] s ON ( ( s.gh = yz.ysgh
                                                             AND s.zt = '1'
                                                             AND s.OrganizeId =@orgId
                                                           )
                                                         ) 
                                      LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] tzr ON ( ( tzr.gh = yz.tzr
                                                             AND tzr.zt = '1'
                                                             AND tzr.OrganizeId =@orgId
                                                           )
                                                         )
                                     LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] zxr ON ( ( zxr.gh = yz.zxr
                                                             AND zxr.zt = '1'
                                                             AND zxr.OrganizeId =@orgId
                                                           )
                                                         )
                                      LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Department] dept ON ( yz.zxksdm=dept.Code 
                                                             AND dept.zt = '1'
                                                             AND dept.OrganizeId =@orgId
                                                           )
                                     LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on yz.ypyfdm = ypyf.yfCode 
									 LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yzpc] yzpc on yz.pcCode = yzpc.yzpcCode and yzpc.OrganizeId = @orgId
                                     LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yp] ypxx  ON yz.xmdm = ypxx.ypCode AND ypxx.OrganizeId =@orgId
                                             WHERE num=1
                                             --AND yz.zt = '1' 
                                             --AND yz.OrganizeId =@orgId
                                             --and yz.zyh=@zyh
                               UNION ALL
                               select '临' yzlb ,iszt,yzh,
                                        case when yzlx in ('6','7') then yzh else yz.Id end Id ,
                                        yzlx ,
                                        kssj ,
                                        s.Name ysmc ,
                                        case when yzlx in ('6','7') then ztmc else xmmc end as yzmc,
                                        CONCAT(CONVERT(float,ypjl),dw) as yzjl, ypyf.yfmc, yzpc.yzpcmc,
                                        yz.zh ,
                                        NULL tzsj ,
                                        NULL tzr ,
                                        zxr.Name zxr ,
                                        yz.yzzt ,
                                        yz.CreateTime,
						                yz.shsj,
						                yz.zxsj,
										dept.Name deptName,yztag,
                                        case yztag when 'JI' then 'Hướng thần I' when 'JII' then 'Hướng thần II' when 'MZ' then 'Gây mê' else yztag end yztagName,
                                        isnull(yz.isjf,1) isjf,
                                        ispscs ,isnull(zzfbz,0) zzfbz,
                                        xmdm,
										case when yzlx='10' then sl* (cast(yz.ypjl as int)) else sl end sl, yz.Px,
										case when yzlx in (2,4,10) then (cast(case when yzlx='10' then convert(numeric(18,2),sl*yz.ypjl) else  sl end as varchar)+ypxx.zycldw) end zycldw,
                                        yfztbs,yply,isfsyz,zyh,hzxm xm
                                        from (
                                            select 'Y' iszt,row_number() over(partition by ztid,yzh order by createtime desc) num ,*
                                            from zy_lsyz jyjcyz with(nolock) where ztid is not null--yzlx in ('6','7') 
                                            and jyjcyz.zt = '1'
                                            and jyjcyz.OrganizeId =@orgId
                                            and jyjcyz.zyh=@zyh and yfztbs is null" + yzztExpr + @"
                                            union all
										     select 'N' iszt,1 num ,*
                                            from zy_lsyz jyjcyz with(nolock) where ztid is  null --yzlx in ('6','7') 
                                            and jyjcyz.zt = '1'
                                            and jyjcyz.OrganizeId =@orgId 
                                            and jyjcyz.zyh=@zyh" + yzztExpr + @"
                                        ) yz
                                        LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] s ON ( ( s.gh = yz.ysgh
                                                             AND s.zt = '1'
                                                             AND s.OrganizeId =@orgId
                                                           )
                                                         ) 
                                        LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Staff] zxr ON ( ( zxr.gh = yz.zxr
                                                             AND zxr.zt = '1'
                                                             AND zxr.OrganizeId =@orgId
                                                           )
                                                         )
                                        LEFT JOIN [NewtouchHIS_Base].[dbo].[Sys_Department] dept ON (  yz.zxksdm=dept.Code 
                                                             AND dept.zt = '1'
                                                             AND dept.OrganizeId =@orgId
                                                           )
                                      LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yp] ypxx
		                                 ON yz.xmdm = ypxx.ypCode
			                            AND ypxx.OrganizeId = @orgId
                                    LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_ypyf] ypyf on yz.ypyfdm = ypyf.yfCode 
									LEFT JOIN [NewtouchHIS_Base].[dbo].[xt_yzpc] yzpc on yz.pcCode = yzpc.yzpcCode and yzpc.OrganizeId = @orgId
                                    WHERE  num=1
                                        --yz.zt = '1'
                                       --  AND yz.OrganizeId =@orgId
                                      --   and yz.zyh=@zyh
                            ) tttt WHERE   1 = 1");
            if (req != null)
            {
                if (req.dryz)//khingàyY lệnh
                {
                    sqlstr.Append(" AND CONVERT(VARCHAR(10), tttt.CreateTime, 111) = CONVERT(VARCHAR(10), GETDATE(), 111)");
                }
                else if (req.wsh)
                {//chưaPhê duyệt
                    //sqlstr.Append(" AND tttt.yzzt=@yzzt");
                    par.Add(new SqlParameter("@yzzt", EnumYzzt.Ds.ToInt()));
                }
                else if (!string.IsNullOrWhiteSpace(req.yzlb))
                {//Loại y lệnh
                    if (req.yzlb == "长" || req.yzlb == "临")
                    {
                        sqlstr.Append(" AND tttt.yzlb = @yzlb");
                        par.Add(new SqlParameter("@yzlb", req.yzlb));
                    }
                    //sqlstr.Append(" AND tttt.yzzt <> @yzzt");
                    par.Add(new SqlParameter("@yzzt", Convert.ToInt32(EnumYzzt.Ds)));
                }
                //if ((!req.wsh) && req.yx == "yx")//chưaPhê duyệtđiều kiện tiên quyếtdướicủaHiệu lựcY lệnh
                //{
                //    sqlstr.Append("  AND (tttt.yzzt=@sh or tttt.yzzt=@zx)");
                //    par.Add(new SqlParameter("@sh", Convert.ToInt32(EnumYzzt.Sh)));
                //    par.Add(new SqlParameter("@zx", Convert.ToInt32(EnumYzzt.Zx)));
                //}
                if (req.yx == "yx") //Hiệu lựcY lệnh
                {
                    if (req.wsh)
                    {//Chờ duyệt
                        //sqlstr.Append("  AND (tttt.yzzt=@ds or tttt.yzzt=@sh or tttt.yzzt=@zx or tttt.yzzt=@dc)");
                        par.Add(new SqlParameter("@ds", Convert.ToInt32(EnumYzzt.Ds)));
                        par.Add(new SqlParameter("@sh", Convert.ToInt32(EnumYzzt.Sh)));
                        par.Add(new SqlParameter("@zx", Convert.ToInt32(EnumYzzt.Zx)));
                        par.Add(new SqlParameter("@dc", Convert.ToInt32(EnumYzzt.DC)));
                    }
                    else
                    {
                        //sqlstr.Append("  AND (tttt.yzzt=@sh or tttt.yzzt=@zx or tttt.yzzt=@dc)");
                        par.Add(new SqlParameter("@sh", Convert.ToInt32(EnumYzzt.Sh)));
                        par.Add(new SqlParameter("@zx", Convert.ToInt32(EnumYzzt.Zx)));
                        par.Add(new SqlParameter("@dc", Convert.ToInt32(EnumYzzt.DC)));
                    }
                }
                if (!string.IsNullOrWhiteSpace(req.yzlx.ToString()) && req.yzlx != 0)//Kiểu y lệnh
                {
                    sqlstr.Append("  AND tttt.yzlx=@yzlx");
                    par.Add(new SqlParameter("@yzlx", req.yzlx));
                }
                if (req.kssj != DateTime.MaxValue && req.kssj != DateTime.MinValue &&
                    req.jssj != DateTime.MaxValue && req.jssj != DateTime.MinValue)
                {
                    sqlstr.Append(" AND tttt.kssj > @kssj AND tttt.kssj < DATEADD(dd, 1, @jssj)");
                    par.Add(new SqlParameter("@kssj", req.kssj));
                    par.Add(new SqlParameter("@jssj", req.jssj));
                }
            }
            par.Add(new SqlParameter("@orgId", req.orgId));
            par.Add(new SqlParameter("@zyh", req.zyh));
            return this.QueryWithPage<AdviceListGridVO>(sqlstr.ToString(), pagination, par.ToArray(), false).ToList();
        }

        /// <summary>
        /// DừngY lệnh dài hạnhoặcHủy bỏY lệnh tạm thời cùngSố nhóm
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="tzsj"></param>
        /// <param name="czr"></param>
        public void AdviceStop(string yzId, DateTime? tzsj, string czr, string orgId, string yzlx, string zyh, string iszt)
        {
            if (string.IsNullOrWhiteSpace(yzId))
            {
                throw new FailedException("Y lệnhkhôngtồn tạiở");
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {

                var cqyz = db.FindEntity<InpatientLongTermOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");

                if (cqyz != null)
                {
                    if (tzsj == null)
                    {
                        throw new FailedException("thiếuThời gian dừng");
                    }
                    if (cqyz.zxsj != null)
                    {
                        if (DateTimeManger.DateDiff(DateInterval.Second, DateTime.Parse(cqyz.zxsj.ToString()), DateTime.Parse(tzsj.ToString())) < 1)
                        {
                            throw new FailedException("Thời gian dừngkhông thểnhỏtạiThời gian thực hiện cuối；Thời gian thực hiện cuối：" + cqyz.zxsj);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(cqyz.zh.ToString()))
                    {
                        var zhlist = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.zh == cqyz.zh && p.OrganizeId == orgId && p.zt == "1");
                        if (zhlist != null && zhlist.Count() > 0)
                        {
                            foreach (var item in zhlist)
                            {
                                item.tzsj = tzsj;
                                item.tzr = czr;
                                item.yzzt = (int)EnumYzzt.DC;
                                db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                                db.Update(item);
                            }
                        }
                    }
                    else
                    {
                        List<InpatientLongTermOrderEntity> cqyzzt = new List<InpatientLongTermOrderEntity>();
                        if (!string.IsNullOrWhiteSpace(cqyz.dcztbs))
                        {
                            cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.dcztbs == cqyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(cqyz.yfztbs))
                                cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.yfztbs == cqyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                            cqyzzt.Add(cqyz);

                        }
                        foreach (var item in cqyzzt)
                        {
                            item.tzsj = tzsj;
                            item.tzr = czr;
                            item.yzzt = (int)EnumYzzt.DC;
                            db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                            db.Update(item);
                        }
                    }
                }
                else
                {
                    if (yzlx == ((int)EnumYzlx.jy).ToString() || yzlx == ((int)EnumYzlx.jc).ToString())
                    {
                        var jyjcyz = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yzh == yzId && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                        if (jyjcyz != null && jyjcyz.Count() > 0)
                        {
                            foreach (var item in jyjcyz)
                            {
                                item.zfsj = DateTime.Now;
                                item.zfr = czr;
                                item.yzzt = (int)EnumYzzt.DC;
                                db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                db.Update(item);
                            }
                        }
                    }
                    else
                    {
                        var lsyz = db.FindEntity<InpatientSTATOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");
                        if (lsyz != null)
                        {
                            if (!string.IsNullOrWhiteSpace(lsyz.zh.ToString()))
                            {
                                var zhlist = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.zh == lsyz.zh && p.OrganizeId == orgId && p.zt == "1");
                                if (zhlist != null && zhlist.Count() > 0)
                                {
                                    foreach (var item in zhlist)
                                    {
                                        item.zfsj = DateTime.Now;
                                        item.zfr = czr;
                                        item.yzzt = (int)EnumYzzt.DC;
                                        db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                        db.Update(item);
                                    }
                                }
                            }
                            else
                            {
                                if (lsyz.sssj != null && lsyz.sqdh != "")
                                {
                                    var sqlpar = new List<SqlParameter>();
                                    var cxsql = @"select 1 as jg from [Newtouch_OR].[dbo].[OR_ApplyInfo] a 
                                        left join [Newtouch_OR].[dbo].[OR_ApplyInfo_Expand] b 
                                        on a.applyno=b.applyno and b.px=1
                                        where a.zyh=@zyh and a.zt=1  and a.OrganizeId=@orgId and a.sqzt<>2  and a.Applyno=@sqdh";
                                    sqlpar.Add(new SqlParameter("@zyh", lsyz.zyh));
                                    sqlpar.Add(new SqlParameter("@orgId", orgId));
                                    sqlpar.Add(new SqlParameter("@sqdh", lsyz.sqdh));
                                    var listjg = FindList<TovoidOperationVO>(cxsql, sqlpar.ToArray());
                                    if (listjg.Count > 0)
                                    {
                                        var sqlupdate = @"update a set a.sqzt=3
                                                        from " + DbNames.Name("OrDb", "Newtouch_OR") + @".[dbo].[OR_ApplyInfo] a 
                                                        left join " + DbNames.Name("OrDb", "Newtouch_OR") + @".[dbo].[OR_ApplyInfo_Expand] b 
                                                        on a.applyno=b.applyno and b.px=1
                                                        where a.zyh=@zyh and  a.zt=1 and a.OrganizeId=@orgId and  a.sqzt<>2 and  a.Applyno=@sqdh";

                                        ExecuteSqlCommand(sqlupdate, new SqlParameter[] { new SqlParameter("@zyh", lsyz.zyh),
                                        new SqlParameter("@orgId", orgId),
                                        new SqlParameter("@sqdh", lsyz.sqdh) });
                                        lsyz.zfsj = DateTime.Now;
                                        lsyz.zfr = czr;
                                        lsyz.yzzt = (int)EnumYzzt.DC;
                                        db.DetacheEntity<InpatientSTATOrderEntity>(lsyz);
                                        db.Update(lsyz);
                                    }
                                    else
                                    {
                                        throw new FailedException("Phê duyệtthông quacủaY lệnhkhông thể xóa！");
                                    }
                                }
                                else
                                {
                                    List<InpatientSTATOrderEntity> lsyzzt = new List<InpatientSTATOrderEntity>();
                                    if (!string.IsNullOrWhiteSpace(lsyz.dcztbs))
                                    {
                                        lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.dcztbs == lsyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(lsyz.yfztbs))
                                            lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yfztbs == lsyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                                        lsyzzt.Add(lsyz);

                                    }
                                    foreach (var lsitem in lsyzzt)
                                    {
                                        lsitem.zfsj = DateTime.Now;
                                        lsitem.zfr = czr;
                                        lsitem.yzzt = (int)EnumYzzt.DC;
                                        db.DetacheEntity<InpatientSTATOrderEntity>(lsitem);
                                        db.Update(lsitem);
                                    }
                                }
                            }

                        }
                    }
                }
                db.Commit();
            }
        }

        /// <summary>
        /// vậtlýXóa,XóacùngSố nhómcủaDữ liệu
        /// </summary>
        /// <param name="yzId"></param>
        public void AdviceDel(string yzId, string orgId, string yzlx, string zyh, string iszt, string conflinktoOR)
        {
            if (string.IsNullOrWhiteSpace(yzId))
            {
                throw new FailedException("Y lệnhkhôngtồn tạiở");
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var cqyz = db.FindEntity<InpatientLongTermOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");
                if (cqyz != null)
                {
                    if (!string.IsNullOrWhiteSpace(cqyz.zh.ToString()))
                    {
                        var zhlist = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.zh == cqyz.zh && p.OrganizeId == orgId && p.zt == "1");
                        if (zhlist != null && zhlist.Count() > 0)
                        {
                            foreach (var item in zhlist)
                            {
                                item.zt = "0";
                                db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                                db.Update(item);
                            }
                        }
                    }
                    else
                    {
                        List<InpatientLongTermOrderEntity> cqyzzt = new List<InpatientLongTermOrderEntity>();
                        if (!string.IsNullOrWhiteSpace(cqyz.dcztbs))
                        {
                            cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.dcztbs == cqyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(cqyz.yfztbs))
                                cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.yfztbs == cqyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                            cqyzzt.Add(cqyz);

                        }
                        foreach (var item in cqyzzt)
                        {
                            item.zt = "0";
                            db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                            db.Update(item);
                        }
                    }
                }
                else
                {
                    //Xét nghiệmKiểm tralấyBộ mẫuXóa
                    if (yzlx == ((int)EnumYzlx.jy).ToString() || yzlx == ((int)EnumYzlx.jc).ToString())
                    {
                        var jyjcyz = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yzh == yzId && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                        if (jyjcyz != null && jyjcyz.Count() > 0)
                        {
                            foreach (var item in jyjcyz)
                            {
                                item.zt = "0";
                                db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                db.Update(item);
                            }

                        }
                    }
                    else
                    {
                        var lsyz = db.FindEntity<InpatientSTATOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");
                        if (lsyz != null)
                        {
                            if (!string.IsNullOrWhiteSpace(lsyz.zh.ToString()))
                            {
                                var zhlist = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.zh == lsyz.zh && p.OrganizeId == orgId && p.zt == "1");
                                if (zhlist != null && zhlist.Count() > 0)
                                {
                                    foreach (var item in zhlist)
                                    {
                                        item.zt = "0";
                                        db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                        db.Update(item);

                                    }

                                }
                            }
                            else
                            {
                                if (lsyz.sssj != null && lsyz.sqdh != "")
                                {

                                    if (!string.IsNullOrWhiteSpace(conflinktoOR) && conflinktoOR == "true")
                                    {
                                        var sqlpar = new List<SqlParameter>();

                                        var cxsql = @"select 1 as jg from [Newtouch_OR].[dbo].[OR_ApplyInfo] a 
                                        left join [Newtouch_OR].[dbo].[OR_ApplyInfo_Expand] b 
                                        on a.applyno=b.applyno and b.px=1
                                        where a.zyh=@zyh and a.zt=1  and a.OrganizeId=@orgId and a.sqzt<>2  and a.Applyno=@sqdh";
                                        sqlpar.Add(new SqlParameter("@zyh", lsyz.zyh));
                                        sqlpar.Add(new SqlParameter("@orgId", orgId));
                                        sqlpar.Add(new SqlParameter("@sqdh", lsyz.sqdh));
                                        var listjg = FindList<SelectoperationjgVO>(cxsql.ToString(), sqlpar.ToArray());
                                        if (listjg.Count > 0)
                                        {
                                            var sqlupdate = @"update a set a.sqzt=3
                                                        from " + DbNames.Name("OrDb", "Newtouch_OR") + @".[dbo].[OR_ApplyInfo] a 
                                                        left join " + DbNames.Name("OrDb", "Newtouch_OR") + @".[dbo].[OR_ApplyInfo_Expand] b 
                                                        on a.applyno=b.applyno and b.px=1
                                                        where a.zyh=@zyh and  a.zt=1 and a.OrganizeId=@orgId and  a.sqzt<>2 and  a.Applyno=@sqdh";

                                            ExecuteSqlCommand(sqlupdate.ToString(), new SqlParameter[] { new SqlParameter("@zyh", lsyz.zyh),
                                        new SqlParameter("@orgId", orgId),
                                        new SqlParameter("@sqdh", lsyz.sqdh)});
                                            lsyz.zt = "0";
                                            db.DetacheEntity<InpatientSTATOrderEntity>(lsyz);
                                            db.Update(lsyz);
                                        }
                                        else
                                        {
                                            throw new FailedException("Phê duyệtthông quacủaY lệnhkhông thể xóa！");
                                        }
                                    }
                                    else
                                    {
                                        lsyz.zt = "0";
                                        db.DetacheEntity<InpatientSTATOrderEntity>(lsyz);
                                        db.Update(lsyz);
                                    }

                                }
                                else
                                {
                                    List<InpatientSTATOrderEntity> lsyzzt = new List<InpatientSTATOrderEntity>();
                                    if (!string.IsNullOrWhiteSpace(lsyz.dcztbs))
                                    {
                                        lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.dcztbs == lsyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(lsyz.yfztbs))
                                            lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yfztbs == lsyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                                        lsyzzt.Add(lsyz);

                                    }
                                    foreach (var lsitem in lsyzzt)
                                    {
                                        lsitem.zt = "0";
                                        db.DetacheEntity<InpatientSTATOrderEntity>(lsitem);
                                        db.Update(lsitem);
                                    }
                                }

                            }
                        }
                    }
                }
                //ăn uốngY lệnh
                var ssyzlist = db.IQueryable<InpatientDietDetailSplitEntity>().Where(p => p.MainId == yzId && p.OrganizeId == orgId && p.zt == "1");
                if (ssyzlist != null && ssyzlist.Count() > 0)
                {
                    foreach (var item in ssyzlist)
                    {
                        item.zt = "0";
                        db.DetacheEntity<InpatientDietDetailSplitEntity>(item);
                        db.Update(item);
                    }

                }
                db.Commit();
            }
        }

        /// <summary>
        /// rútDC rútdccùngSố nhómcủaY lệnh
        /// </summary>
        /// <param name="yzId"></param>
        public void Advicedc(string yzId, string orgId, string yzlx, string zyh, string isFeeGroup)
        {
            if (string.IsNullOrWhiteSpace(yzId))
            {
                throw new FailedException("Y lệnhkhôngtồn tạiở");
            }
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var cqyz = db.FindEntity<InpatientLongTermOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");
                    if (cqyz != null)
                    {
                        if (cqyz.yzzt != (int)EnumYzzt.DC)
                        {
                            throw new FailedException("Y lệnhTrạng tháikhôngđangxác");
                        }
                        if (!string.IsNullOrWhiteSpace(cqyz.zh.ToString()))
                        {
                            var zhlist = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.zh == cqyz.zh && p.OrganizeId == orgId && p.zt == "1");
                            if (zhlist != null && zhlist.Count() > 0)
                            {
                                foreach (var item in zhlist)
                                {
                                    item.tzr = null;
                                    item.tzsj = null;
                                    item.yzzt = item.zxsj != null ? (int)EnumYzzt.Zx : (int)EnumYzzt.Sh;
                                    //cóThời gian thực hiện,lấyđãThực hiệnTrạng thái,cóPhê duyệtThời gian,lấyĐã duyệtTrạng thái
                                    db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                                    db.Update(item);
                                }
                            }
                        }
                        else
                        {
                            List<InpatientLongTermOrderEntity> cqyzzt = new List<InpatientLongTermOrderEntity>();
                            if (!string.IsNullOrWhiteSpace(cqyz.dcztbs))//Mục thu phíBộ mẫu
                            {
                                cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.dcztbs == cqyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(cqyz.yfztbs))//Thuốcmởlập Cách dùngtương ứngBộ mẫu
                                    cqyzzt = db.IQueryable<InpatientLongTermOrderEntity>().Where(p => p.yfztbs == cqyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                                cqyzzt.Add(cqyz);

                            }
                            foreach (var item in cqyzzt)
                            {
                                item.tzr = null;
                                item.tzsj = null;
                                item.yzzt = item.zxsj != null ? (int)EnumYzzt.Zx : (int)EnumYzzt.Sh;
                                //cóThời gian thực hiện,lấyđãThực hiệnTrạng thái,cóPhê duyệtThời gian,lấyĐã duyệtTrạng thái
                                db.DetacheEntity<InpatientLongTermOrderEntity>(item);
                                db.Update(item);
                            }
                        }
                    }
                    else
                    {
                        if (yzlx == ((int)EnumYzlx.jy).ToString() || yzlx == ((int)EnumYzlx.jc).ToString())//Xét nghiệmKiểm tratheoY lệnhsố
                        {
                            var jyjcyz = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yzh == yzId && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                            if (jyjcyz != null && jyjcyz.Count() > 0)
                            {
                                foreach (var item in jyjcyz)
                                {
                                    //cóThời gian thực hiện,lấyđãThực hiệnTrạng thái,cóPhê duyệtThời gian,lấyĐã duyệtTrạng thái
                                    item.yzzt = item.zxsj != null ? (int)EnumYzzt.Zx : (int)EnumYzzt.Sh;
                                    db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                    db.Update(item);
                                }

                            }
                        }
                        else
                        {
                            var lsyz = db.FindEntity<InpatientSTATOrderEntity>(p => p.Id == yzId && p.OrganizeId == orgId && p.zt == "1");
                            if (lsyz != null)
                            {
                                if (lsyz.yzzt != (int)EnumYzzt.DC)
                                {
                                    throw new FailedException("Y lệnhTrạng tháikhôngđangxác");
                                }
                                if (!string.IsNullOrWhiteSpace(lsyz.zh.ToString()))
                                {
                                    var zhlist = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.zh == lsyz.zh && p.OrganizeId == orgId && p.zt == "1");
                                    if (zhlist != null && zhlist.Count() > 0)
                                    {
                                        foreach (var item in zhlist)
                                        {
                                            //cóThời gian thực hiện,lấyđãThực hiệnTrạng thái,cóPhê duyệtThời gian,lấyĐã duyệtTrạng thái
                                            item.yzzt = item.zxsj != null ? (int)EnumYzzt.Zx : (int)EnumYzzt.Sh;

                                            db.DetacheEntity<InpatientSTATOrderEntity>(item);
                                            db.Update(item);
                                        }

                                    }
                                }
                                else
                                {
                                    List<InpatientSTATOrderEntity> lsyzzt = new List<InpatientSTATOrderEntity>();
                                    if (!string.IsNullOrWhiteSpace(lsyz.dcztbs))
                                    {
                                        lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.dcztbs == lsyz.dcztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1").ToList();
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(lsyz.yfztbs))
                                            lsyzzt = db.IQueryable<InpatientSTATOrderEntity>().Where(p => p.yfztbs == lsyz.yfztbs && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1" && p.ztId != null).ToList();

                                        lsyzzt.Add(lsyz);

                                    }
                                    foreach (var lsitem in lsyzzt)
                                    {
                                        //cóThời gian thực hiện,lấyđãThực hiệnTrạng thái,cóPhê duyệtThời gian,lấyĐã duyệtTrạng thái
                                        lsitem.yzzt = lsitem.zxsj != null ? (int)EnumYzzt.Zx : (int)EnumYzzt.Sh;
                                        db.DetacheEntity<InpatientSTATOrderEntity>(lsitem);
                                        db.Update(lsitem);
                                    }
                                }

                            }
                        }
                    }
                    db.Commit();
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Xuất việntoànDừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="tzsj"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        public void AdviceLeaveHospitalStopSubmit(string zyh, DateTime tzsj, string orgId, string czr)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("thiếuSố nhập viện");
            }
            var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
            try
            {
                //Dừngtất cảY lệnh dài hạn
                var cqyzList = _inpatientLongTermOrderRepo.IQueryable().Where(p => p.zyh == zyh && p.yzzt != 3 && p.OrganizeId == orgId && p.zt == "1" && p.yzzt != (int)EnumYzzt.TZ);
                if (cqyzList != null && cqyzList.Count() > 0)
                {
                    var lastzxsj = cqyzList.OrderByDescending(p => p.zxsj).FirstOrDefault().zxsj;
                    if (lastzxsj != null)
                    {
                        if (DateTimeManger.DateDiff(DateInterval.Second, DateTime.Parse(lastzxsj.ToString()), tzsj) < 1)
                        {
                            throw new FailedException("Thời gian dừngkhông thểnhỏtạiThời gian thực hiện cuối；Thời gian thực hiện cuối：" + lastzxsj);
                        }
                    }
                }
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    if (cqyzList != null && cqyzList.Count() > 0)
                    {
                        foreach (var cqyz in cqyzList)
                        {
                            cqyz.tzsj = tzsj;
                            cqyz.tzr = czr;
                            cqyz.yzzt = (int)EnumYzzt.DC;
                            _inpatientLongTermOrderRepo.DetacheEntity(cqyz);
                            db.Update(cqyz);
                        }
                    }
                    //làKhôngtạovăn bảnY lệnh
                    bool AutoGenerateWordAdvice = _sysConfigRepo.GetBoolValueByCode("AutoGenerateWordAdvice", orgId, true).ToBool();
                    if (AutoGenerateWordAdvice)
                    {
                        //tạomột dòngTạm thờivăn bảnY lệnh
                        var lsyz = new InpatientSTATOrderEntity()
                        {
                            xmdm = "999999999999999999",
                            xmmc = DateTime.Now + "Ra viện và tư vấn phục hồi chức năng",
                            zyh = zyh,
                            WardCode = patientobj.WardCode,
                            DeptCode = patientobj.DeptCode,
                            ysgh = czr,
                            pcCode = "00",
                            yzzt = (int)EnumYzzt.Ds,
                            yzlx = (int)EnumYzlx.Wz,
                            yznr = DateTime.Now + "Ra viện và tư vấn phục hồi chức năng     ST",
                            OrganizeId = orgId,
                            kssj = tzsj,
                            hzxm = patientobj.xm

                        };
                        lsyz.Create(true);
                        db.Insert(lsyz);
                    }
                    db.Commit();
                }

            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// chuyển khutoànDừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        public void AdviceTransferWardStopSubmit(TransferWardRequestVO req)
        {
            if (string.IsNullOrWhiteSpace(req.zyh))
            {
                throw new FailedException("thiếuSố nhập viện");
            }

            var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == req.zyh && p.OrganizeId == req.orgId && p.zt == "1");
            try
            {
                //Dừngtất cảY lệnh dài hạn
                var cqyzList = _inpatientLongTermOrderRepo.IQueryable().Where(p => p.zyh == req.zyh && p.yzzt != 3 && p.OrganizeId == req.orgId && p.zt == "1");
                if (cqyzList != null && cqyzList.Count() > 0)
                {
                    var lastzxsj = cqyzList.OrderByDescending(p => p.zxsj).FirstOrDefault().zxsj;
                    if (lastzxsj != null)
                    {
                        if (DateTimeManger.DateDiff(DateInterval.Second, DateTime.Parse(lastzxsj.ToString()), req.kssj) < 1)
                        {
                            throw new FailedException("Thời gian bắt đầukhông thểnhỏtạiThời gian thực hiện cuối；Thời gian thực hiện cuối：" + lastzxsj);
                        }
                    }
                }

                using (var db = new EFDbTransaction().BeginTrans())
                {
                    if (cqyzList != null && cqyzList.Count() > 0)
                    {

                        foreach (var cqyz in cqyzList)
                        {
                            cqyz.tzsj = req.kssj;
                            cqyz.tzr = req.czr;
                            cqyz.yzzt = (int)EnumYzzt.DC;
                            _inpatientLongTermOrderRepo.DetacheEntity(cqyz);
                            db.Update(cqyz);
                        }
                    }
                    //làKhôngBình thườngvăn bảnY lệnh
                    bool AutoGenerateWordAdvice = _sysConfigRepo.GetBoolValueByCode("AutoGenerateWordAdvice", req.orgId, true).ToBool();

                    if (AutoGenerateWordAdvice)
                    {
                        //tạomột dòngTạm thờivăn bảnY lệnh
                        var lsyz = new InpatientSTATOrderEntity()
                        {
                            xmdm = "999999999999999999",
                            xmmc = DateTime.Now + "Ra viện và tư vấn phục hồi chức năng",
                            zyh = req.zyh,
                            WardCode = patientobj.WardCode,
                            DeptCode = patientobj.DeptCode,
                            ysgh = req.czr,
                            pcCode = "00",
                            yzzt = (int)EnumYzzt.Ds,
                            yzlx = (int)EnumYzlx.Wz,
                            yznr = req.kssj.ToString() + "Chuyển đến" + req.bq,
                            OrganizeId = req.orgId,
                            kssj = req.kssj,
                            hzxm = patientobj.xm
                        };
                        lsyz.Create(true);
                        db.Insert(lsyz);
                    }

                    db.Commit();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Tần suất y lệnhcodeLấyTần suấtentity
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysMedicalOrderFrequencyVEntity getpcInfoByCode(string code, string orgId)
        {
            //LấyCách dùng
            var par = new List<SqlParameter>();
            var yfsql = new StringBuilder();
            yfsql.Append(@"SELECT  yzpcId ,
                            yzpcmc,
                            zxsj ,
                            yzpcCode ,
                            zxcs ,
                            zxzq ,
                            zxzqdw
                    FROM    NewtouchHIS_Base..xt_yzpc
                    WHERE   OrganizeId = @orgId
                            AND yzpcCode = @code
                            AND zt = '1';");
            par.Add(new SqlParameter("@code", code));
            par.Add(new SqlParameter("@orgId", orgId));
            return FirstOrDefault<SysMedicalOrderFrequencyVEntity>(yfsql.ToString(), par.ToArray());
        }

        public List<RehabVO> GetSfxmZxksSelectJson(string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"select Code,Name,py from [NewtouchHIS_Base]..Sys_Department 
            where zt=1 and organizeid=@orgId
            and zxks=1
            ");
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<RehabVO>(sqlstr.ToString(), par.ToArray());
        }
        public List<RehabVO> GetSfxmZxksSelectJson(string orgId, string keyword)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"select Code,Name,py from [NewtouchHIS_Base]..Sys_Department 
            where zt=1 and organizeid=@orgId
            and (code like '%" + keyword + @"%' or name like '%" + keyword + @"%' or py like '%" + keyword + @"%')
            and zxks='1'
            ");
            par.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<RehabVO>(sqlstr.ToString(), par.ToArray());
        }

        #region Thu phínhómhợpTra cứu
        public List<SfxmztDto> GetSfxmItem(string sfmbbh, string orgId)
        {
            string sql = @" select  b.sfxm ,isnull(sfxm.sfxmmc,yp.ypmc) sfxmmc,a.sfmbbh ztId,sfmbmc ztmc,
sfxm.dw dw, b.dj dj,sl,a.ks zxks,c.Name zxksmc from NewtouchHIS_Sett..[xt_sfmb] a
join NewtouchHIS_Sett..[xt_sfmbxm] b on a.sfmbbh=b.sfmbbh and a.organizeid=b.organizeid and b.zt=1
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm  ON sfxm.sfxmCode=b.sfxm and sfxm.OrganizeId=b.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_xt_yp yp  ON yp.ypCode=b.sfxm and yp.OrganizeId=b.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department c  ON c.Code=a.ks  AND c.OrganizeId= a.OrganizeId
where a.zt=1 and a.sfmb=@sfmb and a.OrganizeId=@orgId ";
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            sqlpar.Add(new SqlParameter("@sfmb", sfmbbh));
            var listjg = FindList<SfxmztDto>(sql, sqlpar.ToArray());
            return listjg;
        }

        /// <summary>
        /// Cách dùngtương ứngMục thu phíBộ mẫu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yfcode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<yfsfxmdyDto> GetSfxmYfList(string orgId, string yfcode, string keyword)
        {
            var sqlpar = new List<SqlParameter>();
            string sql = @" select sfmb,sfmbmc
 from NewtouchHIS_Sett..[xt_sfmb] a
join NewtouchHIS_Sett..[xt_sfmbxm] b on a.sfmbbh=b.sfmbbh and a.organizeid=b.organizeid and b.zt=1
                            where a.zt=1  and a.OrganizeId=@orgId ";
            if (yfcode != null)
            {
                sql += " and yfdm=@yfcode";
                sqlpar.Add(new SqlParameter("@yfcode", yfcode));
            }
            else
            {
                sql += " and 1=2";
            }
            if (keyword != null)
            {
                sql += " and (a.sfmb like @keyword or a.sfmbmc like @keyword)";
                sqlpar.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            sql += " group by sfmb,sfmbmc,a.py,a.mzzybz,a.ks";
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            var listjg = FindList<yfsfxmdyDto>(sql, sqlpar.ToArray());
            return listjg;
        }
        #endregion

        #region Phẫu thuậtSửa nút
        public List<SelectUpdataOpertionVO> Ssupdate(string yzId, string zyh, string OrganizeId)
        {
            var sqlpar = new List<SqlParameter>();
            var sql = @"select 1 as jg from [Newtouch_CIS].[dbo].[zy_lsyz] 
                       where Id=@yzId and OrganizeId=@OrganizeId and yzlx=3 and sssj is not null and zyh=@zyh and zt=1";
            sqlpar.Add(new SqlParameter("@yzId", yzId));
            sqlpar.Add(new SqlParameter("@OrganizeId", OrganizeId));
            sqlpar.Add(new SqlParameter("@zyh", zyh));
            return FindList<SelectUpdataOpertionVO>(sql, sqlpar.ToArray());
        }
        #endregion

        public PatientInformationDTO patinfo(string zyh, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"
select xzlx,
case rytj when null then '2101' when '24' then '1301' when '52' then '52' else '2101' end yllb,
case zyxx.brxz when '9' then '1' else '0' end ydjy,
zd.zddm ryzd,
zd.zdmc ryzdmc,
isnull(zd2.zddm,zd.zddm) cyzd,
isnull(zd2.zdmc,zd.zdmc) cyzdmc,
case zd2.cyqk when null then 'Không' when '1' then 'Khỏi bệnh' when '2' then 'Đỡ hơn' when '3' then 'Chưa khỏi' when '4' then 'Tử vong' when '5' then 'Khác' when '6' then 'Chuyển viện'  else  'Không' end cyqk,
ks.name ksmc,
ks.Code kscode,
convert(varchar(50),brxx.rqrq,120) rqrq,
convert(varchar(50),isnull(brxx.cqrq,brxx.rqrq),120) cqrq
from zy_brxxk brxx
left join [NewtouchHIS_Sett]..zy_brjbxx zyxx on brxx.zyh=zyxx.zyh and zyxx.OrganizeId=brxx.OrganizeId
left join [NewtouchHIS_Sett].[dbo].[xt_card] kh on kh.patid=zyxx.patid and  kh.OrganizeId=zyxx.OrganizeId
left join [dbo].[zy_PatDxInfo] zd on zd.zyh=brxx.zyh and zd.OrganizeId= zyxx.OrganizeId and zd.zdlb='1' and zd.zt='1' and zd.zddm!='999999999' 
left join [dbo].[zy_PatDxInfo] zd2 on zd2.zyh=brxx.zyh and zd2.OrganizeId= zyxx.OrganizeId and zd2.zdlb='2' and zd2.zt='1'  and zd2.zddm!='999999999' 
left join [NewtouchHIS_Base].[dbo].[Sys_Department] ks on ks.code=zyxx.ks and ks.OrganizeId=zyxx.OrganizeId
where brxx.zyh=@zyh  and brxx.OrganizeId=@orgId
            ");
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@zyh", zyh));
            return this.FirstOrDefault<PatientInformationDTO>(sqlstr.ToString(), par.ToArray());
        }
        public DrugProjectDTO queryypandxm(string xmdm, int yzlx, string orgId)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            if (yzlx == 2)
            {
                sqlstr = sqlstr.Append(@"
               select Convert(decimal(18,3),(xtyp.lsj/xtyp.bzs),3) dj,gjybdm from [NewtouchHIS_Base].[dbo].[V_C_xt_yp] xtyp
where xtyp.ypCode=@xmdm and xtyp.OrganizeId=@orgId and xtyp.zt='1'
            ");
            }
            else
            {
                sqlstr = sqlstr.Append(@"
               select Convert(decimal(18,3),dj,3) dj,gjybdm from [NewtouchHIS_Base].[dbo].[V_S_xt_sfxm] sfxm
where sfxm.sfxmcode=@xmdm and sfxm.OrganizeId=@orgId  and sfxm.zt='1'
            ");

            }
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@xmdm", xmdm));
            return this.FirstOrDefault<DrugProjectDTO>(sqlstr.ToString(), par.ToArray());
        }
        public string pczd(string pcmc)//Tần suấttừ điển
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"
            select 
case pc.yzpcmc when 'qd' then '11'
when 'bid' then '12'
when 'tid' then '13'
when 'st' then '61'
when 'Q6H' then '35'
when 'Q8h' then '36'
when 'QID' then '14'
when 'QN' then '41'
when 'Q2h' then '32'
when 'PRN' then '62'
when 'QOD' then '42'
when 'QW' then '21'
when 'BIW' then '22'
when 'TIW' then '23'
when 'QOW' then '-1'
when '2W' then '215'
when '3W' then '216'
when '4W' then '217'
when 'Q1/2H' then '30'
when 'QH' then '31'
when 'Q3H' then '-1'
when 'Q4H' then '33'
when 'Q12H' then '37'
when '摆药' then '-1'
else '-1'
 end ybpc
 from [NewtouchHIS_Base].[dbo].V_S_xt_yzpc pc
where pc.yzpcmc=@pcmc
            ");
            par.Add(new SqlParameter("@pcmc", pcmc));
            return this.FirstOrDefault<string>(sqlstr.ToString(), par.ToArray());
        }
        public string jytj(string ypyfmc)//Cách dùng thuốctừ điển
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@"
select idl.code from [NewtouchHIS_Base].[dbo].[Sys_Items] items
left join[NewtouchHIS_Base].[dbo].sys_itemsdetail idl
on items.Id= idl.ItemId
where items.Code= 'jytj'
and idl.name=@ypyfmc
            ");
            par.Add(new SqlParameter("@ypyfmc", ypyfmc));
            return this.FirstOrDefault<string>(sqlstr.ToString(), par.ToArray());
        }
        /// <summary>
        /// việctrướcPhê duyệtgiao diệnTra cứuDữ liệu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        public string GetPriorReviewData(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx, string rygh, string username, string HospitalCode, string HospitalName, string yzcfh, string GetMAC)
        {
            try
            {

                var yzh = "";
                yzh = yzcfh;
                var zyh = reqdoctorservices[0].zyh;
                var patientobj = _inpatientPatientInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                var zlysobj = _inpatientpatientdoctorRepo.IQueryable().FirstOrDefault(p => p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                PriorReviewDTO pre = new PriorReviewDTO();
                decimal djzje = 0;
                decimal sbzje = 0;
                PatientInformationDTO brxxdata = patinfo(reqdoctorservices[0].zyh, orgId);
                List<ROW> rOWs = new List<ROW>();
                foreach (var item in reqdoctorservices)
                {
                    DrugProjectDTO drugProjectDTO = queryypandxm(item.xmdm, item.yzlx, orgId);
                    djzje += drugProjectDTO.dj * item.sl;
                    if (item.iszzffffff != null && item.iszzffffff == '1')
                    {
                        sbzje += 0;
                    }
                    else
                    {
                        sbzje += drugProjectDTO.dj * item.sl;
                    }
                    var ITEM_TYPE = "";
                    if (item.yzlx == 2)
                    {
                        ITEM_TYPE = "1";
                    }
                    else
                    {
                        ITEM_TYPE = "2";
                    }
                    ROW rOW = new ROW();
                    rOW.HIS_BILL_ID = yzh;
                    rOW.RECIPE_ID = yzh;
                    rOW.HIS_BILL_DETAIL_ID = yzh;
                    rOW.ITEM_TYPE = ITEM_TYPE;
                    rOW.ITEM_CODE = drugProjectDTO.gjybdm;
                    rOW.ITEM_NAME = item.xmmc;
                    rOW.NUMBER = item.sl;
                    rOW.PRICE = drugProjectDTO.dj;
                    rOW.TOTAL_MONEY = djzje;
                    rOW.HOSPITAL_CODE = HospitalCode;
                    rOW.HOSPITAL_NAME = HospitalName;
                    rOW.OFFICE_CODE = brxxdata.kscode;
                    rOW.OFFICE_NAME = brxxdata.ksmc;
                    rOW.DOCTOR_CODE = zlysobj.ysgh;
                    rOW.DOCTOR_NAME = zlysobj.ysmc;
                    rOW.USE_AMOUNT = "";
                    rOW.PACKAGE_UNIT = "";
                    rOW.STD = item.ypgg;
                    rOW.FREQUENCY = pczd(item.pcmc);
                    rOW.USAGE = "";
                    rOW.USE_MEDI_DAYS = item.ts ?? 1;
                    var GIVE_MEDI_WAY = "";
                    if (item.ypyfdm == null || item.yfmc == null || item.yfmc == "" || item.ypyfdm == "")
                    {
                        GIVE_MEDI_WAY = "";
                    }
                    else
                    {
                        GIVE_MEDI_WAY = jytj(item.yfmc);
                        if (GIVE_MEDI_WAY != null && GIVE_MEDI_WAY != "")
                        {
                            GIVE_MEDI_WAY = jytj(item.yfmc);
                        }
                        else
                        { GIVE_MEDI_WAY = "9"; }
                    }
                    rOW.GIVE_MEDI_WAY = GIVE_MEDI_WAY;
                    rOW.IM_MONEY = sbzje;
                    rOW.REAL_NUMBER = item.sl.ToString("F3");
                    rOW.REAL_MONEY = sbzje.ToString("F3");
                    rOW.DOCTOR_LEVEL_CODE = "1";
                    rOW.DOCTOR_DUTY_CODE = "234";
                    rOW.COL1 = "";
                    rOW.COL2 = "";
                    rOW.REMARK = "";
                    rOWs.Add(rOW);
                }
                Domain.DTO.OutputDto.BEAN bEAN = new Domain.DTO.OutputDto.BEAN();
                Domain.DTO.OutputDto.DATA dATA = new Domain.DTO.OutputDto.DATA();
                bEAN.CURRENT_STATUS = "1";//raNhập việnTrạng thái:1=ởviện,2=Xuất viện,3=Ngoại trú
                bEAN.IS_ADD = "0"; //làKhôngtănglượng.Nhập việnphiếucứ thứmột lầnlàkhôngtănglượng,lấysaulàtănglượng.Ngoại trúphiếucứđềulàkhôngtănglượng. 0biểu thịkhôngtănglượng,1biểu thịtănglượng
                bEAN.BILL_TYPE = "2"; //phiếucứLoại.1biểu thịNgoại trú,2biểu thịNhập viện
                bEAN.HIS_BILL_ID = yzh; //Y lệnh(Đơn thuốc)phiếuID
                bEAN.END_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");                  //quyết toánThời gian,Nhập việnkỳgiữalàgọigiao diệnphụcvụcủaHệ thốnghiện tạiThời gian.
                bEAN.HOSPITAL_CODE = HospitalCode;   //yviệnMã(Sử dụngBảo hiểm y tếtrongtimđầuchoyviệnphútphốicủayviệnMã)
                bEAN.HOSPITAL_NAME = HospitalName;             //yviệnTên
                bEAN.PATIENT_CODE = patientobj.sfzh;              //tham sốbảo ngườiMã.điềnviếttham sốbảo ngườithânphầnnhận dạngsố(CMND/CCCDsố,Bảo hiểm y tếSố thẻ,Số thẻ sức khỏeđợi).
                bEAN.PATIENT_NAME = patientobj.xm;              //tham sốbảo ngườiHọ tên
                bEAN.PATIENT_SEX_CODE = patientobj.sex;          //Giới tínhMã
                string csrq = DateTime.Now.ToString("yyyy-MM-dd");
                if (patientobj.birth != null)
                {
                    csrq = DateTime.Parse(Convert.ToString(patientobj.birth)).ToString("yyyy-MM-dd");
                }
                bEAN.PATIENT_BIRTH = csrq;             //Ngày sinh
                bEAN.PERSON_TYPE_CODE = "-1";          // ngườiviênLoạiMã
                bEAN.BENEFIT_TYPE_CODE = brxxdata.xzlx ?? "310";         //nguy hiểmloạiMã loại
                bEAN.SEE_DOCTOR_TYPE_CODE = brxxdata.yllb;      //y tếLoạiMã
                bEAN.IS_WITHOUT_PLACE = brxxdata.ydjy;          //làKhôngkhác địa phươngthìy,0:Không;1:là
                bEAN.WITHOUT_PLACE_PERSON_TYPE = ""; //khác địa phương ngườiviênLoạiMã
                bEAN.WITHOUT_PLACE_UNIT_CODE = "";   //khác địa phươngtham sốbảođịadòngchínhkhulĩnh vựcMã
                bEAN.IN_HOSPITAL_ICD_CODE = brxxdata.ryzd;      //Chẩn đoán nhập việnMã,Ngoại trú:chínhcầnChẩn đoán;Nhập viện:Chẩn đoán nhập viện
                bEAN.IN_HOSPITAL_ICD_NAME = brxxdata.ryzdmc;      //Chẩn đoán nhập việnTên,Ngoại trú:chínhcầnChẩn đoán;Nhập viện:Chẩn đoán nhập viện
                bEAN.OUT_HOSPITAL_ICD_CODE = brxxdata.cyzd;     //Xuất việnMã chẩn đoán,Ngoại trú: lầncầnChẩn đoán;Nhập viện:Xuất việnChẩn đoán
                bEAN.OUT_HOSPITAL_ICD_NAME = brxxdata.cyzdmc;     //Xuất việnTên chẩn đoán,Ngoại trú: lầncầnChẩn đoán;Nhập viện:Xuất việnChẩn đoán
                bEAN.OUT_HOSPITAL_REASON = brxxdata.cyqk;       //Xuất việnnguyên nhân
                bEAN.ICD1_CODE = "";                 //
                bEAN.ICD1_NAME = "";                 //
                bEAN.ICD2_CODE = "";                 //
                bEAN.ICD2_NAME = "";                 //
                bEAN.ICD3_CODE = "";                 //
                bEAN.ICD3_NAME = "";                 //
                bEAN.ICD4_CODE = "";                 //
                bEAN.ICD4_NAME = "";                 //
                bEAN.ICD5_CODE = "";                 //
                bEAN.ICD5_NAME = "";                 //
                bEAN.ICD6_CODE = "";                 //
                bEAN.ICD6_NAME = "";                 //
                bEAN.ICD7_CODE = "";                 //
                bEAN.ICD7_NAME = "";                 //
                bEAN.ICD8_CODE = "";                 //
                bEAN.ICD8_NAME = "";                 //
                bEAN.ICD9_CODE = "";                 //
                bEAN.ICD9_NAME = "";                 //
                bEAN.ICD10_CODE = "";                //
                bEAN.ICD10_NAME = "";                //
                bEAN.ICD11_CODE = "";                //
                bEAN.ICD11_NAME = "";                //
                bEAN.ICD12_CODE = "";                //
                bEAN.ICD12_NAME = "";                //
                bEAN.ICD13_CODE = "";                //
                bEAN.ICD13_NAME = "";                //
                bEAN.ICD14_CODE = "";                //
                bEAN.ICD14_NAME = "";                //
                bEAN.ICD15_CODE = "";                //
                bEAN.ICD15_NAME = "";                //
                bEAN.ICD16_CODE = "";                //
                bEAN.ICD16_NAME = "";                //
                bEAN.ICD17_CODE = "";                //
                bEAN.ICD17_NAME = "";                //
                bEAN.ICD18_CODE = "";                //
                bEAN.ICD18_NAME = "";                //
                bEAN.ICD19_CODE = "";                //
                bEAN.ICD19_NAME = "";                //
                bEAN.ICD20_CODE = "";                //
                bEAN.ICD20_NAME = "";                //
                bEAN.INPATIENT_NO = zyh;              //Số nhập viện(Số khám ngoại trú)
                bEAN.OUT_HOSPITAL_OFFICE = brxxdata.ksmc;       //Xuất việnKhoa(Khu bệnh).Ngoại trúđiềnviếtKhám bệnhTên khoa,Nhập việnđiềnviếtXuất việnKhoa(Khu bệnh)Tên.điềnviếtyviện(HIS)Hệ thốngtrongbộcủaTên khoa
                bEAN.IN_HOSPITAL_TIME = DateTime.Parse(brxxdata.rqrq).ToString("yyyy-MM-dd HH:mm:ss");          //Thời gian nhập viện.Ngoại trúlàKhám bệnhThời gian,Nhập việnlàThời gian nhập viện
                bEAN.OUT_HOSPITAL_TIME = DateTime.Parse(brxxdata.cqrq).ToString("yyyy-MM-dd HH:mm:ss");         //Thời gian xuất viện.Ngoại trúlàKhám bệnhThời gian,Nhập việnlàThời gian xuất viện
                bEAN.SEE_DOCTOR_TIME = DateTime.Parse(brxxdata.rqrq).ToString("yyyy-MM-dd HH:mm:ss");      //Khám bệnhThời gian.Ngoại trúlàKhám bệnhThời gian,Nhập việnlàThời gian nhập viện
                bEAN.HEIGHT = 0;                    //thâncao(cm)
                bEAN.WEIGHT = 0;                    //thểtrọng(kg)
                bEAN.IS_TRAN_IN_HOSPITAL = "0";       //làKhôngchuyểnNhập viện0:Không;1:là
                bEAN.IS_PREGNANCY = "0";              //làKhôngmang thai0:Không;1:là
                bEAN.IS_SUCKLING = "0";               // Có phải thời kỳ cho con bú: 0: Không; 1: Có
                bEAN.HIS_BILL_NO = yzh;               //Thu phíphiếusố phiếu
                bEAN.HARD_ILL_FLAG = "0";             //làKhôngđặcbệnhchậmbệnhphiếucứCờ0:Không;1:là
                bEAN.HARD_ILL_CODE = "";             //chậmbệnhđặcbệnhMã.nếunếuphiếucứlàchậmbệnhđặcbệnhphiếucứ,cầnnhắccung cấpMãhoặccó thểđủtừMã chẩn đoántìmđếntương ứngcủachậmbệnhđặcbệnh
                bEAN.BILL_TOTAL_MONEY = djzje;          //phiếucứTổng số tiền
                bEAN.IM_TOTAL_MONEY = sbzje;            //Bảo hiểm y tếtrongTổng số tiền(đăng kýbáoGiá trị,Bảo hiểm y tếcầntrảchoyviệncủatiền,khôngvàodòngBảo hiểm y tếbáobáncủachínhphiếuSố tiềnlà0)
                bEAN.COL1 = "";                      //
                bEAN.COL2 = "";                      //
                bEAN.REMARK = "";
                bEAN.ROWS = rOWs;
                Domain.DTO.OutputDto.MESSAGE mESSAGE = new Domain.DTO.OutputDto.MESSAGE();
                mESSAGE.VERSION = "1";
                pre.MESSAGE = mESSAGE;
                dATA.BEAN = bEAN;
                pre.DATA = dATA;
                string responsexml = pre.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                var param = "serviceId{=}AUDIT00001{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var logstr = AuditInterface(responsexml, orgId, zyh, zlysobj.ysgh, zlysobj.ysmc, param, yzh, "医嘱审核", GetMAC);//giao diện
                return logstr;
            }
            catch (Exception e)
            {

                return "" + e.Message;
            }

        }
        /// <summary>
        /// bệnhánPhê duyệt
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        public string GetBashData(string orgId, string zyh, string rygh, string username, string GetMAC)
        {
            try
            {
                string HospitalCode = ConfigurationManager.AppSettings["OrganizeCodeSd"];
                string HospitalName = ConfigurationManager.AppSettings["HospitalName"];
                var sqlpar = new List<SqlParameter>();
                var sql = @"exec usp_zy_QueryBash @orgId,@zyh,@cryzt,@yljgdm,@yljgmc";
                sqlpar.Add(new SqlParameter("@orgId", orgId));
                sqlpar.Add(new SqlParameter("@zyh", zyh));
                sqlpar.Add(new SqlParameter("@cryzt", "1"));
                sqlpar.Add(new SqlParameter("@yljgdm", HospitalCode));
                sqlpar.Add(new SqlParameter("@yljgmc", HospitalName));
                MedicalBEAN me = FirstOrDefault<MedicalBEAN>(sql.ToString(), sqlpar.ToArray());
                if (me == null)
                {
                    return "";
                }
                MedicalDTO medicalDTO = new MedicalDTO();
                MedicalMESSAGE mESSAGE = new MedicalMESSAGE();
                mESSAGE.VERSION = "1";
                medicalDTO.MESSAGE = mESSAGE;
                MedicalDATA dATA = new MedicalDATA();
                dATA.BEAN = me;
                medicalDTO.DATA = dATA;
                string responsexml = medicalDTO.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                var ysgh = "000000";
                var ysmc = "Quản trị viên";
                var param = "serviceId{=}MR00001{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var logstr = AuditInterface(responsexml, orgId, zyh, ysgh, ysmc, param, "", "病案审核", GetMAC);//giao diện
                return logstr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// DIPgiao diện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        public string GetDrgData(string orgId, string zyh, string rygh, string username, string GetMAC)
        {
            try
            {
                //DIPgiao diệnNhập việnThông tin cơ bản
                string HospitalCode = ConfigurationManager.AppSettings["OrganizeCodeSd"];
                string HospitalName = ConfigurationManager.AppSettings["HospitalName"];
                var jbxxsqlpar = new List<SqlParameter>();
                var JBXXsql = @"exec usp_zy_QueryDrg_JBXX @orgId,@zyh,@yljgdm,@yljgmc";
                jbxxsqlpar.Add(new SqlParameter("@orgId", orgId));
                jbxxsqlpar.Add(new SqlParameter("@zyh", zyh));
                jbxxsqlpar.Add(new SqlParameter("@yljgdm", HospitalCode));
                jbxxsqlpar.Add(new SqlParameter("@yljgmc", HospitalName));
                DRGDIPJBXX dRGDIPJBXX = FirstOrDefault<DRGDIPJBXX>(JBXXsql.ToString(), jbxxsqlpar.ToArray());
                //DIPgiao diệnNhập việnthông tin
                var zyxxsqlpar = new List<SqlParameter>();
                var zyxxsql = @"exec usp_zy_QueryDrg_ZYXX @orgId,@zyh";
                zyxxsqlpar.Add(new SqlParameter("@orgId", orgId));
                zyxxsqlpar.Add(new SqlParameter("@zyh", zyh));
                DRGDIPZYXX dRGDIPZyxx = FirstOrDefault<DRGDIPZYXX>(zyxxsql.ToString(), zyxxsqlpar.ToArray());
                //DIPgiao diệnThu phíthông tin
                var sfxxsqlpar = new List<SqlParameter>();
                var sfxxsql = @"exec usp_zy_QueryDrg_SFXX @orgId,@zyh";
                sfxxsqlpar.Add(new SqlParameter("@orgId", orgId));
                sfxxsqlpar.Add(new SqlParameter("@zyh", zyh));
                sfxxsqlpar.Add(new SqlParameter("@yljgdm", HospitalCode));
                sfxxsqlpar.Add(new SqlParameter("@yljgmc", HospitalName));
                DRGDIPSFXX dRGDIPSFXX = FirstOrDefault<DRGDIPSFXX>(sfxxsql.ToString(), sfxxsqlpar.ToArray());
                //DIPgiao diệnKhácthông tin
                DRGDIPQTXX dRGDIPQTXX = new DRGDIPQTXX();
                dRGDIPQTXX.DDYLJG_TBBM_BM = dRGDIPZyxx.CYKB_BM;
                dRGDIPQTXX.DDYLJG_TBBM_MC = dRGDIPZyxx.CYKB_MC;
                dRGDIPQTXX.DDYLJG_TBR_BM = rygh;
                dRGDIPQTXX.DDYLJG_TBR_MC = username;
                dRGDIPQTXX.YBJBJG_BM = HospitalCode;
                dRGDIPQTXX.YBJBJG_MC = HospitalName;
                dRGDIPQTXX.YBJBJG_JBR_BM = "-";
                dRGDIPQTXX.YBJBJG_JBR_MC = "-";
                DRGDIPBEAN me = new DRGDIPBEAN();
                me.JBXX = dRGDIPJBXX;
                me.ZYXX = dRGDIPZyxx;
                me.SFXX = dRGDIPSFXX;
                me.QTXX = dRGDIPQTXX;
                DRGDIPDTO medicalDTO = new DRGDIPDTO();
                DRGDIPMESSAGE mESSAGE = new DRGDIPMESSAGE();
                mESSAGE.VERSION = "1";
                medicalDTO.MESSAGE = mESSAGE;
                DRGDIPDATA dATA = new DRGDIPDATA();
                dATA.BEAN = me;
                medicalDTO.DATA = dATA;
                string responsexml = medicalDTO.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                var ysgh = "000000";
                var ysmc = "Quản trị viên";
                var param = "serviceId{=}AUDIT00101{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var logstr = AuditInterface(responsexml, orgId, zyh, ysgh, ysmc, param, "", "DIP接口", GetMAC);//giao diện
                return logstr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// Phê duyệtphiếucứXóa
        /// </summary>
        /// <returns></returns>
        public string DeletePriorReview(string zyh, string yzid, string yzlx, string OrganizeId, string GetMAC)
        {
            try
            {
                var sqlstr = new StringBuilder();
                var par = new List<SqlParameter>();
                sqlstr = sqlstr.Append(@"select * from (select shlog.outlsh,shlog.yzh,cqyz.Id from [dbo].[shjk_log] shlog
inner join zy_cqyz cqyz on shlog.yzh=cqyz.yzh
 where cqyz.OrganizeId=@orgId
union all
select shlog.outlsh,shlog.yzh,lsyz.Id from [dbo].[shjk_log] shlog
inner join zy_lsyz lsyz on shlog.yzh=lsyz.yzh where lsyz.OrganizeId=@orgId) a
where id=@yzid");
                par.Add(new SqlParameter("@yzid", yzid));
                par.Add(new SqlParameter("@orgId", OrganizeId));
                var delrc = this.FirstOrDefault<DelReviewDTO>(sqlstr.ToString(), par.ToArray());
                if (delrc == null)
                {
                    return "";
                }
                DeletePriorReviewDTO medicalDTO = new DeletePriorReviewDTO();
                DELPRMESSAGE mESSAGE = new DELPRMESSAGE();
                mESSAGE.VERSION = "1";
                medicalDTO.MESSAGE = mESSAGE;
                DELPRDATA dATA = new DELPRDATA();
                DELPRBEAN me = new DELPRBEAN();
                me.BILL_ID = delrc.outlsh;
                me.HIS_BILL_ID = delrc.yzh;
                dATA.BEAN = me;
                medicalDTO.DATA = dATA;
                string responsexml = medicalDTO.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                var ysgh = "000000";
                var ysmc = "Quản trị viên";
                var param = "serviceId{=}AUDIT00002{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var logstr = AuditInterface(responsexml, OrganizeId, zyh, ysgh, ysmc, param, "", "单据删除", GetMAC);//giao diện
                return logstr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string GetDiagnoseData()
        {
            try
            {
                DiagnoseDTO medicalDTO = new DiagnoseDTO();
                DiagnoseSSAGE mESSAGE = new DiagnoseSSAGE();
                mESSAGE.VERSION = "1";
                medicalDTO.MESSAGE = mESSAGE;
                DiagnoseDATA dATA = new DiagnoseDATA();
                DiagnoseBEAN me = new DiagnoseBEAN();
                me.ICD_CODE = "";
                me.ICD_NAME = "流行性感冒";
                dATA.BEAN = me;
                medicalDTO.DATA = dATA;
                string responsexml = medicalDTO.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                var orgId = "";
                var zyh = "00301";
                var ysgh = "000000";
                var ysmc = "Quản trị viên";
                var param = "serviceId{=}ICD00001{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var logstr = AuditInterface(responsexml, orgId, zyh, ysgh, ysmc, param, "", "诊断查询", "");//giao diện
                return logstr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string AuditInterface(string responsexml, string OrganizeId, string zyh, string yhgh, string ysmc, string param, string yzh, string jkType, string GetMAC)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                try
                {
                    string macAddresses = null;
                    try
                    {
                        if (GetMAC == null || GetMAC == "")
                        {
                            return "MACĐịa chỉLấythất bại！vui lòngKích hoạtBảo hiểm y tếnhỏchương trình！";
                        }
                        else
                        {
                            macAddresses = GetMAC;
                        }
                    }
                    catch (Exception ex)
                    {
                        return "MACĐịa chỉLấythất bại！vui lòngKích hoạtBảo hiểm y tếnhỏchương trình！" + ex.Message + "";
                    }
                    param += macAddresses;
                    var responseXML = new WebServiceClient().service(param, responsexml);
                    var jsonout = responseXML.XmlDeSerialize<OutXMLDTO>();
                    var outlsh = "";
                    if (yzh != null && yzh != "")
                    {
                        outlsh = jsonout.DATA.BEAN.BILL_ID;
                    }

                    var shjkldrzEntity = new shjkldrzEntity
                    {
                        OrganizeId = OrganizeId,
                        jydm = "5100",
                        mzzyh = zyh,
                        Type = "2",
                        jkType = jkType,
                        yzh = yzh,
                        outlsh = outlsh,
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    shjkldrzEntity.Create(true);
                    db.Insert(shjkldrzEntity);
                    db.Commit();
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
                return "";
            }
        }

        #region kỹ thuật y họcKhoaThực hiện
        /// <summary>
        /// kỹ thuật y họcThực hiện
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <param name="orgId"></param>
        /// <param name="zxr"></param>
        public void jyjcExec(List<jyjcExecReq> jyjclist, string orgId, string zxr)
        {
            List<XtjyjcExecEntity> entityList = new List<XtjyjcExecEntity>();
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    DateTime date = DateTime.Now;
                    foreach (var item in jyjclist)
                    {
                        XtjyjcExecEntity entity = new XtjyjcExecEntity();
                        entity.OrganizeId = orgId;
                        entity.mzzyh = item.mzzyh;
                        entity.hzlx = item.hzlx;
                        entity.fylx = item.fylx;
                        entity.xm = item.xm;
                        entity.sqdlx = item.sqdlx;
                        entity.sqdh = item.sqdh;
                        entity.sl = item.sl;
                        entity.dj = item.dj;
                        entity.zxr = zxr;
                        entity.zxrq = date;
                        entity.zxks = item.zxks;
                        entity.kdks = item.kdks;
                        entity.je = item.je;
                        entity.ztId = item.ztId;
                        entity.ztmc = item.ztmc;
                        entity.kdrq = item.kdrq;
                        entity.kdys = item.kdys;
                        entity.dw = item.dw;
                        entity.gg = item.gg;
                        entity.shr = item.shr;
                        entity.Create(true);
                        entityList.Add(entity);
                    }
                    if (entityList != null && entityList.Count > 0)
                    {
                        foreach (var item in entityList)
                        {
                            db.Insert(item);
                        }
                    }

                    db.Commit();
                }
            }
            catch (Exception e)
            {
                throw new FailedException("Thực hiệnthất bại，" + e.InnerException);
            }
        }
        /// <summary>
        /// kỹ thuật y họcThực hiệnSửa hạng mụclàKhôngtínhphíTrạng thái(isjf)
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        public void UpdatejyjcExecIsjf(List<string> jyjclist, string orgId, string czr,string status)
        {
            string sql = @"  update zy_fymxk set isjf=@status ,LastModifierCode=@czr,LastModifyTime=getdate()
                                 where yzxh in (select Id from zy_lsyz  with(nolock)  where zt=1 and OrganizeId=@orgId 
                                     and sqdh IN ( SELECT  col FROM dbo.f_split(@str, ',') )) ";
             var d=ExecuteSqlCommand(sql, new[] { new SqlParameter("@orgId", orgId),
                     new SqlParameter("@czr",czr),
                     new SqlParameter("@status",status),
                     new SqlParameter("@str", string.Join(",", jyjclist))});
        }
        /// <summary>
        /// Thu hồiThực hiện
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        public void CancaljyjcExec(List<string> jyjclist, string orgId, string czr)
        {
            string sql = @"  DELETE FROM  xt_jyjcexec 
                                 WHERE zt=1 and OrganizeId=@orgId 
                                       and sqdh IN ( SELECT  col FROM dbo.f_split(@str, ',') ) 

                            DELETE FROM  " + DbNames.Name("SettDb", "NewtouchHIS_Sett") + @"..zy_xmjfb
                                WHERE yzwym in (select Id from zy_lsyz with(nolock) where zt=1 and OrganizeId=@orgId 
                                 and sqdh IN ( SELECT  col FROM dbo.f_split(@str, ',') ))
";
            var d=ExecuteSqlCommand(sql, new[] { new SqlParameter("@orgId", orgId),
                    new SqlParameter("@str", string.Join(",", jyjclist))});
        }
        #endregion
        /// <summary>
        /// kiểm tralàKhônglàBệnh mạn tính
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zdcode"></param>
        /// <returns></returns>
        public string GetmxbzdList(string orgId, string zdcode)
        {
            string sql = @" select zdmc from [NewtouchHIS_Base]..xt_zd where mxb='1' and zdcode in (select * from f_split(@zdcode,','))  and zt='1' and OrganizeId=@orgId";
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@zdcode", zdcode));
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            string listjg = this.FirstOrDefault<string>(sql, sqlpar.ToArray());
            return listjg;
        }
        /// <summary>
        /// mởlậpSố lượnghạnchế
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zdcode"></param>
        /// <returns></returns>
        public string GetxzklsList(string orgId, YpxzkldataDTO Ypxzkldata)
        {
            string message = "";
            foreach (var item in Ypxzkldata.cfdata)
            {
                string sql = @" select xs.dcxl,xs.mbxl,yp.ypmc from  [NewtouchHIS_Base]..xt_ypsx xs
 left join [NewtouchHIS_Base]..xt_yp yp on xs.ypId=yp.ypId and xs.OrganizeId=yp.OrganizeId and xs.zt='1'
 where  xs.ypCode=@ypCode and xs.OrganizeId=@orgId";
                var sqlpar = new List<SqlParameter>();
                sqlpar.Add(new SqlParameter("@ypCode", item.ypCode));
                sqlpar.Add(new SqlParameter("@orgId", orgId));
                var listjg = this.FirstOrDefault<YpxzklDTO>(sql, sqlpar.ToArray());
                if (listjg != null)
                {
                    if (Ypxzkldata.mbzd == "1")
                    {
                        if (listjg.mbxl != null)
                        {
                            if (item.sl > listjg.mbxl)
                            {
                                message += listjg.ypmc + "củaBệnh mạn tínhnhấtlớnmởlậpsốlà【" + listjg.mbxl + "】";
                            }
                            continue;
                        }
                    }
                    if (listjg.dcxl != null && item.sl > listjg.dcxl)
                    {
                        message += listjg.ypmc + "củanhấtlớnmởlậpsốlà【" + listjg.dcxl + "】";
                    }
                }
            }
            return message;
        }
        public string addcqyz(string zyh, string yzh, string zh, List<YzbindingfeeVo> ItemFeeVO, string OrganizeId, string usercode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == zyh && a.zt == "1" && a.OrganizeId == OrganizeId).FirstOrDefault();

                int cqpx = 1;
                int lspx = 1;
                foreach (var item in ItemFeeVO)
                {
                    if (item.yzxz == "2")
                    {
                        InpatientLongTermOrderEntity newcqyz = new InpatientLongTermOrderEntity();
                        newcqyz.Id = Guid.NewGuid().ToString();
                        newcqyz.OrganizeId = OrganizeId;
                        newcqyz.zyh = zyh;
                        if (item.yzlx == "1"&& zh!=null& zh!="")
                        {
                            newcqyz.zh = int.Parse(zh);
                        }
                        newcqyz.WardCode = patInfo.WardCode;
                        newcqyz.DeptCode = patInfo.DeptCode;
                        newcqyz.ysgh = patInfo.ysgh;
                        newcqyz.pcCode = item.pcCode;
                        newcqyz.zxcs = item.zxcs;
                        newcqyz.zxzq = item.zxzq;
                        newcqyz.zxzqdw = item.zxzqdw;
                        newcqyz.zdm = null;
                        newcqyz.xmdm = item.sfxm;
                        newcqyz.xmmc = item.sfxmmc;
                        newcqyz.yzzt = 1;
                        newcqyz.dw = item.dw;
                        newcqyz.zbbz = 0;
                        newcqyz.sl = int.Parse(item.sl.ToString());
                        newcqyz.dwlb = 4;
                        newcqyz.yzlx = (int)EnumYzlx.yyhc;
                        //int yzlx = 1;//thông qualớp nổitruyềnquađếncủalà1,2 1làThuốc 2làMục
                        //if (item.yzlx == "1")
                        //{
                        //    yzlx = 2;
                        //}
                        //else
                        //{
                        //    yzlx = 5;
                        //}
                        //newcqyz.yzlx = yzlx;
                        newcqyz.tzysgh = null;
                        newcqyz.tzsj = null;
                        newcqyz.tzr = null;
                        newcqyz.tzyy = null;
                        newcqyz.shsj = DateTime.Now;
                        newcqyz.shr = usercode;
                        newcqyz.kssj = DateTime.Now;
                        newcqyz.zxsj = null;
                        newcqyz.zxr = null;
                        newcqyz.dcr = null;
                        newcqyz.ypjl = item.sl;
                        newcqyz.ypgg = item.gg;
                        newcqyz.ztnr = null;
                        newcqyz.yznr = item.sfxmmc + " " + item.sl.ToString() + item.dw + " " + item.dlmc + " " + item.pcmc;
                        newcqyz.ypyfdm = null;
                        newcqyz.zxksdm = item.yfdm;
                        newcqyz.printyznr = null;
                        newcqyz.CreateTime = DateTime.Now;
                        newcqyz.CreatorCode = usercode;
                        newcqyz.LastModifyTime = null;
                        newcqyz.LastModifierCode = null;
                        newcqyz.zt = "1";
                        newcqyz.hzxm = patInfo.xm;
                        newcqyz.bw = null;
                        newcqyz.zxsjd = null;
                        newcqyz.nlmddm = null;
                        newcqyz.kssReason = null;
                        newcqyz.ds = null;
                        if (yzh != null && yzh != "")
                        {
                            newcqyz.Px = (getmaxpx(yzh, item.yzxz, OrganizeId) + 1);
                            newcqyz.yzh = yzh;
                        }
                        else
                        {
                            var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                            var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_cqyz.yzh", OrganizeId);
                            newcqyz.yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
                        }
                        newcqyz.zzfbz = 0;
                        newcqyz.yztag = null;
                        newcqyz.isjf = 1;
                        newcqyz.zxing = null;
                        newcqyz.ispscs = "0";
                        newcqyz.ztmc = null;
                        newcqyz.yfztbs = null;
                        newcqyz.ztsl = null;
                        newcqyz.dcztbs = null;
                        newcqyz.isfsyz = "1";
                        if (item.yzlx == "1"&& (yzh == null && yzh == ""))
                        {
                            newcqyz.Px = cqpx;
                            cqpx += 1;
                        }
                         _InpatientLongTermOrderRepo.SubmitForm(newcqyz, null);
                    }
                    else
                    {
                        InpatientSTATOrderEntity newlsyz = new InpatientSTATOrderEntity();
                        newlsyz.Id = Guid.NewGuid().ToString();
                        newlsyz.OrganizeId = OrganizeId;
                        newlsyz.zyh = zyh;
                        if (item.yzlx == "1" && zh != null & zh != "")
                        {
                            newlsyz.zh = int.Parse(zh);
                        }
                        newlsyz.WardCode = patInfo.WardCode;
                        newlsyz.DeptCode = patInfo.DeptCode;
                        newlsyz.ysgh = patInfo.ysgh;
                        newlsyz.pcCode = item.pcCode;
                        newlsyz.zxcs = item.zxcs;
                        newlsyz.zxzq = item.zxzq;
                        newlsyz.zxzqdw = item.zxzqdw;
                        newlsyz.zdm = null;
                        newlsyz.xmdm = item.sfxm;
                        newlsyz.xmmc = item.sfxmmc;
                        newlsyz.yzzt = 1;
                        newlsyz.dw = item.dw;
                        newlsyz.zbbz = 0;
                        newlsyz.sl = int.Parse(item.sl.ToString());
                        newlsyz.dwlb = 4;
                        //int yzlx = 1;//thông qualớp nổitruyềnquađếncủalà1,2 1làThuốc 2làMục
                        newlsyz.yzlx = (int)EnumYzlx.yyhc;
                        //if (item.yzlx == "1")
                        //{
                        //    yzlx = 2;
                        //}
                        //else
                        //{
                        //    yzlx = 5;
                        //}
                        //newlsyz.yzlx = yzlx;
                        newlsyz.shsj = DateTime.Now;
                        newlsyz.shr = usercode;
                        newlsyz.kssj = DateTime.Now;
                        newlsyz.zxsj = null;
                        newlsyz.zxr = null;
                        newlsyz.ypjl = item.sl;
                        newlsyz.ypgg = item.gg;
                        newlsyz.ztnr = null;
                        newlsyz.yznr = item.sfxmmc + " " + item.sl.ToString() + item.dw + " " + item.dlmc + " " + item.pcmc;
                        newlsyz.ypyfdm = null;
                        newlsyz.zxksdm = item.yfdm;
                        newlsyz.printyznr = null;
                        newlsyz.CreateTime = DateTime.Now;
                        newlsyz.CreatorCode = usercode;
                        newlsyz.LastModifyTime = null;
                        newlsyz.LastModifierCode = null;
                        newlsyz.zt = "1";
                        newlsyz.hzxm = patInfo.xm;
                        newlsyz.bw = null;
                        newlsyz.zxsjd = null;
                        newlsyz.nlmddm = null;
                        newlsyz.kssReason = null;
                        newlsyz.ds = null;
                        if (yzh != null && yzh != "")
                        {
                            newlsyz.Px = (getmaxpx(yzh, item.yzxz, OrganizeId) + 1);
                            newlsyz.yzh = yzh;
                        }
                        else
                        {

                            var yzhPart1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                            var yzhPart2 = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.yzh", OrganizeId);
                            newlsyz.yzh = string.Format("{0}{1}", yzhPart1, yzhPart2);
                        }
                        newlsyz.zzfbz = 0;
                        newlsyz.yztag = null;
                        newlsyz.isjf = 1;
                        newlsyz.zxing = null;
                        newlsyz.ispscs = "0";
                        newlsyz.ztmc = null;
                        newlsyz.yfztbs = null;
                        newlsyz.ztsl = null;
                        newlsyz.dcztbs = null;
                        newlsyz.isfsyz = "1";
                        if (item.yzlx == "1"&&( yzh == null || yzh == ""))
                        {
                            newlsyz.Px = lspx;
                            lspx += 1;
                        }
                        _inpatientSTATOrderRepo.SubmitForm(newlsyz, null);
                    }
                }
                db.Commit();
            }
            return "";
        }
        public int getmaxpx(string yzh,string yzxz,string orgId) {
            string sql = @" select isnull(max(px),0) px from zy_lsyz where yzh=@yzh and zt='1' and OrganizeId=@orgId ";
            if (yzxz=="2")
            {
                sql = @" select isnull(max(px),0) px from zy_cqyz where yzh=@yzh and zt='1' and OrganizeId=@orgId ";
            }
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@yzh", yzh));
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            return this.FirstOrDefault<int>(sql, sqlpar.ToArray());
        }
        public PatientMedicalDTO GetlsorcqyzData(string zyh, string yzid, string orgId)
        {
            string sql = @" 		select * from (select xmmc,'2' yzxz,zh,yzh,brxx.xm hzxm,brxx.BedCode bedCode,brxx.DeptCode,yz.px from 
		[Newtouch_CIS].[dbo].zy_brxxk brxx
	left join [Newtouch_CIS].[dbo].[zy_cqyz] yz on brxx.zyh=yz.zyh and yz.OrganizeId=brxx.OrganizeId
	 where (yz.id=@yzid or @yzid='')  and yz.OrganizeId=@orgId and brxx.zyh=@zyh
	union all
  select xmmc,'1' yzxz,zh,yzh,brxx.xm hzxm,brxx.BedCode bedCode,brxx.DeptCode,yz.px from 
		[Newtouch_CIS].[dbo].zy_brxxk brxx
	left join [Newtouch_CIS].[dbo].[zy_lsyz] yz on brxx.zyh=yz.zyh and yz.OrganizeId=brxx.OrganizeId
	 where (yz.id=@yzid or @yzid='')  and yz.OrganizeId=@orgId and brxx.zyh=@zyh
	 ) a  order by a.px desc";
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@zyh", zyh));
            sqlpar.Add(new SqlParameter("@yzid", yzid));
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            return this.FirstOrDefault<PatientMedicalDTO>(sql, sqlpar.ToArray());
        }
        public string DeleteBind(string zyh, string yzid, string yzxz, string orgId)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {

                    if (yzxz == "1")//Y lệnh tạm thời
                    {
                        InpatientSTATOrderEntity lsyzdata = db.IQueryable<InpatientSTATOrderEntity>().Where(a => a.zyh == zyh && a.zt == "1" && a.Id == yzid && a.OrganizeId == orgId&&a.isfsyz != null).FirstOrDefault();
                        lsyzdata.zt = "0";
                        _inpatientSTATOrderRepo.SubmitForm(lsyzdata, yzid);
                    }
                    else
                    {
                        InpatientLongTermOrderEntity cqyzdata = db.IQueryable<InpatientLongTermOrderEntity>().Where(a => a.zyh == zyh && a.zt == "1" && a.Id == yzid && a.OrganizeId == orgId && a.isfsyz != null).FirstOrDefault();
                        cqyzdata.yzzt = Convert.ToInt32(EnumYzzt.TZ);
                        _InpatientLongTermOrderRepo.SubmitForm(cqyzdata, yzid);
                    }
                    db.Commit();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "";
        }

        /// <summary>
        /// TheoSố nhập việnLấyLIS/PACSbáo cáoHoàn thànhSố lượng
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yzh"></param>
        /// <returns></returns>
        public int CountLISztzy(string orgId, string zyh)
        {
            string sql = @" 
				select count(*)  from [Newtouch_CIS].[dbo].[zy_lsyz] where zyh =@zyh and syncStatus=2  and organizeId=@orgId and zt='1'";
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@zyh", zyh));
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            return this.FirstOrDefault<int>(sql, sqlpar.ToArray());
        }

        public List<ypyfdataDto> GetYfData(string orgId)
        {
            string sql = @" 
				select yfbmCode,yfbmmc,mzzybz from NewtouchHIS_Base..xt_yfbm where organizeId=@orgId and zt='1' and fybz='1' ";
            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            return FindList<ypyfdataDto>(sql, sqlpar.ToArray());
        }
    }
}

