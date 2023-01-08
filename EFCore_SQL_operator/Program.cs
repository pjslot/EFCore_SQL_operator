//EFCore_SQL_operator v1.0 (c) KabluchkovDS
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace EFCore_SQL_operator
{
    //СУЩНОСТИ  ===================================
   
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Brand Brand { get; set; }
    }
    
    
    
    
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
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Book> Books { get; set; }
        //public DbSet<Publisher> Publishers { get; set; }
        //public DbSet<Year> Years { get; set; }

        public DbSet<Product> Products { get; set; }


        public ApplicationContext()
        {
            //ОТКРЫВАТЬ ТОЛЬКО НА ПЕРВЫЙ ЗАПУСК ДЛЯ СОЗДАНИЯ НАЧИСТО

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BooksDb;Trusted_Connection=True;");
        }
    }
    //==============================================


    internal class Program
    {
        //методы для работы с базой

        //=========================================  ДОБАВКИ  =========================================
     

        //========================================= СОЗДАНИЕ СУЩНОСТЕЙ  =========================================
        static void DBPushData()
        {
            //СОЗДАНИЕ ЭКЗЕМПЛЯРОВ СУЩНОСТЕЙ
            using (ApplicationContext context = new ApplicationContext())
            {
                //  ЗАПУСКАТЬ ТОЛЬКО НА ПЕРВЫЙ ЗАПУСК ДЛЯ СОЗДАНИЯ НАЧИСТО

                Brand br = new Brand { Name = "Country House" };
                Product p = new Product { Name = "Milk", Brand = br };
                context.Products.Add(p);
                
                
                context.SaveChanges();

                var product = context.Products.First();
                Console.WriteLine(product.Brand.Name);
                Console.ReadLine();

            }
           
        }

        //========================================= ОСНОВНОЙ БЛОК  =========================================
        static void Main(string[] args)
        {

            DBPushData();

            //string mainInput = "";
            //do
            //{
            //    Console.Clear();
            //    Console.WriteLine("================");
            //    Console.WriteLine("Главное меню БД.");
            //    Console.WriteLine("================");
            //    Console.WriteLine("1. Добавление объектов БД.");
            //    Console.WriteLine("2. Чтение объектов БД.");
            //    Console.WriteLine("3. Удаление объектов БД.");
            //    Console.WriteLine("4. Корректировка объектов БД.");
            //    Console.WriteLine("5. Поиск по условию в БД.");
            //    Console.WriteLine("6. Статистика БД.");
            //    Console.WriteLine("0. Выход.");
            //    Console.WriteLine("_____________________________");
            //    Console.WriteLine("Введите номер пункта меню...");
            //    mainInput = Console.ReadLine();
            //    switch (mainInput)
            //    {
            //        case "1":
            //            string addInput = "";
            //            do
            //            {
            //                Console.Clear();
            //                Console.WriteLine("=============================");
            //                Console.WriteLine("Меню добавления объекта в БД.");
            //                Console.WriteLine("=============================");
            //                Console.WriteLine("1. Добавление Автора.");
            //                Console.WriteLine("2. Добавление Издателя.");
            //                Console.WriteLine("3. Добавление Года.");
            //                Console.WriteLine("4. Добавление Книги.");                            
            //                Console.WriteLine("0. Выход в главное меню.");
            //                Console.WriteLine("_____________________________");
            //                Console.WriteLine("Введите номер пункта меню...");
            //                addInput = Console.ReadLine();
            //                switch (addInput)
            //                {
            //                    case "1":
            //                    AddAuthor();
            //                    break;
            //                    case "2":
            //                    AddPublisher();
            //                    break;
            //                    case "3":
            //                    AddYear();
            //                    break;
            //                    case "4":
            //                    AddBook();
            //                    break;
            //                }
            //            } while (addInput != "0");


            //            break;
            //        case "2":
            //            string readInput = "";
            //            do
            //            {
            //                Console.Clear();
            //                Console.WriteLine("=========================");
            //                Console.WriteLine("Меню чтения объекта в БД.");
            //                Console.WriteLine("=========================");
            //                Console.WriteLine("1. Чтение Автора.");
            //                Console.WriteLine("2. Чтение Издателя.");
            //                Console.WriteLine("3. Чтение Года.");
            //                Console.WriteLine("0. Выход в главное меню.");
            //                Console.WriteLine("_____________________________");
            //                Console.WriteLine("Введите номер пункта меню...");
            //                readInput = Console.ReadLine();
            //                switch (readInput)
            //                {
            //                    case "1":
            //                        ReadAuthor();
            //                        break;
            //                    case "2":
            //                        ReadPublisher();
            //                        break;
            //                    case "3":
            //                        ReadYear();
            //                        break;                                
            //                }
            //            } while (readInput != "0");
            //            break;
            //        case "3":
            //            string deleteInput = "";
            //            do
            //            {
            //                Console.Clear();
            //                Console.WriteLine("=============================");
            //                Console.WriteLine("Меню удаления объекта из БД.");
            //                Console.WriteLine("=============================");
            //                Console.WriteLine("1. Удаление Автора.");
            //                Console.WriteLine("2. Удаление Издателя.");
            //                Console.WriteLine("3. Удаление Года.");
            //                Console.WriteLine("4. Удаление Книги.");
            //                Console.WriteLine("0. Выход в главное меню.");
            //                Console.WriteLine("_____________________________");
            //                Console.WriteLine("Введите номер пункта меню...");
            //                deleteInput = Console.ReadLine();
            //                switch (deleteInput)
            //                {
            //                    case "1":
            //                        DeleteAuthor();
            //                        break;
            //                    case "2":
            //                        DeletePublisher();
            //                        break;
            //                    case "3":
            //                        DeleteYear();
            //                        break;
            //                    case "4":
            //                        DeleteBook();
            //                        break;
            //                }
            //            } while (deleteInput != "0");
            //            break;
            //        case "4":
            //            string editInput = "";
            //            do
            //            {
            //                Console.Clear();
            //                Console.WriteLine("==========================");
            //                Console.WriteLine("Меню правки объекта из БД.");
            //                Console.WriteLine("==========================");
            //                Console.WriteLine("1. Правка Автора.");
            //                Console.WriteLine("2. Правка Издателя.");
            //                Console.WriteLine("3. Правка Года.");
            //                Console.WriteLine("4. Правка Книги.");
            //                Console.WriteLine("0. Выход в главное меню.");
            //                Console.WriteLine("_____________________________");
            //                Console.WriteLine("Введите номер пункта меню...");
            //                editInput = Console.ReadLine();
            //                switch (editInput)
            //                {
            //                    case "1":
            //                        EditAuthor();
            //                        break;
            //                    case "2":
            //                        EditPublisher();
            //                        break;
            //                    case "3":
            //                        EditYear();
            //                        break;
            //                    case "4":
            //                        EditBook();
            //                        break;
            //                }
            //            } while (editInput != "0");
            //            break;                
            //        case "5":
            //            string searchInput = "";
            //            do
            //            {
            //                Console.Clear();
            //                Console.WriteLine("============================");
            //                Console.WriteLine("Меню поиска в БД по условию.");
            //                Console.WriteLine("============================");
            //                Console.WriteLine("1. Поиск Автора с наибольшим количеством книг.");
            //                Console.WriteLine("0. Выход в главное меню.");
            //                Console.WriteLine("_____________________________");
            //                Console.WriteLine("Введите номер пункта меню...");
            //                searchInput = Console.ReadLine();
            //                switch (searchInput)
            //                {
            //                    case "1":
            //                        MaxBookAuthor();
            //                        break;                             
            //                }
            //            } while (searchInput != "0");
            //            break;
            //        case "6":
            //            DBStatistic();                      
            //            break;

            //    }

            //} while (mainInput != "0");


        }
    }
}
