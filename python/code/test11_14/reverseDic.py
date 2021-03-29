dicts = {"a": 1, "b": 2}
try:
    key = list(dicts.keys())
    val = list(dicts.values())
    Ndicts = {}
    for i in range(len(dicts)):
        Nkey = val[i]
        Ndicts[Nkey] = key[i]
    print(Ndicts)
except:
    print("输入错误")
