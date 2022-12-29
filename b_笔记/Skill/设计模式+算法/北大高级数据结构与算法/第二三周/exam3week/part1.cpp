#include<iostream>
#include<algorithm>
using namespace std;

struct Cow {
	int a, b, id;
};
Cow cow[100]; 

//判断边权重大小
bool cmp_a(Cow x, Cow y) {
	return x.a > y.a;
}
bool cmp_b(Cow x, Cow y) {
	return x.b > y.b;
}

int main(){
	int n, k;
	cin >> n >>k;
	for(int i=0;i<n;i++){
		cin >> cow[i].a >>cow[i].b;
		cow[i].id=i+1;
	}
	sort(cow,cow+n,cmp_a);	//以a进行比较全部 
	sort(cow,cow+k,cmp_b);	//以b进行比较前k个 
	cout << cow[0].id;
      return 0;
}
