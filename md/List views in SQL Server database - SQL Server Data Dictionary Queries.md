Query below lists all views in SQL Server database

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(schema_id) as schema_name,
       name as view_name
from sys.views
order by schema_name,
         view_name;
```

## Columns

-   **schema\_name** - schema name
-   **view\_name** - view name

## Rows

-   **One row** represents one view in a database
-   **Scope of rows:** all views in SQL Server database
-   **Ordered by** schema name, view name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/list-views-in-a-database.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)