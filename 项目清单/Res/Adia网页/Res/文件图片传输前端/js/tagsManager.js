
$(document).ready(

      //标签录入
      function () {

            var counter = 0;
            //延迟加载点击事件，等待标签从后端读取
            setTimeout(() => {
                  $(function () {
                        treeTags();
                        $('.tags').click(
                              function () {
                                    var text = $(this).text() + ';';
                                    var orgText = $('textarea[name = "sourceDescription"]').val();
                                    var newText = '';

                                    if (counter == 0) {
                                          $('textarea[name = "sourceDescription"]').val(newText);
                                          newText = "录入标签 " + text;
                                          $('textarea[name = "sourceDescription"]').val(newText);
                                          $(this).toggleClass('tagsc');
                                          $(this).removeClass('tags');

                                          if (!$(this).hasClass('tagsc')) {
                                                $(this).addClass('tagsc');
                                          }
                                          //alert(counter);
                                          counter += 10;
                                    } else {

                                          if ($(this).hasClass('tags')) {

                                                $(this).removeClass('tags');
                                                $(this).toggleClass('tagsc');
                                                newText = orgText + text;
                                                $('textarea[name = "sourceDescription"]').val(newText);
                                                // var changeTxt = $('textarea[name = "sourceDescription"]').val();



                                          } else if ($(this).hasClass('tagsc')) {

                                                $(this).toggleClass('tags');
                                                $(this).removeClass('tagsc');

                                                var changeTxt = $('textarea[name = "sourceDescription"]').val();

                                                if (changeTxt.indexOf(text)) {
                                                      //alert("存在" + text);
                                                      changeTxt = changeTxt.replace(text, '');
                                                      //alert(changeTxt);
                                                      $('textarea[name = "sourceDescription"]').val(changeTxt);


                                                }


                                          }

                                    }








                                    //alert(orgText);
                              }
                        );


                  }


                        // $('#sourcePackage').click(
                        //       function () {

                        //       }

                        // );


                  )
            }, 1500);








      }
);




function treeTags() {
      var toggler = document.getElementsByClassName("caret");
      var i;

      for (i = 0; i < toggler.length; i++) {
            toggler[i].addEventListener("click", function () {
                  this.parentElement.querySelector(".nested").classList.toggle("active");
                  this.classList.toggle("caret-down");
                  //TODO
                  this.classList.toggle("tagsccc");

                  let other = []
                  let all = document.getElementsByClassName("caret");

                  let j;
                  for (j = 0; j < all.length; j++) {
                        if (this.classList.contains("tagss") == false) {
                              //判断是否是次级
                              if (all[j].classList.contains("tagscc") == true) {
                                    //隐藏除了点击之外的主级
                                    if (all[j].innerHTML.toString() != this.innerHTML.toString()) {
                                          //alert(all[j].innerHTML.toString());
                                          all[j].classList.toggle("tagscccc");

                                    };
                              }
                        } else {

                        }



                  }
                  other = [];




            });
      }
}
