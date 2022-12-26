# include<stdio.h>
# include<string.h>
# include<algorithm>
using namespace std;
struct E
{
   char s[51];
   int cnt;
   bool operator < (const E &b) const
   {
      return cnt<b.cnt;
   }
}dn[101];
 
int main()
{
	int n,m;
	int index=0,i,j,k;
	while(scanf("%d%d",&n,&m)!=EOF)
	{
	    for(k=0;k<m;k++)
		{
			scanf("%s",dn[k].s);
			int len=strlen(dn[k].s),cnt=0;
			for(i=0;i<len;i++)
			{
			   for(j=i+1;j<len;j++)
			   {
			     if(dn[k].s[i]>dn[k].s[j])
					 cnt++;
			   }
			}
            dn[k].cnt=cnt;
		}
		    
		sort(dn,dn+m);
 
		for(i=0;i<m;i++)
			printf("%s\n",dn[i].s);
	}
 
  return 0;
}