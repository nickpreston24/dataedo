Query below lists tables with most relationships (both foreign keys and FK references from other tables).

![](https://dataedo.com/asset/img/kb/query/table_relationships.png)

See also:

-   [Tables with most foreign keys](https://dataedo.com/kb/query/sql-server/list-tables-with-most-foreign-keys) ,
-   [Summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select tab as [table],
    count(distinct rel_name) as relationships,
    count(distinct fk_name) as foreign_keys,
    count(distinct ref_name) as [references],
    count(distinct rel_object_id) as related_tables,
    count(distinct referenced_object_id) as referenced_tables,
    count(distinct parent_object_id) as referencing_tables
from 
    (select schema_name(tab.schema_id) + '.' + tab.name as tab,
        fk.name as rel_name,
        fk.referenced_object_id as rel_object_id,
        fk.name as fk_name,
        fk.referenced_object_id,
        null as ref_name,
        null as parent_object_id
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.parent_object_id
    union all
    select schema_name(tab.schema_id) + '.' + tab.name as tab,
        fk.name as rel_name,
        fk.parent_object_id as rel_object_id,
        null as fk_name,
        null as referenced_object_id,
        fk.name as ref_name,
        fk.parent_object_id
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.referenced_object_id) q
group by tab
order by count(distinct rel_name) desc
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