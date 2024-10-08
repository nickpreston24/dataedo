Query below returns list indexes and their size.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select ind.name as [index_name],
    ind.type_desc as index_type,
    cast(sum(spc.used_pages * 8)/1024.00 as numeric(36,2)) as used_mb,
    cast(sum(spc.total_pages * 8)/1024.00 as numeric(36,2)) as allocated_mb,
    cast(sum(spc.data_pages * 8)/1024.00 as numeric(36,2)) as data_space_mb,
    ind.is_unique,
    ind.is_primary_key,
    ind.is_unique_constraint,
    schema_name(obj.schema_id) + '.' + obj.name as object_name,
    obj.type_desc as type
from sys.indexes ind
join sys.objects obj 
        on obj.object_id = ind.object_id
        and obj.type in ('U','V')
join sys.partitions part 
        on ind.object_id = part.object_id 
        and ind.index_id = part.index_id
join sys.allocation_units spc
     on part.partition_id = spc.container_id
where ind.index_id &gt; 0
group by obj.schema_id, obj.name, ind.name, ind.type_desc, 
         ind.is_unique, ind.is_primary_key, ind.is_unique_constraint, 
         obj.type_desc
order by sum(spc.total_pages) desc;
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