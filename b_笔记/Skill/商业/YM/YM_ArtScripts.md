# 业务逻辑与渲染表现交互脚本
- 存放位置：
  - Assets/Scripts/Loader/Bridge
## 渲染质量配置器
- 作用：
  - 用于配置渲染表现
  - 方法：YM_SetQulityLevel(int index)
    - index 为 0：低配
    - index 为 1：中配
    - index 为 2：高配
# 艺术家GIT拉取流程
- ref：
  - ref1：https://blog.miniasp.com/post/2022/05/17/Down-size-your-Monorepo-with-Git-Sparse-checkouts
  - ref2：https://www.jianshu.com/p/680f2c6c84de
- git登录：http://61.160.97.94:8929/game
  - 内网：192.168.124.11:8929
- git地址：http://61.160.97.94:8929/game/sjzj/sjzj.git
- 稀疏拉取：git clone --depth 1 --filter=blob:none --sparse http://61.160.97.94:8929/game/sjzj/sjzj.git
- 打开稀疏拉取功能：git config core.sparsecheckout true
- 新建稀疏拉取策略：使用文本编辑打开 .git/info/sparse-checkout 文件 (没有这个文件可以手动创建一个)
- 稀疏拉取艺术家文件夹：git sparse-checkout set "Project/Unity"
- 打开所有：git sparse-checkout disable
# AB包整合
- 目录：
- Assets/Bundles/Unit
# 拉下报错
- 跑一下编译：
  - D:\Projects\YM\sjzj\Project\ExportConfig.bat