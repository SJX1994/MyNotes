#include <iostream>
using namespace std;
// this指针
  // 作用：
    // 指向成员函数所作用的对象
    // 静态成员函数中不能使用 this 指针，因为静态成员函数并不具体作用与某个对象
    // 每一个对象都能通过 this 指针来访问自己的地址。

class Box
{
      
   public:
      // 构造函数定义
      Box(double l=2.0, double b=2.0, double h=2.0)
      {
         cout <<"Constructor called." << endl;
         length = l;
         breadth = b;
         height = h;
      }
      double Volume()
      {
         return length * breadth * height;
      }
      int compare(Box box)
      {
         return this->Volume() > box.Volume();
      }
   private:
      double length;     // Length of a box
      double breadth;    // Breadth of a box
      double height;     // Height of a box
};
 
int main(void)
{
   Box Box1(3.3, 1.2, 1.5);    // Declare box1
   Box Box2(1.5, 1.0, 1.0);    // Declare box2
Box2.length2 = 1000.0;
   if(Box1.compare(Box2))
   {
      cout << "Box2 is smaller than Box1" <<endl;
   }
   else
   {
      cout << "Box2 is equal to or larger than Box1" <<endl;
   }
   return 0;
}