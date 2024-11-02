Query below returns tables in a database with space used by their indexes ordered from the ones using most.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) + '.' + tab.name as [table],
       (sum(spc.total_pages * 8) / 1024.0) as allocated_space,
       (sum(spc.used_pages * 8) / 1024.0) as used_space
from sys.tables tab
join sys.indexes ind
     on tab.object_id = ind.object_id
join sys.partitions part
     on ind.object_id = part.object_id
     and ind.index_id = part.index_id
join sys.allocation_units spc
     on part.partition_id = spc.container_id
group by tab.schema_id, tab.name
order by used_space desc;
```

## Columns

-   **table** - table's schema name and name
-   **allocated\_space** - space allocated for indexes
-   **used\_space** - space actually used by indexes

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/table_size_indexes.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)