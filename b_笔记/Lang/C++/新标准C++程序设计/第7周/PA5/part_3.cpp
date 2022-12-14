#include<iostream>
#include<iomanip>
 
using namespace std;
 
int main() {
    int x;
 
    cin >> x;
 
    cout << hex << x << endl;
    cout << setfill('0');
    cout << dec << setw(10) << x << endl;
  
    return 0;
}