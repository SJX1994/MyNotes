#include<iostream>
#include<iomanip>
// 考验输入输出api的使用
using namespace std;
 
int main() {
    double x;
     
    cin >> x;
    cout.setf(ios::fixed);
    cout << setprecision(5) << x << endl;
    cout.unsetf(ios::fixed);
    cout.setf(ios::scientific);
    cout.precision(7);
    cout << x << endl;
 
    return 0;
}