using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_hljl_yb")]
    public  class bl_hljl_ybEntity : IEntity<bl_hljl_ybEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Ngay
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// Thoi gian
        /// </summary>
        public string sj { get; set; }
        /// <summary>
        /// So nhap vien
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Ho ten
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Cap do dieu duong: 1 Dac biet, 2 Cap mot, 3 Cap hai, 4 Cap ba
        /// </summary>
        public string hldj { get; set; }
        /// <summary>
        /// Loai dieu duong: 1 Dieu duong co ban, 2 Dieu duong dac biet, 3 Dieu duong theo bien chung, 4 Khac
        /// </summary>
        public string hllx { get; set; }
        /// <summary>
        /// Tien su di ung
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// Can nang
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// Nhiet do
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// Tan so ho hap
        /// </summary>
        public string hxpl { get; set; }
        /// <summary>
        /// Mach
        /// </summary>
        public string ml { get; set; }
        /// <summary>
        /// Huyet ap tam thu
        /// </summary>
        public string ssy { get; set; }
        /// <summary>
        /// Huyet ap tam truong
        /// </summary>
        public string szy { get; set; }
        /// <summary>
        /// Do bao hoa oxy trong mau
        /// </summary>
        public string xybhd { get; set; }
        /// <summary>
        /// Tho oxy
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// Mach dong mach mu chan
        /// </summary>
        public string zbdmbz { get; set; }
        /// <summary>
        /// Tinh hinh an uong: 1 Tot, 2 Binh thuong, 3 Kem
        /// </summary>
        public string ysqk { get; set; }
        /// <summary>
        /// Mo ta cham soc ong thong
        /// </summary>
        public string dghl { get; set; }
        /// <summary>
        /// Mo ta cham soc khi quan
        /// </summary>
        public string qghl { get; set; }
        /// <summary>
        /// Cham soc tu the
        /// </summary>
        public string twhl { get; set; }
        /// <summary>
        /// Cham soc da
        /// </summary>
        public string pfhl { get; set; }
        /// <summary>
        /// Cham soc dinh duong
        /// </summary>
        public string yyhl { get; set; }
        /// <summary>
        /// Huong dan an uong: 01 An thuong, 02 An mem, 03 An nua long, 04 An long, 05 Nhin an, 06 Nhin an va nuoc, 07 An qua ong mui, 08 An it muoi it mo, 09 An cho benh tieu duong, 99 Khac
        /// </summary>
        public string yszd { get; set; }
        /// <summary>
        /// Cham soc tam ly: 1 Thuc hien cham soc tam ly theo trang thai tam ly cua benh nhan, 2 Ho tro tam ly cho gia dinh
        /// </summary>
        public string xlhl { get; set; }
        /// <summary>
        /// Cham soc an toan: 1 Thuong xuyen tuan tra phong benh, 2 Them thanh chan giuong, 3 Co dinh tu chi
        /// </summary>
        public string aqhl { get; set; }
        /// <summary>
        /// Tinh trang benh tom tat
        /// </summary>
        public string jybq { get; set; }
        /// <summary>
        /// Ten hang muc quan sat dieu duong
        /// </summary>
        public string hlgcxm { get; set; }
        /// <summary>
        /// Ket qua quan sat dieu duong
        /// </summary>
        public string hlgcjg { get; set; }
        /// <summary>
        /// Ten thao tac dieu duong
        /// </summary>
        public string hlczmc { get; set; }
        /// <summary>
        /// Ten danh muc hang muc thao tac dieu duong
        /// </summary>
        public string hlczxmmc { get; set; }
        /// <summary>
        /// Ket qua quan sat dieu duong
        /// </summary>
        public string hlczjg { get; set; }
        /// <summary>
        /// Phat bang kiem tra an toan phau thuat
        /// </summary>
        public string fcssaqb { get; set; }
        /// <summary>
        /// Thu hoi bang kiem tra an toan phau thuat
        /// </summary>
        public string shssaqb { get; set; }
        /// <summary>
        /// Phat bang danh gia rui ro phau thuat
        /// </summary>
        public string fcssfxpgb { get; set; }
        /// <summary>
        /// Thu hoi bang danh gia rui ro phau thuat
        /// </summary>
        public string shssfxpgb { get; set; }
        /// <summary>
        /// Co cach ly
        /// </summary>
        public string glbz { get; set; }
        /// <summary>
        /// Loai cach ly: 1 Cach ly duong tieu hoa, 2 Cach ly duong ho hap, 3 Cach ly tiep xuc, 4 Cach ly mau - dich co the, 5 Cach ly nghiem ngat, 6 Cach ly con trung, 7 Cach ly bao ve
        /// </summary>
        public string glzl { get; set; }
        /// <summary>
        /// Chu ky dieu duong
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// Thoi gian ky cua dieu duong 
        /// </summary>
        public string hsqmrqsj { get; set; }
        /// <summary>
        /// Y thuc
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Dong tu - trai
        /// </summary>
        public string tk_z { get; set; }
        /// <summary>
        /// Dong tu - phai
        /// </summary>
        public string tk_y { get; set; }
        /// <summary>
        /// Phan ung voi anh sang - trai
        /// </summary>
        public string dgfs_z { get; set; }
        /// <summary>
        /// Phan ung voi anh sang - phai
        /// </summary>
        public string dgfs_y { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
