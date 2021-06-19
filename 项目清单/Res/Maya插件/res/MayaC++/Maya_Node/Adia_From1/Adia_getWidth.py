from maya import cmds
from maya import OpenMaya
import random

faces_G = None
facesDel_G =None
face_G = None
objectName_G = None

def getWidth():
    ram = 'adia_getWidth_Window'

    if(cmds.window(ram, q = True, exists = True )):
        cmds.deleteUI(ram,control = True)
    naemS = "adia_getWidth_Window"
    ram = cmds.window( "adia_getWidth_Window",title= naemS, iconName='ShortName', widthHeight= (300, 300),maximizeButton = False, minimizeButton=True)
    cmds.columnLayout( adjustableColumn=True )
    adia_NormalValue = cmds.floatFieldGrp(precision = 3,numberOfFields = 1, extraLabel="cm", label ="Filter less than", value1 = 100.0 , columnWidth = (1,120))
    adia_NormalValue2 = cmds.floatFieldGrp(precision = 3,numberOfFields = 1, extraLabel="cm", label ="Face area", value1 = 0.0 , columnWidth = (1,120), en = False)
    adia_DeleteOther = cmds.checkBox(label="DeletOther",value = False)
    cmds.button( label='Get area',command = Do )
    cmds.button( label='Filter area',command = Do2 )
    cmds.button( label='Close', command=('cmds.deleteUI(\"' +ram + '\",         window=True)') )
    cmds.setParent( '..' )
    cmds.showWindow( ram )

def Do(*args):
    cmds.confirmDialog( t = "msg", message = "Splash me with an Error message!!!", button = ["OK"])



    


def Do2(*args):
    global faces_G
    global facesDel_G
    global face_G
    global objectName_G



getWidth()