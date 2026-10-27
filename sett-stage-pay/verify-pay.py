import sys
sys.path.insert(0, '/home/hatch/workspace/shot')
from playwright.sync_api import sync_playwright

BASE = 'http://127.0.0.1:5224'
errs = []
with sync_playwright() as p:
    b = p.firefox.launch()
    pg = b.new_page(viewport={'width': 1366, 'height': 900})
    pg.on('console', lambda m: errs.append(f"{m.type}: {m.text}") if m.type == 'error' else None)
    pg.on('pageerror', lambda e: errs.append(f"pageerror: {e}"))
    pg.goto(f'{BASE}/Login/Index', wait_until='networkidle')
    pg.fill('#txt_account', '000000')
    pg.fill('#txt_password', '000000')
    pg.click('#login_button')
    pg.wait_for_url(f'{BASE}/Home/Index', timeout=30000)
    print('logged in, at', pg.url)

    # 1. MicroPayQuery page render
    pg.goto(f'{BASE}/PayManage/MicroPay/MicroPayQuery', wait_until='networkidle')
    pg.wait_for_timeout(4000)
    body = pg.inner_text('body')
    print('QUERY page has Vietnamese labels:', 'Phương thức thanh toán' in body, '| Tra cứu' in body)
    print('QUERY page still has Chinese labels:', ('支付方式' in body), ('查询' in body and 'Tra cứu' not in body))
    grid_html = (pg.inner_html('#gridList') or '')[:300]
    print('grid html snippet:', grid_html.replace('\n', ' ')[:300])
    pg.screenshot(path='/home/hatch/workspace/openhis-core/sett-stage-pay/payquery.png')
    print('screenshot: payquery.png')

    # 2. Direct API check: MicroTradeQuery (jqGrid params)
    api = pg.evaluate("""async () => {
        const r = await fetch('/PayManage/MicroPay/MicroTradeQuery?ksrq=2026-10-19&jsrq=2026-10-26&page=1&rows=15&sidx=OutTradeNo&sord=desc');
        return {status: r.status, text: (await r.text()).slice(0, 600)};
    }""")
    print('MicroTradeQuery API:', api)

    # 3. TradeRefundList + GetTradeInfo (empty, but must be 200 JSON)
    api2 = pg.evaluate("""async () => {
        const r = await fetch('/PayManage/MicroPay/TradeRefundList?outTradeNo=NOPE&tradeNo=');
        const r2 = await fetch('/PayManage/MicroPay/GetTradeInfo?outTradeNo=NOPE');
        return {refund: {status: r.status, text: (await r.text()).slice(0, 200)},
                info: {status: r2.status, text: (await r2.text()).slice(0, 200)}};
    }""")
    print('RefundList/GetTradeInfo API:', api2)

    # 4. Stub actions return explicit error JSON, not 404/fake success
    api3 = pg.evaluate("""async () => {
        const r = await fetch('/PayManage/MicroPay/MicroPaySubmit', {method: 'POST',
            headers: {'Content-Type': 'application/x-www-form-urlencoded'},
            body: 'out_trade_no=T1&subject=test&total_amount=0.01&auth_code=123&djjesszffs=1&patid=P1'});
        const r2 = await fetch('/PayManage/MicroPay/Opr_TradeRefund?outTradeNo=NOPE&refundAmount=0.01&Reason=test');
        return {submit: {status: r.status, text: (await r.text()).slice(0, 300)},
                refund: {status: r2.status, text: (await r2.text()).slice(0, 300)}};
    }""")
    print('Stub actions:', api3)

    # 5. Other views render (form pages)
    for name in ['MicroPay', 'TradeInfo', 'TradeRefund']:
        pg.goto(f'{BASE}/PayManage/MicroPay/{name}', wait_until='networkidle')
        pg.wait_for_timeout(1500)
        t = pg.inner_text('body')
        print(name, 'HTTP ok, body chars:', len(t), '| Vietnamese present:',
              any(k in t for k in ['Mã thanh toán', 'Mã đơn hàng', 'Số tiền còn có thể hoàn']))
    pg.goto(f'{BASE}/PayManage/MicroPay/MicroPayQuery', wait_until='networkidle')
    pg.wait_for_timeout(2000)
    print('JS console errors:', len(errs))
    for e in errs[:10]:
        print(' -', e)
    b.close()
