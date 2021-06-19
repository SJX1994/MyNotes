using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System;

#pragma warning disable 168

namespace USnd
{
    public partial class AudioManager : MonoBehaviour
    {
        private class AudioXmlLoad
        {

            public static XmlDocument Load(Stream xml)
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlReader reader = XmlReader.Create(xml);

                try
                {
                    xmlDoc.Load(reader);
                }
                catch (Exception e)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.LogError(e.ToString());
#endif
                }
                finally
                {
                    reader.Close();
                }

                //Adia
                //Adia

                return xmlDoc;
            }

            public static XmlDocument Load(Stream xsd, Stream xml)
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

                //Adia
                //Adia

                XmlSchema schema = XmlSchema.Read(xsd, ValidationCallback);
                settings.Schemas.Add(schema);

                //Adia
                //Adia
                XmlReader reader = XmlReader.Create(xml, settings);

                try
                {
                    xmlDoc.Load(reader);
                }
                catch (Exception e)
                {
#if USND_DEBUG_LOG
                    AudioDebugLog.LogError(e.ToString());
#endif
                }
                finally
                {
                    reader.Close();
                }

                //Adia
                //Adia

                return xmlDoc;
            }

            static void DebugParse(XmlDocument xmlDoc)
            {
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("MasterSet");

                if (nodeList == null)
                {
                    Debug.Log("not found");
                }
                else
                {
                    Debug.Log("count:" + nodeList.Count);
                    for (int i = 0; i < nodeList.Count; ++i)
                    {
                        XmlNode node = nodeList[i];

                        for (int j = 0; j < node.ChildNodes.Count; ++j)
                        {
                            XmlNode dataNode = node.ChildNodes[j];

                            Debug.Log("dataNode: " + dataNode.Name);

                            if (dataNode.HasChildNodes == true)
                            {
                                XmlNode valueNode = dataNode.ChildNodes[0];
                                Debug.Log("4444444444444444444444444444444");
                                Debug.Log("NodeType; " + valueNode.NodeType.ToString());
                                Debug.Log("LocalName; " + valueNode.LocalName);
                                Debug.Log("Name; " + valueNode.Name);
                                Debug.Log("Value; " + valueNode.Value);
                            }
                        }
                    }
                }
            }

            static void OutputXml(XmlDocument xmlDoc)
            {
                XmlElement elem = xmlDoc.DocumentElement;
                Debug.Log("NodeType; " + elem.NodeType.ToString());
                Debug.Log("LocalName; " + elem.LocalName);
                Debug.Log("Name; " + elem.Name);
                if (elem.HasChildNodes == true)
                {
                    XmlNode childNode = elem.FirstChild;
                    while (childNode != null)
                    {
                        Debug.Log("111111111111111111111111111111");
                        Debug.Log("NodeType; " + elem.NodeType.ToString());
                        Debug.Log("LocalName; " + elem.LocalName);
                        Debug.Log("Name; " + elem.Name);

                        if (childNode.HasChildNodes == true)
                        {
                            for (int i = 0; i < childNode.ChildNodes.Count; ++i)
                            {
                                XmlNode dataNode = childNode.ChildNodes[i];

                                Debug.Log("22222222222222222222222222222222");
                                Debug.Log("NodeType; " + dataNode.NodeType.ToString());
                                Debug.Log("LocalName; " + dataNode.LocalName);
                                Debug.Log("Name; " + dataNode.Name);
                                for (int j = 0; j < dataNode.Attributes.Count; ++j)
                                {
                                    Debug.Log("3333333333333333333333333333333");
                                    XmlAttribute xmlAttr = dataNode.Attributes[j];
                                    Debug.Log("NodeType; " + xmlAttr.NodeType.ToString());
                                    Debug.Log("LocalName; " + xmlAttr.LocalName);
                                    Debug.Log("Name; " + xmlAttr.Name);
                                }

                                if (dataNode.HasChildNodes == true)
                                {
                                    XmlNode valueNode = dataNode.ChildNodes[0];
                                    Debug.Log("4444444444444444444444444444444");
                                    Debug.Log("NodeType; " + valueNode.NodeType.ToString());
                                    Debug.Log("LocalName; " + valueNode.LocalName);
                                    Debug.Log("Name; " + valueNode.Name);
                                    Debug.Log("Value; " + valueNode.Value);
                                }
                            }
                        }
                        childNode = childNode.NextSibling;
                    }
                }
            }

            static void ValidationCallback(object sender, ValidationEventArgs args)
            {
#if USND_DEBUG_LOG
                if (args.Severity == XmlSeverityType.Warning)
                    AudioDebugLog.Log("WARNING:");
                else if (args.Severity == XmlSeverityType.Error)
                    AudioDebugLog.Log("ERROR:");

                AudioDebugLog.Log(args.Message);
#endif
            }
        }
    }

}