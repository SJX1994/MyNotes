#include <iostream>
#include <vector>
using namespace std;
int main() {
// 基础用法
      int i;
      int a[5] = {1,2,3,4,5};  // 定义数组
      vector<int> v(5); // 定义vector
      cout <<  v.end() - v.begin() << endl; // 迭代器下标相减
for( i = 0; i < v.size(); i ++ ) v[i] = i;
      v.at(4) = 100;
for( i = 0; i < v.size(); i ++ )
      cout << v[i] << "," ;
cout << endl;
vector<int> v2(a,a+5); //构造函数
v2.insert(v2.begin() + 2,13); //在begin()+2位置插入13
for( i = 0; i < v2.size(); i ++ )
      cout << v2.at(i) << "," ;
cout << endl;
// 二维动态数组
      vector<vector<int >> vv(3);
      for(int i=0; i<vv.size(); ++i)
            for(int j=0; j<4; ++j)
                  vv[i].push_back(j);
      for(int i=0; i<vv.size(); ++i){
            for(int j=0; j<vv[i].size(); ++j)
                  cout<<vv[i][j]<<" ";
      }
cout<<endl;

return 0;
}