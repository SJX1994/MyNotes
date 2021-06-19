import os
import pathlib
import shutil
import translate
import json
import lxml
import codecs
import urllib
import datetime

from string import digits
from translate.storage.tmx import tmxfile
from io import StringIO, BytesIO
from lxml import etree


fileNames = ["泥土 1", "森林 1", "沙滩 1", "沼泽 1"]
targetNames = ["soil", "forest", "beach", "swamp"]
frontPath = "E:\\Cocos\\第三张图\\Write\\"

# myPath = frontPath+"Lower\\LowerNotSort\\地块色块\\"+fileName+"\\白天\\"
# myPath2 = frontPath+"Lower\\LowerNotSort\\地块体块\\"+fileName+"\\白天\\"
# myPath3 = frontPath+"Lower\\LowerNotSort\\地块色块\\"+fileName+"\\黑夜\\"
# myPath4 = frontPath+"Lower\\LowerNotSort\\地块体块\\"+fileName+"\\黑夜\\"

LowerGetOutPath = frontPath+"Lower\\LowerNotSort\\"
UpperGetOutPath = frontPath+"Upper\\UpperWriteNotSort\\"
AniGetOutPath = frontPath + "Ani\\AniNotShort\\"

LowerBlockDay = frontPath+"Lower\\LowerBlockDay\\"
LowerBlockNight = frontPath+"Lower\\LowerBlockNight\\"
LowerDay = frontPath+"Lower\\LowerDay\\"
LowerNight = frontPath+"Lower\\LowerNight\\"

AniDay = frontPath + "Ani\\AniDay\\"
AniNight = frontPath + "Ani\\AniNight\\"


def NameSets(fileNames, targetNames):

    for fileName in fileNames:
        index = fileNames.index(fileName)
        myPath = frontPath+"Lower\\LowerNotSort\\地块色块\\"+fileName+"\\白天\\"
        myPath2 = frontPath+"Lower\\LowerNotSort\\地块体块\\"+fileName+"\\白天\\"
        myPath3 = frontPath+"Lower\\LowerNotSort\\地块色块\\"+fileName+"\\黑夜\\"
        myPath4 = frontPath+"Lower\\LowerNotSort\\地块体块\\"+fileName+"\\黑夜\\"
        targetName = targetNames[int(index)]
        # print(myPath+"___.__"+fileName+"__._"+targetName)
        ChangeNameWithSubs(myPath, myPath2, myPath3, myPath4, targetName)


def ChangeName(myPath):
    # 获取该目录下所有文件，存入列表中
    fileList = os.listdir(myPath)

    n = 0
    for i in fileList:
        if(i.endswith('.png')):
            print(i)
            # 设置旧文件名（就是路径+文件名）
            oldname = myPath + os.sep + str(i)   # os.sep添加系统分隔符

            # 设置新文件名
            newname = myPath + os.sep + targetName+str(n+1)+'.png'

            os.rename(oldname, newname)  # 用os模块中的rename方法对文件改名
            print(oldname, '======>', newname)

            n += 1
        else:
            continue


def ChageNameWithSub(myPath, targetName):
    n = 0
    for root, dirs, files in os.walk(myPath):
        for f in files:
            oldname = os.path.join(root, f)
            if(oldname.endswith('.png')):
                newname = myPath + os.sep + targetName+str(n+1)+'.png'
                os.rename(oldname, newname)
                print(oldname + '======>' + newname)
                n += 1
            else:
                continue

        for d in dirs:
            print(os.path.join(root, d))


def GetOut(getoutPath):
    now = str(datetime.datetime.now())[:23]
    now = now.replace(":", "_")
    now = now.replace(".", "_")
    print(now)
    for root, dirs, files in os.walk(getoutPath):
        for f in files:

            fName = os.path.join(root, f)

            print(fName)

            if "\\地块体块\\" in fName and "\\白天\\" in fName:
                shutil.copy2(fName, LowerBlockDay)

            if "\\地块体块\\" in fName and "\\黑夜\\" in fName:
                shutil.copy2(fName, LowerBlockNight)

            if "\\地块色块\\" in fName and "\\白天\\" in fName:
                shutil.copy2(fName, LowerDay)

            if "\\地块色块\\" in fName and "\\黑夜\\" in fName:
                shutil.copy2(fName, LowerNight)

            if "\\动画预制体2.0\\" in fName and "\\白天\\" in fName:
                oldName = fName
                newName = fName[:-4]+"_"+now+".png"
                os.rename(oldName, newName)
                shutil.copy2(newName, AniDay)
                print("AniDay:::"+fName)

            if "\\动画预制体2.0\\" in fName and "\\黑夜\\" in fName:
                oldName = fName
                newName = fName[:-4]+"_"+now+"_night.png"
                os.rename(oldName, newName)
                shutil.copy2(newName, AniNight)
                print("AniNight:::"+newName)


def changeSingeName(formName, toName, path):
    fileList = os.listdir(path)
    nameTrans = "good"
    nameTrans2 = "good2"

    for i in fileList:

        if(i == str(formName+'.png')):

            # 设置旧文件名（就是路径+文件名）
            oldname = path + os.sep + str(formName)+'.png'   # os.sep添加系统分隔符

            # 设置新文件名
            newname = path + os.sep + str(nameTrans)+'.png'

            os.rename(oldname, newname)

            for j in fileList:
                if(j == str(toName+'.png')):

                    oldtoName = path + os.sep + str(toName)+'.png'

                    newtoName = path + os.sep + str(nameTrans2)+'.png'

                    os.rename(oldtoName, newtoName)

            os.rename(newname, path+os.sep+str(toName) +
                      '.png')  # 用os模块中的rename方法对文件改名
            os.rename(newtoName, path+os.sep+str(formName) +
                      '.png')
            print(oldname, '<======>', oldtoName)

        else:
            continue


def changeTMXName():
    # for pygame
    # https://stackoverflow.com/questions/20356149/tmxtranslation-memory-exchange-files-in-python
    # https://pytmx.readthedocs.io/en/latest/
    # https://stackoverflow.com/questions/62865612/handling-tile-objects-using-pytmx
    path = "E:\\Cocos\\gitDepot\\assets\\resources\\NewMap03\\NewMap03.tmx"
    # tmxdata = pytmx.TiledMap(path)

    # for layer in tmxdata.visible_layers:
    #     # print(layer.name)
    #     for tile_object in layer.iter_data():
    #         # print(tile_object)

    #         a = tmxdata.get_tile_properties_by_gid(tile_object[2])
    #         if a != None:
    #             aa = json.loads(str(a))
    #             print(aa)
    tree = etree.parse(path)
    root = tree.getroot()
    body = root[2]
    n = 1
    print(len(body))

    for item in body:

        for it in item:

            for i in it:
                if(i.attrib['name'] == "type"):
                    print(i.attrib['value'])
                    i.attrib['value'] = "SingleRiverLand" + str(n)
                    n += 1

    tree.write(path, encoding='utf-8')
    print("end")

# 更改文件名：修改参数 fileName targetName 后
# 格式必须为：
# 地块体块：海洋：白天黑夜
# 地块色块：海洋：白天黑夜


def ChangeNameWithSubs(myPath, myPath2, myPath3, myPath4, targetName):
    ChageNameWithSub(myPath, targetName)
    ChageNameWithSub(myPath2, targetName)
    ChageNameWithSub(myPath3, targetName)
    ChageNameWithSub(myPath4, targetName)


# ChageNameWithSub("E:\\Cocos\\第三张图\\Write\\Upper\\UpperWriteNotSort\\白天\\", "SingleRiverLand")
# ChageNameWithSub("E:\\Cocos\\第三张图\\Write\\Upper\\UpperWriteNotSort\\黑夜\\", "SingleRiverLand")
# GetOut(LowerGetOutPath)
# changeSingeName("Volcanic7", "Volcanic8","E:\\Cocos\\第二张图\\Write\\Lower\\LowerBlockDay\\")
# ChageNameWithSub("E:\\Cocos\\第三张图\\Write\\Ani\\AniNotShort\\动画预制体2.0\\黑夜\\","SingleRiverLandAni")
# NameSets(fileNames, targetNames)

# changeTMXName()
