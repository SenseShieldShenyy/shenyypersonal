fo = open('data.csv')
for line in fo:
    line = line.strip('\n').replace(' ','')
    print(line)
