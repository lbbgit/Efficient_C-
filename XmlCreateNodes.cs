using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web; 
using System.Data;
using System.Collections; 
using System.Xml;
using System.IO;

namespace Efficient_CSHarp
{
    public class XmlCreateNodes
    {


        ///// <summary>
        ///// XML节点新增一个指定名称、指定文本内容的子节点
        ///// </summary> 
        //private void InsertXmlDocument(ref XmlElement xmlElement , string newElementName, object objValue)
        //{
        //    XmlElement _xElement = xmlElement.OwnerDocument.CreateElement(newElementName);
        //    _xElement.InnerText = Convert.ToString(objValue);
        //    xmlElement.AppendChild(_xElement);
        //}


        ///// <summary>
        ///// XML节点新增一个指定名称、指定文本内容的子节点，设置一对属性名称和属性值
        ///// </summary> 
        //private void InsertXmlDocument(ref XmlElement xmlElement, string newElementName, object objValue,
        //    string attribName, string attribValue)
        //{
        //    XmlElement _xElement = xmlElement.OwnerDocument.CreateElement(newElementName);
        //    _xElement.InnerText = Convert.ToString(objValue);
        //    _xElement.SetAttribute(attribName, attribValue);
        //    xmlElement.AppendChild(_xElement);
        //}


        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置多对属性名称和属性值
        /// </summary> 
        private void InsertXmlDocument(ref XmlElement xmlElement, string newElementName, object objValue,
            string[] attribName = null, string[] attribValue = null)
        {
            XmlElement _xElement = xmlElement.OwnerDocument.CreateElement(newElementName);

            //指定内容为空，则不插入指定内容
            if (objValue != null)
                _xElement.InnerText = Convert.ToString(objValue);


            //属性名称和属性值队列，为空不操作
            if (attribName != null)
            {
                for (var i = 0; i < attribName.Length; i++)
                {
                    _xElement.SetAttribute(attribName[i], attribValue[i]);
                }
            }


            xmlElement.AppendChild(_xElement);
        }


        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置多对属性名称和属性值
        /// </summary> 
        private XmlElement CreateXmlDocument(ref XmlDocument _xdoc, string newElementName, object objValue,
            string[] attribName = null, string[] attribValue = null)
        {
            XmlElement _xElement = _xdoc.CreateElement(newElementName);

            //属性名称和属性值队列，为空不操作
            if (attribName != null)
            {
                for (var i = 0; i < attribName.Length; i++)
                {
                    _xElement.SetAttribute(attribName[i], attribValue[i]);
                }
            }

            //指定内容为空，则不插入指定内容
            if (objValue != null)
                _xElement.InnerText = Convert.ToString(objValue);

            return _xElement;
        }

        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置多对属性名称和属性值
        /// </summary> 
        private XmlElement CreateXmlDocument(ref XmlDocument _xdoc, string newElementName, object objValue,
            string attribName, string attribValue)
        {
            XmlElement _xElement = _xdoc.CreateElement(newElementName);

            //属性名称和属性值队列，为空不操作
            if (attribName != null)
            {
                _xElement.SetAttribute(attribName, attribValue);
            }

            //指定内容为空，则不插入指定内容
            if (objValue != null)
            {
                _xElement.InnerText = Convert.ToString(objValue);
            }

            return _xElement;
        }

        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置多对属性名称和属性值
        /// </summary> 
        private void SetXmlDocumentAttributes(ref XmlElement _xElement,
            string[] attribName = null, string[] attribValue = null)
        {
            if (attribName != null)
            {
                for (var i = 0; i < attribName.Length; i++)
                {
                    _xElement.SetAttribute(attribName[i], attribValue[i]);
                }
            }
        }

        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置多对属性名称和属性值
        /// </summary> 
        private void SetXmlDocumentAttribute(ref XmlElement _xElement,
            string attribName, string attribValue)
        {
            _xElement.SetAttribute(attribName, attribValue);
        }

        /// <summary>
        /// MES从TDM获取产品测试任务详情
        /// </summary>
        //[WebMethod(Description = "MES从TDM获取产品测试任务详情")]
        public string getTestTaskDetail(string productProjectCode, string figureCode,
            string productCode, string procedureCode, string stepCode, string testProjectID, string testTaskID)
        {

            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xdoc.AppendChild(dec);
            XmlElement TopNode = xdoc.CreateElement("GetTestTaskListResult");
            XmlElement TopNodeStatus = xdoc.CreateElement("Status");


            try
            {

                #region 产品试验数据节点折叠，第一二层嵌套关系在最外层，方便修改嵌套

                //：产品试验数据
                XmlElement testTaskDetail = xdoc.CreateElement("testTaskDetail");
                XmlElement testTaskDetailRoot = CreateXmlDocument(ref xdoc, "产品试验数据", "",
                    new string[] { "版本", "标识", "密级" }, new string[] { "1.01", "--", "--" });

                //：：产品信息 
                XmlElement prod_info = xdoc.CreateElement("产品信息");
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "产品名称" });
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "产品代号" });
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "产品图号" });//必选
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "产品编号" });
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "型号名称" });
                InsertXmlDocument(ref prod_info, "描述", "---", new string[] { "名称" }, new string[] { "型号代号" });//必选
                testTaskDetailRoot.AppendChild(prod_info);


                //：：试验信息
                XmlElement _testinfo = xdoc.CreateElement("试验信息");
                InsertXmlDocument(ref _testinfo, "试验项目", "");//@
                InsertXmlDocument(ref _testinfo, "试验类型", "");
                InsertXmlDocument(ref _testinfo, "试验场地", "");
                InsertXmlDocument(ref _testinfo, "需求部门", "");
                InsertXmlDocument(ref _testinfo, "操作部门", "");
                InsertXmlDocument(ref _testinfo, "操作人", "");
                XmlElement _testtime = xdoc.CreateElement("试验时间");
                InsertXmlDocument(ref _testtime, "开始时间", "");
                InsertXmlDocument(ref _testtime, "结束时间", "");
                _testinfo.AppendChild(_testtime);
                for (var i = 0; i < 2; i++)
                {
                    XmlElement _equip = xdoc.CreateElement("试验设备");
                    InsertXmlDocument(ref _equip, "设备名称", "");
                    InsertXmlDocument(ref _equip, "设备代号", "");
                    InsertXmlDocument(ref _equip, "设备编号", "");
                    _testinfo.AppendChild(_equip);
                }
                XmlElement _condi = xdoc.CreateElement("环境条件");
                XmlElement _condi_1 = xdoc.CreateElement("场地条件");
                //n
                InsertXmlDocument(ref _condi_1, "描述", "---", new string[] { "名称", "量纲" }, new string[] { "温度", "℃" });
                InsertXmlDocument(ref _condi_1, "描述", "---", new string[] { "名称", "量纲" }, new string[] { "湿度", "％" });
                InsertXmlDocument(ref _condi_1, "描述", "---", new string[] { "名称", "量纲" }, new string[] { "经度", "°" });
                InsertXmlDocument(ref _condi_1, "描述", "---", new string[] { "名称", "量纲" }, new string[] { "维度", "°" });
                InsertXmlDocument(ref _condi_1, "描述", "---", new string[] { "名称", "量纲" }, new string[] { "海拔", "m" });
                _condi.AppendChild(_condi_1);
                XmlElement _condi_2 = xdoc.CreateElement("环境试验条件");
                XmlElement _condi_2_1 = xdoc.CreateElement("描述");
                //n
                InsertXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "低温", "℃" });
                InsertXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "高温", "℃" });
                InsertXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "温度梯度", "℃" });
                InsertXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "保温时间", "℃" });
                InsertXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "循环次数", "℃" });
                _condi_2.AppendChild(_condi_2_1);
                _condi.AppendChild(_condi_2);
                _testinfo.AppendChild(_condi);
                testTaskDetailRoot.AppendChild(_testinfo);



                //：：结论数据 <=1
                XmlElement _resultData = xdoc.CreateElement("结论数据");
                for (int i = 0; i < 3; i++)
                {
                    //：：：参数
                    XmlElement _resultData2 = xdoc.CreateElement("参数");
                    InsertXmlDocument(ref _resultData2, "名称", "---");
                    InsertXmlDocument(ref _resultData2, "量纲", "---");
                    InsertXmlDocument(ref _resultData2, "实测值", "---");
                    InsertXmlDocument(ref _resultData2, "合格", "---");
                    XmlElement _resultData2_1 = xdoc.CreateElement("理论值");
                    InsertXmlDocument(ref _resultData2_1, "上限", "---");
                    InsertXmlDocument(ref _resultData2_1, "下限", "---");
                    _resultData2.AppendChild(_resultData2_1);
                    _resultData.AppendChild(_resultData2);
                }
                testTaskDetailRoot.AppendChild(_resultData);



                //：：原始数据   记录有0-n个
                for (int i = 0; i < 3; i++)
                {
                    XmlElement _orgData = xdoc.CreateElement("原始数据");
                    //分类
                    SetXmlDocumentAttributes(ref _orgData, new string[] { "分类" }, new string[] { "---" });//分类还是分组？
                    XmlElement _orgData_remark = xdoc.CreateElement("备注");
                    _orgData_remark.InnerText = "---";
                    _orgData.AppendChild(_orgData_remark);
                    //初始条件
                    XmlElement _orgData_beginCondi = xdoc.CreateElement("初始条件");
                    XmlElement _orgData_beginCondi_1 = xdoc.CreateElement("描述");
                    InsertXmlDocument(ref _orgData_beginCondi_1, "名称", "--");
                    InsertXmlDocument(ref _orgData_beginCondi_1, "类型", "--");
                    InsertXmlDocument(ref _orgData_beginCondi_1, "量纲", "--");
                    InsertXmlDocument(ref _orgData_beginCondi_1, "取值", "--");
                    _orgData_beginCondi.AppendChild(_orgData_beginCondi_1);
                    _orgData.AppendChild(_orgData_beginCondi);
                    //时间
                    XmlElement _orgData_time = CreateXmlDocument(ref xdoc, "时间", "", new string[] { "模式" }, new string[] { "--" });
                    InsertXmlDocument(ref _orgData_time, "起始时刻", "--", new string[] { "量纲" }, new string[] { "--" });
                    InsertXmlDocument(ref _orgData_time, "时间间隔", "--", new string[] { "量纲" }, new string[] { "--" });
                    _orgData.AppendChild(_orgData_time);
                    //规格
                    XmlElement _orgData_Size = CreateXmlDocument(ref xdoc, "规格", "", new string[] { "模式" }, new string[] { "--" });
                    XmlElement _orgData_Size_1 = CreateXmlDocument(ref xdoc, "通道", "", new string[] { "方向", "横坐标" }, new string[] { "--", "" });//1-,2_ 
                    InsertXmlDocument(ref _orgData_Size_1, "名称", "--");
                    InsertXmlDocument(ref _orgData_Size_1, "类型", "--");
                    InsertXmlDocument(ref _orgData_Size_1, "量纲", "--");
                    InsertXmlDocument(ref _orgData_Size_1, "编码", "--");
                    InsertXmlDocument(ref _orgData_Size_1, "路径", "--");
                    _orgData_Size.AppendChild(_orgData_Size_1);
                    _orgData.AppendChild(_orgData_Size);
                    //数据
                    XmlElement _orgData_data = xdoc.CreateElement("数据");
                    SetXmlDocumentAttributes(ref _orgData_data, new string[] { "通道" }, new string[] { "---" });
                    for (var j = 0; j < 3; j++)
                    {
                        InsertXmlDocument(ref _orgData_data, "组", "--");
                    }
                    _orgData.AppendChild(_orgData_data);
                    testTaskDetailRoot.AppendChild(_orgData);
                }

                //主节点，完成！
                testTaskDetail.AppendChild(testTaskDetailRoot);


                //执行成功，返回T:SUCCESS
                TopNodeStatus.InnerText = "T:SUCCESS";
                TopNode.AppendChild(TopNodeStatus);
                TopNode.AppendChild(testTaskDetail);

                #endregion

                xdoc.AppendChild(TopNode);
                System.IO.File.WriteAllText(@"r:\test4.xml", xdoc.InnerXml, Encoding.UTF8);
                return xdoc.InnerXml;
            }
            catch (Exception e)
            {
                //如果报错失败，返回F:错误信息
                TopNodeStatus.InnerText = "F:" + e.Message;
                TopNode.AppendChild(TopNodeStatus);
                xdoc.AppendChild(TopNode);
                return xdoc.InnerXml;
            }
        }
    }

}