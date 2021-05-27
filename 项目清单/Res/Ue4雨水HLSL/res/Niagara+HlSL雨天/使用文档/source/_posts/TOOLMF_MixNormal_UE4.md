---
title:
- Ue4合并法线_MF
tags: 
    - Ue4
    - 材质球
categories:
    - Ue4
    - Mat
    - Tool
author:
    - 沈捷翔_ue4


---

> <center><big><b>简介：</b></big><br>这是可以通用的工具形Ue4材质球MF,使用MaterialFunctionCall节点来开启MF<br>-----------------------------------------------------------------------------</center>



# <center><b>合并法线_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于合并两个Normal的RGB值



- ## <b>效果预览</b>
![Normal合并节点图](/images/Tool_MixNormal/img-06.jpg "Normal合并节点图")




- ## <b>关于MF</b>
    - *MF名称:*  `Tool_NormalMixFunction`
    - *MF输入:*  `Normal2(3维向量)`，`Normal1(3维向量)` 
    - *MF输出:*  `MixedNormal(3维向量)`






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
