#include <iostream>
#include <iterator>
#include <list>
#include <vector>
using namespace std;
list<int> & FindList(vector<list<int> > & l, int id) {
    int tmp = l.size();
    if (tmp> 0) {
        vector<list<int> >::iterator i;
        i = l.begin();
        return *(i + id - 1);
    }
};
int main() {
    int n;
    cin>>n;
    vector<list<int> > a;
    for (int i = 0; i < n; ++i) {
        string s;
        cin >> s;
        if (s == "new") {
            int id;
            cin >> id;
            a.push_back(list<int>());
        } else if (s == "add") {
            int id, num;
            cin >> id >> num;
            list<int> &temp = FindList(a, id);
            temp.push_back(num);
            temp.sort();
        } else if (s == "merge") {
            int id1, id2;
            cin >> id1 >> id2;
            list<int> &temp1 = FindList(a, id1);;
            list<int> &temp2 = FindList(a, id2);;
            temp1.merge(temp2);
        } else if (s == "unique") {
            int id;
            cin >> id;
            list<int> &temp = FindList(a, id);
            temp.unique();
        } else if (s == "out") {
            int id;
            cin >> id;
            list<int> &temp = FindList(a, id);
            temp.sort();
            if (temp.size() > 0) {
                list<int>::iterator i;
                for(i=temp.begin(); i != temp.end(); ++i) {
                    cout << *i << " ";
                }
            }
            cout << endl;
        }
    }
    return 0;
}