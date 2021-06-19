import shutil, errno ,os ,zipfile,time,progressbar,sched

c = 0
s = sched.scheduler(time.time, time.sleep)


def copyanything(src, dst):#拷贝
    try:
        shutil.copytree(src, dst)
    except OSError as exc: # python >2.5
        if exc.errno == errno.ENOTDIR:
            shutil.copy(src, dst)
        else: raise

def _getFilePath_(path):
 
    
    filePaths = []
    for root,dirs,files in os.walk(path,topdown=True): 
        for fileName in files:
            if not fileName.endswith('_Preview.png') and not fileName.endswith('.zip'):
                
                filePathSub = os.path.join(root,fileName)
                filePaths.append(filePathSub)
        
        
    return filePaths

def _zipTaegetFile_(path,name):
     
    print("压缩zip："+ name)
    filePaths = _getFilePath_(path)
    with zipfile.ZipFile(path+'\\'+name+".zip",'w') as zip:
        for fileS in filePaths:
            
            zip.write(fileS,os.path.basename(fileS))
            os.remove(fileS)
    for x in range(6):
        for root,dirs,files in os.walk(path,topdown=True): 
            if not os.listdir(root):
                os.rmdir(root)

def _reName_(path):
    global c
    for root,dirs,files in os.walk(path,topdown=True):
        for name1 in dirs:
            name1list = name1.split('_')
            #print(name1list[0])
            newName = 'M_'+ name1list[0]+'_'+name1list[1]+'_'+name1list[2]
            for root2,dirs2,files2 in os.walk(path+"\\"+name1,topdown=True):
                for name3 in files2:
                    if name3.endswith('.zip'):
                        os.rename(path+"\\"+name1+"\\"+name3,path+"\\"+name1+"\\"+newName+".zip")
                        c+=1
                        print("正在处理第"+str(c)+"个文件:"+newName)
                    if name3.endswith('.png'):
                        os.rename(path+"\\"+name1+"\\"+name3,path+"\\"+name1+"\\"+newName+".png")
            os.rename(path+"\\"+name1,path+"\\"+newName)
        
def zipVarPath(path):
    
    print('.......zipping & changingNames.......')
    for root,dirs,files in os.walk(path,topdown=True):
        for name2 in dirs:
            
            if ( name2!='2k') and (name2!='1k') and (name2!='previews') and (name2!='Thumbs') and (name2!='4k') :
                
                
                
                _zipTaegetFile_(path+"\\"+name2,name2)
                
    _reName_(path)
    print('done!!!')
    


    
#copyanything('\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\1','\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\2\\3')
#_zipTaegetFile_('\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\newWrite\\surface2\\asphalt_coarse_tl1hdibkw\\')
#_reName_('\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\newWrite\\surface3\\')

zipVarPath('\\\\192.168.10.7\\Assets\\SJX_WorkSpace\\newWrite\\surface3\\')


