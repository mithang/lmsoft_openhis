using Newtonsoft.Json;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.EMR.Domain.DTO;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.DocumentManage;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Newtouch.EMR.Repository;

namespace Newtouch.EMR.DomainServices
{
    public class BlmblbDmnService:EmrServiceBase, IBlmblbDmnService
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IBlmblbRepo _BlmblbRepo;
        private readonly IBlmbqxkzRepo _BlmbqxkzRepo;
        private readonly Ibl_bllxRepo _BllxRepo;
        private readonly IZybrjbxxRepo _zybrjbxxRepo;
        private readonly IMrWritingRulesRepo mrwritingrulesRepo;

        // PORT NOTE: the original declared _BllxRepo, _zybrjbxxRepo and
        // mrwritingrulesRepo but never assigned them in its constructor, so
        // every method using them would NullReference at runtime. The port
        // completes the constructor injection instead of preserving the bug.
        public BlmblbDmnService(ICommonDmnService CommonDmnService, IBlmblbRepo BlmblbRepo, IBlmbqxkzRepo BlmbqxkzRepo,
            Ibl_bllxRepo BllxRepo, IZybrjbxxRepo zybrjbxxRepo, IMrWritingRulesRepo mrwritingrulesRepo)
        {
            _CommonDmnService = CommonDmnService;
            _BlmblbRepo = BlmblbRepo;
            _BlmbqxkzRepo = BlmbqxkzRepo;
            _BllxRepo = BllxRepo;
            _zybrjbxxRepo = zybrjbxxRepo;
            this.mrwritingrulesRepo = mrwritingrulesRepo;
        }
        /// <summary>
        /// Danh sách mẫu bệnh án
        /// </summary>
        /// <param name="mbqx"></param>
        /// <param name="bllx"></param>
        /// <param name="ksbm"></param>
        /// <param name="user"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public IList<BlmbListVO> MedRecordTmpList(int mbqx, string bllx, string ksbm, OperatorModel user, string OrgId,string keyword=null)
        {
            IList<BlmbListVO> data = null;

            if (bllx == null)
            {
                return data;
            }

            string sql = @"select Id, OrganizeId,mbqx,mbbm,mbmc,bllxId,bllxmc,ksbm,ysgh,mblj,py,Isempty,
                    Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,bllx,LoadWay
                    from bl_mblb a with(nolock)
                    where organizeid=@OrgId and zt=1 
                    and exists(select 1
			                    from [NewtouchHIS_Base].[dbo].[V_C_Sys_StaffDuty] b with(nolock),[bl_mbqxkz] c with(nolock)
			                    where b.staffid=@staffId and
			                     b.dutyCode=c.dutyCode and b.zt=1 and c.zt=1 and c.ctrlLevel=2
			                     and a.id=c.mbId and a.organizeid=c.organizeid )";


            bl_bllxEntity mbety = _BllxRepo.FindEntity(p => p.bllx == bllx && p.OrganizeId == OrgId && p.zt == "1");
            if (mbety!=null && string.IsNullOrWhiteSpace(mbety.ParentId))
            {
                string bllxlist = "'" + bllx + "'";
                var list = _CommonDmnService.GetBllxListDetail(OrgId, mbety.Id, null,"2");
                if (list != null && list.Count > 0)
                {
                    
                    foreach (var item in list)
                    {
                        bllxlist +=",'"+ item.bllx+"'";
                    }
                }
                sql += " and bllx in(" + bllxlist + ") ";
            }
            else {
                sql += " and bllx=@bllx ";
            }
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrgId));
            para.Add(new SugarParameter("@mbqx", mbqx));
            para.Add(new SugarParameter("@staffId", user.StaffId));
            para.Add(new SugarParameter("@bllx", bllx??""));
            if (mbqx == Convert.ToInt32(Enummbqx.pub))
            {
                sql += " and mbqx=@mbqx ";
            }
            else if (mbqx == Convert.ToInt32(Enummbqx.prv))
            {
                if (!string.IsNullOrWhiteSpace(user.rygh))
                {
                    sql += " and mbqx=@mbqx and ysgh=@ysgh ";
                    para.Add(new SugarParameter("@ysgh", user.rygh));
                }
                else
                {
                    throw new FailedException("Mã nhân viên bác sĩ không được để trống");
                }
            }
            else if (mbqx == Convert.ToInt32(Enummbqx.dept))
            {
                if (!string.IsNullOrWhiteSpace(ksbm))
                {
                    sql += " and mbqx=@mbqx and ksbm=@ksbm ";
                    para.Add(new SugarParameter("@ksbm", ksbm));
                }
                else
                {
                    throw new FailedException("Khoa trực thuộc không được để trống");
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and mbmc like @keyword ";
                para.Add(new SugarParameter("@keyword", "%"+keyword+"%"));
            }
            
            return this.FindList<BlmbListVO>(sql, para.ToArray());

        }
        /// <summary>
        /// Bảo trì kiểm soát quyền vị trí mẫu
        /// </summary>
        /// <param name="list"></param>
        /// <param name="user"></param>
        public void TempCtrlAssigned(string list, string mbId, OperatorModel user)
        {
            var res = JsonConvert.DeserializeObject<IList<TemplateCtrlRequestDto>>(list);
            foreach (TemplateCtrlRequestDto item in res)
            {
                BlmbqxkzEntity ety = new BlmbqxkzEntity();
                ety.zt = item.zt;
                ety.ctrlLevel = item.ctrlLevel;
                ety.ctrlLevelDesc = item.ctrlLevel == Convert.ToInt32(EnummbqxFp.non) ? "Chưa phân bổ" : "";
                if (string.IsNullOrWhiteSpace(ety.ctrlLevelDesc))
                {
                    ety.ctrlLevelDesc = item.ctrlLevel == Convert.ToInt32(EnummbqxFp.edit) ? "Quyền đọc/ghi" : "";
                }
                if (string.IsNullOrWhiteSpace(ety.ctrlLevelDesc))
                {
                    ety.ctrlLevelDesc = item.ctrlLevel == Convert.ToInt32(EnummbqxFp.read) ? "Quyền chỉ đọc" : "";
                }
                ety.dutyCode = item.dutyCode;
                ety.dutyName = item.dutyName;
                ety.mbId = mbId;
                ety.OrganizeId = user.OrganizeId;
                _BlmbqxkzRepo.SubmitForm(ety, item.gxId);

            }
        }

        public IList<BlmbListVO> GetBlmbList(Pagination pagination, string OrganizeId, string keyword)
        {
            string sql = @"SELECT a.[Id],a.[OrganizeId],[mbqx],[mbbm],mbmc,[bllxId],(case when c.bllxmc>'' then c.bllxmc+'->'+a.[bllxmc] else a.[bllxmc] end) bllxmc,
[ksbm],[ysgh],[mblj],[py],[Isempty],IsYB,[Memo],a.[CreateTime],a.[CreatorCode],a.[LastModifyTime],a.[LastModifierCode],a.[zt],a.bllx,c.bllx bldl,c.bllxmc bldlmc,a.mzbz
FROM[dbo].[bl_mblb] a with(nolock)
left join bl_bllx b with(nolock) on a.OrganizeId=b.OrganizeId and a.bllx=b.bllx and b.zt='1' 
left join bl_bllx c with(nolock) on b.OrganizeId=c.OrganizeId and b.ParentId=c.Id and c.zt='1' 
where a.zt=1 and a.OrganizeId=@OrgId
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrganizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (charindex(@keyword,mbmc)>0  or charindex(@keyword,mbbm)>0) ";
                para.Add(new SugarParameter("@keyword", keyword));
            }
            return this.QueryWithPage<BlmbListVO>(sql, pagination, para.ToArray()).ToList();
        }

        #region Thao tác tệp bệnh án
        public BlConvertToTemplateVO BlConvertToTemplate(string OrgId, string blId, string bllx, int? mbqx)
        {
            try {
                bl_bllxEntity bllxtb = _BllxRepo.FindEntity(p => p.OrganizeId == OrgId && p.zt == "1" && (p.bllx == bllx || p.Id==blId));
                string sql = @"select @mbqx [mbqx],a.blId,a.[bllx],b.blxtml,b.blxtmc_yj blxtmc,0 [Isempty]
                        from [dbo].[zy_meddocs_relation] a with(nolock), " + bllxtb.relTB + @" b with(nolock)
                        where a.blid = b.id
                        and a.blid = @blId and a.OrganizeId = @OrgId";

                var para = new List<SugarParameter>();
                para.Add(new SugarParameter("@OrgId", OrgId));
                para.Add(new SugarParameter("@mbqx", mbqx));
                para.Add(new SugarParameter("@blId", blId));

                return this.FirstOrDefault<BlConvertToTemplateVO>(sql, para.ToArray());
            }
            catch (Exception ex)
            {
                throw new FailedException("Lỗi chuyển đổi mẫu:"+ex.InnerException);
            }


        }

        public void BlConvertToTemplateProcess(BlmblbEntity ety)
        {
            string aimPath = Path.GetDirectoryName(ety.mblj);
            string srcPath = ety.Memo + ".xml";
            bool flag = false;
            try
            {
                flag = CopyDir(srcPath, aimPath, ety.mbmc + ".xml", 0);
            }
            catch
            {
                flag = false;
            }
            if (flag == false)
            {
                var mbinfo = _BlmblbRepo.FindEntity(p => p.OrganizeId == ety.OrganizeId && p.mbbm == ety.mbbm && p.zt == "1");
                mbinfo.zt = "0";
                _BlmblbRepo.SubmitForm(mbinfo, mbinfo.Id);
            }

        }
        /// <summary>
        /// Thao tác tệp
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="aimPath"></param>
        /// <param name="newName"></param>
        /// <param name="type">0 Sao chép  1 Di chuyển</param>
        /// <returns></returns>
        public bool CopyDir(string srcPath, string aimPath, string newName,int type)
        {
            bool flag = false;
            // Kiểm tra thư mục đích có kết thúc bằng ký tự phân tách thư mục hay không, nếu không thì thêm vào
            if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                aimPath += Path.DirectorySeparatorChar;
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                newName = Path.GetFileName(srcPath);
            }

            // Phán đoán thư mục mẫu có tồn tại hay không, nếu không thì tạo mới
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            if (File.Exists(srcPath) && (File.Exists(aimPath + newName)) && type==1)
            {
                File.Delete(aimPath + newName);
            }
            //Nếu tệp tồn tại thì sao chép trực tiếp
            if (File.Exists(srcPath) && !(File.Exists(aimPath + newName)))
            {
                if (type == 0)
                {
                    File.Copy(srcPath, aimPath + newName, true);
                    flag = true;
                }
                else if (type == 1)
                {
                    File.Move(srcPath, aimPath + newName);
                    flag = true;
                }
            }
            

            return flag;
        }


        #endregion


        #region Quyền
        public IList<TemplateQxkzVO> Getqxkz(string staffId, string mbId, string bllx)
        {
            string sql = @"select  a.bllx, isnull(max(c.ctrlLevel),0) ctrlLevel
from bl_mblb a with(nolock),[NewtouchHIS_Base].[dbo].[V_C_Sys_StaffDuty] b with(nolock),[bl_mbqxkz] c with(nolock)
where a.Id=c.mbId and b.StaffId=@staffId and b.OrganizeId=c.OrganizeId and b.DutyCode=c.dutyCode
and a.zt='1' and b.zt='1' and c.zt='1'
";
            if (!string.IsNullOrWhiteSpace(mbId))
            {
                sql += " and a.Id=@mbId ";
            }
            if (!string.IsNullOrWhiteSpace(bllx))
            {
                sql += " and a.bllx=@bllx ";
            }
            sql += " group by a.bllx ";

            return this.FindList<TemplateQxkzVO>(sql, new SugarParameter[] {
                new SugarParameter("@staffId",staffId),
                new SugarParameter("@mbId",mbId ?? ""),
                new SugarParameter("@bllx",bllx)
            });
        }

        #endregion
        /// <summary>
        /// Danh sách mẫu bệnh án
        /// </summary>
        /// <param name="mbqx"></param>
        /// <param name="bllx"></param>
        /// <param name="ksbm"></param>
        /// <param name="user"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public IList<BlmbListVO> MedRecordTmpListTree(string OrgId,string keyValue,string mbqx)
       {
            string sql = @"select id,bllx,bllxmc mbmc,ParentId,'' Turl,px from (SELECT a.[Id]
,a.[bllx]
,a.[bllxmc]
,a.[CreateTime]
,a.[CreatorCode]
,a.[LastModifyTime]
,a.[LastModifierCode]
,a.[zt]
,isnull(a.RelDutys,r.RelDutys)RelDutys
,(select b.[name]+'，' from [NewtouchHIS_Base].[dbo].[V_S_Sys_Duty] b where charindex(','+b.code+',',','+isnull(a.RelDutys,r.RelDutys))>0 for xml path('')) [RelDutysDesc]
,a.[OrganizeId]
,isnull(a.[bllxcode],r.[bllxcode])[bllxcode]
,a.[px],
isnull(a.[relTB],r.[relTB])[relTB],
isnull(a.MenuLev,r.MenuLev)MenuLev,a.ParentId,a.IsRoot,a.mzbz
FROM [dbo].[bl_bllx] a with(nolock)
left join [dbo].[bl_bllx] r with(nolock) on a.ParentId=r.Id
where a.organizeid=@OrgId and a.zt='1') bllx
union all 
select id,bllx,mbmc mbmc,bllxId ParentId,mblj Turl,99 px from (select Id, OrganizeId,mbqx,mbbm,REPLACE(mbmc,'\','/') mbmc,bllxId,bllxmc,ksbm,ysgh,mblj,py,Isempty,
                    Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,bllx,LoadWay
                    from bl_mblb a with(nolock)
					where organizeid=@OrgId and zt=1 
                    and mbqx=@mbqx
                    and mbmc  like @keyValue
					) mbmx  order by px
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrgId));
            para.Add(new SugarParameter("@keyValue", "%" + keyValue + "%"));
            para.Add(new SugarParameter("@mbqx", mbqx ?? ""));
            return this.FindList<BlmbListVO>(sql, para.ToArray());

        }

        /// <summary>
        /// Bệnh án của bệnh nhân đã có mẫu
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <param name="bllx"></param>
        /// <returns></returns>
        public IList<blmbmcVO> getRepeatBl(string OrgId, string zyh, string bllx)
        {
            string sql = @"
select c.bllx,c.bllxmc,mbbm,mbmc,blmc from [zy_meddocs_relation] a
left join [dbo].[bl_mblb] b
on a.mbid=b.id and a.zt=b.zt and a.organizeId=b.organizeId
left join bl_bllx c on c.id =b.bllxId and b.zt=c.zt and b.organizeId=c.organizeId
where a.zt=1 and a.organizeId=@OrgId
and zyh=@zyh
and c.bllx =@bllx
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrgId));
            para.Add(new SugarParameter("@zyh",  zyh ));
            para.Add(new SugarParameter("@bllx", bllx ?? ""));
            return this.FindList<blmbmcVO>(sql, para.ToArray());

        }

        public string CheckBlRules(string OrgId, string zyh, string bllx)
        {
            var zybrxx=_zybrjbxxRepo.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrgId && p.zt == "1");
            
            var rulseentity = mrwritingrulesRepo.FindEntity(p => p.Bllx == bllx && p.OrganizeId == OrgId && p.zt == "1");
            var rulsdate = zybrxx.ryrq.AddDays(rulseentity.RulesDay);
            if (rulseentity != null && rulseentity.RulesDay!=0)
            {
                if (rulsdate<=DateTime.Now)
                {
                    return "F|"+ rulsdate;
                }
            }
            return "T|" + rulsdate;
        }
    }
}
