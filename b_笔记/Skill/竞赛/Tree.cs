// *
//  * Definition for a binary tree node.
//  * public class TreeNode {
//  *     public int val;
//  *     public TreeNode left;
//  *     public TreeNode right;
//  *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
//  *         this.val = val;
//  *         this.left = left;
//  *         this.right = right;
//  *     }
//  * }

using System;
//单个节点
public class Tree {
    //构建树(数据，做分叉，右分叉)公私分明比较安全
    private int nodeData;
  
    public int NodeData
    {
      get{return nodeData;}
    }
    private Tree leftTree;
    public Tree LeftTree
    {
      get{return leftTree;}
      set{leftTree = value;}
    }
    private Tree rightTree;
    public Tree RightTree
    {
      get{return rightTree;}
      set{rightTree = value;}
    }
   
    public Tree (int nodeValue) {
        nodeData = nodeValue;
    }
    //增
    public void Insert(int newItem)
    {
      Console.WriteLine("Tree:Insert: \n Last:" + newItem.ToString() +"\n Now:"+nodeData.ToString());
      
      int currentNodeValue = nodeData;
      //安全校验
      if(newItem >= currentNodeValue)
      {
          //检查 与 生成
          if(leftTree==null)
          {
            //没有就创建一个新的
            Console.WriteLine("Tree:Insert: LeftTree null can be Insert for " + newItem.ToString() + "creat LeftNode");
            leftTree = new Tree(newItem);
          }
          else
          {
            //有了就插入
            Console.WriteLine("Tree:Insert: LeftTree already got one  for "+ newItem.ToString() + "will be LeftNode");
            leftTree.Insert(newItem);
          }
      }else
          {
            if(rightTree==null)
            {
              //没有就创建一个新的
              Console.WriteLine("Tree:Insert: RightTree null can be Insert for "+ newItem.ToString() + "creat RigtNode");
              rightTree = new Tree(newItem);
            }
            else
            {
              //有了就插入
              Console.WriteLine("Tree:Insert: RightTree already got one  for "+ newItem.ToString()+ "will be RightNode");
              rightTree.Insert(newItem);
            }
          }
    }
    //删 TODO
    //改 TODO
    //查
      //就单节点树的高度
      public int Height()
      {
          //递归检查 TODO
        return 0;
      }
      //就单节点自查节点数量
      public int NumberOfLeafNoeds()
      {
          //递归检查
          if (this.leftTree == null && this.rightTree == null)
          {
                return 1; //found a leaf node
          }
          int leftLeaves = 0;
          int rightLeaves = 0;
          if (this.leftTree != null)
          {
                leftLeaves = leftTree.NumberOfLeafNoeds();
          }
          if (this.rightTree != null)
          {
                rightLeaves = rightTree.NumberOfLeafNoeds();
          }
        
          return leftLeaves + rightLeaves;
      }
    
    //可视化2D TODO
    //执行一次Tree生成一个带有 ID 和 标签（左 或者 右），和屏幕坐标，的结构体，并且保存
    //在引擎中实例化出N（执行Tree的次数个）图形，读取这些信息做条件判断，渲染到屏幕坐标上
}
//操作节点
public class BinaryTree
{
   private Tree root;
   public Tree Root
   {
     get{return root;}
   }
    //插入整颗树
   public void Insert(int data)
   {
     if(root != null)
     {
       Console.WriteLine("BinaryTree:Insert: have root"+ data.ToString()+"add");
       root.Insert(data);
     }else
     {
       Console.WriteLine("BinaryTree:Insert: creat root"+ data.ToString()+"creat");
       root = new Tree(data);
     }
   }
    //查找整颗树
    public int NumberOfLeafNodes()
    {
      if (root == null)
      { return 0; }
      return root.NumberOfLeafNoeds();
    }
}
public class Test
{
  public static void Main()
  {
    Console.WriteLine("Hello,World");
    BinaryTree t = new BinaryTree();
    t.Insert(2);
    t.Insert(33);
    t.Insert(22);
    t.Insert(1);
    int treesNmb = t.NumberOfLeafNodes();
    Console.WriteLine("Totel get:" + treesNmb.ToString() + "treeNodes" );
    
  }
}