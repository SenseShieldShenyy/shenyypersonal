import wordcloud
import jieba
txt = input("请输入要转为词云的文字\n")
w = wordcloud.WordCloud(width=1000,height=700)
w.generate(" ".join(jieba.lcut(txt)))
w.to_file("./pywcloud.png")
