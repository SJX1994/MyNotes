#include <iostream> //IO库
int main()          // 入口函数
{
      std::cout << "input 2 nums :" << std::endl;
      int v1(0), v2(0);
      std::cin >> v1 >> v2;
      /*
       * 等价于：
       * std::cin >> v1;
       * std::cin >> v2;
       * 注释符不能嵌套
       */
      std::cout << v1 << " and " << v2 << " sum is:  " << v1 + v2 << std::endl;

      return 0;
}