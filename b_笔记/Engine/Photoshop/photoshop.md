文档规则：
    注释：
        (这是注释)
    省略：s
        ...
    包含：
        <
    值：
        >
    代码：
        <(代码)>

- photoshopScript
  - 官方文档：
    - Adobe Photoshop CC Scripting Guide：
      - 链接：https://www.adobe.com/content/dam/acom/en/devnet/photoshop/pdfs/photoshop-cc-scripting-guide-2019.pdf
      - 中文：http://nullice.com/archives/1822
    - Adobe Photoshop CC JavaScript Reference
      - 链接：https://www.adobe.com/content/dam/acom/en/devnet/photoshop/pdfs/photoshop-cc-javascript-ref-2019.pdf
      - 其他：https://theiviaxx.github.io/photoshop-docs/Photoshop/
      - 中文：https://juejin.cn/post/6844903432495562766
  - 软件结构：
    - Application
      - Notifier(通知)
      - Document(文档)
      - Preferences(用户预设)
    - 细节：
      - Selection(选项)
      - PathItem(路径)
        - SubPathItem(子路径)
        - PathPoint(路径节点)
      - Channel(通道)
      - ArtLayer(艺术创作层)
        - TextItem(object)(文本对象)
      - LayerComp(图层组件)
      - LayerSet(图层集合)
        - LayerSet(图层集合)
        - ArtLayer(艺术图层)
      - CountItem(记数层)
      - ColorSampler(颜色采样)
      - MeasurementScale(测量表)
      - DocumentInfo(文档信息)
      - HistoryState(历史状态)
  - 开始:
    - 安装目录下：PhotoShopxxx\Presets\Scripts 存放脚本
    - 代码提示：  https://github.com/bbb999/Types-for-Adobe
      - 国内镜像：
      - https://github.com.cnpmjs.org/bbb999/Types-for-Adobe.git
    - reactJs/ExtendScript:
      - ArtLayer
        - 创建：
          - var artLayerRef = docRef.artLayers.add();
            artLayerRef.kind = LayerKind.TEXT;
          
        - 访问：
          - 通过名字访问：
            - var layer = app.activeDocument.artLayers.getByName(layer);
              layer.allLocked = true;
          - 访问字符：
            - myLayer[i].textItem.contents = "xxx";
        - textItem
          - 换行：
            - var theText = text.replace(/[\r\n]+/gm, "\r");
            - TEXT_GROUP.artLayers.getByName(layer).textItem.contents = theText;

- photoshopAPIforHtml(CEP)
  - 

- 插件：
  - photoShop2Spine：https://github.com/EsotericSoftware/spine-scripts/tree/master/photoshop