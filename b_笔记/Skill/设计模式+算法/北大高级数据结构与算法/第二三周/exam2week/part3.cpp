#include <iostream>
#include <algorithm>
#include <cmath>

using std::endl;
using std::cout;
using std::cin;
using std::sort;
using std::binary_search;
using std::abs;

// 3. 数组取数

int main(int argc, char *argv[]) {
	int n = 0, t = 0;
	cin >> n >> t;
	t = abs(t);
	int nums[200000];
	for (int i = 0; i < n; i++) {
		cin >> nums[i];
	}

	sort(nums, nums + n);

	int count = 0;
	for (int i = 0; i < n - 1; i++) {
		// 防止重复
		if (i == 0 || (i > 0 && nums[i] != nums[i - 1])) {
			int target = nums[i] + t;
			if (binary_search(nums + i + 1, nums + n, target)) {
				count++;
			}
		}
	}

	cout << count << endl;
	return 0;
}