using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.IO;

/// <summary>
///返回金蝶数据
/// </summary>
public class BatchFactInfo
{
    public int BatchFactID = 2;

    public int StorageID = 3;
    public string StorageType = "L";
    public string StorageName = "1号库";

    public int StorageDetailID = 4;
    public string StorageDetailType = "D";
    public string StorageDetailName = "A13堆";

    public string ContractNo = "41";
    public string BatchNo = "55";
    public string CargoName = "大米";
    public string ProductPlace = "中国";
    public DateTime ProductDate = DateTime.Now;
    public string PlanType = "I";
    public DateTime FinishTime = DateTime.Now;
    public double FinishTotalWeight = 9;
    public string Unit = "包";
    public int Quantity = 4;
    public string StorageCode = "1号库";
    public string StorageDetailCode = "A13堆";

    public bool SetXml(string xml)
    {
        DataSet ds = new DataSet();
        StringReader read = new StringReader(xml);
        try
        {
            ds.ReadXml(read);
            if (ds.Tables.Count < 3)
                return false;

            DataRow dr = ds.Tables[0].Rows[0];
            BatchFactID = int.Parse(dr["BatchFactID"].ToString());
            ContractNo = dr["ContractNo"].ToString();
            BatchNo = dr["BatchNo"].ToString();
            CargoName = dr["CargoName"].ToString();
            ProductPlace = dr["ProductPlace"].ToString();
            ProductDate = DateTime.Parse(dr["ProductDate"].ToString());
            PlanType = dr["PlanType"].ToString();
            FinishTime = DateTime.Parse(dr["FinishTime"].ToString());
            FinishTotalWeight = double.Parse(dr["FinishTotalWeight"].ToString());
            Quantity = int.Parse(dr["Quantity"].ToString());
            Unit = dr["Unit"].ToString();

            dr = ds.Tables[0].Rows[1];
            StorageID = int.Parse(dr["StorageID"].ToString());
            StorageType = dr["StorageType"].ToString();
            StorageName = dr["StorageName"].ToString();

            dr = ds.Tables[0].Rows[2];
            StorageDetailID = int.Parse(dr["StorageDetailID"].ToString());
            StorageDetailType = dr["StorageDetailType"].ToString();
            StorageDetailName = dr["StorageDetailName"].ToString();

        }
        catch
        {
            return false;
        }

        return true;
    }

    public string GetXml()
    {
        XmlDocument doc = new XmlDocument();
        XmlElement newElem;
        XmlNode rootNode;
        XmlElement childNode;

        doc.LoadXml("<BatchFactInfo></BatchFactInfo>");

        newElem = doc.CreateElement("BatchFactID");
        newElem.InnerText = BatchFactID.ToString();
        doc.DocumentElement.AppendChild(newElem);

        rootNode = doc.SelectSingleNode("BatchFactInfo");
        childNode = doc.CreateElement("StorageInfo");
        rootNode.AppendChild(childNode);

        newElem = doc.CreateElement("StorageID");
        newElem.InnerText = StorageID.ToString();
        childNode.AppendChild(newElem);

        newElem = doc.CreateElement("StorageType");
        newElem.InnerText = StorageType;
        childNode.AppendChild(newElem);

        newElem = doc.CreateElement("StorageName");
        newElem.InnerText = StorageName;
        childNode.AppendChild(newElem);

        rootNode = doc.SelectSingleNode("BatchFactInfo");
        childNode = doc.CreateElement("StorageDetailInfo");
        rootNode.AppendChild(childNode);

        newElem = doc.CreateElement("StorageDetailID");
        newElem.InnerText = StorageDetailID.ToString();
        childNode.AppendChild(newElem);

        newElem = doc.CreateElement("StorageDetailType");
        newElem.InnerText = StorageDetailType;
        childNode.AppendChild(newElem);

        newElem = doc.CreateElement("StorageDetailName");
        newElem.InnerText = StorageDetailName;
        childNode.AppendChild(newElem);

        newElem = doc.CreateElement("ContractNo");
        newElem.InnerText = ContractNo;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("BatchNo");
        newElem.InnerText = BatchNo;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("CargoName");
        newElem.InnerText = CargoName;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("ProductPlace");
        newElem.InnerText = ProductPlace;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("ProductDate");
        newElem.InnerText = ProductDate.ToString();
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("PlanType");
        newElem.InnerText = PlanType;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("FinishTime");
        newElem.InnerText = FinishTime.ToString();
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("FinishTotalWeight");
        newElem.InnerText = FinishTotalWeight.ToString();
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("Unit");
        newElem.InnerText = Unit;
        doc.DocumentElement.AppendChild(newElem);

        newElem = doc.CreateElement("Quantity");
        newElem.InnerText = Quantity.ToString();
        doc.DocumentElement.AppendChild(newElem);

        //newElem = doc.CreateElement("State");
        //newElem.InnerText = State;
        //doc.DocumentElement.AppendChild(newElem);

        //newElem = doc.CreateElement("LastUpdateTime");
        //newElem.InnerText = LastUpdateTime.ToString();
        //doc.DocumentElement.AppendChild(newElem);

        //newElem = doc.CreateElement("LastUpdateMan");
        //newElem.InnerText = LastUpdateMan;
        //doc.DocumentElement.AppendChild(newElem);

        return doc.InnerXml;
    }
    public int GetResultValue(string xml)
    {
        int value = 1000;
        DataSet ds = new DataSet();
        StringReader read = new StringReader(xml);
        try
        {
            ds.ReadXml(read);
            if (ds.Tables.Count == 0)
                return value;

            DataRow dr = ds.Tables[0].Rows[0];
            value = int.Parse(dr["resultCode"].ToString());
        }
        catch
        {
            return 1000;
        }

        return value;
    }
}
