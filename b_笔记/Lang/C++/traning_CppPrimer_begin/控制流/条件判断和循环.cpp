#include <iostream>
int main()
{
      // currVal 是正在统计的数字；val 用来存取新的值
      int currVal(0), val(0);
      // 读取第一个数，并确保数据不为空
      if (std::cin >> currVal)
      {
            int cnt(1);             // 用于保存处理值的个数
            while (std::cin >> val) // 读取剩余的数
            {
                  if (val == currVal)
                        ++cnt;
                  else
                  {
                        std::cout << currVal << " appear " << cnt << " times " << std::endl;
                        currVal = val; // 记住新值
                        cnt = 1;       // 重置计数器
                  }
            }                                                                    // 结束循环
            std::cout << currVal << " appear " << cnt << " times " << std::endl; // 打印最后一个值的个数
      }                                                                          // 结束最外层条件判断
      return 0;
}