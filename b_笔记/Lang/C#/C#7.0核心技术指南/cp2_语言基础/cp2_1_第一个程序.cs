using System;
using static System.Console; // import namespace
using cp2_nameSpace; //csc -lib:<yourPath> -reference:cp2_nameSpace.dll cp2_1.cs

namespace cp2 // 命名空间，可以通过using引入
{


    // 计算12*30
    public class cp2_1 // class declaration(声明)
    {
            public static void Main() // Method declaration(声明)
            {
                int x = 12 * 30; // statement1(语句)
                x -= x;
                // WriteLine(x);// statement2(语句)
                // 高层函数调用低层函数 来简化程序 重构（refactor）函数，让函数可以被重用
                WriteLine(FeetToInches(30));
                cp2_nameSpace.Test test = new cp2_nameSpace.Test();
                test.HelloNameSpace();

            }
            // 高层函数调用低层函数 来简化程序 重构（refactor）函数，让函数可以被重用
            public static int FeetToInches(int feet)
            {
                int inches = feet * 12;
                return inches;
            }
    }

}
