#include <iostream>
#include <string.h>
using namespace std;
// 虚函数和多态
      // 目的：
            // 在面向对象的程序设计中使用多态，能够增强程序的可扩充性，即程序需要修改或增加功能的时候，需要改动和增加的代码较少。
      // 虚函数:
            class base {
            public: virtual int get() ;
            };
            int base::get() 
            { cout << "base::get()" << endl; return 0;}
            // virtual 关键字只用在类定义里的函数声明中，写函数体时不用。
      // 多态的表现形式一:
            class CBase { 
            public:
                  virtual void SomeVirtualFunction() { cout << "CBase::SomeVirtualFunction()" << endl; } 
            };
            class CDerived:public CBase {
            public :
                  virtual void SomeVirtualFunction() { cout << "CDerived::SomeVirtualFunction()" << endl; }
            };
      // 多态的表现形式二:
            class CBase2 { 
            public:
                  virtual void SomeVirtualFunction() { cout << "CBase2::SomeVirtualFunction()" << endl; } 
            };
            class CDerived2:public CBase2 {
            public :
                  virtual void SomeVirtualFunction() { cout << "CDerived2::SomeVirtualFunction()" << endl; }
            };

// 练习：
      class A { 
      public : 
            virtual void Print( ) 
            { cout << "A::Print"<<endl ; }
      };
      class B: public A { 
            public : 
            virtual void Print( ) { cout << "B::Print" <<endl; }
      };
      class D: public A { 
            public: 
            virtual void Print( ) { cout << "D::Print" << endl ; }
      };
      class E: public B { 
            virtual void Print( ) { cout << "E::Print" << endl ; }
      };
// 实战1：
      //基类 CCreature：
      class CCreature {
      protected :
            int m_nLifeValue, m_nPower;
      public:
            virtual void Attack( CCreature * pCreature) {}
            virtual void Hurted( int nPower) { }
            virtual void FightBack( CCreature * pCreature) { }
      };
      //派生类 CDragon: 
            class CDragon : public CCreature {
            public:
                  virtual void Attack( CCreature * pCreature);
                  virtual void Hurted( int nPower);
                  virtual void FightBack( CCreature * pCreature);
            };
            void CDragon::Attack(CCreature * p)
            { 
                  // …表现攻击动作的代码 
                  p->Hurted(m_nPower); //多态
                  p->FightBack(this); //多态
                  cout << "CDragon::Attack" << endl;
            }
            void CDragon::Hurted( int nPower) 
            { 
                  // …表现受伤动作的代码
                  m_nLifeValue -= nPower;
                  cout << "CDragon::Hurted" << endl;
            }
            void CDragon::FightBack(CCreature * p
            )
            {
                  // …表现反击动作的代码 
                  p->Hurted(m_nPower/2); //多态

            } 
      //派生类 CTiger: 
            class CTiger : public CCreature {
            public:
                  virtual void Attack( CCreature * pCreature);
                  virtual void Hurted( int nPower);
                  virtual void FightBack( CCreature * pCreature);
            };
            void CTiger::Attack(CCreature * p)
            { 
                  // …表现攻击动作的代码 
                  p->Hurted(m_nPower); //多态
                  p->FightBack(this); //多态
            }
            void CTiger::Hurted( int nPower) 
            { 
                  // …表现受伤动作的代码
                  m_nLifeValue -= nPower;
                  cout << "CTiger::Hurted" << endl;
            }
            void CTiger::FightBack(CCreature * p
            )
            {
                  // …表现反击动作的代码 
                  p->Hurted(m_nPower/2); //多态
                  cout << "CTiger::FightBack" << endl;
            } 
// 实战2：
#include <iostream>
#include <stdlib.h>
#include <math.h>
      // 输入：
           // 第一行是几何形体数目 n
           // 若 c 是 ‘R’，则代表一个矩形，本行后面跟着两个整数，分别是矩形的宽和高；
           // 若 c 是 ‘C’，则代表一个圆，本行后面跟着一个整数代表其半径
           // 若 c 是 ‘T’，则代表一个三角形，本行后面跟着三个整数，代表三条边的长度
            // 3
            // R 3 5
            // C 9
            // T 3 4 5
      // 输出：
            // 形体名称：面积
              // Triangle:6
              // Rectangle:15
              // Circle:254.34
            
      class CShape 
      {
      public:
            virtual double Area() = 0; //纯虚函数
            virtual void PrintInfo() = 0;
      }; 
      // -
      class CRectangle:public CShape 
      {
      public:
            int w,h; 
            virtual double Area();
            virtual void PrintInfo();
      };
      double CRectangle::Area() { 
            return w * h; 
      }
      void CRectangle::PrintInfo() {
            cout << "Rectangle:" << Area() << endl;
      }
      // -
      class CCircle:public CShape {
      public:
            int r; 
            virtual double Area();
            virtual void PrintInfo();
      };
      double CCircle::Area() {
            return 3.14 * r * r ;
      }
      void CCircle::PrintInfo() {
            cout << "Circle:" << Area() << endl;
      }
      // -
      class CTriangle:public CShape {
      public:
            int a,b,c; 
            virtual double Area();
            virtual void PrintInfo();
      };
      double CTriangle::Area() {
            double p = ( a + b + c) / 2.0;
            return sqrt(p * ( p - a)*(p- b)*(p - c));
      }
      void CTriangle::PrintInfo() {
            cout << "Triangle:" << Area() << endl; 
      }
// ---
      CShape * pShapes[100];
      int MyCompare(const void * s1, const void * s2);

int main()
{
      // 虚函数
            base bb;
            bb.get();
      // 多态的表现形式一
            CDerived ODerived;
            CBase OBase;
            CBase * p = & ODerived;
            CBase * p2 = & OBase;
            p2 -> SomeVirtualFunction(); //调用哪个虚函数取决于p指向哪种类型的对象
            // 派生类的指针可以赋给基类指针。
            // 通过基类指针调用基类和派生类中的同名虚函数时:
                  // 若该指针指向一个基类的对象，那么被调用是基类的虚函数；
                  // 若该指针指向一个派生类的对象，那么被调用的是派生类的虚函数。
      // 多态的表现形式二
            CDerived2 ODerived2;
            CBase2 & r = ODerived2;
            r.SomeVirtualFunction(); //调用哪个虚函数取决于r引用哪种类型的对象
            // 通过基类引用调用基类和派生类中的同名虚函数时:
                  // 若该引用引用的是一个基类的对象，那么被调用是基类的虚函数；
                  // 若该引用引用的是一个派生类的对象，那么被调用的是派生类的虚函数。
      // 练习
            A a; B b; E e; D d;
            // - pointer
            A * pa = &a ; B * pb = &b; 
            D * pd = &d ; E * pe = &e; 
            pa -> Print(); // a.Print()被调用，输出：A::Print
            pa = pb; 
            pa -> Print(); //b.Print()被调用，输出：B::Print
            pa = pd;
            pa -> Print(); //d. Print ()被调用,输出：D::Print
            pa = pe;
            pa -> Print(); //e.Print () 被调用,输出：E::Print
            // - reference
            A & ra = a ; 
            ra . Print();
            A & rb = b ;   
            rb . Print();
            A & re = e ;
            re . Print();
            A & rd = d ; 
            rd . Print();
            /* a */
            /* | | */
            /* b d */
            /* |  */
            /* e  */
      // 实战1：
            CDragon Dragon;
            CTiger Tiger;
            Dragon.Attack( & Tiger );
      // 实战2：
            int i; int n;
            CRectangle * pr; CCircle * pc; CTriangle * pt;
            cin >> n;
            for( i = 0;i < n;i ++ ) {
                  char c;
                  cin >> c;
                  switch(c) {
                        case 'R':
                              pr = new CRectangle();
                              cin >> pr->w >> pr->h;
                              pShapes[i] = pr; 
                        break;
                        case 'C':
                              pc = new CCircle();
                              cin >> pc->r;
                              pShapes[i] = pc;
                        break;
                        case 'T':
                              pt = new CTriangle();
                              cin >> pt->a >> pt->b >> pt->c;
                              pShapes[i] = pt; 
                        break;
                  } 
            
            }
            qsort(pShapes,n,sizeof( CShape*),MyCompare);
            for( i = 0;i <n;i ++)
                  pShapes[i]->PrintInfo();
      return 0;
}

int MyCompare(const void * s1, const void * s2)
{
      double a1,a2;
      CShape * * p1 ; // s1,s2 是 void * ，不可写 “* s1”来取得s1指向的内容
      CShape * * p2;
      p1 = ( CShape * * ) s1; //s1,s2指向pShapes数组中的元素，数组元素的类型是CShape *
      p2 = ( CShape * * ) s2; // 故 p1,p2都是指向指针的指针，类型为 CShape ** 
      a1 = (*p1)->Area(); // * p1 的类型是 Cshape * ,是基类指针，故此句为多态
      a2 = (*p2)->Area();
      
 if( a1 < a2 ) 
      return -1;
 else if ( a2 < a1 )
      return 1;
 else
      return 0;
}

