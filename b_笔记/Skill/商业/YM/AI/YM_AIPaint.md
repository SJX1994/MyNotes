# AI绘图
## 概念
## 参考
- 从0开始：https://youtu.be/vhqqmkTBMlU
- webUI: https://youtu.be/AZg6vzWHOTA
# 框架工具
## WebUI （前端框架）

- https://github.com/AUTOMATIC1111/stable-diffusion-webui

- AUTOMATIC1111参数说明：
  - Prompt：（不影响生成品质）
    - 接受带有权重的单个和多个提示。
    - 
  - Width & Hight: （影响生成品质）
    - 小于 1048576 的最大像素限制
    - 尺寸维度建议：
      - 512x512 768x768 1536x512 1536x384 1536x640 1024x576 
  - Stable Diffusion checkpoint
  - Sampling steps:（影响生成品质）
    - 在请求上执行X次扩散
  - Batch Count：（影响生成品质）
    - 要生成的图像数。 允许批量生成图像。
  - CFG Scale：（不影响生成品质）
    - 指示引擎尝试将生成与提供的提示匹配的紧密程度。
    - v2-x 模型对较低的 CFG (4-8) 反应良好，而 v1-x 模型对较高的范围 (IE: 7-14) 反应良好。
  - StableDiffusion checkpoint：（不影响生成品质）
    - 要使用的推理模型
    - check：https://platform.stability.ai/docs/features/api-parameters#cfg_scale
  - Sampling method：（不影响生成品质）
    - 要使用的采样引擎。
  - Seed：（不影响生成品质）
    - 随机潜在噪声生成的种子。
  - ControlNet:
    - 概念：
      - 是一种通过添加额外条件来控制扩散模型的神经网络结构。
      - <img src="./img/CN.png" width="450"/>
    - Model：
      -  Canny 边缘检测
      -  M-LSD 直线检测
      -  HED 边界
      -  Scribbles 涂鸦
      -  fake scribbles 伪涂鸦（照片生成涂鸦）
      -  openPose 人体关键点
      -  segmentation 色块分区
      -  depth map 深度图生成
      -  normal map 发现光照图生成
   - Guess：
     - 将完全释放非常强大的 ControlNet 编码器的所有功能。
## Stable Diffusion （基础模型）
- 简介
  - 是一种由stability.ai开发的深度学习、文本到图像模型
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
## ControlNet（优化控制算法）
- https://github.com/Mikubill/sd-webui-controlnet

# 训练模型
- 网站
  - https://civitai.com/
## DreamLike (厚涂模型)
- 范例：
  - https://www.reddit.com/r/StableDiffusion/comments/ziru2h/new_art_model_dreamlike_diffusion_10_link_in_the/
## elldrethSLucidMix_v10(插画)
- 范例：
  - https://www.kombitz.com/2022/12/21/elldreths-lucid-mix-1-0-stable-diffusion-model-with-automatic1111-on-a-kaggle-notebook/
## NovelAI （生成卡通模型）
- 简介
  - 图像生成能力非常擅长生成接近日本动漫角色
- 流出版
  - 安装教程：https://economylife.net/windows-novelai-leak-use-webui1111/#toc1
## Chilloutmix （卡通到真人算法）
- https://www.uscardforum.com/t/topic/138696
- https://www.youtube.com/watch?v=onmqbI5XPH8
## Colab （训练LoRA模型）（学习中）
- https://blog.toright.com/posts/6725/google-colab-free-gpu-ai-train.html
- https://youtu.be/X-mgG79HOZM
## 角色生成
- https://civitai.com/models/3036/charturner-character-turnaround-helper
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
# 案例
- 扇子icon生成
  - https://www.artstation.com/artwork/QnbElx
  - 尝试 prompt：

- 钱袋icon生成
  - A grey cotton money bag , tied with rope , scattered golden coins,
  - fantasy, intricate, elegant, (highly detailed:1.2), anime, artstation, concept art, smooth, sharp focus, illustration, art by artgerm and greg rutkowski and alphonse mucha,
  - (Shadows:0.01),Screen Space Global Illumination,(front upper right Soft lighting:1.2),Light Mode--no blur
  - Negative prompt: 3d, render, doll, plastic, blur, haze, monochrome, b&w, text, (ugly:1.2), unclear eyes, no arms, bad anatomy, cropped, censoring, asymmetric eyes, bad anatomy, bad proportions, cropped, cross-eyed, deformed, extra arms, extra fingers, extra limbs, fused fingers, jpeg artifacts, malformed, mangled hands, misshapen body, missing arms, missing fingers, missing hands, missing legs, poorly drawn, tentacle finger, too many arms, too many fingers, watermark, logo, text, letters, signature, username, words, blurry, cropped, jpeg artifacts, low quality, lowres,(colored background:1.6)
  - Steps: 25, Sampler: DPM++ SDE, CFG scale: 11.5, Seed: 715233396, Size: 512x512, Model hash: 0aecbcfa2c, Model: dreamlike-diffusion-1.0, ControlNet Enabled: True, ControlNet Module: hed, ControlNet Model: control_sd15_hed [fef5e48e], ControlNet Weight: 1, ControlNet Guidance Start: 0, ControlNet Guidance End: 1

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