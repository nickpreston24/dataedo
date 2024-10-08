Query below list ten largest tables in database.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> top <span>10</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name <span>as</span> [<span>table</span>], 
    <span>cast</span>(<span>sum</span>(spc.used_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> used_mb,
    <span>cast</span>(<span>sum</span>(spc.total_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> allocated_mb
<span>from</span> sys.tables tab
<span>join</span> sys.indexes ind 
     <span>on</span> tab.object_id = ind.object_id
<span>join</span> sys.partitions part 
     <span>on</span> ind.object_id = part.object_id <span>and</span> ind.index_id = part.index_id
<span>join</span> sys.allocation_units spc
     <span>on</span> part.partition_id = spc.container_id
<span>group</span> <span>by</span> schema_name(tab.schema_id) + <span>'.'</span> + tab.name
<span>order</span> <span>by</span> <span>sum</span>(spc.used_pages) <span>desc</span>;
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