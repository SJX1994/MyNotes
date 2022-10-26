/*标识符*/
//是开发者为 类 方法 变量 选择的名字

using System;  // using 是关键字 ; System 是标识符
using static System.Console;
using System.Collections.Generic;
using System.Linq;
/*关键字：delegate*/
    delegate string NamberChanger(int number); // 全局委托
    delegate string NamberMulticasting(string fireInTheHole); // 多播委托
/*关键字：enum*/
    // 值类型 由一组基础整数数字 构成
    enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }
    // 显式指定
    enum ErrorCode : ushort
    {
        None = 0,
        Unknown = 1,
        ConnectionLost = 100,
        OutlierReading = 200
    }
    // 枚举类型表示选项的组合 需要是2的n次方
        [Flags]
        public enum Days
        {
            None      = 0b_0000_0000,  // 0
            Monday    = 0b_0000_0001,  // 1
            Tuesday   = 0b_0000_0010,  // 2
            Wednesday = 0b_0000_0100,  // 4
            Thursday  = 0b_0000_1000,  // 8
            Friday    = 0b_0001_0000,  // 16
            Saturday  = 0b_0010_0000,  // 32
            Sunday    = 0b_0100_0000,  // 64
            Weekend   = Saturday | Sunday
        } // 可以使用 按位逻辑运算符|或&来组合枚举值
class Test // class是关键字 ;Test 是标识符
{
    static string test_multicasting = "Jay  ";
    static void Main() // Main 是标识符; static 和 void 是关键字
    {
        int x = 12; // x 是标识符 int 是关键字
        x += 1;
       // Console.WriteLine(x); // Console 和 WriteLine 是标识符
       /*关键字：abstract*/ 
        // 可以有构造方法
        // 可以有普通成员变量
        // 可以包含非抽象的普通方法
        // 可以包含静态方法
        // 一个类只能继承一个抽象类
        // 接口interface与之相反
        // 表示的是“is a”关系
        // 我的理解：更像是定义了一种超集
        var spiderAnim = new SpiderAnimation(4);
        // spiderAnim.NodesCount();
       /*关键字：as*/
        object as_int = "haha";
            string as_cover = as_int as string;// 如果无法进行转换，则返回 null
            if(as_cover != null)
            {
              //  WriteLine($"res:{as_cover}");
            }else
            {
              //  WriteLine("Failed convert");
            }
       /*关键字：bool*/
        bool debug_mode = false;
       /*关键字：base*/
            // 从派生类中访问基类的成员
            // 条件1：已经被重写了
            // 条件2：指定基类的函数
            // 条件3：只允许在构造函数、实例方法或实例属性访问器 中访问基类
            // 条件4：无法在静态方法中使用
            Weapen001 weapen_001 = new Weapen001(debug_mode);
            weapen_001.GetInformation();
            Weapen002 weapen_002 = new Weapen002(debug_mode);
            weapen_002.GetInformation();
            Jack Jack = new Jack("P-002","Jack",debug_mode);
            Jack.GetInformation();
       /*关键字：break*/
       /*关键字：byte、sbyte*/
        // byte 表示一个 8 位无符号整数。
        string byte_range = $"byte is from {Byte.MinValue} to {Byte.MaxValue}";// 取值范围：0~255
        string sbyte_range = $"byte is from {SByte.MinValue} to {SByte.MaxValue}";// 取值范围：-128~127
        // byte 的二进制、八进制和十六进制 书写形式
        byte[] numbers = {0,16,104,213};
        string title = string.Format("{0}-  {1,10}   {2,5}   {3,3}"/*间隔符*/, "Value", "Binary", "Octal", "Hex");
        // WriteLine(title);
        foreach (byte number in numbers)
        {
            string res = string.Format("{0,5} - {1,10} {2,5} {3,5}", number, Convert.ToString(number, 2), Convert.ToString(number, 8), Convert.ToString(number, 16));
            // WriteLine(res);
        }
       /*关键字：case + switch*/
            int day = 4;
            if(debug_mode)
            {
                switch (day) 
                {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    Console.WriteLine("Saturday");
                    break;
                case 7:
                    Console.WriteLine("Sunday");
                    break;
                }
            }
            
       /*关键字：catch + try + explicit + throw*/
            // catch 用于捕获异常
            // string try_s = null;
            string try_s = "no erreo";
            try
            {
                if (try_s == null)
                {   
                    throw new ArgumentNullException(paramName: nameof(try_s), message: "parameter can't be null.");
                }
            }
            // catch 优先级靠前
            catch (ArgumentNullException e)
            {
                Console.WriteLine("{0} First  exception caught.", e);
            }
            // catch 优先级靠后
            catch (Exception e)
            {
                Console.WriteLine("{0} Second Exception caught.", e);
            }
            

    
       /*关键字：char*/
            // char 表示一个 16 位 Unicode 字符。
            // 字符串类型将文本表示为一系列char值。
            var chars = new[]
            {
                'j', // 字符文字
                '\u006A', // Unicode 十六进制转义序列
                '\x006A', // 十六进制转义序列
                (char)106,// 十进制表示
            };
           // WriteLine(string.Join(" ", chars));
       /*关键字：checked / unchecked*/
        // 溢出检查
        uint int_overflow = uint.MaxValue;
        int_overflow += 1; // 溢出后会进入下一个循环
        unchecked{int_overflow += 1;} // 不检查溢出
        // checked{int_overflow += uint.MaxValue ;} // 溢出异常 可以用 try catch 捕获
       /*关键字：const*/
        // 常量字段和局部变量不是变量，不能修改
       /*关键字：continue*/
        // 继续下一轮循环
       /*关键字：decimal*/
        // decimal 表示一个 128 位精确的十进制值。
        // decimal 不是计算机原始值，需要转换为 int 才能 unchecked 溢出
 
            decimal decimal_num = decimal.MaxValue;
            decimal_num -= 1;
            
     
        string decimal_info = $"decimal from\n{Decimal.MaxValue}\nto\n{Decimal.MinValue}";
        decimal_info += '\n';
       /*关键字：default*/
        // 返回默认值
        int default_int = default(int);
        string default_string = default(string);
        string default_use = string.Format($"{default_int}+{default_string}");
       /*关键字：delegate*/  
        // 对某个方法的 引用类型 变量  
        // 创建委托实例
        
        NamberChanger nc_value = new NamberChanger(NumberValue);
        NamberChanger nc_name = new NamberChanger(NumberName);
        // 使用委托对象调用方法
        int try_delegate = 10;
        string res_delegate = nc_value(try_delegate);
        string res_delegate_name = nc_name(try_delegate);
        // 可以使用“多播”的方法 合并赋值
        NamberMulticasting nm;
        NamberMulticasting nm_multicasting1 = new NamberMulticasting(StringAdder1);
        NamberMulticasting nm_multicasting2 = new NamberMulticasting(StringAdder2);
        nm = nm_multicasting1 + nm_multicasting2;
        // WriteLine(nm("sjx"));
       /*关键字：do while*/        
        // 与while循环相同
        // 但是do while循环至少执行一次
        int do_i = 0;
        do
        { 
           // WriteLine(do_i);
            ++do_i;
            int do_j = 0;
            // 允许嵌套
            do{
               // WriteLine($"haha:{do_j}");
                ++do_j;
            }while(do_j<3);
            
        }
        while(do_i < 5);
       /*关键字：double*/
       /*关键字：enum*/
        // flag 选项组合调用
            Days meetingDay = Days.Monday | Days.Tuesday | Days.Friday | Days.Weekend;
            Days addMeetingDay = Days.Wednesday | Days.Friday;
            bool isMeetingOnTuesday = (meetingDay & Days.Tuesday) == Days.Tuesday;
            bool isMeetingOnThursday = (meetingDay & Days.Thursday) == Days.Thursday;
            // WriteLine($"Meeting on {meetingDay & addMeetingDay}");
            // WriteLine($"{isMeetingOnThursday}");
            // WriteLine($"{isMeetingOnTuesday}");
        // 枚举类型可以直接被转换成int
            int meetingDay_int;
            meetingDay_int = (int)meetingDay;
            var theDay = (Days)32;
            // WriteLine($"{meetingDay_int}");
            // WriteLine($"{theDay}");
            if(isMeetingOnTuesday && isMeetingOnThursday)
            {
                theDay += meetingDay_int + (int)meetingDay + (int)addMeetingDay ;
            }
       /*关键字：event*/ //TODO
        // .NET 中的事件遵循`观察者设计模式`
        // C# 中使用事件机制实现线程间的通信。
        // Ref: https://www.runoob.com/csharp/csharp-event.html

       /*关键字：interface*/
        // 不可以有构造方法
        // 不可以有普通成员变量
        // 不可以包含非抽象的普通方法
        // 不可以包含静态方法
        // 一个类只能继承多个接口
        // 抽象类abstract与之相反
        // 表示的是“like a”关系
        // 我的理解：更像是定义了类的一种属性
        AK47 ak47 = new AK47(10,1,1);
        // ak47.Shoot();
        // WriteLine(ak47.DistanceFromMuzzle); 
        // ak47.Reload(); 
       /*关键字：override + new*/
        // 概念用法 ref: https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/versioning-with-the-override-and-new-keywords
        Dick dick = new Dick();
        dick.headSize = 2;
        dick.headSize = 10;
        dick.balls = 2.5;
        GraphicsClass gc = new GraphicsClass();
        CustomizeDraw cd = new CustomizeDraw();
        GraphicsClass gc_cd_override = new CustomizeDraw(); 
        // gc.DrawPoint();
        // gc.DrawLine();
        // gc.DrawDick(dick);
        // cd.DrawPoint();
        // cd.DrawLine();
        // cd.DrawDick(dick);
        // gc_cd_override.DrawDick(dick); // 重载
        // gc_cd_override.DrawDick(dick.balls); 
        // 使用时机 ref: https://learn.microsoft.com/zh-cn/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords
        // 版本控制
       /*关键字：this*/
         // 1.引用当前类的实例
             Test_this test_this = new Test_this();
             string test_this_res = test_this.Test_this_math(10,10);
             test_this_res += ' ';
             // WriteLine(test_this_res);
         // 2.作为参数传递
            Employee employee = new Employee("sjx", 25);
            // employee.PrintEmployee();
         // 3.索引器
             IndexerBullets clip_20 = new IndexerBullets();
             for(int i = 0; i <5 ; ++i)
             {
                 clip_20[i] = string.Format($"燃烧弹_{i}") ;
             }
             clip_20[5] = "普通子弹_0";
             clip_20[6] = "普通子弹_1";
             for(int j = 0; j < IndexerBullets.bulletCount; ++j)
             {
             //  WriteLine(clip_20[j]);
             }
             // WriteLine(clip_20["燃烧弹_3"]);
       /*关键字：virtual*/ 
            // 情况1：在基类中定义了virtual方法，但在派生类中没有重写该虚方法。那么在对派生类实例的调用中，该虚方法使用的是基类定义的方法。
            // 情况2：在基类中定义了virtual方法，然后在派生类中使用override重写该方法。那么在对派生类实例的调用中，该虚方法使用的是派生重写的方法。
            // 多态性由此体现。常被视为自封装和继承之后，面向对象的编程的第三个支柱。 Polymorphism（多态性）是一个希腊词，指“多种形态”，多态性具有两个截然不同的方面：
            // virtual 关键字用于修改方法、属性、索引器或事件声明，并使它们可以在派生类中被重写。 例如，此方法可被任何继承它的类替代：
                NPC npc = new NPC();
                Enemy enemy = new Enemy();
                NPC npc_enemy = new Enemy();
                // npc_enemy.Talk();
                NPC npc_friend = new Friend();
                // npc_friend.Talk();
            // 计算面积程序实例
                double r = 0.3, h = 0.5;
                Shape cylinder = new Cylinder(r,h);
                Shape sphere = new Sphere(r);
                
                //WriteLine($"球体的表面积为：{sphere.Area()}");
                //WriteLine($"圆柱体的表面积为：{cylinder.Area()}");
            
            // ref:https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/virtual
    }

    /*关键字：delegate*/
        public static string NumberValue(int number)
        {
            return string.Format($"value:{number}");
        }
        public static string NumberName(int number)
        {
            return string.Format($"PragmarName:{ nameof(number)}");
        }
        // 多播
        public static string StringAdder1(string fireInTheHole)
        {
            return test_multicasting += $"{fireInTheHole}_StringAdder1  ";
        }
     
         public static string StringAdder2(string fireInTheHole)
        {
            return test_multicasting += $"{fireInTheHole}_StringAdder2  ";
        }
         public static string StringAdder2(int fireInTheHole) // 重载
        {
            return string.Format($"value:{fireInTheHole}");
        }
}
/*关键字：virtual*/
 // 派生类中的替代成员更改
    public class NPC
    {
        public virtual void Talk()
        {
            Console.WriteLine("NPC: Hello");
        }
        private int _health;
        public virtual int Health
        {
            get { return _health; }
            set { _health = value; }
        }

    }
    public class Enemy : NPC
    {
        public override void Talk()
        {
            base.Talk();
            Console.WriteLine("Enemy: I will destroy you!");
        }
        public override int Health
        {
            get { return base.Health; }
            set { base.Health = value; }
        }
    }

    public class Friend : NPC
    {
        public override void Talk()
        {
            base.Talk();
            Console.WriteLine("Friend: I will help you!");
        }
        public override int Health
        {
            get { return base.Health; }
            set { base.Health = value; }
        }
    }
 // 计算面积程序
    public class Shape
    {
        public const double PI = 3.1415926;
        protected double _x,_y;
        public Shape(){}
        public Shape(double x,double y)
        {
            _x = x;
            _y = y;
        }
        public virtual double Area()
        {
            return 0;
        }
    }
    public class Circle:Shape
    {
        // _x = r,_y = 0
        public Circle(double r):base(r,0) 
        {

        }

        public override double Area()
        {
            return PI * _x * _x;
        }
        
    }
    public class Sphere:Shape
    {
        public Sphere(double r):base(r,0)
        {

        }
        public override double Area()
        {
            return 4 * PI * _x * _x;
        }
    }
    public class Cylinder:Shape
    {
        public Cylinder(double r,double h):base(r,h)
        {

        }
        public override double Area()
        {
            return 2 * PI * _x * _y + 2 * PI * _x * _x;
        }
    }

/*关键字：override + new*/
    public struct Dick
    {
       public int headSize;
       public int bodySize;
       public double balls;
    }
    public class GraphicsClass
    {
        public virtual void DrawPoint(){Console.WriteLine("GraphicsClass DrawPoint");}
        public virtual void DrawLine(){Console.WriteLine("GraphicsClass DrawLine");}
        // 新版本
        //Dick dick = new Dick();
        public virtual void DrawDick(Dick dick){Console.WriteLine($"GraphicsClass DrawDick: {dick.headSize} {dick.bodySize} {dick.balls}");}
        public virtual void DrawDick(double balls){Console.WriteLine($"GraphicsClass DrawDick: {balls} ");}
    }
    public class CustomizeDraw : GraphicsClass
    {
        public new void DrawPoint() // 不需要替代父类方法
        {
            // base.DrawPoint();
            Console.WriteLine("CustomizeDraw DrawPoint");
        }
        public new void DrawLine()
        {
            // base.DrawLine();
            Console.WriteLine("CustomizeDraw DrawLine");
        }
        // 保持自己的版本 对象都将使用 DrawRectangle 的派生类版本
        public override void DrawDick(Dick dick)
        {
            // 调用基类的版本的 DrawDick
            Console.WriteLine($"CustomizeDraw DrawDick {dick}");
        }
        // 多个方法与调用兼容,编译器将选择最佳方法进行调用
        public new void DrawDick(double balls)
        {
            
            Dick dick = new Dick();
            dick.balls = balls;
            base.DrawDick(dick); // 验证父类的版本 确实被改变
            base.DrawDick(dick.balls); 
            Console.WriteLine($"CustomizeDraw DrawDick {balls}");
        }
    }
/*关键字：event*/
    public class EventTest
    {
        //private int value;
        //public delegate void ValueChangedHandler();
        //public event ValueChangedHandler ValueChanged;
        public virtual void OnNumberChange()
        {

        }
    }
/*关键字：this*/
    // 作为参数传递
        class Employee
        {
            public string name;
            public int age;
            private decimal salary = 3000.00m;
            public decimal Salary
            {
                get { return salary; }
            }
            public Employee(string name,int age)
            {
                this.name = name;
                this.age = age;
            }
            public void PrintEmployee()
            {   
                WriteLine($"name:{name},\n age:{age},\n salary:{Salary}");
                WriteLine($" Tax:{Tax.CalculateTax(this)}");
            }
          
        }
        class Tax
        {
            public static  decimal CalculateTax(Employee employee)
            {
                return employee.Salary * 0.2m;
            }
        }
       
    // 作为限定
        public class Test_this
        {
            private int x;
            private int y;
            public string Test_this_math(int x, int y)
            {
                this.x = x;
                this.y = y;
                return string.Format($"value:{x},{y}");
            }
        }
    // 作为索引器 (无安全校验)
        // ref: https://www.tutorialspoint.com/csharp/csharp_indexers.htm
        public class IndexerBullets{
            private string[] typeOfBullet = new string[bulletCount];
            static public int bulletCount = 20;
            public IndexerBullets()
            {
                for(int i = 0; i < bulletCount; i++)
                {
                    typeOfBullet[i] = ("Empty");
                }
            }
            
            public string this[int index]
            {
                get
                {
                    return typeOfBullet[index];
                }
                // 等价于
                    // public int get_num()
                    // {
                    //     return _num;
                    // }
                set 
                {
                    typeOfBullet[index] = value;
                }
                // 等价于：
                    // public void set_num(int value)
                    // {
                    //     _num = value;
                    // }
            }
            // this 重载
            public int this[string name]
            {
                get{
                    int index = 0;
                    
                    while(index < bulletCount)
                    {
                        if( typeOfBullet[index]== name)
                        {
                            return index;
                        }
                        index++;
                    }
                    return index;
                }
            }
        }
/*关键字：interface*/
    //ref:https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/interface
    interface IWapen
    {
        void Shoot();
        void Reload();
        // 接口成员
        int hit_X{get; set; }
        int hit_Y{get; set; }
        int hit_Z{get; set; }
        double DistanceFromMuzzle { get; }
    }
    public class AK47 : IWapen
    {
        public  void Shoot()
        {
            Console.WriteLine("AK47 Shooting");
        }
        public void Reload()
        {
            Console.WriteLine("AK47 Reloading");
        }
        // 接口成员实现
        public AK47(int x, int y,int z)
        {
            hit_X = x;
            hit_Y = y;
            hit_Z = z;
        }
        public int hit_X { get; set; }
        public int hit_Y { get; set; }
        public int hit_Z { get; set; }
        public double DistanceFromMuzzle =>
       Math.Sqrt(hit_X * hit_X + hit_Y * hit_Y + hit_Z * hit_Z);
    }
/*关键字：abstract*/ 
    // 隐藏内部细节仅显示功能
    abstract class ProceduralAnimation
    {
        public abstract int NodesCount();
        public abstract string[] NodeNames();
        /*...*/
    }
    class SpiderAnimation:ProceduralAnimation
    {
        private int _nodesCount;
        public SpiderAnimation(int n) => _nodesCount = n; // 构造函数
        public override int NodesCount()
        {
            for(int i=0; i <= _nodesCount; ++i)
            {
                
                if(i == _nodesCount - 1)
                {
                    Console.WriteLine($"SpiderAnimation has {i+1} nodes");
                } else
                {
                    continue;
                }
            }
            return _nodesCount;
        }
        public override string[] NodeNames()
        {
            return new string[] {"Head", "Body", "Leg1", "Leg2", "Leg3", "Leg4", "Leg5", "Leg6"};
        }
        /*...*/
    }

/*关键字：base*/
    public class Person
    {
         string id = "P-001";
         string name = "Jay";
         bool debug = false;
        public Person()
        {
           
        }
        public Person(bool init_debug)
        {
            debug = init_debug;
            if(debug)
            {
                WriteLine($"基类: Parent 已经构造(传入参数：debug：{debug})");
            }
            
        }
        public Person(string init_id, string init_name,bool init_debug)
        {
            
            
            id = init_id;
            name = init_name;
            debug = init_debug;
            if(debug)
            {
                WriteLine($"基类: Parent 已经构造 (传入参数：id:{id}  name:{name} debug：{debug})");
                WriteLine($"Name: {name}");
                WriteLine($"P ID: {id}");
            }
            
        }
        public virtual void GetInformation()
        { 
            if(debug)
            {
                WriteLine($"Name: {name}");
                WriteLine($"P ID: {id}");
            }
            
        }
        public string getId()
        {
            return id;
        }
        public string getName()
        {
            return name;
        }
        
    }
    // base 用法1
        class Weapen001:Person // 重写
        {
            bool debug = true;
            public string weapen_id = "-Weapen(001)";
            public Weapen001(bool init_debug):base(init_debug)
            {
                debug = init_debug;
            }
            public override void GetInformation()
            {
                base.GetInformation(); // base关键字，可以Getinfo从派生类中调用基类上的方法。 
                if(debug)
                {WriteLine($"get weapen ID: {weapen_id}");}
                
            }
        }
        class Weapen002:Person // 重写
        {
            bool debug = true;
            public string weapen_id = "-Weapen(002)";
            public Weapen002(bool init_debug):base(init_debug)
            {
                debug = init_debug;
            }
            public override void GetInformation()
            {
                base.GetInformation(); // base关键字，可以Getinfo从派生类中调用基类上的方法。 
                if(debug)
                {WriteLine($"get weapen ID: {weapen_id}");}
                
            }
        }
    // base 用法2
        public class Jack:Person // 派生 会使用所有基类的构造函数
        {
            public Jack():base(){}
            public Jack(bool debug):base(debug){}// 派生类会调用 BaseClass.BaseClass(bool debug)
            public Jack(string id, string name,bool debug):base(id,name,debug){}// 派生类会调用 BaseClass.BaseClass(string id, string name)
            public override void GetInformation()
            {
                    
            }
        }