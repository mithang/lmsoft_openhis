#!/usr/bin/env python3
"""DCSoft stub pass for EMR controllers.

Replaces DCSoft engine configuration + writer service actions with explicit stubs:
- GetControlEngine() bodies -> `return new WebWriterControlEngine();`
  (keeps every call site compiling; the stub renders an explicit notice area)
- HandleDCWriterServicePage / HandelDCWriterServicePage / MoreHandleDCWriterServicePage
  + their nested ActionResult classes -> explicit error-JSON actions (never 404)
- [ValidateInput(false)] attributes removed (no such attribute in ASP.NET Core)
- Request.HttpMethod -> Request.Method
"""
import re
import sys

WEB = "/home/hatch/workspace/openhis-core/Newtouch.HIS.EMR/Newtouch.EMR.Web"

STUB_ACTION = '''        // DCSoft.Writer has no .NET Core build: this service endpoint reports the
        // limitation explicitly instead of 404. A replacement web editor is the
        // remaining EMR item.
        public IActionResult {name}()
        {{
            return Content(new AjaxResult
            {{
                state = ResultType.error.ToString(),
                message = "Trình soạn thảo văn bản DCSoft chưa khả dụng trên .NET Core (không có bản build .NET Core). Cần thay bằng trình soạn thảo web khác."
            }}.ToJson());
        }}
'''

def find_block_end(lines, start):
    """Return index just past the closing brace of the block starting at `start`
    (line containing the opening brace, or the next line with one). Handles
    strings, char literals, // and /* */ comments."""
    i = start
    # find opening brace
    while i < len(lines):
        if "{" in lines[i]:
            break
        i += 1
    if i >= len(lines):
        raise ValueError("no opening brace from line %d" % start)
    depth = 0
    in_str = False
    in_char = False
    esc = False
    in_block_comment = False
    j = i
    while j < len(lines):
        line = lines[j]
        k = 0
        while k < len(line):
            ch = line[k]
            nxt = line[k + 1] if k + 1 < len(line) else ""
            if in_block_comment:
                if ch == "*" and nxt == "/":
                    in_block_comment = False
                    k += 2
                    continue
                k += 1
                continue
            if in_str:
                if esc:
                    esc = False
                elif ch == "\\":
                    esc = True
                elif ch == '"':
                    in_str = False
                k += 1
                continue
            if in_char:
                if esc:
                    esc = False
                elif ch == "\\":
                    esc = True
                elif ch == "'":
                    in_char = False
                k += 1
                continue
            if ch == "/" and nxt == "/":
                break
            if ch == "/" and nxt == "*":
                in_block_comment = True
                k += 2
                continue
            if ch == '"':
                in_str = True
                k += 1
                continue
            if ch == "'":
                in_char = True
                k += 1
                continue
            if ch == "{":
                depth += 1
            elif ch == "}":
                depth -= 1
                if depth == 0:
                    return j + 1
            k += 1
        j += 1
    raise ValueError("unbalanced block from line %d" % start)

def replace_method(lines, sig_re, replacement):
    """Replace the first method/class whose signature line matches sig_re."""
    for i, line in enumerate(lines):
        if sig_re.search(line):
            end = find_block_end(lines, i)
            rep = replacement if isinstance(replacement, list) else [replacement]
            return lines[:i] + rep + lines[end:], (i, end)
    return lines, None

def process(path, actions):
    with open(path, encoding="utf-8") as f:
        text = f.read()
    lines = text.split("\n")
    # 1. Remove [ValidateInput(false)]
    lines = [l for l in lines if "[ValidateInput(false)]" not in l]
    # 2. Request.HttpMethod -> Request.Method
    lines = [l.replace("this.Request.HttpMethod", "Request.Method").replace("Request.HttpMethod", "Request.Method") for l in lines]
    # 3. GetControlEngine body -> stub return
    lines, span = replace_method(
        lines, re.compile(r"private\s+WebWriterControlEngine\s+GetControlEngine\s*\("),
        "        private WebWriterControlEngine GetControlEngine()\n        {\n            return new WebWriterControlEngine();\n        }\n")
    print(path, "GetControlEngine:", span)
    # 4. Service actions + nested classes
    for action_name, nested_class in actions:
        # nested class first (it may sit after other actions)
        if nested_class:
            lines, span = replace_method(
                lines, re.compile(r"private\s+class\s+" + nested_class + r"\b"), [])
            print(path, nested_class, "removed:", span)
        # action -> stub
        lines, span = replace_method(
            lines, re.compile(r"public\s+IActionResult\s+" + action_name + r"\s*\("),
            STUB_ACTION.format(name=action_name))
        print(path, action_name, "stubbed:", span)
    # 5. Ensure AjaxResult/ToJson usings
    joined = "\n".join(lines)
    if "using NewtouchHIS.Lib.Base.Model;" not in joined:
        lines.insert(0, "using NewtouchHIS.Lib.Base.Model;")
    if "using NewtouchHIS.Lib.Base.Extension;" not in joined:
        lines.insert(0, "using NewtouchHIS.Lib.Base.Extension;")
    with open(path, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))

process(WEB + "/Areas/MedicalRecordManage/Controllers/MedicalRecordController.cs",
        [("HandleDCWriterServicePage", "DCWriterActionResult")])
process(WEB + "/Areas/MedicalRecordManage/Controllers/MedRecordTemplateController.cs",
        [("HandelDCWriterServicePage", "DCWriterActionResult"),
         ("MoreHandleDCWriterServicePage", "MoreActionResult")])
process(WEB + "/Areas/SystemManage/Controllers/YzPreViewController.cs",
        [("MoreHandleDCWriterServicePage", "MoreActionResult")])
print("done")
