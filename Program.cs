
LinqQueries queries = new LinqQueries();

//ImprimirValores(queries.TodaLaColeccion());

//ImprimirValores(queries.QueryYearFilter(2010));

//ImprimirValores(queries.TitleContains("Python"));
//Console.WriteLine($" Todos los libros tienen estatus? -  {queries.AllStatusNotNul()}");

//var year = 1978;
//Console.WriteLine($" Algun libro publicado en {year} ? -  {queries.AnyCreationYear(year)}");

//ImprimirValores(queries.JavaBooksASC());
//ImprimirValores(queries.OverpageSortDesc(450));
//ImprimirValores(queries.TopnByDateTitle(3,"Java"));
//ImprimirValores(queries.TopNMOverkPages(400,4,2));

//ImprimirValores(queries.SectTopNBooks(10));

// var libro = queries.RecentBook();

// Console.WriteLine($"{libro.Title} - {libro.PublishedDate}");

// var initial = 0;
// var final = 500;
// var total = queries.PagesumBetween(0,500);

// Console.WriteLine($"total paginas entre libros de entre {initial} y {final} paginas - {total}");

//Console.WriteLine(queries.BookTitleAfterDate(2015));

//Console.WriteLine($"El promedio de los caracteres de los titulos de los libros es {queries.AVGCharacterTitle()}");

//ImprimirGrupo(queries.YearGroupedBooks(2000));

// var lookupDict = queries.FirstCharTitleDict();
// ImprimirDict(lookupDict, 'S');

ImprimirValores(queries.SearchOverYearPage(2005,500));

void ImprimirValores(IEnumerable<Book> listdelibros)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach(var item in listdelibros)
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}

void ImprimirGrupo(IEnumerable<IGrouping<int,Book>> ListadeLibros)
{
    foreach(var grupo in ListadeLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: {grupo.Key}");
        Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach(var item in grupo)
        {
            Console.WriteLine("{0,-60} {1,15} {2,15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
        }


    }

}

void ImprimirDict(ILookup<char,Book> BookList, char firstChar)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach(var item in BookList[firstChar])
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}