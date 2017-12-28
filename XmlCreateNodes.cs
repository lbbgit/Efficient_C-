using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web; 
using System.Data;
using System.Collections; 
using System.Xml;
using System.IO;
using System.Runtime.Serialization;//.dll
using System.Runtime.Serialization.Json;

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

        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点
        /// </summary> 
        private void InsertXmlDocument(ref XmlElement xmlElement, string newElementName, object objValue)
        {
            XmlElement _xElement = xmlElement.OwnerDocument.CreateElement(newElementName);
            _xElement.InnerText = Convert.ToString(objValue);
            xmlElement.AppendChild(_xElement);
        }


        /// <summary>
        /// XML节点新增一个指定名称、指定文本内容的子节点，设置一对属性名称和属性值
        /// </summary> 
        private void InsertXmlDocument(ref XmlElement xmlElement, string newElementName, object objValue,
            string attribName, string attribValue)
        {
            XmlElement _xElement = xmlElement.OwnerDocument.CreateElement(newElementName);
            _xElement.InnerText = Convert.ToString(objValue);
            _xElement.SetAttribute(attribName, attribValue);
            xmlElement.AppendChild(_xElement);
        }


        private StructXmlElement CreateStructXmlDocument(ref StructXmlElement _xdoc, string newElementName, object objValue,
            string[] attribName = null, string[] attribValue = null)
        {
            return new StructXmlElement(newElementName, Convert.ToString(objValue), attribName, attribValue);
        }

        private void InsertStructXmlDocument(ref StructXmlElement xmlElement, string newElementName, object objValue,
          string[] attribName = null, string[] attribValue = null)
        {
            StructXmlElement _xElement = new StructXmlElement(newElementName, Convert.ToString(objValue), attribName, attribValue);
            xmlElement.AppendChild(_xElement);
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
         
        //根据sql语句得到DataTable
        private DataTable GetDataTableBySql(string sql)
        {
            DataSet ds = GetDataSet(sql);
            if (ds == null || ds.Tables.Count == 0)
                return new DataTable();//返回0行记录，方便直接跳过对每行记录的遍历
            return ds.Tables[0];
        }
         
        public string getTestTaskDetail_Qucik(string productProjectCode, string figureCode,
            string productCode, string procedureCode, string stepCode, string testProjectID, string testTaskID)
        {

            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xdoc.AppendChild(dec);
            XmlElement TopNode = xdoc.CreateElement("GetTestTaskListResult");
            XmlElement TopNodeStatus = xdoc.CreateElement("Status");


            try
            {
                //查询项目Project
                string sql = string.Format(@"select MODELID,xh.prodtyname,se.gradename, pe.*,p.* 
                from pm_project p
                left join pm_projectextendinfo pe on p.id=pe.projectid
                left join dps_securitygrade se on pe.securitygrade=se.gradeid
                left join XA_PRODUCT_INFO xh on xh.xaid=modelid
                where p.id='IPM8323d932c77842d8b7dee8c71a482af3'
                ");
                DataSet ds = GetDataSet(sql);
                //如果没有记录，则返回码为N:指定的型号-图号-编号下没有测试结果
                if (ds.Tables[0].Rows.Count == 0)
                {
                    TopNodeStatus.InnerText = "N:";
                    TopNode.AppendChild(TopNodeStatus);
                    xdoc.AppendChild(TopNode);
                    //因为无记录，不再往下走
                    return xdoc.InnerXml;
                }


                //查询相关的任务Task，后面每个项目不必再依次查询
                string projectid = Convert.ToString(ds.Tables[0].Rows[0]["projectid"]);//left join
                string sql_Task = string.Format(@"select * from pm_wbstask where projectid in (select P.ID from pm_project p)"); //子项目
                string sqlResult = @"select a.*,to_char(a.lilun_value) as lilun_value_text from TDM_CONCLUSION_DATA a where projectid='" + projectid + "'";//结论数据表 
                string sqlEnv = @"select * from TDM_ENVIRONMENT_DATA where projectid='" + projectid + "'";//环境条件数据表;
                string sqlEquip = @"select b.rsname,b.DEVICEMODEL,a.* 
                from rs_reference a
                left join RS_OBJ_TESTHS b on a.rsid=b.id
                where projectid='" + projectid + "'";//试验设备
                string sqlOrgData = @"select * from dual where 1=0";//原始数据，暂不知从何取值

                DataTable dt_Task = GetDataTableBySql(sql_Task);
                DataTable dtResult = GetDataTableBySql(sqlResult);
                DataTable dtEnv = GetDataTableBySql(sqlEnv);
                DataTable dtEquip = GetDataTableBySql(sqlEquip);
                DataTable dtOrgData = GetDataTableBySql(sqlOrgData);

                DataRow dr = ds.Tables[0].Rows[0];
                DataRow drTask = dt_Task.Rows[0];

                /*
                 * -------------------------------------------------------------
                 * 由于节点数量过多，XMLElement实例化耗资源明显导致效率过低
                 * 这里 region 折叠的部分都采用struct，最后拼接回来转为xml字符串
                 * -------------------------------------------------------------
                */
                #region 产品试验数据节点折叠，第一二层嵌套关系在最外层，方便修改嵌套

                //：产品试验数据
                StructXmlElement testTaskDetail = new StructXmlElement("testTaskDetail");
                StructXmlElement testTaskDetailRoot = new StructXmlElement("产品试验数据", "", new string[] { "版本", "标识", "密级" },
                    new string[] { "1.01", "--", Convert.ToString(dr["GRADENAME"]) });

                //：：产品信息 
                StructXmlElement prod_info = new StructXmlElement("产品信息");
                InsertStructXmlDocument(ref prod_info, "描述", dr["MODELPRODUCT"], new string[] { "名称" }, new string[] { "产品名称" });//表单这个，模型不是
                InsertStructXmlDocument(ref prod_info, "描述", dr["PRODUCTID"], new string[] { "名称" }, new string[] { "产品代号" });
                InsertStructXmlDocument(ref prod_info, "描述", dr["PRODUCTTH"], new string[] { "名称" }, new string[] { "产品图号" });//必选
                InsertStructXmlDocument(ref prod_info, "描述", dr["PRODUCTCODE"], new string[] { "名称" }, new string[] { "产品编号" });
                InsertStructXmlDocument(ref prod_info, "描述", dr["PRODTYNAME"], new string[] { "名称" }, new string[] { "型号名称" });
                InsertStructXmlDocument(ref prod_info, "描述", dr["MODELID"], new string[] { "名称" }, new string[] { "型号代号" });//必选
                testTaskDetailRoot.AppendChild(prod_info);


                //：：试验信息
                StructXmlElement _testinfo = new StructXmlElement("试验信息");
                InsertStructXmlDocument(ref _testinfo, "试验项目", "");//@
                InsertStructXmlDocument(ref _testinfo, "试验类型", dr["TESTCATEGORY"]);//Dicts
                InsertStructXmlDocument(ref _testinfo, "试验场地", dr["SYCD"]);
                InsertStructXmlDocument(ref _testinfo, "需求部门", dr["XQBM"]);
                InsertStructXmlDocument(ref _testinfo, "操作部门", dr["CZBM"]);
                InsertStructXmlDocument(ref _testinfo, "操作人", dr["PROCESSOR"]);
                StructXmlElement _testtime = new StructXmlElement("试验时间");
                InsertStructXmlDocument(ref _testtime, "开始时间", Convert.ToString(drTask["REALSTARTTIME"]));//"yyyy-MM-dd HH:mi:ss"
                InsertStructXmlDocument(ref _testtime, "结束时间", Convert.ToString(drTask["REALENDTIME"]));
                _testinfo.AppendChild(_testtime);
                if (dtEquip == null || dtEquip.Rows.Count == 0)
                {
                    //试验设备节点至少要有1个，不能没有
                    StructXmlElement _equip = new StructXmlElement("试验设备");
                    _testinfo.AppendChild(_equip);
                }
                else
                {
                    foreach (DataRow drEquip in dtEquip.Rows)
                    {
                        StructXmlElement _equip = new StructXmlElement("试验设备");
                        InsertStructXmlDocument(ref _equip, "设备名称", drEquip["RSNAME"]);
                        InsertStructXmlDocument(ref _equip, "设备代号", drEquip["DEVICEMODEL"]);//代号，编号？只有一个RSCODE  代号就是型号规格
                        InsertStructXmlDocument(ref _equip, "设备编号", drEquip["RSCODE"]);
                        _testinfo.AppendChild(_equip);
                    }
                }



                //有且只有一个“环境条件”“场地条件”节点
                StructXmlElement _condi = new StructXmlElement("环境条件");
                StructXmlElement _condi_1 = new StructXmlElement("场地条件");
                foreach (DataRow drEnv in dtEnv.Select("TYPE='场地条件'"))
                {
                    InsertStructXmlDocument(ref _condi_1, "描述", drEnv["value"],
                        new string[] { "名称", "量纲" },
                        new string[] { Convert.ToString(drEnv["name"]), Convert.ToString(drEnv["unit"]) });
                }
                _condi.AppendChild(_condi_1);
                //其他类型有几种，就有几个“环境试验条件”节点
                DataTable dtEnv_Type = dtEnv.DefaultView.ToTable(true, "TYPE");
                foreach (DataRow drEnv_Type in dtEnv_Type.Rows)
                {
                    string type = Convert.ToString(drEnv_Type["TYPE"]);
                    //其他类型就是排除“场地条件”后的其他类型,所以“场地条件”跳过
                    if (type == "场地条件")
                        continue;

                    StructXmlElement _condi_2 = new StructXmlElement("环境试验条件", null, new string[] { "名称" }, new string[] { type });
                    StructXmlElement _condi_2_1 = new StructXmlElement("描述");
                    foreach (DataRow _dr in dtEnv.Select("TYPE='" + type + "'"))
                    {
                        InsertStructXmlDocument(ref _condi_2_1, "参数", _dr["value"],
                            new string[] { "名称", "量纲" },
                            new string[] { Convert.ToString(_dr["name"]), Convert.ToString(_dr["unit"]) });
                        //InsertStructXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "低温", "℃" });
                        //InsertStructXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "高温", "℃" });
                        //InsertStructXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "温度梯度", "℃" });
                        //InsertStructXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "保温时间", "℃" });
                        //InsertStructXmlDocument(ref _condi_2_1, "参数", "---", new string[] { "名称", "量纲" }, new string[] { "循环次数", "℃" });
                    }
                    _condi_2.AppendChild(_condi_2_1);
                    _condi.AppendChild(_condi_2);
                }
                _testinfo.AppendChild(_condi);
                testTaskDetailRoot.AppendChild(_testinfo);



                //：：结论数据 <=1
                StructXmlElement _resultData = new StructXmlElement("结论数据");
                foreach (DataRow drResult in dtResult.Rows)
                {
                    //：：：参数
                    StructXmlElement _resultData2 = new StructXmlElement("参数");
                    InsertStructXmlDocument(ref _resultData2, "名称", drResult["name"]);
                    InsertStructXmlDocument(ref _resultData2, "量纲", drResult["unit"]);
                    InsertStructXmlDocument(ref _resultData2, "实测值", drResult["real_value"]);
                    InsertStructXmlDocument(ref _resultData2, "合格", drResult["flag"]);

                    StructXmlElement _resultData2_1 = new StructXmlElement("理论值");
                    string jsonString = Convert.ToString(drResult["lilun_value_text"]);
                    //json解析为name、value对
                    List<temp_NameValue> result_jsons = new List<temp_NameValue>();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(result_jsons.GetType());
                    MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                    result_jsons = serializer.ReadObject(mStream) as List<temp_NameValue>;
                    if (result_jsons != null && result_jsons.Count > 0)
                    {
                        foreach (temp_NameValue jsonNameValue in result_jsons)
                        {
                            //每条依次插入xml结构中
                            //InsertStructXmlDocument(ref _resultData2_1, "上限", "---");
                            //InsertStructXmlDocument(ref _resultData2_1, "下限", "---");
                            InsertStructXmlDocument(ref _resultData2_1, jsonNameValue.name, jsonNameValue.value);
                        }
                    }
                    _resultData2.AppendChild(_resultData2_1);
                    _resultData.AppendChild(_resultData2);
                }
                testTaskDetailRoot.AppendChild(_resultData);



                //：：原始数据   记录有0-n个 ，原始数据记录来源还没有
                foreach (DataRow drOrgData in dtOrgData.Rows)
                {
                    StructXmlElement _orgData = new StructXmlElement("原始数据", null, new string[] { "分类" }, new string[] { "---" });//分类还是分组？
                    StructXmlElement _orgData_remark = new StructXmlElement("备注", "---");
                    _orgData.AppendChild(_orgData_remark);
                    //初始条件
                    StructXmlElement _orgData_beginCondi = new StructXmlElement("初始条件");
                    StructXmlElement _orgData_beginCondi_1 = new StructXmlElement("描述");
                    InsertStructXmlDocument(ref _orgData_beginCondi_1, "名称", "--");
                    InsertStructXmlDocument(ref _orgData_beginCondi_1, "类型", "--");
                    InsertStructXmlDocument(ref _orgData_beginCondi_1, "量纲", "--");
                    InsertStructXmlDocument(ref _orgData_beginCondi_1, "取值", "--");
                    _orgData_beginCondi.AppendChild(_orgData_beginCondi_1);
                    _orgData.AppendChild(_orgData_beginCondi);
                    //时间
                    StructXmlElement _orgData_time = new StructXmlElement("时间", "", new string[] { "模式" }, new string[] { "--" });
                    InsertStructXmlDocument(ref _orgData_time, "起始时刻", "--", new string[] { "量纲" }, new string[] { "--" });
                    InsertStructXmlDocument(ref _orgData_time, "时间间隔", "--", new string[] { "量纲" }, new string[] { "--" });
                    _orgData.AppendChild(_orgData_time);
                    //规格
                    StructXmlElement _orgData_Size = new StructXmlElement("规格", "", new string[] { "模式" }, new string[] { "--" });
                    StructXmlElement _orgData_Size_1 = new StructXmlElement("通道", "", new string[] { "方向", "横坐标" }, new string[] { "--", "" });//1-,2_ 
                    InsertStructXmlDocument(ref _orgData_Size_1, "名称", "--");
                    InsertStructXmlDocument(ref _orgData_Size_1, "类型", "--");
                    InsertStructXmlDocument(ref _orgData_Size_1, "量纲", "--");
                    InsertStructXmlDocument(ref _orgData_Size_1, "编码", "--");
                    InsertStructXmlDocument(ref _orgData_Size_1, "路径", "--");
                    _orgData_Size.AppendChild(_orgData_Size_1);
                    _orgData.AppendChild(_orgData_Size);
                    //数据
                    StructXmlElement _orgData_data = new StructXmlElement("数据", null, new string[] { "通道" }, new string[] { "---" });
                    for (var j = 0; j < 3; j++)
                    {
                        InsertStructXmlDocument(ref _orgData_data, "组", "--");
                    }
                    _orgData.AppendChild(_orgData_data);
                    testTaskDetailRoot.AppendChild(_orgData);
                }

                //主节点，完成！转为xml字符串
                testTaskDetail.AppendChild(testTaskDetailRoot);
                StringBuilder sb_MainNodeXml = StructXml_To_XmlString(testTaskDetail);

                #endregion


                //执行成功，没有报错，状态信息为T:SUCCESS
                TopNodeStatus.InnerText = "T:SUCCESS";
                TopNode.AppendChild(TopNodeStatus);
                xdoc.AppendChild(TopNode);
                string str_XmlDoc = xdoc.InnerXml;

                //主节点的xml字符串插入外壳xml结构中去，返回合并后的xml内容
                StringBuilder sb = new StringBuilder();
                int indexOfInsert = str_XmlDoc.IndexOf("</GetTestTaskListResult");
                sb.Append(str_XmlDoc.Substring(0, indexOfInsert));
                sb.Append(sb_MainNodeXml);//主节点插入外壳xml节点中去
                sb.Append(str_XmlDoc.Substring(indexOfInsert));
                string str_Xml = sb.ToString();
                System.IO.File.WriteAllText(@"r:\test5.xml", str_Xml, Encoding.UTF8);
                return str_Xml;
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

        //StructXmlElement结构树，转为xml字符串
        private StringBuilder StructXml_To_XmlString(StructXmlElement _root)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<").Append(_root.name);
            //如果有属性，则添加属性
            if (_root.attr != null && _root.attr.Count > 0)
            {
                foreach (var _attr in _root.attr)
                {
                    sb.Append(" ").Append(_attr.Key).Append(" = \"").Append(_attr.Value).Append("\"");
                }
            }
            //添加中间文本内容
            sb.Append(">").Append(_root.InnerText);
            //如果有子节点，则添加子节点
            if (_root.sons != null && _root.sons.Count > 0)
            {
                foreach (StructXmlElement _son in _root.sons)
                {
                    sb.Append(StructXml_To_XmlString(_son));
                }
            }

            sb.Append("</").Append(_root.name).Append(">");
            return sb;
        }

        private DataSet _ds;
        //由于平台环境原因，这个方法暂未实现
        private DataSet GetDataSet(string sql)
        {
            if (_ds == null)
            {
                _ds = new DataSet();
                _ds.Tables.Add("empty table");
            }
            return _ds;
        }
     
    }


    /// <summary>
    /// 模拟xml节点树的struct
    /// 说明：由于XmlElement实例化过于消耗资源，节点过多的情况下会导致效率堪忧，现在采用结构树来模拟Xml节点解决效率问题
    /// </summary>
    struct StructXmlElement
    {
        public string name, InnerText;//节点名称，节点中间文本内容
        public Dictionary<string, string> attr;//属性对
        public List<StructXmlElement> sons;//子节点
        public StructXmlElement(string _name, string _innertext = null,
            string[] names = null, string[] vals = null)
        {
            this.name = _name;
            this.InnerText = _innertext;
            this.sons = null;
            this.attr = null;
            if (names != null)
            {
                attr = new Dictionary<string, string>();
                for (int i = 0; i < names.Length; i++)
                    attr.Add(names[i], vals[i]);
            }
        }
        public void AppendChild(StructXmlElement sonTree)
        {
            if (this.sons == null)
                this.sons = new List<StructXmlElement>();
            sons.Add(sonTree);
        }
    }


    /// <summary>
    /// 这个类用来储存json解析出来的name、value值信息
    /// </summary>
    public class temp_NameValue
    {
        public string name;
        public string value;
    }

}