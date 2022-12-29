// #include <iostream>
// using namespace std;
// 在此处补充你的代码
class CType
{
private:
    int value;
public:
    CType():value(0) {};
    void setvalue(int n)
    {
        value = n;
    }
    CType& operator ++ (int)
    {
//      static CType tmp = *this;
//      static CType tmp = CType();//必须使用static变量,否则返回时内存就被释放了
        static CType tmp;//必须使用static变量,否则返回时内存就被释放了
        tmp.value = value;
        value *= value;
        return tmp;
    }
    friend ostream& operator << (ostream& o, CType& cType)//此处必须为友元函数
    {
        o << cType.value;
        return o;
    }
};

// 在此处补充你的代码
// int main(int argc, char* argv[]) {
//         CType obj;
//         int n;
//         cin>>n;
//         while ( n ) {
//                 obj.setvalue(n);
//                 cout<<obj++<<" "<<obj<<endl;
//                 cin>>n;
//         }
//         return 0;
// }