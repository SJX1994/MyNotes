# 初级渲染_kzPosition

## 初次见面
> 导入模型在世界空间下的三维坐标      


## 自我介绍
> 提供对网格顶点或片段的世界空间下位置的访问      

| 数据流向 | 类型 | 描述                |
| -------- | ---- | ------------------- |
| 传入     | vec3 | 外部导入            |
| 传出     | vec3 | 网格顶点/片段的位置 |

## 变量故事
> 本地坐标是 对象相对于其本地 原点 的坐标。它们是 导入对象的起始坐标。  
---
> 从maya中导入正确的 本地原点坐标：
```c++
      0.选择所有meshs（包含父级控制节点）,【修改——冻结变换（全选）】
      1.选择所有meshs（不包含父级控制节点）,【修改——重置变换（只选平移和旋转）】
      2.只选择控制节点——  【修改烘焙轴枢（选位置和方向）】
      3、导出控制节点整体
      4、导入kanzi: Merge Assets —— 控制节点模型 + merge Source Mesh
      5、所有部件修改完  （删除原工程中对应节点），一起导入，防止出现组件紊乱

                  ↓ 如下图所示 ↓
```
![Phong Lighting Model](ref/img/kzPosition/maya0.jpg)
![Phong Lighting Model](ref/img/kzPosition/maya1.jpg)
![Phong Lighting Model](ref/img/kzPosition/maya2.jpg)

## 动手试一试
> 传递对象空间坐标到片元
```c++
      //顶点代码
      precision mediump float;
      attribute vec3 kzPosition;
      varying lowp vec3 objPos;
      uniform highp mat4 kzProjectionCameraWorldMatrix;

      void main()
      {
            objPos = kzPosition;
            gl_Position = kzProjectionCameraWorldMatrix * vec4(position.xyz, 1.0);
      }
      //片元代码
      varying lowp vec3 objPos;
      void main()
      {
            //这里以x轴为例 xyz 都可以取
            gl_FragColor = vec4(vec3(objPos.x), 1.0) * BlendIntensity;
      }
```



## 亲切昵称
> 通俗解释        
```c++
      就像一张纸 用一个钉子 钉在黑板上，钉子 就是 纸坐标的原点。
      把这张纸变换成3维的 就是kzPosition
      有时候我们需要 几张纸 共用一个钉子,我们就需要在 3维 软件中重置变换
```

## 结伴而行（持续更新）
> 和其他变量的组合创新
---
>  旋转 位移 缩放                        
```c++
//顶点代码  
      precision mediump float;
      attribute vec3 kzPosition;
      varying lowp vec3 objPos;
      uniform highp mat4 kzProjectionCameraWorldMatrix;

      //用于测试
      uniform vec3 testPosit;
      uniform vec3 testScale;
      uniform vec3 testRotat;

      //旋转 位移 缩放 矩阵 
      //根据公式：

      vec4 matrixChange(vec3 Posit,vec3 Scale, vec3 Rotat , vec4 Target )
      {
            //位移矩阵
            vec3 transValues = Posit;
            mat4  transfrom = mat4(
            vec4( 1.0,        0.0,         0.0,  transValues.x ),
            vec4( 0.0,        1.0,         0.0,  transValues.y ),
            vec4( 0.0,        0.0,         1.0,  transValues.z ),
            vec4( 0.0,        0.0,         0.0,  1.0 ) );
            //缩放矩阵
            vec3 scaleV = Scale;
            mat4  scale = mat4(
            vec4( scaleV.x, 0.0,          0.0,  0.0 ),
            vec4( 0.0,      scaleV.y,     0.0,  0.0 ),
            vec4( 0.0,      0.0,     scaleV.z,  0.0 ),
            vec4( 0.0,      0.0,          0.0,  1.0 ) );    
            //旋转矩阵z
            float angleZ=Rotat.z;
            mat4  rotationZ = mat4(
            vec4( cos(angleZ), -sin(angleZ),0.0,  0.0 ),
            vec4( sin(angleZ),  cos(angleZ),0.0,  0.0 ),
            vec4( 0.0,        0.0,          1.0,  0.0 ),
            vec4( 0.0,        0.0,          0.0,  1.0 ) );
            //旋转矩阵x
            float angleX=Rotat.x;
            mat4  rotationX = mat4(
            vec4( 1.0,        0.0,         0.0,  0.0 ),
            vec4( 0.0,cos(angleX),-sin(angleX),  0.0 ),
            vec4( 0.0,sin(angleX), cos(angleX),  0.0 ),
            vec4( 0.0,        0.0,         0.0,  1.0 ) );
            //旋转矩阵z
            float angleY=Rotat.y;
            mat4  rotationY = mat4(
            vec4(  cos(angleY), 0.0, sin(angleY),  0.0 ),
            vec4(  0.0        , 1.0,         0.0,  0.0 ),
            vec4( -sin(angleY), 0.0, cos(angleY),  0.0 ),
            vec4(          0.0, 0.0,         0.0,  1.0 ) );

            return Target*transfrom*scale*rotationX*rotationY*rotationZ;
      }

      void main()
      {
            
            vec3 modifiedPos = (
            matrixChange(
                  vec3(testPosit),
                  vec3(testScale),
                  vec3(testRotat),
                  vec4(kzPosition, 1.0)
                  )).xyz;
                  
            gl_Position = kzProjectionCameraWorldMatrix * vec4(modifiedPos.xyz, 1.0);
      }

      //片元代码

      precision mediump float;

      void main()
      {
            gl_FragColor = vec4(vec3(0.5), 1.0) * BlendIntensity;
      }
```                              

