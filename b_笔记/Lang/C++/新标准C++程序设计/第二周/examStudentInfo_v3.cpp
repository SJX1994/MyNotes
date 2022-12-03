
#include <iostream>

#include <string>     // std::string, std::stoul, std::getline

#include <cstring>

#include <stdio.h>

#include <stdlib.h>

#include <string.h>

using namespace std;

class student{

public:

	void getInput();

	void getAverage();

private:

	char name[50],id[50];

	unsigned int age,grade[5]; 

};

void student::getInput(){

	int i = 1;

	char studentInfo[8][50], *parseWord;

	cin>>studentInfo[0];

	
	parseWord = strtok (studentInfo[0],",");

  while (parseWord != NULL)

  {

		strcpy(studentInfo[i],parseWord);

    parseWord = strtok (NULL, ",");

		switch (i) {

			case 1:

			strcpy(name, studentInfo[i]);

			break;

			case 2:

			age = stoul(studentInfo[i]);

			break;

			case 3:

			strcpy(id, studentInfo[i]);

			break;

			case 4:

			grade[1] = stoul(studentInfo[i]);

			break;

			case 5:

			grade[2] = stoul(studentInfo[i]);

			break;

			case 6:

			grade[3] = stoul(studentInfo[i]);

			break;

			case 7:

			grade[4] = stoul(studentInfo[i]);

			break;

		}

		i++;

  } 

}

void student::getAverage(){

	unsigned int averageGrade;

	for(int i = 1; i < 5; i++){

		grade[0] += grade[i];

	}

	averageGrade = grade[0]/4;

	cout<<name<<","<<age<<","<<id<<","<<averageGrade<<endl;

}

int main()

{

		student a;

		a.getInput();

		a.getAverage();

    return 0;

}