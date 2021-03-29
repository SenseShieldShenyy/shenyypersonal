import requests
url = "https://www.mi.com/buy/detail?product_id=12609"
try :
    kv = {"user-agent":"Mozilla/5.0"}
    r = requests.get(url,headers = kv)
    r.raise_for_status()
    r.enconding = r.apparent_encoding
    print(r.request.headers)
    print(r.text[:1000])
except:
    print("访问错误")
keyword = {"wd":"python"}
url = "http://www.baidu.com/s"
try:
    r = requests.get(url,params = keyword)
    print(r.request.url)
    r.raise_for_status()
    print(len(r.text))
except:
    print("爬取失败")
    
    
