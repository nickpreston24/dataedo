This query returns list of tables in a database with their number of rows.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) + '.' + tab.name as [table], 
       sum(part.rows) as [rows]
   from sys.tables tab
        inner join sys.partitions part
            on tab.object_id = part.object_id
where part.index_id IN (1, 0) -- 0 - table without PK, 1 table with PK
group by schema_name(tab.schema_id) + '.' + tab.name
order by sum(part.rows) desc
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