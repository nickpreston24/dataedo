SQL Server stores large object (LOB) data (e.g. varchar(max), text, image columns) in separate so called allocation units.

Query below can help you find how much space is allocated for LOB data.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select case when spc.type in (1, 3) then 'Regular data'
            else 'LOB data' end as allocation_type,
    cast(sum(spc.used_pages * 8) / 1024.00 as numeric(36, 2)) as used_mb,
    cast(sum(spc.total_pages * 8) / 1024.00 as numeric(36, 2)) as allocated_mb
from sys.tables tab
    inner join sys.indexes ind 
        on tab.object_id = ind.object_id
    inner join sys.partitions part 
        on ind.object_id = part.object_id and ind.index_id = part.index_id
    inner join sys.allocation_units spc
        on part.partition_id = spc.container_id
group by case when spc.type in (1, 3) then 'Regular data' 
        else 'LOB data' end
```

## Columns

-   **allocation\_type** - type of space allcation:
    -   **LOB data** - space allocated for LOB data columns
    -   **Regular data** - space allocated for other columns
-   **used\_mb** - space used in MB
-   **allocated\_mb** - space allocated in MB

## Rows

-   **One row** represents one type of allocation: LOB/regular
-   **Scope of rows:** query returns two rows

## Sample results

Space allocation for LOB and other data types in AdventureWorks:

![](https://dataedo.com/asset/img/kb/query/sql-server/lob_allocation.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)