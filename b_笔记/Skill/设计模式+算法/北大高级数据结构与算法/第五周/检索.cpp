#pragma region 检索
      // 在记录集合中 寻找 关键码 与 值 
      // 效率 是关键 提高效率的办法：
       // 预排序
       // 建立索引
       // 散列（哈希）技术 / B树
      # include <stdio.h>
      #pragma region 线性表的检索
      // 顺序检索
      void SequentialSearch()
      {
            int a[] = {1,5,66,8,55,9};
            int n; //存放数组a中元素的个数
            int m;  //查找的数字
            int i;  //循环变量
            n = sizeof(a) / sizeof(int);  //求出数组中所有元素的个数
            printf("find number:");
            scanf("%d", &m);
            for (i=0; i<n; ++i)
            {
                  if (a[i] == m)
                  {
                        printf("index = %d\n", i);
                        break;
                  }
            }
            if (i == n)
            {
                  printf("sorry not in array\n");
            }

      }
      // 二分检索
      void HalfIntervalSearch()
      {
            int a[] = {13, 45, 78, 90, 127, 189, 243, 355};
            int key;  //存放要查找的数
            int low = 0;
            int high = sizeof(a)/sizeof(a[0]) - 1;
            int mid;
            int flag = 0;  //标志位, 用于判断是否存在要找的数
            printf("find number:");
            scanf("%d", &key);
            while ((low <= high))
            {
                  mid = (low + high) / 2;
                  if (key < a[mid])
                  {
                        high = mid - 1;
                  }
                  else if (a[mid] < key)
                  {
                        low = mid +1;
                  }
                  else
                  {
                        printf("index = %d\n", mid);
                        flag = 1;
                        break;
                  }
            }
            if (0 == flag)
            {
                  printf("sorry, data is not found\n");
            }
      }
      // 分块检索
      #pragma endregion 线性表的检索
      #pragma region 集合的检索
      #pragma endregion 集合的检索
      #pragma region 散列表的检索
      #pragma endregion 散列表的检索
#pragma endregion 检索
int main(void)
{
      // 线性表的检索 - 顺序检索:
      // SequentialSearch();
      // 线性表的检索 - 顺序检索:
      HalfIntervalSearch();
      return 0;
}