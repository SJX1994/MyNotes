// #include <iostream>
// #include <cstring>
// #include <cstdlib>
// using namespace std;
// class Complex {
// private:
//     double r,i;
// public:
//     void Print() {
//         cout << r << "+" << i << "i" << endl;
//     }
// 在此处补充你的代码   
    Complex() :r(0), i(0) {}
  Complex& operator=(const char* c){
   int m=0;
   char b[100];
   while (*c!='+'){
    b[m]=*c;
    c++;
    m++;
   }
   b[m]='\n';
   c++;
   r=atoi(b); // char* 转 int
   i=atoi(c); // char* 转 int
   return *this;
  }

// };
// int main() {
//     Complex a;
//     a = "3+4i"; a.Print();
//     a = "5+6i"; a.Print();
//     return 0;
// }