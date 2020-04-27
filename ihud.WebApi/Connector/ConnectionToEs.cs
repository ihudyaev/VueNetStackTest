using Elasticsearch.Net;
using Nest;
using System;
using ihud.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ihud.WebApi.Connector
{
    public class ConnectionToEs
    {
        #region Connection string to connect with Elasticsearch  

        public ElasticClient EsClient()
        {
            var node = new Uri("http://localhost:9200/");

            var connectionPool = new SingleNodeConnectionPool(node);

            var connectionSettings = new ConnectionSettings(connectionPool)
                .DefaultIndex("default-index")
                .DefaultMappingFor<User>(m => m.IndexName("users"))
                .DefaultMappingFor<Topic>(m => m.IndexName("posts"))
                .DefaultMappingFor<TopicReply>(m => m.IndexName("posts"))
                .EnableHttpCompression()
                .DefaultFieldNameInferrer(p => p.ToLowerInvariant());




            //var connectionSettings = new ConnectionSettings(connectionPool).DisableDirectStreaming();
            var elasticClient = new ElasticClient(connectionSettings);
            
            return elasticClient;
        }

        //public ElasticLowLevelClient ESClientLow()
        //{
        //    var uri = new Uri("http://localhost:9200");

        //    var connectionPool = new SingleNodeConnectionPool(uri);
        //    var lowsettings = new ConnectionConfiguration(connectionPool).RequestTimeout(TimeSpan.FromMinutes(2));

        //    var elasticlowlevel = new ElasticLowLevelClient(lowsettings);
        //    return elasticlowlevel;
        //}

        #endregion Connection string to connect with Elasticsearch  
    }
}
