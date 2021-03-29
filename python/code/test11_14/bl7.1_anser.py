#open latex.log
fo = open('latex.log')
cnt = 0
for line in fo:
    if line == '\n':
        continue
    cnt += 1
print("共{}行".format(cnt))
