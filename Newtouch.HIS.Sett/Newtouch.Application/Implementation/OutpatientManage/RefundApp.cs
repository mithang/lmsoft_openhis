using System.Data;
using Newtouch.HIS.Application.Interface.BusinessManage;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using Newtonsoft.Json;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using newtouchyibao;

namespace Newtouch.HIS.Application.Implementation.BusinessManage
{
    /// <summary>
    /// 
    /// </summary>
    public class RefundApp : AppBase, IRefundApp
    {
        public int jsnmEx = 0;
        private OperatorModel _userModel;
        public OperatorModel userModel
        {
            get
            {
                if (_userModel == null)
                {
                    _userModel = OperatorProvider.GetCurrent();//Lấykhi trư ớc đ ăng nhập ngư ời dùng Đ ối tư ợng
                }
                return _userModel;
            }
        }

        public DataTable zffsDt = new DataTable();
        public List<FinanceReceiptEntity> newFphOrSj = new List<FinanceReceiptEntity>();
        public readonly IFinanceReceiptRepo _finRecRepos; //Số chứng từ biên lai bản ghi 
        public List<OutpatientRegistNonAttendanceEntity> ghjsDt = new List<OutpatientRegistNonAttendanceEntity>();

        /// <summary>
        /// thu tiền phư ơng thức thanh toán 
        /// </summary>
        public DataTable ZffsDt
        {
            get { return zffsDt; }
            set { zffsDt = value; }
        }

        public bool jxyb = false;

        public string kh = string.Empty;

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string Kh
        {
            get { return kh; }
            set { kh = value; }
        }

        public bool isReturnAll = false;

        /// <summary>
        /// Có hay khôngHoàn toàn bộ
        /// </summary>
        public bool IsReturnAll
        {
            get { return isReturnAll; }
            set { isReturnAll = value; }
        }

        public string lblYtk = "";
        public string lblSrce = "";
        private int jsnm = 0;

        /// <summary>
        /// Quyết toán ngoại trú
        /// </summary>
        public int Jsnm
        {
            get { return jsnm; }
            set { jsnm = value; }
        }

        public OutpatientSettlementEntity mzJs = new OutpatientSettlementEntity();
        public string lblFPHNew = "";
        public List<string> fpsqlList = new List<string>();
        private List<string> listSql = new List<string>();

        /// <summary>
        /// cầnhoàn phí chèn bảng tư ơng ứng thông tin
        /// </summary>
        public List<string> ListSql
        {
            get { return listSql; }
            set { listSql = value; }
        }

        public JS_ENTITY accountEntity = new JS_ENTITY();

        /// <summary>
        /// cầnhoàn phí chèn thông tin quyết toán
        /// </summary>
        public JS_ENTITY AccountEntity
        {
            get { return accountEntity; }
            set { accountEntity = value; }
        }

        public bool posTxj = false;
        public bool isAlreadyPosCX = false;
        public bool isChangeBrxz = false;

        /// <summary>
        /// Có hay không tự trả chuyển BHYT
        /// </summary>
        public bool IsChangeBrxz
        {
            get { return isChangeBrxz; }
            set { isChangeBrxz = value; }

        }

        private readonly IRefundDmnService _RefundService;
        private readonly IOutPatientDmnService _IOutPatientDmnService;
        private NB_INTERFACE_MZJS_REQ mzjsReq = null;
        private NB_INTERFACE_MZSS_REP mzssRep = null;
        private bool isAlreadyComfirmTF = false;
        private List<OutpatientSettlementCategoryEntity> jsdlList = new List<OutpatientSettlementCategoryEntity>();
        private string sqxh = string.Empty;
        private string lblSsk = "";
        private OutpatientSettlementEntity newMzJs = new OutpatientSettlementEntity();

        private List<OutpatientSettlementPaymentModelEntity> jszffsArray =
            new List<OutpatientSettlementPaymentModelEntity>();

        private readonly ISysConfigRepo _sysConfigRepo;

        private Dictionary<string, List<GridViewMx>> resultList = new Dictionary<string, List<GridViewMx>>()
        {
            {"all", new List<GridViewMx>()},
            {"any", new List<GridViewMx>()},
            {"nothing", new List<GridViewMx>()}
        };

        public List<OutpatientSettlementPaymentModelEntity> JszffsArray
        {
            get { return jszffsArray; }
        }

        // thu tiền quyết toán phư ơng thức thanh toán 
        private List<tbl_mz_jszffs_Ex> jszffsExListForSK = new List<tbl_mz_jszffs_Ex>();

        //Quyết toán phư ơng thức thanh toán 
        private List<tbl_mz_jszffs_Ex> jszffsExList = new List<tbl_mz_jszffs_Ex>();

        private string kahao;

        public List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph)
        {

            return _RefundService.GetFPHByKh(kh, startTime, endTime, fph);
        }

        public List<MZJS> GetMZJSByJsnm(int jsnm)
        {

            return _RefundService.GetMZJSByJsnm(jsnm);
        }

        public List<GridViewMx> GetGridViewMx(int jsnm)
        {
            /// Hạng mục ngoại trú chi tiết
            var data = _RefundService.GetMz_xmRecordsByJsnm(jsnm);
            /// Đ ăng ký khám ngoại trú hạng mụcchi tiết
            var data2 = _RefundService.GetMz_ghxmRecordsByJsnm(jsnm);
            /// Chi tiết đ ơn thuốc ngoại trú
            var data3 = _RefundService.GetMz_cfmxRecordsByJsnm(jsnm);
            int id = 0;
            data.ForEach(p =>
            {
                id = id + 1;
                p.id = id;
            });
            if (data2.Count > 0)
            {
                data2.ForEach(p =>
                {
                    id = id + 1;
                    p.id = id;
                    p.czh = "gh0209";
                    data.Add(p);
                });

            }
            if (data3.Count > 0)
            {
                data3.ForEach(p =>
                {
                    id = id + 1;
                    p.id = id;
                    data.Add(p);
                });
            }

            return data;
        }

        public decimal getSysl(int patid, int ghnm)
        {
            return _RefundService.getSysl(patid, ghnm);
        }

        public List<OutpatientRegistNonAttendanceEntity> getGhjsByGhnm(int tmpGhnm)
        {
            return _RefundService.getGhjsByGhnm(tmpGhnm);
        }

        /// <summary>
        /// Hoàn phí
        /// </summary>
        public bool btnReturn(string kh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll)
        {
            bool result = false;
            kahao = kh;
            Jsnm = jsnm;
            var data = GetFPHByKh(kh, "", "", "");
            var fapiao = data.Find(p => p.jsnm == jsnm);

            var GridViewMxData = GetGridViewMx(jsnm);
            GridViewMxList.ForEach(p =>
            {
                var tem = GridViewMxData.Find(c => c.id == p.id);
                p.tag = tem.tag;
            });


            OutpatientSettlementEntity tblMzJs = new OutpatientSettlementEntity();
            tblMzJs.fph = fapiao.oldFPH;
            tblMzJs.flzffy = fapiao.flzffy;
            tblMzJs.zffy = fapiao.zffy;
            tblMzJs.jzfy = fapiao.jzfy;
            tblMzJs.zlfy = fapiao.zlfy;

            tblMzJs.jsnm = fapiao.jsnm;
            tblMzJs.patid = fapiao.patid;
            tblMzJs.xjzf = fapiao.xjzf;
            tblMzJs.jslx = fapiao.jslx;
            tblMzJs.brxz = fapiao.brxz;

            tblMzJs.zje = fapiao.zje;
            tblMzJs.xjwc = fapiao.xjwc;
            tblMzJs.zhjz = fapiao.zhjz;
            tblMzJs.xjzffs = fapiao.xjzffs;
            tblMzJs.jszt = fapiao.jszt;
            tblMzJs.cxjsnm = fapiao.cxjsnm;
            tblMzJs.zh = fapiao.zh;
            tblMzJs.fpdm = fapiao.fpdm;
            //  tblMzJs.jmbl = fapiao.jmbl;
            tblMzJs.jmje = fapiao.jmje;
            tblMzJs.jylx = fapiao.jylx;
            tblMzJs.ysk = fapiao.ysk;
            tblMzJs.zl = fapiao.zl;
            tblMzJs.jzsj = fapiao.CreateTime;

            if (tblMzJs != null)
            {
                //trừĐ ăng ký khám của ngoài tất cả hoàn phí
                var MZJS = GetMZJSByJsnm(jsnm);
                var tmpDr = MZJS[0];
                int tmpGhnm = tmpDr.GHNM;

                if (tblMzJs.jslx == "0")
                {
                    decimal sysl = getSysl(tblMzJs.patid, tmpGhnm);

                    if (sysl > 0)
                    {
                        throw new FailedCodeException("OUTPAT_REFUNG_FAIL_GUHAO");

                    }
                }


                DataTable zffsDt = _RefundService.GetMzJszffsForJsnm(tblMzJs.jsnm);


                //Đ ặt hoàn toàn bộ,hoàn một phần, không hoàn chi tiếtlist

                getRecordWithCategory(GridViewMxList);
                JS_ENTITY accountEntityAll = getAccountList(2, tblMzJs, tmpDr);
                JS_ENTITY accountEntity = getAccountList(1, tblMzJs, tmpDr);
                if (accountEntityAll.jsxmList.Count == 0)
                {
                    accountEntityAll = accountEntity;
                }
                isReturnAll = (resultList["any"].Count == 0 && resultList["nothing"].Count == 0);


                // bản ghi trả lại số bảng 
                List<OutpatientRegistNonAttendanceEntity> ghjsDt = new List<OutpatientRegistNonAttendanceEntity>();
                if (fapiao.jslx == "0" && fapiao.zje > 2.0M) // trả lại phí thẻ từ,phí làm thẻ không chèn trả lại số bản ghi 
                {
                    //LấyĐ ăng ký khám thông tin quyết toán
                    ghjsDt = getGhjsByGhnm(tmpGhnm);

                }
                result = false;

                if (true == showTuiFeiJsFrm(kh, zffsDt, tblMzJs.jsnm, isReturnAll, accountEntity, ghjsDt,
                        accountEntityAll))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


            }
            else
            {
                throw new FailedException("OUTPAT_REFUNG_FP_NULL");
            }

            return result;

        }

        public bool showTuiFeiJsFrm(string kh, DataTable ZffsDt, int jsnm, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> ghjsDt, JS_ENTITY accountEntityAll)
        {
            var returnsOk = true;
            IsReturnAll = isReturnAll;
            Kh = kh;
            try
            {
                FrmJS_Load(kh, jsnm, ZffsDt, isReturnAll, accountEntity, ghjsDt, accountEntityAll);
            }
            catch
            {
                returnsOk = false;
            }
            return returnsOk;
        }

        private void FrmJS_Load(string kh, int jsnm, DataTable ZffsDt, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt, JS_ENTITY accountEntityAll)
        {
            this.isAlreadyPosCX = false;
            this.posTxj = false;
            mzJs = _RefundService.GetMZJSFromMz_jsByJsnm(jsnm);
            zffsDt = ZffsDt;
            ghjsDt = GhjsDt;

            if (isReturnAll == false && (accountEntity.isGh == false ||
                                         (accountEntity.isGh == true && accountEntity.isZh == true)))
            {


                //thêmgiườngghi sổ hoàn một phầnin chứng từ(bảng kê)
                //if (mzJs.jslx == "1" && mzJs.jylx == "14" && mzJs.jch != 0)
                //{
                //    flag = false;
                //}

                //Hoàn một phần thì bắt đ ầu lấy số hóa đ ơn
                bool flag = true;

                // List<string> fpsql = new List<string>();
                if (flag == true)
                {

                    lblFPHNew =
                        _IOutPatientDmnService.GetInvoiceListByEmpNo(this.UserIdentity.UserCode, this.OrganizeId);
                    //Chèn/Cập nhậtcw_fp

                    if (!_RefundService.CheckFPH(lblFPHNew))
                    {

                        //fpsqlList.AddRange(fpsql); ;
                    }
                    else
                    {
                        lblFPHNew = string.Empty;
                        throw new FailedException("GET_FPH_FAIL");
                    }
                }
                else
                {
                    lblFPHNew = _finRecRepos.getDQSJH(userModel.UserCode, this.OrganizeId);
                    string type = string.Empty;
                    FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();

                    if (_finRecRepos.checkSJH(lblFPHNew, userModel.UserCode, out sjEntity, out type, this.OrganizeId))
                    {
                        //fpsqlList.AddRange(fpsql); 
                        if (type == "insert")
                        {
                            _finRecRepos.AddReceiptInfo(sjEntity, this.OrganizeId); // Thêm số chứng từ biên lai
                        }
                        if (type == "update")
                        {
                            _finRecRepos.UpdateReceiptInfo(sjEntity, sjEntity.cwsjId); //Sửa số chứng từ biên lai
                        }
                    }
                    else
                    {
                        lblFPHNew = string.Empty;
                        throw new FailedCodeException("GET_SJH_FAIL");
                    }
                }
            }


            lblSsk = (mzJs.xjzf).ToString("0.00");


            //Thứ một lần yêu cầu giao dịch, phán đoán tài khoản có hay không phong tỏa 

            if (false == isReturnAll) //Một phần trả lại cần lại xin BHYT
            {
                // trư ớc yêu cầu giao dịch,tại tính giao dịch 
                newMzJs = applyBackPart(accountEntity, out sqxh, out jsdlList);
                newMzJs.zh = mzJs.zh;
            }


            //Khởi tạodữ liệu

            //Khởi tạođiều khiển 
            // initControl();
            // tải dữ liệu
            loadData(mzJs, isReturnAll, mzJs.jsnm);


            //Tất cả hoàn tiền
            // try
            // {
            bool result = isAlreadyComfirmTF;
            mzJs.CreateTime = DateTime.Now; //Lấy hệ thống máy chủ thời gian
            mzJs.CreatorCode = userModel.UserCode; // bản ghi hoàn phí thao tác viên 

            // pos máy toàn bộ số tiềnhoàn phí

            #region   pos机全额退费

            //Quyết toán phư ơng thức thanh toán bản ghi (Phư ơng thức hoàn tiền)
            jszffsArray = getJszffsEntityForSK();
            if (JszffsArray.Count == 1)
            {
                if (JszffsArray.Find(p => p.xjzffs == "1") != null)
                {
                    //pos Phư ơng thức
                    if (PosJY(kahao, mzJs, mzJs.xjzf, isReturnAll) == false)
                    {
                        //    this.Close();
                        return;
                    }
                }
            }
            else
            {
                // nhiều Phư ơng thức hoàn tiền,hoàn một phầnpos
                if (JszffsArray.Count > 1)
                {
                    OutpatientSettlementPaymentModelEntity mz_jszf = JszffsArray.Find(p => p.xjzffs == "1");
                    if (mz_jszf != null)
                    {
                        if (PosJY(kahao, mzJs, mz_jszf.zfje.ToDecimal(), IsReturnAll) == false)
                        {
                            // this.Close();
                            return;
                        }
                    }

                }
            }

            #endregion


            result = fullBack(Kh, accountEntity.isGh, mzJs, ghjsDt);


            //Thứlần thứ hai lần yêu cầu giao dịch 
            if (false == isReturnAll) //Một phần trả lại cần lại xin BHYT
            {
                // trư ớc yêu cầu giao dịch,tại tính giao dịch 
                newMzJs = applyBackPart(accountEntity, out sqxh, out jsdlList);
                newMzJs.zh = mzJs.zh;
                //Hoàn tiền sau lại lần nữa yêu cầu BHYTtínhthanh toán tiền mặt
                YBTKloadData();
            }




            //báo giá thiết bị hoàn tiền
            //string setFileName = (new Common.ConfigFile.AppConfig()).AppConfigGet("INIFileName");   // ConfigurationManager.AppSettings["INIFileName"].ToString();
            //string fileName = System.IO.Path.GetFullPath("../../") + "\\" + setFileName + ".ini";
            //IniFile Ini = new IniFile(fileName);
            //string BJQpzStr = Ini.IniReadValue(" bản máy tham số", "BJQPZparm");
            //decimal tk = Common.Tools.AnyToDecimal(lblYtk.Text);
            //BF<BjqInterface>.Instance.mz_tk(tk, ClsCurrentUserInfo.UserLogin, BJQpzStr);

            if (jszffsExListForSK.Count < 2 && this.posTxj == false && false == isReturnAll)
            {
                // nhiều phư ơng thức thanh toán hiển thịhoàn tiềngiao diện
                //this.btnOk.PerformClick();
                btnOk_Click(newMzJs, accountEntity, mzJs, jsdlList);
            }

        }

        /// <summary>
        /// theo hoàn toàn bộ,hoàn một phần, không hoàn lấychi tiết
        /// </summary>
        /// <returns>Hoàn toàn bộ,hoàn một phần, không hoàn chi tiết</returns>
        private void getRecordWithCategory(List<GridViewMx> GridViewMxList)
        {
            resultList["all"].Clear();
            resultList["any"].Clear();
            resultList["nothing"].Clear();
            foreach (var GridViewMx in GridViewMxList)
            {

                decimal tmpSl = 0.00M;
                decimal tmpReturnSl = 0.00M;

                decimal.TryParse(string.Format("{0}", GridViewMx.SL), out tmpSl);
                decimal.TryParse(string.Format("{0}", GridViewMx.RETURNS_SL), out tmpReturnSl);
                if (tmpSl == tmpReturnSl && GridViewMx.IS_RETURN == "true")
                {
                    resultList["all"].Add(GridViewMx); //Hoàn toàn bộ
                }
                if ((tmpSl > tmpReturnSl) && GridViewMx.IS_RETURN == "true") //Hoàn một phần
                {
                    resultList["any"].Add(GridViewMx);
                }
                else if ("false" == GridViewMx.IS_RETURN) // không hoàn 
                {
                    resultList["nothing"].Add(GridViewMx);
                }
            }
        }

        /// <summary>
        /// lấyhoàn một phần, không hoàn của thông tin quyết toán (1),lấyhoàn toàn bộ của thông tin quyết toán(2)
        /// </summary>
        /// <returns></returns>
        private JS_ENTITY getAccountList(int flag, OutpatientSettlementEntity fapiao, MZJS tmpDr)
        {
            int tmpGhnm;

            JS_ENTITY returnAny = new JS_ENTITY(); //Hoàn một phần của không hoàn một phần
            returnAny.patid = fapiao.patid;
            if (tmpDr != null)
            {
                returnAny.ghnm = tmpDr.GHNM;
            }
            returnAny.isGh = fapiao.jslx == "0";
            returnAny.jslx = fapiao.jslx;
            returnAny.brxz = fapiao.brxz;
            returnAny.jsxmList = new List<jsxm_entity>();
            returnAny.ghnmList = new List<int>();

            List<GridViewMx> listDataRow = new List<GridViewMx>();
            if (flag == 1)
            {
                listDataRow = resultList["any"];
                listDataRow.AddRange(resultList["nothing"]);
            }
            else
            {
                listDataRow = resultList["all"];
            }
            foreach (var GridViewMx in listDataRow)
            {
                decimal dj = 0.00M;
                decimal fwfdj = 0.00M;
                decimal je = 0.00M;
                decimal sl = 0.00M;
                decimal zfbl = 0.00M;
                decimal returnSl = 0.00M;
                string infoSet = string.Empty;

                DataRow tmpDataRow = GridViewMx.tag as DataRow;
                infoSet = string.Format("{0}", GridViewMx.InfoSet);

                dj = GridViewMx.DJ.ToDecimal(); //thêm phí dịch vụ Đ ơn giá
                fwfdj = GridViewMx.FWFDJ.ToDecimal();
                je = GridViewMx.JE.ToDecimal();
                sl = GridViewMx.SL.ToDecimal();
                zfbl = GridViewMx.ZFBL.ToDecimal();
                returnSl = GridViewMx.RETURNS_SL.ToDecimal();

                jsxm_entity tmpJsxm = new jsxm_entity();
                tmpJsxm.dj = (dj + fwfdj);
                tmpJsxm.dl = GridViewMx.DL;
                if (GridViewMx.IS_RETURN == "true")
                {
                    tmpJsxm.je = (dj + fwfdj) * (sl - returnSl);
                    tmpJsxm.sl = (sl - returnSl);
                }
                else
                {
                    tmpJsxm.je = (dj + fwfdj) * (sl);
                    tmpJsxm.sl = sl;
                }
                //tmpJsxm.mxbm = string.Empty;

                //thành nhóm số và số đ ơn thuốc
                tmpJsxm.czh = GridViewMx.czh;
                tmpJsxm.cfh = GridViewMx.CHUFANGHAO;


                if (infoSet == "mz_xm" || infoSet == "mz_ghxm")
                {
                    tmpJsxm.sfxm = string.Format("{0}", tmpDataRow["SFXM"]);
                    tmpJsxm.mxnm = tmpDataRow["XMNM"].ToInt();
                }
                else if (infoSet == "mz_cfmx")
                {
                    tmpJsxm.sfxm = string.Format("{0}", tmpDataRow["SFXM"]);
                    //tmpJsxm.mxbm = string.Format("{0}", tmpDataRow["SFXM"]);
                    tmpJsxm.cf_mxnm = GridViewMx.CF_MXNM.ToInt();
                }
                tmpJsxm.zfbl = zfbl;
                tmpJsxm.zfxz = string.Format("{0}", tmpDataRow["ZFXZ"]);
                tmpJsxm.Xmbm = string.Format("{0}", tmpDataRow["NBDL"]);
                returnAny.jsxmList.Add(tmpJsxm);

                //Th êmĐ ăng ký khám mã nội bộlist, qua lọc trùng lặp bản ghi 
                int.TryParse(string.Format("{0}", tmpDataRow["GHNM"]), out tmpGhnm);
                if (returnAny.ghnmList.Contains(tmpGhnm) == false)
                {
                    returnAny.ghnmList.Add(tmpGhnm);
                }
            }
            return returnAny;
        }

        /// <summary>
        /// Một phần trả lại yêu cầu BHYT giao dịch 
        /// </summary>
        /// <param name="accountEntity"></param>
        /// <param name="sqxh"></param>
        /// <returns></returns>
        private OutpatientSettlementEntity applyBackPart(JS_ENTITY accountEntity, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl)
        {
            bool ybError = true;
            OutpatientSettlementEntity newMzJs = getMzJs(accountEntity, true, out ybError, out sqxh, out jsdl,
                out mzjsReq, out mzssRep);
            //Nếu yêu cầu BHYT giao dịch thất bại,thì chuyển tự trả
            //if (ybError)
            //{
            //  
            // //if (DialogHelper.Ask_OkCancel("BHYT giao dịch thất bại,có hay không cần chuyển tự trả giao dịch ？") == DialogResult.OK)
            //    //{
            //    FrmBrxzEdit zfForm = new FrmBrxzEdit();
            //    zfForm.currBrxzEntity = BF<xt_brxz>.Instance.GetEntityByID(accountEntity.brxzbh.ToString());
            //    zfForm.brnm = accountEntity.brnm;
            //    if (zfForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            // jxyb = zfForm.jxyb;// tiếp tục BHYT giao dịch 
            //        if (!jxyb)
            //        {
            //            accountEntity.brxzbh = zfForm.currBrxzEntity.brxzbh;
            //            accountEntity.isZh = true;
            //            newMzJs = BF<mz_js>.Instance.getMzJs(accountEntity, out ybError, out sqxh, out jsdl, out mzjsReq, out mzssRep);
            //        }
            //    }
            //    else
            //    {
            //        jxyb = false;
            // throw new FailedCodeException("BHYT giao dịch thất bại!");
            //    }
            //    //}
            //}
            newMzJs.ysk = newMzJs.xjzf;
            newMzJs.zl = 0.00m;
            return newMzJs;
        }

        /// <summary>
        /// CầnQuyết toánCủaHạng mụcThông tin
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="needCalJm">Có hay khôngCầnTínhGiảm trừ</param>
        /// <param name="ybError"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, bool needCalJm, out bool ybError, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl, out NB_INTERFACE_MZJS_REQ msjsReq,
            out NB_INTERFACE_MZSS_REP mzssRep)
        {
            // không làmgiảm trừchức năng 
            //if (needCalJm)
            //{
            //    if (jsEntity.jsxmList == null)
            //        throw new FailedCodeException("JSXM_REQUIRED");

            //    var brxz = _RefundService.GetBrxzByBrbh(Tool.AnyToInt32(jsEntity.brxzbh));
            //    if (brxz == null)
            //        throw new FailedCodeException("OUTPAT_PATIENT_NATURE_IS_NOT_EXIST");

            // Tínhthu phí thuật toán:Hạng mục thu phí, chi phí giảm trừ, thu phí thuật toán 

            //    decimal outJmbl = 0m;
            //    decimal outJmje = 0m;
            //    foreach (jsxm_entity jsxm in jsEntity.jsxmList)
            //    {
            //        Calcjm(brxz.brxz, jsxm.dl, jsxm.sfxm, ((jsxm.dj + jsxm.fwfdj) * jsxm.sl).ToString(), out outJmbl, out outJmje);
            //        jsxm.jmbl = outJmbl;
            //        jsxm.jmje = outJmje;
            //    }
            //}

            return getMzJs(jsEntity, out ybError, out sqxh, out jsdl, out msjsReq, out mzssRep);
        }

        /// <summary>
        /// CầnQuyết toánCủaHạng mụcThông tin
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, out bool ybError, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl,
            out NB_INTERFACE_MZJS_REQ msjsReq, out NB_INTERFACE_MZSS_REP mzssRep)
        {
            msjsReq = null;
            mzssRep = null;

            // theo tính chất bệnh nhân, nhận đ ược truy cập BHYTgiao diện của phư ơng thức (này tính chất bệnh nhân chỉ dùng đ ể phán đoán truy cập cái nào BHYTgiao diện) 
            var brxz = _RefundService.GetBrxzByBrbh(jsEntity.brxz);
            if (brxz == null)
                throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");

            //if (brxz.ybjylx == Constants.ybjylx.ybjylx6)
            //{
            // // BHYT nông thôn mới giao dịch 
            //    return getMzJsNb(jsEntity, brxz, out ybError, out sqxh, out jsdl, out msjsReq, out mzssRep);
            //}
            //else
            //{
            // //Tự trả và BHYT giao dịch 
            //    return getMzJs(jsEntity, brxz, out ybError, out sqxh, out jsdl);
            //}
            return getMzJs(jsEntity, brxz, out ybError, out sqxh, out jsdl);
        }

        /// <summary>
        /// CầnQuyết toánCủaHạng mụcThông tin
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, SysPatientNatureEntity brxz, out bool ybError,
            out string sqxh, out List<OutpatientSettlementCategoryEntity> jsdl)
        {
            bool isScgh = false;
            Constants.ybDealLB jylx = Constants.ybDealLB.yb_deal_wjy;
            sqxh = string.Empty;
            ybError = false;

            if (jsEntity.jsxmList == null)
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            if (jsEntity.ghnmList == null || jsEntity.ghnmList.Count == 0)
                throw new FailedCodeException("GHNM_REQUIRED");

            OutpatientSettlementEntity js = new OutpatientSettlementEntity();
            // khác nhau tính chất bệnh nhân và cửa cấp cứu không thể cùng nhau quyết toán

            /// theo Đ ăng ký khám mã nội bộ lấy thông tin đ ăng ký khám
            List<OutpatientRegistEntity> ghList = _RefundService.getGhs(jsEntity.ghnmList);

            if (jsEntity.ghnmList.Count != ghList.Count)
                throw new FailedCodeException("MSG_ERROR");

            // khác nhau tính chất bệnh nhân và cửa cấp cứu không thể cùng nhau quyết toán
            checkGhjs(ghList);

            // theo Đ ăng ký khám mã nội bộ lấy thông tin đ ăng ký khám
            OutpatientRegistEntity gh = ghList[0];

            //Lấy thông tin bệnh nhân theo mã nội bộ bệnh nhân
            SysPatientBasicInfoEntity brjbxx = _RefundService.GetBrjbxxByPatid(jsEntity.patid);
            if (brjbxx == null)
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");

            //// theo tính chất bệnh nhân, nhận đ ược truy cập BHYTgiao diện của phư ơng thức (này tính chất bệnh nhân chỉ dùng đ ể phán đoán truy cập cái nào BHYTgiao diện) 
            //tbl_xt_brxz brxz = BF<xt_brxz>.Instance.GetEntityByID(jsEntity.brxzbh.ToString());
            //if (brxz == null)
            // throw new FailedCodeException(" này bệnh nhân của tính chất bệnh nhân tại hệ thống trong không tồn tại tại,vui lòng xác nhận！");

            //vìlà tồn tại tại kết hợp tính chất bệnh nhân, vì vậy không làm này phán đoán 
            //Nếu không tồn tại tạiBHYTvà tự trả chuyển đổi,khi trư ớc tính chất bệnh nhân phải và đăng ký khám giống nhau 
            //if (!jsEntity.isZh)
            //{
            //    if (gh.brxz != brxz.brxz)
            // throw new FailedCodeException("Khi trư ớc tính chất bệnh nhân và đăng ký khám tính chất bệnh nhân không nhất quán,vui lòng xác nhận！");
            //}
            //if (!jsEntity.isGh)
            //{
            // //Tính phí dịch vụ 
            //    CMS_Business cb = new CMS_Business();
            //    cb.CalFwf(brxz.brxz, jsEntity.jsxmList);
            //}

            // theo hạng mục thu phí và bệnh nhânthu phí thuật toán, nhận đ ược tính kết quả tập 
            Brsfsf_ReturnValue_YBANDFYB returnValue;
            getBrsfResultNB(jsEntity, brxz.brxz, out returnValue);


            js.jsnm = _RefundService.GetPrimaryKeyByTableName("mz_js");
            js.patid = brjbxx.patid;
            js.brxz = brxz.brxz;
            js.jslx = jsEntity.jslx;
            js.zlfy = returnValue.zlHj + returnValue.sfzlHj; //Chi phí tự chi trả
            js.zffy = returnValue.sfzfHj; //Chi phí tự trả
            js.flzffy = returnValue.flzfHj; //Chi phí tự trả theo phân loại
            js.jzfy = returnValue.jzfyHj; //Phí ghi sổ,nếu là BHYTbệnh nhân,là BHYTquay lại của kết quả
            js.jmje = returnValue.jmjeHj; //Chi phí giảm trừ
            js.jszt = (int)Constants.jsztEnum.YJ; //Trạng thái quyết toán

            js.xjzffs = string.Empty;
            // js.jch = jsEntity.jch;

            //Thanh toán tiền mặt
            decimal ssk = 0m;
            //Sai lệch tiền mặt
            decimal difference = 0m;
            //BHYTquay lại tự trả kết quả
            decimal ybzf = 0m;
            //BHYTquay lại có thể phí ghi sổ
            decimal jzfy = 0m;
            //Số tiền phải thu
            decimal ysk = 0m;

            string ybjylx = brxz.ybjylx;
            //Số tiền là0, không truy cập BHYTgiao diện

            ybjylx = Constants.ybjylx.ybjylx0;
            //if (returnValue.ybjsfwze == 0 && returnValue.jyze == 0)
            //{
            //    ybjylx = Constants.ybjylx.ybjylx0;
            //}
            ////Nếu là giư ờng gia đ ình và là đăng ký khám,thì không truy cập BHYTgiao diện, thông qua số tiền phán đoán 
            //if (ybjylx == Constant.ybjylx.ybjylx3 && jsEntity.isGh)
            //{
            //    ybjylx = Constant.ybjylx.ybjylx0;
            //}
            //BHYTbệnh nhân,và làtúi stomahạng mục, tiến hành túi stoma giao dịch 
            //if (ybjylx != Constants.ybjylx.ybjylx0)
            //{
            // //túi stomavà không phải túi stomahạng mục phải tách ra quyết toán
            //    bool zkd = isZkd(jsEntity);
            //    if (zkd)
            //    {
            //        ybjylx = "ZKD";
            //    }
            //}

            switch (ybjylx)
            {
                // không giao dịch 
                case Constants.ybjylx.ybjylx0:
                    ysk = returnValue.totalHj;
                    jylx = Constants.ybDealLB.yb_deal_wjy;
                    break;

                    #region

                    //Thông thư ờng giao dịch 
                    //case Constants.ybjylx.ybjylx1:
                    //    ybptjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    //    //Phí ghi sổ
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //Số tiền phải thu
                    //    ysk = returnValue.xjHj + ybzf;
                    // //Nếu là đ ăng ký khám thông thư ờng cho ngư ời khuyết tật,phí đ ăng ký khám đ ược ghi sổ
                    //    if (isScgh)
                    //    {
                    //        js.zlfy = js.zlfy - returnValue.YB.fybjsfwgrzf;
                    //        ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //    }
                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_mzgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_mzsf;
                    //    }
                    //    break;

                    ////bệnh nặng giao dịch 
                    //case Constants.ybjylx.ybjylx2:
                    //    ybdbjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    //    //Phí ghi sổ
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //Số tiền phải thu
                    //    ysk = returnValue.xjHj + ybzf;
                    // //Nếu là đ ăng ký khám thông thư ờng cho ngư ời khuyết tật,phí đ ăng ký khám đ ược ghi sổ
                    //    if (isScgh)
                    //    {
                    //        js.zlfy = js.zlfy - returnValue.YB.fybjsfwgrzf;
                    //        ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //    }

                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_dbgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_dbsf;
                    //    }
                    //    break;

                    //// giao dịch giư ờng gia đ ình 
                    //case Constants.ybjylx.ybjylx3:
                    // // giao dịch giư ờng gia đ ình tổngsố tiền(truyền cho BHYTcủa),số dư tài khoản,tổng lũy kế tiền mặt đ ã trả đoạn tự trả ngoại trú,Đ ịnh mức đoạn tự trả ngoại trú
                    //    decimal jc_Jyze = 0;
                    //    //decimal jc_zhye = 0, jc_mzzfdxjzfljs = 0, jc_mzzfdde = 0;
                    // //Có hay không nghỉ hưu, tạichức vụcấu hình của tỷ lệ 
                    //    decimal jc_bl = 0;
                    //    if (jsEntity.jslx == "1")
                    //    {
                    // jc_Jyze = returnValue.jyze;//(bao gồm phí ghi sổ) 
                    // // giư ờng gia đ ình ghi sổ mô phỏng giao dịch 
                    // //Lấy tổng lũy kế tiền mặt đ ã trả đoạn tự trả ngoại trú, đ ịnh mức đoạn tự trả ngoại trú
                    //        //DataTable dtYbzh = BLLFactory<yb_brzhbz>.Instance.getYBbrzhxx(brjbxx.kh);
                    //        //if (dtYbzh != null)
                    //        //{
                    //        //    if (dtYbzh.Rows.Count > 0)
                    //        //    {
                    //        //        jc_mzzfdxjzfljs = Tools.AnyToDecimal(dtYbzh.Rows[0]["mzzfdxjzfljs"].ToString());
                    //        //        jc_mzzfdde = Tools.AnyToDecimal(dtYbzh.Rows[0]["mzzfdde"].ToString());
                    //        //    }
                    //        //}

                    // // mô phỏng BHYT thuật toán update 51200 20150921
                    //        //jzfy = (jc_Jyze - jc_mzzfdxjzfljs - jc_mzzfdde) * jc_bl;//Phí ghi sổ BHYT có thể thanh toán
                    //        jzfy = jc_Jyze * (1 - jc_bl);//Phí ghi sổ BHYT có thể thanh toán
                    //        ybzf = jc_Jyze - jzfy;
                    //        //Phí ghi sổ
                    //        js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //        //Số tiền phải thu
                    //        ysk = returnValue.xjHj + ybzf;
                    //        jylx = Constants.ybDealLB.yb_deal_jcjz;
                    //    }
                    //    else
                    //    {
                    //        ybjcjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, jsEntity.jch, out ybzf, out jzfy, out ybError, out sqxh);
                    //        //Phí ghi sổ
                    //        js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //        //Số tiền phải thu
                    //        ysk = returnValue.xjHj + ybzf;

                    //        jylx = Constants.ybDealLB.yb_deal_jcsf;
                    //    }

                    //    break;

                    ////tai nạn lao động giao dịch 
                    //case Constants.ybjylx.ybjylx4:
                    //    ybgsjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh);
                    //    //Phí ghi sổ
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //Số tiền phải thu
                    //    ysk = returnValue.xjHj + ybzf;
                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_gsgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_gsmz;
                    //    }
                    //    break;

                    ////túi stoma giao dịch 
                    //case "ZKD":
                    //    ybmxxmjy(jsEntity.isGh, brjbxx, gh, brxz, jsEntity, out ybzf, out jzfy, out ybError, out sqxh);
                    //    //Phí ghi sổ
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //Số tiền phải thu
                    //    ysk = returnValue.xjHj + ybzf;

                    //    jylx = Constants.ybDealLB.yb_deal_sssf;
                    //    break;

                    #endregion
            }
            // giao dịch loại 
            js.jylx = ((int)jylx).ToString();
            //Chi phí tự trả
            js.zffy += ybzf;
            Ext.get5s6r(ysk, out ssk, out difference);
            //Thanh toán tiền mặt
            js.xjzf = ssk;
            //Sai lệch tiền mặt
            js.xjwc = difference;
            //Tổng số tiền:Chi phí tự chi trả+Chi phí tự trả+Chi phí tự trả theo phân loại+Phí ghi sổ
            js.zje = js.zlfy + js.zffy + js.flzffy + js.jzfy;
            //Lấy quyết toán loại lớn Đ ối tư ợng
            jsdl = getJsdl(js, returnValue);

            return js;
        }


        /// <summary>
        /// Tính hạng mục thu phí số tiền giảm trừ
        /// Bệnh nhânTrạng thái:1;--Hiệu lực
        /// thay đ ổi dấu hiệu:0;--chưa thay đ ổi 
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl"> loại lớn </param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmJe">Số tiền</param>
        /// <param name="outJmbl">Tỷ lệ giảm trừ</param>
        /// <param name="outJmje">Số tiền giảm trừ</param>
        /// <returns>count:Số tiền sau giảm trừ</returns>
        public decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, string parmJe, out decimal outJmbl,
            out decimal outJmje)
        {
            //Tỷ lệ giảm trừ,số tiền giảm trừ,giảm trừ sau của số tiền
            decimal jmbl = 0, jmje = 0, count = 0;
            //  DataTable dt = new DataTable();
            // dt = BLLFactory<xt_brsfjm>.Instance.GetXT_brsfjm("brxz='" + parmBrxz + "' and sfxm='" + parmSfxm + "'");

            var dt = _RefundService.GetXT_brsfjm(parmBrxz);
            if (dt.Count > 0)
            {
                jmbl = dt[0].jmbl.ToDecimal();
                if (jmbl < 0) //Tự trả đ ịnh mức
                {
                    count = parmJe.ToDecimal() + jmbl;
                    jmje = Math.Abs(jmbl);
                }
                else
                {
                    jmje = parmJe.ToDecimal() * jmbl;
                    count = parmJe.ToDecimal() - jmje;
                }
            }
            else
            {
                dt = _RefundService.GetXT_brsfjm("brxz='" + parmBrxz + "' and dl='" + parmDl + "' and sfxm=''");
                if (dt.Count > 0)
                {
                    jmbl = dt[0].jmbl.ToDecimal();
                    if (jmbl < 0) //Tự trả đ ịnh mức
                    {
                        count = parmJe.ToDecimal() + jmbl;
                        jmje = Math.Abs(jmbl);
                    }
                    else
                    {
                        jmje = parmJe.ToDecimal() * jmbl;
                        count = parmJe.ToDecimal() - jmje;
                    }
                }
            }
            outJmbl = jmbl;
            outJmje = jmje;
            return count;
        }

        /// <summary>
        /// nhiều thông tin đ ăng ký khám cùng nhau quyết toán, khác nhau tính chất bệnh nhân và cửa cấp cứu dấu hiệu không thể cùng nhau quyết toán
        /// </summary>
        /// <param name="ghList"></param>
        public void checkGhjs(List<OutpatientRegistEntity> ghList)
        {
            int brxzTotal = 0;
            int mjzTotal = 0;

            string brxzTemp = ghList[0].brxz;
            string mjzTemp = ghList[0].mjzbz;
            foreach (var gh in ghList)
            {
                if (gh.brxz == brxzTemp)
                    brxzTotal++;

                if (gh.mjzbz == mjzTemp)
                    mjzTotal++;
            }

            if (brxzTotal != ghList.Count)
                throw new FailedCodeException("JS_ERROR！");

            if (mjzTotal != ghList.Count)
                throw new FailedCodeException("JS_ERROR_2");
        }

        /// <summary>
        /// theo hạng mục thu phí và bệnh nhânthu phí thuật toán, nhận đ ược tính kết quả tập 
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="brxz"></param>
        /// <param name="returnValue"></param>
        public void getBrsfResultNB(JS_ENTITY jsEntity, string brxz, out Brsfsf_ReturnValue_YBANDFYB returnValue)
        {
            //CầnQuyết toánCủaHạng mụcDanh sách
            List<jsxm_entity> xmList = jsEntity.jsxmList;
            returnValue = new Brsfsf_ReturnValue_YBANDFYB();

            //Tải lênBHYTvà không tải lênBHYThạng mục
            List<jsxm_entity> xmListYB;
            List<jsxm_entity> xmListFYB;

            //Tính hạng mục thu phí chi phí
            //List<SFXM_FYMX> sfxmFYBDict;
            //List<SFXM_FYMX> sfxmYBDict;

            //Bệnh nhân thuật toán quay lại tính của kết quả tập 
            Brsfsf_ReturnValue returnValueYB;
            Brsfsf_ReturnValue returnValueFYB;

            //Lấy cần tải lênBHYTvà không tải lênBHYTcủa loại lớn 
            getYBANDFYB(xmList, out xmListYB, out xmListFYB);

            //Tải lên và không tải lênthu phí BHYThạng mục tổng cộng 
            getSfxmHj(xmListFYB);
            getSfxmHj(xmListYB);

            // theo hạng mục thu phí tổng cộng và bệnh nhânthu phí thuật toán tính
            //getBrsfResultNB(sfxmFYBDict, out returnValueFYB);
            //getBrsfResultNB(sfxmYBDict, out returnValueYB);
            getBrsfResult(xmListFYB, brxz, out returnValueFYB);
            getBrsfResult(xmListYB, brxz, out returnValueYB);

            returnValue.YB = returnValueYB;
            returnValue.FYB = returnValueFYB;
            // tạm thời không QuậnphânBHYTvà không phải BHYT
            //jsEntity.jsxmList.Clear();
            //jsEntity.jsxmList.AddRange(xmListYB);
            //jsEntity.jsxmList.AddRange(xmListFYB);
        }

        /// <summary>
        /// Có hay khôngtúi stoma
        /// </summary>
        /// <returns></returns>
        public bool isZkd(JS_ENTITY jsEntity)
        {
            int zkdTotal = 0;
            if (jsEntity == null)
                return false;

            var pz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_ZKD, OperatorProvider.GetCurrent().OrganizeId);
            if (pz == null || string.IsNullOrEmpty(pz.Value))
                return false;

            List<string> values = new List<string>(pz.Value.Split(','));
            foreach (jsxm_entity xm in jsEntity.jsxmList)
            {
                bool temp = values.Exists(t => t.ToString() == xm.sfxm && xm.mxnm != 0);
                if (temp)
                {
                    zkdTotal++;
                }
            }

            if (zkdTotal == 0)
                return false;
            else if (zkdTotal == jsEntity.jsxmList.Count)
                return true;
            else
                return true;

            // throw new FailedCodeException("túi stomahạng mục và không phải túi stomahạng mục không thể cùng nhau quyết toán！");
        }

        /// <summary>
        /// Lấy cần tải lênBHYTvà không tải lênBHYTcủa loại lớn 
        /// </summary>
        /// <param name="xmList"></param>
        /// <param name="xmListYB"></param>
        /// <param name="xmListFYB"></param>
        private void getYBANDFYB(List<jsxm_entity> xmList, out List<jsxm_entity> xmListYB,
            out List<jsxm_entity> xmListFYB)
        {
            xmListYB = new List<jsxm_entity>();
            xmListFYB = new List<jsxm_entity>();

            //Lấy ngoại trú cấu hình: không tải lênBHYT loại lớn 

            var entity = _sysConfigRepo.GetByCode(Constants.xtmzpz.DL_BSCYB, OperatorProvider.GetCurrent().OrganizeId);
            if (entity == null)
            {
                xmListYB = xmList;
                return;
            }

            string[] struDl = entity.Value.TrimEnd(',').Split(',');
            Dictionary<string, string> dlDict = new Dictionary<string, string>();
            foreach (string dl in struDl)
            {
                if (dlDict.ContainsKey(dl))
                    continue;

                dlDict.Add(dl, dl);
            }

            // không có cấu hình không tải lênBHYT
            if (struDl.Length == 0)
            {
                xmListYB = xmList;
                return;
            }

            // theo loại lớn Quậnphâncó hay không tải lênBHYT
            foreach (jsxm_entity xm in xmList)
            {
                if (dlDict.ContainsKey(xm.dl))
                {
                    xmListFYB.Add(xm);
                }
                else
                {
                    xmListYB.Add(xm);
                }
            }
        }

        /// <summary>
        /// Tính hạng mục thu phí tổng cộng 
        /// </summary>
        /// <param name="ghxmList"></param>
        /// <returns>Số tiền có thể ghi sổ,Số tiền tự trả theo phân loại,Số tiền tự chi trả,Số tiền giảm trừ</returns>
        public List<CMSEntity> getSfxmHj(List<OutpatientRegistItemEntity> ghxmList)
        {
            //Tính hạng mục thu phí tổng cộng chi phí
            ////key - loại lớn;value - số tiền có thể ghi sổ,số tiền tự trả theo phân loại,số tiền tự chi trả,giảm trừ
            List<CMSEntity> amountList = new List<CMSEntity>();
            Dictionary<string, CMSEntity> amountDict = new Dictionary<string, CMSEntity>();

            // theo hạng mục thu phí tính
            foreach (OutpatientRegistItemEntity ghxm in ghxmList)
            {
                // loại lớn 
                string dl = ghxm.dl;
                //Số tiền sau giảm trừ
                decimal jmhje = (decimal)ghxm.je - ghxm.jmje;
                if (!amountDict.ContainsKey(dl))
                    amountDict.Add(dl, new CMSEntity());

                // loại lớn 
                amountDict[dl].Dl = ghxm.dl;
                //Số tiền giảm trừ
                amountDict[dl].Jmje += ghxm.jmje;

                //Số tiền tự chi trả
                if (ghxm.zfxz == ((int)Constants.zfxzEnum.ZF).ToString())
                {
                    amountDict[dl].Zlfy += jmhje;
                }
                else if (ghxm.zfxz == ((int)Constants.zfxzEnum.KB).ToString())
                {
                    //Số tiền có thể ghi sổ
                    amountDict[dl].Kbje += jmhje;
                }
                else
                {
                    //Có thể thanh toán:Chi phí ×(1－tỷ lệ tự trả)
                    //Tự trả theo phân loại:Chi phí ×tỷ lệ tự trả
                    //Khi tỷ lệ tự trả là số âm giờ,biểu thị tự trả đ ịnh mức
                    if (ghxm.zfbl >= 0)
                    {
                        //Số tiền có thể ghi sổ
                        amountDict[dl].Kbje += (jmhje * (1 - (decimal)ghxm.zfbl));
                        //Số tiền tự trả theo phân loại
                        amountDict[dl].Flzf += (jmhje * (decimal)ghxm.zfbl);
                    }
                    else
                    {
                        //if (ghxm.jmbl < 0)
                        //{
                        // throw new FailedCodeException("Tỷ lệ giảm trừ và tỷ lệ tự trả chỉ có thể có một là định mức,vui lòng cấu hình lại！");
                        //}

                        //Số tiền tự trả theo phân loại: Đ ịnh mức * (1-tỷ lệ giảm trừ)* Số lư ợng
                        amountDict[dl].Flzf += -((decimal)ghxm.zfbl) * (1 - ghxm.jmbl) * ghxm.sl;
                        //Số tiền có thể ghi sổ
                        amountDict[dl].Kbje += jmhje - amountDict[dl].Flzf;
                    }
                }
            }

            foreach (CMSEntity entity in amountDict.Values)
            {
                amountList.Add(entity);
            }

            return amountList;
        }

        /// <summary>
        /// Tính hạng mục thu phí không làm tổng cộng 
        /// </summary>
        /// <param name="xmList"></param>
        /// <returns>Số tiền có thể ghi sổ,Số tiền tự trả theo phân loại,Số tiền tự chi trả,Số tiền giảm trừ</returns>
        public void getSfxmHj(List<jsxm_entity> xmList)
        {
            //xt_brsfsf.mzSfActiveList.Clear();
            ////Lấy tất cả các thuật toán 
            //List<tbl_xt_brsfsf> sfAllList = xt_brsfsf.mzSfActiveList;

            // theo hạng mục thu phí tính
            foreach (jsxm_entity xm in xmList)
            {
                //Số tiền sau giảm trừ
                decimal jmhje = (xm.dj + xm.fwfdj) * xm.sl - xm.jmje;

                //Số tiền tự chi trả
                if (xm.zfxz == ((int)Constants.zfxzEnum.ZF).ToString())
                {
                    xm.zlfy = jmhje;
                }
                else if (xm.zfxz == ((int)Constants.zfxzEnum.KB).ToString())
                {
                    //Số tiền có thể ghi sổ
                    xm.kbje = jmhje;
                }
                else
                {
                    //Có thể thanh toán:Chi phí ×(1－tỷ lệ tự trả)
                    //Tự trả theo phân loại:Chi phí ×tỷ lệ tự trả
                    //Khi tỷ lệ tự trả là số âm giờ,biểu thị tự trả đ ịnh mức
                    if (xm.zfbl >= 0)
                    {
                        //Số tiền có thể ghi sổ
                        xm.kbje = (jmhje * (1 - xm.zfbl));
                        //Số tiền tự trả theo phân loại
                        xm.flzf = (jmhje * xm.zfbl);
                    }
                    else
                    {
                        //if (xm.jmbl < 0)
                        //{
                        // throw new FailedCodeException("Tỷ lệ giảm trừ và tỷ lệ tự trả chỉ có thể có một là định mức,vui lòng cấu hình lại！");
                        //}

                        //Số tiền tự trả theo phân loại: Đ ịnh mức * (1-tỷ lệ giảm trừ)* Số lư ợng
                        xm.flzf = -(xm.zfbl) * (1 - xm.jmbl) * xm.sl;
                        //Số tiền có thể ghi sổ
                        xm.kbje = jmhje - xm.flzf;
                    }
                }
            }
        }

        /// <summary>
        /// theo hạng mục thu phí tổng cộng và bệnh nhânthu phí thuật toán tính
        /// </summary>
        /// <param name="amountList"></param>
        /// <param name="brxz"></param>
        public void getBrsfResult(List<jsxm_entity> amountList, string brxz, out Brsfsf_ReturnValue returnValue)
        {
            returnValue = new Brsfsf_ReturnValue();
            returnValue.dlFymxList = new List<Brsfsf_Dl_Fymx>();

            //Lấy tất cả các thuật toán 
            List<SysPatientChargeAlgorithmEntity> sfAllList = _RefundService.getMzActive();
            // theo thu phí thuật toán tính
            foreach (jsxm_entity entity in amountList)
            {
                Brsfsf_Dl_Fymx fymx = new Brsfsf_Dl_Fymx();
                // loại lớn 
                string dl = entity.dl;
                fymx.dl = dl;
                fymx.sfxm = entity.sfxm;
                fymx.mxnm = entity.mxnm;
                //fymx.mxbm = entity.mxbm;
                fymx.cf_mxnm = entity.cf_mxnm;
                fymx.czh = entity.czh;

                //Chi phí giảm trừ
                fymx.jmje = entity.jmje;

                //// theo tính chất bệnh nhân và loại lớn lấy bệnh nhânthu phí thuật toán 
                //List<tbl_xt_brsfsf> sfList = BF<xt_brsfsf>.Instance.getBrxzAndDl(brxz, dl);
                List<SysPatientChargeAlgorithmEntity> sfList = null;
                //Hạng mục thuật toán ưu tiên trư ớc cấp hơn cao
                if (entity.mxnm != 0)
                {
                    sfList = sfAllList.FindAll(t => t.brxz == brxz && t.sfxm == entity.sfxm);
                }
                // không có hạng mục của thuật toán,thì xem xét loại lớn 
                if (sfList == null || sfList.Count == 0)
                {
                    sfList = sfAllList.FindAll(
                        t => t.brxz == brxz && t.sfxm == "" && (t.dl == dl || t.dl.Trim() == "*"));
                }

                #region 没有病人收费算法

                //Nếu không có bệnh nhânthu phí thuật toán 
                if (sfList == null || sfList.Count == 0)
                {
                    // không có thuật toán tổng chi phí = có thể ghi sổ＋tự trả theo phân loại＋tự chi trả
                    fymx.total = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = entity.flzf;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                    //Tiền mặt
                    fymx.xj = entity.flzf + entity.zlfy;

                    entity.jyje = fymx.jzfy;
                    entity.jyfwje = fymx.jzfy + fymx.flzf;

                    returnValue.dlFymxList.Add(fymx);
                    continue;
                }

                #endregion

                #region 费用合计

                if (sfList[0].fyfw == Constants.fyfw.fyfw0)
                {
                    //Có thể ghi sổ : Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT - Tự trả theo phân loại;Tiền mặt:Tự trả theo phân loại+Tự chi trả+Tự trả
                    //Tiền mặt
                    fymx.xj = entity.flzf + entity.zlfy;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje;
                    //Tự trả theo phân loại
                    fymx.flzf = entity.flzf;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw1)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự chi trả+Tự trả
                    //Tiền mặt
                    fymx.xj = entity.zlfy;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw2)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    //Tiền mặt
                    fymx.xj = 0m;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = 0m;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả + Hoàn toàn tự trả:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    //Tiền mặt
                    fymx.xj = 0m;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = 0m;
                }

                #endregion

                #region 费用计算

                //TínhSố tiền
                decimal jsAmount = fymx.jzfy;
                // theo chi phí phạm vi cần tính của số tiền
                decimal jsedAmount = 0m;
                decimal sfzf = 0m;
                //Nếu có chi phí trên giới hạn,thì theo thuật toán cấp bậc tính(thực tế chỉcầntínhchi phí tự chi trả) 
                foreach (SysPatientChargeAlgorithmEntity sfEntity in sfList)
                {
                    //Nếu cùng một lô thuật toán của tính chất tự trả và chi phí phạm vi không nhất quán thì trực tiếp báo lỗi 
                    //if (sfEntity.zfxz != sfList[0].zfxz)
                    // throw new FailedCodeException("Khi trư ớc tính chất bệnh nhân và loại lớn của tính chất tự trả phải giống nhau,vui lòng cấu hình lại！");
                    //if (sfEntity.fyfw != sfList[0].fyfw)
                    // throw new FailedCodeException("Khi trư ớc tính chất bệnh nhân và loại lớn củachi phí phạm vi phải giống nhau,vui lòng cấu hình lại！");

                    decimal fysx = sfEntity.fysx * entity.sl;

                    if (fysx > fymx.jzfy)
                    {
                        sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        jsedAmount += jsAmount;
                        jsAmount = fymx.jzfy - jsedAmount;
                        break;
                    }
                    else
                    {
                        if (fysx > jsAmount)
                        {
                            jsedAmount += fysx - jsedAmount;
                            sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        }
                        else
                        {
                            if (fysx == 0)
                            {
                                sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                                jsedAmount = jsAmount;
                                break;
                            }
                            else
                            {
                                jsedAmount += fysx;
                                sfzf += calZfbl(fysx, sfEntity.zfbl, entity.sl);
                            }
                        }
                        jsAmount = fymx.jzfy - jsedAmount;
                    }
                }
                if (jsedAmount != fymx.jzfy)
                {
                    sfzf += fymx.jzfy - jsedAmount;
                }

                #endregion

                if (sfList[0].zfxz == ((int)Constants.sfZfxzEnum.ZL).ToString())
                {
                    //Tính chất tự trả là tự chi trả
                    fymx.sfzl += sfzf;
                }
                else
                {
                    fymx.sfzf += sfzf;
                }
                if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //Nếu là hoàn toàn tự trả,ghi sổ số tiền là tự chi trả
                    fymx.zl += fymx.jzfy - sfzf;
                    fymx.jzfy = 0m;
                    fymx.xj += fymx.zl;
                }
                else
                {
                    // có thể phí ghi sổ
                    fymx.jzfy = fymx.jzfy - sfzf;
                }
                //Tiền mặt
                fymx.xj += sfzf;
                //Có thuật toán tổng chi phí = tiền mặt
                fymx.total = fymx.xj;

                entity.jyje = fymx.jzfy;
                entity.jyfwje = fymx.jzfy + fymx.flzf;

                returnValue.dlFymxList.Add(fymx);
            }
        }

        /// <summary>
        /// theo tỷ lệ tự trả tính số tiền tự trả
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="zfbl"></param>
        /// <returns></returns>
        private decimal calZfbl(decimal amount, decimal zfbl, decimal sl)
        {
            if (zfbl >= 0)
            {
                return amount * zfbl;
            }
            else
            {
                if (amount > 0)
                    return -zfbl * sl;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Lấy cửa quyết toán loại lớn 
        /// </summary>
        /// <param name="js"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> getJsdl(OutpatientSettlementEntity js,
            Brsfsf_ReturnValue_YBANDFYB returnValue)
        {

            //Chi phí bảo hiểm y tế
            //foreach (Brsfsf_Dl_Fymx yb in returnValue.YB.dlFymxList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            // jsdl.dl = yb.dl;// loại lớn 
            //    jsdl.flzffy += yb.flzf;//Chi phí tự trả theo phân loại
            //    jsdl.zlfy += yb.zl + yb.sfzl;//Chi phí tự chi trả
            //    jsdl.jmfy += yb.jmje;//Số tiền giảm trừ
            //    if (returnValue.ybjsfwze == 0)
            //        if (0 == 0)
            //        {
            //            jsdl.zffy += 0m;
            //            jsdl.kbfy += 0m;
            //        }
            //        else
            //        {
            //            jsdl.zffy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.zffy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
            //            jsdl.kbfy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.jzfy;//Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
            //        }
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}
            // không phải chi phí bảo hiểm y tế
            //foreach (Brsfsf_Dl_Fymx yb in returnValue.FYB.dlFymxList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            // jsdl.dl = yb.dl;// loại lớn 
            //    jsdl.flzffy += yb.flzf;//Chi phí tự trả theo phân loại
            //    jsdl.zlfy += yb.zl + yb.sfzl;//Chi phí tự chi trả
            //    jsdl.jmfy += yb.jmje;//Số tiền giảm trừ

            //    jsdl.zffy += yb.sfzf;
            //    jsdl.kbfy += yb.jzfy;
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}

            foreach (Brsfsf_Dl_Fymx yb in returnValue.YB.dlFymxList)
            {
                OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
                if (jsdl == null)
                {
                    jsdl = new OutpatientSettlementCategoryEntity();

                }
                jsdl.jsnm = js.jsnm;
                jsdl.dl = yb.dl; // loại lớn 
                jsdl.zje = yb.total;
                jsdl.kbfy = 0;
                jsdl.flzffy += yb.flzf; //Chi phí tự trả theo phân loại
                jsdl.zlfy = yb.total; //Chi phí tự chi trả
                jsdl.jmfy += yb.jmje; //Số tiền giảm trừ
                jsdlList.Add(jsdl);
            }
            foreach (OutpatientSettlementCategoryEntity jsdl in jsdlList)
            {
                jsdl.flzffy = Ext.getsr(jsdl.flzffy);
                jsdl.zlfy = Ext.getsr(jsdl.zlfy);
                jsdl.jmfy = Ext.getsr(jsdl.jmfy);
                jsdl.zffy = Ext.getsr(jsdl.zffy);
                jsdl.kbfy = Ext.getsr(jsdl.kbfy);
                jsdl.zje = Ext.getsr(jsdl.zje);
            }

            return jsdlList;
        }

        /// <summary>
        /// túi stoma giao dịch 
        /// </summary>
        //public void ybmxxmjy(bool isGh, SysPatientBasicInfoEntity brjbxx, OutpatientRegistEntity gh, SysPatientNatureEntity brxz, JS_ENTITY jsEntity,
        //    out decimal ybzf, out decimal jzfy, out bool ybError, out string sqxh)
        //{
        //    sqxh = string.Empty;
        //    ybError = false;
        //    ybzf = 0m;
        //    jzfy = 0m;

        //    if (isGh)
        //    {
        // throw new FailedCodeException("BHYTĐ ăng ký khám không có chi tiết hạng mụcthực tếgiờ giao dịch ！");
        //    }
        //    else
        //    {
        //        tbl_yb_deal_sssf sssf = ybmxxmsfjy(brjbxx, gh, brxz, jsEntity, out ybError);
        // // theo BHYTquay lại của kết quả, nhận đ ược quyết toán dữ liệu
        // //Chi phí tự trả = số tiền mặt đ ã trả đoạn tự trả + số tiền mặt đ ã trả đoạn quỹ chung + số tiền mặt đ ã trả đoạn bổ sung.
        //        ybzf = Convert.ToDecimal(sssf.zf_xjzf) +
        //            Convert.ToDecimal(sssf.tc_xjzf) +
        //            Convert.ToDecimal(sssf.fj_xjzf);
        // //Phí ghi sổ = số tiền tài khoản đ ã trả trong năm + số tiền tài khoản đ ã trả các năm trư ớc + số tiền quỹ chung đ ã trả + số tiền bổ sung đ ã trả
        //        jzfy = Convert.ToDecimal(sssf.dn_zhzf) +
        //            Convert.ToDecimal(sssf.ln_zhzf) +
        //            Convert.ToDecimal(sssf.tc_zf) +
        //            Convert.ToDecimal(sssf.fj_zf);
        //        sqxh = sssf.sqxh;
        //    }
        //}

        private void loadData(OutpatientSettlementEntity mzJs, bool isReturnAll, int Jsnm)
        {

            if (IsReturnAll == true)
            {
                lblSsk = (mzJs.xjzf).ToString("0.00");
            }
            else
            {
                lblSsk = ((mzJs.xjzf) - (newMzJs.xjzf)).ToString("0.00");
            }
            lblYtk = lblSsk;
            jszffsExListForSK = new List<tbl_mz_jszffs_Ex>();
            jszffsExList = new List<tbl_mz_jszffs_Ex>();
            List<tbl_mz_jszffs_Ex> jszffsExListTemp = new List<tbl_mz_jszffs_Ex>();
            if (isReturnAll)
            {
                DataTable jszffsData = _RefundService.GetMzJszffsForJsnm(Jsnm);

                foreach (DataRow entiy in jszffsData.Rows)
                {
                    tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                    tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                    tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                    tbl_jszffs.zfje = entiy["zfje"].ToDecimal();
                    tbl_jszffs.ssry = entiy["ssry"].ToString();
                    tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                    tbl_jszffs.zh = entiy["zh"].ToInt();
                    // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                    jszffsExList.Add(tbl_jszffs);
                }
                // thu tiền quyết toán phư ơng thức thanh toán 
                jszffsExListForSK = jszffsExList;
            }
            else
            {
                DataTable jszffsData = _RefundService.GetMzJszffsForJsnm(Jsnm);

                foreach (DataRow entiy in jszffsData.Rows)
                {
                    tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                    tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                    tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                    tbl_jszffs.zfje = entiy["zfje"].ToDecimal();
                    tbl_jszffs.ssry = entiy["ssry"].ToString();
                    tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                    tbl_jszffs.zh = entiy["zh"].ToInt();
                    // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                    jszffsExListForSK.Add(tbl_jszffs);
                }
                if (jszffsData.Rows.Count == 1)
                {
                    foreach (DataRow entiy in jszffsData.Rows)
                    {
                        tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                        tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                        tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                        tbl_jszffs.zfje = lblYtk.ToDecimal();
                        tbl_jszffs.ssry = entiy["ssry"].ToString();
                        tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                        tbl_jszffs.zh = entiy["zh"].ToInt();
                        // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                        jszffsExList.Add(tbl_jszffs);
                        break;
                    }
                }
            }


            ////Tính tổng số tiền thực thu  ----fangyi
            //if (jszffsExListForSK.Count > 1)
            //{
            //    jsSSK();
            //}

        }

        /// <summary>
        /// Xác nhậnHoàn toàn bộ
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <param name="isGh">Có hay kh ôngĐ ăng ký khám</param>
        /// <param name="mzJs">Quyết toán ngoại trú</param>
        /// <param name="resultList">Chi tiết</param>
        private bool fullBack(string kh, bool isGh, OutpatientSettlementEntity mzJs,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt)
        {
            bool result = false;
            bool ybError = true;
            mzJs.jszt = 2;

            try
            {
                if (ybError)
                {

                    //Tự trả trùng lặp hoàn phídẫn đếnchèn nhiều dòng trùng lặp hoàn toàn bộ bản ghi 
                    if (_RefundService.getTFForFPH(mzJs.jsnm, mzJs.fph) > 0)
                    {
                        return true;
                    }
                    List<SysPatientAccountRevenueAndExpenseEntity> szjlList =
                        new List<SysPatientAccountRevenueAndExpenseEntity>();

                    //Hoàn tiền thành công,chènquyết toán ngoại trú,chi tiết quyết toán ngoại trú. đ ổi quyết toán ngoại trú Củajsnm. đ ổi chi tiết quyết toán mã nội bộ.
                    List<OutpatientSettlementDetailEntity> mzJsmxList = getMzJsMx(mzJs.jslx, mzJs.jsnm);

                    //var mzJszffsList = _RefundService.GetJszffsByJsnm(mzJs.jsnm);
                    //if (this.posTxj == true)
                    //{
                    //    mzJszffsList = getJszffsEntity();
                    //}

                    ////Thêm mới hệ thống bệnh nhân tài khoản thu chi bản ghi 
                    //szjlList = getZhszjlList(mzJs, mzJszffsList, "T");

                    //List<OutpatientSettlementCategoryEntity> jsdl = _RefundService.GetJsdlByJsnm(mzJs.jsnm);
                    mzJs.cxjsnm = mzJs.jsnm; // cũ quyết toán là trả lại mới của hủy bỏ mã nội bộ quyết toán 
                    mzJs.ghnm = mzJs.ghnm;
                    jsnmEx = mzJs.jsnm;
                    result = _RefundService.mzJsFullBack(mzJs, mzJsmxList, GhjsDt);
                }

            }
            catch (Exception ex)
            {
                throw new FailedException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Xác nhận một phần trả lại 
        /// </summary>
        /// <param name="newMzJs"></param>
        /// <param name="accountEntity"></param>
        /// <param name="jszffsArray"></param>
        private void backPart(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            List<OutpatientSettlementPaymentModelEntity> jszffsArray, string sqxh,
            List<OutpatientSettlementCategoryEntity> jsdlList)
        {
            bool ybError = false;
            string sqlUpdate = string.Empty;
            List<string> sqls = new List<string>();
            //Nếu tổng số tiền là0, không in hóa đ ơn
            if (newMzJs.zje != 0m)
            {
                if (fpsqlList != null && fpsqlList.Count > 0)
                {
                    sqls.AddRange(fpsqlList);
                }
                //Số hóa đ ơn
                newMzJs.fph = lblFPHNew.Trim();
            }
            else
            {
                newMzJs.fph = string.Empty;
            }

            try
            {
                List<OutpatientSettlementDetailEntity> listMzJsMx =
                    getMzJsMx(newMzJs.jsnm, newMzJs.jslx, accountEntity.jsxmList);

                try
                {
                    //if (newMzJs.jylx == "10")
                    //{

                    //    mzjsReq.fph = accountEntity.fph;
                    //    //NB_INTERFACE_MZJS_REP rep = BF<mz_js>.Instance.nbDealComfirm(mzjsReq, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);

                    //    //if (mzssRep == null || mzjsReq == null)
                    //    //{
                    //    //    ybError = true;
                    // // throw new FailedCodeException(" bảo hiểm nông nghiệp giao dịch thất bại！");
                    //    //}

                    // //Nếuthử tính và số tiền quyết toán không V.v.,thìlàm hủy bỏ thử tính và hủy bỏ quyết toán,lại tải trang
                    //    if (Convert.ToDecimal(mzssRep.bcze) != Convert.ToDecimal(rep.bcze) ||
                    //        Convert.ToDecimal(mzssRep.kbze) != Convert.ToDecimal(rep.kbze) ||
                    //        Convert.ToDecimal(mzssRep.zfze) != Convert.ToDecimal(rep.zfze))
                    //    {
                    //        if (DialogHelper.Ask_OkCancel("Kết quả tính thử ngoại trú không khớp với kết quả quyết toán ngoại trú, có quyết toán lại không？") == System.Windows.Forms.DialogResult.OK)
                    //        {
                    //            try
                    //            {
                    // // hủy bỏ quyết toán
                    //                BF<mz_js>.Instance.nbmzqxjs(mzjsReq.kbh, rep.bclsh, mzjsReq.yydm, mzjsReq.czydm, mzjsReq.czymc, newMzJs.jsnm, out ybError);
                    //            }
                    //            catch
                    //            {
                    // DialogHelper.DlgError(" hủy bỏ quyết toán thất bại！sẽ thông qua đ ối với sổ sách phư ơng thức cân bằng sổ sách！");
                    //            }
                    //            //isAlreadyComfirmYb = false;
                    //            //return;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    // // trong trung tâm số thứ tự là trống, không đi yêu cầu BHYTxác nhận giao dịch 
                    //    if (string.IsNullOrEmpty(sqxh) == false)
                    //    {
                    // //BHYT giao dịch xác nhận yêu cầu.
                    //        BF<mz_js>.Instance.ybDealComfirm(accountEntity.isGh, sqxh, accountEntity.ghnm, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                    ////NếuBHYT giao dịch thất bại,thì chuyển tự trả
                    //if (!ybError)
                    //    throw;

                    //// giư ờng gia đ ình không chuyển tự trả, tiếp tục thử lại giao dịch 
                    //if (accountEntity.jch > 0)
                    //{
                    //    jxyb = true;
                    //    throw;
                    //}

                    //FrmBrxzEdit zfForm = new FrmBrxzEdit();
                    //zfForm.currBrxzEntity = BF<xt_brxz>.Instance.GetEntityByID(accountEntity.brxzbh.ToString()); ;
                    //zfForm.brnm = accountEntity.brnm;
                    //zfForm.msg = ex.Message;
                    //if (zfForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    // jxyb = zfForm.jxyb;// tiếp tục BHYT giao dịch 
                    //    if (!jxyb)
                    //    {
                    // // chuyển tự trả
                    //        accountEntity.isZh = true;
                    //        accountEntity.brxzbh = zfForm.currBrxzEntity.brxzbh;
                    // //LạiKhởi tạo
                    //        FrmJS_Load(null, null);
                    //    }

                    //}
                    //else
                    //{
                    //    jxyb = false;
                    // throw new FailedCodeException("BHYT giao dịch thất bại!");
                    //}
                }
                //if (string.IsNullOrEmpty(sqlUpdate) && jxyb == true)
                //{
                //    try
                //    {
                // // trong trung tâm số thứ tự là trống, không đi yêu cầu BHYTxác nhận giao dịch 
                //        if (string.IsNullOrEmpty(sqxh) == false)
                //        {
                // //BHYT giao dịch xác nhận yêu cầu.
                //            BF<mz_js>.Instance.ybDealComfirm(accountEntity.isGh, sqxh, accountEntity.ghnm, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);
                //        }
                //    }
                //    catch
                //    {
                //        if (string.IsNullOrEmpty(sqlUpdate))
                //        {
                // throw new FailedCodeException("BHYT mạng vấn đề,vui lòng lạikéo xuống thẻ hoàn phí！");
                //        }
                //    }
                //}
                //BHYTSQL
                if (!string.IsNullOrEmpty(sqlUpdate))
                {
                    sqls.Add(sqlUpdate);
                }

                try
                {
                    newMzJs.cxjsnm = Jsnm; // cũ quyết toán là trả lại mới của hủy bỏ mã nội bộ quyết toán 

                    //Lấy tài khoản thông tin
                    //tbl_xt_brzh brzh = BF<xt_brzh>.Instance.getBrzhByZh(mzJs.zh);
                    //Hệ thống bệnh nhân tài khoản thu chi bản ghi 
                    List<SysPatientAccountRevenueAndExpenseEntity> szjlList = getZhszjlList(newMzJs, jszffsArray, "S");
                    ;
                    //thêmgiườngghi sổ hoàn một phầnlạithu không chènthu chi bản ghi 
                    //if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch > 0)
                    //{
                    //    szjlList = new List<tbl_xt_brzhszjl>();
                    //}

                    var result = _RefundService.InsertRemainFromReturns(newMzJs, listMzJsMx, jszffsArray, jsdlList);
                    if (false == result)
                        throw new FailedCodeException("JS_ERROR_FAIL");
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }



        ////Hoàn tiền sau lại lần nữa yêu cầu BHYTtínhthanh toán tiền mặt
        private void YBTKloadData()
        {
            lblSrce = newMzJs.xjwc.ToString("0.00");
            if (IsReturnAll == true)
            {
                //lblSsk.Text = mzJs.zje.ToString("0.00");
                lblSsk = (mzJs.xjzf).ToString("0.00");
            }
            else
            {
                //lblSsk.Text = (mzJs.zje - newMzJs.zje).ToString("0.00");
                lblSsk = ((mzJs.xjzf) - (newMzJs.xjzf)).ToString("0.00");
            }
            lblYtk = lblSsk;
            jszffsExList = new List<tbl_mz_jszffs_Ex>();

            if (isReturnAll == false)
            {
                List<OutpatientSettlementPaymentModelEntity> jszffsData = _RefundService.GetJszffsByJsnm(Jsnm);
                if (jszffsData.Count == 1)
                {
                    foreach (var entiy in jszffsData)
                    {
                        tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                        tbl_jszffs.jsnm = entiy.jsnm.ToInt();
                        tbl_jszffs.xjzffs = entiy.xjzffs;
                        tbl_jszffs.zfje = lblYtk.ToDecimal();
                        tbl_jszffs.ssry = entiy.ssry.ToString();
                        tbl_jszffs.ssrq = DateTime.Parse(entiy.ssrq.ToString());
                        tbl_jszffs.zh = entiy.zh.ToInt();
                        // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy.xjzffsmc);
                        jszffsExList.Add(tbl_jszffs);
                        break;
                    }
                }

            }
            //Tính tổng số tiền thực thu  ----fangyi
            if (jszffsExListForSK.Count > 1)
            {
                // jsSSK();
            }
        }

        /// <summary>
        /// LấyĐ ơn thuốc trạng thái
        /// </summary>
        private bool getPrescriptionStatus()
        {
            string statusDescription = string.Empty;
            string reqData = string.Empty;
            InterfaceRepParaBase<GET_CF_STATUS> interRepPara = null;
            bool jsgb = false;
            try
            {
                return true;
                //Lấy thực tế cần hoàn phí của thông tin
                List<GET_CF_STATUS> tyCfList = getTyInfo();
                if (tyCfList == null || tyCfList.Count == 0)
                    return true;

                //Y(nhiều Đ ơn thuốctheo","phân tách)
                tyCfList.ForEach(t => reqData += t.prescriptionNo.ToString() + ",");
                //tyCfList.ForEach(delegate(GET_CF_STATUS item)
                //{
                //    reqData += item.prescriptionNo.ToString() + ",";
                //});
                reqData = reqData.TrimEnd(',');
                //reqData = JsonConvert.SerializeObject(reqData); // loại bỏ thêm\"Củatham số

                // yêu cầu giao diện
                //   WebPostAPI.API.post<GET_CF_STATUS>(Constant.InterfaceConstant.GET_CF_STATUS, reqData, out interRepPara, out statusDescription, out jsgb);
                jsgb = true;
                //Giao di ệnĐ óng,trực tiếpquay lại
                if (jsgb)
                {
                    return true;
                }
                //Hệ thống bất thư ờng
                if (interRepPara == null)
                {
                    // DialogHelper.DlgError("Giao diện truy cập bất thư ờng:" + statusDescription);
                    //return false;
                    return true;
                }
                else
                {
                    //Nếu có nghiệp vụ bất thư ờng,thì nhắc nhở
                    if (interRepPara.failureMessage != null && interRepPara.failureMessage.Count > 0)
                    {
                        //       DialogHelper.DlgError(interRepPara.failureMessageShow);
                        //return false;
                        return true;
                    }
                    else
                    {
                        // phán đoán Đ ơn thuốc trạng thái
                        string tempResult = JsonConvert.SerializeObject(interRepPara.result);
                        // return isAllowTy(tyCfList, tempResult);
                    }
                }
            }
            catch (Exception ex)
            {
                //DialogHelper.DlgError("LấyĐ ơn thuốc trạng tháigiao diện xử lý thất bại:" + ex.Message);
                //return false;
                return true;
            }
        }

        /// <summary>
        /// Lấy thực tế trả thuốc thông tin
        /// </summary>
        /// <returns></returns>
        private List<GET_CF_STATUS> getTyInfo()
        {
            //Hoàn toàn bộ CủaĐ ơn thuốc
            DataTable mzCfMxDt = _RefundService.GetMz_cfmxRecords(jsnm);
            List<GET_CF_STATUS> tyCfList = new List<GET_CF_STATUS>();
            foreach (DataRow dr in mzCfMxDt.Rows)
            {
                //Có hay kh ôngĐ ã bao gồm Đ ơn thuốc
                GET_CF_STATUS cf = tyCfList.Find(t => t.prescriptionNo == dr["cfh"].ToString());
                if (cf == null)
                {
                    cf = new GET_CF_STATUS();
                    cf.returnDrugInfo = new List<returnDrugInfo>();
                    //cf.prescriptionNo = dr["pt_cfnm"].ToString() == "0" ? Tools.AnyToString(dr["cfh"]) : dr["pt_cfnm"].ToString();
                    cf.prescriptionNo = dr["cfh"].ToString();
                    tyCfList.Add(cf);
                }
                //Đ ơn thuốc trong có hay kh ôngĐ ã bao gồm thuốc
                returnDrugInfo yp = cf.returnDrugInfo.Find(t => t.drugCode == dr["yp"].ToString());
                if (yp == null)
                {
                    yp = new returnDrugInfo();
                    yp.drugCode = dr["yp"].ToString();
                    cf.returnDrugInfo.Add(yp);
                }

                if (AccountEntity.jsxmList == null)
                    AccountEntity.jsxmList = new List<jsxm_entity>();

                jsxm_entity jsxm = AccountEntity.jsxmList.Find(t => t.cfh == dr["cfh"].ToString()
                                                                    //&& t.mxbm == dr["yp"].ToString()
                                                                    && t.czh == dr["czh"].ToString());

                //Nếu không thu lần thì tính hoàn toàn bộ
                decimal returnCount = 0;
                if (jsxm == null)
                    returnCount = 0;
                else
                    returnCount = jsxm.sl;

                //Thuốcđóng góimã là 3,thì số lư ợng là ba cấp số lư ợng,thuốcđóng góimã là 4,thì số lư ợng là bốn cấp Đ ơn vị
                if (dr["ypbzdm"].ToString() == "3")
                    yp.thirdLevelPkgQty = decimal.ToInt32(Convert.ToDecimal(dr["sl"].ToString()) - returnCount);
                else
                    yp.fourthLevelPkgQty = decimal.ToInt32(Convert.ToDecimal(dr["sl"].ToString()) - returnCount);
            }

            return GetInfoForSelect(tyCfList);
        }

        /// <summary>
        /// Quyết toánDữ liệuXác minh
        /// </summary>
        /// <returns></returns>
        private bool checkJs()
        {
            //Số tiền phải thu >= số tiền thực thu - Chênh lệch làm tròn？
            decimal Ytk = Convert.ToDecimal(lblYtk);
            decimal ssk = Convert.ToDecimal(lblSsk);
            decimal srce = Convert.ToDecimal(lblSrce);

            //if ((Ytk-ssk) > Ytk)
            if (ssk > Ytk)
            {
                throw new FailedException("退款必须小于等于收款。");
                // txtXj.Focus();
                //  return false;
            }
            if (ssk != Ytk)
            {
                throw new FailedException("实际退款金额不对，请确认。");
                // txtXj.Focus();
                //  return false;
            }

            return true;
        }

        /// <summary>
        /// Phư ơng thức thanh toán quyết toán ngoại trú bản ghi --hoàn tiền
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntity()
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsList =
                new List<OutpatientSettlementPaymentModelEntity>();
            foreach (tbl_mz_jszffs_Ex entity in jszffsExList)
            {
                OutpatientSettlementPaymentModelEntity jszffs = (OutpatientSettlementPaymentModelEntity)entity;
                jszffsList.Add(jszffs);
            }

            return jszffsList;
        }

        private void btnOk_Click(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            OutpatientSettlementEntity mzJs, List<OutpatientSettlementCategoryEntity> jsdlList)
        {
            //Tự trả chuyển BHYT không phán đoán Vượng Vângiao diện
            if (IsChangeBrxz == false)
            {
                if (!getPrescriptionStatus())
                    return;
            }

            if (!checkJs())
                return;
            //Quyết toán phư ơng thức thanh toán bản ghi (Phư ơng thức hoàn tiền)
            jszffsArray = getJszffsEntity();

            // phư ơng thức thu tiền - loại bỏ Phư ơng thức hoàn tiền thì là hoàn một phần của quyết toán phư ơng thức 
            var jszffsArrayForSK = getJszffsEntity(jszffsArray);
            if (IsReturnAll == false && accountEntity.jsxmList.Count > 0 && accountEntity.isGh == false)
            {
                bool flag = true;

                //thêmgiườngghi sổ hoàn một phầnin chứng từ(bảng kê)
                if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch != 0)
                {
                    flag = false;
                }
                if (flag == true)
                {
                    if (string.IsNullOrEmpty(lblFPHNew))
                    {
                        throw new FailedCodeException("GET_FPH_FAIL");
                        // return;
                    }
                    //Lại xác minhphiếu số 
                    _RefundService.CheckFPH(lblFPHNew);
                }
                else
                {
                    //chứng từ số 
                    if (string.IsNullOrEmpty(lblFPHNew))
                    {
                        throw new FailedCodeException("GET_SJH_FAIL");
                        //  return;
                    }
                    //Lạikiểm tra số biên lai 
                    checkSJH(lblFPHNew);
                }
            }

            try
            {
                //Tất cả hoàn phí BHYT giao dịch quyết toán
                mzJs.CreatorCode = userModel.UserCode;
                mzJs.CreateTime = DateTime.Now;

                bool result = isAlreadyComfirmTF;
                //if (isAlreadyComfirmTF == false && this.posTxj == true&& false == IsReturnAll)
                //{
                //    result = fullBack(Kh, accountEntity.isGh, mzJs);
                //}
                //if (false == IsReturnAll && result)
                //if (false == IsReturnAll)
                //{
                //    string errCode = "";

                #region 调pos机接口操作

                //Hoàn một phầnposHoàn tiền
                //if (jszffsArrayForSK.Find(p => p.xjzffs == "1") != null && jszffsArrayForSK.Count == 1)
                //{
                //    if (this.isAlreadyPosCX == true)
                //    {
                //        errCode = PosSfInterface(Kh, lblFPHNew, (mzJs.xjzf - Tool.AnyToDecimal(lblSsk)), newMzJs.jsnm);
                // // hủy giao dịch sau thu phí
                //        if (errCode != "00")
                //        {
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        errCode = PosTfInterface(Kh, mzJs.fph, Tool.AnyToDecimal(lblSsk));
                //        if (errCode != "00")
                //        {
                //            //this.Close();
                //            return;
                //        }
                //    }
                //}
                // nhiều Phư ơng thức hoàn tiền,hoàn một phầnpos
                //if (jszffsArrayForSK.Count > 1)
                //{
                //    tbl_mz_jszffs mz_jszf = jszffsArrayForSK.Find(p => p.xjzffs == "1");
                //    if (mz_jszf != null)
                //    {
                //        if (PosJY(Kh, mzJs, Common.Tools.AnyToDecimal(mz_jszf.zfje), IsReturnAll) == false)
                //        {
                //            //this.Close();
                //            return;
                //        }

                //        if (this.isAlreadyPosCX == true)
                //        {
                //            if (PosSfInterface(Kh, lblFPHNew.Text, Common.Tools.AnyToDecimal(mz_jszf.zfje), newMzJs.jsnm) != "00")
                //            {
                //                //this.Close();
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            if (PosTfInterface(Kh, mzJs.fph, Common.Tools.AnyToDecimal(mz_jszf.zfje)) != "00")
                //            {
                //                //this.Close();
                //                return;
                //            }
                //        }
                //    }

                //}

                #endregion

                //Xác nhận một phần trả lại,lạithumột phần
                //backPart(newMzJs, accountEntity, jszffsArray, sqxh);
                newMzJs.CreatorCode = userModel.UserCode;
                newMzJs.CreateTime = DateTime.Now;
                //BHYTHoàn một phầnThu phí
                backPart(newMzJs, accountEntity, jszffsArrayForSK, sqxh, jsdlList);

                //if (IsChangeBrxz == false)
                //{
                // throw new FailedCodeException("Hoàn phí thành công！ số tiền hoàn lại:" + lblSsk);
                //}
                //else
                //{
                // throw new FailedCodeException("Tự trả chuyển BHYTthành công！ số tiền hoàn lại:" + lblSsk);
                //}

                // if ((newMzJs.zje!= 0 && newMzJs.jslx!= "0") || (newMzJs.zje!= 0 && newMzJs.jslx == "0" && IsChangeBrxz == true)) //Loại quyết toán không là đăng ký khám
                //    {
                //        bool flag = true;

                // //1 giư ờng gia đ ình ghi sổ(bao gồm giư ờng gia đ ình ghi sổ) jylx=5(giư ờng gia đ ình thu phí),jylx=14(giư ờng gia đ ình ghi sổ)
                //        //if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch > 0)
                //        //{
                //        //    flag = false;
                //        //}
                //        if (flag == true)
                //        {
                // //In hóa đ ơn
                //            //   printBill(newMzJs, accountEntity);
                //        }
                //        else
                //        {
                //            //Lấy số dư tài khoản
                //            //decimal jc_zhye = 0.00M;
                //            //tbl_xt_brzhszjl entitySz = new tbl_xt_brzhszjl();
                //            //entitySz = BLLFactory<xt_brzhszjl>.Instance.getLastestJlBybrnm(newMzJs.patid);
                //            //if (entitySz != null)
                //            //{
                //            //    jc_zhye = entitySz.zhye;
                //            //}
                // ////lịch sử lần ghi sổ+khi trư ớc ghi sổ và số dư tài khoảnso sánh
                // ////lịch sử lần ghi sổ củatổngsố
                //            //decimal compareJe = 0m;
                //            //getJcbrjzxx(Tools.AnyToInt32(newMzJs.brnm), newMzJs.brxz, newMzJs.jch, out compareJe);
                // ////Inbảng kê 
                //            //DataTable jc = BLLFactory<mz_jsmx>.Instance.getSFQDForJsnm(newMzJs.jsnm);
                //            ////PrintBill print = new PrintBill(jc, newMzJs.xjzf);
                //            //tbl_xt_brjbxx brjbxx = BF<xt_brjbxx>.Instance.GetEntityByID(newMzJs.brnm.ToString());
                //            //PrintBill print = new PrintBill(jc, newMzJs.xjzf, brjbxx.xm, jc_zhye - Convert.ToDecimal(newMzJs.flzffy) - newMzJs.xjzf - compareJe);
                //            //print.ReportPrint();

                //        }
                //    }
                //    // btnOk.Enabled = true;
                //    //  DialogResult = DialogResult.OK;
                //    //this.Close();
                //}
                //else
                //{
                //    //if (result)
                //    //{
                //    //    if (IsChangeBrxz == false)
                //    //    {
                // // throw new FailedCodeException("Hoàn phí thành công！ số tiền hoàn lại:" + lblSsk);
                //    //    }
                //    //    else
                //    //    {
                // // throw new FailedCodeException("Tự trả chuyển BHYTthành công！ số tiền hoàn lại:" + lblSsk);
                //    //    }
                //    //}
                //}

                ////Đ ơn thuốc hoàn phí thông báo 
                //returnMoneyOfPrescriptionNo();
            }
            catch
            {
                throw;
            }
            //DialogResult = DialogResult.OK;
            try
            {
                // phư ơng thức thanh toán là tiền mặt thìKhởi động hộp tiền
                //if (JszffsArray.Find(p => p.xjzffs == "0") != null)
                //{
                //    string setFileName = (new Common.ConfigFile.AppConfig()).AppConfigGet("INIFileName");   // ConfigurationManager.AppSettings["INIFileName"].ToString();
                //    string fileName = System.IO.Path.GetFullPath("../../") + "\\" + setFileName + ".ini";
                //    IniFile Ini = new IniFile(fileName);
                // string ckpzStr = Ini.IniReadValue(" bản máy tham số", "QX_CKPZparm");
                //    if (!string.IsNullOrEmpty(ckpzStr))
                //    {
                //        SerialPort port = new SerialPort();
                // port.PortName = ckpzStr;//Thiết lập cổng nối tiếp
                // port.Open();//Mở cổng nối tiếp
                // port.Write("#$1b#$70#0#$3c#$ff");//Ghi vào cổng nối tiếp lệnh khởi động hộp tiềncủalệnh
                // port.Close();//Giải phóng cổng nối tiếp
                //    }
                // else { DialogHelper.DlgError("Cổng nối tiếp của hộp tiềnchưa cấu hình!"); }
                //}
            }
            catch
            {
            }
            //   this.Close();
        }

        /// <summary>
        /// Phư ơng thức thanh toán quyết toán ngoại trú bản ghi --hoàn tiềncòn lại dư ới của thu tiền 
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntity(
            List<OutpatientSettlementPaymentModelEntity> jszffsListForTK)
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsList =
                new List<OutpatientSettlementPaymentModelEntity>();
            List<OutpatientSettlementPaymentModelEntity> jszffsListFortemp =
                new List<OutpatientSettlementPaymentModelEntity>();
            jszffsListFortemp = jszffsListForTK;
            foreach (tbl_mz_jszffs_Ex entity in jszffsExListForSK)
            {
                OutpatientSettlementPaymentModelEntity jszffs = ChangeTbl_mz_jszffs_Ex(entity);
                //var jszffsForTk = jszffsListForTK.Find(p => p.zfbh == jszffs.zfbh && p.xjzffs == jszffs.xjzffs);
                var jszffsForTk = jszffsListForTK.Find(p => p.xjzffs == jszffs.xjzffs);
                if (jszffsForTk != null)
                {
                    jszffs.zfje = (jszffs.zfje - jszffsForTk.zfje);

                    jszffsListFortemp.Remove(jszffsForTk);
                }
                else
                {
                    if (jszffsExListForSK.Count == 1)
                    {
                        jszffs.zfje = (jszffs.zfje - this.lblSsk.ToDecimal());
                        jszffsListFortemp.Remove(jszffsForTk);
                    }
                    //else
                    //{
                    //    jszffsForTk = jszffs;
                    //    if (jszffsExListForSK.Count > 1 && jszffsForTk != null)
                    //    {
                    //        jszffsForTk.zfje = -(jszffsForTk.zfje);
                    //        jszffsListFortemp.Add(jszffsForTk);
                    //    }
                    //}
                }

                //Sửathao tác viên 
                jszffs.ssry = userModel.UserCode;
                jszffs.ssrq = DateTime.Now;
                jszffsList.Add(jszffs);
            }
            //Phư ơng thức hoàn tiền không là phư ơng thức thu tiền trong bất kỳ một loại 
            foreach (var entity in jszffsListFortemp)
            {
                entity.zfje = -(entity.zfje);
                //Sửathao tác viên 
                entity.ssry = userModel.UserCode;
                entity.ssrq = DateTime.Now;

                jszffsList.Add(entity);
            }
            return jszffsList;
        }

        public OutpatientSettlementPaymentModelEntity ChangeTbl_mz_jszffs_Ex(tbl_mz_jszffs_Ex ex)
        {
            OutpatientSettlementPaymentModelEntity zffs = new OutpatientSettlementPaymentModelEntity();
            zffs.jsnm = ex.jsnm;
            zffs.mzjszffsbh = ex.mzjszffsbh;
            zffs.ssrq = ex.ssrq;
            zffs.ssry = ex.ssry;
            zffs.xjzffs = ex.xjzffs;
            zffs.zfje = ex.zfje;
            zffs.zh = ex.zh;
            return zffs;
        }

        /// <summary>
        /// Chi tiết quyết toán ngoại trú
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <param name="jslx">Loại quyết toán</param>
        /// <param name="xmList">Quyết toánHạng mụcChi tiết</param>
        /// <returns></returns>
        private List<OutpatientSettlementDetailEntity> getMzJsMx(int jsnm, string jslx, List<jsxm_entity> xmList)
        {
            List<OutpatientSettlementDetailEntity> resultList = new List<OutpatientSettlementDetailEntity>();
            foreach (jsxm_entity item in xmList)
            {
                OutpatientSettlementDetailEntity tmpItem = new OutpatientSettlementDetailEntity();
                tmpItem.sl = item.sl;
                tmpItem.jslx = jslx;
                tmpItem.jsnm = jsnm;
                tmpItem.mxnm = item.mxnm;
                tmpItem.cf_mxnm = item.cf_mxnm;
                tmpItem.jyje = item.jyje;
                tmpItem.jyfwje = item.jyfwje;
                resultList.Add(tmpItem);
            }
            return resultList;
        }

        /// <summary>
        /// Đ ơn thuốc hoàn phí thông báo 
        /// </summary>
        private void returnMoneyOfPrescriptionNo()
        {
            string statusDescription = string.Empty;
            string reqData = string.Empty;
            InterfaceRepParaBase<ADD_CF_TFTZ> interRepPara = null;
            bool jsgb = false;
            try
            {
                //Lấy thực tế cần hoàn phí của thông tin,mượn dùng (LấyĐ ơn thuốc trạng thái)CủaĐ ối tư ợng
                List<GET_CF_STATUS> tyCfList = getTyInfo();
                if (tyCfList == null || tyCfList.Count == 0)
                    return;

                List<ADD_CF_TFTZ> tfTzList = new List<ADD_CF_TFTZ>();
                foreach (GET_CF_STATUS cfStatus in tyCfList)
                {
                    //Nếu không có thuốc thông tin,thì biểu thị không hoàn 
                    if (cfStatus.returnDrugInfo == null)
                        continue;

                    ADD_CF_TFTZ tftz = new ADD_CF_TFTZ();
                    tftz.pn = cfStatus.prescriptionNo;
                    tftz.pd = new List<pd>();

                    foreach (returnDrugInfo yp in cfStatus.returnDrugInfo)
                    {
                        if (yp.fourthLevelPkgQty == 0 && yp.thirdLevelPkgQty == 0)
                        {
                            continue;
                        }
                        pd pd = new pd();
                        pd.dc = yp.drugCode;
                        pd.fq = yp.fourthLevelPkgQty;
                        pd.tq = yp.thirdLevelPkgQty;

                        tftz.pd.Add(pd);
                    }
                    if (tftz.pd.Count > 0)
                    {
                        tfTzList.Add(tftz);
                    }
                }

                if (tfTzList.Count < 1)
                {
                    // không có cần trả lại CủaĐ ơn thuốc
                    return;
                }

                reqData = JsonConvert.SerializeObject(tfTzList);

                // yêu cầu giao diện
                // WebPostAPI.API.post<ADD_CF_TFTZ>(Constants.InterfaceConstant.ADD_CF_TFTZ, reqData, out interRepPara, out statusDescription, out jsgb);
                jsgb = true;
                //Giao di ệnĐ óng,trực tiếpquay lại
                if (jsgb)
                {
                    return;
                }
                //Hệ thống bất thư ờng
                if (interRepPara == null)
                {
                    // throw new FailedCodeException("Giao diện truy cập bất thư ờng:" + statusDescription);
                    //   return;
                }
                else
                {
                    //Nếu có nghiệp vụ bất thư ờng,thì nhắc nhở
                    if (interRepPara.failureMessage != null && interRepPara.failureMessage.Count > 0)
                    {
                        //   throw new FailedCodeException(interRepPara.failureMessageShow);
                        //  return;
                    }
                }
            }
            catch (Exception ex)
            {
                // throw new FailedCodeException("Đ ơn thuốc hoàn phí thông báo giao diện xử lý thất bại:" + ex.Message);
                //  return;
            }
        }

        //Lọc không có trả thuốc CủaĐ ơn thuốc và trả thuốc số lư ợng là0 của thuốc
        private List<GET_CF_STATUS> GetInfoForSelect(List<GET_CF_STATUS> tyCfList)
        {
            List<GET_CF_STATUS> retCfList = new List<GET_CF_STATUS>();
            foreach (GET_CF_STATUS cfStatus in tyCfList)
            {
                //Nếu không có thuốc thông tin,thì biểu thị không hoàn 
                if (cfStatus.returnDrugInfo == null)
                    continue;

                GET_CF_STATUS retcfStatus = new GET_CF_STATUS();
                retcfStatus.prescriptionNo = cfStatus.prescriptionNo;
                retcfStatus.status = cfStatus.status;
                retcfStatus.returnDrugInfo = new List<returnDrugInfo>();

                foreach (returnDrugInfo yp in cfStatus.returnDrugInfo)
                {
                    if (yp.fourthLevelPkgQty == 0 && yp.thirdLevelPkgQty == 0)
                    {
                        continue;
                    }
                    retcfStatus.returnDrugInfo.Add(yp);
                }

                if (retcfStatus.returnDrugInfo.Count > 0)
                {
                    retCfList.Add(retcfStatus);
                }
            }
            return retCfList;
        }

        /// <summary>
        /// Phư ơng thức thanh toán quyết toán ngoại trú bản ghi --gốc thu tiền 
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntityForSK()
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsSKList =
                new List<OutpatientSettlementPaymentModelEntity>();
            foreach (tbl_mz_jszffs_Ex entity in jszffsExListForSK)
            {
                OutpatientSettlementPaymentModelEntity jszffs = (OutpatientSettlementPaymentModelEntity)entity;
                jszffsSKList.Add(jszffs);
            }

            return jszffsSKList;
        }

        private bool PosJY(string kh, OutpatientSettlementEntity paramMzjs, decimal xjzf, bool flag)
        {
            return true;
        }



        /// <summary>
        /// Chi tiết quyết toán ngoại trú_ trả lại 
        /// </summary>
        /// <param name="jslx">Loại quyết toán</param>
        /// <param name="detailList">Chi tiết</param>
        /// <returns></returns>
        private List<OutpatientSettlementDetailEntity> getMzJsMx(string jslx, int jsnm)
        {
            List<OutpatientSettlementDetailEntity> resultList = new List<OutpatientSettlementDetailEntity>();


            try
            {
                resultList = _RefundService.getMzJsMx(jslx, jsnm);
            }
            catch
            {
                throw;
            }
            return resultList;
        }

        /// <summary>
        ///Thêm mới hệ thống bệnh nhân tài khoản thu chi bản ghi 
        /// </summary>
        private List<SysPatientAccountRevenueAndExpenseEntity> getZhszjlList(OutpatientSettlementEntity tempMzJs,
            List<OutpatientSettlementPaymentModelEntity> parmjszffsArray, string tsFlag)
        {
            return null;
            //chỉcó tài khoản thanh toán,mới sẽ có thu chi tài khoản bản ghi 
            //  if (brzh == null)
            //   return null;
            //List<tbl_mz_jszffs> tempjszffsArray = parmjszffsArray;
            //if (tempjszffsArray == null || tempjszffsArray.Count == 0)
            //    return null;

            //List<tbl_xt_brzhszjl> szjlList = new List<tbl_xt_brzhszjl>();
            //tbl_xt_brzhszjl szjl = new tbl_xt_brzhszjl();
            //if (accountEntity.jch <= 0)
            //{
            //    tbl_mz_jszffs fs = tempjszffsArray.Find(t => t.xjzffs == NewtouchHIS.MAT.Constant.xtzhfs.ZHZF);
            //    if (fs == null)
            //        return null;
            //    if (tsFlag == "T")
            //    {
            //        //Hoàn tiền
            //        szjl = getSzjl(tempMzJs.jsnm, fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    else
            //    {
            // // thu tiền 
            //        szjl = getSzjl(tempMzJs.jsnm, -fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    szjlList.Add(szjl);
            //}
            //else
            //{
            // // giư ờng gia đ ình bệnh nhân
            //    tbl_mz_jszffs fs = tempjszffsArray.Find(t => t.xjzffs == NewtouchHIS.MAT.Constant.xtzhfs.ZHZF);
            //    if (fs == null)
            //        return null;
            //    if (tsFlag == "T")
            //    {
            //        //Hoàn tiền
            //        szjl = getSzjl(tempMzJs.jsnm, fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    else
            //    {
            // // thu tiền 
            //        szjl = getSzjl(tempMzJs.jsnm, -fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    szjlList.Add(szjl);


            // //Trả lại tiền thừa bản ghi 
            //    tbl_xt_xjzffs xjzffs = BF<xt_xjzffs>.Instance.getModel(NewtouchHIS.MAT.Constant.xtzhfs.XJZF);
            //    if (xjzffs == null)
            // throw new FailedCodeException("Hệ thống trong không tồn tại tại này thanh toán tiền mặt！");
            //    szjl = getSzjl(tempMzJs.jsnm, tempMzJs.zl, NewtouchHIS.MAT.Constant.szxzEnum.MZ_CZ_QK, xjzffs.xjzffsbh);
            //    szjlList.Add(szjl);

            // //// tài khoản chi
            //    //foreach (tbl_mz_jszffs_Ex entity in tempjszffsArray)
            //    //{
            //    //    szjl = getSzjl(brzh, tempMzJs);
            //    //    szjl.szje = -Convert.ToDecimal(lblYsk.Text);
            //    //    szjl.szxz = ((int)NewtouchHIS.MAT.Constant.szxzEnum.MZ_JS_TH).ToString();
            //    //    szjl.xjzffsbh = entity.zfbh;
            //    //    tempMzJs.zhjz = szjl.szje;
            //    //    szjlList.Add(szjl);
            //    //}
            //}

            //return szjlList;
        }

        /// <summary>
        /// Lại kiểm tra số biên lai 
        /// </summary>
        private void checkSJH(string sjh)
        {
            try
            {
                FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();
                string type = string.Empty;
                //Lấy số biên lai 
                //   List<string> fpsql = new List<string>();
                if (_finRecRepos.checkSJH(sjh, userModel.UserCode, out sjEntity, out type, this.OrganizeId))
                {
                    accountEntity.fph = sjh;
                    // fpsqlList = fpsql;

                    if (type == "insert")
                    {
                        _finRecRepos.AddReceiptInfo(sjEntity, this.OrganizeId); // Thêm số chứng từ biên lai
                    }
                    if (type == "update")
                    {
                        _finRecRepos.UpdateReceiptInfo(sjEntity, sjEntity.cwsjId); //Sửa số chứng từ biên lai
                    }
                }
                else
                {
                    accountEntity.fph = string.Empty;
                    fpsqlList = new List<string>();
                }
            }
            catch
            {
                accountEntity.fph = string.Empty;
                fpsqlList = new List<string>();
                throw;
            }
        }

        #region 不做功能

        //private bool PosJY(string kh, tbl_mz_js paramMzjs, decimal xjzf, bool flag)
        //   {
        // // hủy giao dịch 
        //       string code = PosCXInterface(kh, paramMzjs.fph, paramMzjs.jsnm);
        //       if (code != "00")
        //       {
        //           if (code == "12" || code == "E6")
        //           {
        // //pos hủy bỏ 
        //               this.isAlreadyPosCX = true;
        // if (DialogHelper.Ask_YesNo("Thu phí nơi xác nhậnpos hủy giao dịch Đ ã hoàn thành,vui lòng chọnYES,hoàn tiền sẽ tiếp tục,nếu không chấm dứt hoàn tiền.") == DialogResult.No)
        //               {
        //                   return false;
        //               }
        //           }
        //           else if (code == "XX")
        //           {
        // //Không có hủy bỏ 
        //               this.isAlreadyPosCX = false;
        //           }
        //           else
        //           {
        //               return false;
        //           }

        //           if (this.isAlreadyPosCX == false)
        //           {
        //               if (Tools.getIniFileName("SFYXTFPZparm") != "0")
        //               {
        // DialogHelper.DlgInfo("Khi trư ớc windows trên POS máy hoàn phíchức năng Đ ãĐ óng,không có phư ơng pháp làmPOShoàn phí thao tác！");
        //                   return false;
        //               }
        //           }

        // //Hoàn toàn bộ khoản giao dịch 
        //           if (flag == true)  //true == isReturnAll 
        //           {
        //               code = PosTfInterface(Kh, paramMzjs.fph, xjzf);
        //               if (code != "00")
        //               {
        //                   if (code == "12")
        //                   {
        // if (DialogHelper.Ask_YesNo("Thu phí nơi xác nhậnpos hoàn ti ềnĐ ã hoàn thành giờ,vui lòng chọnYES,hoàn tiền sẽ tiếp tục,nếu không chấm dứt hoàn tiền.") == DialogResult.No)
        //                       {
        //                           return false;
        //                       }
        //                   }
        //                   else if (code == "FAIL")
        //                   {
        //                       this.posTxj = true;
        //                   }
        //                   else
        //                   {
        //                       return false;
        //                   }
        //               }
        //           }
        //       }
        //       else
        //       {
        // //pos hủy giao dịch thành công
        //           this.isAlreadyPosCX = true;
        //       }
        //       return true;
        //   }        //private bool PosJY(string kh, tbl_mz_js paramMzjs, decimal xjzf, bool flag)
        //   {
        // // hủy giao dịch 
        //       string code = PosCXInterface(kh, paramMzjs.fph, paramMzjs.jsnm);
        //       if (code != "00")
        //       {
        //           if (code == "12" || code == "E6")
        //           {
        // //pos hủy bỏ 
        //               this.isAlreadyPosCX = true;
        // if (DialogHelper.Ask_YesNo("Thu phí nơi xác nhậnpos hủy giao dịch Đ ã hoàn thành,vui lòng chọnYES,hoàn tiền sẽ tiếp tục,nếu không chấm dứt hoàn tiền.") == DialogResult.No)
        //               {
        //                   return false;
        //               }
        //           }
        //           else if (code == "XX")
        //           {
        // //Không có hủy bỏ 
        //               this.isAlreadyPosCX = false;
        //           }
        //           else
        //           {
        //               return false;
        //           }

        //           if (this.isAlreadyPosCX == false)
        //           {
        //               if (Tools.getIniFileName("SFYXTFPZparm") != "0")
        //               {
        // DialogHelper.DlgInfo("Khi trư ớc windows trên POS máy hoàn phíchức năng Đ ãĐ óng,không có phư ơng pháp làmPOShoàn phí thao tác！");
        //                   return false;
        //               }
        //           }

        // //Hoàn toàn bộ khoản giao dịch 
        //           if (flag == true)  //true == isReturnAll 
        //           {
        //               code = PosTfInterface(Kh, paramMzjs.fph, xjzf);
        //               if (code != "00")
        //               {
        //                   if (code == "12")
        //                   {
        // if (DialogHelper.Ask_YesNo("Thu phí nơi xác nhậnpos hoàn ti ềnĐ ã hoàn thành giờ,vui lòng chọnYES,hoàn tiền sẽ tiếp tục,nếu không chấm dứt hoàn tiền.") == DialogResult.No)
        //                       {
        //                           return false;
        //                       }
        //                   }
        //                   else if (code == "FAIL")
        //                   {
        //                       this.posTxj = true;
        //                   }
        //                   else
        //                   {
        //                       return false;
        //                   }
        //               }
        //           }
        //       }
        //       else
        //       {
        // //pos hủy giao dịch thành công
        //           this.isAlreadyPosCX = true;
        //       }
        //       return true;
        //   }


        #endregion

        #region GRS门诊退费

        /// <summary>
        /// GRSHoàn phí ngoại trú
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="GridViewMxList"></param>
        /// <param name="jsnm"></param>
        /// <param name="isReturnAll"></param>
        /// <returns></returns>
        public bool btnReturnInAcc(string blh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll)
        {
            var result = false;
            kahao = kh;
            Jsnm = jsnm;
            var data = _RefundService.GetjsInfoByblh(blh, "", "", OrganizeId);
            var fapiao = data.Find(p => p.jsnm == jsnm);
            var GridViewMxData = _RefundService.GetGridViewMx(jsnm, OrganizeId);
            GridViewMxList.ForEach(p =>
            {
                var tem = GridViewMxData.Find(c => c.id == p.id);
                p.tag = tem.tag;
                p.IS_RETURN = "true";
            });


            var tblMzJs = new OutpatientSettlementEntity
            {
                fph = fapiao.oldFPH,
                flzffy = fapiao.flzffy,
                zffy = fapiao.zffy,
                jzfy = fapiao.jzfy,
                zlfy = fapiao.zlfy,
                jsnm = fapiao.jsnm,
                patid = fapiao.patid,
                xjzf = fapiao.xjzf,
                jslx = fapiao.jslx,
                brxz = fapiao.brxz,
                zje = fapiao.zje,
                xjwc = fapiao.xjwc,
                zhjz = fapiao.zhjz,
                xjzffs = fapiao.xjzffs,
                jszt = fapiao.jszt,
                cxjsnm = fapiao.cxjsnm,
                zh = fapiao.zh,
                fpdm = fapiao.fpdm,
                jmje = fapiao.jmje,
                jylx = fapiao.jylx,
                ysk = fapiao.ysk,
                zl = fapiao.zl,
                jzsj = fapiao.CreateTime
            };
            if (tblMzJs != null)
            {
                var MZJS = _RefundService.GetMZJSByJsnmInAcc(jsnm, OrganizeId);
                var tmpDr = MZJS[0];

                #region 全退半退数据明细
                //Đ ặt hoàn toàn bộ,hoàn một phần, không hoàn chi tiếtlist

                getRecordWithCategory(GridViewMxList);
                var accountEntityAll = getAccountList(2, tblMzJs, tmpDr);
                var accountEntity = getAccountList(1, tblMzJs, tmpDr);
                if (accountEntityAll.jsxmList.Count == 0)
                {
                    accountEntityAll = accountEntity;
                }
                #endregion

                isReturnAll = (resultList["any"].Count == 0 && resultList["nothing"].Count == 0);
                var returnsOk = true;
                IsReturnAll = isReturnAll;
                Kh = kh;
                try
                {
                    TuiFeiJSInAcc(jsnm, ZffsDt, isReturnAll, accountEntity, ghjsDt);
                }
                catch
                {
                    returnsOk = false;
                }
                return returnsOk;

            }
            isReturnAll = false;
            return result;

        }

        /// <summary>
        /// Hoàn phí quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="ZffsDt"></param>
        /// <param name="isReturnAll"></param>
        /// <param name="accountEntity"></param>
        /// <param name="GhjsDt"></param>
        public void TuiFeiJSInAcc(int jsnm, DataTable ZffsDt, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt)
        {
            isAlreadyPosCX = false;
            posTxj = false;
            mzJs = _RefundService.GetMZJSFromMz_jsByJsnm(jsnm);
            zffsDt = ZffsDt;
            ghjsDt = GhjsDt;

            lblSsk = (mzJs.xjzf).ToString("0.00");

            if (false == isReturnAll) //Nếu là một phần trả lại 
            {
                var ybError = false;
                var tuije = accountEntity.jsxmList.Sum(p => p.je);
                var brxz = _RefundService.GetBrxzByBrbh(accountEntity.brxz);
                //if (brxz == null)
                //    throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");
                // cơ bản thông tin
                newMzJs = getMzJsInAcc(accountEntity, brxz, out ybError, out sqxh);
                newMzJs.jzsj = mzJs.jzsj;
                newMzJs.zh = mzJs.zh;
                newMzJs.zlfy = tuije;
                newMzJs.xjzf = newMzJs.zlfy;
                newMzJs.zje = newMzJs.xjzf;
            }
            mzJs.CreateTime = DateTime.Now; //Lấy hệ thống máy chủ thời gian
            mzJs.CreatorCode = userModel.UserCode; // bản ghi hoàn phí thao tác viên 
            var jzrqList = Convert.ToDateTime(mzJs.jzsj);
            //Hoàn toàn bộ
            fullBack(Kh, accountEntity.isGh, mzJs, ghjsDt);
            if (false == isReturnAll) //Nếu là một phần trả lại 
                PartBackInAcc(newMzJs, accountEntity, mzJs);
            _RefundService.Minusybcs(jzrqList, isReturnAll, OrganizeId, accountEntity.patid);
        }

        /// <summary>
        /// CầnQuyết toánCủaHạng mụcThông tin
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="brxz"></param>
        /// <param name="ybError"></param>
        /// <param name="sqxh"></param>
        /// <param name="jsdl"></param>
        /// <returns></returns>
        private OutpatientSettlementEntity getMzJsInAcc(JS_ENTITY jsEntity, SysPatientNatureEntity brxz,
            out bool ybError,
            out string sqxh)
        {
            var jylx = Constants.ybDealLB.yb_deal_wjy;
            sqxh = string.Empty;
            ybError = false;

            if (jsEntity.jsxmList == null)
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            if (jsEntity.ghnmList == null || jsEntity.ghnmList.Count == 0)
                throw new FailedCodeException("GHNM_REQUIRED");

            var js = new OutpatientSettlementEntity();

            //Lấy thông tin bệnh nhân theo mã nội bộ bệnh nhân
            var brjbxx = _RefundService.GetBrjbxxByPatid(jsEntity.patid);
            if (brjbxx == null)
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");

            js.jsnm = _RefundService.GetPrimaryKeyByTableName("mz_js");
            js.patid = brjbxx.patid;
            js.brxz = brxz == null ? null : brxz.brxz;
            js.jslx = jsEntity.jslx;
            js.zffy = 0; //Chi phí tự trả
            js.flzffy = 0; //Chi phí tự trả theo phân loại
            js.jzfy = 0; //Phí ghi sổ,nếu là BHYTbệnh nhân,là BHYTquay lại của kết quả
            js.jmje = 0; //Chi phí giảm trừ
            js.jszt = (int)Constants.jsztEnum.YJ; //Trạng thái quyết toán
            js.xjzffs = string.Empty;
            js.ghnm = jsEntity.ghnm;
            //Thanh toán tiền mặt
            decimal ssk;
            //Sai lệch tiền mặt
            decimal difference;

            var ysk = jsEntity.jsxmList.Sum(p => p.je);
            jylx = Constants.ybDealLB.yb_deal_wjy;
            js.jylx = ((int)jylx).ToString();
            js.zffy = 0;
            Ext.get5s6r(ysk, out ssk, out difference);
            js.xjwc = difference;
            return js;
        }

        /// <summary>
        /// Hoàn một phần
        /// </summary>
        /// <param name="newMzJs"></param>
        /// <param name="accountEntity"></param>
        /// <param name="mzJs"></param>
        private void PartBackInAcc(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            OutpatientSettlementEntity mzJs)
        {

            //if (!checkJs())
            //    return;
            //Quyết toán phư ơng thức thanh toán bản ghi (Phư ơng thức hoàn tiền)
            //jszffsArray = getJszffsEntity();

            // phư ơng thức thu tiền - loại bỏ Phư ơng thức hoàn tiền thì là hoàn một phần của quyết toán phư ơng thức 
            //var jszffsArrayForSK = getJszffsEntity(jszffsArray);

            mzJs.CreatorCode = userModel.UserCode;
            mzJs.CreateTime = DateTime.Now;
            var result = isAlreadyComfirmTF;
            newMzJs.CreatorCode = userModel.UserCode;
            newMzJs.CreateTime = DateTime.Now;

            #region 半退收费 数据库操作
            newMzJs.fph = string.Empty;
            var listMzJsMx =
                getMzJsMx(newMzJs.jsnm, newMzJs.jslx, accountEntity.jsxmList);
            newMzJs.cxjsnm = Jsnm; // cũ quyết toán là trả lại mới của hủy bỏ mã nội bộ quyết toán 
            var szjlList = getZhszjlList(newMzJs, jszffsArray, "S");
            var result2 = _RefundService.InsertRemainFromReturns(newMzJs, listMzJsMx, jszffsArray, jsdlList);
            if (false == result2)
                throw new FailedCodeException("JS_ERROR_FAIL");
            #endregion
        }
        #endregion

        #region 医保退费

        /// <summary>
        /// Hoàn phíquy trình. mới của chưaquyết toánchi tiết tải lên
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="tmxzje"> trả lại chi tiết tổng số tiền</param>
        /// <param name="yxzje">còn lại dư ới tổng số tiền</param>
        public void DetailsUploadYb(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict
            , string newwjybjsh
            , out decimal tmxzje
            , out decimal yxzje)
        {
            tmxzje = 0;
            yxzje = 0;

            var clearResult = YibaoUniteInterface.ClearDetail(newwjybjsh);
            if (clearResult.Code != 0)
            {
                throw new FailedException("明细重新上传医保失败." + clearResult.ErrorMsg);
            }

            var sourcelist = _RefundService.RefundableQuery(this.OrganizeId, jsnm);

            var ybDetailList = new List<newtouchyibao.Models.DETAIL>();
            foreach (var item in sourcelist)
            {
                decimal tsl = 0;
                decimal newsl = item.sl.Value;
                if (tjsxmDict.TryGetValue(item.jsmxnm, out tsl))
                {
                    newsl = newsl - tsl;
                }

                if (newsl < 0)
                {
                    //throw
                    tmxzje += item.jsmxje.Value;    // đ ược hoàn toàn bộ 
                    continue;
                }
                else if (newsl == 0)
                {
                    tmxzje += item.jsmxje.Value;    // đ ược hoàn toàn bộ 
                    continue;
                }

                var detail = new newtouchyibao.Models.DETAIL();
                detail.ITEMID = item.feeType == 1 
                    ? ("YP" + item.cfmxId.Value.ToString()) 
                    : ("XM" + item.xmnm.Value.ToString());
                //BHYT0Thuốc 1Hạng mục
                detail.DKE085 = item.feeType == 1 ? "0" : "1";
                detail.DKE012 = item.sfxmCode;
                detail.AKE227 = item.klsj;
                detail.AKE226 = item.ks;
                detail.AKE228 = item.ys;
                detail.AKE229 = item.ysmc;
                detail.AKE002 = item.mc;
                //Thuốcquy cách, sản xuất đ ịa 
                detail.AKE207 = item.dw;
                detail.AKE212 = item.sl.Value;
                detail.AKE208 = item.dj;
                detail.AKE198 = item.jsmxje.Value;
                if (newsl != item.sl.Value)
                {
                    // trả lại một phần
                    detail.AKE212 = newsl;
                    detail.AKE198 = item.dj * newsl;

                    tmxzje += item.jsmxje.Value - detail.AKE198;
                }
                //Khác thuốc liên quan 
                ybDetailList.Add(detail);

                yxzje += detail.AKE198;
            }
            if (ybDetailList.Count > 0)
            {
                var preaccResult = YibaoUniteInterface.PreAccount(newwjybjsh, ybDetailList);
                if (preaccResult.Code != 0)
                {
                    throw new FailedException("明细上传医保失败." + preaccResult.ErrorMsg);
                }
            }
            return;
        }

        #endregion

    }
}

