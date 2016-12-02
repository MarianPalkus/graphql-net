using System;
using GraphQL.Net;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _01_simple_query
{
    [TestFixture]
    public class SimpleQueryExample
    {
        class Context
        {
            public Hero Hero { get; set; }
        }

        class Hero
        {
            public string Name { get; set; }
        }
        
        [Test]
        public void RunExample()
        {
            var schema = GraphQL<Context>.CreateDefaultSchema(() => new Context { Hero = new Hero { Name = "R2-D2" } });
            schema.AddType<Hero>().AddAllFields();
            schema.AddField("hero", c => c.Hero);

            schema.Complete();

            var gql = new GraphQL<Context>(schema);
            var queryResult = gql.ExecuteQuery("{hero {name}}");
            DeepEquals(queryResult, "{hero: {name: 'R2-D2'}}");
        }

        private static readonly JsonSerializer Serializer = new JsonSerializer
        {
            Converters = { new StringEnumConverter() },
        };

        private static void DeepEquals(IDictionary<string, object> results, string json)
        {
            var expected = JObject.Parse(json);
            var actual = JObject.FromObject(results, Serializer);
            if (expected.ToString() == actual.ToString())
                return;

            throw new Exception($"Results do not match expectation:\n\nExpected:\n{expected}\n\nActual:\n{actual}");
        }
    }
}
