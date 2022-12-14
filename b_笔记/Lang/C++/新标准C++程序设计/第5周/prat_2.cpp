// 编程题＃2： 魔兽世界之二：装备
#include <iostream>
#include <cstring>
#include <cstdio>
using namespace std;

int M;      //两大阵营开始都有M个生命元
int r_maker[5] = {2,3,4,1,0};
int b_maker[5] = {3,0,1,2,4};
string weapon[3] = {"sword","bomb","arrow"};
int HP[5];
/*
dragon: 0
ninja : 1
iceman: 2
lion  : 3
wolf  : 4
*/

/*
red :0
blue:1
*/
class CampBase
{
      public:
            string camp_name;
            CampBase(int from);
};
CampBase::CampBase(int from)
{
      switch (from) 
      { 
            case 0:camp_name = "red";break;
            case 1:camp_name = "blue";break;
      };
}
class Warrior:public CampBase {
    private:
        int num;//勇士编号 
        int hp;//勇士生命值 
    public:
        Warrior(int from,int name,int warrior_t,int left_t);
};

Warrior::Warrior(int from,int name,int warrior_t,int left_t):CampBase(from)
{
    //阵营名 
    // string camp_name = CampBase::camp_name ;
//     if(from == 0)
//         camp_name = "red";
//     else 
//         camp_name = "blue";
    //武士名
    string warrior_name;
    switch (name) 
    {
        case 0:warrior_name = "dragon";break;
        case 1:warrior_name = "ninja";break;
        case 2:warrior_name = "iceman";break;
        case 3:warrior_name = "lion";break;
        case 4:warrior_name = "wolf";break;
    }
    //武士编号
    int warrior_num = warrior_t;
    //生命值
    int life = HP[name];
    //各自阵营left
    int left = left_t;
    //输出所有的信息 
    cout<<camp_name<<' '<<warrior_name<<' '<<warrior_num<<' ';
    cout<<"born with strength "<<life<<','<<left<<' '<<warrior_name;
    cout<<" in "<<camp_name<<" headquarter"<<endl;
    //最后设置该warrior的参数 
    num = warrior_num;
    hp = life;
}

class Dragon : public Warrior
{
    private:
        double morale;
        int weapon_num;
        string weapon_name;
    public:
        Dragon(int mark,int num1,int num2,int before_left);
};

Dragon::Dragon(int mark,int num1,int num2,int before_left):Warrior(mark,0,num1,num2)
{
    int left = before_left-HP[0];
    morale = (double)left/HP[0];
    int cur_num = num1;//当前的编号 
    weapon_num = cur_num%3;
    weapon_name = weapon[weapon_num];
    //输出Dragon特有的信息
    cout<<"It has a "<<weapon_name<<','<<"and it's morale is ";
    printf("%.2f\n",morale);
}

class Ninja : public Warrior
{
    private:
        int weapon_num1,weapon_num2;
        string weapon_name1,weapon_name2;
    public:
        Ninja(int mark,int num1,int num2);
};

Ninja::Ninja(int mark,int num1,int num2):Warrior(mark,1,num1,num2)
{
    int cur_n = num1;//当前武士编号 
    weapon_num1 = cur_n%3;
    weapon_num2 = (cur_n+1)%3;
    weapon_name1 = weapon[weapon_num1];
    weapon_name2 = weapon[weapon_num2];
    //输出Ninja特有的信息
    cout<<"It has a "<<weapon_name1<<" and a "<<weapon_name2<<endl; 
}

class Iceman : public Warrior
{
    private:
        int weapon_num;
        string weapon_name;
    public:
        Iceman(int mark,int num1,int num2); 
};

Iceman::Iceman(int mark,int num1,int num2):Warrior(mark,2,num1,num2)
{
    int cur_n = num1;
    weapon_num = cur_n%3;
    weapon_name = weapon[weapon_num];
    //输出Ninja特有的信息
    cout<<"It has a "<<weapon_name<<endl;
}

class Lion : public Warrior
{
    private:
        int loyalty;
    public:
        Lion(int mark,int num1,int num2,int before_left);
};

Lion::Lion(int mark,int num1,int num2,int before_left):Warrior(mark,3,num1,num2)
{
    loyalty = before_left-HP[3];
    //输出Ninja特有的信息
    cout<<"It's loyalty is "<<loyalty<<endl; 
}

class Wolf:public Warrior
{
    public:
        Wolf(int mark,int num1,int num2):Warrior(mark,4,num1,num2){} 
}; 

class Camp {
    private:
        int mark;//阵营标记 
        int M_left[2];//两个阵营剩余的生命元
        int state;
        int num[2];//两个阵营分别现有的武士数量 
        int w_num[5][2];//两个阵营各个武士的现有数量 
    public:
        Camp(int nn,int m);
        ~Camp();
        void change(int name);//make完一个warrior后要改变相应的参数 
        void make(int from,int name);//核心函数，制造新的武士 
        int alive();
        int get(int mark);
}; 

Camp::Camp(int nn,int m)
{
    mark = nn;
    M_left[nn] = m;
    state = 1;
    num[0] = num[1] = 1;
    for(int i = 0;i < 5;i++) 
        for(int j = 0;j < 2;j++) 
            w_num[i][j] = 0; 
}

Camp::~Camp() 
{
    state = 0;
    if(mark == 0) 
        cout<<"red headquarter stops making warriors"<<endl;
    else 
        cout<<"blue headquarter stops making warriors"<<endl;
}

void Camp::make(int from,int name)
{
    int n3,n4;
    n3 = num[mark];//当前阵营现有的武士数量 
    n4 = w_num[name][mark]+1;//当前阵营该武士现有的数量 
    if(name == 0)
        Dragon tmp(from,n3,n4,M_left[from]);
    else if(name == 1)
        Ninja tmp(from,n3,n4);
    else if(name == 2)
        Iceman tmp(from,n3,n4);
    else if(name == 3)
        Lion tmp(from,n3,n4,M_left[from]);
    else 
        Wolf tmp(from,n3,n4);
//    Warrior tmp(from,name,n3,n4); 
    change(name);
}

void Camp::change(int name)
{
    num[mark]++;
    M_left[mark] -= HP[name];
    w_num[name][mark]++;
}

int Camp::get(int mark) {
    return M_left[mark];
}

int Camp::alive()
{
    return state;
}

int main()
{
    int C = 0;//Case:测试样例的数量 
    int T;
    cin>>T;
    while(T--)
    {
        int order[2]={0,0};//两大阵营现在分别造到第几个 
        cin>>M;
        for(int i = 0;i < 5;i++)
            cin>>HP[i];
        cout<<"Case:"<<++C<<endl;
        //创建两个阵营
        Camp* R = new Camp(0,M);
        Camp* B = new Camp(1,M);
        Camp* P;
        //默认进行999秒循环，内部中止 
        for(int T = 0;T <= 999;T++)
        {
            if(!R && !B) break;
            for(int j = 0;j < 2;j++)
            { 
                //确定阵营 
                if(j == 0) P = R;
                else P = B;
                if(P->alive()) {
                    //确定本次要创建新士兵 
                    int flag = 0;
                    int nn;
                    int cur,tmp;
                    int left = P->get(j);
                    int tt = (order[j]+5)%5;
                    if(j == 0) 
                        tmp = r_maker[tt];
                    else 
                        tmp = b_maker[tt];
                    if(left >= HP[tmp]) 
                        cur = tmp;
                    else {
                        int cnt = 1;
                        for(int i = tt+1;cnt <= 4;i++,cnt++) {
                            nn = (i+5)%5;
                            int n;
                            if(j == 0) 
                                n = r_maker[nn];
                            else 
                                n = b_maker[nn];
                            if(left >= HP[n]) {
                                cur = n;
                                flag = 1;
                                break;
                            }
                        }
                        if(cnt == 5) {
                            printf("%03d ",T);
                            delete P;
                            continue;
                        }
                    }
                    //创建这个士兵并做好相应数据的记录
                    printf("%03d ",T);
                    P->make(j,cur);
                    if(flag) order[j] = nn+1;
                    else order[j]++;//当前阵营下一个要创造的武士 
                }
            }
        }
    } 
    return 0;
} 