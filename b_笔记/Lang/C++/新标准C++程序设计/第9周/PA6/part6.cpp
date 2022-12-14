#include <iostream>
#include <string>
#include <queue>
#include <cmath>
#include <bitset>
using namespace std;
bitset<3200> tab;
 
int op(int k){
    int num = 0;
    for(int j = 2; j < 3200 && j < k; j++){
        if(tab[j] == 0 && k % j == 0){
            while(k % j == 0)
                k /= j ;
            num ++ ;
            if(k == 1) break ;
        }
    }
    if(k == 1)
        return num;
    else if(num == 0)
        return 0;
    else
        return num + 1;
}

class MyLess{
public:
    bool operator()(pair<int,int> a, pair<int,int> b){
        int tmp1 = a.first;
        int tmp2 = b.first;
        if(tmp1 == tmp2)
            return a.second > b.second;
        else
            return tmp1 > tmp2;
    }
};

class MyGreater{
public:
    bool operator()(pair<int,int> a, pair<int,int> b){
        int tmp1 = a.first;
        int tmp2 = b.first;
        if(tmp1 == tmp2)
            return a.second < b.second;
        else
            return tmp1 < tmp2;
    }
};

int main()
{
    tab[0] = tab[1] = 1;
    for(int i = 2; i < 3200; i++)
            tab[i] = 0;
    for(int i = 2; i < 3200; i++)
        if(!tab[i])
            for(int j = i+i; j < 3200; j += i)
                tab[j] = 1;
    int n;
    cin >> n;
    priority_queue<pair<int,int>, vector<pair<int,int> >, MyGreater> q;
    priority_queue<pair<int,int>, vector<pair<int,int> >, MyLess> p;
    while(n--){
        for(int i = 0; i < 10; ++i){
            int tmp;
            cin >> tmp;
            q.push(make_pair(op(tmp), tmp));
            p.push(make_pair(op(tmp), tmp));
        }
        cout << q.top().second <<' '<< p.top().second << endl;
        q.pop();
        p.pop();
    }
}
