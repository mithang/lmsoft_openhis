#!/usr/bin/env python3
"""Static file server with CORS headers for the OpenHIS static host.

QA-phase2 (SSO-02 / MRQC-02 / SETT-02 / CIS-09 / PDS-04): the plain
`python3 -m http.server` serves fonts without Access-Control-Allow-Origin,
so Firefox blocks cross-origin font loads (the static host runs on a
different port than the apps) and every icon renders as tofu. This server
is identical to http.server except it adds:

    Access-Control-Allow-Origin: *

to every response (fonts, JS, CSS, images alike). The static files are
public UI assets with no credentials, so a wildcard origin is appropriate
here; it does not change what is served, only the CORS header.

Usage:
    python3 ~/workspace/static-cors-server.py [PORT] [DIRECTORY]
Defaults: port 41142, current directory. recovery.sh passes both
explicitly. Run with cwd = the static root, or pass the directory.
"""
import sys
from http.server import SimpleHTTPRequestHandler, ThreadingHTTPServer


class CorsHandler(SimpleHTTPRequestHandler):
    def end_headers(self):
        self.send_header("Access-Control-Allow-Origin", "*")
        super().end_headers()

    def do_OPTIONS(self):
        self.send_response(204)
        self.send_header("Access-Control-Allow-Origin", "*")
        self.send_header("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS")
        self.end_headers()


def main():
    port = int(sys.argv[1]) if len(sys.argv) > 1 else 41142
    directory = sys.argv[2] if len(sys.argv) > 2 else "."
    import os
    os.chdir(directory)
    # NOTE (Docker copy): bind 0.0.0.0, not 127.0.0.1 — Docker port-mapping
    # forwards to the container IP, so a loopback-only listener inside the
    # container would never receive host traffic. The VM copy of this file
    # keeps 127.0.0.1; only this docker/ copy binds all interfaces.
    server = ThreadingHTTPServer(("0.0.0.0", port), CorsHandler)
    print(f"CORS static server on 0.0.0.0:{port} serving {directory}", flush=True)
    server.serve_forever()


if __name__ == "__main__":
    main()
