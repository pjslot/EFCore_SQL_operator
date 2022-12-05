using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

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
        //методы для работы с базой

        //=========================================  ДОБАВКИ  =========================================
        static void AddAuthor()
        {
            //***ДОБАВКА АВТОРА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите нового автора:");
                string authorNameInput = Console.ReadLine();
                //проверка не дублирует ли автор сущестующего
                if (context.Authors.Any(a => a.AuthorName == authorNameInput))
                {
                    Console.WriteLine("Такой автор уже существует. Добавление в базу не совершено. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Author aNew = new Author { AuthorName = authorNameInput };
                    context.Authors.AddRange(aNew);
                    context.SaveChanges();
                    Console.WriteLine("Автор добавлен в базу. Press any key.");
                    Console.ReadKey();
                }
            }
        }
        static void AddPublisher()
        {
            //***ДОБАВКА ИЗДАТЕЛЯ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите нового издателя:");
                string publisherNameInput = Console.ReadLine();
                //проверка не дублирует ли
                if (context.Publishers.Any(a => a.PublisherName == publisherNameInput))
                {
                    Console.WriteLine("Такой издатель уже существует. Добавление в базу не совершено. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Publisher aNew = new Publisher { PublisherName = publisherNameInput };
                    context.Publishers.AddRange(aNew);
                    context.SaveChanges();
                    Console.WriteLine("Издатель добавлен в базу. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void AddYear()
        {
            //***ДОБАВКА ГОДА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите новый год:");
                string yearNameInput = Console.ReadLine();
                //проверка не дублирует ли
                if (context.Years.Any(a => a.YearName == yearNameInput))
                {
                    Console.WriteLine("Такой год уже существует. Добавление в базу не совершено. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Year aNew = new Year { YearName = yearNameInput };
                    context.Years.AddRange(aNew);
                    context.SaveChanges();
                    Console.WriteLine("Год добавлен в базу. Press any key.");
                    Console.ReadKey();
                }
            }
        }
        static void AddBook()
        {
            //***ДОБАВКА КНИГИ  
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите название новой книги:");
                string bookNameInput = Console.ReadLine();
                //проверка не дублирует ли
                if (context.Books.Any(a => a.BookName == bookNameInput))
                {
                    Console.WriteLine("Такая книга уже существует. Добавление в базу не совершено. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Book aNew = new Book { BookName = bookNameInput};
                    context.Books.AddRange(aNew);
                    context.SaveChanges();
                    Console.WriteLine("Книга добавлена в базу. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        //=========================================  УДАЛЕНИЯ  =========================================

        static void DeleteAuthor()
        {
            //***УДАЛЕНИЕ АВТОРА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя автора для удаления:");
                string authorNameInput = Console.ReadLine();
                //проверка есть ли такой автор?
                if (context.Authors.Any(a => a.AuthorName == authorNameInput))
                {
                    context.Authors.RemoveRange(context.Authors.Where(a => a.AuthorName.Contains(authorNameInput)));
                    context.SaveChanges();
                    Console.WriteLine("Автор удалён из базы. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Такого автора нет. Удаление из базы не выполнено. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void DeletePublisher()
        {
            //***УДАЛЕНИЕ ИЗДАТЕЛЯ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя издателя для удаления:");
                string publisherNameInput = Console.ReadLine();
                //проверка есть ли такой?
                if (context.Publishers.Any(a => a.PublisherName == publisherNameInput))
                {
                    context.Publishers.RemoveRange(context.Publishers.Where(a => a.PublisherName.Contains(publisherNameInput)));
                    context.SaveChanges();
                    Console.WriteLine("Издатель удалён из базы. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Такого издателя нет. Удаление из базы не выполнено. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void DeleteYear()
        {
            //***УДАЛЕНИЕ ГОДА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя года для удаления:");
                string yearNameInput = Console.ReadLine();
                //проверка есть ли такой?
                if (context.Years.Any(a => a.YearName == yearNameInput))
                {
                    context.Years.RemoveRange(context.Years.Where(a => a.YearName.Contains(yearNameInput)));
                    context.SaveChanges();
                    Console.WriteLine("Год удалён из базы. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Такого года нет. Удаление из базы не выполнено. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void DeleteBook()
        {
            //***УДАЛЕНИЕ КНИГИ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя книги для удаления:");
                string bookNameInput = Console.ReadLine();
                //проверка есть ли такой?
                if (context.Books.Any(a => a.BookName == bookNameInput))
                {
                    context.Books.RemoveRange(context.Books.Where(a => a.BookName.Contains(bookNameInput)));
                    context.SaveChanges();
                    Console.WriteLine("Книга удалена из базы. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Такой книги нет. Удаление из базы не выполнено. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        //=========================================  ЧТЕНИЯ  =========================================

        static void ReadAuthor()
        {
            //***ЧТЕНИЕ АВТОРА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите имя автора для вывода информации по его книгам:");
                string authorNameInput = Console.ReadLine();
                //проверка если такой автор существует
                if (context.Authors.Any(a => a.AuthorName == authorNameInput))
                {
                    List<Book> authorsBooks = new List<Book>();
                    authorsBooks = context.Authors.Include("Books").FirstOrDefault(a => a.AuthorName.Contains(authorNameInput)).Books;
                    foreach (var b in authorsBooks) Console.WriteLine($"Книга номер {b.BookId}, название {b.BookName}");
                    Console.WriteLine("Вывод закончен. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Автор не найден. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void ReadPublisher()
        {
            //***ЧТЕНИЕ ИЗДАТЕЛЯ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите название издателя для вывода информации по его книгам:");
                string publisherNameInput = Console.ReadLine();
                //проверка если такой 
                if (context.Publishers.Any(a => a.PublisherName == publisherNameInput))
                {
                    List<Book> publishersBooks = new List<Book>();
                    publishersBooks = context.Publishers.Include("Books").FirstOrDefault(a => a.PublisherName.Contains(publisherNameInput)).Books;
                    foreach (var b in publishersBooks) Console.WriteLine($"Книга номер {b.BookId}, название {b.BookName}");
                    Console.WriteLine("Вывод закончен. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Издатель не найден. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void ReadYear()
        {
            //***ЧТЕНИЕ ГОДА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите номер года для получения информации по книгам выпущенным в этом году:");
                string yearNameInput = Console.ReadLine();
                //проверка если такой 
                if (context.Years.Any(a => a.YearName == yearNameInput))
                {
                    List<Book> yearsBooks = new List<Book>();
                    yearsBooks = context.Years.Include("Books").FirstOrDefault(a => a.YearName.Contains(yearNameInput)).Books;
                    foreach (var b in yearsBooks) Console.WriteLine($"Книга номер {b.BookId}, название {b.BookName}");
                    Console.WriteLine("Вывод закончен. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Год не найден. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        //=========================================  ПРАВКИ  =========================================
        static void EditAuthor()
        {
            //***ПРАВКА АВТОРА
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
        }

        static void EditPublisher()
        {
            //***ПРАВКА ИЗДАТЕЛЯ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите название издателя для правки:");
                string publisherNameInput = Console.ReadLine();
                //проверка есть ли такой 
                if (context.Publishers.Any(a => a.PublisherName == publisherNameInput))
                {
                    Console.WriteLine("Введите новое название издательства:");
                    string newpublisherNameInput = Console.ReadLine();
                    Publisher publisherToChange = context.Publishers.FirstOrDefault(a => a.PublisherName.Contains(publisherNameInput));
                    publisherToChange.PublisherName = newpublisherNameInput;
                    context.Publishers.Update(publisherToChange);
                    context.SaveChanges();
                    Console.WriteLine("Правка внесена. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Издатель не найден. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void EditYear()
        {
            //***ПРАВКА ГОДА
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите номер года для правки:");
                string yearNameInput = Console.ReadLine();
                //проверка есть ли такой 
                if (context.Years.Any(a => a.YearName == yearNameInput))
                {
                    Console.WriteLine("Введите новый номер года:");
                    string newyearNameInput = Console.ReadLine();
                    Year yearToChange = context.Years.FirstOrDefault(a => a.YearName.Contains(yearNameInput));
                    yearToChange.YearName = newyearNameInput;
                    context.Years.Update(yearToChange);
                    context.SaveChanges();
                    Console.WriteLine("Правка внесена. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Год не найден. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        static void EditBook()
        {
            //***ПРАВКА КНИГИ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Введите название книги для правки:");
                string bookNameInput = Console.ReadLine();
                //проверка есть ли такой 
                if (context.Books.Any(a => a.BookName == bookNameInput))
                {
                    Console.WriteLine("Введите новое название книги:");
                    string newbookNameInput = Console.ReadLine();
                    Book bookToChange = context.Books.FirstOrDefault(a => a.BookName.Contains(bookNameInput));
                    bookToChange.BookName = newbookNameInput;
                    context.Books.Update(bookToChange);
                    context.SaveChanges();
                    Console.WriteLine("Правка внесена. Press any key.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Книга не найдена. Press any key.");
                    Console.ReadKey();
                }
            }
        }

        //=========================================  ПОИСК  =========================================
        static void MaxBookAuthor()
        {
            //***ПОИСК АВТОРА С НАИБОЛЬШИМ КОЛИЧЕСТВОМ КНИГ
            using (ApplicationContext context = new ApplicationContext())
            {
                Console.Clear();
                Console.WriteLine("Автор с наибольшим количеством книг:");

                //поиск автора с наибольшим количеством книг:
                Author maxBookAuthor = (from a in context.Authors
                                        orderby -a.Books.Count
                                        select a).First();

                Console.WriteLine(maxBookAuthor.AuthorName);
                Console.WriteLine("Вывод закончен. Press any key.");
                Console.ReadKey();
            }
        }

        //=========================================  СТАТИСТИКА  =========================================

        static void DBStatistic()
        {
            //ОБЩАЯ СТАТИСТИКА ПО БАЗЕ
            using (ApplicationContext context = new ApplicationContext())
            {
                //статистика по таблицам
                Console.Clear();
                Console.WriteLine("=============================");
                Console.WriteLine("Статистика по Базе.");
                Console.WriteLine("=============================");
                Console.WriteLine("Всего авторов:" + context.Authors.Count());
                Console.WriteLine("_____________________________");
                var allAuthors = context.Authors.ToList();
                foreach (var a in allAuthors) Console.WriteLine(a.AuthorName);
                Console.WriteLine("=============================");

                Console.WriteLine("Всего издателей:" + context.Publishers.Count());
                Console.WriteLine("_____________________________");
                var allPublishers = context.Publishers.ToList();
                foreach (var p in allPublishers) Console.WriteLine(p.PublisherName);
                Console.WriteLine("=============================");

                Console.WriteLine("Всего годов выпуска:" + context.Years.Count());
                Console.WriteLine("_____________________________");
                var allYears = context.Years.ToList();
                foreach (var y in allYears) Console.WriteLine(y.YearName);
                Console.WriteLine("=============================");

                Console.WriteLine("Всего книг:" + context.Books.Count());
                Console.WriteLine("_____________________________");
                var allBooks = context.Books.ToList();
                foreach (var b in allBooks) Console.WriteLine(b.BookName);
                Console.WriteLine("=============================");
                //статистика по среднему количеству               
                Console.WriteLine("Среднее количество книг на автора: " + (double)context.Books.Count() / (double)context.Authors.Count());
                Console.WriteLine("_____________________________");
                Console.WriteLine("Среднее количество книг на издательство: " + (double)context.Books.Count() / (double)context.Publishers.Count());
                Console.WriteLine("_____________________________");
            }
        }

        //========================================= СОЗДАНИЕ СУЩНОСТЕЙ  =========================================
        static void DBPushData()
        {
            //СОЗДАНИЕ ЭКЗЕМПЛЯРОВ СУЩНОСТЕЙ
            using (ApplicationContext context = new ApplicationContext())
            {
              //  ЗАПУСКАТЬ ТОЛЬКО НА ПЕРВЫЙ ЗАПУСК ДЛЯ СОЗДАНИЯ НАЧИСТО

                Author a1 = new Author { AuthorName = "Sregey Lukianenko" };
                Author a2 = new Author { AuthorName = "Nik Perumov" };
                Author a3 = new Author { AuthorName = "Vladimir Vasilyev" };
                Author a4 = new Author { AuthorName = "Steven King" };
                context.Authors.AddRange(a1, a2, a3, a4);

                Publisher p1 = new Publisher { PublisherName = "OOO BHV" };
                Publisher p2 = new Publisher { PublisherName = "OAO Eksmo" };
                Publisher p3 = new Publisher { PublisherName = "OOO Rosman" };
                Publisher p4 = new Publisher { PublisherName = "OAO Fenix" };
                context.Publishers.AddRange(p1, p2, p3, p4);

                Year y1 = new Year { YearName = "2001" };
                Year y2 = new Year { YearName = "2002" };
                Year y3 = new Year { YearName = "2003" };
                Year y4 = new Year { YearName = "2004" };
                context.Years.AddRange(y1, y2, y3, y4);

                Book b1 = new Book { Author = a1, Publisher = p1, Year = y1, BookName = "Luk Book One" };
                Book b2 = new Book { Author = a1, Publisher = p2, Year = y2, BookName = "Luk Book Two" };
                Book b3 = new Book { Author = a2, Publisher = p2, Year = y3, BookName = "Nik Book One" };
                Book b4 = new Book { Author = a2, Publisher = p3, Year = y4, BookName = "Nik Book Two" };
                Book b5 = new Book { Author = a3, Publisher = p3, Year = y1, BookName = "Vlad Book One" };
                Book b6 = new Book { Author = a3, Publisher = p4, Year = y2, BookName = "Vlad Book Two" };
                Book b7 = new Book { Author = a4, Publisher = p4, Year = y3, BookName = "King Book One" };
                Book b8 = new Book { Author = a4, Publisher = p1, Year = y4, BookName = "King Book Two" };
                Book b9 = new Book { Author = a2, Publisher = p4, Year = y1, BookName = "Nik Book Three" };
                context.Books.AddRange(b1, b2, b3, b4, b5, b6, b7, b8, b9);
                context.SaveChanges();

            }
           
        }

        //========================================= ОСНОВНОЙ БЛОК  =========================================
        static void Main(string[] args)
        {
            string mainInput = "";
            do
            {
                Console.Clear();
                Console.WriteLine("================");
                Console.WriteLine("Главное меню БД.");
                Console.WriteLine("================");
                Console.WriteLine("1. Добавление объектов БД.");
                Console.WriteLine("2. Чтение объектов БД.");
                Console.WriteLine("3. Удаление объектов БД.");
                Console.WriteLine("4. Корректировка объектов БД.");
                Console.WriteLine("5. Поиск по условию в БД.");
                Console.WriteLine("6. Статистика БД.");
                Console.WriteLine("0. Выход.");
                Console.WriteLine("_____________________________");
                Console.WriteLine("Введите номер пункта меню...");
                mainInput = Console.ReadLine();
                switch (mainInput)
                {
                    case "1":
                        string addInput = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("=============================");
                            Console.WriteLine("Меню добавления объекта в БД.");
                            Console.WriteLine("=============================");
                            Console.WriteLine("1. Добавление Автора.");
                            Console.WriteLine("2. Добавление Издателя.");
                            Console.WriteLine("3. Добавление Года.");
                            Console.WriteLine("4. Добавление Книги.");                            
                            Console.WriteLine("0. Выход в главное меню.");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("Введите номер пункта меню...");
                            addInput = Console.ReadLine();
                            switch (addInput)
                            {
                                case "1":
                                AddAuthor();
                                break;
                                case "2":
                                AddPublisher();
                                break;
                                case "3":
                                AddYear();
                                break;
                                case "4":
                                AddBook();
                                break;
                            }
                        } while (addInput != "0");


                        break;
                    case "2":
                        string readInput = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("=========================");
                            Console.WriteLine("Меню чтения объекта в БД.");
                            Console.WriteLine("=========================");
                            Console.WriteLine("1. Чтение Автора.");
                            Console.WriteLine("2. Чтение Издателя.");
                            Console.WriteLine("3. Чтение Года.");
                            Console.WriteLine("0. Выход в главное меню.");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("Введите номер пункта меню...");
                            readInput = Console.ReadLine();
                            switch (readInput)
                            {
                                case "1":
                                    ReadAuthor();
                                    break;
                                case "2":
                                    ReadPublisher();
                                    break;
                                case "3":
                                    ReadYear();
                                    break;                                
                            }
                        } while (readInput != "0");
                        break;
                    case "3":
                        string deleteInput = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("=============================");
                            Console.WriteLine("Меню удаления объекта из БД.");
                            Console.WriteLine("=============================");
                            Console.WriteLine("1. Удаление Автора.");
                            Console.WriteLine("2. Удаление Издателя.");
                            Console.WriteLine("3. Удаление Года.");
                            Console.WriteLine("4. Удаление Книги.");
                            Console.WriteLine("0. Выход в главное меню.");
                            Console.WriteLine("_____________________________");
                            Console.WriteLine("Введите номер пункта меню...");
                            deleteInput = Console.ReadLine();
                            switch (deleteInput)
                            {
                                case "1":
                                    DeleteAuthor();
                                    break;
                                case "2":
                                    DeletePublisher();
                                    break;
                                case "3":
                                    DeleteYear();
                                    break;
                                case "4":
                                    DeleteBook();
                                    break;
                            }
                        } while (deleteInput != "0");
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("пункт корректировок");
                        Console.ReadKey();
                        break;                
                    case "5":
                        Console.Clear();
                        Console.WriteLine("пункт поиска по условию");
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("пункт статистики");
                        Console.ReadKey();
                        break;

                }

            } while (mainInput != "0");












        }
    }
}
