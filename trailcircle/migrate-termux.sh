#!/usr/bin/env bash
# One-time migration from the earlier Termux installation into the Git clone.
# Never deletes or overwrites a pre-existing local TrailCircle database.
set -eu
cd -- "$(dirname -- "${BASH_SOURCE[0]}")"
OLD="$HOME/outbound-social"
if [ -d "$OLD" ]; then
  if [ -f "$OLD/trailcircle.db" ] && [ ! -f "trailcircle.db" ]; then
    cp -p "$OLD/trailcircle.db" "./trailcircle.db"
    echo "Existing accounts and posts copied from the old installation."
  elif [ -f "$OLD/trailcircle.db" ]; then
    echo "Local database already exists here, so the old one was not overwritten."
  fi
  mkdir -p uploads
  if [ -d "$OLD/uploads" ]; then
    for image in "$OLD"/uploads/*; do
      [ -f "$image" ] || continue
      dest="./uploads/${image##*/}"
      if [ ! -e "$dest" ]; then cp -p "$image" "$dest"; fi
    done
    echo "Existing photos copied without overwriting any files."
  fi
fi
echo "Starting TrailCircle with GitHub update checks."
exec bash ./launch.sh
