#include <iostream>
#include <string.h>
using namespace std;
// public继承的赋值兼容规则
      // 如：
            // class base { };
            // class derived : public base { };
            // base b;
            // derived d;
      // 1） 派生类的对象可以赋值给基类对象
            // b = d;
      // 2） 派生类对象可以初始化基类引用
            // base & br = d;
      // 3） 派生类对象的地址可以赋值给基类指针
            // base * pb = & d;

  // 如果派生方式是 private或protected，则上述三条不可行。

// 直接基类和间接基类
      // 多重派生时：只需要列出它的直接基类，派生类沿着类的层次自动向上继承它的间接基类
      // 派生类的成员包括:
            // • 派生类自己定义的成员
            // • 直接基类中的所有成员
            // • 所有间接基类的全部成员
class Base {
      public:
            int n;
            Base(int i):n(i) {
            cout << "Base " << n << " constructed" << endl;
            }
            ~Base() {
            cout << "Base " << n << " destructed" << endl;
            }
};
class Derived:public Base
{
      public:
            Derived(int i):Base(i) {
            cout << "Derived constructed" << endl;
            }
            ~Derived() {
            cout << "Derived destructed" << endl;
            }
};
class MoreDerived:public Derived {
public:
      MoreDerived():Derived(4) {
      cout << "More Derived constructed" << endl;
      }
      ~MoreDerived() {
      cout << "More Derived destructed" << endl;
      }
};
int main()
{
      MoreDerived Obj; 
      return 0;
}