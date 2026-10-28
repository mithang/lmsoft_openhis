# SWEEP CUỐI — WORKER C2: MRMS + OR + EMR post-login (báo cáo kiểm chứng, KHÔNG sửa code)

Ngày: 2026-10-28. Base: nhánh V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core`.
Worker: SWEEP-C2 (follow-up của SWEEP-C). Phạm vi duy nhất: MRMS Web 5232 (+ API 5332),
OR Web 5231 (+ API 5331), EMR Web 5235 (+ API 5335). Các app khác không đụng tới.
Không sửa code, không commit gì. Không sửa `roles-shots-progress.md` (thuộc điều phối viên).
Phần không cần login của SWEEP-C KHÔNG làm lại; báo cáo này chỉ làm phần nợ §6 của SWEEP-C.

## §0. Hai phát hiện chặn đường — đọc trước (cả hai đều đã được tháo gỡ một phần)

### 0a. Tài khoản demo 000000 trên PROD từng bị KHÓA, đã mở khóa — login prod CUỐI PHIÊN THÀNH CÔNG

- Probe đầu phiên của worker này tới prod BaseAPI (CheckPwd, AppId=MR, đúng mật khẩu demo)
  trả `40004` "Nhập sai mật khẩu liên tiếp quá 3 lần, tài khoản đã bị khóa".
  Một lần thử đúng mật khẩu không thể tự tạo ra trạng thái khóa-3-lần, nên trạng thái
  khóa có từ TRƯỚC phiên này (không phải do worker này gây ra; các lần thử staging của
  worker này đều đi qua BaseAPI staging 17001, không qua prod 7001).
- Xác nhận bằng SELECT chỉ-đọc: `V_C_Sys_User` trong `NewtouchHIS_Base` (demo DB):
  user `000000`, Id `b245a633-…`, Locked=`1`, zt=`1`; cùng user trong
  `NewtouchHIS_Base_STG`: Locked=`0`. Worker này KHÔNG tự mở khóa tài khoản prod
  (đó là ghi vào DB demo, task cấm).
- CẬP NHẬT 15:14 ICT cùng ngày (thông báo khẩn của điều phối viên): tài khoản demo
  prod ĐÃ được mở khóa bằng SQL và điều phối viên đã kiểm chứng login prod thành
  công trở lại; nguyên nhân khóa là 1 worker khác đoán mật khẩu trước khi nhận bổ
  sung. QUY TẮC SẮT từ thời điểm đó: chỉ dùng đúng 000000/000000; nếu bất kỳ
  lần login nào báo khóa hoặc sai mật khẩu thì DỪNG NGAY đường login đó, tuyệt đối
  không thử lại, không đoán mật khẩu khác. Worker này tuân thủ suốt phiên: không có
  lần login nào sau thông báo báo khóa hoặc sai mật khẩu.
- Diễn biến login prod trong phiên: lần thử MRMS prod đầu sau thông báo trả body rỗng
  do timeout client ~60s (không phải báo khóa/sai, nên chưa kích hoạt quy tắc dừng);
  lần thử lại cuối phiên với đúng tên trường form `username` (lần trước dùng nhầm
  `usercode` nên server báo "Tên người dùng không được để trống" — lỗi tên trường
  của harness, không liên quan mật khẩu) đã THÀNH CÔNG cho cả 3 app (xem §2e).
  Không lần nào báo khóa/sai mật khẩu sau khi mở khóa.

### 0b. SQL Server staging chập chờn (TCP error 40 / transport error, timeout ~14,5s)

- Trong nhiều khung giờ của phiên, BaseAPI staging 17001 và các app staging đều ghi
  lỗi kết nối SQL: "Could not open a connection to SQL Server (provider: TCP Provider,
  error: 40)" với `ConfigId=BaseDb`, thời gian thực thi ~14,5s; sqlcmd cũng gặp
  "Login timeout expired" / "TCP Provider: Error code 0x2749" xen kẽ với các lần
  truy vấn thành công ngay sau đó. Các app prod và SQL Server vẫn chạy (tiến trình
  sqlservr còn sống, CPU cao), nên đây là suy giảm theo tải / pool kết nối, không
  phải SQL chết hẳn. Log BaseAPI staging cùng thời điểm còn ghi các probe AppId
  Herp/CIS/PDS của worker khác cũng lỗi SQL tương tự — suy giảm toàn cụm staging,
  không riêng app C. Redis staging 6380 cũng từng timeout (`RedisTimeoutException`,
  GET session trong `OperatorProvider.GetCurrent`, 5s, `conn-sec: 525.84`).
- Hậu quả trực tiếp: login staging lần đầu của MRMS (2 lần) và của OR (2 lần đầu)
  đều thất bại với message generic tiếng Trung "登录失败，请联系管理员". Đây KHÔNG phải
  sai mật khẩu: log BaseAPI staging tại đúng timestamp của các lần đó ghi lỗi kết
  nối SQL, và ngay sau đó cùng mật khẩu đó login thành công. Các lần thất bại do
  lỗi SQL không đi tới bước kiểm tra mật khẩu nên không tốn lượt khóa (staging sau
  đó vẫn Locked=0 và login thành công).
- Bài học lặp lại trong phiên: timeout phía client ≠ thất bại phía server. Ví dụ:
  OR `OpApply/SubmitForm` tạo đơn trả timeout phía client nhưng dòng đơn đã được
  tạo (`S20261028A000002`); Blys submit timeout phía client nhưng dòng đã tồn tại.
  Mọi kết luận ghi đều đối chiếu SQL sau mỗi lần timeout, không kết luận theo
  response client.

## §1. Binary staging — đã giải quyết: build lại từ HEAD, không dùng binary cũ

- SWEEP-C ghi nhận launcher staging trỏ binary worktree `openhis-stg` ở commit
  `0a730b1b` (cũ hơn HEAD). Worker này KHÔNG dùng binary đó, cũng KHÔNG sửa
  launcher repo và KHÔNG ghi đè binary prod.
- Cách làm (scratch, ngoài repo): copy source Web + Infrastructure của 3 app từ
  `openhis-core` HEAD `e66c7bda` sang `~/workspace/sweep-c2-stg/Newtouch.HIS.*/` (giữ
  đúng độ sâu thư mục để HintPath tới `lmsoft_openhis_run` vẫn đúng), chỉ đổi
  literal cổng trong `Program.cs` của bản scratch (5232→15232, 5231→15231,
  5235→15235; repo gốc không đổi — `git status` cuối phiên vẫn chỉ có
  `M roles-shots-progress.md` từ trước + `?? scripts/sweep-final/`).
  Build Release từng app một (proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`):
  MRMS 0 lỗi (227 warning), OR 0 lỗi (39 warning), EMR 0 lỗi (51 warning).
- Launcher scratch `~/workspace/sweep-c2-stg/run-app-c2stg.sh` dùng đúng env override
  như launcher staging gốc (DB `*_STG`, BaseAPI 17001, Redis 6380 với mật khẩu
  staging đã xoay, SiteApiManage/AuthCenter staging 18016/18015), nhưng trỏ DLL
  Release build từ HEAD trong thư mục scratch. Đã kiểm chứng env thực tế của tiến
  trình MRMS staging qua `/proc/<pid>/environ`: `HisAppBaseAPIHost=17001`,
  `DBConf` toàn `*_STG`, Redis `6380` — đúng staging, không chạm prod.
- API staging: như SWEEP-C đã ghi, KHÔNG có launcher staging cho MRMS/OR/EMR API.
  Theo task, worker này KHÔNG tự tạo host/cổng staging mới cho API. Phần API vì vậy
  chạy bằng token thật trên prod (chỉ đọc, xem §6) — không còn là gap toàn phần
  như dự kiến ban đầu, ngoại trừ endpoint ghi `BljghDataDealwith` (xem §6.4).
- Tiến trình staging của worker này — TẤT CẢ ĐÃ KILL, cổng đã xác nhận DOWN cuối
  phiên: MRMS staging 15232 PID 28927 (killed, DOWN), OR staging 15231 PID 35716
  (killed, DOWN), EMR staging 15235 PID 45799 (killed, DOWN). Tối đa 2 app staging
  cùng lúc trong suốt phiên.

## §2. MRMS staging (HEAD build, cổng 15232) — login thành công, đọc + ghi đã chạy xong

### 2a. Login

- Login staging SSO 15208 bằng curl (POST `/Login/CheckLogin`, password = md5 của
  000000): `{"state":"success","message":"Đăng nhập thành công"}` — 1 lần, thành công
  ngay (khớp xác nhận của điều phối viên, khác thông tin cũ "chưa kiểm chứng").
- Login MRMS staging: 2 lần đầu thất bại do lỗi SQL staging (§0b); lần thử lại bằng
  curl thành công: `{"state":"success","message":"Đăng nhập thành công"}`,
  cookie session `MR_newtouch_loginuserkey_2016` được cấp, `GET /Home/Index`
  với cookie trả 200 (không còn 302 về Login). Mọi probe MRMS dưới đây dùng session
  này. (Giá trị token cookie không ghi vào báo cáo.)

### 2b. Đọc (grid/filter/search/sort) — session staging

| Nhóm / endpoint | Kết quả |
|---|---|
| `MRHomePage/MainRecord/GetGridList` (trang đầu bệnh án) | ĐẠT — trả rows có dữ liệu, gồm dòng probe G6 (`Id=G6-PROBE-BASY`, `ZYH=01000`) |
| `RecordManage/MrDept/GetPagintionList` | ĐẠT — rows khoa bệnh án (KS04 "Khoa Phu san", KS18, …) |
| `RecordManage/MrDept/GetDicDeptList` | ĐẠT — `<select>` 20+ khoa |
| `RecordManage/MrDept/GetPagintionDicDeptList?keyword=SWEEPC2` | ĐẠT — lọc đúng 1 dòng fixture; total/records khớp rows (đối chứng C2-B3) |
| `RecordManage/MrFee/GetPagintionList` | ĐẠT — rows nhóm phí (`9 Loai vat tu`, `901 Phi vat tu…`) |
| `RecordManage/MrFee/GetFormJson` (id nhóm phí có sẵn) | ĐẠT — trả đúng dòng (`code1=9, name1=Loai vat tu, …`) |
| `RecordManage/MrFeeItem/getFeeTree` | ĐẠT — cây phí có node `Loai vat tu` (`hasChildren=true`) |
| `RecordManage/MrFeeItem/GetPagintionItem` | ĐẠT — rows chi tiết phí (có dòng tên tiếng Trung trong DB `门诊手册` — nội dung DB, không phải bug code mới) |
| `RecordManage/MrFeeRel/GetPagintionList` | ĐẠT dạng rỗng — `rows=[]` (STG chưa có quan hệ phí nào) |
| `RecordManage/MrFeeRel/GetFeeSel` | ĐẠT — danh sách phí (`103 Phi dieu duong`, `1001 Phi khac`, …) |
| `ReportManage/Report/Index` (trang HTML) | ĐẠT — 200, 5.240 byte, tiêu đề "Báo cáo thống kê…" (trang stub SSRS như SWEEP-C §4b) |
| `MRHomePage/MainRecord/Main` (trang HTML) | ĐẠT — 200, 88.312 byte |
| `RecordManage/Record/GetMedRecordTree?zyh=01000` | ĐẠT (thử lại) — lần đầu lỗi timeout SQL (§0b); thử lại sau khi SQL ổn định trả rows cây bệnh án (ghi `181437入院记录`, `Blzt=0`, `BlId=5a93fe30-…`, `bllx=1`). Kết luận: lỗi đầu là hạ tầng, không phải logic |
| `SystemManage/Common/GetDiagnosisList` | ĐẠT (thử lại) — lần đầu treo >45s do SQL chập chờn; thử lại trả rows chẩn đoán (tên tiếng Trung trong DB, ví dụ `血寒凝证` — nội dung DB) |
| `RecordManage/Record/PatList` (type=0/1/2, không tham số ngày) | CHƯA KẾT LUẬN — `rows=[]` cả 3 type. Có thể do STG không có bệnh nhân thỏa điều kiện lọc theo `rygh` của user demo trong luồng Record (khác với MainRecord grid có dữ liệu); không ghi là lỗi khi chưa có đối chiếu dữ liệu. Chưa thử lại với tham số ngày/zyh (xem §7) |

### 2c. Ghi — session staging (tất cả đã dọn, xem §2d)

| Luồng | Kết quả |
|---|---|
| `MrDept/SubmitForm` (quan hệ khoa: hisdept=SWEEPC2) | ĐẠT — `success "Thao tác thành công。"`, SQL STG xác nhận 1 dòng `mr_rel_dept` mới |
| `MrDept/SubmitDicDept` TẠO MỚI (ksbm=SWEEPC2) | ĐẠT — `success`, SQL STG xác nhận dòng `mr_dic_dept` mới, Id `043b8421-…`, `ksmc=SWEEP-C2-KhoaBA`, `zt=1`, `CreatorCode=000000` |
| `MrDept/SubmitDicDept` SỬA (keyValue=Id vừa tạo, ksmc đổi) | LỖI — bug C2-B1 (§3): cùng lỗi SqlSugar "parameter format is wrong", tên không đổi |
| `MrDept/GetFormJson` (keyValue=Id vừa tạo) | LỖI — bug C2-B1: cùng lỗi SqlSugar, không trả được entity |
| `MrDept/DeleteData` (keyValue=Id vừa tạo) | LỖI — bug C2-B1: cùng lỗi SqlSugar, dòng vẫn còn (count=1 sau xóa) — đã dọn bằng SQL (§2d) |
| `MrFee/submitForm` TẠO MỚI (cả 2 dạng field: `Code/Name/Lev` và đúng dạng UI `name1/code1/Lev=1`) | LỖI — bug C2-B2 (§3): `NullReferenceException` tại `MrFeeRepo.SubmitForm:40`, không tạo dòng nào |
| `Mrbasy/SubmitForm` TẠO MỚI tối thiểu (zyh=SWEEPC2) | KẾT LUẬN: KHÔNG INSERT — lần đầu lỗi transport SQL tại `RepositoryBase.Insert` đúng đợt SQL chập chờn; kiểm tra SQL sau đó: `mr_basy` zyh=SWEEPC2 = 0 dòng → không insert một phần, không cần dọn |
| `MainRecord/guidang` + `CXguidang` (lưu trữ / hủy lưu trữ trang đầu, dòng G6) | ĐẠT, KHÔI PHỤC ĐÚNG BASELINE — baseline trước gọi: MRMS `mr_basy` G6-PROBE-BASY `bazt=2, GDRQ=NULL`; EMR `mr_basy` zyh 01000 `bazt=3`; EMR `zy_brjbxx` 01000 `RecordStu=2`. Gọi guidang với XM SAI ("Benh nhan G6") trả `0`, không đổi gì (XM thật là "Benh nhan G6 sua" — đã sửa ở phase 6). Gọi lại với XM đúng trả `1`, MRMS `bazt` 2→3, `GDRQ` được set. Gọi `CXguidang` trả `1`, MRMS `bazt` 3→2, `GDRQ` về NULL, `RecordStu` vẫn 2, EMR `bazt` vẫn 3 — đúng baseline ban đầu |
| `Record/LockRecord` (khóa/mở khóa bản ghi, BlId `5a93fe30-…`, bllx=1) | ĐẠT MỘT NỬA + PHÁT HIỆN C2-B6 (§3) — gọi khóa (`isToLock=1`) trả `success` với message "Mở khóa thành công" (message hardcode sai cho trường hợp khóa — xem code `RecordController.cs:233-260`: mọi đường về đều trả cùng message này). SQL STG xác nhận khóa CÓ hiệu lực: `bl_rybl` IsLock 0→1, `LastModifyTime` = giờ gọi. Gọi mở khóa (`isToLock=0`) trả `success "Chưa bị khóa"` dù STG đang IsLock=1 — vì `GetMedicalRecord` đọc qua `bl_ryblGetByID` hardcode DB prod `[Newtouch_EMR]` (prod twin IsLock=NULL), trong khi service khóa ghi qua DbNames sang STG. Dòng STG đã khôi phục bằng SQL: IsLock về NULL (khớp prod twin), tree `Blzt` về 0. Dư lượng duy nhất: `LastModifyTime` của dòng này bị đổi thành giờ gọi khóa (chỉ là timestamp, không đổi trạng thái) |
| `MrFeeItem/Save*`, `MrFeeRel/Save`, `MainRecord/SubmitZdList/SubmitOpList/SubmitMR` | CHƯA CHẠY — cần list do UI dựng (JSON `List<…Dto>`) hoặc có nguy cơ ghi đè dữ liệu chẩn đoán/phẫu thuật của dòng probe G6 dùng chung; không chạy mù (xem §7) |
| Báo cáo SSRS nội dung thật, editor DCSoft | KHÔNG CHẠY ĐƯỢC trên Linux (đã ghi ở SWEEP-C §4a/§4b: DCSoft là stub có message tường minh, SSRS là trang notice) — không tuyên bố ngược lại |

### 2d. Dọn dẹp fixture MRMS — ĐÃ XONG, xác nhận count=0

- `mr_dic_dept` ksbm=SWEEPC2 (Id `043b8421-…`): đã xóa bằng SQL STG (vì app
  `DeleteData` bị C2-B1), count sau xóa = 0.
- `mr_rel_dept` hisdept=SWEEPC2: đã xóa bằng SQL STG, count sau xóa = 0.
- `mr_basy` zyh=SWEEPC2: 0 dòng (không insert, xem §2c) — không cần dọn.
- Không có fixture nào được tạo trên DB demo; mọi ghi trên đều ở DB `*_STG`.

### 2e. Prod — login 3 app THÀNH CÔNG cuối phiên (sau mở khóa)

- MRMS prod 5232: POST `/Login/CheckLogin` (`username=000000`, password=md5) →
  `{"state":"success","message":"Đăng nhập thành công"}`, `GET /Home/Index` = 200.
  (Lưu ý harness: lần thử trước dùng field `usercode` bị server báo "Tên người dùng
  không được để trống" vì signature là `CheckLogin(string username, …)` —
  `LoginBaseController.cs:79`; sửa field là thành công, không liên quan mật khẩu.)
- OR prod 5231: cùng cách → success, `GET /Home/Index` = 200.
- EMR prod 5235: cùng cách → success, `GET /Home/Index` = 200.
- Không lần nào báo khóa/sai mật khẩu. Prod chỉ đọc/validation-gate + chụp Home
  (§8); không có ghi nào lên prod, không tạo fixture trên prod.

## §3. Bug mới tìm được (live trên staging HEAD, có bước tái hiện)

| ID | App / action | Mức | Tái hiện | Mong đợi vs thực tế | Bằng chứng |
|---|---|---|---|---|---|
| C2-B1 | MRMS Web, `RecordManage/MrDept`: `GetFormJson`, `SubmitDicDept` (sửa, có keyValue), `DeleteData` | Cao (luồng sửa/xem/xóa khoa bệnh án gãy) | Login staging MRMS → tạo khoa bệnh án mới bằng `SubmitDicDept` (thành công, có Id) → gọi `GetFormJson?keyValue=<Id>`, hoặc sửa bằng `SubmitDicDept` kèm keyValue, hoặc `DeleteData` kèm keyValue | Trả entity / cập nhật / xóa rồi báo thành công | Thực tế: cả 3 đều trả `{"state":"error","message":"The parameter format is wrong. \nUse new{xx=xx, xx2=xx2} or \nDictionary<string, object> or \nSugarParameter [] "}` kèm `exStackTrace` (SqlSugar `AdoAccessory.GetParameters`); dòng vẫn còn nguyên, tên không đổi. Điểm chung: cả 3 đều đi qua `MrdicdeptRepo.FindEntity(keyValue)` (repo `MrdicdeptRepo.cs:53,78`; controller `MrDeptController.cs:164,175` và nhánh sửa ở `SubmitForm:53`); trong khi `GetPagintionDicDeptList` (không qua FindEntity) vẫn chạy tốt. Tạo mới (không keyValue) vẫn thành công. Chưa xác định được nguyên nhân gốc trong `RepositoryBase.FindEntity` vì implementation nằm trong DLL Lib, không có source tại đây — ghi đúng giới hạn này, không đoán |
| C2-B2 | MRMS Web, `RecordManage/MrFee/submitForm` TẠO MỚI (keyValue rỗng) | Cao (không tạo được nhóm phí mới) | Login staging MRMS → POST `submitForm` với keyValue rỗng (đã thử cả dạng field `Code/Name/Lev` và đúng dạng UI `name1/code1/Lev=1, px=99`) | Tạo dòng `mr_dic_bafeetype` mới, báo thành công | Thực tế: `{"state":"error","message":"Object reference not set to an instance of an object."}` kèm `exStackTrace` tại `MrFeeRepo.SubmitForm:40`. Nguyên nhân đọc từ source: controller luôn gọi `_MrFeeDmnService.GetFormJson(keyValue, entity.Lev)` kể cả khi tạo mới; với keyValue rỗng, `GetFormJson` trả null, và repo dereference `oldEntity.Lev` ngay dòng 40 (`MrFeeRepo.cs:40`: `(oldEntity.Lev == 1 && …)`) mà không kiểm null. Không tạo dòng nào. (Lưu ý: đường dẫn stack trace trong JSON lỗi của bản staging trỏ tới thư mục build scratch của worker này — đó là artifact của bản build scratch, không phải đường dẫn prod; bản thân việc JSON lỗi kèm `exStackTrace` thì cần vòng fix quyết có tắt ở prod hay không, giống ghi nhận phase 2 về stack trace client) |
| C2-B3 | OR Web, `Operation/OPStaff/GetGridList` với keyword | Thấp (pager lệch khi tìm kiếm) | Login staging OR → xóa mềm 1 nhân sự mổ (Code=SWEEPC2, zt→0) → gọi `GetGridList?rows=20&page=1&keyword=SWEEPC2` | rows rỗng và total/records = 0 (khớp nhau) | Thực tế: `rows=[]` nhưng `records=1, total=1` (bằng metadata của list không keyword). Grid không keyword sau xóa trả đúng 1 dòng đang hoạt động và loại dòng đã xóa, nên phần lọc rows là đúng; chỉ metadata total/records không khớp rows khi có keyword. Chưa xác định count query bỏ qua keyword hay bỏ qua zt — chỉ ghi quan sát. (Đối chứng: MRMS `GetPagintionDicDeptList?keyword=SWEEPC2` trả total/records khớp rows) |
| C2-B4 | EMR Web, `MRHomePage/MRHomePage/GetGridList` (grid trang đầu tiên của EMR) | Cao (grid trang chính EMR gãy cả prod lẫn staging) | Login staging EMR → gọi `GetGridList` (không tham số đặc biệt) | Trả rows trang đầu bệnh án | Thực tế: `{"state":"error","message":"Invalid object name 'dbo.mr_dic_common'."}` kèm stack SqlClient. Nguyên nhân source: `MRHomePageDmnService.cs:62` làm `left join [dbo].[mr_dic_common] …` trong context DB hiện tại (EmrDb). Kiểm chứng SQL: `mr_dic_common` = 0 dòng trong `Newtouch_EMR_STG` VÀ 0 dòng trong `Newtouch_EMR` (prod), nhưng 1 dòng trong `Newtouch_MRMS_STG` và 1 dòng trong `Newtouch_MRMS` (prod) — tức grid đầu trang EMR gãy cả trên prod, không chỉ staging, trong khi grid MainRecord của MRMS chạy được vì DB của nó có bảng này. Ghi nhận kèm: `EMR CommonDmnService.cs:571` truy vấn `[Newtouch_MRMS].dbo.[mr_dic_common]` với tên DB prod viết cứng — là một read leak về phía staging-isolation cùng họ với C2-B6, chưa được phase 7 rà (phase 7 chỉ sửa đường ghi) |
| C2-B5 | EMR Web, `MedicalRecordManage/NursingRecord/GetGridJson` KHÔNG tham số ngày | Thấp (robustness, luồng chính không gãy vì UI luôn gửi ngày) | Login staging EMR → gọi `GetGridJson` không `kssj`/`jssj`, không `hllx` (đi nhánh else) | Trả `[]` hoặc lỗi tham số rõ ràng | Thực tế: `{"state":"error","message":"Nullable object must have a value."}` từ `NursingRecordDmnService.GetPaginationList` — code làm `kssj.Value` / `jssj.Value` không kiểm null (`pars.Add(new SugarParameter("@kssj", kssj.Value.ToString()))`, `jssj.Value.AddDays(1)`). Cùng endpoint với ngày (`kssj=2024-01-01&jssj=2026-12-31&zyh=01000`) trả `[]` thành công (STG không có bản ghi điều dưỡng cho bệnh nhân/khoảng đó) |
| C2-B6 | MRMS Web, `RecordManage/Record/LockRecord` — đường ĐỌC hardcode DB prod, đường GHI qua DbNames | Cao trên staging (khóa được nhưng không mở được qua controller; trên prod 2 đường trùng nhau nên không lộ) | Login staging MRMS → `LockRecord` khóa bản ghi `BlId=5a93fe30-…`, bllx=1, `isToLock=1` → gọi mở khóa `isToLock=0` | Khóa rồi mở được, trạng thái về ban đầu | Thực tế: khóa CÓ hiệu lực trên STG (SQL: `bl_rybl` STG IsLock→1, `LastModifyTime` = giờ gọi) nhưng mở khóa trả `success "Chưa bị khóa"` và không mở, vì controller kiểm tra trạng thái qua `GetMedicalRecord` → `bl_ryblGetByID` hardcode `select * from [Newtouch_EMR].[dbo].bl_rybl` (`RecordDmnService.cs:137`), đọc prod twin (IsLock=NULL) thay vì STG. Cùng file còn 5 helper hardcode tương tự: `bl_bcjlGetByID` (:156), `bl_zqwsGetByID` (:173 — lưu ý helper này truy vấn bảng `bl_bcjl`, không phải `bl_zqws`, nghi copy-paste), `bl_hljlGetByID` (:191), `bl_basyGetByID` (:208), `zy_meddocs_relation` (:226). Đây là read leak cùng họ phase 7 nhưng ở đường đọc, phase 7 chưa rà. Dòng STG đã khôi phục bằng SQL (IsLock về NULL, khớp prod twin). Ghi nhận kèm: message thành công của `LockRecord` luôn là "Mở khóa thành công" kể cả khi gọi khóa (`RecordController.cs:233-260` trả cùng message ở mọi đường về) — lỗi message gốc, mức thấp |

Quan sát chung (không lập bug riêng): nhiều JSON lỗi trong phiên kèm `exStackTrace`
đầy đủ (MRMS C2-B1/C2-B2, lỗi SQL/Redis, lỗi EMR) — cùng pattern middleware; vòng fix
quyết có tắt ở prod hay không.

## §4. OR staging (HEAD build, cổng 15231) — đã chạy xong đọc + ghi + B3, đã kill

- Boot: login page 200, PID 35716. Login: 2 lần đầu thất bại do lỗi SQL staging (§0b);
  lần thử lại sau khi SQL hồi phục thành công: `{"state":"success","message":
  "Đăng nhập thành công"}`, `GET /Home/Index` = 200. App đã kill cuối phần OR, cổng
  15231 xác nhận DOWN, PID không còn.
- Đọc OR (session staging, thử lại từng request một sau khi hạ tầng hồi phục):
  `OpRoom/GetGridList` ĐẠT (rows, gồm "Phong mo so 1"); `OpApply/ApplyIndex` HTML
  ĐẠT (200, 35.563 byte); root `ORApplyInfo/Index` 302 (redirect về ApplyIndex theo
  fix phase 2 — đúng kỳ vọng, chưa kiểm chứng Location header); `OpApply/GetPatGridList`
  (không tham số và với `bq=00000012`) trả `rows=[]` cả 2 lần — ghi dạng rỗng/phụ
  thuộc dữ liệu, chưa kết luận là bug (điều kiện lọc của grid bệnh nhân này chưa
  xác lập được); `OpArrangement/GetGridList` ĐẠT (rows, gồm dòng probe G6);
  `OPStaff/GetGridList` ĐẠT (1 dòng đang hoạt động); `OpOperation/GetGridList` ĐẠT
  (rows, có tên phẫu thuật tiếng Trung trong DB); `OpNotchGrade/GetGridList` ĐẠT;
  `OPAnesthesia/GetGridList` ĐẠT; `SysChargeTemplate/ChargeTemplate_GetGridJson`
  ĐẠT (200, `rows=[]` — STG chưa có mẫu phí); `ChargeTemplateIndex` HTML ĐẠT
  (200, 8.893 byte); `Operation/Common/GetRoomlist` ĐẠT (rows phòng mổ);
  `OpFeeReport/Index` HTML ĐẠT (200, 7.417 byte); `OpRegisterReport/Index` HTML
  ĐẠT (200, 7.458 byte); `ReportManage/Report/Index` HTML ĐẠT (200, 5.330 byte,
  nội dung stub SSRS; root `/Report/Index` và `/Operation/Report/Index` là 404 —
  route area đúng là `ReportManage`, controller ở
  `Areas/ReportManage/Controllers/ReportController.cs`).
- Lỗi hạ tầng trong đợt cao tải (không phải lỗi logic grid): loạt `GetGridList` của
  OpArrangement, OpRegister, OPStaff, OpOperation, OpNotchGrade, OPAnesthesia và cả
  `ChargeTemplateIndex` đều timeout phía client trong đợt cao tải; log OR ghi rõ
  `RedisTimeoutException` — GET session trên Redis staging 6380 timeout 5s trong
  `OperatorProvider.GetCurrent`; thử lại đơn lẻ sau đó vẫn trả lỗi kết nối SQL
  (HTTP 200 kèm error JSON tiếng Trung). Tất cả đều ĐẠT khi thử lại từng request
  một lúc SQL/Redis đã ổn — kết luận: các grid OR chạy được, các lần lỗi trước
  là hạ tầng.
- Ghi OR — phòng mổ: `OpRoom/submitForm` TẠO ĐẠT (Code=SWEEPC2, SQL xác nhận dòng
  mới), `GetFormJson` ĐẠT (trả đúng dòng), `submitForm` SỬA ĐẠT (Name đổi thành
  `…-EDIT` theo SQL), `DeleteData` ĐẠT DẠNG XÓA MỀM — báo success và `zt` đổi 1→0
  (đúng source `ORRoomRepo.DeleteForm:95-101`: `dbEntity.zt="0"` + Update, không
  xóa vật lý); fixture đã dọn vật lý bằng SQL, count=0. Đây là đối chứng cho B3:
  xóa mềm có đổi trạng thái thật, khác hẳn root DeleteForm không đổi gì.
- Ghi OR — đơn mổ đơn lẻ: `OpApply/SubmitForm` không zyh → lỗi đúng `"Thông tin
  bệnh nhân không được để trống"`; đủ field (zyh=01000, ssdm=SS01,
  ssmcn=SWEEP-C2-Mo, sssj=2026-11-01 09:00, ysgh=000000) → `"Gửi đơn thành công"`,
  applyno `S20261028A000001`, SQL xác nhận dòng (sqzt=1, thông tin bệnh nhân tự
  điền từ zyh: xm "A Binh", ks KS01); `GetGridListOP?zyh=01000` liệt kê đúng dòng
  mới; `DeleteApply` → `"Hủy đơn thành công"`, sqzt 1→3; dòng đã dọn vật lý bằng
  SQL, bảng về 2 dòng. Kiểm tra CIS STG: 0 dòng `zy_lsyz` mang `xmmc=SWEEP-C2-Mo`
  — SubmitForm không để lại side-effect CIS.
- Ghi OR — dict: `OpOperation/submitForm` TẠO ĐẠT (ssdm=SWEEPC2), `DeleteData`
  ĐẠT dạng xóa mềm (zt 1→0); `OpNotchGrade/submitForm` TẠO ĐẠT (Code=SWEEPC2),
  `DeleteData` ĐẠT dạng xóa mềm (zt 1→0); `OPStaff/submitForm` TẠO ĐẠT
  (Code=SWEEPC2, Id `ab1517d2-…`), `DeleteData` ĐẠT dạng xóa mềm (SQL xác nhận
  zt 1→0; grid sau xóa loại dòng này); `OPAnesthesia/submitForm` — 2 lần đầu lỗi
  transport SQL, SQL xác nhận 0 dòng (không insert một phần), lần 3 sau khi SQL
  hồi phục TẠO ĐẠT (AnesCode=SWEEPC2), `DeleteData` ĐẠT dạng xóa mềm (zt 1→0).
  Tất cả fixture dict OR đã dọn vật lý bằng SQL, count còn 0. (Bug C2-B3 phát hiện
  từ grid OPStaff sau xóa mềm — xem §3.)
- Ghi OR — mẫu phí (ghi vào Sett STG qua DbNames): `ChargeTemplate_SubmitForm`
  nhánh SỬA (sfmbbh có sẵn nhưng chưa tồn tại) trả success dù update 0 dòng —
  ghi nhận dạng quan sát (UI bình thường chỉ gọi edit với mã đã có), không lập
  bug riêng; nhánh TẠO MỚI (không sfmbbh, sfmb=SWEEPC2MB) ĐẠT — dòng `xt_sfmb`
  mới zt=1 trong Sett STG, đã dọn vật lý bằng SQL, count=0.
- Ghi OR — chuỗi đầy đủ trên artifact riêng (SQL xác nhận từng bước, đã dọn):
  tạo đơn mới qua `OpApply/SubmitForm` (zyh=01000, ssmcn=SWEEP-C2-Mo2,
  sssj=2026-11-02 09:00) → applyno `S20261028A000002`, Id
  `577bb5fa-…`, sqzt=1 (lưu ý: curl tạo đơn này timeout phía client nhưng dòng
  đã tồn tại — xem bài học §0b). `OpArrangement/submitForm` (keyValue=Id đơn,
  oproom=ss01, sssj như trên) → success, đơn sqzt 1→2, dòng sắp xếp mới Id
  `54e9c50a-…`, ApplyId=Id đơn, oproom=ss01, zt=1, sqzt=2 (ghi nhận: cột thô
  `OR_Arrangement.Applyno` là NULL; `GetArrangeObjByApplyId` điền applyno từ join
  nên hiển thị vẫn đúng — quan sát về denormalization, không phải bug).
  `OpArrangement/DeleteForm` (keyValue=Id đơn; controller tự suy ArrangeId) →
  success, sắp xếp sqzt 2→3, đơn sqzt về 1 (lần kiểm tra đầu của worker nhìn nhầm
  cột `zt` vốn vẫn 1; cột trạng thái là `sqzt`). `OpRegister/submitForm`
  (arrangeId=Id sắp xếp, applyId, applyno, zyh=01000, xm, không field nhân sự) →
  success, dòng đăng ký mới Id `cfab0ee9-…`, ssxh `S20261028A000001`, arrangeId
  khớp, zt=1, sắp xếp sqzt→4; `GetPagintionListForRegistration` sau đó liệt kê
  đúng dòng (sqzt=4, ssxh đã set). `OpRegister/DeleteData` (keyValue=Id đăng ký,
  arrangeId, ssxh) → success, đăng ký zt 1→0, sắp xếp sqzt 4→2. Sau đó `DeleteApply`
  hủy đơn (sqzt→3), và cả 3 dòng đã dọn vật lý bằng SQL. Count cuối đã xác nhận:
  `OR_ApplyInfo`=2, `OR_Arrangement`=1 (dòng G6), `OR_Registration`=0 — đúng
  baseline trước chuỗi (baseline: `OR_ApplyInfo` 2 dòng — `8d5cf668-…` applyno
  `S20261027A000001` sqzt=3 và probe G6 `G6-PROBE-OR`/`G6APPLY001` sqzt=0;
  `OR_Arrangement` 1 dòng G6).
- B3 live: xem §5 (đã xác nhận, đã dọn sạch). Các dòng probe G6 không bị sửa bởi
  kiểm thử B3.

## §4b. EMR staging (HEAD build, cổng 15235) — login thành công, đọc + ghi đã chạy xong

- Login: lần đầu bằng curl thành công ngay: `{"state":"success","message":"Đăng nhập
  thành công"}`, `GET /Home/Index` = 200. App đã kill cuối phần EMR, cổng 15235
  xác nhận DOWN, PID không còn.
- Đọc EMR (session staging):
  `MedicalRecordManage/PatInfo/PatList` ĐẠT (rows, bệnh nhân 01000 "A Binh",
  zyh 01000, blh 11000); `PatInfo/GetMedRecordTree?zyh=01000` ĐẠT (nodes, có tên
  bản ghi tiếng Trung trong DB, ví dụ "入院记录" — nội dung DB);
  `MedRecordTemplate/GetGridJson` ĐẠT (rows, đường dẫn file mẫu tiếng Trung trong
  DB, ví dụ `\\File\\Template\\住院文书\\入院记录.xml` — nội dung DB);
  `Bljgh/GetGridJson` ĐẠT (200, `rows=[]` — STG chưa có bản ghi cấu trúc);
  `Blys/GetGridJson` (không tham số, trả mọi dòng zt=1) ĐẠT (rows, có tên nguồn
  dữ liệu tiếng Trung trong DB, ví dụ "国籍" — nội dung DB);
  `MedicalRules/GetRulesList` ĐẠT dạng rỗng (`[]` — STG chưa có quy tắc; lưu ý
  action có thể nhận filter bllx nên rỗng không kết luận là bug);
  `NursingRecord/GetGridJson` CÓ ngày (`kssj=2024-01-01&jssj=2026-12-31&zyh=01000`)
  ĐẠT (`[]` — không có bản ghi điều dưỡng cho bệnh nhân/khoảng đó); KHÔNG ngày →
  lỗi C2-B5 (§3); `SystemManage/Common/GetBllxListGrid` ĐẠT (rows, ví dụ bllx
  `yzd`, "Y lenh don"); `MedicalRecord/RecordList` ĐẠT dạng trang (trả HTML view,
  không phải JSON grid — trang render với static ref tới `127.0.0.1:41142`; action
  grid JSON riêng không probe thêm); `MRHomePage/MRHomePage/GetGridList` LỖI —
  bug C2-B4 (§3); `MRHomePage/Report/BasyReport` HTML ĐẠT (200, 6.260 byte, hiện
  đúng thông báo SSRS tiếng Việt: "Trang đầu bệnh án Báo cáo trang đầu bệnh án cần
  máy chủ SSRS (ReportViewer.aspx) — chưa cấu hình ReportServer.HOST trong triển
  khai này nên không thể hiển thị báo cáo…" — khớp hành vi SWEEP-C §4b, live trên
  staging HEAD); `Blctgl/CTGLList` ĐẠT (200, 7.535 byte), `Blctgl/GetTreeList`
  ĐẠT dạng rỗng (`[]`); `Outpatient/PatList` ĐẠT (200, 5.962 byte);
  `TemplateDesign/Index` ĐẠT (200, 2.465 byte); `SystemManage/YzPreView/YzPreView`
  ĐẠT (200, 13.545 byte — lưu ý: route đúng là area `SystemManage`, không phải
  `MedicalRecordManage`; gọi sai area trước đó trả 404 do sai route, không phải
  bug), `YzPreView/GetZyCqYzData?zyh=01000` ĐẠT dạng rỗng (`[]` — không có y lệnh
  dài hạn cho 01000 ở STG). Lần đầu `MedRecordTemplate/GetGridJson` và
  `Bljgh/GetGridJson` lỗi kết nối SQL đúng đợt chập chờn (§0b); thử lại đơn lẻ
  đều thành công.
- Ghi EMR — mẫu bệnh án: `MedRecordTemplate/submitForm` TẠO: lần 1 (mbmc=
  SWEEP-C2-Mau, mbbm=SWEEPC2, bllx=yzd, không mbqx) lỗi SQL "Cannot insert NULL
  into column 'mbqx'"; lần 2 (mbqx=0) lỗi "Cannot insert NULL into column
  'CreatorCode'" (đường tạo của repo làm `Insert(entity)` thô, không tự set
  CreatorCode — UI phải post field này); lần 3 (mbqx=0, CreatorCode=000000)
  THÀNH CÔNG — dòng Id `52a9aed3-…`, zt=0 vì zt không post dạng "true" (repo map
  zt: `"true"`→"1", còn lại "0"). `GetFormJson` cho Id đó lần đầu trả rỗng (nhiều
  khả năng timeout đúng đợt chập chờn; endpoint còn nhánh blId/bllx nên chưa kết
  luận thêm). SỬA (mbmc→`SWEEP-C2-Mau-EDIT`, zt=true, keyValue=Id) ĐẠT — SQL xác
  nhận tên đổi và zt 0→1. `DeleteForm` ĐẠT — SQL xác nhận zt 1→0 (xóa mềm). Dòng
  đã dọn vật lý bằng SQL, count mbbm=SWEEPC2 = 0.
- Ghi EMR — nguồn dữ liệu (Blys): `Blys/SubmitForm` TẠO (ysmc=SWEEP-C2-Nguon,
  yscode=SWEEPC2, DataSource=zy_brjbxx): client timeout nhưng dòng đã tồn tại
  (Id `97b61a5f-…`, BindingPath tự set = yscode — xem bài học §0b).
  `Blys/DeleteForm` (delYSID=Id) ĐẠT — dòng biến khỏi list đang hoạt động (grep
  count 0); SQL xác nhận sau đó zt=0 (xóa mềm đúng thiết kế); dòng đã dọn vật lý
  bằng SQL, count = 0.
- Ghi EMR — phần tử cấu trúc (Bljgh): `Bljgh/SubmitForm` TẠO ĐẠT (Element_Name=
  SWEEP-C2-PhanTu, Element_Type=0 → mô tả "Văn bản", ElementId=0,
  Table_Name=zy_brjbxx, Table_Column_Code=xm, CreatorCode=000000) — dòng Id 3023
  (PK int) trong `bl_ElementDomain_Detail`, Zt=1. `Bljgh/DeleteForm`
  (delBljgmxId=3023) ĐẠT — Zt 1→0 (xóa mềm); đã dọn vật lý bằng SQL, count
  SWEEP-C2 = 0.
- Ghi EMR — quy tắc viết (MedicalRules): `MedicalRules/SubmitForm` TẠO ĐẠT
  (Bllx=yzd, RulesType=1, RulesDay=3, Remark=SWEEP-C2-QuyTac) — dòng Id
  `25150014-…` trong `Mr_Writing_Rules` (lưu ý: bảng là `Mr_Writing_Rules`, không
  phải `bl_WritingRules`), zt=1. `MedicalRules/RulesDel` (Id) ĐẠT — dòng biến
  khỏi bảng sau đó (xóa vật lý hoặc tương đương); DELETE đối chứng sau đó ảnh
  hưởng 0 dòng, count SWEEP-C2 cuối = 0.
- Ghi EMR — bản ghi điều dưỡng (NursingRecord): `NursingRecord/SubmitForm` TẠO:
  lần 1 (zyh=01000, không CreatorCode) lỗi "Cannot insert NULL into column
  'CreatorCode'" (cùng pattern repo không tự set CreatorCode như mẫu bệnh án);
  lần 2 (CreatorCode=000000) THÀNH CÔNG — dòng Id `b5fc9c5f-…`, zt=1
  (CreateTime mặc định 1900-01-01 vì không post ngày — giá trị mặc định của
  entity, không phải lỗi). `NursingRecord/DeleteForm` (keyValue=Id) ĐẠT —
  message "Hủy hiệu lực thành công。", SQL xác nhận zt 1→0 (xóa mềm); dòng đã
  dọn vật lý bằng SQL, count = 0.
- Ghi EMR — PatInfo (KHÔNG phải bug mới, là giới hạn triển khai đã được ghi trong
  code): `PatInfo/SubmitForm` (Zyh=01000, PatName, Bed, ApplyDeptName) và
  `PatInfo/GetMrqcScore` (zyh=01000, bllxId=`4dd08e85-…`) đều trả lỗi "An invalid
  request URI was provided. Either the request URI must be an absolute URI or
  BaseAddress must be set." (stack HttpClient). Nguyên nhân: 2 action này post
  sang `AuthenManageHelper.SiteConmonAPIHost + "api/MedicalRecordApply/…"` /
  `"api/MRQCScore/…"`; host center này không tồn tại trong triển khai hiện tại
  (center staging 18015/18016 không chạy; config `SiteConmonAPIHost` trống).
  Comment đầu file `EMR.Web/Helpers/AuthenHelper.cs` ĐÃ ghi rõ giới hạn này
  ("common-API host does not exist in this deployment … these two actions report
  the connection failure; the record pages themselves are unaffected") — tức đây
  là hành vi đã được ghi nhận từ trước, không phải bug logic mới; chỉ ghi nhận
  thêm là text lỗi trả về là exception HttpClient thô, chưa phải message thân
  thiện. Không tự dựng center staging host (ngoài phạm vi C2: "không đụng app
  khác"). Không có ghi nào từ 2 action này chạm DB (cả 2 đều lỗi trước khi ghi).
- Chưa chạy ở EMR (xem §7): `MedicalRecord/BL_Save` (cần nội dung bản ghi thật
  qua editor DCSoft — không khả thi trên Linux), `TemplateDesign/BtnSubmit`
  (cần editor), `PatInfo` Commit/BackPatMedRecord (cần quan hệ bản ghi có sẵn —
  không chạy trên dòng thật).

## §5. Kiểm chứng lại các bug B1–B5 của SWEEP-C

| ID | Kết quả C2 |
|---|---|
| B1 (banner IE trên Firefox, login EMR) | **ĐÃ XÁC NHẬN VẪN TỒN TẠI — staging HEAD + prod.** Ảnh login EMR staging (desktop + mobile, §8) và text body trang login prod đều chứa banner "Bạn vẫn đang dùng IE, Trước khi dùng hệ thống bình thường, vui lòng nâng cấp trình duyệt của bạn lên IE8 phiên bản trở lên…" hiển thị trên Firefox. Source HEAD vẫn là nguyên nhân cũ: `EMR/Views/Login/Index.cshtml:22` đặt `<div id="errorie">` KHÔNG bọc conditional comment, trong khi MRMS/OR vẫn bọc |
| B2 (nhãn tab login EMR wrap) | **ĐÃ XÁC NHẬN VẪN TỒN TẠI — staging HEAD + prod.** Ảnh login EMR staging desktop cho thấy rõ: nhãn tab wrap 2 dòng "Chào mừng" / "đăng nhập" (dòng 2 in đậm, đè lên mép trên của ô nhập tài khoản). Source HEAD vẫn là nhãn "Chào mừng đăng nhập" (`Index.cshtml:41`) dài hơn "Chào mừng" của MRMS/OR |
| B3 (OR `ORApplyInfoController.DeleteForm` no-op giả thành công) | **ĐÃ XÁC NHẬN LIVE** trên staging HEAD (session OR): tạo 2 fixture `OR_ApplyInfo` trên STG (sqzt=1, zt=1, OrganizeId demo) → POST root `/ORApplyInfo/DeleteForm` với keyValue=Id fixture 1 trả `{"state":"success","message":"Thao tác thành công."}` nhưng SQL sau đó cho thấy dòng **không đổi gì** (sqzt vẫn 1, zt vẫn 1, vẫn tồn tại) → đúng là no-op giả thành công. Đối chứng area POST `/Operation/OpApply/DeleteApply` với keyValue=Applyno fixture 2 trả `"Hủy đơn thành công"` và sqzt đổi 1→3 (soft-cancel thật) → chứng tỏ harness đo đúng và chỉ đường root là no-op. Cả 2 fixture đã dọn bằng SQL, bảng về đúng 2 dòng ban đầu, SWEEPC2 còn 0. Ở mức source HEAD (đối chiếu sâu hơn SWEEP-C): (1) controller root `ORApplyInfoController.cs:68-72` vẫn comment repo call, luôn `Success`; (2) NUANCE MỚI: call bị comment là `_oRApplyInfoRepo.DeleteForm(keyValue)` 1 tham số, nhưng interface hiện tại `IORApplyInfoRepo.DeleteForm` yêu cầu 3 tham số `(keyValue, orgId, ssmcn)` — tức bỏ comment nguyên văn cũng KHÔNG biên dịch được, quyết định sản phẩm phải gồm cả sửa signature; (3) bản thân `ORApplyInfoRepo.DeleteForm` (3 tham số) cũng KHÔNG xóa vật lý mà chỉ soft-cancel (`sqzt = yqx = 3`) + cập nhật `zy_lsyz` bên CIS (`zt=0`) — nên "xóa" ở đây theo thiết kế gốc là hủy đơn, không phải xóa dòng. CHỈ ghi nhận, không tự bỏ comment |
| B4 (nền login mobile bị cắt) | **ĐÃ XÁC NHẬN VẪN TỒN TẠI (mức thấp) — staging HEAD + prod.** Ảnh login EMR mobile 390×844 (§8): họa tiết nền (vòng tròn HIS) bị cắt ở biên trái/phải, nhưng form đăng nhập vẫn căn giữa và dùng được — đúng như SWEEP-C đã ghi, không chặn luồng. Form dùng được nên đây là vấn đề thẩm mỹ responsive, không phải lỗi chức năng |
| B5 (envelope EMR API lệch) | **ĐÃ XÁC NHẬN VẪN TỒN TẠI TRÊN PROD (sau redeploy 23/23)** ở đường validation: `POST 5335/api/Auth/GetToken {}` và `POST 5335/api/Default/Query {}` đều trả HTTP 400 ProblemDetails `{"errors":{"AppId":["AppId là bắt buộc"]}}`; đối chứng cùng body trên MRMS API 5332 trả envelope `{"code":40004,"sub_code":"TIMESTAMP_IS_REQUIRED","sub_msg":null}`. Đây là probe validation-gate chỉ-đọc, không cần login, không tốn lượt khóa. Lưu ý phân biệt: với token hợp lệ, EMR API vẫn trả envelope bình thường (xem §6 — Query qua header token trả `code:10000`); lệch envelope chỉ ở đường model-validation `[Required]` của ASP.NET (ProblemDetails) so với envelope tự viết của MRMS/OR |

## §6. API với token thật — đã chạy được trên prod (chỉ đọc)

- MRMS API 5332: `POST /api/Auth/GetToken` (Account 000000 + Password md5, TokenType
  MRMS) → `code:10000` + token được cấp (giá trị token không ghi vào báo cáo).
  `POST /api/Default/Query` kèm token trong body → `code:10000`, trả danh sách
  `Sys_Config`. Cùng token với TokenType sai → `40004 TOKEN_TYPE_NOT_MATCH`
  ("Loại token truy cập không khớp") — khóa TokenType theo API hoạt động đúng.
- OR API 5331: `GetToken` (TokenType OR) → `10000` + token. `POST
  /api/ORPatient/ORPatList` (OrganizeId demo, zyh=01000, khoảng ngày 2024–2026)
  → `10000`, `data: []` (prod không có đăng ký mổ cho 01000 trong khoảng đó —
  rỗng hợp lý, không phải lỗi). `POST /api/UA/GetUserInfo` với raw token: lần
  đầu thiếu Timestamp → `40004 TIMESTAMP_IS_REQUIRED`; thêm Timestamp →
  `10000`, `Account: 000000`, `OrganizeId` = demo org — đổi token lấy identity
  hoạt động đúng.
- EMR API 5335: `GetToken` (TokenType EMR) → `10000` + token (dạng 32 ký tự hex,
  khác dạng GUID của MRMS/OR — do `SaveIdentity` của EMR dùng
  `Guid.NewGuid().ToString("N")`). `POST /api/Default/Query` với token trong
  BODY JSON → `40004 TOKEN_IS_EXPIRED` ("Token không hợp lệ hoặc đã hết hạn");
  cùng token qua HEADER `token:` → `10000` + danh sách `Sys_Config`. Kết luận:
  **EMR API chỉ nhận token qua header, không đọc field Token trong body JSON**
  (getter `Identity` trong `ApiControllerBase.cs` đọc header `token` trước, fallback
  không phải body JSON) — đây là khác biệt contract transport so với MRMS/OR
  (nhận Token trong body), ghi nhận dạng quan sát/khác biệt contract, không lập
  thành bug logic vì token hợp lệ vẫn dùng được qua header. Lưu ý kèm:
  `SaveIdentity` của EMR nuốt lỗi Redis (catch rỗng) rồi vẫn trả token — nếu
  Redis của EMR API chết, token được cấp nhưng mọi call sau đều TOKEN_IS_EXPIRED;
  trong phiên này Redis sống nên không gặp trường hợp đó.
- `BljghDataDealwith` (EMR API): KHÔNG GỌI — đây là endpoint GHI (repo
  `BljghDataDealwith` xử lý/ghi dữ liệu cấu trúc), gọi trên prod sẽ vi phạm quy
  tắc "prod chỉ đọc". Trên staging thì không có EMR API host (theo task, không
  tự tạo). Ghi rõ là gap còn lại duy nhất của nhóm API (xem §7).

## §7. Phần chưa test được + lý do (không tuyên bố xong)

1. **MRMS**: `Record/PatList` với tham số ngày/zyh cụ thể (chưa thử lại; bản
   không tham số trả rỗng cả 3 type); `MrFeeItem/Save*`, `MrFeeRel/Save`,
   `MainRecord/SubmitZdList/SubmitOpList/SubmitMR` (cần list do UI dựng hoặc có
   nguy cơ ghi đè dữ liệu của dòng probe dùng chung — không chạy mù); báo cáo
   SSRS nội dung thật + editor DCSoft (không chạy được trên Linux, xem SWEEP-C
   §4a/§4b); ảnh MrDept mobile (screenshot timeout ở viewport mobile do trang
   nặng — bản desktop đã có, §8).
2. **OR**: `OpApply/GetPatGridList` với tham số đầy đủ (2 lần thử trả rỗng — điều
   kiện lọc của grid này chưa xác lập được, cần đối chiếu query/filter trước khi
   kết luận); root `ORApplyInfo/Index` 302 — chưa kiểm chứng Location header.
3. **EMR**: `MedicalRecord/BL_Save` (cần nội dung bản ghi thật qua editor DCSoft —
   không khả thi trên Linux); `TemplateDesign/BtnSubmit` (cần editor);
   `PatInfo` CommitPatMedRecord/BackPatMedRecord (cần quan hệ bản ghi có sẵn —
   không chạy trên dòng thật); `MedRecordTemplate/GetFormJson` trên Id mẫu có
   sẵn (lần thử trên Id fixture trả rỗng, nhiều khả năng timeout đợt chập chờn —
   chưa thử lại trên Id có sẵn); PatInfo SubmitForm/GetMrqcScore chỉ chạy được
   khi có center common-API host (giới hạn triển khai đã ghi trong code, xem
   §4b — không phải bug mới).
4. **API**: `BljghDataDealwith` dạng ghi (xem §6 — cần API staging hoặc môi
   trường ghi cho phép; không gọi trên prod).
5. **Dư lượng duy nhất sau dọn dẹp**: `LastModifyTime` của dòng `bl_rybl` STG
   (BlId `5a93fe30-…`) bị đổi thành giờ gọi khóa trong kiểm thử LockRecord;
   IsLock đã về NULL, `Blzt` về 0 — chỉ là timestamp, không đổi trạng thái.
   Mọi fixture SWEEPC2 khác đã xác nhận count=0 trên STG (MRMS dic/rel/basy,
   OR apply/arrangement/registration/room/operation/notch/staff/anesthesia/
   charge-template, EMR template/blys/bljgh/rules/nursing; CIS STG 0 dòng
   SWEEP-C2).

## §8. Ảnh đã chụp (Playwright + Firefox local, `domcontentloaded` — không dùng `networkidle` vì kẹt)

Lưu tại `~/workspace/openhis-core/shots-sweep/` (đã gitignore theo pattern `shots*/`).
"Mobile" = emulation viewport 390×844; desktop = 1440×900. OpenHIS không có app
native — không ở đâu tuyên bố đã test app native.

| File | Nội dung |
|---|---|
| `sweep-c2-emr-Login_Index-desktop.png` | Login EMR staging — thấy banner IE (B1) + nhãn tab wrap đè ô nhập (B2) |
| `sweep-c2-emr-Login_Index-mobile.png` | Login EMR staging mobile — banner IE, nhãn wrap, nền cắt biên (B4), form vẫn dùng được |
| `sweep-c2-emr-home-Home_Index-desktop.png` | Home EMR staging sau login — menu Quản lý hệ thống / Quản lý bệnh án / Thông tin bệnh nhân… |
| `sweep-c2-emr-home-Home_Index-mobile.png` | Home EMR staging mobile |
| `sweep-c2-emr-home-MedicalRecordManage_PatInfo_Index-desktop.png` | Trang Thông tin bệnh nhân EMR staging — tab Đang nằm viện / Ra viện / Của tôi |
| `sweep-c2-emr-home-MedicalRecordManage_PatInfo_Index-mobile.png` | Trang Thông tin bệnh nhân EMR staging mobile |
| `sweep-c2-mrms-home-Home_Index-desktop.png` | Home MRMS staging sau login |
| `sweep-c2-mrms-home-Home_Index-mobile.png` | Home MRMS staging mobile |
| `sweep-c2-mrms-home-RecordManage_MrDept_Index-desktop.png` | Trang Khoa bệnh án MRMS staging (desktop; bản mobile timeout khi chụp do trang nặng) |
| `sweep-c2-mrms-prod-Home_Index-desktop.png` | Home MRMS prod sau login |
| `sweep-c2-mrms-prod-Home_Index-mobile.png` | Home MRMS prod mobile |
| `sweep-c2-or-prod-Home_Index-desktop.png` | Home OR prod sau login — "Hệ thống phòng mổ", chọn khu vực làm việc |
| `sweep-c2-or-prod-Home_Index-mobile.png` | Home OR prod mobile |
| `sweep-c2-emr-prod-Home_Index-desktop.png` | Home EMR prod sau login |
| `sweep-c2-emr-prod-Home_Index-mobile.png` | Home EMR prod mobile |
| `sweep-c2-emr-prod-login-Login_Index-desktop.png` | Login EMR prod — banner IE (B1) + nhãn "Chào mừng…" (B2) |
| `sweep-c2-emr-prod-login-Login_Index-mobile.png` | Login EMR prod mobile — banner IE, nhãn wrap, nền cắt biên (B4) |

Lưu ý kỹ thuật khi chụp prod: cookie session prod được set HttpOnly (phase 7) nên
file cookie của curl có dòng tiền tố `#HttpOnly_`; script chụp đã sửa để đọc được
dòng này (lần chụp đầu bỏ qua nên về trang login — đã chụp lại đủ sau khi sửa).

## §9. Cam kết cuối phiên — sweep C2 ĐÃ HOÀN TẤT trong phạm vi cho phép

- Không sửa code, không commit, không sửa `roles-shots-progress.md`, không sửa
  launcher repo, không đổi mật khẩu SA, không ghi DB demo (mọi ghi đều ở DB
  `*_STG`; tổng số lệnh ghi vào DB demo trong phiên này: 0 — ngoại trừ việc mở
  khóa tài khoản demo prod do chính điều phối viên thực hiện, không phải worker
  này). HEAD cuối phiên vẫn `e66c7bda`; `git status` chỉ có `M
  roles-shots-progress.md` từ trước + `?? scripts/sweep-final/`.
- Mật khẩu demo chỉ dùng đúng giá trị đã công khai trong task; không đoán mật khẩu
  khác; không lần login nào báo khóa/sai mật khẩu sau thông báo mở khóa; mọi lần
  login thất bại trước đó đều được đối chiếu log (lỗi SQL) trước khi thử lại.
- Tất cả staging của worker này đã kill đúng PID, cổng 15232/15231/15235 xác nhận
  DOWN, PID không còn. Tối đa 2 app staging cùng lúc trong suốt phiên.
- Tất cả fixture SWEEPC2 đã xác nhận count=0 trên STG (trừ dư lượng timestamp duy
  nhất ở §7.5); các dòng probe G6/phase-6 đã về đúng baseline (MRMS basy bazt=2,
  OR apply/arrangement/registration về baseline, EMR bl_rybl IsLock=NULL).
- Kết luận: **phần nợ §6 của SWEEP-C đã chạy xong** — MRMS/OR/EMR staging đều
  login và chạy đọc + ghi (kèm 6 bug mới C2-B1…C2-B6, trong đó C2-B4 gãy cả trên
  prod), B3 xác nhận live là no-op giả thành công, B1/B2/B4 xác nhận vẫn tồn tại
  (staging + prod), B5 xác nhận vẫn tồn tại ở đường validation, login prod 3 app
  thành công + Home prod 200 + ảnh prod, API token thật chạy được trên prod cho
  cả 3 API (chỉ đọc). Phần còn lại là các mục §7 (không chạy mù, cần dữ liệu/
  điều kiện cụ thể hơn hoặc môi trường ghi cho phép) và vòng fix cho C2-B1…C2-B6
  + B1…B5 — thuộc quyết định sản phẩm/vòng fix sau, không thuộc phạm vi sweep
  này (sweep chỉ ghi nhận, không sửa code).
