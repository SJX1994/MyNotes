---
title:
- Ue4 雨水材质球_侧边水流_MF
tags: 
    - Ue4
    - 材质球
categories:
    - Ue4
    - Mat
    - Rain
author:
    - 沈捷翔_ue4


---

> <center><big><b>简介：</b></big><br>这是整套雨水材质的使用流程，其中封装了多个MF，可以将封装好的MF拆开使用。<br>-----------------------------------------------------------------------------</center>



# <center><b>侧边水流_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于控制水流的各种参数，在实例化一颗材质球后，可以看到所有可控参数
<!-- [![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid) -->
![侧边水流参数图](/images/Drip/img-01-02.jpg "侧边水流节点连接图")
![侧边水流参数图](/images/Drip/img-01-01.jpg "侧边水流参数图")

| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **Dirp_SpeedMax** | 侧面水滴向下流动的速度会控制在一个区间，该值控制最大水流速度 |  `浮点数`  |
| **Dirp_SpeedMin** | 侧面水滴向下流动的速度会控制在一个区间，该值控制最小水流速度 |  `浮点数`  |
| **Dirp_Long** | 该值控制水滴流过的长度，该值与水滴流过路径的长度呈反比|  `浮点数`  |
| **Drip_SizeX** | 该值控制水滴的宽度 |  `浮点数`  |
| **Drip_SizeY** | 该值控制水滴Y轴的波动程度，越大水滴路径约曲折 |  `浮点数`  |
| **Dirp_ControlCollection**| 该参数需要输入一张.tga带有通道的贴图文件<br>R通道+G通道：水滴法线RG信息<br/>B通道：水滴Y轴的波动程度<br/>A通道：水滴速度<br/> |  `.tga`  |
| **Dirp_ShapTexture** |该参数需要输入一张.tga带有通道的贴图文件<br>A通道：水滴形状<br/> |  `.tga` |

- ## <b>效果预览</b>
![侧边水流参数图](/images/Drip/img-01.jpg "侧边水流参数图")




- ## <b>关于MF</b>
    - *MF名称:*  `RainDrip_MF`
    - *MF输入:*  `null`
    - *MF输出:*  `Mask` ，`Normal`

- ## <b>开发者选项</b>
    - {% post_link Drip_UE4_MF_Dev %}



<style>
    table th:first-of-type {
    width: 30%;
    }
    table th:nth-of-type(2) {
        width: 50%;
    }
    table th:nth-of-type(3) {
        width: 30%;
    }
</style>
