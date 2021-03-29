import requests
url = "http://image.nationalgeographic.com.cn/2017/0211/20170211061910157.jpg"
r = requests.get(url)
r.raise_for_status()
with  open("./heibao.jpg","wb") as f:
    f.write(r.content )
f.close()

#trump = "https://www.natgeo.com.cn/upload/images/2020/11/76ccf452c50ff074.jpg"
