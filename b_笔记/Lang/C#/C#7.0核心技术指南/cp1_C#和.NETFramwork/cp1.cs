
using System;
using System.Collections.Generic; // dictionary
/********************************* 6.0 特性 *********************************/
/*引入静态成员*/
using static System.Console; // 静态导入

public class cp1
{

      public static void Main(string[] args)
      //静态类
      /*
      * 仅包含静态成员。
      * 无法实例化。
      * 被密封。不能被继承
      * 不能包含实例构造函数。
      */
      {
            Console.WriteLine("CP1: C#和.NET Framework");
            /********************************* 7.0 特性 *********************************/
            /*数字可加入下划线*/
            int milion = 1_000_000;
            var binary = 0b_0001_0001_0001_0001;
            milion += binary;
            /*输出变量及参数忽略*/
            bool success = int.TryParse("123", out int result); // TryParse 将数字的字符串表示形式转换为其等效的 32 位有符号整数。返回值指示操作是否成功。
            var result_type = result.GetType();
            Program.Method(out _, out string message, out _);//忽略参数
            /*模式*/
            int string_length = Mode_Foo("hello");
            int string_length2 = Mode_Foo2("hi");
            string_length += string_length2;
            /*局部方法*/
            int Cube(int value) => value * value * value; // 类似 Lambda 表达式
            int cube = Cube(3);
            cube += Cube(4);
            /*更多的表达式体成员*/
            Person person = new Person("JAY");
            string JAY = person.name;
            JAY += " ";
            /*解构器*/
            Person person_deconstruct = new Person("Jay Shen ");
            var (first, last) = person_deconstruct; // 解构
            first += last;
            /*元组（tuple）*/
            var bob = ("Bob", 23); // 存储相关值的简单方式
            var bob_name = bob.Item1;
            var bob_age = bob.Item2;
            var bob_tuple = (Name: "Bob_tuple", Money: 23_000_000, AGE: 23); // 使用了 System.ValueTuple<T1,T2,T3...> 的语法糖
            var bob_tuple_name = bob_tuple.Name;
            var bob_tuple_money = bob_tuple.Money;
            var bob_tuple_function = GetPerson2(); // 使用元组替代out
            var bob_tuple_function_name = bob_tuple_function.Name;
            (string tuple_deconstruct_name, int tuple_deconstruct_money, int tuple_deconstruct_age) = GetPerson2(); // 元组支持隐式的解构器
            var bob_info = tuple_deconstruct_name + tuple_deconstruct_money;
            /*throw 异常表达式*/
            //Throw_Foo();
            /********************************* 6.0 特性 *********************************/
            /*null*/
            System.Text.StringBuilder sb = null;// null 条件 不会报 空引用异常 NullReferenceException
            string null_result = sb?.ToString();
            /*Lambda表达式*/
            Func<int, int> square = x => x * x; // Lambda 表达式
            int square_result = square(3);
            int timesTwo_result = TimesTwo(3);
            string someProperty_result = SomeProperty;
            /*属性初始化器*/
            DateTime m_time = TimeCreated;// get
            TimeCreated = DateTime.Today;// set
            TimeCreated = TimeCreated_myBirthday_readOnly;// set
            DateTime m_myBirthday = TimeCreated_myBirthday_readOnly;
            /*索引初始化*/
            var dict = new Dictionary<int, string>()
            {
                  [1] = "one",
                  [2] = "two",
                  [3] = "three"
            };
            /*字符串插值*/
            string jayBirthday = $"JAY's bir: {TimeCreated_myBirthday_readOnly} happy birthday";
            /*异常过滤器*/
            try
            {
                  //Throw_Foo();
            }
            catch (Exception ex) when (ex.Message == "JAY")
            {
                  Console.WriteLine("JAY");
                  Console.WriteLine($"ex is :: {ex}");
            }
            catch (Exception ex) when (ex.Message == "SHEN")
            {
                  Console.WriteLine("SHEN");
                  Console.WriteLine($"ex is :: {ex}");
            }
            catch (Exception ex)
            {
                  Console.WriteLine("EXCEPTION");
                  Console.WriteLine($"ex is :: {ex}");
            }
            /*引入静态成员*/
            // WriteLine(jayBirthday);
            /*返回自定义符号名称*/
            int return_the_name = 123;
            string the_name_retruned = nameof(return_the_name);
            Person m_p2 = new Person("JAY_return_the_name");
            the_name_retruned = nameof(m_p2);
            /********************************* 5.0 特性 *********************************/
            /*async 和 await 的异步通信*/
            /********************************* 4.0 特性 *********************************/
            /*动态绑定*/
            /*可选参数和命名参数*/
            /*泛型接口 和 委托 实现类型变化*/
            /*改进COM互操性*/
            /********************************* 3.0 特性 *********************************/
            /*语言集成查找（LINQ_Language Integrated Query）*/
            /*隐式局部变量var*/
            /*构造器/初始化器*/
            /*Lambda表达式*/
            /*拓展方法*/
            /*查询表达式*/
            /*表达式树 Expression<TDelegate>*/
            /*自动化属性*/
            /********************************* 2.0 特性 *********************************/
            /*泛型*/
            /*可空类型*/
            /*分部类、静态类*/
            WriteLine(the_name_retruned);

      }
      /********************************* 6.0 特性 *********************************/
      /*Lambda表达式*/
      public static int TimesTwo(int x) => x * 2; // Lambda 表达式
      public static string SomeProperty => "Hello World"; // Lambda 表达式
      /*属性初始化器*/
      public static DateTime TimeCreated { get; set; } = DateTime.Now; // 自动初始赋值
      public static DateTime TimeCreated_myBirthday_readOnly { get; } = new DateTime(1994, 11, 14, 12, 0, 0);// 支持只读

      /********************************* 7.0 特性 *********************************/
      /*throw 异常表达式*/
      public static string Throw_Foo() => throw new Exception("Throw_Foo!");
      /*元组（tuple）*/
      public static (string Name, int Money, int Age) GetPerson() // 返回元组
      {
            return ("Bob", 23_000_000, 23);
      }
      public static (string Name, int Money, int Age) GetPerson2() => ("Bob", 23_000_000, 10);  // 返回元组
      /*模式*/
      public static int Mode_Foo(object x)//占位符
      {
            if (x is string s) return (s.Length); // is 运算符 引入 模式变量
            else return 0;
      }
      public static int Mode_Foo2(object x)//占位符
      {
            switch (x)
            {
                  case string s: return s.Length;
                  case int i: return i;
                  case bool b: return 1;
                  case null: return 0;
                  default: return -1;
            }
      }
      /*输出变量及参数忽略*/
      public static void Method(out int answer, out string message, out string stillNull)
      {
            answer = 44;
            message = "I've been returned";
            stillNull = null;
      }




}

/*更多的表达式体成员*/
public class Person
{
      public string name;
      public Person(string name) => Name = name + " _sjx";
      public string Name // 构造函数
      {
            get => name;
            set => name = value + "_SJX";
      }
      /*解构器*/
      public void Deconstruct(out string firstName, out string lastName)
      {
            // 解构器与构造函数相反，它将字段反向赋值给变量。
            name = name.Replace("_sjx_SJX", ""); // 去除多余构造函数字符
            int spacePos = name.IndexOf(" ");
            firstName = name.Substring(0, spacePos);
            lastName = name.Substring(spacePos + 1);
      }
      ~Person() => Console.WriteLine("Finalized"); // 析构函数
}