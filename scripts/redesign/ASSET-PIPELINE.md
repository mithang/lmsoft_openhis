# Pipeline asset dùng chung — kết luận chặng 3 nền tảng

> Ngày xác định: 2026-10-28. Ngôn ngữ: tiếng Việt.
> Kết luận ngắn: **KHÔNG sửa file trên static host trong chặng này.**
> File dùng chung mới sống trong repo `openhis-core`, được đồng bộ vào
> `wwwroot` của từng app và nạp **app-local** (sau theme). Lý do và bằng
> chứng ở dưới. Mọi đường dẫn đều là đường dẫn tuyệt đối trên máy này.

## 1. Static host 127.0.0.1:41142 đang phục vụ thư mục nào?

- Tiến trình đang chạy (kiểm chứng `ps` ngày 2026-10-28):

  ```
  python3 /home/hatch/workspace/static-cors-server.py 41142 \
      /home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.Static/Newtouch.HIS.Static
  ```

  tức là static host phục vụ **thư mục run-copy**, không phải repo
  `openhis-core`, cũng không phải bản clone gốc `~/workspace/lmsoft_openhis`.
- Kiểm chứng nội dung: `curl http://127.0.0.1:41142/css/framework-theme.css`
  trả 200, md5 của body HTTP **trùng** md5 của file trên đĩa tại
  `lmsoft_openhis_run/Newtouch.HIS.Static/Newtouch.HIS.Static/css/framework-theme.css`
  (`e86d5bebb29e426fdf8a7dca08d8850f`). Server là file server thuần
  (chỉ thêm header `Access-Control-Allow-Origin: *`): sửa file trên đĩa là
  đổi ngay nội dung phục vụ, **không cần restart**, nhưng cũng **không có
  versioning** (xem §4).
- Khôi phục sau restart: `~/workspace/recovery.sh` §4 chỉ start lại server
  này khi cổng 41142 down, với đúng thư mục trên. Nó không đồng bộ file
  từ đâu cả.

## 2. File CSS/JS dùng chung có nguồn repo ở đâu?

- **Trong `openhis-core`: KHÔNG CÓ project `Newtouch.HIS.Static`.**
  Grep toàn repo không thấy thư mục Static nào (chỉ có bản sao lẻ
  `Newtouch.Sett.Web/wwwroot/Content/css/framework-theme.css`, không được
  layout nào tham chiếu — xem §5).
- Nguồn gốc duy nhất của các file static host là bản vendor V2.2:
  `~/workspace/lmsoft_openhis/Newtouch.HIS.Static/Newtouch.HIS.Static/`
  (clone gốc) và bản run-copy nói trên. Hai bản đã **lệch nhau**:
  `framework-theme.css` còn giống hệt, nhưng `framework-ui.css`,
  `framework-ui.js`, `Newtouch-ui.js` đã khác bản gốc (đã bị sửa trong
  quá trình port trước đây, không có commit nào trong `openhis-core` ghi
  lại các sửa đó).
- Run-copy là một **repo git riêng**, origin của nó là đường dẫn local
  `~/workspace/lmsoft_openhis` — không phải remote `mithang/lmsoft_openhis`
  của `openhis-core`. Vì vậy sửa file trong run-copy **không thể commit
  qua `openhis-core`**, điều phối viên không soát được bằng `git diff`
  của `openhis-core`, và file đó cũng không đi theo bản deploy từ repo.

## 3. Cơ chế tham chiếu (đọc code, không đoán)

File: `openhis-core/Newtouch.HIS.Lib/NewtouchHIS.Lib.Base/Utilities/MvcHelper.cs`

- `MvcHelper.GetStaticResourceScriptUrl(url, isInSiteStatic = true)`:
  - `true` (mặc định): trả về `SiteStaticHost + url` (bỏ `~/`).
    `SiteStaticHost` lấy từ appsettings
    `BusinessConfig.AppAPIHost.SiteStaticHost`
    = `http://127.0.0.1:41142/` ở mọi app.
  - `false`: trả về `/ + url` (bỏ `~/`) **cộng `?V=<ver>`**.
    Đây là đường **app-local**: ASP.NET Core phục vụ từ `wwwroot` của
    chính app đó. `ver` = ticks hiện tại ở DEBUG, = cấu hình `Version`
    ở Release (cache-buster).
- Bằng chứng app-local đã được dùng sẵn trong repo:
  - `/js/jqgrid/grid.locale-vi.js` trong mọi `_LayoutIndex.cshtml`
    → file thật tại `<App>/wwwroot/js/jqgrid/grid.locale-vi.js`
    (ví dụ CIS: `Newtouch.HIS.CIS/Newtouch.CIS.Web/wwwroot/js/jqgrid/grid.locale-vi.js`).
  - SSO portal: `~/css/bootstrap-extensions.css`, `~/js/site.js`
    → `Newtouch.HIS.SSO/HIS.SSO/wwwroot/css|js/...`.
- Vì vậy một file mới đặt trong `wwwroot` của app và nạp bằng
  `GetStaticResourceScriptUrl("~/css/redesign-tokens.css", false)` sẽ được
  chính app đó phục vụ, có cache-buster, và đi theo repo khi commit.

## 4. Quyết định cho chặng nền tảng (đã chốt, có thể đảo ngược)

1. **Không chạm static host.** Sửa file run-copy sẽ đổi giao diện
   **cả prod lẫn STG ngay lập tức** (cả hai cùng dùng cổng 41142, không
   có phân biệt môi trường), không qua review/commit của `openhis-core`,
   và không có `?V=` nên trình duyệt có thể vẫn dùng bản cache cũ.
   Đây là đường runtime-only, không phải đường deploy.
2. **Nguồn chuẩn duy nhất (single source of truth):**
   `openhis-core/scripts/redesign/assets/`:
   - `redesign-tokens.css` — token + rule dùng chung (§2 DESIGN.md).
   - `redesign-grid.js` — helper jqGrid (resize/sidebar, wrapper cuộn A).
   - `redesign-portal.css` / `redesign-portal.js` — drawer portal SSO.
3. **Đồng bộ vào wwwroot bằng script** (không copy tay):
   `openhis-core/scripts/redesign/sync-assets.sh` copy từ `assets/` vào
   `wwwroot/css|js/` của 10 app nghiệp vụ + SSO, in checksum sau copy.
   Các file wwwroot này **là bản sao đã sinh (generated copy)**, nguồn
   chuẩn vẫn là `assets/`; sửa tay trong wwwroot rồi quên sync ngược sẽ
   bị ghi đè ở lần sync sau — script in cảnh báo này mỗi lần chạy.
4. **Nạp sau theme trong layout** (để token override theme mà không sửa
   theme): ngay sau dòng `framework-theme.css` (`id="color-skin"`) thêm

   ```razor
   <link href="@MvcHelper.GetStaticResourceScriptUrl("~/css/redesign-tokens.css", false)" rel="stylesheet" />
   ```

   và ngay sau dòng `Newtouch-ui.js` thêm

   ```razor
   <script src="@MvcHelper.GetStaticResourceScriptUrl("~/js/redesign-grid.js", false)"></script>
   ```

   Áp dụng cho cả `_LayoutIndex.cshtml` (trang danh sách) và
   `_Form.cshtml` (trang form; layout này đã có sẵn
   `<meta name="viewport" content="width=device-width" />`, còn
   `_LayoutIndex.cshtml` thì **chưa có viewport** — chặng này bổ sung).
5. **Đường lên static host trong tương lai (chưa làm, cần quyết riêng):**
   khi nào cần một file thực sự dùng chung qua 41142 (ví dụ để tránh
   11 bản sao wwwroot), điều phối viên quyết rồi mới copy file chuẩn từ
   `assets/` vào thư mục static host **tại thời điểm deploy**, kèm đổi
   `?V=` hoặc tên file có version, và ghi lại commit/deploy tương ứng.
   Không tự ý làm bước này trong chặng nền tảng.

## 5. Các layout và wwwroot liên quan (đã đối chiếu file thật)

| App | Web project (openhis-core) | Layout đã nối |
|---|---|---|
| SSO portal | `Newtouch.HIS.SSO/HIS.SSO` | `Views/Home/SSOIndex.cshtml` (portal shell, `Layout = null`), `Views/Shared/_Index.cshtml`, `Views/Shared/_IndexNavCol.cshtml` (nút đóng drawer) |
| MRQC | `Newtouch.MRQC.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| Sett | `Newtouch.Sett.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| MRMS | `Newtouch.HIS.MRMS/Newtouch.MRMS.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| OR | `Newtouch.HIS.OR/Newtouch.OR.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| Herp | `Newtouch.HIS.Herp/Newtouch.Herp.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| EMR | `Newtouch.HIS.EMR/Newtouch.EMR.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| Base HOSP | `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| Base Web | `Newtouch.HIS.Base/Newtouch.HIS.Base.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| CIS | `Newtouch.HIS.CIS/Newtouch.CIS.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |
| PDS | `Newtouch.HIS.PDS/Newtouch.PDS.Web` | `_LayoutIndex.cshtml`, `_Form.cshtml` |

- Trang chủ các app (ví dụ PDS `Views/Home/Index.cshtml`, CIS
  `Views/Home/Index.cshtml`) đặt `Layout = null` và tự mang `<head>`
  riêng — **không** ăn theo `_LayoutIndex.cshtml`. Sửa home/fragment là
  việc per-page của chặng sau; nền tảng chỉ chuẩn bị token/helper để
  các trang đó dùng lại.
- Footer vendor tiếng Trung trong `_IndexNavCol.cshtml` (chuỗi
  `Copyright © 2025…` + link tiếng Trung): **chỉ liệt kê, không viết lại**
  theo quy tắc chương trình — chặng này không đụng tới span đó.
- Mâu thuẫn receipt-prefix, 22 quyết định mở từ sweep cuối: ngoài phạm
  vi, không tự quyết (xem báo cáo nền tảng).

## 6. Cách kiểm chứng pipeline (ai cũng làm lại được)

```bash
# 1. Static host đang phục vụ thư mục nào?
ps aux | grep static-cors-server | grep -v grep
# 2. Nội dung live có đúng file đĩa run-copy không?
curl -s http://127.0.0.1:41142/css/framework-theme.css | md5sum
md5sum ~/workspace/lmsoft_openhis_run/Newtouch.HIS.Static/Newtouch.HIS.Static/css/framework-theme.css
# 3. openhis-core có Static project không? (kết quả phải là rỗng)
find ~/workspace/openhis-core -type d -name "*Static*" -not -path "*/obj/*" -not -path "*/bin/*"
# 4. File app-local mới có được app phục vụ không? (sau khi chạy STG, xem báo cáo)
curl -s -o /dev/null -w "%{http_code}\n" http://127.0.0.1:15237/css/redesign-tokens.css
```
