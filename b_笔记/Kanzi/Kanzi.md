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

+ 目录结构：
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
    - Engine
      - 
    - Examples
    - Templates
    - Tutorials
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
---
## 学习日志

- 2021/6/21:
  - 简单研究了软件历史
  - 软件组成
  - KanziStudio的目录结构
  - 回顾了OpenGL ES各版本的功能与特性
  
- 2021/6/22:
  - 上午看了目录结构 Studio/ Engin/ C盘 的消息报错信息，
  - 激活软件，尝试将gl文件转"二进制着色器" 
  - 下午新人入职培训
