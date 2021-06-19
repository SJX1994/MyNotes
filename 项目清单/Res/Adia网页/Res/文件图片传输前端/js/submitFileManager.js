
//上传后的反馈文件
var fileText = document.getElementById("sourcePackageText");
var fileElem = document.getElementById("sourcePackage");
var InfoPreview2 = document.getElementById("InfoPreview2");
var fileSelected = null;
fileElem.onclick = function (e) {

      if (fileElem.files.length == 0) {
            //alert("请上传文件并稍作等待")
      } else {
            alert("您正在修改已上传的" + fileElem.files.length + "个文件")

      }
      fileSelected = this.value;
      this.value = null;
};

fileElem.onchange = function (e) {


      handleFileDialog(this.value === fileSelected);
};

function handleFileDialog(changed) {
      alert("上传完毕");

      updateText(InfoPreview2, fileText, fileElem);

      fileText.innerHTML = "已上传" + fileElem.files.length + "个文件";




};

//上传后的反馈图片
var fileTextPre = document.getElementById("sourcePreviewText");
var fileElemPre = document.getElementById("sourcePreview");
var infoPreview = document.getElementById("InfoPreview");
var fileSelected = null;
fileElemPre.onclick = function (e) {

      if (fileElemPre.files.length == 0) {
            //alert("请上传预览图并稍作等待")
      } else {
            alert("您正在修改已上传的" + fileElemPre.files.length + "个预览图")
      }
      fileSelected = this.value;
      this.value = null;
};

fileElemPre.onchange = function (e) {
      handleFileDialogPic(this.value === fileSelected);
};

function handleFileDialogPic(changed) {
      alert("上传完毕");
      //更新文本信息

      updateText(infoPreview, fileTextPre, fileElemPre);
      fileTextPre.innerHTML = "已上传" + fileElemPre.files.length + "个图片";
      //更新预览图
      readURL(fileElemPre);


}
//更新预览图
function readURL(input) {
      if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                  $('#imagePreview').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);

      }
}
//更新文本信息
function updateText(showID, showBtnID, inID) {
      let fileSort = []
      let filesSort = []
      let kinds = []
      var arrary_element = [".bmp", ".jpg", ".png", ".tif", ".gif", ".pcx", ".tga", ".exif", ".fpx", ".svg", ".psd", ".cdr", ".pcd", ".dxf", ".ufo", ".eps", ".ai", ".raw", ".WMF", "webp", "avif"]

      let i;
      for (i = 0; i < inID.files.length; i++) {
            let name = inID.files[i].name;
            kinds.push(name.substring(name.length - 4));

            let ff = name.substring(0, name.length - 4);

            if (ff.length > 7) {
                  for (let v in ff) {

                        if (v <= 6) {
                              console.log(v);
                              fileSort.push(inID.files[i].name[v]);
                        }
                  }
                  fileSort.push("...;")
            } else {
                  fileSort.push(ff + ";");

            }


      }

      let fileSortString = fileSort.toString().replace(/\,/g, '').split(";");
      filesSort = fileSortString;

      showBtnID.innerHTML = "已上传" + inID.files.length + "项";

      fileSort = new Set(fileSort);

      // let files = filesSort[0] ? filesSort[0].toString() : "无名称" + filesSort[1] ? filesSort[1].toString() : " " + filesSort[2] ? filesSort[2].toString() : " ";
      let file1 = filesSort[0] ? filesSort[0].toString() : "无名称";
      let file2 = filesSort[1] ? filesSort[1].toString() : " ";
      let file3 = filesSort[2] ? filesSort[2].toString() : " ";
      let file4 = filesSort[3] ? "等更多文件" : " ";
      let files = file1.toString() + "  " + file2.toString() + "  " + file3.toString() + file4;

      showID.innerHTML = "类型：" + count(kinds) + "<br>" + " 包含：" + files;

}

function count(array_elements) {

      let inArray = []
      array_elements.sort();

      var current = null;
      var cnt = 0;
      for (var i = 0; i < array_elements.length; i++) {
            if (array_elements[i] != current) {
                  if (cnt > 0) {
                        inArray.push((current + '存在' + cnt + '项').toString());
                  }
                  current = array_elements[i];
                  cnt = 1;
            } else {
                  cnt++;
            }
      }
      if (cnt > 0) {
            inArray.push((current + '存在' + cnt + '项').toString());

      }

      return inArray

}