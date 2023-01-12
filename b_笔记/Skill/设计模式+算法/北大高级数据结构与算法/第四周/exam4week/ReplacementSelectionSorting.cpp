# include <iostream>
using namespace std;
 
int m, n, * heap, * num;
 
void Init(){
     cin >> m >> n;
     heap = new int [n+1];
     num = new int [m+1];
     for(int i=0; i<m; ++i)
         cin >> num[i];
     for(int i=0; i<n; ++i)
         cin >> heap[i];
 }
 
 void Exch(int i, int j){
     int tmp = heap[i];
     heap[i] = heap[j];
     heap[j] = tmp;
 }
 
 void ShiftDown(){
     int cur = 0, s1 = 1, s2 = 2;
     while(s2 < n){
         if(heap[cur] <= heap[s1] && heap[cur] <= heap[s2])
             return; //heap is min heap already
         if(heap[s1] > heap[s2]){
             Exch(cur, s2);
             cur = s2;
         }
         else{
             Exch(cur, s1);
             cur = s1;
         }
         s1 = cur * 2 + 1;
         s2 = s1 + 1;
     }
     if(s1 < n && heap[s1] < heap[cur])
         Exch(cur, s1);
     return;
 }
 
 void Insert(){
     //attention! n should > 0
     for(int i=0; i<m && n; ++i){
         cout << heap[0] << ' ';
         if(num[i] >= heap[0])
             heap[0] = num[i];
         else
             heap[0] = heap[--n];
         ShiftDown();
     }
 }
 
 void Dele(){
     delete [] heap;
     delete [] num;
 }
 
 int main(){
     Init();
     Insert();
     Dele();
     return 0;
 }
