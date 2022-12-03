#include <iostream>
using namespace std;
// inline:
      // 函数调用是有时间开销的。减少函数调用的开销，引入了内联函数机制。
      // 编译器处理对内联函数,是将整个函数的代码插入到调用语句处，而不会产生调用函数的语句。
inline int Max(int a,int b) {
      if( a > b) return a;
      return b;
}
// 函数重载
      // 一个或多个函数，名字相同，然而参数个数或参数类型不相同，这叫做函数的重载。
// 缺省参数 default parameters
      // 目的：提高程序的可扩充性
void func( int x1, int x2 = 2, int x3 = 3) { }
// func(10 ) ; //等效于 func(10,2,3)
// func(10,8) ; //等效于 func(10,8,3)

int main()
{
     int a =  Max(1,2);
     cout <<a;
     return 0; 
}