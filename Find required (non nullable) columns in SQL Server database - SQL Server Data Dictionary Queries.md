Query below lists all non-nullable columns in a database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> schema_name,
    tab.name <span>as</span> table_name, 
    col.column_id,
    col.name <span>as</span> column_name,
    t.name <span>as</span> data_type,
    col.max_length,
    col.precision
<span>from</span> sys.tables <span>as</span> tab
    <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span>
        <span>on</span> tab.object_id = col.object_id
    <span>left</span> <span>join</span> sys.types <span>as</span> t
    <span>on</span> col.user_type_id = t.user_type_id
<span>where</span> col.is_nullable = <span>0</span>
<span>order</span> <span>by</span> schema_name,
    table_name, 
    column_name;
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **column\_id** - column position in a table
-   **column\_name** - name of column
-   **data\_type** - column data type
-   **max\_length** - data type max length
-   **precision** - data type precision

## Rows

-   **One row** represents one table column
-   **Scope of rows:** all non-nullable columns in all tables in a database
-   **Ordered by** schema, table name, column id

## Sample results

Non-nullable columns in AdventureWorks database.

![](https://dataedo.com/asset/img/kb/query/sql-server/nonnullable_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)