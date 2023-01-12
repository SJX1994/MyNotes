using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;
namespace cp7
{
      public class R
      {
            static void Main()
            {
                  #region List<T> ArrayList
                        // List<T> ArrayList 是可动态调整大小的对象数组实现
                        // 在集合中追加元素的效率很高（因为数组末尾一般都有空闲位置）
                        // 插入会慢一些，因为需要移动元素
                        List<string> words = new List<string>();
                        words.Add("hello ");                        
                        words.Add("world ");
                        words.AddRange(new[] { "hello1", "world1 " });
                        words.Insert(0, "hello2 "); // 加在第0位
                        words.InsertRange(0, new[] { "hello3", "world3 " });
                        words.Remove("hello "); // 移除第一个匹配的元素
                        words.RemoveAt(3); // 移除第0位的元素
                        words.RemoveRange(0, 2); // 移除第0位开始的2个元素
                        words.RemoveAll(w => w.StartsWith("h")); // 移除所有匹配的元素
                        string[] wordsCopy = new string[1000]; 
                       // words.CopyTo(0,wordsCopy,900,2); // 从第0位开始复制2个元素到第998位
                        foreach (string word in words)
                        {
                              Write(word);
                        }
                         
                  #endregion
                  #region LinkedList<T>
                        // LinkedList<T> 是双向链表实现
                        // 插入元素更高效
                        // 但是访问元素的效率较低 必须遍历每一个节点 并且无法执行二分搜索
                        // 添加节点时可以可以指定他相对其他节点的位置 或者指定列表的开始结束位置
                        LinkedList<string> wordsLinkList = new LinkedList<string>();
                        wordsLinkList.AddFirst("hello ");
                        wordsLinkList.AddLast("world ");
                        wordsLinkList.AddAfter(wordsLinkList.First,"world1 ");
                        wordsLinkList.AddAfter(wordsLinkList.First.Next,"hello1 ");
                        wordsLinkList.AddBefore(wordsLinkList.Last, "hello2 ");
                        wordsLinkList.RemoveFirst();
                        wordsLinkList.RemoveLast();
                        LinkedListNode<string> node = wordsLinkList.Find("hello1 ");
                        wordsLinkList.Remove(node);
                        wordsLinkList.AddFirst("worldAdd ");
                        WriteLine("\nLinkedList\n");
                        foreach (string word in wordsLinkList)
                        {
                              Write(word);
                        }
                  #endregion
                  #region Queue<T> Queue
                  #endregion
            }
      }
      
}