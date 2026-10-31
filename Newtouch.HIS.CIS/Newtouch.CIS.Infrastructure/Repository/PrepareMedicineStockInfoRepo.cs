
using Newtouch.CIS.Repository;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class PrepareMedicineStockInfoRepo : RepositoryBase<PrepareMedicineStockInfoEntity>, IPrepareMedicineStockInfoRepo
    {
        public PrepareMedicineStockInfoRepo()
            : base()
        {
        }

        /// <summary>
        /// Tính toáncó thểdùngTồn kho hiện tạiPhương phápkhôngngàychí
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        public List<PrepareMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_ksby_kcxx(NOLOCK) 
		WHERE ypdm = @ypCode
		AND yfbmCode = @yfbmCode 
		AND tybz = '0' 
		AND kcsl>0
		AND djsl>=0
		AND pc=@pc
		AND ph=@ph
		AND zt='1'
		AND OrganizeId = @OrganizeId 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph)
            };
            return Db.Ado.SqlQuery<PrepareMedicineStockInfoEntity>(sql, Newtouch.CIS.Compat.SqlParam.ToSugar(param)).ToList();
        }

        /// <summary>
        /// đổihơnTồn khoSố lượng
        /// </summary>
        /// <param name="sl">Đơn vị nhỏ nhấtSố lượng +:thêmTồn kho  -:giảmTồn kho</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateKcslWithTrans(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode, Infrastructure.EF.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.xt_ksby_kcxx SET kcsl=kcsl+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sl", sl ),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph",ph ),
                new SqlParameter("@ypCode",ypCode ),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode)
            };
            return db.ExecuteSqlCommand(sql, param);
        }

    }
}
