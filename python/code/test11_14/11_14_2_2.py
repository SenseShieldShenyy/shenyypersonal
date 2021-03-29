with open('hamlet.txt','r',encoding = 'utf-8') as f:
    words = f.read()
    print(type(words))
    words = words.lower()
    for ch in '!"#$%&()*+,-./:;<=>?@[\\]^_‘{|}~':
        words = words.replace(ch, ' ')
    words = words.split()
    count = {}
    for i in words:
        count[i] = count.get(i,0) + 1
    item = list(count.items())
    item.sort(key = lambda x:x[1], reverse = True)
    for i in range(10):
        w , cnt = item[i]
        print(w)
