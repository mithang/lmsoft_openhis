# -*- coding: utf-8 -*-
"""CIS Web translation sweep, tier 4: tail display fragments.
Stricter skip: also skip case lines and data/api/url lines (values, params)."""
import os, re

W = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
COMMENT = re.compile(r"(@\*.*?\*@|<!--.*?-->|(?<!:)//[^\n]*|/\*.*?\*/)", re.DOTALL)
SKIP_LINE = re.compile(r"(==|!=|\.Equals\(|^\s*case\s|SELECT\s|FROM\s|WHERE\s|INSERT\s+INTO|UPDATE\s+\w+\s+SET|DELETE\s+FROM|\bEXEC\b|Log\.|logger\.|Console\.Write|https?://|data\s*:|url\(|\$\.ajax|\$\.get\(|\$\.post\(|params\s*:)")


def load_dict(path, var):
    src = open(path, encoding='utf-8').read()
    dsrc = var + " = " + src.split(var + ' = ', 1)[1]
    dsrc = dsrc[:dsrc.index('\n}\n') + 3]
    ns = {}
    exec(dsrc, ns)
    return ns[var]


D = load_dict('/tmp/cis-tier1.py', 'D')
for pth, var in [('/tmp/cis-tier2.py', 'NEW'), ('/tmp/cis-tier3.py', 'NEW3')]:
    for k, v in load_dict(pth, var).items():
        D.setdefault(k, v)

NEW4 = {
"未婚": "Chưa kết hôn", "已婚": "Đã kết hôn",
"麻醉": "Gây mê", "开方": "Kê đơn", "病床": "Giường bệnh",
"级别": "Cấp độ", "尚未": "Chưa", "分管": "Phụ trách",
"新的": "Mới", "主治": "Điều trị chính", "特殊": "Đặc biệt",
"离休干部": "Cán bộ nghỉ hưu", "特病": "Bệnh đặc biệt", "慢性病": "Bệnh mạn tính",
"精神": "Tinh thần", "存在": "Tồn tại", "页面": "Trang",
"请注意": "Vui lòng chú ý", "请您": "Vui lòng", "将被": "sẽ bị",
"将为您": "sẽ cho bạn", "注意": "Chú ý", "重新": "Lại",
"文字": "Văn bản", "带药": "Mang thuốc", "膳食": "Ăn uống",
"直接": "Trực tiếp", "企业": "Doanh nghiệp", "意见": "Ý kiến",
"定点": "Chỉ định", "复用": "Tái sử dụng", "业务": "Nghiệp vụ",
"通用名": "Tên chung", "所需": "Cần thiết", "中医病名": "Tên bệnh YHCT",
"中医": "YHCT", "査询": "Tra cứu", "家庭": "Gia đình",
"允许": "Cho phép", "大屏": "Màn hình lớn", "入住": "Nhập vào",
"口服药单": "Phiếu thuốc uống", "试管": "Ống nghiệm", "经营企业": "Doanh nghiệp kinh doanh",
"不明": "Không rõ", "肯定": "Chắc chắn", "无关": "Không liên quan",
"待评价": "Chờ đánh giá", "无法评价": "Không thể đánh giá", "成组": "Thành nhóm",
"上下值": "Giá trị trên dưới", "病情观察": "Theo dõi bệnh tình", "及措施": "và biện pháp",
"无需": "Không cần", "拒测": "Từ chối đo", "卧床": "Nằm giường",
"转入": "Chuyển vào", "不同": "Khác nhau", "请在浮层中": "Vui lòng trong lớp nổi",
"并点击": "và nhấp", "不在": "Không ở", "期间": "Trong thời gian",
"请到": "Vui lòng đến", "分配多个": "Phân bổ nhiều", "请重新分配": "Vui lòng phân bổ lại",
"批发": "Bán buôn", "转换因子": "Hệ số chuyển đổi", "当页": "Trang hiện tại",
"条件": "Điều kiện", "朝向": "Hướng", "主病": "Bệnh chính",
# special display literals
">治未病</a>": ">Y học dự phòng</a>",
'type="radio"> 多生牙': 'type="radio"> Răng thừa',
}
D4 = {k: v for k, v in NEW4.items() if k not in D and len(k) >= 2}
keys = sorted(D4, key=len, reverse=True)
changed = 0; replaced = 0
for root, dirs, files in os.walk(W):
    dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
    for fn in files:
        if not (fn.endswith('.cshtml') or fn.endswith('.cs') or fn.endswith('.js')):
            continue
        p = os.path.join(root, fn)
        s = open(p, encoding='utf-8-sig', errors='ignore').read()
        parts = COMMENT.split(s)
        out = []; n = 0
        for i, part in enumerate(parts):
            if i % 2 == 1:
                out.append(part); continue
            plines = part.split('\n')
            nlines = []
            for ln in plines:
                if SKIP_LINE.search(ln):
                    nlines.append(ln); continue
                for k in keys:
                    if k in ln:
                        c = ln.count(k)
                        ln = ln.replace(k, D4[k]); n += c
                nlines.append(ln)
            out.append('\n'.join(nlines))
        if n:
            open(p, 'w', encoding='utf-8').write(''.join(out))
            changed += 1; replaced += n
print("tier4 files changed:", changed, "| replacements:", replaced, "| dict size:", len(D4))
