// #include <iostream>
// using namespace std;
// 在此处补充你的代码
template<class T3>
class CArray3D { // 嵌套类
public:
    template<class T2>
    class CArray2D {
    public:
        template<class T1>
        class CArray1D {
            private:
                T1* ptr1;
            public:
                CArray1D():ptr1(NULL){}
                void set(int l) {
                    ptr1 = new T1[l];
                }
                T1& operator[](int index) {
                    return ptr1[index];
                }
                ~CArray1D() {
                    if (ptr1) delete[] ptr1;
                }
        };
        CArray2D():ptr2(NULL){}
        void set(int x, int y) {
            ptr2 = new CArray1D<T2>[x];
            for (int i = 0; i < x; ++i)
                ptr2[i].set(y);
        }
        CArray1D<T2>& operator[](int index) {
            return ptr2[index];
        }
        ~CArray2D() {
            if (ptr2) delete[] ptr2;
        }
        private:
            CArray1D<T2>* ptr2;
    };
    CArray3D():ptr3(NULL){}
    CArray3D(int n, int x, int y) {
        ptr3 = new CArray2D<T3>[n];
        for (int i = 0; i < n; ++i) {
            ptr3[i].set(x, y);
        }
    }
        CArray2D<T3>& operator[](int index) {
            return ptr3[index];
        }
        ~CArray3D() {
            if (ptr3) delete[] ptr3;
        }
        private:
            CArray2D<T3>* ptr3;
};
// 在此处补充你的代码
// int main()
// {
//     CArray3D<int> a(3,4,5);
//     int No = 0;
//     for( int i = 0; i < 3; ++ i )
//         for( int j = 0; j < 4; ++j )
//             for( int k = 0; k < 5; ++k )
//                 a[i][j][k] = No ++;
//     for( int i = 0; i < 3; ++ i )
//         for( int j = 0; j < 4; ++j )
//             for( int k = 0; k < 5; ++k )
//                 cout << a[i][j][k] << ",";
//     return 0;
// }