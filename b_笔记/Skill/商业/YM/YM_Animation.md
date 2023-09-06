# 动画流程

## Animancer插件
- 路径：Assets/Plugins/Animancer
- 官方文档：https://kybernetik.com.au/animancer/
## 实现
- 人物手部握刀与放松动画
- 关键词：Assets/Plugins/Animancer/Examples/07 Layers
## 动画预览流程
- Avatar动画导入流程：
  - 基于已有的适配的Avatar进行动画测试：
      1. 导入模型 在 Inspector界面下 Rig子模块 选择 Humanoid 将为模型基于现有模型的骨骼 生成一套 unity Avatar 通用骨骼。
      2.  将模型 从 Porject 中拖到 Hierarchy 下，AddComponet -> Animator
      3. 在 Project 下新建一个 Animator
      4. 在 Animator 面板下 右键 Creat State -> Empty
      5. 在 Animator 面板下 选择创建出来的节点 将已有 动画实例 赋值给 Motion
      6. 将 Project中的 Animator 赋值给 Hierarchy 面板下的 Controller
      7. 将 Project 中 已有的Avatar 赋值给 Hierarchy 面板下的 Avatar
      8. 运行游戏或者在 Animation 界面下拖动滑动条进行动画预览。
  - 基于没有的Avatar进行动画测试：
  
      1. 导入模型 在 Inspector界面下 Rig子模块 选择 Humanoid 将为模型基于现有模型的骨骼 生成一套 unity Avatar 通用骨骼。
      2. 在 Project 下新建一个 Animator
      3. 在 Animator 面板下 右键 Creat State -> Empty
      4. 在 Animator 面板下 选择创建出来的节点 将已有 动画实例 赋值给 Motion
      5. 将 Project中的 Animator 赋值给 Hierarchy 面板下的 Controller
      6. 将 Project 中 新创建的Avatar 赋值给 Hierarchy 面板下的 Avatar
      7. 运行游戏或者在 Animation 界面下拖动滑动条进行动画预览。