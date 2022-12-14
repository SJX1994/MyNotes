// 封闭类
  // 成员对象：一个类的成员变量是另一个类的对象
  // 封闭类 (Enclosing)：包含 成员对象 的类
#include <iostream>
using namespace std;
class CTyre { //轮胎类
 private:
  int radius; //半径 
  int width; //宽度
 public:
  CTyre(int r, int w):radius(r), width(w) {
    cout << r << w <<"CTyre contructor" << endl;
  }
  ~CTyre() { cout << "CTyre destructor" << endl; }
};
class CEngine { //引擎类
  public:
    CEngine() { cout << "CEngine contructor" << endl; }
    ~CEngine() { cout << "CEngine destructor" << endl; }
};
// 封闭类（汽车）：
class CCar { 
private:
  int price; //价格
  CTyre tyre;
  CEngine engine;
public:
 CCar(int p, int tr, int tw);
 ~CCar() { cout << "CCar destructor" << endl; }
};
CCar::CCar(int p, int tr, int w):price(p), tyre(tr, w){
    cout << "CCar contructor" << endl; 
};

int main(){
  CCar car(20000,17,225); // 封闭类 必须构造 不然 成员对象 tyre 无法构造
  // 生成顺序：执行所有 成员对象 CTyre CEngine 的构造函数 -> 执行 封闭类 CCar 的构造函数
  // 消亡顺序：执行 封闭类 CCar 的析构函数，执行 成员对象 CTyre CEngine 的析构函数
  return 0;
}
