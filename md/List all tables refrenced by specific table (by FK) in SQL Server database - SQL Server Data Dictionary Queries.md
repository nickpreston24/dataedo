Query below lists all tables refrenced with foregin key by specific table.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select distinct 
    schema_name(fk_tab.schema_id) + '.' + fk_tab.name as foreign_table,
    '&gt;-' as rel,
    schema_name(pk_tab.schema_id) + '.' + pk_tab.name as primary_table
from sys.foreign_keys fk
    inner join sys.tables fk_tab
        on fk_tab.object_id = fk.parent_object_id
    inner join sys.tables pk_tab
        on pk_tab.object_id = fk.referenced_object_id
where fk_tab.[name] = 'Your table' -- enter table name here
--  and schema_name(fk_tab.schema_id) = 'Your table schema name'
order by schema_name(fk_tab.schema_id) + '.' + fk_tab.name,
    schema_name(pk_tab.schema_id) + '.' + pk_tab.name
```

## Columns

-   **foreign\_table** - foreign table name with schema name - the table you provided as a parameter
-   **foreign\_table** - relationship symbol implicating FK and direction
-   **primary\_table** - primary (rerefenced) tables names with schema name - the tables you are looking for

## Rows

-   **One row** represents one referenced table
-   **Scope of rows:** all tables referenced by table with provided name (and optionally schema)
-   **Ordered by** referenced table schema and name

## Notes

-   There can be more tables with the same name. If that's the case, uncomment where clause and provide schema name

## Sample results

All tables referenced with FK by **Sales.SalesOrderHeader** table in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/referenced_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)