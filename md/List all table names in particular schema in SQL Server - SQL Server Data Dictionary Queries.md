The query below lists tables in provided schema .To list tables from all schemas use [this query](https://dataedo.com/kb/query/sql-server/list-of-tables-in-the-database).

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select name as table_name
from sys.tables
where schema_name(schema_id) = 'HumanResources' -- put your schema name here
order by name;
```

## Columns

-   **table\_name** - table name in provided schema

## Rows

-   **One row:** represents one table in a schema
-   **Scope of rows:** all tables in particular schema
-   **Ordered by:** table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/particular_schema_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)