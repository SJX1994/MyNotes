#include <iostream>

#include <vector>
#include <algorithm>
#include <numeric>
#include <functional>

using namespace std;
// 函数对象
 // 若一个类重载了运算符 “()”，则该类的对象就成为函数对象
 class CMyAverage { //函数对象类
public:
      // CMyAverage(int a1, int a2, int a3)
      // {
      //       return (double)(a1 + a2+a3) / 3;
      // } // 构造函数无法直接返回值
      double operator() ( int a1, int a2, int a3 ) {
      return (double)(a1 + a2+a3) / 3;
      }
};
// 应用：
int SumSquares( int total, int value) { return total + value * value; }
template <class T>
void PrintInterval(T first, T last)
{ //输出区间[first,last)中的元素
      for( ; first != last; ++ first)
            cout << * first << " ";
      cout << endl;
}
template<class T> 
class SumPowers
{
      private:
            int power; 
      public:
            SumPowers(int p):power(p) { }
            const T operator() ( const T & total, const T & value) 
            { //计算 value的power次方，加到total上
                  T v = value;
                  for( int i = 0;i < power - 1; ++ i)
                        v = v * value; 
                  return total + v; 
            }
};

int main()
{
      // 基本格式：
            CMyAverage average; //函数对象
            cout << average(3,2,3); // average.operator()(3,2,3)
      // 应用：
            const int SIZE = 10;
            int a1[ ] = { 1,2,3,4,5,6,7,8,9,10 };
            vector<int> v(a1,a1+SIZE);
            cout << "\n1) "; 
            PrintInterval(v.begin(),v.end());
            int result = accumulate(v.begin(),v.end(),0,SumSquares);
            // 实例化出：
            // int accumulate(vector<int>::iterator first,vector<int>::iterator last,int init,int ( * op)( int,int)) 
            // {
            //       for ( ; first != last; ++first)
            //             init = op(init, *first);
            //       return init;
            // }
            cout << "2) sum of squares : " << result << endl;
            result = accumulate(v.begin(),v.end(),0,SumPowers<int>(3)); 
            // 实例化出：
            // int accumulate(vector<int>::iterator first,vector<int>::iterator last,int init, SumPowers<int> op) 
            // {
            //       for ( ; first != last; ++first)
            //             init = op(init, *first);
            //       return init;
            // }
            cout << "3) Cubic sums : " << result << endl;
            result = accumulate(v.begin(),v.end(),0,SumPowers<int>(4)); 
            cout << "4) 4th power : " << result;
            // stl 中以下模板可以生成函数对象：
                //  equal_to / greater / less ...      #include <functional>

      return 0;
}
