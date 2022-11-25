using System;
using System.Collections;
using static System.Console;
namespace cp7
{
      public class R
      {
            static void Main()
            {
                  /*IEnumerable IEnumerator*/
                   // IEnumerator 通过 IEnumerable 提供枚举器
                   // 多个调用者可以同时遍历同一个集合而不互相影响
                   string s = "Hello";
                   IEnumerator ie = s.GetEnumerator(); // 因为 string 是 IEnumerable 的实现类，所以可以调用 GetEnumerator 方法
                        while (ie.MoveNext()) // MoveNext 方法返回 bool 值，表示是否还有下一个元素
                        {
                              char c = (char)ie.Current; // Current 属性返回当前元素
                              Write(c+".");
                        }


            }
      }
}