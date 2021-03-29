class Class:
    num = 1
    name= "python"
    ther= "河南师范大学"
    def __init__(self):
        self.__where = "中国大学MOOC"
    def __printf(self):
        print("课程号：",Class.num)
        print("课程名称：",Class.name)
        print("任课老师：",Class.ther)
        print("上课地点",self.__where)
    def ouput(self):
        self.__printf()
cla = Class()
cla.ouput()
