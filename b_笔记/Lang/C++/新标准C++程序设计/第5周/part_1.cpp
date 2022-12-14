// #include <iostream>
// #include <cstring>
// #include <cstdlib>
// using namespace std;
// 在此处补充你的代码

class MyString :public string {
 
 
public:
	MyString(): string() {};
	//1.0继承类继承父类所有的成员变量和成员函数，但不继承构造函数和析构函数 
	//1.1继承类的无参构造函数，会隐式调用父类的无参构造函数
	MyString(const char *s) :string(s) {};//类型转换构造函数
										  //1.2继承类的有参构造函数，如果父类也有有参构造函数，则必须显示调用它 
										  //2.0这里的参数根据reference有两种选择，此处必须用const char*,"xxx"的类型是const char* 
	MyString(const string &s) :string(s) {};//char*是数据类型，而string是类，要弄清楚。
											//1.3继承类的复制构造函数必须要显示的调用父类的复制构造函数，不然就会默认调用父类的无参构造函数 
	MyString(MyString& myStr) :string(myStr) {};//复制构造函数
												//我们发现在派生类的拷贝构造函数中的初始化列表中，
	//基类拷贝构造函数的参数是派生类，但是这样子是没有关系的，编译系统会自动将派生类缩减成基类规模（这是我的个人理解，进行缩减的只是派生类的临时对象，不会对参数进行修改），
	//然后传入给基类的拷贝构造函数，然后在派生类的拷贝构造函数当中再将派生类比基类多出的成员变量进行拷贝。 
	MyString operator()(int i, int j) {
		return this->substr(i, j);
	}
	
	
 
 
};

// ---
// int CompareString( const void * e1, const void * e2) {
//     MyString * s1 = (MyString * ) e1;
//     MyString * s2 = (MyString * ) e2;
//     if( *s1 < *s2 )     return -1;
//     else if( *s1 == *s2 ) return 0;
//     else if( *s1 > *s2 ) return 1;
// }
// int main() {
//     MyString s1("abcd-"),s2,s3("efgh-"),s4(s1);
//     MyString SArray[4] = {"big","me","about","take"};
//     cout << "1. " << s1 << s2 << s3<< s4<< endl;
//     s4 = s3;    s3 = s1 + s3;
//     cout << "2. " << s1 << endl;
//     cout << "3. " << s2 << endl;
//     cout << "4. " << s3 << endl;
//     cout << "5. " << s4 << endl;
//     cout << "6. " << s1[2] << endl;
//     s2 = s1;    s1 = "ijkl-";
//     s1[2] = 'A' ;
//     cout << "7. " << s2 << endl;
//     cout << "8. " << s1 << endl;
//     s1 += "mnop";
//     cout << "9. " << s1 << endl;
//     s4 = "qrst-" + s2;
//     cout << "10. " << s4 << endl;
//     s1 = s2 + s4 + " uvw " + "xyz";
//     cout << "11. " << s1 << endl;
//     qsort(SArray,4,sizeof(MyString), CompareString);
//     for( int i = 0;i < 4;++i )
//         cout << SArray[i] << endl;
//     //输出s1从下标0开始长度为4的子串
//     cout << s1(0,4) << endl;
//     //输出s1从下标为5开始长度为10的子串
//     cout << s1(5,10) << endl;
//     return 0;
// }