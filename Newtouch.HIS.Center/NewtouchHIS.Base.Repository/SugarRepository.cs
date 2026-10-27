using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Base.Repository
{
    /// <summary>
    /// SqlSugar Kho singleton+Đa cơ sở dữ liệu
    /// </summary>
    public class SugarRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        protected ITenant dbTran => SqlSugarDbContext.Db;//Transaction đa tenant,GetConnection,IsAnyConnectionvà các chức năng
        public SugarRepository()
        {
            //Mẫu singleton
            base.Context = SqlSugarDbContext.Db.GetConnectionScopeWithAttr<TEntity>();//Lấy cơ sở dữ liệu con theo đặc tính thực thểDb
        }

        public SugarRepository(string db)
        {
            //Mẫu singleton
            base.Context = SqlSugarDbContext.Db.GetConnection(db);//Lấy cơ sở dữ liệu con theo đặc tính thực thểDb
        }

        /// <summary>
        /// Phương thức mở rộng,Khi phương thức có sẵn không đáp ứng được thì có thể thêm phương thức mới
        /// </summary>
        /// <returns></returns>
        public List<TEntity> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();có thể lấy đượcSqlSugarClient để làm thao tác phức tạp
            return null;
        }
        #region Thao tác bảng thực thể không nhất quán với cơ sở dữ liệu chính
        /// <summary>
        /// Lời gọi thư viện chương trình hiện tại
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Insertable<T>(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        /// <summary>
        /// Lời gọi thư viện chương trình hiện tại
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Delete<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => Context.Deleteable<T>(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        public async Task<bool> Update<T>(T model) where T : class, new()
        {
            //Cách này sẽ lấyKhóa chínhlàm điều kiện
            var i = await Task.Run(() => Context.Updateable<T>(model).ExecuteCommand());
            return i > 0;
        }
        #endregion
        #region Thao tác bảng thực thể nhất quán với cơ sở dữ liệu chính
        /// <summary>
        /// Ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> Add(TEntity model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        public async Task<long> AddRange(List<TEntity> model)
        {
            var i = await Task.Run(() => Context.Insertable(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }
        /// <summary>
        /// TheoIDXóa theo danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// TheoIDXóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => Context.Deleteable<TEntity>().In(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// Cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity model)
        {
            //Cách này sẽ lấyKhóa chínhlàm điều kiện
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// Cập nhật hàng loạt dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRange(List<TEntity> model)
        {
            //Cách này sẽ lấyKhóa chínhlàm điều kiện
            var i = await Task.Run(() => Context.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        #region Query
        /// <summary>
        /// TheoKhóa chínhTruy vấn
        /// </summary>
        /// <param name="pkValue">Khóa chính</param>
        /// <returns></returns>
        public async Task<TEntity> FindKey(object pkValue)
        {
            return await Context.Queryable<TEntity>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll()
        {
            return await Task.Run(() => Context.Queryable<TEntity>().ToListAsync());
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> FindAll(bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy đầu num bản ghi
        /// </summary>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetTakeList(int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// Lấy dữ liệu một bảng Dữ liệu phân trang
        /// </summary>
        /// <param name="skip">Bỏ qua mấy bản ghi</param>
        /// <param name="take">Lấy mấy bản ghi</param>
        /// <param name="whereExp">Bỏ qua mấy bản ghi</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetPageList(int skip, int take, Expression<Func<TEntity, bool>> whereExp, Expression<Func<TEntity, object>> orderBy, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Skip(skip).Take(take).OrderBy(orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Lấy các bản ghi đầu thỏa điều kiện num bản ghi
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <param name="num">Lấy mấy bản ghi đầu</param>
        /// <returns></returns> 
        public async Task<List<TEntity>> GetTakeList(Expression<Func<TEntity, bool>> where, int num, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).Take(num).ToListAsync();
        }

        /// <summary>
        /// Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().FirstAsync(where);//Truy vấn một bản ghi, không có dữ liệu thì trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi,Firstsẽ không
        }

        /// <summary>
        /// TheoKhóa chính In  Truy vấn
        /// </summary>
        /// <typeparam name="S">Khóa chínhkiểu</typeparam>
        /// <param name="list">Khóa chính In Tập kết quả của thao tác</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định In Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu của cột chỉ định</typeparam>
        /// <param name="column">Cột chỉ định</param>
        /// <param name="list">Cột chỉ định In Thao tác tập kết quả</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByIn<S>(Expression<Func<TEntity, object>> column, List<S> list, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().In<S>(column, list).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo cột chỉ định Not In (!Contain)Truy vấn
        /// </summary>
        /// <typeparam name="S">Kiểu cột chỉ định</typeparam>
        /// <param name="list">Not Intập kết quả</param>
        /// <param name="field">Cột chỉ định</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByNotIn<S>(List<S> list, object field, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(t => !SqlFunc.ContainsArray(list, field)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Theo điều kiện Truy vấn
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhere(Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// Where Truy vấn bảng cùng cơ sở dữ liệu
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
        /// Một điều kiện Theo isWhere Xét Có sử dụng điều kiện này để truy vấn hay không
        /// </summary>
        /// <param name="isWhere">Điều kiện xét có sử dụng điều kiện truy vấn này hay không</param>
        /// <param name="where">Điều kiện truy vấn</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByWhereIF(bool isWhere, Expression<Func<TEntity, bool>> where, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().WhereIF(isWhere, where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Nhiều điều kiện Theo wheres.value  Xét có sử dụng hay không này wheres.key điều kiện
        /// </summary>
        /// <param name="wheres">Điều kiện truy vấn</param>
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
        /// Truy vấn Giá trị của cột chỉ định tại startđếnend dữ liệu trong khoảng
        /// </summary>
        /// <param name="value">Lớp chỉ định</param>
        /// <param name="start">Bắt đầu</param>
        /// <param name="end">Kết thúc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByBetween(object value, object start, object end, bool isOrderBy = false, Expression<Func<TEntity, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable<TEntity>().Where(it => SqlFunc.Between(value, start, end)).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }

        /// <summary>
        /// Xét bản ghi này có tồn tại hay không
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<bool> GetIsAny(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Queryable<TEntity>().AnyAsync(where);
        }

        /// <summary>
        /// Truy vấn hai bảng,
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinList<T1, T2>(Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {

            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy, orderByType).Select(selectExp).ToListAsync();
        }

        /// <summary>
        /// Nối ba bảng Truy vấn
        /// </summary>
        /// <typeparam name="T1">Bảng1</typeparam>
        /// <typeparam name="T2">Bảng2</typeparam>
        /// <typeparam name="T3">Bảng3</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Điều kiện truy vấn</param>
        /// <param name="isOrderBy">Có cần điều kiện sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<dynamic> GetJoinList<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, T3, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, T3, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await Context.Queryable(joinExp).WhereIF(isWhere, whereExp).OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

       

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh ,Trả về tập dữ liệu
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySql(string sql)
        {
            return await Context.SqlQueryable<TEntity>(sql).ToListAsync();
        }

        #region Ado 
        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListBySqlQuery(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.SqlQueryAsync<TEntity>(sql, parameters);
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string sql, params SugarParameter[] parameters)
        {
            return await Context.Ado.GetScalarAsync(sql, parameters);
        }

        #endregion

        #endregion
        #endregion

        #region Theo bảng DB AttrTruy vấn theo đặc tính Áp dụng cho bảng thực thể không chủ thể cùng cơ sở dữ liệu với cơ sở dữ liệu chính

        #region Add Update Delete
        /// <summary>
        /// [DB Attr]Ghi dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> AddWithAttr<T>(T model) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Insertable(model).ExecuteCommand());
            //trả vềiCólongLoại,tại đây bạn có thể xử lý theo nhu cầu nghiệp vụ của mình
            return i;
        }

        /// <summary>
        /// [DB Attr]Cập nhật dữ liệu thực thể
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWithAttr<T>(T model) where T : class, new()
        {
            //Cách này sẽ lấyKhóa chínhlàm điều kiện
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Updateable(model).ExecuteCommand());
            return i > 0;
        }
        /// <summary>
        /// [DB Attr]TheoIDXóa theo danh sách
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsWithAttr<T>(object[] ids) where T : class, new()
        {
            var i = await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Deleteable<T>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// [DB Attr]TheoIDXóa
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
        /// TheoKhóa chínhTruy vấn
        /// </summary>
        /// <param name="pkValue">Khóa chính</param>
        /// <returns></returns>
        public async Task<T> FindKeyWithAttr<T>(object pkValue)
        {
            return await Context.Queryable<T>().InSingleAsync(pkValue);
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>()
        {
            return await Task.Run(() => dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().ToListAsync());
        }

        /// <summary>
        /// Truy vấn tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAllWithAttr<T>(bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }
        /// <summary>
        /// [DB Attr]Lấy theo điều kiện Một bản ghi 
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<T> GetFirstOrDefaultWithAttr<T>(Expression<Func<T, bool>> where)
        {
            return await dbTran.GetConnectionScopeWithAttr<T>().Queryable<T>().Where(where).FirstAsync();//Truy vấn một bản ghi, không có dữ liệu thì trả vềNULL, Singlevượt quá1bản ghi sẽ báo lỗi,Firstsẽ không
        }
        /// <summary>
        ///  Theo điều kiện Truy vấn(Theo T củaDb Truy vấn theo đặc tính)
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
        /// Truy vấn hai bảng,(Theo T1 củaDb Truy vấn theo đặc tính)
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinListWithAttr<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScopeWithAttr<T1>().Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }
        #endregion

        #region DbMuti Query 
        /// <summary>
        /// Theo điều kiện Truy vấn (choose db)
        /// </summary>
        /// <param name="where">Điều kiện</param>
        /// <returns></returns>
        public async Task<List<T>> GetByWhere<T>(string db, Expression<Func<T, bool>> where, bool isOrderBy = false, Expression<Func<T, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable<T>().Where(where).OrderByIF(isOrderBy, orderBy, orderByType).ToListAsync();
        }


        /// <summary>
        /// Truy vấn hai bảng,(choose db)
        /// </summary>
        /// <typeparam name="T1">Bảng trái</typeparam>
        /// <typeparam name="T2">Bảng phải</typeparam>
        /// <param name="joinExp">Cách nối bảng,Trường nối bảng(Quan hệ khóa chính - khóa ngoại)</param>
        /// <param name="selectExp">Kết quả truy vấn nối bảng</param>
        /// <param name="isWhere">Có cần điều kiện truy vấn hay không</param>
        /// <param name="whereExp">Truy vấn theo điều kiện</param>
        /// <param name="isOrderBy">Có cần sắp xếp hay không</param>
        /// <param name="orderBy">Điều kiện sắp xếp</param>
        /// <param name="orderByType">Kiểu sắp xếp(Asc,Desc)</param>
        /// <returns></returns>
        public async Task<List<object>> GetJoinList<T1, T2>(string db, Expression<Func<T1, T2, JoinQueryInfos>> joinExp, Expression<Func<T1, T2, dynamic>> selectExp, bool isWhere = false, Expression<Func<T1, T2, bool>> whereExp = null, bool isOrderBy = false, Expression<Func<T1, T2, object>> orderBy = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await dbTran.GetConnectionScope(db).Queryable(joinExp).WhereIF(isWhere, whereExp)
                .OrderByIF(isOrderBy, orderBy).Select(selectExp).ToListAsync();
        }

        #region Ado Không khuyên dùng
        /// <summary>
        /// Thực thi câu lệnh không truy vấnsqlcâu lệnh,trả về thao tác có thành công hay không(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<bool> ExecuteCommandSql(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.ExecuteCommandAsync(sql, parameters) > 0;
        }

        /// <summary>
        /// Thực thi truy vấnsqlcâu lệnh,Trả về tập kết quả truy vấn(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
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
        /// Thực thi truy vấnsqlcâu lệnh,Trả về Hàng đầu tiên, cột đầu tiên(choose db)
        /// </summary>
        /// <param name="sql">sqlcâu lệnh</param>
        /// <param name="parameters">Tham số hóa</param>
        /// <returns></returns>
        public async Task<object> GetScalar(string db, string sql, params SugarParameter[] parameters)
        {
            return await dbTran.GetConnectionScope(db).Ado.GetScalarAsync(sql, parameters);
        }

        #endregion
        #endregion

        
    }
}
