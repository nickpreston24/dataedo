[This query](https://dataedo.com/kb/query/sql-server/tables-without-foreign-keys) listed tables that have no foreign keys, meaning they are not referencing any table or are not on the "many" side of FK.

Query below lists something a little different - tables that are not referencing and are not referenced by other tables. Something I called [**Loner Tables**](https://dataedo.com/blog/test-if-your-database-has-foreign-keys). This diagram illustrates the concept:

![](https://dataedo.com/asset/img/blog/loner_table.png)

[Learn more about Loner Tables](https://dataedo.com/blog/test-if-your-database-has-foreign-keys)

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select 'No FKs &gt;-' refs,
    fks.tab as [table],
    '&gt;- no FKs' fks
 from
    (select schema_name(tab.schema_id) + '.' + tab.name as tab,
        count(fk.name) as fk_cnt
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.parent_object_id
    group by schema_name(tab.schema_id), tab.name) fks
    inner join 
    (select schema_name(tab.schema_id) + '.' + tab.name as tab,
        count(fk.name) ref_cnt
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.referenced_object_id
    group by schema_name(tab.schema_id), tab.name) refs
    on fks.tab = refs.tab
where fks.fk_cnt + refs.ref_cnt = 0
```

## Columns

-   **refs** - icon symbolizing lack of references by foregin key constraints
-   **table** - name of the table with schema name
-   **fks** - icon symbolizing lack of foregin key constraints

## Rows

-   **One row** represents one table
-   **Scope of rows:** tables that are not related (not refererencing and not being referenced with foreign key constraints) with any table
-   **Ordered by** schema table name

## Sample results

List of **Loner Tables** in AdventureWorks:

![](https://dataedo.com/asset/img/kb/query/sql-server/loner_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)