# Arguments

Grapqhl-net supports field arguments.

The query:
```csharp
{
  human(id: "1000") {
    name
    height
  }
}
```
Should return:
```json
{
  "data": {
    "human": {
      "name": "Luke Skywalker",
      "height": 1.72
    }
  }
}
```

This can be implemented like this:

```csharp
class Context
{
    public IList<Human> Humans { get; set; }
}

class Human
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Height { get; set; }
}

...

var schema = GraphQL<Context>.CreateDefaultSchema(() =>
new Context
{
    Humans = new List<Human> {
        new Human { Id = "1000", Name = "Luke Skywalker", Height = 1.72 }
    }
});
schema.AddType<Human>().AddAllFields();
schema.AddField(
    "human",
    new { id = "-1" },
    (c, args) => c.Humans.SingleOrDefault(h => h.Id == args.id));

schema.Complete();

var gql = new GraphQL<Context>(schema);
var queryResult = gql.ExecuteQuery("{human(id: \"1000\") {name, height}}");
```