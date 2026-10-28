# Báo cáo đợt PER-APP (B) — Khai cột neo + chuẩn hóa nút chính trên lưới/toolbar

> Worker đợt PER-APP (B), chương trình REDESIGN UI/UX OpenHIS.
> **KHÔNG commit, KHÔNG deploy prod.** Mọi thay đổi chỉ nằm trong repo dưới
> dạng chưa commit + build scratch Debug ngoài repo.
> Mobile = giả lập viewport (Playwright + Firefox local), không phải app native.
> Mật khẩu chỉ truyền qua biến môi trường tạm thời của phiên probe, không ghi
> vào file, báo cáo, log hay ảnh. Chỉ đọc, không submit nghiệp vụ, không ghi
> DB nào (kể cả STG, ngoài session login).

## 0. Kết luận nhanh + sự cố chặn cuối phiên (đọc trước)

| Hạng mục | Kết quả |
|---|---|
| Khai `frozen:true` theo hồ sơ A | ✅ 12 file view đã sửa (bảng §1); 2 lưới kiểm chứng live kích hoạt thành công (EMR NursingRecord, PDS StockQuery) |
| Chuẩn hóa nút chính | ✅ Toolbar/vùng thao tác các trang đã sửa: đúng 1 nút chính (bảng §2); kiểm chứng live trên EMR + PDS (desktop + mobile) và Herp (desktop) |
| Nút "Sửa" vô hiệu khi chưa chọn dòng | ✅ Trạng thái vô hiệu ban đầu đo live được trên EMR + Herp kho + Herp mua hàng; **bật lại khi chọn dòng CHƯA đo live được** (xem §4) |
| Đo cuộn đối chứng (neo đứng yên / cột sau dịch chuyển) | ❌ **CHƯA đo được trên lưới nào** — xem §5 (lý do phương pháp + số đo) |
| Herp kho frozen | ❌ **CHƯA ĐẠT live — bị vendor chặn** (treeGrid guard, §3.1) |
| Herp mua hàng frozen theo tên audit ("Số đơn hàng") | ❌ **CHƯA ĐẠT theo tên audit** — vendor chỉ cho neo tiền tố liên tục (§3.2); đã chuyển sang phương án dự phòng cột đầu tiên, **chưa kịp đo live** |
| Sett DaySett, MRMS, OR, Base HOSP/Web | ⏳ Chỉ kiểm chứng từ source (chưa có STG trong đợt này / SQL chết trước khi probe Sett) |
| **SỰ CỐ: SQL Server chết cuối phiên** | 🚨 **sqlservr không còn tiến trình, cổng 1433 refused** — mọi login STG (và cả login prod, vì chung 1 SQL Server) đều lỗi DB. Worker **KHÔNG tự restart SQL** theo quy tắc brief, đã dừng probe và dọn STG. **Cần điều phối viên khôi phục SQL (recovery.sh) rồi mới probe tiếp được.** Chi tiết §6. |

### Thứ tự đọc

- Muốn biết "cái nào đạt": §1 (lưới) + §2 (nút) + §4 (số đo live).
- Muốn biết "cái nào chưa đạt và vì sao": §3 (phát hiện vendor) + §4
  (cột "Chưa đo") + §6 (SQL) + §7 (việc cần điều phối viên).

## 1. Bảng lưới × cột neo đã khai + lý do audit

Quy tắc áp dụng thống nhất: chỉ neo **cột định danh đầu tiên** (hoặc nhóm
định danh tối thiểu theo audit). Cột khóa ẩn đứng trước cột định danh cũng
được khai `frozen:true` để tiền tố frozen liên tục — đây là tiền lệ sẵn có
trong repo (NursingRecord: `Id` ẩn frozen; Combination/Index: `ypId` ẩn
frozen). Không dùng CSS theo vị trí DOM ở bất kỳ đâu trong đợt này.

| # | App / trang / lưới | Cột neo đã khai (name) | Lý do theo hồ sơ audit | Trạng thái live |
|---|---|---|---|---|
| 1 | EMR — `MedicalRecordManage/NursingRecord/Index`, `#gridList` | (không sửa — đã đúng) `Id` ẩn + `zyh` "Số nhập viện" + `xm` "Họ tên" + `rq` "Ngày" | AUDIT-SYNTHESIS §3: A, "neo Số nhập viện/Họ tên/Ngày". Đây là lưới duy nhất đã khai frozen thật trước đây; kiểm tra lại: khai báo đúng, tiền tố liên tục, trang tự gọi `setFrozenColumns` (dòng ~190) | ✅ **ĐẠT kích hoạt**: `frozenColumns=true`, frozenHeaders live = [`Id`, `Số nhập viện`, `Họ tên`, `Ngày`] (desktop + mobile) |
| 2 | PDS — `StorageManage/DrugStockSearch/StockQuery`, `#gridYPZL` | `ypmc` "Tên thuốc" | audit-pds-base §1.4(e): A, "cột neo cố định cho cột Tên thuốc". Lưới chi tiết `#gridYPMXInfo` **cố ý không neo** (cột đầu của nó là "Số lượng tồn kho", không phải định danh) | ✅ **ĐẠT kích hoạt**: `frozenColumns=true`, frozenNames live = [`rn`, `ypmc`], frozen-bdiv có mặt (desktop + mobile) |
| 3 | Herp — `WarehouseManage/Warehouse/Index`, `#gridList` | `Id` ẩn + `name` "Tên kho" | audit-cis-herp §2.3 + §4.4: A, "bổ sung cột neo 'Tên kho'" | ❌ **CHƯA ĐẠT live**: `frozenColumns=false` dù tiền tố hợp lệ — bị guard treeGrid của vendor (§3.1). Cờ vẫn giữ trong source để ghi nhận ý đồ audit |
| 4 | Herp — `PurchaseManage/Purchase/Index`, `#gridList` | **Sau điều chỉnh**: `cgId` ẩn + `ddsj` "Thời gian đơn hàng" (xem §3.2). Ban đầu khai `ddbh` "Số đơn hàng" rồi **đã gỡ** vì vendor không bao giờ kích hoạt nó một mình | audit-cis-herp §2.4 + §4.4: A, "cột neo 'Số đơn hàng' + sửa để cột phải tới được". Tên audit không thực hiện được bằng một cờ frozen đơn lẻ trong jqGrid 5.1 (§3.2) | ⏳ **CHƯA đo live với tiền tố mới** (Herp đã build lại nhưng SQL chết trước probe vòng 2). Với cờ cũ đã đo live: `frozenColumns=false`, prefix = −1 |
| 5 | OR — `Operation/OpStaff/Index`, `#gridList` | `Id` ẩn + `Code` "Mã nhân viên" | audit-mrms-or-emr §3.3: A mặc định (danh mục nhân sự cần đối chiếu ngang; mobile cũ mất 4/6 cột do nén) | ⏳ Source-only (chưa build STG OR trong đợt này) |
| 6 | MRMS — `RecordManage/MrDept/Index`, `#gridList1` (chính) + `#gridList` (Khoa liên kết) | `gridList1`: `Id` ẩn + `ksbm` "Mã khoa bệnh án". `gridList`: `Id` ẩn + `hisdept` "HISMã" | AUDIT-SYNTHESIS §3: MrDept = A | ⏳ Source-only. Lưu ý: `#gridList` là lưới sửa inline (cột sửa là `baksName`, không nằm trong cột neo) nên neo không chặn sửa |
| 7 | MRMS — `RecordManage/MrFee/Index`, `#gridList` | `id1`/`id2`/`id3` ẩn + `code1` "Mã nhóm cấp 1" | AUDIT-SYNTHESIS §3: MrFee = A | ⏳ Source-only |
| 8 | Sett — `DaySettleManage/DaySettle/InpatientDaySett`, `#gridList` | `Id` ẩn + `bcje` "này lần Số tiền quyết toán" (cột hiện đầu tiên của lưới) | audit-sso-mrqc-sett-internal §Sett-05 + AUDIT-SYNTHESIS §3: DaySett A, "đã cuộn ngang nhưng thiếu cột neo". **Audit không nêu tên cột neo cho DaySett** nên lấy cột hiện đầu tiên theo quy tắc chung; cần điều phối viên xác nhận tên cột (xem §7) | ⏳ Source-only + STG đã build (15221, login page 200) nhưng **chưa probe trình duyệt** (SQL chết trước) |
| 9 | Sett — `DaySettleManage/DaySettle/OutpatientDaySett`, `#gridList` | Giống (8): `Id` ẩn + `bcje` | Cùng colModel với bản nội trú; làm cùng để khỏi lệch 2 biến thể DaySett | ⏳ Như (8) |
| 10 | Base HOSP — `ChargeCategory/Index`, `#gridList` | `dlId` ẩn + `dlmc` "Tên" | audit-pds-base §2.3(e): A, "cuộn ngang + frozen cột Tên", 14 cột | ⏳ Source-only |
| 11 | Base HOSP — `Sys/SysGlobalConfig/Index`, `#gridList` | `Id` ẩn + `Name` "Tên" | audit-pds-base §3.3(e): **chọn A, không chọn C** — lý do audit ghi rõ: "C chỉ nếu mobile chỉ kiểm tra trạng thái nhanh, nhưng A an toàn hơn do có cột Giá trị cần đối chiếu" | ⏳ Source-only |
| 12 | Base Web — `SysGlobalConfig/Index`, `#gridList` | `Id` ẩn + `Name` "Tên" | Cùng hồ sơ với (11) (audit gộp HOSP/Base Web; route đúng `/SysGlobalConfig/Index`) | ⏳ Source-only |

**Không nằm trong đợt này (đúng phạm vi):** các lưới chiến lược B
(PatientInHosList, BookkeepInHos, PDS-03 — B là chính, A+frozen chỉ là dự
phòng), OpApply (xếp pane, không phải lưới A), layout scaffold khu vực
(CIS Doctor/Nurse, Sett PatientManage, SSO `_Layout` legacy — khác stack,
để chặng riêng theo brief), CIS (không nằm trong danh sách đợt B).

### File đã sửa (12 file, KHÔNG commit, KHÔNG `git add`)

- `Newtouch.HIS.EMR/Newtouch.EMR.Web/Areas/MedicalRecordManage/Views/NursingRecord/Index.cshtml` (chỉ nút, không đụng colModel đã đúng)
- `Newtouch.HIS.PDS/Newtouch.PDS.Web/Areas/StorageManage/Views/DrugStockSearch/StockQuery.cshtml`
- `Newtouch.HIS.Herp/Newtouch.Herp.Web/Areas/WarehouseManage/Views/Warehouse/Index.cshtml`
- `Newtouch.HIS.Herp/Newtouch.Herp.Web/Areas/PurchaseManage/Views/Purchase/Index.cshtml`
- `Newtouch.HIS.OR/Newtouch.OR.Web/Areas/Operation/Views/OpStaff/Index.cshtml`
- `Newtouch.HIS.MRMS/Newtouch.MRMS.Web/Areas/RecordManage/Views/MrDept/Index.cshtml`
- `Newtouch.HIS.MRMS/Newtouch.MRMS.Web/Areas/RecordManage/Views/MrFee/Index.cshtml`
- `Newtouch.Sett.Web/Areas/DaySettleManage/Views/DaySettle/InpatientDaySett.cshtml`
- `Newtouch.Sett.Web/Areas/DaySettleManage/Views/DaySettle/OutpatientDaySett.cshtml`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web/Views/ChargeCategory/Index.cshtml`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web/Areas/Sys/Views/SysGlobalConfig/Index.cshtml`
- `Newtouch.HIS.Base/Newtouch.HIS.Base.Web/Views/SysGlobalConfig/Index.cshtml`

Lưu ý: repo hiện có thêm nhiều file sửa khác của worker song song
(chủ yếu `Views/Home/*`); 12 file trên là toàn bộ của đợt B, không đụng
tới file của worker khác. `roles-shots-progress.md` (M) đã sửa sẵn từ
trước phiên, để nguyên.

## 2. Bảng toolbar × nút chính trước/sau

Quy tắc: mỗi `.toolbar` (hoặc vùng thao tác tương đương khi trang không có
`.toolbar`) chỉ còn **đúng 1** `.btn-primary` là hành động chính; nút còn
lại xuống `btn-default` (hoặc `btn-info` giữ nguyên ngữ nghĩa phụ ở Herp
mua hàng trước khi đổi — xem bảng); nút nguy hiểm (Xóa/Hủy) thành
`btn-danger` và tách `margin-left:16px` (ghi trực tiếp lên chính nút đó,
không dùng selector vị trí DOM); nút icon-only có `aria-label` + `title`
tiếng Việt; nút "Sửa" thêm lớp vô hiệu trực quan khi chưa chọn dòng
(handler `btn_edit()` vốn đã chặn bằng modal "Vui lòng chọn một bản ghi"
nên lớp này chỉ là trạng thái hiển thị, không đổi luồng nghiệp vụ).

Đếm từ source (đo trong lát `.toolbar` tới `.search` kề sau) + đo live
khi có STG. Nút tìm kiếm icon-only nằm trong vùng `.search` (ngoài
`.toolbar`) được giữ `btn-primary` vì là hành động chính của vùng lọc
riêng, nhưng đã gắn `aria-label="Tìm kiếm"` + `title="Tìm kiếm"`.

| Trang / vùng | Trước | Sau (nút chính duy nhất in đậm) | Kiểm chứng |
|---|---|---|---|
| Herp kho — `.toolbar` | 5× primary: reload(icon), **Thêm mới**, Sửa, Xóa, Đồng bộ vật tư | reload→default + aria "Tải lại"; **Thêm mới** primary; Sửa→default; Xóa→danger + margin-left 16px; Đồng bộ vật tư→default | ✅ Live desktop: `.toolbar .btn-primary` = ["Thêm mới"]; nút Sửa `disabled` ban đầu = true |
| Herp mua hàng — khối nút cuối trang (không có `.toolbar`) | 4× `btn-info` giống hệt nhau (Hủy, Truyền báo, Sửa, Tạo mới) + Tra cứu primary ở vùng lọc | **Tạo mới**→primary; Sửa→default; Truyền báo→default; Hủy→danger + margin-left 16px (từ 20px); Tra cứu giữ primary (vùng lọc riêng) | ✅ Live desktop: bottom panel chỉ Tạo mới là primary; `btn_edit.disabled` ban đầu = true |
| EMR NursingRecord — hàng nút dưới vùng lọc (không có `.toolbar`) | 4× primary: **Nhập mới**, Sửa, Hủy hiệu lực, In (+ Tìm kiếm primary ở hàng lọc) | **Nhập mới** primary; Sửa→default; Hủy hiệu lực→danger + margin-left 16px; In→default; Tìm kiếm giữ primary (hàng lọc riêng) | ✅ Live desktop + mobile: chỉ Nhập mới là primary trong hàng nút; `btn_edit.disabled` ban đầu = true cả 2 viewport |
| PDS StockQuery — hàng nút trong panel lọc (không có `.toolbar`) | 3× primary: **Tra cứu**, In, Tra cứu thuốc hết hạn | **Tra cứu** primary; In→default; Tra cứu thuốc hết hạn→default | ✅ Live desktop + mobile: chỉ Tra cứu là primary |
| OR OPStaff — `.toolbar` | 4× primary: reload(icon), **Tạo mới**, Sửa, Xóa | reload→default + aria "Tải lại"; **Tạo mới** primary; Sửa→default; Xóa→danger + margin-left 16px | ⏳ Source (toolbar slice = 1 primary "Tạo mới") |
| MRMS MrDept — `.toolbar` | 4× primary: reload(icon), **Tạo mới**, Sửa, Xóa | Như OR | ⏳ Source (1 primary "Tạo mới") |
| MRMS MrFee — `.toolbar` | 4× primary: reload(icon), **Tạo mới**, Sửa, Xóa | Như OR | ⏳ Source (1 primary "Tạo mới") |
| HOSP ChargeCategory — `.toolbar` | 2× primary cạnh nhau: **Tạo mới**, Sửa (đúng lỗi audit §2.3) | **Tạo mới** primary; Sửa→default | ⏳ Source (1 primary "Tạo mới") |
| HOSP SysGlobalConfig — `.toolbar` | 3× primary: reload(icon), **Tạo mới**, Sửa | reload→default + aria "Tải lại"; **Tạo mới** primary; Sửa→default | ⏳ Source (1 primary "Tạo mới") |
| Base Web SysGlobalConfig — `.toolbar` | 3× primary: reload(icon), **Tạo mới**, Sửa | Như HOSP SysGlobalConfig | ⏳ Source (1 primary "Tạo mới") |
| Sett DaySett (nội + ngoại trú) — `.toolbar` | 1× primary: reload(icon) duy nhất | reload→default + aria "Tải lại" → `.toolbar` còn **0 primary**. Hành động chính thật của trang là F6 "Xác nhận quyết toán"/F7 "Hủy quyết toán" nằm trong partial dùng chung `_BottomButtonsView` — **không đụng** (đụng partial sẽ ảnh hưởng mọi trang Sett) | ⏳ Source (0 primary trong `.toolbar`, reload có aria). Ghi nhận để điều phối viên quyết có chuẩn hóa partial chung ở chặng khác không |

### Cơ chế "Sửa chỉ bật khi có dòng được chọn" (đã thêm, chưa đo bật-lại live)

Mỗi trang có nút Sửa (trừ Sett DaySett) được thêm một block `<script>`
cuối file, dùng JS sẵn có của trang (jQuery + jqGrid, không viết lại
luồng nghiệp vụ):

- Dạng anchor (`.toolbar a[onclick="btn_edit()"]` — Herp kho, OR, MRMS,
  Base): gắn/bỏ class `disabled` (Bootstrap 3.3.4 chặn click qua
  `pointer-events: none` cho `a.btn.disabled`) + `aria-disabled`, theo sự
  kiện `jqGridSelectRow` / `jqGridGridComplete` / `jqGridAfterGridComplete`
  của đúng lưới mà `btn_edit()` đọc (`gridList`, riêng MrDept là
  `gridList1`), và gọi ngay một lần lúc tải (chưa chọn → vô hiệu).
- Dạng `<input>` (EMR `#btn_edit`, Herp mua hàng `#btn_edit`): dùng
  `.prop("disabled", ...)`; mua hàng dùng cả `selrow` và `selarrrow`
  (lưới multiselect).
- Tất cả bọc `try/catch` để lớp hiển thị này không bao giờ làm hỏng trang
  nếu lưới chưa khởi tạo. Cú pháp JS của 9 block đã qua `node --check`.
- **Chưa đo live "bật lại khi chọn dòng":** EMR lưới rỗng trên STG
  (0 dòng — không chọn được dòng nào mà không bịa dữ liệu); Herp kho
  có 1 dòng trên STG nhưng lần đo click diễn ra khi probe đọc
  `records=0` (dữ liệu tải chậm hơn thời điểm đo) nên click không trúng
  dòng; Herp mua hàng (4 dòng) chưa kịp đo click trước khi SQL chết.

## 3. Phát hiện vendor quyết định (đọc kỹ trước khi sửa tiếp)

Vendor jqGrid 5.1.0 đang phục vụ từ static host
(`lmsoft_openhis_run/Newtouch.HIS.Static/.../js/jqgrid/jqgrid.min.js`,
hàm `setFrozenColumns`) có **hai luật cứng** mà hồ sơ audit không nêu:

### 3.1. Guard thoát im lặng — frozen + treeGrid/subGrid/cellEdit/sortable/scroll = không neo

```js
if ($t.p.subGrid===true || $t.p.treeGrid===true || $t.p.cellEdit===true || $t.p.sortable || $t.p.scroll) { return; }
```

Không lỗi, không cảnh báo — `setFrozenColumns` chỉ đơn giản không làm gì.
Herp kho rơi đúng trường hợp này: trang khai `treeGrid: true`
(`treeGridModel: "adjacency"`, `ExpandColumn: "address"` — hiển thị kho
phân cấp, là chức năng có chủ đích, không phải cấu hình thừa). Số đo
live: `treeGrid=true`, `subGrid/cellEdit/sortable/scroll=false`,
`prefixMaxFrozen=2` (hợp lệ) nhưng `frozenColumns=false`, kể cả khi gọi
trực tiếp `$('#gridList').jqGrid('setFrozenColumns')` trong trình duyệt.
**Kết luận: kho Herp không thể có cột neo bằng jqGrid frozen chừng nào
còn treeGrid.** Lựa chọn thuộc về điều phối viên: giữ treeGrid (không
neo) hoặc tách hiển thị phân cấp khỏi lưới neo (việc thiết kế lại, ngoài
đợt B). Cờ `frozen:true` đã khai vẫn giữ trong source (tiền tố hợp lệ,
sẽ tự kích hoạt nếu treeGrid bị gỡ trong tương lai).

### 3.2. Quy tắc tiền tố — chỉ neo được các cột frozen liên tục từ đầu (sau rn/cb)

Sau khi bỏ qua cột số dòng (`rn`) và cột checkbox multiselect (`cb`),
hàm quét colModel và **dừng ở cột đầu tiên không frozen**:

```js
while (i < len) { if (cm[i].frozen === true) { maxfrozen = i; } else { break; } i++; }
if (maxfrozen >= 0 && frozen) { /* tạo frozen-div/frozen-bdiv */ }
```

Hệ quả cho Herp mua hàng: khai một mình `ddbh` ("Số đơn hàng", cột hiện
thứ 4 sau `cgId` ẩn, `ddsj`, `psdbm`, `ddlx`) **không bao giờ kích hoạt**
— đo live với cờ cũ: `frozenNames=[rn, cb, ddbh]`, `prefixMaxFrozen=−1`,
`frozenColumns=false`, các tham số guard đều false. Muốn neo đúng
"Số đơn hàng" phải neo cả tiền tố `cgId + ddsj + psdbm + ddlx + ddbh`
(≈ 480px cột hiện + rn/cb) — trên mobile 390px, khối neo sẽ che hết
vùng cuộn, đúng trường hợp brief cấm ("frozen nhiều cột dễ vỡ bố cục").
**Phương án đã chọn trong đợt (ghi rõ là lệch tên audit):** neo cột hiện
đầu tiên `ddsj` "Thời gian đơn hàng" (+ `cgId` ẩn) — tiền tố hợp lệ,
chiều rộng neo ≈ 165px (rn + cb + 120px), an toàn cho mobile; "Số đơn
hàng" vẫn tới được bằng cuộn ngang (cần đo live sau khi SQL hồi phục).
**Lựa chọn thay thế cho điều phối viên:** đưa `ddbh` lên đầu colModel
rồi neo nó (đổi thứ tự cột hiển thị — ngoài phạm vi "chỉ khai frozen"
của đợt B, worker không tự quyết).

### 3.3. Hệ quả cho các lưới còn lại

- EMR NursingRecord, PDS StockQuery: tiền tố hợp lệ + guard đều false →
  kích hoạt được (đã đo live, §4).
- Sett DaySett, MRMS, OR, Base: tiền tố hợp lệ (khóa ẩn + cột đầu) +
  không có treeGrid/cellEdit trong code trang → **dự kiến kích hoạt
  được**, nhưng chưa đo live nên không tuyên bố đạt.
- Helper `redesign-grid.js` (`autoWrapGrids` → `activateFrozen`) hoạt
  động đúng như thiết kế: nó đọc colModel live và chỉ gọi
  `setFrozenColumns` khi có cờ frozen; thất bại ở Herp kho/mua hàng cũ
  nằm ở vendor + khai báo, không nằm ở helper.

## 4. Số đo probe live (Playwright + Firefox, tài khoản 000000)

STG dựng đúng thứ tự: Redis STG 6380 (PID 13479) → BaseAPI STG 17001
(PID 13503, script index-wire có sẵn) → EMR STG 15235 (PID 14709) → Herp
STG 15236 → PDS STG 15237 → Sett STG 15221. Build: **chỉ Debug publish**
(`dotnet publish -c Debug -o scratch`), mỗi lần 1 app; app hardcode
`UseUrls` được patch literal cổng lúc build rồi khôi phục ngay, `git
diff` của 5 file `Program.cs` đều rỗng sau build. DLL
`NewtouchHIS.Lib.Base` trong scratch đã thay bằng bản sửa DI
(md5 `8eaae85e…`, trùng `his-dll-common/Net6`) **trước khi chạy app**.
0 lỗi `Unable to resolve service` trên cả 5 log STG. Chỉ đọc, không
submit nghiệp vụ, không ghi DB.

### 4.1. EMR — NursingRecord (`/MedicalRecordManage/NursingRecord/Index`)

| Chỉ số | Desktop 1440 | Mobile 390 |
|---|---|---|
| Login | ✅ attempt 1 → `/Home/Index` | ✅ attempt 1 → `/Home/Index` (các lần chạy r2/r3; lần chạy r5 mobile bị server từ chối — xem §6, không retry) |
| Wrapper `.rd-grid-scroll` | 1 (lưới `#gridList`; 2 lưới phụ `gridListwz`/`gridListss` đang ẩn nên helper bỏ qua đúng thiết kế) | 1 |
| `.frozen-bdiv` / `frozenColumns` | ✅ true | ✅ true |
| Frozen headers live | [`Id`, `Số nhập viện`, `Họ tên`, `Ngày`] | như desktop |
| Bdiv (tự nhiên) | scrollW = clientW = 3496 (lưới nở full tổng cột vì trang standalone thiếu iframe portal — xem §5) | scrollW = clientW = 3496 |
| Hàng nút | chỉ Nhập mới primary; Sửa default + `disabled` ban đầu = true; Hủy danger | như desktop |
| Cuộn đối chứng | ❌ chưa đo (xem §5) | ❌ chưa đo |

### 4.2. PDS — StockQuery (`/StorageManage/DrugStockSearch/StockQuery`)

| Chỉ số | Desktop 1440 | Mobile 390 |
|---|---|---|
| Login | ✅ attempt 1 → `/Home/Index` | ✅ attempt 1 → `/Home/Index` |
| Wrapper `.rd-grid-scroll` | 2 (cả 2 lưới) | 2 |
| `#gridYPZL` frozen | ✅ `frozenBdiv=true`, `frozenColumns=true`, frozenNames = [`rn`, `ypmc`], frozenHeaders = [``, `Tên thuốc`] | như desktop (386px) |
| `#gridYPMXInfo` (chi tiết) | `frozenBdiv=false` — cố ý không neo | như desktop |
| Bdiv (tự nhiên) | 1436 = 1436 (lưới rỗng — StockQuery chỉ tải khi bấm Tra cứu, probe không submit) | 386 = 386 |
| Hàng nút | chỉ Tra cứu primary; In + Tra cứu thuốc hết hạn default | như desktop |
| Cuộn đối chứng | ❌ chưa đo (xem §5) | ❌ chưa đo |

### 4.3. Herp — kho + mua hàng (desktop; mobile chưa hoàn tất)

| Chỉ số | Kho desktop | Mua hàng desktop |
|---|---|---|
| Login | ✅ attempt 1 (lần chạy đầu); các lần sau chập chờn do handler bind chậm — xem §6.1 | ✅ attempt 1 (cùng phiên) |
| Wrapper | 1 | 2 (cả `#gridList` + `#gridDJMXInfo` — lần đo đầu chỉ thấy lưới chi tiết do đo quá sớm, lần chẩn đoán sau (chờ 4s) thấy đủ 2) |
| Frozen | ❌ `frozenColumns=false`, `treeGrid=true` (§3.1) | Với cờ cũ: ❌ `frozenColumns=false`, `prefixMaxFrozen=−1` (§3.2). Với tiền tố mới (`cgId`+`ddsj`): chưa đo live |
| Dữ liệu STG | `records` đọc được 0 (lần chẩn đoán đầu) rồi 1 (lần sau — tải chậm, không phải dữ liệu bịa) | `records=4` |
| Toolbar/nút | `.toolbar .btn-primary` = ["Thêm mới"]; Sửa `disabled` ban đầu = true | Bottom panel: chỉ Tạo mới primary; `btn_edit.disabled` ban đầu = true; Hủy danger |
| Cuộn tới cột phải | Chưa đo (lưới vừa viewport desktop) | Chưa đo live (cần mobile 390 — bị chặn ở §6) |

### 4.4. Sett / MRMS / OR / Base — chưa có số đo trình duyệt

- Sett STG 15221 đã dựng xong, login page 200, 0 lỗi DI, nhưng **chưa
  chạy probe trình duyệt** (SQL chết trước khi tới lượt Sett).
- MRMS / OR / Base HOSP / Base Web: **chưa build STG trong đợt này**
  (vượt mức tối thiểu EMR + PDS + Herp của brief; thời gian phiên đã
  dành cho chẩn đoán vendor + sự cố SQL). Chỉ có đếm source ở §2 và
  kiểm tra cú pháp JS.

## 5. Vì sao phép đo cuộn đối chứng chưa làm được (trung thực về phương pháp)

Tiêu chí brief: cột neo đứng yên khi cuộn ngang (x cột neo trước/sau
`scrollLeft` không đổi, cột sau nó dịch chuyển). Trên STG standalone,
không lưới nào cho ra `scrollLeft` tự nhiên > 0:

- EMR NursingRecord: bdiv rộng 3496px = đúng tổng cột (trang tính
  `dv-right` từ `.Newtouch_iframe` của portal — mở trực tiếp không có
  iframe nên chiều rộng không bị chặn; wrapper đo được `clientW=0`,
  `scrollW≈3500`, tức wrapper cũng không chặn). Cuộn duy nhất có thể là
  cuộn trang (window scroll), mà cuộn trang thì kéo cả khối frozen đi
  cùng — không phải cơ chế frozen của jqGrid.
- PDS StockQuery: lưới rỗng + `shrinkToFit` co cột về đúng viewport
  (1436/386) nên bdiv không tràn.
- Herp desktop: lưới vừa viewport (1438).

Đã thử 3 cách ép tràn chỉ trong phiên đo (không sửa code app):

1. `setGridWidth(2200)` — không hiệu lực (bdiv vẫn 3496/1436 do cột đã
   co theo tổng cột).
2. Thu hẹp `.ui-jqgrid-view` + bdiv (1000/350 desktop/mobile) — bdiv
   `scrollW` cũng co theo (1000/350) vì bảng rộng 100% theo bdiv.
3. Thu hẹp bdiv + ghim bảng theo px gốc (3496/1436) — vẫn `max=0`
   (bảng không giữ chiều rộng px đã ghim trong môi trường đo; nguyên
   nhân chính xác chưa xác định được trước khi SQL chết — có thể do
   handler resize/refresh của jqGrid hoặc của helper chạy lại sau khi
   đổi style).

**Kết luận phương pháp:** bằng chứng "cột neo kích hoạt" đã có
(`frozenColumns=true` + frozen-bdiv + đúng header trong frozen-div),
nhưng bằng chứng "cột neo đứng yên khi cuộn" **chưa có** và cần một
phiên đo riêng sau khi SQL hồi phục, với một trong hai cách: (a) đo
trong iframe portal thật (nơi chiều rộng bị chặn đúng như sử dụng thật
— cách đúng nhất cho EMR); hoặc (b) đo trên lưới có dữ liệu thật và
tổng cột vượt viewport trên mobile (Herp mua hàng 4 dòng là ứng viên
tốt nhất). Worker không bịa số đo cuộn để lấp chỗ này.

## 6. Sự cố trong phiên (ghi trung thực, không giấu)

### 6.1. Login trình duyệt chập chờn (không phải sai mật khẩu)

- Herp mobile lần đầu: ở lại `/Login/Index`, `tips_msg` rỗng (handler
  chưa bind khi bấm). Herp desktop lần chẩn đoán đầu cũng thất bại
  tương tự, lần sau (chờ 6s sau `networkidle`) thành công. Curl login
  cùng tài khoản luôn trả `{"state":"success","message":"Đăng nhập
  thành công"}` → server + tài khoản ổn, lỗi nằm ở handler UI bind
  chậm khi tải nặng (đúng tiền lệ index-wire §4.6).
- Cách xử lý trong probe vòng 2: chờ 6s sau `networkidle`; nếu thất bại
  mà `tips` rỗng (không có thông điệp server) thì thử lại **1 lần cùng
  mật khẩu, không mật khẩu khác**; nếu server trả thông điệp thì dừng
  ngay, không retry. Đây là ranh giới worker đã giữ.
- PDS mobile: `Page.goto` timeout 60s ở `networkidle` khi vào
  `/Login/Index` (app chậm, không phải sai mật khẩu).

### 6.2. Lỗi env STG của worker (đã sửa, không phải lỗi app)

- Lần chạy EMR STG đầu: login trả lỗi DB `Login failed for user 'sa',
  ConfigId="BaseDb"` vì script chạy EMR của worker dùng biến
  `$SA_PASSWORD` không tồn tại (file env chỉ có `MSSQL_SA_PASSWORD`).
  Đã sửa script thành `$MSSQL_SA_PASSWORD`, khởi động lại EMR STG,
  login thành công. Mật khẩu demo đăng nhập không đổi trong suốt phiên.

### 6.3. Lỗi session-cache của PDS STG (đã sửa ở script chạy, không đụng code repo)

- Log PDS STG ban đầu đầy lỗi `Error closing the session`
  (`ArgumentNullException: Value cannot be null (Parameter
  'configuration')` tại `RedisCache.ConnectAsync`) vì script chạy PDS
  (copy từ index-wire) chỉ đặt `RedisConfig__Connection`, trong khi lib
  dùng chung đọc cache qua `ConnectionStrings__RedisConfig__Connection`
  (Herp đã đặt cả hai nên chạy tốt). Đã thêm key còn thiếu vào script
  chạy PDS của đợt này (`~/workspace/perapp-grids-scratch/run-pds-stg.sh`),
  khởi động lại PDS STG: login 200, **0 lỗi session** trong log mới,
  probe trình duyệt sau đó login attempt 1 thành công cả 2 viewport.
  Script chạy là file ngoài repo, không commit; code repo không đổi vì
  việc này. (EMR/Sett không gặp lỗi này với key hiện tại; Sett chưa
  probe nên chưa kết luận.)

### 6.4. SQL Server chết — lý do dừng phiên (🚨 cần điều phối viên)

Diễn biến (giờ ICT):

- ~23:37: EMR mobile login bị server từ chối, thông điệp nguyên văn:
  `登录失败，请联系管理员` ("Đăng nhập thất bại, vui lòng liên hệ quản trị
  viên"). Theo quy tắc (thất bại 1 lần + có thông điệp server → dừng,
  không thử mật khẩu khác, không đoán), worker **không retry** lần
  mobile này. Dữ liệu mobile EMR trong báo cáo lấy từ các lần chạy
  thành công trước đó trên cùng bản STG (không đổi).
- Ngay sau đó, EMR desktop login cũng thất bại; curl `CheckLogin` trả
  cùng thông điệp lỗi; log BaseAPI STG cho thấy `CheckPwd` (AppId EMR)
  mất 14.5s rồi ném lỗi DB: `A network-related or instance-specific
  error occurred while establishing a connection to SQL Server. The
  server was not found or was not accessible. ... (provider: TCP
  Provider, error: 40 - Could not open a connection to SQL Server)
  ... ConfigId="BaseDb"`.
- Kiểm tra: `ss` không còn cổng 1433, `pgrep sqlservr` rỗng, TCP tới
  127.0.0.1:1433 bị refused. Không phải khóa tài khoản (lỗi nằm ở tầng
  kết nối DB, trước khi kiểm tra mật khẩu). Bộ nhớ lúc đó còn trống
  2.5GB/7.9GB; `dmesg` không thấy dòng OOM (có thể bị giới hạn quyền
  đọc). Log SQL cuối (`/var/opt/mssql/log/errorlog`) dừng ở 16:24 UTC
  (23:24 ICT); trước đó 15:56 UTC có dòng `Login failed for user 'sa'
  ... Password did not match` — chính là lần chạy EMR STG đầu của
  worker với env sai (§6.2), không phải tấn công.
- **Worker không restart SQL** (đúng quy tắc brief) và đã dừng mọi
  probe còn lại (Herp vòng 2, Sett, EMR debug cuộn). Hậu quả: vì prod
  và STG chung 1 SQL Server, **login prod cũng sẽ lỗi DB** cho tới khi
  SQL được khôi phục (trang login prod vẫn trả 200 vì trang tĩnh không
  cần DB — đã kiểm tra 11/11 trang login prod 200 sau dọn dẹp, nhưng đó
  không phải bằng chứng login prod chạy được).
- Dọn dẹp cuối phiên: đã kill đúng PID đã khởi động — Redis STG 13479,
  BaseAPI STG 13503, EMR STG 14709, Herp STG 21377, PDS STG 23607, Sett
  STG 21378. Kiểm tra sau kill: 6380/17001/15235/15236/15237/15221 đều
  trống; không còn tiến trình STG nào của đợt này; 11 trang login prod
  vẫn 200. Không chạm run-copy, static host 41142, app prod, SQL.
  (Một worker khác đang chạy STG riêng — `home-responsive-scratch/pds`
  — không bị đụng tới.)

## 7. Phần chưa đạt + việc cần điều phối viên chốt/làm tiếp

1. **🚨 Khôi phục SQL Server trước mọi việc khác** (recovery.sh hoặc
   cơ chế watchdog). Sau đó probe lại theo thứ tự: (a) Herp vòng 2
   (tiền tố mới `cgId`+`ddsj` + click chọn dòng bật nút Sửa + cuộn tới
   cột phải trên mobile 390); (b) Sett DaySett nội + ngoại trú (frozen
   `bcje`, toolbar reload aria); (c) EMR debug cuộn trong iframe portal
   hoặc trên lưới có dữ liệu (xem §5).
2. **Quyết định Herp mua hàng:** giữ phương án `ddsj` (đã làm) hay đưa
   `ddbh` lên đầu colModel rồi neo nó (đổi thứ tự cột)? Worker khuyến
   nghị giữ `ddsj` trừ khi nghiệp vụ yêu cầu "Số đơn hàng" phải là cột
   neo bằng mọi giá — vì đổi thứ tự cột là thay đổi hiển thị, còn neo
   tiền tố 480px thì vỡ mobile.
3. **Quyết định Herp kho:** giữ treeGrid (không neo — hiện trạng) hay
   tách hiển thị phân cấp khỏi lưới neo? Không có cách nào vừa giữ
   treeGrid vừa có frozen trong jqGrid 5.1.
4. **Xác nhận cột neo DaySett:** audit không nêu tên cột; worker đã lấy
   cột hiện đầu tiên (`bcje`, số tiền quyết toán). Nếu nghiệp vụ muốn
   neo "Số hóa đơn" (`fphs`, 400px) hoặc "Người quyết toán" thì đổi
   trước khi coi là xong (lưu ý: neo `fphs` một mình cũng vi phạm quy
   tắc tiền tố nếu đứng sau `bcje` — cần neo cả tiền tố).
5. **Build STG cho MRMS / OR / Base HOSP / Base Web** để đo live phần
   source-only (§1: mục 5, 6, 7, 10, 11, 12; §2 tương ứng). Mẫu script
   chạy STG của đợt này nằm ở `~/workspace/perapp-grids-scratch/
   run-{emr,herp,pds,sett}-stg.sh` (ngoài repo, không commit).
6. **Chuẩn hóa partial `_BottomButtonsView` của Sett?** F6/F7 là hành
   động chính của DaySett nhưng nằm ngoài `.toolbar` và dùng chung mọi
   trang Sett — worker không đụng; nếu muốn 1-primary xuyên suốt thì
   cần chặng riêng cho partial chung.
7. **Login UI chập chờn khi tải nặng** (§6.1) là vấn đề phương pháp đo
   đã biết từ nhiều chặng (handler bind chậm), không phải lỗi app mới;
   probe sau này nên giữ cơ chế "chờ 6s + retry 1 lần khi tips rỗng,
   dừng ngay khi có thông điệp server".

## 8. Ảnh (thư mục bắt buộc, tuyệt đối, gitignored, không commit)

`~/workspace/openhis-core/shots-redesign/perapp-grids/`:

- `emr-nursingrecord-desktop.png` — NursingRecord desktop (vòng r5,
  nút mới: Nhập mới primary, Sửa default, Hủy danger)
- `emr-nursingrecord-mobile.png` — NursingRecord mobile 390 (vòng r3,
  cùng trạng thái nút)
- `herp-warehouse-desktop.png` — Kho desktop (vòng 1: toolbar mới,
  **trước** vòng chẩn đoán treeGrid; trạng thái nút vẫn đúng vì nút
  không đổi sau đó)
- `herp-purchase-desktop.png` — Mua hàng desktop (vòng 1: bottom panel
  mới; **trước** khi đổi tiền tố frozen — ảnh chỉ chứng minh nút, không
  chứng minh frozen mới)
- `pds-stockquery-desktop.png` — StockQuery desktop (vòng r3/r5)
- `pds-stockquery-mobile.png` — StockQuery mobile 390 (vòng r3/r5)

Chưa có ảnh (bị chặn ở §6): Herp mobile, Herp vòng 2 (tiền tố mới),
Sett DaySett, MRMS/OR/Base, ảnh cuộn đối chứng. Ảnh "trước/sau" toolbar
dạng cặp đối chiếu không có vì trang gốc chưa từng được chụp trong
đợt này — bảng §2 thay bằng trước/sau theo class (đo từ source + live).

## 9. Những gì KHÔNG làm (tuân thủ brief)

- Không commit, không `git add`, không sửa run-copy hay static host
  41142, không publish Release từ cây này (chỉ Debug publish ra
  scratch ngoài repo), không copy DLL dưới tiến trình đang chạy (Herp
  được dừng đúng PID trước khi build lại; DLL sửa DI chỉ thay khi app
  chưa chạy).
- Không ghi DB demo (mọi thao tác ghi đều không có; chỉ GET + session
  login trên DB `*_STG`); không submit nghiệp vụ (không bấm Tra cứu
  trên StockQuery, không bấm F6/F7 DaySett, không tạo/sửa/xóa bản ghi
  nào — nút Sửa chỉ được đo trạng thái, không được bấm mở form sửa).
- Không viết lại footer tiếng Trung, không đụng mâu thuẫn receipt-prefix,
  không đụng layout scaffold khu vực, không tuyên bố test app native,
  không báo đạt phần chưa kiểm chứng live (mọi mục đều ghi rõ ĐẠT /
  CHƯA ĐẠT / source-only kèm số đo).
- Không thử mật khẩu khác sau thất bại có thông điệp server; mật khẩu
  demo chỉ qua biến môi trường tạm thời của phiên probe, không xuất
  hiện trong file, báo cáo, log hay ảnh này.
