import sys
sys.path.insert(0, '/home/hatch/workspace/shot')
from playwright.sync_api import sync_playwright

BASE = 'http://127.0.0.1:5223'
PAGES = [
    ('HospitalizationManage/DischargeSettle/CancelIndex', 'cancelindex'),
    ('HospitalizationManage/DischargeSettle/ConfirmForm', 'confirmform'),
    ('HospitalizationManage/BookkeepInHos/InpatientAccountingIndex', 'inpatientaccounting'),
    ('HospitalizationManage/Settlement/GetSettlementItems', 'settlementitems'),
]
with sync_playwright() as p:
    b = p.firefox.launch()
    pg = b.new_page(viewport={'width': 1366, 'height': 900})
    errs = []
    pg.on('console', lambda m: errs.append(f"{m.type}: {m.text}") if m.type == 'error' else None)
    pg.on('pageerror', lambda e: errs.append(f"pageerror: {e}"))
    for path, name in PAGES:
        try:
            r = pg.goto(f'{BASE}/{path}', wait_until='networkidle')
            body = pg.inner_text('body')
            cjk = sum(1 for ch in body if '\u4e00' <= ch <= '\u9fff')
            print(f'{path}: status={r.status if r else None} url={pg.url} chars={len(body)} CJK={cjk}')
            print(f'  head: {body[:200].replace(chr(10), " | ")}')
            pg.screenshot(path=f'/home/hatch/workspace/openhis-core/sett-stage-hospitalization/shots/{name}.png')
        except Exception as e:
            print(f'{path}: FAILED {e}')
    # JSON actions (same session, no login needed in temp build)
    for label, url in [
        ('GetMidwaySettedListJson', '/HospitalizationManage/BookkeepInHos/GetMidwaySettedListJson?zyh=01000'),
        ('GetGridJson', '/HospitalizationManage/BookkeepInHos/GetGridJson?zyh=01000&kh=10000'),
    ]:
        res = pg.evaluate("""async (u) => {
            const r = await fetch(u); const t = await r.text();
            return {status: r.status, len: t.length, text: t.slice(0, 300)};
        }""", url)
        print(f'{label}: {res}')
    print('JS console errors:', len(errs))
    for e in errs[:10]:
        print(' -', e)
    b.close()
