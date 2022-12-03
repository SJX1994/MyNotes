// 编写 交换两个整形变量值的函数
#include <iostream>
using namespace std;
// 地址交换法
void swap_pointer( int * a, int * b)
{
      int tmp; // 临时变量
      tmp = * a; // 临时变量 被 a 解引的值 赋值。
      * a = * b; // 地址a 被 b 解引的值 赋值。
      * b = tmp; // 地址b 被 临时变量(之前 a 解引的值) 赋值。
}

// 引用交换法
void swap_reference(int & a, int & b)
{
      int tmp; // 临时变量
      tmp = a;  // 临时变量 被 a 引用的值 赋值。
      a = b;    // a 的引用的值 被 b的引用的值 赋值
      b = tmp;  // b 的引用的值 被 临时变量(之前 a的引用的值) 赋值
}

// 引用作为函数的返回值
int n = 4;
int & SetValue() { return n; }


int main() 
{
      // 引用类型传值
      SetValue() = 40;
      cout<<n;
      // 地址交换法
      int a = 4, b = 5;
      swap_pointer(&a, &b); // 传入 a 的取址 和 b 的取址
      cout<<"\n"<<a;
      cout<<"-"<<b;
      // 引用交换法
      swap_reference(a, b);
      cout<<"\n"<<a;
      cout<<"-"<<b;
      return 0;
} 