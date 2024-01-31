public class LinqQueries
{
    // Creating book Collection
    private List<Book> librosCollection = new List<Book>();
    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true} );
        }
    
    }

    // extraction for whole Data
    public IEnumerable<Book> TodaLaColeccion()
    {
        return librosCollection;
    }

    // Where implementation filter by year
    public IEnumerable<Book> YearFilter(int year)
    {
        
        return librosCollection.Where(p=> p.PublishedDate.Year > year);
    }

 
     // Where implementation filter by year using Query declaration  
    public IEnumerable<Book> QueryYearFilter(int year)
    {
        return from l in librosCollection where l.PublishedDate.Year > year select l;
    }


    // Where implementation filter by page amount
    public IEnumerable<Book> PageCountFilter(int totalPage)
    {
        return librosCollection.Where(p=> p.PageCount>totalPage);
    }


    // Where implementation filter by searching by book title
    public IEnumerable<Book> TitleContains(string textContained)
    {
        return librosCollection.Where(p => p.Title.Contains(textContained));
    }

    // All implementation shows if every book has a status
    public bool AllStatusNotNul()
    {
        return librosCollection.All(p => p.Status!= string.Empty);
    }

    // Any implementation shows if any book has an specific published year
    public bool AnyCreationYear(int year)
    {
        return librosCollection.Any(p => p.PublishedDate.Year == year);
    }

    // OrderBy implementation, ordering all java books by title
    public IEnumerable<Book> JavaBooksASC()
    {
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderBy(p=>p.Title);
    }

    // OrderByDescending implementation searching pages over provided amount of pages ordering descending
    public IEnumerable<Book> OverpageSortDesc(int PageNum)
    {
        return librosCollection.Where(p=> p.PageCount>PageNum).OrderByDescending(p=>p.PageCount);
    }

    // Take implementation seach last n books by Category provided
    public IEnumerable<Book> TopnByDateTitle(int n , string cat)
    {
        return librosCollection
        .Where(p=> p.Categories.Contains(cat))
        .OrderByDescending(p=> p.PublishedDate)
        .Take(n);
    }

    // Skip implementation seaching over total pages, the first n books and skip
    public IEnumerable<Book> TopNMOverkPages(int totalpage, int totake, int toskip)
    {
        return librosCollection
        .Where(p=> p.PageCount > totalpage)
        .Take(totake)
        .Skip(toskip);
    }

    // Select implementation, over the first n books to show only the title an the page count
    public IEnumerable<Book> SectTopNBooks(int n)
    {
        return librosCollection
        .Take(n)
        .Select(p=> new Book() {Title = p.Title, PageCount = p.PageCount});
    }

    // Count implementation to check the amount of books between a given range of pages
    public int CountByPage(int Lowpages, int Uppages)
    {
        return librosCollection.Count(p => p.PageCount>=Lowpages && p.PageCount<=Uppages);
    }

    // Min implementation to find the oldest date of the library books
    public DateTime MinPublishedDate()
    {
        return librosCollection.Min(p => p.PublishedDate);
    }

    // Max implementation to find the newest date of the library books
    public DateTime MaxPublishedDate()
    {
        return librosCollection.Max(p => p.PublishedDate);
    }

    // MaxBy implementation to find the newest book in the libary
    public Book RecentBook()
    {
        return librosCollection.MaxBy(p => p.PublishedDate);
    }

    // Sum implementation to find the amount of pages form the books between a range of pages provided
    public int PagesumBetween(int initial, int final)
    {
        return librosCollection.Where(p => p.PageCount >=0 && p.PageCount<= 500).Sum(p=> p.PageCount);
    }

    // Aggregate implementation that returns a string with all the book titles that has a published date after a given date
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

    // Average implementation that returns the average title lenght of the books in the library
    public double AVGCharacterTitle()
    {
        return librosCollection.Average(p => p.Title.Length);
    }

    // GrouopBy implementation that returns the books from the given initial year grouped by year 
    public IEnumerable<IGrouping<int,Book>> YearGroupedBooks(int initYear)
    {
        return librosCollection.Where(p=> p.PublishedDate.Year>= initYear).GroupBy(p=>p.PublishedDate.Year);
    }

    // ToLookup implementation that returns books grouped by the first title character into a dictionary
    public ILookup<char,Book> FirstCharTitleDict()
    {
        return librosCollection.ToLookup(p=>p.Title[0], p=>p);
    }

    // Join implementation that returns the books published after given date that also contains more than the given amount of pages
    public IEnumerable<Book> SearchOverYearPage(int year, int page)
    {
        var overYearBooks = librosCollection.Where(p => p.PublishedDate.Year > year);
        var overPageBooks = librosCollection.Where(p => p.PageCount> page);
        
        return overYearBooks.Join(overPageBooks, left => left.Title, right => right.Title , (left,right) => left);
    }

}

