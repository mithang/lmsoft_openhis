using Mapster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;
using System.Data.Common;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.DomainService
{
    public class MsgQueueDmnService : BaseDmnService<MsgNoticeQueueEntity>, IMsgQueueDmnService
    {
        #region Quản lý nhóm thông báo
        public async Task<string> AddNoticeGroup(MsgNoticeGroupBasicVO request)
        {
            MsgNoticeGroupVO vo = request.Adapt<MsgNoticeGroupVO>();
            vo.NewEntity(request.OrganizeId ?? throw new FailedException("Tổ chứcId không được để trống"), request.CreatorCode ?? throw new FailedException("Thông tin người vận hành không được để trống"));
            vo.Id = string.IsNullOrWhiteSpace(vo.Id) ? Guid.NewGuid().ToString() : vo.Id;
            if (!Enum.IsDefined((GroupYwlxEnum)vo.GroupYwlx))
            {
                throw new FailedException("Loại nghiệp vụ[GroupYwlx]Lấy giá trị bất thường");
            }
            if (!Enum.IsDefined((MsgNoticeRangeEnum)vo.NoticeRange))
            {
                throw new FailedException("Thông tin đối tượng tin nhắn [NoticeRange] lấy giá trị bất thường");
            }
            var exists = await GetFirstOrDefaultWithAttr<MsgNoticeGroupEntity>(p => p.OrganizeId == vo.OrganizeId && p.zt == "1"
            && (p.GroupTag == vo.GroupTag || p.GroupName == vo.GroupName));
            if (exists != null)
            {
                throw new FailedException(exists.GroupTag == vo.GroupTag ? "Nhãn nhóm đã được đăng ký" : "Tên nhóm đã được đăng ký");
            }
            string errorMsg = string.Empty;
            if (!ValidRequired(vo, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            var entity = vo.Adapt<MsgNoticeGroupEntity>();
            var result = await AddWithAttr(entity);
            if (result > 0)
            {
                return vo.Id;
            }
            return null;
        }

        public async Task<bool> ModifyNoticeGroup(MsgNoticeGroupBasicVO request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new FailedException("Nhóm tin nhắnId không được để trống");
            }
            var entity = await FindKeyWithAttr<MsgNoticeGroupEntity>(request.Id);
            if (entity == null)
            {
                throw new FailedException("Không tìm thấy nhóm tin nhắn");
            }
            if (request.GroupYwlx != null && !Enum.IsDefined((GroupYwlxEnum)request.GroupYwlx))
            {
                throw new FailedException("Loại nghiệp vụ[GroupYwlx]Lấy giá trị bất thường");
            }
            if (request.NoticeRange != null && !Enum.IsDefined((MsgNoticeRangeEnum)request.NoticeRange))
            {
                throw new FailedException("Thông tin đối tượng tin nhắn [NoticeRange] lấy giá trị bất thường");
            }
            if (!string.IsNullOrWhiteSpace(request.GroupTag) || !string.IsNullOrWhiteSpace(request.GroupName))
            {
                var exists = await GetFirstOrDefaultWithAttr<MsgNoticeGroupEntity>(p => p.OrganizeId == request.OrganizeId && p.zt == "1"
                    && (p.GroupTag == request.GroupTag || p.GroupName == request.GroupName) && p.Id != entity.Id);
                if (exists != null)
                {
                    throw new FailedException(exists.GroupTag == request.GroupTag ? "Nhãn nhóm đã được đăng ký" : "Tên nhóm đã được đăng ký");
                }
            }

            entity.ModifiedEntity(request.OrganizeId ?? throw new FailedException("Tổ chứcId không được để trống"), request.LastModifierCode ?? throw new FailedException("Thông tin người vận hành không được để trống"));
            entity.GroupYwlx = request.GroupYwlx != null ? (int)request.GroupYwlx : entity.GroupYwlx;
            entity.NoticeRange = request.NoticeRange != null ? (int)request.NoticeRange : entity.NoticeRange;
            entity.GroupDesc = string.IsNullOrWhiteSpace(request.GroupDesc) ? entity.GroupDesc : request.GroupDesc;
            entity.DefaultTitle = string.IsNullOrWhiteSpace(request.DefaultTitle) ? entity.DefaultTitle : request.DefaultTitle;
            entity.DefaultContent = string.IsNullOrWhiteSpace(request.DefaultContent) ? entity.DefaultContent : request.DefaultContent;
            entity.DefaultContentData = string.IsNullOrWhiteSpace(request.DefaultContentData) ? entity.DefaultContentData : request.DefaultContentData;
            entity.DefaultOpenPath = string.IsNullOrWhiteSpace(request.DefaultOpenPath) ? entity.DefaultOpenPath : request.DefaultOpenPath;
            entity.DefaultOpenPathPara = string.IsNullOrWhiteSpace(request.DefaultOpenPathPara) ? entity.DefaultOpenPathPara : request.DefaultOpenPathPara;
            return await UpdateWithAttr(entity);
        }

        public async Task<List<MsgNoticeGroupVO>> NoticeGroupQuery(MsgNoticeGroupBasicVO request)
        {
            var where = AssembledWhereEqual<MsgNoticeGroupEntity, MsgNoticeGroupBasicVO>(request);
            var data = await GetByWhereWithAttr(where);
            return data.Adapt<List<MsgNoticeGroupVO>>();
        }
        #endregion

        #region Hàng đợi thông báo
        public async Task<List<string>> AddNotice(MsgNoticeQueueBasicVO request)
        {
            string errorMsg = string.Empty;
            List<string> resultIds = new List<string>();
            if (string.IsNullOrWhiteSpace(request.NoticeGroupId))
            {
                throw new FailedException("Nhóm tin nhắnId không được để trống");
            }
            var group = await FindKeyWithAttr<MsgNoticeGroupEntity>(request.NoticeGroupId);
            if (group == null || group.zt != "1" || group.OrganizeId != request.OrganizeId)
            {
                throw new FailedException("Thông tin Id nhóm tin nhắn bất thường hoặc không nhất quán với thông tin tổ chức được yêu cầu");
            }
            request.GroupYwlx = group.GroupYwlx;
            request.NoticeRange = group.NoticeRange;
            request.NoticeId = Guid.NewGuid().ToString().Replace("-", string.Empty);

            MsgNoticeQueueVO vo = request.Adapt<MsgNoticeQueueVO>();
            vo.NewEntity(request.OrganizeId ?? throw new FailedException("Tổ chứcId không được để trống"), request.CreatorCode ?? throw new FailedException("Thông tin người vận hành không được để trống"));
            vo.Id = string.IsNullOrWhiteSpace(vo.Id) ? Guid.NewGuid().ToString() : vo.Id;
            vo.QueueExecType = vo.QueueExecType == 0 ? (int)MsgQueueExecTypeEnum.Immediately : vo.QueueExecType;
            vo.RecipientType = vo.RecipientType == 0 ? (int)RecipientTypeEnum.user : vo.RecipientType;
            vo.NoticeStu = (int)NoticeStuEnum.Wait;
            vo.px = 0;
            Dictionary<Type, int> enums = new Dictionary<Type, int>
            {
                { typeof(GroupYwlxEnum), vo.GroupYwlx },
                { typeof(MsgQueueExecTypeEnum), vo.QueueExecType },
                { typeof(RecipientTypeEnum), vo.RecipientType },
                { typeof(MsgNoticeRangeEnum), vo.NoticeRange }
            };
            if (!ValidEnum(enums, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            if (!ValidRequired(vo, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            int limitLenOnce = 100;
            if (request.Recipient?.Length > limitLenOnce)
            {
                //Chia lần
                var recips = request.Recipient.Split(',').Distinct().ToList();
                int maxStr = recips.Max(p => p.Length);
                int remainder = 0;
                //Số người nhận có thể gửi mỗi lần
                int point = Math.DivRem(limitLenOnce, maxStr, out remainder);
                point += remainder > 0 ? 1 : 0;
                //Số lần gửi Cần chia thành mấy bản tin
                remainder = 0;
                int cnt = Math.DivRem(recips.Count, point, out remainder);
                cnt += remainder > 0 ? 1 : 0;
                for (int i = 0; i < cnt; i++)
                {
                    int beginNum = i * point;
                    var copy = recips;
                    int endNum = (i + 1) * point;
                    int lastNum = copy.Count() - i * point;
                    var userList = copy.GetRange(beginNum, endNum > copy.Count ? (copy.Count() - i * point) : endNum);

                    var ientity = vo.Adapt<MsgNoticeQueueEntity>();
                    ientity.Recipient = string.Join(",", userList);
                    ientity.Id = Guid.NewGuid().ToString();
                    var iresult = await AddWithAttr(ientity);
                    if (iresult == 0)
                    {
                        errorMsg += $"Tại lần thứ{i}gửi thông báo xếp hàng thất bại,Vui lòng thử lại!";
                    }
                    resultIds.Add(ientity.Id);
                }
                return resultIds;
            }
            else
            {
                var entity = vo.Adapt<MsgNoticeQueueEntity>();
                var result = await AddWithAttr(entity);
                if (result > 0)
                {
                    return new List<string>() { vo.Id };
                }
            }
            return resultIds;
        }
        public async Task<bool> NoticeStuModify(string[] queueIds, string user, NoticeStuEnum noticeStu, bool procSuccess = true)
        {
            var noticeList = await GetByWhere(p => queueIds.Contains(p.Id) && p.zt == "1");
            if (noticeList != null && noticeList.Count > 0)
            {
                List<MsgNoticeQueueEntity> list = new List<MsgNoticeQueueEntity>();
                foreach (var notice in noticeList)
                {
                    bool isModified = true;
                    switch (noticeStu)
                    {
                        case NoticeStuEnum.Wait:
                            if (notice.NoticeStu != (int)NoticeStuEnum.Wait)
                            {
                                //throw new FailedException("Trạng thái tin nhắn đã được cập nhật,Cập nhật trạng thái thất bại");
                                isModified = false;
                            }
                            break;
                        case NoticeStuEnum.UnSend:
                            if (notice.NoticeStu == (int)NoticeStuEnum.Read || notice.NoticeStu != (int)NoticeStuEnum.Send)
                            {
                                //throw new FailedException("Trạng thái tin nhắn đã được cập nhật,Cập nhật trạng thái thất bại");
                                isModified = false;
                            }
                            break;
                        case NoticeStuEnum.Send:
                            if (notice.NoticeStu == (int)NoticeStuEnum.Read)
                            {
                                //throw new FailedException("Tin nhắn đã đọc,Cập nhật trạng thái thất bại");
                                isModified = false;
                            }
                            break;
                        case NoticeStuEnum.Read:
                            if (notice.NoticeStu == (int)NoticeStuEnum.Read)
                            {
                                //throw new FailedException("Tin nhắn đã đọc,Cập nhật trạng thái thất bại");
                                isModified = false;
                            }
                            break;
                    }
                    if (isModified)
                    {
                        notice.NoticeStu = (int)noticeStu;
                        notice.ModifiedEntity(notice.OrganizeId, user, false);
                        if (!procSuccess)
                        {
                            notice.px += 1;
                        }
                        else
                        {
                            notice.px = 0;
                        }
                        list.Add(notice);
                    }
                }
                return await UpdateRangeAsync(noticeList);
            }
            return false;
        }

        /// <summary>
        /// Cập nhậtTin nhắnTrạng thái xử lý 
        /// Mỗi lần xử lý thất bại thì px +1
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="user"></param>
        /// <param name="noticeStu"></param>
        /// <param name="px"></param>
        /// <returns></returns>
        public async Task<bool> NoticeStuModify(string queueId, string user, NoticeStuEnum noticeStu, bool procSuccess = true)
        {
            var notice = await FindKey(queueId);
            if (notice != null)
            {
                switch (noticeStu)
                {
                    case NoticeStuEnum.Wait:
                        if (notice.NoticeStu != (int)NoticeStuEnum.Wait)
                        {
                            throw new FailedException("Trạng thái tin nhắn đã được cập nhật,Cập nhật trạng thái thất bại");
                        }
                        break;
                    case NoticeStuEnum.UnSend:
                        if (notice.NoticeStu == (int)NoticeStuEnum.Read || notice.NoticeStu != (int)NoticeStuEnum.Send)
                        {
                            throw new FailedException("Trạng thái tin nhắn đã được cập nhật,Cập nhật trạng thái thất bại");
                        }
                        break;
                    case NoticeStuEnum.Send:
                        if (notice.NoticeStu == (int)NoticeStuEnum.Read)
                        {
                            throw new FailedException("Tin nhắn đã đọc,Cập nhật trạng thái thất bại");
                        }
                        break;
                    case NoticeStuEnum.Read:
                        break;
                }
                notice.NoticeStu = (int)noticeStu;
                notice.ModifiedEntity(notice.OrganizeId, user, false);
                if (!procSuccess)
                {
                    notice.px += 1;
                }
                else
                {
                    notice.px = 0;
                }
                return await UpdateAsync(notice);
            }
            return false;
        }
        public async Task<bool> NoticeQueueInfoModify(MsgNoticeQueueVO ety)
        {
            var notice = await FindKey(ety.Id);
            if (notice != null && !string.IsNullOrWhiteSpace(ety.LastModifierCode))
            {
                notice.Title = ety.Title ?? notice.Title;
                notice.Content = ety.Content ?? notice.Content;
                notice.ContentData = ety.ContentData ?? notice.ContentData;
                notice.px = ety.px;
                notice.ModifiedEntity(notice.OrganizeId, ety.LastModifierCode, false);
                return await UpdateAsync(notice);
            }
            return false;
        }





        #endregion


        #region Query
        public async Task<List<MsgNoticeQueueVO>> NoticeInfoQuery(MsgNoticeQueueBasicVO request)
        {
            var where = AssembledWhereEqual<MsgNoticeQueueEntity, MsgNoticeQueueBasicVO>(request);
            var data = await GetByWhereWithAttr(where);
            return data.Adapt<List<MsgNoticeQueueVO>>();
        }


        public async Task<List<MsgNoticeQueueModelVO<MrqcMsgNoticeModel>>> MrqcNoticeQuery(MsgNoticeQueueQueryVO request)
        {
            return await NoticeQueueOfModelQuery<MrqcMsgNoticeModel>(request, sysConfig.AppAPIHostName?.SiteMrqcWebHost ?? "MRQC");
        }
        public async Task<List<MsgNoticeQueueModelVO<T>>> NoticeQueueOfModelQuery<T>(MsgNoticeQueueQueryVO request, string AppId) where T : MsgNoticeModel
        {
            //Tạo biểu thức
            var whereExp = Expressionable.Create<MsgNoticeQueueEntity, MsgNoticeGroupEntity>()
                .And((a, b) => a.NoticeGroupId == b.Id && a.OrganizeId == b.OrganizeId && a.zt == "1" && b.zt == "1" && a.OrganizeId == request.OrganizeId);
            if (!string.IsNullOrWhiteSpace(AppId))
            {
                whereExp.And((a, b) => b.AppId == AppId);
            }
            if (request.ksrq != null && request.jsrq != null)
            {
                whereExp.And((a, b) => a.CreateTime >= request.ksrq && a.CreateTime <= request.jsrq);
            }
            if (!string.IsNullOrWhiteSpace(request.SendFrom))
            {
                whereExp.And((a, b) => a.SendFrom == request.SendFrom);
            }

            if (!string.IsNullOrWhiteSpace(request.Recipient))
            {
                whereExp.And((a, b) => a.RecipientType == (int)RecipientTypeEnum.user ? a.Recipient == request.Recipient : a.Recipient.Contains(request.Recipient));
            }
            var list = await GetJoinList(
                (a, b) => new JoinQueryInfos(JoinType.Inner, a.OrganizeId == b.OrganizeId && a.NoticeGroupId == b.Id),
                (a, b) => new MsgNoticeQueueModelVO<T>
                {
                    GroupTag = b.GroupTag,
                    GroupName = b.GroupName,
                    GroupDesc = b.GroupDesc ?? "",
                    Title = a.Title,
                    Content = a.Content,
                    ContentData = a.ContentData,
                    OpenPath = a.OpenPath,
                    OpenPathPara = a.OpenPathPara,
                    SendFrom = a.SendFrom,
                    NoticeGroupId = a.NoticeGroupId,
                    GroupYwlx = a.GroupYwlx,
                    NoticeRange = a.NoticeRange,
                    QueueExecType = a.QueueExecType,
                    ExecCron = a.ExecCron,
                    RecipientType = a.RecipientType,
                    Recipient = a.Recipient,
                    NoticeStu = a.NoticeStu,
                    CreateTime = a.CreateTime,
                    LastModifyTime = a.LastModifyTime,
                    CreatorCode = a.CreatorCode,
                    LastModifierCode = a.LastModifierCode,
                    AppId = a.AppId,
                    NoticeData = !string.IsNullOrWhiteSpace(a.ContentData) ? JsonConvert.DeserializeObject<T>(a.ContentData) : null,
                }, true, whereExp.ToExpression());
            return list.Adapt<List<MsgNoticeQueueModelVO<T>>>();
        }
        public async Task<PageResponseRow<List<MsgNoticeQueueQueryRspoVO>>> NoticeQueryPage(OLPagination<MsgNoticeQueueQueryVO> requestPage, string AppId)
        {
            var resp = new PageResponseRow<List<MsgNoticeQueueQueryRspoVO>>();
            var request = requestPage.queryParams;
            //Tạo biểu thức
            var whereExp = Expressionable.Create<MsgNoticeQueueEntity, MsgNoticeGroupEntity, SysAuthAppEntity>()
                .And((a, b, c) => a.NoticeGroupId == b.Id && a.OrganizeId == b.OrganizeId && a.zt == "1" && b.zt == "1" && a.OrganizeId == request.OrganizeId);
            if (!string.IsNullOrWhiteSpace(AppId))
            {
                whereExp.And((a, b, c) => b.AppId == AppId);
            }
            if (request?.ksrq != null && request?.jsrq != null)
            {
                whereExp.And((a, b, c) => a.CreateTime >= request.ksrq && a.CreateTime <= request.jsrq);
            }
            if (request?.NoticeStu != null)
            {
                whereExp.And((a, b, c) => a.NoticeStu == request.NoticeStu);
            }
            if (!string.IsNullOrWhiteSpace(request?.SendFrom))
            {
                whereExp.And((a, b, c) => a.SendFrom == request.SendFrom);
            }
            if (!string.IsNullOrWhiteSpace(request?.Recipient))
            {
                // FIX-1 (A-01): SqlSugar cannot translate the ternary (SqlFunc.IIF +
                // Contains) used here before, so msgtype=1 (inbox) always errored.
                // Rewritten as an OR of two AND-branches with identical semantics:
                // user recipients match by equality, other recipient types by Contains.
                // RecipientType is a non-nullable int, so the two branches partition
                // all rows exactly like the ternary did.
                whereExp.And((a, b, c) => (a.RecipientType == (int)RecipientTypeEnum.user && a.Recipient == request.Recipient)
                    || (a.RecipientType != (int)RecipientTypeEnum.user && a.Recipient.Contains(request.Recipient)));
            }
            if (!string.IsNullOrWhiteSpace(request?.keyword) && !string.IsNullOrWhiteSpace(request?.SendFrom))
            {
                whereExp.And((a, b, c) => a.Recipient.Contains(request.keyword) || a.ContentData.Contains(request.keyword));
            }
            else if (!string.IsNullOrWhiteSpace(request?.keyword) && !string.IsNullOrWhiteSpace(request?.Recipient))
            {
                whereExp.And((a, b, c) => a.SendFrom.Contains(request.keyword) || a.ContentData.Contains(request.keyword));
            }
            else if (!string.IsNullOrWhiteSpace(request?.keyword))
            {
                whereExp.And((a, b, c) => a.ContentData.Contains(request.keyword));
            }
            var pageData = await GetJoinPageList(requestPage.offset, requestPage.limit,
                (a, b, c) => new JoinQueryInfos(JoinType.Inner, a.OrganizeId == b.OrganizeId && a.NoticeGroupId == b.Id,
                                                JoinType.Left, a.AppId == c.AppId && c.zt == "1"),
                (a, b, c) => new MsgNoticeQueueQueryRspoVO
                {
                    Id= a.Id,
                    GroupTag = b.GroupTag,
                    GroupName = b.GroupName,
                    GroupDesc = b.GroupDesc ?? "",
                    Title = a.Title,
                    Content = a.Content,
                    ContentData = a.ContentData,
                    OpenPath = a.OpenPath,
                    OpenPathPara = a.OpenPathPara,
                    SendFrom = a.SendFrom,
                    NoticeGroupId = a.NoticeGroupId,
                    GroupYwlx = a.GroupYwlx,
                    NoticeRange = a.NoticeRange,
                    QueueExecType = a.QueueExecType,
                    ExecCron = a.ExecCron,
                    RecipientType = a.RecipientType,
                    Recipient = a.Recipient,
                    NoticeStu = a.NoticeStu,
                    CreateTime = a.CreateTime,
                    LastModifyTime = a.LastModifyTime,
                    CreatorCode = a.CreatorCode,
                    LastModifierCode = a.LastModifierCode,
                    AppName = c.AppName,
                    AppId = a.AppId,
                }, true, whereExp.ToExpression(), true, (a, b, c) => new { a.NoticeStu, a.CreateTime }, OrderByType.Desc);
            if (pageData.rows != null && pageData.rows.Count > 0)
            {
                var user = pageData.rows.Select(p => p.SendFrom).Distinct().ToList();
                var recipUser = pageData.rows.Select(p => p.Recipient).Distinct().ToList();
                if (recipUser.Count > 0)
                {
                    user.AddRange(recipUser);
                }
                var staff = await GetByWhere<SysStaffEntity>(p => user.Contains(p.gh) && p.OrganizeId == requestPage.queryParams.OrganizeId && p.zt == "1");
                pageData.rows = (from a in pageData.rows
                                 join b in staff on a.SendFrom equals b.gh
                                 join c in staff on a.Recipient equals c.gh
                                 select new MsgNoticeQueueQueryRspoVO
                                 {
                                     Id = a.Id,
                                     GroupTag = a.GroupTag,
                                     GroupName = a.GroupName,
                                     GroupDesc = a.GroupDesc,
                                     Title = a.Title,
                                     Content = a.Content,
                                     ContentData = a.ContentData,
                                     OpenPath = a.OpenPath,
                                     OpenPathPara = a.OpenPathPara,
                                     SendFrom = a.SendFrom,
                                     NoticeGroupId = a.NoticeGroupId,
                                     GroupYwlx = a.GroupYwlx,
                                     NoticeRange = a.NoticeRange,
                                     QueueExecType = a.QueueExecType,
                                     ExecCron = a.ExecCron,
                                     RecipientType = a.RecipientType,
                                     Recipient = a.Recipient,
                                     NoticeStu = a.NoticeStu,
                                     CreateTime = a.CreateTime,
                                     LastModifyTime = a.LastModifyTime,
                                     CreatorCode = a.CreatorCode,
                                     LastModifierCode = a.LastModifierCode,
                                     AppName = a.AppName,
                                     AppId = a.AppId,
                                     SendFromName = b.Name,
                                     RecipientName = c.Name
                                 }).ToList();
            }
            return pageData;
        }

        /// <summary>
        /// LấyTin nhắnDanh sách thông báo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="AppId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<MsgNoticeQueueQueryRspoVO>> NoticeQuery(MsgNoticeQueueQueryVO request, string AppId, DBEnum db = DBEnum.BaseDb)
        {
            string sqlstr = @"select b.GroupTag,b.GroupName,b.GroupDesc, [Title], [Content],[ContentData], [OpenPath], [OpenPathPara], [SendFrom], [NoticeGroupId], 
a.[GroupYwlx], a.[NoticeRange], [QueueExecType], [ExecCron], [RecipientType], [Recipient], [NoticeStu],b.CreateTime
from [Msg_NoticeQueue] a with(nolock)
join [Msg_NoticeGroup] b with(nolock) on b.Id=a.NoticeGroupId and b.OrganizeId=a.OrganizeId
where a.ZT='1' AND a.OrganizeId=@OrganizeId  AND a.AppId=@AppId  AND a.CreateTime>=@ksrq AND a.CreateTime<=@jsrq
";
            if (!string.IsNullOrWhiteSpace(request.NoticeStu.ToString()))
                sqlstr += " AND a.NoticeStu=@NoticeStu";

            var zkData = await GetListBySqlQuery<MsgNoticeQueueQueryRspoVO>(db.ToString(), sqlstr, new List<DbParameter> {
                new SqlParameter("@OrganizeId",request.OrganizeId),
                new SqlParameter("@AppId",AppId),
                new SqlParameter("@ksrq",request.ksrq),
                new SqlParameter("@jsrq",request.jsrq),
                new SqlParameter("@NoticeStu",request.NoticeStu),
            });
            return zkData;
        }
        #endregion

        #region private


        #endregion
    }
}
