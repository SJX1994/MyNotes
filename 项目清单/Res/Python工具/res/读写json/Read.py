import zipfile
import io
import os
import json
import collections

oldPath = '\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\oldWrite'
newPath = '\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\newWrite\\surface2\\'
dupID = []


def TestJsonZip():
    readZIP = zipfile.ZipFile(
        '\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\newRead\\surface\\tkdhffqaw_4K_Displacement.zip', 'r')
    imgdata = readZIP.read('tkdhffqaw.json')
    fantasy_Json = json.loads(imgdata.decode("utf-8"))
    if "categories" in fantasy_Json:
        print(fantasy_Json["categories"])
    if "semanticTags" in fantasy_Json:
        print(fantasy_Json["semanticTags"]["descriptive"])
    # print(type(imgdata))


def Tool_listToStr(s, a):
    str1 = ""
    for ele in s:
        str1 += (ele + a)
    return str1


def ZipedAssetsTags(path):
    global dupID
    fileName_tags_dict = {}  # 存放文件名字典
    zipFile_counter = 0

    for root, dirs, files in os.walk(path, topdown=True):
        for name1 in files:  # 路径子目录下所有文件
            if not name1.startswith('.') and name1.endswith('.zip'):
                zipFile_counter += 1
                path = ""
                path = os.path.join(root, name1)
                # print(path)
                readZIP = zipfile.ZipFile(path, 'r')
                for data in readZIP.namelist():
                    if data in dupID:
                        IDsID = [i for i, x in enumerate(dupID) if x == data]
                        print('zip'+str(IDsID) + " --- " + path)  # 重复
                    if data.endswith('.json'):
                        data_Json = readZIP.read(data)
                        fantasy_Json = json.loads(data_Json.decode("utf-8"))
                        # print(name1)
                        # print(fantasy_Json['tags'])
                        values = []  # 字典的value
                        if "tags" in fantasy_Json:
                            values.extend(fantasy_Json["tags"])
                        if "categories" in fantasy_Json:
                            values.extend(fantasy_Json["categories"])
                        if "semanticTags" in fantasy_Json:
                            if "descriptive" in fantasy_Json["semanticTags"]:
                                values.extend(
                                    fantasy_Json["semanticTags"]["descriptive"])
                        fileName_tags_dict[name1] = values

        for name2 in dirs:
            continue
        for name3 in root:
            continue
    print('OldFiles:'+str(zipFile_counter))
    return fileName_tags_dict


def FileAssetsTags(path):
    global dupID
    fileName_tags_dict = {}  # 存放文件名字典
    files_counter = 0
    for root, dirs, files in os.walk(path, topdown=True):
        for name1 in files:  # 路径子目录下所有文件
            if not name1.startswith('.') and name1.endswith('.json'):
                files_counter += 1
                # print('---')

                continue
        for name2 in dirs:
            if (not '2k' in name2) and (not '1k' in name2) and (not 'previews' in name2) and (not 'Thumbs' in name2) and (not '4k' in name2):
                with os.scandir(path + "\\" + name2) as it:
                    for entry in it:
                        if not entry.name.startswith('.') and entry.name.endswith('.json'):
                            if entry.name in dupID:
                                IDsID = [i for i, x in enumerate(
                                    dupID) if x == entry.name]
                                print('file'+str(IDsID) +
                                      " --- " + entry.path)  # 重复
                            data = open(entry.path).read()
                            fantasy_Json = json.loads(data)
                            values = []  # 字典的value
                            if "tags" in fantasy_Json:
                                values.extend(fantasy_Json["tags"])
                            if "categories" in fantasy_Json:
                                values.extend(fantasy_Json["categories"])
                            if "semanticTags" in fantasy_Json:
                                if "descriptive" in fantasy_Json["semanticTags"]:
                                    values.extend(
                                        fantasy_Json["semanticTags"]["descriptive"])
                            fileName_tags_dict[name2] = values

            continue
        for name3 in root:
            # print(name3)
            continue
    print('NewFile:'+str(files_counter))
    return fileName_tags_dict


def ReadUnid(zipPath, filePath):
    global dupID
    zipID = []
    fileID = []
    for root, dirs, files in os.walk(zipPath, topdown=True):
        for name1 in files:  # 路径子目录下所有文件
            if not name1.startswith('.') and name1.endswith('.zip'):
                path = ""
                path = os.path.join(root, name1)
                readZIP = zipfile.ZipFile(path, 'r')
                for data in readZIP.namelist():
                    if data.endswith('.json'):
                        zipID.append(data)
    for root, dirs, files in os.walk(filePath, topdown=True):
        for name2 in dirs:
            if (not '2k' in name2) and (not '1k' in name2) and (not 'previews' in name2) and (not 'Thumbs' in name2) and (not '4k' in name2):
                with os.scandir(filePath + "\\" + name2) as it:
                    for entry in it:
                        if not entry.name.startswith('.') and entry.name.endswith('.json'):
                            fileID.append(entry.name)
    allID = []
    allID = zipID+fileID
    print(len(allID))
    if len(allID) == len(set(allID)):
        print('无重复ID')
    else:
        print('重复ID')
    dupID = [item for item, count in collections.Counter(
        allID).items() if count > 1]
    print(dupID)


def Constrast_TagsSimilar():
    global newPath
    global oldPath
    dict1 = {}
    dict2 = {}
    dict_merged = {}
    result_Num = {}
    dict1 = ZipedAssetsTags(oldPath)
    dict2 = FileAssetsTags(newPath)
    dict2.update(dict1)
    dict_merged = dict2
    for key, values in dict_merged.items():
        values.sort()
        values = Tool_listToStr(values, '')
        result_Num.setdefault(values, set()).add(key)
    result = [values for key, values in result_Num.items() if len(values) > 1]

    print("Dup", str(result))


def Constrast_NameAndTag():
    global newPath
    global oldPath

    dict1 = {}
    dict2 = {}
    dict_merged = {}

    dict1 = ZipedAssetsTags(oldPath)
    dict2 = FileAssetsTags(newPath)
    dict2.update(dict1)
    dict_merged = dict2

    with open("NameAndTag.txt", "w+") as f:
        # for i in range(len(dict_merged)):
        for key, values in dict_merged.items():
            # f.write(key)
            values = Tool_listToStr(values, ',')
            f.writelines(key+' --- '+values+'\n')
        f.close

    print('done')


ReadUnid(oldPath, newPath)
FileAssetsTags(newPath)  # countFile
ZipedAssetsTags(oldPath)  # countZip
# Constrast_TagsSimilar()#countSimilar
# Constrast_NameAndTag()#.txt(with FileName & Tags)
# TestJsonZip()
