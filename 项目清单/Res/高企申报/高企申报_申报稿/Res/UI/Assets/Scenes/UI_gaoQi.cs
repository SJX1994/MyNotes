using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_gaoQi : MonoBehaviour
{
    public GUISkin guiSkin;

    Rect windowRect = new Rect(0, 0, 400, 380);
    bool toggleTxt = false;
    string stringToEdit = "输入数字";
    string textToEdit = "TextBox:\nHello World\nI've got few lines...";
    float hSliderValue = 0.0f;
    float vSliderValue = 0.0f;
    float hSbarValue;
    float vSbarValue;
    Vector2 scrollPosition = Vector2.zero;

    void Start()
    {
        windowRect.x = (Screen.width - windowRect.width) / 2;
        windowRect.y = (Screen.height - windowRect.height) / 2;
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        
        windowRect = GUI.Window(0, windowRect, DoMyWindow2, "Surface Smooth v1.0");
    }
    void DoMyWindow(int windowID)
    {
        //reat（左，右，上，下）
        GUI.Box(new Rect(10, 50, 230, 360), "\n\n软件简介\n\n本软件用于曲线捕捉扫描技术\n取代传统点云式扫描\n优化并减少取点数量\n大大缩减扫描所需的时间\n计算准确，无线接近于平滑\n节省批量扫描的时间成本");
        GUI.Button(new Rect(260, 60, 120, 60), "高速建模");
        GUI.Label(new Rect(260, 125, 120, 20), "快速建立3d形象");
        GUI.Button(new Rect(260, 60  + 100, 120, 60), "精细捕捉");
        GUI.Label(new Rect(260, 125 +100, 120, 20), "按住捕捉执行生成");
        GUI.Button(new Rect(260, 70 + 230, 120, 30), "设置");
        GUI.Label(new Rect(260, 125 + 210, 120, 20), "      详细设置");
    }
    void DoMyWindow2(int windowID)
    {
        //reat（左，右，上，下）

        GUI.Box(new Rect(10, 50, 230, 360), "\n高速建模预览微调");
        GUI.Button(new Rect(260, 60, 120, 60), "保存扫描模型");
        GUI.Label(new Rect(260, 125, 120, 20), "保存这个模型到指定路径");
       // GUI.Button(new Rect(260, 60 + 100, 120, 60), "精细捕捉");
        //GUI.Label(new Rect(260, 125 + 100, 120, 20), "按住捕捉执行生成");
        GUI.Button(new Rect(260, 70 + 230, 120, 30), "设置");
        GUI.Label(new Rect(260, 125 + 210, 120, 20), "      详细设置");
        GUI.TextArea(new Rect(260, 160, 185, 100), "3D打印机\n传输完成！", 200);
        var   aa = GUI.skin.GetStyle("PicOne");
        GUI.Label(new Rect(30, 130, 200, 190), "", aa);

    }
    void DoMyWindow3(int windowID)
    {
         GUI.TextField(new Rect(15, 75, 110, 20), "输入数字", 25);
         GUI.TextField(new Rect(15, 80+35, 110, 20), "单位/秒", 25);
         GUI.TextField(new Rect(15, 80 + 71, 110, 20), "0~360度", 25);
        GUI.Box(new Rect(10, 50, 120, 210), "请输入参数");
        GUI.Box(new Rect(135, 50, 120, 210), "\n控制点数量\n\n采集时间\n\n采集角度");
        GUI.Button(new Rect(260, 125, 120, 30), "保存设置");
        GUI.Button(new Rect(260, 175, 120, 30), "恢复设置");
    }
}
