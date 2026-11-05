using SqlSugar;

namespace NewtouchHIS.Lib.Base.Extension
{
    /// <summary>
    /// BA-005: giao dịch SqlSugar "nghiêm" — ngoại lệ PHẢI lan ra ngoài.
    ///
    /// Vấn đề: SqlSugar <see cref="IAdo.UseTran(Action)"/> và
    /// <see cref="IAdo.UseTran(Action, Action{Exception})"/> NUỐT ngoại lệ:
    /// lambda ném lỗi thì transaction rollback nhưng hàm vẫn trả về
    /// DbResult{IsSuccess=false} thay vì ném — caller nào không kiểm tra
    /// IsSuccess sẽ tưởng ghi thành công (đã kiểm chứng bằng harness trên
    /// SqlSugarCore 5.1.4.100 thật: throw sau insert -&gt; không có exception,
    /// IsSuccess=false, dòng đã rollback).
    /// Toàn cây chỉ có 2 call-site Ado.UseTran trực tiếp (cùng file
    /// QcBlzkDmnService.cs) và 1 wrapper chết BaseRepositoryV1.UseTran
    /// (0 caller, không được biên dịch — xem BA005-REPORT.md); KHÔNG có
    /// caller nào dựa vào hành vi nuốt lỗi, nên không sửa ngữ nghĩa toàn cục
    /// của SqlSugar (đó là DLL NuGet, không sửa được) mà cung cấp helper này
    /// và migrate các call-site sang nó.
    ///
    /// Ngữ nghĩa: BeginTran -&gt; chạy action -&gt; CommitTran; nếu action
    /// (hoặc commit) ném thì RollbackTran rồi NÉM LẠI đúng exception gốc
    /// (giữ nguyên type/message/stack). Đường thành công trả DbResult
    /// IsSuccess=true như UseTran cũ để code cũ kiểm tra IsSuccess vẫn đúng.
    /// Đây là cùng mẫu try/Begin/Commit/Rollback+rethrow mà EFDbTransaction
    /// và DbTrans trong cây đã dùng từ trước.
    /// </summary>
    public static class AdoTranExtensions
    {
        /// <summary>UseTran nhưng ngoại lệ lan ra ngoài (không nuốt).</summary>
        public static DbResult<bool> UseTranStrict(this IAdo ado, Action action)
        {
            ado.BeginTran();
            try
            {
                action();
                ado.CommitTran();
                return new DbResult<bool> { IsSuccess = true, Data = true };
            }
            catch
            {
                try { ado.RollbackTran(); } catch { /* đã commit / mất kết nối: không còn gì để rollback */ }
                throw;
            }
        }

        /// <summary>UseTran&lt;S&gt; nhưng ngoại lệ lan ra ngoài (không nuốt).</summary>
        public static DbResult<S> UseTranStrict<S>(this IAdo ado, Func<S> func)
        {
            ado.BeginTran();
            try
            {
                var data = func();
                ado.CommitTran();
                return new DbResult<S> { IsSuccess = true, Data = data };
            }
            catch
            {
                try { ado.RollbackTran(); } catch { /* đã commit / mất kết nối: không còn gì để rollback */ }
                throw;
            }
        }
    }
}
