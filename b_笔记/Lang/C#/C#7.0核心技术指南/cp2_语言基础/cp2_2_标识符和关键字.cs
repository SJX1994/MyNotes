/*标识符*/
//是开发者为 类 方法 变量 选择的名字

using System;  // using 是关键字 ; System 是标识符
using static System.Console;
/*关键字：delegate*/
    delegate string NamberChanger(int number); // 全局委托
    delegate string NamberMulticasting(string fireInTheHole); // 多播委托

class Test // class是关键字 ;Test 是标识符
{
    static string test_multicasting = "Jay  ";
    static void Main() // Main 是标识符; static 和 void 是关键字
    {
        int x = 12; // x 是标识符 int 是关键字
        x += 1;
       // Console.WriteLine(x); // Console 和 WriteLine 是标识符
       /*关键字：abstract*/ 
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
        WriteLine(nm("sjx"));
        
        

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