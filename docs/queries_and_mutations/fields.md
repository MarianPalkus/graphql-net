# Fields
Let's look at the following query:

```
{
  hero {
    name
    # Queries can have comments!
    friends {
      name
    }
  }
}
```
The expected result looks as follows:
```json
{
  "data": {
    "hero": {
      "name": "R2-D2",
      "friends": [
        {
          "name": "Luke Skywalker"
        },
        {
          "name": "Han Solo"
        },
        {
          "name": "Leia Organa"
        }
      ]
    }
  }
}
```

The implementation with graphql-net looks like this:
```csharp
class Context
{
    public Hero Hero { get; set; }
}

class Hero
{
    public string Name { get; set; }
}

var schema = GraphQL<Context>.CreateDefaultSchema(() => new Context { Hero = new Hero { Name = "R2-D2" } });
schema.AddType<Hero>().AddAllFields();
schema.AddField("hero", c => c.Hero);

schema.Complete();

var gql = new GraphQL<Context>(schema);
var queryResult = gql.ExecuteQuery("{hero {name}}");
```

See `examples/01-simple-query/` for the code.