using Newtouch.HIS.Application.Interface.BusinessManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure.Operator;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.BusinessManage.Controllers
{
    public class OutPatientController : ControllerBase
    {
        // GET: BusinessManage/OutPatient
        private readonly IOutpatientRegApp _outpatientRegApp;

        public OutPatientController(IOutpatientRegApp outpatientRegApp)
        {
            this._outpatientRegApp = outpatientRegApp;
        }

        /// <summary>
        /// Đăng ký khám ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientReg()
        {
            return View();
        }

        #region 加载挂号信息
        /// <summary>
        /// Tải thông tin đăng ký khám của bệnh nhân theo số thẻ
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult LoadPatientInfo(string kh)
        {
            OutPatientBasicInfoVO basicInfoVO = _outpatientRegApp.LoadPatientInfo(kh);
            var result = "false";
            if (basicInfoVO == null)
            {
                return Content("{\"result\":\"" + result + "\"}");
            }
            else
            {
                return Content(basicInfoVO.ToJson());
            }
        }
        /// <summary>
        /// Chẩn đoán hệ thống
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SelectZdList(string keyword)
        {
            var ZdList = _outpatientRegApp.SelectZdList(keyword);
            return Content(ZdList.ToJson());
        }
        #endregion

        #region 挂号排班
        /// <summary>
        /// Lịch trực đăng ký khám List
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public JsonResult GetRegScheduleList(string keyword, string mzlx)
        {
            var data = _outpatientRegApp.GetRegScheduleList(keyword, mzlx);
            return Json(data);
        }
        #endregion

        #region 发票号
        /// <summary>
        /// Chọn hóa đơn
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseInvoice(string from = "")
        {
            ViewBag.from = from;
            return View();
        }
        /// <summary>
        /// Lấy số hóa đơn theo mã nhân viên
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public ActionResult GetInvoice()
        {
            var data = _outpatientRegApp.GetInvoice();
            return Content(data);
        }
        /// <summary>
        /// Kiểm tra số hóa đơn đã nhập có dùng được hay không
        /// by caishanshan
        /// </summary>
        /// <param name="inputFPH"></param>
        /// <returns></returns>
        public ActionResult CheckInvoice(string inputFPH)
        {
            _outpatientRegApp.CheckInvoice(inputFPH);
            return Success("true");
        }
        #endregion

        #region 正在挂号列表
        /// <summary>
        /// Danh sách đang đăng ký khám
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult RegisteringList(string list)
        {
            return null;
        }
        /// <summary>
        /// Lấy phí đăng ký khám Phí khám chữa bệnh Phí thẻ từ Phí làm thẻ
        /// </summary>
        /// <param name="ghlx">Loại đăng ký khám</param>
        /// <param name="isZzjm">Miễn giảm chuyển viện</param>
        /// <param name="isCkf">Phí thẻ từ</param>
        /// <param name="isGbf">Phí làm thẻ</param>
        /// <returns></returns>
        public ActionResult GetOutpatientFees(string ghlx, bool isZzjm, bool isCkf, bool isGbf)
        {
            var fees = _outpatientRegApp.GetOutpatientFees(ghlx, isZzjm, isCkf, isGbf);
            return Content(fees.ToJson());
        }
        #endregion

        #region 未结算/已结算列表  需求变更：去掉已结算内容 update by caishanshan 20161229
        /// <summary>
        /// Lấy danh sách quyết toán(Quyết toán/Chưa quyết toán)
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="isJz"></param>
        /// <param name="isJs"></param>
        /// <returns></returns>
        public ActionResult GetSettledFeesList(string kh, bool isJz)
        {
            var data = _outpatientRegApp.GetSettledFeesList(kh, isJz);
            return Content(data.ToJson());
        }
        //public ActionResult GetSettledFeesList(string kh, bool isJz, bool isJs)
        //{
        //    var data = _outpatientRegApp.GetSettledFeesList(kh, isJz, isJs);
        //    return Content(data.ToJson());
        //}
        #endregion

        #region 上一次结算
        /// <summary>
        /// Tải thông tin quyết toán lần trước
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public ActionResult GetLastSettleInfo(string userLogin)
        {
            //Lưu ý  Đổi thành đối tượng
            List<LastSettleInfoVO> info = _outpatientRegApp.GetLastSettleInfo(NewOperatorProvider.Provider.GetCurrent().UserCode);
            return Content(info.ToJson());
        }
        #endregion

        #region 作废
        /// <summary>
        /// Lưu bản ghi hủy đăng ký khám ngoại trú
        /// </summary>
        /// <param name="outPatientRegAbandEntity"></param>
        /// <returns></returns>
        public ActionResult SaveRegAbandRecord(int ghnm)
        {
            //string result = "";
            _outpatientRegApp.SaveRegAbandRecord(ghnm);
            return Success("true");
            //if (flag)
            //{
            //    result = "ok";
            //}
            //else
            //{
            //    result = "error";
            //}
            //return Content("{\"result\":\"" + result + "\"}");
        }
        #endregion

        #region 退号
        /// <summary>
        /// Trả số khám
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public ActionResult BackNum(int patid, int ghnm, bool isReturnAll)
        {
            var data = _outpatientRegApp.BackNum(patid, ghnm, isReturnAll);
            return Content(data.ToJson());
        }
        #endregion

        #region 保存
        /// <summary>
        /// Lấy số thứ tự khám bệnh
        /// </summary>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        public ActionResult GetJZXH(string ghlx, string ks, string ys, string ghzb)
        {
            var jzxh = _outpatientRegApp.GetJZXH(ghlx, ks, ys, ghzb);
            return Content(jzxh.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="brxz"></param>
        /// <param name="brxzbh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ksbh"></param>
        /// <param name="ks"></param>
        /// <param name="rybh"></param>
        /// <param name="ys"></param>
        /// <param name="ghlxbh"></param>
        /// <param name="ghlx"></param>
        /// <param name="ghzbbh"></param>
        /// <param name="ghzb"></param>
        /// <param name="jzxh"></param>
        /// <param name="fzbz"></param>
        /// <param name="jzbz"></param>
        /// <param name="ghzt"></param>
        /// <param name="ghxz"></param>
        /// <param name="dbxm"></param>
        /// <param name="gsrdh"></param>
        /// <param name="zdicd10"></param>
        /// <param name="zdmc"></param>
        /// <param name="ghf"></param>
        /// <param name="zlf"></param>
        /// <param name="ckf"></param>
        /// <param name="gbf"></param>
        /// <param name="zzjm"></param>
        /// <param name="kh"></param>
        /// <param name="isJS">Thay đổi yêu cầu Loại bỏ nội dung chưa quyết toán Vì vậy trường này không dùng nữa</param>
        /// <param name="ysk"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult Save(string patid, string brxz, string brxzbh, string ghly, string mjzbz, string ksbh, string ks, string rybh, string ys, string ghlxbh, string ghlx, string ghzbbh, string ghzb, string jzxh, string fzbz, string jzbz, string ghzt, string ghxz, string dbxm, string gsrdh, string zdicd10, string zdmc, string ghf, string zlf, string ckf, string gbf, string zzjm,string kh, string ysk,string ssk,string fph)
        {
            OutPatientRegEntity gh = new OutPatientRegEntity();
            gh.patid = Convert.ToInt32(patid);
            gh.brxz = brxz;
            gh.brxzbh = Convert.ToInt32(brxzbh);
            gh.ghly = ghly;
            gh.mjzbz = mjzbz;
            gh.ksbh = Convert.ToInt32(ksbh);
            gh.ks = ks;
            if (rybh != "") gh.rybh = Convert.ToInt32(rybh);
            else gh.rybh = 0;
            gh.ys = Convert.ToInt16(ys);
            gh.ghlx = ghlx;
            gh.ghlxbh = Convert.ToInt32(ghlxbh);
            gh.ghzb = ghzb;
            gh.ghzbbh = Convert.ToInt32(ghzbbh);
            gh.ghzb = ghzb;
            gh.jzxh = Convert.ToInt16(jzxh);
            gh.fzbz = Convert.ToByte(fzbz);
            gh.jzbz = jzbz;
            gh.ghzt = ghzt;
            gh.ghxz = ghzt;
            gh.dbxm = dbxm;
            gh.gsrdh = gsrdh;
            gh.zdicd10 = zdicd10;
            gh.zdmc = zdmc;
            gh.ghf = Convert.ToDecimal(ghf);
            gh.zlf = Convert.ToDecimal(zlf);
            gh.ckf = Convert.ToDecimal(ckf);
            gh.gbf = Convert.ToDecimal(gbf);
            if(zzjm== "true") gh.zzjm = true;
            else gh.zzjm = false;
            gh.kh = kh;

            _outpatientRegApp.Save(gh, Convert.ToDecimal(ysk), Convert.ToDecimal(ssk),fph);
            return Success("保存成功");
        }

        #endregion

    }
}