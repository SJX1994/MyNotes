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
                  // 集合
                        // 其他接口的丰富程度依次是：
                         //  IEnumerator < IEnumerator<T> < IEnumerable IEnumerable<T> < ICollection<T> <  ICollection  < IList < IList<T> <  IDictionary < IDictionary<TKey, TValue> 
                  {
                        // 枚举：共性 都要 遍历 集合内容
                        {
                              // 简单如：数组 链表
                              // 复杂如：红黑树 散列表
                              /*IEnumerable IEnumerator*/
                              {
                                    // IEnumerator 和 IEnumerable 及其泛型接口 是.NET Framework 提供的接口
                                     
                                     // IEnumerator 通过 IEnumerable 提供枚举器
                                     // 多个调用者可以同时遍历同一个集合而不互相影响
                                     WriteLine("IEnumerator:");
                                     string s = "Hello";
                                     IEnumerator ie = s.GetEnumerator(); // 因为 string 是 IEnumerable 的实现类，所以可以调用  GetEnumerator 方法
                                           while (ie.MoveNext()) // MoveNext 方法将“游标”向前移动到下一个位置，返回 bool 值，表示 是否还有下一个元素
                                           {
                                                 char c = (char)ie.Current; // Current 属性返回当前元素
                                                 Write(c+".");
                                           }
                                    // ie.MoveNext(); // 会抛出异常，因为已经没有下一个元素了
                                    // 更简洁的方法是 foreach 重写了 上述的 while 循环
                                          WriteLine("\n foreach:");
                                          foreach (char c in s)
                                          {
                                                Write(c + ".");
                                          }
                                   
                              }
                              /*IEnumerable<T> IEnumerator<T>*/
                              {
                                    // 避免了值类型元素的装箱开销
                                    // int[] data_int = { 1, 2, 3, 4, 5 };
                                    // var rator = ((IEnumerable<int>)data_int).GetEnumerator();
                                    // 可以使用 foreach 语句遍历
                                    WriteLine("\n IEnumerable<char>:");
                                    string s2 = "Hello";
                                    var rotor = ((System.Collections.Generic.IEnumerable<char>)s2).GetEnumerator();
                                    using(rotor) // 确保枚举器已经销毁
                                    {
                                          while (rotor.MoveNext())
                                          {
                                                char c = rotor.Current;
                                                Write(c + "!");
                                          }
                                    }
                                    // 等价于：
                                    WriteLine("\n foreach:");
                                    foreach (char c in s2)
                                    {
                                          Write(c + "!");
                                    }
                                    // 用法：递归的计算任意集合的元素个数
                                    // WriteLine("\n 递归的计算任意集合的元素个数:");
                                    // int[] data_int = { 1, 2, 3, 4, 5 };
                                    // string s3 = "Helloooo";
                                    // WriteLine($"{Count(data_int)}");
                              }
                              /*实现枚举接口*/
                              {
                                    // 实现 IEnumerable 和 IEnumerable<T> 接口的原因：
                                          // 为了支持 foreach 语句
                                          // 为了与任何标准集合进行交互
                                          // 为了达到一个成熟的集合接口要求
                                          // 为了支持集合初始化器
                                    // 要实现 IEnumerable 和 IEnumerable<T> 接口，必须实现 GetEnumerator(枚举器) 方法
                                          // 如果这个类 包装 了任何一个集合，那么可以直接返回这个集合的枚举器
                                          // 使用 yield return 来进行迭代     
                                                // 迭代器是 C# 语言的特性，它能够协助完成集合的编写，并且可以用 foreach 消费。

                                          // 实例化自己的 IEnumerator<T> 或 IEnumerator 实现
                                    // 可以使用 Linq 进行查询
                                    // 迭代器实现：
                                          // IEnumerable
                                          MyCollection mc = new MyCollection();
                                          var myCollection = mc.GetEnumerator();
                                          WriteLine("\n GetEnumerator:");
                                          while (myCollection.MoveNext())
                                          {
                                                Write(myCollection.Current+"--");
                                          }
                                          // IEnumerable<T>
                                          MyGenCollection myGenCollection = new MyGenCollection();
                                          var myGenCollection2 = myGenCollection.GetEnumerator();
                                          WriteLine("\n GetEnumerator<T>:");
                                          while (myGenCollection2.MoveNext())
                                          {
                                                Write(myGenCollection2.Current+"-.-");
                                          }
                                          // 简单 IEnumerable<T> 
                                          WriteLine("\n SimpleGetEnumerator<T>:");
                                          foreach(int i in SimpleIE_Test.GetSomeIntegers())
                                          {
                                                Write(i+"-s-");
                                          }
                                          // 编写一个实现 IEnumerator 的类 （与迭代器所作的工作完全相同，大多数情况不需要这么做）
                                          WriteLine("\n 实现 GetEnumerator<T>:");
                                          var myEnumerator = new MyIntList();
                                          foreach(int i in myEnumerator)
                                          {
                                                Write(i+"-a-");
                                          }
                              }
                        }
                        // ICollection 和 IList 接口
                        {
                              // TODO
                        }
                  }

            }
            
            /*IEnumerable<T> IEnumerator<T>*/
             // 存在缺陷：如果存在循环引用，会导致死循环
             public static int Count(IEnumerable data)
             {
                   int count = 0;
                   foreach (var item in data)
                   {
                         var subCollection = item as IEnumerable;
                         if (subCollection != null)
                         {
                               count += Count(subCollection);
                         }
                         else
                         {
                               count++;
                         }
                   }
                   return count;
             }
      

      }
      /*实现枚举接口*/
      public class MyCollection : IEnumerable // 迭代器处理 IEnumerable 和 IEnumerator
      {
            private int[] data = { 1, 2, 3, 4, 5 };
            public IEnumerator GetEnumerator() // 编译器会生成一个隐藏的嵌套枚举器类 并重构 GetEnumerator
            {
                  foreach (var item in data)
                  {
                        yield return item;
                  }
            }
      }
      public class MyGenCollection : IEnumerable<int> // 泛型接口 IEnumerable<T>
      {
            private int[] data = { 1, 2, 3, 4, 5 };
            public IEnumerator<int> GetEnumerator() // 编译器会生成一个隐藏的嵌套枚举器类 并重构 GetEnumerator
            {
                  foreach (var item in data)
                  {
                        yield return item;
                  }
            }
            IEnumerator IEnumerable.GetEnumerator() // 编译器会生成一个隐藏的嵌套枚举器类 并重构 GetEnumerator
            {
                  return GetEnumerator();
            }
      }
      public class SimpleIE_Test// 简单泛型接口 IEnumerable<T>
      {
            // 把迭代逻辑放入一个泛型枚举器中 编译器会完成工作                 
            public static IEnumerable<int> GetSomeIntegers()
            {
                  yield return 1;
                  yield return 10;
                  yield return 3;
                  yield return 1;
                  yield return 5;
            }
      }
      // 编写一个实现 IEnumerator 的类 （与迭代器所作的工作完全相同，大多数情况不需要这么做，这个使用泛型枚举接口的例子会更快，因为没有拆装箱）
            // public class MyIntList : IEnumerable
            // {
            //       private int[] data = { 1, 2, 3, 4, 5 };
            //       public IEnumerator GetEnumerator()
            //       {
            //             return new MyIntListEnumerator(this);
            //       }
            //       class MyIntListEnumerator : IEnumerator // 为 MyIntListEnumerator 定义一个 内部类/嵌套类 为了增加代码的可维护性
            //       {
            //             private MyIntList list;
            //             private int index;
            //             public MyIntListEnumerator(MyIntList list)
            //             {
            //                   this.list = list;
            //                   // index = -1;
            //             }
            //             public object Current
            //             {
            //                   get
            //                   {
            //                         // 边界检查
            //                         if(index==-1)throw new InvalidOperationException("枚举器还未开始");
            //                         if(index==list.data.Length)throw new InvalidOperationException("枚举器已经结束");
            //                         return list.data[index];
            //                   }
            //             }
            //             public bool MoveNext() // 第一次调用moveNext时，返回的是第一个元素
            //             {
            //                   index++;
            //                   if(index >= list.data.Length-1)
            //                   {
            //                         return false;
            //                   }
            //                   return index < list.data.Length;
            //             }
            //             public void Reset()
            //             {
            //                   index = -1; // 不是必须实现的 可以抛出 NotSupportedException
            //             }
            //       }
            // }
       // 为了和迭代器功能保持一致，还必须实现IEnumerable<T>接口
      public class MyIntList : IEnumerable<int>
      {
            private int[] data = { 1, 2, 3, 4, 5 };
            // 通用的枚举器 既可以被 IEnumerable 也可以被 IEnumerable<T> 完成
            // 在实施非通用的 GetEnumerator 方法时，需要明确的避免命名冲突
            public IEnumerator<int> GetEnumerator(){return new MyIntListEnumerator(this);}
            IEnumerator IEnumerable.GetEnumerator(){ return new MyIntListEnumerator(this);}
            class MyIntListEnumerator : IEnumerator<int>
            {
                  private MyIntList list;
                  private int index;
                  public MyIntListEnumerator(MyIntList list)
                  {
                        this.list = list;
                  }
                  public int Current => list.data[index];
                  object IEnumerator.Current => list.data[index];
                  public bool MoveNext() => ++index < list.data.Length;
                  public void Reset() => index = -1;
                  // 我们不需要 Dispose 方法，所以在公开方法中隐藏
                  void IDisposable.Dispose(){}
                 
            }
      }
}