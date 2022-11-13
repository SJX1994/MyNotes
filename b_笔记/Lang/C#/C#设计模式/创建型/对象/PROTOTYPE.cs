using  System;

namespace Prototype.Structural
{
    // 克隆自身的接口
    public interface IEmployee
    {
        IEmployee Clone();
        string GetDetails();
    }
    // 具体的克隆自身的“第一种”操作
        public class Developer : IEmployee
        {
            public int WordsPerMinute { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string PreferredLanguage { get; set; }

            public IEmployee Clone()
            {
            // Shallow Copy: only top-level objects are duplicated
                return (IEmployee)MemberwiseClone();

            // Deep Copy: all objects are duplicated
            //return (IEmployee)this.Clone();
            }

            public string GetDetails()
            {
                return string.Format("{0} - {1} - {2}", Name, Role, PreferredLanguage);
            }
        }
    // 具体的克隆自身的“第二种”操作  /*无限拓展*/
        public class Typist : IEmployee
        {
            public int WordsPerMinute { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }

            public IEmployee Clone()
            {
            // Shallow Copy: only top-level objects are duplicated
                return (IEmployee)MemberwiseClone();

            // Deep Copy: all objects are duplicated
            //return (IEmployee)this.Clone();
            }

            public string GetDetails()
            {
                return string.Format("{0} - {1} - {2}wpm", Name, Role, WordsPerMinute);
            }
        }
    // run
        public class Program
        {
            public static void Main()
            {
                Developer dev = new Developer
                {
                    Name = "John",
                    Role = "Software Developer",
                    PreferredLanguage = "C#"
                };
                Developer devCopy = (Developer)dev.Clone();
                devCopy.Name = "Jane";
                Console.WriteLine(dev.GetDetails());
                Console.WriteLine(devCopy.GetDetails());

                Typist typist = new Typist
                {
                    Name = "Jack",
                    Role = "Typist",
                    WordsPerMinute = 100
                };
                Typist typistCopy = (Typist)typist.Clone();
                typistCopy.Name = "Jill";
                Console.WriteLine(typist.GetDetails());
                Console.WriteLine(typistCopy.GetDetails());
            }
        }
}