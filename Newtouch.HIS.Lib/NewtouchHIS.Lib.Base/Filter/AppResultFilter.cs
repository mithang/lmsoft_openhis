using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System.Diagnostics;
using System.Net;

namespace NewtouchHIS.Lib.Base.Filter
{

    /// <summary>
    /// WebApiBộ lọc kết quả trả về, thống nhất định dạng dữ liệu trả về
    /// </summary>
    public class AppResultFilter : Attribute, IActionFilter, IResultFilter, IExceptionFilter
    {
        /// <summary>
        /// Bắt đầu tính thời gian yêu cầu
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        public AppResultFilter()
        {

        }

        #region Tham số bộ nhớ đệm: lưu nội dung vào bộ nhớ đệm để tiện lấy sau này
        /// <summary>
        /// Tham số yêu cầu
        /// </summary>
        private object? request { get; set; }

        /// <summary>
        /// Nội dung trả về
        /// </summary>
        private Result? rets { get; set; }

        /// <summary>
        /// Đường dẫn yêu cầu
        /// </summary>
        private string? path { get; set; }
        #endregion


        /// <summary>
        /// Trước khi thực thi kết quả trả về
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            watch.Start();//Bắt đầu  
            //Lấy đường dẫn yêu cầu
            this.path = context.HttpContext.Request.Path;

            //Lấy thông tin tiêu đề yêu cầu, nếu tiêu đề yêu cầu chứa nội dung kiểm tra
            var Headers = context.HttpContext.Request.Headers;
            //var Token = Headers["Token"];

            //Lấy tham số yêu cầu
            this.request = context.ActionArguments;
        }

        /// <summary>
        /// Sau khi thực thi kết quả trả về
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            watch.Stop();
            //Triển khai cụ thể theo nhu cầu thực tế
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult == null || objectResult.Value == null)
                {
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.NotFound, Message = "Không tìm thấy tài nguyên", TimeOut = watch.ElapsedMilliseconds });
                }
                else
                {
                    if (objectResult.Value is Result)
                    {
                        var result = (Result)objectResult.Value;
                        result.TimeOut = watch.ElapsedMilliseconds;
                        context.Result = new JsonResult(result);
                        return;
                    }
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công", BusData = objectResult.Value, TimeOut = watch.ElapsedMilliseconds });
                    //Xác định xem giá trị trả về có phải là tuple không
                    //Trả về dữ liệu và trả về tổng số dòng public async Task<(List<object>,int)> Paging(PagingBaseRequest parameter) { retuen ( new List<object>{ },1000) }
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.Name == "ValueTuple`2")
                    {
                        dynamic value = objectResult.Value;
                        if (value.Item1 != null)
                        {
                            if (value.Item1 is int)
                            {
                                //Trả về định dạng tuple (int,List<object>)
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item1, Data = value.Item2 } });
                            }
                            else
                                //Trả về định dạng tuple (List<object>,int)
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item2, Data = value.Item1 } });
                        }
                    }

                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "", TimeOut = watch.ElapsedMilliseconds, BusData = (context.Result as ContentResult)!.Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new JsonResult(new { HttpStatus = (context.Result as StatusCodeResult)!.StatusCode, TimeOut = watch.ElapsedMilliseconds, Message = "" });
            }
            else if (context.Result is Exception)
            {
                var result = context.Result as Exception;
                context.Result = new JsonResult(new { HttpStatus = HttpStatusCode.BadRequest, TimeOut = watch.ElapsedMilliseconds, Message = result!.Message });
            }
        }

        /// <summary>
        /// Trước khi tạo kết quả, lúc này đã có thể lấy được dữ liệu cần trả về
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult!.Value == null)
                {
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.NotFound, Message = "Không tìm thấy tài nguyên", TimeOut = watch.ElapsedMilliseconds });
                }
                else
                {
                    if (objectResult.Value is ValidationProblemDetails)
                    {
                        var result = (ValidationProblemDetails)objectResult.Value;
                        var resultmsg = string.Join(';', result.Errors.Select(p => string.Join(',', p.Value)).ToList());
                        context.Result = new JsonResult(Result.BuildResult(null, resultmsg, watch.ElapsedMilliseconds, ResponseResultCode.ERROR));
                        return;
                    }
                    else if (objectResult.Value is ProblemDetails)
                    {
                        var result = (ProblemDetails)objectResult.Value;
                        context.Result = new JsonResult(Result.BuildResult(null, result.ToJson(), watch.ElapsedMilliseconds, ResponseResultCode.ERROR));
                        return;
                    }

                    //Xác định xem giá trị trả về có phải là tuple không
                    //Trả về dữ liệu và trả về tổng số dòng public async Task<(List<object>,int)> Paging(PagingBaseRequest parameter) { retuen ( new List<object>{ },1000) }
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.Name == "ValueTuple`2")
                    {
                        dynamic value = objectResult.Value;
                        if (value.Item1 != null)
                        {
                            if (value.Item1 is int)
                            {
                                //Trả về định dạng tuple (int,List<object>)
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item1, Data = value.Item2 } });
                            }
                            else
                                //Trả về định dạng tuple (List<object>,int)
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item2, Data = value.Item1 } });
                        }
                    }

                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "Yêu cầu thành công" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "", TimeOut = watch.ElapsedMilliseconds, BusData = (context.Result as ContentResult)!.Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new JsonResult(new { HttpStatus = (context.Result as StatusCodeResult)!.StatusCode, TimeOut = watch.ElapsedMilliseconds, Message = "" });
            }
            else if (context.Result is Exception)
            {
                var result = context.Result as Exception;
                context.Result = new JsonResult(new { HttpStatus = HttpStatusCode.BadRequest, TimeOut = watch.ElapsedMilliseconds, Message = result!.Message });
            }

        }


        /// <summary>
        /// Sau khi tạo kết quả, triển khai theo nhu cầu
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
        /// <summary>
        /// ngoại lệ
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {
            //Có tồn tại lỗi chưa được xử lý không
            if (context.ExceptionHandled == false)
            {
                //Đánh dấu lỗi là đã xử lý
                context.ExceptionHandled = true;
                //Cho trình duyệt biết đây là giá trị trả về bình thường
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                string errormsg = "";
                var result = new Result();
#if DEBUG
                errormsg = $"PATH:{path}======";
#endif
                errormsg = $"[Ngoại lệ]:{errormsg}{context.Exception.Message}";
                //Lấy danh sách tham số tiêu đề yêu cầu
                //var Headers_keys = "Token";
                //object hk = context.HttpContext.Request.Headers.Where(x => Headers_keys.Contains(x.Key)).ToList();

                string[] privateMsg = { Directory.GetCurrentDirectory() };
                errormsg = errormsg.Replace(privateMsg[0], string.Empty);
                //Thông tin lỗi
                switch (context.Exception)
                {
                    case FailedException ex:
                        result = Result.BuildResult(ex.Data, ex.Msg, watch.ElapsedMilliseconds, ResponseResultCode.FAIL);
                        break;
                    default:
                        result = Result.BuildResult(context.Exception.InnerException, errormsg, watch.ElapsedMilliseconds, ResponseResultCode.ERROR);
                        break;
                }
                //Trả về nội dung lỗi
                context.Result = new JsonResult(result);
            }

        }


    }


}
