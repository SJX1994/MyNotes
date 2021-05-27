#include "AdiaMain.h"
#include <maya/MFnPlugin.h>


void* InitUI::creator() {
	ReadDataPath m_readDataPath;
// 	MString mDesPath = Read::getFilePath(m_readDataPath.filePathMCmd);
// 	std::string mDesPathS = mDesPath.asChar();
// 	std::string cmdInstall0 = "move "+mDesPathS;
// 	Read::cmd(cmdInstall0);
// 	MGlobal::displayInfo("installFinish");
	
	
	MString mCmd = Read::batchPath(m_readDataPath.filePathMCmd);
	MString mCmdFilePath = Read::getFilePath(m_readDataPath.filePathMCmd)+ "MayaPlug-inSet_V1.0/";
	std::string cmdLine = mCmd.asChar();
	std::string cmdLine1 = mCmdFilePath.asChar();
	MGlobal::displayInfo(cmdLine.c_str());
	std::string cd = "cd " + cmdLine1;
	Read::cmd(cd+"&&"+ cmdLine);
	MGlobal::displayInfo("initConfigFinish");

	return new InitUI;
}

MStatus InitUI::doIt(const MArgList& argList) {
	ReadDataPath m_readDataPath;
	MGlobal::displayInfo(Read::configPath(m_readDataPath.filePathMCmd));
	MGlobal::displayInfo("initUI");
	MStatus status;
	PlugInID m_plugInID;
	status = Read::safeOpen(m_plugInID.initUI);

	return MS::kSuccess;
}

	
void* VertexNormalBatch::creator() {
	MGlobal::displayInfo("VertexNormalBatch");
	return new VertexNormalBatch;
 }

MStatus VertexNormalBatch::doIt(const MArgList& argList) {
	PlugInID m_plugInID;
	Read::safeOpen(m_plugInID.vertexNormalBatch);

	return MS::kSuccess;
}

void* GetWidth::creator() {
	MGlobal::displayInfo("GetWidth");
	return new GetWidth;
}

MStatus GetWidth::doIt(const MArgList& argList) {
	
	
	PlugInID m_plugInID;
	Read::safeOpen(m_plugInID.getWidth);

	return MS::kSuccess;
}

void* DeletUvSet::creator() {
	MGlobal::displayInfo("DeletUvSet");
	return new DeletUvSet;
}

MStatus DeletUvSet::doIt(const MArgList& argList) {
	

	PlugInID m_plugInID;
	Read::safeOpen(m_plugInID.deletUvSet);

	return MS::kSuccess;
}


void* EdgeNormalChecker::creator() {
	MGlobal::displayInfo("EdgeNormalChecker");
	return new EdgeNormalChecker;
}

MStatus EdgeNormalChecker::doIt(const MArgList& argList) {
	
	

	PlugInID m_plugInID;
	Read::safeOpen(m_plugInID.edgeNormalChecker);

	return MS::kSuccess;
}



MStatus initializePlugin(MObject obj) {
	MFnPlugin plugin(obj, "Chad Vernon", "1.0", "Any");
	MStatus status = plugin.registerCommand("Adia_In", InitUI::creator);
	 status = plugin.registerCommand("vertexNormalBatch", VertexNormalBatch::creator);
	 status = plugin.registerCommand("getWidth", GetWidth::creator);
	 status = plugin.registerCommand("deletUvSet", DeletUvSet::creator);
	 status = plugin.registerCommand("edgeNormalChecker", EdgeNormalChecker::creator);
	CHECK_MSTATUS_AND_RETURN_IT(status);
	return status;
}

MStatus uninitializePlugin(MObject obj) {
	MFnPlugin plugin(obj);
	MStatus status = plugin.deregisterCommand("Adia_In");
	 status = plugin.deregisterCommand("vertexNormalBatch");
	 status = plugin.deregisterCommand("getWidth");
	 status = plugin.deregisterCommand("deletUvSet");
	 status = plugin.deregisterCommand("edgeNormalChecker");
	CHECK_MSTATUS_AND_RETURN_IT(status);
	return status;
}