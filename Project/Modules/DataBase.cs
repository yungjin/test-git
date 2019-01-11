using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Test.Modules
{
    public class DataBase
    {
        private const string conStr = "server=192.168.3.?;user=root;password=test;database=test;port=3306";
        private Hashtable resultMap;
        private ArrayList resultList;

        private MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = conStr;
            try
            {
                conn.Open();
            }
            catch
            {
                Console.WriteLine("Connection Fail!");
                conn = null;
            }
            return conn;
        }

        public Hashtable SelectList()
        {
            resultMap = new Hashtable();
            resultList = new ArrayList();
            MySqlConnection conn = GetConnection();
            if (conn == null)
            {
                resultMap.Add("msg", "오류");
                return resultMap;
            }
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from board where delYn = 'N';";
            comm.Connection = conn;
            MySqlDataReader sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                Hashtable rowMap = new Hashtable();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    rowMap.Add(sdr.GetName(i), sdr.GetValue(i));
                }
                resultList.Add(rowMap);
            }
            resultMap.Add("msg", "성공");
            resultMap.Add("result", resultList);
            sdr.Close();
            conn.Close();
            return resultMap;
        }

        public Hashtable Insert(Hashtable paramMap)
        {
            resultMap = new Hashtable();
            resultList = new ArrayList();
            MySqlConnection conn = GetConnection();
            if (conn == null)
            {
                resultMap.Add("msg", "오류");
                return resultMap;
            }
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = string.Format("insert into board (title, contents) values ('{0}','{1}');",paramMap["title"], paramMap["contents"]);
            comm.Connection = conn;
            int result = comm.ExecuteNonQuery();
            resultMap.Add("msg", "성공");
            resultMap.Add("result", result);
            conn.Close();
            return resultMap;
        }

        public Hashtable Update(Hashtable paramMap)
        {
            resultMap = new Hashtable();
            resultList = new ArrayList();
            MySqlConnection conn = GetConnection();
            if (conn == null)
            {
                resultMap.Add("msg", "오류");
                return resultMap;
            }
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = string.Format("update board set title = '{0}', contents = '{1}' where no = {2};", paramMap["title"], paramMap["contents"], paramMap["no"]);
            comm.Connection = conn;
            int result = comm.ExecuteNonQuery();
            resultMap.Add("msg", "성공");
            resultMap.Add("result", result);
            conn.Close();
            return resultMap;
        }

        public Hashtable Delete(Hashtable paramMap)
        {
            resultMap = new Hashtable();
            resultList = new ArrayList();
            MySqlConnection conn = GetConnection();
            if (conn == null)
            {
                resultMap.Add("msg", "오류");
                return resultMap;
            }
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = string.Format("update board set delYn = 'Y', modDate = now() where no = {0};", paramMap["no"]);
            comm.Connection = conn;
            int result = comm.ExecuteNonQuery();
            resultMap.Add("msg", "성공");
            resultMap.Add("result", result);
            conn.Close();
            return resultMap;
        }
    }
}
