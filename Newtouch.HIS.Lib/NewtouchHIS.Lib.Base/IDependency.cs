namespace NewtouchHIS.Lib.Base
{
    //dùng để định nghĩa giao diện đánh dấu ba vòng đời này

    public interface IDependency
    {
    }

    /// <summary>
    /// tức thời（mỗi lần đều tạo thể hiện mới）
    /// </summary>
    public interface ITransientDependency : IDependency
    {
    }

    /// <summary>
    /// đơn lẻ（duy nhất toàn cục）
    /// </summary>
    public interface ISingletonDependency : IDependency
    {

    }

    /// <summary>
    /// duy nhất trong một yêu cầu（duy nhất trong luồng）
    /// </summary>
    public interface IScopedDependency : IDependency
    {
    }
}
