#include <bits/stdc++.h>
#include <vector>
#include <algorithm>
#include <iostream>
using namespace std;
// 排序问题
int countMe = 0;
#pragma region 基本概念
// 基本概念:
// 
       // 记录（ Record ）排序的基本单位
       // 序列 （Sequence） 由： 记录（ Record ）组成
       // 关键码 （Key） 记录 一个或者多个域
       // 排序码 （Sort Key） 作为排序依据的一个或者多个域
       // 排序行为 （Sort） 依据排序码对记录进行排序
       // 内排序行为 （In-Sort） 在内存中完成的排序
       // 例子：
             // 序列 S = {r1,r2,r3...rn},排序码 K = {k1,k2,k3...kn}
             // 排序码的顺序：
                   // 不减序：k1 <= k2 <= k3 <= ... <= kn
                   // 不增序：k1 >= k2 >= k3 >= ... >= kn
                   // 正序： 1 2 3 4
                   // 逆序： 4 3 2 1
             // 稳定性: 
                   // 存在具有多个相同排序码的记录，排序后相对位置不变
             // 排序算法的衡量标准：
                   // 时间代价：记录的比较次数和移动次数
                   // 空间代价：算法本身的繁杂程度
       // 算法可视化网站：
            // https://visualgo.net/zh/sorting?slide=6-3
       // 案例网站：
            // www.geeksforgeeks.org
      #pragma region Utility
      //Swap function
            void swap(int *xp, int *yp)
            {
                  cout << "!found minest swap! \n";
                  int temp = *xp;
                  *xp = *yp;
                  *yp = temp;
            }    
      // Function to print an array
            void printArray(int arr[], int size)
            {
                  int i;
                  for (i = 0; i < size; i++)
                        cout << arr[i] << " ";
                  cout << endl;
                  cout << "size: " << size << endl;
            }
            void printArray(float arr[], int size)
            {
                  int i;
                  for (i = 0; i < size; i++)
                        cout << arr[i] << " ";
                  cout << endl;
                  cout << "size: " << size << endl;
            }

      #pragma endregion
#pragma endregion

#pragma region 插入排序
// 插入排序 insertion Sort
// 
      // 特性
            // 对手中的扑克牌进行排序
            // 稳定性：稳定
            // 空间代价：O(1)
            // 时间代价：O(n^2)
            // 使用场景：数据量小，数据基本有序
      void insertionSort (int Array[], int n){
            int  TempRecord,i,j; // 临时变量
            for (i=1; i<n; i++){ // 依次插入第 i 个记录
                  TempRecord = Array[i];
                  //从 i 开始往前寻找记录 i 的正确位置
                  j = i-1;
                  //将那些大于等于记录 i 的记录后移
                  while ((j>=0) && (TempRecord < Array[j])){
                        Array[j+1] = Array[j];
                        j = j - 1;
                  }
                  //此时 j 后面就是记录 i 的正确位置，回填
                  Array[j+1] = TempRecord;
                 // 输出第三次结果
                  // if(i == 2)
                  // {
                  //       cout << "insertionSort2: \n";
                  //       printArray(Array, n);
                  // }
            }
      }
#pragma endregion

#pragma region 希尔排序
// 希尔排序 shellSort
// 插入排序的变体，根据 
      // 1.插入排序的使用场景：在插入排序之前先尽量让队列变得有序
      // 2.插入排序只能使用近项：Shell排序 允许交换远项。
      // 特性
            // 稳定性：不稳定
            // 空间代价：O(1)
            // 时间代价：O(n*log n)~O(n 1.25 )
      // 算法思想
            // 先将 序列 转化为若干 小序列 在小序列内进行插入排序
            // 逐渐增加 小序列 规模，而减少小序列个数，使得待排序 序列 逐渐处于更有序状态
            // 最后对整个序列进行一次插入排序
      int shellSort(int arr[], int n)
      {
      // Start with a big gap, then reduce the gap
      for (int gap = n/2; gap > 0; gap /= 2) // 增连序列除以二，可以用 Hibbard 增量序列 或者 其他增量序列 进一步减少时间代价
      {
            // Do a gapped insertion sort for this gap size.
            // The first gap elements a[0..gap-1] are already in gapped order
            // keep adding one more element until the entire array is
            // gap sorted
            for (int i = gap; i < n; i += 1)
            {
                  // add a[i] to the elements that have been gap sorted
                  // save a[i] in temp and make a hole at position i
                  int temp = arr[i];
      
                  // shift earlier gap-sorted elements up until the correct
                  // location for a[i] is found
                  int j;           
                  for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                  {
                  arr[j] = arr[j - gap];
                  } 
                  //  put temp (the original a[i]) in its correct location
                  arr[j] = temp;
            }
      }
      return 0;
      }
#pragma endregion

#pragma region 选择排序
      #pragma region 直接选择排序
// 选择排序 selectionSort
// 
      // 直接选择排序
       // 依次选出剩下 未排序 的记录中的 最小记录
       // •不稳定
       // •空间代价：Θ(1) 
       // •时间代价
       // –比较次数：Θ(n2)
       // –交换次数：n-1 
       // –总时间代价：Θ(n2) 
             
            void selectionSort(int arr[], int n)
            {
                  int i, j, min_idx;
                  
                  // One by one move boundary of
                  // unsorted subarray
                  for (i = 0; i < n-1; i++)
                  {
                        
                        // Find the minimum element in
                        // unsorted array
                        min_idx = i;
                        for (j = i+1; j < n; j++)
                        if (arr[j] < arr[min_idx])
                              min_idx = j;
                  
                        // Swap the found minimum element
                        // with the first element
                        if(min_idx!=i)
                              cout << "minBeforValue:\n"<< arr[i] << endl;
                              swap(&arr[min_idx], &arr[i]);
                              cout << "minAfterValue:\n"<< arr[i] << endl;
                  }
            }
      #pragma endregion
      #pragma region 堆排序
      // 堆排序 eapSort
       // 堆排序：基于最大堆来实现
        // 最大堆：
            // 根节点 最小
        // 最小堆：
            // 根节点 最大
       //  选择类内排序
       //  – 直接选择排序：直接从剩余记录中线性
       //  查找最大记录
       //  – 堆排序：基于最大堆来实现，效率更高
       //  选择类外排序
       //  – 置换选择排序
       //  – 赢者树、败方树
       // 优点：
            // 效率——执行堆排序所需的时间呈对数增长，而其他算法可能会随着要排序的项目数量的增加而呈指数增长。这种排序算法非常高效。
            // 内存使用量——内存使用量是最小的，因为除了保存要排序的初始项目列表所必需的内存之外，它不需要额外的内存空间来工作
            // 简单——它比其他同样高效的排序算法更容易理解，因为它不使用递归等高级计算机科学概念
// To heapify a subtree rooted with node i
// which is an index in arr[].
// n is size of heap
// 数组索引；堆的大小 ；以 i 为根的子树 
void heapify(int arr[], int N, int i)
{
 
    // Initialize largest as root
    int largest = i;
    
    // left = 2*i + 1
    int l = 2 * i + 1;
    // right = 2*i + 2
    int r = 2 * i + 2;
    
    // If left child is larger than root
    if (l < N && arr[l] > arr[largest])
        largest = l;
 
    // If right child is larger than largest
    // so far
    if (r < N && arr[r] > arr[largest])
        largest = r;
 
    // If largest is not root
    if (largest != i) {
        swap(arr[i], arr[largest]);
 
        // Recursively heapify the affected
        // sub-tree
        heapify(arr, N, largest);
    }
//     cout << "largest:\n"<< largest << endl;
//     cout << "left:"<< l <<"---"<< "right:"<< r << endl;
}
 
// Main function to do heap sort
void heapSort(int arr[], int N)
{
 
    // Build heap (rearrange array)
    for (int i = N / 2 - 1; i >= 0; i--)
    {
        cout<< "build heap:\n"<< i << endl;
        heapify(arr, N, i);
    }
 
    // One by one extract an element
    // from heap
    for (int i = N - 1; i > 0; i--) {
        cout<< "extract:\n"<< i << endl;
        // Move current root to end
        swap(arr[0], arr[i]);
 
        // call max heapify on the reduced heap
        heapify(arr, i, 0);
    }
}
      #pragma endregion
#pragma endregion

#pragma region 交换排序
      #pragma region 冒泡排序
// 冒泡排序 Bubble Sort
 // 算法思想：
      // 不停地比较相邻的记录，如果不满足排序要求，就交换相邻记录，直到所有的记录都已经排好序
      // 检查每次冒泡过程中是否发生过交换，如果没有，则表明整个数组已经排好序了，排序结束
      // 避免不必要的比较
 // 算法分析：
      // 稳定性：稳定
      // 时间复杂度：O(n^2)
      // 空间复杂度：O(1)
      // A function to implement bubble sort
void bubbleSort(int arr[], int n)
{
    int i, j;
    for (i = 0; i < n - 1; i++)
      {       
        
        // Last i elements are already
        // in place
        for (j = 0; j < n - i - 1; j++)
        {
            
            if (arr[j] > arr[j + 1])
            {
                  
                  swap(arr[j], arr[j + 1]);
            }
                
        }
            
      }
                
}
      #pragma endregion
      #pragma region 快速排序
// 快速排序 Quick Sort
      // 算法思想：
       // 选择轴值 (pivot) 尽可能使 L, R 长度相等
            // 选择策略：
                  // 1. 选择最左边记录
                  // 2. 随机选择
                  // 3. 选择平均值
       // 将序列划分为两个子序列 L 和 R，使得 L 中所有记录都小于或等于轴值，R 中记录都大于轴值
       // 对子序列 L 和 R 递归进行快速排序
       // 基于分治法的排序：快速、归并
            // 分治策略的实例：
             // BST 查找、插入、删除算法、快速排序、归并排序、二分检索
             // 核心思想：
                  // 划分 求解子问题 (子问题不重叠) 综合解
// 双轴快排实现
int partition(int* arr, int low, int high, int* lp);
void DualPivotQuickSort(int* arr, int low, int high)
{
    if (low < high) {
        // lp means left pivot, and rp means right pivot.
        int lp, rp;
        rp = partition(arr, low, high, &lp);
        DualPivotQuickSort(arr, low, lp - 1);
        DualPivotQuickSort(arr, lp + 1, rp - 1);
        DualPivotQuickSort(arr, rp + 1, high);
    }
}
// 分割函数
int partition(int* arr, int low, int high, int* lp)
{
    if (arr[low] > arr[high])
        swap(&arr[low], &arr[high]);
 
    // p is the left pivot, and q is the right pivot.
    int j = low + 1;
    int g = high - 1, k = low + 1, p = arr[low], q = arr[high];
    while (k <= g) {
 
        // if elements are less than the left pivot
        if (arr[k] < p) {
            swap(&arr[k], &arr[j]);
            j++;
        }
 
        // if elements are greater than or equal
        // to the right pivot
        else if (arr[k] >= q) {
            while (arr[g] > q && k < g)
                g--;
            swap(&arr[k], &arr[g]);
            g--;
            if (arr[k] < p) {
                swap(&arr[k], &arr[j]);
                j++;
            }
        }
        k++;
    }
    j--;
    g++;
 
    // bring pivots to their appropriate positions.
    swap(&arr[low], &arr[j]);
    swap(&arr[high], &arr[g]);
 
    // returning the indices of the pivots.
    *lp = j; // because we cannot return two elements
    // from a function.
 
    return g;
}
      #pragma endregion
#pragma endregion

#pragma region 归并排序
// merge sort 思想：
      // 先划分子序列 再归并
// 算法分析：
      // 空间代价：Θ(n) 
      // • 划分时间、排序时间、归并时间：T(n) = 2T(n/2) + θ(n)
      // • 归并排序总时间代价也为 Θ(nlog n)
      // • 不依赖于原始数组的输入情况，最大、最小以及平均时
      // 间代价均为 Θ(nlog n)

// Merges two subarrays of array[].
// First subarray is arr[begin..mid]
// Second subarray is arr[mid+1..end]
void merge(int array[], int const left, int const mid,
           int const right)
{
    auto const subArrayOne = mid - left + 1;
    auto const subArrayTwo = right - mid;
 
    // Create temp arrays
    auto *leftArray = new int[subArrayOne],
         *rightArray = new int[subArrayTwo];
 
    // Copy data to temp arrays leftArray[] and rightArray[]
    for (auto i = 0; i < subArrayOne; i++)
        leftArray[i] = array[left + i];
    for (auto j = 0; j < subArrayTwo; j++)
        rightArray[j] = array[mid + 1 + j];
 
    auto indexOfSubArrayOne
        = 0, // Initial index of first sub-array
        indexOfSubArrayTwo
        = 0; // Initial index of second sub-array
    int indexOfMergedArray
        = left; // Initial index of merged array
    
    // Merge the temp arrays back into array[left..right]
    while (indexOfSubArrayOne < subArrayOne
           && indexOfSubArrayTwo < subArrayTwo) {
        if (leftArray[indexOfSubArrayOne]
            <= rightArray[indexOfSubArrayTwo]) {
            array[indexOfMergedArray]
                = leftArray[indexOfSubArrayOne];
            indexOfSubArrayOne++;
        }
        else {
            array[indexOfMergedArray]
                = rightArray[indexOfSubArrayTwo];
            indexOfSubArrayTwo++;
        }
        indexOfMergedArray++;
    }
    // Copy the remaining elements of
    // left[], if there are any
    while (indexOfSubArrayOne < subArrayOne) {
        array[indexOfMergedArray]
            = leftArray[indexOfSubArrayOne];
        indexOfSubArrayOne++;
        indexOfMergedArray++;
    }
    // Copy the remaining elements of
    // right[], if there are any
    while (indexOfSubArrayTwo < subArrayTwo) {
        array[indexOfMergedArray]
            = rightArray[indexOfSubArrayTwo];
        indexOfSubArrayTwo++;
        indexOfMergedArray++;
    }
    
    delete[] leftArray;
    delete[] rightArray;
}
 
// begin is for left index and end is
// right index of the sub-array
// of arr to be sorted */
void mergeSort(int array[], int const begin, int const end)
{
  
    cout <<  countMe << endl;
    if (begin >= end)
        return; // Returns recursively
 
    auto mid = begin + (end - begin) / 2;
    mergeSort(array, begin, mid);
    mergeSort(array, mid + 1, end);
    merge(array, begin, mid, end);
//      countMe ++;
//      if(countMe == 22)
//      {
//        printArray(array,12);
//      }
}
// 归并算法优化
// 同优化的快速排序一样，对基本已排序序列直接插入排序
// R.Sedgewick 优化：归并时从两端开始处理，向中间推进，简化边界判断

#pragma endregion

#pragma region 桶排序
// Bucket Sort
      // 使用场景：当输入在一个范围内均匀分布时，桶排序主要有用。例如，考虑以下问题。 对范围从 0.0 到 1.0 并且在范围内均匀分布的大量浮点数进行排序。
      // 1) 创建 n 个空桶（或列表）。
      // 2) 对每个数组元素 arr[i] 执行以下操作： 将 arr[i] 插入 bucket[n*array[i]] 
      // 3) 使用插入排序对单个桶进行排序。
      // 4)连接所有排序的桶。
// Function to sort arr[] of 
// size n using bucket sort
void bucketSort(float arr[], int n)
{
      
    // 1) Create n empty buckets
    vector<float> b[n];
  
    // 2) Put array elements 
    // in different buckets
    for (int i = 0; i < n; i++) {
        int bi = n * arr[i]; // Index in bucket
        b[bi].push_back(arr[i]);
    }
  
    // 3) Sort individual buckets
    for (int i = 0; i < n; i++)
        sort(b[i].begin(), b[i].end());
  
    // 4) Concatenate all buckets into arr[]
    int index = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < b[i].size(); j++)
            arr[index++] = b[i][j];
}
#pragma endregion

#pragma region 基数排序
// Radix Sort  思想：
      // elements are in the range from 1 to n2
      // sort such an array in linear time
      // 基数排序的思想是从最低有效位到最高有效位逐位排序。基数排序使用计数排序作为子程序进行排序。
      // 图解：https://youtu.be/xuU-DS_5Z4g
// A utility function to get maximum value in arr[]
// 基数排序也是一种桶排序。 桶排序是按值区间划分桶，基数排序是按数位来划分；基数排序可以看做是多轮桶排序，每个数位上都进行一轮桶排序。
int getMax(int arr[], int n)
{
    int mx = arr[0];
    for (int i = 1; i < n; i++)
        if (arr[i] > mx)
            mx = arr[i];
    return mx;
}
  
// A function to do counting sort of arr[] according to
// the digit represented by exp.
void countSort(int arr[], int n, int exp)
{
    int output[n]; // output array
    int i, count[10] = { 0 };
  
    // Store count of occurrences in count[]
    for (i = 0; i < n; i++)
        count[(arr[i] / exp) % 10]++;
  
    // Change count[i] so that count[i] now contains actual
    //  position of this digit in output[]
    for (i = 1; i < 10; i++)
        count[i] += count[i - 1];
  
    // Build the output array
    for (i = n - 1; i >= 0; i--) {
        output[count[(arr[i] / exp) % 10] - 1] = arr[i];
        count[(arr[i] / exp) % 10]--;
    }
  
    // Copy the output array to arr[], so that arr[] now
    // contains sorted numbers according to current digit
    for (i = 0; i < n; i++)
        {arr[i] = output[i];}
       
       // test
            countMe ++;
            if(countMe == 2)
            {
                  printArray(arr,8);
            }
}
  
// The main function to that sorts arr[] of size n using
// Radix Sort
void radixsort(int arr[], int n)
{
    // Find the maximum number to know number of digits
    int m = getMax(arr, n);
  
    // Do counting sort for every digit. Note that instead
    // of passing digit number, exp is passed. exp is 10^i
    // where i is current digit number
    for (int exp = 1; m / exp > 0; exp *= 10)
      {
            countSort(arr, n, exp);
             
           
      }  
     
}
#pragma endregion


int main()
{
      int arr[] = { 45, 23, 64, 15, 90, 87, 61, 42};
      float arrf[] = { 0.897, 0.565, 0.656, 0.1234, 0.665, 0.3434 };
      int N = sizeof(arr) / sizeof(arr[0]);
      // insertionSort(arr, N); // 插入排序
      // shellSort(arr, N); // 希尔排序
      // selectionSort(arr, N); // 选择排序
      // heapSort(arr, N); // 堆排序
      // bubbleSort(arr, N); // 冒泡排序
      // DualPivotQuickSort(arr, 0, N-1); // 快速排序(双周中值)
      // mergeSort(arr, 0, N - 1); // 归并排序
      // bucketSort(arrf, N); // 桶排序
      radixsort(arr, N); // 基数排序
      printArray(arr, N);
      
 
    return 0;
}