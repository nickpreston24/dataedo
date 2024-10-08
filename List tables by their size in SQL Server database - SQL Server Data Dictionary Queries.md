Query below returns tables in a database with space they use ordered from the ones using most.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>], 
    <span>cast</span>(<span>sum</span>(spc.used_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> used_mb,
    <span>cast</span>(<span>sum</span>(spc.total_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> allocated_mb
<span>from</span> sys.tables tab
    <span>inner</span> <span>join</span> sys.indexes ind 
        <span>on</span> tab.object_id = ind.object_id
    <span>inner</span> <span>join</span> sys.partitions part 
        <span>on</span> ind.object_id = part.object_id <span>and</span> ind.index_id = part.index_id
    <span>inner</span> <span>join</span> sys.allocation_units spc
        <span>on</span> part.partition_id = spc.container_id
<span>group</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name
<span>order</span> <span>by</span> <span>sum</span>(spc.used_pages) <span>desc</span>
```

## Columns

-   **table** - table name with schema name
-   **used\_mb** - space used in MB
-   **allocated\_mb** - space allocated in MB

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in a database
-   **Ordered by** table used size, from largest to smallest

## Sample results

Tables in AdventureWorks ordered from the ones using most space to least.

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_by_size.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)