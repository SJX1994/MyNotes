#include <bits/stdc++.h>
#include <vector>
#include <algorithm>
#include <iostream>
using namespace std;
#pragma region 基本概念
// 计算机的存储器：
  // 主存储器 primary memory
      // 随机访问存储器 random access memory (RAM)
      // 高速缓存存储器 high-speed cache memory
      // 视频存储器 video memory
  // 外存储器 peripheral storage
      // 磁盘存储器 disk storage
      // 磁带存储器 tape storage
// 文件的逻辑结构
      // 文件是记录的汇集，是一种线性结构
      // 分类：
            // 逻辑文件
              // 代码。
            // 物理文件
              // 成块的分布在磁盘中
            // 文件管理器
              // 操作系统或者数据库系统的部分
// 缓冲区和缓冲池
      // 目的：减少磁盘的访问次数
      // 方法：buffering / caching 在内存中保留尽可能多的块
      // 策略：LRU 最少使用  / LFU 不频繁使用 / FIFO 先进先出
// 外排序：磁盘文件的排序
 // 形成尽可能长的排序顺串
 // 最终形成整个排序文件
 // 简而言之：排序 + 归并
#pragma endregion 基本概念

#pragma region 置换选择排序
// Replacement-Selection Sorting
// 是在树形选择排序的基础上得来的
// 流程
      // 【输入缓冲区 - 到RAM堆计算最小值输出 - 输出缓冲区】 - 输出到磁盘（尽量减少）
// ref：
      // https://youtu.be/m0v-trXc2yI
      // https://blog.csdn.net/ReDreamme/article/details/107547166
      // https://blog.csdn.net/aPKUer/article/details/112307163 （扫雪机模型解释置换排序）
// code:
//排序包含10万个整数的文件，假设内存最多可容纳10000个元素
const int BuffSize = 10000;//假设最大缓存空间为10000
int CreateLosser(vector<pair<int, int>> &list, array<int, BuffSize - 1> &LosserTree);
int AdjustLosser(vector<pair<int, int>> &list, array<int, BuffSize - 1> &LosserTree, int index);
void Replace_Selection(string filepath) {
	ifstream in(filepath);
	ofstream out;
	vector<pair<int,int>> list;
	int filenum = 0;
	string temp;
	int min = -0x3f3f3f3f;
	int winner = BuffSize;
	while (list.size() < BuffSize) {
		if (in >> temp) {
			list.push_back({ stoi(temp),filenum });
		}
		else break;
	}
	list.push_back({ min,0 });//最后一个位置放“最小值”，用于初始化败者树
	array<int, BuffSize - 1> LosserTree;
	out.open("subsection_0_" + to_string(filenum) + ".txt");
	winner = CreateLosser(list, LosserTree);
	out << to_string(list[winner].first) << endl;
	while (true)
	{
		if (in >> temp) {
			if (stoi(temp) < min) {
				list[winner] = { stoi(temp),filenum + 1 };
			}
			else {
				list[winner] = { stoi(temp),filenum };
			}
		}
		else {
			list[winner] = { 0x3f3f3f3f,filenum + 1 };
		}
		winner = AdjustLosser(list, LosserTree, winner);
		min = list[winner].first;
		if (min == 0x3f3f3f3f)break;
		if (list[winner].second > filenum) {
			out.close();
			filenum++;
			out.open("subsection_0_" + to_string(filenum) + ".txt");
			out << to_string(min) << endl;
		}
		else {
			out << to_string(min) << endl;
		}
	}
	in.close();
	out.close();
}

int CreateLosser(vector<pair<int, int>> &list, array<int, BuffSize - 1> &LosserTree) {
	for (auto &item : LosserTree)item = BuffSize;
	int winner = BuffSize;
	for (int i = 0; i < BuffSize; i++) {
		winner = AdjustLosser(list, LosserTree, i);
	}
	return winner;
}

int AdjustLosser(vector<pair<int, int>> &list, array<int, BuffSize - 1> &LosserTree,int index) {
	int winner = index;
    index = index + LosserTree.size();
	while (index>0)
	{
		index = (index - 1) / 2;
		if (list[winner].second > list[LosserTree[index]].second) {
			int temp = LosserTree[index];
			LosserTree[index] = winner;
			winner = temp;
		}
		else if (list[winner].second == list[LosserTree[index]].second) {
			if (list[winner].first > list[LosserTree[index]].first) {
				int temp = LosserTree[index];
				LosserTree[index] = winner;
				winner = temp;
			}
		}
	}
	return  winner;
}


#pragma endregion 置换选择排序

#pragma region 二路外排序
// 归并原理：
      // 通过若干次二路合并，形成一个已排序的文件。
      // 为待排序的文件创建尽可能大的初始顺串，可以大大减少扫描遍数和外存读写次数
      // 把初始顺串长度作为权，然后用 哈夫曼 Huffman 树来找到最优解
#pragma endregion 二路外排序
#pragma region 多路外排序
// Loser trees 败者树：用完全二叉树记录败者/哈夫曼树 额外一个输出节点
      // ref：【【外排序】02-2 败者树】 https://www.bilibili.com/video/BV1Fb4y1p7k2/?share_source=copy_web&vd_source=ee8bf4f80f85e2ba54f02e9721d930d6
      // 败者树的优势：记录前一次的比较结果
// Winner Trees 胜者树：用完全二叉树记录胜者
	// 胜者树的优势：记录前一次的比较结果
	// 胜者树的缺点：需要额外的空间
      
#pragma endregion 多路外排序
int main()
{
      Replace_Selection("./"); // 置换选择排序？
      return 0;
}