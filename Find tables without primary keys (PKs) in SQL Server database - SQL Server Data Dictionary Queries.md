Query below lists tables in a database without primary keys.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) as [schema_name], 
    tab.[name] as table_name
from sys.tables tab
    left outer join sys.indexes pk
        on tab.object_id = pk.object_id 
        and pk.is_primary_key = 1
where pk.object_id is null
order by schema_name(tab.schema_id),
    tab.[name]
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables without primary keys in a database
-   **Ordered by** schema, table name

## Sample results

Below is a list of tables in Microsoft System Center Configuration Manager database **without primary keys**. Is that a lot? Check out [here](https://dataedo.com/kb/query/sql-server/how-many-tables-dont-have-primary-keys).

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_without_primary_keys.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)