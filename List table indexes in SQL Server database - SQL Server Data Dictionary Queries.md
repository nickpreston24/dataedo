Query below lists table (and view) indexes.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>] <span>as</span> table_view, 
    <span>case</span> <span>when</span> t.[<span>type</span>] = <span>'U'</span> <span>then</span> <span>'Table'</span>
        <span>when</span> t.[<span>type</span>] = <span>'V'</span> <span>then</span> <span>'View'</span>
        <span>end</span> <span>as</span> [object_type],
    i.index_id,
    <span>case</span> <span>when</span> i.is_primary_key = <span>1</span> <span>then</span> <span>'Primary key'</span>
        <span>when</span> i.is_unique = <span>1</span> <span>then</span> <span>'Unique'</span>
        <span>else</span> <span>'Not unique'</span> <span>end</span> <span>as</span> [<span>type</span>],
    i.[<span>name</span>] <span>as</span> index_name,
    <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [<span>columns</span>],
    <span>case</span> <span>when</span> i.[<span>type</span>] = <span>1</span> <span>then</span> <span>'Clustered index'</span>
        <span>when</span> i.[<span>type</span>] = <span>2</span> <span>then</span> <span>'Nonclustered unique index'</span>
        <span>when</span> i.[<span>type</span>] = <span>3</span> <span>then</span> <span>'XML index'</span>
        <span>when</span> i.[<span>type</span>] = <span>4</span> <span>then</span> <span>'Spatial index'</span>
        <span>when</span> i.[<span>type</span>] = <span>5</span> <span>then</span> <span>'Clustered columnstore index'</span>
        <span>when</span> i.[<span>type</span>] = <span>6</span> <span>then</span> <span>'Nonclustered columnstore index'</span>
        <span>when</span> i.[<span>type</span>] = <span>7</span> <span>then</span> <span>'Nonclustered hash index'</span>
        <span>end</span> <span>as</span> index_type
<span>from</span> sys.objects t
    <span>inner</span> <span>join</span> sys.indexes i
        <span>on</span> t.object_id = i.object_id
    <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                    <span>from</span> sys.index_columns ic
                        <span>inner</span> <span>join</span> sys.columns <span>col</span>
                            <span>on</span> ic.object_id = col.object_id
                            <span>and</span> ic.column_id = col.column_id
                    <span>where</span> ic.object_id = t.object_id
                        <span>and</span> ic.index_id = i.index_id
                            <span>order</span> <span>by</span> col.column_id
                            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
<span>where</span> t.is_ms_shipped &lt;&gt; <span>1</span>
<span>and</span> index_id &gt; <span>0</span>
<span>order</span> <span>by</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>], i.index_id
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