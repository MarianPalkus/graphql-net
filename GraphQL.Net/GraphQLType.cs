﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GraphQL.Parser;

namespace GraphQL.Net
{
    internal class GraphQLType : IGraphQLType
    {
        public GraphQLType(Type type)
        {
            CLRType = type;
            Name = type.Name;
            Fields = new List<GraphQLField>();
            PossibleTypes = new List<GraphQLType>();
            Interfaces = new List<GraphQLType>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<GraphQLField> Fields { get; set; }
        public List<GraphQLType> PossibleTypes { get; set; }
        public List<GraphQLType> Interfaces { get; set; }
        public Type CLRType { get; set; }
        public Type QueryType { get; set; }
        public TypeKind TypeKind { get; set; }

        public IEnumerable<GraphQLField> GetQueryFields()
        {
            return Fields;
        }
    }
}