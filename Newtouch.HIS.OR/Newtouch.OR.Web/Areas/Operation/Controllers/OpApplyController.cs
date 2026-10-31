using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.OR.ManageSystem.Domain;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class OpApplyController : OrgControllerBase
    {
        //private readonly ISysDepartmentRepo _SysDepartmentRepo;
        //private readonly ISysStaffRepo _SysStaffRepo;      
        private readonly IOPApplyDmnService _applyDmnService;
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORApplyInfoRepo _ORApplyInfoRepo;
        private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;
        private readonly IORStaffRepo _ORStaffRepo;
        private readonly ITemporary_ordersERepo _ITemporary_ordersERepo;
        public OpApplyController(IOPApplyDmnService applyDmnService, ICommonDmnService commonDmnService, IORApplyInfoRepo oRApplyInfoRepo, IORApplyInfoExpandRepo oRApplyInfoExpandRepo, IORStaffRepo oRStaffRepo, ITemporary_ordersERepo iTemporary_ordersERepo)
        {
            _applyDmnService = applyDmnService;
            _CommonDmnService = commonDmnService;
            _ORApplyInfoRepo = oRApplyInfoRepo;
            _ORApplyInfoExpandRepo = oRApplyInfoExpandRepo;
            _ORStaffRepo = oRStaffRepo;
            _ITemporary_ordersERepo = iTemporary_ordersERepo;
        }
        // GET: Operation/OpApply
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Danh sách đơn đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyCard()
        {
            return View();
        }
        /// <summary>
        /// Chi tiết đơn đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyOPDetail()
        {
            return View();
        }
        public IActionResult ApplyIndex()
        {
            return View();
        }
        public IActionResult OpApplyCard()
        {
            return View();
        }

        public IActionResult Getbqlist(string bqcode)
        {
            var bq = _CommonDmnService.GetBqlist(bqcode, OrganizeId, null);
            return Content(bq.ToJson());
        }

        public IActionResult GetOplist(string ssdm)
        {
            var op = _CommonDmnService.GetOplist(ssdm, OrganizeId);
            return Content(op.ToJson());
        }

        public IActionResult GetStafflist(string rygh)
        {
            var ys = _CommonDmnService.GetStafflist(rygh, OrganizeId);
            return Content(ys.ToJson());
        }
        public IActionResult GetAneslist(string code)
        {
            var ys = _CommonDmnService.GetAneslistlist(code, OrganizeId);
            return Content(ys.ToJson());
        }

        public IActionResult GetPatGridList(Pagination pagination, string keyword, string bq)
        {
            var pat = _applyDmnService.GetPatList(keyword, null, bq, this.UserIdentity.rygh, OrganizeId, 1);

            var data = new
            {
                rows = pat,
                total = pat.Count,
                page = 1,
                records = pat.Count
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public IActionResult GetPatGridList(PagingData<QueryBaseDTO> paging)
        {
            #region Dữ liệu kiểm tra
            //object[] pat = new[]
            //{ new{
            //     ID = 1,
            //    hzxm = "Nguyễn Văn A",
            //    zyh = "001",
            //    ryzd = "Viêm ruột thừa",
            //    rysj="2019-10-1",
            //    sqzt="Chưa đăng ký"
            //    },
            //   new{
            //     ID = 2,
            //    hzxm = "Nguyễn Văn B",
            //    zyh = "002",
            //    ryzd = "Xơ gan",
            //    rysj="2019-10-5",
            //    sqzt="Đã duyệt"
            //    }
            //};
            #endregion

            // FIX gstack T-03 (F-02): goi tran thieu queryParams truoc day NRE.
            // UI that luon gui DTO nen guard nay chi chong NRE, khong doi
            // hanh vi UI (DTO rong -> loc rong nhu khi UI gui rong).
            var qp = paging?.queryParams ?? new QueryBaseDTO();
            var pat = _applyDmnService.GetPatList(paging?.search ?? qp.keyword, null, qp.bq, this.UserIdentity.rygh, OrganizeId, 1);

            var data = new
            {
                rows = pat,
                total = pat.Count,
                //page = pagination.page,
                //records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public IActionResult GetPatInfo(QueryBaseDTO request)
        {
            var pat = _applyDmnService.GetPatList(request.keyword, request.zyh, request.bq, this.UserIdentity.rygh, OrganizeId, 1);
            return Content(pat.Count > 0 ? pat[0].ToJson() : "");
        }

        public IActionResult GetGridListOP(string zyh, string sqlx, Pagination pagination)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Content("");
            }
            var oplist = _applyDmnService.GetOpApplybyzyh(zyh, sqlx, OrganizeId);



            var data = new
            {
                rows = oplist,
                page = 1,
                records = 1,
                total = oplist.Count
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public IActionResult GetGridListOP(PagingData<QueryBaseDTO> reqdata)
        {
            #region Dữ liệu kiểm tra
            //object[] oplist = new[]
            //{ new{
            //     ID = 1,
            //    ssmc = "Cắt ruột thừa1",
            //    sssj = "2019-10-1 10:00",
            //    zdys = "Bác sĩ 1",
            //    sszt="Đã duyệt"
            //    },
            //   new{
            //    ID = 2,
            //    ssmc = "Cắt ruột thừa2",
            //    sssj = "2019-10-1 15:30",
            //    zdys = "Bác sĩ 1",
            //    sszt="Chờ duyệt"
            //    }
            //};
            #endregion

            // FIX gstack T-03 (F-01): goi tran thieu queryParams truoc day NRE.
            // Giu dung quy uoc cua overload (string): zyh trong -> tra rong.
            if (string.IsNullOrWhiteSpace(reqdata?.queryParams?.zyh))
            {
                return Content("");
            }
            var oplist = _applyDmnService.GetOpApplybyzyh(reqdata.queryParams.zyh, reqdata.queryParams.sqlx, OrganizeId);



            var data = new
            {
                rows = oplist,
                total = oplist.Count
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy thực thể chi tiết đơn đăng ký
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Content(new OpApplySubmitDTO().ToJson());
            }
            var entity = _applyDmnService.GetOpApplybyApplyNo(keyValue, OrganizeId);
            return Content(entity.ToJson());
        }
        public IActionResult SubmitForm(OpApplySubmitDTO entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.zyh))
            {
                throw new FailedException("Thông tin bệnh nhân không được để trống");
            }
            entity.staffgh = this.UserIdentity.rygh;
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            //Gửi thông tin đơn đăng ký
            string applyno = _applyDmnService.SubmitApplyForm(entity, keyValue);
            return Success("Gửi đơn thành công", new { applyno });
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm1(OpApplyDTO entity, string keyValue, string datas)
        {
            if (entity != null)
            {
                //Gửi thông tin đơn đăng ký
                string applyno = _applyDmnService.SubmitApplyForm(entity, keyValue, datas);

                //Xóa phẫu thuật phụ trợ
                if (keyValue != "")
                {
                    DeleteApplyExtend(keyValue);
                }

                ////Thêm phẫu thuật phụ trợ
                for (var i = 0; i < entity.ss.Count; i++)
                {
                    var ssObj = new ORApplyInfoExpandEntity();
                    ssObj.OrganizeId = entity.organizeid;
                    ssObj.Applyno = applyno;
                    ssObj.zyh = entity.sq_zyh;
                    ssObj.ssmc = entity.ss[i].ssmc;
                    ssObj.ssdm = entity.ss[i].ssdm;
                    ssObj.px = i + 1;
                    _ORApplyInfoExpandRepo.SubmitForm(ssObj, null);
                }

                return Success($"Gửi đơn thành công，Số đơn đăng ký phẫu thuật:{applyno}", new { applyno });
            }

            return Error("Thông tin chưa đầy đủ，Gửi đơn thất bại.");


        }
        /// <summary>
        /// Hủy đơn đăng ký
        /// </summary>
        /// <param name="keyValue">Số đơn đăng ký phẫu thuật</param>
        /// <returns></returns>
        public IActionResult DeleteApply(string keyValue, string ssmcn)
        {
            var ety = _ORApplyInfoRepo.FindEntity(p => p.Applyno == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (ety == null)
            {
                return Error("Không tìm thấy đơn phẫu thuật hợp lệ");
            }
            ety.sqzt = ((int)EnumSqzt.yqx).ToString();
            EntityHelper.Modify(ety, ety.Id);
            _ORApplyInfoRepo.Update(ety);
            return Success("Hủy đơn thành công");
        }

        /// <summary>
        /// Xóa phẫu thuật phụ trợ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteApplyExtend(string keyValue)
        {
            _ORApplyInfoExpandRepo.DeleteForm(keyValue, OrganizeId);
            return Success();
        }
    }
}