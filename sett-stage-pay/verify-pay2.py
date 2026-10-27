import sys
sys.path.insert(0, '/home/hatch/workspace/shot')
from playwright.sync_api import sync_playwright

BASE = 'http://127.0.0.1:5224'
errs = []
with sync_playwright() as p:
    b = p.firefox.launch()
    pg = b.new_page(viewport={'width': 1366, 'height': 900})
    pg.on('pageerror', lambda e: errs.append(f"pageerror: {e}"))
    pg.goto(f'{BASE}/Login/Index', wait_until='networkidle')
    pg.fill('#txt_account', '000000')
    pg.fill('#txt_password', '000000')
    pg.click('#login_button')
    pg.wait_for_url(f'{BASE}/Home/Index', timeout=30000)
    print('logged in')

    # Enums endpoint serves PayManage enums with Vietnamese descriptions?
    enums = pg.evaluate("""async () => {
        const r = await fetch('/ClientsData/GetAsyncClientsDataJson');
        const t = await r.text();
        return {status: r.status, hasTrade: t.includes('EnumTradeType'), hasRefund: t.includes('EnumRefundStatus'),
                vi: t.includes('Thanh toán') || t.includes('Hoàn tiền'), slice: t.slice(0, 400)};
    }""")
    print('enums endpoint:', enums)

    # TradeInfo with nonexistent order: must NOT throw pageerror after null-guard fix
    errs.clear()
    pg.goto(f'{BASE}/PayManage/MicroPay/TradeInfo?outtradeno=NOPE', wait_until='networkidle')
    pg.wait_for_timeout(3000)
    print('TradeInfo pageerrors after fix:', [e for e in errs if 'PayAmount' in e] or 'none (fixed)')

    # MicroPayQuery grid endpoint again on final build
    api = pg.evaluate("""async () => {
        const r = await fetch('/PayManage/MicroPay/MicroTradeQuery?ksrq=2026-10-19&jsrq=2026-10-26&page=1&rows=15');
        return {status: r.status, text: await r.text()};
    }""")
    print('MicroTradeQuery final:', api)
    b.close()
