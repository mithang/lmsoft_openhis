using NewtouchHIS.Lib.Base.Model;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.OR.ManageSystem.Domain.DTO;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtonsoft.Json.Linq;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.OR.ManageSystem.DomainServices
{
    public class OPApplyDmnService : DmnServiceBase, IOPApplyDmnService
    {
        private readonly IORApplyInfoRepo _ORApplyInfoRepo;
        private readonly ISysStaffRepo _SysStaffRepo;
        private readonly ITemporary_ordersERepo _ITemporary_ordersERepo;
        private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;



        public OPApplyDmnService(IORApplyInfoRepo orApplyInfoRepo, ISysStaffRepo sysStaffRepo, ITemporary_ordersERepo temporary_ordersERepo, IORApplyInfoExpandRepo orApplyInfoExpandRepo)
        {
            _ORApplyInfoRepo = orApplyInfoRepo;
            _SysStaffRepo = sysStaffRepo;
            _ITemporary_ordersERepo = temporary_ordersERepo;
            _ORApplyInfoExpandRepo = orApplyInfoExpandRepo;
        }
        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="type"></param>
        /// <param name="brzt"></param>
        /// <returns></returns>
        public IList<PatListVO> GetPatList(string keyword, string zyh, string bq, string ysgh, string OrgId, int type)
        {
            string sql = "";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrgId??"1"));
            para.Add(new SugarParameter("@ysgh", ysgh??"1"));

            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql = @"SELECT [Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                        ,[WardCode],[ysgh],[BedCode] ch,cwmc,[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                        ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                        ,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[zt],(case when zyh=@zyh then 1 else 0 end ) isCheck,
                        c.cwmc,d.bqmc WardName,(case when datediff(yy,a.birth,getdate())>=1 then convert(varchar(3),datediff(yy,a.birth,getdate()))+' tuổi' else convert(varchar(2),datediff(mm,a.birth,getdate()))+' tháng' end  )nl
                        FROM [Newtouch_CIS].[dbo].[zy_brxxk] a  with(nolock)
                        left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1 
                        left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
                        where a.zt=1 and a.[OrganizeId]=@OrgId and a.zyh=@zyh
                        and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		                        where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode and b.zt=1) ";

                para.Add(new SugarParameter("@zyh", zyh));
            }
            else
            {
                sql = @"SELECT [Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                        ,[WardCode],[ysgh],[BedCode] ch,cwmc,[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                        ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                        ,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[zt],0 isCheck,
                        c.cwmc,d.bqmc WardName,(case when datediff(yy,a.birth,getdate())>=1 then convert(varchar(3),datediff(yy,a.birth,getdate()))+' tuổi' else convert(varchar(2),datediff(mm,a.birth,getdate()))+' tháng' end  )nl
                        FROM [Newtouch_CIS].[dbo].[zy_brxxk] a  with(nolock)
                        left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1 
                        left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
                        where a.zt=1 and a.[OrganizeId]=@OrgId 
                        and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		                        where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode  and b.zt=1) ";

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sql += " and (zyh=@keyword or charindex(@keyword,xm)>0) ";
                    para.Add(new SugarParameter("@keyword", keyword));
                }

                if (!string.IsNullOrWhiteSpace(bq))
                {
                    sql += " and WardCode=@bq ";
                    para.Add(new SugarParameter("@bq", bq));
                }
            }

            //在院
            if (type == Convert.ToInt32(EnumZYBZ.Bqz))
            {
                sql += " and zybz in(" + Convert.ToInt32(EnumZYBZ.Bqz) + "," + Convert.ToInt32(EnumZYBZ.Zq) + ")";

            }
            //出院（已结账、待结账）
            else if (type == Convert.ToInt32(EnumZYBZ.Ycy) || type == Convert.ToInt32(EnumZYBZ.Djz))
            {
                sql += " and zybz in(" + Convert.ToInt32(EnumZYBZ.Ycy) + "," + Convert.ToInt32(EnumZYBZ.Djz) + ") ";
            }
            else if (type == 0)
            {
                if (!string.IsNullOrWhiteSpace(ysgh??"1"))
                {
                    sql += " and a.ysgh=@ysgh ";
                }

            }

            return this.FindList<PatListVO>(sql, para.ToArray()).ToList();
        }

        public PatListVO GetPatInfobyzyh(string zyh, string ysgh, string OrgId)
        {
            string sql = "";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrgId));
            para.Add(new SugarParameter("@ysgh", ysgh));
            sql = @"SELECT [Id],a.[OrganizeId],[zyh],[blh],[xm],a.[py],[wb],[sfzh],[sex],[birth],[zybz],[sfqj],[DeptCode]
                        ,[WardCode],[ysgh],[BedCode],[ryrq],[rqrq],[cqrq],[wzjb],[hljb],[ryfs],[cyfs],[gdxmzxrq],[brxzdm],[brxzmc]
                        ,[cardno],[cardtype],[lxr],[lxrgx],[lxrdh],[zddm],[zdmc],[cyzddm],[cyzdmc],[Memo],[CreateTime]
                        ,[CreatorCode],[LastModifyTime],[LastModifierCode],a.[zt],(case when zyh=@zyh then 1 else 0 end ) isCheck,
                        c.cwmc,d.bqmc WardName,(case when datediff(yy,a.birth,getdate())>=1 then convert(varchar(3),datediff(yy,a.birth,getdate()))+' tuổi' else convert(varchar(2),datediff(mm,a.birth,getdate()))+' tháng' end  )nl
                        FROM [Newtouch_CIS].[dbo].[zy_brxxk] a  with(nolock)
                        left join NewtouchHIS_Base.dbo.V_S_xt_cw c with(nolock) on a.organizeid=c.organizeid and a.bedcode=c.cwcode and c.zt=1 
                        left join NewtouchHIS_Base..V_S_xt_bq  d with(nolock) on a.organizeid=d.organizeid and a.wardcode=d.bqcode and d.zt=1
                        where a.zt=1 and a.[OrganizeId]=@OrgId and zyh=@zyh
                        and exists(select 1 from NewtouchHIS_Base..V_C_Sys_StaffWard b with(nolock) 
		                        where a.organizeid=b.organizeid and b.staffgh=@ysgh and b.bqCode=a.WardCode and b.zt=1) ";

            para.Add(new SugarParameter("@zyh", zyh));
            return this.FindList<PatListVO>(sql, para.ToArray()).FirstOrDefault();
        }

        public IList<ORApplyInfoEntity> GetOpApplybyzyh(string zyh, string sqlx, string orgId)
        {
            string sql = @"SELECT a.[OrganizeId],a.[Id],a.[Applyno],a.[zyh],[xm],[xb],[csrq],[sfz],[ks]
,[bq],[ch],[ryrq],[zd],[sqzt],b.ssmc ssmcn,b.[ssdm],[sssj],[ysgh],[ysxm]
,[AnesCode],ssbw,isgl,mzys,a.[zt],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode]
FROM [dbo].[OR_ApplyInfo] a with(nolock) 
left join [dbo].[OR_ApplyInfo_Expand] b
on a.applyno=b.applyno and b.px=1 
    where a.zt='1' and a.[OrganizeId]=@orgId ";
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and a.zyh=@zyh ";
            }

            if (!string.IsNullOrWhiteSpace(sqlx))
            {
                sql += " and a.sqzt=@sqlx ";
            }

            sql += " order by isnull(a.LastModifyTime,a.CreateTime) desc  ";

            return this.FindList<ORApplyInfoEntity>(sql, new SugarParameter[] {
                new SugarParameter("@orgId",orgId),
                new SugarParameter("@zyh",zyh==null?"":zyh),
                new SugarParameter("@sqlx",sqlx==null?"":sqlx)
            });
        }
        public OpApplySubmitDTO GetOpApplybyApplyNo(string applyNo, string orgId)
        {
            var ety = _ORApplyInfoRepo.FindEntity(p => p.Applyno == applyNo && p.OrganizeId == orgId && p.zt == "1");
            if (ety == null)
            {
                throw new FailedException("Không tìm thấy thông tin đơn phẫu thuật hợp lệ!");
            }
            var applyInfo = ety.MapperTo<ORApplyInfoEntity, OpApplySubmitDTO>();
            applyInfo.ssdm = new string[] { ety.ssdm };
            applyInfo.ssmcn = new string[] { ety.ssmcn };
            var ssEty = _ORApplyInfoExpandRepo.getApplyExtendByApplyno(orgId, applyNo);
            if (ssEty != null && ssEty.Count > 0)
            {
                applyInfo.ssdm = ssEty.Select(p => p.ssdm).ToArray();
                //applyInfo.ssmcn = ssEty.Select(p => p.ssmcn).ToArray();
            }
            if (!string.IsNullOrWhiteSpace(applyInfo.mzys) && string.IsNullOrWhiteSpace(applyInfo.mzysxm))
            {
                applyInfo.mzysxm = _SysStaffRepo.GetNameByGh(applyInfo.mzys, orgId);
            }
            return applyInfo;
        }
        /// <summary>
        /// 提交申请信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public string SubmitApplyForm(OpApplySubmitDTO dto, string keyValue)
        {
            var pat = GetPatInfobyzyh(dto.zyh, dto.staffgh, dto.OrganizeId);
            if (pat == null)
            {
                throw new FailedException("Thông tin bệnh nhân bất thường!");
            }
            if (string.IsNullOrWhiteSpace(dto.ysgh))
            {
                throw new FailedException("Bác sĩ mổ chính không được để trống!");
            }
            string ysxm = _SysStaffRepo.GetNameByGh(dto.ysgh, dto.OrganizeId);
            if (!string.IsNullOrWhiteSpace(ysxm))
            {
                dto.ysxm = ysxm;
            }
            ORApplyInfoEntity ety = new ORApplyInfoEntity();
            var db = OrDb.GetConnection();
            db.Ado.BeginTran();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    //信息变更
                    ety = _ORApplyInfoRepo.FindEntity(p => p.Applyno == keyValue && p.zt == "1" && p.OrganizeId == dto.OrganizeId);
                    if (ety != null && ety.sqzt == ((int)EnumSqzt.dsh).ToString())
                    {
                        ety.ssdm = dto.ssdm[0];
                        ety.ssmcn = dto.ssmcn[0].Replace(ety.ssdm, string.Empty).Trim();
                        ety.sssj = dto.sssj;
                        ety.ysgh = dto.ysgh;
                        ety.ysxm = dto.ysxm;
                        ety.AnesCode = dto.AnesCode;
                        ety.isgl = dto.isgl;
                        ety.ssbw = dto.ssbw;
                        ety.mzys = dto.mzys;
                        EntityHelper.Modify(ety, ety.Id);
                        db.Updateable(ety).ExecuteCommand();

                        //删除辅助手术
                        _ORApplyInfoExpandRepo.Delete(p => p.Applyno == ety.Applyno && p.OrganizeId == ety.OrganizeId && p.zt == "1");
                    }
                    else
                    {
                        throw new FailedException("Trạng thái đơn phẫu thuật đã thay đổi, không thể sửa!");
                    }
                }
                else
                {
                    ety = ApplyInfoEntityCreate(dto, pat);
                    db.Insertable(ety).ExecuteCommand();
                }
                dto.Applyno = ety.Applyno;
                #region 辅助手术
                var opList = ORApplyInfoExpandCreate(dto);
                if (opList != null && opList.Count > 0)
                {
                    db.Insertable(opList).ExecuteCommand();
                }
                #endregion
                db.Ado.CommitTran();
            }
            catch
            {
                db.Ado.RollbackTran();
                throw;
            }

            return ety.Applyno;
        }
        public string SubmitApplyForm(OpApplyDTO dto, string keyValue, string datas)
        {
            if (dto != null)
            {
                if (!string.IsNullOrWhiteSpace(dto.sq_zyh))
                {
                    var pat = GetPatInfobyzyh(dto.sq_zyh, dto.staffgh, dto.organizeid);


                    if (pat != null)
                    {
                        if (!string.IsNullOrWhiteSpace(dto.ysgh))
                        {
                            string ysxm = _SysStaffRepo.GetNameByGh(dto.ysgh, dto.organizeid);
                            if (!string.IsNullOrWhiteSpace(ysxm))
                            {
                                dto.ysxm = ysxm;
                            }
                        }

                        ORApplyInfoEntity ety = new ORApplyInfoEntity();
                        if (!string.IsNullOrWhiteSpace(keyValue))
                        {
                            ety = _ORApplyInfoRepo.FindEntity(p => p.Applyno == keyValue && p.zt == "1" && p.OrganizeId == dto.organizeid);
                            if (ety != null)
                            {
                                ety.ssmcn = dto.ssmcn;
                                ety.ssdm = dto.ssdm;
                                ety.sssj = dto.sssj;
                                ety.ysgh = dto.ysgh;
                                ety.ysxm = dto.ysxm;
                                ety.AnesCode = dto.AnesCode;
                                ety.isgl = dto.isgl;
                                ety.ssbw = dto.ssbw;
                                ety.mzys = dto.mzys;
                                _ORApplyInfoRepo.SubmitForm(ety, ety.Id);

                                return ety.Applyno;
                            }
                            else
                            {
                                throw new FailedException("Đơn phẫu thuật đã bị hủy bỏ!");
                            }
                        }
                        else
                        {
                            var newapplyinfo = NewApplyInfo(dto, pat);

                            #region 插入到CIS医嘱表中
                            //JObject json = JObject.Parse(datas);
                            //Temporary_ordersEntity toe = new Temporary_ordersEntity();
                            //toe.OrganizeId = dto.organizeid;
                            //toe.WardCode = pat.WardCode;
                            //toe.DeptCode = pat.DeptCode;
                            //toe.ysgh = dto.ysgh;
                            //toe.pcCode = "00";
                            //toe.zxcs = 1;
                            //toe.zxzq = 1;
                            //toe.zxzqdw = 1;
                            //toe.zdm = "";
                            //toe.xmdm = dto.ssdm;//手术编码
                            //toe.xmmc = dto.ssmcn;
                            //toe.yzzt = 0;//不明确
                            //toe.dw = "";
                            //toe.zbbz = 0;
                            //toe.sl = 0;//数量
                            //toe.dwlb = 0;
                            //toe.yzlx = 3;//医嘱类型
                            //toe.sssj = dto.sssj;
                            //var cjsj = DateTime.Parse(DateTime.Now.ToString());
                            //toe.kssj = cjsj;
                            //toe.zxr = dto.ysgh;//执行人等于医生工号
                            //string time = cjsj.ToString("yyyy-MM-dd");
                            //toe.yznr = dto.ssmcn + "  " + cjsj + "  " + time + "  ST";//医嘱内容=手术名称+手术时间+医生姓名+ST
                            //toe.zxksdm = "";//执行科室
                            //toe.CreateTime = cjsj;
                            //toe.CreatorCode = pat.CreatorCode;
                            //toe.zt = pat.zt;
                            //toe.hzxm = json["xm"].ToString();
                            //toe.zyh = dto.sq_zyh;
                            //toe.LastModifyTime = DateTime.Parse(DateTime.Now.ToString());
                            //toe.zh = null;
                            //toe.sqdh = newapplyinfo;

                            //_ITemporary_ordersERepo.Submitlsyz(toe, keyValue);



                            #endregion



                            return newapplyinfo;
                        }


                    }
                    else
                    {
                        throw new FailedException("Thông tin bệnh nhân bất thường!");
                    }
                }
                else
                {
                    throw new FailedException("Thông tin bệnh nhân bất thường!");
                }
            }
            else
                throw new FailedException("Thông tin quan trọng không được để trống!");


        }

        private ORApplyInfoEntity ApplyInfoEntityCreate(OpApplySubmitDTO dto, PatListVO pat)
        {
            dto.Applyno = EFDBBaseFuncHelper.GetOPApplyNo(pat.OrganizeId);
            var ety = dto.MapperTo<OpApplySubmitDTO, ORApplyInfoEntity>();
            ety.sqzt = ((int)EnumSqzt.dsh).ToString();
            #region 患者信息
            ety.zyh = pat.zyh;
            ety.xm = pat.xm;
            ety.xb = pat.sex;
            ety.csrq = pat.birth == null ? null : Convert.ToDateTime(pat.birth.ToString()).ToString("yyyy-MM-dd");
            ety.sfz = pat.sfzh;
            ety.ks = pat.DeptCode;
            ety.bq = pat.WardCode;
            ety.ch = pat.ch;
            ety.ryrq = pat.ryrq;
            ety.zd = pat.zdmc;
            #endregion
            ety.ssdm = dto.ssdm.Length == 0 ? throw new FailedException("Vui lòng chọn phẫu thuật") : dto.ssdm[0];
            ety.ssmcn = dto.ssmcn.Length == 0 ? throw new FailedException("Vui lòng chọn phẫu thuật") : dto.ssmcn[0].Replace(dto.ssdm[0], string.Empty).Trim();
            EntityHelper.Create(ety, true);
            return ety;
        }

        private List<ORApplyInfoExpandEntity> ORApplyInfoExpandCreate(OpApplySubmitDTO dto)
        {
            var exList = new List<ORApplyInfoExpandEntity>();
            if (dto.ssdm.Length > 0)
            {
                //添加辅助手术
                for (int i = 0; i < dto.ssdm.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dto.ssdm[i]))
                    {
                        var ssObj = new ORApplyInfoExpandEntity();
                        ssObj.OrganizeId = !string.IsNullOrWhiteSpace(dto.OrganizeId) ? dto.OrganizeId : throw new FailedException("Thông tin cơ sở không được để trống");
                        ssObj.Applyno = !string.IsNullOrWhiteSpace(dto.Applyno) ? dto.Applyno : throw new FailedException("Số thứ tự đơn không được để trống");
                        ssObj.zyh = !string.IsNullOrWhiteSpace(dto.zyh) ? dto.zyh : throw new FailedException("Số nhập viện không được để trống"); ;
                        ssObj.ssmc = dto.ssmcn[i].Replace(dto.ssdm[0], string.Empty).Trim();
                        ssObj.ssdm = dto.ssdm[i];
                        ssObj.px = i + 1;
                        EntityHelper.Create(ssObj, true);
                        exList.Add(ssObj);
                    }
                }
            }
            return exList;
        }

        private string NewApplyInfo(OpApplyDTO dto, PatListVO pat)
        {
            ORApplyInfoEntity ety = new ORApplyInfoEntity();
            ety.OrganizeId = dto.organizeid;
            ety.Id = new Guid().ToString();
            ety.Applyno = EFDBBaseFuncHelper.GetOPApplyNo(pat.OrganizeId);
            ety.zyh = pat.zyh;
            ety.xm = pat.xm;
            ety.xb = pat.sex;
            ety.csrq = pat.birth == null ? null : Convert.ToDateTime(pat.birth.ToString()).ToString("yyyy-MM-dd");
            ety.sfz = pat.sfzh;
            ety.ks = pat.DeptCode;
            ety.bq = pat.WardCode;
            ety.ch = pat.cwmc;
            ety.ryrq = pat.ryrq;
            ety.zd = pat.zdmc;
            ety.sqzt = ((int)EnumSqzt.dsh).ToString();
            ety.ssmcn = dto.ssmcn;
            ety.ssdm = dto.ssdm;
            ety.sssj = dto.sssj;
            ety.ysgh = dto.ysgh;
            ety.ysxm = dto.ysxm;
            ety.AnesCode = dto.AnesCode;
            ety.mzys = dto.mzys;
            ety.isgl = dto.isgl;
            ety.ssbw = dto.ssbw;

            _ORApplyInfoRepo.SubmitForm(ety, null);

            return ety.Applyno;
        }
    }
}
