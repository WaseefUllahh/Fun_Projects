#!/usr/bin/env bash
# Run TrailCircle from a Git clone and check for GitHub updates while running.
# Does not expose the app publicly; Termux needs to remain active.
set -u
cd -- "$(dirname -- "${BASH_SOURCE[0]}")" || exit 1
INTERVAL="${TRAIL_UPDATE_INTERVAL:-120}"
case "$INTERVAL" in ''|*[!0-9]*) INTERVAL=120 ;; esac
if [ "$INTERVAL" -lt 30 ]; then INTERVAL=30; fi
child=""
shutdown() {
  if [ -n "$child" ]; then kill "$child" 2>/dev/null || true; wait "$child" 2>/dev/null || true; fi
  exit 0
}
trap shutdown INT TERM
if ! command -v python >/dev/null 2>&1; then echo "Install Python first: pkg install python" >&2; exit 1; fi
while true; do
  python server.py &
  child=$!
  printf 'TrailCircle running at http://localhost:8000. Checking GitHub every %s seconds.\n' "$INTERVAL"
  while kill -0 "$child" 2>/dev/null; do
    sleep "$INTERVAL" & wait $! || true
    if ! kill -0 "$child" 2>/dev/null; then break; fi
    if ! git rev-parse --is-inside-work-tree >/dev/null 2>&1; then continue; fi
    branch="$(git branch --show-current 2>/dev/null || true)"
    if [ -z "$branch" ]; then continue; fi
    if git fetch --quiet origin "$branch" 2>/dev/null; then
      local_head="$(git rev-parse HEAD 2>/dev/null || true)"
      remote_head="$(git rev-parse "origin/$branch" 2>/dev/null || true)"
      if [ -n "$remote_head" ] && [ "$local_head" != "$remote_head" ]; then
        if git merge --ff-only "origin/$branch"; then
          echo 'GitHub update found. Restarting TrailCircle server.'
          kill "$child" 2>/dev/null || true
          wait "$child" 2>/dev/null || true
          child=""
          break
        else
          echo 'Update requires review; local data and files were not overwritten.' >&2
        fi
      fi
    fi
  done
  if [ -n "$child" ]; then wait "$child" 2>/dev/null || true; fi
  child=""
  sleep 2 & wait $! || true
done
