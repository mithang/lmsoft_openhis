# Static host image (Linux). Serves the public UI assets (css/js/fonts/img/
# mobile) with Access-Control-Allow-Origin: * — the plain `python3 -m
# http.server` serves fonts without the CORS header, so Firefox blocks
# cross-origin font loads and every icon renders as tofu (proven on the VM).
# The assets are copied from the run copy (the repo has no Newtouch.HIS.Static
# directory); the server script is a copy of ~/workspace/static-cors-server.py
# with the bind changed to 0.0.0.0 for the container.
FROM python:3-slim
WORKDIR /app
COPY static-cors-server.py /app/
COPY css /static/css
COPY fonts /static/fonts
COPY img /static/img
COPY js /static/js
COPY mobile /static/mobile
EXPOSE 41142
CMD ["python3", "/app/static-cors-server.py", "41142", "/static"]
