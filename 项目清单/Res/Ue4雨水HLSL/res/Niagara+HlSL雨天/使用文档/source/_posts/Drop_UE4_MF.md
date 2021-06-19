---
title:
- Ue4 雨水材质球_顶部雨滴_MF
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



# <center><b>顶部雨滴_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于控制水流的各种参数，在实例化一颗材质球后，可以看到所有可控参数
<!-- [![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid) -->
![顶部雨滴参数图](/images/Drop/img-02-02.jpg "顶部雨滴节点连接图")
![顶部雨滴参数图](/images/Drop/img-02-01.jpg "顶部雨滴参数图")

| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **Drop_Frequency** | 该数值控制雨点动画的频率，表现为降雨量 |  `浮点数`  |
| **Drop_RainStaticNum** | 该数值控制不参与动画雨点的数量 |  `浮点数`  |
| **Drop_RandomLevel** | 该数值控制雨点的随机排布方式 |  `浮点数`  |
| **Drop_Size** | 该值与整体雨点大小呈反比 |  `浮点数`  |
| **Drop_Texture**| 该参数需要输入一张.tga带有通道的贴图文件<br>R通道+G通道：水点法线RG信息<br/>B通道：代表了不参与动画雨点的形状 <br/>A通道：代表了参与雨点动画的动画之间频率差距的信息<br/> |  `.tga`  |
| **Drop_ShapTexture** | 该参数需要输入一张.tga带有通道的贴图文件<br>B通道：随机分布图<br/> |  `.tga` |

- ## <b>效果预览</b>
![顶部雨滴参数图](/images/Drop/img-02.jpg "顶部雨滴参数图")




- ## <b>关于MF</b>
    - *MF名称:*  `RainDrop_MF`
    - *MF输入:*  `null`
    - *MF输出:*  `Mask` ，`Normal`






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
