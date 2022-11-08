using System;
using  C =  System.Console;
namespace cp3
{
    class programRun
    {
        static void Main()
        {
            /*类*/
                // 类是一种引用类型
                {
                    // 复杂类包含：
                        // 在 class 之前的特性(Attribute)非嵌套类的修饰符有：public、internal、abstract、sealed、static、unsafe、partial
                        // 在 class 之后的：泛型参数、唯一基类、接口列表
                        // {}内：类成员(字段、属性、方法、事件、索引器、嵌套类型、运算符、构造函数、析构函数/终结器、静态构造函数)
                }
                // 字段
                {
                    // 修饰符：
                        // 静态修饰符：static
                        // 访问权限相关修饰符：public、protected、internal、private
                        // 继承修饰符：new
                        // 不安全代码修饰符：unsafe
                        // 只读修饰符：readonly
                            // readonly 构造后无法变更
                        // 线程访问修饰符：volatile
                    // 字段初始化
                        // 没有初始化的字段为默认值(0、null、false)
                    // 多字段声明
                        // int x = 1, y = 2, z = 3;
                }
                // 方法
                    // 方法的定义：方法获得输入数据，执行一些操作，并指定输入类型，然后返回数据
                {
                    // 修饰符：
                        // 静态修饰符：static
                        // 访问权限相关修饰符：public、protected、internal、private
                        // 继承修饰符：new、override、virtual、abstract、sealed
                        // 部分方法修饰符：partial
                        // 非托管代码修饰符：unsafe extern
                        // 异步方法修饰符：async
                    // 表达式体方法：
                        // FooFunction3(1);
                    // 重载方法：
                        // FooFunctionOverwrite(2);
                    // 值传递 引用传递
                        // int ref_x = 0;
                        // FooFunctionOverwrite(ref_x);
                    // 局部方法
                        int triple (int x) => x * x * x ; // 确保不会在别的地方使用
                        triple(2);
                        // FooFunctionOverwrite(triple(2));
                }
                // 实例构造器
                    // 类或结构体初始化自己
                {
                    // 修饰符：
                        // 访问权限相关修饰符：public、protected、internal、private
                        // 非托管修饰符：unsafe extern
                    // 实例构造器：
                        cp3_1 cp31 = new cp3_1("bobe");
                        // C.WriteLine(cp31.name); 
                    
                    // 重载构造器：
                    // 隐式无参数构造器：
                        // 如果没有定义构造器，编译器会自动创建一个无参数构造器
                        // 如果定义了构造器，编译器不会自动创建无参数构造器
                    // 初始化顺序
                        // 顺序字段 > 构造器
                    // 非公有构造器
                        // 目的：通过一个静态方法来控制实例的创建
                        // 在引用的统一块内存中持续编辑
                            notPublicConstructor.Create();
                            notPublicConstructor.x = 10;
                            notPublicConstructor.AddOne();
                            // C.WriteLine(notPublicConstructor.x);
                            notPublicConstructor.Create();
                            // C.WriteLine(notPublicConstructor.x);
                    // 解构器
                        // 像是构造器的反过程，传入地址，编辑地址中的值，然后返回
                            var rect = new Rectangle(2.0F,2.0F); // 构造器
                            rect.Deconstruct(out float x, out float y); // 解构器
                            // 上述表达式可简化为： (float x, float y) = rect;
                            // C.WriteLine($"{x} and {y}");
                }
                // 对象初始化器
                {
                    
                }
        }
        // 表达式体方法：
            static int FooFunction(int x){return x*2;}
            static int FooFunction2(int x)=>x*2;
            static void FooFunction3(int x) => C.WriteLine($"{x * 2}") ;
        // 重载方法：
            static void FooFunctionOverwrite(int x) => C.WriteLine($"{x }");
            static void FooFunctionOverwrite(int x, int y) => C.WriteLine($"{x * y}");
            static void FooFunctionOverwrite(int x, int y, int z) => C.WriteLine($"{x * y * z}");
        // 引用传递
            static void FooFunctionOverwrite(ref int x) => C.WriteLine($"{x}");
    }
    class cp3_1
    {
        /*类*/
            // 实例构造器
                public string name; // 申明一块内存
                public int age; 
                public cp3_1(string name) // 构造器
                {
                    this.name = name; // 初始化内存
                }
                // 也可以写作： public cp3_1(string name) => this.name = name;
            // 重载构造器：
                public cp3_1(string name, int age) :this(name) {this.age = age;} // 避免代码重复，this 构造器可以调用另一个构造器
            

    }
    /*类*/
        // 非公有构造器
        public class notPublicConstructor
        {
            public static int x;
            notPublicConstructor(){notPublicConstructor.x = 1;} // 私有构造器
            public static notPublicConstructor Create() => new notPublicConstructor(); // 静态方法
            public static int AddOne()
            {
                return ++x;
            }
        }
        // 解构器
        class Rectangle
        {
            public readonly float Width , Height; 
            public Rectangle(float width, float height) => (this.Width, this.Height) = (width, height);

            public void Deconstruct(out float width, out float height) => (width, height) = (Width+1.0F, Height+1.0F);
        }

}