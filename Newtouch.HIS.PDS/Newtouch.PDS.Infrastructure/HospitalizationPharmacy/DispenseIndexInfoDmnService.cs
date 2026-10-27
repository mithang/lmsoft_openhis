using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Core.Common.Utils;
using FrameworkBase.MultiOrg.DmnService;
using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// phát/Trả thuốc
    /// </summary>
    public class DispenseIndexInfoDmnService : DmnServiceBase, IDispenseIndexInfoDmnService
    {
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly IZyReturnDrugApplyBillDetailRepo _drugApplyBillDetail;
        private readonly IZyReturnDrugApplyBillRepo _drugApplyBill;
        private readonly IZyYpyzxxRepo _yzYpyzxxRepo;
        private readonly IZyYpyzzxphRepo _zyYpyzzxphRepo;
        public DispenseIndexInfoDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }


        #region Chuẩn bị thuốc khu bệnh

        /// <summary>
        /// Thực hiện thao tác chuẩn bị thuốc gọi thủ tục lưu trữ （Nếu số lượng thuốc thực hiện y lệnh lớn hơn0 Dữ liệu và đã lấy được，Trực tiếp thực hiện thao tác chuẩn bị thuốc）
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="tdrq"></param>
        /// <param name="UserLogin"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public string GetBQPYXXList(DispenseBQInfoVO model)
        {
            var outpar = new SugarParameter("@as_err", "") { DbType = System.Data.DbType.AnsiString, Size = 100 };
            outpar.Direction = System.Data.ParameterDirection.Output;
            var sqlParList = new List<SugarParameter>();
            sqlParList.Add(new SugarParameter("@ai_nm", model.zxId));//Mã nội bộ đơn thuốc
            sqlParList.Add(new SugarParameter("@ai_mxnm", ""));//Mã nội bộ chi tiết
            sqlParList.Add(new SugarParameter("@as_yp", model.ypCode.Trim()));//Thuốccode@ai_mxnm
            sqlParList.Add(new SugarParameter("@as_yjbm", model.lyyf.Trim()));//Bộ phận dược(tương ứngks)  
            sqlParList.Add(new SugarParameter("@adc_sl", model.sl));//Số lượng
            sqlParList.Add(new SugarParameter("@ai_cls", model.cls));//Số chia lẻ(Không cần truyền chia lẻ1)
            sqlParList.Add(new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));//OrganizeId
            sqlParList.Add(new SugarParameter("@user_code", OperatorProvider.GetCurrent().UserCode));//Số lượng
            sqlParList.Add(new SugarParameter("@adt_tdrq", model.jdrq));// Ngày lấy hồ sơ
            sqlParList.Add(new SugarParameter("@yzzxid", ""));//Thực hiện y lệnhID
            sqlParList.Add(outpar);
            sqlParList.Add(new SugarParameter("@as_fkcbz", "0"));//Cờ tồn kho âm 0 Không dùng tồn kho âm 1Sử dụng  
            PdsDb.GetConnection().Ado.ExecuteCommand(" Exec YP_XT_OP_PY @ai_nm,@ai_mxnm,@as_yp,"
               + "@as_yjbm,@adc_sl,@ai_cls,@OrganizeId,@user_code,@adt_tdrq,@yzzxid,@as_err out,@as_fkcbz", sqlParList.ToArray());
            if (outpar.Value.ToString().Trim() == "" || outpar.Value.ToString().Trim() == "1")// Thủ tục lưu trữ trả về 1 là thực hiện thành công。
            {
                return "1";
            }
            else
            {
                return "Thực hiện chuẩn bị thuốc thất bại:Mã nội bộ y lệnh【" + model.zxId + "】,Ngày lấy hồ sơ【" + model.jdrq + "】，" + outpar.Value.ToString();
            }
        }

        /// <summary>
        /// Khi thực hiện xếp thuốc thì thao tác trả thuốc gọi thủ tục lưu trữ（Nếu số lượng thuốc thực hiện y lệnh nhỏ hơn hoặc bằng0  Thực hiện thao tác trả thuốc）
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="tdrq"></param>
        /// <param name="UserLogin"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public string GetBQTYXXList(DispenseBQInfoVO model)
        {
            var outpar = new SugarParameter("@a_err", "") { DbType = System.Data.DbType.AnsiString, Size = 100 };
            outpar.Direction = System.Data.ParameterDirection.Output;
            var sqlParList = new List<SugarParameter>();
            sqlParList.Add(new SugarParameter("@a_yzId", model.zxId));//Mã nội bộ đơn thuốc/Y lệnhID
            sqlParList.Add(new SugarParameter("@a_yp", model.ypCode));//Thuốccode
            sqlParList.Add(new SugarParameter("@adt_tdrq", model.lyyf));//Bộ phận dược(tương ứngks)  
            sqlParList.Add(new SugarParameter("@a_yjbm", model.sl));//Số lượng
            sqlParList.Add(new SugarParameter("@@a_sl", model.cls));//Số chia lẻ(Không cần truyền chia lẻ1)    
            sqlParList.Add(new SugarParameter("@a_cls", model.jdrq));// Ngày lấy hồ sơ
            sqlParList.Add(new SugarParameter("@a_fkcbz", "0"));//Cờ tồn kho âm 0 Không dùng tồn kho âm 1Sử dụng 
            sqlParList.Add(outpar);// Thông báo lỗi
            PdsDb.GetConnection().Ado.ExecuteCommand("Exec YP_XT_OP_BQPY @a_yzId, @a_yp,@adt_tdrq, @a_yjbm,@a_sl,@a_cls,@a_fkcbz, @a_err out ", sqlParList.ToArray());
            if (outpar.ToString().Trim() == "" || outpar.ToString().Trim() == "1")// Thủ tục lưu trữ trả về 1 là thực hiện thành công。
            {
                return "1";
            }
            else
            {
                return "Thực hiện chuẩn bị thuốc thất bại:Mã nội bộ y lệnh【" + model.zxId + "】,Ngày lấy hồ sơ【" + model.jdrq + "】," + outpar.Value.ToString();
            }
        }


        #endregion

        #region Phát thuốc

        /// <summary>
        /// Trước khi trả lại phát thuốc kiểm tra đã thao tác chưa
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool IsFYTHHasOperated(List<DispenseBQRYXXIndexVO> list)
        {
            StringBuilder strSql = new StringBuilder("select fybz from NewtouchHIS_PDS.dbo.zy_ypyzxx");
            var parms = new List<SugarParameter>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    strSql.Append(" where 1=0");
                }
                strSql.Append(string.Format(" or Id = @Id{0}", i));
                parms.Add(new SugarParameter(string.Format("@Id{0}", i), list[i].Id));
            }
            List<YPFYPatientInfoVO> listFind = this.FindList<YPFYPatientInfoVO>(strSql.ToString(), parms.ToArray());
            if (listFind != null && listFind.Count > 0)
            {
                foreach (var Row in listFind)
                {
                    string FYStatus = Row.fybz == null ? "" : Row.fybz.ToString();
                    if (FYStatus.Equals(((int)EnumFybz.Wp).ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Trả lại phát thuốc
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string ZYBQFYReturn(List<DispenseBQRYXXIndexVO> list, string fybz)
        {
            if (list.Count < 1)
            {
                return "Dữ liệu gửi trống！";
            }

            if (IsFYTHHasOperated(list))
            {
                return "Trả lại phát thuốc đã được thao tác，Vui lòng làm mới。";
            }

            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    foreach (var item in list)
                    {
                        var strSql = new StringBuilder();
                        //Sửa trạng thái phát thuốcLastModiFierCode
                        strSql.Append(@"UPDATE NewtouchHIS_PDS.dbo.zy_ypyzxx
                                  SET       fybz = @fybz,
                                            LastModiFierCode = @user,
                                            LastModifyTime = @time
                                  WHERE     Id = @Id;");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@fybz", ((int)EnumFybz.Wp).ToString()),
                        new SugarParameter("@user", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@time", DateTime.Now),
                        new SugarParameter("@Id", item.Id)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //Trừ đóng băng phát thuốc khỏi đóng băng
                        strSql.Clear();
                        strSql.Append(@"update NewtouchHIS_PDS.dbo.xt_yp_kcxx
                                  SET	djsl=djsl-@sl,
						                LastModifyTime=GETDATE(),
						                LastModifierCode=@CreatorCode
				                WHERE	pc=@pc
						                AND ph=@ph
						                AND ypdm=@ypCode
						                AND OrganizeId=@OrganizeId
                                        AND yfbmCode=@yfbmCode
                                        AND (djsl - @sl) >= 0;");
                        paraList = new SugarParameter[]
                        {
                        new SugarParameter("@pc", item.yppc),
                        new SugarParameter("@ph", item.ypph),
                        new SugarParameter("@ypCode", item.ypCode),
                        new SugarParameter("@sl", item.sl),
                        new SugarParameter("@OrganizeId", item.OrganizeId),
                        new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                    }
                    db.Commit();
                }
                return "Thao tác thành công";
            }
            catch
            {
                return "Thao tác thất bại";
            }
        }

        /// <summary>
        /// Trước khi phát thuốc kiểm tra đã được thao tác chưa
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool IsFYHasOperated(List<DispenseBQRYXXIndexVO> list)
        {
            StringBuilder strSql = new StringBuilder("select fybz from NewtouchHIS_PDS.dbo.zy_ypyzxx");
            var parms = new List<SugarParameter>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    strSql.Append(" where 1=0");
                }
                strSql.Append(string.Format(" or Id = @Id{0}", i));
                parms.Add(new SugarParameter(string.Format("@Id{0}", i), list[i].Id));
            }
            List<YPFYPatientInfoVO> listFind = this.FindList<YPFYPatientInfoVO>(strSql.ToString(), parms.ToArray());
            if (listFind != null && listFind.Count > 0)
            {
                foreach (var Row in listFind)
                {
                    string FYStatus = Row.fybz == null ? "" : Row.fybz.ToString();
                    if (!FYStatus.Equals(((int)EnumFybz.Yp).ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Thao tác phát thuốc
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string ZYBQFYOperate(List<DispenseBQRYXXIndexVO> list, string fybz)
        {
            if (list.Count < 1)
            {
                return "Dữ liệu gửi trống！";
            }

            if (IsFYHasOperated(list))
            {
                return "Phát thuốc đã được thao tác，Vui lòng làm mới。";
            }

            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    foreach (var item in list)
                    {
                        var strSql = new StringBuilder();
                        //Sửa trạng thái phát thuốcLastModiFierCode
                        strSql.Append(@"UPDATE NewtouchHIS_PDS.dbo.zy_ypyzxx
                                  SET       fybz = @fybz,
                                            LastModiFierCode = @user,
                                            LastModifyTime = @time
                                  WHERE     Id = @Id;");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@fybz", ((int)EnumFybz.Yf).ToString()),
                        new SugarParameter("@user", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@time", DateTime.Now),
                        new SugarParameter("@Id", item.Id)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        //Thêm bản ghi thao tác phát thuốc
                        strSql.Clear();
                        strSql.Append(@"insert into NewtouchHIS_PDS.dbo.zy_ypyzczjl
                                  (ypyzxxId,operateType,ypCode,sl,CreateTime,CreatorCode)
                                  values (@ypyzxxId,@operateType,@ypCode,@sl,@CreateTime,@CreatorCode);");
                        paraList = new SugarParameter[]
                        {
                        new SugarParameter("@ypyzxxId", item.Id),
                        new SugarParameter("@operateType", ((int)EnumOperateType.Fy).ToString()),
                        new SugarParameter("@ypCode", item.ypCode),
                        new SugarParameter("@sl", item.sl),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@CreateTime", DateTime.Now)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //Trừ phát thuốc khỏi tồn kho
                        strSql.Clear();
                        strSql.Append(@"update NewtouchHIS_PDS.dbo.xt_yp_kcxx
                                  SET	kcsl=kcsl-@sl,
						                djsl=djsl-@sl,
						                LastModifyTime=GETDATE(),
						                LastModifierCode=@CreatorCode
				                WHERE	pc=@pc
						                AND ph=@ph
						                AND ypdm=@ypCode
						                AND OrganizeId=@OrganizeId
                                        AND yfbmCode=@yfbmCode
                                        AND (kcsl - @sl) >= 0
                                        AND (djsl - @sl) >= 0;");
                        paraList = new SugarParameter[]
                        {
                        new SugarParameter("@pc", item.yppc),
                        new SugarParameter("@ph", item.ypph),
                        new SugarParameter("@ypCode", item.ypCode),
                        new SugarParameter("@sl", item.sl),
                        new SugarParameter("@OrganizeId", item.OrganizeId),
                        new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                    }
                    db.Commit();
                }
                return "Thao tác thành công";
            }
            catch
            {
                return "Thao tác thất bại";
            }
        }

        //Chuẩn bị thuốc/Phát thuốc Hoàn thành，Sửa trạng thái thực hiện y lệnh
        public string UPBQPYyzzxZT(string tdrq, string zxId, string fybz, string ypdj, string ypje, string zfbl, string zfxz)
        {
            //fybz 0：Chưa chuẩn bị thuốc，1：Đã chuẩn bị thuốc ，2：Đã phát thuốc
            var reqObj = new
            {//Sửa bảng thực hiện y lệnh Trạng thái ghi chú phát thuốc
                pyry = OperatorProvider.GetCurrent().UserCode,
                pyrq = "",
                tdrq = tdrq,
                zxId = zxId,
                fybz = fybz,// Sửa thành 1 là trạng thái chuẩn bị thuốc，2 là trạng thái phát thuốc
                ypdj = ypdj,
                ypje = ypje,
                zfbl = zfbl,
                zfxz = zfxz,
                TimeStamp = DateTime.Now
            };
            var apiResp = SiteSettAPIHelper.Request<object, APIRequestHelper.DefaultResponse>("api/HospitalizationPharmacy/WaitingDispenseDispenseUPBQPYyzzxTotal", reqObj);
            return apiResp.data.ToString();
        }

        /// <summary>
        /// Lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<YPFYPatientInfoVO> SelectDispenseDrugDetail(HospitalizationDispenseDrugParam req)
        {
            string sql = @"
SELECT o.*, dbo.f_getYfbmYpComplexYpSlandDwV2(o.zxdwsl, @yfbmCode, o.ypCode, @OrganizeId,'2') slStr , o.zxdwsl as sl
FROM (
	SELECT SUM(c.sl) zxdwsl, a.zyh, c.ypCode,a.yzId, a.zxId, a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz,
	CASE when a.yzxz = '1' then 'Tạm thời' when a.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc,
    CASE WHEN a.yzxz = '1' THEN lsyz.yzh WHEN a.yzxz = '2' THEN cqyz.yzh ELSE NULL END AS yzh,
	yp.ycmc,(case when cqyz.yzzt=4 or lsyz.yzzt=4 then '[Dừng]'+yp.ypmc else yp.ypmc end) ypmc, ypsx.ypgg,c.CreatorCode pyry, c.CreateTime pyrq, RTRIM(LTRIM(c.ph)) ph, RTRIM(LTRIM(c.pc)) pc,a.ts,ypsx.gjybdm
	FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
	INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.v_s_xt_yp yp ON yp.ypCode=a.ypCode AND yp.OrganizeId=a.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=a.OrganizeId
    left join [Newtouch_CIS].dbo.zy_cqyz cqyz on a.yzId=cqyz.Id and a.OrganizeId=cqyz.OrganizeId and a.yzxz = '2'
    left join [Newtouch_CIS].dbo.zy_lsyz lsyz on a.yzId=lsyz.Id and a.OrganizeId=lsyz.OrganizeId and a.yzxz = '1'
	WHERE a.organizeId=@OrganizeId
	AND a.fybz='1'
	AND ISNULL(c.zh,'')=ISNULL(a.zh,'')
	AND a.fyyf=@yfbmCode  
 ";
            if (req.yzxz != "1" && req.yzxz != "2")
            {
                //req.yzxz = (int)Yzxz.Cq + "," + (int)Yzxz.Ls ;
                sql += "";
            }
            else{
                sql += " and a.yzxz ='"+ req.yzxz + "'";
            }
            sql += @" AND a.CreateTime BETWEEN @kssj AND @jssj
    AND ISNULL(a.patientName, '') LIKE '%' + @patientName + '%'
    AND ISNULL(yp.ypmc, '') LIKE '%' + @ypmc + '%'
    AND ISNULL(a.cw, '') LIKE '%' + @cw + '%'
    AND(a.yzId = @yzId OR '' = @yzId)
    AND c.zyypxxId = a.Id
    GROUP BY lsyz.yzh,cqyz.yzh, a.yzId,c.ypCode, a.zyh, a.zxId, a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz, yp.ycmc, yp.ypmc, ypsx.ypgg,c.CreatorCode, c.CreateTime, c.ph, c.pc, a.ts,cqyz.yzzt,lsyz.yzzt,ypsx.gjybdm
) o
ORDER BY o.yzh, o.zxId, o.ypmc";
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", req.yzId??""),
                new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<YPFYPatientInfoVO>(sql, param);
        }

        /// <summary>
        /// Xóa thông tin y lệnh
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string DeleteYzxx(string yzId, string zxId, string organizeId, string userCode)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var yzxxList = _yzYpyzxxRepo.SelectDataByYzId(yzId, zxId, organizeId);
                    if (yzxxList == null || yzxxList.Count == 0) return "";

                    foreach (var yzxx in yzxxList)
                    {
                        if (new[] { "2", "3" }.Contains(yzxx.fybz)) throw new Exception("Y lệnh đã phát thuốc hoặc trả thuốc không cho phép xóa");
                        var zyphList = _zyYpyzzxphRepo.SelectZyphList(yzId, zxId, yzxx.ypCode, organizeId);
                        if (zyphList != null && zyphList.Count > 0)
                        {
                            zyphList.ForEach(p =>
                            {
                                var param = new CancelArrangement
                                {
                                    yzId = yzId,
                                    zxId = zxId,
                                    OrganizeId = organizeId,
                                    pc = p.pc,
                                    ph = p.ph,
                                    userCode = userCode,
                                    yfbmCode = p.fyyf,
                                    ypCode = yzxx.ypCode
                                };
                                var tmpResult = _dispensingDmnService.HospitalizationCancelArrangement(param);
                                if (!string.IsNullOrWhiteSpace(tmpResult)) throw new Exception(tmpResult);
                            });
                        }
                        yzxx.zt = "0";
                        _yzYpyzxxRepo.Update(yzxx);
                        //_yzYpyzxxRepo.Delete(yzxx);
                    }

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException != null ? e.InnerException.Message : "");
            }
        }
        #endregion

        #region  Lấy thông tin dữ liệu có thể thực hiện thao tác trả thuốc bằng cách tra chuỗi bảng chi tiết trả thuốc và bảng số lô y lệnh

        /// <summary>
        /// Lấy bảng chi tiết trả thuốc，Bảng y lệnh liên kếtID
        /// </summary>
        /// <returns></returns>
        public List<ModelZyTyYszyzIdInfoVO> YpTyyszyzidList()
        {
            string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            string sql = @"select DISTINCT yszyzId from NewtouchHIS_PDS..zy_yptymx  where OrganizeId=@OrganizeId and tybz=1";

            return this.FindList<ModelZyTyYszyzIdInfoVO>(sql, new SugarParameter[] {
                new SugarParameter("@OrganizeId", OrganizeId ?? "")
            });
        }
        /// <summary>
        /// Lấy thông tin chi tiết trả thuốc
        /// </summary>
        /// <returns></returns>
        public List<ModelBQBRYZZXTYInfoVO> YpTymxxxList(List<ModelZyTyYszyzIdInfoVO> list)
        {//yszyzId = "", zxId = "", 
            string strstu = "", StrSQL = "";
            if (list.Count < 1)
            {
                return new List<ModelBQBRYZZXTYInfoVO>();
            }

            foreach (ModelZyTyYszyzIdInfoVO item in list)
            {
                //yszyzId += strstu + item.yszyzId;
                //zxId += strstu + item.zxId;
                StrSQL += strstu + " select yszyzId=" + item.yszyzId + ",zxId=" + item.zxId;
                strstu = " union all ";
            }

            string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            string sql = @" select t.yszyzId,t.tdrq,t.ypCode,t.tysl,t.tymxId,tybz,t.tyjlrq,t.tyrq,t.tyxh,t.tyry,p.Ph ypph,p.Pc yppc"
                    + " ,p.sl,t.tysqczyh from (" + StrSQL + ")y join  NewtouchHIS_PDS..zy_yptymx t on y.yszyzId=t.yszyzId  and t.tybz = '1'"
                    + " join NewtouchHIS_PDS..zy_ypyzzxph p on y.zxId=p.zxId  "
                    + " where t.OrganizeId =@OrganizeId  ";
            return this.FindList<ModelBQBRYZZXTYInfoVO>(sql, new SugarParameter[] {
                new SugarParameter("@OrganizeId", OrganizeId ?? "")
            });
        }

        /// <summary>
        /// Trước khi trả thuốc kiểm tra có phù hợp điều kiện không
        /// </summary>
        /// <param name="list"></param>
        /// <returns>0:Không có thông tin phát thuốc，1：Có thể trả thuốc，2：Không xin trả thuốc，3：Đã trả rồi</returns>
        public int IsTYHasOperated(List<ModelBQBRYZZXTYInfoVO> list)
        {
            StringBuilder strSql = new StringBuilder("select fybz,sqtybz from NewtouchHIS_PDS.dbo.zy_ypyzxx");
            var parms = new List<SugarParameter>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    strSql.Append(" where 1=0");
                }
                strSql.Append(string.Format(" or Id = @Id{0}", i));
                parms.Add(new SugarParameter(string.Format("@Id{0}", i), list[i].Id));
            }
            List<YPFYPatientInfoVO> listFind = this.FindList<YPFYPatientInfoVO>(strSql.ToString(), parms.ToArray());
            if (listFind != null && listFind.Count > 0)
            {
                foreach (var Row in listFind)
                {
                    string FYStatus = Row.fybz == null ? "" : Row.fybz.ToString();
                    string sqtybz = Row.sqtyzt == null ? "" : Row.sqtyzt.ToString();
                    if (FYStatus.Equals(((int)EnumFybz.Yt).ToString()))
                    {
                        return 3;
                    }
                    else if (FYStatus.Equals(((int)EnumFybz.Yf).ToString()) && sqtybz.Equals("0"))
                    {
                        return 2;
                    }
                }
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Thực hiện y lệnh nội trú thao tác trả thuốc
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type">Trạng thái trả thuốc</param>
        /// <returns></returns>
        public string ZYBQTYOperate(List<ModelBQBRYZZXTYInfoVO> list, string type,string OrganizeId)
        {
            if (list.Count < 1)
            {
                return "Dữ liệu gửi trống！";
            }

            int flag = IsTYHasOperated(list);
            if (flag == 0)
            {
                return "Không thể truy vấn thông tin phát thuốc này";
            }
            if (flag == 2)
            {
                return "Không khởi xướng đơn xin trả thuốc，Cấm trả thuốc";
            }
            if (flag == 3)
            {
                return "Đã trả thuốc，Cấm thao tác trùng lặp";
            }
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    foreach (var item in list)
                    {
                        var strSql = new StringBuilder();
                        //Sửa trạng thái phát thuốcLastModiFierCode
                        strSql.Append(@"UPDATE NewtouchHIS_PDS.dbo.zy_ypyzxx
                                  SET       fybz = @fybz,
                                            LastModiFierCode = @user,
                                            LastModifyTime = @time
                                  WHERE     Id = @Id;");
                        var paraList = new SugarParameter[]
                        {
                        new SugarParameter("@fybz", ((int)EnumFybz.Yt).ToString()),
                        new SugarParameter("@user", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@time", DateTime.Now),
                        new SugarParameter("@Id", item.Id)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //Thêm bản ghi thao tác trả thuốc
                        strSql.Clear();
                        strSql.Append(@"insert into NewtouchHIS_PDS.dbo.zy_ypyzczjl
                                  (ypyzxxId,operateType,ypCode,sl,LastModifyTime,LastModifierCode)
                                  values (@ypyzxxId,@operateType,@ypCode,@sl,@LastModifyTime,@LastModifierCode);");
                        paraList = new SugarParameter[]
                        {
                        new SugarParameter("@ypyzxxId", item.Id),
                        new SugarParameter("@operateType", ((int)EnumOperateType.Ty).ToString()),
                        new SugarParameter("@ypCode", item.ypCode),
                        new SugarParameter("@sl", item.tysl),
                        new SugarParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SugarParameter("@LastModifyTime", DateTime.Now)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);

                        //Lượng trả thuốc trả về tồn kho
                        strSql.Clear();
                        strSql.Append(@"update NewtouchHIS_PDS.dbo.xt_yp_kcxx
                                  SET	kcsl=kcsl+@tysl,
						                LastModifyTime=GETDATE(),
						                LastModifierCode=@CreatorCode
				                WHERE	pc=@pc
						                AND ph=@ph
						                AND ypdm=@ypCode
						                AND OrganizeId=@OrganizeId
                                        AND yfbmCode=@yfbmCode;");
                        paraList = new SugarParameter[]
                        {
                        new SugarParameter("@pc", item.yppc),
                        new SugarParameter("@ph", item.ypph),
                        new SugarParameter("@ypCode", item.ypCode),
                        new SugarParameter("@tysl", item.tysl),
                        new SugarParameter("@OrganizeId", item.OrganizeId),
                        new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                        new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                    }
                    db.Commit();
                }
                var zyhArr = list.Select(p => p.zyh).Distinct().ToArray();
                string zyhs = "";
                zyhs = string.Join(",", zyhArr);
                _dispensingDmnService.SyncPatFee(OrganizeId, zyhs, 0);
                _dispensingDmnService.Updatezy_brxxexpand(OrganizeId, zyhs);
                return "Trả thuốc thành công";
            }
            catch
            {
                return "Trả thuốc thất bại";
            }
        }

        /// <summary>
        /// Lấy bệnh nhân trả thuốc nội trú
        /// </summary>
        /// <param name="zybrbq"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<YPFYPatientInfoVO> GetTyPatient(string zybrbq, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT yz.zyh,patientName,cw.BedNo cw 
FROM NewtouchHIS_PDS.dbo.zy_ypyzxx yz (NOLOCK)
LEFT JOIN [Newtouch_CIS].[dbo].[zy_cwsyjlk] cw on yz.zyh=cw.zyh and yz.OrganizeId=cw.OrganizeId and cw.zt =1
WHERE fybz in('2','3') 
AND sqtybz = '1' 
AND bqCode = @bqCode
AND yz.OrganizeId=@OrganizeId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@bqCode", zybrbq),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return FindList<YPFYPatientInfoVO>(sql, param);
        }
        #endregion

        #region Phát thuốc nội trú
        /// <summary>
        /// Lấy khu bệnh cần phát thuốc（Khử trùng lặp）
        /// </summary>
        /// <returns></returns>
        public IList<ZYFYBQModelVO> GetFyBq(string orgId)
        {
            return FindList<ZYFYBQModelVO>(@"select * from [NewtouchHIS_Base]..V_S_xt_bq(nolock) where OrganizeId = @orgId", new SugarParameter[] { new SugarParameter("@orgId", orgId) });
        }

        /// <summary>
        /// Lấy bệnh nhân trả thuốc và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<YPFYPatientInfoVO> GetDispensePatientInfo(string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT yz.zyh,yz.patientName, cw.BedNo cw,yz.bqCode, xtbq.bqmc
FROM dbo.zy_ypyzxx(NOLOCK) yz 
INNER JOIN dbo.zy_ypyzzxph(NOLOCK) yzph ON yzph.ypCode=yz.ypCode AND yzph.yzId=yz.yzId AND yzph.zxId=yz.zxId AND yzph.OrganizeId=yz.OrganizeId AND yzph.gjzt='0' AND yzph.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_bq xtbq ON xtbq.bqCode=yz.bqCode AND xtbq.OrganizeId=yz.OrganizeId AND xtbq.zt='1'
INNER JOIN [Newtouch_CIS].[dbo].[zy_cwsyjlk] cw on yz.zyh=cw.zyh and yz.OrganizeId=cw.OrganizeId and cw.zt =1
WHERE yz.OrganizeId=@OrganizeId
AND yz.fyyf=@yfbmCode
AND yz.fybz='1'
AND yz.Id=yzph.zyypxxId
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FindList<YPFYPatientInfoVO>(sql, param);
        }

        /// <summary>
        /// Lấy bệnh nhân cần phát thuốc
        /// </summary>
        /// <returns></returns>
        public IList<YPFYPatientInfoVO> GetFyPatient(string zybrbq)
        {
            return FindList<YPFYPatientInfoVO>(@"select distinct zyh,patientName,cw from NewtouchHIS_PDS.dbo.zy_ypyzxx where fybz in(1,2) and bqCode = @bqCode", new SugarParameter[] { new SugarParameter("@bqCode", zybrbq) });
        }

        /// <summary>
        /// Chèn đơn xin trả thuốc
        /// </summary>
        /// <param name="applyBillDetail"></param>
        /// <param name="applyBill"></param>
        /// <returns></returns>
        public string InsertReturnDispensingMedicine(List<ZyReturnDrugApplyBillDetailEntity> applyBillDetail, List<ZyReturnDrugApplyBillEntity> applyBill)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    if (applyBill.Count == 0 || applyBillDetail.Count == 0) return "";
                    db.Insert(applyBill);
                    db.Insert(applyBillDetail);
                    applyBill.ForEach(p =>
                    {
                        const string sql = @"
UPDATE dbo.zy_ypyzxx SET sqtybz='1', LastModiFierCode=@userCode, LastModifyTime=GETDATE() 
WHERE yzId=@yzId AND OrganizeId=@OrganizeId 
";
                        var param = new SugarParameter[]
                        {
                            new SugarParameter("@yzId", p.yzId),
                            new SugarParameter("@OrganizeId", p.OrganizeId),
                            new SugarParameter("@userCode", p.CreatorCode)
                        };
                        db.ExecuteSqlCommand(sql, param);
                    });
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                LogCore.Error("InsertReturnDispensingMedicine error", e, string.Format("Ngoại lệ chèn đơn xin trả thuốc applyBillDetail：{0} \n  applyBill：{1}", applyBillDetail.ToJson(), applyBill.ToJson()));
                return e.Message;
            }
        }

        /// <summary>
        /// Lấy bệnh nhân trả thuốc và khu bệnh
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<YPFYPatientInfoVO> GetReturnDispensePatientInfo(string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT yz.zyh,yz.patientName, cw.BedNo cw,yz.bqCode, xtbq.bqmc
FROM dbo.zy_returnDrugApplyBill(NOLOCK) rdb
INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdbd.rabId=rdb.Id AND rdbd.zt='1' 
INNER JOIN dbo.zy_ypyzxx(NOLOCK) yz ON yz.yzId=rdb.yzId AND yz.OrganizeId=rdb.OrganizeId AND yz.ypCode=rdbd.ypCode 
INNER JOIN [Newtouch_CIS].[dbo].[zy_cwsyjlk] cw on yz.zyh=cw.zyh and yz.OrganizeId=cw.OrganizeId and cw.zt =1
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_bq xtbq ON xtbq.bqCode=yz.bqCode AND xtbq.OrganizeId=rdb.OrganizeId AND xtbq.zt='1'
WHERE rdb.OrganizeId=@OrganizeId
AND rdb.ProcessState='0'
AND rdb.zt='1'
AND yz.fyyf=@yfbmCode
AND yz.fybz IN ('2','3')
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FindList<YPFYPatientInfoVO>(sql, param);
        }

        /// <summary>
        /// Lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetail(HospitalizationReturnDrugParam req)
        {
            const string sql = @"
SELECT r.*, CONCAT(r.dj, '元/', r.bmdw) djStr, CONVERT(NUMERIC(11,2),ISNULL(r.zxdwsl*r.dj/r.zhyz,0)) je
FROM (
	SELECT d.patientName, d.zyh, d.cw, d.yzId, d.zxId, d.ypCode, yp.ypmc, ypsx.ypgg,ISNULL(CONVERT(VARCHAR(50),d.zh),'') zh , CASE when d.yzxz = '1' then 'Tạm thời' when d.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc
	,yp.ycmc, CONVERT(INT,ISNULL(d.zxdwsl/d.zhyz,0)) sl, CONVERT(INT,d.zxdwsl) zxdwsl, dbo.f_getyfbmDw(@yfbmCode, d.ypCode, @OrganizeId) bmdw
	,dbo.f_getYfbmYpComplexYpSlandDw(d.zxdwsl, @yfbmCode, d.ypCode, @OrganizeId) slStr, CONCAT(d.yl,d.yldw) ylStr
	,d.dj, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, d.zhyz, rdb.applyNo, rdbd.Id rdbdId
	FROM (
		SELECT SUM(s.zxdwsl) zxdwsl, s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.zyh, s.fybz, s.zhyz
		,s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz
		FROM (
			SELECT c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz 
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
            AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
			AND a.Id=c.zyypxxId
			UNION ALL
			SELECT -1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.yzId=a.yzId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
		) s
		GROUP BY s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz, s.zyh, s.fybz, s.zhyz
	) d
	INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
	INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode AND rdbd.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=d.ypCode AND yp.OrganizeId=rdb.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
	WHERE rdb.ProcessState='0'
	AND rdb.CreateTime BETWEEN @kssj AND @jssj
	AND ISNULL(d.patientName, '') LIKE '%'+@patientName+'%'
	AND ISNULL(yp.ypmc, '') LIKE '%'+@ypmc+'%'
	AND ISNULL(d.cw, '') LIKE '%'+@cw+'%'
) r
ORDER BY r.applyNo, r.ypmc
 ";
            var param = new SugarParameter[]
            {
               new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@fybz", req.fybz??""),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<HospitalizationReturnDispenseDetail>(sql, param);
        }

        /// <summary>
        /// Lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatch(HospitalizationReturnDrugParam req)
        {
            const string sql = @"
SELECT r.*, CONCAT(r.dj, '元/', r.bmdw) djStr, CONVERT(NUMERIC(11,2),ISNULL(r.zxdwsl*r.dj/r.zhyz,0)) je
FROM (
	SELECT d.patientName, d.zyh, d.cw, d.yzId, d.zxId, d.ypCode, yp.ypmc, ypsx.ypgg,ISNULL(CONVERT(VARCHAR(50),d.zh),'') zh 
	,CASE when d.yzxz = '1' then 'Tạm thời' when d.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc
	,yp.ycmc, CONVERT(INT,ISNULL(d.zxdwsl/d.zhyz,0)) sl, CONVERT(INT,d.zxdwsl) zxdwsl, dbo.f_getyfbmDw(@yfbmCode, d.ypCode, @OrganizeId) bmdw
	,dbo.f_getYfbmYpComplexYpSlandDw(d.zxdwsl, @yfbmCode, d.ypCode, @OrganizeId) slStr, CONCAT(d.yl,d.yldw) ylStr
	,d.dj, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, rdbd.zhyz, rdb.applyNo, rdbd.Id rdbdId
	,rdbd.tysl ,dbo.f_getYfbmYpComplexYpSlandDw(rdbd.tysl*rdbd.zhyz, @yfbmCode, d.ypCode,@OrganizeId) tyslStr
	FROM (
		SELECT SUM(s.zxdwsl) zxdwsl, MAX(s.pc) pc, MAX(s.ph) ph, s.ypCode, s.yzId, MAX(s.zxId) zxId, s.zyh, s.fybz, s.zhyz
		,s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz,s.lyxh
		FROM (
			SELECT a.lyxh,c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz 
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
			AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
			AND a.Id=c.zyypxxId
			UNION ALL
			SELECT a.lyxh,-1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.yzId=a.yzId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
		) s
		GROUP BY s.ypCode, s.yzId,s.zxId, s.lyxh, s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz, s.zyh, s.fybz, s.zhyz
	) d
	INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
	INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode AND rdbd.zt='1'  and rdbd.lyxh=d.lyxh
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=d.ypCode AND yp.OrganizeId=rdb.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
	WHERE rdb.ProcessState='0'
	AND rdb.CreateTime BETWEEN @kssj AND @jssj
	AND ISNULL(d.patientName, '') LIKE '%'+@patientName+'%'
	AND ISNULL(yp.ypmc, '') LIKE '%'+@ypmc+'%'
	AND ISNULL(d.cw, '') LIKE '%'+@cw+'%'
) r
ORDER BY r.applyNo, r.ypmc
 ";
            var param = new SugarParameter[]
            {
               new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@fybz", req.fybz??""),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<HospitalizationReturnDispenseDetail>(sql, param);
        }

        /// <summary>
        /// Lấy chi tiết trả thuốc
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetail(string applyNo, string yzId, string zxId, string ypCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT d.yzId, d.zxId, d.ypCode, CONVERT(INT, ISNULL(d.zxdwsl,0)) zxdwsl, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, d.zhyz, rdb.applyNo, rdbd.Id rdbdId
FROM (
	SELECT SUM(s.zxdwsl) zxdwsl, s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.zhyz
	FROM (
		SELECT c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zhyz 
		FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
		INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.yzId=a.yzId AND c.zxId=a.zxId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
		WHERE a.organizeId=@OrganizeId
		AND a.fyyf=@yfbmCode
		AND a.ypCode=@ypCode
		AND a.yzId=@yzId
		AND a.zxId=@zxId
		UNION ALL
		SELECT -1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zhyz
		FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
		INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
		WHERE a.organizeId=@OrganizeId
		AND a.fyyf=@yfbmCode
		AND a.ypCode=@ypCode
		AND a.yzId=@yzId
		AND a.zxId=@zxId
	) s
	GROUP BY s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.zhyz
) d
INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode AND rdbd.zt='1'
WHERE rdb.applyNo=@applyNo
 ";
            var param = new SugarParameter[]
            {
                new SugarParameter("@applyNo", applyNo),
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.SqlQuery<HospitalizationReturnDispenseDetail>(sql, param).ToList();
        }

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicine(HospitalizationReturnDrugParem rd)
        {
            try
            {
                using (var db = new Newtouch.Infrastructure.EFDbTransaction().BeginTrans())
                {
                    var rdbRepo = new ZyReturnDrugApplyBillRepo(new DefaultDatabaseFactory());
                    if (rdbRepo.UpdateProcessStateDoingWithTrans(rd.applyNo, rd.userCode, rd.organizeId, db) <= 0)
                    {
                        return string.Format("Đơn xin trả thuốc【{0}】Đang xử lý，Vui lòng không gửi trùng lặp！", rd.applyNo);
                    }

                    var zyyzxxRepo = new ZyYpyzzxRepo(new DefaultDatabaseFactory());
                    var yzxxRepo = new ZyYpyzzxRepo(new DefaultDatabaseFactory());
                    var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    foreach (var tyParam in rd.rpInfo)
                    {
                        if (tyParam.executeBatchDetail == null || tyParam.executeBatchDetail.Count == 0) return string.Format("Đơn trả thuốc【{0}】Không tìm thấy thông tin lô thực hiện y lệnh！", rd.applyNo);
                        var yzxx = yzxxRepo.SelectDataByYzId(tyParam.yzId, rd.organizeId);
                        if (yzxx == null || yzxx.Count == 0) return string.Format("Đơn trả thuốc【{0}】Không tìm thấy thông tin y lệnh！", rd.applyNo);

                        foreach (var zxpc in tyParam.executeBatchDetail)
                        {
                            //Sửa cờ phát thuốc
                            zyyzxxRepo.UpdateFybzTy(tyParam.yzId, zxpc.zxId, rd.userCode, rd.organizeId, db);
                            var ypyzxxid= yzxxRepo.SelectDataByYzId(tyParam.yzId,zxpc.zxId, rd.organizeId);
                            zxpc.tyRpDetail.ForEach(yp =>
                            {
                                var tyxx = SelectReturnDrugDetail(rd.applyNo, tyParam.yzId, zxpc.zxId, yp.ypCode, rd.yfbmCode, rd.organizeId);
                                if (tyxx == null || tyxx.Count == 0) throw new Exception(string.Format("【{0}】Không tìm thấy thông tin xin trả thuốc！", yp.ypmc));

                                //Chèn bản ghi trả thuốc
                                var czjl = new ZyYpyzczjlEntity
                                {
                                    OrganizeId = rd.organizeId,
                                    bz = "",
                                    CreatorCode = rd.userCode,
                                    CreateTime = DateTime.Now,
                                    LastModifierCode = "",
                                    LastModifyTime = null,
                                    operateType = "2",
                                    sl = yp.drugBatch.Sum(i => i.sl) * yp.zhyz,
                                    ypCode = yp.ypCode,
                                    yzId = tyParam.yzId,
                                    zxId = zxpc.zxId,
                                    ypyzxxId = ypyzxxid.FirstOrDefault().Id,//yzxx.FirstOrDefault().Id
                                    zsm = yp.zsm,
                                    sfcl = yp.sfcl
                                };
                                db.Insert(czjl);

                                yp.drugBatch.ForEach(batch =>
                                {

                                    //Trả tồn kho
                                    kcxxRepo.UpdateKcslWithTrans(batch.sl * yp.zhyz, batch.pc, batch.ph, yp.ypCode, rd.yfbmCode, rd.organizeId, rd.userCode, db);
                                    var tymxEntity = new ZyTymxEntity
                                    {
                                        OrganizeId = rd.organizeId,
                                        CreateTime = DateTime.Now,
                                        CreatorCode = rd.userCode,
                                        zt = "1",
                                        pc = batch.pc,
                                        ph = batch.ph,
                                        LastModifierCode = "",
                                        LastModifyTime = null,
                                        Remark = "",
                                        yzId = tyParam.yzId,
                                        zxId = zxpc.zxId,
                                        returnDrugBillNo = rd.returnDrugBillNo,
                                        ypCode = yp.ypCode,
                                        sl = batch.sl
                                    };
                                    db.Insert(tymxEntity);
                                });
                            });
                        }
                    }
                    rdbRepo.UpdateProcessStateCompleteWithTrans(rd.applyNo, rd.userCode, rd.organizeId, db);

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }
        #endregion


        #region  Nội trú phát/trả/Tất cả Truy vấn thuốc

        /// <summary>
        /// Phát nội trú、Danh sách truy vấn trả thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<HospitalizationDispenseDetail> GetZyDrugQueryList(Pagination pagination, QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
SELECT s.Id,s.yzId,s.zxId,s.patientName,s.pc,s.ypCode, s.ypmc, s.cw,s.czlx, s.slStr	,s.jxmc, s.ypgg, s.pcmc, ISNULL(CONVERT(VARCHAR(50),s.zh),'') zh, s.yl, s.yldw
,s.ylStr,s.yzxzmc,s.ycmc,s.zlff,s.djStr,s.je,s.CreateTime,s.CreatorCode,zyh
FROM (
	SELECT a.Id,a.yzId, a.zxId, c.patientName,a.pc,a.ypCode, b.ypmc, c.cw,'Phát thuốc' czlx
	, dbo.f_getYfbmYpComplexYpSlandDw(SUM(a.sl), @yfbmCode, a.ypCode, @OrganizeId) slStr
	,e.jxmc, ypsx.ypgg, c.pcmc, ISNULL(CONVERT(VARCHAR(50),c.zh),'') zh, c.yl, c.yldw, CONCAT(c.yl,c.yldw) ylStr
	,(case when c.yzxz = '1' then 'Tạm thời' when c.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc
	,b.ycmc,c.zlff,CONCAT(c.dj,'元/',b.djdw) djStr,CONVERT(NUMERIC(11,2),ISNULL(SUM(c.dj/c.zhyz*a.sl),0)) je
	,a.CreateTime,a.CreatorCode,c.bqCode,c.zyh
	FROM NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) a 
	INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) c on c.ypCode=a.ypCode AND c.OrganizeId=a.OrganizeId AND c.zxId=a.zxId AND c.yzId=a.yzId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b on a.ypCode = b.ypCode and b.organizeId = c.organizeId  
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=b.ypId AND ypsx.OrganizeId=a.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = b.jx AND e.zt='1' 
	where a.OrganizeId=@OrganizeId and c.fybz in(@fy,@ty)
	AND a.CreateTime BETWEEN @kssj AND @jssj
	AND a.gjzt='0'
	AND a.zt='1'
	AND a.fyyf=@yfbmCode
    AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
	AND a.zyypxxId=c.Id
	GROUP BY a.Id,c.zyh,a.yzId, a.zxId,a.ypCode, c.patientName,a.pc, b.ypmc, c.cw,e.jxmc, ypsx.ypgg, c.pcmc, c.zh, c.yl, c.yldw,b.ycmc,c.zlff,a.CreateTime,a.CreatorCode, c.yzxz, c.dj, b.djdw ,c.bqCode
	UNION ALL
	SELECT a.Id,a.yzId, a.zxId, c.patientName,a.pc,a.ypCode, b.ypmc, c.cw,'Trả thuốc' czlx
	,dbo.f_getYfbmYpComplexYpSlandDw(SUM(a.sl), @yfbmCode, a.ypCode, @OrganizeId) slStr
	,e.jxmc, ypsx.ypgg, c.pcmc, ISNULL(CONVERT(VARCHAR(50),c.zh),'') zh, c.yl, c.yldw, CONCAT(c.yl,c.yldw) ylStr
	,(case when c.yzxz = '1' then 'Tạm thời' when c.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc
	,b.ycmc,c.zlff,CONCAT(c.dj,'元/',b.djdw) djStr,CONVERT(NUMERIC(11,2),ISNULL(SUM(c.dj/c.zhyz*a.sl),0)) je
	,a.CreateTime,a.CreatorCode,c.bqCode,c.zyh
	FROM NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) a 
	INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) c on a.ypCode = c.ypCode AND c.OrganizeId=a.OrganizeId AND c.zxId=a.zxId AND c.yzId=a.yzId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b on a.ypCode = b.ypCode and b.organizeId = c.organizeId  
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=b.ypId AND ypsx.OrganizeId=a.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = b.jx AND e.zt='1' 
	where a.OrganizeId=@OrganizeId and c.fybz in(@fy,@ty)
	AND a.CreateTime BETWEEN @kssj AND @jssj
	AND a.zt='1'
	AND c.fyyf=@yfbmCode
	GROUP BY a.Id,c.zyh,a.yzId, a.zxId,a.ypCode, c.patientName,a.pc, b.ypmc, c.cw,e.jxmc, ypsx.ypgg, c.pcmc, c.zh, c.yl, c.yldw,b.ycmc,c.zlff,a.CreateTime,a.CreatorCode, c.yzxz, c.dj, b.djdw, c.bqCode
) s
WHERE (s.bqCode=@bqCode OR ''=ISNULL(@bqCode,'')) ");
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@operateType", req.operateType),
                new SugarParameter("@bqCode", req.bqCode),
                new SugarParameter("@fy", (int)EnumFybz.Yf),
                new SugarParameter("@ty", (int)EnumFybz.Yt)
            };
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND s.patientName LIKE '%'+@patientName+'%' ");
                parms.Add(new SugarParameter("@patientName", req.patientName));
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND s.ypmc LIKE '%'+@ypmc+'%' ");
                parms.Add(new SugarParameter("@ypmc", req.ypmc));
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND s.pc LIKE '%'+@pc+'%'");
                parms.Add(new SugarParameter("@pc", req.pc));
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND s.cw LIKE '%'+@cw+'%' ");
                parms.Add(new SugarParameter("@cw", req.cw));
            }
            return QueryWithPage<HospitalizationDispenseDetail>(strSql.ToString(), pagination, parms.ToArray());
        }

        /// <summary>
        /// Danh sách truy vấn phát thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<HospitalizationDispenseDetail> GetZyfyQueryList(Pagination pagination, QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
SELECT a.Id,a.yzId, a.zxId, c.patientName,a.pc,a.ypCode, b.ypmc, c.cw,'Phát thuốc' czlx
, dbo.f_getYfbmYpComplexYpSlandDw(SUM(a.sl), @yfbmCode, a.ypCode, @OrganizeId) slStr
,e.jxmc, ypsx.ypgg, c.pcmc, ISNULL(CONVERT(VARCHAR(50),c.zh),'') zh, c.yl, c.yldw, CONCAT(c.yl,c.yldw) ylStr
,(case when c.yzxz = '1' then 'Tạm thời' when c.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc
,b.ycmc,c.zlff,CONCAT(c.dj,'元/',b.djdw) djStr,CONVERT(NUMERIC(11,2),ISNULL(SUM(c.dj/c.zhyz*a.sl),0)) je
,a.CreateTime,a.CreatorCode ,c.zyh
FROM NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) a 
INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) c on c.ypCode=a.ypCode AND c.OrganizeId=a.OrganizeId AND c.zxId=a.zxId AND c.yzId=a.yzId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b on a.ypCode = b.ypCode and b.organizeId = c.organizeId  
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=b.ypId AND ypsx.OrganizeId=a.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = b.jx AND e.zt='1' 
where a.OrganizeId=@OrganizeId and c.fybz=@fy
AND a.CreateTime BETWEEN @kssj AND @jssj
AND a.gjzt='0'
AND a.zt='1' 
AND c.fyyf=@yfbmCode
AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
AND a.zyypxxId=c.Id
");
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@operateType", req.operateType),
                new SugarParameter("@fy", (int)EnumFybz.Yf),
            };
            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND c.bqCode=@bqCode ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND c.patientName LIKE '%'+@patientName+'%' ");
                parms.Add(new SugarParameter("@patientName", req.patientName));
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND b.ypmc LIKE '%'+@ypmc+'%' ");
                parms.Add(new SugarParameter("@ypmc", req.ypmc));
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND a.pc LIKE '%'+@pc+'%'");
                parms.Add(new SugarParameter("@pc", req.pc));
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND c.cw LIKE '%'+@cw+'%' ");
                parms.Add(new SugarParameter("@cw", req.cw));
            }

            strSql.AppendLine("GROUP BY c.zyh,a.Id,a.yzId, a.zxId,a.ypCode, c.patientName,a.pc, b.ypmc, c.cw,e.jxmc, ypsx.ypgg, c.pcmc, c.zh, c.yl, c.yldw,b.ycmc,c.zlff,a.CreateTime,a.CreatorCode, c.yzxz, c.dj, b.djdw  ");
            return QueryWithPage<HospitalizationDispenseDetail>(strSql.ToString(), pagination, parms.ToArray());
        }

        /// <summary>
        /// Danh sách truy vấn trả thuốc nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<HospitalizationDispenseDetail> GetZytyQueryList(Pagination pagination, QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
SELECT a.Id,a.yzId, a.zxId, c.patientName,a.pc,a.ypCode, b.ypmc, c.cw,'Trả thuốc' czlx
,dbo.f_getYfbmYpComplexYpSlandDw(SUM(a.sl), @yfbmCode, a.ypCode, @OrganizeId) slStr
,e.jxmc, ypsx.ypgg, c.pcmc, ISNULL(CONVERT(VARCHAR(50),c.zh),'') zh, c.yl, c.yldw, CONCAT(c.yl,c.yldw) ylStr
,(case when c.yzxz = '1' then 'Tạm thời' when c.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc
,b.ycmc,c.zlff,CONCAT(c.dj,'元/',b.djdw) djStr,CONVERT(NUMERIC(11,2),ISNULL(SUM(c.dj/c.zhyz*a.sl),0)) je
,a.CreateTime,a.CreatorCode ,c.zyh
FROM NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) a 
INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) c on a.ypCode = c.ypCode AND c.OrganizeId=a.OrganizeId AND c.zxId=a.zxId AND c.yzId=a.yzId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp b on a.ypCode = b.ypCode and b.organizeId = c.organizeId  
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=b.ypId AND ypsx.OrganizeId=a.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = b.jx AND e.zt='1' 
where a.OrganizeId=@OrganizeId and  c.fybz=@ty
AND a.CreateTime BETWEEN @kssj AND @jssj
AND a.zt='1'
AND c.fyyf=@yfbmCode
");
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@operateType", req.operateType),
                new SugarParameter("@ty", (int)EnumFybz.Yt),                
            };
            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND c.bqCode=@bqCode ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND c.patientName LIKE '%'+@patientName+'%' ");
                parms.Add(new SugarParameter("@patientName", req.patientName));
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND b.ypmc LIKE '%'+@ypmc+'%' ");
                parms.Add(new SugarParameter("@ypmc", req.ypmc));
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND a.pc LIKE '%'+@pc+'%'");
                parms.Add(new SugarParameter("@pc", req.pc));
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND c.cw LIKE '%'+@cw+'%' ");
                parms.Add(new SugarParameter("@cw", req.cw));
            }

            strSql.AppendLine("GROUP BY c.zyh,a.Id,a.yzId, a.zxId,a.ypCode, c.patientName,a.pc, b.ypmc, c.cw,e.jxmc, ypsx.ypgg, c.pcmc, c.zh, c.yl, c.yldw,b.ycmc,c.zlff,a.CreateTime,a.CreatorCode, c.yzxz, c.dj, b.djdw  ");
            return QueryWithPage<HospitalizationDispenseDetail>(strSql.ToString(), pagination, parms.ToArray());
        }

        #endregion

        #region Truy vấn bản ghi phát thuốc theo bản ghi thao tác y lệnh nội trú
        public IList<HospitalizationDispenseDetail> GetYzCzjlList(Pagination pagination, QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
select (case when a.operateType='1' then 'Phát thuốc' when a.operateType='2' then 'Trả thuốc' end) czlx,
b.zyh,b.patientName,b.cw,b.ypCode,c.ypmc,b.pcmc,ISNULL(CONVERT(VARCHAR(50),b.zh),'') zh, b.yl, b.yldw, CONCAT(b.yl,b.yldw) ylStr
,(case when b.yzxz = '1' then 'Tạm thời' when b.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc,b.zlff,c.ycmc,CONCAT(b.dj,'元/',c.djdw) djStr,
(case when a.operateType='1' then dbo.f_getYfbmYpComplexYpSlandDw(SUM(d.sl), @yfbmCode, b.ypCode, @OrganizeId) when a.operateType='2' then dbo.f_getYfbmYpComplexYpSlandDw(SUM(a.sl), @yfbmCode, b.ypCode, @OrganizeId) end) slStr,
(case when a.operateType='1' then CONVERT(NUMERIC(11,2),ISNULL(SUM(b.dj/b.zhyz*d.sl),0)) when a.operateType='2' then CONVERT(NUMERIC(11,2),ISNULL(SUM(b.dj/b.zhyz*a.sl),0)) end) je,
a.Id,a.yzId,a.zxId,a.CreateTime,a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg
from zy_ypyzczjl a with(nolock),NewtouchHIS_PDS.dbo.zy_ypyzxx b with(nolock)
left join NewtouchHIS_Base.dbo.V_S_xt_yp c with(nolock) on b.OrganizeId=c.OrganizeId and b.ypCode=c.ypCode
left join NewtouchHIS_PDS.dbo.zy_ypyzzxph d with(nolock) on b.OrganizeId=d.OrganizeId and b.id=d.zyypxxId and d.zt='1'
left JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = c.jx AND e.zt='1'
left JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=c.ypId AND ypsx.OrganizeId=c.OrganizeId
where a.ypyzxxId=b.Id and a.OrganizeId=b.OrganizeId and a.OrganizeId=@OrganizeId
and a.CreateTime BETWEEN @kssj AND @jssj
and d.fyyf=@yfbmCode
and ISNULL(b.zh,'')=ISNULL(d.zh,'')
");
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@ty", (int)EnumFybz.Yt),
            };
            if (!string.IsNullOrWhiteSpace(req.operateType))
            {
                strSql.AppendLine("and a.operateType=@operateType ");
                parms.Add(new SugarParameter("@operateType", req.operateType));
            }
            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND b.bqCode=@bqCode ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND b.patientName LIKE '%'+@patientName+'%' ");
                parms.Add(new SugarParameter("@patientName", req.patientName));
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND c.ypmc LIKE '%'+@ypmc+'%' ");
                parms.Add(new SugarParameter("@ypmc", req.ypmc));
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND b.pc LIKE '%'+@pc+'%'");
                parms.Add(new SugarParameter("@pc", req.pc));
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND b.cw LIKE '%'+@cw+'%' ");
                parms.Add(new SugarParameter("@cw", req.cw));
            }

            strSql.AppendLine(@"group by a.operateType,
b.zyh, b.patientName, b.cw, b.ypCode, c.ypmc, b.pcmc, b.yl, b.yldw, b.zh,
a.Id, a.yzId, a.zxId, a.ypCode, b.yzxz, b.zlff, c.ycmc, b.dj, c.djdw, a.CreateTime, a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg ");
            return QueryWithPage<HospitalizationDispenseDetail>(strSql.ToString(), pagination, parms.ToArray());
        }

        /// <summary>
        /// Trả thuốc nội trúV2
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicineV2(HospitalizationReturnDrugParem rd, string zytyapplyno)
        {
            try
            {
                using (var db = new Newtouch.Infrastructure.EFDbTransaction().BeginTrans())
                {
                    var rdbRepo = new ZyReturnDrugApplyBillRepo(new DefaultDatabaseFactory());
                    if (rdbRepo.UpdateProcessStateDoingWithTrans(rd.applyNo, rd.userCode, rd.organizeId, db) <= 0)
                    {
                        return string.Format("Đơn xin trả thuốc【{0}】Đang xử lý，Vui lòng không gửi trùng lặp！", rd.applyNo);
                    }

                    var zyyzxxRepo = new ZyYpyzzxRepo(new DefaultDatabaseFactory());
                    var yzxxRepo = new ZyYpyzzxRepo(new DefaultDatabaseFactory());
                    var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    foreach (var tyParam in rd.rpInfo)
                    {
                        if (tyParam.executeBatchDetail == null || tyParam.executeBatchDetail.Count == 0) return string.Format("Đơn trả thuốc【{0}】Không tìm thấy thông tin lô thực hiện y lệnh！", rd.applyNo);
                        var yzxx = yzxxRepo.SelectDataByYzId(tyParam.yzId, rd.organizeId);
                        if (yzxx == null || yzxx.Count == 0) return string.Format("Đơn trả thuốc【{0}】Không tìm thấy thông tin y lệnh！", rd.applyNo);

                        foreach (var zxpc in tyParam.executeBatchDetail)
                        {
                            //Sửa cờ phát thuốc
                            zyyzxxRepo.UpdateFybzTy(tyParam.yzId, zxpc.zxId, rd.userCode, rd.organizeId, db);
                            var ypyzxxid = yzxxRepo.SelectDataByYzId(tyParam.yzId, zxpc.zxId, rd.organizeId);
                            zxpc.tyRpDetail.ForEach(yp =>
                            {
                                var tyxx = SelectReturnDrugDetail(rd.applyNo, tyParam.yzId, zxpc.zxId, yp.ypCode, rd.yfbmCode, rd.organizeId);
                                if (tyxx == null || tyxx.Count == 0) throw new Exception(string.Format("【{0}】Không tìm thấy thông tin xin trả thuốc！", yp.ypmc));

                                //Chèn bản ghi trả thuốc
                                var czjl = new ZyYpyzczjlEntity
                                {
                                    OrganizeId = rd.organizeId,
                                    bz = "",
                                    CreatorCode = rd.userCode,
                                    CreateTime = DateTime.Now,
                                    LastModifierCode = "",
                                    LastModifyTime = null,
                                    operateType = "2",
                                    sl = yp.drugBatch.Sum(i => i.sl) * yp.zhyz,
                                    ypCode = yp.ypCode,
                                    yzId = tyParam.yzId,
                                    zxId = zxpc.zxId,
                                    ypyzxxId = ypyzxxid.FirstOrDefault().Id,
                                    zytyapplyno = zytyapplyno,
                                    zsm = yp.zsm,
                                    sfcl= yp.sfcl,
                                };
                                db.Insert(czjl);

                                yp.drugBatch.ForEach(batch =>
                                {

                                    //Trả tồn kho
                                    kcxxRepo.UpdateKcslWithTrans(batch.sl * yp.zhyz, batch.pc, batch.ph, yp.ypCode, rd.yfbmCode, rd.organizeId, rd.userCode, db);
                                    var tymxEntity = new ZyTymxEntity
                                    {
                                        OrganizeId = rd.organizeId,
                                        CreateTime = DateTime.Now,
                                        CreatorCode = rd.userCode,
                                        zt = "1",
                                        pc = batch.pc,
                                        ph = batch.ph,
                                        LastModifierCode = "",
                                        LastModifyTime = null,
                                        Remark = "",
                                        yzId = tyParam.yzId,
                                        zxId = zxpc.zxId,
                                        returnDrugBillNo = rd.returnDrugBillNo,
                                        ypCode = yp.ypCode,
                                        sl = batch.sl,
                                        zytyapplyno = zytyapplyno
                                    };
                                    db.Insert(tymxEntity);
                                });
                            });
                        }
                    }
                    rdbRepo.UpdateProcessStateCompleteWithTrans(rd.applyNo, rd.userCode, rd.organizeId, db);

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        public IList<HospitalizationDispenseDetail> GetYzCzjlListV2(Pagination pagination, QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
select (case when a.operateType='1' then 'Phát thuốc' when a.operateType='2' then 'Trả thuốc' end) czlx,
b.zyh,b.patientName,b.cw,b.ypCode,c.ypmc,b.pcmc,ISNULL(CONVERT(VARCHAR(50),b.zh),'') zh, b.yl, b.yldw, CONCAT(b.yl,b.yldw) ylStr
,(case when b.yzxz = '1' then 'Tạm thời' when b.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc,b.zlff,c.ycmc,CONCAT(b.dj,'元/',c.djdw) djStr,
(case when a.operateType='1' then dbo.f_getYfbmYpComplexYpSlandDwV2(SUM(d.sl), @yfbmCode, b.ypCode, @OrganizeId,'2') when a.operateType='2' then dbo.f_getYfbmYpComplexYpSlandDwV2(SUM(a.sl), @yfbmCode, b.ypCode, @OrganizeId,'2') end) slStr,
(case when a.operateType='1' then CONVERT(NUMERIC(11,2),ISNULL(SUM(b.dj/b.zhyz*d.sl),0)) when a.operateType='2' then CONVERT(NUMERIC(11,2),ISNULL(SUM(b.dj/b.zhyz*a.sl),0)) end) je,
a.Id,a.yzId,a.zxId,a.CreateTime,a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg,a.zytyapplyno,b.bqCode
from zy_ypyzczjl a with(nolock),NewtouchHIS_PDS.dbo.zy_ypyzxx b with(nolock)
left join NewtouchHIS_Base.dbo.V_S_xt_yp c with(nolock) on b.OrganizeId=c.OrganizeId and b.ypCode=c.ypCode
left join NewtouchHIS_PDS.dbo.zy_ypyzzxph d with(nolock) on b.OrganizeId=d.OrganizeId and b.id=d.zyypxxId and d.zt='1'
left JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = c.jx AND e.zt='1'
left JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=c.ypId AND ypsx.OrganizeId=c.OrganizeId
where a.ypyzxxId=b.Id and a.OrganizeId=b.OrganizeId and a.OrganizeId=@OrganizeId
and a.CreateTime BETWEEN @kssj AND @jssj
and d.fyyf=@yfbmCode
and ISNULL(b.zh,'')=ISNULL(d.zh,'') and a.operateType=1 
");
            if (!string.IsNullOrWhiteSpace(req.operateType))
            {
                strSql.AppendLine("and a.operateType=@operateType ");
            }
            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND b.bqCode=@bqCode ");
            }
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND b.patientName LIKE '%'+@patientName+'%' ");
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND c.ypmc LIKE '%'+@ypmc+'%' ");
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND b.pc LIKE '%'+@pc+'%'");
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND b.cw LIKE '%'+@cw+'%' ");
            }

            strSql.AppendLine(@" group by a.operateType,
b.zyh, b.patientName, b.cw, b.ypCode, c.ypmc, b.pcmc, b.yl, b.yldw, b.zh,
a.Id, a.yzId, a.zxId, a.ypCode, b.yzxz, b.zlff, c.ycmc, b.dj, c.djdw, a.CreateTime, a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg,a.zytyapplyno,b.bqCode ");

            strSql.AppendLine(@" union all ");

            strSql.AppendLine(@" 
            select 'Trả thuốc' as czlx,
	            s.zyh,s.patientName,s.cw,s.ypCode,s.ypmc,s.pcmc,s.zh, s.yl, s.yldw, CONCAT(s.yl,s.yldw) ylStr,
                (case when s.yzxz = '1' then 'Tạm thời' when s.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc,s.zlff,s.ycmc, CONCAT(s.dj,'元/',s.djdw) djStr,
	            dbo.f_getYfbmYpComplexYpSlandDwV2(SUM(s.sl),@yfbmCode, s.ypCode, @OrganizeId,'2') slStr,
	            CONVERT(NUMERIC(11,2),ISNULL(SUM(s.dj/s.zhyz*s.sl),0)) je,
	            s.Id,s.yzId,s.zxId,s.CreateTime,s.CreatorCode,s.pc,s.jxmc,s.ypgg,s.zytyapplyno,s.bqCode 
            from (
                select distinct a.operateType,b.zyh,b.patientName,b.cw,b.ypCode,c.ypmc,b.pcmc,ISNULL(CONVERT(VARCHAR(50),b.zh),'') zh, b.yl, b.yldw,
                b.zlff,c.ycmc,b.yzxz,c.djdw,
                a.sl, b.dj, b.zhyz, a.Id,a.yzId,a.zxId,a.CreateTime,a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg,a.zytyapplyno,b.bqCode
                from zy_ypyzczjl a with(nolock),NewtouchHIS_PDS.dbo.zy_ypyzxx b with(nolock)
	                left join NewtouchHIS_Base.dbo.V_S_xt_yp c with(nolock) on b.OrganizeId=c.OrganizeId and b.ypCode=c.ypCode
	                left join NewtouchHIS_PDS.dbo.zy_tymx d with(nolock) on b.OrganizeId=d.OrganizeId and d.zxId =b.zxId and d.yzId = b.yzId and d.zt='1'
	                left JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = c.jx AND e.zt='1'
	                left JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=c.ypId AND ypsx.OrganizeId=c.OrganizeId
                where a.ypyzxxId=b.Id and a.OrganizeId=b.OrganizeId and a.OrganizeId=@OrganizeId
	                and a.CreateTime BETWEEN @kssj AND @jssj
	                and b.fyyf=@yfbmCode
	                and a.operateType = 2 
	        ) s where 1=1
            ");

            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@ty", (int)EnumFybz.Yt),
            };

            if (!string.IsNullOrWhiteSpace(req.operateType))
            {
                strSql.AppendLine("and s.operateType=@operateType ");
                parms.Add(new SugarParameter("@operateType", req.operateType));
            }
            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND s.bqCode=@bqCode ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }
            if (!string.IsNullOrEmpty(req.patientName))
            {
                strSql.AppendLine("AND s.patientName LIKE '%'+@patientName+'%' ");
                parms.Add(new SugarParameter("@patientName", req.patientName));
            }
            if (!string.IsNullOrEmpty(req.ypmc))
            {
                strSql.AppendLine("AND s.ypmc LIKE '%'+@ypmc+'%' ");
                parms.Add(new SugarParameter("@ypmc", req.ypmc));
            }
            if (!string.IsNullOrEmpty(req.pc))
            {
                strSql.AppendLine("AND s.pc LIKE '%'+@pc+'%'");
                parms.Add(new SugarParameter("@pc", req.pc));
            }
            if (!string.IsNullOrEmpty(req.cw))
            {
                strSql.AppendLine("AND s.cw LIKE '%'+@cw+'%' ");
                parms.Add(new SugarParameter("@cw", req.cw));
            }

            strSql.AppendLine(@" group by s.operateType,
s.zyh, s.patientName, s.cw, s.ypCode, s.ypmc, s.pcmc, s.yl, s.yldw, s.zh,
s.Id, s.yzId, s.zxId, s.ypCode, s.yzxz, s.zlff, s.ycmc, s.dj, s.djdw, s.CreateTime, s.CreatorCode,s.pc,s.jxmc,s.ypgg,s.zytyapplyno,s.bqCode ");

            return QueryWithPage<HospitalizationDispenseDetail>(strSql.ToString(), pagination, parms.ToArray());


        }

        /// <summary>
        /// Lấy chi tiết trả thuốcV2
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatchV2(HospitalizationReturnDrugParam req)
        {
            const string sql = @"
SELECT r.*, CONCAT(r.dj, '元/', r.bmdw) djStr, CONVERT(NUMERIC(11,2),ISNULL(r.zxdwsl*r.dj/r.zhyz,0)) je
FROM (
	SELECT d.patientName, d.zyh, d.cw, d.yzId, d.zxId, d.ypCode, yp.ypmc, ypsx.ypgg,ISNULL(CONVERT(VARCHAR(50),d.zh),'') zh 
	,CASE when d.yzxz = '1' then 'Tạm thời' when d.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc
	,yp.ycmc, CONVERT(INT,ISNULL(d.zxdwsl/d.zhyz,0)) sl, CONVERT(INT,d.zxdwsl) zxdwsl, dbo.f_getyfbmDw(@yfbmCode, d.ypCode, @OrganizeId) bmdw
	,dbo.f_getYfbmYpComplexYpSlandDwV2(d.zxdwsl, @yfbmCode, d.ypCode, @OrganizeId,'2') slStr, CONCAT(d.yl,d.yldw) ylStr
	,d.dj, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, rdbd.zhyz, rdb.applyNo, rdbd.Id rdbdId
	,rdbd.tysl ,dbo.f_getYfbmYpComplexYpSlandDwV2(rdbd.tysl*rdbd.zhyz, @yfbmCode, d.ypCode,@OrganizeId,'2') tyslStr, rdb.zytyapplyno
	FROM (
		SELECT SUM(s.zxdwsl) zxdwsl, MAX(s.pc) pc, MAX(s.ph) ph, s.ypCode, s.yzId, MAX(s.zxId) zxId, s.zyh, s.fybz, s.zhyz
		,s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz,s.lyxh
		FROM (
			SELECT a.lyxh,c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz 
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz in (2,3)
			AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
			AND a.Id=c.zyypxxId
			UNION ALL
			SELECT a.lyxh,-1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.yzId=a.yzId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz in (2,3)
		) s
		GROUP BY s.ypCode, s.yzId,s.zxId, s.lyxh, s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz, s.zyh, s.fybz, s.zhyz
	) d
	INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
	INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode AND rdbd.zt='1'  and rdbd.lyxh=d.lyxh
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=d.ypCode AND yp.OrganizeId=rdb.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
	WHERE rdb.ProcessState='0' AND rdb.zytyapplyno=@zytyapplyno
	AND rdb.CreateTime BETWEEN @kssj AND @jssj
	AND ISNULL(d.patientName, '') LIKE '%'+@patientName+'%'
	AND ISNULL(yp.ypmc, '') LIKE '%'+@ypmc+'%'
	AND ISNULL(d.cw, '') LIKE '%'+@cw+'%'
) r
ORDER BY r.applyNo, r.ypmc
 ";
            var param = new SugarParameter[]
            {
               new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                //new SugarParameter("@fybz", req.fybz??""),
                new SugarParameter("@zytyapplyno", req.zytyapplyno??""),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<HospitalizationReturnDispenseDetail>(sql, param);
        }

        public List<ZyTyApplyNoVO> GetZyTyApplyNo(HospitalizationReturnDrugParam req)
        {
            const string sql = @"
SELECT distinct r.zytyapplyno, r.zyh
FROM (
	SELECT d.patientName, d.zyh, d.cw, d.yzId, d.zxId, d.ypCode, yp.ypmc, ypsx.ypgg,ISNULL(CONVERT(VARCHAR(50),d.zh),'') zh 
	,CASE when d.yzxz = '1' then 'Tạm thời' when d.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc
	,yp.ycmc, CONVERT(INT,ISNULL(d.zxdwsl/d.zhyz,0)) sl, CONVERT(INT,d.zxdwsl) zxdwsl, dbo.f_getyfbmDw(@yfbmCode, d.ypCode, @OrganizeId) bmdw
	,dbo.f_getYfbmYpComplexYpSlandDw(d.zxdwsl, @yfbmCode, d.ypCode, @OrganizeId) slStr, CONCAT(d.yl,d.yldw) ylStr
	,d.dj, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, rdbd.zhyz, rdb.applyNo, rdbd.Id rdbdId
	,rdbd.tysl ,dbo.f_getYfbmYpComplexYpSlandDw(rdbd.tysl*rdbd.zhyz, @yfbmCode, d.ypCode,@OrganizeId) tyslStr, rdb.zytyapplyno
	FROM (
		SELECT SUM(s.zxdwsl) zxdwsl, MAX(s.pc) pc, MAX(s.ph) ph, s.ypCode, s.yzId, MAX(s.zxId) zxId, s.zyh, s.fybz, s.zhyz
		,s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz,s.lyxh
		FROM (
			SELECT a.lyxh,c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz 
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz in (2,3)
			AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
			AND a.Id=c.zyypxxId
			UNION ALL
			SELECT a.lyxh,-1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.yzId=a.yzId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz in (2,3)
		) s
		GROUP BY s.ypCode, s.yzId,s.zxId, s.lyxh, s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz, s.zyh, s.fybz, s.zhyz
	) d
	INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
	INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode AND rdbd.zt='1'  and rdbd.lyxh=d.lyxh
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=d.ypCode AND yp.OrganizeId=rdb.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
	WHERE rdb.ProcessState='0'
	AND rdb.CreateTime BETWEEN @kssj AND @jssj
	AND ISNULL(d.patientName, '') LIKE '%'+@patientName+'%'
	AND ISNULL(yp.ypmc, '') LIKE '%'+@ypmc+'%'
	AND ISNULL(d.cw, '') LIKE '%'+@cw+'%'
) r
ORDER BY r.zytyapplyno
 ";
            var param = new SugarParameter[]
            {
               new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                //new SugarParameter("@fybz", req.fybz??""),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<ZyTyApplyNoVO>(sql, param);
        }

        /// <summary>
        /// Lấy chi tiết trả thuốcV2
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailV2(HospitalizationReturnDrugParam req)
        {
            const string sql = @"
SELECT r.*, CONCAT(r.dj, '元/', r.bmdw) djStr, CONVERT(NUMERIC(11,2),ISNULL(r.zxdwsl*r.dj/r.zhyz,0)) je
FROM (
	SELECT d.patientName, d.zyh, d.cw, d.yzId, d.zxId, d.ypCode, yp.ypmc, ypsx.ypgg,ISNULL(CONVERT(VARCHAR(50),d.zh),'') zh , CASE when d.yzxz = '1' then 'Tạm thời' when d.yzxz = '2' then 'Dài hạn' else '/' end yzxzmc
	,yp.ycmc, CONVERT(INT,ISNULL(d.zxdwsl/d.zhyz,0)) sl, CONVERT(INT,d.zxdwsl) zxdwsl, dbo.f_getyfbmDw(@yfbmCode, d.ypCode, @OrganizeId) bmdw
	,dbo.f_getYfbmYpComplexYpSlandDw(d.zxdwsl, @yfbmCode, d.ypCode, @OrganizeId) slStr, CONCAT(d.yl,d.yldw) ylStr, rdb.zytyapplyno
	,d.dj, LTRIM(RTRIM(d.ph)) ph, LTRIM(RTRIM(d.pc)) pc, d.zhyz, rdb.applyNo, rdbd.Id rdbdId
	FROM (
		SELECT lyxh,SUM(s.zxdwsl) zxdwsl, s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.zyh, s.fybz, s.zhyz
		,s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz
		FROM (
			SELECT lyxh,c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz 
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=a.zxId AND c.yzId=a.yzId AND c.ypCode=a.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=a.OrganizeId
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
            AND ISNULL(a.zh,'')=ISNULL(c.zh,'')
			AND a.Id=c.zyypxxId
			UNION ALL
			SELECT lyxh,-1*c.sl zxdwsl, c.pc, c.ph, c.ypCode, a.yzId, a.zxId, a.zyh, a.fybz,a.zhyz
			,a.patientName, a.cw, a.yl, a.yldw, a.pcmc, a.zh, a.zlff, a.dj, a.je, a.yzxz
			FROM NewtouchHIS_PDS.dbo.zy_ypyzxx(NOLOCK) a 
			INNER JOIN NewtouchHIS_PDS.dbo.zy_tymx(NOLOCK) c ON c.ypCode=a.ypCode AND c.zxId=a.zxId AND c.yzId=a.yzId AND c.OrganizeId=a.OrganizeId AND c.zt='1'
			WHERE a.organizeId=@OrganizeId
			AND a.fyyf=@yfbmCode
			AND a.fybz=@fybz
		) s
		GROUP BY s.pc, s.ph, s.ypCode, s.yzId, s.zxId, s.patientName, s.cw, s.yl, s.yldw, s.pcmc, s.zh, s.zlff, s.dj, s.je, s.yzxz, s.zyh, s.fybz, s.zhyz
	) d
	INNER JOIN dbo.zy_returnDrugApplyBill(NOLOCK) rdb ON  rdb.yzId=d.yzId AND rdb.OrganizeId=@OrganizeId AND rdb.zt='1'
	INNER JOIN dbo.zy_returnDrugApplyBillDetail(NOLOCK) rdbd ON rdb.Id=rdbd.rabId AND rdbd.ypCode=d.ypCode and rdbd.lyxh=d.lyxh AND rdbd.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=d.ypCode AND yp.OrganizeId=rdb.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@OrganizeId
	WHERE rdb.ProcessState='0' AND rdb.zytyapplyno=@zytyapplyno
	AND rdb.CreateTime BETWEEN @kssj AND @jssj
	AND ISNULL(d.patientName, '') LIKE '%'+@patientName+'%'
	AND ISNULL(yp.ypmc, '') LIKE '%'+@ypmc+'%'
	AND ISNULL(d.cw, '') LIKE '%'+@cw+'%'
) r
ORDER BY r.applyNo, r.ypmc
 ";
            var param = new SugarParameter[]
            {
               new SugarParameter("@OrganizeId", req.OrganizeId),
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@fybz", req.fybz??""),
                new SugarParameter("@zytyapplyno", req.zytyapplyno??""),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj),
                new SugarParameter("@patientName", req.patientName??""),
                new SugarParameter("@ypmc", req.ypmc??""),
                new SugarParameter("@cw", req.cw??"")
            };
            return FindList<HospitalizationReturnDispenseDetail>(sql, param);
        }

        public List<YPFYPatientInfoVO> GetFybdBrxxTree(string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT 
    DISTINCT yz.zyh,yz.patientName, yz.cw,yz.bqCode, xtbq.bqmc
FROM dbo.zy_ypyzxx(NOLOCK) yz 
    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_bq xtbq ON xtbq.bqCode=yz.bqCode AND xtbq.OrganizeId=yz.OrganizeId AND xtbq.zt='1'
    INNER JOIN dbo.zy_ypyzczjl(NOLOCK) czjl ON czjl.zxId=yz.zxId AND czjl.yzId=yz.yzId AND czjl.ypCode=yz.ypCode AND czjl.OrganizeId=yz.OrganizeId
    inner join NewtouchHIS_Sett.dbo.zy_brjbxx br on br.zyh=yz.zyh and br.zybz in (1,7) 
--inner join Newtouch_CIS.dbo.zy_brxxk(NOLOCK) brk on brk.zyh=yz.zyh and brk.WardCode=yz.bqCode AND brk.OrganizeId=yz.OrganizeId and brk.zt='1' and brk.zybz<>'9'
WHERE yz.OrganizeId= @OrganizeId
    AND yz.fyyf= @yfbmCode
    AND czjl.operatetype='1'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FindList<YPFYPatientInfoVO>(sql, param);
        }

        public IList<HospitalizationDispenseDetailV2> GetFybdList(Pagination pagination, QueryZYFYInfoReqVOV2 req)
        {
            var strSql = new StringBuilder(@"
select 
    'Phát thuốc' czlx,b.lyxh,
    b.zyh,b.patientName,b.cw,b.ypCode,c.ypmc,b.pcmc,ISNULL(CONVERT(VARCHAR(50),b.zh),'') zh, b.yl, b.yldw, CONCAT(b.yl,b.yldw) ylStr
    ,(case when b.yzxz = '1' then 'Tạm thời' when b.yzxz = '2' then 'Dài hạn' else '/' END) yzxzmc,b.zlff,c.ycmc,CONCAT(b.dj,'元/',c.djdw) djStr,
    dbo.f_getYfbmYpComplexYpSlandDw(SUM(d.sl), @yfbmCode, b.ypCode, @OrganizeId) slStr,
    CONVERT(NUMERIC(11,2),ISNULL(SUM(b.dj/b.zhyz*d.sl),0)) je,
    a.Id,a.yzId,a.zxId,a.CreateTime,a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg,b.bqCode
from zy_ypyzczjl a with(nolock),NewtouchHIS_PDS.dbo.zy_ypyzxx b with(nolock)
    left join NewtouchHIS_Base.dbo.V_S_xt_yp c with(nolock) on b.OrganizeId=c.OrganizeId and b.ypCode=c.ypCode
    left join NewtouchHIS_PDS.dbo.zy_ypyzzxph d with(nolock) on b.OrganizeId=d.OrganizeId and b.id=d.zyypxxId and d.zt='1'
    left JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = c.jx AND e.zt='1'
    left JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=c.ypId AND ypsx.OrganizeId=c.OrganizeId
where a.ypyzxxId=b.Id and a.OrganizeId=b.OrganizeId and a.OrganizeId=@OrganizeId
    and a.CreateTime BETWEEN @kssj AND @jssj
    and d.fyyf=@yfbmCode
    and ISNULL(b.zh,'')=ISNULL(d.zh,'') and a.operateType=1 
");

            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj)
            };

            strSql.AppendLine(" AND convert(varchar(20),a.CreateTime,120) in (select col from dbo.f_split(@operatetime,',')) ");
            parms.Add(new SugarParameter("@operatetime", req.operatetime));

            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine(" AND b.bqCode in (select col from dbo.f_split(@bqCode,',')) ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }

            strSql.AppendLine(" AND b.zyh in (select col from dbo.f_split(@zyh,',')) ");
            parms.Add(new SugarParameter("@zyh", req.Zyh));

            strSql.AppendLine(@" group by b.lyxh,
b.zyh, b.patientName, b.cw, b.ypCode, c.ypmc, b.pcmc, b.yl, b.yldw, b.zh,
a.Id, a.yzId, a.zxId, a.ypCode, b.yzxz, b.zlff, c.ycmc, b.dj, c.djdw, a.CreateTime, a.CreatorCode,d.pc,e.jxmc,ypsx.ypgg,b.bqCode ");


            return QueryWithPage<HospitalizationDispenseDetailV2>(strSql.ToString(), pagination, parms.ToArray());
        }

        public IList<FybdComboboxList> GetFybdComboboxList(QueryZYFYInfoReqVO req)
        {
            var strSql = new StringBuilder(@"
select 
    distinct convert(varchar(20), a.createtime, 120) operatetime
from zy_ypyzczjl a with(nolock),NewtouchHIS_PDS.dbo.zy_ypyzxx b with(nolock)
    left join NewtouchHIS_Base.dbo.V_S_xt_yp c with(nolock) on b.OrganizeId=c.OrganizeId and b.ypCode=c.ypCode
    left join NewtouchHIS_PDS.dbo.zy_ypyzzxph d with(nolock) on b.OrganizeId=d.OrganizeId and b.id=d.zyypxxId and d.zt='1'
    left JOIN NewtouchHIS_Base.dbo.xt_ypjx(NOLOCK) e on e.jxCode = c.jx AND e.zt='1'
    left JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=c.ypId AND ypsx.OrganizeId=c.OrganizeId
where a.ypyzxxId=b.Id and a.OrganizeId=b.OrganizeId and a.OrganizeId=@OrganizeId
    and a.CreateTime BETWEEN @kssj AND @jssj
    and d.fyyf=@yfbmCode
    and ISNULL(b.zh,'')=ISNULL(d.zh,'') and a.operateType=1 
");

            var parms = new List<SugarParameter>
            {
                new SugarParameter("@yfbmCode", req.yfbmCode),
                new SugarParameter("@OrganizeId", req.organizeId),
                new SugarParameter("@kssj", req.Kssj),
                new SugarParameter("@jssj", req.Jssj)
            };

            if (!string.IsNullOrEmpty(req.bqCode))
            {
                strSql.AppendLine("AND b.bqCode in (select col from dbo.f_split(@bqCode,',')) ");
                parms.Add(new SugarParameter("@bqCode", req.bqCode));
            }

            strSql.AppendLine("AND b.zyh in (select col from dbo.f_split(@zyh,',')) ");
            parms.Add(new SugarParameter("@zyh", req.Zyh));

            return FindList<FybdComboboxList>(strSql.ToString(), parms.ToArray());
        }


        #endregion

        #region Chuẩn bị thuốc cho khoa
        public List<KSBYSQDInfoVO> GetDeptApplySendInfo(string yfbmCode, string organizeId,string bylx)
        {
            var sql = new StringBuilder(@" SELECT DISTINCT sqd.djh,sqd.bqbm, xtbq.bqmc ");
            if (bylx == "fy")
            {
                sql.AppendLine(@" from [xt_bqksby] (NOLOCK) sqd
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_bq xtbq ON xtbq.bqCode=sqd.bqbm AND xtbq.OrganizeId=sqd.OrganizeId AND xtbq.zt='1'
WHERE sqd.OrganizeId = @OrganizeId
AND sqd.yfbm = @yfbmCode
AND sqd.shzt = '1' ");
            } else
            {
                sql.AppendLine(@" ,thyy from [xt_ksbyth] (NOLOCK) sqd
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_bq xtbq ON xtbq.bqCode=sqd.bqbm AND xtbq.OrganizeId=sqd.OrganizeId AND xtbq.zt='1'
WHERE sqd.OrganizeId = @OrganizeId
AND sqd.yfbm = @yfbmCode
AND sqd.thzt = '1' ");
            }
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FindList<KSBYSQDInfoVO>(sql.ToString(), param);
        }

        /// <summary>
        /// Lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<DeptMedicineApplySendVo> SelectDeptApplySendList(string SqdArr ,string OrgId,string Yfbm)
        {
            string sql = @"select
        djh ,ksbm,Name ksName ,bqbm,bqmc,ypdm,ypmc,ypgg,yfbmCode,kykc,
        case when (kykc%zhyz)>0 
            then dbo.f_getComplexYpSlandDw(kykc,zhyz,bmdw,zxdw)+cast(kykc as varchar)+zxdw 
	        else dbo.f_getComplexYpSlandDw(kykc,zhyz,bmdw,zxdw) end kyslStr,
        bmdw,zhyz,bzdw,zxdw,sl,pc,ph,
        case when sl<zhyz then cast(sl as varchar)+zxdw when 
            (sl%zhyz)>0 then dbo.f_getComplexYpSlandDw(sl,zhyz,bmdw,zxdw)+cast(sl as varchar)+zxdw 
            else dbo.f_getComplexYpSlandDw(sl,zhyz,bmdw,zxdw) end sqslStr,
        pfj,lsj,yxq,ycmc
    from (
        select ksby.djh,ksby.ksbm,ks.Name,ksby.bqbm,bq.bqmc,bymx.ypdm,
            yp.ypmc,yp.ypgg,kcxx.yfbmCode,(kcxx.kcsl-kcxx.djsl) kykc ,
            dbo.f_getyfbmDw(@Yfbm , yp.ypCode, @OrgId) bmdw,
            kcxx.zhyz,yp.zxdw,yp.bzdw,kcxx.pc,kcxx.ph,
            bymx.sl,bymx.pfj,bymx.lsj,kcxx.yxq,yp.ycmc
        from [xt_bqksby] (nolock) ksby
        join [xt_bqksbymx]  (nolock) bymx on ksby.Id=bymx.byId and ksby.OrganizeId=bymx.OrganizeId and bymx.zt=1
        join NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bymx.Ypdm AND yp.OrganizeId=bymx.OrganizeId 
        join dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bymx.ypdm  AND kcxx.OrganizeId=bymx.OrganizeId AND kcxx.zt='1'
        and bymx.pc=kcxx.pc and bymx.ph=kcxx.ph and kcxx.yfbmCode=@Yfbm
        left join newtouchhis_base..V_S_Sys_Department ks on ksby.ksbm=ks.Code and ksby.OrganizeId=ks.OrganizeId
        left join newtouchhis_base..V_S_xt_bq bq on bq.bqCode=ksby.bqbm and bq.OrganizeId=ksby.OrganizeId
        where ksby.zt=1  and ksby.OrganizeId=@OrgId and ksby.shzt=1
          and yfbm=@Yfbm and djh in ( select col from f_split(@SqdArr,','))
    ) sqd
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@SqdArr", SqdArr),
                new SugarParameter("@OrgId", OrgId),
                new SugarParameter("@Yfbm", Yfbm),
            };
            return FindList<DeptMedicineApplySendVo>(sql, param);
        }
        /// <summary>
        /// Phát thuốc chuẩn bị cho khoa
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string ApplyNoSendDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@orgId", organizeId),
                new SugarParameter("@sqdArr", SqdArr),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.KsbyStock, param);
        }

        public List<DeptMedicineApplySendVo> SelectDeptApplyReturnList(string SqdArr, string OrgId, string Yfbm)
        {
            var sql = @"select djh ,ksbm,Name ksName ,bqbm,bqmc,ypdm,ypmc,ypgg,yfbmCode,kykc,
case when (kykc%zhyz)>0 then dbo.f_getComplexYpSlandDw(kykc,zhyz,bmdw,zxdw)+cast(kykc as varchar)+zxdw 
	else dbo.f_getComplexYpSlandDw(kykc,zhyz,bmdw,zxdw) end kyslStr,
bmdw,zhyz,bzdw,zxdw,tsl sl,ph,pc,yfbmmc,
case when tsl<zhyz then cast(tsl as varchar)+zxdw when (tsl%zhyz)>0 then dbo.f_getComplexYpSlandDw(tsl,zhyz,bmdw,zxdw)+cast(tsl as varchar)+zxdw 
 else dbo.f_getComplexYpSlandDw(tsl,zhyz,bmdw,zxdw) end sqslStr,
pfj,lsj,yxq,ycmc, CONVERT(varchar(100), getdate(), 120)CreateTime   
from (
select ksby.djh,ksby.ksbm,ks.Name,ksby.bqbm,bq.bqmc,bymx.ypdm,
yp.ypmc,yp.ypgg,kcxx.yfbmCode,(kcxx.kcsl-kcxx.djsl) kykc ,
dbo.f_getyfbmDw(@Yfbm , yp.ypCode,@OrgId) bmdw,
kcxx.zhyz,yp.zxdw,yp.bzdw,kcxx.ph,kcxx.pc,xtyf.yfbmmc,
bymx.tsl,yp.pfj,yp.lsj,kcxx.yxq,yp.ycmc
from [xt_ksbyth] (nolock) ksby
join [xt_ksbythmx]  (nolock) bymx on ksby.Id=bymx.byId and ksby.OrganizeId=bymx.OrganizeId and bymx.zt=1
join NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bymx.Ypdm AND yp.OrganizeId=bymx.OrganizeId 
join dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bymx.ypdm  AND kcxx.OrganizeId=bymx.OrganizeId AND kcxx.zt='1'
and bymx.pc=kcxx.pc and bymx.ph=kcxx.ph and kcxx.yfbmCode=@Yfbm
left join NewtouchHIS_Base..xt_yfbm xtyf on ksby.OrganizeId = xtyf.OrganizeId and ksby.yfbm = xtyf.yfbmCode  
left join newtouchhis_base..V_S_Sys_Department ks on ksby.ksbm=ks.Code and ksby.OrganizeId=ks.OrganizeId
left join newtouchhis_base..V_S_xt_bq bq on bq.bqCode=ksby.bqbm and bq.OrganizeId=ksby.OrganizeId
where ksby.zt=1  and ksby.OrganizeId=@OrgId
 and yfbm=@Yfbm and djh =@SqdArr
 ) sqd";
            var param = new SugarParameter[]
            {
                new SugarParameter("@SqdArr", SqdArr),
                new SugarParameter("@OrgId", OrgId),
                new SugarParameter("@Yfbm", Yfbm),
            };
            return FindList<DeptMedicineApplySendVo>(sql, param);
        }

        /// <summary>
        /// Trả lại tồn kho chuẩn bị thuốc cho khoa
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string KcApplyNoReturnDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@orgId", organizeId),
                new SugarParameter("@sqd", SqdArr),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.KsbyTslStock, param);
        }
        #endregion


        #region Đơn nội trú
        public IList<ZycfcxList> GetZycfList(Pagination pagination, ZycfcxVo req)
        {
            var parms = new List<SugarParameter>{};
            var strSql = new StringBuilder(@"");
            if (req.yzxz == "1")
            {
                strSql.AppendLine(@"select yz.zyh,hzxm,WardCode,bq.bqmc,DeptCode,yzh cfh,'1' yzxz,'Tạm thời' yzxzmc
	,yz.ysgh,staff.Name ysmc,sum(CONVERT(decimal(12,2),(yz.sl*yp.lsj))) je,yz.yzlx,yz.yzlx yzlxstr,kssj,yzxx.fybz,yzxx.lyxh
from Newtouch_CIS..zy_lsyz  (nolock) yz
join NewtouchHIS_PDS..zy_ypyzxx (nolock) yzxx on yz.Id=yzxx.yzId and yzxx.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_yp yp on yp.ypCode=yz.xmdm and yp.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_bq bq on bq.bqCode=yz.WardCode and bq.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh=yz.ysgh and staff.OrganizeId=yz.OrganizeId
where yz.OrganizeId=@OrganizeId and yz.zt=1 
	and yz.kssj>=@kssj and yz.kssj<=@jssj
	");
                
            }
            else {
                strSql.AppendLine(@"select  yz.zyh,hzxm,WardCode,bq.bqmc,DeptCode,yzh cfh,'2' yzxz,'Dài hạn' yzxzmc
	,yz.ysgh,staff.Name ysmc,sum(CONVERT(decimal(12,2),(yz.sl*yp.lsj))) je,yz.yzlx,yz.yzlx yzlxstr,yzxx.zxrq kssj,yzxx.fybz,yzxx.lyxh
from Newtouch_CIS..zy_cqyz  (nolock) yz
join NewtouchHIS_PDS..zy_ypyzxx (nolock) yzxx on yz.Id=yzxx.yzId
left join NewtouchHIS_Base..V_S_xt_yp yp on yp.ypCode=yz.xmdm and yp.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_bq bq on bq.bqCode=yz.WardCode and bq.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh=yz.ysgh and staff.OrganizeId=yz.OrganizeId
where yz.OrganizeId=@OrganizeId and yz.zt=1 
	and yz.kssj>=@kssj and yz.kssj<=@jssj
");
            }
            if (!string.IsNullOrWhiteSpace(req.bq))
            {
                strSql.AppendLine(" and yz.WardCode=@bq");
                parms.Add(new SugarParameter("@bq", req.bq));
            }
            if (!string.IsNullOrWhiteSpace(req.keyword))
            {
                strSql.AppendLine("  and (yz.zyh like @keyword or yz.hzxm like @keyword)");
                parms.Add(new SugarParameter("@keyword", "%" + req.keyword + "%"));
            }
            if (req.yzxz == "1")
                strSql.AppendLine(@" group by yz.zyh,hzxm,yz.zh,WardCode,bq.bqmc,DeptCode,yzh ,yz.ysgh ,yz.ysgh,staff.Name ,yz.yzlx,kssj,yzxx.fybz,yzxx.lyxh ");
            else
                strSql.AppendLine(@" group by yz.zyh,hzxm,yz.zh,WardCode,bq.bqmc,DeptCode,yzh ,yz.ysgh,yz.ysgh,staff.Name ,yz.yzlx,yzxx.zxrq,yzxx.fybz,yzxx.lyxh");

            parms.Add(new SugarParameter("@OrganizeId", req.organizeId));
            parms.Add(new SugarParameter("@kssj", req.kssj));
            parms.Add(new SugarParameter("@jssj", req.jssj));
            return QueryWithPage<ZycfcxList>(strSql.ToString(), pagination, parms.ToArray());
        }
        public IList<ZycfcxDetailList> GetZycfDetailList(Pagination pagination, ZycfcxVo req)
        {
            var sql = new StringBuilder();
            var parms = new List<SugarParameter>{};
            if (req.yzxz == "1")
            {
                sql.AppendLine(@"select  yz.Id,yz.zyh,hzxm,'1' yzxz,'Tạm thời' yzxzmc,yz.zh,WardCode,bq.bqmc,DeptCode,yzh cfh,yz.ysgh,staff.Name ysmc
,pcCode,xmdm,xmmc,yzzt,dw,yz.sl,yp.lsj dj,CONVERT(decimal(12,2),(yz.sl*yp.lsj)) je,yz.yzlx,kssj,ypjl,yz.ypgg,yznr,yzxx.fybz,yzxx.lyxh,yz.yztag 
,case  yz.yztag when 'JI' then 'Đơn thuốc hướng thần loại I' 
			   when 'JII' then 'Đơn thuốc hướng thần loại II' 
			   when 'MZ' then 'Đơn thuốc gây mê' 
			   when 'LXGB' then 'Cán bộ nghỉ hưu'
			   when 'TBCF' then 'Đơn thuốc bệnh đặc biệt' end yztagName
from Newtouch_CIS..zy_lsyz  (nolock) yz
join NewtouchHIS_PDS..zy_ypyzxx (nolock) yzxx on yz.Id=yzxx.yzId
left join NewtouchHIS_Base..V_S_xt_yp yp on yp.ypCode=yz.xmdm and yp.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_bq bq on bq.bqCode=yz.WardCode and bq.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh=yz.ysgh and staff.OrganizeId=yz.OrganizeId
where yzh=@yzh and yz.OrganizeId=@OrganizeId and yz.zt=1");
            }
            else {
                sql.AppendLine(@" select  yz.Id,yz.zyh,hzxm,'2' yzxz,'Dài hạn' yzxzmc,yz.zh,WardCode,bq.bqmc,DeptCode,yzh cfh,yz.ysgh,staff.Name ysmc
,pcCode,xmdm,xmmc,yzzt,dw,yz.sl,yp.lsj dj,CONVERT(decimal(12,2),(yz.sl*yp.lsj)) je,yz.yzlx,yzxx.zxrq kssj,ypjl,yz.ypgg,yznr,yzxx.fybz,yzxx.lyxh,yz.yztag 
,case  yz.yztag when 'JI' then 'Đơn thuốc hướng thần loại I' 
			   when 'JII' then 'Đơn thuốc hướng thần loại II' 
			   when 'MZ' then 'Đơn thuốc gây mê' 
			   when 'LXGB' then 'Cán bộ nghỉ hưu'
			   when 'TBCF' then 'Đơn thuốc bệnh đặc biệt' end yztagName
from Newtouch_CIS..zy_cqyz  (nolock) yz
join NewtouchHIS_PDS..zy_ypyzxx (nolock) yzxx on yz.Id=yzxx.yzId  
left join NewtouchHIS_Base..V_S_xt_yp yp on yp.ypCode=yz.xmdm and yp.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_xt_bq bq on bq.bqCode=yz.WardCode and bq.OrganizeId=yz.OrganizeId
left join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh=yz.ysgh and staff.OrganizeId=yz.OrganizeId
where yzh=@yzh and yz.OrganizeId=@OrganizeId and yz.zt=1 and  convert(varchar(19),yzxx.zxrq,121)=convert(datetime,@zxrq)");
                parms.Add(new SugarParameter("@zxrq", req.zxrq));
            }
            parms.Add(new SugarParameter("@yzh", req.yzh));
            parms.Add(new SugarParameter("@OrganizeId", req.organizeId));
            return QueryWithPage<ZycfcxDetailList>(sql.ToString(), pagination, parms.ToArray());
        }
        #endregion
    }
}
