using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Tools;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public class SysChargeClassifyApp : ISysChargeClassifyApp
    {
        private readonly ISysChargeClassificationRepo _SysChargeClassifyRepo;
        public SysChargeClassifyApp(ISysChargeClassificationRepo xt_sfflRepository)
        {
            this._SysChargeClassifyRepo = xt_sfflRepository;
        }

        /// <summary>
        /// Lấy tất cả các thu phí phân loại hộp thả xuống 
        /// </summary>
        /// <returns></returns>
        public object GetsfflSelect()
        {
            var list = _SysChargeClassifyRepo.IQueryable().Where(p => p.zt == "1").Select(p => new
            {
                fl = p.fl,
                flbh = p.flbh,
                flmc = p.flmc,
                py = p.py
            }).ToList();
            return list;
        }

        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeClassificationEntity GetForm(int keyValue)
        {
            return _SysChargeClassifyRepo.FindEntity(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _SysChargeClassifyRepo.Delete(t => t.flbh == keyValue);
        }
        public void SubmitForm(SysChargeClassificationEntity xt_sfflEntity, string keyValue)
        {
            _SysChargeClassifyRepo.SubmitForm(xt_sfflEntity, keyValue);
        }


        /// <summary>
        /// GridHiển thị tra cứu lọc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysChargeClassificationEntity> GetListBySearch(Pagination Pagination, string keyword)
        {
            var list = ExtLinq.True<SysChargeClassificationEntity>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.And(p => p.py.Contains(keyword) || (p.flmc.Contains(keyword)) || p.flbh.ToString().Contains(keyword));

            }
            return _SysChargeClassifyRepo.FindList(list, Pagination);
        }
    }
}
