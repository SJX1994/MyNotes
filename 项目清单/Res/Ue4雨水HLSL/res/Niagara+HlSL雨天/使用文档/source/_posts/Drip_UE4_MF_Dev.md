---
title:
- Ue4 雨水材质球_侧边水流_MF_开发者选项
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



# <center><b>侧边水流_MF_开发者选项</b></center>

- ## <b>开始之前</b>
    - *下载* - 必要的资源[下载](https://nodesource.com/products/nsolid)
    - *文档* - 该显示效果需要[使用MF](https://docs.unrealengine.com/en-US/RenderingAndGraphics/Materials/Functions/Using/index.html)


- ## <b>效果预览</b>
![侧边水流参数图](/images/Drip/img-01.jpg "侧边水流参数图")




- ## <b>关于MF</b>
    - *MF名称:*  `RainDrip_MF`
    - *MF输入:*  `null`
    - *MF输出:*  `Mask` ，`Normal`

- ## <b>开发者选项</b>
![侧边水流开发者选项总览](/images/Drip/Developer/img-01-03.jpg "侧边水流开发者选项总览")
    - 侧边水流主要分为4个部分:<br>如图所示：<br>让水流的流淌路径变得自然<br>水流动画控制<br>水流侧边遮罩<br>侧边遮罩的法线图
        ***
        - 第一部分：让水流的流淌路径变得自然
        ***
        ![水流流向前后对比](/images/Drip/Developer/img-01-04.jpg "水流流向前后对比")
        - 上图为操作前后对比
        ![基本贴图设置](/images/Drip/Developer/img-01-05.jpg "基本贴图设置")
            - 1.1 如上图所示<br>定义了3个节点TOOL_UVSetter和textureSample和Multiply.
                - textureSample<br>贴图选择：rain_drips.tga,这个贴图包含4个通道，RG通道代表法线信息(需要自行定义B通道结合着使用),B表示水流扭曲,A表示速度通道，会结合Time节点实现动画效果，如果想自定义这些表现，请使用PhotoShop或者SubstantDesign来修改。注意每一个通道与其他通道之间的匹配。
                - TOOL_UVSetter<br>(传送门：{% post_link TOOLMF_UVSetter_UE4 %} )<br>这个节点需要4个浮点数输入值,其中moveX，moveY在之后完成节点后可以分别控制：水滴宽度和水滴的波动。
                - Multiply<br>如绿色箭头指向的输入点所示，这个点稍后会接入对贴图世界坐标的更改，让水流更加自然。
            ![对UV进行操作](/images/Drip/Developer/img-01-06.jpg "对UV进行操作")
            - 1.2 如上图所示<br>主要分为两部分计算，产生4个随机样式和2个Alpha遮罩。
                - 产生4个随机样式<br>创建absoluteWorldPosition节点，获取在世界空间每个像素点的绝对坐标,通过与两个3维向量相乘，获得两款作用于模型的不同样式，<br>以第一个样式的RB通道获取第三个随机样式<br>再以模块2产生的2个Alpha遮罩中的一个进行插值分配计算.取得的值转变成只有GB通道 的二维向量，以获得第四个随机样式，
                - 2个Alpha遮罩<br>分别通过Sine+Saturate和Abs+Round取得他们的R通道的值，获得两个足够随机的Alpha遮罩
                - 合并<br>通过两个Lerp进行混合，获取一个可以让水流流向足够随机的UV贴图通过绿色箭头和创建好的UV控制节点通过Multiple相结合获得一个目标UV
        ***
        - 第二部分：水流动画控制
        ***
        ![水流静态动态前后对比](/images/Drip/Developer/img-01-07.jpg "水流静态动态前后对比")
        - 上图为操作前后对比
        ![水流动态节点图](/images/Drip/Developer/img-01-08.jpg "水流动态节点图")
            - 如上图所示<br>动态水流分为4个组成部分：流速插值,流动方向,时间,水滴形状
                ![流速插值](/images/Drip/Developer/img-01-09.jpg "流速插值")
                - 流速插值<br>以A通道作为主要每股流速的控制，B通道作为水流形状控制，这里B通道只是作为组成三维向量的第一维借道通过，并无实际作用，通过Lerp插值在基础速度通道图的基础上新增两组钳制，让水流速度更加随机。
                ![流动方向](/images/Drip/Developer/img-01-10.jpg "流动方向")
                - 获取模型absoluteWorldPosition后对z轴进行缩放，以达到水流效果拉伸的效果最后只取得三维向量的第一维度+z到-z的维度，其中的插值代表了水流的长度，Add加上的是时间维度，这样水流就会随着z轴递增，达到往下流淌的效果
                ![时间维度](/images/Drip/Developer/img-01-11.jpg "时间维度")
                - 时间乘的是速度插值，乘以时间以后就形成了每股水流的时间差
                ![水滴形状](/images/Drip/Developer/img-01-12.jpg "水滴形状")
                - 已经拥有时间和时间差的插值和形状通道相乘（正片叠底）后获得了最终的水滴动画
        ***
        - 第三部分：水流侧边遮罩
        ***
        ![水流遮罩输出](/images/Drip/Developer/img-01-13.jpg "水流遮罩输出")
        - 上图为最终遮罩效果图
        ![水流遮罩输出节点图](/images/Drip/Developer/img-01-14.jpg "水流遮罩输出节点图")
            - 如上图所示<br>最终这个MF会输出一个遮罩图，也可用于Roughness节点<br>通过空间法线VertexNormalWS的B值，取得一个z轴的由白到黑的插值，再通过Abs绝对值获取上下白中间黑的空间法线的插值，用lerp节点控制中间黑色的区域，用Saturate节点钳制输出值为0-1之间，用Multiply节点使：已经拥有时间和时间差的插值和形状通道 与 上个节点的输出值 相乘(正片叠底)，获得一个只作用于侧面的水流效果，用Power做一个2次方对水流效果做一个减弱，oneMinus做遮罩反转，导出遮罩
        ***
        - 第四部分：侧边遮罩的法线图
        ***
        ![水流遮罩输出](/images/Drip/Developer/img-01-15.jpg "水流遮罩输出")
        - 上图为最终遮罩后的法线效果图
        ![水流遮罩输出节点图](/images/Drip/Developer/img-01-16.jpg "水流遮罩输出节点图")
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
