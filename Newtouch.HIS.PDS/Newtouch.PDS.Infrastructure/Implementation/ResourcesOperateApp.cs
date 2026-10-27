using NewtouchHIS.Lib.Base.Extension;
using Newtouch.HIS.API.Common.Helper;
using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;

using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Common;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// Hủy đặt trước
    /// </summary>
    public class ResourcesOperateApp : AppBase, IResourcesOperateApp
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mzCfphRepo;
        private readonly IMzCfRepo _mzCfRepo;
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IPyDmnService _pyDmnService;

        #region Ngoại trú

        /// <summary>
        /// Đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult OutpatientBook(OutpatientBookRequestDTO request)
        {
            var result = new OutpatientBookAppV2(request).Process();
            if (!result.IsSucceed)
            {
                throw new FailedException("", result.ResultMsg);
            }

            return result;
        }

        /// <summary>
        /// Sửa đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult OutpatientBookModify(OutpatientBookModifyRequestDTO request)
        {
            var result = new ActResult();
            if (request == null || request.Items == null || request.Items.Count == 0)
            {
                result.IsSucceed = false;
                result.ResultMsg = "Vui lòng truyền thông tin đơn thuốc cần sửa";
                return result;
            }
            request.Items.Select(p => p.Cfh).Distinct().ToList().ForEach(q =>
            {
                var rpInfo = _mzCfRepo.SelectRpList(q, request.OrganizeId);
                if (rpInfo == null || rpInfo.Count == 0)
                {
                    InserNewRp(request, q, result);
                    return;
                }

                if (rpInfo.Exists(r => !((int)EnumFybz.Yp).ToString().Equals(r.fybz)))
                {
                    result.IsSucceed = false;
                    result.ResultMsg += string.Format("Đơn thuốc【{0}】Sửa thất bại，Đơn thuốc bị sửa chỉ có thể là đơn đã xếp thuốc；", q);
                    return;
                }
                string cancelResult;
                try
                {
                    var cancelBookRequestDto = new OutpatientCancelAllBookRequestDTO
                    {
                        OrganizeId = request.OrganizeId,
                        AppId = request.AppId,
                        cfh = q,
                        CreatorCode = request.CreatorCode,
                        fyyf = "",
                        ResponseColumns = request.ResponseColumns,
                        Token = request.Token,
                        Timestamp = request.Timestamp,
                        TokenType = request.TokenType
                    };
                    cancelResult = OutpatientCancelBook(cancelBookRequestDto);
                }
                catch (FailedException e)
                {
                    cancelResult = e.Msg;
                }
                catch (Exception e)
                {
                    cancelResult = e.Message;
                }
                if (!string.IsNullOrWhiteSpace(cancelResult))
                {
                    result.IsSucceed = false;
                    result.ResultMsg += string.Format("Đơn thuốc【{0}】Hủy đơn thuốc gốc thất bại。{1}；", q, cancelResult);
                    return;
                }

                InserNewRp(request, q, result);
            });
            if (result.IsSucceed) return result;
            throw new FailedException("", result.ResultMsg);
        }

        /// <summary>
        /// Thêm đơn thuốc （Khi sửa phát hiện không có đơn thuốc này）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cfh"></param>
        /// <param name="result"></param>
        private void InserNewRp(OutpatientBookModifyRequestDTO request, string cfh, ActResult result)
        {
            try
            {
                var newbookItems = request.Items.FindAll(p => p.Cfh == cfh);
                var newbookRequestDto = new OutpatientBookRequestDTO
                {
                    AppId = request.AppId,
                    CreatorCode = request.CreatorCode,
                    Items = newbookItems,
                    OrganizeId = request.OrganizeId,
                    ResponseColumns = request.ResponseColumns,
                    Timestamp = request.Timestamp,
                    Token = request.Token,
                    TokenType = request.TokenType
                };
                var newBookResult = OutpatientBook(newbookRequestDto);
                if (newBookResult.IsSucceed)
                {
                    return;
                }

                result.Data = newBookResult.Data;
                result.IsSucceed = false;
                result.ResultMsg += string.Format("Đơn thuốc【{0}】Tạo đơn thuốc mới thất bại,{1};", cfh, result.ResultMsg);
            }
            catch (FailedException ex)
            {
                var errorMsg = string.Format("Đơn thuốc【{0}】Tạo đơn thuốc mới thất bại; ", cfh) + ex.Msg;
                result.IsSucceed = false;
                result.ResultMsg = errorMsg;
            }
            catch (Exception ex)
            {
                var errorMsg = string.Format("Đơn thuốc【{0}】Tạo đơn thuốc mới thất bại,{1}; ", cfh, result.ResultMsg) + ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message);
                result.IsSucceed = false;
                result.ResultMsg = errorMsg;
            }
        }

        /// <summary>
        /// (Trước khi phát thuốc)Trả thuốc một phần
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request)
        {
            foreach (var item in request.Items)
            {
                int rpInfo = _mzCfRepo.DeleteCf(item.cfh.ToString(), request.OrganizeId.ToString());
                if (rpInfo <= 0)
                {
                    throw new FailedException("", "Sửa trạng thái đơn thuốc thất bại");
                }
            }
            return "";
        }

        /// <summary>
        /// Hủy đặt trước ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelBook(OutpatientCancelPartBookRequestDTO request)
        {
            if (request == null || !request.Items.Any())
            {
                throw new FailedException("", "Vui lòng truyền chi tiết cần hủy");
            }
            var result = new StringBuilder();
            Parallel.ForEach(request.Items, p =>
            {
                var errorMsg = new DispensingDmnService(new DefaultDatabaseFactory()).OutPatientBookCancel(p.ypdm, p.sl * p.zhyz, p.yfbmCode, p.cfh, request.OrganizeId, request.CreatorCode);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    result.Append(errorMsg + ";");
                }
            });
            if (!string.IsNullOrWhiteSpace(result.ToString()))
            {
                throw new FailedException("", result.ToString());
            }

            return "";
        }

        /// <summary>
        /// Hủy đặt trước ngoại trú Áp dụng cho đơn thuốc không thanh toán
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelBook(OutpatientCancelAllBookRequestDTO request)
        {
            var cfxx = _mzCfRepo.SelectRpList(request.cfh, request.OrganizeId);
            if (cfxx == null || cfxx.Count == 0)
            {
                throw new FailedException("", "Không tìm thấy đơn thuốc chỉ định");
            }

            if (cfxx.Exists(p => p.jsnm > 0))
            {
                throw new FailedException("", "Đơn thuốc đã thanh toán，Không thể hủy đặt trước");
            }

            var mxphList = _mzCfphRepo.GetList(request.cfh, cfxx[0].lyyf, request.OrganizeId, "0");
            var fybz = cfxx[0].fybz;
            Task updateRpZtTask;
            Task updateRpDetailZt;
            if (mxphList.Count == 0 || ((int)EnumFybz.Wp).ToString().Equals(fybz))
            {
                updateRpZtTask = UpdateRpZt(cfxx, request);
                updateRpDetailZt = UpdateRpDetailZt(request);
                updateRpZtTask.Wait();
                updateRpDetailZt.Wait();
                return "";
            }

            if (((int)EnumFybz.Yp).ToString().Equals(cfxx[0].fybz))
            {
                #region use multi threading
                //try
                //{
                //    Parallel.ForEach(mxphList, p =>
                //    {
                //        var errorMsg = new DispensingDmnService(new DefaultDatabaseFactory(), false).OutPatientBookCancel(p.yp, (int)p.sl, p.fyyf, p.cfh, request.OrganizeId, request.CreatorCode);
                //        if (!string.IsNullOrWhiteSpace(errorMsg))
                //        {
                //            result.Append(errorMsg + ";");
                //        }
                //        else
                //        {
                //            p.zt = "0";
                //            p.LastModifyTime = DateTime.Now;
                //            p.LastModifierCode = request.CreatorCode;
                //            new OutpatientPrescriptionDetailBatchNumberRepo(new DefaultDatabaseFactory()).Update(p);
                //        }
                //    });
                //}
                //catch (Exception e)
                //{
                //    result.Append(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
                //    throw;
                //}
                #endregion
                #region use single thread

                var result = _dispensingDmnService.OutPatientBookCancel(mxphList, request.OrganizeId, request.CreatorCode);
                #endregion

                if (!string.IsNullOrWhiteSpace(result))
                {
                    throw new FailedException("", result);
                }

                updateRpZtTask = UpdateRpZt(cfxx, request);
                updateRpDetailZt = UpdateRpDetailZt(request);
                updateRpZtTask.Wait();
                updateRpDetailZt.Wait();
            }
            else
            {
                throw new FailedException("", "Hủy đặt trước chỉ áp dụng đơn đã xếp thuốc");
            }
            return "";
        }

        /// <summary>
        /// Hủy thanh toán ngoại trú trả lại tồn kho đóng băng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelDjYpReturn(OutpatientCancelAllBookRequestDTO request)
        {
            var cfxx = _mzCfRepo.SelectTfRpList(request.cfh, request.OrganizeId);
            if (cfxx == null || cfxx.Count == 0)
            {
                throw new FailedException("", "Không tìm thấy đơn thuốc chỉ định");
            }
            var mxphList = _mzCfphRepo.GetList(request.cfh, cfxx[0].lyyf, request.OrganizeId, "0");
            var fybz = cfxx[0].fybz;
            if (((int)EnumFybz.Yp).ToString().Equals(cfxx[0].fybz))
            {
                var result = _dispensingDmnService.OutPatientYpdjReturn(mxphList, request.OrganizeId, request.CreatorCode);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    throw new FailedException("", result);
                }
            }
            return "";
        }

        /// <summary>
        /// Sửa trạng thái đơn thuốc
        /// </summary>
        /// <param name="rpList"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task UpdateRpZt(List<MzCfEntity> rpList, OutpatientCancelAllBookRequestDTO request)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                var mzCfRepo = new MzCfRepo(new DefaultDatabaseFactory());
                //var rpInfo = mzCfRepo.IQueryable(p => p.cfh == request.cfh && p.fybz == ((int)EnumFybz.Yp).ToString() && p.zt == "1" && p.jsnm == 0 && p.OrganizeId == request.OrganizeId);
                var rpInfo = rpList.FindAll(p => p.fybz == ((int)EnumFybz.Yp).ToString() && p.jsnm == 0);
                if (rpInfo.Any())
                {
                    rpInfo.ToList().ForEach(p =>
                    {
                        p.zt = "0";
                        p.LastModifyTime = DateTime.Now;
                        p.LastModiFierCode = request.CreatorCode;
                        mzCfRepo.Update(p);
                    });
                }
            });
        }

        /// <summary>
        /// Sửa trạng thái đơn thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task UpdateRpDetailZt(OutpatientCancelAllBookRequestDTO request)
        {
            return Task.Run(() =>
            {
                var mzCfmxRepo = new MzCfmxRepo(new DefaultDatabaseFactory());
                var mzCfmx = mzCfmxRepo.IQueryable(p => p.cfh == request.cfh && p.zt == "1" && p.OrganizeId == request.OrganizeId);
                if (mzCfmx.Any())
                {
                    mzCfmx.ToList().ForEach(p =>
                    {
                        p.zt = "0";
                        p.LastModifyTime = DateTime.Now;
                        p.LastModiFierCode = request.CreatorCode;
                        mzCfmxRepo.Update(p);
                    });
                }
            });
        }

        /// <summary>
        /// Xác định tài nguyên ngoại trú（commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCommit(OutpatientCommitRequestDTO request)
        {
            if (request == null)
            {
                throw new FailedException("", "Bản tin yêu cầu không được để trống");
            }

            if (request.Jsnm <= 0)
            {
                throw new FailedException("", "Mã nội bộ thanh toán bắt buộc nhập");
            }

            if (request.Sfsj <= Convert.ToDateTime("1971-01-01"))
            {
                throw new FailedException("", "Thời gian thu phí không hợp lệ");
            }

            var rpInfo = _mzCfRepo.SelectRpList(request.Cfh, request.OrganizeId);
            if (rpInfo == null || rpInfo.Count == 0)
            {
                throw new FailedException("", "Không tìm thấy thông tin đơn thuốc chỉ định");
            }

            if (rpInfo[0].fybz == ((int)EnumFybz.Wp).ToString())
            {
                //Xếp thuốc lại

                throw new NotImplementedException();
            }
            rpInfo[0].cfnm = request.Cfnm;
            rpInfo[0].Fph = request.Fph;
            rpInfo[0].jsnm = request.Jsnm;
            rpInfo[0].sfsj = request.Sfsj;
            rpInfo[0].LastModiFierCode = request.CreatorCode;
            rpInfo[0].LastModifyTime = DateTime.Now;
            if (_mzCfRepo.Update(rpInfo[0]) <= 0) throw new FailedException("", "Xác nhận tài nguyên phát thuốc ngoại trú thất bại");
            var rq = new MqGeneralTaskRequestDto
            {
                Timestamp = DateTime.Now,
                body = new MqGeneralTaskBody
                {
                    OperationType = "supplementPatientBaseInfo",
                    Content = new PatientBaseInfoQueryRequest
                    {
                        cfh = rpInfo[0].cfh,
                        OrganizeId = rpInfo[0].OrganizeId,
                    }.ToJson()
                }.ToJson()
            };
            API.Common.Helper.MqClientHelper.SettGeneralTask(rq);
            return "";

        }

        /// <summary>
        /// Trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNoList"></param>
        /// <returns></returns>
        public string OutpatientReturnDrugs(tyInfo tyInfo, out List<string> returnDrugBillNoList)
        {
            returnDrugBillNoList = new List<string>();
            if (tyInfo == null || tyInfo.tyDrugDetail == null || tyInfo.tyDrugDetail.Count == 0)
            {
                return "Chi tiết trả thuốc không được để trống";
            }
            var errorMsg = new StringBuilder();
            foreach (var s in tyInfo.tyDrugDetail.Select(p => p.cfh).Distinct())
            {
                var findResult = tyInfo.tyDrugDetail.FindAll(p => p.cfh == s);
                findResult.ForEach(p =>
                {
                    if (string.IsNullOrEmpty(p.zsm))
                    {
                        p.zsm = "";
                    }
                });
                var request = new tyInfo
                {
                    userCode = tyInfo.userCode,
                    cfh = s,
                    yfbmCode = tyInfo.yfbmCode,
                    organizeId = tyInfo.organizeId,
                    tyDrugDetail = findResult

                };
                var processResult = new OutpatientReturnDrugsProcess(request).Process();
                if (!processResult.IsSucceed)
                {
                    errorMsg.Append(processResult.ResultMsg + ";");
                }
                else
                {
                    returnDrugBillNoList.Add((string)processResult.Data);
                }
            }

            return errorMsg.ToString();
        }
        #endregion

        #region Nội trú

        /// <summary>
        /// Lưu y lệnh nội trú  Lưu thông tin y lệnh、Xếp thuốc、Đóng băng tồn kho
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActResult HospitalizatiionBook(ZyypyzzxRequest request)
        {
            var result = new YzzxAppV2(request).Process();
            if (result.IsSucceed) return result;
            throw new FailedException("", result.ResultMsg);
        }

        /// <summary>
        /// Trả thuốc nội trú，Chèn đơn xin trả thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request)
        {
            var result = new HospitalizatiionReturnDispensingMedicineProcess(request).Process();
            if (!result.IsSucceed)
            {
                throw new FailedException("", result.ResultMsg);
            }
            return result.ResultMsg;
        }

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">Số đơn trả thuốc</param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicine(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo)
        {
            var effectiveLs = new List<tyParam>();
            var locker = new object();
            Parallel.ForEach(tyDetail, p =>
            {
                if (p.sl <= 0 || p.zhyz <= 0) return;
                lock (locker)
                {
                    effectiveLs.Add(p);
                }
            });
            var tyInfo = new tyInfo
            {
                returnDrugBillNo = OrderExt.GennerateSimpleOrderNo(),
                organizeId = organizeId,
                yfbmCode = yfbmCode,
                userCode = userCode,
                tyDrugDetail = effectiveLs
            };
            var result = new HospitalizatiionReturnMedicineProcess(tyInfo).Process();
            returnDrugBillNo = tyInfo.returnDrugBillNo;
            return result.IsSucceed ? "" : result.ResultMsg;
        }

        /// <summary>
        /// Trả thuốc nội trú
        /// </summary>
        /// <param name="tyDetail"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="returnDrugBillNo">Số đơn trả thuốc</param>
        /// <returns></returns>
        public string HospitalizatiionReturnMedicineV2(List<tyParam> tyDetail, string yfbmCode, string organizeId, string userCode, out string returnDrugBillNo)
        {
            var effectiveLs = new List<tyParam>();
            var locker = new object();
            Parallel.ForEach(tyDetail, p =>
            {
                // Kiểm tra sl và zhyz，Nếu điều kiện không đáp ứng，Trả về trực tiếp
                if (p.sl <= 0 || p.zhyz <= 0) return;

                // Kiểm tra p.zsm Có trống không，Nếu trống thì gán giá trị mặc định
                if (string.IsNullOrEmpty(p.zsm))
                {
                    p.zsm = "";  // Bạn có thể thay bằng giá trị cụ thể theo nhu cầu
                }

                // Sử dụng khóa để bảo vệ tài nguyên chia sẻ effectiveLs truy cập của
                lock (locker)
                {
                    effectiveLs.Add(p);
                }
            });

            if (effectiveLs[0].zytyapplyno == null || effectiveLs[0].zytyapplyno == "")
            {
                throw new Exception("Số đơn xin trả thuốc không được để trống！");
            }

            var tyInfo = new tyInfo
            {
                returnDrugBillNo = OrderExt.GennerateSimpleOrderNo(),
                organizeId = organizeId,
                yfbmCode = yfbmCode,
                userCode = userCode,
                tyDrugDetail = effectiveLs,
                zytyapplyno = effectiveLs[0].zytyapplyno
            };
            var result = new HospitalizatiionReturnMedicineProcessV2(tyInfo).Process();
            returnDrugBillNo = tyInfo.returnDrugBillNo;
            return result.IsSucceed ? "" : result.ResultMsg;
        }


        #endregion


        #region Hủy xếp thuốc

        /// <summary>
        /// Hủy xếp thuốc ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request)
        {
            OutpatientCancelArrangementValidate(request);

            //Hủy đơn thuốc đáp ứng yêu cầu mặc định——Đã lưu，Chưa thanh toán và đã quá thời gian thanh toán muộn nhất
            var settExpiredTime = _sysConfigRepo.GetValueByCode("settExpiredTime", request.OrganizeId);
            int tmpHour;
            int.TryParse(settExpiredTime, out tmpHour);
            tmpHour = tmpHour <= 0 ? 24 : tmpHour;
            var expirationDate = DateTime.Now.AddHours(-1 * tmpHour);
            var rps = _pyDmnService.SelectCancelRps(request.cfh, request.processesMaxNum, expirationDate, request.fyyf, request.OrganizeId);
            if (rps == null || rps.Count == 0)
            {
                return string.IsNullOrWhiteSpace(request.cfh) ? "Không tìm thấy đơn thuốc cần hủy yêu cầu xếp thuốc" : string.Format("Đơn thuốc【{0}】Không phù hợp yêu cầu hủy xếp thuốc，Đơn thuốc này có thể đã hủy！", request.cfh);
            }
            var emsg = new StringBuilder();
            var successCfh = new StringBuilder("Lần thao tác này thành công hủy đóng băng số đơn thuốc là：");
            rps.ForEach(p =>
            {
                var errorMsg = _pyDmnService.OutPatientCancelArrangement(p, request.CreatorCode);
                if (!string.IsNullOrWhiteSpace(errorMsg)) emsg.Append(errorMsg);
                successCfh.Append(p.cfh).Append("、");
            });
            if (string.IsNullOrWhiteSpace(emsg.ToString())) return successCfh.ToString().Trim('、');
            throw new FailedException("", emsg.ToString());
        }

        /// <summary>
        /// Kiểm tra bản tin
        /// </summary>
        /// <param name="request"></param>
        private void OutpatientCancelArrangementValidate(OutpatientCancelArrangementRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId)) throw new FailedException("", "Tổ chức không được để trống");
            if (string.IsNullOrWhiteSpace(request.fyyf)) throw new FailedException("", "Nhà thuốc phát thuốc không được để trống");
        }
        #endregion

    }
}