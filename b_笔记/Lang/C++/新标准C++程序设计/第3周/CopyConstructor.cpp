// 复制构造函数的基本概念
      // 1.只有一个参数,即对同类对象的引用。
      // 2.形如 X::X( X& )或X::X(const X &), 二者选一，后者能以常量对象作为参数
      // 3. 如果没有定义复制构造函数，那么编译器生成默认复制构造函数。默认的复制构造函数完成复制功能。
#include <iostream>
using namespace std;
// 默认的复制构造函数
class Complex {
private :
 double real,imag;
}; 
// 定义的自己的复制构造函数
class Complex_2 {
public :
 double real,imag;
      Complex_2(){ }
      Complex_2( const Complex_2 & c ) {
            real = c.real;
            imag = c.imag;
            cout << "Copy Constructor called";
      }

}; 
 // 复制构造函数起作用的三种情况
       // 1. 当用一个对象去初始化同类的另一个对象时。
void ComplexWork_1()
{
      Complex_2 c1;
      // Complex_2 c2(c1); // 等于下面的语句
      Complex_2 c2 = c1; // 初始化语句，非赋值语句
}
       // 2. 如果 某函数 有一个参数是类 A 的对象，那么 该函数 被调用时，类A的复制构造函数将被调用。
class A 
{
      public:
            A() { };
            A( A & a) { 
            cout << "\n Copy constructor called AAA";
            }
};
void Func(A a1){ }
      // 3. 如果函数的返回值是类A的对象时，则函数返回时，A的复制构造函数被调用:
class B 
{
 public:
      int v;
      B(int n) { v = n; };
      B( B & b) { 
      v = b.v;
      cout << "\n   Copy constructor called BBB" ; // 不会被调用
      }
};
B FuncB() { 
      B b(4); 
      
      return b; 
}

int main()
{
      // Complex c1; // 调用缺省无参构造函数
      // Complex c2(c1); // 调用缺省的复制构造函数,将 c2 初始化成和c1一样
      // Complex_2 c3; 
      // Complex_2 c4(c3);// 调用自己定义的复制构造函数，输出 Copy Constructor called
      ComplexWork_1();
      A a2;
      Func(a2);
      FuncB(); // 目前看起来不会掉用
      return 0;
}