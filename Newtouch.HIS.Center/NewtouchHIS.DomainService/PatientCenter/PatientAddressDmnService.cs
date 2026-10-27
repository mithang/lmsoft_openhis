using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Lib.Base.Exceptions;

namespace NewtouchHIS.DomainService.PatientCenter
{
    public class PatientAddressDmnService : BaseServices<SysPatientAddressEntity>, IPatientAddressDmnService
    {

        public async Task<SysPatientAddressEntity> PatientAddressQuery(int patid, string orgId)
        {
            var result = await baseDal.GetFirstOrDefaultWithAttr<SysPatientAddressEntity>(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1");

            return result;
        }

        public async Task<bool> PatientAddressUpdate(SysPatientAddressEntity entity, string user)
        {
            if (entity.patid == 0)
            {
                throw new FailedException("patId không được để trống");
            }
            if (string.IsNullOrWhiteSpace(entity.dh))
            {
                throw new FailedException("Số điện thoại không được để trống");
            }
            var repeatEntity = await baseDal.GetByWhereWithAttr<SysPatientAddressEntity>(p => p.patid == entity.patid && p.Id != entity.Id && p.dh == entity.dh && p.OrganizeId == entity.OrganizeId && p.zt == "1");
            if (!(repeatEntity == null || repeatEntity.Count == 0))
            {
                throw new FailedException("Thông tin địa chỉ bệnh nhân bất thường, xác nhận số điện thoại có trùng lặp hay không!");
            }

            var dbEntity = await baseDal.GetFirstOrDefaultWithAttr<SysPatientAddressEntity>(p => p.Id == entity.Id && p.OrganizeId == entity.OrganizeId && p.zt == "1");

            entity.zt = "1";
            entity.CreateTime = dbEntity.CreateTime;
            entity.CreatorCode = dbEntity.CreatorCode;
            entity.ModifiedEntity(entity.OrganizeId, user);
            return await baseDal.UpdateAsync(entity);
        }


        public async Task<SysPatientAddressEntity> PatientAddressAdd(SysPatientAddressEntity entity, string user)
        {
            if (entity.patid == 0)
            {
                throw new FailedException("patId không được để trống");
            }
            if (string.IsNullOrWhiteSpace(entity.dh))
            {
                throw new FailedException("Số điện thoại không được để trống");
            }


            //电话一致,Họ tên不一致
            var dbentity = await baseDal.GetByWhereWithAttr<SysPatientAddressEntity>(p => p.patid == entity.patid && p.dh == entity.dh && p.xm != entity.xm && p.OrganizeId == entity.OrganizeId && p.zt == "1");
            if (dbentity.Count > 0)
            {
                throw new FailedException("Thông tin địa chỉ bệnh nhân bất thường, xác nhận số điện thoại có trùng lặp hay không!");
            }

            //电话,Họ tên一致,更新
            var upentity = await baseDal.GetFirstOrDefaultWithAttr<SysPatientAddressEntity>(p => p.patid == entity.patid && p.dh == entity.dh && p.xm == entity.xm && p.OrganizeId == entity.OrganizeId && p.zt == "1");
            if (upentity != null)
            {
                entity.zt = "1";
                entity.CreateTime = upentity.CreateTime;
                entity.CreatorCode = upentity.CreatorCode;
                entity.ModifiedEntity(entity.OrganizeId, user);
                entity.Id = upentity.Id;
                await baseDal.UpdateAsync(entity);
                return entity;
            }
            else
            {  //新增
                entity.NewEntity(entity.OrganizeId, user);
                entity.Id = Guid.NewGuid().ToString();
                await baseDal.InsertAsync(entity);
                var resp = await baseDal.GetFirstOrDefault(p => p.patid == entity.patid && p.dh == entity.dh && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                return resp;
            }
        }


        public async Task<bool> PatientAddressDelete(SysPatientAddressEntity entity, string user)
        {
            if (entity.patid == 0)
            {
                throw new FailedException("patId không được để trống");
            }
            if (string.IsNullOrWhiteSpace(entity.dh))
            {
                throw new FailedException("Số điện thoại không được để trống");
            }

            var dbEntity = await baseDal.GetFirstOrDefaultWithAttr<SysPatientAddressEntity>(p => p.Id == entity.Id && p.OrganizeId == entity.OrganizeId && p.zt == "1");

            entity.zt = "1";
            entity.CreateTime = dbEntity.CreateTime;
            entity.CreatorCode = dbEntity.CreatorCode;
            entity.ModifiedEntity(entity.OrganizeId, user, true);
            return await baseDal.DeleteAsync(entity);
        }


        public async Task<SysPatientAddressEntity> PatientOrderAddressQuery(string visitNo, string ks, string orgId)
        {
            var dz = await baseDal.GetFirstOrDefaultWithAttr<OrderInfoEntity>(p => p.VisitNo == visitNo && p.OrganizeId == orgId && p.zt == "1" && p.dzId != null);
            if (dz == null)
            {
                return null;
            }
            else
            {
                var dzId = dz.dzId;
                var result = await baseDal.GetFirstOrDefaultWithAttr<SysPatientAddressEntity>(p => p.Id == dzId && p.OrganizeId == orgId && p.zt == "1");
                return result;
            }
        }


        public async Task<OutpatientRegistEntity> OutpatientRegistQuery(int patid, string kh, string orgId)
        {
            var gh = await baseDal.GetFirstOrDefaultWithAttr<OutpatientRegistEntity>(p => p.patid == patid && p.kh == kh && p.OrganizeId == orgId && p.zt == "1");
            if (gh == null)
            {
                throw new FailedException("Thông tin bệnh nhân không chính xác,Vui lòng kiểm tra patid và số thẻ");
            }
            else
            {
                return gh;
            }
        }

    }
}
