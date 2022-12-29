#include<cstdio>
#include<cstring>
#include<iostream>
#include<algorithm>
using namespace std;
const int maxn=51;
struct node
{
    int x;
    int y;
    int val;
    bool operator <(const node& a)const
    {
	return val>a.val;
    }
}a[maxn*maxn];
int n,m,k,cur;
int main()
{
    int T;
    scanf("%d",&T);
    while(T--)
    {
	scanf("%d%d%d",&n,&m,&k);
	cur=0;
	for(int i=0;i<n;i++)
	    for(int j=0;j<m;j++)
	    {
		int ita;
		scanf("%d",&ita);
		if(ita)
		{
		    a[cur].x=i+1;
		    a[cur].y=j+1;
		    a[cur++].val=ita;
		}
	    }
	sort(a,a+cur);
	int ans=0,cou=0;
	if(k<a[0].x*2+1)
	{
	    printf("0\n");
	    continue;
	}
	ans+=a[cou].val;
	k-=a[cou++].x+1;
	while(k>0&&cou<cur)
	{
	    if(k<abs(a[cou].x-a[cou-1].x)+abs(a[cou].y-a[cou-1].y)+1+a[cou].x)
		break;
	    ans+=a[cou].val;
	    k-=abs(a[cou].x-a[cou-1].x)+abs(a[cou].y-a[cou-1].y)+1;
	    cou++;
	}
	printf("%d\n",ans);
    }
    return 0;
}
