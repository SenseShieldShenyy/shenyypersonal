import jieba
with open('沉默的羔羊.txt', 'r',encoding = 'utf-8') as f:
    lists =  jieba.lcut(f.read())
    cnt = {}
    for i in lists:
        if len(i) < 2:                                  #要求字符串的长度必须大于2才纳入统计
            continue
        cnt[i] = cnt.get(i,0) + 1
    print(type(cnt.items()))
    items = list(cnt.items())
    items.sort(key = lambda x:x[1],reverse = True)
    word, count = items[0]
    print("{:<10}{:>5}".format(word, count)) 
    

