using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    public interface IDrgGroupBasePolicy
    {
        /// <summary>
        /// Phân tích nhóm hàng loạt ca bệnh
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
        /// <summary>
        /// Phân tích nhóm trang đầu bệnh án bệnh nhân
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        /// <summary>
        /// Phân tích nhóm dữ liệu tệp
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);

    }
    public abstract class DrgGroupBasePolicy : IDrgGroupBasePolicy
    {
        protected virtual bool Validata(DrgAreaMedicalRecord request)
        {
            if (request.zdList.Length == 0)
            {
                throw new FailedException("Thông tin chẩn đoán không được để trống");
            }
            return true;
        }
        protected virtual bool Validata(ref DrgAreaFileInfo request)
        {
            if (request.FileName.Length == 0)
            {
                throw new FailedException("Thông tin tệp không được để trống");
            }
            //Đường dẫn tải lên mặc định
            var defaultPath = $"{Directory.GetCurrentDirectory()}{ConfigInitHelper.SysConfig!.FileConfig!.DrgDataFileDirPath ?? ""}";
            if (!(!string.IsNullOrWhiteSpace(request.FilePath) && File.Exists(defaultPath + request.FilePath + request.FileName)))
            {
                string[] match = { $"{request.FileName}*.csv", request.FileName };
                var files = match.SelectMany(g => Directory.EnumerateFiles(defaultPath, g, SearchOption.AllDirectories)).ToList();
                request.FilePath = files.FirstOrDefault(p => !p.Contains("_result.csv"));
                if (string.IsNullOrWhiteSpace(request.FilePath))
                {
                    throw new FailedException("Vui lòng tải lên tệp Drg liên quan trước");
                }
            }
            else
            {
                request.FilePath = defaultPath + request.FilePath + request.FileName;
            }
            return true;
        }
        protected virtual async Task FileRecordEndProcAsync(DrgAreaFileInfo request)
        {
            await Task.Run(() =>
            {
                var defaultPath = $"{Directory.GetCurrentDirectory()}{ConfigInitHelper.SysConfig!.FileConfig!.DrgDataFileDirPath ?? ""}";
                string[] match = { $"{request.FileName}*.csv", request.FileName };
                var files = match.SelectMany(g => Directory.EnumerateFiles(defaultPath, g, SearchOption.AllDirectories)).ToList();
                if (request.IsTmpFile ?? true)
                {
                    foreach (var f in files)
                    {
                        if (File.Exists(f))
                        {
                            File.Delete(f);
                        }
                    }
                }
            });

        }
        public virtual async Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record)
        {
            return await Task.Run(() => new BusResult<List<DrgGroupResult>>() { code = ResponseResultCode.FAIL, msg = "Giao diện chưa được ủy quyền hoặc chức năng chưa được mở" });
        }
        public virtual async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            return await Task.Run(() => new BusResult<DrgGroupResult>() { code = ResponseResultCode.FAIL, msg = "Giao diện chưa được ủy quyền hoặc chức năng chưa được mở" });
        }
        public virtual async Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record)
        {
            return await Task.Run(() => new BusResult<DrgGroupFileResult>() { code = ResponseResultCode.FAIL, msg = "Giao diện chưa được ủy quyền hoặc chức năng chưa được mở" });
        }

    }
}
