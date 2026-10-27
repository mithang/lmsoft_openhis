using System;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using NewtouchHIS.Lib.Base.Exceptions;


namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Điều chỉnh giá thuốc xt_yptj
    /// </summary>
    public class SysMedicinePriceAdjustmentRepo : RepositoryBase<SysMedicinePriceAdjustmentEntity>, ISysMedicinePriceAdjustmentRepo
    {
        /// <summary>
        /// Khởi tạo hàm tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        /// <summary>
        /// Lấy một điều chỉnh giáentity Chưa duyệt Chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotApproveEntity(string ypCode)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().FirstOrDefault(a => a.ypCode == ypCode && a.zxbz == "0" && (a.shzt == "0" || a.shzt == "1") && a.zxsj > DateTime.Now && a.OrganizeId == organizeId);   //Chưa duyệt Chưa thực hiện
        }

        /// <summary>
        /// Lấy một điều chỉnh giáentity Đã duyệt Chưa thực hiện
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotExecteEntity(string ypCode)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().FirstOrDefault(a => a.ypCode == ypCode && a.zxbz == "0" && a.shzt == "1" && a.OrganizeId == organizeId);   //Đã duyệt Chưa thực hiện
        }

        /// <summary>
        /// Thêm bản ghi điều chỉnh giá thuốc(Xin)
        /// </summary>
        /// <param name="entity"></param>
        public void AddPriceAdjustmentRecord(SysMedicinePriceAdjustmentEntity entity)
        {
            Insert(entity);
        }

        /// <summary>
        /// Kiểm tra có dữ liệu chưa xử lý không
        /// </summary>
        /// <param name="ypCodeList"></param>
        public string CheckStatus(ArrayList ypCodeList)
        {
            if (ypCodeList == null || ypCodeList.Count == 0)
            {
                return "Dữ liệu yêu cầu trống,Vui lòng liên hệ quản trị viên";
            }
            foreach (var item in ypCodeList)
            {
                var entity = IQueryable().OrderByDescending(o=>o.CreateTime).FirstOrDefault(a => a.ypCode == item.ToString() && a.zxsj > DateTime.Now);
                if (entity == null)
                {
                    return string.Format(@"Không tra được mã thuốc là{0}của thuốc！", item);
                }
                if (entity.shzt.Equals("0") || entity.shzt.Equals("2") || entity.shzt.Equals("3"))  //Chưa duyệt Từ chối Thu hồi
                {
                    return entity.ypCode + " Trạng thái chưa duyệt hoặc từ chối hoặc thu hồi，Không thể thực hiện";
                }
                if ("1".Equals(entity.zxbz))   //Thực hiện
                {
                    return entity.ypCode + " Dữ liệu đã thực hiện，Không thể thực hiện trùng lặp";
                }
                if (entity.zxsj <= DateTime.Now)
                {
                    return entity.ypCode + " Đã quá ngày thực hiện";
                }
            }
            return string.Empty;
        }

        ///  <summary>
        ///  Duyệt điều chỉnh giá thuốc
        ///  </summary>
        ///  --shzt:0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
        ///  --zxbz:0:Chưa thực hiện 1:Đã thực hiện
        /// <param name="ypCodeList"></param>
        /// <param name="operationType"></param>
        public string MedicinePriceAdjustment(ArrayList ypCodeList, int operationType)
        {
            var expireApply = new List<string>();
            using (var db = new EFDbTransaction().BeginTrans())
            {
                foreach (var item in ypCodeList)
                {
                    var entity = db.IQueryable<SysMedicinePriceAdjustmentEntity>().FirstOrDefault(a => a.ypCode == item.ToString() && a.zxbz == "0" && a.shzt == "0");
                    if (entity == null) continue;
                    if (entity.zxsj < DateTime.Now)
                    {
                        expireApply.Add(entity.ypCode);
                        continue;
                    }
                    entity.zxbz = ((int)EnumPriceAdjustZXStatus.Not).ToString();
                    switch (operationType)
                    {
                        case (int)EnumPriceAdjustOperationType.Approval:   //Duyệt
                            entity.shzt = operationType.ToString();
                            entity.shczy = OperatorProvider.GetCurrent().UserCode;
                            break;
                        case (int)EnumPriceAdjustOperationType.Refuse:   //Từ chối
                            entity.shzt = operationType.ToString();
                            entity.shczy = OperatorProvider.GetCurrent().UserCode;
                            break;
                        case (int)EnumPriceAdjustOperationType.Withdraw:  //Thu hồi
                            entity.shzt = operationType.ToString();
                            entity.pfj = entity.ypfj;
                            entity.lsj = entity.ylsj;
                            break;
                    }
                    entity.Modify();
                    db.Update(entity);
                }
                db.Commit();
            }
            return expireApply.Count > 0 ? string.Format("Mã thuốc là{0}thao tác thuốc thất bại，Đã quá thời gian thực hiện muộn nhất。", string.Join(",", expireApply)) : "Thao tác thành công";
        }
    }
}
