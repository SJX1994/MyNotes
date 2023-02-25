# 入职练习：3D特效
## 任务描述
- 与团队其他成员协作完成 升级特效练习。
## 练习目标
- 第一阶段：熟悉 visual effect Graph 和 particle System 系统
  1. 可以按要求修改所给素材：
     - Top Down Effects 1.0 + VFX Graph 
     - Mega Pack - Vol. 3
- 第二阶段：完成与实现
  1. 通过与 主美（张亚飞）沟通讨论视觉表现
     1. 分析并列出所需制作的内容
     2. 根据视觉元素比重 列出这些内容制作的优先级
     3. 预估制作时间
     4. 进行制作
  2. 制作完成后 收集 张亚飞 的反馈 - 预估时间 - 进行迭代
- 附加练习：了解底层
  1. 尝试每个参数 并写出该参数对效果的影响
  2. 空余时间可以尝试修改着色器的参数 来理解 着色器是如何运作的
  3. 尝试用着色器根据一张灰度图，制作一个滑动条
  4. 尝试用C#脚本控制粒子系统的播放
## 参考素材
### particle System
- 官方文档：https://docs.unity.cn/cn/2021.3/Manual/class-ParticleSystem.html
- 制作参考：https://www.bilibili.com/video/BV1ft4y1p7Hm/?p=13·
- 素材参考：Top Down Effects 1.0
### visual effect Graph
- 官方文档：https://docs.unity3d.com/Packages/com.unity.visualeffectgraph@12.1/manual/index.html
- 制作参考：https://youtu.be/tnUPngBEnQ8
- 素材参考：VFX Graph - Mega Pack - Vol. 3

# 入版规范
## 特效放置路径：
- Assets/Res/Effect
  - 子文件夹包含：Ani/Mat/Mesh/Prefab/Tex

# Utility
- 翻墙软件: https://xbww9056.xyz/
  - 752523247@qq.com
  - askMe
# UI特效
## 要求
  - 可以在 HDRP UI Camera Stacking 下正常显示
  - 以挂在的方式直接播放，生成和销毁由程序控制 