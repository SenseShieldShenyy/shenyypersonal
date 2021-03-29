# 请在...补充一行或多行代码
def prime(m):
    bo = True
    for i in range(2,m):
        if m % i == 0:
            bo= False
            break
    return(bo)
n = eval(input())
n = int(n)
cnt = 0
while True:
    if prime(n):
        print(n,end='')
        cnt += 1
        if cnt == 5:
            break
        print(',',end='')
    n += 1
