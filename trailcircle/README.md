# TrailCircle | Live Outside

**TrailCircle** is a mobile-first outdoor activity social web app MVP. It runs on Android using Termux and Python 3, without paid services.

It supports account registration, sign-in, outdoor posts with image uploads, likes, comments, saves, follows, profiles, search, challenges, notifications, content reports and an administrator review API. The v1.1 client compresses camera photos before uploading.

## Project structure

- `server.py`: Python standard-library HTTP API and SQLite storage.
- `static/`: web interface (HTML, CSS and JavaScript).
- `uploads/`: user-uploaded photographs (not tracked in Git).
- `trailcircle.db`: local accounts/posts database (not tracked in Git).
- `launch.sh`: local server launcher that checks GitHub for updates periodically.
- `migrate-termux.sh`: one-time import of existing local accounts and photos from an older installation.

## One-time setup on an Android phone (Termux)

Stop the old TrailCircle server with Ctrl+C, then enter:

```bash
pkg install git python -y
cd ~
git clone --depth 1 https://github.com/WaseefUllahh/Fun_Projects.git
cd ~/Fun_Projects/trailcircle
bash migrate-termux.sh
```

If `~/Fun_Projects` already exists, do **not** clone over it. Update that existing clone safely or choose a different destination. The migration script copies the `~/outbound-social/trailcircle.db` database and any files in `~/outbound-social/uploads/` without deleting the originals or overwriting a destination database.

Open **http://localhost:8000** in Chrome on the same phone.

## Regular use

After a Termux restart, start the app with:

```bash
cd ~/Fun_Projects/trailcircle
bash launch.sh
```

While the script runs, it checks the current Git branch for GitHub changes roughly every two minutes (`TRAIL_UPDATE_INTERVAL`) and fast-forwards updates when possible, restarting the server for changes. It does not overwrite local uncommitted code. For a rapid manual update, you can run `git pull --ff-only` from the clone while the app is stopped.

**GitHub is code hosting, not app hosting.** Android must continue running the Python server to access the local app. Cloud hosting, a persistent database/storage service, HTTPS and production security improvements are required before inviting public users.

## Data privacy

This repository is **public**. Do not commit `trailcircle.db`, session records, passwords, `.env` files or any photographs from `uploads/`. The `.gitignore` protects these files in normal workflows, but verify changes before every commit.

## Scope and limitations

This is a local MVP, **not a public production-ready social network**. Outdoor activity labels and challenge progress are self-reported; there is no dependable AI verification. It does not include a secure public deployment, email verification/recovery, production-grade image processing, push notifications, location safety features beyond coarse city text, comprehensive moderation, or thorough security testing.

For a future public launch, add stronger CSRF protection, rate limiting, secure media handling including EXIF removal, backups, persistent cloud storage, validated privacy controls, child safety protections, content moderation operations, HTTPS, logging, monitoring, and independent security review.
