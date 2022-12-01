using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore_SQL_operator
{
    //СУЩНОСТИ  ===================================
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        // Навигационные свойства
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public Year Year { get; set; }
    }
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        // Навигационное свойство
        public List<Book> Books { get; set; }
    }
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        // Навигационное свойство
        public List<Book> Books { get; set; }
    }
    public class Year
    {
        public int YearId { get; set; }
        public string YearName { get; set; }
        // Навигационное свойство
        public List<Book> Books { get; set; }
    }
    //==============================================

    //КЛАСС КОНТЕКСТА
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Year> Years { get; set; }

        public ApplicationContext()
        {
            //ОТКРЫВАТЬ ТОЛЬКО НА ПЕРВЫЙ ЗАПУСК ДЛЯ СОЗДАНИЯ НАЧИСТО

            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BooksDb;Trusted_Connection=True;");
        }
    }
    //==============================================


    internal class Program
    {

        static void Main(string[] args)
        {
            //СОЗДАНИЕ ЭКЗЕМПЛЯРОВ СУЩНОСТЕЙ
            using (ApplicationContext context = new ApplicationContext())
            {
                //ОТКРЫВАТЬ ТОЛЬКО НА ПЕРВЫЙ ЗАПУСК ДЛЯ СОЗДАНИЯ НАЧИСТО

                //Author a1 = new Author { AuthorName = "Sregey Lukianenko" };
                //Author a2 = new Author { AuthorName = "Nik Perumov" };
                //Author a3 = new Author { AuthorName = "Vladimir Vasilyev" };
                //Author a4 = new Author { AuthorName = "Steven King" };
                //context.Authors.AddRange(a1, a2, a3, a4);

                //Publisher p1 = new Publisher { PublisherName = "OOO BHV" };
                //Publisher p2 = new Publisher { PublisherName = "OAO Eksmo" };
                //Publisher p3 = new Publisher { PublisherName = "OOO Rosman" };
                //Publisher p4 = new Publisher { PublisherName = "OAO Fenix" };
                //context.Publishers.AddRange(p1, p2, p3, p4);

                //Year y1 = new Year { YearName = "2001" };
                //Year y2 = new Year { YearName = "2002" };
                //Year y3 = new Year { YearName = "2003" };
                //Year y4 = new Year { YearName = "2004" };
                //context.Years.AddRange(y1, y2, y3, y4);

                //Book b1 = new Book { Author = a1, Publisher = p1, Year = y1, BookName = "Luk Book One" };
                //Book b2 = new Book { Author = a1, Publisher = p2, Year = y2, BookName = "Luk Book Two" };
                //Book b3 = new Book { Author = a2, Publisher = p2, Year = y3, BookName = "Nik Book One" };
                //Book b4 = new Book { Author = a2, Publisher = p3, Year = y4, BookName = "Nik Book Two" };
                //Book b5 = new Book { Author = a3, Publisher = p3, Year = y1, BookName = "Vlad Book One" };
                //Book b6 = new Book { Author = a3, Publisher = p4, Year = y2, BookName = "Vlad Book Two" };
                //Book b7 = new Book { Author = a4, Publisher = p4, Year = y3, BookName = "King Book One" };
                //Book b8 = new Book { Author = a4, Publisher = p1, Year = y4, BookName = "King Book Two" };
                //context.Books.AddRange(b1, b2, b3, b4, b5, b6, b7, b8);
                //context.SaveChanges();

            }
            //==============================================

            //ДОБАВКА АВТОРА
            //using (ApplicationContext context = new ApplicationContext())
            //{
            //    Console.Clear();
            //    Console.WriteLine("Введите нового автора:");
            //    string authorNameInput = Console.ReadLine();
            //    //проверка не дублирует ли автор сущестующего
            //    if (context.Authors.Any(a => a.AuthorName == authorNameInput))
            //    {
            //        Console.WriteLine("Такой автор уже существует. Добавление в базу не совершено. Press any key.");
            //        Console.ReadKey();
            //    }
            //    else
            //    {
            //        Author aNew = new Author { AuthorName = authorNameInput };
            //        context.Authors.AddRange(aNew);
            //        context.SaveChanges();
            //        Console.WriteLine("Автор добавлен в базу. Press any key.");
            //        Console.ReadKey();
            //    }
            //}

            //УДАЛЕНИЕ АВТОРА
            //using (ApplicationContext context = new ApplicationContext())
            //{
            //    Console.Clear();
            //    Console.WriteLine("Введите имя автора для удаления:");
            //    string authorNameInput = Console.ReadLine();
            //    //проверка есть ли такой автор?
            //    if (context.Authors.Any(a => a.AuthorName == authorNameInput))
            //    {
            //       // Author aNew = new Author { AuthorName = authorNameInput };
            //        context.Authors.RemoveRange(context.Authors.Where(a=>a.AuthorName.Contains(authorNameInput)));
            //        context.SaveChanges();
            //        Console.WriteLine("Автор удалён из базы. Press any key.");
            //        Console.ReadKey();                    
            //    }
            //    else
            //    {
            //        Console.WriteLine("Такого автора нет. Удаление из базы не выполнено. Press any key.");
            //        Console.ReadKey();
            //    }
            //}

            //ЧТЕНИЕ АВТОРА
            //using (ApplicationContext context = new ApplicationContext())
            //{
            //    Console.Clear();
            //    Console.WriteLine("Введите имя автора для вывода информации:");
            //    string authorNameInput = Console.ReadLine();
            //    //проверка если такой автор существует
            //    if (context.Authors.Any(a => a.AuthorName == authorNameInput))
            //    {
            //        List<Book> authorsBooks = new List<Book>();
            //        authorsBooks = context.Authors.Include("Books").FirstOrDefault(a => a.AuthorName.Contains(authorNameInput)).Books;
            //        foreach (var b in authorsBooks) Console.WriteLine($"Книга номер {b.BookId}, название {b.BookName}");
            //        Console.WriteLine("Вывод закончен. Press any key.");
            //        Console.ReadKey();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Автор не найден. Press any key.");
            //        Console.ReadKey();
            //    }
            //}

            //ПРАВКА АВТОРА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя автора для правки:");
                string authorNameInput = Console.ReadLine();
                //проверка если такой автор существует
                if (context.Authors.Any(a => a.AuthorName == authorNameInput))
                {
                    Console.WriteLine("Введите новое имя автора:");
                    string newauthorNameInput = Console.ReadLine();
                    Author authorToChange = context.Authors.FirstOrDefault(a => a.AuthorName.Contains(authorNameInput));
                    authorToChange.AuthorName = newauthorNameInput;
                    context.Authors.Update(authorToChange);
                    context.SaveChanges();
                    Console.WriteLine("Правка внесена. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Автор не найден. Press any key.");
                    Console.ReadKey();
                }
            }


            //ОБЩАЯ СТАТИСТИКА ПО БАЗЕ
            using (ApplicationContext context = new ApplicationContext())
            {
                //статистика по таблицам
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("Статистика по Базе.");
                Console.WriteLine("=============================");
                Console.WriteLine("Всего авторов:" + context.Authors.Count());
                Console.WriteLine("==============");
                var allAuthors = context.Authors.ToList();
                foreach (var a in allAuthors) Console.WriteLine(a.AuthorName);                         
                Console.WriteLine("=============================");
               
                Console.WriteLine("Всего издателей:" + context.Publishers.Count());
                Console.WriteLine("================");
                var allPublishers = context.Publishers.ToList();
                foreach (var p in allPublishers) Console.WriteLine(p.PublisherName);
                Console.WriteLine("=============================");

                Console.WriteLine("Всего годов выпуска:" + context.Years.Count());
                Console.WriteLine("====================");
                var allYears = context.Years.ToList();
                foreach (var y in allYears) Console.WriteLine(y.YearName);
                Console.WriteLine("=============================");

                Console.WriteLine("Всего книг:" + context.Books.Count());
                Console.WriteLine("===========");
                var allBooks = context.Books.ToList();
                foreach (var b in allBooks) Console.WriteLine(b.BookName);
                Console.WriteLine("=============================");
                //статистика по среднему количеству               
                Console.WriteLine("Среднее количество книг на автора: " + (double)context.Books.Count() / (double)context.Authors.Count());
                Console.WriteLine("=============================");
                Console.WriteLine("Среднее количество книг на издательство: " + (double)context.Books.Count() / (double)context.Publishers.Count());
                Console.WriteLine("=============================");                
            }


           


           
        }
    }
}
