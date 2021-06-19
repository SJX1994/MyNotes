# Asset_add 接口说明
- `AssetName` 资源名称
- `SourceName` 来源名称
- `AssetPreview` 资源预览图
- `AssetZipFile` 资源包
- `AssetTags` 资源标签
- `AssetType` 资源类型

## 构造JS示例
```

			var fpa = document.getElementById("sourcePackage").files[0];
			var fpv = document.getElementById("sourcePreview").files[0];
			
			var an = document.getElementById("sourceName").value;
			
			var fd = new FormData();
			fd.append("AssetName", an);
			fd.append("AssetZipFile", fpa);
			fd.append("AssetPreview", fpv);

```

# 关于资源类型的说明
为了统一类型，资源类型传递时，只需要传递整型。
```
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum EASSET_TYPE
    {
        /// <summary>
        /// 默认类型（未声明）
        /// </summary>
        DEFAULT = 0,

        /// <summary>
        /// 材质
        /// </summary>
        MATERIAL = 10,

        /// <summary>
        /// 贴图
        /// </summary>
        TEXTURE = 20,

        /// <summary>
        /// 模型
        /// </summary>
        MODEL = 30,

        /// <summary>
        /// 图集
        /// </summary>
        ALTAS = 40,

        /// <summary>
        /// 视频
        /// </summary>
        VIDEO = 50,

        /// <summary>
        /// 脚本
        /// </summary>
        SCRIPT = 60,

        /// <summary>
        /// 插件
        /// </summary>
        PLUGIN = 70,

        /// <summary>
        /// 文章
        /// </summary>
        ARTICLE = 80
    }
```

