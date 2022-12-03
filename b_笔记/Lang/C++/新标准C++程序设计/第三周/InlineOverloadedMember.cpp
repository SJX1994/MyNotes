// 内联成员函数(Inline Member Function)
class B{
      inline void func1();
      void func2() {};
};
void B::func1() { }

// 重载成员函数(Overloaded Member Function)
class Location {
      private :
            int x, y;
      public:
            // 带缺省参数
            void init( int x=0 , int y = 0 );
            // 重载成员函数
            
            void valueX( int val ) { x = val ; }
            // void valueX( int val = 0 ) { x = val; } // 缺省参数 但与下面具有 二义性
            int valueX() { return x; }
}
void Location::init( int X, int Y){
      x = X;
      y = Y;
}


int main() {
      Location A;
      A.init(5); // 缺省参数调用
      A.valueX(5);// 重载成员函数调用
      cout << A.valueX();
      return 0;
}