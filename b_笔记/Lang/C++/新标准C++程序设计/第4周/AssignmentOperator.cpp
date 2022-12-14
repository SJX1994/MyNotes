#include <iostream>
#include <string.h>
using namespace std;
// 赋值运算符重载
 // 赋值运算符 “=” 只能重载为 成员函数

class String {
private: 
      char * str; // 成员变量
public:
      String () : str(NULL) { } //构造函数, 初始化str为NULL
      const char * c_str() { return str; }
      char * operator = (const char * s);
~String( );
};
// 指向动态分配的存储空间：
      // 深拷贝
char * String::operator = (const char * s){ 
      if(str) delete [] str; // 该存储空间用于存放 ‘\0’ 结尾的字符串
      if(s) { //s不为NULL才会执行拷贝
      str = new char[strlen(s)+1];
      strcpy(str, s); // 复制 并 加入 s 指针指向的内存空间
      }
      else
      str = NULL;
return str;
}
      // 浅拷贝
// String & operator = (const String & s) {
//       if(str) delete [] str;
//       str = new char[strlen(s.str)+1];
//       strcpy(str, s.str);
//       return * this;
// }
// 如果析构时任然存在 那么释放存储空间
String::~String( ) { 
      if(str) delete [] str; 
      cout << "delete str" << endl;
};

int main(){
      String s;
      s = "Good Luck," ;
      cout << s.c_str() << endl;
      // String s2 = “hello!”; //这条语句要是不注释掉就会出错
      s = "hunman !";
      cout << s.c_str() << endl;
      return 0;
}

// 浅复制/浅拷贝: 执行逐个字节的复制工作
// 深复制/深拷贝: 将一个对象中指针变量指向的内容,