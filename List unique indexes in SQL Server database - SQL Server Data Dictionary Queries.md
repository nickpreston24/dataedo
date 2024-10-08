Query below lists all **unique** indexes in the database.

Check out also list of [unique constraints](https://dataedo.com/kb/query/sql-server/list-unique-keys-and-indexes-in-the-database).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> i.[<span>name</span>] <span>as</span> index_name,
    <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [<span>columns</span>],
    <span>case</span> <span>when</span> i.[<span>type</span>] = <span>1</span> <span>then</span> <span>'Clustered unique index'</span>
        <span>when</span> i.type = <span>2</span> <span>then</span> <span>'Unique index'</span>
        <span>end</span> <span>as</span> index_type,
    schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>] <span>as</span> table_view, 
    <span>case</span> <span>when</span> t.[<span>type</span>] = <span>'U'</span> <span>then</span> <span>'Table'</span>
        <span>when</span> t.[<span>type</span>] = <span>'V'</span> <span>then</span> <span>'View'</span>
        <span>end</span> <span>as</span> [object_type],
    <span>case</span> <span>when</span> c.[<span>type</span>] = <span>'PK'</span> <span>then</span> <span>'Primary key'</span>
        <span>when</span> c.[<span>type</span>] = <span>'UQ'</span> <span>then</span> <span>'Unique constraint'</span>
        <span>end</span> <span>as</span> constraint_type, 
    c.[<span>name</span>] <span>as</span> constraint_name
<span>from</span> sys.objects t
    <span>left</span> <span>outer</span> <span>join</span> sys.indexes i
        <span>on</span> t.object_id = i.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.key_constraints c
        <span>on</span> i.object_id = c.parent_object_id 
        <span>and</span> i.index_id = c.unique_index_id
   <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                    <span>from</span> sys.index_columns ic
                        <span>inner</span> <span>join</span> sys.columns <span>col</span>
                            <span>on</span> ic.object_id = col.object_id
                            <span>and</span> ic.column_id = col.column_id
                    <span>where</span> ic.object_id = t.object_id
                        <span>and</span> ic.index_id = i.index_id
                            <span>order</span> <span>by</span> col.column_id
                            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
<span>where</span> is_unique = <span>1</span>
<span>and</span> t.is_ms_shipped &lt;&gt; <span>1</span>
<span>order</span> <span>by</span> i.[<span>name</span>]
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