#include <iostream>
#include <string.h>
using namespace std;
// 泛型函数模板
      // 算法实现时不指定具体要操作的数据的类型
      // 泛型 — 算法实现一遍  适用于多种数据结构
      // 优势: 减少重复代码的编写
// 写法：
      // template<class 类型参数1, class 类型参数2, … >
      // 返回值类型 模板名 (形参表)
      // {
      //       函数体
      // }

// 例如：
      // 交换两个值（可能是 int 可能是 double）
template <class T>
void Swap(T & x, T & y) 
{
      T tmp = x;
      x = y;
      y = tmp;
}
int main()
{
            int n = 1, m = 2;
            Swap(n, m); //编译器自动生成 void Swap(int &, int &)函数
            cout << n << " " << m << endl;
            double f = 1.2, g = 2.3;
            Swap(f, g); //编译器自动生成 void Swap(double &, double &)函数
            cout << f << " " << g << endl;
      return 0;
}
// 函数模板中可以有不止一个类型参数
// 函数模板可以重载, 只要它们的形参表不同即可
// C++编译器遵循：
 // 1.参数完全匹配的普通函数
 // 2.参数完全匹配的模板函数
 // 3.实参经过自动类型转换后能够匹配的普通函数
 // 4.都找不到报错
