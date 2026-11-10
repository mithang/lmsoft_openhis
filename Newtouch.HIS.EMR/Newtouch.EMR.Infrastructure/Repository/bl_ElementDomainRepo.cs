using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.EMR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    public class bl_ElementDomainRepo : RepositoryBase<bl_ElementDomainEntity>, Ibl_ElementDomainRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>

        public List<bl_ElementDomainEntity> GetElementTree(string orgId)
        {
            return this.IQueryable().Where(p => p.Zt == 1 && p.OrganizeId == orgId).OrderBy(p => p.Px).ToList();
        }

        public IList<BlysGridVo> GetElementDetail(Pagination pagination, string OrganizeId, string keyword)
        {
            string sql = @" 
     select jgmx.Id
        ,ElementId, Table_Name, Table_Column_No, Table_Column_Code, Table_Colunn_Name
        ,Table_Column_Type, Element_ID, Element_Name, Element_Type, Element_Type_Name, jgmx.Sybz, jgmx.Px
	    ,AreValue,ysmxId,ysmx.ysmxname AreValuemc
     from bl_ElementDomain_Detail jgmx 
	 left join bl_ysmx ysmx on jgmx.ysmxId=ysmx.YsId and ysmx.ysmxcode=arevalue
     where ElementId=@keyword and jgmx.zt=1 and jgmx.OrganizeId=@OrgId
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@OrgId", OrganizeId));
            para.Add(new SugarParameter("@keyword", keyword));
            return this.QueryWithPage<BlysGridVo>(sql, pagination, para.ToArray()).ToList();
        }

        public List<BlysListVo> GetBlys(string keyword, string orgId)
        {
            string sql = @"select top 20 b.ysmc ysdl,a.Id ysId,a.yscode ysdm,a.ysmc,a.yslx,a.BindingPath from bl_ys a
 join bl_ys b on a.yssjid = b.Id and a.OrganizeId=b.OrganizeId 
 where  a.zt = 1 and (a.OrganizeId = @orgId or a.OrganizeId='*')  and (a.ysmc like @keyword or a.py like @keyword)
 and b.zt = 1 --and b.yslx is null
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@keyword",'%'+ keyword+'%'));
            return this.FindList<BlysListVo>(sql, para.ToArray()).ToList();
        }

        public List<TableColumnVo> GetTabColumnList(string tabName,string keyword)
        {
            
            string sql = @"select col.column_id,col.name columnName,lx.name columnType,col.max_length,col.scale
  from sys.columns col,sys.types lx
 where col.object_id = object_id(@tabName)
   and col.system_type_id = lx.system_type_id and col.name like @keyword
   order by col.column_id";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@tabName", tabName));
            para.Add(new SugarParameter("@keyword", '%' + keyword + '%'));
            return this.FindList<TableColumnVo>(sql, para.ToArray()).ToList();
        }
        public List<BlysDeatilVo> GetBlysmx(string keyword, string orgId)
        {
            string sql = @" select ysId,ysmxcode,ysmxName from bl_ysmx where ysId=@ysId and sybz=1 and (OrganizeId= @orgId or OrganizeId='*' ) and zt = 1
";
            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@ysId", keyword));
            return this.FindList<BlysDeatilVo>(sql, para.ToArray()).ToList();
        }

        public void SubmitForm(bl_ElementDomainEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (!int.TryParse(keyValue, out int Id))
                {
                    throw new FailedException("Mã bảng cấu trúc bệnh án không hợp lệ.");
                }
                var dbEntity = this.FindEntity(Id);
                if (dbEntity == null)
                {
                    throw new FailedException("Không tìm thấy bảng cấu trúc bệnh án.");
                }
                dbEntity.Px = entity.Px;
                dbEntity.Regex = entity.Regex;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                this.Update(dbEntity);
            }
            else
            {
                // Bllx la khoa dinh danh + khoa chong trung: khong cho NULL
                // (check trung `Bllx == null` dich ra `IS NULL` nen se khop bat ky
                // dong Bllx NULL nao, bao sai "da ton tai" - bang chung probe #5c).
                if (string.IsNullOrWhiteSpace(entity.Bllx))
                {
                    throw new FailedException("Mã loại bệnh án không được để trống.");
                }
                if (entity.Bllx.Length > 10)
                {
                    throw new FailedException("Mã loại bệnh án tối đa 10 ký tự.");
                }
                if (!string.IsNullOrEmpty(entity.Table_Name) && entity.Table_Name.Length > 30)
                {
                    throw new FailedException("Tên bảng tối đa 30 ký tự.");
                }
                if (!string.IsNullOrEmpty(entity.Table_EngLish_Name) && entity.Table_EngLish_Name.Length > 30)
                {
                    throw new FailedException("Tên bảng (tiếng Anh) tối đa 30 ký tự.");
                }
                if (!string.IsNullOrEmpty(entity.Regex) && entity.Regex.Length > 100)
                {
                    throw new FailedException("Biểu thức chính quy tối đa 100 ký tự.");
                }
                var check = this.FindEntity(p => p.Bllx == entity.Bllx && p.Zt == 1 && p.OrganizeId == entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("Bảng cấu trúc bệnh án này đã tồn tại!");
                }
                
                entity.CreateTime = DateTime.Now;
                entity.Zt =1;
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue, string orgId, string user)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(orgId))
            {
                if (!int.TryParse(keyValue, out int Id))
                {
                    throw new FailedException("Bảng bệnh án không tồn tại hoặc đã bị xóa, vui lòng chọn lại!");
                }
                bl_ElementDomainEntity ety = this.FindEntity(p => p.Id == Id && p.OrganizeId == orgId && p.Zt ==1);
                if (ety != null)
                {
                    ety.Zt = 0;
                    ety.LastModifierCode = user;
                    ety.LastModifyTime = DateTime.Now;
                    this.Update(ety);
                }
                else
                {
                    throw new FailedException("Bảng bệnh án không tồn tại hoặc đã bị xóa, vui lòng chọn lại!");
                }
            }
            else
            {
                throw new FailedException("Bảng bệnh án không tồn tại hoặc đã bị xóa, vui lòng chọn lại!");
            }
        }

        #region Bảo vệ định danh SQL (tên bảng/cột từ từ điển)
        // Tên bảng/cột lấy từ hàng từ điển DB không bao giờ được nội suy trực tiếp:
        // chỉ chấp nhận định danh [A-Za-z_][A-Za-z0-9_]*, còn lại fail-closed
        // (ném FailedException, không thực thi SQL). Mọi giá trị từ điển đang có
        // trong DB demo (TB_BL_RYJL/TB_BL_CYJL/TB_BL_BCJL, bl_bcjl/...,
        // ADM_TIME/...) đều khớp mẫu này — kiểm chứng chỉ-đọc ngày 02/11/2026.
        private static readonly Regex _sqlIdentifierRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);

        private static string RequireValidSqlIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier) || !_sqlIdentifierRegex.IsMatch(identifier))
            {
                throw new FailedException("Tên bảng/cột cấu trúc bệnh án không hợp lệ, không thể ghi dữ liệu!");
            }
            return identifier;
        }
        #endregion

        #region Lưu trữ chuyển đổi bảng dọc cấu trúc bệnh án sang bảng ngang
        public void BljghDataDealwith(BljghReq req)
        {
            //Bảng cấu trúc khớp biểu thức chính quy
            string bljgTable = "";
            var bljgTab = this.IQueryable(p => p.Zt == 1).ToList();
            foreach (var item in bljgTab)
            {
                // FIX gstack T-46: mot dong Regex NULL lam new Regex() throw
                // truoc moi nhanh ghi/xoa, tra 20000 cho moi blmc. Bo qua
                // dong khong co mau (khong the khop) thay vi throw; cac
                // dong co Regex van khop nhu cu. Khong tu dien Regex gia
                // cho dong NULL (do la du lieu, can quyet rieng).
                if (string.IsNullOrWhiteSpace(item.Regex))
                {
                    continue;
                }
                Regex regex = new Regex(item.Regex);
                if (regex.IsMatch(req.blmc))
                {
                    string c1 = (regex.Match(req.blmc).Groups[0].ToString());
                    bljgTable = item.Table_EngLish_Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(bljgTable))
                return;

            // Fail-closed: tên bảng từ từ điển phải là định danh hợp lệ trước mọi câu lệnh.
            RequireValidSqlIdentifier(bljgTable);

            if (req.delzt=="0")
            {
                // FIX SQLi: tham số hóa czr/blId/organizeId (trước đây nội suy trực tiếp).
                string deletesql = $@" update {bljgTable} set bz='0',LastModifyTime=getdate(),LastModifierCode=@czr
   where bl_pzhm=@blId and organizeid=@orgId ";
                this.ExecuteSqlCommand(deletesql, new SugarParameter[] {
                    new SugarParameter("@czr", req.czr ?? ""),
                    new SugarParameter("@blId", req.blId ?? ""),
                    new SugarParameter("@orgId", req.organizeId ?? "")
                           });
                return;
            }
            //Thông tin bệnh nhân
            ZybrjbxxEntity patlist = GetZybrjbxx(req.zyh, req.organizeId);
            //Thông tin bệnh án
            bl_bllxEntity blbentity = GetBllxxx(req.bllx,req.organizeId);
            BlwsInfoVo blwsentity = new BlwsInfoVo ();
            if (blbentity!=null)
            {
                blwsentity = GetBljlInfo(blbentity.relTB,req.blId,req.organizeId);
            }
            //Cấu trúc bảng chi tiết
            List<ElementTablStructureVo> jgmxEntity = GetElementTableColumn(bljgTable,req.organizeId);
            List<ElementTablStructureVo> jgDataEntity = GetVerticalTableData(req.blId, req.zyh,req.organizeId);

            jgmxEntity = (from a in jgmxEntity
                          join b in jgDataEntity on a.Element_ID equals b.Element_ID into ab
                          from ba in ab.DefaultIfEmpty(new ElementTablStructureVo())
                          select new ElementTablStructureVo
                          {
                              Table_Column_Code =a.Table_Column_Code,
                              Table_Colunn_Name=a.Table_Colunn_Name,
                              Element_ID=a.Element_ID,
                              Element_Name=a.Element_Name,
                              Element_Value=ba.Element_Value
                          }
                       ).ToList();

            byte[] bytes = System.Text.Encoding.Default.GetBytes(blwsentity.xmlConten);
            var xmlcontentBase64 = Convert.ToBase64String(bytes);
            if (IsCrossDataExist(bljgTable, req.zyh, req.organizeId, req.blId) == 0)
            {
                string sqlInsert = "";
                try {
                    // FIX SQLi: tham số hóa mọi giá trị; tên cột từ từ điển phải là định danh hợp lệ.
                    foreach (var col in jgmxEntity)
                    {
                        RequireValidSqlIdentifier(col.Table_Column_Code);
                    }
                    var insertPara = new List<SugarParameter>();
                    insertPara.Add(new SugarParameter("@orgId", req.organizeId ?? ""));
                    insertPara.Add(new SugarParameter("@zyh", req.zyh ?? ""));
                    insertPara.Add(new SugarParameter("@bllx", req.bllx ?? ""));
                    insertPara.Add(new SugarParameter("@blId", req.blId ?? ""));
                    insertPara.Add(new SugarParameter("@blrq", (object)blwsentity.blrq ?? DBNull.Value));
                    insertPara.Add(new SugarParameter("@xmlConten", blwsentity.xmlConten ?? ""));
                    insertPara.Add(new SugarParameter("@base64", xmlcontentBase64));
                    insertPara.Add(new SugarParameter("@czr", req.czr ?? ""));
                    string insertCol = $@" insert into {bljgTable} ([OrganizeId],[ZYH],[BL_TYPE],[BL_PZHM],[BL_RECORD_DATE],[BL_TEXT],[BL_BASE64],CreateTime,CreatorCode,bz,";
                    string insertVal = @" values(@orgId,@zyh,@bllx,@blId,@blrq,@xmlConten,@base64,getdate(),@czr,1,";
                    int evIdx = 0;
                    foreach (var item in jgmxEntity)
                    {
                        insertCol += item.Table_Column_Code + ",";
                        string evParam = "@ev" + evIdx++;
                        insertVal += evParam + ",";
                        insertPara.Add(new SugarParameter(evParam, string.IsNullOrWhiteSpace(item.Element_Value) ? (object)DBNull.Value : (object)item.Element_Value.Replace('\u3000', ' ')));
                    }
                    insertCol = insertCol.Substring(0, insertCol.Length - 1) + ")";
                    insertVal = insertVal.Substring(0, insertVal.Length - 1) + ")";

                    sqlInsert = insertCol + insertVal;
                    this.ExecuteSqlCommand(sqlInsert, insertPara.ToArray());
                }
                catch (Exception e)
                {
                    string rc = req.zyh + "|" + req.bllx + "|" + req.blId + "|" + req.blmc;
                    EmrLogger.Info("Chuyển dữ liệu bảng dọc sang bảng ngang khi thêm mới xảy ra lỗi:" + e.Message + ",Tham số đầu vào:" + rc + "+SQL câu lệnh:" + sqlInsert);
                }
            }
            else
            {
                string updatesql = "";
                try {
                    // FIX SQLi: tham số hóa mọi giá trị; tên cột từ từ điển phải là định danh hợp lệ.
                    foreach (var col in jgmxEntity)
                    {
                        RequireValidSqlIdentifier(col.Table_Column_Code);
                    }
                    var updatePara = new List<SugarParameter>();
                    updatePara.Add(new SugarParameter("@xmlConten", blwsentity.xmlConten ?? ""));
                    updatePara.Add(new SugarParameter("@base64", xmlcontentBase64));
                    updatePara.Add(new SugarParameter("@czr", req.czr ?? ""));
                    updatePara.Add(new SugarParameter("@blId", req.blId ?? ""));
                    updatePara.Add(new SugarParameter("@zyh", req.zyh ?? ""));
                    updatesql = $@" update {bljgTable} set BL_TEXT=@xmlConten ,BL_BASE64=@base64,LastModifyTime=getdate(),LastModifierCode=@czr, ";
                    int uvIdx = 0;
                    foreach (var item in jgmxEntity)
                    {
                        var d = string.IsNullOrWhiteSpace(item.Element_Value) ? string.Empty : item.Element_Value.Replace('\u3000', ' ');
                        string uvParam = "@uv" + uvIdx++;
                        updatesql += item.Table_Column_Code + " =" + uvParam + ",";
                        updatePara.Add(new SugarParameter(uvParam, d));
                    }
                    updatesql = updatesql.Substring(0, updatesql.Length - 1);
                    updatesql = updatesql + @" where bl_pzhm=@blId and zyh=@zyh and bz=1";
                    this.ExecuteSqlCommand(updatesql, updatePara.ToArray());
                }
                catch (Exception e) {
                    string rc = req.zyh + "|" + req.bllx + "|" + req.blId + "|" + req.blmc;
                    EmrLogger.Info("Chuyển dữ liệu bảng dọc sang bảng ngang khi cập nhật xảy ra lỗi:" + e.Message + ",Tham số đầu vào:" + rc + "+SQL câu lệnh:" + updatesql);
                }
            }

        }

        public BlwsInfoVo GetBljlInfo(string tableName,string blId,string orgId)
        {
            RequireValidSqlIdentifier(tableName);
            string sql = $@"select dzbl_id
 ,ksmc,ksdm,qmrq,czydm_zzys,a.blrq,a.creatorcode,b.xmlConten from  {tableName}  a
join zy_meddocs_relation b on a.Id=b.blId and a.organizeid=b.organizeid
where a.id=@blId and a.organizeId=@orgId and a.zt='1' ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@blId", blId));
            return this.FindList<BlwsInfoVo>(sql, para.ToArray()).FirstOrDefault();
        }

        public ZybrjbxxEntity GetZybrjbxx(string zyh,string orgId)
        {
            string sql = @"select * from zy_brjbxx where zyh=@zyh and organizeId=@orgId and zt=1  ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@zyh", zyh));
            return this.FindList<ZybrjbxxEntity>(sql, para.ToArray()).FirstOrDefault();
        }
        public bl_bllxEntity GetBllxxx(string bllx, string orgId)
        {
            string sql = @"select * from bl_bllx where bllx=@bllx and organizeId=@orgId and zt=1  ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@bllx", bllx));
            return this.FindList<bl_bllxEntity>(sql, para.ToArray()).FirstOrDefault();
        }

        public int IsCrossDataExist(string tableName,string zyh,string orgId,string blId)
        {
            RequireValidSqlIdentifier(tableName);
            string sql = $@"select count(1) from {tableName} where zyh=@zyh and organizeId=@orgId and BL_PZHM=@blId and bz=1  ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@zyh", zyh));
            para.Add(new SugarParameter("@blId", blId));
            return EmrDb.GetConnection().Ado.SqlQuery<int>(sql, para.ToArray()).FirstOrDefault();
        }

        public List<ElementTablStructureVo> GetElementTableColumn(string tableName, string orgId)
        {
            string sql = $@"select Table_Column_Code,Table_Colunn_Name,Element_ID,Element_Name,'' Element_Value from bl_ElementDomain jg
join bl_ElementDomain_Detail jgmx on jgmx.ElementId=jg.Id and jgmx.organizeId=jg.organizeId and jgmx.zt=1 and sybz=1 
where jg.Table_EngLish_Name=@tableName and jg.organizeId=@orgId and jg.zt=1  ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@tableName", tableName ?? ""));
            return this.FindList<ElementTablStructureVo>(sql, para.ToArray());
        }

        public List<ElementTablStructureVo> GetVerticalTableData(string blId,string zyh, string orgId)
        {
            string sql = @"select yscode Element_ID,ysvalue Element_Value from [bl_ysjgnr] 
where organizeId=@orgId  and zyh=@zyh and blId=@blId and zt=1 ";

            var para = new List<SugarParameter>();
            para.Add(new SugarParameter("@orgId", orgId));
            para.Add(new SugarParameter("@blId", blId));
            para.Add(new SugarParameter("@zyh", zyh));
            return this.FindList<ElementTablStructureVo>(sql, para.ToArray());
        }
       
        #endregion


    }
}
