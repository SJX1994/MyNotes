// #include <iostream>
// #include <cstring>
// using namespace std;
// 在此处补充你的代码
class Array2 {
private:
    int **p; //定义一个二级指针，指的是*int类型的东西
    int row, column;
public:
    Array2(int row_, int column_):row(row_), column(column_) { // 双 int 构造
        p = new int*[row];  // 左右两边都是int** // 二级指针是 row 个 int* 的数组
        for (int i = 0; i < column; ++i) {
            p[i] = new int[column]; // 建立二维数组 // 级指针的每个元素是 column 个 int 的数组
        }
    }
    Array2(){} // 空构造
    int* operator[](int a) {
        return p[a];    //只要重载第一个[]就可以，返回的是int*的数组指针
    }
    int operator()(int a, int b) {
        return p[a][b]; // 重写()生成完整二维数组
    }
    // Array2(const Array2& a) {   //深拷贝 自己
    //     row = a.row;
    //     column = a.column;
    //     p = new int*[row];
    //     for (int i = 0; i < column; ++i) {
    //         p[i] = new int[column];
    //     }
 
    //     for (int i = 0; i < row; ++i) {
    //         for (int j = 0; j < column; ++j) {
    //             p[i][j] = a.p[i][j];
    //         }
    //     }
    // }
    void operator=(const Array2 &a) {   //深拷贝 重载 = 
        row = a.row;
        column = a.column;
        p = new int*[row];
        for (int i = 0; i < column; ++i) {
            p[i] = new int[column];
        }
 
        for (int i = 0; i < row; ++i) {
            for (int j = 0; j < column; ++j) {
                p[i][j] = a.p[i][j];
            }
        }
    }
};
// 在此处补充你的代码
// int main() {
//     Array2 a(3,4); // 双 int 构造
//     int i,j;
//     for(  i = 0;i < 3; ++i )
//         for(  j = 0; j < 4; j ++ )
//             a[i][j] = i * 4 + j; // 二维数组 构造
//     for(  i = 0;i < 3; ++i ) {
//         for(  j = 0; j < 4; j ++ ) {
//             cout << a(i,j) << ","; // 重写 () 或者 << 操作符
//         }
//         cout << endl;
//     }
//     cout << "next" << endl;
//     Array2 b; /*空构造*/  b = a; // = 操作符 重写
//     for(  i = 0;i < 3; ++i ) {
//         for(  j = 0; j < 4; j ++ ) {
//             cout << b[i][j] << ","; // [] 操作符 重写
//         }
//         cout << endl;
//     }
//     return 0;
// }