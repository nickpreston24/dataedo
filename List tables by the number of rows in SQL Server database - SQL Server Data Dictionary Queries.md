This query returns list of tables in a database with their number of rows.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>], 
       <span>sum</span>(part.rows) <span>as</span> [<span>rows</span>]
   <span>from</span> sys.tables tab
        <span>inner</span> <span>join</span> sys.partitions part
            <span>on</span> tab.object_id = part.object_id
<span>where</span> part.index_id <span>IN</span> (<span>1</span>, <span>0</span>) <span>-- 0 - table without PK, 1 table with PK</span>
<span>group</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name
<span>order</span> <span>by</span> <span>sum</span>(part.rows) <span>desc</span>
```

## Columns

-   **table** - table name with schema name
-   **rows** - number of rows in a table

## Rows

-   **One row** represents one table
-   **Scope of rows:** all tables in a database including tables without rows
-   **Ordered by** number of rows descending, from largest to smallest (in terms of number of rows)

## Sample results

Tables by number of rows in our SSRS repository:

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_number_of_rows.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)