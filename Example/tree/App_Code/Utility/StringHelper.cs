using System;
using System.Collections.Generic;
using System.Text;


    public static class StringHelper
    {
        /// <summary>
        /// ����ָ�����ȵ��ַ���,������strLong��str�ַ���
        /// </summary>
        /// <param name="strLong">���ɵĳ���</param>
        /// <param name="str">��str�����ַ���</param>
        /// <returns></returns>
        public static string StringOfChar( int strLong, string str )
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }
    }

