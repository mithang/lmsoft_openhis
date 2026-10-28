# Báo cáo CHẶNG 4 — NHÓM B1: SSO, MRQC, Sett, MRMS, OR (+ API nhóm)

> Worker chặng 4 nhóm B1. Làm liên tục, tự duyệt, xong thì dừng.
> Repo `~/workspace/openhis-core`, nhánh `V2.2`, HEAD `7d30446a`
> (không commit mới, không sửa `roles-shots-progress.md`).
> Build: CHỈ trong worktree staging riêng `~/workspace/stage4-b1`
> (worktree tại HEAD `7d30446a`, detached); KHÔNG build/publish trong
> cây `~/workspace/openhis-core`; KHÔNG patch tạm cổng ở cây build prod.
> Probe: Playwright + Firefox local, tài khoản demo `000000`; mật khẩu
> chỉ qua biến môi trường tạm thời của phiên probe, không ghi vào
> file/báo cáo/log. Mật khẩu SA không lấy, không in.
> Mobile = giả lập viewport, không phải app native.
> KHÔNG ghi DB nào: toàn bộ kiểm chứng là chỉ-đọc trên prod
> (login + xem trang/lưới + chọn dòng phía client, không submit
> nghiệp vụ) nên không cần dựng instance STG cho nhóm này.

## 1. Phạm vi redeploy (từ `git show --stat HEAD` + báo cáo chặng 3)

| App | Đổi trong HEAD 7d30446a? | Hành động |
|---|---|---|
| SSO 5208 | KHÔNG (không file SSO nào trong HEAD) | Chỉ kiểm chứng, không redeploy |
| MRQC Web 5220 | CÓ — `Views/Home/Index.cshtml` | Redeploy |
| Sett Web 5221 | CÓ — `Views/Home/Index.cshtml` + `DaySettle/InpatientDaySett.cshtml` + `OutpatientDaySett.cshtml` | Redeploy |
| MRMS Web 5232 | CÓ — `Views/Home/{Index,Default,_IndexContentWrapper}.cshtml` + `MrDept/Index.cshtml` + `MrFee/Index.cshtml` | Redeploy |
| OR Web 5231 | CÓ — `Views/Home/Index.cshtml` + `OpStaff/Index.cshtml` | Redeploy |
| MRQC.API 5320, MRMS.API 5332, OR.API 5331 | KHÔNG (không file API nào trong HEAD) | Chỉ kiểm chứng, không redeploy |

## 2. Redeploy — config prod trước redeploy + kết quả từng app

Config prod xác định bằng `ps` trước MỌI publish (đúng quy tắc build mới):
SSO Release, MRQC Release, Sett Release, MRMS **Debug**, OR **Debug**
(OR.API cũng Debug nhưng không thuộc diện redeploy).
Mỗi app: build đúng config đó trong worktree → kill ĐÚNG PID cũ →
chờ cổng DOWN → copy DLL+PDB từ worktree sang bin prod (md5 khớp) →
restart bằng launcher của app (proxy env UNSET,
`DOTNET_SYSTEM_NET_DISABLEIPV6=true`, URLS do launcher quy định) →
kiểm chứng login + trang chính rồi mới sang app tiếp theo.
Một build cùng lúc trong phiên này.

| App | Prod trước | PID cũ → mới (lần redeploy) | DLL prod sau copy (md5) | Login sau redeploy |
|---|---|---|---|---|
| MRQC 5220 | Release | 2196 → 63830 | `597404b715cefcd13f9502096ca57116` | ✅ thành công, `/Home/Index` tiêu đề mới "Trang chủ — Kiểm soát chất lượng bệnh án", scrollW=1280 |
| Sett 5221 | Release | 2246 → 64734 | `eb30f8dded8d792938ae543654820e41` | ✅ thành công, `/Home/Index` tiêu đề mới "Trang chủ — Quản lý thu phí và quyết toán", scrollW=1280 |
| MRMS 5232 | Debug | 59737 → 65274 | `a90d89c96274ffa656193d4038d4a312` | ✅ thành công, `/Home/Index` shell lên (tiêu đề shell giữ "Y tế đám mây" theo chặng 3), scrollW=1280 |
| OR 5231 | Debug | 59738 → 65866 | `4b3e6a738d474a1f980bdec0a85fb029` | ✅ thành công, `/Home/Index` tiêu đề mới "Trang chủ — Hệ thống phòng mổ", scrollW=1280 |

Sau mỗi app: trang `/Login/Index` trả 200 trước khi probe login.
Build 0 error (chỉ warning MVC1000/CS0114 có sẵn, không liên quan).

## 3. Sự cố ngoài ý muốn giữa phiên: VM restart, recovery tự chạy

- Giữa lúc quét Sett/MRQC, VM restart (uptime về 0, toàn bộ tiến
  trình dotnet biến mất, `/tmp` bị xóa — probe script phải viết lại
  vào `~/workspace/stage4-b1-probe/`, ngoài repo).
- Một worker khác (worktree `stage4-b2`) đã chạy `recovery.sh`;
  recovery đưa toàn bộ prod lên lại, KỂ CẢ 4 app nhóm B1, từ đúng
  DLL stage3 vừa redeploy trên đĩa (md5 kiểm lại sau restart KHỚP
  4/4 với bảng §2; PID mới: SSO 2109, MRQC 2157, Sett 2219,
  MRMS 2285, OR 2354).
- Toàn bộ re-sweep §4 dưới đây được chạy lại/xác nhận TRÊN các
  tiến trình sau restart này (trừ các mục MRMS/OR đã đo trước
  restart và không bị ảnh hưởng bởi restart vì DLL không đổi).
- Bài học ghi nhận: script probe phải để ngoài `/tmp` ngay từ đầu.

## 4. Re-sweep — đối chiếu checklist chặng 3 (desktop 1280 + mobile 390)

Đo live trên prod (chỉ-đọc). "Frozen" = `.frozen-bdiv` tồn tại +
header neo đọc được. "1 primary" = nút primary của toolbar trang.

### 4.1. MRQC 5220
| Mục | Desktop | Mobile |
|---|---|---|
| `/Home/Index` | ✅ title/h1 "Trang chủ — Kiểm soát chất lượng bệnh án", scrollW=1280, offenders [] | ✅ như desktop, scrollW=390, offenders [] |

### 4.2. Sett 5221 (cột neo `bcje` GIỮ NGUYÊN theo brief)
| Mục | Desktop | Mobile |
|---|---|---|
| `/Home/Index` | ✅ title/h1 "Trang chủ — Quản lý thu phí và quyết toán", scrollW=1280, offenders [] | ✅ scrollW=390, offenders [] |
| `DaySettle/InpatientDaySett` `#gridList` | ✅ frozenBdiv=true, header neo ["", "Khóa chính", "này lần Số tiền quyết toán"] (= `rn`, `Id`, `bcje`), 0 dòng (lưới rỗng như chặng 3 nên không đo cuộn đối chứng), scrollW=1280 | ✅ frozenBdiv=true, cùng header neo, scrollW=390 |
| `DaySettle/OutpatientDaySett` `#gridList` | ✅ frozenBdiv=true, cùng header neo, 0 dòng, scrollW=1280 | (không đo riêng — cùng mẫu code/lưới với nội trú, đã đo mobile ở nội trú) |
| Toolbar | Nút Tải lại đã về `btn-default` (đúng diff HEAD); 2 nút primary F6/F7 đo được toàn trang thuộc partial chung `_BottomButtonsView` — KHÔNG ĐỤNG theo quyết định chặng 3, vẫn chờ điều phối viên nếu muốn chuẩn hóa partial chung | — |

### 4.3. MRMS 5232
| Mục | Desktop | Mobile |
|---|---|---|
| `/Home/Default` (dashboard standalone) | ✅ title "Trang chủ — Quản lý bệnh án", scrollW=1280, offenders [] | ✅ scrollW=390, offenders [] |
| `/Home/Index` (shell) | ✅ h1 "Trang chủ — Quản lý bệnh án", scrollW=1280; offender duy nhất `NAV.page-tabs` 100000px — ĐÃ BIẾT từ chặng 3 (bị `overflow:hidden` chứa, không gây cuộn ngang thật) | ✅ scrollW=390; vượt mép nhẹ `DIV.row` 3px (right=393) — ĐÃ BIẾT từ chặng 3, không gây cuộn ngang thật |
| `MrDept #gridList1` | ✅ frozenBdiv=true, header neo ["", "Id", "Mã khoa bệnh án"], 37 dòng, 1 primary "Tạo mới", scrollW=1280 | ✅ frozenBdiv=true, cùng header neo, 37 dòng, scrollW=390 (lưới cuộn nội bộ, trang không tràn) |
| `MrFee #gridList` | ✅ frozenBdiv=true, header neo ["", "id1", "id2", "id3", "Mã nhóm cấp 1"], 25 dòng, 1 primary "Tạo mới", scrollW=1280 | (không đo riêng — cùng mẫu, đã đo mobile ở MrDept) |
| Click bật nút Sửa (chỉ chọn dòng phía client, không submit) | ✅ MrDept `#gridList1`: trước chọn Sửa `btn btn-default disabled` + aria=true → sau click dòng (selrow=1) thành `btn btn-default` + aria=false | — |
| Lưu ý đo | Lần đo đầu dùng nhầm `#gridList` (lưới liên kết) cho click-test nên Sửa vẫn disabled — ĐO LẠI đúng `#gridList1` thì bật đúng. Không phải bug. | — |

### 4.4. OR 5231
| Mục | Desktop | Mobile |
|---|---|---|
| `/Home/Index` | ✅ title/h1 "Trang chủ — Hệ thống phòng mổ", scrollW=1280, offenders [] | ✅ scrollW=390, offenders [] |
| `OpStaff #gridList` | ✅ frozenBdiv=true, header neo ["", "Id", "Mã nhân viên"], 1 dòng, 1 primary "Tạo mới", scrollW=1280 | ✅ frozenBdiv=true, cùng header neo, 1 dòng, scrollW=390 (lưới cuộn nội bộ) |
| Click bật nút Sửa (chỉ chọn dòng phía client) | ✅ trước chọn class `disabled` + aria=true → sau click dòng (selrow=1) mất `disabled`, aria=false — khớp chứng minh chặng 3 §5.3 | — |

### 4.5. Kiểm chứng-only (không đổi trong HEAD)
- SSO 5208 (Release, PID 2109 sau restart): `/Login/Index` 200;
  probe login thành công → `/Home/Index` 200, scrollW=1280.
  (Tiêu đề portal/footer tiếng Trung của vendor: mục chỉ-liệt-kê
  từ chặng 3, không viết lại.)
- MRQC.API 5320, MRMS.API 5332, OR.API 5331: tiến trình LISTEN,
  `GET /` trả 404 (không có route gốc), không 500 — fail-closed
  như trước, không đổi nên không redeploy.

## 5. Bug mới / sửa trong phiên

KHÔNG CÓ bug mới thuộc nhóm B1, KHÔNG commit, KHÔNG push.
Các điểm ghi nhận (không tự sửa, không tự quyết):

1. **Sett `InpatientDaySett` mang title "OutpatientDaySett":**
   `ViewBag.Title = "OutpatientDaySett"` nằm ngay dòng 2 của file
   gốc; diff HEAD của file này chỉ đổi nút Tải lại + cờ frozen
   nên đây là quirk CÓ SẴN, không phải do chặng 3 hay chặng 4 gây
   ra. Để điều phối viên quyết có sửa title hay không.
2. **Sett partial `_BottomButtonsView`** vẫn mang 2 primary F6/F7
   (xem §4.2) — quyết định chuẩn hóa partial chung vẫn chờ.
3. **MRMS shell:** tab bar 100000px + vượt mép mobile 3px — đã
   biết từ chặng 3, không phải hồi quy.
4. **Mục mở chặng 3 thuộc nhóm khác:** EMR NursingRecord
   bật-lại-khi-chọn-dòng (cần dữ liệu điều dưỡng thật) — không
   thuộc nhóm B1, chỉ ghi nhận, không đo.
5. **Nhắc, không tự quyết:** mâu thuẫn receipt-prefix và 22 quyết
   định mở từ sweep cuối vẫn nguyên trạng.

## 6. Dọn dẹp + trạng thái cuối phiên

- Không dựng instance STG nào trong phiên này (không có ghi kiểm
  thử nào cần STG); kiểm tra cuối: không còn listener STG nào
  (cổng 15xxx, 6380, 17001 đều trống).
- Không chạm: SQL Server, Redis prod, static host 41142, app ngoài
  nhóm (trừ restart toàn cục do `recovery.sh` của worker khác).
- `git status` trong `~/workspace/openhis-core`: chỉ còn
  `M roles-shots-progress.md` (của điều phối viên, KHÔNG đụng);
  không file mới/không commit từ phiên này trong repo — báo cáo
  này là file duy nhất worker B1 tạo trong `scripts/redesign/`
  (điều phối viên commit khi tổng hợp).
- Worktree build giữ lại tại `~/workspace/stage4-b1` (HEAD
  7d30446a) để đối chiếu md5; probe giữ tại
  `~/workspace/stage4-b1-probe/` (ngoài repo).
- Kết luận: **nhóm B1 sạch trong phạm vi chặng 3** — 4/4 app đổi
  đã lên prod đúng build, login + re-sweep desktop/mobile đạt;
  SSO + 3 API kiểm chứng-only đạt; 0 bug mới cần sửa.
