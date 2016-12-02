# Introduction

Many of the .NET GraphQL implementations that have come out so far only seem to work in memory.
For me, this isn't terribly useful since most of my data is stored in a database (and I assume that's the case for many others). 
This library is an implementation of the GraphQL spec that converts GraphQL queries to IQueryable.
That IQueryable can then be executed using the ORM of your choice.