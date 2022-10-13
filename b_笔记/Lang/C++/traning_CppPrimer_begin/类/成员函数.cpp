#include <iostream>
#include "..\..\thirdPart\Sales_item.h"

int main()
{
      Sales_item item1, item2;
      std::cin >> item1 >> item2;
      // 检查书名 成员函数
      if (item1.isbn() == item2.isbn())
      {
            std::cout << item1 + item2 << std::endl;
            return 0;
      }
      else
      {
            std::cerr << "Need Same book name" << std::endl;
            return -1;
      }
}