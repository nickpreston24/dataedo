Query below lists all schemas in SQL Server database. Schemas include default **db\_\*** , **sys**, **information\_schema** and **guest** schemas.

If you want to list user only schemas use [this script](https://dataedo.com/kb/query/sql-server/list-user-schemas-in-database).

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select s.name as schema_name, 
    s.schema_id,
    u.name as schema_owner
from sys.schemas s
    inner join sys.sysusers u
        on u.uid = s.principal_id
order by s.name
```

## Columns

-   **schema\_name** - schema name
-   **schema\_id** - schema id, unique within the database
-   **schema\_owner** - principal that owns this schema

## Rows

-   **One row** represents one schema in a database
-   **Scope of rows:** all schemas in a database, including default ones
-   **Ordered by** schema name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/schemas_in_database.png)

Here is a view of database schemas in SSMS:

![](https://dataedo.com/asset/img/kb/query/sql-server/schemas_in_database_ssms.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)