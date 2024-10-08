Query below lists all **unique** indexes in the database.

Check out also list of [unique constraints](https://dataedo.com/kb/query/sql-server/list-unique-keys-and-indexes-in-the-database).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select i.[name] as index_name,
    substring(column_names, 1, len(column_names)-1) as [columns],
    case when i.[type] = 1 then 'Clustered unique index'
        when i.type = 2 then 'Unique index'
        end as index_type,
    schema_name(t.schema_id) + '.' + t.[name] as table_view, 
    case when t.[type] = 'U' then 'Table'
        when t.[type] = 'V' then 'View'
        end as [object_type],
    case when c.[type] = 'PK' then 'Primary key'
        when c.[type] = 'UQ' then 'Unique constraint'
        end as constraint_type, 
    c.[name] as constraint_name
from sys.objects t
    left outer join sys.indexes i
        on t.object_id = i.object_id
    left outer join sys.key_constraints c
        on i.object_id = c.parent_object_id 
        and i.index_id = c.unique_index_id
   cross apply (select col.[name] + ', '
                    from sys.index_columns ic
                        inner join sys.columns col
                            on ic.object_id = col.object_id
                            and ic.column_id = col.column_id
                    where ic.object_id = t.object_id
                        and ic.index_id = i.index_id
                            order by col.column_id
                            for xml path ('') ) D (column_names)
where is_unique = 1
and t.is_ms_shipped &lt;&gt; 1
order by i.[name]
```

## Columns

-   **index\_name** - name of the index
-   **columns** - index columns separated with ","
-   **index\_type** - type of the index
    -   Clustered unique index
    -   Unique index - non-clustered unique index
-   **table\_view** - schema and name of table or view
-   **object\_type** - type of object which constraint/index was created on
    -   Table
    -   View
-   **constraint\_type** -
    -   Primary key - for primary keys
    -   Unique constraint - for constraints created with CONSTRAINT UNIQUE statement
-   **constraint\_name** - primary/unique key constraint, null for unique indexes without constraints

## Rows

-   **One row** represents one index
-   **Scope of rows:** all unique indexes in the database
-   **Ordered by** index name

## Sample results

Unique indexes in AdventureWorks database:

![](https://dataedo.com/asset/img/kb/query/sql-server/unique_indexes.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)