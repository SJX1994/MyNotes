

var loadAdress = 2;

var JsonDataTypes = ["http://192.168.55.128/up_load/UPLOAD/json/ART_TYPES.json", "http://192.168.55.127/config/asset_all_types", 'json/ART_TYPES.json'];
var JsonDataTags = ["http://192.168.55.128/up_load/UPLOAD/json/TEG_MARK.json", "http://192.168.55.127/tag/get_treeview", "json/TEG_MARK.json"];

$(document).ready(function () {

      $.getJSON(JsonDataTypes[loadAdress], {
            status: 'published',
            dataType: 'json',
            crossDomain: 'true',
            crossOrigin: null,
            jsonpCallback: 'MyJSONPCallback'
      },
            function (data, textStatus, jqXHR) {
                  var typeValues = [];
                  var typeZh = [];
                  jqXHR.done(
                        function (param) {
                              console.log("从  " + JsonDataTypes[loadAdress] + "  加载Json成功");
                        }
                  );
                  jqXHR.fail(
                        function (param) {
                              alert("加载失败");
                        }

                  );
                  if (textStatus == 'success') {

                        $.each(data, function (indexInArray, valueOfElement) {

                              //console.log(indexInArray + "::++" + valueOfElement);

                              $.each(valueOfElement, function (indexInArraySub, vs) {

                                    // console.log(indexInArraySub + "::+" + vs);

                                    typeValues.push(vs.AssetTypeID);
                                    typeZh.push(vs.AssetTypeName_zh_CN);

                                    $.each(vs, function (indexInArraySubSub, vsSub) {

                                          //console.log(indexInArraySubSub + "::" + vsSub);

                                    });
                              });
                        })
                  } else {
                        console.log("读取数据类型Json失败");
                  }
                  //插入选项框
                  $("option").remove(".ifFail");
                  $.each(typeValues, function (indexInArray, valueOfElement) {
                        console.log(typeZh[indexInArray] + valueOfElement);
                        $('#sourceTypes').append("<option value=\"" + valueOfElement + "\">" + typeZh[indexInArray] + "</option>");
                  });

            }
      ).fail(
            function (param) {
                  alert("加载" + JsonDataTypes[loadAdress] + "失败");
            }
      );



      $.getJSON(JsonDataTags[loadAdress], {
            status: 'published',
            dataType: 'json',
            crossDomain: 'true',
            crossOrigin: null,
            jsonpCallback: 'MyJSONPCallback'
      },
            function (data, textStatus, jqXHR) {

                  let ParentID = []


                  jqXHR.done(
                        function (param) {
                              console.log("从  " + JsonDataTags[loadAdress] + "  加载Json成功");
                        }
                  );
                  jqXHR.fail(
                        function (param) {
                              alert("加载失败");
                        }

                  );
                  if (textStatus == 'success') {
                        //根据TagID找TagParentID
                        //TagID自己独特的身份
                        //TagParentID是要找的父级
                        $.each(data, function (indexInArray, valueOfElement) {

                              $.each(valueOfElement, function (indexInArray, vs) {




                                    //创建第一级父级ID(创世ID)
                                    if (vs.TagParentID == 0) {

                                          ParentID.push(parseInt(FirstLevelCreatFather(vs)).toString());


                                    }
                                    // if (parseInt(vs.TagID) % 10 == 0) {}
                                    //获取保存父级ID
                                    $.each(valueOfElement, function (indexInArray, vs2) {

                                          if (vs.TagParentID == vs2.TagID) {

                                                let li = document.createElement("li");
                                                li.class = "tags";
                                                let pID = (vs2.TagID).toString()
                                                li.id = pID;

                                                ParentID.push(pID);



                                                // let parent = document.getElementById((vs.TagParentID).toString());

                                                // console.log(parent);
                                                // parent.className = "tagscc caret";

                                                // parent.appendChild(li);



                                          }



                                    });

                                    //去重父级ID
                                    let uniqueParentID = [...new Set(ParentID)];
                                    ParentID = uniqueParentID;
                                    //创建其他父级ID
                                    $.each(uniqueParentID, function (indexInArray, m_P) {

                                          if (m_P == vs.TagParentID && m_P != 0) {
                                                console.log(m_P + "---" + vs.TagKey_zh);


                                                let parentSpan = document.createElement("span");
                                                parentSpan.className = "tagss caret";
                                                parentSpan.innerHTML = vs.TagKey_zh;
                                                parentSpan.id = vs.TagParentID;

                                                let ul = document.createElement("ul");
                                                ul.className = "nested";
                                                ul.id = vs.TagID;

                                                parentSpan.appendChild(ul);





                                                console.log(parentSpan);

                                                //异步添加子元素
                                                setTimeout(() => {

                                                      if ((parentSpan.id).toString() == m_P) {
                                                            let li = document.createElement("li");



                                                            li.appendChild(parentSpan);

                                                            document.getElementById(m_P).appendChild(li);

                                                            var c = parentSpan.children;
                                                            if (c.length == 1) {
                                                                  //将span和ul平级
                                                                  parentSpan.parentElement.appendChild(c[0]);



                                                            }

                                                            // if(document.getElementById(vs.TagID).childElementCount)

                                                      }
                                                }, 500);


                                                // parent.className = "tagscc caret";

                                                // let son = document.createElement("li");
                                                // son.id = (vs.TagID).toString();

                                                //读档

                                                // parent.firstChild.innerHTML = "213";
                                          }

                                    });






                              });







                              ClickEvent();
                        });


                        console.log("ParentID" + ParentID);

                        //没有深度时变为可选标签：遍历所有ul id为uniqueParentID的,如果子元素为空：span的innerHTML赋予parent（li），删除span和ul
                        var needTags = [];
                        setTimeout(() => {
                              let ccc = document.getElementsByTagName("ul");
                              $.each(ccc, function (indexInArray, c) {


                                    if (c.children.length == 0) {
                                          console.log(c.parentElement);
                                          needTags.push(c.parentElement);
                                    } else {


                                    }

                              });
                              $.each(needTags, function (indexInArray, needTag) {
                                    needTag.className = "tags";
                                    let text = (needTag.children[0].innerHTML).toString();
                                    needTag.innerHTML = text;
                              });
                              console.log(ccc.length);
                        }, 800);

                        //将自己加入可选标签：遍历所有的ul,将同级的span生成一个标签tags加入自己

                        setTimeout(() => {
                              let cccc = document.getElementsByTagName("ul");

                              $.each(cccc, function (indexInArray, c) {
                                    if (c.className == "nested") {
                                          console.log(c.parentElement);
                                          let selfTags = (c.parentElement.children[0].innerHTML).toString();
                                          console.log(selfTags);
                                          let li = document.createElement("li");
                                          li.innerHTML = selfTags;
                                          li.className = "tags";
                                          c.appendChild(li);

                                    }

                              });
                        }, 900);

                  } else {
                        console.log("读取数据标签Json失败");
                  }
            }
      ).fail(
            function (param) {
                  alert("加载" + JsonDataTags[loadAdress] + "失败");
            }
      );
      // setTimeout(() => {
      //       $('.tags').click(
      //             function (param) {
      //                   alert("click");
      //             }
      //       );
      // }, 2000);

});

function FirstLevelCreatFather(vs) {
      console.log("creat first parent");
      let li = document.createElement("li");

      document.getElementById("myUL").appendChild(li);

      let span = document.createElement("span");
      span.className = "tagscc caret";
      span.innerHTML = vs.TagKey_zh;
      span.id = vs.TagParentID;

      li.appendChild(span);

      let ul = document.createElement("ul");
      ul.className = "nested";
      span.parentElement.appendChild(ul);

      ul.id = vs.TagID;



      return vs.TagID;
}
function ClickEvent() {

}
