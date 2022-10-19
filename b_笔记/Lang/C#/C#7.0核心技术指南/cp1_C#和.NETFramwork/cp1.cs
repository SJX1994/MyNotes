
using System;
public class Program
{

    public static void Main(string [] args) 
    //静态类
    /*
    * 仅包含静态成员。
    * 无法实例化。
    * 被密封。不能被继承
    * 不能包含实例构造函数。
    */
    {
        Console.WriteLine("CP1: C#和.NET Framework");
        // ******************************** 7.0 特性 ********************************
        /*数字可加入下划线*/
        int milion = 1_000_000;
        var binary = 0b_0001_0001_0001_0001;
        milion += binary;
        /*输出变量及参数忽略*/
        bool success = int.TryParse("123", out int result); // TryParse 将数字的字符串表示形式转换为其等效的 32 位有符号整数。返回值指示操作是否成功。
        var result_type = result.GetType();
        Program.Method(out _ , out string message , out _) ;//忽略参数
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
        var (first,last) = person_deconstruct; // 解构
        first += last;
        /*元组（tuple）*/
        var bob = ("Bob",23); // 存储相关值的简单方式
        var bob_name = bob.Item1;
        var bob_age = bob.Item2;
        var bob_tuple = (Name:"Bob_tuple", Money:23_000_000 , AGE:23); // 使用了 System.ValueTuple<T1,T2,T3...> 的语法糖
        var bob_tuple_name = bob_tuple.Name;
        var bob_tuple_money = bob_tuple.Money;
        var bob_tuple_function = GetPerson2(); // 使用元组替代out
        var bob_tuple_function_name = bob_tuple_function.Name;
        (string tuple_deconstruct_name, int tuple_deconstruct_money,int tuple_deconstruct_age) = GetPerson2(); // 元组支持隐式的解构器
        var bob_info = tuple_deconstruct_name + tuple_deconstruct_money;
        /*throw 异常表达式*/
        //Throw_Foo();
        // ******************************** 6.0 特性 ********************************
        Console.WriteLine( bob_info);

    }
    /*throw 异常表达式*/
    public static string Throw_Foo() => throw new Exception("Throw_Foo!");
    /*元组（tuple）*/
    public static (string Name, int Money, int Age) GetPerson() // 返回元组
    {
        return ("Bob", 23_000_000, 23);
    }
    public static (string Name, int Money, int Age) GetPerson2() =>("Bob", 23_000_000,10);  // 返回元组
    /*模式*/
    public static int Mode_Foo(object x)//占位符
    {
        if (x is string s) return(s.Length); // is 运算符 引入 模式变量
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
        name = name.Replace("_sjx_SJX","") ; // 去除多余构造函数字符
        int spacePos = name.IndexOf(" ");
        firstName = name.Substring(0, spacePos);
        lastName = name.Substring(spacePos + 1);
    }
    ~Person() => Console.WriteLine("Finalized"); // 析构函数
}