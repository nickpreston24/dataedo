Query below returns foreign key constrant columns defined in a database.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(fk_tab.schema_id) + '.' + fk_tab.name as foreign_table,
    '&gt;-' as rel,
    schema_name(pk_tab.schema_id) + '.' + pk_tab.name as primary_table,
    fk_cols.constraint_column_id as no, 
    fk_col.name as fk_column_name,
    ' = ' as [join],
    pk_col.name as pk_column_name,
    fk.name as fk_constraint_name
from sys.foreign_keys fk
    inner join sys.tables fk_tab
        on fk_tab.object_id = fk.parent_object_id
    inner join sys.tables pk_tab
        on pk_tab.object_id = fk.referenced_object_id
    inner join sys.foreign_key_columns fk_cols
        on fk_cols.constraint_object_id = fk.object_id
    inner join sys.columns fk_col
        on fk_col.column_id = fk_cols.parent_column_id
        and fk_col.object_id = fk_tab.object_id
    inner join sys.columns pk_col
        on pk_col.column_id = fk_cols.referenced_column_id
        and pk_col.object_id = pk_tab.object_id
order by schema_name(fk_tab.schema_id) + '.' + fk_tab.name,
    schema_name(pk_tab.schema_id) + '.' + pk_tab.name, 
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