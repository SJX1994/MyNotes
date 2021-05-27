let packageName = "aida-inputer";
let fs = require('fire-fs');
let os = require('os');
let path = require('fire-path');
Editor.require(`packages://${packageName}/panel/result-item.js`)();
let prefabItem = Editor.require('packages://' + packageName + '/panel/item/prefab-item.js');
let fileStrings = []
let excludePaths = []
let defaultExcludePaths = ["assets/resources"]
const configFilePath = Editor.url('packages://' + packageName + '/config2.json');

// assettype2name: 
// { 'cc.Asset': 'native-asset',
// 'cc.AnimationClip': 'animation-clip',
// 'cc.AudioClip': 'audio-clip',
// 'cc.BitmapFont': 'bitmap-font',
// 'cc.CoffeeScript': 'coffeescript',
// 'cc.TypeScript': 'typescript',
// 'cc.JavaScript': 'javascript',
// 'cc.JsonAsset': 'json',
// 'cc.ParticleAsset': 'particle',
// 'cc.Prefab': 'prefab',
// 'cc.SceneAsset': 'scene',
// 'cc.SpriteAtlas': 'texture-packer',
// 'cc.SpriteFrame': 'sprite-frame',
// 'cc.Texture2D': 'texture',
// 'cc.TTFFont': 'ttf-font',
// 'cc.TextAsset': 'text',
// 'cc.LabelAtlas': 'label-atlas',
// 'cc.RawAsset': 'raw-asset',
// 'cc.Script': 'script',
// 'cc.Font': 'font',
// 'sp.SkeletonData': 'spine',
// 'cc.TiledMapAsset': 'tiled-map',
// 'dragonBones.DragonBonesAsset': 'dragonbones',
// 'dragonBones.DragonBonesAtlasAsset': 'dragonbones-atlas' },

let loadConfig = function () {
    if (!fs.existsSync(configFilePath)) {
        return '[]'
    }
    return fs.readFileSync(configFilePath);
}

let saveConfig = function (text) {
    let dirPath = path.dirname(configFilePath);
    if (!fs.existsSync(dirPath)) {
        fs.mkdirSync(dirPath);
    }
    fs.writeFileSync(configFilePath, text);
};

Editor.Panel.extend({
    style: fs.readFileSync(Editor.url(`packages://${packageName}/panel/index.css`), 'utf8'),
    template: fs.readFileSync(Editor.url(`packages://${packageName}/panel/index.html`), 'utf8'),

    $: {
        logTextArea: '#logTextArea',
        sourcePathInput: '#sourcePath',
    },

    ready() {
        prefabItem.init()
        let configText = loadConfig()
        defaultExcludePaths = JSON.parse(configText)
        excludePaths = defaultExcludePaths
        let str = ""
        let length = excludePaths.length
        for (let i = 0; i < length; i++) {
            let path = excludePaths[i]
            if (path == "assets/resources") {
                continue
            }
            if (i != length - 1) {
                str = str + excludePaths[i] + ";"
            } else {
                str = str + excludePaths[i]
            }
        }
        let sourcePathInput = this.$sourcePathInput
        sourcePathInput.value = str
        let logCtrl = this.$logTextArea;
        let logListScrollToBottom = function () {
            setTimeout(function () {
                logCtrl.scrollTop = logCtrl.scrollHeight;
            }, 10);
        };

        window.plugin = new window.Vue({
            el: this.shadowRoot,
            created() {
                this.initBtnClick()
            },
            init() {
            },
            data: {
                logView: "",
                resultArray: [],
            },
            methods: {
                _addLog(str) {
                    let time = new Date();
                    this.logView += "[" + time.toLocaleString() + "]: " + str + "\n";
                    logListScrollToBottom();

                },
                saveConfigBtnClick() {
                    let json = JSON.stringify(excludePaths)
                    saveConfig(json)
                },
                exclude(resultPath) {
                    let path = resultPath
                    let winOS = os.platform() == "win32"
                    let length = excludePaths.length
                    let has = false
                    if (winOS) {
                        path = path.replace(/\\/g, '/')
                    }
                    for (let i = 0; i < length; i++) {
                        let filter = excludePaths[i]
                        if (path.includes(filter)) {
                            has = true
                            break
                        }
                    }
                    if (!has) {
                        return path
                    } else {
                        return null
                    }
                },
                //初始化
                initBtnClick() {
                    let self = this
                    fileStrings = []
                    let total = 0
                    let curCount = 0
                    Editor.assetdb.queryAssets('db://assets/**\/*', ['prefab', 'bitmap-font', 'scene', 'animation-clip'], function (err, results) {
                        total = results.length
                        self._addLog("总加载资源数：" + total)
                        results.forEach(function (result) {
                            let path = result.path
                            if (result.type == 'bitmap-font') {
                                path = result.path + ".meta"
                            }
                            fs.readFile(path, 'utf-8', function (err, data) {
                                curCount++
                                if (err) {
                                    console.error(err);
                                }
                                else {
                                    let cacheData = {}
                                    cacheData["content"] = data.toString()
                                    cacheData["url"] = result.url
                                    cacheData["path"] = result.path
                                    cacheData["uuid"] = result.uuid
                                    cacheData["type"] = result.type
                                    fileStrings.push(cacheData)
                                }
                                if (curCount == total) {
                                    self._addLog("初始化完成！")
                                }
                            });
                        });
                    });
                },


                onBtnClickSelectSheet() {
                },
                onBtnClickDel(data) {//删除按钮点击
                    Editor.assetdb.delete([data.url]);
                    this._addLog("删除资源：" + data.url)
                    if (data.pngUrl) {
                        Editor.assetdb.delete([data.pngUrl]);
                        this._addLog("删除资源：" + data.pngUrl)
                    }
                    this.removeAssetFromArray(data.uuid);
                },

                dropFile(event) {
                    event.preventDefault();
                    let files = event.dataTransfer.files;
                    if (files.length > 0) {
                        let file = files[0].path;
                        console.log(file);
                    } else {
                        console.log("no file");
                    }
                },
                drag(event) {
                    event.preventDefault();
                    event.stopPropagation();
                },
                removeAssetFromArray(uuid) {
                    for (let i = 0; i < this.resultArray.length; i++) {
                        const element = this.resultArray[i];
                        if (element.uuid == uuid) {
                            this.resultArray.splice(i, 1);
                            return
                        }
                    }
                },
                sourcePathChange(customEvent) {
                    let winOS = os.platform() == "win32"

                    excludePaths = ["assets/resources"]

                    if (customEvent.target.value != "") {
                        let paths = customEvent.target.value.split(';')
                        let length = paths.length
                        for (let i = 0; i < length; i++) {
                            let path = paths[i]
                            if (winOS) {
                                path = path.replace(/\\\\/g, '/')
                                path = path.replace(/\\/g, '/')
                                path = path.replace(/\/\//g, '/')
                            }
                            excludePaths.push(path)
                        }
                        this._addLog("排除查找路径修改：" + excludePaths)
                    } else {
                        this._addLog("排除查找路径修改：" + excludePaths)
                    }
                }
            }
        });
    }
});