#hanoi
count = 0;
def hanoi(n, src, dst, mid):
    global count
    if n == 1:
        print("{}:{}->{}".format(1, src, dst))  #基例 只有一个圆盘的时候直接从原来的柱子搬运到目标柱子
        count += 1
    else:                                       #链条 有N个圆盘的时候将N-1 个圆盘从原柱子搬运到中间柱子目标柱子作为过度
        hanoi(n-1,src, mid, dst)                #调用韩nota函数，先将原柱子中搬运n-1个到中间柱子
        print("{}:{}->{}".format(n, src, dst))    #然后将最后一个圆盘从原柱子搬运到目标柱子
        count += 1                              #搬运次数+1
        hanoi(n-1,mid, dst, src)                #最后将中间柱子的n-1 个圆盘 搬到目标柱子，目标i柱子作为过度
hanoi(3,"A","C","B")
print(count)
