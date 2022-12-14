
// STL中的算法
      // 算法就是一个个函数模板, 大多数在<algorithm> 中定义
      // STL中提供能在各种容器中通用的算法，比如查找，排序等



#include <iostream>
#include <algorithm>
using namespace std;

class A {
      int v;
      public:
      A(int n):v(n) { }
      bool operator < ( const A & a2) const 
      {
            cout << v << "<" << a2.v << "?" << endl;
            return false;
      }
      bool operator ==(const A & a2) const
      {
            cout << v << "==" << a2.v << "?" << endl;
            return v == a2.v;
      }
};

int main() {
            A a [] = { A(1),A(2),A(3),A(4),A(5) };
            cout << binary_search(a,a+4,A(-2)); //折半查找 用的小于号 所以永远是true
            // 主要看二分搜索法的查找过程
      return 0;
}