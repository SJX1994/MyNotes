using System;
using static System.Console;

namespace cp3
{
      public interface IEnumerable // IEnumerator是所有非泛型枚举器的基本接口
      {
            bool MoveNext();
            object Current { get; }
            void Reset();
      }
      internal class CountDown : IEnumerable
      {
            int count = 11;
            public bool MoveNext() => --count >= 0;

            public object Current => count;
            public void Reset()
            {
                  throw new NotImplementedException();
            }

      }
      /*拓展接口*/
            // 接口可以从其他接口派生
            public interface ICloneable { void Undo(); }
            public interface IUndoable : ICloneable { void Redo(); }
      /*显示接口*/
            // 当实现多个接口时，会出现成员前面冲突。显示实现接口成员可以解决冲突
            interface I1 { void M(); }
            interface I2 { int M(); }
            public class C : I1, I2
            {
                  void I1.M() { WriteLine("I1.M"); }
                  int I2.M() { return 42; }
            }
      /*虚方法实现接口成员*/
            // 隐式实现的接口是密封的，为了重写，必须在基类中将其标记为 virtual 或者 abstract
            public interface IUndoableBase { void Undo(); }
            public class TextBox: IUndoableBase
            {
                  public virtual void Undo() { WriteLine("TextBox.Undo"); }
            }
            public class RichTextBox : TextBox
            {
                  public override void Undo() { WriteLine("RichTextBox.Undo"); }
            }

      class ProgramRun
      {
            static void Main()
            {
                  // 接口和类相似,但接口只为成员提供定义而不实现。
                  // - 接口的成员都是隐式抽象的
                  // - 一个类（或者结构体）可以实现多个接口，而类只能继承一个类，结构体完全不接受继承
                  // - 接口总是隐式public的，并且不能显示声明public
                  IEnumerable countDown = new CountDown();
                  while (countDown.MoveNext())
                  {
                        // WriteLine(countDown.Current);
                  }
                  /*拓展接口*/
                        // 接口可以从其他接口派生
                  /*显示接口*/
                        // 当实现多个接口时，会出现成员前面冲突。显示实现接口成员可以解决冲突
                        C c = new C();
                        // ((I1)c).M();
                        ((I2)c).M();
                  /*虚方法实现接口成员*/
                        // 隐式实现的接口是密封的，为了重写，必须在基类中将其标记为 virtual 或者 abstract
                        // 不管从基类还是接口中调用接口成员，调用都是子类的实现
                        RichTextBox r = new RichTextBox();
                        // r.Undo();
                        // ((IUndoableBase)r).Undo();
                        // ((TextBox)r).Undo();



            }
      }
}
