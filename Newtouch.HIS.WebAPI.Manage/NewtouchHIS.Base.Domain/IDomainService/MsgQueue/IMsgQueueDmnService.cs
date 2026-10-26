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
        #region 消息组管理
        /// <summary>
        /// thêm nhóm tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// cập nhật cấu hình nhóm tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ModifyNoticeGroup(MsgNoticeGroupBasicVO request);
        /// <summary>
        /// truy vấn cấu hình nhóm tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeGroupVO>> NoticeGroupQuery(MsgNoticeGroupBasicVO request);
        #endregion

        #region 消息队列
        /// <summary>
        /// thêm tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> AddNotice(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// cập nhật trạng thái tin nhắn
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string queueId, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// cập nhật hàng loạt trạng thái tin nhắn
        /// </summary>
        /// <param name="queueIds"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <param name="procSuccess"></param>
        /// <returns></returns>
        Task<bool> NoticeStuModify(string[] queueIds, string user, NoticeStuEnum noticeStu, bool procSuccess = true);
        /// <summary>
        /// cập nhật thông tin cơ bản tin nhắn
        /// </summary>
        /// <param name="ety"></param>
        /// <returns></returns>
        Task<bool> NoticeQueueInfoModify(MsgNoticeQueueVO ety);
        #endregion

        #region Query
        /// <summary>
        /// lấy chi tiết tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueVO>> NoticeInfoQuery(MsgNoticeQueueBasicVO request);
        /// <summary>
        /// danh sách tin nhắn kiểm soát chất lượng
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueModelVO<MrqcMsgNoticeModel>>> MrqcNoticeQuery(MsgNoticeQueueQueryVO request);
        /// <summary>
        /// lấy danh sách thông báo tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<MsgNoticeQueueQueryRspoVO>> NoticeQuery(MsgNoticeQueueQueryVO request, string AppId, DBEnum db = DBEnum.BaseDb);

        /// <summary>
        /// truy vấn hàng đợi tin nhắn
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<MsgNoticeQueueQueryRspoVO>>> NoticeQueryPage(OLPagination<MsgNoticeQueueQueryVO> request, string AppId);
        #endregion
    }
}
