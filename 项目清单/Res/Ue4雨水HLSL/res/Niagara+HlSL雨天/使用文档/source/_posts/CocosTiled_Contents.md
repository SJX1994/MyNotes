---
title:
- CocosCreator+Tiled工作流_Version0.1
tags: 
    - Cocos
    - Tiled
categories:
    - Cocos
    - Tiled
    - Contents
author:
    - 沈捷翔_Cocos


---

> <center><big><b>简介：</b></big><br>这是以一个项目为范例,面向美术人员结合CocosCreator和Tiled工作流<br>-----------------------------------------------------------------------------</center>

# <center><b>CocosCreator+Tiled工作流</b></center>
- ## <b>开始之前</b>
    - > *下载* - 必要的资源[下载](https://todo)
    - > *文档* - 参考CocosCreator和Tiled官方文档
---
- ## <b>工作流概览：</b>
---
  - <details><summary>Tiled中制作： </summary>

    - **熟悉软件**

      - [快捷键](https://doc.mapeditor.org/en/latest/manual/keyboard-shortcuts/)

    - **位移动画(针对此项目)**

      - 以小汽车为例：
        - 绘制好地图保存后，在`.tmx`文件中搜索对应`物件名字`修改对应`propertyName`的值`value`这个value的值对应`Tile格子的坐标`

    - **命名规范(针对此项目)**

      - 如果没有新增的功能，遵循已经制作好的`.tmx`文件的格式根据需要实现的功能来书写
      - 导入的文件名根据`_night`来区分白天和晚上的素材
      - 分出两个图层`landscape`和`LandLayer`分别表示前景和地块

    - **开发者操作**

      - JavaScript拓展范例
        - 控制台
          - 打开：`视图-视图和工具栏-控制台`
        - JS插件存放读取路径：
          - `编辑-首选项-插件-拓展-打开`
          - [范例脚本/API](https://doc.mapeditor.org/en/latest/reference/scripting/)</details>
---
  - <details><summary>Cocos中制作：</summary>

    - <details><summary>外部修改(针对此项目)</summary>

      - 在拓展新地图时基于框架同时需要：
        - 更新plist,可以使用软件：TexturePacker
        - 添加`landsjson'X'.json`
          - 不添加的话，不改变代码的情况下正常`LandMgr.ts`程序走到`loadLandData`函数下,`data`会报空
        - 在`landMgr.ts`中`canLandMoveUp`函数下，添加对应的`centerSizes`键值对
          - 不添加的话，`centerSize`会报空，地块无法正常显示上浮效果。这段参数代表了`中间城堡的占地面积`
          - 或者在`Config.ts`中添加一项键值对,通过`config`读取</details>

          - <details><summary>内部修改(针对此项目)</summary>

            - 整个文件夹命名好，拖到`resources`下,更新`Config.ts-LAND_TYPE_DATA`中新增一项
            - 在`landMgr.ts`中`canLandMoveUp`函数下，添加对应的`centerSizes`键值对
                - 不添加的话，`centerSize`会报空，地块无法正常显示上浮效果。这段参数代表了中间城堡的占地面积
                - 或者在`Config.ts`中添加一项键值对,通过`config`读取
            - 预览整体效果
                - 在`LandMgr.ts`中,在生命周期`start()`中的`else中的this.loadLandData('x')`;把'x'替换成对应的`地图编号`</details>
              - <details><summary>动画预制体制作</summary>

                - 挂载组件(通常)
                  - `Node`
                  - `Sprite`
                    - 图片路径：`landscape`
                  - `Animation`
                    - 工程路径: `resources_animation`</details></details>
---
- ## <b>该项目的Cocos的目录结构：</b>
---
  - <details>
    <summary>
    |── assets // 游戏代码资源(点击展开)
    </summary>
    
    场景美术资源制作需要用到的路径用<b>加粗</b>来标注
    - <details>
        <summary>
        |-----|── res  // 游戏图片资源
        </summary>

        该目录引用到的图片会被打包发布
        </details>

    - <details>
        <summary>
        |-----|── <b>resources</b> //游戏各类资源目录，该目录所有资源必会打包
        </summary>

        - <details>
        <summary>
        |-----|-----|── <b>animation</b> //动画资源
        </summary>

        | ----- | ----- | ----└── aircraft.anim |
        | ----- | ----- | --------------------- |└── Bonfire.anim
        | ----- | ----- | ----└── carousel_night.anim |
        | ----- | ----- | --------------------------- |└── carousel.anim
        | ----- | ----- | ----└── Castle_night.anim |
        | ----- | ----- | ------------------------- |└── Castle.anim
        | ----- | ----- | ----└── dachangjing_night.anim |
        | ----- | ----- | ------------------------------ |└── dachangjing.anim
        | ----- | ----- | ----└── Dragon.anim |
        | ----- | ----- | ------------------- |└── fengche.anim
        | ----- | ----- | ----└── FerrisWheel_night.anim |
        | ----- | ----- | ------------------------------ |└── FerrisWheel.anim
        | ----- | ----- | ----└── Glowworm.anim |
        | ----- | ----- | --------------------- |└── hudie.anim
        | ----- | ----- | ----└── jingyu.anim |
        | ----- | ----- | ------------------- |└── loading.anim
        | ----- | ----- | ----└── mountain01.anim |
        | ----- | ----- | ----------------------- |└── ship02.anim
        | ----- | ----- | ----└── ship04.anim |
        | ----- | ----- | ------------------- |└── ship05.anim
        | ----- | ----- | ----└── Watermonster.anim |
        | ----- | ----- | ------------------------- |└── windmill2.anim
        |-----|-----|----└── xiaochuan.anim
        </details>

        <details>
        <summary>
        |-----|-----|── <b>Atlantis</b>  //亚特兰蒂斯大陆资源
        </summary>
        
        | ----- | ----- | ---- | ── Landforms //基础地块图片资源 |
        | ----- | ----- | ---- | ------------------------------- |└── LandTiled.plist
        | ----- | ----- | ---- | ----  └── LandTiled.png |
        | ----- | ----- | ---- | ----------------------- |└── LandTiled_night.plist
        | ----- | ----- | ---- | ----  └── LandTiled_night.png |
        | ----- | ----- | ---- | ----------------------------- |└── SEKUAI01.png  //地图编辑器所用资源
        | ----- | ----- | ----└── landscape //景观图片资源 |
        | ----- | ----- | -------------------------------- |└── Atlantis //瓦片地图文件
        |-----|-----|----└── readme
        </details>

        <details>
        <summary>
        |-----|-----|── <b>Byzantium</b>  //拜占庭大陆资源(参考)
        </summary>

        | ----- | ----- | ---- | ── Landforms |
        | ----- | ----- | ---- | ------------ |└── LandTiled.plist
        | ----- | ----- | ---- | ----  └── LandTiled.png |
        | ----- | ----- | ---- | ----------------------- |└── LandTiled_night.plist
        | ----- | ----- | ---- | ----  └── LandTiled_night.png |
        | ----- | ----- | ---- | ----------------------------- |└── SEKUAI01.png 
        | ----- | ----- | ----└── landscape |
        | ----- | ----- | ----------------- |└── Atlantis
        |-----|-----|----└── readme
        </details>

        <details>
        <summary>
        |-----|-----|── Prefabs  //预制体目录
        </summary>

        | ----- | ----- | ----└── attenzione.prefab //禁止点击标识 |
        | ----- | ----- | ---------------------------------------- |└── generalSprite.prefab //一般用纹理
        | ----- | ----- | ----└── hoverIcon.prefab //触摸地块区域标识 |
        | ----- | ----- | ------------------------------------------- |└── landform.prefab //基础地块
        | ----- | ----- | ----└── landscape.prefab //景观 |
        | ----- | ----- | ------------------------------- |└── locationTips.prefab //坐标提示牌
        </details>

        <details>
        <summary>
        |-----|-----|── <b>Preform</b>  //动画预制文件目录(参考)
        </summary>

        | ----- | ----- | ----└── Aircraft.prefab |
        | ----- | ----- | ----------------------- |└── Aircraft_night.prefab
        | ----- | ----- | ----└── Bonfire.prefab |
        | ----- | ----- | ---------------------- |└── Butterfly.prefab
        | ----- | ----- | ----└── Butterfly_night.prefab |
        | ----- | ----- | ------------------------------ |└── carousel.prefab
        | ----- | ----- | ----└── carousel_night.prefab |
        | ----- | ----- | ----------------------------- |└── Castle.prefab
        | ----- | ----- | ----└── Castle_night.prefab |
        | ----- | ----- | --------------------------- |└── city.prefab
        | ----- | ----- | ----└── city_night.prefab |
        | ----- | ----- | ------------------------- |└── Cloud.prefab
        | ----- | ----- | ----└── Cloud_night.prefab |
        | ----- | ----- | -------------------------- |└── Dragon.prefab
        | ----- | ----- | ----└── Dragon_night.prefab |
        | ----- | ----- | --------------------------- |└── FerrisWheel.prefab
        | ----- | ----- | ----└── FerrisWheel_night.prefab |
        | ----- | ----- | -------------------------------- |└── Glowworm.prefab
        | ----- | ----- | ----└── loading.prefab |
        | ----- | ----- | ---------------------- |└── mountain01.prefab
        | ----- | ----- | ----└── mountain01_night.prefab |
        | ----- | ----- | ------------------------------- |└── ship02.prefab
        | ----- | ----- | ----└── ship02_night.prefab |
        | ----- | ----- | --------------------------- |└── ship04 - night.prefab
        | ----- | ----- | ----└── ship04.prefab |
        | ----- | ----- | --------------------- |└── ship05 - night.prefab
        | ----- | ----- | ----└── ship05.prefab |
        | ----- | ----- | --------------------- |└── Water monster.prefab
        | ----- | ----- | ----└── Water monster_night.prefab |
        | ----- | ----- | ---------------------------------- |└── whale.prefab
        | ----- | ----- | ----└── whale_night.prefab |
        | ----- | ----- | -------------------------- |└── windmill.prefab
        | ----- | ----- | ----└── windmill_night.prefab |
        | ----- | ----- | ----------------------------- |└── windmill2.prefab
        |-----|-----|----└── windmill2_night.prefab
        </details>

    - <details>
        <summary>
        |-----|── Scene //游戏场景目录
        </summary>

        |-----|-----|── Mainland //主场景
        </details>

    - <details>
        <summary>
        |-----|── <b>Script</b> //游戏代码
        </summary>

        - <details>
        <summary>
        |-----|-----|── <b>ComUnit</b> //公共配置和组件目录
        </summary>

        | ----- | ----- | ----└── AlartMsg.ts //提示框，暂未用到 |
        | ----- | ----- | -------------------------------------- |└── AppLog.ts //调试日志管理
        | ----- | ----- | ----└── AlartMsg.ts //提示框，暂未用到 |
        | ----- | ----- | -------------------------------------- |└── <b>Config.ts</b> //大陆信息等配置文件
        | ----- | ----- | ----└── CreateTiledData.ts //地图数据打印 |
        | ----- | ----- | ----------------------------------------- |└── Globle.ts
        | ----- | ----- | ----└── HttpMgr.ts |
        | ----- | ----- | ------------------ |└── Language.ts
        | ----- | ----- | ----└── LocalData.ts //存储数据管理 |
        | ----- | ----- | ----------------------------------- |└── NotificationCenter.ts
        |-----|-----|----└── Utils.ts //辅助函数
        </details>

        <details>
        <summary>
        |-----|-----|── <b>GameMgr</b> //游戏逻辑代码目录
        </summary>

        | ----- | ----- | ----└── Clouds.ts |
        | ----- | ----- | ----------------- |└── Land.ts  //基础地块对象
        | ----- | ----- | ----└── <b>LandMgr.ts</b>  //地图管理 |
        | ----- | ----- | ------------------------------------- |└── LandSelectMgr.ts  //大陆选择
        | ----- | ----- | ----└── SearchBtn.ts //搜索 |
        | ----- | ----- | --------------------------- |└── SmallMap.ts //小地图管理
        </details>

        <details>
        <summary>
        |-----|-----|── Scene //主场景代码目录
        </summary>
        
        |-----|-----|----└──Mainland.ts
        </details>
    </details>

      - <details>
    <summary>
    |-----|── Texture //纹理目录
    </summary>
    暂未用到
    </details>
    
  </details>