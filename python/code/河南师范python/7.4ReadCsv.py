s ='''星座,开始月日,结束月日,Unicode

水瓶座,120,218,9810

双鱼座,219,320,9811

白羊座,321,419,9800

金牛座,420,520,9801

双子座,521,621,9802

巨蟹座,622,722,9803

狮子座,723,822,9804

处女座,823,922,9805

天秤座,923,1023,9806

天蝎座,1024,1122,9807

射手座,1123,1221,9808

摩羯座,1222,119,9809'''
const = []
print(s)
ls = list(s.split('\n'))
print(ls)
for line in ls:
    if line == '':
        continue
    const.append(list(line.split(',')))
    print(line)
print(const)
f = open('const.csv','w')
for row in const:
    f.write(','.join(row)+'\n')
f.close()
#读取二维数据
f = open('const.csv','r')
ls = []
for line in f:
    ls.append(line.strip('\n').split(','))
f.close()
while(True):
    st =input()
    if('Q'== st):
        break
    for line in ls:
        if st == line[0]:
            print(line[1:])
            break
    else:
        print('输入的名称有误')

