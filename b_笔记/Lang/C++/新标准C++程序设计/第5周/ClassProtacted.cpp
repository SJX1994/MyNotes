// 基类和派生类有同名成员的情况
#include <iostream>
#include <string.h>
using namespace std;

class base{
 public:
 int i;
 void func(){
      cout << i << endl;
 };
};
class derived : public base{
 public:
 int i;
 void access();
 void func(){
      cout << i << endl;
 }; 
};
void derived::access()
{ 
      i = 5; //引用的是派生类的 i
      base::i = 5; //引用的是基类的 i
      func(); //派生类的
      base::func(); //基类的
}

      int main()
      {
            derived d;
            d.i = 5; //引用的是派生类的 i
            d.base::i = 1; //引用的是基类的 i
            d.func(); //派生类的
            d.base::func(); //基类的

            base *b = new derived();
            return 0;
      }

// 访问范围说明符:
      // 基类的private成员: 可以被下列函数访问
      // • 基类的成员函数
      // • 基类的友员函数
      // 基类的public成员: 可以被下列函数访问
      // • 基类的成员函数
      // • 基类的友员函数
      // • 派生类的成员函数
      // • 派生类的友员函数
      // • 其他的函数
      // 基类的protected成员: 可以被下列函数访问
      // • 基类的成员函数
      // • 基类的友员函数
      // • 派生类的成员函数可以访问当前对象的基类的保护成员
