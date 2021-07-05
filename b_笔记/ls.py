import os
import pathlib
from re import escape
import lxml
import scrapy


class FilePath:

    def thisPath(self):

        path = pathlib.Path(__file__).parent.resolve()
        arr = os.listdir(path)

        print(str(path))

        for a in arr:

            if a.endswith('.py') or a.endswith('.txt'):
                continue
            else:
                print('_'+a+':\n')
                pathSub = os.listdir(str(path)+'\\'+str(a))
                for p in pathSub:
                    print('_____'+p+'\n')


class HtmlHacker():
    def __init__(self, path):
        self.path = path

    def local(self):
        path = pathlib.Path(self.path).resolve()
        arr = os.listdir(path)
        for a in arr:

            if a.endswith('.htm'):
                continue
            else:
                pathS = pathlib.Path(self.path+'\\'+a).resolve()
                arrS = os.listdir(pathS)
                print("\n")
                for aS in arrS:
                    if aS.endswith('.htm'):

                        # print(str(pathS+'\\'+aS))
                        if pathS.__str__().endswith('.htm'):
                            continue
                        else:

                            pathSS = pathS.__str__()+'\\'+str(aS)
                            body = open(pathSS,
                                        "r", encoding='utf-8').read()
                            selector = scrapy.Selector(text=body)

                            a_list = selector.xpath('//title/text()').extract()
                            if a_list[0].startswith('教程：'):
                                print('- '+a_list[0])
                            else:
                                print('  - '+a_list[0])

                    else:
                        continue


fp = FilePath()
fp.thisPath()
# h = HtmlHacker('D:\softWare\Kanzi\Documentation\zh-cn\Content\Tutorials')
# h.local()

