using System;   

class cp2_4_5_6
{
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
            // 堆和栈 是 存储变量和常量的地方
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
                // 存储垃圾回收
    }
    /*变量和参数*/
        // 栈
        static int Factorial(int x)
        {
            if (x == 0) return 1;
            return x * Factorial(x - 1);
        }
}