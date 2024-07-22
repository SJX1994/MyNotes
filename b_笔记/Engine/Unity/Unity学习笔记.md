
# 主程之路：
## 架构：
- siki学院：框架搭建 决定版：架构演化__凉鞋
- 
# 杂
文件结构：
    参考：https://abaojin.github.io/2018/03/11/unity-project/
    官方解释：https://learn.unity.com/tutorial/project-architecture-unity-project-folder-structure?uv=4.x#5c7f8528edbc2a002053b672
    源码管理：
        Assets:
            腾讯整理：
                https://gameinstitute.qq.com/community/detail/129050
            特殊文件夹：
                Editor：
                    无论在根目录或者子目录，该文件下的脚本只能在编辑器中运行。
                Editor Default Resources：
                    （中间空格不能省略）必须放在根目录，用于存放编辑器需要用到的图片文本等资源。（可以通过EditorGUIUtility.Load读取）
                Gizmos：
                    Gizmos.DrawIcon(transform.position,"0.png",true);调用的0.png就是这个文件夹下的文件
                Plugins：
                    做平台开发（ios，安卓，小米...）所依赖的sdk放在这里，打包的时候会把这个文件夹中的sdks一起打包
                Resources：
                    无论在根目录或者子目录，会把文件夹下的资源直接打包，可以读写，会被压缩，Resource.Load直接读取这个路径下的文件
                StreamingAssets：
                    这个文件夹会被打包，只能读取，不会压缩。
                .(隐藏文件)：
                    以.开头的不会被导入到引擎中，不会打包
                WebPlayerTemplates：
                    不会被编译，存放网页相关文件
                Standard Assets：
                    导入标准材质包时创建，由自己的编译顺序
            编译顺序：
                1.运行时依赖的脚本：
                    Standard Assets Pro/Standard Assets/Plugins/MonoBehaviour/ScriptableObject...
                2.编译器中的文件：
                    1.1.Editor中的和Editor相关的文件
                    1.2.Editor之外的脚本
                    1.3.Editor之中的脚本
        ProjectSettings：
           存储所有项目设置（物理，标签，播放器...）表示在编辑器中：“编辑”->“项目设置”的所有内容
        
    从源码管理中产生的：
        Packages：
        Library：
            版本控制时，本地缓存应当完全忽略
        Temp：
            临时文件，可以删除，给MonoDevelop使用
        obj：
            临时文件，可以删除，给Unity使用
        代码文件：
            Assembly-CSharp-vs.csproj & Assembly-CSharp.csproj：
                是由VisualStudio和MonoDevelop产生的文件
            Assembly-CSharp-vs.csproj & Assembly-CSharp.csproj:
                是为JavaScript准备的

MonoDevelop：
    概念：
        https://en.wikipedia.org/wiki/MonoDevelop
        是一个开源的集成开发环境。使用Mono和.net框架(详见windows笔记)

功能模块：
    Shader：
        API:
            Category {}:
                分类是如下任意命令的逻辑组。大多数情况下是被用于继承渲染状态。例如，你的着色器可以有多个子着色器，他们都需要关闭雾效果，附加的混合，等等。
            Fallback "name"
                退回到给定名称的着色器
            Fallback Off
                显式声明没有fallback并且不会打印任何警告，甚至没有子着色器会被运行
        HLSL：
            文档：
                官方：
                    https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.4/manual/index.html
                知乎：
                    https://zhuanlan.zhihu.com/p/84908168
        操作：
            修改颜色和sprite：
                https://dev.twsiyuan.com/2017/10/unity-custom-sprite-shader.html
            透明：
                https://www.jianshu.com/p/bdd56b627356
            贴花：
                github.com/ColinLeung-NiloCat/UnityURPUnlitScreenSpaceDecalShader
                https://www.lfzxb.top/screen-space-decal-in-urp-study/
            遮挡显示：
                https://youtu.be/szsWx9IQVDI
    自带:
        LocalVolumetricFog:
            增加：Sky and Fog Global Volume 添加 Fog 开启 VolumetricFog
        LightingMap:
            ref:https://forum.unity.com/threads/generate-lightmap-uvs-for-editor-combined-mesh.495158/
        Presets(预设)：
            保存：
                Inspector:
                    右上角第一个：
                        左下角SaveCurrtenTo
            读取：
                Inspector:
                    右上角第一个
                        选择
        显示线框：
        particle:
            粒子系统参照：
                ref：
                    https://www.bilibili.com/read/cv3942583/
        内建图标:
            https://www.cnblogs.com/CloudLiu/p/9957335.html
        terrain:
        Burst:
            是一个编译器，与 Unity job system 搭配使用。它可以将代码(LI/.NET)编译成本地代码，通过 LLVM compiler 在多核 CPU 上运行。Burst 会自动检测代码中的循环，并将其转换为 SIMD 指令。这样，就可以在多核 CPU 上并行执行循环。
            文档：  
                https://docs.unity3d.com/Packages/com.unity.burst@1.8/manual/index.html

    插件：
        terrain:
            拓展方案：
                1.编辑模式更加醒目 _完成 待集成
                2.曲面微分使得UV更加平均（方便Terrain绘制）_解决 使用terrain的 paint terrain 模式下的 smooth height
                2.1:自定义terrainshader：
                    https://youtu.be/F_jP3_FKOEE
                    2.1.1:Triplanar Mapping 解决贴图拉伸:
                        ref:
                            https://www.youtube.com/watch?v=sjpszGetM40
                            https://medium.com/@Tgerm/unity-triplanarmapping-11354000a1ac
                3.贝塞尔曲线生成物体
                    ref：
                    https://www.youtube.com/watch?v=sLqXFF8mlEU&t
                    https://www.youtube.com/watch?v=saAQNRSYU9k
                    挑战：导入模型的弯曲
                    3.1 普通物体
                        生成河流 
                            https://youtu.be/e8iJh1v2PyY
                        生成车轮印：
                            https://youtu.be/ThlqTMBzyjI
            当前方案:
                Atlas Terrain Editor:
                    基于：贴图，贝塞尔，进行编辑 在 unity 自带的 terrain 上进行编辑
                    功能结构（编辑器）:
                        AtlasUnityPreviewVolume （入口）支持多地形
                        Stamps（高度 贴图 编辑器）互相之间可以影响，独立Stamps可以绘制，可以编组
                        Spline Stamp (样条线编辑工具) 可生成mesh
                        scatter（散布工具）
        obi Cloth:
            基本设置：
                核心物理引擎：
                    Burst：性能最好，依赖：Burst Collections Mathematics Jobs
                        观察性能：
                            前置操作：
                                1.启用 Burst 编译： jobs > Burst > Enable Compilation
                                2.禁用 LeakDetection： jobs > Burst > Leak Detection> off
                            观察：
                                您可以使用 Unity 的内置分析器。在时间线模式下，您会看到所有工作线程在物理更新调用期间都处于忙碌状态
                                
                    Oni ：C++11 编写的本地库，针对每个平台进行了预编译。配合：ObiProfiler组件，游戏模式下将分析数据
                渲染管线：
                    无关管线与着色器只编辑顶点。
            架构：
                被更新器 更新的 解算器 解算 蓝图 得到 演员。 所有的 演员 是 解算器 子集。多个演员可以共享 蓝图 的数据（像shader 和 matiral 之间的关系）。

                解算器（ObiSolver）：
                    最重要的组件
                    公开一些全局模拟参数，例如重力、惯性比例或速度阻尼
                    特性：
                        可以添加到场景中的任何游戏对象，并且可以有多个解算器在同一场景中同时工作。
                        每个 actor 都需要在其层次结构中有一个求解器，以便进行更新和渲染。
                        每个求解器都完全独立于其他求解器。因此，由不同求解器更新的 actors 不会相互交互/碰撞。只有同一个求解器中的参与者才会相互反应。
                        求解器总是在局部空间中执行模拟。这意味着平移、旋转或缩放求解器将强制更新整个模拟。
                    四个模块：
                        SovleSettings:
                            BackEnd: 设置求解器的内部工作方式 官方支持 Burst 和 Oni
                            Mode：2D 3D
                            Interpollation: 插值:影响物理效果的真实程度（适合慢动作效果），不设置性能最好，其他设置会带来一帧的延迟，如果两个Obi之间有交互，需要设置相同的 插值

                        SimulationSettings:
                            关于外力/内力的各种全局参数，如重力、阻尼、惯性等
                            GravitySpace：局部计算（重力方向会随着局部旋转而旋转） 还是在 世界计算 // 好像反了
                            Gravity:重力方向和大小，在求解器的局部空间才会表示。
                            SleepThreshold:和性能无关 慢动作 或者 冻住效果
                            Damping:阻尼（高阻尼值可用于模拟水下效果。）
                            WorldLinerInertiaScale:世界坐标惯性
                            WorldAngularInertiaScale:离心力和科里奥利
                            MaxAnisotropy:各向异性 用于流体
                            SimulationWhenInvisible:如果需要随时更新，当场景有多个解算器并始终不可见时，禁用可以提高性能
                        CollisionSettings:
                            ContinuousCollisionDetection（CCD）:连续碰撞检测
                                每一个碰撞粒子的碰撞检测范围，0为纯静态检测，1为完全连续的碰撞检测
                            CollectionsMargin:碰撞检测的边距
                                拓展CCD的边界，较大的值会产生更多的接触，可以提高复杂碰撞的鲁棒性，但会降低性能。
                            MaxDepenetration:最大反弹，带出对撞机的最大速度（以米/秒为单位）
                            ShockPropagation:冲击传播，增加粒子的质量，有助于位置碰撞的稳定性
                            SurfaceCollisionIterations:
                                表面碰撞迭代次数，
                                为细化表面碰撞而执行的最大迭代次数。
                                表面碰撞会在碰撞粒子之间产生碰撞表面（防止穿过粒子之间的间隙）
                                对性能会产生负面影响，但如果需要像绳索这样高密度的表面碰撞，启用表面碰撞 好过增加粒子密度 来实现
                                原理：
                                    表面碰撞是对碰撞点之间建面/线来实现
                                    后端不知道碰撞是什么，只关心图形
                                    表面会生成一个碰撞点，然后用迭代算法（Frank-Wolfe算法）找到 碰撞面 上 的碰撞点在哪里
                            SurfaceCollisionTolerance:表面碰撞容差
                                与平坦表面更稳定的表面碰撞。
                        ConstraintsSettings:
                            允许求解器完全跳过与该特定约束类型相关的任何计算。允许求解器完全跳过与该特定约束类型相关的任何计算。以减少开销，或为了表现增加开销。
                            每个约束类型有四个参数：
                                Enable
                                    默认是全部打开的
                                Evaluation
                                    如何执行此类约束
                                     Sequential mode：
                                        根据 Gauss-Seidel 算法 产生 在性能低预算情况使用，采用局部顺序模式迭代响应
                                     Parallel mode：
                                        根据 Jacobi-like solving 算法 产生 在性能高预算情况使用，采用全局并行模式迭代响应
                                Iterations
                                    迭代次数 高迭代次数将使模拟更接近真实，但会降低性能
                                Relaxation
                                    松弛因子
                                    越高性能越好，但仿真的稳定性会降低。
                更新器（ObiUpdater）：
                    使它们与 Unity 自己的物理引擎保持同步。需要精确控制模拟更新周期，您可以编写自己的更新程序。
                    只有一个更新程序。未被任何更新程序引用的求解器将不会更新其模拟。
                    演员特定约束类型的 计算方式为 更新器 的全局更新子步长 * 约束类型的迭代次数。
                    更新类型：
                        ObiFixedUpdater：
                                固定更新程序将在 FixedUpdate() 期间更新模拟。这是最正确的方法，也是您应该在大多数时间使用的方法。
                            Solvers:
                                在此更新器中追踪的求解器列表。
                            Substeps：
                                更新器可以将每个物理步骤分成多个更小的子步骤。
                                例如：Unity 的固定时间步设置为0.02并且更新器中的子步数设置为4，则每个子步将推进模拟 0.02/4 = 0.005秒。
                                越小检测精度越低性能越好
                        ObiLateFixedUpdater：
                                FixedUpdate()执行完毕，Update Physics 的 Animator 更新完毕后执行。
                                通常用于角色服装或任何由角色动画驱动的演员的更新程序。只需确保您的角色的 Animator 设置为 Update Physics！
                        ObiLateUpdater:
                                固定频率更新模拟时才使用它。有时对低质量的角色服装或不需要太多物理精度的辅助视觉效果很有用。
                蓝图（ObiActorBlueprint）：
                    输入一个 mesh：非流形网格（ non-manifold mesh） 作为 内部数据 去生成 衣服。
                    输入的 mesh 必须是紧密 无孔的。每条边必须有两个相邻的面。可以使用 ClothProxies工具 来生成。
                    一个数据容器：
                        蓝图是从网格（ObiCloth）生成的。
                    Generate：
                        1.生成一个 已经合并的mesh的顶点 的列表，无视UV和法线接缝。
                        2.生成一个 已经合并的三角边 和 边界边 的列表。
                        3.为 每一个顶点（之前保存的顶点列表） 约束粒子点。 为 每一条边（之前保存的边列表） 距离约束。
                        4.分析出 最终的 约束网络 生成 一个 碰撞网、体积、空气动力约束。
                        5.为高性能运行时 整理数据 到 批次中。完成后 就可以 进入编辑模式（Edit）。
                    三大编辑模块：
                        Particle selection
                            选择和编辑：
                                鼠标左键选择粒子
                                shift + 鼠标左键 删除粒子
                                BrushSize：笔刷大小
                                particle culling：粒子剔除(方便编辑)
                                    off,front,back
                                快捷工具（方便编辑粒子）：
                                    反选，制空，删除选定区域不会产生影响的粒子，删除，还原。
                                Properties:
                                    编辑当前选择粒子的属性
                                Particle groups: // 待拓展 需要更明显的提示
                                    可以创建特殊意义的粒子组。例如，一组粒子用于悬挂在领一个物体上。也可以在运行时访问。
                                Tethers: // 系绳约束
                                    选择一个组，生成系绳约束，将会吧 组内的粒子 和所有其他 所有粒子 建立系绳约束。
                                    （在renderModes：Tethers constraints 然后在演员中进行设置）
                        Property painting
                            在 renderMode启用mesh后：
                                笔刷有 内径 和 外径 的差值设置。同时 笔刷可以镜像绘制
                        Texture import/export
                            可以加载外部纹理来映射权重。
                            可以导出纹理来保存权重。padding值用来控制纹理边缘出血的像素。
                        RenderModes：
                演员（ObiActor）：
                    被更新器 更新的 解算器 解算 蓝图 得到 演员。 所有的 演员 是 求解器的 子集。多个演员可以共享 蓝图 的数据（像shader 和 matiral 之间的关系）。
                    所有演员的输入是 蓝图。为了让 解算器 成功解算，演员 会 实例化蓝图信息（粒子和约束）。
                    蓝图 的数据集 可以被 多个演员 重用。
                    演员 必须 是解算器的子物体才可以被解算。在运行时 可以以动态分配子物体的方式 将 演员 分配到不同的解算器下。
                    创建:
                        1.创建一个 适当类型的蓝图。（被更新器 更新的 解算器 解算 蓝图 得到 演员。 所有的 演员 是 求解器的 子集。多个演员可以共享 蓝图 的数据（像shader 和 matiral 之间的关系）。）生成并编辑它。  
                        2.创建一个 演员。 并喂 蓝图 给他。（GameObj > 3DObj > Obi > Obi Cloth 创建完成 演员 会自动寻找一个 解算器 成为它的子物体/归入子集。 如果找不到 解算器 就创建一个。创建的 解算器 会自动加入场景中 更新器 的列表。如果 找不到 更新器 会创建一个）
                    属性：
                        衣服演员是由 一堆粒子组成的二维平面，粒子通过 距离和约束 互相连接。
                        三种口味：
                            常规衣服：
                                不可被撕裂。可以用于绑定动画的网格。性能是最好的。
                            撕裂衣服：
                                可以被撕裂。不可以被用作动画绑定的网格。撕裂衣服需要 额外的内存 和 运行时开销。
                            皮肤布料：
                                用于骨骼动画。不可撕裂。
                            
                    演员的特性：
                        生成方式：演员 要求 解算器 根据蓝图的需要分配尽可能多的 碰撞粒子。 多个演员 由 同一个 求解器 从蓝图 分配粒子
                        交互方式：演员 所有在蓝图中能找到的约束类型 并且 更新他们 的粒子索引。所以 演员 可以 正确指向 另一个解算器中的 演员。
                        更新方式：所有演员 的 激活粒子都在 解算器 中被 更新器 更新。
                    模拟：
                        性能预算/开销由解算器给出：
                        Distance constraints：
                            固定粒子组之间的距离约束，表现为布料的轻重，弹性（纱制 或 油布制）。
                        Bend constraints：
                            所有粒子之间的弯曲约束。表现为表面的软硬（更多细节褶皱）
                        Volume constraints：
                        Aerodynamic constraints
                        Tether constraints 
            工具：
                ClothProxies工具：
                    为ObiCloth 蓝图 创建 非流形网格（ non-manifold mesh）
                    使用条件：
                        带有 ObiCloth 和 ObiClothProxy 组件的游戏物体。
                        导入 mesh 必须unity中开启可读写
                    创建：
                        ObiCloth 设置正确的情况下 需要指定目标 mesh
                    属性：
                        Particle proxy
                            需要运行proxy的目标 ObiCloth
                        Skin map
                            利用 Triangle Skin map 指定权重 
                        Target topolgy 
                            目标网格（已弃用，所有信息都在SkinMap中）
                    tips:
                        1.mesh可以创建多个Proxy被使用。
                        2.既可以使用 mesh 也可以 使用 proxy。
                Obi particle attachment：
                    附着于游戏物体上
                Obi particle render：
                    渲染碰撞粒子的体积
                Obi Instanced particle render：
                    渲染碰撞粒子生成物体
                Obi Ambient Force Zone：
                    针对解算器创建全局风力场
                Obi Collider：
                    为解算器计算unity碰撞
                    每一个碰撞体都需要单独的unity碰撞
            第三方：
                测试模型：
                    
            原理：  
            tips：
                关于缩放:
                    解算器 的缩放会缩放整个模型的顶点，重力相对的坐标如果是 解算器的本地坐标 重力也会随着解算器旋转而改变
                    演员 的缩放会缩放打组的粒子
                碰撞厚度：
                    从 Obi Collider 渲染
        MagicaCloth：
            下载：
                CSDN：
                    50元
                官方：
                    25美元一位
        
        shaderGraph：
        TimeLine:
            https://www.youtube.com/watch?v=xw4WJ1z2bjQ
        spine: 
            下载：  
                http://zh.esotericsoftware.com/spine-unity-download
            TimeLine:
                https://www.youtube.com/watch?v=V2z6FZxIrb0
            
版本控制：
    包管理器:
        https://docs.unity3d.com/2021.3/Documentation/Manual/upm-scoped.html
    gitignore：
        https://learn.unity.com/tutorial/project-architecture-unity-project-folder-structure?uv=4.x#5c7f8528edbc2a002053b672

Adia任务：
    unity炸弹人需求：
        \\192.168.10.3\Producer_New\GA_Producer\Incoming\Japan\KDE\FUSE\B00\测试用
    unityXD需求：
        \\192.168.10.3\Producer_New\GA_Producer\Incoming\China\XD\XDXZ\B01\3D道具\New\XDXZ_LookDev
        file://192.168.10.3/Producer_New/GA_Producer/Incoming/China/XD/XDXZ/B01/3D道具/New/物件制作规范.pdf

优化：
    合并mesh减少drawCall插件：
        MeshBacker：
            https://assetstore.unity.com/packages/tools/modeling/mesh-baker-5017
            贴图模糊解决方案：
                1.增大子合并的最大尺寸
                2.提高UV利用率
                3.禁用缩放
                4.减小分批合并的颗粒度
            
    粒子特效:
        原理：
            参考：https://blog.csdn.net/alla_Candy/article/details/103709925
            梳理：
                
        方案：
            参考：https://www.cnblogs.com/cnxkey/articles/9743920.html

    粒子系统消耗检查工具：
        https://networm.me/2019/07/28/unity-particle-effect-profiler/
    优化关键：
        帧率及时间增量：
            实时游戏的帧率(fram rate)是指一连串三维帧以多快的速度向观众显示。
            帧率的单位为赫兹(Hertz,Hz),即每秒的周期数量。
            帧率通常以每秒帧数(fram per second,FPS)来度量。
            两帧之间所经过的时间称为 帧时间(frame time)。或者时间增量(time delta)。
            (ref:游戏引擎架构JasonGregory)
            unity瓶颈分析：
                https://blog.unity.com/technology/detecting-performance-bottlenecks-with-unity-frame-timing-manager
                项目位置：
                    // 编辑器
                        D:\Projects\YM\sjzj\Project\Unity\Library\PackageCache\com.unity.render-pipelines.core@12.1.7\Editor\Debugging
                    // 运行时
                        D:\Projects\YM\sjzj\Project\Unity\Library\PackageCache\com.unity.render-pipelines.core@12.1.7\Runtime\Debugging
                入口：
                    编辑器：Window/Analysis/Rendering Debugger
                    运行时：Ctrl + BackSpace
                入口函数：
                    编辑器: DebugWindow.cs
                编辑器指南：
                    https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@14.0/manual/Render-Pipeline-Debug-Window.html#DecalsPanel
    VirtualTexture:
        https://github.com/jintiao/VirtualTexture
        浅谈Virtual Texture - 李兵的文章 - 知乎
        https://zhuanlan.zhihu.com/p/138484024
    MipmapStreaming：   
        浅谈Unity纹理串流系统Mipmap Streaming System - 欧几里得范数的文章 - 知乎
        https://zhuanlan.zhihu.com/p/600257663
    显存占用优化：
        工具：memory profiler
        关联关键字：
            Reflection Probe
            Render Texture
    官方HDRP优化：
        https://unity.com/how-to/performance-optimization-high-end-graphics
插件：
    Cloth-and-IK-Test：
        https://github.com/SebLague/Cloth-and-IK-Test.git
    粒子特效性能测试：
        https://github.com/sunbrando/ParticleEffectProfiler
    复制粘贴：
        https://www.jianshu.com/p/68680a67ff0e
    texutrePackerGUI：
        https://www.codeandweb.com/texturepacker/tutorials/using-spritesheets-with-unity
    减包体大小分割Texture2D：
        https://www.codenong.com/cs105490621/
    编译dll：
        https://blog.csdn.net/Fenglele_Fans/article/details/82694333
    时间控制time：
        Ludiq.Chronos
渲染管线：
    几何着色器ComputeShader：
        关键字：
            RWTexture2D<类型>:
                概念:
                    此存储类会导致内存同步在整个 GPU 上刷新数据，以便其他组可以看到写入。
                操作：
                    GetDimensions：
                        //获取纹理大小信息。
            numthreads：
                概念：
                    着色器所需要申请的线程大小
                三维向量：
                    int a,b,c
                    [numthreads(a,b,c)] 
                二位向量：
                    int a,b
                    [numthreads(a,b)] 
            unit:
                32 位无符号整数（范围：0 到 4294967295 十进制）
    片元：
        关键字：
            Uniform：
                统一是使用“统一”存储限定符声明的全局着色器变量。
    LWRP:
        着色器架构：
            目标平台着色器平台：
                2.0：
                    全平台

            前向渲染 ForwardLit：
                顶点片元
            阴影渲染 ShadowCaster：
                顶点
            深度渲染 DepthOnly：
                顶点
            全局信息 Meta：
                顶点片元
        安装：
            PackageManager：
                LightweightRenderPipeline
        自定义渲染管线（黑客行为）：
            https://youtu.be/3TULxrZCAdM
        HLSL:
            高精准度： float
            中等精度： half
            低精度： fixed
            整数（int数据类型）
            复合向量/矩阵类型:
                float3
                float4x4
        unity GI （Global Illumination）:
            全局光照：
                光线相互之间的弹射
            阴影存在：
                ShadowCaster
                在C#中存的全局变量
        特定tage渲染：
            https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.0/manual/renderer-features/how-to-custom-effect-render-objects.html
        后处理：
            https://youtu.be/bkPe1hxOmbI
            https://youtu.be/Ts2F2SxeRSY
            设置：
                安装：
                    PackageManager：
                        PostProcess
                目标相机挂载：
                    PostProcessLayer:
                        设置layer
                空物体挂载：
                    PostProcessVolume:
                启用：
                    Graphics：
                        找到渲染管线：
                            启用HDR
    HDRP:
        启用：
            2020之后：
                https://www.youtube.com/watch?v=ad9f_nKU0ZA
            崩溃处理办法：
                更改显卡设置，命令行启动
        文件结构:
            CoreRPLibrary/ShaderLibrary:

                保存了大量的工具函数：光照计算工具函数，随机数计算，矩阵工具，坐标转换工具，风场，ParallaxOcclusionMapping等。

            CoreRPLibrary/ShaderLibrary/API:

                保存了跨平台函数的定义。

            HDRP/Runtime/Material:

                保存了HDRP中默认支持的材质Shader：Lit，LayeredLit，Stacklit等，都是和各自材质相关的计算。不同的材质中包含了不同的BSDF函数的实现、不同的BuiltinData的组织方式。

            HDRP/Runtime/PostProcessing/Shaders:

                后处理的Shader，HDRP中后处理全部使用ComputeShader。

            HDRP/Runtime/RenderPipeline/ShaderPass:

            ShaderPass的定义：包括了Vertex和Fragment程序的定义。

            HDRP/Runtime/RenderPipeline/ShaderLibrary:

                从C#当中设置的Shader参数，包括：各种变换矩阵、获取矩阵的函数、摄像机参数、场景参数、全部buffer、全局纹理、shader控制参数等。
        核心技术：
            TAA：
                temporalAntialiasing：时效性抗锯齿,逐屏幕像素实时采样,进行延迟渲染
        着色器架构： 
            目标平台着色器平台：
                4.5：
                    DirectX 11 功能级别 11+
                    OpenGL 4.3+
                    OpenGL ES 3.1
                    Vulkan
                    Metal
            子着色器1：
                ScenePickingPass：
                    允许编辑器正确处理镶嵌对象和背面对象的拾取  
                SceneSelectionPass:
                    用于渲染对象的通道是ForwardOnly,选择通道着色器用于渲染一个带有对象 id 的缓冲区，稍后在场景视图中使用该对象 id 绘制轮廓
                Gbuffer:
                    通用概念：
                        用于存储有关几何体的不同位信息，例如世界法线、基色、粗糙度等。 Unreal 在光照时对这些缓冲区进行采样计算以确定最终的阴影。
                    HDRP概念：
                        可以存储您想要的任何类型的数据，编写一个自定义 SRP，它可以让您完全控制 GBuffer 布局和格式。
                        目前unity默认只开放4种数据通道：其中空闲的通道只有rt2
                meta：
                    计入全局缓存，用于物体之间的正确计算间接照明
                ShadowCaster：
                    用于将对象渲染到阴影贴图中
                DepthOnly：
                    渲染到深度缓冲区
                MotionVectors：
                    渲染每一个运动的物体，计算每像素运动向量，使用深度纹理来剔除不用计算的向量，避免过多占用算力
                DistortionVectors：
                    失去真向量，仅适用于透明对象
                (以下决定了光追和阴影透明度的正确性，渲染顺序必须按照如下排列)
                TransparentDepthPrepass：
                    将透明表面的多边形添加到深度缓冲区以改进其排序
                TransparentBackface：
                    为了正确的光追而存在，得在前向渲染之前处理才有效
                Forward：
                    根据影响对象的灯光，在一个或多个通道中渲染每个对象。
                TransparentDepthPostpass：
                    透明物体Postpass深度
                RayTracingPrepass:
                    递归渲染依赖这个pass，投射折射和反射光线，这个pass决定了光线可以撞击表面反射的次数
                FullScreenDebug：
                    可以单个输出所有pass的工作状况
            子着色器2：
                关于光追踪对RTX显卡的适配：
                    IndirectDXR
                    ForwardDXR
                    GBufferDXR
                    VisibilityDXR
                    SubSurfaceDXR
                    PathTracingDXR
        渲染管线架构：       
            光线追踪：
        HDRP文档笔记：
            通道纹理打包：
                unity蒙版图Mask map：
                    导入格式：
                        线性色彩空间
                    通道规则：
                        R：金属的
                        G：遮挡AO
                        B：细节遮罩
                        A：平滑度
                unity细节图Detail map：
                    通道规则：
                        R：去色图
                        G：法线 Y
                        B：平滑度
                        A：法线 X

            Alpha Clipping
            半透明剪裁
                "不渲染"alpha值低于0.1的像素

            Alpha Output
            半透明输出选项
                R11G11B10
                    默认渲染方式，最小带宽的颜色缓冲格式 
                    如果后处理格式为这个 将不处理任何 R16G16B16A16(带有alpha通道的屏幕像素)
                    例如：
                    深度变焦后处理
                    如果不启用R16G16B16A16物体将边缘锐利内部模糊
                    TAA(时间抗锯齿)不开启R16G16B16A16会产生边缘模糊的情况
                R16G16B16A16
                    所有alpha通道的 颜色缓冲格式

            Ambient occlusion
            环境光遮蔽：
                不反射光照的遮罩，可以采用形如：xNormal，Substance Designer or Painter，Knald 来制作。
                在2019版本中的unity 延迟渲染光照pass 会影响 自发光， 前向渲染pass 不会影响
                Lit使用：
                    环境光遮蔽在 MaskMap的绿色通道里读取：
                    赋值后：
                       Ambient Occlusion Remapping：
                        通过插值调整范围 
            Atmospheric Scattering
            大气散射：
                现象：
                    大气散射是当悬浮在大气中的粒子向各个方向漫射（或散射）通过它们的一部分光时发生的现象。
                    导致大气散射的自然效果示例包括雾、云或薄雾。
                实现：
                    高清晰度渲染管线 (HDRP) 通过将颜色叠加到对象上来模拟雾化效果，具体取决于对象与相机的距离。
                    可以隐藏远处游戏对象的相机远平面剪裁，这在您减少相机的远剪裁平面以提高性能
                    雾的密度 与 相机的距离 与 世界空间高度 呈正比。所有材质都与雾产生反应，
                    雾可以使用 天空盒子 作为颜色来源。采样 天空盒子的 mipmap, 这样可以非常便宜的做出体积雾的效果
            Anti-Aliasing
            抗锯齿：
                    锯齿：
                        数字采样器试图采样真实世界并尝试数字化时 发生的偏差。
                        例如：
                            音频采样 
                                采用：Nyquist rate 奈奎斯特率 的方法进行 完全描述给定信号的最小采样率，但对于图像计算量太大 -  ![](Img/Nyquist rate.jpg)（类似拟合？）
                    unity的方法：
                        快速近似抗锯齿 (FXAA)。
                            在每个像素边缘平滑,最终图像会略微模糊，不适合大量镜面反射的场景
                            这是HDRP最廉价的后处理抗锯齿

                        时间抗锯齿 (TAA)。
                            适用于缓慢运动物体的边缘，需要开启unity适量运动
                            TAA是缓存了 帧历史缓冲区，所以当物体快速移动时 历史缓冲区的刷新 跟不上 速度就会出现重影。

                        亚像素形态抗锯齿 (SMAA)。
                            在图像的边界中找到图案 混合边界的像素
                            适合平面卡通或干净的艺术风格

                        多样本抗锯齿 (MSAA)。
                            对像素内的多位置采样，并结合样本
                            一种硬件抗锯齿，可以结合其他效果使用（除了TAA，因为MSAA不支持矢量运动效果采样）

--- 

- Look-Up-Table
- 显示查找表：
    - 可以接受所有PS里做的处理作为后处理输入到实时渲染引擎
    - 定义：
        - 将一种颜色空间映射到另一种颜色空间
        - 3D LUT，它与立体3D图像无关，它指向的是色彩空间的3维分布矩阵。
    - opengl实现：
        - https://zhuanlan.zhihu.com/p/43241990
    - 可以用Ps之类的软件映射到unity
    - reference:
    - https://lettier.github.io/3d-game-shaders-for-beginners/lookup-table.html
        - LUT 着色器是调整游戏外观的便捷工具。比如：从颜色分级到将白天变成夜晚
        - ![alt text](image.png)
        - LUT 需要为 256 像素宽 x 16 像素高，并包含 16 个块，每个块为 16 x 16 像素。
    - https://github.com/lettier/3d-game-shaders-for-beginners/blob/master/demonstration/shaders/fragment/lookup-table.frag

--- 

            AxF Shader
            AxF材质：
                Appearance eXchange Format
                X-Rite 公司开发的一款 数字孪生物理世界材质
                通过扫描真实材质 模拟 各向异性 扩散 法线 粗糙度 高光颜色等
            
            Building-For-Consoles
            为不同终端配置：
                PlayStation 4: com.unity.render-pipelines.ps4
                PlayStation 5: com.unity.render-pipelines.ps5
                Xbox One: com.unity.render-pipelines.xboxone
                Game Core Xbox Series: com.unity.render-pipelines.gamecore
                Game Core Xbox One: com.unity.render-pipelines.gamecore

            Camera-relative rendering
            相机关联渲染：
                以相机为原点。
                为了解决：
                    浮点数的绝对精度随着数字变大而降低。游戏世界空间的原点离原点越远浮点数越不精确，比如：z-fight的重影。相机相关渲染将世界原点替换为相机的位置
                对shader的影响:
                    如果启用：
                        `GetAbsolutePositionWS(PositionInputs.positionWS)` 返回非相机相关的世界空间位置。
                        `GetAbsolutePositionWS(float3(0, 0, 0))` 返回等于
                        `_WorldSpaceCameraPos` 的相机的世界空间位置。
                        `GetCameraRelativePositionWS(_WorldSpaceCameraPos)` 返回 `float3(0, 0, 0)`
                    如果禁用：
                        `GetAbsolutePositionWS()` 和 `GetCameraRelativePositionWS()` 返回传递给它们的位置

            Creating-a-Custom-Sky
            自定义天空C#+HlSL:
                C#创建实际渲染天空的类，将其渲染为用于照明的立方体贴图或用于背景的视觉效果。 这是您必须实现特定渲染功能的地方。
                为天空实际创建着色器。 此着色器的内容取决于您要包含的效果。(例如： HDRI 图,一种360图)

            CustomPass:
            注入着色器：
                概念：
                    在渲染循环内的某些点注入shader 和 C#
                    您能够绘制对象、执行全屏通道并读取一些相机缓冲区，如深度、颜色或法线。
                    注入通过volumes（包裹盒）系统实现,
                    可以渐变，由包裹盒实心与空心区分
                    全局 包裹盒 优先于 本地 包裹盒
                6个unity暴露的注入点（从上到下触发）：

                    BeforeRendering
                        可以取到的缓冲区：
                            Depth (Write)
                        描述：
                            可以写入深度缓冲，可以用遮罩来做深度缓冲

                    AfterOpaqueDepthAndNormal
                        缓冲区：
                            Depth (Read | Write), Normal + roughness (Read | Write)
                        描述：
                            您可以使用 DecodeFromNormalBuffer 和 EncodeIntoNormalBuffer 函数来读取/写入法线和粗糙度数据。

                    BeforePreRefraction
                        缓冲区：
                            Color (no pyramid | Read | Write), Depth (Read | Write), Normal + roughness (Read)
                        描述：
                            缓冲区将包含所有不透明对象以及天空。
                    BeforeTransparent
                        缓冲区：
                            Color (Pyramid | Read | Write), Depth (Read | Write), Normal + roughness (Read)
                        描述：
                            在这里，可以对我们用于透明折射的颜色进行采样。您还可以在此处绘制一些需要折射场景的透明对象（例如水）。
                    BeforePostProcess
                        缓冲区：
                            Color (Pyramid | Read | Write), Depth (Read | Write), Normal + roughness (Read)
                        描述：
                            渲染所有HDR
                    AfterPostProcess
                        缓冲区：
                            Color(Read | Write), Depth (Read)
                        描述：
                            深度是动态的
                    ![FullScreenCustomPass_Inspector](IMG/FullScreenCustomPass_Inspector.png)
                具体操作：
                    添加 Custom Pass
                    注入shander代码：
                        Create/Shader/HDRP/Custom Renderers Pass.
                        可引入变量（不够可以黑客）
                            #define ATTRIBUTES_NEED_NORMAL
                            #define ATTRIBUTES_NEED_TANGENT
                            #define ATTRIBUTES_NEED_TEXCOORD0
                            #define ATTRIBUTES_NEED_TEXCOORD1
                            #define ATTRIBUTES_NEED_TEXCOORD2
                            #define ATTRIBUTES_NEED_TEXCOORD3
                            #define ATTRIBUTES_NEED_COLOR

                            #define VARYINGS_NEED_POSITION_WS
                            #define VARYINGS_NEED_TANGENT_TO_WORLD
                            #define VARYINGS_NEED_TEXCOORD0
                            #define VARYINGS_NEED_TEXCOORD1
                            #define VARYINGS_NEED_TEXCOORD2
                            #define VARYINGS_NEED_TEXCOORD3
                            #define VARYINGS_NEED_COLOR
                            #define VARYINGS_NEED_CULLFACE
                        顶点
                            AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
                        片元
                             void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 viewDirection, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
                    可编程渲染管线：
                        可以使用C#
                        或者ShaderGraph
        REF:
            自定义 HDRP shader code：
                http://www.erinz.xyz/index.php/archives/316/
                https://catlikecoding.com/unity/tutorials/scriptable-render-pipeline/custom-shaders/
                https://forum.unity.com/threads/finally-got-working-stencil-on-hdrp-lit-with-light-shadows-see-an-objects-through-other-objects.1194103/
                https://www.redowlgames.nl/2017/01/18/shader-selftuition-stencil-testing-in-unity/
                https://github.com/alelievr/HDRP-Custom-Passes.git
            1.
                https://www.youtube.com/watch?v=ad9f_nKU0ZA
            2.
                https://liangz0707.github.io/whoimi/blogs/HDRPsource/new2.HDRPShader%E5%BC%80%E5%8F%91%E8%AF%B4%E6%98%8E.html          

            3.
                ($Unity工程)Library\PackageCache\com.unity.render-pipelines.high-definition@7.7.1\Documentation~

案例：
    dijkstras寻路：
        https://github.com/hasanbayatme/unity-dijkstras-pathfinding
    UMP包嵌入工程（黑客行为）：
        https://github.com/liortal53/upm-embed
    UI粒子特效：
        https://www.youtube.com/watch?v=Y5WVxdhEiIg&t=694s
    安卓打包:
        https://zhuanlan.zhihu.com/p/113007406
    SpineShaderURP描边：
        https://github.com/chavaloart/urp-multi-pass
        +
        chayan
    HDRP可交互水体：
        https://assetstore.unity.com/packages/2d/textures-materials/water/simple-interactive-water-162033
    程式动画：
        Animancer
    室外照明和场景设置：
        环境照明：
    截图工具：
        https://github.com/Team-on/UnityScreenShooter        
    加载时间Debug：
        https://docs.unity3d.com/Manual/profiler-profiling-applications.html
        检查内存使用情况：
            https://docs.unity3d.com/Packages/com.unity.memoryprofiler@1.0/manual/index.html
        UI优化：
            https://gwb.tencent.com/community/detail/116185
    TPMS中文字体库：
        动态字体：
            https://youtu.be/NY1xKqCIj3c?si=eVOa-pOpK5nkTtqm
        静态字体:
            https://github.com/kaienfr/Font/tree/master/learnfiles
            https://www.cnblogs.com/anderson0/p/16130186.html
    VAT(vertex animation texture):
        https://www.youtube.com/playlist?list=PLXNFA1EysfYn686NxzYbKxm845eOIwDPA
        Maya:https://github.com/ZGeng/VertAnimToTex
        概念：
            将网格动画烘培到一张贴图上，
            这张贴图会在实时渲染中 给  vert shader 使用
        具体实现：
            贴图：纵轴是帧数，横轴是顶点数
    100个实用小框架：
        https://www.bilibili.com/read/cv10038189/
        数学小知识：
            https://github.com/zalo/MathUtilities
    世界空间下Tileable 贴图坐标：
        https://youtu.be/vIh_6xtBwsI
    命名规范：
        文件：
            https://github.com/stillwwater/UnityStyleGuide
        代码：
            https://github.com/raywenderlich/c-sharp-style-guide

    性能测试：
        https://assetstore.unity.com/packages/tools/gui/graphy-ultimate-fps-counter-stats-monitor-debugger-105778
        
    几何着色器（计算着色器）：
        https://youtu.be/EB5HiqDl7VE
    raymatching：
        云雾：
            https://github.com/SebLague/Clouds
        曼德尔球分形 Mandelbulb Fractals：
            https://github.com/SebLague/Ray-Marching
    c# 通过FBX——SDK导出：
        // https://docs.unity3d.com/Packages/com.unity.formats.fbx@2.0/manual/devguide.html
        // https://stackoverflow.com/questions/70110730/using-a-c-sharp-script-export-a-gameobject-as-an-fbx-during-runtime

    通过几何着色器（computer shader） 保存gpu中的模型缓存中的信息到本地 （适用于各种过程动画）：
        // https://gist.github.com/NedMakesGames/fef1bddca8061b21845dbe68d0cddfec

    获取麦克风：
        https://youtu.be/dzD0qP8viLw

    获取游戏时实时数据：
        https://github.com/Tayx94/graphy

    小规模合作对战联网：
        https://blog.unity.com/cn/games/the-8-factors-of-multiplayer-gamedev-in-small-scale-cooperative-games-ft-breakwaters
        案例：
            bossRoom：https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop
    Unity Built-in转URP速查表:
        https://cuihongzhi1991.github.io/blog/2020/05/27/builtinttourp/
    时间控制插件：
        https://ludiq.io/chronos/tutorial/adding-a-clock
    畸变 Distortion ：
        HDRP：
            https://youtu.be/NbXvpy3x4-I
报错：
    VScode加载速度慢：
        https://forum.unity.com/threads/drastically-slower-visual-studio-code-intellisense-in-entities-0-50.1254999/
    运行加载时间长：Reload Script Assemblies
        Project Settings > Editor > Enter Play Mode Settings you can turn off Domain Reload.
    无法确定预设文件位置类型 /Users/ myUserName /Library/Preferences/Unity/Editor-5.x/Presets/Default UnityEngine.GUIUtility:ProcessEvent(Int32, IntPtr)：
        转到您的注册表，Computer\HKEY_CURRENT_USER\Software\Unity Technologies\Unity Editor 5.x，并打开所有包含“CurrentLib”的键（例如我机器上的particleCurvesCurrentLib_h1743720187），然后将二进制字符串编辑为指向正确的路径。
        ref：
            https://answers.unity.com/questions/1566475/how-to-reset-unity-user-preferences.html

    unityHub下载过于缓慢：
        在C盘找到日志：
            %APPDATA%\UnityHub\logs\info-log.json
        可以看到Unity3d 2019.1.14f1的下载地址是：
            https://download.unitychina.cn/download_unity/148b5891095a/Windows64EditorInstaller/UnitySetup64-2019.1.14f1.exe
    
    后台不跑：
        File - BuildSettings - PlayerSettings - Resolution and Presentation - Run in Background
    
    代码无法跳转：
        ctrl+shift+P运行 OmniSharp: Select Project
    
    HDRP打包报错：
        https://learn.unity.com/tutorial/upgrading-an-existing-project-to-use-hdrp-2019-2#5fd514c3edbc2a7538494a67
    locked Library/ArtifactDB:
        解决方法：打开任务管理器，关闭所有Unity相关的后台程序，重新打开项目即可。
wedding项目：
    1.呼吸动画
        -完成
    2.互动动画
        -完成
    3.事件融合动画
    4.弹幕互动位置绑定
    5.弹幕逻辑
    6.弹幕特效
    7.背景着色器
    8.开发者UI
        -完成
    9.接入收音

2022630官方技术分享：
    三星SDK:
        Adaptive Performance:
            调节游戏性能，温控管理
            framdebug?
    URP:
        MRTK
        管线转换器 Converter
        housing
        figma