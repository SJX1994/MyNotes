# kanzi学习笔记

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
    + 共享文件（%ProgramData%\Rightware）：
    
      - Kanzi
        * 注册码 激活码
        
      - Kanzi+对应版本号
        * 配置各种编程语言的环境变量
      
      - KanziStudio的首选项
        - %AppData%\Rightware\<KanziVersion>\userPreferences.xml
        
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
      + Projet：
        + 节点控制面板
          + 创建和修改内容：
            + 创建：
              + 所有提供的节点：
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
                  + Toggle Button
                  + Toggle Button Group 3D
                  + Toggle Button Group 2D
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
      + Prefab：
        + 预制体，构造并统一创建接口
      + Properties:
        + 节点属性编辑
          + 编辑
            + Add Properties window
              + （在面板右下角）添加其他属性
      + NodeComponents：
        + 节点组件：
          + 触发器
            + 通用
              + WirteLog
                + Macros(指令宏)
          + 动画
      + Preview:
        + 预览
        + 分析
          + Borders of 2D nodes
            + 显示所有2D物体的线框
      + Library:
        + 创建和编辑资源(动画，笔刷，材质，纹理，渲染通道)
      + Dictionaries:
        + 创建固定类型的键值对
      + Pages：
        + 节点式控制程序流
        + 操作
          + 点击“+”号添加页面
          + Space + 鼠标左 平移 / 鼠标中键
          + Shift + Alt + 鼠标左 缩放 / 鼠标滚轮
      + Assets：
        + Assets 从工程中导入的资产
      + StateTools:
        + 动画管理器
    
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
