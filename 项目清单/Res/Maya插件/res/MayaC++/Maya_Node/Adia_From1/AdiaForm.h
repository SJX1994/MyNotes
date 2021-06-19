#pragma once
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <memory>
#include <stdexcept>
#include <string>
#include <array>
#include <sys/stat.h>
#include <sys/types.h>
#include <msclr\marshal_cppstd.h>

namespace AdiaFrom1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for AdiaForm
	/// </summary>
	public ref class AdiaForm : public System::Windows::Forms::Form
	{
	public:
		AdiaForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~AdiaForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::Label^ label1;
	public: System::Windows::Forms::TextBox^ textBox1;
	private:

	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->BackColor = System::Drawing::SystemColors::ControlLightLight;
			this->button1->ForeColor = System::Drawing::Color::LimeGreen;
			this->button1->Location = System::Drawing::Point(81, 115);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(125, 25);
			this->button1->TabIndex = 0;
			this->button1->Text = L"Install";
			this->button1->UseVisualStyleBackColor = false;
			this->button1->Click += gcnew System::EventHandler(this, &AdiaForm::button1_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->ForeColor = System::Drawing::Color::Lime;
			this->label1->ImageAlign = System::Drawing::ContentAlignment::TopLeft;
			this->label1->Location = System::Drawing::Point(64, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(161, 36);
			this->label1->TabIndex = 1;
			this->label1->Text = L"请在maya>Mel命令行中输入：\n internalVar -usd \n 将路径黏贴到输入框中";
			this->label1->Click += gcnew System::EventHandler(this, &AdiaForm::label1_Click);
			// 
			// textBox1
			// 
			this->textBox1->ForeColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(0)), static_cast<System::Int32>(static_cast<System::Byte>(192)),
				static_cast<System::Int32>(static_cast<System::Byte>(0)));
			this->textBox1->Location = System::Drawing::Point(81, 66);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(125, 21);
			this->textBox1->TabIndex = 2;
			this->textBox1->TextChanged += gcnew System::EventHandler(this, &AdiaForm::textBox1_TextChanged);
			// 
			// AdiaForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::Black;
			this->ClientSize = System::Drawing::Size(271, 152);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->Name = L"AdiaForm";
			this->Text = L"AdiaPlugInstall";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	int cmd(std::string cmd_s)
	{
		std::string s = cmd_s;
		char* c = const_cast<char*>(s.c_str());
		system(c);
		return 0;
	}
	std::string exec(const char* cmd)
	{
		char buffer[128];
		std::string result = "";
		FILE* pipe = _popen(cmd, "r");
		if (!pipe)throw std::runtime_error("popen Failed");
		try
		{
			while (fgets(buffer, sizeof buffer, pipe) != NULL) {
				result += buffer;
			}
		}
		catch (...)
		{
			_pclose(pipe);
			throw;
		}
		_pclose(pipe);
		
		return result;
	}
	bool replace(std::string& str, const std::string& from, const std::string& to) {
		size_t start_pos = str.find(from);
		if (start_pos == std::string::npos)
			return false;
		str.replace(start_pos, from.length(), to);
		return true;
	}
	void replaceAll(std::string& str, const std::string& from, const std::string& to) {
		if (from.empty())
			return;
		size_t start_pos = 0;
		while ((start_pos = str.find(from, start_pos)) != std::string::npos) {
			str.replace(start_pos, from.length(), to);
			start_pos += to.length(); 
		}
	}
	std::string delWarper(std::string res)
	{
		int r = res.find('\n');
		while (r != std::string::npos)
		{
			if (r != std::string::npos)
			{
				res.replace(r, 1, "");
				r = res.find('\n');
			}
		}
		return res;
	}
	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) 
	{
		String ^ fileNeedMovePath = textBox1->Text;
		msclr::interop::marshal_context context;
		std::string stdFileNeedMovePath = context.marshal_as<std::string>(fileNeedMovePath);

		replaceAll(stdFileNeedMovePath, "/", "\\");
		const char* chFileNeedMovePath = stdFileNeedMovePath.c_str();
		fileNeedMovePath = context.marshal_as<String^>(stdFileNeedMovePath);
		struct stat info;
		if (stat(chFileNeedMovePath, &info) != 0)
		{
			MessageBox::Show(fileNeedMovePath + "无法访问");
		}
		else if(info.st_mode & S_IFDIR)
 		{
			//MessageBox::Show(fileNeedMovePath + "  正确路径");
			
			//cmd("chdir && pause");
			//std::string strOrgPath = exec("chdir")+"\\MayaPlug-inSet_V1.0";
			std::string strOrgPath = exec("chdir")+std::string("\\MayaPlug-inSet_V1.0");
			strOrgPath = delWarper(strOrgPath);
			String^ OrgPath = context.marshal_as<String^>(strOrgPath) ;
			std::string symbolCmd = "\"";

			//replaceAll(strOrgPath,"\\","\\\\");
			//replaceAll(stdFileNeedMovePath, "\\", "\\\\");
			std::string cmdLines0 = std::string("xcopy ")+ symbolCmd+strOrgPath + symbolCmd + std::string(" ")+ symbolCmd+stdFileNeedMovePath +symbolCmd + std::string(" /S /E /H");
			
			std::string press = std::string("xcopy ")+ std::string("\"C:\\Users\\adia1785\\Documents\\maya\\2019\\scripts\"") + std::string("C:\\Users\\adia1785\\Desktop\\新建文件夹\"") +std::string(" /S /E /H && pause");
			std::string press1 = "xcopy \"C:\\Users\\adia1785\\Documents\\maya\\2019\\scripts\" \"C:\\Users\\adia1785\\Desktop\\新建文件夹\" /S /E /H && pause";
			std::string press2 = "xcopy \"" + strOrgPath + "\" \""+ stdFileNeedMovePath +"\" /S /E /H && pause";
			cmd(press2);
			//String^ cmdLines0S = context.marshal_as<String^>(press2);
			//MessageBox::Show(cmdLines0S);
			
		
				MessageBox::Show("安装成功");
			
			

		}
		else
		{
			MessageBox::Show(fileNeedMovePath + "不存在");
		}

		
		
	}
	private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void textBox1_TextChanged(System::Object^ sender, System::EventArgs^ e) 
	{

	}
};
}
