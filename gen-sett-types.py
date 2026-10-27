#!/usr/bin/env python3
"""Generate entity/VO/enum stub types for ONE Area into Domain/Types_<Area>.cs.
Usage: python3 gen-sett-types.py <AreaName>
- Scans ported controllers for Capitalized types not defined anywhere in the
  merged Web/Infrastructure projects, the Lib namespaces, or BCL.
- Props inferred from member usage / object initializers / assignments.
  Prop type: string if string-method/literal use, bool for true/false,
  DateTime for DateTime use, decimal for arithmetic, else object (safest).
- Enum detection: `Type.Member` in comparison/case/assignment contexts where
  Type is otherwise undefined -> enum with collected members.
- Skips service interfaces (I*) — gen-sett-services.py owns those.
- Prints the generated types; caller fixes build errors iteratively.
"""
import re, os, glob, collections, sys

AREA = sys.argv[1]
WEB = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Web"
INF = "/home/hatch/workspace/openhis-core/Newtouch.Sett.Infrastructure"
CDIR = f"{WEB}/Areas/{AREA}/Controllers"

defined = set()
for base in (INF, WEB):
    for f in glob.glob(base + "/**/*.cs", recursive=True):
        if "/obj/" in f or "/bin/" in f: continue
        if f.startswith(CDIR): continue
        # own Types file is REGENERATED: its types must stay "missing"
        if f.endswith(f"/Types_{AREA}.cs"): continue
        txt = open(f, encoding="utf-8", errors="ignore").read()
        defined |= set(re.findall(r"(?:class|interface|enum|struct)\s+(\w+)", txt))
defined |= {
    "Pagination", "AjaxResult", "ControllerBase", "OrgControllerBase", "OperatorModel",
    "HttpContext", "IActionResult", "JsonResult", "ActionResult", "FileResult",
    "ContentResult", "PartialViewResult", "ViewResult", "RedirectResult",
    "string", "int", "long", "short", "byte", "decimal", "double", "float", "bool",
    "DateTime", "DateTimeOffset", "TimeSpan", "Guid", "object", "Exception", "void",
    "List", "Dictionary", "IList", "IEnumerable", "IDictionary", "ICollection",
    "IQueryable", "HashSet", "Queue", "Stack", "String", "Int32", "Int64", "Decimal",
    "Boolean", "Char", "Byte", "Object", "Array", "Func", "Action", "Task",
    "DataTable", "DataSet", "DataRow", "DataColumn", "StringBuilder", "Regex",
    "File", "Directory", "Path", "Stream", "MemoryStream", "FileStream",
    "StreamReader", "StreamWriter", "Encoding", "CultureInfo", "Convert", "Math",
    "FileInfo", "DirectoryInfo", "Uri", "Version", "Random", "Type", "Activator",
    "HttpClient", "HttpRequestMessage", "HttpResponseMessage", "HttpMethod",
    "XDocument", "XElement", "XmlDocument", "XmlNode", "XmlElement",
    "JObject", "JArray", "JToken", "JsonConvert",
    "OperatorProvider", "UserIdentity", "FailedException", "FailedCodeException",
    "JavaScriptSerializer", "Serializer",
    "TreeGridModel", "SettKeyHelper", "SettPathHelper", "SysConfigReader",
    "BottomButtonViewModel", "EnumSelectListExtensions", "LoginBaseController",
    "HandlerLogin", "HandlerAuthorize", "LogTrackingAttribute", "FrameworkAuthorizationFilter",
    "ExceptionHandleMiddleware", "IHttpClientHelper", "HttpClientHelper",
    "IAuthCenterAppService", "AuthCenterAppService", "ILoginAppService", "LoginAppService",
    "ConfigInitHelper", "HttpWebHelper", "WebHelper", "IFormFile", "FormFile",
    "SelectList", "SelectListItem", "Mvc", "Controller", "AreaAttribute",
    "HttpGet", "HttpPost", "HttpPut", "HttpDelete", "HttpPatch", "Route", "NonAction",
    "ActionName", "AcceptVerbs", "FromBody", "FromQuery", "FromRoute", "FromForm",
    "AllowAnonymous", "Authorize", "ValidateAntiForgeryToken", "Bind", "ModelState",
    "ViewBag", "ViewData", "TempData", "Url", "Request", "Response", "Session",
    "CookieOptions", "IWebHostEnvironment", "IConfiguration", "ILogger",
    "NotSupportedException", "NotImplementedException", "ArgumentException",
    "ArgumentNullException", "InvalidOperationException", "FormatException",
    "KeyNotFoundException", "NullReferenceException", "IndexOutOfRangeException",
    "SqlSugarClient", "SqlSugar", "SugarParameter", "Queryable", "ISugarQueryable",
    "Redis", "IDatabase", "ConnectionMultiplexer", "StackExchange",
    "DataAnnotations", "Required", "StringLength", "Display", "Key",
    "DBNull", "Console", "Environment", "Thread", "Task", "CancellationToken",
    "IPrincipal", "ClaimsIdentity", "ClaimsPrincipal", "GenericIdentity",
    "User", "Url", "Model", "RouteData", "ActionContext", "ViewContext",
    "ControllerContext", "ModelStateDictionary", "ViewDataDictionary",
    "RouteValueDictionary", "ActionDescriptor", "ITempDataDictionary",
    "NumberStyles", "DateTimeStyles", "MidpointRounding", "StringComparison",
    "StringSplitOptions", "RegexOptions", "FileMode", "FileAccess", "SeekOrigin",
    "DayOfWeek", "DateTimeKind", "Guid", "Nullable", "Lazy", "WeakReference",
    "Tuple", "ValueTuple", "KeyValuePair", "SortedDictionary", "SortedList",
    "ConcurrentDictionary", "ConcurrentBag", "BlockingCollection",
}
STATIC_CLASSES = {"DateTime", "Guid", "Math", "Convert", "Path", "File", "Directory",
                  "Regex", "Encoding", "JsonConvert", "Enum", "String", "Decimal",
                  "Int32", "Int64", "Double", "Boolean", "Console", "Environment",
                  "StringBuilder", "Activator", "Type", "DBNull", "TimeSpan"}

texts = {}
for f in sorted(glob.glob(CDIR + "/*.cs")):
    texts[f] = open(f, encoding="utf-8").read()

cands = set()
for txt in texts.values():
    for m in re.finditer(r"public\s+(?:IActionResult|string|void|Task<[^>]+>)\s+\w+\(([^)]*)\)", txt):
        for p in m.group(1).split(","):
            p = p.strip()
            p = re.sub(r"=[^,]+$", "", p).strip()
            parts = p.split()
            if len(parts) >= 2:
                t = parts[-2]
                t = re.sub(r"^(?:List|IList|IEnumerable)<(.+)>$", r"\\1", t).rstrip("[]?")
                if re.match(r"^[A-Za-z]\w*$", t): cands.add(t)
    for m in re.finditer(r"new\s+([A-Z]\w*)\s*[\(\{]", txt): cands.add(m.group(1))
    for m in re.finditer(r"new\s+([a-z]\w*)\s*[\(\{]", txt): cands.add(m.group(1))
    # lowercase-starting domain VOs (jgssEarningVO, ybqssq, submitTimeVO):
    # only via explicit generic containers/methods, never bare <...> (less-than)
    for m in re.finditer(r"(?:DeserializeObject|ToList|List|IList|IEnumerable)<([A-Za-z]\w*)", txt): cands.add(m.group(1))
    for m in re.finditer(r"\(([A-Z]\w*)\)\s*[\w\"\'\(]", txt): cands.add(m.group(1))
    for m in re.finditer(r"<([A-Z]\w*)>", txt): cands.add(m.group(1))
    for m in re.finditer(r"\bout\s+([A-Z]\w*)\s+\w+", txt): cands.add(m.group(1))
    for m in re.finditer(r"\btypeof\(([A-Z]\w*)\)", txt): cands.add(m.group(1))
    for m in re.finditer(r"\bis\s+([A-Z]\w*)", txt): cands.add(m.group(1))
    for m in re.finditer(r"\bas\s+([A-Z]\w*)", txt): cands.add(m.group(1))
    for m in re.finditer(r"\b([A-Z]\w*)\s+\w+\s*(?:=|;)", txt):
        if m.group(1) not in ("return",): cands.add(m.group(1))

missing = sorted(c for c in cands if c not in defined and not c.startswith("I"))
print("missing types:", missing)

props = collections.defaultdict(set)
prop_kind = collections.defaultdict(dict)
methods = collections.defaultdict(set)
stmt_methods = collections.defaultdict(set)
null_props = collections.defaultdict(set)
STRING_METHODS = {"Length", "Contains", "Trim", "StartsWith", "EndsWith", "Substring",
                  "ToUpper", "ToLower", "Replace", "Split", "PadLeft", "PadRight",
                  "IndexOf", "LastIndexOf", "Remove", "Insert", "Equals"}
METHOD_START = re.compile(
    r"(?m)^\s*(?:public|private|protected|internal)\s+(?:static\s+)?"
    r"(?:[\w<>\[\],\?\s]+?)\s+\w+\s*\(")


def method_regions(txt):
    starts = [m.start() for m in METHOD_START.finditer(txt)]
    if not starts:
        return [txt]
    regions = []
    for i, s in enumerate(starts):
        regions.append(txt[s:starts[i + 1] if i + 1 < len(starts) else len(txt)])
    return regions


for f, txt in texts.items():
    for m in re.finditer(r"new\s+([A-Z]\w*)\s*(?:\(\))?\s*\{([^}]*)\}", txt):
        t = m.group(1)
        for pm in re.finditer(r"(\w+)\s*=\s*([^,}\n]+)", m.group(2)):
            props[t].add(pm.group(1))
            val = pm.group(2).strip()
            if val.startswith('"'): prop_kind[t][pm.group(1)] = "string"
            elif val in ("true", "false"): prop_kind[t][pm.group(1)] = "bool"
            elif val.startswith("DateTime"): prop_kind[t][pm.group(1)] = "DateTime"
    # PER-METHOD var scoping: the same var name (e.g. `item`) in different
    # methods may have different types; never let one method's uses leak
    # props into another method's type.
    for region in method_regions(txt):
        var_type = {}
        for m in re.finditer(r"public\s+(?:IActionResult|string|void|Task<[^>]+>)\s+\w+\(([^)]*)\)", region):
            for p in m.group(1).split(","):
                p = p.strip(); p = re.sub(r"=[^,]+$", "", p).strip()
                parts = p.split()
                if len(parts) >= 2:
                    orig = parts[-2]
                    t = orig.rstrip("[]?")
                    t = re.sub(r"^(?:List|IList|IEnumerable)<(.+)>$", r"\\1", t)
                    # List<T> params: foreach element type must resolve (else
                    # item.Prop assignments never attribute props to T)
                    lm = re.match(r"^(?:List|IList|IEnumerable)<(.+)>$", orig.rstrip("[]?"))
                    var_type[parts[-1]] = ("LIST:" + lm.group(1)) if lm else t
        for m in re.finditer(r"\b([A-Z]\w*)\s+(\w+)\s*(?:=|;)", region):
            var_type.setdefault(m.group(2), m.group(1))
        for m in re.finditer(r"\bout\s+([A-Z]\w*)\s+(\w+)", region):
            var_type[m.group(2)] = m.group(1)
        # var x = ToList<T>/DeserializeObject<List<T>>/DeserializeObject<T>
        for m in re.finditer(r"\bvar\s+(\w+)\s*=\s*([^;]+);", region):
            gm = re.search(r"(?:ToList|DeserializeObject)<(?:List<)?([A-Za-z]\w*)>?", m.group(2))
            if gm:
                var_type[m.group(1)] = "LIST:" + gm.group(1)
            else:
                # var x = new T() / new T { ... } (incl. service-call-free DTOs)
                nm = re.search(r"new\s+([A-Za-z]\w*)\s*(?:\(\))?\s*[\(\{]", m.group(2))
                if nm:
                    var_type[m.group(1)] = nm.group(1)
        for m in re.finditer(r"foreach\s*\(\s*var\s+(\w+)\s+in\s+(\w+)\s*\)", region):
            src = var_type.get(m.group(2))
            if src and src.startswith("LIST:"):
                var_type[m.group(1)] = src[5:]
        for var, t in list(var_type.items()):
            if t not in missing: continue
            # props: member access NOT followed by a call paren (calls are
            # methods). \b after the group stops backtracking (else
            # `entity.Create()` would yield a bogus `Creat` prop).
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\b(?!\s*\()", region):
                props[t].add(pm.group(1))
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*\(", region):
                methods[t].add(pm.group(1))
            for pm in re.finditer(r"(?m)^\s*" + re.escape(var) + r"\.(\w+)\s*\([^;]*;\s*$", region):
                stmt_methods[t].add(pm.group(1))
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*=\s*null\b", region):
                null_props[t].add(pm.group(1))
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\.Value\b", region):
                props[t].add(pm.group(1))
                if prop_kind[t].get(pm.group(1)) == "DateTime":
                    prop_kind[t][pm.group(1)] = "DateTime?"
                else:
                    prop_kind[t].setdefault(pm.group(1), "decimal?")
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*\.\s*(\w+)\s*\(", region):
                if pm.group(2) in STRING_METHODS: prop_kind[t][pm.group(1)] = "string"
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*(?:\+|-|\*|/|%|>|<|>=|<=)", region):
                prop_kind[t].setdefault(pm.group(1), "decimal")
            for pm in re.finditer(r"\b" + re.escape(var) + r"\.(\w+)\s*=\s*(\"[^\"]*\"|true|false|DateTime\.[^;\n]+)", region):
                v = pm.group(2)
                if v.startswith('"'): prop_kind[t][pm.group(1)] = "string"
                elif v in ("true", "false"): prop_kind[t][pm.group(1)] = "bool"
                elif v.startswith("DateTime"): prop_kind[t][pm.group(1)] = "DateTime"

# enum detection: Type.Member where Member is NOT followed by a call paren or
# another dot (dotted chains like Newtouch.Sett.Web are namespaces, not enums);
# using-lines are excluded outright (they look like Namespace.Member chains).
# Method parameter names are excluded too: Framework controllers often name a
# parameter after its entity type (SubmitForm(FinanceReceiptEntity Entity,...)),
# and Entity.zt then looks exactly like an enum access (Batch D: bogus enum
# Entity shadowed the Newtouch.Sett.Domain.Entity namespace -> 21 errors).
param_names = set()
for txt in texts.values():
    for dm in re.finditer(r"\b(?:public|private|protected|internal)\s+(?:[\w\.]+(?:<[^>]+>)?(?:\[\])?)\s+\w+\s*\(([^)]*)\)", txt):
        for seg in dm.group(1).split(","):
            seg = seg.strip()
            seg = re.sub(r"\s*=\s*.*$", "", seg)
            toks = seg.split()
            if len(toks) >= 2:
                param_names.add(toks[-1])
enums = collections.defaultdict(set)
for txt in texts.values():
    code = "\n".join(ln for ln in txt.split("\n") if not re.match(r"\s*using\s+", ln))
    for m in re.finditer(r"(?<![\w\.])()([A-Z]\w*)\.(\w+)(?![\w\.\(])", code):
        t, mem = m.group(2), m.group(3)
        if t in defined or t in STATIC_CLASSES or t in param_names:
            continue
        enums[t].add(mem)
enums = {t: ms for t, ms in enums.items() if t not in defined and t not in STATIC_CLASSES}
# only keep enum candidates that are NOT missing class types with props
for t in list(enums):
    if t in props and props[t]:
        del enums[t]
print("enum candidates:", {k: sorted(v) for k, v in enums.items()})

out_path = f"{INF}/Domain/Types_{AREA}.cs"
n_classes = len([t for t in missing if t not in enums])
if n_classes == 0 and not enums:
    if os.path.exists(out_path):
        os.remove(out_path)
    print("no missing types; no Types file written")
    sys.exit(0)
lines = ["using System;\nusing System.Collections.Generic;\n\nnamespace Newtouch.Sett.Domain\n{"]
NULLABLE = {"DateTime": "DateTime?", "decimal": "decimal?", "bool": "bool?",
            "int": "int?", "long": "long?", "double": "double?", "float": "float?"}
for t in missing:
    if t in enums: continue
    lines.append(f"    public class {t}\n    {{")
    for p in sorted(props.get(t, set())):
        kind = prop_kind[t].get(p, "object")
        if p in null_props.get(t, set()):
            kind = NULLABLE.get(kind, kind)
        lines.append(f"        public {kind} {p} {{ get; set; }}")
    for m in sorted(methods.get(t, set())):
        if m in props.get(t, set()):
            continue
        if m in stmt_methods.get(t, set()):
            lines.append(f"        public void {m}() {{ }}")
        else:
            lines.append(f"        public object {m}() {{ return null; }}")
    lines.append("    }\n")
for t, ms in sorted(enums.items()):
    lines.append(f"    public enum {t}\n    {{")
    for i, mem in enumerate(sorted(ms)):
        lines.append(f"        {mem} = {i},")
    lines.append("    }\n")
lines.append("}")
os.makedirs(os.path.dirname(out_path), exist_ok=True)
open(out_path, "w", encoding="utf-8").write("\n".join(lines))
print(f"wrote {out_path}: classes={len([t for t in missing if t not in enums])} enums={len(enums)}")
