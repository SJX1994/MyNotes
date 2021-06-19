var cookieHelper = {
  // 设置cookie
  set: function(c_name, value, expiredays) {
    //设置cookie期限
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) + ";expires=" + exdate.toGMTString() + ";path=/";
  },
  // 读取cookie
  get: function(c_name) {
    if (document.cookie.length > 0) {
      c_start = document.cookie.indexOf(c_name + "=")
      if (c_start != -1) {
        c_start = c_start + c_name.length + 1
        c_end = document.cookie.indexOf(";", c_start)
        if (c_end == -1)
          c_end = document.cookie.length
        return unescape(document.cookie.substring(c_start, c_end))
      }
    }
    return ""
  },
  // 检查cookie
  check: function(c_name) {
    username = this.get(c_name);
    if (username != null && username != "" && username!=undefined) {
      return true;
    } else {
      return false;
    }
  },
  // 清除cookie
  clear: function(name) {
    setCookie(name, "", -1);
  },

  test: function() {
    console.log('cookie helper loaded.');
  }

};
