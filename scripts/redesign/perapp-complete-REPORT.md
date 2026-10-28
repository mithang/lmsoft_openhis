# Báo cáo HOÀN TẤT đợt PER-APP (B-vòng-2 + C-hoàn tất) — REDESIGN UI/UX OpenHIS

> Worker hoàn tất đợt per-app. **KHÔNG commit, KHÔNG deploy prod, KHÔNG git add.**
> Repo `~/workspace/openhis-core`, nhánh `V2.2`, HEAD `fb6a25fb`; cây modified
> đầu phiên = cuối phiên (đối chiếu `git status` đầu/cuối: không đổi, 37 dòng).
> Build: **chỉ Debug publish ra scratch** `~/workspace/perapp-complete-scratch/`
> (mỗi lần 1 app; `Program.cs` patch tạm cổng rồi khôi phục bằng sed, `git diff`
> của mọi `Program.cs` đều rỗng sau build; DLL `NewtouchHIS.Lib.Base` trong
> scratch đã thay bằng bản sửa DI, md5 `8eaae85e513ca28d0c06f79eae30eb2c`).
> Probe: Playwright + Firefox local, tài khoản demo `000000`; mật khẩu chỉ qua
> biến môi trường tạm thời của phiên probe, không ghi vào file/báo cáo/log/ảnh.
> Chỉ đọc, không submit nghiệp vụ, không ghi DB (kể cả STG, ngoài session login).
> Mobile = giả lập viewport, không phải app native.

## 0. 🚨 SỰ CỐ P0 DO PHIÊN NÀY GÂY RA — ĐỌC TRƯỚC, CẦN ĐIỀU PHỐI VIÊN XỬ LÝ

**Hiện trạng cuối phiên: 3 trang login prod trả 500 `BadImageFormatException:
Bad IL format` — OR 5231, MRMS 5232, CIS 5234.** 8 trang login prod còn lại
vẫn 200 (5208, 5220, 5221, 5235, 5236, 5237, 5233, 5238); BaseAPI 7001 trả 404
ở `/` như bình thường; SQL 1433 vẫn LISTEN.

**Nguyên nhân (worker tự nhận):** 3 app OR/MRMS/CIS ở prod chạy build **Debug
từ chính cây repo** (`bin/Debug/net6.0/...`, PID khởi động từ 15:24–15:25
ngày 2026-10-28, trước phiên này). Quy tắc brief "chỉ build Debug publish"
được viết với giả định prod chạy `bin/Release`, nhưng với 3 app này,
`dotnet publish -c Debug` của phiên này đã ghi đè `bin/Debug` **dưới tiến
trình prod đang chạy** (dấu vết: mtime các DLL `bin/Debug` của MRMS 16:54,
OR 16:56, CIS 17:18 UTC — đúng khung giờ build của phiên này; các app prod
chạy Release không bị vì prod đọc thư mục `bin/Release` khác). Đây là cùng
họ sự cố "copy DLL dưới tiến trình đang chạy" đã từng làm chết prod SSO,
chỉ khác là lần này do publish Debug, không phải copy tay.

**Bằng chứng DLL trên đĩa vẫn tốt (tiên lượng khôi phục cao):** cùng đúng
bản publish đó đã chạy STG trong phiên này và login + probe đều đạt
(MRMS STG 15332, OR STG 15331, CIS STG 15334 — xem §B5/§C3). Vậy lỗi prod
nằm ở trạng thái tiến trình đang chạy sau khi file dưới nó bị thay, không
nằm ở bản build. **Khôi phục dự kiến: restart 3 app prod đó là đủ**
(khởi động lại từ DLL trên đĩa hiện tại).

**Worker KHÔNG tự restart prod** (đúng quy tắc cứng của brief) và báo cáo
ngay tại đây thay vì tự sửa. Watchdog nhiều khả năng **không tự phát hiện**
vì cổng 5231/5232/5234 vẫn LISTEN (chỉ trang login 500). Đề nghị điều phối
viên restart 3 PID prod sau (đúng PID, không kill STG — STG đã dọn sạch,
§7): OR PID 2349, MRMS PID 2297, CIS PID 2933, rồi kiểm tra lại
`/Login/Index` 200. Sau sự cố này, mọi build Debug tiếp theo cho
OR/MRMS/CIS **phải publish ra scratch mà không chạm `bin/Debug` của cây**
(hoặc dừng prod trước rồi mới build) — brief đợt sau cần sửa quy tắc này.

**BLOCKER chức năng (theo định nghĩa brief B4): KHÔNG có.** Nút Sửa trên
Herp mua hàng bật lại live sau khi chọn dòng (§B4); trên EMR NursingRecord
thì **chưa đo được** vì lưới STG rỗng (không phải kẹt disabled — xem §B4),
đây là mục mở, không phải blocker đã chứng minh.

## 1. Kết luận nhanh theo mục

| Mục | Kết quả |
|---|---|
| B1 — SQL sống trước probe | ✅ ĐẠT: 1433 LISTEN, login STG thành công, 14 DB `*_STG` |
| B2 — Herp mua hàng vòng 2 | ✅ ĐẠT: frozen `cgId`+`ddsj` kích hoạt desktop + mobile; toolbar bottom đúng; cuộn đối chứng đạt |
| B3 — Sett DaySett nội + ngoại trú | ✅ ĐẠT (frozen + toolbar chính); lưới rỗng nên chưa đo cuộn |
| B4 — Kiểm chứng sửa `#GRIDID` | ✅ Herp mua hàng: Sửa disabled true→false live. ⏳ EMR: chưa đo được (lưới STG rỗng, 3 bảng 0 dòng); code đã đọc đúng `#gridList` |
| B5 — MRMS / OR / Base HOSP / Base Web | ✅ ĐẠT, trừ **HOSP ChargeCategory frozen=false do treeGrid** (phát hiện mới, cùng họ Herp kho) |
| B6 — Đo cuộn đối chứng | ✅ ĐẠT trên 2 lưới có dữ liệu thật (Herp mua hàng, MRMS MrDept); các lưới còn lại không tràn tự nhiên — ghi rõ, không ép tràn |
| C1 — Shell PDS tablet 768 | ✅ ĐẠT: iframe w=748/768, ảnh `pds-index-tablet.png` đã chụp lại |
| C2 — Herp home 3 viewport | ✅ ĐẠT: KPI 2/3 cột mobile/tablet, ₫, cảnh báo 0 màu trung tính, trạng thái rỗng |
| C3 — Home 8 app còn lại | ✅ ĐẠT (HOSP, Base Web, MRMS, Sett, MRQC, OR, CIS); ⚠️ EMR: shell đạt, nhưng `/Home/Default` 404 do **thiếu action từ trước** (xem §C3) |
| Dọn dẹp + git | ✅ STG đã kill hết, cổng STG trống, git không đổi, không commit |

## 2. B1 — Kiểm tra SQL Server sống

- `ss`: 1433 LISTEN (`sqlservr` PID 32583).
- `sqlcmd -b` login thành công, liệt kê đủ 14 DB `*_STG`
  (`NewtouchHIS_Base_STG`, `NewtouchHIS_herp_STG`, `NewtouchHIS_PDS_STG`,
  `NewtouchHIS_Sett_STG`, `Newtouch_EMR_STG`, `Newtouch_MRMS_STG`,
  `Newtouch_MRQC_STG`, `Newtouch_CIS_STG`, `Newtouch_OR_STG`,
  `Newtouch_Union_STG`, `bdg_STG`, `Newtouch_His_Log_STG`,
  `Newtouch_Interface_STG`, `NewtouchHis_Job_STG`).
- Hạ tầng STG dựng theo thứ tự: Redis STG 6380 (PID 33711, đã kill cuối
  phiên) → BaseAPI STG 17001 (PID 33729, đã kill cuối phiên) → từng app STG.
- STG cũ của đợt C đã xử lý đúng brief: kill đúng PID Herp 15336
  (15278/15282) và PDS 15337 (27861/27868) trước khi build fresh thay thế;
  không kill STG của worker khác (không còn STG nào khác chạy lúc đó).

## 3. B2 — Herp mua hàng vòng 2 (build fresh 15336, gồm sửa B + C + fix GRIDID)

Trang: `/PurchaseManage/Purchase/Index`, lưới `#gridList`, 4 dòng STG.

| Chỉ số | Desktop 1280 | Mobile 390 |
|---|---|---|
| Login | ok | ok |
| `frozenColumns` / guard | true; treeGrid/subGrid/cellEdit/sortable/scroll đều false | true; guard đều false |
| Tiền tố neo live | [`rn`, `cb`, `cgId`, `ddsj`], prefixMaxFrozen=3, frozenHeaders=[``, ``, `cgId`, `Thời gian đơn hàng`] | như desktop |
| Bdiv tự nhiên | scrollW=1483, clientW=1278, maxScroll=205 | scrollW=1483, clientW=388, maxScroll=1095 |
| Nút Sửa ban đầu | `#btn_edit` disabled=true, aria-disabled=true | như desktop |
| Nút chính | Toàn trang: Tra cứu (vùng lọc riêng) + Tạo mới (bottom) là primary; bottom chỉ Tạo mới là primary | như desktop |
| Nút Hủy | danger, margin-left=16px | danger, margin-left=16px |

**Cuộn đối chứng (B6, lưới này):** mobile, scrollLeft 0→300: cột neo
`ddsj` trong frozen-div giữ nguyên x=75→75; cùng cột trong header chính
dịch 75→−225; cột sau `psdbm` dịch 203→−97. Desktop: frozen th đầu giữ
x=0→0 trong khi header chính dịch −205. **Kết luận: cột neo đứng yên
thật, cột sau dịch chuyển thật — đạt.**

**Quyết định điều phối đã chốt:** giữ nguyên tiền tố `ddsj`+`cgId` ẩn,
không đưa `ddbh` lên đầu colModel (đổi thứ tự cột — để user quyết sau).
Tên audit "Số đơn hàng" (`ddbh`) vẫn tới được bằng cuộn ngang (x=459
trước cuộn mobile, 159 sau cuộn 300).

## 4. B3 — Sett DaySett (build fresh 15221)

| Trang / viewport | frozenColumns | Tiền tố neo | Toolbar chính | Reload aria | Dòng |
|---|---|---|---|---|---|
| InpatientDaySett desktop | true | [`rn`, `Id`, `bcje`] | 0 primary | "Tải lại" (aria + title) | 0 |
| InpatientDaySett mobile | true | [`rn`, `Id`, `bcje`] | 0 primary | "Tải lại" | 0 |
| OutpatientDaySett desktop | true | [`rn`, `Id`, `bcje`] | 0 primary | "Tải lại" | 0 |

- Frozen headers live: [``, `Khóa chính`, `này lần Số tiền quyết toán`]
  (cột `Id` ẩn vẫn hiện label trong frozen-div — hành vi jqGrid có sẵn,
  giống Herp mua hàng hiện `cgId`).
- Ghi chính xác về toolbar: `.toolbar` chính của trang = 0 primary
  (đúng báo cáo B). Partial chung `_BottomButtonsView` là một `.toolbar`
  khác, vẫn có 2 primary F6 "Xác nhận quyết toán" + F7 "Hủy quyết toán"
  — **không đụng theo báo cáo B** (điều phối viên quyết ở chặng riêng
  nếu muốn chuẩn hóa partial chung).
- Cột neo `bcje` là lựa chọn điều phối (audit không nêu tên cột) — vẫn
  chờ user xác nhận trong báo cáo cuối như brief đã dặn.
- Lưới rỗng (0 dòng) nên không đo được cuộn đối chứng ở Sett; không ép tràn.

## 5. B4 — Kiểm chứng sửa `#GRIDID` (hồi quy chức năng)

### 5.1. Herp mua hàng — ✅ ĐẠT (không blocker)

- Trước chọn: `#btn_edit` disabled=true, aria-disabled=true.
- Click DOM vào dòng đầu tiên (rowId `1`, 4 dòng STG): selrow=`1`,
  selarrrow=[`1`].
- Sau chọn: `#btn_edit` **disabled=false, aria-disabled=false**.
- Code đã đọc: dòng 481–482 dùng `#gridList` cho cả `selrow` và
  `selarrrow` — đúng như điều phối viên đã sửa.

### 5.2. EMR NursingRecord — ⏳ CHƯA ĐO ĐƯỢC (không bịa, không chèn dòng giả)

- Code đã đọc đúng: dòng 549 `$("#gridList").jqGrid("getGridParam",
  "selrow")`, dòng 554 bind `jqGridSelectRow / jqGridGridComplete /
  jqGridAfterGridComplete` → `rdSyncEditState()`; cùng mẫu code đã chứng
  minh live ở Herp mua hàng (§5.1) và OR OPStaff (§6).
- Trạng thái ban đầu đo live được cả 2 viewport: `#btn_edit`
  disabled=true, aria-disabled=true; frozen=true, tiền tố
  [`Id`, `zyh`, `xm`, `rq`]; nút chính: Tìm kiếm (hàng lọc) + Nhập mới;
  Hủy hiệu lực danger margin-left=16px.
- **Vì sao chưa đo được bật-lại:** lưới STG 0 dòng với lọc mặc định
  (kssj=2026-10-01 → hôm nay). Đã kiểm tra DB STG (chỉ đọc):
  `bl_hljl` = 0 dòng, `bl_hljl_wz` = 0, `bl_hljl_ss` = 0 — không lọc nào
  (kể cả đổi `hllx` 1/2) có thể ra dòng khi bảng rỗng. Không chèn dòng
  giả, không tuyên bố đạt phần này.
- Cần dữ liệu thật (bệnh nhân + bản ghi điều dưỡng trên STG) mới đo
  được bước click; hoặc đo lại khi STG có dữ liệu.

### 5.3. Bằng chứng phụ (ngoài phạm vi B4, cùng cơ chế anchor)

OR OPStaff (1 dòng STG): nút Sửa dạng anchor, trước chọn class
`btn btn-default disabled` + aria-disabled=true; sau click dòng:
class còn `btn btn-default` (mất `disabled`), aria-disabled=false.
Chứng tỏ cơ chế "Sửa chỉ bật khi chọn dòng" hoạt động cả 2 dạng nút
(`<input>` và anchor) trên code đợt B.

## 6. B5 — MRMS / OR / Base HOSP / Base Web (desktop + mobile)

Mọi mục dưới đây: login ok cả 2 viewport; nút Sửa ban đầu đúng trạng
thái vô hiệu (input: disabled=true; anchor: class `disabled` +
aria-disabled=true — anchor không có thuộc tính `disabled` nên
`disabled=false` ở đây là đúng, không phải lỗi).

| App / trang / lưới | frozen | Cột neo live | Dòng | Toolbar (1 primary) | Sửa ban đầu | Xóa |
|---|---|---|---|---|---|---|
| MRMS MrDept `#gridList1` | true | [`rn`, `Id`, `ksbm`] | 37 | Tạo mới; reload aria "Tải lại" | class disabled | danger 16px |
| MRMS MrDept `#gridList` (liên kết) | true | [`rn`, `Id`, `hisdept`] | 17 | (chung toolbar trên) | — | — |
| MRMS MrFee `#gridList` | true | [`rn`, `id1`, `id2`, `id3`, `code1`] | 25 | Tạo mới; reload aria "Tải lại" | class disabled | — |
| OR OPStaff `#gridList` | true | [`rn`, `Id`, `Code`] | 1 | Tạo mới; reload aria "Tải lại" | class disabled (+ click đạt, §5.3) | danger 16px |
| HOSP ChargeCategory `#gridList` | **false — treeGrid** | tiền tố [`rn`, `dlId`, `dlmc`] hợp lệ nhưng không kích hoạt | 57 | Tạo mới (toolbar của trang); xem ghi chú toolbar dưới | class disabled | — |
| HOSP SysGlobalConfig `#gridList` | true | [`rn`, `Id`, `Name`] | 3 | Tạo mới; reload aria "Tải lại" | class disabled | — |
| Base Web SysGlobalConfig `#gridList` | true | [`rn`, `Id`, `Name`] | 3 | Tạo mới; reload aria "Tải lại" | class disabled | — |

**Phát hiện mới — HOSP ChargeCategory:** trang khai `treeGrid: true`
(dòng 53, `treeGridModel: "adjacency"`) nên vendor jqGrid thoát im lặng
như Herp kho: đo live treeGrid=true, các guard khác false,
frozenColumns=false, frozenBdiv=false. Báo cáo B §3.3 đã dự đoán "dự
kiến kích hoạt được" cho Base — dự đoán này **sai với ChargeCategory**;
cờ frozen vẫn giữ trong source như Herp kho (ghi nhận ý đồ audit, sẽ tự
kích hoạt nếu treeGrid bị gỡ). Quyết định giữ/tách treeGrid thuộc về
điều phối viên, giống Herp kho.

**Ghi chú toolbar ChargeCategory:** trang chỉ có 1 `.toolbar` trong
source (Tạo mới primary + Sửa default). Probe đếm được thêm 1 `.toolbar`
có nút icon-only primary (text rỗng) — nút này thuộc partial/layout
dùng chung (`_OrganizeIdDDLToolbarSearchView` hoặc layout `_Index`),
không thuộc toolbar của trang; không tính là vi phạm "1 primary" của
trang (cùng logic nút Tìm kiếm ở vùng lọc riêng trong báo cáo B).

**Herp kho (đo lại trong phiên, desktop):** frozenBdiv=false như cũ
(treeGrid guard — quyết định giữ nguyên treeGrid đã chốt); toolbar =
["Thêm mới"] duy nhất, reload aria "Tải lại", Xóa danger 16px, Sửa
class disabled + aria true. Lưới vừa viewport desktop (maxScroll=0)
nên không đo cuộn ở đây.

## 7. B6 — Đo cuộn đối chứng (tổng kết)

- ✅ **Herp mua hàng** (desktop + mobile, §3): neo đứng yên, cột sau dịch.
- ✅ **MRMS MrDept mobile** (`#gridList1`, 37 dòng): scrollLeft 0→239
  (maxScroll=239): cột neo `ksbm` trong frozen-div giữ x=294.78→294.78;
  cùng cột trong header chính dịch 294.78→55.78 (−239); cột sau `ksmc`
  dịch 465.78→226.78 (−239). Đối chứng độc lập thứ hai, khác app.
- ⏳ Các lưới còn lại: không có tràn tự nhiên (bdiv scrollW = clientW,
  hoặc lưới rỗng) nên **không đo được cuộn đối chứng** — ghi rõ, không
  ép tràn bằng width giả rồi mạo nhận là số đo thật (đúng quy tắc brief).
- Nhánh iframe portal SSO STG **không thực hiện**: nhánh "lưới có dữ
  liệu thật" của B6 đã đạt trên 2 lưới, nên không cần dựng thêm portal
  STG chỉ để đo iframe.

## 8. C1 — Shell PDS tablet 768 (build fresh 15337)

- Dùng chờ cố định (không `networkidle` cho trang shell — đúng tiền lệ
  timeout), login ok.
- Shell tablet: `.content-iframe` x=0, w=768; **iframe x=10, w=748**,
  h=924; scrollW=768; sidebar ở tablet là `block` (không phải thủ phạm
  bóp iframe như mobile trước fix).
- Dashboard trong iframe: KPI 2 cột (x=12 và 381, w=357) — **đúng
  responsive, không phải lỗi**: iframe rộng 748 < 768 nên rơi vào rule
  `max-width: 767px` (2 cột); Default standalone tablet 768 đo lại vẫn
  3 cột (x=12/264/517, w=240), offenders rỗng, trạng thái rỗng
  "Chưa có số liệu loại thuốc trong năm nay. Tra cứu tồn kho".
- Ảnh đã chụp lại: `pds-index-tablet.png` (bản cũ đã bị xóa vì là ảnh
  trang login) + bản `pds-index-tablet-v2.png` cùng nội dung.
  (Lưu ý nhỏ: panel KPI có 1 grid-child kích thước 0×0 ở cả standalone
  lẫn iframe — phần tử rỗng có sẵn, không ảnh hưởng bố cục.)

## 9. C2 — Herp home 3 viewport (bản build fresh B2, gồm fix clearfix đợt C)

| Chỉ số | Desktop 1280 | Tablet 768 | Mobile 390 |
|---|---|---|---|
| Title / h1 | "Trang chủ — Hệ thống quản lý vật tư" | như desktop | như desktop |
| KPI (`.rd-kpi-grid`) | 5 thẻ 1 hàng (x=50/290/530/771/1011, w=240) | 3 cột (x=50/284/517, w=222) | **2 cột** (x=36/212, w=164) |
| scrollW / offenders | 1280 / [] | 768 / [] | 390 / [] |
| `.fa-cny` / ký hiệu ₫ | 0 / có ₫ | 0 / có ₫ | 0 / có ₫ |
| Cảnh báo tồn kho/quá hạn | — | — | text "Cảnh báo tồn kho: 0 … | Cảnh báo quá hạn: 0 …", màu computed `rgb(107,114,128)` = **#6b7280 trung tính** |
| Panel biểu đồ trống | `.rd-empty` có, "Chưa có số liệu nhập kho trong năm nay. Xem quản lý mua sắm" | như desktop | như desktop |

## 10. C3 — Home các app còn lại (mỗi app 1 cổng STG riêng)

Build trong phiên này hoặc **restart từ đúng bản build fresh của phiên
này** (cây không đổi giữa các lần build — git status không đổi, §12):
HOSP 15333, Base Web 15338, MRMS 15332, EMR 15235, Sett 15221 (đều là
scratch build fresh của phiên); MRQC 15320, CIS 15334 build fresh mới;
OR 15331 restart từ scratch build fresh của phiên.

### 10.1. Shell + dashboard (PDS/Base đã biết chuẩn: iframe mobile ~366px)

| App | Shell iframe (desktop / tablet / mobile) | Dashboard KPI (desktop / tablet / mobile) | scrollW = viewport | Tiêu đề nhiệm vụ |
|---|---|---|---|---|
| HOSP | không có iframe (Default bị comment từ gốc — đúng §5 báo cáo C); `.content-iframe` w=1100/768/386 | 5 thẻ 1 hàng / 3 cột / 2 cột | ✅ cả 3 | h1 "Trang chủ — Quản trị hệ thống" (title shell giữ "Hệ thống quản trị") |
| Base Web | 1081 / 748 / **366** | 5 / 3 / 2 cột | ✅ cả 3 | "Trang chủ — Quản trị dữ liệu cơ sở" |
| MRMS | 1081 / 748 / **366** | 5 / 3 / 2 cột | ✅ cả 3 | h1 "Trang chủ — Quản lý bệnh án" (title shell giữ động "Y tế đám mây") |
| EMR | 1081 / 748 / **366** | xem §10.2 (Default 404) | ✅ cả 3 (shell) | "Trang chủ — Bệnh án điện tử" |

- Offenders: desktop có `NAV.page-tabs menuTabs` rộng 100000px (tồn tại
  trước, bị `overflow: hidden` chứa, scrollW vẫn = viewport — như báo
  cáo C). Mobile shell HOSP/Base Web/MRMS/EMR đều có 1 `DIV.row` vượt
  mép phải **3px** (right=393 trên viewport 390, w=396) nhưng scrollW
  vẫn = 390 (không gây cuộn ngang thật) — ghi nhận là vượt mép nhẹ,
  không phải tràn ngang thật; PDS shell mobile đợt C đo offenders rỗng
  nên đây là khác biệt nhỏ của 4 shell này, cần worker shell xem lại
  nếu muốn triệt để.
- KPI trong iframe ở tablet (748 < 768) hiển thị 2 cột thay vì 3 — đúng
  rule breakpoint như §8, không phải lỗi.
- Chỉ số KPI desktop đo bằng `.dashboard-stats-item` (không dùng
  children của `.rd-kpi-grid` vì ở desktop grid không bật, children
  `<a>` đo được w=0 — artifact đo, không phải lỗi UI; đã sửa probe và
  đo lại: HOSP/Base Web/MRMS desktop đều 5 thẻ 1 hàng).

### 10.2. ⚠️ EMR — `/Home/Default` 404 (thiếu action từ trước, ngoài phạm vi sửa C)

- `HomeController` của EMR **không có action `Default`** (chỉ có
  `Index`, `GetNewFieldUniqueValue`, `SyncSysConfigParams`); file
  `Views/Home/Default.cshtml` tồn tại nhưng không có route tới được.
  Kiểm tra: curl `/Home/Default` → 404; probe trình duyệt →
  `NS_ERROR_NET_EMPTY_RESPONSE`. File controller không nằm trong danh
  sách modified của đợt B/C nên đây là thiếu sót **từ trước**, không
  phải do đợt C gây ra.
- Hệ quả: `_IndexContentWrapper` đặt `defaultPage = "/Home/Default"`
  chỉ khi `IsRoot`; với tài khoản `000000` (không phải root), tab mặc
  định là `/MedicalRecordManage/PatInfo/Index` ("Tổng quan bệnh nhân")
  — và shell EMR đo được chính là tab này (iframe đủ rộng cả 3
  viewport). Đã đo PatInfo standalone thay thế: cả 3 viewport
  scrollW = viewport, offenders rỗng (title trang này rỗng — trang
  nghiệp vụ, không phải trang chủ nên không đánh giá tiêu đề).
- Việc cần điều phối viên: hoặc thêm action `Default` cho EMR, hoặc
  sửa `_IndexContentWrapper` để root cũng dùng PatInfo, hoặc chấp nhận
  Default.cshtml của EMR là dead code. Cho tới lúc đó, phần "dashboard
  EMR" trong đợt C **không kiểm chứng được qua `/Home/Default`** —
  các sửa trong `Default.cshtml` của EMR chưa có đường nào ra UI thật.

### 10.3. Home dạng bare (Sett / MRQC / OR / CIS)

| App | Title | 3 viewport scrollW | Offenders | Ghi chú |
|---|---|---|---|---|
| Sett `/Home/Index` | "Trang chủ — Quản lý thu phí và quyết toán" | 1280/768/390 ✅ | [] cả 3 | h1 trùng title |
| MRQC `/Home/Index` | "Trang chủ — Kiểm soát chất lượng bệnh án" | ✅ | [] cả 3 | h1 trùng title |
| OR `/Home/Index` | "Trang chủ — Hệ thống phòng mổ" | ✅ | [] cả 3 | h1 trùng title |
| CIS `/Home/Index` (chỉ viewport) | "CIS - Hệ thống thông tin lâm sàng" (giữ nguyên theo báo cáo C — không sửa tiêu đề/link) | ✅ | [] cả 3 | meta viewport có sẵn dòng 9 |

- MRMS/EMR dashboard: không có trạng thái rỗng (dữ liệu demo ngẫu
  nhiên) — đúng ghi chú §7 báo cáo C, không bịa trạng thái.

## 11. Phần chưa đạt + việc cần điều phối viên (tổng hợp, không tự quyết)

1. **🚨 P0 — Restart 3 app prod Debug (OR 2349, MRMS 2297, CIS 2933)**
   sau sự cố publish của phiên này (§0). Kiểm tra lại login 200 sau
   restart. Sửa quy tắc build cho các đợt sau (không publish vào
   `bin/Debug` của cây khi prod Debug đang chạy).
2. **EMR click bật-nút-Sửa** (§5.2): cần dữ liệu điều dưỡng thật trên
   STG mới đo được; code đã đúng, mẫu đã chứng minh ở Herp + OR.
3. **Herp kho + HOSP ChargeCategory frozen** (treeGrid guard): giữ
   treeGrid (không neo) hay tách hiển thị phân cấp khỏi lưới neo?
4. **Herp mua hàng:** giữ neo `ddsj` hay đưa `ddbh` lên đầu colModel
   (đổi thứ tự cột)? Worker giữ `ddsj` theo quyết định đã chốt.
5. **Sett DaySett cột neo `bcje`:** chờ user xác nhận (audit không nêu tên cột).
6. **EMR `/Home/Default` 404** (§10.2): thêm action, đổi defaultPage
   cho root, hay chấp nhận dead code?
7. **Vượt mép nhẹ 3px** (`DIV.row`, shell mobile HOSP/Base Web/MRMS/EMR)
   và tab bar tràn dưới 6px + footer tiếng Trung (đã liệt kê từ đợt C):
   chỉ liệt kê, không viết lại.
8. **Mâu thuẫn receipt-prefix, 22 quyết định mở từ trước, 5 app chạy
   Debug ở prod:** ngoài phạm vi đợt này, không đụng.

## 12. Dọn dẹp cuối phiên + tuân thủ

- Đã kill đúng PID STG của phiên: Redis STG 33711, BaseAPI STG 33729,
  Herp 34131, Sett 36850/53515, EMR 37900/50531, MRMS 38828/48759,
  OR 40789/55263, HOSP 41845/44981, BaseWeb 43516/46983, PDS 44341,
  MRQC 54453, CIS 56263; STG cũ đợt C 15278/15282 + 27861/27868 (đã
  ghi ở §2). Kiểm tra cuối: mọi cổng STG (15221/15235/15320/15331/
  15332/15333/15334/15336/15337/15338, 6380, 17001) đều trống.
- **Không chạm:** run-copy, static host 41142, app prod (trừ sự cố
  publish §0 — không kill/restart prod nào), SQL Server.
- Git: HEAD vẫn `fb6a25fb`, không commit, không `git add`, không
  `git checkout/reset/clean`; `git status` cuối phiên = đầu phiên
  (37 dòng: 35 modified + 2 untracked báo cáo B/C); mọi `Program.cs`
  đều khôi phục, diff rỗng.
- Một lần kill tự khớp chính shell (lệnh `kill $(pgrep -f ...emr...)`
  khớp cả shell chứa pattern) — shell chết nhưng EMR STG cũng đã được
  kill đúng ý định; các lần kill sau đều dùng PID chính xác, không lặp
  lại lỗi này.
- Không login thất bại nào có thông điệp server trong phiên (mọi login
  probe đều attempt 1 thành công; không retry mật khẩu, không đoán).
  Một số probe EMR Default thất bại do 404 (không phải login).

## 13. Ảnh (không commit, không ghi đè ảnh cũ — tên `-v2`, trừ file C1 được yêu cầu chụp lại)

`~/workspace/openhis-core/shots-redesign/perapp-grids/`:

- `herp-purchase-v2-desktop.png`, `herp-purchase-v2-mobile.png` (tiền tố mới)
- `herp-purchase-click-v2-desktop.png` (sau click chọn dòng — nút Sửa bật)
- `herp-warehouse-v2-desktop.png`
- `sett-inpatient-v2-desktop.png`, `sett-inpatient-v2-mobile.png`,
  `sett-outpatient-v2-desktop.png`
- `emr-nursingrecord-v2-desktop.png`, `emr-nursingrecord-v2-mobile.png`
- `mrms-mrdept-v2-desktop.png`, `mrms-mrdept-v2-mobile.png`,
  `mrms-mrdept-link-v2-desktop.png`, `mrms-mrdept-scroll-v2-mobile.png`,
  `mrms-mrfee-v2-desktop.png`, `mrms-mrfee-v2-mobile.png`
- `or-opstaff-v2-desktop.png`, `or-opstaff-v2-mobile.png`,
  `or-opstaff-click-v2-desktop.png`
- `hosp-chargecategory-v2-desktop.png`, `hosp-chargecategory-v2-mobile.png`,
  `hosp-chargecategory-diag-v2-desktop.png`,
  `hosp-sysglobalconfig-v2-desktop.png`, `hosp-sysglobalconfig-v2-mobile.png`
- `baseweb-sysglobalconfig-v2-desktop.png`, `baseweb-sysglobalconfig-v2-mobile.png`

`~/workspace/openhis-core/shots-redesign/home-responsive/`:

- `pds-index-tablet.png` (chụp lại theo C1 — bản cũ đã xóa) +
  `pds-index-tablet-v2.png` (cùng nội dung), `pds-default-tablet-v2.png`
- `herp-home-v2-desktop.png`, `herp-home-v2-tablet.png`, `herp-home-v2-mobile.png`
- `hosp-index-v2-{desktop,tablet,mobile}.png`, `hosp-default-v2-{desktop,tablet,mobile}.png`
- `baseweb-index-v2-{desktop,tablet,mobile}.png`, `baseweb-default-v2-{desktop,tablet,mobile}.png`
- `mrms-index-v2-{desktop,tablet,mobile}.png`, `mrms-default-v2-{desktop,tablet,mobile}.png`
- `emr-index-v2-{desktop,tablet,mobile}.png`, `emr-patinfo-v2-{desktop,tablet,mobile}.png`
  (EMR Default không có ảnh vì route 404)
- `sett-index-v2-{desktop,tablet,mobile}.png`
- `mrqc-index-v2-{desktop,tablet,mobile}.png`
- `or-index-v2-{desktop,tablet,mobile}.png`
- `cis-index-v2-{desktop,tablet,mobile}.png`
