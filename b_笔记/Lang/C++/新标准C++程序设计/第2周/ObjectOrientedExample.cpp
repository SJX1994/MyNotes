#include <iostream>
using namespace std;
// 客观事物 -> 类
  // 写一个程序, 输入矩形的宽和高, 输出面积和周长
  // 同一个类的写法：
class CRectangle 
{
      public:
      int w, h; // 矩形的属性 – 宽和高
      // 对矩形的操作：
      void Init( int w_, int h_ ) {
      w = w_; h = h_;
      cout<< " Init CRectangle " << endl; 
      } // 设置宽和高
      int Area() { 
      return w * h; 
      } // 计算面积
      int Perimeter() { 
      return 2 * ( w + h ); 
      } // 计算周长
}; 
  // 分开写法
class CRectangle_separate
{
      public:
      int w, h;
      int Area(); //成员函数仅在此处声明
      int Perimeter() ; 
      void Init( int w_, int h_ );
};
int CRectangle_separate::Area() { 
return w * h; 
}
int CRectangle_separate::Perimeter() { 
return 2 * ( w + h ); 
}
void CRectangle_separate::Init( int w_, int h_ ) {
w = w_; h = h_;
}
// ---
int main()
{ 
      // 对象的内存空间
            cout << sizeof(CRectangle) << endl;
      // 访问类的成员变量和成员函数
       // 访问方法1：对象名.成员名
            CRectangle r; // 定义一个矩形"对象"
            r.Init( 3, 4 ); // 设置宽和高
            cout << r.Area() << endl; // 输出面积
            cout << r.Perimeter() << endl; // 输出周长
       // 访问方法2：指针->成员名 
            CRectangle r1, r2;
            CRectangle * p1 = & r1;
            CRectangle * p2 = & r2;
            p1->w = 5;
            p2->Init(3,4); //Init作用在p2指向的对象上
       // 访问方法3：引用名.成员名
            CRectangle r22;
            CRectangle & rr = r22;
            rr.w = 5;
            rr.Init(3,4); //rr的值变了，r2的值也变
      return 0;
}