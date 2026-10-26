using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineDosageRepo : RepositoryBase<SysMedicineDosageEntity>, ISysMedicineDosageRepo
    {
        public SysMedicineDosageRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Gửi quan hệ tương ứng dạng bào chế thuốc
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
        public void submitUsage(string jxCode, string yfCode)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //Xóa trước
                db.Delete<SysMedicineDosageEntity>(p => p.jxCode == jxCode);
                //Thêm lại
               // var yfCodeArr = (yfCode ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				var userYfbmentity = new SysMedicineDosageEntity();
				userYfbmentity.Id = Guid.NewGuid().ToString();
				userYfbmentity.jxCode = jxCode;
				userYfbmentity.yfCode = yfCode;
				userYfbmentity.Create();
				db.Insert(userYfbmentity);
				//foreach (var YfCode in yfCodeArr)
				//{
				//	//string[] str = YfCode.Split(new string[] { "a88123jkjfwe13120j" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
				//	//if (str.Length != 2)
				//	//{
				//	//    continue;
				//	////}
				//	//string organizeId = str[0];
				//	//string yfbmcode = str[1];
				//	var userYfbmentity = new SysMedicineDosageEntity();
				//	userYfbmentity.Id = Guid.NewGuid().ToString();
				//	userYfbmentity.jxCode = jxCode;
				//	userYfbmentity.yfCode = YfCode;
				//	userYfbmentity.Create();
				//	db.Insert(userYfbmentity);
				//}
				db.Commit();
            }
        }

    }
}
