#cmd 执行：
#CHCP 65001
#F:
#CD Producer_Backup_20150429
# dir /s *.ma *.psd *.ZTL *.jpg *.ai *.max *.obj *.tga *.mp3 *.wmv *.avi *.docx *.pdf *.dmp *.mov *.gif *.psb >pathCollition.txt

import io
import re
import json

from collections import defaultdict

class InputExportTool:
    #def __init__(self):
        


    #导入txt--------------------------------------------------
    def readAndShot(self,strRead):
        f = io.open(strRead,'r',encoding='utf-8')
        fileContents = f.read()


        #print(fileContents)

        #将所有文件切片--------------------------------------------------
        pathList = fileContents.split(" Directory of",-1)
        num = 2
        print("切片完成：其中第"+str(num)+"个元素为：\n"+pathList[num])

        #用键值对归类--------------------------------------------------
        print("归类前的长度: "+str(len(pathList)))
        
        dic = {}

        for siglePathList in pathList:
            siglePathListSplit = siglePathList.split("\n",1)
            key = siglePathListSplit[0].strip()
            value = siglePathListSplit[1]
            
            if key not in dic:

                dic[key] = value

            else:
                    #print(key)
                dic[key] += value

        print("归类后的长度: "+str(len(dic)))

        #print(dic[r'F:\Producer_Backup_20150429\Feedback\Zynga\ZDC\thumbnails\thumbnailer\thumbnailer\thumbnailer'])
        #print(dic)
        f.close()
        return dic

    #导出txt--------------------------------------------------
    def export(self,dic,strOutPut):
        file = io.open(strOutPut,'w',encoding='utf-8')
        for k,v in dic.items():
            
            file.write(str(k)+'  *  '+str(v)+'\n')
        file.io.close()

    #内容排查---------------------------------------------------
    def checkOut(self,strRead):
        I = InputExportTool()
        #导入txt
        f = io.open(strRead,'r',encoding='utf-8')
        fileContents = f.read()
        #切片
        #v1
        pathList = fileContents.split(" bytes",-1)
        #v2
        '''
        pathList = re.split('(F:)',fileContents) 
        for i in range(len(pathList)):
            pathList[i] ="F:"+pathList[i]
        '''
        dic2 = {}

        for siglePathList in pathList:
                
                siglePathListSplit = siglePathList.split("  *  ",1)
                
                key = siglePathListSplit[0].strip()
                #print(key)
                #空文件夹的安全校验
                try:
                    value = siglePathListSplit[1]
                    valueIndex =I.Convert(value)
                    #基于value建立数组
                    '''
                    while(valueIndex.count('\'')):
                        valueIndex.remove('\'')
                    while(valueIndex.count(',')):
                        valueIndex.remove(',')
                    '''
                    valueIndex = I.listToString(valueIndex)
                    #print(valueIndex)
                    #v1
                    valueIndex = re.split(r'(\.MOV|\.TGA|\.JPG|\.PSD|\.ai|\.max|\.psd|\.ZTL|\.jpg|\.ma|\.obj|\.tga|\.mp3|\.wmv|\.avi|\.docx|\.pdf|\.dmp|\.mov|\.gif|\.psb|\.mb|\.MB)',value)
                    valueIndex.append("")
                    valueIndex = ["".join(i) for i in zip(valueIndex[0::2],valueIndex[1::2])]
                    del valueIndex[-1]
                    
                    valueIndexN = []

                    for i in valueIndex:
                        valueIndexN.append(i.strip())
                    
                        
                    dic2[key]=valueIndexN
                    #删除空文件夹
                    if not dic2.get(key):
                        del dic2[key]
                    #存入字典
                    #print(valueIndex)
                    #删除只有jpg的文件夹
                    counter = 0
                    
                    for i in range(len([valueIndex])):
                        de = valueIndex[i].split(".")[-1]
                        #print(valueIndex[i])
                        if de =="jpg" or de =="docx" or de == "mov" or de == "pdf" or de == "gif" or de == "tga" or de == "JPG" or de == "TGA" or de == "MOV": 
                            counter+=1
                            
                        if de == 'ma':
                            valueIndex[i].split(r'.')[-1] = 'ma/max'
                            #print(counter)
                            
                    if counter == len(valueIndex):
                        #print("needDel:"+ listToString(valueIndex))
                        del dic2[key]
                    
                    

                

                    #只取得每个值的后缀名
                    """
                    print(str(valueIndex[0].split(r'.')[-1]))
                    for i in range(len(valueIndex)):
                        valueIndex[i]=str(valueIndex[i].split(r'.')[-1])
                    """
                except IndexError:
                    pass
                continue
        #key = r"F:\Producer_Backup_20150429\Feedback\场景原画\水印"
        #print(dic2.get(key) )
        print("删除空文件夹后的长度："+str(len(dic2)))
        f.close()
        return dic2
    def checkOut2(self,strRead):
        #导入txt
        f = io.open(strRead,'r',encoding='utf-8')
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
            value_list  = eval(value)
            

            s = ""
        
            for i in value_list:
                i = i.split(" ")[-1]
                s += i + '  +  '
                print(i)

            
            
            dic2[key] = s

        f.close()
        return dic2



    #对键值对排序
    def sortDic(self,dic):
        sorted(dic.items(),key=lambda parameter_list: str(parameter_list[0].split(r'\\')[-1]))
        #print(dic)

        return dic

    #冒泡排序    
    def bublle_sort(self,data):
        for i in range(len(data)-1):
            indictor = False
            for j in range(len(data)-1-i):
                if data[j]>data[j+1]:
                    data[j],data[j+1] = data[j+1],data[j]
                    indictor = True
            if not indictor:
                break

    #数组变字符串
    def listToString(self,s):  
        
        # initialize an empty string 
        str1 = ""  
        
        # traverse in the string   
        for ele in s:  
            str1 += ele   
        
        # return string   
        return str1 

    # 字符串变数组
    def Convert(self,string): 
        list1=[] 
        list1[:0]=string 
        return list1



#获取---------------------------------------------------
strRead = 'exprot11111.txt'
strOutPut = 'ForExcel.txt'
#挨个执行一遍
I = InputExportTool()
#I.export(I.sortDic(I.readAndShot(strRead)),strOutPut)
#I.export(I.sortDic(I.checkOut(strRead)),strOutPut)
I.export(I.sortDic(I.checkOut2(strRead)),strOutPut)

            


