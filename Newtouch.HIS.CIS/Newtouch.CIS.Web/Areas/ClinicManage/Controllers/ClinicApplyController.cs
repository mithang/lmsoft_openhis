using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtonsoft.Json;

using Newtouch.CIS.Compat;
using Newtouch.Domain.DTO.InterfaceSync;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices.Clinic;
using Newtouch.Domain.IRepository.Clinic;
using Newtouch.Domain.ValueObjects.Clinic;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;

using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.ClinicManage.Controllers
{
    [Area("ClinicManage")]
    public class ClinicApplyController : OrgControllerBase
    {
        public readonly static string OuterMediaMeettingAPIHost = Newtouch.CIS.Compat.AppConfig.Get("OuterMediaMeettingAPIHost");//apitrongtimtrạmđiểmTruy cậpappid

        private readonly IClinicDmnService _clinicDmnService;
        private readonly IClinicApplyRepo _clinicApplyRepo;

        public ClinicApplyController(IClinicDmnService clinicDmnService, IClinicApplyRepo clinicApplyRepo)
        {
            this._clinicDmnService = clinicDmnService;
            this._clinicApplyRepo = clinicApplyRepo;
        }

        // GET: ClinicManage/ClinicApply
        public override IActionResult Index()
        {
            return View();
        }

        #region Tra cứuNgoại trúĐặt hẹnGhi chép
        public IActionResult GetClinicApplyGridJson(Pagination pagination, string xm, string zjh, string kssj, string jssj, string ksCode, string ysgh, int sqzt)
        {
            var data = new
            {
                rows = _clinicDmnService.GetClinicApplyGridJson(pagination, this.OrganizeId, xm, zjh, kssj, jssj, ksCode, ysgh, sqzt,this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        #endregion

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult Updatesqzt(string id, int sqzt)
        {
            _clinicApplyRepo.Updatesqzt(id, sqzt, this.OrganizeId);
            return Success("Thao tác thành công.");
        }

        public IActionResult Accept(ClinicApplyInfoVO applyInfo)
        {
            var czks = "00000080";//Chẩn đoán từ xaSố hiệu khoa
            #region 1.Đăng ký khám
            var ghpbObj = _clinicDmnService.getClinicScheduleId(this.OrganizeId, czks); //LấyChẩn đoán từ xacủađăng ký khámXếp lịch
            if (ghpbObj == null)
            {
                throw new FailedException("Không tìm thấy thông tin xếp lịch");
            }
            var scheduId = ghpbObj.ScheduId;//LấyChẩn đoán từ xaXếp lịchId
            var regFree = ghpbObj.RegFee;//Lấyđăng ký khámChi phí
            var payLsh = "yczl" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zl_zflsh", OrganizeId, "{0:D6}", true); ;//tựđộngLấychitrảdòngchảysố
            var ghDataObj = new
            {
                kh = applyInfo.kh,
                ScheduId = scheduId,
                ghxz = "0",//tự chi trả
                PayWay = "3",//Alipay
                PayFee = regFree,//0 đồng
                PayLsh = payLsh
            };
            var ghReqObj = new
            {
                Data = ghDataObj,
                AppId = AuthenManageHelper.accessAppId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrgh = AuthenManageHelper.HttpPost<OutpatientRegistVO>(ghReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Outpatient/OutpatientRegist", this.UserIdentity);
            if (!(outstrgh.msg == "" || outstrgh.msg == "Đăng ký khámthành công"))
            {
                throw new FailedException(outstrgh.msg);
            }
            #endregion

            //2.gọiĐăng kýsẽnghịphònggiao diện
            //string token = AuthenManageHelper.GetToken();
            var hysqDataObj = new
            {
                usercode = this.UserIdentity.UserCode,
                username = this.UserIdentity.UserName,
                applyId = applyInfo.Id,//tự chi trả
                roomReset = "true",//Mặc địnhfalse，khisẽnghịvượtkhicầnlạiBắt đầusẽnghịkhi truyềnGiá trị： true
                roomid = applyInfo.mettingId,
                mzh = outstrgh.data.Mzh
            };
            var hysqReqObj = new
            {
                Data = hysqDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrMetting = AuthenManageHelper.HttpPost<MeetingResponseVO>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/DoctorMeetingApply", this.UserIdentity);

            if (!(outstrMetting.msg == "" || outstrMetting.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrMetting.msg);
            }
            var roompath = outstrMetting.data.roompath;
            var mettingUrl = OuterMediaMeettingAPIHost + roompath;

            //Lấyphòng khámToken
            var tokenReqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrToken = AuthenManageHelper.HttpPost<ClinicTokenResponseVO>(tokenReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicToken", this.UserIdentity);

            if (!(outstrToken.msg == "" || outstrToken.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrToken.msg);
            }

            //3.Số cuộc họpthôngbiếtmâyphòng khámgiao diện
            var sqjgDataObj = new
            {
                ApplyId= applyInfo.Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.jzz,
                IsConfirm = "True",  //Bác sĩCóKhôngđồng ýĐăng ký Trueđồng ý falseTừ chối
                roomid = outstrMetting.data.roomid,
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                Sqlsh=applyInfo.sqlsh,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrSqjg.msg);
            }

            var result = new
            {
                mettingUrl = mettingUrl,
                data = outstrMetting.data
            };
            return Content(result.ToJson());
            //return Success("Thao tác thành công");
        }


        public IActionResult Back(ClinicApplyInfoVO applyInfo)
        {
            //1.Cập nhậtTrả lạiTrạng thái
            _clinicApplyRepo.Updatesqzt(applyInfo.Id, (int)Emunzlzt.yth, this.OrganizeId);

            //2.Lấyphòng khámToken
            var tokenReqObj = new
            {
                Data = "",
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrToken = AuthenManageHelper.HttpPost<ClinicTokenResponseVO>(tokenReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicToken", this.UserIdentity);

            if (!(outstrToken.msg == "" || outstrToken.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrToken.msg);
            }

            //3.Số cuộc họpthôngbiếtmâyphòng khámgiao diện
            var sqjgDataObj = new
            {
                ApplyId = applyInfo.Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.yth,
                IsConfirm = "false",  //Bác sĩCóKhôngđồng ýĐăng ký Trueđồng ý falseTừ chối
                roomid = "",
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrSqjg.msg);
            }
            return Success("Thao tác thành công");
        }

        public IActionResult EnterMetting(ClinicApplyInfoVO applyInfo) {
            //Lấytoken
            //string token = AuthenManageHelper.GetToken();

            //2.gọiĐăng kýsẽnghịphònggiao diện
            //string token = AuthenManageHelper.GetToken();
            var hysqDataObj = new
            {
                usercode = this.UserIdentity.UserCode,
                username = this.UserIdentity.UserName,
                applyId = applyInfo.Id,//tự chi trả
                roomid = applyInfo.mettingId,
            };
            var hysqReqObj = new
            {
                Data = hysqDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            //var outstrMetting = AuthenManageHelper.HttpPost<ApiManageResponse<MeetingResponseVO>>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/DoctorMeetingApply", this.UserIdentity);
            var outstrMetting = AuthenManageHelper.HttpPost<MeetingResponseVO>(hysqReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/RemoteTreated/UserJoinMeetingApply", this.UserIdentity);

            if (!(outstrMetting.msg == "" || outstrMetting.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrMetting.msg);
            }
            var roompath = outstrMetting.data.roompath;
            var mettingUrl = OuterMediaMeettingAPIHost + roompath;
            
            var result = new
            {
                mettingUrl = mettingUrl,
                data = outstrMetting.data
            };
            return Content(result.ToJson());
        }

        /// <summary>
        /// Kết thúcKhám bệnh
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult yczlEndOpetration(string Id, TreatmentEntity jzObject, MedicalRecordEntity blObject)
        {
            var applyInfo = _clinicApplyRepo.GetYczl(Id, this.OrganizeId);
            //1.đẩyHồ sơ
            //Lấymâyphòng khámBệnh nhânHồ sơ
            //var  blDataObj = new
            //{
            //    ApplyId = Id,
            //    Sqlsh = applyInfo.sqlsh,
            //};
            //var blReqObj = new
            //{
            //    Data = blDataObj,
            //    AppId = AuthenManageHelper.appId,
            //    OrganizeId = OrganizeId,
            //    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            //};
            //var outstrbl = AuthenManageHelper.HttpPost<PatMedicalRecordVO>(blReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/GetClinicPatMedicalRecord", this.UserIdentity);

            //if (!(outstrbl.msg == "" || outstrbl.msg == "yêu cầuthành công"))
            //{
            //    throw new FailedException(outstrbl.msg);
            //}
            //SendPatMedicalRecordVO cfblDataObj = new SendPatMedicalRecordVO()
            //{
            //    ApplyId = Id,
            //    Sqlsh = applyInfo.sqlsh,
            //    blId = outstrbl.data == null ? null : outstrbl.data.blId,
            //    OrganizeId = outstrbl.data == null ? null : outstrbl.data.OrganizeId,
            //    mzh = outstrbl.data == null ? null : outstrbl.data.mzh,
            //    mjzbz = outstrbl.data == null ? null : outstrbl.data.mjzbz,
            //    blh = outstrbl.data == null ? null : outstrbl.data.blh,
            //    tizhong = outstrbl.data == null ? null : outstrbl.data.tizhong,
            //    tiwen = outstrbl.data == null ? null : outstrbl.data.tiwen,
            //    maibo = outstrbl.data == null ? null : outstrbl.data.maibo,
            //    xuetangclfs = outstrbl.data == null ? null : outstrbl.data.xuetangclfs,
            //    xuetang = outstrbl.data == null ? null : outstrbl.data.xuetang,
            //    shengao = outstrbl.data == null ? null : outstrbl.data.shengao,
            //    shousuoya = outstrbl.data == null ? null : outstrbl.data.shousuoya,
            //    shuzhangya = outstrbl.data == null ? null : outstrbl.data.shuzhangya,
            //    huxi = outstrbl.data == null ? null : outstrbl.data.huxi,
            //    hy = outstrbl.data == null ? null : outstrbl.data.hy,
            //    zs = outstrbl.data == null ? null : outstrbl.data.zs,
            //    fbsj = outstrbl.data == null ? null : outstrbl.data.fbsj,
            //    xbs = outstrbl.data == null ? null : outstrbl.data.xbs,
            //    jws = outstrbl.data == null ? null : outstrbl.data.jws,
            //    ct = outstrbl.data == null ? null : outstrbl.data.ct,
            //    clfa = outstrbl.data == null ? null : outstrbl.data.clfa,
            //    fzjc = outstrbl.data == null ? null : outstrbl.data.fzjc,
            //    yjs = outstrbl.data == null ? null : outstrbl.data.yjs,
            //    gms = outstrbl.data == null ? null : outstrbl.data.gms,
            //};

            //đẩyĐơn thuốcHồ sơ
            SendPatMedicalRecordVO cfblDataObj = new SendPatMedicalRecordVO();
            cfblDataObj.ApplyId = Id;
            cfblDataObj.Sqlsh = applyInfo.sqlsh;
            cfblDataObj.OrganizeId = jzObject.OrganizeId;
            cfblDataObj.mzh = jzObject.mzh;
            cfblDataObj.mjzbz = jzObject.mjzbz;
            cfblDataObj.blh = jzObject.blh;
            cfblDataObj.tizhong = jzObject.tizhong;
            cfblDataObj.tiwen = jzObject.tiwen;
            cfblDataObj.maibo = jzObject.maibo;
            cfblDataObj.xuetangclfs = jzObject.xuetangclfs;
            cfblDataObj.xuetang = jzObject.xuetang;
            cfblDataObj.shengao = jzObject.shengao;
            cfblDataObj.shousuoya = jzObject.shousuoya;
            cfblDataObj.shuzhangya = jzObject.shuzhangya;
            cfblDataObj.huxi = jzObject.huxi;
            cfblDataObj.hy = jzObject.hy;
            cfblDataObj.zs = blObject.zs;
            cfblDataObj.fbsj = blObject.fbsj;
            cfblDataObj.xbs = blObject.xbs;
            cfblDataObj.jws = blObject.jws;
            cfblDataObj.ct = blObject.ct;
            cfblDataObj.clfa = blObject.clfa;
            cfblDataObj.fzjc = blObject.fzjc;
            cfblDataObj.yjs = blObject.yjs;
            cfblDataObj.gms = blObject.gms;

                var cfblReqObj = new
                {
                    Data = cfblDataObj,
                    AppId = AuthenManageHelper.appId,
                    OrganizeId = OrganizeId,
                    Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                var outstrcfbl = AuthenManageHelper.HttpPost<string>(cfblReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/SendHisMedicalRecord", this.UserIdentity);
            
            //if (!(outstrcfbl.msg == "" || outstrcfbl.msg == "yêu cầuthành công"))
            //{
            //    throw new FailedException(outstrcfbl.msg);
            //}

            //2.đẩyĐơn thuốc
            var cfDataObj = new
            {
                ApplyId = Id,
                Sqlsh = applyInfo.sqlsh,
                mzh=applyInfo.mzh
            };
            var cfReqObj = new
            {
                Data = cfDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstrcf = AuthenManageHelper.HttpPost<string>(cfReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/SendHisOutpPrescriptionData", this.UserIdentity);

            //if (!(outstrcf.msg == "" || outstrcf.msg == "yêu cầuthành công"))
            //{
            //    throw new FailedException(outstrcf.msg);
            //}

            //3.GửiKết thúcthôngbiết
            var sqjgDataObj = new
            {
                ApplyId = Id,
                Sqlsh = applyInfo.sqlsh,
                ApplyStu = (int)Emunzlzt.yjs,
                IsConfirm = "True",  //Bác sĩCóKhôngđồng ýĐăng ký Trueđồng ý falseTừ chối
                roomid = "",
            };
            var sqjgReqObj = new
            {
                Data = sqjgDataObj,
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }; var outstrSqjg = AuthenManageHelper.HttpPost<bool>(sqjgReqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/Clinic/TreatedApplyResult", this.UserIdentity);

            if (!(outstrSqjg.msg == "" || outstrSqjg.msg == "Yêu cầuthành công"))
            {
                throw new FailedException(outstrSqjg.msg);
            }
            //3.sẽnghịphòngGửithông tin
            //4.Cập nhậtTrạng thái đăng ký
            _clinicApplyRepo.Updatesqzt(Id, (int)Emunzlzt.yjs, this.OrganizeId);

            return Success("Thao tác thành công");
            //return Content(result.ToJson());
        }
    }
}