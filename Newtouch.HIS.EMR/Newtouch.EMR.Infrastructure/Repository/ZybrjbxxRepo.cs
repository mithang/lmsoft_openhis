using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2018-08-30 11:31
    /// Mô tả:Thông tin bệnh nhân nội trú
    /// </summary>
    public class ZybrjbxxRepo : RepositoryBase<ZybrjbxxEntity>, IZybrjbxxRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh EF</param>

        public ZybrjbxxEntity GetZybrjbxx(string zyh, string OrganizeId)
        {

            return this.FindEntity(p => p.zyh==zyh&&p.OrganizeId== OrganizeId);
        }
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void SubmitForm(ZybrjbxxEntity entity, string keyValue)
        {
            ZybrjbxxEntity dbEntity = null;
            if (entity != null && string.IsNullOrEmpty(keyValue))
            {
                dbEntity = this.FindEntity(p => p.zyh == entity.zyh && p.OrganizeId == entity.OrganizeId);
            }
            else if (entity != null && !string.IsNullOrEmpty(keyValue))
            {
                dbEntity = this.FindEntity(keyValue);
            }

            if (dbEntity != null)
            {
                //properties
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.zyh = entity.zyh;
                dbEntity.blh = entity.blh;
                dbEntity.xm = entity.xm;
                dbEntity.py = entity.py;
                dbEntity.wb = entity.wb;
                dbEntity.sfzh = entity.sfzh;
                dbEntity.sex = entity.sex;
                dbEntity.birth = entity.birth;
                dbEntity.zybz = entity.zybz;
                dbEntity.sfqj = entity.sfqj;
                dbEntity.DeptCode = entity.DeptCode;
                dbEntity.WardCode = entity.WardCode;
                dbEntity.ysgh = entity.ysgh;
                dbEntity.BedCode = entity.BedCode;
                dbEntity.ryrq = entity.ryrq;
                dbEntity.rqrq = entity.rqrq;
                dbEntity.cqrq = entity.cqrq;
                dbEntity.wzjb = entity.wzjb;
                dbEntity.hljb = entity.hljb;
                dbEntity.ryfs = entity.ryfs;
                dbEntity.cyfs = entity.cyfs;
                dbEntity.gdxmzxrq = entity.gdxmzxrq;
                dbEntity.brxzdm = entity.brxzdm;
                dbEntity.brxzmc = entity.brxzmc;
                dbEntity.cardno = entity.cardno;
                dbEntity.cardtype = entity.cardtype;
                dbEntity.lxr = entity.lxr;
                dbEntity.lxrgx = entity.lxrgx;
                dbEntity.lxrdh = entity.lxrdh;
                dbEntity.zddm = entity.zddm;
                dbEntity.zdmc = entity.zdmc;
                dbEntity.cyzddm = entity.cyzddm;
                dbEntity.cyzdmc = entity.cyzdmc;
                //dbEntity.Memo = entity.Memo;
                //dbEntity.CreateTime = entity.CreateTime;
                //dbEntity.CreatorCode = entity.CreatorCode;
                //dbEntity.LastModifyTime = entity.LastModifyTime;
                //dbEntity.LastModifierCode = entity.LastModifierCode;
                //dbEntity.zt = entity.zt;
                this.Update(dbEntity);
            }
            else
            {
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Nộp hồ sơ bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void CommitRecord(string zyh,string OrgId,string user)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                var ety = this.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrgId && p.zt == "1");
                if (ety != null)
                {
                    ety.RecordStu = ((int)EnumRecordStu.ytj).ToString();
                    ety.Commitor = user;
                    ety.CommitTime = DateTime.Now;
                    this.Update(ety);
                }
            }
            else
                throw new FailedException("Thông tin bệnh nhân không được để trống");
        }

        /// <summary>
        /// Trả lại bệnh án
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        public void BackRecord(string zyh, string OrgId, string user)
        {
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                var ety = this.FindEntity(p => p.zyh == zyh && p.OrganizeId == OrgId && p.zt == "1");
                if (ety != null)
                {
                    ety.RecordStu = ((int)EnumRecordStu.th).ToString();
                    ety.Commitor = user;
                    ety.CommitTime = DateTime.Now;
                    this.Update(ety);
                }
            }
            else
                throw new FailedException("Thông tin bệnh nhân không được để trống");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }

    }
}
