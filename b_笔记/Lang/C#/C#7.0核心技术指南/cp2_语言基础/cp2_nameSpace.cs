using System;
using static System.Console; 
namespace cp2_nameSpace
{
    public class Test
    {
        public void HelloNameSpace()
        {
           var out_string_nameof = " what erver"   ;
            WriteLine($"Hello, 你输入的变量名为： {nameof(out_string_nameof)} !");
                   
        }
        
        static void Main(string[] args)
        {
        }
       


    }

}