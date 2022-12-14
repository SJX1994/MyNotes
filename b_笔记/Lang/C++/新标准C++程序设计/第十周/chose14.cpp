
#include <algorithm>
#include <iostream>
#include <list>
using namespace std;
int main()
{
      list <int> v; list <int>::const_iterator ii; 
     for( ii = v.begin(); ii != v.end ();ii ++ ) 

	  cout << * ii;
      return 0;
}