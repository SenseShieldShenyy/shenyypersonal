f = open("data.csv")
for line in f:
    line = line.strip("\n")
    ls = line.split(",")
    ls = ls[::-1]
    print(ls)
    print(",".join(ls))
f.close()

st = '1234567890'
print(','.join(st))
