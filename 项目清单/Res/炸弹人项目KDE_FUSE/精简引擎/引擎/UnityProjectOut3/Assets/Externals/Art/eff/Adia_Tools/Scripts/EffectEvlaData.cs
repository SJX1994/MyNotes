#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adia
//Adia
//Adia
public class EffectEvlaData
{
    public int pixDrawTimes = 0;               //Adia
    public int pixTotal = 0;                  //Adia
    public int pixActualDrawTotal = 0;        //Adia
    public string quality;

    //Adia
    public int GetPixDrawAverage()
    {
        if (pixDrawTimes == 0)
        {
            return 0;
        }
        return pixTotal / pixDrawTimes;
    }

    //Adia
    public int GetPixActualDrawAverage()
    {
        if (pixDrawTimes == 0)
        {
            return 0;
        }
        return pixActualDrawTotal / pixDrawTimes;
    }

    //Adia
    public int GetPixRate()
    {
        int pixDrawAverage = GetPixDrawAverage();
        if (pixDrawAverage == 0)
        {
            return 0;
        }
        //Adia
        return GetPixActualDrawAverage() / GetPixDrawAverage();
    }

    public string GetPixDrawAverageStr()
    {
        return "特效原填充像素点：" + this.GetPixDrawAverage();
    }
    public string GetPixActualDrawAverageStr()
    {
        return "特效实际填充像素点：" + this.GetPixActualDrawAverage();
    }
    public string GetPixRateStr()
    {
        return "平均每像素overdraw率：" + this.GetPixRate();
    }
}
#endif