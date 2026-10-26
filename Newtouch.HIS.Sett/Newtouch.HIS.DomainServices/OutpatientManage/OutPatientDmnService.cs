using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Proxy.Log;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.Tools.DB;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientDmnService : DmnServiceBase, IOutPatientDmnService
    {
        private readonly IFinancialInvoiceRepo _financialInvoiceEntityRepository;
        private readonly IOutpatientRegistNonAttendanceRepo _outPatientBackNumRepo;
        private readonly IOutpatientRegistRepo _outpatientRegRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        #region 门诊记账引用
        private readonly IOutPatChargeDmnService _outPatChargeDmSer;
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;
        private readonly IOutpatientItemRepo _outpatientItemRepo;
        #endregion


        #region  门诊收费 Vision-1.3 门诊记账的第三个版本 
        private readonly IOutpatientSettlementRepo _outpatientSettlementRepo;
        private readonly IOutpatientSettlementDetailRepo _outpatientSettlementDetailRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        #endregion

        #region optima记账 Vision-1.2 门诊记账第二个版本
        private readonly IHospItemBillingRepo _hospItemBillingRepo;
        private readonly IHospDrugBillingRepo _hospDrugBillingRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        #endregion

        public OutPatientDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 挂号界面info

        /// <summary>
        /// theo loại, BHYTĐ ặc biệt chờ 遇, 适 dùng BHYT biện pháp dấu hiệu, phạm vi刷选 lấy hiệu lực tính chất bệnh nhân by caishanshan 20161215
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ghlx">Loại đ ăng ký khám</param>
        /// <param name="ybtsdy">BHYTĐ ặc biệt chờ 遇 dấu hiệu </param>
        /// <param name="syybbf">适 dùng BHYT biện pháp dấu hiệu </param>
        /// <param name="fw">Phạm viLọc</param>
        /// <param name="brxzList"></param>
        /// <returns></returns>
        public List<SysPatiNatureEntity2VO> GetValidBrxzList(string text, string ghlx = "", string ybtsdy = "", string syybbf = "", string fw = "", List<string> brxzList = null)
        {
            //Bắt đ ầu theo từ khóa检索 bản ghi 
            string brxzs = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                          select brxz from xt_brxz where ZT=1 and OrganizeId=@orgId
                         ");
            //Loại đ ăng ký khám
            if (!string.IsNullOrEmpty(ghlx))
            {
                //if (!(ghlx == "0" || ghlx == "2"))
                //{
                //    strSql.Append(" and brxzlb <> 1 ");
                //}
            }
            //BHYTĐ ặc biệt chờ 遇
            if (!string.IsNullOrEmpty(ybtsdy))
            {
                strSql.Append(" and ybtsdy =@ybtsdy");
            }
            //Phạm vi
            if (!string.IsNullOrEmpty(fw))
            {
                strSql.Append(" and (mzzybz = '0' or mzzybz =@fw)");
            }
            //Nếu chỉ đ ịnh tính chất bệnh nhân,另 dòng xử lý
            if (brxzList != null && brxzList.Count > 0)
            {
                foreach (string brxz in brxzList)
                {
                    brxzs += "'" + brxz + "',";
                }
                brxzs = brxzs.TrimEnd(',');
                strSql.Append(" and brxz not in (@brxzs)");
            }
            strSql.Append(" and (brxz like '%" + text + "%' or py like '" + text + "%' or brxz+brxzmc =@text) ");

            SqlParameter[] param =
            {
                new SqlParameter("@ybtsdy",ybtsdy),
                new SqlParameter("@fw",Enummzzybz.mz),
                new SqlParameter("@brxzs",brxzs),
                new SqlParameter("@text",text),
                new SqlParameter("@orgId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return this.FindList<SysPatiNatureEntity2VO>(strSql.ToString(), param);
        }

        #endregion

        /// <summary>
        /// theo số thẻ lấy cơ bản thông tin by caishanshan
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public OutPatientBasicInfoVO GetBasicInfoByCardNo(string kh)
        {
            var strSql = new StringBuilder(@"
             SELECT  CardNo AS kh ,
                        xt_brjbxx.patid ,
                        pzh ,
                        xt_brjbxx.brxz ,
                        xt_brxz.brxzmc ,
                        xt_brxz.brxzbh ,
                        '' db ,
                        '' dbzd ,
                        cardtype ,
                        xm ,
                        xb ,
                        zjh ,
                                   -- csny ,
                        CONVERT(VARCHAR(100), csny, 23) csny ,
                        CAST( FLOOR(datediff(DY,csny,getdate())/365.25) as SMALLINT) nl,
                        blh ,
                        dh ,
                       cast(dybh as varchar(8)) dy,
                        dybh,
                        hf,
                        zjlx,
                        cs_sheng,
                        cs_shi,
                        cs_xian,
                        xian_dz,
                        xian_sheng,
                        xian_shi,
                        xian_xian,
                        hu_dz,
                        hu_sheng,
                        hu_shi,
                        hu_xian,
                        xt_brjbxx.phone,
                        xt_brjbxx.brly,
	                    xt_brjbxx.jjllrgx  lxrgx,
		                xt_brjbxx.jjlldh lxrdh,
		                xt_brjbxx.jjllr lxr
                FROM    xt_card
                        left JOIN xt_brjbxx ON xt_card.patid = xt_brjbxx.patid and xt_brjbxx.OrganizeId=@orgId
                        left JOIN xt_brxz ON xt_brjbxx.brxz = xt_brxz.brxz and xt_brxz.OrganizeId=@orgId 
                WHERE   xt_card.OrganizeId=@orgId
                        AND cardno=@kh
            ");// tại đ ây sql modified by sunny không thể 完全hiển thị tại cơ bản thông tintrang trên 
            DbParameter[] param =
            {
                new SqlParameter("@kh",kh),
                new SqlParameter("@orgId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<OutPatientBasicInfoVO>(strSql.ToString(), param).FirstOrDefault();
        }

        /// <summary>
        /// theo mã nhân viên lấy số hóa đ ơn by caishanshan 20161210
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetInvoiceListByEmpNo(string userCode, string orgId)
        {
            string result = null;
            var list = _financialInvoiceEntityRepository.GetListByUserCode(userCode, orgId);
            if (list.Count < 1)
            {
                LogCore.Fatal("GetInvoiceListByEmpNo.GetListByUserCode Fatal", message: string.Format("userCode:{0};orgId：{1};list:{2}", userCode, orgId, list.ToJson()));
                throw new FailedCodeException("SYS_NO_ASSIGNED_INVOICE_NUMBER_PLEASE_CONTACT_THE_ADMINISTRATOR");
            }
            foreach (var item in list)
            {
                if (item.dqfph < 1)
                {
                    if (!ExistsForInvoiceNo(item.szm + item.qsfph, orgId))
                    {
                        result = item.szm + item.qsfph;
                        break;
                    }
                }
                else if (item.dqfph > 0 && item.dqfph >= item.qsfph && item.dqfph < item.jsfph)
                {
                    if (!ExistsForInvoiceNo(item.szm + (item.dqfph + 1), orgId))
                    {
                        result = item.szm + (item.dqfph + 1);
                        break;
                    }
                }
            }
            return result;

            //AppLogger.Instance.InfoFormat(" theo rybh lấy số hóa đ ơnstart,rybh:{0}", userCode);
            //string result = null;
            //var list = _financialInvoiceEntityRepository.GetListByUserCode(userCode);
            //foreach (var item in list)
            //{
            // AppLogger.Instance.InfoFormat("Đ ã phân phối bản ghi hóa đ ơn:qsfph{0}jsfph{1}dqfph{2}", item.qsfph, item.jsfph, item.dqfph);
            //}
            //if (list.Count < 1)
            //{
            //    throw new FailedCodeException("SYS_NO_ASSIGNED_INVOICE_NUMBER_PLEASE_CONTACT_THE_ADMINISTRATOR");
            //}
            //foreach (var item in list)
            //{
            //    if (item.dqfph < 1)
            //    {
            //        if (!ExistsForInvoiceNo(item.szm + item.qsfph))
            //        {
            //            result = item.szm + item.qsfph;
            // AppLogger.Instance.InfoFormat("匹配 đ ến của bản ghi:fpdm:{0}result:{1}", item.fpdm, result);
            //            break;
            //        }
            //    }
            //    else if (item.dqfph > 0 && item.dqfph >= item.qsfph && item.dqfph < item.jsfph)
            //    {
            //        if (!ExistsForInvoiceNo(item.szm + item.dqfph + 1))
            //        {
            //            result = item.szm + (item.dqfph + 1);
            // AppLogger.Instance.InfoFormat("匹配 đ ến của bản ghi:fpdm:{0}result:{1}", item.fpdm, result);
            //            break;
            //        }
            //    }
            //}
            //return result;
        }

        /// <summary>
        /// check số hóa đ ơn có hay không đ ược sử dụng qua, Đ ã đ ược sử dụng qua quay lạitrue
        /// </summary>
        /// <param name="invoiceNo">Số hóa đ ơn</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool ExistsForInvoiceNo(string invoiceNo, string orgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                           select 1 from 
                            (
                            --select top 1 fph from zy_js where fph = @invoiceNo and OrganizeId=@orgId
                            --union 
                            select top 1 fph from mz_js where fph = @invoiceNo and OrganizeId=@orgId
                            ) t
                        ");
            SqlParameter[] param =
            {
                new SqlParameter("@invoiceNo",invoiceNo),
                new SqlParameter("@orgId",orgId)
            };

            return this.FindList<int>(strSql.ToString(), param).Any();
        }

        /// <summary>
        /// Đ ăng ký khám lịch trực danh sách,sửatrang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="ghpbId"></param>
        /// <returns></returns>
        public List<OutpatientRegistScheduleVO> GetOutpatientRegistScheduleList(string orgId, string keyword = null, int? ghpbId = null)
        {
            var paraList = new List<SqlParameter>() { };
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
				select ghpb.*,sfxm.sfxmmc,zlsfxm.sfxmmc zlxmmc,department.Name departmentName,staff.Name staffName,ghzb.ghzbmc
                from mz_ghpb ghpb
                left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] sfxm 
                on sfxm.sfxmCode=ghpb.ghlx and sfxm.OrganizeId = ghpb.OrganizeId
                left join NewtouchHIS_Base.[dbo].[V_S_Sys_Department] department 
                on department.Code=ghpb.ks and department.OrganizeId = ghpb.OrganizeId
                left join NewtouchHIS_Base.[dbo].[V_S_Sys_Staff] staff 
                on staff.gh=ghpb.ys and staff.OrganizeId= ghpb.OrganizeId
                left join [dbo].[xt_ghzb] ghzb 
                on ghzb.ghzb=ghpb.ghzb and ghzb.OrganizeId = ghpb.OrganizeId
				left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] zlsfxm 
                on zlsfxm.sfxmCode=ghpb.zlxm and zlsfxm.OrganizeId = ghpb.OrganizeId
                where 1 = 1
                ");
            if (ghpbId > 0)
            {
                strSql.Append(" and ghpb.ghpbId=@ghpbId ");
                paraList.Add(new SqlParameter("@ghpbId", ghpbId));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and ghpb.ys like @keyword or staff.Name like @keyword");
                paraList.Add(new SqlParameter("@keyword", '%' + keyword + '%'));
            }
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                strSql.Append(" and ghpb.OrganizeId=@orgId ");
                paraList.Add(new SqlParameter("@orgId", orgId));
            }
            var list = this.FindList<OutpatientRegistScheduleVO>(strSql.ToString(), paraList.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// Đ ăng ký khám lịch trực List
        /// </summary>
        /// <param name="keyword">Khoa/Bác sĩ</param>
        /// <param name="mzlx">Loại ngoại trú phòng khám thông thư ờng/急症/Phòng khám chuyên gia</param>
        /// <returns></returns>
        public List<RegScheduleVO> GetRegScheduleList(string keyword, string mzlx, string orgId)
        {
            var strSql = new StringBuilder(@"
				select a.ghpbId, a.ghlx, a.zlxm,d.sfxmmc,b.Name ksmc,0 ksbh,b.Code ks,c.gh,c.Name rymc, g.sfxmmc zlxmmc
                from mz_ghpb(nolock) a
                inner join [NewtouchHIS_Base]..V_S_Sys_Department b on a.ks = b.Code and b.OrganizeId=@orgId 
                left join [NewtouchHIS_Base]..[V_S_Sys_Staff] c on a.ys = c.gh and c.OrganizeId=@orgId and c.zt = '1'
         
    left join [NewtouchHIS_Base]..V_S_xt_sfxm d on a.ghlx = d.sfxmCode and d.OrganizeId=@orgId
    left join [NewtouchHIS_Base]..V_S_xt_sfxm g on a.zlxm = g.sfxmCode and g.OrganizeId=@orgId
                left join xt_ghzb e on a.ghzb = e.ghzb and e.zt=1 and e.OrganizeId=@orgId
                where a.zt = 1 and a.OrganizeId=@orgId
                ");

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));

            if (mzlx == ((int)EnumOutPatientType.expertOutpat).ToString())
            {
                strSql.Append(" and isnull(a.ys,'')<>''");
            }
            if (!string.IsNullOrWhiteSpace(mzlx))
            {
                strSql.Append(" and isnull(a.mjzbz,'') = @mjzbz");
                pars.Add(new SqlParameter("@mjzbz", mzlx));
            }

            var weekdayField = string.Empty;
            var weekday = DateTime.Now.DayOfWeek;
            MzpbZxsjVO currZxsj = null;
            //定义buổi sáng buổi chiều của thời gian分布 2 buổi chiều 3 buổi sáng
            //[{type:"2",startTime:"07:00:00",endTime:"12:00:00"},{type:"3",startTime:"13:00:00",endTime:"18:00:00"}]
            var zxsjConfigStr = _sysConfigRepo.GetValueByCode(Constants.xtmzpz.S20100, orgId);
            if (!string.IsNullOrWhiteSpace(zxsjConfigStr))
            {
                var zxsjItemLists = zxsjConfigStr.ToList<MzpbZxsjVO>();
                if (zxsjItemLists != null)
                {
                    currZxsj = zxsjItemLists.Find(t => string.Compare(t.startTime, DateTime.Now.ToString("HH:mm:ss"), StringComparison.Ordinal) <= 0 && string.Compare(t.endTime, DateTime.Now.ToString("HH:mm:ss"), StringComparison.Ordinal) >= 0);
                }
            }
            switch (weekday)
            {
                case DayOfWeek.Monday:
                    weekdayField = "zyi";
                    break;
                case DayOfWeek.Tuesday:
                    weekdayField = "zer";
                    break;
                case DayOfWeek.Wednesday:
                    weekdayField = "zsan";
                    break;
                case DayOfWeek.Thursday:
                    weekdayField = "zsi";
                    break;
                case DayOfWeek.Friday:
                    weekdayField = "zwu";
                    break;
                case DayOfWeek.Saturday:
                    weekdayField = "zlv";
                    break;
                case DayOfWeek.Sunday:
                    weekdayField = "zri";
                    break;
            }
            if (string.IsNullOrEmpty(weekdayField))
            {
                strSql.Append(" and 1 = 0 ");
            }
            else
            {
                if (currZxsj == null)
                {
                    strSql.Append(" and (" + weekdayField + " = '1')");
                }
                else
                {
                    strSql.Append(" and (" + weekdayField + " = '1' or " + weekdayField + " = '" + currZxsj.type + "')");
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" and (d.sfxmmc like @keyword or g.sfxmmc like @keyword or b.py like @keyword or b.Name like @keyword or c.py like @keyword or c.Name like @keyword or d.py like @keyword or g.py like @keyword)");

                pars.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
            }

            strSql.Append(" order by d.sfxmmc,b.Name");

            return FindList<RegScheduleVO>(strSql.ToString(), pars.ToArray());
        }
        /// <summary>
        /// Lấy mới Đ ăng ký khám lịch trực 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<RegNewScheduleVO> GetNewRegScheduleList(string keyword, string mzlx, string orgId)
        {
          string strSql = @"select a.ScheduId ghpbId,
[ysgh] gh,
[ysxm] rymc,[czks] ks,[czksmc] ksmc,[RegType],[Title],[Period],[PeriodDesc],[TotalNum]
,[PeriodStart],[PeriodEnd],[RegFee],[IsCancel],[CancelReason],[CancelTime],
YYNum,IsBook,a.[ghlx],a.[zlxm],convert(varchar(2),weekdd) weekdd,d.sfxmmc sfxmmc ,e.sfxmmc zlxmmc
from NewtouchHIS_Base.dbo.V_S_Sys_Department ks with(nolock),
mz_ghpb_schedule a with(nolock)
--left join [dbo].[mz_ghpb_config] b with(nolock) on a.organizeid=b.organizeid and b.ghpbId=a.ghpbId and b.zt='1'
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid =d.organizeid and d.zt='1' and a.ghlx=d.sfxmcode
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] e with(nolock) on a.organizeid =e.organizeid and e.zt='1' and a.zlxm=e.sfxmcode
where a.[OrganizeId]=@orgId and a.zt='1' 
and a.OrganizeId=ks.OrganizeId and ks.zt='1' and a.czks=ks.Code ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            var time = DateTime.Now.ToString("yyyy-MM-dd");

            if (!string.IsNullOrWhiteSpace(mzlx))
            {
                strSql += " and RegType=@RegType";
                para.Add(new SqlParameter("@RegType", mzlx));
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql += " and (czksmc like @keyword or ks.py like @keyword or e.sfxmmc like @keyword)";
                para.Add(new SqlParameter("@keyword","%"+ keyword+"%"));
            }
            strSql += " and OutDate= @time";
            para.Add(new SqlParameter("@time", time));

            return FindList<RegNewScheduleVO>(strSql.ToString(), para.ToArray());
        }
        /// <summary>
        /// Lấy mới Đ ăng ký khám lịch trực (điều trị kết hợp)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<RegNewScheduleVO> GetRegScheduleListbyGroup(string keyword, string mzlx,string pbks, string orgId, DateTime? OutDate,string ys)
        {
          string strSql = @"select a.ScheduId ghpbId,OutDate,
[ysgh] gh,
isnull([ysxm],h.[name]) rymc,[czks] ks,[czksmc] ksmc,[RegType],[Title],[Period],[PeriodDesc],[TotalNum]
,[PeriodStart],[PeriodEnd],[RegFee],[IsCancel],[CancelReason],[CancelTime],
YYNum,IsBook,a.[ghlx],a.[zlxm],convert(varchar(2),weekdd) weekdd,d.sfxmmc sfxmmc ,e.zhmc zlxmmc,ks.ybksbm
from NewtouchHIS_Base.dbo.V_S_Sys_Department ks with(nolock),
mz_ghpb_schedule a with(nolock)
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on a.organizeid =d.organizeid and d.zt='1' and a.ghlx=d.sfxmcode
left join V_mz_pb_zlzh e with(nolock) on a.organizeid =e.organizeid and a.zlxm=e.zhcode
left join NewtouchHIS_Base.dbo.V_S_Sys_Staff h with(nolock) on a.ysgh=h.gh and a.OrganizeId=h.OrganizeId and h.zt='1'
where a.[OrganizeId]=@orgId and a.zt='1' and a.IsCancel='0'
and a.OrganizeId=ks.OrganizeId and ks.zt='1' and a.czks=ks.Code ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@orgId", orgId));

            var time = DateTime.Now.ToString("yyyy-MM-dd");

            if (!string.IsNullOrWhiteSpace(pbks))
            {
                strSql += " and (czks=@ks or czksmc=@ks)";
                para.Add(new SqlParameter("@ks", pbks));
            }
            if (!string.IsNullOrWhiteSpace(ys))
            {
                strSql += " and ysxm=@ys";
                para.Add(new SqlParameter("@ys", ys));
            }

            if (!string.IsNullOrWhiteSpace(mzlx))
            {
                strSql += " and RegType=@RegType";
                para.Add(new SqlParameter("@RegType", mzlx));
            }
            if (OutDate!=null)
            {
                strSql += " and OutDate=@OutDate";
                para.Add(new SqlParameter("@OutDate", OutDate));
            }
            else
            {
                strSql += " and OutDate= @time";
                para.Add(new SqlParameter("@time", time));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql += " and (czksmc like @keyword or ks.py like @keyword or h.py like @keyword or h.name like @keyword or a.Title like @keyword)";
                para.Add(new SqlParameter("@keyword","%"+ keyword+"%"));
            }
            strSql += " order by RegType,ksmc";

            return FindList<RegNewScheduleVO>(strSql.ToString(), para.ToArray());
        }
        /// <summary>
        /// Bệnh nh ânĐ ãĐ ặt lịch đ ăng ký khámlist
        /// </summary>
        /// <param name="mzlx"></param>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MzghbookScheduleVO> GetIsMzghBookSchedule(Pagination pagination,string mzlx,string patid,string orgId,string isfeegroup,string yyzt,string yyly,string keyValue,
           string ks, DateTime? yykssj=null,DateTime? yyjssj=null)
        {
            string sql = "";
            if (isfeegroup == "1") //1:启 dùng thu phí kết hợp 0:停 dùng 
            {
                sql = @" select gb.Regtype,gs.Title,gb.AppId,cast(gb.BookId as int) BookId,
gb.ScheduId,gs.ghlx,gs.zlxm,d.sfxmmc,gb.xm,gb.xb,gb.kh,xtxx.zjh,gb.mzh,gb.RegFee,
gs.czksmc ksmc,gs.czks ks,gb.yyzt,gs.ysgh,gs.ysxm,zl.zhmc+'收费组合' zlxmmc,QueueNo,gb.OutDate,gs.PeriodDesc
from mz_gh_book gb with(nolock)
join xt_brjbxx xtxx with(nolock) on gb.patid=xtxx.patid and gb.organizeid=xtxx.organizeid
left join [dbo].[mz_ghpb_schedule] gs with(nolock) on gs.ScheduId=gb.ScheduId and gs.zt=1 and gb.organizeid=gs.organizeid
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on gb.organizeid =d.organizeid and d.zt='1' and gs.ghlx=d.sfxmcode
left join [dbo].[mz_gh_zlxmzh] zl with(nolock) on zl.zhcode=gs.zlxm and zl.organizeid=gs.organizeid
where gb.zt=1  and gb.organizeid=@orgId ";
            }
            else { 
            sql = @"select gb.Regtype,gs.Title,gb.AppId,cast(gb.BookId as int) BookId,
gb.ScheduId,gs.ghlx,gs.zlxm,d.sfxmmc,gb.xm,gb.xb,gb.kh,xtxx.zjh,gb.mzh,gb.RegFee,
gs.czksmc ksmc,gs.czks ks,gb.yyzt,gs.ysgh,gs.ysxm,e.sfxmmc zlxmmc,QueueNo,gb.OutDate,gs.PeriodDesc
from mz_gh_book gb with(nolock)
join xt_brjbxx xtxx with(nolock) on gb.patid=xtxx.patid and gb.organizeid=xtxx.organizeid
left join [dbo].[mz_ghpb_schedule] gs with(nolock) on gs.ScheduId=gb.ScheduId  and gb.organizeid=gs.organizeid
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] d with(nolock) on gb.organizeid =d.organizeid and d.zt='1' and gs.ghlx=d.sfxmcode
left join [NewtouchHIS_Base].dbo.[V_S_xt_sfxm] e with(nolock) on gb.organizeid =e.organizeid and e.zt='1' and gs.zlxm=e.sfxmcode
 where gb.zt=1  and gb.organizeid=@orgId ";
            }
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrWhiteSpace(mzlx))
            {
                sql += " and isnull(gs.RegType,'')=@RegType";
            }
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and (isnull(gb.ks,'')=@ks or gs.czksmc=@ks)";
            }
            if (!string.IsNullOrWhiteSpace(patid))
            {
                sql += " and gb.patid = @patid";
            }
            if (yykssj != null)//(yykssj==DateTime.MinValue)
            {
                sql += " and gb.OutDate>=@yykssj and gb.OutDate<=@yyjssj ";
            }
            else
            {
                sql += " and gb.yyzt=1 and gb.OutDate= @time";
            }
            if (!string.IsNullOrWhiteSpace(yyzt))
            {
                sql += " and gb.yyzt = @yyzt";
            }
            if (!string.IsNullOrWhiteSpace(yyly))
            {
                sql += " and gb.AppId = @yyly";
            }
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and (gb.xm = @keyValue or zjh=@keyValue)";
            }
            DbParameter[] param =
            {
                new SqlParameter("@RegType",mzlx??""),
                new SqlParameter("@ks",ks??""),
                new SqlParameter("@orgId",orgId??""),
                new SqlParameter("@patid",patid??""),
                new SqlParameter("@time",time),
                new SqlParameter("@yyzt",yyzt ?? ""),
                new SqlParameter("@yyly",string.IsNullOrWhiteSpace(yyly)? "":Enum.Parse(typeof(EnumMzghly), yyly).ToString()),
                new SqlParameter("@keyValue",keyValue ?? ""),
                new SqlParameter("@yykssj",yykssj ?? DateTime.Now),
                new SqlParameter("@yyjssj",yyjssj ?? DateTime.Now)
            };
            return this.QueryWithPage<MzghbookScheduleVO>(sql.ToString(), pagination, param).ToList();
        }

        /// <summary>
        /// máy tự phục vụ lấy lịch trực thông tin
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<zzjRegScheduleVO> GetZzjRegScheduleList(DateTime pbrq, string orgId)
		{
			var strSql = new StringBuilder(@"
				select CONVERT(VARCHAR(50),a.ghpbId) ghpbId,a.ks ks, b.Name ksmc,a.mjzbz
                from mz_ghpb(nolock) a
                inner join [NewtouchHIS_Base]..V_S_Sys_Department b on a.ks = b.Code and b.OrganizeId=@orgId 
                where a.zt = 1 and a.OrganizeId=@orgId
                ");

			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", orgId));
			var weekdayField = string.Empty;
			var weekday = pbrq.DayOfWeek;
			MzpbZxsjVO currZxsj = null;
			//定义buổi sáng buổi chiều của thời gian分布 2 buổi chiều 3 buổi sáng
			//[{type:"2",startTime:"07:00:00",endTime:"12:00:00"},{type:"3",startTime:"13:00:00",endTime:"18:00:00"}]
			var zxsjConfigStr = _sysConfigRepo.GetValueByCode(Constants.xtmzpz.S20100, orgId);
			if (!string.IsNullOrWhiteSpace(zxsjConfigStr))
			{
				var zxsjItemLists = zxsjConfigStr.ToList<MzpbZxsjVO>();
				if (zxsjItemLists != null)
				{
					currZxsj = zxsjItemLists.Find(t => string.Compare(t.startTime, DateTime.Now.ToString("HH:mm:ss"), StringComparison.Ordinal) <= 0 && string.Compare(t.endTime, DateTime.Now.ToString("HH:mm:ss"), StringComparison.Ordinal) >= 0);
				}
			}
			switch (weekday)
			{
				case DayOfWeek.Monday:
					weekdayField = "zyi";
					break;
				case DayOfWeek.Tuesday:
					weekdayField = "zer";
					break;
				case DayOfWeek.Wednesday:
					weekdayField = "zsan";
					break;
				case DayOfWeek.Thursday:
					weekdayField = "zsi";
					break;
				case DayOfWeek.Friday:
					weekdayField = "zwu";
					break;
				case DayOfWeek.Saturday:
					weekdayField = "zlv";
					break;
				case DayOfWeek.Sunday:
					weekdayField = "zri";
					break;
			}
			if (string.IsNullOrEmpty(weekdayField))
			{
				strSql.Append(" and 1 = 0 ");
			}
			else
			{
				if (currZxsj == null)
				{
					strSql.Append(" and (" + weekdayField + " = '1')");
				}
				else
				{
					strSql.Append(" and (" + weekdayField + " = '1' or " + weekdayField + " = '" + currZxsj.type + "')");
				}
			}

			return FindList<zzjRegScheduleVO>(strSql.ToString(), pars.ToArray());
		}

		/// <summary>
		/// Lấykhi trư ớc khoa ngoại trú hiệu lực thời lư ợng by caishanshan 20161214 
		/// </summary>
		/// <param name="isJz">Có hay khôngCấp cứu</param>
		/// <param name="ks">Khoa(-1 biểu thị tất cả các khoa)</param>
		/// <param name="ksDate"></param>
		/// <param name="jsDate"></param>
		private void GetCurrKsDuration(bool isJz, string ks, out DateTime ksDate, out DateTime jsDate)
        {
            DateTime currTime = DateTime.Now;
            var mzActiveDuration = _sysConfigRepo.GetByCode(Constants.xtmzpz.MZ_ACTIVE_DURATION, OperatorProvider.GetCurrent().OrganizeId);
            if (mzActiveDuration.Value == null)
            {
                throw new FailedCodeException("OUTPAT_TIMECONFIG_ERROR");
            }
            else
            {
                List<MzActiveDurationVO> durationList = Json.ToObject<List<MzActiveDurationVO>>(mzActiveDuration.Value);
                var entity = durationList.Find(a => a.ks == ks);
                if (entity == null)
                {
                    throw new FailedCodeException("OUTPAT_PLEASE_CONFIGURE_THE_DEPARTMENT_OUTPATIENT_EFFECTIVE_TIME_DATA");
                }
                int duration = -1;
                if (isJz)
                {
                    duration = entity.jz;
                }
                else
                {
                    duration = entity.mz;
                }
                if (duration == -1)
                {
                    //KhiNgàyHiệu lực
                    ksDate = currTime.Date;
                    jsDate = currTime.Date.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    // một đoạn thời gian trong hiệu lực
                    ksDate = currTime.AddHours(-duration);
                    jsDate = currTime;
                }

            }

        }

        /// <summary>
        /// LấySố thứ tự khám bệnh
        /// </summary>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        public short GetJzxh(string ghlx, string ks, string ys, string ghzb, string userCode, string orgId)
        {
            var inParameters = new Dictionary<string, object>();
            inParameters.Add("@ghlx", ghlx ?? "");
            inParameters.Add("@ks", ks ?? "");
            inParameters.Add("@ys", ys ?? "");
            inParameters.Add("@ghzb", ghzb ?? "");
            inParameters.Add("@CreatorCode", userCode);
            inParameters.Add("@OrganizeId", orgId);
            var outParameter = new SqlParameter("@NumStr", System.Data.SqlDbType.SmallInt);

            DbHelper.ExecuteProcedure("spGetJZXH", inParameters, outParameter);

            return Convert.ToInt16(outParameter.Value);
        }

        #region 门诊记账

        /// <summary>
        /// Ghi sổ ngoại trú lưuthao tác
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        /// <param name="isOptimaApi">Có hay khôngOptimagiao diện Tư ơng ứng củaghi sổ,mặc đ ịnh là trống,1 biểu thị là(kế hoạch ghi sổ không 做dữ liệu xử lý),null và0 biểu thị là không</param>
        /// <param name="isClinic">Có hay không phòng khám ghi sổ,mặc đ ịnh là trống,1 biểu thị là(nhắc nhở bảo hiểm thư ơng mại số lần),null và2 biểu thị là không(nhắc nhở BHYT số lần)</param>
        public void SaveOutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string isOptimaApi = null, string isClinic = null)
        {
            DbParameter[] opar = { new SqlParameter("@Id", orgId) };
            var isZhenSuo = FirstOrDefault<string>("SELECT CategoryCode FROM NewtouchHIS_Base..Sys_Organize WHERE Id=@Id", opar);
            if (isZhenSuo == "Clinic")
            {
                isClinic = "1";
            }

            List<DateTime> jzsjList = new List<DateTime>();//Ghi sổ ngày không trùng lặp 字符串,là BHYThoặc bảo hiểm thư ơng mại số lần thống kê chuẩn bị 
            var optimaId = new Dictionary<string, string>();
            //Quyết toánEntity
            var jsEntity = new SettlementEntityVO();
            //Quyết toán thực thể bảng tư ơng ứngmz_js
            var mzjs = new List<OutpatientSettlementEntity>();
            //Chi tiết quyết toán bảng tư ơng ứngmz_jsmx
            var jsmxList = new List<OutpatientSettlementDetailEntity>();
            OutpatientAccountEntity jzjh = null;
            List<OutpatientAccountDetailEntity> jzjhmxList = null;
            if (isOptimaApi != "1")
            {
                //Kế hoạch ghi sổ
                jzjh = new OutpatientAccountEntity
                {
                    OrganizeId = orgId,
                    patid = bacDto.patid.ToString(),
                    remark = bacDto.jsr,
                    zt = "1"
                };

                //Kế hoạch ghi sổ
                jzjh.Create(true);
                //Chi tiết kế hoạch ghi sổ
                jzjhmxList = new List<OutpatientAccountDetailEntity>();
            }



            var sqlVo = GetOutPatChargeAccuDbvo(bacDto.patid, bacDto.brxz, accDto, out jsEntity, out jzjhmxList, jzjh, orgId, bacDto, out mzjs, out jzsjList, out optimaId, isOptimaApi, isClinic);

            //Tính出quyết toán ngoại trú thông tin getMzJs
            sqlVo.mz_js = mzjs;

            //Lấy chi tiết quyết toán
            jsmxList = _outPatChargeDmSer.getJSMX(jsEntity, orgId);
            sqlVo.mz_jsmxList = jsmxList;

            //Lưu vào cơ sở dữ liệu
            _outPatChargeDmSer.OutPatChargeSettDBInAcc(sqlVo, jzjh, jzjhmxList, optimaId);

            // số lần còn lại của bảo hiểm 
            if (jzsjList == null || jzsjList.Count <= 0 || string.IsNullOrWhiteSpace(bacDto.patid.ToString())) return;
            foreach (var time in jzsjList)
            {
                var strSql = new StringBuilder();
                strSql.Append(@" exec sp_bxba_gxsycs @bxtype, @orgId, @patId, @date");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@bxtype", isClinic??"2"),
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@patId", bacDto.patid),
                    new SqlParameter("@date", time)
                };
                ExecuteSqlCommand(strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// Ghi sổ ngoại trú lưu quyết toán thực thể gán giá trị 
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="gridDto"></param>
        /// <param name="jsEntity"></param>
        /// <param name="jzjhmxEntity"></param>
        /// <param name="jzjhEntity"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <param name="bacDto"></param>
        /// <param name="mzjsList"></param>
        /// <param name="jzsjList"></param>
        /// <param name="isOptimaApi">Có hay khôngOptimagiao diện Tư ơng ứng củaghi sổ,mặc đ ịnh là trống,1 biểu thị là(kế hoạch ghi sổ không 做dữ liệu xử lý),null và0 biểu thị là không</param>
        /// <param name="isClinic">Có hay không phòng khám ghi sổ,mặc đ ịnh là trống,1 biểu thị là(nhắc nhở bảo hiểm thư ơng mại số lần),null và0 biểu thị là không(nhắc nhở BHYT số lần)</param>
        public OutPatChargeSettInAccDataVo GetOutPatChargeAccuDbvo(int patid
            , string brxz
            , IList<OutpatAccGridInfoDto> gridDto
            , out SettlementEntityVO jsEntity
            , out List<OutpatientAccountDetailEntity> jzjhmxEntity
            , OutpatientAccountEntity jzjhEntity
            , string orgId, OutpatAccBasicInfoDto bacDto
            , out List<OutpatientSettlementEntity> mzjsList
            , out List<DateTime> jzsjList
            , out Dictionary<string, string> optimaId
            , string isOptimaApi = null
            , string isClinic = null)
        {
            jzjhmxEntity = null;
            jzsjList = new List<DateTime>();
            optimaId = new Dictionary<string, string>();
            var itemDtoList = new List<OutpatientSettlementEntity>();
            var sqlVo = new OutPatChargeSettInAccDataVo
            {
                mz_cf = new List<OutpatientPrescriptionEntity>(),
                mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>(),
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new List<OutpatientSettlementEntity>(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>()
            };
            if (isOptimaApi != "1" && jzjhEntity != null)
            {
                jzjhmxEntity = new List<OutpatientAccountDetailEntity>();//Chi tiết kế hoạch ghi sổlist
            }
            jsEntity = new SettlementEntityVO(); // tạm lưu thông tin quyết toán 

            var jsxmList = new List<SettleProjectVO>(); //Quyết toánHạng mụcList

            //mz_cf mz_cfmxĐ ơn thuốctham số
            var strArray = new Dictionary<string, string>();  //Tạm lưu số đ ơn thuốc đ ã lưu,Tư ơng ứng của bác sĩ
            var cFisExit = true; // Đ ơn thuốc mặc đ ịnh không tồn tại
            var _cfnm = 0; //Mã nội bộ đ ơn thuốc
            var _cfh = "";//Số đ ơn thuốc
            var ghnmentity = _outpatientRegRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.OrganizeId == orgId);
            if (ghnmentity == null)
            {
                throw new FailedException("门诊号不存在，请确认！");
            }
            var ghnm = ghnmentity.ghnm;
            const decimal outJmbl = 0;
            const decimal outJmje = 0;
            //循环 cần lưu của hạng mục dữ liệu
            if (gridDto.Count > 0)
            {

                var gridindex = 0;
                var cfindex = 0;
                foreach (var dto in gridDto)
                {
                    if (dto.czlx != "0")
                    {
                        optimaId.Add(dto.newid, null);
                    }
                    string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                    //Thanh toán tiền mặt
                    decimal ssk;
                    //Sai lệch tiền mặt
                    decimal difference;
                    //Số tiền phải thu
                    var ysk = 0m;
                    var ysArray = dto.ysList.OrderBy(p => p.gh).ToList();

                    getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);

                    if (gridindex == 0)
                    {
                        #region 结算基本信息
                        jsEntity.patid = patid;
                        jsEntity.brxz = brxz;
                        jsEntity.ghnm = ghnm;
                        jsEntity.isGh = false;
                        jsEntity.isZf = true;
                        jsEntity.jslx = "2";//Ghi sổ ngoại trú
                        #endregion 
                    }
                    if (dto.jzsj != null && !jzsjList.Contains(dto.jzsj)) jzsjList.Add(dto.jzsj);

                    OutpatientAccountDetailEntity mxEntity = null;
                    if (isOptimaApi != "1" && jzjhEntity != null)
                    {
                        #region 记账计划明细
                        mxEntity = new OutpatientAccountDetailEntity
                        {
                            jzjhId = jzjhEntity.jzjhId,
                            zlsc = dto.duration,
                            sfxmCode = dto.sfxmCode,
                            sl = dto.sl,
                            jzsj = dto.jzsj,
                            bz = dto.bz,
                            ys = ys,
                            ks = ks,
                            ysmc = ysmc,
                            ksmc = ksmc,
                            ttbz = dto.ttbz == 1 ? true : false,
                            kflb = dto.kflb,
                            OrganizeId = orgId
                        };
                        mxEntity.Create(true);
                        jzjhmxEntity.Add(mxEntity);
                        #endregion
                    }
                    #region 如果记账时间是同一天，作为一笔记账数据,门诊结算，结算大类，门诊项目重新处理
                    if (itemDtoList.Any(p => p.jzsj == dto.jzsj))
                    {
                        #region 门诊结算
                        var oldDto = itemDtoList.FirstOrDefault(p => p.jzsj == dto.jzsj);
                        if (oldDto != null)
                        {
                            oldDto.zlfy += dto.Zje;
                            oldDto.jzfy = oldDto.zlfy;
                            ysk = oldDto.zlfy;
                            Tools.Ext.get5s6r(ysk, out ssk, out difference);
                            oldDto.xjzf = ssk;
                            oldDto.xjwc = 0;
                            oldDto.jzsj = dto.jzsj;
                            oldDto.zje = oldDto.zlfy;
                        }

                        #endregion

                        if (oldDto != null)
                        {

                            if (dto.yzlx == "1") //Thuốc
                            {
                                #region 药品

                                if (dto.sfxmCode != null)
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                                    {
                                        cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                                        _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                    }
                                //Thêm qua Đ ơn thuốc bản ghi 
                                if (strArray != null && strArray.Count > 0)
                                {
                                    foreach (var t in strArray)
                                    {
                                        if (t.Value.Equals(ys))
                                        {
                                            //Cho biết đ ã tồn tại thông tin bảng chính của đ ơn thuốc này
                                            cFisExit = false; // Đ ơn thuốc đ ã tồn tại
                                        }
                                        else
                                        {
                                            cFisExit = true; // khác nhau chuyên viên trị liệu,Đ ơn thuốc khác nhau 
                                            _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                        }
                                    }
                                }

                                if (cFisExit)
                                {
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    strArray.Add(_cfh, ys);

                                    #region 添加处方

                                    var mzcf =
                                        new OutpatientPrescriptionEntity
                                        {
                                            cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                            ghnm = ghnm,
                                            patid = bacDto.patid,
                                            brxz = brxz,
                                            mjzbz = "1",
                                            ys = ys,
                                            ysmc = ysmc,
                                            ks = ks,
                                            ksmc = ksmc,
                                            cfzt = "1",
                                            jsnm = oldDto.jsnm,
                                            jsrq = dto.jzsj,
                                            cfh = bacDto.cfh
                                        };

                                    mzcf.zt = "1";
                                    mzcf.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                                    mzcf.Create();
                                    _cfnm = mzcf.cfnm;

                                    #endregion

                                    cfindex++;
                                    sqlVo.mz_cf.Add(mzcf);
                                }

                                #endregion

                                #region 处方明细

                                var mzcfmx = new OutpatientPrescriptionDetailEntity
                                {
                                    cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                                    cfnm = _cfnm,
                                    yp = dto.sfxmCode,
                                    dl = dto.sfdlCode,
                                    dj = dto.dj,
                                    sl = dto.sl,
                                    je = dto.Zje,
                                    dw = dto.dw,
                                    zt = "1",
                                    jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                                    kflb = dto.kflb,
                                    OrganizeId = orgId
                                };
                                mzcfmx.Create();
                                sqlVo.mz_cfmxList.Add(mzcfmx);
                                #endregion

                                #region 结算对象赋值 
                                var jsxmDto = addSettleProjectVo(oldDto.jsnm, mzcfmx.yp, 0, mzcfmx.yp, _cfnm, bacDto.cfh, dto);
                                jsxmList.Add(jsxmDto);
                                #endregion
                            }
                            else//Hạng mục
                            {

                                #region 门诊项目
                                var xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                var mzxm2 = new OutpatientItemEntity
                                {
                                    xmnm = xmnm,
                                    ghnm = ghnm,
                                    patid = patid,
                                    brxz = brxz,
                                    mjzbz = "1",
                                    ys = ys,
                                    ks = ks,
                                    ysmc = ysmc,
                                    ksmc = ksmc,
                                    sfxm = dto.sfxmCode,
                                    dl = dto.sfdlCode,
                                    dj = dto.dj,
                                    sl = dto.sl,
                                    je = dto.Zje,
                                    xmzt = "1",
                                    xmly = "0",
                                    ssbz = "0",
                                    ssrq = dto.jzsj,
                                    jsnm = oldDto.jsnm,
                                    jsrq = DateTime.Now,
                                    zt = "1",
                                    OrganizeId = orgId,
                                    kflb = dto.kflb,
                                    jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                                    ttbz = dto.ttbz == 1 ? true : false,
                                    duration = dto.duration
                                };
                                mzxm2.Create();

                                if (optimaId != null)
                                {
                                    if (optimaId.Keys.Contains(dto.newid))
                                    {
                                        optimaId[dto.newid] = mzxm2.xmnm.ToString();
                                    }
                                }
                                sqlVo.mz_xmList.Add(mzxm2);
                                #endregion

                                #region 结算对象赋值
                                jsxmList.Add(addSettleProjectVo(oldDto.jsnm, dto.sfxmCode, xmnm, "", 0, "", dto));

                                #endregion
                            }
                        }
                        continue;
                    }
                    #endregion

                    #region 门诊结算
                    var jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js");
                    var mzjs = new OutpatientSettlementEntity
                    {
                        jsnm = jsnm,
                        patid = jsEntity.patid,
                        ghnm = jsEntity.ghnm,
                        brxz = brxz,
                        jslx = jsEntity.jslx,
                        zlfy = dto.Zje,
                        zffy = 0,
                        flzffy = 0,
                        jzfy = dto.Zje,
                        jmje = 0,
                        jszt = (int)Constants.jsztEnum.YJ,
                        cxjsnm = 0,
                        zh = 0,
                        fpdm = "0",
                        jmbl = 0,
                        jch = 0,
                        zt = "1",
                        zl = 0,
                        jzsj = dto.jzsj,
                        OrganizeId = orgId,
                        CreateTime = DateTime.Now,
                        CreatorCode = OperatorProvider.GetCurrent().UserCode,
                        xm = bacDto.xm,
                        xb = bacDto.xb,
                        blh = bacDto.blh,
                        csny = bacDto.csny,
                        zjh = bacDto.zjh,
                        fph = "0",
                        zjlx = bacDto.zjlx
                    };

                    #region 医保 暂不处理

                    var jylx = Constants.ybDealLB.yb_deal_wjy;
                    ysk = dto.Zje;

                    #endregion
                    mzjs.jylx = ((int)jylx).ToString();
                    mzjs.zffy = 0;
                    Tools.Ext.get5s6r(ysk, out ssk, out difference);
                    mzjs.xjzf = ssk;
                    mzjs.xjwc = difference;
                    mzjs.zje = mzjs.zlfy;
                    itemDtoList.Add(mzjs);
                    #endregion

                    if (dto.yzlx == "1") //Thuốc
                    {//mz_cf mz_cfmx 

                        #region 药品

                        if (dto.sfxmCode != null)
                            //Thêm thông tin bảng chính đ ơn thuốc
                            cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                        _cfh = getCflsh(); //Số thứ tự đ ơn thuốc

                        if (cFisExit)
                        {
                            //Thêm thông tin bảng chính đ ơn thuốc
                            strArray.Add(_cfh, ys);

                            #region 添加处方

                            var mzcf =
                                new OutpatientPrescriptionEntity
                                {
                                    cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                    ghnm = ghnm,
                                    patid = bacDto.patid,
                                    brxz = brxz,
                                    mjzbz = "1",
                                    ys = ys,
                                    ysmc = ysmc,
                                    ks = ks,
                                    ksmc = ksmc,
                                    cfzt = "1",
                                    jsnm = jsnm,
                                    jsrq = dto.jzsj,
                                    cfh = bacDto.cfh
                                };

                            mzcf.zt = "1";
                            mzcf.OrganizeId = orgId;
                            mzcf.Create();
                            _cfnm = mzcf.cfnm;

                            #endregion

                            cfindex++;
                            sqlVo.mz_cf.Add(mzcf);
                        }

                        #endregion

                        #region 处方明细

                        var mzcfmx = new OutpatientPrescriptionDetailEntity
                        {
                            cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                            cfnm = _cfnm,
                            yp = dto.sfxmCode,
                            dl = dto.sfdlCode,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.Zje,
                            dw = dto.dw,
                            zt = "1",
                            jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                            kflb = dto.kflb,
                            OrganizeId = orgId
                        };
                        mzcfmx.Create();
                        sqlVo.mz_cfmxList.Add(mzcfmx);
                        #endregion

                        #region 结算对象赋值 
                        var jsxmDto = addSettleProjectVo(jsnm, mzcfmx.yp, 0, mzcfmx.yp, _cfnm, bacDto.cfh, dto);
                        jsxmList.Add(jsxmDto);
                        #endregion
                    }
                    else//Hạng mục
                    {//mz_xm
                        #region 门诊项目
                        var mzxm = new OutpatientItemEntity
                        {
                            xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                            ghnm = ghnm,
                            patid = patid,
                            brxz = brxz,
                            mjzbz = "1",
                            ys = ys,
                            ks = ks,
                            ysmc = ysmc,
                            ksmc = ksmc,
                            sfxm = dto.sfxmCode,
                            dl = dto.sfdlCode,
                            dj = dto.dj,
                            sl = dto.sl,
                            je = dto.Zje,
                            xmzt = "1",
                            xmly = "0",
                            ssbz = "0",
                            ssrq = dto.jzsj,
                            jsnm = jsnm,
                            jsrq = DateTime.Now,
                            zt = "1",
                            OrganizeId = orgId,
                            kflb = dto.kflb,
                            jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                            ttbz = dto.ttbz == 1 ? true : false,
                            duration = dto.duration
                        };
                        mzxm.Create();
                        if (optimaId != null)
                        {
                            if (optimaId.Keys.Contains(dto.newid))
                            {
                                optimaId[dto.newid] = mzxm.xmnm.ToString();
                            }
                        }
                        sqlVo.mz_xmList.Add(mzxm);

                        #endregion


                        #region 结算对象赋值
                        jsxmList.Add(addSettleProjectVo(jsnm, dto.sfxmCode, mzxm.xmnm, "", 0, "", dto));
                        #endregion
                    }
                    gridindex++;
                }

            }

            jsEntity.jsxmList = jsxmList;
            mzjsList = itemDtoList;

            //Đ ơn thuốc theo chuyên viên trị liệu có hay không cùng một Dòngmz_cf dữ liệu, vì vậy tổng số tiền theo chi tiết đ ơn thuốc tính
            foreach (var item in sqlVo.mz_cf)
            {
                item.zje = sqlVo.mz_cfmxList.Where(p => p.cfnm == item.cfnm).Sum(p => p.dj * p.sl);
            }
            //Thêm mỗi 个Đ ối tư ợng thực thể gán giá trị 
            return sqlVo;

        }

        /// <summary>
        /// Chuyên viên trị liệulist kết hợp 成字符串
        /// </summary>
        /// <param name="ysList"></param>
        /// <param name="ys"></param>
        /// <param name="ysmc"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        private void getYsKsInfo(IEnumerable<InpatientAccountingPlanItemDoctorDto> ysList
            , ref string ys, ref string ysmc, ref string ks, ref string ksmc)
        {

            foreach (var ysItem in ysList)
            {
                if (string.IsNullOrWhiteSpace(ysItem.gh)
                    || string.IsNullOrWhiteSpace(ysItem.Name)
                    || string.IsNullOrWhiteSpace(ysItem.ks)
                    || string.IsNullOrWhiteSpace(ysItem.ksmc))
                {
                    throw new FailedException("治疗师不明确");
                }
                ys += ysItem.gh + ",";
                ysmc += ysItem.Name + ",";
                ks += ysItem.ks + ",";
                ksmc += ysItem.ksmc + ",";
            }
            ys = ys.Trim(',');
            ysmc = ysmc.Trim(',');
            ks = ks.Trim(',');
            ksmc = ksmc.Trim(',');
        }

        /// <summary>
        /// mz_cf số thứ tự đ ơn thuốc
        /// </summary>
        /// <returns></returns>
        private static string getCflsh()
        {
            var cflsh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("xt_cflsh").ToString().PadLeft(5, '0');

            var suffix = "R" + DateTime.Now.ToString("yyyyMMddHH") + cflsh;
            return suffix;
        }

        /// <summary>
        /// Quyết to ánĐ ối tư ợng gán giá trị 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="sfxm"></param>
        /// <param name="mxnm"></param>
        /// <param name="mxbm"></param>
        /// <param name="cf_mxnm"></param>
        /// <param name="cfh"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static SettleProjectVO addSettleProjectVo(int? jsnm, string sfxm, int mxnm, string mxbm, int cf_mxnm, string cfh, OutpatAccGridInfoDto dto)
        {
            var jsxmDto = new SettleProjectVO
            {
                jsnm = jsnm,
                dl = dto.sfdlCode,
                sfxm = sfxm,
                dj = dto.dj,
                sl = dto.sl,
                je = dto.Zje,
                zfbl = 0,
                zfxz = "1",
                jmbl = 0,
                jmje = 0,
                mxnm = mxnm,
                //mxbm = mxbm,
                cf_mxnm = cf_mxnm,
                cfh = cfh,
                fwfdj = 0,
                jzrq = DateTime.Now
            };
            return jsxmDto;
        }

        #endregion

        #region optima记账 包括门诊记账和住院记账，不包含结算表和记账计划 Vision-1.2 门诊记账的第二个版本
        public void CommitAccounting(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            //Chưa xác nhận và thêm mới của dữ liệu
            var adddata = accDto.Where(p => (p.clzt == 1 || p.clzt == null) && p.czlx == "0").ToList();
            AddAccounting(patientType, bacDto, adddata, orgId);
            var updatedata = accDto.Where(p => (p.clzt == 2 || p.clzt == null) && (p.czlx == "1" || p.czlx == "2")).ToList();
            //Đ ãxác nhận và sửa xóa của dữ liệu
            UpdateAccounting(patientType, bacDto, updatedata, orgId);

            //XóaChưaXác nhậnCủaDữ liệu
            var unconfirmdatadel = accDto.Where(p => (p.clzt == 1 && p.czlx == "2")).ToList();
            if (unconfirmdatadel != null && unconfirmdatadel.Count() > 0)
            {
                foreach (var item in unconfirmdatadel)
                {
                    var pars = new List<SqlParameter>();
                    pars.Add(new SqlParameter("@Id", item.newid));
                    pars.Add(new SqlParameter("@czr", OperatorProvider.GetCurrent().UserCode));
                    string sql = @"UPDATE  dbo.TB_Sync_TreatmentServiceRecord
                                SET     clzt = 2 ,
                                        zt = 0 ,
                                        clr = @czr ,
                                        clsj = GETDATE()
                                WHERE   id = @ID";
                    ExecuteSqlCommand(sql, pars.ToArray());
                }
            }

        }

        /// <summary>
        /// Ngoại trú/Nhập viện thêm mớighi sổ chỉ chưa xác nhận
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="bacDto"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        private void AddAccounting(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            if (accDto != null && accDto.Count() > 0)
            {
                var optimaIds = new Dictionary<string, string>();
                if (patientType == "门诊")
                {
                    #region 门诊
                    var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
                    if (ghIsExits == null)
                    {
                        throw new FailedException("门诊号不存在，请确认");
                    }
                    var ghnm = ghIsExits.ghnm;
                    //***************************************Thêm thực thể******************************************************
                    //Thêm mớighi sổ đối tư ợng tập hợp 
                    var sqlVo = new SettInAccOutpatDataVo
                    {
                        mz_xmList = new List<OutpatientItemEntity>(),
                        mz_cf = new List<OutpatientPrescriptionEntity>(),
                        mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>()
                    };
                    var jzsjList = new List<DateTime>();
                    var gridindex = 0;
                    var cfindex = 0;
                    var strArray = new Dictionary<string, string>();  //Tạm lưu số đ ơn thuốc đ ã lưu,Tư ơng ứng của bác sĩ
                    var cFisExit = true; // Đ ơn thuốc mặc đ ịnh không tồn tại
                    var _cfh = "";//Số đ ơn thuốc
                    var _cfnm = 0; //Mã nội bộ đ ơn thuốc
                    //***************************************EndThêm thực thể***************************************************
                    foreach (var item in accDto)
                    {
                        string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                        if (item.ysList != null && item.ysList.Count() > 0)
                        {
                            var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
                            getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
                        }

                        if (item.jzsj != null && !jzsjList.Contains(item.jzsj)) jzsjList.Add(item.jzsj);
                        //ThêmoptimaIds ChưaXác nhận(clzt=1)CủaThêm mới(czlx=0)Dữ liệu
                        if (item.newid != null && !optimaIds.Keys.Contains(item.newid) && item.clzt == 1 && item.czlx
                            == "0") optimaIds.Add(item.newid, null);
                        #region 新增
                        if (item.yzlx == "1") //Thuốc
                        {//mz_cf mz_cfmx 

                            #region 药品

                            if (item.sfxmCode != null)
                                //Thêm thông tin bảng chính đ ơn thuốc
                                if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                                {
                                    cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                                    _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                }
                            //Thêm qua Đ ơn thuốc bản ghi 
                            if (strArray != null && strArray.Count > 0)
                            {
                                foreach (var t in strArray)
                                {
                                    if (t.Value.Equals(ys))
                                    {
                                        //Cho biết đ ã tồn tại thông tin bảng chính của đ ơn thuốc này
                                        cFisExit = false; // Đ ơn thuốc đ ã tồn tại
                                    }
                                    else
                                    {
                                        cFisExit = true; // khác nhau chuyên viên trị liệu,Đ ơn thuốc khác nhau 
                                        _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                    }
                                }
                            }

                            if (cFisExit)
                            {
                                //Thêm thông tin bảng chính đ ơn thuốc
                                strArray.Add(_cfh, ys);

                                #region 添加处方

                                var mzcf =
                                    new OutpatientPrescriptionEntity
                                    {
                                        cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                        ghnm = ghnm,
                                        patid = bacDto.patid,
                                        brxz = bacDto.brxz,
                                        mjzbz = "1",
                                        ys = ys,
                                        ysmc = ysmc,
                                        ks = ks,
                                        ksmc = ksmc,
                                        cfzt = "1",
                                        jsrq = item.jzsj,
                                        cfh = bacDto.cfh
                                    };

                                mzcf.zt = "1";
                                mzcf.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                                mzcf.Create();
                                _cfnm = mzcf.cfnm;

                                #endregion

                                cfindex++;
                                sqlVo.mz_cf.Add(mzcf);

                                #region 处方明细

                                var mzcfmx = new OutpatientPrescriptionDetailEntity
                                {
                                    cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                                    cfnm = _cfnm,
                                    yp = item.sfxmCode,
                                    dl = item.sfdlCode,
                                    dj = item.dj,
                                    sl = item.sl,
                                    je = item.Zje,
                                    dw = item.dw,
                                    zt = "1",
                                    kflb = item.kflb,
                                    OrganizeId = orgId
                                };
                                mzcfmx.Create();
                                sqlVo.mz_cfmxList.Add(mzcfmx);
                                #endregion
                            }

                            #endregion
                        }
                        else//Hạng mục
                        {//mz_xm
                            #region 门诊项目
                            var mzxm = new OutpatientItemEntity
                            {
                                xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                                ghnm = ghnm,
                                patid = bacDto.patid,
                                brxz = bacDto.brxz,
                                mjzbz = "1",
                                ys = ys,
                                ks = ks,
                                ysmc = ysmc,
                                ksmc = ksmc,
                                sfxm = item.sfxmCode,
                                dl = item.sfdlCode,
                                dj = item.dj,
                                sl = item.sl,
                                je = item.Zje,
                                xmzt = "1",
                                xmly = "0",
                                ssbz = "0",
                                ssrq = item.jzsj,
                                jsrq = DateTime.Now,
                                zt = "1",
                                OrganizeId = orgId,
                                kflb = item.kflb,
                                ttbz = item.ttbz == 1 ? true : false,
                                duration = item.duration
                            };
                            mzxm.Create();

                            sqlVo.mz_xmList.Add(mzxm);

                            #endregion

                            if (optimaIds != null)
                            {
                                if (optimaIds.Keys.Contains(item.newid))
                                {
                                    optimaIds[item.newid] = mzxm.xmnm.ToString();
                                }
                            }
                        }
                        gridindex++;

                        #endregion
                    }
                    #endregion
                    //Đ ơn thuốc theo chuyên viên trị liệu có hay không cùng một Dòngmz_cf dữ liệu, vì vậy tổng số tiền theo chi tiết đ ơn thuốc tính
                    foreach (var item in sqlVo.mz_cf)
                    {
                        item.zje = sqlVo.mz_cfmxList.Where(p => p.cfnm == item.cfnm).Sum(p => p.dj * p.sl);
                    }
                    _outPatChargeDmSer.PatsettDBInOptima(sqlVo, null, null, null, null, null, orgId, null, optimaIds);
                }
                else if (patientType == "住院")
                {
                    #region 住院
                    var zyIsExits = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == bacDto.mzh && p.zt == "1" && (p.zybz == "1" || p.zybz == "2" || p.zybz == "3") && p.OrganizeId == orgId);
                    if (zyIsExits == null)
                    {
                        throw new FailedException("该病人不在住院状态，请确认");
                    }
                    //***************************************Thêm thực thể******************************************************
                    //Thêm mớighi sổ đối tư ợng tập hợp 
                    var inpatsqlVo = new SettInAccHospatDataVo
                    {
                        zy_xmjfbList = new List<HospItemBillingEntity>(),
                        zy_ypjfbList = new List<HospDrugBillingEntity>()
                    };
                    //***************************************EndThêm thực thể***************************************************
                    foreach (var item in accDto)
                    {
                        string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                        if (item.ysList != null && item.ysList.Count() > 0)
                        {
                            var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
                            getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
                        }
                        //ThêmoptimaIds ChưaXác nhận(clzt=1)CủaThêm mới(czlx=0)Dữ liệu
                        if (item.newid != null && !optimaIds.Keys.Contains(item.newid) && item.clzt == 1 && item.czlx
                            == "0") optimaIds.Add(item.newid, null);
                        if (item.yzlx == "1") //Thuốc
                        {
                            #region 药品计费表
                            HospDrugBillingEntity zyYpjfb = new HospDrugBillingEntity();
                            zyYpjfb.zyh = bacDto.mzh;
                            zyYpjfb.yzwym = "0";
                            zyYpjfb.tdrq = item.jzsj;
                            zyYpjfb.yp = item.sfxmCode;
                            zyYpjfb.dl = item.sfdlCode;
                            zyYpjfb.ys = ys;
                            zyYpjfb.ks = ks;
                            zyYpjfb.ysmc = ysmc;
                            zyYpjfb.ksmc = ksmc;
                            zyYpjfb.dj = item.dj;
                            zyYpjfb.sl = item.sl;
                            zyYpjfb.jfdw = item.dw;
                            zyYpjfb.pyry = 0;
                            zyYpjfb.pyrq = item.jzsj;
                            zyYpjfb.tdrq = item.jzsj;
                            zyYpjfb.fyry = OperatorProvider.GetCurrent().UserCode;
                            zyYpjfb.fyrq = item.jzsj;
                            zyYpjfb.zxks = ks;
                            zyYpjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                            zyYpjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                            zyYpjfb.cxry = "";
                            zyYpjfb.cxzyjfbbh = 0;
                            zyYpjfb.zxid = 0;
                            zyYpjfb.mxid = 0;
                            zyYpjfb.price = zyYpjfb.dj;
                            zyYpjfb.lb = 0;
                            zyYpjfb.zltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            zyYpjfb.OrganizeId = orgId;
                            zyYpjfb.kflb = item.kflb;
                            zyYpjfb.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_ypjfb"));
                            inpatsqlVo.zy_ypjfbList.Add(zyYpjfb);
                            #endregion
                        }
                        else//Hạng mục
                        {
                            #region 项目计费表
                            HospItemBillingEntity zyXmjfb = new HospItemBillingEntity();
                            zyXmjfb.zyh = bacDto.mzh;
                            zyXmjfb.yzwym = "0";
                            zyXmjfb.tdrq = item.jzsj;
                            zyXmjfb.sfxm = item.sfxmCode;
                            zyXmjfb.dl = item.sfdlCode;
                            zyXmjfb.ys = ys;
                            zyXmjfb.ysmc = ysmc;
                            zyXmjfb.ks = ks;
                            zyXmjfb.ksmc = ksmc;
                            zyXmjfb.dj = item.dj;
                            zyXmjfb.sl = item.sl;
                            zyXmjfb.jfdw = item.dw;
                            zyXmjfb.ssbz = "0";
                            zyXmjfb.ssry = OperatorProvider.GetCurrent().UserCode;
                            zyXmjfb.ssrq = item.jzsj;
                            zyXmjfb.tdrq = item.jzsj;
                            zyXmjfb.zxks = ks;
                            zyXmjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                            zyXmjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                            zyXmjfb.cxry = "";
                            zyXmjfb.cxzyjfbbh = 0;
                            zyXmjfb.zxid = 0;
                            zyXmjfb.mxid = 0;
                            zyXmjfb.price = zyXmjfb.dj;
                            zyXmjfb.lb = 0;
                            zyXmjfb.zltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            zyXmjfb.clzhxmbh = 0;
                            zyXmjfb.OrganizeId = orgId;
                            zyXmjfb.duration = item.duration;
                            zyXmjfb.ttbz = item.ttbz == 1 ? true : false;
                            zyXmjfb.kflb = item.kflb;
                            zyXmjfb.OrganizeId = orgId;
                            zyXmjfb.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_xmjfb"));
                            inpatsqlVo.zy_xmjfbList.Add(zyXmjfb);
                            #endregion

                            if (optimaIds != null)
                            {
                                if (optimaIds.Keys.Contains(item.newid))
                                {
                                    optimaIds[item.newid] = zyXmjfb.jfbbh.ToString();
                                }
                            }
                        }

                    }
                    #endregion
                    _outPatChargeDmSer.PatsettDBInOptima(null, null, null, inpatsqlVo, null, null, orgId, null, optimaIds);
                }
            }
        }

        /// <summary>
        /// Cập nhật(thêm mới,sửa,xóa)ghi sổ, chỉ Đ ãxác nhận
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="bacDto"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        private void UpdateAccounting(string patientType, OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            if (accDto != null && accDto.Count() > 0)
            {

                if (patientType == "门诊")
                {
                    #region 门诊
                    var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
                    if (ghIsExits == null)
                    {
                        throw new FailedException("门诊号不存在，请确认");
                    }
                    var ghnm = ghIsExits.ghnm;
                    //***************************************Thêm thực thể******************************************************
                    //Thêm mớighi sổ đối tư ợng tập hợp 
                    var addVo = new SettInAccOutpatDataVo
                    {
                        mz_xmList = new List<OutpatientItemEntity>(),
                        mz_cf = new List<OutpatientPrescriptionEntity>(),
                        mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>()
                    };
                    var jzsjList = new List<DateTime>();
                    var gridindex = 0;
                    var cfindex = 0;
                    var strArray = new Dictionary<string, string>();  //Tạm lưu số đ ơn thuốc đ ã lưu,Tư ơng ứng của bác sĩ
                    var cFisExit = true; // Đ ơn thuốc mặc đ ịnh không tồn tại
                    var _cfh = "";//Số đ ơn thuốc
                    var _cfnm = 0; //Mã nội bộ đ ơn thuốc
                                   //***************************************EndThêm thực thể***************************************************

                    //***************************************Sửa thực thể***************************************************
                    var updateVo = new updateInAccOutpatVo
                    {
                        mz_xmList = new Dictionary<OutpatientItemEntity, OutpatientItemEntity>(),
                        mz_cf = new Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity>(),
                        mz_cfmxList = new Dictionary<OutpatientPrescriptionDetailEntity, OutpatientPrescriptionDetailEntity>()
                    };
                    var cfmxIds = new List<int>();
                    //***************************************EndSửa thực thể************************************************

                    //***************************************Xóa thực thể***************************************************
                    var delVo = new delInAccOutpatVo
                    {
                        mz_xmList = new List<int>(),
                        mz_cf = new List<int>(),
                        mz_cfmxList = new List<int>()
                    };
                    //***************************************EndXóa thực thể************************************************
                    foreach (var item in accDto)
                    {
                        string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                        if (item.ysList != null && item.ysList.Count() > 0)
                        {
                            var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
                            getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
                        }
                        OutpatientPrescriptionEntity IsSameCF = null;//Đ ơn thuốc ngoại trú bảng, đ ể SửaĐ ơn thuốc备 dùng 
                        if (item.yzlx == "1" && item.czlx == "1")//Đ ơn thuốc đ ã sửa
                        {
                            // ngày và chuyên viên trị liệu, phán đoán có hay không cùng một tờ Đ ơn thuốc. đ ể 避免 trùng lặp 取dữ liệu, trư ớc 取出来 
                            //1.Lấykhi trư ớc Đ ơn thuốc nếu sửa trư ớc là cùng một tờ Đ ơn thuốc, phía sau xử lý,nếu không thêm mới
                            IsSameCF = item.yzlx == "1" ? FirstOrDefault<OutpatientPrescriptionEntity>(@"SELECT  cf.*
                                            FROM    dbo.mz_cf cf
                                                    LEFT JOIN dbo.mz_cfmx cfmx ON cfmx.cfnm = cf.cfnm
                                                                                  AND cfmx.OrganizeId = cf.OrganizeId
                                            WHERE   cfmx.cfmxId = @cfmxnm  and cf.zt=1 and cfmx.zt=1
                                                    AND cf.OrganizeId = @orgId", new[] { new SqlParameter("@cfmxnm", item.jfbId), new SqlParameter("@orgId", orgId) }) : null;

                        }
                        // hai loại tình hình thêm mới:1. bảng tính phí id không tồn tại tại;2. SửaĐ ơn thuốc(ngày hoặc chuyên viên trị liệu sửa của tình hình)
                        if ((IsSameCF != null &&
                            (IsSameCF.jsrq != item.jzsj || IsSameCF.ys != ys) &&
                            item.yzlx == "1" &&
                            !string.IsNullOrWhiteSpace(item.jfbId)) ||
                            string.IsNullOrWhiteSpace(item.jfbId))
                        {
                            if (item.jzsj != null && !jzsjList.Contains(item.jzsj)) jzsjList.Add(item.jzsj);

                            #region 新增
                            if (item.yzlx == "1") //Thuốc
                            {//mz_cf mz_cfmx 

                                #region 药品

                                if (item.sfxmCode != null)
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                                    {
                                        cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                                        _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                    }
                                //Thêm qua Đ ơn thuốc bản ghi 
                                if (strArray != null && strArray.Count > 0)
                                {
                                    foreach (var t in strArray)
                                    {
                                        if (t.Value.Equals(ys))
                                        {
                                            //Cho biết đ ã tồn tại thông tin bảng chính của đ ơn thuốc này
                                            cFisExit = false; // Đ ơn thuốc đ ã tồn tại
                                        }
                                        else
                                        {
                                            cFisExit = true; // khác nhau chuyên viên trị liệu,Đ ơn thuốc khác nhau 
                                            _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                        }
                                    }
                                }

                                if (cFisExit)
                                {
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    strArray.Add(_cfh, ys);

                                    #region 添加处方

                                    var mzcf =
                                        new OutpatientPrescriptionEntity
                                        {
                                            cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                            ghnm = ghnm,
                                            patid = bacDto.patid,
                                            brxz = bacDto.brxz,
                                            mjzbz = "1",
                                            ys = ys,
                                            ysmc = ysmc,
                                            ks = ks,
                                            ksmc = ksmc,
                                            cfzt = "1",
                                            jsrq = item.jzsj,
                                            cfh = bacDto.cfh
                                        };

                                    mzcf.zt = "1";
                                    mzcf.OrganizeId = orgId;
                                    mzcf.Create();
                                    _cfnm = mzcf.cfnm;

                                    #endregion

                                    cfindex++;
                                    addVo.mz_cf.Add(mzcf);

                                    #region 处方明细

                                    var mzcfmx = new OutpatientPrescriptionDetailEntity
                                    {
                                        cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                                        cfnm = _cfnm,
                                        yp = item.sfxmCode,
                                        dl = item.sfdlCode,
                                        dj = item.dj,
                                        sl = item.sl,
                                        je = item.Zje,
                                        dw = item.dw,
                                        zt = "1",
                                        kflb = item.kflb,
                                        OrganizeId = orgId
                                    };
                                    mzcfmx.Create();
                                    addVo.mz_cfmxList.Add(mzcfmx);
                                    #endregion
                                }

                                #endregion
                            }
                            else//Hạng mục
                            {//mz_xm
                                #region 门诊项目
                                var mzxm = new OutpatientItemEntity
                                {
                                    xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                                    ghnm = ghnm,
                                    patid = bacDto.patid,
                                    brxz = bacDto.brxz,
                                    mjzbz = "1",
                                    ys = ys,
                                    ks = ks,
                                    ysmc = ysmc,
                                    ksmc = ksmc,
                                    sfxm = item.sfxmCode,
                                    dl = item.sfdlCode,
                                    dj = item.dj,
                                    sl = item.sl,
                                    je = item.Zje,
                                    xmzt = "1",
                                    xmly = "0",
                                    ssbz = "0",
                                    ssrq = item.jzsj,
                                    jsrq = DateTime.Now,
                                    zt = "1",
                                    OrganizeId = orgId,
                                    kflb = item.kflb,
                                    ttbz = item.ttbz == 1 ? true : false,
                                    duration = item.duration
                                };
                                mzxm.Create();

                                addVo.mz_xmList.Add(mzxm);

                                #endregion
                            }
                            gridindex++;

                            #endregion

                            if (item.yzlx == "1" && item.czlx == "1")//Đ ơn thuốc đ ã sửa,xóa trư ớc đ ó logic 
                            {
                                #region 删除处方明细
                                delVo.mz_cfmxList.Add(int.Parse(item.jfbId));// liên quan chi tiết đ ơn thuốc xóa
                                #endregion
                            }
                        }
                        else
                        {
                            if ((item.czlx == "2" || item.czlx == "1") && string.IsNullOrWhiteSpace(item.jfbId) && !cfmxIds.Contains(int.Parse(item.jfbId)))
                            {
                                cfmxIds.Add(int.Parse(item.jfbId));
                            }
                            if (item.czlx == "2")//Xóa
                            {
                                if (item.yzlx == "1")//Thuốc
                                {
                                    #region 删除处方明细
                                    delVo.mz_cfmxList.Add(int.Parse(item.jfbId));// liên quan chi tiết đ ơn thuốc xóa
                                    #endregion
                                }
                                else if (item.yzlx == "2")//Hạng mục
                                {
                                    delVo.mz_xmList.Add(int.Parse(item.jfbId));
                                }
                            }
                            else if (item.czlx == "1")//Sửa
                            {
                                #region 更改
                                //Chi tiết quyết toán bảng tư ơng ứngmz_jsmx
                                if (item.yzlx == "1") //Thuốc
                                {
                                    // theo ghi sổ ngày và bác sĩ, phán đoán có hay không cùng một tờ Đ ơn thuốc
                                    if (IsSameCF.jsrq == item.jzsj && IsSameCF.ys == ys)
                                    {
                                        var updatemzcfmx = FirstOrDefault<OutpatientPrescriptionDetailEntity>(@"SELECT   mx.*
                                                                               FROM     dbo.mz_cfmx mx
                                                                                        LEFT JOIN mz_cf cf ON cf.cfnm = mx.cfnm
                                                                               WHERE    mx.cfmxId=@cfmxId
                                                                                        AND mx.OrganizeId = @orgId
                                                                                        AND cf.OrganizeId = @orgId",
                                                                                            new[] { new SqlParameter("@cfmxId", item.jfbId),
                                        new SqlParameter("@orgId", orgId) });
                                        var oldmzcfmx = updatemzcfmx.Clone();
                                        if (updatemzcfmx != null)
                                        {
                                            updatemzcfmx.yp = item.sfxmCode;
                                            updatemzcfmx.dl = item.sfdlCode;
                                            updatemzcfmx.dj = item.dj;
                                            updatemzcfmx.sl = item.sl;
                                            updatemzcfmx.je = item.dj * item.sl;
                                            updatemzcfmx.dw = item.dw;
                                            updatemzcfmx.kflb = item.kflb;
                                        }
                                        updatemzcfmx.Modify();
                                        if (!updateVo.mz_cfmxList.ContainsKey(oldmzcfmx))
                                        {
                                            updateVo.mz_cfmxList.Add(oldmzcfmx, updatemzcfmx);
                                        }
                                    }
                                }
                                else if (item.yzlx == "2")//Hạng mục thu phí sửa
                                {

                                    var updatemzxm = FirstOrDefault<OutpatientItemEntity>(@"select * from mz_xm where xmnm=@xmnm AND OrganizeId=@orgId",
                                        new[] { new SqlParameter("@xmnm", item.jfbId),
                                    new SqlParameter("@orgId", orgId) });
                                    var oldmzxm = updatemzxm.Clone();
                                    if (updatemzxm != null)
                                    {
                                        updatemzxm.sfxm = item.sfxmCode;
                                        updatemzxm.ys = ys;
                                        updatemzxm.ysmc = ysmc;
                                        updatemzxm.ks = ks;
                                        updatemzxm.ksmc = ksmc;
                                        updatemzxm.dl = item.sfdlCode;
                                        updatemzxm.dj = item.dj;
                                        updatemzxm.sl = item.sl;
                                        updatemzxm.je = item.dj * item.sl;
                                        updatemzxm.ssrq = item.jzsj;
                                        updatemzxm.ttbz = item.ttbz == 1 ? true : false;
                                        updatemzxm.duration = item.duration;
                                        updatemzxm.kflb = item.kflb;
                                        updatemzxm.Modify();
                                        if (!updateVo.mz_xmList.ContainsKey(oldmzxm))
                                        {
                                            updateVo.mz_xmList.Add(oldmzxm, updatemzxm);
                                        }
                                    }
                                }

                                #endregion
                            }
                        }

                    }
                    #endregion
                    _outPatChargeDmSer.PatsettDBInOptima(addVo, updateVo, delVo, null, null, null, orgId, cfmxIds, null);
                }
                else if (patientType == "住院")
                {
                    #region 住院
                    var zyIsExits = _hospPatientBasicInfoRepo.IQueryable().FirstOrDefault(p => p.zyh == bacDto.mzh && p.zt == "1" && (p.zybz == "1" || p.zybz == "2" || p.zybz == "3") && p.OrganizeId == orgId);
                    if (zyIsExits == null)
                    {
                        throw new FailedException("该病人不在住院状态，请确认");
                    }
                    //***************************************Thêm thực thể******************************************************
                    //Thêm mớighi sổ đối tư ợng tập hợp 
                    var inpataddVo = new SettInAccHospatDataVo
                    {
                        zy_xmjfbList = new List<HospItemBillingEntity>(),
                        zy_ypjfbList = new List<HospDrugBillingEntity>()
                    };
                    //***************************************EndThêm thực thể***************************************************

                    //***************************************Sửa thực thể***************************************************
                    var inpatupdateVo = new updateInAccHospatVo
                    {
                        zy_xmjfbList = new Dictionary<HospItemBillingEntity, HospItemBillingEntity>(),
                        zy_ypjfbList = new Dictionary<HospDrugBillingEntity, HospDrugBillingEntity>()
                    };
                    //***************************************EndSửa thực thể************************************************

                    //***************************************Xóa thực thể***************************************************
                    var inpatdelVo = new delInAccHospatVo
                    {
                        zy_xmjfbList = new List<string>(),
                        zy_ypjfbList = new List<string>()
                    };
                    //***************************************EndXóa thực thể************************************************
                    foreach (var item in accDto)
                    {
                        string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                        if (item.ysList != null && item.ysList.Count() > 0)
                        {
                            var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
                            getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
                        }

                        if (!string.IsNullOrWhiteSpace(item.jfbId) && item.czlx == "0")
                        {
                            #region 新增
                            if (item.yzlx == "1") //Thuốc
                            {
                                #region 药品计费表
                                HospDrugBillingEntity zyYpjfb = new HospDrugBillingEntity();
                                zyYpjfb.zyh = bacDto.mzh;
                                zyYpjfb.yzwym = "0";
                                zyYpjfb.tdrq = item.jzsj;
                                zyYpjfb.yp = item.sfxmCode;
                                zyYpjfb.dl = item.sfdlCode;
                                zyYpjfb.ys = ys;
                                zyYpjfb.ks = ks;
                                zyYpjfb.ysmc = ysmc;
                                zyYpjfb.ksmc = ksmc;
                                zyYpjfb.dj = item.dj;
                                zyYpjfb.sl = item.sl;
                                zyYpjfb.jfdw = item.dw;
                                zyYpjfb.pyry = 0;
                                zyYpjfb.pyrq = item.jzsj;
                                zyYpjfb.fyry = OperatorProvider.GetCurrent().UserCode;
                                zyYpjfb.fyrq = item.jzsj;
                                zyYpjfb.zxks = ks;
                                zyYpjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                                zyYpjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                                zyYpjfb.cxry = "";
                                zyYpjfb.cxzyjfbbh = 0;
                                zyYpjfb.zxid = 0;
                                zyYpjfb.mxid = 0;
                                zyYpjfb.price = zyYpjfb.dj;
                                zyYpjfb.lb = 0;
                                zyYpjfb.zltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                zyYpjfb.OrganizeId = orgId;
                                zyYpjfb.kflb = item.kflb;
                                zyYpjfb.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_ypjfb"));
                                inpataddVo.zy_ypjfbList.Add(zyYpjfb);
                                #endregion
                            }
                            else//Hạng mục
                            {
                                #region 项目计费表
                                HospItemBillingEntity zyXmjfb = new HospItemBillingEntity();
                                zyXmjfb.zyh = bacDto.mzh;
                                zyXmjfb.yzwym = "0";
                                zyXmjfb.tdrq = item.jzsj;
                                zyXmjfb.sfxm = item.sfxmCode;
                                zyXmjfb.dl = item.sfdlCode;
                                zyXmjfb.ys = ys;
                                zyXmjfb.ks = ks;
                                zyXmjfb.ysmc = ysmc;
                                zyXmjfb.ksmc = ksmc;
                                zyXmjfb.dj = item.dj;
                                zyXmjfb.sl = item.sl;
                                zyXmjfb.jfdw = item.dw;
                                zyXmjfb.ssbz = "0";
                                zyXmjfb.ssry = OperatorProvider.GetCurrent().UserCode;
                                zyXmjfb.ssrq = item.jzsj;
                                zyXmjfb.zxks = ks;
                                zyXmjfb.yzxz = ((int)EnumYZXZ.LSYZ).ToString();
                                zyXmjfb.yzzt = ((int)EnumYZZT.WCX).ToString();
                                zyXmjfb.cxry = "";
                                zyXmjfb.cxzyjfbbh = 0;
                                zyXmjfb.zxid = 0;
                                zyXmjfb.mxid = 0;
                                zyXmjfb.price = zyXmjfb.dj;
                                zyXmjfb.lb = 0;
                                zyXmjfb.zltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                zyXmjfb.clzhxmbh = 0;
                                zyXmjfb.duration = item.duration;
                                zyXmjfb.ttbz = item.ttbz == 1 ? true : false;
                                zyXmjfb.kflb = item.kflb;
                                zyXmjfb.OrganizeId = orgId;
                                inpataddVo.zy_xmjfbList.Add(zyXmjfb);
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            if (item.czlx == "2") //Xóa
                            {
                                if (item.yzlx == "1")//Thuốc
                                {
                                    #region 删除药品计费表
                                    inpatdelVo.zy_ypjfbList.Add(item.jfbId);
                                    #endregion
                                }
                                else if (item.yzlx == "2")//Hạng mục
                                {
                                    inpatdelVo.zy_xmjfbList.Add(item.jfbId);
                                }
                            }
                            else if (item.czlx == "1")//Sửa
                            {
                                if (item.yzlx == "1")//Thuốc
                                {
                                    var jfbid = int.Parse(item.jfbId);
                                    var updateyp = _hospDrugBillingRepo.IQueryable().FirstOrDefault(p => p.jfbbh == jfbid && p.zt == "1" && p.OrganizeId == orgId);
                                    var oldyp = updateyp.Clone();
                                    if (updateyp != null)
                                    {
                                        updateyp.yp = item.sfxmCode;
                                        updateyp.dl = item.sfdlCode;
                                        updateyp.ys = ys;
                                        updateyp.ks = ks;
                                        updateyp.ysmc = ysmc;
                                        updateyp.ksmc = ksmc;
                                        updateyp.dj = item.dj;
                                        updateyp.sl = item.sl;
                                        updateyp.jfdw = item.dw;
                                        updateyp.pyrq = item.jzsj;
                                        updateyp.price = updateyp.dj;
                                        updateyp.tdrq = item.jzsj;
                                        updateyp.kflb = item.kflb;
                                        updateyp.Modify();
                                        if (!inpatupdateVo.zy_ypjfbList.ContainsKey(oldyp))
                                        {
                                            inpatupdateVo.zy_ypjfbList.Add(oldyp, updateyp);
                                        }
                                    }

                                }
                                else if (item.yzlx == "2")//Hạng mục
                                {
                                    var jfbid = int.Parse(item.jfbId);
                                    var updatexm = _hospItemBillingRepo.IQueryable().FirstOrDefault(p => p.jfbbh == jfbid && p.zt == "1" && p.OrganizeId == orgId);
                                    var oldxm = updatexm.Clone();
                                    if (oldxm != null)
                                    {
                                        updatexm.sfxm = item.sfxmCode;
                                        updatexm.dl = item.sfdlCode;
                                        updatexm.ys = ys;
                                        updatexm.ks = ks;
                                        updatexm.ysmc = ysmc;
                                        updatexm.ksmc = ksmc;
                                        updatexm.dj = item.dj;
                                        updatexm.sl = item.sl;
                                        updatexm.jfdw = item.dw;
                                        updatexm.tdrq = item.jzsj;
                                        updatexm.price = updatexm.dj;
                                        updatexm.kflb = item.kflb;
                                        updatexm.duration = item.duration;
                                        updatexm.ttbz = item.ttbz == 1 ? true : false;
                                        updatexm.Modify();
                                        if (!inpatupdateVo.zy_xmjfbList.ContainsKey(oldxm))
                                        {
                                            inpatupdateVo.zy_xmjfbList.Add(oldxm, updatexm);
                                        }
                                    }
                                }
                            }
                        }


                    }
                    #endregion

                    _outPatChargeDmSer.PatsettDBInOptima(null, null, null, inpataddVo, inpatupdateVo, inpatdelVo, orgId, null, null);
                }
            }
        }
        #endregion

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// LưuGhi sổ ngoại trú
        /// </summary>
        public void SavepatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
            if (ghIsExits == null)
            {
                throw new FailedException("门诊号不存在，请确认");
            }
            var ghnm = ghIsExits.ghnm;
            //***************************************Thêm thực thể******************************************************
            //Thêm mớighi sổ đối tư ợng tập hợp 
            var sqlVo = new OutPatSettInAccDataVo
            {
                mz_cf = new List<OutpatientPrescriptionEntity>(),
                mz_cfmxList = new List<OutpatientPrescriptionDetailEntity>(),
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new List<OutpatientSettlementEntity>(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>(),
                mz_jzjh = null,
                mz_jzjhmx = new List<OutpatientAccountDetailEntity>()

            };
            var jzsjList = new List<DateTime>();
            var gridindex = 0;
            var cfindex = 0;
            var strArray = new Dictionary<string, string>();  //Tạm lưu số đ ơn thuốc đ ã lưu,Tư ơng ứng của bác sĩ
            var cFisExit = true; // Đ ơn thuốc mặc đ ịnh không tồn tại
            var _cfh = "";//Số đ ơn thuốc
            var _cfnm = 0; //Mã nội bộ đ ơn thuốc

            //***************************************EndThêm thực thể***************************************************

            //***************************************Sửa thực thể***************************************************
            var updateVo = new updatePatSettInAccDataVo
            {
                //Key cũ thực thể,Value mới thực thể,là sửa dữ liệu kho giờ, bản ghi nhật ký chuẩn bị 
                mz_cf = new Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity>(),
                mz_cfmxList = new Dictionary<OutpatientPrescriptionDetailEntity,
                OutpatientPrescriptionDetailEntity>(),
                mz_xmList = new Dictionary<OutpatientItemEntity, OutpatientItemEntity>(),
                mz_js = new Dictionary<OutpatientSettlementEntity, OutpatientSettlementEntity>(),
                mz_jsmxList = new Dictionary<OutpatientSettlementDetailEntity, OutpatientSettlementDetailEntity>(),
                mz_jzjh = new Dictionary<OutpatientAccountEntity, OutpatientAccountEntity>(),
                mz_jzjhmx = new Dictionary<OutpatientAccountDetailEntity, OutpatientAccountDetailEntity>()
            };
            //***************************************EndSửa thực thể***************************************************

            //***************************************Xóa thực thể***************************************************
            var DelVo = new DelPatSettInAccDataVo
            {
                mz_cf = new List<int>(),
                mz_cfmxList = new List<int>(),
                mz_xmList = new List<int>(),
                mz_js = new List<int>(),
                mz_jsmxList = new List<int>(),
                mz_jzjh = new List<string>(),
                mz_jzjhmx = new List<string>(),
            };
            var del_jsnm = new List<int>();//Cần xóa và cập nhật tổng số tiền của mã nội bộ quyết toán
            //***************************************EndXóa thực thể***************************************************
            foreach (var item in accDto)
            {
                string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
                OutpatientPrescriptionEntity IsSameCF = null;//Đ ơn thuốc ngoại trú bảng, đ ể SửaĐ ơn thuốc备 dùng 
                if (item.ysList != null && item.ysList.Count() > 0)
                {
                    var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
                    getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
                }
                OutpatientSettlementDetailEntity updatejsmx = null;
                if (!string.IsNullOrWhiteSpace(item.xmnm.ToString()))//Đ ơn thuốc đ ã sửa
                {
                    updatejsmx = _outpatientSettlementDetailRepo.FindEntity(p => p.mxnm == item.xmnm);
                    if (item.yzlx == "1" && item.czlx == "1")//Đ ơn thuốc đ ã sửa
                    {
                        // phán đoán jsnm, ngày và chuyên viên trị liệu, phán đoán có hay không cùng một tờ Đ ơn thuốc. đ ể 避免 trùng lặp 取dữ liệu, trư ớc 取出来 
                        //1.Lấykhi trư ớc Đ ơn thuốc nếu sửa trư ớc là cùng một tờ Đ ơn thuốc, phía sau xử lý,nếu không thêm mới
                        IsSameCF = item.yzlx == "1" ? FirstOrDefault<OutpatientPrescriptionEntity>(@"SELECT  cf.*
                                            FROM    dbo.mz_cf cf
                                                    LEFT JOIN dbo.mz_cfmx cfmx ON cfmx.cfnm = cf.cfnm
                                                                                  AND cfmx.OrganizeId = cf.OrganizeId
                                                    LEFT JOIN dbo.mz_jsmx js ON cfmx.cfmxId = js.cf_mxnm
                                                                                AND CF.OrganizeId = JS.OrganizeId
                                            WHERE   cfmx.cfmxId = @cfmxnm
                                                    AND cf.OrganizeId = @orgId", new[] { new SqlParameter("@cfmxnm", updatejsmx.cf_mxnm), new SqlParameter("@orgId", orgId) }) : null;

                    }
                }
                // hai loại tình hình thêm mới:1.Chi tiết quyết toán mã nội bộ không tồn tại tại;2. SửaĐ ơn thuốc(ngày hoặc chuyên viên trị liệu sửa của tình hình)
                if (string.IsNullOrWhiteSpace(item.xmnm.ToString()) || (IsSameCF != null && (IsSameCF.jsrq != item.jzsj || IsSameCF.ys != ys) && (!string.IsNullOrWhiteSpace(item.xmnm.ToString())) && item.yzlx == "1"))
                {
                    sqlVo.mz_jzjh = new OutpatientAccountEntity();
                    sqlVo.mz_jzjh.OrganizeId = orgId;
                    sqlVo.mz_jzjh.Create(true, null);

                    #region 新增
                    if (item.jzsj != null && !jzsjList.Contains(item.jzsj)) jzsjList.Add(item.jzsj);

                    #region 记账计划明细
                    var mxEntity = new OutpatientAccountDetailEntity
                    {
                        jzjhId = sqlVo.mz_jzjh.jzjhId,
                        zlsc = item.duration,
                        sfxmCode = item.sfxmCode,
                        sl = item.sl,
                        jzsj = item.jzsj,
                        bz = item.bz,
                        ys = ys,
                        ks = ks,
                        ysmc = ysmc,
                        ksmc = ksmc,
                        ttbz = item.ttbz == 1 ? true : false,
                        kflb = item.kflb,
                        OrganizeId = orgId
                    };
                    mxEntity.Create(true);
                    sqlVo.mz_jzjhmx.Add(mxEntity);
                    #endregion

                    #region 如果记账时间是同一天，作为一笔记账数据,门诊结算，结算大类，门诊项目重新处理
                    if (sqlVo.mz_js.Any(p => p.jzsj == item.jzsj))
                    {
                        #region 门诊结算
                        var oldDto = sqlVo.mz_js.FirstOrDefault(p => p.jzsj == item.jzsj);
                        if (oldDto != null)
                        {
                            oldDto.zje += item.Zje;
                        }

                        #endregion

                        if (oldDto != null)
                        {

                            if (item.yzlx == "1") //Thuốc
                            {
                                #region 药品

                                if (item.sfxmCode != null)
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    if (gridindex == 0 || cfindex == 0 || strArray.Count == 0)
                                    {
                                        cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                                        _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                    }
                                //Thêm qua Đ ơn thuốc bản ghi 
                                if (strArray != null && strArray.Count > 0)
                                {
                                    foreach (var t in strArray)
                                    {
                                        if (t.Value.Equals(ys))
                                        {
                                            //Cho biết đ ã tồn tại thông tin bảng chính của đ ơn thuốc này
                                            cFisExit = false; // Đ ơn thuốc đ ã tồn tại
                                        }
                                        else
                                        {
                                            cFisExit = true; // khác nhau chuyên viên trị liệu,Đ ơn thuốc khác nhau 
                                            _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                                        }
                                    }
                                }

                                if (cFisExit)
                                {
                                    //Thêm thông tin bảng chính đ ơn thuốc
                                    strArray.Add(_cfh, ys);

                                    #region 添加处方

                                    var mzcf =
                                        new OutpatientPrescriptionEntity
                                        {
                                            cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                            ghnm = ghnm,
                                            patid = bacDto.patid,
                                            brxz = bacDto.brxz,
                                            mjzbz = "1",
                                            ys = ys,
                                            ysmc = ysmc,
                                            ks = ks,
                                            ksmc = ksmc,
                                            cfzt = "1",
                                            jsnm = oldDto.jsnm,
                                            jsrq = item.jzsj,
                                            cfh = bacDto.cfh,

                                        };

                                    mzcf.zt = "1";
                                    mzcf.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                                    mzcf.Create();
                                    _cfnm = mzcf.cfnm;

                                    #endregion

                                    cfindex++;
                                    sqlVo.mz_cf.Add(mzcf);


                                }

                                #endregion

                                #region 处方明细

                                var mzcfmx = new OutpatientPrescriptionDetailEntity
                                {
                                    cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                                    cfnm = _cfnm,
                                    yp = item.sfxmCode,
                                    dl = item.sfdlCode,
                                    dj = item.dj,
                                    sl = item.sl,
                                    je = item.Zje,
                                    dw = item.dw,
                                    zt = "1",
                                    //jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                                    kflb = item.kflb,
                                    OrganizeId = orgId
                                };
                                mzcfmx.Create();
                                sqlVo.mz_cfmxList.Add(mzcfmx);
                                #endregion

                                #region 门诊结算明细
                                var mzjsmx = new OutpatientSettlementDetailEntity();
                                mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                                mzjsmx.jsnm = oldDto.jsnm;
                                mzjsmx.mxnm = 0;
                                mzjsmx.sl = item.sl;
                                mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                                mzjsmx.cf_mxnm = mzcfmx.cfmxId;
                                mzjsmx.zt = "1";
                                mzjsmx.OrganizeId = orgId;
                                mzjsmx.CreateTime = DateTime.Now;
                                mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                                mzjsmx.jyje = (item.sl * item.dj);
                                sqlVo.mz_jsmxList.Add(mzjsmx);
                                #endregion
                            }
                            else//Hạng mục
                            {
                                #region 门诊项目
                                var xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                var mzxm2 = new OutpatientItemEntity
                                {
                                    xmnm = xmnm,
                                    ghnm = ghnm,
                                    patid = bacDto.patid,
                                    brxz = bacDto.brxz,
                                    mjzbz = "1",
                                    ys = ys,
                                    ks = ks,
                                    ysmc = ysmc,
                                    ksmc = ksmc,
                                    sfxm = item.sfxmCode,
                                    dl = item.sfdlCode,
                                    dj = item.dj,
                                    sl = item.sl,
                                    je = item.Zje,
                                    xmzt = "1",
                                    xmly = "0",
                                    ssbz = "1",
                                    ssry = ys,
                                    ssrq = item.jzsj,
                                    jsrq = DateTime.Now,
                                    zt = "1",
                                    OrganizeId = orgId,
                                    kflb = item.kflb,
                                    //jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                                    ttbz = item.ttbz == 1 ? true : false,
                                    duration = item.duration,
                                    bz = item.bz,
                                    zfbl = item.zfbl,
                                    zfxz = item.zfxz,
                                    dw = item.dw,
                                    jmbl = null,
                                    jmje = null
                                };
                                mzxm2.Create();

                                sqlVo.mz_xmList.Add(mzxm2);
                                #endregion

                                #region 门诊结算明细
                                var mzjsmx = new OutpatientSettlementDetailEntity();
                                mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                                mzjsmx.jsnm = oldDto.jsnm;
                                mzjsmx.mxnm = mzxm2.xmnm;
                                mzjsmx.sl = item.sl;
                                mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                                mzjsmx.cf_mxnm = 0;
                                mzjsmx.zt = "1";
                                mzjsmx.OrganizeId = orgId;
                                mzjsmx.CreateTime = DateTime.Now;
                                mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                                mzjsmx.jyje = (item.sl * item.dj);
                                sqlVo.mz_jsmxList.Add(mzjsmx);
                                #endregion
                            }
                        }
                        continue;
                        #endregion
                    }
                    else
                    {
                        #region 门诊结算
                        var jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js");
                        var mzjs = new OutpatientSettlementEntity
                        {
                            jsnm = jsnm,
                            patid = bacDto.patid,
                            ghnm = ghnm,
                            brxz = bacDto.brxz,
                            jslx = "2",
                            jszt = (int)Constants.jsztEnum.YJ,
                            zffy = 0,
                            flzffy = 0,
                            cxjsnm = 0,
                            zh = null,
                            fpdm = null,
                            jmje = 0,
                            jmbl = 0,
                            zt = "1",
                            jzsj = item.jzsj,
                            OrganizeId = orgId,
                            CreateTime = DateTime.Now,
                            CreatorCode = OperatorProvider.GetCurrent().UserCode,
                            xm = bacDto.xm,
                            xb = bacDto.xb,
                            blh = bacDto.blh,
                            csny = bacDto.csny,
                            zjh = bacDto.zjh,
                            fph = null,
                            zjlx = bacDto.zjlx
                        };

                        mzjs.zffy = 0;
                        mzjs.xjzf = 0;
                        mzjs.xjwc = 0;
                        mzjs.zlfy = 0;
                        mzjs.jzfy = 0;
                        mzjs.zje = item.Zje;
                        sqlVo.mz_js.Add(mzjs);
                        #endregion

                        if (item.yzlx == "1") //Thuốc
                        {//mz_cf mz_cfmx 

                            #region 药品

                            if (item.sfxmCode != null)
                                //Thêm thông tin bảng chính đ ơn thuốc
                                cFisExit = true; //Đ ơn thuốc không tồn tại,thêm bảng chính đ ơn thuốc
                            _cfh = getCflsh(); //Số thứ tự đ ơn thuốc
                            var mz_cf = new OutpatientPrescriptionEntity();
                            var mz_jsmx = new OutpatientSettlementDetailEntity();
                            var mz_cfmx = new OutpatientPrescriptionDetailEntity();
                            if (cFisExit)
                            {
                                cfindex++;
                            }
                            Addcf(bacDto, item, orgId, ghnm, jsnm, mxEntity, ref strArray, ref _cfnm, _cfh, cFisExit, out mz_cf, out mz_jsmx, out mz_cfmx);
                            sqlVo.mz_cf.Add(mz_cf);
                            sqlVo.mz_jsmxList.Add(mz_jsmx);
                            sqlVo.mz_cfmxList.Add(mz_cfmx);

                            //if (cFisExit)
                            //{
                            ////Thêm thông tin bảng chính đ ơn thuốc
                            //strArray.Add(_cfh, ys);

                            //#region Th êmĐ ơn thuốc

                            //var mzcf =
                            //    new OutpatientPrescriptionEntity
                            //    {
                            //        cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                            //        ghnm = ghnm,
                            //        patid = bacDto.patid,
                            //        brxz = bacDto.brxz,
                            //        mjzbz = "1",
                            //        ys = ys,
                            //        ysmc = ysmc,
                            //        ks = ks,
                            //        ksmc = ksmc,
                            //        cfzt = "1",
                            //        fybz = "0",
                            //        pyry = "0",
                            //        pyrq = DateTime.Now,
                            //        fyry = "0",
                            //        fyrq = DateTime.Now,
                            //        jsnm = jsnm,
                            //        jsrq = DateTime.Now,
                            //        cxjsnm = 0,
                            //        cfff = "",
                            //        cfh = bacDto.cfh
                            //    };

                            //mzcf.zt = "1";
                            //mzcf.OrganizeId = orgId;
                            //mzcf.Create();
                            //_cfnm = mzcf.cfnm;

                            //#endregion

                            //cfindex++;
                            //addVo.mz_cf.Add(mzcf);

                            //#region Chi tiết quyết toán ngoại trú
                            //var mzjsmx = new OutpatientSettlementDetailEntity();
                            //mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                            //mzjsmx.jsnm = jsnm;
                            //mzjsmx.mxnm = 0;
                            //mzjsmx.mxbm = item.sfxmCode;
                            //mzjsmx.sl = item.sl;
                            //mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                            //mzjsmx.cf_mxnm = mzcf.cfnm;
                            //mzjsmx.zt = "1";
                            //mzjsmx.OrganizeId = orgId;
                            //mzjsmx.CreateTime = DateTime.Now;
                            //mzjsmx.CreatorCode = userModel.UserCode;
                            //addVo.mz_jsmxList.Add(mzjsmx);
                            //#endregion
                            //}

                            //#endregion

                            //#region chi tiết đ ơn thuốc

                            //var mzcfmx = new OutpatientPrescriptionDetailEntity
                            //{
                            //    cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                            //    cfnm = _cfnm,
                            //    yp = item.sfxmCode,
                            //    dl = item.sfdlCode,
                            //    plh = 0,
                            //    dj = item.dj,
                            //    sl = item.sl,
                            //    ts = 0,
                            //    je = item.Zje,
                            //    zfbl = 0,
                            //    zfxz = "1",
                            //    zlff = "",
                            //    sjap = "",
                            //    yl = 0,
                            //    yldw = item.dw,
                            //    cxjsnm = 0,
                            //    cxjsbz = "",
                            //    tybz = "",
                            //    bz = "",
                            //    cls = 0,
                            //    yfdm = "",
                            //    bzdm = "",
                            //    fwfdj = 0M,
                            //    zt = "1",
                            //    jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                            //    kflb = item.kflb,
                            //    OrganizeId = orgId
                            //};
                            //mzcfmx.Create();
                            //addVo.mz_cfmxList.Add(mzcfmx);
                            #endregion
                        }
                        else//Hạng mục
                        {//mz_xm
                            #region 门诊项目
                            var mzxm = new OutpatientItemEntity
                            {
                                xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                                ghnm = ghnm,
                                patid = bacDto.patid,
                                brxz = bacDto.brxz,
                                mjzbz = "1",
                                ys = ys,
                                ks = ks,
                                ysmc = ysmc,
                                ksmc = ksmc,
                                sfxm = item.sfxmCode,
                                dl = item.sfdlCode,
                                dj = item.dj,
                                sl = item.sl,
                                je = item.Zje,
                                xmzt = "1",
                                xmly = "0",
                                ssbz = "1",
                                ssry = ys,
                                ssrq = item.jzsj,
                                jsrq = DateTime.Now,
                                zt = "1",
                                OrganizeId = orgId,
                                kflb = item.kflb,
                                //jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                                ttbz = item.ttbz == 1 ? true : false,
                                duration = item.duration,
                                bz = item.bz,
                                zfbl = item.zfbl,
                                zfxz = item.zfxz,
                                dw = item.dw,
                                jmbl = null,
                                jmje = null
                            };
                            mzxm.Create();

                            sqlVo.mz_xmList.Add(mzxm);

                            #endregion

                            #region 门诊结算明细
                            var mzjsmx = new OutpatientSettlementDetailEntity();
                            mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                            mzjsmx.jsnm = jsnm;
                            mzjsmx.mxnm = mzxm.xmnm;
                            mzjsmx.sl = item.sl;
                            mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                            mzjsmx.cf_mxnm = 0;
                            mzjsmx.zt = "1";
                            mzjsmx.OrganizeId = orgId;
                            mzjsmx.CreateTime = DateTime.Now;
                            mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                            mzjsmx.jyje = (item.sl * item.dj);
                            sqlVo.mz_jsmxList.Add(mzjsmx);
                            #endregion
                        }
                        gridindex++;
                    }

                    #endregion
                    if (item.yzlx == "1" && item.czlx == "1")//Đ ơn thuốc đ ã sửa,xóa trư ớc đ ó logic 
                    {
                        #region 删除处方相关
                        DelVo.mz_jsmxList.Add(updatejsmx.jsmxnm);// liên quan chi tiết quyết toán xóa
                        DelVo.mz_cfmxList.Add(updatejsmx.cf_mxnm.Value);// liên quan chi tiết đ ơn thuốc xóa
                        var jzjhmx = FirstOrDefault<string>(@"SELECT TOP 1 jzjhmxId FROM dbo.mz_cfmx WHERE cfmxId=@cfmxnm AND OrganizeId=@orgId",
                               new[] { new SqlParameter("@cfmxnm", updatejsmx.cf_mxnm), new SqlParameter("@orgId", orgId) });
                        DelVo.mz_jzjhmx.Add(jzjhmx);// liên quan chi tiết kế hoạch ghi sổ xóa
                        #endregion
                    }

                }
                else
                {
                    var jsnm = _outpatientSettlementDetailRepo.IQueryable().Where(p => p.mxnm == item.xmnm && p.OrganizeId == orgId).Select(p => p.jsnm).FirstOrDefault();
                    //string jzjhmxId = item.yzlx == "1" ? FirstOrDefault<string>(@"SELECT TOP 1 jzjhmxId FROM dbo.mz_cfmx WHERE cfmxId=@cfmxnm AND OrganizeId=@orgId",
                    //           new[] { new SqlParameter("@cfmxnm", updatejsmx.cf_mxnm), new SqlParameter("@orgId", orgId) }) :
                    //           FirstOrDefault<string>("SELECT DISTINCT jzjhmxId FROM MZ_XM WHERE jsnm = @jsnm",
                    // new[] { new SqlParameter("@jsnm", jsnm) });// cần xóa củachi tiết kế hoạch ghi sổ id
                    del_jsnm.Add(int.Parse(jsnm.ToString()));//Thêm trạng thái quyết toán,刷 mới bảng chính trạng thái và tổng số tiền
                    if (item.czlx == "2")
                    {
                        #region 删除

                        #region 删除结算明细
                        DelVo.mz_jsmxList.Add(updatejsmx.jsmxnm);
                        #endregion

                        #region 删除计费表明细 获取记账计划明细Id
                        if (item.yzlx == "1") //Thuốc
                        {
                            DelVo.mz_cfmxList.Add(updatejsmx.cf_mxnm.Value);

                        }
                        else if (item.yzlx == "2")
                        {
                            //var xmnm = FirstOrDefault<int>(@"SELECT  xm.xmnm
                            //            FROM    mz_xm xm
                            //                    LEFT JOIN dbo.mz_jsmx jsmx ON xm.xmnm = jsmx.mxnm
                            //                                                  AND xm.jsnm = jsmx.jsnm
                            //                                                  AND jsmx.OrganizeId = xm.OrganizeId
                            //            WHERE   xm.jsnm = @jsnm  AND jsmx.jsmxnm = @jsmxnm
                            //                    AND jsmx.OrganizeId = @orgId", new[] { new SqlParameter("@jsmxnm", item.jsmxnm), new SqlParameter("@orgId", orgId), new SqlParameter("@jsnm", jsnm) });

                            DelVo.mz_xmList.Add(item.xmnm.ToInt());
                        }
                        #endregion

                        //#region Xóa chi tiết kế hoạch ghi sổ liên quan
                        //DelVo.mz_jzjhmx.Add(jzjhmxId);
                        //#endregion
                        continue;
                        #endregion
                    }
                    else if (item.czlx == "1")//Sửa
                    {
                        #region 更改
                        //Chi tiết quyết toán bảng tư ơng ứngmz_jsmx

                        if (updatejsmx != null)
                        {
                            var oldjsmx = updatejsmx.Clone();
                            if (item.yzlx == "1") //Thuốc
                            {
                                // theo ghi sổ ngày và bác sĩ, phán đoán có hay không cùng một tờ Đ ơn thuốc
                                if (IsSameCF.jsrq == item.jzsj && IsSameCF.ys == ys)
                                {
                                    var updatemzcfmx = FirstOrDefault<OutpatientPrescriptionDetailEntity>(@"SELECT   mx.*
                                                                               FROM     dbo.mz_cfmx mx
                                                                                        LEFT JOIN mz_cf cf ON cf.cfnm = mx.cfnm
                                                                               WHERE    jsnm = @jsnm
                                                                                        AND mx.OrganizeId = @orgId
                                                                                        AND cf.OrganizeId = @orgId", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                                    var oldmzcfmx = updatemzcfmx.Clone();
                                    if (updatemzcfmx != null)
                                    {
                                        updatemzcfmx.yp = item.sfxmCode;
                                        updatemzcfmx.dl = item.sfdlCode;
                                        updatemzcfmx.dj = item.dj;
                                        updatemzcfmx.sl = item.sl;
                                        updatemzcfmx.je = item.dj * item.sl;
                                        updatemzcfmx.dw = item.dw;
                                        updatemzcfmx.kflb = item.kflb;
                                    }
                                    updatemzcfmx.Modify();
                                    if (!updateVo.mz_cfmxList.ContainsKey(oldmzcfmx))
                                    {
                                        updateVo.mz_cfmxList.Add(oldmzcfmx, updatemzcfmx);
                                    }

                                    if (!string.IsNullOrWhiteSpace(updatemzcfmx.jzjhmxId))
                                    {
                                        //Kế hoạch ghi sổ sửa
                                        var updatejzjhmx = FirstOrDefault<OutpatientAccountDetailEntity>(@"select * from mz_jzjhmx where jzjhmxId=@jzjhmxId AND OrganizeId=@orgId ", new[] { new SqlParameter("@jzjhmxId", updatemzcfmx.jzjhmxId), new SqlParameter("@orgId", orgId) });
                                        var oldjzjhmx = updatejzjhmx.Clone();
                                        updatejzjhmx.sfxmCode = item.sfxmCode;
                                        updatejzjhmx.ys = ys;
                                        updatejzjhmx.ysmc = ysmc;
                                        updatejzjhmx.sl = item.sl;
                                        updatejzjhmx.jzsj = item.jzsj;
                                        updatejzjhmx.ttbz = item.ttbz == 1 ? true : false;
                                        updatejzjhmx.zlsc = item.duration;
                                        updatejzjhmx.kflb = item.kflb;

                                        updatejzjhmx.Modify();
                                        if (!updateVo.mz_jzjhmx.ContainsKey(oldjzjhmx))
                                        {
                                            updateVo.mz_jzjhmx.Add(oldjzjhmx, updatejzjhmx);
                                        }
                                        updatejsmx.sl = item.sl;
                                        updatejsmx.cf_mxnm = updatejsmx.cf_mxnm;
                                        updatejsmx.jyje = (item.sl * item.dj);
                                        updatejsmx.mxnm = 0;
                                        //updatejsmx.Modify();
                                    }
                                }


                            }
                            else if (item.yzlx == "2")//Hạng mục thu phí sửa
                            {

                                var updatemzxm = FirstOrDefault<OutpatientItemEntity>(@"select * from mz_xm where xmnm=@xmnm AND OrganizeId=@orgId", new[] { new SqlParameter("@xmnm", oldjsmx.mxnm), new SqlParameter("@orgId", orgId) });
                                var oldmzxm = updatemzxm.Clone();
                                if (updatemzxm != null)
                                {
                                    updatemzxm.sfxm = item.sfxmCode;
                                    updatemzxm.ys = ys;
                                    updatemzxm.ysmc = ysmc;
                                    updatemzxm.ks = ks;
                                    updatemzxm.ksmc = ksmc;
                                    updatemzxm.dl = item.sfdlCode;
                                    updatemzxm.dj = item.dj;
                                    updatemzxm.sl = item.sl;
                                    updatemzxm.je = item.dj * item.sl;
                                    updatemzxm.ssry = ys;
                                    updatemzxm.ssbz = "1";
                                    updatemzxm.ssrq = item.jzsj;
                                    updatemzxm.ttbz = item.ttbz == 1 ? true : false;
                                    updatemzxm.duration = item.duration;
                                    updatemzxm.kflb = item.kflb;
                                    updatemzxm.bz = item.bz;
                                    updatemzxm.zfbl = item.zfbl;
                                    updatemzxm.zfxz = item.zfxz;
                                    updatemzxm.dw = item.dw;
                                    updatemzxm.jmbl = null;
                                    updatemzxm.jmje = null;
                                    updatemzxm.Modify();
                                    if (!updateVo.mz_xmList.ContainsKey(oldmzxm))
                                    {
                                        updateVo.mz_xmList.Add(oldmzxm, updatemzxm);
                                    }
                                    //if (!string.IsNullOrWhiteSpace(updatemzxm.jzjhmxId))
                                    //{
                                    // //Kế hoạch ghi sổ sửa
                                    //    var updatejzjhmx = FirstOrDefault<OutpatientAccountDetailEntity>(@"select * from mz_jzjhmx where jzjhmxId=@jzjhmxId AND OrganizeId=@orgId", new[] { new SqlParameter("@jzjhmxId", updatemzxm.jzjhmxId), new SqlParameter("@orgId", orgId) });
                                    //    var oldjzjhmx = updatejzjhmx.Clone();
                                    //    updatejzjhmx.sfxmCode = item.sfxmCode;
                                    //    updatejzjhmx.ys = ys;
                                    //    updatejzjhmx.ysmc = ysmc;
                                    //    updatejzjhmx.sl = item.sl;
                                    //    updatejzjhmx.jzsj = item.jzsj;
                                    //    updatejzjhmx.ttbz = item.ttbz == 1 ? true : false;
                                    //    updatejzjhmx.zlsc = item.duration;
                                    //    updatejzjhmx.kflb = item.kflb;

                                    //    updatejzjhmx.Modify();
                                    //    if (!updateVo.mz_jzjhmx.ContainsKey(oldjzjhmx))
                                    //    {
                                    //        updateVo.mz_jzjhmx.Add(oldjzjhmx, updatejzjhmx);
                                    //    }
                                    updatejsmx.sl = item.sl;
                                    updatejsmx.jyje = (item.sl * item.dj);
                                    updatejsmx.mxnm = updatemzxm.xmnm;
                                    //updatejsmx.Modify();

                                    //}
                                }

                            }
                            updatejsmx.Modify();
                            if (!updateVo.mz_jsmxList.ContainsKey(updatejsmx))
                            {
                                updateVo.mz_jsmxList.Add(oldjsmx, updatejsmx);
                            }

                        }

                        #endregion
                    }
                }
            }
            //Đ ơn thuốc theo chuyên viên trị liệu có hay không cùng một Dòngmz_cf dữ liệu, vì vậy tổng số tiền theo chi tiết đ ơn thuốc tính
            foreach (var item in sqlVo.mz_cf)
            {
                item.zje = sqlVo.mz_cfmxList.Where(p => p.cfnm == item.cfnm).Sum(p => p.dj * p.sl);
            }
            _outPatChargeDmSer.OutPatsettDBInCharge(sqlVo, updateVo, del_jsnm, DelVo, orgId);
            // số lần còn lại của bảo hiểm 
            if (jzsjList == null || jzsjList.Count <= 0 || string.IsNullOrWhiteSpace(bacDto.patid.ToString())) return;
            foreach (var time in jzsjList)
            {
                var strSql = new StringBuilder();
                strSql.Append(@" exec sp_bxba_gxsycs @bxtype, @orgId, @patId, @date");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@bxtype","1"),
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@patId", bacDto.patid),
                    new SqlParameter("@date", time)
                };
                ExecuteSqlCommand(strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// TạoĐ ơn thuốc logic (bao gồm tạochi tiết quyết toán)
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        /// <param name="jsnm"></param>
        /// <param name="ysList"></param>
        /// <param name=""></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="cfnm"></param>
        private void Addcf(OutpatAccBasicInfoDto bacDto, OutpatAccGridInfoDto item, string orgId, int ghnm, int jsnm, OutpatientAccountDetailEntity jzjhmx, ref Dictionary<string, string> strArray, ref int cfnm, string _cfh, bool cFisExit, out OutpatientPrescriptionEntity mzcf, out OutpatientSettlementDetailEntity mzjsmx, out OutpatientPrescriptionDetailEntity mzcfmx)
        {
            var ysArray = item.ysList.OrderBy(p => p.gh).ToList();
            string ys = null, ysmc = null, ks = null, ksmc = null;//Khi trư ớc Đ ơn thuốc của bác sĩ
            getYsKsInfo(ysArray, ref ys, ref ysmc, ref ks, ref ksmc);
            if (cFisExit)
            {
                //Thêm thông tin bảng chính đ ơn thuốc
                strArray.Add(_cfh, ys);

                #region 添加处方
                mzcf =
                   new OutpatientPrescriptionEntity
                   {
                       cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                       ghnm = ghnm,
                       patid = bacDto.patid,
                       brxz = bacDto.brxz,
                       mjzbz = "1",
                       ys = ys,
                       ysmc = ysmc,
                       ks = ks,
                       ksmc = ksmc,
                       cfzt = "1",
                       jsnm = jsnm,
                       jsrq = item.jzsj,
                       cfh = bacDto.cfh
                   };

                mzcf.zt = "1";
                mzcf.OrganizeId = orgId;
                mzcf.Create();
                cfnm = mzcf.cfnm;
                #endregion


            }
            else
            {
                mzcf = null;
                mzjsmx = null;
            }
            #region 处方明细

            mzcfmx = new OutpatientPrescriptionDetailEntity
            {
                cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx"),
                cfnm = cfnm,
                yp = item.sfxmCode,
                dl = item.sfdlCode,
                dj = item.dj,
                sl = item.sl,
                je = item.Zje,
                dw = item.dw,
                zt = "1",
                jzjhmxId = jzjhmx == null ? "" : jzjhmx.jzjhmxId,
                kflb = item.kflb,
                OrganizeId = orgId
            };
            mzcfmx.Create();
            #endregion

            #region 门诊结算明细
            mzjsmx = new OutpatientSettlementDetailEntity();
            mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
            mzjsmx.jsnm = jsnm;
            mzjsmx.mxnm = 0;
            mzjsmx.sl = item.sl;
            mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
            mzjsmx.cf_mxnm = mzcfmx.cfmxId;
            mzjsmx.zt = "1";
            mzjsmx.OrganizeId = orgId;
            mzjsmx.CreateTime = DateTime.Now;
            mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
            mzjsmx.jyje = (item.sl * item.dj);
            mzjsmx.jyfwje = (item.sl * item.dj);
            #endregion
        }
        #endregion

        #region Vision-1.4 门诊记账第四个版本 增加临时和长期，单次治疗量
        /// <summary>
        /// LưuGhi sổ ngoại trú
        /// </summary>
        public void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId, string userCode)
        {
            if (accDto == null || accDto.Count() < 1)
            {
                throw new FailedException("不存在记账数据");
            }
            var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
            if (ghIsExits == null)
            {
                throw new FailedException("门诊号不存在，请确认");
            }
            var ghnm = ghIsExits.ghnm;
            //***************************************Thêm thực thể******************************************************
            //Thêm mớighi sổ đối tư ợng tập hợp 
            var sqlVo = new OutPatSettInAccDataVo4
            {
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new List<OutpatientSettlementEntity>(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>(),
                mz_jzjh = null,
                mz_jzjhmx = new List<OutpatientAccountDetailEntity>()
            };
            var tempVo = new List<OutpatientItemExeEntity>();
            var jzsjList = new List<DateTime>();
            var gridindex = 0;
            //***************************************EndThêm thực thể***************************************************
            sqlVo.mz_jzjh = new OutpatientAccountEntity();
            sqlVo.mz_jzjh.OrganizeId = orgId;
            sqlVo.mz_jzjh.patid = bacDto.patid.ToString();
            sqlVo.mz_jzjh.ghnm = ghnm;
            sqlVo.mz_jzjh.Create(true, null);
            foreach (var item in accDto)
            {

                var ys = "";
                var ysmc = "";
                var ks = "";
                var ksmc = "";
                ys = ghIsExits.ys;
                ks = ghIsExits.ks;

                //Sửa: trư ớc 停 dùng,再thêm mới
                if (!string.IsNullOrWhiteSpace(item.jzjhmxId) && (item.czlx == "1"))
                {
                    //停 dùng kế hoạch 
                    overAccountingPlan(item.jzjhmxId, orgId, userCode);
                }

                var jzjhmxId = "";
                //Thêm mới kế hoạch 
                var addvo = AddoutpatientAccountInfo(item, orgId, jzsjList, bacDto, ghnm, gridindex, sqlVo.mz_jzjh, ys, ysmc, ks, ksmc, out jzjhmxId);
                sqlVo.mz_jzjhmx.AddRange(addvo.mz_jzjhmx);
                sqlVo.mz_js.AddRange(addvo.mz_js);
                sqlVo.mz_jsmxList.AddRange(addvo.mz_jsmxList);
                sqlVo.mz_xmList.AddRange(addvo.mz_xmList);
                continue;

                #region 注释
                ////Thêm mới:Chi tiết quyết toán mã nội bộ không tồn tại tại;
                //if (string.IsNullOrWhiteSpace(item.jsmxnm.ToString()))
                //{
                // //Thêm mới kế hoạch 
                //    var addvo = AddoutpatientAccountInfo(item, orgId, jzsjList, bacDto, ghnm, gridindex, sqlVo.mz_jzjh);
                //    sqlVo.mz_jzjhmx.AddRange(addvo.mz_jzjhmx);
                //    sqlVo.mz_js.AddRange(addvo.mz_js);
                //    sqlVo.mz_jsmxList.AddRange(addvo.mz_jsmxList);
                //    sqlVo.mz_xmList.AddRange(addvo.mz_xmList);
                //}
                //else
                //{
                //    updatejsmx = _outpatientSettlementDetailRepo.FindEntity(int.Parse(item.jsmxnm.ToString()));
                // if (item.czlx == "1")//Sửa, trư ớc 停 dùng khi trư ớc kế hoạch,再开启 mới kế hoạch 
                //    {
                //        var updatemzxm = FirstOrDefault<OutpatientItemEntity>(@"select * from mz_xm where xmnm=@xmnm AND OrganizeId=@orgId", new[] { new SqlParameter("@xmnm", updatejsmx.mxnm), new SqlParameter("@orgId", orgId) });
                // //停 dùng kế hoạch 
                //        overAccountingPlan(updatemzxm.jzjhmxId, orgId, userCode);
                // //Thêm mới kế hoạch 
                //        var addvo = AddoutpatientAccountInfo(item, orgId, jzsjList, bacDto, ghnm, gridindex, sqlVo.mz_jzjh);
                //        sqlVo.mz_jzjhmx.AddRange(addvo.mz_jzjhmx);
                //        sqlVo.mz_js.AddRange(addvo.mz_js);
                //        sqlVo.mz_jsmxList.AddRange(addvo.mz_jsmxList);
                //        sqlVo.mz_xmList.AddRange(addvo.mz_xmList);

                //        continue;
                //    }
                //}
                #endregion

            }

            _outPatChargeDmSer.OutPatsettDBInCharge(sqlVo, tempVo, orgId);
            // số lần còn lại của bảo hiểm 
            if (jzsjList == null || jzsjList.Count <= 0 || string.IsNullOrWhiteSpace(bacDto.patid.ToString())) return;
            foreach (var time in jzsjList)
            {
                var strSql = new StringBuilder();
                strSql.Append(@" exec sp_bxba_gxsycs @bxtype, @orgId, @patId, @date");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@bxtype","1"),
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@patId", bacDto.patid),
                    new SqlParameter("@date", time)
                };
                ExecuteSqlCommand(strSql.ToString(), parameters);
            }
        }


        /// <summary>
        /// số lần còn lại của bảo hiểm 
        /// </summary>
        /// <param name="jzsjList"></param>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        public void Execsycs(List<DateTime> jzsjList, string orgId, string patid)
        {

            // số lần còn lại của bảo hiểm 
            if (jzsjList == null || jzsjList.Count <= 0 || string.IsNullOrWhiteSpace(patid)) return;
            foreach (var time in jzsjList)
            {
                var strSql = new StringBuilder();
                strSql.Append(@" exec sp_bxba_gxsycs @bxtype, @orgId, @patId, @date");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@bxtype","1"),
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@patId", patid),
                    new SqlParameter("@date", time)
                };
                ExecuteSqlCommand(strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// Thêm mớiKế hoạch ghi sổ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orgId"></param>
        /// <param name="jzsjList"></param>
        /// <param name="bacDto"></param>
        /// <param name="ghnm"></param>
        /// <param name="gridindex"></param>
        /// <param name="mz_jzjh"></param>
        /// <returns></returns>
        private OutPatSettInAccDataVo4 AddoutpatientAccountInfo(OutpatAccGridInfoDto item, string orgId, List<DateTime> jzsjList, OutpatAccBasicInfoDto bacDto, int ghnm, int gridindex, OutpatientAccountEntity mz_jzjh, string ys, string ysmc, string ks, string ksmc, out string jzjhmxId)
        {
            jzjhmxId = "";
            var sqlVo = new OutPatSettInAccDataVo4
            {
                mz_xmList = new List<OutpatientItemEntity>(),
                mz_js = new List<OutpatientSettlementEntity>(),
                mz_jsmxList = new List<OutpatientSettlementDetailEntity>(),
                mz_jzjh = null,
                mz_jzjhmx = new List<OutpatientAccountDetailEntity>()

            };

            var zje = item.Zje;
            int zsl = 0;//总数
            var dcsl = 0;// mỗi lần số lư ợng= điều trị lư ợng /Đ ơn vị计 lư ợng 数
            int? zzll = item.sl * item.zll;
            var zxzt = (int)EnumJzjhZXZT.None;
            if (item.sfxmCode != null)
            {
                dcsl = CommmHelper.CalcSfxmSl(item.zll, item.dwjls, item.jjcl);
                //总数
                zsl = (dcsl * item.sl).ToInt();
                zje = zsl * item.dj;
            }
            #region 新增
            if (item.jzsj != null && !jzsjList.Contains(item.jzsj)) jzsjList.Add(item.jzsj);

            #region 记账计划明细
            var mxEntity = new OutpatientAccountDetailEntity
            {
                jzjhId = mz_jzjh.jzjhId,
                zlsc = item.duration,
                sfxmCode = item.sfxmCode,
                sl = dcsl,
                jzsj = item.jzsj,
                bz = item.bz,
                ys = ys,
                ks = ks,
                ysmc = ysmc,
                ksmc = ksmc,
                kflb = item.kflb,
                OrganizeId = orgId,
                yzxz = item.yzxz,
                zll = item.zll,
                zxzt = zxzt,
                StartDate = Constants.MinDate,
                EndDate = Constants.MaxDate,
                zcs = item.sl
            };
            mxEntity.Create(true);

            sqlVo.mz_jzjhmx.Add(mxEntity);
            #endregion

            #region 如果记账时间是同一天，作为一笔记账数据,门诊结算，结算大类，门诊项目重新处理
            if (sqlVo.mz_js.Any(p => p.jzsj == item.jzsj))
            {
                #region 门诊结算
                var oldDto = sqlVo.mz_js.FirstOrDefault(p => p.jzsj == item.jzsj);
                if (oldDto != null)
                {
                    oldDto.zlfy += zje;
                    oldDto.jzfy = oldDto.zlfy;
                    oldDto.xjzf = oldDto.zlfy;
                    oldDto.xjwc = 0;
                    oldDto.jzsj = item.jzsj;
                    oldDto.zje = oldDto.zlfy;
                }

                #endregion

                if (oldDto != null)
                {
                    #region 门诊项目
                    var xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                    var mzxm2 = new OutpatientItemEntity
                    {
                        xmnm = xmnm,
                        ghnm = ghnm,
                        patid = bacDto.patid,
                        brxz = bacDto.brxz,
                        mjzbz = "1",
                        ys = ys,
                        ks = ks,
                        ysmc = ysmc,
                        ksmc = ksmc,
                        sfxm = item.sfxmCode,
                        dl = item.sfdlCode,
                        dj = item.dj,
                        sl = zsl,
                        je = zje,
                        xmzt = "1",
                        xmly = "0",
                        ssbz = "0",
                        ssrq = item.jzsj,
                        jsnm = oldDto.jsnm,
                        jsrq = DateTime.Now,
                        zt = "1",
                        OrganizeId = orgId,
                        kflb = item.kflb,
                        jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                        ttbz = item.ttbz == 1 ? true : false,
                        duration = item.duration,
                        zzll = zzll
                    };
                    mzxm2.Create();

                    sqlVo.mz_xmList.Add(mzxm2);
                    #endregion

                    #region 门诊结算明细
                    var mzjsmx = new OutpatientSettlementDetailEntity();
                    mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                    mzjsmx.jsnm = oldDto.jsnm;
                    mzjsmx.mxnm = mzxm2.xmnm;
                    mzjsmx.sl = zsl;
                    mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                    mzjsmx.cf_mxnm = 0;
                    mzjsmx.zt = "1";
                    mzjsmx.jyje = zsl * item.dj;// giao dịch số tiền
                    mzjsmx.OrganizeId = orgId;
                    mzjsmx.CreateTime = DateTime.Now;
                    mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    sqlVo.mz_jsmxList.Add(mzjsmx);
                    #endregion
                }
                return sqlVo;
                #endregion
            }
            else
            {
                #region 门诊结算
                var jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js");
                var mzjs = new OutpatientSettlementEntity
                {
                    jsnm = jsnm,
                    patid = bacDto.patid,
                    ghnm = ghnm,
                    brxz = bacDto.brxz,
                    jslx = "2",
                    zlfy = zje,
                    zffy = 0,
                    flzffy = 0,
                    jzfy = zje,
                    jmje = 0,
                    jszt = (int)Constants.jsztEnum.YJ,
                    cxjsnm = 0,
                    zh = 0,
                    fpdm = "0",
                    jmbl = 0,
                    jch = 0,
                    zt = "1",
                     xjzffs = "0",
                    zl = 0,
                    jzsj = item.jzsj,
                    OrganizeId = orgId,
                    CreateTime = DateTime.Now,
                    CreatorCode = OperatorProvider.GetCurrent().UserCode,
                    xm = bacDto.xm,
                    xb = bacDto.xb,
                    blh = bacDto.blh,
                    csny = bacDto.csny,
                    zjh = bacDto.zjh,
                    fph = "0",
                    zjlx = bacDto.zjlx
                };

                mzjs.jylx = "";
                mzjs.zffy = 0;
                mzjs.xjzf = zje;
                mzjs.xjwc = 0;
                mzjs.zje = mzjs.zlfy;
                sqlVo.mz_js.Add(mzjs);
                #endregion

                #region 门诊项目
                var mzxm = new OutpatientItemEntity
                {
                    xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm"),
                    ghnm = ghnm,
                    patid = bacDto.patid,
                    brxz = bacDto.brxz,
                    mjzbz = "1",
                    ys = ys,
                    ks = ks,
                    ysmc = ysmc,
                    ksmc = ksmc,
                    sfxm = item.sfxmCode,
                    dl = item.sfdlCode,
                    dj = item.dj,
                    sl = zsl,
                    je = zje,
                    xmzt = "1",
                    xmly = "0",
                    ssbz = "0",
                    ssrq = item.jzsj,
                    jsnm = jsnm,
                    jsrq = DateTime.Now,
                    zt = "1",
                    OrganizeId = orgId,
                    kflb = item.kflb,
                    jzjhmxId = mxEntity == null ? "" : mxEntity.jzjhmxId,
                    ttbz = item.ttbz == 1 ? true : false,
                    duration = item.duration,
                    zzll = zzll
                };
                mzxm.Create();

                sqlVo.mz_xmList.Add(mzxm);

                #endregion

                #region 门诊结算明细
                var mzjsmx = new OutpatientSettlementDetailEntity();
                mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                mzjsmx.jsnm = jsnm;
                mzjsmx.mxnm = mzxm.xmnm;
                mzjsmx.sl = zsl;
                mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                mzjsmx.cf_mxnm = 0;
                mzjsmx.zt = "1";
                mzjsmx.OrganizeId = orgId;
                mzjsmx.jyje = zsl * item.dj;// giao dịch số tiền
                mzjsmx.CreateTime = DateTime.Now;
                mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                sqlVo.mz_jsmxList.Add(mzjsmx);
                #endregion
                gridindex++;
            }
            #endregion
            return sqlVo;
        }

        /// <summary>
        /// Ngoại trú dừng kế hoạch 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        public void overAccountingPlan(string jzjhmxId, string orgId, string UserCode)
        {
            //int? jsnm = null;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                string yzxcssql = "SELECT count(*) FROM [dbo].[mz_jzjhmxzx] WHERE [jzjhmxId] = @jzjhmxId and [OrganizeId] = @orgId AND zt=1 ";
                var yzxcs = db.FirstOrDefault<int>(yzxcssql, new[] { new SqlParameter("@jzjhmxId", jzjhmxId)
                                , new SqlParameter("@orgId", orgId) });
                //if (yzxcs == 0)//Chưa thực hiện qua thì nơi về联 bảng trạng thái置là0
                //{
                //    delAccountingPlan(db, jzjhmxId, orgId, "1");
                //    return;
                //}
                //1.Cập nhậtmz_xm
                // theo jzjhmzid, thống kê thực hiện bảng số lư ợng và总 lư ợng 
                //var mxzx = db.FirstOrDefault<mxzxDto>(@"SELECT  ISNULL(SUM(zll), 0) zll ,
                //                        ISNULL(CAST(SUM(sl) AS INT), 0) sl
                //                        FROM [dbo].[mz_jzjhmxzx]
                //                        WHERE  [jzjhmxId] = @jzjhmxId
                //                        AND [OrganizeId] = @orgId AND zt = '1'", new[] { new SqlParameter("@jzjhmxId",jzjhmxId)
                //                , new SqlParameter("@orgId", orgId) });
                var xmitem = db.IQueryable<OutpatientItemEntity>(p => p.jzjhmxId == jzjhmxId && p.zt == "1" && p.OrganizeId == orgId).FirstOrDefault();
                if (xmitem != null)
                {
                    xmitem.ssbz = "1";
                    //xmitem.zzll = mxzx.zll;
                    //xmitem.je = mxzx.sl * xmitem.dj;
                    xmitem.Modify();
                    db.Update(xmitem, new List<string> { "ssbz" });
                }

                //2.Chi tiết quyết toán
                //var jsmxitem = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.mxnm == xmitem.xmnm && p.OrganizeId == orgId).FirstOrDefault();
                //jsmxitem.sl = mxzx.sl;
                //jsnm = jsmxitem.jsnm;
                //db.Update(jsmxitem, new List<string> { "sl", "jsnm" });
                //3.Cập nhậtmz_jzjhmx
                var jzjhmx = db.IQueryable<OutpatientAccountDetailEntity>(p => p.jzjhmxId == jzjhmxId && p.OrganizeId == orgId).FirstOrDefault();
                if (jzjhmx != null)
                {
                    jzjhmx.yzxcs = yzxcs;
                    //if (jzjhmx.zxzt == (int)EnumJzjhZXZT.None && yzxcs == 0)
                    //{
                    // // từ chưa thực hiện qua del
                    //    db.Delete(jzjhmx);
                    //}
                    //else
                    if (jzjhmx.zcs == yzxcs)
                    {
                        jzjhmx.zxzt = (int)EnumJzjhZXZT.Finished;
                        jzjhmx.Modify();
                        db.Update(jzjhmx, new List<string> { "yzxcs", "zxzt" });
                    }
                    else
                    {
                        jzjhmx.zxzt = (int)EnumJzjhZXZT.Stopped;
                        jzjhmx.Modify();
                        db.Update(jzjhmx, new List<string> { "yzxcs", "zxzt" });
                    }
                }

                db.Commit();
            }

            //using (var db2 = new EFDbTransaction(_databaseFactory).BeginTrans())
            //{
            //    #region Sửa tổng số tiền bảng chính quyết toán
            //    if (jsnm != null)
            //    {
            // // phán đoán quyết toán có hay không tồn tại tại khác của chưa xóa
            //        var jsExists = db2.FirstOrDefault<int>(@"SELECT COUNT(1) FROM dbo.mz_jsmx WHERE jsnm=@jsnm AND zt=1 AND OrganizeId=@orgId",
            //            new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
            //        if (jsExists > 0)
            //        {

            // //Cập nhật quyết toán bảng chính tổng số tiền
            //            var xmzj = FirstOrDefault<decimal>(@"SELECT ISNULL(SUM(dj * sl), 0) FROM mz_xm WHERE jsnm = @jsnm
            //                AND OrganizeId =@orgId AND zt = 1", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
            //            var cfzj = FirstOrDefault<decimal>(@"SELECT  ISNULL(SUM(cfmx.dj * cfmx.sl), 0)
            //                                    FROM    mz_cf cf
            //                                            LEFT JOIN dbo.mz_cfmx cfmx ON cfmx.cfnm = cf.cfnm
            //                                                                          AND cfmx.OrganizeId = cf.OrganizeId
            //                                                                          AND cf.OrganizeId =@orgId
            //                                                                          AND cf.zt = 1
            //                                                                          AND cfmx.zt = 1
            //                                                                          AND jsnm = @jsnm", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
            //            db2.ExecuteSqlCommand(@"UPDATE  dbo.mz_js
            //                                  SET     zje = @zj,zlfy=@zj,jzfy=@zj,xjzf=@zj,LastModifierCode=@usercode,LastModifyTime=GETDATE()
            //                                  WHERE   jsnm = @jsnm
            //                                          AND OrganizeId = @orgId
            //                                          AND zt = 1", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@zj", xmzj + cfzj), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", UserCode) });
            //        }
            //    }
            //    #endregion
            //    db2.Commit();
            //}

        }


        /// <summary>
        /// Ngoại trú dừng kế hoạch ghi sổ(mz_jzjhmx)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        public void overjzjhmx(string jzjhmxId, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //Cập nhậtmz_jzjhmx
                var jzjhmx = db.IQueryable<OutpatientAccountDetailEntity>(p => p.jzjhmxId == jzjhmxId && p.OrganizeId == orgId).FirstOrDefault();
                jzjhmx.zxzt = (int)EnumJzjhZXZT.Stopped;
                jzjhmx.Modify();
                db.Update(jzjhmx, new List<string> { "zxzt" });
                db.Commit();
            }
        }

        /// <summary>
        /// Chưa kế hoạch thực hiện giờ,sửa kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxId"></param>
        /// <param name="orgId"></param>
        /// <param name="type">1 biểu thị ngoại trú 2 biểu thị nhập viện</param>
        public void delAccountingPlan(Infrastructure.EF.EFDbTransaction db, string jzjhmxId, string orgId, string type)
        {

            if (!string.IsNullOrWhiteSpace(type))
            {
                switch (type)
                {
                    case "1"://Ngoại trú
                        var jzjhmx = db.IQueryable<OutpatientAccountDetailEntity>().FirstOrDefault(p => p.jzjhmxId == jzjhmxId && p.zt == "1" && p.OrganizeId == orgId);
                        //jzjhmx.zt = "0";//jzjhmxKhông hiệu lực
                        jzjhmx.zxzt = (int)EnumJzjhZXZT.Stopped;
                        jzjhmx.Modify();
                        db.Update(jzjhmx, new List<string> { "zt", "zxzt" });
                        var xmitem = db.IQueryable<OutpatientItemEntity>(p => p.jzjhmxId == jzjhmxId && p.zt == "1" && p.OrganizeId == orgId).FirstOrDefault();
                        xmitem.zt = "0";//mz_xmKhông hiệu lực
                        xmitem.Modify();
                        db.Update(xmitem, new List<string> { "zt" });
                        var jsmx = db.IQueryable<OutpatientSettlementDetailEntity>().FirstOrDefault(p => p.mxnm == xmitem.xmnm && p.zt == "1" && p.OrganizeId == orgId);
                        jsmx.zt = "0";//jsmxKhông hiệu lực
                        jsmx.Modify();
                        db.Update(jsmx, new List<string> { "zt" });
                        //var js = db.IQueryable<OutpatientSettlementEntity>().FirstOrDefault(p => p.jsnm == jsmx.jsnm && p.zt == "1" && p.OrganizeId == orgId);
                        //var xmsum = db.IQueryable<OutpatientItemEntity>().Sum(p => p.dj * p.sl);
                        //js.zje = xmsum;//Cập nhật quyết toán bảng 
                        //js.Modify();
                        //db.Update(js, new List<string> { "zje" });
                        break;
                    case "2":
                        var zyjfb = db.IQueryable<HospItemBillingEntity>().Where(p => p.jzjhmxId == jzjhmxId && p.zt == "1" && p.OrganizeId == orgId);
                        foreach (var item in zyjfb)
                        {
                            item.zt = "0";//Không hiệu lực
                            item.Modify();
                            db.Update(item, new List<string> { "zt" });
                        };
                        var zyjzjhmx = db.IQueryable<HospAccountingPlanDetailEntity>().FirstOrDefault(p => p.jzjhmxId == jzjhmxId && p.zt == "1" && p.OrganizeId == orgId);
                        zyjzjhmx.zt = "0";//Không hiệu lực
                        zyjzjhmx.zxzt = (int)EnumJzjhZXZT.Stopped;
                        zyjzjhmx.Modify();
                        db.Update(zyjzjhmx, new List<string> { "zt", "zxzt" });
                        break;
                }
                db.Commit();

            }
        }

        /// <summary>
        /// Kế hoạch ghi sổ tra cứu
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<AccountingPlanVO> SelectAccountingPlanList(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt)
        {
            StringBuilder strSql = new StringBuilder();
            var pars = new List<SqlParameter>();
            strSql.Append(@"
SELECT   jzjhmx.jzjhmxId,
         jzjhmx.zxzt,
         mzgh.xm,
		 mzgh.blh,
		 mzgh.mzh mzzyh,
         sfxm.sfxmmc,
         sfxm.dw,
		 jzjhmx.zll,
		 jzjhmx.zcs,
            jzjhmx.sl,
		 zhzxsjItem.yzxcs,
        (isnull(jzjhmx.zcs,0) - isnull(zhzxsjItem.yzxcs,0)) sycs,
		 --jzjhmx.LastEexcutionTime,
		 zhzxsjItem.zhzlrq LastEexcutionTime,
         jzjhmx.startDate,
         jzjhmx.endDate,
         jzjhmx.bz,
         jzjhmx.CreateTime,
		 zhzxsjItem.zhxtzxsj,
         jzjhmx.jzsj sfrq
FROM mz_jzjhmx(nolock) jzjhmx
left join mz_jzjh(nolock) jzjh
on jzjh.jzjhId = jzjhmx.jzjhId and jzjh.zt = '1'
LEFT JOIN mz_gh(nolock) mzgh
ON mzgh.ghnm = jzjh.ghnm AND mzgh.OrganizeId = @orgId and mzgh.zt = '1'
  INNER JOIN dbo.xt_brjbxx xx ON xx.blh = mzgh.blh
                                       AND xx.OrganizeId = @orgId
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm(nolock) sfxm
ON sfxm.sfxmCode=jzjhmx.sfxmCode AND sfxm.OrganizeId=@orgId
left join (
	select jzjhmxId, max(CreateTime) zhxtzxsj, max(zxsj) zhzlrq, count(1) yzxcs
	from mz_jzjhmxzx(nolock)
	where zt = '1' and jzjhmxId is not null and OrganizeId = @orgId
	group by jzjhmxId
) as zhzxsjItem
on zhzxsjItem.jzjhmxId = jzjhmx.jzjhmxId
WHERE jzjhmx.OrganizeId=@orgId and jzjhmx.zt = '1' and xx.zt = '1'
                         ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND (mzgh.blh like @keyword or mzgh.xm like @keyword or mzgh.mzh like @keyword or xx.py LIKE @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (kssj.HasValue)
            {
                strSql.Append(" AND jzjhmx.CreateTime>=@kssj ");
                pars.Add(new SqlParameter("@kssj", kssj.Value));
            }
            if (jssj.HasValue)
            {
                strSql.Append(" AND jzjhmx.CreateTime < @jssj ");
                pars.Add(new SqlParameter("@jssj", jssj.Value.AddDays(1).Date));
            }
            if (zxzt.HasValue)
            {
                strSql.Append(" AND jzjhmx.zxzt = @jhzxzt ");
                pars.Add(new SqlParameter("@jhzxzt", zxzt.Value));
            }
            if ((zsftx ?? 0) > 0)//再thu phí nhắc nhở 
            {
                strSql.Append(" AND datediff(day, jzjhmx.jzsj, jzjhmx.LastEexcutionTime) >= @zsftx");
                pars.Add(new SqlParameter("@zsftx", zsftx.Value));
                // chỉ chưa thực hiện hoặc thực hiện trong của
                strSql.AppendFormat(" AND jzjhmx.zxzt in ({0})"
                    , (int)EnumJzjhZXZT.None + "," + (int)EnumJzjhZXZT.Part);
            }
            if ((sycstx ?? 0) > 0)// còn lại số lần nhắc nhở 
            {
                strSql.Append("  AND (ISNULL(jzjhmx.zcs, 0) - ISNULL(zhzxsjItem.yzxcs, 0)) <= @sycstx");
                pars.Add(new SqlParameter("@sycstx", sycstx.Value));
                // chỉ chưa thực hiện hoặc thực hiện trong của
                strSql.AppendFormat(" AND jzjhmx.zxzt in ({0})"
                    , (int)EnumJzjhZXZT.None + "," + (int)EnumJzjhZXZT.Part);
            }
            if (!string.IsNullOrWhiteSpace(sfzt))
            {
                if (sfzt == "1")
                {
                    //Đ ãthu phí
                    strSql.Append("  and jzjhmx.jzsj is not null");
                }
                else if (sfzt == "2")
                {
                    //Chưa thu phí
                    strSql.Append("  and jzjhmx.jzsj is null");
                }
            }
            pars.Add(new SqlParameter("@orgId", orgId));
            var list = this.QueryWithPage<AccountingPlanVO>(strSql.ToString(), pagination, pars.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// Lưu kế hoạch ghi sổ và chi tiết kế hoạch ghi sổ đ ến dữ liệu kho 
        /// </summary>
        /// <param name="jzjh"></param>
        /// <param name="jzjhmxList"></param>
        /// <param name="mz_xm"></param>
        public void OutpatientAccountDBInCharge(OutpatientAccountEntity jzjh, List<OutpatientAccountDetailEntity> jzjhmxList, List<OutpatientItemEntity> mz_xmList)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //Kế hoạch ghi sổ
                if (jzjh != null)
                {
                    db.Insert(jzjh);
                }

                //Chi tiết kế hoạch ghi sổ
                if (jzjhmxList != null && jzjhmxList.Count() > 0)
                {
                    foreach (var item in jzjhmxList)
                    {
                        db.Insert(item);
                    }
                }
                //Hạng mục ngoại trú
                if (mz_xmList != null && mz_xmList.Count > 0)
                {
                    foreach (var mz_xm in mz_xmList)
                    {
                        if (mz_xm != null && !string.IsNullOrWhiteSpace(mz_xm.jzjhmxId))
                        {
                            mz_xm.Modify();
                            db.Update(mz_xm);
                        }
                    }
                }
                db.Commit();
            }
        }
        #endregion

        #region 根据门诊号查询关联处方（但不包括zt=0处方）

        /// <summary>
        /// theo số khám ngoại trú tra cứu liên kết thu ốcĐ ơn thuốc(chưa phát thuốc hoàn phí)(nhưng không bao gồmzt=0Đ ơn thuốc)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="refundDict"></param>
        /// <returns></returns>
        public IList<OutpatientMedicineRefundVO> GetRelatedYpDetailListByMzh(string mzh, string orgId, Dictionary<int, decimal> refundDict)
        {
            if (refundDict == null || refundDict.Count == 0)
            {
                return null;
            }
            var sql = string.Format(@"select jsmx.jsmxnm, mzcf.cfh, ypcfmx.yp ypdm, mzcf.lyyf Yfbm from mz_jsmx jsmx
INNER JOIN mz_js js
on js.jsnm = jsmx.jsnm and js.OrganizeId = jsmx.OrganizeId
inner join mz_gh gh
on gh.ghnm = js.ghnm and gh.OrganizeId = js.OrganizeId
inner join mz_cfmx ypcfmx
on ypcfmx.cfmxId = jsmx.cf_mxnm
inner join mz_cf mzcf
on mzcf.cfnm = ypcfmx.cfnm and mzcf.OrganizeId = js.OrganizeId
where 1 = 1
and jsmx.jsmxnm in ({0})
and gh.mzh = @mzh
and jsmx.OrganizeId = @orgId
--药品处方
and mzcf.cflx = 1
--尚未发药
and isnull(mzcf.fybz,'') <> '2'", string.Join(",", refundDict.Select(p => p.Key).ToArray()));
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@mzh", mzh));
            pars.Add(new SqlParameter("@orgId", orgId));
            return this.FindList<OutpatientMedicineRefundVO>(sql, pars.ToArray());
        }

		#endregion

		#region 医保业务
		/// <summary>
		/// Lấy số đ ơn thuốc ghi đ ỏ hoàn trả dữ liệu,tra c ứuĐ ãtải lên và chưa quyết toán của số đ ơn thuốc, tiến hành ghi đ ỏ hoàn trả xử lý
		/// </summary>
		/// <param name="zymzh"></param>
		/// <param name="orgId"></param>
		/// <param name="type">1Ngoại trú,2Nhập viện</param>
		/// <returns></returns>
		public List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh,string orgId,string type)
		{
			var sql = new StringBuilder();
			if (type=="1")
			{
                sql.Append(@"   SELECT DISTINCT
                        '1' xzlb ,
                        cfh ,
                        zymzh
                FROM    [NewtouchHIS_Sett].[dbo].[cqyb_OutPutInPar04]
                WHERE   zt = '1'
                        AND zymzh = @zymzh
						AND cfh != @zymzh
                        AND OrganizeId = @orgId
                        AND cfh NOT IN (
                        SELECT  a.cfh
                        FROM    [NewtouchHIS_Sett].[dbo].[mz_cf] a
                                LEFT JOIN NewtouchHIS_Sett..mz_gh b ON b.ghnm = a.ghnm
                                                              AND b.OrganizeId = a.OrganizeId
                        WHERE   a.cfzt = '1'
                                AND b.mzh = @zymzh
                                AND b.OrganizeId = @orgId
                                AND b.zt = '1' ); ");
			}
			else
			{
				sql.Append(@"     SELECT DISTINCT
        '1' xzlb ,
        cfh ,
        zymzh
FROM    [NewtouchHIS_Sett].[dbo].[cqyb_OutPutInPar04]
WHERE   zt = '1'
        AND zymzh = @zymzh
        AND OrganizeId = @orgId
        AND isnull(spbz,0)='0'--获取未审批的，已经审批的不再回退
        AND SUBSTRING(cfh, 3, 50) NOT IN (
        SELECT  a.ypjfbbh
        FROM    [NewtouchHIS_Sett].[dbo].[zy_jsmx] a
                LEFT JOIN NewtouchHIS_Sett..zy_js b ON b.OrganizeId = a.OrganizeId
                                                    AND b.jsnm = a.jsnm
                                                    AND b.zt = '1'
                                                    AND b.jszt = '1'
                                                    AND b.jsnm NOT IN (
                                                    SELECT
                                                    cxjsnm
                                                    FROM
                                                    NewtouchHIS_Sett..zy_js
                                                    WHERE
                                                    jszt = '2'
                                                    AND OrganizeId = @orgId
                                                    AND zyh = @zymzh )
        WHERE   b.zyh = @zymzh
                AND b.OrganizeId = @orgId
                AND a.yzlx = '1'
        UNION
        SELECT  a.xmjfbbh
        FROM    [NewtouchHIS_Sett].[dbo].[zy_jsmx] a
                LEFT JOIN NewtouchHIS_Sett..zy_js b ON b.OrganizeId = a.OrganizeId
                                                    AND b.jsnm = a.jsnm
                                                    AND b.zt = '1'
                                                    AND b.jszt = '1'
                                                    AND b.jsnm NOT IN (
                                                    SELECT
                                                    cxjsnm
                                                    FROM
                                                    NewtouchHIS_Sett..zy_js
                                                    WHERE
                                                    jszt = '2'
                                                    AND OrganizeId = @orgId
                                                    AND zyh = @zymzh )
        
        WHERE   b.zyh = @zymzh
                AND b.OrganizeId = @orgId
                AND a.yzlx = '2' )
        UNION
                SELECT DISTINCT
                        '1' xzlb ,
                        cfh ,
                        zymzh
                FROM    [NewtouchHIS_Sett].[dbo].[cqyb_OutPutInPar04]
                WHERE   zt = '1'
                        AND zymzh = @zymzh
                        AND OrganizeId = @orgId
                        --AND spbz = '1'--获取已经审批的，但his发生费用变化了，仍需退掉重传
                        AND SUBSTRING(cfh, 3, 50) IN (
                        SELECT DISTINCT
                                a.cxzyjfbbh
                        FROM    [NewtouchHIS_Sett].[dbo].[zy_ypjfb] a
						INNER JOIN [NewtouchHIS_Sett].[dbo].[zy_ypjfb] b ON b.jfbbh=a.cxzyjfbbh AND b.sl+a.sl=0.00
                        WHERE   a.zyh = @zymzh
                                AND a.OrganizeId = @orgId
                        UNION
                        SELECT DISTINCT
                                a.cxzyjfbbh
                        FROM    [NewtouchHIS_Sett].[dbo].zy_xmjfb a
						INNER JOIN [NewtouchHIS_Sett].[dbo].[zy_xmjfb] b ON b.jfbbh=a.cxzyjfbbh AND b.sl+a.sl=0.00
                        WHERE   a.zyh = @zymzh
                                AND a.OrganizeId = @orgId )
");
			}
		    var pars = new List<SqlParameter>();
		    pars.Add(new SqlParameter("@zymzh", zymzh));
		    pars.Add(new SqlParameter("@orgId", orgId));
			return this.FindList<RefundPrescriptionsInPut>(sql.ToString(), pars.ToArray());
		}

        public List<Input_2205> GetCqybMxCxData(string zymzh, string orgId, string type, string ybver)
        {
            StringBuilder strSql = new StringBuilder();
            if (ybver == "shanghaiV5")
            {
                strSql.Append(@"
                          SELECT DISTINCT
                                    cfh,mxzdh pch
                            FROM    [NewtouchHIS_Sett].[dbo].[Ybjk_SN01_Mxxmz_Input]
                            WHERE   zt = '1'
                                    AND mzzyh = @zymzh
                                    AND cfh != @zymzh
                         ");
            }
            else {
                strSql.Append(@"  SELECT DISTINCT
                                        cfh,pch
                                  FROM  [NewtouchHIS_Sett].[dbo].[cqyb_OutPutInPar04]
                                  WHERE zt = '1'
                                        AND zymzh = @zymzh
                                        AND cfh != @zymzh
                                        AND OrganizeId = @orgId");
            }
            strSql.Append(@" AND cfh NOT IN (
                        SELECT a.cfh
                        FROM    [NewtouchHIS_Sett].[dbo].[mz_cf] a
                                LEFT JOIN NewtouchHIS_Sett..mz_gh b 
                                ON b.ghnm = a.ghnm AND b.OrganizeId = a.OrganizeId
                        WHERE
                        a.cfzt = '1'
                        AND b.mzh = @zymzh
                        AND b.OrganizeId = @orgId
                        AND b.zt = '1')");
          
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@zymzh", zymzh));
            pars.Add(new SqlParameter("@orgId", orgId));

            return this.FindList<Input_2205>(strSql.ToString(), pars.ToArray());
        }
        #endregion
    }
}

