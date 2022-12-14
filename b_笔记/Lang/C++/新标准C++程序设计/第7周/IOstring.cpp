//下面的程序从键盘输入几个学生的姓名的成绩,
//并以二进制, 文件形式存起来
#include <iostream>
#include <fstream>
#include <cstring>
using namespace std;
class CStudent {
 public:
      char szName[20];
      int nScore;
};
int main()
{
      CStudent s;

      // 写
      ofstream OutFile( ".\\students.dat", ios::out|ios::binary );
      while( cin >> s.szName >> s.nScore ) {
      if( stricmp(s.szName,"exit" ) == 0) //名字为exit则结束
            break;
            OutFile.write( (char *) & s, sizeof(s) );
      }
            // // 写入一个学生的信息 
            // s.szName[0] = 'J';
            // s.nScore = 100;
            // OutFile.write( (char *) & s, sizeof(s) );
            // // -- 
            OutFile.close();
      
      
      // 读
      ifstream inFile(".\\students.dat", ios::in | ios::binary );
      if(!inFile) {
            cout << "error" <<endl;
      return 0;
      }
      while( inFile.read( (char* ) & s, sizeof(s) ) ) {
      int nReadedBytes = inFile.gcount(); //看刚才读了多少字节
      cout << s.szName << " " << s.nScore << endl;
      }
      inFile.close();

      //改
      fstream iofile( ".\\students.dat", ios::in|ios::out|ios::binary);
      if(!iofile) {
            cout << "error" ;
      return 0;
      }

 return 0;
}