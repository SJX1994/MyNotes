#include <iostream>
using namespace std;

// 常量对象
      class Demo{
            private :
                  int value = 100;
            public:
                  void SetValue() { }
      };
      const Demo Obj; // 常量对象
// 常量成员函数
     // 在类的成员函数说明后面可以加const关键字，则该成员函数成为常量成员函数。

      class Sample 
      {
            private :
                  int value = 100;
            public:  
                  int GetValue() const 
                  { return value ; } // 不可修改
                  void func() { };
                  Sample() { }
      }; 
// 常引用
      class SampleRef
      {
            private :
                  int value = 100;
            public:
                  void SetValue(int &ref) 
                  {
                        // 这样函数中就能确保不会出现无意中更改o值的语句了。
                         ref += value; 
                  }
      };
      
// 入口程序
int main()
{
      // 常量成员函数
      const Sample objTest1; 
      Sample objTest2;
      cout << objTest1.GetValue() << "," << objTest2.GetValue() ;
      // 常引用
      SampleRef objTestRef;
      int value = 100;
       objTestRef.SetValue(value);
      cout <<"\n"<<value << endl;
      return 0;

}