fo = open("latex.log")
cntrow ,cntline = 0,0
for line in fo:
    if line == '\n':
        continue
    cntrow +=1
    cntline += len(line)
print(round(cntline / cntrow))
print(cntrow)
print(cntline)
 
