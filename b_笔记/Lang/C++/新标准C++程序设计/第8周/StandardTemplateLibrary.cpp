// 标准模板库 (Standard Template Library)
      // 面向对象的思想: 继承和多态，标准类库
      // 概述：
      // 将一些常用的数据结构（比如链表，数组，二叉树）和算法（比如排序，查找）写成模板，以后则不论数据结构里放的是什么对象，算法针对什么样的对象，则都不必重新实现数据结构，重新编写算法。
      // 基本的概念
            // 1. 容器：可容纳各种数据类型的通用数据结构,是类模板.被插入的是对象的一个复制品。
               // 顺序容器:vector, deque,list
                  // vector:
                    // 动态数组。元素在内存连续存放。在尾端增删元素具有较佳的性能
                  // deque:
                    // 双端队列。元素在内存连续存放。在头尾增删元素具有较佳的性能
                  // list:
                    // 双向链表。元素在内存不连续存放。在任意位置增删元素具有较佳的性能
               // 关联容器: set, multiset, map, multimap
                  // set:
                    // 有序集合。元素在内存不连续存放。元素具有唯一性。插入和查找元素具有较佳的性能
                  // multiset:
                    // 有序集合。元素在内存不连续存放。元素不具有唯一性。插入和查找元素具有较佳的性能
                  // map:
                    // 有序映射。元素在内存不连续存放。元素具有唯一性。插入和查找元素具有较佳的性能
                  // multimap:
                    // 有序映射。元素在内存不连续存放。元素不具有唯一性。插入和查找元素具有较佳的性能
               // 容器适配器: stack, queue, priority_queue
                  // stack:
                    // 栈。元素在内存连续存放。在尾端增删元素具有较佳的性能
                  // queue:
                    // 队列。元素在内存连续存放。在尾端增删元素具有较佳的性能
                  // priority_queue:
                    // 优先队列。元素在内存连续存放。在尾端增删元素具有较佳的性能
            // 2. 迭代器：可用于依次存取容器中元素，类似于指针
                  // 定义一个容器类的迭代器的方法可以是：
                        // 容器类名::iterator 变量名;
                        // 或：
                        // 容器类名::const_iterator 变量名;
                        // 访问一个迭代器指向的元素：
                        // * 迭代器变量名
            // 3. 算法：用来操作容器中的元素的函数模板
// 迭代器示例：
#include <vector>
#include <iostream>
using namespace std;
int main() {
      vector<int> v; //一个存放int元素的数组，一开始里面没有元素
      v.push_back(1); 
      v.push_back(2); 
      v.push_back(3); 
      v.push_back(4);
      vector<int>::const_iterator i; //常量迭代器
      for( i = v.begin();i != v.end();++i ) 
      {cout << * i << ",";}
      cout << "\n";
      vector<int>::reverse_iterator r; //反向迭代器
      for( r = v.rbegin();r != v.rend();r++ ) 
      {cout << * r << ",";}
      cout << "\n";
      vector<int>::iterator j; //非常量迭代器
      for( j = v.begin();j != v.end();j ++ ) 
      {* j = 100;}
      for( i = v.begin();i != v.end();i++ ) 
      {cout << * i << ",";}
      cout << "\n";
      return 0;
}