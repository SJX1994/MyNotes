#include <iostream>
using namespace std;
// 运算符重载为友元函数
 // 使用场景：
      // 成员函数不能满足使用要求
      // 普通函数, 又不能访问类的私有成员
class Complex{
private:
      double real, imag;
public:
      Complex(double r, double i):real(r), imag(i){ }; 
      Complex operator+(double r); 
      // 所以将 运算符 + 重载为友元函数
      friend Complex operator + (double r, const Complex & c);

};
// 只能编译 c = c + 5;
Complex Complex::operator+(double r){ //能解释 c+5
 return Complex(real + r, imag); 
}
// 为了编译 c = 5 + c;
Complex operator+ (double r, const Complex & c) {
//能解释 5+c 但 普通函数不能访问私有成员
 return Complex( c.real + r, c.imag);
}
int main()
{
      Complex c (0,0);
      c = c + 5; // c = c.operator +(5); 
      // 为了编译 c = 5 + c; 重载 + 为友元
      Complex c2 (0,0);
      c2 = 5 + c2;
      return 0;
}