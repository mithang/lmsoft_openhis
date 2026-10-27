namespace NewtouchHIS.Lib.Base
{
    //Giao diện đánh dấu dùng để định nghĩa ba loại vòng đời này

    public interface IDependency
    {
    }

    /// <summary>
    /// Nhất thời (mỗi lần đều tạo thể hiện mới)
    /// </summary>
    public interface ITransientDependency : IDependency
    {
    }

    /// <summary>
    /// Đơn lệ (duy nhất toàn cục)
    /// </summary>
    public interface ISingletonDependency : IDependency
    {

    }

    /// <summary>
    /// Duy nhất trong một yêu cầu (duy nhất trong luồng)
    /// </summary>
    public interface IScopedDependency : IDependency
    {
    }
}
