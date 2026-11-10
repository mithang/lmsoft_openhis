using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Đăng ký phẫu thuật
    /// </summary>
    public class ORRegistrationRepo : RepositoryBase<ORRegistrationEntity>, IORRegistrationRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORRegistrationRepo()
        {
            
        }

        /// <summary>
        /// G24: kiem tra do dai chuoi theo cot DB; qua dai -> loi ro rang
        /// thay vi loi cat chuoi cua SQL. Tra ve gia tri goc de gan truc tiep.
        /// </summary>
        private static string Len(string value, int max, string name)
        {
            if (value != null && value.Length > max)
            {
                throw new FailedException(name + " tối đa " + max + " ký tự");
            }
            return value;
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(RegistrationListVO entity, string keyValue,string ssxh)
        {
            if (!(string.IsNullOrEmpty(keyValue) || keyValue == "null"))
            {
                var dbEntity = this.FindEntity(keyValue);
                // G24: sua khong ton tai/khac co so -> loi ro rang thay vi NRE;
                // khong doi OrganizeId cua dong (base ghi de tu form nen sua
                // cross-org doi dong sang co so goi).
                if (dbEntity == null || dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Không tìm thấy đăng ký phẫu thuật");
                }
                // G24 merge-skip-null (G2): sua truong nao thi ghi truong do;
                // truong khong gui (null) giu nguyen gia tri cu thay vi bi
                // wipe ve null (base gan truc tiep ~30 truong).
                if (entity.arrangeId != null)
                {
                    dbEntity.ArrangeId = Len(entity.arrangeId, 50, "Số xếp lịch");
                }
                if (entity.applyno != null)
                {
                    dbEntity.Applyno = Len(entity.applyno, 50, "Số đơn đăng ký");
                }
                if (ssxh != null)
                {
                    dbEntity.ssxh = Len(ssxh, 50, "Số đăng ký phẫu thuật");
                }
                if (entity.zyh != null)
                {
                    dbEntity.zyh = Len(entity.zyh, 50, "Số nhập viện");
                }
                dbEntity.sqzt = "2";
                if (entity.ryzd != null)
                {
                    dbEntity.ryzd = Len(entity.ryzd, 100, "Chẩn đoán lúc nhập viện");
                }
                if (entity.ryzdmc != null)
                {
                    dbEntity.ryzdmc = Len(entity.ryzdmc, 200, "Tên chẩn đoán lúc nhập viện");
                }
                if (entity.sszd != null)
                {
                    dbEntity.sszd = Len(entity.sszd, 100, "Chẩn đoán sau phẫu thuật");
                }
                if (entity.sszdmc != null)
                {
                    dbEntity.sszdmc = Len(entity.sszdmc, 200, "Tên chẩn đoán sau phẫu thuật");
                }
                if (entity.shbq != null)
                {
                    dbEntity.shbq = Len(entity.shbq, 200, "Tình trạng bệnh");
                }
                if (entity.ssmc != null)
                {
                    dbEntity.ssmc = Len(entity.ssmc, 100, "Tên phẫu thuật");
                }
                if (entity.ssdm != null)
                {
                    dbEntity.ssdm = Len(entity.ssdm, 50, "Mã phẫu thuật");
                }
                if (entity.ssapsj.HasValue)
                {
                    dbEntity.ssapsj = entity.ssapsj;
                }
                if (entity.sssqsj.HasValue)
                {
                    dbEntity.sssqsj = entity.sssqsj;
                }
                if (entity.sskssj.HasValue)
                {
                    dbEntity.sskssj = entity.sskssj;
                }
                if (entity.ssjssj.HasValue)
                {
                    dbEntity.ssjssj = entity.ssjssj;
                }
                if (entity.AnesCode != null)
                {
                    dbEntity.AnesCode = Len(entity.AnesCode, 50, "Phương pháp gây mê");
                }
                if (entity.oproom != null)
                {
                    dbEntity.oproom = Len(entity.oproom, 20, "Phòng phẫu thuật");
                }
                if (entity.oporder != null)
                {
                    dbEntity.oporder = Len(entity.oporder, 2, "Lượt mổ");
                }
                if (entity.qkdj != null)
                {
                    dbEntity.qkdj = Len(entity.qkdj, 10, "Cấp vết mổ");
                }
                if (entity.isgl != null)
                {
                    dbEntity.isgl = Len(entity.isgl, 2, "Cách ly");
                }
                if (entity.isjun != null)
                {
                    dbEntity.isjun = Len(entity.isjun, 2, "Nhiễm khuẩn");
                }
                if (entity.shuxl.HasValue)
                {
                    dbEntity.shuxl = entity.shuxl;
                }
                if (entity.shixl.HasValue)
                {
                    dbEntity.shixl = entity.shixl;
                }
                if (entity.zrxl.HasValue)
                {
                    dbEntity.zrxl = entity.zrxl;
                }
                if (entity.zcxl.HasValue)
                {
                    dbEntity.zcxl = entity.zcxl;
                }
                if (entity.ssbw != null)
                {
                    dbEntity.ssbw = Len(entity.ssbw, 100, "Vị trí phẫu thuật");
                }
                dbEntity.zt = "1";
                if (entity.memo != null)
                {
                    dbEntity.memo = Len(entity.memo, 1000, "Ghi chú");
                }
                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            else
            {
            // G24: tao moi — so don + so nhap vien la bat buoc (dang ky
            // khong co lich/benh nhan la dong rac); khong gan Id tu keyValue
            // nua (base: keyValue "null" tao dong co Id chu "null").
            if (string.IsNullOrWhiteSpace(entity.applyno))
            {
                throw new FailedException("Số đơn đăng ký không được để trống");
            }
            if (string.IsNullOrWhiteSpace(entity.zyh))
            {
                throw new FailedException("Số nhập viện không được để trống");
            }
            ORRegistrationEntity regEntity = new ORRegistrationEntity();
            //properties
            regEntity.OrganizeId = entity.OrganizeId;
            regEntity.ArrangeId = Len(entity.arrangeId, 50, "Số xếp lịch");
            regEntity.Applyno = Len(entity.applyno, 50, "Số đơn đăng ký");
            regEntity.ssxh = Len(ssxh, 50, "Số đăng ký phẫu thuật");
            regEntity.zyh = Len(entity.zyh, 50, "Số nhập viện");
            regEntity.sqzt = "2";
            regEntity.ryzd = Len(entity.ryzd, 100, "Chẩn đoán lúc nhập viện");
            regEntity.ryzdmc = Len(entity.ryzdmc, 200, "Tên chẩn đoán lúc nhập viện");
            regEntity.sszd = Len(entity.sszd, 100, "Chẩn đoán sau phẫu thuật");
            regEntity.sszdmc = Len(entity.sszdmc, 200, "Tên chẩn đoán sau phẫu thuật");
            regEntity.shbq = Len(entity.shbq, 200, "Tình trạng bệnh");
            regEntity.ssmc = Len(entity.ssmc, 100, "Tên phẫu thuật");
            regEntity.ssdm = Len(entity.ssdm, 50, "Mã phẫu thuật");
            regEntity.ssapsj = entity.ssapsj;
            regEntity.sssqsj = entity.sssqsj;
            regEntity.sskssj = entity.sskssj;
            regEntity.ssjssj = entity.ssjssj;
            regEntity.AnesCode = Len(entity.AnesCode, 50, "Phương pháp gây mê");
            regEntity.oproom = Len(entity.oproom, 20, "Phòng phẫu thuật");
            regEntity.oporder = Len(entity.oporder, 2, "Lượt mổ");
            regEntity.qkdj = Len(entity.qkdj, 10, "Cấp vết mổ");
            regEntity.isgl = Len(entity.isgl, 2, "Cách ly");
            regEntity.isjun = Len(entity.isjun, 2, "Nhiễm khuẩn");
            regEntity.shuxl = entity.shuxl;
            regEntity.shixl = entity.shixl;
            regEntity.zrxl = entity.zrxl;
            regEntity.zcxl = entity.zcxl;
            regEntity.ssbw = Len(entity.ssbw, 100, "Vị trí phẫu thuật");
            regEntity.zt = "1";
            regEntity.memo = Len(entity.memo, 1000, "Ghi chú");
            EntityHelper.Create(regEntity, true);
            return Insert(regEntity);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            // G24: khoa khong ton tai -> loi ro rang thay vi NRE.
            // Rang buoc co so do controller kiem tra truoc khi goi.
            var dbEntity = this.FindEntity(keyValue);
            if (dbEntity == null)
            {
                throw new FailedException("Không tìm thấy đăng ký phẫu thuật");
            }
            //properties
            dbEntity.zt = "0";
            dbEntity.sqzt = "3";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }
        
    }
}