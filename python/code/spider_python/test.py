import requests
from bs4 import BeautifulSoup
kv = {"user-agent":"Mozilla/5.0"}
url = "https://python123.io/ws/demo.html"
r = requests.get(url,headers = kv)
r.raise_for_status()
demo = r.text
soup = BeautifulSoup(demo,'html.parser')
print(soup.prettify())
print(soup.title)
print(soup.b)
for parent in soup.a.parents:
    if parent is None:
        print(parent)
    else:
        print(parent.name)
