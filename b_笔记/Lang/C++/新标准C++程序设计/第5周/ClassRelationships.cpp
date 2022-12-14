#include <iostream>
#include <string.h>
using namespace std;
// 继承关系和复合关系
 // 继承：“是”关系。
      // 基类 A，B是基类A的派生类。
      // 逻辑上要求：“一个B对象也是一个A对象”。
      // 例如： 男人 继承 人， 女人 继承 人，男人 女人 都是 人。
 // 复合：“有”关系。
      // 类C中“有”成员变量k，k是类D的对象，则C和D是复合关系
      // 一般逻辑上要求：“D对象是C对象的固有属性或组成部分”。
      // 例如：几何程序中 需要一个“点类” 也需要一个 “圆类” ，圆需要“点”

// 例如：小区养狗管理程序，需要写一个“业主”类，还需要写一个“狗”类。而狗是有 “主人” 的，主人当然是业主(假定狗只有一个主人，但一个业主可以有最多10条狗）
class CDog;
class CMaster {
      CDog dogs[10];
};
class CDog {
      CMaster m;
}; 
// 另一种写法：
// 为“狗”类设一个“业主”类的成员对象；
// 为“业主”类设一个“狗”类的对象指针数组。
class CDog;
class CMaster {
 CDog * dogs[10];
};
class CDog {
 CMaster m;
};
// 凑合的写法：
// 为“狗”类设一个“业主”类的对象指针；
// 为“业主”类设一个“狗”类的对象数组
class CMaster; //CMaster必须提前声明，不能先
 //写CMaster类后写Cdog类
class CDog {
      CMaster * pm;
};
class CMaster {
      CDog dogs[10];
};

// 正确的写法：
// 为“狗”类设一个“业主”类的对象指针；
// 为“业主”类设一个“狗”类的对象指针数组

class CMaster; //CMaster必须提前声明，不能先
//写CMaster类后写Cdog类
class CDog {
 CMaster * pm;
};
class CMaster {
 CDog * dogs[10];
};