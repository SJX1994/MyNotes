#include <iostream>
#include "..\..\thirdPart\Sales_item.h"
int main()
{
      Sales_item total; // 保存下一条交易记录的变量
      // 如果有数据可以处理，读入第一条交易记录
      if (std::cin >> total)
      {
            // 保存和的变量
            Sales_item trans;
            // 读入并处理剩余交易记录
            while (std::cin >> trans)
            {
                  // 如果依然在处理相同的书
                  if (total.isbn() == trans.isbn())
                  {
                        total += trans; // 更新总销售额
                  }
                  else
                  {
                        // 打印前一本书的结果
                        std::cout << total << std::endl;
                        total = trans; // total变成下一本书的销售额，进入下一轮计算
                  }
            }
            std::cout << total << std::endl; // 打印输入结束后最后一本书的结果
      }
      else
      {
            // 无效输入
            std::cerr << "No data" << std::endl;
            return -1;
      }
      return 0;
}