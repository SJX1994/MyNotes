#include <iostream>

int getSize()
{
      return 512 + 512;
}
constexpr int c_getSize()
{
      return 512 + 512;
}
int main()
{
      const int bufsize = 512;     // 必须初始化，且为恒定的值
      extern const int bufsizePub; // 创建一个常量能被其他文件访问
      // 常量的引用
      const int ci = 1024;
      const int &ri = ci; // ri 与 ci 都是常量
      // 指针和const
      const double pi = 3.14;
      const double *ptr = &pi; // 常量的地址也必须是常量
      int errNumb = 0;
      int *const curErr = &errNumb; // curErr 将一直指向 errNumb 的地址
      const double c_pi = 3.14;
      const double *const pip = &c_pi; // pip 是指向常量的常量指针
      // 顶层const
      int level_i = 0;
      int *const level_1 = &level_i;      // 不能改变 level_p1 的值，这是一个顶层const
      const int level_2 = 42;             // 不能改变 level_p2 的值，这是一个顶层const
      const int *level_3 = &level_2;      // 允许改变 level_p3 的值，这是一个底层const
      const int *const level_4 = level_3; //左边的 const 是底层 const， 右边的 const 是顶层 const
      const int &level_5 = level_2;       // 用于声明的const都是底层const
      // constexpr常量表达式
      const int max_files = 20;          // 常量表达式
      const int limit = max_files + 1;   // 常量表达式
      int staff_size = 27;               // 不是常量表达式
      const int staff_size2 = getSize(); // 不是常量表达式
      constexpr int c_mf = 20;           // 必须以常量表达式初始化
      constexpr int c_mf = c_getSize();  // 必须以常量表达式初始化
}