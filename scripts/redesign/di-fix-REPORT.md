# Báo cáo sửa lỗi DI — `AddDependencyService` đăng ký nhầm marker interface

Worker: SỬA LỖI BACKEND (phạm vi hẹp, một phương thức duy nhất), chương trình redesign OpenHIS.
Ngày: 2026-10-28. **KHÔNG commit, KHÔNG triển khai lên prod** (điều phối viên soát rồi mới commit).

## 1. Kết quả tóm tắt

- **Đã sửa xong + kiểm chứng xong.** SSO từ HEAD (trước đây abort ở Development) nay khởi động được,
  trang login trả 200, log không còn lỗi DI validation.
- **File code sửa duy nhất:** `Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Extension/ServiceExtensions.cs`,
  chỉ phương thức `AddDependencyService()`. Không đổi chữ ký, không thêm service, không sửa
  controller/domain service nào, không đổi cơ chế quét assembly, không đổi lifetime mặc định.
- **Bằng chứng quyết định (không phải ăn may):** trên CÙNG một thư mục có thứ tự readdir xấu
  (copy nguyên thư mục foundation), DLL cũ abort đúng lỗi gốc, chỉ thay duy nhất
  `NewtouchHIS.Lib.Base.dll` bằng DLL đã sửa thì login 200. Xem §4.

## 2. File sửa và diff

Đường dẫn (từ repo `openhis-core`):
`Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Extension/ServiceExtensions.cs` — phương thức `AddDependencyService()`.

Logic mới (thay cho 3 nhánh `FirstOrDefault` cũ):

```csharp
// Marker interfaces must never be registration targets. Every domain
// interface inherits one of them, so the old FirstOrDefault over the
// scanned interface list could pick the marker (interface order
// follows Directory.GetFiles order, which is filesystem order and
// unsorted) and leave the domain interface unregistered, aborting
// startup at DI validation. Register the class under EVERY
// non-marker interface it implements instead, so the result no
// longer depends on enumeration order at all. Lifetime rules are
// unchanged from the old code.
var markerTypes = new HashSet<Type>
{
    typeof(IDependency),
    typeof(ITransientDependency),
    typeof(ISingletonDependency),
    typeof(IScopedDependency),
};
foreach (var implementType in implementTypes)
{
    var domainInterfaces = interfaceTypes
        .Where(x => !markerTypes.Contains(x) && x.IsAssignableFrom(implementType))
        .OrderBy(x => x.FullName, StringComparer.Ordinal)
        .ToList();
    if (domainInterfaces.Count == 0)
    {
        // Marker-only class (no domain interface): keep the old
        // behaviour so it stays resolvable exactly as before.
        var fallbackInterfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
        if (fallbackInterfaceType == null)
            continue;
        domainInterfaces.Add(fallbackInterfaceType);
    }
    foreach (var interfaceType in domainInterfaces)
    {
        if (typeof(IScopedDependency).IsAssignableFrom(implementType))
            services.AddScoped(interfaceType, implementType);
        else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
            services.AddSingleton(interfaceType, implementType);
        else
            services.AddTransient(interfaceType, implementType);
    }
}
```

Điểm khác so với yêu cầu (có lý do, vẫn trong phạm vi "loại trừ marker"):

- Ngoài 3 marker được nêu (`IDependency`, `IScopedDependency`, `ISingletonDependency`), bản sửa
  loại trừ thêm `ITransientDependency` — marker thứ tư trong `IDependency.cs` mà code cũ cũng
  mắc cùng lỗi (class transient mà marker liệt kê trước thì cũng đăng ký nhầm).
- Đăng ký vào **MỌI** domain interface class đó implement (theo đề xuất "tốt hơn nữa" trong task),
  sắp xếp theo `FullName` để thứ tự descriptor ổn định. Lifetime giữ nguyên quy tắc cũ
  (Scoped → Singleton → Transient).

`git diff --stat` cuối đợt (xem §7): chỉ file trên đổi (+50/−12 dòng trong phương thức);
`roles-shots-progress.md` là thay đổi có sẵn từ trước đợt này (foundation-REPORT §6 đã ghi), để nguyên.

## 3. Cơ chế lỗi — đã xác nhận bằng số liệu, không chỉ suy luận

Thứ tự readdir thô (`ls -U`, chưa sắp xếp) của 2 thư mục:

| Thư mục | `NewtouchHIS.Lib.Base.dll` | `NewtouchHIS.Base.Domain.dll` | Kết quả DLL cũ |
|---|---|---|---|
| `~/workspace/foundation-scratch/sso/` (thư mục foundation, đã biết lỗi) | vị trí **1** | vị trí 4 | **Abort** (xem §4) |
| `~/workspace/di-fix-scratch/sso1/` (build scratch mới đợt này) | vị trí 151 | vị trí 147 | Chạy được (ăn may) |

Tức `Directory.GetFiles` trả `Lib.Base.dll` trước → `IScopedDependency` (nằm trong assembly đó)
được quét trước các domain interface (nằm trong `Base.Domain.dll`) → `FirstOrDefault` chọn marker →
`ISysRoleDmnService`/`ISysStaffDmnService` không có registration nào → validation abort.
Cùng code, khác thư mục → khác kết quả: đúng bản chất xổ số theo thứ tự filesystem như chẩn đoán.

## 4. Kiểm chứng (STG-only, prod chỉ đọc; chỉ GET trang login, không ghi DB demo)

Hạ tầng STG dùng trong đợt này: Redis 6380, BaseAPI STG 17001 (launcher gốc, bin cũ của nó
vẫn chạy được nhờ thứ tự bin ổn định — ngoài phạm vi sửa, chỉ dùng làm API host cho SSO),
DB `*_STG`. Tất cả tiến trình scratch đã dừng cuối đợt; prod (5208/5237/6379) không bị chạm.

Quy ước build (quan trọng, đọc kỹ trước khi đánh giá lại):

- Prod SSO/PDS đang chạy từ `bin/Release` trong cây source (PID 2524/3488), nên `dotnet publish -c Release`
  bình thường sẽ ghi đè vào bin đang bị khóa (lỗi MSB3027 với PDB). Vì vậy các build scratch đợt này
  dùng **`-c Debug`** (ghi vào `bin/Debug` tách biệt, không đụng bin prod). Validation DI abort
  là do môi trường `Development` (ValidateOnBuild), không phụ thuộc cấu hình Debug/Release,
  nên kiểm chứng vẫn có giá trị đầy đủ.
- SSO và PDS đều tham chiếu `Lib.Base` qua **HintPath DLL, không phải ProjectReference**
  (SSO → `his-dll-common/Net6/NewtouchHIS.Lib.Base.dll`, đã track trong git;
  PDS → bản DLL trong cây run-copy). Vì vậy sau mỗi lần publish, DLL trong **thư mục scratch**
  (app chưa chạy) được thay bằng DLL `Lib.Base` build từ source đã sửa, rồi mới chạy app.
  **File DLL chung trong repo KHÔNG bị ghi đè** (xem hệ quả triển khai ở §6).
- MD5 (tính bằng `md5sum`, không gõ tay): DLL `Lib.Base` đã sửa `8eaae85e513ca28d0c06f79eae30eb2c`;
  DLL HintPath cũ của SSO `1cfb5299281dcac3fb8a433a8dd5f20f`; DLL HintPath cũ của PDS
  (run-copy) `fa27fc8f0878e636a85f8b75e6a10c61` — ba bản khác nhau, đúng như kỳ vọng.

### 4.1. Đối chứng: DLL cũ abort đúng lỗi gốc

| Chạy | Cổng | Kết quả |
|---|---|---|
| Thư mục foundation nguyên trạng, DLL cũ (`sso-stg.log` gốc + chạy lại) | 15210 | **Abort**, log: `Unable to resolve service for type '...ISysRoleDmnService'` và `'...ISysStaffDmnService'`, tiến trình thoát |
| Copy nguyên thư mục foundation → `di-fix-scratch/sso-fcopy/`, DLL cũ (thứ tự readdir giữ nguyên: Lib.Base #1, Base.Domain #4) | 15211 | **Abort**, cùng 2 lỗi trên, tiến trình thoát |

### 4.2. Kiểm chứng chính: DLL đã sửa trên cùng thứ tự xấu đó

| Chạy | Cổng | Kết quả |
|---|---|---|
| `sso-fcopy/` chỉ thay `NewtouchHIS.Lib.Base.dll` → bản đã sửa, thứ tự readdir không đổi | 15211 | **Login 200**, log: `Hosting environment: Development`, `Now listening on: http://127.0.0.1:15211`, **0** lần `Unable to resolve service` |

### 4.3. Kiểm chứng theo đúng yêu cầu task (build scratch mới hoàn toàn)

| # | Chạy | Cổng | Kết quả |
|---|---|---|---|
| 1 | SSO build scratch mới #1 (`di-fix-scratch/sso1/`, Debug publish, DLL đã sửa) | 15208 | **Login 200**, Development, 0 lỗi DI trong `sso1.log` |
| 2 | SSO build scratch mới #2 (`di-fix-scratch/sso2/`, build lại từ đầu, DLL đã sửa) | 15212 | **Login 200**, Development, 0 lỗi DI trong `sso2.log` |
| 3 | PDS build scratch mới (`di-fix-scratch/pds1/`, DLL đã sửa) — hồi quy tối thiểu | 15237 | **Login 200**, Development (xem ghi chú dưới), 0 lỗi DI trong `pds1.log` |

Ghi chú PDS:

- PDS hardcode `UseUrls("http://127.0.0.1:5237")` thắng `ASPNETCORE_URLS`, nên đã patch tạm
  literal 5237→15237 trong `Program.cs` để build, **khôi phục ngay sau build và xác nhận
  `git diff` của file đó rỗng** (theo đúng cách foundation-REPORT §5.6).
- Script chạy PDS đợt này đặt `ASPNETCORE_ENVIRONMENT=Development` thay vì `Staging` như launcher
  STG gốc, để ép validation DI lúc khởi động (ở Staging lỗi thiếu service chỉ lộ khi resolve,
  không abort lúc khởi động). Trang login PDS vẫn 200 trong cả hai môi trường về nguyên tắc,
  nhưng con số 200 ở trên là đo ở Development.
- Lần chạy PDS đầu tiên cho thấy khởi động có thể chậm (>10s mới có listener/log): các lần đo
  sau đều chờ 20–25s trước khi curl, tránh kết luận sai "không lên".

### 4.4. Phát hiện phụ trong lúc đối chứng (không phải lỗi mới, mà là bằng chứng xổ số)

DLL cũ chạy trong thư mục scratch mới `sso1` lại **lên được** (đối chứng `sso1-oldctrl`, cổng 15209,
login 200, đã kill sau đó). Điều này không mâu thuẫn với chẩn đoán — nó chính là mặt còn lại
của xổ số: thư mục mới có thứ tự readdir "may mắn" (Base.Domain trước Lib.Base, §3).
Vì vậy **chỉ báo "build mới chạy được" là chưa đủ**; bằng chứng có giá trị là §4.2
(cùng thư mục xấu, cũ abort → mới chạy).

## 5. Service nào đổi registration? (mục 4 của task)

Quét tĩnh trên cây source Center + SSO + PDS (khai báo `class`/`interface` một dòng):

- 54 domain interface (kế thừa marker, không tính 4 marker).
- **Không có** class nào implement trực tiếp ≥2 domain interface.
- **Không có** chuỗi kế thừa interface-domain-nào-kế-thừa-interface-domain-nào.
- **Không có** class marker-only (chỉ implement marker, không có domain interface).
- Không có code nào resolve trực tiếp `IScopedDependency`/`ISingletonDependency`/
  `ITransientDependency` ngoài `ServiceExtensions.cs` và `IDependency.cs`.

Kết luận:

- Service trước đây "ăn may" (domain interface liệt kê trước marker): **giữ nguyên registration**
  — cùng service type, cùng implementation, cùng lifetime. Không service nào mất registration cũ.
- Service trước đây đăng ký nhầm vào marker: nay đăng ký đúng vào domain interface của nó
  (đó chính là bản sửa, không phải hồi quy). Ví dụ trong SSO: `SysConfigDmnService`,
  `SysModuleDmnService`, `SysUserVDmnService` và các service cần `ISysRoleDmnService`/
  `ISysStaffDmnService` — trước đây abort, nay resolve được.
- Trường hợp duy nhất có thể "thêm" registration so với code cũ: class implement một domain
  interface mà interface đó lại kế thừa một domain interface khác (nay đăng ký cả hai).
  Không phát hiện trường hợp nào trong 3 cây đã quét; các assembly khác trong bin SSO/PDS
  (Framework, Lib.Services…) không nằm trong phạm vi quét tĩnh này, nhưng đều đã đi qua
  validation DI lúc khởi động ở Development trong 3 lần kiểm chứng trên nên không còn
  descriptor nào thiếu dependency mà validation bắt được.
- Nhánh fallback marker-only được giữ lại đúng hành vi cũ để không đổi gì với class
  (nếu có ở assembly ngoài phạm vi quét) chỉ implement marker.

## 6. Việc điều phối viên / người triển khai PHẢI làm tiếp (bản sửa source chưa đủ để chạy prod)

1. **Build lại `NewtouchHIS.Lib.Base.dll` từ source đã sửa và đưa vào các vị trí HintPath**
   (`his-dll-common/Net6/` cho SSO/Center và vị trí run-copy cho PDS cùng các app khác),
   vì các app không tham chiếu Lib.Base bằng ProjectReference. Worker cố tình KHÔNG ghi đè
   file DLL đã track trong git — quyết định commit DLL rebuild là của điều phối viên.
   Cho tới khi DLL được rebuild, app nào build từ HEAD mà vẫn dùng DLL HintPath cũ thì
   vẫn có thể abort đúng lỗi cũ (tùy thứ tự thư mục).
2. Sau khi DLL mới vào vị trí HintPath, nên chạy lại một lượt khởi động Development cho
   các app còn lại (ít nhất các app từng dùng `AddDependencyService`) để xác nhận không còn
   descriptor nào đăng ký vào marker — lỗi này có thể đánh trúng bất kỳ app nào khi deploy
   vào thư mục mới.
3. Không cần thay đổi gì thêm ở `ServiceExtensions.cs` cho đợt này; nếu sau này có class
   implement nhiều domain interface thì hành vi mới (đăng ký tất cả) là hành vi mong muốn,
   không phải tác dụng phụ cần dọn.

## 7. Vệ sinh và những gì KHÔNG làm

- **Không commit** (kể cả không `git add` gì). `git status` cuối đợt:
  `M Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Extension/ServiceExtensions.cs` (của worker này)
  và `M roles-shots-progress.md` (có sẵn từ trước, không phải của worker này).
  Không có DLL/PDB/scratch/build artifact nào lọt vào repo (scratch ở
  `~/workspace/di-fix-scratch/`, ngoài repo; log kiểm chứng cũng ở đó:
  `sso1.log`, `sso2.log`, `pds1.log`, `fcopy-fixed.log`, `fcopy-old.log`, `foundation-asis.log`,
  `baseapi-stg.log`).
- Không triển khai lên prod; không chạm tiến trình prod (SSO 5208, PDS 5237, Redis 6379,
  SQL Server) — chỉ đọc PID/cmdline để xác nhận, không kill.
- Không ghi DB demo: mọi env đều trỏ DB `*_STG` + Redis 6380; đợt này chỉ GET trang login,
  không POST đăng nhập (không dùng/không hỏi credential), không ghi thêm dòng nào.
- Không copy DLL dưới tiến trình đang chạy: mọi lần thay DLL scratch đều thực hiện khi
  app đích chưa chạy (hoặc đã thoát sau abort), và đã xác nhận bằng `ps` trước khi copy
  ở lần thay trong thư mục fcopy.
- `Program.cs` của PDS đã khôi phục, diff rỗng (§4.3).
- SA password giữ nguyên; không có secret nào trong báo cáo này (mật khẩu chỉ nằm trong
  file env có sẵn, không trích dẫn).
- Tất cả tiến trình kiểm chứng (SSO 15208/15212, PDS 15237, BaseAPI STG 17001, Redis STG 6380)
  đã dừng cuối đợt; các cổng scratch đều trống. Muốn chạy lại: Redis STG bằng
  `redis-server ~/workspace/redis-stg/redis-stg.conf` với `LD_LIBRARY_PATH` tới
  `~/workspace/redis-libs/usr/lib/x86_64-linux-gnu`, BaseAPI STG bằng
  `openhis-core/run-baseapi-stg.sh`, SSO/PDS STG bằng các script trong `di-fix-scratch/`
  (cần build lại DLL đã sửa vào scratch trước khi chạy).
- Không chụp ảnh (task không yêu cầu; mobile = giả lập viewport nếu có chụp — không áp dụng
  ở đây). Footer tiếng Trung và receipt-prefix ngoài phạm vi, không đụng tới.
