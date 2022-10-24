// ref: https://refactoringguru.cn/design-patterns/observer/csharp/example
using System;
using System.Collections.Generic;
using System.Threading;
namespace Observer.Structural
{
    /*** 观察者 ***/ 
        // 为那些在目标发生改变时需获得通知的对象定义一个更新接口。
        public interface IObserver
        {
            void Update(ISubject subject);
        }
    /*** 观察目标 ***/
        public interface ISubject
        {
            // 订阅
            void Attach(IObserver observer);
            // 取消订阅
            void Detach(IObserver observer);
            // 通知
            void Notify();
        }
    /*** 具体目标 ***/
        public class Subject:ISubject
        {
            // （简化模型）存储所有订阅者的关键状态
            public int State{get; set; } = -0;
            // 观察者队列 这里可以用 event 来替代
            private List<IObserver> _observers = new List<IObserver>();

            // 订阅系统管理
            public void Attach(IObserver observer)
            {
                Console.WriteLine("Subject: Attached an observer.");
                this._observers.Add(observer);
            }
            public void Detach(IObserver observer)
            {
                this._observers.Remove(observer);
                Console.WriteLine("Subject: Detached an observer.");
            }
            // 通知
            public void Notify()
            {
                Console.WriteLine("Subject: Notifying observers...");
                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }
            // 业务逻辑：一旦状态发生改变，就通知观察者
            public void SomeBusinessLogic()
            {
                Console.WriteLine("\n Subject: I'm doing something important.");
                this.State = new Random().Next(0, 10); // 返回一个1-10之间非负的随机数
                Thread.Sleep(15);// 挂起或暂停当前线程执行
                Console.WriteLine("Subject: My state has just changed to: " + this.State);
                this.Notify();
            }
        }
    /*** 具体观察者们 ***/
        // 通过状态来判断那些观察者需要被通知然后响应
        public class ConcreteObserverA : IObserver
        {
            public void Update(ISubject subject)
            {
                if ((subject as Subject).State < 3)
                {
                    Console.WriteLine("ConcreteObserverA: Reacted to the event.");
                }
            }
        }
        public class ConcreteObserverB : IObserver
        {
            public void Update(ISubject subject)
            {
                if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
                {
                    Console.WriteLine("ConcreteObserverB: Reacted to the event.");
                }
            }
        }
        class ProgramRun
        {
            static void Main()
            {
                // 用户开发者代码：
                var subject = new Subject();
                var observerA = new ConcreteObserverA();
                subject.Attach(observerA);
                var observerB = new ConcreteObserverB();
                subject.Attach(observerB);

                subject.SomeBusinessLogic();
                subject.SomeBusinessLogic();

                subject.Detach(observerB);

                subject.SomeBusinessLogic();


            }
        }
}