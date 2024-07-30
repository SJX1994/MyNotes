
## 制作：
### LOD制作：
            官方：
                  https://knowledge.autodesk.com/zh-hans/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2019/CHS/Maya-Basics/files/GUID-092344A4-0BFB-4C1A-8C10-1017AC92BBC4-htm.html
            编辑_LOD_GenerateLODMesh
### 骨骼动画：
            https://youtu.be/1wvdQy2Fdhw
            自动绑定网站：
                  mixamo
### 绑定：
            官方：https://youtu.be/RrZbdO1zmBI?si=CcnRbJSc_IWwqvIz


### 曲线建模：
            ref：
                  https://www.youtube.com/watch?v=ngIaju1eM7s
            创建：
                  EP曲线工具：
                        在曲线上取两点创建新曲线：
                              C + 鼠标左键 分别在不同线段执行两次

            编辑：
                  EP曲线工具：
                        选择曲线：
                              右键：
                                    控制顶点
                        选择线段的位置：
                              右键：
                                    曲线点
                        分割曲线：
                              选择线段的位置 + 空格——曲线——分离
                        对其两条曲线点：
                              空格——曲线——重建曲线设置：
                                    保持原始(勾选)

            输出：
                  形成面：
                        二条曲线段生成几何面：
                              1.空格——曲面——双轨成型
                              2.设置双轨成型选项：
                                    输出几何体：
                                          多边形
                                    类型：
                                          四边形
                                    细分方法：
                                          常规
                                    UV类型：
                                          每个跨度的等参线数
                                    U向数量:
                                          1
                                    V向数量:
                                          1
                              3.根据提示：
                                    选择两条剖面曲线
                                    选择两条轨道曲线
                              4.在形成的面 通道盒/层级编辑器中设置：
                                    输入：
                                          nurbsTessellate2:
                                                UV数量设置
                  多面合并：
                        多边形建模：
                        结合
                        合并：基于临近点合并
### 用方形制作球体：
            方法1：
                      创建方形
                      shift+右键：平滑
                      通道盒子调整输入  
                      变形——雕刻
                      选中曲线球 放大
            方法2：
### 动力学：xgen + nhair
            思路：
                  选择面片 创建毛发系统（曲线） 或者 可以直接创建曲线
                  获得曲线后 nhair 的毛发系统转给 曲线 并且编辑参数
                  为毛发 赋予 nhair的 paint Effect 笔刷
                  再由 nhair 生成的毛发编辑曲线 转成 曲线 删除编辑过程文件
                  可以使用 扫描网格 或者 任何曲线到网格的方式 生成poly
            ref：
                  https://www.bilibili.com/video/BV1TR4y1J7Dh/
### 顶点动画：
            
                                                    

## 编辑器： 
- 动画：
            重影动画检查：
                  可视化——打开重影编辑器
                  ref:
                        https://youtu.be/2-Nn1vGLDjw
            曲线动画：
                  窗口-动画编辑器-曲线图编辑器
            融合动画(blendshape):
                  变形——融合变形
                  增删：
                        变形——编辑——融合变形
            欧拉万向锁：
                  按E+鼠标左键
                  欧拉万向锁的层级
                  原理：
                        https://www.bilibili.com/video/BV1YJ41127qe/?spm_id_from=333.788.recommend_more_video.-1
                  做动画角度最小的放中间
            
            烘培顶点动画：
                  Cache > Geometry Cache > Create new cache
            烘培动画：      
                  https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2020/ENU/Maya-Animation/files/GUID-72394768-6DD7-4676-857F-B0468509CFF6-htm.html
                  混合变形：
                        
                        https://youtu.be/L2Qnuve7sY4

                        创建：
                              变形(创建)-混合变形 3个物体 
                        修改：
                              在 通道层编辑器
                        添加：
                              编辑-混合变形-添加
                        编辑：
                              窗口——动画编辑器——形变编辑器
                              
                              眉毛 胡子之类：
                                    窗口——常规编辑器——连接编辑器
- 设置相对路径贴图：
            Windows-GeneralEditor-FilePathEditor
- 晶格变形：
            变形 > (创建)晶格
            ref：
                  https://knowledge.autodesk.com/zh-hans/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2022/CHS/Maya-CharacterAnimation/files/GUID-334813CE-30E5-43CB-B032-CF8FE1ED96F7-htm.html
- 隐藏天空盒：
            在天空材质：
                  Visibility：
                        Camera:0.0
            相机里设置:
                  背景色：黑色
- 修改 正交/透视 摄像机：
            视图上方的菜单栏
- UV映射：
            UV-平面映射
- 烘焙AO：
            https://youtu.be/N14WqEovkWs
- 创建UV（无UV）：
            空格——UV——自动

- 3D绘制贴图工具：
            渲染模式：
                  3D绘制工具：
                        FileTexture：
                              承载信息的贴图设置
- 保存面板(代码)：
            saveAllShelves $gShelfTopLevel;
- 随时间自动保存：
            https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2020/ENU/Maya-ManagingScenes/files/GUID-BF903937-BAAD-4E28-ADE3-592EB0F8AAA5-htm.html#:~:text=Select%20Windows%20%3E%20Settings%2FPreferences%20%3E,Autosave%20section%2C%20turn%20on%20Enable.&text=See%20the%20Autosave%20section%20of,a%20description%20of%20these%20options.
      
- 输出法线贴图：
            右键“制定新材质”
            渲染模式——照明/着色——传递贴图—
            选择：目标网格——lowPoly
            选择：源网格——highPoly
            检查：
                  在“目标网格”菜单下 显示设为 封套
                  选择映射的大小
            输出：
                  传递贴图输出贴图 法线
            ref：
                  https://download.autodesk.com/us/maya/Maya_2012_GettingStarted_CS/index.html?url=files/GUID-267CCDE6-5697-4235-9728-805879FEBA2-244.htm,topicNumber=d28e26772

- 检查IBL顶点色：
            阿诺德创建天空盒子
            设置灯光为：使用所有灯光
            创建材质球 AiStandSurface 粗糙度和光滑度调到最大
            修改顶点法线方向来优化

- 复制顶点法线：
            网格-镜像，反转法线
            局部空间
            烘培轴枢
            网格-传递属性
      
- 绘制顶点色：

            ref：
                  https://vimeo.com/98373299
            绘制：
                  网格显示——绘制顶点色工具
            显示：
                  工具设置——显示
                  网格显示——切换显示颜色属性
            批量：
                  网格显示——应用颜色
                  ref：
                        https://knowledge.autodesk.com/zh-hans/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2020/CHS/Maya-Modeling/files/GUID-73C39180-81D8-4709-9756-FB7DB0A5E31B-htm.html
            问题：
                  切线不正确：
                        取消导出的fbx网格三角化
                  ref：
                        https://youtu.be/bANGwlye9nM

            复杂的模型可以用属性传递顶点色
- 烘培顶点法线：
            网格-传递属性
- 合并曲线：
            曲线-吸附曲线

- 曲线转化曲面：
            绘制曲线 —— 曲面>倒角 —— 

- 显示属性：
            Display-->Heads Up Display-->Poly Count


- 批量合并顶点：
            编辑网格-> 合并:设置阈值


- 预览多重采样抗锯齿：
            操作面板——渲染器——viewport2.0——抗锯齿

- 穿透选择：
            只选择正面：
                  TAB+鼠标左键
            基于相机选择：
                  Ctrl + Shift + Right Click 
                  _ Select
                  _ Camera_Based Selection

- 模型显示黑色线框无法选中：

            1.未分组
                   ctrl + A 调出该网格的属性编辑器 添加到组别
            2.设置成模板
                  对象显示-模板
- maya依存关系图：
            打开“Hypergraph”（“窗口 > 常规编辑器 > Hypergraph: 连接”(Window > General Editors > Hypergraph: Connections)）。
            选择“Hypergraph”中的以下菜单项之一：
            选择“图表 > 输入和输出连接”(Graph > Input and Output Connections)，以显示引向节点的输入链和从节点引出的输出链。
            选择“图表 > 输入连接”(Graph > Input Connections)，以显示引向节点的输入链。
            选择“图表 > 输出连接”(Graph > Output Connections)，以显示从节点引出的输出链。
      
- 删除融合目标节点：
            动画模式下——变形——编辑——融合变形——移除
- 曲线建模：
            maya2022：
                  https://youtu.be/6RIHSfW5x2Q
- 替换mesh：
            替换：
                  选择节点/节点们 选择 目标 mesh：
                        修改：
                              替换对象
            取消替换：
                  修改——转化——实例到对象

- 随机：
            编辑网格-变换组件-随机

## VRay渲染器：
      简介：
            V-ray离线渲染光追技术，多CPU，GPU 分布式渲染，多插件兼容，可编程着色器渲染管线
            wiki：
                  1997年发行，发行商：美国 Chaos Group
      设置安装：
            盗版：
            启用：
                  windows-settingPreferece-Plugin manager:
                        vray for maya. mll
                        vray volum grid.mll
                        xgen vray.mll
                  render Setting:
                        render Useing:
                              vray
                        
      Coding:
            命令：
            SDK：
            Python：
                  offical reference：
                        https://docs.chaosgroup.com/display/VMAYA/Python+Access+to+the+Translated+V-Ray+Scene
      渲染设置：
            Common:
                  Image file output:
                        file name prefix:
                              输出图片名称
                        Version Label:
                        Image Format:
                              图片格式
                  Animation：
                        渲染动画
                  RenderableCamera：
                        目标相机
                  Resolution:
                        品质
                  
            VRay:
                  ProductionRenderer：
                        选择cpu or gpu ( CUDA 英伟达架构 )
                  V-Ray render devices：
                        渲染硬件选择
                  Image sampler：
                        图片采样
                  
                  
            GI(全局光照):

                  
            Settings:
            Overrides:
                  Camera：
                        运动模糊
                  Environment:
                        环境映射
            RenderElement（渲染管线）:
                  
            IPR（实时渲染）:

      工具栏：
            VRay:
                  V-RayFrameBuffer:
                        实时预览渲染
      视口快捷键：
            Start/Stop V-Ray Viewport IPR：
                  开启实时渲染
      属性栏：
            AttributeEditor:
                  调整灯光属性
## 阿诺德渲染器：
      简介：
            官方：是一款先进的 Monte Carlo 光线追踪渲染器，专为满足长篇动画和视觉效果的需求而构建。
            做什么：
                  管理项目：
                        在 CPU 和 GPU 渲染之间无缝切换，以满足您对角色、风景和照明的制作需求，并在渲染头发、毛皮和皮肤等元素时获得优化的性能。
                  集成和定制您的着色器：
                        轻松将 Arnold 与所有顶级 3D 数字内容创建工具集成，获得开放和可扩展架构的可扩展性和自定义，并创建自定义着色器。
            受众：
                  Arnold 被电影、电视、游戏和设计可视化行业的 3D 建模师、动画师、灯光艺术家和 FX 艺术家使用。
     
      应用:
            只有ai的材质球才能在阿诺德渲染器中显示半透明度            
      编程：
            编写语言：C++
            特征：
                  它的光线追踪引擎经过优化，可在由几何图元（包括多边形、头发样条线和体积）组成的 3D 场景中发送数十亿条空间不相干的光线。
                  集成了OpenImageIO库
                  具有完全可编程的API，并使用用 C++ 或开放着色语言编写的着色器定义材料和纹理。
            范例：
                  https://area.autodesk.com/m/leegriggs/tutorials/creating-a-shader
      历史：
            Arnold由 Marcos Fajardo 在西班牙创建，2021 年全球计算机图形学中使用最广泛的逼真渲染系统之一
## Mel:
      API:
            Ref: 
                  https://help.autodesk.com/cloudhelp/2019/ENU/Maya-Tech-Docs/Commands/
                  https://help.autodesk.com/view/MAYAUL/2017/CHS/
            filterExpand 选择遮罩：
                  https://help.autodesk.com/cloudhelp/2016/ENU/Maya-Tech-Docs/Commands/filterExpand.html

            faceVertexNoraml:
                  http://ewertb.mayasound.com/mel/mel.005.php

            polyListComponentConversion:
                  类型转换

            listAttr：
                  显示所有属性：
                        https://help.autodesk.com/cloudhelp/2017/ENU/Maya-Tech-Docs/Commands/listAttr.html
## 通用：
      剔除背面选择：
            显示 - 多边形 - 背面消隐
            https://youtu.be/eAxjKUAibII?si=OzDQHraGtP1VU_iN
      great youtube：
            中文：
                  https://www.youtube.com/channel/UC8uSIqyCFzO3gvOPgfhyouQ
## 报错：
      The plug-in has detected mesh nodes with unsupported operators that affect the vertex and/or face count. To correct this, delete the Non-deformer history before exporting. The following nodes will not be processed：
      解决方法：
            在绑定之前，应该始终删除网格上的历史记录
