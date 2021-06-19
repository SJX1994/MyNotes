import maya.cmds as cmds
import re #regular expression

class MessageBox:
  def __init__(self, info):
    self.MessageBox(info)
    
  def MessageBox(self,info):
    self.msg = cmds.window()
    cmds.columnLayout()
    cmds.button(l=info, w=300, h=100,c=self.Close)
    cmds.showWindow(self.msg);
    return;
    
  def Close(self,btn):
      cmds.deleteUI(self.msg);
    

def NormalAction(fs):
    for f in fs:
        node = f.split('.')[0];
        pi = cmds.polyInfo(f, fn=True);
        vss = cmds.polyInfo(f,fv = True);
        fn = re.findall(r"[\w.-]+", pi[0]); # convert the string to array with regular expression
        vssre = re.findall(r"[\w]+",vss[0]);
        
        
        vs = [];
        
        for index in range(2,len(vssre)):
            vs.append("{0}.vtx[{1}]".format(node,vssre[index]));
        
        cmds.select(vs,r = True);
        cmds.polyNormalPerVertex( xyz = (float(fn[2]),float(fn[3]),float(fn[4]) ) );
    
    

cmds.ConvertSelectionToFaces();
fs = cmds.filterExpand( sm=34 );

if fs is None:
    MessageBox("Î´Ñ¡ÖÐÃæ£¡");
else:
    NormalAction(fs);
