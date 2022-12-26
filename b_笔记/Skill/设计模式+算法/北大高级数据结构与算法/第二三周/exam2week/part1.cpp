# include <cstdio>
 # include <algorithm>
 # include <cmath>
 using namespace std;
 
 // node
 typedef struct{
     int x, y, z;
 }NODE;
 // edge
 typedef struct{
     int n1, n2, l;
 }EDGE;
 
 int N, i, j, k = 0;
 NODE * n;
 EDGE * e;
 
 // compare if e1 longer than e2
 bool cmp(EDGE e1, EDGE e2){
     return e1.l > e2.l;
 }
 // calculate edge's length between node n1 & n2
 int Len(int n1, int n2){
     int x = n[n1].x - n[n2].x,
         y = n[n1].y - n[n2].y,
         z = n[n1].z - n[n2].z;
     return x*x + y*y + z*z;
 }
 // bubble sort
 void MySort(){
     EDGE t;
     for(i=0; i<k-1; ++i){
         for(j=k-1; j>i; --j){
             // edge[j] > edge[j-1]
             if(cmp(e[j], e[j-1])){
                 t = e[j];
                 e[j] = e[j-1];
                 e[j-1] = t;
             }
         }
     }
 }
 // print result
 void MyPrint(int m){
     int n1 = e[m].n1, n2 = e[m].n2, l = e[m].l;
     printf("(%d,%d,%d)-(%d,%d,%d)=%.2lf\n",
            n[n1].x, n[n1].y, n[n1].z,
            n[n2].x, n[n2].y, n[n2].z, sqrt((double)l));
 }
 
 int main(){
     // initialize
     scanf("%d", &N);
     n = new NODE[N+2];
     e = new EDGE[N*(N+1)/2+2];
     for(i=0; i<N; ++i)
         scanf("%d %d %d", &n[i].x, &n[i].y, & n[i].z);
     // calculate edges' length
     for(i=0; i<N-1; ++i)
         for(j=i+1; j<N; ++j){
             e[k].n1 = i;
             e[k].n2 = j;
             e[k].l = Len(i, j);
             k++;
         }
     // bubble sort
     MySort();
     // print result
     for(i=0; i<k; ++i)
         MyPrint(i);
     // delete
     delete [] n;
     delete [] e;
     return 0;
 }
 

