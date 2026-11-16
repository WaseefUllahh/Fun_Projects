"""TrailCircle: dependency-free outdoor social MVP (Python 3.10+)."""
import os, re, json, sqlite3, secrets, hashlib, hmac, mimetypes, time, datetime, urllib.parse, threading
from http.server import ThreadingHTTPServer, BaseHTTPRequestHandler
from http.cookies import SimpleCookie
from pathlib import Path
ROOT = Path(__file__).resolve().parent
DB = Path(os.environ.get('TRAIL_DB', ROOT/'trailcircle.db'))
UPLOADS=ROOT/'uploads'; UPLOADS.mkdir(exist_ok=True)
PORT=int(os.environ.get('PORT', '8000'))
LOCK=threading.RLock()
CATEGORIES=['Hiking','Walking','Running','Cycling','Cricket','Football','Camping','Nature','Volunteering','Travel']

def db():
 c=sqlite3.connect(DB,timeout=15);c.row_factory=sqlite3.Row;c.execute('PRAGMA foreign_keys=ON');return c

def init():
 with LOCK,db() as c:
  c.executescript('''CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY, username TEXT UNIQUE NOT NULL, display_name TEXT NOT NULL, password_hash TEXT NOT NULL, salt TEXT NOT NULL, bio TEXT DEFAULT '', city TEXT DEFAULT '', private INTEGER DEFAULT 0, created INTEGER NOT NULL);
  CREATE TABLE IF NOT EXISTS sessions(token_hash TEXT PRIMARY KEY,user_id INTEGER NOT NULL,expires INTEGER NOT NULL,FOREIGN KEY(user_id) REFERENCES users(id));
  CREATE TABLE IF NOT EXISTS posts(id INTEGER PRIMARY KEY,user_id INTEGER NOT NULL,caption TEXT NOT NULL, category TEXT NOT NULL, city TEXT DEFAULT '', image TEXT DEFAULT '', created INTEGER NOT NULL, status TEXT DEFAULT 'published',FOREIGN KEY(user_id) REFERENCES users(id));
  CREATE TABLE IF NOT EXISTS likes(user_id INTEGER,post_id INTEGER,PRIMARY KEY(user_id,post_id),FOREIGN KEY(user_id) REFERENCES users(id),FOREIGN KEY(post_id) REFERENCES posts(id));
  CREATE TABLE IF NOT EXISTS saves(user_id INTEGER,post_id INTEGER,PRIMARY KEY(user_id,post_id),FOREIGN KEY(user_id) REFERENCES users(id),FOREIGN KEY(post_id) REFERENCES posts(id));
  CREATE TABLE IF NOT EXISTS follows(follower INTEGER,followed INTEGER,PRIMARY KEY(follower,followed));
  CREATE TABLE IF NOT EXISTS comments(id INTEGER PRIMARY KEY,user_id INTEGER NOT NULL,post_id INTEGER NOT NULL,body TEXT NOT NULL,created INTEGER NOT NULL,FOREIGN KEY(user_id) REFERENCES users(id),FOREIGN KEY(post_id) REFERENCES posts(id));
  CREATE TABLE IF NOT EXISTS challenges(id INTEGER PRIMARY KEY,title TEXT,description TEXT,category TEXT,target INTEGER,unit TEXT,ends TEXT);
  CREATE TABLE IF NOT EXISTS entries(user_id INTEGER,challenge_id INTEGER,progress INTEGER DEFAULT 0,PRIMARY KEY(user_id,challenge_id));
  CREATE TABLE IF NOT EXISTS reports(id INTEGER PRIMARY KEY,reporter_id INTEGER,post_id INTEGER,reason TEXT,created INTEGER,status TEXT DEFAULT 'pending');
  CREATE TABLE IF NOT EXISTS blocks(blocker INTEGER,blocked INTEGER,PRIMARY KEY(blocker,blocked));
  CREATE INDEX IF NOT EXISTS ix_posts_created ON posts(created DESC);''')
  if not c.execute('SELECT 1 FROM challenges LIMIT 1').fetchone():
   c.executemany('INSERT INTO challenges(title,description,category,target,unit,ends) VALUES (?,?,?,?,?,?)',[
    ('7-day outdoor streak','Log seven outdoor sessions this week.','Walking',7,'sessions','2027-01-01'),
    ('Weekend trail explorer','Complete three hiking sessions.','Hiking',3,'hikes','2027-01-01'),
    ('Community clean-up','Join two outdoor clean-up activities.','Volunteering',2,'sessions','2027-01-01')])

def password_hash(p,s):return hashlib.pbkdf2_hmac('sha256',p.encode(),bytes.fromhex(s),260000).hex()
def now():return int(time.time())
def rowdict(r):return dict(r) if r else None

class Handler(BaseHTTPRequestHandler):
 server_version='TrailCircle/1.0'
 def log_message(self, fmt,*args):pass
 def reply(self,status=200,payload=None,headers=None):
  data=json.dumps(payload if payload is not None else {},ensure_ascii=False).encode()
  self.send_response(status);self.send_header('Content-Type','application/json; charset=utf-8');self.send_header('Content-Length',str(len(data)));self.send_header('Cache-Control','no-store');self.send_header('X-Content-Type-Options','nosniff');self.send_header('X-Frame-Options','DENY');self.send_header('Referrer-Policy','strict-origin-when-cross-origin');self.send_header('Content-Security-Policy',"default-src 'self'; img-src 'self' data:; style-src 'self'; script-src 'self'; connect-src 'self'; object-src 'none'")
  for k,v in (headers or {}).items():self.send_header(k,v)
  self.end_headers();self.wfile.write(data)
 def error(self,msg,status=400):self.reply(status,{'error':msg})
 def body(self):
  n=int(self.headers.get('Content-Length',0));
  if n>8_000_000:raise ValueError('Request too large')
  return json.loads(self.rfile.read(n) or b'{}')
 def user(self,c):
  cookie=SimpleCookie();
  try:cookie.load(self.headers.get('Cookie',''))
  except Exception:return None
  item=cookie.get('session')
  if not item:return None
  tokenhash=hashlib.sha256(item.value.encode()).hexdigest()
  return c.execute('SELECT users.* FROM sessions JOIN users ON users.id=sessions.user_id WHERE token_hash=? AND expires>?',(tokenhash,now())).fetchone()
 def auth(self,c):
  u=self.user(c)
  if not u:raise PermissionError('Sign in to continue')
  return u
 def sendfile(self,path,ctype):
  try:data=path.read_bytes()
  except OSError:return self.error('Not found',404)
  self.send_response(200);self.send_header('Content-Type',ctype);self.send_header('Content-Length',str(len(data)));self.send_header('X-Content-Type-Options','nosniff');self.send_header('Cache-Control','no-store' if path.suffix in ('.html','.js','.css','.json') else 'public, max-age=3600');self.end_headers();self.wfile.write(data)
 def do_GET(self):
  p=urllib.parse.urlsplit(self.path);q=urllib.parse.parse_qs(p.query);path=p.path
  if not path.startswith('/api/'):
   if path.startswith('/uploads/'):
    name=path[len('/uploads/'):]
    if re.fullmatch(r'[a-f0-9]{32}\.(jpg|png|webp)',name):return self.sendfile(UPLOADS/name,{'jpg':'image/jpeg','png':'image/png','webp':'image/webp'}[name.split('.')[-1]])
    return self.error('Not found',404)
   f=ROOT/'static'/('index.html' if path=='/' else path.lstrip('/'))
   if f.is_file() and f.resolve().is_relative_to((ROOT/'static').resolve()):return self.sendfile(f,mimetypes.guess_type(f)[0] or 'application/octet-stream')
   return self.sendfile(ROOT/'static'/'index.html','text/html')
  try:
   with LOCK,db() as c:
    u=self.user(c);uid=u['id'] if u else -1
    if path=='/api/me':return self.reply(payload={'user':self.public_user(u) if u else None,'categories':CATEGORIES})
    if path=='/api/feed':
     cat=q.get('category',[''])[0];search=q.get('q',[''])[0].strip()[:80];mode=q.get('mode',['all'])[0];page=min(max(int(q.get('page',['0'])[0]),0),1000)
     where=['p.status="published"','(u.private=0 OR u.id=?)','NOT EXISTS (SELECT 1 FROM blocks b WHERE (b.blocker=? AND b.blocked=u.id) OR (b.blocker=u.id AND b.blocked=?))'];args=[uid,uid,uid]
     if cat in CATEGORIES:where.append('p.category=?');args.append(cat)
     if search:where.append('(p.caption LIKE ? OR p.city LIKE ? OR u.username LIKE ?)');args += ['%'+search+'%']*3
     if mode=='following':where.append('EXISTS(SELECT 1 FROM follows f WHERE f.follower=? AND f.followed=p.user_id)');args.append(uid)
     if mode=='saved':where.append('EXISTS(SELECT 1 FROM saves s WHERE s.user_id=? AND s.post_id=p.id)');args.append(uid)
     if mode=='mine':where.append('p.user_id=?');args.append(uid)
     sql='SELECT p.*,u.username,u.display_name,(SELECT COUNT(*) FROM likes WHERE post_id=p.id) likes,(SELECT COUNT(*) FROM comments WHERE post_id=p.id) comments,EXISTS(SELECT 1 FROM likes WHERE user_id=? AND post_id=p.id) liked,EXISTS(SELECT 1 FROM saves WHERE user_id=? AND post_id=p.id) saved FROM posts p JOIN users u ON u.id=p.user_id WHERE '+ ' AND '.join(where)+' ORDER BY p.created DESC,p.id DESC LIMIT 25 OFFSET ?'
     rows=c.execute(sql,[uid,uid]+args+[page*25]).fetchall();return self.reply(payload={'posts':[dict(x) for x in rows],'page':page})
    if path=='/api/challenges':
     rows=c.execute('SELECT c.*,e.progress, (SELECT COUNT(*) FROM entries WHERE challenge_id=c.id) participants FROM challenges c LEFT JOIN entries e ON e.challenge_id=c.id AND e.user_id=? ORDER BY c.id',(uid,)).fetchall();return self.reply(payload={'challenges':[dict(x) for x in rows]})
    if path=='/api/profile':
     username=q.get('username',[''])[0];target=c.execute('SELECT * FROM users WHERE username=?',(username,)).fetchone()
     if not target:return self.error('User not found',404)
     tid=target['id'];can=not target['private'] or tid==uid
     posts=c.execute('SELECT COUNT(*) FROM posts WHERE user_id=? AND status="published"',(tid,)).fetchone()[0] if can else None
     return self.reply(payload={'user':self.public_user(target),'posts':posts,'followers':c.execute('SELECT COUNT(*) FROM follows WHERE followed=?',(tid,)).fetchone()[0],'following':c.execute('SELECT COUNT(*) FROM follows WHERE follower=?',(tid,)).fetchone()[0],'is_following':bool(c.execute('SELECT 1 FROM follows WHERE follower=? AND followed=?',(uid,tid)).fetchone()),'is_me':uid==tid,'can_view':can})
    if path=='/api/comments':
     pid=int(q.get('post',['0'])[0]);pr=c.execute('SELECT p.*,u.private FROM posts p JOIN users u ON u.id=p.user_id WHERE p.id=?',(pid,)).fetchone()
     if not pr or (pr['private'] and pr['user_id']!=uid):return self.error('Post unavailable',404)
     rows=c.execute('SELECT cm.id,cm.body,cm.created,u.username,u.display_name FROM comments cm JOIN users u ON u.id=cm.user_id WHERE cm.post_id=? ORDER BY cm.created DESC LIMIT 100',(pid,)).fetchall();return self.reply(payload={'comments':[dict(x) for x in rows]})
    if path=='/api/notifications':
     self.auth(c);rows=c.execute('SELECT "like" type,l.user_id actor_id,u.username,p.id post_id,p.caption,0 created FROM likes l JOIN posts p ON p.id=l.post_id JOIN users u ON u.id=l.user_id WHERE p.user_id=? AND l.user_id<>? UNION ALL SELECT "comment",cm.user_id,u.username,p.id,p.caption,cm.created FROM comments cm JOIN posts p ON p.id=cm.post_id JOIN users u ON u.id=cm.user_id WHERE p.user_id=? AND cm.user_id<>? ORDER BY created DESC LIMIT 30',(uid,uid,uid,uid)).fetchall();return self.reply(payload={'notifications':[dict(x) for x in rows]})
    if path=='/api/admin/reports':
     self.auth(c)
     if u['username']!=os.environ.get('TRAIL_ADMIN',''):return self.error('Forbidden',403)
     rows=c.execute('SELECT r.*,u.username reporter,p.caption FROM reports r JOIN users u ON u.id=r.reporter_id LEFT JOIN posts p ON p.id=r.post_id ORDER BY r.created DESC').fetchall();return self.reply(payload={'reports':[dict(x) for x in rows]})
    return self.error('Not found',404)
  except (ValueError,TypeError) as e:return self.error(str(e))
  except PermissionError as e:return self.error(str(e),401)
  except Exception as e:print('GET ERROR',e);return self.error('Server error',500)
 def public_user(self,u):
  return {'id':u['id'],'username':u['username'],'display_name':u['display_name'],'bio':u['bio'],'city':u['city'],'private':bool(u['private'])} if u else None
 def do_POST(self):
  path=urllib.parse.urlsplit(self.path).path
  if not path.startswith('/api/'):return self.error('Not found',404)
  try:
   data=self.body();
   with LOCK,db() as c:
    if path=='/api/register':
     username=str(data.get('username','')).lower().strip();display=str(data.get('display_name','')).strip()[:50];pw=str(data.get('password',''))
     if not re.fullmatch(r'[a-z0-9_]{3,20}',username) or len(pw)<10 or len(pw)>128 or not display:return self.error('Username: 3–20 letters, numbers or underscore; password: 10+ characters; display name required')
     salt=secrets.token_hex(16)
     try:cur=c.execute('INSERT INTO users(username,display_name,password_hash,salt,created) VALUES (?,?,?,?,?)',(username,display,password_hash(pw,salt),salt,now()))
     except sqlite3.IntegrityError:return self.error('Username already exists')
     return self.issue(c,cur.lastrowid)
    if path=='/api/login':
     username=str(data.get('username','')).lower().strip();pw=str(data.get('password',''));u=c.execute('SELECT * FROM users WHERE username=?',(username,)).fetchone()
     if not u or not hmac.compare_digest(password_hash(pw,u['salt']),u['password_hash']):return self.error('Invalid username or password',401)
     return self.issue(c,u['id'])
    u=self.auth(c);uid=u['id']
    if path=='/api/logout':
     cookie=SimpleCookie();cookie.load(self.headers.get('Cookie',''));token=cookie.get('session')
     if token:c.execute('DELETE FROM sessions WHERE token_hash=?',(hashlib.sha256(token.value.encode()).hexdigest(),))
     return self.reply(payload={'ok':True},headers={'Set-Cookie':'session=; HttpOnly; SameSite=Lax; Path=/; Max-Age=0'})
    if path=='/api/post':
     caption=str(data.get('caption','')).strip();cat=str(data.get('category',''));city=str(data.get('city','')).strip()[:80];image=str(data.get('image',''))
     if not 3<=len(caption)<=1200 or cat not in CATEGORIES:return self.error('Caption must be 3–1200 characters and category is required')
     if image and not re.fullmatch(r'/uploads/[a-f0-9]{32}\.(jpg|png|webp)',image):return self.error('Invalid image')
     cur=c.execute('INSERT INTO posts(user_id,caption,category,city,image,created) VALUES (?,?,?,?,?,?)',(uid,caption,cat,city,image,now()));return self.reply(payload={'id':cur.lastrowid,'ok':True})
    if path=='/api/upload':
     raw=str(data.get('data',''));match=re.fullmatch(r'data:image/(jpeg|png|webp);base64,([A-Za-z0-9+/=]+)',raw)
     if not match:return self.error('Upload JPEG, PNG, or WebP')
     import base64
     try:b=base64.b64decode(match[2],validate=True)
     except Exception:return self.error('Invalid image encoding')
     if len(b)>4_000_000:return self.error('Image must be 4 MB or smaller')
     tp=match[1]
     valid=(tp=='jpeg' and b.startswith(b'\xff\xd8\xff')) or (tp=='png' and b.startswith(b'\x89PNG\r\n\x1a\n')) or (tp=='webp' and len(b)>12 and b[:4]==b'RIFF' and b[8:12]==b'WEBP')
     if not valid:return self.error('Image format does not match its contents')
     ext={'jpeg':'jpg','png':'png','webp':'webp'}[tp];name=secrets.token_hex(16)+'.'+ext;(UPLOADS/name).write_bytes(b)
     return self.reply(payload={'url':'/uploads/'+name})
    if path in ['/api/like','/api/save']:
     pid=int(data.get('post_id',0));t='likes' if path=='/api/like' else 'saves';p=c.execute('SELECT p.id,p.user_id,u.private FROM posts p JOIN users u ON u.id=p.user_id WHERE p.id=? AND p.status="published"',(pid,)).fetchone()
     if not p or (p['private'] and p['user_id']!=uid):return self.error('Post unavailable',404)
     old=c.execute('SELECT 1 FROM '+t+' WHERE user_id=? AND post_id=?',(uid,pid)).fetchone()
     if old:c.execute('DELETE FROM '+t+' WHERE user_id=? AND post_id=?',(uid,pid))
     else:c.execute('INSERT INTO '+t+'(user_id,post_id) VALUES (?,?)',(uid,pid))
     return self.reply(payload={'active':not bool(old)})
    if path=='/api/comment':
     pid=int(data.get('post_id',0));body=str(data.get('body','')).strip()
     if not 1<=len(body)<=500:return self.error('Comment must be 1–500 characters')
     p=c.execute('SELECT p.user_id,u.private FROM posts p JOIN users u ON u.id=p.user_id WHERE p.id=?',(pid,)).fetchone()
     if not p or (p['private'] and p['user_id']!=uid):return self.error('Post unavailable',404)
     c.execute('INSERT INTO comments(user_id,post_id,body,created) VALUES (?,?,?,?)',(uid,pid,body,now()));return self.reply(payload={'ok':True})
    if path=='/api/follow':
     username=str(data.get('username',''));target=c.execute('SELECT * FROM users WHERE username=?',(username,)).fetchone()
     if not target or target['private'] or target['id']==uid:return self.error('This user cannot be followed')
     old=c.execute('SELECT 1 FROM follows WHERE follower=? AND followed=?',(uid,target['id'])).fetchone()
     if old:c.execute('DELETE FROM follows WHERE follower=? AND followed=?',(uid,target['id']))
     else:c.execute('INSERT INTO follows(follower,followed) VALUES (?,?)',(uid,target['id']))
     return self.reply(payload={'following':not bool(old)})
    if path=='/api/settings':
     display=str(data.get('display_name','')).strip()[:50];bio=str(data.get('bio','')).strip()[:240];city=str(data.get('city','')).strip()[:80];private=int(bool(data.get('private',False)))
     if not display:return self.error('Display name required')
     c.execute('UPDATE users SET display_name=?,bio=?,city=?,private=? WHERE id=?',(display,bio,city,private,uid));return self.reply(payload={'ok':True})
    if path=='/api/challenge':
     cid=int(data.get('id',0));delta=int(data.get('delta',0))
     if not c.execute('SELECT 1 FROM challenges WHERE id=?',(cid,)).fetchone():return self.error('Challenge not found',404)
     if delta not in (0,1):return self.error('Invalid progress')
     if delta==0:c.execute('INSERT OR IGNORE INTO entries(user_id,challenge_id,progress) VALUES (?,?,0)',(uid,cid))
     else:
      if not c.execute('SELECT 1 FROM entries WHERE user_id=? AND challenge_id=?',(uid,cid)).fetchone():return self.error('Join challenge first')
      c.execute('UPDATE entries SET progress=MIN(progress+1,(SELECT target FROM challenges WHERE id=?)) WHERE user_id=? AND challenge_id=?',(cid,uid,cid))
     return self.reply(payload={'ok':True})
    if path=='/api/report':
     pid=int(data.get('post_id',0));reason=str(data.get('reason','')).strip()[:240]
     if not reason or not c.execute('SELECT 1 FROM posts WHERE id=?',(pid,)).fetchone():return self.error('Select a post and reason')
     c.execute('INSERT INTO reports(reporter_id,post_id,reason,created) VALUES (?,?,?,?)',(uid,pid,reason,now()));return self.reply(payload={'ok':True})
    if path=='/api/block':
     username=str(data.get('username',''));target=c.execute('SELECT id FROM users WHERE username=?',(username,)).fetchone()
     if not target or target['id']==uid:return self.error('Invalid account')
     c.execute('INSERT OR IGNORE INTO blocks(blocker,blocked) VALUES (?,?)',(uid,target['id']));return self.reply(payload={'ok':True})
    if path=='/api/admin/review':
     if u['username']!=os.environ.get('TRAIL_ADMIN',''):return self.error('Forbidden',403)
     rid=int(data.get('id',0));action=str(data.get('action',''))
     if action not in ('dismiss','remove'):return self.error('Invalid action')
     r=c.execute('SELECT * FROM reports WHERE id=?',(rid,)).fetchone()
     if not r:return self.error('Report missing',404)
     c.execute('UPDATE reports SET status=? WHERE id=?',(action,rid))
     if action=='remove':c.execute('UPDATE posts SET status="removed" WHERE id=?',(r['post_id'],))
     return self.reply(payload={'ok':True})
    return self.error('Not found',404)
  except (ValueError,TypeError,KeyError) as e:return self.error(str(e))
  except PermissionError as e:return self.error(str(e),401)
  except Exception as e:print('POST ERROR',e);return self.error('Server error',500)
 def issue(self,c,uid):
  token=secrets.token_urlsafe(32);c.execute('INSERT INTO sessions(token_hash,user_id,expires) VALUES (?,?,?)',(hashlib.sha256(token.encode()).hexdigest(),uid,now()+30*86400))
  # Set TRAIL_SECURE_COOKIE=1 when served with HTTPS.
  secure='; Secure' if os.environ.get('TRAIL_SECURE_COOKIE')=='1' else ''
  u=c.execute('SELECT * FROM users WHERE id=?',(uid,)).fetchone()
  return self.reply(payload={'user':self.public_user(u)},headers={'Set-Cookie':f'session={token}; HttpOnly; SameSite=Lax; Path=/; Max-Age=2592000{secure}'})

if __name__=='__main__':
 init();print(f'TrailCircle listening on http://localhost:{PORT}');ThreadingHTTPServer(('0.0.0.0',PORT),Handler).serve_forever()
