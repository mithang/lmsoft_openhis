namespace NewtouchHIS.Base.Domain.Model
{
    /// <summary>
    /// Chọn thả xuốngModel
    /// (select Điều khiển)
    /// </summary>
    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }
    /// <summary>
    /// treeview Model Không cóNodesLớp cơ sở
    /// </summary>
    public class BsTreeSelectBaseModel
    {
        public string text { get; set; }
        public string? href { get; set; }
        public string? color { get; set; }
        public string? backColor { get; set; }
        public string? icon { get; set; }
        public string[]? tags { get; set; }
        public bool? selected { get; set; } = false;
    }
    /// <summary>
    /// treeview Model Gốc
    /// </summary>
    public class BsTreeSelectModel: BsTreeSelectBaseModel
    {        
        public List<BsTreeSelectModel>? nodes { get; set; }
    }
    /// <summary>
    /// treeview Model Trường mở rộng
    /// Dùng để xử lý dữ liệu đặc biệt Đăng nhập liên kết nghiệp vụ
    /// </summary>
    public class BsTreeSelectExtDataModel : BsTreeSelectBaseModel
    {
        public List<BsTreeSelectExtDataModel>? nodes { get; set; }
        public string Id { get; set; }
        public string? ParentId { get; set; }
        public int? LevelId { get; set; }
        public List<BsTreeSelectExtModel>? ext { get; set; }
        public BsTreeNodeStuModel? state { get; set; }
    }
    public class BsTreeSelectExtModel
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Desc { get; set; }
    }

    public class BsTreeNodeStuModel
    {
        public bool? _checked { get; set; } = false;
        public bool? selected { get; set; } = false;
        public bool? disabled { get; set; } = false;
        public bool? expanded { get; set; } = false;
    }
    public class TreeSelectCModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<TreeSelectCModel>? children { get; set; }
    }
}
