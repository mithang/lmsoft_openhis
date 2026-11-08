using NewtouchHIS.Lib.Base.Model.DRG;
using System.Text.RegularExpressions;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    /// <summary>
    /// Lớp dịch tầng hiển thị cho kết quả phân nhóm DRG (R4-F10, follow-up F-B10).
    ///
    /// BỐI CẢNH — engine DRG là binary vendor đọc bảng .dat (tên chẩn đoán/thủ thuật
    /// tiếng Trung theo chuẩn CHS-DRG/ICD-10-CN). KHÔNG được dịch file .dat (sẽ phá khớp
    /// mã — xem keep-list B10/K-B10-1). Lớp này chỉ dịch CHUỖI TRẢ VỀ, theo MÃ
    /// (DRG/ICD/MDC), tại điểm policy trả kết quả ra API; engine, file .dat, DLL và
    /// GroupHelper (ranh giới engine) hoàn toàn không bị đụng tới.
    ///
    /// NGUYÊN TẮC FALLBACK — mọi tra cứu theo mã mà từ điển chưa có thì GIỮ NGUYÊN
    /// chuỗi gốc tiếng Trung (không bịa tên, không đoán). Mã (drg/mdc/adrg, mã ICD
    /// trong message) không bao giờ bị đổi — chỉ phần tên/mẫu câu quanh mã được dịch.
    ///
    /// NGUỒN CỦA TỪNG TÊN DỊCH — mọi tên tiếng Việt trong file này đều là bản dịch
    /// trực tiếp của tên chuẩn tiếng Trung, nguồn ghi rõ ở từng mục:
    ///  - status (3): chuỗi engine quan sát được trong scripts/drg/REPORT.md và
    ///    File/Drg/*_csharp_result.csv (1.018 dòng lịch sử).
    ///  - mẫu câu (7 pattern): đếm tần suất trên cùng 1.018 dòng lịch sử
    ///    (xem báo cáo R4-F10).
    ///  - tên DRG (2 mã chs_drg_11: HC15, RW23) + tên ICD (11 mã): tên gốc đọc
    ///    từ DRG.dat / ZD_INFO.dat / SS_INFO.dat (CHỈ ĐỌC, không sửa) của 2 bản
    ///    ghi probe live trong scripts/drg/REPORT.md (22058878, STG-2).
    ///  - tên vị trí MDCZ (6): đếm trên 1.018 dòng lịch sử (7 giá trị phân biệt).
    ///
    /// CHỮ TIẾNG TRUNG TRONG FILE NÀY — các literal tiếng Trung ở đây là KHÓA KHỚP
    /// với chuỗi engine trả về (kiểu keep-list K-B8-4/K27: literal giữ để khớp giá
    /// trị upstream), KHÔNG phải chuỗi hiển thị mới. Không thêm chuỗi hiển thị
    /// tiếng Trung nào khác.
    /// </summary>
    public static class DrgDisplayTranslator
    {
        public const string VersionChsDrg11 = "chs_drg_11";
        public const string VersionWuhan2022 = "wuhan_2022";

        /// <summary>
        /// status engine trả về (chuỗi khớp nguyên văn). Nguồn: 1.018 dòng lịch sử
        /// File/Drg/*_csharp_result.csv — 分组成功 1.004, 无法入组 5 — và
        /// scripts/drg/REPORT.md:216 ca 2b (诊断信息不可为空).
        /// </summary>
        private static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "分组成功", "Nhóm thành công" },
            { "无法入组", "Không vào được nhóm" },
            { "诊断信息不可为空", "Thông tin chẩn đoán không được để trống" },
        };

        /// <summary>
        /// Tên DRG theo mã, khóa "version:mã". Tên gốc (tiếng Trung) đọc từ
        /// DRG.dat chs_drg_11; tên Việt là bản dịch trực tiếp.
        /// CHỈ có 2 mã seed (bản ghi probe live). 626/628 mã còn lại của chs_drg_11
        /// (377 tên gốc phân biệt) BLOCKED — xem báo cáo R4-F10.
        /// LƯU Ý PHIÊN BẢN: cùng mã RW23 nhưng tên gốc khác nhau giữa các phiên bản
        /// (chs_drg_11: “…，伴并发症或合并症”; wuhan_2022: “…，伴有一般并发症与合并症”;
        /// HC15 chỉ có ở chs_drg_11) nên khóa bắt buộc kèm version.
        /// </summary>
        private static readonly Dictionary<string, string> DrgNameMap = new Dictionary<string, string>
        {
            // HC15 — DRG.dat chs_drg_11: "HC15 胆总管手术，不伴并发症或合并症"
            // (probe 22058878, scripts/drg/REPORT.md:196-200).
            { "chs_drg_11:HC15", "Phẫu thuật ống mật chủ, không kèm biến chứng hoặc bệnh kèm theo" },
            // RW23 — DRG.dat chs_drg_11: "RW23 恶性增生性疾患维持性治疗，伴并发症或合并症"
            // (probe STG-2, scripts/drg/REPORT-FIX.md:298).
            { "chs_drg_11:RW23", "Điều trị duy trì bệnh tăng sinh ác tính, kèm biến chứng hoặc bệnh kèm theo" },
        };

        /// <summary>
        /// Tên chẩn đoán/thủ thuật theo mã ICD, khóa là mã (không kèm version vì
        /// mã ICD là chuẩn chung; tra cứu thử đúng mã rồi thử dạng VIẾT HOA).
        /// Tên gốc đọc từ ZD_INFO.dat / SS_INFO.dat chs_drg_11; tên Việt là bản
        /// dịch trực tiếp. CHỈ 11 mã seed (2 bản ghi probe live). 33.303 tên
        /// chẩn đoán + 13.686 tên thủ thuật + 10.251 tên SS_VALID còn lại BLOCKED.
        /// K72.905 / Z90.408 trong bản ghi probe KHÔNG seed: 2 mã này không có
        /// trong ZD_INFO.dat (chỉ có ánh xạ trong ZD_MAP.dat) nên không có tên
        /// gốc để dịch trực tiếp.
        /// </summary>
        private static readonly Dictionary<string, string> IcdNameMap = new Dictionary<string, string>
        {
            // ZD_INFO.dat: "K80.302 胆总管结石伴胆管炎"
            { "K80.302", "Sỏi ống mật chủ kèm viêm đường mật" },
            // ZD_INFO.dat: "K80.305 肝内胆管结石伴胆管炎"
            { "K80.305", "Sỏi đường mật trong gan kèm viêm đường mật" },
            // ZD_INFO.dat: "K83.109 梗阻性黄疸"
            { "K83.109", "Vàng da tắc nghẽn" },
            // ZD_INFO.dat: "E14.900x001 糖尿病"
            { "E14.900x001", "Đái tháo đường" },
            // ZD_INFO.dat: "Z51.100x004 恶性肿瘤终末期化疗"
            { "Z51.100x004", "Hóa trị giai đoạn cuối u ác tính" },
            // ZD_INFO.dat: "C34.900x001 肺恶性肿瘤"
            { "C34.900x001", "U ác tính phổi" },
            // SS_INFO.dat: "51.8803 腹腔镜下胆总管切开取石术"
            { "51.8803", "Phẫu thuật mở ống mật chủ lấy sỏi qua nội soi ổ bụng" },
            // SS_INFO.dat: "51.8701 腹腔镜下胆总管T管引流术"
            { "51.8701", "Phẫu thuật dẫn lưu ống mật chủ bằng ống T qua nội soi ổ bụng" },
            // SS_INFO.dat: "54.5100x005 腹腔镜下腹腔粘连松解术"
            { "54.5100x005", "Phẫu thuật gỡ dính ổ bụng qua nội soi ổ bụng" },
            // SS_INFO.dat: "45.1301 十二指肠镜检查术"
            { "45.1301", "Nội soi tá tràng" },
            // SS_INFO.dat: "99.2503 静脉注射化疗药物"
            { "99.2503", "Tiêm tĩnh mạch thuốc hóa trị" },
        };

        /// <summary>
        /// Tên vị trí trong mẫu "诊断匹配MDCZ部位：…". Nguồn: 7 giá trị phân biệt
        /// trên 1.018 dòng lịch sử (5 đơn + 2 ghép, ghép tách bằng ，/、/, rồi
        /// dịch từng phần). Bản dịch trực tiếp: 创伤 = chấn thương.
        /// </summary>
        private static readonly Dictionary<string, string> BodyPartMap = new Dictionary<string, string>
        {
            { "下肢创伤", "Chấn thương chi dưới" },
            { "头颈部创伤", "Chấn thương đầu – cổ" },
            { "胸部创伤", "Chấn thương ngực" },
            { "上肢创伤", "Chấn thương chi trên" },
            { "躯干、脊柱创伤", "Chấn thương thân mình, cột sống" },
            { "腹部创伤", "Chấn thương bụng" },
        };

        // Mẫu câu engine (regex, giữ nguyên mã; ，và ： là dấu toàn giác của engine).
        // Tần suất trên 1.018 dòng lịch sử: 符合…入组条件 2.023, 主诊断…排除表 1.151,
        // 诊断…属于CC，排除表 470, 诊断…在MDCZ的主诊表内 39,
        // 诊断匹配MDCZ部位 39, “…未知名称” 30.
        private static readonly Regex ReEligible = new Regex("^符合([A-Za-z0-9*]+)入组条件$", RegexOptions.Compiled);
        private static readonly Regex ReMainDiagExclude = new Regex("^主诊断(.+)排除表(\\d+)$", RegexOptions.Compiled);
        private static readonly Regex ReCcExclude = new Regex("^诊断(.+?)属于CC，排除表(\\d+)$", RegexOptions.Compiled);
        private static readonly Regex ReInMainDiagTable = new Regex("^诊断(.+?)在(MDC[A-Z])的主诊表内$", RegexOptions.Compiled);
        private static readonly Regex ReMatchBodyPart = new Regex("^诊断匹配(MDC[A-Z])部位：(.+)$", RegexOptions.Compiled);
        private static readonly Regex ReUnknownName = new Regex("^(\\S+)\\s+未知名称$", RegexOptions.Compiled);
        private static readonly Regex ReDrgFinal = new Regex("^\\*\\*\\*([A-Za-z0-9*]+)\\s+(.+)\\*\\*\\*$", RegexOptions.Compiled);
        private static readonly Regex ReCodeName = new Regex("^([A-Za-z0-9][A-Za-z0-9.+\\-*]*)\\s+(.+)$", RegexOptions.Compiled);

        /// <summary>Chất lượng hậu tố tên DRG (ghi chú cho từ điển đầy đủ tương lai —
        /// CHƯA dùng làm bản dịch live vì 377 tên gốc chưa dịch):
        /// 不伴并发症或合并症 = không kèm biến chứng hoặc bệnh kèm theo;
        /// 伴并发症或合并症 = kèm biến chứng hoặc bệnh kèm theo;
        /// 伴严重并发症或合并症 = kèm biến chứng hoặc bệnh kèm theo nghiêm trọng.
        /// DRG.dat chs_drg_11: 184 / 129 / 123 dòng mang 3 hậu tố này, 192 dòng
        /// không hậu tố; 628 dòng = 377 tên gốc phân biệt + 4 dạng hậu tố.</summary>
        public const string DrgQualifierNote = "xem ghi chú hậu tố trong DrgDisplayTranslator";

        public static string TranslateStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return status;
            }
            string translated;
            if (StatusMap.TryGetValue(status, out translated))
            {
                return translated;
            }
            return status;
        }

        public static string TranslateMessage(string message, string version)
        {
            if (string.IsNullOrEmpty(message))
            {
                return message;
            }

            // Câu độc lập, khớp nguyên văn: "không đủ điều kiện vào bất kỳ nhóm MDC nào".
            // Xuất hiện 7 lần trong 1.018 dòng lịch sử (các dòng 无法入组).
            if (message == "不符合所有MDC的入组条件")
            {
                return "Không đủ điều kiện vào bất kỳ nhóm MDC nào";
            }

            Match m;
            // "***MÃ DRG tên DRG***" — dòng kết luận; tên dịch theo mã, miss thì giữ nguyên.
            m = ReDrgFinal.Match(message);
            if (m.Success)
            {
                string code = m.Groups[1].Value;
                string vnName;
                if (!string.IsNullOrEmpty(version) && DrgNameMap.TryGetValue(version + ":" + code, out vnName))
                {
                    return "***" + code + " " + vnName + "***";
                }
                return message;
            }

            // "符合<MÃ>入组条件" — đủ điều kiện vào nhóm <MÃ> (MÃ là MDC hoặc ADRG).
            m = ReEligible.Match(message);
            if (m.Success)
            {
                return "Đủ điều kiện vào nhóm " + m.Groups[1].Value;
            }

            // "主诊断<MÃ>排除表<N>" — chẩn đoán chính <MÃ> loại trừ bảng <N>.
            m = ReMainDiagExclude.Match(message);
            if (m.Success)
            {
                return "Chẩn đoán chính " + m.Groups[1].Value + " loại trừ bảng " + m.Groups[2].Value;
            }

            // "诊断<MÃ>属于CC，排除表<N>" — chẩn đoán <MÃ> thuộc CC, loại trừ bảng <N>.
            // CC là thuật ngữ chuẩn DRG (biến chứng/bệnh kèm theo), giữ nguyên mã CC.
            m = ReCcExclude.Match(message);
            if (m.Success)
            {
                return "Chẩn đoán " + m.Groups[1].Value + " thuộc CC, loại trừ bảng " + m.Groups[2].Value;
            }

            // "诊断<MÃ>在<MDC>的主诊表内" — chẩn đoán <MÃ> trong bảng chẩn đoán chính của <MDC>.
            m = ReInMainDiagTable.Match(message);
            if (m.Success)
            {
                return "Chẩn đoán " + m.Groups[1].Value + " trong bảng chẩn đoán chính của " + m.Groups[2].Value;
            }

            // "诊断匹配<MDC>部位：<vị trí>" — chẩn đoán khớp vị trí <MDC>: <vị trí đã dịch>.
            // Tra cứu CẢ CHUỖI vị trí trước (vd "躯干、脊柱创伤" là một khóa).
            // Chỉ khi không khớp cả chuỗi mới tách bằng ，hoặc , (KHÔNG tách bằng 、
            // vì 、 nằm BÊN TRONG khóa "躯干、脊柱创伤"), rồi dịch từng phần;
            // phần nào chưa có trong map thì giữ nguyên phần đó.
            m = ReMatchBodyPart.Match(message);
            if (m.Success)
            {
                string partText = m.Groups[2].Value;
                string vnWhole;
                if (BodyPartMap.TryGetValue(partText, out vnWhole))
                {
                    return "Chẩn đoán khớp vị trí " + m.Groups[1].Value + ": " + vnWhole;
                }
                string[] parts = partText.Split(new[] { '，', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var translatedParts = new List<string>();
                foreach (string part in parts)
                {
                    string p = part.Trim();
                    string vnPart;
                    translatedParts.Add(BodyPartMap.TryGetValue(p, out vnPart) ? vnPart : p);
                }
                return "Chẩn đoán khớp vị trí " + m.Groups[1].Value + ": " + string.Join(", ", translatedParts);
            }

            // "<mã> 未知名称" — mã không có tên trong từ điển engine: giữ mã, dịch hậu tố.
            m = ReUnknownName.Match(message);
            if (m.Success)
            {
                return m.Groups[1].Value + " chưa rõ tên";
            }

            // "<mã ICD> <tên>" — dòng tên chẩn đoán/thủ thuật; tên dịch theo mã,
            // thử đúng mã rồi thử dạng VIẾT HOA (engine có khi trả mã thường, vd e11.800).
            // Miss thì giữ nguyên cả dòng (tên chưa có trong từ điển hạt giống).
            m = ReCodeName.Match(message);
            if (m.Success)
            {
                string code = m.Groups[1].Value;
                string vnName;
                if (IcdNameMap.TryGetValue(code, out vnName)
                    || IcdNameMap.TryGetValue(code.ToUpperInvariant(), out vnName))
                {
                    return code + " " + vnName;
                }
                return message;
            }

            return message;
        }

        public static List<string> TranslateMessages(IEnumerable<string> messages, string version)
        {
            if (messages == null)
            {
                return null;
            }
            return messages.Select(msg => TranslateMessage(msg, version)).ToList();
        }

        /// <summary>
        /// Dịch một DrgGroupResult: chỉ đổi status/messages; Index/mdc/adrg/drg
        /// (toàn mã) và record (bản ghi đầu vào) giữ nguyên.
        /// </summary>
        public static DrgGroupResult TranslateResult(DrgGroupResult result, string version)
        {
            if (result == null)
            {
                return null;
            }
            return new DrgGroupResult
            {
                Index = result.Index,
                status = TranslateStatus(result.status),
                messages = TranslateMessages(result.messages, version),
                mdc = result.mdc,
                adrg = result.adrg,
                drg = result.drg,
                record = result.record,
            };
        }

        /// <summary>Dịch một dòng kết quả file (DrgMedicalRecordResult): đổi tại chỗ
        /// status/messages; các cột mã và cột đầu vào giữ nguyên.</summary>
        public static void TranslateFileRow(DrgMedicalRecordResult row, string version)
        {
            if (row == null)
            {
                return;
            }
            row.status = TranslateStatus(row.status);
            row.messages = TranslateMessages(row.messages, version);
        }
    }
}
