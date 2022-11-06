using System;   
using static System.Console;
class cp2_4_5_6
{
    // 引用返回值
    static string X = "old value";
    static void Main()
    {
        /*数值字面量*/
            // https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
            // 可以用十进制 或者 16进制表示 或者 二进制表示 或者 指数表示
            int x = 0x2A; // 16进制
            int y = 42; // 10进制
            int z = 0b_0010_1010; // 2进制
            double a = 1E06; // 指数表示
            a += x + y + z; 
            // 显示后缀定义类型
                // https://www.jianshu.com/p/eb45efd9d3ce
            // 数值转换
            // 运算符：
                // + - * / % ++ -- += -= *= /= %=
                // 溢出检查
            
        /*布尔字面量*/
            // 三元条件运算符
               uint a_b = 1U,b_b =3U;
               uint res_b = (a_b>b_b)?a_b:b_b;
        /*字符串和字符*/
            // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/
        /*数组*/
        /*变量和参数*/
        {
            // 堆和栈 是 存储变量和常量的地方 但有着不同的生命周期
            {
                // 栈 存储的是 值类型的变量和常量 
                    // 存储局部变量
                    // 存储参数
                    // 存储返回值
                    // 存储方法调用
                    // 存储异常处理
                Factorial(3);
                // 堆 存储所有 引用类型
                // 存储对象
                // 存储数组
                // 存储字符串
                // 存储委托
                // 存储匿名对象
                // 存储匿名方法
                // 存储lambda表达式
                // 存储泛型
                // 存储迭代器
                // 存储垃圾回收(GC回收未引用的对象,变量和常量会一直存活)
                StringBuilder sb = new StringBuilder("obj1"); // StringBuilder 被 sb 引用，目前被 sb 被 GC 选中
                // WriteLine(sb.S);
                StringBuilder sb2 = new StringBuilder("obj2");
                StringBuilder sb3 = sb2; // sb2 被引用 所以 不会被 GC 选中
                // WriteLine(sb3.S);
            }
            // 明确赋值
            {
                // 除了unsafe 
                // 局部变量在读取前必须赋值
                // 除非标记可选参数 不然必须赋值
                
            }
            // 默认值
            {
                // default 关键字对泛型非常有用
            }
            // 参数
            {
                // 修饰符:
                // 无(值类型)： 按值传递 传入
                    // 值传递
                        int p = 1; 
                        PassParameters(p); // 参数值 + 参数值的副本 总共占用两个内存 存在于两个不同的内存位置
                        // p 依然为 1
                        PassParameters(); // 可选参数
                    // 引用传递
                        StringBuilder sbp = new StringBuilder("passing"); // 目前存在了: 两个引用 sbp fooSB; StringBuilder 的两个变量; 但在 PassRef 中 fooSB 在值传入之前 是为 null 的  
                        PassRef(sbp); // 现在 fooSB 被 sbp 赋值了 
                // ref: 按引用传递 传入
                    // p_ref 和 x_ref 指向同一块内存 所以此时 p_ref 为 4
                        int p_ref = 3;
                        PassWithRefKeyWord(ref p_ref);
                        
                    // 实现交换方法 ref 是必须的：
                            string a_value = "a";
                            string b_value = "b";
                            Swap(ref a_value,ref b_value);
                            // WriteLine($"{a_value} {b_value}");

                // out: 按引用传递 传出
                    // 不要在传入函数之前赋值
                    // 必须在函数结束之前赋值
                    string firstNameOut,lastNameOut; // 创建两个变量的内存
                    PassOutSpilt("jay shen",out firstNameOut,out lastNameOut); // 传入引用 并接受对内存值的改变
                    // 丢弃变量
                    PassOutSpilt("jay shen",out firstNameOut,out _); // 丢弃 第二个变量地址的传入
                // params 修饰符
                    // 参数类型必须声明为数组
                        PassParams(1,2,3,4,5,6,7,8,9,10); // 等价于 PassParams(new int[]{1,2,3,4,5,6,7,8,9,10});
                // 用名称确定参数位置 
                    // COM api 会用到
                    string a_pos_in,b_pos_in,c_pos_in;
                    a_pos_in = "a";
                    b_pos_in = "b";
                    c_pos_in = "c";
                    PassWithPosition(a_pos:a_pos_in,b_pos:b_pos_in,c_pos:c_pos_in); // 传入参数的顺序可以不一样
            }
            // 引用局部变量
            {
                // 用于小范围优化
                    int[] numbers = {0,1,2,3,4,5,6,7,8,9};
                    ref int numRef = ref numbers[2]; // numRef 引用了 numbers[2] 的内存地址
                    numRef = 20; // numbers[2] 也会变成 20
                    // foreach(int n in numbers)
                    // {
                    //      WriteLine(n);
                    // }
                    
            }
            // 引用返回值
            {
                
                ref string XRef = ref GetX(); // XRef 引用了 X 的内存地址
                XRef = "new value"; // X 也会变成 new value
                WriteLine(X);
            }
            // var隐式局部变量
        }
            
    }
    
    /*变量和参数*/
        // 引用返回值
            static ref string GetX()
            {
                return ref X;
            }
        // 用名称确定参数位置
            static void PassWithPosition(string a_pos,string b_pos,string c_pos)
            {
                 //WriteLine($"{a_pos} {b_pos} {c_pos}");
            }
        // params 以数组传入参数
            static void PassParams(params int[] numbers )
            {
                 // WriteLine(numbers.Length);
            }
        // 参数
            // 无
                // 传入时创建了传递参数值的副本
                static void PassParameters(int x = 10) // 当不传入 x 时 x 为 10
                
                {
                    x = ++x ; // x 为 2
                }
                // 传入时创建了引用以及null的实例化的变量
                static void PassRef(StringBuilder fooSB)
                {
                    fooSB.S = fooSB.S;
                }
            // ref
                static void PassWithRefKeyWord( ref int x_ref)
                {
                    x_ref = ++x_ref;
                }
                static void Swap(ref string a, ref string b)
                {
                    string temp = a;
                    a = b;
                    b = temp;
                }
            // out
                static void PassOutSpilt(string name,out string firstName,out string lastName)
                {
                        int i = name.LastIndexOf(' ');
                        // 修改内存内容并传出
                        firstName = name.Substring(0,i); 
                        lastName = name.Substring(i+1); 
                }
        // 栈
            static int Factorial(int x)
            {
                if (x == 0) return 1;
                return x * Factorial(x - 1);
            }
        
}
class StringBuilder
{
    string s = null;
    public StringBuilder(object x)
    {
        this.s = (string)x;
    }
    public string S
    {
        get { return s;}
        set { s = "StringBuilder" + value; }
    }
    
}