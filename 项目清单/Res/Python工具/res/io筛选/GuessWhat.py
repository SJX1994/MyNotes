import io
import re
import json

class GuseeWhatInForlder:
    def __init__(self,txtToRead,txtToExprot):
        self.txtToRead = txtToRead
        self.txtToExprot = txtToExprot
    
    def readByLine(self):
        listR=[]
        print('reading:...'+self.txtToRead)
        f = io.open(self.txtToRead,'r',encoding='utf-8')
        fileContents = f.read()

        fileContents = fileContents.splitlines()
        
        for singlePathSet in fileContents:
            singlePathSetArray = singlePathSet.split("  *  ")
            gassname = singlePathSetArray[0].rsplit("\\",1)
            gassname[-1]
            gassname2 = singlePathSetArray[1].strip().replace(' ','').split("+")[0].split(".")[0]

            FinalGuess = " 最终文件夹名： "+gassname[-1]+"  包含关键字:  "+gassname2 
            print(FinalGuess)
            listR.append(FinalGuess)
        f.close()
        return listR
       

    def outPut(self):
        file = io.open(self.txtToExprot,'w',encoding='utf-8')
        G = GuseeWhatInForlder(self.txtToRead,self.txtToExprot)
        listR =G.readByLine()
        for item in listR:
            file.write(str(item)+'\n')
        
        file.io.close()

G = GuseeWhatInForlder('ForExcel.txt','ForExcel_guess.txt')
G.readByLine()
G.outPut()