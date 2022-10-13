#include <iostream>

int main()
{
      bool b = 33;          // -33 // b 结果为真
      int p = b;            // 结果为1
      p = 3.1415926;        // 结果为3.0
      double pi = p;        // pi的值为3.0
      unsigned char c = -1; // 值为255
      signed char c2 = 255; // 8位（2的8次方） 有效的只有 7位（2的7次方） 所以 只有 最多255 但只有128 有效
      unsigned u = 10;
      int ii = -42;
      int aa = ii + ii;         // 输出-84
      unsigned int bb = u + ii; // 输出 4294967264 （int 32）
      unsigned u1(42), u2(10);
      unsigned cc = u1 - u2; // 输出 32
      unsigned dd = u2 - u1; // 输出 取模后的值
      // std::cout << dd << std::endl;
      // 降序方式输出
      for (int i = 10; i >= 0; --i)
      {
            // std::cout << i << std::endl;
      }
      unsigned uuu = 11;
      while (uuu > 0)
      {
            --uuu;
            // std::cout << uuu << std::endl;
      }
      unsigned uu = 11;
      int x = uu--;                                   // 先赋值 后自减
      int z = --uu;                                   // 先自减 后赋值
      std::cout << "zi \f fu \f chuan " << std::endl; // 字符串字面值
      return 0;
}