fo = open('latex.log')
print('共{}字符'.format(len(fo.read())),end=',')
i = 1
cnt = {}
Lalpha = list('qwertyuiopasdfghjklzxcvbnm')
fo.seek(0)
while i != '':
    i = fo.read(1)
    if i in Lalpha:
        cnt[i] = cnt.get(i,0) +1
ls = list(cnt.items())
ls.sort(key = lambda x:x[0],reverse = False)
for i in range(len(ls)-1):
    print(ls[i][0],end='')
    print(':',end='')
    print(ls[i][1],end=',')
print(ls[i+1][0],end='')
print(':',end='')
print(ls[i+1][1])
