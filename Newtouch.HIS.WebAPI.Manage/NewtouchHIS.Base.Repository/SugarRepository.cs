using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Base.Repository
{
    /// <summary>
    /// SqlSugar kho đơn thể+nhiều cơ sở dữ liệu
    /// </summary>
    public class SugarRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        protected ITenant dbTran => SqlSugarDbContext.Db;//giao dịch nhiều khách thuê、GetConnection、IsAnyConnectioncác chức năng khác
        public SugarRepository()
        {
            //chế độ đơn thể
            base.Context = SqlSugarDbContext.Db.GetConnectionScopeWithAttr<TEntity>();//lấy con theo đặc tính thực thểDb
        }

        public SugarRepository(string db)
        {
            //chế độ đơn thể
            base.Context = SqlSugarDbContext.Db.GetConnection(db);//lấy con theo đặc tính thực thểDb
        }

        /// <summary>
        /// phương thức mở rộng，khi phương thức có sẵn không đáp ứng được thì có thể thêm phương thức mới
        /// </summary>
        /// <returns></returns>
        public List<TEntity> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();có thể lấy đượcSqlSugarClient thực hiện thao tác phức tạp
            return null;
        }
        #region 主库不一致实体表操作
        /// <summary>
        /// lời gọi thư viện chương trình hiện tại
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Insertable<T>(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        /// <summary>
        /// lời gọi thư viện chương trình hiện tại
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Delete<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Deleteable<T>(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        public async Task<bool> Update<T>(T model) where T : class, new()
        {
            //phương thức này sẽ lấy khóa chính làm điều kiện
            var i = await Task.Run(() => Context.Updateable<T>(model).ExecuteCommand());
            return i > 0;
        }
        #endregion
        #region 主库一致实体表操作
        /// <summary>
        /// ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(TEntity model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        public async Task<long> AddRange(List<TEntity> model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        /// <summary>
        /// theoIDxóa danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// theoIDxóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            //phương thức này sẽ lấy khóa chính làm điều kiện
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// cập nhật hàng loạt dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<TEntity> model)
        {
            //phương thức này sẽ lấy khóa chính làm điều kiện
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        #region Query
        /// <summary>
        /// truy vấn theo khóa chính
        /// </summary>
        /// <param name="pkValue">khóa chính</param>
        /// <returns></returns>
        public async Task<TEntity> FindKey(object pkValue)
        {
            return await Context.Queryable<TEntity>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll()
        {
            return await Task.Run(() => Context.Queryable<TEntity>().ToListAsync());
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy đầu num bản ghi dữ liệu
        /// </summary>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// lấy một bảng dữ liệu phân trang
        /// </summary>
        /// <param name="skip">bỏ qua vài bản ghi</param>
        /// <param name="take">lấy vài bản ghi</param>
        /// <param name="whereExp">bỏ qua vài bản ghi</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// lấy các bản ghi đầu thỏa điều kiện num bản ghi dữ liệu
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <param name="num">lấy vài bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<TEntity>> GetTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// lấy theo điều kiện một bản ghi dữ liệu 
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().FirstAsync(where);//truy vấn một bản ghi, không có dữ liệu trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi，Firstkhông
        }

        /// <summary>
        /// theo khóa chính In  truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của khóa chính</typeparam>
        /// <param name="list">khóa chính In tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định In truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu của cột chỉ định</typeparam>
        /// <param name="column">cột chỉ định</param>
        /// <param name="list">cột chỉ định In thao tác tập kết quả của</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo cột chỉ định Not In (!Contain)truy vấn
        /// </summary>
        /// <typeparam name="S">kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả của</param>
        /// <param name="field">cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// theo điều kiện truy vấn
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// Where truy vấn bảng cùng cơ sở dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhere<T>(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// một điều kiện đơn theo isWhere phán đoán có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">điều kiện phán đoán có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// nhiều điều kiện theo wheres.value  phán đoán có sử dụng hay không này wheres.key điều kiện của
        /// </summary>
        /// <param name="wheres">điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhereIF(Dictionary<Expression<Func<TEntity, bool>>, bool> wheres, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            var able = Context.Queryable<TEntity>();
            foreach (var item in wheres)
            {
                able.WhereIF(item.Value, item.Key);
            }
            return await able.OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// truy vấn giá trị của cột chỉ định trong startđếnend dữ liệu giữa
        /// </summary>
        /// <param name="value">lớp chỉ định</param>
        /// <param name="start">bắt đầu</param>
        /// <param name="end">kết thúc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// phán đoán bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<bool> GetIsAny(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().AnyAsync(where);
        }

        /// <summary>
        /// truy vấn hai bảng，
        /// </summary>
        /// <typeparam name="T1">bảng trái</typeparam>
        /// <typeparam name="T2">bảng phải</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">có cần sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }

        /// <summary>
        /// kết nối ba bảng truy vấn
        /// </summary>
        /// <typeparam name="T1">bảng1</typeparam>
        /// <typeparam name="T2">bảng2</typeparam>
        /// <typeparam name="T3">bảng3</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">điều kiện truy vấn</param>
        /// <param name="isOrderBy">có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<dynamic> GetJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

       

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh ，trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySql(string sql)
        {
            return await Context.SqlQueryable<TEntity>(sql).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.GetScalarAsync(sql, parameters);
        }

        #endregion

        #endregion
        #endregion

        #region 根据表 DB Attr特性查询 适用于与主库同库非主体实体表

        #region Add Update Delete
        /// <summary>
        /// [DB Attr]ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> AddWithAttr<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Insertable(model).ExecuteCommand());
            //được trả vềiCólongloại,Ở đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }

        /// <summary>
        /// [DB Attr]cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWithAttr<T>(T model) where T : class, new()
        {
            //phương thức này sẽ lấy khóa chính làm điều kiện
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// [DB Attr]theoIDxóa danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsWithAttr<T>(object[] ids) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Deleteable<T>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// [DB Attr]theoIDxóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdWithAttr<T>(object id) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Deleteable<T>().In(id).ExecuteCommand());
            return i > 0;
        }
        #endregion

        /// <summary>
        /// truy vấn theo khóa chính
        /// </summary>
        /// <param name="pkValue">khóa chính</param>
        /// <returns></returns>
        public async Task<T> FindKeyWithAttr<T>(object pkValue)
        {
            return await Context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>()
        {
            return await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().ToListAsync());
        }

        /// <summary>
        /// truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// [DB Attr]lấy theo điều kiện một bản ghi dữ liệu 
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<T> GetFirstOrDefaultWithAttr<T>(Expression<Func<T, bool>> where)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().Where(where).FirstAsync();//truy vấn một bản ghi, không có dữ liệu trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi，Firstkhông
        }
        /// <summary>
        ///  theo điều kiện truy vấn（theo T củaDb truy vấn đặc tính）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="isOrderBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhereWithAttr<T>(Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// truy vấn hai bảng，(theo T1 củaDb truy vấn đặc tính)
        /// </summary>
        /// <typeparam name="T1">bảng trái</typeparam>
        /// <typeparam name="T2">bảng phải</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">có cần sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinListWithAttr<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        #endregion

        #region DbMuti Query 
        /// <summary>
        /// theo điều kiện truy vấn (choose db)
        /// </summary>
        /// <param name="where">điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }


        /// <summary>
        /// truy vấn hai bảng，(choose db)
        /// </summary>
        /// <typeparam name="T1">bảng trái</typeparam>
        /// <typeparam name="T2">bảng phải</typeparam>
        /// <param name="joinExp">phương thức liên bảng，trường liên bảng（quan hệ khóa chính – khóa ngoại）</param>
        /// <param name="selectExp">kết quả truy vấn liên bảng</param>
        /// <param name="isWhere">có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">có cần sắp xếp hay không</param>
        /// <param name="orderBy">điều kiện sắp xếp</param>
        /// <param name="orderByType">kiểu sắp xếp（Asc、Desc）</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinList<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        #region Ado 不推荐
        /// <summary>
        /// thực thi câu lệnh không phải truy vấnsqlcâu lệnh，trả về thao tác có thành công hay không(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về tập kết quả truy vấn(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<List<T>> GetListBySqlQuery<T>(string db, string sql, List<DbParameter> parameters)
        {
            List<SugarParameter> sugarPara = new List<SugarParameter>();
            parameters.ForEach(p =>
            {
                sugarPara.Add(new SugarParameter(p.ParameterName, p.Value));
            });
            return await dbTran.GetConnectionScope(db).Ado.SqlQueryAsync<T>(sql, sugarPara);
        }

        /// <summary>
        /// thực thi truy vấnsqlcâu lệnh，trả về hàng đầu tiên, cột đầu tiên(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">tham số hóa</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.GetScalarAsync(sql, parameters);
        }

        #endregion
        #endregion

        
    }
}
