using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DocSearch.Model
{
    class DBUtils
    {
        //数据库连接
        SQLiteConnection dbConnection;
        SQLiteCommand command;

        //创建一个空的数据库
        //创建一个连接到指定数据库
        public void createNewDatabase(string path)
        {
            SQLiteConnection.CreateFile(path);
            dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            dbConnection.Open();
        }

        //在指定数据库中创建一个table
        public void createTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS infos (\n" + " id integer PRIMARY KEY,\n"
                + " path text NOT NULL UNIQUE,\n" + " content text,\n" + " md5 text NOT NULL\n" + ");";
            command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void Insert(string tableName, Dictionary<string, object> dic)
        {
            StringBuilder sbCol = new System.Text.StringBuilder();
            StringBuilder sbVal = new System.Text.StringBuilder();

            foreach (KeyValuePair<string, object> kv in dic)
            {
                if (sbCol.Length == 0)
                {
                    sbCol.Append("insert into ");
                    sbCol.Append(tableName);
                    sbCol.Append("(");
                }
                else
                {
                    sbCol.Append(",");
                }

                sbCol.Append("`");
                sbCol.Append(kv.Key);
                sbCol.Append("`");

                if (sbVal.Length == 0)
                {
                    sbVal.Append(" values(");
                }
                else
                {
                    sbVal.Append(", ");
                }

                sbVal.Append("@v");
                sbVal.Append(kv.Key);
            }

            sbCol.Append(") ");
            sbVal.Append(");");

            command.CommandText = sbCol.ToString() + sbVal.ToString();

            foreach (KeyValuePair<string, object> kv in dic)
            {
                command.Parameters.AddWithValue("@v" + kv.Key, kv.Value);
            }

            command.ExecuteNonQuery();
        }


    }
}
