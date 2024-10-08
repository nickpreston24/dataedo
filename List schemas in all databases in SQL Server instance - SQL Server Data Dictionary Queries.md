Query below lists all schemas from all databases on SQL Server instance.

Here you can get list of databases only: [link](https://dataedo.com/kb/query/sql-server/list-databases)

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
declare @query nvarchar(max);

set @query =
(select 'select ''' + name + ''' as database_name,
                name COLLATE DATABASE_DEFAULT as schema_name 
         from ['+ name + '].sys.schemas union all '
  from sys.databases 
  where database_id &gt; 4
  for xml path(''), type).value('.', 'nvarchar(max)');

set @query = left(@query,len(@query)-10) + ' order by database_name, schema_name';
execute (@query);
```

## Columns

-   **database\_name** - database name
-   **schema\_name** - schema name

## Rows

-   **One row** represents one schema within database
-   **Scope of rows:** all schemas from all databases on SQL Server instance
-   **Ordered by** database name, schema name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/database_schemas.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)