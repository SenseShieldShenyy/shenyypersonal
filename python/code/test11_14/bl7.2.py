fo = open("data.csv")
ls = []
for line in fo:
    ls.append(line.replace(" ",'').strip('\n'))
ls.reverse()
for item in ls:
    print(item[::-1].replace(',',';'))
