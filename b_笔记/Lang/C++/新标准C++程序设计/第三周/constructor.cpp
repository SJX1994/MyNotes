// 构造函数
      // 作用：是对对象进行初始化，如给成员变量赋初值。
      // 特性1：
            // 如果定义类时没写构造函数，则编译器生成一个默认的无参数的构造函数
      // 特性2：
            // 对象生成时构造函数自动被调用。只能生成一次
      // 特性3：
            // 一个类可以有多个构造函数

#include <iostream>
using namespace std;

class Complex {
private :
double real, imag;
public:
Complex( double r, double i = 0);
// 多个构造函数:
Complex (double r );
Complex (Complex c1, Complex c2);
}; 

Complex::Complex( double r, double i) {
      real = r; imag = i;
      cout<<("Complex constructor called") << endl;
}
// 多个构造函数:
Complex::Complex(double r)
{
      real = r; imag = 0;
}
Complex::Complex (Complex c1, Complex c2)
{
      real = c1.real+c2.real;
      imag = c1.imag+c2.imag;
}



int main(){
      // Complex c1; // error, 缺少构造函数的参数
      // Complex * pc = new Complex; // error, 没有参数
      Complex c1(2,4), c2(3,5);
      Complex * pc = new Complex(3,4);
      return 0;
}
