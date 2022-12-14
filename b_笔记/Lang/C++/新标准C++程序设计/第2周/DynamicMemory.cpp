

int main()
{
      // 分配一个变量
        // P = new T;
            // T是任意类型名，
            // P是类型为 T * 的指针。
            // 动态分配出一片大小为 sizeof(T)字节的内存空间，并且将该内存空间的起始地址赋值给P。
        int * pn; // 声明 pn 为 int 型指针
        pn = new int; // 动态分配出一片大小为 sizeof(int)字节的内存空间，并且将该内存空间的起始地址赋值给 pn。
        * pn = 5; // 为指针所指的 内存空间赋值。
      // 分配一个数组
        // P = new T[N];
            // T :任意类型名
            // P :类型为T * 的指针  
            // N :要分配的数组元素的个数，可以是整型表达式
        int * pn;
        int i = 5;
        pn = new int[i * 20];
        pn[0] = 20;
        // pn[100] = 30; //编译没问题。运行时导致数组越界
      // 释放变量内存 （delete 指针；）
            // 有 new(分配内存) 一定要有 delete(释放内存)
            int * p = new int;
            * p = 5;
            delete p; // 一片内存只能 delete 一次，不然会空指针
      // 释放数组内存 （delete [ ] 指针；）
            int * p = new int[20];
            p[0] = 1;
            delete [ ] p;
  return 0;
}