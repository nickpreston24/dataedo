Query below returns tables in a database with space used by their indexes ordered from the ones using most.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>],
       (<span>sum</span>(spc.total_pages * <span>8</span>) / <span>1024.0</span>) <span>as</span> allocated_space,
       (<span>sum</span>(spc.used_pages * <span>8</span>) / <span>1024.0</span>) <span>as</span> used_space
<span>from</span> sys.tables tab
<span>join</span> sys.indexes ind
     <span>on</span> tab.object_id = ind.object_id
<span>join</span> sys.partitions part
     <span>on</span> ind.object_id = part.object_id
     <span>and</span> ind.index_id = part.index_id
<span>join</span> sys.allocation_units spc
     <span>on</span> part.partition_id = spc.container_id
<span>group</span> <span>by</span> tab.schema_id, tab.name
<span>order</span> <span>by</span> used_space <span>desc</span>;
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