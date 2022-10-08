#include <iostream>
int main()
{
      int sum(0), value(0);
      // 检测stream（数据流）的状态，如果数据流有效 就会一直循环。
      // 当输入不是int 流就失效了
      while (std::cin >> value)
            sum += value;
      std::cout << sum << std::endl;

      return 0;
}