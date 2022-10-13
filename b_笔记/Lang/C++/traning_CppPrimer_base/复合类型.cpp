#include <iostream>
#include <cstdlib>
int main()
{
      // 引用/别名（reference）
      int ival = 1024;
      int &refVal = ival; // 引用必须初始化
      refVal = 999;       //修改绑定

      // 例子：
      int i(1024), i2(2048);
      int &r = i, r2 = i2; // r 与 i 绑定 ， r2 拷贝了 i2
      int &r3 = r;         // r3 绑定了 r 绑定了 i
      r3 = 10;

      // 指针
      int *ip1, *ip2; // 指向int型对象的指针
      int ival2 = 42;
      int *ip3 = &ival2;               // 获取对象的地址, ip3 存放 ival 的地址
      int &ivalRef = ival2;            // 创建ivalRef绑定引用ival2
      int *ip3_ref = &ivalRef;         // 获取ivalRef的地址
      int ival_fromPointer = *ip3_ref; // 解引
      // 引用和被用于的值指向同一地址
      // std::cout << ip3 << std::endl;
      // std::cout << ip3_ref << std::endl;
      // 例子
      int ii = 42;
      int &r11 = ii; // r 出现在声明中,是引用
      int *p;        // p 出现在声明中,是指针
      p = &r11;      // & 出现在表达式中，是一个取址符
      *p = ii;       // * 出现在表达式中，是一个解引符
      int &r22 = *p; // & 出现在声明中，是引用；* 出现在表达式中，是一个解引符
      // std::cout << r22 << std::endl;

      // 空指针
      int *pp = nullptr; // 等价于 *pp = 0
      int *pp2 = NULL;   // 等价于 *pp2 = 0
      // std::cout << &pp << std::endl;
      // 例子
      int iii = 42;
      int *pi = 0;     // 初始化了一个空指针
      int *pi2 = &iii; // 初始化了指针指向iii的地址

      return 0;
}