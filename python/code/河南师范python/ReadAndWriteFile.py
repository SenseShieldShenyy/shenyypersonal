file = open("test.txt",'w+')
s ='''A lot of people today are animal  rights  advocates.
Some of them are very
Passionate and even quite radical about the issue.
But others argue that “human rights” will always
take priority. In fact, in many places even
the most basic human rights are not adequately
Protected. So why animal rights? What do you
think? And why?'''
file.write(s)
file.close()
Cntline = 0
CntP    = 0
dic     = {} 
opfile = open("test.txt")
filecontent = opfile.readlines()
opfile.close()
for line in filecontent:
    Cntline += 1
    dic[Cntline] = len(line)
    if line[0] == 'P':
        CntP += 1
print('该文本文件共有有{}行'.format(Cntline))
print('文件中以大写字母P开头的有{}行'.format(Cntline))
lis = list(dic.items())
lis.sort(key = lambda x:x[1])
print('包含字符最多的是第{}行'.format(lis[-1][0]))
print('包含字符最少的是第{}行'.format(lis[0][0]))

