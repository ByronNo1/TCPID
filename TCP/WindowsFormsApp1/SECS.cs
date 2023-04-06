
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static WindowsFormsApp1.SECS;

namespace WindowsFormsApp1
{
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
        public static string ConfigDeviceIDandSxFyString(string _DeviceID, string _SxFy)
        {
            string gStr = "";
            string dev = GetDeviceID(ref gStr, _DeviceID);
            string sf = gStr + GetSxFy(_SxFy.ToUpper().Trim());
            gStr = dev + " " + sf + " 00 00 00 00 00 01";
            return gStr;
        }

        public static string GetSxFy( string _SxFy)
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
                TempInt = _Data.Length /2;
                if ((_Data.Length - 2* TempInt ) == 0) //相減後不能有餘數
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
        /// <param name="_dataLenI">有輸入依照輸入的個數,在LIST下 data會輸入""，但是後面有幾LIST</param>
        /// (SECS.DataType.LIST, "",3),(SECS.DataType.ASCII, "323130"),
        /// <returns></returns>
        public static string DataItemOut(DataType dataType,  string data="", int _dataLenI = 0)
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
                dataLen = data.Length / 2;
                if ((data.Length - dataLen * 2) != 0) //有餘數代表輸入資料有問題
                {
                    return "";
                }
            }
            else   //如果有輸入依照輸入的個數,例如LIST會輸入""，但是後面有幾LIST
            {
                dataLen = _dataLenI;
            }
            switch (dataType)
            {
                case DataType.LIST:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString("x2").ToUpper().Trim();
                        TempStr = dataTypeS  + dataLenS;
                    }
                    break;
                case DataType.BOOLEAN:
                    {  //有可能有0x
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString().PadLeft(2, '0');
                        dataStr = Regex.Replace(data.ToString(), "0x", "").PadLeft(2, '0');
                        TempStr = dataTypeS  + dataLenS  + dataStr;
                    }
                    break;
                case DataType.BINARY:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString().PadLeft(2, '0');
                        dataStr = Regex.Replace(data.ToString(), "0x", "").PadLeft(2, '0');
                        TempStr = dataTypeS  + dataLenS  + dataStr;
                    }
                    break;
                case DataType.ASCII:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataLenS = dataLen.ToString("x2").ToUpper().Trim();
                        dataStr = data.ToString();
                        TempStr = dataTypeS  + dataLenS  + dataStr;
                    }
                    break;
                case DataType.INT_1:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(1 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.INT_2:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(2 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.INT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.UINT_1:
                    {
                       dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                       dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                       TempDataStr = dataStr.PadLeft(1 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                       dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.UINT_2:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(2 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.UINT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        dataStr = (Convert.ToInt16(data)).ToString("x2").ToUpper().Trim();
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.FT_4:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        float f1 = Convert.ToSingle(data.ToString().Trim());
                        byte[] b1 = BitConverter.GetBytes(f1);
                        dataStr = string.Empty;
                        foreach (int tmp in b1)
                        {
                            dataStr += tmp.ToString("x2").ToUpper().Trim();
                        }
                        TempDataStr = dataStr.PadLeft(4 * 1 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
                case DataType.FT_8:
                    {
                        dataTypeS = ((int)dataType).ToString("x2").ToUpper().Trim();
                        float f1 = Convert.ToSingle(data.ToString().Trim());
                        byte[] b1 = BitConverter.GetBytes(f1);
                        dataStr = string.Empty;
                        foreach (int tmp in b1)
                        {
                            dataStr += tmp.ToString("x2").ToUpper().Trim();
                        }
                        TempDataStr = dataStr.PadLeft(4 * 2 * dataLen, '0');
                        DataStrB = CharClass.StringAddSpace(TempDataStr);
                        dataLenS = DataStrB.Split(' ').Length.ToString("x2");
                        TempStr = dataTypeS  + dataLenS  + DataStrB;
                    }
                    break;
            }
            return TempStr;
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
            catch (Exception ex )
            {

                throw ex;
            }
          
        }

    }



}
