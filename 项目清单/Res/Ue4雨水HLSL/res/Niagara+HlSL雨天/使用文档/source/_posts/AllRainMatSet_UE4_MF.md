---
title:
- Ue4 雨水材质球_雨水参数全集_MF
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



# <center><b>水流全部节点_MF</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>参数说明</b>
> 用于控制水流的各种参数，在实例化一颗材质球后，可以看到所有可控参数
<!-- [![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid) -->
![水流全部节点参数图](/images/RainAllSet/img-08-02.jpg "水流全部节点连接图")
![水流全部节点参数图](/images/RainAllSet/img-08-01.jpg "水流全部节点参数图")

| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **Dirp** | 使用者：{% post_link Drip_UE4_MF %} <br> 开发者：{% post_link Drip_UE4_MF_Dev %}|  `关于侧边水流的参数集合`  |
| **Drop** | 使用者：{% post_link Drop_UE4_MF %} <br> 开发者：{% post_link %}|  `关于顶面水滴的参数集合`  |
| **Puddle** | 使用者：{% post_link Puddle_UE4_MF %} <br> 开发者：{% post_link %}|  `关于顶面积水的参数集合`  |
| **Ripple** | 使用者：{% post_link Ripple_UE4_MF %} <br> 开发者：{% post_link %}|  `关于顶面涟漪的参数集合`  |

- ### RainOverAll的使用：
![水流全部节点参数图](/images/RainAllSet/img-08-03.jpg "水流全部节点参数图")
| 参数名称 | 控制效果 | 输入 |
| ------ | ------ | ------ |
| **RainOverAll_Cartoon** | 关闭这个选项会多出RainColorBrightness这个选项，开启后颜色渲染会变成卡通效果 |  `布尔值`  |
| **RainOverAll_Color** | 关闭这个选项雨水将不带有颜色，失去RainColorBrightness这个选项 |  `布尔值`  |
| **RainOverAll_Color** | 这个选项在上个选项打开后生效，雨水会变成选中的颜色 |  `三维向量`  |
| **RainOverAll_RainColorBrightness** | 这个选项在RainOverAll_Color打开后,RainOverAll_Cartoon关闭时生效，此值与雨水颜色亮度呈正比 |  `浮点数`  |
| **RainOverAll_Roughness** | 此值与整体材质湿润度/光滑度呈正比 |  `浮点数`  |
| **RainOverAll_Specular** | 此值与整体材质金属质感/高光度呈正比 |  `浮点数`  |

- ## <b>效果预览</b>
![水流全部节点参数图](/images/RainAllSet/img-08.jpg "水流全部节点真实渲染图")
![水流全部节点参数图](/images/RainAllSet/img-08-04.jpg "水流全部节点卡通渲染图")



- ## <b>关于MF</b>
    - *MF名称:*  `Rain_MF`
    - *MF输入:*  `Normal` `ColorIn`
    - *MF输出:*  `Normal` `Color` `Roughness` `Specular`

- ## <b>开发者选项</b>
    - {% post_link TODO %}



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
