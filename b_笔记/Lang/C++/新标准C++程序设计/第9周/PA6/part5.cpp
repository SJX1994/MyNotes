#include <iostream>
#include <cstdio>
#include <map>
#include <algorithm>
using namespace std;
typedef long long int LL;
int main()
{
	map<LL, LL> mp;
	int n;
	cin >> n;
	mp.insert(make_pair(1000000000, 1));
	while (n--)
	{
		LL a, b;
		scanf("%lld%lld", &a, &b);
		mp.insert(make_pair(b, a));
		LL t;
		map<LL, LL>::iterator it;
		map<LL, LL>::iterator it2;
		it = mp.lower_bound(b);
		it2 = mp.upper_bound(b);
		if (it == mp.begin())
		{
			t = it2->second;
		}
		else if (it == mp.end())
		{
			t = it->second;
		}
		else
		{
			it--;
			LL t1, t2;
			t1 = abs(it->first - b);
			t2 = abs(it2->first - b);
			if (t1 > t2) t = it2->second;
			else t = it->second;
		}
		printf("%lld %lld\n", a, t);
	}
}