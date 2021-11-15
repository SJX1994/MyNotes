# kanzi 3.6.13学习笔记
激活码：3404-9800-5938-7033(一年)
+ 文档
  + 着色器：
    + file:///D:/softWare/Kanzi/Documentation/zh-cn/Content/Working%20with/Shaders/Editing%20shaders.htm#Shader2
    
  + JS脚本：
    + file:///D:/softWare/Kanzi/Documentation/zh-cn/Content/Working%20with/Scripts/Reference%20for%20scripting.htm

  + 节点信息：
    + file:///D:/softWare/Kanzi/Documentation/zh-cn/Default.htm#Reference/Property%20types%20reference.htm%3FTocPath%3D%25E5%258F%2582%25E8%2580%2583%7C_____17
  
+ kanzi软件历史
  - 母公司：
    * Rightware:
    * `芬兰`的软件开发公司，为`汽车和其他嵌入式行业`提供`用户界面`
    * 总部在`芬兰赫尔辛基`在硅谷、底特律、东京、首尔、上海、伦敦、慕尼黑设有办事处
    * 2015年 被 ` Deloitte Touche Tohmatsu ` 评为最快增长的 `50家增长最快的` 科技企业之一
    * 2015年被 `创达收购`
    
+ 软件组成：
  - Kanzi Studio
    * 基于pc的实时编辑器，用于创建和定制嵌入式硬件上运行的用户界面
  - Kanzi Engine
    * 一个IDE 使UI可以在任何支持`OpenGL ES`的设备上运行
  - Kanzi Lite
    * 无需独立`GPU`和`OpenGL ES`功能,简化版IDE
  - Kanzi Performance Analyzer
    * 用于`汽车硬件`和`软件平台`的`性能测量`工具包，提供可用`汽车平台`性能的报告和洞察

+ 软件目录结构：
  + Kanzi Studio：
    -  Asset Library
       *  包含`字体`和`材质`
       *  和一些`.kzproj`文件
    -  Bin
       *  `Kanzi Build Environment` 工具，以安装构建和部署您的 Kanzi 应用程序到`不同的平台`所需的软件。
       *  Kanzi Command Prompt 用于`构建 Kanzi 工程`   
  + Kanzi Engin：
    - 3rdPartySDKs
      - 构建到不同平台需要的软件。
        - 安装适用于 Android 的 Kanzi 构建环境
          - 安卓
            - 必须安装环境：
              - Android Studio 4.1.0 & NDK 21.3.6528147
                - 下载路径:https://developer.android.com/studio/archive
              - Java Development Environment Kit 1.8 或更高版本（64 位版本）
              - CMake 3.5.1 或更新版本
              - 适用于您 Android 设备的 USB 设备驱动程序
            - 详细安装步骤见：会随Kanzi安装哪些内容
            
        - 部署 Kanzi 应用程序
          - 可以支持的平台
            - Android (ARM)
            - Linux X11 GLX
            - Win32
            
          - 通过沟通支持的平台
            - Android (x86, x86_64)
            - Linux (Vivante fbdev, WSEGL, Wayland, DRM GBM)
            - INTEGRITY (Vivante fbdev, WSEGL, Renesas WM, GHS FB, GHS GBM)
            - QNX
            - Nucleus (Vivante fbdev, AXSB)
            
    - Assets
      - 存放 `二进制着色器`，转换`二进制着色器的方法`:
        - 文档位置：最佳实践_着色器_使用二进制着色器
    - Documentation
      - 包含Kanzi文档
    - Engine
      - Applications
        - KZB播放器的源码+自定义工具（插件的源码存放）
      - configs
        - 目录包含使用 Kanzi Engine 构建工程所需的全部配置文件。
        - 这包括 SCons 脚本（makeFile的一种）
        - Visual Studio 工程和解决方案文件。
      - headers
        - 目录包含使用 Kanzi Engine 所需的全部头文件（.h 和 .hpp）
      - lib
        - 目录包含适用于每个平台的 Kanzi Engine 二进制素材库
      - libraries
        - 目录包含 Kanzi Engine 所需的第三方素材库。
      - Scripts
        - 目录包含适用于构建您选定平台的 Kanzi 应用程序的脚本。
    - Examples
      - 示例工程
    - Templates
      - 起点模板
    - Tutorials
      - 教程资产
  + C盘：
    + 共享文件（%ProgramData%Rightware）：
    
      - Kanzi
        * 注册码 激活码
        
      - Kanzi+对应版本号
        * 配置各种编程语言的环境变量
      
      - KanziStudio的首选项
        - %AppData%Rightware<KanziVersion>userPreferences.xml
        
    + 私有文件：
      - KanziStudioLogs
        - KanziStudio.log
          - KanziStudio 的消息
        - KanziPreview.log
          - Kanzi Studio 预览 (Preview) 的消息
          - 在 Kanzi Studio > bin > KanziPreview.exe.config 中修改 logLevel 键值对
            - Normal:错误和异常
            - VERBOSE:预览后以毫秒级别的精度记录(时刻调用写入会降低编译器性能)
            - NONE:不记录
          
        - KanziThumbnails.log
          - KanziStudio绘制缩略图的进程消息
          - 
        - KanziStudio_crash<n>.log
          - KanziStudio意外中止的报错


+ 工作流：
  + 基本：
    + 界面设计模式：MVVM（Model-View-ViewMode）
      + View
        + 是用户在屏幕上看到的结构、布局和外观（UI）
      + ViewModel
        + 把View层需要的数据暴露出来，并对View层的数据绑定，事件绑定负责。MVVM没有MVC模式的控制器，也没有MVP模式的presenter，有的是一个绑定器。在ViewModel中，绑定器在视图和 数据绑定器之间进行通信。
      + Model
        + 是指代表真实状态内容的领域模型（面向对象），或指代表内容的数据访问层（以 数据为中心）DataSource。
  + 创建：
    + 位置：
      + 更新相对路径：
        + <ProjectName>/Application/configs/platforms 目录中的 Kanzi Engine
    + 模式：
      + Application：
        + 概念：
          + 基于KanziEngin 使用 KanziEngin API 的逻辑
        + 目录：
          + Application
            + 目录含 Visual Studio 解决方案和模板源代码的 C ++ 应用程序配置。
          + Tool_project
            + Kanzi Studio工程目录
              + .kzproj
                + 工程文件,XML格式
                + （TODO）后期可以尝试用python解析，达成一些批量操作
              + .kzproj_1/2/3/4...
                + 自动备份文件
              + .lock
                + 打开工程时自动生成的锁定文件，防止打开一个工程的多个副本
              + 其他文件夹
                + 素材
                  + 网格数据 (Mesh Data) 和动画 (Animations)
                    + 只能由 Kanzi Studio 使用，如果使用源控制系统，请记住只要您导入网格或在 Kanzi Studio 工程中创建新的动画，就得将这些文件添加到源控制。
                  + 源资产 (Source Assets)
                    + 无法从 Kanzi Studio 访问 源资产 (Source Assets) 目录
                + 还原点(RestorePoints)
                  + 存取办法：
                    + 文件 (File) > 保存还原点 (Save Restore Point)。
      + KanziStudio：
        + 基本的UI界面操作
      + plugin
        + 使用插件
          + ..
      + data source
        + 使用元数据
          + ..
    + 材质：
      + Fast performance vertex shaders
        + 快速顶点着色器
      + High quality fragment shaders
        + 高品质片元着色器
    + 针对工程的其他操作：
      + 配置工程
        + 方法：工程 (Project) > 属性 (Properties)。
        + 属性：（todo）
          + Description
          + Application Settings
          + Binary Export
          + Building
          + Default Assets
          + Kanzi Connect
          + Preview
          + Profile
          + Required Renderer Features
          + Resources
          + Timeline
  + 工作：
    + 常用面板：
      + 工程选项卡：
        + showInPreview
          + 在预览窗口显示工程
      + Projet：
        + 节点控制面板
          + 创建和修改内容：
            + 创建：
              + 所有提供的节点：
                + Screen
                  + Window：
                    + 调节输出屏幕大小
                + RootPage：
                  + PageHost:
                    + DefaultSubpage:
                      + 默认页面
                + 内容控制节点：
                  + Image
                  + Model
                  + Nine Patch Image
                  + Page
                  + Text Block 3D
                  + Text Block 2D
                  + Viewport 2D
                + 交互性控制节点：
                  + Button 3D
                  + Button 2D
                  + Scroll view 3D
                  + Scroll view 2D
                  + Slider 3D
                  + Slider 2D
                  + Toggle button 3D
                  + Toggle button 2D
                    + 2D切换按钮
                  + Toggle Button
                  + Toggle Button Group 3D
                  + Toggle Button Group 2D
                    + 2D切换按钮组
                  + Toggle Button Group
                + 布局控制节点：
                  + Dock Layout
                    + 自动对齐，支持缩放
                  + Empty Node
                    + 空节点
                  + Flow Layout
                    + 自动对齐，不支持缩放
                  + Grid Layout
                    + 按照网格排列，不支持缩放
                  + Stack Layout
                    + 依次排列
                  + Trajectory Layout
                    + 路径排布
                + 容器控件节点：
                  + Grid List Box
                    + 滚动列表
                  + Trajectory List Box 3D
                    + 沿轨迹滚动列表
                + 3D节点：
                  + Camera
                  + Instantiator
                  + Level of Detail
                  + Light
                    + Directional Light
                    + Point Light
                    + Spot Light
                    + Scene
                      + 用于展示3D内容
                + 3D kanzi 物体
                  + Plane
                + 工程操作：
                  + PrefabPlaceholder 2d
                    + 为当前项目创建一个2d渲染框，可以渲染其他子项目
              + Alt+右键：
                + 显示所有可以创建内容
            + 修改：
              + 重命名节点：
                + F2
              + 修改层级：
                + Ctrl + ↑/↓
              + 隐藏：
                + Ctrl + H
            + 其他：
              + Project下的Screen会被加载到目标设备的内存中
      + Prefabs：
        + 预制体，构造并统一创建接口
        + UI:
          + 右下角配置预设件
        + 工程：
          + PrefabProperties
            + VisibilityAcrossProjects:
              + public 公开使用
      + Properties:
        + 节点属性编辑
          + 节点信息：
            + 见文档
          + 编辑
            + Add Properties window
              + （在面板右下角）添加其他属性
            + 每个属性右边向上的箭头
              + 变成公开变量（实例化预制体）
      + Node Components：
        + 节点组件：
          + 触发器 Trigger
            + 生命周期：
              + 输入（接收）
                + 用户输入（触摸/鼠标）
              + 输入操纵器（发送）
                + 例如手势，长按，短按，可以使用API添加更多操纵器
              + 消息（接受）
                + 消息通过隧道和冒泡进程在节点之间传递
              + 触发器（执行）
                + 触发器对消息或事件作出反应，并应用逻辑，可以使用 Kanzi Engine API 定义哪些消息会触发反应。
              + 动作
                + 触发器的结果
            + triggerSetting
          
            + 通用
              + WirteLog
                + Macros(指令宏)
              + Navigate To Nest
                + 指向下一个
              + ExecuteScript
                + 使用自定义脚本
              + Go to State
                + 指向某个状态机的状态
              + OnPropertyChange：
                + 设置一个变量当他变化执行脚本
            + 坑：
              + 看好<Relative>的绝对路径 每个.代表一个层级，第一个.代表自己，@..代表libray里的
          + 动画 Animation
            + Property Target Interpolator
              + 基于数值产生动画
      + Preview:
        + 预览子菜单
          + 快速对其：
            + 9宫格图标
          + 快速选择：
            + 右键元素
          + Restart
            + 回到面板的初始状态
        + 模式：
          + Node tool 节点工具
            + 2D：
              + 移动选定的节点
              + 旋转选定的节点
              + 缩放选定的节点
              + 对其工具
              + 水平拉伸
              + 垂直拉伸
              + 填充拉伸
              + 布局：
                + 通过xy布局变换
                  + 受到布局影响
                + 通过xy渲染变换
                  + 不受布局影响
                + 通过xy做拉伸，缩放
              + 坐标吸附。
            + 3D：
              + 使用世界坐标定位
              + 使用本地坐标定位
              
          + Grid Layout 2D tool 2D 网格布局工具
            + 创建一个布局宽度
            
          + Text Block 2D tool 2D 文本块工具
            + 快速创建2D文本块
          + Stack Layout 2D tool 2D 堆栈布局工具
            + 快速创建一个堆栈布局
          + Flow Layout 2D tool 2D 流布局工具
            + 快速创建一个流布局
          + Camera 摄像机
            + 存储当前相机位置
            + 重置当前相机位置
            + 创建一个新相机
            + 相机控制模式：
              + 围绕查看
              + 自由移动
            + 选择相机
            + 设置视野
        + 分析
          + Borders of 2D nodes
            + 显示所有2D物体的线框
      + Library:
        + 创建和编辑资源(动画，笔刷，材质，纹理，渲染通道)
          + Trajectories
            + 实例化各种形态的滑轨
          + Styles：
            + 创建模板
              + 通过节点PrefabView来实现
          + Themes：
            + 用于承载Styles的变化
            + ThemeEditor
              + CreatTheme 添加一个主题
          + StateManagers
            + 可以批量设置按钮状态
          + PropertyTypes
            + 可以批量创建属性
              + enum
                + 储存无限多的键值对
          + Animations
            + AnimationClip
              + 双击编辑
            + TimelineSeqence
              + 
          + Materials and Tetxures
            + 材质球
              + ColorBrush 颜色笔刷
                + 单色填充2D节点，可以应用背景颜色
              + Texture Brush 纹理笔刷
                + 纹理填充2D节点
              + Material Brush 材质笔刷
                + 材质填充2D节点，将TypeMat填入
          + ProjectReference:
            + 项目导入/合并 引用其他工程的资源
          + Rendering
            + ObjectSources
              + TagFilper
                + 收集被分配标签的节点
          + Localization
            + 本地化翻译
          + 编码
            + Material Type
              + VertexShader
                + 直接编辑GLSL顶点代码
                + 案例：
                  + 深度+贴图：
                  
                    + v：
                          attribute vec3 kzPosition;
                          attribute vec2 kzTextureCoordinate0;
                          uniform highp mat4 kzProjectionCameraWorldMatrix;
                          uniform mediump vec2 TextureOffset;
                          uniform mediump vec2 TextureTiling;

                          varying mediump vec2 vTexCoord;

                          void main()
                          {
                              precision mediump float;
                              
                              vTexCoord = kzTextureCoordinate0*TextureTiling + TextureOffset;
                              gl_Position = kzProjectionCameraWorldMatrix * vec4(kzPosition.xyz, 1.0);
                          }

                          
                    + f：
                    
                        uniform sampler2D Texturesjx;
                        uniform lowp float BlendIntensity123;
                        varying mediump vec2 vTexCoord;
                        uniform mediump vec4 _Diffuse;
                        uniform mediump vec4 _OutlineColor;
                        uniform lowp float _OutlineScale;


                        void main()
                        {
                            precision lowp float;
                            
                            float near = 0.1;
                            float far = 50.0;
                            float z = gl_FragCoord.z * 2.0 - 1.0; // Back to NDC
                            float m =  (2.0 * near) / (far + near - z * (far - near));
                            
                            vec4 color = texture2D(Texturesjx, vTexCoord);
                            
                            vec4 Z = vec4( vec3(m)*12.0,1.0);
                            
                            vec4 zz = vec4(1.0)-pow(Z,vec4(1.0));
                            
                            gl_FragColor.rgba = color.rgba;
                            gl_FragColor.rgba = zz ;
                            gl_FragColor.rgba = color.rgba + zz ;
                            
                            
                            
                        }

              + FragmentShader
              + MaterialTypeMaterial
            + Resource Files:
            + Scripts:
              + 存放JS脚本（使用 Google 的 V8 JavaScript 引擎）
              + 操作：
                + 注释：
                  + Ctrl+K+C
                + 取消注释
                  + Ctrl+K+U
      + Dictionaries:
        + 创建固定类型的键值对
        + 在哪个次级目录下 就通过哪个节点访问
      + Pages：
        + 节点式控制程序流
        + 操作
          + 点击“+”号添加页面
          + Space + 鼠标左 平移 / 鼠标中键
          + Shift + Alt + 鼠标左 缩放 / 鼠标滚轮
        + coverToPageHost:
          + page 和 host 的区别在于 host 可以管理子pages
        + Transition:
          + 进入页面的动画/方式
          + 拖动到转换面板上
        + keepActive
          + 始终保持在最上层
      + Assets：
        + Assets 从工程中导入的资产
      + StateTools:
        + 状态机
          + 子顶栏：
            + 选择触发开关
          + 设置状态
            + 选中具体Project中的物体后，设置状态
          + 设置转换 State Transition Editor
            + 左边对状态做简单设置
              + 开始时间 运动方式...
            + 右键添加状态
            + StateTransitionEditor
              + Add Transition Animation:
                + 添加自定义动画
      + DataSources:
        + 存放源数据
      + UserPreference：
        + Advanced：
          + UpdatePreview when ..not focused
            + 实时更新当外部发生修改时
          + OpenBuildEnviromentConfig
            + 打开环境配置页面
    + c++程序：
      + 先在studio操作，完了导出.kzb,等待visualStudio编辑
        + 字典：
          + project 右键 Alias，再在编译器通过 字符串 调用修改
      + 打开 visual Studio 2017 打开 ..ProjectsHelloWorldApplicationconfigsplatformswin32,（详细配置见文档）平台工具集选2015 v140，2015 debug模式运行
+ 总结

  + view 
    + UI
      + Project
      + Preview
      + Pages
      
  + view model:
    + 绑定器
      + Properties
      + Node Components
      + State Tools
      
      
  + Model：
    + 数据中心：
      + Library
      + Dictionaries
      + Prefabs
      + Assets

错误：
  创建工程卡住:
    从任务管理器详细进程杀掉和preview相关的进程
  缺少dll：kzjs kzui：
    从engin目录拷贝到发布目录
  黑屏：
    找不到kzb拷贝到发布目录
---
## 学习日志

<!-- todo：教程-KS使用入门-4.创建交互 -->

  - 2021/6/21:
    - 简单研究了软件历史
    - 软件组成
    - KanziStudio的目录结构
    - 回顾了OpenGL ES各版本的功能与特性
    
  - 2021/6/22:
    - 上午看了目录结构 Studio/ Engin/ C盘 的消息报错信息，
    - 激活软件，尝试将gl文件转"二进制着色器" 
    - 下午新人入职培训

  - 2021/6/23:
    - 针对`项目工程`的 目录结构，文件作用，配置信息，崩溃措施 简单了解
    - 在Studio中 尝试了`Project面板`下所有可用的节点

  - 2021/6/24:
    
    - 教程:
      - Kanzi Studio 使用入门（初级）
        - 第 1 步 - 创建工程
        - 第 2 步 - 创建和修改内容
        - 第 3 步 - 创建用户界面结构
        - 第 4 步 - 创建交互
        - 第 5 步 - 创建和使用资源
        - 第 6 步 - 添加应用程序状态
      - 在 Kanzi Studio 中创建应用程序（初级）
        - 第 1 步 - 创建新工程并导入资产
        - 第 2 步 - 创建应用程序结构
        - 第 3 步 - 创建在页面 (Page) 节点之间导航的控件
        
    - 其他
      - 尝试写了简单的 glsl
      - 尝试写了简单的 js  
      - 简单解析.kzproj 的XML结构

  - 2021/6/24:
    - 教程:
      - 在 Kanzi Studio 中创建应用程序（初级）
        - 第 3 步 - 创建在页面 (Page) 节点之间导航的控件
        - 第 4 步 - 创建Car 页面的状态
        - 第 5 步 - 创建Media 页面的内容

    - 其他：
      - 练习状态机的转换动画

  - 2021/6/28:
    - 教程:
      - 在 Kanzi Studio 中创建应用程序（初级）
        - 第 6 步 - 部署应用程序
      - 材质和纹理（初级）
        - 第 1 步 - 导入内容并创建背景
        - 第 2 步 - 创建车身的材质类型和纹理
        - 第 3 步 - 设置轮胎、轮毂和镀铬零部件的材质
        - 第 4 步 - 定义车窗和大灯玻璃的材质属性
        - 第 5 步 - 调整材质
      - Hello World! (API)
        - 第 1 步 - 创建含 C++ 应用程序的工程并打印到调试控制台
        - 第 2 步 - 使用 Kanzi Engine API 设置属性值
      - 应用程序逻辑 (API)
        - 第 1 步 - 使用 Kanzi Engine API 加载 Kanzi 二进制文件
        - 第 2 步 - 访问 Kanzi Studio 工程中创建的内容
        - 第 3 步 - 创建预设件实例并从文件系统加载图像
        - 第 4 步 - 创建选择小组件的交互处理程序

  - 2021/6/29:
    - 教程:
      - 应用程序逻辑 (API)
        - 第 6 步 - 构建应用程序并将其部署到 Android 设备
    - 其他：
      - 配置安卓打包环境
      - 温故C++语法

  - 2021/6/30:
    - 教程:
      - 应用程序逻辑 (API)
        - 第 6 步 - 构建应用程序并将其部署到 Android 设备
          - 环境配置后，报 cstdint: No such file or directory 的错误（基本库找不到，不知道什么原因）和 谢琳琳 沟通协助没有解决，所以暂缓
      - 组合工程 (高级)
      - 使用 JavaScript 设置状态（初级）
      - 含页面节点的应用程序流（初级）
        - 第 1 步 - 在页面 (Pages) 窗口创建应用程序结构
        - 第 2 步 - 创建应用程序导航
   
  - 2021/7/1:
    - 教程:
      - 含页面节点的应用程序流（初级）
        - 第 3 步 - 向应用程序添加内容
        - 第 4 步 - 创建弹出窗口
      - 关键帧动画（初级）
        - 第 1 步 - 创建动画
        - 第 2 步 - 微调动画
      - 状态机（初级）
        - 第 1 步 - 创建状态机控制应用程序状态
        - 第 2 步 - 使用 JavaScript 脚本控制应用程序状态(事件绑定有些小问题，明天详细看下)
  - 2021/7/2:
    - 教程:
      - 仪表（高级）
        - 第 1 步 - 创建速度指针(关于 Transformation Origin_旋转中心 和 布局变换 渲染变换)
        - 第 2 步 - 创建燃油和电池指针(创建一个数值控制器)
      - 组合仪表指示灯（高级）（属性控制状态机）
      - 插值属性值（高级）
        - 第 1 步 - 插值指南针表盘的旋转（关于读取外部bin中的xml，和数值属性动画）
        - 第 2 步  - 对已完成的插值作出反应（NodeComponents条件触发器）
      - 3D 模型旋转（高级）（滑动与旋转绑定）

  - 2021/7/5:
    - 教程：
      - 平移、缩放、轻敲
        - 第 1 步 - 平移地图
        - 第 2 步 - 缩放和旋转地图
        - 第 3 步 - 长按以放置别针
      - 创建滑块
      - 创建切换按钮
      - 教程：为应用程序制定主题
        - 第 1 步 - 为组合仪表创建主题

  - 2021/7/6:
    - 教程：
      - 渐进渲染 
        - 第 1 步 - 准备工程中的节点
        - 第 2 步：在多个帧上分布渲染
        - 第 3 步：调优 渐进渲染 2D 视口 (Progressive Rendering Viewport 2D) 节点渲染的内容
      - 使用网格列表框 (Grid List Box) 创建联系人列表 
        - 第 1 步 - 创建联系人列表并从数据源获取联系人
        - 第 2 步 - 更改选定项的外观
      - 创建按钮
      - 创建反射
        - 第 1 步 - 准备工程中的节点
        - 第 2 步 - 渲染反射


  - 2021/7/7:

    - 创建高斯模糊效果
      - 第 1 步 - 创建提供模糊效果的材质
      - 第 2 步 - 应用模糊效果
      - 第 3 步 - 切换模糊效果
      
    - 创建全屏泛光效果
      - 第 1 步 - 渲染汽车和大灯
      - 第 2 步 - 显示全屏泛光效果
      - 第 3 步 - 微调全屏泛光效果

    - 从数据源获取应用程序数据
      - 第 1 步 - 定义数据源
      - 第 2 步 - 使用其中的数据源Kanzi Studio
      - 第 3 步 - 定义和使用静态列表数据源




  - 2021/7/8:
    - 教程：使用 JavaScript 创建演示模式 (未复现)
      - 第 1 步 - 创建演示模式
      - 第 2 步 - 控制演示模式
      
    - 教程：本地化您的应用程序
      - 第 1 步 - 本地化文本和纹理内容
      - 第 2 步 - 为其他地区创建地区包
      - 第 3 步 - 加载地区包

    - 教程：为从右向左书写地区本地化应用程序 (部分未复现)
        - 第 1 步 - 准备从右向左书写地区的应用程序
        - 第 2 步 - 定义从右向左书写地区的应用程序布局
        - 第 3 步 - 加载地区并实例化预设件

  - 2021/7/9:

    - 拖放 (部分未复现)
      - 第 1 步 - 准备内容
      - 第 2 步 - 实现拖放功能
      - 第 3 步 - 动画化用户拖动的按钮
      
    - 使用键盘输入来导航应用程序 (部分未复现)
      - 第 1 步 - 使用键盘键在页面 (Page) 节点之间导航
      - 第 2 步 - 为Home 屏幕创建键盘导航
      - 第 3 步 - 使用键盘键在空调设置中导航
      - 第 4 步 - 使用键盘键滚动
      
    - 全部 教程 完成

  - 2021/7/12:

    - 1.尝试了在3D软件里做骨骼动画导入到Kanzi
      - 结果：
        - 支持带有蒙皮权重的骨骼动画 导入/编辑/状态机/事件 操作
        - 不支持IK控制

    - 2.尝试了 glsl 深度计算
      - 结果：
        - 对深度信息的 颜色减淡/拉大色差/叠加 的操作
        - 结合贴图信息进行渲染

  - 2021/7/13:
    - 0页的拖动逻辑
    - 切图
    
  - 2021/7/13:
    - 协助 书俊 制作模型
    - 上下翻页 + 左右滑动预览 (初步，用js方法实现，明天尝试下用 Scroll View 2D)



  - 2021/7/15:
    
    - 开始制作按钮插件
    
    - 为了方便编写插件：
      - 根据文档 结合泛亚一期的Launcher 尝试理解还原插件的类图：
    
      - 管理：
            shared_ptr(用于管理资源管理的类)：
                  
                  → makeEditorInfo（反射函数名）：
                        PropertyTypeEditorInfoSharedPtr的构造：
                        类型：
                              PropertyTypeEditorInfo：
                                    包含字典AttributeDictionary：
                                          所有插件展示信息

      - 功能：
            Module(功能模块类):
                  getMetaclassesOverride(可重写的虚函数)
            Metaclass(功能检查模块):
                  预处理：
                        KZ_METACLASS_BEGIN + KZ_METACLASS_END：
                              声明元素
                        KZ_METACLASS_PROPERTY_TYPE：
                              注册属性
                        KZ_METACLASS_MESSAGE_TYPE：
                              注册消息
                        KZ_METACLASS_METHOD：
                              注册MetaMethod
            PropertyType(实例化几乎所有函数类型):
                  比如：
                    Object(友元)
                    Node(友元)
                    friend class AbstractPropertyType
                    FixedString
                    ...


      - 传递：
            MessageArgument(消息传递的基类):
                  MessageArgumentsBaseMetaclassWrapper(结构体)：
                        包含：
                        getStaticMetaclass(自定义的一条消息)
            MethodArguments(检查消息传递)：
                  getArgument():
                        获取参数值
                  setArgument():
                        设置参数值
            MetaMethod():
                  TODO 比较复杂暂时没看懂,等具体用到填上

  - 2021/7/26:
    - 764优先
      - 侧边顶部反射
      - 红色反射（天空盒子）
      - 反射太弱
      - 完全一样
    - 707
      
- 阶段总结：

  - 教程：创建全屏泛光效果 (完成)
    - 第 1 步 - 渲染汽车和大灯
    - 第 2 步 - 显示全屏泛光效果
    - 第 3 步 - 微调全屏泛光效果

  - 教程：使用网格列表框 (Grid List Box) 创建联系人列表 (完成)
    - 第 1 步 - 创建联系人列表并从数据源获取联系人
    - 第 2 步 - 更改选定项的外观


  - 教程：创建高斯模糊效果 (完成)
    - 第 1 步 - 创建提供模糊效果的材质
    - 第 2 步 - 应用模糊效果
    - 第 3 步 - 切换模糊效果


  - 教程：创建按钮 (完成)


  - 教程：从数据源获取应用程序数据 (完成 not try)
    - 第 1 步 - 定义数据源
    - 第 2 步 - 使用其中的数据源Kanzi Studio
    - 第 3 步 - 定义和使用静态列表数据源


  - 教程：使用 JavaScript 创建演示模式 (完成 not try)
    - 第 1 步 - 创建演示模式
    - 第 2 步 - 控制演示模式


  - 教程：拖放 (完成 not try)
    - 第 1 步 - 准备内容
    - 第 2 步 - 实现拖放功能
    - 第 3 步 - 动画化用户拖动的按钮


  - 教程：让应用程序拥有动态布局 (完成)


  - 教程：创建简单的车载信息娱乐应用程序 (完成)
    - 第 1 步 - 创建新工程并导入资产
    - 第 2 步 - 创建应用程序结构
    - 第 3 步 - 创建在页面 (Page) 节点之间导航的控件
    - 第 4 步 - 创建Car 页面的状态
    - 第 5 步 - 创建Media 页面的内容
    - 第 6 步 - 部署应用程序


  - 教程：创建仪表 (完成)
    - 第 1 步 - 创建速度指针
    - 第 2 步 - 创建燃油和电池指针


  - 教程：Kanzi Studio 使用入门 (完成)
    - 第 1 步 - 创建工程
    - 第 2 步 - 创建和修改内容
    - 第 3 步 - 创建用户界面结构
    - 第 4 步 - 创建交互
    - 第 5 步 - 创建和使用资源
    - 第 6 步 - 添加应用程序状态


  - 教程：Hello world! (完成)
    - 第 1 步 - 创建含 C++ 应用程序的工程并打印到调试控制台
    - 第 2 步 - 使用 Kanzi Engine API 设置属性值


  - 教程：创建组合仪表指示灯 (完成)


  - 教程：插值属性值 (完成)
    - 第 1 步 - 插值指南针表盘的旋转
    - 第 2 步  - 对已完成的插值作出反应


  - 教程：使用键盘输入来导航应用程序 (完成 not rty)
    - 第 1 步 - 使用键盘键在页面 (Page) 节点之间导航
    - 第 2 步 - 为Home 屏幕创建键盘导航
    - 第 3 步 - 使用键盘键在空调设置中导航
    - 第 4 步 - 使用键盘键滚动


  - 教程：使用网格列表框 (Grid List Box) 创建联系人列表 (完成 not try)
    - 第 1 步 - 创建联系人列表并从数据源获取联系人
    - 第 2 步 - 更改选定项的外观


    - 异步加载和部署资源


  - 教程：本地化您的应用程序 (完成)
    - 第 1 步 - 本地化文本和纹理内容
    - 第 2 步 - 为其他地区创建地区包
    - 第 3 步 - 加载地区包


  - 教程：为从右向左书写地区本地化应用程序 (完成 not try)
    - 第 1 步 - 准备从右向左书写地区的应用程序
    - 第 2 步 - 定义从右向左书写地区的应用程序布局
    - 第 3 步 - 加载地区并实例化预设件


  - 教程：将 Kanzi Studio 工程组合到一个 Kanzi 应用程序 (完成)


  - 教程：使用页面 (Page) 节点创建应用程序流 (完成)
    - 第 1 步 - 在页面 (Pages) 窗口创建应用程序结构
    - 第 2 步 - 创建应用程序导航
    - 第 3 步 - 向应用程序添加内容
    - 第 4 步 - 创建弹出窗口


  - 教程：平移、缩放、轻敲(完成)
    - 第 1 步 - 平移地图
    - 第 2 步 - 缩放和旋转地图
    - 第 3 步 - 长按以放置别针


  - 教程：Kanzi Engine API 高级应用 (完成)
    - 第 1 步 - 使用 Kanzi Engine API 加载 Kanzi 二进制文件
    - 第 2 步 - 访问 Kanzi Studio 工程中创建的内容
    - 第 3 步 - 创建预设件实例并从文件系统加载图像
    - 第 4 步 - 创建选择小组件的交互处理程序
    - 第 5 步 - 为小组件描述面板创建交互
    - 第 6 步 - 构建应用程序并将其部署到 Android 设备


  - 教程：渐进渲染 (完成)
    - 第 1 步 - 准备工程中的节点
    - 第 2 步：在多个帧上分布渲染
    - 第 3 步：调优 渐进渲染 2D 视口 (Progressive Rendering Viewport 2D) 节点渲染的内容


  - 教程：创建反射 (完成)
    - 第 1 步 - 准备工程中的节点
    - 第 2 步 - 渲染反射


  - 教程：材质和纹理 (完成)
    - 第 1 步 - 导入内容并创建背景
    - 第 2 步 - 创建车身的材质类型和纹理
    - 第 3 步 - 设置轮胎、轮毂和镀铬零部件的材质
    - 第 4 步 - 定义车窗和大灯玻璃的材质属性
    - 第 5 步 - 调整材质


  - 教程：旋转 3D 模型 (完成)


  - 教程：使用 JavaScript 控制应用程序状态 (完成)


  - 教程：创建关键帧动画 (完成)
    - 第 1 步 - 创建动画
    - 第 2 步 - 微调动画


  - 教程：创建滑块 (完成)


  - 教程：使用状态机控制您的应用程序 (完成)
    - 第 1 步 - 创建状态机控制应用程序状态
    - 第 2 步 - 使用 JavaScript 脚本控制应用程序状态


  - 教程：应用模具至 3D 内容 (完成  not try)
    - 第 1 步 - 创建模具
    - 第 2 步 - 应用模具


  - 教程：为应用程序制定主题 (完成  not try)
    - 第 1 步 - 为组合仪表创建主题
    - 第 2 步 - 为汽车车型创建主题
    - 第 3 步 - 导出并使用 API 设置应用程序主题
  


  - 教程：创建切换按钮 (完成)
    - s

---


  
- 修改：

  - a柱子颜色调整暗 (完成) A pillar color to black/ darker
  
  - 定制化玻璃反光  (完成) - Make all windows a less transparent, and darken the tint. The goal is to make the inside headrests and high floor less visible. Maybe that inside high floor should be completely matte black./ - Windshield reflections lacking
  
  - 前窗金属贴条+金属部件的反射细节 (完成) -All chrome should reflect a live environment. If high-gloss chrome cannot have active reflections in it (reflections moving when rotating vehicle) , then we need to make all chrome not reflect dark things.  It’s particularly a problem on the chrome piece right below front grille.
  
  - 后灯灯光+后灯第二条LED调暗 (完成) - Tail light tune more red /-If there’s glow on horizontal headlight bar, then we need glow on the back horizontal taillight bar for consistency. Front glow is a bit too large, we need to reduce glow intensity a bit.
     
  - 内部车机纯黑 (完成) -Make all windows a less transparent, and darken the tint. The goal is to make the inside headrests and high floor less visible. Maybe that inside high floor should be completely matte black. / - Adjust the interior to black/ as dark as possible 
  
  - 修复部分顶点法线 (完成) - fix some vertex normal

  - 前车灯横向灯亮度调暗(完成) -If there’s glow on horizontal headlight bar, then we need glow on the back horizontal taillight bar for consistency. Front glow is a bit too large, we need to reduce glow intensity a bit.

  - 删除后车横向纹理(完成) -On the back of side window chrome, there no longer these horizontal graphics stripes. Please delete these, thanks.

  - 红色车身(完成) - Can the body color be changed to these 7 color options?  Can we see this demo with a red car Sports series (Red Carpet TC) ?