import io
import re
import json

class SameNameChackerClass:
    def __init__(self,txtToRead):
        self.txtToRead = txtToRead
    
    def readByLine(self):
        f = io.open(self.txtToRead,'r',encoding='utf-8')
        fileContents = f.read()
        fileContents = fileContents.splitlines()
        allPath = []
        
        for singleContentSet in fileContents:
            singleContentSetArray = singleContentSet.split("  *  ")
            gassname = singleContentSetArray[0].rsplit('\\',1)
            allPath.append(gassname[-1]) 

        duplicated = set()
        seen = set()
        for path in allPath:
            if path not in seen:
                seen.add(path)
            else:
                duplicated.add('\\'+path)
        
        f.close()
        print(duplicated)

    def readByLine2(self):
        f2 = io.open(self.txtToRead,'r',encoding='utf-8')
        fileContents = f2.read()
        fileContents = fileContents.splitlines()
        allPath2 = []
        
        for singleContentSet in fileContents:
            singleContentSetArray = singleContentSet.split("  *  ") 
            gassname = eval(singleContentSetArray[1])
            gassnameStr = str(gassname[-1])
            allPath2.append(gassnameStr) 
            print(gassnameStr)

            
            
        duplicated = set()
        seen = set()
        for path in allPath2:
            if path not in seen:
                seen.add(path)
            else:
                duplicated.add(path)
        f2.close()
        print(duplicated)

    def readByLine3(self):
        f2 = io.open(self.txtToRead,'r',encoding='utf-8')
        fileContents = f2.read()
        fileContents = fileContents.splitlines()
        allPath2 = []
        
        for singleContentSet in fileContents:
            singleContentSetArray = singleContentSet.split("  *  ") 
            gassname = eval(singleContentSetArray[1])
            gassnameStr = str(gassname[-1])
            gassnameStr = gassnameStr.rsplit(" ",1)[-1].split(".")[0]
            allPath2.append(gassnameStr+'.') 
            print(gassnameStr)

            
            
        duplicated = set()
        seen = set()
        for path in allPath2:
            if path not in seen:
                seen.add(path)
            else:
                duplicated.add(path)
        f2.close()
        print(duplicated)

S = SameNameChackerClass('exprot11111.txt')
S.readByLine3()