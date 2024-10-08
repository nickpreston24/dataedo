Query below lists tables with their number of foreign keys and number of tables they refer to.

![](https://dataedo.com/asset/img/kb/query/table_foreign_keys.png)

See also:

-   [Tables with most relationships](https://dataedo.com/kb/query/sql-server/list-tables-with-most-relationships) ,
-   [Most referenced tables](https://dataedo.com/kb/query/sql-server/list-most-referenced-tables-in-a-database),
-   [Summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(fk_tab.schema_id) + '.' + fk_tab.name as [table],
    count(*) foreign_keys,
    count (distinct referenced_object_id) referenced_tables
from sys.foreign_keys fk
    inner join sys.tables fk_tab
        on fk_tab.object_id = fk.parent_object_id
group by schema_name(fk_tab.schema_id) + '.' + fk_tab.name
order by count(*) desc
```

## Columns

-   **table** - table with schema name
-   **foreign\_keys** - number of foreign keys in a table
-   **referenced\_tables** - number of referenced tables. Note that it is not the same as number of foreign keys, as multiple foreign keys may reference the same table.

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** tables in a database that have foreign keys (reference other tables)
-   **Ordered by** number of foreign keys from the ones with the most

## Sample results

Tables in AdventureWorks with most foreign keys:

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_number_of_fks.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)