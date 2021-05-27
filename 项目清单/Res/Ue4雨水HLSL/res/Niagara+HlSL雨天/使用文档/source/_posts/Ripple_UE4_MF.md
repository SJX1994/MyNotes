---
title:
- Ue4 雨水材质球_涟漪_MF
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



# <center><b>涟漪_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于控制水流的各种参数，在实例化一颗材质球后，可以看到所有可控参数
<!-- [![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid) -->
![涟漪参数图](/images/Puddle/Ripple/img-05-01.jpg "涟漪参数图")

| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **Ripple_Frequency** | 该数值控制涟漪动画的频率，用于模拟雨势的大小 |  `浮点数`  |
| **Ripple_QuantityLevels** | 该数值控制涟漪产生波纹的层级数量 |  `浮点数`  |
| **Ripple_Size** |  该数值控制单个涟漪波纹的大小，数值与大小呈反比 |  `浮点数`  |
| **Ripple_Texture**| 该参数需要输入一张.tga带有通道的贴图文件<br>R通道：涟漪从中间到边缘的强度插值<br>G通道+B通道：涟漪的法线RG信息<br>A通道：控制每个涟漪动画之间的速度差 |  `.tga`  |
| **Ripple_Trickness** |  该数值控制单个涟漪波纹的涟漪层级与涟漪中心的距离 |  `浮点数`  |
| **Ripple_Strength** |  该数值控制单个涟漪波纹的强度 |  `浮点数`  |

- ## <b>效果预览</b>
![涟漪参数图](/images/Puddle/Ripple/img-05.jpg "涟漪参数图")




- ## <b>关于MF</b>
    - *MF名称:*  `Rain_RipplesSet_MF`
    - *MF输入:*  `Weight(浮点数)` `UVs(TextureCoord)`
    - *MF输出:*  `Normal`






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
