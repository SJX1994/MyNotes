// 析构函数概念
      // 成员函数的一种，名字与类名相同，在前面加 ‘~’，没有参数和返回值，一个类最多只有一个析构函数
      // 对象消亡时 自动被调用
      // 定义类时没写析构函数, 则编译器生成缺省析构函数
#include <iostream>
using namespace std;
class MString{
      private :
      char * p;
      public:
      MString () {
      p = new char[10];
      }
      ~ MString ()
      {
            delete [] p;
            cout << "\n MString destructor called";
      }
};
int main()
{
      // delete释放时调用
      MString * sp;
      sp = new MString();
      delete sp;
      // 结束运行时调用
      MString s;
      return 0;
}