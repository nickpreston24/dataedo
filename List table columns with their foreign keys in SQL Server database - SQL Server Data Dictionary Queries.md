Query below returns all columns from all tables in a database with a foreign key refererence if column has one.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) + '.' + tab.name as [table],
    col.column_id,
    col.name as column_name,
    case when fk.object_id is not null then '&gt;-' else null end as rel,
    schema_name(pk_tab.schema_id) + '.' + pk_tab.name as primary_table,
    pk_col.name as pk_column_name,
    fk_cols.constraint_column_id as no,
    fk.name as fk_constraint_name
from sys.tables tab
    inner join sys.columns col 
        on col.object_id = tab.object_id
    left outer join sys.foreign_key_columns fk_cols
        on fk_cols.parent_object_id = tab.object_id
        and fk_cols.parent_column_id = col.column_id
    left outer join sys.foreign_keys fk
        on fk.object_id = fk_cols.constraint_object_id
    left outer join sys.tables pk_tab
        on pk_tab.object_id = fk_cols.referenced_object_id
    left outer join sys.columns pk_col
        on pk_col.column_id = fk_cols.referenced_column_id
        and pk_col.object_id = fk_cols.referenced_object_id
order by schema_name(tab.schema_id) + '.' + tab.name,
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