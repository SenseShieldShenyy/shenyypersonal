file = open('abc.txt','r')
for line in file.readlines():
    #line += '[prefix]'
    print(line)
