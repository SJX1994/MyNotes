# 渲染技术大赏

## 光照

### 颜色

#### 颜色显示原理

实际上观察到的物体颜色为光照射到物体所反射的(`Reflected`)颜色，例如原色为红色的物体会吸收(`Absorb`)除红色外的所有光线，所以看到的颜色为红色。

#### Color in Shader

颜色在Shader中可以用RGB来表示，例如下面的代码表示珊瑚红：

```cpp
vec3 coral(1.0f, 0.5f, 0.31f);
```

先来一个正常光照颜色（白色）：

```cpp
vec3 lightColor(1.0f, 1.0f, 1.0f);
```

使用向量乘法来计算出光照射在物体最终的颜色，此时得到的`resultColor`颜色为上面的珊瑚色`coral`

```cpp
vec3 resultColor = lightColor * coral;
```

### 基础光照

#### 冯氏光照模型 `Phong Lighting Model`

冯氏光照模型由三个分量构成，环境`Ambient`、漫反射`Diffuse`和镜面`Specular`光照,效果如下：

![Phong Lighting Model](resources/basic_lighting_phong.png)

- 环境光照 `Ambient Lighting`：即使在黑暗的情况下，世界上通常也仍然有一些光亮（月亮、远处的光），所以物体几乎永远不会是完全黑暗的。为了模拟这个，我们会使用一个环境光照常量，它永远会给物体一些颜色。
- 漫反射光照 `Diffuse Lighting`：模拟光源对物体的方向性影响(Directional Impact)。它是冯氏光照模型中视觉上最显著的分量。物体的某一部分越是正对着光源，它就会越亮。
- 镜面光照 `Specular Lighting`：模拟有光泽物体上面出现的亮点。镜面光照的颜色相比于物体的颜色会更倾向于光的颜色。

#### 环境光照 `Ambient Lighting`

> 由于全局照明`Global Illumination`算法的资源消耗高以及其复杂性，所以这里将全局光源设置为单光源。

设光源为Color in Shader中的 `lightColor`，物体颜色为`objectColor`，环境光强度`ambientStrength`为`0.1`，则物体照射环境光得到的`resultColor`如下：

```cpp
float ambientStrength = 0.1;
vec3 ambient = ambientStrength * lightColor;
vec3 resultColor = ambient * objectColor;
```

#### 漫反射光照 `Diffuse Lighting`

直接上图，根据光的反射性质，当光源位置和人眼位置处于一处时，光线在物体上的入射角θ 越小时，人眼看见物体的亮度就越高：

![Diffuse Lighting](resources/diffuse_light.png)

计算方式：

##### 物体顶点表面法向量计算

有两种方法：

1. 法线贴图，直接从贴图中获取当前顶点的法向量坐标。
2. 公式计算（不推荐）。

##### 计算漫反射光照

顶点着色器：

1. 法线向量`vNormal`计算。
2. 传入光源坐标`vLightPos`。
3. 传入顶点坐标`vPos`。
4. 计算片段的位置`vFragPos`。

计算方式：

```cpp
vFragPos = vec3(model * vec4(vPos, 1.0));//计算片段在世界中的位置
```

片段着色器：

```cpp
vec3 norm = normalize(vNormal);//单位化法线向量
vec3 lightDir = normalize(vLightPos - vFragPos);//计算光源到物体的向量
float diff = max(0.0, dot(norm, lightDir));//叉乘计算反射光强度系数
vec3 resultColor = diff * lightColor;//计算最终颜色
```

##### 物体拉伸对反射的影响

等比拉伸自然不会产生影响，当不等比拉伸出现时，法线向量将会发生错误，直接看图：

![物体拉伸对反射的影响](resources/basic_lighting_normal_transformation.png)

###### 解决方案

使用法线矩阵`Normal Matrix`来解决，相关链接：[The Normal Matrix](http://www.lighthouse3d.com/tutorials/glsl-12-tutorial/the-normal-matrix/)。

在顶点着色器中，我们可以使用`inverse`和`transpose`函数自己生成这个法线矩阵，这两个函数对所有类型矩阵都有效。注意我们还要把被处理过的矩阵强制转换为`3×3`矩阵，来保证它失去了位移属性以及能够乘以`vec3`的法向量。

```cpp
Normal = mat3(transpose(inverse(model))) * vNormal;
```

#### 镜面光照 `Specular Lighting`

直接上图

![Specular Lighting](resources/basic_lighting_specular_theory.png)

意思就是眼睛视线与光线反射的夹角大小的关系。

先假设视角位置为`viewPos`，其他参数与`Diffuse Lighting`中的保持不变。那么，直接上计算：

先计算视角向量并单位化：

```cpp
vec3 viewDir = normalize(viewPos - vFragPos);
```

再计算光线的反射角向量：

```cpp
vec3 reflectDir = reflect(-lightDir, norm);
```

之后使用叉乘就能得到结果了：

```cpp
float spec = pow(max(0.0, dot(viewDir, reflectDir)), shininess);
```

此处的`shininess`为反光度，具体大小以及相对效果如下：

![shininess](resources/basic_lighting_specular_shininess.png)

所以一半取个32左右的大小就行了

#### 三大光照缝合

这边就很简单了，直接上代码：

```cpp
vec3 result = (ambient + diffuse + specular) * objectColor;
FragColor = vec4(result, 1.0);
```

## 材质与贴图

### 材质表

各类材质在冯氏模型中的相对大小：[OpenGL/VRML Materials](http://devernay.free.fr/cours/opengl/materials.html)

下面的图片展示了几种现实世界的材质对我们的立方体的影响：

![在真实世界中材质的效果](resources/materials_real_world.png)

### 光照贴图

#### 漫反射贴图`Diffuse Map`

简而言之就是将光照计算的漫反射变成贴图上的颜色

```cpp
vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
```

所以环境光反射的材质颜色也要换成贴图上的颜色

```cpp
vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
```

#### 镜面光贴图`Specular Map`

有些材质是不需要反光这么强的，所以这也可以使用一些贴图来实现相应的效果

> 镜面高光的强度可以通过图像每个像素的亮度来获取。镜面光贴图上的每个像素都可以由一个颜色向量来表示，比如说黑色代表颜色向量`vec3(0.0)`，灰色代表颜色向量`vec3(0.5)`。在片段着色器中，我们接下来会取样对应的颜色值并将它乘以光源的镜面强度。一个像素越「白」，乘积就会越大，物体的镜面光分量就会越亮。

使用Photoshop或Gimp之类的工具，将漫反射纹理转换为镜面光纹理还是比较容易的，只需要剪切掉一些部分，将图像转换为黑白的，并增加亮度/对比度就好了。

所以最后在代码中缝合之后就变成如下效果：

```cpp
vec3 ambient  = light.ambient  * vec3(texture(material.diffuse, TexCoords));
vec3 diffuse  = light.diffuse  * diff * vec3(texture(material.diffuse, TexCoords));  
vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
FragColor = vec4(ambient + diffuse + specular, 1.0);
```

### 其他贴图

#### 放射光贴图`Emission Map`

自发光部件所需的颜色，例如机器人眼部发光发亮效果。

## PBR on Kanzi

### Original Shader

分析Kanzi中最原始的PBR。

### 