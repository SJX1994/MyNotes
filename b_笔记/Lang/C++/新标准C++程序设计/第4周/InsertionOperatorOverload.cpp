#include <iostream>
#include <string>
using namespace std;
// 流插入运算符 和 流提取运算符的重载
 //  cout << 5 << “this”; (流插入运算符)
 //  cin >> c >> n; (流提取运算符)
class Complex {
      double real,imag; 
public:
      Complex( double r=0, double i=0):real(r),imag(i){ }; 
      friend ostream & operator<<( ostream & os, 
      const Complex & c);
      friend istream & operator>>( istream & is,Complex & c); 
};
ostream & operator<<( ostream & os,const Complex & c)
{
      os << c.real << "+" << c.imag << "i"; //以"a+bi"的形式输出
      return os;
}
istream & operator>>( istream & is,Complex & c)
{
      string s;
      is >> s; //将"a+bi"作为字符串读入, “a+bi” 中间不能有空格
      int pos = s.find("+",0); 
      string sTmp = s.substr(0,pos); //分离出代表实部的字符串
      c.real = atof(sTmp.c_str());//atof库函数能将const char*指针指向的内容转换成 float 
      sTmp = s.substr(pos+1, s.length()-pos-2); //分离出代表虚部的字符串
      c.imag = atof(sTmp.c_str());
      return is;
}

int main()
{
      Complex c;
      int n; 
      cin >> c >> n; 
      cout << c << "," << n;
      // 输入 5+6i 7
      // 输出 5+6i,7
      return 0;
}