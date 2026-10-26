using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class ChatRoom
    {
        public int RoomId { get; set; }
        public string ApplyUser { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxMinuts { get; set; } = 30;
        public string[] Participant { get; set; }
        /// <summary>
        /// lấy số phòng họp mới
        /// </summary>
        /// <returns></returns>
        public double GetNewRoomId(string orgId)
        {
            var key = RedisKey.TreatedRoomOrg(orgId);
            return RedisHelper.IncrKey(key);
        }
    }
}
