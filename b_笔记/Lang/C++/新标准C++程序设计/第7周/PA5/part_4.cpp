#include <iostream>
#include <cstring>
 
using namespace std;
 
int n;
string s[30];
string opt;
 
string getstring(string s = "");
 
int getint() {
    string tp;
    cin >> tp;
    if ('0' <= tp[0] && tp[0] <= '9')
        return tp[0] - '0';
    string S = getstring();
    int N = getint();
    if (tp == "find") {
        int p = s[N].find(S);
        if (p != string::npos)
            return p;
        else
            return s[N].length();
    }
    if (tp == "rfind") {
        int p = s[N].rfind(S);
        if (p != string::npos)
            return p;
        else
            return s[N].length();
    }
}
 
string add(string s1, string s2) {
    int x = 0, y = 0;
    for (int i = 0; i < s1.size(); i++) {
        char c = s1[i];
        if (c > '9' || c < '0') return s1 + s2;
        x = x * 10 + c - '0';
    }
    for (int i = 0; i < s2.size(); i++) {
        char c = s2[i];
        if (c > '9' || c < '0') return s1 + s2;
        y = y * 10 + c - '0';
    }
    if (y > 99999 || x > 99999)return s1 + s2;
    x = x + y;
   // char s[20];
    //sprintf(s, "%d", x);
    string ans = "";
    while (x) {
        ans = ans + (char)(x % 10 + '0');
        x = x / 10;
    }
    for (int i = 0; i < ans.size() / 2; i++)
        swap(ans[i], ans[ans.size() - 1 - i]);
    return ans;
}
 
string getstring(string tp) {
    if (tp.size() == 0)
        cin >> tp;
    if (tp == "copy") {
        int N = getint();
        int X = getint();
        int L = getint();
        return s[N].substr(X, L);
    }
    if (tp == "add") {
        string s1 = getstring();
        string s2 = getstring();
        return add(s1, s2);
    }
    if (tp == "insert") {
        string S = getstring();
        int N = getint();
        int X = getint();
        s[N].insert(X, S);
    }
    if (tp == "reset") {
        string S = getstring();
        int N = getint();
        s[N] = S;
        return s[n];
    }
    //if (tp == "")
    return tp;
}
 
bool work() {
    string opt;
    cin >> opt;
    if (opt == "over") return 0;
    if (opt == "printall") {
        for (int i = 1; i <= n; i++)
            cout << s[i] << "\n";
    }
    if (opt == "print") {
        int n = getint();
        cout << s[n] << "\n";
    }
    getstring(opt);
    return 1;
}
 
int main() {
   // string ss("44"), s("1234");
   // cout << add(ss, s);
    cin >> n;
    for (int i = 1; i <= n; i++) {
        cin >> s[i];
    }
    //cout<<"\n\n";
    while (work());
    return 0;
}