using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建:chl
    /// Ngày kỳ:2019-12-16 17:20
    /// 描 述: đ ặt lịch 
    /// </summary>
    public class MzghbookRepo : RepositoryBase<MzghbookEntity>, IMzghbookRepo
    {
        /// <summary>
        /// Hàm tạo mặc đ ịnh
        /// </summary>
        /// <param name="databaseFactory">EFNhà máy ngữ cảnh</param>
        public MzghbookRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới, Sửa)
        /// </summary>
        /// <param name="entity">Đ ối tư ợng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(MzghbookEntity entity, decimal? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            entity.Create();
            throw new NotImplementedException(); //Khóa chính không phải string không phải dấu hiệu,chưa cho khóa chính gán giá trị 
            return Insert(entity);
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(decimal keyValue)
        {
            return Delete(p => p.BookId == keyValue);
        }
        /// <summary>
        /// SửaĐ ặt lịch đ ăng ký khám đ ặt lịch trạng thái
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zfje"></param>
        /// <param name="AppId"></param>
        /// <param name="orgId"></param>
        public void UpdateMzGhAppointment(string jsnm, string mzh, int patid, string ks, string zfje,string AppId,string orgId,string outdate,string mzlx,string ghf)
        {
            var outdatetime = Convert.ToDateTime(outdate);
            var yyzt = ((int)EnumMzyyzt.book).ToString();
            var enty = this.FindEntity(p => p.patid ==patid && p.zt == "1" && p.ks==ks &&p.OutDate== outdatetime && p.yyzt== yyzt && p.OrganizeId == orgId && p.RegType==mzlx);
            //enty.AppId = AppId;
            enty.mzh = mzh;
            enty.PayFee = Convert.ToDecimal(zfje);
            enty.PayLsh = jsnm;
            enty.RegFee = Convert.ToDecimal(zfje);
            enty.yyzt = ((int)EnumMzyyzt.reg).ToString();
            enty.ghrq = enty.OutDate;
            if (enty != null)
            {
                enty.Modify();
                this.Update(enty);
            }
        }
        public void SigninAppointment(string mzh,string orgId)
        {
            try
            {
            this.ExecuteSqlCommand("EXEC dbo.rpt_PatientAppointment @OrgId,@Mzh", new[] {
                        new SqlParameter("@OrgId", orgId)
                        ,new SqlParameter("@Mzh", mzh)
                    });
            }
            catch (Exception  ex)
            {
            }
        }

		public int FZsjTB(string ghrq, string rygh,string orgid)
		{
			return this.ExecuteSqlCommand("EXEC Newtouch_CIS..mz_分诊页面刷新保存 @ghrq,@orgId,@rygh", new[] {
						new SqlParameter("@ghrq", ghrq)
						,new SqlParameter("@orgId", orgid)
						,new SqlParameter("@rygh", rygh)
					});
		}

	}
}