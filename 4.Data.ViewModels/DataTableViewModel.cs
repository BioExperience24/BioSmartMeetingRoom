using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class DataTableViewModel
    {
        [FromQuery(Name = "draw")]
        public int Draw { get; set; }

        [FromQuery(Name = "columns")]
        public List<DataTableColumnQuery>? Columns { get; set; }

        [FromQuery(Name = "order")]
        public List<DataTableOrderQuery>? Order { get; set; }

        [FromQuery(Name = "start")]
        public int Start { get; set; }

        [FromQuery(Name = "length")]
        public int Length { get; set; }

        [FromQuery(Name = "search_value")]
        public string? SearchValue { get; set; }

        [FromQuery(Name = "search_regex")]
        public bool SearchRegex { get; set; }
    }

    public class DataTableColumnQuery
    {
        [FromQuery(Name = "data")]
        public string? Data { get; set; }

        [FromQuery(Name = "name")]
        public string? Name { get; set; }

        [FromQuery(Name = "searchable")]
        public bool Searchable { get; set; }

        [FromQuery(Name = "orderable")]
        public bool Orderable { get; set; }

        [FromQuery(Name = "search_value")]
        public string? SearchValue { get; set; }

        [FromQuery(Name = "search_regex")]
        public bool SearchRegex { get; set; }
    }

    public class DataTableOrderQuery
    {
        [FromQuery(Name = "column")]
        public int Column { get; set; }

        [FromQuery(Name = "dir")]
        public string? Dir { get; set; }
    }

    public class DataTableResponse
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object? Data { get; set; }
    }

    public class DataTableVM
    {
        public int RecordsTotal { get; set; } 
        public int RecordsFiltered { get; set; }
    }
}