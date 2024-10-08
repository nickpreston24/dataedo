[This query](https://dataedo.com/kb/query/sql-server/find-tables-without-primary-keys) listed tables without primary keys and this one shows how many of them there are and what is the percentage of total tables.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select 
    all_tabs.[tables] as all_tables,
    no_pk.[tables] as no_pk_tables,
    cast(cast(100.0 * no_pk.[tables] / 
    all_tabs.[tables] as decimal(36, 1)) as varchar) + '%' as no_pk_percent
from
    (select count(*) as [tables]
    from sys.tables tab
        left outer join sys.indexes pk
            on tab.object_id = pk.object_id 
            and pk.is_primary_key = 1
    where pk.object_id is null) as no_pk
inner join 
    (select count(*) as [tables]
    from sys.tables) as all_tabs
on 1 = 1
```

## Columns

-   **all\_tables** - number of all tables in a database
-   **no\_pk\_tables** - number of tables without a primary key
-   **no\_pk\_percent** - percentage of tables without primary key in all tables

## Rows

Query returns just **one row**.

## Sample results

I checked how many tables in Microsoft System Center Configuration Manager database do not have primary keys. It turns out it is only 1.3% which for me is a good result.

![](https://dataedo.com/asset/img/kb/query/sql-server/no_pk_tables_percentage.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)