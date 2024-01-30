public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();
    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true} );
        }
    
    }

    public IEnumerable<Book> TodaLaColeccion()
    {
        return librosCollection;
    }

    public IEnumerable<Book> YearFilter(int year)
    {
        
        return librosCollection.Where(p=> p.PublishedDate.Year > year);
    }

    public IEnumerable<Book> QueryYearFilter(int year)
    {
        return from l in librosCollection where l.PublishedDate.Year > year select l;
    }

    public IEnumerable<Book> PageCountFilter(int totalPage)
    {
        return librosCollection.Where(p=> p.PageCount>totalPage);
    }

    public IEnumerable<Book> TitleContains(string textContained)
    {
        return librosCollection.Where(p => p.Title.Contains(textContained));
    }

    public bool AllStatusNotNul()
    {
        return librosCollection.All(p => p.Status!= string.Empty);
    }

    public bool AnyCreationYear(int year)
    {
        return librosCollection.Any(p => p.PublishedDate.Year == year);
    }

    public IEnumerable<Book> JavaBooksASC()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderBy(p=>p.Title);
    }

    public IEnumerable<Book> OverpageSortDesc(int PageNum)
    {
        return librosCollection.Where(p=> p.PageCount>PageNum).OrderByDescending(p=>p.PageCount);
    }

    public IEnumerable<Book> TopnByDateTitle(int n , string cat)
    {
        return librosCollection
        .Where(p=> p.Categories.Contains(cat))
        .OrderByDescending(p=> p.PublishedDate)
        .Take(n);
    }

    public IEnumerable<Book> TopNMOverkPages(int totalpage, int totake, int toskip)
    {
        return librosCollection
        .Where(p=> p.PageCount > totalpage)
        .Take(totake)
        .Skip(toskip);
    }

    public IEnumerable<Book> SectTopNBooks(int n)
    {
        return librosCollection
        .Take(n)
        .Select(p=> new Book() {Title = p.Title, PageCount = p.PageCount});
    }

    public int CountByPage(int Lowpages, int Uppages)
    {
        return librosCollection.Count(p => p.PageCount>=Lowpages && p.PageCount<=Uppages);
    }

    public DateTime MinPublishedDate()
    {
        return librosCollection.Min(p => p.PublishedDate);
    }

    public DateTime MaxPublishedDate()
    {
        return librosCollection.Max(p => p.PublishedDate);
    }

    public Book RecentBook()
    {
        return librosCollection.MaxBy(p => p.PublishedDate);
    }

    public int PagesumBetween(int initial, int final)
    {
        return librosCollection.Where(p => p.PageCount >=0 && p.PageCount<= 500).Sum(p=> p.PageCount);
    }

    public string BookTitleAfterDate(int date)
    {
        return librosCollection
                .Where(p=> p.PublishedDate.Year>date)
                .Aggregate("",(BookTitle,next)=>
                {
                    if (BookTitle != string.Empty)
                    {
                        BookTitle += " - " + next.Title;
                

                    }
                    else
                    {
                        BookTitle += next.Title;
                    }
                    return BookTitle;
                });

                
    }

    public double AVGCharacterTitle()
    {
        return librosCollection.Average(p => p.Title.Length);
    }

    public IEnumerable<IGrouping<int,Book>> YearGroupedBooks(int initYear)
    {
        return librosCollection.Where(p=> p.PublishedDate.Year>= initYear).GroupBy(p=>p.PublishedDate.Year);
    }

    public ILookup<char,Book> FirstCharTitleDict()
    {
        return librosCollection.ToLookup(p=>p.Title[0], p=>p);
    }

    public IEnumerable<Book> SearchOverYearPage(int year, int page)
    {
        var overYearBooks = librosCollection.Where(p => p.PublishedDate.Year > year);
        var overPageBooks = librosCollection.Where(p => p.PageCount> page);
        
        return overYearBooks.Join(overPageBooks, left => left.Title, right => right.Title , (left,right) => left);
    }

}

