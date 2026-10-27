using NewtouchHIS.DrgGroup.wuhan_2022;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    /// <summary>
    /// Bản Vũ Hán 2022 
    /// </summary>
    public interface IDrgWuhan2022Policy : IDrgGroupBasePolicy
    {
        ///// <summary>
        ///// Truy vấn kết quả nhóm của một bệnh án
        ///// </summary>
        ///// <param name="record"></param>
        ///// <returns></returns>
        //Task<BusResult<DrgGroupResult>> GroupMedRecord(DrgAreaMedicalRecord record);
        ///// <summary>
        ///// csvTruy vấn kết quả nhóm tệp
        ///// </summary>
        ///// <param name="record"></param>
        ///// <returns></returns>
        //Task<BusResult<DrgGroupFileResult>> GroupFileRecord(DrgAreaFileInfo record);
    }
    public class DrgWuhan2022Policy : DrgGroupBasePolicy, IDrgWuhan2022Policy
    {
        public override async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            var result = new BusResult<DrgGroupResult>();
            var isValid = base.Validata(record);
            if (!isValid)
            {
                result.msg = "Thông tin quan trọng không được để trống";
                result.code = ResponseResultCode.FAIL;
                return result;//Nếu kiểm tra thất bại thì trả về trực tiếp
            }
            //string str = "22082078,1,24, 9105, 3470,13050201, 6, 1,\"K63.500,K52.910\",\"00.5500,45.4300x010,45.4300x013\"";
            //var recordResult = GroupHelper.GroupRecord(str);
            await Task.Run(() =>
            {
                var recordResult = GroupHelper.GroupRecord(new MedicalRecord
                {
                    Index = record.Index,
                    gender = record.gender,
                    age = record.age,
                    ageDay = record.ageDay,
                    weight = record.weight,
                    dept = record.dept,
                    inHospitalTime = record.inHospitalTime,
                    leavingType = record.leavingType,
                    zdList = record.zdList,
                    ssList = record.ssList,
                    remark = record.remark,
                });
                if (recordResult != null)
                {
                    result.Data = new DrgGroupResult
                    {
                        Index = recordResult.Index,
                        status = recordResult.status,
                        messages = recordResult.messages,
                        mdc = recordResult.mdc,
                        adrg = recordResult.adrg,
                        drg = recordResult.drg
                    };
                    result.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    result.code = ResponseResultCode.FAIL;
                    result.msg = "Bộ nhóm DRG không trả về dữ liệu, vui lòng liên hệ quản trị viên";
                }
            });

            return result;
        }

        public override async Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record)
        {
            var result = new BusResult<DrgGroupFileResult>();
            var isValid = base.Validata(ref record);
            if (!isValid)
            {
                result.msg = "Kiểm tra tham số thất bại";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            if (string.IsNullOrWhiteSpace(record.FilePath))
            {
                result.msg = $"Tìm đường dẫn tệp thất bại";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            var recordResult = GroupHelper.GroupFileRecord(record.FilePath, record.FileCols);
            if (string.IsNullOrWhiteSpace(recordResult))
            {
                result.msg = "Lấy kết quả nhóm thất bại";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            var resultCols = "Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,zdList,ssList,remark,status,messages,mdc,adrg,drg";
            var resultcsv = await GroupHelper.GroupFileReadAsync(recordResult, resultCols.Split(','));
            if (resultcsv.Any())
            {
                List<DrgMedicalRecordResult> csvRead = new List<DrgMedicalRecordResult>();
                foreach (var r in resultcsv)
                {
                    var rArray = r.Split(",");
                    csvRead.Add(new DrgMedicalRecordResult()
                    {
                        Index = rArray[0],
                        gender = rArray[1],
                        age = Convert.ToInt32(rArray[2]),
                        ageDay = Convert.ToInt32(rArray[3]),
                        weight = Convert.ToInt32(rArray[4]),
                        dept = rArray[5],
                        inHospitalTime = Convert.ToInt32(rArray[6]),
                        leavingType = rArray[7],
                        zdList = rArray[8].Split('|'),
                        ssList = rArray[9].Split('|'),
                        remark = rArray[10],
                        status = rArray[11],
                        messages = rArray[12].Split('|').ToList(),
                        mdc = rArray[13],
                        adrg = rArray[14],
                        drg = rArray[15],
                    });
                }
                result.code = ResponseResultCode.SUCCESS;
                result.Data = new DrgGroupFileResult { rows = csvRead, file = Path.GetFileName(recordResult) };
            }
            //result.Data = new DrgGroupFileResult { file = recordResult };
            return result;
        }

    }
}
