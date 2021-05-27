using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UI_gaoQiLOD : ScriptableWizard
{
    public GameObject 主相机=null;
    public string 最远距离 = "";
    public string 最大模型面数 = "";
    public string 最小模型面数 = "";

    //显示窗体
    [MenuItem("艾迪亚 /Level Of Detail")]
    private static void ShowWindow()
    {
        ScriptableWizard.DisplayWizard<UI_gaoQiLOD>("艾迪亚 Level Of Detail V1", "开始使用", "停止使用");
    }
    //显示时调用
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    //更新时调用
    private void OnWizardUpdate()
    {
        Debug.Log("OnWizardUpdate");
        helpString = "请输入\n游戏主相机可以达到的最远距离\n游戏场景中期待的最大模型面数\n游戏场景中期待的最小模型面数\n游戏中的主相机";//帮助提示
      
        if (string.IsNullOrEmpty(最小模型面数) && string.IsNullOrEmpty(最大模型面数) && string.IsNullOrEmpty(最远距离) && 主相机 == null) {
            errorString = "请完整输入内容";//错误提示
            
        }
        
        else
        {
            errorString = "";
            helpString = "请点击确认运行该软件";
        }
    }
    //点击确定按钮时调用
    private void OnWizardCreate()
    {
        Debug.Log("OnWizardCreate");
    }
    //点击第二个按钮时调用
    private void OnWizardOtherButton()
    {
        Debug.Log("OnWizardOtherButton");
    }
    //当ScriptableWizard需要更新其GUI时，将调用此函数以绘制内容
    //为GUI绘制提供自定义行为，默认行为是按垂直方向排列绘制所有公共属性字段
    //一般不重写该方法，按照默认绘制方法即可
    protected override bool DrawWizardGUI()
    {
        return base.DrawWizardGUI();
    }
    //隐藏时调用
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    //销毁时调用
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
