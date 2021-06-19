---
title:
- Ue4 UV控制器_MF
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



# <center><b>控制UV贴图的各种参数_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于合并调整贴图的缩放平移



- ## <b>效果预览</b>
![UVSetter节点图](/images/Tool_UVSetter/img-07.jpg "UVSetter节点图")




- ## <b>关于MF</b>
    - *MF名称:*  `Tool_UVSetter`
    - *MF输入:*  `StretchX(1维向量)`，`StretchY(1维向量)` ,`moveX(1维向量)` ,`moveY(1维向量)` ,`Density(1维向量)` ,
    - *MF输出:*  `UV(3维向量)`

- ## <b>MF输入说明</b>
| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **StretchX** | 默认值为1，该值与UV X 轴的缩放呈反比 |  `浮点数`  |
| **StretchY** | 默认值为1，该值与UV Y 轴的缩放呈反比 |  `浮点数`  |
| **moveX** | 默认值为1，该值与UV X 轴的位移呈反比 |  `浮点数`  |
| **moveY** | 默认值为1，该值与UV Y 轴的位移呈反比 |  `浮点数`  |
| **Density** | 默认值为1，该值控制2维贴图在3维模型中映射的比例 |  `浮点数`  |

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
