using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ihud.WebApi.Models
{
    public partial class ESSearchItem
    {
        public int Id { get; set; }
        public int? RootId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }

        public ESSearchItem (Category cat)
        {
            Id = cat.Id;
            Title = cat.Title;
            Description = cat.Description;
            Type = "Category";
        }
        public ESSearchItem( Topic tpc )
        {
            Id = tpc.Id;
            RootId = tpc.CategoryId;
            Title = tpc.Title;
            Description = tpc.Description;
            Type = "Topic";
        }
        public ESSearchItem( TopicReply tpcr )
        {
            Id = tpcr.Id;
            RootId = tpcr.TopicId;
            Title = "";
            Description = tpcr.Description;
            Type = "TopicReply";
        }
    }
    /// <summary>
    /// Класс для Post запроса
    /// </summary>
    [JsonObject(Title = "str")]
    public partial class ForumSearchRequest
    {
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
        [JsonProperty(PropertyName = "searchstring")]
        public string SearchString { get; set; }
    }



}
