[This query](https://dataedo.com/kb/query/sql-server/tables-without-foreign-keys) listed tables that have no foreign keys, meaning they are not referencing any table or are not on the "many" side of FK.

Query below lists something a little different - tables that are not referencing and are not referenced by other tables. Something I called [**Loner Tables**](https://dataedo.com/blog/test-if-your-database-has-foreign-keys). This diagram illustrates the concept:

![](https://dataedo.com/asset/img/blog/loner_table.png)

[Learn more about Loner Tables](https://dataedo.com/blog/test-if-your-database-has-foreign-keys)

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select count(*) [table_count],
    sum(case when fks.cnt + refs.cnt = 0 then 1 else 0 end) 
    as [loner_tables],
    cast(cast(100.0 * sum(case when fks.cnt + refs.cnt = 0 then 1 else 0 end) 
    / count(*) as decimal(36, 1)) as varchar) + '%' as [loner_ratio]
from (select schema_name(tab.schema_id) + '.' + tab.name as tab,
        count(fk.name) cnt
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.parent_object_id
    group by schema_name(tab.schema_id), tab.name) fks
    inner join 
    (select schema_name(tab.schema_id) + '.' + tab.name as tab,
        count(fk.name) cnt
    from sys.tables as tab
        left join sys.foreign_keys as fk
            on tab.object_id = fk.referenced_object_id
    group by schema_name(tab.schema_id), tab.name) refs
    on fks.tab = refs.tab
```

## Columns

-   **table\_count** - number of tables in database
-   **loner\_tables** - number of [Loner Tables](https://dataedo.com/blog/test-if-your-database-has-foreign-keys) in the database
-   **loner\_ratio** - [Loner Ratio](https://dataedo.com/blog/test-if-your-database-has-foreign-keys) - % of Loner Tables in the database

## Rows

-   **Scope of rows:** query returns one row

## Sample results

Loner Ratio in AdventureWorks sample database is 6.9%, meaning that 6.9% of tables are not related by foreign keys to any other table.

![](https://dataedo.com/asset/img/kb/query/sql-server/loner_ratio.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)