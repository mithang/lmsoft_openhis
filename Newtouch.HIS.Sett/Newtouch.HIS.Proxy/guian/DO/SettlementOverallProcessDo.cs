using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DO
{
    /// <summary>
    /// Quyết toán完整流程 nơi 需tham số
    /// </summary>
    public class SettlementOverallProcessDo
    {
        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        public string outpId { get; set; }
    }

    /// <summary>
    /// 虚拟quyết toán完整流程 nơi 需tham số
    /// </summary>
    public class SimulationSettlementOverallProcessDo
    {
        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên yêu cầu tham số
        /// </summary>
        public S22Param S22Param { get; set; }

        /// <summary>
        /// S21Giao diệnTải lênChi tiết
        /// </summary>
        public List<S21Detail> S21Details { get; set; }
    }

    /// <summary>
    /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên yêu cầu tham số
    /// </summary>
    public class S22Param
    {
        /// <summary>
        /// Thời gian bắt đ ầu(yyyy-mm-dd)
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 截止thời gian(yyyy-mm-dd)
        /// </summary>
        public string endDate { get; set; }
    }

    /// <summary>
    /// S18giao diện nơi 需tham số
    /// </summary>
    public class S18Param
    {
        /// <summary>
        /// mã cá nhân 
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// Khám bệnhThời gian(yyyy-mm-dd)
        /// </summary>
        public DateTime jzsj{ get; set; }

        /// <summary>
        /// Khoa khám bệnh,我们hệ thống nơi dùng khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// Mã bệnh(Giao diệnS29Tải xuống và lấy)
        /// </summary>
        public string diseaseCode { get; set; }
    }

    /// <summary>
    /// S21 tải lênchi tiết nơi 需tham số
    /// </summary>
    public class S21Detail
    {
        /// <summary>
        /// Chi tiết tên tiếng trung 
        /// </summary>
        public string detailName { get; set; }

        /// <summary>
        /// mã hợp tác nông thôn 
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        /// Hischi tiết thứ tự số (duy nhất số hiệu)
        /// </summary>
        public string hisDetailCode { get; set; }
        
        /// <summary>
        /// Đ ịa phư ơng mã PDAcó thể tải lên mã hợp tác nông thôn 
        /// </summary>
        public string detailHosCode { get; set; }
        
        /// <summary>
        /// Đ ịa phư ơng loại mã
        /// </summary>
        public string typeCode { get; set; }
        
        /// <summary>
        /// Số lư ợng(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal num { get; set; }
        
        /// <summary>
        /// Đ ơn giá(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal price { get; set; }
       
        /// <summary>
        /// Tổng giá(đ ộ chính xác 4 chữ số thập phân)
        /// </summary>
        public decimal totalCost { get; set; }
        
        /// <summary>
        /// Ngày dùng thuốc(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }
        
        /// <summary>
        /// Đ ơn vị
        /// </summary>
        public string unit { get; set; }
        
        /// <summary>
        /// Quy cách
        /// </summary>
        public string standard { get; set; }
        
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string formulations { get; set; }
    }
}