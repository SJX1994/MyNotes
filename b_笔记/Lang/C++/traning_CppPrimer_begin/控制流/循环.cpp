#include <iostream>
int main()
{
      int sum_while(0), val_while(1);
      // val值小于10就循环
      /*
       * while(条件)
       * 声明
       */
      while (val_while <= 10)
      {
            sum_while += val_while;
            ++val_while;
      }

      std::cout << "(while)Sum 1-10 : " << sum_while << std::endl;

      int sum_for(0);
      for (int val_for = 1; val_for <= 10; ++val_for)
            sum_for += val_for;

      std::cout << "(for)Sum 1-10 : " << sum_for << std::endl;
}