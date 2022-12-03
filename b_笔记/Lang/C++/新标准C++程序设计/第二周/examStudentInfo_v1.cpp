#include <iostream>
// #include <vector>
using namespace std;
class StudentInfo
{
      private:
      string name,id;
      unsigned int age,s1,s2,s3,s4;

      public:
      void Init(string name_,unsigned int age_,string id_,unsigned int s1_,unsigned int s2_,unsigned int s3_,unsigned int s4_)
      {
             name = name_;
             id = id_;
             age = age_;
             s1 = s1_;
             s2 = s2_;
             s3 = s3_;
             s4 = s4_;
      }
      
      void Print()
      {
             string mark = ",";
             cout<< name<< mark << age<< mark << id<< mark << Avarage() << endl;
      }

      private:
      int Avarage()
      {
             return (s1+s2+s3+s4)/4;
      }

};
vector<string> split(const string &str, const string &pattern)
{
    vector<string> res;
    if(str == "")
        return res;
    string strs = str + pattern;
    size_t pos = strs.find(pattern);

    while(pos != strs.npos)
    {
        string temp = strs.substr(0, pos);
        res.push_back(temp);
        strs = strs.substr(pos+1, strs.size());
        pos = strs.find(pattern);
    }

    return res;
}
int main ()
{
      cin.clear();
      StudentInfo sp;
      string name,id;
      unsigned int age,s1,s2,s3,s4;
      cin >> name >> age >> id >> s1 >> s2 >> s3 >> s4;
      sp.Init(name,age,id,s1,s2,s3,s4);
      sp.Print();
      return 0;
}