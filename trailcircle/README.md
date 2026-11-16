# TrailCircle

TrailCircle is a mobile-first social media MVP where people share outdoor activities such as hiking, walking, cycling, cricket, and camping.

## Status

The working version currently runs locally on Android through Termux. Its complete code is supplied separately in the TrailCircle v1.1 source archive. The repository integration is being prepared in this directory.

The app uses Python's standard library, JavaScript and CSS; SQLite stores registered users and posts; user-uploaded media is stored in `uploads/`.

### Privacy: never commit user data

Keep `trailcircle.db`, `uploads/` (apart from `.gitkeep`), environment secrets, sessions, and temporary files out of GitHub. This repository is **public**.

### Goal

Use GitHub for version-controlled source updates without repeatedly downloading ZIP patches. GitHub alone is *not* a live hosting service for this Python web app. During local use on Android, the Termux Python server must still run. For public multi-user use, a production backend, persistent managed storage, HTTPS, backups, and security hardening are required.

### Existing local data

When switching from the old `~/outbound-social` installation, preserve `trailcircle.db` and the complete `uploads/` directory. Never overwrite or delete them with source updates.
