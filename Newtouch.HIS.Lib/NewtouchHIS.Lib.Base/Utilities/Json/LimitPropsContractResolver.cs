/*******************************************************************************
 * Copyright © 2023 Newtouch giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace NewtouchHIS.Lib.Base.Utilities.Json
{
    /// <summary>
    /// phân tích nội dung
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] _retainProps = null;
        string[] _excludeProps = null;

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="retainProps">trường cần giữ lại</param>
        public LimitPropsContractResolver(string[] retainProps)
        {
            //danh sách các thuộc tính cần tuần tự hóa
            this._retainProps = retainProps;
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="retainProps">trường cần giữ lại</param>
        /// <param name="excludeProps">trường cần loại trừ</param>
        public LimitPropsContractResolver(string[] retainProps, string[] excludeProps)
        {
            //danh sách các thuộc tính cần tuần tự hóa
            this._retainProps = retainProps;

            this._excludeProps = excludeProps;
        }

        /// <summary>
        /// ghi đè
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            return list.Where(p =>
            {
                return (_retainProps == null || _retainProps.Length == 0 || _retainProps.Contains(p.PropertyName))            //chỉ giữ lại các thuộc tính có trong danh sách
                    && (_excludeProps == null || _excludeProps.Length == 0 || !_excludeProps.Contains(p.PropertyName))
                    ;
            }).ToList();
        }
    }
}
