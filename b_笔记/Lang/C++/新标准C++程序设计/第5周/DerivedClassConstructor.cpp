#include <iostream>
#include <string.h>
using namespace std;
// 派生类的构造函数
  // 派生类(子类)对象 包含 基类(父类)对象
  // 执行 派生类 构造函数之前, 先执行 基类 的构造函数
  // 派生类交代基类初始化, 具体形式:
  // 构造函数名(形参表): 基类名(基类构造函数实参表){}
// 显示构造：
      class Bug {
      private :
            int nLegs; 
            int nColor;
      public:
            int nType;
            Bug (int legs, int color); // 基类构造函数
            void PrintBug () {
                  cout << "nType=" << nType << ",nColor=" << nColor << endl;
            };
      };
      class FlyBug: public Bug { // FlyBug是Bug的派生类
            int nWings;
      public:
            FlyBug(int legs, int color, int wings); // 派生类构造函数
      };
      Bug::Bug( int legs, int color) { // 基类构造函数
            nLegs = legs;
            nColor = color;
      }
      //错误的FlyBug构造函数:
      // FlyBug::FlyBug (int legs, int color, int wings) { // 派生类构造函数
      //       // nLegs = legs; // 不能访问
      //       // nColor = color; // 不能访问
      //       nType = 1; // ok
      //       nWings = wings;
      // }
      //正确的FlyBug构造函数:
      FlyBug::FlyBug (int legs, int color, int wings):Bug(legs, color) {
            nWings = wings;
      }
// 隐式构造
// ...
// 包含成员对象的派生类的构造函数
class Skill {
 public:
      Skill(int n) { }
};
class FlyBug2: public Bug {
 int nWings;
      Skill sk1, sk2;
 public:
      FlyBug2(int legs, int color, int wings);
};
FlyBug2::FlyBug2( int legs, int color, int wings):Bug(legs, color), sk1(5), sk2(color) {
      nWings = wings;
}

int main() {
            FlyBug fb ( 2,3,4);
            fb.PrintBug();
            fb.nType = 1; 
            fb.PrintBug();
            // fb.nLegs = 2 ; // error.nLegs is private
      return 0;
}
// 析构函数的调用顺序与构造函数的调用顺序相反：
  // 创建 派生类的对象 时, 执行 派生类的构造函数 之前:
      //  调用 基类 的构造函数
      //  初始化派生类对象中从基类继承的成员
      //  调用 成员对象类 的构造函数
      //  初始化派生类对象中成员对象
  // 执行完 派生类的析构函数 后:
      //  调用 成员对象类 的析构函数
      //  调用 基类 的析构函数