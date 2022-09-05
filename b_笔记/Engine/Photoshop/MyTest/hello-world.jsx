
//extend script
//#target photoshop;

const FILE_PATH = "D:/SoftWare/PhotoShop/Adobe Photoshop CC 2019/Presets/Scripts/MyTest/";

#include "D:/SoftWare/PhotoShop/Adobe Photoshop CC 2019/Presets/Scripts/MyTest/json2.js";
//alert('启动');

var D_O_C = app.activeDocument;
const CONFIG_PATH = FILE_PATH + "config3.json";
var TEXT_GROUP = D_O_C.layerSets.add();
TEXT_GROUP.name = "source";


function PixelToTextDot(Pixel) { return Pixel / 3.36 }

function readJSONFile(file) {
    file.open("r");
    var data = file.read();
    file.close();
    data = JSON.parse(data);
    return data;
}
function creatText(layer) {
    var newlayer = TEXT_GROUP.artLayers.add();
    newlayer.kind = LayerKind.TEXT;
    newlayer.name = layer;
    return layer;
}

function findText(layer) {
    var layerT = TEXT_GROUP.artLayers.getByName(layer);
    return layerT.textItem;
}



function textProperty(namei, text, pos, rgb, pxHeigh, fontIn, direction) {

    if (typeof (text) == "object") {
        var i;
        for (i = 0; i < text.length; i++) {
            findText(namei).contents = text[i];
        }
    }
    else {
        var theText = text.replace(/[\r\n]+/gm, "\r");
        findText(namei).contents = theText;
        findText(namei).hyphenateWordsLongerThan = 15;
        //findText(namei).size = new UnitValue(100, 'px');
    }
    findText(namei).position = [pos[0], pos[1]];
    switch (direction) {
        case "H":
            findText(namei).direction = Direction.HORIZONTAL;
            break;
        case "V":
            findText(namei).direction = Direction.VERTICAL;
            break;
    }

    var textColor = new SolidColor();
    textColor.rgb.red = rgb[0];
    textColor.rgb.green = rgb[1];
    textColor.rgb.blue = rgb[2];
    findText(namei).color = textColor;
    findText(namei).size = PixelToTextDot(pxHeigh);
    //findText(namei).font = "GillSansMT-Bold";
    findText(namei).font = fontIn;
    var just = Justification.LEFT;
    findText(namei).justification = just;

    return findText(namei);
}

//main
if (D_O_C.width.value > D_O_C.height.value) {
    alert("检测到为横向排版");
}
else {
    var jsonFile = File(CONFIG_PATH);
    var config = readJSONFile(jsonFile);

    alert("检测到为纵向排版");

    var i;
    for (i = 0; i < config.length; i++) {
        var namei = config[i].namei;
        var text = config[i].text;
        var pos = config[i].pos;
        var rgb = config[i].rgb;
        var pxHeigh = config[i].pxHeigh;
        var fontIn = config[i].fontIn;
        var direction = config[i].direction;

        var name = creatText(i);
        var text = textProperty(i, text, pos, rgb, pxHeigh, fontIn, direction);
    }

    alert("总共生成字符：" + config.length);



}   