// #include <iostream>
// using namespace std;
// class A {
//     private:
//         int nVal;
//     public:
//         void Fun()
//         { cout << "A::Fun" << endl; }
//         void Do()
//         { cout << "A::Do" << endl; }
// };
// class B:public A {
//     public:
//         virtual void Do()
//         { cout << "B::Do" << endl; }
// };
// class C:public B {
//     public:
//     void Do( )
//     { cout << "C::Do" <<endl; }
//     void Fun()
//     { cout << "C::Fun" << endl; }
// };
// void Call(
// 在此处补充你的代码
    B & p  // 考察虚函数的引用多态性
// 在此处补充你的代码
//          )  {
//     p.Fun();  p.Do();
// }
// int main()  {
//     C c;    Call(c);
//     return 0;
// }