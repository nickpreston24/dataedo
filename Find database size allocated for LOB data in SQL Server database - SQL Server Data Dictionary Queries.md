SQL Server stores large object (LOB) data (e.g. varchar(max), text, image columns) in separate so called allocation units.

Query below can help you find how much space is allocated for LOB data.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> <span>case</span> <span>when</span> spc.type <span>in</span> (<span>1</span>, <span>3</span>) <span>then</span> <span>'Regular data'</span>
            <span>else</span> <span>'LOB data'</span> <span>end</span> <span>as</span> allocation_type,
    <span>cast</span>(<span>sum</span>(spc.used_pages * <span>8</span>) / <span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> used_mb,
    <span>cast</span>(<span>sum</span>(spc.total_pages * <span>8</span>) / <span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>, <span>2</span>)) <span>as</span> allocated_mb
<span>from</span> sys.tables tab
    <span>inner</span> <span>join</span> sys.indexes ind 
        <span>on</span> tab.object_id = ind.object_id
    <span>inner</span> <span>join</span> sys.partitions part 
        <span>on</span> ind.object_id = part.object_id <span>and</span> ind.index_id = part.index_id
    <span>inner</span> <span>join</span> sys.allocation_units spc
        <span>on</span> part.partition_id = spc.container_id
<span>group</span> <span>by</span> <span>case</span> <span>when</span> spc.type <span>in</span> (<span>1</span>, <span>3</span>) <span>then</span> <span>'Regular data'</span> 
        <span>else</span> <span>'LOB data'</span> <span>end</span>
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