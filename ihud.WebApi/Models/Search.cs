using System;
using System.Collections.Generic;

namespace ihud.WebApi.Models
{
    public partial class SearchResult
    {
      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
    /// <summary>
    /// Модель представления для фронтенда
    /// </summary>
    public partial class SearchIndex
    {
        public string uriInd { get; set; }
        public string Description { get; set; }
        public string TypeOf { get; set; }


    }
}
