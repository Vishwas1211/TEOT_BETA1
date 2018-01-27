/*
 *   Theme:
 *   
 *      
 *   Function Des:
 *   1.
 *   2.
 *   
 *   Version 1.0
 *   Created  By Sni  On 1/24/2018
 */


using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;

namespace XmlRead
{
    public class XML_ReadItem : XML_ReadBase
    {
        public XML_ReadItem(string xmlPath, string singleNode) : base(xmlPath, singleNode)
        {
        }

        protected override void LoadXml(XmlDocument xml,string singleNode)
        {
            XmlNodeList nodeList = xml.SelectSingleNode(singleNode).ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                Item item = new Item();
                string id = ((XmlElement)nodeList[i]).GetAttribute("Id");
                item.Id = Convert.ToUInt32(id);
                
                foreach (XmlElement element in nodeList[i])
                {
                    switch (element.Name)
                    {
                        case "Name":
                            item.Name = element.InnerText.ToString();
                            break;
                        case "ItemType":
                            item.ItemType=(ItemType) Convert.ToByte(element.InnerText.ToString());
                            break;
                        case "Des":
                            item.Des= element.InnerText.ToString();
                            break;
                        case "GoPath":
                            item.GoPath= element.InnerText.ToString();
                            break;
                        case "ImagePath":
                            item.ImagePath = element.InnerText.ToString();
                            break;
                        case "IsStack":
                            item.IsStack =Convert.ToBoolean(element.InnerText.ToString());
                            break;
                        case "Identifier":
                            item.Identifier =Convert.ToInt32(element.InnerText.ToString());
                            break;
                    }
                }
            }
        }
    }// class End

}//namespace End


