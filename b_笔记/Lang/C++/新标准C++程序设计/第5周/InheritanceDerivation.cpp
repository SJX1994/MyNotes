#include <iostream>
#include <string.h>
using namespace std;
// 继承和派生
  //  继承：在定义一个新的类B时，如果该类与某个已有的类A相似(指的是B拥有A的全部特点)，那么就可以把A作为一个基类，而把B作为基类的一个派生类(也称子类)。
  // 特点：
      // 1. 派生类是通过对基类进行修改和扩充得到的。在派生类中，可以扩充新的成员变量和成员函数。
      // 2. 派生类一经定义后，可以独立使用，不依赖于基类。
      // 3. 派生类拥有基类的全部成员函数和成员变量，不论是private、protected、public 。
      // 4. 在派生类的各个成员函数中，不能访问基类中的private成员
class CStudent
{
private:
      string sName;
      int nAge;
      bool isThreeGood;
public:
      bool IsThreeGood() {
            return isThreeGood;
      };
      void SetName( const string & name ) 
      { 
            sName = name; 
            if(sName.length()>1)
            {
                  isThreeGood = true;
            }else
            {
                  isThreeGood = false;
            }
      }
      //......
}; 
class CUndergraduateStudent: public CStudent  // class 派生类名：public 基类名
{
private: 
      int nDepartment;
public:
      // bool IsThreeGood() { ...... }; //覆盖
      bool CanBaoYan() { 
            if(CStudent::IsThreeGood()) 
            { return true; }           
            else 
            { return false; }
                 
            };
}; 

class CBase
{
      int v1,v2;
}; // 父类 保存着 v1 v2 的值 内存位置在前
class CDerived:public CBase
{
      int v3;
}; // 子类 保存着 v1 v2 v3 的值 内存位置在后
int main()
{
      CUndergraduateStudent stu;
      stu.SetName("jiBa");
      cout << stu.CanBaoYan() << endl;
      return 0;
}