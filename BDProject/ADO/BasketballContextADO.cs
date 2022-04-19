using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BDProject.ADO
{
    public class BasketballContextADO
    {
        public void Task4()
        {
            string connectionString = @"Data Source=DESKTOP-IGV9F05\SQLEXPRESS;Initial Catalog=BasketballGame;Integrated Security=True";

            string sqlExpression = "SELECT * FROM Teams";
            //Games.Where(g => (g.CntVisitors > 10000) && (g.Team2.Name == t.Name || g.Team1.Name == t.Name)).Count();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("");
                Console.WriteLine("Подключение открыто");
                Console.WriteLine("Команда - суммарное число зрелищных матчей: ");
                List<string> teamList = new List<string>();
                List<int> teamListID = new List<int>();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    //Console.WriteLine("{0}\t", reader.GetName(0));

                    while (reader.Read()) // построчно считываем данные
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);

                        teamListID.Add(id);
                        teamList.Add(name);

                        

                    }
                }
                reader.Close();

                foreach (var id in teamListID)
                {
                    string sqlExpression2 = ($"SELECT COUNT(*) FROM Games WHERE CntVisitors > 10000 AND (Team1ID = {id} OR Team2ID = {id})");
                    SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                    SqlDataReader reader2 = command2.ExecuteReader();

                    if (reader2.HasRows) // если есть данные
                    {
                        while (reader2.Read()) // построчно считываем данные
                        {
                            int cnt = reader2.GetInt32(0);

                            Console.WriteLine($"{teamList[id - 1]} - {cnt}");
                        }
                    }

                    reader2.Close();


                }
            }
            Console.WriteLine("Подключение закрыто...");

            Console.Read();
        }
    }
}