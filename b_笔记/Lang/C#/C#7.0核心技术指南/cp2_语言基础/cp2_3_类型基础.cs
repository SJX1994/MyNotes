using System;
using static System.Console;
class cp2_3
{
    static void Main()
    {
        int x /*变量表示一个存储位置，它的值可能不停变换，常量 const 始终表示一个值*/ = 12 * 3 ; // x表示一个存储位置 储存着 字面量12*3
        x += 1;
        /*所有c#类型*/
            /*值类型*/
                // 数值类型 char类型 bool类型 结构体 枚举
                Point p_valueTypes1 = new Point();
                p_valueTypes1.X = 3;
                Point p_valueTypes2 = p_valueTypes1; // 值类型的赋值是值的复制,所以 1 2 各占一块 (x和y)存储空间
                // 此时 1.x 2.x  的值都为 3
                p_valueTypes2.X = 4;
                // 此时 1.x 的值为3 2.x 的值都为 4 
                // WriteLine(p_valueTypes2.X);
                // WriteLine(p_valueTypes1.X);

            /*引用类型*/
                // 类 接口 数组 委托
                Point_ref p_referenceTypes1 = new Point_ref();
                p_referenceTypes1.X = 3;
                Point_ref p_referenceTypes2 = p_referenceTypes1; // 引用类型的赋值是引用的复制,所以 1 2 同指向一块 (x和y)存储空间
                // 此时 1.x 2.x  的值都为 3
                p_referenceTypes2.X = 4;
                // 此时 1.x 2.x  的值都为 4
                WriteLine(p_referenceTypes2.X);
                WriteLine(p_referenceTypes1.X);
            /*泛型类型*/
            /*指针类型*/
    }
}
/*所有c#类型*/
/*值类型*/
    public struct Point{public int X,Y;} // 申请了两块(x和y) int(32位的) 存储空间

/*引用类型*/
    public class Point_ref{public int X,Y;} // 申请了一块(x和y) int(32位的) 存储空间 和 一个引用