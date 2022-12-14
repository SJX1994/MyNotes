// 友元
#include <iostream>
using namespace std;
 // 友元函数
 
      class Box
      {
      double width;
      private:
      friend void printWidth( Box box );
      public:
      void setWidth( double wid );
      };
      
      // 成员函数定义
      void Box::setWidth( double wid )
      {
      width = wid;
      }
      
      // 请注意：printWidth() 不是任何类的成员函数
      void printWidth( Box box )
      {
      /* 因为 printWidth() 是 Box 的友元，它可以直接访问该类的任何成员 */
      cout << "Width of box : " << box.width <<endl;
      }
 // 友元类
      class CCar {
      private:
            int price;
            friend class CDriver; //声明CDriver为友元类
      };
      class CDriver {
      public:
            CCar myCar;
            void ModifyCar() { //改装汽车
            myCar.price += 1000; // CDriver是CCar的友元类可以访问其私有成员
            cout << "ModifyCar() price = " << myCar.price << endl;
            }
      };
// 程序的主函数
int main( )
{
 // 友元函数
   Box box;
 
   // 使用成员函数设置宽度
   box.setWidth(10.0);
   
   // 使用友元函数输出宽度
   printWidth( box );
 // 友元类
   CDriver driver;
   driver.ModifyCar();
   driver.ModifyCar();
   return 0;
}

