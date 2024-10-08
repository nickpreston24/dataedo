Query below returns all columns from all tables in a database with a foreign key refererence if column has one.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>],
    col.column_id,
    col.name <span>as</span> column_name,
    <span>case</span> <span>when</span> fk.object_id <span>is</span> <span>not</span> <span>null</span> <span>then</span> <span>'&gt;-'</span> <span>else</span> <span>null</span> <span>end</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    pk_col.name <span>as</span> pk_column_name,
    fk_cols.constraint_column_id <span>as</span> <span>no</span>,
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.tables tab
    <span>inner</span> <span>join</span> sys.columns <span>col</span> 
        <span>on</span> col.object_id = tab.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.foreign_key_columns fk_cols
        <span>on</span> fk_cols.parent_object_id = tab.object_id
        <span>and</span> fk_cols.parent_column_id = col.column_id
    <span>left</span> <span>outer</span> <span>join</span> sys.foreign_keys fk
        <span>on</span> fk.object_id = fk_cols.constraint_object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk_cols.referenced_object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.columns pk_col
        <span>on</span> pk_col.column_id = fk_cols.referenced_column_id
        <span>and</span> pk_col.object_id = fk_cols.referenced_object_id
<span>order</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name,
    col.column_id
```

## Columns

-   **table** - table in a database with schema name
-   **column\_id** - number of column in a database
-   **column\_name** - column name
-   **rel** - relationship symbol ('>-') indicating foreign key and direction
-   **primary\_table** - referenced table
-   **pk\_column\_name** - referenced column
-   **no** - column id in a key constraint
-   **fk\_constraint\_name** - foreign key constraint name

## Rows

-   **One row** represents one column of every table in a database
-   **Scope of rows:** all columns from all tables in a database
-   **Ordered by** table schema and name, column id in a table

## Sample results

A few columns in AdventureWorks database with their foregin keys:

![](https://dataedo.com/asset/img/kb/query/sql-server/columns_with_fks.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)