/*******************************************************************************
 * Copyright © 2023 Newtouch. Bảo lưu mọi quyền.
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace NewtouchHIS.Lib.Base.Utilities.Json
{
    /// <summary>
    /// Phân tích nội dung
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] _retainProps = null;
        string[] _excludeProps = null;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="retainProps">Các trường cần giữ lại</param>
        public LimitPropsContractResolver(string[] retainProps)
        {
            //Danh sách các thuộc tính được chỉ định để tuần tự hóa
            this._retainProps = retainProps;
        }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="retainProps">Các trường cần giữ lại</param>
        /// <param name="excludeProps">Các trường cần loại trừ</param>
        public LimitPropsContractResolver(string[] retainProps, string[] excludeProps)
        {
            //Danh sách các thuộc tính được chỉ định để tuần tự hóa
            this._retainProps = retainProps;

            this._excludeProps = excludeProps;
        }

        /// <summary>
        /// Ghi đè
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
                return (_retainProps == null || _retainProps.Length == 0 || _retainProps.Contains(p.PropertyName))            //Chỉ giữ lại các thuộc tính được liệt kê trong danh sách
                    && (_excludeProps == null || _excludeProps.Length == 0 || !_excludeProps.Contains(p.PropertyName))
                    ;
            }).ToList();
        }
    }
}
