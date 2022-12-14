#include <iostream>
#include <string>
#include <vector>
#include <queue>
#include <cstdlib>//atoi字符串转整数的函数
#include <cstring>//memset函数头文件
#include <cstdio>//sprintf函数头文件
using namespace std;

vector<string> strGiven;//存储被给的字符串
queue<string> strCommand;//存储命令字符串
char* strItoa = new char[101];//存储将整数转换的字符串，并作返回值使用

void strStorage(const char* ch);//将输入的一行数据分为若干个命令参数，并存入队列当中
bool strProcess();//开始处理一行命令，注意开头的命令只可能是“insert”“reset”“print”“printall”和“over”
string commandOp();//递归算法处理一行中的命令组合
string nyCopy(int n, int x, int l);//copy N X L：取出第N个字符串第X个字符开始的长度为L的字符串
string nyAdd(string s1, string s2);//add S1 S2：判断S1，S2是否为0-99999之间的整数，若是则将其转化为整数做加法，若不是，则作字符串加法，返回的值为一字符串
int nyFind(int n, string s);//find S N：在第N个字符串中从左开始找寻S字符串，返回其第一次出现的位置，若没有找到，返回字符串的长度。
int nyRFind(int n, string s);//rfind S N：在第N个字符串中从右开始找寻S字符串，返回其第一次出现的位置，若没有找到，返回字符串的长度。
void nyInsert(int n, int x, string s);//在第N个字符串的第X个字符位置中插入S字符串
void nyReset(int n, string s);//将第N个字符串变为S
void nyPrint(int n);//打印输出第N个字符串
void nyPrintAll();//打印输出所有字符串

int main()
{
	int n;
	cin >> n;

	for (int i = 0; i < n; i++)
	{
		string str;
		cin >> str;
		strGiven.push_back(str);
	}
	cin.get();

	char* ch = new char[501];
	while (1)
	{
		cin.getline(ch, 500, '\n');
		strStorage(ch);
		int t = strProcess();
		if (t == false)
		{
			break;
		}
	}
	delete ch;
	delete strItoa;
	return 0;
}
/*
将输入的一行数据分为若干个命令参数，并存入队列当中
*/
void strStorage(const char* ch)
{
	string str;
	for (int i = 0; ch[i] != '\0'; i++)
	{
		if (ch[i] != ' ')
		{
			str.push_back(ch[i]);
		}
		else
		{
			strCommand.push(str);
			str.clear();
		}
	}
	//记录最后一个字符串
	strCommand.push(str);
}
/*
开始处理一行命令，注意开头的命令只可能是“insert”“reset”“print”“printall”和“over”
遇到"over"时返回false，程序结束
*/
bool strProcess()
{
	string str = strCommand.front();
	strCommand.pop();
	if (str == "insert")
	{
		string s = commandOp();
		int n = atoi(commandOp().c_str());
		int x = atoi(commandOp().c_str());
		nyInsert(n, x, s);
	}
	else if (str == "reset")
	{
		string s = commandOp();
		int n = atoi(commandOp().c_str());
		nyReset(n, s);
	}
	else if (str == "print")
	{
		int n = atoi(commandOp().c_str());
		nyPrint(n);
	}
	else if (str == "printall")
	{
		nyPrintAll();
	}
	else if (str == "over")
	{
		return false;
	}
	return true;
}
/*
递归算法处理一行中的命令组合
*/
string commandOp()
{
	string str = strCommand.front();
	strCommand.pop();
	if (str == "copy")
	{
		int n = atoi(commandOp().c_str());
		int x = atoi(commandOp().c_str());
		int l = atoi(commandOp().c_str());
		return nyCopy(n, x, l);
	}
	else if (str == "add")
	{
		string s1 = commandOp();
		string s2 = commandOp();
		return nyAdd(s1, s2);
	}
	else if (str == "find")
	{
		string s = commandOp();
		int n = atoi(commandOp().c_str());
		sprintf(strItoa, "%d", nyFind(n, s));
		return strItoa;
	}
	else if (str == "rfind")
	{
		string s = commandOp();
		int n = atoi(commandOp().c_str());
		sprintf(strItoa, "%d", nyRFind(n, s));
		return strItoa;
	}
	else//参数
	{
		return str;
	}

}
/*
copy函数
*/
string nyCopy(int n, int x, int l)
{
	string str = strGiven.at(n - 1);
	return str.substr(x, l);
}
/*
add函数
*/
string nyAdd(string s1, string s2)
{
	//判断字符串里是否全为数字
	//因为83s经过atoi后会变为83
	for (int i = 0; i < s1.size(); i++)
	{
		if (s1.at(i) < '0' || s1.at(i) > '9')
		{
			return s1 + s2;
		}
	}
	for (int i = 0; i < s2.size(); i++)
	{
		if (s2.at(i) < '0' || s2.at(i) > '9')
		{
			return s1 + s2;
		}
	}
	//若全为数字，判断是否在0和99999之间
	long a = atoi(s1.c_str());
	long b = atoi(s2.c_str());
	if (a >= 0 && a <= 99999 && b >= 0 && b <= 99999)
	{
		long c = a + b;
		sprintf(strItoa, "%d", c);
		return strItoa;
	}
	else
	{
		return s1 + s2;
	}
}
/*
find函数
*/
int nyFind(int n, string s)
{
	int pos = strGiven.at(n - 1).find(s);
	if (pos == string::npos)
	{
		return s.size();
	}
	else
	{
		return pos;
	}
}
/*
rfind函数
*/
int nyRFind(int n, string s)
{
	int pos = strGiven.at(n - 1).rfind(s);
	if (pos == string::npos)
	{
		return s.size();
	}
	else
	{
		return pos;
	}
}
/*
insert函数
*/
void nyInsert(int n, int x, string s)
{
	strGiven.at(n - 1).insert(x, s);
}
/*
reset函数
*/
void nyReset(int n, string s)
{
	strGiven.at(n - 1).swap(s);
}
/*
print函数
*/
void nyPrint(int n)
{
	cout << strGiven.at(n - 1) << endl;
}
/*
printall函数
*/
void nyPrintAll()
{
	for (unsigned int i = 0; i < strGiven.size(); i++)
	{
		cout << strGiven.at(i) << endl;
	}

}