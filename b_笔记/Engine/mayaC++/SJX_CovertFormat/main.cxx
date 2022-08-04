/****************************************************************************************

   Copyright (C) 2015 Autodesk, Inc.
   All rights reserved.

   Use of this software is subject to the terms of the Autodesk license agreement
   provided at the time of installation or download, or which otherwise accompanies
   this software in either electronic or hard copy form.

****************************************************************************************/

/////////////////////////////////////////////////////////////////////////
//
// This program converts any file in a format supported by the FBX SDK 
// into DAE, FBX, 3DS, OBJ and DXF files. 
//
//变量命名含义：
//FBX：
	//大多数 FBX SDK 类名称以 Fbx 开头。示例：FbxNode, FbxScene, FbxCamera
//p：
	//传递给成员函数的参数以小写“P”开头。示例：pWriteFileFormat, pScene, pFilename
//l：
	//局部变量以小写“L”开头。示例：lWriteFileFormat, lScene, lFilename
//g：
	//全局变量以小写“G”开头。示例：gStart, gStop, gCurrentTime
//m：
	//成员数据（成员变量）以小写“M”开头。示例：mDescription, mImportname, mSelect
//SJX_step:
//执行两次fbx文件格式转换（转换为_fbx7ascii.fbx可读文本）
		// Steps:
		// 1. Initialize SDK objects.
		// 2. Load a file(fbx, obj,...) to a FBX scene.
		// 3. Create a exporter.
		// 4. Retrieve the writer ID according to the description of file format.
		// 5. Initialize exporter with specified file format
		// 6. Export.
		// 8. Destroy the FBX SDK manager
		// 7. Destroy the exporter
//对转换后的fbx做以下操作：
	//解析场景元素信息
	//输出场景元素信息
//合并输出的场景元素，导出fbx（_fbx7ascii.fbx可读文本）
/////////////////////////////////////////////////////////////////////////

#include <fbxsdk.h>
#include <iostream>
#include<stdlib.h>
#include<stdio.h>
#include <vector> 
#include "../Common/Common.h"

using namespace std;
using namespace fbxsdk;

//-----------全局变量----------
#define SAMPLE_FILENAME "SJX_FBX_TEST.fbx"
#define SAMPLE_FILENAME1 "SJX_FBX_FIRST.fbx"
#define SAMPLE_FILENAME2 "SJX_FBX_SECOND.fbx"
const char* lFileTypes[] =
{
    "_dae.dae",            "Collada DAE (*.dae)",
    "_fbx7binary.fbx", "FBX binary (*.fbx)",
    "_fbx7ascii.fbx",  "FBX ascii (*.fbx)",
    "_fbx6binary.fbx", "FBX 6.0 binary (*.fbx)",
    "_fbx6ascii.fbx",  "FBX 6.0 ascii (*.fbx)",
    "_obj.obj",            "Alias OBJ (*.obj)",
    "_dxf.dxf",            "AutoCAD DXF (*.dxf)"
};

//-----------工具-------------
class SJX_Debug 
{
	public:
		void debug(const char* logString)
		{
			std::cout << "\n" << logString << std::endl;
			int flag;
			flag = std::cin.get();
		}
		void debug(int argc, char** argv)
		{
			std::cout << "有 " << argc << " 参数:" << std::endl;
			for (int i = 0; i < argc; ++i) {
				std::cout << argv[i] << std::endl;
			}
			int flag;
			flag = std::cin.get();
		}
		void debug(int logInt)
		{
			std::cout << "\n" << logInt << std::endl;
			int flag;
			flag = std::cin.get();
		}
};
//-------过程--------
class FBX_SJX_Process
{
	public:
		const char* lFileAsciiTypeName = "_fbx7ascii.fbx";
		const char* lFileAsciiType = "FBX ascii(*.fbx)";
		fbxsdk::FbxString lFileName;
		//构造
		FBX_SJX_Process(fbxsdk::FbxString GFileName)
		{
			lFileName = GFileName;
		}
		//转译
		bool pFBX2Ascii()
		{
			fbxsdk::FbxString lFilePath("");
			lFilePath = lFileName;
			lResult = FBX_SDK_Init_Checker(lFilePath);
			if (lResult)
			{
				printf("SDK初始化：%s\n", mSuccessed);

				lResult = FBX_SDK_Export_Checker();

				if (lResult)
				{
					printf("SDK导出：%s\n", mSuccessed);
					lExporter->Destroy();
					DestroySdkObjects(lSdkManager, lResult);
					return true;
				}
				else {
					printf("SDK导出：%s\n", mFailed);
					return false;
				}

				
			}
			else
			{
				printf("SDK初始化：%s\n", mFailed);
				return false;
			}
			

		}
		//解析
		bool pFBXParse()
		{
			fbxsdk::FbxString lFilePath("");
			lFilePath = lFileName;
			lResult = FBX_SDK_Init_Checker(lFilePath);
			if (lResult)
			{
				printf("SDK初始化：%s\n", mSuccessed);
				FBX_SDK_Parse();
				return true;
			}
			else 
			{
				printf("SDK初始化：%s\n", mFailed);
				return false;
			}
		}
		//mat & Texture
		bool pFBXmatTex()
		{
			
			//生成fbxSdk document
			bool lResult = false;
			 fbxsdk::FbxDocument* lDocument = NULL;
			 fbxsdk::FbxString lFilePath("");
			 lFilePath = lFileName;
			 
			 lResult = FBX_SDK_Init_Checker(lFilePath);

			 if(lResult)
			 {
				 printf("SDK初始化：%s\n", mSuccessed);
				 
				 lDocument = fbxsdk::FbxDocument::Create(lSdkManager, "RootDoc");

				 //写入document到fbx
				 printf("CreatDoc初始化：%s\n", mSuccessed);
				 CreateDocument(lSdkManager, lDocument);
				 //导出带有fbx的document
				//读取document
				//销毁sdk
				 DestroySdkObjects(lSdkManager, lResult);
			 }
			 else 
			 {
				 printf("SDK初始化：%s\n", mFailed);
				 
			 }
			

			
			return true;
		}
	private:
		fbxsdk::FbxManager* lSdkManager = NULL;
		fbxsdk::FbxScene* lScene = NULL;
		fbxsdk::FbxExporter* lExporter = NULL;
		const char mSuccessed[5] = "成功";
		const char mFailed[5] = "失败";
		bool lResult = true;
		int numTabs = 0;

		bool FBX_SDK_Init_Checker(FbxManager*& pManager)
		{
			//The first thing to do is to create the FBX Manager which is the object allocator for almost all the classes in the SDK
			pManager = FbxManager::Create();
			if (!pManager)
			{
				FBXSDK_printf("Error: Unable to create FBX Manager!\n");
				exit(1);
			}
			else FBXSDK_printf("Autodesk FBX SDK version %s\n", pManager->GetVersion());

			//Create an IOSettings object. This object holds all import/export settings.
			FbxIOSettings* ios = FbxIOSettings::Create(pManager, IOSROOT);
			pManager->SetIOSettings(ios);

			return lResult;
		}

		bool FBX_SDK_Init_Checker(fbxsdk::FbxString lFilePath)
		{
			
			InitializeSdkObjects(lSdkManager, lScene);
			lResult = LoadScene(lSdkManager, lScene, lFilePath.Buffer());

			return lResult;
		}
		
	
		//导出
#pragma region
		bool FBX_SDK_Export_Checker()
		{
			
			FbxExporter* lExporter = FbxExporter::Create(lSdkManager, "");// 创建一个导出器。
			int lFormat = lSdkManager->GetIOPluginRegistry()->FindWriterIDByDescription(lFileAsciiType);//  根据文件格式的描述检索写入ID。
			fbxsdk::FbxString mMove = ".fbx";
			lFileName.ReplaceAll(mMove, "");
			fbxsdk::FbxString lName = lFileName + lFileAsciiTypeName;
			lResult = lExporter->Initialize(lName, 1, lSdkManager->GetIOSettings());// 初始化导出器。
			return lExporter->Export(lScene);// Export the scene. 导出场景。
		}
#pragma endregion
		//解析
#pragma region
		bool FBX_SDK_Parse()
		{
			// Create the IO settings object.
			FbxIOSettings *ios = FbxIOSettings::Create(lSdkManager, IOSROOT);
			lSdkManager->SetIOSettings(ios);
			// Create an importer using the SDK manager.
			FbxImporter* lImporter = FbxImporter::Create(lSdkManager, "");
			FbxNode* lRootNode = lScene->GetRootNode();
			if (lRootNode) {
				for (int i = 0; i < lRootNode->GetChildCount(); i++)
					PrintNode(lRootNode->GetChild(i));
			}
			lSdkManager->Destroy();
			return true;
		}
		void PrintNode(FbxNode* pNode) {
			PrintTabs();
			const char* nodeName = pNode->GetName();
			FbxDouble3 translation = pNode->LclTranslation.Get();
			FbxDouble3 rotation = pNode->LclRotation.Get();
			FbxDouble3 scaling = pNode->LclScaling.Get();

			// Print the contents of the node.
			printf("<node name='%s' translation='(%f, %f, %f)' rotation='(%f, %f, %f)' scaling='(%f, %f, %f)'>\n",
				nodeName,
				translation[0], translation[1], translation[2],
				rotation[0], rotation[1], rotation[2],
				scaling[0], scaling[1], scaling[2]
			);
			numTabs++;

			// Print the node's attributes.
			for (int i = 0; i < pNode->GetNodeAttributeCount(); i++)
				PrintAttribute(pNode->GetNodeAttributeByIndex(i));

			// Recursively print the children.
			for (int j = 0; j < pNode->GetChildCount(); j++)
				PrintNode(pNode->GetChild(j));

			numTabs--;
			PrintTabs();
			printf("</node>\n");
		}
		string PrintAttribute(FbxNodeAttribute* pAttribute) {
			if (!pAttribute) return "none";

			fbxsdk::FbxString typeName = GetAttributeTypeName(pAttribute->GetAttributeType());
			fbxsdk::FbxString attrName = pAttribute->GetName();
			PrintTabs();
			//std::string mTypeName =typeName.Buffer();
			std::string mTypeNameS = typeName.Buffer();
			// Note: to retrieve the character array of a FbxString, use its Buffer() method.
			printf("<attribute type='%s' name='%s'/>\n", typeName.Buffer(), attrName.Buffer());
			
			return mTypeNameS;
		}
		fbxsdk::FbxString GetAttributeTypeName(FbxNodeAttribute::EType type) {
			switch (type) {
			case FbxNodeAttribute::eUnknown: return "unidentified";
			case FbxNodeAttribute::eNull: return "null";
			case FbxNodeAttribute::eMarker: return "marker";
			case FbxNodeAttribute::eSkeleton: return "skeleton";
			case FbxNodeAttribute::eMesh: return "mesh";
			case FbxNodeAttribute::eNurbs: return "nurbs";
			case FbxNodeAttribute::ePatch: return "patch";
			case FbxNodeAttribute::eCamera: return "camera";
			case FbxNodeAttribute::eCameraStereo: return "stereo";
			case FbxNodeAttribute::eCameraSwitcher: return "camera switcher";
			case FbxNodeAttribute::eLight: return "light";
			case FbxNodeAttribute::eOpticalReference: return "optical reference";
			case FbxNodeAttribute::eOpticalMarker: return "marker";
			case FbxNodeAttribute::eNurbsCurve: return "nurbs curve";
			case FbxNodeAttribute::eTrimNurbsSurface: return "trim nurbs surface";
			case FbxNodeAttribute::eBoundary: return "boundary";
			case FbxNodeAttribute::eNurbsSurface: return "nurbs surface";
			case FbxNodeAttribute::eShape: return "shape";
			case FbxNodeAttribute::eLODGroup: return "lodgroup";
			case FbxNodeAttribute::eSubDiv: return "subdiv";
			default: return "unknown";
			}
		}
		void PrintTabs() {
			for (int i = 0; i < numTabs; i++)
				printf("\t");
		}
#pragma endregion
		//合并
		//材质解析
		bool CreateDocument(FbxManager* pManager, fbxsdk::FbxDocument* pDocument)
		{
			
			int lCount = 0;
			
			// create document info
			fbxsdk::FbxDocumentInfo* lDocInfo = fbxsdk::FbxDocumentInfo::Create(pManager, "DocInfo");
			lDocInfo->mTitle = "SJX_Example document";
			lDocInfo->mSubject = "SJX_materials and texture.";
			lDocInfo->mAuthor = "SJX_meme";
			lDocInfo->mRevision = "SJX_rev. 1.0";
			lDocInfo->mKeywords = "SJX_Fbx document";
			lDocInfo->mComment = "SJX_no particular comments required.";

			// add the documentInfo
			pDocument->SetDocumentInfo(lDocInfo);

			// NOTE: Objects created directly in the SDK Manager are not visible
			// to the disk save routines unless they are manually connected to the
			// documents (see below). Ideally, one would directly use the FbxScene/FbxDocument
			// during the creation of objects so they are automatically connected and become visible
			// to the disk save routines.
			 lEachNode(pDocument);
			 
			//// add the geometry to the main document.
			 lCount = pDocument->GetRootMemberCount();
			 printf("totle node:   %i", lCount);
			//FbxMesh belongs to lPlane; Material that connect to lPlane

			//// Create sub document to contain materials.
			FbxDocument* lMatDocument = FbxDocument::Create(pManager, "Material");

			
			//// Connect the light sub document to main document
			//pDocument->AddMember(lMatDocument);

			//// Create sub document to contain lights
			//FbxDocument* lLightDocument = FbxDocument::Create(pManager, "Light");
			//CreateLightDocument(pManager, lLightDocument);
			//// Connect the light sub document to main document
			//pDocument->AddMember(lLightDocument);

			//lCount = pDocument->GetMemberCount();       // lCount = 5 : 3 add two sub document

			//// document can contain animation. Please refer to other sample about how to set animation
			//pDocument->CreateAnimStack("PlanAnim");

			//lCount = pDocument->GetRootMemberCount();  // lCount = 1: only the lPlane
			//lCount = pDocument->GetMemberCount();      // lCount = 7: 5 add AnimStack and AnimLayer
			//lCount = pDocument->GetMemberCount<FbxDocument>();    // lCount = 2

			return true;
		}
		void lEachNode(fbxsdk::FbxDocument* pDocument)
		{
			
			
			FbxNode* mTmp = lScene->GetRootNode();
			// add the geometry to the main document.
			pDocument->AddRootMember(mTmp);
			// add material object to the sub document
			 // get material to document
			/*const int lMaterialCount = mTmp->GetMaterialCount();
			for (int lMaterialIndex = 0; lMaterialIndex < lMaterialCount; ++lMaterialIndex)
			{
				FbxSurfaceMaterial * lMaterial = mTmp->GetMaterial(lMaterialIndex);
				pDocument->AddMember(lMaterial);
			}*/
			
			/*const char* nodeName = mTmp->GetName();
			printf("nnn:: %s", nodeName);*/
		
			for (int i = 0; i < mTmp->GetChildCount(); i++)
			{
				
				string mType = PrintAttribute(mTmp->GetNodeAttributeByIndex(i));
				GetChildNode(mTmp->GetChild(i), pDocument);
				printf("这是 %s \n", mType);
				
				
			}
				
			
			
		}
		void GetChildNode(FbxNode* pNode, fbxsdk::FbxDocument* pDocument) {
			PrintTabs();
			string mType = PrintAttribute(pNode->GetNodeAttributeByIndex(0));
			// add the geometry to the main document.
			pDocument->AddRootMember(pNode);
			const char* nodeName = pNode->GetName();
			char ch[20];
			strcpy(ch, mType.c_str());
			printf("这是 %s \n", ch);
			if (mType == "mesh")
			{
				printf("abc::");
			}
			// Print the contents of the node.
			printf("<node name='%s' >\n",
				nodeName
			);
			numTabs++;

			// Recursively print the children.
			for (int j = 0; j < pNode->GetChildCount(); j++)
			{
			
				string mType = PrintAttribute(pNode->GetNodeAttributeByIndex(j));
				GetChildNode(pNode->GetChild(j),pDocument);

				printf("这是 %s \n", mType);
				if (mType == "mesh")
				{
					printf("这是mesh");

					// get material to document
					/*const int lMaterialCount = mTmp->GetMaterialCount();
					for (int lMaterialIndex = 0; lMaterialIndex < lMaterialCount; ++lMaterialIndex)
					{
						FbxSurfaceMaterial * lMaterial = mTmp->GetMaterial(lMaterialIndex);
						pDocument->AddMember(lMaterial);
						fbxsdk::FbxString mMatName = lMaterial->GetName();

						printf("包含mat：%s", mMatName.Buffer());
					}*/

				}
			}

			numTabs--;
			PrintTabs();
			printf("</node>\n");
		}
		

};

/**
 * Load a scene given an FbxManager, a FbxScene, and a valid filename.
 */
int LoadSceneTest(FbxManager* pSdkManager, FbxScene* pScene, char* filename) {
	// Create the io settings object.
	FbxIOSettings *ios = FbxIOSettings::Create(pSdkManager, IOSROOT);
	pSdkManager->SetIOSettings(ios);

	// Create an importer using our sdk manager.
	FbxImporter* lImporter = FbxImporter::Create(pSdkManager, "");

	// Use the first argument as the filename for the importer.
	if (!lImporter->Initialize(filename, -1, pSdkManager->GetIOSettings())) {
		printf("Call to FbxImporter::Initialize() failed.\n");
		printf("Error returned: %s\n\n", lImporter->GetStatus().GetErrorString());
		lImporter->Destroy();
		return -1;
	}

	// Import the contents of the file into the scene.
	lImporter->Import(pScene);

	// The file has been imported; we can get rid of the importer.
	lImporter->Destroy();
	return 0;
}
int main(int argc, char** argv)
{
	//argc: count 输入的参数
	//argv: vector 输入的具体值
	
	//启动模式
#pragma region
	int i;
	fbxsdk::FbxString lFilePath("");
	for ( i = 1; i < argc; ++i) { 	//第0个是自己所以从1开始
		
		
		if (fbxsdk::FbxString(argv[i]) == "-h" || fbxsdk::FbxString(argv[i]) == "-help")
		{
			char help[71] = "键入参数 -s 或者 -single 开始解析名为“SJX_FBX_TEST”的实例fbx";
			char help2[82] = "键入参数 -s 或者 -single 加 空格 加 文件名.fbx 开始解析名为“文件名”的单个fbx";
			char help3[82] = "键入参数 -v 或者 -view 加 空格 加 文件名.fbx 开始观察节点";
			char help4[92] = "键入参数 -vm 或者 -viewMat 加 空格 加 文件名.fbx 开始观察节点, 检查材质球，以及依赖的贴图名";

			printf("\n    帮助：\n %s \n", help);
			printf(" %s \n", help2);
			printf(" %s \n", help3);
			printf(" %s \n", help4);
		}
		else if (argc ==3)
		{
			//检查材质球，以及依赖的贴图名,用于unity自动附材质
			if (fbxsdk::FbxString(argv[i]) == "-vm" || fbxsdk::FbxString(argv[i]) == "-viewMat")
			{
				printf("开始检索材质球以及贴图 \n");
				
			
				
				lFilePath = fbxsdk::FbxString(argv[0]);
				fbxsdk::FbxString mMove = "ConvertScene.exe";
				fbxsdk::FbxString mCustomizeName("");
				mCustomizeName = fbxsdk::FbxString(argv[2]);
				lFilePath.ReplaceAll(mMove, mCustomizeName);

				FBX_SJX_Process mSJX(lFilePath);
				mSJX.pFBXmatTex();
				

			}
			if (fbxsdk::FbxString(argv[i]) == "-s" || fbxsdk::FbxString(argv[i]) == "-single")
			{
				printf("开始转译自定义单个fbx \n");
				lFilePath = fbxsdk::FbxString(argv[0]);
				fbxsdk::FbxString mMove = "ConvertScene.exe";
				fbxsdk::FbxString mCustomizeName("");
				mCustomizeName = fbxsdk::FbxString(argv[2]);
				if (!mCustomizeName.IsEmpty())
				{
					if (lFilePath.ReplaceAll(mMove, mCustomizeName))
					{
						FBX_SJX_Process mSJX(lFilePath);
						mSJX.pFBX2Ascii();
					}
				}
				
			}
			if (fbxsdk::FbxString(argv[i]) == "-v" || fbxsdk::FbxString(argv[i]) == "-view")
			{
				printf("开始解析自定义单个fbx \n");

				lFilePath = fbxsdk::FbxString(argv[0]);
				
			
					fbxsdk::FbxString mMove = "ConvertScene.exe";
					fbxsdk::FbxString mCustomizeName("");
					mCustomizeName = fbxsdk::FbxString(argv[2]);
					lFilePath.ReplaceAll(mMove, mCustomizeName);
					printf("路径为 %s \n", lFilePath);
					FBX_SJX_Process mSJX(lFilePath);
					mSJX.pFBXParse();
				
			}
		}
		
		else if (fbxsdk::FbxString(argv[i]) == "-s" || fbxsdk::FbxString(argv[i]) == "-single")
		{
			printf("开始转译单个fbx \n");

			lFilePath = fbxsdk::FbxString(argv[0]);
			fbxsdk::FbxString mMove = "ConvertScene.exe";
			if (lFilePath.ReplaceAll(mMove, SAMPLE_FILENAME))
			{
				printf("路径为 %s \n", lFilePath);
				FBX_SJX_Process mSJX(lFilePath);
				mSJX.pFBX2Ascii();
			}


		}
		else if (fbxsdk::FbxString(argv[i]) == "-v" || fbxsdk::FbxString(argv[i]) == "-view")
		{
			printf("开始解析单个fbx \n");

			lFilePath = fbxsdk::FbxString(argv[0]);
			fbxsdk::FbxString mMove = "ConvertScene.exe";
			if (lFilePath.ReplaceAll(mMove, SAMPLE_FILENAME))
			{
				printf("路径为 %s \n", lFilePath);
				FBX_SJX_Process mSJX(lFilePath);
				mSJX.pFBXParse();
			}


		}
		
	}
	


 	SJX_Debug mDebug;
 	mDebug.debug("传入参数debug：");
 	mDebug.debug(argc, argv);
	if (argc == 1)
	{
		printf("尊敬的艺术家您好，键入 -h 获取帮助");
	}
#pragma endregion

//test zone
#pragma region
	//lFilePath = fbxsdk::FbxString(argv[0]);
	//fbxsdk::FbxString mMove = "ConvertScene.exe";
	//lFilePath.ReplaceAll(mMove, SAMPLE_FILENAME);
	//

	//
	//// Create an SDK manager.
	//FbxManager* lSdkManager = FbxManager::Create();

	////// Create a new scene so it can be populated by the imported file.
	//FbxScene* lCurrentScene = FbxScene::Create(lSdkManager, "My Scene");

	////// Load the scene.
	//LoadSceneTest(lSdkManager, lCurrentScene, lFilePath.Buffer());
	//
	////// Modify the scene. In this example, only one node name is changed.
	//lCurrentScene->GetRootNode()->GetChild(0)->SetName("Test Name q");
	//const char* mName;
	//mName= lCurrentScene->GetRootNode()->GetChild(0)->GetName();
	//printf("mName %s",mName);

	//FbxExporter* lExporter = FbxExporter::Create(lSdkManager, "");// 创建一个导出器。

	//lExporter->Initialize(lFilePath.Buffer(), 1, lSdkManager->GetIOSettings());// 初始化导出器。
	//lExporter->Export(lCurrentScene);// Export the scene. 导出场景。

	//lSdkManager->Destroy();
#pragma endregion

    return 0;
}

