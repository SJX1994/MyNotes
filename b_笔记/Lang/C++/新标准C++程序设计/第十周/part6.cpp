// #include <cstring>
// #include <cstdlib>
// #include <string>
// #include <iostream>
// using namespace std;
// 在此处补充你的代码
class MyString :public string{

public:
    MyString() :string(){};                       //构造函数，针对 s2 初始化的默认
    MyString(const char* str) :string(str){};     //构造函数，char*是数据类型，而string是类 http://www.cnblogs.com/GODYCA/archive/2013/01/08/2851923.html
    MyString(MyString &str) :string(str){};       //复制构造函数
    MyString(const string &str) :string(str){};   //类型转换构造函数，把string类转换为MyString类

    MyString operator()(int startPos, int strLengh){
        return this->substr(startPos, strLengh);
    }

    /* 此处不用重载，在派生类对象中，包含着基类对象
    friend ostream& operator<<(ostream& os, string& str){
    os << str;
    return os;
    }
    */
};

// 在此处补充你的代码
// int CompareString( const void * e1, const void * e2)
// {
//         MyString * s1 = (MyString * ) e1;
//         MyString * s2 = (MyString * ) e2;
//         if( * s1 < *s2 )
//                  return -1;
//         else if( *s1 == *s2)
//                  return 0;
//         else if( *s1 > *s2 )
//                  return 1;
// }
// int main()
// {
//         MyString s1("abcd-"),s2,s3("efgh-"),s4(s1);
//         MyString SArray[4] = {"big","me","about","take"};
//         cout << "1. " << s1 << s2 << s3<< s4<< endl;
//         s4 = s3;
//         s3 = s1 + s3;
//         cout << "2. " << s1 << endl;
//         cout << "3. " << s2 << endl;
//         cout << "4. " << s3 << endl;
//         cout << "5. " << s4 << endl;
//         cout << "6. " << s1[2] << endl;
//         s2 = s1;
//         s1 = "ijkl-";
//         s1[2] = 'A' ;
//         cout << "7. " << s2 << endl;
//         cout << "8. " << s1 << endl;
//         s1 += "mnop";
//         cout << "9. " << s1 << endl;
//         s4 = "qrst-" + s2;
//         cout << "10. " << s4 << endl;
//         s1 = s2 + s4 + " uvw " + "xyz";
//         cout << "11. " << s1 << endl;
//         qsort(SArray,4,sizeof(MyString),CompareString);
//         for( int i = 0;i < 4;i ++ )
//         cout << SArray[i] << endl;
//         //s1的从下标0开始长度为4的子串
//         cout << s1(0,4) << endl;
//         //s1的从下标5开始长度为10的子串
//         cout << s1(5,10) << endl;
//         return 0;
// }