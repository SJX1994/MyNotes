## 1. 前言
### 效用？
- 这本书是为那些从事计算机图形学工作的人准备的，
- 他们知道他们必须在日常工作中使用数学，
- 并且不想过多地卷入
- 公理、真理和柏拉图式的现实中。
- 冲着顿悟感来的。

## 2. 数字 Numbers
- 集合论? Set Notation
  - 提供了一个用于 操作和 询问集合的逻辑框架
  - 是 xxx 的成员
  - ∈
- 进制系统? Positional Number System
  - 人类已经进化出十位数字，我们用它来计数。
  - 1234 = 1 × 1000 + 2 × 100 + 3 × 10 + 4 × 1
  - 十进制数字系统对于计算机技术来说并不是很方便，
  - 在计算机技术中，数字电路每秒使用二进制数（以 2 为基数的数字）
  - 打开和关闭数万亿次非常容易。
- 自然数? Natural Numbers
  - 用于计数、排序和标记。
  - 负数不包括在内。
  - N
- 质数? Prime Numbers
  - 是一个自然数，
  - 只能被 1 和它自己整除，
  - 而不留下余数。
  - 任何正整数（1 除外）
  - 都可以以一种且只有一种方式写成两个或多个素数的乘积。
  - 例如：
  ![alt text](image.png)
  - 在计算机图形学中没有发挥作用！- -。
- 整数? Integer Numbers
  - 整数包括正自然数和负自然数
  - Z
- 有理数? Rational Numbers
  - 有理数或 小数是可以表示为 分数
  ![alt text](image-1.png)
  - 有理数可以准确地存储在计算机中，
  - 但许多其他有理数只能近似存储。
  - 例如，4/3 = 1.333333 . . .
  - 产生一个无限的 3 序列
  - 当存储为二进制数时必须被截断。
  - Q
- 无理数? Irrational Numbers
  - 无理数不能表示为分数。
  ![alt text](image-2.png)
  - 这些数字永远不会终止，
  - 并且在存储在计算机中时总是会出现小错误。
- 实数? Real Numbers
  - 有理数和无理数共同组成了 实数
  - R
  - **连续性：** 实数是一个连续的集合，没有间断。
  - **完备性：** 每个有界单调数列在实数中都有极限，且这个极限也属于实数。
- 数字线? The Number Line
  ![alt text](image-3.png)
  - 数字线的形式可视化数字很有用，
  - 这有助于我们理解复数。
- 复数? Complex Numbers
  - 瑞士数学家莱昂哈德·欧拉（Leonhard Euler，1707-1783 年）发明。
  - 起因：尝试求解某些类型的方程时出现的一些尴尬的无解问题
  ![alt text](image-8.png)
  ![alt text](image-9.png)
  ![alt text](image-10.png)
  - 数字线为一种新型数字提供了 图形解释：复数。
  - 他的想法可以在 计算机图形学 中用于旋转。
  - 以定位空间中的物体和虚拟摄像机。
  - **意象展开！**
  - 数字线上的任何数字都通过**逆时针旋转 180** 
  ![alt text](image-4.png)
  - −2 = −1 × 2，或 3 = −1 × −3
  - 其中 −1 实际上是 旋转 180。
  - 但是 180 的旋转可以解释为连续两次 90 的旋转，现在出现了一个问题：
  - 什么代表 90 的旋转？
  - 旋转 90 叫 i （虚数）
  - 字母 i 表示逆时针旋转 90
  - 比如：
    - 2 转两个 90 就表示为 2ii = -2
    - 因为 2ii = -2 所以 i^2 = -1
    - 所以 ![alt text](image-5.png)
    - 由此可见 i 不是一个数字 而是一个运算符
  - 例如：
    ![alt text](image-7.png)
    - 从P旋转90度到Q，
    ![alt text](image-12.png)
    - 再旋转90度到R，
    ![alt text](image-13.png)
    - 再旋转90度到S，
    ![alt text](image-14.png)
    - 再旋转90度回到Q
    ![alt text](image-15.png)
  - 复数在计算机图形学中并没有直接发挥重要作用。
  - **但** 虚数在四元数和几何代数中非常重要。
  - 复数与笛卡尔坐标密切相关，
  - 并且导致了向量和四元数的发现。
  - 最美等式之一：
  ![alt text](image-11.png)
  - e（自然对数的底）、i（虚数单位）、π（圆周率）、1和0。
  - 看似不相关的数学概念之间出人意料的联系。
  - C & i

## 3. 代数 Algebra
### 概念?
  - 代数的一个基本概念是给 未知量命名的想法。
  - 借助 +, −, ×, ÷ 可以开发描述物理过程或逻辑计算行为的表达式。
  - 代数表达式还包含多种 函数。
  ![alt text](image-17.png)
### 代数定律 Algebraic Laws
- 代数定律？
  - 数学表现得很好，因为它受制于定律或公理系统。
  - （游戏学中的构成规则）
- 负数 和 0?
  - 负负得正
  ![alt text](image-16.png)
- 二元运算？Binary Operation
  ![alt text](image-21.png)
- 结合律? Associative Law
  - 当三个或多个元素通过 二元运算连接在一起时，
  - 结果与每对元素的分组方式无关。
  - 加法结合律：（分组是无关紧要的）
  ![alt text](image-18.png)
  - 乘法结合律：（分组也是无关紧要的）
  ![alt text](image-19.png)
  - 减法结合律 不是关联的！
  ![alt text](image-20.png)
- 交换律? Commutative Law
  - 两个元素通过一些 二元运算运算连接时，
  - 结果与元素的顺序无关。
  - 加法交换律：
  ![alt text](image-22.png)
  - 乘法交换律：
  ![alt text](image-23.png)
  - 减法没有交换律！
  ![alt text](image-24.png)
- 分配律? Distributive Law
  - 一种运算，当对 元素组合执行时，与对 单个元素执行运算相同。
  - 乘法置于一组加法之上？成立！
  ![alt text](image-25.png)
  - 加法置于一组乘法之上？不成立！
  ![alt text](image-26.png)
- 局限?
  - 尽管这些定律中的大多数对于数字来说似乎是自然的，
  - 但它们并不一定适用于所有数学结构。
  - 例如，将两个向量相乘的向量积不是可交换的。
### 解二次方程的根？ Solving the Roots of a Quadratic Equation
![alt text](image-27.png)
- 目标：把X放到等号左边。
- 由于X涉及次方。
- 因此，策略是创造一种容易取平方根的情况。
![alt text](image-28.png)
### 指数? Indices
- 指数的规律
![alt text](image-29.png)
![alt text](image-30.png)
### 对数? Logarithms
- 两个发明对数的人：
- 苏格兰神学家数学家 约翰·纳皮尔（John Napier，1550-1617）
- 和瑞士钟表匠和数学家 约斯特·布尔吉（Joost Bürgi，1552-1632）
- 数字相乘上花费的时间 感到沮丧。
- 他们发现：
  对数 利用了上面所示的索引的加法和减法，
  并且总是与基数相关联。
![alt text](image-31.png)
- 这被解释为“10必须被提升到 指数2 才能等于100”。
对数运算找到给定数的基数幂。
因此，使用对数将乘法转换为加法。
![alt text](image-32.png)
- log x 的图表，直到 x = 100，我们可以看到log 20 ≈ 1.3和 log 50 ≈ 1.7。
- **20*50** 可以按照如下表示：
![alt text](image-33.png)
- 超越数 e ： transcendental number. 
- 用于作为计算基础的 是 10 和 e（2.718281846...）
- 超越数 e 不是任何代数方程的根
- 就像 圆的周长与其直径的比 π
- 以 10 为底的对数记为 log
以 e 为底的自然对数记为 ln。
![alt text](image-34.png)
-  ln x 的图表，直到 x = 100，我们看到 ln 20 ≈ 3 和 ln 50 ≈ 3.9
- **20*50** 可以按照如下表示：
![alt text](image-36.png)
- 综上 对数运算定律：
![alt text](image-37.png)
### 更多符号? Further Notation
- 符号如何代替自然语言？
- ![alt text](image-38.png)
- 例如：0 ≤ t ≤ 1。
- 这意味着 t 在 0 和 1 之间变化。
### 函数? Functions
- 莱布尼茨的定义：根据图表的斜率对函数进行了早期定义
- 欧拉的定义：函数是一个变量，其值取决于一个或多个自变量
#### 显式和隐式方程? Explicit and Implicit Equations
- **显示方程:**
- 定义:显示方程是将一个变量明确地表示为其他变量的函数。
形式:通常表示为 y = f(x)，即将 y 明确表示为 x 的函数。
- 特点:
  - 变量之间的关系很清晰,可以直接求出 y 的值。
  - 容易进行微分和积分等数学运算。
  - 通常用于描述函数关系。
- **隐式方程? Explicit and Implicit Equations**
- 定义:隐式方程是将多个变量之间的关系以等式的形式表达,无法直接表示某个变量为其他变量的函数。
- 形式:通常表示为 F(x, y) = 0，即将变量 x 和 y 的关系定义为一个等式。
- 特点:
  - 变量之间的关系不太清晰,需要通过求解等式来确定变量之间的对应关系。
  - 微分和积分等数学运算相对复杂。
  - 通常用于描述曲线、曲面等几何图形。
  
#### **函数表示法? Function Notation**
- ![alt text](image-39.png)
- y=f(x)
  - 在这个表达式中：
  - x 是自变量（输入），我们可以自由选择它的值。
  - y 是因变量（输出），它的值依赖于自变量 x 的值。
- 图形学中的应用：
  - ![alt text](image-40.png)
  - ![alt text](image-41.png)
  - ![alt text](image-42.png)
#### **区间？Intervals**
  - ![alt text](image-43.png)
  - ![alt text](image-44.png)
  - ![alt text](image-45.png)
  - ![alt text](image-46.png)
#### 定义域和区间 Function Domains and Ranges
- 定义域(Domain):
  - **坐标系统:** 在二维和三维坐标系中,函数的定义域通常是实数集R或其子集,因为坐标值都是实数。
  - ![alt text](image-47.png)
  - **图像处理:** 在图像处理中,图像像素的坐标通常是整数,因此函数的定义域就是整数集Z。
  - ![alt text](image-48.png)
  - **参数方程:** 在使用参数方程描述曲线或曲面时,参数的取值范围就构成了函数的定义域。
  - 圆的参数方程 为例：
  - ![alt text](Drafts_圆.jpg)

## 4. 三角学 Trigonometry

### 角度测量单位？ Units of Angular Measurement
- 现代保留的测量单位：度 和 弧度 degrees and radians
- 度（Degrees）
  - 度是一个用于测量角度的单位。一个完整的圆被分成360个等分，
  - 每个等分就是1度（°）。
  - 因此，360度等于一个完整的旋转。
- 弧度（Radians）
  - 弧度是另一个用于测量角度的单位，基于圆的半径。
  - 1弧度是指在单位圆上，圆弧的长度等于圆的半径。
  - 一个完整的圆有 2π 弧度
- 度与弧度的换算
  - 180° = π radians
  - 360° = 2π radians
  - 57.3° = 1 radian

### 三角比？ The Trigonometric Ratios
- 边长与角度之间的固有比例关系，
- 知道这些比例，就可以利用这些比例来解决三角形中未知长度和角度的问题。
- ![alt text](image-49.png)
- 上图描述了一个坐标为(**底边-base,高-height**)的点P,
- 位于一个 **半径-radius** 为1的圆上,旋转了角度θ。
- 在旋转过程中,高和底的符号变化如下:
- ![alt text](image-50.png)
- 高-height 变化如下:
- ![alt text](image-51.png)
- 底边-base 变化如下：
- ![alt text](image-52.png)

- 半径-radius = 斜边-hypotenuse 
- 高-height = 对边-opposite 
- 底边-base = 邻边-adjacent 
- ![alt text](image-53.png)
- ![alt text](image-54.png)
- ![alt text](image-55.png)
- 例子：
- ![alt text](image-56.png)
- 求 对边-opposite 和 邻边-adjacent
- ![alt text](image-57.png)

#### 域和值域？Domains and Ranges
- 域 (Domain)
  - 定义：
    - 函数的 **域** 是指所有可能的输入值（自变量或独立变量）的集合，
    - 通常用 x 表示。
    
- 值域 (Range)
  - 定义：
    - 函数的 **值域** 是指所有可能的输出值（因变量或依赖变量）的集合，
    - 通常用 y 表示。
---
- 三角函数的 域 (Domain)
  - sin θ 、 cos θ 和 tan θ 的周期性质意味着它们的 **域** 无限大。
  - 习惯上将 sin θ 的 **域**限 制为：![alt text](image-58.png)
  - tan θ 的 **域** 限制为：![alt text](image-59.png)
  - ![alt text](image-60.png)
- 三角函数的 值域 (Range)
  - sin θ 和 cos θ 的 **值域** 是 [-1, 1]。
  - tan θ 的 **值域** 是 (-∞, ∞)。

### 反三角函数？ Inverse Trigonometric Functions
- arcsin、arccosarccos、arctanarctan、arccsc、arcsec 和 arccot 都是反三角函数。
- 其中 arc 是 arclength 弧长 的缩写。
- 反三角函数将比率转换回角度。
- ![alt text](image-61.png)

### 三角恒等式？ Trigonometric Identities
- 三角恒等式是指在三角函数中成立的等式。
- 除了位移 90 之外，sin 和 cos 曲线是相同的：
- ![alt text](image-62.png)
- 简单代数和毕达哥拉斯定理可用于推导其他公式：
- ![alt text](image-63.png)

### 正弦法则？ The Sine Rule
![alt text](image-64.png)
![alt text](image-65.png)
### 余弦法则？ The Cosine Rule
![alt text](image-64.png)
![alt text](image-66.png)
![alt text](image-67.png)

### 复合角？ Compound Angles
![alt text](image-68.png)

### 周长关系？ Perimeter Relationships 
![alt text](image-64.png)
![alt text](image-69.png)

### Reference
- Handbook of Mathematics and Computational Science by John Harris and Horst Stocker (1998)
- Mathematics from the Birth of Numbers by Jan Gullberg (1997).

## 5. 坐标系 Coordinate Systems
### 预热
- 笛卡尔坐标系、轴系、空间中两点之间的距离以及简单二维形状的面积。
笛卡尔坐标系：
![alt text](image-73.png)

- 它还涵盖了 极坐标系、球面极坐标系 和 圆柱坐标系。

极坐标系：
![alt text](image-70.png)
球面极坐标系：
![alt text](image-71.png)
圆柱坐标系：
![alt text](image-72.png)

### 函数图？ Function Graphs
- 创建熟悉的图形，以便快速识别函数的来源。
  - ![alt text](image-74.png)
  - 线性函数是直线；
  - 二次函数是抛物线；
  - 三次函数呈现“S”形；
  - 而三角函数通常具有波动的轨迹。
- 亮度与帧率的关系
  - ![alt text](image-75.png)
  - 这些图形在计算机动画中用于控制物体、灯光和虚拟相机的运动。
  - 但这些图形所描绘的不是x和y之间的关系，
  - 而是活动（例如运动、旋转、大小、亮度、颜色等）与时间之间的关系。