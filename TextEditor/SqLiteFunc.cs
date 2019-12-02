using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditor
{
    class SqLiteFunc
    {

            public static void CreateDb(string DataBase)
            {

                SQLiteConnection.CreateFile(DataBase);
                using (SQLiteConnection DbConnection = new SQLiteConnection("Data Source=" + DataBase + "; Version=3;"))
                {
                    string sql = "CREATE TABLE 'MainTable' ( 'NameFile'  TEXT UNIQUE,'Type'  TEXT,'Data'  TEXT,PRIMARY KEY('NameFile')); ";

                    try
                    {
                        DbConnection.Open();
                        SQLiteCommand cmd = new SQLiteCommand(sql, DbConnection);
                       
                        cmd.ExecuteNonQuery();
                        DbConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            public async static void Insert(string DataBase, string NameFile, string Data, string Type)
            {



                using (SQLiteConnection DbConnection = new SQLiteConnection("Data Source=" + DataBase + "; Version=3;"))
                {

                    SQLiteCommand cmd = new SQLiteCommand("REPLACE into MainTable (NameFile,Type,Data)Values(@NameFile,@Type,@Data)", DbConnection);

                    cmd.Parameters.AddWithValue("NameFile", NameFile);
                    cmd.Parameters.AddWithValue("Type", Type);
                    cmd.Parameters.AddWithValue("Data", Data);
                    try
                    {
                        DbConnection.Open();
                        await cmd.ExecuteNonQueryAsync();
                        DbConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }



            }



            public static List<string> GetImportedFileList(string DataBase)
            {
                List<string> SavedFiles = new List<string>();
                using (SQLiteConnection DbConnection = new SQLiteConnection("Data Source=" + DataBase + "; Version=3;"))
                {
                    SQLiteCommand fmd = DbConnection.CreateCommand();
                    fmd.CommandText = @"SELECT NameFile FROM MainTable";
                    try
                    {
                        DbConnection.Open();
                        SQLiteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            string FileNames = (string)r["NameFile"];

                            SavedFiles.Add(FileNames);

                        }
                        DbConnection.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return SavedFiles;
                }

            }

            public static string Open(string DataBase, string NameFile)
            {
                string Data = "";
                using (SQLiteConnection DbConnection = new SQLiteConnection("Data Source=" + DataBase + "; Version=3;"))
                {
                    SQLiteCommand fmd = DbConnection.CreateCommand();
                    fmd.CommandText = @"SELECT Data FROM MainTable where NameFile = '" + NameFile + "'";
                    try
                    {
                        DbConnection.Open();


                        SQLiteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            Data = (string)r["Data"];

                        }
                        DbConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return Data;
                }
            }

        }
    }