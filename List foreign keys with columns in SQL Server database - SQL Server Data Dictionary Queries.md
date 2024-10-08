Query below returns foreign key constrant columns defined in a database.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    fk_cols.constraint_column_id <span>as</span> <span>no</span>, 
    fk_col.name <span>as</span> fk_column_name,
    <span>' = '</span> <span>as</span> [<span>join</span>],
    pk_col.name <span>as</span> pk_column_name,
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
    <span>inner</span> <span>join</span> sys.foreign_key_columns fk_cols
        <span>on</span> fk_cols.constraint_object_id = fk.object_id
    <span>inner</span> <span>join</span> sys.columns fk_col
        <span>on</span> fk_col.column_id = fk_cols.parent_column_id
        <span>and</span> fk_col.object_id = fk_tab.object_id
    <span>inner</span> <span>join</span> sys.columns pk_col
        <span>on</span> pk_col.column_id = fk_cols.referenced_column_id
        <span>and</span> pk_col.object_id = pk_tab.object_id
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name, 
    fk_cols.constraint_column_id
```

## Columns

-   **foreign\_table** - foreign table name with schema name
-   **rel** - relationship symbol implicating direction
-   **primary\_table** - primary (referenced) table name with schema name
-   **no** - id of the column in key. Single coumn keys always have 1, composite keys have 1, 2, ... n for each column of the key
-   **fk\_column\_name** - foreign table column
-   **join** - "=" symbol indicating join operation for pair of columns
-   **pk\_column\_name** - primary (referenced) table column
-   **fk\_constraint\_name** - foreign key constraint name

## Rows

-   **One row** represents one foreign key column. If foreign key consists of multiple columns (composite key), each column appears separately.
-   **Scope of rows:** all foregin keys in a database and their columns
-   **Ordered by** foreign table schema name and table name and column ordinal posion in key

## Sample results

Foreign keys in AdventureWorks database with their columns:

![](https://dataedo.com/asset/img/kb/query/sql-server/foreign_key_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)