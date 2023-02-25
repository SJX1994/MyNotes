# AI绘图
## 概念
## 参考
- 从0开始：https://youtu.be/vhqqmkTBMlU
- webUI:https://youtu.be/AZg6vzWHOTA
# 工具
## ControlNet
- https://github.com/Mikubill/sd-webui-controlnet
## WebUI
- https://github.com/AUTOMATIC1111/stable-diffusion-webui
## Stable Diffusion
- 简介
  - 是一种深度学习、文本到图像模型
  - 架构
    - 3 部分组成：
      - 变分自动编码器 (VAE)  variational autoencoder
        - VAE 编码器将图像从像素空间压缩到更小维度的潜在空间，捕捉图像更基本的语义。
        - VAE 解码器通过将表示转换回像素空间来生成最终图像。
      - U-Net
        - 对前向扩散向后的输出进行去噪以获得潜在表示。
      - 可选的文本编码器 optional text encoder
        - 去噪步骤可以灵活地以文本字符串、图像或其他形式为条件。
  - 训练数据
    - Stable Diffusion 在 LAION-5B 中的成对图像和字幕上进行了训练，LAION-5B 是一个公开可用的数据集，源自从网络上抓取的Common Crawl数据，其中 50 亿个图像-文本对根据语言进行分类，并按分辨率过滤到单独的数据集中，包含水印的预测可能性，以及预测的“审美”分数（例如主观视觉质量）。
  - 培训程序
    - 该模型在Amazon Web Services上使用 256 个Nvidia A100 GPU进行了总计 150,000 GPU 小时的训练，成本为 600,000 美元。
  - 限制
    - 模型没有充分训练来理解人类的四肢和面部，促使模型生成此类图像可能会混淆模型。
- 模型
  - https://golabo.net/installer_stablediffusion_webui/
## NovelAI
- 简介
  - 图像生成能力非常擅长生成接近日本动漫角色
- 流出版
  - 安装教程：https://economylife.net/windows-novelai-leak-use-webui1111/#toc1
# 制作
- 绘画教程
  - https://zhuanlan.zhihu.com/p/574074013
- 输入prompt教程
  - https://aiguidebook.top/index.php/2022/11/07/novel-ai%E5%85%A5%E9%97%A8%E5%AF%BC%E8%AE%BA-%E5%85%83%E7%B4%A0%E5%90%8C%E5%85%B8%EF%BC%9A%E8%BF%9B%E9%98%B6%E8%AF%AD%E6%B3%95/
  - https://lunarmimi.net/inspiration/10-novelai-prompts/
- Sampling：
- ControlNet 参数：
  -  https://www.bilibili.com/video/BV1JM411c7mH/?share_source=copy_web&vd_source=ee8bf4f80f85e2ba54f02e9721d930d6
# 本地
- 入口：
  - D:\Apps\AItool\StableDiffusionWebUI\

# 研究报告：
## 2023/2/25：
- 输入prompt：极度清秀的女孩，视野开阔，态度冷淡，红瞳短发，白发五彩，黑色西装领带，发饰发夹，看起来很清爽，侧脸，白皙大气，单个女孩，{{{ 水墨画}}}，白色背景，干净的黑白衣服
- 输入图片：  
 <img src="./img/input.jpg" width="150"/>  

- 文本自由输出：
  <img src="./img/free.png" width="450"/>  

- ControlNet输出：  
  <img src="./img/controlNet.png" width="450"/>  

- 神经网络架构：ControlNet(0feca46f (Fri Feb 24 23:45:07 2023)) + stable diffusion

- 选择模型：NovalAI (52.1G版本)

- 前端框架：基于 webUI 的 AUTOMATIC1111 

- 计算机配置：
  - 显卡：
<img src="./img/GPU.jpg" width="450"/> 

  - 系统CPU：
<img src="./img/System.jpg" width="450"/> 

- 参数设置：
  - Sampling steps：20
  - Batch count：5 / 10
  - Seed：-1
  - CFG Scale：8.5
  - ControlNet：
  - <img src="./img/controlNetSetting.jpg" width="450"/> 
  - 平均输出时间：30s