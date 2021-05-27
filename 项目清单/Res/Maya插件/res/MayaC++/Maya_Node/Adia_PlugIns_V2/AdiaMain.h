#pragma once


#include <maya/MArgList.h>
#include <maya/MObject.h>
#include <maya/MGlobal.h>
#include <maya/MPxCommand.h>
#pragma warning(disable:4996)
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
#include <exception>
#include <direct.h>
#include <maya/MCommandResult.h>

class InitUI : public MPxCommand {
public:
	InitUI() {};
	virtual MStatus doIt(const MArgList&);
	static void* creator();
};
class VertexNormalBatch : public MPxCommand {
public:
	VertexNormalBatch() {};
	virtual MStatus doIt(const MArgList&);
	static void* creator();
};
class GetWidth : public MPxCommand {
public:
	GetWidth() {};
	virtual MStatus doIt(const MArgList&);
	static void* creator();
};
class DeletUvSet : public MPxCommand {
public:
	DeletUvSet() {};
	virtual MStatus doIt(const MArgList&);
	static void* creator();
};
class EdgeNormalChecker : public MPxCommand {
public:
	EdgeNormalChecker() {};
	virtual MStatus doIt(const MArgList&);
	static void* creator();
};

const struct ReadDataPath
{
public:
	MString filePathMCmd = "internalVar -usd";
	MString filePath;
	std::string plugInPath;
	std::string plugInName;
	int plugInNum = 4;
};
const struct PlugInID
{
public:
	const int initUI = 0;
	const int vertexNormalBatch = 1;
	const int getWidth = 2;
	const int deletUvSet = 3;
	const int edgeNormalChecker = 4;
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
	MString configPath(MString cmd)
	{
		MCommandResult retval;
		MString ms;
		auto stat = MGlobal::executeCommand(cmd, retval,false,false);
		if (!stat) throw std::runtime_error(stat.errorString().asChar());
		retval.getResult(ms);
		ms += "MayaPlug-inSet_V1.0/config.txt";
		

		return ms;
	}
	MString batchPath(MString cmd)
	{
		MCommandResult retval;
		MString ms;
		auto stat = MGlobal::executeCommand(cmd, retval, false, false);
		if (!stat) throw std::runtime_error(stat.errorString().asChar());
		retval.getResult(ms);
		ms += "MayaPlug-inSet_V1.0/scanBatch.bat";


		return ms;
	}
	MString getFilePath(MString cmd)
	{
		MCommandResult retval;
		MString ms;
		auto stat = MGlobal::executeCommand(cmd, retval, false, false);
		if (!stat) throw std::runtime_error(stat.errorString().asChar());
		retval.getResult(ms);
		return ms;
	}
	std::vector<std::string> split(const std::string& s, char delim) {
		std::vector<std::string> elems;
		split(s, delim, std::back_inserter(elems));
		return elems;
	}
	int cmd(std::string cmd_s)
	{
		std::string s = cmd_s;
		char* c = const_cast<char*>(s.c_str());
		system(c);
		return 0;
	}
	std::string ReadTxtArray(int pointNum)
	{
		ReadDataPath m_readDataPath;
		
		MString pathTxt = m_readDataPath.filePath;
		pathTxt = Read::configPath(m_readDataPath.filePathMCmd);
		int plugInNum = m_readDataPath.plugInNum;
		
		//int patTxtlength = pathTxt.MString::length();
		//pathTxt.asChar();

		std::ifstream infile;
		infile.open(pathTxt.asChar(), ios::in);
		std::vector<std::string> paths;
		std::vector<std::string> names;
		std::string x = "";
		std::string pathMark = " Directory of ";
		std::string nameMark = ".mel";
		std::string myText;
		if (infile.is_open())
		{
			while (std::getline(infile, myText))
			{
				size_t pos = myText.find(pathMark);
				if (pos != std::string::npos)
				{

					myText.erase(pos, pathMark.length());
					paths.push_back(myText);


				}
				size_t pos2 = myText.find(nameMark);
				if (pos2 != std::string::npos)
				{
					std::vector<std::string> a = split(myText, ' ');



					for (std::string& s : a)
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

		if (pointNum > plugInNum && pointNum < 0)
		{
			return "Out of bounds";
		}
		else
		{
			if (pointNum != 0)
			{
				m_readDataPath.plugInPath = paths[pointNum];

				std::string cmdLine = "more " + m_readDataPath.plugInPath + "\\ReadMe.txt" + " pause";
				cmd(cmdLine);
			}
			
		
			return paths[pointNum] + "\\" + names[pointNum];
			
			
		}

	}
	
	MStatus safeOpen(int plugID)
	{
		MStatus status;
		if (MGlobal::mayaState() == MGlobal::kInteractive)
		{
			ReadDataPath m_readDataPath;
			m_readDataPath.filePath = Read::configPath(m_readDataPath.filePathMCmd);
			std::ifstream fin(Read::configPath(m_readDataPath.filePathMCmd).asChar());
			if (!fin)
			{
				fin.close();
				MGlobal::displayInfo("No config found please find TA");
			}
			else
			{
				fin.close();
				
				std::string fileName = Read::ReadTxtArray(plugID);
				MString fileName_M = fileName.c_str();
				status = MGlobal::sourceFile(fileName_M);
				CHECK_MSTATUS(status);
				return status;
			}
			return status;
		}
		return status;
	}
	
};

