---
title:
- Ue4 雨水材质球_顶部雨滴_MF_开发者选项
tags: 
    - Ue4
    - 材质球
categories:
    - Ue4
    - Mat
    - Rain
    - Dev
author:
    - 沈捷翔_ue4


---

> <center><big><b>简介：</b></big><br>这是整套雨水材质的使用流程，其中封装了多个MF，可以将封装好的MF拆开使用。此片文档主要面向对已经封装好的MF进行二次开发，建议在二次开发之前新建一个备份文件。以下各个节点有先后顺序，如果先前的节点显示有问题，后面的节点是没有办法正常显示的<br>-----------------------------------------------------------------------------</center>



# <center><b>顶部雨滴_MF_开发者选项</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>效果预览</b>
![顶部雨滴预览1](/images/Drop/img-02.jpg "顶部雨滴预览1")
![顶部雨滴预览2](/images/Drop/img-02.1.jpg "顶部雨滴预览2")




- ## <b>关于MF</b>
    - *MF名称:*  `RainDrop_MF`
    - *MF输入:*  `null`
    - *MF输出:*  `Mask` ，`Normal`

- ## <b>开发者选项</b>
![顶部雨滴开发者选项总览](/images/Drop/Developer/img-02-03.jpg "顶部雨滴开发者选项总览")
    - 顶部雨滴主要分为4个部分:<br>如图所示：<br>让水流的流淌路径变得自然<br>水流动画控制<br>水流侧边遮罩<br>侧边遮罩的法线图
        ***
        - 第一部分：让顶部雨滴变得随机自然
        ***
        - ![随机节点设置](/images/Drop/Developer/img-02-04.jpg "随机节点设置")
            - 1.1 如上图所示<br>定义了主要的3个节点TOOL_UVSetter和textureSample和CustomRotator.
                - textureSample<br>贴图选择：rain_drops.tga,这个贴图包含4个通道，RG通道代表法线信息(需要自行定义B通道结合着使用),B表示水流扭曲,A表示速度通道，会结合Time节点实现动画效果，如果想自定义这些表现，请使用PhotoShop或者SubstantDesign来修改。注意每一个通道与其他通道之间的匹配。
                - TOOL_UVSetter<br>(传送门：{% post_link TOOLMF_UVSetter_UE4 %} )<br>这个节点不是UE4自带的节点，控制了对UV的XY轴的缩放位移。
                - CustomRotator<br>用于旋转TextureSample。
            - 1.2 如上图所示<br>有两个参数暴露
                - Drop_Size: 通过UV的缩放，控制水滴在模型表面的密度
                - Drop_RandomLevel: 通过旋转角度，UV移动控制水滴在模型表面分布的随机度
        ***
        - 第二部分：控制水滴表现作用于顶面
        ***
        - ![顶面作用域节点图](/images/Drop/Developer/img-02-05.jpg "顶面作用域节点图")
            - 如上图所示<br>作用域限制主要靠 VertexNormalWS 获取空间法线方向,用Mask遮罩出Z轴向上的部分再用一个参数Drop_Strength来控制灰度达到控制雨点在表现上的强度
                - TOOL_UVSetter<br>(传送门：{% post_link TOOLMF_UVSetter_UE4 %} )<br>这个节点不是UE4自带的节点，控制了对UV的XY轴的缩放位移。
                - CustomRotator<br>用于旋转TextureSample。
                - textureSample<br>贴图选择：T_ChannelPackedNoise.tga,这个贴图使用的是UE4自带的噪点图只包含随机信息,取出了蓝色通道
        ***
        - 第三部分：让雨点的表现和分布更加随机
        ***
        - ![更加随机](/images/Drop/Developer/img-02-06.jpg "更加随机")
            - 如上图所示<br>红色箭头引入的是Drop_RandomLevel,这里和控制“随机节点设置”部分基本相同,
        ***
        - 第四部分：侧边遮罩的法线图
        ***
        ![水流遮罩输出](/images/Drop/Developer/img-02-15.jpg "水流遮罩输出")
        - 上图为最终遮罩后的法线效果图
        ![水流遮罩输出节点图](/images/Drop/Developer/img-02-16.jpg "水流遮罩输出节点图")
            - 如上图所示<br>最终这个MF会输出一个法线图<br>左边输入的两个值是rain_drips.tga的RG通道，这两个值*2/0.5是为了可以被用作法线图做的处理，将输出的结果 与 已经拥有时间和时间差的插值和形状通道 相乘，获得动画，再与之前生成的遮罩相乘，获得遮罩，合并一个B值，成为3维向量，法线图就完成了




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
