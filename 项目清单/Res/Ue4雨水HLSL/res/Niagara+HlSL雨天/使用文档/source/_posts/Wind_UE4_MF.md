---
title:
- Ue4 雨水材质球_风力_MF
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



# <center><b>风力_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于控制水流的各种参数，在实例化一颗材质球后，可以看到所有可控参数
<!-- [![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid) -->
![风力参数图](/images/Puddle/Wind/img-04-01.jpg "风力参数图")

| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **Puddle_Direction** | 该数值控制水塘受风力影响的方向，目前只暴露了yx与-y-x两个方向 |  `布尔值`  |
| **Puddle_Normal1**| 该参数需要输入一张.tga带有通道的贴图文件<br>R通道+G通道：水点法线RG信息用于表现水塘风力产生的波动<br/> |  `.tga`  |
| **Puddle_Normal2**| 该参数需要输入一张.tga带有通道的贴图文件<br>R通道+G通道：水点法线RG信息用于表现水塘风力产生的波动<br/> |  `.tga`  |


- ## <b>效果预览</b>
![风力参数图](/images/Puddle/Wind/img-04.jpg "风力参数图")




- ## <b>关于MF</b>
    - *MF名称:*  `Wind_MF`
    - *MF输入:*  `Wind_Strengh(浮点数)` 
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
