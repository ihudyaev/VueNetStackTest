using System;
using System.Collections.Generic;

namespace ihud.WebApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Topic = new HashSet<Topic>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Topic> Topic { get; set; }
    }
    /// <summary>
    /// Модель представления для фронтенда
    /// </summary>
    public partial class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TopicCount { get; set; }
        public DateTime LastNewTopic { get; set; }

    }
}
