#include <iostream>
#include <cstdlib>
#define _for(l, a, b) for (int l = (a); l < b; ++l)
typedef struct node
{
      int value;
      node *next_node;
} nodeSJX, *pondeSJX;
int main()
{
      // 引用/别名（reference）
      int ival = 1024;
      int &refVal = ival; // 引用必须初始化
      refVal = 999;       //修改绑定

      // 例子：
      int i(1024), i2(2048);
      int &r = i, r2 = i2; // r 与 i 绑定 ， r2 拷贝了 i2
      int &r3 = r;         // r3 绑定了 r 绑定了 i
      r3 = 10;

      // 指针
      int *ip1, *ip2; // 指向int型对象的指针
      int ival2 = 42;
      int *ip3 = &ival2;               // 获取对象的地址, ip3 存放 ival 的地址
      int &ivalRef = ival2;            // 创建ivalRef绑定引用ival2
      int *ip3_ref = &ivalRef;         // 获取ivalRef的地址
      int ival_fromPointer = *ip3_ref; // 解引
      // 引用和被用于的值指向同一地址
      // std::cout << ip3 << std::endl;
      // std::cout << ip3_ref << std::endl;
      // 例子
      int ii = 42;
      int &r11 = ii; // r 出现在声明中,是引用
      int *p;        // p 出现在声明中,是指针
      p = &r11;      // & 出现在表达式中，是一个取址符
      *p = ii;       // * 出现在表达式中，是一个解引符
      int &r22 = *p; // & 出现在声明中，是引用；* 出现在表达式中，是一个解引符
      // std::cout << r22 << std::endl;

      // 空指针
      int *pp = nullptr; // 等价于 *pp = 0
      int *pp2 = NULL;   // 等价于 *pp2 = 0
      // std::cout << &pp << std::endl;
      // 例子
      int iii = 42;
      int *pi = 0;                                // 初始化了一个空指针
      int *pi2 = &iii;                            // 初始化了指针指向iii的地址
      bool printerChecker = (pi2) ? true : false; // 可以判断是否为空指针
      int **pi2_pointer(0);
      pi2_pointer = &pi2;
      int ***pi2_pointer_pointer(0);
      pi2_pointer_pointer = &pi2_pointer;
      // std::cout << pi2 << std::endl;                 //  iii 的地址
      // std::cout << pi2_pointer << std::endl;         //  指向 iii 地址 的指针 的 地址
      // std::cout << pi2_pointer_pointer << std::endl; //  指向 iii 地址 的指针 的 地址 的指针 的 地址
      // void* 指针

      // 链表：利用碎片化内存，如果存储的内容大于指针的大小，那么存储利用率就是高的
      pondeSJX head = (pondeSJX)malloc(sizeof(nodeSJX));

      pondeSJX list_1 = (pondeSJX)malloc(sizeof(nodeSJX));
      list_1->value = 1;

      pondeSJX list_2 = (pondeSJX)malloc(sizeof(nodeSJX));
      list_2->value = 2;

      pondeSJX rear = (pondeSJX)malloc(sizeof(nodeSJX));
      rear->value = 3;

      head->next_node = list_1;
      list_1->next_node = list_2;
      list_2->next_node = rear;
      rear->next_node = NULL;
      int counter(2);
      for (pondeSJX m_pointer = head->next_node; m_pointer != NULL; m_pointer = m_pointer->next_node, --counter)
      {
            if (counter == 2)
            {
                  // 删
                  // std::cout << " second  value been deleted now value is: " << m_pointer->value << std::endl;
                  // // list_1->next_node = rear;
                  // pondeSJX m_prevPointer = m_pointer;
                  // m_prevPointer->next_node = m_prevPointer->next_node->next_node;
                  // m_prevPointer->next_node->next_node = NULL;
                  // free(m_prevPointer->next_node);
                  // break;
                  // 增
                  pondeSJX m_prevPointer = m_pointer;
                  pondeSJX list_add = (pondeSJX)malloc(sizeof(nodeSJX));
                  list_add->value = 200;
                  list_add->next_node = m_prevPointer->next_node;
                  m_prevPointer->next_node = list_add;
                  break;
            }
      }

      for (pondeSJX h = head->next_node; h != NULL; h = h->next_node)
      {
            // std::cout << h->value << std::endl;
      }

      int array_a[10];
      int l;
      _for(l, 0, 3)
      {
            // std::cout << &array_a[l] << " " << array_a[l] << std::endl;
      }

      // 指向指针的引用
      int value_PR = 42;
      int *p_P;         // p_PR 一个Int型的地址
      int *&r_PR = p_P; // r_PR 是一个对指针 p_PR 的引用
      r_PR = &value_PR; // r_PR 引用了 value_PR 的指针，因此 p_P 指向 value_PR
      *r_PR = 0;        // 解引r_PR(p_P的引用)得到i,将 value_PR 的值改为0
      std::cout << *r_PR << std::endl;
      return 0;
}