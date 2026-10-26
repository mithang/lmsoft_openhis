using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Thông tin đơn thuốc
    /// (Không chocfhKhi đó coi như thêm đơn thuốc mới,Số đơn thuốc tự động tạo)
    /// </summary>
    public class FeeItemsGroupedBO
    {
        /// <summary>
        /// Loại đơn thuốc Enum tương ứng EnumPrescriptionType
        /// </summary>
        public int? cflx { get; set; }

        /// <summary>
        /// Tên loại đơn thuốc
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Khi cần đơn thuốc mới Ngày thu phí(Điều kiện tiên quyết:Nhóm này chỉ làm một đơn thuốc)
        /// </summary>
        public DateTime? cfsfrq { get; set; }

        /// <summary>
        /// Nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Số thang thuốc Trung y
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// Cờ sắc thuốc hộ 1 Cần sắc hộ
        /// </summary>
        public int? djbz { get; set; }

        /// <summary>
        /// Khi có hạng mục mới,Có tạo đơn thuốc liên kết cho nó hay không
        /// </summary>
        public bool xmWithCf { get; set; }

        /// <summary>
        /// Một lần đăng ký khám có thể có các bác sĩ khác nhau kê đơn
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Một lần đăng ký khám có thể có các bác sĩ khác nhau kê đơn
        /// </summary>
        public string ks { get; set; }

        private IList<OutpatientPrescriptionDetailEntity> _ypList;
        /// <summary>
        /// Thêm mới Chi tiết thuốc
        /// sl+jl(Số lượng+Liều lượng)
        /// </summary>
        public IList<OutpatientPrescriptionDetailEntity> ypList
        {
            get
            {
                if (_ypList == null)
                {
                    return new List<OutpatientPrescriptionDetailEntity>();
                }
                return _ypList;
            }
            set
            {
                _ypList = value;
            }
        }

        private IList<OutpatientItemEntity> _xmList;
        /// <summary>
        /// Thêm mới Chi tiết hạng mục điều trị
        /// sl hoặc dczll+zxcs(Số lượng hoặc Liều điều trị mỗi lần+Số lần thực hiện)
        /// </summary>
        public IList<OutpatientItemEntity> xmList
        {
            get
            {
                if (_xmList == null)
                {
                    return new List<OutpatientItemEntity>();
                }
                return _xmList;
            }
            set
            {
                _xmList = value;
            }
        }

        private IList<OutpatientPrescriptionDetailEntity> _ztInvalidYpList;
        /// <summary>
        /// Hủy Chi tiết thuốc
        /// Việc hủy trong đơn thuốc có thể xác định duy nhất bằng mã(Điều kiện tiên quyết:Mã chi tiết trong đơn thuốc không được trùng lặp)
        /// </summary>
        public IList<OutpatientPrescriptionDetailEntity> ztInvalidYpList
        {
            get
            {
                if (_ztInvalidYpList == null)
                {
                    return new List<OutpatientPrescriptionDetailEntity>();
                }
                return _ztInvalidYpList;
            }
            set
            {
                _ztInvalidYpList = value;
            }
        }

        private IList<OutpatientItemEntity> _ztInvalidXmList;
        /// <summary>
        /// Hủy Chi tiết hạng mục điều trị
        /// Việc hủy trong đơn thuốc có thể xác định duy nhất bằng mã(Điều kiện tiên quyết:Mã chi tiết trong đơn thuốc không được trùng lặp)
        /// </summary>
        public IList<OutpatientItemEntity> ztInvalidXmList
        {
            get
            {
                if (_ztInvalidXmList == null)
                {
                    return new List<OutpatientItemEntity>();
                }
                return _ztInvalidXmList;
            }
            set
            {
                _ztInvalidXmList = value;
            }
        }
    }
}
