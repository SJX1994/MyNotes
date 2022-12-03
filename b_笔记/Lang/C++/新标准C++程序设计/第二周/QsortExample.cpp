#include <stdio.h> // 标准 输入和输出库
#include <stdlib.h> // 标准库的头文件，包括内存分配、进程控制、转换等函数

int MyCompare( const void * elem1, const void * elem2 )
{
      unsigned int * p1, * p2;
      p1 = (unsigned int *) elem1; // “* elem1” 非法
      p2 = (unsigned int *) elem2; // “* elem2” 非法
      return (* p1 % 10) - (* p2 % 10 ); 
// 1) 如果 * elem1应该排在 * elem2前面，则函数返回值是负整数
// 2) 如果 * elem1和* elem2哪个排在前面都行，那么函数返回0
// 3) 如果 * elem1应该排在 * elem2后面，则函数返回值是正整数
}
#define NUM 5 // 常量 NUM 的值是 5
int main() 
{

unsigned int an[NUM] = { 8,123,11,10,4 }; // 初始化数组

qsort( an,NUM,sizeof(unsigned int), MyCompare); // 传入数组的首地址、元素个数、每个元素的大小、比较函数的入口地址
for( int i = 0;i < NUM; i ++ )printf("%d ",an[i]); // 按 个位 从大到小排序输出结果。
// %d ： 将整数值作为带符号的十进制整数

return 0;

}
