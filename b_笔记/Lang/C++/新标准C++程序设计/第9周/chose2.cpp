#include <iostream>
#include <iterator>
#include <set>
#include <iostream>
using namespace std;

int main()
{
      int a[] = { 2,5,1,4,3,3,2};

      multiset <int> st(a,a+7);

      cout << * st.lower_bound(3) << "," << *st.upper_bound(3) << endl;
}

