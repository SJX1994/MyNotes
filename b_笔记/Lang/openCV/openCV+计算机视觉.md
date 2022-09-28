# python：   
## Matplotlib:  
            安装：  
                  pip install matplotlib  
            简介：  
                  数值数学扩展 NumPy 的绘图 Matplotlib 库  
## opencv:  
            安装：  
                  pip install opencv-python  
            简介：  
                  主要针对实时计算机视觉的编程函数库。  
# 计算机视觉：  
## 例子：NVIDIA Canvas  
## 机器学习：  
            概念：   
                  Machine learning is nothing but a geometry problem.  
                  机器学习不过是一个几何问题。-The Lazy Programmer   
                  grumpy old statisticians like to say:'machine learning is just glorified curve-fitting'  
                  脾气暴躁的老统计学家会说 机器学习不过是过度美化的曲线拟合  
            深化：  
                  1：2维曲线拟合变成3维（薪水和工作年限的关系，再加上学历、国家）  
                  2：非线性的（比如做两倍的俯卧撑不会导致增加两倍的肌肉）  
            关键词：  
                  分类 classification：  
                        both classification and regression are examples of supervised learning
                        regression: predict a number
                        classification: predict a category/label
                        分类和回归都是监督学习的例子。回归代表预测一个数字，分类代表。分类：预测类别/标签  
                  概率 probabilistic:  
                        - 给定一个x，y=1的概率  
                        - 做出一个预测（prediction），例如：if ( prediction(y=1 | x) >= 50% ){predict = 1}else{predict = 0}  
                        - S型曲线叫做（sigmoid）  
                        - 这种模型叫做 逻辑回归（logistic regression）  
                        - S型曲线 = 逻辑回归  

                        
## 神经网络：
            灵感来源：神经元在视觉皮层上工作
            关键结论1：大脑皮层上的细胞与视觉中的区域相关联，有映射关系。
            关键结论2：神经元间存在分层关系。
            结构：初级层次的细胞对光的方向产生反应，复杂一点的会对光的移动有反应，超复杂的可以反应端点，识别形状。
            ref：https://www.showmeai.tech/article-detail/264
            常规神经网络：
                  常规神经网络的输入是一个向量，比如把一张 32x32x3 的图片延展成 3072*1 的列向量 公式，然后在一系列的隐层中对它做变换。
                  它输出的值被看做是不同类别的评分值。
                  如：W 是 10 个 3072的权重矩阵，最终输出就是 10 * 1 的 得分向量，每一个值是 W 和每一个3072点乘的结果。3072和每一个神经元相连，因此叫 全连接层
            卷积网络：
                  与常规神经网络不同，卷积神经网络的各层中的神经元都是 3 维的：宽度、高度和深度
                  与输入相连的神经元权重不再是 W 的一个行向量（3072个参数），而是与输入数据有同样深度的滤波器（filter，也称作卷积核），比如是 5x5x3 的滤波器 w。
                  这时的神经元（卷积核）不再与输入图像 x 是全连接的，而是局部连接（local connectivity）,只和 x 中一个 5x5x3 的小区域进行全连接
                  结构：
                        输入层：卷积层：ReLU层:池化层：全连接层：
                        例子：
                              输入层：是 32x32x3 的原始像素，宽高32，有3个颜色通道。
                              卷积层：如果使用12个 滤波器/卷积核 输出数据的维度就是 32x32x12
                              池化层：在宽度和高度上进行降采样（downsampling）假设数据尺寸变成16x16x12
                              全连接:层将会计算分类评分,数据尺寸变成 1x1x10  10 个数字对应的就是 CIFAR-10 中 10 个类别的分类评分值


## CNN架构：
            AlexNet
            VGG
            GoogLeNet
            Inception Module
            ResNet(Residual neural network)残差神经网络:
                  长短期记忆 
                  循环神经网络
# 概念：            
## 迁移学习 (Transfer Learning)：
            机器学习的概念，初衷是节省人工标注样本的时间，让模型可以通过已有的标记数据（source domain data）向未标记数据（target domain data）迁移。
## 目标检测(ObjectDetection):
            算法：
                  SSD: Single Shot MultiBox Detector
            机器视觉的中心问题。三个层次分别为：
            分类（Classification）
                  即是将图像结构化为某一类别的信息，用事先确定好的类别(string)或实例ID来描述图片。这一任务是最简单、最基础的图像理解任务，也是深度学习模型最先取得突破和实现大规模应用的任务。在应用领域，人脸、场景的识别等都可以归为分类任务。
            检测（Detection）
                  分类任务关心整体，给出的是整张图片的内容描述，而检测则关注特定的物体目标，要求同时获得这一目标的类别信息和位置信息。检测模型的输出是一个列表，列表的每一项使用一个数据组给出检出目标的类别和位置（常用矩形检测框的坐标表示）。
            分割（Segmentation）
                  分割包括语义分割（semantic segmentation）和实例分割（instance segmentation）。前者是对前背景分离的拓展，要求分离开具有不同语义的图像部分，而后者是检测任务的拓展，要求描述出目标的轮廓（相比检测框更为精细）。分割是对图像的像素级描述，它赋予每个像素类别（实例）意义，适用于理解要求较高的场景，如无人驾驶中对道路和非道路的分割。
## 线性分类理论(Linear Classification Theory):  
 
## 逻辑回归模型(logistic regression):  
            概念：
                  统计学中通过二次函数对某个类别或事件发生的概率进行建模。例如团队获胜的概率、患者健康的概率等
            两个输入变量：
                  因为：
                        一次函数： y = mx + b;
                  所以在笛卡尔坐标系下：
                        x2 = mx1 + b;
                        计算一个点的斜率得出公式：
                        w1x1 + w2x2 + b = 0; (这样写更加便于理解)
                  分类：
                        通过对 a = w1x1 + w2x2 + b; 带入 x1 x2 所有 a >= 0 预测值为 1 ; 带入 x1 x2 所有 a < 0 预测值为 0
            大于2个变量：
`一次函数： y = mx + b:`  
![](resources/openCV_%E4%B8%80%E6%AC%A1%E5%87%BD%E6%95%B0.png)   
`w1x1 + w2x2 + b = a`  
![](resources/openCV_%E5%88%86%E7%B1%BB.png)  