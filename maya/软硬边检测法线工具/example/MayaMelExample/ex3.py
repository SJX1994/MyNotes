import maya.cmds as cmds;
import pymel.core as pm;
from math import pow,sqrt;
version = "安全打开 ";


def safeOpen(*arg):
    
    print"安全打开"
    global filePath
    
    multipleFilters = "Maya Files (*.ma *.mb);;Maya ASCII (*.ma);;Maya Binary (*.mb);;All Files (*.*)"
    
    
    c= cmds.fileDialog2(fileMode=1,fileFilter=multipleFilters, dialogStyle=0)
    print c
    if c :
        cmds.file(c,force=True,esn=False,open=True)
        cmds.confirmDialog(message="已安全打开，结束操作") 
        cmds.deleteUI(safeSaveWin,window=True) 
       
    else:
        cmds.confirmDialog(message="请选择一个文件") 
        
    
        
    return;
    
def Close(*arg):
    
    print"取消"
    cmds.confirmDialog(message="结束操作") 
    cmds.deleteUI(safeSaveWin,window=True)
    return;import maya.cmds as cmds;
import pymel.core as pm;
from math import pow,sqrt;
version = "安全打开 ";


def safeOpen(*arg):
    
    print"安全打开"
    global filePath
    
    multipleFilters = "Maya Files (*.ma *.mb);;Maya ASCII (*.ma);;Maya Binary (*.mb);;All Files (*.*)"
    
    
    c= cmds.fileDialog2(fileMode=1,fileFilter=multipleFilters, dialogStyle=0)
    print c
    if c :
        cmds.file(c,force=True,esn=False,open=True)
        cmds.confirmDialog(message="已安全打开，结束操作") 
        cmds.deleteUI(safeSaveWin,window=True) 
       
    else:
        cmds.confirmDialog(message="请选择一个文件") 
        
    
        
    return;
    
def Close(*arg):
    
    print"取消"
    cmds.confirmDialog(message="结束操作") 
    cmds.deleteUI(safeSaveWin,window=True)
    return;
 
safeSaveWin=cmds.window(version);
  
cmds.window( NameCheckWin, edit=True, widthHeight=(300, 100) );
cmds.columnLayout(rs=10);
cmds.text(version);

cmds.rowColumnLayout( numberOfColumns=2,cs=[2,10],rs=[1,10]);

cmds.button(label="安全模式打开",command=safeOpen);
cmds.button(label="取消",command=Close);
cmds.showWindow(NameCheckWin);
 
safeSaveWin=cmds.window(version);
  
cmds.window( NameCheckWin, edit=True, widthHeight=(300, 100) );
cmds.columnLayout(rs=10);
cmds.text(version);

cmds.rowColumnLayout( numberOfColumns=2,cs=[2,10],rs=[1,10]);

cmds.button(label="安全模式打开",command=safeOpen);
cmds.button(label="取消",command=Close);
cmds.showWindow(NameCheckWin);