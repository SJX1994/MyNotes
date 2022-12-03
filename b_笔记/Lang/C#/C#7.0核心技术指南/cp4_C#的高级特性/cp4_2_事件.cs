using System;
using static System.Console;
// 当使用委托时 会出现两种 角色：广播者(broadcaster) 订阅者(subscriber)
// 广播者：是包含委托字段的类型，它通过调用者决定何时进行广播
// 订阅者：是方法目标的接收者，通过在广播者的委托中调用 += 和 -= 来决定何时监听 何时结束监听。订阅者 不知道也不会印象其他订阅者。
namespace cp4
{
      /*事件*/
        // 委托声明
        public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
      class Run
      {
            
            static void Main()
            {
                  /*事件*/
                  {
                        /*标准事件模式*/
                        {
                              // .Net 为事件编程定义了一个标准模式。其目的是为了保持框架和用户代码的一致性。
                              // 标准事件的核心是 System.EventArgs类，它预定义了一个没有成员的类。
                              // 在示例中我们可以继承 EventArgs 类，以便在事件触发时传递信息。
                              StockStandard stockStandard = new StockStandard("THPW");
                              stockStandard.Price = 27.10M;
                              stockStandard.PriceChanged += stockStandard_PriceChanged; // 订阅价格变动信息
                              stockStandard.Price = 31.59M;
                              stockStandard.Price = 21.59M;
                              stockStandard.PriceChanged -= stockStandard_PriceChanged; // 取消订阅价格变动信息
                              stockStandard.Price = 131.59M;

                              StockEmpty stockEmpty = new StockEmpty("THPW");
                              stockEmpty.Price = 27.10M;
                              stockEmpty.PriceChanged += stockEmpty_PriceChanged_e; // 订阅价格变动信息
                              stockEmpty.Price = 22.10M;
                        }
                        /*事件访问器*/
                        {

                        }
                  }
            }
            /*标准事件模式*/
            static void stockStandard_PriceChanged( object sender, PriceChangedEventArgs e )
            {
                  if((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                  {
                        WriteLine("警报，股价上涨10%！");
                  }
            }

            static void stockEmpty_PriceChanged_e( object sender, EventArgs e )
            {
                  
                  WriteLine("拉拉");
                  
            }
           
      }
      /*标准事件模式*/
      #region 标准事件模式
      // 需要传递信息的委托事件：
            public class PriceChangedEventArgs : EventArgs // 考虑到复用性 EventArgs 子类， 应当更具它包含的信息命名，而非使用它的事件。
            {
                  // 一般将数据以属性或只读字段的方式暴露给外界。
                  public readonly decimal LastPrice;
                  public readonly decimal NewPrice;
                  public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
                  {
                        LastPrice = lastPrice;
                        NewPrice = newPrice;
                  }
            }
            // 选择或者定义事件的委托
                  // 遵循3大规则：
                  // 1.委托必须由void作为返回值
                  // 2.委托必须接受两个类型 object 和 EventArgs 作为参数（object 作为事件的广播者，EventArgs包含需要传递的额外信息）
                  // 3.委托名称必须以EventHandler结尾
                  // 框架定义了名为 System.EventHandler<>的泛型委托，该委托满足以上提到的三个条件。
            public class StockStandard
            {
                  string symbol;
                  decimal price;
                  public StockStandard(string symbol) // 构造
                  {
                        this.symbol = symbol;
                  }
                  /*事件的修饰符*/
                  // 可以是 virtual abstract overridden sealed static
                  // public static event EventHandler<PriceChangedEventArgs> PriceChanged;
                  public event EventHandler<PriceChangedEventArgs> PriceChanged; // 委托类型的事件。这里使用了泛型的EventHandler<>委托
                  // --- 关键
                  protected virtual void OnPriceChanged(PriceChangedEventArgs e) // 事件触发器：受 EventHandler 模式影响 需要时protected 的虚方法才能触发事件，方法名必须和事件名称一致 前面必须加一个On 并且 必须接收唯一的 EventArgs 参数
                  {
                        PriceChanged?.Invoke(this, e); // 在多线程情形下，为了保证线程安全，在测试和调用委托之前要将它保存在临时变量中。
                        // var temp = PriceChanged;
                        // if(temp != null)
                        // {
                        //       temp(this, e);
                        // }
                        // WriteLine("事件被激活");
                  }
                  // --- 关键
                  public decimal Price // 属性
                  {
                        get { return price; }
                        set
                        {
                              if (price == value) return;
                              decimal oldPrice = price;
                              price = value;
                              OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
                        }
                  }
            }

      // 如果事件不需要传递额外信息，可以使用非泛型 EventHandler 委托。
      // 本例中重写 StockStandard ， 当 Price 属性发生变化时，触发 OnPriceChange 事件,事件除了传达已发生的消息之外没有必须包含的信息（传递的参数）。为了避免创建非必要的EventArgs 实例，使用了 System.EventArgs.Empty 静态字段。
            public class StockEmpty
            {
                  string symbol;
                  decimal price;
                  public StockEmpty(string symbol) // 构造
                  {
                        this.symbol = symbol;
                  }
                  public event EventHandler PriceChanged; // 委托类型的事件。这里使用了非泛型 EventHandler 委托
                  /*事件访问器*/
                  // 以上 语句 编译器将执行以下操作：
                  // 1.创建一个私有的委托字段，用于保存事件的订阅者。
                  // 2.创建一个访问器，用于添加和删除事件的订阅者。
                  // 显示替代：
                  // private EventHandler priceChanged;
                  // public event EventHandler PriceChanged
                  // {
                  //       add { priceChanged += value; }
                  //       remove { priceChanged -= value; }
                  // }
                  // 更复杂的事件访问器操作：
                        // 1. 当前访问器 时 广播事件类的 中继器
                        // 2. 当类定义了大量事件，而大部分事件只有很少订阅者。在这种情况下 最好用字典中存储订阅。因为字典比空委托的存储开销更少
                        // 3. 当显式实现声明事件的接口时。// 见 158 页
                  // --- 关键
                  protected virtual void OnPriceChanged(EventArgs e) // 事件触发器：受 EventHandler 模式影响 需要时protected 的虚方法才能触发事件，方法名必须和事件名称一致 前面必须加一个On 并且 必须接收唯一的 EventArgs 参数
                  {
                        PriceChanged?.Invoke(this, e); // 在多线程情形下，为了保证线程安全，在测试和调用委托之前要将它保存在临时变量中。
                        // var temp = PriceChanged;
                        // if(temp != null)
                        // {
                        //       temp(this, e);
                        // }
                        // WriteLine("事件被激活");
                  }
                  // --- 关键
                  public decimal Price // 属性
                  {
                        get { return price; }
                        set
                        {
                              if (price == value) return;
                              price = value;
                              OnPriceChanged(EventArgs.Empty);
                        }
                  }
            }
      #endregion
      /*事件*/
      #region 事件
      class Stock
      {
            string symbol;
            decimal price;
            public Stock(string symbol){this.symbol = symbol;} // 构造
            /*事件*/
              // 事件声明：
              public event PriceChangedHandler PriceChanged; // Stock 类型的代码对 PriceChanged 有完全的访问权，并可以将其视为委托。而 Stock 类型之外的代码 则 仅可以在 PriceChanged 时间上进行 += -= 运算。
              // 此事件委托声明时，内部发生三件事：
              // 1. 编译器将事件翻译成如下：
                  // PriceChangedHandler priceChanged; // 私有委托
                  // public event PriceChangedHandler PriceChanged // 公有事件
                  // {
                  //       add { priceChanged += value; } // 事件访问器 订阅
                  //       remove { priceChanged -= value; } // 事件访问器 取消订阅
                  // }
              // 2. 编译器在 Stock 类型中找到除调用+= 和 -= 之外的 priceChanged 引用点，并将他们重定向到内部的 priceChanged 委托字段。
              // 3. 编译器对事件上的 += 和 -= 相应的调用事件的 add 或 remove 访问器。 += -= 行为是唯一的，无法用 + - 来表示。
            public decimal Price
            {
                  get { return price; }
                  set
                  {
                      if(Price == value) return; // 如果没有改变就退出
                      decimal oldPrice = price;
                      price = value;
                      if(PriceChanged != null) // 如果List中存在调用
                      {
                            PriceChanged(oldPrice, price); // 开启事件
                            WriteLine("事件开启");
                      }
                  }
            }
            // 本例中，如果将 event 关键字去掉,PriceChanged就变成普通的委托字段，虽然运行结果不变，但是 Stock 类就没有原来健壮了。因为这时订阅者可以通过以下方式互相影响。
            // 1. 通过重新指派 PriceChanged 替换其他订阅者（不适用+=来订阅）
            // 2. 可以清楚所有订阅者（将 PriceChanged 设置为 null）
            // 3. 通过调用其委托 广播到其他的订阅者。
      }
      #endregion
}