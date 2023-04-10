
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static WindowsFormsApp1.SECS;

namespace WindowsFormsApp1
{

    public static class SecsSessionType
    {
        //            Select.req：SType = 1，PType = 0
        //            Select.rsp：SType = 2，PType = 0，Header Byte 3 为 SelectStatus 项
        //            Linktest.req：SType = 5，Ptype = 0，Session ID = 0xFFFF
        //            Linktest.rsp：SType = 6，Ptype = 0，Session ID = 0xFFFF
        //            Separate.req：SType = 9，PType = 0
        //            Select.req/Select.rsp
        //           Not Selected 状态转化为 Selected 状态所使用的消息(Active Entity 发送Select.req)
        //           Deselect.req/Deselect.rsp
        //           在双方协议终止通信时使用.由想要通信终止的一端发送Deselect.req.
        //           Linktest.req/Linktest.rsp
        //           连接状态的确认和维护确认, 如果没有应答则转换为Not Connected 状态.
        //           Separate.req
        //           单方面通知通信终止时使用.
        //           Reject.req
        //           收到无效消息时发送

        private const string _SelectRequest = "FFFF00000001";
        private const string _SelectResponse = "FFFF00000002";
        private const string _DeselectRequest = "FFFF00000003";
        private const string _DeselectResponse = "FFFF00000004";
        private const string _LinktestRequest = "FFFF00000005";
        private const string _LinktestResponse = "FFFF00000006";
        private const string _RejectRequest = "FFFF00000007";
        private const string _SeparateRequest = "FFFF00000009";

        public static string StrTimeByte
        {
            get
            {
                string StrTime = (DateTime.Now.Day).ToString("x2").ToUpper();
                StrTime += (DateTime.Now.Hour).ToString("x2").ToUpper();
                StrTime += (DateTime.Now.Minute).ToString("x2").ToUpper();
                StrTime += (DateTime.Now.Second).ToString("x2").ToUpper();
                return StrTime;
            }
        }
        public static string SelectRequest
        {
            get { return _SelectRequest + StrTimeByte; }
        }
        public static string SelectResponse
        {
            get { return _SelectResponse + StrTimeByte; }
        }
        public static string DeselectRequest
        {
            get { return _DeselectRequest + StrTimeByte; }
        }
        public static string DeselectResponse
        {
            get { return _DeselectResponse + StrTimeByte; }
        }
        public static string LinktestRequest
        {
            get { return _LinktestRequest + StrTimeByte; }
        }
        public static string LinktestResponse
        {
            get { return _LinktestResponse + StrTimeByte; }
        }
        public static string RejectRequest
        {
            get { return _RejectRequest + StrTimeByte; }
        }
        public static string SeparateRequest
        {
            get { return _SeparateRequest + StrTimeByte; }
        }
    }

    internal class SECS
    {
        public string strDeviceID = "";
        public enum DataType
        {
            LIST = 0x1,
            BINARY = 0x21,
            BOOLEAN = 0x25,
            ASCII = 0x41,
            INT_8 = 0x61,
            INT_1 = 0x65,
            INT_2 = 0x69,
            INT_4 = 0x71,
            FT_8 = 0x81,
            FT_4 = 0x91,
            UINT_8 = 0xA1,
            UINT_1 = 0xA5,
            UINT_2 = 0xA9,
            UINT_4 = 0xB1,
            CHAR_2 = 0x49,
            JIS = 0x45,
        }

        private static int _intSystemByte = 32768;

        public static string StrSystemByte
        {
            get
            {
                _intSystemByte++;
                if (_intSystemByte > 1073741824)
                {
                    _intSystemByte = 1;
                }
                string StrTime = _intSystemByte.ToString("x2").ToUpper();
                StrTime = StrTime.PadLeft(8, '0');
                return StrTime;
            }
            set
            {  //如果系統Byte 與 連線的太接近必須修改。避免搞錯
                CultureInfo provider;
                provider = new CultureInfo("en-US");
                if (int.TryParse(value, NumberStyles.HexNumber, provider, out _))
                {
                    int.TryParse(value, System.Globalization.NumberStyles.HexNumber, provider, out _intSystemByte); //(str)16進制轉換成int
                }
            }

        }

        public static string ConfigDeviceIDandSxFyString(string _DeviceID, string _SxFy)
        {
            string gStr = "";
            string dev = GetDeviceID(ref gStr, _DeviceID);
            string sf = gStr + GetSxFy(_SxFy.ToUpper().Trim());
            gStr = dev + " " + sf + " 00 00 00 00 00 01";
            return gStr;
        }

        public static string GetSxFy(string _SxFy)
        {
            string TempStr = "";
            try
            {
                string[] StrStrAA = _SxFy.Split('S');
                if (StrStrAA.Length == 2) //只有一個S 只會有2個
                {
                    string[] StrStrBB = StrStrAA[1].Split('F');
                    if (StrStrAA.Length == 2) //只有一個F 只會有2個
                    {
                        string Sx;    //equipment to server connect 8
                        Sx = "8" + StrStrBB[0];    //equipment to server connect 8 對方必須回復
                        Sx = "0" + StrStrBB[0];    //對方不用回復
                        string Fy = Convert.ToString(int.Parse(StrStrBB[1]), 16);
                        Fy = (Fy.PadLeft(2, '0')).ToUpper();
                        TempStr = Sx + " " + Fy;
                    }

                }
            }
            catch (Exception)
            {
                TempStr = "";
            }

            return TempStr;
        }


        private static string GetDeviceID(ref string rawData, string deviceID)
        {
            string HexStr = Convert.ToString(int.Parse(deviceID), 16).ToUpper().Trim();
            string HexStr1 = HexStr.PadLeft(4, '0');
            string HexStr2 = CharClass.StringAddSpace(HexStr1);
            rawData += HexStr2;
            return HexStr2;
        }


        public static string GetDataLenHead(string _Data)
        {
            string TempStr = "";
            int TempInt = 0;
            try
            {
                TempInt = _Data.Length / 2;
                if ((_Data.Length - 2 * TempInt) == 0) //相減後不能有餘數
                {
                    TempStr = Convert.ToString(TempInt, 16);
                    TempStr = TempStr.PadLeft(8, '0').ToUpper();
                }
            }
            catch (Exception)
            {
                TempStr = "";
            }

            return TempStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="_dataLenI">有輸入依照輸入的個數,在LIST下 data會輸入""，所以後面有幾資料</param>
        /// DataItemOut(Ascii,"",0)  , DataItemOut(List,"",0) , DataItemOut(List,"",5) 
        /// <returns></returns>
        public static string DataItemOut(DataType dataType, int _dataLenI, string data)
        {
            string TempStr = "";
            int dataLen;
            string dataTypeS;
            string dataLenS; //資料長度
            string dataStr;
            string TempDataStr;
            string DataStrB;
            if (_dataLenI == 0)
            {
                if ((data.Length - ((int)(data.Length / 2)) * 2) != 0) //有餘數代表輸入資料有問題
                {
                    return "";
                }
                else
                {
                    _dataLenI = (int)(data.Length / 2);
                }
            }
            //如果有輸入依照輸入的個數,例如LIST會輸入""，但是後面有幾LIST
            dataLen = _dataLenI;

            switch (dataType)
            {
                case DataType.LIST:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString("x2").ToUpper().Trim();
                        TempStr = dataTypeS + dataLenS;
                    }
                    break;
                case DataType.BOOLEAN:
                    {  //有可能有0x
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString().PadLeft(2, '0');

                        if (data != "0" || data.ToUpper() != "F")
                        {
                            data = "FF";
                        }
                        else
                        {
                            data = "00";
                        }
                        dataStr = Regex.Replace(data.ToString(), "0x", "").PadLeft(2, '0');
                        TempStr = dataTypeS + dataLenS + dataStr;
                    }
                    break;
                case DataType.BINARY:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString().PadLeft(2, '0');
                        dataStr = Regex.Replace(data.ToString(), "0x", "").PadLeft(2, '0');
                        TempStr = dataTypeS + dataLenS + dataStr;
                    }
                    break;
                case DataType.JIS:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString("x2").ToUpper().Trim();
                        dataStr = data.ToString();
                        dataStr = dataStr.PadLeft(dataLen * 2, '0');  //依照輸入的個數為主補上，因為轉換成HEX 所以要乘2， "A1" => (ASCII)HexString"4131"
                        dataStr = dataStr.Substring(dataStr.Length - dataLen * 2, dataLen * 2); //避免輸入過多
                        TempStr = dataTypeS + dataLenS + dataStr;
                    }
                    break;
                case DataType.ASCII:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString("x2").ToUpper().Trim();
                        dataStr = data.ToString();
                        dataStr = dataStr.PadLeft(dataLen * 2, '0');  //依照輸入的個數為主補上，因為轉換成HEX 所以要乘2， "A1" => (ASCII)HexString"4131"
                        dataStr = dataStr.Substring(dataStr.Length - dataLen * 2, dataLen * 2); //避免輸入過多
                        TempStr = dataTypeS + dataLenS + dataStr;
                    }
                    break;
                case DataType.INT_1:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(1 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.INT_2:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(2 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 2).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.INT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 4).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.INT_8:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(8 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 8).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.UINT_1:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(1 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.UINT_2:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(2 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 2).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.UINT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 4).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.UINT_8:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(8 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (dataLen * 8).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.FT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        float f1 = Convert.ToSingle(data.ToString().Trim());
                        byte[] b1 = BitConverter.GetBytes(f1);
                        dataStr = string.Empty;
                        foreach (int tmp in b1.Reverse())
                        {
                            dataStr += tmp.ToString("x2").ToUpper().Trim();
                        }
                        TempDataStr = dataStr.PadLeft(4 * 1 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (4 * 1 * dataLen).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;
                    }
                    break;
                case DataType.FT_8:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        double f1 = Convert.ToDouble(data.ToString().Trim());
                        byte[] b1 = BitConverter.GetBytes(f1);
                        dataStr = string.Empty;
                        foreach (int tmp in b1.Reverse())
                        {
                            dataStr += tmp.ToString("x2").ToUpper().Trim();
                        }
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = TempDataStr;
                        dataLenS = (4 * 2 * dataLen).ToString("x2");
                        TempStr = dataTypeS + dataLenS + DataStrB;


                    }
                    break;
            }
            return TempStr;
        }

    }


    internal class SECSItem
    {


        public SECSItem(DataType _DataType, int _intItemCount, string _strValue = "", string _strName = "",
            string _Description = "", List<SECSItem> _ListSecSItems = null)
        {
            DataType = _DataType;
            intItemCount = _intItemCount;
            strValue = _strValue;
            strName = _strName;
            Description = _Description;
            ListSecSItems = _ListSecSItems;
            if (_DataType == DataType.LIST)
            {
                ListSecSItems = new List<SECSItem>();
            }
        }

        //  public int intIndex;
        public string strName;
        public string Description;
        //  public string strLevel;
        public string strValue;
        public DataType DataType;
        public int intItemCount;
        public List<SECSItem> ListSecSItems;
        public int intIndexTag;
    }
    internal class SecsTransaction
    {
        #region ex
        //S6F11.Add(new SECSItem(SECS.DataType.UINT_2, 1, "22"));
        //S6F11.Add(new SECSItem(SECS.DataType.UINT_1, 1, "111"));
        //S6F11.Add(new SECSItem(SECS.DataType.BINARY, 1, "35"));
        //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 0, ""));
        //S6F11.Add(new SECSItem(SECS.DataType.INT_8, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.INT_4, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.INT_2, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.INT_1, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.FT_8, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.FT_4, 1, "11"));
        //S6F11.Add(new SECSItem(SECS.DataType.UINT_8, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 4, "ABCD"));
        //S6F11.Add(new SECSItem(SECS.DataType.LIST, 0, ""));
        //S6F11.Add(new SECSItem(SECS.DataType.BINARY, 3, "303030"));
        //S6F11.Add(new SECSItem(SECS.DataType.BOOLEAN, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 1, "1"));
        //S6F11.Add(new SECSItem(SECS.DataType.JIS, 3, "111"));
        //S6F11.Add(new SECSItem(SECS.DataType.INT_8, 1, "1"));
        #endregion
        public SecsTransaction(int _intStreams, int _intFunctions, string _strName = "", string _Description = "",
            int _intItemCount = 0, bool _isReply = false, int _intReplyStreams = 0, int _intReplyFunctions = 0)
        {
            strName = _strName;
            Description = _Description;
            intStreams = _intStreams;
            intFunctions = _intFunctions;
            intItemCount = _intItemCount;
            isReply = _isReply;
            intReplyStreams = _intReplyStreams;
            intReplyFunctions = _intReplyFunctions;
            ListSecSItems = new List<SECSItem>();
            tmpListSItemsNolevel = new List<SECSItem>();
            OLDListSecSItems = new List<SECSItem>();
        }
        public string strName;
        public string Description;
        public int intStreams;
        public int intFunctions;
        public int intItemCount;
        public readonly DataType DataType = SECS.DataType.LIST;
        public List<SECSItem> ListSecSItems;
        private List<SECSItem> OLDListSecSItems;
        //private List<SECSItem> OLDListSecSItemsA;
        private List<SECSItem> tmpListSItemsNolevel; //沒有階層單純存資料用

        public bool isReply;
        public int intReplyStreams;
        public int intReplyFunctions;


        public void Add(SECSItem _SECSItem)
        {
            if (OLDListSecSItems != ListSecSItems) //判斷有沒有不一樣有，代表 ListSecSItems被外部修改過 要重讀
            {
                ListSecSItemsTotmpListSItemsNolevel();
            }
            tmpListSItemsNolevel.Add(_SECSItem);
            AddLevelSecSItems(ListSecSItems, _SECSItem);
            //NoleveltoListSecSItems();
        }



        private void ListSecSItemsTotmpListSItemsNolevel()
        {

        }

        private void NoleveltoListSecSItems()
        {
            SECSItem tmpSECSItem;
            ListSecSItems = new List<SECSItem>();
            for (int i = 0; i < tmpListSItemsNolevel.Count; i++)
            {
                tmpSECSItem = tmpListSItemsNolevel[i];
                AddLevelSecSItems(ListSecSItems, tmpSECSItem);
            }
        }

        private void AddLevelSecSItems(List<SECSItem> _ListSecSItems, SECSItem _tmpSECSItem)
        {

            List<SECSItem> tmpListSecSItemsA = null;
            List<SECSItem> OLDListSecSItemsA = new List<SECSItem>();
            getNeedListSecSItems(_ListSecSItems,ref OLDListSecSItemsA);

            for (int i = 0; i < OLDListSecSItemsA.Count; i++)
            {
                if (OLDListSecSItemsA[OLDListSecSItemsA.Count - 1].intItemCount > OLDListSecSItemsA[OLDListSecSItemsA.Count - 1].ListSecSItems.Count)
                {
                    tmpListSecSItemsA = OLDListSecSItemsA[OLDListSecSItemsA.Count - 1].ListSecSItems;
                }
                
            }
            if (tmpListSecSItemsA == null )
            {
                _ListSecSItems.Add(_tmpSECSItem);
            }
            else
            {
                tmpListSecSItemsA.Add(_tmpSECSItem);
            }
            //int index = _ListSecSItems.Count;
            //if (index != 0)
            //{
            //    if (_ListSecSItems[index - 1].DataType == DataType.LIST &&
            //        (_ListSecSItems[index - 1].intItemCount != _ListSecSItems[index - 1].ListSecSItems.Count()))
            //    {

            //        AddLevelSecSItems(ref _ListSecSItems[index - 1].ListSecSItems, _tmpSECSItem);
            //    }
            //    else
            //    {
            //        _ListSecSItems.Add(_tmpSECSItem);
            //    }

            //}
            //else
            //{
            //    _ListSecSItems.Add(_tmpSECSItem);
            //}
        }


        //找到最底層沒有塞滿的LIST 
        private List<SECSItem> getNeedListSecSItemsBB(List<SECSItem> _ListSecSItems, List<SECSItem> oldListSecSItems = null)
        {
            if (_ListSecSItems == null)
            {
                return _ListSecSItems;
            }

            for (int i = 0; i < _ListSecSItems.Count; i++)
            {
                if (_ListSecSItems[i].ListSecSItems != null && _ListSecSItems[i].intItemCount > _ListSecSItems[i].ListSecSItems.Count)
                {
                    return getNeedListSecSItemsBB(_ListSecSItems[i].ListSecSItems, _ListSecSItems);
                }
                else
                {
                    if (_ListSecSItems[i].DataType == DataType.LIST)
                    {
                        if (_ListSecSItems[i].intItemCount > _ListSecSItems[i].ListSecSItems.Count)
                        {
                            return _ListSecSItems[i].ListSecSItems;
                        }
                        else
                        {
                            // return _ListSecSItems;
                        }
                    }
                    else
                    {
                        // return _ListSecSItems;
                    }

                }
            }
            if (oldListSecSItems == null)
            {
                return _ListSecSItems;
            }
            return oldListSecSItems;
        }


        private List<SECSItem> getNeedListSecSItemsAA(List<SECSItem> _ListSecSItems)
        {
            int index = _ListSecSItems.Count;
            int index2 = 0;
            if (index == 0)
            {
                return _ListSecSItems;
            }
            if (_ListSecSItems[index - 1].DataType == DataType.LIST)
            {
                if (_ListSecSItems[index - 1].intItemCount > _ListSecSItems[index - 1].ListSecSItems.Count)
                {
                    return getNeedListSecSItemsAA(_ListSecSItems[index - 1].ListSecSItems);
                }
                else
                {
                    index2 = _ListSecSItems[index - 1].ListSecSItems.Count;
                    if (_ListSecSItems[index - 1].ListSecSItems[index2 - 1].DataType == DataType.LIST)
                    {
                        if (_ListSecSItems[index - 1].ListSecSItems[index2 - 1].intItemCount >
                            _ListSecSItems[index - 1].ListSecSItems[index2 - 1].ListSecSItems.Count)
                        {
                            return getNeedListSecSItemsAA(_ListSecSItems[index - 1].ListSecSItems[index2 - 1].ListSecSItems);
                        }
                    }



                    return _ListSecSItems;
                }
            }

            return _ListSecSItems;
        }


        private void getNeedListSecSItems(List<SECSItem> _ListSecSItems,ref List<SECSItem> OLDListSecSItemsA, bool isReEntry = false)
        {
            int index = _ListSecSItems.Count;
            if (index == 0 && isReEntry == false)
            {
                OLDListSecSItemsA = _ListSecSItems;
                return ;
            }
            for (int i = 0; i < _ListSecSItems.Count; i++)
            {
                if (_ListSecSItems[i].DataType == DataType.LIST)
                {
                    OLDListSecSItemsA.Add(_ListSecSItems[i]);
                    getNeedListSecSItems(_ListSecSItems[i].ListSecSItems, ref OLDListSecSItemsA, true);
                }
            }

        }

        private List<SECSItem> getNeedSecSItems(SECSItem _SecSItems)
        {
            if (_SecSItems.ListSecSItems.Count < _SecSItems.intItemCount )
            {

            }

            return _SecSItems.ListSecSItems;
        }


        private List<SECSItem> getNeedListSecSItemsCC(List<SECSItem> _ListSecSItems)
        {
            int index = _ListSecSItems.Count;
            int index2 = 0;
            if (index == 0)
            {
                return _ListSecSItems;
            }
            if (_ListSecSItems[index - 1].DataType == DataType.LIST)
            {
                return getNeedListSecSItemsCC(_ListSecSItems[index - 1].ListSecSItems);
            }
            else
            {
                return _ListSecSItems;
            }

            return _ListSecSItems;
        }



        public string GetSendString()
        {
            ListSecSItemsTotmpListSItemsNolevel();
            SECSItem tmpSECSItem;
            string tmpSend = "";
            string strValue = "";
            for (int i = 0; i < tmpListSItemsNolevel.Count; i++)
            {
                tmpSECSItem = tmpListSItemsNolevel[i];

                if (tmpSECSItem.DataType == DataType.ASCII || tmpSECSItem.DataType == DataType.JIS)
                {
                    strValue = CharClass.StringToAscString(tmpSECSItem.strValue);
                }
                else
                {
                    strValue = tmpSECSItem.strValue;
                }
                tmpSend += DataItemOut(tmpSECSItem.DataType, tmpSECSItem.intItemCount, strValue);
            }

            return tmpSend;
        }


    }

    public class CharClass
    {

        public static string StringAddSpace(string inputStr)
        {
            string TempStr = "";
            for (int i = 0; i < inputStr.Length; i += 2) //以兩個為一單位
            {
                string subStr = inputStr.Substring(i, 2);
                TempStr += subStr + " ";
            }
            return TempStr.Trim();
        }

        public static byte[] ConvertHexStrToByteArray(string hexStr)
        {
            string[] array = new string[hexStr.Length / 2];
            byte[] bytes = new byte[array.Length];
            for (int i = 0; i < hexStr.Length; i++)
            {
                array[i / 2] += hexStr[i];
            }
            for (int i = 0; i < array.Length; i++)
            {
                bytes[i] = Convert.ToByte(array[i], 16);
            }
            return bytes;
        }

        public static string StringToAscString(string str)
        {
            try
            {
                string TempStr = "";
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
                for (int i = 0; i < byteArray.Length; i++)
                {
                    TempStr += byteArray[i].ToString("x2");
                }
                return TempStr.ToUpper().Trim();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }



}
