import sys, json
sys.path.insert(0, '/home/hatch/workspace/shot')
from playwright.sync_api import sync_playwright

BASE = 'http://127.0.0.1:5223'
errs = []
with sync_playwright() as p:
    b = p.firefox.launch()
    pg = b.new_page(viewport={'width': 1366, 'height': 900})
    pg.on('console', lambda m: errs.append(f"{m.type}: {m.text}") if m.type == 'error' else None)
    pg.on('pageerror', lambda e: errs.append(f"pageerror: {e}"))
    r = pg.goto(f'{BASE}/Login/Index', wait_until='networkidle')
    print('login page status:', r.status if r else None)
    pg.fill('#txt_account', '000000')
    pg.fill('#txt_password', '000000')
    pg.click('#login_button')
    try:
        pg.wait_for_url(f'{BASE}/Home/Index', timeout=25000)
        print('logged in, at', pg.url)
    except Exception as e:
        print('login wait failed:', e, 'at', pg.url)

    # 1. BookkeepInHos/Index render
    r = pg.goto(f'{BASE}/HospitalizationManage/BookkeepInHos/Index', wait_until='networkidle')
    print('Bookkeep Index status:', r.status if r else None, 'url:', pg.url)
    body = pg.inner_text('body')
    for marker in ['Truy vấn bệnh nhân', 'Mã nhập viện', 'Số thẻ', 'Mục thu phí', 'Ngày ghi sổ']:
        print(f'  marker {marker!r}:', 'FOUND' if marker in body else 'MISSING')
    print('  Chinese chars in body:', sum(1 for ch in body if '\u4e00' <= ch <= '\u9fff'))
    pg.screenshot(path='/home/hatch/workspace/openhis-core/sett-stage-hospitalization/shots/bookkeep-index.png')

    # 2. GetGridJson with the only demo patient (expected: patient-not-found, xt_brzh empty)
    grid = pg.evaluate("""async () => {
        const res = await fetch('/HospitalizationManage/BookkeepInHos/GetGridJson?zyh=01000&kh=10000');
        return {status: res.status, text: (await res.text()).slice(0, 500)};
    }""")
    print('GetGridJson zyh=01000:', grid)

    # 3. Unsettled fee items for 01000 (billing rows exist: 4 item + 7 drug)
    unset = pg.evaluate("""async () => {
        const res = await fetch('/HospitalizationManage/BookkeepInHos/GetUnSettedFeeItemListJson?zyh=01000');
        const t = await res.text();
        return {status: res.status, len: t.length, text: t.slice(0, 800)};
    }""")
    print('GetUnSettedFeeItemListJson zyh=01000:', unset)

    # 4. DischargeSettle/Index render proof
    r = pg.goto(f'{BASE}/HospitalizationManage/DischargeSettle/Index', wait_until='networkidle')
    print('DischargeSettle Index status:', r.status if r else None)
    body2 = pg.inner_text('body')
    print('  Chinese chars in body:', sum(1 for ch in body2 if '\u4e00' <= ch <= '\u9fff'))
    pg.screenshot(path='/home/hatch/workspace/openhis-core/sett-stage-hospitalization/shots/dischargesettle-index.png')

    print('JS console errors:', len(errs))
    for e in errs[:10]:
        print(' -', e)
    b.close()
