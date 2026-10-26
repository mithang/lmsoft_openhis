using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using static Newtouch.Infrastructure.Constants;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Ngoại trú của một 些chung Phư ơng thức
    /// nhất đ ịnh là không Quận分版 bản 
    /// </summary>
    public class OutPatientUniversalDmnService : DmnServiceBase, IOutPatientUniversalDmnService
    {
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysMedicinRepo _sysMedicinRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly ICqybSett23Repo _iCqybSett23Repo;
        public OutPatientUniversalDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Gửichi tiết(重 cần:Nếu là tới Đ ơn thuốc bổ sung hoặc hủy bỏ chi tiết,线 dư ới 意义 này là tại thay đ ổi này tờ Đ ơn thuốc,bác sĩ thông thư ờng không như vậy 做)(Đ ơn thuốc thì cập nhật bảng chính thông tin)(vui lòng Trư ớc xây dựng của nó yêu cầu tham số)(theo chi tiết của khóa chínhxmnm, cfmxid giá trị 来 phán đoán này là thêm mới,还là sửa bản ghi)(khi là thêm mới, theo cfnm trư ờng,又分thêm mớichi tiết và Đ ơn thuốc,还là tới Đ ãcó đơn thuốc bổ sung chi tiết)(sửa thì là trực tiếp cập nhật dữ liệu kho chi tiết bản ghi / này 版 bản tạm thời không hỗ trợ,nếu khôngvui lòng去hoàn phí)
        /// </summary>
        /// <param name="orgid"> y tế cơ quanid</param>
        /// <param name="changedCfnmList">Quay lại dùng của</param>
        /// <param name="changedXmnmList">Quay lại dùng của(nếu hạng mục không liên kết Đ ơn thuốc)</param>
        /// <param name="updateSkipSettledCfhList">(này là một quay lại)cập nh ậtĐ ã quyết to ánĐ ơn thuốc đ ược 跳 qua Củacfh List</param>
        /// <param name="mzh">Nếu cần thêm mới thì, cho 你 chuẩn bị </param>
        /// <param name="ys">Nếu cần thêm mới thì, cho 你 chuẩn bị (chưa chỉ đ ịnh giờ đi đ ăng ký khám Củays)</param>
        /// <param name="ItemsGroupBOList"> tính phí thông tin nhóm (thêm m ớiĐ ơn thuốc/Thêm mớichi tiết đ ơn thuốc/Thêm chi tiết hạng mục)Đ ơn thuốc thông tin/Chi tiết đ ơn thuốc thông tin/Hạng mục thông tin</param>
        /// <param name="settledUpdateForbidden">Đ ã quyết toán giờ có hay không禁止cập nhật</param>
        /// <param name="settledUpdateSkip">(Đ ã quyết toán giờ禁止cập nhật) kiểm tra có hay khôngSkip,true không 抛bất thư ờng跳 qua,nếu không抛bất thư ờng</param>
        /// <param name="cfly">Đ ơn thuốc来源(0 thu phí nơi 1 trạm bác sĩ)</param>
        public void SubmitItems(
            string orgId
            , out IList<int> changedCfnmList, out IList<int> changedXmnmList
            , out IList<string> updateSkipSettledCfhList
            , string mzh = null, string ys = null
            , IList<FeeItemsGroupedBO> ItemsGroupBOList = null
            , bool settledUpdateForbidden = true
            , bool settledUpdateSkip = false
            , string cfly = null)
        {
            changedCfnmList = new List<int>();
            changedXmnmList = new List<int>();
            updateSkipSettledCfhList = new List<string>();

            if (ItemsGroupBOList == null || ItemsGroupBOList.Count == 0)
            {
                return;
            }

            //Hệ thốngNhà thuốc ngoại trú
            IList<SysPharmacyDepartmentVEntity> mzyfList = null;

            //Thuốc có hay không liên kết nhà thuốc kho tồn tại 
            var isMedicineSearchRelatedKC = _sysConfigRepo.GetBoolValueByCode("IS_MedicineSearchRelatedKC", orgId);

            //Tiền xử lý
            foreach (var itemsGroup in ItemsGroupBOList)
            {
                foreach (var item in itemsGroup.ypList)
                {
                    UpdateEntity(orgId, item);
                }
                foreach (var item in itemsGroup.xmList)
                {
                    UpdateEntity(orgId, item);
                }

                if (isMedicineSearchRelatedKC != true || itemsGroup.ypList == null || itemsGroup.ypList.Count <= 0) continue;
                if (!string.IsNullOrWhiteSpace(itemsGroup.lyyf)) continue;
                //Nếu thì 1个nhà thuốc ngoại trú, 
                mzyfList = mzyfList ?? _sysPharmacyDepartmentRepo.GetPharmacyDepartmentList(orgId, ((int)Enummzzybz.mz).ToString());
                if (mzyfList.Count == 1)
                {
                    itemsGroup.lyyf = mzyfList[0].yfbmCode;
                }
            }

            // chuẩn bị thông tin đ ăng ký khám bác sĩ khoa thông tin
            var mzghEntity = _outpatientRegistRepo.FindEntity(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1");
            if (mzghEntity == null)
            {
                throw new FailedException("REGISTINFO_NOT_FOUND", "关联挂号信息异常");
            }
            if (string.IsNullOrWhiteSpace(mzghEntity.ys))
            {
                // không 挂 đ ến khoa,thì尝试取thu phí chi tiết CủaĐ ăng ký khám
                var settPatInfo = GetSettPatientInfo(mzh, orgId);
                if (settPatInfo != null)
                {
                    mzghEntity.ys = settPatInfo.ys;
                }
            }
            ys = ys ?? mzghEntity.ys;
            if (string.IsNullOrWhiteSpace(ys))
            {
                //Thông tin đ ăng ký khám chưa chỉ đ ịnh bác sĩ //Thì尝试取khi trư ớc thao tác者
                try
                {
                    var opr = Newtouch.Common.Operator.OperatorProvider.GetCurrent();
                    if (opr != null)
                    {
                        ys = opr.rygh;
                        //check is Bác sĩ
                        var checkIsDoctorSql = @"select 1 from [NewtouchHIS_Base]..V_C_Sys_StaffDuty where StaffGh = @rygh and DutyCode = 'Doctor' and zt = '1' and OrganizeId = @orgId";
                        var curOprIsDoctor = this.FirstOrDefault<int?>(checkIsDoctorSql, new[] { new SqlParameter("@rygh", ys), new SqlParameter("@orgId", orgId) });
                        if ((curOprIsDoctor ?? 0) == 0)
                        {
                            ys = null;  //Khi trư ớc thao tác人 không là bác sĩ
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }
            var ysEntity = string.IsNullOrWhiteSpace(ys) ? null : _sysStaffRepo.GetValidStaffByGh(ys, orgId);
            if (ysEntity == null)
            {
                throw new FailedException("REGISTDOCTORINFO_NOT_FOUND", "缺少医生信息");
            }
            var ysmc = ysEntity.Name;
            var ks = ysEntity.DepartmentCode;
            var ksmc = _sysDepartmentRepo.GetNameByCode(ysEntity.DepartmentCode, orgId);

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //Đ ơn thuốc bảng chính của một 些 trư ờng 统 một cập nhật dư ới ví dụ ys ks ksmcV.v.
                var cfzbEntityList = new List<OutpatientPrescriptionEntity>();
                //Hạng mục bảng của một 些 trư ờng 统 một cập nhật dư ới ví dụ ys ks ksmcV.v.
                var xmEntityList = new List<OutpatientItemEntity>();

                foreach (var itemsGroup in ItemsGroupBOList)
                {
                    if (itemsGroup.xmList.Count == 0
                        && itemsGroup.ypList.Count == 0
                        && itemsGroup.ztInvalidXmList.Count == 0
                        && itemsGroup.ztInvalidYpList.Count == 0)
                    {
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(itemsGroup.cfh) && (itemsGroup.cflx ?? 0) > 0)
                    {
                        #region 给了我处方号 这一定是在操作一个处方内的数据
                        // cho 我số đ ơn thuốc này nhất đ ịnh là cần thao tác một Đ ơn thuốc trong của dữ liệu
                        //去找Đ ơn thuốc bảng chính, không có thì thêm mới
                        bool isNewCf = false;
                        var cfzbEntity = db.IQueryable<OutpatientPrescriptionEntity>(p => p.OrganizeId == orgId && p.cfh == itemsGroup.cfh && p.cflx == itemsGroup.cflx.Value && p.zt == "1").FirstOrDefault();
                        if (cfzbEntity == null)
                        {
                            isNewCf = true;
                            //Thêm mới một tờ Đ ơn thuốc
                            cfzbEntity = new OutpatientPrescriptionEntity()
                            {
                                cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                zje = 0,    // chỉ có thể phía sau 再 theo chi tiết cập nhật
                                cfzt = "0", //0Chờ quyết toán
                                cfly = cfly,
                                cfh = itemsGroup.cfh,
                                cflx = itemsGroup.cflx,
                                cflxmc = itemsGroup.cflxmc,
                                zt = "1",
                                sfrq = itemsGroup.cfsfrq,
                                ts = (itemsGroup.cflx.Value == (int)EnumPrescriptionType.Medicine) ? itemsGroup.ts : null,
                                djbz = (itemsGroup.cflx.Value == (int)EnumPrescriptionType.Medicine) ? itemsGroup.djbz : null,
                                lyyf = (itemsGroup.cflx.Value == (int)EnumPrescriptionType.Medicine) ? itemsGroup.lyyf : null,
                            };
                            cfzbEntity.Create();
                            db.Insert(cfzbEntity);
                        }
                        else
                        {
                            if (cfzbEntity.cfzt == "1" && settledUpdateForbidden)
                            {
                                if (settledUpdateSkip)
                                {
                                    updateSkipSettledCfhList.Add(cfzbEntity.cfh);
                                    continue;   //跳 qua này Đ ơn thuốc
                                }
                                throw new FailedException("SETTLED_UPDATE_FORBIDDEN", "已结状态不允许修改变更");
                            }
                            if (cfzbEntity.ys != ys)
                            {
                                // lịch sử Đ ơn thuốc đ ổi bác sĩ là không đ ối với của
                                throw new FailedException("DOCTOR_CHANGED_FORBIDDEN", "发生错误处方医生发生变更");
                            }
                            cfzbEntity.Modify();
                            db.Update(cfzbEntity);
                        }
                        changedCfnmList.Add(cfzbEntity.cfnm);
                        cfzbEntityList.Add(cfzbEntity);
                        decimal ypzje = 0.00M;
                        if (itemsGroup.cflx.Value == (int)EnumPrescriptionType.Medicine)
                        {
                            // này là một tờ thu ốcĐ ơn thuốc
                            if (itemsGroup.ypList.Count == 0 && itemsGroup.ztInvalidYpList.Count == 0)
                            {
                                // không cho chi tiết
                                throw new FailedException("PARAMS_REQUIRE_DETAILS", "参数错误缺少明细");
                            }

                            //Dữ liệu kho trong củakhi trư ớc chi tiếtList
                            IList<OutpatientPrescriptionDetailEntity> existList = isNewCf
                                ? new List<OutpatientPrescriptionDetailEntity>()
                                : db.IQueryable<OutpatientPrescriptionDetailEntity>(p => p.OrganizeId == orgId && p.cfnm == cfzbEntity.cfnm && p.zt == "1").ToList();

                            // thay đ ổi trư ớc đ ó của tổng số tiền
                            cfzbEntity.zje = existList.Select(p => p.je).Sum();
                            
                            //Hủy bỏdetails
                            if (itemsGroup.ztInvalidYpList.Count > 0)
                            {
                                var ids = itemsGroup.ztInvalidYpList.Select(p => p.cfmxId).Where(p => p > 0).ToList();
                                var codes = itemsGroup.ztInvalidYpList.Where(p => p.cfmxId == 0).Select(p => p.yp).ToList();
                                var inValidItems = existList.Where(p => (ids.Contains(p.cfmxId) || codes.Contains(p.yp)) && p.zt == "1").ToList();
                                if (inValidItems.Count != itemsGroup.ztInvalidYpList.Count)
                                {
                                    // chỉ đ ịnh của hủy bỏ chi tiết tham số có vấn đ ề
                                    throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误作废明细发生异常");
                                }
                                foreach (var item in inValidItems)
                                {
                                    item.zt = "0";
                                    item.Modify();
                                    db.Update(item);

                                    cfzbEntity.zje -= item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                }
                            }

                            //add/update details
                            if (itemsGroup.ypList.Count > 0)
                            {
                                ;
                                if (itemsGroup.ypList.Select(p => (p.czh ?? "") + p.yp).Count()
                                    != itemsGroup.ypList.Count)
                                {
                                    //#######规thì问题########
                                    throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                }
                                foreach (var item in itemsGroup.ypList)
                                {
                                    if (existList.Where(p => p.zt == "1").Any(p => p.yp == item.yp
                                        && (p.czh ?? "") == (item.czh ?? "")
                                        && p.cfmxId != item.cfmxId))
                                    {
                                        //#######规thì问题########
                                        throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                    }
                                    if (item.cfmxId == 0)
                                    {
                                        // này là cần tới Đ ãcó đơn thuốc bổ sung chi tiết
                                        item.cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx");
                                        item.OrganizeId = orgId;
                                        item.cfnm = cfzbEntity.cfnm;
                                        item.Create();
                                        db.Insert(item);
                                        cfzbEntity.zje += item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                    }
                                    else
                                    {
                                        // này là cần trực tiếpUpdate dữ liệu kho trong củachi tiết tạm thời chưa hỗ trợ 
                                        throw new NotSupportedException();
                                    }
                                }
                            }
                            ypzje = cfzbEntity.zje;
                            if (!existList.Any(p => p.zt == "1") && itemsGroup.ypList.Count == 0)
                            {
                                cfzbEntity.zt = "0";   //Chi tiết nếu đ ều hủy bỏ, bảng chính cũng và hủy bỏ
                                cfzbEntity.Modify();
                                db.Update(cfzbEntity);
                            }

                            #region 药品处方中有收费项目组套

                            if (itemsGroup.xmList.Count > 0)
                            {


                                // này là một tờ hạng m ụcĐ ơn thuốc
                                if (itemsGroup.xmList.Count == 0 && itemsGroup.ztInvalidXmList.Count == 0)
                                {
                                    // không cho chi tiết
                                    throw new FailedException("PARAMS_REQUIRE_DETAILS", "参数错误缺少明细");
                                }

                                //Dữ liệu kho trong củakhi trư ớc chi tiếtList
                                IList<OutpatientItemEntity> existList2 = isNewCf
                                    ? new List<OutpatientItemEntity>()
                                    : db.IQueryable<OutpatientItemEntity>(p => p.OrganizeId == orgId && p.cfnm == cfzbEntity.cfnm && p.zt == "1").ToList();

                                // thay đ ổi trư ớc đ ó của tổng số tiền
                                cfzbEntity.zje = existList2.Select(p => p.je).Sum();
                                
                                //Hủy bỏdetails
                                if (itemsGroup.ztInvalidXmList.Count > 0)
                                {
                                    var ids = itemsGroup.ztInvalidXmList.Select(p => p.xmnm).Where(p => p > 0).ToList();
                                    var codes = itemsGroup.ztInvalidXmList.Where(p => p.xmnm == 0).Select(p => p.sfxm).ToList();
                                    var inValidItems = existList2.Where(p => (ids.Contains(p.xmnm) || codes.Contains(p.sfxm)) && p.zt == "1").ToList();
                                    if (inValidItems.Count != itemsGroup.ztInvalidXmList.Count)
                                    {
                                        // chỉ đ ịnh của hủy bỏ chi tiết tham số có vấn đ ề
                                        throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误作废明细发生异常");
                                    }
                                    foreach (var item in inValidItems)
                                    {
                                        item.zt = "0";
                                        item.Modify();
                                        db.Update(item);

                                        cfzbEntity.zje -= item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                    }
                                }

                                //add/update details
                                if (itemsGroup.xmList.Count > 0)
                                {
                                    if (itemsGroup.xmList.Select(p => p.sfxm).Distinct().Count() !=
                                    itemsGroup.xmList.Count)
                                    {
                                        //#######规thì问题########
                                        throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                    }
                                    foreach (var item in itemsGroup.xmList)
                                    {
                                        if (existList2.Where(p => p.zt == "1").Any(p => p.sfxm == item.sfxm && p.xmnm != item.xmnm))
                                        {
                                            //#######规thì问题########
                                            throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                        }
                                        if (item.xmnm == 0)
                                        {
                                            // này là cần tới Đ ãcó đơn thuốc bổ sung chi tiết
                                            item.xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                            item.cfnm = cfzbEntity.cfnm;
                                            item.xmzt = cfzbEntity.cfzt;
                                            item.xmly = cfzbEntity.cfly;
                                            //Nếu là đã quyết toán tính 取khi trư ớc thời gian
                                            item.jsrq = cfzbEntity.jsrq.HasValue ? (DateTime?)DateTime.Now : null;
                                            item.Create();
                                            db.Insert(item);
                                            xmEntityList.Add(item);

                                            cfzbEntity.zje += item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                        }
                                        else
                                        {
                                            // này là cần trực tiếpUpdate dữ liệu kho trong củachi tiết tạm thời chưa hỗ trợ 
                                            throw new NotSupportedException();
                                        }
                                    }
                                }
                                cfzbEntity.zje += ypzje;
                                if (!existList2.Any(p => p.zt == "1") && itemsGroup.xmList.Count == 0)
                                {
                                    cfzbEntity.zt = "0";   //Chi tiết nếu đ ều hủy bỏ, bảng chính cũng và hủy bỏ
                                    cfzbEntity.Modify();
                                    db.Update(cfzbEntity);
                                }
                            }
                            #endregion

                        }
                        else if (itemsGroup.cflx.Value == (int)EnumPrescriptionType.Treament)
                        {
                            // này là một tờ hạng m ụcĐ ơn thuốc
                            if (itemsGroup.xmList.Count == 0 && itemsGroup.ztInvalidXmList.Count == 0)
                            {
                                // không cho chi tiết
                                throw new FailedException("PARAMS_REQUIRE_DETAILS", "参数错误缺少明细");
                            }

                            //Dữ liệu kho trong củakhi trư ớc chi tiếtList
                            IList<OutpatientItemEntity> existList = isNewCf
                                ? new List<OutpatientItemEntity>()
                                : db.IQueryable<OutpatientItemEntity>(p => p.OrganizeId == orgId && p.cfnm == cfzbEntity.cfnm && p.zt == "1").ToList();

                            // thay đ ổi trư ớc đ ó của tổng số tiền
                            cfzbEntity.zje = existList.Select(p => p.je).Sum();

                            //Hủy bỏdetails
                            if (itemsGroup.ztInvalidXmList.Count > 0)
                            {
                                var ids = itemsGroup.ztInvalidXmList.Select(p => p.xmnm).Where(p => p > 0).ToList();
                                var codes = itemsGroup.ztInvalidXmList.Where(p => p.xmnm == 0).Select(p => p.sfxm).ToList();
                                var inValidItems = existList.Where(p => (ids.Contains(p.xmnm) || codes.Contains(p.sfxm)) && p.zt == "1").ToList();
                                if (inValidItems.Count != itemsGroup.ztInvalidXmList.Count)
                                {
                                    // chỉ đ ịnh của hủy bỏ chi tiết tham số có vấn đ ề
                                    throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误作废明细发生异常");
                                }
                                foreach (var item in inValidItems)
                                {
                                    item.zt = "0";
                                    item.Modify();
                                    db.Update(item);

                                    cfzbEntity.zje -= item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                }
                            }

                            //add/update details
                            if (itemsGroup.xmList.Count > 0)
                            {
                                if (itemsGroup.xmList.Select(p => p.sfxm).Distinct().Count() !=
                                itemsGroup.xmList.Count)
                                {
                                    //#######规thì问题########
                                    throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                }
                                foreach (var item in itemsGroup.xmList)
                                {
                                    if (existList.Where(p => p.zt == "1").Any(p => p.sfxm == item.sfxm && p.xmnm != item.xmnm))
                                    {
                                        //#######规thì问题########
                                        throw new FailedException("DETAILS_REPEATED_ERROR", "单张处方下明细不能重复");
                                    }
                                    if (item.xmnm == 0)
                                    {
                                        // này là cần tới Đ ãcó đơn thuốc bổ sung chi tiết
                                        item.xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                        item.cfnm = cfzbEntity.cfnm;
                                        item.xmzt = cfzbEntity.cfzt;
                                        item.xmly = cfzbEntity.cfly;
                                        //Nếu là đã quyết toán tính 取khi trư ớc thời gian
                                        item.jsrq = cfzbEntity.jsrq.HasValue ? (DateTime?)DateTime.Now : null;
                                        item.Create();
                                        db.Insert(item);
                                        xmEntityList.Add(item);

                                        cfzbEntity.zje += item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                    }
                                    else
                                    {
                                        // này là cần trực tiếpUpdate dữ liệu kho trong củachi tiết tạm thời chưa hỗ trợ 
                                        throw new NotSupportedException();
                                    }
                                }
                            }

                            if (!existList.Any(p => p.zt == "1") && itemsGroup.xmList.Count == 0)
                            {
                                cfzbEntity.zt = "0";   //Chi tiết nếu đ ều hủy bỏ, bảng chính cũng và hủy bỏ
                                cfzbEntity.Modify();
                                db.Update(cfzbEntity);
                            }
                        }
                        else
                        {
                            //Loại đ ơn thuốc bất thư ờng không phải phạm vi trong 
                            throw new FailedException("PARAMS_ERROR_CFLX", "处方类型参数错误");
                        }
                        #endregion 给了我处方号 这一定是在操作一个处方内的数据
                    }
                    else if (string.IsNullOrWhiteSpace(itemsGroup.cfh) && (itemsGroup.cflx ?? 0) == 0)  // không có thể chỉ đ ịnh loại đ ơn thuốc？
                    {
                        #region 没有指定处方号（2018.04.18内部不再自动分方）

                        #region 先药品
                        if (itemsGroup.ypList.Count > 0 || itemsGroup.ztInvalidYpList.Count > 0)
                        {
                            // những này là cần thêm m ớiĐ ơn thuốc của
                            var itemsNew = itemsGroup.ypList.Where(p => p.cfnm == 0).ToList();
                            // những này là tới Đ ãcó đơn thuốc bổ sung chi tiết của(tạm thời không hỗ trợ trực tiếpUpdate dữ liệu kho)
                            var itemsAppendOrUpdate = itemsGroup.ypList.Where(p => p.cfnm > 0).ToList();
                            // phía dư ới 分 hai một phần 1 thêm mới Đ ơn thuốc 2 lịch sử Đ ơn thuốc của bổ sung chi tiết hoặc hủy bỏ chi tiết

                            #region 1新增部分（mz_cfmx + mz_cf）
                            if (itemsNew.Count > 0)
                            {
                                foreach (var item in itemsNew)
                                {
                                    item.cfmxId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cfmx");
                                    item.OrganizeId = orgId;
                                    item.Create();
                                    db.Insert(item);
                                }

                                //Thêm mới1 tờ Đ ơn thuốc
                                var cfzbEntity = new OutpatientPrescriptionEntity()
                                {
                                    cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                    zje = 0,    // chỉ có thể phía sau 再 theo chi tiết cập nhật
                                    cfzt = "0", //0Chờ quyết toán
                                    cfly = cfly,
                                    cfh = _outpatientPrescriptionRepo.GeneratePresNo(orgId, (int)EnumPrescriptionType.Medicine),
                                    cflx = (int)EnumPrescriptionType.Medicine,
                                    zt = "1",
                                    sfrq = itemsGroup.cfsfrq,
                                    ts = itemsGroup.ts,
                                    djbz = itemsGroup.djbz,
                                    lyyf = itemsGroup.lyyf,
                                };

                                cfzbEntity.zje = itemsNew.Sum(p => p.je);  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới

                                cfzbEntity.Create();
                                db.Insert(cfzbEntity);

                                changedCfnmList.Add(cfzbEntity.cfnm);
                                cfzbEntityList.Add(cfzbEntity);

                                //Cập nhậtThêm mớiChi tiếtCủacfnm
                                foreach (var item in itemsNew)
                                {
                                    item.cfnm = cfzbEntity.cfnm;
                                }
                            }
                            #endregion
                            #region 2历史处方的追加明细或作废明细

                            #endregion
                        }
                        #endregion

                        #region 后项目
                        if (itemsGroup.xmList.Count > 0 || itemsGroup.ztInvalidXmList.Count > 0)
                        {
                            // những này là cần thêm m ớiĐ ơn thuốc của //Hoặc chưa sử d ụngĐ ơn thuốc thêm mớichi tiết của
                            var itemsNew = itemsGroup.xmList.Where(p => p.cfnm == null || p.cfnm == 0).ToList();
                            // những này là tới Đ ãcó đơn thuốc bổ sung chi tiết của(tạm thời không hỗ trợ trực tiếpUpdate dữ liệu kho)
                            var itemsAppendOrUpdate = itemsGroup.xmList.Where(p => p.cfnm > 0).ToList();
                            // phía dư ới 分 hai một phần 1 mới hạng mục ngoại trú tính phí (mới Đ ơn thuốc) 2 lịch sử Đ ơn thuốc của bổ sung chi tiết hoặc hủy bỏ chi tiết
                            #region 1新增部分（门诊项目/新处方）（也可能仅门诊项目）
                            if (itemsNew.Count > 0)
                            {
                                foreach (var item in itemsNew)
                                {
                                    item.xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                    item.xmzt = "0";
                                    item.xmly = cfly;
                                    item.jsrq = null;
                                    item.Create();
                                    db.Insert(item);
                                    xmEntityList.Add(item);

                                    if (!itemsGroup.xmWithCf)
                                    {
                                        changedXmnmList.Add(item.xmnm);
                                    }
                                }

                                if (itemsGroup.xmWithCf)
                                {
                                    //Thêm mới1 tờ Đ ơn thuốc
                                    var cfzbEntity = new OutpatientPrescriptionEntity()
                                    {
                                        cfnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_cf"),
                                        zje = 0,    // chỉ có thể phía sau 再 theo chi tiết cập nhật
                                        cfzt = "0", //0Chờ quyết toán
                                        cfly = cfly,
                                        cfh = _outpatientPrescriptionRepo.GeneratePresNo(orgId, (int)EnumPrescriptionType.Treament),
                                        cflx = (int)EnumPrescriptionType.Treament,
                                        zt = "1",
                                        sfrq = itemsGroup.cfsfrq,
                                    };

                                    cfzbEntity.zje = itemsNew.Sum(p => p.je);  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới

                                    cfzbEntity.Create();
                                    db.Insert(cfzbEntity);

                                    changedCfnmList.Add(cfzbEntity.cfnm);
                                    cfzbEntityList.Add(cfzbEntity);

                                    //Cập nhậtThêm mớiChi tiếtCủacfnm
                                    foreach (var item in itemsNew)
                                    {
                                        item.cfnm = cfzbEntity.cfnm;
                                    }
                                }

                            }
                            #endregion
                            #region 2历史处方的追加明细或作废明细
                            if (itemsGroup.xmWithCf)
                            {
                                var inValidItems = new List<OutpatientItemEntity>();    // phía sau cần sẽ của nó hủy bỏ
                                var oldPresCfnmPartList = itemsAppendOrUpdate.Select(p => p.cfnm).ToList();
                                if (itemsGroup.ztInvalidXmList.Count > 0)
                                {
                                    var ids = itemsGroup.ztInvalidXmList.Select(p => p.xmnm).ToList();
                                    inValidItems = db.IQueryable<OutpatientItemEntity>(p => ids.Contains(p.xmnm) && p.zt == "1").ToList();
                                    if (inValidItems.Count != itemsGroup.ztInvalidXmList.Count)
                                    {
                                        // chỉ đ ịnh của hủy bỏ chi tiết tham số có vấn đ ề
                                        throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误作废明细发生异常");
                                    }
                                    oldPresCfnmPartList.AddRange(inValidItems.Select(p => p.cfnm));
                                }
                                var oldPresList = db.IQueryable<OutpatientPrescriptionEntity>(p => oldPresCfnmPartList.Contains(p.cfnm)).ToList();
                                foreach (var cfzbEntity in oldPresList)
                                {
                                    if (cfzbEntity.cfzt == "1" && settledUpdateForbidden)
                                    {
                                        throw new FailedException("SETTLED_UPDATE_FORBIDDEN", "已结状态不允许修改变更");
                                    }
                                    IList<OutpatientItemEntity> existList = db.IQueryable<OutpatientItemEntity>(p => p.OrganizeId == orgId && p.cfnm == cfzbEntity.cfnm && p.zt == "1").ToList();
                                    // thay đ ổi trư ớc đ ó của tổng số tiền
                                    cfzbEntity.zje = existList.Select(p => p.je).Sum();
                                    //Hủy bỏ
                                    foreach (var item in existList.Where(p => inValidItems.Any(t => t.xmnm == p.xmnm)))
                                    {
                                        item.zt = "0";
                                        item.Modify();
                                        db.Update(item);

                                        cfzbEntity.zje -= item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                    }
                                    // bổ sung chi tiết
                                    var thisitemsAppendOrUpdate = itemsAppendOrUpdate.Where(p => p.cfnm == cfzbEntity.cfnm).ToList();
                                    foreach (var item in thisitemsAppendOrUpdate)
                                    {
                                        if (item.xmnm == 0)
                                        {
                                            // này là cần tới Đ ãcó đơn thuốc bổ sung chi tiết
                                            item.xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_xm");
                                            item.cfnm = cfzbEntity.cfnm;
                                            item.xmzt = cfzbEntity.cfzt;
                                            item.xmly = cfzbEntity.cfly;
                                            //Nếu là đã quyết toán tính 取khi trư ớc thời gian
                                            item.jsrq = cfzbEntity.jsrq.HasValue ? (DateTime?)DateTime.Now : null;
                                            item.Create();
                                            db.Insert(item);
                                            xmEntityList.Add(item);

                                            cfzbEntity.zje += item.je;  //Đ ồng thời cập nhật tổng số tiền đ ơn thuốc bên dư ới
                                        }
                                        else
                                        {
                                            // này là cần trực tiếpUpdate dữ liệu kho trong củachi tiết tạm thời chưa hỗ trợ 
                                            throw new NotSupportedException();
                                        }
                                    }
                                    if (!existList.Any(p => p.zt == "1") && thisitemsAppendOrUpdate.Count == 0)
                                    {
                                        cfzbEntity.zt = "0";   //Chi tiết nếu đ ều hủy bỏ, bảng chính cũng và hủy bỏ
                                    }
                                    cfzbEntity.Modify();
                                    db.Update(cfzbEntity);

                                    changedCfnmList.Add(cfzbEntity.cfnm);
                                    cfzbEntityList.Add(cfzbEntity);
                                }
                            }
                            else
                            {
                                //Hạng mục không liên kết Đ ơn thuốc thêm mớichi tiết hoặc hủy bỏ chi tiết
                                //Hủy bỏ
                                if (itemsGroup.ztInvalidXmList.Count > 0)
                                {
                                    var ids = itemsGroup.ztInvalidXmList.Select(p => p.xmnm).ToList();
                                    var inValidItems = db.IQueryable<OutpatientItemEntity>(p => ids.Contains(p.xmnm) && p.zt == "1").ToList();
                                    if (inValidItems.Count != itemsGroup.ztInvalidXmList.Count)
                                    {
                                        // chỉ đ ịnh của hủy bỏ chi tiết tham số có vấn đ ề
                                        throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误作废明细发生异常");
                                    }
                                    foreach (var item in inValidItems)
                                    {
                                        item.zt = "0";
                                        item.Modify();
                                        db.Update(item);

                                        changedXmnmList.Add(item.xmnm);
                                    }
                                }

                                //update
                                if (itemsAppendOrUpdate.Count > 0)
                                {
                                    // này là cần trực tiếpUpdate dữ liệu kho trong củachi tiết tạm thời chưa hỗ trợ 
                                    throw new NotSupportedException();
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #endregion 没有指定处方号（这里面也可能有几个处方）
                    }
                }
                #region 收尾
                // hàng loạt cập nhật dư ới Đ ơn thuốc Củays, ksV.v. trư ờng 
                foreach (var cfzbEntityItem in cfzbEntityList)
                {
                    cfzbEntityItem.OrganizeId = orgId;
                    cfzbEntityItem.ghnm = mzghEntity.ghnm;
                    cfzbEntityItem.patid = mzghEntity.patid;
                    cfzbEntityItem.brxz = mzghEntity.brxz;
                    cfzbEntityItem.mjzbz = mzghEntity.mjzbz;
                    cfzbEntityItem.ys = ys;
                    cfzbEntityItem.ks = ks;
                    cfzbEntityItem.ysmc = ysmc;
                    cfzbEntityItem.ksmc = ksmc;
                }
                // hàng loạt cập nhật dư ới hạng mục Củays, ksV.v. trư ờng 
                foreach (var xmEntity in xmEntityList)
                {
                    xmEntity.OrganizeId = orgId;
                    xmEntity.ghnm = mzghEntity.ghnm;
                    xmEntity.patid = mzghEntity.patid;
                    xmEntity.brxz = mzghEntity.brxz;
                    xmEntity.mjzbz = mzghEntity.mjzbz;
                    xmEntity.ys = ys;
                    xmEntity.ks = ks;
                    xmEntity.ysmc = ysmc;
                    xmEntity.ksmc = ksmc;
                }
                #endregion
                if (changedCfnmList.Count > 0 || changedXmnmList.Count > 0)
                {
                    db.Commit();
                }
            }
            return;
        }

        /// <summary>
        /// Cập nh ậtĐ ơn thuốc ngoại trú/Hạng mục ngoại trú của thông tin quyết toán(cập nhật thông tin quyết toán)
        /// (này cfnm có thể đã hủy bỏ,目của có thể là cũng hủy bỏ thông tin quyết toán)(gọi này Phư ơng thức trư ớc,vui lòng trư ớc cập nhật mz_cfmx, mz_xm, và Đ ã持久化 đ ến dữ liệu kho trong)
        /// theo cfnm tra cứu 出 tất cả các của liên quan bảng mz_js mz_jsmx mz_cf mz_cfmx mz_xm, theo mz_cfmx, mz_xm của chính xác dữ liệu cập nhật khác bảng (có thể sẽ thêm mớimz_jsmx)
        /// (có thể sẽ thêm mớimz_js)(check mz_xm, mz_cf zt 0,Tư ơng ứngmz_jsmx cũng cần cập nhật)(nhiều mz_cf có thể Tư ơng ứng một quyết toán)
        /// (nhưng một mz_cf không thể Tư ơng ứng nhiều quyết toán)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="settlementAddBO"></param>
        /// <param name="feeRelated">Số tiền và thanh toán thông tin</param>
        /// <param name="ybfeeRelated">BHYT liên quan chi phí</param>
        /// <param name="xnhybfeeRelated"></param>
        /// <returns>Quay lại mới tạo của mã nội bộ quyết toánList</returns>
        public IList<int> AddSettlement(string orgId, string mzh
            , OutpatientSettlementAddBO settlementAddBO
            , OutpatientSettFeeRelatedDTO feeRelated = null
            , CQMzjs05Dto ybfeeRelated = null
            , S25ResponseDTO xnhybfeeRelated = null
            , string outTradeNo = null)
        {
            var newJsnmList = new List<int>();

            settlementAddBO.cfnmList = settlementAddBO.cfnmList ?? new List<int>();
            settlementAddBO.xmnmList = settlementAddBO.xmnmList ?? new List<int>();

            if (settlementAddBO.cfnmList.Count == 0 && settlementAddBO.xmnmList.Count == 0)
            {
                return null;
            }
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var mzghEntity = db.IQueryable<OutpatientRegistEntity>(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
                if (mzghEntity == null)
                {
                    throw new FailedException("REGISTINFO_NOT_FOUND", "关联挂号信息异常");
                }
                mzghEntity.ghzt = "1";  //Không có差别至Đ ã quyết toán trạng thái
                db.Update(mzghEntity);

                // trùng lặp 结của phán đoán 
                IList<int> jsnmList = new List<int>();
                if (settlementAddBO.xmnmList.Count > 0)
                {
                    var jT = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.zt == "1" && p.mxnm.HasValue && settlementAddBO.xmnmList.Contains(p.mxnm.Value)).Select(p => p.jsnm).ToList();
                    jsnmList = jsnmList.Union(jT).ToList();
                }
                if (settlementAddBO.cfnmList.Count > 0)
                {
                    var cfmxIdList = db.IQueryable<OutpatientPrescriptionDetailEntity>(p => settlementAddBO.cfnmList.Contains(p.cfnm)).Select(p => p.cfmxId).ToList();
                    if (cfmxIdList.Count > 0)
                    {
                        var jT = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.zt == "1" && p.cf_mxnm.HasValue && cfmxIdList.Contains(p.cf_mxnm.Value)).Select(p => p.jsnm).ToList();
                        jsnmList = jsnmList.Union(jT).ToList();
                    }
                }

                if (jsnmList.Count > 0)
                {
                    if (db.IQueryable<OutpatientSettlementEntity>(p => p.zt == "1" && p.tbz != 1 && jsnmList.Contains(p.jsnm)).Any())
                    {
                        throw new FailedException("REPEATED_SETTLED_ERROR", "重复结算");
                    }
                }

                //结
                var beaddjsCfzbEntityList = db.IQueryable<OutpatientPrescriptionEntity>(p => p.zt == "1" && settlementAddBO.cfnmList.Contains(p.cfnm)).ToList();
                
                var beaddjsMzxmEntityList = db.IQueryable<OutpatientItemEntity>(p => p.zt == "1" && settlementAddBO.xmnmList.Contains(p.xmnm)).ToList();
                if (beaddjsCfzbEntityList.Count > 0 || beaddjsMzxmEntityList.Count > 0)
                {
                    if (settlementAddBO.settSfrq.HasValue || settlementAddBO.isQfyj)
                    {
                        //作là một quyết toán
                        var jsnm = AddSettlement(db, orgId
                            , settlementAddBO.settSfrq, beaddjsCfzbEntityList
                            , beaddjsMzxmEntityList, mzghEntity, feeRelated, ybfeeRelated
                            , xnhybfeeRelated: xnhybfeeRelated
                            , autoGenePlan: settlementAddBO.autoGenePlan
                            , isQfyj: settlementAddBO.isQfyj
                            , fph: (settlementAddBO.isQfyj ? null : settlementAddBO.fph)
                            , outTradeNo: outTradeNo
                            );
                        newJsnmList.Add(jsnm);
                    }
                    else
                    {
                        //Chưa chỉ đ ịnh thu phí ngày nhất đ ịnh là rất 规途径thu phí quyết toán
                        //肯定 sẽ không liên kết số hóa đ ơn
                        var sfrqList = beaddjsCfzbEntityList.Select(p => p.sfrq)
                            .Union(beaddjsMzxmEntityList.Select(p => p.sfrq)).Distinct()
                            .ToList();
                        if (!settlementAddBO.sfrqWithSameJs)
                        {
                            foreach (var sfrq in sfrqList)
                            {
                                //null sẽ riêng lẻ tạo một quyết toán
                                var thiscfzbEntityList = beaddjsCfzbEntityList.Where(p => p.sfrq == sfrq).ToList();
                                var thismzxmEntityList = beaddjsMzxmEntityList.Where(p => p.sfrq == sfrq).ToList();
                                var jsnm = AddSettlement(db, orgId, sfrq, thiscfzbEntityList, thismzxmEntityList, mzghEntity, null, null, null, autoGenePlan: settlementAddBO.autoGenePlan);
                                newJsnmList.Add(jsnm);
                            }
                        }
                        else
                        {
                            DateTime? sfrq = null;
                            if (sfrqList.Count == 1 && sfrqList[0].HasValue)
                            {
                                sfrq = sfrqList[0];
                            }
                            // một quyết toán
                            var jsnm = AddSettlement(db, orgId, sfrq, beaddjsCfzbEntityList, beaddjsMzxmEntityList, mzghEntity, null, null, null, autoGenePlan: settlementAddBO.autoGenePlan);
                            newJsnmList.Add(jsnm);
                        }
                    }
                }

                db.Commit();    // không có cần Commit của sẽ 抛bất thư ờng？
            }

            return newJsnmList;
        }

        /// <summary>
        /// Hoàn phí ngoại trú(không cần liên kết Đ ơn thuốc ngoại trú, hạng mục)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"> trả lại chi tiết quyết toánList jsmxnm:Số lư ợng trả lại số lư ợng trả lại 正数</param>
        /// <param name="expectedTmxZje">预 kỳ hoàn phí hạng mục số tiền tổng cộng (Đ ơn giá*Số lư ợng trả lại), ngăn chặn quá trình trong dữ liệu发生 thay đ ổi </param>
        /// <param name="newfph"> mới quyết toán số hóa đ ơn</param>
        /// <param name="newJszbInfo">Tạo của mới quyết toán thông tin quyết toán</param>
        /// <param name="outTradeNo">原 thanh toán giao dịch số </param>
        /// <param name="refundAmount"> trả lại tiền mặt số tiền(tiền mặt, thanh toán 宝, 微信V.v.)</param>
        /// <param name="hcybjsh">Hoàn phí.BHYT giao dịch. ghi đ ỏ hoàn trả số quyết toán</param>
        /// <param name="newwjybjsh">Hoàn phí.BHYT giao dịch. mới của số quyết toán</param>
        /// <param name="hcybfeeRelated"> ghi đ ỏ hoàn trả quyết toán của quyết toánBHYTsố tiền liên quan </param>
        /// <param name="newybfeeRelated">Hoàn phí.BHYT giao dịch. mới của quyết toán quyết toánBHYTquay lạichi phí thông tin</param>
        /// <param name="guiannewybfeeRelated">贵安và Trùng Khánh BHYTsử dụng, do BHYTquay lại và常熟 trư ờng khác nhau </param>
        /// <param name="s25ResponseDto">贵安 BHYT nông thôn mới BHYTsử dụng,quyết toánquay lại kết quả</param>
        /// <param name="outpid">贵安 BHYT nông thôn mới BHYTsử dụng,số thứ tự bồi thư ờng ngoại trú</param>
        public void RefundSettlement(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict, decimal expectedTmxZje
            , string newfph
            , out object newJszbInfo, out string outTradeNo, out decimal refundAmount, string hcybjsh = null, string newwjybjsh = null
            , CQMzjs05Dto hcybfeeRelated = null
            , OutpatientSettYbFeeRelatedDTO newybfeeRelated = null
            , CQMzjs05Dto guiannewybfeeRelated = null
            , GuiAnMztfProDto guianMztfProDto = null
            , S25ResponseDTO s25ResponseDto = null
            , string outpId = "")
        {
            bool ytw = false;

            outTradeNo = null;
            refundAmount = 0;
            var medicalInsurance = _sysConfigRepo.GetValueByCode("Outpatient_MedicalInsurance", orgId);
            decimal oldxjzf;
            OutpatientSettlementEntity newJszbEntity = null;

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var oldJszbEntity = db.IQueryable<OutpatientSettlementEntity>(p => p.jsnm == jsnm && p.zt == "1" && (p.tbz == null || p.tbz == 0)).FirstOrDefault();
                var oldJsmxEntityList = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.jsnm == jsnm && p.zt == "1").ToList();
                oldxjzf = oldJszbEntity.xjzf;
                //insert list
                var newJszbEntityList = new List<OutpatientSettlementEntity>();
                var newJsmxEntityList = new List<OutpatientSettlementDetailEntity>();

                // trư ớc trả lại 流水 // đ ối với 冲
                var newDcJszbEntity = oldJszbEntity.Clone();
                newDcJszbEntity.LastModifyTime = null;
                newDcJszbEntity.LastModifierCode = null;
                newDcJszbEntity.jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js");
                newDcJszbEntity.tbz = 1;  // trả lại hoặc đ ược trả lại đ ều là1
                newDcJszbEntity.jszt = (int)EnumJieSuanZT.YT;   //Đ ã hoàn
                newDcJszbEntity.cxjsnm = oldJszbEntity.jsnm;    // liên kết oldjsnm
                newJszbEntityList.Add(newDcJszbEntity);
                foreach (var jsmxEntity in oldJsmxEntityList)
                {
                    var newJsmxEntity = jsmxEntity.Clone();
                    newJsmxEntity.LastModifyTime = null;
                    newJsmxEntity.LastModifierCode = null;
                    newJsmxEntity.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                    newJsmxEntity.jsnm = newDcJszbEntity.jsnm;

                    newJsmxEntityList.Add(newJsmxEntity);
                }

                // sau mới 
                newJszbEntity = oldJszbEntity.Clone();
                newJszbEntity.LastModifyTime = null;
                newJszbEntity.LastModifierCode = null;
                newJszbEntity.jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js");
                newJszbEntity.fph = newfph;
                newJszbEntity.ysk = null;   //2018-11-14 quyết toán mới phát sinh do hoàn phí tạm thu mặc đ ịnh dùng thanh toán tiền mặt lần này(ngoại trú biên lai hiển thị)
                newJszbEntity.zl = null;
                newJszbEntity.ybjslsh = newwjybjsh;
                newJszbEntityList.Add(newJszbEntity);

                UpdateCurrentFinancialInvoice(db, orgId, newfph);

                newJszbEntity.zje = 0;  //foreach trong 累加
                decimal tjehj = Convert.ToDecimal(0.0000);  //预 kỳ hoàn phí hạng mục số tiền tổng cộng (Đ ơn giá*Số lư ợng trả lại) //foreach trong 累加
				foreach (var jsmxEntity in oldJsmxEntityList)
                {
                    var newJsmxEntity = jsmxEntity.Clone();
                    if (newJsmxEntity.cf_mxnm > 0)
                    {
                        //Thuốc Đ ã hoàn qua, đ ó có thể số lư ợng trả lại thì là0 phảiV.v. chờ nhà thuốc 发起 mới của trả thuốc 
                        newJsmxEntity.ktsl = 0;
                    }
                    newJsmxEntity.LastModifyTime = null;
                    newJsmxEntity.LastModifierCode = null;
                    newJsmxEntity.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                    newJsmxEntity.jsnm = newJszbEntity.jsnm;
                    //
                    decimal tsl = 0;
                    if (tjsxmDict.TryGetValue(jsmxEntity.jsmxnm, out tsl))
                    {
                        // cần trả lại 
                        if (jsmxEntity.sl > 0)
                        {
                            decimal thisDj = 0;
                            if (jsmxEntity.cf_mxnm > 0)
                            {
                                var thisMxEntity = db.IQueryable<OutpatientPrescriptionDetailEntity>(p => p.cfmxId == jsmxEntity.cf_mxnm).FirstOrDefault();
                                if (thisMxEntity == null)
                                {
                                    throw new FailedException("处方药品明细数据错误，未找到");
                                }
                                thisDj = thisMxEntity.dj;
                            }
                            else if (jsmxEntity.mxnm > 0)
                            {
                                var thisMxEntity = db.IQueryable<OutpatientItemEntity>(p => p.xmnm == jsmxEntity.mxnm).FirstOrDefault();
                                if (thisMxEntity == null)
                                {
                                    throw new FailedException("门诊项目数据错误，未找到");
                                }
                                thisDj = thisMxEntity.dj;
                            }
                            newJsmxEntity.sl = newJsmxEntity.sl - Math.Abs(tsl);
                            newJsmxEntity.jyje = newJsmxEntity.sl * thisDj;

                            tjehj += Math.Abs(tsl) * thisDj;
                        }
                        else
                        {
                            throw new FailedException("数量为0，不可再退！");
                        }
                    }

                    newJszbEntity.zje += (newJsmxEntity.jyje ?? 0);
                    newJsmxEntityList.Add(newJsmxEntity);
                }
				//// loại bỏ 2019-09-12 因 bốn vị 小数问题, thư ờng xuyên báo lỗi, xem xét loại bỏ 
				if (tjehj != expectedTmxZje)
				{
					throw new FailedException("退款金额异常.");
				}

                if (hcybfeeRelated != null && medicalInsurance == "changshu")
                {
                    // này là một lần BHYThoàn phí
                    newJszbEntity.xjzf = newybfeeRelated == null ? 0 : (newybfeeRelated.XJZF ?? 0);
                    //2018-11-14 quyết toán mới phát sinh do hoàn phí tạm thu mặc đ ịnh dùng thanh toán tiền mặt lần này(ngoại trú biên lai hiển thị)
                    newJszbEntity.ysk = newJszbEntity.xjzf;
                    ytw = newybfeeRelated == null || (newybfeeRelated.ZFY ?? 0) <= 0;   //Đ ã hoàn hết hay chưa

                    //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                    if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf - newJszbEntity.xjzf > 0)
                    {
                        refundAmount = oldJszbEntity.xjzf - newJszbEntity.xjzf;  //Nên取 thanh toán của
                        outTradeNo = newJszbEntity.OutTradeNo;
                    }
                }
                else if (!string.IsNullOrEmpty(hcybjsh) && guiannewybfeeRelated != null && medicalInsurance == "guian")
                {
                    // này là một lần BHYThoàn phí
                    newJszbEntity.xjzf = guianMztfProDto == null ? 0 : (guianMztfProDto.XJZF ?? 0);
                    //2018-11-14 quyết toán mới phát sinh do hoàn phí tạm thu mặc đ ịnh dùng thanh toán tiền mặt lần này(ngoại trú biên lai hiển thị)
                    newJszbEntity.ysk = newJszbEntity.xjzf;
                    ytw = guianMztfProDto == null || (guianMztfProDto.ZFY ?? 0) <= 0;   //Đ ã hoàn hết hay chưa

                    //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                    if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf - newJszbEntity.xjzf > 0)
                    {
                        refundAmount = oldJszbEntity.xjzf - newJszbEntity.xjzf;  //Nên取 thanh toán của
                        outTradeNo = newJszbEntity.OutTradeNo;
                    }
                }
                else if (guiannewybfeeRelated != null)// (!string.IsNullOrEmpty(hcybjsh) && guiannewybfeeRelated != null && medicalInsurance == "chongqing")
                {
                    // này là một lần BHYThoàn phí
                    newJszbEntity.xjzf = guianMztfProDto == null ? 0 : (guianMztfProDto.XJZF ?? 0);
                    //2018-11-14 quyết toán mới phát sinh do hoàn phí tạm thu mặc đ ịnh dùng thanh toán tiền mặt lần này(ngoại trú biên lai hiển thị)
                    newJszbEntity.ysk = newJszbEntity.xjzf;
                    ytw = guianMztfProDto == null || (guianMztfProDto.ZFY ?? 0) <= 0;   //Đ ã hoàn hết hay chưa

                    //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                    if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf - newJszbEntity.xjzf > 0)
                    {
                        refundAmount = oldJszbEntity.xjzf - newJszbEntity.xjzf;  //Nên取 thanh toán của
                        outTradeNo = newJszbEntity.OutTradeNo;
                    }
                }
				else if (s25ResponseDto != null && medicalInsurance == "guian")
                {
                    newJszbEntity.xjzf = newJszbEntity.zje - HIS.Proxy.guian.Common.CalculationXnhZhzf(s25ResponseDto);
                    newJszbEntity.ysk = newJszbEntity.xjzf;
                    ytw = oldJszbEntity.zje == newJszbEntity.zje;//Đ ã hoàn hết hay chưa

                    //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                    if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf - newJszbEntity.xjzf > 0)
                    {
                        refundAmount = oldJszbEntity.xjzf - newJszbEntity.xjzf;  //Nên取 thanh toán của
                        outTradeNo = newJszbEntity.OutTradeNo;
                    }
                }
                else
                {
                    if (oldJszbEntity.xjzf != oldJszbEntity.zje)
                    {
                        // không phải 全tự trả quyết toán bản ghi 
                        if (tjehj != oldJszbEntity.zje)
                        {
                            throw new FailedException("非全自费结算记录只支持全退，请重试");
                        }
                        newJszbEntity.xjzf = 0; // trả lại 完 
                        ytw = true;   //Đ ã hoàn完

                        //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                        if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf > 0)
                        {
                            refundAmount = oldJszbEntity.xjzf;  //Nên取 thanh toán của
                            outTradeNo = newJszbEntity.OutTradeNo;
                        }
                    }
                    else
                    {
                        newJszbEntity.xjzf = newJszbEntity.zje;
                        //2018-11-14 quyết toán mới phát sinh do hoàn phí tạm thu mặc đ ịnh dùng thanh toán tiền mặt lần này(ngoại trú biên lai hiển thị)
                        newJszbEntity.ysk = newJszbEntity.xjzf;
                        ytw = newJszbEntity.zje <= 0;   //Đ ã hoàn hết hay chưa

                        //Tính số tiền mặt hoàn lại // điều kiện tiên quyết: chỉ một phư ơng thức thanh toán // chỉ một phư ơng thức thanh toán mz_js.xjzffs sẽ có giá trị 
                        if (!string.IsNullOrWhiteSpace(newJszbEntity.xjzffs) && oldJszbEntity.xjzf - newJszbEntity.xjzf > 0)
                        {
                            refundAmount = oldJszbEntity.xjzf - newJszbEntity.xjzf;  //Nên取 thanh toán của
                            outTradeNo = newJszbEntity.OutTradeNo;
                        }
                    }
                }

                // thay đ ổi dữ liệu kho 
                oldJszbEntity.tbz = 1;
                oldJszbEntity.Modify();
                db.Update(oldJszbEntity);
                //
                for (var iIndex = 0; iIndex < newJszbEntityList.Count; iIndex++)
                {
                    newJszbEntityList[iIndex].Create();
                    newJszbEntityList[iIndex].CreateTime = DateTime.Now.AddSeconds(iIndex);
                    db.Insert(newJszbEntityList[iIndex]);
                }
                //
                foreach (var jsmxEntity in newJsmxEntityList)
                {
                    jsmxEntity.Create();
                    db.Insert(jsmxEntity);
                }

                var isYbjyjz = false;

                switch (medicalInsurance)
                {
                    case "guian":
                        isYbjyjz = !string.IsNullOrWhiteSpace(hcybjsh) && !string.IsNullOrWhiteSpace(newwjybjsh);
                        break;
	                case "chongqing":
		                isYbjyjz = !string.IsNullOrWhiteSpace(hcybjsh) && !string.IsNullOrWhiteSpace(newwjybjsh);
		                break;
					case "changshu":
                        isYbjyjz = !string.IsNullOrWhiteSpace(hcybjsh) && !string.IsNullOrWhiteSpace(newwjybjsh) && hcybfeeRelated != null;
                        break;
                }

                #region 医保结算相关
                if (isYbjyjz)
                {

                    if (!string.IsNullOrWhiteSpace(medicalInsurance) && medicalInsurance == "changshu")
                    {
                        #region 常熟医保逻辑
                        var oldJsYbFeeEntity = db.IQueryable<OutpatientSettlementYBFeeEntity>(p => p.jsnm == jsnm && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
                        if (oldJsYbFeeEntity != null)
                        {
                            var hcybFeeEntity = new OutpatientSettlementYBFeeEntity() { };
                            hcybFeeEntity.jsnm = newDcJszbEntity.jsnm;
                            hcybFeeEntity.ybjsh = hcybjsh;
                            hcybFeeEntity.OrganizeId = orgId;
                            hcybFeeEntity.ZFY = hcybfeeRelated.ZFY;
                            hcybFeeEntity.XJZF = hcybfeeRelated.XJZF;
                            hcybFeeEntity.YBFY = hcybfeeRelated.YBFY;
                            hcybFeeEntity.TSFY = hcybfeeRelated.TSFY;
                            hcybFeeEntity.JBZF = hcybfeeRelated.JBZF;
                            hcybFeeEntity.GBZF = hcybfeeRelated.GBZF;
                            hcybFeeEntity.SUMZFDYBFY = hcybfeeRelated.SUMZFDYBFY;
                            hcybFeeEntity.ZFDYBFY = hcybfeeRelated.ZFDYBFY;
                            hcybFeeEntity.JBYE = hcybfeeRelated.JBYE;
                            hcybFeeEntity.GBYE = hcybfeeRelated.GBYE;
                            hcybFeeEntity.KBXYBFY = hcybfeeRelated.KBXYBFY;
                            hcybFeeEntity.KBXTSFY = hcybfeeRelated.KBXTSFY;
                            hcybFeeEntity.TCZF = hcybfeeRelated.TCZF;
                            hcybFeeEntity.JZZF = hcybfeeRelated.JZZF;
                            hcybFeeEntity.DKC023 = hcybfeeRelated.DKC023;
                            hcybFeeEntity.Create();
                            db.Insert(hcybFeeEntity);
                        }

                        var newybFeeEntity = new OutpatientSettlementYBFeeEntity() { };
                        newybFeeEntity.jsnm = newJszbEntity.jsnm;
                        newybFeeEntity.ybjsh = newwjybjsh;
                        newybFeeEntity.OrganizeId = orgId;
                        newybFeeEntity.ZFY = newybfeeRelated == null ? null : newybfeeRelated.ZFY;
                        newybFeeEntity.XJZF = newybfeeRelated == null ? null : newybfeeRelated.XJZF;
                        newybFeeEntity.YBFY = newybfeeRelated == null ? null : newybfeeRelated.YBFY;
                        newybFeeEntity.TSFY = newybfeeRelated == null ? null : newybfeeRelated.TSFY;
                        newybFeeEntity.JBZF = newybfeeRelated == null ? null : newybfeeRelated.JBZF;
                        newybFeeEntity.GBZF = newybfeeRelated == null ? null : newybfeeRelated.GBZF;
                        newybFeeEntity.SUMZFDYBFY = newybfeeRelated == null ? null : newybfeeRelated.SUMZFDYBFY;
                        newybFeeEntity.ZFDYBFY = newybfeeRelated == null ? null : newybfeeRelated.ZFDYBFY;
                        //Hoàn toàn bộ giờ, không 产生 mới của quyết toán, số dư thì取 ghi đ ỏ hoàn trả đ ó một dòng quyết toán của số dư 
                        newybFeeEntity.JBYE = newybfeeRelated == null ? hcybfeeRelated.JBYE : newybfeeRelated.JBYE;
                        newybFeeEntity.GBYE = newybfeeRelated == null ? hcybfeeRelated.GBYE : newybfeeRelated.GBYE;
                        newybFeeEntity.KBXYBFY = newybfeeRelated == null ? null : newybfeeRelated.KBXYBFY;
                        newybFeeEntity.KBXTSFY = newybfeeRelated == null ? null : newybfeeRelated.KBXTSFY;
                        newybFeeEntity.TCZF = newybfeeRelated == null ? null : newybfeeRelated.TCZF;
                        newybFeeEntity.JZZF = newybfeeRelated == null ? null : newybfeeRelated.JZZF;
                        newybFeeEntity.DKC023 = newybfeeRelated == null ? null : newybfeeRelated.DKC023;
                        newybFeeEntity.Create();
                        db.Insert(newybFeeEntity);
                        #endregion
                    }
                    if (!string.IsNullOrWhiteSpace(medicalInsurance) && medicalInsurance == "guian")
                    {
                        #region 贵安医保逻辑
                        var oldJsYbFeeEntity = db.IQueryable<OutpatientSettlementGAYBFeeEntity>(p => p.jsnm == jsnm && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
                        if (oldJsYbFeeEntity != null)
                        {
                            var hcybFeeEntity = new OutpatientSettlementGAYBFeeEntity() { };
                            hcybFeeEntity.jsnm = newDcJszbEntity.jsnm;
                            hcybFeeEntity.OrganizeId = orgId;
                            hcybFeeEntity.prm_yka198 = hcybjsh;
                            hcybFeeEntity.Create();
                            db.Insert(hcybFeeEntity);
                        }
                        var newybFeeEntity = new OutpatientSettlementGAYBFeeEntity()
                        {
                            OrganizeId = orgId,
                            astr_jylsh = guianMztfProDto.astr_jylsh,
                            astr_jyyzm = guianMztfProDto.astr_jyyzm,
                            aint_appcode = guianMztfProDto.aint_appcode,
                            astr_appmsg = guianMztfProDto.astr_appmsg,
                            prm_akc190 = guiannewybfeeRelated.prm_akc190,
                            prm_yab003 = guiannewybfeeRelated.prm_yab003,
                            prm_yka103 = guiannewybfeeRelated.prm_yka103,
                            prm_aac001 = guiannewybfeeRelated.prm_aac001,
                            prm_yka065 = guiannewybfeeRelated.prm_yka065,
                            prm_aae036 = guiannewybfeeRelated.prm_aae036,
                            prm_yka055 = guiannewybfeeRelated.prm_yka055,
                            prm_yka056 = guiannewybfeeRelated.prm_yka056,
                            prm_yka057 = guiannewybfeeRelated.prm_yka057,
                            prm_yka111 = guiannewybfeeRelated.prm_yka111,
                            prm_yka058 = guiannewybfeeRelated.prm_yka058,
                            prm_yka248 = guiannewybfeeRelated.prm_yka248,
                            prm_yka062 = guiannewybfeeRelated.prm_yka062,
                            prm_yke030 = guiannewybfeeRelated.prm_yke030,
                            prm_ake032 = guiannewybfeeRelated.prm_ake032,
                            prm_ake181 = guiannewybfeeRelated.prm_ake181,
                            prm_ake173 = guiannewybfeeRelated.prm_ake173,
                            prm_akc087 = guiannewybfeeRelated.prm_akc087,
                            prm_ykb037 = guiannewybfeeRelated.prm_ykb037,
                            prm_yka316 = guiannewybfeeRelated.prm_yka316,
                            prm_akc021 = guiannewybfeeRelated.prm_akc021,
                            prm_ykc120 = guiannewybfeeRelated.prm_ykc120,
                            prm_yab139 = guiannewybfeeRelated.prm_yab139,
                            prm_aac003 = guiannewybfeeRelated.prm_aac003,
                            prm_aac004 = guiannewybfeeRelated.prm_aac004,
                            prm_aac002 = guiannewybfeeRelated.prm_aac002,
                            prm_aac006 = guiannewybfeeRelated.prm_aac006,
                            prm_akc023 = guiannewybfeeRelated.prm_akc023,
                            prm_aab001 = guiannewybfeeRelated.prm_aab001,
                            prm_aab004 = guiannewybfeeRelated.prm_aab004,
                            prm_aac031 = guiannewybfeeRelated.prm_aac031,
                            prm_ykc280 = guiannewybfeeRelated.prm_ykc280,
                            prm_ykc281 = guiannewybfeeRelated.prm_ykc281,
                            prm_yka054 = guiannewybfeeRelated.prm_yka054,
                            prm_yae366 = guiannewybfeeRelated.prm_yae366,
                            prm_akc090 = guiannewybfeeRelated.prm_akc090,
                            prm_yka120 = guiannewybfeeRelated.prm_yka120,
                            prm_yka122 = guiannewybfeeRelated.prm_yka122,
                            prm_yka368 = guiannewybfeeRelated.prm_yka368,
                            prm_yke025 = guiannewybfeeRelated.prm_yke025,
                            prm_yka900 = guiannewybfeeRelated.prm_yka900,
                            prm_aae001 = guiannewybfeeRelated.prm_aae001,
                            prm_yka089 = guiannewybfeeRelated.prm_yka089,
                            prm_yka027 = guiannewybfeeRelated.prm_yka027,
                            prm_yka028 = guiannewybfeeRelated.prm_yka028,
                            prm_yka345 = guiannewybfeeRelated.prm_yka345,
                            jsnm = newJszbEntity.jsnm
                        };
                        newybFeeEntity.Create();
                        db.Insert(newybFeeEntity);
                        #endregion
                    }
	                if (!string.IsNullOrWhiteSpace(medicalInsurance) && medicalInsurance == "chongqing")
	                {
		                #region 重庆医保逻辑
		                var oldJsYbFeeEntity = db.IQueryable<CqybSett05Entity>(p => p.jsnm == jsnm && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
						// ghi đ ỏ hoàn trả giao dịch 落 đ ịa 
		                if (oldJsYbFeeEntity != null && hcybfeeRelated !=null)
		                {
                            var hcybFeeEntity = new CqybSett05Entity() {
                                jslb = "2",
                                OrganizeId = orgId,
                                jsnm = jsnm,
                                jylsh = hcybfeeRelated.jylsh,
                                cqtczf = hcybfeeRelated.cqtczf,
                                zhzf = hcybfeeRelated.zhzf,
                                gwybz = hcybfeeRelated.gwybz,
                                cqxjzf = hcybfeeRelated.cqxjzf,
                                delpje = hcybfeeRelated.delpje,
                                lsqfxgwyff = hcybfeeRelated.lsqfxgwyff,
                                zhye = hcybfeeRelated.zhye,
                                dbzddyljgdz = hcybfeeRelated.dbzddyljgdz,
                                mzjzje = hcybfeeRelated.mzjzje,
                                mzjzmzye = hcybfeeRelated.mzjzmzye,
                                ndyxmzfje = hcybfeeRelated.ndyxmzfje,
                                ybzlzfs = hcybfeeRelated.ybzlzfs,
                                shjzjjzfs = hcybfeeRelated.shjzjjzfs,
                                bntczflj = hcybfeeRelated.bntczflj,
                                bndezflj = hcybfeeRelated.bndezflj,
                                tbqfxzflj = hcybfeeRelated.tbqfxzflj,
                                ndyxmlj = hcybfeeRelated.ndyxmlj,
                                bnmzjzzyzflj = hcybfeeRelated.bnmzjzzyzflj,
                                zxjssj = DateTime.Now,//hcybfeeRelated.zxjssj,
				                bcqfxzfje = hcybfeeRelated.bcqfxzfje,
				                bcjrybfwfy = hcybfeeRelated.bcjrybfwfy,
				                ysfwzfs = hcybfeeRelated.ysfwzfs,
				                yycbkkje = hcybfeeRelated.yycbkkje,
				                syjjzf = hcybfeeRelated.syjjzf,
				                syxjzf = hcybfeeRelated.syxjzf,
				                gsjjzf = hcybfeeRelated.gsjjzf,
				                gsxjzf = hcybfeeRelated.gsxjzf,
				                gsdbzjgdz = hcybfeeRelated.gsdbzjgdz,
				                gsqzfyy = hcybfeeRelated.gsqzfyy,
				                qtbz = hcybfeeRelated.qtbz,
				                syzhzf = hcybfeeRelated.syzhzf,
				                gszhzf = hcybfeeRelated.gszhzf,
				                bcjssfjkfpry = hcybfeeRelated.bcjssfjkfpry,
				                jkfpyljj = hcybfeeRelated.jkfpyljj,
				                jztpbxje = hcybfeeRelated.jztpbxje,
				                zt = "1"
			                };
			                hcybFeeEntity.Create();
			                db.Insert(hcybFeeEntity);
		                }
		                if (guiannewybfeeRelated !=null && guiannewybfeeRelated.jylsh != "111111111")
		                {
							//BHYT再结thông tin落 đ ịa 
			                var newybFeeEntity = new CqybSett05Entity()
			                {
				                jslb = "1",
				                OrganizeId = orgId,
				                jsnm = newJszbEntity.jsnm,
				                jylsh = guiannewybfeeRelated.jylsh,
				                cqtczf = guiannewybfeeRelated.cqtczf,
				                zhzf = guiannewybfeeRelated.zhzf,
				                gwybz = guiannewybfeeRelated.gwybz,
				                cqxjzf = guiannewybfeeRelated.cqxjzf,
				                delpje = guiannewybfeeRelated.delpje,
				                lsqfxgwyff = guiannewybfeeRelated.lsqfxgwyff,
				                zhye = guiannewybfeeRelated.zhye,
				                dbzddyljgdz = guiannewybfeeRelated.dbzddyljgdz,
				                mzjzje = guiannewybfeeRelated.mzjzje,
				                mzjzmzye = guiannewybfeeRelated.mzjzmzye,
				                ndyxmzfje = guiannewybfeeRelated.ndyxmzfje,
				                ybzlzfs = guiannewybfeeRelated.ybzlzfs,
				                shjzjjzfs = guiannewybfeeRelated.shjzjjzfs,
				                bntczflj = guiannewybfeeRelated.bntczflj,
				                bndezflj = guiannewybfeeRelated.bndezflj,
				                tbqfxzflj = guiannewybfeeRelated.tbqfxzflj,
				                ndyxmlj = guiannewybfeeRelated.ndyxmlj,
				                bnmzjzzyzflj = guiannewybfeeRelated.bnmzjzzyzflj,
				                zxjssj = guiannewybfeeRelated.zxjssj,
				                bcqfxzfje = guiannewybfeeRelated.bcqfxzfje,
				                bcjrybfwfy = guiannewybfeeRelated.bcjrybfwfy,
				                ysfwzfs = guiannewybfeeRelated.ysfwzfs,
				                yycbkkje = guiannewybfeeRelated.yycbkkje,
				                syjjzf = guiannewybfeeRelated.syjjzf,
				                syxjzf = guiannewybfeeRelated.syxjzf,
				                gsjjzf = guiannewybfeeRelated.gsjjzf,
				                gsxjzf = guiannewybfeeRelated.gsxjzf,
				                gsdbzjgdz = guiannewybfeeRelated.gsdbzjgdz,
				                gsqzfyy = guiannewybfeeRelated.gsqzfyy,
				                qtbz = guiannewybfeeRelated.qtbz,
				                syzhzf = guiannewybfeeRelated.syzhzf,
				                gszhzf = guiannewybfeeRelated.gszhzf,
				                bcjssfjkfpry = guiannewybfeeRelated.bcjssfjkfpry,
				                jkfpyljj = guiannewybfeeRelated.jkfpyljj,
				                jztpbxje = guiannewybfeeRelated.jztpbxje,
				                zt = "1"
			                };
			                newybFeeEntity.Create();
			                db.Insert(newybFeeEntity);
						}
						
		                #endregion
	                }
				}
                #endregion 医保结算相关

                if (s25ResponseDto != null)
                {
                    if (!string.IsNullOrWhiteSpace(medicalInsurance) && medicalInsurance == "guian")
                    {
                        #region 贵安新农合医保逻辑
                        var xnhSett = new OutpatientXnhSettlementResultEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            OrganizeId = orgId,
                            jsnm = newDcJszbEntity.jsnm,
                            outpId = outpId,
                            jszt = 1,
                            accRedeem = s25ResponseDto.accRedeem.ToString(CultureInfo.InvariantCulture),
                            bottomRedeem = s25ResponseDto.bottomRedeem ?? "",
                            bottomSecondRedeem = s25ResponseDto.bottomSecondRedeem ?? "",
                            civilCost = s25ResponseDto.civilCost ?? "",
                            compensateCost = s25ResponseDto.compensateCost ?? "",
                            continentInsuranceCost = s25ResponseDto.continentInsuranceCost ?? "",
                            generalRedeem = s25ResponseDto.generalRedeem ?? "",
                            totalCost = s25ResponseDto.totalCost,
                            insuranceCost = s25ResponseDto.insuranceCost,
                            undulatingLine = s25ResponseDto.undulatingLine,
                            insureCost = s25ResponseDto.insureCost ?? "",
                            salvaCLCost = s25ResponseDto.salvaCLCost ?? "",
                            salvaFPCost = s25ResponseDto.salvaFPCost ?? "",
                            salvaJKCost = s25ResponseDto.salvaJKCost ?? "",
                            salvaJSCost = s25ResponseDto.salvaJSCost ?? "",
                            salvaMZCost = s25ResponseDto.salvaMZCost ?? "",
                            salvaYFCost = s25ResponseDto.salvaYFCost ?? "",
                            medicineCost = s25ResponseDto.medicineCost ?? "",
                            specialPovertyCost = s25ResponseDto.specialPovertyCost ?? "",
                            medicalAssistanceCost = s25ResponseDto.medicalAssistanceCost ?? "",
                            medicalSecondCost = s25ResponseDto.medicalSecondCost ?? "",
                        };
                        xnhSett.Create();
                        db.Insert(xnhSett);
                        if (s25ResponseDto.list != null && s25ResponseDto.list.Any())
                        {
                            var xnhSettResultList = new List<OutpatientXnhSettlementCalcDetailEntity>();
                            s25ResponseDto.list.ForEach(p =>
                            {
                                var item = new OutpatientXnhSettlementCalcDetailEntity
                                {
                                    resultId = xnhSett.Id,
                                    OrganizeId = orgId,
                                    calcAfter = p.calcAfter,
                                    calcBefore = p.calcBefore,
                                    calcMemo = p.calcMemo,
                                    calcName = p.calcName
                                };
                                item.Create();
                                xnhSettResultList.Add(item);
                            });
                            db.Insert(xnhSettResultList);
                        }
                        #endregion
                    }
                }

                bool isGhToYT = newJszbEntity.jslx == ((int)EnumJslx.GH).ToString() && newJszbEntity.zje <= 0;
                //Hoặc chỉ đ ơn lẻ quyết toán, và Đ ã hoàn完,thì số trạng th áiĐ ã hoàn
                isGhToYT = isGhToYT || (isYbjyjz && ytw);
                if (!isGhToYT && newJszbEntity.zje <= 0)
                {
                    //Có một loại tình hình,Đ ăng ký khám giờ chưa产生quyết toán
                    //Cấu hình:Khichi phí trả lại 完giờ, số có hay không tự đ ộng trả lại 
                    var configFeeYtwghtoYt = _sysConfigRepo.GetBoolValueByCode("Outpatient_Refund_FeeYTW_GHToYT", orgId, false);
                    if (configFeeYtwghtoYt.Value)
                    {
                        if (!db.IQueryable<OutpatientSettlementEntity>(p => p.ghnm == newJszbEntity.ghnm && p.zt == "1" && p.tbz != 1 && p.zje > 0
                        && p.jsnm != jsnm && p.jsnm != newJszbEntity.jsnm && p.jsnm != newDcJszbEntity.jsnm).Any())
                        {
                            isGhToYT = true;
                        }
                    }
                }
                if (isGhToYT && medicalInsurance != "chongqing") // Trùng Khánh BHYT do Đ ăng ký khámthu phí, không thể tự đ ộng hủy bỏ đăng ký khám
                {
                    //置Đ ăng ký khám trạng thái Đ ã hoàn
                    var ghEnitty = db.IQueryable<OutpatientRegistEntity>(p => p.ghnm == newJszbEntity.ghnm && p.zt == "1" && p.ghzt == "1").FirstOrDefault();
                    if (ghEnitty == null)
                    {
                        throw new FailedException("内部错误，his本地挂号无法退号：未找到有效状态的挂号数据，挂号内码："+ newJszbEntity.ghnm.ToString());
                    }
                    ghEnitty.ghzt = "2";    //Đ ã hoàn
                    ghEnitty.Modify();
                    db.Update(ghEnitty);
                }

                //Nếu trư ớc đ ó là thanh toán tạm ứng, cần 还 khoản (đ ơn lẻ phư ơng thức thanh toán tình hình nhiều loại kết hợp thanh toán tạm thời chỉ có thể trả lại tiền mặt)
                var preYjjPayEntity = db.IQueryable<OutpatientSettlementPaymentModelEntity>(p => p.jsnm == jsnm && p.zfje > 0 && p.zt=="1").ToList();
                if (preYjjPayEntity.Count ==1 && preYjjPayEntity[0].xjzffs== xtzffs.ZYYJZHZF) {
                    int zhcode = (int)preYjjPayEntity.FirstOrDefault().zh;
                    // cho tài khoản tạm ứng nạp tiền- một phần trả lại tình hình trư ớc hoàn toàn bộ lần tài khoản再insert tạm ứng扣减 bản ghi 
                    var accountEntity =
                        db.IQueryable<SysAccountEntity>(p => p.patid == oldJszbEntity.patid && p.zhCode == zhcode && p.zt=="1")
                            .FirstOrDefault();
                    if (accountEntity != null)
                    {
                        var zhszEntity = new SysAccountRevenueAndExpenseEntity()
                        {
                            OrganizeId = oldJszbEntity.OrganizeId,
                            patid= oldJszbEntity.patid,
                            zhCode = accountEntity.zhCode,
                            szje = preYjjPayEntity.FirstOrDefault().zfje,
                            zhye = accountEntity.zhye + preYjjPayEntity.FirstOrDefault().zfje,
                            pzh = null,
                            szxz = (int)EnumSZXZ.mzjsth,
                            xjzffs = Constants.xtzffs.ZYYJZHZF,
                            jsnm = newDcJszbEntity.jsnm,
                            zt = "1",
                        };
                        zhszEntity.Create(true);
                        db.Insert(zhszEntity);
                        if (newJszbEntity.xjzf > 0)
                        {
                            var newzhszEntity = new SysAccountRevenueAndExpenseEntity()
                            {
                                OrganizeId = oldJszbEntity.OrganizeId,
                                patid = oldJszbEntity.patid,
                                zhCode = accountEntity.zhCode,
                                szje = 0- newJszbEntity.xjzf,
                                zhye = accountEntity.zhye + preYjjPayEntity.FirstOrDefault().zfje- newJszbEntity.xjzf,
                                pzh = null,
                                szxz = (int)EnumSZXZ.mzjs,
                                xjzffs = Constants.xtzffs.ZYYJZHZF,
                                jsnm = newJszbEntity.jsnm,
                                zt = "1",
                            };
                            newzhszEntity.Create(true);
                            db.Insert(newzhszEntity);
                            //insert mz_jszffs 
                            var newmzzfEntity = new OutpatientSettlementPaymentModelEntity()
                            {
                                mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs"),
                                OrganizeId = oldJszbEntity.OrganizeId,
                                jsnm = newJszbEntity.jsnm,
                                xjzffs = xtzffs.ZYYJZHZF,
                                zfje = newJszbEntity.xjzf,
                                zh= accountEntity.zhCode,
                                zt = "1",
                            };
                            newmzzfEntity.Create();
                            db.Insert(newmzzfEntity);
                        }
                        //Cập nhậtSố dư tài khoản
                        accountEntity.zhye = accountEntity.zhye + preYjjPayEntity.FirstOrDefault().zfje - newJszbEntity.xjzf;
                        db.Update(accountEntity);
                    }
                }
                db.Commit();
            }
            newJszbInfo = new
            {
                ytw = ytw,
                jsnm = newJszbEntity.jsnm,
                // mới củaBHYT giao dịch số quyết toán
                ybjsh = newwjybjsh,
                jszje = newJszbEntity.zje,
                jsxjzf = newJszbEntity.xjzf,
                xjzlje = oldxjzf - newJszbEntity.xjzf,
                YBZHZC = guiannewybfeeRelated != null ? guiannewybfeeRelated.zhzf : null,
                JBYE = guiannewybfeeRelated != null ? guiannewybfeeRelated.zhye : 0,
                GBYE = guiannewybfeeRelated != null ? guiannewybfeeRelated.GBYE : null,
            };
        }

        /// <summary>
        /// kế hoạch hoàn phí全停
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        public void AccountPlanRefundAll(string orgId, int jsnm)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var jsyj = (int)EnumJieSuanZT.YJ;
                var jszbEntity = db.IQueryable<OutpatientSettlementEntity>(p => p.jsnm == jsnm && p.OrganizeId == orgId && p.zt == "1" && p.jszt == jsyj && p.tbz != 1).FirstOrDefault();
                if (jszbEntity != null)
                {
                    var jsmxEntityList = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.jsnm == jsnm && p.OrganizeId == orgId).ToList();
                    if (jsmxEntityList.Any(p => p.zt != "1") || jsmxEntityList.Count == 0)
                    {
                        throw new FailedException("非原始费用记录，操作失败");
                    }
                    var xmnmList = jsmxEntityList.Select(p => p.mxnm).Where(p => p > 0).Distinct().ToList();
                    var xmEntityList = db.IQueryable<OutpatientItemEntity>(p => xmnmList.Contains(p.xmnm) && p.zt == "1").ToList();
                    if (xmEntityList.Count != jsmxEntityList.Count)
                    {
                        throw new FailedException("非原始费用记录，操作失败.");
                    }
                    var jzjhmxIdList = xmEntityList.Select(p => p.jzjhmxId).Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().ToList();
                    var jzjhmxEntityList = db.IQueryable<OutpatientAccountDetailEntity>(p => jzjhmxIdList.Contains(p.jzjhmxId) && p.zt == "1").ToList();
                    if (jzjhmxEntityList.Count != jsmxEntityList.Count)
                    {
                        throw new FailedException("非原始费用记录，操作失败..");
                    }
                    var jzjhId = jzjhmxEntityList[0].jzjhId;
                    var sourceJzjhmxEntityList = db.IQueryable<OutpatientAccountDetailEntity>(p => p.jzjhId == jzjhId).ToList();
                    if (jzjhmxEntityList.Count != sourceJzjhmxEntityList.Count)
                    {
                        throw new FailedException("非原始费用记录，操作失败...");
                    }

                    //Quyết toán bảng chính 
                    jszbEntity.jszt = (int)EnumJieSuanZT.YT;
                    jszbEntity.tbz = 1;
                    jszbEntity.Modify();
                    db.Update(jszbEntity);
                    //Chi tiết quyết toán
                    jsmxEntityList.ForEach(p =>
                    {
                        p.zt = "0";
                        p.Modify();
                        db.Update(p);
                    });
                    //Chi tiết kế hoạch ghi sổ
                    jzjhmxEntityList.ForEach(p =>
                    {
                        p.zt = "0";
                        p.Modify();
                        db.Update(p);
                    });
                    // kế hoạch bảng chính 

                    db.Commit();
                }
            }
        }

        /// <summary>
        /// Hủy bỏ đơn thuốc(Đ ã quyết toán nhất đ ịnh là không thể hủy bỏ)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnmList">Mã nội bộ đ ơn thuốcList</param>
        /// <param name="cfhList">Số đ ơn thuốcList</param>
        /// <param name="settledUpdateForbidden">Đ ã quyết toán giờ có hay không禁止cập nhật</param>
        public void CancelPrescription(string orgId
            , IList<int> cfnmList = null
            , IList<string> cfhList = null
            , bool settledUpdateForbidden = true)
        {
            cfnmList = cfnmList ?? new List<int>();
            cfhList = cfhList ?? new List<string>();
            if (cfnmList.Count == 0 && cfhList.Count == 0)
            {
                return;
            }
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var cfzbEntityList = db.IQueryable<OutpatientPrescriptionEntity>(p => p.zt == "1" && p.OrganizeId == orgId && (cfnmList.Contains(p.cfnm) || cfhList.Contains(p.cfh))).ToList();
                if (cfzbEntityList.Count != (cfnmList.Count + cfhList.Count))
                {
                    // chỉ đ ịnh CủaĐ ơn thuốc tham số có vấn đ ề đ ối với không trên 
                    throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "参数错误定位处方异常");
                }
                foreach (var cfzbEntity in cfzbEntityList)
                {
                    if (cfzbEntity.cfzt == "1" && settledUpdateForbidden)
                    {
                        throw new FailedException("SETTLED_UPDATE_FORBIDDEN", "已结状态不允许修改变更");
                    }
                    //Dữ liệu kho trong củakhi trư ớc chi tiếtList
                    IList<OutpatientItemEntity> itemList = db.IQueryable<OutpatientItemEntity>(p => p.OrganizeId == orgId && p.cfnm == cfzbEntity.cfnm && p.zt == "1").ToList();
                    foreach (var item in itemList)
                    {
                        item.zt = "0";
                        item.Modify();
                        db.Update(item);
                    }

                    cfzbEntity.zt = "0";
                    cfzbEntity.Modify();
                    db.Update(cfzbEntity);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// theo số khám ngoại trú V.v. Tra cứu số đ ơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfzt"></param>
        /// <param name="cfly"></param>
        /// <param name="cfhs"> nhiều số đ ơn thuốc,逗 số 分割</param>
        /// <returns></returns>
        public IList<OutpatientPrescriptionEntity> GetValidCfListByMzh(string orgId, string mzh
            , string cfzt = null, string cfly = null, string cfhs = null)
        {
            var sql = @"select * from mz_cf(nolock)
where zt = '1' and OrganizeId = @orgId
and ghnm in
(
select ghnm from mz_gh(nolock) where mzh = @mzh and zt = '1' and OrganizeId = @orgId
)";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@mzh", mzh ?? "-1111111"));
            if (!string.IsNullOrWhiteSpace(cfzt))
            {
                if (cfzt == "0")
                {
                    sql += " and isnull(cfzt, '') <> '1'";  //Chưa thu phí
                }
                else if (cfzt == "1")
                {
                    sql += " and isnull(cfzt, '') = '1'";   //Đ ãthu phí
                }
            }
            if (!string.IsNullOrWhiteSpace(cfly))
            {
                sql += " and cfly = @cfly";   //Đ ơn thuốc来源
                pars.Add(new SqlParameter("@cfly", cfly));
            }
            if (!string.IsNullOrWhiteSpace(cfhs))
            {
                sql += " and cfh in (select col from [dbo].[f_split](@cfhs, ','))";   //Đ ơn thuốc来源
                pars.Add(new SqlParameter("@cfhs", cfhs));
            }
            return this.FindList<OutpatientPrescriptionEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// Cập nhật quyết toán trư ớc khi nợ phí của bản ghi 至Đ ãthu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="feeRelated">Số tiền và thanh toán thông tin</param>
        /// <param name="sfrq"></param>
        /// <param name="fph"></param>
        public void UpdateArrearageSettlement(string orgId, int jsnm
            , OutpatientSettFeeRelatedDTO feeRelated
            , DateTime sfrq
            , string fph = null)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var jszbEntity = db.IQueryable<OutpatientSettlementEntity>(p => p.jsnm == jsnm && p.zt == "1" && (p.tbz == null || p.tbz == 0)).FirstOrDefault();
                if (!(jszbEntity.isQfyj == true))
                {
                    throw new FailedException("ERROR_NOTQFYJ", "非欠费记录，操作失败");
                }
                if (jszbEntity.zje != feeRelated.zje)
                {
                    throw new Exception("结算总金额异常" + jszbEntity.zje.ToString() + "不等于" + feeRelated.zje.ToString());
                }
                //Tổng số tiền 肯定 sẽ không 变
                jszbEntity.fph = fph;
                jszbEntity.jzsj = sfrq;
                jszbEntity.jmbl = feeRelated.zkbl ?? 0;
                jszbEntity.jmje = feeRelated.zkje ?? 0;
                jszbEntity.xjzf = feeRelated.xjzfys ?? 0;
                //Ghi sổ một phần V.v.
                jszbEntity.ysk = feeRelated.ssk;
                jszbEntity.zl = feeRelated.zhaoling;
                jszbEntity.jszt = (int)EnumJieSuanZT.YJ;    //Đ ã quyết toán
                jszbEntity.isQfyj = null;

                UpdateCurrentFinancialInvoice(db, orgId, fph);

                if (feeRelated.orglxjzfys.HasValue && feeRelated.xjzfys.HasValue)
                {
                    // cần 减去ghi sổ một phần V.v.Số tiền
                    if (feeRelated.orglxjzfys.Value != jszbEntity.zje - 0)
                    {
                        throw new FailedException("ERROR_SETT_JE_ERROR", "结算金额异常" + feeRelated.orglxjzfys.Value.ToString() + "不等于" + jszbEntity.zje.ToString());
                    }
                    //orglxjzfys xjzfys zkbl zkje cần 保持 nhất quán 性
                    var zkje = feeRelated.orglxjzfys.Value * Math.Abs(feeRelated.zkbl ?? 0) + Math.Abs(feeRelated.zkje ?? 0);
                    if (Math.Abs(zkje + feeRelated.xjzfys.Value - feeRelated.orglxjzfys.Value) > (decimal)0.01)
                    {
                        throw new FailedException("ERROR_ZKJE_ZFJE_ERROR", "折扣金额异常");
                    }
                }
                else
                {
                    //???xjzf có必 cần gán giá trị 么
                    // cần 减去ghi sổ một phần V.v.Số tiền
                    jszbEntity.xjzf = jszbEntity.zje - 0;
                }

                if (feeRelated.ssk.HasValue && feeRelated.zhaoling.HasValue)
                {
                    //Sai lệch tiền mặt nhận đ ược của-phải thu
                    jszbEntity.xjwc = jszbEntity.xjzf - (feeRelated.ssk.Value - feeRelated.zhaoling.Value);
                    if (Math.Abs(jszbEntity.xjwc) >= (decimal)0.1)
                    {
                        throw new FailedException("ERROR_SSK_ZHAOLING", "实收找零金额异常");
                    }
                }

                jszbEntity.Modify();
                db.Update(jszbEntity);

                PaymentModelAccountReserve(db, jszbEntity, feeRelated);

                //
                var jsmxEntityList = db.IQueryable<OutpatientSettlementDetailEntity>(p => p.jsnm == jsnm && p.zt == "1").ToList();
                var mzxmnmList = jsmxEntityList.Where(p => (p.mxnm ?? 0) > 0).Select(p => p.mxnm.Value).ToList();
                var cfmxIdList = jsmxEntityList.Where(p => (p.cf_mxnm ?? 0) > 0).Select(p => p.cf_mxnm.Value).ToList();
                var cfnmList = new List<int>();
                if (mzxmnmList.Count > 0)
                {
                    var mzxmEntityList = db.IQueryable<OutpatientItemEntity>(p => mzxmnmList.Contains(p.xmnm)).ToList();
                    foreach (var mzxmEntity in mzxmEntityList)
                    {
                        mzxmEntity.xmzt = "1";  //Đ ã quyết toán
                        mzxmEntity.sfrq = sfrq;
                        mzxmEntity.Modify();
                        db.Update(mzxmEntity);
                    }
                    cfnmList.AddRange(mzxmEntityList.Select(p => p.cfnm ?? 0));
                    //mz_jzjhmxCủajzsj
                    var jzjhmxIdList = mzxmEntityList.Select(p => p.jzjhmxId).ToList();
                    var jzjhmxEntityList = db.IQueryable<OutpatientAccountDetailEntity>(p => jzjhmxIdList.Contains(p.jzjhmxId) && p.zt == "1").ToList();
                    foreach (var jzjhmxEntity in jzjhmxEntityList)
                    {
                        jzjhmxEntity.jzsj = sfrq;
                        jzjhmxEntity.Modify();
                        db.Update(jzjhmxEntity);
                    }
                }
                if (cfmxIdList.Count > 0)
                {
                    var cfmxEntityList = db.IQueryable<OutpatientPrescriptionDetailEntity>(p => cfmxIdList.Contains(p.cfmxId)).ToList();
                    cfnmList.AddRange(cfmxEntityList.Select(p => p.cfnm));
                }
                cfnmList = cfnmList.Where(p => p > 0).Distinct().ToList();
                if (cfnmList.Count > 0)
                {
                    var cfEntityList = db.IQueryable<OutpatientPrescriptionEntity>(p => cfnmList.Contains(p.cfnm)).ToList();
                    foreach (var cfEntity in cfEntityList)
                    {
                        cfEntity.cfzt = "1";  //Đ ã quyết toán
                        cfEntity.sfrq = sfrq;
                        cfEntity.Modify();
                        db.Update(cfEntity);
                    }
                }

                db.Commit();
            }
        }

        #region private methods

        /// <summary>
        /// Thêm mới quyết toán(một dòng主quyết toán nhiều dòngchi tiết)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orgId"></param>
        /// <param name="sfrq"></param>
        /// <param name="cfList"></param>
        /// <param name="xmList"></param>
        /// <param name="mzghEntity"></param>
        /// <param name="feeRelated">Số tiền và thanh toán thông tin</param>
        /// <param name="isQfyj">Có hay không là quyết toán trư ớc khi nợ phí </param>
        /// <param name="fph"></param>
        /// <param name="autoGenePlan">Có hay không tự đ ộng tạo ngoại trú kế hoạch </param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="outTradeNo"></param>
        /// <returns>Quay lạiMã nội bộ quyết toán</returns>
        private int AddSettlement(Infrastructure.EF.EFDbTransaction db, string orgId, DateTime? sfrq
            , IList<OutpatientPrescriptionEntity> cfList
            , IList<OutpatientItemEntity> xmList
            , OutpatientRegistEntity mzghEntity
            //Số tiền liên quan 
            , OutpatientSettFeeRelatedDTO feeRelated
            //BHYTsố tiền liên quan 
            , CQMzjs05Dto ybfeeRelated
            , S25ResponseDTO xnhybfeeRelated
            , bool isQfyj = false
            , string fph = null
            , bool autoGenePlan = false
            , string outTradeNo = null)
        {
            var fcgjs = false; // dấu hiệu rất 规quyết toán(thông thư ờng:Chọn phư ơng thức thanh toán, số tiền số hóa đ ơnV.v.)
            if (feeRelated == null)
            {
                fcgjs = true;
                feeRelated = new OutpatientSettFeeRelatedDTO();
            }

            var jszbEntity = new OutpatientSettlementEntity()
            {
                jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js"),
                OrganizeId = orgId,
                patid = mzghEntity.patid,
                brxz = mzghEntity.brxz,
                jslx = "2", //Ngoại trú结账
                fph = fph,
                jszt = (int)EnumJieSuanZT.YJ,
                ghnm = mzghEntity.ghnm,
                xm = mzghEntity.xm,
                xb = mzghEntity.xb,
                csny = mzghEntity.csny,
                blh = mzghEntity.blh,
                zjh = mzghEntity.zjh,
                zjlx = mzghEntity.zjlx,
                jzsj = sfrq,
                jmbl = feeRelated.zkbl ?? 0,
                jmje = feeRelated.zkje ?? 0,
                xjzf = feeRelated.xjzfys ?? 0,
                //Ghi sổ một phần V.v.
                ysk = feeRelated.ssk,
                zl = feeRelated.zhaoling,
                isQfyj = isQfyj ? (bool?)true : null,
                OutTradeNo = outTradeNo,
                ybjslsh=feeRelated.ybjslsh
            };
            jszbEntity.Create();
            db.Insert(jszbEntity);

            UpdateCurrentFinancialInvoice(db, orgId, fph);

            // này lần thêm mới Củajsmx
            var newJsmxEntityList = new List<OutpatientSettlementDetailEntity>();
            // liên quan của hạng mục ngoại trú danh sách
            var mzxmEntityList = new List<OutpatientItemEntity>();

            //Đ ơn thuốc
            foreach (var cf in cfList)
            {
                switch (cf.cflx)
                {
                    case (int)EnumPrescriptionType.Medicine:
                        {
                            if (!string.IsNullOrWhiteSpace(cf.lyyf))
                            {
                                //// thuật toán quyết toán giờ trực tiếp tạo领药窗口, 领药 thứ tự số 
                                //cf.lyck = "2"; //领药窗口
                                //cf.lyxh = 13; //领药 thứ tự số 
                            }
                            var mxList = db.IQueryable<OutpatientPrescriptionDetailEntity>(p => p.cfnm == cf.cfnm && p.zt == "1").ToList();
                            foreach (var item in mxList)
                            {
                                //Thêm mớimz_jsmx
                                var jsmxEntity = new OutpatientSettlementDetailEntity()
                                {
                                    jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx"),
                                    cf_mxnm = item.cfmxId,
                                    sl = item.sl,
                                    jyje = item.je,
                                };
                                jsmxEntity.Create();
                                db.Insert(jsmxEntity);

                                newJsmxEntityList.Add(jsmxEntity);
                            }
                            var mxxmList = db.IQueryable<OutpatientItemEntity>(p => p.cfnm == cf.cfnm && p.zt == "1").ToList();
                            if (mxxmList.Count>0)
                            {
                                mzxmEntityList.AddRange(mxxmList);  //
                                foreach (var item in mxxmList)
                                {
                                    //Thêm mớimz_jsmx
                                    var jsmxEntity = new OutpatientSettlementDetailEntity()
                                    {
                                        jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx"),
                                        mxnm = item.xmnm,
                                        sl = item.sl,
                                        jyje = item.je,
                                    };
                                    jsmxEntity.Create();
                                    db.Insert(jsmxEntity);

                                    newJsmxEntityList.Add(jsmxEntity);

                                    //Cập nhậtmz_xm Đ ã quyết toán
                                    item.xmzt = "1";  //Đ ã quyết toán
                                    item.jsrq = DateTime.Now; //Quyết toán thời gian khi trư ớc thời gian
                                    if (sfrq.HasValue)
                                    {
                                        item.sfrq = sfrq;   //强制cập nhậtthu phí ngày 
                                    }
                                    item.Modify();
                                    db.Update(item);
                                }
                            }

                            break;
                        }
                    case (int)EnumPrescriptionType.Treament:
                        {
                            var mxList = db.IQueryable<OutpatientItemEntity>(p => p.cfnm == cf.cfnm && p.zt == "1").ToList();
                            mzxmEntityList.AddRange(mxList);  //
                            foreach (var item in mxList)
                            {
                                //Thêm mớimz_jsmx
                                var jsmxEntity = new OutpatientSettlementDetailEntity()
                                {
                                    jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx"),
                                    mxnm = item.xmnm,
                                    sl = item.sl,
                                    jyje = item.je,
                                };
                                jsmxEntity.Create();
                                db.Insert(jsmxEntity);

                                newJsmxEntityList.Add(jsmxEntity);

                                //Cập nhậtmz_xm Đ ã quyết toán
                                item.xmzt = "1";  //Đ ã quyết toán
                                item.jsrq = DateTime.Now; //Quyết toán thời gian khi trư ớc thời gian
                                if (sfrq.HasValue)
                                {
                                    item.sfrq = sfrq;   //强制cập nhậtthu phí ngày 
                                }
                                item.Modify();
                                db.Update(item);
                            }

                            break;
                        }
                    default:
                        //Loại đ ơn thuốc bất thư ờng không phải phạm vi trong 
                        throw new FailedException("PARAMS_ERROR_CFLX", "处方类型参数错误");
                }

                //Cập nhậtmz_cf Đ ã quyết toán
                cf.cfzt = "1";  //Đ ã quyết toán
                cf.jsrq = DateTime.Now; //Quyết toán thời gian khi trư ớc thời gian
                if (sfrq.HasValue)
                {
                    cf.sfrq = sfrq;   //强制cập nhậtthu phí ngày 
                }
                cf.Modify();
                db.Update(cf);
            }

            // không liên kết Đ ơn thuốc của một phần
            mzxmEntityList.AddRange(xmList);  //
            foreach (var item in xmList)
            {
                if (newJsmxEntityList.Any(p => p.mxnm == item.xmnm))
                {
                    continue;   // này lỗi gọi có thể避免
                }
                //Thêm mớimz_jsmx
                var jsmxEntity = new OutpatientSettlementDetailEntity()
                {
                    jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx"),
                    mxnm = item.xmnm,
                    sl = item.sl,
                    jyje = item.je,
                };
                jsmxEntity.Create();
                db.Insert(jsmxEntity);

                newJsmxEntityList.Add(jsmxEntity);

                //Cập nhậtmz_xm Đ ã quyết toán
                item.xmzt = "1";  //Đ ã quyết toán
                item.jsrq = DateTime.Now; //Quyết toán thời gian khi trư ớc thời gian
                if (sfrq.HasValue)
                {
                    item.sfrq = sfrq;   //强制cập nhậtthu phí ngày 
                }
                item.Modify();
                db.Update(item);
            }

            //收尾
            foreach (var item in newJsmxEntityList)
            {
                item.OrganizeId = orgId;
                item.jsnm = jszbEntity.jsnm;
                item.jslx = "2";
            }

            var mxzje = newJsmxEntityList.Sum(p => p.jyje ?? 0);
            if (!fcgjs && mxzje != feeRelated.zje)
            {
                throw new Exception("结算总金额异常" + mxzje.ToString() + "不等于" + feeRelated.zje.ToString());
            }
            jszbEntity.zt = "1";
            jszbEntity.zje = mxzje;

            if (feeRelated.orglxjzfys.HasValue && feeRelated.xjzfys.HasValue)
            {
                // cần 减去ghi sổ một phần V.v.Số tiền
                if ((feeRelated.orglxjzfys.Value != jszbEntity.zje - 0)
                    && (ybfeeRelated == null || ybfeeRelated.XJZF != feeRelated.orglxjzfys.Value)
                    && (ybfeeRelated != null && ybfeeRelated.ybdlx == "changshu"))
                {
                    throw new FailedException("ERROR_SETT_JE_ERROR", "结算金额异常");
                }
                //orglxjzfys xjzfys zkbl zkje cần 保持 nhất quán 性
                //var zkje =  Math.Abs(feeRelated.zkje ?? 0);
                //var cs = Math.Abs(zkje + feeRelated.xjzfys.Value - feeRelated.orglxjzfys.Value);
                //if (Math.Abs(zkje + feeRelated.xjzfys.Value - feeRelated.orglxjzfys.Value) > (decimal)0.01)
                //{
                // throw new FailedException("ERROR_ZKJE_ZFJE_ERROR", "折扣số tiền bất thư ờng");
                //}
            }
            else
            {
                //???xjzf có必 cần gán giá trị 么
                // cần 减去ghi sổ một phần V.v.Số tiền
                jszbEntity.xjzf = jszbEntity.zje - 0;
            }

            if (feeRelated.ssk.HasValue && feeRelated.zhaoling.HasValue)
            {
                jszbEntity.xjwc = (decimal)feeRelated.xjwc;
                //Sai lệch tiền mặt nhận đ ược của-phải thu
                //jszbEntity.xjwc = jszbEntity.xjzf - (feeRelated.ssk.Value - feeRelated.zhaoling.Value);
                //if (Math.Abs(jszbEntity.xjwc) >= (decimal)0.1)
                //{
                // throw new FailedException("ERROR_SSK_ZHAOLING", " thực thu trả lại tiền thừa số tiền bất thư ờng");
                //}
            }

            PaymentModelAccountReserveII(db, jszbEntity, feeRelated);

            //Phư ơng thức thanh toán quyết toán ngoại trú

            if (autoGenePlan)
            {
                GenePlan(db, orgId, mzxmEntityList, mzghEntity);
            }

            return jszbEntity.jsnm;
        }


        public void UpdateYPcfxm(List<OutGridInfoDto2018> deletelist,string orgId,string usercode)
        {
            for (int i = 0; i < deletelist.Count; i++)
            {
                var sql = @"update [NewtouchHIS_Sett].[dbo].[mz_xm]  set zt='0',LastModifyTime=GETDATE(),LastModifierCode=@LastModifierCode  
where cfnm=@cfnm and OrganizeId=@orgId and ztId=@ztid 

update [NewtouchHIS_Sett].[dbo].[mz_cf]   set zje=(zje-convert(int,0)),LastModifyTime=GETDATE(),LastModifierCode=@LastModifierCode 
where cfnm=@cfnm   and OrganizeId=@orgId and cflx=@cflx and zt='1' 

update [Newtouch_CIS].[dbo].[xt_cf]  set zje=(zje-convert(int,0)),LastModifyTime=GETDATE(),LastModifierCode=@LastModifierCode 
where cfh=@cfh   and OrganizeId=@orgId and cflx=@cflx and zt='1'  

update [Newtouch_CIS].[dbo].[xt_cfmx]  set  zt='0',LastModifyTime=GETDATE(),LastModifierCode=@LastModifierCode  
where cfId=(select top 1 cfId from [Newtouch_CIS].[dbo].[xt_cf] with(nolock) 
where cfh=@cfh   and OrganizeId=@orgId and cflx=@cflx and zt='1' ) and OrganizeId=@orgId and ztId=@ztid "; 


                ExecuteSqlCommand(sql,
                    new[] { new SqlParameter("@orgId",orgId),
                new SqlParameter("@LastModifierCode",usercode),
                new SqlParameter("@cflx",deletelist[i].cflx),
                new SqlParameter("@cfh",deletelist[i].cfh),
                new SqlParameter("@cfnm",deletelist[i].cfnm),
                new SqlParameter("@ztid",deletelist[i].sfxmCode) });
            }
        }

        /// <summary>
        /// Là hạng mụcList tạo kế hoạch 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orgId"></param>
        /// <param name="xmList"></param>
        /// <param name="mzghEntity"></param>
        private void GenePlan(Infrastructure.EF.EFDbTransaction db, string orgId
            , IList<OutpatientItemEntity> xmList
            , OutpatientRegistEntity mzghEntity)
        {
            var jzjhEntity = new OutpatientAccountEntity()
            {
                jzjhId = Guid.NewGuid().ToString(),
                OrganizeId = orgId,
                patid = mzghEntity.patid.ToString(),
                ghnm = mzghEntity.ghnm,
            };

            bool mxAdded = false;

            foreach (var item in xmList)
            {
                if (!item.dczll.HasValue || !item.zxcs.HasValue || ((int)(item.sl / item.zxcs) == 0))
                {
                    continue;
                }
                var jzjhmxEntity = new OutpatientAccountDetailEntity
                {
                    jzjhmxId = Guid.NewGuid().ToString(),
                    jzjhId = jzjhEntity.jzjhId,
                    OrganizeId = orgId,
                    //zlsc = item.duration ?? 0,
                    sfxmCode = item.sfxm,
                    sl = (int)(item.sl / item.zxcs),  //?
                    jzsj = item.sfrq,
                    bz = item.bz,
                    ys = item.ys,
                    ks = item.ks,
                    ysmc = item.ysmc,
                    ksmc = item.ksmc,
                    //kflb = item.kflb,
                    yzxz = "2", //2 dài hạn 
                    StartDate = Constants.MinDate,
                    EndDate = Constants.MaxDate,
                    zxzt = (int)EnumJzjhZXZT.None,
                    zll = item.dczll,
                    zcs = item.zxcs
                };
                jzjhmxEntity.Create();
                db.Insert(jzjhmxEntity);

                item.jzjhmxId = jzjhmxEntity.jzjhmxId;
                item.ssbz = "9";    //Trực tiếp进入 thực hiện quá trình trong như vậy không có thể hoàn phí
                //item.Modify();
                //db.Update(item);    //Cập nhậtmz_xm   
                //Nên nhiều 余của phía trư ớc 肯定 sẽ cập nhật thiết bị jszt jsrqV.v.

                mxAdded = true;
            }

            if (!mxAdded)
            {
                //return;
            }

            jzjhEntity.Create();
            db.Insert(jzjhEntity);
        }

        /// <summary>
        /// Cập nh ậtĐ ơn giá số tiền V.v.
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jfypEntity"></param>
        private void UpdateEntity(string orgId, OutpatientPrescriptionDetailEntity jfypEntity)
        {
            SysMedicineVEntity ypEntity = _sysMedicinRepo.GetEntityByCode(orgId, jfypEntity.yp);
            if (jfypEntity.sl <= 0)
            {
                throw new FailedException("SL_IS_REQUIRED", "处方药品异常缺少数量");
            }
            //zfxz zfbl 以 này là准
            jfypEntity.zfxz = ypEntity.zfxz;
            jfypEntity.zfbl = ypEntity.zfbl;
            if (string.IsNullOrWhiteSpace(jfypEntity.dl))
            {
                jfypEntity.dl = ypEntity.dlCode;
            }
            if (string.IsNullOrWhiteSpace(jfypEntity.dw))
            {
                //Là ngoại trú拆零Đ ơn vị
                jfypEntity.dw = ypEntity.mzcldw;
            }
            if (jfypEntity.jl > 0 && string.IsNullOrWhiteSpace(jfypEntity.jldw))
            {
                jfypEntity.jldw = ypEntity.jldw;    // liều lư ợng Đ ơn vị
            }
            //Đ ơn giá có thể là tự 定义 của
            if (jfypEntity.dj < 0)  //20180910由 <=0 变là <0
            {
                //Đ ơn vị Đ ơn giá 换 tính 
                if (jfypEntity.dw == ypEntity.bzdw)
                {
                    jfypEntity.dj = ypEntity.lsj;
                }
                else if (jfypEntity.dw == ypEntity.mzcldw)
                {

                    jfypEntity.dj = (ypEntity.lsj / ypEntity.bzs) * ypEntity.mzcls.Value;
                }
                else
                {
                    // đ ó chỉ có thể mặc đ ịnh dùng ngoại trú của 
                    jfypEntity.dw = ypEntity.mzcldw;
                    jfypEntity.dj = (ypEntity.lsj / ypEntity.bzs) * ypEntity.mzcls.Value;
                }
            }
            if (jfypEntity.je <= 0)
            {
                jfypEntity.je = jfypEntity.dj * jfypEntity.sl;
            }
            jfypEntity.je = Math.Round(jfypEntity.je, 2, MidpointRounding.AwayFromZero);// bốn 舍 năm 入
        }

        /// <summary>
        /// theo điều trị lư ợng cập nhật计价số lư ợng Đ ơn giá số tiền V.v.
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jfxmEntity"></param>
        private void UpdateEntity(string orgId, OutpatientItemEntity jfxmEntity)
        {
            var sfxmEntity = _sysChargeItemRepo.SelectSysChargeItemBysfxm(jfxmEntity.sfxm, orgId);
            //zfxz zfbl 以 này là准
            jfxmEntity.zfxz = sfxmEntity.zfxz;
            jfxmEntity.zfbl = sfxmEntity.zfbl;
            if (jfxmEntity.sl <= 0)
            {
                if ((jfxmEntity.dczll ?? 0) > 0 && (jfxmEntity.zxcs ?? 0) > 0)
                {
                    jfxmEntity.sl = CommmHelper.CalcSfxmSl(jfxmEntity.dczll, sfxmEntity.dwjls, sfxmEntity.jjcl) * jfxmEntity.zxcs.Value;
                }
                else
                {
                    throw new FailedException("SL_OR_DCZLL_ZXCS_IS_REQUIRED", "项目异常缺少数量");
                }
            }

            //Đ ơn giá có thể là tự 定义 của
            if (jfxmEntity.dj < 0)  //20180910由 <=0 变là <0
            {
                jfxmEntity.dj = sfxmEntity.dj;
            }
            if (string.IsNullOrWhiteSpace(jfxmEntity.dl))
            {
                jfxmEntity.dl = sfxmEntity.sfdlCode;
            }
            if (string.IsNullOrWhiteSpace(jfxmEntity.dw))
            {
                jfxmEntity.dw = sfxmEntity.dw;
            }
            if (jfxmEntity.je <= 0)
            {
                jfxmEntity.je = jfxmEntity.dj * jfxmEntity.sl;
            }
            if ((jfxmEntity.dczll ?? 0) > 0 && (jfxmEntity.zxcs ?? 0) > 0 && (jfxmEntity.zzll ?? 0) <= 0)
            {
                jfxmEntity.zzll = jfxmEntity.dczll.Value * jfxmEntity.zxcs.Value;
            }
        }

        /// <summary>
        /// Quyết toán phư ơng thức thanh toán, thanh toán tạm ứng thu chi tài khoản
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jszbEntity"></param>
        /// <param name="feeRelated"></param>
        private void PaymentModelAccountReserve(Infrastructure.EF.EFDbTransaction db, OutpatientSettlementEntity jszbEntity
            , OutpatientSettFeeRelatedDTO feeRelated)
        {
            if (jszbEntity.isQfyj == true)
            {
                return; // quyết toán trư ớc khi nợ phí return
            }
            OutpatientSettlementPaymentModelEntity zfEntity1 = null;
            OutpatientSettlementPaymentModelEntity zfEntity2 = null;

            if (!string.IsNullOrWhiteSpace(feeRelated.zffs1) && (feeRelated.zfje1 ?? 0) > 0)
            {
                zfEntity1 = new OutpatientSettlementPaymentModelEntity();
                zfEntity1.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                zfEntity1.OrganizeId = jszbEntity.OrganizeId;
                zfEntity1.jsnm = jszbEntity.jsnm;
                zfEntity1.xjzffs = feeRelated.zffs1;
                zfEntity1.zfje = feeRelated.zfje1.Value;
                zfEntity1.zt = "1";
                zfEntity1.Create();
                db.Insert(zfEntity1);
            }
            if (!string.IsNullOrWhiteSpace(feeRelated.zffs2) && (feeRelated.zfje2 ?? 0) > 0)
            {
                zfEntity2 = new OutpatientSettlementPaymentModelEntity();
                zfEntity2.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                zfEntity2.OrganizeId = jszbEntity.OrganizeId;
                zfEntity2.jsnm = jszbEntity.jsnm;
                zfEntity2.xjzffs = feeRelated.zffs2;
                zfEntity2.zfje = feeRelated.zfje2.Value;
                zfEntity2.zt = "1";
                zfEntity2.Create();
                zfEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                db.Insert(zfEntity2);
            }

            //Nếu只có một loại phư ơng thức thanh toán tồn tại 入js.xjzffs
            //2019.06.06 huangshanshan ngoại trú kế hoạch 录入giờ, báo lỗi 修复
            jszbEntity.xjzffs = zfEntity2 == null ? (zfEntity1 == null ? null : zfEntity1.xjzffs) : null;

            //Thanh toán tạm ứng xây dựng thu chi tài khoản //Thanh toán tạm ứng nhất đ ịnh 作là thứ một phư ơng thức thanh toán 
            if (zfEntity1 != null && zfEntity1.xjzffs == xtzffs.ZYYJZHZF)
            {
                var accountEntity = db.IQueryable<SysAccountEntity>(p => p.patid == jszbEntity.patid).FirstOrDefault();
                if (accountEntity != null)
                {
                    zfEntity1.zh = accountEntity.zhCode;
                    if (!(zfEntity1.zfje == accountEntity.zhye
                        || (zfEntity1.zfje <= feeRelated.xjzfys.Value && zfEntity1.zfje <= accountEntity.zhye)))
                    {
                        throw new FailedException("", "预交账户支付金额异常");
                    }
                    var zhszEntity = new SysAccountRevenueAndExpenseEntity()
                    {
                        OrganizeId = jszbEntity.OrganizeId,
                        zhCode = accountEntity.zhCode,
                        patid = accountEntity.patid,
                        szje = 0 - Math.Min(zfEntity1.zfje, feeRelated.xjzfys.Value),
                        zhye = accountEntity.zhye - Math.Min(zfEntity1.zfje, feeRelated.xjzfys.Value),
                        pzh = null,
                        szxz = (int)EnumSZXZ.mzjs,
                        xjzffs = Constants.xtzffs.ZYYJZHZF,
                        jsnm = jszbEntity.jsnm,
                        zt = "1",
                    };
                    zhszEntity.Create(true);
                    db.Insert(zhszEntity);
                    //
                    accountEntity.zhye = zhszEntity.zhye;
                    db.Update(accountEntity);
                    //Thanh toán tạm ứng Thu chi tài khoản
                    if (zfEntity1.zfje > feeRelated.xjzfys.Value)
                    {
                        //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                        var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                        {
                            OrganizeId = jszbEntity.OrganizeId,
                            zhCode = accountEntity.zhCode,
                            patid = accountEntity.patid,
                            szje = 0 - (zfEntity1.zfje - feeRelated.xjzfys.Value),
                            zhye = accountEntity.zhye - (zfEntity1.zfje - feeRelated.xjzfys.Value),
                            pzh = null,
                            szxz = (int)EnumSZXZ.qk,
                            //xjzffs = Constants.xtzffs.ZYYJZHZF,
                            xjzffs = "0",   //Tiền mặt
                            zt = "1",
                        };
                        zhszEntity2.Create(true);
                        zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                        db.Insert(zhszEntity2);
                        //
                        accountEntity.zhye = zhszEntity2.zhye;
                        //db.Update(accountEntity);
                    }
                }
                else
                {
                    throw new FailedException("ERROR_ACCOUNT_INFO", "获取预交金账户信息失败");
                }
            }
        }
        private void PaymentModelAccountReserveII(Infrastructure.EF.EFDbTransaction db, OutpatientSettlementEntity jszbEntity
           , OutpatientSettFeeRelatedDTO feeRelated)
        {
            if (jszbEntity.isQfyj == true)
            {
                return; // quyết toán trư ớc khi nợ phí return
            }
            OutpatientSettlementPaymentModelEntity mzzfEntity1 = null;
            OutpatientSettlementPaymentModelEntity mzzfEntity2 = null;
            if (!string.IsNullOrWhiteSpace(feeRelated.yjjzfje.ToString()) &&(feeRelated.yjjzfje ?? 0) > 0)
            {
                mzzfEntity1 = new OutpatientSettlementPaymentModelEntity();
                mzzfEntity1.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                mzzfEntity1.OrganizeId = jszbEntity.OrganizeId;
                mzzfEntity1.jsnm = jszbEntity.jsnm;
                mzzfEntity1.xjzffs = xtzffs.ZYYJZHZF;
                mzzfEntity1.zfje = feeRelated.yjjzfje.Value;
                mzzfEntity1.zt = "1";
                mzzfEntity1.Create();
                db.Insert(mzzfEntity1);
                jszbEntity.xjzffs = xtzffs.ZYYJZHZF;
            }
            if (feeRelated.patZflist.Count() > 0)
            {
                foreach (var item in feeRelated.patZflist)
                {
                    mzzfEntity2 = new OutpatientSettlementPaymentModelEntity();
                    mzzfEntity2.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                    mzzfEntity2.OrganizeId = jszbEntity.OrganizeId;
                    mzzfEntity2.jsnm = jszbEntity.jsnm;
                    mzzfEntity2.xjzffs = item.zffsmc;
                    mzzfEntity2.zfje = (decimal)item.zfje;
                    mzzfEntity2.zt = "1";
                    mzzfEntity2.Create();
                    mzzfEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                    db.Insert(mzzfEntity2);
                }
                jszbEntity.xjzffs = feeRelated.djjesszffs;
            }


            //Thanh toán tạm ứng xây dựng thu chi tài khoản //Thanh toán tạm ứng nhất đ ịnh 作là thứ một phư ơng thức thanh toán 
            if (mzzfEntity1 != null && mzzfEntity1.xjzffs == xtzffs.ZYYJZHZF)
            {
                decimal tye = 0;
                if (feeRelated.yjjtye.Value > 0)
                {
                    tye = feeRelated.yjjtye.Value;
                }
                var accountEntity = db.IQueryable<SysAccountEntity>(p => p.patid == jszbEntity.patid).FirstOrDefault();
                if (accountEntity != null)
                {
                    mzzfEntity1.zh = accountEntity.zhCode;
                    if (!((mzzfEntity1.zfje + tye) == accountEntity.zhye
                        || (mzzfEntity1.zfje <= feeRelated.xjzfys.Value && (mzzfEntity1.zfje + tye) <= accountEntity.zhye)))
                    {
                        throw new FailedException("", "预交账户支付金额异常");
                    }
                    var zhszEntity = new SysAccountRevenueAndExpenseEntity()
                    {
                        OrganizeId = jszbEntity.OrganizeId,
                        zhCode = accountEntity.zhCode,
                        patid = accountEntity.patid,
                        //szje = 0 - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        //zhye = accountEntity.zhye - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        szje = 0 - mzzfEntity1.zfje,
                        zhye = accountEntity.zhye - mzzfEntity1.zfje,
                        pzh = null,
                        szxz = (int)EnumSZXZ.mzjs,
                        xjzffs = Constants.xtzffs.ZYYJZHZF,
                        jsnm = jszbEntity.jsnm,
                        zt = "1",
                    };
                    zhszEntity.Create(true);
                    db.Insert(zhszEntity);
                    //
                    accountEntity.zhye = zhszEntity.zhye;
                    db.Update(accountEntity);
                    //Thanh toán tạm ứng Thu chi tài khoản
                    if (feeRelated.yjjtye.Value > 0 && accountEntity.zhye == feeRelated.yjjtye.Value)
                    {
                        //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                        var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                        {
                            OrganizeId = jszbEntity.OrganizeId,
                            zhCode = accountEntity.zhCode,
                            patid = accountEntity.patid,
                            szje = 0 - feeRelated.yjjtye.Value,
                            zhye = accountEntity.zhye - feeRelated.yjjtye.Value,
                            pzh = null,
                            szxz = (int)EnumSZXZ.qk,
                            //xjzffs = Constants.xtzffs.ZYYJZHZF,
                            xjzffs = Constants.xtzffs.XJZF,   //Tiền mặt
                            zt = "1",
                        };
                        zhszEntity2.Create(true);
                        zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                        db.Insert(zhszEntity2);
                        //
                        accountEntity.zhye = zhszEntity2.zhye;
                    }
                    else if (feeRelated.yjjtye.Value > 0)
                    {
                        throw new FailedException("ERROR_ACCOUNT_INFO", "预交账户结算成功，退余额失败");
                    }
                    //if (mzzfEntity1.zfje > feeRelated.xjzfys.Value)
                    //{
                    //    //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                    //    var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                    //    {
                    //        OrganizeId = jszbEntity.OrganizeId,
                    //        zhCode = accountEntity.zhCode,
                    //        patid = accountEntity.patid,
                    //        szje = 0 - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                    //        zhye = accountEntity.zhye - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                    //        pzh = null,
                    //        szxz = (int)EnumSZXZ.qk,
                    //        //xjzffs = Constants.xtzffs.ZYYJZHZF,
                    //        xjzffs = "0",   //Tiền mặt
                    //        zt = "1",
                    //    };
                    //    zhszEntity2.Create(true);
                    //    zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                    //    db.Insert(zhszEntity2);
                    //    //
                    //    accountEntity.zhye = zhszEntity2.zhye;
                    //    //db.Update(accountEntity);
                    //}
                    db.Update(accountEntity);
                }
                else
                {
                    throw new FailedException("ERROR_ACCOUNT_INFO", "获取预交金账户信息失败");
                }
            }
        }

        /// <summary>
        /// Cập nhậtkhi trư ớc số hóa đ ơn
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orgId"></param>
        /// <param name="fph"></param>
        private void UpdateCurrentFinancialInvoice(Infrastructure.EF.EFDbTransaction db, string orgId, string fph)
        {
            if (!string.IsNullOrWhiteSpace(fph))
            {
                // có thể dùng hóa đ ơn cập nhật
                //Chèn/Cập nhậtcw_fp
                var opr = Newtouch.Common.Operator.OperatorProvider.GetCurrent();
                if (opr != null)
                {
                    FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
                    _financialInvoiceRepo.UpdateCurrentGetEntitys(fph, opr.UserCode, out fpUpdateEntity, out fpInsertEntity, orgId);
                    if (fpUpdateEntity != null)
                    {
                        db.Update(fpUpdateEntity);
                    }
                    if (fpInsertEntity != null)
                    {
                        db.Insert(fpInsertEntity);
                    }
                }
                else
                {
                    throw new FailedException("FINANCIALINVOICE_UPDATE_ERROR", "发票号更新异常");
                }
            }
        }

        #endregion

        /// <summary>
        /// Lấy quyết toán ngoại trú/Hoàn phí bệnh nhân xuất viện thông tin(BHYTsố quyết toán, 社保số hiệu, khám bệnh nguyên nhân, khoa, bác sĩ, chẩn đoán)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OutPatChargePatientInfoVO GetSettPatientInfo(string mzh, string orgId)
        {
            var sql = @"select gh.ybjsh, brjbxx.sbbh, gh.jzyy, gh.zdicd10, gh.zdmc
, isnull(ysks.ys, ghysStaff.gh) ys, isnull(ysks.ks, ghDept.Code) ks, isnull(ysks.ysmc, ghysStaff.Name) ysmc, isnull(ysks.ksmc, ghDept.Name) ksmc
from mz_gh(nolock) gh
inner join xt_brjbxx(nolock) brjbxx
on brjbxx.patid = gh.patid and brjbxx.OrganizeId = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff ghysStaff
on ghysStaff.gh = gh.ys and ghysStaff.OrganizeId = @orgId and ghysStaff.zt = '1'
left join [NewtouchHIS_Base]..V_S_Sys_Department ghDept
on ghDept.Code = gh.ks and ghDept.OrganizeId = @orgId and ghDept.zt = '1'
left join 
(
select top 1 *
from
(
select ys, ks, ysmc, ksmc, CreateTime from mz_xm(nolock)
where zt = '1' and isnull(ys,'')<>'' and isnull(ks,'')<>'' and isnull(ysmc,'')<>'' and isnull(ksmc,'')<>'' and ys not LIKE '%,%'
and OrganizeId  = @orgId and patid in(
 select patid from mz_gh(nolock) where OrganizeId = @orgId and zt = '1' and mzh = @mzh
)
union
select ys, ks, ysmc, ksmc, CreateTime from mz_cf(nolock)
where zt = '1' and isnull(ys,'')<>'' and isnull(ks,'')<>'' and isnull(ysmc,'')<>'' and isnull(ksmc,'')<>'' and ys not LIKE '%,%'
and OrganizeId  = @orgId and patid in(
 select patid from mz_gh(nolock) where OrganizeId = @orgId and zt = '1' and mzh = @mzh
)
) as innerjfysks
Order by CreateTime
) ysks
on 1 = 1
where gh.mzh = @mzh and gh.zt = '1' and gh.OrganizeId = @orgId";
            return this.FirstOrDefault<OutPatChargePatientInfoVO>(sql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@mzh", mzh) });
        }

        /// <summary>
        /// Lấy ngoại trú thu phí quyết toán nhóm bộ Tư ơng ứng củachi tiết
        /// </summary>
        /// <param name="cfnms"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<RefundZtDot> RefundZtDetail(string cfnms,string orgId)
        {
            string sql = @"select jsmx.jsmxnm,ztId,dj,jsmx.sl,cfnm from mz_jsmx jsmx 
join mz_xm xm  on jsmx.mxnm=xm.xmnm  and jsmx.organizeid=xm.organizeid and xm.zt=1
where jsmx.zt=1 and xm.cfnm in  (select col from f_split(@cfnms,','))";

            return this.FindList<RefundZtDot>(sql,
                new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@cfnms", cfnms) });
        }

        public CheckApplicationfromDTO pushApplicationform(BasicInfoDto2018 bacDto, int cfnm, string orgid,string typezt)
        {
            CheckApplicationfromDTO checkApplicationfromDTO = new CheckApplicationfromDTO();
            string sql = @"select a.cfh Reqno,a.CreateTime Indate,a.ys DoctorID,a.ysmc DoctorName,gh.mzh RecordID,a.ks WardNo, a.cfh CardID,b.ztmc ApplyName,a.ks ApplyDepartmentCode,a.ksmc ApplyDepartmentName,convert(varchar(50),b.xmnm) Seqno,b.sfxm ItemID,sf.sfxmmc ItemName,convert(varchar(10),b.sl) Qty,convert(varchar(10),b.dj) Price  from [NewtouchHIS_Sett]..mz_cf a
left join [NewtouchHIS_Sett]..mz_gh gh on gh.ghnm=a.ghnm and a.organizeid=gh.organizeid
left join [NewtouchHIS_Sett]..mz_xm b on a.cfnm=b.cfnm and a.organizeid=b.organizeid
left join [NewtouchHIS_Base]..xt_sfxm sf on b.sfxm=sf.sfxmcode and b.organizeid=sf.organizeid
where a.cfnm=@cfnms and a.organizeid=@orgId  and a.cflxmc='检查处方'";

			sql = "exec MZ_pacsxx @cfnms,@orgId";
			var cfmxlist = new List<CheckfromDTO>(); ;
            try
            {
                cfmxlist = this.FindList<CheckfromDTO>(sql,
                new[] { new SqlParameter("@orgId", orgid), new SqlParameter("@cfnms", cfnm.ToString()) });
            }
            catch (Exception e)
            {

                throw;
            }
            
            if (cfmxlist.Count==0|| cfmxlist==null)
            {
                return null;
            }
            RequestHost requestHost = new RequestHost();
            requestHost.MedNo = orgid;// trư ớc truyền 入测试
            requestHost.MedName = "上海明德五官科医院";// trư ớc truyền 入测试

            Patient patient = new Patient();
            patient.Name = bacDto.xm;
            patient.Gender = bacDto.xb=="1"?"M":"F";
            patient.Birthday = bacDto.csny;
            patient.IdentityID = bacDto.zjh;
            patient.WardNo = cfmxlist[0].WardNo;
            patient.BedNo = "";
            patient.CardID = cfmxlist[0].CardID;

            Request request = new Request();
            request.Reqno = cfmxlist[0].Reqno;
            request.Indate = DateTime.Parse(cfmxlist[0].Indate.ToDateTimeString());
            request.DoctorID = cfmxlist[0].DoctorID;
            request.DoctorName = cfmxlist[0].DoctorName;
            request.DoctorIdCard = "";
            request.RecordID = cfmxlist[0].RecordID;
            request.Source = "MagQ700001";
            request.Pic = "";
            request.PicDetail = cfmxlist[0].PicDetail;// trư ớc 放 trống phía sau từ điển Tư ơng ứng
            request.ApplyName = cfmxlist[0].ApplyName;
            request.WhichNo = null;
            request.IsVaild = typezt;
            request.IsCharge = "";
            request.ChargeDate = DateTime.Now;
            request.InvoiceID = "";
            request.CancelDate = null;
            request.Comment = "";
            request.ApplyDepartmentCode = cfmxlist[0].ApplyDepartmentCode;
            request.ApplyDepartmentName = cfmxlist[0].ApplyDepartmentName;
            request.DiagnoseName = "";

            List<Order> order = new List<Order>();
            foreach (var item in cfmxlist)
            {
                Order itemorder = new Order();
                itemorder.Seqno = item.Seqno;
                itemorder.Reqno = item.Reqno;
                itemorder.ItemID = item.ItemID;// trư ớc truyền sfcode phía sau từ điển Tư ơng ứng
                itemorder.ItemName = item.ItemName;
                itemorder.BodyPart = "";
                itemorder.Qty = item.Qty;
                itemorder.Price = item.Price;
                itemorder.SN = "";
                order.Add(itemorder);
            }
            request.Order = order;
            patient.Request = request;
            checkApplicationfromDTO.Patient = patient;
            checkApplicationfromDTO.RequestHost = requestHost;
            return checkApplicationfromDTO;
        }

        public CheckApplicationfromDTO retApplicationform(string mzh, string cfnmList, string orgid, string typezt)
        {
            CheckApplicationfromDTO checkApplicationfromDTO = new CheckApplicationfromDTO();
            string sql = @"select gh.xm,(case when gh.xb='1'then '男' else '女'end)xb,gh.csny,gh.zjh, cf.cfh Reqno,cf.CreateTime Indate,cf.ys DoctorID,cf.ysmc DoctorName,gh.mzh RecordID,cf.ks WardNo, cf.cfh CardID,xm.ztmc ApplyName,cf.ks ApplyDepartmentCode,cf.ksmc ApplyDepartmentName,convert(varchar(50),xm.xmnm) Seqno,xm.sfxm ItemID,sf.sfxmmc ItemName,convert(varchar(10),xm.sl) Qty,convert(varchar(10),xm.dj) Price from [NewtouchHIS_Sett]..mz_cf cf 
left join [NewtouchHIS_Sett]..mz_xm xm  on cf.cfnm=xm.cfnm  and cf.organizeid=xm.organizeid and xm.zt=1
left join [NewtouchHIS_Base]..xt_sfxm sf on xm.sfxm=sf.sfxmcode and xm.organizeid=sf.organizeid
--left join [NewtouchHIS_Sett]..mz_cf cf on xm.cfnm=cf.cfnm  and cf.organizeid=xm.organizeid and cf.zt=1
left join [NewtouchHIS_Sett]..mz_gh gh on gh.ghnm=cf.ghnm and cf.organizeid=gh.organizeid and gh.zt=1
where cf.zt=1  and cf.cfh =@cfh  and gh.mzh=@mzh and cf.organizeid=@orgId and cf.cflxmc='检查处方' ";
            
            var cfmxlist = new List<CheckfromDTO>(); ;
            try
            {
                cfmxlist = this.FindList<CheckfromDTO>(sql,
                new[] { new SqlParameter("@orgId", orgid), new SqlParameter("@cfh", cfnmList), new SqlParameter("@mzh", mzh) });
            }
            catch (Exception e)
            {

                throw;
            }


            if (cfmxlist.Count == 0 || cfmxlist == null)
            {
                return null;
            }
            RequestHost requestHost = new RequestHost();
            requestHost.MedNo = "testclinic1";// trư ớc truyền 入测试
            requestHost.MedName = "正德测试1";// trư ớc truyền 入测试

            Patient patient = new Patient();
            patient.Name = cfmxlist[0].xm;
            patient.Gender = cfmxlist[0].xb;
            patient.Birthday = cfmxlist[0].csny;
            patient.IdentityID = cfmxlist[0].zjh;
            patient.WardNo = cfmxlist[0].WardNo;
            patient.BedNo = "";
            patient.CardID = cfmxlist[0].CardID;

            Request request = new Request();
            request.Reqno = cfmxlist[0].Reqno;
            request.Indate = DateTime.Parse(cfmxlist[0].Indate.ToDateTimeString()); ;
            request.DoctorID = cfmxlist[0].DoctorID;
            request.DoctorName = cfmxlist[0].DoctorName;
            request.DoctorIdCard = "";
            request.RecordID = cfmxlist[0].RecordID;
            request.Source = "MagQ700001";
            request.Pic = "";
            request.PicDetail = "";// trư ớc 放 trống phía sau từ điển Tư ơng ứng
            request.ApplyName = cfmxlist[0].ApplyName;
            request.WhichNo = null;
            request.IsVaild = typezt;
            request.IsCharge = "";
            request.ChargeDate = DateTime.Now;
            request.InvoiceID = "";
            request.CancelDate = null;
            request.Comment = "";
            request.ApplyDepartmentCode = cfmxlist[0].ApplyDepartmentCode;
            request.ApplyDepartmentName = cfmxlist[0].ApplyDepartmentName;
            request.DiagnoseName = "";

            List<Order> order = new List<Order>();
            foreach (var item in cfmxlist)
            {
                Order itemorder = new Order();
                itemorder.Seqno = item.Seqno;
                itemorder.Reqno = item.Reqno;
                itemorder.ItemID = item.ItemID;// trư ớc truyền sfcode phía sau từ điển Tư ơng ứng
                itemorder.ItemName = item.ItemName;
                itemorder.BodyPart = "";
                itemorder.Qty = item.Qty;
                itemorder.Price = item.Price;
                itemorder.SN = "";
                order.Add(itemorder);
            }
            request.Order = order;
            patient.Request = request;
            checkApplicationfromDTO.Patient = patient;
            checkApplicationfromDTO.RequestHost = requestHost;
            return checkApplicationfromDTO;
        }

        public int pushApplicationformRef(string cfh, string orgid,int sqdzt)
        {
            int refcount = 0;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var cfzbEntityList = db.IQueryable<OutpatientPrescriptionEntity>(p => p.zt == "1" && p.OrganizeId == orgid && p.cfh == cfh).ToList();
                if (cfzbEntityList.Count == 0 || cfzbEntityList == null)
                {
                    // chỉ đ ịnh CủaĐ ơn thuốc tham số có vấn đ ề đ ối với không trên 
                    //throw new FailedException("PARAMS_ERROR_CFNMLIST_OR_CFHLIST", "Tham số lỗi定 vị Đ ơn thuốc bất thư ờng");
                    return refcount;
                }
                foreach (var cfzbEntity in cfzbEntityList)
                {

                    cfzbEntity.sqdzt = sqdzt;
                    //cfzbEntity.Modify();
                    db.Update(cfzbEntity);
                }
                refcount = db.Commit();
            }
            return refcount;
        }

		/// <summary>
		/// 开立物资hạng mục扣减 kho tồn tại số lư ợng 扣减冻结số lư ợng
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		public int Sumwzdj(string[] cfh, string orgId, string rygh)
		{
			foreach (var item in cfh)
			{
                string sql = "exec Newtouch_CIS..物资扣减库存量 @cfh,@orgId,@rygh";//"exec NewtouchHIS_herp..herp_物资扣减 kho tồn tại lư ợng @cfh,@orgid,@rygh";
				var pars = new List<SqlParameter>();
				pars.Add(new SqlParameter("@cfh", item));
				pars.Add(new SqlParameter("@orgId", orgId));
				pars.Add(new SqlParameter("@rygh", rygh));
				this.ExecuteSqlCommand(sql, pars.ToArray());
			}

			return 1;
		}

		public int TuiHuiwzdj(string[] cfh, string orgId, string rygh)
		{
			foreach (var item in cfh)
			{
				string sql = "exec Newtouch_CIS..物资处方退费库存量退还 @cfh,@orgId,@rygh,NULL";
				var pars = new List<SqlParameter>();
				pars.Add(new SqlParameter("@cfh", item));
				pars.Add(new SqlParameter("@orgId", orgId));
				pars.Add(new SqlParameter("@rygh", rygh));
				this.ExecuteSqlCommand(sql, pars.ToArray());
			}

			return 1;
		}
	}
}
