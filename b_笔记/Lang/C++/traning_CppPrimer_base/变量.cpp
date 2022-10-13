#include <iostream>
#include "..\..\thirdPart\Sales_item.h"
int main()
{
      int sum(0), value, units_sold(0);
      double price(109.99), discount(price * 0.16);
      extern int i; // 无法初始化，通常出现在头文件里，用于申明全局变量，所有include头文件的代码，都可以直接访问 这个值。

      return 0;
}