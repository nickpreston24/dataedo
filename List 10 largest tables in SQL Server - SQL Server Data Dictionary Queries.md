Query below list ten largest tables in database.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select top 10 schema_name(tab.schema_id) + '.' + tab.name as [table], 
    cast(sum(spc.used_pages * 8)/1024.00 as numeric(36, 2)) as used_mb,
    cast(sum(spc.total_pages * 8)/1024.00 as numeric(36, 2)) as allocated_mb
from sys.tables tab
join sys.indexes ind 
     on tab.object_id = ind.object_id
join sys.partitions part 
     on ind.object_id = part.object_id and ind.index_id = part.index_id
join sys.allocation_units spc
     on part.partition_id = spc.container_id
group by schema_name(tab.schema_id) + '.' + tab.name
order by sum(spc.used_pages) desc;
```

## Columns

-   **table** - table name with schema name
-   **used\_mb** - size of space actually in use by table in MB
-   **allocated\_mb** - size of allocated or reserved space by this table in MB

## Rows

-   **One row** represents one table
-   **Scope of rows:** ten tables which uses most space
-   **Ordered by** actually used space

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/ten_largest_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)