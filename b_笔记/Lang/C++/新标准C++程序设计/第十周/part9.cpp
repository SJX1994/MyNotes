// #include <iostream>
// #include <set>
// #include <iterator>
// #include <algorithm>
// using namespace std;
// 在此处补充你的代码
class A
{
public: 
    int age;
    char s;
    A(int a): age(a), s('A') {}
    A(int a, char str): age(a), s(str) {}
};

class B : public A
{
public:
    B(int b):A(b, 'B') {}
};

struct Comp
{
    bool operator() (const A* a1, const A* a2) const
    {
        return a1->age < a2->age;
    }
};

void Print(const A* a)
{
    cout << a->s << " " << a->age << endl;
}

// 在此处补充你的代码
// int main()
// {

//         int t;
//         cin >> t;
//         set<A*,Comp> ct;
//         while( t -- ) {
//                 int n;
//                 cin >> n;
//                 ct.clear();
//                 for( int i = 0;i < n; ++i)	{
//                         char c; int k;
//                         cin >> c >> k;

//                         if( c == 'A')
//                                 ct.insert(new A(k));
//                         else
//                                 ct.insert(new B(k));
//                 }
//                 for_each(ct.begin(),ct.end(),Print);
//                 cout << "****" << endl;
//         }
// }