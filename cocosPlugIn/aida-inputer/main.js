'use strict';
//var g_nameForAnimPrefab = "03AniCenterCity" + "_night";
var g_nameForAnimPrefab = "03AniWindMill" + "_night";
//问题无法在变量中动态读取json的值
const MY_PATH_LEN =
      [
            {
                  g_animDB: "/assets/resources/animation/",
                  g_animPath: "E:/Cocos/gitDepot",
                  g_animName: g_nameForAnimPrefab
            },
            {
                  g_prefabDB: "/assets/resources/Preform/",
                  g_prefabPath: "E:/Cocos/gitDepot",
                  g_prefabName: g_nameForAnimPrefab
            },
            {
                  g_spriteDB: "/assets/resources/resources/AidaCenter01",
                  g_spritePath: "E:/Cocos/gitDepot",

            }
      ];
const MY_COCOS_JSON =
      [
            {
                  //正常60帧的动画节点
                  g_animJson: "{\n \"__type__\"\:\"cc.AnimationClip\",\n \"_name\"\:\"\",\n \"_objFlags\"\:\"0\",\n \"_duration\"\:\"0\",\n \"sample\"\:\"60\",\n \"curveData\"\:{},\n \"events\"\:[]\n}",
            },
            {
                  //挂载动画和精灵的预制体string形式
                  g_prefabJsonString: "[\n {\n  \"__type__\": \"cc.Prefab\",\n  \"_name\": \"\",\n  \"_objFlags\": 0,\n  \"_native\": \"\",\n  \"data\": {\"__id__\": 1},\n  \"optimizationPolicy\": 0,\n  \"asyncLoadAssets\": false,\n  \"readonly\": false\n},\n ]",
            },
            {

                  //json形式
                  g_prefabJsonFormat:
                        [
                              {
                                    "__type__": "cc.Prefab",
                                    "_name": "",
                                    "_objFlags": 0,
                                    "_native": "",
                                    "data": {
                                          "__id__": 1
                                    },
                                    "optimizationPolicy": 0,
                                    "asyncLoadAssets": false,
                                    "readonly": false
                              },
                              {
                                    "__type__": "cc.Node",
                                    "_name": "",
                                    "_objFlags": 0,
                                    "_parent": null,
                                    "_children": [],
                                    "_active": true,
                                    "_components": [
                                          {
                                                "__id__": 2
                                          }
                                    ],
                                    "_prefab": {
                                          "__id__": 3
                                    },
                                    "_opacity": 255,
                                    "_color": {
                                          "__type__": "cc.Color",
                                          "r": 255,
                                          "g": 255,
                                          "b": 255,
                                          "a": 255
                                    },
                                    "_contentSize": {
                                          "__type__": "cc.Size",
                                          "width": 68,
                                          "height": 43
                                    },
                                    "_anchorPoint": {
                                          "__type__": "cc.Vec2",
                                          "x": 0.5,
                                          "y": 0.5
                                    },
                                    "_trs": {
                                          "__type__": "TypedArray",
                                          "ctor": "Float64Array",
                                          "array": [
                                                569.2150268554688,
                                                987.322998046875,
                                                0,
                                                0,
                                                0,
                                                0,
                                                1,
                                                1,
                                                1,
                                                1
                                          ]
                                    },
                                    "_eulerAngles": {
                                          "__type__": "cc.Vec3",
                                          "x": 0,
                                          "y": 0,
                                          "z": 0
                                    },
                                    "_skewX": 0,
                                    "_skewY": 0,
                                    "_is3DNode": false,
                                    "_groupIndex": 3,
                                    "groupIndex": 3,
                                    "_id": ""
                              },
                              {
                                    "__type__": "cc.Animation",
                                    "_name": "",
                                    "_objFlags": 0,
                                    "node": {
                                          "__id__": 1
                                    },
                                    "_enabled": true,
                                    "_defaultClip": {
                                          "__uuid__": "301bca82-db0b-46a5-a0e0-220388acd0cb"
                                    },
                                    "_clips": [
                                          {
                                                "__uuid__": "301bca82-db0b-46a5-a0e0-220388acd0cb"
                                          }
                                    ],
                                    "playOnLoad": true,
                                    "_id": ""
                              },
                              {
                                    "__type__": "cc.PrefabInfo",
                                    "root": {
                                          "__id__": 1
                                    },
                                    "asset": {
                                          "__uuid__": ""
                                    },
                                    "fileId": "",
                                    "sync": false
                              }

                        ]
            }
      ]

const g_animFile = MY_PATH_LEN[0].g_animPath + MY_PATH_LEN[0].g_animDB + g_nameForAnimPrefab + ".txt";
const g_prefabFile = MY_PATH_LEN[1].g_prefabPath + MY_PATH_LEN[1].g_prefabDB + g_nameForAnimPrefab + ".txt";
const g_spriteFile = MY_PATH_LEN[2].g_spritePath + MY_PATH_LEN[2].g_spriteDB;
const g_configBuf = "E:/Cocos/gitDepot/packages/aida-inputer/buffer.json";
const g_configInit = "E:/Cocos/gitDepot/packages/aida-inputer/config.json";




//在这修改路径







module.exports = {
      load() {
            // 当 package 被正确加载的时候执行
            Editor.log('牛逼插件已加载');
      },

      unload() {
            // 当 package 被正确卸载的时候执行
            Editor.log('牛逼插件被删除');
      },
      getAnimUuid() {

      },
      messages: {
            'open-init'() {
                  //TODO
                  Editor.log("gui未完成");

                  const fs = require('fire-fs');
                  const utf8 = { encoding: "utf8" };
                  let content = JSON.parse(fs.readFileSync(g_configInit, utf8));
                  this.g_nameForAnimPrefab = JSON.stringify(content.g_nameForAnimPrefab);



                  Editor.log(this.g_nameForAnimPrefab)

                  Editor.Panel.open('aida-inputer');


            },
            'creat-animation'() {
                  //初始化           
                  const fs = require('fire-fs');

                  const utf8 = { encoding: "utf8" };



                  //延迟执行
                  var delay = (function () {
                        var timer = 0;
                        return function (callback, ms) {
                              clearTimeout(timer);
                              timer = setTimeout(callback, ms);
                        };
                  })();

                  //开始创建
                  Editor.log('动画节点创建 ' + MY_PATH_LEN[0].g_animName);
                  let data = MY_COCOS_JSON[0].g_animJson;
                  let fileA = g_animFile.replace(/\.[^.]+$/, '.anim');

                  var i;

                  if (fs.existsSync(fileA)) {
                        Editor.log(fileA);

                        //uuid存入缓存
                        let content = JSON.parse(fs.readFileSync(g_configBuf, utf8));
                        let uuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[0].g_animDB + g_nameForAnimPrefab + '.anim');
                        Editor.log(uuid);
                        content.AnimBuffer = uuid;

                        fs.writeFileSync(g_configBuf, JSON.stringify(content), utf8, (err) => {
                              if (err) throw err;
                        });

                        Editor.log(MY_PATH_LEN[0].g_animName + ' 目录下动画已经存在' + '动画uuid已缓存：' + uuid);
                  } else {

                        fs.writeFile(g_animFile, data, utf8, (err) => {
                              if (err) throw err;
                        });

                        delay(function () {

                              const fs = require('fs');
                              fs.renameSync(g_animFile, fileA, (err) => {
                                    if (err) Editor.log(err);
                                    throw err;
                              });

                              Editor.assetdb.refresh('db:/' + MY_PATH_LEN[0].g_animDB);


                              delay(function () {
                                    //uuid存入缓存
                                    let content = JSON.parse(fs.readFileSync(g_configBuf, utf8));
                                    let uuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[0].g_animDB + g_nameForAnimPrefab + '.anim');
                                    Editor.log(uuid);
                                    content.AnimBuffer = uuid;

                                    fs.writeFileSync(g_configBuf, JSON.stringify(content), utf8, (err) => {
                                          if (err) throw err;
                                    });
                              }, 300);

                              Editor.log('创建完成：' + MY_PATH_LEN[0].g_animName + '动画uuid已缓存：' + uuid + ' 可以进行预制体的创建');
                        }, 500);

                  }



                  Editor.assetdb.refresh('db:/' + MY_PATH_LEN[0].g_animDB);



            },
            'creat-prefab'() {
                  //初始化
                  const fs = require('fs');
                  const utf8 = { encoding: "utf8" };
                  let content = JSON.parse(fs.readFileSync(g_configBuf, utf8));
                  let uuid = content.AnimBuffer;
                  let prefabUuid;
                  Editor.log(uuid);

                  //延迟执行
                  var delay = (function () {
                        var timer = 0;
                        return function (callback, ms) {
                              clearTimeout(timer);
                              timer = setTimeout(callback, ms);
                        };
                  })();

                  //开始创建
                  Editor.log('创建预制体开始执行' + MY_PATH_LEN[1].g_prefabName);

                  let data = MY_COCOS_JSON[2].g_prefabJsonFormat;
                  data[2]._defaultClip.__uuid__ = uuid;
                  data[2]._clips[0].__uuid__ = uuid;
                  data[1]._name = MY_PATH_LEN[1].g_prefabName;
                  //Editor.log(data[3]._clips[0].__uuid__);
                  Editor.assetdb.refresh('db:/' + MY_PATH_LEN[1].g_prefabDB);
                  delay(function () {

                        const dataStr = JSON.stringify(data);



                        //Editor.log(data);
                        let fileA = g_prefabFile.replace(/\.[^.]+$/, '.prefab');

                        if (fs.existsSync(fileA)) {
                              //截取prefab的uuid
                              // prefabUuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[1].g_prefabDB + g_nameForAnimPrefab + '.prefab');
                              //Editor.log(prefabUuid);
                              // Editor.Ipc.sendToAll('scene:enter-prefab-edit-mode', prefabUuid);

                              // let spriteFolderUuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[2].g_spriteDB);
                              // Editor.Selection.select('asset', spriteFolderUuid);

                              Editor.log(MY_PATH_LEN[1].g_prefabName + ' 目录下预制体已经存在,为您打开编辑界面,以及贴图所在的位置');
                        } else {
                              fs.writeFile(g_prefabFile, dataStr, utf8, (err) => {
                                    if (err) throw err;
                              });

                              delay(function () {


                                    fs.renameSync(g_prefabFile, fileA, (err) => {
                                          if (err) Editor.log(err); throw err;
                                    });

                                    Editor.assetdb.refresh('db:/' + MY_PATH_LEN[1].g_prefabDB);

                                    Editor.log('牛逼插件生成了：' + MY_PATH_LEN[1].g_prefabPath);
                                    delay(function () {
                                          //高亮贴图所在位置
                                          let spriteFolderUuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[2].g_spriteDB);
                                          Editor.Selection.select('asset', spriteFolderUuid);
                                          Editor.Ipc.sendToAll('assets:hint', spriteFolderUuid);

                                          delay(function () {
                                                //截取prefab的uuid
                                                prefabUuid = Editor.assetdb.urlToUuid('db:/' + MY_PATH_LEN[1].g_prefabDB + g_nameForAnimPrefab + '.prefab');
                                                Editor.Ipc.sendToAll('scene:enter-prefab-edit-mode', prefabUuid);
                                                Editor.log("nnnnnnb");
                                          }, 500)
                                    }, 500);
                              }, 500);
                        }

                  }, 100);
                  Editor.assetdb.refresh('db:/' + MY_PATH_LEN[1].g_prefabDB);





            }
      },

};

