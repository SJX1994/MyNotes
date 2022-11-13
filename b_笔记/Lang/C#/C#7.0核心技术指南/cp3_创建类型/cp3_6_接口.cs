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
      /*在子类中重新实现接口*/
            // 通过在子类中重新实现接口成员，可以实现多态
            public interface IUndoableBase2 { void Undo(); }
            public class TextBox2 : IUndoableBase2
            {
                  // void IUndoableBase2.Undo()  { WriteLine("TextBox2.Undo"); } // 显示实现
                  public void Undo() { WriteLine("TextBox2.Undo"); } // 隐式实现
            }
            public class RichTextBox2 : TextBox2, IUndoableBase2
            {
                  public void Undo() { WriteLine("RichTextBox2.Undo"); }
            }
            // 定义基类时不允许重新实现
            public class TextBox3 : IUndoableBase2
            {
                  void IUndoableBase2.Undo() { Undo(); }// 调用自身的方法
                  protected virtual void Undo() { WriteLine("TextBox3.Undo"); } // sealed 修饰符可以防止重新实现
            }
            public class RichTextBox3 : TextBox3
            {
                  sealed protected override void Undo() { WriteLine("RichTextBox3.Undo"); }
            }
      /*接口和装箱*/
            interface I { void FooBox(); }
            struct S : I { public void FooBox(){} }
            
      class ProgramRun:I
      {
            public void FooBox() { }
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
                  /*虚方法实现接口成员*/ // 隐式
                        // 隐式实现的接口是密封的，为了重写，必须在基类中将其标记为 virtual 或者 abstract
                        // 不管从基类还是接口中调用接口成员，调用都是子类的实现
                        RichTextBox r = new RichTextBox();
                        // r.Undo();
                        // ((IUndoableBase)r).Undo();
                        // ((TextBox)r).Undo();
                  /*在子类中重新实现接口*/ // 显式
                        // 通过接口调用时 重新实现都能劫持成员的实现。
                              RichTextBox2 r2 = new RichTextBox2();
                              // r2.Undo();
                              // ((IUndoableBase2)r2).Undo();
                              // ((TextBox2)r2).Undo();
                        // 定义基类时不允许重新实现
                  /*接口和装箱*/
                        // 将结构体转换为接口会引发装箱。装箱会导致性能损失
                        // 结构体实现接口不会引发装箱。
                        S s = new S();
                        s.FooBox(); // 无装箱
                        I i = s; // 装箱
                       

            }
      }
}
