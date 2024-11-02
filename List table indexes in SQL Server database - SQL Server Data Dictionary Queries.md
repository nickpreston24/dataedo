Query below lists table (and view) indexes.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(t.schema_id) + '.' + t.[name] as table_view, 
    case when t.[type] = 'U' then 'Table'
        when t.[type] = 'V' then 'View'
        end as [object_type],
    i.index_id,
    case when i.is_primary_key = 1 then 'Primary key'
        when i.is_unique = 1 then 'Unique'
        else 'Not unique' end as [type],
    i.[name] as index_name,
    substring(column_names, 1, len(column_names)-1) as [columns],
    case when i.[type] = 1 then 'Clustered index'
        when i.[type] = 2 then 'Nonclustered unique index'
        when i.[type] = 3 then 'XML index'
        when i.[type] = 4 then 'Spatial index'
        when i.[type] = 5 then 'Clustered columnstore index'
        when i.[type] = 6 then 'Nonclustered columnstore index'
        when i.[type] = 7 then 'Nonclustered hash index'
        end as index_type
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
                            order by col.column_id
                            for xml path ('') ) D (column_names)
where t.is_ms_shipped &lt;&gt; 1
and index_id &gt; 0
order by schema_name(t.schema_id) + '.' + t.[name], i.index_id
```

## Columns

-   **table\_view** - name of table or view index is defined for
-   **object\_type** - type of object that index is defined for:
    -   Table
    -   View
-   **index\_id** - id of index (unique in table)
-   **type**
    -   Primary key
    -   Unique
    -   Not unique
-   **index\_name** - index name
-   **columns** - list of index columns separated with ","
-   **index\_type** - index type:
    -   Clustered index
    -   Nonclustered unique index
    -   XML index
    -   Spatial index
    -   Clustered columnstore index
    -   Nonclustered columnstore index
    -   Nonclustered hash index

## Rows

-   **One row** represents represents index
-   **Scope of rows:** all indexes (unique and non unique) in databases
-   **Ordered by** schema, table name, index id

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/table_indexes.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)