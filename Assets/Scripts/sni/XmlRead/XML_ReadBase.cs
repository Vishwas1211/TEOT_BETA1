using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace XmlRead
{
    public abstract class XML_ReadBase
    {
       

        private XmlDocument _xml;
        private XmlReaderSettings _xmlSet;


        public XML_ReadBase(string xmlPath, string singleNode)
        {
            _xml = new XmlDocument();
            _xmlSet = new XmlReaderSettings();
            _xmlSet.IgnoreComments = true;
            _xml.Load(xmlPath);
            LoadXml(_xml, singleNode);
        }

        protected abstract void LoadXml(XmlDocument xml, string singleNode);
        

    }

}
