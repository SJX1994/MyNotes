# UI 操作文档
## 架构
- Dlg面板
  - 描述：用于创建新面板窗口
  - 案例：Assets/Bundles/UI/Dlg/DlgNPCShop.prefab
- ES组件
  - 描述：封装的可复用（超过一处使用）元素集合
  - 案例：Assets/Bundles/UI/Common/Bag/ES_BagEquipCell.prefab
- item元素封装的可复用元素集合
  - 描述：为代码访问需求最小的元素
  - 案例：Assets/Bundles/UI/Item/MainUI/Item_Main_Chat.prefab
## 命名规范
- `E`（Entity）开头：程序需要访问
- `Item_`：为代码访问需求最小的元素
- `ES_`：封装的可复用元素集合
- `Dlg`：面板窗口（最大的集合）
## 依赖组件API
- [UGUI](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/index.html)
- [EUI](https://www.bilibili.com/video/BV12F411e7bP/?spm_id_from=333.337.search-card.all.click&vd_source=462273e904a605ba75ed3099598ed865)
- tips：无遮挡不勾选 Image 组件的 Raycast Target
- tips：记得为AssetBundle打包命名
## 资源文件
- 分类存放依据：
  - 为了运行时最有性能，按照功能一个文件夹代表一个图集。
  - 大图为了减少图集的无效像素，存放在文件夹根目录，不进入图集。
- UI引擎路径：
  - Bundles/UI(所有预制体存放处)
    - Common(公共UI:`ES_`的存放处)
  - Res/UI(所有2D精灵存放处)
    - UIIcon
    - UITex
- tips：如果已有类似结构的UI文件目录，复制 大于 创建。
- [UI设计资源](https://app.mockplus.cn/app/7DyQnIglMW/design/design?hmsr=share1)
- [UI切图资源](sjzj\Resources)
## 插件
- unity自动排版（预研）
  - ref：https://github.com/kyubuns/Baum2
  - js脚本存放路径:D:\Apps\PhotoShop\App\Adobe Photoshop 2021\Presets\Scripts
