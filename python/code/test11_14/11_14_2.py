import re
from collections import Counter
with open('hamlet.txt', 'r', encoding='utf-8') as f:
    words = f.read()
    words = words.lower()
    count = Counter(re.split(r"\W+",words))
result = count.most_common(10)
print(result)
for i in result:
    print(i[0])
