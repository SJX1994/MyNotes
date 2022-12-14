// #include <iostream>
// using namespace std;
// class Sample{
// public:
//     int v;
//     Sample(int n):v(n) { }
// 在此处补充你的代码
    Sample (const Sample &c1) //考察复制构造函数
    {v=c1.v*2;}
// };
// int main() {
//     Sample a(5);
//     Sample b = a;
//     cout << b.v;
//     return 0;
// }