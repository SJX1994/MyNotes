#include <iostream>
#include <string> // string 类 是一个模板类, 它的定义如下: typedef basic_string<char> string;
using namespace std;
int main(int argc, char* argv[ ]){
            string s1("Hello");
            cout << s1 << endl;
            string s2(8, 'x');
            cout << s2 << endl;
            string month = "March";
            cout << month << endl;
            string s;
            s='n';
            cout << s << endl;
      return 0;
}