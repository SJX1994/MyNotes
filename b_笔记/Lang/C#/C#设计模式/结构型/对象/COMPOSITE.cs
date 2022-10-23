using System;
using System.Collections.Generic;
// ref: https://www.dofactory.com/net/composite-design-pattern
namespace Composite.Structural
{
    public class Run
    {
        public static void Main()
        {
            // Create a tree structure
            Composite root = new Composite("root");
            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));
            Composite comp = new Composite("Composite X");
            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            Composite comp2 = new Composite("Composite XX");
            comp2.Add(new Leaf("Leaf XXA"));
            Leaf leaf_comp2_XXB = new Leaf("Leaf XXB");
            comp2.Add(leaf_comp2_XXB);
            comp.Add(comp2);
            comp.Add(new Leaf("Leaf XC"));
            root.Add(comp);
            root.Add(new Leaf("Leaf C"));
            // Add and remove a leaf
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);
            // Recursively display tree
            //root.Display(1);
            comp2.Display(1);
             leaf_comp2_XXB.Display(1);
            // Wait for user
            Console.ReadKey();
        }
    }
    
    public abstract class Component
    {
        protected string name;
        public Component(string name)
        {
            this.name = name;
        }
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    } 
    public class Composite : Component
    {
        string local_string = "Composite：";
        List<Component> children = new List<Component>();
         
        public Composite(string name): base(name)
        {
            name = string.Format($"Composite_{name}");
        }// 派生类会调用 Component.Component(string name)

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
        }

         public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + local_string + name );
            // Recursively display child nodes
            foreach (Component component in children)
            {
                component.Display(depth + 2); // 递归调用
            }
        }

        
    }
    public class Leaf : Component
        {
            string local_string = "Leaf：";
            // Constructor
            public Leaf(string name)
                : base(name)
            {
                name = "123";
            }
            public override void Add(Component c)
            {
                Console.WriteLine("Cannot add to a leaf");
            }
            public override void Remove(Component c)
            {
                Console.WriteLine("Cannot remove from a leaf");
            }
            public override void Display(int depth)
            {
                Console.WriteLine(new String('-', depth) + local_string + name); // 递归调用
            }
        }
}
