Query below returns list indexes and their size.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> ind.name <span>as</span> [index_name],
    ind.type_desc <span>as</span> index_type,
    <span>cast</span>(<span>sum</span>(spc.used_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>,<span>2</span>)) <span>as</span> used_mb,
    <span>cast</span>(<span>sum</span>(spc.total_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>,<span>2</span>)) <span>as</span> allocated_mb,
    <span>cast</span>(<span>sum</span>(spc.data_pages * <span>8</span>)/<span>1024.00</span> <span>as</span> <span>numeric</span>(<span>36</span>,<span>2</span>)) <span>as</span> data_space_mb,
    ind.is_unique,
    ind.is_primary_key,
    ind.is_unique_constraint,
    schema_name(obj.schema_id) + <span>'.'</span> + obj.name <span>as</span> object_name,
    obj.type_desc <span>as</span> <span>type</span>
<span>from</span> sys.indexes ind
<span>join</span> sys.objects obj 
        <span>on</span> obj.object_id = ind.object_id
        <span>and</span> obj.type <span>in</span> (<span>'U'</span>,<span>'V'</span>)
<span>join</span> sys.partitions part 
        <span>on</span> ind.object_id = part.object_id 
        <span>and</span> ind.index_id = part.index_id
<span>join</span> sys.allocation_units spc
     <span>on</span> part.partition_id = spc.container_id
<span>where</span> ind.index_id &gt; <span>0</span>
<span>group</span> <span>by</span> obj.schema_id, obj.name, ind.name, ind.type_desc, 
         ind.is_unique, ind.is_primary_key, ind.is_unique_constraint, 
         obj.type_desc
<span>order</span> <span>by</span> <span>sum</span>(spc.total_pages) <span>desc</span>;
```

## Columns

-   **index\_name** - index name
-   **index\_type** -
    -   CLUSTERED
    -   NONCLUSTERED
-   **used\_mb** - size of space used by index
-   **allocated\_mb** - size of space allocated or reserved by table
-   **data\_space\_mb** - size of space used by index data
-   **is\_unique** - indicate if index is unique
    -   1 - unique
    -   0 - not unique
-   **is\_primary\_key** indicate if index is primary key
    -   1 - primary key
    -   0 - not primary key
-   **is\_unique\_constraint** - indicate if index was created by UNIQUE constraint
-   **object\_name** - indexed table/view name
-   **object\_type** -
    -   USER\_TABLE
    -   VIEW

## Rows

-   **One row** represents one index in the database
-   **Scope of rows:** all indexes in the database
-   **Ordered by** index allocated space

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/size_of_indexes.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)