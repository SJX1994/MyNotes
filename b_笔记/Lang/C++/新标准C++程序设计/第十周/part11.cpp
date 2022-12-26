// #include <iostream>
// #include <string>
// #include <map>
// #include <iterator>
// #include <algorithm>
// using namespace std;
// 在此处补充你的代码
template <class T>
struct myGreater {
    bool operator() (const T& x, const T& y) const { return x>y; }
    typedef T first_argument_type;
    typedef T second_argument_type;
    typedef bool result_type;
};

template<typename K, typename V, class Pred = myGreater<K> >
class MyMultimap{
public:
    typedef std::multimap<K, V, Pred>  myType;
    typedef typename std::multimap<K, V>::iterator iterator;
    myType myMap;
    void insert(std::pair<K, V> _p){
        myMap.insert(_p);
    }
    iterator find(K k){
        return myMap.find(k);
    }
    iterator begin(){
        return myMap.begin();
    }
    iterator end(){
        return myMap.end();
    }
    void Set(K k, V v){
        //修改multimap的pair的second值，利用equal_range函数  
        iterator ending = myMap.equal_range(k).second;
        iterator starting = myMap.equal_range(k).first;
        while (starting != ending){
            starting->second = v;
            starting++;
        }
    }//这个函数可以缓缓  
    void clear(){
        myMap.clear();
    }
};

template<typename T1, typename T2>
std::ostream& operator<<(std::ostream& o, typename std::pair<const T1, T2> _p){
    std::cout << "(" << _p.first << "," << _p.second << ")";
    return o;
}
// 在此处补充你的代码
// struct Student
// {
//         string name;
//         int score;
// };
// template <class T>
// void Print(T first,T last) {
//         for(;first!= last; ++ first)
//                 cout << * first << ",";
//         cout << endl;
// }
// int main()
// {

//         Student s[] = { {"Tom",80},{"Jack",70},
//                                         {"Jone",90},{"Tom",70},{"Alice",100} };
//         MyMultimap<string,int> mp;
//         for(int i = 0; i<5; ++ i)
//                 mp.insert(make_pair(s[i].name,s[i].score));
//         Print(mp.begin(),mp.end()); //按姓名从大到小输出

//         mp.Set("Tom",78); //把所有名为"Tom"的学生的成绩都设置为78
//         Print(mp.begin(),mp.end());


//         MyMultimap<int,string,less<int> > mp2;
//         for(int i = 0; i<5; ++ i)
//                 mp2.insert(make_pair(s[i].score,s[i].name));

//         Print(mp2.begin(),mp2.end()); //按成绩从小到大输出
//         mp2.Set(70,"Error");          //把所有成绩为70的学生，名字都改为"Error"
//         Print(mp2.begin(),mp2.end());
//         cout << "******" << endl;

//         mp.clear();
        
//         string name;
//         string cmd;
//         int score;
//         while(cin >> cmd ) {
//                 if( cmd == "A") {
//                         cin >> name >> score;
//                         if(mp.find(name) != mp.end() ) {
//                                 cout << "erroe" << endl;
//                         }
//                         mp.insert(make_pair(name,score));
//                 }
//                 else if(cmd == "Q") {
//                         cin >> name;
//                         MyMultimap<string,int>::iterator p = mp.find(name);
//                         if( p!= mp.end()) {
//                                 cout << p->second << endl;
//                         }
//                         else {
//                                 cout << "Not Found" << endl;
//                         }
//                 }
//         }
//         return 0;
// }