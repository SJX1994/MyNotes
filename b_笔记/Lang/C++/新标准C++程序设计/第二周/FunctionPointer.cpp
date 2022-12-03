#include <stdio.h> // 标准 输入和输出库
void PrintMin(int a,int b) {
      if( a<b )
      printf("%d",a);
      else
      printf("%d",b);
} // 输出更小的

int main() {

      void (* pf)(int ,int); // pf 作为函数指针的 入口地址
      int x = 4, y = 5;
      pf = PrintMin; // 入口地址指向 PrintMin 方法
      pf(x,y); // 调用指针函数
      return 0; 
 }