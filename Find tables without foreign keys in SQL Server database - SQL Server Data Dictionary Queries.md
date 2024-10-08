Query below lists all tables that do not have foreign keys.

![](https://dataedo.com/asset/img/kb/query/table_no_foreign_keys.png)

See also:

-   [Tables that are not referenced by the foreign keys](https://dataedo.com/kb/query/sql-server/find-tables-that-are-not-referenced-by-the-foreign-keys),
-   [Loner Tables - tables without relationships](https://dataedo.com/kb/query/sql-server/find-tables-without-relationships-loner-tables),
    -   [Summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(fk_tab.schema_id) <span>as</span> schema_name,
    fk_tab.name <span>as</span> table_name,
    <span>'&gt;- no FKs'</span> foreign_keys
<span>from</span> sys.tables fk_tab
    <span>left</span> <span>outer</span> <span>join</span> sys.foreign_keys fk
        <span>on</span> fk_tab.object_id = fk.parent_object_id
<span>where</span> fk.object_id <span>is</span> <span>null</span>
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id),
    fk_tab.name
```

## Columns

-   **schema\_name** - table schema name
-   **table\_name** - table name
-   **foreign\_keys** - symbol indicating lack of FKs

## Rows

-   **One row** represents one table that doesn't have a foreign key
-   **Scope of rows:** all tables in a database that don't have foreign keys (do not refer to other tables)
-   **Ordered by** schema and table name

## Sample results

List of tables in AdventureWorks without foreign keys:

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_without_fks.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)