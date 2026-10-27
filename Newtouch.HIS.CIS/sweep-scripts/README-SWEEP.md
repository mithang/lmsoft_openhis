# CIS Web translation sweep — scripts + notes (2026-10-26)

Tiers run in order: tier1 (4,047 repl) -> tier2 (1,027) -> tier2b (119, CMP-line
message literals) -> tier3 (707) -> tier4 (153) -> tier5 (55+3, placeholder-aware
== handling) -> tier6 polish (repairs + full-width punctuation).

WARNING — tier6 incident: the PUNCT dict entry for U+3010 (LEFT BLACK LENTICULAR
BRACKET) was mangled by the file-write tool into an empty-string key `""`, and
`"".replace` in Python inserts the value between EVERY character. Tier6
corrupted 410 files that way (135,391 lines). The corruption was fully
reversed by positional de-bracketing (corrupted line = odd length, every
even-index char is "]"; original = odd-index chars) + short-line map
(empty->"]", "}"->"]}]", "{"->"]{]", tab->"]\t]", space->"] ]", ","->"],]").
Verified: 0 corrupt-pattern lines remain after repair.
Lesson: NEVER use a raw full-width bracket char as a dict key via the write
tool; use \u escapes (e.g. "\u3010") instead, and always py_compile + dry-run
count sanity before running a sweep script on the tree.
