#include <iostream>
using namespace std;

// 静态成员 static 关键字
 // 1. 普通成员变量每个对象有各自的一份，而静态成员变量一共就一份，为所有对象共享。本质上是全局变量，哪怕一个对象都不存在，类的静态成员变量也存在。
 // 2. 普通成员函数必须具体作用于某个对象，而静态成员函数并不具体作用与某个对象。因此静态成员不需要通过对象就能访问。
 // 目的：是将和某些类紧密相关的全局变量和函数写到类里面，看上去像一个整体，易于维护和理解。
 // tips：在静态成员函数中，不能访问非静态成员变量，也不能调用非静态成员函数。
class CRectangle
{
      private:
      int w, h;
      static int nTotalArea; //静态成员变量
      static int nTotalNumber; //静态成员变量
      public:
      CRectangle();
      CRectangle(int w_,int h_);
      ~CRectangle();
      static void PrintTotal()
      {
            cout << "\n PrintTotal";
      } //静态成员函数 
}; 
int main()
{
      // 具体静态成员访问方法：
       // 类名::成员名
      CRectangle::PrintTotal();
       // 对象名.成员名 // 不可访问
      // CRectangle r;
      // r.PrintTotal();
       // 指针->成员名
      CRectangle * p; 
      p->PrintTotal();
       // 引用.成员名 // 不可访问
      // CRectangle & ref = r; 
      // ref.PrintTotal();
      return 0;
}