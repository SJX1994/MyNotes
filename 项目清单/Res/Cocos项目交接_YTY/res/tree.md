```
├── assets // 游戏代码资源
│   ├── res  // 游戏图片资源，该目录引用到的图片会被打包发布，没用到的不会打包
│   ├── resources //游戏各类资源目录，该目录所有资源必会打包
│   │   ├── animation //动画资源
│   │   |    └── aircraft.anim
│   │   |    └── Bonfire.anim
│   │   |    └── carousel_night.anim
│   │   |    └── carousel.anim
│   │   |    └── Castle_night.anim
│   │   |    └── Castle.anim
│   │   |    └── dachangjing_night.anim
│   │   |    └── dachangjing.anim
│   │   |    └── Dragon.anim
│   │   |    └── fengche.anim
│   │   |    └── FerrisWheel_night.anim
│   │   |    └── FerrisWheel.anim
│   │   |    └── Glowworm.anim
│   │   |    └── hudie.anim
│   │   |    └── jingyu.anim
│   │   |    └── loading.anim
│   │   |    └── mountain01.anim
│   │   |    └── ship02.anim
│   │   |    └── ship04.anim
│   │   |    └── ship05.anim
│   │   |    └── Watermonster.anim
│   │   |    └── windmill2.anim
│   │   |    └── xiaochuan.anim
│   │   |── Atlantis  //亚特兰蒂斯大陆资源
│   │   |    |── Landforms //基础地块图片资源
│   │   |    |      └── LandTiled.plist
│   │   |    |      └── LandTiled.png
│   │   |    |      └── LandTiled_night.plist
│   │   |    |      └── LandTiled_night.png
│   │   |    |      └── SEKUAI01.png  //地图编辑器所用资源
│   │   |    └── landscape //景观图片资源
│   │   |    └── Atlantis //瓦片地图文件
│   │   |    └── readme
│   │   |── Byzantium  //拜占庭大陆资源
│   │   |    └── Landforms
│   │   |    |      └── LandTiled.plist
│   │   |    |      └── LandTiled.png
│   │   |    |      └── LandTiled_night.plist
│   │   |    |      └── LandTiled_night.png
│   │   |    |      └── SEKUAI01.png 
│   │   |    └── landscape
│   │   |    └── Atlantis
│   │   |    └── readme
│   │   └── Prefabs  //预制体目录
│   │   |    └── attenzione.prefab //禁止点击标识
│   │   |    └── generalSprite.prefab //一般用纹理
│   │   |    └── hoverIcon.prefab //触摸地块区域标识
│   │   |    └── landform.prefab //基础地块
│   │   |    └── landscape.prefab //景观
│   │   |    └── locationTips.prefab //坐标提示牌
│   │   |── Preform  //动画预制文件目录
│   │   |    └── Aircraft.prefab
│   │   |    └── Aircraft_night.prefab
│   │   |    └── Bonfire.prefab
│   │   |    └── Butterfly.prefab
│   │   |    └── Butterfly_night.prefab
│   │   |    └── carousel.prefab
│   │   |    └── carousel_night.prefab
│   │   |    └── Castle.prefab
│   │   |    └── Castle_night.prefab
│   │   |    └── city.prefab
│   │   |    └── city_night.prefab
│   │   |    └── Cloud.prefab
│   │   |    └── Cloud_night.prefab
│   │   |    └── Dragon.prefab
│   │   |    └── Dragon_night.prefab
│   │   |    └── FerrisWheel.prefab
│   │   |    └── FerrisWheel_night.prefab
│   │   |    └── Glowworm.prefab
│   │   |    └── loading.prefab
│   │   |    └── mountain01.prefab
│   │   |    └── mountain01_night.prefab
│   │   |    └── ship02.prefab
│   │   |    └── ship02_night.prefab
│   │   |    └── ship04 - night.prefab
│   │   |    └── ship04.prefab
│   │   |    └── ship05 - night.prefab
│   │   |    └── ship05.prefab
│   │   |    └── Water monster.prefab
│   │   |    └── Water monster_night.prefab
│   │   |    └── whale.prefab
│   │   |    └── whale_night.prefab
│   │   |    └── windmill.prefab
│   │   |    └── windmill_night.prefab
│   │   |    └── windmill2.prefab
│   │   |    └── windmill2_night.prefab
│   ├── Scene //游戏场景目录
│   │   ├── Mainland //主场景
│   ├── Script //游戏代码
│   │   ├── ComUnit //公共配置和组件目录
│   │   |    └── AlartMsg.ts //提示框，暂未用到
│   │   |    └── AppLog.ts //调试日志管理
│   │   |    └── AlartMsg.ts //提示框，暂未用到
│   │   |    └── Config.ts //大陆信息等配置文件
│   │   |    └── CreateTiledData.ts //地图数据打印
│   │   |    └── Globle.ts
│   │   |    └── HttpMgr.ts
│   │   |    └── Language.ts
│   │   |    └── LocalData.ts //存储数据管理
│   │   |    └── NotificationCenter.ts
│   │   |    └── Utils.ts //辅助函数
│   │   ├── GameMgr //游戏逻辑代码目录
│   │   |    └── Clouds.ts
│   │   |    └── Land.ts  //基础地块对象
│   │   |    └── LandMgr.ts  //地图管理
│   │   |    └── LandSelectMgr.ts  //大陆选择
│   │   |    └── SearchBtn.ts //搜索
│   │   |    └── SmallMap.ts //小地图管理
│   │   ├── Scene //主场景代码目录
│   │   |    └──Mainland.ts
└   └── Texture //纹理目录，暂未用到