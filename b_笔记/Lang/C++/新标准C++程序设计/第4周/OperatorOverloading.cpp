#include <iostream>
using namespace std;
// 运算符重载
      // C++预定义运算：
      // +, -, *, /, %, ^, &, ~, !, |, =, <<, >>, != ……
      // 调用类的成员函数  操作它的对象
      // 例如: complex_a和complex_b是两个复数对象，求两个复数的和
      // complex_a + complex_b
      // 格式：
      // 返回值类型 operator 运算符（形参表）{}

class Complex {
public:
      Complex( double r = 0.0, double i= 0.0 ){
      real = r; 
      imaginary = i;
      }
      double real; // real part
      double imaginary; // imaginary part
};
Complex operator+ (const Complex & a, const Complex & b)
{
 return Complex( a.real+b.real, a.imaginary+b.imaginary);
} // “类名(参数表)” 就代表一个对象

int main()
{
      Complex a(1.0, 2.0), b(2.0, 3.0), c;
      c = a + b;
      cout << "Complex operator:" << c.real <<" & " << c.imaginary << endl;
      return 0 ;
}
