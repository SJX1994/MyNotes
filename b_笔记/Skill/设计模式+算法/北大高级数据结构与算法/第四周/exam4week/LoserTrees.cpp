# include <iostream>
 using namespace std;
 
 struct LoserTree{
     int n;      // elements to be compared: n
     int LowExt; // lowest external elements number: LowExt
     int s;      // lowest internal level elements: s
     int offset; // total nodes numbers except lowest level
     int * tree; // loser tree
     int * ele;  // elements to be compared
     int winner(int c1, int c2);         // return winner index
     int loser(int c1, int c2);          // return loser index
     void init(int _n, int * _ele);      // initialize loser tree
     void play(int p, int c1, int c2);   // compare ele[c1] & [c2], their parent is p
     void build();   // rebuile loser tree
     void print();   // print tree
     void dele();    // delete tree
 };
 
 int LoserTree::winner(int c1, int c2){
     if(ele[c1] < ele[c2]) return c1;
     else return c2;
 }
 
 int LoserTree::loser(int c1, int c2){
     if(ele[c1] < ele[c2]) return c2;
     else return c1;
 }
 
 void LoserTree::init(int _n, int * _ele){
     ele = _ele;
     n = _n;
     tree = new int [n];
     for(s = 1; s*2 < n; s *= 2);
     offset = 2 * s - 1;
     LowExt = 2 * (n - s);
 }
 
 void LoserTree::play(int p, int c1, int c2){
     int lastwin = winner(c1, c2), tempwin;
     tree[p] = loser(c1, c2);
     while(p > 1 && p % 2 == 1){ // right subtree goes up
         tempwin = winner(lastwin, tree[p/2]);
         tree[p/2] = loser(lastwin, tree[p/2]);
         lastwin = tempwin;
         p /= 2;
     }
     tree[p/2] = lastwin;    // p = 1, or tree[p] is a left sibling
 }
 
 void LoserTree::build(){
     int i;
     // lowest level elements play
     for(i = 2; i <= LowExt; i += 2)
         play((offset+i)/2, i-1, i);
     // one inner node vs one element
     if(n % 2){
         play(n/2, tree[n-1], LowExt+1);
         i = LowExt + 3;
     }
     else i = LowExt + 2;
     // the last second level elements play
     for( ; i <= n; i += 2)
         play((i-LowExt+n-1)/2, i-1, i);
 }
 
 void LoserTree::print(){
     for(int i = 0; i < n; ++i)
         cout << ele[tree[i]] << ' ';
     cout << endl;
 }
 
 void LoserTree::dele(){
     delete [] tree;
 }
 
 int main(){
     int n, m, * ele, index, val;
     LoserTree MyTree;
     cin >> n >> m;
     ele = new int [n+2];
     for(int i=1; i<=n; ++i)
         cin >> ele[i];
     MyTree.init(n, ele);
     MyTree.build();
     MyTree.print();
     for(int i=0; i<m; ++i){
         cin >> index >> val;
         ele[index+1] = val; // change value if ele[index]
         MyTree.build();     // rebuild
         MyTree.print();
     }
     MyTree.dele();
     delete [] ele;  // don't dorget!!!
     return 0;
 }

