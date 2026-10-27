namespace NewtouchHIS.Lib.Base.Extension
{
    public static class PdsMapperExtensions
    {
        public static TTo MapperTo<TTo>(this object src) where TTo : new()
        {
            if (src == null) return default(TTo);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TTo>(Newtonsoft.Json.JsonConvert.SerializeObject(src));
        }
        public static TTo MapperTo<TFrom, TTo>(this TFrom src) where TTo : new()
        {
            if (src == null) return default(TTo);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TTo>(Newtonsoft.Json.JsonConvert.SerializeObject(src));
        }
    }
}
