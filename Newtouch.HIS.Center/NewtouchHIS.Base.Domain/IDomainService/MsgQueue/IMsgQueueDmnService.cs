using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface IMsgQueueDmnService : IScopedDependency
    {
        #region Quản lý nhóm thông báo
        /// <summary>
        /// ThêmNhóm tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// Cập nhậtNhóm tin nhắnCấu hình
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ModifyNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// Nhóm tin nhắnTruy vấn cấu hình
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeGroupVO>> NoticeGroupQuery(MsgNoticeGroupBasicVO request);
        #endregion

        #region Hàng đợi thông báo
        /// <summary>
        /// ThêmTin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> AddNotice(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// Cập nhậtTin nhắnTrạng thái
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string queueId, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// Cập nhật hàng loạtTin nhắnTrạng thái
        /// </summary>
        /// <param name="queueIds"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <param name="procSuccess"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string[] queueIds, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// Cập nhậtTin nhắnThông tin cơ bản
        /// </summary>
        /// <param name="ety"></param>
        /// <returns></returns>
        Task<bool> NoticeQueueInfoModify(MsgNoticeQueueVO ety);
        #endregion

        #region Query
        /// <summary>
        /// LấyTin nhắnChi tiết
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueVO>> NoticeInfoQuery(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// Kiểm soát chất lượngTin nhắnDanh sách
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueModelVO<MrqcMsgNoticeModel>>> MrqcNoticeQuery(MsgNoticeQueueQueryVO request);
        /// <summary>
        /// LấyTin nhắnDanh sách thông báo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueQueryRspoVO>> NoticeQuery(MsgNoticeQueueQueryVO request, string AppId, DBEnum db = DBEnum.BaseDb);

        /// <summary>
        /// Truy vấn hàng đợi thông báo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<MsgNoticeQueueQueryRspoVO>>> NoticeQueryPage(OLPagination<MsgNoticeQueueQueryVO> request, string AppId);
        #endregion
    }
}
