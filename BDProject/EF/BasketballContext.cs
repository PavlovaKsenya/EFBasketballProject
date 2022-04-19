using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDProject.EF
{
    public class Team
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
    }
    public class Game
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int CntVisitors { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
    }

    public class BasketballContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasOne(e => e.Team1).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasOne(e => e.Team2).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-IGV9F05\SQLEXPRESS;Database=BasketballGame;Trusted_Connection=True;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
        public void CreateDbIfNotExist()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.FillData();
        }

        public void FillData()
        {
            Team avtodor = new Team { Name = "Автодор" };
            Team parma = new Team { Name = "Парма" };
            Team unix = new Team { Name = "УНИКС" };
            Team kalev = new Team { Name = "Калев" };
            this.Teams.AddRange(avtodor, parma, unix, kalev);

            Game g1 = new Game
            {
                Year = 2022,
                CntVisitors = 15000,
                Score1 = 130,
                Score2 = 100,
                Team1 = avtodor,
                Team2 = parma
            };

            Game g2 = new Game
            {
                Year = 2003,
                CntVisitors = 9000,
                Score1 = 101,
                Score2 = 99,
                Team1 = avtodor,
                Team2 = kalev
            };

            Game g3 = new Game
            {
                Year = 2020,
                CntVisitors = 20000,
                Score1 = 145,
                Score2 = 135,
                Team1 = parma,
                Team2 = kalev
            };

            Game g4 = new Game
            {
                Year = 2021,
                CntVisitors = 5000,
                Score1 = 104,
                Score2 = 95,
                Team1 = unix,
                Team2 = parma
            };

            Game g5 = new Game
            {
                Year = 2019,
                CntVisitors = 30000,
                Score1 = 170,
                Score2 = 150,
                Team1 = avtodor,
                Team2 = unix
            };

            Game g6 = new Game
            {
                Year = 2018,
                CntVisitors = 17000,
                Score1 = 101,
                Score2 = 98,
                Team1 = unix,
                Team2 = parma
            };

            Game g7 = new Game
            {
                Year = 2020,
                CntVisitors = 18000,
                Score1 = 105,
                Score2 = 100,
                Team1 = kalev,
                Team2 = avtodor
            };

            Game g8 = new Game
            {
                Year = 2017,
                CntVisitors = 7000,
                Score1 = 87,
                Score2 = 85,
                Team1 = unix,
                Team2 = parma
            };

            Game g9 = new Game
            {
                Year = 2017,
                CntVisitors = 17000,
                Score1 = 108,
                Score2 = 100,
                Team1 = avtodor,
                Team2 = unix
            };

            Game g10 = new Game
            {
                Year = 2022,
                CntVisitors = 21000,
                Score1 = 156,
                Score2 = 145,
                Team1 = parma,
                Team2 = kalev
            };

            Game g11 = new Game
            {
                Year = 2015,
                CntVisitors = 35000,
                Score1 = 170,
                Score2 = 150,
                Team1 = avtodor,
                Team2 = kalev
            };

            Game g12 = new Game
            {
                Year = 2020,
                CntVisitors = 8000,
                Score1 = 99,
                Score2 = 80,
                Team1 = unix,
                Team2 = kalev
            };

            this.Games.AddRange(g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, g11, g12);

            this.SaveChanges();
        }

        public void Task1()
        {
            Console.WriteLine("Список матчей: ");
            Console.WriteLine("Год  Команда1 : Команда2 Очки1 : Очки2");

            foreach (Game g in this.Games.OrderBy(g => g.Team1.Name).OrderByDescending(g => g.Year))
            {
                //пробелы для красивого вывода
                var spaceTeam1 = new String(' ', 8 - g.Team1.Name.Length);
                var spaceTeam2 = new String(' ', 8 - g.Team2.Name.Length);
                var spaceSc1 = new String(' ', 5 - Convert.ToString(g.Score1).Length);

                Console.WriteLine($"{g.Year} {g.Team1.Name}{spaceTeam1} : { g.Team2.Name}{spaceTeam2} {g.Score1}{spaceSc1} : {g.Score2}");
            }
        }
        public void Task2()
        {
            Console.WriteLine("");
            Console.WriteLine("3 самых многолюдных матча за всю историю: ");
            Console.WriteLine("Год  Команда1 : Команда2 Количество посетителей");
            foreach (Game g in this.Games.OrderByDescending(g => g.CntVisitors).Take(3))
            {
                //пробелы для красивого вывода
                var spaceTeam1 = new String(' ', 8 - g.Team1.Name.Length); 
                var spaceTeam2 = new String(' ', 8 - g.Team2.Name.Length); 

                Console.WriteLine($"{g.Year} {g.Team1.Name}{spaceTeam1} : {g.Team2.Name}{spaceTeam2} {g.CntVisitors}");
            }

            Console.WriteLine("");
            Console.WriteLine("3 самых многолюдных матча за последние 5 лет: ");
            Console.WriteLine("Год  Команда1 : Команда2 Количество посетителей");
            foreach (Game g in this.Games.Where(g => (g.Year >= 2018)).OrderByDescending(g => g.CntVisitors).Take(3))
            {
                //пробелы для красивого вывода
                var spaceTeam1 = new String(' ', 8 - g.Team1.Name.Length);
                var spaceTeam2 = new String(' ', 8 - g.Team2.Name.Length);

                Console.WriteLine($"{g.Year} {g.Team1.Name}{spaceTeam1} : {g.Team2.Name}{spaceTeam2} {g.CntVisitors}");
            }
        }
        public void Task3()
        {
            Console.WriteLine("");
            Console.WriteLine("Команда - число игр за последние 5 лет: ");

            foreach (Team t in this.Teams)
            {

                int cnt = this.Games.Where(g => (g.Year >= 2018) && (g.Team2.Name == t.Name || g.Team1.Name == t.Name)).Count();
                Console.WriteLine($"{t.Name} - {cnt}");
            }

        }
        public void Task5()
        {
            Console.WriteLine("");
            Console.WriteLine("Команды, отсортированные по наилучшему отношению числа выигранных к числу проигранных матчей за последние 10 лет: ");
            Console.WriteLine("Команда Выиграли Проиграли");
            foreach (Team t in this.Teams.OrderByDescending(t => this.Games.Where(g => (g.Year >= 2012) && ((g.Team2.Name == t.Name && g.Score2 > g.Score1) || (g.Team1.Name == t.Name && g.Score2 < g.Score1))).Count()
                            / this.Games.Where(g => (g.Year >= 2012) && ((g.Team2.Name == t.Name && g.Score2 < g.Score1) || (g.Team1.Name == t.Name && g.Score2 > g.Score1))).Count()))
            {
                var win = this.Games.Where(g => (g.Year >= 2012) && ((g.Team2.Name == t.Name && g.Score2 > g.Score1) || (g.Team1.Name == t.Name && g.Score2 < g.Score1))).Count();
                var lose = this.Games.Where(g => (g.Year >= 2012) && ((g.Team2.Name == t.Name && g.Score2 < g.Score1) || (g.Team1.Name == t.Name && g.Score2 > g.Score1))).Count();

                //пробелы для красивого вывода
                var spaceTeam1 = new String(' ', 7 - t.Name.Length);
                var spaceSc1 = new String(' ', 8 - Convert.ToString(win).Length);
                Console.WriteLine($"{t.Name}{spaceTeam1} {win}{spaceSc1} {lose}");
            }
        }
        public void Task6()
        {
            Console.WriteLine("");
            Console.WriteLine("Игра между активными командами со случайным счётом: ");
            var t = this.Games.OrderByDescending(g => g.Year).FirstOrDefault();
            var t1 = t.Team1;
            var t2 = t.Team2;
            var y = t.Year;
            Random rnd = new Random();
            var sc1 = rnd.Next(0, 500);
            var sc2 = rnd.Next(0, 500);
            Console.WriteLine($"{t1.Name} : {t2.Name} {sc1} : {sc2}");

            Game g13 = new Game
            {
                Year = y,
                CntVisitors = 1,
                Score1 = sc1,
                Score2 = sc2,
                Team1 = t1,
                Team2 = t2
            };

            this.Games.AddRange(g13);

            this.SaveChanges();
            Console.WriteLine("Результат игры сохранён в БД.");
        }
        public void Task7()
        {
            Console.WriteLine("");
            Console.WriteLine("Поправляем количество зрителей у самого незрелищного матча на 15%: ");

            var g = this.Games.OrderBy(g => g.Year).FirstOrDefault();
            var cntVis = g.CntVisitors;
            var newCnt = Convert.ToInt32(Convert.ToDouble(cntVis) * 1.15);

            Console.WriteLine($"Было: {cntVis} Стало: {newCnt}");

            g.CntVisitors = newCnt;
            this.SaveChanges();
            Console.WriteLine("Изменение сохранено в БД.");
        }
        public void Task8()
        {
            Console.WriteLine("");
            Console.WriteLine("Изменение названия команды с наименьшим числом сыгранных матчей: ");

            var minCnt = 100;
            var tt = new Team { Name = "" };

            foreach (Team t in this.Teams)
            {
                int cnt = this.Games.Where(g => g.Team2.Name == t.Name || g.Team1.Name == t.Name).Count();
                if (cnt < minCnt)
                {
                    tt = t;
                    minCnt = cnt;
                }
            }

            var newTeam = new string((tt.Name).Reverse().ToArray());
            Console.WriteLine($"Было: {tt.Name} Стало: {newTeam}");

            tt.Name = newTeam;
            this.SaveChanges();
            Console.WriteLine("Изменение сохранено в БД.");

        }
    }
}