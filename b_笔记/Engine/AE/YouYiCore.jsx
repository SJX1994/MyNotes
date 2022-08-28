{
  /*------------------------*/
  function YouYi(Global) {
    /*------------------------*/
    Global.RET = "\r";
    //-------------------------------------------------------------------------------------------- About info

    Global.ABOUT =
      "有一动画, v1.00 (2020)" +
      Global.RET +
      "选择一个功能模块。" +
      Global.RET +
      "在模块面板内调整参数，找到模块提示的资产挂上。" +
      Global.RET +
      Global.RET +
      "作者 未来 (752523247@qq.com)" +
      Global.RET +
      Global.RET +
      "联系电话 (15365119616)" +
      Global.RET +
      "更多插件开发请联系 ";

    Global.WORKINGBTN =
      "使用方法：" +
      Global.RET +
      "选中图层点击按钮" +
      Global.RET +
      "调节位置关键帧";

    //---------------------------------------------------------------------------------------- Global Variables

    Global.working_幅度 = 9;
    Global.working_频率 = 10;
    Global.递增数量 = 10;
    Global.惊讶背景_最小值 = 30;
    Global.惊讶背景_最大值 = 90;
    Global.惊讶背景_速度 = 30;
    Global.惊讶背景_密度 = 10;
    Global.惊讶背景_数量 = 50;
    Global.弹动一切_最大弹动幅度 = 3;
    Global.弹动一切_速度 = 30;
    Global.弹动一切_弹动次数 = 6;
  }

  /*------------------------*/
  function YouYiUI(UIdraw) {
    /*------------------------*/
    //legend...........................('Window type', 'Name of palette', [left edge, top edge, right edge, bottom edge];
    try {
      var filepass = new File(new File($.fileName).parent);
      var icon = filepass.fullName + "/YouYi_Images";
    } catch (err) {
      alert("错误路径");
    }

    UIunit = 10;
    UIwidth = 275;
    UIheight = UIunit * 28;
    //------------------------------------------------------------------------------------- 头部主界面绘制
    UIdraw.main = new Window("palette", "", [0, 100, UIwidth, UIheight * 3]);

    UIdraw.main.name = UIdraw.main.add(
      "statictext",
      [UIunit, UIunit * 1.5, UIwidth - UIunit * 17, UIunit * 3],
      "有一动画:"
    );
    try {
      UIdraw.main.aboutButton = UIdraw.main.add(
        "iconbutton",
        [UIwidth - UIunit * 3, UIunit - 2, UIwidth - UIunit, UIunit * 3 + 2],
        icon + "/logo.png"
        //,{ style: "toolbutton" }
      );
    } catch (err) {
      UIdraw.main.aboutButton = UIdraw.main.add(
        "button",
        [UIwidth - UIunit * 6, UIunit - 2, UIwidth - UIunit, UIunit * 3 + 2],
        "About"
      );
      alert("欢迎使用");
    }
    UIdraw.main.add(
      "panel",
      [UIunit, UIunit * 4, UIwidth - UIunit, UIunit * 4 + 3],
      ""
    );

    //------------------------------------------------------------------------------------- 绘制走路表达式按钮
    {
      UIdraw.main.workingFDtext = UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 5, UIunit * 9, UIunit * 7 + 2],
        "频率："
      );
      UIdraw.main.workingFD = UIdraw.main.add(
        "editText",
        [UIunit * 4, UIunit * 5, UIunit * 6, UIunit * 7 + 2],
        Global.working_幅度
      );
      UIdraw.main.workingPLtext = UIdraw.main.add(
        "staticText",
        [UIunit * 6, UIunit * 5, UIunit * 9, UIunit * 7 + 2],
        "幅度："
      );
      UIdraw.main.workingPL = UIdraw.main.add(
        "editText",
        [UIunit * 9, UIunit * 5, UIunit * 12, UIunit * 7 + 2],
        Global.working_频率
      );
      UIdraw.main.workingButton = UIdraw.main.add(
        "button",
        [UIunit * 12 + 4, UIunit * 5, UIwidth - UIunit * 9, UIunit * 7 + 2],
        "人物走路"
      );
      UIdraw.main.workingAboutButton = UIdraw.main.add(
        "button",
        [UIwidth - UIunit * 8, UIunit * 5, UIwidth - UIunit, UIunit * 7 + 2],
        "？"
      );
      UIdraw.main.add(
        "panel",
        [UIunit, UIunit * 8, UIwidth - UIunit, UIunit * 8 + 3],
        ""
      );
    }
    //------------------------------------------------------------------------------------- 循环功能按钮
    //循环方式：pingpong，cycle
    {
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 8.6, UIwidth - UIunit, UIunit * 10.6],
        "循环方式："
      );
      UIdraw.main.CYCLE = UIdraw.main.add(
        "radiobutton",
        [UIunit * 8, UIunit * 8.6, UIwidth - UIunit, UIunit * 10.6],
        "cycle"
      );
      UIdraw.main.PINGPONG = UIdraw.main.add(
        "radiobutton",
        [UIunit * 13, UIunit * 8.6, UIwidth - UIunit, UIunit * 10.6],
        "pingpong"
      );
      UIdraw.main.LOOP = UIdraw.main.add(
        "button",
        [UIunit * 20, UIunit * 8.6, UIwidth - UIunit, UIunit * 10.6],
        "循环"
      );
      UIdraw.main.PINGPONG.value = true;
      UIdraw.main.add(
        "panel",
        [UIunit, UIunit * 11, UIwidth - UIunit, UIunit * 11 + 3],
        ""
      );
    }
    //------------------------------------------------------------------------------------- update功能按钮
    //递增函数
    {
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 11.6, UIwidth - UIunit, UIunit * 13.6],
        "递增参数："
      );
      UIdraw.main.递增数量 = UIdraw.main.add(
        "editText",
        [UIunit * 8, UIunit * 11.6, UIunit * 11, UIunit * 13.6],
        Global.递增数量
      );
      UIdraw.main.单值递增 = UIdraw.main.add(
        "radiobutton",
        [UIunit * 12, UIunit * 11.6, UIwidth - UIunit, UIunit * 13.6],
        "单"
      );
      UIdraw.main.Y值递增 = UIdraw.main.add(
        "radiobutton",
        [UIunit * 16, UIunit * 11.6, UIwidth - UIunit, UIunit * 13.6],
        "Y"
      );
      UIdraw.main.X值递增 = UIdraw.main.add(
        "radiobutton",
        [UIunit * 19, UIunit * 11.6, UIwidth - UIunit, UIunit * 13.6],
        "X"
      );
      UIdraw.main.递增按钮 = UIdraw.main.add(
        "button",
        [UIunit * 11, UIunit * 14.6, UIunit * 24, UIunit * 16.6],
        "递增函数"
      );
      UIdraw.main.单值递增.value = true;
      UIdraw.main.add(
        "panel",
        [UIunit, UIunit * 17.6, UIwidth - UIunit, UIunit * 17.6 + 3],
        ""
      );
    }
    //------------------------------------------------------------------------------------- 惊讶动态背景
    //随即距离ab,速度s,密度
    {
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 18.6, UIwidth - UIunit, UIunit * 20.6],
        "最大值"
      );
      UIdraw.main.惊讶背景最大随机区间 = UIdraw.main.add(
        "editText",
        [UIunit * 6, UIunit * 18.6, UIwidth - UIunit * 18, UIunit * 20.6],
        Global.惊讶背景_最大值
      );
      UIdraw.main.add(
        "staticText",
        [UIunit * 10, UIunit * 18.6, UIwidth - UIunit, UIunit * 20.6],
        "最小值"
      );
      UIdraw.main.惊讶背景最小随机区间 = UIdraw.main.add(
        "editText",
        [UIunit * 15, UIunit * 18.6, UIwidth - UIunit * 8, UIunit * 20.6],
        Global.惊讶背景_最小值
      );
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 20.6, UIwidth - UIunit, UIunit * 22.6],
        "速度"
      );
      UIdraw.main.惊讶背景速度 = UIdraw.main.add(
        "editText",
        [UIunit * 6, UIunit * 20.6, UIwidth - UIunit * 18, UIunit * 22.6],
        Global.惊讶背景_速度
      );
      UIdraw.main.add(
        "staticText",
        [UIunit * 10, UIunit * 20.6, UIwidth - UIunit, UIunit * 22.6],
        "密度"
      );
      UIdraw.main.惊讶背景密度 = UIdraw.main.add(
        "editText",
        [UIunit * 15, UIunit * 20.6, UIwidth - UIunit * 8, UIunit * 22.6],
        Global.惊讶背景_密度
      );
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 22.6, UIwidth - UIunit, UIunit * 24.6],
        "个数"
      );
      UIdraw.main.惊讶背景数量 = UIdraw.main.add(
        "editText",
        [UIunit * 6, UIunit * 22.6, UIwidth - UIunit * 18, UIunit * 24.6],
        Global.惊讶背景_数量
      );
      UIdraw.main.惊讶背景 = UIdraw.main.add(
        "button",
        [UIunit * 11, UIunit * 22.6, UIunit * 24, UIunit * 24.6],
        "惊讶背景"
      );
      UIdraw.main.add(
        "panel",
        [UIunit, UIunit * 24.6, UIwidth - UIunit, UIunit * 24.6 + 3],
        ""
      );
    }
    //------------------------------------------------------------------------------------- 标记弹动
    {
      //幅度，速度，次数
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 26.6, UIwidth - UIunit, UIunit * 28.6],
        "幅度"
      );
      UIdraw.main.弹动一切_最大弹动幅度 = UIdraw.main.add(
        "editText",
        [UIunit * 6, UIunit * 26.6, UIwidth - UIunit * 18, UIunit * 28.6],
        Global.弹动一切_最大弹动幅度
      );
      UIdraw.main.add(
        "staticText",
        [UIunit * 10, UIunit * 26.6, UIwidth - UIunit, UIunit * 28.6],
        "速度"
      );
      UIdraw.main.弹动一切_速度 = UIdraw.main.add(
        "editText",
        [UIunit * 15, UIunit * 26.6, UIwidth - UIunit * 8, UIunit * 28.6],
        Global.弹动一切_速度
      );
      UIdraw.main.add(
        "staticText",
        [UIunit, UIunit * 29.6, UIwidth - UIunit, UIunit * 31.6],
        "次数"
      );
      UIdraw.main.弹动一切_弹动次数 = UIdraw.main.add(
        "editText",
        [UIunit * 6, UIunit * 29.6, UIwidth - UIunit * 18, UIunit * 31.6],
        Global.弹动一切_弹动次数
      );
      UIdraw.main.弹动一切 = UIdraw.main.add(
        "button",
        [UIunit * 11, UIunit * 29.6, UIunit * 24, UIunit * 31.6],
        "弹动一切"
      );
    }

    //------------------------------------------------------------------------------ 主界面功能设计

    UIdraw.main.center();
    UIdraw.main.show();
    {
      //About button
      UIdraw.main.aboutButton.onClick = function () {
        alert(Global.ABOUT);
      };
      //working Button
      UIdraw.main.workingButton.onClick = function () {
        var selected = app.project.activeItem.selectedLayers;

        if (selected.length == 0) {
          alert("请选择一个或者多个位移选项");
        } else {
          for (var i = 0; i < selected.length; i++) {
            var layer = selected[i];
            layer.position.addKey(0.5);
            layer.position.addKey(3);
            // alert(expressionInput);
            layer.position.expression =
              "t =0; x = position[0];y = position[1];st = position.key(1).time;et = position.key(2).time;if(time>st&&time<et){while (t <= time * 15) {y ;x ;t++}y;[x, y + Math.cos(time * " +
              UIdraw.main.workingFD.text +
              ") * " +
              UIdraw.main.workingPL.text +
              "];}else{value;}";
          }
        }
      };
      UIdraw.main.LOOP.onClick = function () {
        var selected = app.project.activeItem.selectedProperties;
        if (selected.length == 0) {
          alert("请选择一个带有两个关键帧的属性");
        } else {
          for (var i = 0; i < selected.length; i++) {
            if (UIdraw.main.PINGPONG.value == true) {
              // alert(expressionInput);
              selected[i].expression =
                'loopOut(type ="pingpong", numKeyframes = 0)';
            } else if (UIdraw.main.CYCLE.value == true) {
              selected[i].expression =
                'loopOut(type ="cycle", numKeyframes = 0)';
            }
          }
        }
      };
      UIdraw.main.递增按钮.onClick = function () {
        var selected = app.project.activeItem.selectedProperties;
        if (selected.length == 0) {
          alert("请选择一个带有两个关键帧的属性");
        } else {
          for (var i = 0; i < selected.length; i++) {
            if (UIdraw.main.单值递增.value == true) {
              selected[i].expression =
                "x =0;t =0;while(x<=time*60){t+=" +
                UIdraw.main.递增数量.text +
                ";x++}t;";
            } else if (UIdraw.main.X值递增.value == true) {
              selected[i].expression =
                "x =0;t =0;while(x<=time*60){t+=" +
                UIdraw.main.递增数量.text +
                ";x++}[value[0]+t,value[1]];";
            } else if (UIdraw.main.Y值递增.value == true) {
              selected[i].expression =
                "x =0;t =0;while(x<=time*60){t+=" +
                UIdraw.main.递增数量.text +
                ";x++}[value[0],value[1]+t];";
            }
          }
        }
      };
      UIdraw.main.惊讶背景.onClick = function () {
        var selected = app.project.activeItem.selectedLayers;
        var counter = 0;
        if (selected.length == 0) {
          alert("请制作一个90度向上的单个图形，锚点放在根部，并选中");
        } else {
          for (var i = 0; i < selected.length; i++) {
            var layer = selected[i];
            layer.position.expression =
              "function center(){return [thisComp.width/2,thisComp.height/2];}center();";
            layer.scale.expression =
              "seedRandom(Math.floor(time*5),true);" +
              " \n " +
              "onceRand2 = random(" +
              UIdraw.main.惊讶背景最小随机区间.text +
              "," +
              UIdraw.main.惊讶背景最大随机区间.text +
              ");" +
              " \n " +
              "maxDev =onceRand2;" +
              " \n " +
              "spd = " +
              UIdraw.main.惊讶背景速度.text +
              ";" +
              " \n " +
              "t = (time - inPoint);" +
              " \n " +
              "//y = scale[1]+maxDev*Math.sin(spd*t);" +
              " \n " +
              "//y=onceRand2;" +
              " \n " +
              "y = scale[1]+=maxDev;" +
              " \n " +
              "x = scale[0];" +
              " \n " +
              "[x,y]";
            layer.rotation.expression =
              "offset = " +
              UIdraw.main.惊讶背景密度.text +
              ";x = rotation[0]+offset*index;";

            while (counter < UIdraw.main.惊讶背景数量.text - 1) {
              layer.duplicate();
              counter++;
            }
          }
        }
      };
      UIdraw.main.弹动一切.onClick = function () {
        var selected = app.project.activeItem.selectedLayers;

        if (selected.length == 0) {
          alert("选择一个需要弹动的图层，执行操作，拖动标记来决定弹动位置");
        } else {
          for (var i = 0; i < selected.length; i++) {
            var layer = selected[i];
            var myMarker = new MarkerValue("此处弹动");
            layer.property("Marker").setValueAtTime(2, myMarker);
            layer.scale.expression =
              "maxDev =" +
              UIdraw.main.弹动一切_最大弹动幅度.text +
              ";" +
              " \n " +
              "spd = " +
              UIdraw.main.弹动一切_速度.text +
              ";" +
              " \n " +
              "decay = " +
              UIdraw.main.弹动一切_弹动次数.text +
              ";" +
              " \n " +
              "t = thisLayer.marker.key(1).time;" +
              " \n " +
              "if(time >=t){" +
              " \n " +
              "t = time - t;" +
              " \n " +
              "x = scale[0]+maxDev*Math.sin(spd*t)/Math.exp(decay*t)*3;" +
              " \n " +
              "y = scale[0]*scale[1]/x;" +
              " \n " +
              "[x,y];" +
              " \n " +
              "}else{value}";
          }
        }
      };
      //working use Button
      UIdraw.main.workingAboutButton.onClick = function () {
        alert(Global.WORKINGBTN);
      };
    }
  }
  var Global = new Object();
  YouYi(Global);
  var UIdraw = new Object();
  YouYiUI(UIdraw);
}
