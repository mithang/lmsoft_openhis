using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    class RecordDmnService : DmnServiceBase, IRecordDmnService
    {
#pragma warning disable CS0649 // Chưa từng gán giá trị cho trường“RecordDmnService._CommonDmnService”gán giá trị, trường sẽ luôn giữ giá trị mặc định của nó null
        private readonly ICommonDmnService _CommonDmnService;
#pragma warning restore CS0649 // Chưa từng gán giá trị cho trường“RecordDmnService._CommonDmnService”gán giá trị, trường sẽ luôn giữ giá trị mặc định của nó null
#pragma warning disable CS0169 // Không bao giờ sử dụng trường“RecordDmnService._EMRDmnService”
        private readonly IEMRDmnService _EMRDmnService;
#pragma warning restore CS0169 // Không bao giờ sử dụng trường“RecordDmnService._EMRDmnService”
        public RecordDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        ///Khóa/Mở khóa sửa đổi bệnh án
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blgxId">Quan hệ bệnh ánId</param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void LockRecord(string blid, string bllx, string OrganizeId, string UserCode, int isLock)
        {

            var bllxmc = _CommonDmnService.GetBllxTB(bllx.Substring(0,1));

            string sql = @"update a
                        set a.IsLock = @IsLock,a.LastModifierCode = '" + UserCode + @"',a.LastModifyTime = getdate()
                        from [Newtouch_EMR].[dbo]." + bllxmc + @" a
                        where a.id = @id and a.OrganizeId = @OrgId ";
            try
            {

                var para = new List<SqlParameter>();
                para.Add(new SqlParameter("@OrgId", OrganizeId));
                para.Add(new SqlParameter("@Id", blid));
                para.Add(new SqlParameter("@IsLock", isLock));
                this.ExecuteSqlCommand(sql, para.ToArray());
            }
            catch (FailedException ex)
            {
                throw new FailedException("操作失败(" + ex.InnerException + ")");
            }

        }

        public medicalRecordVO GetMedicalRecord(string blid, string bllx,string organizeId)
        {
            var medicalRecordVO = new medicalRecordVO();
            
            switch (bllx.Substring(0,1))
            {
                //Bệnh án nhập viện
                case "1":
                    var rybl = bl_ryblGetByID(blid,organizeId);
                    medicalRecordVO.blxtml = rybl.blxtml;
                    medicalRecordVO.blxtmc_yj = rybl.blxtmc_yj;
                    medicalRecordVO.IsLock = rybl.IsLock;
                    medicalRecordVO.LastModifierCode = rybl.LastModifierCode;
                    break;
                //Ghi chép diễn biến bệnh
                case "2":
                    var bcjl = bl_bcjlGetByID(blid,organizeId);
                    medicalRecordVO.blxtml = bcjl.blxtml;
                    medicalRecordVO.blxtmc_yj = bcjl.blxtmc_yj;
                    medicalRecordVO.IsLock = bcjl.IsLock;
                    medicalRecordVO.LastModifierCode = bcjl.LastModifierCode;
                    break;
                //Văn bản y tế
                case "3":
                    var ylws = bl_zqwsGetByID(blid, organizeId);
                    medicalRecordVO.blxtml = ylws.blxtml;
                    medicalRecordVO.blxtmc_yj = ylws.blxtmc_yj;
                    medicalRecordVO.IsLock = ylws.IsLock;
                    medicalRecordVO.LastModifierCode = ylws.LastModifierCode;
                    break;
                //Ghi chép điều dưỡng
                case "4":
                    var hljl = bl_hljlGetByID(blid, organizeId);
                    medicalRecordVO.blxtml = hljl.blxtml;
                    medicalRecordVO.blxtmc_yj = hljl.blxtmc_yj;
                    medicalRecordVO.IsLock = hljl.IsLock;
                    medicalRecordVO.LastModifierCode = hljl.LastModifierCode;
                    break;
                //Trang đầu bệnh án
                case "5":
                    var basy = bl_basyGetByID(blid,organizeId);
                    medicalRecordVO.blxtml = basy.blxtml;
                    medicalRecordVO.blxtmc_yj = basy.blxtmc_yj;
                    medicalRecordVO.IsLock = basy.IsLock;
                    medicalRecordVO.LastModifierCode = basy.LastModifierCode;
                    break;
                //Đánh giá phục hồi chức năng
                case "6":
                    var kfpg = bl_basyGetByID(blid, organizeId);
                    medicalRecordVO.blxtml = kfpg.blxtml;
                    medicalRecordVO.blxtmc_yj = kfpg.blxtmc_yj;
                    medicalRecordVO.IsLock = kfpg.IsLock;
                    medicalRecordVO.mbbh = kfpg.mbbh;
                    medicalRecordVO.LastModifierCode = kfpg.LastModifierCode;
                    break;
            }
            return medicalRecordVO;
        }



        public bl_ryblVO bl_ryblGetByID(string ID, string organizeId)
        {
            string sql = @"select * from [Newtouch_EMR].[dbo].bl_rybl (nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(ID))
            {
                sql += " and Id = @ID";
            }
            var result = this.FirstOrDefault<bl_ryblVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@ID", ID)
            });
            return result;
        }

        public bl_bcjlVO bl_bcjlGetByID(string ID, string organizeId)
        {

            string sql = @"select * from [Newtouch_EMR].[dbo].bl_bcjl (nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(ID))
            {
                sql += " and Id = @ID";
            }
            var result = this.FirstOrDefault<bl_bcjlVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@ID", ID)
            });
            return result;
        }
        public bl_zqwsVO bl_zqwsGetByID(string ID, string organizeId)
        {
            string sql = @"select * from [Newtouch_EMR].[dbo].bl_bcjl (nolock) where  zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(ID))
            {
                sql += " and Id = @ID";
            }
            var result = this.FirstOrDefault<bl_zqwsVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@ID", ID)
            });
            return result;
        }

        public bl_hljlVO bl_hljlGetByID(string ID, string organizeId)
        {
            string sql = @"select * from [Newtouch_EMR].[dbo].bl_hljl (nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(ID))
            {
                sql += " and Id = @ID";
            }
            var result = this.FirstOrDefault<bl_hljlVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@ID", ID)
            });
            return result;
        }
        public BlbasyVO bl_basyGetByID(string ID, string organizeId)
        {
            string sql = @"select * from [Newtouch_EMR].[dbo].bl_basy (nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(ID))
            {
                sql += " and Id = @ID";
            }
            var result = this.FirstOrDefault<BlbasyVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@ID", ID)
            });
            return result;
        }

        public ZymeddocsrelationVO GetZymeddocsrelation(string blId, string organizeId)
        {
            string sql = @"select * from [Newtouch_EMR].[dbo].zy_meddocs_relation (nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(blId))
            {
                sql += " and blId = @blId";
            }
            var result = this.FirstOrDefault<ZymeddocsrelationVO>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@blId", blId)
            });
            return result;
        }

        public int updateRecordStu(string id, string organizeId,string LastModifierCode, DateTime LastModifyTime,string blzt)
        {
            //string sql = "update [Newtouch_EMR].[dbo].[zy_brjbxx] set RecordStu='3',LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where 1=1 ";
            string sql = "update [Newtouch_EMR].[dbo].[zy_brjbxx] set RecordStu=@blzt,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where 1=1 ";

            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeId = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                sql += "  and id in (" + id + ")";
            }
            SqlParameter[] para ={
                new SqlParameter("@LastModifyTime",Convert.ToDateTime(LastModifyTime)),
                new SqlParameter("@LastModifierCode",LastModifierCode ?? ""),
				new SqlParameter("@blzt",blzt ?? ""),
                //new SqlParameter("@id",id ?? ""),

                new SqlParameter("@organizeId",organizeId ?? "")
                };
            return this.ExecuteSqlCommand(sql, para);
        }
    }
}
