#include <maya/MSimple.h>
#include <maya/MIOStream.h>
#include <maya/MLibrary.h>
#include <maya/MGlobal.h>
#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <vector>
#include <iterator>
#include <sstream>

#pragma warning(disable:4996)
using namespace std;
const struct ReadDataPath
{
public:
	MString filePath = "E:/MayaPlug-inSet_V1.0/config.txt";
	std::string plugInPath;
	std::string plugInName;
	int plugInNum=4;
};
const struct PlugInID
{
public:
	const int initUI = 0;
	const int vertexNormal = 1;
	
};

namespace Read {
	
	template <typename Out>
	void split(const std::string& s, char delim, Out result) {
		std::istringstream iss(s);
		std::string item;
		while (std::getline(iss, item, delim)) {
			*result++ = item;
		}
	}

	std::vector<std::string> split(const std::string& s, char delim) {
		std::vector<std::string> elems;
		split(s, delim, std::back_inserter(elems));
		return elems;
	}

	std::string ReadTxtArray(int pointNum)
	{
		ReadDataPath m_readDataPath;
		MString pathTxt = m_readDataPath.filePath;
		int plugInNum = m_readDataPath.plugInNum;
		
		//int patTxtlength = pathTxt.MString::length();
		//pathTxt.asChar();
		
		std::ifstream infile;
		infile.open(pathTxt.asChar(), ios::in);
		std::vector<string> paths;
		std::vector<string> names;
		std::string x = "";
		std::string pathMark = " Directory of ";
		std::string nameMark = ".mel";
		std::string myText;
		if (infile.is_open())
		{
			while (std::getline(infile, myText))
			{
				size_t pos = myText.find(pathMark);
				if ( pos != std::string::npos)
				{
					
					myText.erase(pos,pathMark.length() );
					paths.push_back(myText);
					
					
				}
				size_t pos2 = myText.find(nameMark);
				if (pos2 != std::string::npos)
				{
					std::vector<std::string> a = split(myText, ' ');

					

					for (string& s : a)
					{
						if (s != " ")
						{
							size_t pos3 = s.find(nameMark);
							if (pos3 != std::string::npos)
							{
								names.push_back(s);
							}
						}
						
					}

				}
			}
			
		}
		
		
		infile.close();

		if (pointNum > plugInNum && pointNum<0)
		{
			return "Out of bounds";
		}
		else
		{
			return paths[pointNum]+"\\"+names[pointNum];
		}
		
	}
	
	
};




DeclareSimpleCommand(InitUI, "Autodesk", "2019");
// 
// MStatus vertexNormal::doIt(const MArgList& args)
// {
// 	cout << "vertexNormal\n" << endl;
// 
// 
// 	ReadDataPath m_readDataPath;
// 	PlugInID m_plugInID;
// 	// Write the text out in 3 different ways.
// 	//cout << "Hello World! (cout)\n";
// 	//MGlobal::displayInfo("Hello world! (script output)");
// 	std::string x= Read::ReadTxtArray(m_plugInID.vertexNormal);
// 	//MGlobal::displayInfo(x.c_str());
// 	
// 	int a = m_readDataPath.plugInNum;
// 	
// 	
// 	//MGlobal::executeCommandOnIdle(MString(d), true);
// 
// 	if (MGlobal::mayaState()==MGlobal::kInteractive)
// 	{
// 		MString fileName = x.c_str();
// 		//MString sCmd = MString("file -q  -rfn  \"")+fileName+"\""+MString("-type \"mel\"");
// 		//MGlobal::executeCommand(sCmd, true);
// 		MGlobal::sourceFile(fileName);
// 	}
// 
// 	//MLibrary::cleanup();
// 	return MS::kSuccess;
// }



MStatus InitUI::doIt(const MArgList& args)
{
	ReadDataPath m_readDataPath;
	PlugInID m_plugInID;
	std::string x = Read::ReadTxtArray(m_plugInID.initUI);

	if (MGlobal::mayaState() == MGlobal::kInteractive)
	{
		MString fileName = x.c_str();
		MGlobal::sourceFile(fileName);
	}
	return MS::kSuccess;
}

 
