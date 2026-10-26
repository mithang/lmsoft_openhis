using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Chung tu nhap/xuat kho
    /// </summary>
    public interface IKfCrkdjDmnService
    {
        /// <summary>
        /// Lay thong tin bang chinh chung tu nhap/xuat kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="alldjlx">Tat ca cac loai chung tu ma nguoi dung nay co the so huu</param>
        /// <param name="warehouseId">Kho hien tai</param>
        /// <param name="ope">Thao tac  query:Tra cuu  approval:Duyet</param>
        /// <returns></returns>
        IList<VCrkdjEntity> GetCrkdjMainList(Pagination pagination, CrkdjSearchParamDTO param, string[] alldjlx, string warehouseId, string ope = "");

        /// <summary>
        /// Lay chi tiet nhap/xuat kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCrkdjmxEntity> GetCrkdjmxList(string crkId, string organizeId);

        /// <summary>
        /// Lay thong tin nha cung cap va hoa don
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VFphAndGysEntity> GetFphAndGysInfo(string keyWord, string warehouseId, string organizeId);


        /// <summary>
        /// Xoa bang chinh va bang chi tiet chung tu Su dung than trong
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteDjById(long crkId, string organizeId);

        /// <summary>
        /// Luu chung tu
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        string SaveDj(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// Huy chi tiet da chen thanh cong
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="successList"></param>
        void CancelCrkmx(KfCrkdjEntity dj, List<KfCrkmxEntity> successList);

        /// <summary>
        /// Tra cuu So phieu giao hang nhap kho
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        List<VInStorageDeliveryInfoEntity> SelectInStorageDeliveryInfo(string keyword, string warehouseId, string userCode, string organizeid, string shzt = "4");

        /// <summary>
        /// Lay chi tiet nhap/xuat kho qua So phieu giao hang
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="djh"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeid"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        List<VCrkdjmxInfoEntity> SelectCrkmxByDeliveryNo(string deliveryNo, string djh, string warehouseId, string organizeid, string gysId = "");

        /// <summary>
        /// Xoa chung tu nhap/xuat kho va chi tiet da chi dinh
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string DeleteCrkdj(long crkId);
    }
}