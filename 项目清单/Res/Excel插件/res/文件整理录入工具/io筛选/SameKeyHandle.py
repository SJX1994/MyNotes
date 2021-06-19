


import io
import re
import json


def readTxtOne(strRead1):
    f = io.open(strRead1,'r',encoding='utf-8')
    fileContents = f.read()
    #切片
    pathList = re.split('F:',fileContents) 
    for i in range(len(pathList)):
        pathList[i] ="F:"+pathList[i]
    del pathList[0]
    #申请字典
    dic2={}
    #单个数据组
    for siglePathList in pathList:
        siglePathListSplit = siglePathList.split("  *  ",1)
        key = siglePathListSplit[0].strip()

        
        value = siglePathListSplit[1].strip()
       

        
        
        dic2[key] = value

    f.close()
    return dic2

def export(dic,strOutPut):
    
    file = io.open(strOutPut,'w',encoding='utf-8')
    for k,v in dic.items():
        
        file.write(str(k)+'  *  '+str(v)+'\n')
    file.io.close()


#执行
print("1")
strRead1 = 'exprot1 - 副本 - 副本 - out.txt'
strRead2 = 'pathCollitionMB_out2.txt'
strOutPut = 'exprot11111.txt'
b=readTxtOne(strRead1)
a=readTxtOne(strRead2)
c = {}
sameKey = {}
for key in a:
    if(key in b and a[key] == b[key]):
        sameKey[key] = a[key]

print(sameKey)
c = dict(a,**b)

export(c,strOutPut)
#print(strRead1+"与"+strRead2+"共有的路径："+ a.keys() & b.keys)
