#include <iostream>
#include <cstring>
using namespace std;
 
const int MAXLEN = 201;
 
int Substract(int *p1,int *p2,int len1,int len2) // 减法
{
    int i;
    if(len1<len2)
        return -1; // 为负符号
    if(len1==len2) // 长度相等
    {
        for(i=len1-1; i>=0;--i)
        {
            if(p1[i]<p2[i])    //p1<p2
                return -1;     // 为负符号
            else if(p1[i]>p2[i]) //p1>p2
                break;
        }
    }
    for(i=0;i<len1;++i)//要求调用本函数确保当i>=len2时,p2[i]＝0
    {
        p1[i]-=p2[i];
        if(p1[i]<0)
        {
            p1[i]+=10;
            --p1[i+1];
        }
    }
    for(i=len1-1;i>=0;--i)
        if(p1[i])    //找到最高位第一个不为0
            return i+1;
    return 0;   //全部为0，说明两者相等
}
 
class Integer
{
private:
    int is_neg;
    int len;
    int s[MAXLEN]; // 所有输入的int值
    char str[MAXLEN]; // 所有输入的string值
    
public:
    Integer(const char *string = "") // 在构造中存入所有输入的int值
    {
        memset(s,0,MAXLEN*sizeof(int)); // 初始化
        memset(str,0,MAXLEN*sizeof(char)); // 初始化
        strcpy(str,string); // 复制字符串
        is_neg = 0;
        len = strlen(str); // 计算字符串长度
        for (int i=0; i<len; i++)
            s[i] = int(str[len-1-i]) - 48; //s[i]低位在左，高位在右。
    }
//     Integer & operator = (const Integer & oth)
//     {
//         if(this == &oth) return *this;
//         memset(s,0,MAXLEN*sizeof(int));
//         memset(str,0,MAXLEN*sizeof(char));
//         is_neg = oth.is_neg;
//         len = oth.len;
//         for (int i = 0; i<oth.len; i++)
//             s[i] = oth.s[i];
//         strcpy(str,oth.str);
//         return *this;
//     }
    
//     bool operator == (const Integer & oth)
//     {
//         if (this == & oth) return true;
//         bool ret = true;
//         if(len != oth.len || is_neg != oth.is_neg)
//             ret = false;
//         if(strcmp(str, oth.str)) ret = false;
//         for (int i = 0; i<oth.len; i++)
//         {
//             if (s[i] != oth.s[i]) ret = false;
//         }
//         return ret;
//     }
    
//     bool operator != (const Integer & oth)
//     {
//         return !(*this == oth);
//     }
    
    Integer operator+(const Integer & oth)
    {
        Integer c;
        int length = len >= oth.len ? len : oth.len;
        length += 1;   //防止有进位
        c.len = length;
        for(int i=0; i<c.len; i++)
        {
            c.s[i] += s[i] + oth.s[i]; //注意是“+=”而不是“=”
            c.s[i+1] += c.s[i] / 10;
            c.s[i] = c.s[i] % 10;
        }
        while ((c.len > 1) && (c.s[c.len-1] == 0)) c.len--;  //去除后面多余的0
        /*int k=0;
         for (int i = c.len-1; i>=0; i--)
         {
         c.str[k++] = (char)(c.s[i]+48);
         }*/
        return c;
    }
    
    Integer operator - (const Integer & oth)
    {
        Integer c;    //用来存放结果
        int flag = 0;
        
        if(len>oth.len) flag = 1;
        else if(len < oth.len) flag = -1;
        else flag = strcmp(str, oth.str);   //找出两个数中的大数和小数
        
        if(flag >= 0) //确保 大数 - 小数
        {
            c.len = len;
            int borrow = 0;
            for (int i=0; i<c.len; i++)
            {
                c.s[i] += s[i] - oth.s[i];
                if (borrow) c.s[i] -= 1;
                if(c.s[i]<0)
                {
                    c.s[i] += 10;
                    borrow = 1;
                }
                else
                    borrow = 0;
            }
            while ((c.len>1) && (c.s[c.len-1] == 0)) c.len--;
        }
        else // 确保 大数 - 小数
        {
            c.is_neg = 1;
            c.len = oth.len;
            int borrow = 0;
            for (int i = 0; i<c.len; i++)
            {
                c.s[i] += oth.s[i] - s[i];
                if (borrow) c.s[i] -= 1;
                if(c.s[i]<0)
                {
                    c.s[i] += 10;
                    borrow = 1;
                }
                else
                    borrow = 0;
            }
            while ((c.len>1) && (c.s[c.len-1] == 0)) c.len--;
        }
        
        /*int k=0;
         for (int i=c.len-1; i>=0; i--)
         {
         c.str[k++] = (char)(c.s[i]+48);
         }*/
        return c;
    }
    
    Integer operator*(const Integer & oth)
    {
        Integer c;
        c.len = len + oth.len + 1;
        for (int i = 0; i< len; i++)
        {
            for (int j=0; j<oth.len; j++)
            {
                c.s[i+j] += s[i] * oth.s[j];
                c.s[i+j+1] += c.s[i+j] / 10;
                c.s[i+j] = c.s[i+j] % 10;
            }
        }
        //cout<<c.len<<endl;
        while ((c.len >1) && (c.s[c.len-1] == 0)) c.len--;
        //cout<<c.len<<endl;
        /*int k=0;
         for (int i=c.len-1; i>=0; i--)
         {
         c.str[k++] = (char)(c.s[i]+48);
         }*/
        return c;
    }
    
    Integer operator/ (Integer & oth)
    {
        Integer c;
        c.len = MAXLEN;
        int i, temp;
        if(len<oth.len){
            for (int i=0; i<MAXLEN; i++)
                c.s[i]=0;
            return c;
        }
        int nTimes = len - oth.len;
        if(nTimes>0)
        {
            for(i=len-1;i>=nTimes;--i)
                oth.s[i] = oth.s[i-nTimes];//朝高位移动
            for(;i>=0;--i)
                oth.s[i]=0;    //低位补零
            oth.len=len;
        }
        for(i=0;i<=nTimes;++i)
        {
            // 判断减法正负
            while((temp=Substract(s,oth.s+i,len,oth.len-i))>=0)  //注意while循环中的括号不能少
            {
                len=temp;
                ++c.s[nTimes-i];//每成功减一次，则将商的相应位加1
            }
        }
        //for(i=MAXLEN;i>=0 && c.s[i]==0;--i);
        while ((c.len >1) && (c.s[c.len-1] == 0)) c.len--;
        //cout<<c.len<<endl;
        //for(int i=oth.len; i>=0; i--)
        //cout<<oth.s[i]<<endl;
        return c;
    }
    
    friend ostream & operator << (ostream & out, const Integer & oth) // 重写 输出流 为 可以显示负数
    {
        if(oth.is_neg) out<<"-";
        for (int k = oth.len-1; k>=0; k--)
            out<<oth.s[k];
        return out;
    }
};
 
 
int main()
{
    //Integer c;
    char s1[MAXLEN], s2[MAXLEN];
    char ope;
    cin>>s1;
    cin>>ope;
    cin>>s2;
    Integer a(s1);
    Integer b(s2);
    
    switch (ope)
    {
        case '+': cout<<a+b<<endl;
            break;
        case '-': cout<<a-b<<endl;
            break;
        case '*': cout<<a*b<<endl;
            break;
        case '/': cout<<a/b<<endl;
        default: break;
    }
    return 0;
}
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 