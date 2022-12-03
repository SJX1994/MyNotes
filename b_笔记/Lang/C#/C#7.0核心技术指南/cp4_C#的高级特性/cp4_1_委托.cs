using System;
using static System.Console;
namespace cp4
{
      class Run
      {
            // 委托 delegate
             // 委托类型 Transformer
             delegate int Transformer(int x);
            static void Main()
            {
                 // 委托 delegate
                 {
                        // 委托是一种知道如何调用方法的对象
                        // 委托和回调类似。一般指 捕获指针的结构
                        // 委托类型（delegate type）定义了一种委托实例（delegate instance）可以调用的方法。
                        // 委托类型（delegate type）定义了方法的返回类型（return type）和参数类型（pargrameter type）。
                        // 存在意义：委托作为调用者的代理，这种 间接调用方式可以将 调用者 和 目标方法 解耦。
                              // 委托类型 Transformer 的委托实例
                              Transformer t = Square; // Transformer t = new Transformer(Square); 的简写
                              // 就可以像方法一样调用
                              int answer = t(3); // out 9 // t.Invoke(3); 的简写
                              // int answer2 = t.Invoke(2); // 在t所在的 指针结构中 传入 2 并返回结果
                        // 用委托书写插件方法
                        {
                              // 委托变量可以在运行时指定一个目标方法，这样就可以在不修改委托类型的情况下，改变委托实例所调用的方法。
                        }


                 }
            }
            // 次方 可以被 委托类型 Transformer 兼容
            static int Square(int x) => x * x; //static int Square(int x) { return x * x; }


      }
}