#!/usr/bin/env python3
"""Sett Phase 1 merge: 3 staging apps -> main scaffold (~/workspace/openhis-core/).
Corrected helpers: block spans are (line_start, open_brace, close_brace).
Rules:
- Interfaces/enums/DTOs/entity in Newtouch.Sett.Domain are UNIONED (overloads coexist).
- One impl per colliding interface (best one wins); losers are stripped, never copied.
- Main scaffold HintPaths + port 5221 kept; staging csproj path changes NOT merged.
- BottomButtonViewModel is merged manually afterwards (not in this script).
- No DB writes, no run-copy edits.
"""
import os, re, shutil

BASE = os.path.expanduser('~/workspace/openhis-core')
MAIN_W = f'{BASE}/Newtouch.Sett.Web'
MAIN_I = f'{BASE}/Newtouch.Sett.Infrastructure'
ST = {s: f'{BASE}/sett-stage-{s}' for s in ('outpatient', 'hospitalization', 'pay')}
os.makedirs(f'{MAIN_I}/DomainServices', exist_ok=True)
os.makedirs(f'{MAIN_W}/Helpers', exist_ok=True)
os.makedirs(f'{MAIN_W}/Areas', exist_ok=True)


def log(m):
    print(m)


def find_block(txt, kind, name):
    """Find 'kind name' declaration; return (line_start, open_brace, close_brace) or None."""
    i = txt.find(kind + ' ' + name)
    if i < 0:
        return None
    ls = txt.rfind('\n', 0, i) + 1
    j = txt.find('{', i)
    if j < 0:
        return None
    depth = 0
    for k in range(j, len(txt)):
        if txt[k] == '{':
            depth += 1
        elif txt[k] == '}':
            depth -= 1
            if depth == 0:
                return (ls, j, k)
    return None


def strip_classes(txt, classes):
    for cls in classes:
        b = find_block(txt, 'class', cls)
        if b:
            ls, _, cb = b
            end = cb + 1
            if end < len(txt) and txt[end] == '\n':
                end += 1
            txt = txt[:ls] + txt[end:]
            log(f'  stripped class {cls}')
        else:
            log(f'  (class {cls} not found, skip)')
    return txt


def block_text(txt, kind, name):
    b = find_block(txt, kind, name)
    return txt[b[0]:b[2] + 1] if b else None


def interface_members(txt, name):
    b = find_block(txt, 'interface', name)
    if not b:
        return []
    inner = txt[b[1] + 1:b[2]]
    inner = re.sub(r'//.*', '', inner)
    inner = re.sub(r'/\*.*?\*/', '', inner, flags=re.S)
    return [m.strip() for m in inner.split(';') if m.strip()]


def add_members_to_interface(txt, name, members):
    b = find_block(txt, 'interface', name)
    assert b, f'interface {name} not found'
    ins = '\n' + '\n'.join('        ' + m + ';' for m in members) + '\n    '
    return txt[:b[2]] + ins + txt[b[2]:]


def replace_interface_body(txt, name, members):
    b = find_block(txt, 'interface', name)
    assert b, f'interface {name} not found'
    body = '\n' + '\n'.join('        ' + m + ';' for m in members) + '\n    '
    return txt[:b[1] + 1] + body + txt[b[2]:]


def change_interface_decl(txt, name, new_decl):
    """Replace 'interface Name...' header (up to open brace) with new_decl."""
    b = find_block(txt, 'interface', name)
    assert b, f'interface {name} not found'
    return txt[:b[0]] + new_decl + ' {' + txt[b[1] + 1:]


def add_methods_to_class(txt, cls, methods):
    b = find_block(txt, 'class', cls)
    assert b, f'class {cls} not found'
    ins = '\n' + '\n'.join(methods) + '\n    '
    return txt[:b[2]] + ins + txt[b[2]:]


def replace_enum_block(txt, name, new_block):
    b = find_block(txt, 'enum', name)
    assert b, f'enum {name} not found'
    return txt[:b[0]] + new_block + txt[b[2] + 1:]


def copy(src, dst):
    os.makedirs(os.path.dirname(dst), exist_ok=True)
    shutil.copy2(src, dst)
    log(f'  copied {src.replace(BASE, "")} -> {dst.replace(BASE, "")}')


# ---------------------------------------------------------------- Web tree
log('== Web: Areas ==')
for area, s in [('OutpatientManage', 'outpatient'), ('HospitalizationManage', 'hospitalization'), ('PayManage', 'pay')]:
    src = f"{ST[s]}/Newtouch.Sett.Web/Areas/{area}"
    dst = f"{MAIN_W}/Areas/{area}"
    if os.path.exists(dst):
        shutil.rmtree(dst)
    shutil.copytree(src, dst, ignore=shutil.ignore_patterns('bin', 'obj'))
    log(f'  area {area} from {s}')
log('== Web: Areas/_ViewImports (outpatient superset) ==')
copy(f"{ST['outpatient']}/Newtouch.Sett.Web/Areas/_ViewImports.cshtml",
     f"{MAIN_W}/Areas/_ViewImports.cshtml")
log('== Web: Hosp area _ViewImports ==')
copy(f"{ST['hospitalization']}/Newtouch.Sett.Web/Areas/HospitalizationManage/Views/_ViewImports.cshtml",
     f"{MAIN_W}/Areas/HospitalizationManage/Views/_ViewImports.cshtml")
log('== Web: root Shared extras from Outpatient ==')
for f in ['YibaoRedCardCommon.cshtml', '_YibaoCommonView.cshtml', '_MiddleButtonsView.cshtml']:
    copy(f"{ST['outpatient']}/Newtouch.Sett.Web/Views/Shared/{f}",
         f"{MAIN_W}/Views/Shared/{f}")
log('== Web: Hosp area Shared stubs ==')
for f in ['_MiddleButtonsView.cshtml', '_YibaoCommonView.cshtml', 'YibaoRedCardCommon.cshtml']:
    copy(f"{ST['hospitalization']}/Newtouch.Sett.Web/Areas/HospitalizationManage/Views/Shared/{f}",
         f"{MAIN_W}/Areas/HospitalizationManage/Views/Shared/{f}")
log('== Web: Helpers + ControllerBase ==')
copy(f"{ST['hospitalization']}/Newtouch.Sett.Web/Helpers/SysConfigReader.cs",
     f"{MAIN_W}/Helpers/SysConfigReader.cs")
copy(f"{ST['outpatient']}/Newtouch.Sett.Web/Controllers/ControllerBase.cs",
     f"{MAIN_W}/Controllers/ControllerBase.cs")
log('== Web: ClientsDataController (scaffold + Pay enums) ==')
cdc = open(f'{MAIN_W}/Controllers/ClientsDataController.cs', encoding='utf-8').read()
old = '                enums = new object[0],'
new = ('                // PayManage enums (Infrastructure ns, no clash with Domain enums).\n'
       '                enums = new[]\n'
       '                {\n'
       '                    new { Type = "EnumTradeType", Items = EnumItems<EnumTradeType>() },\n'
       '                    new { Type = "EnumRefundStatus", Items = EnumItems<EnumRefundStatus>() },\n'
       '                },')
if old in cdc:
    cdc = cdc.replace(old, new)
    open(f'{MAIN_W}/Controllers/ClientsDataController.cs', 'w', encoding='utf-8').write(cdc)
    log('  added Pay enums to ClientsDataController')
elif 'EnumTradeType' in cdc:
    log('  (Pay enums already present, skip)')

# ---------------------------------------------------------------- Infra copies
log('== Infra: OUT files ==')
OUT_I = f"{ST['outpatient']}/Newtouch.Sett.Infrastructure"
copy(f'{OUT_I}/Domain/Helpers.cs', f'{MAIN_I}/Domain/Helpers.cs')
copy(f'{OUT_I}/Domain/RealTypes.cs', f'{MAIN_I}/Domain/RealTypes.cs')
# NOTE: OUT Repository/Repositories.cs is identical to the scaffold generic base -> skip it
# (HOS superset base replaces the scaffold base below).
types = open(f'{OUT_I}/Domain/Types.cs', encoding='utf-8').read()
types = strip_classes(types, ['CqybSett05Entity', 'OutpatAccBasicInfoDto', 'OutpatAccGridInfoDto'])
open(f'{MAIN_I}/Domain/Types.cs', 'w', encoding='utf-8').write(types)
log('  wrote OUT Types.cs minus 3 colliding types')
stubs = open(f'{OUT_I}/Domain/Stubs.cs', encoding='utf-8').read()
stubs = strip_classes(stubs, ['PayApp', 'SysConfigRepo', 'PatientBasicInfoDmnService',
                              'CqybSett05Repo', 'OrderPayInfoRepo'])
open(f'{MAIN_I}/DomainServices/OutStubs.cs', 'w', encoding='utf-8').write(stubs)
log('  wrote OUT Stubs.cs minus 5 colliding impl classes')
out_if = open(f'{OUT_I}/Domain/Interfaces.cs', encoding='utf-8').read()
hos_if = open(f"{ST['hospitalization']}/Newtouch.Sett.Infrastructure/Domain/Interfaces.cs", encoding='utf-8').read()
pay_if = open(f"{ST['pay']}/Newtouch.Sett.Infrastructure/Domain/Interfaces.cs", encoding='utf-8').read()

log('== Infra: HOS files ==')
HOS_I = f"{ST['hospitalization']}/Newtouch.Sett.Infrastructure"
copy(f'{HOS_I}/Domain/Entities.cs', f'{MAIN_I}/Domain/Entities.cs')
dtos = open(f'{HOS_I}/Domain/DTOs.cs', encoding='utf-8').read()
dtos = add_methods_to_class(dtos, 'OutpatAccBasicInfoDto', [
    '        public string mzh { get; set; }',
    '        public string fph { get; set; }',
    '        public bool isQfyj { get; set; }',
    '        public DateTime? sfrq { get; set; }'])
open(f'{MAIN_I}/Domain/DTOs.cs', 'w', encoding='utf-8').write(dtos)
log('  wrote HOS DTOs.cs + OUT OutpatAccBasicInfoDto props')
hos_stubs = open(f'{HOS_I}/DomainServices/Stubs.cs', encoding='utf-8').read()
hos_stubs = strip_classes(hos_stubs, ['SysUserDmnServiceStub', 'SyncTreatmentServiceRecordDmnServiceStub',
                                      'RefundDmnServiceStub', 'OutPatientDmnServiceStub',
                                      'OutPatChargeAppStub', 'PayAppStub'])
open(f'{MAIN_I}/DomainServices/HosStubs.cs', 'w', encoding='utf-8').write(hos_stubs)
log('  wrote HOS Stubs.cs minus 6 colliding stub classes')
real = open(f'{HOS_I}/DomainServices/RealServices.cs', encoding='utf-8').read()
real = add_methods_to_class(real, 'SysConfigRepo', [
    '        public bool? GetBoolValueByCode(object code, object orgId)',
    '        {',
    '            return GetBoolValueByCode(code?.ToString(), orgId?.ToString(), null);',
    '        }',
    '        public bool? GetBoolValueByCode(object code, object orgId, object defaultValue)',
    '        {',
    '            bool? dv = defaultValue is bool b ? b : (bool?)null;',
    '            return GetBoolValueByCode(code?.ToString(), orgId?.ToString(), dv);',
    '        }',
    '        public SysConfigDto GetByCode(object code, object orgId)',
    '        {',
    '            var e = GetByCode(code?.ToString(), orgId?.ToString());',
    '            return e == null ? null : new SysConfigDto { Code = e.Code, Value = e.Value };',
    '        }',
    '        public string GetValueByCode(object code, object orgId)',
    '        {',
    '            return GetValueByCode(code?.ToString(), orgId?.ToString());',
    '        }'])
real = add_methods_to_class(real, 'PatientBasicInfoDmnService', [
    '        public object GetQHDjzdjInfo(object p0, object p1)',
    '        {',
    '            return null;',
    '        }'])
open(f'{MAIN_I}/DomainServices/RealServices.cs', 'w', encoding='utf-8').write(real)
log('  wrote HOS RealServices.cs + OUT object members + GetQHDjzdjInfo')
repos = open(f'{HOS_I}/Repository/Repos.cs', encoding='utf-8').read()
repos = add_methods_to_class(repos, 'CqybSett05Repo', [
    '        public object SaveCqybSett(object p0, object p1)',
    '        {',
    '            throw new NotSupportedException("Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc).");',
    '        }'])
open(f'{MAIN_I}/Repository/HosRepos.cs', 'w', encoding='utf-8').write(repos)
log('  wrote HOS Repos.cs + SaveCqybSett stub')
copy(f'{HOS_I}/Repository/Repositories.cs', f'{MAIN_I}/Repository/Repositories.cs')
hos_helpers = f'{HOS_I}/Infrastructure/Helpers.cs'
if os.path.exists(hos_helpers):
    copy(hos_helpers, f'{MAIN_I}/Infrastructure/HosHelpers.cs')

log('== Infra: PAY files ==')
PAY_I = f"{ST['pay']}/Newtouch.Sett.Infrastructure"
copy(f'{PAY_I}/Domain/Entity/PayEntities.cs', f'{MAIN_I}/Domain/PayEntities.cs')
copy(f'{PAY_I}/Domain/DTO/MicroPayTradeInfoDTO.cs', f'{MAIN_I}/Domain/MicroPayTradeInfoDTO.cs')
copy(f'{PAY_I}/Infrastructure/PayEnums.cs', f'{MAIN_I}/Infrastructure/PayEnums.cs')
copy(f'{PAY_I}/Repository/PayRepositories.cs', f'{MAIN_I}/Repository/PayRepositories.cs')
pay_dm = open(f'{PAY_I}/DomainServices/PayManage/PayDmnService.cs', encoding='utf-8').read()
pay_dm = strip_classes(pay_dm, ['PayApp'])
open(f'{MAIN_I}/DomainServices/PayDmnService.cs', 'w', encoding='utf-8').write(pay_dm)
log('  wrote PAY files minus PayApp class')

log('== Infra: merged Interfaces.cs (union) ==')
merged = out_if
# usings: union of all three Interfaces.cs using-lines
us = []
for t in (out_if, hos_if, pay_if):
    for ln in t.splitlines():
        if ln.startswith('using ') and ln not in us:
            us.append(ln)
head_end = merged.find('namespace ')
merged = '\n'.join(us) + '\n\n' + merged[head_end:]
log(f'  usings union: {us}')
# colliding interfaces: append HOS members (ICqybSett05Repo handled by extends-change below)
additions = {
    'IOutPatChargeApp': ['System.Collections.Generic.List<ChargeItemDetailVO> GetYpItemInfo(string keyword)'],
    'IOutPatientDmnService': [
        'object SelectAccountingPlanList(NewtouchHIS.Lib.Base.Model.Pagination pagination, string keyword, System.DateTime? kssj, System.DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt)',
        'void overAccountingPlan(string mxId, string orgId, string rygh)'],
    'IPatientBasicInfoDmnService': [
        'System.Collections.Generic.List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid)',
        'System.DateTime? IFCQRQISJZSJ(string zyh, string orgId)'],
    'IPayApp': ['int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo, out string errorMsg)'],
    'IRefundDmnService': [
        'object GetBasicInfoSearchListInRegister(string keyword, string orgId)',
        'object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType)'],
    'ISyncTreatmentServiceRecordDmnService': ['object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null)'],
    'ISysUserDmnService': ['bool CheckStaffIsBelongDuty(string staffId, string duty)'],
    'ISysConfigRepo': [
        'SysConfigEntity GetByCode(string code, string orgId)',
        'string GetValueByCode(string code, string orgId)',
        'int GetIntValueByCode(string code, string orgId, int defaultValue = 0)',
        'bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)'],
}
for name, mems in additions.items():
    merged = add_members_to_interface(merged, name, mems)
    log(f'  {name}: +{len(mems)} members')
# ICqybSett05Repo: extend the generic base (FindEntity/Insert come from it) instead of re-declaring
merged = change_interface_decl(merged, 'ICqybSett05Repo',
                               'public interface ICqybSett05Repo : IRepositoryBase<CqybSett05Entity>')
log('  ICqybSett05Repo: now extends IRepositoryBase<CqybSett05Entity>')
# IOrderPayInfoRepo: replace empty body with PAY members
merged = replace_interface_body(merged, 'IOrderPayInfoRepo', [
    'void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null)',
    'OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo)'])
log('  IOrderPayInfoRepo: replaced empty body with PAY members')
# append HOS-only + Pay-only interfaces (full blocks, public preserved)
def iface_names(txt):
    return set(re.findall(r'interface\s+(I[A-Za-z0-9_]+)', txt))
out_names = iface_names(out_if)
for src_txt, src_name in [(hos_if, 'HOS'), (pay_if, 'PAY')]:
    for nm in sorted(iface_names(src_txt) - out_names):
        blk = block_text(src_txt, 'interface', nm)
        if blk:
            merged = merged.rstrip()
            assert merged.endswith('}')
            merged = merged[:merged.rfind('}')] + '\n    ' + blk + '\n}\n'
            log(f'  appended {src_name}-only interface {nm}')
            out_names.add(nm)
open(f'{MAIN_I}/Domain/Interfaces.cs', 'w', encoding='utf-8').write(merged)
log('  wrote merged Interfaces.cs')

log('== Infra: merged Enums.cs (HOS base + OUT clean versions) ==')
hos_en = open(f"{HOS_I}/Domain/Enums.cs", encoding='utf-8').read()
out_en = open(f'{OUT_I}/Domain/Enums.cs', encoding='utf-8').read()
for nm in ['EnumOutpatientJzbz', 'EnumPrescriptionType', 'EnumRefundStatus']:
    hos_en = replace_enum_block(hos_en, nm, block_text(out_en, 'enum', nm))
    log(f'  enum {nm}: replaced with OUT clean version')
for nm, member, want in [('EnumXTZHXZ', 'MZYJKZH', '6'), ('EnumCardType', 'XNHJYK', '5')]:
    blk = block_text(hos_en, 'enum', nm)
    m = re.search(member + r'\s*=\s*(\d+)', blk)
    got = m.group(1) if m else None
    log(f'  enum {nm}.{member} = {got} (want {want})' + ('' if got == want else '  <-- FIXING'))
    if got != want:
        hos_en = re.sub(r'(' + member + r'\s*=\s*)\d+', r'\g<1>' + want, hos_en, count=1)
open(f'{MAIN_I}/Domain/Enums.cs', 'w', encoding='utf-8').write(hos_en)
log('  wrote merged Enums.cs')

log('== Infra: OUT stub additions (HOS members, NotSupported style) ==')
stubs_path = f'{MAIN_I}/DomainServices/OutStubs.cs'
stubs = open(stubs_path, encoding='utf-8').read()
stubs = add_methods_to_class(stubs, 'OutPatChargeApp', [
    '        public System.Collections.Generic.List<ChargeItemDetailVO> GetYpItemInfo(string keyword)',
    '        {',
    '            throw new NotSupportedException("Chức năng tra cứu thuốc/mục thu phí này chưa được triển khai trong bản .NET Core.");',
    '        }'])
stubs = add_methods_to_class(stubs, 'OutPatientDmnService', [
    '        public object SelectAccountingPlanList(NewtouchHIS.Lib.Base.Model.Pagination pagination, string keyword, System.DateTime? kssj, System.DateTime? jssj, int? zxzt, string orgId, int? zsftx, int? sycstx, string sfzt)',
    '        {',
    '            throw new NotSupportedException("Chức năng truy vấn kế hoạch ghi账 ngoại trú chưa được triển khai trong bản .NET Core.");',
    '        }',
    '        public void overAccountingPlan(string mxId, string orgId, string rygh)',
    '        {',
    '            throw new NotSupportedException("Chức năng kết thúc kế hoạch ghi账 ngoại trú chưa được triển khai trong bản .NET Core.");',
    '        }'])
stubs = add_methods_to_class(stubs, 'RefundDmnService', [
    '        public object GetBasicInfoSearchListInRegister(string keyword, string orgId)',
    '        {',
    '            throw new NotSupportedException("Chức năng tìm kiếm bệnh nhân đăng ký chưa được triển khai trong bản .NET Core.");',
    '        }',
    '        public object GetBasicInfoSearchList(string keyword, string orgId, string userCode, string p4, string from, string brzybzType)',
    '        {',
    '            throw new NotSupportedException("Chức năng tìm kiếm bệnh nhân ghi账 chưa được triển khai trong bản .NET Core.");',
    '        }'])
stubs = add_methods_to_class(stubs, 'SyncTreatmentServiceRecordDmnService', [
    '        public object GetList(string organizeId, int? clzt = null, string mzh = null, string zyh = null, string zlsgh = null)',
    '        {',
    '            throw new NotSupportedException("Chức năng tra cứu bản ghi đồng bộ dịch vụ điều trị chưa được triển khai trong bản .NET Core.");',
    '        }'])
stubs = add_methods_to_class(stubs, 'SysUserDmnService', [
    '        public bool CheckStaffIsBelongDuty(string staffId, string duty)',
    '        {',
    '            return CheckStaffIsBelongDuty((object)staffId, (object)duty);',
    '        }'])
open(stubs_path, 'w', encoding='utf-8').write(stubs)
log('  wrote OUT stub additions')

log('== Infra: merged PayApp ==')
payapp = '''using Newtouch.Sett.Domain;

namespace Newtouch.Sett.DomainServices
{
    /// <summary>Merged PayApp stub: typed overload = Pay gateway stub (Failed);
    /// object overload (Outpatient signature) converts and delegates.</summary>
    public class PayApp : IPayApp
    {
        public int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo,
            out string errorMsg)
        {
            errorMsg = "Hoàn tiền qua cổng thanh toán chưa khả dụng trong bản port .NET 6 này (cổng Alipay/WeChat/NxPay chưa được chuyển đổi); không thực hiện hoàn tiền và không ghi dữ liệu.";
            return (int)EnumRefundStatus.Failed;
        }

        public int TradeRefund(object p0, object p1, object p2, object p3, out string p4)
        {
            string outTradeNo = p0?.ToString();
            decimal refundAmount = 0;
            try { refundAmount = System.Convert.ToDecimal(p1); } catch { refundAmount = 0; }
            string refundReason = p2?.ToString();
            string outRequestNo = p3?.ToString();
            return TradeRefund(outTradeNo, refundAmount, refundReason, outRequestNo, out p4);
        }
    }
}
'''
open(f'{MAIN_I}/DomainServices/MergedPayApp.cs', 'w', encoding='utf-8').write(payapp)
log('  wrote MergedPayApp.cs')

log('== Web: Program.cs union DI ==')
prog = open(f'{MAIN_W}/Program.cs', encoding='utf-8').read()
# per-staging DI lines with staging tags
staged = []
for s in ('outpatient', 'hospitalization', 'pay'):
    txt = open(f"{ST[s]}/Newtouch.Sett.Web/Program.cs", encoding='utf-8').read()
    for ln in txt.splitlines():
        t = ln.strip()
        if t.startswith('builder.Services.AddScoped<') and \
           'IAuthCenterAppService' not in t and 'IHttpClientHelper' not in t:
            staged.append((s, t))
COLLIDING = {'ISysConfigRepo', 'IPatientBasicInfoDmnService', 'ICqybSett05Repo',
             'IOrderPayInfoRepo', 'IPayApp', 'ISysUserDmnService',
             'ISyncTreatmentServiceRecordDmnService', 'IRefundDmnService',
             'IOutPatientDmnService', 'IOutPatChargeApp'}
CHOSEN_IMPL = {  # interface short name -> substring identifying the kept line
    'ISysConfigRepo': 'Newtouch.Sett.Domain.SysConfigRepo',
    'IPatientBasicInfoDmnService': 'Newtouch.Sett.Domain.PatientBasicInfoDmnService',
    'ICqybSett05Repo': 'Newtouch.Sett.Repository.CqybSett05Repo',
    'IOrderPayInfoRepo': 'OrderPayInfoRepo',  # PAY line (OUT identical line dedupes to same text)
    'IPayApp': None,  # merged line added below
    'ISysUserDmnService': 'SysUserDmnService',  # OUT line (short); HOS *Stub dropped
    'ISyncTreatmentServiceRecordDmnService': 'SyncTreatmentServiceRecordDmnService',
    'IRefundDmnService': 'RefundDmnService',
    'IOutPatientDmnService': 'OutPatientDmnService',
    'IOutPatChargeApp': 'OutPatChargeApp',
}
kept = []
for s, t in staged:
    m = re.match(r'builder\.Services\.AddScoped<([^,]+),\s*([^>]+)>\s*\(\)', t)
    if not m:
        kept.append(t)
        continue
    iface_full, impl = m.group(1).strip(), m.group(2).strip()
    iface = iface_full.split('.')[-1]
    if iface in COLLIDING:
        if iface == 'IPayApp':
            continue  # merged line below
        if s == 'outpatient' and iface in ('ISysConfigRepo', 'IPatientBasicInfoDmnService',
                                           'ICqybSett05Repo', 'IOrderPayInfoRepo'):
            continue  # HOS/PAY lines kept instead
        if s == 'hospitalization' and impl.endswith('Stub'):
            continue  # HOS stub lines for colliding interfaces dropped
        if s == 'hospitalization' and iface in ('ISysConfigRepo', 'IPatientBasicInfoDmnService',
                                                'ICqybSett05Repo'):
            pass  # keep HOS FQN real lines
        if s == 'pay' and iface == 'IOrderPayInfoRepo':
            pass  # keep PAY line
    kept.append(t)
uniq = []
for t in kept:
    if t not in uniq:
        uniq.append(t)
final_di = uniq + ['builder.Services.AddScoped<Newtouch.Sett.Domain.IPayApp, Newtouch.Sett.DomainServices.PayApp>();']
marker = 'builder.Services.AddControllersWithViews()'
assert marker in prog
prog = prog.replace(marker, '\n'.join(final_di) + '\n\n' + marker, 1)
for u in ['using Newtouch.Sett.Domain;', 'using Newtouch.Sett.DomainServices;', 'using Newtouch.Sett.Repository;']:
    if u not in prog:
        if 'using Newtouch.Sett.Infrastructure;\n' in prog:
            prog = prog.replace('using Newtouch.Sett.Infrastructure;\n',
                                'using Newtouch.Sett.Infrastructure;\n' + u + '\n', 1)
        else:
            prog = u + '\n' + prog
open(f'{MAIN_W}/Program.cs', 'w', encoding='utf-8').write(prog)
log(f'  Program.cs: {len(final_di)} DI lines')
for t in final_di:
    log(f'    {t}')

print('\nMERGE SCRIPT DONE.')
print('NEXT (manual): BottomButtonViewModel merge, dotnet build, fix errors, run 5221, verify, screenshot.')
