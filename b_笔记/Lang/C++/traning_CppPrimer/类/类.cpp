#include "Sales_item.h"
#include <iostream>

int main()
{
      Sales_item book, book2;
      // 输入ISBN号 售出册数 销售价格
      std::cin >> book;
      std::cin >> book2;
      // 输出ISBN号 售出册数 总销售额 平均价格
      std::cout << book + book2 << std::endl;
      return 0;
}