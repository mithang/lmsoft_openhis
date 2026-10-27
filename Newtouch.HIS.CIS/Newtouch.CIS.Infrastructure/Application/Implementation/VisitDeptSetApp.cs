using System;
using Newtouch.CIS.Compat;

using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Ngoại trúrakhámCài đặt
    /// </summary>
    public class VisitDeptSetApp : AppBase, IVisitDeptSetApp
    {
        private readonly IVisitDeptSetRepo _visitDeptSetRepo;

        /// <summary>
        /// Gửibảngphiếu
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitForm(VisitDeptDetail vo, string userCode, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(vo.Id))
            {
                var entity = new VisitDeptSetEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    czlx = vo.czlx,
                    OrganizeId = organizeId,
                    ysgh = vo.ysgh,
                    visitksCode = vo.visitksCode,
                    SubordinateDepartments = vo.SubordinateDepartments,
                    CreatorCode = userCode,
                    CreateTime = DateTime.Now,
                    zt = vo.zt
                };
                return _visitDeptSetRepo.Insert(entity) > 0 ? "" : "Thêm mớingoại trúKhoathất bại";
            }

            var oldEntity = _visitDeptSetRepo.FindEntity(vo.Id);
            if (oldEntity == null || string.IsNullOrWhiteSpace(oldEntity.ysgh)) return "Lấylịch sửngoại trúKhoathông tinthất bại";
            oldEntity.SubordinateDepartments = vo.SubordinateDepartments;
            oldEntity.visitksCode = vo.visitksCode;
            oldEntity.czlx = vo.czlx;
            oldEntity.zt = vo.zt;
            oldEntity.LastModifierCode = userCode;
            oldEntity.LastModifyTime = DateTime.Now;
            return _visitDeptSetRepo.Update(oldEntity) > 0 ? "" : "sửangoại trúKhoathất bại";
        }
    }
}