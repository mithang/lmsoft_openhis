using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common.Utils;
using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;

using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.DTO.Medicine;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.HospitalizationPharmacy;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;
using Newtouch.PDS.Requset.Zyypyz;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Phát thuốc
    /// </summary>
    public class DispensingDmnService : DmnServiceBase, IDispensingDmnService
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mzphRepo;
        private readonly ISysMedicineStockInfoRepo _kcxx;
        private readonly IMzCfRepo _mzCfRepo;
        private readonly IMzCfmxRepo _mzCfmxRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public DispensingDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        #region Ngoại trú

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public string OutPatientBook(BookItemDo bookItemDo)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypdm", bookItemDo.YpCode),
                new SugarParameter("@zxdwsl", bookItemDo.Sl),
                new SugarParameter("@yfbmCode", bookItemDo.Yfbm),
                new SugarParameter("@cfh", bookItemDo.Cfh),
                new SugarParameter("@cfmxId", bookItemDo.CfmxId),
                new SugarParameter("@czh", bookItemDo.czh??""),
                new SugarParameter("@OrganizeId", bookItemDo.OrganizeId),
                new SugarParameter("@CreatorCode", bookItemDo.CreatorCode)
            };
            return FirstOrDefaultNoLog<string>(TSqlDispensing.mz_yp_book, param);
        }

        /// <summary>
        /// Đặt trước đơn ngoại trú（Đóng băng tồn kho） V2
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public string OutPatientBookV2(BookItemDo bookItemDo)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@cfh", bookItemDo.Cfh),
                new SugarParameter("@OrganizeId", bookItemDo.OrganizeId),
                new SugarParameter("@yfbmCode", bookItemDo.Yfbm),
                new SugarParameter("@CreatorCode", bookItemDo.CreatorCode)
            };
            return FirstOrDefault<string>(TSqlDispensing.mz_yp_book_v2, param);
        }

        /// <summary>
        /// Hủy đặt trước--Hỗ trợ nhiều lô
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutPatientBookCancelMulti(string cfmxphid, string ypCode, int sl, string yfbmCode, string cfh, string organizeId, string creatorCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypdm", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@sl", sl),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@CreatorCode", creatorCode),
                new SugarParameter("@oldcfmxphid",cfmxphid)
            };
            return FirstOrDefaultNoLog<string>(TSqlDispensing.mz_yp_book_cancel_v2, param);
        }


        /// <summary>
        /// Hủy đặt trước
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutPatientBookCancel(string ypCode, int sl, string yfbmCode, string cfh, string organizeId, string creatorCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypdm", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@sl", sl),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@CreatorCode", creatorCode)
            };
            return FirstOrDefaultNoLog<string>(TSqlDispensing.mz_yp_book_cancel, param);
        }

        /// <summary>
        /// Hủy đặt trước
        /// </summary>
        /// <param name="mxphlist"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutPatientBookCancel(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphlist, string organizeId, string creatorCode)
        {
            if (mxphlist == null || mxphlist.Count == 0)
            {
                return "Vui lòng truyền chi tiết lịch trực cần hủy";
            }

            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var p in mxphlist)
                {
                    var tempResult = OutPatientBookCancelMulti(p.cfmxphId, p.yp, (int)p.sl, p.fyyf, p.cfh, organizeId, creatorCode);
                    if (!string.IsNullOrWhiteSpace(tempResult)) return tempResult;
                    p.zt = "0";
                    p.gjzt = "1";
                    p.LastModifyTime = DateTime.Now;
                    p.LastModifierCode = creatorCode;
                    _mzphRepo.Update(p);
                };
                db.Commit();

                return "";
            }
        }

        /// <summary>
        /// HISHủy thanh toán ngoại trú trả lại đóng băng
        /// </summary>
        /// <param name="mxphlist"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutPatientYpdjReturn(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphlist, string organizeId, string creatorCode)
        {
            if (mxphlist == null || mxphlist.Count == 0)
            {
                return "Vui lòng truyền chi tiết lịch trực cần hủy";
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var p in mxphlist)
                {
                    var tempResult = OutPatientCancelSettMulti(p.cfmxphId, p.yp, (int)p.sl, p.fyyf, p.cfh, organizeId, creatorCode);
                    if (!string.IsNullOrWhiteSpace(tempResult)) return tempResult;
                    p.zt = "0";
                    p.gjzt = "1";
                    p.LastModifyTime = DateTime.Now;
                    p.LastModifierCode = creatorCode;
                    _mzphRepo.Update(p);
                };
                db.Commit();

                return "";
            }
        }
        /// <summary>
        /// HISHủy thanh toán ngoại trú trả lại đóng băng
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutPatientCancelSettMulti(string cfmxphid, string ypCode, int sl, string yfbmCode, string cfh, string organizeId, string creatorCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypdm", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@sl", sl),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@CreatorCode", creatorCode),
                new SugarParameter("@oldcfmxphid",cfmxphid)
            };
            return FirstOrDefaultNoLog<string>(TSqlDispensing.mz_yp_cancelSett_kcjd, param);
        }

        /// <summary>
        /// outpatient commit
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public string OutpatientCommit(string ypCode, string yfbmCode, string cfh, string organizeId, string creatorCode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var mzCf = _mzCfRepo.FindEntity(p =>
                    p.cfh == cfh && p.OrganizeId == organizeId && p.lyyf == yfbmCode && p.zt == "1");
                if (mzCf == null) throw new FailedException("Lấy thông tin đơn thuốc thất bại");
                if (mzCf.fybz != ((int)EnumFybz.Yp).ToString()) throw new FailedException("Chỉ có thể thao tác đơn thuốc trạng thái phát là đã xếp");

                var mzphs = _mzphRepo.IQueryable(p =>
                    p.yp == ypCode && p.fyyf == yfbmCode && p.cfh == cfh && p.OrganizeId == organizeId &&
                    p.gjzt == "0" && p.zt == "1");
                if (mzphs == null || !mzphs.Any()) throw new FailedException("Không tìm thấy thuốc đã đặt trước chỉ định");
                mzphs.ToList().ForEach(mzph =>
                {
                    var kcxx = _kcxx.IQueryable(p =>
                        p.ypdm == mzph.yp && p.OrganizeId == organizeId && p.zt == ((int)EnumKCZT.Enabled).ToString() &&
                        p.pc == mzph.pc && p.ph == mzph.ph && p.yfbmCode == yfbmCode && p.yxq == mzph.yxq);
                    if (!kcxx.Any()) throw new FailedException("Trong tồn kho không tìm thấy thuốc lô/số lô chỉ định");
                    if (kcxx.Select(p => p.djsl).Sum() < mzph.sl) throw new FailedException("Số lượng đã đóng băng không đủ");
                    mzCf.fybz = ((int)EnumFybz.Yf).ToString();
                    mzCf.LastModiFierCode = creatorCode;
                    mzCf.LastModifyTime = DateTime.Now;
                    _mzCfRepo.Update(mzCf);

                    var sysl = (int)mzph.sl;
                    kcxx.ToList().ForEach(p =>
                    {
                        if (sysl <= 0) return;
                        if (p.djsl >= sysl)
                        {
                            p.djsl -= sysl;
                            p.kcsl -= sysl;
                            sysl = 0;
                        }
                        else
                        {
                            sysl -= p.djsl;
                            p.djsl = 0;
                            p.kcsl -= p.djsl;
                        }
                        _kcxx.Update(p);
                    });
                });
                db.Commit();
            }
            return "";
        }

        /// <summary>
        /// Trừ tồn kho phát thuốc ngoại trú
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="cfmxId">mz_cfmx.id</param>
        /// <returns></returns>
        public string OutpatientDispensingDrug(string ypCode, long cfmxId, int sl, string yfbmCode, string cfh, string organizeId, string userCode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var reduceStockResult = OutpatientDispensingReduceStock(ypCode, yfbmCode, cfh, organizeId, userCode);
                if (!string.IsNullOrWhiteSpace(reduceStockResult)) return reduceStockResult;
                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// Trừ tồn kho phát thuốc ngoại trú
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string OutpatientDispensingReduceStock(string ypCode, string yfbmCode, string cfh, string organizeId, string userCode)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@userCode", userCode)
            };
            return FirstOrDefaultNoLog<string>(TSqlOutpatient.mz_reduce_stock, param.ToArray());
        }

        /// <summary>
        /// Trả thuốc ngoại trú trả tồn kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zxdwsl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="returnDrugBillNo">Số đơn trả thuốc</param>
        /// <returns></returns>
        public string OutpatientReturnDrugAddStock(string ypCode, string ph,
            string pc, int zxdwsl,
            string yfbmCode, string cfh,
            string organizeId, string userCode, string returnDrugBillNo)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypCode",ypCode ),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@ph", ph),
                new SugarParameter("@pc", pc),
                new SugarParameter("@cfh", cfh),
                new SugarParameter("@tysl", zxdwsl),//Số lượng đơn vị nhỏ nhất
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@returnDrugBillNo", returnDrugBillNo)
            };
            return FirstOrDefaultNoLog<string>(TSqlOutpatient.mz_rp_reture_drug, param);
        }

        #endregion

        #region Nội trú

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public string HospitalizationBook(BookItemDo bookItemDo)
        {
            try
            {
                using (var db = new Newtouch.Infrastructure.EFDbTransaction().BeginTrans())
                {
                    if (bookItemDo.Sl <= 0) return string.Format("Thuốc【{0}】Số lượng không thể nhỏ hơn 0", bookItemDo.YpCode);
                    var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    var kcxx = kcxxRepo.SelectData(bookItemDo.YpCode, bookItemDo.Yfbm, bookItemDo.OrganizeId);
                    if (kcxx == null || kcxx.Count == 0) return string.Format("Thuốc【{0}】Không tìm thấy tồn kho khả dụng", bookItemDo.YpCode);
                    var yxkcxx = kcxx.FindAll(p => p.kcsl - p.djsl > 0);
                    if (yxkcxx.Count == 0) return string.Format("Thuốc【{0}】Tồn kho không đủ", bookItemDo.YpCode);
                    if (yxkcxx.Sum(p => p.kcsl - p.djsl) < bookItemDo.Sl) return string.Format("Thuốc【{0}】Tồn kho không đủ", bookItemDo.YpCode);
                    var sykc = bookItemDo.Sl;
                    foreach (var itemKc in yxkcxx.OrderBy(p => p.yxq))
                    {
                        if (sykc <= 0) break;
                        var kykc = itemKc.kcsl - itemKc.djsl;
                        var tagDjsl = 0;
                        var zyphEntity = new ZyYpyzzxphEntity
                        {
                            OrganizeId = bookItemDo.OrganizeId,
                            zxId = bookItemDo.zxId,
                            ph = itemKc.ph,
                            pc = itemKc.pc,
                            ypCode = itemKc.ypdm,
                            yxq = itemKc.yxq,
                            fyyf = itemKc.yfbmCode,
                            zt = "1",
                            gjzt = "0",
                            CreateTime = DateTime.Now,
                            CreatorCode = bookItemDo.CreatorCode
                        };
                        if (kykc >= sykc)
                        {
                            //Tồn kho lô hiện tại Đủ
                            tagDjsl = sykc;
                            zyphEntity.sl = sykc;
                            sykc = 0;
                        }
                        else
                        {
                            //Tồn kho lô hiện tại Không đủ
                            tagDjsl = kykc;
                            itemKc.djsl = itemKc.djsl + kykc;
                            zyphEntity.sl = kykc;
                            sykc -= kykc;
                        }

                        db.Insert(zyphEntity);

                        #region Sửa số lượng đóng băng tồn kho

                        const string sqlKcxx = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@djsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE ypdm=@ypCode AND yfbmCode=@yfbmCode AND ph=@ph AND pc=@pc AND zt='1' AND tybz='0' ";
                        var paramKcxx = new SugarParameter[]
                        {
                        new SugarParameter("@djsl", tagDjsl),
                        new SugarParameter("@userCode", bookItemDo.CreatorCode),
                        new SugarParameter("@ypCode", itemKc.ypdm),
                        new SugarParameter("@yfbmCode",itemKc.yfbmCode ),
                        new SugarParameter("@ph", itemKc.ph),
                        new SugarParameter("@pc", itemKc.pc),
                        };
                        db.ExecuteSqlCommandNoLog(sqlKcxx, paramKcxx);

                        #endregion Sửa số lượng đóng băng tồn kho
                    }
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        public string getYpDl(string ypcode, string organizeId)
        {
            string sql = @"select p.dlCode from NewtouchHIS_Base.dbo.xt_yp p where p.ypCode=@ypCode and p.organizeId=@organizeId";
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypCode", ypcode),
                new SugarParameter("@organizeId", organizeId)
            };
            return FirstOrDefaultNoLog<string>(sql, param);
        }

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId">Thực hiệnID</param>
        /// <param name="organizeId">Tổ chứcID</param>
        /// <param name="yfbmCode">Mã bộ phận nhà thuốc</param>
        /// <param name="creatorCode">Người tạo</param>
        /// <param name="patientName">Bệnh nhân thuộc y lệnh</param>
        /// <returns></returns>
        public string HospitalizationBook(string yzId, string zxId, string organizeId, string yfbmCode, string creatorCode, string patientName)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId),
                new SugarParameter("@OrganizeId",organizeId ),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@CreatorCode", creatorCode),
                new SugarParameter("@patientName", patientName)
            };
            return FirstOrDefault<string>(TSqlDispensing.zy_yp_book, param);
        }

        /// <summary>
        /// Hủy đóng băng，và xóa vật lý thông tin y lệnh Thận trọng khi dùng
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <returns></returns>
        public string CancelForzenAndDeleteYz(string yzId, string zxId)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yzId", yzId),
                new SugarParameter("@zxId", zxId)
            };
            return FirstOrDefault<string>(TSqlDispensing.zy_yp_cancelForzen_deleteData, param);
        }

        /// <summary>
        /// Nội trúBookĐóng băng tồn kho và lưuzy_ypyzzxph
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="yzDetails"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string HospitalizationBook(string zxId, List<YzDetail> yzDetails, string organizeId)
        {
            try
            {
                if (yzDetails == null || yzDetails.Count == 0) return "Chi tiết y lệnh không được để trống";
                using (var db = new Newtouch.Infrastructure.EFDbTransaction().BeginTrans())
                {
                    foreach (var yzxx in yzDetails)
                    {
                        var bookItemDo = new BookItemDo
                        {
                            YpCode = yzxx.ypCode,
                            Sl = (int)yzxx.sl * yzxx.zhyz,
                            Yfbm = string.IsNullOrWhiteSpace(yzxx.fyyf) ? Constants.CurrentYfbm.yfbmCode : yzxx.fyyf,
                            OrganizeId = organizeId ?? "",
                            CreatorCode = yzxx.yzzxsqr ?? "",
                            zxId = zxId,
                            zh = yzxx.zh
                        };

                        if (bookItemDo.Sl <= 0) throw new Exception(string.Format("Thuốc【{0}】Số lượng không thể nhỏ hơn 0", yzxx.ypmc));
                        var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                        var kcxx = kcxxRepo.SelectData(bookItemDo.YpCode, bookItemDo.Yfbm, bookItemDo.OrganizeId);
                        if (kcxx == null || kcxx.Count == 0) throw new Exception(string.Format("Thuốc【{0}】Không tìm thấy tồn kho khả dụng", yzxx.ypmc));
                        var yxkcxx = kcxx.FindAll(p => p.kcsl - p.djsl > 0);
                        if (yxkcxx.Count == 0) throw new Exception(string.Format("Thuốc【{0}】Tồn kho không đủ", yzxx.ypmc));
                        if (yxkcxx.Sum(p => p.kcsl - p.djsl) < bookItemDo.Sl) throw new Exception(string.Format("Thuốc【{0}】Tồn kho không đủ", yzxx.ypmc));
                        var sykc = bookItemDo.Sl;
                        foreach (var itemKc in yxkcxx.OrderBy(p => p.yxq))
                        {
                            if (sykc <= 0) break;
                            var kykc = itemKc.kcsl - itemKc.djsl;
                            int tagDjsl;
                            var zyphEntity = new ZyYpyzzxphEntity
                            {
                                yzId = yzxx.yzId,
                                OrganizeId = bookItemDo.OrganizeId,
                                zxId = bookItemDo.zxId,
                                ph = itemKc.ph,
                                pc = itemKc.pc,
                                ypCode = itemKc.ypdm,
                                yxq = itemKc.yxq,
                                fyyf = itemKc.yfbmCode,
                                zt = "1",
                                gjzt = "0",
                                zh = yzxx.zh,
                                CreateTime = DateTime.Now,
                                CreatorCode = bookItemDo.CreatorCode
                            };
                            if (kykc >= sykc)
                            {
                                //Tồn kho lô hiện tại Đủ
                                tagDjsl = sykc;
                                zyphEntity.sl = sykc;
                                sykc = 0;
                            }
                            else
                            {
                                //Tồn kho lô hiện tại Không đủ
                                tagDjsl = kykc;
                                itemKc.djsl = itemKc.djsl + kykc;
                                zyphEntity.sl = kykc;
                                sykc -= kykc;
                            }

                            db.Insert(zyphEntity);

                            #region Sửa số lượng đóng băng tồn kho

                            const string sqlKcxx = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl+@djsl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE ypdm=@ypCode AND yfbmCode=@yfbmCode AND ph=@ph AND pc=@pc AND zt='1' AND tybz='0' ";
                            var paramKcxx = new SugarParameter[]
                            {
                                new SugarParameter("@djsl", tagDjsl),
                                new SugarParameter("@userCode", bookItemDo.CreatorCode),
                                new SugarParameter("@ypCode", itemKc.ypdm),
                                new SugarParameter("@yfbmCode",itemKc.yfbmCode ),
                                new SugarParameter("@ph", itemKc.ph),
                                new SugarParameter("@pc", itemKc.pc),
                            };
                            db.ExecuteSqlCommandNoLog(sqlKcxx, paramKcxx);

                            #endregion Sửa số lượng đóng băng tồn kho
                        }
                    }
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        /// <summary>
        /// Hủy nội trúbook
        /// </summary>
        /// <param name="zxId"></param>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public string HospitalizationCancelBook(string zxId, BookItemDo bookItemDo)
        {
            if (bookItemDo.Sl <= 0) return "";
            var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
            var kcxx = kcxxRepo.SelectData(bookItemDo.YpCode, bookItemDo.Yfbm, bookItemDo.OrganizeId);
            if (kcxx == null || kcxx.Count == 0) return "";
            var yxkcxx = kcxx.FindAll(p => p.djsl > 0);
            if (yxkcxx.Count == 0) return "";
            if (yxkcxx.Sum(p => p.djsl) < bookItemDo.Sl) return string.Format("Thuốc【{0}】Số lượng đóng băng tồn kho không đủ", bookItemDo.YpCode);
            var zyphRepo = new ZyYpyzzxphRepo(new DefaultDatabaseFactory());
            var zyphList = zyphRepo.SelectZyphList(zxId, bookItemDo.zxId, bookItemDo.YpCode, bookItemDo.OrganizeId);
            if (zyphList == null || zyphList.Count == 0) return "";
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var zyph in zyphList)
                {
                    var sykc = zyph.sl;
                    var pckcxxList = yxkcxx.FindAll(p => p.ph == zyph.ph && p.pc == zyph.pc);
                    if (pckcxxList.Count == 0) continue;
                    foreach (var pckcxx in pckcxxList)
                    {
                        int subtactSl;
                        if (pckcxx.djsl >= sykc)
                        {
                            subtactSl = (int)sykc;
                            sykc = 0;
                        }
                        else
                        {
                            subtactSl = pckcxx.djsl;
                            sykc -= pckcxx.djsl;
                        }
                        kcxxRepo.SubtractForzenKc(subtactSl, pckcxx.pc, pckcxx.ph, pckcxx.ypdm, pckcxx.yfbmCode,
                            pckcxx.OrganizeId, bookItemDo.CreatorCode);
                    }

                    zyph.gjzt = "1";
                    zyph.zt = "0";
                    zyphRepo.Update(zyph);
                }

                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// Hủy xếp thuốc
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string HospitalizationCancelArrangement(CancelArrangement param)
        {
            var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
            var zyphRepo = new ZyYpyzzxphRepo(new DefaultDatabaseFactory());
            var kcxx = kcxxRepo.SelectData(param.ypCode, param.ph, param.pc, param.yfbmCode, param.OrganizeId);
            if (kcxx == null || kcxx.Count == 0) return UpdateArrangementZtAndGjzt(zyphRepo, param);

            var yxkcxx = kcxx.FindAll(p => p.djsl > 0);
            if (yxkcxx.Count == 0) return UpdateArrangementZtAndGjzt(zyphRepo, param);

            var zyphList = zyphRepo.SelectZyphList(param.yzId, param.zxId, param.ypCode, param.pc, param.ph, param.OrganizeId);
            if (zyphList == null || zyphList.Count == 0) return "";

            if (yxkcxx.Sum(p => p.djsl) < zyphList.Sum(p => p.sl)) return string.Format("Thuốc【{0}】Số lượng đóng băng tồn kho không đủ", param.ypCode);

            foreach (var zyph in zyphList)
            {
                var sykc = zyph.sl;
                foreach (var pckcxx in yxkcxx)
                {
                    int subtactSl;
                    if (pckcxx.djsl >= sykc)
                    {
                        subtactSl = (int)sykc;
                        sykc = 0;
                    }
                    else
                    {
                        subtactSl = pckcxx.djsl;
                        sykc -= pckcxx.djsl;
                    }
                    kcxxRepo.SubtractForzenKc(subtactSl, pckcxx.pc, pckcxx.ph, pckcxx.ypdm, pckcxx.yfbmCode,
                        pckcxx.OrganizeId, param.userCode);
                }

                zyph.gjzt = "1";
                zyph.zt = "0";
                zyphRepo.Update(zyph);
            }

            return "";
        }


        /// <summary>
        /// Xếp thuốc
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string HospitalizationCancelArrangementWithTrans(CancelArrangement param)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                var tmpResult = HospitalizationCancelArrangement(param);
                if (string.IsNullOrWhiteSpace(tmpResult)) db.Commit();
                return tmpResult;
            }
        }

        /// <summary>
        /// Sửa trạng thái xếp thuốc và cờ xếp kệ
        /// </summary>
        /// <param name="zyphRepo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string UpdateArrangementZtAndGjzt(ZyYpyzzxphRepo zyphRepo, CancelArrangement param)
        {
            //Trực tiếp hủy xếp thuốc
            var zyph = zyphRepo.FindEntity(p =>
                p.zxId == param.zxId && p.yzId == param.yzId && p.OrganizeId == param.OrganizeId && p.ph == param.ph && p.pc == param.pc &&
                p.zt == "1" && p.gjzt == "0" && p.fyyf == param.yfbmCode);
            if (zyph == null) return "";
            zyph.zt = "0";
            zyph.gjzt = "1";
            return zyphRepo.Update(zyph) > 0 ? "" : "Sửa cờ xếp kệ xếp thuốc thất bại";
        }

        /// <summary>
        /// Phát thuốc nội trú Hỗ trợ nhiều y lệnh nhiều chi tiết xếp thuốc，Nhưng y lệnh và chi tiết xếp thuốc phải tương ứng
        /// </summary>
        /// <param name="zyphLs"></param>
        /// <param name="yzLs"></param>
        /// <param name="userCode"></param>
        /// <param name="fyid"></param>
        /// <returns></returns>
        public string HospitalizationDispensing(List<ZyYpyzzxphEntity> zyphLs, List<ZyYpyzxxEntity> yzLs, string userCode,out string fyid,string organizeId)
        {
            fyid = Guid.NewGuid().ToString();//Phát thuốcID
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var zyph in zyphLs)
                {
                    if (zyph.sl <= 0) continue;

                    var drug = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(zyph.OrganizeId, zyph.ypCode);
                    var ypmc = drug != null && !string.IsNullOrWhiteSpace(drug.ypmc) ? drug.ypmc : zyph.ypCode;
                    #region Lấy thông tin tồn kho
                    const string sql1 = @"
SELECT * FROM xt_yp_kcxx
WHERE ypdm = @ypCode AND yfbmCode = @yfbmCode AND tybz = '0' AND kcsl>0 AND djsl>=0 
AND pc=@pc AND ph=@ph AND zt='1' AND OrganizeId = @OrganizeId 
";
                    var param1 = new SugarParameter[]
                    {
                        new SugarParameter("@ypCode", zyph.ypCode),
                        new SugarParameter("@yfbmCode", zyph.fyyf),
                        new SugarParameter("@OrganizeId", zyph.OrganizeId),
                        new SugarParameter("@pc", zyph.pc),
                        new SugarParameter("@ph",zyph.ph)
                    };
                    var kcxxLs = db.FindList<SysMedicineStockInfoEntity>(sql1, param1);
                    if (kcxxLs == null || kcxxLs.Count == 0) return string.Format("Lô【{1}】của thuốc【{0}】Không tìm thấy tồn kho hiệu lực", ypmc, zyph.pc);
                    if (kcxxLs.Sum(p => p.kcsl) < zyph.sl) return string.Format("Lô【{1}】của thuốc【{0}】Số lượng tồn kho không đủ，Không thể phát thuốc", ypmc, zyph.pc);
                    #endregion
                    #region Trừ tồn kho

                    var sysl = (int)zyph.sl;
                    if (kcxxLs.Sum(p => p.djsl) < zyph.sl)
                    {
                        //Số đóng băng không đủ
                        foreach (var kcxx in kcxxLs.TakeWhile(kcxx => sysl > 0))
                        {
                            int subtractKc;
                            if (kcxx.kcsl >= sysl)
                            {
                                subtractKc = sysl;
                                sysl = 0;
                            }
                            else
                            {
                                subtractKc = kcxx.kcsl;
                                sysl -= kcxx.kcsl;
                            }
                            const string sql2 = @"
UPDATE dbo.xt_yp_kcxx SET djsl=0, kcsl=kcsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
                            var param2 = new SugarParameter[]
                            {
                                new SugarParameter("@sl", subtractKc ),
                                new SugarParameter("@userCode", userCode),
                                new SugarParameter("@pc", kcxx.pc),
                                new SugarParameter("@ph",kcxx.ph ),
                                new SugarParameter("@ypCode",kcxx.ypdm ),
                                new SugarParameter("@OrganizeId", kcxx.OrganizeId),
                                new SugarParameter("@yfbmCode", kcxx.yfbmCode)
                            };
                            db.ExecuteSqlCommand(sql2, param2);
                        }
                    }
                    else
                    {
                        //Số đóng băng đủ
                        foreach (var kcxx in kcxxLs.TakeWhile(kcxx => sysl > 0))
                        {
                            int subtractKc;
                            if (kcxx.kcsl >= sysl)
                            {
                                subtractKc = sysl;
                                sysl = 0;
                            }
                            else
                            {
                                subtractKc = kcxx.kcsl;
                                sysl -= kcxx.kcsl;
                            }

                            const string sql2 = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, kcsl=kcsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
                            var param2 = new SugarParameter[]
                            {
                                new SugarParameter("@sl", subtractKc ),
                                new SugarParameter("@userCode", userCode),
                                new SugarParameter("@pc", kcxx.pc),
                                new SugarParameter("@ph",kcxx.ph ),
                                new SugarParameter("@ypCode",kcxx.ypdm ),
                                new SugarParameter("@OrganizeId", kcxx.OrganizeId),
                                new SugarParameter("@yfbmCode", kcxx.yfbmCode)
                            };
                            db.ExecuteSqlCommand(sql2, param2);
                        }
                    }
                    #endregion
                }
                int isdjnr = 0;
                foreach (var yzxx in yzLs)
                {
                    #region Sửa trạng thái phát thuốc Đã xếp => Đã phát

                    yzxx.fybz = "2";
                    yzxx.LastModifierCode = userCode;
                    yzxx.LastModifyTime = DateTime.Now;
                    db.Update(yzxx);
                    if(yzxx.yzlx == ((int)Yzlx.zcy))
                    {
                        if (isdjnr == 0)
                        {
                            var data = GetBindTCMDj(yzxx.OrganizeId);
                            if (data != null)
                            {
                                string sql2 = @"
 select * from [Newtouch_CIS].dbo.zy_lsyz where Id=@yzId  and OrganizeId=@OrganizeId and zt='1' and djbz='1' 
";
                                var param2 = new SugarParameter[]
                                {
                        new SugarParameter("@yzId", yzxx.yzId),
                        new SugarParameter("@OrganizeId", yzxx.OrganizeId)
                                };
                                zylsyzDTO lsyz = db.FirstOrDefault<zylsyzDTO>(sql2, param2);
                                if (lsyz != null && lsyz.yzh != "")
                                {
                                    var xmObj = new
                                    {
                                        OrganizeId = yzxx.OrganizeId,
                                        zyh = lsyz.zyh,
                                        sfxm = data.sfxmCode,
                                        dl = data.sfdlCode,
                                        ys = lsyz.ysgh,
                                        ks = lsyz.zxksdm,
                                        ysmc = "",
                                        ksmc = "",
                                        dj = data.dj,
                                        sl = lsyz.sl,
                                        dw = data.dw,
                                        zxks = lsyz.zxksdm,
                                        zfxz = data.zfxz,
                                        zfbl = data.zfbl,
                                        yzh=lsyz.yzh
                                    };
                                    var apiResp = SiteSettAPIHelper.Request<object, DefaultResponse>("/api/Prescription/DjxmInsert", xmObj);
                                    isdjnr++;
                                }
                            }
                        }
                    }
                    #endregion
                    #region Thêm bản ghi thao tác

                    var zyczjlEntity = new ZyYpyzczjlEntity
                    {
                        OrganizeId = yzxx.OrganizeId,
                        ypyzxxId = yzxx.Id,
                        operateType = "1",
                        ypCode = yzxx.ypCode,
                        sl = yzxx.sl * yzxx.zhyz,
                        zxId = yzxx.zxId,
                        yzId = yzxx.yzId,
                        CreatorCode = userCode,
                        CreateTime = DateTime.Now,
                        bz = "",
                        LastModifierCode = "",
                        LastModifyTime = null,
                        zyfyapplyno= fyid,
                        zsm = yzxx.zsm,

                    };
                    db.Insert(zyczjlEntity);
                    #endregion
                }
                db.Commit();
                var zyhArr = yzLs.Select(p => p.zyh).Distinct().ToArray();
                string zyhs = "";
                zyhs = string.Join(",", zyhArr);
                SyncPatFee(organizeId, zyhs, 1);
                Updatezy_brxxexpand(organizeId, zyhs);
                return "";
            }
        }
        #endregion
        #region Tính toán thời gian thực phí bệnh nhân
        public void Updatezy_brxxexpand(string OrganizeId, string zyh)
        {
            try
            {
                string sql = @" exec Newtouch_CIS..usp_zy_brxxexpand_update @orgId,@zyh";
                SugarParameter[] para ={
                new SugarParameter("@orgId",OrganizeId),
                 new SugarParameter("@zyh",zyh)
                };
                int i = this.ExecuteSqlCommand(sql, para);
            }
            catch (Exception)
            {
            }

        }
        #endregion
        #region Tạo phí hạng mục
        /// <summary>
        /// Đồng bộ mới nhấtCPOEPhí hạng mục
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        public void SyncPatFee(string orgId, string zyh, int zxtype)
        {
            //Có bật cơ chế đồng bộ phí mới nhất không
            using (var db = new EFDbTransaction().BeginTrans())
            {
                string sql = "";
                if (zxtype == 0) //Đồng bộ phí hạng mục
                {
                    sql = @" exec [NewtouchHIS_Sett]..[skd_syncxmjfbcope] @orgId=@orgId,@lqrq=@lqrq,@zyh=@zyh ";
                }
                else //Đồng bộ phí thuốc
                {
                    sql = @" exec [NewtouchHIS_Sett]..[skd_syncypjfbcope] @orgId=@orgId,@lqrq=@lqrq,@zyh=@zyh";
                }
                SugarParameter[] para = new SugarParameter[] {
                        new SugarParameter("@orgId",orgId),
                        new SugarParameter("@lqrq",DateTime.Now),
                        new SugarParameter("@zyh",zyh)
                    };
                db.ExecuteSqlCommand(sql, para);
                db.Commit();
            }
        }
        #endregion
        public TCMDjXMVO GetBindTCMDj(string OrganizeId)
        {
            var sfxmCode = _sysConfigRepo.GetValueByCode("TCM_DaiJianCode", OrganizeId);
            var sql = @"
SELECT *
  FROM[NewtouchHIS_Base]..[V_S_xt_sfxm]
WHERE zt = '1' and organizeId = @orgId and sfxmCode = @sfxmCode";
            return this.FirstOrDefault<TCMDjXMVO>(sql, new[] { new SugarParameter("@orgId", OrganizeId), new SugarParameter("@sfxmCode", sfxmCode) });

        }
        public string PrepareMedicine(BYDjInfoDTO yplist, string organizeId, string yhgh)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var ksbyid = Guid.NewGuid().ToString();
                    var byEntity = new XtksbyEntity
                    {
                        Id = ksbyid,
                        OrganizeId = organizeId,
                        yfbm = yplist.yfbm,
                        bqbm = yplist.bqbm,
                        ksbm = yplist.ksbm,
                        djh = yplist.djh,
                        shzt = "1",
                        zt = "1",
                        CreatorCode = yhgh,
                        CreateTime = DateTime.Now,
                        LastModifierCode = "",
                        LastModifyTime = null,
                    };
                    db.Insert(byEntity);
                    foreach (var byxx in yplist.mx)
                    {
                        var bymxEntity = new XtksbymxEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            byId = ksbyid,
                            OrganizeId = organizeId,
                            ypdm = byxx.ypdm,
                            sl = byxx.sl.ToString(),
                            dw = byxx.ckdw,
                            zhyz = byxx.zhyz.ToString(),
                            pfj = byxx.pfj.ToString(),
                            lsj = byxx.lsj.ToString(),
                            zje = byxx.zje.ToString(),
                            zt = "1",
                            CreatorCode = yhgh,
                            CreateTime = DateTime.Now,
                            LastModifierCode = "",
                            LastModifyTime = null,
                            gg = byxx.gg,
                            yplb = byxx.yplb,
                            yxq = byxx.yxq,
                            sccj = byxx.sccj,
                            ypmc = byxx.ypmc,
                            pc = byxx.pc,
                            ph = byxx.ph
                        };
                        db.Insert(bymxEntity);
                    };
                    db.Commit();
                }

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return "";
        }
        public string WithdrawalPreparation(string Djh, string organizeId, string yhgh,string shzt)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var sql = @"
select [Id]
      ,[OrganizeId]
      ,[yfbm]
      ,[bqbm]
      ,[ksbm]
      ,[djh]
      ,[shzt]
      ,[zt]
      ,[CreatorCode]
      ,convert(date,[CreateTime]) CreateTime
      ,[LastModiFierCode]
      ,[LastModifyTime] from [NewtouchHIS_PDS].dbo.xt_bqksby where
 organizeId=@orgId  and zt='1' and djh=@Djh";
                  var ksbyxx=this.FirstOrDefault<XtksbyEntity>(sql, new[] { new SugarParameter("@orgId", organizeId), new SugarParameter("@Djh", Djh.Trim()) });
                    if (ksbyxx.shzt!="1")
                    {
                        return "Thu hồi thất bại！Chứng từ phải ở trạng thái đã gửi mới có thể thu hồi！";
                    }
                    string sqlByxx = @"
delete from  xt_bqksby where id=@Id and organizeId=@orgId";
                    var paramByxx = new SugarParameter[]
                    {
                        new SugarParameter("@Id", ksbyxx.Id),
                        new SugarParameter("@orgId", organizeId),
                    };
                    db.ExecuteSqlCommand(sqlByxx, paramByxx);
                    string sqlBymxxx = @"
delete from  xt_bqksbymx where byid=@byid and organizeId=@orgId";
                    var paramBymxxx = new SugarParameter[]
                    {
                        new SugarParameter("@byid", ksbyxx.Id),
                        new SugarParameter("@orgId", organizeId),
                    };
                    db.ExecuteSqlCommand(sqlBymxxx, paramBymxxx);
                    db.Commit();
                }
                
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return "";
        }
        public string PrepareMedicineReturn(BythDjInfoDTO yplist, string organizeId, string yhgh)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var ksbyid = Guid.NewGuid().ToString();
                    var byEntity = new XtksbythEntity
                    {
                        Id = ksbyid,
                        OrganizeId = organizeId,
                        djh = yplist.djh,
                        thzt = "2",
                        yfbm = yplist.yfbm,
                        ksbm = yplist.ksbm,
                        bqbm = yplist.rkbm,
                        tjsj = DateTime.Now.Date,
                        thyy = yplist.thyy,
                        zt = "1",
                        CreatorCode = yhgh,
                        CreateTime = DateTime.Now,
                        LastModifierCode = "",
                        LastModifyTime = null,
                    };
                    db.Insert(byEntity);
                    foreach (var byxx in yplist.mx)
                    {
                        var bymxEntity = new XtksbythmxEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            byId = ksbyid,
                            OrganizeId = organizeId,
                            ypdm = byxx.ypdm,
                            ypmc = byxx.ypmc,
                            yplb = byxx.yplb,
                            tsl = byxx.tsl,
                            dw = byxx.dw,
                            gg = byxx.gg,
                            yxq = byxx.yxq,
                            //yfbm=byxx.yfbm,
                            sccj = byxx.sccj,
                            //thyy=byxx.thyy,
                            ph = byxx.ph,
                            pc = byxx.pc,
                            zt = "1",
                            CreatorCode = yhgh,
                            CreateTime = DateTime.Now,
                            LastModifierCode = "",
                            LastModifyTime = null,
                        };
                        db.Insert(bymxEntity);
                    };
                    db.Commit();
                }

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return "";
        }

        public string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt)
        {
            try
            {
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    var sql = @" 
SELECT [Id]
      ,[OrganizeId]
      ,[djh]
      ,[thzt]
      ,[yfbm]
      ,[ksbm]
      ,[bqbm]
      ,[tjsj]
      ,[thyy]
      ,[zt]
      ,[CreatorCode]
      ,[CreateTime]
      ,[LastModiFierCode]
      ,[LastModifyTime]
  FROM [NewtouchHIS_PDS].[dbo].[xt_ksbyth] where
 organizeId=@orgId  and zt='1' and djh=@Djh";
                    var ksbyxx = this.FirstOrDefault<XtksbythEntity>(sql, new[] { new SugarParameter("@orgId", organizeId), new SugarParameter("@Djh", Djh.Trim()) });
                    if (ksbyxx.thzt != "2")
                    {
                        return "Thu hồi thất bại！Chứng từ phải ở trạng thái đã gửi mới có thể thu hồi！";
                    }
                    string sqlByxx = @"
delete from  xt_ksbyth where id=@Id and organizeId=@orgId";
                    var paramByxx = new SugarParameter[]
                    {
                        new SugarParameter("@Id", ksbyxx.Id),
                        new SugarParameter("@orgId", organizeId),
                    };
                    db.ExecuteSqlCommand(sqlByxx, paramByxx);
                    string sqlBymxxx = @"
delete from  xt_ksbythmx where byid=@byid and organizeId=@orgId";
                    var paramBymxxx = new SugarParameter[]
                    {
                        new SugarParameter("@byid", ksbyxx.Id),
                        new SugarParameter("@orgId", organizeId),
                    };
                    db.ExecuteSqlCommand(sqlBymxxx, paramBymxxx);
                    db.Commit();
                }

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return "";
        }
    }
}