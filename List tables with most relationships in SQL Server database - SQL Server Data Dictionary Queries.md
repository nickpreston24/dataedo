Query below lists tables with most relationships (both foreign keys and FK references from other tables).

![](https://dataedo.com/asset/img/kb/query/table_relationships.png)

See also:

-   [Tables with most foreign keys](https://dataedo.com/kb/query/sql-server/list-tables-with-most-foreign-keys) ,
-   [Summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<br><span>select</span> tab <span>as</span> [<span>table</span>],
    <span>count</span>(<span>distinct</span> rel_name) <span>as</span> relationships,
    <span>count</span>(<span>distinct</span> fk_name) <span>as</span> foreign_keys,
    <span>count</span>(<span>distinct</span> ref_name) <span>as</span> [<span>references</span>],
    <span>count</span>(<span>distinct</span> rel_object_id) <span>as</span> related_tables,
    <span>count</span>(<span>distinct</span> referenced_object_id) <span>as</span> referenced_tables,
    <span>count</span>(<span>distinct</span> parent_object_id) <span>as</span> referencing_tables
<span>from</span> 
    (<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> tab,
        fk.name <span>as</span> rel_name,
        fk.referenced_object_id <span>as</span> rel_object_id,
        fk.name <span>as</span> fk_name,
        fk.referenced_object_id,
        <span>null</span> <span>as</span> ref_name,
        <span>null</span> <span>as</span> parent_object_id
    <span>from</span> sys.tables <span>as</span> tab
        <span>left</span> <span>join</span> sys.foreign_keys <span>as</span> fk
            <span>on</span> tab.object_id = fk.parent_object_id
    <span>union</span> all
    <span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> tab,
        fk.name <span>as</span> rel_name,
        fk.parent_object_id <span>as</span> rel_object_id,
        <span>null</span> <span>as</span> fk_name,
        <span>null</span> <span>as</span> referenced_object_id,
        fk.name <span>as</span> ref_name,
        fk.parent_object_id
    <span>from</span> sys.tables <span>as</span> tab
        <span>left</span> <span>join</span> sys.foreign_keys <span>as</span> fk
            <span>on</span> tab.object_id = fk.referenced_object_id) q
<span>group</span> <span>by</span> tab
<span>order</span> <span>by</span> <span>count</span>(<span>distinct</span> rel_name) <span>desc</span>
```

## Columns

-   **table** - name of table with schema name
-   **relationships** - number of relationships (FKs and FK references)
-   **foreign\_keys** - number of foreign keys in a table
-   **references** - number of FK references from other tables
-   **related\_tables** - number of different related tables (regardless of relationship type/direction table is counted only once)
-   **referenced\_tables** - number of different tables referenced with FKs (please note that table can be related more than once so number of FKs and number of referenced tables can be different)
-   **referencing\_tables** - number of different tables referencing with foreign keys this table

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in a database
-   **Ordered by** number of relationships (foreing keys and references) from the ones with the most

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_with_most_relationships.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)