Query below lists all table columns in a database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) as schema_name,
    tab.name as table_name, 
    col.column_id,
    col.name as column_name, 
    t.name as data_type,    
    col.max_length,
    col.precision
from sys.tables as tab
    inner join sys.columns as col
        on tab.object_id = col.object_id
    left join sys.types as t
    on col.user_type_id = t.user_type_id
order by schema_name,
    table_name, 
    column_id;
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **column\_id** - table column id, starting at 1 for each table
-   **column\_name** - name of column
-   **data\_type** - column data type
-   **max\_length** - data type max length
-   **precision** - data type precision

## Rows

-   **One row** represents one table column
-   **Scope of rows:** all columns in all tables in a database
-   **Ordered by** schema, table name, column id

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/table_columns.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial?cta=QuerySQLTableCols)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)