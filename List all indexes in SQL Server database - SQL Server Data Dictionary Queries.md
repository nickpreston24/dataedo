Query below lists all indexes in the database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select i.[name] as index_name,
    substring(column_names, 1, len(column_names)-1) as [columns],
    case when i.[type] = 1 then 'Clustered index'
        when i.[type] = 2 then 'Nonclustered unique index'
        when i.[type] = 3 then 'XML index'
        when i.[type] = 4 then 'Spatial index'
        when i.[type] = 5 then 'Clustered columnstore index'
        when i.[type] = 6 then 'Nonclustered columnstore index'
        when i.[type] = 7 then 'Nonclustered hash index'
        end as index_type,
    case when i.is_unique = 1 then 'Unique'
        else 'Not unique' end as [unique],
    schema_name(t.schema_id) + '.' + t.[name] as table_view, 
    case when t.[type] = 'U' then 'Table'
        when t.[type] = 'V' then 'View'
        end as [object_type]
from sys.objects t
    inner join sys.indexes i
        on t.object_id = i.object_id
    cross apply (select col.[name] + ', '
                    from sys.index_columns ic
                        inner join sys.columns col
                            on ic.object_id = col.object_id
                            and ic.column_id = col.column_id
                    where ic.object_id = t.object_id
                        and ic.index_id = i.index_id
                            order by key_ordinal
                            for xml path ('') ) D (column_names)
where t.is_ms_shipped &lt;&gt; 1
and index_id &gt; 0
order by i.[name]
```

## Columns

-   **index\_name** - index name
-   **columns** - list of index columns separated with ","
-   **index\_type**
    -   Clustered index
    -   Nonclustered unique index
    -   XML index
    -   Spatial index
    -   Clustered columnstore index
    -   Nonclustered columnstore index
    -   Nonclustered hash index
-   **unique** - whether index is unique
    -   Unique
    -   Not unique
-   **table\_view** - index table or view schema and name
-   **object\_type** - type of object index is defined for:
    -   Table
    -   View

## Rows

-   **One row** represents one index
-   **Scope of rows:** all indexes in the database
-   **Ordered by** index name

## Sample results

Indexes in database.

![](https://dataedo.com/asset/img/kb/query/sql-server/indexes.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)