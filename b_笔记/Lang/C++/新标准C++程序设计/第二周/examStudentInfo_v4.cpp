#include<iostream>
#include<stdio.h>
#include<cstring>
#include<string>
#include<string.h>

using namespace std;

class student {
	char Sname[20];
	int age = 0;
	char id[20];
	int grade[4];
	public:
		void setName(char* name);
		void setAge(int stAge);
		void setID(char* ID);
		int averageTotal(int a1, int a2, int a3, int a4);
};

void student::setName(char* name) {
	//老版本用strcpy
	// strcpy_s(Sname, name);
	strcpy(Sname, name);
}
void student::setAge(int stAge) {
	stAge = age;
}
void student::setID(char* ID) {
	// strcpy_s(id, ID);
	strcpy(id, ID);
}
int student::averageTotal(int a1, int a2, int a3, int a4) {
	return (a1 + a2 + a3 + a4) / 4;
}

int main() {
	char* p = new char[20];
	char* q = new char[20];
	//char name[20];
	//char id[20];
	int age = 0;
	int a1 = 0, a2 = 0, a3 = 0, a4 = 0;
	char k;
	cin.getline(p, 20, ',');
	//cin.getline(name, 20, ',');
	cin >> age >> k;
	cin.getline(q, 20, ',');
	//cin.getline(id, 20, ',');
	cin >> a1 >> k >> a2 >> k >> a3 >> k >> a4;
	student s;
	s.setName(p);
	//s.setName(name);
	s.setAge(age);
	s.setID(q);
	//s.setID(id);
	s.averageTotal(a1, a2, a3, a4);
	cout << p << ',' << age << ',' << q << ',' << s.averageTotal(a1, a2, a3, a4);
	//cout << name << ',' << age << ',' << id << ',' << s.averageTotal(a1, a2, a3, a4);
	delete[] p;
	delete[] q;
	return 0;
}