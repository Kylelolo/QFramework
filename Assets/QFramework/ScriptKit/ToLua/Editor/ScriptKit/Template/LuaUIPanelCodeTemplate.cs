/****************************************************************************
 * Copyright (c) 2019.3 vin129
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

namespace QFramework
{
	using System;
	using System.IO;
	using System.Text;
	using UnityEngine;

	public class LuaUIPanelCodeTemplate:IBaseTemplate
	{
		public void Generate(string generateFilePath, string behaviourName, string nameSpace,PanelCodeData panelCodeData)
		{
			var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
			StringBuilder strBuilder = new StringBuilder();

			strBuilder.AppendLine("/****************************************************************************");
			strBuilder.AppendFormat(" * {0}.{1} {2}\n", DateTime.Now.Year, DateTime.Now.Month, SystemInfo.deviceName);
			strBuilder.AppendLine(" ****************************************************************************/");
			strBuilder.AppendLine();

			strBuilder.AppendLine("using System;");
			strBuilder.AppendLine("using System.Collections.Generic;");
			strBuilder.AppendLine("using UnityEngine;");
			strBuilder.AppendLine("using UnityEngine.UI;");
			strBuilder.AppendLine("using QFramework;").AppendLine();

			strBuilder.AppendLine("namespace " + nameSpace);
			strBuilder.AppendLine("{");
			strBuilder.Append("\t").AppendFormat("public class {0}Data : UIPanelData", behaviourName).AppendLine();
			strBuilder.Append("\t").AppendLine("{");
			strBuilder.Append("\t\t").AppendLine("// TODO: Query Mgr's Data");
			strBuilder.Append("\t").AppendLine("}");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("\tpublic partial class {0} : UILuaPanel", behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("\t{");
			strBuilder.Append("\t\t").AppendLine("protected override void ProcessMsg (int eventId,QMsg msg)");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t\t").AppendLine("throw new System.NotImplementedException ();");
			strBuilder.Append("\t\t").AppendLine("}")
				.AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void OnInit(IUIData uiData = null)");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t").Append("\t")
				.AppendLine("mData = uiData as " + behaviourName + "Data ?? new " + behaviourName + "Data();");
			strBuilder.Append("\t\t").Append("\t").AppendLine("//please add init code here");
			strBuilder.Append("\t\t").Append("\t").AppendLine("BindLuaComponent();");
			strBuilder.Append("\t\t").AppendLine("}").AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void OnOpen(IUIData uiData = null)");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t").AppendLine("}").AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void OnShow()");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t").AppendLine("}").AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void OnHide()");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t").AppendLine("}").AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void OnClose()");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t").AppendLine("}").AppendLine();
			strBuilder.Append("\t\t").AppendLine("void ShowLog(string content)");
			strBuilder.Append("\t\t").AppendLine("{");
			strBuilder.Append("\t\t\t").AppendFormat("Debug.Log(\"[ {0}:]\" + content);", behaviourName).AppendLine();
			strBuilder.Append("\t\t").AppendLine("}");
			strBuilder.Append("\t}").AppendLine();
			strBuilder.Append("}");

			sw.Write(strBuilder);
			sw.Flush();
			sw.Close();
		}
    }

	public class LuaPanelComponentsCodeTemplate:IBaseTemplate
	{
		public void Generate(string generateFilePath, string behaviourName, string nameSpace,
		    PanelCodeData panelCodeData)
		{
			var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
			var strBuilder = new StringBuilder();

			strBuilder.AppendLine("/****************************************************************************");
			strBuilder.AppendFormat(" * {0}.{1} {2}\n", DateTime.Now.Year, DateTime.Now.Month, SystemInfo.deviceName);
			strBuilder.AppendLine(" ****************************************************************************/");
			strBuilder.AppendLine();
			strBuilder.AppendLine("namespace " + nameSpace);
			strBuilder.AppendLine("{");
			strBuilder.AppendLine("\tusing UnityEngine;");
			strBuilder.AppendLine("\tusing UnityEngine.UI;");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("\tpublic partial class {0}\n", behaviourName);
			strBuilder.AppendLine("\t{");
			strBuilder.AppendFormat("\t\tpublic const string NAME = \"{0}\";", behaviourName).AppendLine();
			strBuilder.AppendLine();

			foreach (var objInfo in panelCodeData.MarkedObjInfos)
			{
				var strUIType = objInfo.MarkObj.ComponentName;
				strBuilder.AppendFormat("\t\t[SerializeField] public {0} {1};\r\n",
					strUIType, objInfo.Name);
			}

			strBuilder.AppendLine();
			strBuilder.Append("\t\t").AppendLine("protected override void ClearUIComponents()");
			strBuilder.Append("\t\t").AppendLine("{");
			
			foreach (var markInfo in panelCodeData.MarkedObjInfos)
			{
				strBuilder.AppendFormat("\t\t\t{0} = null;\r\n",
					markInfo.Name);
			}

			strBuilder.AppendLine("\t\t\tmData = null;");
			
			strBuilder.Append("\t\t").AppendLine("}");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("\t\tprivate {0}Data mPrivateData = null;\n", behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendFormat("\t\tpublic {0}Data mData\n", behaviourName);
			strBuilder.AppendLine("\t\t{");
			strBuilder.Append("\t\t\tget { return mPrivateData ?? (mPrivateData = new ").Append(behaviourName)
				.Append("Data()); }")
				.AppendLine();
			strBuilder.AppendLine("\t\t\tset");
			strBuilder.AppendLine("\t\t\t{");
			strBuilder.AppendLine("\t\t\t\tmUIData = value;");
			strBuilder.AppendLine("\t\t\t\tmPrivateData = value;");
			strBuilder.AppendLine("\t\t\t}");
			strBuilder.AppendLine("\t\t}");
			strBuilder.AppendLine("\t}");
			strBuilder.AppendLine("}");

			sw.Write(strBuilder);
			sw.Flush();
			sw.Close();
		}
	}


	public class LuaPanelTemplate:IBaseTemplate
	{	
		public void Generate(string generateFilePath, string behaviourName, string nameSpace,
		PanelCodeData panelCodeData)
		{
			var sw = new StreamWriter(generateFilePath, false, new UTF8Encoding(false));
			var strBuilder = new StringBuilder();

			strBuilder.AppendLine("--=============================================================================");
			strBuilder.AppendFormat("-- {0}.{1} {2}\n", DateTime.Now.Year, DateTime.Now.Month, SystemInfo.deviceName);
			strBuilder.AppendLine("--=============================================================================");
			strBuilder.AppendFormat("local {0} = class(\"{0}\",LuaBehaviour)\n", behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("--===== 初始化流程:注意Awake方法不要重写 =====");
			strBuilder.AppendFormat("function {0}:BindUI()\n", behaviourName);
			foreach (var objInfo in panelCodeData.MarkedObjInfos)
			{
				var strUIType = objInfo.MarkObj.ComponentName;
				strBuilder.AppendFormat("\tself.{0} = self:Find(self.gameObject,\"{1}\");\r\n",
					objInfo.Name,objInfo.PathToElement);
			}
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("function {0}:RegisterUIEvent()\n", behaviourName);
			foreach (var objInfo in panelCodeData.MarkedObjInfos)
			{
				var strUIType = objInfo.MarkObj.ComponentName;
				var qUIHelperFunc = GetQUIFunctionNameByUIType(strUIType);
				if(qUIHelperFunc.IsNotNullAndEmpty())
					strBuilder.AppendLine();
					strBuilder.AppendFormat("\t{0}(self.{1},function()\n",qUIHelperFunc,objInfo.Name);
					strBuilder.AppendLine();
					strBuilder.AppendLine("\tend)");
			}
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendLine("--===== Behaviour生命周期函数 =====");
			strBuilder.AppendFormat("function {0}:OnEnable()\n", behaviourName);
			strBuilder.AppendFormat("\tlog(\"{0}:OnEnable\")\n",behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("function {0}:Start()\n", behaviourName);
			strBuilder.AppendFormat("\tlog(\"{0}:Start\")\n",behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("function {0}:Update()\n", behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("function {0}:OnDisable()\n", behaviourName);
			strBuilder.AppendFormat("\tlog(\"{0}:OnDisable\")\n",behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("end");
			strBuilder.AppendLine();
			strBuilder.AppendFormat("function {0}:OnDestroy()\n", behaviourName);
			strBuilder.AppendFormat("\tlog(\"{0}:OnDestroy\")\n",behaviourName);
			strBuilder.AppendLine();
			strBuilder.AppendLine("end");
			strBuilder.AppendLine("--================================");
			strBuilder.AppendFormat("return {0}.new();", behaviourName);
			sw.Write(strBuilder);
			sw.Flush();
			sw.Close();
		}

		private string GetQUIFunctionNameByUIType(string uiType){
			switch(uiType){
				case "Button":
					return "QUIHelper.SetButtonClickEvent";
			}
			return string.Empty;
		}
	}
}
